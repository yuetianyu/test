Imports ShisakuCommon.Db.EBom.Vo.Helper
Imports ShisakuCommon.Db
Imports ShisakuCommon.Db.EBom.Dao
Imports ShisakuCommon
Imports ShisakuCommon.Util
Imports ShisakuCommon.Db.EBom.Vo

Namespace YosanBuhinEdit.Logic

    ''' <summary>
    ''' 予算書部品表編集画面（ヘッダー部）の表示全般を担うクラス
    ''' </summary>
    ''' <remarks></remarks>
    Public Class BuhinEditHeaderSubject : Inherits Observable
        Private ReadOnly _yosanEventCode As String
        Private ReadOnly _yosanBukaCode As String
        Private ReadOnly aLoginInfo As LoginInfo
        Private ReadOnly yosanEventDao As TYosanEventDao
        Private ReadOnly aRhac1560Dao As Rhac1560Dao
        Private ReadOnly aRhac2130Dao As Rhac2130Dao
        Private ReadOnly aYosanDao As YosanDao
        Private ReadOnly aShisakuDate As ShisakuDate
        Private ReadOnly telNoDao As TShisakuTelNoDao
        Private KSMode As Integer

        ''' <summary>
        ''' コンストラクタ
        ''' </summary>
        ''' <param name="yosanEventCode">予算書イベントコード</param>
        ''' <param name="aLoginInfo">ログイン情報</param>
        ''' <param name="aShisakuDate">試作日付</param>
        ''' <param name="yosanEventDao">予算書イベント情報Dao</param>
        ''' <param name="aRhac1560Dao">課マスタDao</param>
        ''' <param name="aRhac2130Dao">社員マスタDao</param>
        ''' <param name="aYosanDao">予算書システムDao</param>
        ''' <remarks></remarks>
        Public Sub New(ByVal yosanEventCode As String, _
                       ByVal aLoginInfo As LoginInfo, _
                       ByVal aShisakuDate As ShisakuDate, _
                       ByVal yosanEventDao As TYosanEventDao, _
                       ByVal aRhac1560Dao As Rhac1560Dao, _
                       ByVal aRhac2130Dao As Rhac2130Dao, _
                       ByVal aYosanDao As YosanDao, _
                       ByVal KSMode As Integer, _
                       ByVal telNoDao As TShisakuTelNoDao)
            Me._yosanEventCode = yosanEventCode
            Me._yosanBukaCode = aLoginInfo.BukaCode
            Me.aLoginInfo = aLoginInfo
            Me.yosanEventDao = yosanEventDao
            Me.aRhac1560Dao = aRhac1560Dao
            Me.aShisakuDate = aShisakuDate
            Me.aYosanDao = aYosanDao
            Me.aRhac2130Dao = aRhac2130Dao
            Me.KSMode = KSMode
            Me.telNoDao = telNoDao
            Load()
        End Sub

#Region "公開プロパティ"
        '予算書部品情報
        Private _yosanEventVo As TYosanEventVo

        '開発符号
        Private _yosanKaihatsuFugo As String
        'イベント名称
        Private _yosanEventName As String
        '担当設計
        Private _yosanBukaName As String
        '担当者ID
        Private _userId As String
        '担当者名
        Private _userName As String
        'TEL
        Private _telNo As String

        'イベント(画面選択値)
        Private _yosanEventSelectedValue As String

        ' 一時保存のメモ
        Private _memo As String

        ''' <summary>予算書部品情報（キー情報としても有効）</summary>
        Public ReadOnly Property yosanEventVo() As TYosanEventVo
            Get
                Return _yosanEventVo
            End Get
        End Property

        ''' <summary>予算書開発符号</summary>
        ''' <value>予算書開発符号</value>
        ''' <returns>予算書開発符号</returns>
        Public Property YosanKaihatsuFugo() As String
            Get
                Return _yosanKaihatsuFugo
            End Get
            Set(ByVal value As String)
                _yosanKaihatsuFugo = value
            End Set
        End Property

        ''' <summary>予算書イベント名称</summary>
        ''' <value>予算書イベント名称</value>
        ''' <returns>予算書イベント名称</returns>
        Public Property YosanEventName() As String
            Get
                Return _yosanEventName
            End Get
            Set(ByVal value As String)
                _yosanEventName = value
            End Set
        End Property

        ''' <summary>担当設計</summary>
        ''' <value>担当設計</value>
        ''' <returns>担当設計</returns>
        Public Property YosanBukaName() As String
            Get
                Return _yosanBukaName
            End Get
            Set(ByVal value As String)
                _yosanBukaName = value
            End Set
        End Property

        ''' <summary>担当者ID</summary>
        ''' <value>担当者ID</value>
        ''' <returns>担当者ID</returns>
        Public Property UserId() As String
            Get
                Return _userId
            End Get
            Set(ByVal value As String)
                _userId = value
            End Set
        End Property

        ''' <summary>担当者名</summary>
        ''' <value>担当者名</value>
        ''' <returns>担当者名</returns>
        Public Property UserName() As String
            Get
                Return _userName
            End Get
            Set(ByVal value As String)
                _userName = value
            End Set
        End Property

        ''' <summary>TEL</summary>
        ''' <value>TEL</value>
        ''' <returns>TEL</returns>
        Public Property TelNo() As String
            Get
                Return _telNo
            End Get
            Set(ByVal value As String)
                _telNo = value
            End Set
        End Property

        ''' <summary>イベント(画面選択値)</summary>
        ''' <value>イベント(画面選択値)</value>
        ''' <returns>イベント(画面選択値)</returns>
        Public Property YosanEventSelectedValue() As String
            Get
                Return _yosanEventSelectedValue
            End Get
            Set(ByVal value As String)
                _yosanEventSelectedValue = value
            End Set
        End Property

        ''' <summary>一時保存のメモ</summary>
        ''' <value>一時保存のメモ</value>
        ''' <returns>一時保存のメモ</returns>
        Public Property Memo() As String
            Get
                Return _memo
            End Get
            Set(ByVal value As String)
                _memo = value
            End Set
        End Property
#End Region

        ''' <summary>
        ''' データを読み込む
        ''' </summary>
        ''' <remarks></remarks>
        Private Sub Load()
            '予算イベント情報を取得
            _yosanEventVo = yosanEventDao.FindByPk(_yosanEventCode)

            If Not _yosanEventCode.Equals(String.Empty) Then
                If _yosanEventVo Is Nothing Then
                    Throw New RecordNotFoundException(String.Format("予算書イベント情報が見つかりません. (YosanEventCode) = ({0})", _yosanEventCode))
                End If
            End If

            '開発符号、イベント名、メモ
            If _yosanEventVo Is Nothing Then
                _yosanKaihatsuFugo = String.Empty
                _yosanEventName = String.Empty
                _memo = String.Empty
            Else
                _yosanKaihatsuFugo = _yosanEventVo.YosanKaihatsuFugo
                _yosanEventName = _yosanEventVo.YosanEventName
                _memo = _yosanEventVo.Memo
            End If

            '担当者ID
            _userId = aLoginInfo.UserId

            '現在ログインしている人の部課コードで検索する
            Dim bukaVo As Rhac1560Vo = aRhac1560Dao.FindByPk(LoginInfo.Now.BuCode, Rhac1560VoHelper.SiteKbn.Gumma, LoginInfo.Now.KaCode)
            '担当設計
            If bukaVo Is Nothing Then
                _yosanBukaName = LoginInfo.Now.BukaRyakuName
            Else
                _yosanBukaName = bukaVo.KaRyakuName
            End If

            Dim user2130Vo As Rhac2130Vo = aRhac2130Dao.FindByPk(_userId)
            Dim userVo As Rhac0650Vo = aYosanDao.FindUserById(_userId)
            Dim telVo As TShisakuTelNoVo = telNoDao.FindByPk(_userId)
            '担当者
            If userVo Is Nothing Then
                If user2130Vo Is Nothing Then
                    _userName = LoginInfo.Now.ShainName
                Else
                    _userName = user2130Vo.ShainName
                End If
            Else
                _userName = userVo.ShainName
            End If
            'TEL
            If telVo Is Nothing Then
                If userVo Is Nothing Then
                    If user2130Vo Is Nothing Then
                        _telNo = String.Empty
                    Else
                        _telNo = user2130Vo.NaisenNo
                    End If
                Else
                    _telNo = userVo.NaisenNo
                End If
            Else
                _telNo = telVo.TelNo
            End If

            SetChanged()
        End Sub

        ''' <summary>
        ''' 保存する
        ''' </summary>
        ''' <remarks></remarks>
        Public Sub Save()
            RegisterMain(False)
        End Sub
        ''' <summary>
        ''' 登録する
        ''' </summary>
        ''' <remarks></remarks>
        Public Sub Register()
            RegisterMain(True)
        End Sub

        ''' <summary>
        ''' 登録処理の本体
        ''' </summary>
        ''' <param name="isRegister">「登録」の場合、true</param>
        ''' <remarks></remarks>
        Private Sub RegisterMain(ByVal isRegister As Boolean)

            Dim yosanVo As TYosanEventVo = yosanEventDao.FindByPk(_yosanEventVo.YosanEventCode)

            If yosanVo Is Nothing Then
                yosanVo = _yosanEventVo

                If Not isRegister Then
                    yosanVo.Memo = Memo
                    yosanVo.YosanSaveDateBuhin = aShisakuDate.CurrentDateDbFormat
                    yosanVo.YosanSaveTimeBuhin = aShisakuDate.CurrentTimeDbFormat
                Else
                    yosanVo.YosanRegisterDateBuhin = aShisakuDate.CurrentDateDbFormat
                    yosanVo.YosanRegisterTimeBuhin = aShisakuDate.CurrentTimeDbFormat
                End If
                yosanVo.UpdatedUserId = aLoginInfo.UserId
                yosanVo.UpdatedDate = aShisakuDate.CurrentDateDbFormat
                yosanVo.UpdatedTime = aShisakuDate.CurrentTimeDbFormat

                yosanEventDao.InsertBy(yosanVo)
            Else
                If Not isRegister Then
                    yosanVo.Memo = Memo
                    yosanVo.YosanSaveDateBuhin = aShisakuDate.CurrentDateDbFormat
                    yosanVo.YosanSaveTimeBuhin = aShisakuDate.CurrentTimeDbFormat
                Else
                    yosanVo.YosanRegisterDateBuhin = aShisakuDate.CurrentDateDbFormat
                    yosanVo.YosanRegisterTimeBuhin = aShisakuDate.CurrentTimeDbFormat
                End If
                yosanVo.UpdatedUserId = aLoginInfo.UserId
                yosanVo.UpdatedDate = aShisakuDate.CurrentDateDbFormat
                yosanVo.UpdatedTime = aShisakuDate.CurrentTimeDbFormat

                yosanEventDao.UpdateByPk(yosanVo)

            End If

            _yosanEventVo = yosanVo

        End Sub

    End Class
End Namespace
