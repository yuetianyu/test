Namespace Db.EBom.Vo
    ''' <summary>
    ''' 部品(FM5以降)
    ''' </summary>
    ''' <remarks></remarks>
    Public Class Rhac0533Vo
        '' 部品番号 
        Private _BuhinNo As String
        '' 改訂No 
        Private _KaiteiNo As String
        '' 部品名称 
        Private _BuhinName As String
        '' 補助名称 
        Private _HojoName As String
        '' 部品 
        Private _BuhinKind As String
        ''  内製区分
        Private _NaiseiKbn As String
        '' 板厚数量 
        Private _BankoSuryo As String
        '' 材料期日 
        Private _ZairyoKijitsu As String
        '' サブスタンスID
        Private _SubStanceId As String
        '' 図面番号
        Private _ZumenNo As String
        '' 図面改訂No 
        Private _ZumenKaiteiNo As String
        '' 図面ノート 
        Private _ZumenNote As String
        '' シリーズコード 
        Private _SiriesCode As String
        '' 承認日 
        Private _ShoninDate As Nullable(Of Int32)
        '' 採用日 
        Private _SaiyoDate As Nullable(Of Int32)
        '' 廃止日 
        Private _HaisiDate As Nullable(Of Int32)
        '' 図面オーバー 
        Private _ZumenOver As String
        '' 図面列 
        Private _ZumenColumn As String
        '' 最終区分 
        Private _FinalKbn As String
        '' 作成APPNo 
        Private _CreatedAppNo As String
        '' 更新APPNo 
        Private _UpdatedAppNo As String
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

        ''' <summary>部品番号</summary>
        ''' <value>部品番号</value>
        ''' <returns>部品番号</returns>
        Public Property BuhinNo() As String
            Get
                Return _BuhinNo
            End Get
            Set(ByVal value As String)
                _BuhinNo = value
            End Set
        End Property

        ''' <summary>改訂No</summary>
        ''' <value>改訂No</value>
        ''' <returns>改訂No</returns>
        Public Property KaiteiNo() As String
            Get
                Return _KaiteiNo
            End Get
            Set(ByVal value As String)
                _KaiteiNo = value
            End Set
        End Property

        ''' <summary>部品名称</summary>
        ''' <value>部品名称</value>
        ''' <returns>部品名称</returns>
        Public Property BuhinName() As String
            Get
                Return _BuhinName
            End Get
            Set(ByVal value As String)
                _BuhinName = value
            End Set
        End Property

        ''' <summary>補助名称</summary>
        ''' <value>補助名称</value>
        ''' <returns>補助名称</returns>
        Public Property HojoName() As String
            Get
                Return _HojoName
            End Get
            Set(ByVal value As String)
                _HojoName = value
            End Set
        End Property

        ''' <summary>部品種類</summary>
        ''' <value>部品種類</value>
        ''' <returns>部品種類</returns>
        Public Property BuhinKind() As String
            Get
                Return _BuhinKind
            End Get
            Set(ByVal value As String)
                _BuhinKind = value
            End Set
        End Property

        ''' <summary>内製区分</summary>
        ''' <value>内製区分</value>
        ''' <returns>内製区分</returns>
        Public Property NaiseiKbn() As String
            Get
                Return _NaiseiKbn
            End Get
            Set(ByVal value As String)
                _NaiseiKbn = value
            End Set
        End Property

        ''↓↓2014/08/19 Ⅰ.5.EBOM差分出力 be) (TES)張 ADD BEGIN
        ''' <summary>板厚数量</summary>
        ''' <value>板厚数量</value>
        ''' <returns>板厚数量</returns>
        Public Property BankoSuryo() As String
            Get
                Return _BankoSuryo
            End Get
            Set(ByVal value As String)
                _BankoSuryo = value
            End Set
        End Property
        ''↑↑2014/08/19 Ⅰ.5.EBOM差分出力 be) (TES)張 ADD END

        ''' <summary>材料期日</summary>
        ''' <value>材料期日</value>
        ''' <returns>材料期日</returns>
        Public Property ZairyoKijutsu() As String
            Get
                Return _ZairyoKijitsu
            End Get
            Set(ByVal value As String)
                _ZairyoKijitsu = value
            End Set
        End Property

        ''' <summary>サブスタンスID</summary>
        ''' <value>サブスタンスID</value>
        ''' <returns>サブスタンスID</returns>
        Public Property SubStanceId() As String
            Get
                Return _SubStanceId
            End Get
            Set(ByVal value As String)
                _SubStanceId = value
            End Set
        End Property

        ''' <summary>図面番号</summary>
        ''' <value>図面番号</value>
        ''' <returns>図面番号</returns>
        Public Property ZumenNo() As String
            Get
                Return _ZumenNo
            End Get
            Set(ByVal value As String)
                _ZumenNo = value
            End Set
        End Property

        ''' <summary>図面改訂No</summary>
        ''' <value>図面改訂No</value>
        ''' <returns>図面改訂No</returns>
        Public Property ZumenKaiteiNo() As String
            Get
                Return _ZumenKaiteiNo
            End Get
            Set(ByVal value As String)
                _ZumenKaiteiNo = value
            End Set
        End Property

        ''' <summary>図面ノート</summary>
        ''' <value>図面ノート</value>
        ''' <returns>図面ノート</returns>
        Public Property ZumenNote() As String
            Get
                Return _ZumenNote
            End Get
            Set(ByVal value As String)
                _ZumenNote = value
            End Set
        End Property

        ''' <summary>シリーズコード</summary>
        ''' <value>シリーズコード</value>
        ''' <returns>シリーズコード</returns>
        Public Property SiriesCode() As String
            Get
                Return _SiriesCode
            End Get
            Set(ByVal value As String)
                _SiriesCode = value
            End Set
        End Property

        ''' <summary>承認日</summary>
        ''' <value>承認日</value>
        ''' <returns>承認日</returns>
        Public Property ShoninDate() As Nullable(Of Int32)
            Get
                Return _ShoninDate
            End Get
            Set(ByVal value As Nullable(Of Int32))
                _ShoninDate = value
            End Set
        End Property

        ''' <summary>採用日</summary>
        ''' <value>採用日</value>
        ''' <returns>採用日</returns>
        Public Property SaiyoDate() As Nullable(Of Int32)
            Get
                Return _SaiyoDate
            End Get
            Set(ByVal value As Nullable(Of Int32))
                _SaiyoDate = value
            End Set
        End Property

        ''' <summary>廃止日</summary>
        ''' <value>廃止日</value>
        ''' <returns>廃止日</returns>
        Public Property HaisiDate() As Nullable(Of Int32)
            Get
                Return _HaisiDate
            End Get
            Set(ByVal value As Nullable(Of Int32))
                _HaisiDate = value
            End Set
        End Property

        ''' <summary>図面オーバー</summary>
        ''' <value>図面オーバー</value>
        ''' <returns>図面オーバー</returns>
        Public Property ZumenOver() As String
            Get
                Return _ZumenOver
            End Get
            Set(ByVal value As String)
                _ZumenOver = value
            End Set
        End Property

        ''' <summary>図面列</summary>
        ''' <value>図面列</value>
        ''' <returns>図面列</returns>
        Public Property ZumenColumn() As String
            Get
                Return _ZumenColumn
            End Get
            Set(ByVal value As String)
                _ZumenColumn = value
            End Set
        End Property

        ''' <summary>最終区分</summary>
        ''' <value>最終区分</value>
        ''' <returns>最終区分</returns>
        Public Property FinalKbn() As String
            Get
                Return _FinalKbn
            End Get
            Set(ByVal value As String)
                _FinalKbn = value
            End Set
        End Property

        ''' <summary>作成APPNo</summary>
        ''' <value>作成APPNo</value>
        ''' <returns>作成APPNo</returns>
        Public Property CreatedAppNo() As String
            Get
                Return _CreatedAppNo
            End Get
            Set(ByVal value As String)
                _CreatedAppNo = value
            End Set
        End Property

        ''' <summary>更新APPNo</summary>
        ''' <value>更新APPNo</value>
        ''' <returns>更新APPNo</returns>
        Public Property UpdatedAppNo() As String
            Get
                Return _UpdatedAppNo
            End Get
            Set(ByVal value As String)
                _UpdatedAppNo = value
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