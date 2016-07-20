﻿Imports ShisakuCommon.Db.EBom.Vo.Helper
Imports ShisakuCommon.Db.EBom.Dao
Imports EventSakusei.EventEdit.Dao
Imports ShisakuCommon
Imports ShisakuCommon.Util.LabelValue
Imports ShisakuCommon.Db.EBom.Vo
Imports ShisakuCommon.Util
Imports ShisakuCommon.ShisakuComFunc
Imports EventSakusei.ShisakuBuhinMenu.Dao
Imports ShisakuCommon.Db.EBom.Dao.Impl

Namespace EventEdit.Logic
    ''' <summary>
    ''' 
    ''' </summary>
    ''' <remarks>ヘッダー部分とnewのsyncが要らない  ''↓↓2014/08/05 Ⅰ.5.EBOM差分出力　k) (TES)施 ADD BEGIN</remarks>
    Public Class EventEditEbomKanshi : Inherits Observable
        Private shisakuEventCode As String
        Private ReadOnly login As LoginInfo
        Private ReadOnly baseCarDao As EventEditBaseCarDao

        'Private ReadOnly shisakuEventBaseDao As TShisakuEventBaseSeisakuIchiranDao
        Private ReadOnly ebomKanshiDao As TShisakuEventEbomKanshiDao = New TShisakuEventEbomKanshiDaoImpl
        Private ReadOnly shisakuEventBaseKaiteiDao As TShisakuEventBaseKaiteiDao    '改訂情報
        Private ReadOnly aDate As ShisakuDate
        'Private ReadOnly aEzSync As EzSyncShubetsuGosha
        Private ReadOnly aEzSyncBaseTenkai As EzSyncBaseTenkai

        'ベース車情報
        Private seisakuHakouNo As String
        Private seisakuHakouNoKaiteiNo As String
        Private baseList As New List(Of TSeisakuIchiranBaseVo)
        Private wbList As New List(Of TSeisakuIchiranWbVo)

        ''' <summary>
        ''' 初期設定
        ''' </summary>
        ''' <param name="shisakuEventCode">試作イベントコード</param>
        ''' <param name="login">ログイン情報</param>
        ''' <param name="aEzSync">同期クラス</param>
        ''' <param name="isSekkeiTenkaiIkou">設計展開以降か</param>
        ''' <param name="baseCarDao">ベース車Dao</param>
        ''' <param name="shisakuEventBaseDao">ベース情報Dao</param>
        ''' <param name="shisakuEventBaseKaiteiDao">ベース情報改訂Dao</param>
        ''' <param name="aDate">日付</param>
        ''' <param name="seisakuHakouNo">製作一覧発行№</param>
        ''' <param name="seisakuHakouNoKaiteiNo">製作一覧発行№改訂№</param>
        ''' <remarks></remarks>
        Public Sub New(ByVal shisakuEventCode As String, _
                       ByVal login As LoginInfo, _
                       ByVal aEzSync As EzSyncShubetsuGosha, _
                       ByVal aEzSyncBaseTenkai As EzSyncBaseTenkai, _
                       ByVal aEzSyncEbomKanshi As EzSyncEbomKanshi, _
                       ByVal isSekkeiTenkaiIkou As Boolean, _
                       ByVal baseCarDao As EventEditBaseCarDao, _
                       ByVal shisakuEventBaseDao As TShisakuEventBaseSeisakuIchiranDao, _
                       ByVal shisakuEventBaseKaiteiDao As TShisakuEventBaseKaiteiDao, _
                       ByVal aDate As ShisakuDate, _
                       ByVal seisakuHakouNo As String, _
                       ByVal seisakuHakouNoKaiteiNo As String)
            Me.shisakuEventCode = shisakuEventCode
            Me.login = login
            Me.baseCarDao = baseCarDao
            '  Me.shisakuEventBaseDao = shisakuEventBaseDao
            Me.shisakuEventBaseKaiteiDao = shisakuEventBaseKaiteiDao
            Me.aDate = aDate
            'Me.aEzSync = aEzSync
            Me.aEzSyncBaseTenkai = aEzSyncBaseTenkai
            Me._isSekkeiTenkaiIkou = isSekkeiTenkaiIkou

            Me.seisakuHakouNo = seisakuHakouNo
            Me.seisakuHakouNoKaiteiNo = seisakuHakouNoKaiteiNo

            '製作一覧キー情報があれば製作一覧情報からセットする。
            If StringUtil.IsNotEmpty(seisakuHakouNo) Then
                '編集モードかつ設計展開以降なら
                If Not IsAddMode() And isSekkeiTenkaiIkou Then

                    ''↓↓2014/08/05 Ⅰ.5.EBOM差分出力 ap) (TES)施 ADD BEGIN
                    ReadRecords()
                    ' ReadRecordsUpdate()
                    ''↑↑2014/08/05 Ⅰ.5.EBOM差分出力 ap) (TES)施 ADD END

                Else
                    ReadRecordsInitial()
                End If
            Else
                If Not IsAddMode() Then
                    ReadRecords()
                End If
            End If

            SetChanged()
        End Sub

#Region "ベース車情報のDelegateプロパティ"

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
                If IsEditModes(rowNo) AndAlso SekkeiTenkaiKbn(rowNo) Is Nothing Then
                    SekkeiTenkaiKbn(rowNo) = Convert.ToString(True)
                End If
                ''↓↓2014/08/05 Ⅰ.5.EBOM差分出力 ad) (TES)施 DELETE START
                'aEzSync.NotifyShubetsu(Me, rowNo)
                'aEzSyncBaseTenkai.NotifyShubetsu(Me, rowNo)
                ''↑↑2014/08/05 Ⅰ.5.EBOM差分出力 ad) (TES)施 DELETE END
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
                If IsEditModes(rowNo) AndAlso SekkeiTenkaiKbn(rowNo) Is Nothing Then
                    SekkeiTenkaiKbn(rowNo) = Convert.ToString(True)
                End If
                ''↓↓2014/08/05 Ⅰ.5.EBOM差分出力 ad) (TES)施 DELETE START
                ' aEzSync.NotifyGosha(Me, rowNo)
                '設計展開以前なら下記の処理を実施
                'If Not IsSekkeiTenkaiIkou = True Then
                'aEzSyncBaseTenkai.NotifyGosha(Me, rowNo)
                ' End If
                ''↑↑2014/08/05 Ⅰ.5.EBOM差分出力 ad) (TES)施 DELETE END
            End Set
        End Property

        ''' <summary>設計展開選択区分</summary>
        ''' <value>設計展開選択区分</value>
        ''' <returns>設計展開選択区分</returns>
        Public Property SekkeiTenkaiKbn(ByVal rowNo As Integer) As String
            Get
                Return Records(rowNo).SekkeiTenkaiKbn
            End Get
            Set(ByVal value As String)
                If EzUtil.IsEqualIfNull(Records(rowNo).SekkeiTenkaiKbn, value) Then
                    Return
                End If
                Records(rowNo).SekkeiTenkaiKbn = value
                SetChanged()
            End Set
        End Property

        ''' <summary>ベース車開発符号</summary>
        ''' <value>ベース車開発符号</value>
        ''' <returns>ベース車開発符号</returns>
        Public Property BaseKaihatsuFugo(ByVal rowNo As Integer) As String
            Get
                Return Records(rowNo).BaseKaihatsuFugo
            End Get
            Set(ByVal value As String)
                If EzUtil.IsEqualIfNull(Records(rowNo).BaseKaihatsuFugo, value) Then
                    Return
                End If
                Records(rowNo).BaseKaihatsuFugo = value
                SetChanged()

                'Dim wasChanged As Boolean = hasChanged()
                'setChanged()
                'BaseShiyoujyouhouNoLabelValues(rowNo) = GetLabelValues_ShiyoshoSeqno(rowNo)
                'BaseShiyoujyouhouNo(rowNo) = BaseShiyoujyouhouNo(rowNo)
                'If Not wasChanged Then
                '    notifyObservers(rowNo)
                'End If
                '' 上記の方法だと、コンボボックス値を選択して"TABキー"したときに、カーソルが移動しない
                '' SpreadのOnChangeの中で、#Update() すると、カーソルが移動するので、とりあえずその方法にする

                BaseShiyoujyouhouNoLabelValues(rowNo) = GetLabelValues_ShiyoshoSeqno(rowNo)
                '' 開発符号変更時は、仕様情報以下をクリアする
                BaseShiyoujyouhouNo(rowNo) = Nothing
                ''↓↓2014/08/05 Ⅰ.5.EBOM差分出力 ad) (TES)施 DELETE START
                '
                '設計展開以前なら下記の処理を実施
                If Not IsSekkeiTenkaiIkou = True Then
                    ' aEzSyncBaseTenkai.NotifyBaseKaihatsuFugo(Me, rowNo)
                End If
                ''↑↑2014/08/05 Ⅰ.5.EBOM差分出力 ad) (TES)施 DELETE END
            End Set
        End Property

        ''' <summary>ベース車仕様情報№</summary>
        ''' <value>ベース車仕様情報№</value>
        ''' <returns>ベース車仕様情報№</returns>
        Public Property BaseShiyoujyouhouNo(ByVal rowNo As Integer) As String
            Get
                Return Records(rowNo).BaseShiyoujyouhouNo
            End Get
            Set(ByVal value As String)
                If EzUtil.IsEqualIfNull(Records(rowNo).BaseShiyoujyouhouNo, value) AndAlso Not HasChanged() Then
                    Return
                End If
                '20101016
                'Add.Tsunoda
                '値がある場合は、値の内容を000のフォーマットを行ったうえで評価に回す
                If Not value Is Nothing Then
                    value = Right("000" & value, 3)
                End If

                Records(rowNo).BaseShiyoujyouhouNo = IIf(ExistValueIgnoreNull(BaseShiyoujyouhouNoLabelValues(rowNo), value), value, Nothing)
                SetChanged()

                AppliedNoKatashikiFugo7s(rowNo) = GetAppliedNoKatashikiFugo7(rowNo)
                BaseAppliedNoLabelValues(rowNo) = ExtractLabelValues_AppliedNo(AppliedNoKatashikiFugo7s(rowNo))
                BaseKatashikiLabelValues(rowNo) = ExtractLabelValues_KatashikiFugo7(AppliedNoKatashikiFugo7s(rowNo))

                BaseAppliedNo(rowNo) = BaseAppliedNo(rowNo)
                BaseKatashiki(rowNo) = BaseKatashiki(rowNo)
                If Not StringUtil.IsEmpty(BaseAppliedNo(rowNo)) AndAlso StringUtil.IsEmpty(BaseKatashiki(rowNo)) Then
                    SyncAppliedNokatashikiFugo7(rowNo, True)
                ElseIf StringUtil.IsEmpty(BaseAppliedNo(rowNo)) AndAlso Not StringUtil.IsEmpty(BaseKatashiki(rowNo)) Then
                    SyncAppliedNokatashikiFugo7(rowNo, False)
                End If
                ''↓↓2014/08/05 Ⅰ.5.EBOM差分出力 ad) (TES)施 DELETE START
                '設計展開以前なら下記の処理を実施
                'If Not IsSekkeiTenkaiIkou = True Then
                'aEzSyncBaseTenkai.NotifyBaseShiyoujyouhouNo(Me, rowNo)
                'End If
                ''↑↑2014/08/05 Ⅰ.5.EBOM差分出力 ad) (TES)施 DELETE END
            End Set
        End Property

        ''' <summary>ベース車アプライド№</summary>
        ''' <value>ベース車アプライド№</value>
        ''' <returns>ベース車アプライド№</returns>
        Public Property BaseAppliedNo(ByVal rowNo As Integer) As String
            Get
                Return Records(rowNo).BaseAppliedNo
            End Get
            Set(ByVal value As String)
                If EzUtil.IsEqualIfNull(Records(rowNo).BaseAppliedNo, value) AndAlso Not HasChanged() Then
                    Return
                End If
                Records(rowNo).BaseAppliedNo = IIf(ExistValueIgnoreNull(BaseAppliedNoLabelValues(rowNo), value), value, Nothing)
                If Not HasChanged() Then
                    SetChanged()
                    SyncAppliedNokatashikiFugo7(rowNo, True)
                    '' Sync～メソッドの中でKatashikiにSetされて「仕向」連動しているから
                    '' Sync～メソッド終了時は「仕向」連動しない
                Else
                    BaseShimukeLabelValues(rowNo) = GetLabelValues_ShimukechiCode(rowNo)
                    BaseShimuke(rowNo) = BaseShimuke(rowNo)
                End If
                ''↓↓2014/08/05 Ⅰ.5.EBOM差分出力 ad) (TES)施 DELETE START
                '設計展開以前なら下記の処理を実施
                ' If Not IsSekkeiTenkaiIkou = True Then
                'aEzSyncBaseTenkai.NotifyBaseAppliedNo(Me, rowNo)
                'End If
                ''↑↑2014/08/05 Ⅰ.5.EBOM差分出力 ad) (TES)施 DELETE END
            End Set
        End Property

        ''' <summary>ベース車型式</summary>
        ''' <value>ベース車型式</value>
        ''' <returns>ベース車型式</returns>
        Public Property BaseKatashiki(ByVal rowNo As Integer) As String
            Get
                Return Records(rowNo).BaseKatashiki
            End Get
            Set(ByVal value As String)
                If EzUtil.IsEqualIfNull(Records(rowNo).BaseKatashiki, value) AndAlso Not HasChanged() Then
                    Return
                End If
                Records(rowNo).BaseKatashiki = IIf(ExistValueIgnoreNull(BaseKatashikiLabelValues(rowNo), value), value, Nothing)
                If Not HasChanged() Then
                    SetChanged()
                    SyncAppliedNokatashikiFugo7(rowNo, False)
                    '' Sync～メソッドの中でAppliedにSetされて「仕向」連動しているから
                    '' Sync～メソッド終了時は「仕向」連動しない
                Else
                    BaseShimukeLabelValues(rowNo) = GetLabelValues_ShimukechiCode(rowNo)
                    BaseShimuke(rowNo) = BaseShimuke(rowNo)
                End If
                ''↓↓2014/08/05 Ⅰ.5.EBOM差分出力 ad) (TES)施 DELETE START
                '設計展開以前なら下記の処理を実施
                'If Not IsSekkeiTenkaiIkou = True Then
                'aEzSyncBaseTenkai.NotifyBaseKatashiki(Me, rowNo)
                'End If
                ''↑↑2014/08/05 Ⅰ.5.EBOM差分出力 ad) (TES)施 DELETE END
            End Set
        End Property
        ''' <summary>ベース車仕向（Excel出力「国内」ラベル用）</summary>
        ''' <value>ベース車仕向</value>
        ''' <returns>ベース車仕向</returns>
        Public Property BaseShimukeKokunai(ByVal rowNo As Integer) As String
            Get
                Dim lv As New LabelValueVo
                If BaseShimukeLabelValues(rowNo) Is Nothing Then
                    Return ""
                End If
                If BaseShimukeLabelValues(rowNo).Count > 0 Then
                    lv = BaseShimukeLabelValues(rowNo)(0)
                    Return lv.Label
                Else
                    Return ""
                End If
            End Get
            Set(ByVal value As String)

            End Set
        End Property

        ''' <summary>ベース車仕向</summary>
        ''' <value>ベース車仕向</value>
        ''' <returns>ベース車仕向</returns>
        Public Property BaseShimuke(ByVal rowNo As Integer) As String
            Get
                Return Records(rowNo).BaseShimuke
            End Get
            Set(ByVal value As String)
                If EzUtil.IsEqualIfNull(Records(rowNo).BaseShimuke, value) AndAlso Not HasChanged() Then
                    Return
                End If
                Records(rowNo).BaseShimuke = IIf(ExistValueIgnoreNull(BaseShimukeLabelValues(rowNo), value), value, Nothing)
                SetChanged()

                BaseOpLabelValues(rowNo) = GetLabelValues_OpCode(rowNo)
                BaseOp(rowNo) = BaseOp(rowNo)
                ''↓↓2014/08/05 Ⅰ.5.EBOM差分出力 ad) (TES)施 DELETE START
                '設計展開以前なら下記の処理を実施
                'If Not IsSekkeiTenkaiIkou = True Then
                'aEzSyncBaseTenkai.NotifyBaseShimuke(Me, rowNo)
                'End If
                ''↑↑2014/08/05 Ⅰ.5.EBOM差分出力 ad) (TES)施 DELETE END
            End Set
        End Property

        ''' <summary>ベース車OP</summary>
        ''' <value>ベース車OP</value>
        ''' <returns>ベース車OP</returns>
        Public Property BaseOp(ByVal rowNo As Integer) As String
            Get
                Return Records(rowNo).BaseOp
            End Get
            Set(ByVal value As String)
                If EzUtil.IsEqualIfNull(Records(rowNo).BaseOp, value) AndAlso Not HasChanged() Then
                    Return
                End If
                Records(rowNo).BaseOp = IIf(ExistValueIgnoreNull(BaseOpLabelValues(rowNo), value), value, Nothing)
                SetChanged()

                BaseGaisousyokuLabelValues(rowNo) = GetLabelValues_GaisoShoku(rowNo)
                BaseNaisousyokuLabelValues(rowNo) = GetLabelValues_NaisoShoku(rowNo)
                BaseGaisousyoku(rowNo) = BaseGaisousyoku(rowNo)
                BaseNaisousyoku(rowNo) = BaseNaisousyoku(rowNo)
                ''↓↓2014/08/05 Ⅰ.5.EBOM差分出力 ad) (TES)施 DELETE START
                '設計展開以前なら下記の処理を実施
                'If Not IsSekkeiTenkaiIkou = True Then
                'aEzSyncBaseTenkai.NotifyBaseOp(Me, rowNo)
                'End If
                ''↑↑2014/08/05 Ⅰ.5.EBOM差分出力 ad) (TES)施 DELETE END
            End Set
        End Property

        ''' <summary>ベース車外装色</summary>
        ''' <value>ベース車外装色</value>
        ''' <returns>ベース車外装色</returns>
        Public Property BaseGaisousyoku(ByVal rowNo As Integer) As String
            Get
                Return Records(rowNo).BaseGaisousyoku
            End Get
            Set(ByVal value As String)
                If EzUtil.IsEqualIfNull(Records(rowNo).BaseGaisousyoku, value) AndAlso Not HasChanged() Then
                    Return
                End If
                Records(rowNo).BaseGaisousyoku = IIf(ExistValueIgnoreNull(BaseGaisousyokuLabelValues(rowNo), value), value, Nothing)
                SetChanged()
                ''↓↓2014/08/05 Ⅰ.5.EBOM差分出力 ad) (TES)施 DELETE START
                '設計展開以前なら下記の処理を実施
                'If Not IsSekkeiTenkaiIkou = True Then
                'aEzSyncBaseTenkai.NotifyBaseGaisousyoku(Me, rowNo)
                'End If
                ''↑↑2014/08/05 Ⅰ.5.EBOM差分出力 ad) (TES)施 DELETE END
            End Set
        End Property

        ''' <summary>ベース車内装色</summary>
        ''' <value>ベース車内装色</value>
        ''' <returns>ベース車内装色</returns>
        Public Property BaseNaisousyoku(ByVal rowNo As Integer) As String
            Get
                Return Records(rowNo).BaseNaisousyoku
            End Get
            Set(ByVal value As String)
                If EzUtil.IsEqualIfNull(Records(rowNo).BaseNaisousyoku, value) AndAlso Not HasChanged() Then
                    Return
                End If
                Records(rowNo).BaseNaisousyoku = IIf(ExistValueIgnoreNull(BaseNaisousyokuLabelValues(rowNo), value), value, Nothing)
                SetChanged()
                ''↓↓2014/08/05 Ⅰ.5.EBOM差分出力 ad) (TES)施 DELETE START
                '設計展開以前なら下記の処理を実施
                'If Not IsSekkeiTenkaiIkou = True Then
                'aEzSyncBaseTenkai.NotifyBaseNaisousyoku(Me, rowNo)
                'End If
                ''↑↑2014/08/05 Ⅰ.5.EBOM差分出力 ad) (TES)施 DELETE END
            End Set
        End Property

        ''' <summary>試作ベースイベントコード</summary>
        ''' <value>試作ベースイベントコード</value>
        ''' <returns>試作ベースイベントコード</returns>
        Public Property ShisakuBaseEventCode(ByVal rowNo As Integer) As String
            Get
                Return Records(rowNo).ShisakuBaseEventCode
            End Get
            Set(ByVal value As String)
                If EzUtil.IsEqualIfNull(Records(rowNo).ShisakuBaseEventCode, value) Then
                    Return
                End If
                Records(rowNo).ShisakuBaseEventCode = value
                SetChanged()

                ShisakuBaseGousyaLabelValues(rowNo) = GetLabelValues_ShisakuGousya(rowNo)
                ShisakuBaseGousya(rowNo) = ShisakuBaseGousya(rowNo)
                '
                ''↓↓2014/08/05 Ⅰ.5.EBOM差分出力 ad) (TES)施 DELETE START
                '設計展開以前なら下記の処理を実施
                'If Not IsSekkeiTenkaiIkou = True Then
                'aEzSyncBaseTenkai.NotifyShisakuBaseEventCode(Me, rowNo)
                'End If
                ''↑↑2014/08/05 Ⅰ.5.EBOM差分出力 ad) (TES)施 DELETE END
            End Set
        End Property

        ''' <summary>試作ベース号車</summary>
        ''' <value>試作ベース号車</value>
        ''' <returns>試作ベース号車</returns>
        Public Property ShisakuBaseGousya(ByVal rowNo As Integer) As String
            Get
                Return Records(rowNo).ShisakuBaseGousya
            End Get
            Set(ByVal value As String)
                If EzUtil.IsEqualIfNull(Records(rowNo).ShisakuBaseGousya, value) Then
                    Return
                End If
                Records(rowNo).ShisakuBaseGousya = IIf(ExistValueIgnoreNull(ShisakuBaseGousyaLabelValues(rowNo), value), value, Nothing)
                SetChanged()
                ''↓↓2014/08/05 Ⅰ.5.EBOM差分出力 ad) (TES)施 DELETE START
                '設計展開以前なら下記の処理を実施
                'If Not IsSekkeiTenkaiIkou = True Then
                'aEzSyncBaseTenkai.NotifyShisakuBaseGousya(Me, rowNo)
                'End If
                ''↑↑2014/08/05 Ⅰ.5.EBOM差分出力 ad) (TES)施 DELETE END
            End Set
        End Property


        ''' <summary>ベース車仕様情報№の選択値</summary>
        ''' <value>ベース車仕様情報№の選択値</value>
        ''' <returns>ベース車仕様情報№の選択値</returns>
        Public Property BaseShiyoujyouhouNoLabelValues(ByVal rowNo As Integer) As List(Of LabelValueVo)
            Get
                Return Records(rowNo).BaseShiyoujyouhouNoLabelValues
            End Get
            Set(ByVal value As List(Of LabelValueVo))
                Records(rowNo).BaseShiyoujyouhouNoLabelValues = value
            End Set
        End Property

        ''' <summary>アプライド№と型式の選択値元データ</summary>
        ''' <value>アプライド№と型式の選択値元データ</value>
        ''' <returns>アプライド№と型式の選択値元データ</returns>
        Public Property AppliedNoKatashikiFugo7s(ByVal rowNo As Integer) As List(Of Rhac0230Vo)
            Get
                Return Records(rowNo).AppliedNoKatashikiFugo7s
            End Get
            Set(ByVal value As List(Of Rhac0230Vo))
                Records(rowNo).AppliedNoKatashikiFugo7s = value
            End Set
        End Property

        ''' <summary>ベース車アプライド№の選択値</summary>
        ''' <value>ベース車アプライド№の選択値</value>
        ''' <returns>ベース車アプライド№の選択値</returns>
        Public Property BaseAppliedNoLabelValues(ByVal rowNo As Integer) As List(Of LabelValueVo)
            Get
                Return Records(rowNo).BaseAppliedNoLabelValues
            End Get
            Set(ByVal value As List(Of LabelValueVo))
                Records(rowNo).BaseAppliedNoLabelValues = value
            End Set
        End Property

        ''' <summary>ベース車型式の選択値</summary>
        ''' <value>ベース車型式の選択値</value>
        ''' <returns>ベース車型式の選択値</returns>
        Public Property BaseKatashikiLabelValues(ByVal rowNo As Integer) As List(Of LabelValueVo)
            Get
                Return Records(rowNo).BaseKatashikiLabelValues
            End Get
            Set(ByVal value As List(Of LabelValueVo))
                Records(rowNo).BaseKatashikiLabelValues = value
            End Set
        End Property

        ''' <summary>ベース車仕向の選択値</summary>
        ''' <value>ベース車仕向の選択値</value>
        ''' <returns>ベース車仕向の選択値</returns>
        Public Property BaseShimukeLabelValues(ByVal rowNo As Integer) As List(Of LabelValueVo)
            Get
                Return Records(rowNo).BaseShimukeLabelValues
            End Get
            Set(ByVal value As List(Of LabelValueVo))
                Records(rowNo).BaseShimukeLabelValues = value
            End Set
        End Property

        ''' <summary>ベース車OPの選択値</summary>
        ''' <value>ベース車OPの選択値</value>
        ''' <returns>ベース車OPの選択値</returns>
        Public Property BaseOpLabelValues(ByVal rowNo As Integer) As List(Of LabelValueVo)
            Get
                Return Records(rowNo).BaseOpLabelValues
            End Get
            Set(ByVal value As List(Of LabelValueVo))
                Records(rowNo).BaseOpLabelValues = value
            End Set
        End Property

        ''' <summary>ベース車外装色の選択値</summary>
        ''' <value>ベース車外装色の選択値</value>
        ''' <returns>ベース車外装色の選択値</returns>
        Public Property BaseGaisousyokuLabelValues(ByVal rowNo As Integer) As List(Of LabelValueVo)
            Get
                Return Records(rowNo).BaseGaisousyokuLabelValues
            End Get
            Set(ByVal value As List(Of LabelValueVo))
                Records(rowNo).BaseGaisousyokuLabelValues = value
            End Set
        End Property

        ''' <summary>ベース車内装色の選択値</summary>
        ''' <value>ベース車内装色の選択値</value>
        ''' <returns>ベース車内装色の選択値</returns>
        Public Property BaseNaisousyokuLabelValues(ByVal rowNo As Integer) As List(Of LabelValueVo)
            Get
                Return Records(rowNo).BaseNaisousyokuLabelValues
            End Get
            Set(ByVal value As List(Of LabelValueVo))
                Records(rowNo).BaseNaisousyokuLabelValues = value
            End Set
        End Property

        ''' <summary>試作ベース号車の選択値</summary>
        ''' <value>試作ベース号車の選択値</value>
        ''' <returns>試作ベース号車の選択値</returns>
        Public Property ShisakuBaseGousyaLabelValues(ByVal rowNo As Integer) As List(Of LabelValueVo)
            Get
                Return Records(rowNo).ShisakuBaseGousyaLabelValues
            End Get
            Set(ByVal value As List(Of LabelValueVo))
                Records(rowNo).ShisakuBaseGousyaLabelValues = value
            End Set
        End Property



        ''' <summary>製作一覧_車種</summary>
        ''' <value>製作一覧_車種</value>
        ''' <returns>製作一覧_車種</returns>
        Public Property SeisakuSyasyu(ByVal rowNo As Integer) As String
            Get
                Return Records(rowNo).SeisakuSyasyu
            End Get
            Set(ByVal value As String)
                Records(rowNo).SeisakuSyasyu = value
            End Set
        End Property
        ''' <summary>製作一覧_グレード</summary>
        ''' <value>製作一覧_グレード</value>
        ''' <returns>製作一覧_グレード</returns>
        Public Property SeisakuGrade(ByVal rowNo As Integer) As String
            Get
                Return Records(rowNo).SeisakuGrade
            End Get
            Set(ByVal value As String)
                Records(rowNo).SeisakuGrade = value
            End Set
        End Property
        ''' <summary>製作一覧_仕向地・仕向け</summary>
        ''' <value>製作一覧_仕向地・仕向け</value>
        ''' <returns>製作一覧_仕向地・仕向け</returns>
        Public Property SeisakuShimuke(ByVal rowNo As Integer) As String
            Get
                Return Records(rowNo).SeisakuShimuke
            End Get
            Set(ByVal value As String)
                Records(rowNo).SeisakuShimuke = value
            End Set
        End Property
        ''' <summary>製作一覧_仕向地・ハンドル</summary>
        ''' <value>製作一覧_仕向地・ハンドル</value>
        ''' <returns>製作一覧_仕向地・ハンドル</returns>
        Public Property SeisakuHandoru(ByVal rowNo As Integer) As String
            Get
                Return Records(rowNo).SeisakuHandoru
            End Get
            Set(ByVal value As String)
                Records(rowNo).SeisakuHandoru = value
            End Set
        End Property
        ''' <summary>製作一覧_E/G仕様・排気量</summary>
        ''' <value>製作一覧_E/G仕様・排気量</value>
        ''' <returns>製作一覧_E/G仕様・排気量</returns>
        Public Property SeisakuEgHaikiryou(ByVal rowNo As Integer) As String
            Get
                Return Records(rowNo).SeisakuEgHaikiryou
            End Get
            Set(ByVal value As String)
                Records(rowNo).SeisakuEgHaikiryou = value
            End Set
        End Property
        ''' <summary>製作一覧_E/G仕様・型式</summary>
        ''' <value>製作一覧_E/G仕様・型式</value>
        ''' <returns>製作一覧_E/G仕様・型式</returns>
        Public Property SeisakuEgKatashiki(ByVal rowNo As Integer) As String
            Get
                Return Records(rowNo).SeisakuEgKatashiki
            End Get
            Set(ByVal value As String)
                Records(rowNo).SeisakuEgKatashiki = value
            End Set
        End Property
        ''' <summary>製作一覧_E/G仕様・過給器</summary>
        ''' <value>製作一覧_E/G仕様・過給器</value>
        ''' <returns>製作一覧_E/G仕様・過給器</returns>
        Public Property SeisakuEgKakyuuki(ByVal rowNo As Integer) As String
            Get
                Return Records(rowNo).SeisakuEgKakyuuki
            End Get
            Set(ByVal value As String)
                Records(rowNo).SeisakuEgKakyuuki = value
            End Set
        End Property
        ''' <summary>製作一覧_T/M仕様・駆動方式</summary>
        ''' <value>製作一覧_T/M仕様・駆動方式</value>
        ''' <returns>製作一覧_T/M仕様・駆動方式</returns>
        Public Property SeisakuTmKudou(ByVal rowNo As Integer) As String
            Get
                Return Records(rowNo).SeisakuTmKudou
            End Get
            Set(ByVal value As String)
                Records(rowNo).SeisakuTmKudou = value
            End Set
        End Property
        ''' <summary>製作一覧_T/M仕様・変速機</summary>
        ''' <value>製作一覧_T/M仕様・変速機</value>
        ''' <returns>製作一覧_T/M仕様・変速機</returns>
        Public Property SeisakuTmHensokuki(ByVal rowNo As Integer) As String
            Get
                Return Records(rowNo).SeisakuTmHensokuki
            End Get
            Set(ByVal value As String)
                Records(rowNo).SeisakuTmHensokuki = value
            End Set
        End Property
        ''' <summary>製作一覧_車体№</summary>
        ''' <value>製作一覧_車体№</value>
        ''' <returns>製作一覧_車体№</returns>
        Public Property SeisakuSyataiNo(ByVal rowNo As Integer) As String
            Get
                Return Records(rowNo).SeisakuSyataiNo
            End Get
            Set(ByVal value As String)
                Records(rowNo).SeisakuSyataiNo = value
            End Set
        End Property


#End Region

#Region "行情報取得・操作"

        Public _record As New IndexedList(Of EventEditBaseCarVo)

        ''' <summary>ベース車情報</summary>
        ''' <returns>ベース車情報</returns>
        Public ReadOnly Property FriendRecords(ByVal rowNo As Integer) As EventEditBaseCarVo
            Get
                Return _record.Value(rowNo)
            End Get
        End Property
        ''' <summary>ベース車情報</summary>
        ''' <returns>ベース車情報</returns>
        Public ReadOnly Property Records(ByVal rowNo As Integer) As EventEditBaseCarVo
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

        ''' <summary>
        ''' 行の読み込み
        '''　通常
        ''' </summary>
        ''' <remarks></remarks>
        Private Sub ReadRecords()


            ''↓↓2014/08/05 Ⅰ.5.EBOM差分出力 aq) (TES)施 ADD BEGIN
            Dim ebomKanshiDao As TShisakuEventEbomKanshiDao = New TShisakuEventEbomKanshiDaoImpl
            Dim vos As List(Of TShisakuEventEbomKanshiVo) = ebomKanshiDao.FindByEvent(shisakuEventCode)

            ' Dim param As New TShisakuEventBaseSeisakuIchiranVo
            ' param.ShisakuEventCode = shisakuEventCode
            ' Dim vos As List(Of TShisakuEventBaseSeisakuIchiranVo) = shisakuEventBaseDao.FindBy(param)
            'イベントコードを検索キーとして、T_SHISAKU_EVENT_EBOM_KANSHIから前回設定を取得して画面表示する																																																	
            ''↑↑2014/08/05 Ⅰ.5.EBOM差分出力 aq) (TES)施 ADD END
            For Each vo As TShisakuEventEbomKanshiVo In vos
                Dim rowNo As Integer = Convert.ToInt32(vo.HyojijunNo) '' 表示順は NotNull項目
                Dim record As New EventEditBaseCarVo
                VoUtil.CopyProperties(vo, record)
                ''↓↓2014/08/26 Ⅰ.5.EBOM差分出力 酒井 ADD BEGIN
                record.BaseKatashiki = vo.EbomKanshiKatashiki
                ''↑↑2014/08/26 Ⅰ.5.EBOM差分出力 酒井 ADD END
                If record.SekkeiTenkaiKbn IsNot Nothing Then
                    record.SekkeiTenkaiKbn = Convert.ToString(TShisakuEventBaseSeisakuIchiranVoHelper.SekkeiTenkaiKbn.JURYO_GO.Equals(record.SekkeiTenkaiKbn))
                End If
                _record.Add(rowNo, record)
                BaseShiyoujyouhouNoLabelValues(rowNo) = GetLabelValues_ShiyoshoSeqno(rowNo)
                AppliedNoKatashikiFugo7s(rowNo) = GetAppliedNoKatashikiFugo7(rowNo)
                BaseAppliedNoLabelValues(rowNo) = ExtractLabelValues_AppliedNo(AppliedNoKatashikiFugo7s(rowNo))
                BaseKatashikiLabelValues(rowNo) = ExtractLabelValues_KatashikiFugo7(AppliedNoKatashikiFugo7s(rowNo))
                BaseShimukeLabelValues(rowNo) = GetLabelValues_ShimukechiCode(rowNo)
                BaseOpLabelValues(rowNo) = GetLabelValues_OpCode(rowNo)
                BaseGaisousyokuLabelValues(rowNo) = GetLabelValues_GaisoShoku(rowNo)
                BaseNaisousyokuLabelValues(rowNo) = GetLabelValues_NaisoShoku(rowNo)
            Next
        End Sub

        ''' <summary>
        ''' 行の読み込み
        '''　イニシャル
        ''' 　　（号車のみセットする）
        ''' </summary>
        ''' <remarks></remarks>
        Private Sub ReadRecordsInitial()

            '初期設定
            Dim HoldRowNo As Integer = 0
            Dim Ichiran = New SeisakuIchiranDaoImpl
            'ベース車情報
            baseList = Ichiran.GetTSeisakuIchiranBase(seisakuHakouNo, seisakuHakouNoKaiteiNo)
            '製作一覧HD情報
            Dim getSeisakuIchiranHd As New SeisakuIchiranDaoImpl
            Dim tSeisakuHakouHdVo As New TSeisakuHakouHdVo
            tSeisakuHakouHdVo = getSeisakuIchiranHd.GetSeisakuIchiranHd(seisakuHakouNo, seisakuHakouNoKaiteiNo)

            For Each vo As TSeisakuIchiranBaseVo In baseList
                Dim rowNo As Integer = Convert.ToInt32(vo.HyojijunNo) - 1 '' 表示順は NotNull項目　製作一覧は1番からなのでマイナス１
                Dim record As New EventEditBaseCarVo
                '号車のみ
                '   開発符号＋号車
                record.ShisakuGousya = tSeisakuHakouHdVo.KaihatsuFugo & vo.Gousya
                '   号車が７桁未満なら開発符号と製作一覧号車の間に「#」を付ける。
                If record.ShisakuGousya.Length <= 7 Then
                    record.ShisakuGousya = tSeisakuHakouHdVo.KaihatsuFugo & "#" & vo.Gousya
                End If

                record.SekkeiTenkaiKbn = Nothing

                _record.Add(rowNo, record)

                BaseShiyoujyouhouNoLabelValues(rowNo) = GetLabelValues_ShiyoshoSeqno(rowNo)
                AppliedNoKatashikiFugo7s(rowNo) = GetAppliedNoKatashikiFugo7(rowNo)
                BaseAppliedNoLabelValues(rowNo) = ExtractLabelValues_AppliedNo(AppliedNoKatashikiFugo7s(rowNo))
                BaseKatashikiLabelValues(rowNo) = ExtractLabelValues_KatashikiFugo7(AppliedNoKatashikiFugo7s(rowNo))
                BaseShimukeLabelValues(rowNo) = GetLabelValues_ShimukechiCode(rowNo)
                BaseOpLabelValues(rowNo) = GetLabelValues_OpCode(rowNo)
                BaseGaisousyokuLabelValues(rowNo) = GetLabelValues_GaisoShoku(rowNo)
                BaseNaisousyokuLabelValues(rowNo) = GetLabelValues_NaisoShoku(rowNo)

                HoldRowNo = HoldRowNo + 1
            Next

            'WB車情報
            If HoldRowNo <> 0 Then
                HoldRowNo = HoldRowNo + 1
            End If
            wbList = Ichiran.GetTSeisakuIchiranWb(seisakuHakouNo, seisakuHakouNoKaiteiNo)

            For Each vo As TSeisakuIchiranWbVo In wbList
                Dim rowNo As Integer = HoldRowNo + Convert.ToInt32(vo.HyojijunNo) - 1 '' 表示順は NotNull項目　製作一覧は1番からなのでマイナス１
                Dim record As New EventEditBaseCarVo
                '号車のみ
                '   開発符号＋号車
                record.ShisakuGousya = tSeisakuHakouHdVo.KaihatsuFugo & vo.Gousya
                '   号車が７桁未満なら開発符号と製作一覧号車の間に「W」を付ける。

                '   2014/02/14 製作一覧の号車の１桁目にWがついている場合がある。
                '       その時はWを付けない。
                If record.ShisakuGousya.Length <= 7 Then
                    If vo.Gousya.IndexOf("W") = 0 Then
                        record.ShisakuGousya = tSeisakuHakouHdVo.KaihatsuFugo & vo.Gousya
                    Else
                        record.ShisakuGousya = tSeisakuHakouHdVo.KaihatsuFugo & "W" & vo.Gousya
                    End If
                End If
                'If record.ShisakuGousya.Length <= 7 Then
                '    record.ShisakuGousya = tSeisakuHakouHdVo.KaihatsuFugo & "W" & vo.Gousya
                'End If

                record.ShisakuSyubetu = "W" 'ホワイトボディ
                record.SekkeiTenkaiKbn = Nothing

                _record.Add(rowNo, record)

                BaseShiyoujyouhouNoLabelValues(rowNo) = GetLabelValues_ShiyoshoSeqno(rowNo)
                AppliedNoKatashikiFugo7s(rowNo) = GetAppliedNoKatashikiFugo7(rowNo)
                BaseAppliedNoLabelValues(rowNo) = ExtractLabelValues_AppliedNo(AppliedNoKatashikiFugo7s(rowNo))
                BaseKatashikiLabelValues(rowNo) = ExtractLabelValues_KatashikiFugo7(AppliedNoKatashikiFugo7s(rowNo))
                BaseShimukeLabelValues(rowNo) = GetLabelValues_ShimukechiCode(rowNo)
                BaseOpLabelValues(rowNo) = GetLabelValues_OpCode(rowNo)
                BaseGaisousyokuLabelValues(rowNo) = GetLabelValues_GaisoShoku(rowNo)
                BaseNaisousyokuLabelValues(rowNo) = GetLabelValues_NaisoShoku(rowNo)
            Next
        End Sub


#End Region

#Region "公開プロパティ"
        Private _isViewerMode As Boolean
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
        ''' ラベルと値の一覧に、検索値があるかを返す(検索値がNullなら常にfalse)
        ''' </summary>
        ''' <param name="labelValues">ラベルと値の一覧</param>
        ''' <param name="value">検索値</param>
        ''' <returns>ある場合、true(検索値がNullなら常にfalse)</returns>
        ''' <remarks></remarks>
        Public Function ExistValueIgnoreNull(ByVal labelValues As List(Of LabelValueVo), ByVal value As String) As Boolean
            If value Is Nothing Then
                Return False
            End If
            Return ExistValue(labelValues, value)
        End Function
        ''' <summary>
        ''' ラベルと値の一覧に、検索値があるかを返す
        ''' </summary>
        ''' <param name="labelValues">ラベルと値の一覧</param>
        ''' <param name="value">検索値</param>
        ''' <returns>ある場合、true</returns>
        ''' <remarks></remarks>
        Public Function ExistValue(ByVal labelValues As List(Of LabelValueVo), ByVal value As String) As Boolean
            If labelValues Is Nothing Then
                Return False
            End If
            For Each vo As LabelValueVo In labelValues
                If vo.Value IsNot Nothing AndAlso vo.Value.Equals(value) Then
                    Return True
                End If
            Next
            Return False
        End Function

        ''' <summary>
        ''' 開発符号コンボボックスの表示値を返す
        ''' </summary>
        ''' <returns>表示値</returns>
        ''' <remarks></remarks>
        Public Function GetLabelValues_ShisakuKaihatuFugo() As List(Of LabelValueVo)
            Return baseCarDao.FindKaihatsuFugoLabelValues
        End Function

        Private Shared ReadOnly EMPTY_RHAC0230_LIST As List(Of Rhac0230Vo) = New List(Of Rhac0230Vo)
        Private Shared ReadOnly EMPTY_LIST As New List(Of LabelValueVo)


        ''' <summary>
        ''' 仕様書一連Noのラベル取得
        ''' </summary>
        ''' <param name="rowNo">行番号</param>
        ''' <returns>仕様書一連Noのラベル</returns>
        ''' <remarks></remarks>
        Private Function GetLabelValues_ShiyoshoSeqno(ByVal rowNo As Integer) As List(Of LabelValueVo)
            If EzUtil.ContainsNull(Records(rowNo).BaseKaihatsuFugo) Then
                Return EMPTY_LIST
            End If
            Return baseCarDao.FindShiyoshoSeqnoLabelValues(Records(rowNo).BaseKaihatsuFugo)
        End Function

        ''' <summary>
        ''' アプライドNoと７桁型式の取得
        ''' </summary>
        ''' <param name="rowNo">行番号</param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Private Function GetAppliedNoKatashikiFugo7(ByVal rowNo As Integer) As List(Of Rhac0230Vo)
            If EzUtil.ContainsNull(Records(rowNo).BaseKaihatsuFugo, Records(rowNo).BaseShiyoujyouhouNo) Then
                Return EMPTY_RHAC0230_LIST
            End If
            Return baseCarDao.FindRhac0230By(Records(rowNo).BaseKaihatsuFugo, _
                                             Records(rowNo).BaseShiyoujyouhouNo)
        End Function

        Private Class AppliedNoExtraction : Implements ILabelValueExtraction
            Public Sub Extraction(ByVal aLocator As ShisakuCommon.Util.LabelValue.LabelValueLocator) Implements ShisakuCommon.Util.LabelValue.ILabelValueExtraction.Extraction
                Dim vo As New Rhac0230Vo
                aLocator.IsA(vo).Label(vo.AppliedNo).Value(vo.AppliedNo)
            End Sub
        End Class

        Public Function ExtractLabelValues_AppliedNo(ByVal vos As List(Of Rhac0230Vo)) As List(Of LabelValueVo)
            Dim results As List(Of LabelValueVo) = _
                LabelValueExtracter(Of Rhac0230Vo).Extract(vos, New AppliedNoExtraction)
            results.Sort(LabelValueComparer.NewIntValueSort)
            Return results
        End Function

        Private Class KatashikiFugo7NoExtraction : Implements ILabelValueExtraction
            Public Sub Extraction(ByVal aLocator As ShisakuCommon.Util.LabelValue.LabelValueLocator) Implements ShisakuCommon.Util.LabelValue.ILabelValueExtraction.Extraction
                Dim vo As New Rhac0230Vo
                aLocator.IsA(vo).Label(vo.KatashikiFugo7).Value(vo.KatashikiFugo7)
            End Sub
        End Class

        Public Function ExtractLabelValues_KatashikiFugo7(ByVal vos As List(Of Rhac0230Vo)) As List(Of LabelValueVo)
            Dim results As List(Of LabelValueVo) = _
                LabelValueExtracter(Of Rhac0230Vo).Extract(vos, New KatashikiFugo7NoExtraction)
            results.Sort(New LabelValueComparer)
            Return results
        End Function

        ''' <summary>
        ''' アプライドNoの選択値と、型式の選択値との同期をとる
        ''' </summary>
        ''' <param name="rowNo">行No</param>
        ''' <param name="IsApplied">アプライドNoを基点に同期をとる場合、true</param>
        ''' <remarks></remarks>
        Private Sub SyncAppliedNokatashikiFugo7(ByVal rowNo As Integer, ByVal IsApplied As Boolean)
            If IsApplied Then
                If BaseAppliedNo(rowNo) Is Nothing Then
                    BaseKatashiki(rowNo) = Nothing
                    Return
                End If
                Dim aBaseAppliedNo As Integer = CType(BaseAppliedNo(rowNo), Integer)
                For Each vo As Rhac0230Vo In AppliedNoKatashikiFugo7s(rowNo)
                    If vo.AppliedNo = aBaseAppliedNo Then
                        BaseKatashiki(rowNo) = vo.KatashikiFugo7
                        Return
                    End If
                Next
                BaseKatashiki(rowNo) = Nothing
            Else
                If BaseKatashiki(rowNo) Is Nothing Then
                    BaseAppliedNo(rowNo) = Nothing
                    Return
                End If
                For Each vo As Rhac0230Vo In AppliedNoKatashikiFugo7s(rowNo)
                    If BaseKatashiki(rowNo).Equals(vo.KatashikiFugo7) Then
                        BaseAppliedNo(rowNo) = vo.AppliedNo
                        Return
                    End If
                Next
                BaseAppliedNo(rowNo) = Nothing
            End If
        End Sub

        Private Function GetLabelValues_ShimukechiCode(ByVal rowNo As Integer) As List(Of LabelValueVo)
            If EzUtil.ContainsNull(Records(rowNo).BaseKaihatsuFugo, _
                           Records(rowNo).BaseShiyoujyouhouNo, _
                           Records(rowNo).BaseAppliedNo, _
                           Records(rowNo).BaseKatashiki) Then
                Return EMPTY_LIST
            End If
            Dim results As List(Of LabelValueVo) = baseCarDao.FindShimukechiCodeLabelValues(Records(rowNo).BaseKaihatsuFugo, _
                                                       Records(rowNo).BaseShiyoujyouhouNo, _
                                                       Records(rowNo).BaseAppliedNo, _
                                                       Records(rowNo).BaseKatashiki)
            '' "国内"表示
            For Each vo As LabelValueVo In results
                If TShisakuEventBaseSeisakuIchiranVoHelper.BaseShimuke.KOKUNAI.Equals(vo.Value) Then
                    vo.Label = TShisakuEventBaseSeisakuIchiranVoHelper.BaseShimukeName.KOKUNAI
                    Exit For
                End If
            Next
            Return results
        End Function

        Private Function GetLabelValues_OpCode(ByVal rowNo As Integer) As List(Of LabelValueVo)
            If EzUtil.ContainsNull(Records(rowNo).BaseKaihatsuFugo, _
                           Records(rowNo).BaseShiyoujyouhouNo, _
                           Records(rowNo).BaseAppliedNo, _
                           Records(rowNo).BaseKatashiki, _
                           Records(rowNo).BaseShimuke) Then
                Return EMPTY_LIST
            End If
            Return baseCarDao.FindOpCodeLabelValues(Records(rowNo).BaseKaihatsuFugo, _
                                                       Records(rowNo).BaseShiyoujyouhouNo, _
                                                       Records(rowNo).BaseAppliedNo, _
                                                       Records(rowNo).BaseKatashiki, _
                                                       Records(rowNo).BaseShimuke)
        End Function

        Private Function GetLabelValues_GaisoShoku(ByVal rowNo As Integer) As List(Of LabelValueVo)
            If EzUtil.ContainsNull(Records(rowNo).BaseKaihatsuFugo, _
                           Records(rowNo).BaseShiyoujyouhouNo, _
                           Records(rowNo).BaseAppliedNo, _
                           Records(rowNo).BaseKatashiki, _
                           Records(rowNo).BaseShimuke, _
                           Records(rowNo).BaseOp) Then
                Return EMPTY_LIST
            End If
            Return baseCarDao.FindGaisoShokuLabelValues(Records(rowNo).BaseKaihatsuFugo, _
                                                       Records(rowNo).BaseShiyoujyouhouNo, _
                                                       Records(rowNo).BaseAppliedNo, _
                                                       Records(rowNo).BaseKatashiki, _
                                                       Records(rowNo).BaseShimuke, _
                                                       Records(rowNo).BaseOp)
        End Function

        Private Function GetLabelValues_NaisoShoku(ByVal rowNo As Integer) As List(Of LabelValueVo)
            If EzUtil.ContainsNull(Records(rowNo).BaseKaihatsuFugo, _
                           Records(rowNo).BaseShiyoujyouhouNo, _
                           Records(rowNo).BaseAppliedNo, _
                           Records(rowNo).BaseKatashiki, _
                           Records(rowNo).BaseShimuke, _
                           Records(rowNo).BaseOp) Then
                Return EMPTY_LIST
            End If
            Return baseCarDao.FindNaisoShokuLabelValues(Records(rowNo).BaseKaihatsuFugo, _
                                                       Records(rowNo).BaseShiyoujyouhouNo, _
                                                       Records(rowNo).BaseAppliedNo, _
                                                       Records(rowNo).BaseKatashiki, _
                                                       Records(rowNo).BaseShimuke, _
                                                       Records(rowNo).BaseOp)
        End Function


        ''' <summary>
        ''' イベントコード の抽出実装クラス
        ''' </summary>
        ''' <remarks></remarks>
        Private Class ShisakuEventCodeExtraction : Implements ILabelValueExtraction
            Public Sub Extraction(ByVal aLocator As ShisakuCommon.Util.LabelValue.LabelValueLocator) Implements ShisakuCommon.Util.LabelValue.ILabelValueExtraction.Extraction
                Dim vo As New TShisakuSekkeiBlockInstlVo
                aLocator.IsA(vo).Label(vo.ShisakuEventCode).Value(vo.ShisakuEventCode)
            End Sub
        End Class

        ''' <summary>
        ''' イベントコードコンボボックスの表示値を返す
        ''' </summary>
        ''' <returns>表示値</returns>
        ''' <remarks></remarks>
        Public Function GetLabelValues_ShisakuEventCode() As List(Of LabelValueVo)
            Dim results As List(Of LabelValueVo) = _
                LabelValueExtracter(Of TShisakuSekkeiBlockInstlVo).Extract(baseCarDao.FindBlockInstlForLabelValuesByWithout(shisakuEventCode), New ShisakuEventCodeExtraction)
            results.Sort(New LabelValueComparer)
            Return results
        End Function

        Private Class ShisakuGousyaExtraction : Implements ILabelValueExtraction
            Public Sub Extraction(ByVal aLocator As ShisakuCommon.Util.LabelValue.LabelValueLocator) Implements ShisakuCommon.Util.LabelValue.ILabelValueExtraction.Extraction
                Dim vo As New TShisakuSekkeiBlockInstlVo
                aLocator.IsA(vo).Label(vo.ShisakuGousya).Value(vo.ShisakuGousya)
            End Sub
        End Class

        ''' <summary>
        ''' 号車コンボボックスの表示値を返す
        ''' </summary>
        ''' <returns>表示値</returns>
        ''' <remarks></remarks>
        Public Function GetLabelValues_ShisakuGousya(ByVal rowNo As Integer) As List(Of LabelValueVo)
            If EzUtil.ContainsNull(Records(rowNo).ShisakuBaseEventCode) Then
                Return EMPTY_LIST
            End If
            Dim results As List(Of LabelValueVo) = _
                LabelValueExtracter(Of TShisakuSekkeiBlockInstlVo).Extract(baseCarDao.FindBlockInstlForLabelValuesBy(Records(rowNo).ShisakuBaseEventCode), New ShisakuGousyaExtraction)
            results.Sort(New LabelValueComparer)
            Return results
        End Function

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
            '' 既存データを削除
            If Not IsAddMode() Then
                Dim param As New TShisakuEventEbomKanshiVo
                param.ShisakuEventCode = newShisakuEventCode
                ebomKanshiDao.DeleteBy(param)
            End If

            For Each key As Integer In _record.Keys
                Dim dispValue As TShisakuEventBaseSeisakuIchiranVo = _record.Value(key)
                ''↓↓2014/08/05 Ⅰ.5.EBOM差分出力 ar) (TES)施 ADD BEGIN
                Dim value As New TShisakuEventEbomKanshiVo
                ' Dim value As New TShisakuEventBaseSeisakuIchiranVo
                ''↑↑2014/08/05 Ⅰ.5.EBOM差分出力 ar) (TES)施 ADD END
                'ハンドルの値の変換　xHD⇒xへ　（HDを取る）
                If StringUtil.IsNotEmpty(dispValue.SeisakuHandoru) Then
                    dispValue.SeisakuHandoru = dispValue.SeisakuHandoru.Replace("HD", "")
                End If
                'コピー
                VoUtil.CopyProperties(dispValue, value)
                ''↓↓2014/08/26 Ⅰ.5.EBOM差分出力 酒井 ADD BEGIN
                value.EbomKanshiKatashiki = dispValue.BaseKatashiki
                ''↑↑2014/08/26 Ⅰ.5.EBOM差分出力 酒井 ADD END

                '2012/02/21 号車がNothingの列はインサートしない
                If StringUtil.IsEmpty(value.ShisakuGousya) Then
                    Continue For
                End If

                value.ShisakuEventCode = newShisakuEventCode
                value.HyojijunNo = key
                If value.SekkeiTenkaiKbn IsNot Nothing Then
                    value.SekkeiTenkaiKbn = Convert.ToString(IIf(Convert.ToBoolean(value.SekkeiTenkaiKbn), _
                                                TShisakuEventBaseSeisakuIchiranVoHelper.SekkeiTenkaiKbn.JURYO_GO, _
                                                TShisakuEventBaseSeisakuIchiranVoHelper.SekkeiTenkaiKbn.JURYO_MAE))
                End If

                'NEW設計展開用に以下の値を取得しBASE情報に更新する。　2011/03/16　柳沼
                Dim getSobiKaitei As New EventEditBaseCarDaoImpl
                Dim GetValue = getSobiKaitei.FindSobiKaitei(value.BaseKaihatsuFugo, _
                                                                      value.BaseShiyoujyouhouNo, _
                                                                      value.EbomKanshiKatashiki)
                If Not StringUtil.IsEmpty(GetValue) Then
                    value.BaseKatashikiScd7 = GetValue.KatashikiScd7
                    value.BaseSobiKaiteiNo = GetValue.SobiKaiteiNo
                End If
                '空文字だと設計展開エラーが発生する'
                If StringUtil.IsEmpty(value.BaseShiyoujyouhouNo) Then
                    value.BaseShiyoujyouhouNo = Nothing
                End If


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



                ''↓↓2014/08/05 Ⅰ.5.EBOM差分出力 ar) (TES)施 ADD BEGIN

                ' shisakuEventBaseDao.InsertBy(value)
                '更新先テーブルを、T_SHISAKU_EVENT_EBOM_KANSHIに変更する


                ebomKanshiDao.InsertBy(value)


                ''↑↑2014/08/05 Ⅰ.5.EBOM差分出力 ar) (TES)施 ADD END




            Next

            shisakuEventCode = newShisakuEventCode
        End Sub

        Private Sub RegisterMainKaitei(ByVal newShisakuEventCode As String, ByVal IsRegister As Boolean)
            '' 既存データを削除
            If Not IsAddMode() Then
                Dim param As New TShisakuEventBaseKaiteiVo
                param.ShisakuEventCode = newShisakuEventCode
                shisakuEventBaseKaiteiDao.DeleteBy(param)
            End If

            For Each key As Integer In _record.Keys
                Dim dispValue As TShisakuEventBaseSeisakuIchiranVo = _record(key)



                ''↓↓2014/08/05 Ⅰ.5.EBOM差分出力 as) (TES)施 修正 BEGIN
                Dim value As New TShisakuEventEbomKanshiVo
                ' Dim value As New TShisakuEventBaseKaiteiVo

                ''↑↑2014/08/05 Ⅰ.5.EBOM差分出力 as) (TES)施 修正 END

                'ハンドルの値の変換　xHD⇒xへ　（HDを取る）
                If StringUtil.IsNotEmpty(dispValue.SeisakuHandoru) Then
                    dispValue.SeisakuHandoru = dispValue.SeisakuHandoru.Replace("HD", "")
                End If
                'コピー
                VoUtil.CopyProperties(dispValue, value)

                '2012/02/21 号車がNothingの列はインサートしない
                If StringUtil.IsEmpty(value.ShisakuGousya) Then
                    Continue For
                End If

                value.ShisakuEventCode = newShisakuEventCode
                value.HyojijunNo = key
                If value.SekkeiTenkaiKbn IsNot Nothing Then
                    value.SekkeiTenkaiKbn = Convert.ToString(IIf(Convert.ToBoolean(value.SekkeiTenkaiKbn), _
                                                TShisakuEventBaseSeisakuIchiranVoHelper.SekkeiTenkaiKbn.JURYO_GO, _
                                                TShisakuEventBaseSeisakuIchiranVoHelper.SekkeiTenkaiKbn.JURYO_MAE))
                End If

                'NEW設計展開用に以下の値を取得しBASE情報に更新する。　2011/03/16　柳沼
                Dim getSobiKaitei As New EventEditBaseCarDaoImpl
                Dim GetValue = getSobiKaitei.FindSobiKaitei(value.BaseKaihatsuFugo, _
                                                                      value.BaseShiyoujyouhouNo, _
                                                                      value.EbomKanshiKatashiki)
                If Not StringUtil.IsEmpty(GetValue) Then
                    value.BaseKatashikiScd7 = GetValue.KatashikiScd7
                    value.BaseSobiKaiteiNo = GetValue.SobiKaiteiNo
                End If

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


                ''↓↓2014/08/05 Ⅰ.5.EBOM差分出力 as) (TES)施 修正 BEGIN
                '更新先テーブルを、T_SHISAKU_EVENT_EBOM_KANSHIに変更する	
                Dim ebomKanshiDao As TShisakuEventEbomKanshiDao = New TShisakuEventEbomKanshiDaoImpl
                ebomKanshiDao.InsertBy(value)
                'shisakuEventBaseKaiteiDao.InsertBy(value)
                ''↑↑2014/08/05 Ⅰ.5.EBOM差分出力 as) (TES)施 修正 END

            Next

            shisakuEventCode = newShisakuEventCode
        End Sub

        Public Sub Apply(ByVal intRowNo As Integer)

            '画面起動後変更があった場合に
            If StringUtil.Equals(HasChanged, True) Then

                '最初にクリア
                SeisakuSyasyu(intRowNo) = Nothing
                SeisakuGrade(intRowNo) = Nothing
                SeisakuShimuke(intRowNo) = Nothing
                SeisakuHandoru(intRowNo) = Nothing
                SeisakuEgHaikiryou(intRowNo) = Nothing
                SeisakuEgKatashiki(intRowNo) = Nothing
                SeisakuEgKakyuuki(intRowNo) = Nothing
                SeisakuTmKudou(intRowNo) = Nothing
                SeisakuTmHensokuki(intRowNo) = Nothing

                If Not EzUtil.ContainsNull(BaseKaihatsuFugo(intRowNo), BaseShiyoujyouhouNo(intRowNo), BaseAppliedNo(intRowNo), _
                                           BaseKatashiki(intRowNo), BaseShimuke(intRowNo), BaseOp(intRowNo), _
                                           BaseGaisousyoku(intRowNo), BaseNaisousyoku(intRowNo)) Then
                    Dim vo As Rhac0230Vo = baseCarDao.FindRhac0230By(BaseKaihatsuFugo(intRowNo), BaseShiyoujyouhouNo(intRowNo), _
                                                                     BaseKatashiki(intRowNo), BaseAppliedNo(intRowNo), _
                                                                     BaseShimuke(intRowNo), BaseOp(intRowNo), _
                                                                     BaseGaisousyoku(intRowNo), Rhac0430VoHelper.NaigaisoKbn.Gaiso)
                    If vo IsNot Nothing Then
                        SeisakuSyasyu(intRowNo) = vo.BodyKihonKata
                        SeisakuGrade(intRowNo) = vo.GradeCode
                        SeisakuShimuke(intRowNo) = vo.ShimukeDaiKbn
                        SeisakuHandoru(intRowNo) = vo.HandlePos
                        SeisakuEgHaikiryou(intRowNo) = vo.EgHaikiryo
                        SeisakuEgKatashiki(intRowNo) = vo.DobenkeiCode
                        SeisakuEgKakyuuki(intRowNo) = vo.KakyukiCode
                        SeisakuTmKudou(intRowNo) = vo.KudoHosiki
                        SeisakuTmHensokuki(intRowNo) = vo.TransMission
                    End If
                End If

            End If

        End Sub

        Public Function ShimukechiShimukeHenkan(ByVal strShimuke As String, ByVal strBaseShimuke As String)
            '仕向地・仕向け変換
            Dim strShimukechiShimuke As String = ""
            If strShimuke = "0" Then
                strShimukechiShimuke = _
                    TShisakuEventBaseSeisakuIchiranVoHelper.BaseShimukechiShimukeName.KOKUNAI
            ElseIf strShimuke = "1" Then
                strShimukechiShimuke = _
                    TShisakuEventBaseSeisakuIchiranVoHelper.BaseShimukechiShimukeName.HOKUBEI
            ElseIf strShimuke = "2" Then
                If strBaseShimuke = "KA" Then
                    strShimukechiShimuke = _
                        TShisakuEventBaseSeisakuIchiranVoHelper.BaseShimukechiShimukeName.GOSYU
                Else
                    strShimukechiShimuke = _
                        TShisakuEventBaseSeisakuIchiranVoHelper.BaseShimukechiShimukeName.OHSYUMIGI
                End If
            ElseIf strShimuke = "3" Then
                If strBaseShimuke = "EH" Or strBaseShimuke = "ET" Then
                    strShimukechiShimuke = _
                        TShisakuEventBaseSeisakuIchiranVoHelper.BaseShimukechiShimukeName.CHUGOKU
                Else
                    strShimukechiShimuke = _
                        TShisakuEventBaseSeisakuIchiranVoHelper.BaseShimukechiShimukeName.OHSYUHIDARI
                End If
            End If
            'ブランクなら受け取った仕向けを返す
            If StringUtil.IsEmpty(strShimukechiShimuke) Then strShimukechiShimuke = strShimuke

            Return strShimukechiShimuke
        End Function

    End Class
End Namespace