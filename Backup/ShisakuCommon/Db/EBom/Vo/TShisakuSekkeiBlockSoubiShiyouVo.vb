Namespace Db.EBom.Vo
    ''' <summary>
    ''' 試作設計ブロック装備仕様情報
    ''' </summary>
    ''' <remarks></remarks>
    Public Class TShisakuSekkeiBlockSoubiShiyouVo
        '' 試作イベントコード
        Private _ShisakuEventCode As String
        '' 試作部課コード
        Private _ShisakuBukaCode As String
        '' 試作ブロック№
        Private _ShisakuBlockNo As String
        '' 試作ブロック№改訂№
        Private _ShisakuBlockNoKaiteiNo As String
        '' 試作装備表示順
        Private _ShisakuSoubiHyoujiJun As Nullable(of Int32)
        '' 試作装備区分
        Private _ShisakuSoubiKbn As String
        '' 列項目コード
        Private _ShisakuRetuKoumokuCode As String
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

        ''' <summary>試作部課コード</summary>
        ''' <value>試作部課コード</value>
        ''' <returns>試作部課コード</returns>
        Public Property ShisakuBukaCode() As String
            Get
                Return _ShisakuBukaCode
            End Get
            Set(ByVal value As String)
                _ShisakuBukaCode = value
            End Set
        End Property

        ''' <summary>試作ブロック№</summary>
        ''' <value>試作ブロック№</value>
        ''' <returns>試作ブロック№</returns>
        Public Property ShisakuBlockNo() As String
            Get
                Return _ShisakuBlockNo
            End Get
            Set(ByVal value As String)
                _ShisakuBlockNo = value
            End Set
        End Property

        ''' <summary>試作ブロック№改訂№</summary>
        ''' <value>試作ブロック№改訂№</value>
        ''' <returns>試作ブロック№改訂№</returns>
        Public Property ShisakuBlockNoKaiteiNo() As String
            Get
                Return _ShisakuBlockNoKaiteiNo
            End Get
            Set(ByVal value As String)
                _ShisakuBlockNoKaiteiNo = value
            End Set
        End Property

        ''' <summary>試作装備表示順</summary>
        ''' <value>試作装備表示順</value>
        ''' <returns>試作装備表示順</returns>
        Public Property ShisakuSoubiHyoujiJun() As Nullable(of Int32)
            Get
                Return _ShisakuSoubiHyoujiJun
            End Get
            Set(ByVal value As Nullable(of Int32))
                _ShisakuSoubiHyoujiJun = value
            End Set
        End Property

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
