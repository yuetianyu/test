Imports ShisakuCommon
Imports ShisakuCommon.Db.EBom.Vo
Imports EventSakusei.TehaichoMenu.Impl
Imports EventSakusei.TehaichoMenu.Dao
Imports EventSakusei.TehaichoMenu

Imports EBom.Common

Namespace TehaichoMenu
    Public Class TehaichoErrorCheck

        Private errorImpl As TehaichoMenuDao
        Private sakuseiImpl As TehaichoSakusei.Dao.TehaichoSakuseiDao

        ''' <summary>
        ''' 初期設定
        ''' </summary>
        ''' <remarks></remarks>
        Public Sub New()
            errorImpl = New TehaichoMenuDaoImpl
            sakuseiImpl = New TehaichoSakusei.Dao.TehaichoSakuseiDaoImpl
        End Sub

        ''' <summary>
        ''' エラーチェック本体
        ''' </summary>
        ''' <param name="shisakuEventCode">試作イベントコード</param>
        ''' <param name="shisakuListCode">試作リストコード</param>
        ''' <returns>結果</returns>
        ''' <remarks></remarks>
        Public Function ErrorCheck(ByVal shisakuEventCode As String, ByVal shisakuListCode As String) As Boolean
            Dim watch As New Stopwatch()
            errorImpl.DeleteByError(shisakuEventCode, shisakuListCode)

            watch.Start()

            Dim StatusFlg As Boolean = False

            Dim errorVoList As New List(Of TShisakuTehaiErrorVo)

            'ブロック情報を取得する'
            For Each blockVo As TShisakuSekkeiBlockVo In errorImpl.FindByShisakuBlockList(shisakuEventCode, shisakuListCode)
                For Each kihonVo As TShisakuTehaiKihonVo In errorImpl.FindByListKihon(shisakuEventCode, shisakuListCode, blockVo.ShisakuBukaCode, blockVo.ShisakuBlockNo)
                    If StringUtil.Equals(kihonVo.TehaiKigou, "F") Then
                        Continue For
                    End If

                    Dim errorFlg = False
                    Dim warningFlg = False

                    Dim errorVo As TShisakuTehaiErrorVo
                    '新調達か現調品か'
                    If Not kihonVo.TehaiKigou = "J" AndAlso Not kihonVo.TehaiKigou = "B" Then
                        '柳沼修正
                        errorVo = ErrorCheckShinThotatsu(kihonVo, errorFlg, warningFlg)
                    Else
                        '柳沼修正
                        errorVo = ErrorCheckGencho(kihonVo, errorFlg, warningFlg)
                    End If
                    errorVo.ShisakuEventCode = kihonVo.ShisakuEventCode
                    errorVo.ShisakuListCode = kihonVo.ShisakuListCode
                    errorVo.ShisakuListCodeKaiteiNo = kihonVo.ShisakuListCodeKaiteiNo
                    errorVo.ShisakuBukaCode = kihonVo.ShisakuBukaCode
                    errorVo.ShisakuBuhinHyoujiJun = kihonVo.BuhinNoHyoujiJun
                    errorVo.ShisakuBlockNo = kihonVo.ShisakuBlockNo
                    errorVo.BuhinNo = kihonVo.BuhinNo
                    errorVo.BuhinNoKbn = kihonVo.BuhinNoKbn
                    errorVo.BuhinName = kihonVo.BuhinName
                    errorVo.TotalInsuSuryo = kihonVo.TotalInsuSuryo
                    errorVo.NounyuShijibi = kihonVo.NounyuShijibi
                    errorVo.Nouba = kihonVo.Nouba
                    errorVo.KyoukuSection = kihonVo.KyoukuSection
                    errorVo.Koutan = kihonVo.Koutan
                    errorVo.Torihikisaki = kihonVo.TorihikisakiCode

                    'エラーがあるかチェック'
                    If errorFlg Then
                        errorVo.ErrorKbn = "E"
                        StatusFlg = True
                    ElseIf warningFlg Then
                        errorVo.ErrorKbn = "W"
                        StatusFlg = True
                    End If
                    errorVoList.Add(errorVo)
                Next
            Next

            watch.Stop()
            Console.WriteLine(String.Format("CheckEnd : {0}ms", watch.ElapsedMilliseconds))
            watch.Reset()
            watch.Start()

            errorImpl.InsertByError(errorVoList)
            watch.Stop()
            Console.WriteLine(String.Format("DataInsert : {0}ms", watch.ElapsedMilliseconds))
            watch.Reset()
            watch.Start()
            errorImpl.UpdateByErrorKbn(errorVoList)
            watch.Stop()
            Console.WriteLine(String.Format("DataUpdate : {0}ms", watch.ElapsedMilliseconds))
            watch.Reset()
            watch.Start()

            Return StatusFlg

        End Function

        ''' <summary>
        ''' 新調達のエラーチェックを行う
        ''' </summary>
        ''' <param name="voVal">リスト番号</param>
        ''' <remarks></remarks>
        Private Function ErrorCheckShinThotatsu(ByVal voVal As TShisakuTehaiKihonVo, ByRef errorFlg As Boolean, ByRef warningFlg As Boolean) As TShisakuTehaiErrorVo
            Dim errorVo As New TShisakuTehaiErrorVo
            errorVo.KokunaiGenchoFlg = "1"
            'ブロックNoがブランクの場合エラー(E01)'
            If StringUtil.IsEmpty(voVal.ShisakuBlockNo) Then
                errorFlg = True
                errorVo.EcShisakuBlockNo = "E01"
            End If
            '部品番号がブランクの場合エラー(E03)'
            If StringUtil.IsEmpty(voVal.BuhinNo) Then
                errorFlg = True
                errorVo.EcBuhinNo = "E03"

                '部品番号に使用禁止文字がある場合はエラー(E04)'
            ElseIf Not StrErrCheck(voVal.BuhinNo) Then
                errorFlg = True
                errorVo.EcBuhinNo = "E04"
                '12桁の場合後ろ２文字が##ならエラーにする'
                If StringUtil.IsEmpty(errorVo.EcBuhinNo) Then
                    If voVal.BuhinNo.Length = 12 Then
                        If StringUtil.Equals(Right(voVal.BuhinNo, 2), "##") Then
                            errorFlg = True
                            errorVo.EcBuhinNo = "E04"
                        End If
                    End If
                End If
            End If

            '部品名称がブランクの場合エラー'
            If StringUtil.IsEmpty(voVal.BuhinName) Then
                errorFlg = True
                errorVo.EcBuhinName = "E05"
            End If

            '購担がブランクの場合エラー(E16)'
            If StringUtil.IsEmpty(voVal.Koutan) Then
                errorFlg = True
                errorVo.EcKoutanSection = "E16"
            End If

            '得意先CD(取引先CD)がブランクの場合エラー(E18)'
            If StringUtil.IsEmpty(voVal.TorihikisakiCode) Then
                errorFlg = True
                errorVo.EcTorihikisaki = "E18"
            End If

            '手配個数(納入指示数)が0の場合エラー(E06)'
            If StringUtil.IsEmpty(voVal.TotalInsuSuryo) Then
                errorFlg = True
                errorVo.EcTotalInsuSuryo = "E06"
            ElseIf voVal.TotalInsuSuryo = 0 Then
                errorFlg = True
                errorVo.EcTotalInsuSuryo = "E06"
            End If

            '納期が空欄(0)の場合エラー(E07)'
            If StringUtil.IsEmpty(voVal.NounyuShijibi) OrElse voVal.NounyuShijibi = 0 Then
                errorFlg = True
                errorVo.EcNounyuShijibi = "E07"
            Else
                Dim wkNounyuShijibi As Long = DateDiff("d", Now, Date.Parse(Format(CInt(voVal.NounyuShijibi.ToString()), "0000/00/00")))
                '納期<チェック日(処理日)の場合はエラー(E08)'
                If wkNounyuShijibi < 0 Then
                    errorFlg = True
                    errorVo.EcNounyuShijibi = "E08"
                ElseIf wkNounyuShijibi <= 3 Then
                    '３日以内ならエラー(E09)'
                    errorFlg = True
                    errorVo.EcNounyuShijibi = "E09"
                ElseIf wkNounyuShijibi <= 5 Then
                    '５日以内ならワーニング(E10)'
                    warningFlg = True
                    errorVo.EcNounyuShijibi = "E10"
                End If
            End If
            '納入場所がブランクの場合エラー(E11)'
            If StringUtil.IsEmpty(voVal.Nouba) Then
                errorFlg = True
                errorVo.EcNouba = "E11"
            End If


            '供給セクションがブランクの場合エラー(E12)'
            If StringUtil.IsEmpty(voVal.KyoukuSection) Then
                errorFlg = True
                errorVo.EcKyoukuSection = "E12"
            Else
                '３ヶ月インフォメーション取得'
                Dim akpsmVo = errorImpl.FindBy3Month(voVal.ShisakuSeihinKbn, voVal.BuhinNo)

                '納入区分 = 4 取引先コード上１桁 = 9 かつ 供給セクション = 9の場合はエラー(E13)'
                If Not akpsmVo Is Nothing Then
                    If Not StringUtil.IsEmpty(akpsmVo.Nokm) Then
                        If StringUtil.Equals(akpsmVo.Nokm, "4") Then
                            If StringUtil.Equals(Left(voVal.TorihikisakiCode, 1), "9") Then
                                If StringUtil.Equals(voVal.KyoukuSection, "9") Then
                                    errorFlg = True
                                    errorVo.EcKyoukuSection = "E13"
                                End If
                            End If
                        End If
                    Else
                        '海外生産情報取得'
                        Dim aGKpsmVo = errorImpl.FindByForign(voVal.ShisakuSeihinKbn, voVal.BuhinNo)

                        If Not aGKpsmVo Is Nothing Then
                            If Not StringUtil.IsEmpty(aGKpsmVo.Nokm) Then
                                If StringUtil.Equals(aGKpsmVo.Nokm, "4") Then
                                    If StringUtil.Equals(Left(voVal.TorihikisakiCode, 1), "9") Then
                                        If StringUtil.Equals(voVal.KyoukuSection, "9") Then
                                            errorFlg = True
                                            errorVo.EcKyoukuSection = "E13"
                                        End If
                                    End If
                                End If
                            End If
                        End If
                    End If
                End If

                '供給セクションの1～4桁目が、緊急・限定の取引先マスタと違う場合エラー(E14)'
                If Not voVal.KyoukuSection.Length < 4 Then
                    Dim bAsPAPF14Vo As AsPAPF14Vo = errorImpl.FindByAsPAPF14(Left(voVal.KyoukuSection, 4))
                    If bAsPAPF14Vo IsNot Nothing Then
                        If bAsPAPF14Vo.Toricd Is Nothing Then
                            errorFlg = True
                            errorVo.EcKyoukuSection = "E14"
                        End If
                    End If
                End If
            End If

            If StringUtil.IsEmpty(voVal.Koutan) Then
                '新調達取引先取得'
                Dim aAsPAPF14Vo = errorImpl.FindByAsPAPF14(Trim(voVal.TorihikisakiCode))

                '購担が緊急・限定の取引先マスタと違う場合はエラー(E20)'
                If aAsPAPF14Vo IsNot Nothing Then
                    If StringUtil.IsNotEmpty(voVal.Koutan) Then
                        If Not StringUtil.Equals(voVal.Koutan, Trim(aAsPAPF14Vo.Koutan1)) Then
                            If Not StringUtil.Equals(voVal.Koutan, Trim(aAsPAPF14Vo.Koutan2)) Then
                                If Not StringUtil.Equals(voVal.Koutan, Trim(aAsPAPF14Vo.Koutan3)) Then
                                    errorFlg = True
                                    errorVo.EcKoutanSection = "E20"
                                End If
                            End If
                        End If
                    End If
                End If
            End If

            Dim TmpVo As TShisakuBuhinEditTmpVo = sakuseiImpl.FindByKoutanTorihikisaki(voVal.BuhinNo)

            '購担はまだエラーではない'
            If StringUtil.IsEmpty(errorVo.EcKoutanSection) Then

                '取得できているかチェック'
                If StringUtil.IsNotEmpty(TmpVo.Koutan) Then
                    '取得できていれば確認'
                    If Not StringUtil.Equals(voVal.Koutan, TmpVo.Koutan) Then
                        '一致していない場合ワーニング'
                        warningFlg = True
                        errorVo.EcKoutanSection = "E17"
                        'マスタ参照に書き込み'
                        errorVo.MasterKoutan = TmpVo.Koutan
                    End If
                End If
            Else
                If StringUtil.IsNotEmpty(TmpVo.Koutan) Then
                    errorVo.MasterKoutan = TmpVo.Koutan
                End If
            End If

            If StringUtil.IsEmpty(errorVo.EcTorihikisaki) Then

                If StringUtil.IsNotEmpty(TmpVo.MakerCode) Then
                    If Not StringUtil.Equals(voVal.TorihikisakiCode, TmpVo.MakerCode) Then
                        '一致していない場合ワーニング'
                        warningFlg = True
                        errorVo.EcTorihikisaki = "E19"
                        'マスタ参照に書き込む'
                        errorVo.MasterTorihikisaki = TmpVo.MakerCode
                    End If
                End If
            Else
                If StringUtil.IsNotEmpty(TmpVo.MakerCode) Then
                    errorVo.MasterTorihikisaki = TmpVo.MakerCode
                End If
            End If

            Return errorVo
        End Function

        ''' <summary>
        ''' 現調品のエラーチェックを行う
        ''' </summary>
        ''' <param name="voVal">リスト番号</param>
        ''' <remarks></remarks>
        Private Function ErrorCheckGencho(ByVal voVal As TShisakuTehaiKihonVo, ByRef errorFlg As Boolean, ByRef warningFlg As Boolean) As TShisakuTehaiErrorVo
            Dim errorVo As New TShisakuTehaiErrorVo

            errorVo.KokunaiGenchoFlg = "2"
            'ブロックNoがブランクの場合エラー(E01)'
            If StringUtil.IsEmpty(voVal.ShisakuBlockNo) Then
                errorFlg = True
                errorVo.EcShisakuBlockNo = "E01"
            End If
            '部品番号がブランクの場合エラー(E03)'
            If StringUtil.IsEmpty(voVal.BuhinNo) Then
                errorFlg = True
                errorVo.EcBuhinNo = "E03"
            End If
            '部品番号に使用禁止文字がある場合はエラー(E04)'
            If Not StrErrCheck(voVal.BuhinNo) Then
                errorFlg = True
                errorVo.EcBuhinNo = "E04"
                '12桁の場合後ろ２文字が##ならエラーにする'
                If StringUtil.IsEmpty(errorVo.EcBuhinNo) Then
                    If voVal.BuhinNo.Length = 12 Then
                        If StringUtil.Equals(Right(voVal.BuhinNo, 2), "##") Then
                            errorFlg = True
                            errorVo.EcBuhinNo = "E04"
                        End If
                    End If
                End If
            End If
            '部品名称がブランクの場合エラー'
            If StringUtil.IsEmpty(voVal.BuhinName) Then
                errorFlg = True
                errorVo.EcBuhinName = "E05"
            End If

            '購担がブランクの場合エラー(E16)'
            If StringUtil.IsEmpty(voVal.Koutan) Then
                errorFlg = True
                errorVo.EcKoutanSection = "E16"
            End If

            '得意先CD(取引先CD)がブランクの場合エラー(E18)'
            If StringUtil.IsEmpty(voVal.TorihikisakiCode) Then
                errorFlg = True
                errorVo.EcTorihikisaki = "E18"
            ElseIf IsNumeric(Left(voVal.TorihikisakiCode, 1)) Then
                If Integer.Parse(Left(voVal.TorihikisakiCode, 1)) >= 0 AndAlso Integer.Parse(Left(voVal.TorihikisakiCode, 1)) <= 9 Then
                    warningFlg = True
                    errorVo.EcTorihikisaki = "E22"
                End If
            End If

            '手配個数(納入指示数)が0の場合エラー(E06)'
            If StringUtil.IsEmpty(voVal.TotalInsuSuryo) Then
                errorFlg = True
                errorVo.EcTotalInsuSuryo = "E06"
            ElseIf voVal.TotalInsuSuryo = 0 Then
                errorFlg = True
                errorVo.EcTotalInsuSuryo = "E06"
            End If

            '納期が空欄(0)の場合エラー(E07)'
            If StringUtil.IsEmpty(voVal.NounyuShijibi) OrElse voVal.NounyuShijibi = 0 Then
                errorFlg = True
                errorVo.EcNounyuShijibi = "E07"
                '納期<チェック日(処理日)の場合はエラー(E08)'
            Else
                Dim wkNounyuShijibi As Long = DateDiff("d", Now, Date.Parse(Format(CInt(voVal.NounyuShijibi.ToString()), "0000/00/00")))
                If wkNounyuShijibi < 0 Then
                    errorFlg = True
                    errorVo.EcNounyuShijibi = "E08"
                ElseIf wkNounyuShijibi <= 3 Then
                    '３日以内ならエラー(E09)'
                    errorFlg = True
                    errorVo.EcNounyuShijibi = "E09"
                ElseIf wkNounyuShijibi <= 5 Then
                    '５日以内ならワーニング(E10)'
                    warningFlg = True
                    errorVo.EcNounyuShijibi = "E10"
                End If
            End If

            'Dim akpsmVo = errorImpl.FindBy3Month(voVal.ShisakuSeihinKbn, voVal.BuhinNo)
            'Dim aAsPAPF14Vo = errorImpl.FindByAsPAPF14(voVal.TorihikisakiCode)

            '供給セクションがブランクの場合エラー(E12)'
            If StringUtil.IsEmpty(voVal.KyoukuSection) Then
                errorFlg = True
                errorVo.EcKyoukuSection = "E12"

            Else
                '手配記号=Jの場合、供給セクションの上１桁がAの時ワーニング。手配記号=Bの場合、上一桁がAでなければワーニング'
                If voVal.TehaiKigou = "J" Then
                    If Left(voVal.KyoukuSection, 1) = "A" Then
                        warningFlg = True
                        errorVo.EcKyoukuSection = "E31"
                    End If
                ElseIf voVal.TehaiKigou = "B" Then
                    If Not Left(voVal.KyoukuSection, 1) = "A" Then
                        warningFlg = True
                        errorVo.EcKyoukuSection = "E31"
                    End If
                End If
            End If

            Dim TmpVo As TShisakuBuhinEditTmpVo = Nothing

            '購担はまだエラーではない'
            If StringUtil.IsEmpty(errorVo.EcKoutanSection) Then

                TmpVo = sakuseiImpl.FindByKoutanTorihikisaki(voVal.BuhinNo)

                '取得できているかチェック'
                If StringUtil.IsNotEmpty(TmpVo.Koutan) Then
                    '取得できていれば確認'
                    If Not StringUtil.Equals(voVal.Koutan, TmpVo.Koutan) Then
                        '一致していない場合ワーニング'
                        warningFlg = True
                        errorVo.EcKoutanSection = "E17"
                        'マスタ参照に書き込み'
                        errorVo.MasterKoutan = TmpVo.Koutan
                    End If
                Else
                    '存在しないならワーニング'
                    warningFlg = True
                    errorVo.EcKoutanSection = "E17"
                End If
            End If

            If StringUtil.IsEmpty(errorVo.EcTorihikisaki) Then
                If TmpVo Is Nothing Then TmpVo = sakuseiImpl.FindByKoutanTorihikisaki(voVal.BuhinNo)

                If StringUtil.IsNotEmpty(TmpVo.MakerCode) Then
                    If Not StringUtil.Equals(voVal.TorihikisakiCode, TmpVo.MakerCode) Then
                        '一致していない場合ワーニング'
                        warningFlg = True
                        errorVo.EcTorihikisaki = "E19"
                        'マスタ参照に書き込む'
                        errorVo.MasterTorihikisaki = TmpVo.MakerCode
                    End If
                Else
                    '存在していない場合ワーニング'
                    warningFlg = True
                    errorVo.EcTorihikisaki = "E19"
                End If
            End If

            Return errorVo
        End Function

        '''' <summary>
        '''' 前のエラーチェック内容を削除する
        '''' </summary>
        '''' <param name="shisakuEventCode">試作イベントコード</param>
        '''' <param name="shisakuListCode">試作リストコード</param>
        '''' <remarks></remarks>
        'Private Sub DelError(ByVal shisakuEventCode As String, ByVal shisakuListCode As String)
        '    errorImpl.DeleteByError(shisakuEventCode, shisakuListCode)
        'End Sub

        ''' <summary>
        ''' 文字列のエラーチェック
        ''' </summary>
        ''' <param name="str">キャメル記法の文字列</param>
        ''' <returns>半角英数および-,#,@以外の文字が含まれていた場合False</returns>
        ''' <remarks></remarks>
        Private Function StrErrCheck(ByVal str As String) As Boolean
            If str = String.Empty Then
                Return False
            End If
            '一文字ずつチェックする'
            Dim chars() As Char = str.ToCharArray
            'Dim result As Boolean = True
            For i As Integer = 0 To chars.Length - 1
                If "A" <= chars(i) AndAlso chars(i) <= "Z" Then
                    'result = True
                ElseIf "0" <= chars(i) AndAlso chars(i) <= "9" Then
                    'result = True
                ElseIf chars(i) = "-" OrElse chars(i) = "#" OrElse chars(i) = "@" Then
                    'result = True
                Else
                    Return False
                End If
            Next
            'Return result
            Return True
        End Function

    End Class
End Namespace