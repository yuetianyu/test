Imports EventSakusei.ShisakuBuhinEdit.Logic.Detect
Imports EventSakusei.ShisakuBuhinEdit.Ikkatsu.Logic
Imports EventSakusei.ShisakuBuhinEdit.SourceSelector.Logic
Imports EventSakusei.ShisakuBuhinEdit.Kosei.Logic.Matrix
Imports EventSakusei.ShisakuBuhinEdit.Logic
Imports ShisakuCommon.Db.EBom.Dao
Imports EventSakusei.ShisakuBuhinEdit.Kosei.Logic.Merge
Imports ShisakuCommon
Imports ShisakuCommon.Util
Imports ShisakuCommon.Db.EBom.Vo
Imports EventSakusei.ShisakuBuhinEdit.Kosei.Ui
Imports EventSakusei.ShisakuBuhinEdit.Kosei.Dao
'↓↓2014/10/29 酒井 ADD BEGIN
'Ver6_2 1.95以降の修正内容の展開
Imports EBom.Common
'↑↑2014/10/29 酒井 ADD END
Imports ShisakuCommon.Util.LabelValue
Imports System.Text
Imports ShisakuCommon.Db.EBom
''↓↓2014/09/08 Ⅰ.3.設計編集 ベース車改修専用化 酒井 ADD BEGIN
Imports ShisakuCommon.Db.EBom.Dao.Impl
''↑↑2014/09/08 Ⅰ.3.設計編集 ベース車改修専用化 酒井 ADD END

Namespace ShisakuBuhinEdit.Kosei.Logic
    ''' <summary>
    ''' 部品構成編集画面の表示全般を担うクラス
    ''' </summary>
    ''' <remarks></remarks>
    Public Class BuhinEditKoseiSubject : Inherits Observable

        ''↓↓2014/08/29 Ⅰ.3.設計編集 ベース車改修専用化 酒井 ADD BEGIN
        'Private _koseiMatrix As BuhinKoseiMatrix
        Public _koseiMatrix As BuhinKoseiMatrix
        ''↑↑2014/08/29 Ⅰ.3.設計編集 ベース車改修専用化 酒井 ADD END

        ''↓↓2014/09/08 Ⅰ.3.設計編集 ベース車改修専用化 酒井 ADD BEGIN
        Public JikyuFlg As Boolean
        ''↑↑2014/09/08 Ⅰ.3.設計編集 ベース車改修専用化 酒井 ADD END

        Private blockVo As TShisakuSekkeiBlockVo
        Private ReadOnly login As LoginInfo
        Private ReadOnly aShisakuDate As ShisakuDate
        Private ReadOnly detector As DetectLatestStructure

        ''↓↓2014/08/29 Ⅰ.3.設計編集 ベース車改修専用化 酒井 ADD BEGIN
        'Private ReadOnly instlTitle As BuhinEditKoseiInstlTitle
        Public ReadOnly instlTitle As BuhinEditKoseiInstlTitle
        ''↑↑2014/08/29 Ⅰ.3.設計編集 ベース車改修専用化 酒井 ADD END

        Private ReadOnly editDao As TShisakuBuhinEditDao
        Private ReadOnly editInstlDao As TShisakuBuhinEditInstlDao

        Private ReadOnly make As MakeStructureResult
        Private ReadOnly aMakerNameResolver As MakerNameResolver

        Private isWaitingKoseiTenkai As Boolean
        Private shisakuEventCode As String
        Public ReadOnly Property EventCode() As String
            Get
                Return shisakuEventCode
            End Get
        End Property
        Public ReadOnly Property BaseBuhinFlg(ByVal rowIndex As Integer) As String
            Get
                Return _koseiMatrix.Record(rowIndex).BaseBuhinFlg
            End Get
        End Property

        Public Property BaseInstlFlg(ByVal columnIndex As Integer) As String
            Get
                Return instlTitle.BaseInstlFlg(columnIndex)
            End Get
            Set(ByVal value As String)
                If EzUtil.IsEqualIfNull(instlTitle.BaseInstlFlg(columnIndex), value) Then
                    Return
                End If
                instlTitle.BaseInstlFlg(columnIndex) = value
                SetChanged()

                OnChangedInstlHinbanOrKbn(columnIndex)
            End Set
        End Property

        Public KounyuShiji As String

        Private Jikyu As String
        Private _Tsukurikata As String
        Public ReadOnly Property Tsukurikata() As Integer
            Get
                Return _Tsukurikata
            End Get
        End Property
        Private makeShisakuBlockDao As MakeShisakuBlockDao = New MakeShisakuBlockDaoImpl
        '2012/02/02 部品取得時に使用'
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
                SetChanged()
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
                SetChanged()
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
                SetChanged()
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
                SetChanged()
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
                SetChanged()

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
                SetChanged()
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
                SetChanged()

                '通常は子部品情報を取得しない。　（仕様変更）　By柳沼
                OnChangedBuhinNoGetMakerAndName(rowIndex)
            End Set
        End Property

        ''' <summary>部品番号</summary>
        ''' <value>部品番号</value>
        ''' <returns>部品番号</returns>
        Public Property BuhinNoBom(ByVal rowIndex As Integer) As String
            Get
                Return Record(rowIndex).BuhinNo
            End Get
            Set(ByVal value As String)

                '以下の処理は変更の有無をチェックしている。
                'このプロパティはメニューから実行されるので変更有無チェックは不要とする。　ｂｙ柳沼
                'If EzUtil.IsEqualIfNull(Record(rowIndex).BuhinNo, value) Then
                '    Return
                'End If

                Record(rowIndex).BuhinNo = value
                SetChanged()

                '子部品を取得する　By柳沼
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
                SetChanged()
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
                SetChanged()
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
                SetChanged()
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
                SetChanged()
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
                SetChanged()
            End Set
        End Property

        ''' <summary>
        ''' 供給セクション
        ''' </summary>
        ''' <param name="rowIndex"></param>
        ''' <value>供給セクション</value>
        ''' <returns>供給セクション</returns>
        ''' <remarks></remarks>
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
                SetChanged()
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
                SetChanged()
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
                SetChanged()
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
                SetChanged()
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
                SetChanged()
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
                SetChanged()
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
                SetChanged()
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
                SetChanged()
            End Set
        End Property

        ''↓↓2014/07/23 Ⅰ.2.管理項目追加_w) (TES)張 ADD BEGIN
        ''' <summary>作り方・製作方法</summary>
        ''' <value>作り方・製作方法</value>
        ''' <returns>作り方・製作方法</returns>
        Public Property TsukurikataSeisaku(ByVal rowIndex As Integer) As String
            Get
                Return Record(rowIndex).TsukurikataSeisaku
            End Get
            Set(ByVal value As String)
                If EzUtil.IsEqualIfNull(Record(rowIndex).TsukurikataSeisaku, value) Then
                    Return
                End If
                Record(rowIndex).TsukurikataSeisaku = value
                SetChanged()
            End Set
        End Property

        ''' <summary>作り方・型仕様</summary>
        ''' <value>作り方・型仕様</value>
        ''' <returns>作り方・型仕様</returns>
        Public Property TsukurikataKatashiyou1(ByVal rowIndex As Integer) As String
            Get
                Return Record(rowIndex).TsukurikataKatashiyou1
            End Get
            Set(ByVal value As String)
                If EzUtil.IsEqualIfNull(Record(rowIndex).TsukurikataKatashiyou1, value) Then
                    Return
                End If
                Record(rowIndex).TsukurikataKatashiyou1 = value
                SetChanged()
            End Set
        End Property
        Public Property TsukurikataKatashiyou2(ByVal rowIndex As Integer) As String
            Get
                Return Record(rowIndex).TsukurikataKatashiyou2
            End Get
            Set(ByVal value As String)
                If EzUtil.IsEqualIfNull(Record(rowIndex).TsukurikataKatashiyou2, value) Then
                    Return
                End If
                Record(rowIndex).TsukurikataKatashiyou2 = value
                SetChanged()
            End Set
        End Property
        Public Property TsukurikataKatashiyou3(ByVal rowIndex As Integer) As String
            Get
                Return Record(rowIndex).TsukurikataKatashiyou3
            End Get
            Set(ByVal value As String)
                If EzUtil.IsEqualIfNull(Record(rowIndex).TsukurikataKatashiyou3, value) Then
                    Return
                End If
                Record(rowIndex).TsukurikataKatashiyou3 = value
                SetChanged()
            End Set
        End Property

        ''' <summary>作り方・治具</summary>
        ''' <value>作り方・治具</value>
        ''' <returns>作り方・治具</returns>
        Public Property TsukurikataTigu(ByVal rowIndex As Integer) As String
            Get
                Return Record(rowIndex).TsukurikataTigu
            End Get
            Set(ByVal value As String)
                If EzUtil.IsEqualIfNull(Record(rowIndex).TsukurikataTigu, value) Then
                    Return
                End If
                Record(rowIndex).TsukurikataTigu = value
                SetChanged()
            End Set
        End Property


        ''' <summary>作り方・納入見通し</summary>
        ''' <value>作り方・納入見通し</value>
        ''' <returns>作り方・納入見通し</returns>
        ''' <history>
        '''   20140818 Sakai Add
        ''' </history>
        Public Property TsukurikataNounyu(ByVal rowIndex As Integer) As Int32?
            Get
                Return Record(rowIndex).TsukurikataNounyu
            End Get
            '20140818 Sakai Add
            '            Set(ByVal value As String)
            Set(ByVal value As Int32?)
                If EzUtil.IsEqualIfNull(Record(rowIndex).TsukurikataNounyu, value) Then
                    Return
                End If
                Record(rowIndex).TsukurikataNounyu = value
                SetChanged()
            End Set
        End Property

        ''' <summary>作り方・部品製作規模・概要</summary>
        ''' <value>作り方・部品製作規模・概要</value>
        ''' <returns>作り方・部品製作規模・概要</returns>
        Public Property TsukurikataKibo(ByVal rowIndex As Integer) As String
            Get
                Return Record(rowIndex).TsukurikataKibo
            End Get
            Set(ByVal value As String)
                If EzUtil.IsEqualIfNull(Record(rowIndex).TsukurikataKibo, value) Then
                    Return
                End If
                Record(rowIndex).TsukurikataKibo = value
                SetChanged()
            End Set
        End Property
        ''↑↑2014/07/23 Ⅰ.2.管理項目追加_w) (TES)張 ADD END

        '↓↓↓2014/12/24 メタル項目を追加 TES)張 ADD BEGIN
        ''' <summary>製品サイズ・製品長</summary>
        ''' <value>製品サイズ・製品長</value>
        ''' <returns>製品サイズ・製品長</returns>
        Public Property MaterialInfoLength(ByVal rowIndex As Integer) As Int32?
            Get
                Return Record(rowIndex).MaterialInfoLength
            End Get
            Set(ByVal value As Int32?)
                If EzUtil.IsEqualIfNull(Record(rowIndex).MaterialInfoLength, value) Then
                    Return
                End If
                Record(rowIndex).MaterialInfoLength = value
                SetChanged()
            End Set
        End Property
        ''' <summary>製品サイズ・製品幅</summary>
        ''' <value>製品サイズ・製品幅</value>
        ''' <returns>製品サイズ・製品幅</returns>
        Public Property MaterialInfoWidth(ByVal rowIndex As Integer) As Int32?
            Get
                Return Record(rowIndex).MaterialInfoWidth
            End Get
            Set(ByVal value As Int32?)
                If EzUtil.IsEqualIfNull(Record(rowIndex).MaterialInfoWidth, value) Then
                    Return
                End If
                Record(rowIndex).MaterialInfoWidth = value
                SetChanged()
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
                SetChanged()
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
                SetChanged()
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
                SetChanged()
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
                SetChanged()
            End Set
        End Property
        '↑↑↑2014/12/24 メタル項目を追加 TES)張 ADD END

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
                SetChanged()
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
                SetChanged()
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
                SetChanged()
            End Set
        End Property
        ''' <summary>部品ノート</summary>
        ''' <value>部品ノート</value>
        ''' <returns>部品ノート</returns>
        Public Property BuhinNote(ByVal rowIndex As Integer) As String
            '2012/01/25 部品ノート追加
            Get
                Return Record(rowIndex).BuhinNote
            End Get
            Set(ByVal value As String)
                If EzUtil.IsEqualIfNull(Record(rowIndex).BuhinNote, value) Then
                    Return
                End If
                Record(rowIndex).BuhinNote = value
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

        Public Sub New(ByVal blockVo As TShisakuSekkeiBlockVo, ByVal login As LoginInfo, ByVal aShisakuDate As ShisakuDate, _
                       ByVal anEzTunnel As EzTunnelAlKosei, _
                       ByVal detector As DetectLatestStructure, _
                       ByVal makeStructure As MakeStructureResult, _
                       ByVal aMakerNameResolver As MakerNameResolver, _
                       ByVal editDao As TShisakuBuhinEditDao, _
                       ByVal editInstlDao As TShisakuBuhinEditInstlDao)
            Me.blockVo = blockVo
            Me.login = login
            Me.aShisakuDate = aShisakuDate
            Me.detector = detector
            Me.make = makeStructure
            Me.aMakerNameResolver = aMakerNameResolver

            shisakuEventCode = blockVo.ShisakuEventCode

            '2012/01/16 自給品有無判定追加
            Dim mimpl As MakeShisakuBlockDao = New MakeShisakuBlockDaoImpl
            Jikyu = mimpl.FindByJikyuUmu(shisakuEventCode)

            ''↓↓2014/07/24 Ⅰ.3.設計編集 ベース改修専用化_f) (TES)張 ADD BEGIN
            KounyuShiji = mimpl.FindByKounyuShiji(shisakuEventCode)

            ''↓↓2014/07/23 Ⅰ.2.管理項目追加_af) (TES)張 ADD BEGIN
            _Tsukurikata = mimpl.FindByTsukurikata(shisakuEventCode)

            Me.editDao = editDao
            Me.editInstlDao = editInstlDao

            Me.instlTitle = New BuhinEditKoseiInstlTitle(anEzTunnel)

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

            SetChanged()
        End Sub

        ''' <summary>
        ''' 必要な諸設定が済んだ後に呼び出される初期処理
        ''' </summary>
        ''' <remarks></remarks>
        Public Sub PerformInitialized()
            If Not isWaitingKoseiTenkai Then
                Return
            End If
            isWaitingKoseiTenkai = False
            _koseiMatrix = NewMatrixKoseiTenkai(True, "0")
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
                SetChanged()

                OnChangedInstlHinbanOrKbn(columnIndex)
            End Set
        End Property

        ''' <summary>INSTL品番</summary>
        ''' <param name="columnIndex">列No</param>
        Public Property InstlHinban2(ByVal columnIndex As Integer) As String
            Get
                Return instlTitle.InstlHinban(columnIndex)
            End Get
            Set(ByVal value As String)
                If EzUtil.IsEqualIfNull(instlTitle.InstlHinban(columnIndex), value) Then
                    Return
                End If
                instlTitle.InstlHinban(columnIndex) = value

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
                SetChanged()

                OnChangedInstlHinbanOrKbn(columnIndex)
            End Set
        End Property


        ''' <summary>INSTLデータ区分</summary>
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
                SetChanged()

                OnChangedInstlHinbanOrKbn(columnIndex)
            End Set
        End Property

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
            NotifyObservers(NotifyInfo.NewRow(rowIndexes))
        End Sub

        ''' <summary>
        ''' 列タイトル部を表示更新する
        ''' </summary>
        ''' <param name="columnIndex">列タイトルの列indx</param>
        ''' <remarks></remarks>
        Public Sub NotifyTitleObservers(ByVal columnIndex As Integer)
            NotifyObservers(NotifyInfo.NewTitleColumn(columnIndex))
        End Sub

        ''' <summary>
        ''' 列タイトル部を表示更新する(イベントコピー用)
        ''' </summary>
        ''' <param name="columnIndex">列タイトルの列indx</param>
        ''' <remarks></remarks>
        Public Sub NotifyTitleObserversEventCopy(ByVal columnIndex As Integer)
            NotifyObservers(NotifyInfo.NewTitleColumn(columnIndex))
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

            'Dim newKoseiMatrix As New BuhinKoseiMatrix

            'newKoseiMatrix = NewMatrixKoseiTenkai(True)

            'Dim editSupplier As New BuhinEditKoseiEditSupplier(blockVo, newKoseiMatrix)
            'editSupplier.Update(login, editDao, aShisakuDate)

            'Dim editInstlSupplier As New BuhinEditKoseiEditInstlSupplier(blockVo, newKoseiMatrix)
            'editInstlSupplier.Update(login, editInstlDao, aShisakuDate)

            Dim editSupplier As New BuhinEditKoseiEditSupplier(blockVo, _koseiMatrix)
            editSupplier.Update(login, editDao, aShisakuDate)

            Dim editInstlSupplier As New BuhinEditKoseiEditInstlSupplier(blockVo, _koseiMatrix)
            editInstlSupplier.Update(login, editInstlDao, aShisakuDate)
        End Sub

        ''' <summary>
        ''' 部品構成編集クラスを差し替える
        ''' </summary>
        ''' <param name="newMatrix">新しい部品構成編集クラス</param>
        ''' <remarks></remarks>
        Public Sub SupersedeMatrix(ByVal newMatrix As BuhinKoseiMatrix)
            Me._koseiMatrix = newMatrix
            SetChanged()
        End Sub

        ''' <summary>
        ''' 「最新化」処理の結果、新しい部品構成編集クラスを返す
        ''' </summary>
        ''' <param name="columnIndex">対象の列index</param>
        ''' <param name="columnCount">対象列の数</param>
        ''' <param name="isReuseStructure"></param>
        ''' <returns>新しい部品構成編集クラス</returns>
        ''' <remarks></remarks>
        Public Function NewMatrixLatest(ByVal columnIndex As Integer, ByVal columnCount As Integer, ByVal isReuseStructure As Boolean, ByVal JikyuUmu As String) As BuhinKoseiMatrix
            Jikyu = JikyuUmu

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

                    'Dim tescolumnBag As New InstlColumnBag

                    '自給品の削除'
                    If StringUtil.Equals(Jikyu, "0") Then
                        columnBag = removeJikyu(columnBag)
                    End If

                    'mergeColumn.Compute(buhinKoseiMatrix.InstlColumn(0), inputtedColumnIndex)

                    'If columnBag.Count = 0 Then
                    '    Dim Level0InstlColumn As New InstlColumnBag()
                    '    Level0InstlColumn.Insert(columnIndex, _koseiMatrix.InstlColumn(columnIndex).Record(0), _koseiMatrix.InstlColumn(columnIndex).InsuVo(0))
                    '    mergeColumn.Compute(Level0InstlColumn, columnIndex)
                    'Else
                    mergeColumn.Compute(columnBag, inputtedColumnIndex)
                    'End If
                Else
                    '05/10 樺澤
                    '列情報が消える場合がある
                    'InstlColumnの問題？
                    '
                    If isReuseStructure Then
                        'InstlColumn列の存在チェック
                        If Not _koseiMatrix.InstlColumn(inputtedColumnIndex) Is Nothing Then

                            If StringUtil.Equals(Jikyu, "0") Then
                                _koseiMatrix.InstlColumnAdd(inputtedColumnIndex, removeJikyu(_koseiMatrix.InstlColumn(inputtedColumnIndex)))
                            End If

                            mergeColumn.Compute(_koseiMatrix.InstlColumn(inputtedColumnIndex), inputtedColumnIndex)
                        End If
                    Else
                        Dim Level0InstlColumn As New InstlColumnBag()
                        Level0InstlColumn.Insert(inputtedColumnIndex, _koseiMatrix.InstlColumn(inputtedColumnIndex).Record(0), _koseiMatrix.InstlColumn(inputtedColumnIndex).InsuVo(0))
                        'Level0InstlColumn.Insert(columnIndex, _koseiMatrix.InstlColumn(columnIndex).Record(0), _koseiMatrix.InstlColumn(columnIndex).InsuVo(0))

                        'InstlColumnのレベル０の存在チェック
                        If Not Level0InstlColumn.Record(0) Is Nothing Then
                            'mergeColumn.Compute(Level0InstlColumn, columnIndex)
                            mergeColumn.Compute(Level0InstlColumn, inputtedColumnIndex)
                        End If

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
            '2012/02/25 列のないデータ対策'
            If columnBag Is Nothing Then
                Return Nothing
            End If

            If columnBag.Count = 0 Then
                Return columnBag
            End If


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
        ''' <param name="kaiteiNo">改定No　  2014/08/04 Ⅰ.11.改訂戻し機能　ｋ) (TES)施 追加 </param>
        ''' <returns>新しい部品構成編集クラス</returns>
        ''' <remarks></remarks>
        Public Function NewMatrixKoseiTenkai(ByVal isReuseStructure As Boolean, ByVal JikyuUmu As String, Optional ByVal KaiteiNo As String = "", Optional ByVal eventCopyFlg As Boolean = False, Optional ByVal baseFlg As Boolean = False, Optional ByVal addStartIndex As Integer = 0) As BuhinKoseiMatrix
            '↓↓2014/10/23 酒井 ADD BEGIN
            'Public Function NewMatrixKoseiTenkai(ByVal isReuseStructure As Boolean, ByVal JikyuUmu As String, Optional ByVal KaiteiNo As String = "", Optional ByVal eventCopyFlg As Boolean = False) As BuhinKoseiMatrix
            '↑↑2014/10/23 酒井 ADD END
            '↓↓2014/09/25 酒井 ADD BEGIN
            'Public Function NewMatrixKoseiTenkai(ByVal isReuseStructure As Boolean, ByVal JikyuUmu As String, Optional ByVal KaiteiNo As String = "") As BuhinKoseiMatrix
            '↑↑2014/09/25 酒井 ADD END
            Dim newMatrix As New BuhinKoseiMatrix
            Dim mergeColumn As New MergeInstlColumnBag(newMatrix)
            Jikyu = JikyuUmu
            a0553flag = 1



            Dim indexes As ICollection(Of Integer) = MergeCollectionAsSorted(_koseiMatrix.GetInputInsuColumnIndexes(), instlTitle.GetInputInstlHinbanColumnIndexes)
            For Each columnIndex As Integer In indexes
                '↓↓2014/10/23 酒井 ADD BEGIN
                If eventCopyFlg Then
                    If baseFlg Then
                        If columnIndex < addStartIndex Then
                            'addStartIndex以前はベース部品のため部品構成取得から除外
                            Continue For
                        End If
                    End If
                End If
                '↑↑2014/10/23 酒井 ADD END
                If GetStructureResult(columnIndex) Is Nothing Then
                    Throw New InvalidProgramException("anEzTunnel.DetectStructureResultsしたあとに nothing が返る事はあり得ない")
                End If

                '存在する部品番号によって処理を分岐。（IsExist＝存在する部品番号か？）
                If GetStructureResult(columnIndex).IsExist Then

                    ''↓↓2014/08/04 Ⅰ.11.改訂戻し機能 ｋ) (TES)施 ADD BEGIN
                    Dim columnBag As InstlColumnBag
                    If KaiteiNo = "" Then
                        '↓↓2014/09/25 酒井 ADD BEGIN
                        '                        columnBag = CreateInstlColumnBag(GetStructureResult(columnIndex))
                        If Not eventCopyFlg Then
                            columnBag = CreateInstlColumnBag(GetStructureResult(columnIndex))
                        Else
                            columnBag = CreateInstlColumnBag(GetStructureResult(columnIndex), KaiteiNo, InstlDataKbn(columnIndex), BaseInstlFlg(columnIndex), eventCopyFlg)
                        End If
                        '↑↑2014/09/25 酒井 ADD END
                    Else
                        ''↓↓2014/09/23 酒井 ADD BEGIN
                        'columnBag = CreateInstlColumnBag(GetStructureResult(columnIndex), KaiteiNo)
                        columnBag = CreateInstlColumnBag(GetStructureResult(columnIndex), KaiteiNo, InstlDataKbn(columnIndex), BaseInstlFlg(columnIndex))
                        ''↑↑2014/09/23 酒井 ADD END
                    End If

                    ''↑↑2014/08/04 Ⅰ.11.改訂戻し機能 ｋ) (TES)施 ADD END


                    '樺澤　構成無い場合に'
                    If columnBag Is Nothing Then
                        Continue For
                    End If
                    If columnBag.Count = 0 Then
                        Continue For
                    End If
                    '自給品の削除'
                    If StringUtil.Equals(Jikyu, "0") Then
                        columnBag = removeJikyu(columnBag)
                    End If
                    'If columnBag.Count = 0 Then
                    '    Dim Level0InstlColumn As New InstlColumnBag()
                    '    Level0InstlColumn.Insert(columnIndex, _koseiMatrix.InstlColumn(columnIndex).Record(0), _koseiMatrix.InstlColumn(columnIndex).InsuVo(0))
                    '    mergeColumn.Compute(Level0InstlColumn, columnIndex)
                    'Else
                    ''↓↓2014/09/24 酒井 ADD BEGIN
                    'mergeColumn.Compute(columnBag, columnIndex)
                    mergeColumn.Compute(columnBag, columnIndex, KaiteiNo)
                    ''↑↑2014/09/24 酒井 ADD END
                    'End If


                Else
                    If isReuseStructure Then
                        If StringUtil.Equals(Jikyu, "0") Then
                            _koseiMatrix.InstlColumnAdd(columnIndex, removeJikyu(_koseiMatrix.InstlColumn(columnIndex)))
                        End If

                        ''↓↓2014/09/24 酒井 ADD BEGIN
                        'mergeColumn.Compute(_koseiMatrix.InstlColumn(columnIndex), columnIndex)
                        mergeColumn.Compute(_koseiMatrix.InstlColumn(columnIndex), columnIndex, KaiteiNo)
                        ''↑↑2014/09/24 酒井 ADD END

                    Else
                        Dim Level0InstlColumn As New InstlColumnBag()
                        If _koseiMatrix.InstlColumn(columnIndex).Count > 0 Then
                            Level0InstlColumn.Insert(columnIndex, _koseiMatrix.InstlColumn(columnIndex).Record(0), _koseiMatrix.InstlColumn(columnIndex).InsuVo(0))
                            'InstlColumnのレベル０の存在チェック
                            If Not Level0InstlColumn.Record(0) Is Nothing Then
                                ''↓↓2014/09/24 酒井 ADD BEGIN
                                'mergeColumn.Compute(Level0InstlColumn, columnIndex)
                                mergeColumn.Compute(Level0InstlColumn, columnIndex, KaiteiNo)
                                ''↑↑2014/09/24 酒井 ADD END
                            End If
                        End If

                        'Level0InstlColumn.Insert(columnIndex, _koseiMatrix.InstlColumn(columnIndex).Record(0), _koseiMatrix.InstlColumn(columnIndex).InsuVo(0))

                        'InstlColumnのレベル０の存在チェック
                        'If Not Level0InstlColumn.Record(0) Is Nothing Then
                        '    mergeColumn.Compute(Level0InstlColumn, columnIndex)
                        'End If



                        ' nop
                        ' 「いいえ」選択時は、員数をクリアする
                        ' なので、ここは何もしなければ、員数がマージされずにクリアされる
                    End If
                End If
            Next
            Return newMatrix
        End Function

        ''' <summary>
        ''' 「一括設定」の情報をもとに、新しい部品構成編集クラスを返す
        ''' </summary>
        ''' <param name="structureResults">「一括設定」の情報</param>
        ''' <param name="JikyuUmu">自給有無（0:無し　1:あり</param>
        ''' <param name="sw">取得したINSTL品番を入れ替えるかどうかのフラグ（0:差し替えない　1:差し替える）</param>
        ''' <returns>新しい部品構成編集クラス</returns>
        ''' <remarks></remarks>
        Public Function NewMatrixBySpecified(ByVal isReuseStructure As Boolean, ByVal structureResults As IndexedList(Of StructureResult), ByVal JikyuUmu As String, ByVal sw As Integer, Optional ByVal flg As Boolean = False) As BuhinKoseiMatrix
            Dim newMatrix As New BuhinKoseiMatrix
            Dim mergeColumn As New MergeInstlColumnBag(newMatrix)
            Dim columnIndexes As ICollection(Of Integer) = MergeCollectionAsSorted(_koseiMatrix.GetInputInsuColumnIndexes(), structureResults.Keys)

            Jikyu = JikyuUmu
            '該当イベント取得
            Dim eventDao As TShisakuEventDao = New TShisakuEventDaoImpl
            Dim eventVo As TShisakuEventVo
            eventVo = eventDao.FindByPk(shisakuEventCode)

            For Each columnIndex As Integer In columnIndexes
                If eventVo.BlockAlertKind = 2 And eventVo.KounyuShijiFlg IsNot Nothing Then
                    If flg = True Then
                        '対象INSTL比較差分反映のため、ベース部品を削除する
                        If BaseInstlFlg(columnIndex) = "1" Then
                            Continue For
                        End If
                    End If
                End If
                If structureResults.Keys.Contains(columnIndex) AndAlso structureResults(columnIndex).IsExist Then

                    Dim columnBag As InstlColumnBag = CreateInstlColumnBag(structureResults(columnIndex))
                    '取得したINSTL品番を入れ替える。参照先のINSTL品番に変わっちゃうのが嫌なので。
                    'これが存在するとALのイベント品番コピー時にレベル０を消してしまう'
                    '呼び出だし元に応じてINSTL品番を入れ替えるかどうかを切り替える
                    If sw = 1 Then
                        '取得したINSTL品番を入れ替える。参照先のINSTL品番に変わっちゃうのが嫌なので。
                        If columnBag IsNot Nothing Then
                            columnBag.Remove(columnBag.Record(0))
                        Else
                            columnBag = New InstlColumnBag
                        End If
                        columnBag.Insert(0, _koseiMatrix.InstlColumn(columnIndex).Record(0), _koseiMatrix.InstlColumn(columnIndex).InsuVo(0))
                    End If

                    '自給品の削除'  2012/02/07 s.ota
                    If StringUtil.Equals(Jikyu, "0") Then
                        columnBag = removeJikyu(columnBag)
                    End If

                    mergeColumn.Compute(columnBag, columnIndex)
                Else

                    '該当イベント取得
                    If eventVo.BlockAlertKind = 2 And eventVo.KounyuShijiFlg IsNot Nothing And _
                       _koseiMatrix.InstlColumn(columnIndex).Record(0).BaseBuhinFlg = "1" Then
                        If StringUtil.Equals(Jikyu, "0") Then
                            _koseiMatrix.InstlColumnAdd(columnIndex, removeJikyu(_koseiMatrix.InstlColumn(columnIndex)))
                        End If

                        mergeColumn.Compute(_koseiMatrix.InstlColumn(columnIndex), columnIndex)
                    ElseIf isReuseStructure Then
                        'はい選択時の動き'
                        If StringUtil.Equals(Jikyu, "0") Then
                            _koseiMatrix.InstlColumnAdd(columnIndex, removeJikyu(_koseiMatrix.InstlColumn(columnIndex)))
                        End If

                        mergeColumn.Compute(_koseiMatrix.InstlColumn(columnIndex), columnIndex)
                    Else

                        '対象になった列以外は上と同じ'

                        'いいえ選択時の動き'
                        Dim Level0InstlColumn As New InstlColumnBag()
                        If _koseiMatrix.InstlColumn(columnIndex).Count > 0 Then
                            Level0InstlColumn.Insert(columnIndex, _koseiMatrix.InstlColumn(columnIndex).Record(0), _koseiMatrix.InstlColumn(columnIndex).InsuVo(0))
                            'InstlColumnのレベル０の存在チェック
                            If Not Level0InstlColumn.Record(0) Is Nothing Then
                                mergeColumn.Compute(Level0InstlColumn, columnIndex)
                            End If
                        End If

                    End If
                End If
            Next


            Return newMatrix
        End Function

        Public Function NewMatrixBySpecified(ByVal isReuseStructure As Boolean, ByVal structureResults As IndexedList(Of StructureResult), ByVal JikyuUmu As String, ByVal sw As Integer, ByVal column As Integer, ByVal columncount As Integer, Optional ByVal flg As Boolean = False) As BuhinKoseiMatrix
            Dim eventDao As TShisakuEventDao = New TShisakuEventDaoImpl
            Dim eventVo As TShisakuEventVo
            eventVo = eventDao.FindByPk(shisakuEventCode)
            Dim flgIkansha As Boolean = eventVo.BlockAlertKind = 2 And eventVo.KounyuShijiFlg = "0"


            Dim newMatrix As New BuhinKoseiMatrix
            'Dim mergeColumn As New MergeInstlColumnBag(newMatrix)
            Dim mergeColumn As New MergeInstlColumnBag(newMatrix, True, flgIkansha)
            Dim columnIndexes As ICollection(Of Integer) = MergeCollectionAsSorted(_koseiMatrix.GetInputInsuColumnIndexes(), structureResults.Keys)

            Jikyu = JikyuUmu
            '該当イベント取得
            For Each columnIndex As Integer In columnIndexes
                If Not IsTargetColumn(columnIndex, column, columncount) Then
                    '↓↓2014/10/14 酒井 ADD BEGIN
                    If Not flg Then
                        mergeColumn.Compute(_koseiMatrix.InstlColumn(columnIndex), columnIndex)
                    End If
                    '↑↑2014/10/14 酒井 ADD END
                    Continue For
                End If
                If structureResults.Keys.Contains(columnIndex) AndAlso structureResults(columnIndex).IsExist Then
                    'If structureResults.Keys.Contains(columnIndex) AndAlso structureResults(columnIndex).IsExist Then
                    '対象列以外には関与しない'

                    Dim columnBag As InstlColumnBag = CreateInstlColumnBag(structureResults(columnIndex))
                    '取得したINSTL品番を入れ替える。参照先のINSTL品番に変わっちゃうのが嫌なので。
                    'これが存在するとALのイベント品番コピー時にレベル０を消してしまう'
                    '呼び出だし元に応じてINSTL品番を入れ替えるかどうかを切り替える


                    If sw = 1 Then
                        '取得したINSTL品番を入れ替える。参照先のINSTL品番に変わっちゃうのが嫌なので。
                        columnBag.Remove(columnBag.Record(0))
                        columnBag.Insert(0, _koseiMatrix.InstlColumn(columnIndex).Record(0), _koseiMatrix.InstlColumn(columnIndex).InsuVo(0))
                    End If

                    '自給品の削除'  2012/02/07 s.ota
                    If StringUtil.Equals(Jikyu, "0") Then
                        columnBag = removeJikyu(columnBag)
                    End If

                    mergeColumn.Compute(columnBag, columnIndex)
                Else
                    ' ''↓↓2014/09/08 Ⅰ.3.設計編集 ベース車改修専用化 酒井 ADD BEGIN
                    If eventVo.BlockAlertKind = 2 AndAlso eventVo.KounyuShijiFlg = "0" AndAlso _
                       _koseiMatrix.InstlColumn(columnIndex).Record(0) IsNot Nothing AndAlso _koseiMatrix.InstlColumn(columnIndex).Record(0).BaseBuhinFlg = "1" Then
                        If StringUtil.Equals(Jikyu, "0") Then
                            _koseiMatrix.InstlColumnAdd(columnIndex, removeJikyu(_koseiMatrix.InstlColumn(columnIndex)))
                        End If

                        mergeColumn.Compute(_koseiMatrix.InstlColumn(columnIndex), columnIndex)
                    ElseIf isReuseStructure Then
                        ''↑↑2014/09/08 Ⅰ.3.設計編集 ベース車改修専用化 酒井 ADD END
                        'If isReuseStructure Then

                        'はい選択時の動き'
                        If StringUtil.Equals(Jikyu, "0") Then
                            _koseiMatrix.InstlColumnAdd(columnIndex, removeJikyu(_koseiMatrix.InstlColumn(columnIndex)))
                        End If

                        mergeColumn.Compute(_koseiMatrix.InstlColumn(columnIndex), columnIndex)

                    Else

                        '対象になった列以外は上と同じ'

                        'いいえ選択時の動き'
                        Dim Level0InstlColumn As New InstlColumnBag()
                        If _koseiMatrix.InstlColumn(columnIndex).Count > 0 Then
                            Level0InstlColumn.Insert(columnIndex, _koseiMatrix.InstlColumn(columnIndex).Record(0), _koseiMatrix.InstlColumn(columnIndex).InsuVo(0))
                            'InstlColumnのレベル０の存在チェック
                            If Not Level0InstlColumn.Record(0) Is Nothing Then
                                mergeColumn.Compute(Level0InstlColumn, columnIndex)
                            End If
                        End If

                    End If
                    '_koseiMatrix.InstlColumnAdd(columnIndex, _koseiMatrix.InstlColumn(columnIndex))

                    'mergeColumn.Compute(_koseiMatrix.InstlColumn(columnIndex), columnIndex)
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
                '子部品展開'
                a0553flag = 2
                Dim newKoseiMatrix As BuhinKoseiMatrix = GetNewKoseiMatrix(aStructureResult, Level(rowIndex))

                '構成が無い場合Nothingが帰る。
                'If newKoseiMatrix.InputRowCount = 0 Then
                '    Return
                'End If
                If newKoseiMatrix Is Nothing Then
                    Return
                End If

                Dim rowCount As Integer = 0
                '2012/03/05'
                '自給品を行から消してもデータ自体は行自体は残っているのでINSTLCOLUMNを利用する'

                If StringUtil.Equals(Jikyu, "0") Then
                    '自給品を削除
                    For Each srcIndex As Integer In newKoseiMatrix.GetInputRowIndexes
                        If StringUtil.Equals(newKoseiMatrix(srcIndex).ShukeiCode, "J") Then
                            newKoseiMatrix.RemoveRow(srcIndex)
                            rowCount = rowCount + 1
                        End If
                    Next
                    For Each index As Integer In newKoseiMatrix.GetInputInsuColumnIndexes()
                        newKoseiMatrix.InstlColumn(index) = removeJikyu(newKoseiMatrix.InstlColumn(index))
                    Next
                End If

                '2012/02/28 挿入を制限する'
                InsertRow(rowIndex + 1, newKoseiMatrix.InstlColumn(0).Count - 1)
                'InsertRow(rowIndex + 1, newKoseiMatrix.GetInputRowIndexes().Count - rowCount)

                Dim destIndex As Integer = 1
                For Each srcIndex As Integer In newKoseiMatrix.GetInputRowIndexes()
                    '自分は除く
                    If srcIndex <> -1 Then
                        If StringUtil.Equals(Jikyu, "0") Then
                            If Not StringUtil.IsEmpty(newKoseiMatrix(srcIndex).BuhinNo) Then
                                If StringUtil.IsEmpty(newKoseiMatrix(srcIndex).ShukeiCode) Then
                                    If Not StringUtil.Equals(newKoseiMatrix(srcIndex).SiaShukeiCode, "J") Then
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
                                    End If
                                Else
                                    If Not StringUtil.Equals(newKoseiMatrix(srcIndex).ShukeiCode, "J") Then
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
                                    End If

                                End If
                            End If
                        Else
                            If Not StringUtil.IsEmpty(newKoseiMatrix(srcIndex).BuhinNo) Then
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
                            End If
                        End If

                    End If
                    'destIndex += 1
                Next



                NotifyObservers()

                '部品構成情報をセットした行数を返す。
                SpdKoseiObserver.SPREAD_ROW = rowIndex + 1
                SpdKoseiObserver.SPREAD_ROWCOUNT = newKoseiMatrix.InstlColumn(0).Count - 1
                'SpdKoseiObserver.SPREAD_ROWCOUNT = newKoseiMatrix.GetInputRowIndexes().Count - 1
            Finally
                IsSuspendOnChangedBuhinNo = False
            End Try
        End Sub

        ''' <summary>
        ''' 画面の自給品有無を取得させる
        ''' </summary>
        ''' <remarks></remarks>
        Public Sub JikyuSet(ByVal JikyuUmu As String)
            If StringUtil.Equals(JikyuUmu, "N") Then
                Jikyu = "0"
            Else
                Jikyu = "1"
            End If
        End Sub

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
                If Me._koseiMatrix(rowindex).Level > 0 Then
                    Conditions1List = SortSetList(Conditions1, Me._koseiMatrix.Record(rowindex), Conditions1List)
                    'Conditions2List = SortSetList(Conditions2, Me._koseiMatrix.Record(rowindex), Conditions2List)
                    'Conditions3List = SortSetList(Conditions3, Me._koseiMatrix.Record(rowindex), Conditions3List)
                Else
                    Me._koseiMatrix.Record(rowindex).SortFlag = 1
                    level0Count = level0Count + 1
                End If
            Next
            'レベル０の直下を基準にする'
            level0Count = level0Count
            'level0Count = level0Count + 1
            'リストをソートする'
            'このリストの０番目が基準となる'
            Conditions1List.Sort()
            Conditions2List.Sort()
            Conditions3List.Sort()

            '降順の場合リストを逆転させる'
            If Not order1 Then
                Conditions1List.Reverse()
            End If
            'If Not order2 Then
            '    Conditions2List.Reverse()
            'End If
            'If Not order3 Then
            '    Conditions3List.Reverse()
            'End If


            For conIndex As Integer = 0 To Conditions1List.Count - 1
                For Each rowindex As Integer In Me._koseiMatrix.GetInputRowIndexes
                    If Not Me._koseiMatrix(rowindex).Level = 0 Then
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
                    ConditionsList.Add(record.Level)
                Case "取引先コード"
                    ConditionsList.Add(record.MakerCode)
                Case "部品番号"
                    ConditionsList.Add(record.BuhinNo)
                Case "部品名称"
                    ConditionsList.Add(record.BuhinName)
                Case "供給セクション"
                    ConditionsList.Add(record.KyoukuSection)
                Case "出図予定日"
                    ConditionsList.Add(record.ShutuzuYoteiDate)
                Case "材質規格1"
                    ConditionsList.Add(record.ZaishituKikaku1)
                Case "材質規格2"
                    ConditionsList.Add(record.ZaishituKikaku2)
                Case "材質規格3"
                    ConditionsList.Add(record.ZaishituKikaku3)
                Case "材質メッキ"
                    ConditionsList.Add(record.ZaishituMekki)
                Case "板厚"
                    ConditionsList.Add(record.ShisakuBankoSuryo)
                Case "板厚u"
                    ConditionsList.Add(record.ShisakuBankoSuryoU)
                Case "試作部品費"
                    ConditionsList.Add(record.ShisakuBuhinHi)
                Case "試作型費"
                    ConditionsList.Add(record.ShisakuKataHi)
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
                    If Integer.Parse(param) = record.Level Then
                        Return True
                    Else
                        Return False
                    End If
                Case "取引先コード"
                    If StringUtil.Equals(param, record.MakerCode) Then
                        Return True
                    Else
                        Return False
                    End If
                Case "部品番号"
                    If StringUtil.Equals(param, record.BuhinNo) Then
                        Return True
                    Else
                        Return False
                    End If
                Case "部品名称"
                    If StringUtil.Equals(param, record.BuhinName) Then
                        Return True
                    Else
                        Return False
                    End If
                Case "供給セクション"
                    If StringUtil.Equals(param, record.KyoukuSection) Then
                        Return True
                    Else
                        Return False
                    End If
                Case "出図予定日"
                    If Integer.Parse(param) = record.ShutuzuYoteiDate Then
                        Return True
                    Else
                        Return False
                    End If
                Case "材質規格1"
                    If StringUtil.Equals(param, record.ZaishituKikaku1) Then
                        Return True
                    Else
                        Return False
                    End If
                Case "材質規格2"
                    If StringUtil.Equals(param, record.ZaishituKikaku2) Then
                        Return True
                    Else
                        Return False
                    End If
                Case "材質規格3"
                    If StringUtil.Equals(param, record.ZaishituKikaku3) Then
                        Return True
                    Else
                        Return False
                    End If
                Case "材質メッキ"
                    If StringUtil.Equals(param, record.ZaishituMekki) Then
                        Return True
                    Else
                        Return False
                    End If
                Case "板厚"
                    If StringUtil.Equals(param, record.ShisakuBankoSuryo) Then
                        Return True
                    Else
                        Return False
                    End If
                Case "板厚u"
                    If StringUtil.Equals(param, record.ShisakuBankoSuryoU) Then
                        Return True
                    Else
                        Return False
                    End If
                Case "試作部品費"
                    If Integer.Parse(param) = record.ShisakuBuhinHi Then
                        Return True
                    Else
                        Return False
                    End If
                Case "試作型費"
                    If Integer.Parse(param) = record.ShisakuKataHi Then
                        Return True
                    Else
                        Return False
                    End If
            End Select
            Return False
        End Function

        ''' <summary>
        ''' ソート条件に合致するかチェックする
        ''' </summary>
        ''' <param name="Condition">条件</param>
        ''' <param name="record">値</param>
        ''' <param name="record2">値2</param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Private Function SortCheck(ByVal Condition As String, ByVal record As BuhinKoseiRecordVo, ByVal record2 As BuhinKoseiRecordVo) As Boolean
            Select Case Condition
                Case "レベル"
                    If record.Level = record2.Level Then
                        Return True
                    Else
                        Return False
                    End If
                Case "取引先コード"
                    If StringUtil.Equals(record.MakerCode, record2.MakerCode) Then
                        Return True
                    Else
                        Return False
                    End If
                Case "部品番号"
                    If StringUtil.Equals(record.BuhinNo, record2.BuhinNo) Then
                        Return True
                    Else
                        Return False
                    End If
                Case "部品名称"
                    If StringUtil.Equals(record.BuhinName, record2.BuhinName) Then
                        Return True
                    Else
                        Return False
                    End If
                Case "供給セクション"
                    If StringUtil.Equals(record.KyoukuSection, record2.KyoukuSection) Then
                        Return True
                    Else
                        Return False
                    End If
                Case "出図予定日"
                    If record.ShutuzuYoteiDate = record2.ShutuzuYoteiDate Then
                        Return True
                    Else
                        Return False
                    End If
                Case "材質規格1"
                    If StringUtil.Equals(record.ZaishituKikaku1, record2.ZaishituKikaku1) Then
                        Return True
                    Else
                        Return False
                    End If
                Case "材質規格2"
                    If StringUtil.Equals(record.ZaishituKikaku2, record2.ZaishituKikaku2) Then
                        Return True
                    Else
                        Return False
                    End If
                Case "材質規格3"
                    If StringUtil.Equals(record.ZaishituKikaku3, record2.ZaishituKikaku3) Then
                        Return True
                    Else
                        Return False
                    End If
                Case "材質メッキ"
                    If StringUtil.Equals(record.ZaishituMekki, record2.ZaishituMekki) Then
                        Return True
                    Else
                        Return False
                    End If
                Case "板厚"
                    If StringUtil.Equals(record.ShisakuBankoSuryo, record2.ShisakuBankoSuryo) Then
                        Return True
                    Else
                        Return False
                    End If
                Case "板厚u"
                    If StringUtil.Equals(record.ShisakuBankoSuryoU, record2.ShisakuBankoSuryoU) Then
                        Return True
                    Else
                        Return False
                    End If
                Case "試作部品費"
                    If record.ShisakuBuhinHi = record2.ShisakuBuhinHi Then
                        Return True
                    Else
                        Return False
                    End If
                Case "試作型費"
                    If record.ShisakuKataHi = record2.ShisakuKataHi Then
                        Return True
                    Else
                        Return False
                    End If
            End Select
            Return False
        End Function

        ''' <summary>
        ''' 前のソートを崩さないようにソートする
        ''' </summary>
        ''' <param name="conditions">ソート条件</param>
        ''' <param name="conditionsList">ソートリスト</param>
        ''' <remarks></remarks>
        Private Sub SortMatrix23(ByVal conditions As String, ByVal pConditions As String, ByVal conditionsList As List(Of String), ByVal level0Count As Integer, Optional ByVal pConditions2 As String = "")

            For Each rowindex As Integer In Me._koseiMatrix.GetInputRowIndexes
                If Me._koseiMatrix(rowindex).Level > 0 Then
                    Me._koseiMatrix.Record(rowindex).SortFlag = 0
                Else
                    Me._koseiMatrix.Record(rowindex).SortFlag = 1
                End If
            Next

            For Each cond As String In conditionsList
                For Each rowindex As Integer In Me._koseiMatrix.GetInputRowIndexes
                    'レベル０は対象外'
                    If Not Me._koseiMatrix(rowindex).Level = 0 Then
                        'ソートが完了していないこと'
                        If Me._koseiMatrix(rowindex).SortFlag = 0 Then
                            'ソート対象かチェックする'
                            If SortCheck(conditions, cond, Me._koseiMatrix.Record(rowindex)) Then
                                'レベル０の直下か、直上が同一の値なら移動する必要は無い'
                                If Me._koseiMatrix(rowindex - 1).Level = 0 OrElse SortCheck(conditions, Me._koseiMatrix(rowindex), Me._koseiMatrix(rowindex - 1)) Then
                                    Me._koseiMatrix.Record(rowindex).SortFlag = 1
                                    Continue For
                                End If
                                Dim index As Integer = level0Count

                                '行が同じなら移動する必要は無い'
                                If SortCheck(conditions, conditionsList(rowindex - index + 1), Me._koseiMatrix(rowindex)) Then
                                    Me._koseiMatrix.Record(rowindex).SortFlag = 1
                                    Continue For
                                End If

                                '移動先の検索'
                                For Each rowindex2 As Integer In Me._koseiMatrix.GetInputRowIndexes
                                    'レベル0でもソート済みでもないものの中から移動先を探す'
                                    If Me._koseiMatrix(rowindex2).Level <> 0 Then
                                        If Me._koseiMatrix(rowindex2).SortFlag = 0 Then
                                            '優先順位の高いソートキーで条件が同一のものを探す。ただし、行は異なること
                                            If SortCheck(pConditions, Me._koseiMatrix(rowindex2), Me._koseiMatrix(rowindex)) AndAlso rowindex <> rowindex2 Then
                                                '対象の値が異なるものを探す'
                                                If StringUtil.IsEmpty(pConditions2) Then
                                                    If Not SortCheck(conditions, Me._koseiMatrix(rowindex2), Me._koseiMatrix(rowindex)) Then
                                                        index = rowindex2
                                                        Exit For
                                                    End If
                                                Else
                                                    If SortCheck(pConditions2, Me._koseiMatrix(rowindex2), Me._koseiMatrix(rowindex)) AndAlso rowindex <> rowindex2 Then
                                                        If Not SortCheck(conditions, Me._koseiMatrix(rowindex2), Me._koseiMatrix(rowindex)) Then
                                                            index = rowindex2
                                                            Exit For
                                                        End If
                                                    End If
                                                End If
                                            End If

                                        End If
                                    End If
                                Next

                                '移動先がソート済みならソートしない'
                                If Me._koseiMatrix(index).SortFlag = 1 Then
                                    Continue For
                                End If

                                '移動用レコード'
                                Dim buhinVo As New BuhinKoseiRecordVo
                                '移動先のレコード取得'
                                buhinVo = Me._koseiMatrix.Record(index)
                                '移動する'
                                Me._koseiMatrix.Record(index) = Me._koseiMatrix.Record(rowindex)
                                '移動先と移動元を入れ替える'
                                Me._koseiMatrix.Record(rowindex) = buhinVo
                                For Each colindex As Integer In Me._koseiMatrix.GetInputInsuColumnIndexes

                                    Dim insuVo As New BuhinKoseiInsuCellVo
                                    insuVo = Me._koseiMatrix.InsuVo(index, colindex)
                                    Me._koseiMatrix.InsuVo(index, colindex) = Me._koseiMatrix.InsuVo(rowindex, colindex)
                                    Me._koseiMatrix.InsuVo(rowindex, colindex) = insuVo
                                Next
                                'ソート完了を知らせる'
                                Me._koseiMatrix.Record(index).SortFlag = 1
                            End If

                        End If
                    End If
                Next
            Next

        End Sub

        ''' <summary>
        ''' 集計コードAに供給セクションを振る
        ''' </summary>
        ''' <remarks></remarks>
        Public Sub setKyouku()

            For Each rowindex As Integer In GetInputRowIndexes()
                If StringUtil.IsEmpty(Matrix.Record(rowindex).ShukeiCode) Then
                    If Not StringUtil.IsEmpty(Matrix.Record(rowindex).SiaShukeiCode) Then
                        If StringUtil.Equals(Matrix.Record(rowindex).SiaShukeiCode, "A") AndAlso StringUtil.IsEmpty(Matrix.Record(rowindex).KyoukuSection) Then
                            Matrix.Record(rowindex).KyoukuSection = "9SH10"
                        End If
                    End If
                Else
                    If StringUtil.Equals(Matrix.Record(rowindex).ShukeiCode, "A") AndAlso StringUtil.IsEmpty(Matrix.Record(rowindex).KyoukuSection) Then
                        Matrix.Record(rowindex).KyoukuSection = "9SH10"
                    End If
                End If
            Next

            SetChanged()
        End Sub

        Public Function copy() As BuhinKoseiMatrix
            Return _koseiMatrix.Copy
        End Function


        ''' <summary>
        ''' 部品番号が変更されたときに取引先コードと取引先名称と部品名称をとってくる
        ''' </summary>
        ''' <param name="rowIndex">行番号</param>
        ''' <remarks></remarks>
        Private Sub OnChangedBuhinNoGetMakerAndName(ByVal rowIndex As Integer)
            If IsSuspendOnChangedBuhinNo Then
                Return
            End If
            IsSuspendOnChangedBuhinNo = True
            Dim inputedBuhinNo As String = Record(rowIndex).BuhinNo

            If Not StringUtil.IsEmpty(inputedBuhinNo) Then
                Dim impl As Dao.MakeStructureResultDao = New Dao.MakeStructureResultDaoImpl
                Dim vo As New TShisakuBuhinEditVo
                vo = impl.FindByBuhinMaker(inputedBuhinNo)
                If Not vo Is Nothing Then
                    If Not StringUtil.IsEmpty(vo.BuhinName) Then
                        Record(rowIndex).BuhinName = vo.BuhinName
                    End If
                    If Not StringUtil.IsEmpty(vo.MakerCode) Then
                        Record(rowIndex).MakerCode = vo.MakerCode
                    End If
                    If Not StringUtil.IsEmpty(vo.MakerCode) Then
                        Record(rowIndex).MakerName = vo.MakerName
                    End If
                    NotifyObservers()
                End If
            End If
            IsSuspendOnChangedBuhinNo = False
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
            SetChanged()

            NotifyObservers(rowIndex)
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
            Dim columnList As List(Of Integer) = _koseiMatrix.GetInputInsuColumnIndexes

            For Each rowIndex As Integer In rowIndexesOnColumn
                '員数の入った行のみ変換'
                For Each col As Integer In columnList
                    If Record(rowIndex).Level = 0 Then
                        If Not StringUtil.IsEmpty(InsuSuryo(rowIndex, columnIndex)) Then
                            'オリジナル品番を表示する。　BY 柳沼
                            'Record(rowIndex).BuhinNo = IIf(aStructureResult.IsEBom, aStructureResult.BuhinNo, instlTitle.InstlHinban(columnIndex))
                            Record(rowIndex).BuhinNo = instlTitle.InstlHinban(columnIndex)
                            If Not aStructureResult Is Nothing Then
                                Record(rowIndex).BuhinNoKbn = IIf(aStructureResult.IsEBom, aStructureResult.BuhinNoKbn, instlTitle.InstlHinbanKbn(columnIndex))
                            Else
                                Record(rowIndex).BuhinNoKbn = instlTitle.InstlHinbanKbn(columnIndex)
                            End If




                            'INSTL品番が削除された場合、以下のセルの値をクリアする。  By柳沼
                            If StringUtil.IsEmpty(instlTitle.InstlHinban(columnIndex)) Then
                                Record(rowIndex).Level = 0
                                Record(rowIndex).SiaShukeiCode = Nothing
                                Record(rowIndex).ShukeiCode = Nothing
                                Record(rowIndex).GencyoCkdKbn = Nothing
                                Record(rowIndex).MakerCode = Nothing
                                Record(rowIndex).MakerName = Nothing
                                Record(rowIndex).KaiteiHandanFlg = Nothing
                                Record(rowIndex).EdaBan = Nothing
                                Record(rowIndex).BuhinName = Nothing
                                Record(rowIndex).Saishiyoufuka = Nothing
                                '2012/01/23 供給セクション追加
                                Record(rowIndex).KyoukuSection = Nothing
                                Record(rowIndex).ShutuzuYoteiDate = Nothing
                                Record(rowIndex).ZaishituKikaku1 = Nothing
                                Record(rowIndex).ZaishituKikaku2 = Nothing
                                Record(rowIndex).ZaishituKikaku3 = Nothing
                                Record(rowIndex).ZaishituMekki = Nothing
                                Record(rowIndex).ShisakuBankoSuryo = Nothing
                                Record(rowIndex).ShisakuBankoSuryoU = Nothing
                                Record(rowIndex).ShisakuBuhinHi = Nothing
                                Record(rowIndex).ShisakuKataHi = Nothing
                                Record(rowIndex).Bikou = Nothing
                                '2012/01/25 部品ノート追加
                                Record(rowIndex).BuhinNote = Nothing

                                '↓↓↓2014/12/26 メタル項目を追加 (DANIEL)柳沼 ADD BEGIN
                                Record(rowIndex).MaterialInfoLength = Nothing
                                Record(rowIndex).MaterialInfoWidth = Nothing
                                Record(rowIndex).DataItemKaiteiNo = Nothing
                                Record(rowIndex).DataItemAreaName = Nothing
                                Record(rowIndex).DataItemSetName = Nothing
                                Record(rowIndex).DataItemKaiteiInfo = Nothing
                                '↑↑↑2014/12/26 メタル項目を追加 (DANIEL)柳沼 ADD END

                            End If
                            notifyRowIndexes.Add(rowIndex)
                        End If
                    End If
                Next
            Next

            Dim wasInsertRow As Boolean = False
            If notifyRowIndexes.Count = 0 Then
                'レベル0の行の合計を出す'
                Dim insertRowIndex As Integer = GetRowIndexLastLevelZero() + 1

                '今回入力された列が既に入力されている列内に存在する場合'
                '行番号は若くなる'
                '入力列よりも手前の列のレベル0を数える'
                Dim incount As Integer = 0
                For Each col As Integer In GetInputInstlHinbanColumnIndexes()
                    If columnIndex < col Then
                        Continue For
                    End If
                    For Each row As Integer In GetInputRowIndexes()
                        If _koseiMatrix(row).Level = 0 Then
                            If Not _koseiMatrix.InsuVo(row, col) Is Nothing Then
                                If Not _koseiMatrix.InsuSuryo(row, col) Is Nothing Then
                                    incount = incount + 1
                                End If
                            End If
                        End If
                    Next
                Next

                incount = incount
                If Not insertRowIndex = incount Then
                    insertRowIndex = incount
                End If


                InsertRow(insertRowIndex, 1)
                wasInsertRow = True
                Record(insertRowIndex).Level = 0
                InsuSuryo(insertRowIndex, columnIndex) = 1
                Record(insertRowIndex).BuhinNo = IIf(aStructureResult.IsEBom, instlTitle.InstlHinban(columnIndex), aStructureResult.BuhinNo)
                Record(insertRowIndex).BuhinNoKbn = IIf(aStructureResult.IsEBom, instlTitle.InstlHinbanKbn(columnIndex), aStructureResult.BuhinNoKbn)
                'Record(insertRowIndex).BuhinNo = IIf(aStructureResult.IsEBom, aStructureResult.BuhinNo, instlTitle.InstlHinban(columnIndex))
                'Record(insertRowIndex).BuhinNoKbn = IIf(aStructureResult.IsEBom, aStructureResult.BuhinNoKbn, instlTitle.InstlHinbanKbn(columnIndex))
                notifyRowIndexes.Add(insertRowIndex)
            End If
            If Not aStructureResult Is Nothing Then
                If aStructureResult.IsEBom Then
                    '2012/01/16 引数追加
                    Dim koseiMatrix As BuhinKoseiMatrix = GetNewKoseiMatrix(aStructureResult, Jikyu)
                    '2012/02/28 取得方法を変える'
                    'RHACLIBF0532'
                    'RHACLIBF0533'
                    'RHACLIBF0530'

                    'RHACLIBF610'

                    '何もかからない'

                    If koseiMatrix Is Nothing Then
                        For Each rowIndex As Integer In notifyRowIndexes
                            With Record(rowIndex)
                                .BuhinNo = instlTitle.InstlHinban(columnIndex)
                                .ShukeiCode = Nothing
                                .SiaShukeiCode = Nothing
                                .GencyoCkdKbn = Nothing
                                .MakerCode = Nothing
                                .MakerName = Nothing
                                .BuhinNoKaiteiNo = Nothing
                                .EdaBan = Nothing
                                .BuhinName = Nothing
                            End With
                        Next
                    Else
                        If koseiMatrix.InputRowCount = 0 Then
                            'Throw New InvalidProgramException("EBomに存在するハズの品番で、結果がゼロ件になるはずはない")
                        End If
                        For Each rowIndex As Integer In notifyRowIndexes
                            With Record(rowIndex)
                                If Not StringUtil.IsEmpty(koseiMatrix(-1).BuhinNo) Then
                                    .BuhinNo = koseiMatrix(-1).BuhinNo
                                    .ShukeiCode = koseiMatrix(-1).ShukeiCode
                                    .SiaShukeiCode = koseiMatrix(-1).SiaShukeiCode
                                    .GencyoCkdKbn = koseiMatrix(-1).GencyoCkdKbn
                                    .MakerCode = koseiMatrix(-1).MakerCode
                                    .MakerName = koseiMatrix(-1).MakerName
                                    .BuhinNoKaiteiNo = koseiMatrix(-1).BuhinNoKaiteiNo
                                    .EdaBan = koseiMatrix(-1).EdaBan
                                    .BuhinName = koseiMatrix(-1).BuhinName
                                Else
                                    .BuhinNo = koseiMatrix(0).BuhinNo
                                    .ShukeiCode = koseiMatrix(0).ShukeiCode
                                    .SiaShukeiCode = koseiMatrix(0).SiaShukeiCode
                                    .GencyoCkdKbn = koseiMatrix(0).GencyoCkdKbn
                                    .MakerCode = koseiMatrix(0).MakerCode
                                    .MakerName = koseiMatrix(0).MakerName
                                    .BuhinNoKaiteiNo = koseiMatrix(0).BuhinNoKaiteiNo
                                    .EdaBan = koseiMatrix(0).EdaBan
                                    .BuhinName = koseiMatrix(0).BuhinName

                                End If
                            End With
                        Next
                    End If
                End If
            End If



            If Not HasChanged() Then
                Return
            End If


            If wasInsertRow Then
                NotifyObservers()   ' Insert行の下の行情報もすべて再表示
                Return
            End If
            NotifyObserversByRow(notifyRowIndexes.ToArray)
            SetChanged()
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
        ''' <param name="kaiteiNo">改定No　  2014/08/04 Ⅰ.11.改訂戻し機能 ｌ) (TES)施 追加 </param>
        ''' <returns>INSTL列情報</returns>
        ''' <remarks></remarks>
        Private Function CreateInstlColumnBag(ByVal aStructureResult As StructureResult, Optional ByVal KaiteiNo As String = "", Optional ByVal aInstlDataKbn As String = "", Optional ByVal aBaseInstlFlg As String = "", Optional ByVal eventCopyFlg As Boolean = False) As InstlColumnBag

            ''↓↓2014/08/04 Ⅰ.11.改訂戻し機能 ｌ) (TES)施 ADD BEGIN
            Dim koseiMatrix As BuhinKoseiMatrix

            If KaiteiNo = "" Then
                '↓↓2014/09/25 酒井 ADD BEGIN
                '                koseiMatrix = GetNewKoseiMatrix(aStructureResult, 0)
                If Not eventCopyFlg Then
                    koseiMatrix = GetNewKoseiMatrix(aStructureResult, 0)
                Else
                    koseiMatrix = GetNewKoseiMatrix(aStructureResult, 0, "", aInstlDataKbn, aBaseInstlFlg, eventCopyFlg)
                End If
                '↑↑2014/09/25 酒井 ADD END
            Else
                ''↓↓2014/09/23 酒井 ADD BEGIN
                'koseiMatrix = GetNewKoseiMatrix(aStructureResult, 0, KaiteiNo)
                koseiMatrix = GetNewKoseiMatrix(aStructureResult, 0, KaiteiNo, aInstlDataKbn, aBaseInstlFlg)
                ''↑↑2014/09/23 酒井 ADD END
            End If
            ''↑↑2014/08/04 Ⅰ.11.改訂戻し機能 ｌ) (TES)施 ADD END



            '樺澤 構成が無い'
            If koseiMatrix Is Nothing Then
                Return Nothing
            End If
            '樺澤 構成が無い'
            Dim wCheck As String = Nothing
            '2012/03/08 -1の値も存在しているのでカウントでは取れない'
            'For INDEX As Integer = 0 To koseiMatrix.Records.Count - 1
            '    If Not StringUtil.IsEmpty(koseiMatrix.Record(INDEX).BuhinNo) Then
            '        wCheck = "OK"
            '    End If
            'Next
            For Each INDEX As Integer In koseiMatrix.GetInputRowIndexes
                If Not StringUtil.IsEmpty(koseiMatrix.Record(INDEX).BuhinNo) Then
                    wCheck = "OK"
                End If
            Next

            If wCheck <> "OK" Then
                Return Nothing
            End If


            For Each rowIndex As Integer In koseiMatrix.GetInputRowIndexes()
                For Each columnIndex As Integer In koseiMatrix.GetInputInsuColumnIndexesOnRow(rowIndex)
                    Return koseiMatrix.InstlColumn(columnIndex)
                Next
            Next
            If 0 < koseiMatrix.GetInputRowIndexes.Count Then
                Throw New InvalidOperationException("員数が未入力で InstlColumnBag を作成出来ない")
            End If
            Return New InstlColumnBag ' EMPTY
        End Function

        ''' <summary>
        ''' 構成情報を元にした部品表のINSTL列情報を作成して返す(構成再展開用)
        ''' </summary>
        ''' <param name="aStructureResult">構成の情報</param>
        ''' <returns>INSTL列情報</returns>
        ''' <remarks></remarks>
        Private Function CreateInstlColumnBagKosei(ByVal aStructureResult As StructureResult) As InstlColumnBag
            '2012/01/16 引数追加
            Dim koseiMatrix As BuhinKoseiMatrix = GetNewKoseiMatrixKosei(aStructureResult)
            '樺澤 構成が無い'
            If koseiMatrix Is Nothing Then
                Return Nothing
            End If
            '樺澤 構成が無い'
            Dim wCheck As String = Nothing
            For INDEX As Integer = 0 To koseiMatrix.Records.Count - 1
                If Not StringUtil.IsEmpty(koseiMatrix.Record(INDEX).BuhinNo) Then
                    wCheck = "OK"
                End If
            Next
            If wCheck <> "OK" Then
                Return Nothing
            End If


            For Each rowIndex As Integer In koseiMatrix.GetInputRowIndexes()
                For Each columnIndex As Integer In koseiMatrix.GetInputInsuColumnIndexesOnRow(rowIndex)
                    Return koseiMatrix.InstlColumn(columnIndex)
                Next
            Next
            If 0 < koseiMatrix.GetInputRowIndexes.Count Then
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
        Public Function GetNewKoseiMatrix(ByVal aStructureResult As StructureResult) As BuhinKoseiMatrix
            'エラーチェック時に使ってるのみ'
            Return GetNewKoseiMatrix(aStructureResult, 0)
        End Function


        ''' <summary>
        ''' 「構成の情報」を元に部品表を作成する
        ''' </summary>
        ''' <param name="aStructureResult"></param>
        ''' <param name="baseLevel"></param>
        ''' <param name="kaiteiNo">改定No　  2014/08/04 Ⅰ.11.改訂戻し機能 i) (TES)施 追加 </param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Private Function GetNewKoseiMatrix(ByVal aStructureResult As StructureResult, ByVal baseLevel As Integer?, Optional ByVal KaiteiNo As String = "", Optional ByVal aInstlDataKbn As String = "", Optional ByVal aBaseInstlFlg As String = "", Optional ByVal eventCopyFlg As Boolean = False) As BuhinKoseiMatrix
            If KaiteiNo = "" Then
                If Not eventCopyFlg Then
                    Return make.Compute(aStructureResult, a0553flag, baseLevel)
                Else
                    Return make.Compute(aStructureResult, a0553flag, baseLevel, "", aInstlDataKbn, aBaseInstlFlg, eventCopyFlg)
                End If
            Else
                Return make.Compute(aStructureResult, a0553flag, baseLevel, KaiteiNo, aInstlDataKbn, aBaseInstlFlg)
            End If
        End Function

        ''' <summary>
        ''' 「構成の情報」を元に部品表を作成する
        ''' </summary>
        ''' <param name="aStructureResult"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Private Function GetNewKoseiMatrixKosei(ByVal aStructureResult As StructureResult) As BuhinKoseiMatrix
            Return make.GetBuhinKosei(aStructureResult, a0553flag)
        End Function


        ''' <summary>
        ''' INSTLの部品情報だけ取得する
        ''' </summary>
        ''' <param name="aStructureResult"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Private Function GetNewBuhinKosei(ByVal aStructureResult As StructureResult) As BuhinKoseiMatrix
            Return make.GetBuhinKosei(aStructureResult, a0553flag)
        End Function

        ''' <summary>
        ''' 行を挿入する
        ''' </summary>
        ''' <param name="rowIndex">行index</param>
        ''' <param name="count">挿入行数</param>
        ''' <remarks></remarks>
        Public Sub InsertRow(ByVal rowIndex As Integer, ByVal count As Integer, Optional ByVal level0 As Boolean = False)
            '↓↓2014/10/29 酒井 ADD BEGIN
            'Ver6_2 1.95以降の修正内容の展開
            'Public Sub InsertRow(ByVal rowIndex As Integer, ByVal count As Integer)
            '↑↑2014/10/29 酒井 ADD END
            ' 上の行のレベル
            Dim baseLevel As Integer?
            Dim wRowIndex As Integer = rowIndex

            If 0 < wRowIndex Then
                If SpdKoseiObserver.SPREAD_JIKYU = "N" Then
                    Do While wRowIndex > 0
                        wRowIndex = wRowIndex - 1
                        '上の行のレベルが自給品だったらループ
                        If Me._koseiMatrix.Record(wRowIndex).ShukeiCode = "J" And _
                            Me._koseiMatrix.Record(wRowIndex).SiaShukeiCode = "J" Or _
                            Me._koseiMatrix.Record(wRowIndex).ShukeiCode = "J" And _
                            Me._koseiMatrix.Record(wRowIndex).SiaShukeiCode <> "J" Or _
                            Me._koseiMatrix.Record(wRowIndex).ShukeiCode = " " And _
                            Me._koseiMatrix.Record(wRowIndex).SiaShukeiCode = "J" Then
                        Else
                            Exit Do
                        End If
                    Loop
                Else
                    wRowIndex = wRowIndex - 1
                End If

                If wRowIndex < 0 Then
                    '↓↓2014/10/29 酒井 ADD BEGIN
                    'Ver6_2 1.95以降の修正内容の展開
                    If level0 Then
                        Exit Sub
                    End If
                    '↑↑2014/10/29 酒井 ADD END
                    'レベルが無いなら0にする'
                    baseLevel = 0
                Else
                    '↓↓2014/10/29 酒井 ADD BEGIN
                    'Ver6_2 1.95以降の修正内容の展開
                    If level0 AndAlso Me._koseiMatrix.Record(wRowIndex).Level.Equals(0) Then
                        ComFunc.ShowErrMsgBox("レベルに0を設定することは出来ません。")
                        Exit Sub
                    End If
                    '↑↑2014/10/29 酒井 ADD END
                    baseLevel = Me._koseiMatrix.Record(wRowIndex).Level
                End If
            Else
                '↓↓2014/10/29 酒井 ADD BEGIN
                'Ver6_2 1.95以降の修正内容の展開
                If level0 Then
                    Exit Sub
                End If
                '↑↑2014/10/29 酒井 ADD END
                'レベルが無いなら0にする'
                baseLevel = 0
            End If

            For i As Integer = 0 To count - 1
                Me._koseiMatrix.InsertRow(rowIndex)
                Me._koseiMatrix.Record(rowIndex).Level = baseLevel
                'INSTL品番が入ったときに'
                'Me._koseiMatrix.InsuSuryo(rowIndex, rowIndex + i) = 1
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
                        If Me._koseiMatrix.Record(wRowIndex).ShukeiCode = "J" And _
                            Me._koseiMatrix.Record(wRowIndex).SiaShukeiCode = "J" Or _
                            Me._koseiMatrix.Record(wRowIndex).ShukeiCode = "J" And _
                            Me._koseiMatrix.Record(wRowIndex).SiaShukeiCode <> "J" Or _
                            Me._koseiMatrix.Record(wRowIndex).ShukeiCode = " " And _
                            Me._koseiMatrix.Record(wRowIndex).SiaShukeiCode = "J" Then
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
                    baseLevel = Me._koseiMatrix.Record(wRowIndex).Level + 1
                End If
            Else
                'レベルが無いなら0にする'
                baseLevel = 0
            End If

            For i As Integer = 0 To count - 1
                Me._koseiMatrix.InsertRow(rowIndex)
                Me._koseiMatrix.Record(rowIndex).Level = baseLevel
                'INSTL品番が入ったときに'
                'Me._koseiMatrix.InsuSuryo(rowIndex, rowIndex) = 1
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
                baseLevel = Me._koseiMatrix.Record(rowIndex - 1).Level

            Else
                'レベルが無いなら0にする'
                baseLevel = 0
            End If

            Dim level0Count As Integer = 0
            For Each index As Integer In _koseiMatrix.GetInputRowIndexes
                If Me._koseiMatrix.Record(index).Level = 0 Then
                    'レベル0の行数をカウント'
                    level0Count = level0Count + 1
                End If
            Next

            For i As Integer = 0 To count - 1
                '列と行が同じとは限らないから'
                'レベル0の行数と列番号を比較'
                If level0Count >= rowIndex Then
                    Me._koseiMatrix.InsertRow(rowIndex + i)
                    Me._koseiMatrix.Record(rowIndex + i).Level = baseLevel
                    Me._koseiMatrix.InsuSuryo(rowIndex + i, rowIndex + i) = 1
                    'Me._koseiMatrix.Record(rowIndex + i).BuhinNoHyoujiJun = rowIndex + i
                    'Me._koseiMatrix.InsuVo(rowIndex + i, rowIndex + i).BuhinNoHyoujiJun = rowIndex + i
                    'Me._koseiMatrix.InsuVo(rowIndex + i, rowIndex + i).InstlHinbanHyoujiJun = rowIndex + i

                Else
                    'レベル0の行数よりも大きければ後ろにつける'
                    Me._koseiMatrix.InsertRow(level0Count + i)
                    Me._koseiMatrix.Record(level0Count + i).Level = baseLevel
                    Me._koseiMatrix.InsuSuryo(level0Count + i, rowIndex + i) = 1
                    'Me._koseiMatrix.Record(level0Count + i).BuhinNoHyoujiJun = level0Count + i
                    'Me._koseiMatrix.InsuVo(level0Count + i, rowIndex + i).BuhinNoHyoujiJun = level0Count + i
                    'Me._koseiMatrix.InsuVo(level0Count + i, rowIndex + i).InstlHinbanHyoujiJun = level0Count + i

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
                If Me._koseiMatrix.Record(rowIndex).Level = 0 Then
                    Me._koseiMatrix.RemoveRow(rowIndex)
                End If
            Next
        End Sub

        ''' <summary>
        ''' 列削除されたINSTLと同一の行を削除する
        ''' </summary>
        ''' <remarks></remarks>
        Private Sub RemoveRowInstl()
            Dim f As Boolean = True
            For Each subjectRow As Integer In GetInputRowIndexes()
                If Level(subjectRow) = 0 Then
                    f = False
                    For Each columnIndex As Integer In GetInputInstlHinbanColumnIndexes()
                        If StringUtil.Equals(BuhinNo(subjectRow), InstlHinban(columnIndex)) Then
                            If StringUtil.Equals(BuhinNoKbn(subjectRow), InstlHinbanKbn(columnIndex)) Then
                                ''↓↓2014/09/02 Ⅰ.3.設計編集 ベース改修専用化 酒井 ADD BEGIN
                                If InsuSuryo(subjectRow, columnIndex) = "1" Then
                                    ''↑↑2014/09/02 Ⅰ.3.設計編集 ベース改修専用化 酒井 ADD END
                                    f = True
                                    Exit For
                                    ''↓↓2014/09/02 Ⅰ.3.設計編集 ベース改修専用化 酒井 ADD BEGIN
                                End If
                                ''↑↑2014/09/02 Ⅰ.3.設計編集 ベース改修専用化 酒井 ADD END
                            Else
                                If StringUtil.IsEmpty(BuhinNoKbn(subjectRow)) _
                                AndAlso StringUtil.IsEmpty(InstlHinbanKbn(columnIndex)) Then
                                    ''↓↓2014/09/02 Ⅰ.3.設計編集 ベース改修専用化 酒井 ADD BEGIN
                                    If InsuSuryo(subjectRow, columnIndex) = "1" Then
                                        ''↑↑2014/09/02 Ⅰ.3.設計編集 ベース改修専用化 酒井 ADD END
                                        f = True
                                        Exit For
                                        ''↓↓2014/09/02 Ⅰ.3.設計編集 ベース改修専用化 酒井 ADD BEGIN
                                    End If
                                    ''↑↑2014/09/02 Ⅰ.3.設計編集 ベース改修専用化 酒井 ADD END
                                End If
                            End If
                        End If
                    Next
                    If Not f Then
                        RemoveRow(subjectRow, 1)
                        'この時点で並び順が変わるので'
                        Exit For
                    End If
                End If
            Next

            If Not f Then
                RemoveRowInstl()
            End If

        End Sub

        ''' <summary>
        ''' 行の内容を消す
        ''' </summary>
        ''' <param name="rowIndex"></param>
        ''' <param name="count"></param>
        ''' <remarks></remarks>
        Public Sub ClearRow(ByVal rowIndex As Integer, ByVal count As Integer)
            For i As Integer = 0 To count - 1
                'Me._koseiMatrix.ClearRow(rowIndex)
            Next
        End Sub

        ''' <summary>
        ''' セルの内容を空にする
        ''' </summary>
        ''' <param name="rowindex"></param>
        ''' <param name="colIndex"></param>
        ''' <remarks></remarks>
        Public Sub ClearCell(ByVal rowindex As Integer, ByVal colIndex As Integer)
            Select Case colIndex
                Case 0
                    'レベル'
                Case 1
                    '国内集計コード'
                Case 2
                    '海外集計コード'
                Case 3
                    '現調区分'
                Case 4
                    '取引先コード'
                Case 5
                    '取引先名称'
                Case 6
                    '部品番号'
                Case 7
                    '試作区分'
                Case 8
                    '改訂'
                Case 9
                    '枝番
                Case 10
                    '部品名称'
            End Select
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
        ''' 部品構成呼出（一括設定）用のSubjectを生成する
        ''' </summary>
        ''' <returns>新しいインスタンス</returns>
        ''' <remarks></remarks>
        Public Function NewSourceSelectorSubject(Optional ByVal selection As Dictionary(Of String, Integer) = Nothing) As SourceSelectorSubject
            Return New SourceSelectorSubject(blockVo, instlTitle, selection)
            'Return New SourceSelectorSubject()
        End Function


        ''' <summary>
        ''' INSTL品番列に列挿入する
        ''' </summary>
        ''' <param name="columnIndex">INSTL品番の中の列index</param>
        ''' <param name="insertCount">挿入列数</param>
        ''' <remarks></remarks>
        Public Sub InsertColumnInInstl(ByVal columnIndex As Integer, ByVal insertCount As Integer)
            Dim columnList As New List(Of Integer)
            Dim columncount As Integer = 0
            '直接いじるとエラーになるので列数と列番号を取得'
            For Each iCounter As Integer In _koseiMatrix.GetInputInsuColumnIndexes
                'このままだといじれないから'
                columnList.Insert(columncount, iCounter)
                columncount = columncount + 1
            Next

            '構成に空列を追加'
            _koseiMatrix.InsertColumn(columnIndex, insertCount)
            ''元々の構成をInsertCount分動かす'

            For index As Integer = columnList.Count - 1 To 0 Step -1
                '一番後ろが消えるのは-1してるから？
                'For index As Integer = columnList.Count To 0 Step -1

                If index < columnIndex Then
                    Continue For
                End If

                '空列に元の構成を追加する'
                _koseiMatrix.InstlColumnAdd(columnList(index) + insertCount, _koseiMatrix.InstlColumn(columnList(index)))

                'INSTL列上の行情報を削除(削除というよりは新規にする)'
                _koseiMatrix.InstlColumnAdd(columnList(index), New InstlColumnBag)
                '_koseiMatrix.InstlColumnRemove(columnList(index))
            Next
            instlTitle.Insert(columnIndex, insertCount)


            '_koseiMatrix.InsertColumn(columnIndex, insertCount)

            ''挿入元の構成は不必要なので消す() '
            'For i As Integer = 0 To insertCount - 1
            '    If Not _koseiMatrix.InstlColumn(columnIndex + i).Count = 0 Then
            '        For index As Integer = 0 To _koseiMatrix.InstlColumn(columnIndex + i).Count
            '            'これだと構成全部が消える・・・・'
            '            '_koseiMatrix.InstlColumn(columnIndex + i).Remove(_koseiMatrix.Record(index))
            '        Next
            '    End If
            'Next

            SetChanged()
        End Sub

        ''' <summary>
        ''' ブロックNoを差し替える
        ''' </summary>
        ''' <param name="blockVo">ブロックVo</param>
        ''' <remarks></remarks>
        Public Sub SupersedeBlockVo(ByVal blockVo As TShisakuSekkeiBlockVo)
            Me.blockVo = blockVo

            'Subjectの余分な情報が残っているので削除しておく'
            For Each index As Integer In GetInputInstlHinbanColumnIndexes()
                RemoveColumnInInstl(index, 1)
            Next


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

            SetChanged()
        End Sub

        ''' <summary>
        ''' INSTL品番列を列削除する
        ''' </summary>
        ''' <param name="columnIndex">INSTL品番の中の列index</param>
        ''' <param name="removeCount">削除列数</param>
        ''' <remarks></remarks>
        Public Sub RemoveColumnInInstl(ByVal columnIndex As Integer, ByVal removeCount As Integer)
            'INSTL品番列が削除されたとき、削除されたINSTL品番列より右側の構成は削除された列数分だけ左にずらさなければならない'
            'INSTL列タイトル部を削除'
            instlTitle.Remove(columnIndex, removeCount)

            '構成からINSTL列上の構成を削除'
            '実際に削除されるべきなのは一番右の列から削除カウント分'

            'リスト作成'
            Dim InstlList As New List(Of Integer)
            Dim instlCount As Integer = 0
            For Each index As Integer In _koseiMatrix.GetInputInsuColumnIndexes
                InstlList.Insert(instlCount, index)
                instlCount = instlCount + 1
            Next
            InstlList.Sort()

            instlCount = instlCount - 1
            'INSTLColumBagをずらす'
            For i As Integer = 0 To InstlList.Count - 1
                '削除対象列以前は何もしない'
                If InstlList(i) <= columnIndex Then
                    Continue For
                End If
                '削除後、前の構成の情報がまだ残っている'

                '2011/06/07 一番左の行で行挿入削除した場合エラーになるのを回避。By柳沼
                Dim abc As Integer = i - 1
                If abc >= 0 Then
                    _koseiMatrix.InstlColumnAdd(InstlList(i - 1), New InstlColumnBag())
                    _koseiMatrix.InstlColumnAdd(InstlList(i) - 1, _koseiMatrix.InstlColumn(InstlList(i)))
                End If
            Next

            If removeCount = 1 Then
                '一番後ろの列のみ削除'
                '_koseiMatrix.RemoveColumn(InstlList(instlCount), removeCount)
                _koseiMatrix.RemoveColumn(columnIndex, removeCount)
            Else
                '複数の場合'
                _koseiMatrix.RemoveColumn(columnIndex, removeCount)
            End If
            '2014/05/14 kabasawa 削除時に消す'
            RemoveRowInstl()

            SetChanged()
        End Sub

        ''' <summary>
        ''' 3Dビュー呼出し
        ''' </summary>
        ''' <param name="frm"></param>
        ''' <remarks></remarks>
        Public Sub callXVL(ByVal frm As Frm41DispShisakuBuhinEdit20, ByVal range As FarPoint.Win.Spread.Model.CellRange)
            Dim frmXVL As New PreViewParent(shisakuEventCode, blockVo, frm, range)
            frmXVL.ShowDialog(frm)
        End Sub

        Protected Overrides Sub Finalize()
            MyBase.Finalize()
        End Sub

        ''↓↓2014/08/01 Ⅰ.2.管理項目追加_bl) (TES)張 ADD BEGIN
        Public Function GetLabelValues_TsukurikataSeisaku() As List(Of LabelValueVo)
            Return makeShisakuBlockDao.FindTsukurikataSeisakuLabelValues
        End Function
        ''↑↑2014/08/01 Ⅰ.2.管理項目追加_bl) (TES)張 ADD END

        ''↓↓2014/08/01 Ⅰ.2.管理項目追加_br) (TES)張 ADD BEGIN
        Public Function GetLabelValues_TsukurikataKatashiyou1() As List(Of LabelValueVo)
            Return makeShisakuBlockDao.FindTsukurikataKatashiyou1LabelValues
        End Function

        Public Function GetLabelValues_TsukurikataKatashiyou2() As List(Of LabelValueVo)
            Return makeShisakuBlockDao.FindTsukurikataKatashiyou2LabelValues
        End Function

        Public Function GetLabelValues_TsukurikataKatashiyou3() As List(Of LabelValueVo)
            Return makeShisakuBlockDao.FindTsukurikataKatashiyou3LabelValues
        End Function
        ''↑↑2014/08/01 Ⅰ.2.管理項目追加_br) (TES)張 ADD END

        ''↓↓2014/08/01 Ⅰ.2.管理項目追加_bu) (TES)張 ADD BEGIN
        Public Function GetLabelValues_TsukurikataTigu() As List(Of LabelValueVo)
            Return makeShisakuBlockDao.FindTsukurikataTiguLabelValues
        End Function
        ''↑↑2014/08/01 Ⅰ.2.管理項目追加_bu) (TES)張 ADD END
        ''↓↓2014/08/12 Ⅰ.3.設計編集 ベース改修専用化_cf) (TES)張 ADD BEGIN
        Public Function GetFinalBuhinNo(ByVal BuhinNo As String) As Rhac2210Vo
            Dim sql As New StringBuilder()
            With sql
                .Remove(0, .Length)
                .AppendLine("SELECT HAISI_DATE ")
                .AppendLine("FROM " & RHACLIBF_DB_NAME & ".dbo.RHAC2210 WITH (NOLOCK, NOWAIT, INDEX(PK_RHAC2210)) ")
                .AppendLine("WHERE ")
                .AppendLine("F_BUHIN_NO = @FBuhinNo AND HAISI_DATE = 99999999")
            End With
            Dim db As New EBomDbClient
            Dim r2210Vo As New Rhac2210Vo
            r2210Vo.FBuhinNo = BuhinNo

            Return db.QueryForObject(Of Rhac2210Vo)(sql.ToString(), r2210Vo)
        End Function
        ''↑↑2014/08/12 Ⅰ.3.設計編集 ベース改修専用化_cf) (TES)張 ADD END

        ''↓↓2014/08/29 Ⅰ.3.設計編集 ベース改修専用化 酒井 ADD BEGIN
        Public Sub AddKoseiRow(ByVal src As BuhinKoseiRecordVo, ByVal instlColumn As Integer, ByVal Insu As Integer, ByVal index As Integer)
            ''↓↓2014/09/09 Ⅰ.3.設計編集 ベース改修専用化 酒井 ADD BEGIN
            'Public Sub AddKoseiRow(ByVal src As BuhinKoseiRecordVo, ByVal instlColumn As Integer, ByVal Insu As Integer)
            'Dim index As Integer = _koseiMatrix.Records.Count
            If index < _koseiMatrix.Records.Count Then
                InsertRow(index, 1)
            End If
            ''↑↑2014/09/09 Ⅰ.3.設計編集 ベース改修専用化 酒井 ADD END
            Me.InsuSuryo(index, instlColumn) = Insu
            VoUtil.CopyProperties(src, _koseiMatrix.Record(index))
        End Sub
        ''↑↑2014/08/29 Ⅰ.3.設計編集 ベース改修専用化 酒井 ADD END
        '↓↓2014/10/06 酒井 ADD BEGIN
        Public Sub CallSetChanged()
            SetChanged()
        End Sub
        '↑↑2014/10/06 酒井 ADD END
    End Class
End Namespace