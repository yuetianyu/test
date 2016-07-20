Imports EventSakusei.ShisakuBuhinEdit.Logic.Detect
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
Imports ShisakuCommon.Db.EBom
Imports System.Text
Imports ShisakuCommon.Util.LabelValue

Namespace ShisakuBuhinEdit.Kosei.Logic
    ''' <summary>
    ''' 部品構成編集画面の表示全般を担うクラス
    ''' </summary>
    ''' <remarks></remarks>
    Public Class HoyouBuhinBuhinEditKoseiSubject : Inherits Observable

        Private _koseiMatrix As HoyouBuhinBuhinKoseiMatrix

        Private tantoVo As THoyouSekkeiTantoVo
        Private ReadOnly login As LoginInfo
        Private ReadOnly aShisakuDate As ShisakuDate
        Private ReadOnly detector As HoyouBuhinDetectLatestStructure

        Private ReadOnly editDao As THoyouBuhinEditDao
        Private ReadOnly editInstlDao As TShisakuBuhinEditInstlDao

        Private ReadOnly make As HoyouBuhinMakeStructureResult
        Private ReadOnly aMakerNameResolver As MakerNameResolver

        Private isWaitingKoseiTenkai As Boolean
        Private hoyouEventCode As String
        Private Jikyu As String

        '設計展開時：0, 構成再展開、最新化、部品構成呼び出し時：1, 子部品展開時：2'
        Private a0553flag As Integer

        Private makeShisakuBlockDao As MakeShisakuBlockDao = New MakeShisakuBlockDaoImpl


#Region "Recordの各プロパティ"
        Public ReadOnly Property Matrix() As HoyouBuhinBuhinKoseiMatrix
            Get
                Return _koseiMatrix
            End Get
        End Property
        Private ReadOnly Property Record(ByVal rowIndex As Integer) As HoyouBuhinBuhinKoseiRecordVo
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

        ''' <summary>員数</summary>
        ''' <value>員数</value>
        ''' <returns>員数</returns>
        Public Property InsuSuryo(ByVal rowIndex As Integer) As String
            Get
                Return Record(rowIndex).InsuSuryo
            End Get
            Set(ByVal value As String)
                If EzUtil.IsEqualIfNull(Record(rowIndex).InsuSuryo, value) Then
                    Return
                End If
                Record(rowIndex).InsuSuryo = value
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

        ''' <summary>
        ''' メモ１
        ''' </summary>
        ''' <param name="rowIndex"></param>
        ''' <value>メモ１</value>
        ''' <returns>メモ１</returns>
        ''' <remarks></remarks>
        Public Property Memo1(ByVal rowIndex As Integer) As String
            Get
                Return Record(rowIndex).Memo1
            End Get
            Set(ByVal value As String)
                If EzUtil.IsEqualIfNull(Record(rowIndex).Memo1, value) Then
                    Return
                End If
                Record(rowIndex).Memo1 = value
                SetChanged()
            End Set
        End Property

        ''' <summary>
        ''' メモ２
        ''' </summary>
        ''' <param name="rowIndex"></param>
        ''' <value>メモ２</value>
        ''' <returns>メモ２</returns>
        ''' <remarks></remarks>
        Public Property Memo2(ByVal rowIndex As Integer) As String
            Get
                Return Record(rowIndex).Memo2
            End Get
            Set(ByVal value As String)
                If EzUtil.IsEqualIfNull(Record(rowIndex).Memo2, value) Then
                    Return
                End If
                Record(rowIndex).Memo2 = value
                SetChanged()
            End Set
        End Property

        ''' <summary>
        ''' メモ３
        ''' </summary>
        ''' <param name="rowIndex"></param>
        ''' <value>メモ３</value>
        ''' <returns>メモ３</returns>
        ''' <remarks></remarks>
        Public Property Memo3(ByVal rowIndex As Integer) As String
            Get
                Return Record(rowIndex).Memo3
            End Get
            Set(ByVal value As String)
                If EzUtil.IsEqualIfNull(Record(rowIndex).Memo3, value) Then
                    Return
                End If
                Record(rowIndex).Memo3 = value
                SetChanged()
            End Set
        End Property

        ''' <summary>
        ''' メモ４
        ''' </summary>
        ''' <param name="rowIndex"></param>
        ''' <value>メモ４</value>
        ''' <returns>メモ４</returns>
        ''' <remarks></remarks>
        Public Property Memo4(ByVal rowIndex As Integer) As String
            Get
                Return Record(rowIndex).Memo4
            End Get
            Set(ByVal value As String)
                If EzUtil.IsEqualIfNull(Record(rowIndex).Memo4, value) Then
                    Return
                End If
                Record(rowIndex).Memo4 = value
                SetChanged()
            End Set
        End Property

        ''' <summary>
        ''' メモ５
        ''' </summary>
        ''' <param name="rowIndex"></param>
        ''' <value>メモ５</value>
        ''' <returns>メモ５</returns>
        ''' <remarks></remarks>
        Public Property Memo5(ByVal rowIndex As Integer) As String
            Get
                Return Record(rowIndex).Memo5
            End Get
            Set(ByVal value As String)
                If EzUtil.IsEqualIfNull(Record(rowIndex).Memo5, value) Then
                    Return
                End If
                Record(rowIndex).Memo5 = value
                SetChanged()
            End Set
        End Property

        ''' <summary>
        ''' メモ６
        ''' </summary>
        ''' <param name="rowIndex"></param>
        ''' <value>メモ６</value>
        ''' <returns>メモ６</returns>
        ''' <remarks></remarks>
        Public Property Memo6(ByVal rowIndex As Integer) As String
            Get
                Return Record(rowIndex).Memo6
            End Get
            Set(ByVal value As String)
                If EzUtil.IsEqualIfNull(Record(rowIndex).Memo6, value) Then
                    Return
                End If
                Record(rowIndex).Memo6 = value
                SetChanged()
            End Set
        End Property

        ''' <summary>
        ''' メモ７
        ''' </summary>
        ''' <param name="rowIndex"></param>
        ''' <value>メモ７</value>
        ''' <returns>メモ７</returns>
        ''' <remarks></remarks>
        Public Property Memo7(ByVal rowIndex As Integer) As String
            Get
                Return Record(rowIndex).Memo7
            End Get
            Set(ByVal value As String)
                If EzUtil.IsEqualIfNull(Record(rowIndex).Memo7, value) Then
                    Return
                End If
                Record(rowIndex).Memo7 = value
                SetChanged()
            End Set
        End Property

        ''' <summary>
        ''' メモ８
        ''' </summary>
        ''' <param name="rowIndex"></param>
        ''' <value>メモ８</value>
        ''' <returns>メモ８</returns>
        ''' <remarks></remarks>
        Public Property Memo8(ByVal rowIndex As Integer) As String
            Get
                Return Record(rowIndex).Memo8
            End Get
            Set(ByVal value As String)
                If EzUtil.IsEqualIfNull(Record(rowIndex).Memo8, value) Then
                    Return
                End If
                Record(rowIndex).Memo8 = value
                SetChanged()
            End Set
        End Property

        ''' <summary>
        ''' メモ９
        ''' </summary>
        ''' <param name="rowIndex"></param>
        ''' <value>メモ９</value>
        ''' <returns>メモ９</returns>
        ''' <remarks></remarks>
        Public Property Memo9(ByVal rowIndex As Integer) As String
            Get
                Return Record(rowIndex).Memo9
            End Get
            Set(ByVal value As String)
                If EzUtil.IsEqualIfNull(Record(rowIndex).Memo9, value) Then
                    Return
                End If
                Record(rowIndex).Memo9 = value
                SetChanged()
            End Set
        End Property

        ''' <summary>
        ''' メモ１０
        ''' </summary>
        ''' <param name="rowIndex"></param>
        ''' <value>メモ１０</value>
        ''' <returns>メモ１０</returns>
        ''' <remarks></remarks>
        Public Property Memo10(ByVal rowIndex As Integer) As String
            Get
                Return Record(rowIndex).Memo10
            End Get
            Set(ByVal value As String)
                If EzUtil.IsEqualIfNull(Record(rowIndex).Memo10, value) Then
                    Return
                End If
                Record(rowIndex).Memo10 = value
                SetChanged()
            End Set
        End Property

        ''' <summary>メモ１タイトル</summary>
        ''' <param name="rowIndex"></param>
        ''' <value>メモ１タイトル</value>
        ''' <returns>メモ１タイトル</returns>
        ''' <remarks></remarks>
        Public Property Memo1T(ByVal rowIndex As Integer) As String
            Get
                Return Record(rowIndex).Memo1T
            End Get
            Set(ByVal value As String)
                If EzUtil.IsEqualIfNull(Record(rowIndex).Memo1T, value) Then
                    Return
                End If
                Record(rowIndex).Memo1T = value
                SetChanged()
            End Set
        End Property

        ''' <summary>メモ２タイトル</summary>
        ''' <param name="rowIndex"></param>
        ''' <value>メモ２タイトル</value>
        ''' <returns>メモ２タイトル</returns>
        ''' <remarks></remarks>
        Public Property Memo2T(ByVal rowIndex As Integer) As String
            Get
                Return Record(rowIndex).Memo2T
            End Get
            Set(ByVal value As String)
                If EzUtil.IsEqualIfNull(Record(rowIndex).Memo2T, value) Then
                    Return
                End If
                Record(rowIndex).Memo2T = value
                SetChanged()
            End Set
        End Property

        ''' <summary>メモ３タイトル</summary>
        ''' <param name="rowIndex"></param>
        ''' <value>メモ３タイトル</value>
        ''' <returns>メモ３タイトル</returns>
        ''' <remarks></remarks>
        Public Property Memo3T(ByVal rowIndex As Integer) As String
            Get
                Return Record(rowIndex).Memo3T
            End Get
            Set(ByVal value As String)
                If EzUtil.IsEqualIfNull(Record(rowIndex).Memo3T, value) Then
                    Return
                End If
                Record(rowIndex).Memo3T = value
                SetChanged()
            End Set
        End Property

        ''' <summary>メモ４タイトル</summary>
        ''' <param name="rowIndex"></param>
        ''' <value>メモ４タイトル</value>
        ''' <returns>メモ４タイトル</returns>
        ''' <remarks></remarks>
        Public Property Memo4T(ByVal rowIndex As Integer) As String
            Get
                Return Record(rowIndex).Memo4T
            End Get
            Set(ByVal value As String)
                If EzUtil.IsEqualIfNull(Record(rowIndex).Memo4T, value) Then
                    Return
                End If
                Record(rowIndex).Memo4T = value
                SetChanged()
            End Set
        End Property

        ''' <summary>メモ５タイトル</summary>
        ''' <param name="rowIndex"></param>
        ''' <value>メモ５タイトル</value>
        ''' <returns>メモ５タイトル</returns>
        ''' <remarks></remarks>
        Public Property Memo5T(ByVal rowIndex As Integer) As String
            Get
                Return Record(rowIndex).Memo5T
            End Get
            Set(ByVal value As String)
                If EzUtil.IsEqualIfNull(Record(rowIndex).Memo5T, value) Then
                    Return
                End If
                Record(rowIndex).Memo5T = value
                SetChanged()
            End Set
        End Property

        ''' <summary>メモ６タイトル</summary>
        ''' <param name="rowIndex"></param>
        ''' <value>メモ６タイトル</value>
        ''' <returns>メモ６タイトル</returns>
        ''' <remarks></remarks>
        Public Property Memo6T(ByVal rowIndex As Integer) As String
            Get
                Return Record(rowIndex).Memo6T
            End Get
            Set(ByVal value As String)
                If EzUtil.IsEqualIfNull(Record(rowIndex).Memo6T, value) Then
                    Return
                End If
                Record(rowIndex).Memo6T = value
                SetChanged()
            End Set
        End Property

        ''' <summary>メモ７タイトル</summary>
        ''' <param name="rowIndex"></param>
        ''' <value>メモ７タイトル</value>
        ''' <returns>メモ７タイトル</returns>
        ''' <remarks></remarks>
        Public Property Memo7T(ByVal rowIndex As Integer) As String
            Get
                Return Record(rowIndex).Memo7T
            End Get
            Set(ByVal value As String)
                If EzUtil.IsEqualIfNull(Record(rowIndex).Memo7T, value) Then
                    Return
                End If
                Record(rowIndex).Memo7T = value
                SetChanged()
            End Set
        End Property

        ''' <summary>メモ８タイトル</summary>
        ''' <param name="rowIndex"></param>
        ''' <value>メモ８タイトル</value>
        ''' <returns>メモ８タイトル</returns>
        ''' <remarks></remarks>
        Public Property Memo8T(ByVal rowIndex As Integer) As String
            Get
                Return Record(rowIndex).Memo8T
            End Get
            Set(ByVal value As String)
                If EzUtil.IsEqualIfNull(Record(rowIndex).Memo8T, value) Then
                    Return
                End If
                Record(rowIndex).Memo8T = value
                SetChanged()
            End Set
        End Property

        ''' <summary>メモ９タイトル</summary>
        ''' <param name="rowIndex"></param>
        ''' <value>メモ９タイトル</value>
        ''' <returns>メモ９タイトル</returns>
        ''' <remarks></remarks>
        Public Property Memo9T(ByVal rowIndex As Integer) As String
            Get
                Return Record(rowIndex).Memo9T
            End Get
            Set(ByVal value As String)
                If EzUtil.IsEqualIfNull(Record(rowIndex).Memo9T, value) Then
                    Return
                End If
                Record(rowIndex).Memo9T = value
                SetChanged()
            End Set
        End Property

        ''' <summary>メモ１０タイトル</summary>
        ''' <param name="rowIndex"></param>
        ''' <value>メモ１０タイトル</value>
        ''' <returns>メモ１０タイトル</returns>
        ''' <remarks></remarks>
        Public Property Memo10T(ByVal rowIndex As Integer) As String
            Get
                Return Record(rowIndex).Memo10T
            End Get
            Set(ByVal value As String)
                If EzUtil.IsEqualIfNull(Record(rowIndex).Memo10T, value) Then
                    Return
                End If
                Record(rowIndex).Memo10T = value
                SetChanged()
            End Set
        End Property


        ''' <summary>
        ''' メモ_シート系０１
        ''' </summary>
        ''' <param name="rowIndex"></param>
        ''' <value>メモ_シート系０１</value>
        ''' <returns>メモ_シート系０１</returns>
        ''' <remarks></remarks>
        Public Property MemoSheet1(ByVal rowIndex As Integer) As String
            Get
                Return Record(rowIndex).MemoSheet1
            End Get
            Set(ByVal value As String)
                If EzUtil.IsEqualIfNull(Record(rowIndex).MemoSheet1, value) Then
                    Return
                End If
                Record(rowIndex).MemoSheet1 = value
                SetChanged()
            End Set
        End Property
        ''' <summary>
        ''' メモ_シート系０２
        ''' </summary>
        ''' <param name="rowIndex"></param>
        ''' <value>メモ_シート系０２</value>
        ''' <returns>メモ_シート系０２</returns>
        ''' <remarks></remarks>
        Public Property MemoSheet2(ByVal rowIndex As Integer) As String
            Get
                Return Record(rowIndex).MemoSheet2
            End Get
            Set(ByVal value As String)
                If EzUtil.IsEqualIfNull(Record(rowIndex).MemoSheet2, value) Then
                    Return
                End If
                Record(rowIndex).MemoSheet2 = value
                SetChanged()
            End Set
        End Property
        ''' <summary>
        ''' メモ_シート系０３
        ''' </summary>
        ''' <param name="rowIndex"></param>
        ''' <value>メモ_シート系０３</value>
        ''' <returns>メモ_シート系０３</returns>
        ''' <remarks></remarks>
        Public Property MemoSheet3(ByVal rowIndex As Integer) As String
            Get
                Return Record(rowIndex).MemoSheet3
            End Get
            Set(ByVal value As String)
                If EzUtil.IsEqualIfNull(Record(rowIndex).MemoSheet3, value) Then
                    Return
                End If
                Record(rowIndex).MemoSheet3 = value
                SetChanged()
            End Set
        End Property
        ''' <summary>
        ''' メモ_シート系０４
        ''' </summary>
        ''' <param name="rowIndex"></param>
        ''' <value>メモ_シート系０４</value>
        ''' <returns>メモ_シート系０４</returns>
        ''' <remarks></remarks>
        Public Property MemoSheet4(ByVal rowIndex As Integer) As String
            Get
                Return Record(rowIndex).MemoSheet4
            End Get
            Set(ByVal value As String)
                If EzUtil.IsEqualIfNull(Record(rowIndex).MemoSheet4, value) Then
                    Return
                End If
                Record(rowIndex).MemoSheet4 = value
                SetChanged()
            End Set
        End Property
        ''' <summary>
        ''' メモ_シート系０５
        ''' </summary>
        ''' <param name="rowIndex"></param>
        ''' <value>メモ_シート系０５</value>
        ''' <returns>メモ_シート系０５</returns>
        ''' <remarks></remarks>
        Public Property MemoSheet5(ByVal rowIndex As Integer) As String
            Get
                Return Record(rowIndex).MemoSheet5
            End Get
            Set(ByVal value As String)
                If EzUtil.IsEqualIfNull(Record(rowIndex).MemoSheet5, value) Then
                    Return
                End If
                Record(rowIndex).MemoSheet5 = value
                SetChanged()
            End Set
        End Property
        ''' <summary>
        ''' メモ_シート系０６
        ''' </summary>
        ''' <param name="rowIndex"></param>
        ''' <value>メモ_シート系０６</value>
        ''' <returns>メモ_シート系０６</returns>
        ''' <remarks></remarks>
        Public Property MemoSheet6(ByVal rowIndex As Integer) As String
            Get
                Return Record(rowIndex).MemoSheet6
            End Get
            Set(ByVal value As String)
                If EzUtil.IsEqualIfNull(Record(rowIndex).MemoSheet6, value) Then
                    Return
                End If
                Record(rowIndex).MemoSheet6 = value
                SetChanged()
            End Set
        End Property
        ''' <summary>
        ''' メモ_シート系０７
        ''' </summary>
        ''' <param name="rowIndex"></param>
        ''' <value>メモ_シート系０７</value>
        ''' <returns>メモ_シート系０７</returns>
        ''' <remarks></remarks>
        Public Property MemoSheet7(ByVal rowIndex As Integer) As String
            Get
                Return Record(rowIndex).MemoSheet7
            End Get
            Set(ByVal value As String)
                If EzUtil.IsEqualIfNull(Record(rowIndex).MemoSheet7, value) Then
                    Return
                End If
                Record(rowIndex).MemoSheet7 = value
                SetChanged()
            End Set
        End Property
        ''' <summary>
        ''' メモ_シート系０８
        ''' </summary>
        ''' <param name="rowIndex"></param>
        ''' <value>メモ_シート系０８</value>
        ''' <returns>メモ_シート系０８</returns>
        ''' <remarks></remarks>
        Public Property MemoSheet8(ByVal rowIndex As Integer) As String
            Get
                Return Record(rowIndex).MemoSheet8
            End Get
            Set(ByVal value As String)
                If EzUtil.IsEqualIfNull(Record(rowIndex).MemoSheet8, value) Then
                    Return
                End If
                Record(rowIndex).MemoSheet8 = value
                SetChanged()
            End Set
        End Property
        ''' <summary>
        ''' メモ_ドアトリム系０１
        ''' </summary>
        ''' <param name="rowIndex"></param>
        ''' <value>メモ_ドアトリム系０１</value>
        ''' <returns>メモ_ドアトリム系０１</returns>
        ''' <remarks></remarks>
        Public Property MemoDoorTrim1(ByVal rowIndex As Integer) As String
            Get
                Return Record(rowIndex).MemoDoorTrim1
            End Get
            Set(ByVal value As String)
                If EzUtil.IsEqualIfNull(Record(rowIndex).MemoDoorTrim1, value) Then
                    Return
                End If
                Record(rowIndex).MemoDoorTrim1 = value
                SetChanged()
            End Set
        End Property
        ''' <summary>
        ''' メモ_ドアトリム系０２
        ''' </summary>
        ''' <param name="rowIndex"></param>
        ''' <value>メモ_ドアトリム系０２</value>
        ''' <returns>メモ_ドアトリム系０２</returns>
        ''' <remarks></remarks>
        Public Property MemoDoorTrim2(ByVal rowIndex As Integer) As String
            Get
                Return Record(rowIndex).MemoDoorTrim2
            End Get
            Set(ByVal value As String)
                If EzUtil.IsEqualIfNull(Record(rowIndex).MemoDoorTrim2, value) Then
                    Return
                End If
                Record(rowIndex).MemoDoorTrim2 = value
                SetChanged()
            End Set
        End Property
        ''' <summary>
        ''' メモ_ルーフトリム系０１
        ''' </summary>
        ''' <param name="rowIndex"></param>
        ''' <value>メモ_ルーフトリム系０１</value>
        ''' <returns>メモ_ルーフトリム系０１</returns>
        ''' <remarks></remarks>
        Public Property MemoRoofTrim1(ByVal rowIndex As Integer) As String
            Get
                Return Record(rowIndex).MemoRoofTrim1
            End Get
            Set(ByVal value As String)
                If EzUtil.IsEqualIfNull(Record(rowIndex).MemoRoofTrim1, value) Then
                    Return
                End If
                Record(rowIndex).MemoRoofTrim1 = value
                SetChanged()
            End Set
        End Property
        ''' <summary>
        ''' メモ_ルーフトリム系０２
        ''' </summary>
        ''' <param name="rowIndex"></param>
        ''' <value>メモ_ルーフトリム系０２</value>
        ''' <returns>メモ_ルーフトリム系０２</returns>
        ''' <remarks></remarks>
        Public Property MemoRoofTrim2(ByVal rowIndex As Integer) As String
            Get
                Return Record(rowIndex).MemoRoofTrim2
            End Get
            Set(ByVal value As String)
                If EzUtil.IsEqualIfNull(Record(rowIndex).MemoRoofTrim2, value) Then
                    Return
                End If
                Record(rowIndex).MemoRoofTrim2 = value
                SetChanged()
            End Set
        End Property
        ''' <summary>
        ''' メモ_サンルールトリム系０１
        ''' </summary>
        ''' <param name="rowIndex"></param>
        ''' <value>メモ_サンルールトリム系０１</value>
        ''' <returns>メモ_サンルールトリム系０１</returns>
        ''' <remarks></remarks>
        Public Property MemoSunroofTrim1(ByVal rowIndex As Integer) As String
            Get
                Return Record(rowIndex).MemoSunroofTrim1
            End Get
            Set(ByVal value As String)
                If EzUtil.IsEqualIfNull(Record(rowIndex).MemoSunroofTrim1, value) Then
                    Return
                End If
                Record(rowIndex).MemoSunroofTrim1 = value
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

        ''' <summary>入力フラグ</summary>
        ''' <value>入力フラグ</value>
        ''' <returns>入力フラグ</returns>
        Public Property InputFlg(ByVal rowIndex As Integer) As String
            Get
                Return Record(rowIndex).InputFlg
            End Get
            Set(ByVal value As String)
                If EzUtil.IsEqualIfNull(Record(rowIndex).InputFlg, value) Then
                    Return
                End If
                Record(rowIndex).InputFlg = value
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

        Public Sub New(ByVal tantoVo As THoyouSekkeiTantoVo, ByVal login As LoginInfo, ByVal aShisakuDate As ShisakuDate, _
                       ByVal detector As HoyouBuhinDetectLatestStructure, _
                       ByVal makeStructure As HoyouBuhinMakeStructureResult, _
                       ByVal aMakerNameResolver As MakerNameResolver, _
                       ByVal editDao As THoyouBuhinEditDao, _
                       ByVal editInstlDao As TShisakuBuhinEditInstlDao, _
                       ByVal hoyouVos As List(Of THoyouBuhinEditVo), _
                       ByVal hyouInstlVos As List(Of TShisakuBuhinEditInstlVo))
            Me.tantoVo = tantoVo
            Me.login = login
            Me.aShisakuDate = aShisakuDate
            Me.detector = detector
            Me.make = makeStructure
            Me.aMakerNameResolver = aMakerNameResolver

            hoyouEventCode = tantoVo.HoyouEventCode

            Jikyu = ""

            Me.editDao = editDao
            Me.editInstlDao = editInstlDao

            _koseiMatrix = New HoyouBuhinBuhinKoseiMatrix(hoyouVos, hyouInstlVos)

            SetChanged()
        End Sub

        Private Function FindEditBy(ByVal tantoVo As THoyouSekkeiTantoVo) As List(Of THoyouBuhinEditVo)
            Dim param As New THoyouBuhinEditVo
            'XXX = XXXしか受け付けないから'

            'Return editDao.FindByPkNotJikyu(blockVo.ShisakuEventCode, blockVo.ShisakuBukaCode, blockVo.ShisakuBlockNo, _
            'blockVo.ShisakuBlockNoKaiteiNo, "J")

            'Jが消えてるか比較するため残しておく() '
            param.HoyouEventCode = tantoVo.HoyouEventCode
            param.HoyouBukaCode = tantoVo.HoyouBukaCode
            param.HoyouTanto = tantoVo.HoyouTanto
            param.HoyouTantoKaiteiNo = tantoVo.HoyouTantoKaiteiNo
            Return editDao.FindBy(param)
        End Function

        Private Function FindEditInstlBy(ByVal tantoVo As THoyouSekkeiTantoVo) As List(Of TShisakuBuhinEditInstlVo)
            Dim param As New TShisakuBuhinEditInstlVo
            param.ShisakuEventCode = tantoVo.HoyouEventCode
            param.ShisakuBukaCode = tantoVo.HoyouBukaCode
            param.ShisakuBlockNo = tantoVo.HoyouTanto
            param.ShisakuBlockNoKaiteiNo = tantoVo.HoyouTantoKaiteiNo
            Return editInstlDao.FindBy(param)
        End Function

        ''' <summary>
        ''' 登録する
        ''' </summary>
        ''' <remarks></remarks>
        Public Sub Register()
            'このあたりを使ってBASEを作るか・・・樺澤'
            '樺澤'
            '** 試作部品編集情報／試作部品編集INSTL情報 **

            Dim editSupplier As New HoyouBuhinBuhinEditKoseiEditSupplier(tantoVo, _koseiMatrix)
            editSupplier.Update(login, editDao, aShisakuDate)

        End Sub

        ''' <summary>
        ''' 部品構成編集クラスを差し替える
        ''' </summary>
        ''' <param name="newMatrix">新しい部品構成編集クラス</param>
        ''' <remarks></remarks>
        Public Sub SupersedeMatrix(ByVal newMatrix As HoyouBuhinBuhinKoseiMatrix)
            Me._koseiMatrix = newMatrix
            SetChanged()
        End Sub

        ''' <summary>
        ''' 自給品の存在する行を削除する
        ''' </summary>
        ''' <param name="columnBag">列構成情報</param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Function removeJikyu(ByVal columnBag As HoyouBuhinInstlColumnBag) As HoyouBuhinInstlColumnBag
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
        ''' 「一括設定」の情報をもとに、新しい部品構成編集クラスを返す
        ''' </summary>
        ''' <param name="structureResults">「一括設定」の情報</param>
        ''' <param name="JikyuUmu">自給有無（0:無し　1:あり</param>
        ''' <param name="sw">取得したINSTL品番を入れ替えるかどうかのフラグ（0:差し替えない　1:差し替える）</param>
        ''' <returns>新しい部品構成編集クラス</returns>
        ''' <remarks></remarks>
        Public Function NewMatrixBySpecified(ByVal isReuseStructure As Boolean, ByVal structureResults As IndexedList(Of HoyouBuhinStructureResult), ByVal JikyuUmu As String, ByVal sw As Integer) As HoyouBuhinBuhinKoseiMatrix
            Dim newMatrix As New HoyouBuhinBuhinKoseiMatrix
            Dim mergeColumn As New HoyouBuhinMergeInstlColumnBag(newMatrix)
            Dim columnIndexes As ICollection(Of Integer) = MergeCollectionAsSorted(_koseiMatrix.GetInputInsuColumnIndexes(), structureResults.Keys)

            Jikyu = JikyuUmu

            For Each columnIndex As Integer In columnIndexes
                If structureResults.Keys.Contains(columnIndex) AndAlso structureResults(columnIndex).IsExist Then
                    'If structureResults.Keys.Contains(columnIndex) AndAlso structureResults(columnIndex).IsExist Then

                    Dim columnBag As HoyouBuhinInstlColumnBag = CreateInstlColumnBag(structureResults(columnIndex))
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
                    If isReuseStructure Then

                        'はい選択時の動き'
                        If StringUtil.Equals(Jikyu, "0") Then
                            _koseiMatrix.InstlColumnAdd(columnIndex, removeJikyu(_koseiMatrix.InstlColumn(columnIndex)))
                        End If

                        mergeColumn.Compute(_koseiMatrix.InstlColumn(columnIndex), columnIndex)

                    Else

                        '対象になった列以外は上と同じ'

                        'いいえ選択時の動き'
                        Dim Level0InstlColumn As New HoyouBuhinInstlColumnBag()
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

        Public Function NewMatrixBySpecified(ByVal isReuseStructure As Boolean, ByVal structureResults As IndexedList(Of HoyouBuhinStructureResult), ByVal JikyuUmu As String, ByVal sw As Integer, ByVal column As Integer, ByVal columncount As Integer) As HoyouBuhinBuhinKoseiMatrix
            Dim newMatrix As New HoyouBuhinBuhinKoseiMatrix
            Dim mergeColumn As New HoyouBuhinMergeInstlColumnBag(newMatrix)
            Dim columnIndexes As ICollection(Of Integer) = MergeCollectionAsSorted(_koseiMatrix.GetInputInsuColumnIndexes(), structureResults.Keys)

            Jikyu = JikyuUmu

            For Each columnIndex As Integer In columnIndexes
                If Not IsTargetColumn(columnIndex, column, columncount) Then
                    mergeColumn.Compute(_koseiMatrix.InstlColumn(columnIndex), columnIndex)
                    Continue For
                End If

                If structureResults.Keys.Contains(columnIndex) AndAlso structureResults(columnIndex).IsExist Then
                    'If structureResults.Keys.Contains(columnIndex) AndAlso structureResults(columnIndex).IsExist Then
                    '対象列以外には関与しない'

                    Dim columnBag As HoyouBuhinInstlColumnBag = CreateInstlColumnBag(structureResults(columnIndex))
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
                    If isReuseStructure Then

                        'はい選択時の動き'
                        If StringUtil.Equals(Jikyu, "0") Then
                            _koseiMatrix.InstlColumnAdd(columnIndex, removeJikyu(_koseiMatrix.InstlColumn(columnIndex)))
                        End If

                        mergeColumn.Compute(_koseiMatrix.InstlColumn(columnIndex), columnIndex)

                    Else

                        '対象になった列以外は上と同じ'

                        'いいえ選択時の動き'
                        Dim Level0InstlColumn As New HoyouBuhinInstlColumnBag()
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

                ' ""は開発号
                Dim aStructureResult As HoyouBuhinStructureResult = detector.Compute(inputedBuhinNo, Nothing, False, "")
                If Not aStructureResult.IsExist Then
                    Return
                End If
                '子部品展開'
                a0553flag = 2
                Dim newKoseiMatrix As HoyouBuhinBuhinKoseiMatrix = GetNewKoseiMatrix(aStructureResult, Level(rowIndex))

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
                    'If srcIndex <> -1 Then
                    If srcIndex <> -1 And srcIndex <> 0 Then
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
                            Dim buhinVo As New HoyouBuhinBuhinKoseiRecordVo
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

            '条件リストを基準にソートする'
            'For Each cond As String In Conditions1List
            '    For Each rowindex As Integer In Me._koseiMatrix.GetInputRowIndexes
            '        'レベル０は対象外'
            '        If Not Me._koseiMatrix(rowindex).Level = 0 Then
            '            'ソートが完了していないこと'
            '            If Me._koseiMatrix(rowindex).SortFlag = 0 Then
            '                'ソート対象かチェックする'
            '                If SortCheck(Conditions1, cond, Me._koseiMatrix.Record(rowindex)) Then
            '                    'レベル０の直下か、直上が同一の値なら移動する必要は無い'
            '                    If Me._koseiMatrix(rowindex - 1).Level = 0 OrElse SortCheck(Conditions1, Me._koseiMatrix(rowindex), Me._koseiMatrix(rowindex - 1)) Then
            '                        Me._koseiMatrix.Record(rowindex).SortFlag = 1
            '                        Continue For
            '                    End If
            '                    Dim index As Integer = level0Count

            '                    '行が同じなら移動する必要は無い'
            '                    If SortCheck(Conditions1, Conditions1List(rowindex - index + 1), Me._koseiMatrix(rowindex)) Then
            '                        Me._koseiMatrix.Record(rowindex).SortFlag = 1
            '                        Continue For
            '                    End If

            '                    '移動先の検索'
            '                    For Each rowindex2 As Integer In Me._koseiMatrix.GetInputRowIndexes
            '                        'レベル0でもソート済みでもないものの中から移動先を探す'
            '                        If Me._koseiMatrix(rowindex2).Level <> 0 Then
            '                            If Me._koseiMatrix(rowindex2).SortFlag = 0 Then
            '                                '対象の値が異なるものを探す'
            '                                If Not SortCheck(Conditions1, Me._koseiMatrix(rowindex2), Me._koseiMatrix(rowindex)) Then
            '                                    index = rowindex2
            '                                    Exit For
            '                                End If
            '                            End If
            '                        End If
            '                    Next

            '                    If StringUtil.Equals("42066AJ060", Me._koseiMatrix.Record(index).BuhinNo) Then
            '                        Dim a As String = ""
            '                    End If
            '                    If StringUtil.Equals("42066AJ060", Me._koseiMatrix.Record(rowindex).BuhinNo) Then
            '                        Dim a As String = ""
            '                    End If

            '                    '移動先がソート済みならソートしない'
            '                    If Me._koseiMatrix(index).SortFlag = 1 Then
            '                        Continue For
            '                    End If

            '                    '移動用レコード'
            '                    Dim buhinVo As New BuhinKoseiRecordVo
            '                    '移動先のレコード取得'
            '                    buhinVo = Me._koseiMatrix.Record(index)
            '                    '移動する'
            '                    Me._koseiMatrix.Record(index) = Me._koseiMatrix.Record(rowindex)
            '                    '移動先と移動元を入れ替える'
            '                    Me._koseiMatrix.Record(rowindex) = buhinVo
            '                    For Each colindex As Integer In Me._koseiMatrix.GetInputInsuColumnIndexes

            '                        Dim insuVo As New BuhinKoseiInsuCellVo
            '                        insuVo = Me._koseiMatrix.InsuVo(index, colindex)
            '                        Me._koseiMatrix.InsuVo(index, colindex) = Me._koseiMatrix.InsuVo(rowindex, colindex)
            '                        Me._koseiMatrix.InsuVo(rowindex, colindex) = insuVo

            '                    Next
            '                    'ソート完了を知らせる'
            '                    Me._koseiMatrix.Record(index).SortFlag = 1
            '                End If
            '            End If
            '        End If
            '    Next
            'Next

            'If Not StringUtil.IsEmpty(Conditions2) Then
            '    SortMatrix23(Conditions2, Conditions1, Conditions2List, level0Count)
            'End If
            'If Not StringUtil.IsEmpty(Conditions3) Then
            '    SortMatrix23(Conditions3, Conditions1, Conditions2List, level0Count, Conditions3)
            'End If


            SetChanged()
        End Sub

        ''' <summary>
        ''' 条件ごとにリストに追加する
        ''' </summary>
        ''' <param name="Conditions"></param>
        ''' <param name="record"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Private Function SortSetList(ByVal Conditions As String, ByVal record As HoyouBuhinBuhinKoseiRecordVo, ByVal ConditionsList As List(Of String)) As List(Of String)
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
        Private Function SortCheck(ByVal Condition As String, ByVal param As String, ByVal record As HoyouBuhinBuhinKoseiRecordVo) As Boolean
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
        Private Function SortCheck(ByVal Condition As String, ByVal record As HoyouBuhinBuhinKoseiRecordVo, ByVal record2 As HoyouBuhinBuhinKoseiRecordVo) As Boolean
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
                                Dim buhinVo As New HoyouBuhinBuhinKoseiRecordVo
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
                            Matrix.Record(rowindex).KyoukuSection = "9SX00"
                        End If
                    End If
                Else
                    If StringUtil.Equals(Matrix.Record(rowindex).ShukeiCode, "A") AndAlso StringUtil.IsEmpty(Matrix.Record(rowindex).KyoukuSection) Then
                        Matrix.Record(rowindex).KyoukuSection = "9SX00"
                    End If
                End If
            Next

            SetChanged()
        End Sub

        Public Function copy() As HoyouBuhinBuhinKoseiMatrix
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
                Dim impl As Dao.HoyouBuhinMakeStructureResultDao = New Dao.HoyouBuhinMakeStructureResultDaoImpl
                Dim vo As New THoyouBuhinEditVo
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
                    'クリアは不要？
                    'NotifyObservers()
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
        Private Function CreateInstlColumnBag(ByVal aStructureResult As HoyouBuhinStructureResult) As HoyouBuhinInstlColumnBag
            '2012/01/16 引数追加
            Dim koseiMatrix As HoyouBuhinBuhinKoseiMatrix = GetNewKoseiMatrix(aStructureResult, 0)
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
                Throw New InvalidOperationException("員数が未入力で HoyouBuhinInstlColumnBag を作成出来ない")
            End If
            Return New HoyouBuhinInstlColumnBag ' EMPTY
        End Function

        ''' <summary>
        ''' 構成情報を元にした部品表のINSTL列情報を作成して返す(構成再展開用)
        ''' </summary>
        ''' <param name="aStructureResult">構成の情報</param>
        ''' <returns>INSTL列情報</returns>
        ''' <remarks></remarks>
        Private Function CreateInstlColumnBagKosei(ByVal aStructureResult As HoyouBuhinStructureResult) As HoyouBuhinInstlColumnBag
            '2012/01/16 引数追加
            Dim koseiMatrix As HoyouBuhinBuhinKoseiMatrix = GetNewKoseiMatrixKosei(aStructureResult)
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
                Throw New InvalidOperationException("員数が未入力で HoyouBuhinInstlColumnBag を作成出来ない")
            End If
            Return New HoyouBuhinInstlColumnBag ' EMPTY
        End Function

        ''' <summary>
        ''' 構成情報を元に部品表を作成して返す
        ''' </summary>
        ''' <param name="aStructureResult">構成の情報</param>
        ''' <returns>新しい部品表</returns>
        ''' <remarks>構成の情報が無い、オリジナル品番の場合、レコード数ゼロの部品表を返す</remarks>
        Public Function GetNewKoseiMatrix(ByVal aStructureResult As HoyouBuhinStructureResult) As HoyouBuhinBuhinKoseiMatrix
            'エラーチェック時に使ってるのみ'
            Return GetNewKoseiMatrix(aStructureResult, 0)
        End Function

        ''' <summary>
        ''' 「構成の情報」を元に部品表を作成する
        ''' </summary>
        ''' <param name="aStructureResult"></param>
        ''' <param name="baseLevel"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Private Function GetNewKoseiMatrix(ByVal aStructureResult As HoyouBuhinStructureResult, ByVal baseLevel As Integer?) As HoyouBuhinBuhinKoseiMatrix
            Return make.Compute(aStructureResult, a0553flag, baseLevel, String.Empty)
        End Function

        ''' <summary>
        ''' 「構成の情報」を元に部品表を作成する
        ''' </summary>
        ''' <param name="aStructureResult"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Private Function GetNewKoseiMatrixKosei(ByVal aStructureResult As HoyouBuhinStructureResult) As HoyouBuhinBuhinKoseiMatrix
            Return make.GetBuhinKosei(aStructureResult, a0553flag)
        End Function

        '''' <summary>
        '''' INSTLの部品情報だけ取得する
        '''' </summary>
        '''' <param name="aStructureResult"></param>
        '''' <returns></returns>
        '''' <remarks></remarks>
        'Private Function GetNewBuhinKosei(ByVal aStructureResult As StructureResult) As HoyouBuhinBuhinKoseiMatrix
        '    Return make.GetBuhinKosei(aStructureResult, a0553flag)
        'End Function

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
                    baseLevel = Me._koseiMatrix.Record(wRowIndex).Level
                End If
            Else
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
        ''' 担当者を差し替える
        ''' </summary>
        ''' <param name="tantoVo">担当者</param>
        ''' <remarks></remarks>
        Public Sub SupersedeTantoVo(ByVal tantoVo As THoyouSekkeiTantoVo)
            Me.tantoVo = tantoVo

            ' TShisakuBuhinEdit をDaoで参照
            Dim editVos As List(Of THoyouBuhinEditVo) = FindEditBy(tantoVo)
            If 0 < editVos.Count Then
                ' 存在したら、TShisakuBuhinEditInstl も参照して、それで _koseiMatrix を初期化＆表示 ※員数は「EditInstlのINSTL品番表示順」に合せる
                Dim editInstlVos As List(Of TShisakuBuhinEditInstlVo) = FindEditInstlBy(tantoVo)
                _koseiMatrix = New HoyouBuhinBuhinKoseiMatrix(editVos, editInstlVos)
            Else
                _koseiMatrix = New HoyouBuhinBuhinKoseiMatrix
                isWaitingKoseiTenkai = True
            End If

            SetChanged()
        End Sub

        ''↓↓2014/08/26 Ⅰ.3.設計編集 ベース車改修専用化_bx) (TES)張 ADD BEGIN
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
        Public Property TsukurikataNounyu(ByVal rowIndex As Integer) As Int32?
            Get
                Return Record(rowIndex).TsukurikataNounyu
            End Get
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

        Public Function GetLabelValues_TsukurikataSeisaku() As List(Of LabelValueVo)
            Return makeShisakuBlockDao.FindTsukurikataSeisakuLabelValues
        End Function

        Public Function GetLabelValues_TsukurikataKatashiyou1() As List(Of LabelValueVo)
            Return MakeShisakuBlockDao.FindTsukurikataKatashiyou1LabelValues
        End Function

        Public Function GetLabelValues_TsukurikataKatashiyou2() As List(Of LabelValueVo)
            Return MakeShisakuBlockDao.FindTsukurikataKatashiyou2LabelValues
        End Function

        Public Function GetLabelValues_TsukurikataKatashiyou3() As List(Of LabelValueVo)
            Return MakeShisakuBlockDao.FindTsukurikataKatashiyou3LabelValues
        End Function

        Public Function GetLabelValues_TsukurikataTigu() As List(Of LabelValueVo)
            Return MakeShisakuBlockDao.FindTsukurikataTiguLabelValues
        End Function

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

        '↓↓↓2014/12/25 メタル項目を追加 TES)張 ADD BEGIN
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
        '↑↑↑2014/12/25 メタル項目を追加 TES)張 ADD END

    End Class
End Namespace