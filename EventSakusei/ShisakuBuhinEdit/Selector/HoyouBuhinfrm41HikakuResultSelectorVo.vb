Imports EventSakusei.ShisakuBuhinEdit.Kosei.Logic

''↓↓2014/08/01 Ⅰ.3.設計編集 ベース車改修専用化_bt) (TES)張 ADD BEGIN
Namespace ShisakuBuhinEdit.Selector
    ''' <summary>比較結果画面用の表示情報</summary>
    Public Class HoyouBuhinfrm41HikakuResultSelectorVo

        ''' <summary>比較構成情報</summary>
        Private _BuhinKoseiRecordVo As BuhinKoseiRecordVo
        ''' <summary>比較構成情報</summary>
        Private _HoyouBuhinBuhinKoseiRecordVo As HoyouBuhinBuhinKoseiRecordVo
        ''' <summary>フラグ</summary>
        Private _Flag As String
        ''' <summary>区分</summary>
        Private _Kubun As String
        ''' <summary>選択区分</summary>
        Private _CheckedKbn As Boolean
        ''' <summary>員数</summary>
        Private _Insu As Integer
        ''' <summary>遷移元</summary>
        Private _MotoGamen As String

        ''' <summary>比較構成情報</summary>
        ''' <value>比較構成情報</value>
        ''' <returns>比較構成情報</returns>
        Public Property BuhinKoseiRecordVo() As BuhinKoseiRecordVo
            Get
                Return _BuhinKoseiRecordVo
            End Get
            Set(ByVal value As BuhinKoseiRecordVo)
                _BuhinKoseiRecordVo = value
            End Set
        End Property
        ''' <summary>比較構成情報</summary>
        ''' <value>比較構成情報</value>
        ''' <returns>比較構成情報</returns>
        Public Property HoyouBuhinBuhinKoseiRecordVo() As HoyouBuhinBuhinKoseiRecordVo
            Get
                Return _HoyouBuhinBuhinKoseiRecordVo
            End Get
            Set(ByVal value As HoyouBuhinBuhinKoseiRecordVo)
                _HoyouBuhinBuhinKoseiRecordVo = value
            End Set
        End Property
        ''' <summary>フラグ</summary>
        ''' <value>フラグ</value>
        ''' <returns>フラグ</returns>
        Public Property Flag() As String
            Get
                Return _Flag
            End Get
            Set(ByVal value As String)
                _Flag = value
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
                _Kubun = value
            End Set
        End Property
        ''' <summary>選択区分</summary>
        ''' <value>選択区分</value>
        ''' <returns>選択区分</returns>
        Public Property CheckedKbn() As Boolean
            Get
                Return _CheckedKbn
            End Get
            Set(ByVal value As Boolean)
                _CheckedKbn = value
            End Set
        End Property
        ''' <summary>員数</summary>
        ''' <value>員数</value>
        ''' <returns>員数</returns>
        Public Property Insu() As Integer
            Get
                Return _Insu
            End Get
            Set(ByVal value As Integer)
                _Insu = value
            End Set
        End Property
        ''' <summary>遷移元</summary>
        ''' <value>遷移元</value>
        ''' <returns>遷移元</returns>
        Public Property MotoGamen() As String
            Get
                Return _MotoGamen
            End Get
            Set(ByVal value As String)
                _MotoGamen = value
            End Set
        End Property
    End Class
End Namespace