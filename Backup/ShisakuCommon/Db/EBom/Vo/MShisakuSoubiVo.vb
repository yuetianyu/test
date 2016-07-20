Namespace Db.EBom.Vo
    ''' <summary>
    ''' 試作装備マスター
    ''' </summary>
    ''' <remarks></remarks>
    Public Class MShisakuSoubiVo
        '' 試作装備区分
        Private _ShisakuSoubiKbn As String
        '' 列項目コード
        Private _ShisakuRetuKoumokuCode As String
        '' 列項目名
        Private _ShisakuRetuKoumokuName As String
        '' 列項目名（大区分）
        Private _ShisakuRetuKoumokuNameDai As String
        '' 列項目名（中区分））
        Private _ShisakuRetuKoumokuNameChu As String
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

        ''' <summary>試作装備区分</summary>
        ''' <value>試作装備区分</value>
        ''' <returns>試作装備区分</returns>
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

        ''' <summary>列項目名</summary>
        ''' <value>列項目名</value>
        ''' <returns>列項目名</returns>
        Public Property ShisakuRetuKoumokuName() As String
            Get
                Return _ShisakuRetuKoumokuName
            End Get
            Set(ByVal value As String)
                _ShisakuRetuKoumokuName = value
            End Set
        End Property

        ''' <summary>列項目名（大区分）</summary>
        ''' <value>列項目名（大区分）</value>
        ''' <returns>列項目名（大区分）</returns>
        Public Property ShisakuRetuKoumokuNameDai() As String
            Get
                Return _ShisakuRetuKoumokuNameDai
            End Get
            Set(ByVal value As String)
                _ShisakuRetuKoumokuNameDai = value
            End Set
        End Property

        ''' <summary>列項目名（中区分）</summary>
        ''' <value>列項目名（中区分）</value>
        ''' <returns>列項目名（中区分）</returns>
        Public Property ShisakuRetuKoumokuNameChu() As String
            Get
                Return _ShisakuRetuKoumokuNameChu
            End Get
            Set(ByVal value As String)
                _ShisakuRetuKoumokuNameChu = value
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
