Imports EventSakusei.YosanSetteiBuhinEdit.Dao
Imports ShisakuCommon.Db.EBom.Vo
Imports ShisakuCommon
Imports ShisakuCommon.Db.EBom
Imports EventSakusei.YosanSetteiBuhinSakusei.Dao

Namespace YosanSetteiBuhinEdit.Logic

    ''' <summary>
    ''' 部品表比較して最新化するクラス
    ''' </summary>
    ''' <remarks></remarks>
    Public Class YosanCompareLatest

#Region "プライベート変数"

        Private _ShisakuEventCode As String
        Private _YosanListCode As String
        Private dao As YosanSetteiBuhinEditHeaderDao
        Private _CompareList As List(Of TYosanSetteiBuhinVo)
        Private _HistoryList As List(Of TYosanSetteiBuhinRirekiVo)
        Private _ChangeBlockList As List(Of String) '比較して変更が存在したブロックのリスト

#End Region

#Region "定数"

        ''' <summary>追加フラグ</summary>
        Private Const ADD_FLAG As String = "A"

        ''' <summary>削除フラグ</summary>
        Private Const DEL_FLAG As String = "D"

        ''' <summary>変更フラグ</summary>
        Private Const CHANGE_FLAG As String = "C"

#End Region

#Region "コンストラクタ"

        ''' <summary>
        ''' コンストラクタ
        ''' </summary>
        ''' <param name="shisakuEventCode">試作イベントコード</param>
        ''' <remarks></remarks>
        Public Sub New(ByVal shisakuEventCode As String, _
                       ByVal yosanListCode As String)

            _ShisakuEventCode = shisakuEventCode
            _YosanListCode = yosanListCode
            dao = New YosanSetteiBuhinEditHeaderDaoImpl
            _CompareList = New List(Of TYosanSetteiBuhinVo)
            _HistoryList = New List(Of TYosanSetteiBuhinRirekiVo)
            _ChangeBlockList = New List(Of String)

        End Sub

#End Region



        ''' <summary>
        ''' 比較(改訂抽出と同じ？)
        ''' </summary>
        ''' <remarks></remarks>
        Public Sub Compare()
            '現在のT_YOSAN_SETTEI_BUHINの内容と比較する。'


            '現在のT_YOSAN_SETTEI_BUHINを取得する'
            Dim vos As List(Of TYosanSetteiBuhinVo) = dao.FindByTYosanSetteiBuhin(_ShisakuEventCode, _YosanListCode)
            '最初に今までの履歴格納'
            _HistoryList = dao.FindByTYosanSetteiBuhinRireki(_ShisakuEventCode, _YosanListCode)


            '比較用の情報を部品編集表から取得'
            'Dim latestVos As List(Of YosanSetteiBuhinEditTmpVo) = dao.FindByYosanSetteiBuhinEditTmp(_ShisakuEventCode)
            Dim latestVos As New List(Of TYosanSetteiBuhinVo)
            latestVos = MakeCompareList()


            '比較条件1'
            'ブロック№と部品番号'
            Dim blockLevelBuhinKyokuDic As New Dictionary(Of String, TYosanSetteiBuhinVo)    'ブロックとレベルと部品番号と集計コードと供給セクション
            Dim blockList As New List(Of String)

            '現状'
            For Each vo As TYosanSetteiBuhinVo In vos
                '前回での対象フラグを消す。(削除はそのまま)'
                If StringUtil.Equals(vo.AudFlag, ADD_FLAG) _
                OrElse StringUtil.Equals(vo.AudFlag, CHANGE_FLAG) Then
                    vo.AudFlag = ""
                End If

                If StringUtil.IsEmpty(vo.YosanShukeiCode) Then
                    vo.YosanShukeiCode = ""
                End If
                If StringUtil.IsEmpty(vo.YosanSiaShukeiCode) Then
                    vo.YosanSiaShukeiCode = ""
                End If

                'NULLと空は別物'
                If StringUtil.IsEmpty(vo.YosanKyoukuSection) Then
                    vo.YosanKyoukuSection = ""
                End If

                Dim key As String = EzUtil.MakeKey(vo.YosanBlockNo.Trim, vo.YosanLevel, vo.YosanBuhinNo.Trim, vo.YosanShukeiCode, vo.YosanSiaShukeiCode, vo.YosanKyoukuSection.Trim)

                If Not blockLevelBuhinKyokuDic.ContainsKey(key) Then
                    blockLevelBuhinKyokuDic.Add(key, New TYosanSetteiBuhinVo)
                End If
                blockLevelBuhinKyokuDic(key) = vo

            Next

            Dim blockLevelBuhinKyokuDic2 As New Dictionary(Of String, TYosanSetteiBuhinVo)   'ブロック№とレベルと部品と供給セクション
            
            '比較その１変更と追加'
            '最新から見て既存がどう変化したか'
            For Each vo As TYosanSetteiBuhinVo In latestVos

                Dim key As String = EzUtil.MakeKey(vo.YosanBlockNo.Trim, vo.YosanLevel, vo.YosanBuhinNo.Trim, vo.YosanShukeiCode, vo.YosanSiaShukeiCode, vo.YosanKyoukuSection.Trim)

                '予算設定部品表内に存在チェック'
                If blockLevelBuhinKyokuDic.ContainsKey(key) Then
                    '存在するので比較対象'
                    '比較条件2'
                    'レベル、国内集計、海外集計、部品名称、総数、取引先コード、購担、製作方法、型仕様、治具'
                    '部品製作規模・概要、試作部品費(円)、試作型費(千円)、備考'
                    '国外区分、MIXコスト部品費(円/ｾﾝﾄ)、引用元情報'
                    'を比較。差異があれば変更扱い'

                    '国外区分、MIXコスト部品費(円/ｾﾝﾄ)、引用元情報はどうしよう・・・？'
                    Dim addVo As TYosanSetteiBuhinVo = DataCompare(vo, blockLevelBuhinKyokuDic(key))
                    _CompareList.Add(addVo)
                Else

                    '最新キーで既存を見たらいなかった'
                    '最新側にしかいないので追加扱い'
                    vo.YosanListCode = _YosanListCode
                    vo.YosanKonkyoKeisu1 = 1.0
                    vo.YosanWaritukeKeisu2 = 1.0
                    vo.AudFlag = ADD_FLAG

                    '後で振るのでとりあえず設定'
                    vo.YosanGyouId = ""

                    _CompareList.Add(vo)
                    If Not ChangeBlockList.Contains(vo.YosanBlockNo) Then
                        _ChangeBlockList.Add(vo.YosanBlockNo)
                    End If
                End If

                If Not blockList.Contains(vo.YosanBlockNo) Then
                    blockList.Add(vo.YosanBlockNo)
                End If

                If Not blockLevelBuhinKyokuDic2.ContainsKey(key) Then
                    blockLevelBuhinKyokuDic2.Add(key, vo)
                End If
            Next


            '既存で探索'
            For Each key As String In blockLevelBuhinKyokuDic.Keys
                If Not blockLevelBuhinKyokuDic2.ContainsKey(key) Then
                    'ブロック自体無かったら無視'
                    If blockList.Contains(blockLevelBuhinKyokuDic(key).YosanBlockNo) Then
                        '既存にあって最新にない'
                        '削除'
                        Dim vo As TYosanSetteiBuhinVo = blockLevelBuhinKyokuDic(key)
                        vo.AudFlag = DEL_FLAG
                        _CompareList.Add(vo)
                        If Not ChangeBlockList.Contains(vo.YosanBlockNo) Then
                            ChangeBlockList.Add(vo.YosanBlockNo)
                        End If
                    Else
                        ''無視される項目は変更一切なし'
                        _CompareList.Add(blockLevelBuhinKyokuDic(key))
                    End If
                End If
            Next

            '追加扱いの項目に対して、部品番号表示順、ソート順、行IDを設定''
            SetSort()

        End Sub

        ''' <summary>
        ''' 同一データの存在チェック
        ''' </summary>
        ''' <param name="checkVo">最新VO(今回値)</param>
        ''' <param name="vo">画面のVO(前回値)</param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Private Function DataCompare(ByVal checkVo As TYosanSetteiBuhinVo, ByVal vo As TYosanSetteiBuhinVo) As TYosanSetteiBuhinVo
            '変化点があればこの処理内で履歴用リストにvoを格納する'
            Dim result As New TYosanSetteiBuhinVo
            Dim isChange As Boolean = False

            '画面VoのフラグがDならそのままを返す'
            If StringUtil.Equals(vo.AudFlag, DEL_FLAG) Then
                Return vo
            End If

            result.ShisakuEventCode = vo.ShisakuEventCode
            result.YosanListCode = vo.YosanListCode
            result.YosanBukaCode = vo.YosanBukaCode
            result.YosanBlockNo = vo.YosanBlockNo
            result.BuhinNoHyoujiJun = vo.BuhinNoHyoujiJun
            result.YosanSortJun = vo.YosanSortJun
            result.YosanGyouId = vo.YosanGyouId
            result.YosanBuhinNo = vo.YosanBuhinNo
            result.YosanLevel = vo.YosanLevel

            '国内集計、海外集計、部品名称、総数、取引先コード、購担、製作方法、型仕様、治具'
            '部品製作規模・概要、試作部品費(円)、試作型費(千円)、NOTE、備考'
            '国外区分、MIXコスト部品費(円/ｾﾝﾄ)、引用元情報'
            'If StringUtil.Equals(StringUtil.Nvl(checkVo.YosanLevel), StringUtil.Nvl(vo.YosanLevel)) Then
            '    result.YosanLevel = vo.YosanLevel
            'Else
            '    result.YosanLevel = checkVo.YosanLevel
            '    isChange = True
            '    'ID不明なのでテキトー'
            '    CreateAddRirekiVo(result, "0001", NmSpdTagBase.TAG_YOSAN_LEVEL, vo.YosanLevel, checkVo.YosanLevel)
            'End If
            '集計コード'
            If StringUtil.Equals(StringUtil.Nvl(checkVo.YosanShukeiCode), StringUtil.Nvl(vo.YosanShukeiCode)) Then
                result.YosanShukeiCode = vo.YosanShukeiCode
            Else
                result.YosanShukeiCode = checkVo.YosanShukeiCode
                isChange = True
                CreateAddRirekiVo(result, "5001", NmSpdTagBase.TAG_YOSAN_SHUKEI_CODE, vo.YosanShukeiCode, checkVo.YosanShukeiCode)
            End If
            '海外集計コード'
            If StringUtil.Equals(StringUtil.Nvl(checkVo.YosanSiaShukeiCode), StringUtil.Nvl(vo.YosanSiaShukeiCode)) Then
                result.YosanSiaShukeiCode = vo.YosanSiaShukeiCode
            Else
                result.YosanSiaShukeiCode = checkVo.YosanSiaShukeiCode
                CreateAddRirekiVo(result, "5002", NmSpdTagBase.TAG_YOSAN_SIA_SHUKEI_CODE, vo.YosanSiaShukeiCode, checkVo.YosanSiaShukeiCode)
                isChange = True
            End If
            '部品名称'
            If StringUtil.Equals(StringUtil.Nvl(checkVo.YosanBuhinName).TrimEnd, StringUtil.Nvl(vo.YosanBuhinName).TrimEnd) Then
                result.YosanBuhinName = vo.YosanBuhinName
            Else
                result.YosanBuhinName = checkVo.YosanBuhinName
                isChange = True
                CreateAddRirekiVo(result, "5004", NmSpdTagBase.TAG_YOSAN_BUHIN_NAME, vo.YosanBuhinName, checkVo.YosanBuhinName)
            End If
            '員数'
            '----------------------------------------
            '員数がマイナスの場合-1を設定する。
            '----------------------------------------
            If checkVo.YosanInsu < 0 Then
                checkVo.YosanInsu = -1
            End If
            '----------------------------------------

            If StringUtil.Equals(StringUtil.Nvl(checkVo.YosanInsu), StringUtil.Nvl(vo.YosanInsu)) Then
                result.YosanInsu = vo.YosanInsu
            Else
                result.YosanInsu = checkVo.YosanInsu
                isChange = True
                CreateAddRirekiVo(result, "5005", NmSpdTagBase.TAG_YOSAN_INSU, vo.YosanInsu, checkVo.YosanInsu)
            End If

            '取引先コード'
            If StringUtil.Equals(StringUtil.Nvl(checkVo.YosanMakerCode), StringUtil.Nvl(vo.YosanMakerCode)) Then
                result.YosanMakerCode = vo.YosanMakerCode
            Else
                result.YosanMakerCode = checkVo.YosanMakerCode
                isChange = True
                CreateAddRirekiVo(result, "5006", NmSpdTagBase.TAG_YOSAN_MAKER_CODE, vo.YosanMakerCode, checkVo.YosanMakerCode)
            End If

            '供給セクションは無視'
            '手配記号は無視'
            result.YosanKyoukuSection = vo.YosanKyoukuSection

            '購担'
            If StringUtil.Equals(StringUtil.Nvl(checkVo.YosanKoutan), StringUtil.Nvl(vo.YosanKoutan)) Then
                result.YosanKoutan = vo.YosanKoutan
            Else
                result.YosanKoutan = checkVo.YosanKoutan
                isChange = True
                CreateAddRirekiVo(result, "5008", NmSpdTagBase.TAG_YOSAN_KOUTAN, vo.YosanKoutan, checkVo.YosanKoutan)
            End If

            '手配記号は無視'
            result.YosanTehaiKigou = vo.YosanTehaiKigou

            '製作方法'
            If StringUtil.Equals(StringUtil.Nvl(checkVo.YosanTsukurikataSeisaku), StringUtil.Nvl(vo.YosanTsukurikataSeisaku)) Then
                result.YosanTsukurikataSeisaku = vo.YosanTsukurikataSeisaku
            Else
                result.YosanTsukurikataSeisaku = checkVo.YosanTsukurikataSeisaku
                isChange = True
                CreateAddRirekiVo(result, "5010", NmSpdTagBase.TAG_YOSAN_TSUKURIKATA_SEISAKU, vo.YosanTsukurikataSeisaku, checkVo.YosanTsukurikataSeisaku)
            End If
            '型仕様'
            If StringUtil.Equals(StringUtil.Nvl(checkVo.YosanTsukurikataKatashiyou1), StringUtil.Nvl(vo.YosanTsukurikataKatashiyou1)) Then
                result.YosanTsukurikataKatashiyou1 = vo.YosanTsukurikataKatashiyou1
            Else
                result.YosanTsukurikataKatashiyou1 = checkVo.YosanTsukurikataKatashiyou1
                isChange = True
                CreateAddRirekiVo(result, "5011", NmSpdTagBase.TAG_YOSAN_TSUKURIKATA_KATASHIYOU_1, vo.YosanTsukurikataKatashiyou1, checkVo.YosanTsukurikataKatashiyou1)
            End If
            '型仕様2'
            If StringUtil.Equals(StringUtil.Nvl(checkVo.YosanTsukurikataKatashiyou2), StringUtil.Nvl(vo.YosanTsukurikataKatashiyou2)) Then
                result.YosanTsukurikataKatashiyou2 = vo.YosanTsukurikataKatashiyou2
            Else
                result.YosanTsukurikataKatashiyou2 = checkVo.YosanTsukurikataKatashiyou2
                isChange = True
                CreateAddRirekiVo(result, "5012", NmSpdTagBase.TAG_YOSAN_TSUKURIKATA_KATASHIYOU_2, vo.YosanTsukurikataKatashiyou2, checkVo.YosanTsukurikataKatashiyou2)
            End If
            '型仕様3'
            If StringUtil.Equals(StringUtil.Nvl(checkVo.YosanTsukurikataKatashiyou3), StringUtil.Nvl(vo.YosanTsukurikataKatashiyou3)) Then
                result.YosanTsukurikataKatashiyou3 = vo.YosanTsukurikataKatashiyou3
            Else
                result.YosanTsukurikataKatashiyou3 = checkVo.YosanTsukurikataKatashiyou3
                isChange = True
                CreateAddRirekiVo(result, "5013", NmSpdTagBase.TAG_YOSAN_TSUKURIKATA_KATASHIYOU_3, vo.YosanTsukurikataKatashiyou3, checkVo.YosanTsukurikataKatashiyou3)
            End If

            '治具'
            If StringUtil.Equals(StringUtil.Nvl(checkVo.YosanTsukurikataTigu), StringUtil.Nvl(vo.YosanTsukurikataTigu)) Then
                result.YosanTsukurikataTigu = vo.YosanTsukurikataTigu
            Else
                result.YosanTsukurikataTigu = checkVo.YosanTsukurikataTigu
                isChange = True
                CreateAddRirekiVo(result, "5014", NmSpdTagBase.TAG_YOSAN_TSUKURIKATA_TIGU, vo.YosanTsukurikataTigu, checkVo.YosanTsukurikataTigu)
            End If
            '部品製作規模・概要'
            If StringUtil.Equals(StringUtil.Nvl(checkVo.YosanTsukurikataKibo), StringUtil.Nvl(vo.YosanTsukurikataKibo)) Then
                result.YosanTsukurikataKibo = vo.YosanTsukurikataKibo
            Else
                result.YosanTsukurikataKibo = checkVo.YosanTsukurikataKibo
                isChange = True
                CreateAddRirekiVo(result, "5015", NmSpdTagBase.TAG_YOSAN_TSUKURIKATA_KIBO, vo.YosanTsukurikataKibo, checkVo.YosanTsukurikataKibo)
            End If
            '試作部品費'
            If StringUtil.Equals(StringUtil.Nvl(checkVo.YosanShisakuBuhinHi), StringUtil.Nvl(vo.YosanShisakuBuhinHi)) Then
                result.YosanShisakuBuhinHi = vo.YosanShisakuBuhinHi
            Else
                result.YosanShisakuBuhinHi = checkVo.YosanShisakuBuhinHi
                isChange = True
                CreateAddRirekiVo(result, "5016", NmSpdTagBase.TAG_YOSAN_SHISAKU_BUHIN_HI, vo.YosanShisakuBuhinHi, checkVo.YosanShisakuBuhinHi)
            End If
            '試作型費'
            If StringUtil.Equals(StringUtil.Nvl(checkVo.YosanShisakuKataHi), StringUtil.Nvl(vo.YosanShisakuKataHi)) Then
                result.YosanShisakuKataHi = vo.YosanShisakuKataHi
            Else
                result.YosanShisakuKataHi = checkVo.YosanShisakuKataHi
                isChange = True
                CreateAddRirekiVo(result, "5017", NmSpdTagBase.TAG_YOSAN_SHISAKU_KATA_HI, vo.YosanShisakuKataHi, checkVo.YosanShisakuKataHi)
            End If
            'NOTE'
            If StringUtil.Equals(StringUtil.Nvl(checkVo.YosanBuhinNote).Trim, StringUtil.Nvl(vo.YosanBuhinNote).Trim) Then
                result.YosanBuhinNote = vo.YosanBuhinNote
            Else
                result.YosanBuhinNote = checkVo.YosanBuhinNote
                isChange = True
                CreateAddRirekiVo(result, "5018", NmSpdTagBase.TAG_YOSAN_BUHIN_NOTE, vo.YosanBuhinNote, checkVo.YosanBuhinNote)
            End If
            '備考'
            If StringUtil.Equals(StringUtil.Nvl(checkVo.YosanBikou).Trim, StringUtil.Nvl(vo.YosanBikou).Trim) Then
                result.YosanBikou = vo.YosanBikou
            Else
                result.YosanBikou = checkVo.YosanBikou
                isChange = True
                CreateAddRirekiVo(result, "5019", NmSpdTagBase.TAG_YOSAN_BIKOU, vo.YosanBikou, checkVo.YosanBikou)
            End If

            result.YosanKonkyoKokugaiKbn = vo.YosanKonkyoKokugaiKbn
            result.YosanKonkyoMixBuhinHi = vo.YosanKonkyoMixBuhinHi
            result.YosanKonkyoInyouMixBuhinHi = vo.YosanKonkyoInyouMixBuhinHi
            result.YosanKonkyoKeisu1 = vo.YosanKonkyoKeisu1
            result.YosanKonkyoKouhou = vo.YosanKonkyoKouhou
            result.YosanWaritukeBuhinHi = vo.YosanWaritukeBuhinHi
            result.YosanWaritukeKeisu2 = vo.YosanWaritukeKeisu2

            result.Kako1RyosanTanka = vo.Kako1RyosanTanka
            result.Kako1WaritukeBuhinHi = vo.Kako1WaritukeBuhinHi
            result.Kako1WaritukeKataHi = vo.Kako1WaritukeKataHi
            result.Kako1WaritukeKouhou = vo.Kako1WaritukeKouhou
            result.Kako1MakerBuhinHi = vo.Kako1MakerBuhinHi
            result.Kako1MakerKataHi = vo.Kako1MakerKataHi
            result.Kako1MakerKouhou = vo.Kako1MakerKouhou
            result.Kako1ShingiBuhinHi = vo.Kako1ShingiBuhinHi
            result.Kako1ShingiKataHi = vo.Kako1ShingiKataHi
            result.Kako1ShingiKouhou = vo.Kako1ShingiKouhou
            result.Kako1KounyuKibouTanka = vo.Kako1KounyuKibouTanka
            result.Kako1KounyuTanka = vo.Kako1KounyuTanka
            result.Kako1ShikyuHin = vo.Kako1ShikyuHin
            result.Kako1KoujiShireiNo = vo.Kako1KoujiShireiNo
            result.Kako1EventName = vo.Kako1EventName
            result.Kako1HachuBi = vo.Kako1HachuBi
            result.Kako1KenshuBi = vo.Kako1KenshuBi

            result.Kako2RyosanTanka = vo.Kako2RyosanTanka
            result.Kako2WaritukeBuhinHi = vo.Kako2WaritukeBuhinHi
            result.Kako2WaritukeKataHi = vo.Kako2WaritukeKataHi
            result.Kako2WaritukeKouhou = vo.Kako2WaritukeKouhou
            result.Kako2MakerBuhinHi = vo.Kako2MakerBuhinHi
            result.Kako2MakerKataHi = vo.Kako2MakerKataHi
            result.Kako2MakerKouhou = vo.Kako2MakerKouhou
            result.Kako2ShingiBuhinHi = vo.Kako2ShingiBuhinHi
            result.Kako2ShingiKataHi = vo.Kako2ShingiKataHi
            result.Kako2ShingiKouhou = vo.Kako2ShingiKouhou
            result.Kako2KounyuKibouTanka = vo.Kako2KounyuKibouTanka
            result.Kako2KounyuTanka = vo.Kako2KounyuTanka
            result.Kako2ShikyuHin = vo.Kako2ShikyuHin
            result.Kako2KoujiShireiNo = vo.Kako2KoujiShireiNo
            result.Kako2EventName = vo.Kako2EventName
            result.Kako2HachuBi = vo.Kako2HachuBi
            result.Kako2KenshuBi = vo.Kako2KenshuBi


            result.Kako3RyosanTanka = vo.Kako3RyosanTanka
            result.Kako3WaritukeBuhinHi = vo.Kako3WaritukeBuhinHi
            result.Kako3WaritukeKataHi = vo.Kako3WaritukeKataHi
            result.Kako3WaritukeKouhou = vo.Kako3WaritukeKouhou
            result.Kako3MakerBuhinHi = vo.Kako3MakerBuhinHi
            result.Kako3MakerKataHi = vo.Kako3MakerKataHi
            result.Kako3MakerKouhou = vo.Kako3MakerKouhou
            result.Kako3ShingiBuhinHi = vo.Kako3ShingiBuhinHi
            result.Kako3ShingiKataHi = vo.Kako3ShingiKataHi
            result.Kako3ShingiKouhou = vo.Kako3ShingiKouhou
            result.Kako3KounyuKibouTanka = vo.Kako3KounyuKibouTanka
            result.Kako3KounyuTanka = vo.Kako3KounyuTanka
            result.Kako3ShikyuHin = vo.Kako3ShikyuHin
            result.Kako3KoujiShireiNo = vo.Kako3KoujiShireiNo
            result.Kako3EventName = vo.Kako3EventName
            result.Kako3HachuBi = vo.Kako3HachuBi
            result.Kako3KenshuBi = vo.Kako3KenshuBi


            result.Kako4RyosanTanka = vo.Kako4RyosanTanka
            result.Kako4WaritukeBuhinHi = vo.Kako4WaritukeBuhinHi
            result.Kako4WaritukeKataHi = vo.Kako4WaritukeKataHi
            result.Kako4WaritukeKouhou = vo.Kako4WaritukeKouhou
            result.Kako4MakerBuhinHi = vo.Kako4MakerBuhinHi
            result.Kako4MakerKataHi = vo.Kako4MakerKataHi
            result.Kako4MakerKouhou = vo.Kako4MakerKouhou
            result.Kako4ShingiBuhinHi = vo.Kako4ShingiBuhinHi
            result.Kako4ShingiKataHi = vo.Kako4ShingiKataHi
            result.Kako4ShingiKouhou = vo.Kako4ShingiKouhou
            result.Kako4KounyuKibouTanka = vo.Kako4KounyuKibouTanka
            result.Kako4KounyuTanka = vo.Kako4KounyuTanka
            result.Kako4ShikyuHin = vo.Kako4ShikyuHin
            result.Kako4KoujiShireiNo = vo.Kako4KoujiShireiNo
            result.Kako4EventName = vo.Kako4EventName
            result.Kako4HachuBi = vo.Kako4HachuBi
            result.Kako4KenshuBi = vo.Kako4KenshuBi


            result.Kako5RyosanTanka = vo.Kako5RyosanTanka
            result.Kako5WaritukeBuhinHi = vo.Kako5WaritukeBuhinHi
            result.Kako5WaritukeKataHi = vo.Kako5WaritukeKataHi
            result.Kako5WaritukeKouhou = vo.Kako5WaritukeKouhou
            result.Kako5MakerBuhinHi = vo.Kako5MakerBuhinHi
            result.Kako5MakerKataHi = vo.Kako5MakerKataHi
            result.Kako5MakerKouhou = vo.Kako5MakerKouhou
            result.Kako5ShingiBuhinHi = vo.Kako5ShingiBuhinHi
            result.Kako5ShingiKataHi = vo.Kako5ShingiKataHi
            result.Kako5ShingiKouhou = vo.Kako5ShingiKouhou
            result.Kako5KounyuKibouTanka = vo.Kako5KounyuKibouTanka
            result.Kako5KounyuTanka = vo.Kako5KounyuTanka
            result.Kako5ShikyuHin = vo.Kako5ShikyuHin
            result.Kako5KoujiShireiNo = vo.Kako5KoujiShireiNo
            result.Kako5EventName = vo.Kako5EventName
            result.Kako5HachuBi = vo.Kako5HachuBi
            result.Kako5KenshuBi = vo.Kako5KenshuBi


            result.YosanWaritukeBuhinHiTotal = vo.YosanWaritukeBuhinHiTotal
            result.YosanWaritukeKataHi = vo.YosanWaritukeKataHi
            result.YosanKounyuKibouTanka = vo.YosanKounyuKibouTanka
            result.YosanKounyuKibouBuhinHi = vo.YosanKounyuKibouBuhinHi
            result.YosanKounyuKibouBuhinHiTotal = vo.YosanKounyuKibouBuhinHiTotal
            result.YosanKounyuKibouKataHi = vo.YosanKounyuKibouKataHi
            result.AudBi = vo.AudBi
            result.Henkaten = vo.Henkaten

            If isChange Then
                '変更'
                result.AudFlag = CHANGE_FLAG
                If Not _ChangeBlockList.Contains(vo.YosanBlockNo) Then
                    _ChangeBlockList.Add(vo.YosanBlockNo)
                End If
            Else
                '変化ないなら空にする'
                result.AudFlag = ""
            End If

            Return result
        End Function

        ''' <summary>
        ''' 履歴情報設定
        ''' </summary>
        ''' <param name="resultVo">設定元のVO</param>
        ''' <param name="columnId">列ID</param>
        ''' <param name="columnName">列名</param>
        ''' <param name="beforeValue">前回値</param>
        ''' <param name="afterValue">今回値</param>
        ''' <remarks></remarks>
        Private Sub CreateAddRirekiVo(ByVal resultVo As TYosanSetteiBuhinVo, _
                                      ByVal columnId As String, _
                                      ByVal columnName As String, _
                                      ByVal beforeValue As String, _
                                      ByVal afterValue As String)

            Dim vo As New TYosanSetteiBuhinRirekiVo
            Dim aDate As New ShisakuDate

            vo.ShisakuEventCode = resultVo.ShisakuEventCode
            vo.YosanListCode = resultVo.YosanListCode
            vo.YosanBukaCode = resultVo.YosanBukaCode
            vo.YosanBlockNo = resultVo.YosanBlockNo
            vo.YosanBuhinNo = resultVo.YosanBuhinNo
            vo.BuhinNoHyoujiJun = resultVo.BuhinNoHyoujiJun
            vo.ColumnId = columnId
            vo.ColumnName = columnName
            vo.UpdateBi = aDate.CurrentDateDbFormat
            vo.UpdateJikan = aDate.CurrentTimeDbFormat
            vo.Before = beforeValue
            vo.After = afterValue
            If vo.After Is Nothing Then
                vo.After = ""
            End If
            vo.CreatedUserId = LoginInfo.Now.UserId
            vo.CreatedDate = aDate.CurrentDateDbFormat
            vo.CreatedTime = aDate.CurrentTimeDbFormat
            vo.UpdatedUserId = LoginInfo.Now.UserId
            vo.UpdatedDate = aDate.CurrentDateDbFormat
            vo.UpdatedTime = aDate.CurrentTimeDbFormat

            _HistoryList.Add(vo)
        End Sub

        ''' <summary>
        ''' 追加扱いの項目に対して、部品番号表示順、ソート順、行IDを設定
        ''' </summary>
        ''' <remarks></remarks>
        Private Sub SetSort()
            Dim blockHyoujijun As New Dictionary(Of String, Integer)
            Dim blockSortJun As New Dictionary(Of String, Integer)
            Dim blockGyouId As New Dictionary(Of String, String)

            'それぞれの現時点での最大値を振る'
            For Each vo As TYosanSetteiBuhinVo In CompareList
                If Not blockHyoujijun.ContainsKey(vo.YosanBlockNo) Then
                    blockHyoujijun.Add(vo.YosanBlockNo, vo.BuhinNoHyoujiJun)
                End If
                If blockHyoujijun(vo.YosanBlockNo) < vo.BuhinNoHyoujiJun Then
                    blockHyoujijun(vo.YosanBlockNo) = vo.BuhinNoHyoujiJun
                End If

                If Not blockSortJun.ContainsKey(vo.YosanBlockNo) Then
                    blockSortJun.Add(vo.YosanBlockNo, vo.YosanSortJun)
                End If
                If blockSortJun(vo.YosanBlockNo) < vo.YosanSortJun Then
                    blockSortJun(vo.YosanBlockNo) = vo.YosanSortJun
                End If

                If Not blockGyouId.ContainsKey(vo.YosanBlockNo) Then
                    blockGyouId.Add(vo.YosanBlockNo, vo.YosanGyouId)
                End If
                If IsNumeric(blockGyouId(vo.YosanBlockNo)) AndAlso IsNumeric(vo.YosanGyouId) Then
                    If Integer.Parse(blockGyouId(vo.YosanBlockNo)) < Integer.Parse(vo.YosanGyouId) Then
                        blockGyouId(vo.YosanBlockNo) = vo.YosanGyouId
                    End If
                End If
            Next

            'ソート用'
            Dim bList As New List(Of String)
            Dim bDic As New Dictionary(Of String, List(Of TYosanSetteiBuhinVo))


            For Each vo As TYosanSetteiBuhinVo In CompareList
                '追加分だけ再設定'
                If StringUtil.Equals(vo.AudFlag, ADD_FLAG) Then

                    '未登録のもののみ'
                    If StringUtil.IsEmpty(vo.YosanGyouId) Then
                        '現最大値+1を設定'
                        blockHyoujijun(vo.YosanBlockNo) = blockHyoujijun(vo.YosanBlockNo) + 1
                        vo.BuhinNoHyoujiJun = blockHyoujijun(vo.YosanBlockNo)

                        blockSortJun(vo.YosanBlockNo) = blockSortJun(vo.YosanBlockNo) + 1
                        vo.YosanSortJun = blockSortJun(vo.YosanBlockNo)

                        If IsNumeric(blockGyouId(vo.YosanBlockNo)) Then
                            blockGyouId(vo.YosanBlockNo) = Right("000" & StringUtil.ToString(Integer.Parse(blockGyouId(vo.YosanBlockNo)) + 1), 3)
                        Else
                            blockGyouId(vo.YosanBlockNo) = "001"
                        End If
                        vo.YosanGyouId = blockGyouId(vo.YosanBlockNo)


                    End If

                End If

                If Not bList.Contains(vo.YosanBlockNo) Then
                    bList.Add(vo.YosanBlockNo)
                End If
                If Not bDic.ContainsKey(vo.YosanBlockNo) Then
                    bDic.Add(vo.YosanBlockNo, New List(Of TYosanSetteiBuhinVo))
                End If
                bDic(vo.YosanBlockNo).Add(vo)
            Next

            bList.Sort()  'ソートする

            Dim c As New sortJunComparer
            Dim resultList As New List(Of TYosanSetteiBuhinVo)
            For Each block As String In bList
                bDic(block).Sort(c)
                For Each vo As TYosanSetteiBuhinVo In bDic(block)
                    resultList.Add(vo)
                Next
            Next

            _CompareList = resultList


        End Sub


#Region "比較用情報作成"

        ''' <summary>
        ''' 比較用の情報を作成
        ''' </summary>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Private Function MakeCompareList() As List(Of TYosanSetteiBuhinVo)


            Dim BuhinEditVoList As New List(Of TehaichoMenu.Vo.TShisakuBuhinEditVoSekkeiHelper)
            Dim CyushutuImpl As YosanSetteiBuhinEditHeaderDao = New YosanSetteiBuhinEditHeaderDaoImpl
            BuhinEditVoList = CyushutuImpl.FindByNewBuhinEditListSaishin(_ShisakuEventCode, _YosanListCode)

            '取得した内容をマージする'
            Dim newBuhinEditVoList As New List(Of TehaichoMenu.Vo.TShisakuBuhinEditVoSekkeiHelper)
            newBuhinEditVoList = MergeList(BuhinEditVoList)

            '国内集計と海外集計は見る？'
            Dim dic As New Dictionary(Of String, Dictionary(Of String, Dictionary(Of Integer, Dictionary(Of String, TYosanSetteiBuhinVo))))

            Dim dic2 As New Dictionary(Of String, Dictionary(Of String, Dictionary(Of String, Dictionary(Of String, Dictionary(Of Integer, Dictionary(Of String, TYosanSetteiBuhinVo))))))

            For Each vo As TehaichoMenu.Vo.TShisakuBuhinEditVoSekkeiHelper In newBuhinEditVoList
                '圧縮完了しているので'
                '総数以外は同一のはず'
                vo.ShisakuBlockNo = vo.ShisakuBlockNo.Trim
                vo.BuhinNo = vo.BuhinNo.Trim
                If StringUtil.IsEmpty(vo.KyoukuSection) Then
                    vo.KyoukuSection = ""
                End If

                If StringUtil.IsEmpty(vo.ShukeiCode) Then
                    vo.ShukeiCode = ""
                End If
                If StringUtil.IsEmpty(vo.SiaShukeiCode) Then
                    vo.SiaShukeiCode = ""
                End If


                'If Not dic.ContainsKey(vo.ShisakuBlockNo) Then
                '    dic.Add(vo.ShisakuBlockNo, New Dictionary(Of String, Dictionary(Of Integer, Dictionary(Of String, TYosanSetteiBuhinVo))))
                'End If

                'If Not dic(vo.ShisakuBlockNo).ContainsKey(vo.BuhinNo) Then
                '    dic(vo.ShisakuBlockNo).Add(vo.BuhinNo, New Dictionary(Of Integer, Dictionary(Of String, TYosanSetteiBuhinVo)))
                'End If

                'If Not dic(vo.ShisakuBlockNo)(vo.BuhinNo).ContainsKey(vo.BuhinNoHyoujiJun) Then
                '    dic(vo.ShisakuBlockNo)(vo.BuhinNo).Add(vo.BuhinNoHyoujiJun, New Dictionary(Of String, TYosanSetteiBuhinVo))
                'End If

                'If dic(vo.ShisakuBlockNo)(vo.BuhinNo)(vo.BuhinNoHyoujiJun).ContainsKey(vo.KyoukuSection) Then
                '    dic(vo.ShisakuBlockNo)(vo.BuhinNo)(vo.BuhinNoHyoujiJun)(vo.KyoukuSection).YosanInsu = dic(vo.ShisakuBlockNo)(vo.BuhinNo)(vo.BuhinNoHyoujiJun)(vo.KyoukuSection).YosanInsu + vo.InsuSuryo
                'Else
                '    dic(vo.ShisakuBlockNo)(vo.BuhinNo)(vo.BuhinNoHyoujiJun).Add(vo.KyoukuSection, New TYosanSetteiBuhinVo)


                '    Dim addVo As New TYosanSetteiBuhinVo
                '    addVo.ShisakuEventCode = vo.ShisakuEventCode
                '    addVo.YosanListCode = vo.ShisakuListCode
                '    addVo.YosanBukaCode = vo.ShisakuBukaCode
                '    addVo.YosanBlockNo = vo.ShisakuBlockNo
                '    addVo.BuhinNoHyoujiJun = vo.BuhinNoHyoujiJun
                '    addVo.YosanSortJun = 0
                '    addVo.YosanGyouId = ""
                '    addVo.YosanLevel = vo.Level
                '    addVo.YosanShukeiCode = vo.ShukeiCode
                '    addVo.YosanSiaShukeiCode = vo.SiaShukeiCode
                '    addVo.YosanBuhinNo = vo.BuhinNo
                '    addVo.YosanBuhinName = vo.BuhinName
                '    addVo.YosanInsu = vo.InsuSuryo
                '    addVo.YosanMakerCode = vo.MakerCode
                '    addVo.YosanKyoukuSection = vo.KyoukuSection
                '    addVo.YosanKoutan = ""
                '    addVo.YosanTehaiKigou = ""

                '    'addVo.YosanTsukurikataSeisaku = ""
                '    'addVo.YosanTsukurikataKatashiyou1 = ""
                '    'addVo.YosanTsukurikataKatashiyou2 = ""
                '    'addVo.YosanTsukurikataKatashiyou3 = ""
                '    'addVo.YosanTsukurikataTigu = ""
                '    'addVo.YosanTsukurikataKibo = ""
                '    addVo.YosanShisakuBuhinHi = vo.ShisakuBuhinHi
                '    addVo.YosanShisakuKataHi = vo.ShisakuKataHi
                '    addVo.YosanBuhinNote = vo.BuhinNote
                '    addVo.YosanBikou = vo.Bikou
                '    'addVo.YosanKonkyoKokugaiKbn = ""
                '    'addVo.YosanKonkyoMixBuhinHi = ""
                '    'addVo.YosanKonkyoInyouMixBuhinHi = ""
                '    'addVo.YosanKonkyoKeisu1 = ""
                '    'addVo.YosanKonkyoKouhou = ""
                '    'addVo.YosanWaritukeBuhinHi = ""
                '    'addVo.YosanWaritukeKeisu2 = ""
                '    'addVo.YosanWaritukeBuhinHiTotal = ""
                '    'addVo.YosanWaitukeKataHi = ""
                '    'addVo.YosanKounyuKibouTanka = ""
                '    'addVo.YosanKounyuKibouBuhinHi = ""
                '    'addVo.YosanKounyuKibouBuhinHiTotal = ""
                '    'addVo.YosanKounyuKibouKataHi = ""

                '    dic(vo.ShisakuBlockNo)(vo.BuhinNo)(vo.BuhinNoHyoujiJun)(vo.KyoukuSection) = addVo
                'End If






                '集計コードみる版'
                If Not dic2.ContainsKey(vo.ShisakuBlockNo) Then
                    dic2.Add(vo.ShisakuBlockNo, New Dictionary(Of String, Dictionary(Of String, Dictionary(Of String, Dictionary(Of Integer, Dictionary(Of String, TYosanSetteiBuhinVo))))))
                End If

                If Not dic2(vo.ShisakuBlockNo).ContainsKey(vo.BuhinNo) Then
                    dic2(vo.ShisakuBlockNo).Add(vo.BuhinNo, New Dictionary(Of String, Dictionary(Of String, Dictionary(Of Integer, Dictionary(Of String, TYosanSetteiBuhinVo)))))
                End If

                If Not dic2(vo.ShisakuBlockNo)(vo.BuhinNo).ContainsKey(vo.ShukeiCode) Then
                    dic2(vo.ShisakuBlockNo)(vo.BuhinNo).Add(vo.ShukeiCode, New Dictionary(Of String, Dictionary(Of Integer, Dictionary(Of String, TYosanSetteiBuhinVo))))
                End If

                If Not dic2(vo.ShisakuBlockNo)(vo.BuhinNo)(vo.ShukeiCode).ContainsKey(vo.SiaShukeiCode) Then
                    dic2(vo.ShisakuBlockNo)(vo.BuhinNo)(vo.ShukeiCode).Add(vo.SiaShukeiCode, New Dictionary(Of Integer, Dictionary(Of String, TYosanSetteiBuhinVo)))
                End If


                If Not dic2(vo.ShisakuBlockNo)(vo.BuhinNo)(vo.ShukeiCode)(vo.SiaShukeiCode).ContainsKey(vo.BuhinNoHyoujiJun) Then
                    dic2(vo.ShisakuBlockNo)(vo.BuhinNo)(vo.ShukeiCode)(vo.SiaShukeiCode).Add(vo.BuhinNoHyoujiJun, New Dictionary(Of String, TYosanSetteiBuhinVo))
                End If

                If dic2(vo.ShisakuBlockNo)(vo.BuhinNo)(vo.ShukeiCode)(vo.SiaShukeiCode)(vo.BuhinNoHyoujiJun).ContainsKey(vo.KyoukuSection) Then
                    dic2(vo.ShisakuBlockNo)(vo.BuhinNo)(vo.ShukeiCode)(vo.SiaShukeiCode)(vo.BuhinNoHyoujiJun)(vo.KyoukuSection).YosanInsu = dic2(vo.ShisakuBlockNo)(vo.BuhinNo)(vo.ShukeiCode)(vo.SiaShukeiCode)(vo.BuhinNoHyoujiJun)(vo.KyoukuSection).YosanInsu + vo.InsuSuryo
                Else
                    dic2(vo.ShisakuBlockNo)(vo.BuhinNo)(vo.ShukeiCode)(vo.SiaShukeiCode)(vo.BuhinNoHyoujiJun).Add(vo.KyoukuSection, New TYosanSetteiBuhinVo)

                    Dim addVo As New TYosanSetteiBuhinVo
                    addVo.ShisakuEventCode = vo.ShisakuEventCode
                    addVo.YosanListCode = vo.ShisakuListCode
                    addVo.YosanBukaCode = vo.ShisakuBukaCode
                    addVo.YosanBlockNo = vo.ShisakuBlockNo
                    addVo.BuhinNoHyoujiJun = vo.BuhinNoHyoujiJun
                    addVo.YosanSortJun = 0
                    addVo.YosanGyouId = ""
                    addVo.YosanLevel = vo.Level
                    addVo.YosanShukeiCode = vo.ShukeiCode
                    addVo.YosanSiaShukeiCode = vo.SiaShukeiCode
                    addVo.YosanBuhinNo = vo.BuhinNo
                    addVo.YosanBuhinName = vo.BuhinName
                    addVo.YosanInsu = vo.InsuSuryo
                    addVo.YosanMakerCode = vo.MakerCode
                    addVo.YosanKyoukuSection = vo.KyoukuSection
                    addVo.YosanKoutan = ""
                    addVo.YosanTehaiKigou = ""

                    'addVo.YosanTsukurikataSeisaku = ""
                    'addVo.YosanTsukurikataKatashiyou1 = ""
                    'addVo.YosanTsukurikataKatashiyou2 = ""
                    'addVo.YosanTsukurikataKatashiyou3 = ""
                    'addVo.YosanTsukurikataTigu = ""
                    'addVo.YosanTsukurikataKibo = ""
                    addVo.YosanShisakuBuhinHi = vo.ShisakuBuhinHi
                    addVo.YosanShisakuKataHi = vo.ShisakuKataHi
                    addVo.YosanBuhinNote = vo.BuhinNote
                    addVo.YosanBikou = vo.Bikou
                    'addVo.YosanKonkyoKokugaiKbn = ""
                    'addVo.YosanKonkyoMixBuhinHi = ""
                    'addVo.YosanKonkyoInyouMixBuhinHi = ""
                    'addVo.YosanKonkyoKeisu1 = ""
                    'addVo.YosanKonkyoKouhou = ""
                    'addVo.YosanWaritukeBuhinHi = ""
                    'addVo.YosanWaritukeKeisu2 = ""
                    'addVo.YosanWaritukeBuhinHiTotal = ""
                    'addVo.YosanWaitukeKataHi = ""
                    'addVo.YosanKounyuKibouTanka = ""
                    'addVo.YosanKounyuKibouBuhinHi = ""
                    'addVo.YosanKounyuKibouBuhinHiTotal = ""
                    'addVo.YosanKounyuKibouKataHi = ""

                    dic2(vo.ShisakuBlockNo)(vo.BuhinNo)(vo.ShukeiCode)(vo.SiaShukeiCode)(vo.BuhinNoHyoujiJun)(vo.KyoukuSection) = addVo
                End If


            Next



            Dim resultList As New List(Of TYosanSetteiBuhinVo)

            'For Each blockNo As String In dic.Keys
            '    For Each buhinNo As String In dic(blockNo).Keys
            '        For Each buhinNohyoujiJun As Integer In dic(blockNo)(buhinNo).Keys
            '            For Each kyoku As String In dic(blockNo)(buhinNo)(buhinNohyoujiJun).Keys
            '                resultList.Add(dic(blockNo)(buhinNo)(buhinNohyoujiJun)(kyoku))
            '            Next
            '        Next
            '    Next
            'Next


            For Each blockNo As String In dic2.Keys
                For Each buhinNo As String In dic2(blockNo).Keys
                    For Each shukeiCode As String In dic2(blockNo)(buhinNo).Keys
                        For Each siaShukeiCode As String In dic2(blockNo)(buhinNo)(shukeiCode).Keys
                            For Each buhinNohyoujiJun As Integer In dic2(blockNo)(buhinNo)(shukeiCode)(siaShukeiCode).Keys
                                For Each kyoku As String In dic2(blockNo)(buhinNo)(shukeiCode)(siaShukeiCode)(buhinNohyoujiJun).Keys
                                    resultList.Add(dic2(blockNo)(buhinNo)(shukeiCode)(siaShukeiCode)(buhinNohyoujiJun)(kyoku))
                                Next
                            Next
                        Next
                    Next
                Next
            Next

            SetKoutan(resultList)

            Return resultList
        End Function


        ''' <summary>
        ''' マージ処理を行う
        ''' </summary>
        ''' <param name="BuhinEditVoList">マージ用リスト</param>
        ''' <returns>部品編集情報</returns>
        ''' <remarks></remarks>
        Private Function MergeList(ByVal BuhinEditVoList As List(Of TehaichoMenu.Vo.TShisakuBuhinEditVoSekkeiHelper)) As List(Of TehaichoMenu.Vo.TShisakuBuhinEditVoSekkeiHelper)
            '部品番号順にソートが完了したので今度は圧縮'

            '具体的にはブロックNo, レベル, 部品番号, 集計コード, 手配記号, 供給セクションが同一の項目は'
            '部品番号表示順が最も小さいものと同一の部品番号表示順を持つ'
            'この場合、試作号車表示順は異なっている'
            For Each Vo As TehaichoMenu.Vo.TShisakuBuhinEditVoSekkeiHelper In BuhinEditVoList
                'If StringUtil.IsEmpty(Vo.ShukeiCode) Then
                '    Vo.ShukeiCode = Vo.SiaShukeiCode
                'End If
                If StringUtil.IsEmpty(Vo.KyoukuSection) Then
                    Vo.KyoukuSection = ""
                End If

                If StringUtil.IsEmpty(Vo.ShukeiCode) Then
                    Vo.ShukeiCode = ""
                End If
                If StringUtil.IsEmpty(Vo.SiaShukeiCode) Then
                    Vo.SiaShukeiCode = ""
                End If

            Next

            Dim flg As Boolean = False
            flg = True
            For index As Integer = 0 To BuhinEditVoList.Count - 1
                For index2 As Integer = index + 1 To BuhinEditVoList.Count - 1

                    '使用するか不明'
                    ''2015/08/28 追加 E.Ubukata Ver2.11.0
                    '' フル組の際にはベース部品フラグを参照しないように変更
                    'If Me.isIkansha Then
                    '    flg = StringUtil.Equals(BuhinEditVoList(index).BaseBuhinFlg, BuhinEditVoList(index2).BaseBuhinFlg)
                    'Else
                    '    flg = True
                    'End If

                    If flg Then
                        'ブロックNoのチェックをする'
                        If StringUtil.Equals(BuhinEditVoList(index).ShisakuBlockNo, BuhinEditVoList(index2).ShisakuBlockNo) Then
                            'レベル,部品番号,集計コードはNULLがないのでまとめて同一チェック'
                            '20110730試作区分のチェックを追加 樺澤'
                            '試作区分はNULLのパターンも存在するので'
                            Dim kbn1 As String = BuhinEditVoList(index).BuhinNoKbn
                            Dim kbn2 As String = BuhinEditVoList(index2).BuhinNoKbn
                            If StringUtil.IsEmpty(kbn1) Then
                                kbn1 = ""
                            End If
                            If StringUtil.IsEmpty(kbn2) Then
                                kbn2 = ""
                            End If
                            '区分いないから区分わけ不要では？'
                            'If StringUtil.Equals(kbn1, kbn2) Then
                            If BuhinEditVoList(index).Level = BuhinEditVoList(index2).Level AndAlso _
                            StringUtil.Equals(BuhinEditVoList(index).BuhinNo.Trim, BuhinEditVoList(index2).BuhinNo.Trim) AndAlso _
                            StringUtil.Equals(BuhinEditVoList(index).ShukeiCode, BuhinEditVoList(index2).ShukeiCode) AndAlso _
                            StringUtil.Equals(BuhinEditVoList(index).SiaShukeiCode, BuhinEditVoList(index2).SiaShukeiCode) Then

                                If StringUtil.Equals(BuhinEditVoList(index).KyoukuSection, BuhinEditVoList(index2).KyoukuSection) Then
                                    '2013/05/29 再使用不可の判断を追加
                                    If StringUtil.Equals(BuhinEditVoList(index).Saishiyoufuka, BuhinEditVoList(index2).Saishiyoufuka) Then
                                        '号車表示順が若い方はどっち？'
                                        If BuhinEditVoList(index).ShisakuGousyaHyoujiJun < BuhinEditVoList(index2).ShisakuGousyaHyoujiJun Then
                                            '行IDが001同士か000同士ならマージ'
                                            BuhinEditVoList(index2).BuhinNoHyoujiJun = BuhinEditVoList(index).BuhinNoHyoujiJun
                                        ElseIf BuhinEditVoList(index).ShisakuGousyaHyoujiJun > BuhinEditVoList(index2).ShisakuGousyaHyoujiJun Then
                                            BuhinEditVoList(index).BuhinNoHyoujiJun = BuhinEditVoList(index2).BuhinNoHyoujiJun
                                        Else
                                            '号車表示順が同じでも部品番号表示順が違うパターンがいる'
                                            If BuhinEditVoList(index).BuhinNoHyoujiJun < BuhinEditVoList(index2).BuhinNoHyoujiJun Then
                                                BuhinEditVoList(index2).BuhinNoHyoujiJun = BuhinEditVoList(index).BuhinNoHyoujiJun
                                            ElseIf BuhinEditVoList(index).BuhinNoHyoujiJun < BuhinEditVoList(index2).BuhinNoHyoujiJun Then
                                                BuhinEditVoList(index).BuhinNoHyoujiJun = BuhinEditVoList(index2).BuhinNoHyoujiJun
                                            End If
                                        End If
                                    End If
                                End If

                            End If

                            'End If

                        End If
                    End If
                Next
            Next

            Return BuhinEditVoList
        End Function

        ''' <summary>
        ''' 購担設定
        ''' </summary>
        ''' <param name="resultList"></param>
        ''' <remarks></remarks>
        Private Sub SetKoutan(ByRef resultList As List(Of TYosanSetteiBuhinVo))

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


            Dim dao As YosanSetteiBuhinSakuseiDao = New YosanSetteiBuhinSakuseiDaoImpl

            For Each vo As TYosanSetteiBuhinVo In resultList
                Dim tmp As New TYosanSetteiBuhinTmpVo
                tmp = dao.FindByKoutanTorihikisakiSakusei(vo.YosanBuhinNo, aAsKpsm10p, aAsPartsp, aAsGkpsm10p, aAsBuhin01, aTValueDev, aRhac0610)

                Dim strKoutan As String = ""
                Dim strTorihikisakiCode As String = ""
                Dim strMakerCode As String = ""
                If Not tmp Is Nothing Then
                    vo.YosanKoutan = tmp.Koutan
                End If
            Next



        End Sub

        Private Sub SetTehaiKigou(ByVal resultList As List(Of TYosanSetteiBuhinVo))


            For Each vo As TYosanSetteiBuhinVo In resultList
                '手配記号がすでにFなら何もしない'
                If vo.YosanTehaiKigou = "F" Then
                    Continue For
                Else

                    vo.YosanTehaiKigou = "F"

                End If

                ''集計コードがあるかチェック(無ければ海外集計有)'
                'If vo.YosanShukeiCode.TrimEnd <> "" Then
                '    If vo.YosanShukeiCode = "X" Then
                '        vo.YosanTehaiKigou = "F"
                '    ElseIf vo.YosanShukeiCode = "A" Then
                '        vo.YosanTehaiKigou = ""
                '    ElseIf vo.YosanShukeiCode = "E" Or vo.YosanShukeiCode = "Y" Then
                '        '専用品チェック'
                '        If StringUtil.IsEmpty(vo.SenyouMark) Then
                '            '共用品なら'
                '            vo.YosanTehaiKigou = "A"
                '        Else
                '            '専用品なら'
                '            vo.YosanTehaiKigou = "D"
                '            vo.SenyouMark = "*"
                '        End If
                '    ElseIf vo.YosanShukeiCode = "R" Or vo.YosanShukeiCode = "J" Then
                '        vo.YosanTehaiKigou = "F"
                '    End If
                'Else

                '    '海外集計コード'
                '    '取引先コードで処理が分岐する'
                '    If StringUtil.IsEmpty(vo.YosanMakerCode) Then
                '        If vo.YosanSiaShukeiCode = "X" Then
                '            vo.YosanTehaiKigou = "F"
                '        ElseIf vo.YosanSiaShukeiCode = "A" Then
                '            vo.YosanTehaiKigou = "J"
                '        ElseIf vo.YosanSiaShukeiCode = "E" Then
                '            vo.YosanTehaiKigou = "B"
                '        ElseIf vo.YosanSiaShukeiCode = "Y" Then
                '            vo.YosanTehaiKigou = "B"
                '        ElseIf vo.YosanSiaShukeiCode = "J" Or vo.YosanSiaShukeiCode = "R" Then
                '            vo.YosanTehaiKigou = "F"
                '        End If
                '    Else
                '        If Left(vo.YosanMakerCode, 1) <> "A" Then
                '            If vo.YosanSiaShukeiCode = "X" Then
                '                vo.YosanTehaiKigou = "F"
                '            ElseIf vo.YosanSiaShukeiCode = "A" Then
                '                vo.YosanTehaiKigou = ""
                '            ElseIf vo.YosanSiaShukeiCode = "E" Or vo.YosanSiaShukeiCode = "Y" Then
                '                '専用品チェック'
                '                If StringUtil.IsEmpty(vo.SenyouMark) Then
                '                    '共用品なら'
                '                    vo.YosanTehaiKigou = "A"
                '                Else
                '                    '専用品なら'
                '                    vo.YosanTehaiKigou = "D"
                '                    vo.SenyouMark = "*"
                '                End If
                '            ElseIf vo.YosanSiaShukeiCode = "R" Or vo.YosanSiaShukeiCode = "J" Then
                '                vo.YosanTehaiKigou = "F"
                '            End If
                '        Else

                '            If vo.YosanSiaShukeiCode = "X" Then
                '                vo.YosanTehaiKigou = "F"
                '            ElseIf vo.YosanSiaShukeiCode = "A" Then
                '                vo.YosanTehaiKigou = "J"
                '            ElseIf vo.YosanSiaShukeiCode = "E" Then
                '                vo.YosanTehaiKigou = "B"
                '            ElseIf vo.YosanSiaShukeiCode = "Y" Then
                '                vo.YosanTehaiKigou = "B"
                '            ElseIf vo.YosanSiaShukeiCode = "J" Or vo.YosanSiaShukeiCode = "R" Then
                '                vo.YosanTehaiKigou = "F"
                '            End If
                '        End If
                '    End If
                'End If
            Next



        End Sub


#End Region

#Region "プロパティ"

        ''' <summary>
        ''' 最新化リスト
        ''' </summary>
        ''' <value></value>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public ReadOnly Property CompareList() As List(Of TYosanSetteiBuhinVo)
            Get
                Return _CompareList
            End Get
        End Property

        ''' <summary>
        ''' 変更履歴リスト
        ''' </summary>
        ''' <value></value>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public ReadOnly Property HistoryList() As List(Of TYosanSetteiBuhinRirekiVo)
            Get
                Return _HistoryList
            End Get
        End Property

        ''' <summary>
        ''' 変更履歴リスト
        ''' </summary>
        ''' <value></value>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public ReadOnly Property ChangeBlockList() As List(Of String)
            Get
                Return _ChangeBlockList
            End Get
        End Property

#End Region


        Private Class sortJunComparer
            Implements System.Collections.Generic.IComparer(Of TYosanSetteiBuhinVo)



            'xがyより小さいときはマイナスの数、大きいときはプラスの数、同じときは0を返す
            Public Function Compare(ByVal x As ShisakuCommon.Db.EBom.Vo.TYosanSetteiBuhinVo, ByVal y As ShisakuCommon.Db.EBom.Vo.TYosanSetteiBuhinVo) As Integer _
                Implements System.Collections.Generic.IComparer(Of ShisakuCommon.Db.EBom.Vo.TYosanSetteiBuhinVo).Compare

                'Nothingが最も小さいとする
                If x.YosanSortJun = y.YosanSortJun Then
                    Return 0
                End If
                If x.YosanSortJun < y.YosanSortJun Then
                    Return -1
                End If
                If x.YosanSortJun > y.YosanSortJun Then
                    Return 1
                End If
            End Function

        End Class


    End Class

End Namespace