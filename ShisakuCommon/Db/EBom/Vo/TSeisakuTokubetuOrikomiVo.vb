Namespace Db.EBom.Vo
    ''' <summary>
    ''' 製作一覧特別織込項目情報
    ''' </summary>
    ''' <remarks></remarks>
    Public Class TSeisakuTokubetuOrikomiVo
        '' 発行№
        Private _HakouNo As String
        '' 改訂№
        Private _KaiteiNo As String
        '' 種別
        Private _Syubetu As String
        '' 表示順
        Private _HyojijunNo As Nullable(of Int32)
        '' 特別織込列表示順
        Private _TokubetuOrikomiHyojijunNo As Nullable(Of Int32)
        '' 号車
        Private _Gousya As String
        '' 大区分名
        Private _DaiKbnName As String
        '' 中区分名
        Private _ChuKbnName As String
        '' 小区分名（詳細）
        Private _ShoKbnName As String
        '' 適用
        Private _Tekiyou As String
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

        ''' <summary>発行№</summary>
        ''' <value>発行№</value>
        ''' <returns>発行№</returns>
        Public Property HakouNo() As String
            Get
                Return _HakouNo
            End Get
            Set(ByVal value As String)
                _HakouNo = value
            End Set
        End Property

        ''' <summary>改訂№</summary>
        ''' <value>改訂№</value>
        ''' <returns>改訂№</returns>
        Public Property KaiteiNo() As String
            Get
                Return _KaiteiNo
            End Get
            Set(ByVal value As String)
                _KaiteiNo = value
            End Set
        End Property

        ''' <summary>種別</summary>
        ''' <value>種別</value>
        ''' <returns>種別</returns>
        Public Property Syubetu() As String
            Get
                Return _Syubetu
            End Get
            Set(ByVal value As String)
                _Syubetu = value
            End Set
        End Property

        ''' <summary>表示順</summary>
        ''' <value>表示順</value>
        ''' <returns>表示順</returns>
        Public Property HyojijunNo() As Nullable(of Int32)
            Get
                Return _HyojijunNo
            End Get
            Set(ByVal value As Nullable(of Int32))
                _HyojijunNo = value
            End Set
        End Property

        ''' <summary>特別織込列表示順</summary>
        ''' <value>特別織込列表示順</value>
        ''' <returns>特別織込列表示順</returns>
        Public Property TokubetuOrikomiHyojijunNo() As Nullable(Of Int32)
            Get
                Return _TokubetuOrikomiHyojijunNo
            End Get
            Set(ByVal value As Nullable(Of Int32))
                _TokubetuOrikomiHyojijunNo = value
            End Set
        End Property

        ''' <summary>号車</summary>
        ''' <value>号車</value>
        ''' <returns>号車</returns>
        Public Property Gousya() As String
            Get
                Return _Gousya
            End Get
            Set(ByVal value As String)
                _Gousya = value
            End Set
        End Property

        ''' <summary>大区分名</summary>
        ''' <value>大区分名</value>
        ''' <returns>大区分名</returns>
        Public Property DaiKbnName() As String
            Get
                Return _DaiKbnName
            End Get
            Set(ByVal value As String)
                _DaiKbnName = value
            End Set
        End Property

        ''' <summary>中区分名</summary>
        ''' <value>中区分名</value>
        ''' <returns>中区分名</returns>
        Public Property ChuKbnName() As String
            Get
                Return _ChuKbnName
            End Get
            Set(ByVal value As String)
                _ChuKbnName = value
            End Set
        End Property

        ''' <summary>小区分名（詳細）</summary>
        ''' <value>小区分名（詳細）</value>
        ''' <returns>小区分名（詳細）</returns>
        Public Property ShoKbnName() As String
            Get
                Return _ShoKbnName
            End Get
            Set(ByVal value As String)
                _ShoKbnName = value
            End Set
        End Property

        ''' <summary>適用</summary>
        ''' <value>適用</value>
        ''' <returns>適用</returns>
        Public Property Tekiyou() As String
            Get
                Return _Tekiyou
            End Get
            Set(ByVal value As String)
                _Tekiyou = value
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
