Imports ShisakuCommon
Imports ShisakuCommon.Db.EBom.Dao
Imports EventSakusei.ShisakuBuhinMenu.Dao
Imports ShisakuCommon.Db.EBom.Vo.Helper
Imports ShisakuCommon.Util.Grouping
Imports ShisakuCommon.Db.EBom.Vo
Imports EventSakusei.ShisakuBuhinEdit.Kosei.Dao
Imports EventSakusei.ShisakuBuhinEdit.Logic.Detect
'Imports EventSakusei.ShisakuBuhinEditBlock.Dao

Namespace ShisakuBuhinMenu.Logic.Impl.Tenkai
    Public Class SekkeiBlockInstlSupplier
        Private ReadOnly shisakuEventCode As String
        Private ReadOnly UnitKbn As String
        Private ReadOnly dao As SekkeiBlockDao
        Private ReadOnly daoBase As ShisakuEventBaseDao     '2012/01/13

        Private ReadOnly makeDao As New MakeStructureResultDaoImpl

        '/*** 20140911 CHANGE START ***/
        Private kaihatsuFugo As Dictionary(Of String, String)
        Private unitKbnDictionary As Dictionary2(Of String, String, String)
        '/*** 20140911 CHANGE END ***/
        
        '/*** 20140911 CHANGE START（コンストラクタ引数追加） ***/
                Public Sub New(ByVal shisakuEventVo As TShisakuEventVo)
            'Me.New(shisakuEventVo.ShisakuEventCode, New SekkeiBlockDaoImpl, New ShisakuEventBaseDaoImpl, shisakuEventVo.UnitKbn)
            'Me.UnitKbn = shisakuEventVo.UnitKbn
            Me.New(shisakuEventVo.ShisakuEventCode,  New Dictionary2(Of String, String, String), New SekkeiBlockDaoImpl, New ShisakuEventBaseDaoImpl)
            Me.UnitKbn = shisakuEventVo.UnitKbn
        End Sub
        Public Sub New(ByVal shisakuEventCode As String, ByVal dao As SekkeiBlockDao, ByVal daoBase As ShisakuEventBaseDao, ByVal Unitkbn As String)
            'Me.shisakuEventCode = shisakuEventCode
            'Me.dao = dao
            'Me.daoBase = daoBase
            Me.New(shisakuEventCode, New Dictionary2(Of String, String, String), New SekkeiBlockDaoImpl, New ShisakuEventBaseDaoImpl)
        End Sub
        Public Sub New(ByVal shisakuEventVo As TShisakuEventVo, ByRef unitKbnDictionary As Dictionary2(Of String, String, String))
            Me.New(shisakuEventVo.ShisakuEventCode, unitKbnDictionary, New SekkeiBlockDaoImpl, New ShisakuEventBaseDaoImpl)
            Me.UnitKbn = shisakuEventVo.UnitKbn
        End Sub
        Public Sub New(ByVal shisakuEventCode As String, ByRef unitKbnDictionary As Dictionary2(Of String, String, String), ByVal dao As SekkeiBlockDao, ByVal daoBase As ShisakuEventBaseDao)
            Me.shisakuEventCode = shisakuEventCode
            Me.dao = dao
            Me.daoBase = daoBase

            Me.kaihatsuFugo = New Dictionary(Of String, String)
            Me.unitKbnDictionary = unitKbnDictionary
        End Sub

        ''' <summary>
        ''' 
        ''' </summary>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Function MakeRegisterValues() As List(Of TShisakuSekkeiBlockInstlVo)
            Dim alVosFinal As New List(Of SekkeiBlockAlResultVo)
            Dim alVosFinalwk As New List(Of SekkeiBlockAlResultVo)
            Dim alVosFinalOld As New List(Of SekkeiBlockAlResultVo)
            Dim alVosFinalOldwk As New List(Of SekkeiBlockAlResultVo)
            Dim alVosLast As New List(Of SekkeiBlockAlResultVo)
            Dim alVos As List(Of SekkeiBlockAlResultVo) = dao.FindAlByShisakuEventBaseByBlock(shisakuEventCode, "")
            Dim alVosOld As List(Of SekkeiBlockAlResultVo) = dao.FindAlByShisakuEventBaseOLDByBlock(shisakuEventCode, "")
            Dim alVos2 As List(Of SekkeiBlockAlResultVo) = dao.FindAlByShisakuEventBase2ByBlock(shisakuEventCode, "")
            Dim alVosOld2 As List(Of SekkeiBlockAlResultVo) = dao.FindAlByShisakuEventBaseOLD2ByBlock(shisakuEventCode, "")

            Dim wFlg As String = Nothing
            Dim BLOCKNO As String = Nothing
            Dim SHISAKUGOUSYA As String = Nothing
            Dim TOPCOLORKAITEINO As String = Nothing

            For Each group As SekkeiBlockAlResultVo In alVos2
                If Not StringUtil.IsEmpty(group.KumiawaseCodeSo) Then
                    alVosFinal.Add(group)
                End If
            Next
            For Each group As SekkeiBlockAlResultVo In alVos
                wFlg = Nothing
                For Each group2 As SekkeiBlockAlResultVo In alVosFinal
                    If group.BlockNo = group2.BlockNo AndAlso _
                       group.ShisakuGousya = group2.ShisakuGousya Then
                        wFlg = "NO"
                        Exit For
                    End If
                Next
                If wFlg <> "NO" Then

                    ' どの条件でも同じ処理のため、If文を削除
                    alVosFinalwk.Add(group)
                End If

                BLOCKNO = group.BlockNo
                SHISAKUGOUSYA = group.ShisakuGousya
                TOPCOLORKAITEINO = group.TopColorKaiteiNo
            Next
            If alVosFinalwk.Count > 0 Then
                alVosFinal.AddRange(alVosFinalwk)
            End If

            For Each group As SekkeiBlockAlResultVo In alVosOld2
                If Not StringUtil.IsEmpty(group.KumiawaseCodeSo) Then
                    alVosFinalOld.Add(group)
                End If
            Next

            For Each groupOLD As SekkeiBlockAlResultVo In alVosOld
                wFlg = Nothing
                For Each groupOLD2 As SekkeiBlockAlResultVo In alVosFinalOld
                    If groupOLD.BlockNo = groupOLD2.BlockNo AndAlso _
                       groupOLD.ShisakuGousya = groupOLD2.ShisakuGousya Then
                        wFlg = "NO"
                        Exit For
                    End If
                Next
                If wFlg <> "NO" Then
                    If BLOCKNO = groupOLD.BlockNo AndAlso _
                        SHISAKUGOUSYA = groupOLD.ShisakuGousya Then
                        If StringUtil.IsEmpty(TOPCOLORKAITEINO) AndAlso _
                            StringUtil.IsEmpty(groupOLD.TopColorKaiteiNo) Then
                            alVosFinalOldwk.Add(groupOLD)
                        ElseIf Not StringUtil.IsEmpty(TOPCOLORKAITEINO) AndAlso _
                                Not StringUtil.IsEmpty(groupOLD.TopColorKaiteiNo) Then
                            alVosFinalOldwk.Add(groupOLD)
                        End If
                    Else
                        alVosFinalOldwk.Add(groupOLD)
                    End If
                End If

                BLOCKNO = groupOLD.BlockNo
                SHISAKUGOUSYA = groupOLD.ShisakuGousya
                TOPCOLORKAITEINO = groupOLD.TopColorKaiteiNo
            Next
            If alVosFinalOldwk.Count > 0 Then
                alVosFinalOld.AddRange(alVosFinalOldwk)
            End If

            For Each groupOLD As SekkeiBlockAlResultVo In alVosFinalOld
                wFlg = Nothing
                For Each group As SekkeiBlockAlResultVo In alVosFinal
                    If groupOLD.BlockNo = group.BlockNo AndAlso _
                       groupOLD.ShisakuGousya = group.ShisakuGousya Then
                        wFlg = "NO"
                        Exit For
                    End If
                Next
                If wFlg <> "NO" Then
                    alVosLast.Add(groupOLD)
                End If
            Next
            If alVosLast.Count > 0 Then
                alVosFinal.AddRange(alVosLast)
            End If

            Dim instlVos As List(Of SekkeiBlockInstlResultVo) = dao.FindShisakuBlockInstlByShisakuEventBaseByBlock(shisakuEventCode, UnitKbn, "")

            Dim groupedVos As List(Of BukaCodeBlockNoVo) = GroupBukaCodeBlockNos(alVosFinal, instlVos)

            Dim results As New List(Of TShisakuSekkeiBlockInstlVo)
            For Each group As BukaCodeBlockNoVo In groupedVos
                Dim vo As New TShisakuSekkeiBlockInstlVo
                ''色付き品番の場合、構成マスタに色付きで登録されているとは限らない。
                '' 例）　****AV　⇒　****##
                ''　構成マスタ（552⇒553⇒551の順番で）を色付き品番で検索し、
                ''　該当データが無ければ##品番に置き換えて以下のロジックから使用する。

                '＃＃に変換して落とすとINSTLの0レベルが壊れるのでそのまま落とす。
                vo.BfBuhinNo = group.BfBuhinNo

                vo.ShisakuEventCode = shisakuEventCode
                vo.ShisakuBukaCode = group.BukaCode
                vo.ShisakuBlockNo = group.BlockNo
                vo.ShisakuBlockNoKaiteiNo = TShisakuSekkeiBlockInstlVoHelper.ShisakuBlockNoKaiteiNo.DEFAULT_VALUE
                vo.ShisakuGousya = group.Gosha
                vo.InstlHinbanHyoujiJun = group.HyoujiJun
                vo.InstlHinban = group.FfBuhinNo
                vo.InstlHinbanKbn = group.InstlHinbanKbn
                vo.InstlDataKbn = group.InstlDataKbn
                'vo.BfBuhinNo = group.BfBuhinNo
                vo.InsuSuryo = 1
                vo.SaisyuKoushinbi = Nothing
                vo.BaseInstlHinbanHyoujiJun = group.BaseHyoujiJun
                results.Add(vo)
            Next



            Return results
        End Function

        ''' <summary>
        ''' 品番を##品番にして返す
        ''' </summary>
        ''' <param name="instlHinban">品番</param>
        ''' <returns>##品番</returns>
        ''' <remarks></remarks>
        Private Function Conv0532HinbanByRule(ByVal instlHinban As String) As String

            If instlHinban Is Nothing OrElse instlHinban.Length < 12 _
                OrElse instlHinban.Length >= 13 Then
                Return instlHinban
            End If
            Return instlHinban.Substring(0, 10) & "##" & instlHinban.Substring(12)
        End Function

        ''' <summary>
        ''' 試作設計ブロックINSTL情報へ登録する
        ''' </summary>
        ''' <param name="login">ログイン情報</param>
        ''' <param name="instlDao">試作設計ブロックINSTL情報Dao</param>
        ''' <param name="aShisakuDate">試作システム日付</param>
        ''' <remarks></remarks>
        Public Sub Register(ByVal login As LoginInfo, _
                            ByVal instlDao As TShisakuSekkeiBlockInstlDao, _
                            ByVal aShisakuDate As ShisakuDate)

            Dim registeredDataList As List(Of TShisakuSekkeiBlockInstlVo) = New List(Of TShisakuSekkeiBlockInstlVo)
            Dim vos As List(Of TShisakuSekkeiBlockInstlVo) = MakeRegisterValues()

            For Each vo As TShisakuSekkeiBlockInstlVo In vos

                If Not CheckChofuku(vo, registeredDataList) Then
                    vo.CreatedUserId = login.UserId
                    vo.CreatedDate = aShisakuDate.CurrentDateDbFormat
                    vo.CreatedTime = aShisakuDate.CurrentTimeDbFormat
                    vo.UpdatedUserId = login.UserId
                    vo.UpdatedDate = aShisakuDate.CurrentDateDbFormat
                    vo.UpdatedTime = aShisakuDate.CurrentTimeDbFormat
                    vo.BaseInstlFlg = 1

                    ''2015/07/09 エラー虫ロジック
                    Try
                        instlDao.InsertBy(vo)
                        'ベース用に追加'
                        vo.ShisakuBlockNoKaiteiNo = "  0"
                        instlDao.InsertBy(vo)
                    Catch ex As Exception

                    End Try



                    ' リストに追加
                    registeredDataList.Add(vo)
                End If

            Next
            registeredDataList = Nothing
        End Sub
        
        
        Private Function CheckChofuku(ByVal vo As TShisakuSekkeiBlockInstlVo, ByRef registeredDataList As List(Of TShisakuSekkeiBlockInstlVo)) As Boolean
            For Each registeredData As TShisakuSekkeiBlockInstlVo In registeredDataList

                If vo.ShisakuBukaCode.Equals(registeredData.ShisakuBukaCode) _
                    AndAlso vo.ShisakuBlockNo.Equals(registeredData.ShisakuBlockNo) _
                    AndAlso vo.ShisakuBlockNoKaiteiNo.Equals(registeredData.ShisakuBlockNoKaiteiNo) _
                    AndAlso vo.ShisakuGousya.Equals(registeredData.ShisakuGousya) _
                    AndAlso vo.InstlDataKbn.Equals(registeredData.InstlDataKbn) _
                    AndAlso vo.InstlHinban.Equals(registeredData.InstlHinban) _
                    AndAlso vo.InstlHinbanKbn.Equals(registeredData.InstlHinbanKbn) Then
                    'If vo.ShisakuBlockNo.Equals(registeredData.ShisakuBlockNo) _
                    '    AndAlso vo.ShisakuBlockNoKaiteiNo.Equals(registeredData.ShisakuBlockNoKaiteiNo) _
                    '    AndAlso vo.ShisakuGousya.Equals(registeredData.ShisakuGousya) _
                    '    AndAlso vo.InstlDataKbn.Equals(registeredData.InstlDataKbn) _
                    '    AndAlso vo.InstlHinban.Equals(registeredData.InstlHinban) _
                    '    AndAlso vo.InstlHinbanKbn.Equals(registeredData.InstlHinbanKbn) Then

                    ' データがあった場合
                    Return True
                End If

            Next

            ' データがなかった場合
            Return False

        End Function

        Private Class RecordGroupingRule : Implements VoGroupingRule(Of BukaCodeBlockNoVo)
            Public Sub GroupRule(ByVal groupBy As VoGroupingLocator, ByVal vo As BukaCodeBlockNoVo) Implements VoGroupingRule(Of BukaCodeBlockNoVo).GroupRule
                ''2015/09/01 追加 E.Ubukata Ver2.11.0
                '' 移管車改修のイベントで別の列に定義されているものが同一に扱われてしまうため追加
                'groupBy.Prop(vo.BukaCode).Prop(vo.BlockNo).Prop(vo.Gosha).Prop(vo.FfBuhinNo).Prop(vo.InstlHinbanKbn).Prop(vo.BfBuhinNo).Prop(vo.InstlDataKbn)
                groupBy.Prop(vo.BukaCode).Prop(vo.BlockNo).Prop(vo.Gosha).Prop(vo.FfBuhinNo).Prop(vo.InstlHinbanKbn).Prop(vo.BfBuhinNo).Prop(vo.InstlDataKbn).Prop(vo.BaseHyoujiJun)
            End Sub
        End Class

        Private Class BukaBlockComparer : Implements IComparer(Of BukaCodeBlockNoVo)
            Public Function Compare(ByVal x As BukaCodeBlockNoVo, ByVal y As BukaCodeBlockNoVo) As Integer Implements IComparer(Of BukaCodeBlockNoVo).Compare
                Dim resultA As Integer = x.BukaCode.CompareTo(y.BukaCode)
                If resultA <> 0 Then
                    Return resultA
                End If
                Dim resultB As Integer = x.BlockNo.CompareTo(y.BlockNo)
                If resultB <> 0 Then
                    Return resultB
                End If
                '↓↓2014/10/10 酒井 ADD BEGIN
                Dim resultF As Integer = x.InstlDataKbn.CompareTo(y.InstlDataKbn)
                If resultF <> 0 Then
                    Return resultF
                End If
                '↑↑2014/10/10 酒井 ADD END
                Dim resultC As Integer = x.FfBuhinNo.CompareTo(y.FfBuhinNo)
                If resultC <> 0 Then
                    Return resultC
                End If
                Dim resultD As Integer = x.InstlHinbanKbn.CompareTo(y.InstlHinbanKbn)
                If resultD <> 0 Then
                    Return resultD
                End If
                'Nothingならブランクに変換
                If StringUtil.IsEmpty(x.BfBuhinNo) Then
                    x.BfBuhinNo = ""
                End If
                If StringUtil.IsEmpty(y.BfBuhinNo) Then
                    y.BfBuhinNo = ""
                End If
                Dim resultE As Integer = x.BfBuhinNo.CompareTo(y.BfBuhinNo)
                If resultE <> 0 Then
                    Return resultE
                End If

                Return x.Gosha.CompareTo(y.Gosha)
            End Function
        End Class

        ''' <summary>
        ''' 設計課とブロックNo・号車・付加F品番でグループ化した情報を返す
        ''' </summary>
        ''' <param name="alVos">A/Lの素の一覧</param>
        ''' <param name="instlVos">試作設計ブロックINSTL情報の一覧</param>
        ''' <returns>グループ化した情報</returns>
        ''' <remarks>表示順は、設計課＆ブロックNoごと</remarks>
        Public Function GroupBukaCodeBlockNos(ByVal alVos As List(Of SekkeiBlockAlResultVo), _
                                              ByVal instlVos As List(Of SekkeiBlockInstlResultVo) _
                                              ) As List(Of BukaCodeBlockNoVo)

            Dim packedVo As New List(Of BukaCodeBlockNoVo)
            Dim MUnitKbn As New Rhac0080Vo
            Dim tmpUnitKbn As String
            For Each alVo As SekkeiBlockAlResultVo In alVos
                Dim baseVo As New TShisakuEventBaseVo
                Dim shisakuKaihatsuFugo As String

                If kaihatsuFugo.ContainsKey(alVo.ShisakuGousya) Then
                    shisakuKaihatsuFugo = kaihatsuFugo(alVo.ShisakuGousya)
                Else
                    '2012/02/16 号車ごとの開発符号を取得
                    baseVo = daoBase.FindShisakuEventBaseByEventCodeAndGousyaForShisakuGousya(shisakuEventCode, alVo.ShisakuGousya)
                    If baseVo Is Nothing Then
                        baseVo = daoBase.FindShisakuEventBaseByEventCodeAndGousyaForShisakuBaseGousya(shisakuEventCode, alVo.ShisakuGousya)
                    End If

                    kaihatsuFugo.Add(alVo.ShisakuGousya, baseVo.BaseKaihatsuFugo)
                    shisakuKaihatsuFugo = baseVo.BaseKaihatsuFugo
                End If


                'マスターからブロックに該当するユニット区分を取得する。
                If unitKbnDictionary.ContainsKeys(alVo.BlockNo, shisakuKaihatsuFugo) Then
                    tmpUnitKbn = unitKbnDictionary.item(alVo.BlockNo)(shisakuKaihatsuFugo)
                Else
                    'マスターからブロックに該当するユニット区分を取得する。
                    MUnitKbn = dao.FindShisakuBlockUnit(alVo.BlockNo, shisakuKaihatsuFugo)

                    If alVo.BlockNo = "604A" Then
                        tmpUnitKbn = "M"
                    Else
                        tmpUnitKbn = MUnitKbn.MtKbn
                    End If

                    unitKbnDictionary.Put(alVo.BlockNo, shisakuKaihatsuFugo, tmpUnitKbn)
                End If

                '/*** 20140911 CHANGE END ***/
                'イベント情報のユニット区分に該当するデータのみ処理する。
                ' S:全て対象
                ' M:M、ブランクのみ対象
                ' T:T、ブランクのみ対象
                If UnitKbn <> "S" Then
                    '/*** 20140911 CHANGE START ***/
                    'If UnitKbn <> MUnitKbn.MtKbn And Not StringUtil.IsEmpty(MUnitKbn.MtKbn) Then
                    If UnitKbn <> tmpUnitKbn AndAlso Not StringUtil.IsEmpty(tmpUnitKbn) Then
                        '/*** 20140911 CHANGE END ***/
                        '条件に該当しないので次のデータへ
                        Continue For
                    End If
                End If
                'データ登録
                Rhac1500VoHelper.ResolveRule(alVo)
                Dim vo As New BukaCodeBlockNoVo

                'マスタに無い場合、課略名をセットします。　2011/2/3 by柳沼
                If StringUtil.IsEmpty(alVo.BuCode & alVo.KaCode) Then
                    vo.BukaCode = alVo.BukaCode  '課略名
                Else
                    '------------------------------------------------------------------
                    '２次改修
                    '　部課コードの問題について
                    '   2012/07/30 部課略名で更新してみる。
                    'vo.BukaCode = alVo.BuCode & alVo.KaCode  '部コードと課コード
                    vo.BukaCode = alVo.BukaCode  '課略名
                    '------------------------------------------------------------------
                End If

                vo.BlockNo = alVo.BlockNo
                vo.Gosha = alVo.ShisakuGousya
                vo.FfBuhinNo = alVo.FfBuhinNo
                vo.InstlHinbanKbn = String.Empty
                vo.InstlDataKbn = "0"
                vo.BfBuhinNo = StringUtil.Nvl(alVo.BfBuhinNo)
                packedVo.Add(vo)
            Next

            Dim backBlockNo As String = ""
            Dim backInstlHinban As String = ""
            Dim backInstlHinbanHyuojiJun As Integer = 0
            Dim backShisakuEventCode As String = ""
            Dim i As Integer = 0

            For Each instlVo As SekkeiBlockInstlResultVo In instlVos
                If Not backInstlHinban = instlVo.InstlHinban Then
                    i = 1
                Else
                    '過去（別イベント）の表示順で比較しても意味がないため
                    'If Not backInstlHinbanHyuojiJun = instlVo.InstlHinbanHyoujiJun Then
                    '    i = i + 1
                    'End If

                    '2015/08/07 変更 E.Ubukata
                    'If Not (backShisakuEventCode = instlVo.ShisakuEventCode) Then
                    If Not (backShisakuEventCode = instlVo.ShisakuEventCode And backInstlHinbanHyuojiJun = instlVo.InstlHinbanHyoujiJun) Then
                        i = i + 1
                    End If
                End If
                backBlockNo = instlVo.ShisakuBlockNo
                backInstlHinban = instlVo.InstlHinban
                backInstlHinbanHyuojiJun = instlVo.InstlHinbanHyoujiJun
                backShisakuEventCode = instlVo.ShisakuEventCode


                Dim vo As New BukaCodeBlockNoVo
                vo.BukaCode = instlVo.ShisakuBukaCode
                vo.BlockNo = instlVo.ShisakuBlockNo
                vo.Gosha = instlVo.BaseShisakuGousya
                vo.FfBuhinNo = instlVo.InstlHinban
                vo.InstlHinbanKbn = StringUtil.Nvl(instlVo.InstlHinbanKbn)
                vo.InstlDataKbn = i
                vo.BfBuhinNo = instlVo.BfBuhinNo
                vo.BaseHyoujiJun = instlVo.InstlHinbanHyoujiJun
                packedVo.Add(vo)

            Next

            Dim groupInstl As New VoGrouping(Of BukaCodeBlockNoVo)(New RecordGroupingRule)

            Dim results As List(Of BukaCodeBlockNoVo) = groupInstl.Grouping(packedVo)
            results.Sort(New BukaBlockComparer)

            Dim backBukaCode As String = String.Empty
            Dim bukaCode As String = String.Empty
            backBlockNo = String.Empty
            Dim hinban As String = String.Empty
            Dim hinbanKbn As String = String.Empty
            Dim dataKbn As String = String.Empty
            Dim hyoujiJun As Integer

            Dim daoSekkeika As ShisakuBlockSekkeikaTmpDao = New ShisakuBlockSekkeikaTmpDaoImpl
            Dim hasSekkeika As New Hashtable
            For Each vo As ShisakuBlockSekkeikaTmp4EbomVo In daoSekkeika.FindByAll4Ebom(shisakuEventCode)
                If Not hasSekkeika.Contains(vo.ShisakuBlockNo) Then
                    hasSekkeika.Add(vo.ShisakuBlockNo, vo)
                End If
            Next


            For Each vo As BukaCodeBlockNoVo In results
                'If vo.BukaCode.Equals(backBukaCode) AndAlso vo.BlockNo.Equals(backBlockNo) Then
                If vo.BlockNo.Equals(backBlockNo) Then
                    If Not (EzUtil.IsEqualIfNull(hinban, vo.FfBuhinNo) AndAlso EzUtil.IsEqualIfNull(hinbanKbn, vo.InstlHinbanKbn) AndAlso EzUtil.IsEqualIfNull(dataKbn, vo.InstlDataKbn)) Then
                        hyoujiJun += 1
                        hinban = vo.FfBuhinNo
                        hinbanKbn = vo.InstlHinbanKbn
                        dataKbn = vo.InstlDataKbn
                    End If
                Else
                    hyoujiJun = TShisakuSekkeiBlockInstlVoHelper.InstlHinbanHyoujiJun.START_VALUE
                    backBukaCode = vo.BukaCode
                    backBlockNo = vo.BlockNo
                    hinban = vo.FfBuhinNo
                    hinbanKbn = vo.InstlHinbanKbn
                    dataKbn = vo.InstlDataKbn
                    If hasSekkeika.Contains(vo.BlockNo) Then
                        bukaCode = CType(hasSekkeika.Item(vo.BlockNo), ShisakuBlockSekkeikaTmp4EbomVo).ShisakuBukaCode
                    Else
                        bukaCode = vo.BukaCode
                    End If
                End If
                vo.BukaCode = bukaCode
                vo.HyoujiJun = hyoujiJun
            Next
            Return results
        End Function

        ''' <summary>
        ''' ブロックNoに対する名称をもつDictionaryを返す
        ''' </summary>
        ''' <param name="alVos">A/Lの素の一覧</param>
        ''' <param name="instlVos">試作設計ブロックINSTL情報の一覧</param>
        ''' <returns>ブロック名称をもつDictionary</returns>
        ''' <remarks></remarks>
        Public Function ExtractBlockNames(ByVal alVos As List(Of SekkeiBlockAlResultVo), ByVal instlVos As List(Of SekkeiBlockInstlResultVo)) As Dictionary(Of String, String)

            Dim blockNames As New Dictionary(Of String, String)
            For Each vo As SekkeiBlockAlResultVo In alVos
                If blockNames.ContainsKey(vo.BlockNo) Then
                    Continue For
                End If
                blockNames.Add(vo.BlockNo, vo.BlockName)
            Next
            For Each vo As SekkeiBlockInstlResultVo In instlVos
                If blockNames.ContainsKey(vo.ShisakuBlockNo) Then
                    Continue For
                End If
                blockNames.Add(vo.ShisakuBlockNo, vo.ShisakuBlockName)
            Next
            Return blockNames
        End Function




    End Class
End Namespace