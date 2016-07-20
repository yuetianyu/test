Imports ShisakuCommon.Db.EBom.Vo.Helper
Imports ShisakuCommon
Imports ShisakuCommon.Util.LabelValue
Imports ShisakuCommon.Db.EBom.Dao
Imports ShisakuCommon.Db.EBom.Vo
Imports ShisakuCommon.Util
Imports EventSakusei.EventEdit.Dao

Namespace ShisakuBuhinEditBlock.Logic
    Public Class EventEditHeader : Inherits Observable

        Private ReadOnly login As LoginInfo
        Private ReadOnly aKaihatufugoDao As MShisakuKaihatufugoDao
        Private ReadOnly aEventDao As TShisakuEventDao
        Private ReadOnly aStatusDao As MShisakuStatusDao
        Private ReadOnly aDate As ShisakuDate
#Region "公開プロパティ値"
        '' 試作イベントコード
        Private _shisakuEventCode As String
        '' 試作開発符号  
        Private _ShisakuKaihatsuFugo As String
        '' 試作イベントフェーズ名  
        Private _ShisakuEventPhaseName As String
        '' 試作イベント名称  
        Private _ShisakuEventName As String
        '' ユニット区分  
        Private _UnitKbn As String
        '' 製作次期FROM  
        Private _SeisakujikiFrom As DateTime?
        '' 製作次期TO  
        Private _SeisakujikiTo As DateTime?
        '' 製作台数・完成車  
        Private _SeisakudaisuKanseisya As String
        '' 製作台数・W/B  
        Private _SeisakudaisuWb As String
        '' 製作台数・製作中止  
        Private _SeisakudaisuChushi As String
        '' 製作一覧発行№  
        Private _SeisakuichiranHakouNo As String
        '' 製作一覧発行№改訂  
        Private _SeisakuichiranHakouNoKai As String
        '' 発注の有無  
        Private _HachuUmu As String
        '' 自給品の有無
        Private _JikyuUmu As String     '2012/01/07
        '' 現在進捗状況
        Private _status As String
        '' 現在進捗状況名称
        Private _statusName As String
        '' 現在データ状況名称
        Private _dataKbnName As String
        '' 設計展開日  
        Private _SekkeiTenkaibi As String    '2012/01/10
        '' お知らせ通知フラグ  
        Private _InfoMailFlg As String    '2013/02/08
        '' 保存フラグ  
        Private _SaveFlg As String    '2013/02/08

#End Region

#Region "公開プロパティGet/Set"
        ''' <summary>試作イベントコード</summary>
        ''' <returns>試作イベントコード</returns>
        Public Property ShisakuEventCode() As String
            Get
                Return _shisakuEventCode
            End Get
            Set(ByVal value As String)
                _shisakuEventCode = value
            End Set
        End Property

        ''' <summary>試作開発符号</summary>
        ''' <value>試作開発符号</value>
        ''' <returns>試作開発符号</returns>
        Public Property ShisakuKaihatsuFugo() As String
            Get
                Return _ShisakuKaihatsuFugo
            End Get
            Set(ByVal value As String)
                If EzUtil.IsEqualIfNull(_ShisakuKaihatsuFugo, value) Then
                    Return
                End If
                _ShisakuKaihatsuFugo = value
                setChanged()

                ShisakuEventPhaseNameLabelValues = GetLabelValues_EventPhaseName()
            End Set
        End Property

        ''' <summary>試作イベントフェーズ名</summary>
        ''' <value>試作イベントフェーズ名</value>
        ''' <returns>試作イベントフェーズ名</returns>
        Public Property ShisakuEventPhaseName() As String
            Get
                Return _ShisakuEventPhaseName
            End Get
            Set(ByVal value As String)
                If EzUtil.IsEqualIfNull(_ShisakuEventPhaseName, value) Then
                    Return
                End If
                _ShisakuEventPhaseName = value
                setChanged()
            End Set
        End Property

        ''' <summary>試作イベント名称</summary>
        ''' <value>試作イベント名称</value>
        ''' <returns>試作イベント名称</returns>
        Public Property ShisakuEventName() As String
            Get
                Return _ShisakuEventName
            End Get
            Set(ByVal value As String)
                If EzUtil.IsEqualIfNull(_ShisakuEventName, value) Then
                    Return
                End If
                _ShisakuEventName = value
                setChanged()
            End Set
        End Property

        ''' <summary>ユニット区分</summary>
        ''' <value>ユニット区分</value>
        ''' <returns>ユニット区分</returns>
        Public Property UnitKbn() As String
            Get
                Return _UnitKbn
            End Get
            Set(ByVal value As String)
                If EzUtil.IsEqualIfNull(_UnitKbn, value) Then
                    Return
                End If
                _UnitKbn = value
                setChanged()
            End Set
        End Property

        ''' <summary>製作次期FROM</summary>
        ''' <value>製作次期FROM</value>
        ''' <returns>製作次期FROM</returns>
        Public Property SeisakujikiFrom() As DateTime?
            Get
                Return _SeisakujikiFrom
            End Get
            Set(ByVal value As DateTime?)
                If EzUtil.IsEqualIfNull(_SeisakujikiFrom, value) Then
                    Return
                End If
                _SeisakujikiFrom = value
                setChanged()
            End Set
        End Property

        ''' <summary>製作次期TO</summary>
        ''' <value>製作次期TO</value>
        ''' <returns>製作次期TO</returns>
        Public Property SeisakujikiTo() As DateTime?
            Get
                Return _SeisakujikiTo
            End Get
            Set(ByVal value As DateTime?)
                If EzUtil.IsEqualIfNull(_SeisakujikiTo, value) Then
                    Return
                End If
                _SeisakujikiTo = value
                setChanged()
            End Set
        End Property

        ''' <summary>製作台数・完成車</summary>
        ''' <value>製作台数・完成車</value>
        ''' <returns>製作台数・完成車</returns>
        Public Property SeisakudaisuKanseisya() As String
            Get
                Return _SeisakudaisuKanseisya
            End Get
            Set(ByVal value As String)
                If EzUtil.IsEqualIfNull(_SeisakudaisuKanseisya, value) Then
                    Return
                End If
                _SeisakudaisuKanseisya = value
                setChanged()
            End Set
        End Property

        ''' <summary>製作台数・W/B</summary>
        ''' <value>製作台数・W/B</value>
        ''' <returns>製作台数・W/B</returns>
        Public Property SeisakudaisuWb() As String
            Get
                Return _SeisakudaisuWb
            End Get
            Set(ByVal value As String)
                If EzUtil.IsEqualIfNull(_SeisakudaisuWb, value) Then
                    Return
                End If
                _SeisakudaisuWb = value
                setChanged()
            End Set
        End Property

        ''' <summary>製作台数・製作中止</summary>
        ''' <value>製作台数・製作中止</value>
        ''' <returns>製作台数・製作中止</returns>
        Public Property SeisakudaisuChushi() As String
            Get
                Return _SeisakudaisuChushi
            End Get
            Set(ByVal value As String)
                If EzUtil.IsEqualIfNull(_SeisakudaisuChushi, value) Then
                    Return
                End If
                _SeisakudaisuChushi = value
                setChanged()
            End Set
        End Property

        ''' <summary>製作一覧発行№</summary>
        ''' <value>製作一覧発行№</value>
        ''' <returns>製作一覧発行№</returns>
        Public Property SeisakuichiranHakouNo() As String
            Get
                Return _SeisakuichiranHakouNo
            End Get
            Set(ByVal value As String)
                If EzUtil.IsEqualIfNull(_SeisakuichiranHakouNo, value) Then
                    Return
                End If
                _SeisakuichiranHakouNo = value
                setChanged()
            End Set
        End Property

        ''' <summary>製作一覧発行№改訂</summary>
        ''' <value>製作一覧発行№改訂</value>
        ''' <returns>製作一覧発行№改訂</returns>
        Public Property SeisakuichiranHakouNoKai() As String
            Get
                Return _SeisakuichiranHakouNoKai
            End Get
            Set(ByVal value As String)
                If EzUtil.IsEqualIfNull(_SeisakuichiranHakouNoKai, value) Then
                    Return
                End If
                _SeisakuichiranHakouNoKai = value
                setChanged()
            End Set
        End Property

        ''' <summary>発注の有無</summary>
        ''' <value>発注の有無</value>
        ''' <returns>発注の有無</returns>
        Public Property HachuUmu() As String
            Get
                Return _HachuUmu
            End Get
            Set(ByVal value As String)
                If EzUtil.IsEqualIfNull(_HachuUmu, value) Then
                    Return
                End If
                _HachuUmu = value
                setChanged()
            End Set
        End Property

        '2012/01/07
        ''' <summary>自給品の有無</summary>
        ''' <value>自給品の有無</value>
        ''' <returns>自給品の有無</returns>
        Public Property JikyuUmu() As String
            Get
                Return _JikyuUmu
            End Get
            Set(ByVal value As String)
                If EzUtil.IsEqualIfNull(_JikyuUmu, value) Then
                    Return
                End If
                _JikyuUmu = value
                SetChanged()
            End Set
        End Property

        ''' <summary>現在進捗状況</summary>
        ''' <returns>現在進捗状況</returns>
        Public ReadOnly Property StatusName() As String
            Get
                Return _statusName
            End Get
        End Property

        ''' <summary>現在データ状況</summary>
        ''' <returns>現在データ状況</returns>
        Public Property DataKbnName() As String
            Get
                Return _dataKbnName
            End Get
            Set(ByVal value As String)
                _dataKbnName = value
            End Set
        End Property

        ''' <summary>使用可否情報</summary>
        ''' <returns>使用可否情報</returns>
        Public ReadOnly Property Enabled() As EventEditEnabled
            Get
                Return EventEditEnabled.Detect(_status, IsAddMode())
            End Get
        End Property

        '2012/01/10
        ''' <summary>設計展開日</summary>
        ''' <value>設計展開日</value>
        ''' <returns>設計展開日</returns>
        Public Property SekkeiTenkaibi() As String
            Get
                Return _SekkeiTenkaibi
            End Get
            Set(ByVal value As String)
                If EzUtil.IsEqualIfNull(_SekkeiTenkaibi, value) Then
                    Return
                End If
                _SekkeiTenkaibi = value
                SetChanged()
            End Set
        End Property

        '2013/02/08
        ''' <summary>お知らせ通知フラグ</summary>
        ''' <value>お知らせ通知フラグ</value>
        ''' <returns>お知らせ通知フラグ</returns>
        Public Property InfoMailFlg() As String
            Get
                Return _InfoMailFlg
            End Get
            Set(ByVal value As String)
                If EzUtil.IsEqualIfNull(_InfoMailFlg, value) Then
                    Return
                End If
                _InfoMailFlg = value
            End Set
        End Property
        ''' <summary>保存フラグ</summary>
        ''' <value>保存フラグ</value>
        ''' <returns>保存フラグ</returns>
        Public Property SaveFlg() As String
            Get
                Return _SaveFlg
            End Get
            Set(ByVal value As String)
                If EzUtil.IsEqualIfNull(_SaveFlg, value) Then
                    Return
                End If
                _SaveFlg = value
            End Set
        End Property

        Public ReadOnly Property IsSekkeiTenkaiIkou() As Boolean
            Get
                Select Case _status
                    Case TShisakuEventVoHelper.Status.SEKKEI_MAINTAINING, _
                            TShisakuEventVoHelper.Status.KAITEI_UKETSUKE_ING, _
                            TShisakuEventVoHelper.Status.KANRYO, _
                            TShisakuEventVoHelper.Status.CHUSHI_UKETSUKE_GO, _
                            TShisakuEventVoHelper.Status.CHUSHI_UKETSUKE_MAE
                        Return True
                End Select
                Return False
            End Get
        End Property
#End Region
#Region "公開プロパティの選択値"
        '' 試作開発符号の選択値
        Private _ShisakuKaihatsuFugoLabelValues As List(Of LabelValueVo)
        '' 試作イベントフェーズ名の選択値
        Private _ShisakuEventPhaseNameLabelValues As List(Of LabelValueVo)
        '' ユニット区分の選択値
        Private _UnitKbnLabelValues As List(Of LabelValueVo)
        '' 発注有無の選択値
        Private _HachuUmuLabelValues As List(Of LabelValueVo)
        '' 自給品の選択値
        Private _JikyuUmuLabelValues As List(Of LabelValueVo)   '2011/01/07

        ''' <summary>試作開発符号の選択値</summary>
        ''' <value>試作開発符号の選択値</value>
        ''' <returns>試作開発符号の選択値</returns>
        Public Property ShisakuKaihatsuFugoLabelValues() As List(Of LabelValueVo)
            Get
                Return _ShisakuKaihatsuFugoLabelValues
            End Get
            Set(ByVal value As List(Of LabelValueVo))
                _ShisakuKaihatsuFugoLabelValues = value
            End Set
        End Property

        ''' <summary>試作イベントフェーズ名の選択値</summary>
        ''' <value>試作イベントフェーズ名の選択値</value>
        ''' <returns>試作イベントフェーズ名の選択値</returns>
        Public Property ShisakuEventPhaseNameLabelValues() As List(Of LabelValueVo)
            Get
                Return _ShisakuEventPhaseNameLabelValues
            End Get
            Set(ByVal value As List(Of LabelValueVo))
                _ShisakuEventPhaseNameLabelValues = value
            End Set
        End Property

        ''' <summary>ユニット区分の選択値</summary>
        ''' <value>ユニット区分の選択値</value>
        ''' <returns>ユニット区分の選択値</returns>
        Public Property UnitKbnLabelValues() As List(Of LabelValueVo)
            Get
                Return _UnitKbnLabelValues
            End Get
            Set(ByVal value As List(Of LabelValueVo))
                _UnitKbnLabelValues = value
            End Set
        End Property

        ''' <summary>発注有無の選択値</summary>
        ''' <value>発注有無の選択値</value>
        ''' <returns>発注有無の選択値</returns>
        Public Property HachuUmuLabelValues() As List(Of LabelValueVo)
            Get
                Return _HachuUmuLabelValues
            End Get
            Set(ByVal value As List(Of LabelValueVo))
                _HachuUmuLabelValues = value
            End Set
        End Property

        '2012/01/07
        ''' <summary>自給品の選択値</summary>
        ''' <value>自給品の選択値</value>
        ''' <returns>自給品の選択値</returns>
        Public Property JikyuUmuLabelValues() As List(Of LabelValueVo)
            Get
                Return _JikyuUmuLabelValues
            End Get
            Set(ByVal value As List(Of LabelValueVo))
                _JikyuUmuLabelValues = value
            End Set
        End Property

#End Region

#Region "公開プロパティ"
        ''' 参照モードかを保持
        Private _isViewerMode As Boolean
        ''' <summary>参照モードか</summary>
        ''' <value>参照モードか</value>
        ''' <returns>参照モードか</returns>
        Public Property IsViewerMode() As Boolean
            Get
                Return _isViewerMode
            End Get
            Set(ByVal value As Boolean)
                If EzUtil.IsEqualIfNull(_isViewerMode, value) Then
                    Return
                End If
                _isViewerMode = value
                setChanged()
            End Set
        End Property
#End Region

        Public Sub New(ByVal shisakuEventCode As String, _
                       ByVal login As LoginInfo, _
                       ByVal aKaihatufugoDao As MShisakuKaihatufugoDao, _
                       ByVal aEventDao As TShisakuEventDao, _
                       ByVal aStatusDao As MShisakuStatusDao, _
                       ByVal aDate As ShisakuDate)

            _shisakuEventCode = shisakuEventCode
            Me.login = login
            Me.aKaihatufugoDao = aKaihatufugoDao
            Me.aEventDao = aEventDao
            Me.aStatusDao = aStatusDao
            Me.aDate = aDate

            ShisakuKaihatsuFugoLabelValues = GetLabelValues_ShisakuKaihatuFugo()

            UnitKbnLabelValues = TShisakuEventVoHelper.MakeUnitKbnLabeValues
            HachuUmuLabelValues = TShisakuEventVoHelper.MakeHachuUmuLabeValues()
            JikyuUmuLabelValues = TShisakuEventVoHelper.MakeJikyuUmuLabeValues()    '2012/01/07

            If IsAddMode() Then
                _SeisakujikiFrom = DateTime.Now
                _SeisakujikiTo = DateTime.Now
                _HachuUmu = TShisakuEventVoHelper.HachuUmu.ARI      '' デフォルトは「有」
                _JikyuUmu = TShisakuEventVoHelper.JikyuUmu.NASHI    '' デフォルトは「無」　2012/01/07

            Else
                Dim vo As TShisakuEventVo = Me.aEventDao.FindByPk(_shisakuEventCode)
                _ShisakuKaihatsuFugo = vo.ShisakuKaihatsuFugo
                ShisakuEventPhaseNameLabelValues = GetLabelValues_EventPhaseName()
                _ShisakuEventPhaseName = vo.ShisakuEventPhaseName
                _ShisakuEventName = vo.ShisakuEventName
                _UnitKbn = vo.UnitKbn
                _SeisakujikiFrom = DateUtil.ConvYyyymmddToDate(vo.SeisakujikiFrom)
                _SeisakujikiTo = DateUtil.ConvYyyymmddToDate(vo.SeisakujikiTo)
                _SeisakudaisuKanseisya = StringUtil.ToString(vo.SeisakudaisuKanseisya)
                _SeisakudaisuWb = StringUtil.ToString(vo.SeisakudaisuWb)
                _SeisakudaisuChushi = StringUtil.ToString(vo.SeisakudaisuChushi)
                _SeisakuichiranHakouNo = vo.SeisakuichiranHakouNo
                _SeisakuichiranHakouNoKai = StringUtil.ToString(vo.SeisakuichiranHakouNoKai)
                _HachuUmu = vo.HachuUmu
                _JikyuUmu = vo.JikyuUmu    '2012/01/07
                Dim dataKbnVo As MShisakuStatusVo = aStatusDao.FindByPk(vo.DataKbn)
                If dataKbnVo IsNot Nothing Then
                    _dataKbnName = dataKbnVo.ShisakuStatusName
                End If
                _status = vo.Status
                Dim statusVo As MShisakuStatusVo = aStatusDao.FindByPk(_status)
                If statusVo IsNot Nothing Then
                    _statusName = statusVo.ShisakuStatusName
                End If
                '2012/01/10
                '設計展開の判断を設計展開日の設定有無で判断することとする（暫定）
                If vo.SekkeiTenkaibi IsNot Nothing Then
                    _SekkeiTenkaibi = vo.SekkeiTenkaibi
                End If

                'お知らせ通知フラグと保存フラグにも値をセットする。
                _InfoMailFlg = vo.InfoMailFlg
                _SaveFlg = vo.SaveFlg

            End If
            SetChanged()
        End Sub

        ''' <summary>
        ''' 登録モードかを返す
        ''' </summary>
        ''' <returns>登録モードの場合、true</returns>
        ''' <remarks></remarks>
        Private Function IsAddMode() As Boolean
            Return _shisakuEventCode Is Nothing OrElse StringUtil.IsEmpty(_shisakuEventCode)
        End Function

        ''' <summary>
        ''' イベント情報コピー処理時の初期化など
        ''' </summary>
        ''' <param name="shisakuEventCode">元試作イベントコード</param>
        ''' <remarks></remarks>
        Friend Sub ProcessPostCopy(ByVal shisakuEventCode As String)
            _shisakuEventCode = shisakuEventCode
        End Sub

        Private Class KaihatufugoExtraction : Implements ILabelValueExtraction
            Public Sub Extraction(ByVal aLocator As ShisakuCommon.Util.LabelValue.LabelValueLocator) Implements ShisakuCommon.Util.LabelValue.ILabelValueExtraction.Extraction
                Dim vo As New MShisakuKaihatufugoVo
                aLocator.IsA(vo).Label(vo.ShisakuKaihatsuFugo).Value(vo.ShisakuKaihatsuFugo)
            End Sub
        End Class

        Public Function GetLabelValues_ShisakuKaihatuFugo() As List(Of LabelValueVo)
            Dim results As List(Of LabelValueVo) = _
                LabelValueExtracter(Of MShisakuKaihatufugoVo).Extract(aKaihatufugoDao.FindByAll(), New KaihatufugoExtraction)
            results.Sort(New LabelValueComparer)
            Return results
        End Function

        Private Class EventPhaseNameExtraction : Implements ILabelValueExtraction
            Public Sub Extraction(ByVal aLocator As ShisakuCommon.Util.LabelValue.LabelValueLocator) Implements ShisakuCommon.Util.LabelValue.ILabelValueExtraction.Extraction
                Dim vo As New MShisakuKaihatufugoVo
                aLocator.IsA(vo).Label(vo.ShisakuEventPhaseName).Value(vo.ShisakuEventPhaseName)
            End Sub
        End Class

        Public Function GetLabelValues_EventPhaseName() As List(Of LabelValueVo)
            If ShisakuKaihatsuFugo Is String.Empty Then
                Return New List(Of LabelValueVo)
            End If
            Dim vo As New MShisakuKaihatufugoVo
            vo.ShisakuKaihatsuFugo = ShisakuKaihatsuFugo
            Dim results As List(Of LabelValueVo) = _
                LabelValueExtracter(Of MShisakuKaihatufugoVo).Extract(aKaihatufugoDao.FindBy(vo), New EventPhaseNameExtraction)

            '表示順で表示したいのでソートは不要
            'results.Sort(New LabelValueComparer)

            Return results
        End Function

    End Class
End Namespace