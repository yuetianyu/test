Imports ShisakuCommon.Db.EBom.Vo.Helper
Imports ShisakuCommon
Imports ShisakuCommon.Db.EBom.Dao.Impl
Imports ShisakuCommon.Db.EBom.Dao
Imports ShisakuCommon.Db.EBom.Vo
Imports EventSakusei.ShisakuBuhinEdit.Kosei.Dao

Namespace ShisakuBuhinEdit.Logic.Detect
    ''' <summary>
    ''' 最新の構成情報のありか探索するメソッドクラス
    ''' </summary>
    ''' <remarks></remarks>
    Public Class HoyouBuhinDetectLatestStructureImpl
        Implements HoyouBuhinDetectLatestStructure

        Private ReadOnly tantoKeyVo As THoyouSekkeiTantoVo

        Private ReadOnly aRhac0532Dao As Rhac0532Dao

        Private ReadOnly aRhac0533Dao As Rhac0533Dao
        Private ReadOnly aRhac0530Dao As Rhac0530Dao


        Private ReadOnly instlDao As TShisakuSekkeiBlockInstlDao
        Private ReadOnly editDao As THoyouBuhinEditDao

        'Private ReadOnly bfBuhinDao As New MakeStructureResultDaoImpl
        Private ReadOnly makeDao As New MakeStructureResultDaoImpl

        ''' <summary>
        ''' コンストラクタ
        ''' </summary>
        ''' <param name="tantoKeyVo">補用設計担当情報(キー情報)</param>
        ''' <remarks></remarks>
        Public Sub New(ByVal tantoKeyVo As THoyouSekkeiTantoVo)
            Me.New(tantoKeyVo, New Rhac0532DaoImpl, New Rhac0533DaoImpl, New Rhac0530DaoImpl, New TShisakuSekkeiBlockInstlDaoImpl, New THoyouBuhinEditDaoImpl)
        End Sub

        Private hoyouEventCode As String

        ''' <summary>
        ''' コンストラクタ
        ''' </summary>
        ''' <param name="tantoKeyVo">補用設計担当情報(キー情報)</param>
        ''' <param name="aRhac0532Dao">部品Dao</param>
        ''' <param name="instlDao">試作設計ブロックINSTL情報Dao</param>
        ''' <param name="editDao">補用部品編集情報Dao</param>
        ''' <remarks></remarks>
        Public Sub New(ByVal tantoKeyVo As THoyouSekkeiTantoVo, _
                       ByVal aRhac0532Dao As Rhac0532Dao, _
                       ByVal aRhac0533Dao As Rhac0533Dao, _
                       ByVal aRhac0530Dao As Rhac0530Dao, _
                       ByVal instlDao As TShisakuSekkeiBlockInstlDao, _
                       ByVal editDao As THoyouBuhinEditDao)

            Me.tantoKeyVo = tantoKeyVo
            Me.hoyouEventCode = tantoKeyVo.HoyouEventCode
            Me.aRhac0532Dao = aRhac0532Dao
            Me.aRhac0533Dao = aRhac0533Dao
            Me.aRhac0530Dao = aRhac0530Dao
            Me.instlDao = instlDao
            Me.editDao = editDao

            EzUtil.AssertParameterIsNotNull(tantoKeyVo, "tantoKeyVo")
            EzUtil.AssertParameterIsNotNull(tantoKeyVo.HoyouEventCode, "tantoKeyVo.HoyouEventCode")
            EzUtil.AssertParameterIsNotNull(tantoKeyVo.HoyouBukaCode, "tantoKeyVo.HoyouBukaCode")
            EzUtil.AssertParameterIsNotNull(tantoKeyVo.HoyouTanto, "tantoKeyVo.HoyouTanto")
            EzUtil.AssertParameterIsNotNull(tantoKeyVo.HoyouTantoKaiteiNo, "tantoKeyVo.HoyouTantoKaiteiNo")
        End Sub

        ''' <summary>
        ''' 部品番号を構成する、最新の構成の情報を返す
        ''' </summary>
        ''' <param name="buhinNo">部品番号</param>
        ''' <param name="buhinNoKbn">区分</param>
        ''' <param name="IsInstlHinban">INSTL品番を参照する場合、true</param>
        ''' <param name="kaihatsuFugo">開発符号</param>
        ''' <returns>構成の情報</returns>
        ''' <remarks></remarks>
        Public Function Compute(ByVal buhinNo As String, _
                                ByVal buhinNoKbn As String, _
                                ByVal IsInstlHinban As Boolean, _
                                ByVal kaihatsuFugo As String) As HoyouBuhinStructureResult Implements HoyouBuhinDetectLatestStructure.Compute

            If StringUtil.IsEmpty(buhinNo) Then
                Return HoyouBuhinStructureResult.NewNotExist(buhinNo, buhinNoKbn)
            End If

            '2012/02/27 テストデータとして存在したため回避処理を追加'
            If StringUtil.Equals(buhinNo, "1") OrElse StringUtil.Equals(buhinNo, "1111111111") Then
                Return HoyouBuhinStructureResult.NewNotExist(buhinNo, buhinNoKbn)
            End If

            If StringUtil.IsEmpty(buhinNoKbn) Then
                buhinNoKbn = ""
            End If

            Dim result As HoyouBuhinStructureResult
            If StringUtil.IsEmpty(buhinNoKbn) Then
                Dim resolvedBuhinNo As String = buhinNo
                If IsInstlHinban Then

                    '12桁以外の場合処理無し。
                    If buhinNo Is Nothing OrElse buhinNo.Length < 12 _
                        OrElse buhinNo.Length >= 13 Then
                        resolvedBuhinNo = buhinNo
                    Else
                        '-------------------------------------------------------------
                        '２次改修
                        '552には色無しでもチェックする。
                        Dim BuhinNoIroNashi = Conv0532HinbanByRule(buhinNo)
                        'まずは552を色コードを＃＃に置き換えた部品番号でチェック
                        Dim Rhac0552VoNashi As New Rhac0552Vo
                        Rhac0552VoNashi = makeDao.FindByBuhinRhac0552(BuhinNoIroNashi)
                        '-------------------------------------------------------------
                        'まずは552を色変換後の部品番号でチェック
                        Dim Rhac0552Vo As New Rhac0552Vo
                        Rhac0552Vo = makeDao.FindByBuhinRhac0552(buhinNo)
                        '次に553をチェック
                        Dim Rhac0553Vo As New Rhac0553Vo
                        '2012/03/08 開発符号が固定されてるのはダメ'
                        Rhac0553Vo = makeDao.FindByBuhinRhac0553("FM5", buhinNo)
                        '最後に551をチェック
                        Dim Rhac0551Vo As New Rhac0551Vo
                        Rhac0551Vo = makeDao.FindByBuhinRhac0551(buhinNo)
                        '全てのテーブルに存在しない場合は、基本Ｆ品番の色コードを＃＃に置き換える。
                        If Rhac0552Vo Is Nothing And Rhac0553Vo Is Nothing And Rhac0551Vo Is Nothing Then
                            resolvedBuhinNo = Conv0532HinbanByRule(buhinNo)
                        Else
                            '----------------------------------------------------------------------------
                            '２次改修
                            '   ５５２に＃＃付き品番があるならそちらを優先（＃＃に変換する）
                            If Rhac0552VoNashi Is Nothing Then
                                resolvedBuhinNo = buhinNo
                            Else
                                resolvedBuhinNo = Conv0532HinbanByRule(buhinNo)
                            End If
                            '----------------------------------------------------------------------------
                        End If
                    End If

                End If

                'まずは５３２をチェック
                '無ければ５３３をチェック
                'さらに無ければ５３０をチェック
                result = DetectRhac0532(buhinNo, resolvedBuhinNo, "")
                If result IsNot Nothing Then
                    Return result
                Else
                    result = DetectRhac0533(buhinNo, resolvedBuhinNo, "")
                    If result IsNot Nothing Then
                        Return result
                    Else
                        result = DetectRhac0530(buhinNo, resolvedBuhinNo, "")
                        If result IsNot Nothing Then
                            Return result
                        End If
                    End If
                End If
            End If

            'イベントコード(構成再展開時に指定したイベントコード)とINSTL品番とINSTL品番区分で設計ブロックINSTL情報を探索'
            result = DetectShisakuSekkeiBlockInstl(hoyouEventCode, buhinNo, buhinNoKbn)
            'result = DetectShisakuSekkeiBlockInstl(buhinNo, buhinNoKbn)
            If result IsNot Nothing Then
                Return result
            End If


            'If StringUtil.IsEmpty(buhinNoKbn) Then
            '    result = DetectShisakuBuhinEdit(buhinNo)
            '    If result IsNot Nothing Then
            '        Return result
            '    End If
            'End If

            Return HoyouBuhinStructureResult.NewNotExist(buhinNo, buhinNoKbn)
        End Function

        ''' <summary>
        ''' 部品番号を構成する、最新の構成の情報を返す(一括用なのでEBOMも試作も必ず両方見る)
        ''' </summary>
        ''' <param name="buhinNo">部品番号</param>
        ''' <param name="buhinNoKbn">区分</param>
        ''' <returns>構成の情報</returns>
        ''' <remarks></remarks>
        Public Function ComputeEK(ByVal buhinNo As String, ByVal buhinNoKbn As String, Optional ByVal KaihatsuFugo As String = "", Optional ByVal shisakuEventCode As String = "") As HoyouBuhinStructureResult Implements HoyouBuhinDetectLatestStructure.ComputeEK
            If StringUtil.IsEmpty(buhinNo) Then
                Return HoyouBuhinStructureResult.NewNotExist(buhinNo, buhinNoKbn)
            End If

            '2012/02/27 テストデータとして存在したため回避処理を追加'
            If StringUtil.Equals(buhinNo, "1") OrElse StringUtil.Equals(buhinNo, "1111111111") Then
                Return HoyouBuhinStructureResult.NewNotExist(buhinNo, buhinNoKbn)
            End If

            Dim flag552 As Boolean = False
            Dim flag553 As Boolean = False
            Dim flag551 As Boolean = False
            Dim flagShisaku As Boolean = False

            Dim result As HoyouBuhinStructureResult
            Dim resolvedBuhinNo As String = buhinNo

            '12桁以外の場合処理無し。
            If buhinNo Is Nothing OrElse buhinNo.Length < 12 _
                OrElse buhinNo.Length >= 13 Then
                resolvedBuhinNo = buhinNo
            Else
                'まずは552をチェック
                Dim Rhac0552Vo As New Rhac0552Vo
                Rhac0552Vo = makeDao.FindByBuhinRhac0552(buhinNo)
                '次に553をチェック
                Dim Rhac0553Vo As New Rhac0553Vo
                Rhac0553Vo = makeDao.FindByBuhinRhac0553("FM5", buhinNo)
                '最後に551をチェック
                Dim Rhac0551Vo As New Rhac0551Vo
                Rhac0551Vo = makeDao.FindByBuhinRhac0551(buhinNo)
                '全てのテーブルに存在しない場合は、基本Ｆ品番の色コードを＃＃に置き換える。
                If Rhac0552Vo Is Nothing And Rhac0553Vo Is Nothing And Rhac0551Vo Is Nothing Then
                    resolvedBuhinNo = Conv0532HinbanByRule(buhinNo)
                Else
                    resolvedBuhinNo = buhinNo
                End If

            End If

            'まずは５３２をチェック
            '無ければ５３３をチェック
            'さらに無ければ５３０をチェック


            result = DetectRhac0532(buhinNo, resolvedBuhinNo, "")
            If result IsNot Nothing Then
                flag552 = True
            End If
            result = DetectRhac0533(buhinNo, resolvedBuhinNo, "")
            If result IsNot Nothing Then
                flag553 = True
            End If
            result = DetectRhac0530(buhinNo, resolvedBuhinNo, "")
            If result IsNot Nothing Then
                flag551 = True
            End If



            'イベントコード(構成再展開時に指定したイベントコード)とINSTL品番とINSTL品番区分で設計ブロックINSTL情報を探索'
            result = DetectShisakuSekkeiBlockInstl(shisakuEventCode, buhinNo, buhinNoKbn)
            'result = DetectShisakuSekkeiBlockInstl(buhinNo, buhinNoKbn)
            If result IsNot Nothing Then
                flagShisaku = True
            End If

            If flagShisaku Then
                If flag551 OrElse flag552 OrElse flag553 Then
                    Return HoyouBuhinStructureResult.NewShisakuEBOM(result.InstlVo, "")
                Else
                    Return HoyouBuhinStructureResult.NewShisaku(result.InstlVo, "")
                End If
            Else
                If flag551 OrElse flag552 OrElse flag553 Then
                    Return HoyouBuhinStructureResult.NewEBom(resolvedBuhinNo, buhinNo, "")
                Else
                    Return HoyouBuhinStructureResult.NewNotExist(buhinNo, buhinNoKbn)
                End If
            End If

            'If StringUtil.IsEmpty(buhinNoKbn) Then
            '    result = DetectShisakuBuhinEdit(buhinNo)
            '    If result IsNot Nothing Then
            '        Return result
            '    End If
            'End If


        End Function

        ''' <summary>
        ''' 部品番号を構成する、最新の構成の情報を返す
        ''' </summary>
        ''' <param name="buhinNo">部品番号</param>
        ''' <param name="buhinNoKbn">区分</param>
        ''' <param name="IsInstlHinban">INSTL品番を参照する場合、true</param>
        ''' <param name="KaihatsuFugo">開発符号</param>
        ''' <param name="targetTable">検索対象テーブル</param>
        ''' <returns>構成の情報</returns>
        ''' <remarks></remarks>
        Public Function Compute(ByVal buhinNo As String, _
                                ByVal buhinNoKbn As String, _
                                ByVal IsInstlHinban As Boolean, _
                                ByVal KaihatsuFugo As String, _
                                ByVal targetTable As String, _
                                Optional ByVal shisakuEventCode As String = "") As HoyouBuhinStructureResult Implements HoyouBuhinDetectLatestStructure.Compute
            If StringUtil.IsEmpty(buhinNo) Then
                Return HoyouBuhinStructureResult.NewNotExist(buhinNo, buhinNoKbn)
            End If
            'Dim parentVoForBuhinNo As New Rhac0552Vo


            Dim result As HoyouBuhinStructureResult
            If targetTable <> "SHISAKU" Then
                Dim resolvedBuhinNo As String = buhinNo
                If IsInstlHinban Then
                    ''基本Ｆ品番とマッチしなかった部品は＃＃に変換。　22011/2/3　柳沼
                    ''　（色符号付の場合）
                    'Dim BfBuhinNoMatch As New TShisakuSekkeiBlockInstlVo
                    'BfBuhinNoMatch = bfBuhinDao.FindShisakuBlockInstlbfBuhinNo(resolvedBuhinNo)
                    'If BfBuhinNoMatch Is Nothing Then
                    '    resolvedBuhinNo = Conv0532HinbanByRule(buhinNo)
                    'End If

                    '12桁以外の場合処理無し。
                    If buhinNo Is Nothing OrElse buhinNo.Length < 12 _
                        OrElse buhinNo.Length >= 13 Then
                        resolvedBuhinNo = buhinNo
                    Else
                        If targetTable = "0532" Then
                            '552をチェック
                            Dim Rhac0552Vo As New Rhac0552Vo
                            Rhac0552Vo = makeDao.FindByBuhinRhac0552(buhinNo)
                            If Rhac0552Vo Is Nothing Then
                                resolvedBuhinNo = Conv0532HinbanByRule(buhinNo)
                            Else
                                resolvedBuhinNo = buhinNo
                            End If
                        End If
                        If targetTable = "0533" Then
                            '553をチェック
                            'まず利用されている開発符号をチェック
                            Dim Rhac0553Vo As New Rhac0553Vo
                            Rhac0553Vo = makeDao.FindByBuhinRhac0553(KaihatsuFugo, buhinNo)
                            If Rhac0553Vo Is Nothing Then
                                resolvedBuhinNo = Conv0532HinbanByRule(buhinNo)
                            Else
                                resolvedBuhinNo = buhinNo
                            End If
                        End If
                    End If
                End If


                If targetTable = "0532" Then
                    '2012/02/07 追加 親が無い、構成が無い場合はNothingを返す
                    'parentVoForBuhinNo = makeDao.FindLastestRhac0552ByBuhinNoKo(resolvedBuhinNo)
                    'If StringUtil.IsEmpty(parentVoForBuhinNo) Then
                    '    Return Nothing
                    'End If
                    '2012/03/08 構成の存在だけ確認できればよい'
                    'Dim rhac0532Vos As List(Of Rhac0532Vo) = makeDao.FindStructure0552ByBuhinNoOyaAnd0532ForKo(resolvedBuhinNo)
                    'If rhac0532Vos.Count = 0 Then
                    '    Return Nothing
                    'End If
                    result = DetectRhac0532(buhinNo, resolvedBuhinNo, targetTable)
                    If result IsNot Nothing Then
                        Return result
                    Else
                        Return Nothing
                    End If
                End If
                If targetTable = "0533" Then
                    If KaihatsuFugo <> "" Then
                        result = DetectRhac0533(buhinNo, resolvedBuhinNo, targetTable & "-" & KaihatsuFugo)
                    Else
                        result = DetectRhac0533(buhinNo, resolvedBuhinNo, targetTable)
                    End If
                    If result IsNot Nothing Then
                        Return result
                    Else
                        Return Nothing
                    End If
                End If
                'If targetTable = "0530" Then
                '    '2012/02/07 追加 親が無い、構成が無い場合はNothingを返す
                '    Dim rhac0551Vos As List(Of Rhac0551Vo) = makeDao.FindStructure0551ByBuhinNoOya(resolvedBuhinNo)
                '    If rhac0551Vos.Count = 0 Then
                '        Return Nothing
                '    End If
                '    result = DetectRhac0530(buhinNo, resolvedBuhinNo, targetTable)
                '    If result IsNot Nothing Then
                '        Return result
                '    Else
                '        Return Nothing
                '    End If
                'End If
            Else

                If StringUtil.IsEmpty(shisakuEventCode) Then
                    'イベントコード抜きで検索する'
                    result = DetectShisakuSekkeiBlockInstl(shisakuEventCode, buhinNo, buhinNoKbn)
                Else
                    'イベント品番コピーの場合、'
                    '特定のイベントコードで検索する'
                    result = DetectShisakuSekkeiBlockInstlEventCopy(shisakuEventCode, buhinNo, buhinNoKbn)
                End If


                If result IsNot Nothing Then
                    Return result
                End If

                If StringUtil.IsEmpty(buhinNoKbn) Then
                    result = DetectShisakuBuhinEdit(buhinNo)
                    If result IsNot Nothing Then
                        Return result
                    End If
                End If
            End If

            Return Nothing
        End Function

        ''' <summary>
        ''' ルールに従いINSTL品番を通常品番にして返す
        ''' </summary>
        ''' <param name="instlHinban">INSTL品番</param>
        ''' <returns>通常品番</returns>
        ''' <remarks></remarks>
        Private Function Conv0532HinbanByRule(ByVal instlHinban As String) As String

            If instlHinban Is Nothing OrElse instlHinban.Length < 12 _
                OrElse instlHinban.Length >= 13 Then
                Return instlHinban
            End If
            Return instlHinban.Substring(0, 10) & "##" & instlHinban.Substring(12)
        End Function

        '各ＤＢ用の「DetectRhac***」を作成　Ｂｙ柳沼
        Private Function DetectRhac0532(ByVal originalBuhinNo As String, ByVal buhinNo As String, ByVal JikyuUmu As String, ByVal YobidashiMoto As String) As HoyouBuhinStructureResult
            Dim param As New Rhac0532Vo
            param.BuhinNo = buhinNo
            param.ShukeiCode = JikyuUmu
            param.HaisiDate = Rhac0532VoHelper.HaisiDate.NOW_EFFECTIVE_DATE
            If 0 = aRhac0532Dao.CountBy(param) Then
                Return Nothing
            End If

            'Dim param0530 As New Rhac0530Vo
            'param0530.BuhinNo = buhinNo
            'param0530.HaisiDate = Rhac0532VoHelper.HaisiDate.NOW_EFFECTIVE_DATE
            'If 0 = aRhac0530Dao.CountBy(param0530) Then
            '    Return Nothing
            'End If

            'Dim param0533 As New Rhac0533Vo
            'param0533.BuhinNo = buhinNo

            'param0533.HaisiDate = Rhac0532VoHelper.HaisiDate.NOW_EFFECTIVE_DATE
            'If 0 = aRhac0533Dao.CountBy(param0533) Then
            '    Return Nothing
            'End If


            Return HoyouBuhinStructureResult.NewEBom(originalBuhinNo, buhinNo, YobidashiMoto)
        End Function

        '各ＤＢ用の「DetectRhac***」を作成　Ｂｙ柳沼
        Private Function DetectRhac0532(ByVal originalBuhinNo As String, ByVal buhinNo As String, ByVal YobidashiMoto As String) As HoyouBuhinStructureResult
            Dim param As New Rhac0532Vo
            param.BuhinNo = buhinNo
            param.HaisiDate = Rhac0532VoHelper.HaisiDate.NOW_EFFECTIVE_DATE
            If 0 = aRhac0532Dao.CountBy(param) Then
                Return Nothing
            End If

            'Dim param0530 As New Rhac0530Vo
            'param0530.BuhinNo = buhinNo
            'param0530.HaisiDate = Rhac0532VoHelper.HaisiDate.NOW_EFFECTIVE_DATE
            'If 0 = aRhac0530Dao.CountBy(param0530) Then
            '    Return Nothing
            'End If

            'Dim param0533 As New Rhac0533Vo
            'param0533.BuhinNo = buhinNo

            'param0533.HaisiDate = Rhac0532VoHelper.HaisiDate.NOW_EFFECTIVE_DATE
            'If 0 = aRhac0533Dao.CountBy(param0533) Then
            '    Return Nothing
            'End If


            Return HoyouBuhinStructureResult.NewEBom(originalBuhinNo, buhinNo, YobidashiMoto)
        End Function

        Private Function DetectRhac0533(ByVal originalBuhinNo As String, ByVal buhinNo As String, ByVal YobidashiMoto As String) As HoyouBuhinStructureResult
            Dim param As New Rhac0533Vo
            param.BuhinNo = buhinNo
            param.HaisiDate = Rhac0533VoHelper.HaisiDate.NOW_EFFECTIVE_DATE
            If 0 = aRhac0533Dao.CountBy(param) Then
                Return Nothing
            End If
            Return HoyouBuhinStructureResult.NewEBom(originalBuhinNo, buhinNo, YobidashiMoto)
        End Function
        Private Function DetectRhac0530(ByVal originalBuhinNo As String, ByVal buhinNo As String, ByVal YobidashiMoto As String) As HoyouBuhinStructureResult
            Dim param As New Rhac0530Vo
            param.BuhinNo = buhinNo
            '廃止日は見ない。
            'param.HaisiDate = Rhac0530VoHelper.HaisiDate.NOW_EFFECTIVE_DATE
            If 0 = aRhac0530Dao.CountBy(param) Then
                Return Nothing
            End If
            Return HoyouBuhinStructureResult.NewEBom(originalBuhinNo, buhinNo, YobidashiMoto)
        End Function

        Private Function DetectShisakuSekkeiBlockInstl(ByVal buhinNo As String, ByVal buhinNoKbn As String) As HoyouBuhinStructureResult

            Dim param As New TShisakuSekkeiBlockInstlVo
            param.InstlHinban = buhinNo
            ' INSTL品番区分は必須項目だからnullは無い
            param.InstlHinbanKbn = StringUtil.Nvl(buhinNoKbn)
            '2012/02/29 自身のイベントは無視する'
            Dim instlVos3 As List(Of TShisakuSekkeiBlockInstlVo) = makeDao.FindbySekkeiBlockInstlStructure("", buhinNo, buhinNoKbn)

            'Dim instlVos3 As List(Of TShisakuSekkeiBlockInstlVo) = instlDao.FindBy(param)

            Dim instlVos As New List(Of TShisakuSekkeiBlockInstlVo)
            For Each instlVo As TShisakuSekkeiBlockInstlVo In instlVos3

                '以下の処理は同じキーだったら除外している。コメントにしてみる。　2011/03/10　柳沼
                'If IsEqualKey(blockKeyVo, instlVo) Then
                '    Continue For
                'End If

                instlVos.Add(instlVo)
            Next
            '1だと自身だけだから'
            If 2 > instlVos.Count Then
                Return HoyouBuhinStructureResult.NewNotExist(buhinNo, buhinNoKbn)
            End If
            instlVos.Sort(New InstlDescendingUpdated)
            Return HoyouBuhinStructureResult.NewShisaku(instlVos(0))
        End Function

        ''' <summary>
        ''' イベントコードも指定して設計ブロックINSTLを探す
        ''' </summary>
        ''' <param name="buhinNo"></param>
        ''' <param name="buhinNoKbn"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Private Function DetectShisakuSekkeiBlockInstl(ByVal shisakuEventCode As String, ByVal buhinNo As String, ByVal buhinNoKbn As String) As HoyouBuhinStructureResult

            Dim param As New TShisakuSekkeiBlockInstlVo
            param.InstlHinban = buhinNo
            ' INSTL品番区分は必須項目だからnullは無い
            param.InstlHinbanKbn = StringUtil.Nvl(buhinNoKbn)
            param.ShisakuEventCode = shisakuEventCode
            Dim instlVos3 As List(Of TShisakuSekkeiBlockInstlVo) = makeDao.FindbySekkeiBlockInstlStructure(shisakuEventCode, buhinNo, buhinNoKbn)
            Dim instlVos As New List(Of TShisakuSekkeiBlockInstlVo)
            For Each instlVo As TShisakuSekkeiBlockInstlVo In instlVos3
                instlVos.Add(instlVo)
            Next
            If 0 = instlVos.Count Then
                Return Nothing
            End If
            instlVos.Sort(New InstlDescendingUpdated)
            Return HoyouBuhinStructureResult.NewShisaku(instlVos(0))
        End Function

        Private Function DetectShisakuSekkeiBlockInstlEventCopy(ByVal shisakuEventCode As String, ByVal buhinNo As String, ByVal buhinNoKbn As String) As HoyouBuhinStructureResult

            Dim param As New TShisakuSekkeiBlockInstlVo
            param.InstlHinban = buhinNo
            ' INSTL品番区分は必須項目だからnullは無い
            param.InstlHinbanKbn = StringUtil.Nvl(buhinNoKbn)
            param.ShisakuEventCode = shisakuEventCode
            Dim instlVos3 As List(Of TShisakuSekkeiBlockInstlVo) = makeDao.FindbySekkeiBlockInstlStructureEventCopy(shisakuEventCode, buhinNo, buhinNoKbn)
            Dim instlVos As New List(Of TShisakuSekkeiBlockInstlVo)
            For Each instlVo As TShisakuSekkeiBlockInstlVo In instlVos3
                instlVos.Add(instlVo)
            Next
            If 0 = instlVos.Count Then
                Return Nothing
            End If
            instlVos.Sort(New InstlDescendingUpdated)
            Return HoyouBuhinStructureResult.NewShisaku(instlVos(0))
        End Function

        ''' <summary>
        ''' 
        ''' </summary>
        ''' <param name="shisakuEventCode"></param>
        ''' <param name="buhinNo"></param>
        ''' <param name="buhinNoKbn"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Private Function DetectEK(ByVal shisakuEventCode As String, ByVal buhinNo As String, ByVal buhinNoKbn As String) As HoyouBuhinStructureResult

            '部品構成を見に行く'

            Dim param1 As New Rhac0532Vo
            param1.BuhinNo = buhinNo
            param1.HaisiDate = Rhac0532VoHelper.HaisiDate.NOW_EFFECTIVE_DATE
            If 0 = aRhac0532Dao.CountBy(param1) Then
                Return Nothing
            End If
            Dim param2 As New Rhac0533Vo
            param2.BuhinNo = buhinNo
            param2.HaisiDate = Rhac0533VoHelper.HaisiDate.NOW_EFFECTIVE_DATE
            If 0 = aRhac0533Dao.CountBy(param2) Then
                Return Nothing
            End If
            Dim param3 As New Rhac0530Vo
            param3.BuhinNo = buhinNo
            '廃止日は見ない。
            'param.HaisiDate = Rhac0530VoHelper.HaisiDate.NOW_EFFECTIVE_DATE
            If 0 = aRhac0530Dao.CountBy(param3) Then
                Return Nothing
            End If
            'Return HoyouBuhinStructureResult.NewEBom(originalBuhinNo, buhinNo, YobidashiMoto)

            'INSTL品番を見に行く'
            Dim param4 As New TShisakuSekkeiBlockInstlVo
            param4.InstlHinban = buhinNo
            ' INSTL品番区分は必須項目だからnullは無い
            param4.InstlHinbanKbn = StringUtil.Nvl(buhinNoKbn)
            param4.ShisakuEventCode = shisakuEventCode
            Dim instlVos3 As List(Of TShisakuSekkeiBlockInstlVo) = makeDao.FindbySekkeiBlockInstlStructure(shisakuEventCode, buhinNo, buhinNoKbn)
            Dim instlVos As New List(Of TShisakuSekkeiBlockInstlVo)
            For Each instlVo As TShisakuSekkeiBlockInstlVo In instlVos3
                instlVos.Add(instlVo)
            Next
            If 0 = instlVos.Count Then
                Return Nothing
            End If
            instlVos.Sort(New InstlDescendingUpdated)
            Return HoyouBuhinStructureResult.NewShisaku(instlVos(0))
        End Function



        Private Function IsEqualKey(ByVal tantoVo As THoyouSekkeiTantoVo, ByVal instlVo As TShisakuSekkeiBlockInstlVo) As Boolean
            Return tantoVo.HoyouEventCode.Equals(instlVo.ShisakuEventCode) _
                   AndAlso tantoVo.HoyouBukaCode.Equals(instlVo.ShisakuBukaCode) _
                   AndAlso tantoVo.HoyouTanto.Equals(instlVo.ShisakuBlockNo) _
                   AndAlso tantoVo.HoyouTantoKaiteiNo.Equals(instlVo.ShisakuBlockNoKaiteiNo)
        End Function

        ''' <summary>
        ''' 更新日時で降順にする IComparer実装クラス
        ''' </summary>
        ''' <remarks></remarks>
        Private Class InstlDescendingUpdated : Implements IComparer(Of TShisakuSekkeiBlockInstlVo)

            Public Function Compare(ByVal x As ShisakuCommon.Db.EBom.Vo.TShisakuSekkeiBlockInstlVo, ByVal y As ShisakuCommon.Db.EBom.Vo.TShisakuSekkeiBlockInstlVo) As Integer Implements System.Collections.Generic.IComparer(Of ShisakuCommon.Db.EBom.Vo.TShisakuSekkeiBlockInstlVo).Compare
                Dim result As Integer = y.UpdatedDate.CompareTo(x.UpdatedDate)
                If result <> 0 Then
                    Return result
                End If
                Return y.UpdatedTime.CompareTo(x.UpdatedTime)
            End Function
        End Class

        Private Function DetectShisakuBuhinEdit(ByVal buhinNo As String) As HoyouBuhinStructureResult
            Dim param As New THoyouBuhinEditVo
            param.BuhinNo = buhinNo
            Dim instlVos3 As List(Of THoyouBuhinEditVo) = editDao.FindBy(param)
            Dim editVos As New List(Of THoyouBuhinEditVo)

            For Each editVo As THoyouBuhinEditVo In instlVos3
                If IsEqualKey(tantoKeyVo, editVo) Then
                    Continue For
                End If
                editVos.Add(editVo)
            Next
            If 0 = editVos.Count Then
                Return Nothing
            End If
            editVos.Sort(New EditDescendingUpdated)
            Return HoyouBuhinStructureResult.NewShisaku(editVos(0))
        End Function

        Private Function IsEqualKey(ByVal tantoVo As THoyouSekkeiTantoVo, ByVal instlVo As THoyouBuhinEditVo) As Boolean
            Return tantoVo.HoyouEventCode.Equals(instlVo.HoyouEventCode) _
                   AndAlso tantoVo.HoyouBukaCode.Equals(instlVo.HoyouBukaCode) _
                   AndAlso tantoVo.HoyouTanto.Equals(instlVo.HoyouTanto) _
                   AndAlso tantoVo.HoyouTantoKaiteiNo.Equals(instlVo.HoyouTantoKaiteiNo)
        End Function

        ''' <summary>
        ''' 更新日時で降順にする IComparer実装クラス
        ''' </summary>
        ''' <remarks></remarks>
        Private Class EditDescendingUpdated : Implements IComparer(Of THoyouBuhinEditVo)

            Public Function Compare(ByVal x As THoyouBuhinEditVo, ByVal y As THoyouBuhinEditVo) As Integer Implements IComparer _
                                                                                                                   (Of THoyouBuhinEditVo) _
                                                                                                                   .Compare
                Dim result As Integer = y.UpdatedDate.CompareTo(x.UpdatedDate)
                If result <> 0 Then
                    Return result
                End If
                Return y.UpdatedTime.CompareTo(x.UpdatedTime)
            End Function
        End Class

    End Class
End Namespace