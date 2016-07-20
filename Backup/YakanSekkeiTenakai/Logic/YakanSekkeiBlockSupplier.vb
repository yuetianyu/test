Imports ShisakuCommon.Db.EBom.Vo.Helper
Imports ShisakuCommon
Imports ShisakuCommon.Db.EBom.Dao
Imports EventSakusei.ShisakuBuhinMenu.Dao
Imports ShisakuCommon.Util.Grouping
Imports ShisakuCommon.Db.EBom.Vo
Imports EventSakusei.ShisakuBuhinMenu.Logic.Impl.Tenkai
Imports EventSakusei
Imports YakanSekkeiTenakai.Dao
Imports YakanSekkeiTenakai.Vo

'Imports EventSakusei.ShisakuBuhinEditBlock.Dao

Namespace Logic
    Public Class YakanSekkeiBlockSupplier
        Private ReadOnly shisakuEventCode As String
        Private ReadOnly unitKbn As String
        Private ReadOnly dao As YakanSekkeiBlockDao
        Private ReadOnly daoBase As ShisakuEventBaseDao     '2012/01/13

        '/*** 20140911 CHANGE START ***/
        Private kaihatsuFugo As Dictionary(Of String, String)
        Private unitKbnDictionary As Dictionary2(Of String, String, String)
        '/*** 20140911 CHANGE END ***/

        '/*** 20140911 CHANGE START（コンストラクタ引数追加） ***/
        Public Sub New(ByVal shisakuEventVo As TShisakuEventVo)
        '     Me.New(shisakuEventVo, New YakanSekkeiBlockDaoImpl, New ShisakuEventBaseDaoImpl)
            Me.New(shisakuEventVo, New Dictionary2(Of String, String, String), New SekkeiBlockDaoImpl, New ShisakuEventBaseDaoImpl)
        End Sub
        Public Sub New(ByVal shisakuEventVo As TShisakuEventVo, _
                ByVal dao As YakanSekkeiBlockDao, ByVal daoBase As ShisakuEventBaseDao)
            '     Me.shisakuEventCode = shisakuEventVo.ShisakuEventCode
            '     Me.unitKbn = shisakuEventVo.UnitKbn
            '     Me.dao = dao
            '     Me.daoBase = daoBase
            Me.New(shisakuEventVo, New Dictionary2(Of String, String, String), dao, daoBase)

        End Sub
        Public Sub New(ByVal shisakuEventVo As TShisakuEventVo, ByRef unitKbnDictionary As Dictionary2(Of String, String, String))
            Me.New(shisakuEventVo, unitKbnDictionary, New YakanSekkeiBlockDaoImpl, New ShisakuEventBaseDaoImpl)
        End Sub
        Public Sub New(ByVal shisakuEventVo As TShisakuEventVo, _
                       ByRef unitKbnDictionary As Dictionary2(Of String, String, String), _
                       ByVal dao As YakanSekkeiBlockDao, ByVal daoBase As ShisakuEventBaseDao)
            Me.shisakuEventCode = shisakuEventVo.ShisakuEventCode
            Me.unitKbn = shisakuEventVo.UnitKbn
            Me.dao = dao
            Me.daoBase = daoBase

            Me.kaihatsuFugo = New Dictionary(Of String, String)
            Me.unitKbnDictionary = unitKbnDictionary
        End Sub
        '/*** 20140911 CHANGE END ***/

        ''↓↓2014/08/19 Ⅰ.5.EBOM差分出力  bd) (TES)張 CHG BEGIN
        'Public Function MakeRegisterValues(ByVal baseVo As TShisakuEventBaseVo) As List(Of TShisakuSekkeiBlockVo)
        Public Function MakeRegisterValues(ByVal login As LoginInfo, ByVal baseVo As TShisakuEventBaseVo) As List(Of TShisakuSekkeiBlockEbomKanshiVo)
            ''↑↑2014/08/19 Ⅰ.5.EBOM差分出力 bd) (TES)張 CHG END
            Dim shisakuKaihatsuFugo As String = ""       '号車の開発符号
            Dim HyojijunNo = baseVo.HyojijunNo      '号車の表示順
            Dim baseVo1 As New TShisakuEventBaseVo
            '↓↓2014/09/24 酒井 ADD BEGIN
            Dim ebomKanshiVo1 As New TShisakuEventEbomKanshiVo
            '↑↑2014/09/24 酒井 ADD END
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


            ''↓↓2014/08/13 1 ベース部品表作成表機能増強_Ⅰ③) (ダニエル上海)柳沼 ADD BEGIN
            '2012/02/16 号車ごとの開発符号を取得
            Dim wFlg As String = Nothing
            Dim BLOCKNO As String = Nothing
            Dim SHISAKUGOUSYA As String = Nothing
            Dim TOPCOLORKAITEINO As String = Nothing

            '/*** 20140911 CHANGE START ***/
            ' Dim alVosFinal As List(Of SekkeiBlockAlResultVo) = dao.FindAlByShisakuEventBase("A")
            ' Dim alVosFinalwk As List(Of SekkeiBlockAlResultVo) = dao.FindAlByShisakuEventBase("A")
            ' Dim alVosFinalOld As List(Of SekkeiBlockAlResultVo) = dao.FindAlByShisakuEventBase("A")
            ' Dim alVosFinalOldwk As List(Of SekkeiBlockAlResultVo) = dao.FindAlByShisakuEventBase("A")
            ' Dim alVosLast As List(Of SekkeiBlockAlResultVo) = dao.FindAlByShisakuEventBase("A")
            Dim alVosFinal As New List(Of SekkeiBlockAlResultVo)
            Dim alVosFinalwk As New List(Of SekkeiBlockAlResultVo)
            Dim alVosFinalOld As New List(Of SekkeiBlockAlResultVo)
            Dim alVosFinalOldwk As New List(Of SekkeiBlockAlResultVo)
            Dim alVosLast As New List(Of SekkeiBlockAlResultVo)
            '/*** 20140911 CHANGE END ***/

            Dim alVos As New List(Of SekkeiBlockAlResultVo)
            Dim alVosOld As New List(Of SekkeiBlockAlResultVo)
            Dim alVos2 As New List(Of SekkeiBlockAlResultVo)
            Dim alVosOld2 As New List(Of SekkeiBlockAlResultVo)
            Dim instlVos As New List(Of YakanSekkeiBlockInstlResultVo)

            If Not baseVo.ShisakuBaseEventCode = "" Then

                '2014/07/29 YAGINUMA EDIT START
                isEventGosya = True
                '2014/07/29 YAGINUMA EDIT END
                ''↓↓2014/08/05 Ⅰ.5.EBOM差分出力  bd) (TES)施 EDIT BEGIN
                If login IsNot Nothing Then
                    ''↑↑2014/08/05 Ⅰ.5.EBOM差分出力 bd) (TES)施 EDIT END
                    instlVos = dao.FindShisakuBlockInstlByShisakuEventBase(shisakuEventCode, unitKbn, HyojijunNo)
                    If instlVos.Count = 0 Then
                        Return Nothing
                    End If
                    shisakuEventCodeFromEvent = instlVos(0).ShisakuEventCode
                    ''↓↓2014/08/05 Ⅰ.5.EBOM差分出力  bd) (TES)施 EDIT BEGIN
                End If
                ''↑↑2014/08/05 Ⅰ.5.EBOM差分出力 bd) (TES)施 EDIT END
            End If

            If Not baseVo.BaseKaihatsuFugo = "" Then
                '↓↓2014/09/24 酒井 ADD BEGIN
                'baseVo1 = daoBase.FindShisakuEventBaseByEventCodeAndGousyaForShisakuGousya(shisakuEventCode, baseVo.ShisakuGousya)
                'shisakuKaihatsuFugo = baseVo1.BaseKaihatsuFugo
                ebomKanshiVo1 = daoBase.FindShisakuEventEbomKanshiByEventCodeAndGousyaForShisakuGousya(shisakuEventCode, baseVo.ShisakuGousya)
                shisakuKaihatsuFugo = ebomKanshiVo1.BaseKaihatsuFugo
                '↑↑2014/09/24 酒井 ADD END
                '↓↓2014/10/13 酒井 ADD BEGIN
                'End If
                ''-------------------------------------------------------

                'If Not isEventGosya Then
                '↑↑2014/10/13 酒井 ADD END
                '/*** 20140911 CHANGE START ***/
                ' 高速化対応のため使用しない項目は取得しない
                'alVos = dao.FindAlByShisakuEventBase(shisakuEventCode, HyojijunNo)
                'alVosOld = dao.FindAlByShisakuEventBaseOLD(shisakuEventCode, HyojijunNo)
                'alVos2 = dao.FindAlByShisakuEventBase2(shisakuEventCode, HyojijunNo)
                'alVosOld2 = dao.FindAlByShisakuEventBaseOLD2(shisakuEventCode, HyojijunNo)

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
            ''↑↑2014/08/13 1 ベース部品表作成表機能増強_Ⅰ①) (ダニエル上海)柳沼 ADD END


            Dim blockNos As List(Of BukaCodeBlockNoVo) = GroupBukaCodeBlockNos(alVosFinal, instlVos)
            'Dim blockNames As Dictionary(Of String, String) = ExtractBlockNames(alVos, alVosOld, instlVos)
            'Dim blockNos As List(Of BukaCodeBlockNoVo) = GroupBukaCodeBlockNos(alVos, alVosOld, instlVos)

            Dim results As New List(Of TShisakuSekkeiBlockEbomKanshiVo)

            For Each instl As BukaCodeBlockNoVo In blockNos

                Dim vo As New TShisakuSekkeiBlockEbomKanshiVo

                results.Add(vo)

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
                    If unitKbnDictionary.ContainsKeys(instl.BlockNo, shisakuKaihatsuFugo) Then
                        vo.UnitKbn = unitKbnDictionary.item(instl.BlockNo)(shisakuKaihatsuFugo)
                    Else
                        'マスターからブロックに該当するユニット区分を取得する。

                        Dim MUnitKbn As Rhac0080Vo = dao.FindShisakuBlockUnit(instl.BlockNo, shisakuKaihatsuFugo)

                        If MUnitKbn IsNot Nothing Then

                            If instl.BlockNo = "604A" Then
                                vo.UnitKbn = "M"
                            ElseIf MUnitKbn.MtKbn IsNot Nothing Then
                                vo.UnitKbn = MUnitKbn.MtKbn
                            End If

                        End If

                        unitKbnDictionary.Put(instl.BlockNo, shisakuKaihatsuFugo, vo.UnitKbn)
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
                    Dim MUnitKbn As TShisakuSekkeiBlockEbomKanshiVo = dao.FindShisakuBlockUnitFromSekkeiBlock(shisakuEventCodeFromEvent, instl.BlockNo)

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
                vo.Memo = String.Empty
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
            Next

            Return results
        End Function

        ''' <summary>
        ''' 試作設計ブロック情報へ登録する
        ''' </summary>
        ''' <param name="login">ログイン情報</param>
        ''' <param name="shisakuSekkeiBlockEbomKanshiDao">試作設計ブロック情報Dao</param>
        ''' <param name="aShisakuDate">試作システム日付</param>
        ''' <remarks></remarks>
        Public Sub Register(ByVal login As LoginInfo, _
                            ByVal shisakuSekkeiBlockEbomKanshiDao As TShisakuSekkeiBlockEbomKanshiDao, _
                            ByVal aShisakuDate As ShisakuDate)
            '↓↓2014/09/24 酒井 ADD BEGIN
            'Dim alVosBase As List(Of TShisakuEventBaseVo) = daoBase.FindShisakuEventBase(shisakuEventCode)
            Dim alVosBase As List(Of TShisakuEventEbomKanshiVo) = daoBase.FindShisakuEventEbomKanshi(shisakuEventCode)
            '↑↑2014/09/24 酒井 ADD END

            '/*** 20140911 CHANGE START ***/
            Dim registeredDataList As List(Of TShisakuSekkeiBlockEbomKanshiVo) = New List(Of TShisakuSekkeiBlockEbomKanshiVo)
            '/*** 20140911 CHANGE END ***/

            '↓↓2014/09/24 酒井 ADD BEGIN
            'For Each VoBase As TShisakuEventBaseVo In alVosBase
            For Each VoEbomKanshi As TShisakuEventEbomKanshiVo In alVosBase
                Dim VoBase As New TShisakuEventBaseVo
                VoUtil.CopyProperties(VoEbomKanshi, VoBase)
                '↑↑2014/09/24 酒井 ADD END
#If DEBUG Then
                'If Not VoBase.HyojijunNo = 0 Then
                '    Continue For
                'End If
#End If

                ''↓↓2014/08/19 Ⅰ.5.EBOM差分出力  bd) (TES)張 CHG BEGIN
                'Dim vos As List(Of TShisakuSekkeiBlockVo) = MakeRegisterValues(VoBase)
                Dim vos As List(Of TShisakuSekkeiBlockEbomKanshiVo) = MakeRegisterValues(login, VoBase)
                ''↑↑2014/08/19 Ⅰ.5.EBOM差分出力 bd) (TES)張 CHG END
                If vos Is Nothing Then
                    Continue For
                End If


                For Each vo As TShisakuSekkeiBlockEbomKanshiVo In vos
                    ''''今まで重複チェックってやっていなかったがここでやらないと
                    ''''エラーが発生してしまう。SekkeiBlockSupplierOLDにはこの重複チェックを
                    ''''実装していた形跡がある　 by 太田
                    '重複キーが無いかチェック。
                    'If shisakuSekkeiBlockDao.FindByPk(vo.ShisakuEventCode, _
                    '                                  vo.ShisakuBukaCode, _
                    '                                  vo.ShisakuBlockNo, _
                    '                                  vo.ShisakuBlockNoKaiteiNo) Is Nothing Then
                    '/*** 20140911 CHANGE START ***/
                    'If dao.FindSekkeiBlock(vo.ShisakuEventCode, _
                    '                       vo.ShisakuBukaCode, _
                    '                       vo.ShisakuBlockNo, _
                    '                       vo.ShisakuBlockNoKaiteiNo) Is Nothing Then
                    If Not CheckChofuku(vo, registeredDataList) Then
                        '/*** 20140911 CHANGE END ***/

                        ''↓↓2014/08/19 Ⅰ.5.EBOM差分出力   (TES)張 ADD BEGIN
                        ''loginがブランクと可能
                        ''↓↓2014/08/27 Ⅰ.5.EBOM差分出力   酒井 ADD BEGIN
                        'If login IsNot Nothing Then
                        If login Is Nothing Then
                            ''↑↑2014/08/19 Ⅰ.5.EBOM差分出力  (TES)張 ADD END
                            'vo.CreatedUserId = login.UserId
                            vo.CreatedUserId = ""
                            vo.CreatedDate = aShisakuDate.CurrentDateDbFormat
                            vo.CreatedTime = aShisakuDate.CurrentTimeDbFormat
                            'vo.UpdatedUserId = login.UserId
                            vo.UpdatedUserId = ""
                            ''↑↑2014/08/27 Ⅰ.5.EBOM差分出力  酒井 ADD END
                            vo.UpdatedDate = aShisakuDate.CurrentDateDbFormat
                            vo.UpdatedTime = aShisakuDate.CurrentTimeDbFormat

                            shisakuSekkeiBlockEbomKanshiDao.InsertBy(vo)
                            ''↓↓2014/08/19 Ⅰ.5.EBOM差分出力   (TES)張 ADD BEGIN
                        End If
                        ''↑↑2014/08/19 Ⅰ.5.EBOM差分出力  (TES)張 ADD END

                        '/*** 20140911 CHANGE START ***/
                        ' リストに追加
                        registeredDataList.Add(vo)
                        '/*** 20140911 CHANGE END ***/
                    End If


                Next
            Next

        End Sub

        '/*** 20140911 CHANGE START ***/
        Private Function CheckChofuku(ByVal vo As TShisakuSekkeiBlockEbomKanshiVo, ByRef registeredDataList As List(Of TShisakuSekkeiBlockEbomKanshiVo)) As Boolean
            For Each registeredData As TShisakuSekkeiBlockEbomKanshiVo In registeredDataList

                ' チェックにイベントコードは不要
                'If vo.ShisakuEventCode = registeredData.ShisakuEventCode _
                '     AndAlso vo.ShisakuBukaCode = registeredData.ShisakuBukaCode _
                '     AndAlso vo.ShisakuBlockNo = registeredData.ShisakuBlockNo _
                '     AndAlso vo.ShisakuBlockNoKaiteiNo = registeredData.ShisakuBlockNoKaiteiNo Then

                If vo.ShisakuBukaCode = registeredData.ShisakuBukaCode _
                     AndAlso vo.ShisakuBlockNo = registeredData.ShisakuBlockNo _
                     AndAlso vo.ShisakuBlockNoKaiteiNo = registeredData.ShisakuBlockNoKaiteiNo Then

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

        Private Class BukaBlockComparer : Implements IComparer(Of BukaCodeBlockNoVo)
            Public Function Compare(ByVal x As BukaCodeBlockNoVo, ByVal y As BukaCodeBlockNoVo) As Integer Implements IComparer(Of BukaCodeBlockNoVo).Compare
                Dim resultA As Integer = x.BukaCode.CompareTo(y.BukaCode)
                If resultA <> 0 Then
                    Return resultA
                End If
                Return x.BlockNo.CompareTo(y.BlockNo)
            End Function
        End Class

        ''' <summary>
        ''' 設計課とブロックNoでグループ化した情報を返す
        ''' </summary>
        ''' <param name="alVos">A/Lの素の一覧</param>
        ''' <param name="instlVos">試作設計ブロックINSTL情報の一覧</param>
        ''' <returns>グループ化した情報</returns>
        ''' <remarks></remarks>
        Public Function GroupBukaCodeBlockNos(ByVal alVos As List(Of SekkeiBlockAlResultVo), _
                                              ByVal instlVos As List(Of YakanSekkeiBlockInstlResultVo) _
                                              ) As List(Of BukaCodeBlockNoVo)
            'ByVal alVosOld As List(Of SekkeiBlockAlResultVo), _

            Dim results As New List(Of BukaCodeBlockNoVo)
            For Each alVo As SekkeiBlockAlResultVo In alVos
                Dim baseVo As New TShisakuEventBaseVo
                '↓↓2014/09/24 酒井 ADD BEGIN
                Dim ebomKanshiVo As New TShisakuEventEbomKanshiVo
                '↑↑2014/09/24 酒井 ADD END
                '/*** 20140911 CHANGE START ***/
                Dim tmpUnitKbn As String
                '/*** 20140911 CHANGE END ***/
                '#If DEBUG Then

                '                If alVo.BlockNo = "960J" Then
                '                    Dim x As String
                '                    x = "22"
                '                End If
                '#End If
                Dim shisakuKaihatsuFugo As String
                
                '/*** 20140911 CHANGE START（キャッシュ対応） ***/

                ''2012/02/16 号車ごとの開発符号を取得
                'baseVo = daoBase.FindShisakuEventBaseByEventCodeAndGousyaForShisakuGousya(shisakuEventCode, alVo.ShisakuGousya)
                'If baseVo Is Nothing Then
                '    baseVo = daoBase.FindShisakuEventBaseByEventCodeAndGousyaForShisakuBaseGousya(shisakuEventCode, alVo.ShisakuGousya)
                'End If
                'shisakuKaihatsuFugo = baseVo.BaseKaihatsuFugo


                If kaihatsuFugo.ContainsKey(alVo.ShisakuGousya) Then
                    shisakuKaihatsuFugo = kaihatsuFugo(alVo.ShisakuGousya)
                Else
                    '2012/02/16 号車ごとの開発符号を取得
                    '↓↓2014/09/24 酒井 ADD BEGIN
                    'baseVo = daoBase.FindShisakuEventBaseByEventCodeAndGousyaForShisakuGousya(shisakuEventCode, alVo.ShisakuGousya)
                    ebomKanshiVo = daoBase.FindShisakuEventEbomKanshiByEventCodeAndGousyaForShisakuGousya(shisakuEventCode, alVo.ShisakuGousya)
                    'If baseVo Is Nothing Then
                    If ebomKanshiVo Is Nothing Then
                        'baseVo = daoBase.FindShisakuEventBaseByEventCodeAndGousyaForShisakuBaseGousya(shisakuEventCode, alVo.ShisakuGousya)
                        ebomKanshiVo = daoBase.FindShisakuEventEbomKanshiByEventCodeAndGousyaForShisakuBaseGousya(shisakuEventCode, alVo.ShisakuGousya)
                    End If
                    VoUtil.CopyProperties(ebomKanshiVo, baseVo)
                    '↑↑2014/09/24 酒井 ADD END

                    kaihatsuFugo.Add(alVo.ShisakuGousya, baseVo.BaseKaihatsuFugo)
                    shisakuKaihatsuFugo = baseVo.BaseKaihatsuFugo
                End If
                '/*** 20140911 CHANGE END ***/

                '/*** 20140911 CHANGE START（キャッシュ対応） ***/
                'マスターからブロックに該当するユニット区分を取得する。
                'Dim MUnitKbn As Rhac0080Vo = dao.FindShisakuBlockUnit(alVo.BlockNo, shisakuKaihatsuFugo)
                'If alVo.BlockNo = "604A" Then
                '    MUnitKbn.MtKbn = "M"
                'End If
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
                '/*** 20140911 CHANGE END ***/

                'イベント情報のユニット区分に該当するデータのみ処理する。
                ' S:全て対象
                ' M:M、ブランクのみ対象
                ' T:T、ブランクのみ対象
                If unitKbn <> "S" Then
                    '/*** 20140911 CHANGE START ***/
                    'If unitKbn <> MUnitKbn.MtKbn And Not StringUtil.IsEmpty(MUnitKbn.MtKbn) Then
                    If unitKbn <> tmpUnitKbn AndAlso Not StringUtil.IsEmpty(tmpUnitKbn) Then
                        '/*** 20140911 CHANGE END ***/
                        '条件に該当しないので次のデータへ
                        Continue For
                    End If
                End If
                'データを追加
                Dim vo As New BukaCodeBlockNoVo

                '/*** 20140911 CHANGE START（IF、ELSEともに結果が同じ） ***/
                ' 'マスタに無い場合、課略名をセットします。　2011/2/3 by柳沼
                ' If StringUtil.IsEmpty(alVo.BuCode & alVo.KaCode) Then
                '     vo.BukaCode = alVo.BukaCode  '課略名
                ' Else
                '     '------------------------------------------------------------------
                '     '２次改修
                '     '　部課コードの問題について
                '     '   2012/07/30 部課略名で更新してみる。
                '     'vo.BukaCode = alVo.BuCode & alVo.KaCode  '部コードと課コード
                '     vo.BukaCode = alVo.BukaCode  '課略名
                '     '------------------------------------------------------------------
                ' End If
                vo.BukaCode = alVo.BukaCode  '課略名
                '/*** 20140911 CHANGE END ***/

                vo.BlockNo = alVo.BlockNo
                results.Add(vo)
            Next
            'For Each alVo As SekkeiBlockAlResultVo In alVosOld
            '    'マスターからブロックに該当するユニット区分を取得する。
            '    Dim MUnitKbn As Rhac0080Vo = dao.FindShisakuBlockUnit(alVo.BlockNo)
            '    If alVo.BlockNo = "604A" Then
            '        MUnitKbn.MtKbn = "M"
            '    End If
            '    'イベント情報のユニット区分に該当するデータのみ処理する。
            '    ' S:全て対象
            '    ' M:M、ブランクのみ対象
            '    ' T:T、ブランクのみ対象
            '    If unitKbn <> "S" Then
            '        If unitKbn <> MUnitKbn.MtKbn And Not StringUtil.IsEmpty(MUnitKbn.MtKbn) Then
            '            '条件に該当しないので次のデータへ
            '            Continue For
            '        End If
            '    End If
            '    'データを追加
            '    Dim vo As New BukaCodeBlockNoVo

            '    'マスタに無い場合、課略名をセットします。　2011/2/3 by柳沼
            '    If StringUtil.IsEmpty(alVo.BuCode & alVo.KaCode) Then
            '        vo.BukaCode = alVo.BukaCode  '課略名
            '    Else
            '        vo.BukaCode = alVo.BuCode & alVo.KaCode  '部コードと課コード
            '    End If

            '    vo.BlockNo = alVo.BlockNo
            '    results.Add(vo)
            'Next

            '------------------------------------------------------------------
            '２次改修
            '　部課コードの問題について
            '   2012/08/08 部課略名で更新してみる。
            Dim KARyakuName = New ShisakuBuhinEditBlock.Dao.KaRyakuNameDaoImpl '部課略称名取得IMPL
            Dim strRyakuName As String
            '------------------------------------------------------------------

            For Each instlVo As YakanSekkeiBlockInstlResultVo In instlVos
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

                results.Add(vo)
            Next

            Dim groupInstl As New VoGrouping(Of BukaCodeBlockNoVo)(New BukaBlockGroup)

            results = groupInstl.Grouping(results)
            results.Sort(New BukaBlockComparer)

            Dim backBukaCode As String = String.Empty
            Dim hyoujiJun As Integer

            For Each vo As BukaCodeBlockNoVo In results
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
                                          ByVal instlVos As List(Of YakanSekkeiBlockInstlResultVo)) As Dictionary(Of String, String)
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
            For Each vo As YakanSekkeiBlockInstlResultVo In instlVos
                If blockNames.ContainsKey(vo.ShisakuBlockNo) Then
                    Continue For
                End If
                blockNames.Add(vo.ShisakuBlockNo, vo.ShisakuBlockName)
            Next
            Return blockNames
        End Function
    End Class
End Namespace