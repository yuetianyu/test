Imports ShisakuCommon.Db.EBom
Imports ShisakuCommon
Imports ShisakuCommon.Db.EBom.Dao
Imports ShisakuCommon.Db.EBom.Vo
Imports ShisakuCommon.Util
Imports YosansyoTool.YosanBuhinEdit.Logic
Imports YosansyoTool.YosanBuhinEdit.Logic.Detect
Imports YosansyoTool.YosanBuhinEdit.Kosei.Logic.Matrix
Imports YosansyoTool.YosanBuhinEdit.Kosei.Ui

Namespace YosanBuhinEdit.Kosei.Logic
    ''' <summary>
    ''' 部品構成編集画面の表示全般を担うクラス
    ''' </summary>
    ''' <remarks></remarks>
    Public Class BuhinEditKoseiSubject : Inherits Observable

        Private _koseiMatrix As BuhinKoseiMatrix

        Private yosanEvent As TYosanEventVo
        Private yosanEventCode As String
        Private yosanUnitKbn As String

        Private ReadOnly login As LoginInfo
        Private ReadOnly aShisakuDate As ShisakuDate
        Private ReadOnly detector As DetectLatestStructure

        Private ReadOnly editDao As TYosanBuhinEditDao
        Private ReadOnly editInsuDao As TYosanBuhinEditInsuDao
        Private ReadOnly editPatternDao As TYosanBuhinEditPatternDao
        Private ReadOnly editRirekiDao As TYosanBuhinEditRirekiDao
        Private ReadOnly editInsuRirekiDao As TYosanBuhinEditInsuRirekiDao
        Private ReadOnly editPatternRirekiDao As TYosanBuhinEditPatternRirekiDao

        Private ReadOnly make As MakeStructureResult
        Private ReadOnly aMakerNameResolver As MakerNameResolver

        Private isWaitingKoseiTenkai As Boolean
        Private Jikyu As String

        Private _patternVos As List(Of TYosanBuhinEditPatternVo)

        '設計展開時：0, 構成再展開、最新化、部品構成呼び出し時：1, 子部品展開時：2'
        Private a0553flag As Integer

#Region "Recordの各プロパティ"
        Public ReadOnly Property Matrix() As BuhinKoseiMatrix
            Get
                Return _koseiMatrix
            End Get
        End Property
        Private ReadOnly Property Record(ByVal rowIndex As Integer) As BuhinKoseiRecordVo
            Get
                Return _koseiMatrix.Record(rowIndex)
            End Get
        End Property

        ''' <summary>
        ''' 行indexを返す(順昇順)
        ''' </summary>
        ''' <returns>行indexのコレクション</returns>
        Public Function GetInputRowIndexes() As ICollection(Of Integer)
            Return _koseiMatrix.GetInputRowIndexes()
        End Function

        ''' <summary>
        ''' 行indexの最大値を返す
        ''' </summary>
        ''' <returns>行indexの最大値</returns>
        Public Function GetMaxInputRowIndex() As Integer
            Return _koseiMatrix.GetMaxInputRowIndex()
        End Function

        ''' <summary>
        ''' 新規行indexを返す
        ''' </summary>
        ''' <returns>新規行index</returns>
        ''' <remarks></remarks>
        Private Function GetRecordNewRowIndex() As Integer

            Return _koseiMatrix.GetNewRowIndex
        End Function

        Public ReadOnly Property InsuColumnIndexes(ByVal rowIndex As Integer) As ICollection(Of Integer)
            Get
                Return _koseiMatrix.GetInputInsuColumnIndexesOnRow(rowIndex)
            End Get
        End Property

        ''' <summary>
        ''' 員数を返す
        ''' </summary>
        ''' <param name="rowIndex">行index</param>
        ''' <param name="columnIndex">列index</param>
        ''' <value>員数</value>
        ''' <returns>員数</returns>
        Public Property InsuSuryo(ByVal rowIndex As Integer, ByVal columnIndex As Integer) As String
            Get
                Return BuhinEditInsu.ConvDbToForm(_koseiMatrix.InsuSuryo(rowIndex, columnIndex))
            End Get
            Set(ByVal val As String)
                Dim value As Integer? = BuhinEditInsu.ConvFormToDb(val)
                If EzUtil.IsEqualIfNull(_koseiMatrix.InsuSuryo(rowIndex, columnIndex), value) Then
                    Return
                End If
                _koseiMatrix.InsuSuryo(rowIndex, columnIndex) = value
                SetChanged()
            End Set
        End Property

        ''' <summary>予算部課コード</summary>
        ''' <value>予算部課コード</value>
        ''' <returns>予算部課コード</returns>
        Public Property YosanBukaCode(ByVal rowIndex As Integer) As String
            Get
                Return Record(rowIndex).YosanBukaCode
            End Get
            Set(ByVal value As String)
                If EzUtil.IsEqualIfNull(Record(rowIndex).YosanBukaCode, value) Then
                    Return
                End If
                Record(rowIndex).YosanBukaCode = value
                SetChanged()
            End Set
        End Property

        ''' <summary>予算ブロック№</summary>
        ''' <value>予算ブロック№</value>
        ''' <returns>予算ブロック№</returns>
        Public Property YosanBlockNo(ByVal rowIndex As Integer) As String
            Get
                Return Record(rowIndex).YosanBlockNo
            End Get
            Set(ByVal value As String)
                If EzUtil.IsEqualIfNull(Record(rowIndex).YosanBlockNo, value) Then
                    Return
                End If
                Record(rowIndex).YosanBlockNo = value
                SetChanged()
            End Set
        End Property

        ''' <summary>レベル</summary>
        ''' <value>レベル</value>
        ''' <returns>レベル</returns>
        Public Property YosanLevel(ByVal rowIndex As Integer) As Int32?
            Get
                Return Record(rowIndex).YosanLevel
            End Get
            Set(ByVal value As Int32?)
                If EzUtil.IsEqualIfNull(Record(rowIndex).YosanLevel, value) Then
                    Return
                End If
                Record(rowIndex).YosanLevel = value
                SetChanged()
            End Set
        End Property

        ''' <summary>国内集計コード</summary>
        ''' <value>国内集計コード</value>
        ''' <returns>国内集計コード</returns>
        Public Property YosanShukeiCode(ByVal rowIndex As Integer) As String
            Get
                Return Record(rowIndex).YosanShukeiCode
            End Get
            Set(ByVal value As String)
                If EzUtil.IsEqualIfNull(Record(rowIndex).YosanShukeiCode, value) Then
                    Return
                End If
                Record(rowIndex).YosanShukeiCode = value
                SetChanged()
            End Set
        End Property

        ''' <summary>海外SIA集計コード</summary>
        ''' <value>海外SIA集計コード</value>
        ''' <returns>海外SIA集計コード</returns>
        Public Property YosanSiaShukeiCode(ByVal rowIndex As Integer) As String
            Get
                Return Record(rowIndex).YosanSiaShukeiCode
            End Get
            Set(ByVal value As String)
                If EzUtil.IsEqualIfNull(Record(rowIndex).YosanSiaShukeiCode, value) Then
                    Return
                End If
                Record(rowIndex).YosanSiaShukeiCode = value
                SetChanged()
            End Set
        End Property

        ''' <summary>取引先コード</summary>
        ''' <value>取引先コード</value>
        ''' <returns>取引先コード</returns>
        Public Property YosanMakerCode(ByVal rowIndex As Integer) As String
            Get
                Return Record(rowIndex).YosanMakerCode
            End Get
            Set(ByVal value As String)
                If EzUtil.IsEqualIfNull(Record(rowIndex).YosanMakerCode, value) Then
                    Return
                End If
                Record(rowIndex).YosanMakerCode = value
                SetChanged()
            End Set
        End Property

        ''' <summary>取引先名称</summary>
        ''' <value>取引先名称</value>
        ''' <returns>取引先名称</returns>
        Public Property YosanMakerName(ByVal rowIndex As Integer) As String
            Get
                Return Record(rowIndex).YosanMakerName
            End Get
            Set(ByVal value As String)
                If EzUtil.IsEqualIfNull(Record(rowIndex).YosanMakerName, value) Then
                    Return
                End If
                Record(rowIndex).YosanMakerName = value
                SetChanged()
            End Set
        End Property

        ''' <summary>部品番号</summary>
        ''' <value>部品番号</value>
        ''' <returns>部品番号</returns>
        Public Property YosanBuhinNo(ByVal rowIndex As Integer) As String
            Get
                Return Record(rowIndex).YosanBuhinNo
            End Get
            Set(ByVal value As String)
                If EzUtil.IsEqualIfNull(Record(rowIndex).YosanBuhinNo, value) Then
                    Return
                End If
                Record(rowIndex).YosanBuhinNo = value
                SetChanged()
            End Set
        End Property

        ''' <summary>部品番号試作区分</summary>
        ''' <value>部品番号試作区分</value>
        ''' <returns>部品番号試作区分</returns>
        Public Property YosanBuhinNoKbn(ByVal rowIndex As Integer) As String
            Get
                Return Record(rowIndex).YosanBuhinNoKbn
            End Get
            Set(ByVal value As String)
                If EzUtil.IsEqualIfNull(Record(rowIndex).YosanBuhinNoKbn, value) Then
                    Return
                End If
                Record(rowIndex).YosanBuhinNoKbn = value
                SetChanged()
            End Set
        End Property

        ''' <summary>部品名称</summary>
        ''' <value>部品名称</value>
        ''' <returns>部品名称</returns>
        Public Property YosanBuhinName(ByVal rowIndex As Integer) As String
            Get
                Return Record(rowIndex).YosanBuhinName
            End Get
            Set(ByVal value As String)
                If EzUtil.IsEqualIfNull(Record(rowIndex).YosanBuhinName, value) Then
                    Return
                End If
                Record(rowIndex).YosanBuhinName = value
                SetChanged()
            End Set
        End Property

        ''' <summary>供給セクション</summary>
        ''' <value>供給セクション</value>
        ''' <returns>供給セクション</returns>
        Public Property YosanKyoukuSection(ByVal rowIndex As Integer) As String
            Get
                Return Record(rowIndex).YosanKyoukuSection
            End Get
            Set(ByVal value As String)
                If EzUtil.IsEqualIfNull(Record(rowIndex).YosanKyoukuSection, value) Then
                    Return
                End If
                Record(rowIndex).YosanKyoukuSection = value
                SetChanged()
            End Set
        End Property

        ''' <summary>員数</summary>
        ''' <value>員数</value>
        ''' <returns>員数</returns>
        Public Property YosanInsu(ByVal rowIndex As Integer) As Integer?
            Get
                Return Record(rowIndex).YosanInsu
            End Get
            Set(ByVal value As Integer?)
                If EzUtil.IsEqualIfNull(Record(rowIndex).YosanInsu, value) Then
                    Return
                End If
                Record(rowIndex).YosanInsu = value
                SetChanged()
            End Set
        End Property

        ''' <summary>変更概要</summary>
        ''' <value>変更概要</value>
        ''' <returns>変更概要</returns>
        Public Property YosanHenkoGaiyo(ByVal rowIndex As Integer) As String
            Get
                Return Record(rowIndex).YosanHenkoGaiyo
            End Get
            Set(ByVal value As String)
                If EzUtil.IsEqualIfNull(Record(rowIndex).YosanHenkoGaiyo, value) Then
                    Return
                End If
                Record(rowIndex).YosanHenkoGaiyo = value
                SetChanged()
            End Set
        End Property

        ''' <summary>部品費（量産）</summary>
        ''' <value>部品費（量産）</value>
        ''' <returns>部品費（量産）</returns>
        Public Property YosanBuhinHiRyosan(ByVal rowIndex As Integer) As Decimal?
            Get
                Return Record(rowIndex).YosanBuhinHiRyosan
            End Get
            Set(ByVal value As Decimal?)
                If EzUtil.IsEqualIfNull(Record(rowIndex).YosanBuhinHiRyosan, value) Then
                    Return
                End If
                Record(rowIndex).YosanBuhinHiRyosan = value
                SetChanged()

            End Set
        End Property

        ''' <summary>部品費（部品表）</summary>
        ''' <value>部品費（部品表）</value>
        ''' <returns>部品費（部品表）</returns>
        Public Property YosanBuhinHiBuhinhyo(ByVal rowIndex As Integer) As Decimal?
            Get
                Return Record(rowIndex).YosanBuhinHiBuhinhyo
            End Get
            Set(ByVal value As Decimal?)
                If EzUtil.IsEqualIfNull(Record(rowIndex).YosanBuhinHiBuhinhyo, value) Then
                    Return
                End If
                Record(rowIndex).YosanBuhinHiBuhinhyo = value
                SetChanged()

            End Set
        End Property

        ''' <summary>部品費（特記）</summary>
        ''' <value>部品費（特記）</value>
        ''' <returns>部品費（特記）</returns>
        Public Property YosanBuhinHiTokki(ByVal rowIndex As Integer) As Decimal?
            Get
                Return Record(rowIndex).YosanBuhinHiTokki
            End Get
            Set(ByVal value As Decimal?)
                If EzUtil.IsEqualIfNull(Record(rowIndex).YosanBuhinHiTokki, value) Then
                    Return
                End If
                Record(rowIndex).YosanBuhinHiTokki = value
                SetChanged()
            End Set
        End Property

        ''' <summary>型費</summary>
        ''' <value>型費</value>
        ''' <returns>型費</returns>
        Public Property YosanKataHi(ByVal rowIndex As Integer) As Decimal?
            Get
                Return Record(rowIndex).YosanKataHi
            End Get
            Set(ByVal value As Decimal?)
                If EzUtil.IsEqualIfNull(Record(rowIndex).YosanKataHi, value) Then
                    Return
                End If
                Record(rowIndex).YosanKataHi = value
                SetChanged()
            End Set
        End Property

        ''' <summary>
        ''' 治具費
        ''' </summary>
        ''' <param name="rowIndex"></param>
        ''' <value>治具費</value>
        ''' <returns>治具費</returns>
        ''' <remarks></remarks>
        Public Property YosanJiguHi(ByVal rowIndex As Integer) As Decimal?
            Get
                Return Record(rowIndex).YosanJiguHi
            End Get
            Set(ByVal value As Decimal?)
                If EzUtil.IsEqualIfNull(Record(rowIndex).YosanJiguHi, value) Then
                    Return
                End If
                Record(rowIndex).YosanJiguHi = value
                SetChanged()
            End Set
        End Property

        ''' <summary>
        ''' 工数
        ''' </summary>
        ''' <param name="rowIndex"></param>
        ''' <value>工数</value>
        ''' <returns>工数</returns>
        ''' <remarks></remarks>
        Public Property YosanKosu(ByVal rowIndex As Integer) As Decimal?
            Get
                Return Record(rowIndex).YosanKosu
            End Get
            Set(ByVal value As Decimal?)
                If EzUtil.IsEqualIfNull(Record(rowIndex).YosanKosu, value) Then
                    Return
                End If
                Record(rowIndex).YosanKosu = value
                SetChanged()
            End Set
        End Property

        ''' <summary>
        ''' 発注実績(割付値全体と平均値)
        ''' </summary>
        ''' <param name="rowIndex"></param>
        ''' <value>発注実績(割付値全体と平均値)</value>
        ''' <returns>発注実績(割付値全体と平均値)</returns>
        ''' <remarks></remarks>
        Public Property YosanHachuJisekiMix(ByVal rowIndex As Integer) As Decimal?
            Get
                Return Record(rowIndex).YosanHachuJisekiMix
            End Get
            Set(ByVal value As Decimal?)
                If EzUtil.IsEqualIfNull(Record(rowIndex).YosanHachuJisekiMix, value) Then
                    Return
                End If
                Record(rowIndex).YosanHachuJisekiMix = value
                SetChanged()
            End Set
        End Property

#End Region

        ''' 参照モードかを保持
        Private _isViewerMode As Boolean
        ''' <summary>参照モードか</summary>
        ''' <value>参照モードか</value>
        ''' <returns>参照モードか</returns>
        Public Property IsViewerMode() As Boolean
            Get
                Return _isViewerMode
            End Get
            Set(ByVal value As Boolean)
                If EzUtil.IsEqualIfNull(_isViewerMode, value) Then
                    Return
                End If
                _isViewerMode = value
                SetChanged()
            End Set
        End Property

        ''' <summary>
        ''' コンストラクタ
        ''' </summary>
        ''' <param name="yosanEventCode"></param>
        ''' <param name="yosanUnitKbn"></param>
        ''' <param name="login"></param>
        ''' <param name="aShisakuDate"></param>
        ''' <param name="detector"></param>
        ''' <param name="yosanEventVo"></param>
        ''' <param name="makeStructure"></param>
        ''' <param name="aMakerNameResolver"></param>
        ''' <param name="editDao"></param>
        ''' <param name="editInsuDao"></param>
        ''' <param name="editPatternDao"></param>
        ''' <remarks></remarks>
        Public Sub New(ByVal yosanEventCode As String, ByVal yosanUnitKbn As String, _
                       ByVal login As LoginInfo, ByVal aShisakuDate As ShisakuDate, _
                       ByVal detector As DetectLatestStructure, ByVal yosanEventVo As TYosanEventVo, _
                       ByVal makeStructure As MakeStructureResult, _
                       ByVal aMakerNameResolver As MakerNameResolver, _
                       ByVal editDao As TYosanBuhinEditDao, _
                       ByVal editInsuDao As TYosanBuhinEditInsuDao, _
                       ByVal editPatternDao As TYosanBuhinEditPatternDao, _
                       ByVal editRirekiDao As TYosanBuhinEditRirekiDao, _
                       ByVal editInsuRirekiDao As TYosanBuhinEditInsuRirekiDao, _
                       ByVal editPatternRirekiDao As TYosanBuhinEditPatternRirekiDao)
            Me.yosanEvent = yosanEventVo
            Me.login = login
            Me.aShisakuDate = aShisakuDate
            Me.detector = detector
            Me.make = makeStructure
            Me.aMakerNameResolver = aMakerNameResolver

            Me.yosanEventCode = yosanEventCode
            Me.yosanUnitKbn = yosanUnitKbn

            Jikyu = ""

            Me.editDao = editDao
            Me.editInsuDao = editInsuDao
            Me.editPatternDao = editPatternDao
            Me.editRirekiDao = editRirekiDao
            Me.editInsuRirekiDao = editInsuRirekiDao
            Me.editPatternRirekiDao = editPatternRirekiDao

            '予算部品編集情報取得
            Dim _editVos As List(Of TYosanBuhinEditVo) = FindEditBy(yosanEventCode, yosanUnitKbn)
            '予算部品編集員数情報取得
            Dim _editInsuVos As List(Of TYosanBuhinEditInsuVo) = FindEditInsuBy(yosanEventCode, yosanUnitKbn)
            'For Each Vo As TYosanBuhinEditVo In editVos
            '    Vo.YosanBuhinNo = Vo.YosanBuhinNo.TrimEnd
            'Next

            '予算部品編集情報を取得できた場合
            If 0 < _editVos.Count Then
                '予算部品編集パターン情報取得
                _patternVos = FindEditPatternBy(yosanEventCode, yosanUnitKbn)
                '予算部品編集員数情報取得
                _koseiMatrix = New BuhinKoseiMatrix(_editVos, _editInsuVos)
            Else
                '_koseiMatrix = New BuhinKoseiMatrix

                isWaitingKoseiTenkai = True

                Dim editVo As New TYosanBuhinEditVo
                editVo.YosanEventCode = yosanEventCode
                editVo.BuhinhyoName = yosanUnitKbn
                editVo.BuhinNoHyoujiJun = 0
                _editVos = New List(Of TYosanBuhinEditVo)()
                _editVos.Add(editVo)

                Dim editInsuVo As New TYosanBuhinEditInsuVo
                editInsuVo.YosanEventCode = yosanEventCode
                editInsuVo.BuhinhyoName = yosanUnitKbn
                editInsuVo.BuhinNoHyoujiJun = 0
                editInsuVo.PatternHyoujiJun = 0
                _editInsuVos = New List(Of TYosanBuhinEditInsuVo)()
                _editInsuVos.Add(editInsuVo)

                Dim patternVo As New TYosanBuhinEditPatternVo
                patternVo.YosanEventCode = yosanEventCode
                patternVo.BuhinhyoName = yosanUnitKbn
                patternVo.PatternHyoujiJun = 0
                patternVo.PatternName = ""
                _patternVos = New List(Of TYosanBuhinEditPatternVo)()
                _patternVos.Add(patternVo)

                '
                '_koseiMatrix = New BuhinKoseiMatrix
                '予算部品編集員数情報取得
                _koseiMatrix = New BuhinKoseiMatrix(_editVos, _editInsuVos)
            End If


            SetChanged()
        End Sub

        ''' <summary>
        ''' 予算部品編集情報を取得
        ''' </summary>
        ''' <param name="yosanEventCode"></param>
        ''' <param name="yosanUnitKbn"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Private Function FindEditBy(ByVal yosanEventCode As String, ByVal yosanUnitKbn As String) As List(Of TYosanBuhinEditVo)
            'Dim param As New TYosanBuhinEditVo

            'param.YosanEventCode = yosanEventCode
            'param.UnitKbn = yosanUnitKbn
            'Return editDao.FindBy(param)

            Dim sql As New System.Text.StringBuilder()
            With sql
                .Remove(0, .Length)
                .AppendLine("SELECT * ")
                .AppendLine("FROM " & MBOM_DB_NAME & ".dbo.T_YOSAN_BUHIN_EDIT ")
                .AppendLine("WHERE YOSAN_EVENT_CODE = @YosanEventCode ")
                .AppendLine("AND BUHINHYO_NAME = @BuhinhyoName ")
                .AppendLine("ORDER BY YOSAN_BLOCK_NO, BUHIN_NO_HYOUJI_JUN")
            End With

            Dim db As New EBomDbClient
            Dim param As New TYosanBuhinEditVo
            param.YosanEventCode = yosanEventCode
            param.BuhinhyoName = yosanUnitKbn
            Return db.QueryForList(Of TYosanBuhinEditVo)(sql.ToString, param)

        End Function

        ''' <summary>
        ''' 予算部品編集員数情報を取得
        ''' </summary>
        ''' <param name="yosanEventCode"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Private Function FindEditInsuBy(ByVal yosanEventCode As String, ByVal yosanUnitKbn As String) As List(Of TYosanBuhinEditInsuVo)
            Dim param As New TYosanBuhinEditInsuVo

            param.YosanEventCode = yosanEventCode
            param.BuhinhyoName = yosanUnitKbn
            Return editInsuDao.FindBy(param)
        End Function

        ''' <summary>
        ''' 予算部品編集員数情報を取得
        ''' </summary>
        ''' <param name="yosanEventCode"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Private Function FindEditPatternBy(ByVal yosanEventCode As String, ByVal yosanUnitKbn As String) As List(Of TYosanBuhinEditPatternVo)
            Dim param As New TYosanBuhinEditPatternVo

            param.YosanEventCode = yosanEventCode
            param.BuhinhyoName = yosanUnitKbn
            Return editPatternDao.FindBy(param)
        End Function

        ''' <summary>
        ''' 登録する
        ''' </summary>
        ''' <remarks></remarks>
        Public Sub Register()

            Dim editSupplier As New BuhinEditKoseiEditSupplier(yosanEventCode, yosanUnitKbn, _koseiMatrix)
            editSupplier.Update(login, editDao, editRirekiDao, aShisakuDate)

            Dim editInstlSupplier As New BuhinEditKoseiEditInstlSupplier(yosanEventCode, yosanUnitKbn, _koseiMatrix, _patternVos)
            editInstlSupplier.Update(login, editInsuDao, editInsuRirekiDao, editPatternDao, editPatternRirekiDao, aShisakuDate)

        End Sub

        ''' <summary>
        ''' 自給品の存在する行を削除する
        ''' </summary>
        ''' <param name="columnBag">列構成情報</param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Function removeJikyu(ByVal columnBag As InstlColumnBag) As InstlColumnBag
            If columnBag Is Nothing Then
                Return Nothing
            End If

            If columnBag.Count = 0 Then
                Return columnBag
            End If

            For rowindex As Integer = 0 To columnBag.Count - 1

                If Not columnBag.Record(rowindex) Is Nothing Then
                    If StringUtil.IsEmpty(columnBag.Record(rowindex).YosanShukeiCode) Then
                        If StringUtil.Equals(columnBag.Record(rowindex).YosanSiaShukeiCode, "J") Then
                            columnBag.Remove(columnBag.Record(rowindex))
                            columnBag = removeJikyu(columnBag)
                            Exit For
                        End If
                    Else
                        If StringUtil.Equals(columnBag.Record(rowindex).YosanShukeiCode, "J") Then
                            columnBag.Remove(columnBag.Record(rowindex))
                            columnBag = removeJikyu(columnBag)
                            Exit For
                        End If
                    End If
                End If
            Next

            Return columnBag
        End Function

        ''' <summary>
        ''' ソート処理を行う
        ''' </summary>
        ''' <param name="Conditions1">最優先のキー</param>
        ''' <param name="order1">昇順ならTrue,降順ならfalse</param>
        ''' <remarks></remarks>
        Public Sub SortMatrix(ByVal Conditions1 As String, ByVal order1 As Boolean)
            '引数チェック'
            If StringUtil.IsEmpty(Conditions1) Then
                '最優先が空の場合は何もしない'
                Exit Sub
            End If

            '最優先キーのリスト'
            Dim Conditions1List As New List(Of String)
            '2番目に優先キーのリスト'
            Dim Conditions2List As New List(Of String)
            '3番目に優先キーのリスト'
            Dim Conditions3List As New List(Of String)

            Dim level0Count As Integer = 0

            '最初にソートフラグに大まかな番号を振る'
            For Each rowindex As Integer In Me._koseiMatrix.GetInputRowIndexes
                'レベル０は対象外'
                Me._koseiMatrix.Record(rowindex).SortFlag = 0
                If Me._koseiMatrix(rowindex).YosanLevel > 0 Then
                    Conditions1List = SortSetList(Conditions1, Me._koseiMatrix.Record(rowindex), Conditions1List)
                Else
                    Me._koseiMatrix.Record(rowindex).SortFlag = 1
                    level0Count = level0Count + 1
                End If
            Next
            'レベル０の直下を基準にする'
            level0Count = level0Count
            'リストをソートする'
            'このリストの０番目が基準となる'
            Conditions1List.Sort()
            Conditions2List.Sort()
            Conditions3List.Sort()

            '降順の場合リストを逆転させる'
            If Not order1 Then
                Conditions1List.Reverse()
            End If

            For conIndex As Integer = 0 To Conditions1List.Count - 1
                For Each rowindex As Integer In Me._koseiMatrix.GetInputRowIndexes
                    If Not Me._koseiMatrix(rowindex).YosanLevel = 0 Then
                        '条件に該当するものを該当する位置に配置する'
                        If SortCheck(Conditions1, Conditions1List(conIndex), Me._koseiMatrix.Record(rowindex)) Then
                            '条件はあっているので配置をいじる'

                            '移動用レコード'
                            Dim buhinVo As New BuhinKoseiRecordVo
                            '移動先のレコード取得'
                            buhinVo = Me._koseiMatrix.Record(conIndex + level0Count)
                            '移動する'
                            Me._koseiMatrix.Record(conIndex + level0Count) = Me._koseiMatrix.Record(rowindex)
                            '移動先と移動元を入れ替える'
                            Me._koseiMatrix.Record(rowindex) = buhinVo
                            For Each colindex As Integer In Me._koseiMatrix.GetInputInsuColumnIndexes

                                Dim insuVo As New BuhinKoseiInsuCellVo
                                insuVo = Me._koseiMatrix.InsuVo(conIndex + level0Count, colindex)
                                Me._koseiMatrix.InsuVo(conIndex + level0Count, colindex) = Me._koseiMatrix.InsuVo(rowindex, colindex)
                                Me._koseiMatrix.InsuVo(rowindex, colindex) = insuVo
                            Next
                        End If
                    End If
                Next
            Next

            SetChanged()
        End Sub

        ''' <summary>
        ''' 条件ごとにリストに追加する
        ''' </summary>
        ''' <param name="Conditions"></param>
        ''' <param name="record"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Private Function SortSetList(ByVal Conditions As String, ByVal record As BuhinKoseiRecordVo, ByVal ConditionsList As List(Of String)) As List(Of String)
            Select Case Conditions
                Case "レベル"
                    ConditionsList.Add(record.YosanLevel)
                Case "取引先コード"
                    ConditionsList.Add(record.YosanMakerCode)
                Case "部品番号"
                    ConditionsList.Add(record.YosanBuhinNo)
                Case "部品名称"
                    ConditionsList.Add(record.YosanBuhinName)
                Case "供給セクション"
                    ConditionsList.Add(record.YosanKyoukuSection)
            End Select

            Return ConditionsList
        End Function

        ''' <summary>
        ''' ソート条件に合致するかチェックする
        ''' </summary>
        ''' <param name="Condition">条件</param>
        ''' <param name="param">値１</param>
        ''' <param name="record">値２</param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Private Function SortCheck(ByVal Condition As String, ByVal param As String, ByVal record As BuhinKoseiRecordVo) As Boolean
            Select Case Condition
                Case "レベル"
                    If Integer.Parse(param) = record.YosanLevel Then
                        Return True
                    Else
                        Return False
                    End If
                Case "取引先コード"
                    If StringUtil.Equals(param, record.YosanMakerCode) Then
                        Return True
                    Else
                        Return False
                    End If
                Case "部品番号"
                    If StringUtil.Equals(param, record.YosanBuhinNo) Then
                        Return True
                    Else
                        Return False
                    End If
                Case "部品名称"
                    If StringUtil.Equals(param, record.YosanBuhinName) Then
                        Return True
                    Else
                        Return False
                    End If
                Case "供給セクション"
                    If StringUtil.Equals(param, record.YosanKyoukuSection) Then
                        Return True
                    Else
                        Return False
                    End If
            End Select
            Return False
        End Function

        ''' <summary>
        ''' 「構成の情報」を元に部品表を作成する
        ''' </summary>
        ''' <param name="aStructureResult"></param>
        ''' <param name="baseLevel"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Private Function GetNewKoseiMatrix(ByVal aStructureResult As StructureResult, ByVal baseLevel As Integer?) As BuhinKoseiMatrix
            Return make.Compute(aStructureResult, a0553flag, baseLevel, String.Empty)
        End Function

        ''' <summary>
        ''' 行を挿入する
        ''' </summary>
        ''' <param name="rowIndex">行index</param>
        ''' <param name="count">挿入行数</param>
        ''' <remarks></remarks>
        Public Sub InsertRow(ByVal rowIndex As Integer, ByVal count As Integer)
            ' 上の行のレベル
            Dim baseLevel As Integer?
            Dim wRowIndex As Integer = rowIndex

            If 0 < wRowIndex Then
                If SpdKoseiObserver.SPREAD_JIKYU = "N" Then
                    Do While wRowIndex > 0
                        wRowIndex = wRowIndex - 1
                        '上の行のレベルが自給品だったらループ
                        If Me._koseiMatrix.Record(wRowIndex).YosanShukeiCode = "J" And _
                            Me._koseiMatrix.Record(wRowIndex).YosanSiaShukeiCode = "J" Or _
                            Me._koseiMatrix.Record(wRowIndex).YosanShukeiCode = "J" And _
                            Me._koseiMatrix.Record(wRowIndex).YosanSiaShukeiCode <> "J" Or _
                            Me._koseiMatrix.Record(wRowIndex).YosanShukeiCode = " " And _
                            Me._koseiMatrix.Record(wRowIndex).YosanSiaShukeiCode = "J" Then
                        Else
                            Exit Do
                        End If
                    Loop
                Else
                    wRowIndex = wRowIndex - 1
                End If

                If wRowIndex < 0 Then
                    'レベルが無いなら0にする'
                    baseLevel = 0
                Else
                    baseLevel = Me._koseiMatrix.Record(wRowIndex).YosanLevel
                End If
            Else
                'レベルが無いなら0にする'
                baseLevel = 0
            End If

            For i As Integer = 0 To count - 1
                Me._koseiMatrix.InsertRow(rowIndex)
                Me._koseiMatrix.Record(rowIndex).YosanLevel = baseLevel
            Next
            SetChanged()
        End Sub

        ''' <summary>
        ''' 行を挿入する（上の行の下位レベルを設定）
        ''' </summary>
        ''' <param name="rowIndex">行index</param>
        ''' <param name="count">挿入行数</param>
        ''' <remarks></remarks>
        Public Sub InsertRowNext(ByVal rowIndex As Integer, ByVal count As Integer)
            ' 上の行のレベル
            Dim baseLevel As Integer?
            Dim wRowIndex As Integer = rowIndex

            If 0 < wRowIndex Then
                If SpdKoseiObserver.SPREAD_JIKYU = "N" Then
                    Do While wRowIndex > 0
                        wRowIndex = wRowIndex - 1
                        '上の行のレベルが自給品だったらループ
                        If Me._koseiMatrix.Record(wRowIndex).YosanShukeiCode = "J" And _
                            Me._koseiMatrix.Record(wRowIndex).YosanSiaShukeiCode = "J" Or _
                            Me._koseiMatrix.Record(wRowIndex).YosanShukeiCode = "J" And _
                            Me._koseiMatrix.Record(wRowIndex).YosanSiaShukeiCode <> "J" Or _
                            Me._koseiMatrix.Record(wRowIndex).YosanShukeiCode = " " And _
                            Me._koseiMatrix.Record(wRowIndex).YosanSiaShukeiCode = "J" Then
                        Else
                            Exit Do
                        End If
                    Loop
                Else
                    wRowIndex = wRowIndex - 1
                End If

                If wRowIndex < 0 Then
                    'レベルが無いなら0にする'
                    baseLevel = 0
                Else
                    baseLevel = Me._koseiMatrix.Record(wRowIndex).YosanLevel + 1
                End If
            Else
                'レベルが無いなら0にする'
                baseLevel = 0
            End If

            For i As Integer = 0 To count - 1
                Me._koseiMatrix.InsertRow(rowIndex)
                Me._koseiMatrix.Record(rowIndex).YosanLevel = baseLevel
            Next
            SetChanged()
        End Sub

        ''' <summary>
        ''' 行を挿入する(INSTL品番挿入時)
        ''' </summary>
        ''' <param name="rowIndex">行index</param>
        ''' <param name="count">挿入行数</param>
        ''' <remarks></remarks>
        Public Sub InsertRowInstl(ByVal rowIndex As Integer, ByVal count As Integer)
            ' 上の行のレベル
            Dim baseLevel As Integer?
            If 0 < rowIndex Then
                baseLevel = Me._koseiMatrix.Record(rowIndex - 1).YosanLevel

            Else
                'レベルが無いなら0にする'
                baseLevel = 0
            End If

            Dim level0Count As Integer = 0
            For Each index As Integer In _koseiMatrix.GetInputRowIndexes
                If Me._koseiMatrix.Record(index).YosanLevel = 0 Then
                    'レベル0の行数をカウント'
                    level0Count = level0Count + 1
                End If
            Next

            For i As Integer = 0 To count - 1
                '列と行が同じとは限らないから'
                'レベル0の行数と列番号を比較'
                If level0Count >= rowIndex Then
                    Me._koseiMatrix.InsertRow(rowIndex + i)
                    Me._koseiMatrix.Record(rowIndex + i).YosanLevel = baseLevel
                    Me._koseiMatrix.InsuSuryo(rowIndex + i, rowIndex + i) = 1
                Else
                    'レベル0の行数よりも大きければ後ろにつける'
                    Me._koseiMatrix.InsertRow(level0Count + i)
                    Me._koseiMatrix.Record(level0Count + i).YosanLevel = baseLevel
                    Me._koseiMatrix.InsuSuryo(level0Count + i, rowIndex + i) = 1
                End If
            Next
            SetChanged()
        End Sub

        ''' <summary>
        ''' 行を削除する
        ''' </summary>
        ''' <param name="rowIndex">行index</param>
        ''' <param name="count">削除行数</param>
        ''' <remarks></remarks>
        Public Sub RemoveRow(ByVal rowIndex As Integer, ByVal count As Integer)
            For i As Integer = 0 To count - 1
                Me._koseiMatrix.RemoveRow(rowIndex)
            Next
        End Sub

        ''' <summary>
        ''' 行を削除する
        ''' </summary>
        ''' <param name="rowIndex">行index</param>
        ''' <param name="count">削除行数</param>
        ''' <remarks></remarks>
        Public Sub RemoveRowInstl(ByVal rowIndex As Integer, ByVal count As Integer)
            For i As Integer = 0 To count - 1
                If Me._koseiMatrix.Record(rowIndex).YosanLevel = 0 Then
                    Me._koseiMatrix.RemoveRow(rowIndex)
                End If
            Next
        End Sub

        Private IsSuspendOnChangedBuhinNo As Boolean
        ''' <summary>
        ''' 部品番号が入力・変更された時に呼ばれるリスナー
        ''' </summary>
        ''' <param name="rowIndex">行index</param>
        ''' <remarks></remarks>
        Public Sub OnChangedBuhinNo(ByVal rowIndex As Integer)
            If IsSuspendOnChangedBuhinNo Then
                Return
            End If
            IsSuspendOnChangedBuhinNo = True
            Try
                Dim inputedBuhinNo As String = Record(rowIndex).YosanBuhinNo

                ' ""は開発号
                Dim aStructureResult As StructureResult = detector.Compute(inputedBuhinNo, Nothing, False, "")
                If Not aStructureResult.IsExist Then
                    Return
                End If
                '子部品展開'
                a0553flag = 2
                Dim newKoseiMatrix As BuhinKoseiMatrix = GetNewKoseiMatrix(aStructureResult, YosanLevel(rowIndex))

                If newKoseiMatrix Is Nothing Then
                    Return
                End If

                Dim rowCount As Integer = 0

                If StringUtil.Equals(Jikyu, "0") Then
                    ''自給品を削除
                    For Each srcIndex As Integer In newKoseiMatrix.GetInputRowIndexes
                        If StringUtil.Equals(newKoseiMatrix(srcIndex).YosanShukeiCode, "J") Then
                            newKoseiMatrix.RemoveRow(srcIndex)
                            rowCount = rowCount + 1
                        End If
                    Next
                    For Each index As Integer In newKoseiMatrix.GetInputInsuColumnIndexes()
                        newKoseiMatrix.InstlColumn(index) = removeJikyu(newKoseiMatrix.InstlColumn(index))
                    Next
                End If

                InsertRow(rowIndex + 1, newKoseiMatrix.InstlColumn(0).Count - 1)

                Dim destIndex As Integer = 1
                For Each srcIndex As Integer In newKoseiMatrix.GetInputRowIndexes()
                    '自分は除く
                    If srcIndex <> -1 And srcIndex <> 0 Then
                        If StringUtil.Equals(Jikyu, "0") Then
                            If Not StringUtil.IsEmpty(newKoseiMatrix(srcIndex).YosanBuhinNo) Then
                                If StringUtil.IsEmpty(newKoseiMatrix(srcIndex).YosanShukeiCode) Then
                                    If Not StringUtil.Equals(newKoseiMatrix(srcIndex).YosanSiaShukeiCode, "J") Then
                                        '国内集計、海外集計、現調区分、取引先コード、取引先名称
                                        With newKoseiMatrix(srcIndex)
                                            ' プロパティアックセッサを使うとイベントが動くので直接代入する
                                            Record(rowIndex + destIndex).YosanLevel = .YosanLevel
                                            Record(rowIndex + destIndex).YosanShukeiCode = .YosanShukeiCode
                                            Record(rowIndex + destIndex).YosanSiaShukeiCode = .YosanSiaShukeiCode
                                            Record(rowIndex + destIndex).YosanMakerCode = .YosanMakerCode
                                            Record(rowIndex + destIndex).YosanMakerName = .YosanMakerName
                                            Record(rowIndex + destIndex).YosanBuhinNo = .YosanBuhinNo
                                            Record(rowIndex + destIndex).YosanBuhinName = .YosanBuhinName
                                        End With
                                        destIndex += 1
                                    End If
                                Else
                                    If Not StringUtil.Equals(newKoseiMatrix(srcIndex).YosanShukeiCode, "J") Then
                                        '国内集計、海外集計、現調区分、取引先コード、取引先名称
                                        With newKoseiMatrix(srcIndex)
                                            ' プロパティアックセッサを使うとイベントが動くので直接代入する
                                            Record(rowIndex + destIndex).YosanLevel = .YosanLevel
                                            Record(rowIndex + destIndex).YosanShukeiCode = .YosanShukeiCode
                                            Record(rowIndex + destIndex).YosanSiaShukeiCode = .YosanSiaShukeiCode
                                            Record(rowIndex + destIndex).YosanMakerCode = .YosanMakerCode
                                            Record(rowIndex + destIndex).YosanMakerName = .YosanMakerName
                                            Record(rowIndex + destIndex).YosanBuhinNo = .YosanBuhinNo
                                            Record(rowIndex + destIndex).YosanBuhinName = .YosanBuhinName
                                        End With
                                        destIndex += 1
                                    End If

                                End If
                            End If
                        Else
                            If Not StringUtil.IsEmpty(newKoseiMatrix(srcIndex).YosanBuhinNo) Then
                                '国内集計、海外集計、現調区分、取引先コード、取引先名称
                                With newKoseiMatrix(srcIndex)
                                    ' プロパティアックセッサを使うとイベントが動くので直接代入する
                                    Record(rowIndex + destIndex).YosanLevel = .YosanLevel
                                    Record(rowIndex + destIndex).YosanShukeiCode = .YosanShukeiCode
                                    Record(rowIndex + destIndex).YosanSiaShukeiCode = .YosanSiaShukeiCode
                                    Record(rowIndex + destIndex).YosanMakerCode = .YosanMakerCode
                                    Record(rowIndex + destIndex).YosanMakerName = .YosanMakerName
                                    Record(rowIndex + destIndex).YosanBuhinNo = .YosanBuhinNo
                                    Record(rowIndex + destIndex).YosanBuhinName = .YosanBuhinName
                                End With
                                destIndex += 1
                            End If
                        End If

                    End If
                Next

                NotifyObservers()

                '部品構成情報をセットした行数を返す。
                SpdKoseiObserver.SPREAD_ROW = rowIndex + 1
                SpdKoseiObserver.SPREAD_ROWCOUNT = newKoseiMatrix.InstlColumn(0).Count - 1
            Finally
                IsSuspendOnChangedBuhinNo = False
            End Try
        End Sub

#Region "表示更新の情報"
        ''' <summary>表示更新の情報</summary>
        Public Class NotifyInfo
            ''' <summary>タイトル行か？</summary>
            Public IsTitle As Boolean
            ''' <summary>行index</summary>
            Public RowIndexes As Integer()
            ''' <summary>列index</summary>
            Public ColumnIndexes As Integer()

            Public Shared Function NewTitleColumn(ByVal ParamArray columnIndexes As Integer()) As NotifyInfo
                Return New NotifyInfo(True, Nothing, columnIndexes)
            End Function
            Public Shared Function NewRow(ByVal ParamArray rowIndexes As Integer()) As NotifyInfo
                Return New NotifyInfo(False, rowIndexes, Nothing)
            End Function
            Public Sub New(ByVal IsTitle As Boolean, ByVal RowIndexes As Integer(), ByVal ColumnIndexes As Integer())
                Me.IsTitle = IsTitle
                Me.RowIndexes = RowIndexes
                Me.ColumnIndexes = ColumnIndexes
            End Sub
        End Class
#End Region

#Region "員数関連"
        ''' <summary>
        ''' パターン
        ''' </summary>
        ''' <param name="columnIndex"></param>
        ''' <value></value>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Property PatternName(ByVal columnIndex As Integer) As String
            Get
                Return _patternVos(columnIndex).PatternName
            End Get
            Set(ByVal value As String)
                If EzUtil.IsEqualIfNull(_patternVos(columnIndex), value) Then
                    Return
                End If
                _patternVos(columnIndex).PatternName = value
                SetChanged()
            End Set
        End Property

        Public ReadOnly Property PatternInfos() As List(Of TYosanBuhinEditPatternVo)
            Get
                Return _patternVos
            End Get
        End Property

        ''' <summary>
        ''' 入力した列タイトルの列No一覧を返す
        ''' </summary>
        ''' <returns>入力した列タイトルの列No一覧</returns>
        ''' <remarks></remarks>
        Public Function GetInputInstlPatternColumnCount() As Integer
            If StringUtil.IsEmpty(_patternVos) Then
                Return 1
            Else
                Return _patternVos.Count
            End If
        End Function

        Public ReadOnly Property GetInsuColumnIndexes() As ICollection(Of Integer)
            Get
                Return _koseiMatrix.GetInputInsuColumnIndexes()
            End Get
        End Property

        ''' <summary>
        ''' INSTL品番列に列挿入する
        ''' </summary>
        ''' <param name="columnIndex">INSTL品番の中の列index</param>
        ''' <param name="insertCount">挿入列数</param>
        ''' <remarks></remarks>
        Public Sub InsertColumnInInstl(ByVal columnIndex As Integer, ByVal insertCount As Integer)
            _koseiMatrix.InsertColumn(columnIndex, insertCount)

            ''2015/04/07 追加 E.Ubukata
            ''　PatternHyoujiJunの調整
            For Each vo As TYosanBuhinEditPatternVo In _patternVos
                If vo.PatternHyoujiJun >= columnIndex Then
                    vo.PatternHyoujiJun += insertCount
                End If
            Next

            Dim addList As New List(Of TYosanBuhinEditPatternVo)

            ''2015/04/09 変更 E.Ubukata
            '' 複数行追加に対応
            'For index As Integer = columnIndex To columnIndex
            For index As Integer = columnIndex To columnIndex + insertCount - 1
                Dim patternVo As New TYosanBuhinEditPatternVo
                patternVo.YosanEventCode = yosanEventCode
                patternVo.BuhinhyoName = yosanUnitKbn
                patternVo.PatternName = String.Empty

                ''2015/04/07 追加 E.Ubukata
                ''　PatternHyoujiJunの調整
                patternVo.PatternHyoujiJun = index


                addList.Add(patternVo)
            Next
            _patternVos.InsertRange(columnIndex, addList)
            NotifyObservers()
            'SetChanged()
        End Sub

        ''' <summary>
        ''' INSTL品番列を列削除する
        ''' </summary>
        ''' <param name="columnIndex">INSTL品番の中の列index</param>
        ''' <param name="removeCount">削除列数</param>
        ''' <remarks></remarks>
        Public Sub RemoveColumnInInstl(ByVal columnIndex As Integer, ByVal removeCount As Integer)
            _koseiMatrix.RemoveColumn(columnIndex, removeCount)
            _patternVos.RemoveRange(columnIndex, removeCount)

            ''2015/04/07 追加 E.Ubukata
            ''　PatternHyoujiJunの調整
            For Each vo As TYosanBuhinEditPatternVo In _patternVos
                If vo.PatternHyoujiJun >= columnIndex Then
                    vo.PatternHyoujiJun -= removeCount
                End If
            Next

            NotifyObservers()
            'SetChanged()
        End Sub
#End Region

    End Class
End Namespace