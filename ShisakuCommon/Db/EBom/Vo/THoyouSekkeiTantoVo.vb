Namespace Db.EBom.Vo
    ''' <summary>
    ''' 補用設計担当情報
    ''' </summary>
    ''' <remarks></remarks>
    Public Class THoyouSekkeiTantoVo
        '' 補用イベントコード
        Private _HoyouEventCode As String
        '' 補用部課コード
        Private _HoyouBukaCode As String
        '' 補用担当表示順
        Private _HoyouTantoHyoujiJun As Nullable(Of Int32)
        '' 補用担当
        Private _HoyouTanto As String
        '' 補用担当名称
        Private _HoyouTantoName As String
        '' 補用担当改訂№
        Private _HoyouTantoKaiteiNo As String
        '' 担当不要
        Private _TantoFuyou As String
        '' 状態
        Private _Jyoutai As String
        '' 担当メモ
        Private _TantoMemo As String
        '' ＴＥＬ
        Private _TelNo As String
        '' 改訂内容
        Private _KaiteiNaiyou As String
        '' 最終更新日
        Private _SaisyuKoushinbi As Nullable(Of Int32)
        '' 最終更新時間
        Private _SaisyuKoushinjikan As Nullable(Of Int32)
        '' 一時保存時のメモ
        Private _Memo As String
        '' 担当承認状態
        Private _TantoSyouninJyoutai As String
        '' 担当承認・所属
        Private _TantoSyouninKa As String
        '' 担当承認・承認者
        Private _TantoSyouninSya As String
        '' 担当承認・承認日
        Private _TantoSyouninHi As Nullable(Of Int32)
        '' 担当承認・承認時間
        Private _TantoSyouninJikan As Nullable(Of Int32)
        '' 課長主査承認状態
        Private _KachouSyouninJyoutai As String
        '' 課長主査承認・所属
        Private _KachouSyouninKa As String
        '' 課長主査承認・承認者
        Private _KachouSyouninSya As String
        '' 課長主査承認・承認日
        Private _KachouSyouninHi As Nullable(Of Int32)
        '' 課長主査承認・承認時間
        Private _KachouSyouninJikan As Nullable(Of Int32)
        '' 改訂判断フラグ
        Private _KaiteiHandanFlg As String
        '' 作成ユーザーID
        Private _CreatedUserId As String
        '' 作成日
        Private _CreatedDate As String
        '' 作成時
        Private _CreatedTime As String
        '' 更新ユーザーID
        Private _UpdatedUserId As String
        '' 更新日
        Private _UpdatedDate As String
        '' 更新時間
        Private _UpdatedTime As String

        ''' <summary>補用イベントコード</summary>
        ''' <value>補用イベントコード</value>
        ''' <returns>補用イベントコード</returns>
        Public Property HoyouEventCode() As String
            Get
                Return _HoyouEventCode
            End Get
            Set(ByVal value As String)
                _HoyouEventCode = value
            End Set
        End Property

        ''' <summary>補用部課コード</summary>
        ''' <value>補用部課コード</value>
        ''' <returns>補用部課コード</returns>
        Public Property HoyouBukaCode() As String
            Get
                Return _HoyouBukaCode
            End Get
            Set(ByVal value As String)
                _HoyouBukaCode = value
            End Set
        End Property

        ''' <summary>補用担当表示順</summary>
        ''' <value>補用担当表示順</value>
        ''' <returns>補用担当表示順</returns>
        Public Property HoyouTantoHyoujiJun() As Nullable(Of Int32)
            Get
                Return _HoyouTantoHyoujiJun
            End Get
            Set(ByVal value As Nullable(Of Int32))
                _HoyouTantoHyoujiJun = value
            End Set
        End Property

        ''' <summary>補用担当</summary>
        ''' <value>補用担当</value>
        ''' <returns>補用担当</returns>
        Public Property HoyouTanto() As String
            Get
                Return _HoyouTanto
            End Get
            Set(ByVal value As String)
                _HoyouTanto = value
            End Set
        End Property

        ''' <summary>補用担当名称</summary>
        ''' <value>補用担当名称</value>
        ''' <returns>補用担当名称</returns>
        Public Property HoyouTantoName() As String
            Get
                Return _HoyouTantoName
            End Get
            Set(ByVal value As String)
                _HoyouTantoName = value
            End Set
        End Property

        ''' <summary>補用担当改訂№</summary>
        ''' <value>補用担当改訂№</value>
        ''' <returns>補用担当改訂№</returns>
        Public Property HoyouTantoKaiteiNo() As String
            Get
                Return _HoyouTantoKaiteiNo
            End Get
            Set(ByVal value As String)
                _HoyouTantoKaiteiNo = value
            End Set
        End Property

        ''' <summary>担当不要</summary>
        ''' <value>担当不要</value>
        ''' <returns>担当不要</returns>
        Public Property TantoFuyou() As String
            Get
                Return _TantoFuyou
            End Get
            Set(ByVal value As String)
                _TantoFuyou = value
            End Set
        End Property

        ''' <summary>状態</summary>
        ''' <value>状態</value>
        ''' <returns>状態</returns>
        Public Property Jyoutai() As String
            Get
                Return _Jyoutai
            End Get
            Set(ByVal value As String)
                _Jyoutai = value
            End Set
        End Property

        ''' <summary>担当メモ</summary>
        ''' <value>担当メモ</value>
        ''' <returns>担当メモ</returns>
        Public Property TantoMemo() As String
            Get
                Return _TantoMemo
            End Get
            Set(ByVal value As String)
                _TantoMemo = value
            End Set
        End Property

        ''' <summary>ＴＥＬ</summary>
        ''' <value>ＴＥＬ</value>
        ''' <returns>ＴＥＬ</returns>
        Public Property TelNo() As String
            Get
                Return _TelNo
            End Get
            Set(ByVal value As String)
                _TelNo = value
            End Set
        End Property

        ''' <summary>改訂内容</summary>
        ''' <value>改訂内容</value>
        ''' <returns>改訂内容</returns>
        Public Property KaiteiNaiyou() As String
            Get
                Return _KaiteiNaiyou
            End Get
            Set(ByVal value As String)
                _KaiteiNaiyou = value
            End Set
        End Property

        ''' <summary>最終更新日</summary>
        ''' <value>最終更新日</value>
        ''' <returns>最終更新日</returns>
        Public Property SaisyuKoushinbi() As Nullable(Of Int32)
            Get
                Return _SaisyuKoushinbi
            End Get
            Set(ByVal value As Nullable(Of Int32))
                _SaisyuKoushinbi = value
            End Set
        End Property

        ''' <summary>最終更新時間</summary>
        ''' <value>最終更新時間</value>
        ''' <returns>最終更新時間</returns>
        Public Property SaisyuKoushinjikan() As Nullable(Of Int32)
            Get
                Return _SaisyuKoushinjikan
            End Get
            Set(ByVal value As Nullable(Of Int32))
                _SaisyuKoushinjikan = value
            End Set
        End Property

        ''' <summary>一時保存時のメモ</summary>
        ''' <value>一時保存時のメモ</value>
        ''' <returns>一時保存時のメモ</returns>
        Public Property Memo() As String
            Get
                Return _Memo
            End Get
            Set(ByVal value As String)
                _Memo = value
            End Set
        End Property

        ''' <summary>担当承認状態</summary>
        ''' <value>担当承認状態</value>
        ''' <returns>担当承認状態</returns>
        Public Property TantoSyouninJyoutai() As String
            Get
                Return _TantoSyouninJyoutai
            End Get
            Set(ByVal value As String)
                _TantoSyouninJyoutai = value
            End Set
        End Property

        ''' <summary>担当承認・所属</summary>
        ''' <value>担当承認・所属</value>
        ''' <returns>担当承認・所属</returns>
        Public Property TantoSyouninKa() As String
            Get
                Return _TantoSyouninKa
            End Get
            Set(ByVal value As String)
                _TantoSyouninKa = value
            End Set
        End Property

        ''' <summary>担当承認・承認者</summary>
        ''' <value>担当承認・承認者</value>
        ''' <returns>担当承認・承認者</returns>
        Public Property TantoSyouninSya() As String
            Get
                Return _TantoSyouninSya
            End Get
            Set(ByVal value As String)
                _TantoSyouninSya = value
            End Set
        End Property

        ''' <summary>担当承認・承認日</summary>
        ''' <value>担当承認・承認日</value>
        ''' <returns>担当承認・承認日</returns>
        Public Property TantoSyouninHi() As Nullable(Of Int32)
            Get
                Return _TantoSyouninHi
            End Get
            Set(ByVal value As Nullable(Of Int32))
                _TantoSyouninHi = value
            End Set
        End Property

        ''' <summary>担当承認・承認時間</summary>
        ''' <value>担当承認・承認時間</value>
        ''' <returns>担当承認・承認時間</returns>
        Public Property TantoSyouninJikan() As Nullable(Of Int32)
            Get
                Return _TantoSyouninJikan
            End Get
            Set(ByVal value As Nullable(Of Int32))
                _TantoSyouninJikan = value
            End Set
        End Property

        ''' <summary>課長主査承認状態</summary>
        ''' <value>課長主査承認状態</value>
        ''' <returns>課長主査承認状態</returns>
        Public Property KachouSyouninJyoutai() As String
            Get
                Return _KachouSyouninJyoutai
            End Get
            Set(ByVal value As String)
                _KachouSyouninJyoutai = value
            End Set
        End Property

        ''' <summary>課長主査承認・所属</summary>
        ''' <value>課長主査承認・所属</value>
        ''' <returns>課長主査承認・所属</returns>
        Public Property KachouSyouninKa() As String
            Get
                Return _KachouSyouninKa
            End Get
            Set(ByVal value As String)
                _KachouSyouninKa = value
            End Set
        End Property

        ''' <summary>課長主査承認・承認者</summary>
        ''' <value>課長主査承認・承認者</value>
        ''' <returns>課長主査承認・承認者</returns>
        Public Property KachouSyouninSya() As String
            Get
                Return _KachouSyouninSya
            End Get
            Set(ByVal value As String)
                _KachouSyouninSya = value
            End Set
        End Property

        ''' <summary>課長主査承認・承認日</summary>
        ''' <value>課長主査承認・承認日</value>
        ''' <returns>課長主査承認・承認日</returns>
        Public Property KachouSyouninHi() As Nullable(Of Int32)
            Get
                Return _KachouSyouninHi
            End Get
            Set(ByVal value As Nullable(Of Int32))
                _KachouSyouninHi = value
            End Set
        End Property

        ''' <summary>課長主査承認・承認時間</summary>
        ''' <value>課長主査承認・承認時間</value>
        ''' <returns>課長主査承認・承認時間</returns>
        Public Property KachouSyouninJikan() As Nullable(Of Int32)
            Get
                Return _KachouSyouninJikan
            End Get
            Set(ByVal value As Nullable(Of Int32))
                _KachouSyouninJikan = value
            End Set
        End Property

        ''' <summary>改訂判断フラグ</summary>
        ''' <value>改訂判断フラグ</value>
        ''' <returns>改訂判断フラグ</returns>
        Public Property KaiteiHandanFlg() As String
            Get
                Return _KaiteiHandanFlg
            End Get
            Set(ByVal value As String)
                _KaiteiHandanFlg = value
            End Set
        End Property

        ''' <summary>作成ユーザーID</summary>
        ''' <value>作成ユーザーID</value>
        ''' <returns>作成ユーザーID</returns>
        Public Property CreatedUserId() As String
            Get
                Return _CreatedUserId
            End Get
            Set(ByVal value As String)
                _CreatedUserId = value
            End Set
        End Property

        ''' <summary>作成日</summary>
        ''' <value>作成日</value>
        ''' <returns>作成日</returns>
        Public Property CreatedDate() As String
            Get
                Return _CreatedDate
            End Get
            Set(ByVal value As String)
                _CreatedDate = value
            End Set
        End Property

        ''' <summary>作成時</summary>
        ''' <value>作成時</value>
        ''' <returns>作成時</returns>
        Public Property CreatedTime() As String
            Get
                Return _CreatedTime
            End Get
            Set(ByVal value As String)
                _CreatedTime = value
            End Set
        End Property

        ''' <summary>更新ユーザーID</summary>
        ''' <value>更新ユーザーID</value>
        ''' <returns>更新ユーザーID</returns>
        Public Property UpdatedUserId() As String
            Get
                Return _UpdatedUserId
            End Get
            Set(ByVal value As String)
                _UpdatedUserId = value
            End Set
        End Property

        ''' <summary>更新日</summary>
        ''' <value>更新日</value>
        ''' <returns>更新日</returns>
        Public Property UpdatedDate() As String
            Get
                Return _UpdatedDate
            End Get
            Set(ByVal value As String)
                _UpdatedDate = value
            End Set
        End Property

        ''' <summary>更新時間</summary>
        ''' <value>更新時間</value>
        ''' <returns>更新時間</returns>
        Public Property UpdatedTime() As String
            Get
                Return _UpdatedTime
            End Get
            Set(ByVal value As String)
                _UpdatedTime = value
            End Set
        End Property
    End Class
End Namespace
