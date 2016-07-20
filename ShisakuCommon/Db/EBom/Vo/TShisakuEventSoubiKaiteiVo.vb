Namespace Db.EBom.Vo
    ''' <summary>
    ''' 試作イベント特別装備仕様
    ''' </summary>
    ''' <remarks></remarks>
    Public Class TShisakuEventSoubiKaiteiVo
        '' 試作イベントコード
        Private _ShisakuEventCode As String
        '' 表示順
        Private _HyojijunNo As Nullable(Of Int32)
        '' 試作装備表示順
        Private _ShisakuSoubiHyoujiNo As Nullable(Of Int32)
        '' 試作装備区分
        Private _ShisakuSoubiKbn As String
        '' 列項目コード
        Private _ShisakuRetuKoumokuCode As String
        '' 試作適用
        Private _ShisakuTekiyou As String
        '' 試作適用（大区分）
        Private _ShisakuTekiyouDai As String
        '' 試作適用（中区分）
        Private _ShisakuTekiyouChu As String
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

        ''' <summary>試作イベントコード</summary>
        ''' <value>試作イベントコード</value>
        ''' <returns>試作イベントコード</returns>
        Public Property ShisakuEventCode() As String
            Get
                Return _ShisakuEventCode
            End Get
            Set(ByVal value As String)
                _ShisakuEventCode = value
            End Set
        End Property

        ''' <summary>表示順</summary>
        ''' <value>表示順</value>
        ''' <returns>表示順</returns>
        Public Property HyojijunNo() As Nullable(Of Int32)
            Get
                Return _HyojijunNo
            End Get
            Set(ByVal value As Nullable(Of Int32))
                _HyojijunNo = value
            End Set
        End Property



        ''' <summary>試作装備表示順</summary>
        ''' <value>試作装備表示順</value>
        ''' <returns>試作装備表示順</returns>
        Public Property ShisakuSoubiHyoujiNo() As Nullable(Of Int32)
            Get
                Return _ShisakuSoubiHyoujiNo
            End Get
            Set(ByVal value As Nullable(Of Int32))
                _ShisakuSoubiHyoujiNo = value
            End Set
        End Property

        ''' <summary>試作特別装備区分</summary>
        ''' <value>試作特別装備区分</value>
        ''' <returns>試作特別装備区分</returns>
        Public Property ShisakuSoubiKbn() As String
            Get
                Return _ShisakuSoubiKbn
            End Get
            Set(ByVal value As String)
                _ShisakuSoubiKbn = value
            End Set
        End Property

        ''' <summary>列項目コード</summary>
        ''' <value>列項目コード</value>
        ''' <returns>列項目コード</returns>
        Public Property ShisakuRetuKoumokuCode() As String
            Get
                Return _ShisakuRetuKoumokuCode
            End Get
            Set(ByVal value As String)
                _ShisakuRetuKoumokuCode = value
            End Set
        End Property

        ''' <summary>試作適用</summary>
        ''' <value>試作適用</value>
        ''' <returns>試作適用</returns>
        Public Property ShisakuTekiyou() As String
            Get
                Return _ShisakuTekiyou
            End Get
            Set(ByVal value As String)
                _ShisakuTekiyou = value
            End Set
        End Property

        ''' <summary>試作適用（大区分）</summary>
        ''' <value>試作適用（大区分）</value>
        ''' <returns>試作適用（大区分）</returns>
        Public Property ShisakuTekiyouDai() As String
            Get
                Return _ShisakuTekiyouDai
            End Get
            Set(ByVal value As String)
                _ShisakuTekiyouDai = value
            End Set
        End Property

        ''' <summary>試作適用（中区分）</summary>
        ''' <value>試作適用（中区分）</value>
        ''' <returns>試作適用（中区分）</returns>
        Public Property ShisakuTekiyouChu() As String
            Get
                Return _ShisakuTekiyouChu
            End Get
            Set(ByVal value As String)
                _ShisakuTekiyouChu = value
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
