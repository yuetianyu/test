Imports ShisakuCommon
Imports ShisakuCommon.Db.EBom.Dao
Imports EventSakusei.ShisakuBuhinMenu.Dao
Imports ShisakuCommon.Db.EBom.Vo.Helper
Imports ShisakuCommon.Util.Grouping
Imports ShisakuCommon.DateUtil
Imports ShisakuCommon.Db.EBom.Vo
Imports EventSakusei.ShisakuBuhinEdit.Logic
Imports EventSakusei.ShisakuBuhinEdit.Al.Dao
Imports EventSakusei.ShisakuBuhinEdit.Al.Logic
Imports ShisakuCommon.Db.EBom.Dao.Impl
Imports EventSakusei.ShisakuBuhinEdit.Logic.Detect

Namespace ShisakuBuhinMenu.Logic.Impl.Tenkai

    'A/L用(実験クラス)'
    Public Class BuhinEditInstlSupplier

        Private Shared ReadOnly DEFAULT_SOUBI_VOS As List(Of TShisakuSekkeiBlockSoubiVo)

        Private _blockKeyVo As TShisakuSekkeiBlockVo
        Private ReadOnly login As LoginInfo
        Private ReadOnly aShisakuDate As ShisakuDate
        Private ReadOnly anEzSync As EzSyncInstlHinban
        Private ReadOnly soubiDao As TShisakuSekkeiBlockSoubiDao
        Private ReadOnly soubiShiyoDao As TShisakuSekkeiBlockSoubiShiyouDao
        Private ReadOnly alDao As BuhinEditAlDao

        Private _showColumnBag As BuhinEditAlShowColumnBag

        Private alEvent As BuhinEditAlEvent
        Private alBasicOption As BuhinEditAlOption
        Private alSpecialOption As BuhinEditAlOption

        Private memoSupplier As BuhinEditAlMemoSupplier
        Private instlSupplier As BuhinEditAlInstlSupplier

        ''' <summary>
        ''' コンストラクタ
        ''' </summary>
        ''' <param name="blockKeyVo">表示対象の試作設計ブロック情報</param>
        ''' <param name="login">ログイン情報</param>
        ''' <param name="aShisakuDate">試作日付</param>
        ''' <param name="soubiDao">試作設計ブロック装備Dao</param>
        ''' <param name="soubiShiyoDao">試作設計ブロック装備仕様Dao</param>
        ''' <param name="alDao">A/L表示機能Dao</param>
        ''' <remarks></remarks>
        Public Sub New(ByVal blockKeyVo As TShisakuSekkeiBlockVo, _
                       ByVal login As LoginInfo, _
                       ByVal aShisakuDate As ShisakuDate, _
                       ByVal anEzSync As EzSyncInstlHinban, _
                       ByVal soubiDao As TShisakuSekkeiBlockSoubiDao, _
                       ByVal soubiShiyoDao As TShisakuSekkeiBlockSoubiShiyouDao, _
                       ByVal alDao As BuhinEditAlDao)

            Me._blockKeyVo = blockKeyVo
            Me.login = login
            Me.aShisakuDate = aShisakuDate
            Me.anEzSync = anEzSync
            Me.soubiDao = soubiDao
            Me.soubiShiyoDao = soubiShiyoDao
            Me.alDao = alDao

            '_showColumnBag = MakeShowColumnBag(blockKeyVo)

            Me.alEvent = New BuhinEditAlEvent(blockKeyVo.ShisakuEventCode, Nothing, alDao)

            Dim instlDao As TShisakuSekkeiBlockInstlDao = New TShisakuSekkeiBlockInstlDaoImpl
            ''↓↓2014/08/28 Ⅰ.5.EBOM差分出力 酒井 ADD BEGIN
            Dim instlEbomKanshiDao As TShisakuSekkeiBlockInstlEbomKanshiDao = New TShisakuSekkeiBlockInstlEbomKanshiDaoImpl
            'Me.instlSupplier = New BuhinEditAlInstlSupplier(blockKeyVo, alEvent.GetRowNoByGoshaIndexes, instlDao, New DetectLatestStructureImpl(blockKeyVo), alDao)
            Me.instlSupplier = New BuhinEditAlInstlSupplier(blockKeyVo, alEvent.GetRowNoByGoshaIndexes, instlDao, New DetectLatestStructureImpl(blockKeyVo), alDao, instlEbomKanshiDao, login)
            ''↑↑2014/08/28 Ⅰ.5.EBOM差分出力 酒井 ADD END

            '仕様情報の画面表示用にイベント情報の最終更新日と部品表の最終更新日を取得します。
            '部品表の最終更新日を取得します。
            If Not StringUtil.IsEmpty(blockKeyVo.SaisyuKoushinbi) Then
                _SaisyuKoushinbi = blockKeyVo.SaisyuKoushinbi
            Else
                _SaisyuKoushinbi = Nothing
            End If
            'イベントコードで試作イベント情報を抽出する。
            'イベントの最終更新日を取得するためにイベントのDAO,VO追加
            Dim eventDao As TShisakuEventDao = New TShisakuEventDaoImpl
            Dim eventVo As TShisakuEventVo
            eventVo = eventDao.FindByPk(blockKeyVo.ShisakuEventCode)
            'イベントの最終更新日を取得します。
            If Not StringUtil.IsEmpty(eventVo.UpdatedDate) Then
                'ハイフンを取って返す。
                Dim hyphenNonDate As String = ConvHyphenYyyymmddToYyyymmdd(eventVo.UpdatedDate)
                _EventUpdatedDate = hyphenNonDate
            Else
                _EventUpdatedDate = Nothing
            End If

        End Sub
        
        '/*** 20140911 CHANGE START ***/
        ''' <summary>
        ''' コンストラクタ
        ''' </summary>
        ''' <param name="blockKeyVo">表示対象の試作設計ブロック情報</param>
        ''' <param name="login">ログイン情報</param>
        ''' <param name="aShisakuDate">試作日付</param>
        ''' <param name="soubiDao">試作設計ブロック装備Dao</param>
        ''' <param name="soubiShiyoDao">試作設計ブロック装備仕様Dao</param>
        ''' <param name="alDao">A/L表示機能Dao</param>
        ''' <param name="eventUpdatedDate">更新日</param>
        ''' <param name="alEventList">A/Lイベントリスト</param>
        ''' <remarks></remarks>
        Public Sub New(ByVal blockKeyVo As TShisakuSekkeiBlockVo, _
                       ByVal login As LoginInfo, _
                       ByVal aShisakuDate As ShisakuDate, _
                       ByVal anEzSync As EzSyncInstlHinban, _
                       ByVal soubiDao As TShisakuSekkeiBlockSoubiDao, _
                       ByVal soubiShiyoDao As TShisakuSekkeiBlockSoubiShiyouDao, _
                       ByVal alDao As BuhinEditAlDao, _
                       ByVal eventUpdatedDate As String, _
                       ByVal alEventList As List(Of BuhinEditAlEventVo))

            Me._blockKeyVo = blockKeyVo
            Me.login = login
            Me.aShisakuDate = aShisakuDate
            Me.anEzSync = anEzSync
            Me.soubiDao = soubiDao
            Me.soubiShiyoDao = soubiShiyoDao
            Me.alDao = alDao

            '_showColumnBag = MakeShowColumnBag(blockKeyVo)

            '20140911 CHANGE 引数追加
            'Me.alEvent = New BuhinEditAlEvent(blockKeyVo.ShisakuEventCode, Nothing, alDao)
            Me.alEvent = New BuhinEditAlEvent(blockKeyVo.ShisakuEventCode, Nothing, alDao, alEventList)

            Dim instlDao As TShisakuSekkeiBlockInstlDao = New TShisakuSekkeiBlockInstlDaoImpl
            Dim instlEbomKanshiDao As TShisakuSekkeiBlockInstlEbomKanshiDao = New TShisakuSekkeiBlockInstlEbomKanshiDaoImpl
            '20140911 CHANGE 引数追加
            Me.instlSupplier = New BuhinEditAlInstlSupplier(blockKeyVo, alEvent.GetRowNoByGoshaIndexes, instlDao, New DetectLatestStructureImpl(blockKeyVo), alDao, alEventList, instlEbomKanshiDao, login)

            
            '仕様情報の画面表示用にイベント情報の最終更新日と部品表の最終更新日を取得します。
            '部品表の最終更新日を取得します。
            If Not StringUtil.IsEmpty(blockKeyVo.SaisyuKoushinbi) Then
                _SaisyuKoushinbi = blockKeyVo.SaisyuKoushinbi
            Else
                _SaisyuKoushinbi = Nothing
            End If
            'イベントコードで試作イベント情報を抽出する。
            If Not StringUtil.IsEmpty(eventUpdatedDate) Then
                'ハイフンを取って返す。
                Dim hyphenNonDate As String = ConvHyphenYyyymmddToYyyymmdd(eventUpdatedDate)
                _EventUpdatedDate = hyphenNonDate
            Else
                _EventUpdatedDate = Nothing
            End If

        End Sub
        '/*** 20140911 CHANGE END ***/
        
        

        ''' <summary>
        ''' 装備品列表示情報を作成する
        ''' </summary>
        ''' <param name="blockVo">試作設計ブロック情報</param>
        ''' <returns>装備品列表示情報</returns>
        ''' <remarks></remarks>
        Private Function MakeShowColumnBag(ByVal blockVo As TShisakuSekkeiBlockVo) As BuhinEditAlShowColumnBag

            Dim soubiVos As List(Of TShisakuSekkeiBlockSoubiVo) = FindSoubi(blockVo)
            Dim soubiShiyouVos As List(Of TShisakuSekkeiBlockSoubiShiyouVo) = FindSoubiShiyo(blockVo)
            If soubiVos.Count = 0 AndAlso soubiShiyouVos.Count = 0 Then
                soubiVos = DEFAULT_SOUBI_VOS
            End If

            Return New BuhinEditAlShowColumnBag(soubiVos, soubiShiyouVos)
        End Function

        ''' <summary>
        ''' ブロックNoを差し替える
        ''' </summary>
        ''' <param name="blockVo">ブロックVo</param>
        ''' <remarks></remarks>
        Public Sub SupersedeBlockVo(ByVal blockVo As TShisakuSekkeiBlockVo)
            Me._blockKeyVo = blockVo
            instlSupplier.SupersedeBlockVo(blockVo)

            ShowColumnBag = MakeShowColumnBag(blockVo)
        End Sub

        ''' <summary>表示列情報</summary>
        Public Property ShowColumnBag() As BuhinEditAlShowColumnBag
            Get
                Return _showColumnBag
            End Get
            Set(ByVal value As BuhinEditAlShowColumnBag)
                If _showColumnBag Is value Then
                    Return
                End If
                _showColumnBag = value
                alEvent.ShowColumnInfos = value.SoubiVos
                alBasicOption.ShowColumnInfos = value.SoubiShiyouVos
                alSpecialOption.ShowColumnInfos = value.SoubiShiyouVos
                'SetChanged()
            End Set
        End Property

        ''' <summary>
        ''' 登録する
        ''' </summary>
        ''' <remarks></remarks>
        Public Sub Register()
            '** 設計ブロック装備情報／設計ブロック装備仕様情報 **
            ' 削除
            DeleteSoubiByEventCode(_blockKeyVo.ShisakuEventCode, _
                                   _blockKeyVo.ShisakuBukaCode, _
                                   _blockKeyVo.ShisakuBlockNo, _
                                   _blockKeyVo.ShisakuBlockNoKaiteiNo)

            DeleteSoubiShiyouByEventCode(_blockKeyVo.ShisakuEventCode, _
                                   _blockKeyVo.ShisakuBukaCode, _
                                   _blockKeyVo.ShisakuBlockNo, _
                                   _blockKeyVo.ShisakuBlockNoKaiteiNo)

            ' 挿入
            InsertSoubis(_showColumnBag.SoubiVos)
            InsertSoubiShiyous(_showColumnBag.SoubiShiyouVos)

            memoSupplier.Update(login, aShisakuDate)
            instlSupplier.Update(login, aShisakuDate)
        End Sub

        ''' <summary>
        ''' 試作設計ブロック装備仕様情報を追加する
        ''' </summary>
        ''' <param name="insertValues">追加する情報</param>
        ''' <remarks></remarks>
        Private Sub InsertSoubiShiyous(ByVal insertValues As List(Of TShisakuSekkeiBlockSoubiShiyouVo))

            For Each vo As TShisakuSekkeiBlockSoubiShiyouVo In insertValues
                vo.ShisakuEventCode = _blockKeyVo.ShisakuEventCode
                vo.ShisakuBukaCode = _blockKeyVo.ShisakuBukaCode
                vo.ShisakuBlockNo = _blockKeyVo.ShisakuBlockNo
                vo.ShisakuBlockNoKaiteiNo = _blockKeyVo.ShisakuBlockNoKaiteiNo
                If StringUtil.IsEmpty(vo.CreatedUserId) Then
                    vo.CreatedUserId = login.UserId
                    vo.CreatedDate = aShisakuDate.CurrentDateDbFormat
                    vo.CreatedTime = aShisakuDate.CurrentTimeDbFormat
                End If
                vo.UpdatedUserId = login.UserId
                vo.UpdatedDate = aShisakuDate.CurrentDateDbFormat
                vo.UpdatedTime = aShisakuDate.CurrentTimeDbFormat

                soubiShiyoDao.InsertBy(vo)
            Next
        End Sub

        ''' <summary>
        ''' 試作設計ブロック装備情報を追加する
        ''' </summary>
        ''' <param name="insertValues">追加する情報</param>
        ''' <remarks></remarks>
        Private Sub InsertSoubis(ByVal insertValues As List(Of TShisakuSekkeiBlockSoubiVo))

            For Each vo As TShisakuSekkeiBlockSoubiVo In insertValues
                vo.ShisakuEventCode = _blockKeyVo.ShisakuEventCode
                vo.ShisakuBukaCode = _blockKeyVo.ShisakuBukaCode
                vo.ShisakuBlockNo = _blockKeyVo.ShisakuBlockNo
                vo.ShisakuBlockNoKaiteiNo = _blockKeyVo.ShisakuBlockNoKaiteiNo
                If StringUtil.IsEmpty(vo.CreatedUserId) Then
                    vo.CreatedUserId = login.UserId
                    vo.CreatedDate = aShisakuDate.CurrentDateDbFormat
                    vo.CreatedTime = aShisakuDate.CurrentTimeDbFormat
                End If
                vo.UpdatedUserId = login.UserId
                vo.UpdatedDate = aShisakuDate.CurrentDateDbFormat
                vo.UpdatedTime = aShisakuDate.CurrentTimeDbFormat

                soubiDao.InsertBy(vo)
            Next
        End Sub

        ''' <summary>
        ''' 試作設計ブロック装備仕様情報を削除する
        ''' </summary>
        ''' <param name="shisakuEventCode">試作イベントコード</param>
        ''' <remarks></remarks>
        Private Sub DeleteSoubiShiyouByEventCode(ByVal shisakuEventCode As String, _
                                           ByVal ShisakuBukaCode As String, _
                                           ByVal ShisakuBlockNo As String, _
                                           ByVal ShisakuBlockNoKaiteiNo As String)

            Dim param2 As New TShisakuSekkeiBlockSoubiShiyouVo
            '以下のキーで削除する。
            param2.ShisakuEventCode = shisakuEventCode
            param2.ShisakuBukaCode = ShisakuBukaCode
            param2.ShisakuBlockNo = ShisakuBlockNo
            param2.ShisakuBlockNoKaiteiNo = ShisakuBlockNoKaiteiNo

            soubiShiyoDao.DeleteBy(param2)
        End Sub

        ''' <summary>
        ''' 試作設計ブロック装備情報を削除する
        ''' </summary>
        ''' <param name="shisakuEventCode">試作イベントコード</param>
        ''' <remarks></remarks>
        Private Sub DeleteSoubiByEventCode(ByVal shisakuEventCode As String, _
                                           ByVal ShisakuBukaCode As String, _
                                           ByVal ShisakuBlockNo As String, _
                                           ByVal ShisakuBlockNoKaiteiNo As String)

            Dim param As New TShisakuSekkeiBlockSoubiVo
            '以下のキーで削除する。
            param.ShisakuEventCode = shisakuEventCode
            param.ShisakuBukaCode = ShisakuBukaCode
            param.ShisakuBlockNo = ShisakuBlockNo
            param.ShisakuBlockNoKaiteiNo = ShisakuBlockNoKaiteiNo

            soubiDao.DeleteBy(param)
        End Sub

        ''' <summary>
        ''' 入力行の行Noの一覧を返す
        ''' </summary>
        ''' <returns>入力行の行Noの一覧</returns>
        ''' <remarks></remarks>
        Public Function GetInputRowIndexes() As ICollection(Of Integer)
            Return alEvent.GetInputRowNos
        End Function

        ''' <summary>
        ''' 入力したメモタイトルの列No一覧を返す
        ''' </summary>
        ''' <returns>入力したメモタイトルの列No一覧</returns>
        ''' <remarks></remarks>
        Public Function GetInputMemoTitleColumnIndexes() As ICollection(Of Integer)
            Return memoSupplier.GetInputTitleColumnIndexes
        End Function

        ''' <summary>
        ''' 入力した適用の列No一覧を返す
        ''' </summary>
        ''' <param name="rowIndex">行index</param>
        ''' <returns>入力した適用の列No一覧</returns>
        ''' <remarks></remarks>
        Public Function GetInputMemoTekiyouColumnIndexes(ByVal rowIndex As Integer) As ICollection(Of Integer)
            Return memoSupplier.GetInputTekiyouColumnIndexes(rowIndex)
        End Function

        ''' <summary>
        ''' 入力した列タイトルの列No一覧を返す
        ''' </summary>
        ''' <returns>入力した列タイトルの列No一覧</returns>
        ''' <remarks></remarks>
        Public Function GetInputInstlHinbanColumnIndexes() As ICollection(Of Integer)
            Return instlSupplier.GetInputInstlHinbanColumnIndexes
        End Function

        ''' <summary>
        ''' 入力した列の列No一覧を返す
        ''' </summary>
        ''' <param name="rowIndex">行No</param>
        ''' <returns>入力した列の列No一覧</returns>
        ''' <remarks></remarks>
        Public Function GetInputInsuColumnIndexes(ByVal rowIndex As Integer) As ICollection(Of Integer)
            Return instlSupplier.GetInputColumnIndexes(rowIndex)
        End Function

#Region "公開プロパティ"

        ' イベントの更新日
        Private _EventUpdatedDate As String
        ' 部品表の最終更新日
        Private _SaisyuKoushinbi As String

        ''' <summary>イベントの更新日</summary>
        ''' <value>イベントの更新日</value>
        ''' <returns>イベントの更新日</returns>
        Public Property EventUpdatedDate() As String
            Get
                Return _EventUpdatedDate
            End Get
            Set(ByVal value As String)
                _EventUpdatedDate = value
            End Set
        End Property

        ''' <summary>部品表の最終更新日</summary>
        ''' <value>部品表の最終更新日</value>
        ''' <returns>部品表の最終更新日</returns>
        Public Property SaisyuKoushinbi() As String
            Get
                Return _SaisyuKoushinbi
            End Get
            Set(ByVal value As String)
                _SaisyuKoushinbi = value
            End Set
        End Property


        ''' <summary>号車</summary>
        ''' <param name="rowNo">行No</param>
        Public ReadOnly Property ShisakuGosha(ByVal rowNo As Integer) As String
            Get
                Return alEvent.ShisakuGosha(rowNo)
            End Get
        End Property

        ''' <summary>動的列（イベント情報・装備品）の列数</summary>
        Public ReadOnly Property DynamicColumnCount() As Integer
            Get
                Return alEvent.ColumnCount + alBasicOption.ColumnCount + alSpecialOption.ColumnCount
            End Get
        End Property

        ''' <summary>
        ''' 動的列（イベント情報・装備品）の内容
        ''' </summary>
        ''' <param name="rowNo">行No</param>
        ''' <param name="columnIndex">列index</param>
        ''' <returns>内容</returns>
        ''' <remarks></remarks>
        Public ReadOnly Property DynamicInfo(ByVal rowNo As Integer, ByVal columnIndex As Integer) As String
            Get
                If columnIndex < alEvent.ColumnCount Then
                    Return alEvent.Info(rowNo, columnIndex)
                ElseIf columnIndex < alEvent.ColumnCount + alBasicOption.ColumnCount Then
                    Return alBasicOption.Info(rowNo, columnIndex - alEvent.ColumnCount)
                Else
                    Return alSpecialOption.Info(rowNo, columnIndex - alEvent.ColumnCount - alBasicOption.ColumnCount)
                End If
            End Get
        End Property

        ''' <summary>メモ列の列数</summary>
        Public Property MemoColumnCount() As Integer
            Get
                Return memoSupplier.ColumnCount
            End Get
            Set(ByVal value As Integer)
                memoSupplier.ColumnCount = value
            End Set
        End Property

        ''' <summary>適用</summary>
        ''' <param name="rowIndex">行index</param>
        ''' <param name="columnIndex">列index</param>
        Public Property MemoTekiyou(ByVal rowIndex As Integer, ByVal columnIndex As Integer) As String
            Get
                Return memoSupplier.Tekiyou(rowIndex, columnIndex)
            End Get
            Set(ByVal value As String)
                memoSupplier.Tekiyou(rowIndex, columnIndex) = value
            End Set
        End Property

        ''' <summary>メモ</summary>
        ''' <param name="columnIndex">列index</param>
        Public Property MemoTitle(ByVal columnIndex As Integer) As String
            Get
                Return memoSupplier.MemoTitle(columnIndex)
            End Get
            Set(ByVal value As String)
                memoSupplier.MemoTitle(columnIndex) = value
            End Set
        End Property


        ''' <summary>員数</summary>
        ''' <param name="rowIndex">行No</param>
        ''' <param name="columnIndex">列No</param>
        Public Property InsuSuryo(ByVal rowIndex As Integer, ByVal columnIndex As Integer) As String
            Get
                Return BuhinEditInsu.ConvDbToForm(instlSupplier.InsuSuryo(rowIndex, columnIndex))
            End Get
            Set(ByVal val As String)
                Dim value As Integer? = BuhinEditInsu.ConvFormToDb(val)
                If EzUtil.IsEqualIfNull(instlSupplier.InsuSuryo(rowIndex, columnIndex), value) Then
                    Return
                End If
                instlSupplier.InsuSuryo(rowIndex, columnIndex) = value
                'SetChanged()
            End Set
        End Property
        ''' <summary>INSTL品番</summary>
        ''' <param name="columnIndex">列No</param>
        Public Property InstlHinban(ByVal columnIndex As Integer) As String
            Get
                Return instlSupplier.InstlHinban(columnIndex)
            End Get
            Set(ByVal value As String)
                If EzUtil.IsEqualIfNull(instlSupplier.InstlHinban(columnIndex), value) Then
                    Return
                End If
                instlSupplier.InstlHinban(columnIndex) = value
                'SetChanged()
                anEzSync.NotifyInstlHinban(columnIndex)
            End Set
        End Property

        ''' <summary>INSTL品番の列数</summary>
        Public ReadOnly Property InstlHinbanCount() As Integer
            Get
                Dim result As Integer = 0
                For Each i As Integer In GetInputInstlHinbanColumnIndexes()
                    result += 1
                Next
                Return result
            End Get
        End Property

        ''' <summary>INSTL品番区分</summary>
        ''' <param name="columnIndex">列No</param>
        Public Property InstlHinbanKbn(ByVal columnIndex As Integer) As String
            Get
                Return instlSupplier.InstlHinbanKbn(columnIndex)
            End Get
            Set(ByVal value As String)
                If EzUtil.IsEqualIfNull(instlSupplier.InstlHinbanKbn(columnIndex), value) Then
                    Return
                End If
                instlSupplier.InstlHinbanKbn(columnIndex) = value
                'SetChanged()
                anEzSync.NotifyInstlHinbanKbn(columnIndex)
            End Set
        End Property

        ''↓↓2014/08/22 1)EBOM-新試作手配システム過去データの組み合わせ抽出 酒井 ADD BEGIN
        ''' <summary>INSTL品番区分</summary>
        ''' <param name="columnIndex">列No</param>
        Public Property InstlDataKbn(ByVal columnIndex As Integer) As String
            Get
                Return instlSupplier.InstlDataKbn(columnIndex)
            End Get
            Set(ByVal value As String)
                If EzUtil.IsEqualIfNull(instlSupplier.InstlDataKbn(columnIndex), value) Then
                    Return
                End If
                instlSupplier.InstlDataKbn(columnIndex) = value
                'SetChanged()
                anEzSync.NotifyInstlDataKbn(columnIndex)
            End Set
        End Property
        ''↑↑2014/08/22 1)EBOM-新試作手配システム過去データの組み合わせ抽出 酒井 ADD END
        '↓↓2014/08/15 Ⅰ.3.設計編集 ベース改修専用化_t) (TES)張 ADD BEGIN
        ''' <summary>ベース情報フラグ</summary>
        ''' <param name="columnIndex">列No</param>
        Public Property BaseInstlFlg(ByVal columnIndex As Integer) As String
            Get
                Return instlSupplier.BaseInstlFlg(columnIndex)
            End Get
            Set(ByVal value As String)
                If EzUtil.IsEqualIfNull(instlSupplier.BaseInstlFlg(columnIndex), value) Then
                    Return
                End If
                instlSupplier.BaseInstlFlg(columnIndex) = value
                'SetChanged()
                anEzSync.NotifyBaseInstlFlg(columnIndex)
            End Set
        End Property
        ''↑↑2014/08/15 Ⅰ.3.設計編集 ベース改修専用化_t) (TES)張 ADD END

        ''' <summary>構成の情報</summary>
        ''' <param name="columnIndex">列No</param>
        Public ReadOnly Property StructureResult(ByVal columnIndex As Integer) As StructureResult
            Get
                Return instlSupplier.StructureResult(columnIndex)
            End Get
        End Property


        ''' <summary>イベント情報列の列数</summary>
        Public ReadOnly Property EventColumnCount() As Integer
            Get
                Return alEvent.ColumnCount
            End Get
        End Property

        ''' <summary>イベント情報のタイトル</summary>
        ''' <param name="columnIndex">列No</param>
        Public ReadOnly Property EventTitle(ByVal columnIndex As Integer) As String
            Get
                Return alEvent.Title(columnIndex)
            End Get
        End Property

        ''' <summary>
        ''' イベント情報の内容
        ''' </summary>
        ''' <param name="rowNo">行No</param>
        ''' <param name="columnIndex">列index</param>
        ''' <returns>内容</returns>
        ''' <remarks></remarks>
        Public ReadOnly Property EventInfo(ByVal rowNo As Integer, ByVal columnIndex As Integer) As String
            Get
                Return alEvent.Info(rowNo, columnIndex)
            End Get
        End Property

        ''' <summary>
        ''' ベース車情報の列数を返す
        ''' </summary>
        ''' <returns>ベース車情報の列数</returns>
        ''' <remarks></remarks>
        Public Function GetEventColumnCountBase() As Integer
            Return alEvent.GetColumnCountBase
        End Function

        ''' <summary>装備品情報の列数</summary>
        Public ReadOnly Property OptionColumnCount() As Integer
            Get
                Return alBasicOption.ColumnCount + alSpecialOption.ColumnCount
            End Get
        End Property

        ''' <summary>基本装備仕様の列数</summary>
        Public ReadOnly Property BasicOptionColumnCount() As Integer
            Get
                Return alBasicOption.ColumnCount
            End Get
        End Property

        ''' <summary>特別装備仕様の列数</summary>
        Public ReadOnly Property SpecialOptionColumnCount() As Integer
            Get
                Return alSpecialOption.ColumnCount
            End Get
        End Property

        ''' <summary>装備品情報のタイトル</summary>
        ''' <param name="columnIndex">列No</param>
        Public ReadOnly Property OptionTitle(ByVal columnIndex As Integer) As String
            Get
                If columnIndex < alBasicOption.ColumnCount Then
                    Return alBasicOption.Title(columnIndex)
                Else
                    Return alSpecialOption.Title(columnIndex - alBasicOption.ColumnCount)
                End If
            End Get
        End Property

        ''' <summary>
        ''' 装備品情報の内容
        ''' </summary>
        ''' <param name="rowNo">行No</param>
        ''' <param name="columnIndex">列index</param>
        ''' <returns>内容</returns>
        ''' <remarks></remarks>
        Public ReadOnly Property OptionInfo(ByVal rowNo As Integer, ByVal columnIndex As Integer) As String
            Get
                If columnIndex < alBasicOption.ColumnCount Then
                    Return alBasicOption.Info(rowNo, columnIndex)
                Else
                    Return alSpecialOption.Info(rowNo, columnIndex - alBasicOption.ColumnCount)
                End If
            End Get
        End Property
#End Region

        Private Class SoubiComparerable : Implements IComparer(Of TShisakuSekkeiBlockSoubiVo)
            Public Function Compare(ByVal x As ShisakuCommon.Db.EBom.Vo.TShisakuSekkeiBlockSoubiVo, ByVal y As ShisakuCommon.Db.EBom.Vo.TShisakuSekkeiBlockSoubiVo) As Integer Implements System.Collections.Generic.IComparer(Of ShisakuCommon.Db.EBom.Vo.TShisakuSekkeiBlockSoubiVo).Compare
                Return x.ShisakuSoubiHyoujiJun.CompareTo(y.ShisakuSoubiHyoujiJun)
            End Function
        End Class

        Private Function FindSoubi(ByVal blockVo As TShisakuSekkeiBlockVo) As List(Of TShisakuSekkeiBlockSoubiVo)
            Dim param As New TShisakuSekkeiBlockSoubiVo
            param.ShisakuEventCode = blockVo.ShisakuEventCode
            param.ShisakuBukaCode = blockVo.ShisakuBukaCode
            param.ShisakuBlockNo = blockVo.ShisakuBlockNo
            param.ShisakuBlockNoKaiteiNo = blockVo.ShisakuBlockNoKaiteiNo
            Dim results As List(Of TShisakuSekkeiBlockSoubiVo) = soubiDao.FindBy(param)
            If results.Count = 0 Then
                Return results
            End If
            results.Sort(New SoubiComparerable)
            Return results
        End Function

        Private Class SoubiShiyoComparerable : Implements IComparer(Of TShisakuSekkeiBlockSoubiShiyouVo)
            Public Function Compare(ByVal x As ShisakuCommon.Db.EBom.Vo.TShisakuSekkeiBlockSoubiShiyouVo, ByVal y As ShisakuCommon.Db.EBom.Vo.TShisakuSekkeiBlockSoubiShiyouVo) As Integer Implements System.Collections.Generic.IComparer(Of ShisakuCommon.Db.EBom.Vo.TShisakuSekkeiBlockSoubiShiyouVo).Compare
                If x.ShisakuSoubiKbn Is Nothing OrElse y.ShisakuSoubiKbn Is Nothing Then
                    Throw New InvalidOperationException("試作装備表示順は、NotNull項目なのに、Nullを検知した")
                End If
                If x.ShisakuSoubiHyoujiJun Is Nothing OrElse y.ShisakuSoubiHyoujiJun Is Nothing Then
                    Throw New InvalidOperationException("試作装備表示順は、NotNull項目なのに、Nullを検知した")
                End If
                Dim result As Integer = x.ShisakuSoubiKbn.CompareTo(y.ShisakuSoubiKbn)
                If result <> 0 Then
                    Return result
                End If
                Return CInt(x.ShisakuSoubiHyoujiJun).CompareTo(CInt(y.ShisakuSoubiHyoujiJun))
            End Function
        End Class

        Private Function FindSoubiShiyo(ByVal blockVo As TShisakuSekkeiBlockVo) As List(Of TShisakuSekkeiBlockSoubiShiyouVo)
            Dim param As New TShisakuSekkeiBlockSoubiShiyouVo
            param.ShisakuEventCode = blockVo.ShisakuEventCode
            param.ShisakuBukaCode = blockVo.ShisakuBukaCode
            param.ShisakuBlockNo = blockVo.ShisakuBlockNo
            param.ShisakuBlockNoKaiteiNo = blockVo.ShisakuBlockNoKaiteiNo
            Dim results As List(Of TShisakuSekkeiBlockSoubiShiyouVo) = soubiShiyoDao.FindBy(param)
            If results.Count = 0 Then
                Return results
            End If
            results.Sort(New SoubiShiyoComparerable)
            Return results
        End Function

        'Public Sub DetectStructureResults()
        '    instlSupplier.DetectStructureResults()
        'End Sub

        Public Sub DetectStructureResult(ByVal columnIndex As Integer)
            instlSupplier.DetectStructureResult(columnIndex)
        End Sub

        ''' <summary>
        ''' INSTL品番列に列挿入する
        ''' </summary>
        ''' <param name="columnIndex">INSTL品番の中の列index</param>
        ''' <param name="insertCount">挿入列数</param>
        ''' <remarks></remarks>
        Public Sub InsertColumnInInstl(ByVal columnIndex As Integer, ByVal insertCount As Integer)
            instlSupplier.InsertColumn(columnIndex, insertCount)
            anEzSync.InsertColumnInInstl(columnIndex, insertCount)
        End Sub

        ''' <summary>
        ''' INSTL品番列を列削除する
        ''' </summary>
        ''' <param name="columnIndex">INSTL品番の中の列index</param>
        ''' <param name="removeCount">削除列数</param>
        ''' <remarks></remarks>
        Public Sub RemoveColumnInInstl(ByVal columnIndex As Integer, ByVal removeCount As Integer)
            instlSupplier.RemoveColumn(columnIndex, removeCount)
            anEzSync.RemoveColumnInInstl(columnIndex, removeCount)
        End Sub

        ''' <summary>
        ''' メモ欄に列挿入する
        ''' </summary>
        ''' <param name="columnIndex">メモ欄の中の列index</param>
        ''' <param name="insertCount">挿入列数</param>
        ''' <remarks></remarks>
        Public Sub InsertColumnInMemo(ByVal columnIndex As Integer, ByVal insertCount As Integer)
            memoSupplier.InsertColumn(columnIndex, insertCount)
        End Sub

        ''' <summary>
        ''' メモ欄を列削除する
        ''' </summary>
        ''' <param name="columnIndex">メモ欄の中の列index</param>
        ''' <param name="removeCount">削除列数</param>
        ''' <remarks></remarks>
        Public Sub RemoveColumnInMemo(ByVal columnIndex As Integer, ByVal removeCount As Integer)
            memoSupplier.RemoveColumn(columnIndex, removeCount)
        End Sub


    End Class
End Namespace