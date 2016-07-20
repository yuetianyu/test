Imports ShisakuCommon
Imports ShisakuCommon.Db.EBom
Imports ShisakuCommon.Db.EBom.Dao
Imports ShisakuCommon.Db.EBom.Dao.Impl
Imports ShisakuCommon.Db.EBom.Vo
Imports ShisakuCommon.Db.EBom.Vo.Helper
Imports YosansyoTool.YosanBuhinEdit.Logic
Imports YosansyoTool.YosanBuhinEdit.Logic.Detect
Imports YosansyoTool.YosanBuhinEdit.Kosei.Dao
Imports YosansyoTool.YosanBuhinEdit.Kosei.Logic.Merge
Imports YosansyoTool.YosanBuhinEdit.Kosei.Logic.Tree

Namespace YosanBuhinEdit.Kosei.Logic.Matrix
    ''' <summary>
    ''' 「構成の情報」で部品表を作成するメソッドクラス
    ''' </summary>
    ''' <remarks></remarks>
    Public Class MakeStructureResultImpl
        Implements MakeStructureResult

        Private ReadOnly editDao As TYosanBuhinEditDao
        Private ReadOnly editInsuDao As TYosanBuhinEditInsuDao
        Private ReadOnly makeDao As MakeStructureResultDao
        Private shisakuEventCode As String
        Private shisakuBukaCode As String
        Private shisakuBlockNo As String
        '0553に至る場合用にどこからきたのか判断させる'
        '設計展開時：0, 構成再展開、最新化、部品構成呼び出し時：1, 子部品展開時：2'
        Private a0553flag As Integer
        Private KaihatsuFugo As String

        ''' <summary>
        ''' コンストラクタ
        ''' </summary>
        ''' <remarks></remarks>
        Public Sub New()
            Me.New(New TYosanBuhinEditDaoImpl, New TYosanBuhinEditInsuDaoImpl, New MakeStructureResultDaoImpl)
        End Sub

        ''' <summary>
        ''' コンストラクタ
        ''' </summary>
        ''' <remarks></remarks>
        Public Sub New(ByVal shisakuEventCode As String, ByVal shisakuBukaCode As String, ByVal shisakuBlockNo As String)
            Me.New(New TYosanBuhinEditDaoImpl, New TYosanBuhinEditInsuDaoImpl, New MakeStructureResultDaoImpl)
            Me.shisakuEventCode = shisakuEventCode
            Me.shisakuBukaCode = shisakuBukaCode
            Me.shisakuBlockNo = shisakuBlockNo
        End Sub

        ''' <summary>
        ''' コンストラクタ
        ''' </summary>
        ''' <param name="editDao">試作部品編集Dao</param>
        ''' <param name="editInsuDao">試作部品編集INSTL Dao</param>
        ''' <param name="makeDao">当メソッドクラス専用Dao</param>
        ''' <remarks></remarks>
        Public Sub New(ByVal editDao As TYosanBuhinEditDao, ByVal editInsuDao As TYosanBuhinEditInsuDao, ByVal makeDao As MakeStructureResultDao)
            Me.editDao = editDao
            Me.editInsuDao = editInsuDao
            Me.makeDao = makeDao
        End Sub

        '構成再展開から開発符号をセットする'
        Public Sub KaihatsuFugoSet(ByVal KaihatsuFugo As String) Implements MakeStructureResult.KaihatsuFugoSet
            Me.KaihatsuFugo = KaihatsuFugo
        End Sub

        ''' <summary>
        ''' 「構成の情報」を元に部品表を作成する
        ''' </summary>
        ''' <param name="aStructureResult">構成の情報</param>
        ''' <returns>部品表</returns>
        ''' <remarks></remarks>baseLevel
        Public Function Compute(ByVal aStructureResult As StructureResult, _
                                ByVal baseLevel As Integer?, _
                                ByVal kaihatsuFugo As String) As BuhinKoseiMatrix Implements MakeStructureResult.Compute
            Return Compute(aStructureResult, a0553flag, baseLevel, kaihatsuFugo)
        End Function

        Private oriBuhinNo As String
        Private bfBuhinNo As String

        ''' <summary>
        ''' 「構成の情報」を元に部品表を作成する(自給品有り)
        ''' </summary>
        ''' <param name="aStructureResult">構成の情報</param>
        ''' <param name="_a0553flag">設計展開時：0, 構成再展開、最新化、部品構成呼び出し時：1, 子部品展開時：2'</param>
        ''' <param name="baseLevel">基点のレベル</param>
        ''' <returns>部品表</returns>
        ''' <remarks></remarks>
        Public Function Compute(ByVal aStructureResult As StructureResult, _
                                ByVal _a0553flag As Integer, _
                                ByVal baseLevel As Integer?, _
                                ByVal kaihatsuFugo As String) As BuhinKoseiMatrix Implements MakeStructureResult.Compute
            If Not aStructureResult.IsExist Then
                Return New BuhinKoseiMatrix
            End If

            oriBuhinNo = aStructureResult.OriginalBuhinNo
            bfBuhinNo = aStructureResult.OriginalBuhinNo

            If aStructureResult.YobidashiMoto <> "" Then
                Return Compute2(aStructureResult, _a0553flag, baseLevel)
            End If

            If Not aStructureResult.IsEBom Then

                If aStructureResult.EditVo IsNot Nothing Then
                    ' 5keyで最小のINSTL品番表示順のBuhinEditInstl

                    Dim editInsuVo As TYosanBuhinEditInsuVo = FindMinInstlHinbanHyoujiJunEditInstlBy(aStructureResult.EditVo)

                    Dim editInsuVos As List(Of TYosanBuhinEditInsuVo) = FindEditInstlByInstlHinbanHyoujiJun(editInsuVo)

                    '自給品を含めて取得する。
                    Dim editVos As List(Of TYosanBuhinEditVo) = FindEditBy(editInsuVo)

                    Dim result As BuhinKoseiMatrix = BuhinKoseiMatrix.NewInnerJoinHyoujiJun(editVos, editInsuVos)
                    result = result.ExtractUnderLevel(aStructureResult.EditVo.BuhinNoHyoujiJun)
                    result.RemoveNullRecords()
                    result.CorrectLevelBy(baseLevel)
                    Return result

                ElseIf aStructureResult.InstlVo IsNot Nothing Then
                    ' 
                    Dim editInsuVos As List(Of TYosanBuhinEditInsuVo) = FindEditInstlByInstlHinbanHyoujiJun(aStructureResult.InstlVo)

                    '自給品を含めて取得する。
                    Dim editVos As List(Of TYosanBuhinEditVo) = FindEditBy(aStructureResult.InstlVo)

                    Dim result As BuhinKoseiMatrix = BuhinKoseiMatrix.NewInnerJoinHyoujiJun(editVos, editInsuVos)
                    result.RemoveNullRecords()
                    result.CorrectLevelBy(baseLevel)
                    Return result
                End If
                Throw New ArgumentException("プロパティの内容が不正", "aStructureResult")
            End If

            Dim inputedBuhinNo As String = aStructureResult.BuhinNo

            'ベース車情報の開発符号毎に0532を見るのか0533を見るのか分けたい'
            Dim Rhac0552Vo As New Rhac0552Vo

            Rhac0552Vo = makeDao.FindByBuhinRhac0552(inputedBuhinNo)

            If Rhac0552Vo Is Nothing Then
                Dim BfBuhinNoVo As New TShisakuSekkeiBlockInstlVo
                Dim checkVo As New Rhac0553Vo

                If _a0553flag = 0 Then
                    '設計展開時の処理'
                    If StringUtil.IsEmpty(kaihatsuFugo) Then
                        Dim eventVo As New THoyouEventVo
                        eventVo = makeDao.FindByEvent(shisakuEventCode)
                        kaihatsuFugo = eventVo.HoyouKaihatsuFugo
                    End If

                ElseIf _a0553flag = 2 Then
                    '子部品展開時の処理'
                    'イベント情報から開発符号を取得する'
                    Dim eventVo As New THoyouEventVo
                    eventVo = makeDao.FindByEvent(shisakuEventCode)
                    kaihatsuFugo = eventVo.HoyouKaihatsuFugo
                Else
                    '構成再展開、最新化、構成呼び出し時の処理'
                    If StringUtil.IsEmpty(kaihatsuFugo) Then
                        Dim eventVo As New THoyouEventVo
                        eventVo = makeDao.FindByKaihatsuFugo(shisakuEventCode)
                        kaihatsuFugo = eventVo.HoyouKaihatsuFugo
                    End If
                End If

                checkVo = makeDao.FindByBuhinRhac0553(kaihatsuFugo, inputedBuhinNo)
                If Not checkVo Is Nothing Then
                    Return BuhinKousei(inputedBuhinNo, aStructureResult, baseLevel, kaihatsuFugo)
                Else
                    '553にも無ければ551を見に行く'
                    Dim checkRhac0551Vo As New Rhac0551Vo
                    checkRhac0551Vo = makeDao.FindByBuhinRhac0551(inputedBuhinNo)
                    Return BuhinKouseiRhac0551(inputedBuhinNo, aStructureResult, baseLevel)
                End If
            End If
            Dim inputedBuhinNoVo As Rhac0532BuhinNoteVo = makeDao.FindLastestRhac0532MakerNameByBuhinNo(inputedBuhinNo)

            ' 入力部品番号を親品番として全構成を取得
            Dim rhac0552Vos As List(Of Rhac0552Vo) = makeDao.FindStructure0552ByBuhinNoOya(inputedBuhinNo)

            Dim parentVoForBuhinNo As New Rhac0552Vo

            Dim newKoseiMatrix As New BuhinKoseiMatrix

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

            'For Each Vo As Rhac0532Vo In rhac0532Vos
            '    If Not StringUtil.IsEmpty(Vo.BuhinNo) Then
            '        Vo.BuhinNo = Trim(Vo.BuhinNo)
            '    End If
            'Next
            'For Each Vo As Rhac0552Vo In rhac0552Vos
            '    If Not StringUtil.IsEmpty(Vo.BuhinNoOya) Then
            '        Vo.BuhinNoOya = Trim(Vo.BuhinNoOya)
            '    End If
            '    If Not StringUtil.IsEmpty(Vo.BuhinNoKo) Then
            '        Vo.BuhinNoKo = Trim(Vo.BuhinNoKo)
            '    End If
            'Next

            '集計コードブランクを除いて構成を返す。
            Dim singleMatrices As BuhinSingleMatrix(Of Rhac0552Vo, Rhac0532Vo)() = BuhinTreeMaker.NewSingleMatrices(rhac0552Vos, rhac0532Vos)

            If singleMatrices.Length <> 1 Then
                Throw New InvalidProgramException("SQLで1品番の構成だけ取って来ているのに NewSingleMatrices で構成が " & singleMatrices.Length & " 個")
            End If

            Dim mergeNode As New MergeNodeList(Of Rhac0552Vo, Rhac0532Vo)(New RhacNodeAccessor(New MakerNameResolverImpl(rhac0610Vos, inputedBuhinNoVo)))

            mergeNode.Compute(singleMatrices(0).Nodes)

            newKoseiMatrix = mergeNode.ResultMatrix
            If Not isInstlHinban Then
                newKoseiMatrix.RemoveRow(0)
            End If

            '余分な行が存在した場合削除する'
            For Each index As Integer In newKoseiMatrix.GetInputRowIndexes
                If index < 0 Then
                    newKoseiMatrix.RemoveRow(index)
                ElseIf StringUtil.IsEmpty(newKoseiMatrix(index).YosanBuhinNo) Then
                    newKoseiMatrix.RemoveRow(index)
                End If
            Next

            '取引先名称の取得が遅い？？？？　２０１５・３・３　柳沼
            '' 2015/05/07 取引先表示が必要なので復活させました E.Ubukata

            ''余分な行を削除した後で取引先コードと名称を取得する'
            For Each index As Integer In newKoseiMatrix.GetInputRowIndexes
                If StringUtil.IsEmpty(newKoseiMatrix(index).YosanMakerCode) And StringUtil.IsEmpty(newKoseiMatrix(index).YosanMakerName) Then
                    If Not StringUtil.IsEmpty(newKoseiMatrix(index).YosanBuhinNo) Then
                        Dim MakerVo As New TShisakuBuhinEditVo
                        MakerVo = Me.FindByKoutanTorihikisaki(newKoseiMatrix(index).YosanBuhinNo)
                        newKoseiMatrix(index).YosanMakerCode = MakerVo.MakerCode
                        newKoseiMatrix(index).YosanMakerName = MakerVo.MakerName
                    End If

                End If
            Next

            '供給セクションを振る'
            Dim BuhinNo As String = ""
            Dim impl As Dao.MakeStructureResultDao = New Dao.MakeStructureResultDaoImpl
            Dim vo As New THoyouBuhinEditVo
            If Not newKoseiMatrix Is Nothing Then
                For Each index As Integer In newKoseiMatrix.GetInputRowIndexes
                    BuhinNo = ""
                    If Not StringUtil.IsEmpty(newKoseiMatrix(index).YosanBuhinNo) Then

                        '集計コードを再設定する'"R"
                        If (newKoseiMatrix(index).YosanShukeiCode = "R") _
                        Or (newKoseiMatrix(index).YosanSiaShukeiCode = "R") Then

                            vo = impl.FindByBuhinShukei(newKoseiMatrix(index).YosanBuhinNo)
                            If Not vo Is Nothing Then
                                If Not StringUtil.IsEmpty(vo.ShukeiCode) _
                                Or Not StringUtil.IsEmpty(vo.SiaShukeiCode) Then
                                    newKoseiMatrix(index).YosanShukeiCode = vo.ShukeiCode
                                    newKoseiMatrix(index).YosanSiaShukeiCode = vo.SiaShukeiCode
                                End If
                            End If
                        End If

                        '集計コードを確認する'
                        If StringUtil.IsEmpty(newKoseiMatrix(index).YosanShukeiCode) Then
                            '海外集計'
                            If StringUtil.Equals(newKoseiMatrix(index).YosanSiaShukeiCode, "X") Or StringUtil.Equals(newKoseiMatrix(index).YosanSiaShukeiCode, "R") Or StringUtil.Equals(newKoseiMatrix(index).YosanSiaShukeiCode, "J") Then
                                '供給セクションは空'
                            ElseIf StringUtil.Equals(newKoseiMatrix(index).YosanSiaShukeiCode, "A") Then
                                '供給セクション「09SX00」固定'
                                newKoseiMatrix(index).YosanKyoukuSection = "9SX00"
                            ElseIf StringUtil.Equals(newKoseiMatrix(index).YosanSiaShukeiCode, "E") Or StringUtil.Equals(newKoseiMatrix(index).YosanSiaShukeiCode, "Y") Then
                                '親の供給セクションを取得する'
                                BuhinNo = newKoseiMatrix(index).YosanBuhinNo
                                newKoseiMatrix = SetKyoukuSection0552(BuhinNo, BuhinNo, rhac0552Vos, newKoseiMatrix)
                            End If
                        Else
                            '国内集計'
                            If StringUtil.Equals(newKoseiMatrix(index).YosanShukeiCode, "X") Or StringUtil.Equals(newKoseiMatrix(index).YosanShukeiCode, "R") Or StringUtil.Equals(newKoseiMatrix(index).YosanShukeiCode, "J") Then
                                '供給セクションは空'
                            ElseIf StringUtil.Equals(newKoseiMatrix(index).YosanShukeiCode, "A") Then
                                '供給セクション「09SX00」固定'
                                newKoseiMatrix(index).YosanKyoukuSection = "9SX00"
                            ElseIf StringUtil.Equals(newKoseiMatrix(index).YosanShukeiCode, "E") Or StringUtil.Equals(newKoseiMatrix(index).YosanShukeiCode, "Y") Then
                                '親の供給セクションを取得する'
                                BuhinNo = newKoseiMatrix(index).YosanBuhinNo
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

                    Dim editInsuVo As TYosanBuhinEditInsuVo = FindMinInstlHinbanHyoujiJunEditInstlBy(aStructureResult.EditVo)

                    Dim editInsuVos As List(Of TYosanBuhinEditInsuVo) = FindEditInstlByInstlHinbanHyoujiJun(editInsuVo)

                    '自給品を含めて取得する。
                    Dim editVos As List(Of TYosanBuhinEditVo) = FindEditBy(editInsuVo)

                    Dim result As BuhinKoseiMatrix = BuhinKoseiMatrix.NewInnerJoinHyoujiJun(editVos, editInsuVos)
                    result = result.ExtractUnderLevel(aStructureResult.EditVo.BuhinNoHyoujiJun)
                    result.RemoveNullRecords()
                    result.CorrectLevelBy(baseLevel)
                    Return result

                ElseIf aStructureResult.InstlVo IsNot Nothing Then
                    ' 
                    Dim editInsuVos As List(Of TYosanBuhinEditInsuVo) = FindEditInstlByInstlHinbanHyoujiJun(aStructureResult.InstlVo)

                    '自給品を含めて取得する。
                    Dim editVos As List(Of TYosanBuhinEditVo) = FindEditBy(aStructureResult.InstlVo)

                    Dim result As BuhinKoseiMatrix = BuhinKoseiMatrix.NewInnerJoinHyoujiJun(editVos, editInsuVos)
                    result.RemoveNullRecords()
                    result.CorrectLevelBy(baseLevel)
                    If result.Records.Count = 0 Then
                        Dim editInstlVos2 As New List(Of TYosanBuhinEditInsuVo)
                        Dim dummyEditInstlVos2 As New TYosanBuhinEditInsuVo
                        dummyEditInstlVos2.PatternHyoujiJun = 0
                        dummyEditInstlVos2.BuhinNoHyoujiJun = 0
                        dummyEditInstlVos2.InsuSuryo = 1
                        editInstlVos2.Add(dummyEditInstlVos2)

                        Dim editVos2 As New List(Of TYosanBuhinEditVo)
                        Dim dummyEditVos2 As New TYosanBuhinEditVo
                        dummyEditVos2.YosanBuhinNo = aStructureResult.InstlVo.PatternName
                        dummyEditVos2.YosanLevel = 0

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

            'ベース車情報の開発符号毎に0532を見るのか0533を見るのか分けたい'
            Dim Rhac0552Vo As New Rhac0552Vo

            Rhac0552Vo = makeDao.FindByBuhinRhac0552(inputedBuhinNo)

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
            Dim singleMatrices As BuhinSingleMatrix(Of Rhac0552Vo, Rhac0532Vo)() = BuhinTreeMaker.NewSingleMatrices(rhac0552Vos, rhac0532Vos)

            If singleMatrices.Length <> 1 Then
                Throw New InvalidProgramException("SQLで1品番の構成だけ取って来ているのに NewSingleMatrices で構成が " & singleMatrices.Length & " 個")
            End If

            Dim mergeNode As New MergeNodeList(Of Rhac0552Vo, Rhac0532Vo)(New RhacNodeAccessor(New MakerNameResolverImpl(rhac0610Vos, inputedBuhinNoVo)))

            mergeNode.Compute(singleMatrices(0).Nodes)

            newKoseiMatrix = mergeNode.ResultMatrix
            If Not isInstlHinban Then
                newKoseiMatrix.RemoveRow(0)
            End If
            '集計コードがＲの子部品の構成情報を除外する。

            '余分な行が存在した場合削除する'
            For Each index As Integer In newKoseiMatrix.GetInputRowIndexes
                If index < 0 Then
                    newKoseiMatrix.RemoveRow(index)
                ElseIf StringUtil.IsEmpty(newKoseiMatrix(index).YosanBuhinNo) Then
                    newKoseiMatrix.RemoveRow(index)
                End If
            Next

            ''余分な行を削除した後で取引先コードと名称を取得する'
            'For Each index As Integer In newKoseiMatrix.GetInputRowIndexes
            '    If StringUtil.IsEmpty(newKoseiMatrix(index).YosanMakerCode) And StringUtil.IsEmpty(newKoseiMatrix(index).YosanMakerName) Then
            '        If Not StringUtil.IsEmpty(newKoseiMatrix(index).YosanBuhinNo) Then
            '            Dim MakerVo As New TShisakuBuhinEditVo
            '            MakerVo = Me.FindByKoutanTorihikisaki(newKoseiMatrix(index).YosanBuhinNo)
            '            newKoseiMatrix(index).YosanMakerCode = MakerVo.MakerCode
            '            newKoseiMatrix(index).YosanMakerName = MakerVo.MakerName
            '        End If

            '    End If
            'Next


            '供給セクションを振る'
            If Not newKoseiMatrix Is Nothing Then
                For Each index As Integer In newKoseiMatrix.GetInputRowIndexes
                    Dim BuhinNo As String = ""
                    If Not StringUtil.IsEmpty(newKoseiMatrix(index).YosanBuhinNo) Then

                        '集計コードを確認する'
                        If StringUtil.IsEmpty(newKoseiMatrix(index).YosanShukeiCode) Then
                            '海外集計'
                            If StringUtil.Equals(newKoseiMatrix(index).YosanSiaShukeiCode, "X") Or StringUtil.Equals(newKoseiMatrix(index).YosanSiaShukeiCode, "R") Or StringUtil.Equals(newKoseiMatrix(index).YosanSiaShukeiCode, "J") Then
                                '供給セクションは空'
                            ElseIf StringUtil.Equals(newKoseiMatrix(index).YosanSiaShukeiCode, "A") Then
                                newKoseiMatrix(index).YosanKyoukuSection = "9SX00"
                            ElseIf StringUtil.Equals(newKoseiMatrix(index).YosanSiaShukeiCode, "E") Or StringUtil.Equals(newKoseiMatrix(index).YosanSiaShukeiCode, "Y") Then
                                '親の供給セクションを取得する'
                                BuhinNo = newKoseiMatrix(index).YosanBuhinNo
                                newKoseiMatrix = SetKyoukuSection0552(BuhinNo, BuhinNo, rhac0552Vos, newKoseiMatrix)
                            End If
                        Else
                            '国内集計'
                            If StringUtil.Equals(newKoseiMatrix(index).YosanShukeiCode, "X") Or StringUtil.Equals(newKoseiMatrix(index).YosanShukeiCode, "R") Or StringUtil.Equals(newKoseiMatrix(index).YosanShukeiCode, "J") Then
                                '供給セクションは空'
                            ElseIf StringUtil.Equals(newKoseiMatrix(index).YosanShukeiCode, "A") Then
                                newKoseiMatrix(index).YosanKyoukuSection = "9SX00"
                            ElseIf StringUtil.Equals(newKoseiMatrix(index).YosanShukeiCode, "E") Or StringUtil.Equals(newKoseiMatrix(index).YosanShukeiCode, "Y") Then
                                '親の供給セクションを取得する'
                                BuhinNo = newKoseiMatrix(index).YosanBuhinNo
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

        Private koseiFlag As Integer

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

                    Dim editInsuVo As TYosanBuhinEditInsuVo = FindMinInstlHinbanHyoujiJunEditInstlBy(aStructureResult.EditVo)

                    Dim editInsuVos As List(Of TYosanBuhinEditInsuVo) = FindEditInstlByInstlHinbanHyoujiJun(editInsuVo)

                    '自給品を含めて取得する。
                    Dim editVos As List(Of TYosanBuhinEditVo) = FindEditBy(editInsuVo)

                    Dim result As BuhinKoseiMatrix = BuhinKoseiMatrix.NewInnerJoinHyoujiJun(editVos, editInsuVos)
                    result = result.ExtractUnderLevel(aStructureResult.EditVo.BuhinNoHyoujiJun)
                    result.RemoveNullRecords()
                    result.CorrectLevelBy(0)
                    Return result

                ElseIf aStructureResult.InstlVo IsNot Nothing Then
                    ' 
                    Dim editInsuVos As List(Of TYosanBuhinEditInsuVo) = FindEditInstlByInstlHinbanHyoujiJun(aStructureResult.InstlVo)

                    '自給品を含めて取得する。
                    Dim editVos As List(Of TYosanBuhinEditVo) = FindEditBy(aStructureResult.InstlVo)

                    Dim result As BuhinKoseiMatrix = BuhinKoseiMatrix.NewInnerJoinHyoujiJun(editVos, editInsuVos)
                    result.RemoveNullRecords()
                    result.CorrectLevelBy(0)
                    If result.Records.Count = 0 Then
                        Dim editInstlVos2 As New List(Of TYosanBuhinEditInsuVo)
                        Dim dummyEditInstlVos2 As New TYosanBuhinEditInsuVo
                        dummyEditInstlVos2.PatternHyoujiJun = 0
                        dummyEditInstlVos2.BuhinNoHyoujiJun = 0
                        dummyEditInstlVos2.InsuSuryo = 1
                        editInstlVos2.Add(dummyEditInstlVos2)

                        Dim editVos2 As New List(Of TYosanBuhinEditVo)
                        Dim dummyEditVos2 As New TYosanBuhinEditVo
                        dummyEditVos2.YosanBuhinNo = aStructureResult.InstlVo.PatternName
                        dummyEditVos2.YosanLevel = 0

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

            'ベース車情報の開発符号毎に0532を見るのか0533を見るのか分けたい'
            Dim Rhac0552Vo As New Rhac0552Vo

            Rhac0552Vo = makeDao.FindByBuhinRhac0552(inputedBuhinNo)

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

            '集計コードブランクを除いて構成を返す。
            Dim singleMatrices As BuhinSingleMatrix(Of Rhac0552Vo, Rhac0532Vo)() = BuhinTreeMaker.NewSingleMatrices(rhac0552Vos, rhac0532Vos)

            If singleMatrices.Length <> 1 Then
                Throw New InvalidProgramException("SQLで1品番の構成だけ取って来ているのに NewSingleMatrices で構成が " & singleMatrices.Length & " 個")
            End If

            Dim mergeNode As New MergeNodeList(Of Rhac0552Vo, Rhac0532Vo)(New RhacNodeAccessor(New MakerNameResolverImpl(rhac0610Vos, inputedBuhinNoVo)))

            mergeNode.Compute(singleMatrices(0).Nodes)

            newKoseiMatrix = mergeNode.ResultMatrix
            If Not isInstlHinban Then
                newKoseiMatrix.RemoveRow(0)
            End If

            '余分な行が存在した場合削除する'
            For Each index As Integer In newKoseiMatrix.GetInputRowIndexes
                If index < 0 Then
                    newKoseiMatrix.RemoveRow(index)
                ElseIf StringUtil.IsEmpty(newKoseiMatrix(index).YosanBuhinNo) Then
                    newKoseiMatrix.RemoveRow(index)
                End If
            Next

            ''余分な行を削除した後で取引先コードと名称を取得する'
            'For Each index As Integer In newKoseiMatrix.GetInputRowIndexes
            '    If StringUtil.IsEmpty(newKoseiMatrix(index).YosanMakerCode) And StringUtil.IsEmpty(newKoseiMatrix(index).YosanMakerName) Then
            '        If Not StringUtil.IsEmpty(newKoseiMatrix(index).YosanBuhinNo) Then
            '            Dim MakerVo As New TShisakuBuhinEditVo
            '            MakerVo = Me.FindByKoutanTorihikisaki(newKoseiMatrix(index).YosanBuhinNo)
            '            newKoseiMatrix(index).YosanMakerCode = MakerVo.MakerCode
            '            newKoseiMatrix(index).YosanMakerName = MakerVo.MakerName
            '        End If

            '    End If
            'Next

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
                    ''存在したら親部品が構成マトリクスに存在するかチェックする'
                    'For Each index As Integer In KoseiMatrix.GetInputRowIndexes
                    '    If StringUtil.Equals(KoseiMatrix(index).BuhinNo, buhinNoOya) Then
                    '        '存在したら集計コードをチェック'
                    '        If StringUtil.IsEmpty(KoseiMatrix(index).ShukeiCode) Then
                    '            '海外集計'
                    '            If StringUtil.Equals(KoseiMatrix(index).SiaShukeiCode, "J") Then
                    '                'Jなら一段階上の親を探す'
                    '                KoseiMatrix = SetKyoukuSection0552(KoseiMatrix(index).BuhinNo, originalBuhinNo, rhac0552Vos, KoseiMatrix)
                    '            Else
                    '                'J以外ならメーカーコード+0で供給セクションを追加'
                    '                For Each index2 As Integer In KoseiMatrix.GetInputRowIndexes
                    '                    If StringUtil.Equals(KoseiMatrix(index2).BuhinNo, originalBuhinNo) Then
                    '                        Dim KyoukuSection As String = ""
                    '                        If Not StringUtil.IsEmpty(KoseiMatrix(index).MakerCode) Then
                    '                            KyoukuSection = KoseiMatrix(index).MakerCode + "0"
                    '                        End If
                    '                        If StringUtil.IsEmpty(KoseiMatrix(index2).KyoukuSection) Then
                    '                            KoseiMatrix(index2).KyoukuSection = KyoukuSection
                    '                            Return KoseiMatrix
                    '                        End If
                    '                    End If
                    '                Next
                    '            End If
                    '        Else
                    '            '国内集計'
                    '            '海外集計'
                    '            If StringUtil.Equals(KoseiMatrix(index).ShukeiCode, "J") Then
                    '                'Jなら一段階上の親を探す'
                    '                KoseiMatrix = SetKyoukuSection0552(KoseiMatrix(index).BuhinNo, originalBuhinNo, rhac0552Vos, KoseiMatrix)
                    '            Else
                    '                'J以外ならメーカーコード+0で供給セクションを追加'
                    '                For Each index2 As Integer In KoseiMatrix.GetInputRowIndexes
                    '                    If StringUtil.Equals(KoseiMatrix(index2).BuhinNo, originalBuhinNo) Then
                    '                        Dim KyoukuSection As String = ""
                    '                        If Not StringUtil.IsEmpty(KoseiMatrix(index).MakerCode) Then
                    '                            KyoukuSection = KoseiMatrix(index).MakerCode + "0"
                    '                        End If
                    '                        If StringUtil.IsEmpty(KoseiMatrix(index2).KyoukuSection) Then
                    '                            KoseiMatrix(index2).KyoukuSection = KyoukuSection
                    '                            Return KoseiMatrix
                    '                        End If
                    '                    End If
                    '                Next
                    '            End If
                    '        End If

                    '    End If
                    'Next
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
                    If koseiMatrix(index).YosanLevel = level Then
                        '上5桁取得'
                        Dim buhinNo5 As String = Left(koseiMatrix(index).YosanBuhinNo, 5)
                        For Each index2 As Integer In koseiMatrix.GetInputRowIndexes
                            '同レベル'
                            If koseiMatrix(index2).YosanLevel = level Then
                                '同部品番号でない'
                                If Not StringUtil.Equals(koseiMatrix(index).YosanBuhinNo, koseiMatrix(index2).YosanBuhinNo) Then
                                    '上5桁が同じ'
                                    If StringUtil.Equals(buhinNo5, Left(koseiMatrix(index2).YosanBuhinNo, 5)) Then
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
                If koseiMatrix(index).YosanLevel = 0 Then
                    Continue For
                End If
                For Each index2 As Integer In koseiMatrix.GetInputRowIndexes
                    If koseiMatrix(index2).YosanLevel = 0 Then
                        Continue For
                    End If

                    '同レベル,同部品番号、同集計コード、同供給セクションのとき員数をマージする'
                    If koseiMatrix(index).YosanLevel = koseiMatrix(index2).YosanLevel _
                            AndAlso StringUtil.Equals(koseiMatrix(index).YosanBuhinNo, koseiMatrix(index2).YosanBuhinNo) Then
                        'AndAlso EzUtil.IsEqualIfNull(koseiMatrix(index).ShukeiCode, koseiMatrix(index2).ShukeiCode) _
                        'AndAlso EzUtil.IsEqualIfNull(koseiMatrix(index).SiaShukeiCode, koseiMatrix(index2).SiaShukeiCode) _
                        'AndAlso EzUtil.IsEqualIfNull(koseiMatrix(index).KyoukuSection, koseiMatrix(index2).KyoukuSection) Then
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
                If Not koseiMatrix(index).YosanBuhinNo Is Nothing Then
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
                If koseiMatrix(index).YosanLevel > result Then
                    result = koseiMatrix(index).YosanLevel
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
        Private Function GetBuhinColor(ByVal buhinNo As String, ByVal rhFlag As Integer, Optional ByVal kaihatsuFugo As String = "") As String
            Dim color As String = ""
            '先に部品自身の色が存在するか探す'
            color = makeDao.FindByBuhinColor2220(shisakuEventCode, shisakuBlockNo, oriBuhinNo, bfBuhinNo, buhinNo)

            'いないならベース車の情報を元に色を探す'
            If StringUtil.IsEmpty(color) Then

                Select Case rhFlag
                    Case 0
                        color = makeDao.FindByBuhinColor0532(shisakuEventCode, _
                                                             shisakuBukaCode, _
                                                             shisakuBlockNo, _
                                                             oriBuhinNo, _
                                                             bfBuhinNo, _
                                                             buhinNo)
                    Case 1
                        color = makeDao.FindByBuhinColor0533(shisakuEventCode, _
                                                             shisakuBukaCode, _
                                                             shisakuBlockNo, _
                                                             oriBuhinNo, _
                                                             bfBuhinNo, _
                                                             kaihatsuFugo, _
                                                             buhinNo)
                    Case 2
                        color = makeDao.FindByBuhinColor0530(shisakuEventCode, _
                                                             shisakuBukaCode, _
                                                             shisakuBlockNo, _
                                                             oriBuhinNo, _
                                                             bfBuhinNo, _
                                                             buhinNo)
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
        Private Function GetBuhinColorKosei(ByVal buhinNo As String, ByVal rhFlag As Integer, Optional ByVal kaihatsuFugo As String = "") As String
            Dim color As String = ""
            '先に部品自身の色が存在するか探す'
            color = makeDao.FindByBuhinColor2220(shisakuEventCode, shisakuBlockNo, oriBuhinNo, bfBuhinNo, buhinNo)

            'いないならベース車の情報を元に色を探す'
            If StringUtil.IsEmpty(color) Then

                Select Case rhFlag
                    Case 0
                        color = makeDao.FindByBuhinColor0532Kosei(shisakuEventCode, _
                                                             shisakuBukaCode, _
                                                             shisakuBlockNo, _
                                                             oriBuhinNo, _
                                                             bfBuhinNo, _
                                                             buhinNo)
                    Case 1
                        color = makeDao.FindByBuhinColor0533Kosei(shisakuEventCode, _
                                                             shisakuBukaCode, _
                                                             shisakuBlockNo, _
                                                             oriBuhinNo, _
                                                             bfBuhinNo, _
                                                             kaihatsuFugo, _
                                                             buhinNo)
                    Case 2
                        color = makeDao.FindByBuhinColor0530Kosei(shisakuEventCode, _
                                                             shisakuBukaCode, _
                                                             shisakuBlockNo, _
                                                             oriBuhinNo, _
                                                             bfBuhinNo, _
                                                             buhinNo)
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
        Private Function FindMinInstlHinbanHyoujiJunEditInstlBy(ByVal editVo As TYosanBuhinEditVo) As TYosanBuhinEditInsuVo

            Dim param As New TYosanBuhinEditInsuVo
            param.YosanEventCode = editVo.YosanEventCode
            param.BuhinhyoName = editVo.BuhinhyoName
            param.YosanBukaCode = editVo.YosanBukaCode
            param.YosanBlockNo = editVo.YosanBlockNo
            param.BuhinNoHyoujiJun = editVo.BuhinNoHyoujiJun
            Dim editInstlVos As List(Of TYosanBuhinEditInsuVo) = editInsuDao.FindBy(param)

            Dim result As TYosanBuhinEditInsuVo = Nothing
            For Each vo As TYosanBuhinEditInsuVo In editInstlVos
                If result Is Nothing Then
                    result = vo
                Else
                    If vo.PatternHyoujiJun < result.PatternHyoujiJun Then
                        result = vo
                    End If
                End If
            Next
            Return result
        End Function

        Private Function FindEditInstlByInstlHinbanHyoujiJun(ByVal editInstlVo As TYosanBuhinEditInsuVo) As List(Of TYosanBuhinEditInsuVo)
            Dim param As New TYosanBuhinEditInsuVo
            param.YosanEventCode = editInstlVo.YosanEventCode
            param.BuhinhyoName = editInstlVo.BuhinhyoName
            param.YosanBukaCode = editInstlVo.YosanBukaCode
            param.YosanBlockNo = editInstlVo.YosanBlockNo
            param.PatternHyoujiJun = editInstlVo.PatternHyoujiJun
            Return editInsuDao.FindBy(param)
        End Function

        Private Function FindEditInstlByInstlHinbanHyoujiJun(ByVal patternVo As TYosanBuhinEditPatternVo) As List(Of TYosanBuhinEditInsuVo)
            Dim result As New List(Of TYosanBuhinEditInsuVo)
            result = makeDao.FindbyEditInstl(patternVo.YosanEventCode, patternVo.PatternName)
            Return result
        End Function

        Private Function FindEditBy(ByVal editInstlVo As TYosanBuhinEditInsuVo) As List(Of TYosanBuhinEditVo)
            Dim param As New TYosanBuhinEditVo
            param.YosanEventCode = editInstlVo.YosanEventCode
            param.BuhinhyoName = editInstlVo.BuhinhyoName
            param.YosanBukaCode = editInstlVo.YosanBukaCode
            param.YosanBlockNo = editInstlVo.YosanBlockNo
            Return editDao.FindBy(param)
        End Function

        Private Function FindEditBy(ByVal patternVo As TYosanBuhinEditPatternVo) As List(Of TYosanBuhinEditVo)
            Dim result As New List(Of TYosanBuhinEditVo)
            result = makeDao.FindbyEdit(patternVo.YosanEventCode, patternVo.PatternName)
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
        Private Function BuhinKousei(ByVal inputedBuhinNo As String, ByVal aStructureResult As StructureResult, ByVal baseLevel As Integer, ByVal kaihatsuFugo As String) As BuhinKoseiMatrix
            '0553注記：設計展開時はベース車情報の開発符号を使用'
            '構成再展開、最新化、構成呼び出しは画面側から取得した開発符号を使用'
            '子部品展開はイベント情報の開発符号を使用する'
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
            Dim rhac0610Vos As New List(Of Rhac0610Vo)

            If Not isInstlHinban Then
                rhac0553Vos.Add(parentVoForBuhinNo)
                Dim dummy As New Rhac0533Vo
                dummy.BuhinNo = parentVoForBuhinNo.BuhinNoOya
                rhac0533Vos.Add(dummy)
            End If

            Dim colorA As String = ""

            If rhac0553Vos(0).BuhinNoOya.Length = 12 Then
                colorA = GetBuhinColor(rhac0553Vos(0).BuhinNoOya, 1)
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
                        Dim color As String = GetBuhinColor(Vo.BuhinNoOya, 1)
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
                        Dim color As String = GetBuhinColor(Vo.BuhinNoKo, 1)
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
                        Dim color As String = GetBuhinColor(Vo.BuhinNo, 1)
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

            Dim singleMatrices As BuhinSingleMatrix(Of Rhac0553Vo, Rhac0533Vo)() = BuhinTreeMaker.NewSingleMatrices(RemoveNonShukeiDummyRhac0553(rhac0553Vos), rhac0533Vos)

            If singleMatrices.Length <> 1 Then
                Throw New InvalidProgramException("SQLで1品番の構成だけ取って来ているのに NewSingleMatrices で構成が " & singleMatrices.Length & " 個")
            End If

            Dim mergeNode As New MergeNodeList(Of Rhac0553Vo, Rhac0533Vo)(New Rhac0553NodeAccessor(New MakerNameResolverImplRhac0553(Nothing, inputedBuhinNoVo)))

            mergeNode.Compute(singleMatrices(0).Nodes)

            Dim newKoseiMatrix As BuhinKoseiMatrix = mergeNode.ResultMatrix
            If Not isInstlHinban Then
                newKoseiMatrix.RemoveRow(0)
            End If
            newKoseiMatrix.CorrectLevelBy(baseLevel)
            'ここでやってみるか'
            For Each index As Integer In newKoseiMatrix.GetInputRowIndexes
                If Not StringUtil.IsEmpty(newKoseiMatrix(index).YosanBuhinNo) Then
                    Dim MakerVo As New TShisakuBuhinEditVo
                    MakerVo = Me.FindByKoutanTorihikisaki(newKoseiMatrix(index).YosanBuhinNo)
                    newKoseiMatrix(index).YosanMakerCode = MakerVo.MakerCode
                    newKoseiMatrix(index).YosanMakerName = MakerVo.MakerName
                End If
            Next
            '供給セクションを振る'
            If Not newKoseiMatrix Is Nothing Then
                For Each index As Integer In newKoseiMatrix.GetInputRowIndexes
                    Dim BuhinNo As String = ""
                    If Not StringUtil.IsEmpty(newKoseiMatrix(index).YosanBuhinNo) Then

                        '集計コードを確認する'
                        If StringUtil.IsEmpty(newKoseiMatrix(index).YosanShukeiCode) Then
                            '海外集計'
                            If StringUtil.Equals(newKoseiMatrix(index).YosanSiaShukeiCode, "X") Or StringUtil.Equals(newKoseiMatrix(index).YosanSiaShukeiCode, "R") Or StringUtil.Equals(newKoseiMatrix(index).YosanSiaShukeiCode, "J") Then
                                '供給セクションは空'
                            ElseIf StringUtil.Equals(newKoseiMatrix(index).YosanSiaShukeiCode, "A") Then
                                newKoseiMatrix(index).YosanKyoukuSection = "9SX00"
                            ElseIf StringUtil.Equals(newKoseiMatrix(index).YosanSiaShukeiCode, "E") Or StringUtil.Equals(newKoseiMatrix(index).YosanSiaShukeiCode, "Y") Then
                                '親の供給セクションを取得する'
                                BuhinNo = newKoseiMatrix(index).YosanBuhinNo
                                newKoseiMatrix = SetKyoukuSection0553(BuhinNo, BuhinNo, rhac0553Vos, newKoseiMatrix)
                            End If
                        Else
                            '国内集計'
                            If StringUtil.Equals(newKoseiMatrix(index).YosanShukeiCode, "X") Or StringUtil.Equals(newKoseiMatrix(index).YosanShukeiCode, "R") Or StringUtil.Equals(newKoseiMatrix(index).YosanShukeiCode, "J") Then
                                '供給セクションは空'
                            ElseIf StringUtil.Equals(newKoseiMatrix(index).YosanShukeiCode, "A") Then
                                newKoseiMatrix(index).YosanKyoukuSection = "9SX00"
                            ElseIf StringUtil.Equals(newKoseiMatrix(index).YosanShukeiCode, "E") Or StringUtil.Equals(newKoseiMatrix(index).YosanShukeiCode, "Y") Then
                                '親の供給セクションを取得する'
                                BuhinNo = newKoseiMatrix(index).YosanBuhinNo
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
                        If StringUtil.Equals(KoseiMatrix(index).YosanBuhinNo, buhinNoOya) Then
                            '存在したら集計コードをチェック'
                            If StringUtil.IsEmpty(KoseiMatrix(index).YosanShukeiCode) Then
                                '海外集計'
                                If StringUtil.Equals(KoseiMatrix(index).YosanSiaShukeiCode, "J") Then
                                    'Jなら一段階上の親を探す'
                                    KoseiMatrix = SetKyoukuSection0553(KoseiMatrix(index).YosanBuhinNo, originalBuhinNo, rhac0553Vos, KoseiMatrix)
                                Else
                                    'J以外ならメーカーコード+0で供給セクションを追加'
                                    For Each index2 As Integer In KoseiMatrix.GetInputRowIndexes
                                        If StringUtil.Equals(KoseiMatrix(index2).YosanBuhinNo, originalBuhinNo) Then
                                            Dim KyoukuSection As String = ""
                                            If Not StringUtil.IsEmpty(KoseiMatrix(index).YosanMakerCode) Then
                                                KyoukuSection = KoseiMatrix(index).YosanMakerCode + "0"
                                            End If
                                            KoseiMatrix(index2).YosanKyoukuSection = KyoukuSection
                                            Return KoseiMatrix
                                        End If
                                    Next
                                End If
                            Else
                                '国内集計'
                                '海外集計'
                                If StringUtil.Equals(KoseiMatrix(index).YosanShukeiCode, "J") Then
                                    'Jなら一段階上の親を探す'
                                    KoseiMatrix = SetKyoukuSection0553(KoseiMatrix(index).YosanBuhinNo, originalBuhinNo, rhac0553Vos, KoseiMatrix)
                                Else
                                    'J以外ならメーカーコード+0で供給セクションを追加'
                                    For Each index2 As Integer In KoseiMatrix.GetInputRowIndexes
                                        If StringUtil.Equals(KoseiMatrix(index2).YosanBuhinNo, originalBuhinNo) Then
                                            Dim KyoukuSection As String = ""
                                            If Not StringUtil.IsEmpty(KoseiMatrix(index).YosanMakerCode) Then
                                                KyoukuSection = KoseiMatrix(index).YosanMakerCode + "0"
                                            End If
                                            KoseiMatrix(index2).YosanKyoukuSection = KyoukuSection
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
        Private Function BuhinKouseiRhac0551(ByVal inputedBuhinNo As String, ByVal aStructureResult As StructureResult, ByVal baseLevel As Integer) As BuhinKoseiMatrix

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
                    colorA = GetBuhinColorKosei(rhac0551Vos(0).BuhinNoOya, 0)
                Else
                    colorA = GetBuhinColor(rhac0551Vos(0).BuhinNoOya, 0)
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
                            color = GetBuhinColorKosei(Vo.BuhinNoOya, 2)
                        Else
                            color = GetBuhinColor(Vo.BuhinNoOya, 2)
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
                            color = GetBuhinColorKosei(Vo.BuhinNoOya, 2)
                        Else
                            color = GetBuhinColor(Vo.BuhinNoOya, 2)
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
                            color = GetBuhinColorKosei(Vo.BuhinNo, 2)
                        Else
                            color = GetBuhinColor(Vo.BuhinNo, 2)
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

            If singleMatrices.Length <> 1 Then
            End If

            Dim mergeNode As New MergeNodeList(Of Rhac0551Vo, Rhac0530Vo)(New Rhac0551NodeAccessor(New MakerNameResolverImplRhac0551(Nothing, inputedBuhinNoVo)))

            mergeNode.Compute(singleMatrices(0).Nodes)

            Dim newKoseiMatrix As BuhinKoseiMatrix = mergeNode.ResultMatrix
            If Not isInstlHinban Then
                newKoseiMatrix.RemoveRow(0)
            End If
            newKoseiMatrix.CorrectLevelBy(baseLevel)
            'ここでやってみるか'
            For Each index As Integer In newKoseiMatrix.GetInputRowIndexes
                If Not StringUtil.IsEmpty(newKoseiMatrix(index).YosanBuhinNo) Then
                    If StringUtil.IsEmpty(newKoseiMatrix(index).YosanMakerCode) And StringUtil.IsEmpty(newKoseiMatrix(index).YosanMakerName) Then
                        Dim MakerVo As New TShisakuBuhinEditVo
                        MakerVo = Me.FindByKoutanTorihikisaki(newKoseiMatrix(index).YosanBuhinNo)
                        newKoseiMatrix(index).YosanMakerCode = MakerVo.MakerCode
                        newKoseiMatrix(index).YosanMakerName = MakerVo.MakerName
                    End If
                End If
            Next
            '供給セクションを振る'
            If Not newKoseiMatrix Is Nothing Then
                For Each index As Integer In newKoseiMatrix.GetInputRowIndexes
                    Dim BuhinNo As String = ""
                    If Not StringUtil.IsEmpty(newKoseiMatrix(index).YosanBuhinNo) Then

                        '集計コードを確認する'
                        If StringUtil.IsEmpty(newKoseiMatrix(index).YosanShukeiCode) Then
                            '海外集計'
                            If StringUtil.Equals(newKoseiMatrix(index).YosanSiaShukeiCode, "X") Or StringUtil.Equals(newKoseiMatrix(index).YosanSiaShukeiCode, "R") Or StringUtil.Equals(newKoseiMatrix(index).YosanSiaShukeiCode, "J") Then
                                '供給セクションは空'
                            ElseIf StringUtil.Equals(newKoseiMatrix(index).YosanSiaShukeiCode, "A") Then
                                newKoseiMatrix(index).YosanKyoukuSection = "9SX00"
                            ElseIf StringUtil.Equals(newKoseiMatrix(index).YosanSiaShukeiCode, "E") Or StringUtil.Equals(newKoseiMatrix(index).YosanSiaShukeiCode, "Y") Then
                                '親の供給セクションを取得する'
                                BuhinNo = newKoseiMatrix(index).YosanBuhinNo
                                newKoseiMatrix = SetKyoukuSection0551(BuhinNo, BuhinNo, rhac0551Vos, newKoseiMatrix)
                            End If
                        Else
                            '国内集計'
                            If StringUtil.Equals(newKoseiMatrix(index).YosanShukeiCode, "X") Or StringUtil.Equals(newKoseiMatrix(index).YosanShukeiCode, "R") Or StringUtil.Equals(newKoseiMatrix(index).YosanShukeiCode, "J") Then
                                '供給セクションは空'
                            ElseIf StringUtil.Equals(newKoseiMatrix(index).YosanShukeiCode, "A") Then
                                newKoseiMatrix(index).YosanKyoukuSection = "9SX00"
                            ElseIf StringUtil.Equals(newKoseiMatrix(index).YosanShukeiCode, "E") Or StringUtil.Equals(newKoseiMatrix(index).YosanShukeiCode, "Y") Then
                                '親の供給セクションを取得する'
                                BuhinNo = newKoseiMatrix(index).YosanBuhinNo
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
                        If StringUtil.Equals(KoseiMatrix(index).YosanBuhinNo, buhinNoOya) Then
                            '存在したら集計コードをチェック'
                            If StringUtil.IsEmpty(KoseiMatrix(index).YosanShukeiCode) Then
                                '海外集計'
                                If StringUtil.Equals(KoseiMatrix(index).YosanSiaShukeiCode, "J") Then
                                    'Jなら一段階上の親を探す'
                                    KoseiMatrix = SetKyoukuSection0551(KoseiMatrix(index).YosanBuhinNo, originalBuhinNo, rhac0551Vos, KoseiMatrix)
                                Else
                                    'J以外ならメーカーコード+0で供給セクションを追加'
                                    For Each index2 As Integer In KoseiMatrix.GetInputRowIndexes
                                        If StringUtil.Equals(KoseiMatrix(index2).YosanBuhinNo, originalBuhinNo) Then
                                            Dim KyoukuSection As String = ""
                                            If Not StringUtil.IsEmpty(KoseiMatrix(index).YosanMakerCode) Then
                                                KyoukuSection = KoseiMatrix(index).YosanMakerCode + "0"
                                            End If
                                            KoseiMatrix(index2).YosanKyoukuSection = KyoukuSection
                                            Return KoseiMatrix
                                        End If

                                    Next
                                End If
                            Else
                                '国内集計'
                                '海外集計'
                                If StringUtil.Equals(KoseiMatrix(index).YosanShukeiCode, "J") Then
                                    'Jなら一段階上の親を探す'
                                    KoseiMatrix = SetKyoukuSection0551(KoseiMatrix(index).YosanBuhinNo, originalBuhinNo, rhac0551Vos, KoseiMatrix)
                                Else
                                    'J以外ならメーカーコード+0で供給セクションを追加'
                                    For Each index2 As Integer In KoseiMatrix.GetInputRowIndexes
                                        If StringUtil.Equals(KoseiMatrix(index2).YosanBuhinNo, originalBuhinNo) Then
                                            Dim KyoukuSection As String = ""
                                            If Not StringUtil.IsEmpty(KoseiMatrix(index).YosanMakerCode) Then
                                                KyoukuSection = KoseiMatrix(index).YosanMakerCode + "0"
                                            End If
                                            KoseiMatrix(index2).YosanKyoukuSection = KyoukuSection
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
                If newKoseiMatrixNotJikyu.Record(index).YosanLevel = "0" Then
                    Continue For
                End If
                '以下の条件にマッチしたら除外する。
                If newKoseiMatrixNotJikyu.Record(index).YosanShukeiCode = "J" Or _
                   newKoseiMatrixNotJikyu.Record(index).YosanShukeiCode <> "J" _
                                    AndAlso newKoseiMatrixNotJikyu.Record(index).YosanSiaShukeiCode = "J" Or _
                   StringUtil.IsEmpty(newKoseiMatrixNotJikyu.Record(index).YosanShukeiCode) _
                                    AndAlso newKoseiMatrixNotJikyu.Record(index).YosanSiaShukeiCode = "J" Then

                    '自給品の構成を除去する。
                    newKoseiMatrixNotJikyu.RemoveRow(index)

                End If

            Next

            Return newKoseiMatrixNotJikyu

        End Function

#End Region

        ''' <summary>
        ''' 購担/取引先を取得する
        ''' </summary>
        ''' <param name="buhinNo">部品No</param>
        ''' <returns>購担と取引先</returns>
        ''' <remarks></remarks>
        Private Function FindByKoutanTorihikisaki(ByVal buhinNo As String) As TShisakuBuhinEditVo

            Dim db As New EBomDbClient

            Dim sql As String = _
            " SELECT KA, " _
            & " TRCD " _
            & " FROM " & MBOM_DB_NAME & ".dbo.AS_KPSM10P WITH (NOLOCK, NOWAIT)" _
            & " WHERE  " _
            & " BUBA_15 = @Buba15 " _
            & " ORDER BY UPDATED_DATE DESC, UPDATED_TIME DESC "

            Dim NewBuhinNo As String = ""
            If StringUtil.Equals(Left(buhinNo, 1), "-") Then
                NewBuhinNo = Replace(buhinNo, "-", " ")
            Else
                NewBuhinNo = buhinNo
            End If

            Dim paramK As New AsKPSM10PVo
            Dim ETVO As New TShisakuBuhinEditVo

            paramK.Buba15 = NewBuhinNo

            Dim aKPSM As New AsKPSM10PVo

            aKPSM = db.QueryForObject(Of AsKPSM10PVo)(sql, paramK)
            '存在チェックその１(３ヶ月インフォメーション)'
            If aKPSM Is Nothing Then
                NewBuhinNo = ""
                '無ければパーツプライリスト'
                Dim sql2 As String = _
                " SELECT KA, " _
                & " TRCD " _
                & " FROM " & MBOM_DB_NAME & ".dbo.AS_PARTSP WITH (NOLOCK, NOWAIT)" _
                & " WHERE BUBA_13 = @Buba13 " _
                & " ORDER BY UPDATED_DATE DESC, UPDATED_TIME DESC "

                Dim paramP As New AsPARTSPVo

                If buhinNo.Length < 13 Then
                    If StringUtil.Equals(Left(buhinNo, 1), "-") Then
                        NewBuhinNo = Replace(buhinNo, "-", " ")
                    Else
                        NewBuhinNo = buhinNo
                    End If
                    paramP.Buba13 = NewBuhinNo
                Else
                    If StringUtil.Equals(Left(buhinNo, 1), "-") Then
                        NewBuhinNo = Left(Replace(buhinNo, "-", " "), 13)
                    Else
                        NewBuhinNo = Left(buhinNo, 13)
                    End If
                    paramP.Buba13 = NewBuhinNo
                End If

                Dim aPARTS As New AsPARTSPVo
                aPARTS = db.QueryForObject(Of AsPARTSPVo)(sql2, paramP)

                '存在チェックその２(パーツプライリスト)'
                If aPARTS Is Nothing Then
                    NewBuhinNo = ""
                    '無ければ海外生産マスタ'
                    Dim sql3 As String = _
                    " SELECT KA, " _
                    & " TRCD " _
                    & " FROM " & MBOM_DB_NAME & ".dbo.AS_GKPSM10P WITH (NOLOCK, NOWAIT)" _
                    & " WHERE BUBA_15 = @Buba15 " _
                    & " ORDER BY UPDATED_DATE DESC, UPDATED_TIME DESC "

                    Dim paramG As New AsGKPSM10PVo
                    If StringUtil.Equals(Left(buhinNo, 1), "-") Then
                        NewBuhinNo = Replace(buhinNo, "-", " ")
                    Else
                        NewBuhinNo = buhinNo
                    End If
                    paramG.Buba15 = NewBuhinNo
                    Dim aGKPSM As New AsGKPSM10PVo
                    aGKPSM = db.QueryForObject(Of AsGKPSM10PVo)(sql3, paramG)

                    '存在チェックその３(海外生産マスタ) '
                    If aGKPSM Is Nothing Then
                        NewBuhinNo = ""
                        '無ければ部品マスタ'
                        If buhinNo.Length < 10 Then
                            If StringUtil.Equals(Left(buhinNo, 1), "-") Then
                                NewBuhinNo = Replace(buhinNo, "-", " ")
                            Else
                                NewBuhinNo = buhinNo
                            End If
                        Else
                            If StringUtil.Equals(Left(buhinNo, 1), "-") Then
                                NewBuhinNo = Left(Replace(buhinNo, "-", " "), 10)
                            Else
                                NewBuhinNo = Left(buhinNo, 10)
                            End If
                        End If
                        Dim sql4 As String = _
                        " SELECT KOTAN, " _
                        & " MAKER " _
                        & " FROM " & MBOM_DB_NAME & ".dbo.AS_BUHIN01 WITH (NOLOCK, NOWAIT)" _
                        & " WHERE " _
                        & " GZZCP = @Gzzcp " _
                        & " ORDER BY UPDATED_DATE DESC, UPDATED_TIME DESC "

                        Dim param4 As New AsBUHIN01Vo
                        param4.Gzzcp = NewBuhinNo

                        Dim aBuhin01 As New AsBUHIN01Vo
                        aBuhin01 = db.QueryForObject(Of AsBUHIN01Vo)(sql4, param4)

                        '存在チェックその４(部品マスタ)'
                        If aBuhin01 Is Nothing Then
                            '無ければ属性管理'
                            Dim sql5 As String = _
                            "SELECT " _
                            & " FHI_NOMINATE_KOTAN, " _
                            & " TORIHIKISAKI_CODE " _
                            & " FROM " _
                            & " " + EBOM_DB_NAME + ".dbo.T_VALUE_DEV WITH (NOLOCK, NOWAIT)" _
                            & " WHERE " _
                            & "  AN_LEVEL = '1' " _
                            & " AND BUHIN_NO = @BuhinNo " _
                            & " ORDER BY UPDATED_DATE DESC, UPDATED_TIME DESC "

                            Dim aDev As New TValueDevVo
                            Dim paramT As New TValueDevVo
                            If buhinNo.Length < 10 Then
                                paramT.BuhinNo = buhinNo
                            Else
                                paramT.BuhinNo = Left(buhinNo, 10)
                            End If

                            aDev = db.QueryForObject(Of TValueDevVo)(sql5, paramT)

                            '存在チェックその５(属性管理(開発符号毎)) '
                            If aDev Is Nothing Then
                                ETVO.MakerCode = ""
                            Else
                                ETVO.MakerCode = aDev.TorihikisakiCode
                            End If

                        Else
                            ETVO.MakerCode = aBuhin01.Maker
                        End If
                    Else
                        ETVO.MakerCode = aGKPSM.Trcd
                    End If
                Else
                    ETVO.MakerCode = aPARTS.Trcd
                End If
            Else
                ETVO.MakerCode = aKPSM.Trcd
            End If

            If Not StringUtil.IsEmpty(ETVO.MakerCode) Then
                Dim Msql As String = _
                " SELECT MAKER_NAME " _
                & " FROM " & RHACLIBF_DB_NAME & ".dbo.RHAC0610 WITH (NOLOCK, NOWAIT)" _
                & " WHERE " _
                & " MAKER_CODE = @MakerCode "

                Dim Mparam As New Rhac0610Vo
                Mparam.MakerCode = ETVO.MakerCode
                Dim MVo As New Rhac0610Vo
                MVo = db.QueryForObject(Of Rhac0610Vo)(Msql, Mparam)
                If MVo Is Nothing Then
                    ETVO.MakerName = ""
                Else
                    ETVO.MakerName = MVo.MakerName

                End If
            End If

            Return ETVO
        End Function

    End Class
End Namespace