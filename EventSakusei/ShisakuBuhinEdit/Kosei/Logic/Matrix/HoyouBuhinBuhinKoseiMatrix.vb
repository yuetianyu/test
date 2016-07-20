Imports EventSakusei.ShisakuBuhinEdit.Kosei.Logic.Merge
Imports ShisakuCommon
Imports ShisakuCommon.Util
Imports ShisakuCommon.Db.EBom.Vo

Namespace ShisakuBuhinEdit.Kosei.Logic.Matrix

    ''' <summary>
    ''' 試作部品構成編集画面のSpread全行を表すクラス
    ''' </summary>
    ''' <remarks></remarks>
    Public Class HoyouBuhinBuhinKoseiMatrix
        ''' <summary>Spread一行を表すクラス</summary>
        Private Class RecordBag
            ''' <summary>員数以外の一行情報</summary>
            Public KoseiRecordVo As HoyouBuhinBuhinKoseiRecordVo
            ''' <summary>員数列の情報</summary>
            Public InsuVos As New IndexedList(Of BuhinKoseiInsuCellVo)

            ''' <summary>
            ''' コンストラクタ
            ''' </summary>
            ''' <remarks></remarks>
            Public Sub New()
                Me.New(New HoyouBuhinBuhinKoseiRecordVo)
            End Sub
            ''' <summary>
            ''' コンストラクタ
            ''' </summary>
            ''' <param name="KoseiRecordVo">員数以外の一行情報</param>
            ''' <remarks></remarks>
            Public Sub New(ByVal KoseiRecordVo As HoyouBuhinBuhinKoseiRecordVo)
                Me.KoseiRecordVo = KoseiRecordVo
            End Sub
            ''' <summary>
            ''' 員数列に空列を挿入する
            ''' </summary>
            ''' <param name="columnIndex">列index</param>
            ''' <param name="insertCount">挿入列数</param>
            ''' <remarks></remarks>
            Public Sub InsertInsuColumn(ByVal columnIndex As Integer, ByVal insertCount As Integer)
                InsuVos.Insert(columnIndex, insertCount)
            End Sub
            ''' <summary>
            ''' 員数列を除去する
            ''' </summary>
            ''' <param name="columnIndex">列index</param>
            ''' <param name="removeCount">除去列数</param>
            ''' <remarks></remarks>
            Public Sub RemoveInsuColumn(ByVal columnIndex As Integer, ByVal removeCount As Integer)
                InsuVos.Remove(columnIndex, removeCount)
            End Sub
        End Class

        Private _recordBags As New IndexedList(Of RecordBag)
        Private _columnBags As New IndexedList(Of HoyouBuhinInstlColumnBag)
        Private farColumnIndex As Integer = -1
        Private JikyuUmu As Integer

        ''' <summary>
        ''' コンストラクタ
        ''' </summary>
        ''' <remarks></remarks>
        Public Sub New()

        End Sub

        ''' <summary>
        ''' コンストラクタ
        ''' </summary>
        ''' <param name="editVos">試作部品編集情報</param>
        ''' <param name="editInstlVos">試作部品編集INSTL情報</param>
        ''' <remarks></remarks>
        Public Sub New(ByVal editVos As List(Of THoyouBuhinEditVo), ByVal editInstlVos As List(Of TShisakuBuhinEditInstlVo))
            'ここでeditVoと照らし合わせてとるかとらないか判断させる'

            Dim instlVosByHyojijun As Dictionary(Of Integer, List(Of TShisakuBuhinEditInstlVo)) = MakeInstlVosByHyojijun(editVos, editInstlVos)

            For Each editVo As THoyouBuhinEditVo In editVos
                VoUtil.CopyProperties(editVo, Record(editVo.BuhinNoHyoujiJun))
                If Not instlVosByHyojijun.ContainsKey(editVo.BuhinNoHyoujiJun) Then
                    Continue For
                End If
                For Each instlVo As TShisakuBuhinEditInstlVo In instlVosByHyojijun(editVo.BuhinNoHyoujiJun)
                    'ここに条件を追加'
                    ''↓↓2014/09/05 Ⅰ.3.設計編集 ベース車改修専用化 酒井 ADD BEGIN
                    '新規追加INSTLを指定して更新を選択した場合、instlVo.InstlHinbanHyoujiJunがNothingのため回避
                    If Not instlVo.InstlHinbanHyoujiJun Is Nothing Then
                        ''↑↑2014/09/05 Ⅰ.3.設計編集 ベース車改修専用化 酒井 ADD END
                        Dim insu As BuhinKoseiInsuCellVo = InsuVo(editVo.BuhinNoHyoujiJun, instlVo.InstlHinbanHyoujiJun)
                        VoUtil.CopyProperties(instlVo, insu)
                        PostUpdateInsuVo(editVo.BuhinNoHyoujiJun, instlVo.InstlHinbanHyoujiJun, insu)
                        ''↓↓2014/09/05 Ⅰ.3.設計編集 ベース車改修専用化 酒井 ADD BEGIN
                    End If
                    ''↑↑2014/09/05 Ⅰ.3.設計編集 ベース車改修専用化 酒井 ADD END
                Next
            Next
        End Sub

        ''' <summary>
        ''' コンストラクタ
        ''' </summary>
        ''' <param name="editVos">試作部品編集情報</param>
        ''' <param name="editInstlVos">試作部品編集INSTL情報</param>
        ''' <remarks></remarks>
        Public Sub New(ByVal editVos As List(Of TShisakuBuhinEditVo), ByVal editInstlVos As List(Of TShisakuBuhinEditInstlVo), ByVal flag As Integer)
            'ここでeditVoと照らし合わせてとるかとらないか判断させる'

            '2012/02/24'
            'For Each Vo As TShisakuBuhinEditVo In editVos
            '    'レベル０を元にCraeteColumnを作成する'
            '    Dim instlColumn As HoyouBuhinInstlColumnBag

            'Next

            Dim instlVosByHyojijun As Dictionary(Of Integer, List(Of TShisakuBuhinEditInstlVo)) = MakeInstlVosByHyojijunA(editVos, editInstlVos)
            Dim No As Integer = 0
            For Each editVo As TShisakuBuhinEditVo In editVos
                editVo.BuhinNoHyoujiJun = No
                VoUtil.CopyProperties(editVo, Record(editVo.BuhinNoHyoujiJun))
                If Not instlVosByHyojijun.ContainsKey(editVo.BuhinNoHyoujiJun) Then
                    No = No + 1
                    Continue For
                End If
                For Each instlVo As TShisakuBuhinEditInstlVo In instlVosByHyojijun(editVo.BuhinNoHyoujiJun)
                    'ここに条件を追加'
                    Dim insu As BuhinKoseiInsuCellVo = InsuVo(editVo.BuhinNoHyoujiJun, instlVo.InstlHinbanHyoujiJun)
                    VoUtil.CopyProperties(instlVo, insu)
                    PostUpdateInsuVo(editVo.BuhinNoHyoujiJun, instlVo.InstlHinbanHyoujiJun, insu)
                Next
                No = No + 1
            Next
        End Sub

        ''' <summary>
        ''' 表示順NoによってINSTL情報を返すDictionaryを作成する
        ''' </summary>
        ''' <param name="editInstlVos">試作部品編集INSTL情報</param>
        ''' <returns>Dictionary</returns>
        ''' <remarks></remarks>
        Private Shared Function MakeInstlVosByHyojijun(ByVal editVos As List(Of THoyouBuhinEditVo), ByVal editInstlVos As List(Of TShisakuBuhinEditInstlVo)) As Dictionary(Of Integer, List(Of TShisakuBuhinEditInstlVo))

            Dim instlVosByHyojijun As New Dictionary(Of Integer, List(Of TShisakuBuhinEditInstlVo))
            For Each vo As TShisakuBuhinEditInstlVo In editInstlVos
                If Not instlVosByHyojijun.ContainsKey(vo.BuhinNoHyoujiJun) Then
                    instlVosByHyojijun.Add(vo.BuhinNoHyoujiJun, New List(Of TShisakuBuhinEditInstlVo))
                End If
                instlVosByHyojijun(vo.BuhinNoHyoujiJun).Add(vo)
            Next
            Return instlVosByHyojijun
        End Function

        ''' <summary>
        ''' 表示順NoによってINSTL情報を返すDictionaryを作成する
        ''' </summary>
        ''' <param name="editInstlVos">試作部品編集INSTL情報</param>
        ''' <returns>Dictionary</returns>
        ''' <remarks></remarks>
        Private Shared Function MakeInstlVosByHyojijunA(ByVal editVos As List(Of TShisakuBuhinEditVo), ByVal editInstlVos As List(Of TShisakuBuhinEditInstlVo)) As Dictionary(Of Integer, List(Of TShisakuBuhinEditInstlVo))

            Dim instlVosByHyojijun As New Dictionary(Of Integer, List(Of TShisakuBuhinEditInstlVo))

            Dim No As Integer = 0
            For Each vo As TShisakuBuhinEditInstlVo In editInstlVos
                If Not instlVosByHyojijun.ContainsKey(No) Then
                    instlVosByHyojijun.Add(No, New List(Of TShisakuBuhinEditInstlVo))
                End If

                If vo.BuhinNoHyoujiJun = No Then
                    instlVosByHyojijun(vo.BuhinNoHyoujiJun).Add(vo)
                Else
                    vo.BuhinNoHyoujiJun = No
                    instlVosByHyojijun(vo.BuhinNoHyoujiJun).Add(vo)
                End If
                No = No + 1
            Next
            Return instlVosByHyojijun
        End Function

        ''' <summary>
        ''' 部品編集のコピーを作成
        ''' </summary>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Function Copy() As HoyouBuhinBuhinKoseiMatrix
            Dim result As New HoyouBuhinBuhinKoseiMatrix()
            For Each colindex As Integer In Me.GetInputInsuColumnIndexes
                result.InstlColumn(colindex) = Me.InstlColumn(colindex)
            Next
            For Each rowindex As Integer In Me.GetInputRowIndexes
                result.Record(rowindex) = Me.Record(rowindex)
            Next

            For Each colindex As Integer In Me.GetInputInsuColumnIndexes
                For Each rowindex As Integer In Me.GetInputRowIndexes
                    result.InsuVo(rowindex, colindex) = Me.InsuVo(rowindex, colindex)
                    result.InsuSuryo(rowindex, colindex) = Me.InsuSuryo(rowindex, colindex)
                Next
            Next

            Return result
        End Function

        ''' <summary>
        ''' 試作部品編集INSTL情報の表示順に該当した試作部品編集情報とだけで部品表を作成する
        ''' </summary>
        ''' <param name="editVos">試作部品編集情報</param>
        ''' <param name="editInstlVos">試作部品編集INSTL情報</param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Shared Function NewInnerJoinHyoujiJun(ByVal editVos As List(Of THoyouBuhinEditVo), ByVal editInstlVos As List(Of TShisakuBuhinEditInstlVo)) As HoyouBuhinBuhinKoseiMatrix

            Dim instlVosByHyojijun As Dictionary(Of Integer, List(Of TShisakuBuhinEditInstlVo)) = MakeInstlVosByHyojijun(editVos, editInstlVos)
            Dim extractedEditVos As New List(Of THoyouBuhinEditVo)
            For Each editVo As THoyouBuhinEditVo In editVos
                If instlVosByHyojijun.ContainsKey(editVo.BuhinNoHyoujiJun) Then
                    extractedEditVos.Add(editVo)
                End If
            Next
            Return New HoyouBuhinBuhinKoseiMatrix(extractedEditVos, editInstlVos)
        End Function

        ''' <summary>
        ''' 員数情報を返す
        ''' </summary>
        ''' <param name="rowIndex">行index</param>
        ''' <param name="columnIndex">列index</param>
        ''' <value>員数情報</value>
        ''' <returns>員数情報</returns>
        ''' <remarks></remarks>
        Public Property InsuVo(ByVal rowIndex As Integer, ByVal columnIndex As Integer) As BuhinKoseiInsuCellVo
            Get
                farColumnIndex = Math.Max(farColumnIndex, columnIndex)
                Return _recordBags.Value(rowIndex).InsuVos(columnIndex)
            End Get
            Set(ByVal value As BuhinKoseiInsuCellVo)
                farColumnIndex = Math.Max(farColumnIndex, columnIndex)
                _recordBags.Value(rowIndex).InsuVos(columnIndex) = value

                PostUpdateInsuVo(rowIndex, columnIndex, value)
            End Set
        End Property

        ''' <summary>
        ''' 員数セルVOプロパティが更新された後の後処理
        ''' </summary>
        ''' <param name="rowIndex">行index</param>
        ''' <param name="columnIndex">列index</param>
        ''' <param name="updatedValue">更新した員数セルVO</param>
        ''' <remarks></remarks>
        Private Sub PostUpdateInsuVo(ByVal rowIndex As Integer, ByVal columnIndex As Integer, ByVal updatedValue As BuhinKoseiInsuCellVo)

            'INSTL列の同期
            Dim columnBag As HoyouBuhinInstlColumnBag = InstlColumn(columnIndex)
            Dim recordVo As HoyouBuhinBuhinKoseiRecordVo = Record(rowIndex)
            If updatedValue Is Nothing OrElse updatedValue.InsuSuryo Is Nothing Then
                ' INSTL列情報から InsuCellBag を除去
                If Not columnBag.Contains(recordVo) Then
                    Return
                End If
                columnBag.Remove(recordVo)
            ElseIf columnBag.Contains(recordVo) Then
                columnBag.InsuVo(columnBag.IndexOf(recordVo)) = updatedValue
            Else
                ' INSTL列情報に InsuCellBag を追加・挿入
                Dim beforeIndexRecord As HoyouBuhinBuhinKoseiRecordVo = GetBeforeIndexRecord(rowIndex, columnIndex)
                Dim index As Integer = 0
                If beforeIndexRecord IsNot Nothing Then
                    index = columnBag.IndexOf(beforeIndexRecord) + 1
                End If
                columnBag.Insert(index, recordVo, updatedValue)
            End If
        End Sub

        ''' <summary>
        ''' 行情報を返す（員数以外）
        ''' </summary>
        ''' <param name="rowIndex">行index</param>
        ''' <value>行情報</value>
        ''' <returns>行情報</returns>
        ''' <remarks></remarks>
        Default Public Property Record(ByVal rowIndex As Integer) As HoyouBuhinBuhinKoseiRecordVo
            Get
                Return _recordBags.Value(rowIndex).KoseiRecordVo
            End Get
            Set(ByVal value As HoyouBuhinBuhinKoseiRecordVo)
                _recordBags.Value(rowIndex).KoseiRecordVo = value
            End Set
        End Property
        ''' <summary>
        ''' 行情報を返す（員数以外）
        ''' </summary>
        Public ReadOnly Property Records() As ICollection(Of HoyouBuhinBuhinKoseiRecordVo)
            Get
                Dim result As New List(Of HoyouBuhinBuhinKoseiRecordVo)
                For Each rowIndex As Integer In GetInputRowIndexes()
                    result.Add(Record(rowIndex))
                Next
                Return result
            End Get
        End Property
        ''' <summary>
        ''' INSTL列情報を返す
        ''' </summary>
        ''' <param name="columnIndex">行index</param>
        Public Property InstlColumn(ByVal columnIndex As Integer) As HoyouBuhinInstlColumnBag
            Get
                Return _columnBags(columnIndex)
            End Get
            Set(ByVal value As HoyouBuhinInstlColumnBag)
                _columnBags(columnIndex) = value
            End Set
        End Property

        ''' <summary>
        ''' INSTL列情報を追加する
        ''' </summary>
        ''' <param name="columnIndex">列番号</param>
        ''' <param name="value">INSTL列情報</param>
        ''' <remarks></remarks>
        Public Sub InstlColumnAdd(ByVal columnIndex As Integer, ByVal value As HoyouBuhinInstlColumnBag)
            _columnBags(columnIndex) = value
        End Sub

        ''' <summary>
        ''' INSTL列情報上の行情報を削除する
        ''' </summary>
        ''' <param name="columnIndex">列番号</param>
        ''' <remarks></remarks>
        Public Sub InstlColumnRemove(ByVal columnIndex As Integer)

            '該当する全てのセル情報を削除する'
            For index As Integer = 0 To _columnBags(columnIndex).Count - 1
                _columnBags(columnIndex).RemoveCell(index)
            Next
        End Sub

        ''' <summary>
        ''' 員数列を遡って直上の員数入力Recordを返す
        ''' </summary>
        ''' <param name="rowIndex">基点となるrowIndex</param>
        ''' <param name="columnIndex">員数列</param>
        ''' <returns>存在しなければ、nothing</returns>
        ''' <remarks></remarks>
        Public Function GetBeforeIndexRecord(ByVal rowIndex As Integer, ByVal columnIndex As Integer) As HoyouBuhinBuhinKoseiRecordVo

            Dim indexes As List(Of Integer) = GetInputRowIndexesOnColumn(columnIndex)
            For i As Integer = 1 To indexes.Count
                If rowIndex <= indexes(indexes.Count - i) Then
                    Continue For
                End If
                Return Record(indexes(indexes.Count - i))
            Next
            Return Nothing
        End Function

        ''' <summary>
        ''' 員数列を遡って親レベルRecordを返す
        ''' </summary>
        ''' <param name="rowIndex">基点となるrowIndex</param>
        ''' <param name="columnIndex">員数列</param>
        ''' <returns>存在しなければ、nothing</returns>
        ''' <remarks></remarks>
        Public Function GetParentRecord(ByVal rowIndex As Integer, ByVal columnIndex As Integer) As HoyouBuhinBuhinKoseiRecordVo

            Dim HoyouBuhinBuhinKoseiRecordVo As HoyouBuhinBuhinKoseiRecordVo = Record(rowIndex)
            If HoyouBuhinBuhinKoseiRecordVo.Level = 0 Then
                Return Nothing
            End If
            Dim indexes As List(Of Integer) = GetInputRowIndexesOnColumn(columnIndex)
            For i As Integer = 1 To indexes.Count
                Dim index As Integer = indexes(indexes.Count - i)
                If rowIndex <= index Then
                    Continue For
                End If
                If Record(index).Level + 1 = HoyouBuhinBuhinKoseiRecordVo.Level Then
                    Return Record(index)
                End If
            Next
            Return Nothing
        End Function

        ''' <summary>
        ''' 員数列を遡って親レベルRecordを返す
        ''' </summary>
        ''' <param name="rowIndex">基点となるrowIndex</param>
        ''' <returns>存在しなければ、長さ0の配列</returns>
        ''' <remarks></remarks>
        Public Function GetParentRecords(ByVal rowIndex As Integer) As HoyouBuhinBuhinKoseiRecordVo()
            Dim aSet As New Dictionary(Of HoyouBuhinBuhinKoseiRecordVo, Integer)
            For Each columnIndex As Integer In GetInputInsuColumnIndexesOnRow(rowIndex)
                Dim record As HoyouBuhinBuhinKoseiRecordVo = GetParentRecord(rowIndex, columnIndex)
                If record Is Nothing OrElse aSet.ContainsKey(record) Then
                    Continue For
                End If
                aSet.Add(record, 0)
            Next
            Dim results As New List(Of HoyouBuhinBuhinKoseiRecordVo)(aSet.Keys)
            Return results.ToArray
        End Function

        ''' <summary>
        ''' 入力した行数
        ''' </summary>
        ''' <returns>入力した行数</returns>
        Public ReadOnly Property InputRowCount() As Integer
            Get
                Return _recordBags.Keys.Count
            End Get
        End Property

        ''' <summary>
        ''' 行indexを返す(順昇順)
        ''' </summary>
        ''' <returns>行indexのコレクション</returns>
        Public Function GetInputRowIndexes() As ICollection(Of Integer)
            Dim aList As New List(Of Integer)(_recordBags.Keys)
            aList.Sort()
            Return aList
        End Function

        ''' <summary>
        ''' 行indexを返す(順昇順)
        ''' </summary>
        ''' <param name="columnIndex">列index</param>
        ''' <returns>行indexのコレクション</returns>
        ''' <remarks></remarks>
        Public Function GetInputRowIndexesOnColumn(ByVal columnIndex As Integer) As List(Of Integer)
            Dim results As New List(Of Integer)
            For Each rowIndex As Integer In GetInputRowIndexes()
                For Each rowColumnIndex As Integer In GetInputInsuColumnIndexesOnRow(rowIndex)
                    If rowColumnIndex = columnIndex Then
                        results.Add(rowIndex)
                        Exit For
                    End If
                Next
            Next
            results.Sort()
            Return results
        End Function

        ''' <summary>
        ''' 入力行indexの最大値を返す
        ''' </summary>
        ''' <returns>入力行indexの最大値</returns>
        Public Function GetMaxInputRowIndex() As Integer
            Dim aList As New List(Of Integer)(_recordBags.Keys)
            If aList.Count = 0 Then
                Return -1
            End If
            aList.Sort()
            Return aList(aList.Count - 1)
        End Function

        ''' <summary>
        ''' 新規行indexを返す
        ''' </summary>
        ''' <returns>新規行index</returns>
        ''' <remarks></remarks>
        Public Function GetNewRowIndex() As Integer
            Return GetMaxInputRowIndex() + 1
        End Function

        ''' <summary>
        ''' 指定行に員数が入力された列indexを返す
        ''' </summary>
        ''' <param name="rowIndex">行index</param>
        ''' <returns>列index</returns>
        ''' <remarks></remarks>
        Public Function GetInputInsuColumnIndexesOnRow(ByVal rowIndex As Integer) As ICollection(Of Integer)
            Dim aList As New List(Of Integer)(_recordBags.Value(rowIndex).InsuVos.Keys)
            aList.Sort()
            Return aList
        End Function

        ''' <summary>
        ''' （全ての行にわたって）員数が入力された列indexを返す
        ''' </summary>
        ''' <returns>列index</returns>
        ''' <remarks></remarks>
        Public Function GetInputInsuColumnIndexes() As ICollection(Of Integer)
            Dim aList As New List(Of Integer)
            For Each rowIndex As Integer In GetInputRowIndexes()
                For Each columnIndex As Integer In GetInputInsuColumnIndexesOnRow(rowIndex)
                    If aList.Contains(columnIndex) Then
                        Continue For
                    End If
                    aList.Add(columnIndex)
                Next
            Next
            aList.Sort()
            Return aList
        End Function

        ''' <summary>
        ''' 入力された列indexを返す
        ''' </summary>
        ''' <returns>列index</returns>
        ''' <remarks></remarks>
        Public Function GetInputColumnIndexes() As Integer
            Dim aList As New List(Of Integer)
            For Each rowIndex As Integer In GetInputRowIndexes()
                For Each columnIndex As Integer In GetInputInsuColumnIndexesOnRow(rowIndex)
                    If aList.Contains(columnIndex) Then
                        Continue For
                    End If
                    aList.Add(columnIndex)
                Next
            Next
            aList.Sort()
            aList.Reverse()

            Return aList(0)
        End Function

        ''' <summary>
        ''' 新規列indexを返す
        ''' </summary>
        ''' <returns>新規列index</returns>
        ''' <remarks></remarks>
        Public Function GetNewInsuColumnIndex() As Integer
            Return farColumnIndex + 1
        End Function

        ''' <summary>
        ''' 員数を返す
        ''' </summary>
        ''' <param name="rowIndex">行index</param>
        ''' <param name="columnIndex">列index</param>
        ''' <value>員数</value>
        ''' <returns>員数</returns>
        Public Property InsuSuryo(ByVal rowIndex As Integer, ByVal columnIndex As Integer) As Integer?
            Get
                farColumnIndex = Math.Max(farColumnIndex, columnIndex)
                Return InsuVo(rowIndex, columnIndex).InsuSuryo
            End Get
            Set(ByVal value As Integer?)
                farColumnIndex = Math.Max(farColumnIndex, columnIndex)
                Dim aInsuVo As BuhinKoseiInsuCellVo = InsuVo(rowIndex, columnIndex)
                aInsuVo.InsuSuryo = value

                PostUpdateInsuVo(rowIndex, columnIndex, aInsuVo)
            End Set
        End Property

        ''' <summary>
        ''' 部品表を挿入する
        ''' </summary>
        ''' <param name="rowIndex">行index</param>
        ''' <param name="insertRecord">挿入する部品表</param>
        ''' <remarks></remarks>
        Public Sub Insert(ByVal rowIndex As Integer, ByVal insertRecord As HoyouBuhinBuhinKoseiMatrix)
            Dim destIndex As Integer = 0
            For Each srcIndex As Integer In insertRecord.GetInputRowIndexes()
                InsertRow(rowIndex + destIndex)
                _recordBags(rowIndex + destIndex) = insertRecord._recordBags(srcIndex)
                destIndex += 1
            Next
        End Sub

        ''' <summary>
        ''' 空行を挿入する
        ''' </summary>
        ''' <param name="rowIndex">行index</param>
        ''' <param name="insertCount">挿入行数</param>
        ''' <remarks></remarks>
        Public Sub InsertRow(ByVal rowIndex As Integer, Optional ByVal insertCount As Integer = 1)
            _recordBags.Insert(rowIndex, insertCount)
        End Sub

        ''' <summary>
        ''' 行情報を削除する
        ''' </summary>
        ''' <param name="rowIndex">行index</param>
        ''' <param name="removeCount">除去行数</param>
        ''' <remarks></remarks>
        Public Sub RemoveRow(ByVal rowIndex As Integer, Optional ByVal removeCount As Integer = 1)
            For i As Integer = 1 To removeCount
                Dim index As Integer = rowIndex + removeCount - i
                For Each columnIndex As Integer In GetInputInsuColumnIndexesOnRow(index)
                    PostUpdateInsuVo(index, columnIndex, Nothing)       ' とりあえずこれでColumnBagの同期をしてもらう
                Next
            Next
            _recordBags.Remove(rowIndex, removeCount)
        End Sub

        ''' <summary>
        ''' 員数列に空列を挿入する
        ''' </summary>
        ''' <param name="columnIndex">列index</param>
        ''' <param name="insertCount">挿入列数</param>
        ''' <remarks></remarks>
        Public Sub InsertColumn(ByVal columnIndex As Integer, Optional ByVal insertCount As Integer = 1)
            farColumnIndex += insertCount
            For Each rowIndex As Integer In GetInputRowIndexes()
                _recordBags(rowIndex).InsertInsuColumn(columnIndex, insertCount)
            Next
        End Sub

        ''' <summary>
        ''' 員数列を除去する
        ''' </summary>
        ''' <param name="columnIndex">列index</param>
        ''' <param name="removeCount">除去列数</param>
        ''' <remarks></remarks>
        Public Sub RemoveColumn(ByVal columnIndex As Integer, Optional ByVal removeCount As Integer = 1)
            farColumnIndex -= removeCount
            For Each rowIndex As Integer In GetInputRowIndexes()
                _recordBags(rowIndex).RemoveInsuColumn(columnIndex, removeCount)
            Next
        End Sub

        ''' <summary>
        ''' 空行を削除して、行を詰める
        ''' </summary>
        ''' <remarks></remarks>
        Public Sub RemoveNullRecords()
            Dim aList As New List(Of Integer)(_recordBags.Keys)
            If aList.Count = 0 Then
                Return
            End If
            aList.Sort()

            Dim beforeIndex As Integer = aList(aList.Count - 1)
            For i As Integer = 2 To aList.Count
                Dim srcIndex As Integer = aList(aList.Count - i)
                If srcIndex + 1 < beforeIndex Then
                    For j As Integer = 1 To beforeIndex - srcIndex - 1
                        RemoveRow(beforeIndex - j)
                    Next
                End If
                beforeIndex = srcIndex
            Next
            If 0 < beforeIndex Then
                For j As Integer = 1 To beforeIndex
                    RemoveRow(beforeIndex - j)
                Next
            End If
        End Sub

        Public Sub RemoveJikyu()
            For index As Integer = Records.Count - 1 To 0 Step -1
                If Not Record(index) Is Nothing Then
                    If StringUtil.IsEmpty(Record(index).ShukeiCode) Then
                        If StringUtil.Equals(Record(index).SiaShukeiCode, "J") Then
                            RemoveRow(index)
                        End If
                    Else
                        If StringUtil.Equals(Record(index).ShukeiCode, "J") Then
                            RemoveRow(index)
                        End If
                    End If
                End If
            Next

        End Sub

        ''' <summary>
        ''' 指定レベル配下の構成情報を抽出して返す
        ''' </summary>
        ''' <param name="rowIndex">抽出の基点となる構成情報の行index</param>
        ''' <returns>新しい部品表</returns>
        ''' <remarks></remarks>
        Public Function ExtractUnderLevel(ByVal rowIndex As Integer) As HoyouBuhinBuhinKoseiMatrix
            ' 前提条件
            ' レベルだけを見て、レベル配下を抽出する
            ' 員数は無視するので、複数INSTL品番の場合、整合性が取れない事になる

            Dim result As New HoyouBuhinBuhinKoseiMatrix
            If _recordBags.Keys.Count = 0 Then
                Return result
            End If
            Dim aList As New List(Of Integer)(_recordBags.Keys)
            aList.Sort()
            If Not IsNumeric(Record(rowIndex).Level) Then
                Throw New ArgumentException("指定の表示順はレベルが未入力")
            End If

            For Each index As Integer In aList
                If index < rowIndex Then
                    Continue For
                End If
                If rowIndex < index Then
                    If Not IsNumeric(Record(index).Level) Then
                        Continue For
                    End If
                    If Record(index).Level <= Record(rowIndex).Level Then
                        Exit For
                    End If
                End If
                result.Record(index) = Record(index)
                For Each columnIndex As Integer In GetInputInsuColumnIndexesOnRow(index)
                    result.InsuVo(index, columnIndex) = InsuVo(index, columnIndex)
                Next
            Next
            Return result
        End Function

        ''' <summary>
        ''' レベルをゼロ基点にする
        ''' </summary>
        ''' <remarks></remarks>
        Public Sub CorrectLevelZeroBase()
            CorrectLevelBy(0)
        End Sub

        ''' <summary>
        ''' 基点となるレベルを変更し、全体のレベルを調整する
        ''' </summary>
        ''' <param name="baseLevel">基点のレベル</param>
        ''' <remarks></remarks>
        Public Sub CorrectLevelBy(ByVal baseLevel As Integer?)
            If 0 = _recordBags.Keys.Count Then
                Return
            End If

            If baseLevel Is Nothing Then
                For Each index As Integer In GetInputRowIndexes()
                    Record(index).Level = Nothing
                Next
                Return
            End If

            Dim indexes As New List(Of Integer)(GetInputRowIndexes())
            If Not IsNumeric(Record(indexes(0)).Level) Then
                'エラーを発生させない'
                Return
                Throw New ArgumentException("基点となるレベル値が数値ではない")
            End If
            Dim diffLevel As Integer = Record(indexes(0)).Level - baseLevel
            For Each index As Integer In indexes
                If Not IsNumeric(Record(index).Level) Then
                    Continue For
                End If
                Record(index).Level -= diffLevel
            Next
        End Sub

    End Class
End Namespace