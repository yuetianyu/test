Imports EventSakusei.ShisakuBuhinEdit.Al.Dao
Imports ShisakuCommon.Db.EBom.Dao.Impl
Imports ShisakuCommon.Db.EBom.Vo.Helper
Imports ShisakuCommon
Imports ShisakuCommon.Db.EBom.Dao
Imports ShisakuCommon.Db.EBom.Vo
Imports ShisakuCommon.Util

Namespace ShisakuBuhinEdit.Al.Logic
    ''' <summary>
    ''' A/LのMenu情報を担うクラス
    ''' </summary>
    ''' <remarks></remarks>
    Public Class BuhinEditAlMemoSupplier

        Private Const TITLE_ROW_INDEX As Integer = -1

        Private blockKeyVo As TShisakuSekkeiBlockVo
        Private ReadOnly rowIndexByGosha As Dictionary(Of String, Integer)
        Private ReadOnly memoDao As TShisakuSekkeiBlockMemoDao
        Private ReadOnly alDao As BuhinEditAlDao

        ''' <summary>
        ''' コンストラクタ
        ''' </summary>
        ''' <param name="blockKeyVo">試作設計ブロック情報（キー情報）</param>
        ''' <param name="rowIndexByGosha">「号車」で行indexを参照するDictionary</param>
        ''' <remarks></remarks>
        Public Sub New(ByVal blockKeyVo As TShisakuSekkeiBlockVo, _
                       ByVal rowIndexByGosha As Dictionary(Of String, Integer), ByVal alDao As BuhinEditAlDao)
            Me.New(blockKeyVo, rowIndexByGosha, New TShisakuSekkeiBlockMemoDaoImpl, alDao)

        End Sub
        ''' <summary>
        ''' コンストラクタ
        ''' </summary>
        ''' <param name="blockKeyVo">試作設計ブロック情報（キー情報）</param>
        ''' <param name="rowIndexByGosha">「号車」で行indexを参照するDictionary</param>
        ''' <param name="memoDao">試作設計ブロックメモ情報Dao</param>
        ''' <remarks></remarks>
        Public Sub New(ByVal blockKeyVo As TShisakuSekkeiBlockVo, _
                       ByVal rowIndexByGosha As Dictionary(Of String, Integer), _
                       ByVal memoDao As TShisakuSekkeiBlockMemoDao, ByVal alDao As BuhinEditAlDao)
            Me.blockKeyVo = blockKeyVo
            Me.rowIndexByGosha = rowIndexByGosha
            Me.memoDao = memoDao

            'ベース情報を読込む
            Me.alDao = alDao
            ReadRecord()

            Apply(FindBlockMemoBy())
        End Sub

        ''' <summary>
        ''' ブロックNoを差し替える
        ''' </summary>
        ''' <param name="blockVo">ブロックVo</param>
        ''' <remarks></remarks>
        Public Sub SupersedeBlockVo(ByVal blockVo As TShisakuSekkeiBlockVo)
            Me.blockKeyVo = blockVo
            _recordDimension.Clear()

            Apply(FindBlockMemoBy())
        End Sub

#Region "レコード操作"

        Private _recordDimension As New IndexedList(Of IndexedList(Of TShisakuSekkeiBlockMemoVo))

        ''' <summary>メモ情報</summary>
        ''' <param name="rowIndex">行No</param>
        ''' <param name="columnIndex">列No</param>
        ''' <returns>メモ情報</returns>
        ''' <remarks></remarks>
        Private ReadOnly Property Record(ByVal rowIndex As Integer, ByVal columnIndex As Integer) As TShisakuSekkeiBlockMemoVo
            Get
                Return _recordDimension.Value(rowIndex).Value(columnIndex)
            End Get
        End Property

        ''' <summary>
        ''' 入力行の行Noの一覧を返す（タイトル行を除く）
        ''' </summary>
        ''' <returns>入力行の行Noの一覧</returns>
        ''' <remarks></remarks>
        Private Function GetInputRowFirstIndex() As Integer
            For Each rowIndex As Integer In _recordDimension.Keys
                If rowIndex = TITLE_ROW_INDEX Then
                    Continue For
                End If
                Return rowIndex
            Next
            Throw New InvalidProgramException("号車が未入力はあり得ない.")
        End Function

        ''' <summary>
        ''' 入力行の行Noの一覧を返す（タイトル行含む） ※順不同
        ''' </summary>
        ''' <returns>入力行の行Noの一覧</returns>
        ''' <remarks></remarks>
        Private Function GetInputRowIndexesWithTitle() As ICollection(Of Integer)
            Return _recordDimension.Keys
        End Function

        ''' <summary>
        ''' 入力行の行Noの一覧を返す（タイトル行を除く） ※順不同
        ''' </summary>
        ''' <returns>入力行の行Noの一覧</returns>
        ''' <remarks></remarks>
        Public Function GetInputRowIndexes() As ICollection(Of Integer)
            Dim results As New List(Of Integer)
            For Each rowIndex As Integer In GetInputRowIndexesWithTitle()
                If rowIndex = TITLE_ROW_INDEX Then
                    Continue For
                End If
                results.Add(rowIndex)
            Next
            Return results
        End Function

        ''' <summary>
        ''' 入力した列タイトルの列No一覧を返す
        ''' </summary>
        ''' <returns>入力した列タイトルの列No一覧</returns>
        ''' <remarks></remarks>
        Private Function GetMaxInputMemoColumnIndex() As Integer
            Dim result As Integer = -1
            For Each columnIndex As Integer In GetInputColumnIndexes(TITLE_ROW_INDEX)
                result = Math.Max(columnIndex, result)
            Next
            Return result
        End Function
        ''' <summary>
        ''' 列タイトルの列数を返す
        ''' </summary>
        ''' <returns>列数</returns>
        ''' <remarks></remarks>
        Public Function GetMemoColumnCount() As Integer
            Return GetMaxInputMemoColumnIndex() + 1
        End Function

        ''' <summary>
        ''' 入力した内容のうち一番右の列Noを返す
        ''' </summary>
        ''' <returns>一番右の列No</returns>
        ''' <remarks></remarks>
        Private Function GetMaxInputMemoInputTekiyouColumnIndex() As Integer
            Dim result As Integer = -1
            For Each columnIndex As Integer In GetInputTitleColumnIndexes()
                result = Math.Max(columnIndex, result)
            Next
            For Each rowIndex As Integer In GetInputRowIndexes()
                For Each columnIndex As Integer In GetInputTekiyouColumnIndexes(rowIndex)
                    result = Math.Max(columnIndex, result)
                Next
            Next
            Return result
        End Function

        ''' <summary>
        ''' 最初は員数を入力していて、いまは未入力になったセルのデータを返す
        ''' </summary>
        ''' <param name="columnIndex">列index</param>
        ''' <returns>今は未入力のセル</returns>
        ''' <remarks>今も昔も員数未入力なら、最初の号車の行に空のセルデータを作成して返す</remarks>
        Private Function GetUninputInsuReocrd(ByVal columnIndex) As TShisakuSekkeiBlockMemoVo
            Dim result As TShisakuSekkeiBlockMemoVo = Nothing
            For Each rowIndex As Integer In GetInputRowIndexes()
                For Each columnIndex2 As Integer In GetInputColumnIndexes(rowIndex)
                    If columnIndex <> columnIndex2 Then
                        Continue For
                    End If
                    result = Record(rowIndex, columnIndex)
                    If Not StringUtil.IsEmpty(result.ShisakuTekiyou) Then
                        Return result
                    End If
                Next
            Next
            If result Is Nothing Then
                ' 最初の号車の行に空のセルデータ
                Return Record(GetInputRowFirstIndex, columnIndex)
            End If
            Return result
        End Function


        ''' <summary>
        ''' 行に一度でも入力された列No一覧を返す
        ''' </summary>
        ''' <param name="rowIndex">行index</param>
        ''' <returns>入力した列No一覧</returns>
        ''' <remarks></remarks>
        Public Function GetInputColumnIndexes(ByVal rowIndex As Integer) As ICollection(Of Integer)
            Return _recordDimension.Value(rowIndex).Keys
        End Function

        ''' <summary>
        ''' メモタイトルが入力された列No一覧を返す
        ''' </summary>
        ''' <remarks></remarks>
        Public Function GetInputTitleColumnIndexes() As ICollection(Of Integer)
            Dim results As New List(Of Integer)
            For Each columnIndex As Integer In GetInputColumnIndexes(TITLE_ROW_INDEX)
                If StringUtil.IsEmpty(MemoTitle(columnIndex)) Then
                    Continue For
                End If
                results.Add(columnIndex)
            Next
            Return results
        End Function

        ''' <summary>
        ''' 適用が入力された列の列No一覧を返す
        ''' </summary>
        ''' <param name="rowIndex">行index</param>
        ''' <returns>入力した列No一覧</returns>
        ''' <remarks></remarks>
        Public Function GetInputTekiyouColumnIndexes(ByVal rowIndex As Integer) As ICollection(Of Integer)
            Dim results As New List(Of Integer)
            For Each columnIndex As Integer In GetInputColumnIndexes(rowIndex)
                If StringUtil.IsEmpty(Tekiyou(rowIndex, columnIndex)) Then
                    Continue For
                End If
                results.Add(columnIndex)
            Next
            Return results
        End Function

        Private _columnCount As Integer
        ''' <summary>メモ列の列数</summary>
        Public Property ColumnCount() As Integer
            Get
                Return _columnCount
            End Get
            Set(ByVal value As Integer)
                _columnCount = value
            End Set
        End Property

        ''' <summary>
        ''' 列indexを元に、メモ列数を補正する
        ''' </summary>
        ''' <param name="columnIndex"></param>
        ''' <remarks></remarks>
        Private Sub CorrectColumnCount(ByVal columnIndex As Integer)
            _columnCount = Math.Max(_columnCount, columnIndex + 1)
        End Sub

        ''' <summary>適用</summary>
        ''' <param name="rowIndex">行index</param>
        ''' <param name="columnIndex">列index</param>
        Public Property Tekiyou(ByVal rowIndex As Integer, ByVal columnIndex As Integer) As String
            Get
                Return Record(rowIndex, columnIndex).ShisakuTekiyou
            End Get
            Set(ByVal value As String)
                CorrectColumnCount(columnIndex)
                Record(rowIndex, columnIndex).ShisakuTekiyou = value
            End Set
        End Property

        ''' <summary>メモ</summary>
        ''' <param name="columnIndex">列index</param>
        Public Property MemoTitle(ByVal columnIndex As Integer) As String
            Get
                Return Record(TITLE_ROW_INDEX, columnIndex).ShisakuMemo
            End Get
            Set(ByVal value As String)
                CorrectColumnCount(columnIndex)
                Record(TITLE_ROW_INDEX, columnIndex).ShisakuMemo = value
            End Set
        End Property

#End Region

        ''' <summary>
        ''' メモ情報を自身に反映する
        ''' </summary>
        ''' <param name="memoVos">試作設計ブロックメモ情報の一覧</param>
        ''' <remarks></remarks>
        Private Sub Apply(ByVal memoVos As List(Of TShisakuSekkeiBlockMemoVo))
            _columnCount = 0
            For Each vo As TShisakuSekkeiBlockMemoVo In memoVos
                Dim rowIndex As Integer
                If TShisakuSekkeiBlockMemoVoHelper.IsMemoTitleData(vo) Then
                    rowIndex = TITLE_ROW_INDEX
                Else
                    If Not rowIndexByGosha.ContainsKey(vo.ShisakuGousya) Then
                        Throw New ShisakuException("試作設計ブロックメモ情報に未知の号車を検出しました. " & vo.ShisakuGousya)
                    End If
                    rowIndex = rowIndexByGosha(vo.ShisakuGousya)
                End If
                Dim columnIndex As Integer = vo.ShisakuMemoHyoujiJun - TShisakuSekkeiBlockMemoVoHelper.ShisakuMemoHyoujiJun.START_VALUE
                ' 試作適用をset
                VoUtil.CopyProperties(vo, Record(rowIndex, columnIndex))

                _columnCount = Math.Max(_columnCount, columnIndex + 1)
            Next
        End Sub

        ''' <summary>
        ''' メモ情報を取得する
        ''' </summary>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Private Function FindBlockMemoBy() As List(Of TShisakuSekkeiBlockMemoVo)

            Dim param As New TShisakuSekkeiBlockMemoVo
            param.ShisakuEventCode = blockKeyVo.ShisakuEventCode
            param.ShisakuBukaCode = blockKeyVo.ShisakuBukaCode
            param.ShisakuBlockNo = blockKeyVo.ShisakuBlockNo
            param.ShisakuBlockNoKaiteiNo = blockKeyVo.ShisakuBlockNoKaiteiNo
            Return memoDao.FindBy(param)
        End Function

        ''' <summary>
        ''' 情報を読み込む
        ''' </summary>
        ''' <remarks></remarks>
        Private Sub ReadRecord()
            Dim vos As List(Of BuhinEditAlEventVo) = alDao.FindEventInfoById(blockKeyVo.ShisakuEventCode)
            For Each vo As BuhinEditAlEventVo In vos
                _record.Add(vo.HyojijunNo, vo)
            Next
        End Sub

        Private _record As New IndexedList(Of BuhinEditAlEventVo)

        ''' <summary>ベース車・完成車情報</summary>
        ''' <returns>ベース車・完成車情報</returns>
        Private ReadOnly Property Records(ByVal rowNo As Integer) As BuhinEditAlEventVo
            Get
                Return _record.Value(rowNo)
            End Get
        End Property

        ''' <summary>
        ''' 更新データを作成する
        ''' </summary>
        ''' <returns>更新データ</returns>
        ''' <remarks></remarks>
        Public Function MakeValues() As List(Of TShisakuSekkeiBlockMemoVo)
            Dim goshaByRowIndex As New Dictionary(Of Integer, String)
            For Each gosha As String In rowIndexByGosha.Keys
                goshaByRowIndex.Add(rowIndexByGosha(gosha), gosha)
            Next

            Dim results As New List(Of TShisakuSekkeiBlockMemoVo)

            For Each columnIndex As Integer In GetInputTitleColumnIndexes()
                results.Add(MakeInsertVo(TITLE_ROW_INDEX, columnIndex, TShisakuSekkeiBlockMemoVoHelper.ShisakuGousya.TITLE_VALUE))
            Next

            If GetMaxInputMemoInputTekiyouColumnIndex() < _columnCount - 1 Then
                results.Add(MakeInsertVo(TITLE_ROW_INDEX, _columnCount - 1, TShisakuSekkeiBlockMemoVoHelper.ShisakuGousya.TITLE_VALUE))
            End If

            For Each rowIndex As Integer In GetInputRowIndexes()

                '号車がブランクの場合は処理対象外とする。
                If Not StringUtil.IsEmpty(Records(rowIndex).ShisakuGousya) Then
                    Dim gosha As String = goshaByRowIndex(rowIndex)
                    For Each columnIndex As Integer In GetInputTekiyouColumnIndexes(rowIndex)
                        results.Add(MakeInsertVo(rowIndex, columnIndex, gosha))
                    Next
                End If

            Next
            Return results
        End Function

        ''' <summary>
        ''' 更新データを作成する
        ''' </summary>
        ''' <param name="rowIndex">行index</param>
        ''' <param name="columnIndex">列index</param>
        ''' <param name="gosha">号車</param>
        ''' <returns>更新データ</returns>
        ''' <remarks></remarks>
        Private Function MakeInsertVo(ByVal rowIndex As Integer, ByVal columnIndex As Integer, ByVal gosha As String) As TShisakuSekkeiBlockMemoVo
            Dim memoVo As TShisakuSekkeiBlockMemoVo = Record(rowIndex, columnIndex)
            SettingRecordTo(memoVo, gosha, columnIndex)
            Return memoVo
        End Function

        ''' <summary>
        ''' 更新データの中身を設定する
        ''' </summary>
        ''' <param name="instlVo">更新データ</param>
        ''' <param name="gosha">データの号車</param>
        ''' <param name="columnIndex">データの列index</param>
        ''' <remarks></remarks>
        Private Sub SettingRecordTo(ByVal instlVo As TShisakuSekkeiBlockMemoVo, ByVal gosha As String, ByVal columnIndex As Integer)

            If instlVo.ShisakuMemoHyoujiJun Is Nothing Then
                instlVo.ShisakuEventCode = blockKeyVo.ShisakuEventCode
                instlVo.ShisakuBukaCode = blockKeyVo.ShisakuBukaCode
                instlVo.ShisakuBlockNo = blockKeyVo.ShisakuBlockNo
                instlVo.ShisakuBlockNoKaiteiNo = blockKeyVo.ShisakuBlockNoKaiteiNo
                instlVo.ShisakuGousya = gosha
            End If
            instlVo.ShisakuMemoHyoujiJun = columnIndex + TShisakuSekkeiBlockMemoVoHelper.ShisakuMemoHyoujiJun.START_VALUE
        End Sub

        ''' <summary>
        ''' 更新する
        ''' </summary>
        ''' <param name="info">ログイン情報</param>
        ''' <param name="aDate">試作日付</param>
        ''' <remarks></remarks>
        Public Sub Update(ByVal info As LoginInfo, ByVal aDate As ShisakuDate)

            Dim param As New TShisakuSekkeiBlockMemoVo
            param.ShisakuEventCode = blockKeyVo.ShisakuEventCode
            param.ShisakuBukaCode = blockKeyVo.ShisakuBukaCode
            param.ShisakuBlockNo = blockKeyVo.ShisakuBlockNo
            param.ShisakuBlockNoKaiteiNo = blockKeyVo.ShisakuBlockNoKaiteiNo
            memoDao.DeleteBy(param)

            Dim values As List(Of TShisakuSekkeiBlockMemoVo) = MakeValues()
            For Each vo As TShisakuSekkeiBlockMemoVo In values
                If StringUtil.IsEmpty(vo.CreatedUserId) Then
                    vo.CreatedUserId = info.UserId
                    vo.CreatedDate = aDate.CurrentDateDbFormat
                    vo.CreatedTime = aDate.CurrentTimeDbFormat
                End If
                vo.UpdatedUserId = info.UserId
                vo.UpdatedDate = aDate.CurrentDateDbFormat
                vo.UpdatedTime = aDate.CurrentTimeDbFormat
                memoDao.InsertBy(vo)
            Next
        End Sub

        ''' <summary>
        ''' 列挿入する
        ''' </summary>
        ''' <param name="columnIndex">列index</param>
        ''' <param name="insertCount">挿入列数</param>
        ''' <remarks></remarks>
        Public Sub InsertColumn(ByVal columnIndex As Integer, ByVal insertCount As Integer)
            For Each rowIndex As Integer In GetInputRowIndexesWithTitle()
                Me._recordDimension(rowIndex).Insert(columnIndex, insertCount)
            Next
            _columnCount += insertCount
        End Sub

        ''' <summary>
        ''' 列削除する
        ''' </summary>
        ''' <param name="columnIndex">列index</param>
        ''' <param name="removeCount">削除列数</param>
        ''' <remarks></remarks>
        Public Sub RemoveColumn(ByVal columnIndex As Integer, ByVal removeCount As Integer)
            For Each rowIndex As Integer In GetInputRowIndexesWithTitle()
                Me._recordDimension(rowIndex).Remove(columnIndex, removeCount)
            Next
            _columnCount -= removeCount
        End Sub

    End Class
End Namespace