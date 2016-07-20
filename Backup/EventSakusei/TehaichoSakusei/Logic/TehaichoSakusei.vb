'とりあえずインポートしとくだけ'
Imports ShisakuCommon.Db.Impl
Imports ShisakuCommon.Db.EBom
Imports ShisakuCommon.Db.EBom.Exclusion
Imports ShisakuCommon.Db.EBom.Dao.Impl
Imports ShisakuCommon.Db.EBom.Dao
Imports ShisakuCommon
Imports ShisakuCommon.Util
Imports ShisakuCommon.Db.EBom.Vo
Imports ShisakuCommon.Db
Imports EventSakusei.TehaichoSakusei.Logic
Imports EventSakusei.TehaichoSakusei.Dao
Imports ShisakuCommon.Util.LabelValue
Imports EventSakusei.TehaichoSakusei.Vo
Imports System.Text

Namespace TehaichoSakusei.Logic

    ''' <summary>
    ''' 手配帳作成クラス
    ''' </summary>
    ''' <remarks></remarks>
    Public Class TehaichoSakusei : Inherits Observable

        Private ReadOnly aLoginInfo As LoginInfo
        Private ReadOnly aEventDao As TShisakuEventDao
        Private ReadOnly aKanseiDao As TShisakuEventKanseiDao
        Private ReadOnly aSKMSDao As TShisakuEventDao
        Private ReadOnly exclusionShisakuEvent As New TShisakuEventExclusion

        Private ReadOnly tehaichoSakusei As TehaichoSakuseiDao
        Private aDate As ShisakuDate
        Private aList As TShisakuListcodeDao
        Private ListCode As String
        Private KaihatsuFugo As String
        Private hikakuImpl As TehaichoHikakuDao = New TehaichoHikakuDaoImpl
        Private GousyaVo As New TShisakuBuhinEditGousyaTmpVo
        Private TimeOutFlag As Boolean

        Private _Eventvo As TShisakuEventVo

        ''' <summary>
        ''' 初期設定
        ''' </summary>
        ''' <param name="aLoginInfo">ログイン情報</param>
        ''' <param name="shisakuEventCode">イベントコード</param>
        ''' <remarks></remarks>
        Public Sub New(ByVal aLoginInfo As LoginInfo, ByVal shisakuEventCode As String)

            Me.aLoginInfo = aLoginInfo
            Me._ShisakuEventCode = shisakuEventCode

            Dim aTShisakuEventDao As TShisakuEventDaoImpl = New TShisakuEventDaoImpl
            Dim aTShisakuEventKanseiDao As TShisakuEventKanseiDaoImpl = New TShisakuEventKanseiDaoImpl
            Dim aSKMSDao As AsSKMSDaoImpl = New AsSKMSDaoImpl

            tehaichoSakusei = New TehaichoSakuseiDaoImpl
            'グループNo'
            GroupNoLabelValues = GetLabelValues_GroupNo()
            '製品区分'
            SeihinKbnLabelValues = GetLabelValues_SeihinKbn()
            'ユニット区分'
            UnitKbnLabelValues = GetLabelValues_UnitKbn()
            '完成車情報'
            Dim Kanseivo As New TShisakuEventKanseiVo
            Kanseivo = tehaichoSakusei.FindByGroupNo(shisakuEventCode)

            Dim Eventvo = tehaichoSakusei.FindByEventName(shisakuEventCode)

            Dim Seisakuvo As AsSKMSVo = tehaichoSakusei.FindByAsSNKMOne()

            If Not Kanseivo Is Nothing Then
                _GroupNo = Kanseivo.ShisakuGroup
            End If


            EventName = Eventvo.ShisakuEventName
            KaihatsuFugo = Eventvo.ShisakuKaihatsuFugo


            _SeihinKbn = Seisakuvo.Snkm

            KoujiKbn = ""
            KoujiNo = ""
            KoujiShireiNo = ""


            Dim daoEv As TShisakuEventDao = New TShisakuEventDaoImpl
            _Eventvo = daoEv.FindByPk(shisakuEventCode)


            SetChanged()

        End Sub

        ''' <summary>
        ''' 登録処理
        ''' </summary>
        ''' <remarks></remarks>
        Public Sub Register()
            Try
                'db.BeginTransaction()
                RegisterMain()
                'db.Commit()
            Catch ex As SqlClient.SqlException
                MsgBox("エラー" & ex.Message)
                'db.Rollback()
            End Try

            'db.BeginTransaction()
            'RegisterMain()
            'db.Commit()

        End Sub


        ''' <summary>
        ''' 登録処理の本体
        ''' </summary>
        ''' <remarks></remarks>
        Private Sub RegisterMain()

            TimeOutFlag = False
            Dim impl As TehaichoSakuseiDao = New TehaichoSakuseiDaoImpl

            '2012/02/27 何があってもリストコードは作成する'
            Try
                impl.DeleteByGousyaTmp(ShisakuEventCode)
                impl.DeleteByBuhinTmp(ShisakuEventCode)

                getListCode()

                'ヘッダー情報はここで作成しておく。
                InsertListCode()

                '改訂ブロックを削除する'
                impl.DeleteByKaiteiBlock(ShisakuEventCode, ListCode)

                '*****************************************************************************
                '図面設通No取得用データをここで取得しておく。
                '   （購担、取引先チェックにも使用）
                Dim dbZumen As New EBomDbClient
                Dim sqlaAsBuhin01 As String = _
                    " SELECT MAKER,KOTAN,DHSTBA,STSR,GZZCP " _
                    & " FROM " & MBOM_DB_NAME & ".dbo.AS_BUHIN01 WITH (NOLOCK, NOWAIT) " _
                    & " ORDER BY UPDATED_DATE DESC, UPDATED_TIME DESC "
                Dim aAsBuhin01 As New List(Of AsBUHIN01Vo)
                aAsBuhin01 = dbZumen.QueryForList(Of AsBUHIN01Vo)(sqlaAsBuhin01)
                '*****************************************************************************
                '*****************************************************************************
                '専用マーク、購担、取引先取得用データをここで取得しておく。
                Dim dbTori As New EBomDbClient
                Dim sqlaAsKpsm10p As String = _
                    " SELECT BUBA_15, KA, " _
                    & " TRCD " _
                    & " FROM " & MBOM_DB_NAME & ".dbo.AS_KPSM10P WITH (NOLOCK, NOWAIT) " _
                    & " ORDER BY UPDATED_DATE DESC, UPDATED_TIME DESC "
                Dim aAsKpsm10p As New List(Of AsKPSM10PVo)
                aAsKpsm10p = dbTori.QueryForList(Of AsKPSM10PVo)(sqlaAsKpsm10p)
                '
                Dim sqlaAsPartsp As String = _
                    " SELECT BUBA_13, KA, " _
                    & " TRCD " _
                    & " FROM " & MBOM_DB_NAME & ".dbo.AS_PARTSP WITH (NOLOCK, NOWAIT) " _
                    & " ORDER BY UPDATED_DATE DESC, UPDATED_TIME DESC "
                Dim aAsPartsp As New List(Of AsPARTSPVo)
                aAsPartsp = dbTori.QueryForList(Of AsPARTSPVo)(sqlaAsPartsp)
                '
                Dim sqlaAsGkpsm10p As String = _
                    " SELECT BUBA_15, KA, " _
                    & " TRCD " _
                    & " FROM " & MBOM_DB_NAME & ".dbo.AS_GKPSM10P WITH (NOLOCK, NOWAIT) " _
                    & " ORDER BY UPDATED_DATE DESC, UPDATED_TIME DESC "
                Dim aAsGkpsm10p As New List(Of AsGKPSM10PVo)
                aAsGkpsm10p = dbTori.QueryForList(Of AsGKPSM10PVo)(sqlaAsGkpsm10p)
                '
                'BUHIN01は図面設通用を使用する。
                '
                Dim sqlaTValueDev As String = _
                    "SELECT " _
                    & " BUHIN_NO, FHI_NOMINATE_KOTAN, " _
                    & " TORIHIKISAKI_CODE " _
                    & " FROM " _
                    & " " & EBOM_DB_NAME & ".dbo.T_VALUE_DEV WITH (NOLOCK, NOWAIT) " _
                    & " WHERE " _
                    & "  AN_LEVEL = '1' " _
                    & " ORDER BY UPDATED_DATE DESC, UPDATED_TIME DESC "
                Dim aTValueDev As New List(Of TValueDevVo)
                aTValueDev = dbTori.QueryForList(Of TValueDevVo)(sqlaTValueDev)
                '
                Dim sqlaRhac0610 As String = _
                    " SELECT MAKER_CODE, MAKER_NAME " _
                    & " FROM " & RHACLIBF_DB_NAME & ".dbo.RHAC0610 WITH (NOLOCK, NOWAIT) "
                Dim aRhac0610 As New List(Of Rhac0610Vo)
                aRhac0610 = dbTori.QueryForList(Of Rhac0610Vo)(sqlaRhac0610)
                '*****************************************************************************

                'TMP情報を追加する' GYOU_ID:999
                InsertBuhinTmp(aAsKpsm10p, aAsPartsp, aAsGkpsm10p)

                '比較結果織込みをするかしないか'
                If _OrikomiSuru Then
                    '集計コードからの展開をする'
                    UpdateBuhinTmpHikakuTenkaiAri()
                Else
                    If _SyukeiSuru Then
                        '比較結果織込みなし集計コード展開有り'
                        UpdateBuhinTmpTenkaiAri()
                    Else
                        '比較結果織込みなし集計コード展開無し'
                        '行IDを正常値にする'
                        impl.UpdateByTmpGyouId(ShisakuEventCode, ListCode)
                    End If
                End If

                UpdateShisakuKoushiNo()

                Dim BlockListVo As New List(Of TShisakuSekkeiBlockVo)
                'ブロックNoごとに分けておく'
                BlockListVo = impl.FindByBlockNoList(ShisakuEventCode, ListCode)

                Dim MergeList As New List(Of TehaichoBuhinEditTmpVo)
                Dim DummyVo As New TShisakuTehaiGousyaVo

                'ブロックNo毎にマージ・圧縮用Voを取得'
                For Each Vo As TShisakuSekkeiBlockVo In BlockListVo

                    'マージ・圧縮用リストを取得'
                    MergeList = impl.FindByTehaiMergeList(ShisakuEventCode, ListCode, Vo.ShisakuBukaCode, Vo.ShisakuBlockNo, Vo.ShisakuBlockNoKaiteiNo)

                    If Not MergeList.Count = 0 Then
                        'マージする'
                        '試作号車表示順、レベルが若いものから順に部品番号表示順を振る'

                        Dim NewMergeList As New List(Of TehaichoBuhinEditTmpVo)
                        NewMergeList = Merge(MergeList)

                        impl.InsertByTehaiGousya2(NewMergeList, ListCode)
                        impl.InsertByTehaiKihon2(NewMergeList, SeihinKbn, ListCode, Vo.UnitKbn, aAsBuhin01, _
                                                 aAsKpsm10p, aAsPartsp, aAsGkpsm10p, aTValueDev, aRhac0610)
                        '初回にも手配改訂抽出ブロック情報を追加'
                        impl.InsertByTehaiKaiteiBlock(ShisakuEventCode, ListCode, Vo.ShisakuBukaCode, Vo.ShisakuBlockNo, Vo.ShisakuBlockNoKaiteiNo)

                        DummyVo.ShisakuBukaCode = MergeList(0).ShisakuBukaCode
                        DummyVo.ShisakuBlockNo = MergeList(0).ShisakuBlockNo
                    End If
                Next

                'If MergeList.Count > 0 Then
                '    'ダミー列用に取得'
                '    Dim DummyGousyaVo As New TShisakuTehaiGousyaVo
                '    DummyGousyaVo = impl.FindByDummyTehaiGousya(ShisakuEventCode, _
                '                                                ListCode, _
                '                                                "000", _
                '                                                DummyVo.ShisakuBukaCode, _
                '                                                DummyVo.ShisakuBlockNo)
                '    DummyGousyaVo.ShisakuGousyaHyoujiJun = DummyGousyaVo.ShisakuGousyaHyoujiJun + 2

                '    impl.InserByDummyTehaiGousya(DummyGousyaVo)
                'Else

                '2012/03/02'
                'データ無しでも手配帳編集するために現時点で種別D以外の号車を用意する'
                Dim baseList As New List(Of TShisakuEventBaseVo)
                baseList = impl.FindByBaseList(ShisakuEventCode, _GroupNo)


                'ベースの番号を振りなおす'
                Dim i As Integer = 0
                For Each baseVo As TShisakuEventBaseVo In baseList
                    Dim DummyGousyaVo As New TShisakuTehaiGousyaVo
                    DummyGousyaVo.ShisakuEventCode = ShisakuEventCode
                    DummyGousyaVo.ShisakuListCode = ListCode
                    DummyGousyaVo.ShisakuListCodeKaiteiNo = "000"
                    DummyGousyaVo.ShisakuBukaCode = ""
                    DummyGousyaVo.ShisakuBlockNo = ""
                    DummyGousyaVo.ShisakuGousya = baseVo.ShisakuGousya
                    DummyGousyaVo.ShisakuGousyaHyoujiJun = baseVo.HyojijunNo
                    DummyGousyaVo.InsuSuryo = 0

                    impl.InsertByTehaiGousya(DummyGousyaVo)
                    i = baseVo.HyojijunNo
                Next

                'End If


                Dim DummyGousyaVo2 As New TShisakuTehaiGousyaVo
                DummyGousyaVo2 = impl.FindByDummyTehaiGousya(ShisakuEventCode, _
                                                            ListCode, _
                                                            "000", _
                                                            "", _
                                                            "")
                'ダミーを除く最大改訂を取得する'
                'インサート時にプラス２されるのでここではしない'
                Dim kaiteiDao As TehaichoMenu.Dao.KaiteiChusyutuDao = New TehaichoMenu.Impl.KaiteiChusyutuDaoImpl
                Dim maxGousyaHyoujiJun As Integer = kaiteiDao.FindByMaxGousyaHyoujijunNotDummy(ShisakuEventCode, ListCode, "000")

                'DummyGousyaVo2がNothingの時がある。
                If StringUtil.IsNotEmpty(DummyGousyaVo2) Then

                    DummyGousyaVo2.ShisakuGousyaHyoujiJun = maxGousyaHyoujiJun

                    impl.InserByDummyTehaiGousya(DummyGousyaVo2)

                End If
                '行IDがずれるから振り直す'
                Dim kihonList As New List(Of TShisakuTehaiKihonVo)
                kihonList = impl.FindByTehaiKihonList(ShisakuEventCode, ListCode)
                impl.UpdateByGyouId(kihonList)

                '2012/02/22
                '試作イベントの手配張作成日をセット
                impl.UpdateByShisakuEvent(ShisakuEventCode)

                '終わったらTMPを消す'
                impl.DeleteByGousyaTmp(ShisakuEventCode)
                impl.DeleteByBuhinTmp(ShisakuEventCode)
            Catch ex As Exception
                MsgBox("手配帳の作成に失敗しました。", MsgBoxStyle.Information, "エラー")
            Finally
                '2012/10/04　柳沼　ここにﾘｽﾄｺｰﾄﾞ作成があるとダメ
                '           途中で落ちたことを考えてない！！
                'InsertListCode()
            End Try

        End Sub

#Region "各テーブルの操作"
        'TODO あまり多くなるようなら別のクラスに移す'

        Private Sub getListCode()
            'リストコードは試作イベントコード+ - +工事指令No+NN'
            Dim ListCodeCheck = tehaichoSakusei.FindByListCode(_ShisakuEventCode, _KoujiShireiNo)
            If StringUtil.IsEmpty(ListCodeCheck.ShisakuListCode) Then
                ListCode = _ShisakuEventCode + "-" + _KoujiShireiNo + "01"
            Else
                Dim newListCode As Integer = Integer.Parse(ListCodeCheck.ShisakuListCode) + 1
                ListCode = _ShisakuEventCode + "-" + _KoujiShireiNo + Right("00" + newListCode.ToString(), 2)
            End If
        End Sub


        Private Sub InsertListCode()
            'とりあえずリストコードだけ作る'
            aList = New TShisakuListcodeDaoImpl
            aDate = New ShisakuDate
            Dim value As New TShisakuListcodeVo

            value.ShisakuEventCode = _ShisakuEventCode

            '試作イベントコードごとに連番'
            Dim hyoujijun = tehaichoSakusei.FindByHyoujijunNo(_ShisakuEventCode)
            If StringUtil.IsEmpty(hyoujijun.ShisakuListHyojijunNo) Then
                value.ShisakuListHyojijunNo = 0
            Else
                value.ShisakuListHyojijunNo = hyoujijun.ShisakuListHyojijunNo + 1
            End If

            'リストコードは試作イベントコード+ - +工事指令No+NN'
            'Dim ListCodeCheck = tehaichoSakusei.FindByListCode(_ShisakuEventCode, _KoujiShireiNo)
            'If StringUtil.IsEmpty(ListCodeCheck.ShisakuListCode) Then
            '    value.ShisakuListCode = _ShisakuEventCode + "-" + _KoujiShireiNo + "01"
            '    ListCode = _ShisakuEventCode + "-" + _KoujiShireiNo + "01"
            'Else
            '    Dim newListCode As Integer = Integer.Parse(ListCodeCheck.ShisakuListCode) + 1
            '    value.ShisakuListCode = _ShisakuEventCode + "-" + _KoujiShireiNo + Right("00" + newListCode.ToString(), 2)
            '    ListCode = _ShisakuEventCode + "-" + _KoujiShireiNo + Right("00" + newListCode.ToString(), 2)
            'End If
            value.ShisakuListCode = ListCode

            '試作リストコード改訂No'
            value.ShisakuListCodeKaiteiNo = "000"

            '試作グループNo'
            value.ShisakuGroupNo = _GroupNo

            '試作工事区分'
            value.ShisakuKoujiKbn = _KoujiKbn

            '試作製品区分'
            value.ShisakuSeihinKbn = _SeihinKbn

            'ユニット区分'
            value.UnitKbn = _UnitKbn

            '試作工事指令No'
            value.ShisakuKoujiShireiNo = _KoujiShireiNo

            '試作工事No'
            value.ShisakuKoujiNo = _KoujiNo

            '試作イベント名称'
            value.ShisakuEventName = _EventName

            '試作自給品の消しこみ'
            If _JikyuSuru = True Then
                value.ShisakuJikyuhin = "1"
            Else
                value.ShisakuJikyuhin = "0"
            End If

            If _JikyuShinai = True Then
                value.ShisakuJikyuhin = "0"
            Else
                value.ShisakuJikyuhin = "1"
            End If



            '試作比較結果の織り込み'
            If _OrikomiSuru = True Then
                value.ShisakuHikakukekka = "1"
            Else
                value.ShisakuHikakukekka = "0"
            End If

            '試作集計コードからの展開'
            If _SyukeiSuru = True Then
                value.ShisakuSyuukeiCode = "1"
            Else
                value.ShisakuSyuukeiCode = "0"
            End If

            '試作台数'
            Dim Gosha As New TehaichoSakuseiDaoImpl
            Dim Goshavo As List(Of TShisakuEventBaseVo)
            Goshavo = Gosha.FindBySyubetu(_ShisakuEventCode, _GroupNo)
            Dim kanseiDaisu As Integer = 0
            Dim WDaisu As Integer = 0
            For index As Integer = 0 To Goshavo.Count - 1
                If StringUtil.IsEmpty(Goshavo(index).ShisakuSyubetu) Then
                    kanseiDaisu = kanseiDaisu + 1
                ElseIf Goshavo(index).ShisakuSyubetu = "W" Then
                    WDaisu = WDaisu + 1
                End If
            Next
            value.ShisakuDaisu = kanseiDaisu.ToString() + "+" + WDaisu.ToString()
            '試作手配帳作成日'
            value.ShisakuTehaichoSakuseibi = Replace(aDate.CurrentDateDbFormat, "-", "")
            '試作手配帳作成時間'
            value.ShisakuTehaichoSakuseijikan = Replace(aDate.CurrentTimeDbFormat, ":", "")
            '旧リストコード'
            value.OldListCode = ""
            '試作手配帳発注用データ登録日'
            value.ShisakuDataTourokubi = 0
            '試作手配帳発注用データ登録時間'
            value.ShisakuDataTourokujikan = 0
            '履歴'
            'value.Rireki = ""
            '試作メモ欄'
            value.ShisakuMemo = ""
            '試作新調達への転送日'
            value.ShisakuTensoubi = "0"
            '試作新調達への転送時間'
            value.ShisakuTensoujikan = "0"
            '前回改定日'
            value.ZenkaiKaiteibi = "0"
            '最新抽出日'
            value.SaishinChusyutubi = "0"
            '最新抽出時間'
            value.SaishinChusyutujikan = "0"
            'ステータス'
            value.Status = ""
            '自動織込みフラグ'
            value.AutoOrikomiFlag = "0"
            'デモ用に6A(登録完了にする)'
            'value.Status = "6A"
            '作成ユーザーID'
            value.CreatedUserId = LoginInfo.Now.UserId
            '作成年月日'
            value.CreatedDate = aDate.CurrentDateDbFormat
            '作成時分秒'
            value.CreatedTime = aDate.CurrentTimeDbFormat
            '更新ユーザーID'
            value.UpdatedUserId = LoginInfo.Now.UserId
            '更新年月日'
            value.UpdatedDate = aDate.CurrentDateDbFormat
            '更新時分秒'
            value.UpdatedTime = aDate.CurrentTimeDbFormat

            aList.InsertBy(value)
        End Sub

        '試作部品表編集情報(TMP)を作成する'
        Private Sub InsertBuhinTmp(ByVal aAsKpsm10p As List(Of AsKPSM10PVo), _
                                   ByVal aAsPartsp As List(Of AsPARTSPVo), _
                                   ByVal aAsGkpsm10p As List(Of AsGKPSM10PVo))

            Dim tmpimpl As TehaichoSakuseiDao = New TehaichoSakuseiDaoImpl

            Dim aBuhinvo As New List(Of TehaichoBuhinEditTmpVo)
            aBuhinvo = tmpimpl.FindByBuhin(_ShisakuEventCode, _GroupNo, _JikyuSuru)

            '先にTMPをマージする'
            Dim MergeList As New List(Of TehaichoBuhinEditTmpVo)

            MergeList = MergeTmp(aBuhinvo)
            'マージした部品編集情報を追加'
            tmpimpl.InsertByBuhinEditTMP(MergeList, _SeihinKbn, ListCode, KaihatsuFugo, SyukeiSuru, _
                                         aAsKpsm10p, aAsPartsp, aAsGkpsm10p)

            'マージした部品編集TMP情報を追加'
            tmpimpl.InsertByGousyaTMP(MergeList)
        End Sub

        '試作部品表編集情報(TMP)を更新する'
        Private Sub UpdateBuhinTmpHikakuTenkaiAri()
            Dim uTmpImpl As TehaichoSakuseiDao = New TehaichoSakuseiDaoImpl
            Dim hikakuImpl As TehaichoHikakuDao = New TehaichoHikakuDaoImpl
            Dim hikakuBuhinvo As New List(Of TShisakuBuhinEditVo)
            Dim baseBuhinvo As New List(Of TShisakuBuhinEditBaseVo)
            Dim tmpvo As New List(Of TShisakuBuhinEditTmpVo)

            'ここで全部とる'
            tmpvo = uTmpImpl.FindByBuhinTmp(_ShisakuEventCode, ListCode)
            hikakuBuhinvo = ExchangeBuhinEdit(tmpvo)

            '比較結果織込み時'
            If _OrikomiSuru Then

                '比較結果織込み処理'
                Dim hikaku As New TehaichoHikaku(hikakuBuhinvo, ShisakuEventCode, ListCode, _SeihinKbn, _GroupNo, JikyuSuru)

                hikaku.Hikaku()

                'HikakuKekka(baseBuhinvo, hikakuBuhinvo)
                '集計コードからの展開'
                tmpvo = uTmpImpl.FindByBuhinHikakuTmp(_ShisakuEventCode, ListCode)

                hikakuImpl.EventVo = _Eventvo
                hikakuImpl.UpdateByBuhinTmpSyukeiTenkai(tmpvo, _SeihinKbn)

                'If _Eventvo.BlockAlertKind = 2 And _Eventvo.KounyuShijiFlg = "0" Then
                '    hikakuImpl.UpdateByBuhinTmpIkanshaKaishu(ShisakuEventCode, ListCode)
                'End If
            End If

        End Sub


        Private Sub UpdateBuhinIkanshaKaishu()
            Dim hikakuImpl As TehaichoHikakuDao = New TehaichoHikakuDaoImpl
        End Sub

        '比較織込みなし、集計コードからの展開あり'
        Private Sub UpdateBuhinTmpTenkaiAri()
            Dim uTmpImpl As TehaichoSakuseiDao = New TehaichoSakuseiDaoImpl
            Dim tmpvo As New List(Of TShisakuBuhinEditTmpVo)

            '行IDを正常値にする'
            uTmpImpl.UpdateByTmpGyouId(ShisakuEventCode, ListCode)


            'これだと行IDがおかしいまま'
            tmpvo = uTmpImpl.FindByBuhinTmp(_ShisakuEventCode, ListCode)

            hikakuImpl.UpdateByBuhinTmpSyukeiTenkai(tmpvo, _SeihinKbn)
        End Sub

        '完成車情報の工指Noを更新する'
        Private Sub UpdateShisakuKoushiNo()
            Dim aKansei = New TehaichoSakuseiDaoImpl
            aKansei.UpdateByKoushiNo(_ShisakuEventCode, _GroupNo, _KoujiShireiNo)
        End Sub

#End Region


        Private Function ResolveUserName(ByVal userId As String) As String

            Dim dao As New ShisakuDaoImpl
            Dim userVo As Rhac0650Vo = dao.FindUserById(userId)

            If userVo Is Nothing Then
                Return userId
            End If
            Return userVo.ShainName
        End Function

        ''' <summary>
        ''' マージ・圧縮の処理
        ''' </summary>
        ''' <param name="MergeList">マージ・圧縮用リスト</param>
        ''' <remarks></remarks>
        Private Function Merge(ByVal MergeList As List(Of TehaichoBuhinEditTmpVo)) As List(Of TehaichoBuhinEditTmpVo)

            '部品番号順にソートが完了したので今度は圧縮'

            '具体的にはブロックNo, レベル, 部品番号, 集計コード, 手配記号, 供給セクションが同一の項目は'
            '部品番号表示順が最も小さいものと同一の部品番号表示順を持つ'
            'この場合、試作号車表示順は異なっている'

            '別のやり方を模索中・・・・'
            Dim kyoku1 As String = ""
            Dim kyoku2 As String = ""

            ''2014/12/15 追加
            ''同一部品Noで供給セクションが別のものをはじくために使用
            Dim hajikuList As New ArrayList


            For index As Integer = 0 To MergeList.Count - 1
                'マージ済みならスルー
                If MergeList(index).CreatedUserId = "Merge" Then
                    Continue For
                End If

                'デバッグコード
                WriteConsoleMoto(index, MergeList)

                'For index2 As Integer = 0 To MergeList.Count - 1
                For index2 As Integer = index + 1 To MergeList.Count - 1

                    'マージ済みならスルー
                    If MergeList(index2).CreatedUserId = "Merge" Then
                        Continue For
                    End If

                    'デバッグコード
                    WriteConsoleSaki(index, index2, MergeList)

                    '↓↓2014/09/26 酒井 ADD BEGIN
                    Dim flg As Boolean = True
                    If _Eventvo.BlockAlertKind = 2 And _Eventvo.KounyuShijiFlg = "0" Then
                        flg = (MergeList(index).BaseBuhinFlg = MergeList(index2).BaseBuhinFlg)
                    End If
                    If flg Then
                        'If MergeList(index).BaseBuhinFlg = MergeList(index2).BaseBuhinFlg Then
                        '↑↑2014/09/26 酒井 ADD END
                        'ブロックNo毎に取り出しているのでブロックNoは見ない'
                        'レベル,部品番号,集計コードはNULLがないのでまとめて同一チェック'
                        If MergeList(index).Level = MergeList(index2).Level Then
                            If MergeList(index).BuhinNo = MergeList(index2).BuhinNo Then

                                '20110730試作区分のチェックを追加 樺澤'
                                '区分が違った時点で次の比較へ
                                If MergeList(index).BuhinNoKbn = MergeList(index2).BuhinNoKbn Then

                                    '2012/01/21 供給セクションのチェックを追加
                                    kyoku1 = MergeList(index).KyoukuSection
                                    kyoku2 = MergeList(index2).KyoukuSection
                                    If StringUtil.IsEmpty(kyoku1) Then
                                        kyoku1 = ""
                                    End If
                                    If StringUtil.IsEmpty(kyoku2) Then
                                        kyoku2 = ""
                                    End If

                                    '集計コードが同一か？'
                                    If MergeList(index).ShukeiCode.TrimEnd = "" Then

                                        '比較先集計コードが無い場合
                                        If MergeList(index2).ShukeiCode.TrimEnd = "" Then

                                            '集計コードが空なら海外を比較'
                                            If MergeList(index).SiaShukeiCode = MergeList(index2).SiaShukeiCode Then

                                                'あっていれば続き'
                                                '手配記号が同一か？'
                                                If MergeList(index).TehaiKigou = MergeList(index2).TehaiKigou OrElse _
                                                   (MergeList(index).TehaiKigou.TrimEnd = "" AndAlso _
                                                    MergeList(index2).TehaiKigou.TrimEnd = "") Then
                                                    '再使用区分が同一か？'
                                                    If MergeList(index).Saishiyoufuka = MergeList(index2).Saishiyoufuka Then

                                                        '2015/12/15 変更
                                                        '供給セクションが同一か？'
                                                        If kyoku1 = kyoku2 Then
                                                            '供給セクションが同一の場合マージ処理

                                                            MeargeCore(index, index2, MergeList)

                                                        Else

                                                        End If
                                                    End If
                                                End If



                                            Else
                                                '国内集計コードが両方空且つ、海外集計コードが一致しない場合
                                                '比較先または比較元のいずれかが空の場合はマージ処理へ回す
                                                If StringUtil.IsEmpty(MergeList(index).SiaShukeiCode) OrElse StringUtil.IsEmpty(MergeList(index2).SiaShukeiCode) Then

                                                    'あっていれば続き'
                                                    '手配記号が同一か？'
                                                    If MergeList(index).TehaiKigou = MergeList(index2).TehaiKigou OrElse _
                                                       (MergeList(index).TehaiKigou.TrimEnd = "" AndAlso _
                                                        MergeList(index2).TehaiKigou.TrimEnd = "") Then
                                                        '再使用区分が同一か？'
                                                        If MergeList(index).Saishiyoufuka = MergeList(index2).Saishiyoufuka Then

                                                            '2015/12/15 変更
                                                            '供給セクションが同一か？'
                                                            If kyoku1 = kyoku2 Then
                                                                '供給セクションが同一の場合マージ処理

                                                                MeargeCore(index, index2, MergeList)

                                                            Else

                                                            End If
                                                        End If
                                                    End If



                                                End If

                                            End If

                                        Else
                                            '比較元の国内集計コードが無の場合で、比較先の国内集計コードがある場合
                                            If MergeList(index).SiaShukeiCode = MergeList(index2).ShukeiCode Then

                                                'あっていれば続き'
                                                '手配記号が同一か？'
                                                If MergeList(index).TehaiKigou = MergeList(index2).TehaiKigou OrElse _
                                                   (MergeList(index).TehaiKigou.TrimEnd = "" AndAlso _
                                                    MergeList(index2).TehaiKigou.TrimEnd = "") Then
                                                    '再使用区分が同一か？'
                                                    If MergeList(index).Saishiyoufuka = MergeList(index2).Saishiyoufuka Then

                                                        '2015/12/15 変更
                                                        '供給セクションが同一か？'
                                                        If kyoku1 = kyoku2 Then
                                                            '供給セクションが同一の場合マージ処理

                                                            MeargeCore(index, index2, MergeList)

                                                        Else

                                                        End If
                                                    End If
                                                End If

                                            End If

                                        End If
                                    Else
                                        '集計コードが同一なら続き'
                                        If MergeList(index).ShukeiCode = MergeList(index2).ShukeiCode Then
                                            '手配記号が同一か？'
                                            If MergeList(index).TehaiKigou = MergeList(index2).TehaiKigou OrElse _
                                               (MergeList(index).TehaiKigou.TrimEnd = "" AndAlso _
                                                MergeList(index2).TehaiKigou.TrimEnd = "") Then
                                                '再使用区分が同一か？'
                                                '供給セクションが同一か？'
                                                If MergeList(index).Saishiyoufuka = MergeList(index2).Saishiyoufuka Then
                                                    If kyoku1 = kyoku2 Then
                                                        '供給セクションが同一の場合マージ処理

                                                        MeargeCore(index, index2, MergeList)

                                                    Else

                                                    End If
                                                End If
                                            End If
                                        Else
                                            '比較元の集計コードがあり、比較先の集計コードと一致しない場合
                                            '比較先の海外集計コードとの一致判定を行い、符号が一致していれば諸条件判定の上マージ
                                            '※比較元の国内集計コードがある場合はこれを基点として、海外集計コードとの斜め判定を行う
                                            If MergeList(index).ShukeiCode = MergeList(index2).SiaShukeiCode Then

                                                '手配記号が同一か？'
                                                If MergeList(index).TehaiKigou = MergeList(index2).TehaiKigou OrElse _
                                                   (MergeList(index).TehaiKigou.TrimEnd = "" AndAlso _
                                                    MergeList(index2).TehaiKigou.TrimEnd = "") Then
                                                    '再使用区分が同一か？'
                                                    '供給セクションが同一か？'
                                                    If MergeList(index).Saishiyoufuka = MergeList(index2).Saishiyoufuka Then
                                                        If kyoku1 = kyoku2 Then
                                                            '供給セクションが同一の場合マージ処理

                                                            MeargeCore(index, index2, MergeList)

                                                        Else

                                                        End If
                                                    End If
                                                End If

                                            Else
                                                If MergeList(index2).SiaShukeiCode = "" Then

                                                    '手配記号が同一か？'
                                                    If MergeList(index).TehaiKigou = MergeList(index2).TehaiKigou OrElse _
                                                       (MergeList(index).TehaiKigou.TrimEnd = "" AndAlso _
                                                        MergeList(index2).TehaiKigou.TrimEnd = "") Then
                                                        '再使用区分が同一か？'
                                                        '供給セクションが同一か？'
                                                        If MergeList(index).Saishiyoufuka = MergeList(index2).Saishiyoufuka Then
                                                            If kyoku1 = kyoku2 Then
                                                                '供給セクションが同一の場合マージ処理

                                                                MeargeCore(index, index2, MergeList)

                                                            Else

                                                            End If
                                                        End If
                                                    End If

                                                End If

                                            End If



                                        End If

                                    End If
                                End If
                            End If
                        End If
                        '↓↓2014/09/26 酒井 ADD BEGIN
                    End If
                    '↑↑2014/09/26 酒井 ADD END
                Next
            Next

            Return MergeList
        End Function

        Private Sub MeargeCore(ByVal index As Integer, ByVal index2 As Integer, ByRef MergeList As List(Of TehaichoBuhinEditTmpVo))

            '号車表示順が若い方はどっち？'
            If MergeList(index).ShisakuGousyaHyoujiJun < MergeList(index2).ShisakuGousyaHyoujiJun Then
                '行IDが001同士か000同士ならマージ'
                If MergeList(index).GyouId = MergeList(index2).GyouId Then
                    MergeList(index2).BuhinNoHyoujiJun = MergeList(index).BuhinNoHyoujiJun
                    MergeList(index2).CreatedUserId = "Merge"
                    '変化点は引き継ぐ'
                    If MergeList(index).Henkaten.TrimEnd <> "" Then
                        MergeList(index2).Henkaten = MergeList(index).Henkaten
                    End If
                    If MergeList(index2).Henkaten.TrimEnd <> "" Then
                        MergeList(index).Henkaten = MergeList(index2).Henkaten
                    End If
                End If
            ElseIf MergeList(index).ShisakuGousyaHyoujiJun > MergeList(index2).ShisakuGousyaHyoujiJun Then
                If MergeList(index).GyouId = MergeList(index2).GyouId Then
                    MergeList(index).BuhinNoHyoujiJun = MergeList(index2).BuhinNoHyoujiJun
                    MergeList(index).CreatedUserId = "Merge"
                    '変化点は引き継ぐ'
                    If MergeList(index).Henkaten.TrimEnd <> "" Then
                        MergeList(index2).Henkaten = MergeList(index).Henkaten
                    End If
                    If MergeList(index2).Henkaten.TrimEnd <> "" Then
                        MergeList(index).Henkaten = MergeList(index2).Henkaten
                    End If
                End If
            Else
                '号車表示順が同じでも部品番号表示順が違うパターンがいる'
                If MergeList(index).BuhinNoHyoujiJun < MergeList(index2).BuhinNoHyoujiJun Then
                    If MergeList(index).GyouId = MergeList(index2).GyouId Then
                        MergeList(index2).BuhinNoHyoujiJun = MergeList(index).BuhinNoHyoujiJun
                        MergeList(index2).CreatedUserId = "Merge"
                        '変化点は引き継ぐ'
                        If MergeList(index).Henkaten.TrimEnd <> "" Then
                            MergeList(index2).Henkaten = MergeList(index).Henkaten
                        End If
                        If MergeList(index2).Henkaten.TrimEnd <> "" Then
                            MergeList(index).Henkaten = MergeList(index2).Henkaten
                        End If
                    End If
                ElseIf MergeList(index).BuhinNoHyoujiJun > MergeList(index2).BuhinNoHyoujiJun Then
                    If MergeList(index).GyouId = MergeList(index2).GyouId Then
                        MergeList(index).BuhinNoHyoujiJun = MergeList(index2).BuhinNoHyoujiJun
                        MergeList(index).CreatedUserId = "Merge"
                        '変化点は引き継ぐ'
                        If MergeList(index).Henkaten.TrimEnd <> "" Then
                            MergeList(index2).Henkaten = MergeList(index).Henkaten
                        End If
                        If MergeList(index2).Henkaten.TrimEnd <> "" Then
                            MergeList(index).Henkaten = MergeList(index2).Henkaten
                        End If
                    End If
                End If
            End If

        End Sub

        Private Sub WriteConsoleMoto(ByVal index As Integer, ByVal MergeList As List(Of TehaichoBuhinEditTmpVo))

            If MergeList(index).BuhinNo <> "46022AL000" Then
                Exit Sub
            End If

            Dim buf As New StringBuilder

            With buf
                .Remove(0, .Length)
                .Append(MergeList(index).ShisakuBlockNo)
                .Append(Space(1))
                .Append(MergeList(index).BuhinNo)
                .Append(Space(1))
                .Append(MergeList(index).ShukeiCode)
                .Append(Space(1))
                .Append(MergeList(index).SiaShukeiCode)
                .Append(Space(1))
                .Append(MergeList(index).KyoukuSection)
                .Append(Space(1))
                .Append(MergeList(index).TehaiKigou)
                .Append(Space(1))
                .Append(MergeList(index).BuhinNoKbn)
                .Append(vbCrLf)
            End With

            Console.Write(buf.ToString)

        End Sub

        Private Sub WriteConsoleSaki(ByVal index As Integer, ByVal index2 As Integer, ByVal MergeList As List(Of TehaichoBuhinEditTmpVo))


            If MergeList(index).BuhinNo <> "46022AL000" Then
                Exit Sub
            End If

            If MergeList(index2).BuhinNo <> "46022AL000" Then
                Exit Sub
            End If

            Dim buf As New StringBuilder

            With buf
                .Remove(0, .Length)
                .Append(vbTab)
                .Append(MergeList(index2).ShisakuBlockNo)
                .Append(Space(1))
                .Append(MergeList(index2).BuhinNo)
                .Append(Space(1))
                .Append(MergeList(index2).ShukeiCode)
                .Append(Space(1))
                .Append(MergeList(index2).SiaShukeiCode)
                .Append(Space(1))
                .Append(MergeList(index2).KyoukuSection)
                .Append(Space(1))
                .Append(MergeList(index2).TehaiKigou)
                .Append(Space(1))
                .Append(MergeList(index2).BuhinNoKbn)
                .Append(vbCrLf)
            End With

            Console.Write(buf.ToString)

        End Sub

        ''' <summary>
        ''' マージテンポラリ用として
        ''' </summary>
        Private Shared FindShisakuBukaCode As String '部課コード
        Private Shared FindShisakuBlockNo As String 'ブロック№
        Private Shared FindLevel As String 'レベル
        Private Shared FindBuhinNo As String '部品番号
        Private Shared FindBuhinNoKbn As String '部品番号区分
        Private Shared FindKyoukuSection As String '供給セクション
        Private Shared FindSaishiyoufuka As String '再使用不可
        Private Shared FindShukeiCode As String '国内集計コード
        Private Shared FindSiaShukeiCode As String '海外集計コード
        Private Shared Function FindMergeListTemp(ByVal Vo As TehaichoBuhinEditTmpVo) As Boolean

            '試作区分はNULLのパターンも存在するので'
            'Dim kbn1 As String = Vo.BuhinNoKbn
            'If StringUtil.IsEmpty(kbn1) Then
            '    kbn1 = ""
            'End If
            '再使用不可はNULLのパターンも存在するので'
            'Dim saishiyou1 As String = Vo.Saishiyoufuka
            'If StringUtil.IsEmpty(saishiyou1) Then
            '    saishiyou1 = ""
            'End If

            '供給セクションはNULLのパターンも存在するので'
            Dim kyoku1 As String = Vo.KyoukuSection
            If StringUtil.IsEmpty(kyoku1) Then
                kyoku1 = ""
            End If
            '
            If Vo.ShisakuBukaCode = FindShisakuBukaCode AndAlso _
                Vo.ShisakuBlockNo = FindShisakuBlockNo AndAlso _
                Vo.Level = FindLevel AndAlso _
                Vo.BuhinNo = FindBuhinNo AndAlso _
                Vo.BuhinNoKbn = FindBuhinNoKbn Then
                '
                If StringUtil.IsEmpty(FindShukeiCode) Then
                    '国内集計コードが両方空なら海外集計コードを比較'
                    '海外集計コードが同一ならマージ可能'
                    '供給セクションを比較'
                    '再使用区分が同一か？'
                    If Vo.ShukeiCode.TrimEnd = "" AndAlso _
                        FindSiaShukeiCode = Vo.SiaShukeiCode AndAlso _
                        kyoku1 = FindKyoukuSection AndAlso _
                        Vo.Saishiyoufuka = FindSaishiyoufuka Then
                        Return True
                    End If
                    Return False
                Else
                    '両方からでないかチェック'
                    '両方空でないならチェック'
                    '国内集計コードが同一'
                    '供給セクションを比較'
                    '再使用区分が同一か？'
                    If Vo.ShukeiCode.TrimEnd <> "" AndAlso _
                        FindShukeiCode = Vo.ShukeiCode AndAlso _
                        kyoku1 = FindKyoukuSection AndAlso _
                        Vo.Saishiyoufuka = FindSaishiyoufuka Then
                        Return True
                    End If
                    Return False
                End If
                Return False
            Else
                Return False
            End If
        End Function
        ''' <summary>
        ''' マージする(TMP用)
        ''' </summary>
        ''' <param name="MergeList"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Private Function MergeTmp(ByVal MergeList As List(Of TehaichoBuhinEditTmpVo)) As List(Of TehaichoBuhinEditTmpVo)

            '別のやり方を模索中・・・・'
            Dim kyoku1 As String = ""
            Dim kyoku2 As String = ""

            ''2014/12/15 追加
            ''同一部品Noで供給セクションが別のものをはじくために使用
            Dim hajikuList As New ArrayList

            For index As Integer = 0 To MergeList.Count - 1
                'マージ済みならスルー
                If MergeList(index).CreatedUserId = "Merge" Then
                    Continue For
                End If

                'For index2 As Integer = 0 To MergeList.Count - 1
                For index2 As Integer = index + 1 To MergeList.Count - 1
                    If index = index2 Then
                        Continue For
                    End If

                    'マージ済みならスルー
                    If MergeList(index2).CreatedUserId = "Merge" Then
                        Continue For
                    End If
                    '↓↓2014/09/26 酒井 ADD BEGIN

                    Dim flg As Boolean = True
                    If _Eventvo.BlockAlertKind = 2 And _Eventvo.KounyuShijiFlg = "0" Then
                        flg = (MergeList(index).BaseBuhinFlg = MergeList(index2).BaseBuhinFlg)
                    End If

                    If flg Then
                        '↑↑2014/09/26 酒井 ADD END
                        '設計課が同一かチェック'
                        If MergeList(index).ShisakuBukaCode = MergeList(index2).ShisakuBukaCode Then
                            'ブロックNoが同一かチェック'
                            If MergeList(index).ShisakuBlockNo = MergeList(index2).ShisakuBlockNo Then
                                'レベルが同一かチェック'
                                If MergeList(index).Level = MergeList(index2).Level Then
                                    '部品番号が同一かチェック'
                                    If MergeList(index).BuhinNo = MergeList(index2).BuhinNo Then
                                        '部品番号区分が同一かチェック'
                                        If MergeList(index).BuhinNoKbn = MergeList(index2).BuhinNoKbn Then
                                            '-------------------------------------------------------------------------------
                                            '2012/01/21 供給セクションのチェックを追加
                                            kyoku1 = MergeList(index).KyoukuSection
                                            kyoku2 = MergeList(index2).KyoukuSection
                                            If StringUtil.IsEmpty(kyoku1) Then
                                                kyoku1 = ""
                                            End If
                                            If StringUtil.IsEmpty(kyoku2) Then
                                                kyoku2 = ""
                                            End If
                                            '-------------------------------------------------------------------------------

                                            '集計コードのチェックは複数パターンある'

                                            '比較元国内集計コードが無い場合
                                            If MergeList(index).ShukeiCode.TrimEnd = "" Then

                                                '比較先国内集計コードが無い場合
                                                If MergeList(index2).ShukeiCode.TrimEnd = "" Then

                                                    '国内集計コードが両方空なら海外集計コードを比較'

                                                    '海外集計コードが一致する場合
                                                    If MergeList(index).SiaShukeiCode = MergeList(index2).SiaShukeiCode Then '海外集計コードが同一ならマージ可能'
                                                        MergeCore(kyoku1, kyoku2, index, index2, MergeList, hajikuList) '複数回同一判定を行うためリファクタリングを行った
                                                    Else

                                                        '国内集計コードが両方空且つ、海外集計コードが一致しない場合
                                                        '比較先または比較元のいずれかが空の場合はマージ処理へ回す
                                                        If StringUtil.IsEmpty(MergeList(index).SiaShukeiCode) OrElse StringUtil.IsEmpty(MergeList(index2).SiaShukeiCode) Then
                                                            MergeCore(kyoku1, kyoku2, index, index2, MergeList, hajikuList) '複数回同一判定を行うためリファクタリングを行った
                                                        End If

                                                    End If

                                                Else

                                                    '比較元の国内集計コードが無の場合で、比較先の国内集計コードがある場合
                                                    If MergeList(index).SiaShukeiCode = MergeList(index2).ShukeiCode Then
                                                        MergeCore(kyoku1, kyoku2, index, index2, MergeList, hajikuList)
                                                    End If

                                                End If

                                            Else
                                                '複数回同一判定を行うためリファクタリングを行った

                                                If MergeList(index2).ShukeiCode.TrimEnd <> "" Then

                                                    '両方空でないならチェック'
                                                    If MergeList(index).ShukeiCode = MergeList(index2).ShukeiCode Then '国内集計コードが同一'
                                                        MergeCore(kyoku1, kyoku2, index, index2, MergeList, hajikuList)
                                                    End If

                                                Else
                                                    '比較元の集計コードがあり、比較先の集計コードと一致しない場合
                                                    '比較先の海外集計コードとの一致判定を行い、符号が一致していれば諸条件判定の上マージ
                                                    '※比較元の国内集計コードがある場合はこれを基点として、海外集計コードとの斜め判定を行う
                                                    If MergeList(index).ShukeiCode = MergeList(index2).SiaShukeiCode Then
                                                        MergeCore(kyoku1, kyoku2, index, index2, MergeList, hajikuList)
                                                    Else
                                                        If MergeList(index2).SiaShukeiCode = "" Then
                                                            MergeCore(kyoku1, kyoku2, index, index2, MergeList, hajikuList)
                                                        End If

                                                    End If

                                                End If
                                            End If
                                        End If
                                    End If
                                End If
                            End If
                        End If
                        '↓↓2014/09/26 酒井 ADD BEGIN
                    End If
                    '↑↑2014/09/26 酒井 ADD END
                Next
            Next

            Return MergeList

        End Function

        Private Sub MergeCore(ByVal kyoku1 As String, _
                              ByVal kyoku2 As String, _
                              ByVal index As Integer, _
                              ByVal index2 As Integer, _
                              ByRef MergeList As List(Of TehaichoBuhinEditTmpVo), _
                              ByRef hajikuList As ArrayList)

            '供給セクションを比較'
            If kyoku1 = kyoku2 Then

                '再使用区分が同一か？'
                If MergeList(index).Saishiyoufuka = MergeList(index2).Saishiyoufuka Then

                    '号車表示順が若い方はどっち？'
                    If MergeList(index).ShisakuGousyaHyoujiJun < MergeList(index2).ShisakuGousyaHyoujiJun Then
                        MergeList(index2).BuhinNoHyoujiJun = MergeList(index).BuhinNoHyoujiJun
                        MergeList(index2).CreatedUserId = "Merge"
                    ElseIf MergeList(index).ShisakuGousyaHyoujiJun > MergeList(index2).ShisakuGousyaHyoujiJun Then
                        MergeList(index).BuhinNoHyoujiJun = MergeList(index2).BuhinNoHyoujiJun
                        MergeList(index).CreatedUserId = "Merge"
                    Else
                        '号車表示順が同じでも部品番号表示順が違うパターンがいる'
                        If MergeList(index).BuhinNoHyoujiJun < MergeList(index2).BuhinNoHyoujiJun Then
                            MergeList(index2).BuhinNoHyoujiJun = MergeList(index).BuhinNoHyoujiJun
                            MergeList(index2).CreatedUserId = "Merge"
                        ElseIf MergeList(index).BuhinNoHyoujiJun > MergeList(index2).BuhinNoHyoujiJun Then
                            MergeList(index).BuhinNoHyoujiJun = MergeList(index2).BuhinNoHyoujiJun
                            MergeList(index).CreatedUserId = "Merge"
                        End If
                    End If

                End If

            Else
                '供給セクションが同一でない場合、どちらかの手配記号を空白にする
                Dim key1 As New System.Text.StringBuilder
                With key1
                    .AppendLine(MergeList(index).ShisakuBlockNo)
                    .AppendLine(MergeList(index).BuhinNo)
                    .AppendLine(kyoku1)
                End With

                If hajikuList.Contains(key1.ToString) Then
                    MergeList(index2).TehaiKigou = ""
                Else
                    Dim key2 As New System.Text.StringBuilder
                    With key2
                        .AppendLine(MergeList(index2).ShisakuBlockNo)
                        .AppendLine(MergeList(index2).BuhinNo)
                        .AppendLine(kyoku2)
                    End With
                    If hajikuList.Contains(key2.ToString) Then
                        MergeList(index).TehaiKigou = ""
                    Else
                        MergeList(index2).TehaiKigou = ""
                        hajikuList.Add(key1.ToString)
                    End If
                End If
            End If
        End Sub


        ''' <summary>
        ''' 部品編集TMP情報を部品編集情報に変更する
        ''' </summary>
        ''' <param name="BuhinEditTmpVoList"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Private Function ExchangeBuhinEdit(ByVal BuhinEditTmpVoList As List(Of TShisakuBuhinEditTmpVo)) As List(Of TShisakuBuhinEditVo)
            Dim BuhinEditVoList As New List(Of TShisakuBuhinEditVo)


            For Each buhinEditTmpvo As TShisakuBuhinEditTmpVo In BuhinEditTmpVoList
                Dim BuhinEditVo As New TShisakuBuhinEditVo
                BuhinEditVo.ShisakuEventCode = buhinEditTmpvo.ShisakuEventCode
                BuhinEditVo.ShisakuBukaCode = buhinEditTmpvo.ShisakuBukaCode
                BuhinEditVo.ShisakuBlockNo = buhinEditTmpvo.ShisakuBlockNo
                BuhinEditVo.ShisakuBlockNoKaiteiNo = buhinEditTmpvo.ShisakuBlockNoKaiteiNo
                BuhinEditVo.BuhinNoHyoujiJun = buhinEditTmpvo.BuhinNoHyoujiJun
                BuhinEditVo.Level = buhinEditTmpvo.Level
                BuhinEditVo.ShukeiCode = buhinEditTmpvo.ShukeiCode
                BuhinEditVo.SiaShukeiCode = buhinEditTmpvo.SiaShukeiCode
                BuhinEditVo.GencyoCkdKbn = buhinEditTmpvo.GencyoCkdKbn
                BuhinEditVo.MakerCode = buhinEditTmpvo.MakerCode
                BuhinEditVo.MakerName = buhinEditTmpvo.MakerName
                BuhinEditVo.BuhinNo = buhinEditTmpvo.BuhinNo
                BuhinEditVo.BuhinNoKbn = buhinEditTmpvo.BuhinNoKbn
                BuhinEditVo.BuhinNoKaiteiNo = buhinEditTmpvo.BuhinNoKaiteiNo
                BuhinEditVo.EdaBan = buhinEditTmpvo.EdaBan
                BuhinEditVo.BuhinName = buhinEditTmpvo.BuhinName
                BuhinEditVo.Saishiyoufuka = buhinEditTmpvo.Saishiyoufuka
                BuhinEditVo.ShutuzuYoteiDate = buhinEditTmpvo.ShutuzuYoteiDate
                BuhinEditVo.ZaishituKikaku1 = buhinEditTmpvo.ZaishituKikaku1
                BuhinEditVo.ZaishituKikaku2 = buhinEditTmpvo.ZaishituKikaku2
                BuhinEditVo.ZaishituKikaku3 = buhinEditTmpvo.ZaishituKikaku3
                BuhinEditVo.ZaishituMekki = buhinEditTmpvo.ZaishituMekki
                ''↓↓2014/07/24 Ⅰ.2.管理項目追加_ao) (TES)張 ADD BEGIN
                BuhinEditVo.TsukurikataSeisaku = buhinEditTmpvo.TsukurikataSeisaku
                BuhinEditVo.TsukurikataKatashiyou1 = buhinEditTmpvo.TsukurikataKatashiyou1
                BuhinEditVo.TsukurikataKatashiyou2 = buhinEditTmpvo.TsukurikataKatashiyou2
                BuhinEditVo.TsukurikataKatashiyou3 = buhinEditTmpvo.TsukurikataKatashiyou3
                BuhinEditVo.TsukurikataTigu = buhinEditTmpvo.TsukurikataTigu
                BuhinEditVo.TsukurikataNounyu = buhinEditTmpvo.TsukurikataNounyu
                BuhinEditVo.TsukurikataKibo = buhinEditTmpvo.TsukurikataKibo
                ''↑↑2014/07/24 Ⅰ.2.管理項目追加_ao) (TES)張 ADD END
                '↓↓2014/09/26 酒井 ADD BEGIN
                BuhinEditVo.BaseBuhinFlg = buhinEditTmpvo.BaseBuhinFlg
                '↑↑2014/09/26 酒井 ADD END
                BuhinEditVo.ShisakuBankoSuryo = buhinEditTmpvo.ShisakuBankoSuryo
                BuhinEditVo.ShisakuBankoSuryoU = buhinEditTmpvo.ShisakuBankoSuryoU
                ''↓↓2014/12/26 18手配帳作成 (TES)張 ADD BEGIN
                BuhinEditVo.MaterialInfoLength = buhinEditTmpvo.MaterialInfoLength
                BuhinEditVo.MaterialInfoWidth = buhinEditTmpvo.MaterialInfoWidth
                BuhinEditVo.DataItemKaiteiNo = buhinEditTmpvo.DataItemKaiteiNo
                BuhinEditVo.DataItemAreaName = buhinEditTmpvo.DataItemAreaName
                BuhinEditVo.DataItemSetName = buhinEditTmpvo.DataItemSetName
                BuhinEditVo.DataItemKaiteiInfo = buhinEditTmpvo.DataItemKaiteiInfo
                ''↑↑2014/12/26 18手配帳作成 (TES)張 ADD END
                BuhinEditVo.ShisakuBuhinHi = buhinEditTmpvo.ShisakuBuhinHi
                BuhinEditVo.ShisakuKataHi = buhinEditTmpvo.ShisakuKataHi
                BuhinEditVo.Bikou = buhinEditTmpvo.Bikou
                BuhinEditVo.KyoukuSection = buhinEditTmpvo.KyoukuSection

                BuhinEditVoList.Add(BuhinEditVo)
            Next

            Return BuhinEditVoList
        End Function




#Region "コンボボックスの処理"
        'グループNo'
        Private Class GroupNoExtraction : Implements ILabelValueExtraction
            Public Sub Extraction(ByVal aLocator As ShisakuCommon.Util.LabelValue.LabelValueLocator) Implements ShisakuCommon.Util.LabelValue.ILabelValueExtraction.Extraction
                Dim vo As New TShisakuEventKanseiVo
                aLocator.IsA(vo).Label(vo.ShisakuGroup).Value(vo.ShisakuGroup)
            End Sub
        End Class

        Public Function GetLabelValues_GroupNo() As List(Of LabelValueVo)
            If GroupNo Is String.Empty Then
                Return New List(Of LabelValueVo)
            End If
            Dim vo As New TShisakuEventKanseiVo
            vo.ShisakuGroup = GroupNo
            Dim results As List(Of LabelValueVo) = _
                LabelValueExtracter(Of TShisakuEventKanseiVo).Extract(tehaichoSakusei.FindByGroupNoList(ShisakuEventCode), New GroupNoExtraction)
            results.Sort(New LabelValueComparer)
            Return results
        End Function

        '製品区分SKMSだとS_K_M_Sに変換される'
        Private Class SeihinKbnExtraction : Implements ILabelValueExtraction
            Public Sub Extraction(ByVal aLocator As ShisakuCommon.Util.LabelValue.LabelValueLocator) Implements ShisakuCommon.Util.LabelValue.ILabelValueExtraction.Extraction
                Dim vo As New AsSKMSVo
                aLocator.IsA(vo).Label(vo.Snkm).Value(vo.Snkm)
            End Sub
        End Class

        Public Function GetLabelValues_SeihinKbn() As List(Of LabelValueVo)
            If SeihinKbn Is String.Empty Then
                Return New List(Of LabelValueVo)
            End If
            Dim vo As New AsSKMSVo
            vo.Snkm = SeihinKbn
            Dim results As List(Of LabelValueVo) = _
                LabelValueExtracter(Of AsSKMSVo).Extract(tehaichoSakusei.FindByAsSNKM, New SeihinKbnExtraction)
            results.Sort(New LabelValueComparer)
            Return results
        End Function

        Public Function GetLabelValues_UnitKbn() As List(Of LabelValueVo)
            Dim results As New List(Of LabelValueVo)
            results.Add(New LabelValueVo("T", "T"))
            results.Add(New LabelValueVo("M", "M"))
            Return results
        End Function

#End Region

#Region "公開プロパティ"

        '' 試作イベントコード
        Private _ShisakuEventCode As String
        '' グループNo
        Private _GroupNo As String
        '' 工事区分
        Private _KoujiKbn As String
        '' 製品区分
        Private _SeihinKbn As String
        '' ユニット区分
        Private _UnitKbn As String
        '' 工事指令No
        Private _KoujiShireiNo As String
        '' 工事No
        Private _KoujiNo As String
        '' イベント名称
        Private _EventName As String
        '' 自給品の消しこみする
        Private _JikyuSuru As Boolean
        '' 自給品の消しこみしない
        Private _JikyuShinai As Boolean
        '' 織込みする
        Private _OrikomiSuru As Boolean
        '' 織込みしない
        Private _OrikomiShinai As Boolean
        '' 集計コードからの展開する
        Private _SyukeiSuru As Boolean
        '' 集計コードからの展開しない
        Private _SyukeiShinai As Boolean

#End Region

#Region "公開プロパティの実装"
        ''' <summary>試作イベントコード</summary>
        ''' <value>試作イベントコード</value>
        ''' <returns>試作イベントコード</returns>
        Public Property ShisakuEventCode() As String
            Get
                Return _ShisakuEventCode
            End Get
            Set(ByVal value As String)
                _ShisakuEventCode = value
            End Set
        End Property

        ''' <summary>グループNo</summary>
        ''' <returns>グループNo</returns>
        Public Property GroupNo() As String
            Get
                Return _GroupNo
            End Get
            Set(ByVal value As String)
                '_GroupNo = value

                If EzUtil.IsEqualIfNull(_GroupNo, value) Then
                    Return
                End If
                _GroupNo = value
                SetChanged()

                GroupNoLabelValues() = GetLabelValues_GroupNo()
            End Set
        End Property

        ''' <summary>工事区分</summary>
        ''' <returns>工事区分</returns>
        Public Property KoujiKbn() As String
            Get
                Return _KoujiKbn
            End Get
            Set(ByVal value As String)
                _KoujiKbn = value
            End Set
        End Property

        ''' <summary>製品区分</summary>
        ''' <returns>製品区分</returns>
        Public Property SeihinKbn() As String
            Get
                Return _SeihinKbn
            End Get
            Set(ByVal value As String)
                _SeihinKbn = value
                SetChanged()
            End Set
        End Property

        ''' <summary>ユニット区分</summary>
        ''' <returns>ユニット区分</returns>
        Public Property UnitKbn() As String
            Get
                Return _UnitKbn
            End Get
            Set(ByVal value As String)
                _UnitKbn = value
                SetChanged()
            End Set
        End Property

        ''' <summary>工事指令No</summary>
        ''' <returns>工事指令No</returns>
        Public Property KoujiShireiNo() As String
            Get
                Return _KoujiShireiNo
            End Get
            Set(ByVal value As String)
                _KoujiShireiNo = value
            End Set
        End Property

        ''' <summary>工事No</summary>
        ''' <value>工事No</value>
        ''' <returns>工事No</returns>
        Public Property KoujiNo() As String
            Get
                Return _KoujiNo
            End Get
            Set(ByVal value As String)
                _KoujiNo = value
            End Set
        End Property

        ''' <summary>イベント名称</summary>
        ''' <value>イベント名称</value>
        ''' <returns>イベント名称</returns>
        Public Property EventName() As String
            Get
                Return _EventName
            End Get
            Set(ByVal value As String)
                _EventName = value
            End Set
        End Property

        ''' <summary>自給品消しこみする</summary>
        ''' <value>自給品消しこみする</value>
        ''' <returns>自給品消しこみする</returns>
        Public Property JikyuSuru() As Boolean
            Get
                Return _JikyuSuru
            End Get
            Set(ByVal value As Boolean)
                _JikyuSuru = value
            End Set
        End Property

        ''' <summary>自給品消しこみしない</summary>
        ''' <value>自給品消しこみしない</value>
        ''' <returns>自給品消しこみしない</returns>
        Public Property JikyuShinai() As Boolean
            Get
                Return _JikyuShinai
            End Get
            Set(ByVal value As Boolean)
                _JikyuShinai = value
            End Set
        End Property

        ''' <summary>織込みする</summary>
        ''' <value>織込みする</value>
        ''' <returns>織込みする</returns>
        Public Property OrikomiSuru() As Boolean
            Get
                Return _OrikomiSuru
            End Get
            Set(ByVal value As Boolean)
                _OrikomiSuru = value
            End Set
        End Property

        ''' <summary>織込みしない</summary>
        ''' <value>織込みしない</value>
        ''' <returns>織込みしない</returns>
        Public Property OrikomiShinai() As Boolean
            Get
                Return _OrikomiShinai
            End Get
            Set(ByVal value As Boolean)
                _OrikomiShinai = value
            End Set
        End Property

        ''' <summary>集計コードからの展開する</summary>
        ''' <value>集計コードからの展開する</value>
        ''' <returns>集計コードからの展開する</returns>
        Public Property SyukeiSuru() As Boolean
            Get
                Return _SyukeiSuru
            End Get
            Set(ByVal value As Boolean)
                _SyukeiSuru = value
            End Set
        End Property

        ''' <summary>集計コードからの展開しない</summary>
        ''' <value>集計コードからの展開しない</value>
        ''' <returns>集計コードからの展開しない</returns>
        Public Property SyukeiShinai() As Boolean
            Get
                Return _SyukeiShinai
            End Get
            Set(ByVal value As Boolean)
                _SyukeiShinai = value
            End Set
        End Property

#End Region

#Region "公開プロパティの選択値"
        '' グループNoの選択値
        Private _GroupNoLabelValues As List(Of LabelValueVo)
        '' 製品区分の選択値
        Private _SeihinKbnLabelValues As List(Of LabelValueVo)
        '' ユニット区分の選択値
        Private _UnitKbnLabelValues As List(Of LabelValueVo)

        ''' <summary>グループNoの選択値</summary>
        ''' <value>グループNoの選択値</value>
        ''' <returns>グループNoの選択値</returns>
        Public Property GroupNoLabelValues() As List(Of LabelValueVo)
            Get
                Return _GroupNoLabelValues
            End Get
            Set(ByVal value As List(Of LabelValueVo))
                _GroupNoLabelValues = value
            End Set
        End Property

        ''' <summary>製品区分の選択値</summary>
        ''' <value>製品区分の選択値</value>
        ''' <returns>製品区分の選択値</returns>
        Public Property SeihinKbnLabelValues() As List(Of LabelValueVo)
            Get
                Return _SeihinKbnLabelValues
            End Get
            Set(ByVal value As List(Of LabelValueVo))
                _SeihinKbnLabelValues = value
            End Set
        End Property

        ''' <summary>ユニット区分の選択値</summary>
        ''' <value>ユニット区分の選択値</value>
        ''' <returns>ユニット区分の選択値</returns>
        Public Property UnitKbnLabelValues() As List(Of LabelValueVo)
            Get
                Return _UnitKbnLabelValues
            End Get
            Set(ByVal value As List(Of LabelValueVo))
                _UnitKbnLabelValues = value
            End Set
        End Property

#End Region

    End Class

End Namespace