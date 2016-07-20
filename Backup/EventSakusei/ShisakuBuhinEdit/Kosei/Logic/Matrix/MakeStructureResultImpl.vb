Imports EventSakusei.ShisakuBuhinEdit.Logic
Imports ShisakuCommon
Imports ShisakuCommon.Db.EBom.Dao.Impl
Imports ShisakuCommon.Db.EBom.Dao
Imports EventSakusei.ShisakuBuhinEdit.Kosei.Dao
Imports EventSakusei.ShisakuBuhinEdit.Logic.Detect
Imports EventSakusei.ShisakuBuhinEdit.Kosei.Logic.Merge
Imports ShisakuCommon.Db.EBom.Vo.Helper
Imports ShisakuCommon.Db.EBom.Vo
Imports EventSakusei.ShisakuBuhinEdit.Kosei.Logic.Tree

Namespace ShisakuBuhinEdit.Kosei.Logic.Matrix
    ''' <summary>
    ''' 「構成の情報」で部品表を作成するメソッドクラス
    ''' </summary>
    ''' <remarks></remarks>
    Public Class MakeStructureResultImpl
        Implements MakeStructureResult

        Private ReadOnly editDao As TShisakuBuhinEditDao
        Private ReadOnly editInstlDao As TShisakuBuhinEditInstlDao
        Private ReadOnly makeDao As MakeStructureResultDao
        Private shisakuEventCode As String
        Private shisakuBukaCode As String
        Private shisakuBlockNo As String
        '0553に至る場合用にどこからきたのか判断させる'
        '設計展開時：0, 構成再展開、最新化、部品構成呼び出し時：1, 子部品展開時：2'
        Private a0553flag As Integer
        Private KaihatsuFugo As String

        '/*** 20140911 CHANGE START ***/
         Dim kotanTorihikisakiSelected As Dictionary(Of String, TShisakuBuhinEditVo)
        '/*** 20140911 CHANGE END ***/

        ''' <summary>
        ''' コンストラクタ
        ''' </summary>
        ''' <remarks></remarks>
        Public Sub New()
            Me.New(New TShisakuBuhinEditDaoImpl, New TShisakuBuhinEditInstlDaoImpl, New MakeStructureResultDaoImpl)
            
            '/*** 20140911 CHANGE START ***/
            initDictionaryIfNothing()
            '/*** 20140911 CHANGE END ***/
        End Sub
        
        '/*** 20140911 CHANGE START ***/
        Private Sub initDictionaryIfNothing()
            'kotanTorihikisakiSelectedが初期化されていない場合は、初期化する
            If kotanTorihikisakiSelected Is Nothing Then

                kotanTorihikisakiSelected = New Dictionary(Of String, TShisakuBuhinEditVo)
            End If
        End Sub
        '/*** 20140911 CHANGE END ***/

        ''' <summary>
        ''' コンストラクタ
        ''' </summary>
        ''' <remarks></remarks>
        Public Sub New(ByVal shisakuEventCode As String, ByVal shisakuBukaCode As String, ByVal shisakuBlockNo As String)
            Me.New(New TShisakuBuhinEditDaoImpl, New TShisakuBuhinEditInstlDaoImpl, New MakeStructureResultDaoImpl)
            Me.shisakuEventCode = shisakuEventCode
            '2012/02/02 開発符号を取得させる'
            Me.shisakuBukaCode = shisakuBukaCode
            Me.shisakuBlockNo = shisakuBlockNo
            
            '/*** 20140911 CHANGE START ***/
            initDictionaryIfNothing()
            '/*** 20140911 CHANGE END ***/
        End Sub
        
        '/*** 20140911 CHANGE START（コンストラクタ追加） ***/
        ''' <summary>
        ''' コンストラクタ
        ''' </summary>
        ''' <remarks></remarks>
        Public Sub New(ByVal shisakuEventCode As String, ByVal shisakuBukaCode As String, ByVal shisakuBlockNo As String, _
                                ByRef kotanTorihikisakiSelected As Dictionary(Of String, TShisakuBuhinEditVo), _
                                ByVal editDao As TShisakuBuhinEditDao, ByVal editInstlDao As TShisakuBuhinEditInstlDao, ByVal makeDao As MakeStructureResultDao)

            Me.New(editDao, editInstlDao, makeDao)

            Me.shisakuEventCode = shisakuEventCode
            '2012/02/02 開発符号を取得させる'
            Me.shisakuBukaCode = shisakuBukaCode
            Me.shisakuBlockNo = shisakuBlockNo

            Me.kotanTorihikisakiSelected = kotanTorihikisakiSelected

        End Sub

        ''' <summary>
        ''' コンストラクタ
        ''' </summary>
        ''' <remarks></remarks>
        Public Sub New(ByVal shisakuEventCode As String, ByVal shisakuBukaCode As String, ByVal shisakuBlockNo As String, _
                                ByRef kotanTorihikisakiSelected As Dictionary(Of String, TShisakuBuhinEditVo))

            Me.New(New TShisakuBuhinEditDaoImpl, New TShisakuBuhinEditInstlDaoImpl, New MakeStructureResultDaoImpl)

            Me.shisakuEventCode = shisakuEventCode
            '2012/02/02 開発符号を取得させる'
            Me.shisakuBukaCode = shisakuBukaCode
            Me.shisakuBlockNo = shisakuBlockNo

            Me.kotanTorihikisakiSelected = kotanTorihikisakiSelected

        End Sub
        '/*** 20140911 CHANGE END ***/


        ''' <summary>
        ''' コンストラクタ
        ''' </summary>
        ''' <param name="editDao">試作部品編集Dao</param>
        ''' <param name="editInstlDao">試作部品編集INSTL Dao</param>
        ''' <param name="makeDao">当メソッドクラス専用Dao</param>
        ''' <remarks></remarks>
        Public Sub New(ByVal editDao As TShisakuBuhinEditDao, ByVal editInstlDao As TShisakuBuhinEditInstlDao, ByVal makeDao As MakeStructureResultDao)
            Me.editDao = editDao
            Me.editInstlDao = editInstlDao
            Me.makeDao = makeDao
        End Sub

        '構成再展開から開発符号をセットする'
        Public Sub KaihatsuFugoSet(ByVal KaihatsuFugo As String) Implements MakeStructureResult.KaihatsuFugoSet
            Me.KaihatsuFugo = KaihatsuFugo
        End Sub



        '2012/01/16 引数追加

        ''' <summary>
        ''' 「構成の情報」を元に部品表を作成する
        ''' </summary>
        ''' <param name="aStructureResult">構成の情報</param>
        ''' <returns>部品表</returns>
        ''' <remarks></remarks>
        Public Function Compute(ByVal aStructureResult As StructureResult, ByVal JikyuUmu As String) As BuhinKoseiMatrix Implements MakeStructureResult.Compute
            Return Compute(aStructureResult, a0553flag, 0)
        End Function


        Private oriBuhinNo As String
        Private bfBuhinNo As String

        ''' <summary>
        ''' 「構成の情報」を元に部品表を作成する(自給品有り)
        ''' </summary>
        ''' <param name="aStructureResult">構成の情報</param>
        ''' <param name="baseLevel">基点のレベル</param>
        ''' <param name="kaiteiNo">改定No　  2014/08/04 Ⅰ.11.改訂戻し機能 o) (TES)施 追加 </param>
        ''' <returns>部品表</returns>
        ''' <remarks></remarks>
        Public Function Compute(ByVal aStructureResult As StructureResult, ByVal a0553flag As Integer, ByVal baseLevel As Integer?, Optional ByVal KaiteiNo As String = "", Optional ByVal aInstlDataKbn As String = "", Optional ByVal aBaseInstlFlg As String = "", Optional ByVal eventCopyFlg As Boolean = False, Optional ByVal yakanFlg As Boolean = False) As BuhinKoseiMatrix Implements MakeStructureResult.Compute
            If Not aStructureResult.IsExist Then
                Return New BuhinKoseiMatrix
            End If

            oriBuhinNo = aStructureResult.OriginalBuhinNo
            bfBuhinNo = aStructureResult.OriginalBuhinNo


            '2012/02/03 aStructureResult.YobidashiMotoがあればメソッド分岐
            If aStructureResult.YobidashiMoto <> "" Then
                Return Compute2(aStructureResult, a0553flag, baseLevel)
            End If

            If Not aStructureResult.IsEBom Then

                If aStructureResult.EditVo IsNot Nothing Then
                    ' 5keyで最小のINSTL品番表示順のBuhinEditInstl
                    '自給品を含めて取得する。

                    Dim editInstlVo As New TShisakuBuhinEditInstlVo
                    Dim editInstlVos As New List(Of TShisakuBuhinEditInstlVo)
                    Dim editVos As New List(Of TShisakuBuhinEditVo)
                    If Not yakanFlg Then
                        editInstlVo = FindMinInstlHinbanHyoujiJunEditInstlBy(aStructureResult.EditVo)
                        editInstlVos = FindEditInstlByInstlHinbanHyoujiJun(editInstlVo)
                        editVos = FindEditBy(editInstlVo)
                    Else
                        Dim editInstlEbomKanshiVo As TShisakuBuhinEditInstlEbomKanshiVo = FindMinInstlHinbanHyoujiJunEditInstlEbomKanshiBy(aStructureResult.EditVo)
                        VoUtil.CopyProperties(editInstlEbomKanshiVo, editInstlVo)

                        Dim editInstlEbomKanshiVos As List(Of TShisakuBuhinEditInstlEbomKanshiVo) = FindEditInstlEbomKanshiByInstlHinbanHyoujiJun(editInstlVo)
                        For Each src As TShisakuBuhinEditInstlEbomKanshiVo In editInstlEbomKanshiVos
                            Dim dest As New TShisakuBuhinEditInstlVo
                            VoUtil.CopyProperties(src, dest)
                            editInstlVos.Add(dest)
                        Next

                        Dim editEbomKanshiVos As List(Of TShisakuBuhinEditEbomKanshiVo) = FindEditEbomKanshiBy(editInstlVo)
                        For Each src As TShisakuBuhinEditEbomKanshiVo In editEbomKanshiVos
                            Dim dest As New TShisakuBuhinEditVo
                            VoUtil.CopyProperties(src, dest)
                            editVos.Add(dest)
                        Next
                    End If
                    '↑↑2014/09/25 酒井 ADD END

                    ''2015/08/27 追加 E.Ubukata Ver 2.11.0
                    '' BaseBuhinFlgは引き継いではいけない
                    For Each vo As TShisakuBuhinEditVo In editVos
                        vo.BaseBuhinFlg = Nothing
                    Next

                    Dim result As BuhinKoseiMatrix = BuhinKoseiMatrix.NewInnerJoinHyoujiJun(editVos, editInstlVos)
                    result = result.ExtractUnderLevel(aStructureResult.EditVo.BuhinNoHyoujiJun)
                    result.RemoveNullRecords()
                    result.CorrectLevelBy(baseLevel)
                    Return result

                ElseIf aStructureResult.InstlVo IsNot Nothing Then
                    ''↓↓2014/08/04 Ⅰ.11.改訂戻し機能 o) (TES)施 ADD BEGIN`
                    Dim editInstlVos As List(Of TShisakuBuhinEditInstlVo)
                    Dim editVos As List(Of TShisakuBuhinEditVo)
                    If KaiteiNo = "" Then
                        '↓↓2014/09/25 酒井 ADD BEGIN
                        'editInstlVos = FindEditInstlByInstlHinbanHyoujiJun(aStructureResult.InstlVo)
                        'editVos = FindEditBy(aStructureResult.InstlVo)
                        If Not eventCopyFlg Then
                            If Not yakanFlg Then
                                editInstlVos = FindEditInstlByInstlHinbanHyoujiJun(aStructureResult.InstlVo)
                                editVos = FindEditBy(aStructureResult.InstlVo)
                            Else
                                editInstlVos = FindEditInstlByInstlHinbanHyoujiJun(aStructureResult.InstlVo, "", "", "", False, yakanFlg)
                                editVos = FindEditBy(aStructureResult.InstlVo, "", "", "", False, yakanFlg)
                            End If
                        Else
                            editInstlVos = FindEditInstlByInstlHinbanHyoujiJun(aStructureResult.InstlVo, "", aInstlDataKbn, aBaseInstlFlg, eventCopyFlg)
                            editVos = FindEditBy(aStructureResult.InstlVo, "", aInstlDataKbn, aBaseInstlFlg, eventCopyFlg)
                        End If
                            '↑↑2014/09/25 酒井 ADD END
                    Else
                        ''↓↓2014/09/23 酒井 ADD BEGIN`
                        'editInstlVos = FindEditInstlByInstlHinbanHyoujiJun(aStructureResult.InstlVo, KaiteiNo)
                        'editVos = FindEditBy(aStructureResult.InstlVo, KaiteiNo)
                        editInstlVos = FindEditInstlByInstlHinbanHyoujiJun(aStructureResult.InstlVo, KaiteiNo, aInstlDataKbn, aBaseInstlFlg)
                        editVos = FindEditBy(aStructureResult.InstlVo, KaiteiNo, aInstlDataKbn, aBaseInstlFlg)
                        ''↑↑2014/09/23 酒井 ADD END
                    End If

                    ''↑↑2014/08/04 Ⅰ.11.改訂戻し機能 o) (TES)施 ADD END

                    ''2015/08/27 追加 E.Ubukata Ver 2.11.0
                    '' BaseBuhinFlgは引き継いではいけない
                    For Each vo As TShisakuBuhinEditVo In editVos
                        vo.BaseBuhinFlg = Nothing
                    Next

                    Dim result As BuhinKoseiMatrix = BuhinKoseiMatrix.NewInnerJoinHyoujiJun(editVos, editInstlVos)
                    result.RemoveNullRecords()
                    result.CorrectLevelBy(baseLevel)
                    Return result
                End If
                Throw New ArgumentException("プロパティの内容が不正", "aStructureResult")
            End If

            Dim inputedBuhinNo As String = aStructureResult.BuhinNo

            'ここから追加 樺澤 2010/12/7 '
            'ベース車情報の開発符号毎に0532を見るのか0533を見るのか分けたい'
            Dim Rhac0552Vo As New Rhac0552Vo

            Rhac0552Vo = makeDao.FindByBuhinRhac0552(inputedBuhinNo)

            If Rhac0552Vo Is Nothing Then
                Dim eventBaseVo As New TShisakuEventBaseVo
                Dim BfBuhinNoVo As New TShisakuSekkeiBlockInstlVo
                Dim checkVo As New Rhac0553Vo
                Dim aKaihatsuFugo As String = ""

                '2012/02/02 '
                'INSTL品番を見に行く時にはオリジナル品番で見に行くようにする。


                '0553注記：設計展開時はベース車情報の開発符号を使用'
                '構成再展開、最新化、構成呼び出しは画面側から取得した開発符号を使用'
                '子部品展開はイベント情報の開発符号を使用する'


                If a0553flag = 0 Then
                    '設計展開時の処理'
                    '設計ブロックINSTL情報を元に、ベース車情報の開発符号を取得する'
                    '↓↓2014/09/25 酒井 ADD BEGIN
                    eventBaseVo = makeDao.FindByInstlBase(shisakuEventCode, shisakuBukaCode, shisakuBlockNo, aStructureResult.OriginalBuhinNo)
                    If Not yakanFlg Then
                        eventBaseVo = makeDao.FindByInstlBase(shisakuEventCode, shisakuBukaCode, shisakuBlockNo, aStructureResult.OriginalBuhinNo)
                        If eventBaseVo Is Nothing Then
                            Dim eventVo As New TShisakuEventVo
                            eventVo = makeDao.FindByEvent(shisakuEventCode)
                            aKaihatsuFugo = eventVo.ShisakuKaihatsuFugo
                        Else
                            aKaihatsuFugo = eventBaseVo.BaseKaihatsuFugo
                        End If
                    Else
                        Dim eventEbomKanshiVo As TShisakuEventEbomKanshiVo = makeDao.FindByInstlEbomKanshi(shisakuEventCode, shisakuBukaCode, shisakuBlockNo, aStructureResult.OriginalBuhinNo)
                        If eventEbomKanshiVo Is Nothing Then
                            eventBaseVo = Nothing
                            Dim eventVo As New TShisakuEventVo
                            eventVo = makeDao.FindByEvent(shisakuEventCode)
                            aKaihatsuFugo = eventVo.ShisakuKaihatsuFugo
                        Else
                            VoUtil.CopyProperties(eventEbomKanshiVo, eventBaseVo)
                            aKaihatsuFugo = eventBaseVo.BaseKaihatsuFugo
                        End If
                    End If

                    '↑↑2014/09/25 酒井 ADD END
                ElseIf a0553flag = 2 Then
                    '子部品展開時の処理'
                    'イベント情報から開発符号を取得する'
                    Dim eventVo As New TShisakuEventVo
                    eventVo = makeDao.FindByEvent(shisakuEventCode)
                    aKaihatsuFugo = eventVo.ShisakuKaihatsuFugo
                Else
                    '構成再展開、最新化、構成呼び出し時の処理'
                    '指定された開発符号を使用する'
                    aKaihatsuFugo = KaihatsuFugo

                    If StringUtil.IsEmpty(aKaihatsuFugo) Then
                        '↓↓2014/09/25 酒井 ADD BEGIN
                        If Not yakanFlg Then
                            eventBaseVo = makeDao.FindByInstlBase(shisakuEventCode, shisakuBukaCode, shisakuBlockNo, aStructureResult.OriginalBuhinNo)

                            If eventBaseVo Is Nothing Then
                                Dim eventVo As New TShisakuEventVo
                                eventVo = makeDao.FindByKaihatsuFugo(shisakuEventCode)
                                aKaihatsuFugo = eventVo.ShisakuKaihatsuFugo
                            Else
                                aKaihatsuFugo = eventBaseVo.BaseKaihatsuFugo
                            End If
                        Else
                            Dim eventEbomKanshiVo As TShisakuEventEbomKanshiVo = makeDao.FindByInstlEbomKanshi(shisakuEventCode, shisakuBukaCode, shisakuBlockNo, aStructureResult.OriginalBuhinNo)

                            If eventEbomKanshiVo Is Nothing Then
                                eventBaseVo = Nothing
                                Dim eventVo As New TShisakuEventVo
                                eventVo = makeDao.FindByKaihatsuFugo(shisakuEventCode)
                                aKaihatsuFugo = eventVo.ShisakuKaihatsuFugo
                            Else
                                VoUtil.CopyProperties(eventEbomKanshiVo, eventBaseVo)
                                aKaihatsuFugo = eventBaseVo.BaseKaihatsuFugo
                            End If

                        End If
                        '↑↑2014/09/25 酒井 ADD END
                End If
                End If

                checkVo = makeDao.FindByBuhinRhac0553(aKaihatsuFugo, inputedBuhinNo)
                If Not checkVo Is Nothing Then
                    '↓↓2014/09/25 酒井 ADD BEGIN
                    'Return BuhinKousei(inputedBuhinNo, aStructureResult, baseLevel, aKaihatsuFugo)
                    Return BuhinKousei(inputedBuhinNo, aStructureResult, baseLevel, aKaihatsuFugo, yakanFlg)
                    '↑↑2014/09/25 酒井 ADD END

                Else
                    '553にも無ければ551を見に行く'
                    Dim checkRhac0551Vo As New Rhac0551Vo
                    checkRhac0551Vo = makeDao.FindByBuhinRhac0551(inputedBuhinNo)
                    '↓↓2014/09/25 酒井 ADD BEGIN
                    '                    Return BuhinKouseiRhac0551(inputedBuhinNo, aStructureResult, baseLevel)
                    Return BuhinKouseiRhac0551(inputedBuhinNo, aStructureResult, baseLevel, yakanFlg)
                    '↑↑2014/09/25 酒井 ADD END
                End If
            End If
            'ここまで追加'

            '2012/01/25 部品ノート追加
            '532のChukiKijutsuをBuhinNoteとして取得する
            'Dim inputedBuhinNoVo As Rhac0532MakerNameVo = makeDao.FindLastestRhac0532MakerNameByBuhinNo(inputedBuhinNo)
            Dim inputedBuhinNoVo As Rhac0532BuhinNoteVo = makeDao.FindLastestRhac0532MakerNameByBuhinNo(inputedBuhinNo)
            '   Ｊだったらブランクにする。
            If StringUtil.Equals(inputedBuhinNoVo.ShukeiCode, "J") Then
                inputedBuhinNoVo.ShukeiCode = ""    'ブランクをセット。
            End If
            If StringUtil.Equals(inputedBuhinNoVo.SiaShukeiCode, "J") Then
                inputedBuhinNoVo.SiaShukeiCode = ""    'ブランクをセット。
            End If

            ' 入力部品番号を親品番として全構成を取得
            Dim rhac0552Vos As List(Of Rhac0552Vo) = makeDao.FindStructure0552ByBuhinNoOya(inputedBuhinNo)

            Dim parentVoForBuhinNo As New Rhac0552Vo

            Dim newKoseiMatrix As New BuhinKoseiMatrix

            ''ここのロジックがあるとテストクラスがエラー'
            ''ここのロジックが無いと設計展開でエラー'
            ''無ければ自身を構成させる'
            'If rhac0552Vos.Count = 0 Then
            '    'どうすれば・・・・？'

            '    '親品番をとりあえず設定'
            '    'parentVoForBuhinNo.BuhinNoOya = inputedBuhinNo

            '    'Rhac0552VoHelper.ResolveRule(rhac0552Vos)
            '    'Rhac0552VoHelper.ResolveRule(parentVoForBuhinNo)

            '    Return Nothing


            '    'newKoseiMatrix(0).BuhinNo = inputedBuhinNo
            '    'newKoseiMatrix(0).BuhinNoHyoujiJun = 0
            '    'newKoseiMatrix(0).Level = 0
            '    'newKoseiMatrix(0).ShukeiCode = "J"
            '    'newKoseiMatrix(0).BuhinName = "テスト用"
            '    'newKoseiMatrix.InsuSuryo(0, 0) = 1

            '    'Return newKoseiMatrix

            '    'Dim isInstlHinban As Boolean = parentVoForBuhinNo Is Nothing

            '    '' 入力部品番号を親品番として取得した全構成の子品番の情報
            '    'Dim rhac0532Vos As List(Of Rhac0532Vo) = makeDao.FindStructure0552ByBuhinNoOyaAnd0532ForKo(inputedBuhinNo)
            '    'rhac0532Vos.Add(inputedBuhinNoVo)

            '    'Dim rhac0610Vos As List(Of Rhac0610Vo) = makeDao.FindStructure0552ByBuhinNoOyaAnd0610ForKo(inputedBuhinNo)

            '    'If Not isInstlHinban Then
            '    '    parentVoForBuhinNo.BuhinNoOya = inputedBuhinNo
            '    '    parentVoForBuhinNo.BuhinNoKo = inputedBuhinNo
            '    '    parentVoForBuhinNo.ShukeiCode = "J"

            '    '    rhac0552Vos.Add(parentVoForBuhinNo)
            '    '    Dim dummy As New Rhac0532Vo
            '    '    dummy.BuhinNo = parentVoForBuhinNo.BuhinNoOya
            '    '    rhac0532Vos.Add(dummy)
            '    'End If

            '    'If ShisakuRule.IsHinbanReplacedColor(aStructureResult.BuhinNo) Then
            '    '    Dim colorCode As String = ShisakuRule.GetColorFromHinban(aStructureResult.OriginalBuhinNo)
            '    '    ' ↑これが色
            '    '    ' TODO 11.27本間コメント 時間が足りないので調査結果を記す
            '    '    ' rhac0552Vosの中の"##"品番を色に置換える
            '    '    UpdateColor0552(colorCode, rhac0552Vos)

            '    '    ' rhac0532Vosの中の"##"品番を色に置換える
            '    '    UpdateColor0532(colorCode, rhac0532Vos)

            '    '    '上記の、「色置換え」ロジックは、ShisakuRuleクラスにメソッドとして持たせるのがベター
            '    '    ' ReplaceColor(hinban As String, replaceColor As String) As String
            '    '    'こんなメソッドで。

            '    '    'それと、当メソッドをコピペして作られた、ComputeNotJikyu()も同様の修正が必要。
            '    '    ' 二重ロジック止めて欲しい。
            '    'End If

            '    'Dim singleMatrices As BuhinSingleMatrix(Of Rhac0552Vo, Rhac0532Vo)() = BuhinTreeMaker.NewSingleMatrices(RemoveNonShukei(rhac0552Vos), rhac0532Vos)
            '    'If singleMatrices.Length <> 1 Then

            '    '    Throw New InvalidProgramException("SQLで1品番の構成だけ取って来ているのに NewSingleMatrices で構成が " & singleMatrices.Length & " 個")
            '    'End If

            '    'Dim mergeNode As New MergeNodeList(Of Rhac0552Vo, Rhac0532Vo)(New RhacNodeAccessor(New MakerNameResolverImpl(rhac0610Vos, inputedBuhinNoVo)))
            '    'mergeNode.Compute(singleMatrices(0).Nodes)

            '    'newKoseiMatrix = mergeNode.ResultMatrix
            '    ''Dim newKoseiMatrix As BuhinKoseiMatrix = mergeNode.ResultMatrix
            '    'If Not isInstlHinban Then
            '    '    newKoseiMatrix.RemoveRow(0) ' parentVoForBuhinNo.BuhinNoOya が先頭にいるから削除しておく
            '    'End If
            '    'newKoseiMatrix.CorrectLevelBy(baseLevel)



            'Else

            '構成がナッシング（Count＝０）の場合、親品番を再取得 BY 柳沼
            If rhac0552Vos.Count = 0 Then
                rhac0552Vos = makeDao.FindByBuhinRhac0552LIST(inputedBuhinNo)
            End If

            ' 入力部品番号を子品番として直親のみの構成を取得
            parentVoForBuhinNo = makeDao.FindLastestRhac0552ByBuhinNoKo(inputedBuhinNo)
            '親部品の集計コードを取得し、入れ替える。
            If Not StringUtil.IsEmpty(parentVoForBuhinNo) Then
                Dim ShuKeiCD As Rhac0532Vo = makeDao.FindLastestRhac0532ByBuhinNoOya(inputedBuhinNo)

                '   Ｊだったらブランクにする。
                If StringUtil.Equals(ShuKeiCD.ShukeiCode, "J") Then
                    parentVoForBuhinNo.ShukeiCode = ""    'ブランクをセット。
                Else
                    parentVoForBuhinNo.ShukeiCode = ShuKeiCD.ShukeiCode '国内集計コード
                End If
                If StringUtil.Equals(ShuKeiCD.SiaShukeiCode, "J") Then
                    parentVoForBuhinNo.SiaShukeiCode = ""    'ブランクをセット。
                Else
                    parentVoForBuhinNo.SiaShukeiCode = ShuKeiCD.SiaShukeiCode '海外集計コード
                End If

            End If

            Rhac0552VoHelper.ResolveRule(rhac0552Vos)
            Rhac0552VoHelper.ResolveRule(parentVoForBuhinNo)

            Dim isInstlHinban As Boolean = parentVoForBuhinNo Is Nothing

            ' 入力部品番号を親品番として取得した全構成の子品番の情報
            Dim rhac0532Vos As List(Of Rhac0532Vo) = makeDao.FindStructure0552ByBuhinNoOyaAnd0532ForKo(inputedBuhinNo)
            rhac0532Vos.Add(inputedBuhinNoVo)

            Dim rhac0610Vos As List(Of Rhac0610Vo) = makeDao.FindStructure0552ByBuhinNoOyaAnd0610ForKo(inputedBuhinNo)

            If Not isInstlHinban Then
                rhac0552Vos.Add(parentVoForBuhinNo)
                Dim dummy As New Rhac0532Vo
                dummy.BuhinNo = parentVoForBuhinNo.BuhinNoOya
                rhac0532Vos.Add(dummy)
            End If

            Dim colorA As String = ""
            If rhac0552Vos(0).BuhinNoOya.Length = 12 Then
                '↓↓2014/09/25 酒井 ADD BEGIN
                'colorA = GetBuhinColor(rhac0552Vos(0).BuhinNoOya, 0)
                colorA = GetBuhinColor(rhac0552Vos(0).BuhinNoOya, 0, "", yakanFlg)
                '↑↑2014/09/25 酒井 ADD END
            Else
                colorA = ""
            End If
            '12桁未満ならNothingが返ってくる'
            Dim colorCode As String = ShisakuRule.GetColorFromHinban(aStructureResult.OriginalBuhinNo)
            If StringUtil.IsEmpty(colorCode) Then
                colorCode = ""
            End If

            '基準の色が同じか、基準の色が存在しない場合構成に色を振る'
            If StringUtil.Equals(colorCode, colorA) OrElse StringUtil.IsEmpty(colorCode) Then
                For Each Vo As Rhac0552Vo In rhac0552Vos
                    '部品番号(親に色を付加)'
                    If Vo.BuhinNoOya.Length = 12 AndAlso StringUtil.Equals(Right(Vo.BuhinNoOya, 2), "##") Then
                        '↓↓2014/09/25 酒井 ADD BEGIN
                        'Dim color As String = GetBuhinColor(Vo.BuhinNoOya, 0)
                        Dim color As String = GetBuhinColor(Vo.BuhinNoOya, 0, "", yakanFlg)
                        '↑↑2014/09/25 酒井 ADD END
                        If StringUtil.IsEmpty(color) Then
                            If Not StringUtil.IsEmpty(colorCode) Then
                                '自身の色が取れなかった場合は親の色を振る'
                                Vo.BuhinNoOya = Left(Vo.BuhinNoOya, 10) + colorCode
                            End If
                        Else
                            '自身の色が取れた場合は自身の色を振る'
                            Vo.BuhinNoOya = Left(Vo.BuhinNoOya, 10) + color
                        End If
                    End If
                    '部品番号(子に色を付加)'
                    If Vo.BuhinNoKo.Length = 12 AndAlso StringUtil.Equals(Right(Vo.BuhinNoKo, 2), "##") Then
                        '↓↓2014/09/25 酒井 ADD BEGIN
                        'Dim color As String = GetBuhinColor(Vo.BuhinNoKo, 0)
                        Dim color As String = GetBuhinColor(Vo.BuhinNoKo, 0, "", yakanFlg)
                        '↑↑2014/09/25 酒井 ADD END
                        If StringUtil.IsEmpty(color) Then
                            If Not StringUtil.IsEmpty(colorCode) Then
                                '自身の色が取れなかった場合は親の色を振る'
                                Vo.BuhinNoKo = Left(Vo.BuhinNoKo, 10) + colorCode
                            End If
                        Else
                            '自身の色が取れた場合は自身の色を振る'
                            Vo.BuhinNoKo = Left(Vo.BuhinNoKo, 10) + color
                        End If
                    End If
                Next
                '部品番号に色を付加'
                For Each Vo As Rhac0532Vo In rhac0532Vos
                    If Vo.BuhinNo.Length = 12 AndAlso StringUtil.Equals(Right(Vo.BuhinNo, 2), "##") Then
                        '↓↓2014/09/25 酒井 ADD BEGIN
                        'Dim color As String = GetBuhinColor(Vo.BuhinNo, 0)
                        Dim color As String = GetBuhinColor(Vo.BuhinNo, 0, "", yakanFlg)
                        '↑↑2014/09/25 酒井 ADD END
                        If StringUtil.IsEmpty(color) Then
                            If Not StringUtil.IsEmpty(colorCode) Then
                                '自身の色が取れなかった場合は親の色を振る'
                                Vo.BuhinNo = Left(Vo.BuhinNo, 10) + colorCode
                            End If
                        Else
                            '自身の色が取れた場合は自身の色を振る'
                            Vo.BuhinNo = Left(Vo.BuhinNo, 10) + color
                        End If
                    End If
                Next
            Else
                If Not StringUtil.IsEmpty(colorCode) Then
                    ' rhac0552Vosの中の"##"品番を色に置換える
                    UpdateColor0552(colorCode, rhac0552Vos)

                    ' rhac0532Vosの中の"##"品番を色に置換える
                    UpdateColor0532(colorCode, rhac0532Vos)
                Else

                End If
            End If


            '上記の、「色置換え」ロジックは、ShisakuRuleクラスにメソッドとして持たせるのがベター
            ' ReplaceColor(hinban As String, replaceColor As String) As String
            'こんなメソッドで。

            'それと、当メソッドをコピペして作られた、ComputeNotJikyu()も同様の修正が必要。
            ' 二重ロジック止めて欲しい。
            '2012/03/16 スペースの存在で取れない構成がいるので'
            For Each Vo As Rhac0532Vo In rhac0532Vos
                If Not StringUtil.IsEmpty(Vo.BuhinNo) Then
                    Vo.BuhinNo = Trim(Vo.BuhinNo)
                End If
            Next
            For Each Vo As Rhac0552Vo In rhac0552Vos
                If Not StringUtil.IsEmpty(Vo.BuhinNoOya) Then
                    Vo.BuhinNoOya = Trim(Vo.BuhinNoOya)
                End If
                If Not StringUtil.IsEmpty(Vo.BuhinNoKo) Then
                    Vo.BuhinNoKo = Trim(Vo.BuhinNoKo)
                End If
            Next
            '集計コードブランクを除いて構成を返す。
            ' [RemoveNonShukei]
            'Dim singleMatrices As BuhinSingleMatrix(Of Rhac0552Vo, Rhac0532Vo)() = BuhinTreeMaker.NewSingleMatrices(RemoveNonShukei(rhac0552Vos), rhac0532Vos)
            Dim singleMatrices As BuhinSingleMatrix(Of Rhac0552Vo, Rhac0532Vo)() = BuhinTreeMaker.NewSingleMatrices(rhac0552Vos, rhac0532Vos)

            If singleMatrices.Length <> 1 Then
                Throw New InvalidProgramException("SQLで1品番の構成だけ取って来ているのに NewSingleMatrices で構成が " & singleMatrices.Length & " 個")
            End If

            Dim mergeNode As New MergeNodeList(Of Rhac0552Vo, Rhac0532Vo)(New RhacNodeAccessor(New MakerNameResolverImpl(rhac0610Vos, inputedBuhinNoVo)))

            mergeNode.Compute(singleMatrices(0).Nodes)

            newKoseiMatrix = mergeNode.ResultMatrix
            'Dim newKoseiMatrix As BuhinKoseiMatrix = mergeNode.ResultMatrix
            If Not isInstlHinban Then
                newKoseiMatrix.RemoveRow(0) ' parentVoForBuhinNo.BuhinNoOya が先頭にいるから削除しておく
            End If
            '集計コードがＲの子部品の構成情報を除外する。
            RemoveNonRbuhin(newKoseiMatrix)

            '############################################################################
            '2012/01/12 現状：購担取引先の処理時間が掛かりすぎ（無駄が多い）
            '　　　　　 結論：実装方法を考え直す必要有り
            '　　　　　 対策：現状は、非常に遅いので実装方法が確定するまでは、
            '　　　　　 　　　コメントアウトとする
            '############################################################################
            'For Each index As Integer In newKoseiMatrix.GetInputRowIndexes
            '    If Not StringUtil.IsEmpty(newKoseiMatrix(index).BuhinNo) Then
            '        If StringUtil.IsEmpty(newKoseiMatrix(index).MakerCode) And StringUtil.IsEmpty(newKoseiMatrix(index).MakerName) Then
            '            Dim MakerVo As New TShisakuBuhinEditVo
            '            Dim impl As ShisakuBuhinMenu.Dao. = New ShisakuBuhinMenu.Dao.BuhinEditBaseDaoImpl
            'MakerVo = BuhinEditBaseDaoimpl.FindByKoutanTorihikisaki(newKoseiMatrix(index).BuhinNo)
            '            newKoseiMatrix(index).MakerCode = MakerVo.MakerCode
            '            newKoseiMatrix(index).MakerName = MakerVo.MakerName
            '        End If
            '    Else
            '        '部品情報が無い場合、削除してみる。※ブランクデータが出来ちゃっているので。
            '        newKoseiMatrix.RemoveRow(index)
            '    End If
            'Next

            '余分な行が存在した場合削除する'
            For Each index As Integer In newKoseiMatrix.GetInputRowIndexes
                If index < 0 Then
                    newKoseiMatrix.RemoveRow(index)
                ElseIf StringUtil.IsEmpty(newKoseiMatrix(index).BuhinNo) Then
                    newKoseiMatrix.RemoveRow(index)
                End If
            Next

            '余分な行を削除した後で取引先コードと名称を取得する'
            For Each index As Integer In newKoseiMatrix.GetInputRowIndexes
                If StringUtil.IsEmpty(newKoseiMatrix(index).MakerCode) And StringUtil.IsEmpty(newKoseiMatrix(index).MakerName) Then
                    If Not StringUtil.IsEmpty(newKoseiMatrix(index).BuhinNo) Then
                        Dim MakerVo As New TShisakuBuhinEditVo
                        Dim impl As ShisakuBuhinMenu.Dao.BuhinEditBaseDao = New ShisakuBuhinMenu.Dao.BuhinEditBaseDaoImpl
                        '/*** 20140911 CHANGE START（引数追加） ***/
                        'MakerVo = impl.FindByKoutanTorihikisaki(newKoseiMatrix(index).BuhinNo)
                        MakerVo = impl.FindByKoutanTorihikisakiUseDictionary(newKoseiMatrix(index).BuhinNo, kotanTorihikisakiSelected)
                        '/*** 20140911 CHANGE END ***/
                        newKoseiMatrix(index).MakerCode = MakerVo.MakerCode
                        newKoseiMatrix(index).MakerName = MakerVo.MakerName
                    End If

                End If
                'If Not StringUtil.IsEmpty(newKoseiMatrix(index).BuhinNo) Then
                'Else
                '    '部品情報が無い場合、削除してみる。※ブランクデータが出来ちゃっているので。
                '    newKoseiMatrix.RemoveRow(index)
                'End If
            Next


            '供給セクションを振る'
            If Not newKoseiMatrix Is Nothing Then
                For Each index As Integer In newKoseiMatrix.GetInputRowIndexes
                    Dim BuhinNo As String = ""
                    If Not StringUtil.IsEmpty(newKoseiMatrix(index).BuhinNo) Then

                        '集計コードを確認する'
                        If StringUtil.IsEmpty(newKoseiMatrix(index).ShukeiCode) Then
                            '海外集計'
                            If StringUtil.Equals(newKoseiMatrix(index).SiaShukeiCode, "X") Or StringUtil.Equals(newKoseiMatrix(index).SiaShukeiCode, "R") Or StringUtil.Equals(newKoseiMatrix(index).SiaShukeiCode, "J") Then
                                '供給セクションは空'
                            ElseIf StringUtil.Equals(newKoseiMatrix(index).SiaShukeiCode, "A") Then
                                '供給セクション「09SH10」固定'
                                newKoseiMatrix(index).KyoukuSection = "9SH10"
                            ElseIf StringUtil.Equals(newKoseiMatrix(index).SiaShukeiCode, "E") Or StringUtil.Equals(newKoseiMatrix(index).SiaShukeiCode, "Y") Then
                                '親の供給セクションを取得する'
                                BuhinNo = newKoseiMatrix(index).BuhinNo
                                newKoseiMatrix = SetKyoukuSection0552(BuhinNo, BuhinNo, rhac0552Vos, newKoseiMatrix)
                            End If
                        Else
                            '国内集計'
                            If StringUtil.Equals(newKoseiMatrix(index).ShukeiCode, "X") Or StringUtil.Equals(newKoseiMatrix(index).ShukeiCode, "R") Or StringUtil.Equals(newKoseiMatrix(index).ShukeiCode, "J") Then
                                '供給セクションは空'
                            ElseIf StringUtil.Equals(newKoseiMatrix(index).ShukeiCode, "A") Then
                                '供給セクション「09SH10」固定'
                                newKoseiMatrix(index).KyoukuSection = "9SH10"
                            ElseIf StringUtil.Equals(newKoseiMatrix(index).ShukeiCode, "E") Or StringUtil.Equals(newKoseiMatrix(index).ShukeiCode, "Y") Then
                                '親の供給セクションを取得する'
                                BuhinNo = newKoseiMatrix(index).BuhinNo
                                newKoseiMatrix = SetKyoukuSection0552(BuhinNo, BuhinNo, rhac0552Vos, newKoseiMatrix)
                            End If
                        End If
                    End If
                Next
            End If

            '全体のレベルを再調整する。
            newKoseiMatrix.CorrectLevelBy(baseLevel)

            'ソート処理'
            'newKoseiMatrix = SortMatrix(newKoseiMatrix)

            'ここで圧縮処理をしてみる'

            'newKoseiMatrix = MergeMatrix(newKoseiMatrix)
            Return newKoseiMatrix

        End Function
        ''' <summary>
        ''' 「構成の情報」を元に部品表を作成する(自給品有り)
        ''' </summary>
        ''' <param name="aStructureResult">構成の情報</param>
        ''' <param name="baseLevel">基点のレベル</param>
        ''' <returns>部品表</returns>
        ''' <remarks></remarks>
        Public Function Compute2(ByVal aStructureResult As StructureResult, ByVal a0553flag As Integer, ByVal baseLevel As Integer?) As BuhinKoseiMatrix Implements MakeStructureResult.Compute2
            Dim target As String
            Dim KaihatsuFugo As String
            If Left(aStructureResult.YobidashiMoto, 4) = "0533" Then
                target = "0533"
                KaihatsuFugo = Split(aStructureResult.YobidashiMoto, "-")(1)
            Else
                If aStructureResult.YobidashiMoto = "0532" Then
                    target = "0532"
                    KaihatsuFugo = ""
                ElseIf aStructureResult.YobidashiMoto = "0530" Then
                    target = "0530"
                    KaihatsuFugo = ""
                Else
                    target = "SHISAKU"
                    KaihatsuFugo = ""
                End If
            End If

            oriBuhinNo = aStructureResult.OriginalBuhinNo
            bfBuhinNo = aStructureResult.BuhinNo


            If Not aStructureResult.IsExist Then
                Return New BuhinKoseiMatrix
            End If


            If Not aStructureResult.IsEBom Then

                If aStructureResult.EditVo IsNot Nothing Then
                    ' 5keyで最小のINSTL品番表示順のBuhinEditInstl

                    Dim editInstlVo As TShisakuBuhinEditInstlVo = FindMinInstlHinbanHyoujiJunEditInstlBy(aStructureResult.EditVo)

                    Dim editInstlVos As List(Of TShisakuBuhinEditInstlVo) = FindEditInstlByInstlHinbanHyoujiJun(editInstlVo)

                    '自給品を含めて取得する。
                    Dim editVos As List(Of TShisakuBuhinEditVo) = FindEditBy(editInstlVo)

                    Dim result As BuhinKoseiMatrix = BuhinKoseiMatrix.NewInnerJoinHyoujiJun(editVos, editInstlVos)
                    result = result.ExtractUnderLevel(aStructureResult.EditVo.BuhinNoHyoujiJun)
                    result.RemoveNullRecords()
                    result.CorrectLevelBy(baseLevel)
                    Return result

                ElseIf aStructureResult.InstlVo IsNot Nothing Then
                    ' 
                    Dim editInstlVos As List(Of TShisakuBuhinEditInstlVo) = FindEditInstlByInstlHinbanHyoujiJun(aStructureResult.InstlVo)

                    '自給品を含めて取得する。
                    Dim editVos As List(Of TShisakuBuhinEditVo) = FindEditBy(aStructureResult.InstlVo)

                    Dim result As BuhinKoseiMatrix = BuhinKoseiMatrix.NewInnerJoinHyoujiJun(editVos, editInstlVos)
                    result.RemoveNullRecords()
                    result.CorrectLevelBy(baseLevel)
                    If result.Records.Count = 0 Then
                        Dim editInstlVos2 As New List(Of TShisakuBuhinEditInstlVo)
                        Dim dummyEditInstlVos2 As New TShisakuBuhinEditInstlVo
                        'dummyEditInstlVos2.InstlHinbanHyoujiJun = aStructureResult.InstlVo.InstlHinbanHyoujiJun
                        dummyEditInstlVos2.InstlHinbanHyoujiJun = 0
                        dummyEditInstlVos2.BuhinNoHyoujiJun = 0
                        dummyEditInstlVos2.InsuSuryo = 1
                        editInstlVos2.Add(dummyEditInstlVos2)

                        Dim editVos2 As New List(Of TShisakuBuhinEditVo)
                        Dim dummyEditVos2 As New TShisakuBuhinEditVo
                        dummyEditVos2.BuhinNo = aStructureResult.InstlVo.InstlHinban
                        dummyEditVos2.BuhinNoKbn = aStructureResult.InstlVo.InstlHinbanKbn
                        dummyEditVos2.Level = 0

                        dummyEditVos2.BuhinNoHyoujiJun = 0
                        editVos2.Add(dummyEditVos2)

                        Dim result2 As BuhinKoseiMatrix = BuhinKoseiMatrix.NewInnerJoinHyoujiJun(editVos2, editInstlVos2)

                        result2.RemoveNullRecords()
                        result2.CorrectLevelBy(baseLevel)
                        Return result2
                    End If
                    Return result
                End If
                Throw New ArgumentException("プロパティの内容が不正", "aStructureResult")
            End If

            Dim inputedBuhinNo As String = aStructureResult.BuhinNo

            'ここから追加 樺澤 2010/12/7 '
            'ベース車情報の開発符号毎に0532を見るのか0533を見るのか分けたい'
            Dim Rhac0552Vo As New Rhac0552Vo

            Rhac0552Vo = makeDao.FindByBuhinRhac0552(inputedBuhinNo)



            '2012/02/02 '
            'INSTL品番を見に行く時にはオリジナル品番で見に行くようにする。


            '0553注記：設計展開時はベース車情報の開発符号を使用'
            '構成再展開、最新化、構成呼び出しは画面側から取得した開発符号を使用'
            '子部品展開はイベント情報の開発符号を使用する'


            '構成再展開、最新化、構成呼び出し時の処理'
            Select Case target
                Case "0530"
                    '過去データ
                    Return BuhinKouseiRhac0551(inputedBuhinNo, aStructureResult, baseLevel)
                Case "0533"
                    '開発管理表
                    Return BuhinKousei(inputedBuhinNo, aStructureResult, baseLevel, KaihatsuFugo)
                Case Else
            End Select


            'ここまで追加'

            '以下は0532の場合のロジック*******************

            '2012/01/25 部品ノート追加
            '532のChukiKijutsuをBuhinNoteとして取得する
            'Dim inputedBuhinNoVo As Rhac0532MakerNameVo = makeDao.FindLastestRhac0532MakerNameByBuhinNo(inputedBuhinNo)
            Dim inputedBuhinNoVo As Rhac0532BuhinNoteVo = makeDao.FindLastestRhac0532MakerNameByBuhinNo(inputedBuhinNo)

            ' 入力部品番号を親品番として全構成を取得
            Dim rhac0552Vos As List(Of Rhac0552Vo) = makeDao.FindStructure0552ByBuhinNoOya(inputedBuhinNo)

            Dim parentVoForBuhinNo As New Rhac0552Vo

            Dim newKoseiMatrix As New BuhinKoseiMatrix

            ''ここのロジックがあるとテストクラスがエラー'
            ''ここのロジックが無いと設計展開でエラー'
            ''無ければ自身を構成させる'
            'If rhac0552Vos.Count = 0 Then
            '    'どうすれば・・・・？'

            '    '親品番をとりあえず設定'
            '    'parentVoForBuhinNo.BuhinNoOya = inputedBuhinNo

            '    'Rhac0552VoHelper.ResolveRule(rhac0552Vos)
            '    'Rhac0552VoHelper.ResolveRule(parentVoForBuhinNo)

            '    Return Nothing


            '    'newKoseiMatrix(0).BuhinNo = inputedBuhinNo
            '    'newKoseiMatrix(0).BuhinNoHyoujiJun = 0
            '    'newKoseiMatrix(0).Level = 0
            '    'newKoseiMatrix(0).ShukeiCode = "J"
            '    'newKoseiMatrix(0).BuhinName = "テスト用"
            '    'newKoseiMatrix.InsuSuryo(0, 0) = 1

            '    'Return newKoseiMatrix

            '    'Dim isInstlHinban As Boolean = parentVoForBuhinNo Is Nothing

            '    '' 入力部品番号を親品番として取得した全構成の子品番の情報
            '    'Dim rhac0532Vos As List(Of Rhac0532Vo) = makeDao.FindStructure0552ByBuhinNoOyaAnd0532ForKo(inputedBuhinNo)
            '    'rhac0532Vos.Add(inputedBuhinNoVo)

            '    'Dim rhac0610Vos As List(Of Rhac0610Vo) = makeDao.FindStructure0552ByBuhinNoOyaAnd0610ForKo(inputedBuhinNo)

            '    'If Not isInstlHinban Then
            '    '    parentVoForBuhinNo.BuhinNoOya = inputedBuhinNo
            '    '    parentVoForBuhinNo.BuhinNoKo = inputedBuhinNo
            '    '    parentVoForBuhinNo.ShukeiCode = "J"

            '    '    rhac0552Vos.Add(parentVoForBuhinNo)
            '    '    Dim dummy As New Rhac0532Vo
            '    '    dummy.BuhinNo = parentVoForBuhinNo.BuhinNoOya
            '    '    rhac0532Vos.Add(dummy)
            '    'End If

            '    'If ShisakuRule.IsHinbanReplacedColor(aStructureResult.BuhinNo) Then
            '    '    Dim colorCode As String = ShisakuRule.GetColorFromHinban(aStructureResult.OriginalBuhinNo)
            '    '    ' ↑これが色
            '    '    ' TODO 11.27本間コメント 時間が足りないので調査結果を記す
            '    '    ' rhac0552Vosの中の"##"品番を色に置換える
            '    '    UpdateColor0552(colorCode, rhac0552Vos)

            '    '    ' rhac0532Vosの中の"##"品番を色に置換える
            '    '    UpdateColor0532(colorCode, rhac0532Vos)

            '    '    '上記の、「色置換え」ロジックは、ShisakuRuleクラスにメソッドとして持たせるのがベター
            '    '    ' ReplaceColor(hinban As String, replaceColor As String) As String
            '    '    'こんなメソッドで。

            '    '    'それと、当メソッドをコピペして作られた、ComputeNotJikyu()も同様の修正が必要。
            '    '    ' 二重ロジック止めて欲しい。
            '    'End If

            '    'Dim singleMatrices As BuhinSingleMatrix(Of Rhac0552Vo, Rhac0532Vo)() = BuhinTreeMaker.NewSingleMatrices(RemoveNonShukei(rhac0552Vos), rhac0532Vos)
            '    'If singleMatrices.Length <> 1 Then

            '    '    Throw New InvalidProgramException("SQLで1品番の構成だけ取って来ているのに NewSingleMatrices で構成が " & singleMatrices.Length & " 個")
            '    'End If

            '    'Dim mergeNode As New MergeNodeList(Of Rhac0552Vo, Rhac0532Vo)(New RhacNodeAccessor(New MakerNameResolverImpl(rhac0610Vos, inputedBuhinNoVo)))
            '    'mergeNode.Compute(singleMatrices(0).Nodes)

            '    'newKoseiMatrix = mergeNode.ResultMatrix
            '    ''Dim newKoseiMatrix As BuhinKoseiMatrix = mergeNode.ResultMatrix
            '    'If Not isInstlHinban Then
            '    '    newKoseiMatrix.RemoveRow(0) ' parentVoForBuhinNo.BuhinNoOya が先頭にいるから削除しておく
            '    'End If
            '    'newKoseiMatrix.CorrectLevelBy(baseLevel)



            'Else

            '構成がナッシング（Count＝０）の場合、親品番を再取得 BY 柳沼
            If rhac0552Vos.Count = 0 Then
                rhac0552Vos = makeDao.FindByBuhinRhac0552LIST(inputedBuhinNo)
            End If

            ' 入力部品番号を子品番として直親のみの構成を取得
            parentVoForBuhinNo = makeDao.FindLastestRhac0552ByBuhinNoKo(inputedBuhinNo)
            '親部品の集計コードを取得し、入れ替える。
            If Not StringUtil.IsEmpty(parentVoForBuhinNo) Then
                Dim ShuKeiCD As Rhac0532Vo = makeDao.FindLastestRhac0532ByBuhinNoOya(inputedBuhinNo)
                parentVoForBuhinNo.ShukeiCode = ShuKeiCD.ShukeiCode '国内集計コード
                parentVoForBuhinNo.SiaShukeiCode = ShuKeiCD.SiaShukeiCode '海外集計コード
            End If

            Rhac0552VoHelper.ResolveRule(rhac0552Vos)
            Rhac0552VoHelper.ResolveRule(parentVoForBuhinNo)

            Dim isInstlHinban As Boolean = parentVoForBuhinNo Is Nothing

            ' 入力部品番号を親品番として取得した全構成の子品番の情報
            Dim rhac0532Vos As List(Of Rhac0532Vo) = makeDao.FindStructure0552ByBuhinNoOyaAnd0532ForKo(inputedBuhinNo)
            rhac0532Vos.Add(inputedBuhinNoVo)


            Dim rhac0610Vos As List(Of Rhac0610Vo) = makeDao.FindStructure0552ByBuhinNoOyaAnd0610ForKo(inputedBuhinNo)

            If Not isInstlHinban Then
                rhac0552Vos.Add(parentVoForBuhinNo)
                Dim dummy As New Rhac0532Vo
                dummy.BuhinNo = parentVoForBuhinNo.BuhinNoOya
                rhac0532Vos.Add(dummy)
            End If

            '2012/02/27 子供の色は問答無用で振る'
            'If ShisakuRule.IsHinbanReplacedColor(aStructureResult.BuhinNo) Then

            Dim colorA As String = ""

            If rhac0552Vos(0).BuhinNoOya.Length = 12 Then
                colorA = GetBuhinColor(rhac0552Vos(0).BuhinNoOya, 0)
            Else
                colorA = ""
            End If

            '12桁未満ならNothingが返ってくる'
            Dim colorCode As String = ShisakuRule.GetColorFromHinban(aStructureResult.OriginalBuhinNo)

            If StringUtil.IsEmpty(colorCode) Then
                colorCode = ""
            End If

            '基準の色が同じなら、基準の色が存在しない場合'
            If StringUtil.Equals(colorCode, colorA) OrElse StringUtil.IsEmpty(colorCode) Then
                For Each Vo As Rhac0552Vo In rhac0552Vos
                    If Vo.BuhinNoOya.Length = 12 AndAlso StringUtil.Equals(Right(Vo.BuhinNoOya, 2), "##") Then
                        Dim color As String = GetBuhinColor(Vo.BuhinNoOya, 0)
                        If StringUtil.IsEmpty(color) Then
                            If Not StringUtil.IsEmpty(colorCode) Then
                                '自身の色が取れなかった場合は親の色を振る'
                                Vo.BuhinNoOya = Left(Vo.BuhinNoOya, 10) + colorCode
                            End If
                        Else
                            '自身の色が取れた場合は自身の色を振る'
                            Vo.BuhinNoOya = Left(Vo.BuhinNoOya, 10) + color
                        End If

                    End If
                    If Vo.BuhinNoKo.Length = 12 AndAlso StringUtil.Equals(Right(Vo.BuhinNoKo, 2), "##") Then
                        Dim color As String = GetBuhinColor(Vo.BuhinNoKo, 0)
                        If StringUtil.IsEmpty(color) Then
                            If Not StringUtil.IsEmpty(colorCode) Then
                                '自身の色が取れなかった場合は親の色を振る'
                                Vo.BuhinNoKo = Left(Vo.BuhinNoKo, 10) + colorCode
                            End If
                        Else
                            '自身の色が取れた場合は自身の色を振る'
                            Vo.BuhinNoKo = Left(Vo.BuhinNoKo, 10) + color
                        End If
                    End If
                Next
                For Each Vo As Rhac0532Vo In rhac0532Vos
                    If Vo.BuhinNo.Length = 12 AndAlso StringUtil.Equals(Right(Vo.BuhinNo, 2), "##") Then
                        Dim color As String = GetBuhinColor(Vo.BuhinNo, 0)
                        If StringUtil.IsEmpty(color) Then
                            If Not StringUtil.IsEmpty(colorCode) Then
                                '自身の色が取れなかった場合は親の色を振る'
                                Vo.BuhinNo = Left(Vo.BuhinNo, 10) + colorCode
                            End If
                        Else
                            '自身の色が取れた場合は自身の色を振る'
                            Vo.BuhinNo = Left(Vo.BuhinNo, 10) + color
                        End If
                    End If
                Next
            Else
                If Not StringUtil.IsEmpty(colorCode) Then
                    ' rhac0552Vosの中の"##"品番を色に置換える
                    UpdateColor0552(colorCode, rhac0552Vos)

                    ' rhac0532Vosの中の"##"品番を色に置換える
                    UpdateColor0532(colorCode, rhac0532Vos)
                End If
            End If

            ' ↑これが色
            ' TODO 11.27本間コメント 時間が足りないので調査結果を記す
            ' rhac0552Vosの中の"##"品番を色に置換える

            'UpdateColor0552(colorCode, rhac0552Vos)

            ' rhac0532Vosの中の"##"品番を色に置換える
            'UpdateColor0532(colorCode, rhac0532Vos)

            '上記の、「色置換え」ロジックは、ShisakuRuleクラスにメソッドとして持たせるのがベター
            ' ReplaceColor(hinban As String, replaceColor As String) As String
            'こんなメソッドで。

            'それと、当メソッドをコピペして作られた、ComputeNotJikyu()も同様の修正が必要。
            ' 二重ロジック止めて欲しい。
            'End If
            '2012/03/16 スペースの存在で取れない構成がいるので'
            For Each Vo As Rhac0532Vo In rhac0532Vos
                If Not StringUtil.IsEmpty(Vo.BuhinNo) Then
                    Vo.BuhinNo = Trim(Vo.BuhinNo)
                End If
            Next
            For Each Vo As Rhac0552Vo In rhac0552Vos
                If Not StringUtil.IsEmpty(Vo.BuhinNoOya) Then
                    Vo.BuhinNoOya = Trim(Vo.BuhinNoOya)
                End If
                If Not StringUtil.IsEmpty(Vo.BuhinNoKo) Then
                    Vo.BuhinNoKo = Trim(Vo.BuhinNoKo)
                End If
            Next

            '集計コードブランクを除いて構成を返す。
            ' [RemoveNonShukei]
            'Dim singleMatrices As BuhinSingleMatrix(Of Rhac0552Vo, Rhac0532Vo)() = BuhinTreeMaker.NewSingleMatrices(RemoveNonShukei(rhac0552Vos), rhac0532Vos)
            Dim singleMatrices As BuhinSingleMatrix(Of Rhac0552Vo, Rhac0532Vo)() = BuhinTreeMaker.NewSingleMatrices(rhac0552Vos, rhac0532Vos)

            If singleMatrices.Length <> 1 Then
                Throw New InvalidProgramException("SQLで1品番の構成だけ取って来ているのに NewSingleMatrices で構成が " & singleMatrices.Length & " 個")
            End If

            Dim mergeNode As New MergeNodeList(Of Rhac0552Vo, Rhac0532Vo)(New RhacNodeAccessor(New MakerNameResolverImpl(rhac0610Vos, inputedBuhinNoVo)))

            mergeNode.Compute(singleMatrices(0).Nodes)

            newKoseiMatrix = mergeNode.ResultMatrix
            'Dim newKoseiMatrix As BuhinKoseiMatrix = mergeNode.ResultMatrix
            If Not isInstlHinban Then
                newKoseiMatrix.RemoveRow(0) ' parentVoForBuhinNo.BuhinNoOya が先頭にいるから削除しておく
            End If
            '集計コードがＲの子部品の構成情報を除外する。
            RemoveNonRbuhin(newKoseiMatrix)

            '############################################################################
            '2012/01/12 現状：購担取引先の処理時間が掛かりすぎ（無駄が多い）
            '　　　　　 結論：実装方法を考え直す必要有り
            '　　　　　 対策：現状は、非常に遅いので実装方法が確定するまでは、
            '　　　　　 　　　コメントアウトとする
            '############################################################################
            'For Each index As Integer In newKoseiMatrix.GetInputRowIndexes
            '    If Not StringUtil.IsEmpty(newKoseiMatrix(index).BuhinNo) Then
            '        If StringUtil.IsEmpty(newKoseiMatrix(index).MakerCode) And StringUtil.IsEmpty(newKoseiMatrix(index).MakerName) Then
            '            Dim MakerVo As New TShisakuBuhinEditVo
            '            Dim impl As ShisakuBuhinMenu.Dao. = New ShisakuBuhinMenu.Dao.BuhinEditBaseDaoImpl
            'MakerVo = BuhinEditBaseDaoimpl.FindByKoutanTorihikisaki(newKoseiMatrix(index).BuhinNo)
            '            newKoseiMatrix(index).MakerCode = MakerVo.MakerCode
            '            newKoseiMatrix(index).MakerName = MakerVo.MakerName
            '        End If
            '    Else
            '        '部品情報が無い場合、削除してみる。※ブランクデータが出来ちゃっているので。
            '        newKoseiMatrix.RemoveRow(index)
            '    End If
            'Next

            '余分な行が存在した場合削除する'
            For Each index As Integer In newKoseiMatrix.GetInputRowIndexes
                If index < 0 Then
                    newKoseiMatrix.RemoveRow(index)
                ElseIf StringUtil.IsEmpty(newKoseiMatrix(index).BuhinNo) Then
                    newKoseiMatrix.RemoveRow(index)
                End If
            Next

            '余分な行を削除した後で取引先コードと名称を取得する'
            For Each index As Integer In newKoseiMatrix.GetInputRowIndexes
                If StringUtil.IsEmpty(newKoseiMatrix(index).MakerCode) And StringUtil.IsEmpty(newKoseiMatrix(index).MakerName) Then
                    If Not StringUtil.IsEmpty(newKoseiMatrix(index).BuhinNo) Then
                        Dim MakerVo As New TShisakuBuhinEditVo
                        Dim impl As ShisakuBuhinMenu.Dao.BuhinEditBaseDao = New ShisakuBuhinMenu.Dao.BuhinEditBaseDaoImpl
                        '/*** 20140911 CHANGE START（引数追加） ***/
                        'MakerVo = impl.FindByKoutanTorihikisaki(newKoseiMatrix(index).BuhinNo)
                        MakerVo = impl.FindByKoutanTorihikisakiUseDictionary(newKoseiMatrix(index).BuhinNo, kotanTorihikisakiSelected)
                        '/*** 20140911 CHANGE END ***/
                        
                        newKoseiMatrix(index).MakerCode = MakerVo.MakerCode
                        newKoseiMatrix(index).MakerName = MakerVo.MakerName
                    End If

                End If
                'If Not StringUtil.IsEmpty(newKoseiMatrix(index).BuhinNo) Then
                'Else
                '    '部品情報が無い場合、削除してみる。※ブランクデータが出来ちゃっているので。
                '    newKoseiMatrix.RemoveRow(index)
                'End If
            Next


            '供給セクションを振る'
            If Not newKoseiMatrix Is Nothing Then
                For Each index As Integer In newKoseiMatrix.GetInputRowIndexes
                    Dim BuhinNo As String = ""
                    If Not StringUtil.IsEmpty(newKoseiMatrix(index).BuhinNo) Then

                        '集計コードを確認する'
                        If StringUtil.IsEmpty(newKoseiMatrix(index).ShukeiCode) Then
                            '海外集計'
                            If StringUtil.Equals(newKoseiMatrix(index).SiaShukeiCode, "X") Or StringUtil.Equals(newKoseiMatrix(index).SiaShukeiCode, "R") Or StringUtil.Equals(newKoseiMatrix(index).SiaShukeiCode, "J") Then
                                '供給セクションは空'
                            ElseIf StringUtil.Equals(newKoseiMatrix(index).SiaShukeiCode, "A") Then
                                newKoseiMatrix(index).KyoukuSection = "9SH10"
                            ElseIf StringUtil.Equals(newKoseiMatrix(index).SiaShukeiCode, "E") Or StringUtil.Equals(newKoseiMatrix(index).SiaShukeiCode, "Y") Then
                                '親の供給セクションを取得する'
                                BuhinNo = newKoseiMatrix(index).BuhinNo
                                newKoseiMatrix = SetKyoukuSection0552(BuhinNo, BuhinNo, rhac0552Vos, newKoseiMatrix)
                            End If
                        Else
                            '国内集計'
                            If StringUtil.Equals(newKoseiMatrix(index).ShukeiCode, "X") Or StringUtil.Equals(newKoseiMatrix(index).ShukeiCode, "R") Or StringUtil.Equals(newKoseiMatrix(index).ShukeiCode, "J") Then
                                '供給セクションは空'
                            ElseIf StringUtil.Equals(newKoseiMatrix(index).ShukeiCode, "A") Then
                                newKoseiMatrix(index).KyoukuSection = "9SH10"
                            ElseIf StringUtil.Equals(newKoseiMatrix(index).ShukeiCode, "E") Or StringUtil.Equals(newKoseiMatrix(index).ShukeiCode, "Y") Then
                                '親の供給セクションを取得する'
                                BuhinNo = newKoseiMatrix(index).BuhinNo
                                newKoseiMatrix = SetKyoukuSection0552(BuhinNo, BuhinNo, rhac0552Vos, newKoseiMatrix)
                            End If
                        End If
                    End If
                Next
            End If

            '全体のレベルを再調整する。
            newKoseiMatrix.CorrectLevelBy(baseLevel)

            'ソート処理'
            'newKoseiMatrix = SortMatrix(newKoseiMatrix)

            'ここで圧縮処理をしてみる'

            'newKoseiMatrix = MergeMatrix(newKoseiMatrix)
            Return newKoseiMatrix

        End Function

        Private koseiFlag As Integer

        ''' <summary>
        ''' 「構成の情報」を元に部品表を作成する(構成再展開、最新化、一括構成呼び出し時)
        ''' </summary>
        ''' <param name="aStructureResult">構成の情報</param>
        ''' <param name="a0553Flag">どの操作から来たのか 0:設計展開,1:構成再展開、最新化、部品構成呼び出し、2:子部品呼び出し</param>
        ''' <param name="baseLevel">基点のレベル</param>
        ''' <returns>部品表</returns>
        ''' <remarks></remarks>
        Public Function ComputeKosei(ByVal aStructureResult As StructureResult, ByVal a0553Flag As Integer, ByVal baseLevel As Integer?) As BuhinKoseiMatrix Implements MakeStructureResult.ComputeKosei
            Dim target As String
            Dim KaihatsuFugo As String

            '2012/02/29 開発符号は新規の部品も存在するので、イベント開発符号を取得して使用する'
            If Left(aStructureResult.YobidashiMoto, 4) = "0533" Then
                target = "0533"
                KaihatsuFugo = Split(aStructureResult.YobidashiMoto, "-")(1)
            Else
                If aStructureResult.YobidashiMoto = "0532" Then
                    target = "0532"
                    KaihatsuFugo = ""
                    koseiFlag = 0
                ElseIf aStructureResult.YobidashiMoto = "0530" Then
                    target = "0530"
                    KaihatsuFugo = ""
                    koseiFlag = 1
                Else
                    target = "SHISAKU"
                    KaihatsuFugo = ""
                End If
            End If

            oriBuhinNo = aStructureResult.OriginalBuhinNo
            bfBuhinNo = aStructureResult.BuhinNo


            If Not aStructureResult.IsExist Then
                Return New BuhinKoseiMatrix
            End If


            If Not aStructureResult.IsEBom Then

                If aStructureResult.EditVo IsNot Nothing Then
                    ' 5keyで最小のINSTL品番表示順のBuhinEditInstl

                    Dim editInstlVo As TShisakuBuhinEditInstlVo = FindMinInstlHinbanHyoujiJunEditInstlBy(aStructureResult.EditVo)

                    Dim editInstlVos As List(Of TShisakuBuhinEditInstlVo) = FindEditInstlByInstlHinbanHyoujiJun(editInstlVo)

                    '自給品を含めて取得する。
                    Dim editVos As List(Of TShisakuBuhinEditVo) = FindEditBy(editInstlVo)

                    Dim result As BuhinKoseiMatrix = BuhinKoseiMatrix.NewInnerJoinHyoujiJun(editVos, editInstlVos)
                    result = result.ExtractUnderLevel(aStructureResult.EditVo.BuhinNoHyoujiJun)
                    result.RemoveNullRecords()
                    result.CorrectLevelBy(baseLevel)
                    Return result

                ElseIf aStructureResult.InstlVo IsNot Nothing Then
                    ' 
                    Dim editInstlVos As List(Of TShisakuBuhinEditInstlVo) = FindEditInstlByInstlHinbanHyoujiJun(aStructureResult.InstlVo)

                    '自給品を含めて取得する。
                    Dim editVos As List(Of TShisakuBuhinEditVo) = FindEditBy(aStructureResult.InstlVo)

                    Dim result As BuhinKoseiMatrix = BuhinKoseiMatrix.NewInnerJoinHyoujiJun(editVos, editInstlVos)
                    result.RemoveNullRecords()
                    result.CorrectLevelBy(baseLevel)
                    If result.Records.Count = 0 Then
                        Dim editInstlVos2 As New List(Of TShisakuBuhinEditInstlVo)
                        Dim dummyEditInstlVos2 As New TShisakuBuhinEditInstlVo
                        'dummyEditInstlVos2.InstlHinbanHyoujiJun = aStructureResult.InstlVo.InstlHinbanHyoujiJun
                        dummyEditInstlVos2.InstlHinbanHyoujiJun = 0
                        dummyEditInstlVos2.BuhinNoHyoujiJun = 0
                        dummyEditInstlVos2.InsuSuryo = 1
                        editInstlVos2.Add(dummyEditInstlVos2)

                        Dim editVos2 As New List(Of TShisakuBuhinEditVo)
                        Dim dummyEditVos2 As New TShisakuBuhinEditVo
                        dummyEditVos2.BuhinNo = aStructureResult.InstlVo.InstlHinban
                        dummyEditVos2.BuhinNoKbn = aStructureResult.InstlVo.InstlHinbanKbn
                        dummyEditVos2.Level = 0

                        dummyEditVos2.BuhinNoHyoujiJun = 0
                        editVos2.Add(dummyEditVos2)

                        Dim result2 As BuhinKoseiMatrix = BuhinKoseiMatrix.NewInnerJoinHyoujiJun(editVos2, editInstlVos2)

                        result2.RemoveNullRecords()
                        result2.CorrectLevelBy(baseLevel)
                        Return result2
                    End If
                    Return result
                End If
                Throw New ArgumentException("プロパティの内容が不正", "aStructureResult")
            End If

            Dim inputedBuhinNo As String = aStructureResult.BuhinNo

            'ここから追加 樺澤 2010/12/7 '
            'ベース車情報の開発符号毎に0532を見るのか0533を見るのか分けたい'
            Dim Rhac0552Vo As New Rhac0552Vo

            Rhac0552Vo = makeDao.FindByBuhinRhac0552(inputedBuhinNo)



            '2012/02/02 '
            'INSTL品番を見に行く時にはオリジナル品番で見に行くようにする。


            '0553注記：設計展開時はベース車情報の開発符号を使用'
            '構成再展開、最新化、構成呼び出しは画面側から取得した開発符号を使用'
            '子部品展開はイベント情報の開発符号を使用する'


            '構成再展開、最新化、構成呼び出し時の処理'
            Select Case target
                Case "0530"
                    '過去データ
                    Return BuhinKouseiRhac0551(inputedBuhinNo, aStructureResult, baseLevel)
                Case "0533"
                    '開発管理表
                    Return BuhinKousei(inputedBuhinNo, aStructureResult, baseLevel, KaihatsuFugo)
                Case Else
            End Select


            'ここまで追加'

            '以下は0532の場合のロジック*******************

            '2012/01/25 部品ノート追加
            '532のChukiKijutsuをBuhinNoteとして取得する
            Dim inputedBuhinNoVo As Rhac0532BuhinNoteVo = makeDao.FindLastestRhac0532MakerNameByBuhinNo(inputedBuhinNo)

            ' 入力部品番号を親品番として全構成を取得
            Dim rhac0552Vos As List(Of Rhac0552Vo) = makeDao.FindStructure0552ByBuhinNoOya(inputedBuhinNo)

            Dim parentVoForBuhinNo As New Rhac0552Vo

            Dim newKoseiMatrix As New BuhinKoseiMatrix


            '構成がナッシング（Count＝０）の場合、親品番を再取得 BY 柳沼
            If rhac0552Vos.Count = 0 Then
                rhac0552Vos = makeDao.FindByBuhinRhac0552LIST(inputedBuhinNo)
            End If

            ' 入力部品番号を子品番として直親のみの構成を取得
            parentVoForBuhinNo = makeDao.FindLastestRhac0552ByBuhinNoKo(inputedBuhinNo)
            '親部品の集計コードを取得し、入れ替える。
            If Not StringUtil.IsEmpty(parentVoForBuhinNo) Then
                Dim ShuKeiCD As Rhac0532Vo = makeDao.FindLastestRhac0532ByBuhinNoOya(inputedBuhinNo)
                parentVoForBuhinNo.ShukeiCode = ShuKeiCD.ShukeiCode '国内集計コード
                parentVoForBuhinNo.SiaShukeiCode = ShuKeiCD.SiaShukeiCode '海外集計コード
            End If

            Rhac0552VoHelper.ResolveRule(rhac0552Vos)
            Rhac0552VoHelper.ResolveRule(parentVoForBuhinNo)

            Dim isInstlHinban As Boolean = parentVoForBuhinNo Is Nothing

            ' 入力部品番号を親品番として取得した全構成の子品番の情報
            Dim rhac0532Vos As List(Of Rhac0532Vo) = makeDao.FindStructure0552ByBuhinNoOyaAnd0532ForKo(inputedBuhinNo)
            rhac0532Vos.Add(inputedBuhinNoVo)


            Dim rhac0610Vos As List(Of Rhac0610Vo) = makeDao.FindStructure0552ByBuhinNoOyaAnd0610ForKo(inputedBuhinNo)

            If Not isInstlHinban Then
                rhac0552Vos.Add(parentVoForBuhinNo)
                Dim dummy As New Rhac0532Vo
                dummy.BuhinNo = parentVoForBuhinNo.BuhinNoOya
                rhac0532Vos.Add(dummy)
            End If


            Dim colorA As String = ""

            If rhac0552Vos(0).BuhinNoOya.Length = 12 Then
                colorA = GetBuhinColor(rhac0552Vos(0).BuhinNoOya, 0)
            Else
                colorA = ""
            End If

            '12桁未満ならNothingが返ってくる'
            Dim colorCode As String = ShisakuRule.GetColorFromHinban(aStructureResult.OriginalBuhinNo)

            If StringUtil.IsEmpty(colorCode) Then
                colorCode = ""
            End If

            '基準の色が同じなら、基準の色が存在しない場合'
            If StringUtil.Equals(colorCode, colorA) OrElse StringUtil.IsEmpty(colorCode) Then
                For Each Vo As Rhac0552Vo In rhac0552Vos
                    If Vo.BuhinNoOya.Length = 12 AndAlso StringUtil.Equals(Right(Vo.BuhinNoOya, 2), "##") Then
                        Dim color As String = GetBuhinColor(Vo.BuhinNoOya, 0)
                        If StringUtil.IsEmpty(color) Then
                            If Not StringUtil.IsEmpty(colorCode) Then
                                '自身の色が取れなかった場合は親の色を振る'
                                Vo.BuhinNoOya = Left(Vo.BuhinNoOya, 10) + colorCode
                            End If
                        Else
                            '自身の色が取れた場合は自身の色を振る'
                            Vo.BuhinNoOya = Left(Vo.BuhinNoOya, 10) + color
                        End If

                    End If
                    If Vo.BuhinNoKo.Length = 12 AndAlso StringUtil.Equals(Right(Vo.BuhinNoKo, 2), "##") Then
                        Dim color As String = GetBuhinColor(Vo.BuhinNoKo, 0)
                        If StringUtil.IsEmpty(color) Then
                            If Not StringUtil.IsEmpty(colorCode) Then
                                '自身の色が取れなかった場合は親の色を振る'
                                Vo.BuhinNoKo = Left(Vo.BuhinNoKo, 10) + colorCode
                            End If
                        Else
                            '自身の色が取れた場合は自身の色を振る'
                            Vo.BuhinNoKo = Left(Vo.BuhinNoKo, 10) + color
                        End If
                    End If
                Next
                For Each Vo As Rhac0532Vo In rhac0532Vos
                    If Vo.BuhinNo.Length = 12 AndAlso StringUtil.Equals(Right(Vo.BuhinNo, 2), "##") Then
                        Dim color As String = GetBuhinColor(Vo.BuhinNo, 0)
                        If StringUtil.IsEmpty(color) Then
                            If Not StringUtil.IsEmpty(colorCode) Then
                                '自身の色が取れなかった場合は親の色を振る'
                                Vo.BuhinNo = Left(Vo.BuhinNo, 10) + colorCode
                            End If
                        Else
                            '自身の色が取れた場合は自身の色を振る'
                            Vo.BuhinNo = Left(Vo.BuhinNo, 10) + color
                        End If
                    End If
                Next
            Else
                If Not StringUtil.IsEmpty(colorCode) Then
                    ' rhac0552Vosの中の"##"品番を色に置換える
                    UpdateColor0552(colorCode, rhac0552Vos)

                    ' rhac0532Vosの中の"##"品番を色に置換える
                    UpdateColor0532(colorCode, rhac0532Vos)
                End If
            End If

            '2012/03/16 スペースの存在で取れない構成がいるので'
            For Each Vo As Rhac0532Vo In rhac0532Vos
                Vo.BuhinNo = Trim(Vo.BuhinNo)
            Next
            For Each Vo As Rhac0552Vo In rhac0552Vos
                If Not StringUtil.IsEmpty(Vo.BuhinNoOya) Then
                    Vo.BuhinNoOya = Trim(Vo.BuhinNoOya)
                End If
                If Not StringUtil.IsEmpty(Vo.BuhinNoKo) Then
                    Vo.BuhinNoKo = Trim(Vo.BuhinNoKo)
                End If
            Next

            '集計コードブランクを除いて構成を返す。
            ' [RemoveNonShukei]
            'Dim singleMatrices As BuhinSingleMatrix(Of Rhac0552Vo, Rhac0532Vo)() = BuhinTreeMaker.NewSingleMatrices(RemoveNonShukei(rhac0552Vos), rhac0532Vos)
            Dim singleMatrices As BuhinSingleMatrix(Of Rhac0552Vo, Rhac0532Vo)() = BuhinTreeMaker.NewSingleMatrices(rhac0552Vos, rhac0532Vos)

            If singleMatrices.Length <> 1 Then
                Throw New InvalidProgramException("SQLで1品番の構成だけ取って来ているのに NewSingleMatrices で構成が " & singleMatrices.Length & " 個")
            End If

            Dim mergeNode As New MergeNodeList(Of Rhac0552Vo, Rhac0532Vo)(New RhacNodeAccessor(New MakerNameResolverImpl(rhac0610Vos, inputedBuhinNoVo)))

            mergeNode.Compute(singleMatrices(0).Nodes)

            newKoseiMatrix = mergeNode.ResultMatrix
            'Dim newKoseiMatrix As BuhinKoseiMatrix = mergeNode.ResultMatrix
            If Not isInstlHinban Then
                newKoseiMatrix.RemoveRow(0) ' parentVoForBuhinNo.BuhinNoOya が先頭にいるから削除しておく
            End If
            '集計コードがＲの子部品の構成情報を除外する。
            RemoveNonRbuhin(newKoseiMatrix)


            '余分な行が存在した場合削除する'
            For Each index As Integer In newKoseiMatrix.GetInputRowIndexes
                If index < 0 Then
                    newKoseiMatrix.RemoveRow(index)
                ElseIf StringUtil.IsEmpty(newKoseiMatrix(index).BuhinNo) Then
                    newKoseiMatrix.RemoveRow(index)
                End If
            Next

            '余分な行を削除した後で取引先コードと名称を取得する'
            For Each index As Integer In newKoseiMatrix.GetInputRowIndexes
                If StringUtil.IsEmpty(newKoseiMatrix(index).MakerCode) And StringUtil.IsEmpty(newKoseiMatrix(index).MakerName) Then
                    If Not StringUtil.IsEmpty(newKoseiMatrix(index).BuhinNo) Then
                        Dim MakerVo As New TShisakuBuhinEditVo
                        Dim impl As ShisakuBuhinMenu.Dao.BuhinEditBaseDao = New ShisakuBuhinMenu.Dao.BuhinEditBaseDaoImpl
                        '/*** 20140911 CHANGE START（引数追加） ***/
                        'MakerVo = impl.FindByKoutanTorihikisaki(newKoseiMatrix(index).BuhinNo)
                        MakerVo = impl.FindByKoutanTorihikisakiUseDictionary(newKoseiMatrix(index).BuhinNo, kotanTorihikisakiSelected)
                        '/*** 20140911 CHANGE END ***/
                        
                        newKoseiMatrix(index).MakerCode = MakerVo.MakerCode
                        newKoseiMatrix(index).MakerName = MakerVo.MakerName
                    End If

                End If
            Next


            '供給セクションを振る'
            If Not newKoseiMatrix Is Nothing Then
                For Each index As Integer In newKoseiMatrix.GetInputRowIndexes
                    Dim BuhinNo As String = ""
                    If Not StringUtil.IsEmpty(newKoseiMatrix(index).BuhinNo) Then

                        '集計コードを確認する'
                        If StringUtil.IsEmpty(newKoseiMatrix(index).ShukeiCode) Then
                            '海外集計'
                            If StringUtil.Equals(newKoseiMatrix(index).SiaShukeiCode, "X") Or StringUtil.Equals(newKoseiMatrix(index).SiaShukeiCode, "R") Or StringUtil.Equals(newKoseiMatrix(index).SiaShukeiCode, "J") Then
                                '供給セクションは空'
                            ElseIf StringUtil.Equals(newKoseiMatrix(index).SiaShukeiCode, "A") Then
                                newKoseiMatrix(index).KyoukuSection = "9SH10"
                            ElseIf StringUtil.Equals(newKoseiMatrix(index).SiaShukeiCode, "E") Or StringUtil.Equals(newKoseiMatrix(index).SiaShukeiCode, "Y") Then
                                '親の供給セクションを取得する'
                                BuhinNo = newKoseiMatrix(index).BuhinNo
                                newKoseiMatrix = SetKyoukuSection0552(BuhinNo, BuhinNo, rhac0552Vos, newKoseiMatrix)
                            End If
                        Else
                            '国内集計'
                            If StringUtil.Equals(newKoseiMatrix(index).ShukeiCode, "X") Or StringUtil.Equals(newKoseiMatrix(index).ShukeiCode, "R") Or StringUtil.Equals(newKoseiMatrix(index).ShukeiCode, "J") Then
                                '供給セクションは空'
                            ElseIf StringUtil.Equals(newKoseiMatrix(index).ShukeiCode, "A") Then
                                newKoseiMatrix(index).KyoukuSection = "9SH10"
                            ElseIf StringUtil.Equals(newKoseiMatrix(index).ShukeiCode, "E") Or StringUtil.Equals(newKoseiMatrix(index).ShukeiCode, "Y") Then
                                '親の供給セクションを取得する'
                                BuhinNo = newKoseiMatrix(index).BuhinNo
                                newKoseiMatrix = SetKyoukuSection0552(BuhinNo, BuhinNo, rhac0552Vos, newKoseiMatrix)
                            End If
                        End If
                    End If
                Next
            End If

            '全体のレベルを再調整する。
            newKoseiMatrix.CorrectLevelBy(baseLevel)

            Return newKoseiMatrix
        End Function

        ''' <summary>
        ''' 部品の構成を一つだけ取得する
        ''' </summary>
        ''' <param name="aStructureResult"></param>
        ''' <param name="a0553Flag"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Function GetBuhinKosei(ByVal aStructureResult As StructureResult, ByVal a0553Flag As Integer) As BuhinKoseiMatrix Implements MakeStructureResult.GetBuhinKosei
            Dim target As String
            Dim KaihatsuFugo As String
            If Left(aStructureResult.YobidashiMoto, 4) = "0533" Then
                target = "0533"
                KaihatsuFugo = Split(aStructureResult.YobidashiMoto, "-")(1)
            Else
                If aStructureResult.YobidashiMoto = "0532" Then
                    target = "0532"
                    KaihatsuFugo = ""
                ElseIf aStructureResult.YobidashiMoto = "0530" Then
                    target = "0530"
                    KaihatsuFugo = ""
                Else
                    target = "SHISAKU"
                    KaihatsuFugo = ""
                End If
            End If

            oriBuhinNo = aStructureResult.OriginalBuhinNo
            bfBuhinNo = aStructureResult.BuhinNo


            If Not aStructureResult.IsExist Then
                Return New BuhinKoseiMatrix
            End If


            If Not aStructureResult.IsEBom Then

                If aStructureResult.EditVo IsNot Nothing Then
                    ' 5keyで最小のINSTL品番表示順のBuhinEditInstl

                    Dim editInstlVo As TShisakuBuhinEditInstlVo = FindMinInstlHinbanHyoujiJunEditInstlBy(aStructureResult.EditVo)

                    Dim editInstlVos As List(Of TShisakuBuhinEditInstlVo) = FindEditInstlByInstlHinbanHyoujiJun(editInstlVo)

                    '自給品を含めて取得する。
                    Dim editVos As List(Of TShisakuBuhinEditVo) = FindEditBy(editInstlVo)

                    Dim result As BuhinKoseiMatrix = BuhinKoseiMatrix.NewInnerJoinHyoujiJun(editVos, editInstlVos)
                    result = result.ExtractUnderLevel(aStructureResult.EditVo.BuhinNoHyoujiJun)
                    result.RemoveNullRecords()
                    result.CorrectLevelBy(0)
                    Return result

                ElseIf aStructureResult.InstlVo IsNot Nothing Then
                    ' 
                    Dim editInstlVos As List(Of TShisakuBuhinEditInstlVo) = FindEditInstlByInstlHinbanHyoujiJun(aStructureResult.InstlVo)

                    '自給品を含めて取得する。
                    Dim editVos As List(Of TShisakuBuhinEditVo) = FindEditBy(aStructureResult.InstlVo)

                    Dim result As BuhinKoseiMatrix = BuhinKoseiMatrix.NewInnerJoinHyoujiJun(editVos, editInstlVos)
                    result.RemoveNullRecords()
                    result.CorrectLevelBy(0)
                    If result.Records.Count = 0 Then
                        Dim editInstlVos2 As New List(Of TShisakuBuhinEditInstlVo)
                        Dim dummyEditInstlVos2 As New TShisakuBuhinEditInstlVo
                        'dummyEditInstlVos2.InstlHinbanHyoujiJun = aStructureResult.InstlVo.InstlHinbanHyoujiJun
                        dummyEditInstlVos2.InstlHinbanHyoujiJun = 0
                        dummyEditInstlVos2.BuhinNoHyoujiJun = 0
                        dummyEditInstlVos2.InsuSuryo = 1
                        editInstlVos2.Add(dummyEditInstlVos2)

                        Dim editVos2 As New List(Of TShisakuBuhinEditVo)
                        Dim dummyEditVos2 As New TShisakuBuhinEditVo
                        dummyEditVos2.BuhinNo = aStructureResult.InstlVo.InstlHinban
                        dummyEditVos2.BuhinNoKbn = aStructureResult.InstlVo.InstlHinbanKbn
                        dummyEditVos2.Level = 0

                        dummyEditVos2.BuhinNoHyoujiJun = 0
                        editVos2.Add(dummyEditVos2)

                        Dim result2 As BuhinKoseiMatrix = BuhinKoseiMatrix.NewInnerJoinHyoujiJun(editVos2, editInstlVos2)

                        result2.RemoveNullRecords()
                        result2.CorrectLevelBy(0)
                        Return result2
                    End If
                    Return result
                End If
                Throw New ArgumentException("プロパティの内容が不正", "aStructureResult")
            End If

            Dim inputedBuhinNo As String = aStructureResult.BuhinNo

            'ここから追加 樺澤 2010/12/7 '
            'ベース車情報の開発符号毎に0532を見るのか0533を見るのか分けたい'
            Dim Rhac0552Vo As New Rhac0552Vo

            Rhac0552Vo = makeDao.FindByBuhinRhac0552(inputedBuhinNo)

            '2012/02/02 '
            'INSTL品番を見に行く時にはオリジナル品番で見に行くようにする。


            '0553注記：設計展開時はベース車情報の開発符号を使用'
            '構成再展開、最新化、構成呼び出しは画面側から取得した開発符号を使用'
            '子部品展開はイベント情報の開発符号を使用する'


            '構成再展開、最新化、構成呼び出し時の処理'
            Select Case target
                Case "0530"
                    '過去データ
                    Return BuhinKouseiRhac0551(inputedBuhinNo, aStructureResult, 0)
                Case "0533"
                    '開発管理表
                    Return BuhinKousei(inputedBuhinNo, aStructureResult, 0, KaihatsuFugo)
                Case Else
            End Select


            'ここまで追加'

            '以下は0532の場合のロジック*******************

            '2012/01/25 部品ノート追加
            '532のChukiKijutsuをBuhinNoteとして取得する
            'Dim inputedBuhinNoVo As Rhac0532MakerNameVo = makeDao.FindLastestRhac0532MakerNameByBuhinNo(inputedBuhinNo)
            Dim inputedBuhinNoVo As Rhac0532BuhinNoteVo = makeDao.FindLastestRhac0532MakerNameByBuhinNo(inputedBuhinNo)

            ' 入力部品番号を親品番として全構成を取得
            Dim rhac0552Vos As List(Of Rhac0552Vo) = makeDao.FindStructure0552ByBuhinNoOya(inputedBuhinNo)

            Dim parentVoForBuhinNo As New Rhac0552Vo

            Dim newKoseiMatrix As New BuhinKoseiMatrix

            ''ここのロジックがあるとテストクラスがエラー'
            ''ここのロジックが無いと設計展開でエラー'
            ''無ければ自身を構成させる'
            'If rhac0552Vos.Count = 0 Then
            '    'どうすれば・・・・？'

            '    '親品番をとりあえず設定'
            '    'parentVoForBuhinNo.BuhinNoOya = inputedBuhinNo

            '    'Rhac0552VoHelper.ResolveRule(rhac0552Vos)
            '    'Rhac0552VoHelper.ResolveRule(parentVoForBuhinNo)

            '    Return Nothing


            '    'newKoseiMatrix(0).BuhinNo = inputedBuhinNo
            '    'newKoseiMatrix(0).BuhinNoHyoujiJun = 0
            '    'newKoseiMatrix(0).Level = 0
            '    'newKoseiMatrix(0).ShukeiCode = "J"
            '    'newKoseiMatrix(0).BuhinName = "テスト用"
            '    'newKoseiMatrix.InsuSuryo(0, 0) = 1

            '    'Return newKoseiMatrix

            '    'Dim isInstlHinban As Boolean = parentVoForBuhinNo Is Nothing

            '    '' 入力部品番号を親品番として取得した全構成の子品番の情報
            '    'Dim rhac0532Vos As List(Of Rhac0532Vo) = makeDao.FindStructure0552ByBuhinNoOyaAnd0532ForKo(inputedBuhinNo)
            '    'rhac0532Vos.Add(inputedBuhinNoVo)

            '    'Dim rhac0610Vos As List(Of Rhac0610Vo) = makeDao.FindStructure0552ByBuhinNoOyaAnd0610ForKo(inputedBuhinNo)

            '    'If Not isInstlHinban Then
            '    '    parentVoForBuhinNo.BuhinNoOya = inputedBuhinNo
            '    '    parentVoForBuhinNo.BuhinNoKo = inputedBuhinNo
            '    '    parentVoForBuhinNo.ShukeiCode = "J"

            '    '    rhac0552Vos.Add(parentVoForBuhinNo)
            '    '    Dim dummy As New Rhac0532Vo
            '    '    dummy.BuhinNo = parentVoForBuhinNo.BuhinNoOya
            '    '    rhac0532Vos.Add(dummy)
            '    'End If

            '    'If ShisakuRule.IsHinbanReplacedColor(aStructureResult.BuhinNo) Then
            '    '    Dim colorCode As String = ShisakuRule.GetColorFromHinban(aStructureResult.OriginalBuhinNo)
            '    '    ' ↑これが色
            '    '    ' TODO 11.27本間コメント 時間が足りないので調査結果を記す
            '    '    ' rhac0552Vosの中の"##"品番を色に置換える
            '    '    UpdateColor0552(colorCode, rhac0552Vos)

            '    '    ' rhac0532Vosの中の"##"品番を色に置換える
            '    '    UpdateColor0532(colorCode, rhac0532Vos)

            '    '    '上記の、「色置換え」ロジックは、ShisakuRuleクラスにメソッドとして持たせるのがベター
            '    '    ' ReplaceColor(hinban As String, replaceColor As String) As String
            '    '    'こんなメソッドで。

            '    '    'それと、当メソッドをコピペして作られた、ComputeNotJikyu()も同様の修正が必要。
            '    '    ' 二重ロジック止めて欲しい。
            '    'End If

            '    'Dim singleMatrices As BuhinSingleMatrix(Of Rhac0552Vo, Rhac0532Vo)() = BuhinTreeMaker.NewSingleMatrices(RemoveNonShukei(rhac0552Vos), rhac0532Vos)
            '    'If singleMatrices.Length <> 1 Then

            '    '    Throw New InvalidProgramException("SQLで1品番の構成だけ取って来ているのに NewSingleMatrices で構成が " & singleMatrices.Length & " 個")
            '    'End If

            '    'Dim mergeNode As New MergeNodeList(Of Rhac0552Vo, Rhac0532Vo)(New RhacNodeAccessor(New MakerNameResolverImpl(rhac0610Vos, inputedBuhinNoVo)))
            '    'mergeNode.Compute(singleMatrices(0).Nodes)

            '    'newKoseiMatrix = mergeNode.ResultMatrix
            '    ''Dim newKoseiMatrix As BuhinKoseiMatrix = mergeNode.ResultMatrix
            '    'If Not isInstlHinban Then
            '    '    newKoseiMatrix.RemoveRow(0) ' parentVoForBuhinNo.BuhinNoOya が先頭にいるから削除しておく
            '    'End If
            '    'newKoseiMatrix.CorrectLevelBy(baseLevel)



            'Else

            '構成がナッシング（Count＝０）の場合、親品番を再取得 BY 柳沼
            If rhac0552Vos.Count = 0 Then
                rhac0552Vos = makeDao.FindByBuhinRhac0552LIST(inputedBuhinNo)
            End If

            ' 入力部品番号を子品番として直親のみの構成を取得
            parentVoForBuhinNo = makeDao.FindLastestRhac0552ByBuhinNoKo(inputedBuhinNo)
            '親部品の集計コードを取得し、入れ替える。
            If Not StringUtil.IsEmpty(parentVoForBuhinNo) Then
                Dim ShuKeiCD As Rhac0532Vo = makeDao.FindLastestRhac0532ByBuhinNoOya(inputedBuhinNo)
                parentVoForBuhinNo.ShukeiCode = ShuKeiCD.ShukeiCode '国内集計コード
                parentVoForBuhinNo.SiaShukeiCode = ShuKeiCD.SiaShukeiCode '海外集計コード
            End If

            Rhac0552VoHelper.ResolveRule(rhac0552Vos)
            Rhac0552VoHelper.ResolveRule(parentVoForBuhinNo)

            Dim isInstlHinban As Boolean = parentVoForBuhinNo Is Nothing

            ' 入力部品番号を親品番として取得した全構成の子品番の情報
            Dim rhac0532Vos As List(Of Rhac0532Vo) = makeDao.FindStructure0552ByBuhinNoOyaAnd0532ForKo(inputedBuhinNo)
            rhac0532Vos.Add(inputedBuhinNoVo)


            Dim rhac0610Vos As List(Of Rhac0610Vo) = makeDao.FindStructure0552ByBuhinNoOyaAnd0610ForKo(inputedBuhinNo)

            If Not isInstlHinban Then
                rhac0552Vos.Add(parentVoForBuhinNo)
                Dim dummy As New Rhac0532Vo
                dummy.BuhinNo = parentVoForBuhinNo.BuhinNoOya
                rhac0532Vos.Add(dummy)
            End If

            '2012/02/27 子供の色は問答無用で振る'
            'If ShisakuRule.IsHinbanReplacedColor(aStructureResult.BuhinNo) Then

            Dim colorA As String = ""

            If rhac0552Vos(0).BuhinNoOya.Length = 12 Then
                colorA = GetBuhinColor(rhac0552Vos(0).BuhinNoOya, 0)
            Else
                colorA = ""
            End If

            '12桁未満ならNothingが返ってくる'
            Dim colorCode As String = ShisakuRule.GetColorFromHinban(aStructureResult.OriginalBuhinNo)

            If StringUtil.IsEmpty(colorCode) Then
                colorCode = ""
            End If

            '基準の色が同じなら、基準の色が存在しない場合'
            If StringUtil.Equals(colorCode, colorA) OrElse StringUtil.IsEmpty(colorCode) Then
                For Each Vo As Rhac0552Vo In rhac0552Vos
                    If Vo.BuhinNoOya.Length = 12 AndAlso StringUtil.Equals(Right(Vo.BuhinNoOya, 2), "##") Then
                        Dim color As String = GetBuhinColor(Vo.BuhinNoOya, 0)
                        If StringUtil.IsEmpty(color) Then
                            If Not StringUtil.IsEmpty(colorCode) Then
                                '自身の色が取れなかった場合は親の色を振る'
                                Vo.BuhinNoOya = Left(Vo.BuhinNoOya, 10) + colorCode
                            End If
                        Else
                            '自身の色が取れた場合は自身の色を振る'
                            Vo.BuhinNoOya = Left(Vo.BuhinNoOya, 10) + color
                        End If

                    End If
                    If Vo.BuhinNoKo.Length = 12 AndAlso StringUtil.Equals(Right(Vo.BuhinNoKo, 2), "##") Then
                        Dim color As String = GetBuhinColor(Vo.BuhinNoKo, 0)
                        If StringUtil.IsEmpty(color) Then
                            If Not StringUtil.IsEmpty(colorCode) Then
                                '自身の色が取れなかった場合は親の色を振る'
                                Vo.BuhinNoKo = Left(Vo.BuhinNoKo, 10) + colorCode
                            End If
                        Else
                            '自身の色が取れた場合は自身の色を振る'
                            Vo.BuhinNoKo = Left(Vo.BuhinNoKo, 10) + color
                        End If
                    End If
                Next
                For Each Vo As Rhac0532Vo In rhac0532Vos
                    If Vo.BuhinNo.Length = 12 AndAlso StringUtil.Equals(Right(Vo.BuhinNo, 2), "##") Then
                        Dim color As String = GetBuhinColor(Vo.BuhinNo, 0)
                        If StringUtil.IsEmpty(color) Then
                            If Not StringUtil.IsEmpty(colorCode) Then
                                '自身の色が取れなかった場合は親の色を振る'
                                Vo.BuhinNo = Left(Vo.BuhinNo, 10) + colorCode
                            End If
                        Else
                            '自身の色が取れた場合は自身の色を振る'
                            Vo.BuhinNo = Left(Vo.BuhinNo, 10) + color
                        End If
                    End If
                Next
            Else
                If Not StringUtil.IsEmpty(colorCode) Then
                    ' rhac0552Vosの中の"##"品番を色に置換える
                    UpdateColor0552(colorCode, rhac0552Vos)

                    ' rhac0532Vosの中の"##"品番を色に置換える
                    UpdateColor0532(colorCode, rhac0532Vos)
                End If
            End If

            ' ↑これが色
            ' TODO 11.27本間コメント 時間が足りないので調査結果を記す
            ' rhac0552Vosの中の"##"品番を色に置換える

            'UpdateColor0552(colorCode, rhac0552Vos)

            ' rhac0532Vosの中の"##"品番を色に置換える
            'UpdateColor0532(colorCode, rhac0532Vos)

            '上記の、「色置換え」ロジックは、ShisakuRuleクラスにメソッドとして持たせるのがベター
            ' ReplaceColor(hinban As String, replaceColor As String) As String
            'こんなメソッドで。

            'それと、当メソッドをコピペして作られた、ComputeNotJikyu()も同様の修正が必要。
            ' 二重ロジック止めて欲しい。
            'End If

            '集計コードブランクを除いて構成を返す。
            ' [RemoveNonShukei]
            'Dim singleMatrices As BuhinSingleMatrix(Of Rhac0552Vo, Rhac0532Vo)() = BuhinTreeMaker.NewSingleMatrices(RemoveNonShukei(rhac0552Vos), rhac0532Vos)
            Dim singleMatrices As BuhinSingleMatrix(Of Rhac0552Vo, Rhac0532Vo)() = BuhinTreeMaker.NewSingleMatrices(rhac0552Vos, rhac0532Vos)

            If singleMatrices.Length <> 1 Then
                Throw New InvalidProgramException("SQLで1品番の構成だけ取って来ているのに NewSingleMatrices で構成が " & singleMatrices.Length & " 個")
            End If

            Dim mergeNode As New MergeNodeList(Of Rhac0552Vo, Rhac0532Vo)(New RhacNodeAccessor(New MakerNameResolverImpl(rhac0610Vos, inputedBuhinNoVo)))

            mergeNode.Compute(singleMatrices(0).Nodes)

            newKoseiMatrix = mergeNode.ResultMatrix
            'Dim newKoseiMatrix As BuhinKoseiMatrix = mergeNode.ResultMatrix
            If Not isInstlHinban Then
                newKoseiMatrix.RemoveRow(0) ' parentVoForBuhinNo.BuhinNoOya が先頭にいるから削除しておく
            End If
            '集計コードがＲの子部品の構成情報を除外する。
            RemoveNonRbuhin(newKoseiMatrix)

            '############################################################################
            '2012/01/12 現状：購担取引先の処理時間が掛かりすぎ（無駄が多い）
            '　　　　　 結論：実装方法を考え直す必要有り
            '　　　　　 対策：現状は、非常に遅いので実装方法が確定するまでは、
            '　　　　　 　　　コメントアウトとする
            '############################################################################
            'For Each index As Integer In newKoseiMatrix.GetInputRowIndexes
            '    If Not StringUtil.IsEmpty(newKoseiMatrix(index).BuhinNo) Then
            '        If StringUtil.IsEmpty(newKoseiMatrix(index).MakerCode) And StringUtil.IsEmpty(newKoseiMatrix(index).MakerName) Then
            '            Dim MakerVo As New TShisakuBuhinEditVo
            '            Dim impl As ShisakuBuhinMenu.Dao. = New ShisakuBuhinMenu.Dao.BuhinEditBaseDaoImpl
            'MakerVo = BuhinEditBaseDaoimpl.FindByKoutanTorihikisaki(newKoseiMatrix(index).BuhinNo)
            '            newKoseiMatrix(index).MakerCode = MakerVo.MakerCode
            '            newKoseiMatrix(index).MakerName = MakerVo.MakerName
            '        End If
            '    Else
            '        '部品情報が無い場合、削除してみる。※ブランクデータが出来ちゃっているので。
            '        newKoseiMatrix.RemoveRow(index)
            '    End If
            'Next

            '余分な行が存在した場合削除する'
            For Each index As Integer In newKoseiMatrix.GetInputRowIndexes
                If index < 0 Then
                    newKoseiMatrix.RemoveRow(index)
                ElseIf StringUtil.IsEmpty(newKoseiMatrix(index).BuhinNo) Then
                    newKoseiMatrix.RemoveRow(index)
                End If
            Next

            '余分な行を削除した後で取引先コードと名称を取得する'
            For Each index As Integer In newKoseiMatrix.GetInputRowIndexes
                If StringUtil.IsEmpty(newKoseiMatrix(index).MakerCode) And StringUtil.IsEmpty(newKoseiMatrix(index).MakerName) Then
                    If Not StringUtil.IsEmpty(newKoseiMatrix(index).BuhinNo) Then
                        Dim MakerVo As New TShisakuBuhinEditVo
                        Dim impl As ShisakuBuhinMenu.Dao.BuhinEditBaseDao = New ShisakuBuhinMenu.Dao.BuhinEditBaseDaoImpl
                        '/*** 20140911 CHANGE START（引数追加） ***/
                        'MakerVo = impl.FindByKoutanTorihikisaki(newKoseiMatrix(index).BuhinNo)
                        MakerVo = impl.FindByKoutanTorihikisakiUseDictionary(newKoseiMatrix(index).BuhinNo, kotanTorihikisakiSelected)
                        '/*** 20140911 CHANGE END ***/
                        newKoseiMatrix(index).MakerCode = MakerVo.MakerCode
                        newKoseiMatrix(index).MakerName = MakerVo.MakerName
                    End If

                End If
                'If Not StringUtil.IsEmpty(newKoseiMatrix(index).BuhinNo) Then
                'Else
                '    '部品情報が無い場合、削除してみる。※ブランクデータが出来ちゃっているので。
                '    newKoseiMatrix.RemoveRow(index)
                'End If
            Next

            Return newKoseiMatrix
        End Function

        ''' <summary>
        ''' 供給セクションをふる
        ''' </summary>
        ''' <param name="buhinNo">子部品番号</param>
        ''' <param name="originalBuhinNo">供給セクションを追加する部品番号</param>
        ''' <param name="rhac0552Vos"></param>
        ''' <param name="KoseiMatrix"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Private Function SetKyoukuSection0552(ByVal buhinNo As String, ByVal originalBuhinNo As String, ByVal rhac0552Vos As List(Of Rhac0552Vo), ByVal KoseiMatrix As BuhinKoseiMatrix) As BuhinKoseiMatrix


            '部品番号の存在をチェック'
            For Each Vo As Rhac0552Vo In rhac0552Vos
                If StringUtil.Equals(buhinNo, Vo.BuhinNoKo) Then
                    Dim buhinNoOya As String = Vo.BuhinNoOya
                    '存在したら親部品が構成マトリクスに存在するかチェックする'
                    For Each index As Integer In KoseiMatrix.GetInputRowIndexes
                        If StringUtil.Equals(KoseiMatrix(index).BuhinNo, buhinNoOya) Then
                            '存在したら集計コードをチェック'
                            If StringUtil.IsEmpty(KoseiMatrix(index).ShukeiCode) Then
                                '海外集計'
                                If StringUtil.Equals(KoseiMatrix(index).SiaShukeiCode, "J") Then
                                    'Jなら一段階上の親を探す'
                                    KoseiMatrix = SetKyoukuSection0552(KoseiMatrix(index).BuhinNo, originalBuhinNo, rhac0552Vos, KoseiMatrix)
                                Else
                                    'J以外ならメーカーコード+0で供給セクションを追加'
                                    For Each index2 As Integer In KoseiMatrix.GetInputRowIndexes
                                        If StringUtil.Equals(KoseiMatrix(index2).BuhinNo, originalBuhinNo) Then
                                            Dim KyoukuSection As String = ""
                                            If Not StringUtil.IsEmpty(KoseiMatrix(index).MakerCode) Then
                                                KyoukuSection = KoseiMatrix(index).MakerCode + "0"
                                            End If
                                            If StringUtil.IsEmpty(KoseiMatrix(index2).KyoukuSection) Then
                                                KoseiMatrix(index2).KyoukuSection = KyoukuSection
                                                Return KoseiMatrix
                                            End If
                                        End If
                                    Next
                                End If
                            Else
                                '国内集計'
                                '海外集計'
                                If StringUtil.Equals(KoseiMatrix(index).ShukeiCode, "J") Then
                                    'Jなら一段階上の親を探す'
                                    KoseiMatrix = SetKyoukuSection0552(KoseiMatrix(index).BuhinNo, originalBuhinNo, rhac0552Vos, KoseiMatrix)
                                Else
                                    'J以外ならメーカーコード+0で供給セクションを追加'
                                    For Each index2 As Integer In KoseiMatrix.GetInputRowIndexes
                                        If StringUtil.Equals(KoseiMatrix(index2).BuhinNo, originalBuhinNo) Then
                                            Dim KyoukuSection As String = ""
                                            If Not StringUtil.IsEmpty(KoseiMatrix(index).MakerCode) Then
                                                KyoukuSection = KoseiMatrix(index).MakerCode + "0"
                                            End If
                                            If StringUtil.IsEmpty(KoseiMatrix(index2).KyoukuSection) Then
                                                KoseiMatrix(index2).KyoukuSection = KyoukuSection
                                                Return KoseiMatrix
                                            End If
                                        End If
                                    Next
                                End If
                            End If

                        End If
                    Next
                End If
            Next
            Return KoseiMatrix
        End Function

        ''' <summary>
        ''' 並び順を変更する
        ''' </summary>
        ''' <param name="koseiMatrix">構成</param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Private Function SortMatrix(ByVal koseiMatrix As BuhinKoseiMatrix) As BuhinKoseiMatrix
            'レベル０は対象外'
            Dim lastLevel As Integer = GetLastLevel(koseiMatrix)

            For level As Integer = 1 To lastLevel
                For Each index As Integer In koseiMatrix.GetInputRowIndexes
                    If koseiMatrix(index).Level = level Then
                        '上5桁取得'
                        Dim buhinNo5 As String = Left(koseiMatrix(index).BuhinNo, 5)
                        For Each index2 As Integer In koseiMatrix.GetInputRowIndexes
                            '同レベル'
                            If koseiMatrix(index2).Level = level Then
                                '同部品番号でない'
                                If Not StringUtil.Equals(koseiMatrix(index).BuhinNo, koseiMatrix(index2).BuhinNo) Then
                                    '上5桁が同じ'
                                    If StringUtil.Equals(buhinNo5, Left(koseiMatrix(index2).BuhinNo, 5)) Then
                                        '並び順をindexの真下にする'
                                        koseiMatrix.InsertRow(index + 1)
                                        koseiMatrix.Record(index + 1) = koseiMatrix.Record(index2)
                                        '移動したので削除する '
                                        koseiMatrix.RemoveRow(index2 + 1)
                                    End If
                                End If
                            End If
                        Next
                    End If
                Next
            Next
            Return koseiMatrix
        End Function

        ''' <summary>
        ''' マージ処理
        ''' </summary>
        ''' <param name="koseiMatrix"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Private Function MergeMatrix(ByVal koseiMatrix As BuhinKoseiMatrix) As BuhinKoseiMatrix

            '部品構成内で同レベル、同部品番号、同集計コード、同供給セクションの時に員数をマージする'
            For Each index As Integer In koseiMatrix.GetInputRowIndexes
                If koseiMatrix(index).Level = 0 Then
                    Continue For
                End If
                For Each index2 As Integer In koseiMatrix.GetInputRowIndexes
                    If koseiMatrix(index2).Level = 0 Then
                        Continue For
                    End If

                    '同レベル,同部品番号、同集計コード、同供給セクションのとき員数をマージする'
                    If koseiMatrix(index).Level = koseiMatrix(index2).Level _
                            AndAlso StringUtil.Equals(koseiMatrix(index).BuhinNo, koseiMatrix(index2).BuhinNo) _
                            AndAlso EzUtil.IsEqualIfNull(koseiMatrix(index).ShukeiCode, koseiMatrix(index2).ShukeiCode) _
                            AndAlso EzUtil.IsEqualIfNull(koseiMatrix(index).SiaShukeiCode, koseiMatrix(index2).SiaShukeiCode) _
                            AndAlso EzUtil.IsEqualIfNull(koseiMatrix(index).KyoukuSection, koseiMatrix(index2).KyoukuSection) Then
                        '員数のマージを行う'
                        koseiMatrix.InsuVo(index, 0).InsuSuryo = koseiMatrix.InsuVo(index, 0).InsuSuryo + koseiMatrix.InsuVo(index2, 0).InsuSuryo

                        '余計な行の削除を行う'
                        koseiMatrix.RemoveRow(index2)
                    End If
                Next
            Next

            koseiMatrix.RemoveNullRecords()
            Dim newkoseiMatrix As New BuhinKoseiMatrix
            newkoseiMatrix.InstlColumnAdd(0, koseiMatrix.InstlColumn(0))

            For Each index As Integer In koseiMatrix.GetInputRowIndexes
                If Not koseiMatrix(index).BuhinNo Is Nothing Then
                    newkoseiMatrix.InsertRow(index)
                    newkoseiMatrix.Record(index) = koseiMatrix.Record(index)
                    newkoseiMatrix.InsuVo(index, 0) = koseiMatrix.InsuVo(index, 0)
                End If
            Next


            Return newkoseiMatrix
        End Function


        ''' <summary>
        ''' 最大レベルを取得する
        ''' </summary>
        ''' <param name="koseiMatrix">構成</param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Private Function GetLastLevel(ByVal koseiMatrix As BuhinKoseiMatrix) As Integer
            Dim result As Integer = 0
            For Each index As Integer In koseiMatrix.GetInputRowIndexes
                If koseiMatrix(index).Level > result Then
                    result = koseiMatrix(index).Level
                End If
            Next
            Return result
        End Function


        ''' <summary>
        ''' 構成情報の色コード変換
        ''' </summary>
        ''' <param name="vos">構成情報</param>
        ''' <returns>色コード（##→色コード）を変換した構成情報</returns>
        ''' <remarks></remarks>
        Private Function UpdateColor0552(ByVal colorCode As String, ByVal vos As List(Of Rhac0552Vo)) As List(Of Rhac0552Vo)
            Dim result As New List(Of Rhac0552Vo)
            For Each vo As Rhac0552Vo In vos
                vo.BuhinNoOya = ShisakuRule.ReplaceColor(vo.BuhinNoOya, colorCode)
                vo.BuhinNoKo = ShisakuRule.ReplaceColor(vo.BuhinNoKo, colorCode)

                result.Add(vo)
            Next
            Return result
        End Function

        ''' <summary>
        ''' 部品情報の色コード変換
        ''' </summary>
        ''' <param name="vos">部品情報</param>
        ''' <returns>色コード（##→色コード）を変換した部品情報</returns>
        ''' <remarks></remarks>
        Private Function UpdateColor0532(ByVal colorCode As String, ByVal vos As List(Of Rhac0532Vo)) As List(Of Rhac0532Vo)
            Dim result As New List(Of Rhac0532Vo)
            For Each vo As Rhac0532Vo In vos
                vo.BuhinNo = ShisakuRule.ReplaceColor(vo.BuhinNo, colorCode)
                result.Add(vo)
            Next
            Return result
        End Function

        ''' <summary>
        ''' 部品ごとに色を取得する
        ''' </summary>
        ''' <param name="buhinNo">部品番号</param>
        ''' <param name="rhFlag">0:RHAC0532, 1:RHAC0533, 2:RHAC0530</param>
        ''' <returns>該当する部品の色</returns>
        ''' <remarks></remarks>
        Private Function GetBuhinColor(ByVal buhinNo As String, ByVal rhFlag As Integer, Optional ByVal kaihatsuFugo As String = "", Optional ByVal yakanFlg As Boolean = False) As String
            '↓↓2014/09/25 酒井 ADD BEGIN
            '        Private Function GetBuhinColor(ByVal buhinNo As String, ByVal rhFlag As Integer, Optional ByVal kaihatsuFugo As String = "") As String
            '↑↑2014/09/25 酒井 ADD END
            Dim color As String = ""
            '先に部品自身の色が存在するか探す'
            '↓↓2014/09/25 酒井 ADD BEGIN
            'color = makeDao.FindByBuhinColor2220(shisakuEventCode, shisakuBlockNo, oriBuhinNo, bfBuhinNo, buhinNo)
            color = makeDao.FindByBuhinColor2220(shisakuEventCode, shisakuBlockNo, oriBuhinNo, bfBuhinNo, buhinNo, yakanFlg)
            '↑↑2014/09/25 酒井 ADD END

            'いないならベース車の情報を元に色を探す'
            If StringUtil.IsEmpty(color) Then

                Select Case rhFlag
                    '↓↓2014/09/25 酒井 ADD BEGIN
                    'Case 0
                    'color = makeDao.FindByBuhinColor0532(shisakuEventCode, _
                    'shisakuBukaCode, _
                    'shisakuBlockNo, _
                    'oriBuhinNo, _
                    'bfBuhinNo, _
                    'buhinNo)
                    'Case 1
                    'color = makeDao.FindByBuhinColor0533(shisakuEventCode, _
                    'shisakuBukaCode, _
                    'shisakuBlockNo, _
                    'oriBuhinNo, _
                    'bfBuhinNo, _
                    'kaihatsuFugo, _
                    'buhinNo)
                    'Case 2
                    'color = makeDao.FindByBuhinColor0530(shisakuEventCode, _
                    'shisakuBukaCode, _
                    'shisakuBlockNo, _
                    'oriBuhinNo, _
                    'bfBuhinNo, _
                    'buhinNo)
                    Case 0
                        color = makeDao.FindByBuhinColor0532(shisakuEventCode, _
                                                             shisakuBukaCode, _
                                                             shisakuBlockNo, _
                                                             oriBuhinNo, _
                                                             bfBuhinNo, _
                                                             buhinNo, yakanFlg)
                    Case 1
                        color = makeDao.FindByBuhinColor0533(shisakuEventCode, _
                                                             shisakuBukaCode, _
                                                             shisakuBlockNo, _
                                                             oriBuhinNo, _
                                                             bfBuhinNo, _
                                                             kaihatsuFugo, _
                                                             buhinNo, yakanFlg)
                    Case 2
                        color = makeDao.FindByBuhinColor0530(shisakuEventCode, _
                                                             shisakuBukaCode, _
                                                             shisakuBlockNo, _
                                                             oriBuhinNo, _
                                                             bfBuhinNo, _
                                                             buhinNo, yakanFlg)
                        '↑↑2014/09/25 酒井 ADD END
                End Select
            End If

            Return color
        End Function

        ''' <summary>
        ''' 部品ごとに色を取得する
        ''' </summary>
        ''' <param name="buhinNo">部品番号</param>
        ''' <param name="rhFlag">0:RHAC0532, 1:RHAC0533, 2:RHAC0530</param>
        ''' <returns>該当する部品の色</returns>
        ''' <remarks></remarks>
        Private Function GetBuhinColorKosei(ByVal buhinNo As String, ByVal rhFlag As Integer, Optional ByVal kaihatsuFugo As String = "", Optional ByVal yakanFlg As Boolean = False) As String
            '↓↓2014/09/25 酒井 ADD BEGIN
            '        Private Function GetBuhinColorKosei(ByVal buhinNo As String, ByVal rhFlag As Integer, Optional ByVal kaihatsuFugo As String = "") As String

            '↑↑2014/09/25 酒井 ADD END
            Dim color As String = ""
            '先に部品自身の色が存在するか探す'
            color = makeDao.FindByBuhinColor2220(shisakuEventCode, shisakuBlockNo, oriBuhinNo, bfBuhinNo, buhinNo)

            'いないならベース車の情報を元に色を探す'
            If StringUtil.IsEmpty(color) Then

                Select Case rhFlag
                    '↓↓2014/09/25 酒井 ADD BEGIN
                    'Case 0
                    'color = makeDao.FindByBuhinColor0532Kosei(shisakuEventCode, _
                    'shisakuBukaCode, _
                    'shisakuBlockNo, _
                    'oriBuhinNo, _
                    'bfBuhinNo, _
                    'buhinNo)
                    'Case 1
                    'color = makeDao.FindByBuhinColor0533Kosei(shisakuEventCode, _
                    'shisakuBukaCode, _
                    'shisakuBlockNo, _
                    'oriBuhinNo, _
                    'bfBuhinNo, _
                    'kaihatsuFugo, _
                    'buhinNo)
                    'Case 2
                    'color = makeDao.FindByBuhinColor0530Kosei(shisakuEventCode, _
                    'shisakuBukaCode, _
                    'shisakuBlockNo, _
                    'oriBuhinNo, _
                    'bfBuhinNo, _
                    'buhinNo)
                    Case 0
                        color = makeDao.FindByBuhinColor0532Kosei(shisakuEventCode, _
                                                             shisakuBukaCode, _
                                                             shisakuBlockNo, _
                                                             oriBuhinNo, _
                                                             bfBuhinNo, _
                                                             buhinNo, yakanFlg)
                    Case 1
                        color = makeDao.FindByBuhinColor0533Kosei(shisakuEventCode, _
                                                             shisakuBukaCode, _
                                                             shisakuBlockNo, _
                                                             oriBuhinNo, _
                                                             bfBuhinNo, _
                                                             kaihatsuFugo, _
                                                             buhinNo, yakanFlg)
                    Case 2
                        color = makeDao.FindByBuhinColor0530Kosei(shisakuEventCode, _
                                                             shisakuBukaCode, _
                                                             shisakuBlockNo, _
                                                             oriBuhinNo, _
                                                             bfBuhinNo, _
                                                             buhinNo, yakanFlg)
                        '↑↑2014/09/25 酒井 ADD END
                End Select
            End If

            Return color
        End Function


        ''' <summary>
        ''' 集計コード無しの構成を除外して返す
        ''' </summary>
        ''' <param name="vos">構成情報</param>
        ''' <returns>除外した構成情報</returns>
        ''' <remarks></remarks>
        Private Function RemoveNonShukei(ByVal vos As List(Of Rhac0552Vo)) As List(Of Rhac0552Vo)
            Dim result As New List(Of Rhac0552Vo)
            For Each vo As Rhac0552Vo In vos
                If StringUtil.IsEmpty(vo.ShukeiCode) AndAlso StringUtil.IsEmpty(vo.SiaShukeiCode) Then
                    Continue For
                End If
                result.Add(vo)
            Next
            Return result
        End Function

        ''' <summary>
        ''' INSTL品番表示順が最小で、部品No表示順に該当する試作部品編集INSTL情報を返す
        ''' </summary>
        ''' <param name="editVo">部品No表示順他をもつキーVo</param>
        ''' <returns>試作部品編集INSTL情報</returns>
        ''' <remarks></remarks>
        Private Function FindMinInstlHinbanHyoujiJunEditInstlBy(ByVal editVo As TShisakuBuhinEditVo) As TShisakuBuhinEditInstlVo

            Dim param As New TShisakuBuhinEditInstlVo
            param.ShisakuEventCode = editVo.ShisakuEventCode
            param.ShisakuBukaCode = editVo.ShisakuBukaCode
            param.ShisakuBlockNo = editVo.ShisakuBlockNo
            param.ShisakuBlockNoKaiteiNo = editVo.ShisakuBlockNoKaiteiNo
            param.BuhinNoHyoujiJun = editVo.BuhinNoHyoujiJun
            Dim editInstlVos As List(Of TShisakuBuhinEditInstlVo) = editInstlDao.FindBy(param)

            Dim result As TShisakuBuhinEditInstlVo = Nothing
            For Each vo As TShisakuBuhinEditInstlVo In editInstlVos
                If result Is Nothing Then
                    result = vo
                Else
                    If vo.InstlHinbanHyoujiJun < result.InstlHinbanHyoujiJun Then
                        result = vo
                    End If
                End If
            Next
            Return result
        End Function
        '↓↓2014/09/25 酒井 ADD BEGIN
        Private Function FindMinInstlHinbanHyoujiJunEditInstlEbomKanshiBy(ByVal editVo As TShisakuBuhinEditVo) As TShisakuBuhinEditInstlEbomKanshiVo

            Dim param As New TShisakuBuhinEditInstlEbomKanshiVo
            param.ShisakuEventCode = editVo.ShisakuEventCode
            param.ShisakuBukaCode = editVo.ShisakuBukaCode
            param.ShisakuBlockNo = editVo.ShisakuBlockNo
            param.ShisakuBlockNoKaiteiNo = editVo.ShisakuBlockNoKaiteiNo
            param.BuhinNoHyoujiJun = editVo.BuhinNoHyoujiJun
            Dim editInstlEbomKanshiDao As TShisakuBuhinEditInstlEbomKanshiDao = New TShisakuBuhinEditInstlEbomKanshiDaoImpl
            Dim editInstlEbomKanshiVos As List(Of TShisakuBuhinEditInstlEbomKanshiVo) = editInstlEbomKanshiDao.FindBy(param)

            Dim result As TShisakuBuhinEditInstlEbomKanshiVo = Nothing
            For Each vo As TShisakuBuhinEditInstlEbomKanshiVo In editInstlEbomKanshiVos
                If result Is Nothing Then
                    result = vo
                Else
                    If vo.InstlHinbanHyoujiJun < result.InstlHinbanHyoujiJun Then
                        result = vo
                    End If
                End If
            Next
            Return result
        End Function
        '↑↑2014/09/25 酒井 ADD END
        Private Function FindEditInstlByInstlHinbanHyoujiJun(ByVal editInstlVo As TShisakuBuhinEditInstlVo) As List(Of TShisakuBuhinEditInstlVo)
            Dim param As New TShisakuBuhinEditInstlVo
            param.ShisakuEventCode = editInstlVo.ShisakuEventCode
            param.ShisakuBukaCode = editInstlVo.ShisakuBukaCode
            param.ShisakuBlockNo = editInstlVo.ShisakuBlockNo
            param.ShisakuBlockNoKaiteiNo = editInstlVo.ShisakuBlockNoKaiteiNo
            param.InstlHinbanHyoujiJun = editInstlVo.InstlHinbanHyoujiJun
            Return editInstlDao.FindBy(param)
        End Function
        '↓↓2014/09/25 酒井 ADD BEGIN
        Private Function FindEditInstlEbomKanshiByInstlHinbanHyoujiJun(ByVal editInstlVo As TShisakuBuhinEditInstlVo) As List(Of TShisakuBuhinEditInstlEbomKanshiVo)
            Dim param As New TShisakuBuhinEditInstlEbomKanshiVo
            param.ShisakuEventCode = editInstlVo.ShisakuEventCode
            param.ShisakuBukaCode = editInstlVo.ShisakuBukaCode
            param.ShisakuBlockNo = editInstlVo.ShisakuBlockNo
            param.ShisakuBlockNoKaiteiNo = editInstlVo.ShisakuBlockNoKaiteiNo
            param.InstlHinbanHyoujiJun = editInstlVo.InstlHinbanHyoujiJun
            Dim editInstlEbomKanshiDao As TShisakuBuhinEditInstlEbomKanshiDao = New TShisakuBuhinEditInstlEbomKanshiDaoImpl
            Return editInstlEbomKanshiDao.FindBy(param)
        End Function
        '↑↑2014/09/25 酒井 ADD END
        ''↓↓2014/08/04 Ⅰ.11.改訂戻し機能　p) (TES)施 修正 BEGIN
        Private Function FindEditInstlByInstlHinbanHyoujiJun(ByVal blockInstlVo As TShisakuSekkeiBlockInstlVo, Optional ByVal KaiteiNo As String = "", Optional ByVal aInstlDataKbn As String = "", Optional ByVal aBaseInstlFlg As String = "", Optional ByVal eventCopyFlg As Boolean = False, Optional ByVal yakanFlg As Boolean = False) As List(Of TShisakuBuhinEditInstlVo)
            '↓↓2014/09/25 酒井 ADD BEGIN
            '        Private Function FindEditInstlByInstlHinbanHyoujiJun(ByVal blockInstlVo As TShisakuSekkeiBlockInstlVo, Optional ByVal KaiteiNo As String = "", Optional ByVal aInstlDataKbn As String = "", Optional ByVal aBaseInstlFlg As String = "") As List(Of TShisakuBuhinEditInstlVo)
            '↑↑2014/09/25 酒井 ADD END
            ''↓↓2014/09/23 酒井 ADD BEGIN
            'Private Function FindEditInstlByInstlHinbanHyoujiJun(ByVal blockInstlVo As TShisakuSekkeiBlockInstlVo, Optional ByVal KaiteiNo As String = "") As List(Of TShisakuBuhinEditInstlVo)
            ''↑↑2014/09/23 酒井 ADD END
            ''↑↑2014/08/04 Ⅰ.11.改訂戻し機能 p) (TES)施 修正 END
            'Dim param As New TShisakuBuhinEditInstlVo
            'param.ShisakuEventCode = blockInstlVo.ShisakuEventCode
            'param.ShisakuBukaCode = blockInstlVo.ShisakuBukaCode
            'param.ShisakuBlockNo = blockInstlVo.ShisakuBlockNo
            'param.ShisakuBlockNoKaiteiNo = blockInstlVo.ShisakuBlockNoKaiteiNo
            'param.InstlHinbanHyoujiJun = blockInstlVo.InstlHinbanHyoujiJun

            'Return editInstlDao.FindBy(param)
            Dim result As New List(Of TShisakuBuhinEditInstlVo)
            ''↓↓2014/08/04 Ⅰ.11.改訂戻し機能　p) (TES)施 修正 BEGIN
            If KaiteiNo = "" Then
                '↓↓2014/09/25 酒井 ADD BEGIN
                '                result = makeDao.FindbyEditInstl(blockInstlVo.ShisakuEventCode, blockInstlVo.InstlHinban, blockInstlVo.InstlHinbanKbn)
                If Not eventCopyFlg Then
                    If yakanFlg Then
                        result = makeDao.FindbyEditInstl(blockInstlVo.ShisakuEventCode, blockInstlVo.InstlHinban, blockInstlVo.InstlHinbanKbn)
                    Else
                        'Dim vos As List(Of TShisakuBuhinEditInstlEbomKanshiVo) = makeDao.FindbyEditInstlEbomKanshi(blockInstlVo.ShisakuEventCode, blockInstlVo.InstlHinban, blockInstlVo.InstlHinbanKbn, "", "", "", False)
                        'For Each Vo As TShisakuBuhinEditInstlEbomKanshiVo In vos
                        '    Dim tmp As New TShisakuBuhinEditInstlVo
                        '    VoUtil.CopyProperties(Vo, tmp)
                        '    result.Add(tmp)
                        'Next
                        result = makeDao.FindbyEditInstl(blockInstlVo.ShisakuEventCode, blockInstlVo.InstlHinban, blockInstlVo.InstlHinbanKbn)
                    End If
                Else
                    result = makeDao.FindbyEditInstl(blockInstlVo.ShisakuEventCode, blockInstlVo.InstlHinban, blockInstlVo.InstlHinbanKbn, "", aInstlDataKbn, aBaseInstlFlg, eventCopyFlg)
                End If
                    '↑↑2014/09/25 酒井 ADD END
            Else
                ''↓↓2014/09/23 酒井 ADD BEGIN
                'result = makeDao.FindbyEditInstl(blockInstlVo.ShisakuEventCode, blockInstlVo.InstlHinban, blockInstlVo.InstlHinbanKbn, KaiteiNo)
                result = makeDao.FindbyEditInstl(blockInstlVo.ShisakuEventCode, blockInstlVo.InstlHinban, blockInstlVo.InstlHinbanKbn, KaiteiNo, aInstlDataKbn, aBaseInstlFlg)
                ''↑↑2014/09/23 酒井 ADD END
            End If

            ''↑↑2014/08/04 Ⅰ.11.改訂戻し機能 p) (TES)施 修正 END
            Return result
        End Function

        Private Function FindEditBy(ByVal editInstlVo As TShisakuBuhinEditInstlVo) As List(Of TShisakuBuhinEditVo)
            Dim param As New TShisakuBuhinEditVo
            param.ShisakuEventCode = editInstlVo.ShisakuEventCode
            param.ShisakuBukaCode = editInstlVo.ShisakuBukaCode
            param.ShisakuBlockNo = editInstlVo.ShisakuBlockNo
            param.ShisakuBlockNoKaiteiNo = editInstlVo.ShisakuBlockNoKaiteiNo

            ''2015/02/26 再使用不可フラグクリア
            Dim rtnval As List(Of TShisakuBuhinEditVo) = editDao.FindBy(param)
            For Each vo As TShisakuBuhinEditVo In rtnval
                vo.Saishiyoufuka = " "
            Next

            Return rtnval
        End Function
        '↓↓2014/09/25 酒井 ADD BEGIN

        Private Function FindEditEbomKanshiBy(ByVal editInstlVo As TShisakuBuhinEditInstlVo) As List(Of TShisakuBuhinEditEbomKanshiVo)
            Dim param As New TShisakuBuhinEditEbomKanshiVo
            param.ShisakuEventCode = editInstlVo.ShisakuEventCode
            param.ShisakuBukaCode = editInstlVo.ShisakuBukaCode
            param.ShisakuBlockNo = editInstlVo.ShisakuBlockNo
            param.ShisakuBlockNoKaiteiNo = editInstlVo.ShisakuBlockNoKaiteiNo
            Dim editEbomKanshiDao As TShisakuBuhinEditEbomKanshiDao = New TShisakuBuhinEditEbomKanshiDaoImpl
            Return editEbomKanshiDao.FindBy(param)
        End Function
        '↑↑2014/09/25 酒井 ADD END
        ''↓↓2014/08/04 Ⅰ.11.改訂戻し機能 s) (TES)施 修正 BEGIN
        Private Function FindEditBy(ByVal blockInstlVo As TShisakuSekkeiBlockInstlVo, Optional ByVal KaiteiNo As String = "", Optional ByVal aInstlDataKbn As String = "", Optional ByVal aBaseInstlFlg As String = "", Optional ByVal eventCopyFlg As Boolean = False, Optional ByVal yakanFlg As Boolean = False) As List(Of TShisakuBuhinEditVo)
            '↓↓2014/09/25 酒井 ADD BEGIN
            '        Private Function FindEditBy(ByVal blockInstlVo As TShisakuSekkeiBlockInstlVo, Optional ByVal KaiteiNo As String = "", Optional ByVal aInstlDataKbn As String = "", Optional ByVal aBaseInstlFlg As String = "") As List(Of TShisakuBuhinEditVo)
            '↑↑2014/09/25 酒井 ADD END
            '2014/09/23 酒井 ADD BEGIN
            'Private Function FindEditBy(ByVal blockInstlVo As TShisakuSekkeiBlockInstlVo, Optional ByVal KaiteiNo As String = "") As List(Of TShisakuBuhinEditVo)
            '2014/09/23 酒井 ADD END

            'Dim param As New TShisakuBuhinEditVo
            ''↑↑2014/08/04 Ⅰ.11.改訂戻し機能 s) (TES)施 修正 END
            'param.ShisakuEventCode = blockInstlVo.ShisakuEventCode
            'param.ShisakuBukaCode = blockInstlVo.ShisakuBukaCode
            'param.ShisakuBlockNo = blockInstlVo.ShisakuBlockNo
            'param.ShisakuBlockNoKaiteiNo = blockInstlVo.ShisakuBlockNoKaiteiNo
            'Return editDao.FindBy(param)
            Dim result As New List(Of TShisakuBuhinEditVo)
            ''↓↓2014/08/04 Ⅰ.11.改訂戻し機能 ｓ) (TES)施 ADD BEGIN
            If KaiteiNo = "" Then
                '↓↓2014/09/25 酒井 ADD BEGIN
                '                result = makeDao.FindbyEdit(blockInstlVo.ShisakuEventCode, blockInstlVo.InstlHinban, blockInstlVo.InstlHinbanKbn)
                If Not eventCopyFlg Then
                    If Not yakanFlg Then
                        result = makeDao.FindbyEdit(blockInstlVo.ShisakuEventCode, blockInstlVo.InstlHinban, blockInstlVo.InstlHinbanKbn)
                    Else
                        Dim vos As List(Of TShisakuBuhinEditEbomKanshiVo) = makeDao.FindbyEditEbomKanshi(blockInstlVo.ShisakuEventCode, blockInstlVo.InstlHinban, blockInstlVo.InstlHinbanKbn, "", "", "", False)
                        For Each Vo As TShisakuBuhinEditEbomKanshiVo In vos
                            Dim tmp As New TShisakuBuhinEditVo
                            VoUtil.CopyProperties(Vo, tmp)
                            result.Add(tmp)
                        Next
                    End If
                Else
                    result = makeDao.FindbyEdit(blockInstlVo.ShisakuEventCode, blockInstlVo.InstlHinban, blockInstlVo.InstlHinbanKbn, "", aInstlDataKbn, aBaseInstlFlg, eventCopyFlg)
                End If
                    '↑↑2014/09/25 酒井 ADD END
            Else
                '2014/09/23 酒井 ADD BEGIN
                'result = makeDao.FindbyEdit(blockInstlVo.ShisakuEventCode, blockInstlVo.InstlHinban, blockInstlVo.InstlHinbanKbn, KaiteiNo)
                result = makeDao.FindbyEdit(blockInstlVo.ShisakuEventCode, blockInstlVo.InstlHinban, blockInstlVo.InstlHinbanKbn, KaiteiNo, aInstlDataKbn, aBaseInstlFlg)
                '2014/09/23 酒井 ADD END
            End If
            ''↑↑2014/08/04 Ⅰ.11.改訂戻し機能 ｓ) (TES)施 ADD END
            Return result
        End Function

        ''' <summary>
        ''' 与えられた取引先情報からだけ、取引先名称を解決する実装クラス
        ''' </summary>
        ''' <remarks></remarks>
        Friend Class MakerNameResolverImpl : Implements MakerNameResolver
            Private ReadOnly makerNameByCode As New Dictionary(Of String, String)
            ''' <summary>
            ''' コンストラクタ
            ''' </summary>
            ''' <param name="rhac0610Vos">取引先情報</param>
            ''' <remarks></remarks>
            Public Sub New(ByVal rhac0610Vos As List(Of Rhac0610Vo))
                Me.New(rhac0610Vos, Nothing)
            End Sub
            ''' <summary>
            ''' コンストラクタ
            ''' </summary>
            ''' <param name="rhac0610Vos">取引先情報</param>
            ''' <param name="aRhac0532MakerNameVo">取引先名称付きの部品情報</param>
            ''' <remarks></remarks>
            Public Sub New(ByVal rhac0610Vos As List(Of Rhac0610Vo), ByVal aRhac0532MakerNameVo As Rhac0532MakerNameVo)
                If rhac0610Vos IsNot Nothing Then
                    For Each vo As Rhac0610Vo In rhac0610Vos
                        makerNameByCode.Add(vo.MakerCode, vo.MakerName)
                    Next
                End If
                If aRhac0532MakerNameVo IsNot Nothing AndAlso aRhac0532MakerNameVo.MakerCode IsNot Nothing Then
                    If Not makerNameByCode.ContainsKey(aRhac0532MakerNameVo.MakerCode) Then
                        makerNameByCode.Add(aRhac0532MakerNameVo.MakerCode, aRhac0532MakerNameVo.MakerName)
                    End If
                End If
            End Sub
            Public Function Resolve(ByVal makerCode As String) As String Implements MakerNameResolver.Resolve
                If makerCode Is Nothing Then
                    Return Nothing
                End If
                If Not makerNameByCode.ContainsKey(makerCode) Then
                    Return Nothing
                End If
                Return makerNameByCode(makerCode)
            End Function
        End Class

#Region "FM5以降～"

        ''' <summary>
        ''' 「構成の情報」を元に部品表を作成する(見づらいので分けます)
        ''' </summary>
        ''' <param name="aStructureResult">構成の情報</param>
        ''' <param name="inputedBuhinNo">部品番号(親)</param>
        ''' <returns>部品表</returns>
        ''' <remarks></remarks>
        Private Function BuhinKousei(ByVal inputedBuhinNo As String, ByVal aStructureResult As StructureResult, ByVal baseLevel As Integer, ByVal kaihatsuFugo As String, Optional ByVal yakanFlg As Boolean = False) As BuhinKoseiMatrix
            '↓↓2014/09/25 酒井 ADD BEGIN
            '        Private Function BuhinKousei(ByVal inputedBuhinNo As String, ByVal aStructureResult As StructureResult, ByVal baseLevel As Integer, ByVal kaihatsuFugo As String) As BuhinKoseiMatrix

            '↑↑2014/09/25 酒井 ADD END
            '0553注記：設計展開時はベース車情報の開発符号を使用'
            '構成再展開、最新化、構成呼び出しは画面側から取得した開発符号を使用'
            '子部品展開はイベント情報の開発符号を使用する'

            '全ての部品構成に対応したVOを用意すれば多重ロジックにならない'
            'RHAC0533にはメーカーコードが存在しない・・・'
            'Dim inputedBuhinNoVo As Rhac0533MakerNameVo = makeDao.FindLastestRhac0533MakerNameByBuhinNo(inputedBuhinNo)
            'kaihatsuFugo.ShisakuKaihatsuFugo = "FM5"

            '2012/01/25 部品ノート追加
            '533のZumenNoteをBuhinNoteとして取得する
            'Dim inputedBuhinNoVo As Rhac0533MakerNameVo = makeDao.FindLastestRhac0533MakerNameByBuhinNo(inputedBuhinNo)
            Dim inputedBuhinNoVo As Rhac0533BuhinNoteVo = makeDao.FindLastestRhac0533MakerNameByBuhinNo(inputedBuhinNo)

            ' 入力部品番号を親品番として全構成を取得
            Dim rhac0553Vos As List(Of Rhac0553Vo) = makeDao.FindStructure0553ByBuhinNoOya(inputedBuhinNo, kaihatsuFugo)

            '構成がナッシング（Count＝０）の場合、親品番を再取得 BY 柳沼
            If rhac0553Vos.Count = 0 Then
                rhac0553Vos = makeDao.FindByBuhinRhac0553LIST(kaihatsuFugo, inputedBuhinNo)
            End If

            ' 入力部品番号を子品番として直親のみの構成を取得
            Dim parentVoForBuhinNo As Rhac0553Vo = makeDao.FindLastestRhac0553ByBuhinNoKo(inputedBuhinNo)

            Rhac0553VoHelper.ResolveRule(rhac0553Vos)
            Rhac0553VoHelper.ResolveRule(parentVoForBuhinNo)

            Dim isInstlHinban As Boolean = parentVoForBuhinNo Is Nothing

            ' 入力部品番号を親品番として取得した全構成の子品番の情報
            Dim rhac0533Vos As List(Of Rhac0533Vo) = makeDao.FindStructure0553ByBuhinNoOyaAnd0533ForKo(inputedBuhinNo, kaihatsuFugo)

            '空文字入りが存在したので'
            For index As Integer = 0 To rhac0533Vos.Count - 1
                Dim str As String = Trim(rhac0533Vos(index).BuhinNo)
                rhac0533Vos(index).BuhinNo = str
            Next

            rhac0533Vos.Add(inputedBuhinNoVo)

            'メーカーコードが無いから取れない'
            'Dim rhac0610Vos As List(Of Rhac0610Vo) = makeDao.FindStructure0553ByBuhinNoOyaAnd0610ForKo(inputedBuhinNo)
            Dim rhac0610Vos As New List(Of Rhac0610Vo)

            If Not isInstlHinban Then
                rhac0553Vos.Add(parentVoForBuhinNo)
                Dim dummy As New Rhac0533Vo
                dummy.BuhinNo = parentVoForBuhinNo.BuhinNoOya
                rhac0533Vos.Add(dummy)
            End If

            Dim colorA As String = ""

            If rhac0553Vos(0).BuhinNoOya.Length = 12 Then
                '↓↓2014/09/25 酒井 ADD BEGIN
                '                colorA = GetBuhinColor(rhac0553Vos(0).BuhinNoOya, 1)
                colorA = GetBuhinColor(rhac0553Vos(0).BuhinNoOya, 1, "", False)
                '↑↑2014/09/25 酒井 ADD END
            Else
                colorA = ""
            End If

            '12桁未満ならNothingが返ってくる'
            Dim colorCode As String = ShisakuRule.GetColorFromHinban(aStructureResult.OriginalBuhinNo)

            If StringUtil.IsEmpty(colorCode) Then
                colorCode = ""
            End If

            '基準の色が同じなら'
            If StringUtil.Equals(colorCode, colorA) OrElse StringUtil.IsEmpty(colorCode) Then
                For Each Vo As Rhac0553Vo In rhac0553Vos
                    If Vo.BuhinNoOya.Length = 12 AndAlso StringUtil.Equals(Right(Vo.BuhinNoOya, 2), "##") Then
                        '↓↓2014/09/25 酒井 ADD BEGIN
                        '                        Dim color As String = GetBuhinColor(Vo.BuhinNoOya, 1)
                        Dim color As String = GetBuhinColor(Vo.BuhinNoOya, 1, "", yakanFlg)
                        '↑↑2014/09/25 酒井 ADD END
                        If StringUtil.IsEmpty(color) Then
                            If Not StringUtil.IsEmpty(colorCode) Then
                                '自身の色が取れなかった場合は親の色を振る'
                                Vo.BuhinNoOya = Left(Vo.BuhinNoOya, 10) + colorCode
                            End If
                        Else
                            '自身の色が取れた場合は自身の色を振る'
                            Vo.BuhinNoOya = Left(Vo.BuhinNoOya, 10) + color
                        End If

                    End If
                    If Vo.BuhinNoKo.Length = 12 AndAlso StringUtil.Equals(Right(Vo.BuhinNoKo, 2), "##") Then
                        '↓↓2014/09/25 酒井 ADD BEGIN
                        '                        Dim color As String = GetBuhinColor(Vo.BuhinNoKo, 1)
                        Dim color As String = GetBuhinColor(Vo.BuhinNoKo, 1, "", yakanFlg)
                        '↑↑2014/09/25 酒井 ADD END
                        If StringUtil.IsEmpty(color) Then
                            If Not StringUtil.IsEmpty(colorCode) Then
                                '自身の色が取れなかった場合は親の色を振る'
                                Vo.BuhinNoKo = Left(Vo.BuhinNoKo, 10) + colorCode
                            End If
                        Else
                            '自身の色が取れた場合は自身の色を振る'
                            Vo.BuhinNoKo = Left(Vo.BuhinNoKo, 10) + color
                        End If
                    End If
                Next
                For Each Vo As Rhac0533Vo In rhac0533Vos
                    If Vo.BuhinNo.Length = 12 AndAlso StringUtil.Equals(Right(Vo.BuhinNo, 2), "##") Then
                        '↓↓2014/09/25 酒井 ADD BEGIN
                        '                        Dim color As String = GetBuhinColor(Vo.BuhinNo, 1)
                        Dim color As String = GetBuhinColor(Vo.BuhinNo, 1, "", yakanFlg)
                        '↑↑2014/09/25 酒井 ADD END
                        If StringUtil.IsEmpty(color) Then
                            If Not StringUtil.IsEmpty(colorCode) Then
                                '自身の色が取れなかった場合は親の色を振る'
                                Vo.BuhinNo = Left(Vo.BuhinNo, 10) + colorCode
                            End If
                        Else
                            '自身の色が取れた場合は自身の色を振る'
                            Vo.BuhinNo = Left(Vo.BuhinNo, 10) + color
                        End If
                    End If
                Next
            Else
                If Not StringUtil.IsEmpty(colorCode) Then
                    ' rhac0552Vosの中の"##"品番を色に置換える
                    UpdateColor0553(colorCode, rhac0553Vos)

                    ' rhac0532Vosの中の"##"品番を色に置換える
                    UpdateColor0533(colorCode, rhac0533Vos)
                End If
            End If

            '2012/03/16 スペースの存在で取れない構成がいるので'
            For Each Vo As Rhac0533Vo In rhac0533Vos
                If Not StringUtil.IsEmpty(Vo.BuhinNo) Then
                    Vo.BuhinNo = Trim(Vo.BuhinNo)
                End If
            Next
            For Each Vo As Rhac0553Vo In rhac0553Vos
                If Not StringUtil.IsEmpty(Vo.BuhinNoOya) Then
                    Vo.BuhinNoOya = Trim(Vo.BuhinNoOya)
                End If
                If Not StringUtil.IsEmpty(Vo.BuhinNoKo) Then
                    Vo.BuhinNoKo = Trim(Vo.BuhinNoKo)
                End If
            Next

            '2012/07/26　集計コードが無い場合、構成データが0件になってエラーが出てしまうことを回避するために以下の処理へ変更
            '   集計コードがが無い場合ダミー集計コードを付けて返す。
            'Dim singleMatrices As BuhinSingleMatrix(Of Rhac0553Vo, Rhac0533Vo)() = BuhinTreeMaker.NewSingleMatrices(RemoveNonShukeiRhac0553(rhac0553Vos), rhac0533Vos)
            Dim singleMatrices As BuhinSingleMatrix(Of Rhac0553Vo, Rhac0533Vo)() = BuhinTreeMaker.NewSingleMatrices(RemoveNonShukeiDummyRhac0553(rhac0553Vos), rhac0533Vos)

            If singleMatrices.Length <> 1 Then
                Throw New InvalidProgramException("SQLで1品番の構成だけ取って来ているのに NewSingleMatrices で構成が " & singleMatrices.Length & " 個")
            End If

            'Dim mergeNode As New MergeNodeList(Of Rhac0553Vo, Rhac0533Vo)(New RhacNodeAccessor(New MakerNameResolverImplRhac0553(rhac0610Vos, inputedBuhinNoVo)))
            Dim mergeNode As New MergeNodeList(Of Rhac0553Vo, Rhac0533Vo)(New Rhac0553NodeAccessor(New MakerNameResolverImplRhac0553(Nothing, inputedBuhinNoVo)))

            mergeNode.Compute(singleMatrices(0).Nodes)

            Dim newKoseiMatrix As BuhinKoseiMatrix = mergeNode.ResultMatrix
            If Not isInstlHinban Then
                newKoseiMatrix.RemoveRow(0) ' parentVoForBuhinNo.BuhinNoOya が先頭にいるから削除しておく
            End If
            newKoseiMatrix.CorrectLevelBy(baseLevel)
            'ここでやってみるか'
            For Each index As Integer In newKoseiMatrix.GetInputRowIndexes
                If Not StringUtil.IsEmpty(newKoseiMatrix(index).BuhinNo) Then
                    Dim MakerVo As New TShisakuBuhinEditVo
                    Dim impl As ShisakuBuhinMenu.Dao.BuhinEditBaseDao = New ShisakuBuhinMenu.Dao.BuhinEditBaseDaoImpl
                    '/*** 20140911 CHANGE START（引数追加） ***/
                    'MakerVo = impl.FindByKoutanTorihikisaki(newKoseiMatrix(index).BuhinNo)
                    MakerVo = impl.FindByKoutanTorihikisakiUseDictionary(newKoseiMatrix(index).BuhinNo, kotanTorihikisakiSelected)
                    '/*** 20140911 CHANGE END ***/

                    newKoseiMatrix(index).MakerCode = MakerVo.MakerCode
                    newKoseiMatrix(index).MakerName = MakerVo.MakerName
                End If
            Next
            '供給セクションを振る'
            If Not newKoseiMatrix Is Nothing Then
                For Each index As Integer In newKoseiMatrix.GetInputRowIndexes
                    Dim BuhinNo As String = ""
                    If Not StringUtil.IsEmpty(newKoseiMatrix(index).BuhinNo) Then

                        '集計コードを確認する'
                        If StringUtil.IsEmpty(newKoseiMatrix(index).ShukeiCode) Then
                            '海外集計'
                            If StringUtil.Equals(newKoseiMatrix(index).SiaShukeiCode, "X") Or StringUtil.Equals(newKoseiMatrix(index).SiaShukeiCode, "R") Or StringUtil.Equals(newKoseiMatrix(index).SiaShukeiCode, "J") Then
                                '供給セクションは空'
                            ElseIf StringUtil.Equals(newKoseiMatrix(index).SiaShukeiCode, "A") Then
                                newKoseiMatrix(index).KyoukuSection = "9SH10"
                            ElseIf StringUtil.Equals(newKoseiMatrix(index).SiaShukeiCode, "E") Or StringUtil.Equals(newKoseiMatrix(index).SiaShukeiCode, "Y") Then
                                '親の供給セクションを取得する'
                                BuhinNo = newKoseiMatrix(index).BuhinNo
                                newKoseiMatrix = SetKyoukuSection0553(BuhinNo, BuhinNo, rhac0553Vos, newKoseiMatrix)
                            End If
                        Else
                            '国内集計'
                            If StringUtil.Equals(newKoseiMatrix(index).ShukeiCode, "X") Or StringUtil.Equals(newKoseiMatrix(index).ShukeiCode, "R") Or StringUtil.Equals(newKoseiMatrix(index).ShukeiCode, "J") Then
                                '供給セクションは空'
                            ElseIf StringUtil.Equals(newKoseiMatrix(index).ShukeiCode, "A") Then
                                newKoseiMatrix(index).KyoukuSection = "9SH10"
                            ElseIf StringUtil.Equals(newKoseiMatrix(index).ShukeiCode, "E") Or StringUtil.Equals(newKoseiMatrix(index).ShukeiCode, "Y") Then
                                '親の供給セクションを取得する'
                                BuhinNo = newKoseiMatrix(index).BuhinNo
                                newKoseiMatrix = SetKyoukuSection0553(BuhinNo, BuhinNo, rhac0553Vos, newKoseiMatrix)
                            End If

                        End If
                    End If
                Next
            End If

            Return newKoseiMatrix
        End Function

        ''' <summary>
        ''' 供給セクションをふる
        ''' </summary>
        ''' <param name="buhinNo">子部品番号</param>
        ''' <param name="originalBuhinNo">供給セクションを追加する部品番号</param>
        ''' <param name="rhac0553Vos"></param>
        ''' <param name="KoseiMatrix"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Private Function SetKyoukuSection0553(ByVal buhinNo As String, ByVal originalBuhinNo As String, ByVal rhac0553Vos As List(Of Rhac0553Vo), ByVal KoseiMatrix As BuhinKoseiMatrix) As BuhinKoseiMatrix

            '部品番号の存在をチェック'
            For Each Vo As Rhac0553Vo In rhac0553Vos
                If StringUtil.Equals(buhinNo, Vo.BuhinNoKo) Then
                    Dim buhinNoOya As String = Vo.BuhinNoOya
                    '存在したら親部品が構成マトリクスに存在するかチェックする'
                    For Each index As Integer In KoseiMatrix.GetInputRowIndexes
                        If StringUtil.Equals(KoseiMatrix(index).BuhinNo, buhinNoOya) Then
                            '存在したら集計コードをチェック'
                            If StringUtil.IsEmpty(KoseiMatrix(index).ShukeiCode) Then
                                '海外集計'
                                If StringUtil.Equals(KoseiMatrix(index).SiaShukeiCode, "J") Then
                                    'Jなら一段階上の親を探す'
                                    KoseiMatrix = SetKyoukuSection0553(KoseiMatrix(index).BuhinNo, originalBuhinNo, rhac0553Vos, KoseiMatrix)
                                Else
                                    'J以外ならメーカーコード+0で供給セクションを追加'
                                    For Each index2 As Integer In KoseiMatrix.GetInputRowIndexes
                                        If StringUtil.Equals(KoseiMatrix(index2).BuhinNo, originalBuhinNo) Then
                                            Dim KyoukuSection As String = ""
                                            If Not StringUtil.IsEmpty(KoseiMatrix(index).MakerCode) Then
                                                KyoukuSection = KoseiMatrix(index).MakerCode + "0"
                                            End If
                                            KoseiMatrix(index2).KyoukuSection = KyoukuSection
                                            Return KoseiMatrix
                                        End If
                                    Next
                                End If
                            Else
                                '国内集計'
                                '海外集計'
                                If StringUtil.Equals(KoseiMatrix(index).ShukeiCode, "J") Then
                                    'Jなら一段階上の親を探す'
                                    KoseiMatrix = SetKyoukuSection0553(KoseiMatrix(index).BuhinNo, originalBuhinNo, rhac0553Vos, KoseiMatrix)
                                Else
                                    'J以外ならメーカーコード+0で供給セクションを追加'
                                    For Each index2 As Integer In KoseiMatrix.GetInputRowIndexes
                                        If StringUtil.Equals(KoseiMatrix(index2).BuhinNo, originalBuhinNo) Then
                                            Dim KyoukuSection As String = ""
                                            If Not StringUtil.IsEmpty(KoseiMatrix(index).MakerCode) Then
                                                KyoukuSection = KoseiMatrix(index).MakerCode + "0"
                                            End If
                                            KoseiMatrix(index2).KyoukuSection = KyoukuSection
                                            Return KoseiMatrix
                                        End If
                                    Next
                                End If
                            End If

                        End If
                    Next
                End If
            Next
            Return KoseiMatrix
        End Function

        ''' <summary>
        ''' 構成情報の色コード変換(FM5～)
        ''' </summary>
        ''' <param name="vos">構成情報</param>
        ''' <returns>色コード（##→色コード）を変換した構成情報</returns>
        ''' <remarks></remarks>
        Private Function UpdateColor0553(ByVal colorCode As String, ByVal vos As List(Of Rhac0553Vo)) As List(Of Rhac0553Vo)
            Dim result As New List(Of Rhac0553Vo)
            For Each vo As Rhac0553Vo In vos
                vo.BuhinNoOya = ShisakuRule.ReplaceColor(vo.BuhinNoOya, colorCode)
                vo.BuhinNoKo = ShisakuRule.ReplaceColor(vo.BuhinNoKo, colorCode)
                result.Add(vo)
            Next
            Return result
        End Function

        ''' <summary>
        ''' 部品情報の色コード変換(FM5～)
        ''' </summary>
        ''' <param name="vos">部品情報</param>
        ''' <returns>色コード（##→色コード）を変換した部品情報</returns>
        ''' <remarks></remarks>
        Private Function UpdateColor0533(ByVal colorCode As String, ByVal vos As List(Of Rhac0533Vo)) As List(Of Rhac0533Vo)
            Dim result As New List(Of Rhac0533Vo)
            For Each vo As Rhac0533Vo In vos
                vo.BuhinNo = ShisakuRule.ReplaceColor(vo.BuhinNo, colorCode)
                result.Add(vo)
            Next
            Return result
        End Function

        ''' <summary>
        ''' 集計コード無しの構成を除外して返す(FM5以降～)
        ''' </summary>
        ''' <param name="vos">構成情報</param>
        ''' <returns>除外した構成情報</returns>
        ''' <remarks></remarks>
        Private Function RemoveNonShukeiRhac0553(ByVal vos As List(Of Rhac0553Vo)) As List(Of Rhac0553Vo)
            Dim result As New List(Of Rhac0553Vo)
            For Each vo As Rhac0553Vo In vos
                If StringUtil.IsEmpty(vo.ShukeiCode) AndAlso StringUtil.IsEmpty(vo.SiaShukeiCode) Then
                    Continue For
                End If
                result.Add(vo)
            Next
            Return result
        End Function

        '------------------------------------------------------------------------------------------------------
        '2012/07/26　集計コードが無いと構成展開でエラーが出てしまうデータを回避するための処理を追加
        ''' <summary>
        ''' 集計コード無しの構成はダミー集計コードを付けて返す(FM5以降～)
        ''' </summary>
        ''' <param name="vos">構成情報</param>
        ''' <returns>ダミー集計コードを追加した構成情報</returns>
        ''' <remarks></remarks>
        Private Function RemoveNonShukeiDummyRhac0553(ByVal vos As List(Of Rhac0553Vo)) As List(Of Rhac0553Vo)
            Dim result As New List(Of Rhac0553Vo)
            For Each vo As Rhac0553Vo In vos
                If StringUtil.IsEmpty(vo.ShukeiCode) AndAlso StringUtil.IsEmpty(vo.SiaShukeiCode) Then
                    vo.ShukeiCode = "Z"
                End If
                result.Add(vo)
            Next
            Return result
        End Function
        '------------------------------------------------------------------------------------------------------

        ''' <summary>
        ''' 与えられた取引先情報からだけ、取引先名称を解決する実装クラス(FM5以降～)
        ''' </summary>
        ''' <remarks></remarks>
        Friend Class MakerNameResolverImplRhac0553 : Implements MakerNameResolver
            Private ReadOnly makerNameByCode As New Dictionary(Of String, String)
            ''' <summary>
            ''' コンストラクタ
            ''' </summary>
            ''' <param name="rhac0610Vos">取引先情報</param>
            ''' <remarks></remarks>
            Public Sub New(ByVal rhac0610Vos As List(Of Rhac0610Vo))
                Me.New(rhac0610Vos, Nothing)
            End Sub
            ''' <summary>
            ''' コンストラクタ
            ''' </summary>
            ''' <param name="rhac0610Vos">取引先情報</param>
            ''' <param name="aRhac0533MakerNameVo">取引先名称付きの部品情報</param>
            ''' <remarks></remarks>
            Public Sub New(ByVal rhac0610Vos As List(Of Rhac0610Vo), ByVal aRhac0533MakerNameVo As Rhac0533MakerNameVo)
                If rhac0610Vos IsNot Nothing Then
                    For Each vo As Rhac0610Vo In rhac0610Vos
                        makerNameByCode.Add(vo.MakerCode, vo.MakerName)
                    Next
                End If
                If aRhac0533MakerNameVo IsNot Nothing AndAlso aRhac0533MakerNameVo.MakerCode IsNot Nothing Then
                    If Not makerNameByCode.ContainsKey(aRhac0533MakerNameVo.MakerCode) Then
                        makerNameByCode.Add(aRhac0533MakerNameVo.MakerCode, aRhac0533MakerNameVo.MakerName)
                    End If
                End If
            End Sub
            Public Function Resolve(ByVal makerCode As String) As String Implements MakerNameResolver.Resolve
                If makerCode Is Nothing Then
                    Return Nothing
                End If
                If Not makerNameByCode.ContainsKey(makerCode) Then
                    Return Nothing
                End If
                Return makerNameByCode(makerCode)
            End Function
        End Class

#End Region


#Region "パンダ前"

        ''' <summary>
        ''' 「構成の情報」を元に部品表を作成する(見づらいので分けます)
        ''' </summary>
        ''' <param name="aStructureResult">構成の情報</param>
        ''' <param name="inputedBuhinNo">部品番号(親)</param>
        ''' <returns>部品表</returns>
        ''' <remarks></remarks>
        Private Function BuhinKouseiRhac0551(ByVal inputedBuhinNo As String, ByVal aStructureResult As StructureResult, ByVal baseLevel As Integer, Optional ByVal yakanFlg As Boolean = False) As BuhinKoseiMatrix
            '↓↓2014/09/25 酒井 ADD BEGIN
            '        Private Function BuhinKouseiRhac0551(ByVal inputedBuhinNo As String, ByVal aStructureResult As StructureResult, ByVal baseLevel As Integer) As BuhinKoseiMatrix

            '↑↑2014/09/25 酒井 ADD END
            '全ての部品構成に対応したVOを用意すれば多重ロジックにならない'
            'RHAC0533にはメーカーコードが存在しない・・・'
            'Dim inputedBuhinNoVo As Rhac0533MakerNameVo = makeDao.FindLastestRhac0533MakerNameByBuhinNo(inputedBuhinNo)

            Dim inputedBuhinNoVo As Rhac0530MakerNameVo = makeDao.FindLastestRhac0530MakerNameByBuhinNo(inputedBuhinNo)

            ' 入力部品番号を親品番として全構成を取得
            Dim rhac0551Vos As List(Of Rhac0551Vo) = makeDao.FindStructure0551ByBuhinNoOya(inputedBuhinNo)

            '構成がナッシング（Count＝０）の場合、親品番を再取得 BY 柳沼
            If rhac0551Vos.Count = 0 Then
                rhac0551Vos = makeDao.FindByBuhinRhac0551LIST(inputedBuhinNo)
            End If
            'それでも無い場合があるので'
            If rhac0551Vos.Count = 0 Then
                Return Nothing
            End If

            ' 入力部品番号を子品番として直親のみの構成を取得
            Dim parentVoForBuhinNo As Rhac0551Vo = makeDao.FindLastestRhac0551ByBuhinNoKo(inputedBuhinNo)

            '親部品の集計コードを取得し、入れ替える。
            Dim ShuKeiCD As Rhac0530Vo = makeDao.FindLastestRhac0530ByBuhinNoOya(inputedBuhinNo)
            If Not StringUtil.IsEmpty(parentVoForBuhinNo) Then
                parentVoForBuhinNo.ShukeiCode = ShuKeiCD.ShukeiCode '国内集計コード
                parentVoForBuhinNo.SiaShukeiCode = ShuKeiCD.SiaShukeiCode '海外集計コード
                'Else

                '    '構成が無い場合（マスタメンテ異常時だけだが・・・）NEWを返す。
                '    Return New BuhinKoseiMatrix


                '    ''構成が無い場合（マスタメンテ異常時だけだが・・・）０レベルを１件作成する。
                '    ''あと処理でエラーが出るためダミーデータを作成しておく。
                '    'Dim parentVoForBuhinNoNashi As New Rhac0551Vo
                '    'parentVoForBuhinNoNashi.BuhinNoOya = inputedBuhinNo
                '    'parentVoForBuhinNoNashi.BuhinNoKo = "-----ZZ"
                '    'parentVoForBuhinNoNashi.KaiteiNo = "000"
                '    'parentVoForBuhinNoNashi.ShukeiCode = ShuKeiCD.ShukeiCode '国内集計コード
                '    'parentVoForBuhinNoNashi.SiaShukeiCode = ShuKeiCD.SiaShukeiCode '海外集計コード
                '    'parentVoForBuhinNoNashi.InsuSuryo = 1
                '    'parentVoForBuhinNo = parentVoForBuhinNoNashi
                '    ''構成がナッシング（Count＝０）の場合セット BY 柳沼
                '    'If rhac0551Vos.Count = 0 Then
                '    '    rhac0551Vos.Add(parentVoForBuhinNo)
                '    'End If

            End If

            Rhac0551VoHelper.ResolveRule(rhac0551Vos)
            Rhac0551VoHelper.ResolveRule(parentVoForBuhinNo)

            Dim isInstlHinban As Boolean = parentVoForBuhinNo Is Nothing

            ' 入力部品番号を親品番として取得した全構成の子品番の情報
            Dim rhac0530Vos As List(Of Rhac0530Vo) = makeDao.FindStructure0551ByBuhinNoOyaAnd0530ForKo(inputedBuhinNo)

            '空文字入りが存在したので'
            For index As Integer = 0 To rhac0530Vos.Count - 1
                Dim str As String = Trim(rhac0530Vos(index).BuhinNo)
                rhac0530Vos(index).BuhinNo = str
            Next

            rhac0530Vos.Add(inputedBuhinNoVo)

            'メーカーコードが無いから取れない'
            'Dim rhac0610Vos As List(Of Rhac0610Vo) = makeDao.FindStructure0553ByBuhinNoOyaAnd0610ForKo(inputedBuhinNo)
            Dim rhac0610Vos As New List(Of Rhac0610Vo)

            If Not isInstlHinban Then
                rhac0551Vos.Add(parentVoForBuhinNo)
                Dim dummy As New Rhac0530Vo
                dummy.BuhinNo = parentVoForBuhinNo.BuhinNoOya
                rhac0530Vos.Add(dummy)
            End If

            Dim colorA As String = ""

            If rhac0551Vos(0).BuhinNoOya.Length = 12 Then
                If koseiFlag = 1 Then
                    '↓↓2014/09/25 酒井 ADD BEGIN
                    'colorA = GetBuhinColorKosei(rhac0551Vos(0).BuhinNoOya, 0)
                    colorA = GetBuhinColorKosei(rhac0551Vos(0).BuhinNoOya, 0, "", yakanFlg)
                    '↑↑2014/09/25 酒井 ADD END
                Else
                    '↓↓2014/09/25 酒井 ADD BEGIN
                    '                    colorA = GetBuhinColor(rhac0551Vos(0).BuhinNoOya, 0)
                    colorA = GetBuhinColor(rhac0551Vos(0).BuhinNoOya, 0, "", yakanFlg)
                    '↑↑2014/09/25 酒井 ADD END
                End If

            Else
                colorA = ""
            End If

            '12桁未満ならNothingが返ってくる'
            Dim colorCode As String = ShisakuRule.GetColorFromHinban(aStructureResult.OriginalBuhinNo)

            If StringUtil.IsEmpty(colorCode) Then
                colorCode = ""
            End If

            '基準の色が同じなら'
            If StringUtil.Equals(colorCode, colorA) OrElse StringUtil.IsEmpty(colorCode) Then
                For Each Vo As Rhac0551Vo In rhac0551Vos
                    If Vo.BuhinNoOya.Length = 12 AndAlso StringUtil.Equals(Right(Vo.BuhinNoOya, 2), "##") Then

                        Dim color As String = ""
                        If koseiFlag = 1 Then
                            '↓↓2014/09/25 酒井 ADD BEGIN
                            '                            color = GetBuhinColorKosei(Vo.BuhinNoOya, 2)
                            color = GetBuhinColorKosei(Vo.BuhinNoOya, 2, "", yakanFlg)
                            '↑↑2014/09/25 酒井 ADD END
                        Else
                            '↓↓2014/09/25 酒井 ADD BEGIN
                            '                            color = GetBuhinColor(Vo.BuhinNoOya, 2)
                            color = GetBuhinColor(Vo.BuhinNoOya, 2, "", yakanFlg)
                            '↑↑2014/09/25 酒井 ADD END
                        End If


                        If StringUtil.IsEmpty(color) Then
                            If Not StringUtil.IsEmpty(colorCode) Then
                                '自身の色が取れなかった場合は親の色を振る'
                                Vo.BuhinNoOya = Left(Vo.BuhinNoOya, 10) + colorCode
                            End If
                        Else
                            '自身の色が取れた場合は自身の色を振る'
                            Vo.BuhinNoOya = Left(Vo.BuhinNoOya, 10) + color
                        End If

                    End If
                    If Vo.BuhinNoKo.Length = 12 AndAlso StringUtil.Equals(Right(Vo.BuhinNoKo, 2), "##") Then
                        Dim color As String = ""
                        If koseiFlag = 1 Then
                            '↓↓2014/09/25 酒井 ADD BEGIN
                            '                            color = GetBuhinColorKosei(Vo.BuhinNoOya, 2)
                            color = GetBuhinColorKosei(Vo.BuhinNoOya, 2, "", yakanFlg)
                            '↑↑2014/09/25 酒井 ADD END
                        Else
                            '↓↓2014/09/25 酒井 ADD BEGIN
                            '                            color = GetBuhinColor(Vo.BuhinNoOya, 2)
                            color = GetBuhinColor(Vo.BuhinNoOya, 2, "", yakanFlg)
                            '↑↑2014/09/25 酒井 ADD END
                        End If
                        If StringUtil.IsEmpty(color) Then
                            If Not StringUtil.IsEmpty(colorCode) Then
                                '自身の色が取れなかった場合は親の色を振る'
                                Vo.BuhinNoKo = Left(Vo.BuhinNoKo, 10) + colorCode
                            End If
                        Else
                            '自身の色が取れた場合は自身の色を振る'
                            Vo.BuhinNoKo = Left(Vo.BuhinNoKo, 10) + color
                        End If
                    End If
                Next
                For Each Vo As Rhac0530Vo In rhac0530Vos
                    If Vo.BuhinNo.Length = 12 AndAlso StringUtil.Equals(Right(Vo.BuhinNo, 2), "##") Then
                        Dim color As String = ""
                        If koseiFlag = 1 Then
                            '↓↓2014/09/25 酒井 ADD BEGIN
                            '                            color = GetBuhinColorKosei(Vo.BuhinNo, 2)
                            color = GetBuhinColorKosei(Vo.BuhinNo, 2, "", yakanFlg)
                            '↑↑2014/09/25 酒井 ADD END
                        Else
                            '↓↓2014/09/25 酒井 ADD BEGIN
                            '                            color = GetBuhinColor(Vo.BuhinNo, 2)
                            color = GetBuhinColor(Vo.BuhinNo, 2, "", yakanFlg)
                            '↑↑2014/09/25 酒井 ADD END
                        End If
                        If StringUtil.IsEmpty(color) Then
                            If Not StringUtil.IsEmpty(colorCode) Then
                                '自身の色が取れなかった場合は親の色を振る'
                                Vo.BuhinNo = Left(Vo.BuhinNo, 10) + colorCode
                            End If
                        Else
                            '自身の色が取れた場合は自身の色を振る'
                            Vo.BuhinNo = Left(Vo.BuhinNo, 10) + color
                        End If
                    End If
                Next
            Else
                If Not StringUtil.IsEmpty(colorCode) Then
                    ' rhac0552Vosの中の"##"品番を色に置換える
                    UpdateColor0551(colorCode, rhac0551Vos)

                    ' rhac0532Vosの中の"##"品番を色に置換える
                    UpdateColor0530(colorCode, rhac0530Vos)
                End If
            End If

            '2012/03/16 スペースの存在で取れない構成がいるので'
            For Each Vo As Rhac0530Vo In rhac0530Vos
                If Not StringUtil.IsEmpty(Vo.BuhinNo) Then
                    Vo.BuhinNo = Trim(Vo.BuhinNo)
                End If
            Next
            For Each Vo As Rhac0551Vo In rhac0551Vos
                If Not StringUtil.IsEmpty(Vo.BuhinNoOya) Then
                    Vo.BuhinNoOya = Trim(Vo.BuhinNoOya)
                End If
                If Not StringUtil.IsEmpty(Vo.BuhinNoKo) Then
                    Vo.BuhinNoKo = Trim(Vo.BuhinNoKo)
                End If
            Next


            Dim singleMatrices As BuhinSingleMatrix(Of Rhac0551Vo, Rhac0530Vo)() = BuhinTreeMaker.NewSingleMatrices(rhac0551Vos, rhac0530Vos)
            'Dim singleMatrices As BuhinSingleMatrix(Of Rhac0551Vo, Rhac0530Vo)() = BuhinTreeMaker.NewSingleMatrices(RemoveNonShukeiRhac0551(rhac0551Vos), rhac0530Vos)

            '2013/09/10 ５５１の場合、１構成で２親品番が見つかる事があるので以下のロジックはスルーでも問題ない。
            If singleMatrices.Length <> 1 Then
                'Throw New InvalidProgramException("SQLで1品番の構成だけ取って来ているのに NewSingleMatrices で構成が " & singleMatrices.Length & " 個")
            End If

            Dim mergeNode As New MergeNodeList(Of Rhac0551Vo, Rhac0530Vo)(New Rhac0551NodeAccessor(New MakerNameResolverImplRhac0551(Nothing, inputedBuhinNoVo)))
            'Dim mergeNode As New MergeNodeList(Of Rhac0551Vo, Rhac0530Vo)(New Rhac0553NodeAccessor(New MakerNameResolverImplRhac0553(Nothing, inputedBuhinNoVo)))

            mergeNode.Compute(singleMatrices(0).Nodes)

            Dim newKoseiMatrix As BuhinKoseiMatrix = mergeNode.ResultMatrix
            If Not isInstlHinban Then
                newKoseiMatrix.RemoveRow(0) ' parentVoForBuhinNo.BuhinNoOya が先頭にいるから削除しておく
            End If
            newKoseiMatrix.CorrectLevelBy(baseLevel)
            'ここでやってみるか'
            For Each index As Integer In newKoseiMatrix.GetInputRowIndexes
                If Not StringUtil.IsEmpty(newKoseiMatrix(index).BuhinNo) Then
                    If StringUtil.IsEmpty(newKoseiMatrix(index).MakerCode) And StringUtil.IsEmpty(newKoseiMatrix(index).MakerName) Then
                        Dim MakerVo As New TShisakuBuhinEditVo
                        Dim impl As ShisakuBuhinMenu.Dao.BuhinEditBaseDao = New ShisakuBuhinMenu.Dao.BuhinEditBaseDaoImpl
                        '/*** 20140911 CHANGE START（引数追加） ***/
                        'MakerVo = impl.FindByKoutanTorihikisaki(newKoseiMatrix(index).BuhinNo)
                        MakerVo = impl.FindByKoutanTorihikisakiUseDictionary(newKoseiMatrix(index).BuhinNo, kotanTorihikisakiSelected)
                        '/*** 20140911 CHANGE END ***/

                        newKoseiMatrix(index).MakerCode = MakerVo.MakerCode
                        newKoseiMatrix(index).MakerName = MakerVo.MakerName
                    End If
                End If
            Next
            '供給セクションを振る'
            If Not newKoseiMatrix Is Nothing Then
                For Each index As Integer In newKoseiMatrix.GetInputRowIndexes
                    Dim BuhinNo As String = ""
                    If Not StringUtil.IsEmpty(newKoseiMatrix(index).BuhinNo) Then

                        '集計コードを確認する'
                        If StringUtil.IsEmpty(newKoseiMatrix(index).ShukeiCode) Then
                            '海外集計'
                            If StringUtil.Equals(newKoseiMatrix(index).SiaShukeiCode, "X") Or StringUtil.Equals(newKoseiMatrix(index).SiaShukeiCode, "R") Or StringUtil.Equals(newKoseiMatrix(index).SiaShukeiCode, "J") Then
                                '供給セクションは空'
                            ElseIf StringUtil.Equals(newKoseiMatrix(index).SiaShukeiCode, "A") Then
                                newKoseiMatrix(index).KyoukuSection = "9SH10"
                            ElseIf StringUtil.Equals(newKoseiMatrix(index).SiaShukeiCode, "E") Or StringUtil.Equals(newKoseiMatrix(index).SiaShukeiCode, "Y") Then
                                '親の供給セクションを取得する'
                                BuhinNo = newKoseiMatrix(index).BuhinNo
                                newKoseiMatrix = SetKyoukuSection0551(BuhinNo, BuhinNo, rhac0551Vos, newKoseiMatrix)
                            End If
                        Else
                            '国内集計'
                            If StringUtil.Equals(newKoseiMatrix(index).ShukeiCode, "X") Or StringUtil.Equals(newKoseiMatrix(index).ShukeiCode, "R") Or StringUtil.Equals(newKoseiMatrix(index).ShukeiCode, "J") Then
                                '供給セクションは空'
                            ElseIf StringUtil.Equals(newKoseiMatrix(index).ShukeiCode, "A") Then
                                newKoseiMatrix(index).KyoukuSection = "9SH10"
                            ElseIf StringUtil.Equals(newKoseiMatrix(index).ShukeiCode, "E") Or StringUtil.Equals(newKoseiMatrix(index).ShukeiCode, "Y") Then
                                '親の供給セクションを取得する'
                                BuhinNo = newKoseiMatrix(index).BuhinNo
                                newKoseiMatrix = SetKyoukuSection0551(BuhinNo, BuhinNo, rhac0551Vos, newKoseiMatrix)
                            End If

                        End If
                    End If
                Next
            End If

            Return newKoseiMatrix
        End Function

        ''' <summary>
        ''' 供給セクションをふる
        ''' </summary>
        ''' <param name="buhinNo">子部品番号</param>
        ''' <param name="originalBuhinNo">供給セクションを追加する部品番号</param>
        ''' <param name="rhac0551Vos"></param>
        ''' <param name="KoseiMatrix"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Private Function SetKyoukuSection0551(ByVal buhinNo As String, ByVal originalBuhinNo As String, ByVal rhac0551Vos As List(Of Rhac0551Vo), ByVal KoseiMatrix As BuhinKoseiMatrix) As BuhinKoseiMatrix


            If StringUtil.Equals(originalBuhinNo, "83071FJ000") _
                OrElse StringUtil.Equals(originalBuhinNo, "83071FSJ00") _
                OrElse StringUtil.Equals(originalBuhinNo, "83071FSJ04") Then
                Dim a As String = ""
            End If

            '部品番号の存在をチェック'
            For Each Vo As Rhac0551Vo In rhac0551Vos
                If StringUtil.Equals(buhinNo, Vo.BuhinNoKo) Then
                    Dim buhinNoOya As String = Vo.BuhinNoOya
                    '存在したら親部品が構成マトリクスに存在するかチェックする'
                    For Each index As Integer In KoseiMatrix.GetInputRowIndexes
                        If StringUtil.Equals(KoseiMatrix(index).BuhinNo, buhinNoOya) Then
                            '存在したら集計コードをチェック'
                            If StringUtil.IsEmpty(KoseiMatrix(index).ShukeiCode) Then
                                '海外集計'
                                If StringUtil.Equals(KoseiMatrix(index).SiaShukeiCode, "J") Then
                                    'Jなら一段階上の親を探す'
                                    KoseiMatrix = SetKyoukuSection0551(KoseiMatrix(index).BuhinNo, originalBuhinNo, rhac0551Vos, KoseiMatrix)
                                Else
                                    'J以外ならメーカーコード+0で供給セクションを追加'
                                    For Each index2 As Integer In KoseiMatrix.GetInputRowIndexes
                                        If StringUtil.Equals(KoseiMatrix(index2).BuhinNo, originalBuhinNo) Then
                                            Dim KyoukuSection As String = ""
                                            If Not StringUtil.IsEmpty(KoseiMatrix(index).MakerCode) Then
                                                KyoukuSection = KoseiMatrix(index).MakerCode + "0"
                                            End If
                                            KoseiMatrix(index2).KyoukuSection = KyoukuSection
                                            Return KoseiMatrix
                                        End If

                                    Next
                                End If
                            Else
                                '国内集計'
                                '海外集計'
                                If StringUtil.Equals(KoseiMatrix(index).ShukeiCode, "J") Then
                                    'Jなら一段階上の親を探す'
                                    KoseiMatrix = SetKyoukuSection0551(KoseiMatrix(index).BuhinNo, originalBuhinNo, rhac0551Vos, KoseiMatrix)
                                Else
                                    'J以外ならメーカーコード+0で供給セクションを追加'
                                    For Each index2 As Integer In KoseiMatrix.GetInputRowIndexes
                                        If StringUtil.Equals(KoseiMatrix(index2).BuhinNo, originalBuhinNo) Then
                                            Dim KyoukuSection As String = ""
                                            If Not StringUtil.IsEmpty(KoseiMatrix(index).MakerCode) Then
                                                KyoukuSection = KoseiMatrix(index).MakerCode + "0"
                                            End If
                                            KoseiMatrix(index2).KyoukuSection = KyoukuSection
                                            Return KoseiMatrix
                                        End If
                                    Next
                                End If
                            End If

                        End If
                    Next
                End If
            Next
            Return KoseiMatrix
        End Function

        ''' <summary>
        ''' 構成情報の色コード変換(パンダ前)
        ''' </summary>
        ''' <param name="vos">構成情報</param>
        ''' <returns>色コード（##→色コード）を変換した構成情報</returns>
        ''' <remarks></remarks>
        Private Function UpdateColor0551(ByVal colorCode As String, ByVal vos As List(Of Rhac0551Vo)) As List(Of Rhac0551Vo)
            Dim result As New List(Of Rhac0551Vo)
            For Each vo As Rhac0551Vo In vos
                vo.BuhinNoOya = ShisakuRule.ReplaceColor(vo.BuhinNoOya, colorCode)
                vo.BuhinNoKo = ShisakuRule.ReplaceColor(vo.BuhinNoKo, colorCode)
                result.Add(vo)
            Next
            Return result
        End Function

        ''' <summary>
        ''' 部品情報の色コード変換(パンダ前)
        ''' </summary>
        ''' <param name="vos">部品情報</param>
        ''' <returns>色コード（##→色コード）を変換した部品情報</returns>
        ''' <remarks></remarks>
        Private Function UpdateColor0530(ByVal colorCode As String, ByVal vos As List(Of Rhac0530Vo)) As List(Of Rhac0530Vo)
            Dim result As New List(Of Rhac0530Vo)
            For Each vo As Rhac0530Vo In vos
                vo.BuhinNo = ShisakuRule.ReplaceColor(vo.BuhinNo, colorCode)
                result.Add(vo)
            Next
            Return result
        End Function

        ''' <summary>
        ''' 集計コード無しの構成を除外して返す(パンダ前)
        ''' </summary>
        ''' <param name="vos">構成情報</param>
        ''' <returns>除外した構成情報</returns>
        ''' <remarks></remarks>
        Private Function RemoveNonShukeiRhac0551(ByVal vos As List(Of Rhac0551Vo)) As List(Of Rhac0551Vo)
            Dim result As New List(Of Rhac0551Vo)
            For Each vo As Rhac0551Vo In vos
                If StringUtil.IsEmpty(vo.ShukeiCode) AndAlso StringUtil.IsEmpty(vo.SiaShukeiCode) Then
                    Continue For
                End If
                result.Add(vo)
            Next
            Return result
        End Function

        ''' <summary>
        ''' 与えられた取引先情報からだけ、取引先名称を解決する実装クラス(FM5以降～)
        ''' </summary>
        ''' <remarks></remarks>
        Friend Class MakerNameResolverImplRhac0551 : Implements MakerNameResolver
            Private ReadOnly makerNameByCode As New Dictionary(Of String, String)
            ''' <summary>
            ''' コンストラクタ
            ''' </summary>
            ''' <param name="rhac0610Vos">取引先情報</param>
            ''' <remarks></remarks>
            Public Sub New(ByVal rhac0610Vos As List(Of Rhac0610Vo))
                Me.New(rhac0610Vos, Nothing)
            End Sub

            ''' <summary>
            ''' コンストラクタ
            ''' </summary>
            ''' <param name="rhac0610Vos">取引先情報</param>
            ''' <param name="aRhac0530MakerNameVo">取引先名称付きの部品情報</param>
            ''' <remarks></remarks>
            Public Sub New(ByVal rhac0610Vos As List(Of Rhac0610Vo), ByVal aRhac0530MakerNameVo As Rhac0530MakerNameVo)
                If rhac0610Vos IsNot Nothing Then
                    For Each vo As Rhac0610Vo In rhac0610Vos
                        makerNameByCode.Add(vo.MakerCode, vo.MakerName)
                    Next
                End If
                If aRhac0530MakerNameVo IsNot Nothing AndAlso aRhac0530MakerNameVo.MakerCode IsNot Nothing Then
                    If Not makerNameByCode.ContainsKey(aRhac0530MakerNameVo.MakerCode) Then
                        makerNameByCode.Add(aRhac0530MakerNameVo.MakerCode, aRhac0530MakerNameVo.MakerName)
                    End If
                End If
            End Sub

            Public Function Resolve(ByVal makerCode As String) As String Implements MakerNameResolver.Resolve
                If makerCode Is Nothing Then
                    Return Nothing
                End If
                If Not makerNameByCode.ContainsKey(makerCode) Then
                    Return Nothing
                End If
                Return makerNameByCode(makerCode)
            End Function
        End Class

#End Region


#Region "自給品の構成を除外する"

        ''' <summary>
        ''' 自給品の構成を除外する
        ''' </summary>
        ''' <returns>除外した構成情報</returns>
        ''' <remarks></remarks>
        Private Function RemoveNonJikyu(ByVal newKoseiMatrixNotJikyu As BuhinKoseiMatrix)
            Dim indexes As New List(Of Integer)(newKoseiMatrixNotJikyu.GetInputRowIndexes())
            For Each index As Integer In indexes
                If newKoseiMatrixNotJikyu.Record(index).Level = "0" Then
                    Continue For
                End If
                '以下の条件にマッチしたら除外する。
                If newKoseiMatrixNotJikyu.Record(index).ShukeiCode = "J" Or _
                   newKoseiMatrixNotJikyu.Record(index).ShukeiCode <> "J" _
                                    AndAlso newKoseiMatrixNotJikyu.Record(index).SiaShukeiCode = "J" Or _
                   StringUtil.IsEmpty(newKoseiMatrixNotJikyu.Record(index).ShukeiCode) _
                                    AndAlso newKoseiMatrixNotJikyu.Record(index).SiaShukeiCode = "J" Then

                    '自給品の構成を除去する。
                    newKoseiMatrixNotJikyu.RemoveRow(index)

                End If

            Next

            Return newKoseiMatrixNotJikyu

        End Function

#End Region

#Region "集計コードが無い構成を除外する"

        ''' <summary>
        ''' 集計コードが無い構成を除外する
        ''' </summary>
        ''' <returns>除外した構成情報</returns>
        ''' <remarks></remarks>
        Private Function RemoveNonShukeiCode(ByVal newKoseiMatrixNotShukeiCode As BuhinKoseiMatrix)
            Dim indexes As New List(Of Integer)(newKoseiMatrixNotShukeiCode.GetInputRowIndexes())
            For Each index As Integer In indexes
                If newKoseiMatrixNotShukeiCode.Record(index).Level = "0" Then
                    Continue For
                End If
                '以下の条件にマッチしたら除外する。
                If StringUtil.IsEmpty(newKoseiMatrixNotShukeiCode.Record(index).ShukeiCode) _
                                    AndAlso StringUtil.IsEmpty(newKoseiMatrixNotShukeiCode.Record(index).ShukeiCode) Then

                    '自給品の構成を除去する。
                    newKoseiMatrixNotShukeiCode.RemoveRow(index)

                End If

            Next

            Return newKoseiMatrixNotShukeiCode

        End Function

#End Region

#Region "集計コードがＲの子部品を構成情報から除外する"

        ''' <summary>
        ''' 集計コードがＲの子部品を構成情報から除外する
        ''' </summary>
        ''' <returns>Ｒの子部品を除外した構成情報</returns>
        ''' <remarks></remarks>
        Private Function RemoveNonRbuhin(ByVal newKoseiMatrixNotRbuhin As BuhinKoseiMatrix)
            Dim indexes As New List(Of Integer)(newKoseiMatrixNotRbuhin.GetInputRowIndexes())
            Dim zenLevel As Integer = 0
            Dim wStartFlg As String = Nothing
            Dim I As Integer = 0

            For Each index As Integer In indexes

                'Ｒ部品の下位レベル部品の場合、不要構成情報なので削除する。
                If newKoseiMatrixNotRbuhin.Record(index - I).Level > 0 Then

                    If newKoseiMatrixNotRbuhin.Record(index - 1 - I).Level < newKoseiMatrixNotRbuhin.Record(index - I).Level Then

                        If (newKoseiMatrixNotRbuhin.Record(index - 1 - I).ShukeiCode = "R" _
                            And newKoseiMatrixNotRbuhin.Record(index - 1 - I).SiaShukeiCode = "R") Or _
                           (newKoseiMatrixNotRbuhin.Record(index - 1 - I).ShukeiCode = "R" _
                            And newKoseiMatrixNotRbuhin.Record(index - 1 - I).SiaShukeiCode <> "R") Or _
                           (newKoseiMatrixNotRbuhin.Record(index - 1 - I).ShukeiCode = " " _
                            And newKoseiMatrixNotRbuhin.Record(index - 1 - I).SiaShukeiCode = "R") Then

                            newKoseiMatrixNotRbuhin.RemoveRow(index - I)
                            I = I + 1

                        End If

                    End If

                End If

                ''集計コード（国内／海外）がＲの場合レベルを保持する。
                'If (newKoseiMatrixNotRbuhin.Record(index - I).ShukeiCode = "R" _
                '    And newKoseiMatrixNotRbuhin.Record(index - I).SiaShukeiCode = "R") Or _
                '   (newKoseiMatrixNotRbuhin.Record(index - I).ShukeiCode = "R" _
                '    And newKoseiMatrixNotRbuhin.Record(index - I).SiaShukeiCode <> "R") Or _
                '   (newKoseiMatrixNotRbuhin.Record(index - I).ShukeiCode = " " _
                '    And newKoseiMatrixNotRbuhin.Record(index - I).SiaShukeiCode = "R") Then
                '    If zenLevel = 0 Or zenLevel > newKoseiMatrixNotRbuhin.Record(index - I).Level Then
                '        zenLevel = newKoseiMatrixNotRbuhin.Record(index - I).Level
                '        wStartFlg = "S"
                '    End If
                'End If
                ''Ｒ部品の下位レベル部品の場合、不要構成情報なので削除する。
                'If zenLevel < newKoseiMatrixNotRbuhin.Record(index - I).Level _
                '   And wStartFlg = "S" Then
                '    If zenLevel < newKoseiMatrixNotRbuhin.Record(index - I).Level Then
                '        newKoseiMatrixNotRbuhin.RemoveRow(index - I)
                '        I = I + 1
                '    Else
                '        wStartFlg = Nothing
                '    End If
                '    'Else
                '    '    wStartFlg = Nothing
                'End If
            Next

            Return newKoseiMatrixNotRbuhin
            ''Ｒの子部品の構成情報を除外するため空情報をセットする。
            'newKoseiMatrixNotRbuhin.Record(index).Bikou = Nothing
            'newKoseiMatrixNotRbuhin.Record(index).BuhinName = Nothing
            'newKoseiMatrixNotRbuhin.Record(index).BuhinNo = Nothing
            'newKoseiMatrixNotRbuhin.Record(index).BuhinNoHyoujiJun = Nothing
            'newKoseiMatrixNotRbuhin.Record(index).BuhinNoKaiteiNo = Nothing
            'newKoseiMatrixNotRbuhin.Record(index).BuhinNoKbn = Nothing
            'newKoseiMatrixNotRbuhin.Record(index).CreatedDate = Nothing
            'newKoseiMatrixNotRbuhin.Record(index).CreatedTime = Nothing
            'newKoseiMatrixNotRbuhin.Record(index).CreatedUserId = Nothing
            'newKoseiMatrixNotRbuhin.Record(index).EdaBan = Nothing
            'newKoseiMatrixNotRbuhin.Record(index).EditTourokubi = Nothing
            'newKoseiMatrixNotRbuhin.Record(index).EditTourokujikan = Nothing
            'newKoseiMatrixNotRbuhin.Record(index).GencyoCkdKbn = Nothing
            'newKoseiMatrixNotRbuhin.Record(index).KaiteiHandanFlg = Nothing
            'newKoseiMatrixNotRbuhin.Record(index).Level = Nothing
            'newKoseiMatrixNotRbuhin.Record(index).MakerCode = Nothing
            'newKoseiMatrixNotRbuhin.Record(index).MakerName = Nothing
            'newKoseiMatrixNotRbuhin.Record(index).Saishiyoufuka = Nothing
            'newKoseiMatrixNotRbuhin.Record(index).ShisakuBankoSuryo = Nothing
            'newKoseiMatrixNotRbuhin.Record(index).ShisakuBankoSuryoU = Nothing
            'newKoseiMatrixNotRbuhin.Record(index).ShisakuBlockNo = Nothing
            'newKoseiMatrixNotRbuhin.Record(index).ShisakuBlockNoKaiteiNo = Nothing
            'newKoseiMatrixNotRbuhin.Record(index).ShisakuBuhinHi = Nothing
            'newKoseiMatrixNotRbuhin.Record(index).ShisakuBukaCode = Nothing
            'newKoseiMatrixNotRbuhin.Record(index).ShisakuEventCode = Nothing
            'newKoseiMatrixNotRbuhin.Record(index).ShisakuKataHi = Nothing
            'newKoseiMatrixNotRbuhin.Record(index).ShisakuListCode = Nothing
            'newKoseiMatrixNotRbuhin.Record(index).ShukeiCode = Nothing
            'newKoseiMatrixNotRbuhin.Record(index).ShutuzuYoteiDate = Nothing
            'newKoseiMatrixNotRbuhin.Record(index).SiaShukeiCode = Nothing
            'newKoseiMatrixNotRbuhin.Record(index).UpdatedDate = Nothing
            'newKoseiMatrixNotRbuhin.Record(index).UpdatedTime = Nothing
            'newKoseiMatrixNotRbuhin.Record(index).UpdatedUserId = Nothing
            'newKoseiMatrixNotRbuhin.Record(index).ZaishituKikaku1 = Nothing
            'newKoseiMatrixNotRbuhin.Record(index).ZaishituKikaku2 = Nothing
            'newKoseiMatrixNotRbuhin.Record(index).ZaishituKikaku3 = Nothing
            'newKoseiMatrixNotRbuhin.Record(index).ZaishituMekki = Nothing
            ''員数も消去
            'newKoseiMatrixNotRbuhin.InsuVo(index, 0).BuhinNoHyoujiJun = Nothing
            'newKoseiMatrixNotRbuhin.InsuVo(index, 0).CreatedDate = Nothing
            'newKoseiMatrixNotRbuhin.InsuVo(index, 0).CreatedTime = Nothing
            'newKoseiMatrixNotRbuhin.InsuVo(index, 0).CreatedUserId = Nothing
            'newKoseiMatrixNotRbuhin.InsuVo(index, 0).InstlHinbanHyoujiJun = Nothing
            'newKoseiMatrixNotRbuhin.InsuVo(index, 0).InsuSuryo = Nothing
            'newKoseiMatrixNotRbuhin.InsuVo(index, 0).SaisyuKoushinbi = Nothing
            'newKoseiMatrixNotRbuhin.InsuVo(index, 0).ShisakuBlockNo = Nothing
            'newKoseiMatrixNotRbuhin.InsuVo(index, 0).ShisakuBlockNoKaiteiNo = Nothing
            'newKoseiMatrixNotRbuhin.InsuVo(index, 0).ShisakuBukaCode = Nothing
            'newKoseiMatrixNotRbuhin.InsuVo(index, 0).ShisakuEventCode = Nothing
            'newKoseiMatrixNotRbuhin.InsuVo(index, 0).UpdatedDate = Nothing
            'newKoseiMatrixNotRbuhin.InsuVo(index, 0).UpdatedTime = Nothing
            'newKoseiMatrixNotRbuhin.InsuVo(index, 0).UpdatedUserId = Nothing
        End Function

#End Region

    End Class
End Namespace