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
Imports EventSakusei.ShisakuBuhinKaiteiBlock.Dao

Namespace TehaichoMenu.Logic

    ''' <summary>
    ''' 改訂抽出をする
    ''' </summary>
    ''' <remarks></remarks>
    Public Class TehaichoKaiteiChusyutu

        ''イベントコード
        Private shisakuEventCode As String
        ''リストコード
        Private shisakuListCode As String
        ''最新リストコード改訂No
        Private shisakuListCodeKaiteiNo As String
        ''baseflag
        Private base As Boolean
        ''2014/08/28 追加
        ''抽出開始日時
        ''  本クラスがインスタンス化された日時を使用する
        'Private kaishiNitiji As ShisakuDate
        Private kaishiNitiji As Long
        Private kaishiBi As Integer
        Private kaishiJikan As Integer
        Private isIkansha As Boolean

        ''' <summary>
        ''' 初期化
        ''' </summary>
        ''' <param name="shisakuEventCode">イベントコード</param>
        ''' <param name="shisakuListCode">リストコード</param>
        ''' <param name="shisakuListCodeKaiteiNo">リストコード改訂No</param>
        ''' <remarks></remarks>
        Public Sub New(ByVal shisakuEventCode As String, _
                       ByVal shisakuListCode As String, _
                       ByVal shisakuListCodeKaiteiNo As String, ByVal base As Boolean)

            Me.shisakuEventCode = shisakuEventCode
            Me.shisakuListCode = shisakuListCode
            Me.shisakuListCodeKaiteiNo = shisakuListCodeKaiteiNo
            Me.base = base
            '2014/08/28 追加
            Dim dt = New ShisakuDate
            kaishiNitiji = Integer.Parse(dt.CurrentDateDbFormat.Replace("-", ""))
            kaishiNitiji *= 1000000
            kaishiNitiji += Integer.Parse(dt.CurrentTimeDbFormat.Replace(":", ""))
            kaishiBi = Integer.Parse(dt.CurrentDateDbFormat.Replace("-", ""))
            kaishiJikan = Integer.Parse(dt.CurrentTimeDbFormat.Replace(":", ""))

            Dim dao As TShisakuEventDao = New TShisakuEventDaoImpl
            Dim vo As TShisakuEventVo = dao.FindByPk(shisakuEventCode)
            Me.isIkansha = (vo.BlockAlertKind = "2" And vo.KounyuShijiFlg = "0")


        End Sub

        ''' <summary>
        ''' 改訂抽出処理
        ''' </summary>
        ''' <remarks></remarks>
        Public Sub KaiteiCyushutu()
            Dim CyushutuImpl As KaiteiChusyutuDao = New KaiteiChusyutuDaoImpl
            '先に重複しないように削除しておく'
            CyushutuImpl.DeleteByBuhinEditKaitei(shisakuEventCode, shisakuListCode, shisakuListCodeKaiteiNo)
            CyushutuImpl.DeleteByBuhinEditKaiteiGousya(shisakuEventCode, shisakuListCode, shisakuListCodeKaiteiNo)
            CyushutuImpl.DelteByTmpEdit(shisakuEventCode)

            KaiteiCyushutu(CyushutuImpl)

            '2014/04/02 kabasawa'
            'DUMMY列のズレが発生していることがあるのでDUMMYを除いた号車列の最大値を求め'
            'ずれた号車列を訂正する'
            'ダミー列を除く号車表示順の最大値を取得'
            Dim maxGousyaHyoujijunNo As Integer = CyushutuImpl.FindByMaxGousyaHyoujijunNotDummy(shisakuEventCode, _
                                                                                                shisakuListCode, _
                                                                                                shisakuListCodeKaiteiNo)
            'ダミー列を除いた号車順+2が正しいダミーの位置'
            maxGousyaHyoujijunNo = maxGousyaHyoujijunNo + 2

            CyushutuImpl.UpdateByMaxGousyaHyoujijunDummy(shisakuEventCode, shisakuListCode, shisakuListCodeKaiteiNo, maxGousyaHyoujijunNo)


        End Sub

        ''' <summary>
        ''' 自動織込み込みの改訂抽出
        ''' </summary>
        ''' <remarks></remarks>
        Public Sub KaiteiCyushutuAuto()
            Dim CyushutuImpl As KaiteiChusyutuDao = New KaiteiChusyutuDaoImpl
            '先に重複しないように削除しておく'
            CyushutuImpl.DeleteByBuhinEditKaitei(shisakuEventCode, shisakuListCode, shisakuListCodeKaiteiNo)
            CyushutuImpl.DeleteByBuhinEditKaiteiGousya(shisakuEventCode, shisakuListCode, shisakuListCodeKaiteiNo)
            CyushutuImpl.DelteByTmpEdit(shisakuEventCode)

            KaiteiCyushutu(CyushutuImpl)

            '自動織込み'
            AutoOrikomi(CyushutuImpl)

            '2014/04/02 kabasawa'
            'DUMMY列のズレが発生していることがあるのでDUMMYを除いた号車列の最大値を求め'
            'ずれた号車列を訂正する'
            'ダミー列を除く号車表示順の最大値を取得'
            Dim maxGousyaHyoujijunNo As Integer = CyushutuImpl.FindByMaxGousyaHyoujijunNotDummy(shisakuEventCode, _
                                                                                                shisakuListCode, _
                                                                                                shisakuListCodeKaiteiNo)
            'ダミー列を除いた号車順+2が正しいダミーの位置'
            maxGousyaHyoujijunNo = maxGousyaHyoujijunNo + 2

            CyushutuImpl.UpdateByMaxGousyaHyoujijunDummy(shisakuEventCode, shisakuListCode, shisakuListCodeKaiteiNo, maxGousyaHyoujijunNo)

        End Sub

        ''' <summary>
        ''' 改訂抽出
        ''' </summary>
        ''' <param name="CyushutuImpl">改訂抽出Dao</param>
        ''' <remarks></remarks>
        Private Sub Kaiteicyushutu(ByVal CyushutuImpl As KaiteiChusyutuDao)
            '最新改訂Noの部品編集情報を取得'
            Dim BuhinEditVoList As New List(Of TShisakuBuhinEditVoSekkeiHelper)
            If base Then
                BuhinEditVoList = CyushutuImpl.FindByNewBuhinEditListSaishin(shisakuEventCode, shisakuListCode, base)
            Else
                ''2014/08/28 変更
                ''　開始時刻で情報を抽出するよう変更
                'BuhinEditVoList = CyushutuImpl.FindByNewBuhinEditList(shisakuEventCode, shisakuListCode, base)
                BuhinEditVoList = CyushutuImpl.FindByNewBuhinEditList(shisakuEventCode, shisakuListCode, kaishiNitiji, base, Me.isIkansha)
            End If

            '最初に圧縮した状態の部品編集改訂情報と部品編集号車改訂情報を作成する'
            Dim newBuhinEditVoList As New List(Of TShisakuBuhinEditVoSekkeiHelper)
            newBuhinEditVoList = MergeList(BuhinEditVoList)


            '追加する'
            CyushutuImpl.InsertByBuhinEditTehaiKaitei(newBuhinEditVoList, True)
            CyushutuImpl.InsertByBuhinEditGousyaKaitei(newBuhinEditVoList, True)

            'リストコード改訂Noに該当する手配改訂ブロック情報を元に部品編集情報を取得する'
            Dim tehaiKaiteiBlockVoList As New List(Of TShisakuTehaiKaiteiBlockVoHelper)

            '2012/06/02 baseget'
            'コンボボックスでBaseが指定されたら以下の処理をおこなう。
            'コンボボックスへBaseを追加する方法が不明。どうするか？
            If base Then
                Dim baseList As New List(Of TShisakuBuhinEditVoSekkeiHelper)
                baseList = CyushutuImpl.FindByBuhinEditBase(shisakuEventCode, shisakuListCode)

                Dim newZenkaiList As New List(Of TShisakuBuhinEditVoSekkeiHelper)
                newZenkaiList = MergeList(baseList)
                CyushutuImpl.InsertByBuhinEditTehaiKaitei(newZenkaiList, False)
                CyushutuImpl.InsertByBuhinEditGousyaKaitei(newZenkaiList, False)

                '前回の改訂Noと今回の改訂Noの比較を開始する'    
                KaiteiCyushutuKaiteiBlockTehaiKaiteiBlock(CyushutuImpl)

            Else
                'Base以外の場合、通常処理を行う。

                '手配改訂抽出ブロック情報を取得'
                tehaiKaiteiBlockVoList = CyushutuImpl.FindByBuhinEditKaiteiNo(shisakuEventCode, shisakuListCode, Me.isIkansha)

                '取得できたかチェック'
                If tehaiKaiteiBlockVoList.Count = 0 Then
                    '取得できなかったら最新の改訂No-1で改訂抽出'
                    'KaiteiCyushutuKaiteiBlock(CyushutuImpl, BuhinEditVoList)
                Else
                    '取得できたら前回改訂Noを使って前回の部品編集情報リストを取得する'

                    Dim goshaList As Hashtable = CyushutuImpl.FindByTShisakuTehaiGousya(shisakuEventCode, shisakuListCode)
                    Dim newAddZenkaiList As New List(Of TShisakuBuhinEditVoSekkeiHelper)
                    For Each tVo As TShisakuTehaiKaiteiBlockVoHelper In tehaiKaiteiBlockVoList
                        Dim flg As Boolean = False
                        '最新の部品編集情報と同じブロックNoなら作る() '
                        For Each nVo As TShisakuBuhinEditVoSekkeiHelper In newBuhinEditVoList
                            If StringUtil.Equals(tVo.ShisakuBlockNo, nVo.ShisakuBlockNo) Then
                                flg = True
                                Exit For
                            End If
                        Next

                        '--------------------------------------------------------------------------------------
                        '2012/10/11　柳沼修正
                        '前回グループのINSTL員数が全て削除された場合、削除として改訂抽出されない現象を回避する。
                        '   試作ブロック情報の最新改訂№を取得
                        '   前回改訂ブロック改訂№より大きければ改訂抽出対象とみなす。
                        Dim ShisakuBlockVoList As New List(Of ShisakuBuhinBlockVo)

                        ''2014/08/28 変更
                        ''　開始時刻で情報を抽出するよう変更
                        ShisakuBlockVoList = CyushutuImpl.FindByNewBlockList(shisakuEventCode, _
                                                                             shisakuListCode, _
                                                                             tVo.ShisakuBlockNo, _
                                                                             kaishiNitiji)
                        'ShisakuBlockVoList = CyushutuImpl.FindByNewBlockList(shisakuEventCode, _
                        '                                                     shisakuListCode, _
                        '                                                     tVo.ShisakuBlockNo)

                        '前回改訂より大きければフラグを立てる。
                        If ShisakuBlockVoList.Count > 0 Then
                            If tVo.ZenkaiBlockNoKaiteiNo < ShisakuBlockVoList(0).ShisakuBlockNoKaiteiNo Then
                                '手配帳改訂抽出ブロック情報の更新用に基本情報をBuhinEditVoListに追加する。
                                Dim BlockNoVo As New TShisakuBuhinEditVoSekkeiHelper
                                BlockNoVo.ShisakuEventCode = ShisakuBlockVoList(0).ShisakuEventCode
                                BlockNoVo.ShisakuListCode = shisakuListCode
                                BlockNoVo.ShisakuBukaCode = ShisakuBlockVoList(0).ShisakuBukaCode
                                BlockNoVo.ShisakuBlockNo = ShisakuBlockVoList(0).ShisakuBlockNo
                                BlockNoVo.ShisakuBlockNoKaiteiNo = ShisakuBlockVoList(0).ShisakuBlockNoKaiteiNo
                                BuhinEditVoList.Add(BlockNoVo)
                                'フラグをたてる。
                                flg = True
                            End If
                        End If
                        '--------------------------------------------------------------------------------------

                        If flg Then

                            '前回の改訂№が空のケースはブロック№を追加したときしかありえない
                            ''前回の改訂Noが空の場合は最新-1で改訂抽出'
                            'If StringUtil.IsEmpty(tVo.ZenkaiBlockNoKaiteiNo) Then
                            '    tVo.ZenkaiBlockNoKaiteiNo = Right("000" + StringUtil.ToString(Integer.Parse(tVo.KonkaiBlockNoKaiteiNo) - 1), 3)
                            'End If

                            Dim zenkaiList As New List(Of TShisakuBuhinEditVoSekkeiHelper)
                            zenkaiList = CyushutuImpl.FindByZenkaiTehaikaiteiBlock(tVo, goshaList, Me.isIkansha)

                            Dim newZenkaiList As New List(Of TShisakuBuhinEditVoSekkeiHelper)
                            newZenkaiList = MergeList(zenkaiList)
                            newAddZenkaiList.AddRange(newZenkaiList)
                            'CyushutuImpl.InsertByBuhinEditTehaiKaitei(newZenkaiList, False)
                            'CyushutuImpl.InsertByBuhinEditGousyaKaitei(newZenkaiList, False)
                        End If
                    Next
                    CyushutuImpl.InsertByBuhinEditTehaiKaitei(newAddZenkaiList, False)
                    CyushutuImpl.InsertByBuhinEditGousyaKaitei(newAddZenkaiList, False)

                    '前回の改訂Noと今回の改訂Noの比較を開始する'
                    KaiteiCyushutuKaiteiBlockTehaiKaiteiBlock(CyushutuImpl)
                End If


                '終わったら手配帳改訂抽出ブロック情報を更新、追加'
                Dim BlockNoList As New List(Of TShisakuTehaiKaiteiBlockVo)

                For index As Integer = 0 To BuhinEditVoList.Count - 1
                    Dim BlockNoVo As New TShisakuTehaiKaiteiBlockVo
                    BlockNoVo.ShisakuEventCode = BuhinEditVoList(index).ShisakuEventCode
                    BlockNoVo.ShisakuListCode = shisakuListCode
                    BlockNoVo.ShisakuListCodeKaiteiNo = shisakuListCodeKaiteiNo
                    BlockNoVo.ShisakuBukaCode = BuhinEditVoList(index).ShisakuBukaCode
                    BlockNoVo.ShisakuBlockNo = BuhinEditVoList(index).ShisakuBlockNo
                    BlockNoVo.KonkaiBlockNoKaiteiNo = BuhinEditVoList(index).ShisakuBlockNoKaiteiNo
                    If index = 0 Then
                        BlockNoList.Add(BlockNoVo)
                    Else
                        If Not StringUtil.Equals(BuhinEditVoList(index).ShisakuBlockNo, BuhinEditVoList(index - 1).ShisakuBlockNo) Then
                            BlockNoList.Add(BlockNoVo)
                        End If
                    End If
                Next

                ''2015/08/17 変更E.Ubukata
                ''処理速度向上のため処理をプロシージャに変更
                ''2012/06/02 baseget'
                ''ベースと作成時のデータをぶつけるので以下はいらないでしょう？
                ''改訂ともぶつけたいとなった場合には以下の処理を復活。
                ''If base Then
                ''改訂抽出ブロックに追加をする'
                'For Each BlockVo As TShisakuTehaiKaiteiBlockVo In BlockNoList
                '    Dim result As New TShisakuTehaiKaiteiBlockVo
                '    result = CyushutuImpl.TehaiKaiteiBlock(BlockVo.ShisakuEventCode, _
                '                                           BlockVo.ShisakuListCode, _
                '                                           BlockVo.ShisakuBukaCode, _
                '                                           BlockVo.ShisakuBlockNo)
                '    If result Is Nothing Then
                '        CyushutuImpl.InsertByTehaiKaitei(BlockVo.ShisakuEventCode, _
                '                                         BlockVo.ShisakuListCode, _
                '                                         BlockVo.ShisakuBukaCode, _
                '                                         BlockVo.ShisakuBlockNo, _
                '                                         BlockVo.KonkaiBlockNoKaiteiNo)
                '    Else
                '        CyushutuImpl.UpdateByTehaiKaitei(BlockVo.ShisakuEventCode, _
                '                                         BlockVo.ShisakuListCode, _
                '                                         BlockVo.ShisakuBukaCode, _
                '                                         BlockVo.ShisakuBlockNo, _
                '                                         BlockVo.KonkaiBlockNoKaiteiNo)

                '    End If
                'Next

                CyushutuImpl.SetTehaiKaiteiBlock(BlockNoList)



                '2012/02/22
                '試作イベントの最終改訂抽出日をセット
                CyushutuImpl.UpdateByShisakuEvent(shisakuEventCode, kaishiBi)

                CyushutuImpl.UpdateBySaishinChusyutu(shisakuEventCode, _
                                                     shisakuListCode, _
                                                     shisakuListCodeKaiteiNo, _
                                                     kaishiBi, _
                                                     kaishiJikan)


            End If

            '終わったら比較用の手配帳データを削除'
            CyushutuImpl.DelteByTmpEdit(shisakuEventCode)

        End Sub


        ''' <summary>
        ''' 手配改訂情報を使って改訂抽出
        ''' </summary>
        ''' <param name="CyushutuImpl">改訂抽出Dao</param>
        ''' <remarks></remarks>
        Private Sub KaiteiCyushutuKaiteiBlockTehaiKaiteiBlock(ByVal CyushutuImpl As KaiteiChusyutuDao)

            Dim zenkaiBlock As String = ""

            '今回改訂Noの部品編集改訂情報'
            Dim konkaiBuhinEditVoList As New List(Of TShisakuBuhinEditVoSekkeiHelper)
            konkaiBuhinEditVoList = CyushutuImpl.FindByBuhinEditVoSekkeiHelper(shisakuEventCode, True, Me.isIkansha)




            '今回の部品改訂情報を元に探す'
            For index As Integer = 0 To konkaiBuhinEditVoList.Count - 1
                '前回の部品編集改訂情報を取得する'
                Dim bBuhinEditVo As New TShisakuBuhinEditVoSekkeiHelper
                '前回のブロック改訂情報を取得する'
                Dim bBlockVo As New TShisakuBuhinEditVoSekkeiHelper

                '同一ブロック内に同一レベルの同一部品番号は存在しないはず'
                '   2013/05/29　再使用不可を追加
                bBuhinEditVo = CyushutuImpl.FindByBeforeBuhinEdit(konkaiBuhinEditVoList(index).ShisakuEventCode, _
                                                                  konkaiBuhinEditVoList(index).ShisakuBukaCode, _
                                                                  konkaiBuhinEditVoList(index).ShisakuBlockNo, _
                                                                  konkaiBuhinEditVoList(index).Level, _
                                                                  konkaiBuhinEditVoList(index).ShukeiCode, _
                                                                  konkaiBuhinEditVoList(index).SiaShukeiCode, _
                                                                  konkaiBuhinEditVoList(index).BuhinNo, _
                                                                  konkaiBuhinEditVoList(index).BuhinNoKbn, _
                                                                  konkaiBuhinEditVoList(index).KyoukuSection, _
                                                                  konkaiBuhinEditVoList(index).Saishiyoufuka, _
                                                                  False)
                '   2013/06/26　前回ブロック情報の取得
                bBlockVo = CyushutuImpl.FindByBeforeBlock(konkaiBuhinEditVoList(index).ShisakuEventCode, _
                                                                  konkaiBuhinEditVoList(index).ShisakuBukaCode, _
                                                                  konkaiBuhinEditVoList(index).ShisakuBlockNo, _
                                                                  False)
                'まずはブロック対ブロックでチェック
                If Not bBlockVo Is Nothing Then
                    '前回のブロックが存在する場合'

                    '改訂Noが同じなら比較しない'
                    If Not base Then
                        If StringUtil.Equals(bBlockVo.ShisakuBlockNoKaiteiNo, konkaiBuhinEditVoList(index).ShisakuBlockNoKaiteiNo) Then
                            Continue For
                        End If
                    End If

                    'ブロック不要をチェックする'
                    If StringUtil.Equals(konkaiBuhinEditVoList(index).BlockFuyou, "0") AndAlso StringUtil.Equals(bBlockVo.BlockFuyou, "1") Then
                        '今回要、前回不要の場合追加で登録'
                        CyushutuImpl.InsertByBuhinEditKaitei(konkaiBuhinEditVoList(index), shisakuListCode, shisakuListCodeKaiteiNo, "1", "")
                        Dim KonkaiGousyaVoList As New List(Of TShisakuBuhinEditVoSekkeiHelper)

                        KonkaiGousyaVoList = CyushutuImpl.FindByBuhinEditGousya(konkaiBuhinEditVoList(index).ShisakuEventCode, _
                                                                                konkaiBuhinEditVoList(index).ShisakuBukaCode, _
                                                                                konkaiBuhinEditVoList(index).ShisakuBlockNo, _
                                                                                konkaiBuhinEditVoList(index).ShisakuBlockNoKaiteiNo, _
                                                                                konkaiBuhinEditVoList(index).BuhinNoHyoujiJun, _
                                                                                True)

                        For Each KVo As TShisakuBuhinEditVoSekkeiHelper In KonkaiGousyaVoList
                            '追加処理(追加
                            CyushutuImpl.InsertByBuhinEditGousya(shisakuEventCode, _
                                                                 shisakuListCode, _
                                                                 shisakuListCodeKaiteiNo, _
                                                                 KVo.ShisakuBukaCode, _
                                                                 KVo.ShisakuBlockNo, _
                                                                 KVo.ShisakuBlockNoKaiteiNo, _
                                                                 "", _
                                                                 KVo.BuhinNoHyoujiJun, _
                                                                 KVo.ShisakuGousyaHyoujiJun, _
                                                                 KVo.ShisakuGousya, _
                                                                 KVo.InsuSuryo, _
                                                                 "1")
                        Next
                        Continue For
                    ElseIf StringUtil.Equals(konkaiBuhinEditVoList(index).BlockFuyou, "1") AndAlso StringUtil.Equals(bBlockVo.BlockFuyou, "0") Then
                        '同一ブロック内に同一レベルの同一部品番号は存在しないはず'
                        '   2013/05/29　再使用不可を追加
                        bBuhinEditVo = CyushutuImpl.FindByBeforeBuhinEditFuyou(konkaiBuhinEditVoList(index).ShisakuEventCode, _
                                                                               konkaiBuhinEditVoList(index).ShisakuBukaCode, _
                                                                               konkaiBuhinEditVoList(index).ShisakuBlockNo, _
                                                                               konkaiBuhinEditVoList(index).Level, _
                                                                               konkaiBuhinEditVoList(index).ShukeiCode, _
                                                                               konkaiBuhinEditVoList(index).SiaShukeiCode, _
                                                                               konkaiBuhinEditVoList(index).BuhinNo, _
                                                                               konkaiBuhinEditVoList(index).BuhinNoKbn, _
                                                                               False)
                        If Not bBuhinEditVo Is Nothing Then
                            '今回不要、前回要の場合削除で登録'
                            CyushutuImpl.InsertByBuhinEditKaiteiDel(shisakuEventCode, shisakuListCode, shisakuListCodeKaiteiNo, _
                                                                    konkaiBuhinEditVoList(index).ShisakuBukaCode, konkaiBuhinEditVoList(index).ShisakuBlockNo, _
                                                                    konkaiBuhinEditVoList(index).ShisakuBlockNoKaiteiNo, bBuhinEditVo.ShisakuBlockNoKaiteiNo, _
                                                                    konkaiBuhinEditVoList(index).BuhinNoHyoujiJun, bBuhinEditVo, "2")

                            Dim ZenkaiGousyaVoList As New List(Of TShisakuBuhinEditVoSekkeiHelper)
                            ZenkaiGousyaVoList = CyushutuImpl.FindByBuhinEditGousya(shisakuEventCode, _
                                                                                    bBuhinEditVo.ShisakuBukaCode, _
                                                                                    bBuhinEditVo.ShisakuBlockNo, _
                                                                                    bBuhinEditVo.ShisakuBlockNoKaiteiNo, _
                                                                                    bBuhinEditVo.BuhinNoHyoujiJun, _
                                                                                    False)

                            For Each zenkaiVo As TShisakuBuhinEditVoSekkeiHelper In ZenkaiGousyaVoList
                                CyushutuImpl.InsertByBuhinEditGousyaKaiteiDel(zenkaiVo.ShisakuEventCode, _
                                                                              shisakuListCode, _
                                                                              shisakuListCodeKaiteiNo, _
                                                                              zenkaiVo.ShisakuBukaCode, _
                                                                              zenkaiVo.ShisakuBlockNo, _
                                                                              konkaiBuhinEditVoList(index).ShisakuBlockNoKaiteiNo, _
                                                                              zenkaiVo.ShisakuBlockNoKaiteiNo, _
                                                                              konkaiBuhinEditVoList(index).BuhinNoHyoujiJun, _
                                                                              zenkaiVo.ShisakuGousyaHyoujiJun, _
                                                                              zenkaiVo.ShisakuGousya, _
                                                                              zenkaiVo.InsuSuryo, _
                                                                              "2")
                            Next
                        End If
                        Continue For

                    ElseIf StringUtil.Equals(konkaiBuhinEditVoList(index).BlockFuyou, "1") AndAlso StringUtil.Equals(bBlockVo.BlockFuyou, "1") Then
                        '今回前回ともに不要なら改訂抽出対象外'
                        Continue For
                    End If
                End If
                '部品単位でチェック
                If Not bBuhinEditVo Is Nothing Then
                    'ここから改訂抽出'
                    '全件チェック'
                    If CheckByAllBuhin(konkaiBuhinEditVoList(index), bBuhinEditVo) Then
                        '全件チェックで変更無し'
                        '員数チェック'
                        InsuCheckNotChange(CyushutuImpl, konkaiBuhinEditVoList(index), bBuhinEditVo.ShisakuBlockNoKaiteiNo, bBuhinEditVo.BuhinNoHyoujiJun)
                    Else
                        '全件チェックで変更有り'
                        InsuCheckChange(CyushutuImpl, konkaiBuhinEditVoList(index), bBuhinEditVo)
                    End If
                Else
                    '前回の部品が存在しない場合'
                    '追加が確定'
                    '追加'

                    ' 2014/09/02 
                    '　　条件追加:前回部品がなくても今回のブロックに不要フラグが立っていないものが追加対象となる
                    If konkaiBuhinEditVoList(index).BlockFuyou.Equals("0") Then

                        CyushutuImpl.InsertByBuhinEditKaitei(konkaiBuhinEditVoList(index), shisakuListCode, shisakuListCodeKaiteiNo, "1", "")

                        Dim KonkaiGousyaVoList As New List(Of TShisakuBuhinEditVoSekkeiHelper)

                        KonkaiGousyaVoList = CyushutuImpl.FindByBuhinEditGousya(konkaiBuhinEditVoList(index).ShisakuEventCode, _
                                                                                konkaiBuhinEditVoList(index).ShisakuBukaCode, _
                                                                                konkaiBuhinEditVoList(index).ShisakuBlockNo, _
                                                                                konkaiBuhinEditVoList(index).ShisakuBlockNoKaiteiNo, _
                                                                                konkaiBuhinEditVoList(index).BuhinNoHyoujiJun, _
                                                                                True)

                        For Each KVo As TShisakuBuhinEditVoSekkeiHelper In KonkaiGousyaVoList
                            '追加処理(追加)'
                            CyushutuImpl.InsertByBuhinEditGousya(shisakuEventCode, _
                                                                 shisakuListCode, _
                                                                 shisakuListCodeKaiteiNo, _
                                                                 KVo.ShisakuBukaCode, _
                                                                 KVo.ShisakuBlockNo, _
                                                                 KVo.ShisakuBlockNoKaiteiNo, _
                                                                 "", _
                                                                 KVo.BuhinNoHyoujiJun, _
                                                                 KVo.ShisakuGousyaHyoujiJun, _
                                                                 KVo.ShisakuGousya, _
                                                                 KVo.InsuSuryo, _
                                                                 "1")
                        Next
                    End If
                End If
            Next

            '前回改訂Noの部品編集改訂情報'
            Dim zenkaiBuhinEditVoList As New List(Of TShisakuBuhinEditVoSekkeiHelper)
            '前回のリストを使って削除されたものを見つける'
            zenkaiBuhinEditVoList = CyushutuImpl.FindByBuhinEditVoSekkeiHelper(shisakuEventCode, False, Me.isIkansha)

            For index As Integer = 0 To zenkaiBuhinEditVoList.Count - 1

                '   2013/05/29　再使用不可を追加
                Dim KonkaiBuhinEditVo As New TShisakuBuhinEditVoSekkeiHelper
                KonkaiBuhinEditVo = CyushutuImpl.FindByBeforeBuhinEdit(zenkaiBuhinEditVoList(index).ShisakuEventCode, _
                                                                       zenkaiBuhinEditVoList(index).ShisakuBukaCode, _
                                                                       zenkaiBuhinEditVoList(index).ShisakuBlockNo, _
                                                                       zenkaiBuhinEditVoList(index).Level, _
                                                                       zenkaiBuhinEditVoList(index).ShukeiCode, _
                                                                       zenkaiBuhinEditVoList(index).SiaShukeiCode, _
                                                                       zenkaiBuhinEditVoList(index).BuhinNo, _
                                                                       zenkaiBuhinEditVoList(index).BuhinNoKbn, _
                                                                       zenkaiBuhinEditVoList(index).KyoukuSection, _
                                                                       zenkaiBuhinEditVoList(index).Saishiyoufuka, _
                                                                       True)

                If KonkaiBuhinEditVo Is Nothing And zenkaiBuhinEditVoList(index).BlockFuyou <> "1" Then
                    '今回に存在しないなら削除された項目'    ブロック不要の部品は除外する。
                    Dim konkaiBlockNoKaiteiNo As String = ""

                    For Each Kvo As TShisakuBuhinEditVoSekkeiHelper In konkaiBuhinEditVoList
                        If StringUtil.Equals(Kvo.ShisakuBlockNo, zenkaiBuhinEditVoList(index).ShisakuBlockNo) Then
                            konkaiBlockNoKaiteiNo = Kvo.ShisakuBlockNoKaiteiNo
                        End If

                    Next

                    CyushutuImpl.InsertByBuhinEditKaiteiDel(shisakuEventCode, _
                                                            shisakuListCode, _
                                                            shisakuListCodeKaiteiNo, _
                                                            zenkaiBuhinEditVoList(index).ShisakuBukaCode, _
                                                            zenkaiBuhinEditVoList(index).ShisakuBlockNo, _
                                                            konkaiBlockNoKaiteiNo, _
                                                            zenkaiBuhinEditVoList(index).ShisakuBlockNoKaiteiNo, _
                                                            zenkaiBuhinEditVoList(index).BuhinNoHyoujiJun, _
                                                            zenkaiBuhinEditVoList(index), _
                                                            "7")

                    Dim ZenkaiGousyaVoList As New List(Of TShisakuBuhinEditVoSekkeiHelper)
                    ZenkaiGousyaVoList = CyushutuImpl.FindByBuhinEditGousya(shisakuEventCode, _
                                                                            zenkaiBuhinEditVoList(index).ShisakuBukaCode, _
                                                                            zenkaiBuhinEditVoList(index).ShisakuBlockNo, _
                                                                            zenkaiBuhinEditVoList(index).ShisakuBlockNoKaiteiNo, _
                                                                            zenkaiBuhinEditVoList(index).BuhinNoHyoujiJun, _
                                                                            False)

                    For Each zenkaiVo As TShisakuBuhinEditVoSekkeiHelper In ZenkaiGousyaVoList
                        CyushutuImpl.InsertByBuhinEditGousyaKaiteiDel(zenkaiVo.ShisakuEventCode, _
                                                                      shisakuListCode, _
                                                                      shisakuListCodeKaiteiNo, _
                                                                      zenkaiVo.ShisakuBukaCode, _
                                                                      zenkaiVo.ShisakuBlockNo, _
                                                                      konkaiBlockNoKaiteiNo, _
                                                                      zenkaiVo.ShisakuBlockNoKaiteiNo, _
                                                                      zenkaiVo.BuhinNoHyoujiJun, _
                                                                      zenkaiVo.ShisakuGousyaHyoujiJun, _
                                                                      zenkaiVo.ShisakuGousya, _
                                                                      zenkaiVo.InsuSuryo, _
                                                                      "7")

                    Next
                End If
            Next

        End Sub

        ''' <summary>
        ''' 員数チェック(全件チェックで変更無し)
        ''' </summary>
        ''' <param name="CyushutuImpl">改訂抽出Dao</param>
        ''' <param name="buhinEditVo">部品編集情報</param>
        ''' <param name="zenkaiBlockNoKaiteiNo">前回ブロックNo改訂No</param>
        ''' <param name="zenkaiBuhinNoHyoujiJun">前回部品番号表示順</param>
        ''' <remarks></remarks>
        Private Sub InsuCheckNotChange(ByVal CyushutuImpl As KaiteiChusyutuDao, _
                                       ByVal buhinEditVo As TShisakuBuhinEditVoSekkeiHelper, _
                                       ByVal zenkaiBlockNoKaiteiNo As String, _
                                       ByVal zenkaiBuhinNoHyoujiJun As Integer)



            '今回改訂Noの部品編集号車リスト'
            Dim konkaiBuhinEditGousyaList As New List(Of TShisakuBuhinEditVoSekkeiHelper)
            '前回改訂Noの部品編集号車リスト'
            Dim zenkaiBuhinEditGousyaList As New List(Of TShisakuBuhinEditVoSekkeiHelper)

            '今回のブロックNo改訂NoでINSTL情報を取得'
            konkaiBuhinEditGousyaList = CyushutuImpl.FindByBuhinEditGousya(buhinEditVo.ShisakuEventCode, _
                                                                           buhinEditVo.ShisakuBukaCode, _
                                                                           buhinEditVo.ShisakuBlockNo, _
                                                                           buhinEditVo.ShisakuBlockNoKaiteiNo, _
                                                                           buhinEditVo.BuhinNoHyoujiJun, _
                                                                           True)

            '前回のブロックNo改訂NoでINSTL情報を取得'
            zenkaiBuhinEditGousyaList = CyushutuImpl.FindByBuhinEditGousya(buhinEditVo.ShisakuEventCode, _
                                                                           buhinEditVo.ShisakuBukaCode, _
                                                                           buhinEditVo.ShisakuBlockNo, _
                                                                           zenkaiBlockNoKaiteiNo, _
                                                                           zenkaiBuhinNoHyoujiJun, _
                                                                           False)


            '員数の合計'
            Dim NewTotalInsu As Integer = 0
            For Each konkaiVo As TShisakuBuhinEditVoSekkeiHelper In konkaiBuhinEditGousyaList
                If konkaiVo.InsuSuryo > 0 Then
                    NewTotalInsu = NewTotalInsu + konkaiVo.InsuSuryo
                End If
            Next

            '前回員数の合計'
            Dim btotalInsu As Integer = 0
            For Each zenkaiVo As TShisakuBuhinEditVoSekkeiHelper In zenkaiBuhinEditGousyaList
                If zenkaiVo.InsuSuryo > 0 Then
                    btotalInsu = btotalInsu + zenkaiVo.InsuSuryo
                End If
            Next


            'もし員数がマイナスだったら比較しない'

            '部品の員数比較'
            If NewTotalInsu < btotalInsu Then
                '員数減'
                '追加'
                'CyushutuImpl.InsertByBuhinEditKaitei(buhinEditVo, shisakuListCode, shisakuListCodeKaiteiNo, "1", "")
                ''削除'

                ''部品番号表示順がずれてるので最新の部品番号表示順と合わせる'

                ''中身は同じだから'
                'CyushutuImpl.InsertByBuhinEditKaiteiDel(buhinEditVo.ShisakuEventCode, _
                '                                        shisakuListCode, _
                '                                        shisakuListCodeKaiteiNo, _
                '                                        buhinEditVo.ShisakuBukaCode, _
                '                                        buhinEditVo.ShisakuBlockNo, _
                '                                        buhinEditVo.ShisakuBlockNoKaiteiNo, _
                '                                        zenkaiBlockNoKaiteiNo, _
                '                                        buhinEditVo.BuhinNoHyoujiJun, _
                '                                        buhinEditVo, _
                '                                        "2")

                'Dim delFlag As Boolean = False
                'Dim insusa As Integer = 0
                ''今回と前回の号車の員数を比較する'
                'If zenkaiBuhinEditGousyaList.Count > 0 Then
                '    If konkaiBuhinEditGousyaList.Count < zenkaiBuhinEditGousyaList.Count Then
                '        '今回<前回なら削除フラグを立てる'
                '        delFlag = True
                '    Else

                '        '列の総数が同じか今回>前回なら削除フラグを立てるか調べる'
                '        '今回と前回の号車の員数を比較する'
                '        For kindex As Integer = 0 To konkaiBuhinEditGousyaList.Count - 1
                '            For bindex As Integer = 0 To zenkaiBuhinEditGousyaList.Count - 1
                '                '前回の号車表示順が今回の号車表示順に存在しない場合削除フラグを立てる'
                '                If konkaiBuhinEditGousyaList(kindex).ShisakuGousyaHyoujiJun = zenkaiBuhinEditGousyaList(bindex).ShisakuGousyaHyoujiJun Then
                '                    '同一のものが存在したら削除フラグは立てない'
                '                    delFlag = False
                '                    Exit For
                '                Else
                '                    delFlag = True
                '                End If
                '            Next
                '        Next
                '    End If
                'End If


                'For kindex As Integer = 0 To konkaiBuhinEditGousyaList.Count - 1
                '    Dim a As Integer = 0
                '    Dim b As Integer = 0
                '    If zenkaiBuhinEditGousyaList.Count > 0 Then
                '        For bindex As Integer = 0 To zenkaiBuhinEditGousyaList.Count - 1
                '            If konkaiBuhinEditGousyaList(kindex).ShisakuGousyaHyoujiJun = zenkaiBuhinEditGousyaList(bindex).ShisakuGousyaHyoujiJun Then
                '                insusa = 0
                '                'マイナスは0扱いにする'
                '                If konkaiBuhinEditGousyaList(kindex).InsuSuryo > 0 Then
                '                    a = konkaiBuhinEditGousyaList(kindex).InsuSuryo
                '                End If
                '                If zenkaiBuhinEditGousyaList(bindex).InsuSuryo > 0 Then
                '                    b = zenkaiBuhinEditGousyaList(bindex).InsuSuryo
                '                End If
                '                insusa = a - b
                '                Exit For
                '            Else
                '                insusa = konkaiBuhinEditGousyaList(kindex).InsuSuryo
                '            End If
                '        Next
                '    Else
                '        '比較対象がいないから'
                '        If konkaiBuhinEditGousyaList(kindex).InsuSuryo > 0 Then
                '            insusa = konkaiBuhinEditGousyaList(kindex).InsuSuryo
                '        End If
                '    End If
                '    CyushutuImpl.InsertByBuhinEditGousya(buhinEditVo.ShisakuEventCode, _
                '                                         shisakuListCode, _
                '                                         shisakuListCodeKaiteiNo, _
                '                                         buhinEditVo.ShisakuBukaCode, _
                '                                         buhinEditVo.ShisakuBlockNo, _
                '                                         buhinEditVo.ShisakuBlockNoKaiteiNo, _
                '                                         zenkaiBlockNoKaiteiNo, _
                '                                         buhinEditVo.BuhinNoHyoujiJun, _
                '                                         konkaiBuhinEditGousyaList(kindex).ShisakuGousyaHyoujiJun, _
                '                                         konkaiBuhinEditGousyaList(kindex).ShisakuGousya, _
                '                                         insusa, _
                '                                         "1")

                'Next

                ''削除の登録'
                'For bindex As Integer = 0 To zenkaiBuhinEditGousyaList.Count - 1
                '    Dim a As Integer = 0
                '    Dim b As Integer = 0
                '    Dim del As Boolean = False
                '    For kindex As Integer = 0 To konkaiBuhinEditGousyaList.Count - 1
                '        If zenkaiBuhinEditGousyaList(bindex).ShisakuGousyaHyoujiJun = konkaiBuhinEditGousyaList(kindex).ShisakuGousyaHyoujiJun Then
                '            If konkaiBuhinEditGousyaList(kindex).InsuSuryo > 0 Then
                '                a = konkaiBuhinEditGousyaList(kindex).InsuSuryo
                '            End If
                '            If zenkaiBuhinEditGousyaList(bindex).InsuSuryo > 0 Then
                '                b = zenkaiBuhinEditGousyaList(bindex).InsuSuryo
                '            End If

                '            If (a - b) = 0 Then
                '                del = True
                '                zenkaiBuhinEditGousyaList(bindex).InsuSuryo = 0
                '                Exit For
                '            End If
                '            del = False
                '            Exit For
                '        Else
                '            del = True
                '        End If
                '    Next

                '    If del Then
                '        CyushutuImpl.InsertByBuhinEditGousyaKaiteiDel(buhinEditVo.ShisakuEventCode, _
                '                                                      shisakuListCode, _
                '                                                      shisakuListCodeKaiteiNo, _
                '                                                      buhinEditVo.ShisakuBukaCode, _
                '                                                      buhinEditVo.ShisakuBlockNo, _
                '                                                      buhinEditVo.ShisakuBlockNoKaiteiNo, _
                '                                                      zenkaiBlockNoKaiteiNo, _
                '                                                      buhinEditVo.BuhinNoHyoujiJun, _
                '                                                      zenkaiBuhinEditGousyaList(bindex).ShisakuGousyaHyoujiJun, _
                '                                                      zenkaiBuhinEditGousyaList(bindex).ShisakuGousya, _
                '                                                      zenkaiBuhinEditGousyaList(bindex).InsuSuryo, _
                '                                                      "2")
                '    End If
                '    del = False
                'Next


                '追加'
                CyushutuImpl.InsertByBuhinEditKaitei(buhinEditVo, shisakuListCode, shisakuListCodeKaiteiNo, "1", "")
                '削除でも中身は同じだから'
                CyushutuImpl.InsertByBuhinEditKaiteiDel(buhinEditVo.ShisakuEventCode, _
                                                        shisakuListCode, _
                                                        shisakuListCodeKaiteiNo, _
                                                        buhinEditVo.ShisakuBukaCode, _
                                                        buhinEditVo.ShisakuBlockNo, _
                                                        buhinEditVo.ShisakuBlockNoKaiteiNo, _
                                                        zenkaiBlockNoKaiteiNo, _
                                                        buhinEditVo.BuhinNoHyoujiJun, _
                                                        buhinEditVo, _
                                                        "2")


                For Each KVo As TShisakuBuhinEditVoSekkeiHelper In konkaiBuhinEditGousyaList
                    '今回の員数を追加'
                    '追加処理(追加)'

                    CyushutuImpl.InsertByBuhinEditGousya(shisakuEventCode, _
                                                         shisakuListCode, _
                                                         shisakuListCodeKaiteiNo, _
                                                         KVo.ShisakuBukaCode, _
                                                         KVo.ShisakuBlockNo, _
                                                         KVo.ShisakuBlockNoKaiteiNo, _
                                                         "", _
                                                         KVo.BuhinNoHyoujiJun, _
                                                         KVo.ShisakuGousyaHyoujiJun, _
                                                         KVo.ShisakuGousya, _
                                                         KVo.InsuSuryo, _
                                                         "1")
                Next

                For Each BVo As TShisakuBuhinEditVoSekkeiHelper In zenkaiBuhinEditGousyaList
                    ''前回の員数
                    '前回改訂Noの部品編集号車改訂情報'
                    '追加処理(削除)'
                    CyushutuImpl.InsertByBuhinEditGousyaKaiteiDel(buhinEditVo.ShisakuEventCode, _
                                                                  shisakuListCode, _
                                                                  shisakuListCodeKaiteiNo, _
                                                                  buhinEditVo.ShisakuBukaCode, _
                                                                  buhinEditVo.ShisakuBlockNo, _
                                                                  buhinEditVo.ShisakuBlockNoKaiteiNo, _
                                                                  zenkaiBlockNoKaiteiNo, _
                                                                  buhinEditVo.BuhinNoHyoujiJun, _
                                                                  BVo.ShisakuGousyaHyoujiJun, _
                                                                  BVo.ShisakuGousya, _
                                                                  BVo.InsuSuryo, _
                                                                  "2")
                Next



            ElseIf NewTotalInsu > btotalInsu Then

                '員数増'
                Dim delFlag As Boolean = False
                Dim insusa As Integer = 0
                '号車が一致するものがあったら立てるフラグ'
                Dim mFlag As Boolean = False
                '今回と前回の号車の員数を比較する'
                If zenkaiBuhinEditGousyaList.Count > 0 Then
                    If konkaiBuhinEditGousyaList.Count < zenkaiBuhinEditGousyaList.Count Then
                        '今回<前回なら削除フラグを立てる'
                        delFlag = True
                    Else

                        '列の総数が同じか今回>前回なら削除フラグを立てるか調べる'
                        '一度でも削除フラグが立ったら追加・削除で登録する'
                        '今回と前回の号車の員数を比較する'
                        For kindex As Integer = 0 To konkaiBuhinEditGousyaList.Count - 1
                            For bindex As Integer = 0 To zenkaiBuhinEditGousyaList.Count - 1
                                '前回の号車表示順が今回の号車表示順に存在しない場合削除フラグを立てる'
                                If konkaiBuhinEditGousyaList(kindex).ShisakuGousyaHyoujiJun = zenkaiBuhinEditGousyaList(bindex).ShisakuGousyaHyoujiJun Then
                                    '20110624 樺澤'
                                    mFlag = True
                                    '員数の比較をする'
                                    If konkaiBuhinEditGousyaList(kindex).InsuSuryo < zenkaiBuhinEditGousyaList(bindex).InsuSuryo Then
                                        '今回<前回が一箇所でもあれば削除フラグを立てる'
                                        delFlag = True
                                        Exit For
                                    End If
                                End If
                            Next
                        Next
                    End If
                    For Each Vo As TShisakuBuhinEditVoSekkeiHelper In zenkaiBuhinEditGousyaList
                        Dim f As Boolean = False
                        For Each kvo As TShisakuBuhinEditVoSekkeiHelper In konkaiBuhinEditGousyaList
                            If StringUtil.Equals(kvo.ShisakuGousya, Vo.ShisakuGousya) Then
                                f = True
                            End If
                        Next

                        If Not f Then
                            delFlag = True
                        End If
                    Next
                End If


                '削除フラグが立っていなくてもmFlagがFalseなら全ての号車表示順が不一致だった証拠なので削除フラグを立てる'
                If Not delFlag Then
                    If Not mFlag Then
                        delFlag = True
                    End If
                End If

                If delFlag Then
                    '削除フラグがTRUEなら削除、追加で登録'
                    '員数はそのままで追加'
                    '20110531 樺澤'
                    'もし員数差で登録する場合は「員数差付きで登録」をコメント解除して'
                    '「員数差無しで登録」をコメントアウトしてください'
                    For kindex As Integer = 0 To konkaiBuhinEditGousyaList.Count - 1
                        CyushutuImpl.InsertByBuhinEditGousya(buhinEditVo.ShisakuEventCode, _
                                                             shisakuListCode, _
                                                             shisakuListCodeKaiteiNo, _
                                                             buhinEditVo.ShisakuBukaCode, _
                                                             buhinEditVo.ShisakuBlockNo, _
                                                             buhinEditVo.ShisakuBlockNoKaiteiNo, _
                                                             zenkaiBlockNoKaiteiNo, _
                                                             buhinEditVo.BuhinNoHyoujiJun, _
                                                             konkaiBuhinEditGousyaList(kindex).ShisakuGousyaHyoujiJun, _
                                                             konkaiBuhinEditGousyaList(kindex).ShisakuGousya, _
                                                             konkaiBuhinEditGousyaList(kindex).InsuSuryo, _
                                                             "1")
                    Next

                    '「員数差付きで登録」'
                    '員数差を用意する'
                    'For kindex As Integer = 0 To konkaiBuhinEditGousyaList.Count - 1
                    '    Dim a As Integer = 0
                    '    Dim b As Integer = 0
                    '    If zenkaiBuhinEditGousyaList.Count > 0 Then
                    '        For bindex As Integer = 0 To zenkaiBuhinEditGousyaList.Count - 1
                    '            If konkaiBuhinEditGousyaList(kindex).ShisakuGousyaHyoujiJun = zenkaiBuhinEditGousyaList(bindex).ShisakuGousyaHyoujiJun Then
                    '                insusa = 0
                    '                'マイナスは0扱いにする() '
                    '                If konkaiBuhinEditGousyaList(kindex).InsuSuryo > 0 Then
                    '                    a = konkaiBuhinEditGousyaList(kindex).InsuSuryo
                    '                End If
                    '                If zenkaiBuhinEditGousyaList(bindex).InsuSuryo > 0 Then
                    '                    b = zenkaiBuhinEditGousyaList(bindex).InsuSuryo
                    '                End If
                    '                insusa = a - b
                    '                Exit For
                    '            Else
                    '                insusa = konkaiBuhinEditGousyaList(kindex).InsuSuryo
                    '            End If
                    '        Next
                    '    Else
                    '        '比較対象がいないから() '
                    '        If konkaiBuhinEditGousyaList(kindex).InsuSuryo > 0 Then
                    '            insusa = konkaiBuhinEditGousyaList(kindex).InsuSuryo
                    '        End If
                    '    End If
                    '    CyushutuImpl.InsertByBuhinEditGousya(buhinEditVo.ShisakuEventCode, _
                    '                                         shisakuListCode, _
                    '                                         shisakuListCodeKaiteiNo, _
                    '                                         buhinEditVo.ShisakuBukaCode, _
                    '                                         buhinEditVo.ShisakuBlockNo, _
                    '                                         buhinEditVo.ShisakuBlockNoKaiteiNo, _
                    '                                         zenkaiBlockNoKaiteiNo, _
                    '                                         buhinEditVo.BuhinNoHyoujiJun, _
                    '                                         konkaiBuhinEditGousyaList(kindex).ShisakuGousyaHyoujiJun, _
                    '                                         konkaiBuhinEditGousyaList(kindex).ShisakuGousya, _
                    '                                         insusa, _
                    '                                         "1")

                    'Next

                    '員数すべて登録'
                    For bindex As Integer = 0 To zenkaiBuhinEditGousyaList.Count - 1
                        CyushutuImpl.InsertByBuhinEditGousyaKaiteiDel(buhinEditVo.ShisakuEventCode, _
                          shisakuListCode, _
                          shisakuListCodeKaiteiNo, _
                          buhinEditVo.ShisakuBukaCode, _
                          buhinEditVo.ShisakuBlockNo, _
                          buhinEditVo.ShisakuBlockNoKaiteiNo, _
                          zenkaiBlockNoKaiteiNo, _
                          buhinEditVo.BuhinNoHyoujiJun, _
                          zenkaiBuhinEditGousyaList(bindex).ShisakuGousyaHyoujiJun, _
                          zenkaiBuhinEditGousyaList(bindex).ShisakuGousya, _
                          zenkaiBuhinEditGousyaList(bindex).InsuSuryo, _
                          "2")
                    Next



                    '削除の登録'
                    'For bindex As Integer = 0 To zenkaiBuhinEditGousyaList.Count - 1
                    '    Dim a As Integer = 0
                    '    Dim b As Integer = 0
                    '    Dim del As Boolean = False
                    '    For kindex As Integer = 0 To konkaiBuhinEditGousyaList.Count - 1
                    '        If zenkaiBuhinEditGousyaList(bindex).ShisakuGousyaHyoujiJun = konkaiBuhinEditGousyaList(kindex).ShisakuGousyaHyoujiJun Then
                    '            If konkaiBuhinEditGousyaList(kindex).InsuSuryo > 0 Then
                    '                a = konkaiBuhinEditGousyaList(kindex).InsuSuryo
                    '            End If
                    '            If zenkaiBuhinEditGousyaList(bindex).InsuSuryo > 0 Then
                    '                b = zenkaiBuhinEditGousyaList(bindex).InsuSuryo
                    '            End If

                    '            If (a - b) = 0 Then
                    '                del = True
                    '                zenkaiBuhinEditGousyaList(bindex).InsuSuryo = 0
                    '                Exit For
                    '            End If
                    '            del = False
                    '            Exit For
                    '        Else
                    '            del = True
                    '        End If
                    '    Next

                    '    If del Then
                    '        CyushutuImpl.InsertByBuhinEditGousyaKaiteiDel(buhinEditVo.ShisakuEventCode, _
                    '                                                      shisakuListCode, _
                    '                                                      shisakuListCodeKaiteiNo, _
                    '                                                      buhinEditVo.ShisakuBukaCode, _
                    '                                                      buhinEditVo.ShisakuBlockNo, _
                    '                                                      buhinEditVo.ShisakuBlockNoKaiteiNo, _
                    '                                                      zenkaiBlockNoKaiteiNo, _
                    '                                                      buhinEditVo.BuhinNoHyoujiJun, _
                    '                                                      zenkaiBuhinEditGousyaList(bindex).ShisakuGousyaHyoujiJun, _
                    '                                                      zenkaiBuhinEditGousyaList(bindex).ShisakuGousya, _
                    '                                                      zenkaiBuhinEditGousyaList(bindex).InsuSuryo, _
                    '                                                      "2")
                    '    End If
                    '    del = False
                    'Next


                    '追加'
                    CyushutuImpl.InsertByBuhinEditKaitei(buhinEditVo, shisakuListCode, shisakuListCodeKaiteiNo, "1", "")
                    '削除でも中身は同じだから'
                    CyushutuImpl.InsertByBuhinEditKaiteiDel(buhinEditVo.ShisakuEventCode, _
                                                            shisakuListCode, _
                                                            shisakuListCodeKaiteiNo, _
                                                            buhinEditVo.ShisakuBukaCode, _
                                                            buhinEditVo.ShisakuBlockNo, _
                                                            buhinEditVo.ShisakuBlockNoKaiteiNo, _
                                                            zenkaiBlockNoKaiteiNo, _
                                                            buhinEditVo.BuhinNoHyoujiJun, _
                                                            buhinEditVo, _
                                                            "2")
                Else

                    '員数差を用意する'
                    For kindex As Integer = 0 To konkaiBuhinEditGousyaList.Count - 1
                        Dim a As Integer = 0
                        Dim b As Integer = 0
                        If zenkaiBuhinEditGousyaList.Count > 0 Then
                            For bindex As Integer = 0 To zenkaiBuhinEditGousyaList.Count - 1
                                If konkaiBuhinEditGousyaList(kindex).ShisakuGousyaHyoujiJun = zenkaiBuhinEditGousyaList(bindex).ShisakuGousyaHyoujiJun Then
                                    insusa = 0
                                    'マイナスは0扱いにする'
                                    If konkaiBuhinEditGousyaList(kindex).InsuSuryo > 0 Then
                                        a = konkaiBuhinEditGousyaList(kindex).InsuSuryo
                                    End If
                                    If zenkaiBuhinEditGousyaList(bindex).InsuSuryo > 0 Then
                                        b = zenkaiBuhinEditGousyaList(bindex).InsuSuryo
                                    End If

                                    insusa = a - b
                                    '20110617 kabasawa'
                                    '員数差が０未満の場合は今回の員数を員数差として登録させる'
                                    If insusa < 0 Then
                                        '上で今回の員数がマイナスの場合は０にしているがこの場合はどうする？'
                                        insusa = a
                                    End If
                                    Exit For
                                Else
                                    insusa = konkaiBuhinEditGousyaList(kindex).InsuSuryo
                                End If
                            Next
                        Else
                            '比較対象がいないから'
                            If konkaiBuhinEditGousyaList(kindex).InsuSuryo > 0 Then
                                insusa = konkaiBuhinEditGousyaList(kindex).InsuSuryo
                            End If
                        End If

                        CyushutuImpl.InsertByBuhinEditGousya(buhinEditVo.ShisakuEventCode, _
                                                             shisakuListCode, _
                                                             shisakuListCodeKaiteiNo, _
                                                             buhinEditVo.ShisakuBukaCode, _
                                                             buhinEditVo.ShisakuBlockNo, _
                                                             buhinEditVo.ShisakuBlockNoKaiteiNo, _
                                                             zenkaiBlockNoKaiteiNo, _
                                                             buhinEditVo.BuhinNoHyoujiJun, _
                                                             konkaiBuhinEditGousyaList(kindex).ShisakuGousyaHyoujiJun, _
                                                             konkaiBuhinEditGousyaList(kindex).ShisakuGousya, _
                                                             insusa, _
                                                             "1")

                    Next

                    For Each BVo As TShisakuBuhinEditVoSekkeiHelper In zenkaiBuhinEditGousyaList
                        CyushutuImpl.InsertByBuhinEditGousya(buhinEditVo.ShisakuEventCode, _
                                                             shisakuListCode, _
                                                             shisakuListCodeKaiteiNo, _
                                                             buhinEditVo.ShisakuBukaCode, _
                                                             buhinEditVo.ShisakuBlockNo, _
                                                             buhinEditVo.ShisakuBlockNoKaiteiNo, _
                                                             "", _
                                                             buhinEditVo.BuhinNoHyoujiJun, _
                                                             BVo.ShisakuGousyaHyoujiJun, _
                                                             BVo.ShisakuGousya, _
                                                             BVo.InsuSuryo, _
                                                             "0")

                    Next
                    '追加'
                    CyushutuImpl.InsertByBuhinEditKaitei(buhinEditVo, shisakuListCode, shisakuListCodeKaiteiNo, "1", "")
                    '変更無しで元の情報を追加'
                    CyushutuImpl.InsertByBuhinEditKaitei(buhinEditVo, shisakuListCode, shisakuListCodeKaiteiNo, "0", "")
                End If



            ElseIf NewTotalInsu = btotalInsu Then
                '変更無し'
                '追加'

                '合計員数に変更が無くても号車ごとで見ると差異がある場合があるのでチェックする'
                Dim changeFlag As Boolean = False

                'パターン１号車の総数に差異があるパターン'
                If Not konkaiBuhinEditGousyaList.Count = zenkaiBuhinEditGousyaList.Count Then
                    changeFlag = True
                Else
                    'パターン２号車の総数はあっているが適用のある号車が異なっているパターン'
                    For index As Integer = 0 To konkaiBuhinEditGousyaList.Count - 1
                        If Not konkaiBuhinEditGousyaList(index).ShisakuGousyaHyoujiJun = zenkaiBuhinEditGousyaList(index).ShisakuGousyaHyoujiJun Then
                            changeFlag = True
                            Exit For
                        Else
                            'パターン３号車の適用もあっているが員数に差があるパターン'

                            Dim konkaiInsu As Integer = konkaiBuhinEditGousyaList(index).InsuSuryo
                            Dim zenkaiInsu As Integer = zenkaiBuhinEditGousyaList(index).InsuSuryo
                            If konkaiBuhinEditGousyaList(index).InsuSuryo < 0 Then
                                konkaiInsu = 0
                            End If
                            If zenkaiBuhinEditGousyaList(index).InsuSuryo < 0 Then
                                zenkaiInsu = 0
                            End If

                            If Not konkaiInsu = zenkaiInsu Then
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
                    CyushutuImpl.InsertByBuhinEditKaitei(buhinEditVo, shisakuListCode, shisakuListCodeKaiteiNo, "3", "")

                    For Each kVo As TShisakuBuhinEditVoSekkeiHelper In konkaiBuhinEditGousyaList
                        '追加処理(追加)'
                        CyushutuImpl.InsertByBuhinEditGousya(buhinEditVo.ShisakuEventCode, _
                                                             shisakuListCode, _
                                                             shisakuListCodeKaiteiNo, _
                                                             buhinEditVo.ShisakuBukaCode, _
                                                             buhinEditVo.ShisakuBlockNo, _
                                                             buhinEditVo.ShisakuBlockNoKaiteiNo, _
                                                             "", _
                                                             buhinEditVo.BuhinNoHyoujiJun, _
                                                             kVo.ShisakuGousyaHyoujiJun, _
                                                             kVo.ShisakuGousya, _
                                                             kVo.InsuSuryo, _
                                                             "3")
                    Next

                    Dim konkaiBlockNoKaiteiNo As String = buhinEditVo.ShisakuBlockNoKaiteiNo
                    buhinEditVo.ShisakuBlockNoKaiteiNo = zenkaiBlockNoKaiteiNo

                    '変更前'
                    CyushutuImpl.InsertByBuhinEditKaiteiDel(buhinEditVo.ShisakuEventCode, _
                                                            shisakuListCode, _
                                                            shisakuListCodeKaiteiNo, _
                                                            buhinEditVo.ShisakuBukaCode, _
                                                            buhinEditVo.ShisakuBlockNo, _
                                                            buhinEditVo.ShisakuBlockNoKaiteiNo, _
                                                            zenkaiBlockNoKaiteiNo, _
                                                            buhinEditVo.BuhinNoHyoujiJun, _
                                                            buhinEditVo, _
                                                            "4")

                    '前回改訂Noの部品編集号車改訂情報'
                    '追加処理(変更前)'

                    For Each BVo As TShisakuBuhinEditVoSekkeiHelper In zenkaiBuhinEditGousyaList
                        CyushutuImpl.InsertByBuhinEditGousyaKaiteiDel(buhinEditVo.ShisakuEventCode, _
                                                  shisakuListCode, _
                                                  shisakuListCodeKaiteiNo, _
                                                  buhinEditVo.ShisakuBukaCode, _
                                                  buhinEditVo.ShisakuBlockNo, _
                                                  buhinEditVo.ShisakuBlockNoKaiteiNo, _
                                                  zenkaiBlockNoKaiteiNo, _
                                                  buhinEditVo.BuhinNoHyoujiJun, _
                                                  BVo.ShisakuGousyaHyoujiJun, _
                                                  BVo.ShisakuGousya, _
                                                  BVo.InsuSuryo, _
                                                  "4")
                    Next


                Else
                    '変更箇所が無ければ変更無しで追加'
                    CyushutuImpl.InsertByBuhinEditKaitei(buhinEditVo, shisakuListCode, shisakuListCodeKaiteiNo, "0", "")

                    For Each KVo As TShisakuBuhinEditVoSekkeiHelper In konkaiBuhinEditGousyaList
                        CyushutuImpl.InsertByBuhinEditGousya(buhinEditVo.ShisakuEventCode, _
                                                             shisakuListCode, _
                                                             shisakuListCodeKaiteiNo, _
                                                             buhinEditVo.ShisakuBukaCode, _
                                                             buhinEditVo.ShisakuBlockNo, _
                                                             buhinEditVo.ShisakuBlockNoKaiteiNo, _
                                                             "", _
                                                             buhinEditVo.BuhinNoHyoujiJun, _
                                                             KVo.ShisakuGousyaHyoujiJun, _
                                                             KVo.ShisakuGousya, _
                                                             KVo.InsuSuryo, _
                                                             "0")
                    Next
                End If
            End If

        End Sub

        ''' <summary>
        ''' 員数チェック(変更有り)
        ''' </summary>
        ''' <param name="CyushutuImpl">改訂抽出Dao</param>
        ''' <param name="buhinEditVo">部品編集情報</param>
        ''' <param name="zenkaiBuhinEditVo">前回部品編集情報</param>
        ''' <remarks></remarks>
        Private Sub InsuCheckChange(ByVal CyushutuImpl As KaiteiChusyutuDao, ByVal buhinEditVo As TShisakuBuhinEditVoSekkeiHelper, ByVal zenkaiBuhinEditVo As TShisakuBuhinEditVoSekkeiHelper)

            '今回改訂Noの部品編集号車リスト'
            Dim konkaiBuhinEditGousyaList As New List(Of TShisakuBuhinEditVoSekkeiHelper)
            '前回改訂Noの部品編集号車リスト'
            Dim zenkaiBuhinEditGousyaList As New List(Of TShisakuBuhinEditVoSekkeiHelper)

            '今回のブロックNo改訂NoでINSTL情報を取得'
            konkaiBuhinEditGousyaList = CyushutuImpl.FindByBuhinEditGousya(buhinEditVo.ShisakuEventCode, _
                                                                           buhinEditVo.ShisakuBukaCode, _
                                                                           buhinEditVo.ShisakuBlockNo, _
                                                                           buhinEditVo.ShisakuBlockNoKaiteiNo, _
                                                                           buhinEditVo.BuhinNoHyoujiJun, _
                                                                           True)

            '今回のブロックNo改訂NoでINSTL情報を取得'
            zenkaiBuhinEditGousyaList = CyushutuImpl.FindByBuhinEditGousya(buhinEditVo.ShisakuEventCode, _
                                                                           buhinEditVo.ShisakuBukaCode, _
                                                                           buhinEditVo.ShisakuBlockNo, _
                                                                           zenkaiBuhinEditVo.ShisakuBlockNoKaiteiNo, _
                                                                           zenkaiBuhinEditVo.BuhinNoHyoujiJun, _
                                                                           False)
            '員数の合計'
            Dim NewTotalInsu As Integer = 0
            For Each konkaiVo As TShisakuBuhinEditVoSekkeiHelper In konkaiBuhinEditGousyaList
                If konkaiVo.InsuSuryo > 0 Then
                    NewTotalInsu = NewTotalInsu + konkaiVo.InsuSuryo
                End If
            Next

            '前回員数の合計'
            Dim btotalInsu As Integer = 0
            For Each zenkaiVo As TShisakuBuhinEditVoSekkeiHelper In zenkaiBuhinEditGousyaList
                If zenkaiVo.InsuSuryo > 0 Then
                    btotalInsu = btotalInsu + zenkaiVo.InsuSuryo
                End If
            Next

            'カウント数比較'
            If NewTotalInsu < btotalInsu Then
                '員数減'
                'Dim delFlag As Boolean = False
                Dim insusa As Integer = 0
                ''今回と前回の号車の員数を比較する'
                'If zenkaiBuhinEditGousyaList.Count > 0 Then
                '    If konkaiBuhinEditGousyaList.Count < zenkaiBuhinEditGousyaList.Count Then
                '        '今回<前回なら削除フラグを立てる'
                '        delFlag = True
                '    Else

                '        '列の総数が同じか今回>前回なら削除フラグを立てるか調べる'
                '        '今回と前回の号車の員数を比較する'
                '        For kindex As Integer = 0 To konkaiBuhinEditGousyaList.Count - 1
                '            For bindex As Integer = 0 To zenkaiBuhinEditGousyaList.Count - 1
                '                '前回の号車表示順が今回の号車表示順に存在しない場合削除フラグを立てる'
                '                If konkaiBuhinEditGousyaList(kindex).ShisakuGousyaHyoujiJun = zenkaiBuhinEditGousyaList(bindex).ShisakuGousyaHyoujiJun Then
                '                    '同一のものが存在したら削除フラグは立てない'
                '                    delFlag = False
                '                    Exit For
                '                Else
                '                    delFlag = True
                '                End If
                '            Next
                '        Next
                '    End If
                'End If


                'For kindex As Integer = 0 To konkaiBuhinEditGousyaList.Count - 1
                '    Dim a As Integer = 0
                '    Dim b As Integer = 0
                '    If zenkaiBuhinEditGousyaList.Count > 0 Then
                '        For bindex As Integer = 0 To zenkaiBuhinEditGousyaList.Count - 1
                '            If konkaiBuhinEditGousyaList(kindex).ShisakuGousyaHyoujiJun = zenkaiBuhinEditGousyaList(bindex).ShisakuGousyaHyoujiJun Then
                '                insusa = 0
                '                'マイナスは0扱いにする'
                '                If konkaiBuhinEditGousyaList(kindex).InsuSuryo > 0 Then
                '                    a = konkaiBuhinEditGousyaList(kindex).InsuSuryo
                '                End If
                '                If zenkaiBuhinEditGousyaList(bindex).InsuSuryo > 0 Then
                '                    b = zenkaiBuhinEditGousyaList(bindex).InsuSuryo
                '                End If
                '                insusa = a - b
                '                Exit For
                '            Else
                '                insusa = konkaiBuhinEditGousyaList(kindex).InsuSuryo
                '            End If
                '        Next
                '    Else
                '        '比較対象がいないから'
                '        If konkaiBuhinEditGousyaList(kindex).InsuSuryo > 0 Then
                '            insusa = konkaiBuhinEditGousyaList(kindex).InsuSuryo
                '        End If
                '    End If
                '    CyushutuImpl.InsertByBuhinEditGousya(buhinEditVo.ShisakuEventCode, _
                '                                         shisakuListCode, _
                '                                         shisakuListCodeKaiteiNo, _
                '                                         buhinEditVo.ShisakuBukaCode, _
                '                                         buhinEditVo.ShisakuBlockNo, _
                '                                         buhinEditVo.ShisakuBlockNoKaiteiNo, _
                '                                         zenkaiBuhinEditVo.ShisakuBlockNoKaiteiNo, _
                '                                         buhinEditVo.BuhinNoHyoujiJun, _
                '                                         konkaiBuhinEditGousyaList(kindex).ShisakuGousyaHyoujiJun, _
                '                                         konkaiBuhinEditGousyaList(kindex).ShisakuGousya, _
                '                                         insusa, _
                '                                         "1")

                'Next

                ''削除の登録'
                'For bindex As Integer = 0 To zenkaiBuhinEditGousyaList.Count - 1
                '    Dim a As Integer = 0
                '    Dim b As Integer = 0
                '    Dim del As Boolean = False
                '    For kindex As Integer = 0 To konkaiBuhinEditGousyaList.Count - 1
                '        If zenkaiBuhinEditGousyaList(bindex).ShisakuGousyaHyoujiJun = konkaiBuhinEditGousyaList(kindex).ShisakuGousyaHyoujiJun Then
                '            If konkaiBuhinEditGousyaList(kindex).InsuSuryo > 0 Then
                '                a = konkaiBuhinEditGousyaList(kindex).InsuSuryo
                '            End If
                '            If zenkaiBuhinEditGousyaList(bindex).InsuSuryo > 0 Then
                '                b = zenkaiBuhinEditGousyaList(bindex).InsuSuryo
                '            End If

                '            If (a - b) = 0 Then
                '                del = True
                '                zenkaiBuhinEditGousyaList(bindex).InsuSuryo = 0
                '                Exit For
                '            End If
                '            del = False
                '            Exit For
                '        Else
                '            del = True
                '        End If
                '    Next

                '    If del Then
                '        CyushutuImpl.InsertByBuhinEditGousyaKaiteiDel(buhinEditVo.ShisakuEventCode, _
                '                                                      shisakuListCode, _
                '                                                      shisakuListCodeKaiteiNo, _
                '                                                      buhinEditVo.ShisakuBukaCode, _
                '                                                      buhinEditVo.ShisakuBlockNo, _
                '                                                      buhinEditVo.ShisakuBlockNoKaiteiNo, _
                '                                                      zenkaiBuhinEditVo.ShisakuBlockNoKaiteiNo, _
                '                                                      buhinEditVo.BuhinNoHyoujiJun, _
                '                                                      zenkaiBuhinEditGousyaList(bindex).ShisakuGousyaHyoujiJun, _
                '                                                      zenkaiBuhinEditGousyaList(bindex).ShisakuGousya, _
                '                                                      zenkaiBuhinEditGousyaList(bindex).InsuSuryo, _
                '                                                      "2")
                '    End If
                '    del = False
                'Next


                '追加'
                CyushutuImpl.InsertByBuhinEditKaitei(buhinEditVo, shisakuListCode, shisakuListCodeKaiteiNo, "1", "")
                '削除'
                '中身は違うから'
                CyushutuImpl.InsertByBuhinEditKaiteiDel(buhinEditVo.ShisakuEventCode, _
                                                        shisakuListCode, _
                                                        shisakuListCodeKaiteiNo, _
                                                        buhinEditVo.ShisakuBukaCode, _
                                                        buhinEditVo.ShisakuBlockNo, _
                                                        buhinEditVo.ShisakuBlockNoKaiteiNo, _
                                                        zenkaiBuhinEditVo.ShisakuBlockNoKaiteiNo, _
                                                        buhinEditVo.BuhinNoHyoujiJun, _
                                                        zenkaiBuhinEditVo, _
                                                        "2")
                '部品編集号車改訂を追加'
                '最新の員数'

                For Each KVo As TShisakuBuhinEditVoSekkeiHelper In konkaiBuhinEditGousyaList
                    CyushutuImpl.InsertByBuhinEditGousya(buhinEditVo.ShisakuEventCode, _
                                                         shisakuListCode, _
                                                         shisakuListCodeKaiteiNo, _
                                                         buhinEditVo.ShisakuBukaCode, _
                                                         buhinEditVo.ShisakuBlockNo, _
                                                         buhinEditVo.ShisakuBlockNoKaiteiNo, _
                                                         "", _
                                                         buhinEditVo.BuhinNoHyoujiJun, _
                                                         KVo.ShisakuGousyaHyoujiJun, _
                                                         KVo.ShisakuGousya, _
                                                         KVo.InsuSuryo, _
                                                         "1")
                Next



                For Each BVo As TShisakuBuhinEditVoSekkeiHelper In zenkaiBuhinEditGousyaList
                    '前回改訂Noの部品編集号車改訂情報'
                    CyushutuImpl.InsertByBuhinEditGousyaKaiteiDel(buhinEditVo.ShisakuEventCode, _
                                                                  shisakuListCode, _
                                                                  shisakuListCodeKaiteiNo, _
                                                                  buhinEditVo.ShisakuBukaCode, _
                                                                  buhinEditVo.ShisakuBlockNo, _
                                                                  buhinEditVo.ShisakuBlockNoKaiteiNo, _
                                                                  zenkaiBuhinEditVo.ShisakuBlockNoKaiteiNo, _
                                                                  buhinEditVo.BuhinNoHyoujiJun, _
                                                                  BVo.ShisakuGousyaHyoujiJun, _
                                                                  BVo.ShisakuGousya, _
                                                                  BVo.InsuSuryo, _
                                                                  "2")
                Next

            ElseIf NewTotalInsu > btotalInsu Then
                '員数増'

                Dim insusa As Integer = 0
                '削除フラグ'
                Dim delFlag As Boolean = False
                '号車が一致するものがあったら立てるフラグ'
                Dim mFlag As Boolean = False
                '削除部分があるかチェックする'
                If zenkaiBuhinEditGousyaList.Count > 0 Then
                    If konkaiBuhinEditGousyaList.Count < zenkaiBuhinEditGousyaList.Count Then
                        '今回<前回なら削除フラグを立てる'
                        delFlag = True
                    Else

                        '列の総数が同じか今回>前回なら削除フラグを立てるか調べる'
                        '今回と前回の号車の員数を比較する'
                        For kindex As Integer = 0 To konkaiBuhinEditGousyaList.Count - 1
                            For bindex As Integer = 0 To zenkaiBuhinEditGousyaList.Count - 1
                                '前回の号車表示順が今回の号車表示順に存在しない場合削除フラグを立てる'
                                If konkaiBuhinEditGousyaList(kindex).ShisakuGousyaHyoujiJun = zenkaiBuhinEditGousyaList(bindex).ShisakuGousyaHyoujiJun Then
                                    '20110624 樺澤'
                                    mFlag = True
                                    '員数の比較をする'
                                    If konkaiBuhinEditGousyaList(kindex).InsuSuryo < zenkaiBuhinEditGousyaList(bindex).InsuSuryo Then
                                        '今回<前回が一箇所でもあれば削除フラグを立てる'
                                        delFlag = True
                                        Exit For
                                    End If
                                End If
                            Next
                        Next
                        If Not delFlag Then
                            For Each Vo As TShisakuBuhinEditVoSekkeiHelper In zenkaiBuhinEditGousyaList
                                Dim f As Boolean = False
                                For Each kvo As TShisakuBuhinEditVoSekkeiHelper In konkaiBuhinEditGousyaList
                                    If StringUtil.Equals(kvo.ShisakuGousya, Vo.ShisakuGousya) Then
                                        f = True
                                    End If
                                Next

                                If Not f Then
                                    delFlag = True
                                End If
                            Next
                        End If

                    End If
                End If


                '削除フラグが立っていなくてもmFlagがFalseなら全ての号車表示順が不一致だった証拠なので削除フラグを立てる'
                If Not delFlag Then
                    If Not mFlag Then
                        delFlag = True
                    End If
                End If


                If delFlag Then
                    '削除、追加で登録'
                    '員数はそのままで追加'
                    '20110531 樺澤'
                    'もし員数差で登録する場合は「員数差付きで登録」をコメント解除して'
                    '「員数差無しで登録」をコメントアウトしてください'

                    '「員数差無しで登録」'
                    For kindex As Integer = 0 To konkaiBuhinEditGousyaList.Count - 1
                        CyushutuImpl.InsertByBuhinEditGousya(buhinEditVo.ShisakuEventCode, _
                                                             shisakuListCode, _
                                                             shisakuListCodeKaiteiNo, _
                                                             buhinEditVo.ShisakuBukaCode, _
                                                             buhinEditVo.ShisakuBlockNo, _
                                                             buhinEditVo.ShisakuBlockNoKaiteiNo, _
                                                             zenkaiBuhinEditVo.ShisakuBlockNoKaiteiNo, _
                                                             buhinEditVo.BuhinNoHyoujiJun, _
                                                             konkaiBuhinEditGousyaList(kindex).ShisakuGousyaHyoujiJun, _
                                                             konkaiBuhinEditGousyaList(kindex).ShisakuGousya, _
                                                             konkaiBuhinEditGousyaList(kindex).InsuSuryo, _
                                                             "1")

                    Next

                    '「員数差付きで登録」'
                    '員数差を用意する'
                    'For kindex As Integer = 0 To konkaiBuhinEditGousyaList.Count - 1
                    '    For bindex As Integer = 0 To zenkaiBuhinEditGousyaList.Count - 1
                    '        If konkaiBuhinEditGousyaList(kindex).ShisakuGousyaHyoujiJun = zenkaiBuhinEditGousyaList(bindex).ShisakuGousyaHyoujiJun Then
                    '            Dim a As Integer = 0
                    '            Dim b As Integer = 0
                    '            insusa = 0
                    '            'マイナスは0扱いにする'
                    '            If konkaiBuhinEditGousyaList(kindex).InsuSuryo > 0 Then
                    '                a = konkaiBuhinEditGousyaList(kindex).InsuSuryo
                    '            End If
                    '            If zenkaiBuhinEditGousyaList(bindex).InsuSuryo > 0 Then
                    '                b = zenkaiBuhinEditGousyaList(bindex).InsuSuryo
                    '            End If
                    '            insusa = a - b
                    '            Exit For
                    '        End If
                    '    Next
                    '    CyushutuImpl.InsertByBuhinEditGousya(buhinEditVo.ShisakuEventCode, _
                    '                                         shisakuListCode, _
                    '                                         shisakuListCodeKaiteiNo, _
                    '                                         buhinEditVo.ShisakuBukaCode, _
                    '                                         buhinEditVo.ShisakuBlockNo, _
                    '                                         buhinEditVo.ShisakuBlockNoKaiteiNo, _
                    '                                         zenkaiBuhinEditVo.ShisakuBlockNoKaiteiNo, _
                    '                                         buhinEditVo.BuhinNoHyoujiJun, _
                    '                                         konkaiBuhinEditGousyaList(kindex).ShisakuGousyaHyoujiJun, _
                    '                                         konkaiBuhinEditGousyaList(kindex).ShisakuGousya, _
                    '                                         insusa, _
                    '                                         "1")

                    'Next

                    '削除の登録'
                    'For bindex As Integer = 0 To zenkaiBuhinEditGousyaList.Count - 1
                    '    Dim a As Integer = 0
                    '    Dim b As Integer = 0
                    '    Dim del As Boolean = False
                    '    For kindex As Integer = 0 To konkaiBuhinEditGousyaList.Count - 1
                    '        If zenkaiBuhinEditGousyaList(bindex).ShisakuGousyaHyoujiJun = konkaiBuhinEditGousyaList(kindex).ShisakuGousyaHyoujiJun Then
                    '            If konkaiBuhinEditGousyaList(kindex).InsuSuryo > 0 Then
                    '                a = konkaiBuhinEditGousyaList(kindex).InsuSuryo
                    '            End If
                    '            If zenkaiBuhinEditGousyaList(bindex).InsuSuryo > 0 Then
                    '                b = zenkaiBuhinEditGousyaList(bindex).InsuSuryo
                    '            End If

                    '            If (a - b) = 0 Then
                    '                del = True
                    '                zenkaiBuhinEditGousyaList(bindex).InsuSuryo = 0
                    '                Exit For
                    '            End If
                    '            del = False
                    '            Exit For
                    '        Else
                    '            del = True
                    '        End If
                    '    Next

                    '    If del Then
                    '        CyushutuImpl.InsertByBuhinEditGousyaKaiteiDel(buhinEditVo.ShisakuEventCode, _
                    '                                                      shisakuListCode, _
                    '                                                      shisakuListCodeKaiteiNo, _
                    '                                                      buhinEditVo.ShisakuBukaCode, _
                    '                                                      buhinEditVo.ShisakuBlockNo, _
                    '                                                      buhinEditVo.ShisakuBlockNoKaiteiNo, _
                    '                                                      zenkaiBuhinEditVo.ShisakuBlockNoKaiteiNo, _
                    '                                                      buhinEditVo.BuhinNoHyoujiJun, _
                    '                                                      zenkaiBuhinEditGousyaList(bindex).ShisakuGousyaHyoujiJun, _
                    '                                                      zenkaiBuhinEditGousyaList(bindex).ShisakuGousya, _
                    '                                                      zenkaiBuhinEditGousyaList(bindex).InsuSuryo, _
                    '                                                      "2")
                    '    End If
                    '    del = False
                    'Next

                    For Each BVo As TShisakuBuhinEditVoSekkeiHelper In zenkaiBuhinEditGousyaList
                        CyushutuImpl.InsertByBuhinEditGousyaKaiteiDel(buhinEditVo.ShisakuEventCode, _
                                                                      shisakuListCode, _
                                                                      shisakuListCodeKaiteiNo, _
                                                                      buhinEditVo.ShisakuBukaCode, _
                                                                      buhinEditVo.ShisakuBlockNo, _
                                                                      buhinEditVo.ShisakuBlockNoKaiteiNo, _
                                                                      zenkaiBuhinEditVo.ShisakuBlockNoKaiteiNo, _
                                                                      buhinEditVo.BuhinNoHyoujiJun, _
                                                                      BVo.ShisakuGousyaHyoujiJun, _
                                                                      BVo.ShisakuGousya, _
                                                                      BVo.InsuSuryo, _
                                                                      "2")

                    Next

                    '追加'
                    CyushutuImpl.InsertByBuhinEditKaitei(buhinEditVo, shisakuListCode, shisakuListCodeKaiteiNo, "1", "")

                    '削除'
                    '中身は違うから'
                    CyushutuImpl.InsertByBuhinEditKaiteiDel(buhinEditVo.ShisakuEventCode, _
                                                            shisakuListCode, _
                                                            shisakuListCodeKaiteiNo, _
                                                            buhinEditVo.ShisakuBukaCode, _
                                                            buhinEditVo.ShisakuBlockNo, _
                                                            buhinEditVo.ShisakuBlockNoKaiteiNo, _
                                                            zenkaiBuhinEditVo.ShisakuBlockNoKaiteiNo, _
                                                            buhinEditVo.BuhinNoHyoujiJun, _
                                                            zenkaiBuhinEditVo, _
                                                            "2")


                Else
                    '員数差を用意する'
                    For kindex As Integer = 0 To konkaiBuhinEditGousyaList.Count - 1
                        For bindex As Integer = 0 To zenkaiBuhinEditGousyaList.Count - 1
                            If konkaiBuhinEditGousyaList(kindex).ShisakuGousyaHyoujiJun = zenkaiBuhinEditGousyaList(bindex).ShisakuGousyaHyoujiJun Then
                                Dim a As Integer = 0
                                Dim b As Integer = 0
                                insusa = 0
                                'マイナスは0扱いにする'
                                If konkaiBuhinEditGousyaList(kindex).InsuSuryo > 0 Then
                                    a = konkaiBuhinEditGousyaList(kindex).InsuSuryo
                                End If
                                If zenkaiBuhinEditGousyaList(bindex).InsuSuryo > 0 Then
                                    b = zenkaiBuhinEditGousyaList(bindex).InsuSuryo
                                End If
                                insusa = a - b
                                '20110617 kabasawa'
                                '員数差が０未満の場合は今回の員数を員数差として登録させる'
                                If insusa < 0 Then
                                    '上で今回の員数がマイナスの場合は０にしているがこの場合はどうする？'
                                    insusa = a
                                End If

                                Exit For
                            Else
                                insusa = konkaiBuhinEditGousyaList(kindex).InsuSuryo
                            End If
                        Next




                        CyushutuImpl.InsertByBuhinEditGousya(buhinEditVo.ShisakuEventCode, _
                                                             shisakuListCode, _
                                                             shisakuListCodeKaiteiNo, _
                                                             buhinEditVo.ShisakuBukaCode, _
                                                             buhinEditVo.ShisakuBlockNo, _
                                                             buhinEditVo.ShisakuBlockNoKaiteiNo, _
                                                             zenkaiBuhinEditVo.ShisakuBlockNoKaiteiNo, _
                                                             buhinEditVo.BuhinNoHyoujiJun, _
                                                             konkaiBuhinEditGousyaList(kindex).ShisakuGousyaHyoujiJun, _
                                                             konkaiBuhinEditGousyaList(kindex).ShisakuGousya, _
                                                             insusa, _
                                                             "1")

                    Next


                    For Each BVo As TShisakuBuhinEditVoSekkeiHelper In zenkaiBuhinEditGousyaList
                        CyushutuImpl.InsertByBuhinEditGousya(buhinEditVo.ShisakuEventCode, _
                                                             shisakuListCode, _
                                                             shisakuListCodeKaiteiNo, _
                                                             buhinEditVo.ShisakuBukaCode, _
                                                             buhinEditVo.ShisakuBlockNo, _
                                                             buhinEditVo.ShisakuBlockNoKaiteiNo, _
                                                             "", _
                                                             buhinEditVo.BuhinNoHyoujiJun, _
                                                             BVo.ShisakuGousyaHyoujiJun, _
                                                             BVo.ShisakuGousya, _
                                                             BVo.InsuSuryo, _
                                                             "0")

                    Next
                    '追加'
                    CyushutuImpl.InsertByBuhinEditKaitei(buhinEditVo, shisakuListCode, shisakuListCodeKaiteiNo, "1", "")

                    '変更無しで元の情報を追加'
                    CyushutuImpl.InsertByBuhinEditKaitei(buhinEditVo, shisakuListCode, shisakuListCodeKaiteiNo, "0", "")


                End If





            ElseIf NewTotalInsu = btotalInsu Then
                '員数に変更無し'
                '変更前、変更後で作成'
                '追加(変更後)'
                CyushutuImpl.InsertByBuhinEditKaitei(buhinEditVo, shisakuListCode, shisakuListCodeKaiteiNo, "3", "")

                For Each kVo As TShisakuBuhinEditVoSekkeiHelper In konkaiBuhinEditGousyaList
                    '追加処理(追加)'
                    CyushutuImpl.InsertByBuhinEditGousya(buhinEditVo.ShisakuEventCode, _
                                                         shisakuListCode, _
                                                         shisakuListCodeKaiteiNo, _
                                                         buhinEditVo.ShisakuBukaCode, _
                                                         buhinEditVo.ShisakuBlockNo, _
                                                         buhinEditVo.ShisakuBlockNoKaiteiNo, _
                                                         "", _
                                                         buhinEditVo.BuhinNoHyoujiJun, _
                                                         kVo.ShisakuGousyaHyoujiJun, _
                                                         kVo.ShisakuGousya, _
                                                         kVo.InsuSuryo, _
                                                         "3")
                Next

                '変更前'
                CyushutuImpl.InsertByBuhinEditKaiteiDel(buhinEditVo.ShisakuEventCode, _
                                                        shisakuListCode, _
                                                        shisakuListCodeKaiteiNo, _
                                                        buhinEditVo.ShisakuBukaCode, _
                                                        buhinEditVo.ShisakuBlockNo, _
                                                        buhinEditVo.ShisakuBlockNoKaiteiNo, _
                                                        zenkaiBuhinEditVo.ShisakuBlockNoKaiteiNo, _
                                                        buhinEditVo.BuhinNoHyoujiJun, _
                                                        zenkaiBuhinEditVo, _
                                                        "4")

                '前回改訂Noの部品編集号車改訂情報'
                '追加処理(変更前)'

                For Each BVo As TShisakuBuhinEditVoSekkeiHelper In zenkaiBuhinEditGousyaList
                    CyushutuImpl.InsertByBuhinEditGousyaKaiteiDel(buhinEditVo.ShisakuEventCode, _
                                              shisakuListCode, _
                                              shisakuListCodeKaiteiNo, _
                                              buhinEditVo.ShisakuBukaCode, _
                                              buhinEditVo.ShisakuBlockNo, _
                                              buhinEditVo.ShisakuBlockNoKaiteiNo, _
                                              zenkaiBuhinEditVo.ShisakuBlockNoKaiteiNo, _
                                              buhinEditVo.BuhinNoHyoujiJun, _
                                              BVo.ShisakuGousyaHyoujiJun, _
                                              BVo.ShisakuGousya, _
                                              BVo.InsuSuryo, _
                                              "4")
                Next
            End If

        End Sub

        ''' <summary>
        ''' 部品番号とプライマリキー以外が同一かチェック
        ''' </summary>
        ''' <param name="BuhinEditVo">部品編集情報</param>
        ''' <param name="BuhinBaseVo">前回部品編集情報</param>
        ''' <returns>全件同一ならTrue</returns>
        ''' <remarks></remarks>
        Private Function CheckByAllBuhin(ByVal BuhinEditVo As TShisakuBuhinEditVoSekkeiHelper, ByVal BuhinBaseVo As TShisakuBuhinEditVo) As Boolean

            'レベル、集計コード、海外集計コード、現調CKD区分、取引先コード、取引先名称'
            '部品番号区分、部品番号改訂No、枝番、部品名称、再使用不可、出図予定日'
            '材質規格１,２,３,メッキ、板厚数量,数量u、試作部品費、試作型費、備考、改訂判断フラグ'
            'が同一であるかをチェックする'

            '出図予定日99999999は0扱い'
            If BuhinEditVo.ShutuzuYoteiDate = 99999999 Then
                BuhinEditVo.ShutuzuYoteiDate = 0
            End If
            If BuhinBaseVo.ShutuzuYoteiDate = 99999999 Then
                BuhinBaseVo.ShutuzuYoteiDate = 0
            End If

            '2012/03/02 部品のチェックを修正
            'レベルや集計コードは仕様変更した時用に置いている

            'レベル'
            'If BuhinEditVo.Level <> BuhinBaseVo.Level Then
            '    Return False
            'End If
            '集計コード'

            '優先順位は国内、海外の順'
            '国内'
            'If Not StringUtil.IsEmpty(BuhinEditVo.ShukeiCode) Then
            '    '国内が空でないなら国内を比較'
            '    If Not StringUtil.IsEmpty(BuhinBaseVo.ShukeiCode) Then
            '        '国内集計コードが異なる'
            '        If Not StringUtil.Equals(BuhinEditVo.ShukeiCode, BuhinBaseVo.ShukeiCode) Then
            '            Return False
            '        End If
            '    Else
            '        'BuhinEditVoは国内だがBuhinBaseVoは海外'
            '        Return False
            '    End If
            'Else
            '    'BuhinEditVoは海外集計コードが入力されている'
            '    If Not StringUtil.IsEmpty(BuhinBaseVo.SiaShukeiCode) Then
            '        '海外集計コードが空でないなら海外を比較'
            '        If Not StringUtil.Equals(BuhinEditVo.SiaShukeiCode, BuhinBaseVo.SiaShukeiCode) Then
            '            '海外集計コードが異なる'
            '            Return False
            '        End If
            '    Else
            '        'BuhinEditVoは海外だがBuhinBaseVoは国内'
            '        Return False
            '    End If
            'End If

            '現調CKD区分'
            ''2015/09/30 廃止 E.Ubukata Ver.2.11.3
            '' 手配帳上、現調CKDは意識しないためチェックの対象外とする
            'If Not StringUtil.IsEmpty(BuhinEditVo.GencyoCkdKbn) Then
            '    If Not StringUtil.IsEmpty(BuhinBaseVo.GencyoCkdKbn) Then
            '        If Not StringUtil.Equals(Trim(BuhinEditVo.GencyoCkdKbn), Trim(BuhinBaseVo.GencyoCkdKbn)) Then
            '            '現調CKD区分が異なる'
            '            Return False
            '        End If
            '    Else
            '        'BuhinEditVoが空だが、BuhinBaseVoは空じゃない'
            '        Return False
            '    End If
            'Else
            '    'BuhinEditVoが空ではないが、BuhinBaseVoは空'
            '    If Not StringUtil.IsEmpty(BuhinBaseVo.GencyoCkdKbn) Then
            '        Return False
            '    End If
            'End If



            '取引先コード'
            If Not StringUtil.IsEmpty(BuhinEditVo.MakerCode) Then
                If Not StringUtil.IsEmpty(BuhinBaseVo.MakerCode) Then
                    If Not StringUtil.Equals(Trim(BuhinEditVo.MakerCode), Trim(BuhinBaseVo.MakerCode)) Then
                        '取引先コードが異なる'
                        Return False
                    End If
                Else
                    'BuhinEditVoが空ではないが、BuhinBaseVoは空'
                    Return False
                End If
            Else
                'BuhinEditVoが空だが、BuhinBaseVoは空ではない'
                If Not StringUtil.IsEmpty(BuhinBaseVo.MakerCode) Then
                    Return False
                End If
            End If

            '取引先名称'
            If Not StringUtil.IsEmpty(BuhinEditVo.MakerName) Then
                If Not StringUtil.IsEmpty(BuhinBaseVo.MakerName) Then
                    If Not StringUtil.Equals(Trim(BuhinEditVo.MakerName), Trim(BuhinBaseVo.MakerName)) Then
                        '取引先名称が異なる'
                        Return False
                    End If
                Else
                    'BuhinEditVoが空ではないが、BuhinBaseVoは空'
                    Return False
                End If
            Else
                'BuhinEditVoが空だが、BuhinBaseVoは空ではない'
                If Not StringUtil.IsEmpty(BuhinBaseVo.MakerName) Then
                    Return False
                End If
            End If

            '試作区分'
            If Not StringUtil.IsEmpty(BuhinEditVo.BuhinNoKbn) Then
                If Not StringUtil.IsEmpty(BuhinBaseVo.BuhinNoKbn) Then
                    If Not StringUtil.Equals(Trim(BuhinEditVo.BuhinNoKbn), Trim(BuhinBaseVo.BuhinNoKbn)) Then
                        '試作区分が異なる'
                        Return False
                    End If
                Else
                    'BuhinEditVoが空ではないが、BuhinBaseVoは空'
                    Return False
                End If
            Else
                'BuhinEditVoが空だが、BuhinBaseVoは空ではない'
                If Not StringUtil.IsEmpty(BuhinBaseVo.BuhinNoKbn) Then
                    Return False
                End If
            End If

            '部品番号改訂No'
            If Not StringUtil.IsEmpty(BuhinEditVo.BuhinNoKaiteiNo) Then
                If Not StringUtil.IsEmpty(BuhinBaseVo.BuhinNoKaiteiNo) Then
                    If Not StringUtil.Equals(Trim(BuhinEditVo.BuhinNoKaiteiNo), Trim(BuhinBaseVo.BuhinNoKaiteiNo)) Then
                        '部品番号改訂Noが異なる'
                        Return False
                    End If
                Else
                    'BuhinEditVoが空ではないが、BuhinBaseVoは空'
                    Return False
                End If
            Else
                'BuhinEditVoが空だが、BuhinBaseVoは空ではない'
                If Not StringUtil.IsEmpty(BuhinBaseVo.BuhinNoKaiteiNo) Then
                    Return False
                End If
            End If

            '枝番'
            If Not StringUtil.IsEmpty(BuhinEditVo.EdaBan) Then
                If Not StringUtil.IsEmpty(BuhinBaseVo.EdaBan) Then
                    If Not StringUtil.Equals(Trim(BuhinEditVo.EdaBan), Trim(BuhinBaseVo.EdaBan)) Then
                        '枝番が異なる'
                        Return False
                    End If
                Else
                    'BuhinEditVoが空ではないが、BuhinBaseVoは空'
                    Return False
                End If
            Else
                'BuhinEditVoが空だが、BuhinBaseVoは空ではない'
                If Not StringUtil.IsEmpty(BuhinBaseVo.EdaBan) Then
                    Return False
                End If
            End If

            '部品名称'
            If Not StringUtil.IsEmpty(BuhinEditVo.BuhinName) Then
                If Not StringUtil.IsEmpty(BuhinBaseVo.BuhinName) Then
                    If Not StringUtil.Equals(Trim(BuhinEditVo.BuhinName), Trim(BuhinBaseVo.BuhinName)) Then
                        '部品名称が異なる'
                        Return False
                    End If
                Else
                    'BuhinEditVoが空ではないが、BuhinBaseVoは空'
                    Return False
                End If
            Else
                'BuhinEditVoが空だが、BuhinBaseVoは空ではない'
                If Not StringUtil.IsEmpty(BuhinBaseVo.BuhinName) Then
                    Return False
                End If
            End If

            '再使用不可'
            If Not StringUtil.IsEmpty(BuhinEditVo.Saishiyoufuka) Then
                If Not StringUtil.IsEmpty(BuhinBaseVo.Saishiyoufuka) Then
                    If Not StringUtil.Equals(Trim(BuhinEditVo.Saishiyoufuka), Trim(BuhinBaseVo.Saishiyoufuka)) Then
                        '再使用不可が異なる'
                        Return False
                    End If
                Else
                    'BuhinEditVoが空ではないが、BuhinBaseVoは空'
                    Return False
                End If
            Else
                'BuhinEditVoが空だが、BuhinBaseVoは空ではない'
                If Not StringUtil.IsEmpty(BuhinBaseVo.Saishiyoufuka) Then
                    Return False
                End If
            End If

            '出図予定日'
            If BuhinEditVo.ShutuzuYoteiDate <> BuhinBaseVo.ShutuzuYoteiDate Then
                Return False
            End If
            '↓↓2014/09/26 酒井 ADD BEGIN
            If Not StringUtil.IsEmpty(BuhinEditVo.TsukurikataSeisaku) Then
                If Not StringUtil.IsEmpty(BuhinBaseVo.TsukurikataSeisaku) Then
                    If Not StringUtil.Equals(Trim(BuhinEditVo.TsukurikataSeisaku), Trim(BuhinBaseVo.TsukurikataSeisaku)) Then
                        Return False
                    End If
                Else
                    'BuhinEditVoが空だが、BuhinBaseVoは空ではない'
                    Return False
                End If
            Else
                'BuhinEditVoが空ではないが、BuhinBaseVoは空'
                If Not StringUtil.IsEmpty(BuhinBaseVo.TsukurikataSeisaku) Then
                    Return False
                End If
            End If

            If Not StringUtil.IsEmpty(BuhinEditVo.TsukurikataKatashiyou1) Then
                If Not StringUtil.IsEmpty(BuhinBaseVo.TsukurikataKatashiyou1) Then
                    If Not StringUtil.Equals(Trim(BuhinEditVo.TsukurikataKatashiyou1), Trim(BuhinBaseVo.TsukurikataKatashiyou1)) Then
                        Return False
                    End If
                Else
                    'BuhinEditVoが空だが、BuhinBaseVoは空ではない'
                    Return False
                End If
            Else
                'BuhinEditVoが空ではないが、BuhinBaseVoは空'
                If Not StringUtil.IsEmpty(BuhinBaseVo.TsukurikataKatashiyou1) Then
                    Return False
                End If
            End If

            If Not StringUtil.IsEmpty(BuhinEditVo.TsukurikataKatashiyou2) Then
                If Not StringUtil.IsEmpty(BuhinBaseVo.TsukurikataKatashiyou2) Then
                    If Not StringUtil.Equals(Trim(BuhinEditVo.TsukurikataKatashiyou2), Trim(BuhinBaseVo.TsukurikataKatashiyou2)) Then
                        Return False
                    End If
                Else
                    'BuhinEditVoが空だが、BuhinBaseVoは空ではない'
                    Return False
                End If
            Else
                'BuhinEditVoが空ではないが、BuhinBaseVoは空'
                If Not StringUtil.IsEmpty(BuhinBaseVo.TsukurikataKatashiyou2) Then
                    Return False
                End If
            End If

            If Not StringUtil.IsEmpty(BuhinEditVo.TsukurikataKatashiyou3) Then
                If Not StringUtil.IsEmpty(BuhinBaseVo.TsukurikataKatashiyou3) Then
                    If Not StringUtil.Equals(Trim(BuhinEditVo.TsukurikataKatashiyou3), Trim(BuhinBaseVo.TsukurikataKatashiyou3)) Then
                        Return False
                    End If
                Else
                    'BuhinEditVoが空だが、BuhinBaseVoは空ではない'
                    Return False
                End If
            Else
                'BuhinEditVoが空ではないが、BuhinBaseVoは空'
                If Not StringUtil.IsEmpty(BuhinBaseVo.TsukurikataKatashiyou3) Then
                    Return False
                End If
            End If

            If Not StringUtil.IsEmpty(BuhinEditVo.TsukurikataTigu) Then
                If Not StringUtil.IsEmpty(BuhinBaseVo.TsukurikataTigu) Then
                    If Not StringUtil.Equals(Trim(BuhinEditVo.TsukurikataTigu), Trim(BuhinBaseVo.TsukurikataTigu)) Then
                        Return False
                    End If
                Else
                    'BuhinEditVoが空だが、BuhinBaseVoは空ではない'
                    Return False
                End If
            Else
                'BuhinEditVoが空ではないが、BuhinBaseVoは空'
                If Not StringUtil.IsEmpty(BuhinBaseVo.TsukurikataTigu) Then
                    Return False
                End If
            End If

            If BuhinEditVo.TsukurikataNounyu <> BuhinBaseVo.TsukurikataNounyu Then
                Return False
            End If

            If Not StringUtil.IsEmpty(BuhinEditVo.TsukurikataKibo) Then
                If Not StringUtil.IsEmpty(BuhinBaseVo.TsukurikataKibo) Then
                    If Not StringUtil.Equals(Trim(BuhinEditVo.TsukurikataKibo), Trim(BuhinBaseVo.TsukurikataKibo)) Then
                        Return False
                    End If
                Else
                    'BuhinEditVoが空だが、BuhinBaseVoは空ではない'
                    Return False
                End If
            Else
                'BuhinEditVoが空ではないが、BuhinBaseVoは空'
                If Not StringUtil.IsEmpty(BuhinBaseVo.TsukurikataKibo) Then
                    Return False
                End If
            End If
            '↑↑2014/09/26 酒井 ADD END
            '材質規格１'
            If Not StringUtil.IsEmpty(BuhinEditVo.ZaishituKikaku1) Then
                If Not StringUtil.IsEmpty(BuhinBaseVo.ZaishituKikaku1) Then
                    If Not StringUtil.Equals(Trim(BuhinEditVo.ZaishituKikaku1), Trim(BuhinBaseVo.ZaishituKikaku1)) Then
                        '材質規格１が異なる'
                        Return False
                    End If
                Else
                    'BuhinEditVoが空だが、BuhinBaseVoは空ではない'
                    Return False
                End If
            Else
                'BuhinEditVoが空ではないが、BuhinBaseVoは空'
                If Not StringUtil.IsEmpty(BuhinBaseVo.ZaishituKikaku1) Then
                    Return False
                End If
            End If

            '材質規格２'
            If Not StringUtil.IsEmpty(BuhinEditVo.ZaishituKikaku2) Then
                If Not StringUtil.IsEmpty(BuhinBaseVo.ZaishituKikaku2) Then
                    If Not StringUtil.Equals(Trim(BuhinEditVo.ZaishituKikaku2), Trim(BuhinBaseVo.ZaishituKikaku2)) Then
                        '材質規格２が異なる'
                        Return False
                    End If
                Else
                    'BuhinEditVoが空ではないが、BuhinBaseVoは空'
                    Return False
                End If
            Else
                'BuhinEditVoが空だが、BuhinBaseVoは空ではない'
                If Not StringUtil.IsEmpty(BuhinBaseVo.ZaishituKikaku2) Then
                    Return False
                End If
            End If

            '材質規格３'
            If Not StringUtil.IsEmpty(BuhinEditVo.ZaishituKikaku3) Then
                If Not StringUtil.IsEmpty(BuhinBaseVo.ZaishituKikaku3) Then
                    If Not StringUtil.Equals(Trim(BuhinEditVo.ZaishituKikaku3), Trim(BuhinBaseVo.ZaishituKikaku3)) Then
                        '材質規格３が異なる'
                        Return False
                    End If
                Else
                    'BuhinEditVoが空ではないが、BuhinBaseVoは空'
                    Return False
                End If
            Else
                'BuhinEditVoが空だが、BuhinBaseVoは空ではない'
                If Not StringUtil.IsEmpty(BuhinBaseVo.ZaishituKikaku3) Then
                    Return False
                End If
            End If

            '材質メッキ'
            If Not StringUtil.IsEmpty(BuhinEditVo.ZaishituMekki) Then
                If Not StringUtil.IsEmpty(BuhinBaseVo.ZaishituMekki) Then
                    If Not StringUtil.Equals(Trim(BuhinEditVo.ZaishituMekki), Trim(BuhinBaseVo.ZaishituMekki)) Then
                        '材質メッキが異なる'
                        Return False
                    End If
                Else
                    'BuhinEditVoが空ではないが、BuhinBaseVoは空'
                    Return False
                End If
            Else
                'BuhinEditVoが空だが、BuhinBaseVoは空ではない'
                If Not StringUtil.IsEmpty(BuhinBaseVo.ZaishituMekki) Then
                    Return False
                End If
            End If

            '板厚数量'
            If Not StringUtil.IsEmpty(BuhinEditVo.ShisakuBankoSuryo) Then
                If Not StringUtil.IsEmpty(BuhinBaseVo.ShisakuBankoSuryo) Then
                    If Not StringUtil.Equals(Trim(BuhinEditVo.ShisakuBankoSuryo), Trim(BuhinBaseVo.ShisakuBankoSuryo)) Then
                        '板厚数量が異なる'
                        Return False
                    End If
                Else
                    'BuhinEditVoが空ではないが、BuhinBaseVoは空'
                    Return False
                End If
            Else
                'BuhinEditVoが空だが、BuhinBaseVoは空ではない'
                If Not StringUtil.IsEmpty(BuhinBaseVo.ShisakuBankoSuryo) Then
                    Return False
                End If
            End If

            '板厚数量u'
            If Not StringUtil.IsEmpty(BuhinEditVo.ShisakuBankoSuryoU) Then
                If Not StringUtil.IsEmpty(BuhinBaseVo.ShisakuBankoSuryoU) Then
                    If Not StringUtil.Equals(Trim(BuhinEditVo.ShisakuBankoSuryoU), Trim(BuhinBaseVo.ShisakuBankoSuryoU)) Then
                        '板厚数量uが異なる'
                        Return False
                    End If
                Else
                    'BuhinEditVoが空ではないが、BuhinBaseVoは空'
                    Return False
                End If
            Else
                'BuhinEditVoが空だが、BuhinBaseVoは空ではない'
                If Not StringUtil.IsEmpty(BuhinBaseVo.ShisakuBankoSuryoU) Then
                    Return False
                End If
            End If


            ''↓↓2014/12/29 メタル対応追加フィールド (DANIEL)柳沼 ADD BEGIN
            '
            If BuhinEditVo.MaterialInfoLength <> BuhinBaseVo.MaterialInfoLength Then
                Return False
            End If
            '
            If BuhinEditVo.MaterialInfoWidth <> BuhinBaseVo.MaterialInfoWidth Then
                Return False
            End If
            '
            If Not StringUtil.IsEmpty(BuhinEditVo.DataItemKaiteiNo) Then
                If Not StringUtil.IsEmpty(BuhinBaseVo.DataItemKaiteiNo) Then
                    If Not StringUtil.Equals(Trim(BuhinEditVo.DataItemKaiteiNo), Trim(BuhinBaseVo.DataItemKaiteiNo)) Then
                        Return False
                    End If
                Else
                    'BuhinEditVoが空ではないが、BuhinBaseVoは空'
                    Return False
                End If
            Else
                'BuhinEditVoが空だが、BuhinBaseVoは空ではない'
                If Not StringUtil.IsEmpty(BuhinBaseVo.DataItemKaiteiNo) Then
                    Return False
                End If
            End If
            '
            If Not StringUtil.IsEmpty(BuhinEditVo.DataItemAreaName) Then
                If Not StringUtil.IsEmpty(BuhinBaseVo.DataItemAreaName) Then
                    If Not StringUtil.Equals(Trim(BuhinEditVo.DataItemAreaName), Trim(BuhinBaseVo.DataItemAreaName)) Then
                        Return False
                    End If
                Else
                    'BuhinEditVoが空ではないが、BuhinBaseVoは空'
                    Return False
                End If
            Else
                'BuhinEditVoが空だが、BuhinBaseVoは空ではない'
                If Not StringUtil.IsEmpty(BuhinBaseVo.DataItemAreaName) Then
                    Return False
                End If
            End If
            '
            If Not StringUtil.IsEmpty(BuhinEditVo.DataItemSetName) Then
                If Not StringUtil.IsEmpty(BuhinBaseVo.DataItemSetName) Then
                    If Not StringUtil.Equals(Trim(BuhinEditVo.DataItemSetName), Trim(BuhinBaseVo.DataItemSetName)) Then
                        Return False
                    End If
                Else
                    'BuhinEditVoが空ではないが、BuhinBaseVoは空'
                    Return False
                End If
            Else
                'BuhinEditVoが空だが、BuhinBaseVoは空ではない'
                If Not StringUtil.IsEmpty(BuhinBaseVo.DataItemSetName) Then
                    Return False
                End If
            End If
            '
            If Not StringUtil.IsEmpty(BuhinEditVo.DataItemKaiteiInfo) Then
                If Not StringUtil.IsEmpty(BuhinBaseVo.DataItemKaiteiInfo) Then
                    If Not StringUtil.Equals(Trim(BuhinEditVo.DataItemKaiteiInfo), Trim(BuhinBaseVo.DataItemKaiteiInfo)) Then
                        Return False
                    End If
                Else
                    'BuhinEditVoが空ではないが、BuhinBaseVoは空'
                    Return False
                End If
            Else
                'BuhinEditVoが空だが、BuhinBaseVoは空ではない'
                If Not StringUtil.IsEmpty(BuhinBaseVo.DataItemKaiteiInfo) Then
                    Return False
                End If
            End If
            ''↑↑2014/12/29 メタル対応追加フィールド (DANIEL)柳沼 ADD END


            '部品費'
            If BuhinEditVo.ShisakuBuhinHi <> BuhinBaseVo.ShisakuBuhinHi Then
                Return False
            End If

            '型費'
            If BuhinEditVo.ShisakuKataHi <> BuhinBaseVo.ShisakuKataHi Then
                Return False
            End If

            '備考'
            If Not StringUtil.IsEmpty(BuhinEditVo.Bikou) Then
                If Not StringUtil.IsEmpty(BuhinBaseVo.Bikou) Then
                    If Not StringUtil.Equals(Trim(BuhinEditVo.Bikou), Trim(BuhinBaseVo.Bikou)) Then
                        '備考が異なる'
                        Return False
                    End If
                Else
                    'BuhinEditVoが空ではないが、BuhinBaseVoは空'
                    Return False
                End If
            Else
                'BuhinEditVoが空だが、BuhinBaseVoは空ではない'
                If Not StringUtil.IsEmpty(BuhinBaseVo.Bikou) Then
                    Return False
                End If
            End If

            '改訂判断フラグ'
            If Not StringUtil.IsEmpty(BuhinEditVo.KaiteiHandanFlg) Then
                If Not StringUtil.IsEmpty(BuhinBaseVo.KaiteiHandanFlg) Then
                    If Not StringUtil.Equals(Trim(BuhinEditVo.KaiteiHandanFlg), Trim(BuhinBaseVo.KaiteiHandanFlg)) Then
                        '改訂判断フラグが異なる'
                        Return False
                    End If
                Else
                    'BuhinEditVoが空ではないが、BuhinBaseVoは空'
                    Return False
                End If
            Else
                'BuhinEditVoが空だが、BuhinBaseVoは空ではない'
                If Not StringUtil.IsEmpty(BuhinBaseVo.KaiteiHandanFlg) Then
                    Return False
                End If
            End If

            '供給セクション'
            If Not StringUtil.IsEmpty(BuhinEditVo.KyoukuSection) Then
                If Not StringUtil.IsEmpty(BuhinBaseVo.KyoukuSection) Then
                    If Not StringUtil.Equals(Trim(BuhinEditVo.KyoukuSection), Trim(BuhinBaseVo.KyoukuSection)) Then
                        '供給セクションが異なる'
                        Return False
                    End If
                Else
                    'BuhinEditVoが空ではないが、BuhinBaseVoは空'
                    Return False
                End If
            Else
                'BuhinEditVoが空だが、BuhinBaseVoは空ではない'
                If Not StringUtil.IsEmpty(BuhinBaseVo.KyoukuSection) Then
                    Return False
                End If
            End If

            '部品NOTE'
            If Not StringUtil.IsEmpty(BuhinEditVo.BuhinNote) Then
                If Not StringUtil.IsEmpty(BuhinBaseVo.BuhinNote) Then
                    If Not StringUtil.Equals(Trim(BuhinEditVo.BuhinNote), Trim(BuhinBaseVo.BuhinNote)) Then
                        '改訂判断フラグが異なる'
                        Return False
                    End If
                Else
                    'BuhinEditVoが空ではないが、BuhinBaseVoは空'
                    Return False
                End If
            Else
                'BuhinEditVoが空だが、BuhinBaseVoは空ではない'
                If Not StringUtil.IsEmpty(BuhinBaseVo.BuhinNote) Then
                    Return False
                End If
            End If

            '前回のソース'
            'If Not StringUtil.Equals(Trim(BuhinEditVo.BuhinName), Trim(BuhinBaseVo.BuhinName)) Then
            '    Return False
            'End If
            'If Not (StringUtil.IsEmpty(BuhinEditVo.BuhinName) AndAlso StringUtil.IsEmpty(BuhinBaseVo)) Then
            '    Return False
            'End If

            'If Not StringUtil.Equals(BuhinEditVo.Level, BuhinBaseVo.Level) Then
            '    If Not StringUtil.IsEmpty(BuhinEditVo.Level) Or Not StringUtil.IsEmpty(BuhinBaseVo.Level) Then
            '        Return False
            '    End If
            'ElseIf Not StringUtil.Equals(BuhinEditVo.ShukeiCode, BuhinBaseVo.ShukeiCode) Then
            '    If Not StringUtil.IsEmpty(BuhinEditVo.ShukeiCode) Or Not StringUtil.IsEmpty(BuhinBaseVo.ShukeiCode) Then
            '        Return False
            '    End If
            'ElseIf Not StringUtil.Equals(BuhinEditVo.SiaShukeiCode, BuhinBaseVo.SiaShukeiCode) Then
            '    If Not StringUtil.IsEmpty(BuhinEditVo.SiaShukeiCode) Or Not StringUtil.IsEmpty(BuhinBaseVo.SiaShukeiCode) Then
            '        Return False
            '    End If
            'ElseIf Not StringUtil.Equals(BuhinEditVo.GencyoCkdKbn, BuhinBaseVo.GencyoCkdKbn) Then
            '    If Not StringUtil.IsEmpty(BuhinEditVo.GencyoCkdKbn) Or Not StringUtil.IsEmpty(BuhinBaseVo.GencyoCkdKbn) Then
            '        Return False
            '    End If
            'ElseIf Not StringUtil.Equals(BuhinEditVo.MakerCode, BuhinBaseVo.MakerCode) Then
            '    If Not StringUtil.IsEmpty(BuhinEditVo.MakerCode) Or Not StringUtil.IsEmpty(BuhinBaseVo.MakerCode) Then
            '        Return False
            '    End If
            'ElseIf Not StringUtil.Equals(BuhinEditVo.MakerName, BuhinBaseVo.MakerName) Then
            '    If Not StringUtil.IsEmpty(BuhinEditVo.MakerName) Or Not StringUtil.IsEmpty(BuhinBaseVo.MakerName) Then
            '        Return False
            '    End If
            'ElseIf Not StringUtil.Equals(BuhinEditVo.BuhinNoKbn, BuhinBaseVo.BuhinNoKbn) Then
            '    If Not StringUtil.IsEmpty(BuhinEditVo.BuhinNoKbn) Or Not StringUtil.IsEmpty(BuhinBaseVo.BuhinNoKbn) Then
            '        Return False
            '    End If
            'ElseIf Not StringUtil.Equals(BuhinEditVo.BuhinNoKaiteiNo, BuhinBaseVo.BuhinNoKaiteiNo) Then
            '    If Not StringUtil.IsEmpty(BuhinEditVo.BuhinNoKaiteiNo) Or Not StringUtil.IsEmpty(BuhinBaseVo.BuhinNoKaiteiNo) Then
            '        Return False
            '    End If
            'ElseIf Not StringUtil.Equals(BuhinEditVo.EdaBan, BuhinBaseVo.EdaBan) Then
            '    If Not StringUtil.IsEmpty(BuhinEditVo.EdaBan) Or Not StringUtil.IsEmpty(BuhinBaseVo.EdaBan) Then
            '        Return False
            '    End If
            'ElseIf Not StringUtil.Equals(BuhinEditVo.Saishiyoufuka, BuhinBaseVo.Saishiyoufuka) Then
            '    If Not StringUtil.IsEmpty(BuhinEditVo.Saishiyoufuka) Or Not StringUtil.IsEmpty(BuhinBaseVo.Saishiyoufuka) Then
            '        Return False
            '    End If
            'ElseIf Not StringUtil.Equals(BuhinEditVo.ShutuzuYoteiDate, BuhinBaseVo.ShutuzuYoteiDate) Then
            '    If Not StringUtil.IsEmpty(BuhinEditVo.ShutuzuYoteiDate) Or Not StringUtil.IsEmpty(BuhinBaseVo.ShutuzuYoteiDate) Then
            '        Return False
            '    End If
            'ElseIf Not StringUtil.Equals(BuhinEditVo.ZaishituKikaku1, BuhinBaseVo.ZaishituKikaku1) Then
            '    If Not StringUtil.IsEmpty(BuhinEditVo.ZaishituKikaku1) Or Not StringUtil.IsEmpty(BuhinBaseVo.ZaishituKikaku1) Then
            '        Return False
            '    End If
            'ElseIf Not StringUtil.Equals(BuhinEditVo.ZaishituKikaku2, BuhinBaseVo.ZaishituKikaku2) Then
            '    If Not StringUtil.IsEmpty(BuhinEditVo.ZaishituKikaku2) Or Not StringUtil.IsEmpty(BuhinBaseVo.ZaishituKikaku2) Then
            '        Return False
            '    End If
            'ElseIf Not StringUtil.Equals(BuhinEditVo.ZaishituKikaku3, BuhinBaseVo.ZaishituKikaku3) Then
            '    If Not StringUtil.IsEmpty(BuhinEditVo.ZaishituKikaku3) Or Not StringUtil.IsEmpty(BuhinBaseVo.ZaishituKikaku3) Then
            '        Return False
            '    End If
            'ElseIf Not StringUtil.Equals(BuhinEditVo.ZaishituMekki, BuhinBaseVo.ZaishituMekki) Then
            '    If Not StringUtil.IsEmpty(BuhinEditVo.ZaishituMekki) Or Not StringUtil.IsEmpty(BuhinBaseVo.ZaishituMekki) Then
            '        Return False
            '    End If
            'ElseIf Not StringUtil.Equals(BuhinEditVo.ShisakuBankoSuryo, BuhinBaseVo.ShisakuBankoSuryo) Then
            '    If Not StringUtil.IsEmpty(BuhinEditVo.ShisakuBankoSuryo) Or Not StringUtil.IsEmpty(BuhinBaseVo.ShisakuBankoSuryo) Then
            '        Return False
            '    End If
            'ElseIf Not StringUtil.Equals(BuhinEditVo.ShisakuBankoSuryoU, BuhinBaseVo.ShisakuBankoSuryoU) Then
            '    If Not StringUtil.IsEmpty(BuhinEditVo.ShisakuBankoSuryoU) Or Not StringUtil.IsEmpty(BuhinBaseVo.ShisakuBankoSuryoU) Then
            '        Return False
            '    End If
            'ElseIf Not StringUtil.Equals(BuhinEditVo.ShisakuBuhinHi, BuhinBaseVo.ShisakuBuhinHi) Then
            '    If Not StringUtil.IsEmpty(BuhinEditVo.ShisakuBuhinHi) Or Not StringUtil.IsEmpty(BuhinBaseVo.ShisakuBuhinHi) Then
            '        Return False
            '    End If
            'ElseIf Not StringUtil.Equals(BuhinEditVo.ShisakuKataHi, BuhinBaseVo.ShisakuKataHi) Then
            '    If Not StringUtil.IsEmpty(BuhinEditVo.ShisakuKataHi) Or Not StringUtil.IsEmpty(BuhinBaseVo.ShisakuKataHi) Then
            '        Return False
            '    End If
            'ElseIf Not StringUtil.Equals(BuhinEditVo.Bikou, BuhinBaseVo.Bikou) Then
            '    If Not StringUtil.IsEmpty(BuhinEditVo.Bikou) Or Not StringUtil.IsEmpty(BuhinBaseVo.Bikou) Then
            '        Return False
            '    End If
            'ElseIf Not StringUtil.Equals(BuhinEditVo.KaiteiHandanFlg, BuhinBaseVo.KaiteiHandanFlg) Then
            '    If Not StringUtil.IsEmpty(BuhinEditVo.KaiteiHandanFlg) Or Not StringUtil.IsEmpty(BuhinBaseVo.KaiteiHandanFlg) Then
            '        Return False
            '    End If
            'ElseIf Not StringUtil.Equals(BuhinEditVo.KyoukuSection, BuhinBaseVo.KyoukuSection) Then
            '    If Not StringUtil.IsEmpty(BuhinEditVo.KyoukuSection) OrElse Not StringUtil.IsEmpty(BuhinBaseVo.KyoukuSection) Then
            '        Return False
            '    End If
            'ElseIf Not StringUtil.Equals(BuhinEditVo.BuhinNote, BuhinBaseVo.BuhinNote) Then
            '    If Not StringUtil.IsEmpty(BuhinEditVo.BuhinNote) OrElse Not StringUtil.IsEmpty(BuhinBaseVo.BuhinNote) Then
            '        Return False
            '    End If
            'End If


            'If Not StringUtil.Equals(Trim(sBuhinEditVo.BuhinName), Trim(BuhinBaseVo.BuhinName)) Then
            '    Return False
            'End If
            'If Not (StringUtil.IsEmpty(BuhinEditVo.BuhinName) AndAlso StringUtil.IsEmpty(BuhinBaseVo)) Then
            '    Return False
            'End If

            Return True
        End Function

        ''' <summary>
        ''' 部品番号がずれた可能性があるので存在チェックを念入りにする
        ''' </summary>
        ''' <param name="BuhinEditVo">今回部品編集情報</param>
        ''' <param name="bBuhinEditVoList">前回部品編集情報</param>
        ''' <returns>該当する前回部品編集情報なければNothing</returns>
        ''' <remarks></remarks>
        Private Function FindByBuhinEditVo(ByVal BuhinEditVo As TShisakuBuhinEditVo, ByVal bBuhinEditVoList As List(Of TShisakuBuhinEditVo)) As TShisakuBuhinEditVo

            Dim buhinHyoujiJun As Integer = 0
            Dim konkaiBuhinNoHyoujiJun As Integer = 0
            Dim result As New TShisakuBuhinEditVo

            '部品番号表示順がずれただけなのか検索する'
            For index As Integer = 0 To bBuhinEditVoList.Count - 1
                '部品番号は該当しているのでその他の該当が該当するかチェック'
                If CheckByAllBuhin(BuhinEditVo, bBuhinEditVoList(index)) Then
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
        ''' マージ処理を行う
        ''' </summary>
        ''' <param name="BuhinEditVoList">マージ用リスト</param>
        ''' <returns>部品編集情報</returns>
        ''' <remarks></remarks>
        Private Function MergeList(ByVal BuhinEditVoList As List(Of TShisakuBuhinEditVoSekkeiHelper)) As List(Of TShisakuBuhinEditVoSekkeiHelper)
            '部品番号順にソートが完了したので今度は圧縮'

            '具体的にはブロックNo, レベル, 部品番号, 集計コード, 手配記号, 供給セクションが同一の項目は'
            '部品番号表示順が最も小さいものと同一の部品番号表示順を持つ'
            'この場合、試作号車表示順は異なっている'
            For Each Vo As TShisakuBuhinEditVoSekkeiHelper In BuhinEditVoList
                If StringUtil.IsEmpty(Vo.ShukeiCode) Then
                    Vo.ShukeiCode = Vo.SiaShukeiCode
                End If
            Next



            '別のやり方を模索中・・・・'
            'For Each vo1 As TShisakuBuhinEditVoSekkeiHelper In BuhinEditVoList
            '    For Each vo2 As TShisakuBuhinEditVoSekkeiHelper In BuhinEditVoList
            '        If StringUtil.Equals(vo1.BaseBuhinFlg, vo2.BaseBuhinFlg) Then
            '            'ブロックNoのチェックをする'
            '            If StringUtil.Equals(vo1.ShisakuBlockNo, vo2.ShisakuBlockNo) Then
            '                'レベル,部品番号,集計コードはNULLがないのでまとめて同一チェック'
            '                '20110730試作区分のチェックを追加 樺澤'
            '                '試作区分はNULLのパターンも存在するので'
            '                Dim kbn1 As String = vo1.BuhinNoKbn
            '                Dim kbn2 As String = vo2.BuhinNoKbn
            '                If StringUtil.IsEmpty(kbn1) Then
            '                    kbn1 = ""
            '                End If
            '                If StringUtil.IsEmpty(kbn2) Then
            '                    kbn2 = ""
            '                End If
            '                If StringUtil.Equals(kbn1, kbn2) Then
            '                    If vo1.Level = vo2.Level AndAlso _
            '                       StringUtil.Equals(vo1.BuhinNo.Trim, vo2.BuhinNo.Trim) AndAlso _
            '                       StringUtil.Equals(vo1.ShukeiCode, vo2.ShukeiCode) AndAlso _
            '                       StringUtil.Equals(vo1.KyoukuSection, vo2.KyoukuSection) AndAlso _
            '                       StringUtil.Equals(vo1.Saishiyoufuka, vo2.Saishiyoufuka) Then

            '                        '号車表示順が若い方はどっち？'
            '                        If vo1.ShisakuGousyaHyoujiJun < vo2.ShisakuGousyaHyoujiJun Then
            '                            '行IDが001同士か000同士ならマージ'
            '                            vo2.BuhinNoHyoujiJun = vo1.BuhinNoHyoujiJun
            '                        ElseIf vo1.ShisakuGousyaHyoujiJun > vo2.ShisakuGousyaHyoujiJun Then
            '                            vo1.BuhinNoHyoujiJun = vo2.BuhinNoHyoujiJun
            '                        Else
            '                            '号車表示順が同じでも部品番号表示順が違うパターンがいる'
            '                            If vo1.BuhinNoHyoujiJun < vo2.BuhinNoHyoujiJun Then
            '                                vo2.BuhinNoHyoujiJun = vo1.BuhinNoHyoujiJun
            '                            ElseIf vo1.BuhinNoHyoujiJun < vo2.BuhinNoHyoujiJun Then
            '                                vo1.BuhinNoHyoujiJun = vo2.BuhinNoHyoujiJun
            '                            End If
            '                        End If

            '                    End If

            '                End If

            '            End If
            '        End If
            '    Next
            'Next

            Dim flg As Boolean = False
            For index As Integer = 0 To BuhinEditVoList.Count - 1
                For index2 As Integer = index + 1 To BuhinEditVoList.Count - 1

                    ''2015/08/28 追加 E.Ubukata Ver2.11.0
                    '' フル組の際にはベース部品フラグを参照しないように変更
                    If Me.isIkansha Then
                        flg = StringUtil.Equals(BuhinEditVoList(index).BaseBuhinFlg, BuhinEditVoList(index2).BaseBuhinFlg)
                    Else
                        flg = True
                    End If

                    'If StringUtil.Equals(BuhinEditVoList(index).BaseBuhinFlg, BuhinEditVoList(index2).BaseBuhinFlg) Then
                    If flg Then
                        'ブロックNoのチェックをする'
                        If StringUtil.Equals(BuhinEditVoList(index).ShisakuBlockNo, BuhinEditVoList(index2).ShisakuBlockNo) Then
                            'レベル,部品番号,集計コードはNULLがないのでまとめて同一チェック'
                            '20110730試作区分のチェックを追加 樺澤'
                            '試作区分はNULLのパターンも存在するので'
                            Dim kbn1 As String = BuhinEditVoList(index).BuhinNoKbn
                            Dim kbn2 As String = BuhinEditVoList(index2).BuhinNoKbn
                            If StringUtil.IsEmpty(kbn1) Then
                                kbn1 = ""
                            End If
                            If StringUtil.IsEmpty(kbn2) Then
                                kbn2 = ""
                            End If
                            If StringUtil.Equals(kbn1, kbn2) Then
                                If BuhinEditVoList(index).Level = BuhinEditVoList(index2).Level AndAlso _
                                StringUtil.Equals(BuhinEditVoList(index).BuhinNo.Trim, BuhinEditVoList(index2).BuhinNo.Trim) AndAlso _
                                StringUtil.Equals(BuhinEditVoList(index).ShukeiCode, BuhinEditVoList(index2).ShukeiCode) Then

                                    If StringUtil.Equals(BuhinEditVoList(index).KyoukuSection, BuhinEditVoList(index2).KyoukuSection) Then
                                        '2013/05/29 再使用不可の判断を追加
                                        If StringUtil.Equals(BuhinEditVoList(index).Saishiyoufuka, BuhinEditVoList(index2).Saishiyoufuka) Then
                                            '号車表示順が若い方はどっち？'
                                            If BuhinEditVoList(index).ShisakuGousyaHyoujiJun < BuhinEditVoList(index2).ShisakuGousyaHyoujiJun Then
                                                '行IDが001同士か000同士ならマージ'
                                                BuhinEditVoList(index2).BuhinNoHyoujiJun = BuhinEditVoList(index).BuhinNoHyoujiJun
                                            ElseIf BuhinEditVoList(index).ShisakuGousyaHyoujiJun > BuhinEditVoList(index2).ShisakuGousyaHyoujiJun Then
                                                BuhinEditVoList(index).BuhinNoHyoujiJun = BuhinEditVoList(index2).BuhinNoHyoujiJun
                                            Else
                                                '号車表示順が同じでも部品番号表示順が違うパターンがいる'
                                                If BuhinEditVoList(index).BuhinNoHyoujiJun < BuhinEditVoList(index2).BuhinNoHyoujiJun Then
                                                    BuhinEditVoList(index2).BuhinNoHyoujiJun = BuhinEditVoList(index).BuhinNoHyoujiJun
                                                ElseIf BuhinEditVoList(index).BuhinNoHyoujiJun < BuhinEditVoList(index2).BuhinNoHyoujiJun Then
                                                    BuhinEditVoList(index).BuhinNoHyoujiJun = BuhinEditVoList(index2).BuhinNoHyoujiJun
                                                End If
                                            End If
                                        End If
                                    End If

                                End If

                            End If

                        End If
                    End If
                Next
            Next

            Return BuhinEditVoList
        End Function

        ''' <summary>
        ''' 自動織込み処理
        ''' </summary>
        ''' <remarks></remarks>
        Public Sub AutoOrikomi(ByVal CyushutuImpl As KaiteiChusyutuDao)

            '2012/01/25 樺澤'
            'ここで自動織り込みかどうかを判断する'

            '改訂ブロック情報を抽出し、手配帳基本、手配帳号車に抽出結果を反映する'
            Dim kaiteiBlockVoList As New List(Of TShisakuTehaiKaiteiBlockVo)
            Dim sakimpl As TehaichoSakusei.Dao.TehaichoSakuseiDao = New TehaichoSakusei.Dao.TehaichoSakuseiDaoImpl

            '2012/02/23 途中で落ちたとき対策として、一旦基本情報と号車情報を保持しておく'
            Dim backKihonList As New List(Of TShisakuTehaiKihonVo)
            Dim backGousyaList As New List(Of TShisakuTehaiGousyaVo)
            backKihonList = CyushutuImpl.FindByAllTehaiKihon(shisakuEventCode, shisakuListCode, shisakuListCodeKaiteiNo)
            backGousyaList = CyushutuImpl.FindByAllTehaiGousya(shisakuEventCode, shisakuListCode, shisakuListCodeKaiteiNo)

            '製品区分は試作リストコード情報から'
            Dim listVo As New TShisakuListcodeVo
            listVo = CyushutuImpl.FindByListCode(shisakuEventCode, shisakuListCode, shisakuListCodeKaiteiNo)

            Dim blockVo As New TShisakuSekkeiBlockVo

            'ダミーで無い列の最大値を取得'
            Dim maxGousyaHyoujJun As Integer = CyushutuImpl.FindByMaxGousyaHyoujijunNotDummy(shisakuEventCode, shisakuListCode, shisakuListCodeKaiteiNo)
            'ダミーで無い列の最大値+2がダミーの号車表示順となる'
            Dim dummyGousyaHyoujiJun As Integer = maxGousyaHyoujJun + 2

            'ユニット区分は試作設計ブロック情報から抽出'

            '変更のあったブロックのリストを抽出する'
            kaiteiBlockVoList = CyushutuImpl.FindByKaiteiBlockList(shisakuEventCode, shisakuListCode)

            Try
                For Each kVo As TShisakuTehaiKaiteiBlockVo In kaiteiBlockVoList
                    '抽出はブロック毎で行う'

                    '改訂部品情報を取得する'
                    Dim EditKaiteiVoList As New List(Of TShisakuBuhinEditKaiteiVo)
                    EditKaiteiVoList = CyushutuImpl.FindByBuhinEditKaiteiList(kVo.ShisakuEventCode, _
                                                                            kVo.ShisakuListCode, _
                                                                            shisakuListCodeKaiteiNo, _
                                                                            kVo.ShisakuBukaCode, _
                                                                            kVo.ShisakuBlockNo)

                    'ブロック単位で抽出'
                    Dim KihonBlockVoList As New List(Of TShisakuTehaiKihonVo)
                    KihonBlockVoList = CyushutuImpl.FindByTehaiKihonBlockList(kVo.ShisakuEventCode, _
                                                                              kVo.ShisakuListCode, _
                                                                              shisakuListCodeKaiteiNo, _
                                                                              kVo.ShisakuBukaCode, _
                                                                              kVo.ShisakuBlockNo)

                    '追加用リスト'
                    Dim InsertKihonVoList As New List(Of TShisakuTehaiKihonVo)
                    Dim InsertGousyaVoList As New List(Of TShisakuTehaiGousyaVo)
                    '削除用リスト'
                    Dim DelKihonVoList As New List(Of TShisakuTehaiKihonVo)
                    Dim delGousyaVoList As New List(Of TShisakuTehaiGousyaVo)
                    '変更用リスト'
                    Dim UpdateKihonVoList As New List(Of TShisakuTehaiKihonVo)
                    Dim UpdateGousyaVoList As New List(Of TShisakuTehaiGousyaVo)
                    '織込み不可リスト'
                    Dim UpdateNotOrikomiList As New List(Of TShisakuBuhinEditKaiteiVo)

                    blockVo = CyushutuImpl.FindBySekkeiBlock(kVo.ShisakuEventCode, kVo.ShisakuBukaCode, kVo.ShisakuBlockNo, kVo.KonkaiBlockNoKaiteiNo)

                    '納入指示日と供給セクション以外はこちらで用意'

                    '部品番号表示順'
                    Dim maxkihonVo As New TShisakuTehaiKihonVo
                    maxkihonVo = CyushutuImpl.FindByLatestGyouIdBuhinNoHyoujiJun(kVo.ShisakuEventCode, kVo.ShisakuListCode, shisakuListCodeKaiteiNo, kVo.ShisakuBukaCode, kVo.ShisakuBlockNo)
                    Dim buhinNoHyoujiJun As Integer = 0
                    Dim gyouId As String = "000"
                    If Not maxkihonVo Is Nothing Then
                        buhinNoHyoujiJun = maxkihonVo.BuhinNoHyoujiJun
                        gyouId = maxkihonVo.GyouId
                    End If

                    '抽出した部品に該当する手配帳'
                    For Each ekVo As TShisakuBuhinEditKaiteiVo In EditKaiteiVoList

                        'If StringUtil.Equals("20254FG030", ekVo.BuhinNo) Then
                        '    Dim a As String = ""
                        'End If

                        '追加は必ず一番下に追加する'
                        If Not StringUtil.Equals(ekVo.Flag, "1") Then

                            '変更無しに対しては手をださない'
                            If StringUtil.Equals("0", ekVo.Flag) Then
                                Continue For
                            End If

                            '該当する手配基本情報が複数存在した場合織込み不可能とする'
                            Dim checkList As New List(Of TShisakuTehaiKihonVo)
                            'レベル、集計コード、取引先コード、部品番号、供給セクションが同じ部品を探索'
                            checkList = CyushutuImpl.FindByOrikomiKihon(ekVo, shisakuListCode, shisakuListCodeKaiteiNo)
                            'ブロック内に同一部品が複数存在する場合はどちらを対象するのか不明なので織込み不可として織込み不可リストに追加'
                            If checkList.Count <> 1 Then
                                '織込み不可'
                                UpdateNotOrikomiList.Add(ekVo)
                            End If

                            'その他については該当すれば何かする'
                            For Each kbVo As TShisakuTehaiKihonVo In KihonBlockVoList

                                '同レベル、同集計コード、同取引先コード、同部品番号、同供給セクションの情報を比較する'
                                '   2013/05/29　同再使用不可の情報も比較する。
                                If ekVo.Level = kbVo.Level _
                                AndAlso StringUtil.Equals(Trim(ekVo.BuhinNo), Trim(kbVo.BuhinNo)) _
                                AndAlso EzUtil.IsEqualIfNull(ekVo.MakerCode, kbVo.TorihikisakiCode) Then

                                    If ekVo.KyoukuSection Is Nothing Then
                                        ekVo.KyoukuSection = ""
                                    End If
                                    If kbVo.KyoukuSection Is Nothing Then
                                        kbVo.KyoukuSection = ""
                                    End If
                                    '供給セクションが異なる'
                                    If Not StringUtil.Equals(ekVo.KyoukuSection, kbVo.KyoukuSection) Then
                                        Continue For
                                    End If

                                    '2013/05/29 同再使用不可かチェックする。
                                    If Not StringUtil.Equals(ekVo.Saishiyoufuka, kbVo.Saishiyoufuka) Then
                                        Continue For
                                    End If

                                    '手配帳側は集計コードがまとめられているので'
                                    If StringUtil.IsEmpty(ekVo.ShukeiCode) Then
                                        If Not StringUtil.Equals(ekVo.SiaShukeiCode, kbVo.ShukeiCode) Then
                                            Continue For
                                        End If
                                    Else
                                        If Not StringUtil.Equals(ekVo.ShukeiCode, kbVo.ShukeiCode) Then
                                            Continue For
                                        End If
                                    End If

                                    '一致した項目について改訂部品情報のフラグを確認する'
                                    Select Case ekVo.Flag
                                        Case "2"
                                            'ブロック不要扱いで削除'
                                            Dim aDate As New ShisakuDate
                                            '号車をダミーに移すだけで基本情報に対しては何もしない'
                                            'ダミー列の情報を取得する'
                                            Dim dummyVo As New TShisakuTehaiGousyaVo
                                            '基本情報とくっつくダミー列を取得'
                                            dummyVo = CyushutuImpl.FindByDummy(shisakuEventCode, shisakuListCode, shisakuListCodeKaiteiNo, kbVo.ShisakuBukaCode, kbVo.ShisakuBlockNo, kbVo.BuhinNoHyoujiJun)
                                            'ダミー列が存在するかチェック'
                                            If dummyVo Is Nothing Then
                                                '存在しないのでダミー列は追加となる'
                                                '' 2013/12/02　DUMMY列の最大値を求めないと号車列と被って落ちることがあるので対策として追加。
                                                ''   改訂№を外した。
                                                'dummyVo = CyushutuImpl.FindByDummy2222(shisakuEventCode, shisakuListCode)
                                                'dummyVo = CyushutuImpl.FindByDummy2(shisakuEventCode, shisakuListCode, shisakuListCodeKaiteiNo)
                                                'If dummyVo Is Nothing Then

                                                dummyVo = New TShisakuTehaiGousyaVo
                                                dummyVo.ShisakuEventCode = shisakuEventCode
                                                dummyVo.ShisakuListCode = shisakuListCode
                                                dummyVo.ShisakuListCodeKaiteiNo = shisakuListCodeKaiteiNo
                                                dummyVo.ShisakuBukaCode = kbVo.ShisakuBukaCode
                                                dummyVo.ShisakuBlockNo = kbVo.ShisakuBlockNo
                                                dummyVo.ShisakuGousyaHyoujiJun = dummyGousyaHyoujiJun
                                                '部品番号表示順は基本にあわせる'
                                                dummyVo.BuhinNoHyoujiJun = kbVo.BuhinNoHyoujiJun
                                                dummyVo.ShisakuGousya = "DUMMY"

                                                'End If

                                                '新規のダミー列なので作成する'
                                                dummyVo = DummySet(dummyVo, kbVo, listVo.ShisakuGroupNo, ekVo, CyushutuImpl)
                                                '号車のリストを取得する'
                                                Dim gousyaList As New List(Of TShisakuTehaiGousyaVo)
                                                gousyaList = CyushutuImpl.FindByTehaiGousyaList(kbVo.ShisakuEventCode, kbVo.ShisakuListCode, kbVo.ShisakuListCodeKaiteiNo, kbVo.ShisakuBukaCode, kbVo.ShisakuBlockNo, kbVo.BuhinNoHyoujiJun)
                                                '号車の員数を０にする'
                                                For Each gVo As TShisakuTehaiGousyaVo In gousyaList
                                                    gVo.InsuSuryo = 0
                                                    '更新リストに追加'
                                                    UpdateGousyaVoList.Add(gVo)
                                                Next
                                                '追加リストにダミーを追加'
                                                InsertGousyaVoList.Add(dummyVo)

                                            Else
                                                '存在するのでダミー列は更新となる'
                                                dummyVo.InsuSuryo = kbVo.TotalInsuSuryo
                                                UpdateGousyaVoList.Add(dummyVo)
                                            End If

                                            '号車が移るので織込みする'
                                            kbVo.AutoOrikomiKaiteiNo = kbVo.ShisakuListCodeKaiteiNo
                                            UpdateKihonVoList.Add(kbVo)

                                        Case "3"
                                            '変更後'
                                            '存在はするので、置き換えればよい'
                                            '存在する場合としない場合がある'
                                            Dim kihonVo As New TShisakuTehaiKihonVo
                                            If StringUtil.Equals("A", ekVo.Saishiyoufuka) Then
                                                '再使用不可は織込み不可にする'
                                                UpdateNotOrikomiList.Add(ekVo)
                                                Continue For
                                            End If

                                            ''2015/09/30 追加 E.Ubukata Ver.2.11.3
                                            '' 履歴情報に"*"が入っていた場合、基本情報を変更しない
                                            Dim aRireki As String = Trim(kbVo.Rireki)
                                            Dim aTotalInsuSuryo As Integer = kbVo.TotalInsuSuryo
                                            kihonVo = KihonVoSet(kbVo, ekVo, sakimpl, listVo.ShisakuSeihinKbn, blockVo.UnitKbn, listVo.ShisakuGroupNo, CyushutuImpl)

                                            If aRireki = "*" Then
                                                If aTotalInsuSuryo <> kihonVo.TotalInsuSuryo Then
                                                    ''員数数量が異なっていたら織込み不可リストに追加
                                                    UpdateNotOrikomiList.Add(ekVo)
                                                    Continue For
                                                End If
                                            Else
                                                UpdateKihonVoList.Add(kihonVo)
                                            End If

                                            '部品番号表示順は手配基本情報のものを使う'
                                            Dim delGousyaList As New List(Of TShisakuTehaiGousyaVo)
                                            delGousyaList = CyushutuImpl.FindByTehaiGousyaList(shisakuEventCode, _
                                                                                              kbVo.ShisakuListCode, _
                                                                                              kbVo.ShisakuListCodeKaiteiNo, _
                                                                                              ekVo.ShisakuBukaCode, _
                                                                                              ekVo.ShisakuBlockNo, _
                                                                                              kbVo.BuhinNoHyoujiJun)

                                            '削除のため号車を取得する'
                                            For Each gVo As TShisakuTehaiGousyaVo In delGousyaList
                                                gVo.BuhinNoHyoujiJun = kihonVo.BuhinNoHyoujiJun
                                                delGousyaVoList.Add(gVo)
                                            Next
                                            '追加のため号車を取得する'
                                            Dim insGousyaList As New List(Of TShisakuTehaiGousyaVo)
                                            insGousyaList = CyushutuImpl.FindByGousyaInsuList(shisakuEventCode, _
                                                                                              shisakuListCode, _
                                                                                              shisakuListCodeKaiteiNo, _
                                                                                              ekVo.ShisakuBukaCode, _
                                                                                              ekVo.ShisakuBlockNo, _
                                                                                              ekVo.ShisakuBlockNoKaiteiNo, _
                                                                                              ekVo.BuhinNoHyoujiJun, _
                                                                                              ekVo.Flag)
                                            For Each igVo As TShisakuTehaiGousyaVo In insGousyaList
                                                igVo.BuhinNoHyoujiJun = kbVo.BuhinNoHyoujiJun
                                                InsertGousyaVoList.Add(igVo)
                                            Next

                                        Case "7"
                                            '削除'
                                            '1と対になる処理'
                                            '手配側に存在するが、部品側には存在しない'
                                            '単純に削除されただけ'
                                            Dim aDate As New ShisakuDate

                                            '号車をダミーに移すだけで基本情報に対しては何もしない'
                                            'ダミー列の情報を取得する'
                                            Dim dummyVo As New TShisakuTehaiGousyaVo
                                            dummyVo = CyushutuImpl.FindByDummy(shisakuEventCode, shisakuListCode, shisakuListCodeKaiteiNo, kbVo.ShisakuBukaCode, kbVo.ShisakuBlockNo, kbVo.BuhinNoHyoujiJun)
                                            'ダミー列が存在するかチェック'
                                            If dummyVo Is Nothing Then
                                                '存在しないのでダミー列は追加となる'
                                                ' 2013/12/02　DUMMY列の最大値を求めないと号車列と被って落ちることがあるので対策として追加。
                                                '   改訂№を外した。
                                                'dummyVo = CyushutuImpl.FindByDummy2222(shisakuEventCode, shisakuListCode)
                                                'dummyVo = CyushutuImpl.FindByDummy2(shisakuEventCode, shisakuListCode, shisakuListCodeKaiteiNo)
                                                'If dummyVo Is Nothing Then

                                                dummyVo = New TShisakuTehaiGousyaVo
                                                dummyVo.ShisakuEventCode = shisakuEventCode
                                                dummyVo.ShisakuListCode = shisakuListCode
                                                dummyVo.ShisakuListCodeKaiteiNo = shisakuListCodeKaiteiNo
                                                dummyVo.ShisakuBukaCode = kbVo.ShisakuBukaCode
                                                dummyVo.ShisakuBlockNo = kbVo.ShisakuBlockNo
                                                dummyVo.ShisakuGousyaHyoujiJun = dummyGousyaHyoujiJun
                                                dummyVo.ShisakuGousya = "DUMMY"

                                                'End If

                                                '新規のダミー列なので作成する'
                                                dummyVo = DummySet(dummyVo, kbVo, listVo.ShisakuGroupNo, ekVo, CyushutuImpl)
                                                Dim gousyaList As New List(Of TShisakuTehaiGousyaVo)
                                                gousyaList = CyushutuImpl.FindByTehaiGousyaList(kbVo.ShisakuEventCode, kbVo.ShisakuListCode, kbVo.ShisakuListCodeKaiteiNo, kbVo.ShisakuBukaCode, kbVo.ShisakuBlockNo, kbVo.BuhinNoHyoujiJun)
                                                For Each gVo As TShisakuTehaiGousyaVo In gousyaList
                                                    gVo.InsuSuryo = 0
                                                    UpdateGousyaVoList.Add(gVo)
                                                Next
                                                InsertGousyaVoList.Add(dummyVo)
                                            Else
                                                '存在するのでダミー列は更新となる'
                                                dummyVo.InsuSuryo = kbVo.TotalInsuSuryo
                                                UpdateGousyaVoList.Add(dummyVo)
                                            End If
                                            '削除ならアップデートもする'
                                            kbVo.AutoOrikomiKaiteiNo = kbVo.ShisakuListCodeKaiteiNo
                                            UpdateKihonVoList.Add(kbVo)

                                    End Select
                                End If
                            Next

                        ElseIf StringUtil.Equals(ekVo.Flag, "1") Then

                            buhinNoHyoujiJun = buhinNoHyoujiJun + 1
                            gyouId = Right("000" + StringUtil.ToString((Integer.Parse(gyouId + 1))), 3)

                            '手配帳に存在しないので追加する処理のみ'
                            '追加扱いの部品'
                            Dim vo As New TShisakuTehaiKihonVo
                            vo = KihonVoSet(ekVo, sakimpl, listVo.ShisakuSeihinKbn, blockVo.UnitKbn, listVo.ShisakuGroupNo, CyushutuImpl, buhinNoHyoujiJun, gyouId)
                            Dim gVo As New List(Of TShisakuTehaiGousyaVo)
                            InsertKihonVoList.Add(vo)

                            Dim gousyaVoList As New List(Of TShisakuTehaiGousyaVo)
                            gousyaVoList = CyushutuImpl.FindByGousyaInsuList(shisakuEventCode, _
                                                                             shisakuListCode, _
                                                                             shisakuListCodeKaiteiNo, _
                                                                             ekVo.ShisakuBukaCode, _
                                                                             ekVo.ShisakuBlockNo, _
                                                                             ekVo.ShisakuBlockNoKaiteiNo, _
                                                                             ekVo.BuhinNoHyoujiJun, _
                                                                             ekVo.Flag)

                            For Each gVo2 As TShisakuTehaiGousyaVo In gousyaVoList
                                gVo2.BuhinNoHyoujiJun = buhinNoHyoujiJun
                                InsertGousyaVoList.Add(gVo2)
                            Next
                        End If
                    Next

                    '削除リストを元に削除'
                    For Each delVo As TShisakuTehaiKihonVo In DelKihonVoList
                        delVo.ShisakuEventCode = shisakuEventCode
                        delVo.ShisakuListCode = shisakuListCode
                        delVo.ShisakuListCodeKaiteiNo = shisakuListCodeKaiteiNo
                        CyushutuImpl.DelteByKihonGousya(delVo)
                    Next
                    For Each delgVo As TShisakuTehaiGousyaVo In delGousyaVoList
                        delgVo.ShisakuEventCode = shisakuEventCode
                        delgVo.ShisakuListCode = shisakuListCode
                        delgVo.ShisakuListCodeKaiteiNo = shisakuListCodeKaiteiNo
                        CyushutuImpl.DelteByGousya(delgVo)
                    Next

                    '更新リストを元に更新'
                    For Each insVo As TShisakuTehaiKihonVo In UpdateKihonVoList
                        insVo.AutoOrikomiKaiteiNo = insVo.ShisakuListCodeKaiteiNo
                        CyushutuImpl.UpdateByTehaiKihon(insVo)
                    Next
                    For Each insgVo As TShisakuTehaiGousyaVo In UpdateGousyaVoList
                        'DUMMY列で号車表示順がアンマッチなら削除後インサートする。
                        If StringUtil.Equals(insgVo.ShisakuGousya.TrimEnd, "DUMMY") And _
                            Not StringUtil.Equals(insgVo.ShisakuGousyaHyoujiJun, dummyGousyaHyoujiJun) Then
                            insgVo.ShisakuEventCode = shisakuEventCode
                            insgVo.ShisakuListCode = shisakuListCode
                            insgVo.ShisakuListCodeKaiteiNo = shisakuListCodeKaiteiNo
                            'まずは削除。
                            CyushutuImpl.DelteByGousya(insgVo)
                            '号車表示順を置き換えて追加。
                            insgVo.ShisakuGousyaHyoujiJun = dummyGousyaHyoujiJun
                            CyushutuImpl.InsertByTehaiGousya(insgVo)
                        Else
                            CyushutuImpl.UpdateByTehaiGousya(insgVo)
                        End If
                    Next

                    '追加リストを元に追加'
                    For Each insVo As TShisakuTehaiKihonVo In InsertKihonVoList
                        CyushutuImpl.InsertByTehaiKihon(insVo)
                    Next
                    For Each insgVo As TShisakuTehaiGousyaVo In InsertGousyaVoList
                        '-----------------------------------------------------------------
                        '号車別納期日を取得
                        Dim GousyaNoukiVo As New TShisakuTehaiGousyaVo
                        GousyaNoukiVo = CyushutuImpl.FindByTehaiGousyaNouki(insgVo.ShisakuEventCode, _
                                                                            insgVo.ShisakuListCode, _
                                                                            insgVo.ShisakuListCodeKaiteiNo, _
                                                                            insgVo.ShisakuBukaCode, _
                                                                            insgVo.ShisakuBlockNo, _
                                                                            insgVo.ShisakuGousya)
                        If StringUtil.IsNotEmpty(GousyaNoukiVo) Then
                            insgVo.MNounyuShijibi = GousyaNoukiVo.MNounyuShijibi
                            insgVo.TNounyuShijibi = GousyaNoukiVo.TNounyuShijibi
                        End If
                        '-----------------------------------------------------------------
                        insgVo.ShisakuListCode = shisakuListCode
                        insgVo.ShisakuListCodeKaiteiNo = shisakuListCodeKaiteiNo
                        insgVo.SortJun = insgVo.BuhinNoHyoujiJun
                        'DUMMY列なら号車表示順を置き換える。
                        If StringUtil.Equals(insgVo.ShisakuGousya.TrimEnd, "DUMMY") Then
                            insgVo.ShisakuGousyaHyoujiJun = dummyGousyaHyoujiJun
                        End If
                        '重複対策'
                        CyushutuImpl.DelteByGousya(insgVo)
                        CyushutuImpl.InsertByTehaiGousya(insgVo)
                    Next

                    '織込み不可リストを元に更新'
                    For Each oVo As TShisakuBuhinEditKaiteiVo In UpdateNotOrikomiList
                        oVo.AutoOrikomiKaiteiNo = shisakuListCodeKaiteiNo
                        CyushutuImpl.UpdateByOrikomi(oVo)
                    Next
                Next
                CyushutuImpl.UpdateByListCode(shisakuEventCode, shisakuListCode, shisakuListCodeKaiteiNo)

            Catch ex As Exception

                CyushutuImpl.DeleteByAllTehaiKihon(shisakuEventCode, shisakuListCode, shisakuListCodeKaiteiNo)
                CyushutuImpl.DeleteByAllTehaiGousya(shisakuEventCode, shisakuListCode, shisakuListCodeKaiteiNo)
                CyushutuImpl.InsertByAllTehaiKihon(backKihonList)
                CyushutuImpl.InsertByAllTehaiGousya(backGousyaList)
                MsgBox("自動織込みに失敗しました。", MsgBoxStyle.OkOnly, "エラー")
            End Try

        End Sub

        ''' <summary>
        ''' 登録用手配基本情報の設定
        ''' </summary>
        ''' <param name="buhinVo">部品編集改訂情報</param>
        ''' <param name="sakimpl"></param>
        ''' <param name="seihinKbn">製品区分</param>
        ''' <param name="UnitKbn">ユニット区分</param>
        ''' <param name="shisakuGroup">試作グループ</param>
        ''' <param name="CyushutuImpl"></param>
        ''' <param name="buhinNoHyoujiJun">部品番号表示順</param>
        ''' <param name="gyouId">行ID</param>
        ''' <returns>手配基本情報</returns>
        ''' <remarks></remarks>
        Private Function KihonVoSet(ByVal buhinVo As TShisakuBuhinEditKaiteiVo, _
                                    ByVal sakimpl As TehaichoSakusei.Dao.TehaichoSakuseiDao, _
                                    ByVal seihinKbn As String, _
                                    ByVal UnitKbn As String, _
                                    ByVal shisakuGroup As String, _
                                    ByVal CyushutuImpl As KaiteiChusyutuDao, _
                                    ByVal buhinNoHyoujiJun As Integer, _
                                    ByVal gyouId As String) As TShisakuTehaiKihonVo
            Dim vo As New TShisakuTehaiKihonVo
            vo.ShisakuEventCode = buhinVo.ShisakuEventCode
            vo.ShisakuBukaCode = buhinVo.ShisakuBukaCode
            vo.ShisakuListCode = shisakuListCode
            vo.ShisakuListCodeKaiteiNo = shisakuListCodeKaiteiNo
            vo.ShisakuBlockNo = buhinVo.ShisakuBlockNo
            vo.Level = buhinVo.Level
            vo.BuhinNo = buhinVo.BuhinNo
            vo.BuhinNoKbn = buhinVo.BuhinNoKbn
            vo.BuhinNoKaiteiNo = buhinVo.BuhinNoKaiteiNo
            vo.EdaBan = buhinVo.EdaBan
            vo.BuhinName = buhinVo.BuhinName
            '集計コード'
            If StringUtil.IsEmpty(buhinVo.ShukeiCode) Then
                vo.ShukeiCode = buhinVo.SiaShukeiCode
            Else
                vo.ShukeiCode = buhinVo.ShukeiCode
            End If
            vo.GencyoCkdKbn = buhinVo.GencyoCkdKbn
            vo.KyoukuSection = buhinVo.KyoukuSection
            vo.NounyuShijibi = 0
            vo.Saishiyoufuka = buhinVo.Saishiyoufuka
            vo.ShutuzuYoteiDate = buhinVo.ShutuzuYoteiDate
            vo.ZaishituKikaku1 = buhinVo.ZaishituKikaku1
            vo.ZaishituKikaku2 = buhinVo.ZaishituKikaku2
            vo.ZaishituKikaku3 = buhinVo.ZaishituKikaku3
            vo.ZaishituMekki = buhinVo.ZaishituMekki
            vo.ShisakuBankoSuryo = buhinVo.ShisakuBankoSuryo
            vo.ShisakuBankoSuryoU = buhinVo.ShisakuBankoSuryoU


            ''↓↓2014/12/29 メタル対応追加フィールド (DANIEL)柳沼 ADD BEGIN
            vo.MaterialInfoLength = buhinVo.MaterialInfoLength
            vo.MaterialInfoWidth = buhinVo.MaterialInfoWidth
            vo.DataItemKaiteiNo = buhinVo.DataItemKaiteiNo
            vo.DataItemAreaName = buhinVo.DataItemAreaName
            vo.DataItemSetName = buhinVo.DataItemSetName
            vo.DataItemKaiteiInfo = buhinVo.DataItemKaiteiInfo
            ''↑↑2014/12/29 メタル対応追加フィールド (DANIEL)柳沼 ADD END


            vo.ShisakuBuhinnHi = buhinVo.ShisakuBuhinHi
            vo.ShisakuKataHi = buhinVo.ShisakuKataHi
            vo.Bikou = buhinVo.Bikou


            '部品番号表示順をどうするか？'
            'ソート順は部品番号表示順と同じ'

            vo.BuhinNoHyoujiJun = buhinNoHyoujiJun
            vo.SortJun = Integer.Parse(gyouId)

            '図面説通No'
            vo.ShutuzuJisekiStsrDhstba = sakimpl.FindByZumenNo(buhinVo.BuhinNo)
            '後で振る必要がある、あるいは最後の行ID+1を取得する'
            vo.GyouId = ""
            '専用品かチェックする'
            If Not sakimpl.FindBySenyouCheck(buhinVo.BuhinNo, seihinKbn) Then
                vo.SenyouMark = "*"
            Else
                vo.SenyouMark = ""
            End If

            '合計員数数量'
            '承認状態とか見るの？'
            'イベントコード、部品番号、レベル、供給セクション'
            vo.TotalInsuSuryo = CyushutuImpl.GetTotalInsuSuryo(buhinVo.ShisakuEventCode, _
                                                               buhinVo.ShisakuBukaCode, _
                                                               buhinVo.ShisakuBlockNo, _
                                                               buhinVo.ShisakuBlockNoKaiteiNo, _
                                                               shisakuGroup, _
                                                               buhinVo.BuhinNoHyoujiJun)

            If vo.TotalInsuSuryo < 0 Then
                '手配記号'
                vo.TehaiKigou = "F"
                '納場'
                vo.Nouba = ""
            Else
                If Not StringUtil.IsEmpty(buhinVo.ShukeiCode) Then
                    Select Case buhinVo.ShukeiCode
                        Case "X", "R", "J"
                            vo.TehaiKigou = "F"
                            vo.Nouba = ""
                        Case "A"
                            vo.TehaiKigou = ""
                            vo.Nouba = "X1"
                        Case "E", "Y"
                            If StringUtil.IsEmpty(vo.SenyouMark) Then
                                vo.TehaiKigou = "A"
                                vo.Nouba = "A0"
                            Else
                                vo.TehaiKigou = "D"
                                vo.Nouba = "X1"
                            End If
                    End Select
                Else
                    Select Case buhinVo.SiaShukeiCode
                        Case "X", "R", "J"
                            vo.TehaiKigou = "F"
                            vo.Nouba = ""
                        Case "A"
                            vo.TehaiKigou = "J"
                            vo.Nouba = "US"
                        Case "E", "Y"
                            vo.TehaiKigou = "B"
                            vo.Nouba = "US"
                    End Select

                End If
            End If

            '購担'
            '取引先コードが変更になる可能性があるのでどうする？'

            Dim result As New TShisakuBuhinEditTmpVo
            result = sakimpl.FindByKoutanTorihikisaki(vo.BuhinNo)
            If Not result Is Nothing Then
                vo.Koutan = result.Koutan
            Else
                vo.Koutan = ""
            End If
            vo.TorihikisakiCode = buhinVo.MakerCode
            vo.GyouId = gyouId

            '履歴'
            vo.Rireki = ""
            'ユニット区分'
            vo.UnitKbn = UnitKbn
            vo.MakerCode = buhinVo.MakerName
            vo.BuhinNoOya = ""
            vo.BuhinNoKbnOya = ""
            vo.ErrorKbn = ""
            vo.AudFlag = ""
            vo.AudBi = 0
            vo.KetugouNo = ""
            vo.Henkaten = ""
            vo.ShisakuSeihinKbn = seihinKbn
            vo.AutoOrikomiKaiteiNo = shisakuListCodeKaiteiNo

            Return vo
        End Function

        ''' <summary>
        ''' 登録用手配基本情報の設定
        ''' </summary>
        ''' <param name="Vo">手配基本情報</param>
        ''' <param name="buhinVo">部品編集改訂情報</param>
        ''' <param name="sakimpl">手配帳作成Dao</param>
        ''' <param name="seihinKbn">製品区分</param>
        ''' <param name="UnitKbn">ユニット区分</param>
        ''' <param name="shisakuGroup">試作グループ</param>
        ''' <param name="CyushutuImpl">改訂抽出Dao</param>
        ''' <returns>手配基本情報</returns>
        ''' <remarks></remarks>
        Private Function KihonVoSet(ByVal Vo As TShisakuTehaiKihonVo, _
                                    ByVal buhinVo As TShisakuBuhinEditKaiteiVo, _
                                    ByVal sakimpl As TehaichoSakusei.Dao.TehaichoSakuseiDao, _
                                    ByVal seihinKbn As String, _
                                    ByVal UnitKbn As String, _
                                    ByVal shisakuGroup As String, _
                                    ByVal CyushutuImpl As KaiteiChusyutuDao) As TShisakuTehaiKihonVo
            '変更後で使用する'
            Vo.Level = buhinVo.Level
            Vo.BuhinNoKbn = buhinVo.BuhinNoKbn
            Vo.BuhinNoKaiteiNo = buhinVo.BuhinNoKaiteiNo
            Vo.EdaBan = buhinVo.EdaBan
            Vo.BuhinName = buhinVo.BuhinName
            '集計コードはそのまま'
            Vo.GencyoCkdKbn = buhinVo.GencyoCkdKbn
            Vo.KyoukuSection = buhinVo.KyoukuSection
            '納入指示日'
            Vo.Saishiyoufuka = buhinVo.Saishiyoufuka
            Vo.ShutuzuYoteiDate = buhinVo.ShutuzuYoteiDate
            Vo.ZaishituKikaku1 = buhinVo.ZaishituKikaku1
            Vo.ZaishituKikaku2 = buhinVo.ZaishituKikaku2
            Vo.ZaishituKikaku3 = buhinVo.ZaishituKikaku3
            Vo.ZaishituMekki = buhinVo.ZaishituMekki
            Vo.ShisakuBankoSuryo = buhinVo.ShisakuBankoSuryo
            Vo.ShisakuBankoSuryoU = buhinVo.ShisakuBankoSuryoU


            ''↓↓2014/12/29 メタル対応追加フィールド (DANIEL)柳沼 ADD BEGIN
            Vo.MaterialInfoLength = buhinVo.MaterialInfoLength
            Vo.MaterialInfoWidth = buhinVo.MaterialInfoWidth
            Vo.DataItemKaiteiNo = buhinVo.DataItemKaiteiNo
            Vo.DataItemAreaName = buhinVo.DataItemAreaName
            Vo.DataItemSetName = buhinVo.DataItemSetName
            Vo.DataItemKaiteiInfo = buhinVo.DataItemKaiteiInfo
            ''↑↑2014/12/29 メタル対応追加フィールド (DANIEL)柳沼 ADD END


            Vo.ShisakuBuhinnHi = buhinVo.ShisakuBuhinHi
            Vo.ShisakuKataHi = buhinVo.ShisakuKataHi
            Vo.Bikou = buhinVo.Bikou


            '部品番号表示順とソート順は同じ'

            '図面説通No'
            Vo.ShutuzuJisekiStsrDhstba = sakimpl.FindByZumenNo(buhinVo.BuhinNo)
            '後で振る必要がある、あるいは最後の行ID+1を取得する'
            Vo.GyouId = ""
            '専用品かチェックする'
            If Not sakimpl.FindBySenyouCheck(buhinVo.BuhinNo, seihinKbn) Then
                Vo.SenyouMark = "*"
            Else
                Vo.SenyouMark = ""
            End If

            '合計員数数量'
            '承認状態とか見るの？'
            'イベントコード、部品番号、レベル、供給セクション'
            Vo.TotalInsuSuryo = CyushutuImpl.GetTotalInsuSuryo(buhinVo.ShisakuEventCode, _
                                                               buhinVo.ShisakuBukaCode, _
                                                               buhinVo.ShisakuBlockNo, _
                                                               buhinVo.ShisakuBlockNoKaiteiNo, _
                                                               shisakuGroup, _
                                                               buhinVo.BuhinNoHyoujiJun)

            '手配記号はそのまま'

            '購担はそのまま'
            Vo.TorihikisakiCode = buhinVo.MakerCode

            '履歴'
            Vo.Rireki = ""
            'ユニット区分'
            Vo.UnitKbn = UnitKbn
            Vo.MakerCode = buhinVo.MakerName
            Vo.BuhinNoOya = ""
            Vo.BuhinNoKbnOya = ""
            Vo.ErrorKbn = ""
            Vo.AudFlag = ""
            Vo.AudBi = 0
            Vo.KetugouNo = ""
            Vo.Henkaten = ""
            Vo.ShisakuSeihinKbn = seihinKbn
            Vo.AutoOrikomiKaiteiNo = shisakuListCodeKaiteiNo

            Return Vo
        End Function

        ''' <summary>
        ''' ダミー列のセット(ダミー列が存在しない場合)
        ''' </summary>
        ''' <param name="dummyVo">手配号車情報</param>
        ''' <param name="tehaikihonVo">手配基本情報</param>
        ''' <param name="shisakuGroup">試作グループ</param>
        ''' <param name="editKaiteiVo">部品編集改訂情報</param>
        ''' <param name="CyushutuImpl">改訂抽出Dao</param>
        ''' <returns>手配号車情報</returns>
        ''' <remarks></remarks>
        Private Function DummySet(ByVal dummyVo As TShisakuTehaiGousyaVo, ByVal tehaikihonVo As TShisakuTehaiKihonVo, ByVal shisakuGroup As String, ByVal editKaiteiVo As TShisakuBuhinEditKaiteiVo, ByVal CyushutuImpl As KaiteiChusyutuDao) As TShisakuTehaiGousyaVo
            '員数だけをセットする'
            dummyVo.ShisakuEventCode = tehaikihonVo.ShisakuEventCode
            dummyVo.ShisakuListCode = tehaikihonVo.ShisakuListCode
            dummyVo.ShisakuListCodeKaiteiNo = tehaikihonVo.ShisakuListCodeKaiteiNo
            dummyVo.ShisakuBukaCode = tehaikihonVo.ShisakuBukaCode
            dummyVo.ShisakuBlockNo = tehaikihonVo.ShisakuBlockNo
            dummyVo.BuhinNoHyoujiJun = tehaikihonVo.BuhinNoHyoujiJun
            dummyVo.GyouId = tehaikihonVo.GyouId
            dummyVo.SortJun = tehaikihonVo.BuhinNoHyoujiJun
            dummyVo.InsuSuryo = tehaikihonVo.TotalInsuSuryo

            Return dummyVo
        End Function


    End Class
End Namespace