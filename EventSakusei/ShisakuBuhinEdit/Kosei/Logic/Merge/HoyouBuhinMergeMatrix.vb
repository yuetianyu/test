Imports EventSakusei.ShisakuBuhinEdit.Kosei.Logic.Matrix
Imports ShisakuCommon

Namespace ShisakuBuhinEdit.Kosei.Logic.Merge

    ''' <summary>
    ''' BuhinKoseiMatix クラスを基に、マージするメソッドクラス
    ''' </summary>
    ''' <remarks></remarks>
    Friend Class HoyouBuhinMergeMatrix

        Private indexStack As ExpandedIndexStack
        Private columnIndexes As List(Of Integer)
        ''' <summary>公開プロパティ</summary>
        Friend AProperty As New Prop

        ''' <summary>
        ''' コンストラクタ
        ''' </summary>
        ''' <remarks></remarks>
        Public Sub New()
            Me.new(New HoyouBuhinBuhinKoseiMatrix)
        End Sub
        ''' <summary>
        ''' コンストラクタ
        ''' </summary>
        ''' <param name="matrix">部品表</param>
        ''' <remarks></remarks>
        Public Sub New(ByVal matrix As HoyouBuhinBuhinKoseiMatrix)
            Me._matrix = matrix
        End Sub

#Region "Matrix関連"
        Private _matrix As HoyouBuhinBuhinKoseiMatrix
        ''' <summary>
        ''' 部品表の行情報
        ''' </summary>
        ''' <param name="rowIndex">行index</param>
        Private Property Record(ByVal rowIndex As Integer) As HoyouBuhinBuhinKoseiRecordVo
            Get
                Return _matrix.Record(rowIndex)
            End Get
            Set(ByVal value As HoyouBuhinBuhinKoseiRecordVo)
                _matrix.Record(rowIndex) = value
            End Set
        End Property

        Private ReadOnly Property RecordIndexes() As ICollection(Of Integer)
            Get
                Return _matrix.GetInputRowIndexes
            End Get
        End Property

        Private ReadOnly Property RecordNewIndex() As Integer
            Get
                Return _matrix.GetNewRowIndex
            End Get
        End Property

        Private ReadOnly Property InsuColumnIndexes(ByVal rowIndex As Integer) As ICollection(Of Integer)
            Get
                Return _matrix.GetInputInsuColumnIndexesOnRow(rowIndex)
            End Get
        End Property

        Private Property InsuVo(ByVal rowIndex As Integer, ByVal columnIndex As Integer) As BuhinKoseiInsuCellVo
            Get
                Return _matrix.InsuVo(rowIndex, columnIndex)
            End Get
            Set(ByVal value As BuhinKoseiInsuCellVo)
                _matrix.InsuVo(rowIndex, columnIndex) = value
            End Set
        End Property

        Private Sub InsertRecord(ByVal insertIndex As Integer)
            _matrix.InsertRow(insertIndex)
        End Sub

        Private Function GetParentRecords(ByVal rowIndex As Integer) As HoyouBuhinBuhinKoseiRecordVo()
            Return _matrix.GetParentRecords(rowIndex)
        End Function
#End Region


        ''' <summary>
        ''' レベル・挿入indexだけのクラス
        ''' </summary>
        Private Class LevelIndex
            Public Level As Integer
            Public Index As Integer
            Public Sub New(ByVal level As Integer, ByVal index As Integer)
                Me.Level = level
                Me.Index = index
            End Sub
        End Class

        ''' <summary>
        ''' 展開中の親レベルの挿入index管理を担うクラス
        ''' </summary>
        ''' <remarks></remarks>
        Private Class ExpandedIndexStack
            Public RootLevel As Integer = 0
            Private indexStack As New Stack(Of LevelIndex)
            ''' <summary>
            ''' 挿入したNodeのレベル・位置をPush
            ''' </summary>
            ''' <param name="level">レベル</param>
            ''' <param name="index">挿入index</param>
            ''' <remarks></remarks>
            Public Sub Push(ByVal level As Integer, ByVal index As Integer)
                indexStack.Push(New LevelIndex(level, index))
            End Sub
            ''' <summary>
            ''' 直前の位置indexを返す
            ''' </summary>
            ''' <returns>直前の位置index</returns>
            ''' <remarks></remarks>
            Public Function GetLastIndex() As Integer
                If indexStack.Count = 0 Then
                    Return -1
                End If
                Return indexStack.Peek.Index
            End Function
            ''' <summary>
            ''' 直前のレベルを返す
            ''' </summary>
            ''' <returns>直前のレベル</returns>
            ''' <remarks></remarks>
            Public Function GetLastLevel() As Integer
                If indexStack.Count = 0 Then
                    Return -1
                End If
                Return indexStack.Peek.Level
            End Function
            ''' <summary>
            ''' Stackを、親NodeまでPopして、親の挿入indexを返す
            ''' </summary>
            ''' <param name="mySelfLevel">自分のレベル</param>
            ''' <returns>親の挿入index</returns>
            ''' <remarks></remarks>
            Public Function PopParentIndex(ByVal mySelfLevel As Integer) As Integer
                If mySelfLevel = RootLevel Then
                    Return -1
                End If
                While mySelfLevel <= indexStack.Peek.Level
                    indexStack.Pop()
                End While
                Return indexStack.Peek.Index
            End Function
        End Class

        ''' <summary>
        ''' 先頭からマージするための初期化
        ''' </summary>
        ''' <remarks></remarks>
        Friend Sub InitializeMergeTop()
            InitializeMergeTop(Nothing)
        End Sub
        ''' <summary>
        ''' 先頭からマージするための初期化
        ''' </summary>
        ''' <param name="rootLevel">これからマージする列の先頭の（基点となる）レベル</param>
        ''' <remarks></remarks>
        Friend Sub InitializeMergeTop(ByVal rootLevel As Integer?)
            indexStack = New ExpandedIndexStack
            indexStack.RootLevel = IIf(rootLevel Is Nothing, 0, rootLevel)
            columnIndexes = New List(Of Integer)(_matrix.GetInputInsuColumnIndexes())
        End Sub

        ''' <summary>
        ''' (Matrix上の)最初の列indexかを返す
        ''' </summary>
        ''' <param name="columnIndex">列index</param>
        ''' <returns>判定結果</returns>
        ''' <remarks></remarks>
        Private Function IsFirstColumnIndex(ByVal columnIndex) As Boolean
            Dim count As Integer = columnIndexes.Count
            Return count = 0 OrElse (count = 1 And columnIndexes(0) = columnIndex)
        End Function

        ''' <summary>
        ''' マージする(メソッドクラス本体)
        ''' </summary>
        ''' <param name="columnIndex">マージ追加する列を表すindex</param>
        ''' <param name="aData">マージする値</param>
        ''' <remarks></remarks>
        Friend Sub Compute(ByVal columnIndex As Integer, ByVal aData As IData)

            Dim brotherIndex As Integer = -1
            Dim lastSameLevelIndex As Integer = -1
            Dim downLevelIndex As Integer = -1

            '最初の列でもマージを行う'
            If Not IsFirstColumnIndex(columnIndex) Then
                Dim lastIndex As Integer = indexStack.GetLastIndex
                Dim lastLevel As Integer = indexStack.GetLastLevel
                Dim parentIndex As Integer = GetParentIndex(aData)
                Dim changedFromLastLevel As Boolean = False

                For Each rowIndex As Integer In RecordIndexes

                    '親の行より上か、最終行より上'
                    'If rowIndex <= parentIndex OrElse rowIndex < lastIndex Then
                    '    Continue For
                    'End If

                    '親子関係が必要なくなったので'
                    'If rowIndex <= parentIndex Then
                    '    Continue For
                    'End If


                    'レベルが同一ではない'
                    If Not EzUtil.IsEqualIfNull(Record(rowIndex).Level, aData.GetLevel) Then
                        '最終レベルが異なる'
                        If Not EzUtil.IsEqualIfNull(Record(rowIndex).Level, lastLevel) Then
                            changedFromLastLevel = True
                        End If
                        '最終レベル変換フラグかレベルが異なる'
                        If changedFromLastLevel AndAlso Record(rowIndex).Level < aData.GetLevel Then
                            downLevelIndex = rowIndex
                            '2012/01/26 上下の関係を崩してでもマージさせる'
                            'Exit For
                            Continue For
                        Else ' aData.GetLevel < Record(rowIndex).Level か、片方がnull
                            Continue For
                        End If
                    End If

                    '最終同一レベルインデックス'
                    lastSameLevelIndex = rowIndex


                    'If CanMerge(Record(rowIndex), aData) _
                    '                                    AndAlso (aData.GetLevel Is Nothing OrElse aData.GetLevel = 0 _
                    '                                    OrElse (0 < aData.GetLevel AndAlso CanMergeIrregularCase(GetParentRecords(rowIndex), aData.GetParent))) Then

                    'End If

                    'マージ可能かチェック'
                    If CanMerge(Record(rowIndex), aData) _
                       AndAlso (aData.GetLevel Is Nothing OrElse aData.GetLevel = 0 _
                                OrElse (0 < aData.GetLevel)) Then

                        '同一イベント、部課、ブロック、改訂、INSTLで部品番号表示順違いがあったら員数をプラス（マージ）する。
                        'それ以外はＶＯをセット。　2011/03/01　By柳沼

                        If InsuVo(rowIndex, columnIndex).BuhinNoHyoujiJun <> aData.GetInsuVo.BuhinNoHyoujiJun _
                           And InsuVo(rowIndex, columnIndex).InstlHinbanHyoujiJun = aData.GetInsuVo.InstlHinbanHyoujiJun _
                           And InsuVo(rowIndex, columnIndex).ShisakuBlockNo = aData.GetInsuVo.ShisakuBlockNo _
                           And InsuVo(rowIndex, columnIndex).ShisakuBlockNoKaiteiNo = aData.GetInsuVo.ShisakuBlockNoKaiteiNo _
                           And InsuVo(rowIndex, columnIndex).ShisakuBukaCode = aData.GetInsuVo.ShisakuBukaCode _
                           And InsuVo(rowIndex, columnIndex).ShisakuEventCode = aData.GetInsuVo.ShisakuEventCode Then

                            '員数を合計する'
                            InsuVo(rowIndex, columnIndex).InsuSuryo = _
                                      InsuVo(rowIndex, columnIndex).InsuSuryo + aData.GetInsuVo.InsuSuryo

                        Else
                            '員数の位置を変更'
                            InsuVo(rowIndex, columnIndex) = aData.GetInsuVo
                        End If

                        Return

                    Else
                        '柳沼　レングスが７桁以上の時、以下の判断を行う。
                        If Not aData Is Nothing Then
                            If aData.GetBuhinNo().Length >= 7 Then
                                If Record(rowIndex).BuhinNo Is Nothing Then
                                    Record(rowIndex).BuhinNo = "       "
                                End If
                                If Record(rowIndex).BuhinNo.StartsWith(aData.GetBuhinNo().Substring(0, 7)) Then
                                    brotherIndex = rowIndex
                                End If
                            End If
                        End If
                    End If
                Next
            End If


            Dim index As Integer
            If 0 <= brotherIndex Then
                index = brotherIndex + 1
                InsertRecord(index)
            ElseIf 0 <= lastSameLevelIndex AndAlso aData.GetLevel IsNot Nothing Then
                index = lastSameLevelIndex + 1
                InsertRecord(index)
            ElseIf 0 <= downLevelIndex Then
                index = downLevelIndex
                InsertRecord(index)
            Else
                index = RecordNewIndex
            End If

            If aData.GetLevel IsNot Nothing Then
                indexStack.Push(aData.GetLevel, index)
            End If

            Record(index) = New HoyouBuhinBuhinKoseiRecordVo
            '#######################################################################
            '2012/01/12 
            '設計展開処理において、登録対象となるデータがない場合、処理中断
            '#######################################################################
            If aData.GetRecordNewRow Is Nothing Then
                Return
            Else
                VoUtil.CopyProperties(aData.GetRecordNewRow, Record(index))
                InsuVo(index, columnIndex) = aData.GetInsuVo
            End If

        End Sub

        ''' <summary>
        ''' 列毎にソートする
        ''' </summary>
        ''' <param name="columnIndex"></param>
        ''' <remarks></remarks>
        Friend Sub Sort(ByVal columnIndex As Integer)

            For Each rowIndex As Integer In RecordIndexes
                Record(rowIndex).SortFlag = 0
            Next

            For Each rowIndex As Integer In RecordIndexes
                '同レベル内で、上５桁が同一の部品番号を寄せる'
                If Record(rowIndex).Level <> 0 Then
                    For Each rowIndex2 As Integer In RecordIndexes
                        If rowIndex <> rowIndex2 Then

                            If Record(rowIndex2).SortFlag = 1 Then
                                Continue For
                            End If
                            If Record(rowIndex2).Level <> 0 Then
                                If Record(rowIndex).Level = Record(rowIndex2).Level Then
                                    If Record(rowIndex).BuhinNo.Length > 5 AndAlso Record(rowIndex2).BuhinNo.Length > 5 Then
                                        If StringUtil.Equals(Left(Record(rowIndex).BuhinNo, 5), Left(Record(rowIndex2).BuhinNo, 5)) Then
                                            'indexの直下にindex2を置く'
                                            Dim newRecord As New HoyouBuhinBuhinKoseiRecordVo
                                            Dim newInsuVo As New BuhinKoseiInsuCellVo

                                            If columnIndex = 2 Then
                                                If StringUtil.Equals(Record(rowIndex2).BuhinNo, "94210FJ060VH") Then
                                                    Dim b As String = "2"
                                                End If
                                                If StringUtil.Equals(Record(rowIndex2).BuhinNo, "94210FJ070VH") Then
                                                    Dim b As String = "2"
                                                End If
                                            End If

                                            Dim i As Integer = 0

                                            'ソートフラグがあると間に入れない２列目以降が入れない'
                                            If Record(rowIndex + 1).SortFlag = 1 Then
                                                'まだソートしていない行を探す'
                                                For Each rowIndex3 As Integer In RecordIndexes
                                                    If Record(rowIndex).Level <> 0 Then
                                                        If Record(rowIndex3).SortFlag = 0 Then
                                                            i = rowIndex3
                                                            Exit For
                                                        End If
                                                    End If
                                                Next
                                            Else
                                                i = rowIndex + 1
                                            End If

                                            i = rowIndex + 1
                                            'ソートする'
                                            If columnIndex = 0 Then
                                                newRecord = Record(i)
                                                newInsuVo = InsuVo(i, columnIndex)

                                                Record(i) = Record(rowIndex2)
                                                InsuVo(i, columnIndex) = InsuVo(rowIndex2, columnIndex)

                                                Record(rowIndex2) = newRecord
                                                InsuVo(rowIndex2, columnIndex) = newInsuVo

                                                'Record(i).SortFlag = 1
                                            Else
                                                newRecord = Record(i)
                                                Record(i) = Record(rowIndex2)
                                                Record(rowIndex2) = newRecord
                                                For col As Integer = 0 To columnIndex

                                                    newInsuVo = InsuVo(i, col)
                                                    InsuVo(i, col) = InsuVo(rowIndex2, col)
                                                    InsuVo(rowIndex2, col) = newInsuVo

                                                Next
                                            End If


                                        End If
                                    End If
                                End If

                            End If
                        End If
                    Next
                End If
                '基準位置'
                Record(rowIndex).SortFlag = 1
            Next
        End Sub

        Private Function GetParentIndex(ByVal aData As IData) As Integer

            If aData.GetLevel Is Nothing Then
                Return -1
            End If
            Return indexStack.PopParentIndex(aData.GetLevel)
        End Function

        ''' <summary>
        ''' マージする値に対してのinterface
        ''' </summary>
        ''' <remarks></remarks>
        Friend Interface IData
            ''' <summary>
            ''' 新規行の行情報を返す
            ''' </summary>
            ''' <remarks></remarks>
            Function GetRecordNewRow() As HoyouBuhinBuhinKoseiRecordVo
            ''' <summary>
            ''' 親情報を返す
            ''' </summary>
            ''' <returns>親情報</returns>
            ''' <remarks></remarks>
            Function GetParent() As IData
            ''' <summary>
            ''' レベルを返す
            ''' </summary>
            ''' <returns>レベル</returns>
            ''' <remarks></remarks>
            Function GetLevel() As Integer?
            ''' <summary>
            ''' 員数を返す
            ''' </summary>
            ''' <returns>員数</returns>
            ''' <remarks></remarks>
            Function GetInsuVo() As BuhinKoseiInsuCellVo
            ''' <summary>
            ''' 部品番号を返す
            ''' </summary>
            ''' <returns>部品番号</returns>
            ''' <remarks></remarks>
            Function GetBuhinNo() As String
            ''' <summary>
            ''' メーカーコードを返す
            ''' </summary>
            ''' <returns>メーカーコード</returns>
            ''' <remarks></remarks>
            Function GetMakerCode() As String
            ''' <summary>
            ''' 国内集計コードを返す
            ''' </summary>
            ''' <returns>国内集計コード</returns>
            ''' <remarks></remarks>
            Function GetShukeiCode() As String
            ''' <summary>
            ''' 海外SIA集計コードを返す
            ''' </summary>
            ''' <returns>海外SIA集計コード</returns>
            ''' <remarks></remarks>
            Function GetSiaShukeiCode() As String
            ''' <summary>
            ''' 現調CKD区分を返す
            ''' </summary>
            ''' <returns>現調CKD区分</returns>
            ''' <remarks></remarks>
            Function GetGencyoCkdKbn() As String
            ''' <summary>
            ''' 供給セクションを返す
            ''' </summary>
            ''' <returns>供給セクション</returns>
            ''' <remarks>2012/01/23 供給セクション追加</remarks>
            Function GetKyoukuSection() As String
        End Interface

        ''' <summary>外部へ公開する当クラスのプロパティ</summary>
        ''' <remarks></remarks>
        Friend Class Prop
            ''' <summary>親品番違いでもマージする場合、true</summary>
            Public CanMergeIfDefferenceOyaHinban As Boolean
        End Class

        ''' <summary>
        ''' マージ出来るかを返す
        ''' </summary>
        ''' <param name="recordRecordVo">行情報</param>
        ''' <param name="aData">情報Accessor</param>
        ''' <returns>判定結果</returns>
        ''' <remarks></remarks>
        Private Function CanMerge(ByVal recordRecordVo As HoyouBuhinBuhinKoseiRecordVo, ByVal aData As IData) As Boolean

            '「再使用不可」もマージ判断に追加する。
            '2011/03/14 柳沼

            '供給セクションもマージ判断に追加する？？？ 2012/01/23


            'スペースが入っているものが存在する'
            Return EzUtil.IsEqualIfNull(recordRecordVo.Level, aData.GetLevel) _
                    AndAlso EzUtil.IsEqualIfNull(Trim(recordRecordVo.ShukeiCode), Trim(aData.GetShukeiCode)) _
                    AndAlso EzUtil.IsEqualIfNull(Trim(recordRecordVo.SiaShukeiCode), Trim(aData.GetSiaShukeiCode)) _
                    AndAlso EzUtil.IsEqualIfNull(Trim(recordRecordVo.GencyoCkdKbn), Trim(aData.GetGencyoCkdKbn)) _
                    AndAlso EzUtil.IsEqualIfNull(Trim(recordRecordVo.MakerCode), Trim(aData.GetMakerCode)) _
                    AndAlso EzUtil.IsEqualIfNull(recordRecordVo.BuhinNo, aData.GetBuhinNo) _
                    AndAlso EzUtil.IsEqualIfNull(recordRecordVo.KyoukuSection, aData.GetKyoukuSection)

        End Function

        ''' <summary>
        ''' マージ可でも親部品番号の関係でもマージ出来るか？を判断する
        ''' </summary>
        ''' <param name="parentRecordVos">親情報</param>
        ''' <param name="parentData">親Node</param>
        ''' <returns>判定結果</returns>
        ''' <remarks></remarks>
        Private Function CanMergeIrregularCase(ByVal parentRecordVos As HoyouBuhinBuhinKoseiRecordVo(), ByVal parentData As IData) As Boolean
            If parentData Is Nothing Then
                Return True
            End If

            ' 親の部品番号が同じでメーカーも同じだとマージする！！(trueを返す)
            For Each recordVo As HoyouBuhinBuhinKoseiRecordVo In parentRecordVos
                If EzUtil.IsEqualIfNull(recordVo.MakerCode, parentData.GetMakerCode) _
                        AndAlso (AProperty.CanMergeIfDefferenceOyaHinban OrElse recordVo.BuhinNo.Equals(parentData.GetBuhinNo)) Then
                    Return True
                End If
            Next

            Return False
        End Function
    End Class
End Namespace