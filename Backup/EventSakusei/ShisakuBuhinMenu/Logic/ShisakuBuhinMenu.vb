Imports EventSakusei.ShisakuBuhinMenu.Logic.Impl
Imports EventSakusei.ShisakuBuhinMenu.Dao
Imports ShisakuCommon.Db.Impl
Imports ShisakuCommon.Db.EBom
Imports ShisakuCommon.Db.EBom.Exclusion
Imports ShisakuCommon.Db.EBom.Dao.Impl
Imports ShisakuCommon.Db.EBom.Dao
Imports ShisakuCommon
Imports ShisakuCommon.Util
Imports ShisakuCommon.Db.EBom.Vo
Imports ShisakuCommon.Db

Namespace ShisakuBuhinMenu.Logic

    Public Class ShisakuBuhinMenu : Inherits Observable
#Region "公開プロパティ"
        '' 試作イベントコード
        Private _ShisakuEventCode As String
        '' 編集モード(1:手配担当モード、2:予算担当モード)
        Private _EditMode As String
        '' イベントコード
        Private _EventCode As String
        '' イベント名
        Private _EventName As String
        '' ステータス
        Private _Status As String
        '' ステータス名
        Private _StatusName As String
        '' 〆切日
        Private _Shimekiribi As String
        '' 設計処置〆切日
        Private _SyochiShimekiribi As Nullable(Of DateTime)
#End Region
        Private Shared ReadOnly EMPTY_EVENT_VO As New TShisakuEventVo
        Private shisakuEventVo As TShisakuEventVo
        Private ReadOnly aLoginInfo As LoginInfo
        Private ReadOnly shisakuEventDao As TShisakuEventDao
        Private ReadOnly shisakuStatusDao As MShisakuStatusDao
        Private ReadOnly exclusionShisakuEvent As TShisakuEventExclusion
        Public Sub New(ByVal aLoginInfo As LoginInfo, ByVal shisakuEventCode As String, ByVal editMode As String)
            Me.New(aLoginInfo, _
                   shisakuEventCode, _
                   editMode, _
                   New TShisakuEventDaoImpl, _
                   New MShisakuStatusDaoImpl)
        End Sub
        Public Sub New(ByVal aLoginInfo As LoginInfo, ByVal shisakuEventCode As String, ByVal editMode As String, ByVal shisakuEventDao As TShisakuEventDao, ByVal shisakuStatusDao As MShisakuStatusDao)
            Me.aLoginInfo = aLoginInfo
            Me._ShisakuEventCode = shisakuEventCode
            Me._editmode = editMode
            Me.shisakuEventDao = shisakuEventDao
            Me.shisakuStatusDao = shisakuStatusDao

            exclusionShisakuEvent = New TShisakuEventExclusion

            If IsAddMode() Then
                InitializeAddMode()
            Else
                Load()
            End If
        End Sub

        ''' <summary>
        ''' 登録モードかを返す
        ''' </summary>
        ''' <returns>登録モードの場合、true</returns>
        ''' <remarks></remarks>
        Public Function IsAddMode() As Boolean
            Return ShisakuEventCode Is Nothing
        End Function
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
        ''' <summary>編集モード</summary>
        ''' <value>編集モード</value>
        ''' <returns>編集モード</returns>
        Public Property EditMode() As String
            Get
                Return _EditMode
            End Get
            Set(ByVal value As String)
                _EditMode = value
            End Set
        End Property
        ''' <summary>イベントコード</summary>
        ''' <returns>イベントコード</returns>
        Public ReadOnly Property EventCode() As String
            Get
                Return _EventCode
            End Get
        End Property

        ''' <summary>イベント名</summary>
        ''' <returns>イベント名</returns>
        Public ReadOnly Property EventName() As String
            Get
                Return _EventName
            End Get
        End Property

        ''' <summary>ステータス</summary>
        ''' <returns>ステータス</returns>
        Public ReadOnly Property Status() As String
            Get
                Return _Status
            End Get
        End Property

        ''' <summary>ステータス名</summary>
        ''' <returns>ステータス名</returns>
        Public ReadOnly Property StatusName() As String
            Get
                Return _StatusName
            End Get
        End Property

        ''' <summary>〆切日</summary>
        ''' <returns>〆切日</returns>
        Public ReadOnly Property Shimekiribi() As String
            Get
                Return _Shimekiribi
            End Get
        End Property

        ''' <summary>設計処置〆切日</summary>
        ''' <value>設計処置〆切日</value>
        ''' <returns>設計処置〆切日</returns>
        Public Property SyochiShimekiribi() As Nullable(Of DateTime)
            Get
                Return _SyochiShimekiribi
            End Get
            Set(ByVal value As Nullable(Of DateTime))
                If EzUtil.IsEqualIfNull(_SyochiShimekiribi, value) Then
                    Return
                End If
                _SyochiShimekiribi = value
                SetChanged()
            End Set
        End Property

        ''' <summary>使用可否</summary>
        ''' <returns>使用可否</returns>
        Public ReadOnly Property Enabled() As ShisakuBuhinMenuBtnEnable
            Get
                Return ShisakuBuhinMenuBtnEnable.Detect(shisakuEventVo.DataKbn, _Status, _SyochiShimekiribi)
            End Get
        End Property
#End Region

        Private Sub InitializeAddMode()

            shisakuEventVo = EMPTY_EVENT_VO
            SetChanged()
        End Sub

        Public Sub Load()
            Dim vo As TShisakuEventVo = shisakuEventDao.FindByPk(ShisakuEventCode)
            _EventCode = ShisakuEventCode
            _EventName = vo.ShisakuKaihatsuFugo & vo.ShisakuEventName
            _Status = vo.Status
            Dim statusVo As MShisakuStatusVo = shisakuStatusDao.FindByPk(_Status)
            If statusVo IsNot Nothing Then
                _StatusName = statusVo.ShisakuStatusName
            Else
                _StatusName = String.Empty
            End If
            _SyochiShimekiribi = DateUtil.ConvYyyymmddToDate(vo.KaiteiSyochiShimekiribi)
            _Shimekiribi = DateUtil.ConvYyyymmddToSlashYyyymmdd(vo.Shimekiribi)
            If _SyochiShimekiribi Is Nothing Then
                _SyochiShimekiribi = DateTime.Now
            End If

            shisakuEventVo = vo
            exclusionShisakuEvent.Save(_ShisakuEventCode)
            SetChanged()
        End Sub

        Private Sub PerformUpdate(ByVal cmd As Command)
            'Using db As New EBomDbClient
            '    db.BeginTransaction()
            '    If exclusionShisakuEvent.WasUpdatedBySomeone() Then
            '        db.Rollback()
            '        Dim userName As String = ResolveUserName(exclusionShisakuEvent.GetUpdatedUserId)
            '        Throw New TableExclusionException(String.Format("このデータは先程 {0} 様に更新されました。" & vbCrLf & "画面を開き直してください。", userName))
            '    End If

            '    cmd.Perform()

            '    db.Commit()
            'End Using

            cmd.Perform()
            ' cmd.Perform() で TShisakuEvent を更新しているから、Exclusion#Update はしていない
            exclusionShisakuEvent.Save(_ShisakuEventCode)

            ' 新ステータスを当インスタンスに反映する
            ApplyStatus(cmd.GetNewStatus)
            SetChanged()
        End Sub

        Private Function ResolveUserName(ByVal userId As String) As String

            Dim dao As New ShisakuDaoImpl
            Dim userVo As Rhac0650Vo = dao.FindUserById(userId)

            If userVo Is Nothing Then
                Return userId
            End If
            Return userVo.ShainName
        End Function

        ''' <summary>
        ''' 設計展開処理黄
        ''' </summary>
        ''' <remarks></remarks>
        Public Sub PerformSekkeiTenkai(ByVal kaiteiSyochiBi As Date)
            Dim command As New SekkeiTenkaiCommand(shisakuEventVo, _
                                                   aLoginInfo, _
                                                   New ShisakuDaoImpl, _
                                                   shisakuEventDao, _
                                                   New TShisakuSekkeiBlockDaoImpl, _
                                                   New TShisakuSekkeiBlockInstlDaoImpl, _
                                                   New TShisakuBuhinKouseiDaoImpl, _
                                                   New TShisakuBuhinDaoImpl, _
                                                   kaiteiSyochiBi)
            PerformUpdate(command)
        End Sub

        ''' <summary>
        ''' 新ステータスを当インスタンスに反映する
        ''' </summary>
        ''' <param name="newStatus">新ステータス</param>
        ''' <remarks></remarks>
        Private Sub ApplyStatus(ByVal newStatus As String)

            _Status = newStatus
            Dim statusVo As MShisakuStatusVo = shisakuStatusDao.FindByPk(newStatus)
            If statusVo Is Nothing Then
                Throw New ArgumentException("値 '" & newStatus & "' に該当する名称が、試作ステータスマスタにありません.")
            End If
            _StatusName = statusVo.ShisakuStatusName
        End Sub

        ''' <summary>
        ''' 差戻し処理
        ''' </summary>
        ''' <remarks></remarks>
        Public Sub PerformSashimodoshi()
            Dim command As New SashimodoshiCommand(ShisakuEventCode, _
                                                    aLoginInfo, _
                                                    New ShisakuDaoImpl, _
                                                    shisakuEventDao, _
                                                    New TShisakuSekkeiBlockDaoImpl, _
                                                    New TShisakuSekkeiBlockSoubiDaoImpl, _
                                                    New TShisakuSekkeiBlockSoubiShiyouDaoImpl, _
                                                    New TShisakuSekkeiBlockMemoDaoImpl, _
                                                    New TShisakuSekkeiBlockInstlDaoImpl, _
                                                    New TShisakuBuhinEditDaoImpl, _
                                                    New TShisakuBuhinEditInstlDaoImpl, _
                                                    New TShisakuBuhinEditBaseDaoImpl, _
                                                    New TShisakuBuhinEditInstlBaseDaoImpl, _
                                                    New TShisakuEventKanseiRirekiDaoImpl, _
                                                    New TShisakuEventSoubiRirekiDaoImpl)
            'New TShisakuBuhinDaoImpl, _
            'New TShisakuBuhinKouseiDaoImpl, _
            PerformUpdate(command)
        End Sub

        ''' <summary>
        ''' 設計処置〆切処理
        ''' </summary>
        ''' <param name="shimekiribi">〆切日</param>
        ''' <remarks></remarks>
        Public Sub PerformSyochiShimekiri(ByVal shimekiribi As DateTime)
            Dim command As New ShochiShimekiriCommand(ShisakuEventCode, _
                                                      shimekiribi, _
                                                      aLoginInfo, _
                                                      New ShisakuDaoImpl, _
                                                      shisakuEventDao)
            PerformUpdate(command)
            _Shimekiribi = shimekiribi.ToString("yyyy/MM/dd")
        End Sub
        ''' <summary>
        ''' 完了処理
        ''' </summary>
        ''' <remarks></remarks>
        Public Sub PerformKanryo()
            Dim command As New KanryoCommand(ShisakuEventCode, _
                                             aLoginInfo, _
                                             New ShisakuDaoImpl, _
                                             shisakuEventDao)
            PerformUpdate(command)
        End Sub
        ''' <summary>
        ''' 中止処理
        ''' </summary>
        ''' <remarks></remarks>
        Public Sub PerformChushi()
            Dim command As New ChushiCommand(ShisakuEventCode, _
                                             aLoginInfo, _
                                             New ShisakuDaoImpl, _
                                             shisakuEventDao)
            PerformUpdate(command)
        End Sub

        ''' <summary>
        ''' 手配情報関係の削除処理
        ''' </summary>
        ''' <remarks></remarks>
        Public Sub PerformDelete(ByVal ShisakuEventCode As String, ByVal ShisakuListCode As String)
            Dim ListImpl As ListCodeDao = New ListCodeDaoImpl

            '削除するもの'
            'T_SHISAKU_LIST_CODE'
            ListImpl.DeleteByListCode(ShisakuEventCode, ShisakuListCode)
            'T_SHISAKU_TEHAI_KIHON'
            ListImpl.DeleteByTehaiKihon(ShisakuEventCode, ShisakuListCode)
            'T_SHISAKU_TEHAI_GOUGYA'
            ListImpl.DeleteByTehaiGousya(ShisakuEventCode, ShisakuListCode)
            'T_SHISAKU_TEHAI_ERROR'
            ListImpl.DeleteByTehaiError(ShisakuEventCode, ShisakuListCode)
            'T_SHISAKU_TEHAI_KAITEI_BLOCK'
            ListImpl.DeleteByTehaiKaiteiBlock(ShisakuEventCode, ShisakuListCode)
            'T_SHISAKU_TEHAI_TEISEI_KIHON'
            ListImpl.DeleteByTeiseiKihon(ShisakuEventCode, ShisakuListCode)
            'T_SHISAKU_TEHAI_TEISEI_GOUSYA'
            ListImpl.DeleteByTehaiTeiseiGousya(ShisakuEventCode, ShisakuListCode)
            'T_SHISAKU_BUHIN_EDIT_TMP'
            ListImpl.DeleteByKihonTmp(ShisakuEventCode)
            'T_SHISAKU_BUHIN_EDIT_GOUSYA_TMP'
            ListImpl.DeleteByGousyaTmp(ShisakuEventCode)
            'T_SHISAKU_BUHIN_EDIT_KAITEI'
            ListImpl.DeleteByBuhinEditKaitei(ShisakuEventCode, ShisakuListCode)
            'T_SHISAKU_BUHIN_EDIT_GOUSYA_KAITEI'
            ListImpl.DeleteByBuhinEditGousyaKaitei(ShisakuEventCode, ShisakuListCode)


            'T_SHISAKU_TEHAI_SHUTUZU_JISEKI'
            ListImpl.DeleteByTehaiShutuzuJiseki(ShisakuEventCode, ShisakuListCode)
            'T_SHISAKU_TEHAI_SHUTUZU_JISEKI_INPUT'
            ListImpl.DeleteByTehaiShutuzuJisekiInput(ShisakuEventCode, ShisakuListCode)
            'T_SHISAKU_TEHAI_SHUTUZU_ORIKOMI'
            ListImpl.DeleteByTehaiShutuzuOrikomi(ShisakuEventCode, ShisakuListCode)
            'T_SHISAKU_TEHAI_GOUSYA_GROUP'
            ListImpl.DeleteByTehaiGousyaGroup(ShisakuEventCode, ShisakuListCode)


        End Sub

        ''' <summary>
        ''' 削除してもよいかチェック
        ''' </summary>
        ''' <param name="ShisakuEventCode">イベントコード</param>
        ''' <param name="ShisakuListCode">リストコード</param>
        ''' <returns>削除不可ならTrue</returns>
        ''' <remarks></remarks>
        Public Function DeleteCheck(ByVal ShisakuEventCode As String, ByVal ShisakuListCode As String) As Boolean
            Dim ListImpl As ListCodeDao = New ListCodeDaoImpl
            Dim rireki As String

            rireki = ListImpl.FindByHacchuJisseki(ShisakuEventCode, ShisakuListCode)

            If StringUtil.IsEmpty(rireki) Then
                Return False
            End If

            Return True

        End Function



    End Class
End Namespace