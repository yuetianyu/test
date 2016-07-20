Namespace Db.EBom.Vo

    ''' <summary>
    ''' 製作一覧予算コード紐付け情報
    ''' </summary>
    ''' <remarks></remarks>
    Public Class MSeisakuLinkYosanVo

        ''' <summary>発行No.</summary>
        Private _HakouNo As String

        ''' <summary>開発符号</summary>
        Private _KaihatsuFugo As String

        ''' <summary>イベント</summary>
        Private _SeisakuEvent As String

        ''' <summary>イベント名</summary>
        Private _SeisakuEventName As String

        ''' <summary>試作種別(" " OR W)</summary>
        Private _ShisakuSyubetu As String

        ''' <summary>表示順</summary>
        Private _HyojijunNo As Integer

        ''' <summary>予算コード表示順</summary>
        Private _YosanCodeHyojijunNo As Integer

        ''' <summary>予算コード</summary>
        Private _YosanCode As String

        ''' <summary>計算期日</summary>
        Private _KeisanKijitsu As Nullable(Of DateTime)

        ''' <summary>作成ユーザーID</summary>
        Private _CreatedUserId As String

        ''' <summary>作成日</summary>
        Private _CreatedDate As String

        ''' <summary>作成時間</summary>
        Private _CreatedTime As String

        ''' <summary>更新ユーザーID</summary>
        Private _UpdatedUserId As String

        ''' <summary>更新日</summary>
        Private _UpdatedDate As String

        ''' <summary>更新時間</summary>
        Private _UpdatedTime As String


        ''' <summary>発行No.取得・設定</summary>
        ''' <value>発行No.</value>
        ''' <returns>発行No.</returns>
        Public Property HakouNo() As String
            Get
                Return _HakouNo
            End Get
            Set(ByVal value As String)
                _HakouNo = value
            End Set
        End Property

        ''' <summary>開発符号取得・設定</summary>
        ''' <value>開発符号</value>
        ''' <returns>開発符号</returns>
        Public Property KaihatsuFugo() As String
            Get
                Return _KaihatsuFugo
            End Get
            Set(ByVal value As String)
                _KaihatsuFugo = value
            End Set
        End Property

        ''' <summary>イベント取得・設定</summary>
        ''' <value>イベント</value>
        ''' <returns>イベント</returns>
        Public Property SeisakuEvent() As String
            Get
                Return _SeisakuEvent
            End Get
            Set(ByVal value As String)
                _SeisakuEvent = value
            End Set
        End Property

        ''' <summary>イベント名取得・設定</summary>
        ''' <value>イベント名</value>
        ''' <returns>イベント名</returns>
        Public Property SeisakuEventName() As String
            Get
                Return _SeisakuEventName
            End Get
            Set(ByVal value As String)
                _SeisakuEventName = value
            End Set
        End Property

        ''' <summary>試作種別取得・設定</summary>
        ''' <value>試作種別</value>
        ''' <returns>試作種別</returns>
        Public Property ShisakuSyubetu() As String
            Get
                Return _ShisakuSyubetu
            End Get
            Set(ByVal value As String)
                _ShisakuSyubetu = value
            End Set
        End Property

        ''' <summary>表示順取得・設定</summary>
        ''' <value>表示順</value>
        ''' <returns>表示順</returns>
        Public Property HyojijunNo() As Integer
            Get
                Return _HyojijunNo
            End Get
            Set(ByVal value As Integer)
                _HyojijunNo = value
            End Set
        End Property

        ''' <summary>予算コード表示順取得・設定</summary>
        ''' <value>予算コード表示順</value>
        ''' <returns>予算コード表示順</returns>
        Public Property YosanCodeHyojijunNo() As Integer
            Get
                Return _YosanCodeHyojijunNo
            End Get
            Set(ByVal value As Integer)
                _YosanCodeHyojijunNo = value
            End Set
        End Property

        ''' <summary>予算コード取得・設定</summary>
        ''' <value>予算コード</value>
        ''' <returns>予算コード</returns>
        Public Property YosanCode() As String
            Get
                Return _YosanCode
            End Get
            Set(ByVal value As String)
                _YosanCode = value
            End Set
        End Property

        ''' <summary>計算期日取得・設定</summary>
        ''' <value>計算期日</value>
        ''' <returns>計算期日</returns>
        Public Property KeisanKijitsu() As Nullable(Of DateTime)
            Get
                Return _KeisanKijitsu
            End Get
            Set(ByVal value As Nullable(Of DateTime))
                _KeisanKijitsu = value
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