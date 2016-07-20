Imports ShisakuCommon.Db.Impl
Imports ShisakuCommon.Db.EBom
Imports ShisakuCommon.Db.EBom.Exclusion
Imports ShisakuCommon.Db.EBom.Dao.Impl
Imports ShisakuCommon.Db.EBom.Dao
Imports ShisakuCommon
Imports ShisakuCommon.Util
Imports ShisakuCommon.Db.EBom.Vo
Imports ShisakuCommon.Db
Imports EBom.Data
Imports EBom.Common
Imports EventSakusei.YosanSetteiBuhinSakusei.Dao


Namespace YosanSetteiBuhinSakusei.Logic

    ''' <summary>
    ''' 手配帳作成比較織込み
    ''' </summary>
    ''' <remarks></remarks>
    Public Class YosanSetteiBuhinHikaku

        Private _BuhinEditVoList As List(Of TShisakuBuhinEditVo)
        Private _shisakuEventCode As String
        Private _shisakuListCode As String
        Private _seihinKbn As String
        Private _shisakuGroup As String
        Private _JikyuFlag As Boolean
        Private HikakuImpl As YosanSetteiBuhinHikakuDao
        Private SakuseiImpl As YosanSetteiBuhinSakuseiDao
        Private sql As String
        Private GousyaList As New List(Of TShisakuEventBaseVo)
        Private _Eventvo As TShisakuEventVo

        ''' <summary>
        ''' 初期設定
        ''' </summary>
        ''' <param name="BuhinEditVoList">部品編集情報</param>
        ''' <param name="shisakuEventCode">試作イベントコード</param>
        ''' <param name="shisakuListCode">試作リストコード</param>
        ''' <param name="seihinKbn">製品区分</param>
        ''' <param name="shisakuGroup">試作グループ</param>
        ''' <param name="JikyuFlag">自給品フラグ</param>
        ''' <remarks></remarks>
        Public Sub New(ByVal BuhinEditVoList As List(Of TShisakuBuhinEditVo), _
                       ByVal shisakuEventCode As String, _
                       ByVal shisakuListCode As String, _
                       ByVal seihinKbn As String, _
                       ByVal shisakuGroup As String, _
                       ByVal JikyuFlag As Boolean)
            _BuhinEditVoList = BuhinEditVoList
            _shisakuEventCode = shisakuEventCode
            _shisakuListCode = shisakuListCode
            _seihinKbn = seihinKbn
            _shisakuGroup = shisakuGroup
            _JikyuFlag = JikyuFlag
            HikakuImpl = New YosanSetteiBuhinHikakuDaoImpl
            SakuseiImpl = New YosanSetteiBuhinSakuseiDaoImpl
            _shisakuGroup = shisakuGroup
            Dim daoEv As TShisakuEventDao = New TShisakuEventDaoImpl
            _Eventvo = daoEv.FindByPk(shisakuEventCode)

        End Sub


        ''' <summary>
        ''' 部品編集情報とベースとなる部品編集情報を比較する
        ''' </summary>
        ''' <remarks></remarks>
        Public Sub Hikaku()

            'ベースの情報を手配帳イメージにする'GYOU_ID:888
            CreateBaseTmp()

            '現在のTMPイメージでまわす'
            For Each Vo As TShisakuBuhinEditVo In _BuhinEditVoList

                If Vo.KyoukuSection Is Nothing Then
                    Vo.KyoukuSection = ""
                End If
                If StringUtil.IsEmpty(Vo.ShukeiCode) Then
                    Vo.ShukeiCode = ""
                End If
                If StringUtil.IsEmpty(Vo.SiaShukeiCode) Then
                    Vo.SiaShukeiCode = ""
                End If

                'イベントコード、部課コード、ブロックNo、レベル、部品番号、部品番号試作区分で'
                'ベースとなる部品編集情報を検索する'
                Dim GousyaList As New List(Of TYosanSetteiGousyaTmpVo)
                Dim BuhinEditBaseVoList As New List(Of TShisakuBuhinEditBaseVo)
                Dim BuhinEditBaseVo As New TShisakuBuhinEditBaseVo

                'ベースの部品編集情報を取得する'
                If _Eventvo.BlockAlertKind = 2 And _Eventvo.KounyuShijiFlg = "0" Then
                    Dim param As New TShisakuBuhinEditBaseVo
                    param.ShisakuEventCode = Vo.ShisakuEventCode
                    param.ShisakuBukaCode = Vo.ShisakuBukaCode
                    param.ShisakuBlockNo = Vo.ShisakuBlockNo
                    param.Level = Vo.Level
                    param.BuhinNo = Vo.BuhinNo
                    param.BuhinNoKbn = Vo.BuhinNoKbn
                    param.ShukeiCode = Vo.ShukeiCode
                    param.SiaShukeiCode = Vo.SiaShukeiCode
                    'param.GencyoCkdKbn = Vo.GencyoCkdKbn
                    param.KyoukuSection = Vo.KyoukuSection
                    param.BaseBuhinFlg = Vo.BaseBuhinFlg
                    BuhinEditBaseVoList = HikakuImpl.FindByTsuikaHinban(param)
                Else
                    BuhinEditBaseVoList = HikakuImpl.FindByTsuikaHinban(Vo.ShisakuEventCode, Vo.ShisakuBukaCode, Vo.ShisakuBlockNo, Vo.Level, Vo.BuhinNo, Vo.BuhinNoKbn)
                End If
                '集計コードで確認する'
                If BuhinEditBaseVoList.Count > 1 Then
                    '本当に一致するデータか確認する'
                    For Each baseVo As TShisakuBuhinEditBaseVo In BuhinEditBaseVoList

                        If StringUtil.IsEmpty(baseVo.ShukeiCode) Then
                            baseVo.ShukeiCode = ""
                        End If
                        If StringUtil.IsEmpty(baseVo.SiaShukeiCode) Then
                            baseVo.SiaShukeiCode = ""
                        End If

                        If StringUtil.Equals(Vo.ShukeiCode, baseVo.ShukeiCode) _
                        AndAlso StringUtil.Equals(Vo.SiaShukeiCode, baseVo.SiaShukeiCode) Then
                            If StringUtil.Equals(Vo.KyoukuSection, baseVo.KyoukuSection) Then
                                BuhinEditBaseVo = baseVo
                                Exit For
                            End If

                        End If

                        'If Vo.ShukeiCode.TrimEnd = "" Then
                        '    If baseVo.ShukeiCode.TrimEnd = "" Then
                        '        '国内が両方空なら海外'
                        '        If Vo.SiaShukeiCode = baseVo.SiaShukeiCode Then
                        '            '供給セクションが同一か'
                        '            If Vo.KyoukuSection = baseVo.KyoukuSection Then
                        '                BuhinEditBaseVo = baseVo
                        '                Exit For
                        '            End If
                        '        End If
                        '    End If
                        'Else
                        '    If baseVo.ShukeiCode.TrimEnd <> "" Then
                        '        If Vo.ShukeiCode = baseVo.ShukeiCode Then
                        '            '供給セクションが同一か'
                        '            If Vo.KyoukuSection = baseVo.KyoukuSection Then
                        '                BuhinEditBaseVo = baseVo
                        '                Exit For
                        '            End If
                        '        End If
                        '    End If
                        'End If
                    Next
                ElseIf BuhinEditBaseVoList.Count = 1 Then
                    BuhinEditBaseVo = BuhinEditBaseVoList(0)
                End If

                If StringUtil.IsEmpty(BuhinEditBaseVo.ShisakuEventCode) Then
                    BuhinEditBaseVo = Nothing
                End If

                If Vo.Saishiyoufuka.TrimEnd <> "" Then
                    '再使用不可に何かあれば追加にする'
                    SqlCreateBuhinEdit(Vo, 3)
                    GousyaList = HikakuImpl.FindByBuhinEditGousya(Vo.ShisakuEventCode, _
                                                                  Vo.ShisakuBukaCode, _
                                                                  Vo.ShisakuBlockNo, _
                                                                  Vo.ShisakuBlockNoKaiteiNo, _
                                                                  Vo.BuhinNoHyoujiJun)

                    For Each gVo As TYosanSetteiGousyaTmpVo In GousyaList
                        SqlCreateGousyaEdit(gVo, 3)
                    Next
                    Continue For
                End If


                '該当データが存在するかチェック'
                If Not BuhinEditBaseVo Is Nothing Then
                    '該当データが存在した場合'

                    '員数チェックする'
                    If InsuCheck(Vo, BuhinEditBaseVo) Then
                        '該当データが存在した場合その他の全ての項目がマッチするかチェック'
                        If AllCheck(BuhinEditBaseVo, Vo) Then
                            '存在しなければ追加'
                            SqlCreateBuhinEdit(Vo, 1)
                            '部品編集号車TMPにも追加'
                            GousyaList = HikakuImpl.FindByBuhinEditGousya(Vo.ShisakuEventCode, _
                                                                          Vo.ShisakuBukaCode, _
                                                                          Vo.ShisakuBlockNo, _
                                                                          Vo.ShisakuBlockNoKaiteiNo, _
                                                                          Vo.BuhinNoHyoujiJun)
                            For Each gVo As TYosanSetteiGousyaTmpVo In GousyaList
                                gVo.UpdatedUserId = "14"
                                SqlCreateGousyaEdit(gVo, 1)
                            Next
                        Else

                            'TMPへ存在チェック'
                            'どのSQL文にするか'
                            Dim flag As Boolean = False

                            '集計コードが両方空か？'
                            If Vo.ShukeiCode.TrimEnd = "" AndAlso BuhinEditBaseVo.ShukeiCode.TrimEnd = "" Then
                                '両方空なら海外集計は必ず存在するから'
                                If Vo.SiaShukeiCode = BuhinEditBaseVo.SiaShukeiCode Then
                                    '同一なら現調区分をチェック'
                                    If StringUtil.IsEmpty(Vo.GencyoCkdKbn) AndAlso StringUtil.IsEmpty(BuhinEditBaseVo.GencyoCkdKbn) Then
                                        '両方空なら変更無し'
                                        flag = False
                                    Else
                                        If Vo.GencyoCkdKbn = BuhinEditBaseVo.GencyoCkdKbn Then
                                            '現調区分が同一なら変更なし'
                                            flag = False
                                        Else
                                            '同一でないなら変更あり'
                                            flag = True
                                        End If
                                    End If
                                Else
                                    '海外集計が異なるから変更有り'
                                    flag = True
                                End If
                            Else
                                If Vo.ShukeiCode = BuhinEditBaseVo.ShukeiCode Then
                                    '集計コードが同一なら変更無し'
                                    flag = False
                                Else
                                    '集計コードが同一で無いなら変更有り'
                                    flag = True
                                End If
                            End If


                            '行IDでわけたので'

                            '変更有りか変更無しか'
                            If flag Then
                                '変更有りで存在していないので'
                                SqlCreateBuhinEdit(Vo, 7)
                                GousyaList = HikakuImpl.FindByBuhinEditGousya(Vo.ShisakuEventCode, _
                                                                              Vo.ShisakuBukaCode, _
                                                                              Vo.ShisakuBlockNo, _
                                                                              Vo.ShisakuBlockNoKaiteiNo, _
                                                                              Vo.BuhinNoHyoujiJun)
                                '2012/02/24 何故頭だけいれる？'
                                'For Each gVo As TYosanSetteiGousyaTmpVo In GousyaList
                                '    SqlCreateGousyaEdit(GousyaList(0), 7)
                                'Next
                                '2012/02/24 こっちでよくない？'
                                '2012/02/25 仕様見た限りでは頭だけなんてかいてないので'
                                For Each gVo As TYosanSetteiGousyaTmpVo In GousyaList
                                    SqlCreateGousyaEdit(gVo, 7)
                                Next
                            Else
                                '変更無しで存在していないので'
                                SqlCreateBuhinEdit(Vo, 1)
                                GousyaList = HikakuImpl.FindByBuhinEditGousya(Vo.ShisakuEventCode, _
                                                                              Vo.ShisakuBukaCode, _
                                                                              Vo.ShisakuBlockNo, _
                                                                              Vo.ShisakuBlockNoKaiteiNo, _
                                                                              Vo.BuhinNoHyoujiJun)
                                '2012/02/24 号車一つだけ登録するのはおかしい・・・が何か意図があったのかも？'
                                'SqlCreateGousyaEdit(GousyaList(0), 1)

                                '2012/02/24 こっちでよくない？'
                                '2012/02/25 仕様見た限りでは頭だけなんてかいてないので'
                                For Each gVo As TYosanSetteiGousyaTmpVo In GousyaList
                                    gVo.UpdatedUserId = "15"
                                    SqlCreateGousyaEdit(gVo, 1)
                                Next

                            End If
                        End If
                    End If
                Else
                    '存在しない場合は削除'
                    SqlCreateBuhinEdit(Vo, 3)
                    GousyaList = HikakuImpl.FindByBuhinEditGousya(Vo.ShisakuEventCode, _
                                                                  Vo.ShisakuBukaCode, _
                                                                  Vo.ShisakuBlockNo, _
                                                                  Vo.ShisakuBlockNoKaiteiNo, _
                                                                  Vo.BuhinNoHyoujiJun)

                    For Each gVo As TYosanSetteiGousyaTmpVo In GousyaList
                        SqlCreateGousyaEdit(gVo, 3)
                    Next
                End If
            Next
        End Sub

        ''' <summary>
        ''' ベースの情報を使って手配帳イメージを作成する
        ''' </summary>
        ''' <remarks></remarks>
        Private Sub CreateBaseTmp()
            'ベースの情報を取得する'

            Dim BaseTmpVoList As New List(Of YosanSetteiBuhinEditTmpVo)
            BaseTmpVoList = HikakuImpl.FindByBaseTmpVoList(_shisakuEventCode, _shisakuGroup, _JikyuFlag)

            '移管車改修の場合、供給セクションを無視してマージを行いたいため
            '最新の供給セクションをベースへコピーする
            If _Eventvo.BlockAlertKind = 2 And _Eventvo.KounyuShijiFlg = "0" Then
                Dim lstKyoukyuSection As Hashtable = getKyokyusectionList()
                For Each vo As YosanSetteiBuhinEditTmpVo In BaseTmpVoList
                    If vo.BaseBuhinFlg = "1" Then
                        Dim key As New System.Text.StringBuilder
                        key.AppendLine(vo.ShisakuBlockNo)
                        key.AppendLine(CStr(vo.BuhinNoHyoujiJun))
                        If lstKyoukyuSection.Contains(key.ToString) Then
                            vo.KyoukuSection = lstKyoukyuSection.Item(key.ToString)
                        End If
                    End If
                Next
            End If

            'マージする'
            Dim NewBaseTmpVoList As New List(Of YosanSetteiBuhinEditTmpVo)
            NewBaseTmpVoList = MergeTmp(BaseTmpVoList)

            'マージした結果をTMPに追加する'
            HikakuImpl.InsertByBuhinEditTMPBase(NewBaseTmpVoList, _seihinKbn)
            HikakuImpl.InsertByGousyaTMPBase(NewBaseTmpVoList)
        End Sub

        ''' <summary>
        ''' 最新のデータの供給セクションをベースデータの供給セクションへ上書きする
        ''' </summary>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Private Function getKyokyusectionList() As Hashtable
            Dim hsh As New Hashtable

            For Each voBlock As TShisakuSekkeiBlockVo In HikakuImpl.getBlockList(_shisakuEventCode)
                Dim vosBase As List(Of TShisakuBuhinEditVo) = HikakuImpl.FindTShisakuBuhinEdit(_shisakuEventCode, voBlock.ShisakuBlockNo, "BASE")
                Dim vosNew As List(Of TShisakuBuhinEditVo) = HikakuImpl.FindTShisakuBuhinEdit(_shisakuEventCode, voBlock.ShisakuBlockNo, voBlock.ShisakuBlockNoKaiteiNo)
                For Each voBase As TShisakuBuhinEditVo In vosBase
                    For i As Integer = 0 To vosNew.Count - 1

                        ''2015/09/03
                        If voBase.BaseBuhinSeq IsNot Nothing Then
                            If voBase.BaseBuhinSeq = vosNew(i).BaseBuhinSeq Then
                                Dim key As New System.Text.StringBuilder
                                key.AppendLine(voBase.ShisakuBlockNo)
                                key.AppendLine(CStr(voBase.BuhinNoHyoujiJun))
                                hsh.Add(key.ToString, vosNew(i).KyoukuSection)
                                vosNew.RemoveAt(i)
                                Exit For
                            End If
                        Else
                            If voBase.BuhinNo = vosNew(i).BuhinNo AndAlso _
                               voBase.BuhinNoKbn = vosNew(i).BuhinNoKbn AndAlso _
                               voBase.ShukeiCode = vosNew(i).ShukeiCode AndAlso _
                               voBase.SiaShukeiCode = vosNew(i).SiaShukeiCode AndAlso _
                               voBase.Level = vosNew(i).Level AndAlso _
                               voBase.GencyoCkdKbn = vosNew(i).GencyoCkdKbn Then

                                Dim key As New System.Text.StringBuilder
                                key.AppendLine(voBase.ShisakuBlockNo)
                                key.AppendLine(CStr(voBase.BuhinNoHyoujiJun))
                                hsh.Add(key.ToString, vosNew(i).KyoukuSection)
                                vosNew.RemoveAt(i)
                                Exit For
                            End If
                        End If
                    Next
                Next
            Next

            Return hsh
        End Function


        ''' <summary>
        ''' マージする(TMP用)
        ''' </summary>
        ''' <param name="MergeList"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Private Function MergeTmp(ByVal MergeList As List(Of YosanSetteiBuhinEditTmpVo)) As List(Of YosanSetteiBuhinEditTmpVo)
            '別のやり方を模索中・・・・'
            For index As Integer = 0 To MergeList.Count - 1

                'マージ済みならスルー
                If MergeList(index).CreatedUserId = "Merge" Then
                    Continue For
                End If

                If StringUtil.IsEmpty(MergeList(index).ShukeiCode) Then
                    MergeList(index).ShukeiCode = ""
                End If
                If StringUtil.IsEmpty(MergeList(index).SiaShukeiCode) Then
                    MergeList(index).SiaShukeiCode = ""
                End If

                'Nothingと空は比較しないので'
                If MergeList(index).KyoukuSection Is Nothing Then
                    MergeList(index).KyoukuSection = ""
                End If

                For index2 As Integer = 0 To MergeList.Count - 1

                    'マージ済みならスルー
                    If MergeList(index2).CreatedUserId = "Merge" Then
                        Continue For
                    End If
                    '部課コードがアンマッチならスルー
                    If MergeList(index).ShisakuBukaCode <> MergeList(index2).ShisakuBukaCode Then
                        Continue For
                    End If
                    If MergeList(index2).KyoukuSection Is Nothing Then
                        MergeList(index2).KyoukuSection = ""
                    End If


                    'ブロックNoが同一かチェック'
                    If MergeList(index).ShisakuBlockNo = MergeList(index2).ShisakuBlockNo Then

                        'If StringUtil.Equals(MergeList(index).ShisakuBukaCode, MergeList(index2).ShisakuBukaCode) Then

                        'レベル,部品番号,集計コードはNULLがないのでまとめて同一チェック'
                        If MergeList(index).Level = MergeList(index2).Level Then

                            If MergeList(index).BuhinNo = MergeList(index2).BuhinNo Then

                                '集計コードのチェックは複数パターンある'

                                '比較元の集計コードがない場合

                                If StringUtil.Equals(MergeList(index).ShukeiCode, MergeList(index2).ShukeiCode) _
                                AndAlso StringUtil.Equals(MergeList(index).SiaShukeiCode, MergeList(index2).SiaShukeiCode) Then
                                    MeargeCore(index, index2, MergeList)
                                End If


                                'If MergeList(index).ShukeiCode.TrimEnd = "" Then

                                '    '比較先の集計コードが無い時
                                '    If StringUtil.IsEmpty(MergeList(index2).ShukeiCode) Then

                                '        '国内集計コードが両方空なら海外集計コードを比較'
                                '        If MergeList(index).SiaShukeiCode = MergeList(index2).SiaShukeiCode Then

                                '            '供給セクションの同一判定・号車表示順評価（分かりにくいのでリファクタリング）
                                '            MeargeCore(index, index2, MergeList)

                                '        Else
                                '            '国内集計コードが両方空且つ、海外集計コードが一致しない場合
                                '            '比較先または比較元のいずれかが空の場合はマージ処理へ回す
                                '            If StringUtil.IsEmpty(MergeList(index).SiaShukeiCode) OrElse StringUtil.IsEmpty(MergeList(index2).SiaShukeiCode) Then

                                '                '供給セクションの同一判定・号車表示順評価（分かりにくいのでリファクタリング）
                                '                MeargeCore(index, index2, MergeList)

                                '            End If
                                '        End If

                                '    Else

                                '        '比較元の国内集計コードが無の場合で、比較先の国内集計コードがある場合
                                '        If MergeList(index).SiaShukeiCode = MergeList(index2).ShukeiCode Then

                                '            '供給セクションの同一判定・号車表示順評価（分かりにくいのでリファクタリング）
                                '            'MeargeCore(index, index2, MergeList)

                                '        End If


                                '    End If
                                'Else
                                '    '比較元の集計コードがある場合
                                '    '両方からでないかチェック'
                                '    If MergeList(index2).ShukeiCode.TrimEnd <> "" Then
                                '        '両方空でないならチェック'

                                '        If MergeList(index).ShukeiCode = MergeList(index2).ShukeiCode Then

                                '            '供給セクションの同一判定・号車表示順評価（分かりにくいのでリファクタリング）
                                '            MeargeCore(index, index2, MergeList)

                                '        End If
                                '    Else
                                '        '比較元の集計コードがあり、比較先の集計コードと一致しない場合
                                '        '比較先の海外集計コードとの一致判定を行い、符号が一致していれば諸条件判定の上マージ
                                '        '※比較元の国内集計コードがある場合はこれを基点として、海外集計コードとの斜め判定を行う
                                '        If MergeList(index).ShukeiCode = MergeList(index2).SiaShukeiCode Then

                                '            '供給セクションの同一判定・号車表示順評価（分かりにくいのでリファクタリング）
                                '            'MeargeCore(index, index2, MergeList)

                                '        Else
                                '            If MergeList(index2).SiaShukeiCode = "" Then

                                '                '供給セクションの同一判定・号車表示順評価（分かりにくいのでリファクタリング）
                                '                MeargeCore(index, index2, MergeList)

                                '            End If

                                '        End If

                                '    End If

                                'End If
                            End If
                        End If
                        'End If
                    End If
                Next
            Next

            Return MergeList

        End Function

        Private Sub MeargeCore(ByVal index As Integer, ByVal index2 As Integer, ByRef MergeList As List(Of YosanSetteiBuhinEditTmpVo))

            '供給セクションが同一か？'
            '一致しなければ評価終わり
            If MergeList(index).KyoukuSection <> MergeList(index2).KyoukuSection Then
                Exit Sub
            End If

            '再使用区分が同一か？'
            '一致しなければ評価終わり
            If MergeList(index).Saishiyoufuka <> MergeList(index2).Saishiyoufuka Then
                Exit Sub
            End If

            '海外集計コードが同一ならマージ可能'
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

                ElseIf MergeList(index).BuhinNoHyoujiJun < MergeList(index2).BuhinNoHyoujiJun Then

                    MergeList(index).BuhinNoHyoujiJun = MergeList(index2).BuhinNoHyoujiJun
                    MergeList(index).CreatedUserId = "Merge"

                End If
            End If

        End Sub



        ''' <summary>
        ''' 部品編集情報を基準に員数を比較
        ''' </summary>
        ''' <param name="BuhinEditVo">部品編集情報</param>
        ''' <returns>Trueなら変更無し</returns>
        ''' <remarks></remarks>
        Private Function InsuCheck(ByVal BuhinEditVo As TShisakuBuhinEditVo, ByVal BuhinEditBaseVo As TShisakuBuhinEditBaseVo) As Boolean

            Dim NotChangeFlag As Boolean = True
            Dim buhinNoHyoujiJun As Integer

            buhinNoHyoujiJun = HikakuImpl.FindByNewBuhinNoHyoujiJun(BuhinEditVo.ShisakuEventCode, BuhinEditVo.ShisakuBukaCode, BuhinEditVo.ShisakuBlockNo)

            Dim Check As String = ""

            'ベースとなる部品編集INSTL情報'gyoid=888
            Dim BuhinEditBaseInstlListVo As New List(Of TYosanSetteiGousyaTmpVo)
            BuhinEditBaseInstlListVo = HikakuImpl.FindByBuhinEditGousyaBase(BuhinEditBaseVo.ShisakuEventCode, _
                                                                            BuhinEditBaseVo.ShisakuBukaCode, _
                                                                            BuhinEditBaseVo.ShisakuBlockNo, _
                                                                            BuhinEditBaseVo.BuhinNoHyoujiJun)

            '部品編集INSTL情報'gyoid=999
            Dim BuhinEditInstlListVo As New List(Of TYosanSetteiGousyaTmpVo)
            BuhinEditInstlListVo = HikakuImpl.FindByBuhinEditGousya(BuhinEditVo.ShisakuEventCode, _
                                                                    BuhinEditVo.ShisakuBukaCode, _
                                                                    BuhinEditVo.ShisakuBlockNo, _
                                                                    BuhinEditVo.ShisakuBlockNoKaiteiNo, _
                                                                    BuhinEditVo.BuhinNoHyoujiJun)

            'ベースとなる員数の合計'
            Dim BaseInsuSuryo As Integer = 0
            If BuhinEditBaseInstlListVo.Count = 0 Then
                BaseInsuSuryo = 0
            Else
                For Each baseInstlVo As TYosanSetteiGousyaTmpVo In BuhinEditBaseInstlListVo
                    BaseInsuSuryo = BaseInsuSuryo + baseInstlVo.InsuSuryo
                Next
            End If

            '員数の合計'
            Dim EditInsuSuryo As Integer = 0
            If BuhinEditInstlListVo.Count = 0 Then
                EditInsuSuryo = 0
            Else
                For Each editInstlVo As TYosanSetteiGousyaTmpVo In BuhinEditInstlListVo
                    EditInsuSuryo = EditInsuSuryo + editInstlVo.InsuSuryo
                Next
            End If


            '合計員数が0未満なら0にする'

            If EditInsuSuryo < 0 Then
                EditInsuSuryo = 0
            End If

            If BaseInsuSuryo < 0 Then
                BaseInsuSuryo = 0
            End If



            '員数を比較'
            If EditInsuSuryo = BaseInsuSuryo Then
                '合計員数に変更が無くても号車ごとで見ると差異がある場合があるのでチェックする'
                Dim changeFlag As Boolean = False

                'パターン１号車の総数に差異があるパターン'
                If Not BuhinEditInstlListVo.Count = BuhinEditBaseInstlListVo.Count Then
                    changeFlag = True
                Else
                    'パターン２号車の総数はあっているが適用のある号車が異なっているパターン'
                    For index As Integer = 0 To BuhinEditInstlListVo.Count - 1
                        If Not BuhinEditInstlListVo(index).ShisakuGousyaHyoujiJun = BuhinEditBaseInstlListVo(index).ShisakuGousyaHyoujiJun Then
                            changeFlag = True
                            Exit For
                        Else
                            'パターン３号車の適用もあっているが員数に差があるパターン'
                            Dim editInsu As Integer = BuhinEditInstlListVo(index).InsuSuryo
                            Dim baseInsu As Integer = BuhinEditBaseInstlListVo(index).InsuSuryo

                            If editInsu < 0 Then
                                editInsu = 0
                            End If

                            If baseInsu < 0 Then
                                baseInsu = 0
                            End If

                            If Not editInsu = baseInsu Then
                                changeFlag = True
                                Exit For
                            End If
                        End If
                    Next
                End If



                '変更箇所があるかないか'
                If changeFlag Then
                    '変更箇所があった場合は変更箇所は手配対象・変更の無い箇所は手配非対象'
                    '員数増と同じ処理になる'
                    'TMPに存在チェック'

                    '行IDでわけるから必ず追加'

                    '変更無しを追加'
                    SqlCreateBuhinEdit(BuhinEditVo, 8)
                    'ベース側の員数をFで'
                    For Each gVo As TYosanSetteiGousyaTmpVo In BuhinEditBaseInstlListVo
                        '員数と号車はベース、それ以外は前回で追加'
                        Dim NewGousya As New TYosanSetteiGousyaTmpVo
                        NewGousya.ShisakuEventCode = BuhinEditVo.ShisakuEventCode
                        NewGousya.ShisakuBukaCode = BuhinEditVo.ShisakuBukaCode
                        NewGousya.ShisakuBlockNo = BuhinEditVo.ShisakuBlockNo
                        NewGousya.ShisakuBlockNoKaiteiNo = BuhinEditVo.ShisakuBlockNoKaiteiNo
                        NewGousya.BuhinNoHyoujiJun = BuhinEditVo.BuhinNoHyoujiJun
                        NewGousya.ShisakuGousyaHyoujiJun = gVo.ShisakuGousyaHyoujiJun
                        NewGousya.ShisakuGousya = gVo.ShisakuGousya
                        NewGousya.InsuSuryo = gVo.InsuSuryo
                        NewGousya.UpdatedUserId = "11"


                        SqlCreateGousyaEdit(NewGousya, 1)
                    Next

                    '部品番号表示順を変更する'
                    Dim newBuhinEditVo As New TShisakuBuhinEditVo
                    newBuhinEditVo = BuhinEditVo
                    newBuhinEditVo.BuhinNoHyoujiJun = buhinNoHyoujiJun

                    '追加分の員数を追加する'
                    SqlCreateBuhinEdit(newBuhinEditVo, 5)


                    For index As Integer = 0 To BuhinEditInstlListVo.Count - 1
                        Dim insusa As Integer = 0
                        For index2 As Integer = 0 To BuhinEditBaseInstlListVo.Count - 1
                            If BuhinEditInstlListVo(index).ShisakuGousyaHyoujiJun = BuhinEditBaseInstlListVo(index2).ShisakuGousyaHyoujiJun Then
                                '同一の号車表示順の員数で差を見る'
                                insusa = BuhinEditInstlListVo(index).InsuSuryo - BuhinEditBaseInstlListVo(index2).InsuSuryo
                                Exit For
                            Else
                                '号車の表示順で該当するものが無い場合は'
                                insusa = BuhinEditInstlListVo(index).InsuSuryo
                            End If
                        Next
                        '0以下は登録しない'
                        If insusa <= 0 Then
                            Continue For
                        End If

                        Dim NewGousyaVo As New TYosanSetteiGousyaTmpVo
                        NewGousyaVo.ShisakuEventCode = BuhinEditVo.ShisakuEventCode
                        NewGousyaVo.ShisakuBukaCode = BuhinEditVo.ShisakuBukaCode
                        NewGousyaVo.ShisakuBlockNo = BuhinEditVo.ShisakuBlockNo
                        NewGousyaVo.ShisakuBlockNoKaiteiNo = BuhinEditVo.ShisakuBlockNoKaiteiNo
                        NewGousyaVo.BuhinNoHyoujiJun = buhinNoHyoujiJun
                        NewGousyaVo.ShisakuGousyaHyoujiJun = BuhinEditInstlListVo(index).ShisakuGousyaHyoujiJun
                        NewGousyaVo.ShisakuGousya = BuhinEditInstlListVo(index).ShisakuGousya
                        NewGousyaVo.InsuSuryo = insusa


                        SqlCreateGousyaEdit(NewGousyaVo, 5)
                    Next
                    NotChangeFlag = False
                Else
                    '員数側に変更箇所は無いのでそのままにしておく'
                    ''変更箇所は無いのでそのまま追加'
                    'If HikakuImpl.FindByTsuikaHinbanTmp(BuhinEditVo) Then
                    '    '存在したら更新'
                    '    SqlCreateBuhinEdit(BuhinEditVo, 0)
                    'Else
                    '    '存在していないなら追加'
                    '    SqlCreateBuhinEdit(BuhinEditVo, 1)
                    '    For Each gVo As TYosanSetteiGousyaTmpVo In BuhinEditInstlListVo
                    '        SqlCreateGousyaEdit(gVo, 1)
                    '    Next
                    'End If
                End If
            ElseIf EditInsuSuryo < BaseInsuSuryo Then
                '員数減'
                '存在していないなら追加'
                'SqlCreateBuhinEdit(BuhinEditVo, 7)
                'For Each gVo As TYosanSetteiGousyaTmpVo In BuhinEditInstlListVo
                '    SqlCreateGousyaEdit(gVo, 7)
                'Next

                'ベース変更なしで追加'
                SqlCreateBuhinEdit(BuhinEditVo, 8)

                '前回分を変更無しで追加'
                For Each gVo As TYosanSetteiGousyaTmpVo In BuhinEditBaseInstlListVo
                    Dim gousyaVo As New TYosanSetteiGousyaTmpVo
                    gousyaVo.ShisakuEventCode = BuhinEditVo.ShisakuEventCode
                    gousyaVo.ShisakuBukaCode = BuhinEditVo.ShisakuBukaCode
                    gousyaVo.ShisakuBlockNo = BuhinEditVo.ShisakuBlockNo
                    gousyaVo.ShisakuBlockNoKaiteiNo = BuhinEditVo.ShisakuBlockNoKaiteiNo
                    gousyaVo.BuhinNoHyoujiJun = BuhinEditVo.BuhinNoHyoujiJun
                    gousyaVo.ShisakuGousyaHyoujiJun = gVo.ShisakuGousyaHyoujiJun
                    gousyaVo.ShisakuGousya = gVo.ShisakuGousya
                    gousyaVo.InsuSuryo = gVo.InsuSuryo

                    gousyaVo.UpdatedUserId = "12"
                    SqlCreateGousyaEdit(gousyaVo, 1)
                Next

                '存在していないなら追加'
                BuhinEditVo.BuhinNoHyoujiJun = buhinNoHyoujiJun
                Dim flag As Boolean = False

                '員数の差を員数増で追加'
                For index As Integer = 0 To BuhinEditInstlListVo.Count - 1
                    Dim insusa As Integer = 0
                    For index2 As Integer = 0 To BuhinEditBaseInstlListVo.Count - 1
                        If BuhinEditInstlListVo(index).ShisakuGousyaHyoujiJun = BuhinEditBaseInstlListVo(index2).ShisakuGousyaHyoujiJun Then
                            '同一の号車表示順の員数で差を見る'
                            Dim iInsu As Integer = BuhinEditInstlListVo(index).InsuSuryo
                            Dim bInsu As Integer = BuhinEditBaseInstlListVo(index2).InsuSuryo

                            If iInsu < 0 Then
                                iInsu = 0
                            End If
                            If bInsu < 0 Then
                                bInsu = 0
                            End If

                            insusa = iInsu - bInsu
                            Exit For
                        Else
                            '号車の表示順で該当するものが無い場合は'
                            insusa = BuhinEditInstlListVo(index).InsuSuryo
                        End If
                    Next

                    '0以下は登録しない'
                    If insusa <= 0 Then
                        Continue For
                    End If

                    Dim NewGousyaVo As New TYosanSetteiGousyaTmpVo
                    NewGousyaVo.ShisakuEventCode = BuhinEditVo.ShisakuEventCode
                    NewGousyaVo.ShisakuBukaCode = BuhinEditVo.ShisakuBukaCode
                    NewGousyaVo.ShisakuBlockNo = BuhinEditVo.ShisakuBlockNo
                    NewGousyaVo.ShisakuBlockNoKaiteiNo = BuhinEditVo.ShisakuBlockNoKaiteiNo
                    NewGousyaVo.BuhinNoHyoujiJun = buhinNoHyoujiJun
                    NewGousyaVo.ShisakuGousyaHyoujiJun = BuhinEditInstlListVo(index).ShisakuGousyaHyoujiJun
                    NewGousyaVo.ShisakuGousya = BuhinEditInstlListVo(index).ShisakuGousya
                    NewGousyaVo.InsuSuryo = insusa

                    SqlCreateGousyaEdit(NewGousyaVo, 5)
                    flag = True
                Next
                If flag Then
                    SqlCreateBuhinEdit(BuhinEditVo, 5)
                End If

                NotChangeFlag = False
            ElseIf EditInsuSuryo > BaseInsuSuryo Then
                '員数増'

                '行IDでわけてあるから必ず追加'

                '変更無しで追加'
                SqlCreateBuhinEdit(BuhinEditVo, 8)

                '前回分を変更無しで追加'
                For Each gVo As TYosanSetteiGousyaTmpVo In BuhinEditBaseInstlListVo
                    Dim gousyaVo As New TYosanSetteiGousyaTmpVo
                    gousyaVo.ShisakuEventCode = BuhinEditVo.ShisakuEventCode
                    gousyaVo.ShisakuBukaCode = BuhinEditVo.ShisakuBukaCode
                    gousyaVo.ShisakuBlockNo = BuhinEditVo.ShisakuBlockNo
                    gousyaVo.ShisakuBlockNoKaiteiNo = BuhinEditVo.ShisakuBlockNoKaiteiNo
                    gousyaVo.BuhinNoHyoujiJun = BuhinEditVo.BuhinNoHyoujiJun
                    gousyaVo.ShisakuGousyaHyoujiJun = gVo.ShisakuGousyaHyoujiJun
                    gousyaVo.ShisakuGousya = gVo.ShisakuGousya
                    gousyaVo.InsuSuryo = gVo.InsuSuryo
                    gousyaVo.UpdatedUserId = "13"

                    SqlCreateGousyaEdit(gousyaVo, 1)
                Next

                '存在していないなら追加'
                BuhinEditVo.BuhinNoHyoujiJun = buhinNoHyoujiJun

                Dim flag As Boolean = False

                For index As Integer = 0 To BuhinEditInstlListVo.Count - 1
                    Dim insusa As Integer = 0
                    For index2 As Integer = 0 To BuhinEditBaseInstlListVo.Count - 1
                        If BuhinEditInstlListVo(index).ShisakuGousyaHyoujiJun = BuhinEditBaseInstlListVo(index2).ShisakuGousyaHyoujiJun Then
                            '同一の号車表示順の員数で差を見る'
                            Dim iInsu As Integer = BuhinEditInstlListVo(index).InsuSuryo
                            Dim bInsu As Integer = BuhinEditBaseInstlListVo(index2).InsuSuryo

                            If iInsu < 0 Then
                                iInsu = 0
                            End If
                            If bInsu < 0 Then
                                bInsu = 0
                            End If

                            insusa = iInsu - bInsu
                            Exit For
                        Else
                            '号車の表示順で該当するものが無い場合は'
                            insusa = BuhinEditInstlListVo(index).InsuSuryo
                        End If
                    Next

                    '0以下は登録しない'
                    If insusa <= 0 Then
                        Continue For
                    End If



                    Dim NewGousyaVo As New TYosanSetteiGousyaTmpVo
                    NewGousyaVo.ShisakuEventCode = BuhinEditVo.ShisakuEventCode
                    NewGousyaVo.ShisakuBukaCode = BuhinEditVo.ShisakuBukaCode
                    NewGousyaVo.ShisakuBlockNo = BuhinEditVo.ShisakuBlockNo
                    NewGousyaVo.ShisakuBlockNoKaiteiNo = BuhinEditVo.ShisakuBlockNoKaiteiNo
                    NewGousyaVo.BuhinNoHyoujiJun = buhinNoHyoujiJun
                    NewGousyaVo.ShisakuGousyaHyoujiJun = BuhinEditInstlListVo(index).ShisakuGousyaHyoujiJun
                    NewGousyaVo.ShisakuGousya = BuhinEditInstlListVo(index).ShisakuGousya
                    NewGousyaVo.InsuSuryo = insusa

                    SqlCreateGousyaEdit(NewGousyaVo, 5)
                    flag = True
                Next

                If flag Then
                    '員数の差を員数増で追加'
                    SqlCreateBuhinEdit(BuhinEditVo, 5)
                End If

                NotChangeFlag = False
            End If



            Return NotChangeFlag
        End Function

        ''' <summary>
        ''' 全ての項目がマッチするかチェックする
        ''' </summary>
        ''' <param name="BuhinEditBaseVo">ベースとなる部品編集情報</param>
        ''' <param name="BuhinEditVo">部品編集情報</param>
        ''' <returns>マッチしたらTrue</returns>
        ''' <remarks></remarks>
        Private Function AllCheck(ByVal BuhinEditBaseVo As TShisakuBuhinEditBaseVo, ByVal BuhinEditVo As TShisakuBuhinEditVo) As Boolean

            '出図予定日99999999は0扱い'
            If BuhinEditVo.ShutuzuYoteiDate = 99999999 Then
                BuhinEditVo.ShutuzuYoteiDate = 0
            End If
            If BuhinEditBaseVo.ShutuzuYoteiDate = 99999999 Then
                BuhinEditBaseVo.ShutuzuYoteiDate = 0
            End If



            '部品番号表示順が同一か？'
            'If Not BuhinEditBaseVo.BuhinNoHyoujiJun = BuhinEditVo.BuhinNoHyoujiJun Then
            '    Return False
            'End If
            '集計コードが同一か？'
            If BuhinEditBaseVo.ShukeiCode <> BuhinEditVo.ShukeiCode Then
                '20130510　以下のチェックは不要？　上記で同一チェックして違う場合はどちらかに値が有るわけだから。。。
                'If Not StringUtil.IsEmpty(BuhinEditVo.ShukeiCode) Or Not StringUtil.IsEmpty(BuhinEditBaseVo.ShukeiCode) Then
                Return False
                'End If
            End If
            '海外集計コードが同一か？'
            If BuhinEditBaseVo.SiaShukeiCode <> BuhinEditVo.SiaShukeiCode Then
                'If Not StringUtil.IsEmpty(BuhinEditVo.SiaShukeiCode) Or Not StringUtil.IsEmpty(BuhinEditBaseVo.SiaShukeiCode) Then
                Return False
                'End If
            End If

            '現調CKD区分が同一か？'
            If BuhinEditBaseVo.GencyoCkdKbn <> BuhinEditVo.GencyoCkdKbn Then
                'If Not StringUtil.IsEmpty(BuhinEditVo.GencyoCkdKbn) Or Not StringUtil.IsEmpty(BuhinEditBaseVo.GencyoCkdKbn) Then
                Return False
                'End If
            End If


            '取引先コードが同一か？'
            If BuhinEditBaseVo.MakerCode <> BuhinEditVo.MakerCode Then
                'If Not StringUtil.IsEmpty(BuhinEditVo.MakerCode) Or Not StringUtil.IsEmpty(BuhinEditBaseVo.MakerCode) Then
                Return False
                'End If
            End If
            '取引先名称が同一か？'
            If BuhinEditBaseVo.MakerName <> BuhinEditVo.MakerName Then
                'If Not StringUtil.IsEmpty(BuhinEditVo.MakerName) Or Not StringUtil.IsEmpty(BuhinEditBaseVo.MakerName) Then
                Return False
                'End If
            End If
            '部品番号が同一か？'
            'If Not StringUtil.Equals(BuhinEditBaseVo.BuhinNo, BuhinEditVo.BuhinNo) Then
            '    If Not StringUtil.IsEmpty(BuhinEditVo.SiaShukeiCode) Or Not StringUtil.IsEmpty(BuhinEditBaseVo.BuhinNo) Then
            '        Return False
            '    End If
            'End If
            '部品番号試作区分が同一か？'
            If BuhinEditBaseVo.BuhinNoKbn <> BuhinEditVo.BuhinNoKbn Then
                'If Not StringUtil.IsEmpty(BuhinEditVo.BuhinNoKbn) Or Not StringUtil.IsEmpty(BuhinEditBaseVo.BuhinNoKbn) Then
                Return False
                'End If
            End If
            '部品番号改訂Noが同一か？'
            If BuhinEditBaseVo.BuhinNoKaiteiNo <> BuhinEditVo.BuhinNoKaiteiNo Then
                'If Not StringUtil.IsEmpty(BuhinEditVo.BuhinNoKaiteiNo) Or Not StringUtil.IsEmpty(BuhinEditBaseVo.BuhinNoKaiteiNo) Then
                Return False
                'End If
            End If
            '枝番が同一か？'
            If BuhinEditBaseVo.EdaBan <> BuhinEditVo.EdaBan Then
                'If Not StringUtil.IsEmpty(BuhinEditVo.EdaBan) Or Not StringUtil.IsEmpty(BuhinEditBaseVo.EdaBan) Then
                Return False
                'End If
            End If
            '部品名称が同一か？'
            If Trim(BuhinEditBaseVo.BuhinName) <> Trim(BuhinEditVo.BuhinName) Then
                Return False
            End If
            '再使用不可が同一か？'
            If BuhinEditBaseVo.Saishiyoufuka <> BuhinEditVo.Saishiyoufuka Then
                'If Not StringUtil.IsEmpty(BuhinEditVo.Saishiyoufuka) Or Not StringUtil.IsEmpty(BuhinEditBaseVo.Saishiyoufuka) Then
                Return False
                'End If
            End If
            '出図予定日が同一か？'
            If Not BuhinEditBaseVo.ShutuzuYoteiDate = BuhinEditVo.ShutuzuYoteiDate Then
                'If Not StringUtil.IsEmpty(BuhinEditVo.ShutuzuYoteiDate) Or Not StringUtil.IsEmpty(BuhinEditBaseVo.ShutuzuYoteiDate) Then
                Return False
                'End If
            End If
            ''↓↓2014/07/24 Ⅰ.2.管理項目追加_aq) (TES)張 ADD BEGIN
            '作り方・製作方法が同一か？'
            If BuhinEditBaseVo.TsukurikataSeisaku <> BuhinEditVo.TsukurikataSeisaku Then
                'If Not StringUtil.IsEmpty(BuhinEditVo.TsukurikataSeisaku) Or Not StringUtil.IsEmpty(BuhinEditBaseVo.TsukurikataSeisaku) Then
                Return False
                'End If
            End If
            '作り方・型仕様1が同一か？'
            If BuhinEditBaseVo.TsukurikataKatashiyou1 <> BuhinEditVo.TsukurikataKatashiyou1 Then
                'If Not StringUtil.IsEmpty(BuhinEditVo.TsukurikataKatashiyou1) Or Not StringUtil.IsEmpty(BuhinEditBaseVo.TsukurikataKatashiyou1) Then
                Return False
                'End If
            End If
            '作り方・型仕様2が同一か？'
            If BuhinEditBaseVo.TsukurikataKatashiyou2 <> BuhinEditVo.TsukurikataKatashiyou2 Then
                'If Not StringUtil.IsEmpty(BuhinEditVo.TsukurikataKatashiyou2) Or Not StringUtil.IsEmpty(BuhinEditBaseVo.TsukurikataKatashiyou2) Then
                Return False
                'End If
            End If
            '作り方・型仕様3が同一か？'
            If BuhinEditBaseVo.TsukurikataKatashiyou3 <> BuhinEditVo.TsukurikataKatashiyou3 Then
                'If Not StringUtil.IsEmpty(BuhinEditVo.TsukurikataKatashiyou3) Or Not StringUtil.IsEmpty(BuhinEditBaseVo.TsukurikataKatashiyou3) Then
                Return False
                'End If
            End If
            '作り方・治具が同一か？'
            If BuhinEditBaseVo.TsukurikataTigu <> BuhinEditVo.TsukurikataTigu Then
                'If Not StringUtil.IsEmpty(BuhinEditVo.TsukurikataTigu) Or Not StringUtil.IsEmpty(BuhinEditBaseVo.TsukurikataTigu) Then
                Return False
                'End If
            End If
            '作り方・納入見通しが同一か？'
            If BuhinEditBaseVo.TsukurikataNounyu <> BuhinEditVo.TsukurikataNounyu Then
                'If Not StringUtil.IsEmpty(BuhinEditVo.TsukurikataNounyu) Or Not StringUtil.IsEmpty(BuhinEditBaseVo.TsukurikataNounyu) Then
                Return False
                'End If
            End If
            '作り方・部品製作規模・概要が同一か？'
            If BuhinEditBaseVo.TsukurikataKibo <> BuhinEditVo.TsukurikataKibo Then
                'If Not StringUtil.IsEmpty(BuhinEditVo.TsukurikataKibo) Or Not StringUtil.IsEmpty(BuhinEditBaseVo.TsukurikataKibo) Then
                Return False
                'End If
            End If
            ''↑↑2014/07/24 Ⅰ.2.管理項目追加_aq) (TES)張 ADD END
            '↓↓2014/09/26 酒井 ADD BEGIN
            If BuhinEditBaseVo.BaseBuhinFlg <> BuhinEditVo.BaseBuhinFlg Then
                'If Not StringUtil.IsEmpty(BuhinEditVo.TsukurikataKibo) Or Not StringUtil.IsEmpty(BuhinEditBaseVo.TsukurikataKibo) Then
                Return False
                'End If
            End If
            '↑↑2014/09/26 酒井 ADD END
            '材質規格１が同一か？'
            If BuhinEditBaseVo.ZaishituKikaku1 <> BuhinEditVo.ZaishituKikaku1 Then
                'If Not StringUtil.IsEmpty(BuhinEditVo.ZaishituKikaku1) Or Not StringUtil.IsEmpty(BuhinEditBaseVo.ZaishituKikaku1) Then
                Return False
                'End If
            End If
            '材質規格２が同一か？'
            If BuhinEditBaseVo.ZaishituKikaku2 <> BuhinEditVo.ZaishituKikaku2 Then
                'If Not StringUtil.IsEmpty(BuhinEditVo.ZaishituKikaku2) Or Not StringUtil.IsEmpty(BuhinEditBaseVo.ZaishituKikaku2) Then
                Return False
                'End If
            End If
            '材質規格３が同一か？'
            If BuhinEditBaseVo.ZaishituKikaku3 <> BuhinEditVo.ZaishituKikaku3 Then
                'If Not StringUtil.IsEmpty(BuhinEditVo.ZaishituKikaku3) Or Not StringUtil.IsEmpty(BuhinEditBaseVo.ZaishituKikaku3) Then
                Return False
                'End If
            End If
            '材質メッキが同一か？'
            If BuhinEditBaseVo.ZaishituMekki <> BuhinEditVo.ZaishituMekki Then
                'If Not StringUtil.IsEmpty(BuhinEditVo.ZaishituMekki) Or Not StringUtil.IsEmpty(BuhinEditBaseVo.ZaishituMekki) Then
                Return False
                'End If
            End If
            '板厚が同一か？'
            If BuhinEditBaseVo.ShisakuBankoSuryo <> BuhinEditVo.ShisakuBankoSuryo Then
                'If Not StringUtil.IsEmpty(BuhinEditVo.ShisakuBankoSuryo) Or Not StringUtil.IsEmpty(BuhinEditBaseVo.ShisakuBankoSuryo) Then
                Return False
                'End If
            End If
            '板厚uが同一か？'
            If BuhinEditBaseVo.ShisakuBankoSuryoU <> BuhinEditVo.ShisakuBankoSuryoU Then
                'If Not StringUtil.IsEmpty(BuhinEditVo.ShisakuBankoSuryoU) Or Not StringUtil.IsEmpty(BuhinEditBaseVo.ShisakuBankoSuryoU) Then
                Return False
                'End If
            End If


            ''↓↓2014/12/29 メタル対応追加フィールド (DANIEL)柳沼 ADD BEGIN
            '材料情報・製品長が同一か？'
            If BuhinEditBaseVo.MaterialInfoLength <> BuhinEditVo.MaterialInfoLength Then
                Return False
            End If
            '材料情報・製品幅が同一か？'
            If BuhinEditBaseVo.MaterialInfoWidth <> BuhinEditVo.MaterialInfoWidth Then
                Return False
            End If
            'データ項目・改訂№が同一か？'
            If BuhinEditBaseVo.DataItemKaiteiNo <> BuhinEditVo.DataItemKaiteiNo Then
                Return False
            End If
            'データ項目・エリア名が同一か？'
            If BuhinEditBaseVo.DataItemAreaName <> BuhinEditVo.DataItemAreaName Then
                Return False
            End If
            'データ項目・セット名が同一か？'
            If BuhinEditBaseVo.DataItemSetName <> BuhinEditVo.DataItemSetName Then
                Return False
            End If
            'データ項目・改訂情報が同一か？'
            If BuhinEditBaseVo.DataItemKaiteiInfo <> BuhinEditVo.DataItemKaiteiInfo Then
                Return False
            End If
            ''↑↑2014/12/29 メタル対応追加フィールド (DANIEL)柳沼 ADD BEGIN


            '試作部品費が同一か？'
            If BuhinEditBaseVo.ShisakuBuhinHi <> BuhinEditVo.ShisakuBuhinHi Then
                'If Not StringUtil.IsEmpty(BuhinEditVo.ShisakuBuhinHi) Or Not StringUtil.IsEmpty(BuhinEditBaseVo.ShisakuBuhinHi) Then
                Return False
                'End If
            End If
            '試作型費が同一か？'
            If BuhinEditBaseVo.ShisakuKataHi <> BuhinEditVo.ShisakuKataHi Then
                'If Not StringUtil.IsEmpty(BuhinEditVo.ShisakuKataHi) Or Not StringUtil.IsEmpty(BuhinEditBaseVo.ShisakuKataHi) Then
                Return False
                'End If
            End If
            '備考が同一か？'
            If BuhinEditBaseVo.Bikou <> BuhinEditVo.Bikou Then
                'If Not StringUtil.IsEmpty(BuhinEditVo.Bikou) Or Not StringUtil.IsEmpty(BuhinEditBaseVo.Bikou) Then
                Return False
                'End If
            End If
            '2012/02/23 供給セクションとNOTE追加'
            If BuhinEditBaseVo.KyoukuSection <> BuhinEditVo.KyoukuSection Then
                'If Not StringUtil.IsEmpty(BuhinEditVo.KyoukuSection) Or Not StringUtil.IsEmpty(BuhinEditBaseVo.KyoukuSection) Then
                Return False
                'End If
            End If
            If BuhinEditBaseVo.BuhinNote <> BuhinEditVo.BuhinNote Then
                'If Not StringUtil.IsEmpty(BuhinEditVo.BuhinNote) Or Not StringUtil.IsEmpty(BuhinEditBaseVo.BuhinNote) Then
                Return False
                'End If
            End If

            Return True
        End Function

        ''' <summary>
        ''' 部品番号がずれた可能性があるので存在チェックを念入りにする
        ''' </summary>
        ''' <param name="BuhinEditVo">今回部品編集情報</param>
        ''' <param name="bBuhinEditVoList">前回部品編集情報</param>
        ''' <returns>該当する前回部品編集情報なければNothing</returns>
        ''' <remarks></remarks>
        Private Function FindByBuhinEditBaseVo(ByVal BuhinEditVo As TShisakuBuhinEditVo, ByVal bBuhinEditVoList As List(Of TShisakuBuhinEditBaseVo)) As TShisakuBuhinEditBaseVo

            Dim buhinHyoujiJun As Integer = 0
            Dim konkaiBuhinNoHyoujiJun As Integer = 0
            Dim result As New TShisakuBuhinEditBaseVo

            '部品番号表示順がずれただけなのか検索する'
            For index As Integer = 0 To bBuhinEditVoList.Count - 1
                '部品番号は該当しているのでその他の該当が該当するかチェック'
                If AllCheck(bBuhinEditVoList(index), BuhinEditVo) Then
                    '全件チェックで該当するなら部品番号表示順のズレが最も少ないものをあたりとする'

                    '差を絶対値で取得'
                    Dim i As Integer = BuhinEditVo.BuhinNoHyoujiJun - bBuhinEditVoList(index).BuhinNoHyoujiJun
                    buhinHyoujiJun = Math.Abs(i)
                    If index = 0 Then
                        konkaiBuhinNoHyoujiJun = buhinHyoujiJun
                        result = bBuhinEditVoList(index)
                    Else
                        '差が小さいほうが正解'
                        If konkaiBuhinNoHyoujiJun > buhinHyoujiJun Then
                            result = bBuhinEditVoList(index)
                        End If
                    End If
                End If
            Next
            Return result
        End Function

        ''' <summary>
        ''' SQL文の作成
        ''' </summary>
        ''' <param name="BuhinEditVo">部品編集情報</param>
        ''' <param name="Flag">変更無し(更新):0、変更無し(追加):1、追加品番(更新):2、追加品番(追加):3、員数増(更新):4、員数増(追加):5、員数減(更新):6、員数減(追加):7、員数増減(ベース情報追加):8</param>
        ''' <remarks></remarks>
        Private Sub SqlCreateBuhinEdit(ByVal BuhinEditVo As TShisakuBuhinEditVo, ByVal Flag As Integer)
            Dim sql As New System.Text.StringBuilder
            Dim aDate As New ShisakuDate
            Dim CreatedUserId As String = LoginInfo.Now.UserId
            Dim CreatedUserDate As String = aDate.CurrentDateDbFormat
            Dim CreatedUserTime As String = aDate.CurrentTimeDbFormat
            Dim UpdatedUserId As String = LoginInfo.Now.UserId
            Dim UpdatedDate As String = aDate.CurrentDateDbFormat
            Dim UpdatedTime As String = aDate.CurrentTimeDbFormat
            Dim SenyouMark As String

            '専用マークをつける'
            If Not SakuseiImpl.FindBySenyouCheck(BuhinEditVo.BuhinNo, _seihinKbn) Then
                SenyouMark = "*"
            Else
                SenyouMark = ""
            End If

            '員数増の追加は部品番号表示順を増やすので'
            'Dim buhinNoHyoujiJun As Integer = 0
            'If Flag = 5 Then
            '    buhinNoHyoujiJun = HikakuImpl.FindByNewBuhinNoHyoujiJun(BuhinEditVo.ShisakuEventCode, BuhinEditVo.ShisakuBukaCode, BuhinEditVo.ShisakuBlockNo)
            'End If

            Using insert As New SqlAccess(g_kanrihyoIni(DB_KEY_EBOM))
                insert.Open()
                insert.BeginTransaction()

                '変更無し(更新)'
                Select Case Flag
                    Case 0
                        '変更無し(更新)'
                        With sql
                            .AppendLine("UPDATE " & MBOM_DB_NAME & ".dbo.T_YOSAN_SETTEI_BUHIN_TMP ")
                            .AppendLine("SET ")
                            .AppendLine("  TEHAI_KIGOU = 'F' ")
                            .AppendLine("  , UPDATED_USER_ID = @UpdatedUserId ")
                            .AppendLine("  , UPDATED_DATE = @UpdatedDate ")
                            .AppendLine("  , UPDATED_TIME = @UpdatedTime ")
                            .AppendLine("WHERE ")
                            .AppendLine("  SHISAKU_EVENT_CODE = @ShisakuEventCode ")
                            .AppendLine("  AND SHISAKU_LIST_CODE = @ShisakuListCode ")
                            .AppendLine("  AND SHISAKU_BUKA_CODE = @ShisakuBukaCode ")
                            .AppendLine("  AND SHISAKU_BLOCK_NO = @ShisakuBlockNo ")
                            .AppendLine("  AND SHISAKU_BLOCK_NO_KAITEI_NO = @ShisakuBlockNoKaiteiNo ")
                            .AppendLine("  AND BUHIN_NO_HYOUJI_JUN = @BuhinNoHyoujiJun ")
                            .AppendLine("  AND GYOU_ID = '000' ")
                        End With

                        With insert
                            .ClearParameters()
                            .AddParameter("@UpdatedUserId", UpdatedUserId)
                            .AddParameter("@UpdatedDate", UpdatedDate)
                            .AddParameter("@UpdatedTime", UpdatedTime)

                            .AddParameter("@ShisakuEventCode", BuhinEditVo.ShisakuEventCode)
                            .AddParameter("@ShisakuListCode", _shisakuListCode)
                            .AddParameter("@ShisakuBukaCode", BuhinEditVo.ShisakuBukaCode)
                            .AddParameter("@ShisakuBlockNo", BuhinEditVo.ShisakuBlockNo)
                            .AddParameter("@ShisakuBlockNoKaiteiNo", BuhinEditVo.ShisakuBlockNoKaiteiNo)
                            .AddParameter("@BuhinNoHyoujiJun", BuhinEditVo.BuhinNoHyoujiJun)
                        End With

                    Case 1
                        '変更無し(追加)'
                        With sql
                            .AppendLine("INSERT ")
                            .AppendLine("INTO " & MBOM_DB_NAME & ".dbo.T_YOSAN_SETTEI_BUHIN_TMP( ")
                            .AppendLine("  SHISAKU_EVENT_CODE ")
                            .AppendLine("  , SHISAKU_BUKA_CODE ")
                            .AppendLine("  , SHISAKU_BLOCK_NO ")
                            .AppendLine("  , SHISAKU_BLOCK_NO_KAITEI_NO ")
                            .AppendLine("  , BUHIN_NO_HYOUJI_JUN ")
                            .AppendLine("  , GYOU_ID ")
                            .AppendLine("  , LEVEL ")
                            .AppendLine("  , SHUKEI_CODE ")
                            .AppendLine("  , SIA_SHUKEI_CODE ")
                            .AppendLine("  , GENCYO_CKD_KBN ")
                            .AppendLine("  , MAKER_CODE ")
                            .AppendLine("  , MAKER_NAME ")
                            .AppendLine("  , BUHIN_NO ")
                            .AppendLine("  , BUHIN_NO_KBN ")
                            .AppendLine("  , BUHIN_NO_KAITEI_NO ")
                            .AppendLine("  , EDA_BAN ")
                            .AppendLine("  , BUHIN_NAME ")
                            .AppendLine("  , SAISHIYOUFUKA ")
                            .AppendLine("  , SHUTUZU_YOTEI_DATE ")
                            .AppendLine("  , TSUKURIKATA_SEISAKU ")
                            .AppendLine("  , TSUKURIKATA_KATASHIYOU_1 ")
                            .AppendLine("  , TSUKURIKATA_KATASHIYOU_2 ")
                            .AppendLine("  , TSUKURIKATA_KATASHIYOU_3 ")
                            .AppendLine("  , TSUKURIKATA_TIGU ")
                            .AppendLine("  , TSUKURIKATA_NOUNYU ")
                            .AppendLine("  , TSUKURIKATA_KIBO ")
                            .AppendLine("  , BASE_BUHIN_FLG ")
                            .AppendLine("  , ZAISHITU_KIKAKU_1 ")
                            .AppendLine("  , ZAISHITU_KIKAKU_2 ")
                            .AppendLine("  , ZAISHITU_KIKAKU_3 ")
                            .AppendLine("  , ZAISHITU_MEKKI ")
                            .AppendLine("  , SHISAKU_BANKO_SURYO ")
                            .AppendLine("  , SHISAKU_BANKO_SURYO_U ")
                            '.AppendLine("  , MATERIAL_INFO_LENGTH ")
                            '.AppendLine("  , MATERIAL_INFO_WIDTH ")
                            '.AppendLine("  , DATA_ITEM_KAITEI_NO ")
                            '.AppendLine("  , DATA_ITEM_AREA_NAME ")
                            '.AppendLine("  , DATA_ITEM_SET_NAME ")
                            '.AppendLine("  , DATA_ITEM_KAITEI_INFO ")
                            .AppendLine("  , SHISAKU_BUHIN_HI ")
                            .AppendLine("  , SHISAKU_KATA_HI ")
                            .AppendLine("  , BIKOU ")
                            .AppendLine("  , EDIT_TOUROKUBI ")
                            .AppendLine("  , EDIT_TOUROKUJIKAN ")
                            .AppendLine("  , KAITEI_HANDAN_FLG ")
                            .AppendLine("  , TEHAI_KIGOU ")
                            .AppendLine("  , NOUBA ")
                            .AppendLine("  , KYOUKU_SECTION ")
                            .AppendLine("  , SENYOU_MARK ")
                            .AppendLine("  , KOUTAN ")
                            .AppendLine("  , STSR_DHSTBA ")
                            .AppendLine("  , HENKATEN ")
                            .AppendLine("  , SHISAKU_SEIHIN_KBN ")
                            .AppendLine("  , SHISAKU_LIST_CODE ")
                            .AppendLine("  , BUHIN_NOTE ")
                            .AppendLine("  , CREATED_USER_ID ")
                            .AppendLine("  , CREATED_DATE ")
                            .AppendLine("  , CREATED_TIME ")
                            .AppendLine("  , UPDATED_USER_ID ")
                            .AppendLine("  , UPDATED_DATE ")
                            .AppendLine("  , UPDATED_TIME ")
                            .AppendLine(") ")
                            .AppendLine("VALUES ( ")
                            .AppendLine(" @ShisakuEventCode")
                            .AppendLine(",@ShisakuBukaCode")
                            .AppendLine(",@ShisakuBlockNo")
                            .AppendLine(",@ShisakuBlockNoKaiteiNo")
                            .AppendLine(",@BuhinNoHyoujiJun")
                            .AppendLine(",'000'")
                            .AppendLine(",@Level")
                            .AppendLine(",@ShukeiCode")
                            .AppendLine(",@SiaShukeiCode")
                            .AppendLine(",@GencyoCkdKbn")
                            .AppendLine(",@MakerCode")
                            .AppendLine(",@MakerName")
                            .AppendLine(",@BuhinNo")
                            .AppendLine(",@BuhinNoKbn")
                            .AppendLine(",@BuhinNoKaiteiNo")
                            .AppendLine(",@EdaBan")
                            .AppendLine(",@BuhinName")
                            .AppendLine(",@Saishiyoufuka")
                            .AppendLine(",@ShutuzuYoteiDate")
                            .AppendLine(",@TsukurikataSeisaku")
                            .AppendLine(",@TsukurikataKatashiyou1")
                            .AppendLine(",@TsukurikataKatashiyou2")
                            .AppendLine(",@TsukurikataKatashiyou3")
                            .AppendLine(",@TsukurikataTigu")
                            .AppendLine(",@TsukurikataNounyu")
                            .AppendLine(",@TsukurikataKibo")
                            .AppendLine(",@BaseBuhinFlg")
                            .AppendLine(",@ZaishituKikaku1")
                            .AppendLine(",@ZaishituKikaku2")
                            .AppendLine(",@ZaishituKikaku3")
                            .AppendLine(",@ZaishituMekki")
                            .AppendLine(",@ShisakuBankoSuryo")
                            .AppendLine(",@ShisakuBankoSuryoU")
                            '.AppendLine(",@MaterialInfoLength")
                            '.AppendLine(",@MaterialInfoWidth")
                            '.AppendLine(",@DataItemKaiteiNo")
                            '.AppendLine(",@DataItemAreaName")
                            '.AppendLine(",@DataItemSetName")
                            '.AppendLine(",@DataItemKaiteiInfo")
                            .AppendLine(",@ShisakuBuhinHi")
                            .AppendLine(",@ShisakuKataHi")
                            .AppendLine(",@Bikou")
                            .AppendLine(",@EditTourokubi")
                            .AppendLine(",@EditTourokujikan")
                            .AppendLine(",@KaiteiHandanFlg")
                            .AppendLine(",'F'")
                            .AppendLine(",''")
                            .AppendLine(",@KyoukuSection")
                            .AppendLine(",@SenyouMark")
                            .AppendLine(",''")
                            .AppendLine(",''")
                            .AppendLine(",''")
                            .AppendLine(",@SeihinKbn")
                            .AppendLine(",@ShisakuListCode")
                            .AppendLine(",@BuhinNote")
                            .AppendLine(",@CreatedUserId")
                            .AppendLine(",@CreatedUserDate")
                            .AppendLine(",@CreatedUserTime")
                            .AppendLine(",@UpdatedUserId")
                            .AppendLine(",@UpdatedDate")
                            .AppendLine(",@UpdatedTime")
                            .AppendLine(" )")
                        End With

                        With insert
                            .ClearParameters()
                            .AddParameter("@ShisakuEventCode", BuhinEditVo.ShisakuEventCode)
                            .AddParameter("@ShisakuBukaCode", BuhinEditVo.ShisakuBukaCode)
                            .AddParameter("@ShisakuBlockNo", BuhinEditVo.ShisakuBlockNo)
                            .AddParameter("@ShisakuBlockNoKaiteiNo", BuhinEditVo.ShisakuBlockNoKaiteiNo)
                            .AddParameter("@BuhinNoHyoujiJun", BuhinEditVo.BuhinNoHyoujiJun)
                            .AddParameter("@Level", BuhinEditVo.Level)
                            .AddParameter("@ShukeiCode", BuhinEditVo.ShukeiCode)
                            .AddParameter("@SiaShukeiCode", BuhinEditVo.SiaShukeiCode)
                            .AddParameter("@GencyoCkdKbn", BuhinEditVo.GencyoCkdKbn)
                            .AddParameter("@MakerCode", BuhinEditVo.MakerCode)
                            .AddParameter("@MakerName", BuhinEditVo.MakerName)
                            .AddParameter("@BuhinNo", BuhinEditVo.BuhinNo)
                            .AddParameter("@BuhinNoKbn", BuhinEditVo.BuhinNoKbn)
                            .AddParameter("@BuhinNoKaiteiNo", BuhinEditVo.BuhinNoKaiteiNo)
                            .AddParameter("@EdaBan", BuhinEditVo.EdaBan)
                            .AddParameter("@BuhinName", BuhinEditVo.BuhinName)
                            .AddParameter("@Saishiyoufuka", BuhinEditVo.Saishiyoufuka)
                            .AddParameter("@ShutuzuYoteiDate", BuhinEditVo.ShutuzuYoteiDate)
                            .AddParameter("@TsukurikataSeisaku", BuhinEditVo.TsukurikataSeisaku)
                            .AddParameter("@TsukurikataKatashiyou1", BuhinEditVo.TsukurikataKatashiyou1)
                            .AddParameter("@TsukurikataKatashiyou2", BuhinEditVo.TsukurikataKatashiyou2)
                            .AddParameter("@TsukurikataKatashiyou3", BuhinEditVo.TsukurikataKatashiyou3)
                            .AddParameter("@TsukurikataTigu", BuhinEditVo.TsukurikataTigu)
                            .AddParameter("@TsukurikataNounyu", BuhinEditVo.TsukurikataNounyu)
                            .AddParameter("@TsukurikataKibo", BuhinEditVo.TsukurikataKibo)
                            .AddParameter("@BaseBuhinFlg", BuhinEditVo.BaseBuhinFlg)
                            .AddParameter("@ZaishituKikaku1", BuhinEditVo.ZaishituKikaku1)
                            .AddParameter("@ZaishituKikaku2", BuhinEditVo.ZaishituKikaku2)
                            .AddParameter("@ZaishituKikaku3", BuhinEditVo.ZaishituKikaku3)
                            .AddParameter("@ZaishituMekki", BuhinEditVo.ZaishituMekki)
                            .AddParameter("@ShisakuBankoSuryo", BuhinEditVo.ShisakuBankoSuryo)
                            .AddParameter("@ShisakuBankoSuryoU", BuhinEditVo.ShisakuBankoSuryoU)
                            '.AddParameter("@MaterialInfoLength", BuhinEditVo.MaterialInfoLength)
                            '.AddParameter("@MaterialInfoWidth", BuhinEditVo.MaterialInfoWidth)
                            '.AddParameter("@DataItemKaiteiNo", BuhinEditVo.DataItemKaiteiNo)
                            '.AddParameter("@DataItemAreaName", BuhinEditVo.DataItemAreaName)
                            '.AddParameter("@DataItemSetName", BuhinEditVo.DataItemSetName)
                            '.AddParameter("@DataItemKaiteiInfo", BuhinEditVo.DataItemKaiteiInfo)
                            .AddParameter("@ShisakuBuhinHi", BuhinEditVo.ShisakuBuhinHi)
                            .AddParameter("@ShisakuKataHi", BuhinEditVo.ShisakuKataHi)
                            .AddParameter("@Bikou", BuhinEditVo.Bikou)
                            .AddParameter("@EditTourokubi", BuhinEditVo.EditTourokubi)
                            .AddParameter("@EditTourokujikan", BuhinEditVo.EditTourokujikan)
                            .AddParameter("@KaiteiHandanFlg", BuhinEditVo.KaiteiHandanFlg)
                            .AddParameter("@KyoukuSection", BuhinEditVo.KyoukuSection)
                            .AddParameter("@SenyouMark", SenyouMark)
                            .AddParameter("@SeihinKbn", _seihinKbn)
                            .AddParameter("@ShisakuListCode", _shisakuListCode)
                            .AddParameter("@BuhinNote", BuhinEditVo.BuhinNote)
                            .AddParameter("@CreatedUserId", CreatedUserId)
                            .AddParameter("@CreatedUserDate", CreatedUserDate)
                            .AddParameter("@CreatedUserTime", CreatedUserTime)
                            .AddParameter("@UpdatedUserId", UpdatedUserId)
                            .AddParameter("@UpdatedDate", UpdatedDate)
                            .AddParameter("@UpdatedTime", UpdatedTime)
                        End With
                    Case 2
                        '追加品番(更新)'
                        With sql
                            .AppendLine("UPDATE " & MBOM_DB_NAME & ".dbo.T_YOSAN_SETTEI_BUHIN_TMP ")
                            .AppendLine("SET ")
                            .AppendLine("  HENKATEN = '1' ")
                            .AppendLine("  , UPDATED_USER_ID = @UpdatedUserId ")
                            .AppendLine("  , UPDATED_DATE = @UpdatedDate ")
                            .AppendLine("  , UPDATED_TIME = @UpdatedTime ")
                            .AppendLine("WHERE ")
                            .AppendLine("  SHISAKU_EVENT_CODE = @ShisakuEventCode ")
                            .AppendLine("  AND SHISAKU_LIST_CODE = @ShisakuListCode ")
                            .AppendLine("  AND SHISAKU_BUKA_CODE = @ShisakuBukaCode ")
                            .AppendLine("  AND SHISAKU_BLOCK_NO = @ShisakuBlockNo ")
                            .AppendLine("  AND SHISAKU_BLOCK_NO_KAITEI_NO = @ShisakuBlockNoKaiteiNo ")
                            .AppendLine("  AND BUHIN_NO_HYOUJI_JUN = @BuhinNoHyoujiJun ")
                            .AppendLine("  AND GYOU_ID = '000' ")
                        End With

                        With insert
                            .ClearParameters()
                            .AddParameter("@UpdatedUserId", UpdatedUserId)
                            .AddParameter("@UpdatedDate", UpdatedDate)
                            .AddParameter("@UpdatedTime", UpdatedTime)

                            .AddParameter("@ShisakuEventCode", BuhinEditVo.ShisakuEventCode)
                            .AddParameter("@ShisakuListCode", _shisakuListCode)
                            .AddParameter("@ShisakuBukaCode", BuhinEditVo.ShisakuBukaCode)
                            .AddParameter("@ShisakuBlockNo", BuhinEditVo.ShisakuBlockNo)
                            .AddParameter("@ShisakuBlockNoKaiteiNo", BuhinEditVo.ShisakuBlockNoKaiteiNo)
                            .AddParameter("@BuhinNoHyoujiJun", BuhinEditVo.BuhinNoHyoujiJun)
                        End With
                    Case 3
                        '追加品番(追加)'
                        With sql
                            .AppendLine("INSERT ")
                            .AppendLine("INTO " & MBOM_DB_NAME & ".dbo.T_YOSAN_SETTEI_BUHIN_TMP( ")
                            .AppendLine("  SHISAKU_EVENT_CODE ")
                            .AppendLine("  , SHISAKU_BUKA_CODE ")
                            .AppendLine("  , SHISAKU_BLOCK_NO ")
                            .AppendLine("  , SHISAKU_BLOCK_NO_KAITEI_NO ")
                            .AppendLine("  , BUHIN_NO_HYOUJI_JUN ")
                            .AppendLine("  , GYOU_ID ")
                            .AppendLine("  , LEVEL ")
                            .AppendLine("  , SHUKEI_CODE ")
                            .AppendLine("  , SIA_SHUKEI_CODE ")
                            .AppendLine("  , GENCYO_CKD_KBN ")
                            .AppendLine("  , MAKER_CODE ")
                            .AppendLine("  , MAKER_NAME ")
                            .AppendLine("  , BUHIN_NO ")
                            .AppendLine("  , BUHIN_NO_KBN ")
                            .AppendLine("  , BUHIN_NO_KAITEI_NO ")
                            .AppendLine("  , EDA_BAN ")
                            .AppendLine("  , BUHIN_NAME ")
                            .AppendLine("  , SAISHIYOUFUKA ")
                            .AppendLine("  , SHUTUZU_YOTEI_DATE ")
                            .AppendLine("  , TSUKURIKATA_SEISAKU ")
                            .AppendLine("  , TSUKURIKATA_KATASHIYOU_1 ")
                            .AppendLine("  , TSUKURIKATA_KATASHIYOU_2 ")
                            .AppendLine("  , TSUKURIKATA_KATASHIYOU_3 ")
                            .AppendLine("  , TSUKURIKATA_TIGU ")
                            .AppendLine("  , TSUKURIKATA_NOUNYU ")
                            .AppendLine("  , TSUKURIKATA_KIBO ")
                            .AppendLine("  , BASE_BUHIN_FLG ")
                            .AppendLine("  , ZAISHITU_KIKAKU_1 ")
                            .AppendLine("  , ZAISHITU_KIKAKU_2 ")
                            .AppendLine("  , ZAISHITU_KIKAKU_3 ")
                            .AppendLine("  , ZAISHITU_MEKKI ")
                            .AppendLine("  , SHISAKU_BANKO_SURYO ")
                            .AppendLine("  , SHISAKU_BANKO_SURYO_U ")
                            '.AppendLine("  , MATERIAL_INFO_LENGTH ")
                            '.AppendLine("  , MATERIAL_INFO_WIDTH ")
                            '.AppendLine("  , DATA_ITEM_KAITEI_NO ")
                            '.AppendLine("  , DATA_ITEM_AREA_NAME ")
                            '.AppendLine("  , DATA_ITEM_SET_NAME ")
                            '.AppendLine("  , DATA_ITEM_KAITEI_INFO ")
                            .AppendLine("  , SHISAKU_BUHIN_HI ")
                            .AppendLine("  , SHISAKU_KATA_HI ")
                            .AppendLine("  , BIKOU ")
                            .AppendLine("  , EDIT_TOUROKUBI ")
                            .AppendLine("  , EDIT_TOUROKUJIKAN ")
                            .AppendLine("  , KAITEI_HANDAN_FLG ")
                            .AppendLine("  , TEHAI_KIGOU ")
                            .AppendLine("  , NOUBA ")
                            .AppendLine("  , KYOUKU_SECTION ")
                            .AppendLine("  , SENYOU_MARK ")
                            .AppendLine("  , KOUTAN ")
                            .AppendLine("  , STSR_DHSTBA ")
                            .AppendLine("  , HENKATEN ")
                            .AppendLine("  , SHISAKU_SEIHIN_KBN ")
                            .AppendLine("  , SHISAKU_LIST_CODE ")
                            .AppendLine("  , BUHIN_NOTE ")
                            .AppendLine("  , CREATED_USER_ID ")
                            .AppendLine("  , CREATED_DATE ")
                            .AppendLine("  , CREATED_TIME ")
                            .AppendLine("  , UPDATED_USER_ID ")
                            .AppendLine("  , UPDATED_DATE ")
                            .AppendLine("  , UPDATED_TIME ")
                            .AppendLine(") ")
                            .AppendLine("VALUES ( ")
                            .AppendLine(" @ShisakuEventCode")
                            .AppendLine(",@ShisakuBukaCode")
                            .AppendLine(",@ShisakuBlockNo")
                            .AppendLine(",@ShisakuBlockNoKaiteiNo")
                            .AppendLine(",@BuhinNoHyoujiJun")
                            .AppendLine(",'000'")
                            .AppendLine(",@Level")
                            .AppendLine(",@ShukeiCode")
                            .AppendLine(",@SiaShukeiCode")
                            .AppendLine(",@GencyoCkdKbn")
                            .AppendLine(",@MakerCode")
                            .AppendLine(",@MakerName")
                            .AppendLine(",@BuhinNo")
                            .AppendLine(",@BuhinNoKbn")
                            .AppendLine(",@BuhinNoKaiteiNo")
                            .AppendLine(",@EdaBan")
                            .AppendLine(",@BuhinName")
                            .AppendLine(",@Saishiyoufuka")
                            .AppendLine(",@ShutuzuYoteiDate")
                            .AppendLine(",@TsukurikataSeisaku")
                            .AppendLine(",@TsukurikataKatashiyou1")
                            .AppendLine(",@TsukurikataKatashiyou2")
                            .AppendLine(",@TsukurikataKatashiyou3")
                            .AppendLine(",@TsukurikataTigu")
                            .AppendLine(",@TsukurikataNounyu")
                            .AppendLine(",@TsukurikataKibo")
                            .AppendLine(",@BaseBuhinFlg")
                            .AppendLine(",@ZaishituKikaku1")
                            .AppendLine(",@ZaishituKikaku2")
                            .AppendLine(",@ZaishituKikaku3")
                            .AppendLine(",@ZaishituMekki")
                            .AppendLine(",@ShisakuBankoSuryo")
                            .AppendLine(",@ShisakuBankoSuryoU")
                            '.AppendLine(",@MaterialInfoLength")
                            '.AppendLine(",@MaterialInfoWidth")
                            '.AppendLine(",@DataItemKaiteiNo")
                            '.AppendLine(",@DataItemAreaName")
                            '.AppendLine(",@DataItemSetName")
                            '.AppendLine(",@DataItemKaiteiInfo")
                            .AppendLine(",@ShisakuBuhinHi")
                            .AppendLine(",@ShisakuKataHi")
                            .AppendLine(",@Bikou")
                            .AppendLine(",@EditTourokubi")
                            .AppendLine(",@EditTourokujikan")
                            .AppendLine(",@KaiteiHandanFlg")
                            .AppendLine(",''")
                            .AppendLine(",''")
                            .AppendLine(",@KyoukuSection")
                            .AppendLine(",@SenyouMark")
                            .AppendLine(",''")
                            .AppendLine(",''")
                            .AppendLine(",'1'")
                            .AppendLine(",@SeihinKbn")
                            .AppendLine(",@ShisakuListCode")
                            .AppendLine(",@BuhinNote")
                            .AppendLine(",@CreatedUserId")
                            .AppendLine(",@CreatedUserDate")
                            .AppendLine(",@CreatedUserTime")
                            .AppendLine(",@UpdatedUserId")
                            .AppendLine(",@UpdatedDate")
                            .AppendLine(",@UpdatedTime")
                            .AppendLine(" )")
                        End With
                        With insert
                            .ClearParameters()
                            .AddParameter("@ShisakuEventCode", BuhinEditVo.ShisakuEventCode)
                            .AddParameter("@ShisakuBukaCode", BuhinEditVo.ShisakuBukaCode)
                            .AddParameter("@ShisakuBlockNo", BuhinEditVo.ShisakuBlockNo)
                            .AddParameter("@ShisakuBlockNoKaiteiNo", BuhinEditVo.ShisakuBlockNoKaiteiNo)
                            .AddParameter("@BuhinNoHyoujiJun", BuhinEditVo.BuhinNoHyoujiJun)
                            .AddParameter("@Level", BuhinEditVo.Level)
                            .AddParameter("@ShukeiCode", BuhinEditVo.ShukeiCode)
                            .AddParameter("@SiaShukeiCode", BuhinEditVo.SiaShukeiCode)
                            .AddParameter("@GencyoCkdKbn", BuhinEditVo.GencyoCkdKbn)
                            .AddParameter("@MakerCode", BuhinEditVo.MakerCode)
                            .AddParameter("@MakerName", BuhinEditVo.MakerName)
                            .AddParameter("@BuhinNo", BuhinEditVo.BuhinNo)
                            .AddParameter("@BuhinNoKbn", BuhinEditVo.BuhinNoKbn)
                            .AddParameter("@BuhinNoKaiteiNo", BuhinEditVo.BuhinNoKaiteiNo)
                            .AddParameter("@EdaBan", BuhinEditVo.EdaBan)
                            .AddParameter("@BuhinName", BuhinEditVo.BuhinName)
                            .AddParameter("@Saishiyoufuka", BuhinEditVo.Saishiyoufuka)
                            .AddParameter("@ShutuzuYoteiDate", BuhinEditVo.ShutuzuYoteiDate)
                            .AddParameter("@TsukurikataSeisaku", BuhinEditVo.TsukurikataSeisaku)
                            .AddParameter("@TsukurikataKatashiyou1", BuhinEditVo.TsukurikataKatashiyou1)
                            .AddParameter("@TsukurikataKatashiyou2", BuhinEditVo.TsukurikataKatashiyou2)
                            .AddParameter("@TsukurikataKatashiyou3", BuhinEditVo.TsukurikataKatashiyou3)
                            .AddParameter("@TsukurikataTigu", BuhinEditVo.TsukurikataTigu)
                            .AddParameter("@TsukurikataNounyu", BuhinEditVo.TsukurikataNounyu)
                            .AddParameter("@TsukurikataKibo", BuhinEditVo.TsukurikataKibo)
                            .AddParameter("@BaseBuhinFlg", BuhinEditVo.BaseBuhinFlg)
                            .AddParameter("@ZaishituKikaku1", BuhinEditVo.ZaishituKikaku1)
                            .AddParameter("@ZaishituKikaku2", BuhinEditVo.ZaishituKikaku2)
                            .AddParameter("@ZaishituKikaku3", BuhinEditVo.ZaishituKikaku3)
                            .AddParameter("@ZaishituMekki", BuhinEditVo.ZaishituMekki)
                            .AddParameter("@ShisakuBankoSuryo", BuhinEditVo.ShisakuBankoSuryo)
                            .AddParameter("@ShisakuBankoSuryoU", BuhinEditVo.ShisakuBankoSuryoU)
                            '.AddParameter("@MaterialInfoLength", BuhinEditVo.MaterialInfoLength)
                            '.AddParameter("@MaterialInfoWidth", BuhinEditVo.MaterialInfoWidth)
                            '.AddParameter("@DataItemKaiteiNo", BuhinEditVo.DataItemKaiteiNo)
                            '.AddParameter("@DataItemAreaName", BuhinEditVo.DataItemAreaName)
                            '.AddParameter("@DataItemSetName", BuhinEditVo.DataItemSetName)
                            '.AddParameter("@DataItemKaiteiInfo", BuhinEditVo.DataItemKaiteiInfo)
                            .AddParameter("@ShisakuBuhinHi", BuhinEditVo.ShisakuBuhinHi)
                            .AddParameter("@ShisakuKataHi", BuhinEditVo.ShisakuKataHi)
                            .AddParameter("@Bikou", BuhinEditVo.Bikou)
                            .AddParameter("@EditTourokubi", BuhinEditVo.EditTourokubi)
                            .AddParameter("@EditTourokujikan", BuhinEditVo.EditTourokujikan)
                            .AddParameter("@KaiteiHandanFlg", BuhinEditVo.KaiteiHandanFlg)
                            .AddParameter("@KyoukuSection", BuhinEditVo.KyoukuSection)
                            .AddParameter("@SenyouMark", SenyouMark)
                            .AddParameter("@SeihinKbn", _seihinKbn)
                            .AddParameter("@ShisakuListCode", _shisakuListCode)
                            .AddParameter("@BuhinNote", BuhinEditVo.BuhinNote)
                            .AddParameter("@CreatedUserId", CreatedUserId)
                            .AddParameter("@CreatedUserDate", CreatedUserDate)
                            .AddParameter("@CreatedUserTime", CreatedUserTime)
                            .AddParameter("@UpdatedUserId", UpdatedUserId)
                            .AddParameter("@UpdatedDate", UpdatedDate)
                            .AddParameter("@UpdatedTime", UpdatedTime)
                        End With

                    Case 4
                        '員数増(更新)'
                        With sql
                            .AppendLine("UPDATE " & MBOM_DB_NAME & ".dbo.T_YOSAN_SETTEI_BUHIN_TMP ")
                            .AppendLine("SET ")
                            .AppendLine("  HENKATEN = '1' ")
                            .AppendLine("  , GYOU_ID = '001' ")
                            .AppendLine("  , UPDATED_USER_ID = @UpdatedUserId ")
                            .AppendLine("  , UPDATED_DATE = @UpdatedDate ")
                            .AppendLine("  , UPDATED_TIME = @UpdatedTime ")
                            .AppendLine("WHERE ")
                            .AppendLine("  SHISAKU_EVENT_CODE = @ShisakuEventCode ")
                            .AppendLine("  AND SHISAKU_LIST_CODE = @ShisakuListCode ")
                            .AppendLine("  AND SHISAKU_BUKA_CODE = @ShisakuBukaCode ")
                            .AppendLine("  AND SHISAKU_BLOCK_NO = @ShisakuBlockNo ")
                            .AppendLine("  AND SHISAKU_BLOCK_NO_KAITEI_NO = @ShisakuBlockNoKaiteiNo ")
                            .AppendLine("  AND BUHIN_NO_HYOUJI_JUN = @BuhinNoHyoujiJun ")
                            .AppendLine("  AND GYOU_ID = '000' ")
                        End With

                        With insert
                            .ClearParameters()
                            .AddParameter("@UpdatedUserId", UpdatedUserId)
                            .AddParameter("@UpdatedDate", UpdatedDate)
                            .AddParameter("@UpdatedTime", UpdatedTime)

                            .AddParameter("@ShisakuEventCode", BuhinEditVo.ShisakuEventCode)
                            .AddParameter("@ShisakuListCode", _shisakuListCode)
                            .AddParameter("@ShisakuBukaCode", BuhinEditVo.ShisakuBukaCode)
                            .AddParameter("@ShisakuBlockNo", BuhinEditVo.ShisakuBlockNo)
                            .AddParameter("@ShisakuBlockNoKaiteiNo", BuhinEditVo.ShisakuBlockNoKaiteiNo)
                            .AddParameter("@BuhinNoHyoujiJun", BuhinEditVo.BuhinNoHyoujiJun)
                        End With

                    Case 5
                        '員数増(追加)'
                        With sql
                            .AppendLine("INSERT ")
                            .AppendLine("INTO " & MBOM_DB_NAME & ".dbo.T_YOSAN_SETTEI_BUHIN_TMP( ")
                            .AppendLine("  SHISAKU_EVENT_CODE ")
                            .AppendLine("  , SHISAKU_BUKA_CODE ")
                            .AppendLine("  , SHISAKU_BLOCK_NO ")
                            .AppendLine("  , SHISAKU_BLOCK_NO_KAITEI_NO ")
                            .AppendLine("  , BUHIN_NO_HYOUJI_JUN ")
                            .AppendLine("  , GYOU_ID ")
                            .AppendLine("  , LEVEL ")
                            .AppendLine("  , SHUKEI_CODE ")
                            .AppendLine("  , SIA_SHUKEI_CODE ")
                            .AppendLine("  , GENCYO_CKD_KBN ")
                            .AppendLine("  , MAKER_CODE ")
                            .AppendLine("  , MAKER_NAME ")
                            .AppendLine("  , BUHIN_NO ")
                            .AppendLine("  , BUHIN_NO_KBN ")
                            .AppendLine("  , BUHIN_NO_KAITEI_NO ")
                            .AppendLine("  , EDA_BAN ")
                            .AppendLine("  , BUHIN_NAME ")
                            .AppendLine("  , SAISHIYOUFUKA ")
                            .AppendLine("  , SHUTUZU_YOTEI_DATE ")
                            .AppendLine("  , TSUKURIKATA_SEISAKU ")
                            .AppendLine("  , TSUKURIKATA_KATASHIYOU_1 ")
                            .AppendLine("  , TSUKURIKATA_KATASHIYOU_2 ")
                            .AppendLine("  , TSUKURIKATA_KATASHIYOU_3 ")
                            .AppendLine("  , TSUKURIKATA_TIGU ")
                            .AppendLine("  , TSUKURIKATA_NOUNYU ")
                            .AppendLine("  , TSUKURIKATA_KIBO ")
                            .AppendLine("  , BASE_BUHIN_FLG ")
                            .AppendLine("  , ZAISHITU_KIKAKU_1 ")
                            .AppendLine("  , ZAISHITU_KIKAKU_2 ")
                            .AppendLine("  , ZAISHITU_KIKAKU_3 ")
                            .AppendLine("  , ZAISHITU_MEKKI ")
                            .AppendLine("  , SHISAKU_BANKO_SURYO ")
                            .AppendLine("  , SHISAKU_BANKO_SURYO_U ")
                            '.AppendLine("  , MATERIAL_INFO_LENGTH ")
                            '.AppendLine("  , MATERIAL_INFO_WIDTH ")
                            '.AppendLine("  , DATA_ITEM_KAITEI_NO ")
                            '.AppendLine("  , DATA_ITEM_AREA_NAME ")
                            '.AppendLine("  , DATA_ITEM_SET_NAME ")
                            '.AppendLine("  , DATA_ITEM_KAITEI_INFO ")
                            .AppendLine("  , SHISAKU_BUHIN_HI ")
                            .AppendLine("  , SHISAKU_KATA_HI ")
                            .AppendLine("  , BIKOU ")
                            .AppendLine("  , EDIT_TOUROKUBI ")
                            .AppendLine("  , EDIT_TOUROKUJIKAN ")
                            .AppendLine("  , KAITEI_HANDAN_FLG ")
                            .AppendLine("  , TEHAI_KIGOU ")
                            .AppendLine("  , NOUBA ")
                            .AppendLine("  , KYOUKU_SECTION ")
                            .AppendLine("  , SENYOU_MARK ")
                            .AppendLine("  , KOUTAN ")
                            .AppendLine("  , STSR_DHSTBA ")
                            .AppendLine("  , HENKATEN ")
                            .AppendLine("  , SHISAKU_SEIHIN_KBN ")
                            .AppendLine("  , SHISAKU_LIST_CODE ")
                            .AppendLine("  , BUHIN_NOTE ")
                            .AppendLine("  , CREATED_USER_ID ")
                            .AppendLine("  , CREATED_DATE ")
                            .AppendLine("  , CREATED_TIME ")
                            .AppendLine("  , UPDATED_USER_ID ")
                            .AppendLine("  , UPDATED_DATE ")
                            .AppendLine("  , UPDATED_TIME ")
                            .AppendLine(") ")
                            .AppendLine("VALUES ( ")
                            .AppendLine(" @ShisakuEventCode")
                            .AppendLine(",@ShisakuBukaCode")
                            .AppendLine(",@ShisakuBlockNo")
                            .AppendLine(",@ShisakuBlockNoKaiteiNo")
                            .AppendLine(",@BuhinNoHyoujiJun")
                            .AppendLine(",'001'")
                            .AppendLine(",@Level")
                            .AppendLine(",@ShukeiCode")
                            .AppendLine(",@SiaShukeiCode")
                            .AppendLine(",@GencyoCkdKbn")
                            .AppendLine(",@MakerCode")
                            .AppendLine(",@MakerName")
                            .AppendLine(",@BuhinNo")
                            .AppendLine(",@BuhinNoKbn")
                            .AppendLine(",@BuhinNoKaiteiNo")
                            .AppendLine(",@EdaBan")
                            .AppendLine(",@BuhinName")
                            .AppendLine(",@Saishiyoufuka")
                            .AppendLine(",@ShutuzuYoteiDate")
                            .AppendLine(",@TsukurikataSeisaku")
                            .AppendLine(",@TsukurikataKatashiyou1")
                            .AppendLine(",@TsukurikataKatashiyou2")
                            .AppendLine(",@TsukurikataKatashiyou3")
                            .AppendLine(",@TsukurikataTigu")
                            .AppendLine(",@TsukurikataNounyu")
                            .AppendLine(",@TsukurikataKibo")
                            .AppendLine(",@BaseBuhinFlg")
                            .AppendLine(",@ZaishituKikaku1")
                            .AppendLine(",@ZaishituKikaku2")
                            .AppendLine(",@ZaishituKikaku3")
                            .AppendLine(",@ZaishituMekki")
                            .AppendLine(",@ShisakuBankoSuryo")
                            .AppendLine(",@ShisakuBankoSuryoU")
                            '.AppendLine(",@MaterialInfoLength")
                            '.AppendLine(",@MaterialInfoWidth")
                            '.AppendLine(",@DataItemKaiteiNo")
                            '.AppendLine(",@DataItemAreaName")
                            '.AppendLine(",@DataItemSetName")
                            '.AppendLine(",@DataItemKaiteiInfo")
                            .AppendLine(",@ShisakuBuhinHi")
                            .AppendLine(",@ShisakuKataHi")
                            .AppendLine(",@Bikou")
                            .AppendLine(",@EditTourokubi")
                            .AppendLine(",@EditTourokujikan")
                            .AppendLine(",@KaiteiHandanFlg")
                            .AppendLine(",''")
                            .AppendLine(",''")
                            .AppendLine(",@KyoukuSection")
                            .AppendLine(",@SenyouMark")
                            .AppendLine(",''")
                            .AppendLine(",''")
                            .AppendLine(",'1'")
                            .AppendLine(",@SeihinKbn")
                            .AppendLine(",@ShisakuListCode")
                            .AppendLine(",@BuhinNote")
                            .AppendLine(",@CreatedUserId")
                            .AppendLine(",@CreatedUserDate")
                            .AppendLine(",@CreatedUserTime")
                            .AppendLine(",@UpdatedUserId")
                            .AppendLine(",@UpdatedDate")
                            .AppendLine(",@UpdatedTime")
                            .AppendLine(" )")
                        End With
                        With insert
                            .ClearParameters()
                            .AddParameter("@ShisakuEventCode", BuhinEditVo.ShisakuEventCode)
                            .AddParameter("@ShisakuBukaCode", BuhinEditVo.ShisakuBukaCode)
                            .AddParameter("@ShisakuBlockNo", BuhinEditVo.ShisakuBlockNo)
                            .AddParameter("@ShisakuBlockNoKaiteiNo", BuhinEditVo.ShisakuBlockNoKaiteiNo)
                            .AddParameter("@BuhinNoHyoujiJun", BuhinEditVo.BuhinNoHyoujiJun)
                            .AddParameter("@Level", BuhinEditVo.Level)
                            .AddParameter("@ShukeiCode", BuhinEditVo.ShukeiCode)
                            .AddParameter("@SiaShukeiCode", BuhinEditVo.SiaShukeiCode)
                            .AddParameter("@GencyoCkdKbn", BuhinEditVo.GencyoCkdKbn)
                            .AddParameter("@MakerCode", BuhinEditVo.MakerCode)
                            .AddParameter("@MakerName", BuhinEditVo.MakerName)
                            .AddParameter("@BuhinNo", BuhinEditVo.BuhinNo)
                            .AddParameter("@BuhinNoKbn", BuhinEditVo.BuhinNoKbn)
                            .AddParameter("@BuhinNoKaiteiNo", BuhinEditVo.BuhinNoKaiteiNo)
                            .AddParameter("@EdaBan", BuhinEditVo.EdaBan)
                            .AddParameter("@BuhinName", BuhinEditVo.BuhinName)
                            .AddParameter("@Saishiyoufuka", BuhinEditVo.Saishiyoufuka)
                            .AddParameter("@ShutuzuYoteiDate", BuhinEditVo.ShutuzuYoteiDate)
                            .AddParameter("@TsukurikataSeisaku", BuhinEditVo.TsukurikataSeisaku)
                            .AddParameter("@TsukurikataKatashiyou1", BuhinEditVo.TsukurikataKatashiyou1)
                            .AddParameter("@TsukurikataKatashiyou2", BuhinEditVo.TsukurikataKatashiyou2)
                            .AddParameter("@TsukurikataKatashiyou3", BuhinEditVo.TsukurikataKatashiyou3)
                            .AddParameter("@TsukurikataTigu", BuhinEditVo.TsukurikataTigu)
                            .AddParameter("@TsukurikataNounyu", BuhinEditVo.TsukurikataNounyu)
                            .AddParameter("@TsukurikataKibo", BuhinEditVo.TsukurikataKibo)
                            .AddParameter("@BaseBuhinFlg", BuhinEditVo.BaseBuhinFlg)
                            .AddParameter("@ZaishituKikaku1", BuhinEditVo.ZaishituKikaku1)
                            .AddParameter("@ZaishituKikaku2", BuhinEditVo.ZaishituKikaku2)
                            .AddParameter("@ZaishituKikaku3", BuhinEditVo.ZaishituKikaku3)
                            .AddParameter("@ZaishituMekki", BuhinEditVo.ZaishituMekki)
                            .AddParameter("@ShisakuBankoSuryo", BuhinEditVo.ShisakuBankoSuryo)
                            .AddParameter("@ShisakuBankoSuryoU", BuhinEditVo.ShisakuBankoSuryoU)
                            '.AddParameter("@MaterialInfoLength", BuhinEditVo.MaterialInfoLength)
                            '.AddParameter("@MaterialInfoWidth", BuhinEditVo.MaterialInfoWidth)
                            '.AddParameter("@DataItemKaiteiNo", BuhinEditVo.DataItemKaiteiNo)
                            '.AddParameter("@DataItemAreaName", BuhinEditVo.DataItemAreaName)
                            '.AddParameter("@DataItemSetName", BuhinEditVo.DataItemSetName)
                            '.AddParameter("@DataItemKaiteiInfo", BuhinEditVo.DataItemKaiteiInfo)
                            .AddParameter("@ShisakuBuhinHi", BuhinEditVo.ShisakuBuhinHi)
                            .AddParameter("@ShisakuKataHi", BuhinEditVo.ShisakuKataHi)
                            .AddParameter("@Bikou", BuhinEditVo.Bikou)
                            .AddParameter("@EditTourokubi", BuhinEditVo.EditTourokubi)
                            .AddParameter("@EditTourokujikan", BuhinEditVo.EditTourokujikan)
                            .AddParameter("@KaiteiHandanFlg", BuhinEditVo.KaiteiHandanFlg)
                            .AddParameter("@KyoukuSection", BuhinEditVo.KyoukuSection)
                            .AddParameter("@SenyouMark", SenyouMark)
                            .AddParameter("@SeihinKbn", _seihinKbn)
                            .AddParameter("@ShisakuListCode", _shisakuListCode)
                            .AddParameter("@BuhinNote", BuhinEditVo.BuhinNote)
                            .AddParameter("@CreatedUserId", CreatedUserId)
                            .AddParameter("@CreatedUserDate", CreatedUserDate)
                            .AddParameter("@CreatedUserTime", CreatedUserTime)
                            .AddParameter("@UpdatedUserId", UpdatedUserId)
                            .AddParameter("@UpdatedDate", UpdatedDate)
                            .AddParameter("@UpdatedTime", UpdatedTime)
                        End With

                    Case 6
                        '員数減(更新)'
                        With sql
                            .AppendLine("UPDATE " & MBOM_DB_NAME & ".dbo.T_YOSAN_SETTEI_BUHIN_TMP ")
                            .AppendLine("SET ")
                            .AppendLine("  TEHAI_KIGOU = 'F' ")
                            .AppendLine("  , HENKATEN = '1' ")
                            .AppendLine("  , UPDATED_USER_ID = @UpdatedUserId ")
                            .AppendLine("  , UPDATED_DATE = @UpdatedDate ")
                            .AppendLine("  , UPDATED_TIME = @UpdatedTime ")
                            .AppendLine("WHERE ")
                            .AppendLine("  SHISAKU_EVENT_CODE = @ShisakuEventCode ")
                            .AppendLine("  AND SHISAKU_LIST_CODE = @ShisakuListCode ")
                            .AppendLine("  AND SHISAKU_BUKA_CODE = @ShisakuBukaCode ")
                            .AppendLine("  AND SHISAKU_BLOCK_NO = @ShisakuBlockNo ")
                            .AppendLine("  AND SHISAKU_BLOCK_NO_KAITEI_NO = @ShisakuBlockNoKaiteiNo ")
                            .AppendLine("  AND BUHIN_NO_HYOUJI_JUN = @BuhinNoHyoujiJun ")
                            .AppendLine("  AND GYOU_ID = '000' ")
                        End With

                        With insert
                            .ClearParameters()
                            .AddParameter("@UpdatedUserId", UpdatedUserId)
                            .AddParameter("@UpdatedDate", UpdatedDate)
                            .AddParameter("@UpdatedTime", UpdatedTime)

                            .AddParameter("@ShisakuEventCode", BuhinEditVo.ShisakuEventCode)
                            .AddParameter("@ShisakuListCode", _shisakuListCode)
                            .AddParameter("@ShisakuBukaCode", BuhinEditVo.ShisakuBukaCode)
                            .AddParameter("@ShisakuBlockNo", BuhinEditVo.ShisakuBlockNo)
                            .AddParameter("@ShisakuBlockNoKaiteiNo", BuhinEditVo.ShisakuBlockNoKaiteiNo)
                            .AddParameter("@BuhinNoHyoujiJun", BuhinEditVo.BuhinNoHyoujiJun)
                        End With

                    Case 7
                        '員数減(追加)'
                        With sql
                            .AppendLine("INSERT ")
                            .AppendLine("INTO " & MBOM_DB_NAME & ".dbo.T_YOSAN_SETTEI_BUHIN_TMP( ")
                            .AppendLine("  SHISAKU_EVENT_CODE ")
                            .AppendLine("  , SHISAKU_BUKA_CODE ")
                            .AppendLine("  , SHISAKU_BLOCK_NO ")
                            .AppendLine("  , SHISAKU_BLOCK_NO_KAITEI_NO ")
                            .AppendLine("  , BUHIN_NO_HYOUJI_JUN ")
                            .AppendLine("  , GYOU_ID ")
                            .AppendLine("  , LEVEL ")
                            .AppendLine("  , SHUKEI_CODE ")
                            .AppendLine("  , SIA_SHUKEI_CODE ")
                            .AppendLine("  , GENCYO_CKD_KBN ")
                            .AppendLine("  , MAKER_CODE ")
                            .AppendLine("  , MAKER_NAME ")
                            .AppendLine("  , BUHIN_NO ")
                            .AppendLine("  , BUHIN_NO_KBN ")
                            .AppendLine("  , BUHIN_NO_KAITEI_NO ")
                            .AppendLine("  , EDA_BAN ")
                            .AppendLine("  , BUHIN_NAME ")
                            .AppendLine("  , SAISHIYOUFUKA ")
                            .AppendLine("  , SHUTUZU_YOTEI_DATE ")
                            .AppendLine("  , TSUKURIKATA_SEISAKU ")
                            .AppendLine("  , TSUKURIKATA_KATASHIYOU_1 ")
                            .AppendLine("  , TSUKURIKATA_KATASHIYOU_2 ")
                            .AppendLine("  , TSUKURIKATA_KATASHIYOU_3 ")
                            .AppendLine("  , TSUKURIKATA_TIGU ")
                            .AppendLine("  , TSUKURIKATA_NOUNYU ")
                            .AppendLine("  , TSUKURIKATA_KIBO ")
                            .AppendLine("  , BASE_BUHIN_FLG ")
                            .AppendLine("  , ZAISHITU_KIKAKU_1 ")
                            .AppendLine("  , ZAISHITU_KIKAKU_2 ")
                            .AppendLine("  , ZAISHITU_KIKAKU_3 ")
                            .AppendLine("  , ZAISHITU_MEKKI ")
                            .AppendLine("  , SHISAKU_BANKO_SURYO ")
                            .AppendLine("  , SHISAKU_BANKO_SURYO_U ")
                            '.AppendLine("  , MATERIAL_INFO_LENGTH ")
                            '.AppendLine("  , MATERIAL_INFO_WIDTH ")
                            '.AppendLine("  , DATA_ITEM_KAITEI_NO ")
                            '.AppendLine("  , DATA_ITEM_AREA_NAME ")
                            '.AppendLine("  , DATA_ITEM_SET_NAME ")
                            '.AppendLine("  , DATA_ITEM_KAITEI_INFO ")
                            .AppendLine("  , SHISAKU_BUHIN_HI ")
                            .AppendLine("  , SHISAKU_KATA_HI ")
                            .AppendLine("  , BIKOU ")
                            .AppendLine("  , EDIT_TOUROKUBI ")
                            .AppendLine("  , EDIT_TOUROKUJIKAN ")
                            .AppendLine("  , KAITEI_HANDAN_FLG ")
                            .AppendLine("  , TEHAI_KIGOU ")
                            .AppendLine("  , NOUBA ")
                            .AppendLine("  , KYOUKU_SECTION ")
                            .AppendLine("  , SENYOU_MARK ")
                            .AppendLine("  , KOUTAN ")
                            .AppendLine("  , STSR_DHSTBA ")
                            .AppendLine("  , HENKATEN ")
                            .AppendLine("  , SHISAKU_SEIHIN_KBN ")
                            .AppendLine("  , SHISAKU_LIST_CODE ")
                            .AppendLine("  , BUHIN_NOTE ")
                            .AppendLine("  , CREATED_USER_ID ")
                            .AppendLine("  , CREATED_DATE ")
                            .AppendLine("  , CREATED_TIME ")
                            .AppendLine("  , UPDATED_USER_ID ")
                            .AppendLine("  , UPDATED_DATE ")
                            .AppendLine("  , UPDATED_TIME ")
                            .AppendLine(") ")
                            .AppendLine("VALUES ( ")
                            .AppendLine(" @ShisakuEventCode")
                            .AppendLine(",@ShisakuBukaCode")
                            .AppendLine(",@ShisakuBlockNo")
                            .AppendLine(",@ShisakuBlockNoKaiteiNo")
                            .AppendLine(",@BuhinNoHyoujiJun")
                            .AppendLine(",'000'")
                            .AppendLine(",@Level")
                            .AppendLine(",@ShukeiCode")
                            .AppendLine(",@SiaShukeiCode")
                            .AppendLine(",@GencyoCkdKbn")
                            .AppendLine(",@MakerCode")
                            .AppendLine(",@MakerName")
                            .AppendLine(",@BuhinNo")
                            .AppendLine(",@BuhinNoKbn")
                            .AppendLine(",@BuhinNoKaiteiNo")
                            .AppendLine(",@EdaBan")
                            .AppendLine(",@BuhinName")
                            .AppendLine(",@Saishiyoufuka")
                            .AppendLine(",@ShutuzuYoteiDate")
                            .AppendLine(",@TsukurikataSeisaku")
                            .AppendLine(",@TsukurikataKatashiyou1")
                            .AppendLine(",@TsukurikataKatashiyou2")
                            .AppendLine(",@TsukurikataKatashiyou3")
                            .AppendLine(",@TsukurikataTigu")
                            .AppendLine(",@TsukurikataNounyu")
                            .AppendLine(",@TsukurikataKibo")
                            .AppendLine(",@BaseBuhinFlg")
                            .AppendLine(",@ZaishituKikaku1")
                            .AppendLine(",@ZaishituKikaku2")
                            .AppendLine(",@ZaishituKikaku3")
                            .AppendLine(",@ZaishituMekki")
                            .AppendLine(",@ShisakuBankoSuryo")
                            .AppendLine(",@ShisakuBankoSuryoU")
                            '.AppendLine(",@MaterialInfoLength")
                            '.AppendLine(",@MaterialInfoWidth")
                            '.AppendLine(",@DataItemKaiteiNo")
                            '.AppendLine(",@DataItemAreaName")
                            '.AppendLine(",@DataItemSetName")
                            '.AppendLine(",@DataItemKaiteiInfo")
                            .AppendLine(",@ShisakuBuhinHi")
                            .AppendLine(",@ShisakuKataHi")
                            .AppendLine(",@Bikou")
                            .AppendLine(",@EditTourokubi")
                            .AppendLine(",@EditTourokujikan")
                            .AppendLine(",@KaiteiHandanFlg")
                            .AppendLine(",'F'")
                            .AppendLine(",''")
                            .AppendLine(",@KyoukuSection")
                            .AppendLine(",@SenyouMark")
                            .AppendLine(",''")
                            .AppendLine(",''")
                            .AppendLine(",'1'")
                            .AppendLine(",@SeihinKbn")
                            .AppendLine(",@ShisakuListCode")
                            .AppendLine(",@BuhinNote")
                            .AppendLine(",@CreatedUserId")
                            .AppendLine(",@CreatedUserDate")
                            .AppendLine(",@CreatedUserTime")
                            .AppendLine(",@UpdatedUserId")
                            .AppendLine(",@UpdatedDate")
                            .AppendLine(",@UpdatedTime")
                            .AppendLine(" )")
                        End With
                        With insert
                            .ClearParameters()
                            .AddParameter("@ShisakuEventCode", BuhinEditVo.ShisakuEventCode)
                            .AddParameter("@ShisakuBukaCode", BuhinEditVo.ShisakuBukaCode)
                            .AddParameter("@ShisakuBlockNo", BuhinEditVo.ShisakuBlockNo)
                            .AddParameter("@ShisakuBlockNoKaiteiNo", BuhinEditVo.ShisakuBlockNoKaiteiNo)
                            .AddParameter("@BuhinNoHyoujiJun", BuhinEditVo.BuhinNoHyoujiJun)
                            .AddParameter("@Level", BuhinEditVo.Level)
                            .AddParameter("@ShukeiCode", BuhinEditVo.ShukeiCode)
                            .AddParameter("@SiaShukeiCode", BuhinEditVo.SiaShukeiCode)
                            .AddParameter("@GencyoCkdKbn", BuhinEditVo.GencyoCkdKbn)
                            .AddParameter("@MakerCode", BuhinEditVo.MakerCode)
                            .AddParameter("@MakerName", BuhinEditVo.MakerName)
                            .AddParameter("@BuhinNo", BuhinEditVo.BuhinNo)
                            .AddParameter("@BuhinNoKbn", BuhinEditVo.BuhinNoKbn)
                            .AddParameter("@BuhinNoKaiteiNo", BuhinEditVo.BuhinNoKaiteiNo)
                            .AddParameter("@EdaBan", BuhinEditVo.EdaBan)
                            .AddParameter("@BuhinName", BuhinEditVo.BuhinName)
                            .AddParameter("@Saishiyoufuka", BuhinEditVo.Saishiyoufuka)
                            .AddParameter("@ShutuzuYoteiDate", BuhinEditVo.ShutuzuYoteiDate)
                            .AddParameter("@TsukurikataSeisaku", BuhinEditVo.TsukurikataSeisaku)
                            .AddParameter("@TsukurikataKatashiyou1", BuhinEditVo.TsukurikataKatashiyou1)
                            .AddParameter("@TsukurikataKatashiyou2", BuhinEditVo.TsukurikataKatashiyou2)
                            .AddParameter("@TsukurikataKatashiyou3", BuhinEditVo.TsukurikataKatashiyou3)
                            .AddParameter("@TsukurikataTigu", BuhinEditVo.TsukurikataTigu)
                            .AddParameter("@TsukurikataNounyu", BuhinEditVo.TsukurikataNounyu)
                            .AddParameter("@TsukurikataKibo", BuhinEditVo.TsukurikataKibo)
                            .AddParameter("@BaseBuhinFlg", BuhinEditVo.BaseBuhinFlg)
                            .AddParameter("@ZaishituKikaku1", BuhinEditVo.ZaishituKikaku1)
                            .AddParameter("@ZaishituKikaku2", BuhinEditVo.ZaishituKikaku2)
                            .AddParameter("@ZaishituKikaku3", BuhinEditVo.ZaishituKikaku3)
                            .AddParameter("@ZaishituMekki", BuhinEditVo.ZaishituMekki)
                            .AddParameter("@ShisakuBankoSuryo", BuhinEditVo.ShisakuBankoSuryo)
                            .AddParameter("@ShisakuBankoSuryoU", BuhinEditVo.ShisakuBankoSuryoU)
                            '.AddParameter("@MaterialInfoLength", BuhinEditVo.MaterialInfoLength)
                            '.AddParameter("@MaterialInfoWidth", BuhinEditVo.MaterialInfoWidth)
                            '.AddParameter("@DataItemKaiteiNo", BuhinEditVo.DataItemKaiteiNo)
                            '.AddParameter("@DataItemAreaName", BuhinEditVo.DataItemAreaName)
                            '.AddParameter("@DataItemSetName", BuhinEditVo.DataItemSetName)
                            '.AddParameter("@DataItemKaiteiInfo", BuhinEditVo.DataItemKaiteiInfo)
                            .AddParameter("@ShisakuBuhinHi", BuhinEditVo.ShisakuBuhinHi)
                            .AddParameter("@ShisakuKataHi", BuhinEditVo.ShisakuKataHi)
                            .AddParameter("@Bikou", BuhinEditVo.Bikou)
                            .AddParameter("@EditTourokubi", BuhinEditVo.EditTourokubi)
                            .AddParameter("@EditTourokujikan", BuhinEditVo.EditTourokujikan)
                            .AddParameter("@KaiteiHandanFlg", BuhinEditVo.KaiteiHandanFlg)
                            .AddParameter("@KyoukuSection", BuhinEditVo.KyoukuSection)
                            .AddParameter("@SenyouMark", SenyouMark)
                            .AddParameter("@SeihinKbn", _seihinKbn)
                            .AddParameter("@ShisakuListCode", _shisakuListCode)
                            .AddParameter("@BuhinNote", BuhinEditVo.BuhinNote)
                            .AddParameter("@CreatedUserId", CreatedUserId)
                            .AddParameter("@CreatedUserDate", CreatedUserDate)
                            .AddParameter("@CreatedUserTime", CreatedUserTime)
                            .AddParameter("@UpdatedUserId", UpdatedUserId)
                            .AddParameter("@UpdatedDate", UpdatedDate)
                            .AddParameter("@UpdatedTime", UpdatedTime)
                        End With

                    Case 8
                        With sql
                            .AppendLine("INSERT ")
                            .AppendLine("INTO " & MBOM_DB_NAME & ".dbo.T_YOSAN_SETTEI_BUHIN_TMP( ")
                            .AppendLine("  SHISAKU_EVENT_CODE ")
                            .AppendLine("  , SHISAKU_BUKA_CODE ")
                            .AppendLine("  , SHISAKU_BLOCK_NO ")
                            .AppendLine("  , SHISAKU_BLOCK_NO_KAITEI_NO ")
                            .AppendLine("  , BUHIN_NO_HYOUJI_JUN ")
                            .AppendLine("  , GYOU_ID ")
                            .AppendLine("  , LEVEL ")
                            .AppendLine("  , SHUKEI_CODE ")
                            .AppendLine("  , SIA_SHUKEI_CODE ")
                            .AppendLine("  , GENCYO_CKD_KBN ")
                            .AppendLine("  , MAKER_CODE ")
                            .AppendLine("  , MAKER_NAME ")
                            .AppendLine("  , BUHIN_NO ")
                            .AppendLine("  , BUHIN_NO_KBN ")
                            .AppendLine("  , BUHIN_NO_KAITEI_NO ")
                            .AppendLine("  , EDA_BAN ")
                            .AppendLine("  , BUHIN_NAME ")
                            .AppendLine("  , SAISHIYOUFUKA ")
                            .AppendLine("  , SHUTUZU_YOTEI_DATE ")
                            .AppendLine("  , TSUKURIKATA_SEISAKU ")
                            .AppendLine("  , TSUKURIKATA_KATASHIYOU_1 ")
                            .AppendLine("  , TSUKURIKATA_KATASHIYOU_2 ")
                            .AppendLine("  , TSUKURIKATA_KATASHIYOU_3 ")
                            .AppendLine("  , TSUKURIKATA_TIGU ")
                            .AppendLine("  , TSUKURIKATA_NOUNYU ")
                            .AppendLine("  , TSUKURIKATA_KIBO ")
                            .AppendLine("  , BASE_BUHIN_FLG ")
                            .AppendLine("  , ZAISHITU_KIKAKU_1 ")
                            .AppendLine("  , ZAISHITU_KIKAKU_2 ")
                            .AppendLine("  , ZAISHITU_KIKAKU_3 ")
                            .AppendLine("  , ZAISHITU_MEKKI ")
                            .AppendLine("  , SHISAKU_BANKO_SURYO ")
                            .AppendLine("  , SHISAKU_BANKO_SURYO_U ")
                            '.AppendLine("  , MATERIAL_INFO_LENGTH ")
                            '.AppendLine("  , MATERIAL_INFO_WIDTH ")
                            '.AppendLine("  , DATA_ITEM_KAITEI_NO ")
                            '.AppendLine("  , DATA_ITEM_AREA_NAME ")
                            '.AppendLine("  , DATA_ITEM_SET_NAME ")
                            '.AppendLine("  , DATA_ITEM_KAITEI_INFO ")
                            .AppendLine("  , SHISAKU_BUHIN_HI ")
                            .AppendLine("  , SHISAKU_KATA_HI ")
                            .AppendLine("  , BIKOU ")
                            .AppendLine("  , EDIT_TOUROKUBI ")
                            .AppendLine("  , EDIT_TOUROKUJIKAN ")
                            .AppendLine("  , KAITEI_HANDAN_FLG ")
                            .AppendLine("  , TEHAI_KIGOU ")
                            .AppendLine("  , NOUBA ")
                            .AppendLine("  , KYOUKU_SECTION ")
                            .AppendLine("  , SENYOU_MARK ")
                            .AppendLine("  , KOUTAN ")
                            .AppendLine("  , STSR_DHSTBA ")
                            .AppendLine("  , HENKATEN ")
                            .AppendLine("  , SHISAKU_SEIHIN_KBN ")
                            .AppendLine("  , SHISAKU_LIST_CODE ")
                            .AppendLine("  , BUHIN_NOTE ")
                            .AppendLine("  , CREATED_USER_ID ")
                            .AppendLine("  , CREATED_DATE ")
                            .AppendLine("  , CREATED_TIME ")
                            .AppendLine("  , UPDATED_USER_ID ")
                            .AppendLine("  , UPDATED_DATE ")
                            .AppendLine("  , UPDATED_TIME ")
                            .AppendLine(") ")
                            .AppendLine("VALUES ( ")
                            .AppendLine(" @ShisakuEventCode")
                            .AppendLine(",@ShisakuBukaCode")
                            .AppendLine(",@ShisakuBlockNo")
                            .AppendLine(",@ShisakuBlockNoKaiteiNo")
                            .AppendLine(",@BuhinNoHyoujiJun")
                            .AppendLine(",'000'")
                            .AppendLine(",@Level")
                            .AppendLine(",@ShukeiCode")
                            .AppendLine(",@SiaShukeiCode")
                            .AppendLine(",@GencyoCkdKbn")
                            .AppendLine(",@MakerCode")
                            .AppendLine(",@MakerName")
                            .AppendLine(",@BuhinNo")
                            .AppendLine(",@BuhinNoKbn")
                            .AppendLine(",@BuhinNoKaiteiNo")
                            .AppendLine(",@EdaBan")
                            .AppendLine(",@BuhinName")
                            .AppendLine(",@Saishiyoufuka")
                            .AppendLine(",@ShutuzuYoteiDate")
                            .AppendLine(",@TsukurikataSeisaku")
                            .AppendLine(",@TsukurikataKatashiyou1")
                            .AppendLine(",@TsukurikataKatashiyou2")
                            .AppendLine(",@TsukurikataKatashiyou3")
                            .AppendLine(",@TsukurikataTigu")
                            .AppendLine(",@TsukurikataNounyu")
                            .AppendLine(",@TsukurikataKibo")
                            .AppendLine(",@BaseBuhinFlg")
                            .AppendLine(",@ZaishituKikaku1")
                            .AppendLine(",@ZaishituKikaku2")
                            .AppendLine(",@ZaishituKikaku3")
                            .AppendLine(",@ZaishituMekki")
                            .AppendLine(",@ShisakuBankoSuryo")
                            .AppendLine(",@ShisakuBankoSuryoU")
                            '.AppendLine(",@MaterialInfoLength")
                            '.AppendLine(",@MaterialInfoWidth")
                            '.AppendLine(",@DataItemKaiteiNo")
                            '.AppendLine(",@DataItemAreaName")
                            '.AppendLine(",@DataItemSetName")
                            '.AppendLine(",@DataItemKaiteiInfo")
                            .AppendLine(",@ShisakuBuhinHi")
                            .AppendLine(",@ShisakuKataHi")
                            .AppendLine(",@Bikou")
                            .AppendLine(",@EditTourokubi")
                            .AppendLine(",@EditTourokujikan")
                            .AppendLine(",@KaiteiHandanFlg")
                            .AppendLine(",'F'")
                            .AppendLine(",''")
                            .AppendLine(",@KyoukuSection")
                            .AppendLine(",@SenyouMark")
                            .AppendLine(",''")
                            .AppendLine(",''")
                            .AppendLine(",'2'")
                            .AppendLine(",@SeihinKbn")
                            .AppendLine(",@ShisakuListCode")
                            .AppendLine(",@BuhinNote")
                            .AppendLine(",@CreatedUserId")
                            .AppendLine(",@CreatedUserDate")
                            .AppendLine(",@CreatedUserTime")
                            .AppendLine(",@UpdatedUserId")
                            .AppendLine(",@UpdatedDate")
                            .AppendLine(",@UpdatedTime")
                            .AppendLine(" )")
                        End With
                        With insert
                            .ClearParameters()
                            .AddParameter("@ShisakuEventCode", BuhinEditVo.ShisakuEventCode)
                            .AddParameter("@ShisakuBukaCode", BuhinEditVo.ShisakuBukaCode)
                            .AddParameter("@ShisakuBlockNo", BuhinEditVo.ShisakuBlockNo)
                            .AddParameter("@ShisakuBlockNoKaiteiNo", BuhinEditVo.ShisakuBlockNoKaiteiNo)
                            .AddParameter("@BuhinNoHyoujiJun", BuhinEditVo.BuhinNoHyoujiJun)
                            .AddParameter("@Level", BuhinEditVo.Level)
                            .AddParameter("@ShukeiCode", BuhinEditVo.ShukeiCode)
                            .AddParameter("@SiaShukeiCode", BuhinEditVo.SiaShukeiCode)
                            .AddParameter("@GencyoCkdKbn", BuhinEditVo.GencyoCkdKbn)
                            .AddParameter("@MakerCode", BuhinEditVo.MakerCode)
                            .AddParameter("@MakerName", BuhinEditVo.MakerName)
                            .AddParameter("@BuhinNo", BuhinEditVo.BuhinNo)
                            .AddParameter("@BuhinNoKbn", BuhinEditVo.BuhinNoKbn)
                            .AddParameter("@BuhinNoKaiteiNo", BuhinEditVo.BuhinNoKaiteiNo)
                            .AddParameter("@EdaBan", BuhinEditVo.EdaBan)
                            .AddParameter("@BuhinName", BuhinEditVo.BuhinName)
                            .AddParameter("@Saishiyoufuka", BuhinEditVo.Saishiyoufuka)
                            .AddParameter("@ShutuzuYoteiDate", BuhinEditVo.ShutuzuYoteiDate)
                            .AddParameter("@TsukurikataSeisaku", BuhinEditVo.TsukurikataSeisaku)
                            .AddParameter("@TsukurikataKatashiyou1", BuhinEditVo.TsukurikataKatashiyou1)
                            .AddParameter("@TsukurikataKatashiyou2", BuhinEditVo.TsukurikataKatashiyou2)
                            .AddParameter("@TsukurikataKatashiyou3", BuhinEditVo.TsukurikataKatashiyou3)
                            .AddParameter("@TsukurikataTigu", BuhinEditVo.TsukurikataTigu)
                            .AddParameter("@TsukurikataNounyu", BuhinEditVo.TsukurikataNounyu)
                            .AddParameter("@TsukurikataKibo", BuhinEditVo.TsukurikataKibo)
                            .AddParameter("@BaseBuhinFlg", BuhinEditVo.BaseBuhinFlg)
                            .AddParameter("@ZaishituKikaku1", BuhinEditVo.ZaishituKikaku1)
                            .AddParameter("@ZaishituKikaku2", BuhinEditVo.ZaishituKikaku2)
                            .AddParameter("@ZaishituKikaku3", BuhinEditVo.ZaishituKikaku3)
                            .AddParameter("@ZaishituMekki", BuhinEditVo.ZaishituMekki)
                            .AddParameter("@ShisakuBankoSuryo", BuhinEditVo.ShisakuBankoSuryo)
                            .AddParameter("@ShisakuBankoSuryoU", BuhinEditVo.ShisakuBankoSuryoU)
                            '.AddParameter("@MaterialInfoLength", BuhinEditVo.MaterialInfoLength)
                            '.AddParameter("@MaterialInfoWidth", BuhinEditVo.MaterialInfoWidth)
                            '.AddParameter("@DataItemKaiteiNo", BuhinEditVo.DataItemKaiteiNo)
                            '.AddParameter("@DataItemAreaName", BuhinEditVo.DataItemAreaName)
                            '.AddParameter("@DataItemSetName", BuhinEditVo.DataItemSetName)
                            '.AddParameter("@DataItemKaiteiInfo", BuhinEditVo.DataItemKaiteiInfo)
                            .AddParameter("@ShisakuBuhinHi", BuhinEditVo.ShisakuBuhinHi)
                            .AddParameter("@ShisakuKataHi", BuhinEditVo.ShisakuKataHi)
                            .AddParameter("@Bikou", BuhinEditVo.Bikou)
                            .AddParameter("@EditTourokubi", BuhinEditVo.EditTourokubi)
                            .AddParameter("@EditTourokujikan", BuhinEditVo.EditTourokujikan)
                            .AddParameter("@KaiteiHandanFlg", BuhinEditVo.KaiteiHandanFlg)
                            .AddParameter("@KyoukuSection", BuhinEditVo.KyoukuSection)
                            .AddParameter("@SenyouMark", SenyouMark)
                            .AddParameter("@SeihinKbn", _seihinKbn)
                            .AddParameter("@ShisakuListCode", _shisakuListCode)
                            .AddParameter("@BuhinNote", BuhinEditVo.BuhinNote)
                            .AddParameter("@CreatedUserId", CreatedUserId)
                            .AddParameter("@CreatedUserDate", CreatedUserDate)
                            .AddParameter("@CreatedUserTime", CreatedUserTime)
                            .AddParameter("@UpdatedUserId", UpdatedUserId)
                            .AddParameter("@UpdatedDate", UpdatedDate)
                            .AddParameter("@UpdatedTime", UpdatedTime)
                        End With
                End Select

                Try
                    insert.ExecuteNonQuery(sql.ToString)

                Catch ex As SqlClient.SqlException
                    Dim prm As Integer = ex.Message.IndexOf("PRIMARY")
                    If prm < 0 Then
                        Dim msg As String = sql.ToString + ex.Message
                        MsgBox(ex.Message)
                    End If
                End Try
                insert.Commit()
            End Using
        End Sub

        ''' <summary>
        ''' SQL文の作成(号車)
        ''' </summary>
        ''' <param name="BuhinEditGousyaTmp">部品編集号車</param>
        ''' <param name="flag">変更無し(更新):0、変更無し(追加):1、追加品番(更新):2、追加品番(追加):3、員数増(更新):4、員数増(追加):5、員数減(更新):6、員数減(追加):7</param>
        ''' <remarks></remarks>
        Private Sub SqlCreateGousyaEdit(ByVal BuhinEditGousyaTmp As TYosanSetteiGousyaTmpVo, ByVal flag As Integer)
            sql = ""
            Dim aDate As New ShisakuDate
            Dim CreatedUserId As String = LoginInfo.Now.UserId
            Dim CreatedUserDate As String = aDate.CurrentDateDbFormat
            Dim CreatedUserTime As String = aDate.CurrentTimeDbFormat
            Dim UpdatedUserId As String = LoginInfo.Now.UserId
            Dim UpdatedDate As String = aDate.CurrentDateDbFormat
            Dim UpdatedTime As String = aDate.CurrentTimeDbFormat
            'Dim buhinNoHyoujiJun As Integer = 0
            'If flag = 5 Then
            '    buhinNoHyoujiJun = HikakuImpl.FindByNewGousyaBuhinNoHyoujiJun(BuhinEditGousyaTmp.ShisakuEventCode, BuhinEditGousyaTmp.ShisakuBukaCode, BuhinEditGousyaTmp.ShisakuBlockNo)
            'End If


            Select Case flag
                Case 0
                    '変更無し(更新)'
                    sql = _
                    " UPDATE " & MBOM_DB_NAME & ".dbo.T_YOSAN_SETTEI_GOUSYA_TMP  " _
                    & " SET " _
                    & " UPDATED_USER_ID = '0' ," _
                    & " UPDATED_DATE = '" & UpdatedDate & "' ," _
                    & " UPDATED_TIME = '" & UpdatedTime & "' " _
                    & " WHERE SHISAKU_EVENT_CODE = '" & BuhinEditGousyaTmp.ShisakuEventCode & "' " _
                    & " AND SHISAKU_BUKA_CODE = '" & BuhinEditGousyaTmp.ShisakuBukaCode & "' " _
                    & " AND SHISAKU_BLOCK_NO = '" & BuhinEditGousyaTmp.ShisakuBlockNo & "' " _
                    & " AND SHISAKU_BLOCK_NO_KAITEI_NO = '" & BuhinEditGousyaTmp.ShisakuBlockNoKaiteiNo & "' " _
                    & " AND BUHIN_NO_HYOUJI_JUN = '" & BuhinEditGousyaTmp.BuhinNoHyoujiJun & "' " _
                    & " AND SHISAKU_GOUSYA_HYOUJI_JUN = '" & BuhinEditGousyaTmp.ShisakuGousyaHyoujiJun & "'" _
                    & " AND GYOU_ID = '000' "

                Case 1
                    '変更無し(追加)'
                    sql = _
                    " INSERT INTO " & MBOM_DB_NAME & ".dbo.T_YOSAN_SETTEI_GOUSYA_TMP ( " _
                    & " SHISAKU_EVENT_CODE, " _
                    & " SHISAKU_BUKA_CODE, " _
                    & " SHISAKU_BLOCK_NO, " _
                    & " SHISAKU_BLOCK_NO_KAITEI_NO, " _
                    & " BUHIN_NO_HYOUJI_JUN, " _
                    & " SHISAKU_GOUSYA_HYOUJI_JUN, " _
                    & " GYOU_ID, " _
                    & " SHISAKU_GOUSYA, " _
                    & " INSU_SURYO, " _
                    & " CREATED_USER_ID, " _
                    & " CREATED_DATE, " _
                    & " CREATED_TIME, " _
                    & " UPDATED_USER_ID, " _
                    & " UPDATED_DATE, " _
                    & " UPDATED_TIME " _
                    & " ) " _
                    & " VALUES ( " _
                    & "'" & BuhinEditGousyaTmp.ShisakuEventCode & "' , " _
                    & "'" & BuhinEditGousyaTmp.ShisakuBukaCode & "' , " _
                    & "'" & BuhinEditGousyaTmp.ShisakuBlockNo & "' , " _
                    & "'" & BuhinEditGousyaTmp.ShisakuBlockNoKaiteiNo & "' , " _
                    & "" & BuhinEditGousyaTmp.BuhinNoHyoujiJun & " , " _
                    & "" & BuhinEditGousyaTmp.ShisakuGousyaHyoujiJun & " , " _
                    & "'000', " _
                    & "'" & BuhinEditGousyaTmp.ShisakuGousya & "' , " _
                    & "" & BuhinEditGousyaTmp.InsuSuryo & " , " _
                    & " '" & CreatedUserId & "' ," _
                    & " '" & CreatedUserDate & "' ," _
                    & " '" & CreatedUserTime & "' ," _
                    & " '" & BuhinEditGousyaTmp.UpdatedUserId & "' ," _
                    & " '" & UpdatedDate & "' ," _
                    & " '" & UpdatedTime & "' " _
                    & " ) "

                Case 2
                    '追加品番(更新)'
                    sql = _
                    " UPDATE " & MBOM_DB_NAME & ".dbo.T_YOSAN_SETTEI_GOUSYA_TMP  " _
                    & " SET " _
                    & " UPDATED_USER_ID = '2' ," _
                    & " UPDATED_DATE = '" & UpdatedDate & "' ," _
                    & " UPDATED_TIME = '" & UpdatedTime & "' " _
                    & " WHERE SHISAKU_EVENT_CODE = '" & BuhinEditGousyaTmp.ShisakuEventCode & "' " _
                    & " AND SHISAKU_BUKA_CODE = '" & BuhinEditGousyaTmp.ShisakuBukaCode & "' " _
                    & " AND SHISAKU_BLOCK_NO = '" & BuhinEditGousyaTmp.ShisakuBlockNo & "' " _
                    & " AND SHISAKU_BLOCK_NO_KAITEI_NO = '" & BuhinEditGousyaTmp.ShisakuBlockNoKaiteiNo & "' " _
                    & " AND BUHIN_NO_HYOUJI_JUN = '" & BuhinEditGousyaTmp.BuhinNoHyoujiJun & "' " _
                    & " AND SHISAKU_GOUSYA_HYOUJI_JUN = '" & BuhinEditGousyaTmp.ShisakuGousyaHyoujiJun & "'" _
                    & " AND GYOU_ID = '000' "

                Case 3
                    '追加品番(追加)'
                    sql = _
                    " INSERT INTO " & MBOM_DB_NAME & ".dbo.T_YOSAN_SETTEI_GOUSYA_TMP ( " _
                    & " SHISAKU_EVENT_CODE, " _
                    & " SHISAKU_BUKA_CODE, " _
                    & " SHISAKU_BLOCK_NO, " _
                    & " SHISAKU_BLOCK_NO_KAITEI_NO, " _
                    & " BUHIN_NO_HYOUJI_JUN, " _
                    & " SHISAKU_GOUSYA_HYOUJI_JUN, " _
                    & " GYOU_ID," _
                    & " SHISAKU_GOUSYA, " _
                    & " INSU_SURYO, " _
                    & " CREATED_USER_ID, " _
                    & " CREATED_DATE, " _
                    & " CREATED_TIME, " _
                    & " UPDATED_USER_ID, " _
                    & " UPDATED_DATE, " _
                    & " UPDATED_TIME " _
                    & " ) " _
                    & " VALUES ( " _
                    & "'" & BuhinEditGousyaTmp.ShisakuEventCode & "' , " _
                    & "'" & BuhinEditGousyaTmp.ShisakuBukaCode & "' , " _
                    & "'" & BuhinEditGousyaTmp.ShisakuBlockNo & "' , " _
                    & "'" & BuhinEditGousyaTmp.ShisakuBlockNoKaiteiNo & "' , " _
                    & "" & BuhinEditGousyaTmp.BuhinNoHyoujiJun & " , " _
                    & "" & BuhinEditGousyaTmp.ShisakuGousyaHyoujiJun & " , " _
                    & "'000', " _
                    & "'" & BuhinEditGousyaTmp.ShisakuGousya & "' , " _
                    & "" & BuhinEditGousyaTmp.InsuSuryo & " , " _
                    & " '" & CreatedUserId & "' ," _
                    & " '" & CreatedUserDate & "' ," _
                    & " '" & CreatedUserTime & "' ," _
                    & " '3' ," _
                    & " '" & UpdatedDate & "' ," _
                    & " '" & UpdatedTime & "' " _
                    & " ) "
                Case 4
                    '員数増(更新)'
                    sql = _
                    " UPDATE " & MBOM_DB_NAME & ".dbo.T_YOSAN_SETTEI_GOUSYA_TMP  " _
                    & " SET " _
                    & " INSU_SURYO =  " & BuhinEditGousyaTmp.InsuSuryo & "," _
                    & " GYOU_ID = '001' ," _
                    & " UPDATED_USER_ID = '4' ," _
                    & " UPDATED_DATE = '" & UpdatedDate & "' ," _
                    & " UPDATED_TIME = '" & UpdatedTime & "' " _
                    & " WHERE SHISAKU_EVENT_CODE = '" & BuhinEditGousyaTmp.ShisakuEventCode & "' " _
                    & " AND SHISAKU_BUKA_CODE = '" & BuhinEditGousyaTmp.ShisakuBukaCode & "' " _
                    & " AND SHISAKU_BLOCK_NO = '" & BuhinEditGousyaTmp.ShisakuBlockNo & "' " _
                    & " AND SHISAKU_BLOCK_NO_KAITEI_NO = '" & BuhinEditGousyaTmp.ShisakuBlockNoKaiteiNo & "' " _
                    & " AND BUHIN_NO_HYOUJI_JUN = '" & BuhinEditGousyaTmp.BuhinNoHyoujiJun & "' " _
                    & " AND SHISAKU_GOUSYA_HYOUJI_JUN = '" & BuhinEditGousyaTmp.ShisakuGousyaHyoujiJun & "'" _
                    & " AND GYOU_ID = '000' "

                Case 5
                    '員数増(追加)'
                    '変更無し(追加)'
                    sql = _
                    " INSERT INTO " & MBOM_DB_NAME & ".dbo.T_YOSAN_SETTEI_GOUSYA_TMP ( " _
                    & " SHISAKU_EVENT_CODE, " _
                    & " SHISAKU_BUKA_CODE, " _
                    & " SHISAKU_BLOCK_NO, " _
                    & " SHISAKU_BLOCK_NO_KAITEI_NO, " _
                    & " BUHIN_NO_HYOUJI_JUN, " _
                    & " SHISAKU_GOUSYA_HYOUJI_JUN, " _
                    & " GYOU_ID, " _
                    & " SHISAKU_GOUSYA, " _
                    & " INSU_SURYO, " _
                    & " CREATED_USER_ID, " _
                    & " CREATED_DATE, " _
                    & " CREATED_TIME, " _
                    & " UPDATED_USER_ID, " _
                    & " UPDATED_DATE, " _
                    & " UPDATED_TIME " _
                    & " ) " _
                    & " VALUES ( " _
                    & "'" & BuhinEditGousyaTmp.ShisakuEventCode & "' , " _
                    & "'" & BuhinEditGousyaTmp.ShisakuBukaCode & "' , " _
                    & "'" & BuhinEditGousyaTmp.ShisakuBlockNo & "' , " _
                    & "'" & BuhinEditGousyaTmp.ShisakuBlockNoKaiteiNo & "' , " _
                    & "" & BuhinEditGousyaTmp.BuhinNoHyoujiJun & " , " _
                    & "" & BuhinEditGousyaTmp.ShisakuGousyaHyoujiJun & " , " _
                    & "'001', " _
                    & "'" & BuhinEditGousyaTmp.ShisakuGousya & "' , " _
                    & "" & BuhinEditGousyaTmp.InsuSuryo & " , " _
                    & " '" & CreatedUserId & "' ," _
                    & " '" & CreatedUserDate & "' ," _
                    & " '" & CreatedUserTime & "' ," _
                    & " '5' ," _
                    & " '" & UpdatedDate & "' ," _
                    & " '" & UpdatedTime & "' " _
                    & " ) "
                Case 6
                    '員数減(更新)'
                    sql = _
                    " UPDATE " & MBOM_DB_NAME & ".dbo.T_YOSAN_SETTEI_GOUSYA_TMP  " _
                    & " SET " _
                    & " UPDATED_USER_ID = '6' ," _
                    & " UPDATED_DATE = '" & UpdatedDate & "' ," _
                    & " UPDATED_TIME = '" & UpdatedTime & "' " _
                    & " WHERE SHISAKU_EVENT_CODE = '" & BuhinEditGousyaTmp.ShisakuEventCode & "' " _
                    & " AND SHISAKU_BUKA_CODE = '" & BuhinEditGousyaTmp.ShisakuBukaCode & "' " _
                    & " AND SHISAKU_BLOCK_NO = '" & BuhinEditGousyaTmp.ShisakuBlockNo & "' " _
                    & " AND SHISAKU_BLOCK_NO_KAITEI_NO = '" & BuhinEditGousyaTmp.ShisakuBlockNoKaiteiNo & "' " _
                    & " AND BUHIN_NO_HYOUJI_JUN = '" & BuhinEditGousyaTmp.BuhinNoHyoujiJun & "' " _
                    & " AND SHISAKU_GOUSYA_HYOUJI_JUN = '" & BuhinEditGousyaTmp.ShisakuGousyaHyoujiJun & "'" _
                    & " AND GYOU_ID = '000' "
                Case 7
                    '員数減(追加)'
                    '変更無し(追加)'
                    sql = _
                    " INSERT INTO " & MBOM_DB_NAME & ".dbo.T_YOSAN_SETTEI_GOUSYA_TMP ( " _
                    & " SHISAKU_EVENT_CODE, " _
                    & " SHISAKU_BUKA_CODE, " _
                    & " SHISAKU_BLOCK_NO, " _
                    & " SHISAKU_BLOCK_NO_KAITEI_NO, " _
                    & " BUHIN_NO_HYOUJI_JUN, " _
                    & " SHISAKU_GOUSYA_HYOUJI_JUN, " _
                    & " GYOU_ID, " _
                    & " SHISAKU_GOUSYA, " _
                    & " INSU_SURYO, " _
                    & " CREATED_USER_ID, " _
                    & " CREATED_DATE, " _
                    & " CREATED_TIME, " _
                    & " UPDATED_USER_ID, " _
                    & " UPDATED_DATE, " _
                    & " UPDATED_TIME " _
                    & " ) " _
                    & " VALUES ( " _
                    & "'" & BuhinEditGousyaTmp.ShisakuEventCode & "' , " _
                    & "'" & BuhinEditGousyaTmp.ShisakuBukaCode & "' , " _
                    & "'" & BuhinEditGousyaTmp.ShisakuBlockNo & "' , " _
                    & "'" & BuhinEditGousyaTmp.ShisakuBlockNoKaiteiNo & "' , " _
                    & "" & BuhinEditGousyaTmp.BuhinNoHyoujiJun & " , " _
                    & "" & BuhinEditGousyaTmp.ShisakuGousyaHyoujiJun & " , " _
                    & "'000', " _
                    & "'" & BuhinEditGousyaTmp.ShisakuGousya & "' , " _
                    & "" & BuhinEditGousyaTmp.InsuSuryo & " , " _
                    & " '" & UpdatedUserId & "' ," _
                    & " '" & CreatedUserDate & "' ," _
                    & " '" & CreatedUserTime & "' ," _
                    & " '7' ," _
                    & " '" & UpdatedDate & "' ," _
                    & " '" & UpdatedTime & "' " _
                    & " ) "
            End Select


            '員数増のみ部品番号表示順が変わる可能性があるから先に飛ばす'
            'If flag = 5 Then
            Using insert As New SqlAccess(g_kanrihyoIni(DB_KEY_EBOM))
                insert.Open()
                insert.BeginTransaction()
                Try
                    insert.ExecuteNonQuery(sql)

                Catch ex As SqlClient.SqlException
                    Dim prm As Integer = ex.Message.IndexOf("PRIMARY")
                    If prm < 0 Then
                        Dim msg As String = sql + ex.Message
                        MsgBox(ex.Message)
                    End If
                End Try
                insert.Commit()
            End Using
            'End If

        End Sub




    End Class

End Namespace
