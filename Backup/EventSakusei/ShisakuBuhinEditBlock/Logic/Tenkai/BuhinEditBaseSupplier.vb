Imports ShisakuCommon
Imports ShisakuCommon.Db.EBom.Dao
Imports EventSakusei.ShisakuBuhinEditBlock.Dao
Imports ShisakuCommon.Db.EBom.Vo.Helper
Imports ShisakuCommon.Util.Grouping
Imports ShisakuCommon.Db.EBom.Vo
Imports EventSakusei.ShisakuBuhinEdit.Ikkatsu.Logic
Imports EventSakusei.ShisakuBuhinEdit.Kosei.Logic
Imports EventSakusei.ShisakuBuhinEdit.Kosei.Logic.Matrix
Imports EventSakusei.ShisakuBuhinEdit.Logic.Detect
Imports EventSakusei.ShisakuBuhinEdit.Logic
Imports ShisakuCommon.Util
Imports EventSakusei.ShisakuBuhinEdit.Kosei.Logic.Merge
Imports ShisakuCommon.Db.EBom.Dao.Impl

Namespace ShisakuBuhinEditBlock.Logic.Tenkai
    '構成用(実験クラス)'
    Public Class BuhinEditBaseSupplier

        Private _koseiMatrix As BuhinKoseiMatrix

        Private blockVo As TShisakuSekkeiBlockVo
        Private ReadOnly login As LoginInfo
        Private ReadOnly aShisakuDate As ShisakuDate
        Private ReadOnly detector As DetectLatestStructure
        Private ReadOnly instlTitle As BuhinEditKoseiInstlTitle

        Private ReadOnly editDao As TShisakuBuhinEditDao
        Private ReadOnly editInstlDao As TShisakuBuhinEditInstlDao
        Private ReadOnly sbiDao As ShisakuSekkeiBlockInstlDaoImpl
        Private ReadOnly make As MakeStructureResult
        Private ReadOnly aMakerNameResolver As MakerNameResolver

        Private Flag As Boolean

        Private isWaitingKoseiTenkai As Boolean

        Private JikyuUmu As String

        ''↓↓2014/07/23 Ⅰ.2.管理項目追加_aa) (TES)張 ADD BEGIN
        Private TsukurikataFlg As String
        ''↑↑2014/07/23 Ⅰ.2.管理項目追加_aa) (TES)張 ADD END

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
            End Set
        End Property

        ''' <summary>レベル</summary>
        ''' <value>レベル</value>
        ''' <returns>レベル</returns>
        Public Property Level(ByVal rowIndex As Integer) As Int32?
            Get
                Return Record(rowIndex).Level
            End Get
            Set(ByVal value As Int32?)
                If EzUtil.IsEqualIfNull(Record(rowIndex).Level, value) Then
                    Return
                End If
                Record(rowIndex).Level = value
            End Set
        End Property

        ''' <summary>国内集計コード</summary>
        ''' <value>国内集計コード</value>
        ''' <returns>国内集計コード</returns>
        Public Property ShukeiCode(ByVal rowIndex As Integer) As String
            Get
                Return Record(rowIndex).ShukeiCode
            End Get
            Set(ByVal value As String)
                If EzUtil.IsEqualIfNull(Record(rowIndex).ShukeiCode, value) Then
                    Return
                End If
                Record(rowIndex).ShukeiCode = value
            End Set
        End Property

        ''' <summary>海外SIA集計コード</summary>
        ''' <value>海外SIA集計コード</value>
        ''' <returns>海外SIA集計コード</returns>
        Public Property SiaShukeiCode(ByVal rowIndex As Integer) As String
            Get
                Return Record(rowIndex).SiaShukeiCode
            End Get
            Set(ByVal value As String)
                If EzUtil.IsEqualIfNull(Record(rowIndex).SiaShukeiCode, value) Then
                    Return
                End If
                Record(rowIndex).SiaShukeiCode = value
            End Set
        End Property

        ''' <summary>現調CKD区分</summary>
        ''' <value>現調CKD区分</value>
        ''' <returns>現調CKD区分</returns>
        Public Property GencyoCkdKbn(ByVal rowIndex As Integer) As String
            Get
                Return Record(rowIndex).GencyoCkdKbn
            End Get
            Set(ByVal value As String)
                If EzUtil.IsEqualIfNull(Record(rowIndex).GencyoCkdKbn, value) Then
                    Return
                End If
                Record(rowIndex).GencyoCkdKbn = value
            End Set
        End Property

        ''' <summary>取引先コード</summary>
        ''' <value>取引先コード</value>
        ''' <returns>取引先コード</returns>
        Public Property MakerCode(ByVal rowIndex As Integer) As String
            Get
                Return Record(rowIndex).MakerCode
            End Get
            Set(ByVal value As String)
                If EzUtil.IsEqualIfNull(Record(rowIndex).MakerCode, value) Then
                    Return
                End If
                Record(rowIndex).MakerCode = value

                OnChangedMakerCode(rowIndex)
            End Set
        End Property

        ''' <summary>取引先名称</summary>
        ''' <value>取引先名称</value>
        ''' <returns>取引先名称</returns>
        Public Property MakerName(ByVal rowIndex As Integer) As String
            Get
                Return Record(rowIndex).MakerName
            End Get
            Set(ByVal value As String)
                If EzUtil.IsEqualIfNull(Record(rowIndex).MakerName, value) Then
                    Return
                End If
                Record(rowIndex).MakerName = value
            End Set
        End Property

        ''' <summary>部品番号</summary>
        ''' <value>部品番号</value>
        ''' <returns>部品番号</returns>
        Public Property BuhinNo(ByVal rowIndex As Integer) As String
            Get
                Return Record(rowIndex).BuhinNo
            End Get
            Set(ByVal value As String)
                If EzUtil.IsEqualIfNull(Record(rowIndex).BuhinNo, value) Then
                    Return
                End If
                Record(rowIndex).BuhinNo = value

                OnChangedBuhinNo(rowIndex)
            End Set
        End Property

        ''' <summary>部品番号試作区分</summary>
        ''' <value>部品番号試作区分</value>
        ''' <returns>部品番号試作区分</returns>
        Public Property BuhinNoKbn(ByVal rowIndex As Integer) As String
            Get
                Return Record(rowIndex).BuhinNoKbn
            End Get
            Set(ByVal value As String)
                If EzUtil.IsEqualIfNull(Record(rowIndex).BuhinNoKbn, value) Then
                    Return
                End If
                Record(rowIndex).BuhinNoKbn = value
            End Set
        End Property

        ''' <summary>部品番号改訂No.</summary>
        ''' <value>部品番号改訂No.</value>
        ''' <returns>部品番号改訂No.</returns>
        Public Property BuhinNoKaiteiNo(ByVal rowIndex As Integer) As String
            Get
                Return Record(rowIndex).BuhinNoKaiteiNo
            End Get
            Set(ByVal value As String)
                If EzUtil.IsEqualIfNull(Record(rowIndex).BuhinNoKaiteiNo, value) Then
                    Return
                End If
                Record(rowIndex).BuhinNoKaiteiNo = value
            End Set
        End Property

        ''' <summary>枝番</summary>
        ''' <value>枝番</value>
        ''' <returns>枝番</returns>
        Public Property EdaBan(ByVal rowIndex As Integer) As String
            Get
                Return Record(rowIndex).EdaBan
            End Get
            Set(ByVal value As String)
                If EzUtil.IsEqualIfNull(Record(rowIndex).EdaBan, value) Then
                    Return
                End If
                Record(rowIndex).EdaBan = value
            End Set
        End Property

        ''' <summary>部品名称</summary>
        ''' <value>部品名称</value>
        ''' <returns>部品名称</returns>
        Public Property BuhinName(ByVal rowIndex As Integer) As String
            Get
                Return Record(rowIndex).BuhinName
            End Get
            Set(ByVal value As String)
                If EzUtil.IsEqualIfNull(Record(rowIndex).BuhinName, value) Then
                    Return
                End If
                Record(rowIndex).BuhinName = value
            End Set
        End Property

        ''' <summary>再使用不可</summary>
        ''' <value>再使用不可</value>
        ''' <returns>再使用不可</returns>
        Public Property Saishiyoufuka(ByVal rowIndex As Integer) As String
            Get
                Return Record(rowIndex).Saishiyoufuka
            End Get
            Set(ByVal value As String)
                If EzUtil.IsEqualIfNull(Record(rowIndex).Saishiyoufuka, value) Then
                    Return
                End If
                Record(rowIndex).Saishiyoufuka = value
            End Set
        End Property

        ''' <summary>供給セクション</summary>
        ''' <value>供給セクション</value>
        ''' <returns>供給セクション</returns>
        Public Property KyoukuSection(ByVal rowIndex As Integer) As String
            '2012/01/23 供給セクション追加
            Get
                Return Record(rowIndex).KyoukuSection
            End Get
            Set(ByVal value As String)
                If EzUtil.IsEqualIfNull(Record(rowIndex).KyoukuSection, value) Then
                    Return
                End If
                Record(rowIndex).KyoukuSection = value
            End Set
        End Property

        ''' <summary>出図予定日</summary>
        ''' <value>出図予定日</value>
        ''' <returns>出図予定日</returns>
        Public Property ShutuzuYoteiDate(ByVal rowIndex As Integer) As Int32?
            Get
                Return Record(rowIndex).ShutuzuYoteiDate
            End Get
            Set(ByVal value As Int32?)
                If EzUtil.IsEqualIfNull(Record(rowIndex).ShutuzuYoteiDate, value) Then
                    Return
                End If
                Record(rowIndex).ShutuzuYoteiDate = value
            End Set
        End Property

        ''' <summary>材質・規格１</summary>
        ''' <value>材質・規格１</value>
        ''' <returns>材質・規格１</returns>
        Public Property ZaishituKikaku1(ByVal rowIndex As Integer) As String
            Get
                Return Record(rowIndex).ZaishituKikaku1
            End Get
            Set(ByVal value As String)
                If EzUtil.IsEqualIfNull(Record(rowIndex).ZaishituKikaku1, value) Then
                    Return
                End If
                Record(rowIndex).ZaishituKikaku1 = value
            End Set
        End Property

        ''' <summary>材質・規格２</summary>
        ''' <value>材質・規格２</value>
        ''' <returns>材質・規格２</returns>
        Public Property ZaishituKikaku2(ByVal rowIndex As Integer) As String
            Get
                Return Record(rowIndex).ZaishituKikaku2
            End Get
            Set(ByVal value As String)
                If EzUtil.IsEqualIfNull(Record(rowIndex).ZaishituKikaku2, value) Then
                    Return
                End If
                Record(rowIndex).ZaishituKikaku2 = value
            End Set
        End Property

        ''' <summary>材質・規格３</summary>
        ''' <value>材質・規格３</value>
        ''' <returns>材質・規格３</returns>
        Public Property ZaishituKikaku3(ByVal rowIndex As Integer) As String
            Get
                Return Record(rowIndex).ZaishituKikaku3
            End Get
            Set(ByVal value As String)
                If EzUtil.IsEqualIfNull(Record(rowIndex).ZaishituKikaku3, value) Then
                    Return
                End If
                Record(rowIndex).ZaishituKikaku3 = value
            End Set
        End Property

        ''' <summary>材質・メッキ</summary>
        ''' <value>材質・メッキ</value>
        ''' <returns>材質・メッキ</returns>
        Public Property ZaishituMekki(ByVal rowIndex As Integer) As String
            Get
                Return Record(rowIndex).ZaishituMekki
            End Get
            Set(ByVal value As String)
                If EzUtil.IsEqualIfNull(Record(rowIndex).ZaishituMekki, value) Then
                    Return
                End If
                Record(rowIndex).ZaishituMekki = value
            End Set
        End Property

        ''' <summary>板厚・板厚</summary>
        ''' <value>板厚・板厚</value>
        ''' <returns>板厚・板厚</returns>
        Public Property ShisakuBankoSuryo(ByVal rowIndex As Integer) As String
            Get
                Return Record(rowIndex).ShisakuBankoSuryo
            End Get
            Set(ByVal value As String)
                If EzUtil.IsEqualIfNull(Record(rowIndex).ShisakuBankoSuryo, value) Then
                    Return
                End If
                Record(rowIndex).ShisakuBankoSuryo = value
            End Set
        End Property

        ''' <summary>板厚・ｕ</summary>
        ''' <value>板厚・ｕ</value>
        ''' <returns>板厚・ｕ</returns>
        Public Property ShisakuBankoSuryoU(ByVal rowIndex As Integer) As String
            Get
                Return Record(rowIndex).ShisakuBankoSuryoU
            End Get
            Set(ByVal value As String)
                If EzUtil.IsEqualIfNull(Record(rowIndex).ShisakuBankoSuryoU, value) Then
                    Return
                End If
                Record(rowIndex).ShisakuBankoSuryoU = value
            End Set
        End Property


        ''↓↓2014/12/26 メタル対応追加フィールド (DANIEL)柳沼 ADD BEGIN
        ''' <summary>材料情報・製品長</summary>
        ''' <value>材料情報・製品長</value>
        ''' <returns>材料情報・製品長</returns>
        Public Property MaterialInfoLength(ByVal rowIndex As Integer) As Int32?
            Get
                Return Record(rowIndex).MaterialInfoLength
            End Get
            Set(ByVal value As Int32?)
                If EzUtil.IsEqualIfNull(Record(rowIndex).MaterialInfoLength, value) Then
                    Return
                End If
                Record(rowIndex).MaterialInfoLength = value
            End Set
        End Property
        ''' <summary>材料情報・製品幅</summary>
        ''' <value>材料情報・製品幅</value>
        ''' <returns>材料情報・製品幅</returns>
        Public Property MaterialInfoWidth(ByVal rowIndex As Integer) As Int32?
            Get
                Return Record(rowIndex).MaterialInfoWidth
            End Get
            Set(ByVal value As Int32?)
                If EzUtil.IsEqualIfNull(Record(rowIndex).MaterialInfoWidth, value) Then
                    Return
                End If
                Record(rowIndex).MaterialInfoWidth = value
            End Set
        End Property
        ''' <summary>データ項目・改訂№</summary>
        ''' <value>データ項目・改訂№</value>
        ''' <returns>データ項目・改訂№</returns>
        Public Property DataItemKaiteiNo(ByVal rowIndex As Integer) As String
            Get
                Return Record(rowIndex).DataItemKaiteiNo
            End Get
            Set(ByVal value As String)
                If EzUtil.IsEqualIfNull(Record(rowIndex).DataItemKaiteiNo, value) Then
                    Return
                End If
                Record(rowIndex).DataItemKaiteiNo = value
            End Set
        End Property
        ''' <summary>データ項目・エリア名</summary>
        ''' <value>データ項目・エリア名</value>
        ''' <returns>データ項目・エリア名</returns>
        Public Property DataItemAreaName(ByVal rowIndex As Integer) As String
            Get
                Return Record(rowIndex).DataItemAreaName
            End Get
            Set(ByVal value As String)
                If EzUtil.IsEqualIfNull(Record(rowIndex).DataItemAreaName, value) Then
                    Return
                End If
                Record(rowIndex).DataItemAreaName = value
            End Set
        End Property
        ''' <summary>データ項目・セット名</summary>
        ''' <value>データ項目・セット名</value>
        ''' <returns>データ項目・セット名</returns>
        Public Property DataItemSetName(ByVal rowIndex As Integer) As String
            Get
                Return Record(rowIndex).DataItemSetName
            End Get
            Set(ByVal value As String)
                If EzUtil.IsEqualIfNull(Record(rowIndex).DataItemSetName, value) Then
                    Return
                End If
                Record(rowIndex).DataItemSetName = value
            End Set
        End Property
        ''' <summary>データ項目・改訂情報</summary>
        ''' <value>データ項目・改訂情報</value>
        ''' <returns>データ項目・改訂情報</returns>
        Public Property DataItemKaiteiInfo(ByVal rowIndex As Integer) As String
            Get
                Return Record(rowIndex).DataItemKaiteiInfo
            End Get
            Set(ByVal value As String)
                If EzUtil.IsEqualIfNull(Record(rowIndex).DataItemKaiteiInfo, value) Then
                    Return
                End If
                Record(rowIndex).DataItemKaiteiInfo = value
            End Set
        End Property
        ''↑↑2014/12/24 メタル対応追加フィールド (DANIEL)柳沼 ADD END


        ''' <summary>試作部品費（円）</summary>
        ''' <value>試作部品費（円）</value>
        ''' <returns>試作部品費（円）</returns>
        Public Property ShisakuBuhinHi(ByVal rowIndex As Integer) As Int32?
            Get
                Return Record(rowIndex).ShisakuBuhinHi
            End Get
            Set(ByVal value As Int32?)
                If EzUtil.IsEqualIfNull(Record(rowIndex).ShisakuBuhinHi, value) Then
                    Return
                End If
                Record(rowIndex).ShisakuBuhinHi = value
            End Set
        End Property

        ''' <summary>試作型費（千円）</summary>
        ''' <value>試作型費（千円）</value>
        ''' <returns>試作型費（千円）</returns>
        Public Property ShisakuKataHi(ByVal rowIndex As Integer) As Int32?
            Get
                Return Record(rowIndex).ShisakuKataHi
            End Get
            Set(ByVal value As Int32?)
                If EzUtil.IsEqualIfNull(Record(rowIndex).ShisakuKataHi, value) Then
                    Return
                End If
                Record(rowIndex).ShisakuKataHi = value
            End Set
        End Property

        ''' <summary>備考</summary>
        ''' <value>備考</value>
        ''' <returns>備考</returns>
        Public Property Bikou(ByVal rowIndex As Integer) As String
            Get
                Return Record(rowIndex).Bikou
            End Get
            Set(ByVal value As String)
                If EzUtil.IsEqualIfNull(Record(rowIndex).Bikou, value) Then
                    Return
                End If
                Record(rowIndex).Bikou = value
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
            End Set
        End Property

        ''' <summary>
        ''' 初期設定
        ''' </summary>
        ''' <param name="blockVo">ブロック情報</param>
        ''' <param name="login">ログイン情報</param>
        ''' <param name="aShisakuDate">日付</param>
        ''' <param name="anEzTunnel">INSTLの構成クラス</param>
        ''' <param name="detector">構成探索クラス</param>
        ''' <param name="makeStructure">構成の情報クラス</param>
        ''' <param name="aMakerNameResolver">取引先</param>
        ''' <param name="editDao">部品編集Dao</param>
        ''' <param name="editInstlDao">部品編集INSTLDao</param>
        ''' <param name="sbiDao">設計ブロックINSTLDao</param>
        ''' <remarks></remarks>
        Public Sub New(ByVal blockVo As TShisakuSekkeiBlockVo, ByVal login As LoginInfo, ByVal aShisakuDate As ShisakuDate, _
                       ByVal anEzTunnel As EzTunnelAlKosei, _
                       ByVal detector As DetectLatestStructure, _
                       ByVal makeStructure As MakeStructureResult, _
                       ByVal aMakerNameResolver As MakerNameResolver, _
                       ByVal editDao As TShisakuBuhinEditDao, _
                       ByVal editInstlDao As TShisakuBuhinEditInstlDao, _
                       ByVal sbiDao As ShisakuSekkeiBlockInstlDaoImpl)

            Me.blockVo = blockVo
            Me.login = login
            Me.aShisakuDate = aShisakuDate
            Me.detector = detector
            Me.make = makeStructure
            Me.aMakerNameResolver = aMakerNameResolver
            Me.editDao = editDao
            Me.editInstlDao = editInstlDao
            Me.sbiDao = sbiDao
            Me.instlTitle = New BuhinEditKoseiInstlTitle(anEzTunnel)

            ' TShisakuBuhinEdit をDaoで参照
            'Dim editVos As List(Of TShisakuBuhinEditVo) = FindEditBy(blockVo)

            'イベントコードをベースイベントコードで引っ張ってくる'
            Dim editVos As New List(Of TShisakuBuhinEditVo)
            Dim bimpl As BuhinEditBaseDao = New BuhinEditBaseDaoImpl
            'editVos = bimpl.FindByBuhinEdit(blockVo.ShisakuEventCode, blockVo.ShisakuBukaCode, blockVo.ShisakuBlockNo)
            '----------------------------------
            '２次改修
            '   2012/08/22 部課コード対策
            '   部課コードをキーから外してみる。
            editVos = bimpl.FindByBuhinEditKai(blockVo.ShisakuEventCode, blockVo.ShisakuBlockNo)
            'editVos = bimpl.FindByBuhinEditKai(blockVo.ShisakuEventCode, blockVo.ShisakuBukaCode, blockVo.ShisakuBlockNo)
            '----------------------------------

            '2012/01/23 樺澤'
            '自給品の有無を取得する'
            Dim eventVo As New TShisakuEventVo
            Dim eimpl As TShisakuEventDao = New TShisakuEventDaoImpl
            eventVo = eimpl.FindByPk(blockVo.ShisakuEventCode)
            '0が自給品無し,1が自給品有り'
            JikyuUmu = eventVo.JikyuUmu

            ''↓↓2014/07/23 Ⅰ.2.管理項目追加_z) (TES)張 ADD BEGIN
            TsukurikataFlg = eventVo.TsukurikataFlg
            ''↑↑2014/07/23 Ⅰ.2.管理項目追加_z) (TES)張 ADD END

            Flag = False
            If 0 < editVos.Count Then
                ' 存在したら、TShisakuBuhinEditInstl も参照して、それで _koseiMatrix を初期化＆表示 ※員数は「EditInstlのINSTL品番表示順」に合せる
                '----------------------------------
                '２次改修
                '   2012/08/22 部課コード対策
                '   部課コードをキーから外してみる。
                Dim editInstlVos As List(Of TShisakuBuhinEditInstlVo) = bimpl.FindByBuhinEditInstl(blockVo.ShisakuEventCode, blockVo.ShisakuBlockNo)
                'Dim editInstlVos As List(Of TShisakuBuhinEditInstlVo) = bimpl.FindByBuhinEditInstl(blockVo.ShisakuEventCode, blockVo.ShisakuBukaCode, blockVo.ShisakuBlockNo)
                '----------------------------------

                Dim EIVos As New List(Of TShisakuBuhinEditInstlVo)
                Dim EIVos2 As New List(Of TShisakuBuhinEditInstlVo)

                Dim BEVos As New List(Of TShisakuBuhinEditVo)
                Dim BeVos2 As New List(Of TShisakuBuhinEditVo)

                '2012/03/06'

                '設計ブロックINSTL情報を元に取る'
                '設計ブロックのINSTLを重複無しで取得する'
                '----------------------------------
                '２次改修
                '   2012/08/22 部課コード対策
                '   部課コードをキーから外してみる。
                Dim sekkeiBlockInstlVos As List(Of TShisakuSekkeiBlockInstlVo) = bimpl.FindBySekkeiBlockInstl(blockVo.ShisakuEventCode, blockVo.ShisakuBlockNo)
                'Dim sekkeiBlockInstlVos As List(Of TShisakuSekkeiBlockInstlVo) = bimpl.FindBySekkeiBlockInstl(blockVo.ShisakuEventCode, blockVo.ShisakuBukaCode, blockVo.ShisakuBlockNo)
                '----------------------------------

                'INSTL品番で取得部品の情報を取得する'
                Dim instlHinbanHyoujiJun As Integer = 0
                Dim BuhinNoHyoujiJun As Integer = 0
                Dim BuhinNoHyoujiJun2 As Integer = 0
                Dim instlHinban As String = ""
                Dim instlHinbanKbn As String = ""
                ''↓↓2014/08/21 1 ベース部品表作成表機能増強 ADD BEGIN
                Dim instlDataKbn As String = ""
                Dim BaseInstlFlg As String = ""
                ''↑↑2014/08/21 1 ベース部品表作成表機能増強 ADD END

                'INSTL品番の重複フラグ'
                Dim instlFlag As Boolean = False
                '設計ブロックINSTLの情報を元に部品情報を取得する'
                For Each sbvo As TShisakuSekkeiBlockInstlVo In sekkeiBlockInstlVos
                    instlFlag = False
                    If StringUtil.Equals(sbvo.InstlHinban, instlHinban) Then
                        '同一'
                        If StringUtil.Equals(sbvo.InstlHinbanKbn, instlHinbanKbn) Then
                            ''↓↓2014/08/21 1 ベース部品表作成表機能増強 ADD BEGIN
                            If StringUtil.Equals(sbvo.InstlDataKbn, instlDataKbn) Then
                                If StringUtil.Equals(sbvo.BaseInstlFlg, BaseInstlFlg) Then
                                    ''↑↑2014/08/21 1 ベース部品表作成表機能増強 ADD END
                                    '区分も同じならfalse'
                                    instlFlag = False
                                Else
                                    '元データ区分が違うならtrue'
                                    instlFlag = True
                                End If
                                ''↓↓2014/08/21 1 ベース部品表作成表機能増強 ADD BEGIN
                            Else
                                '元データ区分が違うならtrue'
                                instlFlag = True
                            End If

                        Else
                            '区分が違うならtrue'
                            instlFlag = True
                        End If
                    Else
                        '違うならtrue'
                        instlFlag = True
                    End If


                    If instlFlag Then

                        '----------------------------------
                        '２次改修
                        '   2012/08/22 部課コード対策
                        '   部課コードをキーから外してみる。
                        ''↓↓2014/08/21 1 ベース部品表作成表機能増強 CHG BEGIN
                        EIVos = bimpl.FindByBuhinEditInstl2(sbvo.ShisakuEventCode, _
                                                                sbvo.ShisakuBlockNo, _
                                                                sbvo.InstlHinban, _
                                                                sbvo.InstlHinbanKbn, _
                                                                sbvo.InstlDataKbn)
                        ''↑↑2014/08/21 1 ベース部品表作成表機能増強 CHG END
                        'EIVos = bimpl.FindByBuhinEditInstl2(sbvo.ShisakuEventCode, _
                        '                                        sbvo.ShisakuBukaCode, _
                        '                                        sbvo.ShisakuBlockNo, _
                        '                                        sbvo.InstlHinban, _
                        '                                        sbvo.InstlHinbanKbn)
                        '----------------------------------

                        '----------------------------------
                        '２次改修
                        '   2012/08/22 部課コード対策
                        '   部課コードをキーから外してみる。
                        ''↓↓2014/08/21 1 ベース部品表作成表機能増強 CHG BEGIN
                        BEVos = bimpl.FindByBuhinEdit2(sbvo.ShisakuEventCode, _
                                                       sbvo.ShisakuBlockNo, _
                                                       sbvo.InstlHinban, _
                                                       sbvo.InstlHinbanKbn, _
                                                       sbvo.InstlDataKbn)
                        ''↑↑2014/08/21 1 ベース部品表作成表機能増強 CHG END
                        'BEVos = bimpl.FindByBuhinEdit2(sbvo.ShisakuEventCode, _
                        '                               sbvo.ShisakuBukaCode, _
                        '                               sbvo.ShisakuBlockNo, _
                        '                               sbvo.InstlHinban, _
                        '                               sbvo.InstlHinbanKbn)
                        '----------------------------------

                        '------------------------------------------------------------------
                        '２次改修
                        '　部課コードの問題について
                        '   2012/08/22 部課略名で更新してみる。
                        'Dim KARyakuName = New KaRyakuNameDaoImpl '部課略称名取得IMPL
                        'Dim strRyakuName As String
                        '------------------------------------------------------------------

                        If EIVos.Count > 0 Then
                            'INSTL品番表示順を０から振りなおす'
                            For Each eivo As TShisakuBuhinEditInstlVo In EIVos

                                '------------------------------------------------------------------
                                '２次改修
                                '　部課コードの問題について
                                '   2012/08/22 部課略名で更新してみる。
                                'strRyakuName = KARyakuName.GetKa_Ryaku_Name(eivo.ShisakuBukaCode).KaRyakuName()
                                '------------------------------------------------------------------
                                '------------------------------------------------------------------
                                '２次改修
                                '　部課コードの問題について
                                '   2012/08/22 部課略名で更新してみる。
                                'If StringUtil.IsNotEmpty(strRyakuName) Then
                                '    eivo.ShisakuBukaCode = strRyakuName
                                'End If
                                '------------------------------------------------------------------

                                eivo.InstlHinbanHyoujiJun = instlHinbanHyoujiJun
                                '部品番号表示順を０から振りなおす'
                                eivo.BuhinNoHyoujiJun = BuhinNoHyoujiJun
                                EIVos2.Add(eivo)
                                BuhinNoHyoujiJun = BuhinNoHyoujiJun + 1
                            Next

                            For Each bevo As TShisakuBuhinEditVo In BEVos

                                '------------------------------------------------------------------
                                '２次改修
                                '　部課コードの問題について
                                '   2012/08/22 部課略名で更新してみる。
                                'strRyakuName = KARyakuName.GetKa_Ryaku_Name(bevo.ShisakuBukaCode).KaRyakuName()
                                '------------------------------------------------------------------
                                '------------------------------------------------------------------
                                '２次改修
                                '　部課コードの問題について
                                '   2012/08/22 部課略名で更新してみる。
                                'If StringUtil.IsNotEmpty(strRyakuName) Then
                                '    bevo.ShisakuBukaCode = strRyakuName
                                'End If
                                '------------------------------------------------------------------

                                '部品番号表示順を０から振りなおす'
                                bevo.BuhinNoHyoujiJun = BuhinNoHyoujiJun2
                                BeVos2.Add(bevo)
                                BuhinNoHyoujiJun2 = BuhinNoHyoujiJun2 + 1
                            Next
                            instlHinbanHyoujiJun = instlHinbanHyoujiJun + 1
                        End If
                    End If

                    '比較用に取っておく'
                    instlHinban = sbvo.InstlHinban
                    instlHinbanKbn = sbvo.InstlHinbanKbn
                    ''↓↓2014/08/21 1 ベース部品表作成表機能増強 ADD BEGIN
                    instlDataKbn = sbvo.InstlDataKbn
                    BaseInstlFlg = sbvo.BaseInstlFlg
                    ''↑↑2014/08/21 1 ベース部品表作成表機能増強 ADD END
                Next

                'マトリクスを作成する'
                _koseiMatrix = New BuhinKoseiMatrix(BeVos2, EIVos2)
                Dim newMatrix As New BuhinKoseiMatrix
                Dim mergeColumn As New MergeInstlColumnBag(newMatrix)

                'マージとソートを行う'
                For Each columnIndex As Integer In _koseiMatrix.GetInputInsuColumnIndexes
                    mergeColumn.Compute(_koseiMatrix.InstlColumn(columnIndex), columnIndex)
                Next
                _koseiMatrix = newMatrix

                '2012/02/16 ここをTrueにしたらエラーにならなかった
                Flag = True
            Else

                _koseiMatrix = New BuhinKoseiMatrix
                isWaitingKoseiTenkai = True
                Flag = False
            End If

        End Sub

        ''' <summary>
        ''' 必要な諸設定が済んだ後に呼び出される初期処理
        ''' </summary>
        ''' <remarks></remarks>
        Public Sub PerformInitialized()

            '部課コードが空の情報が存在する'
            If StringUtil.IsEmpty(blockVo.ShisakuBukaCode) Then
                Return
            End If
            '自給品の有無を取得する'
            If Not StringUtil.IsEmpty(blockVo.ShisakuEventCode) Then
                Dim baseimpl As TShisakuEventDao = New TShisakuEventDaoImpl
                Dim basevo As New TShisakuEventVo
                basevo = baseimpl.FindByPk(blockVo.ShisakuEventCode)
                JikyuUmu = basevo.JikyuUmu
            End If

            isWaitingKoseiTenkai = False


            If Not Flag Then
                '_koseiMatrix = NewMatrixKoseiTenkai(True, JikyuUmu)
                NewMatrixKoseiTenkai(True, JikyuUmu)

            Else
                Flag = True
                '_koseiMatrix = NewMatrixKoseiTenkai(False, JikyuUmu)
            End If


            If Not _koseiMatrix Is Nothing Then

                If _koseiMatrix.Records.Count = 0 Then
                    '最初のINSTL品番列が無ければ構成は無い・・・はず'
                    If Not Flag Then
                        'イベントコードからの参照の場合ブロック情報とブロックINSTL情報を削除する'
                        'DeleteBySekkeiBlockAndInstl()
                    End If

                Else
                    '取引先コードと取引先名称を取得する'
                    '２テーブル同時書き込み'
                    If Flag Then
                        '自給品の削除を行う'
                        For Each columnIndex As Integer In _koseiMatrix.GetInputInsuColumnIndexes()
                            If StringUtil.Equals(JikyuUmu, "0") Then
                                '自給品の削除'
                                _koseiMatrix.InstlColumn(columnIndex) = removeJikyu(_koseiMatrix.InstlColumn(columnIndex))
                            End If
                        Next

                        '別イベントからの場合こっち'
                        InsertByBuhinEditAndBaseEvent(_koseiMatrix)
                        InsertByBuhinEditInstlAndBaseEvent(_koseiMatrix)
                        UpdateBySekkeiBlockInstlAndBase(_koseiMatrix)
                        '一度登録したデータを再度取得して圧縮をする'

                    Else
                        '部品情報'
                        InsertByBuhinEditAndBase(_koseiMatrix)
                        '部品INSTL情報'
                        InsertByBuhinEditInstlAndBase(_koseiMatrix)
                    End If

                    System.GC.Collect()
                End If

            End If
        End Sub

        Private Function FindEditBy(ByVal blockVo As TShisakuSekkeiBlockVo) As List(Of TShisakuBuhinEditVo)
            Dim param As New TShisakuBuhinEditVo
            'XXX = XXXしか受け付けないから'

            'Return editDao.FindByPkNotJikyu(blockVo.ShisakuEventCode, blockVo.ShisakuBukaCode, blockVo.ShisakuBlockNo, _
            'blockVo.ShisakuBlockNoKaiteiNo, "J")

            'Jが消えてるか比較するため残しておく() '
            param.ShisakuEventCode = blockVo.ShisakuEventCode
            param.ShisakuBukaCode = blockVo.ShisakuBukaCode
            param.ShisakuBlockNo = blockVo.ShisakuBlockNo
            param.ShisakuBlockNoKaiteiNo = blockVo.ShisakuBlockNoKaiteiNo
            Return editDao.FindBy(param)
        End Function

        Private Function FindEditInstlBy(ByVal blockVo As TShisakuSekkeiBlockVo) As List(Of TShisakuBuhinEditInstlVo)
            Dim param As New TShisakuBuhinEditInstlVo
            param.ShisakuEventCode = blockVo.ShisakuEventCode
            param.ShisakuBukaCode = blockVo.ShisakuBukaCode
            param.ShisakuBlockNo = blockVo.ShisakuBlockNo
            param.ShisakuBlockNoKaiteiNo = blockVo.ShisakuBlockNoKaiteiNo
            Return editInstlDao.FindBy(param)
        End Function

#Region "INSTL品番関連"
        ''' <summary>INSTL品番</summary>
        ''' <param name="columnIndex">列No</param>
        Public Property InstlHinban(ByVal columnIndex As Integer) As String
            Get
                Return instlTitle.InstlHinban(columnIndex)
            End Get
            Set(ByVal value As String)
                If EzUtil.IsEqualIfNull(instlTitle.InstlHinban(columnIndex), value) Then
                    Return
                End If
                instlTitle.InstlHinban(columnIndex) = value

                OnChangedInstlHinbanOrKbn(columnIndex)
            End Set
        End Property

        ''' <summary>INSTL品番区分</summary>
        ''' <param name="columnIndex">列No</param>
        Public Property InstlHinbanKbn(ByVal columnIndex As Integer) As String
            Get
                Return instlTitle.InstlHinbanKbn(columnIndex)
            End Get
            Set(ByVal value As String)
                If EzUtil.IsEqualIfNull(instlTitle.InstlHinbanKbn(columnIndex), value) Then
                    Return
                End If
                instlTitle.InstlHinbanKbn(columnIndex) = value

                OnChangedInstlHinbanOrKbn(columnIndex)
            End Set
        End Property

        ''↓↓2014/08/22 1)EBOM-新試作手配システム過去データの組み合わせ抽出 酒井 ADD BEGIN
        ''' <summary>INSTL品番区分</summary>
        ''' <param name="columnIndex">列No</param>
        Public Property InstlDataKbn(ByVal columnIndex As Integer) As String
            Get
                Return instlTitle.InstlDataKbn(columnIndex)
            End Get
            Set(ByVal value As String)
                If EzUtil.IsEqualIfNull(instlTitle.InstlDataKbn(columnIndex), value) Then
                    Return
                End If
                instlTitle.InstlDataKbn(columnIndex) = value

                '                OnChangedInstlHinbanOrKbn(columnIndex)
            End Set
        End Property
        ''↑↑2014/08/22 1)EBOM-新試作手配システム過去データの組み合わせ抽出 酒井 ADD END
        '↓↓2014/08/15 Ⅰ.3.設計編集 ベース改修専用化_t) (TES)張 ADD BEGIN
        ''' <summary>ベース情報フラグ</summary>
        ''' <param name="columnIndex">列No</param>
        Public Property BaseInstlFlg(ByVal columnIndex As Integer) As String
            Get
                Return instlTitle.BaseInstlFlg(columnIndex)
            End Get
            Set(ByVal value As String)
                If EzUtil.IsEqualIfNull(instlTitle.BaseInstlFlg(columnIndex), value) Then
                    Return
                End If
                instlTitle.BaseInstlFlg(columnIndex) = value

                'OnChangedInstlHinbanOrKbn(columnIndex)
            End Set
        End Property
        ''↑↑2014/08/15 Ⅰ.3.設計編集 ベース改修専用化_t) (TES)張 ADD END

        ''' <summary>
        ''' 構成の情報を返す
        ''' </summary>
        ''' <param name="columnIndex">列index</param>
        ''' <returns>構成の情報</returns>
        ''' <remarks></remarks>
        Public Function GetStructureResult(ByVal columnIndex As Integer) As StructureResult
            Return instlTitle.GetStructureResult(columnIndex)
        End Function

        ''' <summary>
        ''' 入力した列タイトルの列No一覧を返す
        ''' </summary>
        ''' <returns>入力した列タイトルの列No一覧</returns>
        ''' <remarks></remarks>
        Public Function GetInputInstlHinbanColumnIndexes() As ICollection(Of Integer)
            Return instlTitle.GetInputInstlHinbanColumnIndexes
        End Function

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

        ''' <summary>
        ''' 行情報を表示更新する
        ''' </summary>
        ''' <param name="rowIndexes">行index</param>
        ''' <remarks></remarks>
        Public Sub NotifyObserversByRow(ByVal ParamArray rowIndexes As Integer())
            'NotifyObservers(NotifyInfo.NewRow(rowIndexes))
        End Sub
        ''' <summary>
        ''' 列タイトル部を表示更新する
        ''' </summary>
        ''' <param name="columnIndex">列タイトルの列indx</param>
        ''' <remarks></remarks>
        Public Sub NotifyTitleObservers(ByVal columnIndex As Integer)
            'NotifyObservers(NotifyInfo.NewTitleColumn(columnIndex))
        End Sub
#End Region

        ''' <summary>
        ''' 登録する
        ''' </summary>
        ''' <remarks></remarks>
        Public Sub Register()
            'このあたりを使ってBASEを作るか・・・樺澤'
            '樺澤'
            '** 試作部品編集情報／試作部品編集INSTL情報 **
            'Dim editSupplier As New BuhinEditKoseiEditSupplier(blockVo, _koseiMatrix)
            'editSupplier.Update(login, editDao, aShisakuDate)

            'Dim editInstlSupplier As New BuhinEditKoseiEditInstlSupplier(blockVo, _koseiMatrix)
            'editInstlSupplier.Update(login, editInstlDao, aShisakuDate)
        End Sub


        ''' <summary>
        ''' 「最新化」処理の結果、新しい部品構成編集クラスを返す
        ''' </summary>
        ''' <param name="columnIndex">対象の列index</param>
        ''' <param name="columnCount">対象列の数</param>
        ''' <returns>新しい部品構成編集クラス</returns>
        ''' <remarks></remarks>
        Public Function NewMatrixLatest(ByVal columnIndex As Integer, ByVal columnCount As Integer, ByVal isReuseStructure As Boolean) As BuhinKoseiMatrix
            Dim newMatrix As New BuhinKoseiMatrix
            Dim mergeColumn As New MergeInstlColumnBag(newMatrix)
            Dim indexes As ICollection(Of Integer) = MergeCollectionAsSorted(_koseiMatrix.GetInputInsuColumnIndexes(), instlTitle.GetInputInstlHinbanColumnIndexes)
            For Each inputtedColumnIndex As Integer In indexes
                If Not IsTargetColumn(inputtedColumnIndex, columnIndex, columnCount) Then
                    mergeColumn.Compute(_koseiMatrix.InstlColumn(inputtedColumnIndex), inputtedColumnIndex)
                    Continue For
                End If

                If GetStructureResult(inputtedColumnIndex) Is Nothing Then
                    Throw New InvalidProgramException("anEzTunnel.DetectStructureResultsしたあとに nothing が返る事はあり得ない")
                End If
                If GetStructureResult(inputtedColumnIndex).IsExist Then
                    Dim columnBag As InstlColumnBag = CreateInstlColumnBag(GetStructureResult(inputtedColumnIndex))
                    'mergeColumn.Compute(buhinKoseiMatrix.InstlColumn(0), inputtedColumnIndex)
                    mergeColumn.Compute(columnBag, inputtedColumnIndex)
                Else
                    If isReuseStructure Then
                        mergeColumn.Compute(_koseiMatrix.InstlColumn(inputtedColumnIndex), inputtedColumnIndex)
                    Else
                        ' nop
                        ' 「いいえ」選択時は、員数をクリアする
                        ' なので、ここは何もしなければ、員数がマージされずにクリアされる
                    End If
                End If
            Next
            Return newMatrix
        End Function

        ''' <summary>
        ''' 対象の列かを返す
        ''' </summary>
        ''' <param name="columnIndex">列index</param>
        ''' <param name="targetColumnIndex">対象の列index</param>
        ''' <param name="targetColumnCount">対象の列の数</param>
        ''' <returns>判定結果</returns>
        ''' <remarks></remarks>
        Private Function IsTargetColumn(ByVal columnIndex As Integer, ByVal targetColumnIndex As Integer, ByVal targetColumnCount As Integer) As Boolean

            Return targetColumnIndex <= columnIndex AndAlso columnIndex < targetColumnIndex + targetColumnCount
        End Function

        ''' <summary>
        ''' 「構成再展開」処理した新しい部品構成編集クラスを返す
        ''' </summary>
        ''' <param name="isReuseStructure">構成情報無しのINSTL品番の構成を再利用する場合、true</param>
        ''' <returns>新しい部品構成編集クラス</returns>
        ''' <remarks></remarks>
        Public Function NewMatrixKoseiTenkai(ByVal isReuseStructure As Boolean, ByVal JikyuUmu As String) As BuhinKoseiMatrix
            '2012/01/21 樺澤
            '2012/01/23 樺澤 自給品の有無で取得する内容を変更させる'
            Dim newMatrix As New BuhinKoseiMatrix
            Dim mergeColumn As New MergeInstlColumnBag(newMatrix)

            Dim indexes As ICollection(Of Integer) = MergeCollectionAsSorted(_koseiMatrix.GetInputInsuColumnIndexes(), instlTitle.GetInputInstlHinbanColumnIndexes)
            For Each columnIndex As Integer In indexes

                If GetStructureResult(columnIndex) Is Nothing Then
                    Throw New InvalidProgramException("anEzTunnel.DetectStructureResultsしたあとに nothing が返る事はあり得ない")
                End If

                If GetStructureResult(columnIndex).IsExist Then
                    If GetStructureResult(columnIndex).IsEBom Then
                        Dim columnBag As InstlColumnBag = CreateInstlColumnBag(GetStructureResult(columnIndex))
                        '--------------------------------------------------------------------------------------
                        '２次改修
                        '   NOTHINGを返さずに0レベルを最低限で登録する。
                        '   じゃないと部品表が空になってしまうので。
                        '無いなら構成をNOTHINGで返す'
                        If columnBag Is Nothing Then
                            'Return Nothing
                            Dim RVo As New BuhinKoseiRecordVo()
                            RVo.BuhinNo = GetStructureResult(columnIndex).BuhinNo
                            RVo.BuhinNoKbn = GetStructureResult(columnIndex).BuhinNoKbn
                            RVo.Level = 0
                            Dim insuVo As New BuhinKoseiInsuCellVo()
                            insuVo.InsuSuryo = 1

                            _koseiMatrix.InstlColumn(columnIndex).Add(RVo, insuVo)
                            mergeColumn.Compute(_koseiMatrix.InstlColumn(columnIndex), columnIndex)
                        Else
                            If StringUtil.Equals(JikyuUmu, "0") Then
                                '自給品の削除'
                                columnBag = removeJikyu(columnBag)

                            End If
                            mergeColumn.Compute(columnBag, columnIndex)
                        End If
                        '--------------------------------------------------------------------------------------
                    Else
                        '2012/03/15 設計展開時に構成の無いものは最低限で登録'
                        Dim RVo As New BuhinKoseiRecordVo()
                        RVo.BuhinNo = GetStructureResult(columnIndex).BuhinNo
                        RVo.BuhinNoKbn = GetStructureResult(columnIndex).BuhinNoKbn
                        RVo.Level = 0
                        Dim insuVo As New BuhinKoseiInsuCellVo()
                        insuVo.InsuSuryo = 1

                        _koseiMatrix.InstlColumn(columnIndex).Add(RVo, insuVo)
                        mergeColumn.Compute(_koseiMatrix.InstlColumn(columnIndex), columnIndex)
                    End If
                Else
                    If isReuseStructure Then
                        'レベル０の構成だけ作成する'
                        Dim RVo As New BuhinKoseiRecordVo()
                        RVo.BuhinNo = GetStructureResult(columnIndex).BuhinNo
                        RVo.BuhinNoKbn = GetStructureResult(columnIndex).BuhinNoKbn
                        RVo.Level = 0
                        Dim insuVo As New BuhinKoseiInsuCellVo()
                        insuVo.InsuSuryo = 1

                        _koseiMatrix.InstlColumn(columnIndex).Add(RVo, insuVo)
                        mergeColumn.Compute(_koseiMatrix.InstlColumn(columnIndex), columnIndex)
                    Else
                        ' nop
                        ' 「いいえ」選択時は、員数をクリアする
                        ' なので、ここは何もしなければ、員数がマージされずにクリアされる
                    End If
                End If
            Next
            Return newMatrix
        End Function

        ''' <summary>
        ''' 自給品の存在する行を削除する
        ''' </summary>
        ''' <param name="columnBag">列構成情報</param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Function removeJikyu(ByVal columnBag As InstlColumnBag) As InstlColumnBag
            For rowindex As Integer = 0 To columnBag.Count - 1

                If Not columnBag.Record(rowindex) Is Nothing Then
                    If StringUtil.IsEmpty(columnBag.Record(rowindex).ShukeiCode) Then
                        If StringUtil.Equals(columnBag.Record(rowindex).SiaShukeiCode, "J") Then
                            columnBag.Remove(columnBag.Record(rowindex))
                            'columnBag.RemoveCell(rowindex)
                            columnBag = removeJikyu(columnBag)
                            Exit For
                        End If
                    Else
                        If StringUtil.Equals(columnBag.Record(rowindex).ShukeiCode, "J") Then
                            columnBag.Remove(columnBag.Record(rowindex))
                            'columnBag.RemoveCell(rowindex)
                            columnBag = removeJikyu(columnBag)
                            Exit For
                        End If
                    End If
                End If
            Next

            Return columnBag
        End Function

        ''' <summary>
        ''' 「一括設定」の情報をもとに、新しい部品構成編集クラスを返す
        ''' </summary>
        ''' <param name="structureResults">「一括設定」の情報</param>
        ''' <returns>新しい部品構成編集クラス</returns>
        ''' <remarks></remarks>
        Public Function NewMatrixBySpecified(ByVal structureResults As IndexedList(Of StructureResult)) As BuhinKoseiMatrix
            Dim newMatrix As New BuhinKoseiMatrix
            Dim mergeColumn As New MergeInstlColumnBag(newMatrix)
            Dim columnIndexes As ICollection(Of Integer) = MergeCollectionAsSorted(_koseiMatrix.GetInputInsuColumnIndexes(), structureResults.Keys)
            For Each columnIndex As Integer In columnIndexes
                If structureResults.Keys.Contains(columnIndex) AndAlso structureResults(columnIndex).IsExist Then
                    Dim columnBag As InstlColumnBag = CreateInstlColumnBag(structureResults(columnIndex))
                    mergeColumn.Compute(columnBag, columnIndex)
                Else
                    mergeColumn.Compute(_koseiMatrix.InstlColumn(columnIndex), columnIndex)
                End If
            Next
            Return newMatrix
        End Function

        ''' <summary>
        ''' コレクションをマージする
        ''' </summary>
        ''' <param name="a">コレクション(配列)A</param>
        ''' <param name="b">コレクション(配列)B</param>
        ''' <returns>ソート済みのマージの結果</returns>
        ''' <remarks></remarks>
        Private Shared Function MergeCollectionAsSorted(ByVal a As ICollection(Of Integer), ByVal b As ICollection(Of Integer)) As ICollection(Of Integer)
            Dim result As New List(Of Integer)(a)
            For Each i As Integer In b
                If 0 <= result.IndexOf(i) Then
                    Continue For
                End If
                result.Add(i)
            Next
            result.Sort()
            Return result
        End Function

        Private IsSuspendOnChangedBuhinNo As Boolean

        ''' <summary>
        ''' 部品番号が入力・変更された時に呼ばれるリスナー
        ''' </summary>
        ''' <param name="rowIndex">行index</param>
        ''' <remarks></remarks>
        Private Sub OnChangedBuhinNo(ByVal rowIndex As Integer)
            If IsSuspendOnChangedBuhinNo Then
                Return
            End If
            IsSuspendOnChangedBuhinNo = True
            Try
                Dim inputedBuhinNo As String = Record(rowIndex).BuhinNo

                Dim aStructureResult As StructureResult = detector.Compute(inputedBuhinNo, Nothing, False)
                If Not aStructureResult.IsExist Then
                    Return
                End If
                Dim newKoseiMatrix As BuhinKoseiMatrix = GetNewKoseiMatrix(aStructureResult, Level(rowIndex), JikyuUmu)
                If newKoseiMatrix.InputRowCount = 0 Then
                    Return
                End If

                InsertRow(rowIndex + 1, newKoseiMatrix.GetInputRowIndexes().Count - 1)
                Dim destIndex As Integer = 0
                For Each srcIndex As Integer In newKoseiMatrix.GetInputRowIndexes()
                    '国内集計、海外集計、現調区分、取引先コード、取引先名称
                    With newKoseiMatrix(srcIndex)
                        ' プロパティアックセッサを使うとイベントが動くので直接代入する
                        Record(rowIndex + destIndex).Level = .Level
                        Record(rowIndex + destIndex).ShukeiCode = .ShukeiCode
                        Record(rowIndex + destIndex).SiaShukeiCode = .SiaShukeiCode
                        Record(rowIndex + destIndex).GencyoCkdKbn = .GencyoCkdKbn
                        Record(rowIndex + destIndex).MakerCode = .MakerCode
                        Record(rowIndex + destIndex).MakerName = .MakerName
                        Record(rowIndex + destIndex).BuhinNo = .BuhinNo
                        Record(rowIndex + destIndex).BuhinName = .BuhinName
                    End With
                    destIndex += 1
                Next

                'NotifyObservers()
            Finally
                IsSuspendOnChangedBuhinNo = False
            End Try
        End Sub

        ''' <summary>
        ''' 取引先コードが入力・変更された時に呼ばれるリスナー
        ''' </summary>
        ''' <param name="rowIndex">行index</param>
        ''' <remarks></remarks>
        Private Sub OnChangedMakerCode(ByVal rowIndex As Integer)
            Dim makerName As String = aMakerNameResolver.Resolve(Record(rowIndex).MakerCode)
            If makerName Is Nothing Then
                Return
            End If
            Record(rowIndex).MakerName = makerName

            'NotifyObservers(rowIndex)
        End Sub

        Friend IsSuspend_OnChangedInstlHinbanOrKbn As Boolean
        ''' <summary>
        ''' ヘッダー部のINSTL品番・試作区分が変更された時に呼ばれるリスナー
        ''' </summary>
        ''' <param name="columnIndex">列index</param>
        ''' <remarks></remarks>
        Private Sub OnChangedInstlHinbanOrKbn(ByVal columnIndex As Integer)
            If IsSuspend_OnChangedInstlHinbanOrKbn Then
                Return
            End If

            Dim notifyRowIndexes As New List(Of Integer)

            Dim aStructureResult As StructureResult = GetStructureResult(columnIndex)
            Dim rowIndexesOnColumn As List(Of Integer) = _koseiMatrix.GetInputRowIndexesOnColumn(columnIndex)
            For Each rowIndex As Integer In rowIndexesOnColumn
                If Record(rowIndex).Level = 0 Then
                    Record(rowIndex).BuhinNo = IIf(aStructureResult.IsEBom, aStructureResult.BuhinNo, instlTitle.InstlHinban(columnIndex))
                    Record(rowIndex).BuhinNoKbn = IIf(aStructureResult.IsEBom, aStructureResult.BuhinNoKbn, instlTitle.InstlHinbanKbn(columnIndex))
                    notifyRowIndexes.Add(rowIndex)
                End If
            Next

            Dim wasInsertRow As Boolean = False
            If notifyRowIndexes.Count = 0 Then
                Dim insertRowIndex As Integer = GetRowIndexLastLevelZero() + 1
                InsertRow(insertRowIndex, 1)
                wasInsertRow = True
                Record(insertRowIndex).Level = 0
                InsuSuryo(insertRowIndex, columnIndex) = 1
                Record(insertRowIndex).BuhinNo = IIf(aStructureResult.IsEBom, aStructureResult.BuhinNo, instlTitle.InstlHinban(columnIndex))
                Record(insertRowIndex).BuhinNoKbn = IIf(aStructureResult.IsEBom, aStructureResult.BuhinNoKbn, instlTitle.InstlHinbanKbn(columnIndex))
                notifyRowIndexes.Add(insertRowIndex)
            End If

            If aStructureResult.IsEBom Then
                Dim koseiMatrix As BuhinKoseiMatrix = GetNewKoseiMatrix(aStructureResult)
                If koseiMatrix.InputRowCount = 0 Then
                    Throw New InvalidProgramException("EBomに存在するハズの品番で、結果がゼロ件になるはずはない")
                End If
                For Each rowIndex As Integer In notifyRowIndexes
                    With Record(rowIndex)
                        .ShukeiCode = koseiMatrix(0).ShukeiCode
                        .SiaShukeiCode = koseiMatrix(0).SiaShukeiCode
                        .GencyoCkdKbn = koseiMatrix(0).GencyoCkdKbn
                        .MakerCode = koseiMatrix(0).MakerCode
                        .MakerName = koseiMatrix(0).MakerName
                        .BuhinNoKaiteiNo = koseiMatrix(0).BuhinNoKaiteiNo
                        .EdaBan = koseiMatrix(0).EdaBan
                        .BuhinName = koseiMatrix(0).BuhinName
                    End With
                Next
            End If

            'If Not HasChanged() Then
            '    Return
            'End If
            'If wasInsertRow Then
            '    NotifyObservers()   ' Insert行の下の行情報もすべて再表示
            '    Return
            'End If
            'NotifyObserversByRow(notifyRowIndexes.ToArray)
            'SetChanged()
        End Sub

        ''' <summary>
        ''' 部品表上部にあるレベル0の最終行indexを返す
        ''' </summary>
        ''' <returns>部品表上部のレベル0最終行index</returns>
        ''' <remarks></remarks>
        Private Function GetRowIndexLastLevelZero() As Integer
            Dim result As Integer = -1
            For Each rowIndex As Integer In GetInputRowIndexes()
                If EzUtil.IsEqualIfNull(Record(rowIndex).Level, 0) Then
                    result = rowIndex
                Else
                    Return result
                End If
            Next
            Return result
        End Function

        ''' <summary>
        ''' 構成情報を元にした部品表のINSTL列情報を作成して返す
        ''' </summary>
        ''' <param name="aStructureResult">構成の情報</param>
        ''' <returns>INSTL列情報</returns>
        ''' <remarks></remarks>
        Private Function CreateInstlColumnBag(ByVal aStructureResult As StructureResult) As InstlColumnBag
            Dim koseiMatrix As BuhinKoseiMatrix = GetNewKoseiMatrix(aStructureResult)
            '構成が無いならNOTHINGを返す 樺澤 '
            If koseiMatrix Is Nothing Then
                Return Nothing
            End If

            For Each rowIndex As Integer In koseiMatrix.GetInputRowIndexes()
                For Each columnIndex As Integer In koseiMatrix.GetInputInsuColumnIndexesOnRow(rowIndex)

                    Return koseiMatrix.InstlColumn(columnIndex)
                Next
            Next
            If 0 < koseiMatrix.GetInputRowIndexes.Count Then
                'エラーを返さない'
                Return Nothing
                Throw New InvalidOperationException("員数が未入力で InstlColumnBag を作成出来ない")
            End If
            Return New InstlColumnBag ' EMPTY
        End Function

        ''' <summary>
        ''' 構成情報を元に部品表を作成して返す
        ''' </summary>
        ''' <param name="aStructureResult">構成の情報</param>
        ''' <returns>新しい部品表</returns>
        ''' <remarks>構成の情報が無い、オリジナル品番の場合、レコード数ゼロの部品表を返す</remarks>
        Private Function GetNewKoseiMatrix(ByVal aStructureResult As StructureResult) As BuhinKoseiMatrix
            Return GetNewKoseiMatrix(aStructureResult, 0, JikyuUmu)
        End Function

        ''' <summary>
        ''' 「構成の情報」を元に部品表を作成する(自給品有り)
        ''' </summary>
        ''' <param name="aStructureResult"></param>
        ''' <param name="baseLevel"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Private Function GetNewKoseiMatrix(ByVal aStructureResult As StructureResult, ByVal baseLevel As Integer?, ByVal JikyuUmu As String) As BuhinKoseiMatrix
            Return make.Compute(aStructureResult, 0, baseLevel)
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
            If 0 < rowIndex Then
                baseLevel = Me._koseiMatrix.Record(rowIndex - 1).Level
            End If
            For i As Integer = 0 To count - 1
                Me._koseiMatrix.InsertRow(rowIndex)
                Me._koseiMatrix.Record(rowIndex).Level = baseLevel
            Next
            'SetChanged()
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
        ''' 部品構成呼出（一括設定）用のSubjectを生成する
        ''' </summary>
        ''' <returns>新しいインスタンス</returns>
        ''' <remarks></remarks>
        Public Function NewIkkatsuSubject() As BuhinEditIkkatsuSubject
            Return New BuhinEditIkkatsuSubject(blockVo, instlTitle)
        End Function

        ''' <summary>
        ''' INSTL品番列に列挿入する
        ''' </summary>
        ''' <param name="columnIndex">INSTL品番の中の列index</param>
        ''' <param name="insertCount">挿入列数</param>
        ''' <remarks></remarks>
        Public Sub InsertColumnInInstl(ByVal columnIndex As Integer, ByVal insertCount As Integer)
            instlTitle.Insert(columnIndex, insertCount)
            _koseiMatrix.InsertColumn(columnIndex, insertCount)
            'SetChanged()
        End Sub

        ''' <summary>
        ''' ブロックNoを差し替える
        ''' </summary>
        ''' <param name="blockVo">ブロックVo</param>
        ''' <remarks></remarks>
        Public Sub SupersedeBlockVo(ByVal blockVo As TShisakuSekkeiBlockVo)
            Me.blockVo = blockVo

            ' TShisakuBuhinEdit をDaoで参照
            Dim editVos As List(Of TShisakuBuhinEditVo) = FindEditBy(blockVo)
            If 0 < editVos.Count Then
                ' 存在したら、TShisakuBuhinEditInstl も参照して、それで _koseiMatrix を初期化＆表示 ※員数は「EditInstlのINSTL品番表示順」に合せる
                Dim editInstlVos As List(Of TShisakuBuhinEditInstlVo) = FindEditInstlBy(blockVo)
                _koseiMatrix = New BuhinKoseiMatrix(editVos, editInstlVos)
            Else
                _koseiMatrix = New BuhinKoseiMatrix
                isWaitingKoseiTenkai = True
            End If

            'SetChanged()
        End Sub

        ''' <summary>
        ''' INSTL品番列を列削除する
        ''' </summary>
        ''' <param name="columnIndex">INSTL品番の中の列index</param>
        ''' <param name="removeCount">削除列数</param>
        ''' <remarks></remarks>
        Public Sub RemoveColumnInInstl(ByVal columnIndex As Integer, ByVal removeCount As Integer)
            instlTitle.Remove(columnIndex, removeCount)
            _koseiMatrix.RemoveColumn(columnIndex, removeCount)
            'SetChanged()
        End Sub

#Region "部品編集情報を登録する"

        ''' <summary>
        ''' 部品表編集情報を登録する
        ''' </summary>
        ''' <param name="koseiMatrix">構成マトリクス(部品表編集情報)</param>
        ''' <remarks></remarks>
        Private Sub InsertByBuhinEdit(ByVal koseiMatrix As BuhinKoseiMatrix)

            Dim aDate As New ShisakuDate
            Dim editdao As TShisakuBuhinEditDao = New TShisakuBuhinEditDaoImpl



            '部品番号表示順用'
            Dim BuhinNoHyoujiJun As Integer = 0

            For index As Integer = 0 To koseiMatrix.GetInputRowIndexes.Count
                Dim param As New TShisakuBuhinEditVo

                If StringUtil.IsEmpty(koseiMatrix(index).BuhinNo) Then
                    Continue For
                End If

                param.ShisakuEventCode = blockVo.ShisakuEventCode
                param.ShisakuBukaCode = blockVo.ShisakuBukaCode
                param.ShisakuBlockNo = blockVo.ShisakuBlockNo
                param.ShisakuBlockNoKaiteiNo = blockVo.ShisakuBlockNoKaiteiNo

                param.BuhinNoHyoujiJun = BuhinNoHyoujiJun

                '2012/01/23 供給セクション追加
                param.Level = koseiMatrix(index).Level
                param.ShukeiCode = koseiMatrix(index).ShukeiCode
                param.SiaShukeiCode = koseiMatrix(index).SiaShukeiCode
                param.GencyoCkdKbn = koseiMatrix(index).GencyoCkdKbn
                param.KyoukuSection = koseiMatrix(index).KyoukuSection
                param.MakerCode = koseiMatrix(index).MakerCode
                param.MakerName = koseiMatrix(index).MakerName
                param.BuhinNo = koseiMatrix(index).BuhinNo
                param.BuhinNoKbn = koseiMatrix(index).BuhinNoKbn
                param.BuhinNoKaiteiNo = koseiMatrix(index).BuhinNoKaiteiNo
                param.EdaBan = koseiMatrix(index).EdaBan
                param.BuhinName = koseiMatrix(index).BuhinName
                param.Saishiyoufuka = koseiMatrix(index).Saishiyoufuka
                param.ShutuzuYoteiDate = koseiMatrix(index).ShutuzuYoteiDate
                param.ZaishituKikaku1 = koseiMatrix(index).ZaishituKikaku1
                param.ZaishituKikaku2 = koseiMatrix(index).ZaishituKikaku2
                param.ZaishituKikaku3 = koseiMatrix(index).ZaishituKikaku3
                param.ZaishituMekki = koseiMatrix(index).ZaishituMekki
                param.ShisakuBankoSuryo = koseiMatrix(index).ShisakuBankoSuryo
                param.ShisakuBankoSuryoU = koseiMatrix(index).ShisakuBankoSuryoU


                ''↓↓2014/12/26 メタル対応追加フィールド (DANIEL)柳沼 ADD BEGIN
                '材料情報・製品長
                param.MaterialInfoLength = koseiMatrix(index).MaterialInfoLength
                '' 材料情報・製品幅
                param.MaterialInfoWidth = koseiMatrix(index).MaterialInfoWidth
                '' データ項目・改訂№
                param.DataItemKaiteiNo = koseiMatrix(index).DataItemKaiteiNo
                '' データ項目・エリア名
                param.DataItemAreaName = koseiMatrix(index).DataItemAreaName
                '' データ項目・セット名
                param.DataItemSetName = koseiMatrix(index).DataItemSetName
                '' データ項目・改訂情報
                param.DataItemKaiteiInfo = koseiMatrix(index).DataItemKaiteiInfo
                ''↑↑2014/12/24 メタル対応追加フィールド (DANIEL)柳沼 ADD END


                param.ShisakuBuhinHi = koseiMatrix(index).ShisakuBuhinHi
                param.ShisakuKataHi = koseiMatrix(index).ShisakuKataHi
                '2012/01/25 部品ノート追加
                param.BuhinNote = koseiMatrix(index).BuhinNote
                param.Bikou = koseiMatrix(index).Bikou
                param.EditTourokubi = koseiMatrix(index).EditTourokubi
                param.EditTourokujikan = koseiMatrix(index).EditTourokujikan
                param.KaiteiHandanFlg = koseiMatrix(index).KaiteiHandanFlg
                param.ShisakuListCode = koseiMatrix(index).ShisakuListCode

                param.CreatedUserId = LoginInfo.Now.UserId
                param.CreatedDate = aDate.CurrentDateDbFormat
                param.CreatedTime = aDate.CurrentTimeDbFormat
                param.UpdatedUserId = LoginInfo.Now.UserId
                param.UpdatedDate = aDate.CurrentDateDbFormat
                param.UpdatedTime = aDate.CurrentTimeDbFormat

                editdao.InsertBy(param)

                BuhinNoHyoujiJun = BuhinNoHyoujiJun + 1

            Next
        End Sub

        ''' <summary>
        ''' 部品表編集情報を登録する
        ''' </summary>
        ''' <param name="koseiMatrix">構成マトリクス(部品表編集情報)</param>
        ''' <remarks></remarks>
        Private Sub InsertByBuhinEditBase(ByVal koseiMatrix As BuhinKoseiMatrix)

            Dim aDate As New ShisakuDate
            Dim editBasedao As TShisakuBuhinEditBaseDao = New TShisakuBuhinEditBaseDaoImpl

            '部品番号表示順用'
            Dim BuhinNoHyoujiJun As Integer = 0

            For index As Integer = 0 To koseiMatrix.GetInputRowIndexes.Count - 1

                'テストのため、回数を一旦制限しておく'
                'If index = 2 Then
                '    Return
                'End If
                If StringUtil.IsEmpty(koseiMatrix(index).BuhinNo) Then
                    Continue For
                End If

                Dim param As New TShisakuBuhinEditBaseVo
                param.ShisakuEventCode = blockVo.ShisakuEventCode
                param.ShisakuBukaCode = blockVo.ShisakuBukaCode
                param.ShisakuBlockNo = blockVo.ShisakuBlockNo
                param.ShisakuBlockNoKaiteiNo = blockVo.ShisakuBlockNoKaiteiNo
                param.BuhinNoHyoujiJun = BuhinNoHyoujiJun
                param.Level = koseiMatrix(index).Level
                param.ShukeiCode = koseiMatrix(index).ShukeiCode
                param.SiaShukeiCode = koseiMatrix(index).SiaShukeiCode
                param.GencyoCkdKbn = koseiMatrix(index).GencyoCkdKbn
                param.KyoukuSection = koseiMatrix(index).KyoukuSection
                param.MakerCode = koseiMatrix(index).MakerCode
                param.MakerName = koseiMatrix(index).MakerName
                param.BuhinNo = koseiMatrix(index).BuhinNo
                param.BuhinNoKbn = koseiMatrix(index).BuhinNoKbn
                param.BuhinNoKaiteiNo = koseiMatrix(index).BuhinNoKaiteiNo
                param.EdaBan = koseiMatrix(index).EdaBan
                param.BuhinName = koseiMatrix(index).BuhinName
                param.Saishiyoufuka = koseiMatrix(index).Saishiyoufuka
                param.ShutuzuYoteiDate = koseiMatrix(index).ShutuzuYoteiDate
                param.ZaishituKikaku1 = koseiMatrix(index).ZaishituKikaku1
                param.ZaishituKikaku2 = koseiMatrix(index).ZaishituKikaku2
                param.ZaishituKikaku3 = koseiMatrix(index).ZaishituKikaku3
                param.ZaishituMekki = koseiMatrix(index).ZaishituMekki
                param.ShisakuBankoSuryo = koseiMatrix(index).ShisakuBankoSuryo
                param.ShisakuBankoSuryoU = koseiMatrix(index).ShisakuBankoSuryoU


                ''↓↓2014/12/26 メタル対応追加フィールド (DANIEL)柳沼 ADD BEGIN
                '材料情報・製品長
                param.MaterialInfoLength = koseiMatrix(index).MaterialInfoLength
                '' 材料情報・製品幅
                param.MaterialInfoWidth = koseiMatrix(index).MaterialInfoWidth
                '' データ項目・改訂№
                param.DataItemKaiteiNo = koseiMatrix(index).DataItemKaiteiNo
                '' データ項目・エリア名
                param.DataItemAreaName = koseiMatrix(index).DataItemAreaName
                '' データ項目・セット名
                param.DataItemSetName = koseiMatrix(index).DataItemSetName
                '' データ項目・改訂情報
                param.DataItemKaiteiInfo = koseiMatrix(index).DataItemKaiteiInfo
                ''↑↑2014/12/26 メタル対応追加フィールド (DANIEL)柳沼 ADD END


                param.ShisakuBuhinHi = koseiMatrix(index).ShisakuBuhinHi
                param.ShisakuKataHi = koseiMatrix(index).ShisakuKataHi
                '2012/01/25 部品ノート追加
                param.BuhinNote = koseiMatrix(index).BuhinNote
                param.Bikou = koseiMatrix(index).Bikou
                param.EditTourokubi = koseiMatrix(index).EditTourokubi
                param.EditTourokujikan = koseiMatrix(index).EditTourokujikan
                param.KaiteiHandanFlg = koseiMatrix(index).KaiteiHandanFlg
                param.ShisakuListCode = koseiMatrix(index).ShisakuListCode

                param.CreatedUserId = LoginInfo.Now.UserId
                param.CreatedDate = aDate.CurrentDateDbFormat
                param.CreatedTime = aDate.CurrentTimeDbFormat
                param.UpdatedUserId = LoginInfo.Now.UserId
                param.UpdatedDate = aDate.CurrentDateDbFormat
                param.UpdatedTime = aDate.CurrentTimeDbFormat

                editBasedao.InsertBy(param)

                BuhinNoHyoujiJun = BuhinNoHyoujiJun + 1

            Next
        End Sub

        Private Sub InsertByBuhinEditInstl(ByVal koseiMatrix As BuhinKoseiMatrix)

            Dim aDate As New ShisakuDate
            Dim editInstldao As TShisakuBuhinEditInstlDao = New TShisakuBuhinEditInstlDaoImpl

            '縦'
            Dim row As Integer = 0
            Dim col As Integer = 0

            For rowindex As Integer = 0 To koseiMatrix.GetInputRowIndexes.Count - 1
                '横'
                For columnIndex As Integer = 0 To koseiMatrix.GetInputInsuColumnIndexes.Count - 1
                    Dim param As New TShisakuBuhinEditInstlVo
                    param.ShisakuEventCode = blockVo.ShisakuEventCode
                    param.ShisakuBukaCode = blockVo.ShisakuBukaCode
                    param.ShisakuBlockNo = blockVo.ShisakuBlockNo
                    param.ShisakuBlockNoKaiteiNo = blockVo.ShisakuBlockNoKaiteiNo

                    param.InstlHinbanHyoujiJun = columnIndex
                    param.BuhinNoHyoujiJun = rowindex
                    param.InsuSuryo = koseiMatrix.InsuSuryo(rowindex, columnIndex)

                    'Nothingの項目を飛ばす'
                    If param.InsuSuryo Is Nothing Then
                        Continue For
                    End If


                    param.SaisyuKoushinbi = Integer.Parse(Replace(aDate.CurrentDateDbFormat, "-", ""))

                    param.CreatedUserId = LoginInfo.Now.UserId
                    param.CreatedDate = aDate.CurrentDateDbFormat
                    param.CreatedTime = aDate.CurrentTimeDbFormat
                    param.UpdatedUserId = LoginInfo.Now.UserId
                    param.UpdatedDate = aDate.CurrentDateDbFormat
                    param.UpdatedTime = aDate.CurrentTimeDbFormat

                    editInstldao.InsertBy(param)

                Next
                row = row + 1
            Next

        End Sub

        Private Sub InsertByBuhinEditInstlBase(ByVal koseiMatrix As BuhinKoseiMatrix)

            Dim aDate As New ShisakuDate
            Dim editInstldao As TShisakuBuhinEditInstlBaseDao = New TShisakuBuhinEditInstlBaseDaoImpl

            '縦'
            For rowindex As Integer = 0 To koseiMatrix.GetInputRowIndexes.Count - 1
                'If rowindex = 2 Then
                '    Return
                'End If
                'テスト用(員数がNothingの項目がある)'
                'If StringUtil.Equals(blockVo.ShisakuBlockNo, "443A") Then
                '    Dim str As String = "AAA"
                'End If

                '横'
                For columnIndex As Integer = 0 To koseiMatrix.GetInputInsuColumnIndexes.Count - 1
                    Dim param As New TShisakuBuhinEditInstlBaseVo
                    param.ShisakuEventCode = blockVo.ShisakuEventCode
                    param.ShisakuBukaCode = blockVo.ShisakuBukaCode
                    param.ShisakuBlockNo = blockVo.ShisakuBlockNo
                    param.ShisakuBlockNoKaiteiNo = blockVo.ShisakuBlockNoKaiteiNo

                    param.InstlHinbanHyoujiJun = columnIndex
                    param.BuhinNoHyoujiJun = rowindex

                    'Nothingの場合がある・・・ 樺澤 '
                    param.InsuSuryo = koseiMatrix.InsuSuryo(rowindex, columnIndex)

                    'Nothingの項目を飛ばす'
                    If param.InsuSuryo Is Nothing Then
                        Continue For
                    End If

                    param.SaisyuKoushinbi = Integer.Parse(Replace(aDate.CurrentDateDbFormat, "-", ""))

                    param.CreatedUserId = LoginInfo.Now.UserId
                    param.CreatedDate = aDate.CurrentDateDbFormat
                    param.CreatedTime = aDate.CurrentTimeDbFormat
                    param.UpdatedUserId = LoginInfo.Now.UserId
                    param.UpdatedDate = aDate.CurrentDateDbFormat
                    param.UpdatedTime = aDate.CurrentTimeDbFormat

                    editInstldao.InsertBy(param)

                Next
            Next

        End Sub


        ''' <summary>
        ''' 部品表編集情報を登録する
        ''' </summary>
        ''' <param name="koseiMatrix">構成マトリクス(部品表編集情報)</param>
        ''' <remarks></remarks>
        Private Sub InsertByBuhinEditAndBase(ByVal koseiMatrix As BuhinKoseiMatrix)

            Dim impl As BuhinEditBaseDao = New BuhinEditBaseDaoImpl

            ''↓↓2014/08/19 Ⅰ.5.EBOM差分出力 ba) (TES)張 CHG BEGIN
            'impl.InsertBySekkeiBuhinEdit(blockVo.ShisakuEventCode, _
            '                             blockVo.ShisakuBukaCode, _
            '                             blockVo.ShisakuBlockNo, _
            '                             blockVo.ShisakuBlockNoKaiteiNo, _
            '                             koseiMatrix, _
            '                             JikyuUmu, _
            '                             TsukurikataFlg)
            impl.InsertBySekkeiBuhinEdit(blockVo.ShisakuEventCode, _
                                         blockVo.ShisakuBukaCode, _
                                         blockVo.ShisakuBlockNo, _
                                         blockVo.ShisakuBlockNoKaiteiNo, _
                                         koseiMatrix, _
                                         JikyuUmu, _
                                         TsukurikataFlg, _
                                         login)
            ''↑↑2014/08/19 Ⅰ.5.EBOM差分出力 ba) (TES)張 CHG END
        End Sub

        ''' <summary>
        ''' 部品表編集情報を登録する
        ''' </summary>
        ''' <param name="koseiMatrix">構成マトリクス(部品表編集情報)</param>
        ''' <remarks></remarks>
        Private Sub InsertByBuhinEditInstlAndBase(ByVal koseiMatrix As BuhinKoseiMatrix)

            Dim impl As BuhinEditBaseDao = New BuhinEditBaseDaoImpl

            ''↓↓2014/08/19 Ⅰ.5.EBOM差分出力 bb) (TES)張 CHG START
            'impl.InsertBySekkeiBuhinEditInstl(blockVo.ShisakuEventCode, _
            '                                 blockVo.ShisakuBukaCode, _
            '                                 blockVo.ShisakuBlockNo, _
            '                                 blockVo.ShisakuBlockNoKaiteiNo, _
            '                                 koseiMatrix)
            impl.InsertBySekkeiBuhinEditInstl(blockVo.ShisakuEventCode, _
                                            blockVo.ShisakuBukaCode, _
                                            blockVo.ShisakuBlockNo, _
                                            blockVo.ShisakuBlockNoKaiteiNo, _
                                            koseiMatrix, _
                                            login)
            ''↑↑2014/08/19 Ⅰ.5.EBOM差分出力 bb) (TES)張 CHG END
        End Sub

        ''' <summary>
        ''' 部品表編集情報を登録する
        ''' </summary>
        ''' <param name="koseiMatrix">構成マトリクス(部品表編集情報)</param>
        ''' <remarks></remarks>
        Private Sub InsertByBuhinEditAndBaseEvent(ByVal koseiMatrix As BuhinKoseiMatrix)

            Dim impl As BuhinEditBaseDao = New BuhinEditBaseDaoImpl

            impl.InsertBySekkeiBuhinEditEvent(blockVo.ShisakuEventCode, _
                                              blockVo.ShisakuBukaCode, _
                                              blockVo.ShisakuBlockNo, _
                                              blockVo.ShisakuBlockNoKaiteiNo, _
                                              koseiMatrix, _
                                              JikyuUmu, _
                                              TsukurikataFlg)
        End Sub

        ''' <summary>
        ''' 部品表編集情報を登録する
        ''' </summary>
        ''' <param name="koseiMatrix">構成マトリクス(部品表編集情報)</param>
        ''' <remarks></remarks>
        Private Sub InsertByBuhinEditInstlAndBaseEvent(ByVal koseiMatrix As BuhinKoseiMatrix)

            Dim impl As BuhinEditBaseDao = New BuhinEditBaseDaoImpl

            impl.InsertBySekkeiBuhinEditInstlEvent(blockVo.ShisakuEventCode, _
                                             blockVo.ShisakuBukaCode, _
                                             blockVo.ShisakuBlockNo, _
                                             blockVo.ShisakuBlockNoKaiteiNo, _
                                             koseiMatrix, _
                                             JikyuUmu)
        End Sub

        ''' <summary>
        ''' 設計ブロックINSTL情報を交信する
        ''' </summary>
        ''' <param name="koseiMatrix"></param>
        ''' <remarks></remarks>
        Private Sub UpdateBySekkeiBlockInstlAndBase(ByVal koseiMatrix As BuhinKoseiMatrix)
            Dim impl As BuhinEditBaseDao = New BuhinEditBaseDaoImpl

            impl.UpdateBySekkeiBlockInstl(blockVo.ShisakuEventCode, _
                                             blockVo.ShisakuBukaCode, _
                                             blockVo.ShisakuBlockNo, _
                                             koseiMatrix)
        End Sub

        ''' <summary>
        ''' 設計ブロック情報と設計ブロックINSTL情報を削除する
        ''' </summary>
        ''' <remarks></remarks>
        Private Sub DeleteBySekkeiBlockAndInstl()
            Dim impl As BuhinEditBaseDao = New BuhinEditBaseDaoImpl

            impl.DeleteBySekkeiBlockAndInstl(blockVo.ShisakuEventCode, _
                                             blockVo.ShisakuBukaCode, _
                                             blockVo.ShisakuBlockNo)
        End Sub


#End Region

        Protected Overrides Sub Finalize()
            MyBase.Finalize()
        End Sub
    End Class

End Namespace