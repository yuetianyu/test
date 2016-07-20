Imports ShisakuCommon.Db.EBom.Dao
Imports ShisakuCommon
Imports ShisakuCommon.Db.EBom.Vo
Imports ShisakuCommon.Util
Imports ShisakuCommon.Db.EBom.Dao.Impl
Imports ShisakuCommon.DateUtil
Imports EventSakusei.EventEdit.Dao
Imports ShisakuCommon.Db.EBom.Vo.Helper.TShisakuSekkeiBlockSoubiVoHelper
Imports ShisakuCommon.ShisakuComFunc

Namespace EventEdit.Logic
    Public Class EventEditCompleteCar : Inherits Observable

        Private shisakuEventCode As String
        Private ReadOnly login As LoginInfo
        Private ReadOnly kanseiDao As TShisakuEventKanseiDao
        Private ReadOnly kanseiKaiteiDao As TShisakuEventKanseikaiteiDao
        Private ReadOnly aDate As ShisakuDate
        Private ReadOnly aEzSync As EzSyncShubetsuGosha
        Private importFlag As Boolean

        '完成車車情報
        Private seisakuHakouNo As String
        Private seisakuHakouNoKaiteiNo As String
        Private kanseiList As New List(Of TSeisakuIchiranKanseiVo)
        Private wbList As New List(Of TSeisakuIchiranWbVo)

        Private shisakuHakouNo As String
        Private shisakuHakouNoKaiteiNo As Integer

        ''' <summary>
        ''' 初期設定
        ''' </summary>
        ''' <param name="shisakuEventCode">試作イベントコード</param>
        ''' <param name="login">ログイン情報</param>
        ''' <param name="aEzSync">同期クラス</param>
        ''' <param name="isSekkeiTenkaiIkou">設計展開以降か</param>
        ''' <param name="kanseiDao">完成車Dao</param>
        ''' <param name="kanseiKaiteiDao">完成車Dao</param>
        ''' <param name="aDate">日付</param>
        ''' <param name="importFlag">インポートフラグ</param>
        ''' <param name="seisakuHakouNo">製作一覧発行№</param>
        ''' <param name="seisakuHakouNoKaiteiNo">製作一覧発行№改訂№</param>
        ''' <param name="shisakuHakouNo">前回保存発行№</param>
        ''' <param name="shisakuHakouNoKaiteiNo">前回保存発行№改訂№</param>
        ''' <remarks></remarks>
        Public Sub New(ByVal shisakuEventCode As String, _
                       ByVal login As LoginInfo, _
                       ByVal aEzSync As EzSyncShubetsuGosha, _
                       ByVal isSekkeiTenkaiIkou As Boolean, _
                       ByVal kanseiDao As TShisakuEventKanseiDao, _
                       ByVal kanseiKaiteiDao As TShisakuEventKanseiKaiteiDao, _
                       ByVal aDate As ShisakuDate, _
                       ByVal importFlag As Boolean, _
                       ByVal seisakuHakouNo As String, _
                       ByVal seisakuHakouNoKaiteiNo As String, _
                       ByVal shisakuHakouNo As String, _
                       ByVal shisakuHakouNoKaiteiNo As Integer)
            Me.shisakuEventCode = shisakuEventCode
            Me.login = login
            Me.kanseiDao = kanseiDao
            Me.kanseiKaiteiDao = kanseiKaiteiDao
            Me.aDate = aDate
            Me.aEzSync = aEzSync
            Me._isSekkeiTenkaiIkou = isSekkeiTenkaiIkou
            Me.importFlag = importFlag

            Me.seisakuHakouNo = seisakuHakouNo
            Me.seisakuHakouNoKaiteiNo = seisakuHakouNoKaiteiNo
            '前回登録時の内容
            Me.shisakuHakouNo = shisakuHakouNo
            Me.shisakuHakouNoKaiteiNo = shisakuHakouNoKaiteiNo

            '製作一覧キー情報があれば製作一覧情報からセットする。
            If StringUtil.IsNotEmpty(seisakuHakouNo) Then
                '編集モードかつ設計展開以降なら
                If Not IsAddMode() And isSekkeiTenkaiIkou Then
                    ReadRecordsUpdate()
                End If
            Else
                If Not IsAddMode() Then
                    ReadRecords()
                End If
            End If
            SetChanged()
        End Sub

#Region "完成車情報のDelegateプロパティ"
        ''' <summary>試作種別</summary>
        ''' <value>試作種別</value>
        ''' <returns>試作種別</returns>
        Public Property ShisakuSyubetu(ByVal rowNo As Integer) As String
            Get
                Return Records(rowNo).ShisakuSyubetu
            End Get
            Set(ByVal value As String)
                If EzUtil.IsEqualIfNull(Records(rowNo).ShisakuSyubetu, value) Then
                    Return
                End If
                Records(rowNo).ShisakuSyubetu = value
                SetChanged()
                aEzSync.NotifyShubetsu(Me, rowNo)
            End Set
        End Property

        ''' <summary>試作号車</summary>
        ''' <value>試作号車</value>
        ''' <returns>試作号車</returns>
        Public Property ShisakuGousya(ByVal rowNo As Integer) As String
            Get
                Return Records(rowNo).ShisakuGousya
            End Get
            Set(ByVal value As String)
                If EzUtil.IsEqualIfNull(Records(rowNo).ShisakuGousya, value) Then
                    Return
                End If
                Records(rowNo).ShisakuGousya = value
                SetChanged()
                aEzSync.NotifyGosha(Me, rowNo)
            End Set
        End Property

        ''' <summary>試作車型</summary>
        ''' <value>試作車型</value>
        ''' <returns>試作車型</returns>
        Public Property ShisakuSyagata(ByVal rowNo As Integer) As String
            Get
                Return Records(rowNo).ShisakuSyagata
            End Get
            Set(ByVal value As String)
                If EzUtil.IsEqualIfNull(Records(rowNo).ShisakuSyagata, value) Then
                    Return
                End If
                Records(rowNo).ShisakuSyagata = value
                SetChanged()
            End Set
        End Property

        ''' <summary>試作グレード</summary>
        ''' <value>試作グレード</value>
        ''' <returns>試作グレード</returns>
        Public Property ShisakuGrade(ByVal rowNo As Integer) As String
            Get
                Return Records(rowNo).ShisakuGrade
            End Get
            Set(ByVal value As String)
                If EzUtil.IsEqualIfNull(Records(rowNo).ShisakuGrade, value) Then
                    Return
                End If
                Records(rowNo).ShisakuGrade = value
                SetChanged()
            End Set
        End Property

        ''' <summary>仕向地・仕向け</summary>
        ''' <value>仕向地・仕向け</value>
        ''' <returns>仕向地・仕向け</returns>
        Public Property ShisakuShimukechiShimuke(ByVal rowNo As Integer) As String
            Get
                Return Records(rowNo).ShisakuShimukechiShimuke
            End Get
            Set(ByVal value As String)
                If EzUtil.IsEqualIfNull(Records(rowNo).ShisakuShimukechiShimuke, value) Then
                    Return
                End If
                Records(rowNo).ShisakuShimukechiShimuke = value
                SetChanged()
            End Set
        End Property

        ''' <summary>試作ハンドル</summary>
        ''' <value>試作ハンドル</value>
        ''' <returns>試作ハンドル</returns>
        Public Property ShisakuHandoru(ByVal rowNo As Integer) As String
            Get
                Return Records(rowNo).ShisakuHandoru
            End Get
            Set(ByVal value As String)
                If EzUtil.IsEqualIfNull(Records(rowNo).ShisakuHandoru, value) Then
                    Return
                End If
                Records(rowNo).ShisakuHandoru = value
                SetChanged()
            End Set
        End Property

        ''' <summary>試作E/G型式</summary>
        ''' <value>試作E/G型式</value>
        ''' <returns>試作E/G型式</returns>
        Public Property ShisakuEgKatashiki(ByVal rowNo As Integer) As String
            Get
                Return Records(rowNo).ShisakuEgKatashiki
            End Get
            Set(ByVal value As String)
                If EzUtil.IsEqualIfNull(Records(rowNo).ShisakuEgKatashiki, value) Then
                    Return
                End If
                Records(rowNo).ShisakuEgKatashiki = value
                SetChanged()
            End Set
        End Property

        ''' <summary>試作E/G排気量</summary>
        ''' <value>試作E/G排気量</value>
        ''' <returns>試作E/G排気量</returns>
        Public Property ShisakuEgHaikiryou(ByVal rowNo As Integer) As String
            Get
                Return Records(rowNo).ShisakuEgHaikiryou
            End Get
            Set(ByVal value As String)
                If EzUtil.IsEqualIfNull(Records(rowNo).ShisakuEgHaikiryou, value) Then
                    Return
                End If
                Records(rowNo).ShisakuEgHaikiryou = value
                SetChanged()
            End Set
        End Property

        ''' <summary>試作E/Gシステム</summary>
        ''' <value>試作E/Gシステム</value>
        ''' <returns>試作E/Gシステム</returns>
        Public Property ShisakuEgSystem(ByVal rowNo As Integer) As String
            Get
                Return Records(rowNo).ShisakuEgSystem
            End Get
            Set(ByVal value As String)
                If EzUtil.IsEqualIfNull(Records(rowNo).ShisakuEgSystem, value) Then
                    Return
                End If
                Records(rowNo).ShisakuEgSystem = value
                SetChanged()
            End Set
        End Property

        ''' <summary>試作E/G過給機</summary>
        ''' <value>試作E/G過給機</value>
        ''' <returns>試作E/G過給機</returns>
        Public Property ShisakuEgKakyuuki(ByVal rowNo As Integer) As String
            Get
                Return Records(rowNo).ShisakuEgKakyuuki
            End Get
            Set(ByVal value As String)
                If EzUtil.IsEqualIfNull(Records(rowNo).ShisakuEgKakyuuki, value) Then
                    Return
                End If
                Records(rowNo).ShisakuEgKakyuuki = value
                SetChanged()
            End Set
        End Property

        ''' <summary>試作E/Gメモ１</summary>
        ''' <value>試作E/Gメモ１</value>
        ''' <returns>試作E/Gメモ１</returns>
        Public Property ShisakuEgMemo1(ByVal rowNo As Integer) As String
            Get
                Return Records(rowNo).ShisakuEgMemo1
            End Get
            Set(ByVal value As String)
                If EzUtil.IsEqualIfNull(Records(rowNo).ShisakuEgMemo1, value) Then
                    Return
                End If
                Records(rowNo).ShisakuEgMemo1 = value
                SetChanged()
            End Set
        End Property

        ''' <summary>試作E/Gメモ２</summary>
        ''' <value>試作E/Gメモ２</value>
        ''' <returns>試作E/Gメモ２</returns>
        Public Property ShisakuEgMemo2(ByVal rowNo As Integer) As String
            Get
                Return Records(rowNo).ShisakuEgMemo2
            End Get
            Set(ByVal value As String)
                If EzUtil.IsEqualIfNull(Records(rowNo).ShisakuEgMemo2, value) Then
                    Return
                End If
                Records(rowNo).ShisakuEgMemo2 = value
                SetChanged()
            End Set
        End Property

        ''' <summary>試作E/Gメモ１のラベル</summary>
        ''' <value>試作E/Gメモ１のラベル</value>
        ''' <returns>試作E/Gメモ１のラベル</returns>
        Public Property ShisakuEgMemo1Label(ByVal rowNo As Integer) As String
            Get
                Return Records(rowNo).ShisakuEgMemo1Label
            End Get
            Set(ByVal value As String)
                If EzUtil.IsEqualIfNull(Records(rowNo).ShisakuEgMemo1Label, value) Then
                    Return
                End If
                Records(rowNo).ShisakuEgMemo1Label = value
                SetChanged()
            End Set
        End Property

        ''' <summary>試作E/Gメモ２</summary>
        ''' <value>試作E/Gメモ２</value>
        ''' <returns>試作E/Gメモ２</returns>
        Public Property ShisakuEgMemo2Label(ByVal rowNo As Integer) As String
            Get
                Return Records(rowNo).ShisakuEgMemo2Label
            End Get
            Set(ByVal value As String)
                If EzUtil.IsEqualIfNull(Records(rowNo).ShisakuEgMemo2Label, value) Then
                    Return
                End If
                Records(rowNo).ShisakuEgMemo2Label = value
                SetChanged()
            End Set
        End Property

        ''' <summary>試作T/M駆動</summary>
        ''' <value>試作T/M駆動</value>
        ''' <returns>試作T/M駆動</returns>
        Public Property ShisakuTmKudou(ByVal rowNo As Integer) As String
            Get
                Return Records(rowNo).ShisakuTmKudou
            End Get
            Set(ByVal value As String)
                If EzUtil.IsEqualIfNull(Records(rowNo).ShisakuTmKudou, value) Then
                    Return
                End If
                Records(rowNo).ShisakuTmKudou = value
                SetChanged()
            End Set
        End Property

        ''' <summary>試作T/M変速機</summary>
        ''' <value>試作T/M変速機</value>
        ''' <returns>試作T/M変速機</returns>
        Public Property ShisakuTmHensokuki(ByVal rowNo As Integer) As String
            Get
                Return Records(rowNo).ShisakuTmHensokuki
            End Get
            Set(ByVal value As String)
                If EzUtil.IsEqualIfNull(Records(rowNo).ShisakuTmHensokuki, value) Then
                    Return
                End If
                Records(rowNo).ShisakuTmHensokuki = value
                SetChanged()
            End Set
        End Property

        ''' <summary>試作T/M副変速機</summary>
        ''' <value>試作T/M副変速機</value>
        ''' <returns>試作T/M副変速機</returns>
        Public Property ShisakuTmFukuHensokuki(ByVal rowNo As Integer) As String
            Get
                Return Records(rowNo).ShisakuTmFukuHensokuki
            End Get
            Set(ByVal value As String)
                If EzUtil.IsEqualIfNull(Records(rowNo).ShisakuTmFukuHensokuki, value) Then
                    Return
                End If
                Records(rowNo).ShisakuTmFukuHensokuki = value
                SetChanged()
            End Set
        End Property

        ''' <summary>試作T/Mメモ１</summary>
        ''' <value>試作T/Mメモ１</value>
        ''' <returns>試作T/Mメモ１</returns>
        Public Property ShisakuTmMemo1(ByVal rowNo As Integer) As String
            Get
                Return Records(rowNo).ShisakuTmMemo1
            End Get
            Set(ByVal value As String)
                If EzUtil.IsEqualIfNull(Records(rowNo).ShisakuTmMemo1, value) Then
                    Return
                End If
                Records(rowNo).ShisakuTmMemo1 = value
                SetChanged()
            End Set
        End Property

        ''' <summary>試作T/Mメモ２</summary>
        ''' <value>試作T/Mメモ２</value>
        ''' <returns>試作T/Mメモ２</returns>
        Public Property ShisakuTmMemo2(ByVal rowNo As Integer) As String
            Get
                Return Records(rowNo).ShisakuTmMemo2
            End Get
            Set(ByVal value As String)
                If EzUtil.IsEqualIfNull(Records(rowNo).ShisakuTmMemo2, value) Then
                    Return
                End If
                Records(rowNo).ShisakuTmMemo2 = value
                SetChanged()
            End Set
        End Property

        ''' <summary>試作T/Mメモ１のラベル</summary>
        ''' <value>試作T/Mメモ１のラベル</value>
        ''' <returns>試作T/Mメモ１のラベル</returns>
        Public Property ShisakuTmMemo1Label(ByVal rowNo As Integer) As String
            Get
                Return Records(rowNo).ShisakuTmMemo1Label
            End Get
            Set(ByVal value As String)
                If EzUtil.IsEqualIfNull(Records(rowNo).ShisakuTmMemo1Label, value) Then
                    Return
                End If
                Records(rowNo).ShisakuTmMemo1Label = value
                SetChanged()
            End Set
        End Property

        ''' <summary>試作T/Mメモ２のラベル</summary>
        ''' <value>試作T/Mメモ２のラベル</value>
        ''' <returns>試作T/Mメモ２のラベル</returns>
        Public Property ShisakuTmMemo2Label(ByVal rowNo As Integer) As String
            Get
                Return Records(rowNo).ShisakuTmMemo2Label
            End Get
            Set(ByVal value As String)
                If EzUtil.IsEqualIfNull(Records(rowNo).ShisakuTmMemo2Label, value) Then
                    Return
                End If
                Records(rowNo).ShisakuTmMemo2Label = value
                SetChanged()
            End Set
        End Property

        ''' <summary>試作型式</summary>
        ''' <value>試作型式</value>
        ''' <returns>試作型式</returns>
        Public Property ShisakuKatashiki(ByVal rowNo As Integer) As String
            Get
                Return Records(rowNo).ShisakuKatashiki
            End Get
            Set(ByVal value As String)
                If EzUtil.IsEqualIfNull(Records(rowNo).ShisakuKatashiki, value) Then
                    Return
                End If
                Records(rowNo).ShisakuKatashiki = value
                SetChanged()
            End Set
        End Property

        ''' <summary>試作仕向け</summary>
        ''' <value>試作仕向け</value>
        ''' <returns>試作仕向け</returns>
        Public Property ShisakuShimuke(ByVal rowNo As Integer) As String
            Get
                Return Records(rowNo).ShisakuShimuke
            End Get
            Set(ByVal value As String)
                If EzUtil.IsEqualIfNull(Records(rowNo).ShisakuShimuke, value) Then
                    Return
                End If
                Records(rowNo).ShisakuShimuke = value
                SetChanged()
            End Set
        End Property

        ''' <summary>試作OP</summary>
        ''' <value>試作OP</value>
        ''' <returns>試作OP</returns>
        Public Property ShisakuOp(ByVal rowNo As Integer) As String
            Get
                Return Records(rowNo).ShisakuOp
            End Get
            Set(ByVal value As String)
                If EzUtil.IsEqualIfNull(Records(rowNo).ShisakuOp, value) Then
                    Return
                End If
                Records(rowNo).ShisakuOp = value
                SetChanged()
            End Set
        End Property

        ''' <summary>試作外装色</summary>
        ''' <value>試作外装色</value>
        ''' <returns>試作外装色</returns>
        Public Property ShisakuGaisousyoku(ByVal rowNo As Integer) As String
            Get
                Return Records(rowNo).ShisakuGaisousyoku
            End Get
            Set(ByVal value As String)
                If EzUtil.IsEqualIfNull(Records(rowNo).ShisakuGaisousyoku, value) Then
                    Return
                End If
                Records(rowNo).ShisakuGaisousyoku = value
                SetChanged()
            End Set
        End Property

        ''' <summary>試作外装色名</summary>
        ''' <value>試作外装色名</value>
        ''' <returns>試作外装色名</returns>
        Public Property ShisakuGaisousyokuName(ByVal rowNo As Integer) As String
            Get
                Return Records(rowNo).ShisakuGaisousyokuName
            End Get
            Set(ByVal value As String)
                If EzUtil.IsEqualIfNull(Records(rowNo).ShisakuGaisousyokuName, value) Then
                    Return
                End If
                Records(rowNo).ShisakuGaisousyokuName = value
                SetChanged()
            End Set
        End Property

        ''' <summary>試作内装色</summary>
        ''' <value>試作内装色</value>
        ''' <returns>試作内装色</returns>
        Public Property ShisakuNaisousyoku(ByVal rowNo As Integer) As String
            Get
                Return Records(rowNo).ShisakuNaisousyoku
            End Get
            Set(ByVal value As String)
                If EzUtil.IsEqualIfNull(Records(rowNo).ShisakuNaisousyoku, value) Then
                    Return
                End If
                Records(rowNo).ShisakuNaisousyoku = value
                SetChanged()
            End Set
        End Property

        ''' <summary>試作内装色名</summary>
        ''' <value>試作内装色名</value>
        ''' <returns>試作内装色名</returns>
        Public Property ShisakuNaisousyokuName(ByVal rowNo As Integer) As String
            Get
                Return Records(rowNo).ShisakuNaisousyokuName
            End Get
            Set(ByVal value As String)
                If EzUtil.IsEqualIfNull(Records(rowNo).ShisakuNaisousyokuName, value) Then
                    Return
                End If
                Records(rowNo).ShisakuNaisousyokuName = value
                SetChanged()
            End Set
        End Property

        ''' <summary>試作車台№</summary>
        ''' <value>試作車台№</value>
        ''' <returns>試作車台№</returns>
        Public Property ShisakuSyadaiNo(ByVal rowNo As Integer) As String
            Get
                Return Records(rowNo).ShisakuSyadaiNo
            End Get
            Set(ByVal value As String)
                If EzUtil.IsEqualIfNull(Records(rowNo).ShisakuSyadaiNo, value) Then
                    Return
                End If
                Records(rowNo).ShisakuSyadaiNo = value
                SetChanged()
            End Set
        End Property

        ''' <summary>使用目的</summary>
        ''' <value>使用目的</value>
        ''' <returns>使用目的</returns>
        Public Property ShisakuShiyouMokuteki(ByVal rowNo As Integer) As String
            Get
                Return Records(rowNo).ShisakuShiyouMokuteki
            End Get
            Set(ByVal value As String)
                If EzUtil.IsEqualIfNull(Records(rowNo).ShisakuShiyouMokuteki, value) Then
                    Return
                End If
                Records(rowNo).ShisakuShiyouMokuteki = value
                SetChanged()
            End Set
        End Property

        ''' <summary>試作試験目的</summary>
        ''' <value>試作試験目的</value>
        ''' <returns>試作試験目的</returns>
        Public Property ShisakuShikenMokuteki(ByVal rowNo As Integer) As String
            Get
                Return Records(rowNo).ShisakuShikenMokuteki
            End Get
            Set(ByVal value As String)
                If EzUtil.IsEqualIfNull(Records(rowNo).ShisakuShikenMokuteki, value) Then
                    Return
                End If
                Records(rowNo).ShisakuShikenMokuteki = value
                SetChanged()
            End Set
        End Property

        ''' <summary>試作使用部署</summary>
        ''' <value>試作使用部署</value>
        ''' <returns>試作使用部署</returns>
        Public Property ShisakuSiyouBusyo(ByVal rowNo As Integer) As String
            Get
                Return Records(rowNo).ShisakuSiyouBusyo
            End Get
            Set(ByVal value As String)
                If EzUtil.IsEqualIfNull(Records(rowNo).ShisakuSiyouBusyo, value) Then
                    Return
                End If
                Records(rowNo).ShisakuSiyouBusyo = value
                SetChanged()
            End Set
        End Property

        ''' <summary>試作グループ</summary>
        ''' <value>試作グループ</value>
        ''' <returns>試作グループ</returns>
        Public Property ShisakuGroup(ByVal rowNo As Integer) As String
            Get
                Return Records(rowNo).ShisakuGroup
            End Get
            Set(ByVal value As String)
                If EzUtil.IsEqualIfNull(Records(rowNo).ShisakuGroup, value) Then
                    Return
                End If
                Records(rowNo).ShisakuGroup = value
                SetChanged()
            End Set
        End Property

        ''' <summary>製作順序</summary>
        ''' <value>製作順序</value>
        ''' <returns>製作順序</returns>
        Public Property ShisakuSeisakuJunjyo(ByVal rowNo As Integer) As String
            Get
                Return Records(rowNo).ShisakuSeisakuJunjyo
            End Get
            Set(ByVal value As String)
                If EzUtil.IsEqualIfNull(Records(rowNo).ShisakuSeisakuJunjyo, value) Then
                    Return
                End If
                Records(rowNo).ShisakuSeisakuJunjyo = value
                SetChanged()
            End Set
        End Property

        ''' <summary>試作完成日</summary>
        ''' <value>試作完成日</value>
        ''' <returns>試作完成日</returns>
        Public Property ShisakuKanseibi(ByVal rowNo As Integer) As Nullable(Of Int32)
            Get
                Return Records(rowNo).ShisakuKanseibi
            End Get
            Set(ByVal value As Nullable(Of Int32))
                If EzUtil.IsEqualIfNull(Records(rowNo).ShisakuKanseibi, value) Then
                    Return
                End If
                Records(rowNo).ShisakuKanseibi = value
                SetChanged()
            End Set
        End Property

        ''' <summary>試作工指№</summary>
        ''' <value>試作工指№</value>
        ''' <returns>試作工指№</returns>
        Public Property ShisakuKoushiNo(ByVal rowNo As Integer) As String
            Get
                Return Records(rowNo).ShisakuKoushiNo
            End Get
            Set(ByVal value As String)
                If EzUtil.IsEqualIfNull(Records(rowNo).ShisakuKoushiNo, value) Then
                    Return
                End If
                Records(rowNo).ShisakuKoushiNo = value
                SetChanged()
            End Set
        End Property

        ''' <summary>製作方法区分</summary>
        ''' <value>製作方法区分</value>
        ''' <returns>製作方法区分</returns>
        Public Property ShisakuSeisakuHouhouKbn(ByVal rowNo As Integer) As String
            Get
                Return Records(rowNo).ShisakuSeisakuHouhouKbn
            End Get
            Set(ByVal value As String)
                If EzUtil.IsEqualIfNull(Records(rowNo).ShisakuSeisakuHouhouKbn, value) Then
                    Return
                End If
                Records(rowNo).ShisakuSeisakuHouhouKbn = value
                SetChanged()
            End Set
        End Property

        ''' <summary>製作方法</summary>
        ''' <value>製作方法</value>
        ''' <returns>製作方法</returns>
        Public Property ShisakuSeisakuHouhou(ByVal rowNo As Integer) As String
            Get
                Return Records(rowNo).ShisakuSeisakuHouhou
            End Get
            Set(ByVal value As String)
                If EzUtil.IsEqualIfNull(Records(rowNo).ShisakuSeisakuHouhou, value) Then
                    Return
                End If
                Records(rowNo).ShisakuSeisakuHouhou = value
                SetChanged()
            End Set
        End Property

        ''' <summary>メモ欄</summary>
        ''' <value>メモ欄</value>
        ''' <returns>メモ欄</returns>
        Public Property ShisakuMemo(ByVal rowNo As Integer) As String
            Get
                Return Records(rowNo).ShisakuMemo
            End Get
            Set(ByVal value As String)
                If EzUtil.IsEqualIfNull(Records(rowNo).ShisakuMemo, value) Then
                    Return
                End If
                Records(rowNo).ShisakuMemo = value
                SetChanged()
            End Set
        End Property

#End Region

#Region "行情報取得・操作"
        Private _record As New IndexedList(Of EventEditCompleteCarVo)

        ''' <summary>完成車情報</summary>
        ''' <returns>完成車情報</returns>
        Private ReadOnly Property Records(ByVal rowNo As Integer) As EventEditCompleteCarVo
            Get
                Return _record.Value(rowNo)
            End Get
        End Property

        ''' <summary>
        ''' 入力行の行Noの一覧を返す
        ''' </summary>
        ''' <returns>入力行の行Noの一覧</returns>
        ''' <remarks></remarks>
        Public Function GetInputRowNos() As ICollection(Of Integer)
            Return _record.Keys
        End Function

        ''' <summary>
        ''' 行を挿入する
        ''' </summary>
        ''' <param name="rowNo">挿入先の行No</param>
        ''' <remarks></remarks>
        Public Sub InsertRow(ByVal rowNo As Integer)
            _record.Insert(rowNo)
        End Sub

        ''' <summary>
        ''' 行を削除する
        ''' </summary>
        ''' <param name="rowNo">削除する行No</param>
        ''' <remarks></remarks>
        Public Sub DeleteRow(ByVal rowNo As Integer)
            _record.Remove(rowNo)
        End Sub

        Private Sub ReadRecords()
            '製作一覧HD情報
            Dim getSeisakuIchiranHd As New SeisakuIchiranDaoImpl
            Dim tSeisakuHakouHdVo As New TSeisakuHakouHdVo
            tSeisakuHakouHdVo = getSeisakuIchiranHd.GetSeisakuIchiranHd(shisakuHakouNo, _
                                                                        CStr(shisakuHakouNoKaiteiNo).PadLeft(2, "0"c))

            Dim param As New TShisakuEventKanseiVo
            param.ShisakuEventCode = shisakuEventCode
            Dim vos As List(Of TShisakuEventKanseiVo) = kanseiDao.FindBy(param)
            For Each vo As TShisakuEventKanseiVo In vos

                '工指Noはコピー時とエクセルインポート時には空白にする'
                If importFlag Then
                    vo.ShisakuKoushiNo = ""
                End If

                Dim record As New EventEditCompleteCarVo
                VoUtil.CopyProperties(vo, record)

                'E/G、T/Mのメモらカラムヘッダー
                If StringUtil.IsNotEmpty(tSeisakuHakouHdVo) Then
                    If StringUtil.IsNotEmpty(tSeisakuHakouHdVo.KanseiEgMemo1) Then
                        record.ShisakuEgMemo1Label = tSeisakuHakouHdVo.KanseiEgMemo1
                    Else
                        record.ShisakuEgMemo1Label = "メモ１"
                    End If
                    If StringUtil.IsNotEmpty(tSeisakuHakouHdVo.KanseiEgMemo2) Then
                        record.ShisakuEgMemo2Label = tSeisakuHakouHdVo.KanseiEgMemo2
                    Else
                        record.ShisakuEgMemo2Label = "メモ２"
                    End If
                    If StringUtil.IsNotEmpty(tSeisakuHakouHdVo.KanseiTmMemo1) Then
                        record.ShisakuTmMemo1Label = tSeisakuHakouHdVo.KanseiTmMemo1
                    Else
                        record.ShisakuTmMemo1Label = "メモ１"
                    End If
                    If StringUtil.IsNotEmpty(tSeisakuHakouHdVo.KanseiTmMemo2) Then
                        record.ShisakuTmMemo2Label = tSeisakuHakouHdVo.KanseiTmMemo2
                    Else
                        record.ShisakuTmMemo2Label = "メモ２"
                    End If
                End If

                '' Hyojijunは NotNull項目なので、強制的にキャスト
                _record.Add(CType(vo.HyojijunNo, Integer), record)
            Next
        End Sub


        ''' <summary>
        ''' 行の読み込み
        '''　製作一覧更新
        ''' </summary>
        ''' <remarks></remarks>
        Private Sub ReadRecordsUpdate()

            '初期設定
            Dim Ichiran = New SeisakuIchiranDaoImpl
            Dim HoldRowNo As Integer = 0
            'Dim strGousya As String
            '製作一覧HD情報
            Dim getSeisakuIchiranHd As New SeisakuIchiranDaoImpl
            Dim tSeisakuHakouHdVo As New TSeisakuHakouHdVo
            tSeisakuHakouHdVo = getSeisakuIchiranHd.GetSeisakuIchiranHd(seisakuHakouNo, seisakuHakouNoKaiteiNo)
            'ベース車情報
            Dim BaseVo As TShisakuEventBaseSeisakuIchiranVo
            Dim eventBaseCarDao As EventEditCompleteCarRirekiDao = New EventEditCompleteCarRirekiDaoImpl()
            '完成車情報
            kanseiList = Ichiran.GetTSeisakuIchiranKansei(seisakuHakouNo, seisakuHakouNoKaiteiNo)
            'ＷＢ車情報
            wbList = Ichiran.GetTSeisakuIchiranWb(seisakuHakouNo, seisakuHakouNoKaiteiNo)
            Dim param As New TShisakuEventKanseiVo
            param.ShisakuEventCode = shisakuEventCode
            Dim vos As List(Of TShisakuEventKanseiVo) = kanseiDao.FindBy(param)

            For Each vo As TShisakuEventKanseiVo In vos '試作イベントのVO
                Dim record As New EventEditCompleteCarVo
                'E/G、T/Mのメモらカラムヘッダー
                If StringUtil.IsNotEmpty(tSeisakuHakouHdVo) Then
                    If StringUtil.IsNotEmpty(tSeisakuHakouHdVo.KanseiEgMemo1) Then
                        record.ShisakuEgMemo1Label = tSeisakuHakouHdVo.KanseiEgMemo1
                    Else
                        record.ShisakuEgMemo1Label = "メモ１"
                    End If
                    If StringUtil.IsNotEmpty(tSeisakuHakouHdVo.KanseiEgMemo2) Then
                        record.ShisakuEgMemo2Label = tSeisakuHakouHdVo.KanseiEgMemo2
                    Else
                        record.ShisakuEgMemo2Label = "メモ２"
                    End If
                    If StringUtil.IsNotEmpty(tSeisakuHakouHdVo.KanseiTmMemo1) Then
                        record.ShisakuTmMemo1Label = tSeisakuHakouHdVo.KanseiTmMemo1
                    Else
                        record.ShisakuTmMemo1Label = "メモ１"
                    End If
                    If StringUtil.IsNotEmpty(tSeisakuHakouHdVo.KanseiTmMemo2) Then
                        record.ShisakuTmMemo2Label = tSeisakuHakouHdVo.KanseiTmMemo2
                    Else
                        record.ShisakuTmMemo2Label = "メモ２"
                    End If
                End If
                'ベース車の情報を取得する。
                BaseVo = eventBaseCarDao.FindShisakuEventBaseSeisakuIchiranCar(vo.ShisakuEventCode, vo.HyojijunNo)

                ''   設計展開前なら完成車情報を更新する。
                'If StringUtil.Equals(IsSekkeiTenkaiIkou, False) Then

                If StringUtil.IsEmpty(BaseVo.ShisakuSyubetu) Then    'ホワイトボディ、削除は除く
                    '製作一覧から設定
                    For Each voSeisakuKansei As TSeisakuIchiranKanseiVo In kanseiList '製作一覧完成車シートの完成車情報
                        '' 開発符号をブランクに置き換える
                        'strGousya = BaseVo.ShisakuGousya.Replace(tSeisakuHakouHdVo.KaihatsuFugo, "")
                        '試作イベントの号車から開発符号、#（完成車）、W（ＷＢ車）を取り除いて、
                        '   製作一覧の号車と比較する。（４桁未満は頭0付きで比較）
                        Dim strShisakuGousya As String = kaihatsuFugo4keta(tSeisakuHakouHdVo.KaihatsuFugo, _
                                                                           BaseVo.ShisakuGousya)

                        '2014/02/18
                        'Dim strSeisakuGousya As String = voSeisakuKansei.Gousya.PadLeft(4, "0")
                        Dim strSeisakuGousya As String = kaihatsuFugo4keta(tSeisakuHakouHdVo.KaihatsuFugo, _
                                                                           voSeisakuKansei.Gousya)

                        '製作一覧の号車が試作イベントの号車を含むなら
                        If 0 <= strShisakuGousya.IndexOf(strSeisakuGousya) Then
                            ''号車が同じなら
                            'If StringUtil.Equals(voSeisakuKansei.Gousya, strGousya) Then
                            '製作一覧から値をセット
                            record.ShisakuSyubetu = BaseVo.ShisakuSyubetu
                            record.ShisakuGousya = BaseVo.ShisakuGousya

                            record.ShisakuSyagata = voSeisakuKansei.Syasyu
                            record.ShisakuGrade = voSeisakuKansei.Grade
                            record.ShisakuShimukechiShimuke = voSeisakuKansei.Shimuke
                            'ハンドルはHDを付ける。
                            If StringUtil.Equals(voSeisakuKansei.Handoru, "L") Or _
                                StringUtil.Equals(voSeisakuKansei.Handoru, "R") Then
                                record.ShisakuHandoru = voSeisakuKansei.Handoru & "HD"
                            Else
                                record.ShisakuHandoru = voSeisakuKansei.Handoru
                            End If
                            If StringUtil.IsNotEmpty(vo.ShisakuEgKatashiki) Then
                                record.ShisakuEgKatashiki = vo.ShisakuEgKatashiki   '前回の値を設定
                            Else
                                record.ShisakuEgKatashiki = ""  '手入力
                            End If
                            record.ShisakuEgHaikiryou = voSeisakuKansei.EgHaikiryou
                            'システムには型式をセット
                            record.ShisakuEgSystem = voSeisakuKansei.EgKatashiki

                            'record.ShisakuEgKakyuuki = voSeisakuKansei.EgKakyuuki
                            '製作一覧はﾀｰﾎﾞでもっているので変換する。
                            If StringUtil.Equals(voSeisakuKansei.EgKakyuuki, "ﾀｰﾎﾞ") Then
                                record.ShisakuEgKakyuuki = "B"
                            Else
                                record.ShisakuEgKakyuuki = voSeisakuKansei.EgKakyuuki
                            End If

                            record.ShisakuEgMemo1 = voSeisakuKansei.EgEgName
                            record.ShisakuEgMemo2 = voSeisakuKansei.EgIss
                            record.ShisakuTmKudou = voSeisakuKansei.TmKudou
                            record.ShisakuTmHensokuki = voSeisakuKansei.TmHensokuki
                            If StringUtil.IsNotEmpty(vo.ShisakuTmFukuHensokuki) Then
                                record.ShisakuTmFukuHensokuki = vo.ShisakuTmFukuHensokuki   '前回の値を設定
                            Else
                                record.ShisakuTmFukuHensokuki = ""    '手入力
                            End If
                            record.ShisakuTmMemo1 = voSeisakuKansei.TmTmName
                            record.ShisakuTmMemo2 = voSeisakuKansei.TmRdGiahi
                            record.ShisakuKatashiki = voSeisakuKansei.KatashikiScd7
                            record.ShisakuShimuke = voSeisakuKansei.KatashikiShimuke
                            record.ShisakuOp = voSeisakuKansei.KatashikiOp
                            record.ShisakuGaisousyoku = voSeisakuKansei.Gaisousyoku
                            record.ShisakuGaisousyokuName = voSeisakuKansei.GaisousyokuName
                            record.ShisakuNaisousyoku = voSeisakuKansei.Naisousyoku
                            record.ShisakuNaisousyokuName = voSeisakuKansei.NaisousyokuName
                            record.ShisakuSyadaiNo = voSeisakuKansei.SyataiNo
                            record.ShisakuShiyouMokuteki = voSeisakuKansei.ShiyouMokuteki
                            record.ShisakuShikenMokuteki = voSeisakuKansei.SyuyoukakuninKoumoku
                            record.ShisakuSiyouBusyo = voSeisakuKansei.ShiyouBusyo
                            If StringUtil.IsNotEmpty(vo.ShisakuGroup) Then
                                record.ShisakuGroup = vo.ShisakuGroup   '前回の値を設定
                            Else
                                '製作グループが未入力の場合、製作一覧情報のグループを設定する。
                                record.ShisakuGroup = voSeisakuKansei.SeisakuGroup
                                'record.ShisakuGroup = ""    '手入力
                            End If
                            If StringUtil.IsNotEmpty(vo.ShisakuSeisakuJunjyo) Then
                                record.ShisakuSeisakuJunjyo = vo.ShisakuSeisakuJunjyo   '前回の値を設定
                            Else
                                record.ShisakuSeisakuJunjyo = ""    '手入力
                            End If
                            If StringUtil.IsNotEmpty(vo.ShisakuKanseibi) Then
                                record.ShisakuKanseibi = vo.ShisakuKanseibi   '前回の値を設定
                            Else
                                '日付ならセット
                                If IsDate(voSeisakuKansei.KanseiKibouBi) Then
                                    record.ShisakuKanseibi = ConvHyphenYyyymmddToYyyymmdd(voSeisakuKansei.KanseiKibouBi)
                                Else
                                    record.ShisakuKanseibi = Nothing '日付じゃないからセットしない（手入力）
                                End If
                            End If
                            If StringUtil.IsNotEmpty(vo.ShisakuKoushiNo) Then
                                record.ShisakuKoushiNo = vo.ShisakuKoushiNo   '前回の値を設定
                            Else
                                record.ShisakuKoushiNo = ""    '手入力
                            End If
                            record.ShisakuSeisakuHouhouKbn = voSeisakuKansei.SeisakuHouhouKbn
                            record.ShisakuSeisakuHouhou = voSeisakuKansei.SeisakuHouhou
                            record.ShisakuMemo = voSeisakuKansei.Memo
                            record.HyojijunNo = vo.HyojijunNo   '表示順№

                            _record.Add(vo.HyojijunNo, record)

                            Exit For
                        End If
                    Next
                ElseIf StringUtil.Equals(BaseVo.ShisakuSyubetu, "W") Then    'ホワイトボディのみ

                    '製作一覧から設定
                    For Each voSeisakuWb As TSeisakuIchiranWbVo In wbList

                        '' 開発符号をブランクに置き換える
                        'strGousya = BaseVo.ShisakuGousya.Replace(tSeisakuHakouHdVo.KaihatsuFugo, "")
                        '試作イベントの号車から開発符号、#（完成車）、W（ＷＢ車）を取り除いて、
                        '   製作一覧の号車と比較する。（４桁未満は頭0付きで比較）
                        Dim strShisakuGousya As String = kaihatsuFugo4keta(tSeisakuHakouHdVo.KaihatsuFugo, _
                                                                           BaseVo.ShisakuGousya)

                        '2014/02/18
                        'Dim strSeisakuGousya As String = voSeisakuWb.Gousya.PadLeft(4, "0")
                        Dim strSeisakuGousya As String = kaihatsuFugo4keta(tSeisakuHakouHdVo.KaihatsuFugo, _
                                                                           voSeisakuWb.Gousya)

                        '製作一覧の号車が試作イベントの号車を含むなら
                        If 0 <= strShisakuGousya.IndexOf(strSeisakuGousya) Then
                            ''号車が同じなら
                            'If StringUtil.Equals(voSeisakuWb.Gousya, strGousya) Then
                            '製作一覧から値をセット
                            record.ShisakuSyubetu = BaseVo.ShisakuSyubetu
                            record.ShisakuGousya = BaseVo.ShisakuGousya

                            record.ShisakuSyagata = voSeisakuWb.Syasyu
                            record.ShisakuGrade = voSeisakuWb.Grade
                            record.ShisakuShimukechiShimuke = voSeisakuWb.Shimuke

                            'ハンドルはHDを付ける。
                            If StringUtil.Equals(voSeisakuWb.Handoru, "L") Or _
                                StringUtil.Equals(voSeisakuWb.Handoru, "R") Then
                                record.ShisakuHandoru = voSeisakuWb.Handoru & "HD"
                            Else
                                record.ShisakuHandoru = voSeisakuWb.Handoru
                            End If

                            If StringUtil.IsNotEmpty(vo.ShisakuEgKatashiki) Then
                                record.ShisakuEgKatashiki = vo.ShisakuEgKatashiki   '前回の値を設定
                            Else
                                record.ShisakuEgKatashiki = ""  '手入力
                            End If
                            record.ShisakuEgHaikiryou = voSeisakuWb.EgHaikiryou
                            If StringUtil.IsNotEmpty(vo.ShisakuEgSystem) Then
                                record.ShisakuEgSystem = vo.ShisakuEgSystem   '前回の値を設定
                            Else
                                record.ShisakuEgSystem = ""    '手入力
                            End If
                            If StringUtil.IsNotEmpty(vo.ShisakuEgKakyuuki) Then
                                record.ShisakuEgKakyuuki = vo.ShisakuEgKakyuuki   '前回の値を設定
                            Else
                                record.ShisakuEgKakyuuki = ""    '手入力
                            End If
                            If StringUtil.IsNotEmpty(vo.ShisakuEgMemo1) Then
                                record.ShisakuEgMemo1 = vo.ShisakuEgMemo1  '前回の値を設定
                            Else
                                record.ShisakuEgMemo1 = ""    '手入力
                            End If
                            If StringUtil.IsNotEmpty(vo.ShisakuEgMemo2) Then
                                record.ShisakuEgMemo2 = vo.ShisakuEgMemo2  '前回の値を設定
                            Else
                                record.ShisakuEgMemo2 = ""    '手入力
                            End If
                            If StringUtil.IsNotEmpty(vo.ShisakuTmKudou) Then
                                record.ShisakuTmKudou = vo.ShisakuTmKudou  '前回の値を設定
                            Else
                                record.ShisakuTmKudou = ""    '手入力
                            End If
                            record.ShisakuTmHensokuki = voSeisakuWb.TmHensokuki
                            If StringUtil.IsNotEmpty(vo.ShisakuTmFukuHensokuki) Then
                                record.ShisakuTmFukuHensokuki = vo.ShisakuTmFukuHensokuki   '前回の値を設定
                            Else
                                record.ShisakuTmFukuHensokuki = ""    '手入力
                            End If
                            If StringUtil.IsNotEmpty(vo.ShisakuTmMemo1) Then
                                record.ShisakuTmMemo1 = vo.ShisakuTmMemo1  '前回の値を設定
                            Else
                                record.ShisakuTmMemo1 = ""    '手入力
                            End If
                            If StringUtil.IsNotEmpty(vo.ShisakuTmMemo2) Then
                                record.ShisakuTmMemo2 = vo.ShisakuTmMemo2  '前回の値を設定
                            Else
                                record.ShisakuTmMemo2 = ""    '手入力
                            End If
                            'record.ShisakuTmMemo1 = ""
                            'record.ShisakuTmMemo2 = ""
                            record.ShisakuKatashiki = voSeisakuWb.KatashikiScd7
                            record.ShisakuShimuke = voSeisakuWb.KatashikiShimuke
                            record.ShisakuOp = voSeisakuWb.KatashikiOp
                            record.ShisakuGaisousyoku = voSeisakuWb.Gaisousyoku
                            record.ShisakuGaisousyokuName = voSeisakuWb.GaisousyokuName
                            record.ShisakuNaisousyoku = voSeisakuWb.Naisousyoku
                            record.ShisakuNaisousyokuName = voSeisakuWb.NaisousyokuName
                            record.ShisakuSyadaiNo = voSeisakuWb.SyataiNo
                            record.ShisakuShiyouMokuteki = voSeisakuWb.ShiyouMokuteki
                            record.ShisakuShikenMokuteki = voSeisakuWb.SyuyoukakuninKoumoku
                            record.ShisakuSiyouBusyo = voSeisakuWb.ShiyouBusyo
                            If StringUtil.IsNotEmpty(vo.ShisakuGroup) Then
                                record.ShisakuGroup = vo.ShisakuGroup   '前回の値を設定
                            Else
                                '製作グループが未入力の場合、製作一覧情報のグループを設定する。
                                record.ShisakuGroup = voSeisakuWb.SeisakuGroup
                                'record.ShisakuGroup = ""    '手入力
                            End If
                            If StringUtil.IsNotEmpty(vo.ShisakuSeisakuJunjyo) Then
                                record.ShisakuSeisakuJunjyo = vo.ShisakuSeisakuJunjyo   '前回の値を設定
                            Else
                                record.ShisakuSeisakuJunjyo = ""    '手入力
                            End If
                            If StringUtil.IsNotEmpty(vo.ShisakuKanseibi) Then
                                record.ShisakuKanseibi = vo.ShisakuKanseibi   '前回の値を設定
                            Else
                                '日付ならセット
                                If IsDate(voSeisakuWb.KanseiKibouBi) Then
                                    record.ShisakuKanseibi = ConvHyphenYyyymmddToYyyymmdd(voSeisakuWb.KanseiKibouBi)
                                Else
                                    record.ShisakuKanseibi = Nothing '日付じゃないからセットしない（手入力）
                                End If
                            End If
                            If StringUtil.IsNotEmpty(vo.ShisakuKoushiNo) Then
                                record.ShisakuKoushiNo = vo.ShisakuKoushiNo   '前回の値を設定
                            Else
                                record.ShisakuKoushiNo = ""    '手入力
                            End If
                            'record.ShisakuSeisakuHouhouKbn = ""
                            'record.ShisakuSeisakuHouhou = ""
                            record.ShisakuMemo = voSeisakuWb.Memo
                            record.HyojijunNo = vo.HyojijunNo   '表示順

                            _record.Add(vo.HyojijunNo, record)

                            Exit For
                        End If
                    Next
                End If

                'Else
                'Dim i As Long = 0
                '' 開発符号をブランクに置き換える
                'strGousya = BaseVo.ShisakuGousya.Replace(tSeisakuHakouHdVo.KaihatsuFugo, "")
                'If StringUtil.IsEmpty(BaseVo.ShisakuSyubetu) Then    'ホワイトボディ、削除は除く
                '    For Each voSeisakuKansei As TSeisakuIchiranKanseiVo In kanseiList '製作一覧完成車シートの完成車情報
                '        '号車が同じなら
                '        If StringUtil.Equals(voSeisakuKansei.Gousya, strGousya) Then
                '            i = i + 1
                '            Exit For
                '        End If
                '    Next
                'End If
                'If StringUtil.Equals(BaseVo.ShisakuSyubetu, "W") Then    'ホワイトボディ、削除は除く
                '    For Each voSeisakuKansei As TSeisakuIchiranWbVo In wbList '製作一覧ＷＢ車シートのＷＢ車情報
                '        '号車が同じなら
                '        If StringUtil.Equals(voSeisakuKansei.Gousya, strGousya) Then
                '            i = i + 1
                '            Exit For
                '        End If
                '    Next
                'End If
                ''データセット
                'VoUtil.CopyProperties(vo, record)
                ''１件も無ければ削除コードをセット
                'If i = 0 Then
                '    record.ShisakuSyubetu = "D" '削除=D
                'End If
                '_record.Add(vo.HyojijunNo, record)
                'End If

                '表示順取得
                If vo.HyojijunNo > HoldRowNo Then HoldRowNo = vo.HyojijunNo
            Next

            '削除号車の処理
            '   完成車及びＷＢ車情報
            '製作一覧から設定
            For Each vo As TShisakuEventKanseiVo In vos '試作イベントの完成車情報
                'ベース車の情報を取得する。
                BaseVo = eventBaseCarDao.FindShisakuEventBaseSeisakuIchiranCar(vo.ShisakuEventCode, vo.HyojijunNo)

                Dim i As Long = 0
                '' 開発符号をブランクに置き換える
                'strGousya = BaseVo.ShisakuGousya.Replace(tSeisakuHakouHdVo.KaihatsuFugo, "")
                If StringUtil.IsEmpty(BaseVo.ShisakuSyubetu) Then    'ホワイトボディ、削除は除く
                    For Each voSeisakuKansei As TSeisakuIchiranKanseiVo In kanseiList '製作一覧完成車シートの完成車情報
                        '試作イベントの号車から開発符号、#（完成車）、W（ＷＢ車）を取り除いて、
                        '   製作一覧の号車と比較する。（４桁未満は頭0付きで比較）
                        Dim strShisakuGousya As String = kaihatsuFugo4keta(tSeisakuHakouHdVo.KaihatsuFugo, _
                                                                           BaseVo.ShisakuGousya)

                        '2014/02/18
                        'Dim strSeisakuGousya As String = voSeisakuKansei.Gousya.PadLeft(4, "0")
                        Dim strSeisakuGousya As String = kaihatsuFugo4keta(tSeisakuHakouHdVo.KaihatsuFugo, _
                                                                           voSeisakuKansei.Gousya)

                        '製作一覧の号車が試作イベントの号車を含むなら
                        If 0 <= strShisakuGousya.IndexOf(strSeisakuGousya) Then
                            ''号車が同じなら
                            'If StringUtil.Equals(voSeisakuKansei.Gousya, strGousya) Then
                            i = i + 1
                            Exit For
                        End If
                    Next
                End If
                If StringUtil.Equals(BaseVo.ShisakuSyubetu, "W") Then    'ホワイトボディ、削除は除く
                    For Each voSeisakuKansei As TSeisakuIchiranWbVo In wbList '製作一覧ＷＢ車シートのＷＢ車情報
                        '試作イベントの号車から開発符号、#（完成車）、W（ＷＢ車）を取り除いて、
                        '   製作一覧の号車と比較する。（４桁未満は頭0付きで比較）
                        Dim strShisakuGousya As String = kaihatsuFugo4keta(tSeisakuHakouHdVo.KaihatsuFugo, _
                                                                           BaseVo.ShisakuGousya)

                        '2014/02/18
                        'Dim strSeisakuGousya As String = voSeisakuKansei.Gousya.PadLeft(4, "0")
                        Dim strSeisakuGousya As String = kaihatsuFugo4keta(tSeisakuHakouHdVo.KaihatsuFugo, _
                                                                           voSeisakuKansei.Gousya)

                        '製作一覧の号車が試作イベントの号車を含むなら
                        If 0 <= strShisakuGousya.IndexOf(strSeisakuGousya) Then
                            ''号車が同じなら
                            'If StringUtil.Equals(voSeisakuKansei.Gousya, strGousya) Then
                            i = i + 1
                            Exit For
                        End If
                    Next
                End If
                '１件も無ければ
                If i = 0 Then
                    '表示順取得
                    If vo.HyojijunNo > HoldRowNo Then HoldRowNo = vo.HyojijunNo
                    Dim record As New EventEditCompleteCarVo
                    VoUtil.CopyProperties(vo, record)

                    record.ShisakuSyubetu = "D" '削除コード

                    'E/G、T/Mのメモらカラムヘッダー
                    If StringUtil.IsNotEmpty(tSeisakuHakouHdVo) Then
                        If StringUtil.IsNotEmpty(tSeisakuHakouHdVo.KanseiEgMemo1) Then
                            record.ShisakuEgMemo1Label = tSeisakuHakouHdVo.KanseiEgMemo1
                        Else
                            record.ShisakuEgMemo1Label = "メモ１"
                        End If
                        If StringUtil.IsNotEmpty(tSeisakuHakouHdVo.KanseiEgMemo2) Then
                            record.ShisakuEgMemo2Label = tSeisakuHakouHdVo.KanseiEgMemo2
                        Else
                            record.ShisakuEgMemo2Label = "メモ２"
                        End If
                        If StringUtil.IsNotEmpty(tSeisakuHakouHdVo.KanseiTmMemo1) Then
                            record.ShisakuTmMemo1Label = tSeisakuHakouHdVo.KanseiTmMemo1
                        Else
                            record.ShisakuTmMemo1Label = "メモ１"
                        End If
                        If StringUtil.IsNotEmpty(tSeisakuHakouHdVo.KanseiTmMemo2) Then
                            record.ShisakuTmMemo2Label = tSeisakuHakouHdVo.KanseiTmMemo2
                        Else
                            record.ShisakuTmMemo2Label = "メモ２"
                        End If
                    End If

                    _record.Add(vo.HyojijunNo, record)

                End If
            Next

            '設計展開校は号車の追加は不要
            ''設計展開以降か？
            ''   設計展開前ならベース情報を追加（号車追加）する。
            'If StringUtil.Equals(IsSekkeiTenkaiIkou, False) Then

            '    '完成車ベース情報の号車追加処理
            '    If HoldRowNo <> 0 Then HoldRowNo = HoldRowNo + 1

            '    '追加号車の処理
            '    For Each voSeisakuKansei As TSeisakuIchiranKanseiVo In kanseiList '製作一覧完成車の完成車情報

            '        Dim i As Long = 0
            '        'イベントから設定
            '        For Each vo As TShisakuEventKanseiVo In vos '試作イベントの完成車情報
            '            'ベース車の情報を取得する。
            '            BaseVo = eventBaseCarDao.FindShisakuEventBaseCar(shisakuEventCode, vo.HyojijunNo)
            '            '試作イベントの号車から開発符号、#（完成車）、W（ＷＢ車）を取り除いて、
            '            '   製作一覧の号車と比較する。（４桁未満は頭0付きで比較）
            '            Dim strShisakuGousya As String = kaihatsuFugo4keta(tSeisakuHakouHdVo.KaihatsuFugo, _
            '                                                               BaseVo.ShisakuGousya)
            '            Dim strSeisakuGousya As String = kaihatsuFugo4keta(tSeisakuHakouHdVo.KaihatsuFugo, _
            '                                                               voSeisakuKansei.Gousya)
            '            '' 開発符号をブランクに置き換える
            '            'strGousya = BaseVo.ShisakuGousya.Replace(tSeisakuHakouHdVo.KaihatsuFugo, "")
            '            '製作一覧の号車が試作イベントの号車を含むなら
            '            If 0 <= strShisakuGousya.IndexOf(strSeisakuGousya) Then
            '                ''号車が同じなら
            '                'If StringUtil.Equals(voSeisakuKansei.Gousya, strGousya) Then
            '                i = i + 1
            '                Exit For
            '            End If
            '        Next

            '        '該当号車が１件も無い場合は追加
            '        If i = 0 Then
            '            Dim record As New EventEditCompleteCarVo

            '            record.ShisakuSyubetu = ""
            '            '開発符号と号車を結合してセット
            '            record.ShisakuGousya = tSeisakuHakouHdVo.KaihatsuFugo & voSeisakuKansei.Gousya

            '            record.ShisakuSyagata = voSeisakuKansei.Syasyu
            '            record.ShisakuGrade = voSeisakuKansei.Grade
            '            record.ShisakuShimukechiShimuke = voSeisakuKansei.Shimuke
            '            record.ShisakuHandoru = voSeisakuKansei.Handoru
            '            record.ShisakuEgKatashiki = voSeisakuKansei.EgKatashiki
            '            record.ShisakuEgHaikiryou = voSeisakuKansei.EgHaikiryou
            '            'record.ShisakuEgSystem = ""    '手入力
            '            record.ShisakuEgKakyuuki = voSeisakuKansei.EgKakyuuki
            '            record.ShisakuEgMemo1 = voSeisakuKansei.EgEgName
            '            record.ShisakuEgMemo2 = voSeisakuKansei.EgIss
            '            record.ShisakuTmKudou = voSeisakuKansei.TmKudou
            '            record.ShisakuTmHensokuki = voSeisakuKansei.TmHensokuki
            '            'record.ShisakuTmFukuHensokuki = ""    '手入力
            '            record.ShisakuTmMemo1 = voSeisakuKansei.TmTmName
            '            record.ShisakuTmMemo2 = voSeisakuKansei.TmRdGiahi
            '            record.ShisakuKatashiki = voSeisakuKansei.KatashikiScd7
            '            record.ShisakuShimuke = voSeisakuKansei.KatashikiShimuke
            '            record.ShisakuOp = voSeisakuKansei.KatashikiOp
            '            record.ShisakuGaisousyoku = voSeisakuKansei.Gaisousyoku
            '            record.ShisakuGaisousyokuName = voSeisakuKansei.GaisousyokuName
            '            record.ShisakuNaisousyoku = voSeisakuKansei.Naisousyoku
            '            record.ShisakuNaisousyokuName = voSeisakuKansei.NaisousyokuName
            '            record.ShisakuSyadaiNo = voSeisakuKansei.SyataiNo
            '            record.ShisakuShiyouMokuteki = voSeisakuKansei.ShiyouMokuteki
            '            record.ShisakuShikenMokuteki = voSeisakuKansei.SyuyoukakuninKoumoku
            '            record.ShisakuSiyouBusyo = voSeisakuKansei.ShiyouBusyo
            '            'record.ShisakuGroup = ""    '手入力
            '            record.ShisakuSeisakuJunjyo = voSeisakuKansei.SeisakuJunjyo
            '            record.ShisakuKanseibi = ConvHyphenYyyymmddToYyyymmdd(voSeisakuKansei.KanseiKibouBi)
            '            'record.ShisakuKoushiNo = ""    '手入力
            '            record.ShisakuSeisakuHouhouKbn = voSeisakuKansei.SeisakuHouhouKbn
            '            record.ShisakuSeisakuHouhou = voSeisakuKansei.SeisakuHouhou
            '            record.ShisakuMemo = voSeisakuKansei.Memo

            '            'E/G、T/Mのメモらカラムヘッダー
            '            If StringUtil.IsNotEmpty(tSeisakuHakouHdVo) Then
            '                If StringUtil.IsNotEmpty(tSeisakuHakouHdVo.KanseiEgMemo1) Then
            '                    record.ShisakuEgMemo1Label = tSeisakuHakouHdVo.KanseiEgMemo1
            '                Else
            '                    record.ShisakuEgMemo1Label = "メモ１"
            '                End If
            '                If StringUtil.IsNotEmpty(tSeisakuHakouHdVo.KanseiEgMemo2) Then
            '                    record.ShisakuEgMemo2Label = tSeisakuHakouHdVo.KanseiEgMemo2
            '                Else
            '                    record.ShisakuEgMemo2Label = "メモ２"
            '                End If
            '                If StringUtil.IsNotEmpty(tSeisakuHakouHdVo.KanseiTmMemo1) Then
            '                    record.ShisakuTmMemo1Label = tSeisakuHakouHdVo.KanseiTmMemo1
            '                Else
            '                    record.ShisakuTmMemo1Label = "メモ１"
            '                End If
            '                If StringUtil.IsNotEmpty(tSeisakuHakouHdVo.KanseiTmMemo2) Then
            '                    record.ShisakuTmMemo2Label = tSeisakuHakouHdVo.KanseiTmMemo2
            '                Else
            '                    record.ShisakuTmMemo2Label = "メモ２"
            '                End If
            '            End If

            '            record.HyojijunNo = HoldRowNo

            '            _record.Add(HoldRowNo, record)

            '            HoldRowNo = HoldRowNo + 1

            '        End If

            '    Next

            '    'ＷＢ車ベース情報の号車追加処理
            '    '追加号車の更新
            '    For Each voSeisakuWb As TSeisakuIchiranWbVo In wbList '製作一覧のＷＢ車情報

            '        Dim i As Long = 0
            '        'イベントから設定
            '        For Each vo As TShisakuEventKanseiVo In vos '試作イベントの完成車情報
            '            'ベース車の情報を取得する。
            '            BaseVo = eventBaseCarDao.FindShisakuEventBaseCar(shisakuEventCode, vo.HyojijunNo)
            '            '' 開発符号をブランクに置き換える
            '            'strGousya = BaseVo.ShisakuGousya.Replace(tSeisakuHakouHdVo.KaihatsuFugo, "")
            '            '試作イベントの号車から開発符号、#（完成車）、W（ＷＢ車）を取り除いて、
            '            '   製作一覧の号車と比較する。（４桁未満は頭0付きで比較）
            '            Dim strShisakuGousya As String = kaihatsuFugo4keta(tSeisakuHakouHdVo.KaihatsuFugo, _
            '                                                               BaseVo.ShisakuGousya)
            '            Dim strSeisakuGousya As String = kaihatsuFugo4keta(tSeisakuHakouHdVo.KaihatsuFugo, _
            '                                                               voSeisakuWb.Gousya)
            '            '製作一覧の号車が試作イベントの号車を含むなら
            '            If 0 <= strShisakuGousya.IndexOf(strSeisakuGousya) Then
            '                ''号車が同じなら
            '                'If StringUtil.Equals(voSeisakuWb.Gousya, strGousya) Then
            '                i = i + 1
            '                Exit For
            '            End If
            '        Next

            '        '該当号車が１件も無い場合は追加
            '        If i = 0 Then
            '            Dim record As New EventEditCompleteCarVo

            '            record.ShisakuSyubetu = "W" 'ホワイトボディ
            '            '開発符号と号車を結合してセット
            '            record.ShisakuGousya = tSeisakuHakouHdVo.KaihatsuFugo & voSeisakuWb.Gousya

            '            record.ShisakuSyagata = voSeisakuWb.Syasyu
            '            record.ShisakuGrade = voSeisakuWb.Grade
            '            record.ShisakuShimukechiShimuke = voSeisakuWb.Shimuke
            '            record.ShisakuHandoru = voSeisakuWb.Handoru
            '            'record.ShisakuEgKatashiki = ""
            '            record.ShisakuEgHaikiryou = voSeisakuWb.EgHaikiryou
            '            'record.ShisakuEgSystem = ""    '手入力
            '            'record.ShisakuEgKakyuuki = ""
            '            'record.ShisakuEgMemo1 = ""
            '            'record.ShisakuEgMemo2 = ""
            '            'record.ShisakuTmKudou = ""
            '            record.ShisakuTmHensokuki = voSeisakuWb.TmHensokuki
            '            'record.ShisakuTmFukuHensokuki = ""    '手入力
            '            'record.ShisakuTmMemo1 = ""
            '            'record.ShisakuTmMemo2 = ""
            '            record.ShisakuKatashiki = voSeisakuWb.KatashikiScd7
            '            record.ShisakuShimuke = voSeisakuWb.KatashikiShimuke
            '            record.ShisakuOp = voSeisakuWb.KatashikiOp
            '            record.ShisakuGaisousyoku = voSeisakuWb.Gaisousyoku
            '            record.ShisakuGaisousyokuName = voSeisakuWb.GaisousyokuName
            '            record.ShisakuNaisousyoku = voSeisakuWb.Naisousyoku
            '            record.ShisakuNaisousyokuName = voSeisakuWb.NaisousyokuName
            '            record.ShisakuSyadaiNo = voSeisakuWb.SyataiNo
            '            record.ShisakuShiyouMokuteki = voSeisakuWb.ShiyouMokuteki
            '            record.ShisakuShikenMokuteki = voSeisakuWb.SyuyoukakuninKoumoku
            '            record.ShisakuSiyouBusyo = voSeisakuWb.ShiyouBusyo
            '            'record.ShisakuGroup = ""    '手入力
            '            record.ShisakuSeisakuJunjyo = voSeisakuWb.SeisakuJunjyo
            '            record.ShisakuKanseibi = ConvHyphenYyyymmddToYyyymmdd(voSeisakuWb.KanseiKibouBi)
            '            'record.ShisakuKoushiNo = ""    '手入力
            '            'record.ShisakuSeisakuHouhouKbn = ""
            '            'record.ShisakuSeisakuHouhou = ""
            '            record.ShisakuMemo = voSeisakuWb.Memo
            '            record.HyojijunNo = voSeisakuWb.HyojijunNo   '表示順

            '            'E/G、T/Mのメモらカラムヘッダー
            '            If StringUtil.IsNotEmpty(tSeisakuHakouHdVo) Then
            '                If StringUtil.IsNotEmpty(tSeisakuHakouHdVo.KanseiEgMemo1) Then
            '                    record.ShisakuEgMemo1Label = tSeisakuHakouHdVo.KanseiEgMemo1
            '                Else
            '                    record.ShisakuEgMemo1Label = "メモ１"
            '                End If
            '                If StringUtil.IsNotEmpty(tSeisakuHakouHdVo.KanseiEgMemo2) Then
            '                    record.ShisakuEgMemo2Label = tSeisakuHakouHdVo.KanseiEgMemo2
            '                Else
            '                    record.ShisakuEgMemo2Label = "メモ２"
            '                End If
            '                If StringUtil.IsNotEmpty(tSeisakuHakouHdVo.KanseiTmMemo1) Then
            '                    record.ShisakuTmMemo1Label = tSeisakuHakouHdVo.KanseiTmMemo1
            '                Else
            '                    record.ShisakuTmMemo1Label = "メモ１"
            '                End If
            '                If StringUtil.IsNotEmpty(tSeisakuHakouHdVo.KanseiTmMemo2) Then
            '                    record.ShisakuTmMemo2Label = tSeisakuHakouHdVo.KanseiTmMemo2
            '                Else
            '                    record.ShisakuTmMemo2Label = "メモ２"
            '                End If
            '            End If

            '            _record.Add(HoldRowNo, record)

            '            HoldRowNo = HoldRowNo + 1

            '        End If

            '    Next

            'End If

        End Sub

#End Region

#Region "公開プロパティ"
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
        ' 設計展開以降か？
        Private _isSekkeiTenkaiIkou As Boolean
        ''' <summary>設計展開以降か？</summary>
        ''' <value>設計展開以降か？</value>
        ''' <returns>設計展開以降か？</returns>
        Public ReadOnly Property IsSekkeiTenkaiIkou() As Boolean
            Get
                Return _isSekkeiTenkaiIkou
            End Get
        End Property
#End Region

        Private Function IsAddMode() As Boolean
            Return StringUtil.IsEmpty(shisakuEventCode)
        End Function

        ''' <summary>
        ''' イベント情報コピー処理時の初期化など
        ''' </summary>
        ''' <param name="shisakuEventCode">元試作イベントコード</param>
        ''' <remarks></remarks>
        Friend Sub ProcessPostCopy(ByVal shisakuEventCode As String)
            Me.shisakuEventCode = shisakuEventCode
            ' 自身が登録ユーザーになるようにクリア
            For Each rowNo As Integer In GetInputRowNos()
                Records(rowNo).CreatedUserId = Nothing
                Records(rowNo).CreatedDate = Nothing
                Records(rowNo).CreatedTime = Nothing
            Next
        End Sub

        ''' <summary>
        ''' 編集モードかを返す
        ''' </summary>
        ''' <param name="rowNo">行No</param>
        ''' <returns>編集モードなら、true</returns>
        ''' <remarks></remarks>
        Public Function IsEditModes(ByVal rowNo As Integer) As Boolean
            Return Not IsViewerMode _
                AndAlso (Not StringUtil.IsEmpty(ShisakuSyubetu(rowNo)) OrElse Not StringUtil.IsEmpty(ShisakuGousya(rowNo)))
        End Function

        Public Sub Register(ByVal newShisakuEventCode As String)
            RegisterMain(newShisakuEventCode, True)
        End Sub
        Public Sub RegisterKaitei(ByVal newShisakuEventCode As String)
            RegisterMainKaitei(newShisakuEventCode, True)
        End Sub

        Public Sub Save(ByVal newShisakuEventCode As String)
            RegisterMain(newShisakuEventCode, False)
        End Sub
        Private Sub RegisterMain(ByVal newShisakuEventCode As String, ByVal IsRegister As Boolean)

            '-------------------------------------------------------------------------------------------------
            ' ２次改修分　変更箇所を登録する。
            '試作イベント完成車情報を取得
            Dim strKanseibi As String '完成日はNullの時があるので
            Dim strKanseibiDisp As String '完成日はNullの時があるので
            Dim KanseiList As List(Of TShisakuEventKanseiVo)
            Dim eventCompleteCarDao As EventEditCompleteCarRirekiDao = New EventEditCompleteCarRirekiDaoImpl()
            KanseiList = eventCompleteCarDao.GetShisakuEventCompleteCarList(newShisakuEventCode)

            Dim KanseiVo As TShisakuEventKanseiKaiteiVo

            '設計展開以降か？そうなら変更点を作成する。
            '   true:設計展開以降、false:設計展開前
            '   IsRegisterがTrue：登録ボタン押下、IsRegisterがFalse：保存ボタン押下
            If IsSekkeiTenkaiIkou() And StringUtil.Equals(IsRegister, True) Then

                For Each key As Integer In _record.Keys
                    Dim dispValue As TShisakuEventKanseiVo = _record.Value(key)

                    ''2012/02/21 号車がNothingの列はインサートしない
                    ''試作イベントコードと表示順（key）からBASEを取得し、無ければインサートしない
                    Dim shisakuEventBaseDao As New TShisakuEventBaseSeisakuIchiranDaoImpl
                    Dim eventBaseVo As TShisakuEventBaseSeisakuIchiranVo = shisakuEventBaseDao.FindByPk(newShisakuEventCode, key)
                    If eventBaseVo Is Nothing Then
                        Continue For
                    End If

                    'DB情報を取得する。
                    KanseiVo = eventCompleteCarDao.FindShisakuEventCompleteCarKaitei(newShisakuEventCode, key)
                    '値があればDB情報と画面情報を比較する。
                    If StringUtil.IsNotEmpty(KanseiVo) Then
                        '車型
                        If KanseiVo.ShisakuSyagata <> dispValue.ShisakuSyagata Then
                            'Nothingならブランクをセット
                            Dim strShisakuSyagata As String
                            If KanseiVo.ShisakuSyagata Is Nothing Then
                                strShisakuSyagata = ""
                            Else
                                strShisakuSyagata = KanseiVo.ShisakuSyagata
                            End If
                            Dim strShisakuSyagata2 As String
                            If dispValue.ShisakuSyagata Is Nothing Then
                                strShisakuSyagata2 = ""
                            Else
                                strShisakuSyagata2 = dispValue.ShisakuSyagata
                            End If
                            '変更情報を作成
                            eventCompleteCarDao.InsertShisakuEventCompleteCar(newShisakuEventCode, key, _
                                                                              ShisakuSoubiHyoujiJun.KANSEI_SYAGATA, _
                                                                              "TAG_SHAGATA", _
                                                                              strShisakuSyagata, _
                                                                              strShisakuSyagata2)
                        End If
                        'グレード
                        If KanseiVo.ShisakuGrade <> dispValue.ShisakuGrade Then
                            'Nothingならブランクをセット
                            Dim strShisakuGrade As String
                            If KanseiVo.ShisakuGrade Is Nothing Then
                                strShisakuGrade = ""
                            Else
                                strShisakuGrade = KanseiVo.ShisakuGrade
                            End If
                            Dim strShisakuGrade2 As String
                            If dispValue.ShisakuGrade Is Nothing Then
                                strShisakuGrade2 = ""
                            Else
                                strShisakuGrade2 = dispValue.ShisakuGrade
                            End If
                            '変更情報を作成
                            eventCompleteCarDao.InsertShisakuEventCompleteCar(newShisakuEventCode, key, _
                                                                              ShisakuSoubiHyoujiJun.KANSEI_GRADE, _
                                                                              "TAG_GRADE", _
                                                                              strShisakuGrade, _
                                                                              strShisakuGrade2)
                        End If
                        '仕向地・仕向け
                        If KanseiVo.ShisakuShimukechiShimuke <> dispValue.ShisakuShimukechiShimuke Then
                            'Nothingならブランクをセット
                            Dim strShisakuShimukechiShimuke As String
                            If KanseiVo.ShisakuShimukechiShimuke Is Nothing Then
                                strShisakuShimukechiShimuke = ""
                            Else
                                strShisakuShimukechiShimuke = KanseiVo.ShisakuShimukechiShimuke
                            End If
                            Dim strShisakuShimukechiShimuke2 As String
                            If dispValue.ShisakuShimukechiShimuke Is Nothing Then
                                strShisakuShimukechiShimuke2 = ""
                            Else
                                strShisakuShimukechiShimuke2 = dispValue.ShisakuShimukechiShimuke
                            End If
                            '変更情報を作成
                            eventCompleteCarDao.InsertShisakuEventCompleteCar(newShisakuEventCode, key, _
                                                                              ShisakuSoubiHyoujiJun.KANSEI_SHISAKU_SHIMUKECHI_SHIMUKE, _
                                                                              "TAG_SHIMUKECHI_SHIMUKE", _
                                                                              strShisakuShimukechiShimuke, _
                                                                              strShisakuShimukechiShimuke2)
                        End If
                        'ハンドル
                        If KanseiVo.ShisakuHandoru <> dispValue.ShisakuHandoru Then
                            'Nothingならブランクをセット
                            Dim strShisakuHandoru As String
                            If KanseiVo.ShisakuHandoru Is Nothing Then
                                strShisakuHandoru = ""
                            Else
                                strShisakuHandoru = KanseiVo.ShisakuHandoru
                            End If
                            Dim strShisakuHandoru2 As String
                            If dispValue.ShisakuHandoru Is Nothing Then
                                strShisakuHandoru2 = ""
                            Else
                                strShisakuHandoru2 = dispValue.ShisakuHandoru
                            End If
                            '変更情報を作成
                            eventCompleteCarDao.InsertShisakuEventCompleteCar(newShisakuEventCode, key, _
                                                                              ShisakuSoubiHyoujiJun.KANSEI_HANDORU, _
                                                                              "TAG_HANDLE", _
                                                                              strShisakuHandoru, _
                                                                              strShisakuHandoru2)
                        End If
                        'E/G型式
                        If KanseiVo.ShisakuEgKatashiki <> dispValue.ShisakuEgKatashiki Then
                            'Nothingならブランクをセット
                            Dim strShisakuEgKatashiki As String
                            If KanseiVo.ShisakuEgKatashiki Is Nothing Then
                                strShisakuEgKatashiki = ""
                            Else
                                strShisakuEgKatashiki = KanseiVo.ShisakuEgKatashiki
                            End If
                            Dim strShisakuEgKatashiki2 As String
                            If dispValue.ShisakuEgKatashiki Is Nothing Then
                                strShisakuEgKatashiki2 = ""
                            Else
                                strShisakuEgKatashiki2 = dispValue.ShisakuEgKatashiki
                            End If
                            '変更情報を作成
                            eventCompleteCarDao.InsertShisakuEventCompleteCar(newShisakuEventCode, key, _
                                                                              ShisakuSoubiHyoujiJun.KANSEI_EG_KATASHIKI, _
                                                                              "TAG_EG_KATASHIKI", _
                                                                              strShisakuEgKatashiki, _
                                                                              strShisakuEgKatashiki2)
                        End If
                        'E/G排気量
                        If KanseiVo.ShisakuEgHaikiryou <> dispValue.ShisakuEgHaikiryou Then
                            'Nothingならブランクをセット
                            Dim strShisakuEgHaikiryou As String
                            If KanseiVo.ShisakuEgHaikiryou Is Nothing Then
                                strShisakuEgHaikiryou = ""
                            Else
                                strShisakuEgHaikiryou = KanseiVo.ShisakuEgHaikiryou
                            End If
                            Dim strShisakuEgHaikiryou2 As String
                            If dispValue.ShisakuEgHaikiryou Is Nothing Then
                                strShisakuEgHaikiryou2 = ""
                            Else
                                strShisakuEgHaikiryou2 = dispValue.ShisakuEgHaikiryou
                            End If
                            '変更情報を作成
                            eventCompleteCarDao.InsertShisakuEventCompleteCar(newShisakuEventCode, key, _
                                                                              ShisakuSoubiHyoujiJun.KANSEI_EG_HAIKIRYOU, _
                                                                              "TAG_EG_HAIKIRYO", _
                                                                              strShisakuEgHaikiryou, _
                                                                              strShisakuEgHaikiryou2)
                        End If
                        'E/Gシステム
                        If KanseiVo.ShisakuEgSystem <> dispValue.ShisakuEgSystem Then
                            'Nothingならブランクをセット
                            Dim strShisakuEgSystem As String
                            If KanseiVo.ShisakuEgSystem Is Nothing Then
                                strShisakuEgSystem = ""
                            Else
                                strShisakuEgSystem = KanseiVo.ShisakuEgSystem
                            End If
                            Dim strShisakuEgSystem2 As String
                            If dispValue.ShisakuEgSystem Is Nothing Then
                                strShisakuEgSystem2 = ""
                            Else
                                strShisakuEgSystem2 = dispValue.ShisakuEgSystem
                            End If
                            '変更情報を作成
                            eventCompleteCarDao.InsertShisakuEventCompleteCar(newShisakuEventCode, key, _
                                                                              ShisakuSoubiHyoujiJun.KANSEI_EG_SYSTEM, _
                                                                              "TAG_EG_SYSTEM", _
                                                                              strShisakuEgSystem, _
                                                                              strShisakuEgSystem2)
                        End If
                        'E/G過給機
                        If KanseiVo.ShisakuEgKakyuuki <> dispValue.ShisakuEgKakyuuki Then
                            'Nothingならブランクをセット
                            Dim strShisakuEgKakyuuki As String
                            If KanseiVo.ShisakuEgKakyuuki Is Nothing Then
                                strShisakuEgKakyuuki = ""
                            Else
                                strShisakuEgKakyuuki = KanseiVo.ShisakuEgKakyuuki
                            End If
                            Dim strShisakuEgKakyuuki2 As String
                            If dispValue.ShisakuEgKakyuuki Is Nothing Then
                                strShisakuEgKakyuuki2 = ""
                            Else
                                strShisakuEgKakyuuki2 = dispValue.ShisakuEgKakyuuki
                            End If
                            '変更情報を作成
                            eventCompleteCarDao.InsertShisakuEventCompleteCar(newShisakuEventCode, key, _
                                                                              ShisakuSoubiHyoujiJun.KANSEI_EG_KAKYUUKI, _
                                                                              "TAG_EG_KAKYUKI", _
                                                                              strShisakuEgKakyuuki, _
                                                                              strShisakuEgKakyuuki2)
                        End If
                        'E/Gメモ１
                        If KanseiVo.ShisakuEgMemo1 <> dispValue.ShisakuEgMemo1 Then
                            'Nothingならブランクをセット
                            Dim strShisakuEgMemo1 As String
                            If KanseiVo.ShisakuEgMemo1 Is Nothing Then
                                strShisakuEgMemo1 = ""
                            Else
                                strShisakuEgMemo1 = KanseiVo.ShisakuEgMemo1
                            End If
                            Dim strShisakuEgMemo12 As String
                            If dispValue.ShisakuEgMemo1 Is Nothing Then
                                strShisakuEgMemo12 = ""
                            Else
                                strShisakuEgMemo12 = dispValue.ShisakuEgMemo1
                            End If
                            '変更情報を作成
                            eventCompleteCarDao.InsertShisakuEventCompleteCar(newShisakuEventCode, key, _
                                                                              ShisakuSoubiHyoujiJun.KANSEI_EG_MEMO1, _
                                                                              "TAG_EG_MEMO_1", _
                                                                              strShisakuEgMemo1, _
                                                                              strShisakuEgMemo12)
                        End If
                        'E/Gメモ２
                        If KanseiVo.ShisakuEgMemo2 <> dispValue.ShisakuEgMemo2 Then
                            'Nothingならブランクをセット
                            Dim strShisakuEgMemo2 As String
                            If KanseiVo.ShisakuEgMemo2 Is Nothing Then
                                strShisakuEgMemo2 = ""
                            Else
                                strShisakuEgMemo2 = KanseiVo.ShisakuEgMemo2
                            End If
                            Dim strShisakuEgMemo22 As String
                            If dispValue.ShisakuEgMemo2 Is Nothing Then
                                strShisakuEgMemo22 = ""
                            Else
                                strShisakuEgMemo22 = dispValue.ShisakuEgMemo2
                            End If
                            '変更情報を作成
                            eventCompleteCarDao.InsertShisakuEventCompleteCar(newShisakuEventCode, key, _
                                                                              ShisakuSoubiHyoujiJun.KANSEI_EG_MEMO2, _
                                                                              "TAG_EG_MEMO_2", _
                                                                              strShisakuEgMemo2, _
                                                                              strShisakuEgMemo22)
                        End If
                        'T/M駆動
                        If KanseiVo.ShisakuTmKudou <> dispValue.ShisakuTmKudou Then
                            'Nothingならブランクをセット
                            Dim strShisakuTmKudou As String
                            If KanseiVo.ShisakuTmKudou Is Nothing Then
                                strShisakuTmKudou = ""
                            Else
                                strShisakuTmKudou = KanseiVo.ShisakuTmKudou
                            End If
                            Dim strShisakuTmKudou2 As String
                            If dispValue.ShisakuTmKudou Is Nothing Then
                                strShisakuTmKudou2 = ""
                            Else
                                strShisakuTmKudou2 = dispValue.ShisakuTmKudou
                            End If
                            '変更情報を作成
                            eventCompleteCarDao.InsertShisakuEventCompleteCar(newShisakuEventCode, key, _
                                                                              ShisakuSoubiHyoujiJun.KANSEI_TM_KUDOU, _
                                                                              "TAG_TM_KUDO", _
                                                                              strShisakuTmKudou, _
                                                                              strShisakuTmKudou2)
                        End If
                        'T/M変速機
                        If KanseiVo.ShisakuTmHensokuki <> dispValue.ShisakuTmHensokuki Then
                            'Nothingならブランクをセット
                            Dim strShisakuTmHensokuki As String
                            If KanseiVo.ShisakuTmHensokuki Is Nothing Then
                                strShisakuTmHensokuki = ""
                            Else
                                strShisakuTmHensokuki = KanseiVo.ShisakuTmHensokuki
                            End If
                            Dim strShisakuTmHensokuki2 As String
                            If dispValue.ShisakuTmHensokuki Is Nothing Then
                                strShisakuTmHensokuki2 = ""
                            Else
                                strShisakuTmHensokuki2 = dispValue.ShisakuTmHensokuki
                            End If
                            '変更情報を作成
                            eventCompleteCarDao.InsertShisakuEventCompleteCar(newShisakuEventCode, key, _
                                                                              ShisakuSoubiHyoujiJun.KANSEI_TM_HENSOKUKI, _
                                                                              "TAG_TM_HENSOKUKI", _
                                                                              strShisakuTmHensokuki, _
                                                                              strShisakuTmHensokuki2)
                        End If
                        'T/M副変速機
                        If KanseiVo.ShisakuTmFukuHensokuki <> dispValue.ShisakuTmFukuHensokuki Then
                            'Nothingならブランクをセット
                            Dim strShisakuTmFukuHensokuki As String
                            If KanseiVo.ShisakuTmFukuHensokuki Is Nothing Then
                                strShisakuTmFukuHensokuki = ""
                            Else
                                strShisakuTmFukuHensokuki = KanseiVo.ShisakuTmFukuHensokuki
                            End If
                            Dim strShisakuTmFukuHensokuki2 As String
                            If dispValue.ShisakuTmFukuHensokuki Is Nothing Then
                                strShisakuTmFukuHensokuki2 = ""
                            Else
                                strShisakuTmFukuHensokuki2 = dispValue.ShisakuTmFukuHensokuki
                            End If
                            '変更情報を作成
                            eventCompleteCarDao.InsertShisakuEventCompleteCar(newShisakuEventCode, key, _
                                                                              ShisakuSoubiHyoujiJun.KANSEI_TM_FUKU_HENSOKUKI, _
                                                                              "TAG_TM_FUKU_HENSOKUKI", _
                                                                              strShisakuTmFukuHensokuki, _
                                                                              strShisakuTmFukuHensokuki2)
                        End If
                        'T/Mメモ１
                        If KanseiVo.ShisakuTmMemo1 <> dispValue.ShisakuTmMemo1 Then
                            'Nothingならブランクをセット
                            Dim strShisakuTmMemo1 As String
                            If KanseiVo.ShisakuTmMemo1 Is Nothing Then
                                strShisakuTmMemo1 = ""
                            Else
                                strShisakuTmMemo1 = KanseiVo.ShisakuTmMemo1
                            End If
                            Dim strShisakuTmMemo12 As String
                            If dispValue.ShisakuTmMemo1 Is Nothing Then
                                strShisakuTmMemo12 = ""
                            Else
                                strShisakuTmMemo12 = dispValue.ShisakuTmMemo1
                            End If
                            '変更情報を作成
                            eventCompleteCarDao.InsertShisakuEventCompleteCar(newShisakuEventCode, key, _
                                                                              ShisakuSoubiHyoujiJun.KANSEI_TM_MEMO1, _
                                                                              "TAG_TM_MEMO_1", _
                                                                              strShisakuTmMemo1, _
                                                                              strShisakuTmMemo12)
                        End If
                        'T/Mメモ２
                        If KanseiVo.ShisakuTmMemo2 <> dispValue.ShisakuTmMemo2 Then
                            'Nothingならブランクをセット
                            Dim strShisakuTmMemo2 As String
                            If KanseiVo.ShisakuTmMemo2 Is Nothing Then
                                strShisakuTmMemo2 = ""
                            Else
                                strShisakuTmMemo2 = KanseiVo.ShisakuTmMemo2
                            End If
                            Dim strShisakuTmMemo22 As String
                            If dispValue.ShisakuTmMemo2 Is Nothing Then
                                strShisakuTmMemo22 = ""
                            Else
                                strShisakuTmMemo22 = dispValue.ShisakuTmMemo2
                            End If
                            '変更情報を作成
                            eventCompleteCarDao.InsertShisakuEventCompleteCar(newShisakuEventCode, key, _
                                                                              ShisakuSoubiHyoujiJun.KANSEI_TM_MEMO2, _
                                                                              "TAG_TM_MEMO_2", _
                                                                              strShisakuTmMemo2, _
                                                                              strShisakuTmMemo22)
                        End If
                        '型式
                        If KanseiVo.ShisakuKatashiki <> dispValue.ShisakuKatashiki Then
                            'Nothingならブランクをセット
                            Dim strShisakuKatashiki As String
                            If KanseiVo.ShisakuKatashiki Is Nothing Then
                                strShisakuKatashiki = ""
                            Else
                                strShisakuKatashiki = KanseiVo.ShisakuKatashiki
                            End If
                            Dim strShisakuKatashiki2 As String
                            If dispValue.ShisakuKatashiki Is Nothing Then
                                strShisakuKatashiki2 = ""
                            Else
                                strShisakuKatashiki2 = dispValue.ShisakuKatashiki
                            End If
                            '変更情報を作成
                            eventCompleteCarDao.InsertShisakuEventCompleteCar(newShisakuEventCode, key, _
                                                                              ShisakuSoubiHyoujiJun.KANSEI_KATASHIKI, _
                                                                              "TAG_KATASHIKI", _
                                                                              strShisakuKatashiki, _
                                                                              strShisakuKatashiki2)
                        End If
                        '仕向け
                        If KanseiVo.ShisakuShimuke <> dispValue.ShisakuShimuke Then
                            'Nothingならブランクをセット
                            Dim strShisakuShimuke As String
                            If KanseiVo.ShisakuShimuke Is Nothing Then
                                strShisakuShimuke = ""
                            Else
                                strShisakuShimuke = KanseiVo.ShisakuShimuke
                            End If
                            Dim strShisakuShimuke2 As String
                            If dispValue.ShisakuShimuke Is Nothing Then
                                strShisakuShimuke2 = ""
                            Else
                                strShisakuShimuke2 = dispValue.ShisakuShimuke
                            End If
                            '変更情報を作成
                            eventCompleteCarDao.InsertShisakuEventCompleteCar(newShisakuEventCode, key, _
                                                                              ShisakuSoubiHyoujiJun.KANSEI_SHIMUKE, _
                                                                              "TAG_SHIMUKE", _
                                                                              strShisakuShimuke, _
                                                                              strShisakuShimuke2)
                        End If
                        'ＯＰ
                        If KanseiVo.ShisakuOp <> dispValue.ShisakuOp Then
                            'Nothingならブランクをセット
                            Dim strShisakuOp As String
                            If KanseiVo.ShisakuOp Is Nothing Then
                                strShisakuOp = ""
                            Else
                                strShisakuOp = KanseiVo.ShisakuOp
                            End If
                            Dim strShisakuOp2 As String
                            If dispValue.ShisakuOp Is Nothing Then
                                strShisakuOp2 = ""
                            Else
                                strShisakuOp2 = dispValue.ShisakuOp
                            End If
                            '変更情報を作成
                            eventCompleteCarDao.InsertShisakuEventCompleteCar(newShisakuEventCode, key, _
                                                                              ShisakuSoubiHyoujiJun.KANSEI_OP, _
                                                                              "TAG_OP", _
                                                                              strShisakuOp, _
                                                                              strShisakuOp2)
                        End If
                        '外装色
                        If KanseiVo.ShisakuGaisousyoku <> dispValue.ShisakuGaisousyoku Then
                            'Nothingならブランクをセット
                            Dim strShisakuGaisousyoku As String
                            If KanseiVo.ShisakuGaisousyoku Is Nothing Then
                                strShisakuGaisousyoku = ""
                            Else
                                strShisakuGaisousyoku = KanseiVo.ShisakuGaisousyoku
                            End If
                            Dim strShisakuGaisousyoku2 As String
                            If dispValue.ShisakuGaisousyoku Is Nothing Then
                                strShisakuGaisousyoku2 = ""
                            Else
                                strShisakuGaisousyoku2 = dispValue.ShisakuGaisousyoku
                            End If
                            '変更情報を作成
                            eventCompleteCarDao.InsertShisakuEventCompleteCar(newShisakuEventCode, key, _
                                                                              ShisakuSoubiHyoujiJun.KANSEI_GAISOUSYOKU, _
                                                                              "TAG_GAISO_SHOKU", _
                                                                              strShisakuGaisousyoku, _
                                                                              strShisakuGaisousyoku2)
                        End If
                        '外装色名
                        If KanseiVo.ShisakuGaisousyokuName <> dispValue.ShisakuGaisousyokuName Then
                            'Nothingならブランクをセット
                            Dim strShisakuGaisousyokuName As String
                            If KanseiVo.ShisakuGaisousyokuName Is Nothing Then
                                strShisakuGaisousyokuName = ""
                            Else
                                strShisakuGaisousyokuName = KanseiVo.ShisakuGaisousyokuName
                            End If
                            Dim strShisakuGaisousyokuName2 As String
                            If dispValue.ShisakuGaisousyokuName Is Nothing Then
                                strShisakuGaisousyokuName2 = ""
                            Else
                                strShisakuGaisousyokuName2 = dispValue.ShisakuGaisousyokuName
                            End If
                            '変更情報を作成
                            eventCompleteCarDao.InsertShisakuEventCompleteCar(newShisakuEventCode, key, _
                                                                              ShisakuSoubiHyoujiJun.KANSEI_GAISOUSYOKU_NAME, _
                                                                              "TAG_GAISO_SHOKU", _
                                                                              strShisakuGaisousyokuName, _
                                                                              strShisakuGaisousyokuName2)
                        End If
                        '内装色
                        If KanseiVo.ShisakuNaisousyoku <> dispValue.ShisakuNaisousyoku Then
                            'Nothingならブランクをセット
                            Dim strShisakuNaisousyoku As String
                            If KanseiVo.ShisakuNaisousyoku Is Nothing Then
                                strShisakuNaisousyoku = ""
                            Else
                                strShisakuNaisousyoku = KanseiVo.ShisakuNaisousyoku
                            End If
                            Dim strShisakuNaisousyoku2 As String
                            If dispValue.ShisakuNaisousyoku Is Nothing Then
                                strShisakuNaisousyoku2 = ""
                            Else
                                strShisakuNaisousyoku2 = dispValue.ShisakuNaisousyoku
                            End If
                            '変更情報を作成
                            eventCompleteCarDao.InsertShisakuEventCompleteCar(newShisakuEventCode, key, _
                                                                              ShisakuSoubiHyoujiJun.KANSEI_NAISOUSYOKU, _
                                                                              "TAG_NAISO_SHOKU_NAME", _
                                                                              strShisakuNaisousyoku, _
                                                                              strShisakuNaisousyoku2)
                        End If
                        '内装色名
                        If KanseiVo.ShisakuNaisousyokuName <> dispValue.ShisakuNaisousyokuName Then
                            'Nothingならブランクをセット
                            Dim strShisakuNaisousyokuName As String
                            If KanseiVo.ShisakuNaisousyokuName Is Nothing Then
                                strShisakuNaisousyokuName = ""
                            Else
                                strShisakuNaisousyokuName = KanseiVo.ShisakuNaisousyokuName
                            End If
                            Dim strShisakuNaisousyokuName2 As String
                            If dispValue.ShisakuNaisousyokuName Is Nothing Then
                                strShisakuNaisousyokuName2 = ""
                            Else
                                strShisakuNaisousyokuName2 = dispValue.ShisakuNaisousyokuName
                            End If
                            '変更情報を作成
                            eventCompleteCarDao.InsertShisakuEventCompleteCar(newShisakuEventCode, key, _
                                                                              ShisakuSoubiHyoujiJun.KANSEI_NAISOUSYOKU_NAME, _
                                                                              "TAG_NAISO_SHOKU_NAME", _
                                                                              strShisakuNaisousyokuName, _
                                                                              strShisakuNaisousyokuName2)
                        End If
                        '車台№
                        If KanseiVo.ShisakuSyadaiNo <> dispValue.ShisakuSyadaiNo Then
                            'Nothingならブランクをセット
                            Dim strShisakuSyadaiNo As String
                            If KanseiVo.ShisakuSyadaiNo Is Nothing Then
                                strShisakuSyadaiNo = ""
                            Else
                                strShisakuSyadaiNo = KanseiVo.ShisakuSyadaiNo
                            End If
                            Dim strShisakuSyadaiNo2 As String
                            If dispValue.ShisakuSyadaiNo Is Nothing Then
                                strShisakuSyadaiNo2 = ""
                            Else
                                strShisakuSyadaiNo2 = dispValue.ShisakuSyadaiNo
                            End If
                            '変更情報を作成
                            eventCompleteCarDao.InsertShisakuEventCompleteCar(newShisakuEventCode, key, _
                                                                              ShisakuSoubiHyoujiJun.KANSEI_SYADAI_NO, _
                                                                              "TAG_SHADAI_NO", _
                                                                              strShisakuSyadaiNo, _
                                                                              strShisakuSyadaiNo2)
                        End If
                        '使用目的
                        If KanseiVo.ShisakuShiyouMokuteki <> dispValue.ShisakuShiyouMokuteki Then
                            'Nothingならブランクをセット
                            Dim strShisakuShiyouMokuteki As String
                            If KanseiVo.ShisakuShiyouMokuteki Is Nothing Then
                                strShisakuShiyouMokuteki = ""
                            Else
                                strShisakuShiyouMokuteki = KanseiVo.ShisakuShiyouMokuteki
                            End If
                            Dim strShisakuShiyouMokuteki2 As String
                            If dispValue.ShisakuShiyouMokuteki Is Nothing Then
                                strShisakuShiyouMokuteki2 = ""
                            Else
                                strShisakuShiyouMokuteki2 = dispValue.ShisakuShiyouMokuteki
                            End If
                            '変更情報を作成
                            eventCompleteCarDao.InsertShisakuEventCompleteCar(newShisakuEventCode, key, _
                                                                              ShisakuSoubiHyoujiJun.KANSEI_SHIYOU_MOKUTEKI, _
                                                                              "TAG_SHIYOU_MOKUTEKI", _
                                                                              strShisakuShiyouMokuteki, _
                                                                              strShisakuShiyouMokuteki2)
                        End If
                        '主要確認項目
                        If KanseiVo.ShisakuShikenMokuteki <> dispValue.ShisakuShikenMokuteki Then
                            'Nothingならブランクをセット
                            Dim strShisakuShikenMokuteki As String
                            If KanseiVo.ShisakuShikenMokuteki Is Nothing Then
                                strShisakuShikenMokuteki = ""
                            Else
                                strShisakuShikenMokuteki = KanseiVo.ShisakuShikenMokuteki
                            End If
                            Dim strShisakuShikenMokuteki2 As String
                            If dispValue.ShisakuShikenMokuteki Is Nothing Then
                                strShisakuShikenMokuteki2 = ""
                            Else
                                strShisakuShikenMokuteki2 = dispValue.ShisakuShikenMokuteki
                            End If
                            '変更情報を作成
                            eventCompleteCarDao.InsertShisakuEventCompleteCar(newShisakuEventCode, key, _
                                                                              ShisakuSoubiHyoujiJun.KANSEI_SHIKEN_MOKUTEKI, _
                                                                              "TAG_SHIKEN_MOKUTEKI", _
                                                                              strShisakuShikenMokuteki, _
                                                                              strShisakuShikenMokuteki2)
                        End If
                        '使用部署
                        If KanseiVo.ShisakuSiyouBusyo <> dispValue.ShisakuSiyouBusyo Then
                            'Nothingならブランクをセット
                            Dim strShisakuSiyouBusyo As String
                            If KanseiVo.ShisakuSiyouBusyo Is Nothing Then
                                strShisakuSiyouBusyo = ""
                            Else
                                strShisakuSiyouBusyo = KanseiVo.ShisakuSiyouBusyo
                            End If
                            Dim strShisakuSiyouBusyo2 As String
                            If dispValue.ShisakuSiyouBusyo Is Nothing Then
                                strShisakuSiyouBusyo2 = ""
                            Else
                                strShisakuSiyouBusyo2 = dispValue.ShisakuSiyouBusyo
                            End If
                            '変更情報を作成
                            eventCompleteCarDao.InsertShisakuEventCompleteCar(newShisakuEventCode, key, _
                                                                              ShisakuSoubiHyoujiJun.KANSEI_SIYOU_BUSYO, _
                                                                              "TAG_SHIYO_BUSHO", _
                                                                              strShisakuSiyouBusyo, _
                                                                              strShisakuSiyouBusyo2)
                        End If
                        'グループ
                        If KanseiVo.ShisakuGroup <> dispValue.ShisakuGroup Then
                            'Nothingならブランクをセット
                            Dim strShisakuGroup As String
                            If KanseiVo.ShisakuGroup Is Nothing Then
                                strShisakuGroup = ""
                            Else
                                strShisakuGroup = KanseiVo.ShisakuGroup
                            End If
                            Dim strShisakuGroup2 As String
                            If dispValue.ShisakuGroup Is Nothing Then
                                strShisakuGroup2 = ""
                            Else
                                strShisakuGroup2 = dispValue.ShisakuGroup
                            End If
                            '変更情報を作成
                            eventCompleteCarDao.InsertShisakuEventCompleteCar(newShisakuEventCode, key, _
                                                                              ShisakuSoubiHyoujiJun.KANSEI_GROUP, _
                                                                              "TAG_GROUP", _
                                                                              strShisakuGroup, _
                                                                              strShisakuGroup2)
                        End If
                        '製作順序
                        If KanseiVo.ShisakuSeisakuJunjyo <> dispValue.ShisakuSeisakuJunjyo Then
                            'Nothingならブランクをセット
                            Dim strShisakuSeisakuJunjyo As String
                            If KanseiVo.ShisakuSeisakuJunjyo Is Nothing Then
                                strShisakuSeisakuJunjyo = ""
                            Else
                                strShisakuSeisakuJunjyo = KanseiVo.ShisakuSeisakuJunjyo
                            End If
                            Dim strShisakuSeisakuJunjyo2 As String
                            If dispValue.ShisakuSeisakuJunjyo Is Nothing Then
                                strShisakuSeisakuJunjyo2 = ""
                            Else
                                strShisakuSeisakuJunjyo2 = dispValue.ShisakuSeisakuJunjyo
                            End If
                            '変更情報を作成
                            eventCompleteCarDao.InsertShisakuEventCompleteCar(newShisakuEventCode, key, _
                                                                              ShisakuSoubiHyoujiJun.KANSEI_SEISAKU_JUNJYO, _
                                                                              "TAG_SEISAKU_JUNJYO", _
                                                                              strShisakuSeisakuJunjyo, _
                                                                              strShisakuSeisakuJunjyo2)
                        End If
                        '完成日
                        If StringUtil.IsEmpty(KanseiVo.ShisakuKanseibi) Then
                            strKanseibi = ""
                        Else
                            strKanseibi = KanseiVo.ShisakuKanseibi
                        End If
                        If StringUtil.IsEmpty(dispValue.ShisakuKanseibi) Then
                            strKanseibiDisp = ""
                        Else
                            strKanseibiDisp = dispValue.ShisakuKanseibi
                        End If
                        If strKanseibi <> strKanseibiDisp Then
                            '変更情報を作成
                            eventCompleteCarDao.InsertShisakuEventCompleteCar(newShisakuEventCode, key, _
                                                                              ShisakuSoubiHyoujiJun.KANSEI_KANSEIBI, _
                                                                              "TAG_KANSEIBI", _
                                                                              strKanseibi, _
                                                                              strKanseibiDisp)
                        End If
                        '工事指令№
                        If KanseiVo.ShisakuKoushiNo <> dispValue.ShisakuKoushiNo Then
                            'Nothingならブランクをセット
                            Dim strShisakuKoushiNo As String
                            If KanseiVo.ShisakuKoushiNo Is Nothing Then
                                strShisakuKoushiNo = ""
                            Else
                                strShisakuKoushiNo = KanseiVo.ShisakuKoushiNo
                            End If
                            Dim strShisakuKoushiNo2 As String
                            If dispValue.ShisakuKoushiNo Is Nothing Then
                                strShisakuKoushiNo2 = ""
                            Else
                                strShisakuKoushiNo2 = dispValue.ShisakuKoushiNo
                            End If
                            '変更情報を作成
                            eventCompleteCarDao.InsertShisakuEventCompleteCar(newShisakuEventCode, key, _
                                                                              ShisakuSoubiHyoujiJun.KANSEI_KOUSHI_NO, _
                                                                              "TAG_KOSHI_NO", _
                                                                              strShisakuKoushiNo, _
                                                                              strShisakuKoushiNo2)
                        End If
                        '製作方法区分
                        If KanseiVo.ShisakuSeisakuHouhouKbn <> dispValue.ShisakuSeisakuHouhouKbn Then
                            'Nothingならブランクをセット
                            Dim strShisakuSeisakuHouhouKbn As String
                            If KanseiVo.ShisakuSeisakuHouhouKbn Is Nothing Then
                                strShisakuSeisakuHouhouKbn = ""
                            Else
                                strShisakuSeisakuHouhouKbn = KanseiVo.ShisakuSeisakuHouhouKbn
                            End If
                            Dim strShisakuSeisakuHouhouKbn2 As String
                            If dispValue.ShisakuSeisakuHouhouKbn Is Nothing Then
                                strShisakuSeisakuHouhouKbn2 = ""
                            Else
                                strShisakuSeisakuHouhouKbn2 = dispValue.ShisakuSeisakuHouhouKbn
                            End If
                            '変更情報を作成
                            eventCompleteCarDao.InsertShisakuEventCompleteCar(newShisakuEventCode, key, _
                                                                              ShisakuSoubiHyoujiJun.KANSEI_SEISAKU_HOUHOU_KBN, _
                                                                              "TAG_SEISAKU_HOUHOU_KBN", _
                                                                              strShisakuSeisakuHouhouKbn, _
                                                                              strShisakuSeisakuHouhouKbn2)
                        End If
                        '製作方法
                        If KanseiVo.ShisakuSeisakuHouhou <> dispValue.ShisakuSeisakuHouhou Then
                            'Nothingならブランクをセット
                            Dim strShisakuSeisakuHouhou As String
                            If KanseiVo.ShisakuSeisakuHouhou Is Nothing Then
                                strShisakuSeisakuHouhou = ""
                            Else
                                strShisakuSeisakuHouhou = KanseiVo.ShisakuSeisakuHouhou
                            End If
                            Dim strShisakuSeisakuHouhou2 As String
                            If dispValue.ShisakuSeisakuHouhou Is Nothing Then
                                strShisakuSeisakuHouhou2 = ""
                            Else
                                strShisakuSeisakuHouhou2 = dispValue.ShisakuSeisakuHouhou
                            End If
                            '変更情報を作成
                            eventCompleteCarDao.InsertShisakuEventCompleteCar(newShisakuEventCode, key, _
                                                                              ShisakuSoubiHyoujiJun.KANSEI_SEISAKU_HOUHOU, _
                                                                              "TAG_SEISAKU_HOUHOU", _
                                                                              strShisakuSeisakuHouhou, _
                                                                              strShisakuSeisakuHouhou2)
                        End If
                        'メモ欄
                        If KanseiVo.ShisakuMemo <> dispValue.ShisakuMemo Then
                            'Nothingならブランクをセット
                            Dim strShisakuMemo As String
                            If KanseiVo.ShisakuMemo Is Nothing Then
                                strShisakuMemo = ""
                            Else
                                strShisakuMemo = KanseiVo.ShisakuMemo
                            End If
                            Dim strShisakuMemo2 As String
                            If dispValue.ShisakuMemo Is Nothing Then
                                strShisakuMemo2 = ""
                            Else
                                strShisakuMemo2 = dispValue.ShisakuMemo
                            End If
                            '変更情報を作成
                            eventCompleteCarDao.InsertShisakuEventCompleteCar(newShisakuEventCode, key, _
                                                                              ShisakuSoubiHyoujiJun.KANSEI_SHISAKU_MEMO, _
                                                                              "TAG_SHISAKU_MEMO", _
                                                                              strShisakuMemo, _
                                                                              strShisakuMemo2)
                        End If
                    End If
                Next

            End If
            '-------------------------------------------------------------------------------------------------

            '' 既存データを削除
            If Not IsAddMode() Then
                Dim param As New TShisakuEventKanseiVo
                param.ShisakuEventCode = newShisakuEventCode
                kanseiDao.DeleteBy(param)
            End If

            For Each key As Integer In _record.Keys
                Dim dispValue As TShisakuEventKanseiVo = _record.Value(key)
                Dim value As New TShisakuEventKanseiVo
                VoUtil.CopyProperties(dispValue, value)

                ''2012/02/21 号車がNothingの列はインサートしない
                ''試作イベントコードと表示順（key）からBASEを取得し、無ければインサートしない
                Dim shisakuEventBaseDao As New TShisakuEventBaseSeisakuIchiranDaoImpl
                Dim eventBaseVo As TShisakuEventBaseSeisakuIchiranVo = shisakuEventBaseDao.FindByPk(newShisakuEventCode, key)
                If eventBaseVo Is Nothing Then
                    Continue For
                End If

                value.ShisakuEventCode = newShisakuEventCode
                value.HyojijunNo = key
                If StringUtil.IsEmpty(value.CreatedUserId) Then
                    value.CreatedUserId = login.UserId
                    value.CreatedDate = aDate.CurrentDateDbFormat
                    value.CreatedTime = aDate.CurrentTimeDbFormat
                    dispValue.CreatedUserId = login.UserId
                    dispValue.CreatedDate = aDate.CurrentDateDbFormat
                    dispValue.CreatedTime = aDate.CurrentTimeDbFormat
                End If
                value.UpdatedUserId = login.UserId
                value.UpdatedDate = aDate.CurrentDateDbFormat
                value.UpdatedTime = aDate.CurrentTimeDbFormat

                kanseiDao.InsertBy(value)
            Next

            shisakuEventCode = newShisakuEventCode
        End Sub

        Private Sub RegisterMainKaitei(ByVal newShisakuEventCode As String, ByVal IsRegister As Boolean)

            '-------------------------------------------------------------------------------------------------
            '' 既存データを削除
            If Not IsAddMode() Then
                Dim param As New TShisakuEventKanseiKaiteiVo
                param.ShisakuEventCode = newShisakuEventCode
                kanseiKaiteiDao.DeleteBy(param)
            End If

            For Each key As Integer In _record.Keys
                Dim dispValue As TShisakuEventKanseiVo = _record.Value(key)
                Dim value As New TShisakuEventKanseiKaiteiVo
                VoUtil.CopyProperties(dispValue, value)

                ''2012/02/21 号車がNothingの列はインサートしない
                ''試作イベントコードと表示順（key）からBASEを取得し、無ければインサートしない
                Dim shisakuEventBaseKaiteiDao As New TShisakuEventBaseKaiteiDaoImpl
                Dim eventBaseVo As TShisakuEventBaseKaiteiVo = shisakuEventBaseKaiteiDao.FindByPk(newShisakuEventCode, key)
                If eventBaseVo Is Nothing Then
                    Continue For
                End If

                value.ShisakuEventCode = newShisakuEventCode
                value.HyojijunNo = key
                If StringUtil.IsEmpty(value.CreatedUserId) Then
                    value.CreatedUserId = login.UserId
                    value.CreatedDate = aDate.CurrentDateDbFormat
                    value.CreatedTime = aDate.CurrentTimeDbFormat
                    dispValue.CreatedUserId = login.UserId
                    dispValue.CreatedDate = aDate.CurrentDateDbFormat
                    dispValue.CreatedTime = aDate.CurrentTimeDbFormat
                End If
                value.UpdatedUserId = login.UserId
                value.UpdatedDate = aDate.CurrentDateDbFormat
                value.UpdatedTime = aDate.CurrentTimeDbFormat

                kanseiKaiteiDao.InsertBy(value)
            Next

            shisakuEventCode = newShisakuEventCode
        End Sub
    End Class
End Namespace