Imports EBom.Common
Imports EventSakusei.TehaichoSakusei
Imports ShisakuCommon
Imports ShisakuCommon.Util
Imports ShisakuCommon.Ui
Imports ShisakuCommon.Db.Impl
Imports ShisakuCommon.Db.EBom
Imports ShisakuCommon.Db.EBom.Exclusion
Imports ShisakuCommon.Db.EBom.Dao.Impl
Imports ShisakuCommon.Db.EBom.Dao
Imports ShisakuCommon.Db.EBom.Vo
Imports ShisakuCommon.Ui.Access
Imports ShisakuCommon.Db
Imports EventSakusei.TehaichoMenu.Impl
Imports EventSakusei.TehaichoMenu.Dao
Imports EventSakusei.TehaichoMenu
Imports EventSakusei.TehaichoMenu.Vo

Namespace TehaichoMenu.Logic

    Public Class Frm19TehaichoMenu

        Private ReadOnly aLoginInfo As LoginInfo
        Private ReadOnly MenuImpl As TehaichoMenuDao = New TehaichoMenuDaoImpl
        Private aListVo As TShisakuListcodeVo
        Private aDate As ShisakuDate

        ''' <summary>
        ''' 初期設定
        ''' </summary>
        ''' <param name="aLoginInfo">ログイン情報</param>
        ''' <param name="shisakuEventCode">試作イベントコード</param>
        ''' <param name="listcode">試作リストコード</param>
        ''' <remarks></remarks>
        Public Sub New(ByVal aLoginInfo As LoginInfo, ByVal shisakuEventCode As String, ByVal listcode As String)

            Me.aLoginInfo = aLoginInfo
            aListVo = New TShisakuListcodeVo
            aListVo = MenuImpl.FindByListCode(shisakuEventCode, listcode)

            _ShisakuListCode = aListVo.ShisakuListCode
            _ZenkaiKaiteibi = aListVo.ZenkaiKaiteibi
            _ShisakuListCodeKaiteiNo = aListVo.ShisakuListCodeKaiteiNo
            _ShisakuEventName = aListVo.ShisakuEventName
            _ShisakuKoujiShireiNo = aListVo.ShisakuKoujiShireiNo
            _SaishinChusyutubi = aListVo.SaishinChusyutubi
            _SaishinChusyutujikan = aListVo.SaishinChusyutujikan
            _OldListCode = aListVo.OldListCode
            _Status = aListVo.Status

        End Sub

        ''' <summary>
        ''' エラーチェック
        ''' </summary>
        ''' <param name="shisakuEventCode">イベントコード</param>
        ''' <param name="shisakuListCode">リストコード</param>
        ''' <returns>結果</returns>
        ''' <remarks></remarks>
        Public Function ErrorCheck(ByVal shisakuEventCode As String, ByVal shisakuListCode As String) As Boolean

            Dim errorChecker As New TehaichoErrorCheck()

            Return errorChecker.ErrorCheck(shisakuEventCode, shisakuListCode)
        End Function

        ''' <summary>
        ''' 発注用データ登録をする
        ''' </summary>
        ''' <param name="shisakuEventCode">イベントコード</param>
        ''' <param name="shisakuListCode">リストコード</param>
        ''' <param name="oldListCode">旧リストコード</param>
        ''' <remarks></remarks>
        Public Sub HacchuDateRegister(ByVal shisakuEventCode As String, ByVal shisakuListCode As String, ByVal oldListCode As String)

            Dim KetugoListVo As New List(Of TShisakuTehaiKihonVo)
            Dim ketugoMax As String

            '結合Noを振る対象を取得'
            KetugoListVo = MenuImpl.FindByKetsugoNo(shisakuEventCode, shisakuListCode)

            '結合Noを用意'
            Dim ketugoYear As String = Right(Now.Year.ToString, 2)
            Dim ketugoMonth As String = Right("0" + Now.Month.ToString, 2)

            Dim ketugoNoHeader As String = ketugoYear + ketugoMonth + "-"
            ketugoMax = MenuImpl.FindByMaxKetsugoNo(ketugoNoHeader).KetugouNo

            Dim ketugoindex As Integer = 0

            If Not ketugoMax Is Nothing Then
                ketugoindex = Integer.Parse(Right(ketugoMax, 7))
            Else
                ketugoindex = 0
            End If


            Dim ketugoNoBody As String
            Dim ketugoNo As String

            '結合No付与'
            For index As Integer = 0 To KetugoListVo.Count - 1
                ketugoNoBody = Right("000000" + (ketugoindex + index + 1).ToString, 7)
                ketugoNo = ketugoNoHeader + ketugoNoBody
                MenuImpl.UpdateKetsugoNo(KetugoListVo(index), ketugoNo)
                ketugoNo = ""
            Next

            '旧リストコード更新'
            MenuImpl.UpdateByOldListCode(shisakuEventCode, shisakuListCode, oldListCode, True)
            'ステータスの更新'
            'MenuImpl.UpdateByStatus(shisakuEventCode, shisakuListCode, "62")
        End Sub

        ''' <summary>
        ''' 訂正通知の処理
        ''' </summary>
        ''' <param name="shisakuEventCode">イベントコード</param>
        ''' <param name="shisakuListCode">リストコード</param>
        ''' <param name="kaiteiNo">取得したい改訂No</param>
        ''' <remarks></remarks>
        Public Sub TeiseiTsuti(ByVal shisakuEventCode As String, ByVal shisakuListCode As String, ByVal kaiteiNo As String)
            Dim teiseiImpl As TeiseiTsuchiDao = New TeiseiTsuchiDaoImpl
            '訂正通知を削除する(最新の改訂のみ)'

            teiseiImpl.DeleteByTeiseiKihon(shisakuEventCode, shisakuListCode, kaiteiNo)
            teiseiImpl.DeleteByTeiseiGousya(shisakuEventCode, shisakuListCode, kaiteiNo)

            Dim NewKaiteiNo As String = Right("000" + (Integer.Parse(kaiteiNo) - 1).ToString, 3)


            '最新の改訂No'
            Dim aTeiseiVo As New List(Of TShisakuTehaiKihonVo)
            Dim aTeiseiGousyaVo As New List(Of TShisakuTehaiGousyaVo)
            '直前の改訂No'
            Dim aTeiseiBeforeVo As New List(Of TShisakuTehaiKihonVo)
            Dim aTeiseiBeforeGousyaVo As New List(Of TShisakuTehaiGousyaVo)

            '最新の基本・号車情報リストを取得する'
            aTeiseiVo = MenuImpl.FindByListKihonTeisei(shisakuEventCode, shisakuListCode)
            'aTeiseiGousyaVo = teiseiImpl.FindByTehaiGousya(shisakuEventCode, shisakuListCode)

            '直前の改訂の基本・号車情報を取得する'
            aTeiseiBeforeVo = teiseiImpl.FindByBeforeTehaiKihon(shisakuEventCode, shisakuListCode, NewKaiteiNo)
            'aTeiseiBeforeGousyaVo = teiseiImpl.FindByBeforeTehaiGousya(shisakuEventCode, shisakuListCode)

            Dim checkKihonVo As New TShisakuTehaiKihonVo
            Dim checkGousyaVoList As New List(Of TShisakuTehaiGousyaVo)

            ''↓↓2015/01/12 メタル改訂抽出追加_z) (TES)劉 CHG BEGIN
            '直前の改訂を基準にチェックする'
            For index As Integer = 0 To aTeiseiBeforeVo.Count - 1
                '最新に同一のプライマリキーが存在しているかチェック'

                '最新の手配基本情報を取得'
                'checkKihonVo = teiseiImpl.FindByNewTehaiKihon(aTeiseiBeforeVo(index).ShisakuEventCode, _
                '                                              aTeiseiBeforeVo(index).ShisakuListCode, _
                '                                              aTeiseiBeforeVo(index).ShisakuBukaCode, _
                '                                              aTeiseiBeforeVo(index).ShisakuBlockNo, _
                '                                              aTeiseiBeforeVo(index).GyouId)
                checkKihonVo = teiseiImpl.FindByNewTehaiKihon(aTeiseiBeforeVo(index).ShisakuEventCode, _
                                                              aTeiseiBeforeVo(index).ShisakuListCode, _
                                                              aTeiseiBeforeVo(index).ShisakuBukaCode, _
                                                              aTeiseiBeforeVo(index).ShisakuBlockNo, _
                                                              aTeiseiBeforeVo(index).Level, _
                                                              aTeiseiBeforeVo(index).BuhinNo)
                'aTeiseiBeforeVo(index).ShisakuBlockNo, _
                '  aTeiseiBeforeVo(index).ShisakuListCodeKaiteiNo, _
                '  aTeiseiBeforeVo(index).BuhinNoHyoujiJun)
                ''↑↑2015/01/12 メタル改訂抽出追加_z) (TES)劉 CHG END

                If Not checkKihonVo Is Nothing Then
                    'あればその行は削除されていない'
                    '最新号車情報を取得しておく'
                    checkGousyaVoList = teiseiImpl.FindByNewTehaiGousya(checkKihonVo.ShisakuEventCode, _
                                                                        checkKihonVo.ShisakuListCode, _
                                                                        checkKihonVo.ShisakuListCodeKaiteiNo, _
                                                                        checkKihonVo.ShisakuBukaCode, _
                                                                        checkKihonVo.ShisakuBlockNo, _
                                                                        checkKihonVo.BuhinNoHyoujiJun)

                    '何故か-1のやつがいる'
                    If checkKihonVo.TotalInsuSuryo < 0 Then
                        checkKihonVo.TotalInsuSuryo = 0
                    End If

                    '手配記号が同じかチェック'
                    If StringUtil.Equals(aTeiseiBeforeVo(index).TehaiKigou, checkKihonVo.TehaiKigou) Then
                        '同一なら手配記号は変更されていない'

                        '手配記号と作成、更新部分を除いた全てが同一かチェック'
                        If CheckByAll(checkKihonVo, aTeiseiBeforeVo(index)) Then
                            '全件一致なら変更無し'

                            'ここで員数チェック'
                            InsuCheck(checkKihonVo, aTeiseiBeforeVo(index), teiseiImpl)

                        Else
                            '一致しないなら変更あり'
                            teiseiImpl.InsetByTeiseiKihonDel(checkKihonVo.ShisakuEventCode, _
                                  checkKihonVo.ShisakuListCode, _
                                  checkKihonVo.ShisakuListCodeKaiteiNo, _
                                  aTeiseiBeforeVo(index), "4")

                            teiseiImpl.InsetByTeiseiKihon(checkKihonVo, "3")

                            Dim beforeGousyaVoList As New List(Of TShisakuTehaiGousyaVo)

                            beforeGousyaVoList = teiseiImpl.FindByBeforeGousya(aTeiseiBeforeVo(index).ShisakuEventCode, _
                                                                               aTeiseiBeforeVo(index).ShisakuListCode, _
                                                                               aTeiseiBeforeVo(index).ShisakuBukaCode, _
                                                                               aTeiseiBeforeVo(index).ShisakuBlockNo, _
                                                                               aTeiseiBeforeVo(index).BuhinNoHyoujiJun, _
                                                                               NewKaiteiNo)

                            teiseiImpl.InsetByTeiseiGousya(checkGousyaVoList, "3")

                            'リストコードの改訂Noを最新のものに変更する'
                            For Each bgVo As TShisakuTehaiGousyaVo In beforeGousyaVoList
                                bgVo.ShisakuListCodeKaiteiNo = checkKihonVo.ShisakuListCodeKaiteiNo
                            Next

                            teiseiImpl.InsetByTeiseiGousya(beforeGousyaVoList, "4")
                        End If

                    ElseIf StringUtil.Equals(aTeiseiBeforeVo(index).TehaiKigou, "F") Then
                        '一致しない場合で直前の手配記号がFなら手配記号が変更されている'
                        teiseiImpl.InsetByTeiseiKihon(checkKihonVo, "5")

                        teiseiImpl.InsetByTeiseiGousya(checkGousyaVoList, "5")

                    End If

                Else
                    'なければその行は削除されている'
                    '改訂Noは最新にする'
                    teiseiImpl.InsetByTeiseiKihonDel(aTeiseiBeforeVo(index).ShisakuEventCode, aTeiseiBeforeVo(index).ShisakuListCode, _
                                                     kaiteiNo, aTeiseiBeforeVo(index), "2")

                    '前回の号車のリストを取得する'
                    Dim beforeGousyaVoList As New List(Of TShisakuTehaiGousyaVo)

                    beforeGousyaVoList = teiseiImpl.FindByBeforeGousya(aTeiseiBeforeVo(index).ShisakuEventCode, _
                                                                       aTeiseiBeforeVo(index).ShisakuListCode, _
                                                                       aTeiseiBeforeVo(index).ShisakuBukaCode, _
                                                                       aTeiseiBeforeVo(index).ShisakuBlockNo, _
                                                                       aTeiseiBeforeVo(index).BuhinNoHyoujiJun, _
                                                                       NewKaiteiNo)

                    For Each bgVo As TShisakuTehaiGousyaVo In beforeGousyaVoList
                        '改訂Noを最新にする'
                        teiseiImpl.InsetByTeiseiGousyaDel(aTeiseiBeforeVo(index).ShisakuEventCode, _
                                                          aTeiseiBeforeVo(index).ShisakuListCode, _
                                                          kaiteiNo, _
                                                          bgVo, _
                                                          "2")
                    Next
                End If
            Next


            '最新の改訂を基準にチェックする'
            For index As Integer = 0 To aTeiseiVo.Count - 1
                '直前の改訂に同一のプライマリキーが存在しているかチェック '

                ''↓↓2015/01/12 メタル改訂抽出追加_z) (TES)劉 CHG BEGIN
                'If teiseiImpl.CheckByBefore(aTeiseiVo(index).ShisakuEventCode, _
                '                            aTeiseiVo(index).ShisakuListCode, _
                '                            aTeiseiVo(index).ShisakuBukaCode, _
                '                            aTeiseiVo(index).ShisakuBlockNo, _
                '                            aTeiseiVo(index).GyouId, _
                '                            NewKaiteiNo) Then
                If teiseiImpl.CheckByBefore(aTeiseiVo(index).ShisakuEventCode, _
                                            aTeiseiVo(index).ShisakuListCode, _
                                            aTeiseiVo(index).ShisakuBukaCode, _
                                            aTeiseiVo(index).ShisakuBlockNo, _
                                            aTeiseiVo(index).Level, _
                                            aTeiseiVo(index).BuhinNo, _
                                            NewKaiteiNo) Then
                    ''↑↑2015/01/12 メタル改訂抽出追加_z) (TES)劉 CHG END

                    'あれば上の処理で対処されているのでここは何もしない'

                Else

                    'なければその行は追加されている'
                    '最新号車情報を取得しておく'
                    checkGousyaVoList = teiseiImpl.FindByNewTehaiGousya(aTeiseiVo(index).ShisakuEventCode, _
                                                                    aTeiseiVo(index).ShisakuListCode, _
                                                                    aTeiseiVo(index).ShisakuListCodeKaiteiNo, _
                                                                    aTeiseiVo(index).ShisakuBukaCode, _
                                                                    aTeiseiVo(index).ShisakuBlockNo, _
                                                                    aTeiseiVo(index).BuhinNoHyoujiJun)

                    Dim strFlg As String = "1" '追加扱い
                    '変化点が削(3)か？
                    If StringUtil.Equals(aTeiseiVo(index).Henkaten, "3") Then
                        strFlg = "2" '削除扱い
                    End If

                    teiseiImpl.InsetByTeiseiKihon(aTeiseiVo(index), strFlg)

                    teiseiImpl.InsetByTeiseiGousya(checkGousyaVoList, strFlg)

                End If
            Next


            teiseiImpl.UpdateByTeiseiChusyutubi(shisakuEventCode, shisakuListCode)

        End Sub

        ''' <summary>
        ''' 員数チェック
        ''' </summary>
        ''' <param name="kihonVo">手配基本情報</param>
        ''' <param name="teiseiImpl"></param>
        ''' <remarks></remarks>
        Private Sub InsuCheck(ByVal kihonVo As TShisakuTehaiKihonVo, _
                              ByVal bKihonVo As TShisakuTehaiKihonVo, _
                              ByVal teiseiImpl As TeiseiTsuchiDao)

            Dim NewGousyaVoList As New List(Of TShisakuTehaiGousyaVo)
            Dim BGousyaVoList As New List(Of TShisakuTehaiGousyaVo)

            NewGousyaVoList = teiseiImpl.FindByNewTehaiGousya(kihonVo.ShisakuEventCode, _
                                                              kihonVo.ShisakuListCode, _
                                                              kihonVo.ShisakuListCodeKaiteiNo, _
                                                              kihonVo.ShisakuBukaCode, _
                                                              kihonVo.ShisakuBlockNo, _
                                                              kihonVo.BuhinNoHyoujiJun)

            BGousyaVoList = teiseiImpl.FindByBeforeGousya(bKihonVo.ShisakuEventCode, _
                                                          bKihonVo.ShisakuListCode, _
                                                          bKihonVo.ShisakuBukaCode, _
                                                          bKihonVo.ShisakuBlockNo, _
                                                          bKihonVo.BuhinNoHyoujiJun, _
                                                          bKihonVo.ShisakuListCodeKaiteiNo)


            '員数の合計'
            Dim NewTotalInsu As Integer = 0
            For Each konkaiVo As TShisakuTehaiGousyaVo In NewGousyaVoList
                If konkaiVo.InsuSuryo > 0 Then
                    NewTotalInsu = NewTotalInsu + konkaiVo.InsuSuryo
                End If
            Next

            '前回員数の合計'
            Dim btotalInsu As Integer = 0
            For Each zenkaiVo As TShisakuTehaiGousyaVo In BGousyaVoList
                If zenkaiVo.InsuSuryo > 0 Then
                    btotalInsu = btotalInsu + zenkaiVo.InsuSuryo
                End If
            Next


            'もし員数がマイナスだったら比較しない'

            '部品の員数比較'
            If NewTotalInsu < btotalInsu Then
                '員数減'
                '追加'
                teiseiImpl.InsetByTeiseiKihon(kihonVo, "1")

                '削除'
                teiseiImpl.InsetByTeiseiKihonDel(kihonVo.ShisakuEventCode, _
                                                 kihonVo.ShisakuListCode, _
                                                 kihonVo.ShisakuListCodeKaiteiNo, _
                                                 bKihonVo, _
                                                 "2")

                teiseiImpl.InsetByTeiseiGousya(NewGousyaVoList, "1")


                For Each bVo As TShisakuTehaiGousyaVo In BGousyaVoList
                    teiseiImpl.InsetByTeiseiGousyaDel(kihonVo.ShisakuEventCode, _
                                                      kihonVo.ShisakuListCode, _
                                                      kihonVo.ShisakuListCodeKaiteiNo, _
                                                      bVo, _
                                                      "2")

                Next

            ElseIf NewTotalInsu > btotalInsu Then
                '員数増'
                '追加'
                teiseiImpl.InsetByTeiseiKihon(kihonVo, "1")
                teiseiImpl.InsetByTeiseiKihonDel(kihonVo.ShisakuEventCode, _
                                                 kihonVo.ShisakuListCode, _
                                                 kihonVo.ShisakuListCodeKaiteiNo, _
                                                 bKihonVo, _
                                                 "2")

                Dim insusa As Integer = 0
                '員数差を用意する'
                For kindex As Integer = 0 To NewGousyaVoList.Count - 1
                    Dim a As Integer = 0
                    Dim b As Integer = 0
                    If BGousyaVoList.Count > 0 Then
                        For bindex As Integer = 0 To BGousyaVoList.Count - 1
                            If NewGousyaVoList(kindex).ShisakuGousyaHyoujiJun = BGousyaVoList(bindex).ShisakuGousyaHyoujiJun Then
                                insusa = 0
                                'マイナスは0扱いにする'
                                If NewGousyaVoList(kindex).InsuSuryo > 0 Then
                                    a = NewGousyaVoList(kindex).InsuSuryo
                                End If
                                If BGousyaVoList(bindex).InsuSuryo > 0 Then
                                    b = BGousyaVoList(bindex).InsuSuryo
                                End If
                                insusa = a - b
                                Exit For
                            Else
                                insusa = NewGousyaVoList(kindex).InsuSuryo
                            End If
                        Next
                    Else
                        '比較対象がいないから'
                        If NewGousyaVoList(kindex).InsuSuryo > 0 Then
                            insusa = NewGousyaVoList(kindex).InsuSuryo
                        End If
                    End If

                    teiseiImpl.InsertByTeiseiGousyaAdd(NewGousyaVoList(kindex).ShisakuEventCode, _
                                                       NewGousyaVoList(kindex).ShisakuListCode, _
                                                       NewGousyaVoList(kindex).ShisakuListCodeKaiteiNo, _
                                                       NewGousyaVoList(kindex).ShisakuBukaCode, _
                                                       NewGousyaVoList(kindex).ShisakuBlockNo, _
                                                       NewGousyaVoList(kindex).BuhinNoHyoujiJun, _
                                                       "1", _
                                                       NewGousyaVoList(kindex).GyouId, _
                                                       NewGousyaVoList(kindex).ShisakuGousyaHyoujiJun, _
                                                       NewGousyaVoList(kindex).ShisakuGousya, _
                                                       insusa, _
                                                       NewGousyaVoList(kindex).MNounyuShijibi, _
                                                       NewGousyaVoList(kindex).TNounyuShijibi)
                Next

                For Each BVo As TShisakuTehaiGousyaVo In BGousyaVoList
                    teiseiImpl.InsetByTeiseiGousyaDel(kihonVo.ShisakuEventCode, _
                                                      kihonVo.ShisakuListCode, _
                                                      kihonVo.ShisakuListCodeKaiteiNo, _
                                                      BVo, _
                                                      "2")
                Next

            ElseIf NewTotalInsu = btotalInsu Then
                '変更無し'
                '追加'

                '合計員数に変更が無くても号車ごとで見ると差異がある場合があるのでチェックする'
                Dim changeFlag As Boolean = False

                'パターン１号車の総数に差異があるパターン'
                If Not NewGousyaVoList.Count = BGousyaVoList.Count Then
                    changeFlag = True
                Else
                    'パターン２号車の総数はあっているが適用のある号車が異なっているパターン'
                    For index As Integer = 0 To NewGousyaVoList.Count - 1
                        If Not NewGousyaVoList(index).ShisakuGousyaHyoujiJun = BGousyaVoList(index).ShisakuGousyaHyoujiJun Then
                            changeFlag = True
                            Exit For
                        Else
                            'パターン３号車の適用もあっているが員数に差があるパターン'
                            Dim newInsu As Integer = NewGousyaVoList(index).InsuSuryo
                            Dim bInsu As Integer = BGousyaVoList(index).InsuSuryo

                            If NewGousyaVoList(index).InsuSuryo < 0 Then
                                newInsu = 0
                            End If
                            If BGousyaVoList(index).InsuSuryo < 0 Then
                                bInsu = 0
                            End If

                            If Not newInsu = bInsu Then
                                changeFlag = True
                                Exit For
                            End If
                        End If
                    Next
                End If

                '変更箇所があるかないか'
                If changeFlag Then
                    '変更箇所があれば変更前、変更後で追加'

                    '追加(変更後)'
                    teiseiImpl.InsetByTeiseiKihon(kihonVo, "4")
                    teiseiImpl.InsetByTeiseiKihonDel(kihonVo.ShisakuEventCode, _
                                                     kihonVo.ShisakuListCode, _
                                                     kihonVo.ShisakuListCodeKaiteiNo, _
                                                     bKihonVo, _
                                                     "3")

                    '追加処理(変更後)'
                    teiseiImpl.InsetByTeiseiGousya(NewGousyaVoList, "4")


                    '変更前'

                    For Each bVo As TShisakuTehaiGousyaVo In BGousyaVoList
                        teiseiImpl.InsetByTeiseiGousyaDel(kihonVo.ShisakuEventCode, _
                                                          kihonVo.ShisakuListCode, _
                                                          kihonVo.ShisakuListCodeKaiteiNo, _
                                                          bVo, _
                                                          "3")
                    Next

                Else
                    '変更箇所が無ければ変更無しで追加'

                    teiseiImpl.InsetByTeiseiKihon(kihonVo, "")
                    teiseiImpl.InsetByTeiseiGousya(NewGousyaVoList, "")

                End If
            End If

        End Sub

        ''' <summary>
        ''' 新調達への転送
        ''' </summary>
        ''' <param name="ListCodeVo">リストコード情報</param>
        ''' <remarks></remarks>
        Public Sub Tensou(ByVal ListCodeVo As TShisakuListcodeVo)
            '履歴を更新する'
            MenuImpl.UpdateByRireki(ListCodeVo.ShisakuEventCode, ListCodeVo.ShisakuListCode)
            'ステータスを更新する'
            MenuImpl.UpdateByStatus(ListCodeVo.ShisakuEventCode, ListCodeVo.ShisakuListCode, "63")
            '改訂繰上げをする'
            '改訂No000のときは何もさせない'

            KaiteiUp(ListCodeVo)
        End Sub

        ''' <summary>
        ''' 改訂繰上げ
        ''' </summary>
        ''' <param name="ListCodeVo">リストコード情報</param>
        ''' <remarks></remarks>
        Private Sub KaiteiUp(ByVal ListCodeVo As TShisakuListcodeVo)
            '改訂繰上げをする'
            Dim KaiteiImpl As KaiteiUpDao = New KaiteiUpDaoImpl
            'Dim BukaCode As New List(Of TShisakuTehaiKihonVo)
            'BukaCode = MenuImpl.FindByListKihon(ListCodeVo.ShisakuEventCode, ListCodeVo.ShisakuListCode)


            If Not StringUtil.Equals(ListCodeVo.ShisakuListCodeKaiteiNo, "000") Then
                '試作手配改訂ブロック情報の中身が無い(いつ作るのか不明)'
                '最新のブロックNoの改訂Noを取得'
                Dim sekkeiBlockVoList As New List(Of TShisakuSekkeiBlockVo)
                sekkeiBlockVoList = KaiteiImpl.FindBySekkeiBlock(ListCodeVo.ShisakuEventCode)

                '無ければ追加あれば更新で試作手配改訂抽出ブロック情報を更新'
                KaiteiImpl.InsertByKaiteiBlock(ListCodeVo.ShisakuEventCode, _
                                               ListCodeVo.ShisakuListCode, _
                                               ListCodeVo.ShisakuListCodeKaiteiNo, _
                                               sekkeiBlockVoList)
            End If

            '改訂があがったリストコードを追加'
            KaiteiImpl.InsertByListKaiteiNo(ListCodeVo)

            Dim KihonVo As New List(Of TShisakuTehaiKihonVo)
            Dim GousyaVo As New List(Of TShisakuTehaiGousyaVo)

            KihonVo = KaiteiImpl.FindByTehaiKihon(ListCodeVo.ShisakuEventCode, ListCodeVo.ShisakuListCode)
            GousyaVo = KaiteiImpl.FindByTehaiGousya(ListCodeVo.ShisakuEventCode, ListCodeVo.ShisakuListCode)


            '手配基本情報を追加'
            KaiteiImpl.InsertByTehaiKihonKaiteiNo(KihonVo)
            '手配号車情報を追加'
            KaiteiImpl.InsertByTehaiGousyaKaiteiNo(GousyaVo)


            '試作手配出図実績情報の改訂ＵＰ
            Dim TehaiShutuzuJisekiVo As New List(Of TShisakuTehaiShutuzuJisekiVo)
            TehaiShutuzuJisekiVo = KaiteiImpl.FindByTehaiShutuzuJiseki(ListCodeVo.ShisakuEventCode, ListCodeVo.ShisakuListCode)
            KaiteiImpl.InsertByTehaiShutuzuJisekiKaiteiNo(TehaiShutuzuJisekiVo, ListCodeVo.ShisakuListCodeKaiteiNo)
            '試作手配出図実績手入力情報の改訂ＵＰ
            Dim TehaiShutuzuJisekiInputVo As New List(Of TShisakuTehaiShutuzuJisekiInputVo)
            TehaiShutuzuJisekiInputVo = KaiteiImpl.FindByTehaiShutuzuJisekiInput(ListCodeVo.ShisakuEventCode, ListCodeVo.ShisakuListCode)
            KaiteiImpl.InsertByTehaiShutuzuJisekiInputKaiteiNo(TehaiShutuzuJisekiInputVo, ListCodeVo.ShisakuListCodeKaiteiNo)
            '試作手配出図織込情報の改訂ＵＰ
            Dim TehaiShutuzuOrikomiVo As New List(Of TShisakuTehaiShutuzuOrikomiVo)
            TehaiShutuzuOrikomiVo = KaiteiImpl.FindByTehaiShutuzuOrikomi(ListCodeVo.ShisakuEventCode, ListCodeVo.ShisakuListCode)
            KaiteiImpl.InsertByTehaiShutuzuOrikomiKaiteiNo(TehaiShutuzuOrikomiVo, ListCodeVo.ShisakuListCodeKaiteiNo)
            '試作手配帳情報（号車グループ情報）の改訂ＵＰ
            Dim TehaiGousyaGroupVo As New List(Of TShisakuTehaiGousyaGroupVo)
            TehaiGousyaGroupVo = KaiteiImpl.FindByTehaiGousyaGroup(ListCodeVo.ShisakuEventCode, ListCodeVo.ShisakuListCode)
            KaiteiImpl.InsertByTehaiGousyaGroupKaiteiNo(TehaiGousyaGroupVo, ListCodeVo.ShisakuListCodeKaiteiNo)

        End Sub

        ''' <summary>
        ''' 改訂抽出
        ''' </summary>
        ''' <param name="shisakuEventCode">イベントコード</param>
        ''' <param name="shisakuListCode">リストコード</param>
        ''' <param name="shisakuListCodeKaiteiNo">リストコード改訂No</param>
        ''' <remarks></remarks>
        Public Sub KaiteiChushutu(ByVal shisakuEventCode As String, ByVal shisakuListCode As String, ByVal shisakuListCodeKaiteiNo As String, ByVal zenkaiShisakuListCodeKaiteiNo As String)
            Dim kaitei As New TehaichoKaiteiChusyutu(shisakuEventCode, shisakuListCode, shisakuListCodeKaiteiNo, False)
            kaitei.KaiteiCyushutu()
        End Sub

        ''' <summary>
        ''' 改訂抽出
        ''' </summary>
        ''' <param name="shisakuEventCode">イベントコード</param>
        ''' <param name="shisakuListCode">リストコード</param>
        ''' <param name="shisakuListCodeKaiteiNo">リストコード改訂No</param>
        ''' <remarks></remarks>
        Public Sub KaiteiChushutuAuto(ByVal shisakuEventCode As String, ByVal shisakuListCode As String, ByVal shisakuListCodeKaiteiNo As String, ByVal zenkaiShisakuListCodeKaiteiNo As String)
            Dim kaitei As New TehaichoKaiteiChusyutu(shisakuEventCode, shisakuListCode, shisakuListCodeKaiteiNo, False)
            kaitei.KaiteiCyushutuAuto()
        End Sub

        ''' <summary>
        ''' 改訂抽出　ベース
        ''' </summary>
        ''' <param name="shisakuEventCode">イベントコード</param>
        ''' <param name="shisakuListCode">リストコード</param>
        ''' <param name="shisakuListCodeKaiteiNo">リストコード改訂No</param>
        ''' <remarks></remarks>
        Public Sub KaiteiChushutuBase(ByVal shisakuEventCode As String, ByVal shisakuListCode As String, ByVal shisakuListCodeKaiteiNo As String, ByVal zenkaiShisakuListCodeKaiteiNo As String)
            Dim kaitei As New TehaichoKaiteiChusyutu(shisakuEventCode, shisakuListCode, shisakuListCodeKaiteiNo, True)
            kaitei.KaiteiCyushutu()
        End Sub

        ''' <summary>
        ''' エラーがあったときにステータスを元に戻す
        ''' </summary>
        ''' <param name="shisakuEventCode">イベントコード</param>
        ''' <param name="shisakuListCode">リストコード</param>
        ''' <param name="status">ステータス</param>
        ''' <remarks></remarks>
        Public Sub BackStatus(ByVal shisakuEventCode As String, ByVal shisakuListCode As String, ByVal status As String)
            Dim impl As TehaichoMenuDao = New TehaichoMenuDaoImpl
            impl.UpdateByStatus(shisakuEventCode, shisakuListCode, status)
        End Sub

        ''' <summary>
        ''' 手配記号以外の項目が同一かチェック
        ''' </summary>
        ''' <param name="kihonVo">最新の手配基本情報</param>
        ''' <param name="beforeVo">指定された改訂Noの手配基本情報</param>
        ''' <returns>全件同一ならTrue</returns>
        ''' <remarks></remarks>
        Private Function CheckByAll(ByVal kihonVo As TShisakuTehaiKihonVo, ByVal beforeVo As TShisakuTehaiKihonVo) As Boolean

            '履歴'
            If Not StringUtil.Equals(kihonVo.Rireki, beforeVo.Rireki) Then
                Return False
            End If
            '行ID'
            If Not StringUtil.Equals(kihonVo.GyouId, beforeVo.GyouId) Then
                Return False
            End If
            '専用マーク'
            If Not StringUtil.Equals(kihonVo.SenyouMark, beforeVo.SenyouMark) Then
                Return False
            End If
            'レベル'
            If Not StringUtil.Equals(kihonVo.Level, beforeVo.Level) Then
                Return False
            End If
            'ユニット区分'
            If Not StringUtil.Equals(kihonVo.UnitKbn, beforeVo.UnitKbn) Then
                Return False
            End If
            '部品番号'
            If Not StringUtil.Equals(kihonVo.BuhinNo.Trim, beforeVo.BuhinNo.Trim) Then
                Return False
            End If
            '部品番号試作区分'
            If Not StringUtil.Equals(kihonVo.BuhinNoKbn, beforeVo.BuhinNoKbn) Then
                Return False
            End If
            '部品番号改訂No'
            If Not StringUtil.Equals(kihonVo.BuhinNoKaiteiNo, beforeVo.BuhinNoKaiteiNo) Then
                Return False
            End If
            '枝番'
            If Not StringUtil.Equals(kihonVo.EdaBan, beforeVo.EdaBan) Then
                Return False
            End If
            '部品名称'
            If Not StringUtil.Equals(kihonVo.BuhinName.Trim, beforeVo.BuhinName.Trim) Then
                Return False
            End If
            '集計コード'
            If Not StringUtil.Equals(kihonVo.ShukeiCode, beforeVo.ShukeiCode) Then
                Return False
            End If
            '現調区分'
            If Not StringUtil.Equals(kihonVo.GencyoCkdKbn, beforeVo.GencyoCkdKbn) Then
                Return False
            End If
            '購担'
            If Not StringUtil.Equals(kihonVo.Koutan, beforeVo.Koutan) Then
                Return False
            End If
            '取引先コード'
            If Not StringUtil.Equals(kihonVo.TorihikisakiCode, beforeVo.TorihikisakiCode) Then
                Return False
            End If
            '納場'
            If Not StringUtil.Equals(kihonVo.Nouba, beforeVo.Nouba) Then
                Return False
            End If
            '供給セクション'
            If Not StringUtil.Equals(kihonVo.KyoukuSection, beforeVo.KyoukuSection) Then
                Return False
            End If
            '納入指示日'
            If Not kihonVo.NounyuShijibi = beforeVo.NounyuShijibi Then
                Return False
            End If
            '合計員数数量'

            If kihonVo.TotalInsuSuryo < 0 Then
                kihonVo.TotalInsuSuryo = 0
            End If
            If beforeVo.TotalInsuSuryo < 0 Then
                beforeVo.TotalInsuSuryo = 0
            End If

            If Not kihonVo.TotalInsuSuryo = beforeVo.TotalInsuSuryo Then
                Return False
            End If


            '再使用不可'
            If Not StringUtil.Equals(kihonVo.Saishiyoufuka, beforeVo.Saishiyoufuka) Then
                Return False
            End If

            ' '
            If Not StringUtil.Equals(kihonVo.GousyaHachuTenkaiFlg, beforeVo.GousyaHachuTenkaiFlg) Then
                Return False
            End If
            If Not StringUtil.Equals(kihonVo.GousyaHachuTenkaiUnitKbn, beforeVo.GousyaHachuTenkaiUnitKbn) Then
                Return False
            End If
            '出図予定日'
            If Not kihonVo.ShutuzuYoteiDate = beforeVo.ShutuzuYoteiDate Then
                Return False
            End If
            If Not StringUtil.Equals(kihonVo.ShutuzuJisekiDate, beforeVo.ShutuzuJisekiDate) Then
                Return False
            End If
            If Not StringUtil.Equals(kihonVo.ShutuzuJisekiKaiteiNo, beforeVo.ShutuzuJisekiKaiteiNo) Then
                Return False
            End If
            '図面設通No'
            If Not StringUtil.Equals(kihonVo.ShutuzuJisekiStsrDhstba, beforeVo.ShutuzuJisekiStsrDhstba) Then
                Return False
            End If
            If Not StringUtil.Equals(kihonVo.SaisyuSetsuhenDate, beforeVo.SaisyuSetsuhenDate) Then
                Return False
            End If
            If Not StringUtil.Equals(kihonVo.SaisyuSetsuhenKaiteiNo, beforeVo.SaisyuSetsuhenKaiteiNo) Then
                Return False
            End If
            If Not StringUtil.Equals(kihonVo.StsrDhstba, beforeVo.StsrDhstba) Then
                Return False
            End If
            '材質規格１'
            If Not StringUtil.Equals(kihonVo.ZaishituKikaku1, beforeVo.ZaishituKikaku1) Then
                Return False
            End If
            '材質規格２'
            If Not StringUtil.Equals(kihonVo.ZaishituKikaku2, beforeVo.ZaishituKikaku2) Then
                Return False
            End If
            '材質規格３'
            If Not StringUtil.Equals(kihonVo.ZaishituKikaku3, beforeVo.ZaishituKikaku3) Then
                Return False
            End If
            '材質メッキ'
            If Not StringUtil.Equals(kihonVo.ZaishituMekki, beforeVo.ZaishituMekki) Then
                Return False
            End If

            If Not StringUtil.Equals(kihonVo.TsukurikataSeisaku, beforeVo.TsukurikataSeisaku) Then
                Return False
            End If
            If Not StringUtil.Equals(kihonVo.TsukurikataKatashiyou1, beforeVo.TsukurikataKatashiyou1) Then
                Return False
            End If
            If Not StringUtil.Equals(kihonVo.TsukurikataKatashiyou2, beforeVo.TsukurikataKatashiyou2) Then
                Return False
            End If
            If Not StringUtil.Equals(kihonVo.TsukurikataKatashiyou3, beforeVo.TsukurikataKatashiyou3) Then
                Return False
            End If
            If Not StringUtil.Equals(kihonVo.TsukurikataTigu, beforeVo.TsukurikataTigu) Then
                Return False
            End If
            If Not StringUtil.Equals(kihonVo.TsukurikataNounyu, beforeVo.TsukurikataNounyu) Then
                Return False
            End If
            If Not StringUtil.Equals(kihonVo.TsukurikataKibo, beforeVo.TsukurikataKibo) Then
                Return False
            End If
            '試作板厚数量'
            If Not StringUtil.Equals(kihonVo.ShisakuBankoSuryo, beforeVo.ShisakuBankoSuryo) Then
                Return False
            End If
            '試作板厚数量U'
            If Not StringUtil.Equals(kihonVo.ShisakuBankoSuryoU, beforeVo.ShisakuBankoSuryoU) Then
                Return False
            End If
            If Not StringUtil.Equals(kihonVo.MaterialInfoLength, beforeVo.MaterialInfoLength) Then
                Return False
            End If
            If Not StringUtil.Equals(kihonVo.MaterialInfoWidth, beforeVo.MaterialInfoWidth) Then
                Return False
            End If
            If Not StringUtil.Equals(kihonVo.ZairyoSunpoX, beforeVo.ZairyoSunpoX) Then
                Return False
            End If
            If Not StringUtil.Equals(kihonVo.ZairyoSunpoY, beforeVo.ZairyoSunpoY) Then
                Return False
            End If
            If Not StringUtil.Equals(kihonVo.ZairyoSunpoZ, beforeVo.ZairyoSunpoZ) Then
                Return False
            End If
            If Not StringUtil.Equals(kihonVo.ZairyoSunpoXy, beforeVo.ZairyoSunpoXy) Then
                Return False
            End If
            If Not StringUtil.Equals(kihonVo.ZairyoSunpoXz, beforeVo.ZairyoSunpoXz) Then
                Return False
            End If
            If Not StringUtil.Equals(kihonVo.ZairyoSunpoYz, beforeVo.ZairyoSunpoYz) Then
                Return False
            End If
            If Not StringUtil.Equals(kihonVo.MaterialInfoOrderTarget, beforeVo.MaterialInfoOrderTarget) Then
                Return False
            End If
            If StringUtil.IsNotEmpty(kihonVo.MaterialInfoOrderTargetDate) And StringUtil.IsNotEmpty(beforeVo.MaterialInfoOrderTargetDate) Then
                If Not StringUtil.Equals(kihonVo.MaterialInfoOrderTargetDate, beforeVo.MaterialInfoOrderTargetDate) Then
                    Return False
                End If
            End If
            If Not StringUtil.Equals(kihonVo.MaterialInfoOrderChk, beforeVo.MaterialInfoOrderChk) Then
                Return False
            End If
            If StringUtil.IsNotEmpty(kihonVo.MaterialInfoOrderChkDate) And StringUtil.IsNotEmpty(beforeVo.MaterialInfoOrderChkDate) Then
                If Not StringUtil.Equals(kihonVo.MaterialInfoOrderChkDate, beforeVo.MaterialInfoOrderChkDate) Then
                    Return False
                End If
            End If
            If Not StringUtil.Equals(kihonVo.DataItemKaiteiNo, beforeVo.DataItemKaiteiNo) Then
                Return False
            End If
            If Not StringUtil.Equals(kihonVo.DataItemAreaName, beforeVo.DataItemAreaName) Then
                Return False
            End If
            If Not StringUtil.Equals(kihonVo.DataItemSetName, beforeVo.DataItemSetName) Then
                Return False
            End If
            If Not StringUtil.Equals(kihonVo.DataItemKaiteiInfo, beforeVo.DataItemKaiteiInfo) Then
                Return False
            End If
            If Not StringUtil.Equals(kihonVo.DataItemDataProvision, beforeVo.DataItemDataProvision) Then
                Return False
            End If
            If StringUtil.IsNotEmpty(kihonVo.DataItemDataProvisionDate) And StringUtil.IsNotEmpty(beforeVo.DataItemDataProvisionDate) Then
                If Not StringUtil.Equals(kihonVo.DataItemDataProvisionDate, beforeVo.DataItemDataProvisionDate) Then
                    Return False
                End If
            End If
            '試作部品費'
            If Not kihonVo.ShisakuBuhinnHi = beforeVo.ShisakuBuhinnHi Then
                Return False
            End If
            '試作型費'
            If Not kihonVo.ShisakuKataHi = beforeVo.ShisakuKataHi Then
                Return False
            End If
            '取引先名称'
            If Not StringUtil.Equals(kihonVo.MakerCode, beforeVo.MakerCode) Then
                Return False
            End If
            '備考'
            If Not StringUtil.Equals(kihonVo.Bikou, beforeVo.Bikou) Then
                Return False
            End If

            ''部品番号(親)'
            'If Not StringUtil.Equals(kihonVo.BuhinNoOya, beforeVo.BuhinNoOya) Then
            '    Return False
            'End If
            ''部品番号試作区分(親)'
            'If Not StringUtil.Equals(kihonVo.BuhinNoKbnOya, beforeVo.BuhinNoKbnOya) Then
            '    Return False
            'End If
            ''エラー区分'
            'If Not StringUtil.Equals(kihonVo.ErrorKbn, beforeVo.ErrorKbn) Then
            '    Return False
            'End If
            ''Audフラグ'
            'If Not StringUtil.Equals(kihonVo.AudFlag, beforeVo.AudFlag) Then
            '    Return False
            'End If
            ''Aud日'
            'If Not kihonVo.AudBi = beforeVo.AudBi Then
            '    Return False
            'End If
            ''結合No'
            'If Not StringUtil.Equals(kihonVo.KetugouNo, beforeVo.KetugouNo) Then
            '    Return False
            'End If

            '20150120　メタル対応　下記チェックは不要
            ''変化点'
            'If Not StringUtil.Equals(kihonVo.Henkaten, beforeVo.Henkaten) Then
            '    Return False
            'End If

            '試作製品区分'
            If Not StringUtil.Equals(kihonVo.ShisakuSeihinKbn, beforeVo.ShisakuSeihinKbn) Then
                Return False
            End If
            Return True
        End Function

        ''' <summary>
        ''' 部品番号とプライマリキー以外が同一かチェック
        ''' </summary>
        ''' <param name="BuhinEditVo">部品編集情報</param>
        ''' <param name="BuhinBaseVo">部品編集ベース情報</param>
        ''' <returns>全件同一ならTrue</returns>
        ''' <remarks></remarks>
        Private Function CheckByAllBuhin(ByVal BuhinEditVo As TShisakuBuhinEditVo, ByVal BuhinBaseVo As TShisakuBuhinEditBaseVo) As Boolean

            If Not StringUtil.Equals(BuhinEditVo.Level, BuhinBaseVo.Level) Then
                Return False
            ElseIf Not StringUtil.Equals(BuhinEditVo.ShukeiCode, BuhinBaseVo.ShukeiCode) Then
                Return False
            ElseIf Not StringUtil.Equals(BuhinEditVo.SiaShukeiCode, BuhinBaseVo.SiaShukeiCode) Then
                Return False
            ElseIf Not StringUtil.Equals(BuhinEditVo.GencyoCkdKbn, BuhinBaseVo.GencyoCkdKbn) Then
                Return False
            ElseIf Not StringUtil.Equals(BuhinEditVo.MakerCode, BuhinBaseVo.MakerCode) Then
                Return False
            ElseIf Not StringUtil.Equals(BuhinEditVo.MakerName, BuhinBaseVo.MakerName) Then
                Return False
            ElseIf Not StringUtil.Equals(BuhinEditVo.BuhinNoKbn, BuhinBaseVo.BuhinNoKbn) Then
                Return False
            ElseIf Not StringUtil.Equals(BuhinEditVo.BuhinNoKaiteiNo, BuhinBaseVo.BuhinNoKaiteiNo) Then
                Return False
            ElseIf Not StringUtil.Equals(BuhinEditVo.EdaBan, BuhinBaseVo.EdaBan) Then
                Return False
            ElseIf Not StringUtil.Equals(BuhinEditVo.BuhinName, BuhinBaseVo.BuhinName) Then
                Return False
            ElseIf Not StringUtil.Equals(BuhinEditVo.Saishiyoufuka, BuhinBaseVo.Saishiyoufuka) Then
                Return False
            ElseIf Not StringUtil.Equals(BuhinEditVo.ShutuzuYoteiDate, BuhinBaseVo.ShutuzuYoteiDate) Then
                Return False
            ElseIf Not StringUtil.Equals(BuhinEditVo.ZaishituKikaku1, BuhinBaseVo.ZaishituKikaku1) Then
                Return False
            ElseIf Not StringUtil.Equals(BuhinEditVo.ZaishituKikaku2, BuhinBaseVo.ZaishituKikaku2) Then
                Return False
            ElseIf Not StringUtil.Equals(BuhinEditVo.ZaishituKikaku3, BuhinBaseVo.ZaishituKikaku3) Then
                Return False
            ElseIf Not StringUtil.Equals(BuhinEditVo.ZaishituMekki, BuhinBaseVo.ZaishituMekki) Then
                Return False
            ElseIf Not StringUtil.Equals(BuhinEditVo.ShisakuBankoSuryo, BuhinBaseVo.ShisakuBankoSuryo) Then
                Return False
            ElseIf Not StringUtil.Equals(BuhinEditVo.ShisakuBankoSuryoU, BuhinBaseVo.ShisakuBankoSuryoU) Then
                Return False
            ElseIf Not StringUtil.Equals(BuhinEditVo.MaterialInfoLength, BuhinBaseVo.MaterialInfoLength) Then
                Return False
            ElseIf Not StringUtil.Equals(BuhinEditVo.MaterialInfoWidth, BuhinBaseVo.MaterialInfoWidth) Then
                Return False
            ElseIf Not StringUtil.Equals(BuhinEditVo.DataItemKaiteiNo, BuhinBaseVo.DataItemKaiteiNo) Then
                Return False
            ElseIf Not StringUtil.Equals(BuhinEditVo.DataItemAreaName, BuhinBaseVo.DataItemAreaName) Then
                Return False
            ElseIf Not StringUtil.Equals(BuhinEditVo.DataItemSetName, BuhinBaseVo.DataItemSetName) Then
                Return False
            ElseIf Not StringUtil.Equals(BuhinEditVo.DataItemKaiteiInfo, BuhinBaseVo.DataItemKaiteiInfo) Then
                Return False
            ElseIf Not StringUtil.Equals(BuhinEditVo.ShisakuBuhinHi, BuhinBaseVo.ShisakuBuhinHi) Then
                Return False
            ElseIf Not StringUtil.Equals(BuhinEditVo.ShisakuKataHi, BuhinBaseVo.ShisakuKataHi) Then
                Return False
            ElseIf Not StringUtil.Equals(BuhinEditVo.Bikou, BuhinBaseVo.Bikou) Then
                Return False
            ElseIf Not StringUtil.Equals(BuhinEditVo.KaiteiHandanFlg, BuhinBaseVo.KaiteiHandanFlg) Then
                Return False
            End If
            Return True
        End Function

        ''' <summary>
        ''' 部品番号とプライマリキー以外が同一かチェック
        ''' </summary>
        ''' <param name="BuhinEditVo">部品編集情報</param>
        ''' <param name="ZenkaiBuhinVo">前回部品編集情報</param>
        ''' <returns>全件同一ならTrue</returns>
        ''' <remarks></remarks>
        Private Function CheckByAllBuhin(ByVal BuhinEditVo As TShisakuBuhinEditVo, ByVal ZenkaiBuhinVo As TShisakuBuhinEditVo) As Boolean

            If Not StringUtil.Equals(BuhinEditVo.Level, ZenkaiBuhinVo.Level) Then
                Return False
            ElseIf Not StringUtil.Equals(BuhinEditVo.ShukeiCode, ZenkaiBuhinVo.ShukeiCode) Then
                Return False
            ElseIf Not StringUtil.Equals(BuhinEditVo.SiaShukeiCode, ZenkaiBuhinVo.SiaShukeiCode) Then
                Return False
            ElseIf Not StringUtil.Equals(BuhinEditVo.GencyoCkdKbn, ZenkaiBuhinVo.GencyoCkdKbn) Then
                Return False
            ElseIf Not StringUtil.Equals(BuhinEditVo.MakerCode, ZenkaiBuhinVo.MakerCode) Then
                Return False
            ElseIf Not StringUtil.Equals(BuhinEditVo.MakerName, ZenkaiBuhinVo.MakerName) Then
                Return False
            ElseIf Not StringUtil.Equals(BuhinEditVo.BuhinNoKbn, ZenkaiBuhinVo.BuhinNoKbn) Then
                Return False
            ElseIf Not StringUtil.Equals(BuhinEditVo.BuhinNoKaiteiNo, ZenkaiBuhinVo.BuhinNoKaiteiNo) Then
                Return False
            ElseIf Not StringUtil.Equals(BuhinEditVo.EdaBan, ZenkaiBuhinVo.EdaBan) Then
                Return False
            ElseIf Not StringUtil.Equals(BuhinEditVo.BuhinName, ZenkaiBuhinVo.BuhinName) Then
                Return False
            ElseIf Not StringUtil.Equals(BuhinEditVo.Saishiyoufuka, ZenkaiBuhinVo.Saishiyoufuka) Then
                Return False
            ElseIf Not StringUtil.Equals(BuhinEditVo.ShutuzuYoteiDate, ZenkaiBuhinVo.ShutuzuYoteiDate) Then
                Return False
            ElseIf Not StringUtil.Equals(BuhinEditVo.ZaishituKikaku1, ZenkaiBuhinVo.ZaishituKikaku1) Then
                Return False
            ElseIf Not StringUtil.Equals(BuhinEditVo.ZaishituKikaku2, ZenkaiBuhinVo.ZaishituKikaku2) Then
                Return False
            ElseIf Not StringUtil.Equals(BuhinEditVo.ZaishituKikaku3, ZenkaiBuhinVo.ZaishituKikaku3) Then
                Return False
            ElseIf Not StringUtil.Equals(BuhinEditVo.ZaishituMekki, ZenkaiBuhinVo.ZaishituMekki) Then
                Return False
            ElseIf Not StringUtil.Equals(BuhinEditVo.ShisakuBankoSuryo, ZenkaiBuhinVo.ShisakuBankoSuryo) Then
                Return False
            ElseIf Not StringUtil.Equals(BuhinEditVo.ShisakuBankoSuryoU, ZenkaiBuhinVo.ShisakuBankoSuryoU) Then
                Return False
            ElseIf Not StringUtil.Equals(BuhinEditVo.MaterialInfoLength, ZenkaiBuhinVo.MaterialInfoLength) Then
                Return False
            ElseIf Not StringUtil.Equals(BuhinEditVo.MaterialInfoWidth, ZenkaiBuhinVo.MaterialInfoWidth) Then
                Return False
            ElseIf Not StringUtil.Equals(BuhinEditVo.DataItemKaiteiNo, ZenkaiBuhinVo.DataItemKaiteiNo) Then
                Return False
            ElseIf Not StringUtil.Equals(BuhinEditVo.DataItemAreaName, ZenkaiBuhinVo.DataItemAreaName) Then
                Return False
            ElseIf Not StringUtil.Equals(BuhinEditVo.DataItemSetName, ZenkaiBuhinVo.DataItemSetName) Then
                Return False
            ElseIf Not StringUtil.Equals(BuhinEditVo.DataItemKaiteiInfo, ZenkaiBuhinVo.DataItemKaiteiInfo) Then
                Return False
            ElseIf Not StringUtil.Equals(BuhinEditVo.ShisakuBuhinHi, ZenkaiBuhinVo.ShisakuBuhinHi) Then
                Return False
            ElseIf Not StringUtil.Equals(BuhinEditVo.ShisakuKataHi, ZenkaiBuhinVo.ShisakuKataHi) Then
                Return False
            ElseIf Not StringUtil.Equals(BuhinEditVo.Bikou, ZenkaiBuhinVo.Bikou) Then
                Return False
            ElseIf Not StringUtil.Equals(BuhinEditVo.EditTourokubi, ZenkaiBuhinVo.EditTourokubi) Then
                Return False
            ElseIf Not StringUtil.Equals(BuhinEditVo.EditTourokujikan, ZenkaiBuhinVo.EditTourokujikan) Then
                Return False
            ElseIf Not StringUtil.Equals(BuhinEditVo.KaiteiHandanFlg, ZenkaiBuhinVo.KaiteiHandanFlg) Then
                Return False
            ElseIf Not StringUtil.Equals(BuhinEditVo.ShisakuListCode, ZenkaiBuhinVo.ShisakuListCode) Then
                Return False
            End If
            Return True
        End Function





#Region "公開プロパティ"

        '' 試作イベントコード
        Private _ShisakuEventCode As String
        '' 試作リスト表示順No
        Private _ShisakuListHyoujiJun As String
        '' 試作リストコード
        Private _ShisakuListCode As String
        '' 試作リストコード改訂No
        Private _ShisakuListCodeKaiteiNo As String
        '' 試作グループNo
        Private _ShisakuGroupNo As String
        '' 試作製品区分
        Private _ShisakuSeihinKbn As String
        '' 試作工事指令No
        Private _ShisakuKoujiShireiNo As String
        '' 試作工事No
        Private _ShisakuKoujiNo As String
        '' 試作イベント名称
        Private _ShisakuEventName As String
        '' 旧試作リストコード
        Private _OldListCode As String
        '' 訂正抽出日
        Private _TeiseiChusyutubi As Integer
        '' 訂正抽出時間
        Private _TeiseiChusyutujikan As Integer
        '' 試作転送日
        Private _ShisakuTensoubi As Integer
        '' 試作転送時間
        Private _ShisakuTensoujikan As Integer
        '' 前回改定日
        Private _ZenkaiKaiteibi As Integer
        '' 最新抽出日
        Private _SaishinChusyutubi As Integer
        '' 最新抽出時間
        Private _SaishinChusyutujikan As Integer
        '' ステータス
        Private _Status As String
#End Region

#Region "公開プロパティの実装"
        ''' <summary>試作イベントコード</summary>
        ''' <value>試作イベントコード</value>
        ''' <returns>試作イベントコード</returns>
        Public Property ShisakuEventCode() As String
            Get
                Return _ShisakuEventCode
            End Get
            Set(ByVal value As String)
                _ShisakuEventCode = value
            End Set
        End Property

        ''' <summary>リストコード改定No</summary>
        ''' <returns>リストコード改定No</returns>
        Public Property ListcodeKaiteiNo() As String
            Get
                Return _ShisakuListCodeKaiteiNo
            End Get
            Set(ByVal value As String)
                _ShisakuListCodeKaiteiNo = value
            End Set
        End Property

        ''' <summary>試作リストコード</summary>
        ''' <returns>試作リストコード</returns>
        Public Property ListCode() As String
            Get
                Return _ShisakuListCode
            End Get
            Set(ByVal value As String)
                _ShisakuListCode = value
            End Set
        End Property

        ''' <summary>製品区分</summary>
        ''' <returns>製品区分</returns>
        Public Property SeihinKbn() As String
            Get
                Return _ShisakuSeihinKbn
            End Get
            Set(ByVal value As String)
                _ShisakuSeihinKbn = value
            End Set
        End Property

        ''' <summary>工事指令No</summary>
        ''' <returns>工事指令No</returns>
        Public Property ShisakuKoujiShireiNo() As String
            Get
                Return _ShisakuKoujiShireiNo
            End Get
            Set(ByVal value As String)
                _ShisakuKoujiShireiNo = value
            End Set
        End Property

        ''' <summary>工事No</summary>
        ''' <value>工事No</value>
        ''' <returns>工事No</returns>
        Public Property KoujiNo() As String
            Get
                Return _ShisakuKoujiNo
            End Get
            Set(ByVal value As String)
                _ShisakuKoujiNo = value
            End Set
        End Property

        ''' <summary>イベント名称</summary>
        ''' <value>イベント名称</value>
        ''' <returns>イベント名称</returns>
        Public Property EventName() As String
            Get
                Return _ShisakuEventName
            End Get
            Set(ByVal value As String)
                _ShisakuEventName = value
            End Set
        End Property

        ''' <summary>旧リストコード</summary>
        ''' <value>旧リストコード</value>
        ''' <returns>旧リストコード</returns>
        Public Property OldListCode() As String
            Get
                Return _OldListCode
            End Get
            Set(ByVal value As String)
                _OldListCode = value
            End Set
        End Property

        ''' <summary>前回改定日</summary>
        ''' <value>前回改定日</value>
        ''' <returns>前回改定日</returns>
        Public Property ZenkaiKaiteibi() As Integer
            Get
                Return _ZenkaiKaiteibi
            End Get
            Set(ByVal value As Integer)
                _ZenkaiKaiteibi = value
            End Set
        End Property

        ''' <summary>最新抽出日</summary>
        ''' <value>最新抽出日</value>
        ''' <returns>最新抽出日</returns>
        Public Property SaishinChusyutubi() As Integer
            Get
                Return _SaishinChusyutubi
            End Get
            Set(ByVal value As Integer)
                _SaishinChusyutubi = value
            End Set
        End Property

        ''' <summary>最新抽出時間</summary>
        ''' <value>最新抽出時間</value>
        ''' <returns>最新抽出時間</returns>
        Public Property SaishinChusyutujikan() As Integer
            Get
                Return _SaishinChusyutujikan
            End Get
            Set(ByVal value As Integer)
                _SaishinChusyutujikan = value
            End Set
        End Property

        ''' <summary>ステータス</summary>
        ''' <value>ステータス</value>
        ''' <returns>ステータス</returns>
        Public Property Status() As String
            Get
                Return _Status
            End Get
            Set(ByVal value As String)
                _Status = value
            End Set
        End Property

#End Region



    End Class

End Namespace