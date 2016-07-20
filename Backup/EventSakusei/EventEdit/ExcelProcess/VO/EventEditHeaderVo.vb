Namespace EventEdit.Vo
    ''' <summary>
    ''' Head
    ''' </summary>
    ''' <remarks></remarks>
    Public Class EventEditHeaderVo
        ''' <summary>
        ''' 試作イベントコード
        ''' </summary>
        ''' <remarks></remarks>
        Private _ShisakuEventCode As String
        ''' <summary>
        ''' 試作開発符号
        ''' </summary>
        ''' <remarks></remarks>
        Private _ShisakuKaihatsuFugo As String
        ''' <summary>
        ''' 試作イベントフェーズ名
        ''' </summary>
        ''' <remarks></remarks>
        Private _ShisakuEventPhaseName As String
        ''' <summary>
        ''' 試作イベント名称
        ''' </summary>
        ''' <remarks></remarks>
        Private _ShisakuEventName As String
        ''' <summary>
        ''' ユニット区分
        ''' </summary>
        ''' <remarks></remarks>
        Private _UnitKbn As String
        ''' <summary>
        ''' >製作次期FROM
        ''' </summary>
        ''' <remarks></remarks>
        Private _SeisakujikiFrom As DateTime?
        ''' <summary>
        ''' 製作次期TO
        ''' </summary>
        ''' <remarks></remarks>
        Private _SeisakujikiTo As DateTime?
        ''' <summary>
        ''' 製作台数・完成車
        ''' </summary>
        ''' <remarks></remarks>
        Private _SeisakudaisuKanseisya As String
        ''' <summary>
        ''' 製作台数・W/B
        ''' </summary>
        ''' <remarks></remarks>
        Private _SeisakudaisuWb As String
        ''' <summary>
        ''' 製作台数・製作中止
        ''' </summary>
        ''' <remarks></remarks>
        Private _SeisakudaisuChushi As String
        ''' <summary>
        ''' 製作一覧発行№
        ''' </summary>
        ''' <remarks></remarks>
        Private _SeisakuichiranHakouNo As String
        ''' <summary>
        ''' 製作一覧発行№改訂
        ''' </summary>
        ''' <remarks></remarks>
        Private _SeisakuichiranHakouNoKai As String
        ''' <summary>
        ''' 発注の有無
        ''' </summary>
        ''' <remarks></remarks>
        Private _HachuUmu As String
        ''' <summary>
        ''' 現在進捗状況
        ''' </summary>
        ''' <remarks></remarks>
        Private _StatusName As String
        ''' <summary>
        ''' 現在データ状況
        ''' </summary>
        ''' <remarks></remarks>
        Private _DataKbnName As String

        ''' <summary>試作イベントコード</summary>
        ''' <returns>試作イベントコード</returns>
        Public Property ShisakuEventCode() As String
            Get
                Return _ShisakuEventCode
            End Get
            Set(ByVal value As String)
                _ShisakuEventCode = value
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
                _ShisakuKaihatsuFugo = value
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
                _ShisakuEventPhaseName = value
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
                _ShisakuEventName = value
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
                _UnitKbn = value
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
                _SeisakujikiFrom = value
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
                _SeisakujikiTo = value
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
                _SeisakudaisuKanseisya = value
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
                _SeisakudaisuWb = value
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
                _SeisakudaisuChushi = value
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
                _SeisakuichiranHakouNo = value
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
                _SeisakuichiranHakouNoKai = value
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
                _HachuUmu = value
            End Set
        End Property

        ''' <summary>現在進捗状況</summary>
        ''' <returns>現在進捗状況</returns>
        Public Property StatusName() As String
            Get
                Return _StatusName
            End Get
            Set(ByVal value As String)
                _StatusName = value
            End Set
        End Property

        ''' <summary>現在データ状況</summary>
        ''' <returns>現在データ状況</returns>
        Public Property DataKbnName() As String
            Get
                Return _DataKbnName
            End Get
            Set(ByVal value As String)
                _DataKbnName = value
            End Set
        End Property
    End Class
End Namespace

