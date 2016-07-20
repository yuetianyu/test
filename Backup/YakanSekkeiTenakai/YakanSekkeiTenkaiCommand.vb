Imports ShisakuCommon.Db.EBom.Vo.Helper
Imports ShisakuCommon.Db.EBom.Dao
Imports ShisakuCommon.Db.EBom.Dao.Impl
Imports ShisakuCommon.Db
Imports ShisakuCommon
Imports ShisakuCommon.Db.EBom.Vo
Imports EventSakusei.ShisakuBuhinMenu.Logic.Impl.Tenkai
Imports EventSakusei.ShisakuBuhinEdit.Kosei.Logic.Matrix
Imports EventSakusei.ShisakuBuhinMenu.Dao
Imports EventSakusei.ShisakuBuhinMenu.Logic
Imports EventSakusei
'/*** 20140923 CHANGE START（高速化対応） ***/
Imports YakanSekkeiTenakai.Vo
'/*** 20140923 CHANGE END ***/

Namespace ShisakuBuhinMenu.Logic.Impl
    Public Class YakanSekkeiTenkaiCommand : Implements Command
        Private ReadOnly shisakuEventVo As TShisakuEventVo
        Private ReadOnly login As LoginInfo
        Private ReadOnly aShisakuDao As ShisakuDao
        Private ReadOnly shisakuEventDao As TShisakuEventDao
        Private ReadOnly blockDao As TShisakuSekkeiBlockEbomKanshiDao
        Private ReadOnly instlDao As TShisakuSekkeiBlockInstlEbomKanshiDao
        Private ReadOnly kouseiDao As TShisakuBuhinKouseiDao
        Private ReadOnly buhinDao As TShisakuBuhinDao
        Private ReadOnly kaiteiSyochiBi As DateTime
        '/*** 20140923 CHANGE START（高速化対応） ***/
        Private ReadOnly aDate As ShisakuDate
        '/*** 20140923 CHANGE END ***/

        Private _newStatus As String

        Public Sub New(ByVal shisakuEventVo As TShisakuEventVo, _
                       ByVal login As LoginInfo, _
                       ByVal aShisakuDao As ShisakuDao, _
                       ByVal shisakuEventDao As TShisakuEventDao, _
                       ByVal blockDao As TShisakuSekkeiBlockEbomKanshiDao, _
                       ByVal instlDao As TShisakuSekkeiBlockInstlEbomKanshiDao, _
                       ByVal kouseiDao As TShisakuBuhinKouseiDao, _
                       ByVal buhinDao As TShisakuBuhinDao, _
                       ByVal kaiteiSyochiBi As DateTime)
            Me.shisakuEventVo = shisakuEventVo
            Me.login = login
            Me.aShisakuDao = aShisakuDao
            Me.shisakuEventDao = shisakuEventDao
            Me.blockDao = blockDao
            Me.instlDao = instlDao
            Me.kouseiDao = kouseiDao
            Me.buhinDao = buhinDao
            Me.kaiteiSyochiBi = kaiteiSyochiBi

            '/*** 20140923 CHANGE START（高速化対応） ***/
            aDate = New ShisakuDate
            '/*** 20140923 CHANGE END ***/
        End Sub

        Public Function GetNewStatus() As String Implements Command.GetNewStatus
            If _newStatus Is Nothing Then
                Throw New InvalidOperationException("#Perform()メソッドを実行してください.")
            End If
            Return _newStatus
        End Function

        Public Sub Perform() Implements Command.Perform
            '/*** 20140923 CHANGE START（高速化対応） ***/
            'Dim aDate As New ShisakuDate
            '/*** 20140923 CHANGE END ***/

            '/*** 20140911 CHANGE START ***/
            Dim eventDao As IShisakuEventDao = New ShisakuEventDaoImpl
            Dim unitKbnDictionary As New Dictionary2(Of String, String, String)()
            '/*** 20140911 CHANGE END ***/

            ' T_SHISAKU_EVENT ステータス更新
            ''↓↓2014/08/05 Ⅰ.5.EBOM差分出力 az) (TES)施 ADD START
            If login IsNot Nothing Then '夜間設計の場合　loginはブランク

                ''↑↑2014/08/05 Ⅰ.5.EBOM差分出力 az) (TES)施 ADD END
                _newStatus = TShisakuEventVoHelper.Status.SEKKEI_MAINTAINING
                '/*** 20140911 CHANGE START ***/
                'UpdateTShisakuEventStatus(shisakuEventVo.ShisakuEventCode, _newStatus, aDate, kaiteiSyochiBi, "0")
                ' 更新前にselectしない
                UpdateTShisakuEventStatus(eventDao, shisakuEventVo.ShisakuEventCode, _newStatus, aDate, kaiteiSyochiBi, "0")
                '/*** 20140911 CHANGE END ***/
            End If
            '4/7以下の処理は中止にする。

            ' T_SHISAKU_SEKKEI_BLOCK_EBOM_KANSHI 追加
            '/*** 20140911 CHANGE START（引数追加） ***/
            'Dim sekkeiBlock As New YakanSekkeiTenakai.Logic.YakanSekkeiBlockSupplier(shisakuEventVo)
            Dim sekkeiBlock As New YakanSekkeiTenakai.Logic.YakanSekkeiBlockSupplier(shisakuEventVo, unitKbnDictionary)
            '/*** 20140911 CHANGE END ***/
            sekkeiBlock.Register(login, blockDao, aDate)

            ' T_SHISAKU_SEKKEI_BLOCK_INSTL_EBOM_KANSHI 追加
            '/*** 20140911 CHANGE START（引数追加） ***/
            'Dim sekkeiBlockInstl As New YakanSekkeiTenakai.Logic.YakanSekkeiBlockInstlSupplier(shisakuEventVo)
            Dim sekkeiBlockInstl As New YakanSekkeiTenakai.Logic.YakanSekkeiBlockInstlSupplier(shisakuEventVo, unitKbnDictionary)
            '/*** 20140911 CHANGE END ***/
            sekkeiBlockInstl.Register(login, instlDao, aDate)

            '' T_SHISAKU_BUHIN_EDIT 追加
            '↓↓2014/09/25 酒井 ADD BEGIN
            'Dim buhinEdit As New BuhinEditTenkaiSubject(shisakuEventVo.ShisakuEventCode)   '2012/01/11 ここが遅い！！
            Dim buhinEdit As New BuhinEditTenkaiSubject(shisakuEventVo.ShisakuEventCode, Nothing, True)   '2012/01/11 ここが遅い！！
            '↑↑2014/09/25 酒井 ADD END

            buhinEdit.UpdateKyoukuSection()

            ''↓↓2014/08/05 Ⅰ.5.EBOM差分出力 az) (TES)施 ADD START
            If login IsNot Nothing Then   '夜間設計の場合　loginはブランク
                ''↑↑2014/08/05 Ⅰ.5.EBOM差分出力 az) (TES)施 ADD END
                '2012/03/07 最後に展開ステータスを１に更新する
                '/*** 20140911 CHANGE START ***/
                'UpdateTShisakuEventStatus(shisakuEventVo.ShisakuEventCode, _newStatus, aDate, kaiteiSyochiBi, "1")
                UpdateTShisakuEventStatus(eventDao, shisakuEventVo.ShisakuEventCode, _newStatus, aDate, kaiteiSyochiBi, "1")
                '/*** 20140911 CHANGE END ***/
            End If

        End Sub

        ''' <summary>
        ''' 試作イベント情報のステータスを更新する
        ''' </summary>
        ''' <param name="eventCode">試作イベントコード</param>
        ''' <param name="status">ステータス</param>
        ''' <param name="aShisakuDate">試作システム日付</param>
        ''' <param name="kaiteiSyochiBi">改訂処置日</param>
        ''' <param name="tenkaiStatus">0:未展開 1:展開済み</param>
        ''' <remarks>展開ステータスは処理はじめに０を設定し、処理終了後１に更新する</remarks>
        Private Sub UpdateTShisakuEventStatus(ByVal eventCode As String, ByVal status As String, ByVal aShisakuDate As ShisakuDate, ByVal kaiteiSyochiBi As DateTime, ByVal tenkaiStatus As String)
            ' TODO どのCommand実装クラスでもある処理で冗長。リファクタリングしたい！！
            Dim vo As TShisakuEventVo = shisakuEventDao.FindByPk(eventCode)
            vo.Status = status
            vo.SekkeiTenkaibi = DateUtil.ConvDateToIneteger(aShisakuDate.CurrentDateTime)
            vo.KaiteiSyochiShimekiribi = DateUtil.ConvDateToIneteger(kaiteiSyochiBi)
            vo.UpdatedUserId = login.UserId
            vo.UpdatedDate = aShisakuDate.CurrentDateDbFormat
            vo.UpdatedTime = aShisakuDate.CurrentTimeDbFormat
            '2012/03/07 設計展開ステータスを更新
            vo.TenkaiStatus = tenkaiStatus
            vo.KounyuShijiFlg = 0
            shisakuEventDao.UpdateByPk(vo)
        End Sub

        '/*** 20140911 CHANGE START ***/
        ''' <summary>
        ''' 試作イベント情報のステータスを更新する
        ''' </summary>
        ''' <param name="eventCode">試作イベントコード</param>
        ''' <param name="status">ステータス</param>
        ''' <param name="aShisakuDate">試作システム日付</param>
        ''' <param name="kaiteiSyochiBi">改訂処置日</param>
        ''' <param name="tenkaiStatus">0:未展開 1:展開済み</param>
        ''' <remarks>展開ステータスは処理はじめに０を設定し、処理終了後１に更新する</remarks>
        Private Sub UpdateTShisakuEventStatus(ByVal eventDao As IShisakuEventDao, ByVal eventCode As String, ByVal status As String, ByVal aShisakuDate As ShisakuDate, ByVal kaiteiSyochiBi As DateTime, ByVal tenkaiStatus As String)
            Dim vo As New TShisakuEventVo
            vo.ShisakuEventCode = eventCode
            vo.Status = status
            vo.SekkeiTenkaibi = DateUtil.ConvDateToIneteger(aShisakuDate.CurrentDateTime)
            vo.KaiteiSyochiShimekiribi = DateUtil.ConvDateToIneteger(kaiteiSyochiBi)
            vo.UpdatedUserId = login.UserId
            vo.UpdatedDate = aShisakuDate.CurrentDateDbFormat
            vo.UpdatedTime = aShisakuDate.CurrentTimeDbFormat
            '2012/03/07 設計展開ステータスを更新
            vo.TenkaiStatus = tenkaiStatus
            ''↓↓2014/07/24 Ⅰ.3.設計編集 ベース改修専用化_e) (TES)張 ADD BEGIN
            vo.KounyuShijiFlg = 0
            ''↑↑2014/07/24 Ⅰ.3.設計編集 ベース改修専用化_e) (TES)張 ADD END

            eventDao.UpdStatus(vo)
        End Sub
        '/*** 20140911 CHANGE END ***/

        ''' <summary>
        ''' 試作イベント情報のステータスを更新する
        ''' </summary>
        ''' <param name="eventCode">試作イベントコード</param>
        ''' <remarks></remarks>
        Private Sub UpdateTShisakuEventBase(ByVal eventCode As String)
            Dim wSobiKaiteiNo As String = Nothing
            Dim i As Integer = 0
            Dim rhac2210Vos As List(Of Rhac2210Vo) = Nothing

            Dim dao As SekkeiBlockDao = New SekkeiBlockDaoImpl

            '試作イベント情報を取得する。
            Dim eventBaseVos As List(Of TShisakuEventBaseVo) = dao.FindByShisakuEventBase(eventCode)
            'イベント情報分以下の処理を繰り返す。
            For Each baseVo As TShisakuEventBaseVo In eventBaseVos

                If StringUtil.IsEmpty(baseVo.ShisakuGousya) Then
                    Continue For
                End If

                'ベースの改訂№で存在しない場合、ひとつ前の改訂№で再チェックする。
                Dim wFlg As String = Nothing
                Do

                    wSobiKaiteiNo = baseVo.BaseSobiKaiteiNo

                    rhac2210Vos = dao.FindRHAC2210(baseVo.BaseKaihatsuFugo, _
                                                   wSobiKaiteiNo, _
                                                   baseVo.BaseKatashikiScd7, _
                                                   baseVo.BaseShimuke, _
                                                   baseVo.BaseOp)

                    'データが無ければ一つ前の改訂№で再チェック
                    If rhac2210Vos.Count = 0 Then
                        i = Integer.Parse(wSobiKaiteiNo) - 1
                        wSobiKaiteiNo = Format(i.ToString, "000")
                        wFlg = "ZEN"
                    End If
                    If wSobiKaiteiNo = "000" Then
                        Exit Do
                    End If

                Loop Until rhac2210Vos.Count > 0

                If rhac2210Vos.Count > 0 AndAlso Not StringUtil.IsEmpty(wFlg) Then

                    '' 既存データを更新
                    Dim shisakuEventBaseDao As TShisakuEventBaseDao = New TShisakuEventBaseDaoImpl
                    Dim param As New TShisakuEventBaseVo

                    Dim vo As TShisakuEventBaseVo = shisakuEventBaseDao.FindByPk(eventCode, baseVo.HyojijunNo)

                    vo.BaseSobiKaiteiNo = wSobiKaiteiNo
                    shisakuEventBaseDao.UpdateByPk(vo)

                End If

            Next

        End Sub

        '/*** 20140923 CHANGE START（高速化対応） ***/
        Public Sub GetTsuchishoNoZairyou()
            Dim yakanSekkeiTenkaiDao As New YakanSekkeiTenakai.Dao.YakanSekkeiTenkaiDaoImpl
            Dim yakanSekkeiBlockDao As New YakanSekkeiTenakai.Dao.YakanSekkeiBlockDaoImpl
            Dim tsuchishoDao As TShisakuBuhinEditEbomKanshiTsuchishoDao = New TShisakuBuhinEditEbomKanshiTsuchishoDaoImpl
            Dim zairyouDao As TShisakuBuhinEditEbomKanshiZairyouDao = New TShisakuBuhinEditEbomKanshiZairyouDaoImpl

            'T_SHISAKU_BUHIN_EDIT_EBOM_KANSHIから該当イベント（shisakuEventVo.ShisakuEventCode）の最新改訂Noの全部品情報を取得
            Dim ebomKanshiList As List(Of YakanSekkeiGetForCreateTsuchishoAndZairyouTargetVo) = _
                yakanSekkeiTenkaiDao.GetShisakuBuhinEditEbomKanshiForCreateTsuchishoAndZairyou(shisakuEventVo.ShisakuEventCode)

            '試作日はコンストラクタでインスタンスを生成したものを使用する
            'Dim aDate As New ShisakuDate

            'ユーザID
            Dim userId As String = ""
            If LoginInfo.Now.UserId IsNot Nothing Then
                userId = LoginInfo.Now.UserId
            End If

            Dim zairyouVo As New TShisakuBuhinEditEbomKanshiZairyouVo
            Dim tsuchishoVo As New TShisakuBuhinEditEbomKanshiTsuchishoVo

            '' 作成ユーザーID
            zairyouVo.CreatedUserId = userId
            '' 更新ユーザーID
            zairyouVo.UpdatedUserId = userId
            '' 作成日
            zairyouVo.CreatedDate = aDate.CurrentDateDbFormat
            '' 作成時
            zairyouVo.CreatedTime = aDate.CurrentTimeDbFormat
            '' 更新日
            zairyouVo.UpdatedDate = aDate.CurrentDateDbFormat
            '' 更新時
            zairyouVo.UpdatedTime = aDate.CurrentTimeDbFormat

            '' 作成ユーザーID
            tsuchishoVo.CreatedUserId = userId
            '' 更新ユーザーID
            tsuchishoVo.UpdatedUserId = userId
            '' 作成日
            tsuchishoVo.CreatedDate = aDate.CurrentDateDbFormat
            '' 作成時
            tsuchishoVo.CreatedTime = aDate.CurrentTimeDbFormat
            '' 更新日
            tsuchishoVo.UpdatedDate = aDate.CurrentDateDbFormat
            '' 更新時
            tsuchishoVo.UpdatedTime = aDate.CurrentTimeDbFormat

            Dim createData As YakanSekkeiGetForCreateTsuchishoAndZairyouVo
            Dim emptyData As New YakanSekkeiGetForCreateTsuchishoAndZairyouVo

            For Each ebomKanshiVo As YakanSekkeiGetForCreateTsuchishoAndZairyouTargetVo In ebomKanshiList

                '※ 廃止日はSQL中でチェックしている
                ''RHAC532から、材質、板厚、RHAC0700から、該当図面No、図面改訂Noの、設計通知書番号を取得
                createData = yakanSekkeiBlockDao.FindRHAC0532AndRHAC0700(ebomKanshiVo.BuhinNo)
                If createData Is Nothing Then

                    ''存在しなければ、RHAC533から、材質、板厚、RHAC0700から、該当図面No、図面改訂Noの、設計通知書番号を取得
                    createData = yakanSekkeiBlockDao.FindRHAC0533AndRHAC0700(ebomKanshiVo.BuhinNo)

                    If createData Is Nothing Then
                        ''存在しなければ、RHAC530から、材質、板厚、RHAC0700から、該当図面No、図面改訂Noの、設計通知書番号を取得
                        createData = yakanSekkeiBlockDao.FindRHAC0530AndRHAC0700(ebomKanshiVo.BuhinNo)

                        If createData Is Nothing Then
                            '' 存在しなければ、空データを設定
                            createData = emptyData
                        End If
                    End If
                End If

                '通知書登録
                'tsuchishoVo = New TShisakuBuhinEditEbomKanshiTsuchishoVo
                '' 試作イベントコード
                tsuchishoVo.ShisakuEventCode = ebomKanshiVo.ShisakuEventCode
                '' 試作部課コード
                tsuchishoVo.ShisakuBukaCode = ebomKanshiVo.ShisakuBukaCode
                '' 試作ブロック№
                tsuchishoVo.ShisakuBlockNo = ebomKanshiVo.ShisakuBlockNo
                '' 試作ブロック№改訂№
                tsuchishoVo.ShisakuBlockNoKaiteiNo = ebomKanshiVo.ShisakuBlockNoKaiteiNo
                '' 部品番号表示順
                tsuchishoVo.BuhinNoHyoujiJun = ebomKanshiVo.BuhinNoHyoujiJun

                '' 図面番号
                tsuchishoVo.ZumenNo = StringUtil.Nvl(createData.ZumenNo)
                '' 図面改訂No.
                tsuchishoVo.ZumenKaiteiNo = StringUtil.Nvl(createData.ZumenKaiteiNo)
                '' 設計通知書番号
                tsuchishoVo.TsuchishoNo = StringUtil.Nvl(createData.TsuchishoNo)

                'tsuchishoVo.CreatedUserId = userId
                'tsuchishoVo.UpdatedUserId = userId
                'tsuchishoVo.CreatedDate = aDate.CurrentDateDbFormat
                'tsuchishoVo.CreatedTime = aDate.CurrentTimeDbFormat
                'tsuchishoVo.UpdatedDate = aDate.CurrentDateDbFormat
                'tsuchishoVo.UpdatedTime = aDate.CurrentTimeDbFormat
                tsuchishoDao.InsertBy(tsuchishoVo)

                '材料登録
                ' T_SHISAKU_BUHIN_EDIT_EBOM_KANSHI_ZAIRYOUにINSERT
                'zairyouVo = New TShisakuBuhinEditEbomKanshiZairyouVo
                '' 試作イベントコード
                zairyouVo.ShisakuEventCode = ebomKanshiVo.ShisakuEventCode
                '' 試作部課コード
                zairyouVo.ShisakuBukaCode = ebomKanshiVo.ShisakuBukaCode
                '' 試作ブロック№
                zairyouVo.ShisakuBlockNo = ebomKanshiVo.ShisakuBlockNo
                '' 試作ブロック№改訂№
                zairyouVo.ShisakuBlockNoKaiteiNo = ebomKanshiVo.ShisakuBlockNoKaiteiNo
                '' 部品番号表示順
                zairyouVo.BuhinNoHyoujiJun = ebomKanshiVo.BuhinNoHyoujiJun

                '' 材料・材質
                zairyouVo.ZairyoKijutsu = StringUtil.Nvl(createData.ZairyoKijutsu)
                '' 板厚
                zairyouVo.BankoSuryo = StringUtil.Nvl(createData.BankoSuryo)

                'zairyouVo.CreatedUserId = userId
                'zairyouVo.UpdatedUserId = userId

                'zairyouVo.CreatedDate = aDate.CurrentDateDbFormat
                'zairyouVo.CreatedTime = aDate.CurrentTimeDbFormat
                'zairyouVo.UpdatedDate = aDate.CurrentDateDbFormat
                'zairyouVo.UpdatedTime = aDate.CurrentTimeDbFormat
                zairyouDao.InsertBy(zairyouVo)


            Next
        End Sub
        ' ''↓↓2014/08/19 Ⅰ.5.EBOM差分出力 be) (TES)張 ADD BEGIN
        'Public Sub GetTsuchishoNoZairyou()

        '    Dim yakanSekkeiTenkaiDao As New YakanSekkeiTenakai.Dao.YakanSekkeiTenkaiDaoImpl
        '    ''↓↓2014/08/27 Ⅰ.5.EBOM差分出力 be) 酒井 ADD BEGIN
        '    Dim yakanSekkeiBlockDao As New YakanSekkeiTenakai.Dao.YakanSekkeiBlockDaoImpl
        '    ''↑↑2014/08/27 Ⅰ.5.EBOM差分出力 be) 酒井 ADD END

        '    'T_SHISAKU_BUHIN_EDIT_EBOM_KANSHIから該当イベント（shisakuEventVo.ShisakuEventCode）の最新改訂Noの全部品情報を取得
        '    Dim ebomKanshiList As List(Of TShisakuBuhinEditEbomKanshiVo) = yakanSekkeiTenkaiDao.GetShisakuBuhinEditEbomKanshi(shisakuEventVo.ShisakuEventCode)

        '    Dim r0532Dao As Rhac0532Dao = New Rhac0532DaoImpl
        '    Dim r0533Dao As Rhac0533Dao = New Rhac0533DaoImpl
        '    Dim r0530Dao As Rhac0530Dao = New Rhac0530DaoImpl
        '    Dim r0700Dao As Rhac0700Dao = New Rhac0700DaoImpl
        '    Dim tsuchishoDao As TShisakuBuhinEditEbomKanshiTsuchishoDao = New TShisakuBuhinEditEbomKanshiTsuchishoDaoImpl
        '    Dim zairyouDao As TShisakuBuhinEditEbomKanshiZairyouDao = New TShisakuBuhinEditEbomKanshiZairyouDaoImpl

        '    Dim aDate As New ShisakuDate

        '    For Each ebomKanshiVo As TShisakuBuhinEditEbomKanshiVo In ebomKanshiList

        '        Dim r0532Vo As New Rhac0532Vo
        '        Dim r0533Vo As New Rhac0533Vo
        '        Dim r0530Vo As New Rhac0530Vo
        '        Dim r0700Vo As New Rhac0700Vo

        '        ''↓↓2014/08/27 Ⅰ.5.EBOM差分出力 be) 酒井 ADD BEGIN
        '        'RHAC532から、該当部品、最新改訂Noの、図面No、図面改訂No、材質、板厚を取得
        '        'r0532Vo = r0532Dao.FindByPk(ebomKanshiVo.BuhinNo, ebomKanshiVo.BuhinNoKaiteiNo)
        '        '
        '        '存在しなければ、RHAC0533から該当部品、最新改訂Noの、図面No、図面改訂No、材質、板厚を取得
        '        'If r0532Vo.HaisiDate <> 99999999 Then
        '        'r0533Vo = r0533Dao.FindByPk(ebomKanshiVo.BuhinNo, ebomKanshiVo.BuhinNoKaiteiNo)
        '        '
        '        '存在しなければ、RHAC0530から該当部品、最新改訂Noの、図面No、図面改訂No、材質、板厚を取得																																																																	
        '        'If r0533Vo.HaisiDate <> 99999999 Then
        '        'r0530Vo = r0530Dao.FindByPk(ebomKanshiVo.BuhinNo, ebomKanshiVo.BuhinNoKaiteiNo)
        '        'If r0530Vo.HaisiDate = 99999999 Then
        '        'RHAC0700から、該当図面No、図面改訂Noの、設計通知書番号を取得	
        '        'r0700Vo = r0700Dao.FindByPk(r0530Vo.ZumenNo, r0530Vo.ZumenKaiteiNo)
        '        'End If
        '        'Else
        '        'RHAC0700から、該当図面No、図面改訂Noの、設計通知書番号を取得																																																																	
        '        'r0700Vo = r0700Dao.FindByPk(r0533Vo.ZumenNo, r0533Vo.ZumenKaiteiNo)
        '        'End If
        '        'Else
        '        'RHAC0700から、該当図面No、図面改訂Noの、設計通知書番号を取得																																																																	
        '        'r0700Vo = r0700Dao.FindByPk(r0532Vo.ZumenNo, r0532Vo.ZumenKaiteiNo)
        '        'End If

        '        Dim r0532flg As Boolean = False
        '        Dim r0533flg As Boolean = False
        '        Dim r0530flg As Boolean = False
        '        ''↓↓2014/08/28 Ⅰ.5.EBOM差分出力 be) 酒井 ADD BEGIN
        '        Dim r0700flg As Boolean = False
        '        ''↑↑2014/08/28 Ⅰ.5.EBOM差分出力 be) 酒井 ADD END
        '        'RHAC532から、該当部品、最新改訂Noの、図面No、図面改訂No、材質、板厚を取得
        '        'r0532Vo = r0532Dao.FindByPk(ebomKanshiVo.BuhinNo, ebomKanshiVo.BuhinNoKaiteiNo)
        '        r0532Vo = yakanSekkeiBlockDao.FindRHAC0532(ebomKanshiVo.BuhinNo)
        '        If Not r0532Vo Is Nothing Then
        '            If r0532Vo.HaisiDate = 99999999 Then
        '                r0532flg = True
        '            End If
        '        End If

        '        If r0532flg = True Then
        '            'RHAC0700から、該当図面No、図面改訂Noの、設計通知書番号を取得																																																																	
        '            r0700Vo = r0700Dao.FindByPk(r0532Vo.ZumenNo, r0532Vo.ZumenKaiteiNo)
        '        Else
        '            '存在しなければ、RHAC0533から該当部品、最新改訂Noの、図面No、図面改訂No、材質、板厚を取得
        '            'r0533Vo = r0533Dao.FindByPk(ebomKanshiVo.BuhinNo, ebomKanshiVo.BuhinNoKaiteiNo)
        '            r0533Vo = yakanSekkeiBlockDao.FindRHAC0533(ebomKanshiVo.BuhinNo)
        '            If Not r0533Vo Is Nothing Then
        '                If r0533Vo.HaisiDate = 99999999 Then
        '                    r0533flg = True
        '                End If
        '            End If

        '            If r0533flg = True Then
        '                'RHAC0700から、該当図面No、図面改訂Noの、設計通知書番号を取得																																																																	
        '                r0700Vo = r0700Dao.FindByPk(r0533Vo.ZumenNo, r0533Vo.ZumenKaiteiNo)
        '            Else
        '                '存在しなければ、RHAC0530から該当部品、最新改訂Noの、図面No、図面改訂No、材質、板厚を取得	
        '                'r0530Vo = r0530Dao.FindByPk(ebomKanshiVo.BuhinNo, ebomKanshiVo.BuhinNoKaiteiNo)
        '                r0530Vo = yakanSekkeiBlockDao.FindRHAC0530(ebomKanshiVo.BuhinNo)
        '                If Not r0530Vo Is Nothing Then
        '                    If r0530Vo.HaisiDate = 99999999 Then
        '                        r0530flg = True
        '                    End If
        '                End If

        '                If r0530flg = True Then
        '                    'RHAC0700から、該当図面No、図面改訂Noの、設計通知書番号を取得	
        '                    r0700Vo = r0700Dao.FindByPk(r0530Vo.ZumenNo, r0530Vo.ZumenKaiteiNo)
        '                End If
        '            End If
        '        End If
        '        ''↑↑2014/08/27 Ⅰ.5.EBOM差分出力 be) 酒井 ADD END

        '        ' T_SHISAKU_BUHIN_EDIT_EBOM_KANSHI_TSUCHISHOにINSERT
        '        Dim tsuchishoVo As New TShisakuBuhinEditEbomKanshiTsuchishoVo
        '        '' 試作イベントコード
        '        tsuchishoVo.ShisakuEventCode = ebomKanshiVo.ShisakuEventCode
        '        '' 試作部課コード
        '        tsuchishoVo.ShisakuBukaCode = ebomKanshiVo.ShisakuBukaCode
        '        '' 試作ブロック№
        '        tsuchishoVo.ShisakuBlockNo = ebomKanshiVo.ShisakuBlockNo
        '        '' 試作ブロック№改訂№
        '        tsuchishoVo.ShisakuBlockNoKaiteiNo = ebomKanshiVo.ShisakuBlockNoKaiteiNo
        '        '' 部品番号表示順
        '        tsuchishoVo.BuhinNoHyoujiJun = ebomKanshiVo.BuhinNoHyoujiJun

        '        ''↓↓2014/08/27 Ⅰ.5.EBOM差分出力 be) 酒井 ADD BEGIN
        '        If Not r0700Vo Is Nothing Then
        '            If r0700Vo.HaisiDate = 99999999 Then
        '                r0700flg = True
        '            End If
        '        End If
        '        If r0700flg = True Then
        '            '                    If r0700Vo.HaisiDate = 99999999 Then
        '            ''↑↑2014/08/27 Ⅰ.5.EBOM差分出力 be) 酒井 ADD END

        '            '' 図面番号
        '            tsuchishoVo.ZumenNo = r0700Vo.ZumenNo
        '            '' 図面改訂No.
        '            tsuchishoVo.ZumenKaiteiNo = r0700Vo.ZumenKaiteiNo
        '            '' 設計通知書番号
        '            tsuchishoVo.TsuchishoNo = r0700Vo.TsuchishoNo
        '            ''↓↓2014/08/28 Ⅰ.5.EBOM差分出力 be) 酒井 ADD BEGIN
        '        Else
        '            tsuchishoVo.ZumenNo = ""
        '            tsuchishoVo.ZumenKaiteiNo = ""
        '            tsuchishoVo.TsuchishoNo = ""
        '            ''↑↑2014/08/28 Ⅰ.5.EBOM差分出力 be) 酒井 ADD END
        '        End If
        '        '' 作成ユーザーID
        '        ''↓↓2014/08/27 Ⅰ.5.EBOM差分出力 be) 酒井 ADD BEGIN
        '        If LoginInfo.Now.UserId Is Nothing Then
        '            tsuchishoVo.CreatedUserId = ""
        '        Else
        '            ''↑↑2014/08/27 Ⅰ.5.EBOM差分出力 be) 酒井 ADD END
        '            tsuchishoVo.CreatedUserId = LoginInfo.Now.UserId
        '            ''↓↓2014/08/27 Ⅰ.5.EBOM差分出力 be) 酒井 ADD BEGIN
        '        End If
        '        ''↑↑2014/08/27 Ⅰ.5.EBOM差分出力 be) 酒井 ADD END
        '        '' 作成日
        '        tsuchishoVo.CreatedDate = aDate.CurrentDateDbFormat
        '        '' 作成時
        '        tsuchishoVo.CreatedTime = aDate.CurrentTimeDbFormat
        '        '' 更新ユーザーID
        '        ''↓↓2014/08/27 Ⅰ.5.EBOM差分出力 be) 酒井 ADD BEGIN
        '        If LoginInfo.Now.UserId Is Nothing Then
        '            tsuchishoVo.UpdatedUserId = ""
        '        Else
        '            ''↑↑2014/08/27 Ⅰ.5.EBOM差分出力 be) 酒井 ADD END
        '            tsuchishoVo.UpdatedUserId = LoginInfo.Now.UserId
        '            ''↓↓2014/08/27 Ⅰ.5.EBOM差分出力 be) 酒井 ADD BEGIN
        '        End If
        '        ''↑↑2014/08/27 Ⅰ.5.EBOM差分出力 be) 酒井 ADD END
        '        '' 更新日
        '        tsuchishoVo.UpdatedDate = aDate.CurrentDateDbFormat
        '        '' 更新時間
        '        tsuchishoVo.UpdatedTime = aDate.CurrentTimeDbFormat
        '        tsuchishoDao.InsertBy(tsuchishoVo)

        '        ' T_SHISAKU_BUHIN_EDIT_EBOM_KANSHI_ZAIRYOUにINSERT
        '        Dim zairyouVo As New TShisakuBuhinEditEbomKanshiZairyouVo
        '        '' 試作イベントコード
        '        zairyouVo.ShisakuEventCode = ebomKanshiVo.ShisakuEventCode
        '        '' 試作部課コード
        '        zairyouVo.ShisakuBukaCode = ebomKanshiVo.ShisakuBukaCode
        '        '' 試作ブロック№
        '        zairyouVo.ShisakuBlockNo = ebomKanshiVo.ShisakuBlockNo
        '        '' 試作ブロック№改訂№
        '        zairyouVo.ShisakuBlockNoKaiteiNo = ebomKanshiVo.ShisakuBlockNoKaiteiNo
        '        '' 部品番号表示順
        '        zairyouVo.BuhinNoHyoujiJun = ebomKanshiVo.BuhinNoHyoujiJun

        '        ''↓↓2014/08/27 Ⅰ.5.EBOM差分出力 be) 酒井 ADD BEGIN
        '        'If r0532Vo.HaisiDate = 99999999 Then
        '        If r0532flg = True Then
        '            ''↑↑2014/08/27 Ⅰ.5.EBOM差分出力 be) 酒井 ADD END
        '            '' 材料・材質
        '            zairyouVo.ZairyoKijutsu = r0532Vo.ZairyoKijutsu
        '            '' 板厚
        '            zairyouVo.BankoSuryo = r0532Vo.BankoSuryo
        '        Else
        '            ''↓↓2014/08/27 Ⅰ.5.EBOM差分出力 be) 酒井 ADD BEGIN
        '            'If r0533Vo.HaisiDate = 99999999 Then
        '            If r0533flg = True Then
        '                ''↑↑2014/08/27 Ⅰ.5.EBOM差分出力 be) 酒井 ADD END
        '                '' 材料・材質
        '                zairyouVo.ZairyoKijutsu = r0533Vo.ZairyoKijutsu
        '                '' 板厚
        '                zairyouVo.BankoSuryo = r0533Vo.BankoSuryo
        '            Else
        '                ''↓↓2014/08/27 Ⅰ.5.EBOM差分出力 be) 酒井 ADD BEGIN
        '                'If r0530Vo.HaisiDate = 99999999 Then
        '                If r0530flg = True Then
        '                    ''↑↑2014/08/27 Ⅰ.5.EBOM差分出力 be) 酒井 ADD END
        '                    '' 材料・材質
        '                    zairyouVo.ZairyoKijutsu = r0530Vo.ZairyoKijutsu
        '                    '' 板厚
        '                    zairyouVo.BankoSuryo = r0530Vo.BankoSuryo
        '                    ''↓↓2014/08/28 Ⅰ.5.EBOM差分出力 be) 酒井 ADD BEGIN
        '                Else
        '                    zairyouVo.ZairyoKijutsu = ""
        '                    zairyouVo.BankoSuryo = ""
        '                    ''↑↑2014/08/28 Ⅰ.5.EBOM差分出力 be) 酒井 ADD END
        '                End If
        '            End If
        '        End If
        '        '' 作成ユーザーID
        '        ''↓↓2014/08/27 Ⅰ.5.EBOM差分出力 be) 酒井 ADD BEGIN
        '        If LoginInfo.Now.UserId Is Nothing Then
        '            zairyouVo.CreatedUserId = ""
        '        Else
        '            ''↑↑2014/08/27 Ⅰ.5.EBOM差分出力 be) 酒井 ADD END
        '            zairyouVo.CreatedUserId = LoginInfo.Now.UserId
        '            ''↓↓2014/08/27 Ⅰ.5.EBOM差分出力 be) 酒井 ADD BEGIN
        '        End If
        '        ''↑↑2014/08/27 Ⅰ.5.EBOM差分出力 be) 酒井 ADD END
        '        '' 作成日
        '        zairyouVo.CreatedDate = aDate.CurrentDateDbFormat
        '        '' 作成時
        '        zairyouVo.CreatedTime = aDate.CurrentTimeDbFormat
        '        '' 更新ユーザーID
        '        ''↓↓2014/08/27 Ⅰ.5.EBOM差分出力 be) 酒井 ADD BEGIN
        '        If LoginInfo.Now.UserId Is Nothing Then
        '            zairyouVo.UpdatedUserId = ""
        '        Else
        '            ''↑↑2014/08/27 Ⅰ.5.EBOM差分出力 be) 酒井 ADD END
        '            zairyouVo.UpdatedUserId = LoginInfo.Now.UserId
        '            ''↓↓2014/08/27 Ⅰ.5.EBOM差分出力 be) 酒井 ADD BEGIN
        '        End If
        '        ''↑↑2014/08/27 Ⅰ.5.EBOM差分出力 be) 酒井 ADD END
        '        '' 更新日
        '        zairyouVo.UpdatedDate = aDate.CurrentDateDbFormat
        '        '' 更新時間
        '        zairyouVo.UpdatedTime = aDate.CurrentTimeDbFormat
        '        zairyouDao.InsertBy(zairyouVo)
        '    Next

        'End Sub
        ' ''↑↑2014/08/19 Ⅰ.5.EBOM差分出力 be) (TES)張 ADD END
        '/*** 20140923 CHANGE END ***/

    End Class
End Namespace