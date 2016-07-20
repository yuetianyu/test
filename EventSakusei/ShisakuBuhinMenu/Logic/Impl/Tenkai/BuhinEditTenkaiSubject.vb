Imports ShisakuCommon
Imports ShisakuCommon.Db.EBom.Exclusion
Imports EventSakusei.ShisakuBuhinMenu.Dao
Imports ShisakuCommon.Db.EBom.Vo
Imports EventSakusei.ShisakuBuhinEdit.Al.Dao
Imports EventSakusei.ShisakuBuhinEdit.Logic.Detect
Imports ShisakuCommon.Db.EBom
Imports EventSakusei.ShisakuBuhinEdit.Logic
Imports ShisakuCommon.Db.EBom.Dao
Imports ShisakuCommon.Db.EBom.Dao.Impl
Imports ShisakuCommon.Db
Imports ShisakuCommon.Db.Impl
Imports EventSakusei.ShisakuBuhinEdit.Kosei.Logic.Matrix
Imports EventSakusei.ShisakuBuhinEdit.Kosei.Vo
Imports EventSakusei.ShisakuBuhinEdit.Kosei.Dao

'Imports EventSakusei.ShisakuBuhinEditBlock.Dao

Namespace ShisakuBuhinMenu.Logic.Impl.Tenkai
    '構成用(実験クラス)'
    Public Class BuhinEditTenkaiSubject

        Private ReadOnly aShisakuDate As New ShisakuDate
        Private ReadOnly exclusionShisakuEvent As New TShisakuEventExclusion

        Private ReadOnly aLoginInfo As LoginInfo
        Private ReadOnly anAlSubject As BuhinEditInstlSupplier
        Private ReadOnly aKoseiSubject As BuhinEditBaseSupplier

        Private ReadOnly anEzSync As BuhinEditInstlHinban
        Private impl As EventSakusei.ShisakuBuhinMenu.Dao.BuhinEditBaseDao
        Private sbVoList As List(Of TShisakuSekkeiBlockVo)
        Private eventCode As String
        Private jikyuUmu As String
        Public Sub New()

        End Sub

        Public Sub New(ByVal shisakuEventCode As String, Optional ByVal login As LoginInfo = Nothing, Optional ByVal yakanFlg As Boolean = False)
            Me.aLoginInfo = aLoginInfo
            If Not (login Is Nothing) Then
                Me.aLoginInfo = login
            End If
            Me.anEzSync = New BuhinEditInstlHinban
            impl = New EventSakusei.ShisakuBuhinMenu.Dao.BuhinEditBaseDaoImpl

            Dim editDao As TShisakuBuhinEditDao = New TShisakuBuhinEditDaoImpl
            Dim editInstlDao As TShisakuBuhinEditInstlDao = New TShisakuBuhinEditInstlDaoImpl
            Dim makeDao As MakeStructureResultDao = New MakeStructureResultDaoImpl
            Dim blockInstl As ShisakuSekkeiBlockInstlDao = New ShisakuSekkeiBlockInstlDaoImpl
            Dim alDao As New BuhinEditAlDaoImpl
            Dim kotanTorihikisakiSelected As New Dictionary(Of String, TShisakuBuhinEditVo)

            sbVoList = New List(Of TShisakuSekkeiBlockVo)

            If Not (login Is Nothing) Then
                sbVoList = impl.FindBySekkeiBlockAll(shisakuEventCode)
            Else
                Dim sbVoListEbomKanashi As List(Of TShisakuSekkeiBlockEbomKanshiVo)
                sbVoListEbomKanashi = impl.FindBySekkeiBlockEbomKanshiAll(shisakuEventCode)

                For index = 0 To sbVoListEbomKanashi.Count - 1
                    Dim sbVo As New TShisakuSekkeiBlockVo
                    VoUtil.CopyProperties(sbVoListEbomKanashi(index), sbVo)
                    sbVoList.Add(sbVo)
                Next
            End If
            eventCode = shisakuEventCode

            Dim eventDao As TShisakuEventDao = New TShisakuEventDaoImpl
            '自給品有無を取得
            Dim eventVo As New TShisakuEventVo
            eventVo = eventDao.FindByPk(shisakuEventCode)
            Dim alEventList As List(Of BuhinEditAlEventVo) = alDao.FindEventInfoById(shisakuEventCode)

            Dim ezTunnel As New BuhinEditEzTunnel

            '############################################################################
            '2012/01/12 現状：ブロック単位で繰り返し処理（無駄がある）
            '　　　　　 結論：実装方法を考え直す必要有り（ベース単位で処理できないか？）
            '　　　　　 対策：現状は、遅いので実装方法が確定するまでは、そのままとする
            '############################################################################
            '念のため、同じブロックNoは無視させるため'
            '部課コードが異なる場合はそのまま通す'
            Dim blockNo As String = ""
            Dim bukaCode As String = ""

            For Each AVo As TShisakuSekkeiBlockVo In sbVoList

                If StringUtil.Equals(bukaCode, AVo.ShisakuBukaCode) Then
                    If StringUtil.Equals(blockNo, AVo.ShisakuBlockNo) Then
                        Continue For
                    End If
                End If

                anAlSubject = New BuhinEditInstlSupplier(AVo, aLoginInfo, aShisakuDate, anEzSync, Nothing, Nothing, alDao, eventVo.UpdatedDate, alEventList)

                aKoseiSubject = New BuhinEditBaseSupplier(AVo, aLoginInfo, aShisakuDate, ezTunnel, _
                                                          Nothing, _
                                                          New MakeStructureResultImpl(shisakuEventCode, AVo.ShisakuBukaCode, AVo.ShisakuBlockNo, kotanTorihikisakiSelected, editDao, editInstlDao, makeDao), _
                                                          New MakerNameResolverImpl, _
                                                          editDao, _
                                                          editInstlDao, _
                                                          blockInstl, _
                                                          eventVo)

                anEzSync.Initialize(anAlSubject, aKoseiSubject)
                ezTunnel.Initialize(anAlSubject, aKoseiSubject)
                'ここから部品構成の取得開始'
                aKoseiSubject.PerformInitialized(yakanFlg)

                blockNo = AVo.ShisakuBlockNo
                bukaCode = AVo.ShisakuBukaCode
            Next

        End Sub

        Private Class MakerNameResolverImpl : Implements MakerNameResolver
            Private dao As New Rhac0610DaoImpl
            Private cache As New Dictionary(Of String, Rhac0610Vo)
            Public Function Resolve(ByVal makerCode As String) As String Implements MakerNameResolver.Resolve
                If StringUtil.IsEmpty(makerCode) Then
                    Return Nothing
                End If
                If Not cache.ContainsKey(makerCode) Then
                    cache.Add(makerCode, dao.FindByPk(makerCode))
                End If

                Dim result As Rhac0610Vo = cache(makerCode)
                If result Is Nothing Then
                    Return Nothing
                End If
                Return result.MakerName
            End Function
        End Class

        ''' <summary>
        ''' 登録処理
        ''' </summary>
        ''' <remarks></remarks>
        Public Sub Register()
            Register("登録", AddressOf RegisterMain)
        End Sub
        ''' <summary>
        ''' 登録処理本体
        ''' </summary>
        ''' <remarks></remarks>
        Private Sub RegisterMain()
            aShisakuDate.Clear()
            anAlSubject.Register()
            aKoseiSubject.Register()
        End Sub

        Private Delegate Sub RegisterInner()
        ''' <summary>
        ''' 登録処理
        ''' </summary>
        ''' <param name="processName">処理名</param>
        ''' <param name="aRegisterInner">処理本体のDelegate</param>
        ''' <remarks></remarks>
        Private Sub Register(ByVal processName As String, ByVal aRegisterInner As RegisterInner)
            Using db As New EBomDbClient
                db.BeginTransaction()
                If exclusionShisakuEvent.WasUpdatedBySomeone() Then
                    db.Rollback()
                    Dim userName As String = ResolveUserName(exclusionShisakuEvent.GetUpdatedUserId)
                    ' TODO メッセージ適当。適宜修正する。
                    Throw New TableExclusionException(String.Format("このデータは先程 {0} 様に更新されました。" & vbCrLf & "「戻る」から更新内容を確認してください。", userName))
                End If

                aRegisterInner()

                exclusionShisakuEvent.UpdateAndSave(aLoginInfo.UserId)

                db.Commit()
            End Using
        End Sub

        ''' <summary>
        ''' ユーザー名を解決する
        ''' </summary>
        ''' <param name="userId">ユーザーID</param>
        ''' <returns>ユーザー名</returns>
        ''' <remarks></remarks>
        Private Function ResolveUserName(ByVal userId As String) As String

            Dim dao As New ShisakuDaoImpl
            Dim userVo As Rhac0650Vo = dao.FindUserById(userId)

            If userVo Is Nothing Then
                Return userId
            End If
            Return userVo.ShainName
        End Function

        ''' <summary>A/L画面用のSubject</summary>
        Public ReadOnly Property AlSubject() As BuhinEditInstlSupplier
            Get
                Return anAlSubject
            End Get
        End Property
        ''' <summary>部品構成画面用のSubject</summary>
        Public ReadOnly Property KoseiSubject() As BuhinEditBaseSupplier
            Get
                Return aKoseiSubject
            End Get
        End Property

#Region "自給品無しの場合に部品の無いブロックが存在するので削除する"

        '2012/01/23'

        ''' <summary>
        ''' 試作設計ブロックINSTL情報と試作設計ブロック情報を削除する
        ''' </summary>
        ''' <remarks></remarks>
        Public Sub Delete()

            Dim dao As Dao.SekkeiBlockDao = New Dao.SekkeiBlockDaoImpl

            '自給品の有無を確認'
            Dim eventVo As New TShisakuEventVo
            eventVo = dao.FindByShisakuEvent(eventCode)
            If StringUtil.Equals(eventVo.JikyuUmu, "1") Then
                '自給品有りなら削除処理は行わない'
                Return
            End If

            '試作設計ブロック情報を取得する'
            Dim blockVoList As New List(Of TShisakuSekkeiBlockVo)
            blockVoList = dao.FindByShisakuBlockAll(eventCode)

            For Each vo As TShisakuSekkeiBlockVo In blockVoList
                '試作設計ブロックINSTL情報を取得する'
                Dim instlVoList As New List(Of TShisakuSekkeiBlockInstlVo)
                instlVoList = dao.FindByShisakuSekkeiBlockInstl(vo.ShisakuEventCode, vo.ShisakuBukaCode, vo.ShisakuBlockNo)
                Dim count As Integer = 0
                For Each ivo As TShisakuSekkeiBlockInstlVo In instlVoList
                    '設計ブロックINSTL情報を元に部品編集INSTL情報を確認する'
                    Dim editInstlVos As New List(Of TShisakuBuhinEditInstlVo)
                    editInstlVos = dao.FindByShisakuBuhinEditInstl(ivo.ShisakuEventCode, ivo.ShisakuBukaCode, ivo.ShisakuBlockNo, ivo.InstlHinbanHyoujiJun)

                    If editInstlVos.Count = 0 Then
                        count = count + 1
                    End If
                Next
                '親品番は全て自給品なので削除する'
                If count = instlVoList.Count Then
                    dao.DeleteByShisakuBuhinEditInstl(vo.ShisakuEventCode, vo.ShisakuBukaCode, vo.ShisakuBlockNo)

                End If

            Next
        End Sub



#End Region


        ''↓↓2014/08/04 ２）集計コード R/Yのブロック間紐付け_b) (TES) 施 ADD BEGIN
        ''' <summary>
        '''施雪辰
        ''' </summary>
        Public Sub UpdateKyoukuSection()
            Dim shisakuSekkeiBlockInstlDao As TShisakuSekkeiBlockInstlDao = New TShisakuSekkeiBlockInstlDaoImpl
            Dim shisakuBuhinEditInstlDao As TShisakuBuhinEditInstlDao = New TShisakuBuhinEditInstlDaoImpl
            Dim shisakuBuhinEditDao As TShisakuBuhinEditDao = New TShisakuBuhinEditDaoImpl
            Dim iShisakuBuhinEditDao As ShisakuBuhinEditBlock.Dao.IShisakuBuhinEditDao = New ShisakuBuhinEditDaoImpl
            Dim oyaBuhinNoListString As String = ""
            For Each oneRecord As Temp In GetUpdateSource(Me.eventCode)

                '親部品番号リストをカンマ区切り
                ''↓↓2014/09/17 ２）集計コード R/Yのブロック間紐付け_b) 酒井 ADD BEGIN
                oyaBuhinNoListString = ""
                ''↑↑2014/09/17 ２）集計コード R/Yのブロック間紐付け_b) 酒井 ADD END
                For Each oyaBuhinNo As String In oneRecord.oyaBuHinNoList
                    oyaBuhinNoListString = oyaBuhinNoListString & oyaBuhinNo & ","
                Next

                '最後のカマをカット
                oyaBuhinNoListString = oyaBuhinNoListString.Substring(0, oyaBuhinNoListString.Length - 1)
                ''↓↓2014/09/11 ２）集計コード R/Yのブロック間紐付け_b) 酒井 ADD BEGIN
                '文字数ではなく、バイト数が正しい。
                oyaBuhinNoListString = Left(oyaBuhinNoListString, 60)
                ''↑↑2014/09/11 ２）集計コード R/Yのブロック間紐付け_b) 酒井 ADD END

                ''3-1)T_SHISAKU_SEKKEI_BLOCK_INSTLから該当イベント内で、部品番号と同じINSTL品番をもつレコードを取得		
                '  Dim blockParam As TShisakuSekkeiBlockInstlVo
                'blockParam = New TShisakuSekkeiBlockInstlVo
                'blockParam.ShisakuEventCode = Me.eventCode
                'blockParam.InstlHinban = oneRecord.koBuhinNo
                'For Each blockInstlVo As TShisakuSekkeiBlockInstlVo In shisakuSekkeiBlockInstlDao.FindBy(blockParam)
                'Next
                '3-2)T_SHISAKU_BUHIN_EDIT_INSTLから上記3-1)で取得したINSTL品番の員数ゼロでない部品番号、かつ集計コードYの部品を取得																																																			
                For Each oyaBuhinVo As TShisakuBuhinEditVo In iShisakuBuhinEditDao.FindBuhinWithCodeYByInstl(oneRecord.koBuhinNo, Me.eventCode)
                    '3-3)備考に親部品番号リストをカンマ区切りで更新
                    oyaBuhinVo.Bikou = oyaBuhinNoListString
                    Dim maker As String = ""
                    '親取引先コードリスト.count=1の場合、供給セクションに親取引先コード+0で更新																																										
                    If (oneRecord.oyaBuhinMakerCodeList.Count = 1) Then
                        'QA

                        oyaBuhinVo.KyoukuSection = oneRecord.oyaBuhinMakerCodeList(0) & "0"
                        maker = oyaBuhinVo.KyoukuSection
                    End If
                    '親部品更新
                    shisakuBuhinEditDao.UpdateByPk(oyaBuhinVo)

                    '3-4)T_SHISAKU_BUHIN_EDITからLEVEL>0、部品番号と同じ部品、かつ集計コードYの部品を取得																																								
                    '3-5)備考に親部品番号リストをカンマ区切りで更新																																								
                    '親取引先コードリスト.count=1の場合、供給セクションに親取引先コード+0で更新	
                    ''↓↓2014/09/17 ２）集計コード R/Yのブロック間紐付け_b) 酒井 ADD BEGIN
                    'iShisakuBuhinEditDao.UpdateKoBuhinBikou(oyaBuhinVo.BuhinNo, oyaBuhinNoListString, maker, Me.eventCode)
                    iShisakuBuhinEditDao.UpdateKoBuhinBikou(oneRecord.koBuhinNo, oyaBuhinNoListString, maker, Me.eventCode)
                    ''↑↑2014/09/17 ２）集計コード R/Yのブロック間紐付け_b) 酒井 ADD END
                Next

            Next


        End Sub

        ''' <summary>
        ''' 更新元のデータリストを得る
        ''' </summary>
        ''' <param name="eventCode">イベントコード</param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Private Function GetUpdateSource(ByVal eventCode As String) As List(Of Temp)

            Dim shisakuBuhinEditDao As ShisakuBuhinEditBlock.Dao.IShisakuBuhinEditDao = New ShisakuBuhinEditDaoImpl
            '1)該当イベントのEBOM設計展開情報から、国内集計コードRまたは海外集計コードRの部品を取得		
            Dim rCodeList As List(Of TShisakuBuhinEditVo) = shisakuBuhinEditDao.FindCodeRBuhinByEventCode(eventCode)
            '親部品のリスト
            Dim oyaBuhinList As List(Of TShisakuBuhinEditVo)
            '更新元
            Dim updateSource As List(Of Temp) = New List(Of Temp)
            '親取引先コード
            Dim makerCode As String
            '海外集計コード
            Dim kaigaiCode As String
            '国内集計コード
            Dim kokunaiCode As String
            '一レコード
            Dim oneRecode As Temp
            '親部品番号リスト
            Dim oyaBuhinNoList As List(Of String)
            '親引取コードリスト
            Dim oyaMakerCodeList As List(Of String)
            '2)1)の各々について																																																																							
            '2-1)EBOM設計展開情報から親部品番号を取得	
            '2-1-1)1)のLEVELが1の場合、T_SHISAKU_BUHIN_EDIT_INSTLから員数ゼロでないINSTL品番を親部品番号とする
            '2-1-2)1)のLEVELが2以上の場合、T_SHISAKU_BUHIN_EDITからLEVELが直上となる、直近の部品を親部品番号とする	
            For Each vo As TShisakuBuhinEditVo In rCodeList
                If vo.Level() = 1 Then
                    oyaBuhinList = shisakuBuhinEditDao.FindOyaBuhinByLevelOne(vo.ShisakuEventCode, vo.ShisakuBukaCode, vo.ShisakuBlockNo, vo.ShisakuBlockNoKaiteiNo, vo.BuhinNoHyoujiJun)
                    ''↓↓2014/08/26 ２）集計コード R/Yのブロック間紐付け_b) 酒井 ADD BEGIN
                    'Else
                ElseIf vo.Level() > 1 Then
                    'oyaBuhinList = shisakuBuhinEditDao.FindOyaBuhinByHighLevel(vo.ShisakuEventCode, vo.ShisakuBukaCode, vo.ShisakuBlockNo, vo.ShisakuBlockNoKaiteiNo, vo.BuhinNoHyoujiJun)
                    oyaBuhinList = shisakuBuhinEditDao.FindOyaBuhinByHighLevel(vo.ShisakuEventCode, vo.ShisakuBukaCode, vo.ShisakuBlockNo, vo.ShisakuBlockNoKaiteiNo, vo.BuhinNoHyoujiJun, vo.Level)
                Else
                    Continue For
                    ''↑↑2014/08/26 ２）集計コード R/Yのブロック間紐付け_b) 酒井 ADD END
                End If
                '2-2)T_SHISAKU_BUHIN_EDITから2-1)で取得した親部品番号の取引先コード（親取引先コード）と国内集計コードと海外集計コードを取得する																																																																				
                '2-3)2-2)の国内集計コードまたは海外集計コードが、A,EまたはYの場合			
                oneRecode = New Temp
                oyaBuhinNoList = New List(Of String)
                oyaMakerCodeList = New List(Of String)
                For Each oyaVo As TShisakuBuhinEditVo In oyaBuhinList
                    kaigaiCode = oyaVo.SiaShukeiCode()
                    kokunaiCode = oyaVo.ShukeiCode()
                    makerCode = oyaVo.MakerCode()
                    If (kaigaiCode = "A" OrElse kaigaiCode = "E" OrElse kaigaiCode = "Y" OrElse kokunaiCode = "A" OrElse kokunaiCode = "E" OrElse kokunaiCode = "Y") Then
                        oneRecode.koBuhinNo = vo.BuhinNo
                        '親部品番号リスト内はユニークとする
                        If Not (oyaBuhinNoList.Contains(oyaVo.BuhinNo())) Then
                            oyaBuhinNoList.Add(oyaVo.BuhinNo())
                        End If
                        '親取引先コードリスト内はユニークとし、ブランクは追加しない
                        If (Not (oyaMakerCodeList.Contains(makerCode)) AndAlso Not (makerCode Is Nothing) AndAlso (makerCode <> "")) Then
                            oyaMakerCodeList.Add(makerCode)
                        End If
                    End If
                Next
                oneRecode.oyaBuhinMakerCodeList = oyaMakerCodeList
                oneRecode.oyaBuHinNoList = oyaBuhinNoList
                If Not (oneRecode.koBuhinNo Is Nothing) Then
                    '部品番号はユニークとする
                    '更新元リスト更新フラグ
                    Dim updateFlg As Boolean = False
                    '同じ部品番号が複数にあるなら
                    '更新元を循環、
                    For Each tempRecode As Temp In updateSource
                        ' 同じ部品番号があるなら、その親部品リストと親取引先コードリストを集合する
                        If tempRecode.koBuhinNo = vo.BuhinNo Then
                            updateFlg = True
                            tempRecode.oyaBuhinMakerCodeList.AddRange(oneRecode.oyaBuhinMakerCodeList)
                            'ユニークのため
                            For Each code As String In oneRecode.oyaBuhinMakerCodeList
                                If Not (tempRecode.oyaBuhinMakerCodeList.Contains(code)) Then
                                    tempRecode.oyaBuhinMakerCodeList.Add(code)
                                End If
                            Next

                            For Each code As String In oneRecode.oyaBuHinNoList
                                If Not (tempRecode.oyaBuHinNoList.Contains(code)) Then
                                    tempRecode.oyaBuHinNoList.Add(code)
                                End If
                            Next
                        End If
                    Next
                    '元のリストに同じ部品番号のデータがありません
                    If (updateFlg = False) Then
                        updateSource.Add(oneRecode)
                    End If
                End If
            Next
            Return updateSource
        End Function

        ''' <summary>
        ''' 集計コード R/Yのブロック間紐付けb専用一時Structure
        ''' </summary>
        ''' <remarks></remarks>
        Private Structure Temp
            ''' <summary>
            ''' 子部品
            ''' </summary>
            ''' <remarks></remarks>
            Dim koBuhinNo As String
            ''' <summary>
            ''' 親部品リスト
            ''' </summary>
            ''' <remarks></remarks>
            Dim oyaBuHinNoList As List(Of String)
            ''' <summary>
            ''' 親部品取引コードリスト
            ''' </summary>
            ''' <remarks></remarks>
            Dim oyaBuhinMakerCodeList As List(Of String)
        End Structure

        Public Sub InstlAssyuku()
            '構成違いflg
            Dim koseiFlag As Boolean = False

            Dim blockInstlDao As ShisakuSekkeiBlockInstlDao = New ShisakuSekkeiBlockInstlDaoImpl
            Dim instlDao As TShisakuSekkeiBlockInstlDao = New TShisakuSekkeiBlockInstlDaoImpl
            Dim editInstlDao As TShisakuBuhinEditInstlDao = New TShisakuBuhinEditInstlDaoImpl
            Dim editDao As TShisakuBuhinEditDao = New TShisakuBuhinEditDaoImpl

            Dim editInstlBaseDao As TShisakuBuhinEditInstlBaseDao = New TShisakuBuhinEditInstlBaseDaoImpl
            Dim editBaseDao As TShisakuBuhinEditBaseDao = New TShisakuBuhinEditBaseDaoImpl
            Dim blockInstlVos As List(Of TShisakuSekkeiBlockInstlVo) = blockInstlDao.FindByEventCode(eventCode)

            Dim instlKoseiVos As List(Of BuhinEditInstlKoseiVo)
            Dim instlKoseiVos2 As List(Of BuhinEditInstlKoseiVo)
            Dim tempInstlVos As List(Of TShisakuSekkeiBlockInstlVo)

            For index1 As Integer = 0 To blockInstlVos.Count - 1
                For index2 As Integer = index1 + 1 To blockInstlVos.Count - 1
                    '結果セットの中にブロックNo、INSTL品番、INSTL品番区分が同じレコードが存在する場合
                    If (blockInstlVos(index1).ShisakuBlockNo.Equals(blockInstlVos(index2).ShisakuBlockNo) AndAlso _
                        blockInstlVos(index1).InstlHinban.Equals(blockInstlVos(index2).InstlHinban) AndAlso _
                        blockInstlVos(index1).InstlDataKbn.Equals(blockInstlVos(index2).InstlDataKbn) AndAlso _
                        blockInstlVos(index1).InstlHinbanKbn.Equals(blockInstlVos(index2).InstlHinbanKbn)) Then


                        'T_SHISAKU_BUHIN_EDIT、T_SHISAKU_BUHIN_EDIT_INSTLから各々の部品構成と員数を、
                        'LEVEL / 国内集計コード　/　海外集計コード / 部品番号 / メーカーコード / 供給セクション / 員数順に取得
                        instlKoseiVos = impl.FindByBuhinKousei(blockInstlVos(index1).ShisakuEventCode, blockInstlVos(index1).ShisakuBukaCode, blockInstlVos(index1).ShisakuBlockNo, blockInstlVos(index1).ShisakuBlockNoKaiteiNo, blockInstlVos(index1).InstlHinbanHyoujiJun)
                        instlKoseiVos2 = impl.FindByBuhinKousei(blockInstlVos(index2).ShisakuEventCode, blockInstlVos(index2).ShisakuBukaCode, blockInstlVos(index2).ShisakuBlockNo, blockInstlVos(index2).ShisakuBlockNoKaiteiNo, blockInstlVos(index2).InstlHinbanHyoujiJun)
                        If instlKoseiVos.Count = instlKoseiVos2.Count Then
                            koseiFlag = True
                            For buhinIndex1 As Integer = 0 To instlKoseiVos.Count - 1
                                If Not (instlKoseiVos(buhinIndex1).Level.Equals(instlKoseiVos2(buhinIndex1).Level) AndAlso _
                                    instlKoseiVos(buhinIndex1).ShukeiCode.Equals(instlKoseiVos2(buhinIndex1).ShukeiCode) AndAlso _
                                    instlKoseiVos(buhinIndex1).SiaShukeiCode.Equals(instlKoseiVos2(buhinIndex1).SiaShukeiCode) AndAlso _
                                    instlKoseiVos(buhinIndex1).BuhinNo.Equals(instlKoseiVos2(buhinIndex1).BuhinNo) AndAlso _
                                    instlKoseiVos(buhinIndex1).MakerCode.Equals(instlKoseiVos2(buhinIndex1).MakerCode) AndAlso _
                                    instlKoseiVos(buhinIndex1).KyoukuSection.Equals(instlKoseiVos2(buhinIndex1).KyoukuSection) AndAlso _
                                    instlKoseiVos(buhinIndex1).InsuSuryo.Equals(instlKoseiVos2(buhinIndex1).InsuSuryo)) Then
                                    koseiFlag = False
                                    Exit For
                                End If

                            Next
                        End If
                        Dim mergeInstl As TShisakuSekkeiBlockInstlVo
                        Dim deleteInstl As TShisakuSekkeiBlockInstlVo
                        Dim gousyas As New List(Of String)
                        If koseiFlag Then
                            '比較した結果セットの内、INSTL元データ区分が小さい方を圧縮先INSTL、大きい方を削除対象INSTLとする	
                            If Integer.Parse(blockInstlVos(index1).InstlDataKbn) < Integer.Parse(blockInstlVos(index2).InstlDataKbn) Then
                                mergeInstl = blockInstlVos(index1)
                                deleteInstl = blockInstlVos(index2)
                            Else
                                mergeInstl = blockInstlVos(index2)
                                deleteInstl = blockInstlVos(index1)
                            End If
                            tempInstlVos = instlDao.FindBy(deleteInstl)
                            For Each tempInstlVo As TShisakuSekkeiBlockInstlVo In tempInstlVos
                                If tempInstlVo.InsuSuryo = 1 Then
                                    gousyas.Add(tempInstlVo.ShisakuGousya)
                                End If
                            Next
                            tempInstlVos = instlDao.FindBy(mergeInstl)
                            For Each tempInstlVo As TShisakuSekkeiBlockInstlVo In tempInstlVos
                                For Each gousya As String In gousyas
                                    tempInstlVo.ShisakuGousya = gousya
                                    'PKで削除に変更　2014/12/05
                                    'instlDao.DeleteBy(tempInstlVo)
                                    instlDao.DeleteByPk(tempInstlVo.ShisakuEventCode, _
                                                        tempInstlVo.ShisakuBukaCode, _
                                                        tempInstlVo.ShisakuBlockNo, _
                                                        tempInstlVo.ShisakuBlockNoKaiteiNo, _
                                                        tempInstlVo.ShisakuGousya, _
                                                        tempInstlVo.InstlHinban, _
                                                        tempInstlVo.InstlHinbanKbn, _
                                                        tempInstlVo.InstlDataKbn, _
                                                        tempInstlVo.BaseInstlFlg)
                                    tempInstlVo.InsuSuryo = 1
                                    instlDao.InsertBy(tempInstlVo)
                                Next
                            Next
                            Dim deleteBuhin As New List(Of TShisakuBuhinEditVo)
                            impl.DeleteByBuhinKousei(deleteInstl.ShisakuEventCode, deleteInstl.ShisakuBukaCode, deleteInstl.ShisakuBlockNo, deleteInstl.ShisakuBlockNoKaiteiNo, deleteInstl.InstlHinbanHyoujiJun, deleteBuhin)

                            'PKで削除に変更　2014/12/05
                            'instlDao.DeleteBy(deleteInstl)
                            instlDao.DeleteByPk(deleteInstl.ShisakuEventCode, _
                                                deleteInstl.ShisakuBukaCode, _
                                                deleteInstl.ShisakuBlockNo, _
                                                deleteInstl.ShisakuBlockNoKaiteiNo, _
                                                deleteInstl.ShisakuGousya, _
                                                deleteInstl.InstlHinban, _
                                                deleteInstl.InstlHinbanKbn, _
                                                deleteInstl.InstlDataKbn, _
                                                deleteInstl.BaseInstlFlg)




                            Dim deleteInstl2 As New TShisakuSekkeiBlockInstlVo
                            VoUtil.CopyProperties(deleteInstl, deleteInstl2)
                            deleteInstl2.ShisakuBlockNoKaiteiNo = "  0"
                            instlDao.DeleteBy(deleteInstl2)
                            Dim editInstlVo As New TShisakuBuhinEditInstlVo
                            editInstlVo.ShisakuEventCode = deleteInstl.ShisakuEventCode
                            editInstlVo.ShisakuBukaCode = deleteInstl.ShisakuBukaCode
                            editInstlVo.ShisakuBlockNo = deleteInstl.ShisakuBlockNo
                            editInstlVo.ShisakuBlockNoKaiteiNo = deleteInstl.ShisakuBlockNoKaiteiNo
                            editInstlVo.InstlHinbanHyoujiJun = deleteInstl.InstlHinbanHyoujiJun
                            editInstlDao.DeleteBy(editInstlVo)
                            Dim editInstlBaseVo As New TShisakuBuhinEditInstlBaseVo
                            VoUtil.CopyProperties(editInstlVo, editInstlBaseVo)
                            editInstlBaseDao.DeleteBy(editInstlBaseVo)

                            impl.UpdateInstlHinbanHyoujiJunBySekkeiBlockInstl(deleteInstl)
                            impl.UpdateInstlHinbanHyoujiJunByBuhinEditInstl(deleteInstl)
                            impl.UpdateBuhinNoHyoujiJunByBuhinEdit(deleteBuhin)
                            impl.UpdateBuhinNoHyoujiJunByBuhinEditInstl(deleteBuhin)

                        End If
                    End If
                Next
            Next
        End Sub


        Public Sub BuhinSortLevelZero()
            ''2015/08/06 変更 E.Ubukata

            For Each buhineditVo As TShisakuBuhinEditVo In impl.FindByEventCode(eventCode)
                Do
                    'LEVEL0の品番で表示順とインストール品番表示順が不一致の物を抽出(インストール品番表示順でソート）
                    Dim vos As List(Of TShisakuBuhinEditInstlVo) = impl.FindByLevelZero2(buhineditVo.ShisakuEventCode, buhineditVo.ShisakuBukaCode, buhineditVo.ShisakuBlockNo, buhineditVo.ShisakuBlockNoKaiteiNo)
                    '無ければ次ブロック
                    If vos.Count = 0 Then Exit Do

                    '処理対象はインストール品番の一番若いレコードのみ
                    '→入れ替え処理でその他の品番の表示順序が１段ずれるため入れ替え作業後再度検索し直し処理を行う
                    '→これによりレベル０の品番の員数を斜めに表示させる
                    Dim vo As TShisakuBuhinEditInstlVo = vos(0)

                    '表示順入れ替え処理（表示順をインストール品番表示順に合わせる）
                    impl.UpdateLevelZeroBuhinNoHyoujiJunByBuhinEdit(vo.ShisakuEventCode, vo.ShisakuBukaCode, vo.ShisakuBlockNo, vo.ShisakuBlockNoKaiteiNo, vo.BuhinNoHyoujiJun, vo.InstlHinbanHyoujiJun)
                    impl.UpdateLevelZeroBuhinNoHyoujiJunByBuhinEditBase(vo.ShisakuEventCode, vo.ShisakuBukaCode, vo.ShisakuBlockNo, vo.ShisakuBlockNoKaiteiNo, vo.BuhinNoHyoujiJun, vo.InstlHinbanHyoujiJun)
                    impl.UpdateLevelZeroBuhinNoHyoujiJunByBuhinEditInstl(vo.ShisakuEventCode, vo.ShisakuBukaCode, vo.ShisakuBlockNo, vo.ShisakuBlockNoKaiteiNo, vo.BuhinNoHyoujiJun, vo.InstlHinbanHyoujiJun)
                    impl.UpdateLevelZeroBuhinNoHyoujiJunByBuhinEditInstlBase(vo.ShisakuEventCode, vo.ShisakuBukaCode, vo.ShisakuBlockNo, vo.ShisakuBlockNoKaiteiNo, vo.BuhinNoHyoujiJun, vo.InstlHinbanHyoujiJun)
                Loop
            Next

        End Sub
        ''↑↑2014/09/18 1 ベース部品表作成表機能増強 酒井 ADD END
        '↓↓2014/10/23 酒井 ADD BEGIN
        Public Sub BuhinAssyuku()
            '同一ブロック内に、同一部品が複数存在するリストを抽出
            Dim Vos As List(Of Buhin4UpdateBuhinHyoujiJunVo) = impl.FindBuhin4UpdateBuhinHyoujiJun(eventCode)

            Dim beImpl As TShisakuBuhinEditDao = New TShisakuBuhinEditDaoImpl
            For Each Vo As Buhin4UpdateBuhinHyoujiJunVo In Vos
                'TShisakuBuhinEditから削除
                Dim beVo As New TShisakuBuhinEditVo
                VoUtil.CopyProperties(Vo, beVo)
                beImpl.DeleteBy(beVo)

                'TShisakuBuhinEditInstlの部品表示順を修正
                'TShisakuBuhinEditの該当部品以降の部品表示順を-1
                'TShisakuBuhinEditInstlの該当部品以降の部品表示順を-1
                impl.UpdateNewBuhinHyoujiJun(Vo)
            Next

            Dim VosBase As List(Of Buhin4UpdateBuhinHyoujiJunVo) = impl.FindBuhin4UpdateBuhinHyoujiJunBase(eventCode)

            Dim beImplBase As TShisakuBuhinEditBaseDao = New TShisakuBuhinEditBaseDaoImpl
            For Each VoBase As Buhin4UpdateBuhinHyoujiJunVo In VosBase
                'TShisakuBuhinEditから削除
                Dim beVoBase As New TShisakuBuhinEditBaseVo
                VoUtil.CopyProperties(VoBase, beVoBase)
                beImplBase.DeleteBy(beVoBase)

                'TShisakuBuhinEditInstlの部品表示順を修正
                'TShisakuBuhinEditの該当部品以降の部品表示順を-1
                'TShisakuBuhinEditInstlの該当部品以降の部品表示順を-1
                impl.UpdateNewBuhinHyoujiJunBase(VoBase)

            Next

        End Sub
        '↑↑2014/10/23 酒井 ADD END

        '↓↓2014/10/24 酒井 ADD BEGIN
        Public Sub UpdateBukaCodeFromEbom()
            Dim sbDao As SekkeiBlockDao = New SekkeiBlockDaoImpl
            Dim tsbDao As TShisakuSekkeiBlockDao = New TShisakuSekkeiBlockDaoImpl

            '最終設計課でないものを取得
            Dim sb4EbomVos As List(Of ShisakuSekkeiBlock4EbomVo) = sbDao.FindSekkeika4Update(eventCode)

            For Each sb4EbomVo As ShisakuSekkeiBlock4EbomVo In sb4EbomVos
                '********************************************************
                'TShisakuSekkeiBlock更新
                '********************************************************

                Dim sbVo1 As TShisakuSekkeiBlockVo = tsbDao.FindByPk(sb4EbomVo.ShisakuEventCode, sb4EbomVo.ShisakuBukaCodeNew, sb4EbomVo.ShisakuBlockNo, sb4EbomVo.ShisakuBlockNoKaiteiNo)
                If sbVo1 Is Nothing Then
                    '最終設計課レコードが存在しない場合、

                    '　挿入するBLOCK_NO_HYOUJI_JUNを取得
                    Dim shisakuBlockNoHyoujiJunNew As Integer
                    Dim sbvo2 As List(Of TShisakuSekkeiBlockVo) = sbDao.FindShisakuSekkeiBlockByShisakuBlockNoHyoujiJun(sb4EbomVo.ShisakuEventCode, sb4EbomVo.ShisakuBukaCodeNew, sb4EbomVo.ShisakuBlockNo, sb4EbomVo.ShisakuBlockNoKaiteiNo)
                    If sbvo2 Is Nothing Then
                        shisakuBlockNoHyoujiJunNew = 0
                    Else
                        shisakuBlockNoHyoujiJunNew = sbvo2.Count
                    End If

                    '　更新先のブロック表示順を下げて空ける
                    sbDao.UpdateShisakuSekkeiBlockByShisakuBlockNoHyoujiJun(sb4EbomVo.ShisakuEventCode, sb4EbomVo.ShisakuBlockNoKaiteiNo, sb4EbomVo.ShisakuBukaCodeNew, shisakuBlockNoHyoujiJunNew)

                    '　元レコードの設計課を更新
                    sbDao.UpdateShisakuSekkeiBlockByShisakuBukaCode(sb4EbomVo.ShisakuEventCode, sb4EbomVo.ShisakuBukaCode, sb4EbomVo.ShisakuBlockNo, sb4EbomVo.ShisakuBlockNoKaiteiNo, sb4EbomVo.ShisakuBukaCodeNew, shisakuBlockNoHyoujiJunNew)

                Else
                    '最終設計課レコードが存在する場合（試作イベントで同じブロックがあった）
                    '　元レコードを削除
                    tsbDao.DeleteByPk(sb4EbomVo.ShisakuEventCode, sb4EbomVo.ShisakuBukaCode, sb4EbomVo.ShisakuBlockNo, sb4EbomVo.ShisakuBlockNoKaiteiNo)
                End If

                '　更新元のブロック表示順を更新（上げて詰める）
                sbDao.UpdateShisakuSekkeiBlockByShisakuBlockNoHyoujiJun2(sb4EbomVo.ShisakuEventCode, sb4EbomVo.ShisakuBlockNoKaiteiNo, sb4EbomVo.ShisakuBukaCode, sb4EbomVo.ShisakuBlockNoHyoujiJun)

                '********************************************************
                'TShisakuSekkeiBlockInstl更新
                '********************************************************
                '更新先ブロック、部課のMAX INSTL表示順を取得
                Dim sbiVos2 As List(Of TShisakuSekkeiBlockInstlVo) = sbDao.FindShisakuSekkeiBlockInstlByInstlHinbanHyoujiJun(sb4EbomVo.ShisakuEventCode, sb4EbomVo.ShisakuBukaCodeNew, sb4EbomVo.ShisakuBlockNo, sb4EbomVo.ShisakuBlockNoKaiteiNo)
                Dim InstlHinbanHyoujiJunNew As Integer
                If sbiVos2 Is Nothing Then
                    InstlHinbanHyoujiJunNew = 0
                ElseIf sbiVos2.Count = 0 Then
                    InstlHinbanHyoujiJunNew = 0
                Else
                    InstlHinbanHyoujiJunNew = sbiVos2(0).InstlHinbanHyoujiJun + 1
                End If
                '部課コード、INSTL表示順を更新
                sbDao.UpdateShisakuSekkeiBlockInstlByInstlHinbanHyoujiJun(sb4EbomVo.ShisakuEventCode, sb4EbomVo.ShisakuBukaCode, sb4EbomVo.ShisakuBlockNo, sb4EbomVo.ShisakuBlockNoKaiteiNo, sb4EbomVo.ShisakuBukaCodeNew, InstlHinbanHyoujiJunNew)

                '********************************************************
                'TShisakuBuhinEdit更新
                '********************************************************
                '更新先ブロック、部課のMAX BUHIN表示順を取得
                Dim beVos As List(Of TShisakuBuhinEditVo) = sbDao.FindShisakuBuhinEditByBuhinNoHyoujiJun(sb4EbomVo.ShisakuEventCode, sb4EbomVo.ShisakuBukaCodeNew, sb4EbomVo.ShisakuBlockNo, sb4EbomVo.ShisakuBlockNoKaiteiNo)
                Dim BuhinNoHyoujiJunNew As Integer
                If beVos Is Nothing Then
                    BuhinNoHyoujiJunNew = 0
                ElseIf beVos.Count = 0 Then
                    BuhinNoHyoujiJunNew = 0
                Else
                    BuhinNoHyoujiJunNew = beVos(0).BuhinNoHyoujiJun + 1
                End If

                '部課コード、部品表示順を更新
                sbDao.UpdateShisakuBuhinEditByBuhinNoHyoujiJun(sb4EbomVo.ShisakuEventCode, sb4EbomVo.ShisakuBukaCode, sb4EbomVo.ShisakuBlockNo, sb4EbomVo.ShisakuBlockNoKaiteiNo, sb4EbomVo.ShisakuBukaCodeNew, BuhinNoHyoujiJunNew)

                '********************************************************
                'TShisakuBuhinEditInstl更新
                '********************************************************
                '部課コード、部品表示順、Instl表示順を更新
                sbDao.UpdateShisakuBuhinEditInstlByBuhinJunInstlJun(sb4EbomVo.ShisakuEventCode, sb4EbomVo.ShisakuBukaCode, sb4EbomVo.ShisakuBlockNo, sb4EbomVo.ShisakuBlockNoKaiteiNo, sb4EbomVo.ShisakuBukaCodeNew, BuhinNoHyoujiJunNew, InstlHinbanHyoujiJunNew)

            Next

            'base
            'Dim sbDao As SekkeiBlockDao = New SekkeiBlockDaoImpl
            'Dim tsbDao As TShisakuSekkeiBlockDao = New TShisakuSekkeiBlockDaoImpl

            '最終設計課でないものを取得
            'Dim sb4EbomVos As List(Of ShisakuSekkeiBlock4EbomVo) = sbDao.FindSekkeika4Update(eventCode)
            'sb4EbomVos = sbDao.FindSekkeika4Update(eventCode, True)

            For Each sb4EbomVo As ShisakuSekkeiBlock4EbomVo In sb4EbomVos
                '********************************************************
                'TShisakuSekkeiBlock更新
                '********************************************************

                'Dim sbVo1 As TShisakuSekkeiBlockVo = tsbDao.FindByPk(sb4EbomVo.ShisakuEventCode, sb4EbomVo.ShisakuBukaCodeNew, sb4EbomVo.ShisakuBlockNo, sb4EbomVo.ShisakuBlockNoKaiteiNo)
                'If sbVo1 Is Nothing Then
                '    '最終設計課レコードが存在しない場合、

                '    '　挿入するBLOCK_NO_HYOUJI_JUNを取得
                '    Dim shisakuBlockNoHyoujiJunNew As Integer
                '    Dim sbvo2 As List(Of TShisakuSekkeiBlockVo) = sbDao.FindShisakuSekkeiBlockByShisakuBlockNoHyoujiJun(sb4EbomVo.ShisakuEventCode, sb4EbomVo.ShisakuBukaCodeNew, sb4EbomVo.ShisakuBlockNo, sb4EbomVo.ShisakuBlockNoKaiteiNo)
                '    If sbvo2 Is Nothing Then
                '        shisakuBlockNoHyoujiJunNew = 0
                '    Else
                '        shisakuBlockNoHyoujiJunNew = sbvo2.Count
                '    End If

                '    '　更新先のブロック表示順を下げて空ける
                '    sbDao.UpdateShisakuSekkeiBlockByShisakuBlockNoHyoujiJun(sb4EbomVo.ShisakuEventCode, sb4EbomVo.ShisakuBlockNoKaiteiNo, sb4EbomVo.ShisakuBukaCodeNew, shisakuBlockNoHyoujiJunNew)

                '    '　元レコードの設計課を更新
                '    sbDao.UpdateShisakuSekkeiBlockByShisakuBukaCode(sb4EbomVo.ShisakuEventCode, sb4EbomVo.ShisakuBukaCode, sb4EbomVo.ShisakuBlockNo, sb4EbomVo.ShisakuBlockNoKaiteiNo, sb4EbomVo.ShisakuBukaCodeNew, shisakuBlockNoHyoujiJunNew)

                'Else
                '    '最終設計課レコードが存在する場合（試作イベントで同じブロックがあった）
                '    '　元レコードを削除
                '    tsbDao.DeleteByPk(sb4EbomVo.ShisakuEventCode, sb4EbomVo.ShisakuBukaCode, sb4EbomVo.ShisakuBlockNo, sb4EbomVo.ShisakuBlockNoKaiteiNo)
                'End If

                ''　更新元のブロック表示順を更新（上げて詰める）
                'sbDao.UpdateShisakuSekkeiBlockByShisakuBlockNoHyoujiJun2(sb4EbomVo.ShisakuEventCode, sb4EbomVo.ShisakuBlockNoKaiteiNo, sb4EbomVo.ShisakuBukaCode, sb4EbomVo.ShisakuBlockNoHyoujiJun)

                '********************************************************
                'TShisakuSekkeiBlockInstl更新
                '********************************************************
                '更新先ブロック、部課のMAX INSTL表示順を取得
                Dim sbiVos2 As List(Of TShisakuSekkeiBlockInstlVo) = sbDao.FindShisakuSekkeiBlockInstlByInstlHinbanHyoujiJun(sb4EbomVo.ShisakuEventCode, sb4EbomVo.ShisakuBukaCodeNew, sb4EbomVo.ShisakuBlockNo, "  0")
                Dim InstlHinbanHyoujiJunNew As Integer
                If sbiVos2 Is Nothing Then
                    InstlHinbanHyoujiJunNew = 0
                ElseIf sbiVos2.Count = 0 Then
                    InstlHinbanHyoujiJunNew = 0
                Else
                    InstlHinbanHyoujiJunNew = sbiVos2(0).InstlHinbanHyoujiJun + 1
                End If
                '部課コード、INSTL表示順を更新
                sbDao.UpdateShisakuSekkeiBlockInstlByInstlHinbanHyoujiJun(sb4EbomVo.ShisakuEventCode, sb4EbomVo.ShisakuBukaCode, sb4EbomVo.ShisakuBlockNo, "  0", sb4EbomVo.ShisakuBukaCodeNew, InstlHinbanHyoujiJunNew)

                '********************************************************
                'TShisakuBuhinEdit更新
                '********************************************************
                '更新先ブロック、部課のMAX BUHIN表示順を取得
                Dim beVos As List(Of TShisakuBuhinEditVo) = sbDao.FindShisakuBuhinEditByBuhinNoHyoujiJun(sb4EbomVo.ShisakuEventCode, sb4EbomVo.ShisakuBukaCodeNew, sb4EbomVo.ShisakuBlockNo, sb4EbomVo.ShisakuBlockNoKaiteiNo, True)
                Dim BuhinNoHyoujiJunNew As Integer
                If beVos Is Nothing Then
                    BuhinNoHyoujiJunNew = 0
                ElseIf beVos.Count = 0 Then
                    BuhinNoHyoujiJunNew = 0
                Else
                    BuhinNoHyoujiJunNew = beVos(0).BuhinNoHyoujiJun + 1
                End If

                '部課コード、部品表示順を更新
                sbDao.UpdateShisakuBuhinEditByBuhinNoHyoujiJun(sb4EbomVo.ShisakuEventCode, sb4EbomVo.ShisakuBukaCode, sb4EbomVo.ShisakuBlockNo, sb4EbomVo.ShisakuBlockNoKaiteiNo, sb4EbomVo.ShisakuBukaCodeNew, BuhinNoHyoujiJunNew, True)

                '********************************************************
                'TShisakuBuhinEditInstl更新
                '********************************************************
                '部課コード、部品表示順、Instl表示順を更新
                sbDao.UpdateShisakuBuhinEditInstlByBuhinJunInstlJun(sb4EbomVo.ShisakuEventCode, sb4EbomVo.ShisakuBukaCode, sb4EbomVo.ShisakuBlockNo, sb4EbomVo.ShisakuBlockNoKaiteiNo, sb4EbomVo.ShisakuBukaCodeNew, BuhinNoHyoujiJunNew, InstlHinbanHyoujiJunNew, True)

            Next

        End Sub
        '↑↑2014/10/24 酒井 ADD END
    End Class
End Namespace