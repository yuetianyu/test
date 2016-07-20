Imports EventSakusei.ShisakuBuhinEdit.Al.Dao
Imports EventSakusei.ShisakuBuhinEdit.Logic.Detect
Imports ShisakuCommon.Db.EBom.Vo.Helper
Imports ShisakuCommon
Imports ShisakuCommon.Db.EBom.Dao
Imports ShisakuCommon.Db.EBom.Vo
Imports ShisakuCommon.Util

Namespace ShisakuBuhinEdit.Al.Logic
    ''' <summary>
    ''' A/LのINSTL品番列の情報を担うクラス
    ''' </summary>
    ''' <remarks></remarks>
    Public Class BuhinEditAlInstlSupplier

        Private Const TITLE_ROW_INDEX As Integer = -1

        Private _blockKeyVo As TShisakuSekkeiBlockVo
        Private ReadOnly rowIndexByGosha As Dictionary(Of String, Integer)
        Private ReadOnly instlDao As TShisakuSekkeiBlockInstlDao
        ''↓↓2014/08/28 Ⅰ.5.EBOM差分出力 酒井 ADD BEGIN
        Private ReadOnly instlEbomKanshiDao As TShisakuSekkeiBlockInstlEbomKanshiDao
        ''↑↑2014/08/28 Ⅰ.5.EBOM差分出力 酒井 ADD END
        Private ReadOnly aLatestStructure As DetectLatestStructure
        Private ReadOnly alDao As BuhinEditAlDao
        'イベントコピー用イベントコード'
        Private copyEventCode As String

        ''' <summary>
        ''' コンストラクタ
        ''' </summary>
        ''' <param name="blockKeyVo">試作設計ブロック情報（キー情報）</param>
        ''' <param name="rowIndexByGosha">「号車」で行indexを参照するDictionary</param>
        ''' <param name="instlDao">試作設計ブロックINSTL情報Dao</param>
        ''' <param name="aLatestStructure">構成の情報を探索するメソッド</param>
        ''' <remarks></remarks>
        Public Sub New(ByVal blockKeyVo As TShisakuSekkeiBlockVo, _
                       ByVal rowIndexByGosha As Dictionary(Of String, Integer), _
                       ByVal instlDao As TShisakuSekkeiBlockInstlDao, _
                       ByVal aLatestStructure As DetectLatestStructure, ByVal alDao As BuhinEditAlDao, Optional ByVal instlEbomKanshiDao As TShisakuSekkeiBlockInstlEbomKanshiDao = Nothing, Optional ByVal login As LoginInfo = Nothing)
            ''↓↓2014/08/28 Ⅰ.5.EBOM差分出力 酒井 ADD BEGIN
            '        Public Sub New(ByVal blockKeyVo As TShisakuSekkeiBlockVo, _
            '           ByVal rowIndexByGosha As Dictionary(Of String, Integer), _
            '           ByVal instlDao As TShisakuSekkeiBlockInstlDao, _
            '           ByVal aLatestStructure As DetectLatestStructure, ByVal alDao As BuhinEditAlDao)
            ''↑↑2014/08/28 Ⅰ.5.EBOM差分出力 酒井 ADD END

            Me._blockKeyVo = blockKeyVo
            Me.rowIndexByGosha = rowIndexByGosha
            Me.instlDao = instlDao
            ''↓↓2014/08/28 Ⅰ.5.EBOM差分出力 酒井 ADD BEGIN
            Me.instlEbomKanshiDao = instlEbomKanshiDao
            ''↑↑2014/08/28 Ⅰ.5.EBOM差分出力 酒井 ADD END
            Me.aLatestStructure = aLatestStructure
            copyEventCode = ""

            ''↓↓2014/08/28 Ⅰ.5.EBOM差分出力 酒井 ADD BEGIN
            'Nothing-Nothingは、EBOM差分監視夜間設計展開以外の既存ロジックからの呼出し
            If (login Is Nothing) And Not (instlEbomKanshiDao Is Nothing) Then
                Dim sbiVoList As New List(Of TShisakuSekkeiBlockInstlVo)
                Dim sbiVoListEbomKanshi As List(Of TShisakuSekkeiBlockInstlEbomKanshiVo)
                sbiVoListEbomKanshi = FindInstlEbomKanshiBy()
                For index = 0 To sbiVoListEbomKanshi.Count - 1
                    Dim sbiVo As New TShisakuSekkeiBlockInstlVo
                    VoUtil.CopyProperties(sbiVoListEbomKanshi(index), sbiVo)
                    sbiVoList.Add(sbiVo)
                Next
                Apply(sbiVoList)
            Else
                ''↑↑2014/08/28 Ⅰ.5.EBOM差分出力 酒井 ADD END
                Apply(FindInstlBy())
                ''↓↓2014/08/28 Ⅰ.5.EBOM差分出力 酒井 ADD BEGIN
            End If
            ''↑↑2014/08/28 Ⅰ.5.EBOM差分出力 酒井 ADD END

            'ベース情報読込み
            Me.alDao = alDao
            ReadRecord()

        End Sub

'/*** 20140911 CHANGE START（コンストラクタ追加） ***/
        ''' <summary>
        ''' コンストラクタ
        ''' </summary>
        ''' <param name="blockKeyVo">試作設計ブロック情報（キー情報）</param>
        ''' <param name="rowIndexByGosha">「号車」で行indexを参照するDictionary</param>
        ''' <param name="instlDao">試作設計ブロックINSTL情報Dao</param>
        ''' <param name="aLatestStructure">構成の情報を探索するメソッド</param>
        ''' <remarks></remarks>
        Public Sub New(ByVal blockKeyVo As TShisakuSekkeiBlockVo, _
                       ByVal rowIndexByGosha As Dictionary(Of String, Integer), _
                       ByVal instlDao As TShisakuSekkeiBlockInstlDao, _
                       ByVal aLatestStructure As DetectLatestStructure, ByVal alDao As BuhinEditAlDao, _
                       ByVal vos As List(Of BuhinEditAlEventVo), _
                       Optional ByVal instlEbomKanshiDao As TShisakuSekkeiBlockInstlEbomKanshiDao = Nothing, _
                       Optional ByVal login As LoginInfo = Nothing)

            Me._blockKeyVo = blockKeyVo
            Me.rowIndexByGosha = rowIndexByGosha
            Me.instlDao = instlDao
            Me.instlEbomKanshiDao = instlEbomKanshiDao
            Me.aLatestStructure = aLatestStructure
            copyEventCode = ""

            If (login Is Nothing) And Not (instlEbomKanshiDao Is Nothing) Then
                Dim sbiVoList As New List(Of TShisakuSekkeiBlockInstlVo)
                Dim sbiVoListEbomKanshi As List(Of TShisakuSekkeiBlockInstlEbomKanshiVo)
                sbiVoListEbomKanshi = FindInstlEbomKanshiBy()
                For index = 0 To sbiVoListEbomKanshi.Count - 1
                    Dim sbiVo As New TShisakuSekkeiBlockInstlVo
                    VoUtil.CopyProperties(sbiVoListEbomKanshi(index), sbiVo)
                    sbiVoList.Add(sbiVo)
                Next
                Apply(sbiVoList)
            Else
                Apply(FindInstlBy())
            End If

            'ベース情報読込み
            Me.alDao = alDao
            ReadRecord(vos)

        End Sub
        ''' <summary>
        ''' 情報を読み込む
        ''' </summary>
        ''' <remarks></remarks>
        Private Sub ReadRecord(ByVal vos As List(Of BuhinEditAlEventVo))
            For Each vo As BuhinEditAlEventVo In vos
                _record.Add(vo.HyojijunNo, vo)
            Next
        End Sub
        '/*** 20140911 CHANGE END ***/
        ''' <summary>
        ''' ブロックNoを差し替える
        ''' </summary>
        ''' <param name="blockVo">ブロックVo</param>
        ''' <remarks></remarks>
        Public Sub SupersedeBlockVo(ByVal blockVo As TShisakuSekkeiBlockVo)
            Me._blockKeyVo = blockVo
            _recordDimension.Clear()
            _StructureResults.Clear()

            Apply(FindInstlBy())
        End Sub

        ''' <summary>
        ''' INSTL品番をコピーする
        ''' </summary>
        ''' <returns>INSTL品番のコピー</returns>
        ''' <remarks></remarks>
        Public Function CloneCopy() As BuhinEditAlInstlSupplier
            Dim result As New BuhinEditAlInstlSupplier(_blockKeyVo, rowIndexByGosha, instlDao, aLatestStructure, alDao)

            '2014/12/23 カラム初期化処理追加。上のnew 処理ではカラムが初期状態（DB内の状態）になってしまうため
            '　　　　　　　　　　　　　　　 　現状のカラムがDB内の情報より少ない場合、下の処理を行っても余分なカラムが発生してしまう。
            For Each colIndex As Integer In result.GetInputColumnIndexes(TITLE_ROW_INDEX)
                result.RemoveColumn(colIndex, 1)
            Next
            For Each colIndex As Integer In GetInputInstlHinbanColumnIndexes()
                result.InstlHinban(colIndex) = InstlHinban(colIndex)
                result.InstlHinbanKbn(colIndex) = InstlHinbanKbn(colIndex)
                result.InstlDataKbn(colIndex) = InstlDataKbn(colIndex)
                result.BaseInstlFlg(colIndex) = BaseInstlFlg(colIndex)
            Next

            '員数をコピー'
            For Each rowindex As Integer In GetInputRowIndexes()
                '列数'
                For Each colIndex As Integer In GetInputInstlHinbanColumnIndexes()
                    result.InsuSuryo(rowindex, colIndex) = InsuSuryo(rowindex, colIndex)
                    '2014/12/23 追加
                    result.InstlHinban(colIndex) = InstlHinban(colIndex)
                    result.InstlHinbanKbn(colIndex) = InstlHinbanKbn(colIndex)
                    result.InstlDataKbn(colIndex) = InstlDataKbn(colIndex)
                    result.BaseInstlFlg(colIndex) = BaseInstlFlg(colIndex)
                Next
            Next
            Return result
        End Function




#Region "レコード操作"

        Private _recordDimension As New IndexedList(Of IndexedList(Of TShisakuSekkeiBlockInstlVo))
        Private _StructureResults As New IndexedList(Of StructureResult)(False)

        ''' <summary>装備情報</summary>
        ''' <param name="rowIndex">行No</param>
        ''' <param name="columnIndex">列No</param>
        ''' <returns>装備情報</returns>
        ''' <remarks></remarks>
        Private ReadOnly Property Record(ByVal rowIndex As Integer, ByVal columnIndex As Integer) As TShisakuSekkeiBlockInstlVo
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
        Public Function GetInputRowIndexesWithTitle() As ICollection(Of Integer)
            Return _recordDimension.Keys
        End Function

        ''' <summary>
        ''' 入力行の行Noの一覧を返す（タイトル行を除く） ※順不同
        ''' </summary>
        ''' <returns>入力行の行Noの一覧</returns>
        ''' <remarks></remarks>
        Public Function GetInputRowIndexes() As ICollection(Of Integer)
            Dim results As New List(Of Integer)
            For Each rowIndex As Integer In _recordDimension.Keys
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
        Public Function GetInputInstlHinbanColumnIndexes() As ICollection(Of Integer)
            Dim results As New List(Of Integer)
            For Each columnIndex As Integer In GetInputColumnIndexes(TITLE_ROW_INDEX)
                If StringUtil.IsEmpty(InstlHinban(columnIndex)) AndAlso StringUtil.IsEmpty(InstlHinbanKbn(columnIndex)) Then
                    Continue For
                End If
                results.Add(columnIndex)
            Next
            Return results
        End Function

        Public Sub RemoveInstlHinbanColumn(ByVal columnIndex As Integer)
            Me.RemoveColumn(columnIndex, TITLE_ROW_INDEX)
        End Sub



        ''' <summary>
        ''' 員数未入力の列タイトルの列No一覧を返す
        ''' </summary>
        ''' <returns>員数未入力の列タイトルの列No一覧</returns>
        ''' <remarks></remarks>
        Private Function GetUninputInsuInstlHinbanColumnIndexes() As List(Of Integer)
            Dim results As New List(Of Integer)(GetInputInstlHinbanColumnIndexes())
            For Each rowIndex As Integer In GetInputRowIndexes()
                For Each columnIndex As Integer In GetInputInsuColumnIndexes(rowIndex)
                    If Not results.Contains(columnIndex) Then
                        Continue For
                    End If
                    results.Remove(columnIndex)
                Next
            Next
            Return results
        End Function

        ''' <summary>
        ''' 最初は員数を入力していて、いまは未入力になったセルのデータを返す
        ''' </summary>
        ''' <param name="columnIndex">列index</param>
        ''' <returns>今は未入力のセル</returns>
        ''' <remarks>今も昔も員数未入力なら、最初の号車の行に空のセルデータを作成して返す</remarks>
        Private Function GetUninputInsuReocrd(ByVal columnIndex) As TShisakuSekkeiBlockInstlVo
            Dim result As TShisakuSekkeiBlockInstlVo = Nothing
            For Each rowIndex As Integer In GetInputRowIndexes()
                For Each columnIndex2 As Integer In GetInputColumnIndexes(rowIndex)
                    If columnIndex <> columnIndex2 Then
                        Continue For
                    End If
                    result = Record(rowIndex, columnIndex)
                    ' TODO これなんで、員数じゃなく、INSTL品番なんだ？要調査。
                    If Not StringUtil.IsEmpty(result.InstlHinban) Then
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
        ''' 行に入力した列No一覧を返す
        ''' </summary>
        ''' <param name="rowIndex">行index</param>
        ''' <returns>入力した列No一覧</returns>
        ''' <remarks></remarks>
        Public Function GetInputColumnIndexes(ByVal rowIndex As Integer) As ICollection(Of Integer)
            Return _recordDimension.Value(rowIndex).Keys
        End Function

        ''' <summary>
        ''' 員数が入力された列の列No一覧を返す
        ''' </summary>
        ''' <param name="rowIndex">行No</param>
        ''' <returns>入力した列の列No一覧</returns>
        ''' <remarks></remarks>
        Public Function GetInputInsuColumnIndexes(ByVal rowIndex As Integer) As ICollection(Of Integer)
            Dim results As New List(Of Integer)
            For Each columnIndex As Integer In _recordDimension.Value(rowIndex).Keys
                If InsuSuryo(rowIndex, columnIndex) Is Nothing Then
                    Continue For
                End If
                results.Add(columnIndex)
            Next
            Return results
        End Function

        ''' <summary>員数</summary>
        ''' <param name="rowIndex">行No</param>
        ''' <param name="columnIndex">列No</param>
        Public Property InsuSuryo(ByVal rowIndex As Integer, ByVal columnIndex As Integer) As Integer?
            Get
                Return Record(rowIndex, columnIndex).InsuSuryo
            End Get
            Set(ByVal value As Integer?)
                Record(rowIndex, columnIndex).InsuSuryo = value
            End Set
        End Property

        ''' <summary>INSTL品番</summary>
        ''' <param name="columnIndex">列No</param>
        Public Property InstlHinban(ByVal columnIndex As Integer) As String
            Get
                Return Record(TITLE_ROW_INDEX, columnIndex).InstlHinban
            End Get
            Set(ByVal value As String)
                Record(TITLE_ROW_INDEX, columnIndex).InstlHinban = value
                '' INSTL品番が変わったら、基本F品番をクリアして再取得を促す
                BfBuhinNo(columnIndex) = Nothing
            End Set
        End Property

        ''' <summary>INSTL品番区分</summary>
        ''' <param name="columnIndex">列No</param>
        Public Property InstlHinbanKbn(ByVal columnIndex As Integer) As String
            Get
                Return Record(TITLE_ROW_INDEX, columnIndex).InstlHinbanKbn
            End Get
            Set(ByVal value As String)
                Record(TITLE_ROW_INDEX, columnIndex).InstlHinbanKbn = value
                '' INSTL品番区分が変わったら、基本F品番をクリアして再取得を促す
                BfBuhinNo(columnIndex) = Nothing
            End Set
        End Property

        ''↓↓2014/07/25 Ⅰ.3.設計編集 ベース改修専用化_o) (TES)張 ADD BEGIN
        ''' <summary>INSTLデータ区分</summary>
        ''' <param name="columnIndex">列No</param>
        Public Property BaseInstlFlg(ByVal columnIndex As Integer) As String
            Get
                Return Record(TITLE_ROW_INDEX, columnIndex).BaseInstlFlg
            End Get
            Set(ByVal value As String)
                Record(TITLE_ROW_INDEX, columnIndex).BaseInstlFlg = value
                '' INSTLデータ区分が変わったら、基本F品番をクリアして再取得を促す
                BfBuhinNo(columnIndex) = Nothing
            End Set
        End Property
        ''↑↑2014/07/25 Ⅰ.3.設計編集 ベース改修専用化_o) (TES)張 ADD END


        Public Property InstlDataKbn(ByVal columnIndex As Integer) As String
            Get
                Return Record(TITLE_ROW_INDEX, columnIndex).InstlDataKbn
            End Get
            Set(ByVal value As String)
                Record(TITLE_ROW_INDEX, columnIndex).InstlDataKbn = value
                '' INSTLデータ区分が変わったら、基本F品番をクリアして再取得を促す
                BfBuhinNo(columnIndex) = Nothing
            End Set

        End Property

        ''' <summary>基本F品番</summary>
        ''' <param name="columnIndex">列No</param>
        Friend Property BfBuhinNo(ByVal columnIndex As Integer) As String
            Get
                Return Record(TITLE_ROW_INDEX, columnIndex).BfBuhinNo
            End Get
            Set(ByVal value As String)
                Record(TITLE_ROW_INDEX, columnIndex).BfBuhinNo = value
                '' 基本F品番が変わったら、構成の情報をクリアして再取得を促す
                StructureResult(columnIndex) = Nothing
            End Set
        End Property

        ''' <summary>構成の情報</summary>
        ''' <param name="columnIndex">列No</param>
        Protected Friend Property StructureResult(ByVal columnIndex As Integer) As StructureResult
            Get
                Return _StructureResults(columnIndex)
            End Get
            Set(ByVal value As StructureResult)
                _StructureResults(columnIndex) = value
            End Set
        End Property

#End Region

        ''' <summary>
        ''' INTSL情報を自身に反映する
        ''' </summary>
        ''' <param name="instlVos">試作設計ブロックINSTL情報の一覧</param>
        ''' <remarks></remarks>
        Public Sub Apply(ByVal instlVos As List(Of TShisakuSekkeiBlockInstlVo))
            ''↓↓2014/08/29 Ⅰ.3.設計編集 ベース改修専用化_ap) 酒井 ADD BEGIN
            'Private Sub Apply(ByVal instlVos As List(Of TShisakuSekkeiBlockInstlVo))
            ''↑↑2014/08/29 Ⅰ.3.設計編集 ベース改修専用化_ap) 酒井 ADD END

            For Each vo As TShisakuSekkeiBlockInstlVo In instlVos
                If Not rowIndexByGosha.ContainsKey(vo.ShisakuGousya) Then
                    Throw New ShisakuException("試作設計ブロックINSTL情報に未知の号車を検出しました. " & vo.ShisakuGousya)
                End If
                Dim columnIndex As Integer = vo.InstlHinbanHyoujiJun - TShisakuSekkeiBlockInstlVoHelper.InstlHinbanHyoujiJun.START_VALUE
                ' 員数情報をset
                VoUtil.CopyProperties(vo, Record(rowIndexByGosha(vo.ShisakuGousya), columnIndex))
                ' INSTL品番が未ならset
                If StringUtil.IsEmpty(Record(TITLE_ROW_INDEX, columnIndex).InstlHinban) Then
                    VoUtil.CopyProperties(vo, Record(TITLE_ROW_INDEX, columnIndex))
                End If
            Next
        End Sub

        Private Function FindInstlBy() As List(Of TShisakuSekkeiBlockInstlVo)

            Dim param As New TShisakuSekkeiBlockInstlVo
            param.ShisakuEventCode = _blockKeyVo.ShisakuEventCode
            'param.ShisakuBukaCode = _blockKeyVo.ShisakuBukaCode
            param.ShisakuBlockNo = _blockKeyVo.ShisakuBlockNo
            param.ShisakuBlockNoKaiteiNo = _blockKeyVo.ShisakuBlockNoKaiteiNo
            Return instlDao.FindBy(param)
        End Function

        ''↓↓2014/08/28 Ⅰ.5.EBOM差分出力 酒井 ADD BEGIN

        Private Function FindInstlEbomKanshiBy() As List(Of TShisakuSekkeiBlockInstlEbomKanshiVo)

            Dim param As New TShisakuSekkeiBlockInstlEbomKanshiVo
            param.ShisakuEventCode = _blockKeyVo.ShisakuEventCode
            param.ShisakuBukaCode = _blockKeyVo.ShisakuBukaCode
            param.ShisakuBlockNo = _blockKeyVo.ShisakuBlockNo
            param.ShisakuBlockNoKaiteiNo = _blockKeyVo.ShisakuBlockNoKaiteiNo
            Return instlEbomKanshiDao.FindBy(param)
        End Function
        ''↑↑2014/08/28 Ⅰ.5.EBOM差分出力 酒井 ADD END
        ''' <summary>
        ''' 情報を読み込む
        ''' </summary>
        ''' <remarks></remarks>
        Private Sub ReadRecord()
            Dim vos As List(Of BuhinEditAlEventVo) = alDao.FindEventInfoById(Me._blockKeyVo.ShisakuEventCode)
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
        Public Function MakeValues() As List(Of TShisakuSekkeiBlockInstlVo)
            Dim goshaByRowNoIndexes As New Dictionary(Of Integer, String)
            For Each gosha As String In rowIndexByGosha.Keys
                goshaByRowNoIndexes.Add(rowIndexByGosha(gosha), gosha)
            Next

            Dim results As New List(Of TShisakuSekkeiBlockInstlVo)

            Dim firstRowIndex As Integer = GetInputRowFirstIndex()
            Dim firstGosha As String = goshaByRowNoIndexes(firstRowIndex)
            For Each columnIndex As Integer In GetUninputInsuInstlHinbanColumnIndexes()
                Dim instlVo As TShisakuSekkeiBlockInstlVo = GetUninputInsuReocrd(columnIndex)
                results.Add(instlVo)
                SettingRecordTo(instlVo, firstGosha, columnIndex)
            Next

            Dim inputInstlHinbanColumnIndexes As New List(Of Integer)(GetInputInstlHinbanColumnIndexes())

            For Each rowIndex As Integer In GetInputRowIndexes()

                '号車がブランクの場合は処理対象外とする。
                If Not StringUtil.IsEmpty(Records(rowIndex).ShisakuGousya) Then
                    Dim gosha As String = goshaByRowNoIndexes(rowIndex)
                    If gosha = "W/B303" Then
                        Dim ddd As String = ""
                    End If

                    For Each columnIndex As Integer In GetInputInsuColumnIndexes(rowIndex)

                        '員数が入っていてINSTL品番が未入力の場合、例外ではＮＧ
                        '員数＝有、INSTL品番＝無の場合、読み飛ばしにする（登録対象外）
                        '　2011/03/03　Ｂｙ柳沼
                        If inputInstlHinbanColumnIndexes.IndexOf(columnIndex) < 0 Then
                            ' INSTL品番が未入力の列だから、DB登録出来ないから、例外
                            'Throw New InvalidOperationException("INSTL品番・区分が未入力なのに員数が入力されている")
                            Dim a As String = ""
                        Else
                            '員数がないなら登録しない'
                            If Not StringUtil.IsEmpty(Record(rowIndex, columnIndex).InsuSuryo) Or Record(rowIndex, columnIndex).InsuSuryo Is Nothing Then
                                Dim instlVo As TShisakuSekkeiBlockInstlVo = Record(rowIndex, columnIndex)
                                results.Add(instlVo)
                                SettingRecordTo(instlVo, gosha, columnIndex)
                            End If
                        End If

                    Next
                End If

            Next

            Return results
        End Function

        ''' <summary>
        ''' 更新データの中身を設定する
        ''' </summary>
        ''' <param name="instlVo">更新データ</param>
        ''' <param name="gosha">データの号車</param>
        ''' <param name="columnIndex">データの列index</param>
        ''' <remarks></remarks>
        Private Sub SettingRecordTo(ByVal instlVo As TShisakuSekkeiBlockInstlVo, ByVal gosha As String, ByVal columnIndex As Integer)

            If StringUtil.IsEmpty(instlVo.InstlHinban) Then
                instlVo.ShisakuEventCode = _blockKeyVo.ShisakuEventCode
                instlVo.ShisakuBukaCode = _blockKeyVo.ShisakuBukaCode
                instlVo.ShisakuBlockNo = _blockKeyVo.ShisakuBlockNo
                instlVo.ShisakuBlockNoKaiteiNo = _blockKeyVo.ShisakuBlockNoKaiteiNo
                instlVo.ShisakuGousya = gosha
            End If
            instlVo.InstlHinbanHyoujiJun = columnIndex + TShisakuSekkeiBlockInstlVoHelper.InstlHinbanHyoujiJun.START_VALUE
            instlVo.InstlHinban = InstlHinban(columnIndex)
            instlVo.InstlHinbanKbn = StringUtil.Nvl(InstlHinbanKbn(columnIndex))
            ''↓↓2014/08/22 1)EBOM-新試作手配システム過去データの組み合わせ抽出 酒井 ADD BEGIN
            If StringUtil.IsNotEmpty(InstlDataKbn(columnIndex)) Then
                instlVo.InstlDataKbn = InstlDataKbn(columnIndex)
            Else
                instlVo.InstlDataKbn = "0"
            End If
            If StringUtil.IsNotEmpty(BaseInstlFlg(columnIndex)) Then
                instlVo.BaseInstlFlg = BaseInstlFlg(columnIndex)
            Else
                instlVo.BaseInstlFlg = "0"
            End If
            ''↑↑2014/08/22 1)EBOM-新試作手配システム過去データの組み合わせ抽出 酒井 ADD END
            If StringUtil.IsEmpty(BfBuhinNo(columnIndex)) Then
                DetectStructureResult(columnIndex)
            End If
            instlVo.BfBuhinNo = BfBuhinNo(columnIndex)
        End Sub

        ''' <summary>
        ''' 更新する
        ''' </summary>
        ''' <param name="info">ログイン情報</param>
        ''' <param name="aDate">試作日付</param>
        ''' <remarks></remarks>
        Public Sub Update(ByVal info As LoginInfo, ByVal aDate As ShisakuDate)

            Dim param As New TShisakuSekkeiBlockInstlVo
            param.ShisakuEventCode = _blockKeyVo.ShisakuEventCode
            param.ShisakuBukaCode = _blockKeyVo.ShisakuBukaCode
            param.ShisakuBlockNo = _blockKeyVo.ShisakuBlockNo
            param.ShisakuBlockNoKaiteiNo = _blockKeyVo.ShisakuBlockNoKaiteiNo
            instlDao.DeleteBy(param)

            Dim values As List(Of TShisakuSekkeiBlockInstlVo) = MakeValues()
            For Each vo As TShisakuSekkeiBlockInstlVo In values
                If StringUtil.IsEmpty(vo.CreatedUserId) Then
                    vo.CreatedUserId = info.UserId
                    vo.CreatedDate = aDate.CurrentDateDbFormat
                    vo.CreatedTime = aDate.CurrentTimeDbFormat
                End If
                vo.SaisyuKoushinbi = DateUtil.ConvDateToIneteger(aDate.CurrentDateTime)
                vo.UpdatedUserId = info.UserId
                vo.UpdatedDate = aDate.CurrentDateDbFormat
                vo.UpdatedTime = aDate.CurrentTimeDbFormat
                instlDao.InsertBy(vo)
            Next
        End Sub

        ''' <summary>
        ''' 「構成の情報」を探索する
        ''' </summary>
        ''' <param name="columnIndex">INSTL列index</param>
        ''' <remarks></remarks>
        Public Sub DetectStructureResult(ByVal columnIndex As Integer)
            StructureResult(columnIndex) = aLatestStructure.Compute(InstlHinban(columnIndex), InstlHinbanKbn(columnIndex), True)
            If StructureResult(columnIndex).IsEBom Then
                Record(TITLE_ROW_INDEX, columnIndex).BfBuhinNo = StructureResult(columnIndex).BuhinNo
            End If
        End Sub

        ''' <summary>
        ''' 「構成の情報」を探索する(イベントコピー用)
        ''' </summary>
        ''' <param name="columnIndex">INSTL列index</param>
        ''' <remarks></remarks>
        Public Sub DetectStructureResult(ByVal columnIndex As Integer, ByVal shisakuEventCode As String)
            copyEventCode = shisakuEventCode
            'イベントなので試作固定'
            StructureResult(columnIndex) = aLatestStructure.Compute(InstlHinban(columnIndex), InstlHinbanKbn(columnIndex), True, "SHISAKU", "", shisakuEventCode)
            'イベントコピー先が空の場合だとエラーが発生するので'
            If Not StructureResult(columnIndex) Is Nothing Then
                If StructureResult(columnIndex).IsEBom Then
                    Record(TITLE_ROW_INDEX, columnIndex).BfBuhinNo = StructureResult(columnIndex).BuhinNo
                End If
            End If
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
            Me._StructureResults.Insert(columnIndex, insertCount)
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
            Me._StructureResults.Remove(columnIndex, removeCount)
        End Sub

    End Class
End Namespace