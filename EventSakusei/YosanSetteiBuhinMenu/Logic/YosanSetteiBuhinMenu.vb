Imports ShisakuCommon.Db.Impl
Imports ShisakuCommon.Db.EBom
Imports ShisakuCommon.Db.EBom.Exclusion
Imports ShisakuCommon.Db.EBom.Dao.Impl
Imports ShisakuCommon.Db.EBom.Dao
Imports ShisakuCommon
Imports ShisakuCommon.Util
Imports ShisakuCommon.Db.EBom.Vo
Imports ShisakuCommon.Db
Imports EventSakusei.YosanSetteiBuhinMenu.Dao
Imports EventSakusei.YosanSetteiBuhinMenu.Logic.Impl

Namespace YosanSetteiBuhinMenu.Logic

    Public Class YosanSetteiBuhinMenu : Inherits Observable
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

        Private Function ResolveUserName(ByVal userId As String) As String

            Dim dao As New ShisakuDaoImpl
            Dim userVo As Rhac0650Vo = dao.FindUserById(userId)

            If userVo Is Nothing Then
                Return userId
            End If
            Return userVo.ShainName
        End Function

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
        ''' 手配情報関係の削除処理
        ''' </summary>
        ''' <remarks></remarks>
        Public Sub PerformDelete(ByVal ShisakuEventCode As String, ByVal ShisakuListCode As String)
            Dim ListImpl As ListCodeDao = New ListCodeDaoImpl

            '削除するもの'
            'T_YOSAN_SETTEI_LISTCODE'
            ListImpl.DeleteByListCode(ShisakuEventCode, ShisakuListCode)
            'T_YOSAN_SETTEI_BUHIN'
            ListImpl.DeleteByTehaiKihon(ShisakuEventCode, ShisakuListCode)
            'T_YOSAN_SETTEI_GOUSYA'
            ListImpl.DeleteByTehaiGousya(ShisakuEventCode, ShisakuListCode)
            'T_YOSAN_SETTEI_BUHIN_TMP'
            ListImpl.DeleteByKihonTmp(ShisakuEventCode)
            'T_YOSAN_SETTEI_GOUSYA_TMP'
            ListImpl.DeleteByGousyaTmp(ShisakuEventCode)

            'T_YOSAN_SETTEI_BUHIN_RIREKI'
            ListImpl.DeleteByYosanSetteiBuhinRireki(ShisakuEventCode, ShisakuListCode)

        End Sub

        ''' <summary>
        ''' 削除してもよいかチェック
        ''' </summary>
        ''' <param name="ShisakuEventCode">イベントコード</param>
        ''' <param name="ShisakuListCode">リストコード</param>
        ''' <returns>削除不可ならTrue</returns>
        ''' <remarks></remarks>
        Public Function DeleteCheck(ByVal ShisakuEventCode As String, ByVal ShisakuListCode As String) As Boolean


            '削除ロジックが必要なら下記の様にコーディングする。


            'Dim ListImpl As ListCodeDao = New ListCodeDaoImpl
            'Dim rireki As String

            'rireki = ListImpl.FindByHacchuJisseki(ShisakuEventCode, ShisakuListCode)

            'If StringUtil.IsEmpty(rireki) Then
            Return False
            'End If

            'Return True

        End Function

    End Class
End Namespace