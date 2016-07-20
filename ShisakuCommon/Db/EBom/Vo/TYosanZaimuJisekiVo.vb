Namespace Db.EBom.Vo
    ''' <summary>予算書イベント別年月別財務実績情報</summary>
    Public Class TYosanZaimuJisekiVo
        ''' <summary>予算コード</summary>
        Private _YosanCode As String
        ''' <summary>財務実績計上年月</summary>
        Private _YosanZaimuJisekiYyyyMm As String
        ''' <summary>比例費／固定費区分</summary>
        Private _YosanZaimuHireiKoteiKbn As String
        ''' <summary>財務実績区分</summary>
        Private _YosanZaimuJisekiKbn As String
        ''' <summary>財務報告実績_取込値</summary>
        Private _YosanZaimuImportQty As Nullable(Of Decimal)
        ''' <summary>財務報告実績_入力値</summary>
        Private _YosanZaimuInputQty As Nullable(Of Decimal)
        ''' <summary>作成ユーザーID</summary>
        Private _CreatedUserId As String
        ''' <summary>作成日</summary>
        Private _CreatedDate As String
        ''' <summary>作成時</summary>
        Private _CreatedTime As String
        ''' <summary>更新ユーザーID</summary>
        Private _UpdatedUserId As String
        ''' <summary>更新日</summary>
        Private _UpdatedDate As String
        ''' <summary>更新時間</summary>
        Private _UpdatedTime As String

        ''' <summary>予算コード</summary>
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
        ''' <summary>財務実績計上年月</summary>
        ''' <value>財務実績計上年月</value>
        ''' <returns>財務実績計上年月</returns>
        Public Property YosanZaimuJisekiYyyyMm() As String
            Get
                Return _YosanZaimuJisekiYyyyMm
            End Get
            Set(ByVal value As String)
                _YosanZaimuJisekiYyyyMm = value
            End Set
        End Property
        ''' <summary>比例費／固定費区分</summary>
        ''' <value>比例費／固定費区分</value>
        ''' <returns>比例費／固定費区分</returns>
        Public Property YosanZaimuHireiKoteiKbn() As String
            Get
                Return _YosanZaimuHireiKoteiKbn
            End Get
            Set(ByVal value As String)
                _YosanZaimuHireiKoteiKbn = value
            End Set
        End Property
        ''' <summary>財務実績区分</summary>
        ''' <value>財務実績区分</value>
        ''' <returns>財務実績区分</returns>
        Public Property YosanZaimuJisekiKbn() As String
            Get
                Return _YosanZaimuJisekiKbn
            End Get
            Set(ByVal value As String)
                _YosanZaimuJisekiKbn = value
            End Set
        End Property
        ''' <summary>財務報告実績_取込値</summary>
        ''' <value>財務報告実績_取込値</value>
        ''' <returns>財務報告実績_取込値</returns>
        Public Property YosanZaimuImportQty() As Nullable(Of Decimal)
            Get
                Return _YosanZaimuImportQty
            End Get
            Set(ByVal value As Nullable(Of Decimal))
                _YosanZaimuImportQty = value
            End Set
        End Property
        ''' <summary>財務報告実績_入力値</summary>
        ''' <value>財務報告実績_入力値</value>
        ''' <returns>財務報告実績_入力値</returns>
        Public Property YosanZaimuInputQty() As Nullable(Of Decimal)
            Get
                Return _YosanZaimuInputQty
            End Get
            Set(ByVal value As Nullable(Of Decimal))
                _YosanZaimuInputQty = value
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