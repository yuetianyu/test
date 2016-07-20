Imports ShisakuCommon.Db.EBom.Vo.Helper
Imports ShisakuCommon
Imports ShisakuCommon.Db.EBom.Dao
Imports EventSakusei.ShisakuBuhinMenu.Dao
Imports ShisakuCommon.Util.Grouping
Imports ShisakuCommon.Db.EBom.Vo
'Imports EventSakusei.ShisakuBuhinEditBlock.Dao
'↓↓2014/10/24 酒井 ADD BEGIN
Imports ShisakuCommon.Db.EBom.Dao.Impl
'↑↑2014/10/24 酒井 ADD END

Namespace ShisakuBuhinMenu.Logic.Impl.Tenkai
    Public Class SekkeiBlockSupplier
        Private ReadOnly shisakuEventCode As String
        '↓↓2014/10/21 酒井 ADD BEGIN
        Private ReadOnly shisakuKaihatsuFugo As String
        '↑↑2014/10/21 酒井 ADD END
        Private ReadOnly unitKbn As String
        Private ReadOnly dao As SekkeiBlockDao
        Private ReadOnly daoBase As ShisakuEventBaseDao     '2012/01/13
        
        '/*** 20140911 CHANGE START ***/
        Private kaihatsuFugo As Dictionary(Of String, String)
        Private unitKbnDictionary As Dictionary2(Of String, String, String)
        '/*** 20140911 CHANGE END ***/

        '/*** 20140911 CHANGE START（コンストラクタ引数追加） ***/
        Public Sub New(ByVal shisakuEventVo As TShisakuEventVo)
        '     Me.New(shisakuEventVo, New SekkeiBlockDaoImpl, New ShisakuEventBaseDaoImpl)
            Me.New(shisakuEventVo, New Dictionary2(Of String, String, String), New SekkeiBlockDaoImpl, New ShisakuEventBaseDaoImpl)
        End Sub
        Public Sub New(ByVal shisakuEventVo As TShisakuEventVo, _
                ByVal dao As SekkeiBlockDao, ByVal daoBase As ShisakuEventBaseDao)
            '     Me.shisakuEventCode = shisakuEventVo.ShisakuEventCode
            '     Me.unitKbn = shisakuEventVo.UnitKbn
            '     Me.dao = dao
            '     Me.daoBase = daoBase
            Me.New(shisakuEventVo, New Dictionary2(Of String, String, String), dao, daoBase)

        End Sub
        Public Sub New(ByVal shisakuEventVo As TShisakuEventVo, ByRef unitKbnDictionary As Dictionary2(Of String, String, String))
            Me.New(shisakuEventVo, unitKbnDictionary, New SekkeiBlockDaoImpl, New ShisakuEventBaseDaoImpl)
        End Sub
        Public Sub New(ByVal shisakuEventVo As TShisakuEventVo, _
                       ByRef unitKbnDictionary As Dictionary2(Of String, String, String), _
                       ByVal dao As SekkeiBlockDao, ByVal daoBase As ShisakuEventBaseDao)
            Me.shisakuEventCode = shisakuEventVo.ShisakuEventCode
            Me.shisakuKaihatsuFugo = shisakuEventVo.ShisakuKaihatsuFugo
            Me.unitKbn = shisakuEventVo.UnitKbn
            Me.dao = dao
            Me.daoBase = daoBase

            Me.kaihatsuFugo = New Dictionary(Of String, String)
            Me.unitKbnDictionary = unitKbnDictionary
        End Sub

        Public Function MakeRegisterValues(ByVal baseVo As TShisakuEventBaseVo, Optional ByVal flg As Boolean = False) As List(Of TShisakuSekkeiBlockVo)
            Me.kaihatsuFugo.Clear()
            Me.unitKbnDictionary.Clear()

            Dim shisakuKaihatsuFugoGosha As String = ""       '号車の開発符号
            Dim HyojijunNo = baseVo.HyojijunNo      '号車の表示順
            Dim baseVo1 As New TShisakuEventBaseVo
            Dim isEventGosya As Boolean = False
            Dim shisakuEventCodeFromEvent As String = "" 'イベントから号車を選択された場合の対象号車のイベントコード


            '-------------------------------------------------------
            '２次改修
            '   2012/08/21
            '   baseVo.BaseKaihatsuFugoはNothingの他にブランクの時もある。
            '       その場合、ベース情報を試作イベントコード、号車にした場合、
            '       スルーされてデータが作成されされずスルーする。
            '       Nothingの場合、ブランクにして（ブランクで統一）スルーしないようにしてみた。
            If baseVo.BaseKaihatsuFugo Is Nothing Then
                baseVo.BaseKaihatsuFugo = ""
            End If


            Dim wFlg As String = Nothing
            Dim BLOCKNO As String = Nothing
            Dim SHISAKUGOUSYA As String = Nothing
            Dim TOPCOLORKAITEINO As String = Nothing

            Dim alVosFinal As New List(Of SekkeiBlockAlResultVo)
            Dim alVosFinalwk As New List(Of SekkeiBlockAlResultVo)
            Dim alVosFinalOld As New List(Of SekkeiBlockAlResultVo)
            Dim alVosFinalOldwk As New List(Of SekkeiBlockAlResultVo)
            Dim alVosLast As New List(Of SekkeiBlockAlResultVo)

            Dim alVos As New List(Of SekkeiBlockAlResultVo)
            Dim alVosOld As New List(Of SekkeiBlockAlResultVo)
            Dim alVos2 As New List(Of SekkeiBlockAlResultVo)
            Dim alVosOld2 As New List(Of SekkeiBlockAlResultVo)
            Dim instlVos As New List(Of SekkeiBlockInstlResultVo)


            If baseVo.ShisakuBaseEventCode <> "" And baseVo.ShisakuBaseGousya <> "" Then

            End If

            If Not baseVo.ShisakuBaseEventCode = "" Then

                isEventGosya = True
                instlVos = dao.FindShisakuBlockInstlByShisakuEventBase(shisakuEventCode, unitKbn, HyojijunNo, flg)

                If flg Then
                    '設計課更新画面からの呼出しの場合、最新設計課に置き換える
                    Dim r0080Vo As Rhac0080Vo
                    Dim SekkeiBlockDao As New SekkeiBlockDaoImpl

                    For Each instlVo As SekkeiBlockInstlResultVo In instlVos
                        '今回イベントの開発符号を優先して、EBOM設計課を取得する。
                        r0080Vo = SekkeiBlockDao.FindTantoBushoByBlock(instlVo.ShisakuKaihatsuFugo, instlVo.ShisakuBlockNo)
                        If r0080Vo Is Nothing Then
                            '存在しない場合、ベース車の開発符号で取得する。
                            Dim tmpVos As List(Of TShisakuEventBaseVo)
                            tmpVos = SekkeiBlockDao.FindByShisakuEventBase(shisakuEventCode)
                            For Each tmpVo In tmpVos
                                If Not StringUtil.IsEmpty(tmpVo.BaseKaihatsuFugo) Then
                                    r0080Vo = SekkeiBlockDao.FindTantoBushoByBlock(tmpVo.BaseKaihatsuFugo, instlVo.ShisakuBlockNo)
                                    If Not r0080Vo Is Nothing Then
                                        Exit For
                                    End If
                                End If
                            Next
                        End If
                        If Not r0080Vo Is Nothing Then
                            instlVo.ShisakuBukaCode = r0080Vo.TantoBusho
                        End If
                    Next
                End If
                shisakuEventCodeFromEvent = shisakuEventCode
            End If

            If Not baseVo.BaseKaihatsuFugo = "" Then
                baseVo1 = daoBase.FindShisakuEventBaseByEventCodeAndGousyaForShisakuGousya(shisakuEventCode, baseVo.ShisakuGousya)
                shisakuKaihatsuFugoGosha = baseVo1.BaseKaihatsuFugo

                alVos = dao.FindAlByShisakuEventBaseNoBuhin(shisakuEventCode, HyojijunNo)
                alVosOld = dao.FindAlByShisakuEventBaseOLDNoBuhin(shisakuEventCode, HyojijunNo)
                alVos2 = dao.FindAlByShisakuEventBase2NoBuhin(shisakuEventCode, HyojijunNo)
                alVosOld2 = dao.FindAlByShisakuEventBaseOLD2NoBuhin(shisakuEventCode, HyojijunNo)
                '/*** 20140911 CHANGE END ***/
            Else

            End If

            '組み合わせを持つものをループしてKumiawaseCodeSoがあるものをalVosFinalに追加
            For Each group As SekkeiBlockAlResultVo In alVos2
                If Not StringUtil.IsEmpty(group.KumiawaseCodeSo) Then
                    alVosFinal.Add(group)
                End If
            Next
            '組み合わせが無いものをループ
            For Each group As SekkeiBlockAlResultVo In alVos
                wFlg = Nothing
                'alVosFinalでループ
                For Each group2 As SekkeiBlockAlResultVo In alVosFinal
                    '組み合わせ無しのループのブロックNo＝alVosFinalのループのブロックNoの場合
                    '組み合わせ無しの号車をalVosFinal（組み合わせ持つデータ）の号車に差し替え
                    'wFlg＝NOにし、ループを抜ける
                    If group.BlockNo = group2.BlockNo AndAlso _
                       group.ShisakuGousya = group2.ShisakuGousya Then
                        wFlg = "NO"
                        Exit For
                    End If
                Next
                'wFlgがNOで無い場合
                If wFlg <> "NO" Then
                    '（前回ループの？）ブロックNo＝今回のループのブロックNoかつ
                    '（前回ループの？）号車＝今回のループの号車の場合
                    If BLOCKNO = group.BlockNo AndAlso _
                        SHISAKUGOUSYA = group.ShisakuGousya Then
                        '（前回ループの？）TOPCOLORKAITEINOがエンプティでなくかつ
                        '今回ループのTopColorKaiteiNoがエンプティで無い場合
                        If StringUtil.IsEmpty(TOPCOLORKAITEINO) AndAlso _
                            StringUtil.IsEmpty(group.TopColorKaiteiNo) Then
                            alVosFinalwk.Add(group)
                        ElseIf Not StringUtil.IsEmpty(TOPCOLORKAITEINO) AndAlso _
                                Not StringUtil.IsEmpty(group.TopColorKaiteiNo) Then
                            alVosFinalwk.Add(group)
                        End If
                    Else
                        '（前回ループの？）ブロックNo＝今回のループのブロックNoの場合
                        'alVosFinalwkにgroupをAdd
                        alVosFinalwk.Add(group)
                    End If
                End If

                BLOCKNO = group.BlockNo
                SHISAKUGOUSYA = group.ShisakuGousya
                TOPCOLORKAITEINO = group.TopColorKaiteiNo
            Next


            'alVosFinalwkがあった場合、alVosFinalに追加
            If alVosFinalwk.Count > 0 Then
                alVosFinal.AddRange(alVosFinalwk)
            End If

            '以下は仕様書No-1のデータに対して上と同じ処理を行う。
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

            Dim blockNames As Dictionary(Of String, String) = ExtractBlockNames(alVosFinal, instlVos)
            Dim blockNos As List(Of BukaCodeBlockNoVo) = GroupBukaCodeBlockNos(alVosFinal, instlVos, flg)

            Dim results As New List(Of TShisakuSekkeiBlockVo)

            For Each instl As BukaCodeBlockNoVo In blockNos

                Dim vo As New TShisakuSekkeiBlockVo

                vo.ShisakuEventCode = shisakuEventCode
                vo.ShisakuBukaCode = instl.BukaCode
                vo.ShisakuBlockNoHyoujiJun = instl.HyoujiJun
                vo.ShisakuBlockNo = instl.BlockNo
                vo.ShisakuBlockNoKaiteiNo = TShisakuSekkeiBlockVoHelper.ShisakuBlockNoKaiteiNo.DEFAULT_VALUE
                vo.BlockFuyou = TShisakuSekkeiBlockVoHelper.BlockFuyou.NECESSARY
                vo.Jyoutai = String.Empty

                '1 ベース部品表作成　機能増強 2014/07/07 ※既存ソースに対して修正を加える。
                'イベントからの号車の場合

                '2014/07/29 TSUNODA EDIT START

                'MUnitKbn自体に戻り値が得られない場合がある為、戻り値に対する
                'Nothing時の判定を追加及び、ルーチンの簡略化を行った

                vo.UnitKbn = ""

                If Not isEventGosya Then

                    '/*** 20140911 CHANGE START ***/
                    'マスターからブロックに該当するユニット区分を取得する。
                    'Dim MUnitKbn As Rhac0080Vo = dao.FindShisakuBlockUnit(instl.BlockNo, shisakuKaihatsuFugo)
                    If unitKbnDictionary.ContainsKeys(instl.BlockNo, shisakuKaihatsuFugoGosha) Then
                        vo.UnitKbn = unitKbnDictionary.item(instl.BlockNo)(shisakuKaihatsuFugoGosha)
                    Else
                        'マスターからブロックに該当するユニット区分を取得する。

                        Dim MUnitKbn As Rhac0080Vo = dao.FindShisakuBlockUnit(instl.BlockNo, shisakuKaihatsuFugoGosha)

                        If MUnitKbn IsNot Nothing Then

                            If instl.BlockNo = "604A" Then
                                vo.UnitKbn = "M"
                            ElseIf MUnitKbn.MtKbn IsNot Nothing Then
                                vo.UnitKbn = MUnitKbn.MtKbn
                            End If

                        End If

                        unitKbnDictionary.Put(instl.BlockNo, shisakuKaihatsuFugoGosha, vo.UnitKbn)
                    End If

                    ' 上位のIf内で判定し、毎回判定しないようにする
                    'If MUnitKbn IsNot Nothing Then

                    'If instl.BlockNo = "604A" Then
                    'MUnitKbn.MtKbn = "M"
                    'End If

                    'If MUnitKbn.MtKbn IsNot Nothing Then
                    'vo.UnitKbn = MUnitKbn.MtKbn
                    'End If

                    'End If

                    '/*** 20140911 CHANGE END ***/

                Else
                    'マスターからブロックに該当するユニット区分を取得する。
                    Dim MUnitKbn As TShisakuSekkeiBlockVo = dao.FindShisakuBlockUnitFromSekkeiBlock(shisakuEventCodeFromEvent, instl.BlockNo)

                    If MUnitKbn IsNot Nothing Then

                        If MUnitKbn.UnitKbn IsNot Nothing Then
                            vo.UnitKbn = MUnitKbn.UnitKbn
                        End If

                    End If

                End If

                '2014/07/29 TSUNODA EDIT END

                vo.ShisakuBlockName = blockNames(instl.BlockNo)
                vo.UserId = String.Empty
                vo.TelNo = String.Empty
                vo.SaisyuKoushinbi = Nothing
                vo.SaisyuKoushinjikan = Nothing
                '↓↓2014/10/21 酒井 ADD BEGIN
                'vo.Memo = String.Empty
                If flg Then
                    'メモ欄を利用して、呼出し元画面に元データ区分の識別を返す。
                    vo.Memo = instl.InstlDataKbn
                Else
                    vo.Memo = String.Empty
                End If
                '↑↑2014/10/21 酒井 ADD END
                vo.TantoSyouninJyoutai = String.Empty
                vo.TantoSyouninKa = String.Empty
                vo.TantoSyouninSya = String.Empty
                vo.TantoSyouninHi = Nothing
                vo.TantoSyouninJikan = Nothing
                vo.KachouSyouninJyoutai = String.Empty
                vo.KachouSyouninKa = String.Empty
                vo.KachouSyouninSya = String.Empty
                vo.KachouSyouninHi = Nothing
                vo.KachouSyouninJikan = Nothing
                vo.KaiteiHandanFlg = String.Empty
                'Return results

                results.Add(vo)
            Next

            Return results
        End Function

        ''' <summary>
        ''' 試作設計ブロック情報へ登録する
        ''' </summary>
        ''' <param name="login">ログイン情報</param>
        ''' <param name="shisakuSekkeiBlockDao">試作設計ブロック情報Dao</param>
        ''' <param name="aShisakuDate">試作システム日付</param>
        ''' <remarks></remarks>
        Public Sub Register(ByVal login As LoginInfo, _
                            ByVal shisakuSekkeiBlockDao As TShisakuSekkeiBlockDao, _
                            ByVal aShisakuDate As ShisakuDate)

            Dim alVosBase As List(Of TShisakuEventBaseVo) = daoBase.FindShisakuEventBase(shisakuEventCode)

            Dim registeredDataList As List(Of TShisakuSekkeiBlockVo) = New List(Of TShisakuSekkeiBlockVo)

            Dim daoSekkeika As ShisakuBlockSekkeikaTmpDao = New ShisakuBlockSekkeikaTmpDaoImpl
            Dim hshSekkeika As New Hashtable
            For Each vo As ShisakuBlockSekkeikaTmp4EbomVo In daoSekkeika.FindByAll4Ebom(shisakuEventCode)
                If Not hshSekkeika.Contains(vo.ShisakuBlockNo) Then
                    hshSekkeika.Add(vo.ShisakuBlockNo, vo)
                End If
            Next
            Dim hshTourokuZumi As New Hashtable

            For Each VoBase As TShisakuEventBaseVo In alVosBase
                Dim vos As List(Of TShisakuSekkeiBlockVo) = MakeRegisterValues(VoBase, True)
                If vos Is Nothing Then
                    Continue For
                End If

                For Each vo As TShisakuSekkeiBlockVo In vos
                    ''''今まで重複チェックってやっていなかったがここでやらないと
                    ''''エラーが発生してしまう。SekkeiBlockSupplierOLDにはこの重複チェックを
                    ''''実装していた形跡がある　 by 太田
                    '重複キーが無いかチェック。
                    'If shisakuSekkeiBlockDao.FindByPk(vo.ShisakuEventCode, _
                    '                                  vo.ShisakuBukaCode, _
                    '                                  vo.ShisakuBlockNo, _
                    '                                  vo.ShisakuBlockNoKaiteiNo) Is Nothing Then
                    '/*** 20140911 CHANGE START ***/
                    ' If dao.FindSekkeiBlock(vo.ShisakuEventCode, _
                    '                        vo.ShisakuBukaCode, _
                    '                        vo.ShisakuBlockNo, _
                    '                        vo.ShisakuBlockNoKaiteiNo) Is Nothing Then

                    Dim key As New System.Text.StringBuilder
                    key.AppendLine(vo.ShisakuBlockNo)
                    key.AppendLine(vo.ShisakuBlockNoKaiteiNo)
                    If Not hshTourokuZumi.Contains(key.ToString) Then
                        'If Not CheckChofuku(vo, registeredDataList) Then
                        '/*** 20140911 CHANGE END ***/
                        If hshSekkeika.Contains(vo.ShisakuBlockNo) Then
                            vo.ShisakuBukaCode = CType(hshSekkeika.Item(vo.ShisakuBlockNo), ShisakuBlockSekkeikaTmp4EbomVo).ShisakuBukaCode
                        End If

                        vo.CreatedUserId = login.UserId
                        vo.CreatedDate = aShisakuDate.CurrentDateDbFormat
                        vo.CreatedTime = aShisakuDate.CurrentTimeDbFormat
                        vo.UpdatedUserId = login.UserId
                        vo.UpdatedDate = aShisakuDate.CurrentDateDbFormat
                        vo.UpdatedTime = aShisakuDate.CurrentTimeDbFormat


                        shisakuSekkeiBlockDao.InsertBy(vo)

                        '/*** 20140911 CHANGE START ***/
                        ' リストに追加
                        registeredDataList.Add(vo)
                        '/*** 20140911 CHANGE END ***/
                        'End If
                        hshTourokuZumi.Add(key.ToString, vo)
                    End If
                Next
            Next

        End Sub
        
        '/*** 20140911 CHANGE START ***/
        Private Function CheckChofuku(ByVal vo As TShisakuSekkeiBlockVo, ByRef registeredDataList As List(Of TShisakuSekkeiBlockVo)) As Boolean
            For Each registeredData As TShisakuSekkeiBlockVo In registeredDataList

                ' チェックにイベントコードは不要
                'If vo.ShisakuEventCode = registeredData.ShisakuEventCode _
                '     AndAlso vo.ShisakuBukaCode = registeredData.ShisakuBukaCode _
                '     AndAlso vo.ShisakuBlockNo = registeredData.ShisakuBlockNo _
                '     AndAlso vo.ShisakuBlockNoKaiteiNo = registeredData.ShisakuBlockNoKaiteiNo Then

                If vo.ShisakuBukaCode = registeredData.ShisakuBukaCode _
                     AndAlso vo.ShisakuBlockNo = registeredData.ShisakuBlockNo _
                     AndAlso vo.ShisakuBlockNoKaiteiNo = registeredData.ShisakuBlockNoKaiteiNo Then

                    'If vo.ShisakuBlockNo = registeredData.ShisakuBlockNo _
                    '  AndAlso vo.ShisakuBlockNoKaiteiNo = registeredData.ShisakuBlockNoKaiteiNo Then
                    ' データがあった場合
                    Return True
                End If

            Next

            ' データがなかった場合
            Return False

        End Function
        '/*** 20140911 CHANGE END ***/

        Private Class BukaBlockGroup : Implements VoGroupingRule(Of BukaCodeBlockNoVo)
            Public Sub GroupRule(ByVal groupBy As VoGroupingLocator, ByVal vo As BukaCodeBlockNoVo) Implements VoGroupingRule(Of BukaCodeBlockNoVo).GroupRule
                groupBy.Prop(vo.BukaCode).Prop(vo.BlockNo)
            End Sub
        End Class
        '↓↓2014/10/21 酒井 ADD BEGIN
        Private Class BlockGroup : Implements VoGroupingRule(Of BukaCodeBlockNoVo)
            Public Sub GroupRule(ByVal groupBy As VoGroupingLocator, ByVal vo As BukaCodeBlockNoVo) Implements VoGroupingRule(Of BukaCodeBlockNoVo).GroupRule
                groupBy.Prop(vo.BlockNo)
            End Sub
        End Class

        '↑↑2014/10/21 酒井 ADD END
        Private Class BukaBlockComparer : Implements IComparer(Of BukaCodeBlockNoVo)
            Public Function Compare(ByVal x As BukaCodeBlockNoVo, ByVal y As BukaCodeBlockNoVo) As Integer Implements IComparer(Of BukaCodeBlockNoVo).Compare
                'Dim resultA As Integer = x.BukaCode.CompareTo(y.BukaCode)
                'If resultA <> 0 Then
                '    Return resultA
                'End If
                'Return x.BlockNo.CompareTo(y.BlockNo)
            End Function
        End Class
        '↓↓2014/10/21 酒井 ADD BEGIN
        Public Class BlockBukaComparer : Implements IComparer(Of BukaCodeBlockNoVo)
            Public Function Compare(ByVal x As BukaCodeBlockNoVo, ByVal y As BukaCodeBlockNoVo) As Integer Implements IComparer(Of BukaCodeBlockNoVo).Compare
                Dim resultA As Integer = String.Compare(x.BlockNo, y.BlockNo)
                If resultA <> 0 Then
                    Return resultA
                End If
                Return String.Compare(x.BukaCode, y.BukaCode)
            End Function
        End Class
        '↑↑2014/10/21 酒井 ADD END
        '↓↓2014/10/22 酒井 ADD BEGIN
        Public Class BlockBukaBlockNameComparer : Implements IComparer(Of TShisakuBlockSekkeikaTmpVo)
            Public Function Compare(ByVal x As TShisakuBlockSekkeikaTmpVo, ByVal y As TShisakuBlockSekkeikaTmpVo) As Integer Implements IComparer(Of TShisakuBlockSekkeikaTmpVo).Compare
                Dim resultA As Integer = String.Compare(x.ShisakuBlockNo, y.ShisakuBlockNo)
                If resultA <> 0 Then
                    Return resultA
                End If

                Dim resultB As Integer = String.Compare(x.ShisakuBukaCode, y.ShisakuBukaCode)
                If resultB <> 0 Then
                    Return resultB
                End If

                Return String.Compare(x.ShisakuBlockName, y.ShisakuBlockName)
            End Function
        End Class
        '↑↑2014/10/22 酒井 ADD END
        ''' <summary>
        ''' 設計課とブロックNoでグループ化した情報を返す
        ''' </summary>
        ''' <param name="alVos">A/Lの素の一覧</param>
        ''' <param name="instlVos">試作設計ブロックINSTL情報の一覧</param>
        ''' <returns>グループ化した情報</returns>
        ''' <remarks></remarks>
        Public Function GroupBukaCodeBlockNos(ByVal alVos As List(Of SekkeiBlockAlResultVo), _
                                              ByVal instlVos As List(Of SekkeiBlockInstlResultVo), _
                                              Optional ByVal flg As Boolean = False) As List(Of BukaCodeBlockNoVo)
            Dim sekkeikaDao As ShisakuBlockSekkeikaTmpDao = New ShisakuBlockSekkeikaTmpDaoImpl
            Dim sekkeikaVos As List(Of ShisakuBlockSekkeikaTmp4EbomVo) = sekkeikaDao.FindByAll4Ebom(shisakuEventCode)
            Dim sekkeikaDic As New Dictionary(Of String, ShisakuBlockSekkeikaTmp4EbomVo)
            If Not flg Then
                For Each sekkeikaVo As ShisakuBlockSekkeikaTmp4EbomVo In sekkeikaVos
                    sekkeikaDic.Add(sekkeikaVo.ShisakuBlockNo, sekkeikaVo)
                Next
            End If

            Dim results As New List(Of BukaCodeBlockNoVo)
            For Each alVo As SekkeiBlockAlResultVo In alVos
                Dim baseVo As New TShisakuEventBaseVo
                Dim tmpUnitKbn As String
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
                    Dim MUnitKbn As Rhac0080Vo = dao.FindShisakuBlockUnit(alVo.BlockNo, shisakuKaihatsuFugo)
                    If alVo.BlockNo = "604A" Then
                        tmpUnitKbn = "M"
                    Else
                        tmpUnitKbn = MUnitKbn.MtKbn
                    End If
                    unitKbnDictionary.Put(alVo.BlockNo, shisakuKaihatsuFugo, tmpUnitKbn)
                End If

                'イベント情報のユニット区分に該当するデータのみ処理する。
                ' S:全て対象
                ' M:M、ブランクのみ対象
                ' T:T、ブランクのみ対象
                If unitKbn <> "S" Then
                    If unitKbn <> tmpUnitKbn AndAlso Not StringUtil.IsEmpty(tmpUnitKbn) Then
                        '条件に該当しないので次のデータへ
                        Continue For
                    End If
                End If
                'データを追加
                Dim vo As New BukaCodeBlockNoVo

                vo.BukaCode = alVo.BukaCode  '課略名

                vo.BlockNo = alVo.BlockNo
                vo.InstlDataKbn = "0"

                '設計課更新機能として、
                'EBOM同士で設計課が複数存在した場合、ユニークに圧縮する。
                '一旦、圧縮先を決定する。
                '（EBOM部品構成抽出は従来の設計課でないと抽出できないため、設計課更新処理は最後に実装する。）
                If Not flg Then
                    If sekkeikaDic(vo.BlockNo).ShisakuBukaCode = vo.BukaCode Then
                        sekkeikaDic(vo.BlockNo).ShisakuBukaCode4Ebom = vo.BukaCode
                    ElseIf StringUtil.IsEmpty(sekkeikaDic(vo.BlockNo).ShisakuBukaCode4Ebom) Then
                        sekkeikaDic(vo.BlockNo).ShisakuBukaCode4Ebom = vo.BukaCode
                    End If
                End If
                '↑↑2014/10/24 酒井 ADD END

                results.Add(vo)
            Next

            '------------------------------------------------------------------
            '２次改修
            '　部課コードの問題について
            '   2012/08/08 部課略名で更新してみる。
            Dim KARyakuName = New ShisakuBuhinEditBlock.Dao.KaRyakuNameDaoImpl '部課略称名取得IMPL
            Dim strRyakuName As String
            '------------------------------------------------------------------

            For Each instlVo As SekkeiBlockInstlResultVo In instlVos
                '------------------------------------------------------------------
                '２次改修
                '　部課コードの問題について
                '   2012/08/08 部課略名で更新してみる。
                strRyakuName = KARyakuName.GetKa_Ryaku_Name(instlVo.ShisakuBukaCode).KaRyakuName()
                '------------------------------------------------------------------

                Dim vo As New BukaCodeBlockNoVo
                '------------------------------------------------------------------
                '２次改修
                '　部課コードの問題について
                '   2012/08/08 部課略名で更新してみる。
                'vo.BukaCode = instlVo.ShisakuBukaCode
                If StringUtil.IsEmpty(strRyakuName) Then
                    vo.BukaCode = instlVo.ShisakuBukaCode
                Else
                    vo.BukaCode = strRyakuName
                End If
                '------------------------------------------------------------------
                vo.BlockNo = instlVo.ShisakuBlockNo
                vo.InstlDataKbn = "1"

                results.Add(vo)
            Next

            Dim groupInstl As New VoGrouping(Of BukaCodeBlockNoVo)(New BukaBlockGroup)

            results = groupInstl.Grouping(results)
            'results.Sort(New BukaBlockComparer)
            If flg Then
                results.Sort(New BlockBukaComparer)
            Else
                results.Sort(New BukaBlockComparer)
            End If

            Dim backBukaCode As String = String.Empty
            Dim hyoujiJun As Integer

            For Each vo As BukaCodeBlockNoVo In results
                If Not flg Then
                    '圧縮される設計課は削除しておく
                    If vo.BukaCode = sekkeikaDic(vo.BlockNo).ShisakuBukaCode Then
                        '試作イベントから抽出されたブロック、部課
                        'または、EBOMから抽出されたブロック、部課、かつ、部課が設計課更新画面入力値と同じ
                    Else
                        If vo.BukaCode = sekkeikaDic(vo.BlockNo).ShisakuBukaCode4Ebom Then
                            'EBOMから抽出されたブロック、部課、かつ、部課が圧縮先と同じ
                        Else
                            'EBOMから抽出されたブロック、部課、かつ、部課が圧縮先と異なる
                            results.Remove(vo)
                            Continue For
                        End If
                    End If
                End If
                If vo.BukaCode.Equals(backBukaCode) Then
                    hyoujiJun += 1
                Else
                    hyoujiJun = TShisakuSekkeiBlockVoHelper.ShisakuBlockNoHyoujiJun.START_VALUE
                    backBukaCode = vo.BukaCode
                End If
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
        Public Function ExtractBlockNames(ByVal alVos As List(Of SekkeiBlockAlResultVo), _
                                          ByVal instlVos As List(Of SekkeiBlockInstlResultVo)) As Dictionary(Of String, String)
            'ByVal alVosOld As List(Of SekkeiBlockAlResultVo), _

            Dim blockNames As New Dictionary(Of String, String)
            For Each vo As SekkeiBlockAlResultVo In alVos
                If blockNames.ContainsKey(vo.BlockNo) Then
                    Continue For
                End If
                blockNames.Add(vo.BlockNo, vo.BlockName)
            Next
            'For Each vo As SekkeiBlockAlResultVo In alVosOld
            '    If blockNames.ContainsKey(vo.BlockNo) Then
            '        Continue For
            '    End If
            '    blockNames.Add(vo.BlockNo, vo.BlockName)
            'Next
            For Each vo As SekkeiBlockInstlResultVo In instlVos
                If blockNames.ContainsKey(vo.ShisakuBlockNo) Then
                    Continue For
                End If
                blockNames.Add(vo.ShisakuBlockNo, vo.ShisakuBlockName)
            Next
            Return blockNames
        End Function
        '↓↓2014/10/21 酒井 ADD BEGIN
        Public Function ConvSB2BukaBlock(ByVal sbVos As List(Of TShisakuSekkeiBlockVo)) As List(Of TShisakuBlockSekkeikaTmpVo)
            Dim results As New List(Of TShisakuBlockSekkeikaTmpVo)
            For Each sbVo As TShisakuSekkeiBlockVo In sbVos
                Dim result As New TShisakuBlockSekkeikaTmpVo
                result.ShisakuBlockNo = sbVo.ShisakuBlockNo
                result.ShisakuBlockName = sbVo.ShisakuBlockName
                result.ShisakuBukaCode = sbVo.ShisakuBukaCode
                results.Add(result)
            Next
            results.Sort(New BlockBukaBlockNameComparer)
            Return results
        End Function
        '↑↑2014/10/21 酒井 ADD END
    End Class
End Namespace