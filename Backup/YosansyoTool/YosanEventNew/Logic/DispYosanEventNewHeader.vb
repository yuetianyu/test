Imports EBom.Data
Imports EBom.Common
Imports ShisakuCommon
Imports ShisakuCommon.Db.EBom.Dao
Imports ShisakuCommon.Db.EBom.Vo
Imports ShisakuCommon.Util
Imports ShisakuCommon.Util.LabelValue
Imports ShisakuCommon.Ui
Imports YosansyoTool.YosanEventNew.Dao

Namespace YosanEventNew.Logic
    Public Class DispYosanEventNewHeader : Inherits Observable

        Private ReadOnly login As LoginInfo
        Private ReadOnly aKaihatufugoDao As MShisakuKaihatufugoDao
        Private ReadOnly yosanEventDao As TYosanEventDao
        Private ReadOnly aYosanEventNewDao As YosanEventNewDao
        Private ReadOnly aDate As ShisakuDate

#Region "公開プロパティ値"
        '' イベントフェーズ
        Private _YosanEventPhase As String
        '' イベント
        Private _YosanEvent As String
        '' 開発符号
        Private _KaihatsuFugo As String
        '' 区分
        Private _Kubun As String
        '' イベント名
        Private _EventName As String
        '' 予算コード
        Private _yosanCode As String
        '' 主な変更概要
        Private _yosanMainHenkoGaiyo As String
        '' 造り方及び製作条件
        Private _yosanTsukurikataSeisakujyoken As String
        '' その他
        Private _yosanSonota As String

        Private _eventCode As String

#End Region

#Region "公開プロパティGet/Set"

        ''' <summary>イベントフェーズ</summary>
        ''' <value>イベントフェーズ</value>
        ''' <returns>イベントフェーズ</returns>
        Public Property YosanEventPhase() As String
            Get
                Return _YosanEventPhase
            End Get
            Set(ByVal value As String)
                If EzUtil.IsEqualIfNull(_YosanEventPhase, value) Then
                    Return
                End If
                _YosanEventPhase = value
                SetChanged()
            End Set
        End Property

        ''' <summary>イベント</summary>
        ''' <returns>イベント</returns>
        Public Property YosanEvent() As String
            Get
                Return _YosanEvent
            End Get
            Set(ByVal value As String)
                If EzUtil.IsEqualIfNull(_YosanEvent, value) Then
                    Return
                End If
                _YosanEvent = value
                SetChanged()
            End Set
        End Property

        ''' <summary>開発符号</summary>
        ''' <value>開発符号</value>
        ''' <returns>開発符号</returns>
        Public Property KaihatsuFugo() As String
            Get
                Return _KaihatsuFugo
            End Get
            Set(ByVal value As String)
                If EzUtil.IsEqualIfNull(_KaihatsuFugo, value) Then
                    Return
                End If
                _KaihatsuFugo = value
                SetChanged()
            End Set
        End Property

        ''' <summary>区分</summary>
        ''' <value>区分</value>
        ''' <returns>区分</returns>
        Public Property Kubun() As String
            Get
                Return _Kubun
            End Get
            Set(ByVal value As String)
                If EzUtil.IsEqualIfNull(_Kubun, value) Then
                    Return
                End If
                _Kubun = value
                SetChanged()
            End Set
        End Property

        ''' <summary>イベント名</summary>
        ''' <value>イベント名</value>
        ''' <returns>イベント名</returns>
        Public Property EventName() As String
            Get
                Return _EventName
            End Get
            Set(ByVal value As String)
                If EzUtil.IsEqualIfNull(_EventName, value) Then
                    Return
                End If
                _EventName = value
                SetChanged()
            End Set
        End Property

        ''' <summary>予算コード</summary>
        ''' <value>予算コード</value>
        ''' <returns>予算コード</returns>
        Public Property yosanCode() As String
            Get
                Return _yosanCode
            End Get
            Set(ByVal value As String)
                If EzUtil.IsEqualIfNull(_yosanCode, value) Then
                    Return
                End If
                _yosanCode = value
                SetChanged()
            End Set
        End Property

        ''' <summary>主な変更概要</summary>
        ''' <value>主な変更概要</value>
        ''' <returns>主な変更概要</returns>
        Public Property yosanMainHenkoGaiyo() As String
            Get
                Return _yosanMainHenkoGaiyo
            End Get
            Set(ByVal value As String)
                If EzUtil.IsEqualIfNull(_yosanMainHenkoGaiyo, value) Then
                    Return
                End If
                _yosanMainHenkoGaiyo = value
                SetChanged()
            End Set
        End Property

        ''' <summary>造り方及び製作条件</summary>
        ''' <value>造り方及び製作条件</value>
        ''' <returns>造り方及び製作条件</returns>
        Public Property yosanTsukurikataSeisakujyoken() As String
            Get
                Return _yosanTsukurikataSeisakujyoken
            End Get
            Set(ByVal value As String)
                If EzUtil.IsEqualIfNull(_yosanTsukurikataSeisakujyoken, value) Then
                    Return
                End If
                _yosanTsukurikataSeisakujyoken = value
                SetChanged()
            End Set
        End Property

        ''' <summary>その他</summary>
        ''' <returns>その他</returns>
        Public Property yosanSonota() As String
            Get
                Return _yosanSonota
            End Get
            Set(ByVal value As String)
                If EzUtil.IsEqualIfNull(_yosanSonota, value) Then
                    Return
                End If
                _yosanSonota = value
                SetChanged()
            End Set
        End Property
#End Region

#Region "公開プロパティの選択値"
        '' 開発符号の選択値
        Private _KaihatsuFugoLabelValues As List(Of LabelValueVo)
        '' イベントの選択値
        Private _EventCodeLabelValues As List(Of LabelValueVo)
        '' 区分の選択値
        Private _KubunLabelValues As List(Of LabelValueVo)

        ''' <summary>開発符号の選択値</summary>
        ''' <value>開発符号の選択値</value>
        ''' <returns>開発符号の選択値</returns>
        Public Property KaihatsuFugoLabelValues() As List(Of LabelValueVo)
            Get
                Return _KaihatsuFugoLabelValues
            End Get
            Set(ByVal value As List(Of LabelValueVo))
                _KaihatsuFugoLabelValues = value
            End Set
        End Property

        ''' <summary>イベントの選択値</summary>
        ''' <value>イベントの選択値</value>
        ''' <returns>イベントの選択値</returns>
        Public Property EventCodeLabelValues() As List(Of LabelValueVo)
            Get
                Return _EventCodeLabelValues
            End Get
            Set(ByVal value As List(Of LabelValueVo))
                _EventCodeLabelValues = value
            End Set
        End Property

        ''' <summary>区分の選択値</summary>
        ''' <value>区分の選択値</value>
        ''' <returns>区分の選択値</returns>
        Public Property KubunLabelValues() As List(Of LabelValueVo)
            Get
                Return _KubunLabelValues
            End Get
            Set(ByVal value As List(Of LabelValueVo))
                _KubunLabelValues = value
            End Set
        End Property

#End Region

        ''' <summary>
        ''' コンストラクタ
        ''' </summary>
        ''' <param name="aEventCode">イベント</param>
        ''' <param name="aLogin">ログイン情報</param>
        ''' <param name="aKaihatufugoDao"></param>
        ''' <param name="aEventDao"></param>
        ''' <param name="aYosanEventNewDao"></param>
        ''' <param name="aDate"></param>
        ''' <remarks></remarks>
        Public Sub New(ByVal aEventCode As String, _
                       ByVal aLogin As LoginInfo, _
                       ByVal aKaihatufugoDao As MShisakuKaihatufugoDao, _
                       ByVal aEventDao As TYosanEventDao, _
                       ByVal aYosanEventNewDao As YosanEventNewDao, _
                       ByVal aDate As ShisakuDate)

            Me._eventCode = aEventCode
            Me.login = aLogin
            Me.aKaihatufugoDao = aKaihatufugoDao
            Me.yosanEventDao = aEventDao
            Me.aYosanEventNewDao = aYosanEventNewDao
            Me.aDate = aDate

            '区分
            KubunLabelValues = GetLabelValues_kubun()
            '開発符号
            KaihatsuFugoLabelValues = GetLabelValues_ShisakuKaihatuFugo()

            ''イベント名
            'GenchoEventNameLabelValues = GetLabelValues_OrderSheetEventName()

            If _eventCode Is Nothing OrElse StringUtil.IsEmpty(_eventCode) Then
                ''一般納期日付
                '_GenchoIpanNouki = aDate.CurrentDateTime
                ''支給納期日付
                '_GenchoShikyuNouki = aDate.CurrentDateTime

            End If

            SetChanged()

        End Sub

#Region " 区分取得 "
        '固定値。
        Public Function GetLabelValues_Kubun() As List(Of LabelValueVo)
            Dim results As New List(Of LabelValueVo)
            results.Add(New LabelValueVo("本体", "本体"))
            results.Add(New LabelValueVo("先開", "先開"))
            results.Add(New LabelValueVo("その他", "その他"))
            Return results
        End Function

#End Region

#Region " 開発符号取得 "
        '試作手配システムのマスタから開発符号を取得する。
        Public Function GetLabelValues_ShisakuKaihatuFugo() As List(Of LabelValueVo)
            Dim results As List(Of LabelValueVo) = _
                LabelValueExtracter(Of MShisakuKaihatufugoVo).Extract(aKaihatufugoDao.FindByAll(), New KaihatufugoExtraction)
            results.Sort(New LabelValueComparer)
            Return results
        End Function

        Private Class KaihatufugoExtraction : Implements ILabelValueExtraction
            Public Sub Extraction(ByVal aLocator As ShisakuCommon.Util.LabelValue.LabelValueLocator) Implements ShisakuCommon.Util.LabelValue.ILabelValueExtraction.Extraction
                Dim vo As New MShisakuKaihatufugoVo
                aLocator.IsA(vo).Label(vo.ShisakuKaihatsuFugo).Value(vo.ShisakuKaihatsuFugo)
            End Sub
        End Class
#End Region

#Region " イベント取得 "
        Public Function GetLabelValues_Event(ByVal kaihatsuFugo As String) As List(Of LabelValueVo)
            Dim ShisakuKaihatufugoVo As New MShisakuKaihatufugoVo
            ShisakuKaihatufugoVo.ShisakuKaihatsuFugo = kaihatsuFugo
            Dim results As List(Of LabelValueVo) = _
                LabelValueExtracter(Of MShisakuKaihatufugoVo).Extract(aKaihatufugoDao.FindBy(ShisakuKaihatufugoVo), New ListCodeExtraction)
            results.Sort(New LabelValueComparer)
            Return results
        End Function

        Private Class ListCodeExtraction : Implements ILabelValueExtraction
            Public Sub Extraction(ByVal aLocator As ShisakuCommon.Util.LabelValue.LabelValueLocator) Implements ShisakuCommon.Util.LabelValue.ILabelValueExtraction.Extraction
                Dim vo As New MShisakuKaihatufugoVo
                aLocator.IsA(vo).Label(vo.ShisakuEventPhaseName).Value(vo.ShisakuEventPhase)
            End Sub
        End Class
#End Region

#Region " 登録／保存 "
        ''' <summary>
        ''' 登録処理
        ''' </summary>
        ''' <remarks></remarks>
        Public Sub Register()
            RegisterMain(True)
        End Sub

        ''' <summary>
        ''' 保存処理
        ''' </summary>
        ''' <remarks></remarks>
        Public Sub Save()
            RegisterMain(False)
        End Sub

        Private Sub RegisterMain(ByVal isRegister As Boolean)

            Dim value As TYosanEventVo
            If isRegister Then

                value = New TYosanEventVo

            End If           

            Dim para As New TYosanEventVo
            para.YosanEvent = YosanEventPhase
            para.YosanKaihatsuFugo = KaihatsuFugo

            value = aYosanEventNewDao.FindMaxEventCode(para)

            If value.YosanEventCode Is Nothing Then
                '予算イベントコード
                value.YosanEventCode = KaihatsuFugo + "-" + YosanEventPhase + "-" + "01"
            Else
                Dim index As Integer = CInt(value.YosanEventCode.Substring(value.YosanEventCode.LastIndexOf("-") + 1))
                If index <= 9 Then
                    '予算イベントコード
                    value.YosanEventCode = KaihatsuFugo + "-" + YosanEventPhase + "-" + "0" + (index + 1).ToString
                ElseIf index > 9 Then
                    '予算イベントコード
                    value.YosanEventCode = KaihatsuFugo + "-" + YosanEventPhase + "-" + index.ToString
                End If
            End If

            '区分
            value.YosanEventKbnName = Kubun
            '開発符号
            value.YosanKaihatsuFugo = KaihatsuFugo
            'イベント
            value.YosanEvent = YosanEvent
            'イベント名称
            value.YosanEventName = EventName
            '予算コード
            value.YosanCode = yosanCode
            '主な変更概要
            value.YosanMainHenkoGaiyo = yosanMainHenkoGaiyo
            '造り方及び製作条件
            value.YosanTsukurikataSeisakujyoken = yosanTsukurikataSeisakujyoken
            'その他aYosanEventNewDao
            value.YosanSonota = yosanSonota
            'ステータス
            value.YosanStatus = "00"
            'イベント登録日
            value.YosanEventTourokubi = aDate.CurrentDateTime.ToString("yyyyMMdd")

            If isRegister Then
                value.CreatedUserId = login.UserId
                value.CreatedDate = aDate.CurrentDateDbFormat
                value.CreatedTime = aDate.CurrentTimeDbFormat
            End If
            value.UpdatedUserId = login.UserId
            value.UpdatedDate = aDate.CurrentDateDbFormat
            value.UpdatedTime = aDate.CurrentTimeDbFormat

            If isRegister Then
                yosanEventDao.InsertBy(value)
            Else
                yosanEventDao.UpdateByPk(value)
            End If

        End Sub
#End Region

    End Class

End Namespace