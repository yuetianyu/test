Imports ShisakuCommon.Db.EBom.Vo.Helper
Imports ShisakuCommon.Db
Imports ShisakuCommon.Util.LabelValue
Imports ShisakuCommon.Db.EBom.Dao
Imports ShisakuCommon
Imports ShisakuCommon.Util
Imports ShisakuCommon.Db.EBom.Vo

Namespace ShisakuBuhinEdit.Logic

    ''' <summary>
    ''' 試作部品表編集画面（ヘッダー部）の表示全般を担うクラス
    ''' </summary>
    ''' <remarks></remarks>
    Public Class BuhinEditHeaderSubject : Inherits Observable
        Private ReadOnly _shisakuEventCode As String
        Private ReadOnly _shisakuBukaCode As String
        Private ReadOnly aLoginInfo As LoginInfo
        Private ReadOnly eventDao As TShisakuEventDao
        Private ReadOnly blockDao As TShisakuSekkeiBlockDao
        Private ReadOnly aRhac1560Dao As Rhac1560Dao
        Private ReadOnly aRhac2130Dao As Rhac2130Dao
        Private ReadOnly aShisakuDao As ShisakuDao
        Private ReadOnly aShisakuDate As ShisakuDate
        Private ReadOnly telNoDao As TShisakuTelNoDao
        Private KSMode As Integer

        ''' <summary>
        ''' コンストラクタ
        ''' </summary>
        ''' <param name="shisakuEventCode">試作イベントコード</param>
        ''' <param name="shisakuBukaCode">試作部課コード</param>
        ''' <param name="shisakuBlockNo">試作ブロックNo</param>
        ''' <param name="aLoginInfo">ログイン情報</param>
        ''' <param name="aShisakuDate">試作日付</param>
        ''' <param name="eventDao">試作イベント情報Dao</param>
        ''' <param name="blockDao">試作設計ブロック情報Dao</param>
        ''' <param name="aRhac1560Dao">課マスタDao</param>
        ''' <param name="aRhac2130Dao">社員マスタDao</param>
        ''' <param name="aShisakuDao">試作システムDao</param>
        ''' <remarks></remarks>
        Public Sub New(ByVal shisakuEventCode As String, _
                       ByVal shisakuBukaCode As String, _
                       ByVal shisakuBlockNo As String, _
                       ByVal aLoginInfo As LoginInfo, _
                       ByVal aShisakuDate As ShisakuDate, _
                       ByVal eventDao As TShisakuEventDao, _
                       ByVal blockDao As TShisakuSekkeiBlockDao, _
                       ByVal aRhac1560Dao As Rhac1560Dao, _
                       ByVal aRhac2130Dao As Rhac2130Dao, _
                       ByVal aShisakuDao As ShisakuDao, _
                       ByVal KSMode As Integer, _
                       ByVal telNoDao As TShisakuTelNoDao)
            Me._shisakuEventCode = shisakuEventCode
            Me._shisakuBukaCode = shisakuBukaCode
            Me._shisakuBlockNo = shisakuBlockNo
            Me.aLoginInfo = aLoginInfo
            Me.eventDao = eventDao
            Me.blockDao = blockDao
            Me.aRhac1560Dao = aRhac1560Dao
            Me.aShisakuDate = aShisakuDate
            Me.aShisakuDao = aShisakuDao
            Me.aRhac2130Dao = aRhac2130Dao
            Me.KSMode = KSMode
            Me.telNoDao = telNoDao
            Load()
        End Sub

#Region "公開プロパティ"
        '試作開発符号
        Private _shisakuKaihatsuFugo As String
        '試作イベント名称
        Private _shisakuEventName As String
        '担当設計
        Private _shisakuBukaName As String
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
        'ブロックの改訂№
        Private _kaiteiNo As String
        '--------------------------

        'ブロック№
        Private _shisakuBlockNo As String
        'ブロック№(画面選択値)
        Private _shisakuBlockNoSelectedValue As String
        ' 一時保存のメモ
        Private _memo As String

        ''' <summary>試作開発符号</summary>
        ''' <value>試作開発符号</value>
        ''' <returns>試作開発符号</returns>
        Public Property ShisakuKaihatsuFugo() As String
            Get
                Return _shisakuKaihatsuFugo
            End Get
            Set(ByVal value As String)
                _shisakuKaihatsuFugo = value
            End Set
        End Property

        ''' <summary>試作イベント名称</summary>
        ''' <value>試作イベント名称</value>
        ''' <returns>試作イベント名称</returns>
        Public Property ShisakuEventName() As String
            Get
                Return _shisakuEventName
            End Get
            Set(ByVal value As String)
                _shisakuEventName = value
            End Set
        End Property

        ''' <summary>担当設計</summary>
        ''' <value>担当設計</value>
        ''' <returns>担当設計</returns>
        Public Property ShisakuBukaName() As String
            Get
                Return _shisakuBukaName
            End Get
            Set(ByVal value As String)
                _shisakuBukaName = value
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

        ''' <summary>ブロック№</summary>
        ''' <value>ブロック№</value>
        ''' <returns>ブロック№</returns>
        Public Property ShisakuBlockNo() As String
            Get
                Return _shisakuBlockNo
            End Get
            Set(ByVal value As String)
                _shisakuBlockNo = value
            End Set
        End Property

        ''' <summary>ブロック№(画面選択値)</summary>
        ''' <value>ブロック№(画面選択値)</value>
        ''' <returns>ブロック№(画面選択値)</returns>
        Public Property ShisakuBlockNoSelectedValue() As String
            Get
                Return _shisakuBlockNoSelectedValue
            End Get
            Set(ByVal value As String)
                _shisakuBlockNoSelectedValue = value
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
        '' ブロックNoの選択値
        Private _blockNoLabelValues As List(Of LabelValueVo)

        ''' <summary>ブロックNoの選択値</summary>
        ''' <value>ブロックNoの選択値</value>
        ''' <returns>ブロックNoの選択値</returns>
        Public Property BlockNoLabelValues() As List(Of LabelValueVo)
            Get
                Return _blockNoLabelValues
            End Get
            Set(ByVal value As List(Of LabelValueVo))
                _blockNoLabelValues = value
            End Set
        End Property
#End Region

        Private eventVo As TShisakuEventVo
        Private _blockVo As TShisakuSekkeiBlockVo
        ''' <summary>ブロック情報（キー情報としても有効）</summary>
        Public ReadOnly Property BlockVo() As TShisakuSekkeiBlockVo
            Get
                Return _blockVo
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
            eventVo = eventDao.FindByPk(_shisakuEventCode)
            If eventVo Is Nothing Then
                Throw New RecordNotFoundException(String.Format("試作イベント情報が見つかりません. (ShisakuEventCode) = ({0})", _shisakuEventCode))
            End If

            _blockVo = FindNewestBlockBy()
            'ブロック情報があるかどうかが判断条件だとマズイ
            If _blockVo Is Nothing Then
                _IsAddMode = True
                ' 「登録モード」でも他の協調クラスが、BlockVoのキー値を参照するので作成する
                _blockVo = New TShisakuSekkeiBlockVo
                _blockVo.ShisakuEventCode = _shisakuEventCode
                _blockVo.ShisakuBukaCode = _shisakuBukaCode
                _blockVo.ShisakuBlockNo = _shisakuBlockNo
                _blockVo.ShisakuBlockNoKaiteiNo = TShisakuSekkeiBlockVoHelper.ShisakuBlockNoKaiteiNo.DEFAULT_VALUE

                _userId = aLoginInfo.UserId
            Else
                _IsAddMode = False
                '_userId = _blockVo.UserId
                _userId = aLoginInfo.UserId
            End If

            '現在ログインしている人の部課コードで検索する
            'Dim bukaVo As Rhac1560Vo = aRhac1560Dao.FindByPk(_shisakuBukaCode.Substring(0, 2), Rhac1560VoHelper.SiteKbn.Gumma, _shisakuBukaCode.Substring(2))
            Dim bukaVo As Rhac1560Vo = aRhac1560Dao.FindByPk(LoginInfo.Now.BuCode, Rhac1560VoHelper.SiteKbn.Gumma, LoginInfo.Now.KaCode)
            _shisakuKaihatsuFugo = eventVo.ShisakuKaihatsuFugo
            _shisakuEventName = eventVo.ShisakuEventName

            If bukaVo Is Nothing Then
                _shisakuBukaName = LoginInfo.Now.BukaRyakuName
            Else
                _shisakuBukaName = bukaVo.KaRyakuName
            End If

            Dim user2130Vo As Rhac2130Vo = aRhac2130Dao.FindByPk(_userId)
            Dim userVo As Rhac0650Vo = aShisakuDao.FindUserById(_userId)
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
                If StringUtil.IsEmpty(_blockVo.TelNo) Then
                    'If user2130Vo Is Nothing Then
                    '    _telNo = String.Empty
                    'Else
                    '    _telNo = user2130Vo.NaisenNo
                    'End If
                    _memo = _blockVo.Memo
                    _naiyou = _blockVo.KaiteiNaiyou '２次改修で追加
                Else
                    _telNo = _blockVo.TelNo
                    _memo = _blockVo.Memo
                    _naiyou = _blockVo.KaiteiNaiyou '２次改修で追加
                End If
            End If
            If StringUtil.IsEmpty(_blockVo.TelNo) Then
                If telVo Is Nothing Then
                    _telNo = String.Empty
                Else
                    _telNo = telVo.TelNo
                End If
            Else
                _telNo = _blockVo.TelNo
            End If

            '---------------------------------------------
            '２次改修で追加
            '   改訂№を返す。
            _kaiteiNo = _blockVo.ShisakuBlockNoKaiteiNo
            '---------------------------------------------

            BlockNoLabelValues = GetLabelValues_BlockNo()

            SetChanged()
        End Sub

        ''' <summary>
        ''' 最新のブロックNo改訂Noをもつ試作設計ブロック情報を参照する
        ''' </summary>
        ''' <returns>試作設計ブロック情報</returns>
        ''' <remarks></remarks>
        Private Function FindNewestBlockBy() As TShisakuSekkeiBlockVo

            Dim param As New TShisakuSekkeiBlockVo
            param.ShisakuEventCode = _shisakuEventCode
            param.ShisakuBukaCode = _shisakuBukaCode
            param.ShisakuBlockNo = _shisakuBlockNo
            Dim blockVos As List(Of TShisakuSekkeiBlockVo) = blockDao.FindBy(param)
            If blockVos.Count = 0 Then
                Return Nothing
            End If
            blockVos.Sort(New BlockNoKaiteiNoComparer)
            Return blockVos(blockVos.Count - 1)
        End Function

        ''' <summary>
        ''' ブロックNo改訂Noでソートする IComperer実装クラス
        ''' </summary>
        ''' <remarks></remarks>
        Private Class BlockNoKaiteiNoComparer : Implements IComparer(Of TShisakuSekkeiBlockVo)
            Public Function Compare(ByVal x As ShisakuCommon.Db.EBom.Vo.TShisakuSekkeiBlockVo, ByVal y As ShisakuCommon.Db.EBom.Vo.TShisakuSekkeiBlockVo) As Integer Implements System.Collections.Generic.IComparer(Of ShisakuCommon.Db.EBom.Vo.TShisakuSekkeiBlockVo).Compare
                If x.ShisakuBlockNoKaiteiNo Is Nothing AndAlso y.ShisakuBlockNoKaiteiNo Is Nothing Then
                    Return 0
                ElseIf y.ShisakuBlockNoKaiteiNo Is Nothing Then
                    Return 1
                ElseIf x.ShisakuBlockNoKaiteiNo Is Nothing Then
                    Return -1
                End If
                Return CInt(x.ShisakuBlockNoKaiteiNo).CompareTo(CInt(y.ShisakuBlockNoKaiteiNo))
            End Function
        End Class

        ''' <summary>
        ''' ブロックNoのLabelValueを抽出する ILabelValueExtraction実装クラス
        ''' </summary>
        ''' <remarks></remarks>
        Private Class BlockNoExtraction : Implements ILabelValueExtraction
            Public Sub Extraction(ByVal aLocator As ShisakuCommon.Util.LabelValue.LabelValueLocator) Implements ShisakuCommon.Util.LabelValue.ILabelValueExtraction.Extraction
                Dim vo As New TShisakuSekkeiBlockVo
                aLocator.IsA(vo).Label(vo.ShisakuBlockNo).Value(vo.ShisakuBlockNo)
            End Sub
        End Class

        ''' <summary>ブロックNo表示順でソート</summary>
        Private Class BlockNoComparer : Implements IComparer(Of TShisakuSekkeiBlockVo)
            Public Function Compare(ByVal x As ShisakuCommon.Db.EBom.Vo.TShisakuSekkeiBlockVo, ByVal y As ShisakuCommon.Db.EBom.Vo.TShisakuSekkeiBlockVo) As Integer Implements System.Collections.Generic.IComparer(Of ShisakuCommon.Db.EBom.Vo.TShisakuSekkeiBlockVo).Compare
                If x.ShisakuBlockNoHyoujiJun Is Nothing AndAlso y.ShisakuBlockNoHyoujiJun Is Nothing Then
                    Return 0
                ElseIf y.ShisakuBlockNoHyoujiJun Is Nothing Then
                    Return 1
                ElseIf x.ShisakuBlockNoHyoujiJun Is Nothing Then
                    Return -1
                End If
                Return CInt(x.ShisakuBlockNoHyoujiJun).CompareTo(CInt(y.ShisakuBlockNoHyoujiJun))
            End Function
        End Class

        ''' <summary>
        ''' ブロックNoのLabelValueVoを参照する
        ''' </summary>
        ''' <returns>ブロックNoの一覧</returns>
        ''' <remarks></remarks>
        Private Function GetLabelValues_BlockNo() As List(Of LabelValueVo)
            Dim param As New TShisakuSekkeiBlockVo
            param.ShisakuEventCode = _shisakuEventCode
            param.ShisakuBukaCode = _shisakuBukaCode
            Dim blockVos As List(Of TShisakuSekkeiBlockVo) = blockDao.FindBy(param)
            blockVos.Sort(New BlockNoComparer)
            Dim results As List(Of LabelValueVo) = LabelValueExtracter(Of TShisakuSekkeiBlockVo).Extract(blockVos, New BlockNoExtraction)
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
            Dim blockVo As TShisakuSekkeiBlockVo
            If IsAddMode() Then
                blockVo = _blockVo
                '改訂編集からきた場合、改訂は001からはじめる'
                If KSMode = 1 AndAlso StringUtil.Equals(blockVo.ShisakuBlockNoKaiteiNo, "000") Then
                    blockVo.ShisakuBlockNoKaiteiNo = "001"
                End If

                blockVo.ShisakuBlockNoHyoujiJun = FindMaxShisakuBlockNoHyoujiJun(_shisakuEventCode, _shisakuBukaCode) + 1
                blockVo.BlockFuyou = TShisakuSekkeiBlockVoHelper.BlockFuyou.NECESSARY
                blockVo.UnitKbn = eventVo.UnitKbn
                blockVo.ShisakuBlockName = Nothing
                blockVo.CreatedUserId = aLoginInfo.UserId
                blockVo.CreatedDate = aShisakuDate.CurrentDateDbFormat
                blockVo.CreatedTime = aShisakuDate.CurrentTimeDbFormat
            Else
                blockVo = blockDao.FindByPk(_blockVo.ShisakuEventCode, _blockVo.ShisakuBukaCode, _blockVo.ShisakuBlockNo, _blockVo.ShisakuBlockNoKaiteiNo)
            End If
            blockVo.Jyoutai = IIf(isRegister, TShisakuSekkeiBlockVoHelper.Jyoutai.REGISTER, TShisakuSekkeiBlockVoHelper.Jyoutai.SAVE)
            blockVo.UserId = UserId
            blockVo.TelNo = TelNo

            '--------------------------------------------------------
            '２次改修で追加
            '   改訂№：000の場合は内容を固定
            If blockVo.ShisakuBlockNoKaiteiNo = "000" Then
                blockVo.KaiteiNaiyou = "イニシャル" '２次改修で追加
            Else
                blockVo.KaiteiNaiyou = Naiyou '２次改修で追加
            End If
            '--------------------------------------------------------

            blockVo.Memo = Memo
            blockVo.SaisyuKoushinbi = DateUtil.ConvDateToIneteger(aShisakuDate.CurrentDateTime)
            blockVo.SaisyuKoushinjikan = DateUtil.ConvTimeToIneteger(aShisakuDate.CurrentDateTime)

            blockVo.UpdatedUserId = aLoginInfo.UserId
            blockVo.UpdatedDate = aShisakuDate.CurrentDateDbFormat
            blockVo.UpdatedTime = aShisakuDate.CurrentTimeDbFormat

            '追加モードの場合、インサートする。
            If IsAddMode() Then
                blockDao.InsertBy(blockVo)
            Else
                blockDao.UpdateByPk(blockVo)
            End If
            Dim telVo As New TShisakuTelNoVo
            telVo = telNoDao.FindByPk(blockVo.UpdatedUserId)
            If telVo Is Nothing Then
                telVo = New TShisakuTelNoVo
                telVo.UserId = aLoginInfo.UserId
                telVo.TelNo = TelNo
                telNoDao.InsertBy(telVo)
            Else
                If telVo.UserId Is Nothing Then
                    telVo.UserId = aLoginInfo.UserId
                    telVo.TelNo = TelNo
                    telNoDao.InsertBy(telVo)
                Else
                    telVo.UserId = aLoginInfo.UserId
                    telVo.TelNo = TelNo
                    telNoDao.UpdateByPk(telVo)
                End If
            End If


            _blockVo = blockVo
            _IsAddMode = False
        End Sub

        ''' <summary>
        ''' 試作ブロックNo表示順の最大値を参照して返す
        ''' </summary>
        ''' <param name="shisakuEventCode">試作イベントコード</param>
        ''' <param name="shisakuBukaCode">試作部課コード</param>
        ''' <returns>試作ブロックNo表示順の最大値</returns>
        ''' <remarks></remarks>
        Private Function FindMaxShisakuBlockNoHyoujiJun(ByVal shisakuEventCode As String, ByVal shisakuBukaCode As String)
            Dim param As New TShisakuSekkeiBlockVo
            param.ShisakuEventCode = shisakuEventCode
            param.ShisakuBukaCode = shisakuBukaCode
            Dim vos As List(Of TShisakuSekkeiBlockVo) = blockDao.FindBy(param)
            Dim result As Integer = -1
            For Each vo As TShisakuSekkeiBlockVo In vos
                If vo.ShisakuBlockNoHyoujiJun IsNot Nothing AndAlso result < CInt(vo.ShisakuBlockNoHyoujiJun) Then
                    result = CInt(vo.ShisakuBlockNoHyoujiJun)
                End If
            Next
            Return result
        End Function

        ''' <summary>
        ''' 画面で選択中のブロック№を、実際の値として、反映する
        ''' </summary>
        ''' <remarks></remarks>
        Public Sub ApplyBlockNo()
            _shisakuBlockNo = _shisakuBlockNoSelectedValue
            Load()
        End Sub

    End Class
End Namespace
