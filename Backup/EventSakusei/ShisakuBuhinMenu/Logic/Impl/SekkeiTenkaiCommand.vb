Imports ShisakuCommon.Db.EBom.Vo.Helper
Imports ShisakuCommon.Db.EBom.Dao
Imports ShisakuCommon.Db.EBom.Dao.Impl
Imports ShisakuCommon.Db
Imports ShisakuCommon
Imports ShisakuCommon.Db.EBom
Imports ShisakuCommon.Db.EBom.Vo
Imports EventSakusei.ShisakuBuhinMenu.Logic.Impl.Tenkai
Imports EventSakusei.ShisakuBuhinEdit.Kosei.Logic.Matrix
Imports EventSakusei.ShisakuBuhinMenu.Dao

Namespace ShisakuBuhinMenu.Logic.Impl
    Public Class SekkeiTenkaiCommand : Implements Command
        Private ReadOnly shisakuEventVo As TShisakuEventVo
        Private ReadOnly login As LoginInfo
        Private ReadOnly aShisakuDao As ShisakuDao
        Private ReadOnly shisakuEventDao As TShisakuEventDao
        Private ReadOnly blockDao As TShisakuSekkeiBlockDao
        Private ReadOnly instlDao As TShisakuSekkeiBlockInstlDao
        Private ReadOnly kouseiDao As TShisakuBuhinKouseiDao
        Private ReadOnly buhinDao As TShisakuBuhinDao
        Private ReadOnly kaiteiSyochiBi As DateTime
        Private _newStatus As String

        Public Sub New(ByVal shisakuEventVo As TShisakuEventVo, _
                       ByVal login As LoginInfo, _
                       ByVal aShisakuDao As ShisakuDao, _
                       ByVal shisakuEventDao As TShisakuEventDao, _
                       ByVal blockDao As TShisakuSekkeiBlockDao, _
                       ByVal instlDao As TShisakuSekkeiBlockInstlDao, _
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
        End Sub

        Public Function GetNewStatus() As String Implements Command.GetNewStatus
            If _newStatus Is Nothing Then
                Throw New InvalidOperationException("#Perform()メソッドを実行してください.")
            End If
            Return _newStatus
        End Function

        Public Sub Perform() Implements Command.Perform
            Dim aDate As New ShisakuDate
            
            Dim eventDao As IShisakuEventDao = New ShisakuEventDaoImpl
            Dim unitKbnDictionary As New Dictionary2(Of String, String, String)()


            If Not (login Is Nothing) Then '夜間設計の場合　loginはブランク

                _newStatus = TShisakuEventVoHelper.Status.SEKKEI_MAINTAINING

                ' 更新前にselectしない
                UpdateTShisakuEventStatus(eventDao, shisakuEventVo.ShisakuEventCode, _newStatus, aDate, kaiteiSyochiBi, "0")
            End If

            Dim sekkeiBlock As New SekkeiBlockSupplier(shisakuEventVo, unitKbnDictionary)
            sekkeiBlock.Register(login, blockDao, aDate)

            Dim sekkeiBlockInstl As New SekkeiBlockInstlSupplier(shisakuEventVo, unitKbnDictionary)
            sekkeiBlockInstl.Register(login, instlDao, aDate)

            Dim buhinEdit As New BuhinEditTenkaiSubject(shisakuEventVo.ShisakuEventCode, login)   '2012/01/11 ここが遅い！！


            If Not login Is Nothing Then
                '以降の処理は画面表示上の問題等、EBOM部品構成に依存しないため、夜間処理時は実行対象外とした。

                'EBOMの設計課を更新する
                'buhinEdit.UpdateBukaCodeFromEbom()

                '２集計コード R/Yのブロック間紐付け_a) (TES)張 ADD BEGIN
                buhinEdit.UpdateKyoukuSection()

                'EBOM/試作イベントで同一のINSTLがあった場合は、EBOMに寄せる。
                buhinEdit.InstlAssyuku()


                '試作イベント部品抽出とEBOM部品抽出を（組み合わせ抽出時）直列にしたため、
                '後者のlevel=0の部品が前者のlevel=0以外の後にInsertされているので、先頭に集める。
                buhinEdit.BuhinSortLevelZero()
            End If

            Dim daoTemp As ShisakuBlockSekkeikaTmpDao = New ShisakuBlockSekkeikaTmpDaoImpl
            daoTemp.DeleteByEventCode(shisakuEventVo.ShisakuEventCode)



            If Not (login Is Nothing) Then   '夜間設計の場合　loginはブランク

                UpdateTShisakuEventStatus(eventDao, shisakuEventVo.ShisakuEventCode, _newStatus, aDate, kaiteiSyochiBi, "1")
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
            ''↓↓2014/07/24 Ⅰ.3.設計編集 ベース改修専用化_e) (TES)張 ADD BEGIN
            vo.KounyuShijiFlg = 0
            ''↑↑2014/07/24 Ⅰ.3.設計編集 ベース改修専用化_e) (TES)張 ADD END
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

        ''↓↓2014/08/05 Ⅰ.5.EBOM差分出力 be) (TES)施 ADD BEGIN
        Public Sub GetTsuchishoNoZairyou()

            Dim ebomKanshiDao As TShisakuBuhinEditEbomKanshiDao = New TShisakuBuhinEditEbomKanshiDaoImpl
            Dim paraVo As TShisakuBuhinEditEbomKanshiVo = New TShisakuBuhinEditEbomKanshiVo
            paraVo.ShisakuEventCode = shisakuEventVo.ShisakuEventCode

            '廃止日：99999999のデータが最新の改訂№になります
            'T_SHISAKU_BUHIN_EDIT_EBOM_KANSHIから該当イベント（shisakuEventVo.ShisakuEventCode）の最新改訂Noの全部品情報を取得
            Dim buhinList As List(Of TShisakuBuhinEditEbomKanshiVo) = ebomKanshiDao.FindBy(paraVo)
            Dim r0532Dao As Rhac0532Dao = New Rhac0532DaoImpl
            Dim r0533Dao As Rhac0533Dao = New Rhac0533DaoImpl
            Dim r0530Dao As Rhac0533Dao = New Rhac0533DaoImpl
            Dim r0700Dao As Rhac0700Dao = New Rhac0700DaoImpl
            For Each vo As TShisakuBuhinEditEbomKanshiVo In buhinList
                ' vo.BuhinNoKaiteiNo


                'RHAC532から、該当部品、最新改訂Noの、図面No、図面改訂No、材質、板厚を取得	


                '存在しなければ、RHAC0533から該当部品、最新改訂Noの、図面No、図面改訂No、材質、板厚を取得																																																																	
                '存在しなければ、RHAC0530から該当部品、最新改訂Noの、図面No、図面改訂No、材質、板厚を取得																																																																	

                'RHAC0700から、該当図面No、図面改訂Noの、設計通知書番号を取得																																																																	

                ' T_SHISAKU_BUHIN_EDIT_EBOM_KANSHI_TSUCHISHOにINSERT()
                ' T_SHISAKU_BUHIN_EDIT_EBOM_KANSHI_ZAIRYOUにINSERT()
            Next

        End Sub


        ''↑↑2014/08/05 Ⅰ.5.EBOM差分出力 be) (TES)施 ADD END

    End Class
End Namespace