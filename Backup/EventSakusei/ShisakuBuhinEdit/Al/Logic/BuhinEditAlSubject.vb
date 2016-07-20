Imports EventSakusei.ShisakuBuhinEdit.Logic.Detect
Imports EventSakusei.ShisakuBuhinEdit.Al.Dao
Imports EventSakusei.ShisakuBuhinEdit.Logic
Imports ShisakuCommon
Imports ShisakuCommon.Db.EBom.Dao.Impl
Imports ShisakuCommon.Db.EBom.Vo.Helper
Imports ShisakuCommon.Db.EBom.Dao
Imports ShisakuCommon.Util
Imports ShisakuCommon.Db.EBom.Vo
Imports ShisakuCommon.DateUtil
Imports EventSakusei.Soubi
Imports ShisakuCommon.Db.EBom
Imports EventSakusei.ShisakuBuhinEdit.Kosei.Logic.Matrix
'↓↓2014/10/23 酒井 ADD BEGIN
Imports EventSakusei.ShisakuBuhinEdit.Kosei.Logic
'↑↑2014/10/23 酒井 ADD END

Namespace ShisakuBuhinEdit.Al.Logic

    ''' <summary>
    ''' A/Lの表示全般を担うクラス
    ''' </summary>
    ''' <remarks></remarks>
    Public Class BuhinEditAlSubject : Inherits Observable
        Private Shared ReadOnly DEFAULT_SOUBI_VOS As List(Of TShisakuSekkeiBlockSoubiVo)

        ''' <summary>
        ''' 初期設定
        ''' </summary>
        ''' <remarks></remarks>
        Shared Sub New()
            '完成車情報の仕向けを表示させる'
            Dim v1 As New TShisakuSekkeiBlockSoubiVo
            v1.ShisakuSoubiHyoujiJun = TShisakuSekkeiBlockSoubiVoHelper.ShisakuSoubiHyoujiJun.KANSEI_SHIMUKE
            Dim v2 As New TShisakuSekkeiBlockSoubiVo
            v2.ShisakuSoubiHyoujiJun = TShisakuSekkeiBlockSoubiVoHelper.ShisakuSoubiHyoujiJun.KANSEI_HANDORU
            Dim v3 As New TShisakuSekkeiBlockSoubiVo
            v3.ShisakuSoubiHyoujiJun = TShisakuSekkeiBlockSoubiVoHelper.ShisakuSoubiHyoujiJun.KANSEI_SYAGATA
            '2012/01/25 デフォルト表示にグループを追加
            Dim v4 As New TShisakuSekkeiBlockSoubiVo
            v4.ShisakuSoubiHyoujiJun = TShisakuSekkeiBlockSoubiVoHelper.ShisakuSoubiHyoujiJun.KANSEI_GROUP
            Dim v5 As New TShisakuSekkeiBlockSoubiVo
            v5.ShisakuSoubiHyoujiJun = TShisakuSekkeiBlockSoubiVoHelper.ShisakuSoubiHyoujiJun.KANSEI_EG_HAIKIRYOU
            Dim v6 As New TShisakuSekkeiBlockSoubiVo
            v6.ShisakuSoubiHyoujiJun = TShisakuSekkeiBlockSoubiVoHelper.ShisakuSoubiHyoujiJun.KANSEI_EG_KAKYUUKI
            Dim v7 As New TShisakuSekkeiBlockSoubiVo
            v7.ShisakuSoubiHyoujiJun = TShisakuSekkeiBlockSoubiVoHelper.ShisakuSoubiHyoujiJun.KANSEI_TM_KUDOU
            Dim v8 As New TShisakuSekkeiBlockSoubiVo
            v8.ShisakuSoubiHyoujiJun = TShisakuSekkeiBlockSoubiVoHelper.ShisakuSoubiHyoujiJun.KANSEI_TM_HENSOKUKI
            DEFAULT_SOUBI_VOS = New List(Of TShisakuSekkeiBlockSoubiVo)(New TShisakuSekkeiBlockSoubiVo() {v1, v2, v3, v4, v5, v6, v7, v8})
        End Sub


        Private _blockKeyVo As TShisakuSekkeiBlockVo
        Private ReadOnly login As LoginInfo
        Private ReadOnly aShisakuDate As ShisakuDate
        Private ReadOnly anEzSync As EzSyncInstlHinban
        Private ReadOnly soubiDao As TShisakuSekkeiBlockSoubiDao
        Private ReadOnly soubiShiyoDao As TShisakuSekkeiBlockSoubiShiyouDao
        Private ReadOnly alDao As BuhinEditAlDao

        Private _showColumnBag As BuhinEditAlShowColumnBag

        Private alEvent As BuhinEditAlEvent
        Private alBasicOption As BuhinEditAlOption
        Private alSpecialOption As BuhinEditAlOption

        Private memoSupplier As BuhinEditAlMemoSupplier
        ''↓↓2014/08/29 Ⅰ.3.設計編集 ベース改修専用化_ap) 酒井 ADD BEGIN
        'Private instlSupplier As BuhinEditAlInstlSupplier
        Public instlSupplier As BuhinEditAlInstlSupplier
        ''↑↑2014/08/29 Ⅰ.3.設計編集 ベース改修専用化_ap) 酒井 ADD END

        Private copyEventCode As String

        ''↓↓2014/09/01 Ⅰ.3.設計編集 ベース改修専用化_ap) 酒井 ADD BEGIN
        Public CheckFlg As New List(Of Boolean)
        ''↑↑2014/09/01 Ⅰ.3.設計編集 ベース改修専用化_ap) 酒井 ADD END

        'イベントコピー処理に対する列位置記憶用カウンター
        Public _EventCopyBeforeCnt As Integer = -1

        ''' <summary>
        ''' コンストラクタ
        ''' </summary>
        ''' <param name="blockKeyVo">表示対象の試作設計ブロック情報</param>
        ''' <param name="login">ログイン情報</param>
        ''' <param name="aShisakuDate">試作日付</param>
        ''' <param name="soubiDao">試作設計ブロック装備Dao</param>
        ''' <param name="soubiShiyoDao">試作設計ブロック装備仕様Dao</param>
        ''' <param name="alDao">A/L表示機能Dao</param>
        ''' <remarks></remarks>
        Public Sub New(ByVal blockKeyVo As TShisakuSekkeiBlockVo, _
                       ByVal login As LoginInfo, _
                       ByVal aShisakuDate As ShisakuDate, _
                       ByVal anEzSync As EzSyncInstlHinban, _
                       ByVal soubiDao As TShisakuSekkeiBlockSoubiDao, _
                       ByVal soubiShiyoDao As TShisakuSekkeiBlockSoubiShiyouDao, _
                       ByVal alDao As BuhinEditAlDao)

            Me._blockKeyVo = blockKeyVo
            Me.login = login
            Me.aShisakuDate = aShisakuDate
            Me.anEzSync = anEzSync
            Me.soubiDao = soubiDao
            Me.soubiShiyoDao = soubiShiyoDao
            Me.alDao = alDao
            _blockNo = blockKeyVo.ShisakuBlockNo

            shisakuEventCode = blockKeyVo.ShisakuEventCode

            _showColumnBag = MakeShowColumnBag(blockKeyVo)

            Me.alEvent = New BuhinEditAlEvent(blockKeyVo.ShisakuEventCode, _showColumnBag.SoubiVos, alDao)

            Dim optionDao As EventSoubiDao = New EventSoubiDaoImpl
            Dim eventSoubiDao As TShisakuEventSoubiDao = New TShisakuEventSoubiDaoImpl
            Me.alBasicOption = New BuhinEditAlOption(blockKeyVo.ShisakuEventCode, TShisakuEventSoubiVoHelper.ShisakuSoubiKbn.BASIC_OPTION, eventSoubiDao, optionDao, _showColumnBag.SoubiShiyouVos)
            Me.alSpecialOption = New BuhinEditAlOption(blockKeyVo.ShisakuEventCode, TShisakuEventSoubiVoHelper.ShisakuSoubiKbn.SPECIAL_OPTION, eventSoubiDao, optionDao, _showColumnBag.SoubiShiyouVos)

            Dim instlDao As TShisakuSekkeiBlockInstlDao = New TShisakuSekkeiBlockInstlDaoImpl
            Me.instlSupplier = New BuhinEditAlInstlSupplier(Me._blockKeyVo, alEvent.GetRowNoByGoshaIndexes, instlDao, New DetectLatestStructureImpl(blockKeyVo), Me.alDao)


            Me.memoSupplier = New BuhinEditAlMemoSupplier(blockKeyVo, alEvent.GetRowNoByGoshaIndexes, alDao)

            '仕様情報の画面表示用にイベント情報の最終更新日と部品表の最終更新日を取得します。
            '部品表の最終更新日を取得します。
            If Not StringUtil.IsEmpty(blockKeyVo.SaisyuKoushinbi) Then
                _SaisyuKoushinbi = blockKeyVo.SaisyuKoushinbi
                If Not StringUtil.IsEmpty(blockKeyVo.SaisyuKoushinjikan) Then
                    _SaisyuKoushinjikan = blockKeyVo.SaisyuKoushinjikan
                End If
            Else
                _SaisyuKoushinbi = Nothing
            End If
            'イベントコードで試作イベント情報を抽出する。
            'イベントの最終更新日を取得するためにイベントのDAO,VO追加
            Dim eventDao As TShisakuEventDao = New TShisakuEventDaoImpl
            Dim eventVo As TShisakuEventVo
            eventVo = eventDao.FindByPk(blockKeyVo.ShisakuEventCode)
            'イベントの最終更新日を取得します。
            If Not StringUtil.IsEmpty(eventVo.UpdatedDate) Then
                'ハイフンを取って返す。
                Dim hyphenNonDate As String = ConvHyphenYyyymmddToYyyymmdd(eventVo.UpdatedDate)
                _EventUpdatedDate = hyphenNonDate
                If Not StringUtil.IsEmpty(eventVo.UpdatedTime) Then
                    ' コロンを取って返す。
                    Dim colonNonTime As String = ConvTimeToIneteger(eventVo.UpdatedTime).ToString
                    _EventUpdatedTime = colonNonTime
                End If

            Else
                _EventUpdatedDate = Nothing
            End If
            '2012/03/07 「イベントが更新されました」のメッセージを出力する基準を
            '更新日時から登録日時（新しい属性）に変更
            'イベントの最終登録日を取得します。
            If Not StringUtil.IsEmpty(eventVo.RegisterDate) Then
                'ハイフンを取って返す。
                Dim hyphenNonDate As String = ConvHyphenYyyymmddToYyyymmdd(eventVo.RegisterDate)
                _EventRegisterDate = hyphenNonDate
                If Not StringUtil.IsEmpty(eventVo.RegisterTime) Then
                    ' コロンを取って返す。
                    Dim colonNonTime As String = ConvTimeToIneteger(eventVo.RegisterTime).ToString
                    _EventRegisterTime = colonNonTime
                End If

            Else
                _EventRegisterDate = Nothing
            End If

            SetChanged()
        End Sub

        ''' <summary>
        ''' 装備品列表示情報を作成する
        ''' </summary>
        ''' <param name="blockVo">試作設計ブロック情報</param>
        ''' <returns>装備品列表示情報</returns>
        ''' <remarks></remarks>
        Private Function MakeShowColumnBag(ByVal blockVo As TShisakuSekkeiBlockVo) As BuhinEditAlShowColumnBag

            Dim soubiVos As List(Of TShisakuSekkeiBlockSoubiVo) = FindSoubi(blockVo)
            Dim soubiShiyouVos As List(Of TShisakuSekkeiBlockSoubiShiyouVo) = FindSoubiShiyo(blockVo)
            If soubiVos.Count = 0 AndAlso soubiShiyouVos.Count = 0 Then
                soubiVos = DEFAULT_SOUBI_VOS
                ''↓↓2014/07/25 Ⅰ.4.特別織込みアラート追加_a) (TES)張 ADD BEGIN
                soubiShiyouVos = FindSoubiShiyoAlart(blockVo)
                ''↑↑2014/07/25 Ⅰ.4.特別織込みアラート追加_a) (TES)張 ADD END
            End If

            Return New BuhinEditAlShowColumnBag(soubiVos, soubiShiyouVos)
        End Function

        ''' <summary>
        ''' ブロックNoを差し替える
        ''' </summary>
        ''' <param name="blockVo">ブロックVo</param>
        ''' <remarks></remarks>
        Public Sub SupersedeBlockVo(ByVal blockVo As TShisakuSekkeiBlockVo)
            Me._blockKeyVo = blockVo
            _blockNo = Me._blockKeyVo.ShisakuBlockNo
            instlSupplier.SupersedeBlockVo(blockVo)

            ShowColumnBag = MakeShowColumnBag(blockVo)
        End Sub

        ''' <summary>
        ''' イベント品番のコピー
        ''' </summary>
        ''' <param name="shisakuEventCode"></param>
        ''' <param name="KaiteiNo">2014/08/04 Ⅰ.11.改訂戻し機能 f) (TES)施　追加</param>
        ''' <remarks>2014/08/04 Ⅰ.11.改訂戻し機能 f) (TES)施  修正</remarks>
        Public Sub EventCopy(ByVal shisakuEventCode As String, Optional ByVal KaiteiNo As String = "", Optional ByVal eventCopyFlg As Boolean = False, Optional ByVal baseFlg As Boolean = False, Optional ByVal koseiSubject As BuhinEditKoseiSubject = Nothing)
            '受け取ったイベントコードを元にALの中身を差し替える'
            'イベントコードとブロックNoを元に設計ブロックINSTL情報を取得する'
            Dim selDao As Selector.Dao.DispEventBuhinCopySelectorDao = New Selector.Dao.DispEventBuhinCopySelectorDaoImpl
            Dim sbiList As New List(Of TShisakuSekkeiBlockInstlVo)
            copyEventCode = shisakuEventCode
            '設計ブロックINSTL情報の取得'
            sbiList = selDao.FindBySekkeiBlockInstl(shisakuEventCode, BlockNo, KaiteiNo)

            Dim addStartIndex As Integer = 0
            If baseFlg Then
                If GetInputInstlHinbanColumnIndexes.Count = 0 Then
                    addStartIndex = 0
                Else
                    For Each index As Integer In GetInputInstlHinbanColumnIndexes()
                        If addStartIndex < index Then
                            addStartIndex = index
                        End If
                    Next
                    addStartIndex = addStartIndex + 1
                End If
            End If

            If sbiList.Count <> 0 Then

                Dim maeInstlHinbanHyoujiJun As Integer = -1
                For index As Integer = 0 To sbiList.Count - 1
                    If sbiList(index).InstlHinbanHyoujiJun = maeInstlHinbanHyoujiJun Then
                        Continue For
                    End If
                    InstlHinbanKbn(sbiList(index).InstlHinbanHyoujiJun + addStartIndex) = sbiList(index).InstlHinbanKbn
                    InstlHinban(sbiList(index).InstlHinbanHyoujiJun + addStartIndex) = sbiList(index).InstlHinban
                    InstlDataKbn(sbiList(index).InstlHinbanHyoujiJun + addStartIndex) = sbiList(index).InstlDataKbn
                    anEzSync.NotifyInstlDataKbn(sbiList(index).InstlHinbanHyoujiJun + addStartIndex)
                    BaseInstlFlg(sbiList(index).InstlHinbanHyoujiJun + addStartIndex) = sbiList(index).BaseInstlFlg
                    maeInstlHinbanHyoujiJun = sbiList(index).InstlHinbanHyoujiJun
                    anEzSync.NotifyInstlDataKbn(sbiList(index).InstlHinbanHyoujiJun + addStartIndex)
                Next

                If Not KaiteiNo = "" Then
                    Dim baseList As New List(Of TShisakuEventBaseVo)
                    baseList = selDao.FindByEventBase(shisakuEventCode)
                    For index As Integer = 0 To sbiList.Count - 1
                        For index2 As Integer = 0 To baseList.Count - 1
                            If baseList(index2).ShisakuGousya = sbiList(index).ShisakuGousya Then
                                InsuSuryo(baseList(index2).HyojijunNo, sbiList(index).InstlHinbanHyoujiJun + addStartIndex) = IIf(sbiList(index).InsuSuryo Is Nothing, "", sbiList(index).InsuSuryo)
                                Exit For
                            End If
                        Next
                    Next
                End If
                anEzSync.NotifyInstlHinbanGetKosei(shisakuEventCode, BlockNo, KaiteiNo, eventCopyFlg, baseFlg, addStartIndex)
                If eventCopyFlg Then
                    For Each index As Integer In GetInputInstlHinbanColumnIndexes()
                        If index >= addStartIndex Then
                            BaseInstlFlg(index) = "0"
                        End If
                    Next
                End If
                copyEventCode = ""
                SetChanged()
            End If

        End Sub

        Private backInstlSupplier As BuhinEditAlInstlSupplier

        ''' <summary>
        ''' バックアップを保持しておく
        ''' </summary>
        ''' <remarks></remarks>
        Public Sub backup()

            _EventCopyBeforeCnt = -1

            backInstlSupplier = instlSupplier.CloneCopy
            '構成のバックアップを取得する'
            anEzSync.SetBackUpKosei()
            '名前を画面のものと入れ替える'
            'For Each index As Integer In backInstlSupplier.GetInputInstlHinbanColumnIndexes
            '    backInstlSupplier.InstlHinban(index) = InstlHinban(index)
            '    backInstlSupplier.InstlHinbanKbn(index) = InstlHinbanKbn(index)
            'Next
            '現在のを入力'
            For Each index As Integer In instlSupplier.GetInputInstlHinbanColumnIndexes
                backInstlSupplier.InstlHinban(index) = InstlHinban(index)
                backInstlSupplier.InstlHinbanKbn(index) = InstlHinbanKbn(index)
                backInstlSupplier.InstlDataKbn(index) = InstlDataKbn(index)
                backInstlSupplier.BaseInstlFlg(index) = BaseInstlFlg(index)
                If _EventCopyBeforeCnt < index Then
                    _EventCopyBeforeCnt = index
                End If
            Next

        End Sub

        'イベントを元に戻す'
        Public Sub BackEvent()

            For Each colindex As Integer In backInstlSupplier.GetInputInstlHinbanColumnIndexes

                '2014/12/22 修正　構成情報のデータ区分が空白になってしまうので変更
                instlSupplier.InstlHinban(colindex) = backInstlSupplier.InstlHinban(colindex)
                instlSupplier.InstlHinbanKbn(colindex) = backInstlSupplier.InstlHinbanKbn(colindex)
                instlSupplier.InstlDataKbn(colindex) = backInstlSupplier.InstlDataKbn(colindex)
                instlSupplier.BaseInstlFlg(colindex) = backInstlSupplier.BaseInstlFlg(colindex)
                'InstlHinban(colindex) = backInstlSupplier.InstlHinban(colindex)
                'InstlHinbanKbn(colindex) = backInstlSupplier.InstlHinbanKbn(colindex)
                'InstlDataKbn(colindex) = backInstlSupplier.InstlDataKbn(colindex)
                'BaseInstlFlg(colindex) = backInstlSupplier.BaseInstlFlg(colindex)
                '↓↓2014/10/01 酒井 ADD BEGIN
                '↑↑2014/10/01 酒井 ADD END
                For Each rowindex As Integer In backInstlSupplier.GetInputRowIndexes
                    If Not backInstlSupplier.InsuSuryo(rowindex, colindex) Is Nothing Then
                        instlSupplier.InsuSuryo(rowindex, colindex) = backInstlSupplier.InsuSuryo(rowindex, colindex)
                    End If
                Next
                SetChanged()
                anEzSync.NotifyInstlHinban(colindex)
                anEzSync.NotifyInstlHinbanKbn(colindex)
                anEzSync.NotifyInstlDataKbn(colindex)
                anEzSync.NotifyBaseInstlFlg(colindex)
            Next
            'instlSupplier = backInstlSupplier
            SetChanged()
        End Sub

        'イベントを元に戻す'
        Public Sub BackEventKaiteiCopy()

            For Each colindex As Integer In backInstlSupplier.GetInputInstlHinbanColumnIndexes

                If _EventCopyBeforeCnt < colindex Then
                    Continue For
                End If

                InstlHinban(colindex) = backInstlSupplier.InstlHinban(colindex)
                InstlHinbanKbn(colindex) = backInstlSupplier.InstlHinbanKbn(colindex)
                '↓↓2014/10/01 酒井 ADD BEGIN
                InstlDataKbn(colindex) = backInstlSupplier.InstlDataKbn(colindex)
                BaseInstlFlg(colindex) = backInstlSupplier.BaseInstlFlg(colindex)
                '↑↑2014/10/01 酒井 ADD END
                For Each rowindex As Integer In backInstlSupplier.GetInputRowIndexes
                    If Not backInstlSupplier.InsuSuryo(rowindex, colindex) Is Nothing Then
                        InsuSuryo(rowindex, colindex) = backInstlSupplier.InsuSuryo(rowindex, colindex)
                    End If
                Next

            Next
            'instlSupplier = backInstlSupplier
            SetChanged()
        End Sub

        Public Sub test()
            anEzSync.BakEvent()
        End Sub



        ''' <summary>表示列情報</summary>
        Public Property ShowColumnBag() As BuhinEditAlShowColumnBag
            Get
                Return _showColumnBag
            End Get
            Set(ByVal value As BuhinEditAlShowColumnBag)
                If _showColumnBag Is value Then
                    Return
                End If
                _showColumnBag = value
                alEvent.ShowColumnInfos = value.SoubiVos
                alBasicOption.ShowColumnInfos = value.SoubiShiyouVos
                alSpecialOption.ShowColumnInfos = value.SoubiShiyouVos
                SetChanged()
            End Set
        End Property

        ''' <summary>
        ''' 登録する
        ''' </summary>
        ''' <remarks></remarks>
        Public Sub Register()
            '** 設計ブロック装備情報／設計ブロック装備仕様情報 **
            ' 削除
            DeleteSoubiByEventCode(_blockKeyVo.ShisakuEventCode, _
                                   _blockKeyVo.ShisakuBukaCode, _
                                   _blockKeyVo.ShisakuBlockNo, _
                                   _blockKeyVo.ShisakuBlockNoKaiteiNo)

            DeleteSoubiShiyouByEventCode(_blockKeyVo.ShisakuEventCode, _
                                   _blockKeyVo.ShisakuBukaCode, _
                                   _blockKeyVo.ShisakuBlockNo, _
                                   _blockKeyVo.ShisakuBlockNoKaiteiNo)

            ' 挿入
            InsertSoubis(_showColumnBag.SoubiVos)
            InsertSoubiShiyous(_showColumnBag.SoubiShiyouVos)

            memoSupplier.Update(login, aShisakuDate)
            instlSupplier.Update(login, aShisakuDate)
        End Sub

        ''' <summary>
        ''' 試作設計ブロック装備仕様情報を追加する
        ''' </summary>
        ''' <param name="insertValues">追加する情報</param>
        ''' <remarks></remarks>
        Private Sub InsertSoubiShiyous(ByVal insertValues As List(Of TShisakuSekkeiBlockSoubiShiyouVo))

            For Each vo As TShisakuSekkeiBlockSoubiShiyouVo In insertValues
                vo.ShisakuEventCode = _blockKeyVo.ShisakuEventCode
                vo.ShisakuBukaCode = _blockKeyVo.ShisakuBukaCode
                vo.ShisakuBlockNo = _blockKeyVo.ShisakuBlockNo
                vo.ShisakuBlockNoKaiteiNo = _blockKeyVo.ShisakuBlockNoKaiteiNo
                If StringUtil.IsEmpty(vo.CreatedUserId) Then
                    vo.CreatedUserId = login.UserId
                    vo.CreatedDate = aShisakuDate.CurrentDateDbFormat
                    vo.CreatedTime = aShisakuDate.CurrentTimeDbFormat
                End If
                vo.UpdatedUserId = login.UserId
                vo.UpdatedDate = aShisakuDate.CurrentDateDbFormat
                vo.UpdatedTime = aShisakuDate.CurrentTimeDbFormat

                soubiShiyoDao.InsertBy(vo)
            Next
        End Sub

        ''' <summary>
        ''' 試作設計ブロック装備情報を追加する
        ''' </summary>
        ''' <param name="insertValues">追加する情報</param>
        ''' <remarks></remarks>
        Private Sub InsertSoubis(ByVal insertValues As List(Of TShisakuSekkeiBlockSoubiVo))

            For Each vo As TShisakuSekkeiBlockSoubiVo In insertValues
                vo.ShisakuEventCode = _blockKeyVo.ShisakuEventCode
                vo.ShisakuBukaCode = _blockKeyVo.ShisakuBukaCode
                vo.ShisakuBlockNo = _blockKeyVo.ShisakuBlockNo
                vo.ShisakuBlockNoKaiteiNo = _blockKeyVo.ShisakuBlockNoKaiteiNo
                If StringUtil.IsEmpty(vo.CreatedUserId) Then
                    vo.CreatedUserId = login.UserId
                    vo.CreatedDate = aShisakuDate.CurrentDateDbFormat
                    vo.CreatedTime = aShisakuDate.CurrentTimeDbFormat
                End If
                vo.UpdatedUserId = login.UserId
                vo.UpdatedDate = aShisakuDate.CurrentDateDbFormat
                vo.UpdatedTime = aShisakuDate.CurrentTimeDbFormat

                soubiDao.InsertBy(vo)
            Next
        End Sub

        ''' <summary>
        ''' 試作設計ブロック装備仕様情報を削除する
        ''' </summary>
        ''' <param name="shisakuEventCode">試作イベントコード</param>
        ''' <remarks></remarks>
        Private Sub DeleteSoubiShiyouByEventCode(ByVal shisakuEventCode As String, _
                                           ByVal ShisakuBukaCode As String, _
                                           ByVal ShisakuBlockNo As String, _
                                           ByVal ShisakuBlockNoKaiteiNo As String)

            Dim param2 As New TShisakuSekkeiBlockSoubiShiyouVo
            '以下のキーで削除する。
            param2.ShisakuEventCode = shisakuEventCode
            param2.ShisakuBukaCode = ShisakuBukaCode
            param2.ShisakuBlockNo = ShisakuBlockNo
            param2.ShisakuBlockNoKaiteiNo = ShisakuBlockNoKaiteiNo

            soubiShiyoDao.DeleteBy(param2)
        End Sub



        ''' <summary>
        ''' 試作設計ブロック装備情報を削除する
        ''' </summary>
        ''' <param name="shisakuEventCode">試作イベントコード</param>
        ''' <remarks></remarks>
        Private Sub DeleteSoubiByEventCode(ByVal shisakuEventCode As String, _
                                           ByVal ShisakuBukaCode As String, _
                                           ByVal ShisakuBlockNo As String, _
                                           ByVal ShisakuBlockNoKaiteiNo As String)

            Dim param As New TShisakuSekkeiBlockSoubiVo
            '以下のキーで削除する。
            param.ShisakuEventCode = shisakuEventCode
            param.ShisakuBukaCode = ShisakuBukaCode
            param.ShisakuBlockNo = ShisakuBlockNo
            param.ShisakuBlockNoKaiteiNo = ShisakuBlockNoKaiteiNo

            soubiDao.DeleteBy(param)
        End Sub

        ''' <summary>
        ''' 入力行の行Noの一覧を返す
        ''' </summary>
        ''' <returns>入力行の行Noの一覧</returns>
        ''' <remarks></remarks>
        Public Function GetInputRowIndexes() As ICollection(Of Integer)
            Return alEvent.GetInputRowNos
        End Function

        ''' <summary>
        ''' 入力したメモタイトルの列No一覧を返す
        ''' </summary>
        ''' <returns>入力したメモタイトルの列No一覧</returns>
        ''' <remarks></remarks>
        Public Function GetInputMemoTitleColumnIndexes() As ICollection(Of Integer)
            Return memoSupplier.GetInputTitleColumnIndexes
        End Function

        ''' <summary>
        ''' 入力した適用の列No一覧を返す
        ''' </summary>
        ''' <param name="rowIndex">行index</param>
        ''' <returns>入力した適用の列No一覧</returns>
        ''' <remarks></remarks>
        Public Function GetInputMemoTekiyouColumnIndexes(ByVal rowIndex As Integer) As ICollection(Of Integer)
            Return memoSupplier.GetInputTekiyouColumnIndexes(rowIndex)
        End Function

        ''' <summary>
        ''' 入力した列タイトルの列No一覧を返す
        ''' </summary>
        ''' <returns>入力した列タイトルの列No一覧</returns>
        ''' <remarks></remarks>
        Public Function GetInputInstlHinbanColumnIndexes() As ICollection(Of Integer)
            Return instlSupplier.GetInputInstlHinbanColumnIndexes
        End Function

        ''' <summary>
        ''' 入力した列の列No一覧を返す
        ''' </summary>
        ''' <param name="rowIndex">行No</param>
        ''' <returns>入力した列の列No一覧</returns>
        ''' <remarks></remarks>
        Public Function GetInputInsuColumnIndexes(ByVal rowIndex As Integer) As ICollection(Of Integer)
            Return instlSupplier.GetInputColumnIndexes(rowIndex)
        End Function

#Region "公開プロパティ"

        ' イベントの更新日
        Private _EventUpdatedDate As String
        ' イベントの更新時間
        Private _EventUpdatedTime As String
        ' 部品表の最終更新日
        Private _SaisyuKoushinbi As String
        ' 部品表の最終更新時間
        Private _SaisyuKoushinjikan As String
        'ブロックNo
        '↓↓2014/10/09 酒井 ADD BEGIN
        'Private _blockNo As String
        Public _blockNo As String
        '↑↑2014/10/09 酒井 ADD END

        '工事指令Noの更新でイベントの更新日が更新されてしまい
        '部品表編集画面で「イベントが更新されました」
        'メッセージが出てしまうのを防ぐ為、イベントの更新日とは
        'は別の登録日で判定する
        ' イベントの登録日
        Private _EventRegisterDate As String
        ' イベントの登録時間
        Private _EventRegisterTime As String

        'イベントコード
        '↓↓2014/10/09 酒井 ADD BEGIN
        'Private _ShisakuEventCode As String
        Public _ShisakuEventCode As String
        '↑↑2014/10/09 酒井 ADD END

        ''' <summary>イベントの更新日</summary>
        ''' <value>イベントの更新日</value>
        ''' <returns>イベントの更新日</returns>
        Public Property EventUpdatedDate() As String
            Get
                Return _EventUpdatedDate
            End Get
            Set(ByVal value As String)
                _EventUpdatedDate = value
            End Set
        End Property

        ''' <summary>イベントの更新時間</summary>
        ''' <value>イベントの更新時間</value>
        ''' <returns>イベントの更新時間</returns>
        Public Property EventUpdatedTime() As String
            Get
                Return _EventUpdatedTime
            End Get
            Set(ByVal value As String)
                _EventUpdatedTime = value
            End Set
        End Property
        ''' <summary>イベントの登録日</summary>
        ''' <value>イベントの登録日</value>
        ''' <returns>イベントの登録日</returns>
        Public Property EventRegisterDate() As String
            Get
                Return _EventRegisterDate
            End Get
            Set(ByVal value As String)
                _EventRegisterDate = value
            End Set
        End Property
        ''' <summary>イベントの登録時間</summary>
        ''' <value>イベントの登録時間</value>
        ''' <returns>イベントの登録時間</returns>
        Public Property EventRegisterTime() As String
            Get
                Return _EventRegisterTime
            End Get
            Set(ByVal value As String)
                _EventRegisterTime = value
            End Set
        End Property


        ''' <summary>部品表の最終更新日</summary>
        ''' <value>部品表の最終更新日</value>
        ''' <returns>部品表の最終更新日</returns>
        Public Property SaisyuKoushinbi() As String
            Get
                Return _SaisyuKoushinbi
            End Get
            Set(ByVal value As String)
                _SaisyuKoushinbi = value
            End Set
        End Property

        ''' <summary>部品表の最終更新時間</summary>
        ''' <value>部品表の最終更新時間</value>
        ''' <returns>部品表の最終更新時間</returns>
        Public Property SaisyuKoushinjikan() As String
            Get
                Return _SaisyuKoushinjikan
            End Get
            Set(ByVal value As String)
                _SaisyuKoushinjikan = value
            End Set
        End Property

        ''' <summary>イベントコード</summary>
        ''' <value>イベントコード</value>
        ''' <returns>イベントコード</returns>
        Public Property shisakuEventCode() As String
            Get
                Return _ShisakuEventCode
            End Get
            Set(ByVal value As String)
                _ShisakuEventCode = value
            End Set
        End Property

        ''' <summary>号車</summary>
        ''' <param name="rowNo">行No</param>
        Public ReadOnly Property ShisakuGosha(ByVal rowNo As Integer) As String
            Get
                Return alEvent.ShisakuGosha(rowNo)
            End Get
        End Property
        ''' 2012-01-11 
        ''' <summary>試作種別</summary>
        ''' <param name="rowNo">行No</param>
        Public ReadOnly Property ShisakuSyubetu(ByVal rowNo As Integer) As String
            Get
                Return alEvent.ShisakuSyubetu(rowNo)
            End Get
        End Property

        ''' <summary>動的列（イベント情報・装備品）の列数</summary>
        Public ReadOnly Property DynamicColumnCount() As Integer
            Get
                Return alEvent.ColumnCount + alBasicOption.ColumnCount + alSpecialOption.ColumnCount
            End Get
        End Property

        '-------------------------------------------------------------------------------------------------------
        '２次改修
        '   完成品の場合カラムIDを返す。装備仕様の場合区分を返す用
        ''' <summary>
        ''' 動的列（イベント情報・装備品）の内容
        ''' </summary>
        ''' <param name="rowNo">行No</param>
        ''' <param name="columnIndex">列index</param>
        ''' <returns>内容</returns>
        ''' <remarks></remarks>
        Public ReadOnly Property DynamicInfoColumnId(ByVal rowNo As Integer, ByVal columnIndex As Integer) As String
            Get
                If columnIndex < alEvent.ColumnCount Then
                    Return alEvent.InfoColumnId(rowNo, columnIndex)
                ElseIf columnIndex < alEvent.ColumnCount + alBasicOption.ColumnCount Then
                    Return "1" '基本仕様
                Else
                    Return "2"  '特別仕様
                End If
            End Get
        End Property
        '-------------------------------------------------------------------------------------------------------
        '２次改修
        '   装備仕様の場合カラム位置を返す用
        ''' <summary>
        ''' 動的列（イベント情報・装備品）の内容
        ''' </summary>
        ''' <param name="rowNo">行No</param>
        ''' <param name="columnIndex">列index</param>
        ''' <returns>内容</returns>
        ''' <remarks></remarks>
        Public ReadOnly Property DynamicInfoColumnIchi(ByVal rowNo As Integer, ByVal columnIndex As Integer) As Integer
            Get
                If columnIndex < alEvent.ColumnCount + alBasicOption.ColumnCount Then
                    Return alBasicOption.SoubiColumnInfo(rowNo, columnIndex - alEvent.ColumnCount) '基本仕様
                Else
                    Return alSpecialOption.SoubiColumnInfo(rowNo, columnIndex - alEvent.ColumnCount - alBasicOption.ColumnCount)  '特別仕様
                End If
            End Get
        End Property
        '-------------------------------------------------------------------------------------------------------

        ''' <summary>
        ''' 動的列（イベント情報・装備品）の内容
        ''' </summary>
        ''' <param name="rowNo">行No</param>
        ''' <param name="columnIndex">列index</param>
        ''' <returns>内容</returns>
        ''' <remarks></remarks>
        Public ReadOnly Property DynamicInfo(ByVal rowNo As Integer, ByVal columnIndex As Integer) As String
            Get
                If columnIndex < alEvent.ColumnCount Then
                    Return alEvent.Info(rowNo, columnIndex)
                ElseIf columnIndex < alEvent.ColumnCount + alBasicOption.ColumnCount Then
                    Return alBasicOption.Info(rowNo, columnIndex - alEvent.ColumnCount)
                Else
                    Return alSpecialOption.Info(rowNo, columnIndex - alEvent.ColumnCount - alBasicOption.ColumnCount)
                End If
            End Get
        End Property

        ''' <summary>メモ列の列数</summary>
        Public Property MemoColumnCount() As Integer
            Get
                Return memoSupplier.ColumnCount
            End Get
            Set(ByVal value As Integer)
                memoSupplier.ColumnCount = value
            End Set
        End Property

        ''' <summary>適用</summary>
        ''' <param name="rowIndex">行index</param>
        ''' <param name="columnIndex">列index</param>
        Public Property MemoTekiyou(ByVal rowIndex As Integer, ByVal columnIndex As Integer) As String
            Get
                Return memoSupplier.Tekiyou(rowIndex, columnIndex)
            End Get
            Set(ByVal value As String)
                memoSupplier.Tekiyou(rowIndex, columnIndex) = value
            End Set
        End Property

        ''' <summary>メモ</summary>
        ''' <param name="columnIndex">列index</param>
        Public Property MemoTitle(ByVal columnIndex As Integer) As String
            Get
                Return memoSupplier.MemoTitle(columnIndex)
            End Get
            Set(ByVal value As String)
                memoSupplier.MemoTitle(columnIndex) = value
                SetChanged()
            End Set
        End Property


        ''' <summary>員数</summary>
        ''' <param name="rowIndex">行No</param>
        ''' <param name="columnIndex">列No</param>
        Public Property InsuSuryo(ByVal rowIndex As Integer, ByVal columnIndex As Integer) As String
            Get
                Return BuhinEditInsu.ConvDbToForm(instlSupplier.InsuSuryo(rowIndex, columnIndex))
            End Get
            Set(ByVal val As String)
                Dim value As Integer? = BuhinEditInsu.ConvFormToDb(val)
                If EzUtil.IsEqualIfNull(instlSupplier.InsuSuryo(rowIndex, columnIndex), value) Then
                    Return
                End If
                instlSupplier.InsuSuryo(rowIndex, columnIndex) = value
                SetChanged()
            End Set
        End Property
        ''' <summary>INSTL品番</summary>
        ''' <param name="columnIndex">列No</param>
        Public Property InstlHinban(ByVal columnIndex As Integer) As String
            Get
                Return instlSupplier.InstlHinban(columnIndex)
            End Get
            Set(ByVal value As String)
                If EzUtil.IsEqualIfNull(instlSupplier.InstlHinban(columnIndex), value) Then
                    Return
                End If
                instlSupplier.InstlHinban(columnIndex) = value
                SetChanged()
                anEzSync.NotifyInstlHinban(columnIndex)
            End Set
        End Property

        ''' <summary>INSTL品番の列数</summary>
        Public ReadOnly Property InstlHinbanCount() As Integer
            Get
                Dim result As Integer = 0
                For Each i As Integer In GetInputInstlHinbanColumnIndexes()
                    result += 1
                Next
                Return result
            End Get
        End Property

        ''' <summary>INSTL品番区分</summary>
        ''' <param name="columnIndex">列No</param>
        Public Property InstlHinbanKbn(ByVal columnIndex As Integer) As String
            Get
                Return instlSupplier.InstlHinbanKbn(columnIndex)
            End Get
            Set(ByVal value As String)
                If EzUtil.IsEqualIfNull(instlSupplier.InstlHinbanKbn(columnIndex), value) Then
                    Return
                End If
                instlSupplier.InstlHinbanKbn(columnIndex) = value
                SetChanged()
                anEzSync.NotifyInstlHinbanKbn(columnIndex)
            End Set
        End Property

        '↓↓2014/07/25 Ⅰ.3.設計編集 ベース改修専用化_t) (TES)張 ADD BEGIN
        Public Property BaseInstlFlg(ByVal columnIndex As Integer) As String
            Get
                Return instlSupplier.BaseInstlFlg(columnIndex)
            End Get
            Set(ByVal value As String)
                If EzUtil.IsEqualIfNull(instlSupplier.BaseInstlFlg(columnIndex), value) Then
                    Return
                End If
                instlSupplier.BaseInstlFlg(columnIndex) = value
                SetChanged()
                anEzSync.NotifyBaseInstlFlg(columnIndex)
            End Set
        End Property
        ''↑↑2014/07/25 Ⅰ.3.設計編集 ベース改修専用化_t) (TES)張 ADD END

        ''' <summary>INSTLデータ区分</summary>
        ''' <param name="columnIndex">列No</param>
        Public Property InstlDataKbn(ByVal columnIndex As Integer) As String
            Get
                Return instlSupplier.InstlDataKbn(columnIndex)
            End Get
            Set(ByVal value As String)
                If EzUtil.IsEqualIfNull(instlSupplier.InstlDataKbn(columnIndex), value) Then
                    Return
                End If
                instlSupplier.InstlDataKbn(columnIndex) = value
                SetChanged()

            End Set
        End Property

        ''' <summary>構成の情報</summary>
        ''' <param name="columnIndex">列No</param>
        Public ReadOnly Property StructureResult(ByVal columnIndex As Integer) As StructureResult
            Get
                Return instlSupplier.StructureResult(columnIndex)
            End Get
        End Property

        ''' <summary>イベント情報列の列数</summary>
        Public ReadOnly Property EventColumnCount() As Integer
            Get
                Return alEvent.ColumnCount
            End Get
        End Property

        ''' <summary>イベント情報のタイトル</summary>
        ''' <param name="columnIndex">列No</param>
        Public ReadOnly Property EventTitle(ByVal columnIndex As Integer) As String
            Get
                Return alEvent.Title(columnIndex)
            End Get
        End Property

        ''' <summary>
        ''' イベント情報の内容
        ''' </summary>
        ''' <param name="rowNo">行No</param>
        ''' <param name="columnIndex">列index</param>
        ''' <returns>内容</returns>
        ''' <remarks></remarks>
        Public ReadOnly Property EventInfo(ByVal rowNo As Integer, ByVal columnIndex As Integer) As String
            Get
                Return alEvent.Info(rowNo, columnIndex)
            End Get
        End Property

        ''' <summary>
        ''' ベース車情報の列数を返す
        ''' </summary>
        ''' <returns>ベース車情報の列数</returns>
        ''' <remarks></remarks>
        Public Function GetEventColumnCountBase() As Integer
            Return alEvent.GetColumnCountBase
        End Function

        ''' <summary>
        ''' 設計展開ベース車情報の列数を返す
        ''' </summary>
        ''' <returns>設計展開ベース車情報の列数</returns>
        ''' <remarks></remarks>
        Public Function GetEventColumnCountBaseTenkai() As Integer
            Return alEvent.GetColumnCountBaseTenkai
        End Function

        ''' <summary>装備品情報の列数</summary>
        Public ReadOnly Property OptionColumnCount() As Integer
            Get
                Return alBasicOption.ColumnCount + alSpecialOption.ColumnCount
            End Get
        End Property

        ''' <summary>基本装備仕様の列数</summary>
        Public ReadOnly Property BasicOptionColumnCount() As Integer
            Get
                Return alBasicOption.ColumnCount
            End Get
        End Property

        ''' <summary>特別装備仕様の列数</summary>
        Public ReadOnly Property SpecialOptionColumnCount() As Integer
            Get
                Return alSpecialOption.ColumnCount
            End Get
        End Property

        ''' <summary>装備品情報のタイトル</summary>
        ''' <param name="columnIndex">列No</param>
        Public ReadOnly Property OptionTitle(ByVal columnIndex As Integer) As String
            Get
                If columnIndex < alBasicOption.ColumnCount Then
                    Return alBasicOption.Title(columnIndex)
                Else
                    Return alSpecialOption.Title(columnIndex - alBasicOption.ColumnCount)
                End If
            End Get
        End Property

        ''' <summary>装備品情報のタイトル（大区分）</summary>
        ''' <param name="columnIndex">列No</param>
        Public ReadOnly Property OptionTitleDai(ByVal columnIndex As Integer) As String
            Get
                If columnIndex < alBasicOption.ColumnCount Then
                    Return alBasicOption.TitleDai(columnIndex)
                Else
                    Return alSpecialOption.TitleDai(columnIndex - alBasicOption.ColumnCount)
                End If
            End Get
        End Property

        ''' <summary>装備品情報のタイトル（中区分）</summary>
        ''' <param name="columnIndex">列No</param>
        Public ReadOnly Property OptionTitleChu(ByVal columnIndex As Integer) As String
            Get
                If columnIndex < alBasicOption.ColumnCount Then
                    Return alBasicOption.TitleChu(columnIndex)
                Else
                    Return alSpecialOption.TitleChu(columnIndex - alBasicOption.ColumnCount)
                End If
            End Get
        End Property

        ''' <summary>
        ''' 装備品情報の内容
        ''' </summary>
        ''' <param name="rowNo">行No</param>
        ''' <param name="columnIndex">列index</param>
        ''' <returns>内容</returns>
        ''' <remarks></remarks>
        Public ReadOnly Property OptionInfo(ByVal rowNo As Integer, ByVal columnIndex As Integer) As String
            Get
                If columnIndex < alBasicOption.ColumnCount Then
                    Return alBasicOption.Info(rowNo, columnIndex)
                Else
                    Return alSpecialOption.Info(rowNo, columnIndex - alBasicOption.ColumnCount)
                End If
            End Get
        End Property

        ''' <summary>
        ''' ブロックNo
        ''' </summary>
        ''' <value>ブロックNo</value>
        ''' <returns>ブロックNo</returns>
        ''' <remarks></remarks>
        Public Property BlockNo() As String
            Get
                Return _blockNo
            End Get
            Set(ByVal value As String)
                _blockNo = value
            End Set
        End Property



#End Region

        Private Class SoubiComparerable : Implements IComparer(Of TShisakuSekkeiBlockSoubiVo)
            Public Function Compare(ByVal x As ShisakuCommon.Db.EBom.Vo.TShisakuSekkeiBlockSoubiVo, ByVal y As ShisakuCommon.Db.EBom.Vo.TShisakuSekkeiBlockSoubiVo) As Integer Implements System.Collections.Generic.IComparer(Of ShisakuCommon.Db.EBom.Vo.TShisakuSekkeiBlockSoubiVo).Compare
                Return x.ShisakuSoubiHyoujiJun.CompareTo(y.ShisakuSoubiHyoujiJun)
            End Function
        End Class

        Private Function FindSoubi(ByVal blockVo As TShisakuSekkeiBlockVo) As List(Of TShisakuSekkeiBlockSoubiVo)
            Dim param As New TShisakuSekkeiBlockSoubiVo
            param.ShisakuEventCode = blockVo.ShisakuEventCode
            param.ShisakuBukaCode = blockVo.ShisakuBukaCode
            param.ShisakuBlockNo = blockVo.ShisakuBlockNo
            param.ShisakuBlockNoKaiteiNo = blockVo.ShisakuBlockNoKaiteiNo
            Dim results As List(Of TShisakuSekkeiBlockSoubiVo) = soubiDao.FindBy(param)
            If results.Count = 0 Then
                Return results
            End If
            results.Sort(New SoubiComparerable)
            Return results
        End Function

        Private Class SoubiShiyoComparerable : Implements IComparer(Of TShisakuSekkeiBlockSoubiShiyouVo)
            Public Function Compare(ByVal x As ShisakuCommon.Db.EBom.Vo.TShisakuSekkeiBlockSoubiShiyouVo, ByVal y As ShisakuCommon.Db.EBom.Vo.TShisakuSekkeiBlockSoubiShiyouVo) As Integer Implements System.Collections.Generic.IComparer(Of ShisakuCommon.Db.EBom.Vo.TShisakuSekkeiBlockSoubiShiyouVo).Compare
                If x.ShisakuSoubiKbn Is Nothing OrElse y.ShisakuSoubiKbn Is Nothing Then
                    Throw New InvalidOperationException("試作装備表示順は、NotNull項目なのに、Nullを検知した")
                End If
                If x.ShisakuSoubiHyoujiJun Is Nothing OrElse y.ShisakuSoubiHyoujiJun Is Nothing Then
                    Throw New InvalidOperationException("試作装備表示順は、NotNull項目なのに、Nullを検知した")
                End If
                Dim result As Integer = x.ShisakuSoubiKbn.CompareTo(y.ShisakuSoubiKbn)
                If result <> 0 Then
                    Return result
                End If
                Return CInt(x.ShisakuSoubiHyoujiJun).CompareTo(CInt(y.ShisakuSoubiHyoujiJun))
            End Function
        End Class

        Private Function FindSoubiShiyo(ByVal blockVo As TShisakuSekkeiBlockVo) As List(Of TShisakuSekkeiBlockSoubiShiyouVo)
            Dim param As New TShisakuSekkeiBlockSoubiShiyouVo
            param.ShisakuEventCode = blockVo.ShisakuEventCode
            param.ShisakuBukaCode = blockVo.ShisakuBukaCode
            param.ShisakuBlockNo = blockVo.ShisakuBlockNo
            param.ShisakuBlockNoKaiteiNo = blockVo.ShisakuBlockNoKaiteiNo
            Dim results As List(Of TShisakuSekkeiBlockSoubiShiyouVo) = soubiShiyoDao.FindBy(param)
            If results.Count = 0 Then
                Return results
            End If
            results.Sort(New SoubiShiyoComparerable)
            Return results
        End Function

        ''↓↓2014/07/25 Ⅰ.4.特別織込みアラート追加_b) (TES)張 ADD BEGIN
        Private Function FindSoubiShiyoAlart(ByVal blockVo As TShisakuSekkeiBlockVo) As List(Of TShisakuSekkeiBlockSoubiShiyouVo)
            Dim param As String
            param = blockVo.ShisakuBlockNo
            Dim results As List(Of TShisakuSekkeiBlockSoubiShiyouVo) = FindByBlockNo(param)
            If results.Count = 0 Then
                Return results
            End If
            results.Sort(New SoubiShiyoComparerable)
            Return results
        End Function

        ''' <summary>
        ''' ブロックNoの情報を取得する
        ''' </summary>
        ''' <param name="shisakuBlockNo">ブロックNo</param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Private Function FindByBlockNo(ByVal shisakuBlockNo As String) As List(Of TShisakuSekkeiBlockSoubiShiyouVo)
            '20140820 Sakai Add
            'Dim sql As String = _
            '" SELECT" _
            '& " EVENT.SHISAKU_SOUBI_HYOUJI_NO, " _
            '& " EVENT.SHISAKU_SOUBI_KBN, " _
            '& " EVENT.SHISAKU_RETU_KOUMOKU_CODE " _
            '& " FROM  " _
            '& MBOM_DB_NAME & ".dbo.T_SHISAKU_EVENT_SOUBI AS EVENT  " _
            '& " INNER JOIN " _
            '& MBOM_DB_NAME & ".dbo.M_SHISAKU_TOKUBETU_ORIKOMI_ALART AS ALART " _
            '& " ON " _
            '& " EVENT.SHISAKU_TEKIYOU_DAI = ALART.DAI_KBN_NAME " _
            '& " AND " _
            '& " EVENT.SHISAKU_TEKIYOU_CHU = ALART.CHU_KBN_NAME " _
            '& " AND " _
            '& " EVENT.SHISAKU_TEKIYOU = ALART.SHO_KBN_NAME " _
            '& " WHERE " _
            '& " ALART.BLOCK_NO = @ShisakuBlockNo "

            ''↓↓2014/09/22 Ⅰ.4.特別織込みアラート追加_b) 酒井 ADD BEGIN
            'Dim sql As String = _
            '" SELECT DISTINCT" _
            '& " EVENT.SHISAKU_SOUBI_HYOUJI_NO, " _
            '& " EVENT.SHISAKU_SOUBI_KBN, " _
            '& " EVENT.SHISAKU_RETU_KOUMOKU_CODE " _
            '& " FROM  " _
            '& MBOM_DB_NAME & ".dbo.T_SHISAKU_EVENT_SOUBI AS EVENT " _
            '& " INNER JOIN " _
            '& MBOM_DB_NAME & ".dbo.M_SHISAKU_SOUBI AS M " _
            '& " ON EVENT.SHISAKU_SOUBI_KBN = M.SHISAKU_SOUBI_KBN " _
            '& " AND EVENT.SHISAKU_RETU_KOUMOKU_CODE = M.SHISAKU_RETU_KOUMOKU_CODE " _
            '& " INNER JOIN " _
            '& MBOM_DB_NAME & ".dbo.M_SHISAKU_TOKUBETU_ORIKOMI_ALART AS ALART " _
            '& " ON M.SHISAKU_RETU_KOUMOKU_NAME_DAI = ALART.DAI_KBN_NAME " _
            '& " AND M.SHISAKU_RETU_KOUMOKU_NAME_CHU = ALART.CHU_KBN_NAME " _
            '& " AND M.SHISAKU_RETU_KOUMOKU_NAME = ALART.SHO_KBN_NAME" _
            '& " WHERE EVENT.SHISAKU_EVENT_CODE = @ShisakuEventCode"
            'Dim sql As String = _
            '" SELECT DISTINCT" _
            '& " EVENT.SHISAKU_SOUBI_HYOUJI_NO AS SHISAKU_SOUBI_HYOUJI_JUN, " _
            '& " EVENT.SHISAKU_SOUBI_KBN, " _
            '& " EVENT.SHISAKU_RETU_KOUMOKU_CODE " _
            '& " FROM  " _
            '& MBOM_DB_NAME & ".dbo.T_SHISAKU_EVENT_SOUBI AS EVENT " _
            '& " INNER JOIN " _
            '& MBOM_DB_NAME & ".dbo.M_SHISAKU_SOUBI AS M " _
            '& " ON EVENT.SHISAKU_SOUBI_KBN = M.SHISAKU_SOUBI_KBN " _
            '& " AND EVENT.SHISAKU_RETU_KOUMOKU_CODE = M.SHISAKU_RETU_KOUMOKU_CODE " _
            '& " INNER JOIN " _
            '& MBOM_DB_NAME & ".dbo.M_SHISAKU_TOKUBETU_ORIKOMI_ALART AS ALART " _
            '& " ON M.SHISAKU_RETU_KOUMOKU_NAME_DAI = ALART.DAI_KBN_NAME " _
            '& " AND M.SHISAKU_RETU_KOUMOKU_NAME_CHU = ALART.CHU_KBN_NAME " _
            '& " AND M.SHISAKU_RETU_KOUMOKU_NAME = ALART.SHO_KBN_NAME" _
            '& " WHERE EVENT.SHISAKU_EVENT_CODE = @ShisakuEventCode" _
            '& " AND ALART.BLOCK_NO = @ShisakuBlockNo " 
            ''↑↑2014/09/22 Ⅰ.4.特別織込みアラート追加_b) 酒井 ADD END

            '' yaginuma Ⅰ.8.号車別仕様書 作成機能 ファンクション定義.xlsのh)のＳＱＬは間違い？
            'Dim sql As String = _
            '" SELECT" _
            '& " EVENT.SHISAKU_SOUBI_HYOUJI_NO, " _
            '& " EVENT.SHISAKU_SOUBI_KBN, " _
            '& " EVENT.SHISAKU_RETU_KOUMOKU_CODE " _
            '& " FROM  " _
            '& MBOM_DB_NAME & ".dbo.T_SHISAKU_EVENT_SOUBI AS EVENT  " _
            '& " INNER JOIN " _
            '& MBOM_DB_NAME & ".dbo.M_SHISAKU_TOKUBETU_ORIKOMI_ALART AS ALART " _
            '& " ON " _
            '& " EVENT.SHISAKU_SOUBI_KBN = ALART.SHISAKU_SOUBI_KBN " _
            '& " AND " _
            '& " EVENT.SHISAKU_TEKIYOU_DAI = ALART.DAI_KBN_NAME " _
            '& " AND " _
            '& " EVENT.SHISAKU_TEKIYOU_CHU = ALART.CHU_KBN_NAME " _
            '& " AND " _
            '& " EVENT.SHISAKU_TEKIYOU = ALART.SHO_KBN_NAME " _
            '& " WHERE " _
            '& " ALART.BLOCK_NO = @ShisakuBlockNo "
            Dim sql As New System.Text.StringBuilder
            With sql
                .AppendLine("SELECT DISTINCT")
                .AppendLine(" T.SHISAKU_SOUBI_HYOUJI_JUN, ")
                .AppendLine(" T.SHISAKU_SOUBI_KBN, ")
                .AppendLine(" T.SHISAKU_RETU_KOUMOKU_CODE ")
                .AppendLine(" FROM (")
                .AppendLine(" SELECT DISTINCT")
                .AppendLine(" EVENT.SHISAKU_SOUBI_HYOUJI_NO AS SHISAKU_SOUBI_HYOUJI_JUN, ")
                .AppendLine(" EVENT.SHISAKU_SOUBI_KBN, ")
                .AppendLine(" EVENT.SHISAKU_RETU_KOUMOKU_CODE ")
                .AppendLine(" FROM ")
                .AppendLine(MBOM_DB_NAME & ".dbo.T_SHISAKU_EVENT_SOUBI AS EVENT ")
                .AppendLine(" INNER JOIN ")
                .AppendLine(MBOM_DB_NAME & ".dbo.M_SHISAKU_SOUBI AS M ")
                .AppendLine(" ON EVENT.SHISAKU_SOUBI_KBN = M.SHISAKU_SOUBI_KBN ")
                .AppendLine(" AND EVENT.SHISAKU_RETU_KOUMOKU_CODE = M.SHISAKU_RETU_KOUMOKU_CODE ")
                .AppendLine(" INNER JOIN ")
                .AppendLine(MBOM_DB_NAME & ".dbo.M_SHISAKU_TOKUBETU_ORIKOMI_ALART AS ALART ")
                .AppendLine(" ON M.SHISAKU_RETU_KOUMOKU_NAME_DAI = ALART.DAI_KBN_NAME ")
                .AppendLine(" AND M.SHISAKU_RETU_KOUMOKU_NAME_CHU = ALART.CHU_KBN_NAME ")
                .AppendLine(" AND M.SHISAKU_RETU_KOUMOKU_NAME = ALART.SHO_KBN_NAME")
                .AppendLine(" WHERE EVENT.SHISAKU_EVENT_CODE = @ShisakuEventCode")
                .AppendLine(" AND ALART.BLOCK_NO = @ShisakuBlockNo ")
                .AppendLine(" UNION ")
                .AppendLine(" SELECT DISTINCT")
                .AppendLine(" EVENT.SHISAKU_SOUBI_HYOUJI_NO AS SHISAKU_SOUBI_HYOUJI_JUN, ")
                .AppendLine(" EVENT.SHISAKU_SOUBI_KBN, ")
                .AppendLine(" EVENT.SHISAKU_RETU_KOUMOKU_CODE ")
                .AppendLine(" FROM ")
                .AppendLine(MBOM_DB_NAME & ".dbo.T_SHISAKU_EVENT_SOUBI AS EVENT ")
                .AppendLine(" INNER JOIN ")
                .AppendLine(MBOM_DB_NAME & ".dbo.M_SHISAKU_SOUBI AS M ")
                .AppendLine(" ON EVENT.SHISAKU_SOUBI_KBN = M.SHISAKU_SOUBI_KBN ")
                .AppendLine(" AND EVENT.SHISAKU_RETU_KOUMOKU_CODE = M.SHISAKU_RETU_KOUMOKU_CODE ")
                .AppendLine(" INNER JOIN ")
                .AppendLine(MBOM_DB_NAME & ".dbo.M_SHISAKU_TOKUBETU_ORIKOMI_ALART AS ALART ")
                .AppendLine(" ON M.SHISAKU_RETU_KOUMOKU_NAME_DAI = 'W ' + ALART.DAI_KBN_NAME ")
                .AppendLine(" AND M.SHISAKU_RETU_KOUMOKU_NAME_CHU = ALART.CHU_KBN_NAME ")
                .AppendLine(" AND M.SHISAKU_RETU_KOUMOKU_NAME = ALART.SHO_KBN_NAME")
                .AppendLine(" WHERE EVENT.SHISAKU_EVENT_CODE = @ShisakuEventCode")
                .AppendLine(" AND ALART.BLOCK_NO = @ShisakuBlockNo ")
                .AppendLine(") AS T")

            End With




            Dim db As New EBomDbClient
            Dim param As New TShisakuSekkeiBlockSoubiShiyouVo

            param.ShisakuBlockNo = shisakuBlockNo
            '20140820 Sakai Add
            param.ShisakuEventCode = shisakuEventCode

            Return db.QueryForList(Of TShisakuSekkeiBlockSoubiShiyouVo)(sql.ToString, param)
        End Function
        ''↑↑2014/07/25 Ⅰ.4.特別織込みアラート追加_b) (TES)張 ADD END

        ''' <summary>
        ''' 構成の情報を探索する
        ''' </summary>
        ''' <param name="columnIndex"></param>
        ''' <remarks></remarks>
        Public Sub DetectStructureResult(ByVal columnIndex As Integer)
            If StringUtil.IsEmpty(copyEventCode) Then
                instlSupplier.DetectStructureResult(columnIndex)
            Else
                instlSupplier.DetectStructureResult(columnIndex, copyEventCode)
            End If
        End Sub

        ''' <summary>
        ''' INSTL品番列に列挿入する
        ''' </summary>
        ''' <param name="columnIndex">INSTL品番の中の列index</param>
        ''' <param name="insertCount">挿入列数</param>
        ''' <remarks></remarks>
        Public Sub InsertColumnInInstl(ByVal columnIndex As Integer, ByVal insertCount As Integer)
            instlSupplier.InsertColumn(columnIndex, insertCount)
            anEzSync.InsertColumnInInstl(columnIndex, insertCount)
            SetChanged()
        End Sub

        ''' <summary>
        ''' INSTL品番列を列削除する
        ''' </summary>
        ''' <param name="columnIndex">INSTL品番の中の列index</param>
        ''' <param name="removeCount">削除列数</param>
        ''' <remarks></remarks>
        Public Sub RemoveColumnInInstlEventCopy(ByVal columnIndex As Integer, ByVal removeCount As Integer)
            instlSupplier.RemoveColumn(columnIndex, removeCount)
            'anEzSync.RemoveColumnInInstl(columnIndex, removeCount)
            For index As Integer = 0 To removeCount - 1
                anEzSync.NotifyInstlHinbanEventCopy(columnIndex + index)
            Next
            SetChanged()
        End Sub

        ''' <summary>
        ''' INSTL品番列を列削除する
        ''' </summary>
        ''' <param name="columnIndex">INSTL品番の中の列index</param>
        ''' <param name="removeCount">削除列数</param>
        ''' <remarks></remarks>
        Public Sub RemoveColumnInInstl(ByVal columnIndex As Integer, ByVal removeCount As Integer)
            instlSupplier.RemoveColumn(columnIndex, removeCount)
            anEzSync.RemoveColumnInInstl(columnIndex, removeCount)
            SetChanged()
        End Sub

        ''' <summary>
        ''' メモ欄に列挿入する
        ''' </summary>
        ''' <param name="columnIndex">メモ欄の中の列index</param>
        ''' <param name="insertCount">挿入列数</param>
        ''' <remarks></remarks>
        Public Sub InsertColumnInMemo(ByVal columnIndex As Integer, ByVal insertCount As Integer)
            memoSupplier.InsertColumn(columnIndex, insertCount)
            SetChanged()
        End Sub

        ''' <summary>
        ''' メモ欄を列削除する
        ''' </summary>
        ''' <param name="columnIndex">メモ欄の中の列index</param>
        ''' <param name="removeCount">削除列数</param>
        ''' <remarks></remarks>
        Public Sub RemoveColumnInMemo(ByVal columnIndex As Integer, ByVal removeCount As Integer)
            memoSupplier.RemoveColumn(columnIndex, removeCount)
            SetChanged()
        End Sub

        ''↓↓2014/08/01 Ⅰ.3.設計編集 ベース車改修専用化_bk) (TES)張 ADD BEGIN
        Public Sub HoyouAdd(ByVal HoyouKoseiMatrix As BuhinKoseiMatrix)
            '↓↓2014/10/09 酒井 ADD BEGIN
            '呼出し元へ移植。ezSyncで値が変わってしまうため。
            'Dim i As Integer = 0
            'For index As Integer = 0 To HoyouKoseiMatrix.Records.Count - 1
            '    If HoyouKoseiMatrix.Record(index).Level = "0" Then

            '        ''↓↓2014/09/09 Ⅰ.3.設計編集 ベース車改修専用化_bk) 酒井 ADD BEGIN
            '        'EzSync内BuhinEditKoseiSubject.OnChangedInstlHinbanOrKbnで、
            '        'EBOMデータに上書きされた値を、Hoyou画面の値に戻す。
            '        Dim hoyouBuhinNo As String = HoyouKoseiMatrix.Record(index).BuhinNo
            '        ''↓↓2014/09/19 Ⅰ.3.設計編集 ベース車改修専用化_bk) 酒井 ADD BEGIN
            '        Dim hoyouBuhinNoKbn As String = HoyouKoseiMatrix.Record(index).BuhinNoKbn
            '        ''↑↑2014/09/19 Ⅰ.3.設計編集 ベース車改修専用化_bk) 酒井 ADD END
            '        Dim hoyouShukeiCode As String = HoyouKoseiMatrix.Record(index).ShukeiCode
            '        Dim hoyouSiaShukeiCode As String = HoyouKoseiMatrix.Record(index).SiaShukeiCode
            '        Dim hoyouGencyoCkdKbn As String = HoyouKoseiMatrix.Record(index).GencyoCkdKbn
            '        Dim hoyouMakerCode As String = HoyouKoseiMatrix.Record(index).MakerCode
            '        Dim hoyouMakerName As String = HoyouKoseiMatrix.Record(index).MakerName
            '        Dim hoyouBuhinNoKaiteiNo As String = HoyouKoseiMatrix.Record(index).BuhinNoKaiteiNo
            '        Dim hoyouEdaBan As String = HoyouKoseiMatrix.Record(index).EdaBan
            '        Dim hoyouBuhinName As String = HoyouKoseiMatrix.Record(index).BuhinName
            '        ''↑↑2014/09/09 Ⅰ.3.設計編集 ベース車改修専用化_bk) 酒井 ADD END

            '        ''↓↓2014/09/19 Ⅰ.3.設計編集 ベース車改修専用化_bk) 酒井 ADD BEGIN
            '        'InstlHinban(i) = HoyouKoseiMatrix.Record(index).BuhinNo
            '        InstlHinban(i) = hoyouBuhinNo
            '        ''↓↓2014/08/29 Ⅰ.3.設計編集 ベース車改修専用化_bk) 酒井 ADD BEGIN
            '        'InstlHinbanKbn(index) = ""
            '        'InstlHinbanKbn(i) = HoyouKoseiMatrix.Record(index).BuhinNoKbn
            '        InstlHinbanKbn(i) = hoyouBuhinNoKbn
            '        ''↑↑2014/09/19 Ⅰ.3.設計編集 ベース車改修専用化_bk) 酒井 ADD END
            '        ''↑↑2014/08/29 Ⅰ.3.設計編集 ベース車改修専用化_bk) 酒井 ADD END
            '        ''↓↓2014/09/09 Ⅰ.3.設計編集 ベース車改修専用化_bk) 酒井 ADD BEGIN
            '        'InstlDataKbn(i) = "0"
            '        'BaseInstlFlg(i) = "0"
            '        ''↑↑2014/09/09 Ⅰ.3.設計編集 ベース車改修専用化_bk) 酒井 ADD END

            '        ''↓↓2014/09/09 Ⅰ.3.設計編集 ベース車改修専用化_bk) 酒井 ADD BEGIN
            '        HoyouKoseiMatrix.Record(index).BuhinNo = hoyouBuhinNo
            '        ''↓↓2014/09/19 Ⅰ.3.設計編集 ベース車改修専用化_bk) 酒井 ADD BEGIN
            '        HoyouKoseiMatrix.Record(index).BuhinNoKbn = hoyouBuhinNoKbn
            '        ''↑↑2014/09/19 Ⅰ.3.設計編集 ベース車改修専用化_bk) 酒井 ADD END
            '        HoyouKoseiMatrix.Record(index).ShukeiCode = hoyouShukeiCode
            '        HoyouKoseiMatrix.Record(index).SiaShukeiCode = hoyouSiaShukeiCode
            '        HoyouKoseiMatrix.Record(index).GencyoCkdKbn = hoyouGencyoCkdKbn
            '        HoyouKoseiMatrix.Record(index).MakerCode = hoyouMakerCode
            '        HoyouKoseiMatrix.Record(index).MakerName = hoyouMakerName
            '        HoyouKoseiMatrix.Record(index).BuhinNoKaiteiNo = hoyouBuhinNoKaiteiNo
            '        HoyouKoseiMatrix.Record(index).EdaBan = hoyouEdaBan
            '        HoyouKoseiMatrix.Record(index).BuhinName = hoyouBuhinName
            '        ''↑↑2014/09/09 Ⅰ.3.設計編集 ベース車改修専用化_bk) 酒井 ADD END

            '        i = i + 1
            '    End If
            'Next
            '’↑↑2014/10/09 酒井 ADD END

            anEzSync.NotifyHoyouGetKosei(HoyouKoseiMatrix)
            copyEventCode = ""

            SetChanged()
        End Sub
        ''↑↑2014/08/01 Ⅰ.3.設計編集 ベース車改修専用化_bk) (TES)張 ADD END

        ''↓↓2014/08/26 Ⅰ.3.設計編集 ベース車改修専用化_bz) (TES)張 ADD BEGIN
        Public Sub setSpdChanged()
            SetChanged()
        End Sub
        ''↑↑2014/08/26 Ⅰ.3.設計編集 ベース車改修専用化_bz) (TES)張 ADD END
    End Class
End Namespace