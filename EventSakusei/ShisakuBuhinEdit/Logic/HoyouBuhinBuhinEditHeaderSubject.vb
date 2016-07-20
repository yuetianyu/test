Imports ShisakuCommon.Db.EBom.Vo.Helper
Imports ShisakuCommon.Db
Imports ShisakuCommon.Util.LabelValue
Imports ShisakuCommon.Db.EBom.Dao
Imports ShisakuCommon
Imports ShisakuCommon.Util
Imports ShisakuCommon.Db.EBom.Vo

Namespace ShisakuBuhinEdit.Logic

    ''' <summary>
    ''' 補用部品表編集画面（ヘッダー部）の表示全般を担うクラス
    ''' </summary>
    ''' <remarks></remarks>
    Public Class HoyouBuhinBuhinEditHeaderSubject : Inherits Observable
        Private ReadOnly _hoyouEventCode As String
        Private ReadOnly _hoyouBukaCode As String
        Private ReadOnly aLoginInfo As LoginInfo
        Private ReadOnly eventDao As THoyouEventDao
        Private ReadOnly tantoDao As THoyouSekkeiTantoDao
        Private ReadOnly aRhac1560Dao As Rhac1560Dao
        Private ReadOnly aRhac2130Dao As Rhac2130Dao
        Private ReadOnly aHoyouDao As HoyouDao
        Private ReadOnly aShisakuDate As ShisakuDate
        Private ReadOnly telNoDao As TShisakuTelNoDao
        Private KSMode As Integer

        ''' <summary>
        ''' コンストラクタ
        ''' </summary>
        ''' <param name="hoyouEventCode">補用イベントコード</param>
        ''' <param name="hoyouBukaCode">補用部課コード</param>
        ''' <param name="hoyouTantoNo">補用担当No</param>
        ''' <param name="aLoginInfo">ログイン情報</param>
        ''' <param name="aShisakuDate">試作日付</param>
        ''' <param name="eventDao">補用イベント情報Dao</param>
        ''' <param name="tantoDao">補用設計担当情報Dao</param>
        ''' <param name="aRhac1560Dao">課マスタDao</param>
        ''' <param name="aRhac2130Dao">社員マスタDao</param>
        ''' <param name="aHoyouDao">補用システムDao</param>
        ''' <remarks></remarks>
        Public Sub New(ByVal hoyouEventCode As String, _
                       ByVal hoyouBukaCode As String, _
                       ByVal hoyouTantoNo As String, _
                       ByVal aLoginInfo As LoginInfo, _
                       ByVal aShisakuDate As ShisakuDate, _
                       ByVal eventDao As THoyouEventDao, _
                       ByVal tantoDao As THoyouSekkeiTantoDao, _
                       ByVal aRhac1560Dao As Rhac1560Dao, _
                       ByVal aRhac2130Dao As Rhac2130Dao, _
                       ByVal aHoyouDao As HoyouDao, _
                       ByVal KSMode As Integer, _
                       ByVal telNoDao As TShisakuTelNoDao)
            Me._hoyouEventCode = hoyouEventCode
            Me._hoyouBukaCode = hoyouBukaCode
            Me._hoyoutantoNo = hoyouTantoNo
            Me.aLoginInfo = aLoginInfo
            Me.eventDao = eventDao
            Me.tantoDao = tantoDao
            Me.aRhac1560Dao = aRhac1560Dao
            Me.aShisakuDate = aShisakuDate
            Me.aHoyouDao = aHoyouDao
            Me.aRhac2130Dao = aRhac2130Dao
            Me.KSMode = KSMode
            Me.telNoDao = telNoDao
            Load()
        End Sub

#Region "公開プロパティ"
        '補用開発符号
        Private _hoyouKaihatsuFugo As String
        '補用イベント名称
        Private _hoyouEventName As String
        '担当設計
        Private _hoyouBukaName As String
        '担当者ID
        Private _userId As String
        '担当者名
        Private _userName As String
        'TEL
        Private _telNo As String

        '--------------------------
        '２次改修で追加
        '内容
        Private _naiyou As String
        '担当者の改訂№
        Private _kaiteiNo As String
        '--------------------------

        '担当№
        Private _hoyouTantoNo As String
        '担当№(画面選択値)
        Private _hoyouTantoNoSelectedValue As String
        ' 一時保存のメモ
        Private _memo As String

        ''' <summary>補用開発符号</summary>
        ''' <value>補用開発符号</value>
        ''' <returns>補用開発符号</returns>
        Public Property HoyouKaihatsuFugo() As String
            Get
                Return _hoyouKaihatsuFugo
            End Get
            Set(ByVal value As String)
                _hoyouKaihatsuFugo = value
            End Set
        End Property

        ''' <summary>補用イベント名称</summary>
        ''' <value>補用イベント名称</value>
        ''' <returns>補用イベント名称</returns>
        Public Property HoyouEventName() As String
            Get
                Return _hoyouEventName
            End Get
            Set(ByVal value As String)
                _hoyouEventName = value
            End Set
        End Property

        ''' <summary>担当設計</summary>
        ''' <value>担当設計</value>
        ''' <returns>担当設計</returns>
        Public Property HoyouBukaName() As String
            Get
                Return _hoyouBukaName
            End Get
            Set(ByVal value As String)
                _hoyouBukaName = value
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

        ''' <summary>内容</summary>
        ''' <value>内容</value>
        ''' <returns>内容</returns>
        Public Property Naiyou() As String
            Get
                Return _naiyou
            End Get
            Set(ByVal value As String)
                _naiyou = value
            End Set
        End Property

        ''' <summary>改訂№</summary>
        ''' <value>改訂№</value>
        ''' <returns>改訂№</returns>
        Public Property KaiteiNo() As String
            Get
                Return _kaiteiNo
            End Get
            Set(ByVal value As String)
                _kaiteiNo = value
            End Set
        End Property

        ''' <summary>担当№</summary>
        ''' <value>担当№</value>
        ''' <returns>担当№</returns>
        Public Property HoyouTantoNo() As String
            Get
                Return _hoyouTantoNo
            End Get
            Set(ByVal value As String)
                _hoyouTantoNo = value
            End Set
        End Property

        ''' <summary>担当№(画面選択値)</summary>
        ''' <value>担当№(画面選択値)</value>
        ''' <returns>担当№(画面選択値)</returns>
        Public Property HoyouTantoNoSelectedValue() As String
            Get
                Return _hoyouTantoNoSelectedValue
            End Get
            Set(ByVal value As String)
                _hoyouTantoNoSelectedValue = value
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

#Region "公開プロパティ選択値"
        '' 担当Noの選択値
        Private _tantoNoLabelValues As List(Of LabelValueVo)

        ''' <summary>担当Noの選択値</summary>
        ''' <value>担当Noの選択値</value>
        ''' <returns>担当Noの選択値</returns>
        Public Property TantoNoLabelValues() As List(Of LabelValueVo)
            Get
                Return _tantoNoLabelValues
            End Get
            Set(ByVal value As List(Of LabelValueVo))
                _tantoNoLabelValues = value
            End Set
        End Property
#End Region

        Private eventVo As THoyouEventVo
        Private _tantoVo As THoyouSekkeiTantoVo
        ''' <summary>担当情報（キー情報としても有効）</summary>
        Public ReadOnly Property TantoVo() As THoyouSekkeiTantoVo
            Get
                Return _tantoVo
            End Get
        End Property

        Private _IsAddMode As Boolean
        ''' <summary>
        ''' 登録モードかを返す
        ''' </summary>
        ''' <returns>登録モードの場合、true</returns>
        ''' <remarks></remarks>
        Public Function IsAddMode() As Boolean
            Return _IsAddMode
        End Function

        ''' <summary>
        ''' データを読み込む
        ''' </summary>
        ''' <remarks></remarks>
        Private Sub Load()
            eventVo = eventDao.FindByPk(_hoyouEventCode)
            If Not _hoyouEventCode.Equals(String.Empty) Then
                If eventVo Is Nothing Then
                    Throw New RecordNotFoundException(String.Format("補用イベント情報が見つかりません. (HoyouEventCode) = ({0})", _hoyouEventCode))
                End If
            End If

            _tantoVo = FindNewestTantoBy()
            'ブロック情報があるかどうかが判断条件だとマズイ
            If _tantoVo Is Nothing Then
                _IsAddMode = True
                ' 「登録モード」でも他の協調クラスが、BlockVoのキー値を参照するので作成する
                _tantoVo = New THoyouSekkeiTantoVo
                _tantoVo.HoyouEventCode = _hoyouEventCode
                _tantoVo.HoyouBukaCode = _hoyouBukaCode
                _tantoVo.HoyouTanto = _hoyouTantoNo
                _tantoVo.HoyouTantoKaiteiNo = THoyouSekkeiTantoVoHelper.HoyouTantoKaiteiNo.DEFAULT_VALUE

                _userId = aLoginInfo.UserId
            Else
                _IsAddMode = False
                '_userId = _blockVo.UserId
                _userId = aLoginInfo.UserId
            End If

            '現在ログインしている人の部課コードで検索する
            'Dim bukaVo As Rhac1560Vo = aRhac1560Dao.FindByPk(_shisakuBukaCode.Substring(0, 2), Rhac1560VoHelper.SiteKbn.Gumma, _shisakuBukaCode.Substring(2))
            Dim bukaVo As Rhac1560Vo = aRhac1560Dao.FindByPk(LoginInfo.Now.BuCode, Rhac1560VoHelper.SiteKbn.Gumma, LoginInfo.Now.KaCode)

            If eventVo Is Nothing Then
                _hoyouKaihatsuFugo = String.Empty
                _hoyouEventName = String.Empty
            Else
                _hoyouKaihatsuFugo = eventVo.HoyouKaihatsuFugo
                _hoyouEventName = eventVo.HoyouEventName
            End If

            If bukaVo Is Nothing Then
                _hoyouBukaName = LoginInfo.Now.BukaRyakuName
            Else
                _hoyouBukaName = bukaVo.KaRyakuName
            End If

            Dim user2130Vo As Rhac2130Vo = aRhac2130Dao.FindByPk(_userId)
            Dim userVo As Rhac0650Vo = aHoyouDao.FindUserById(_userId)
            Dim telVo As TShisakuTelNoVo = telNoDao.FindByPk(_userId)
            If userVo Is Nothing Then
                If user2130Vo Is Nothing Then
                    _userName = LoginInfo.Now.ShainName
                Else
                    _userName = user2130Vo.ShainName
                End If
            Else
                _userName = userVo.ShainName
            End If
            'IsAddModeがちゃんと動くか見る 樺澤
            If IsAddMode() Then
                If userVo Is Nothing Then
                    If telVo Is Nothing Then
                        _telNo = String.Empty
                    Else
                        _telNo = user2130Vo.NaisenNo
                    End If
                Else
                    _telNo = userVo.NaisenNo
                End If
                _memo = String.Empty
                _naiyou = String.Empty '２次改修で追加
            Else
                If StringUtil.IsEmpty(_tantoVo.TelNo) Then
                    'If user2130Vo Is Nothing Then
                    '    _telNo = String.Empty
                    'Else
                    '    _telNo = user2130Vo.NaisenNo
                    'End If
                    _memo = _tantoVo.Memo
                    _naiyou = _tantoVo.KaiteiNaiyou '２次改修で追加
                Else
                    _telNo = _tantoVo.TelNo
                    _memo = _tantoVo.Memo
                    _naiyou = _tantoVo.KaiteiNaiyou '２次改修で追加
                End If
            End If
            If StringUtil.IsEmpty(_tantoVo.TelNo) Then
                If telVo Is Nothing Then
                    _telNo = String.Empty
                Else
                    _telNo = telVo.TelNo
                End If
            Else
                _telNo = _tantoVo.TelNo
            End If

            '---------------------------------------------
            '２次改修で追加
            '   改訂№を返す。
            _kaiteiNo = _tantoVo.HoyouTantoKaiteiNo
            '---------------------------------------------

            TantoNoLabelValues = GetLabelValues_TantoNo()

            SetChanged()
        End Sub

        ''' <summary>
        ''' 最新の担当No改訂Noをもつ補用設計担当情報を参照する
        ''' </summary>
        ''' <returns>補用設計担当情報</returns>
        ''' <remarks></remarks>
        Private Function FindNewestTantoBy() As THoyouSekkeiTantoVo

            Dim param As New THoyouSekkeiTantoVo
            param.HoyouEventCode = _hoyouEventCode
            param.HoyouBukaCode = _hoyouBukaCode
            param.HoyouTanto = _hoyouTantoNo
            Dim tantoVos As List(Of THoyouSekkeiTantoVo) = tantoDao.FindBy(param)
            If tantoVos.Count = 0 Then
                Return Nothing
            End If
            tantoVos.Sort(New TantoNoKaiteiNoComparer)
            Return tantoVos(tantoVos.Count - 1)
        End Function

        ''' <summary>
        ''' 担当No改訂Noでソートする IComperer実装クラス
        ''' </summary>
        ''' <remarks></remarks>
        Private Class TantoNoKaiteiNoComparer : Implements IComparer(Of THoyouSekkeiTantoVo)
            Public Function Compare(ByVal x As ShisakuCommon.Db.EBom.Vo.THoyouSekkeiTantoVo, ByVal y As ShisakuCommon.Db.EBom.Vo.THoyouSekkeiTantoVo) As Integer Implements System.Collections.Generic.IComparer(Of ShisakuCommon.Db.EBom.Vo.THoyouSekkeiTantoVo).Compare
                If x.HoyouTantoKaiteiNo Is Nothing AndAlso y.HoyouTantoKaiteiNo Is Nothing Then
                    Return 0
                ElseIf y.HoyouTantoKaiteiNo Is Nothing Then
                    Return 1
                ElseIf x.HoyouTantoKaiteiNo Is Nothing Then
                    Return -1
                End If
                Return CInt(x.HoyouTantoKaiteiNo).CompareTo(CInt(y.HoyouTantoKaiteiNo))
            End Function
        End Class

        ''' <summary>
        ''' 担当NoのLabelValueを抽出する ILabelValueExtraction実装クラス
        ''' </summary>
        ''' <remarks></remarks>
        Private Class TantoNoExtraction : Implements ILabelValueExtraction
            Public Sub Extraction(ByVal aLocator As ShisakuCommon.Util.LabelValue.LabelValueLocator) Implements ShisakuCommon.Util.LabelValue.ILabelValueExtraction.Extraction
                Dim vo As New THoyouSekkeiTantoVo
                aLocator.IsA(vo).Label(vo.HoyouTanto).Value(vo.HoyouTanto)
            End Sub
        End Class

        ''' <summary>担当No表示順でソート</summary>
        Private Class TantoNoComparer : Implements IComparer(Of THoyouSekkeiTantoVo)
            Public Function Compare(ByVal x As ShisakuCommon.Db.EBom.Vo.THoyouSekkeiTantoVo, ByVal y As ShisakuCommon.Db.EBom.Vo.THoyouSekkeiTantoVo) As Integer Implements System.Collections.Generic.IComparer(Of ShisakuCommon.Db.EBom.Vo.THoyouSekkeiTantoVo).Compare
                If x.HoyouTantoHyoujiJun Is Nothing AndAlso y.HoyouTantoHyoujiJun Is Nothing Then
                    Return 0
                ElseIf y.HoyouTantoHyoujiJun Is Nothing Then
                    Return 1
                ElseIf x.HoyouTantoHyoujiJun Is Nothing Then
                    Return -1
                End If
                Return CInt(x.HoyouTantoHyoujiJun).CompareTo(CInt(y.HoyouTantoHyoujiJun))
            End Function
        End Class

        ''' <summary>
        ''' 担当NoのLabelValueVoを参照する
        ''' </summary>
        ''' <returns>担当Noの一覧</returns>
        ''' <remarks></remarks>
        Private Function GetLabelValues_TantoNo() As List(Of LabelValueVo)
            Dim param As New THoyouSekkeiTantoVo
            param.HoyouEventCode = _hoyouEventCode
            'param.HoyouBukaCode = _hoyouBukaCode
            Dim tantoVos As List(Of THoyouSekkeiTantoVo) = tantoDao.FindBy(param)
            tantoVos.Sort(New TantoNoComparer)
            Dim results As List(Of LabelValueVo) = LabelValueExtracter(Of THoyouSekkeiTantoVo).Extract(tantoVos, New TantoNoExtraction)
            Return results
        End Function

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
            Dim tantoVo As THoyouSekkeiTantoVo
            If IsAddMode() Then
                tantoVo = _tantoVo
                '改訂編集からきた場合、改訂は001からはじめる'
                If KSMode = 1 AndAlso StringUtil.Equals(tantoVo.HoyouTantoKaiteiNo, "000") Then
                    tantoVo.HoyouTantoKaiteiNo = "001"
                End If

                tantoVo.HoyouTantoHyoujiJun = FindMaxHoyouTantoHyoujiJun(_hoyouEventCode, _hoyouBukaCode) + 1
                tantoVo.TantoFuyou = THoyouSekkeiTantoVoHelper.TantoFuyou.NECESSARY
                tantoVo.TantoMemo = Nothing
                tantoVo.CreatedUserId = aLoginInfo.UserId
                tantoVo.CreatedDate = aShisakuDate.CurrentDateDbFormat
                tantoVo.CreatedTime = aShisakuDate.CurrentTimeDbFormat
            Else
                tantoVo = tantoDao.FindByPk(_tantoVo.HoyouEventCode, _tantoVo.HoyouBukaCode, _
                                            _tantoVo.HoyouTanto, _tantoVo.HoyouTantoKaiteiNo)
            End If
            tantoVo.Jyoutai = IIf(isRegister, THoyouSekkeiTantoVoHelper.Jyoutai.REGISTER, THoyouSekkeiTantoVoHelper.Jyoutai.SAVE)

            '--------------------------------------------------------
            '   改訂№：000の場合は内容を固定
            If tantoVo.HoyouTantoKaiteiNo = "000" Then
                tantoVo.KaiteiNaiyou = "イニシャル"
            Else
                tantoVo.KaiteiNaiyou = Naiyou
            End If
            '--------------------------------------------------------

            tantoVo.Memo = Memo
            tantoVo.SaisyuKoushinbi = DateUtil.ConvDateToIneteger(aShisakuDate.CurrentDateTime)
            tantoVo.SaisyuKoushinjikan = DateUtil.ConvTimeToIneteger(aShisakuDate.CurrentDateTime)

            tantoVo.UpdatedUserId = aLoginInfo.UserId
            tantoVo.UpdatedDate = aShisakuDate.CurrentDateDbFormat
            tantoVo.UpdatedTime = aShisakuDate.CurrentTimeDbFormat

            '追加モードの場合、インサートする。
            If IsAddMode() Then
                tantoDao.InsertBy(tantoVo)
            Else
                tantoDao.UpdateByPk(tantoVo)
            End If

            _tantoVo = tantoVo
            _IsAddMode = False
        End Sub

        ''' <summary>
        ''' 補用担当者表示順の最大値を参照して返す
        ''' </summary>
        ''' <param name="hoyouEventCode">補用イベントコード</param>
        ''' <param name="hoyouBukaCode">補用部課コード</param>
        ''' <returns>補用担当者表示順の最大値</returns>
        ''' <remarks></remarks>
        Private Function FindMaxHoyouTantoHyoujiJun(ByVal hoyouEventCode As String, ByVal hoyouBukaCode As String)
            Dim param As New THoyouSekkeiTantoVo
            param.HoyouEventCode = hoyouEventCode
            param.HoyouBukaCode = hoyouBukaCode
            Dim vos As List(Of THoyouSekkeiTantoVo) = tantoDao.FindBy(param)
            Dim result As Integer = -1
            For Each vo As THoyouSekkeiTantoVo In vos
                If vo.HoyouTantoHyoujiJun IsNot Nothing AndAlso result < CInt(vo.HoyouTantoHyoujiJun) Then
                    result = CInt(vo.HoyouTantoHyoujiJun)
                End If
            Next
            Return result
        End Function

        ''' <summary>
        ''' 画面で選択中の担当者を、実際の値として、反映する
        ''' </summary>
        ''' <remarks></remarks>
        Public Sub ApplyTanto()
            _hoyouTantoNo = _hoyouTantoNoSelectedValue
            Load()
        End Sub

    End Class
End Namespace
