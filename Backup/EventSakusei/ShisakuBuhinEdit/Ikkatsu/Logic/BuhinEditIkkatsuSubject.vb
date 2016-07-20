Imports EventSakusei.ShisakuBuhinEdit.Logic.Detect
Imports EventSakusei.ShisakuBuhinEdit.Ikkatsu.Dao
Imports EventSakusei.ShisakuBuhinEdit.Kosei.Logic
Imports ShisakuCommon
Imports ShisakuCommon.Util.LabelValue
Imports ShisakuCommon.Util
Imports ShisakuCommon.Db.EBom.Vo
Imports EventSakusei.ShisakuBuhinEdit.Logic


Namespace ShisakuBuhinEdit.Ikkatsu.Logic
    ''' <summary>
    ''' 部品構成呼出（一括設定）画面の表示全般を担うクラス
    ''' </summary>
    ''' <remarks></remarks>
    Public Class BuhinEditIkkatsuSubject : Inherits Observable
        ''' このクラスを元にSourceSelectorSubjectを作成したのでこのクラスへ修正を加えた場合
        ''' SourceSelectorSubjectも修正をする必要がある可能性がある。

        Private ReadOnly blockKeyVo As TShisakuSekkeiBlockVo
        Private ReadOnly koseiInstlTitle As BuhinEditKoseiInstlTitle
        Private ReadOnly ikkatsuDao As BuhinEditIkkatsuDao
        Private ReadOnly detectStructure As DetectLatestStructure
        Private instlVoList As List(Of TShisakuSekkeiBlockInstlVo)

        Private _records As New IndexedList(Of BuhinEditIkkatsuVo)

        ''' <summary>
        ''' コンストラクタ
        ''' </summary>
        ''' <param name="blockKeyVo">試作設計ブロック情報（キー情報）</param>
        ''' <param name="koseiInstlTitle">INSTL品番情報</param>
        ''' <remarks></remarks>
        Public Sub New(ByVal blockKeyVo As TShisakuSekkeiBlockVo, ByVal koseiInstlTitle As BuhinEditKoseiInstlTitle)
            Me.New(blockKeyVo, koseiInstlTitle, New BuhinEditIkkatsuDaoImpl, New DetectLatestStructureImpl(blockKeyVo))
        End Sub
        ''' <summary>
        ''' コンストラクタ
        ''' </summary>
        ''' <param name="blockKeyVo">試作設計ブロック情報（キー情報）</param>
        ''' <param name="koseiInstlTitle">INSTL品番情報</param>
        ''' <param name="ikkatsuDao">部品構成呼出（一括設定）用のDao</param>
        ''' <param name="detectStructure">構成情報探索メソッド</param>
        ''' <remarks></remarks>
        Public Sub New(ByVal blockKeyVo As TShisakuSekkeiBlockVo, ByVal koseiInstlTitle As BuhinEditKoseiInstlTitle, ByVal ikkatsuDao As BuhinEditIkkatsuDao, ByVal detectStructure As DetectLatestStructure)
            Me.blockKeyVo = blockKeyVo
            Me.koseiInstlTitle = koseiInstlTitle
            Me.ikkatsuDao = ikkatsuDao
            Me.detectStructure = detectStructure

            EzUtil.AssertParameterIsNotNull(blockKeyVo, "blockKeyVo")
            EzUtil.AssertParameterIsNotNull(blockKeyVo.ShisakuEventCode, "blockKeyVo")
            EzUtil.AssertParameterIsNotNull(blockKeyVo.ShisakuBukaCode, "blockKeyVo.ShisakuBukaCode")
            EzUtil.AssertParameterIsNotNull(blockKeyVo.ShisakuBlockNo, "blockKeyVo.ShisakuBlockNo")
            EzUtil.AssertParameterIsNotNull(blockKeyVo.ShisakuBlockNoKaiteiNo, "blockKeyVo.ShisakuBlockNoKaiteiNo")

            For Each columnIndex As Integer In koseiInstlTitle.GetInputInstlHinbanColumnIndexes
                _records(columnIndex).InstlHinban = koseiInstlTitle.InstlHinban(columnIndex)
                _records(columnIndex).InstlHinbanKbn = koseiInstlTitle.InstlHinbanKbn(columnIndex)
                ''↓↓2014/08/21 1)EBOM-新試作手配システム過去データの組み合わせ抽出_⑥VI) (TES)張 ADD BEGIN
                _records(columnIndex).InstlDataKbn = koseiInstlTitle.InstlDataKbn(columnIndex)
                ''↑↑2014/08/21 1)EBOM-新試作手配システム過去データの組み合わせ抽出_⑥VI) (TES)張 ADD END
                '↓↓2014/10/20 酒井 ADD BEGIN
                _records(columnIndex).BaseInstlFlg = koseiInstlTitle.BaseInstlFlg(columnIndex)
                '↑↑2014/10/20 酒井 ADD END
            Next
            SetChanged()
        End Sub

#Region "Recordの各プロパティdelegate"
        Public Property InstlHinban(ByVal rowIndex As Integer) As String
            Get
                Return _records(rowIndex).InstlHinban
            End Get
            Set(ByVal value As String)
                _records(rowIndex).InstlHinban = value
            End Set
        End Property

        Public Property InstlHinbanKbn(ByVal rowIndex As Integer) As String
            Get
                Return _records(rowIndex).InstlHinbanKbn
            End Get
            Set(ByVal value As String)
                _records(rowIndex).InstlHinbanKbn = value
            End Set
        End Property
        ''↓↓2014/08/21 1)EBOM-新試作手配システム過去データの組み合わせ抽出_⑥V(10)) (TES)張 ADD BEGIN
        Public Property InstlDataKbn(ByVal rowIndex As Integer) As String
            Get
                Return _records(rowIndex).InstlDataKbn
            End Get
            Set(ByVal value As String)
                _records(rowIndex).InstlDataKbn = value
            End Set
        End Property
        ''↑↑2014/08/21 1)EBOM-新試作手配システム過去データの組み合わせ抽出_⑥V(10)) (TES)張 ADD END
        '↓↓2014/10/20 酒井 ADD BEGIN
        Public Property BaseInstlFlg(ByVal rowIndex As Integer) As String
            Get
                Return _records(rowIndex).BaseInstlFlg
            End Get
            Set(ByVal value As String)
                _records(rowIndex).BaseInstlFlg = value
            End Set
        End Property
        '↑↑2014/10/20 酒井 ADD END
        Public Property BaseHinban(ByVal rowIndex As Integer) As String
            Get
                Return _records(rowIndex).BaseHinban
            End Get
            Set(ByVal value As String)
                If EzUtil.IsEqualIfNull(_records(rowIndex).BaseHinban, value) Then
                    Return
                End If
                _records(rowIndex).BaseHinban = value
                SetChanged()

                If value = Nothing Then
                    _records(rowIndex).YobidashiMoto = Nothing
                    'BaseStructureResult(rowIndex) = Nothing
                    _records(rowIndex).StructureResult = Nothing
                    Return
                End If

                BaseStructureResult(rowIndex) = DetectStructureResult(rowIndex)
                '入力されたら区分を探しに行く'
                '自身のイベントを除いて試作区分を探す'
                instlVoList = ikkatsuDao.FindBySekkeiBlockInstlKbn(blockKeyVo.ShisakuEventCode, value)

                If instlVoList.Count > 0 Then
                    BaseHinbanKbn(rowIndex) = instlVoList(0).InstlHinbanKbn
                    BaseHinbanKbnLabelValues(rowIndex) = GetLabelValues_BaseHinbanKbn(rowIndex, value)
                Else
                    BaseHinbanKbnLabelValues(rowIndex) = New List(Of LabelValueVo)
                    BaseHinbanKbn(rowIndex) = ""
                End If


                '区分をラベルに格納する'
                'BaseHinbanKbn(rowIndex) = BaseHinbanKbn(rowIndex)


                YobidashiMotoLabelValues(rowIndex) = GetLabelValues_StructureResults(rowIndex)
                YobidashiMoto(rowIndex) = YobidashiMotoLabelValues(rowIndex)(0).Value


            End Set
        End Property

        Private Shared ReadOnly EMPTY_LIST As New List(Of LabelValueVo)
        Private Function GetLabelValues_BaseHinbanKbn(ByVal rowIndex As Integer, ByVal InstlHinban As String) As List(Of LabelValueVo)
            'If BaseStructureResult(rowIndex).IsShisaku AndAlso BaseStructureResult(rowIndex).InstlVo IsNot Nothing Then
            '    Return LabelValueExtracter(Of TShisakuSekkeiBlockInstlVo).Extract(BaseHinbanKbnInstlVos(rowIndex), New InstlHinbanKbnExtractRule)
            'End If
            'If BaseHinbanKbn(rowIndex) Is String.Empty Then
            '    Return New List(Of LabelValueVo)
            'End If
            Dim vo As New TShisakuSekkeiBlockInstlVo
            vo.InstlHinbanKbn = BaseHinbanKbn(rowIndex)
            Dim results As List(Of LabelValueVo) = _
                LabelValueExtracter(Of TShisakuSekkeiBlockInstlVo).Extract(ikkatsuDao.FindBySekkeiBlockInstlKbn(blockKeyVo.ShisakuEventCode, InstlHinban), New InstlHinbanKbnExtractRule)
            results.Sort(New LabelValueComparer)
            Return results

            'Return LabelValueExtracter(Of TShisakuSekkeiBlockInstlVo).Extract(BaseHinbanKbnInstlVos(rowIndex), New InstlHinbanKbnExtractRule)
            'Return EMPTY_LIST
        End Function
        Private Function GetLabelValues_StructureResults(ByVal rowIndex As Integer) As List(Of LabelValueVo)
            Dim result As New List(Of LabelValueVo)
            Dim stdList As SortedList = DetectStructureResults(rowIndex)
            If stdList.Count = 0 Then
                Dim lv As New LabelValueVo("データ無し", "000")
                result.Add(lv)
                'Return EMPTY_LIST
            Else
                '強制的に先に図面データを表示させてしまう
                If stdList.Contains("0532") Then
                    Dim lv As New LabelValueVo(stdList("0532"), "0532")
                    result.Add(lv)
                    stdList.Remove("0532")
                End If
                For Each item In stdList
                    Dim lv As New LabelValueVo(item.value, item.key)
                    result.Add(lv)
                Next
            End If

            Return result
        End Function

        Public Property BaseHinbanKbn(ByVal rowIndex As Integer) As String
            Get
                Return _records(rowIndex).BaseHinbanKbn
            End Get
            Set(ByVal value As String)
                If EzUtil.IsEqualIfNull(_records(rowIndex).BaseHinbanKbn, value) AndAlso Not HasChanged() Then
                    Return
                End If
                '2012/01/16 BaseHinbanKbnLabelValuesがnullの場合があるので緊急的に配置 樺澤'
                If BaseHinbanKbnLabelValues(rowIndex) IsNot Nothing Then
                    If 0 < BaseHinbanKbnLabelValues(rowIndex).Count Then
                        _records(rowIndex).BaseHinbanKbn = IIf(ExistValueIgnoreNull(BaseHinbanKbnLabelValues(rowIndex), value), value, BaseHinbanKbnLabelValues(rowIndex)(0).Value)
                        ''''ここで抽出元の選択肢を呼び出す
                        'BaseStructureResult(rowIndex) = NewStructureResultByKbn(rowIndex)
                        BaseStructureResult(rowIndex) = DetectStructureResult(rowIndex)
                        BaseHinbanKbnLabelValues(rowIndex) = GetLabelValues_BaseHinbanKbn(rowIndex, BaseHinban(rowIndex))
                        'BaseHinbanKbn(rowIndex) = BaseHinbanKbn(rowIndex)

                        'ここでEBOMかどうかは必要ないと思われる為修正
                        '2012/03/6
                        YobidashiMotoLabelValues(rowIndex) = GetLabelValues_StructureResults(rowIndex)
                        YobidashiMoto(rowIndex) = YobidashiMotoLabelValues(rowIndex)(0).Value
                        '試作の場合
                        'If BaseStructureResult(rowIndex).IsEBom Then
                        '    'ベースの構成がEBOMの場合
                        '    YobidashiMotoLabelValues(rowIndex) = GetLabelValues_StructureResults(rowIndex)
                        'Else
                        '    '試作の場合
                        '    YobidashiMotoLabelValues(rowIndex) = GetLabelValues_StructureResults(rowIndex)
                        '    YobidashiMoto(rowIndex) = YobidashiMotoLabelValues(rowIndex)(0).Value
                        '    'If YobidashiMotoLabelValues(rowIndex).Count = 1 And YobidashiMotoLabelValues(rowIndex)(0).Value = "000" Then
                        '    '    YobidashiMoto(rowIndex) = ""
                        '    'Else
                        '    '    YobidashiMoto(rowIndex) = YobidashiMotoLabelValues(rowIndex)(0).Value
                        '    'End If
                        'End If
                    Else
                        _records(rowIndex).BaseHinbanKbn = Nothing
                    End If
                Else
                    _records(rowIndex).BaseHinbanKbn = Nothing
                End If
                SetChanged()
            End Set
        End Property
        Public Property YobidashiMoto(ByVal rowIndex As Integer) As String
            Get
                Return _records(rowIndex).YobidashiMoto

            End Get
            Set(ByVal value As String)
                If YobidashiMotoLabelValues(rowIndex) IsNot Nothing Then
                    If YobidashiMotoLabelValues(rowIndex).Count = 1 And YobidashiMotoLabelValues(rowIndex)(0).Value = "000" Then
                        _records(rowIndex).YobidashiMoto = IIf(ExistValueIgnoreNull(YobidashiMotoLabelValues(rowIndex), value), value, YobidashiMotoLabelValues(rowIndex)(0).Value)
                        '_records(rowIndex).YobidashiMoto = Nothing
                        BaseStructureResult(rowIndex) = NewStructureResultByYobidashiMoto(rowIndex)
                    Else
                        _records(rowIndex).YobidashiMoto = IIf(ExistValueIgnoreNull(YobidashiMotoLabelValues(rowIndex), value), value, YobidashiMotoLabelValues(rowIndex)(0).Value)
                        ''''ここで抽出元の選択肢を呼び出す
                        BaseStructureResult(rowIndex) = NewStructureResultByYobidashiMoto(rowIndex)
                    End If
                End If

            End Set
        End Property


        Private Function NewStructureResultByYobidashiMoto(ByVal rowIndex As Integer) As StructureResult
            Select Case _records(rowIndex).YobidashiMoto
                Case "0532"
                    '図面構成（標題欄情報）
                    Return detectStructure.Compute(BaseHinban(rowIndex), Nothing, True, "0532")
                Case "0530"
                    '過去データ
                    Return detectStructure.Compute(BaseHinban(rowIndex), Nothing, True, "0530")
                Case Else
                    If Left(_records(rowIndex).YobidashiMoto, 4) = "0533" Then
                        '開発管理表（構想）
                        Dim kaihatsuFugo As String
                        kaihatsuFugo = Split(_records(rowIndex).YobidashiMoto, "-")(1)
                        Return detectStructure.Compute(BaseHinban(rowIndex), Nothing, True, "0533", kaihatsuFugo)
                    Else
                        '試作
                        '2012/03/05 BaseHinbanKbnがNothingの場合""でイベントを検索するように修正
                        Dim shisakuKbn As String = ""
                        If StringUtil.IsEmpty(_records(rowIndex).BaseHinbanKbn) Then
                            shisakuKbn = ""
                        Else
                            shisakuKbn = _records(rowIndex).BaseHinbanKbn
                        End If
                        BaseHinbanKbnInstlVos(rowIndex) = ikkatsuDao.FindShisakuEventByInstlHinbanAndKbn(BaseHinban(rowIndex), blockKeyVo.ShisakuEventCode, blockKeyVo.ShisakuBukaCode, blockKeyVo.ShisakuBlockNo, blockKeyVo.ShisakuBlockNoKaiteiNo, shisakuKbn)
                        '---------------------------------------------------
                        ''BaseHinbanKbnInstlVos(rowIndex) = sourceSelectorDao.FindLatestInstlHinbanKbnBy(BaseHinban(rowIndex), blockKeyVo.ShisakuEventCode, blockKeyVo.ShisakuBukaCode, blockKeyVo.ShisakuBlockNo, blockKeyVo.ShisakuBlockNoKaiteiNo)
                        'BaseHinbanKbnInstlVos(rowIndex) = ikkatsuDao.FindShisakuEventByInstlHinbanAndKbn(BaseHinban(rowIndex), blockKeyVo.ShisakuEventCode, blockKeyVo.ShisakuBukaCode, blockKeyVo.ShisakuBlockNo, blockKeyVo.ShisakuBlockNoKaiteiNo, _records(rowIndex).BaseHinbanKbn)
                        '---------------------------------------------------
                        For Each vo As TShisakuSekkeiBlockInstlVo In BaseHinbanKbnInstlVos(rowIndex)
                            'If vo.ShisakuEventCode = YobidashiMoto(rowIndex) And _records(rowIndex).BaseHinbanKbn.Equals(vo.InstlHinbanKbn) Then
                            If vo.ShisakuEventCode = YobidashiMoto(rowIndex) Then
                                Return StructureResult.NewShisaku(vo)
                            End If
                            'End If
                        Next
                        '2012/02/26 データが無かった場合、新しい設計ブロックINSTLを作成し、
                        'INSTL品番と品番区分を入れてNewShisakuに渡す
                        Dim emptyVo As New TShisakuSekkeiBlockInstlVo()
                        emptyVo.InstlHinban = _records(rowIndex).BaseHinban
                        emptyVo.InstlHinbanKbn = _records(rowIndex).BaseHinbanKbn
                        Return StructureResult.NewNotExist(emptyVo.InstlHinban, emptyVo.InstlHinbanKbn)
                        'Return StructureResult.NewShisaku(emptyVo, YobidashiMoto(rowIndex))
                    End If
            End Select
            'Return StructureResult.NewShisaku(BaseHinbanKbnInstlVos(rowIndex)(0))
            'For Each vo As TShisakuSekkeiBlockInstlVo In BaseHinbanKbnInstlVos(rowIndex)
            '    If _records(rowIndex).BaseHinbanKbn.Equals(vo.InstlHinbanKbn) Then
            '        Return StructureResult.NewShisaku(vo)
            '    End If
            'Next
            Throw New InvalidProgramException("区分に一致するべきなのに一致しなかった")
        End Function
        Private Function NewStructureResultByKbn(ByVal rowIndex As Integer) As StructureResult
            For Each vo As TShisakuSekkeiBlockInstlVo In BaseHinbanKbnInstlVos(rowIndex)
                If _records(rowIndex).BaseHinbanKbn.Equals(vo.InstlHinbanKbn) Then
                    Return StructureResult.NewShisaku(vo)
                End If
            Next
            Throw New InvalidProgramException("区分に一致するべきなのに一致しなかった")
        End Function

        Public Property BaseHinbanKbnLabelValues(ByVal rowIndex As Integer) As List(Of LabelValueVo)
            Get
                Return _records(rowIndex).BaseHinbanKbnLabelValues
            End Get
            Set(ByVal value As List(Of LabelValueVo))
                _records(rowIndex).BaseHinbanKbnLabelValues = value
            End Set
        End Property

        Public Property YobidashiMotoLabelValues(ByVal rowIndex As Integer) As List(Of LabelValueVo)
            Get
                Return _records(rowIndex).YobidashiMotoLabelValues
            End Get
            Set(ByVal value As List(Of LabelValueVo))
                _records(rowIndex).YobidashiMotoLabelValues = value
            End Set
        End Property
        Private Property BaseHinbanKbnInstlVos(ByVal rowIndex As Integer) As List(Of TShisakuSekkeiBlockInstlVo)
            Get
                Return _records(rowIndex).BaseHinbanKbnInstlVos
            End Get
            Set(ByVal value As List(Of TShisakuSekkeiBlockInstlVo))
                _records(rowIndex).BaseHinbanKbnInstlVos = value
            End Set
        End Property
        ''↓↓2014/08/21 1)EBOM-新試作手配システム過去データの組み合わせ抽出_⑥V(13)) (TES)張 ADD BEGIN
        Private Property BaseDataKbnInstlVos(ByVal rowIndex As Integer) As List(Of TShisakuSekkeiBlockInstlVo)
            Get
                Return _records(rowIndex).BaseHinbanKbnInstlVos
            End Get
            Set(ByVal value As List(Of TShisakuSekkeiBlockInstlVo))
                _records(rowIndex).BaseHinbanKbnInstlVos = value
            End Set
        End Property
        ''↑↑2014/08/21 1)EBOM-新試作手配システム過去データの組み合わせ抽出_⑥V(13)) (TES)張 ADD END
        Private Property BaseStructureResult(ByVal rowIndex As Integer) As StructureResult
            Get
                Return _records(rowIndex).StructureResult
            End Get
            Set(ByVal value As StructureResult)
                _records(rowIndex).StructureResult = value
            End Set
        End Property

        Private Function DetectStructureResults(ByVal rowIndex As Integer) As SortedList
            Dim stdList As New SortedList

            Dim buhinNo As String = BaseHinban(rowIndex)
            Dim buhinNoKbn As String = BaseHinbanKbn(rowIndex)

            If StringUtil.IsEmpty(buhinNo) Then
                Return Nothing
            End If
            If StringUtil.IsEmpty(buhinNoKbn) Then
                buhinNoKbn = ""
            End If

            If StringUtil.IsEmpty(buhinNoKbn) Then
                'If buhinNoKbn Is Nothing Then
                '0532を見る
                Dim result0532 As StructureResult = detectStructure.Compute(ShisakuRule.Conv0532Hinban(buhinNo), Nothing, True, "0532")
                If Not result0532 Is Nothing Then
                    stdList.Add("0532", "図面データ")
                End If
                '0533を見る
                Dim resultKaihatsuFugo As List(Of TShisakuEventVo) = ikkatsuDao.FindKaihatsuFugoOf553ByInstlHinban(ShisakuRule.Conv0532Hinban(buhinNo))
                For Each vo As TShisakuEventVo In resultKaihatsuFugo
                    Dim result0533 As StructureResult = detectStructure.Compute(buhinNo, Nothing, True, "0533", vo.ShisakuKaihatsuFugo)
                    If Not result0533 Is Nothing Then
                        stdList.Add("0533-" & vo.ShisakuKaihatsuFugo, "構成編集ツール" & "(" & vo.ShisakuKaihatsuFugo & ")")
                    End If
                Next
            End If
            ''0530を見る
            'Dim result0530 As StructureResult = detectStructure.Compute(buhinNo, Nothing, True, "0530")
            'If Not result0530 Is Nothing Then
            '    ht.Add("0530", "過去データ")
            'End If
            If stdList.Count > 0 Then
                Return stdList
            End If

            'Else
            'BaseHinbanKbnInstlVos(rowIndex) = sourceSelectorDao.FindLatestInstlHinbanKbnBy(buhinNo, blockKeyVo.ShisakuEventCode, blockKeyVo.ShisakuBukaCode, blockKeyVo.ShisakuBlockNo, blockKeyVo.ShisakuBlockNoKaiteiNo)
            BaseHinbanKbnInstlVos(rowIndex) = ikkatsuDao.FindShisakuEventByInstlHinbanAndKbn(buhinNo, blockKeyVo.ShisakuEventCode, blockKeyVo.ShisakuBukaCode, blockKeyVo.ShisakuBlockNo, blockKeyVo.ShisakuBlockNoKaiteiNo, buhinNoKbn)
            If 0 < BaseHinbanKbnInstlVos(rowIndex).Count Then
                For Each vo As TShisakuSekkeiBlockInstlVo In BaseHinbanKbnInstlVos(rowIndex)
                    'If buhinNoKbn.Equals(vo.InstlHinbanKbn) Then
                    Dim eventVo As TShisakuEventVo = ikkatsuDao.FindShisakuEventName(vo.ShisakuEventCode)
                    If stdList.ContainsKey(eventVo.ShisakuEventCode) Then Continue For
                    stdList.Add(eventVo.ShisakuEventCode, eventVo.ShisakuEventCode & "(" & eventVo.ShisakuKaihatsuFugo & eventVo.ShisakuEventName & ")")
                    'End If
                Next
                Return stdList
            End If
            'End If
            Return stdList
        End Function

        Private Function DetectStructureResult(ByVal rowIndex As Integer) As StructureResult
            BaseHinbanKbnInstlVos(rowIndex) = Nothing

            Dim buhinNo As String = BaseHinban(rowIndex)

            If StringUtil.IsEmpty(buhinNo) Then
                Return Nothing
            End If

            '2012/02/26 
            Dim buhinNoKbn As String = BaseHinbanKbn(rowIndex)
            If StringUtil.IsEmpty(buhinNoKbn) Then
                buhinNoKbn = ""
            End If

            '2012/02/21  区分にNothingは存在しない'
            'Dim result As StructureResult = detectStructure.Compute(buhinNo, buhinNoKbn, True)
            Dim result As StructureResult = detectStructure.ComputeEK(buhinNo, buhinNoKbn)

            Return result

            'If result.IsEBom Then   ' 0532に存在した
            '    If result.IsShisaku Then

            '    End If
            '    'Return result
            'Else

            'End If

            ' この時点で、result.IsShisaku = True の場合もある
            '   1) INSTL品番区分が、Emptyで、INSTL情報が登録されていた場合
            '   2) TShisakuBuhinEdit に存在した場合

            ' 1) は、Empty以外の区分が隠れている可能性があるから、再検索
            ' 2) だとしても、先に、INSTL情報を検索して次に、TShisakuBuhinEdit

            BaseHinbanKbnInstlVos(rowIndex) = ikkatsuDao.FindLatestInstlHinbanKbnBy(buhinNo, blockKeyVo.ShisakuEventCode, blockKeyVo.ShisakuBukaCode, blockKeyVo.ShisakuBlockNo, blockKeyVo.ShisakuBlockNoKaiteiNo)
            If 0 < BaseHinbanKbnInstlVos(rowIndex).Count Then
                Return StructureResult.NewShisakuEBOM(BaseHinbanKbnInstlVos(rowIndex)(0))
            End If

            Return result
        End Function

        Private Class InstlHinbanKbnExtractRule : Implements ILabelValueExtraction
            Public Sub Extraction(ByVal aLocator As LabelValueLocator) Implements ILabelValueExtraction.Extraction
                Dim vo As New TShisakuSekkeiBlockInstlVo
                aLocator.IsA(vo).Label(vo.InstlHinbanKbn).Value(vo.InstlHinbanKbn)
            End Sub
        End Class
        Private Class StructureResultsExtractRule : Implements ILabelValueExtraction
            Public Sub Extraction(ByVal aLocator As LabelValueLocator) Implements ILabelValueExtraction.Extraction
                Dim ht As New Hashtable
                aLocator.IsA(ht).Label(ht.Keys).Value(ht.Values)
            End Sub
        End Class
#End Region

        ''' <summary>
        ''' ラベルと値の一覧に、検索値があるかを返す(検索値がNullなら常にfalse)
        ''' </summary>
        ''' <param name="labelValues">ラベルと値の一覧</param>
        ''' <param name="value">検索値</param>
        ''' <returns>ある場合、true(検索値がNullなら常にfalse)</returns>
        ''' <remarks></remarks>
        Public Function ExistValueIgnoreNull(ByVal labelValues As List(Of LabelValueVo), ByVal value As String) As Boolean
            If value Is Nothing Then
                Return False
            End If
            Return ExistValue(labelValues, value)
        End Function
        ''' <summary>
        ''' ラベルと値の一覧に、検索値があるかを返す
        ''' </summary>
        ''' <param name="labelValues">ラベルと値の一覧</param>
        ''' <param name="value">検索値</param>
        ''' <returns>ある場合、true</returns>
        ''' <remarks></remarks>
        Public Function ExistValue(ByVal labelValues As List(Of LabelValueVo), ByVal value As String) As Boolean
            If labelValues Is Nothing Then
                Return False
            End If
            For Each vo As LabelValueVo In labelValues
                If vo.Value IsNot Nothing AndAlso vo.Value.Equals(value) Then
                    Return True
                End If
            Next
            Return False
        End Function

        ''' <summary>
        ''' 編集モードかを返す
        ''' </summary>
        ''' <param name="rowNo">行No</param>
        ''' <returns>編集モードなら、true</returns>
        ''' <remarks></remarks>
        Public Function IsEditModes(ByVal rowNo As Integer) As Boolean
            ' E-Bomデータだったら編集不可
            Return Not (koseiInstlTitle.GetStructureResult(rowNo).IsExist AndAlso koseiInstlTitle.GetStructureResult(rowNo).IsEBom)
        End Function

        ''' <summary>
        ''' 入力中の行indexを返す
        ''' </summary>
        ''' <returns>行index</returns>
        ''' <remarks></remarks>
        Public Function GetRowIndexes() As ICollection(Of Integer)
            Dim result As New List(Of Integer)(_records.Keys)
            result.Sort()
            Return result
        End Function

        ''' <summary>
        ''' 有効な「構成の情報」を返す
        ''' </summary>
        ''' <returns>行index</returns>
        ''' <remarks></remarks>
        Public Function GetValidatedStructureResult() As IndexedList(Of StructureResult)
            Dim results As New IndexedList(Of StructureResult)(False)
            For Each rowIndex As Integer In _records.Keys
                If BaseStructureResult(rowIndex) Is Nothing Then
                    Continue For
                End If
                results(rowIndex) = BaseStructureResult(rowIndex)
            Next
            Return results
        End Function
    End Class
End Namespace