Namespace Db.EBom.Vo
    ''' <summary>
    ''' 試作イベント特別装備仕様履歴
    ''' </summary>
    ''' <remarks></remarks>
    Public Class TShisakuEventSoubiRirekiVo
        '' 試作イベントコード
        Private _ShisakuEventCode As String
        '' 表示順
        Private _HyojijunNo As Nullable(of Int32)
        '' 試作装備表示順
        Private _ShisakuSoubiHyoujiNo As Nullable(Of Int32)
        '' 試作装備区分
        Private _ShisakuSoubiKbn As String
        '' 更新日
        Private _UpdateBi As String
        '' 更新時間
        Private _UpdateJikan As String
        '' 列項目コードの更新前
        Private _ShisakuRetuKoumokuCodeBefore As String
        '' 列項目コードの更新後
        Private _ShisakuRetuKoumokuCodeAfter As String
        '' 試作適用の更新前
        Private _ShisakuTekiyouBefore As String
        '' 試作適用の更新後
        Private _ShisakuTekiyouAfter As String
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

        ''' <summary>変更日</summary>
        ''' <value>変更日</value>
        ''' <returns>変更日</returns>
        Public Property UpdateBi() As String
            Get
                Return _UpdateBi
            End Get
            Set(ByVal value As String)
                _UpdateBi = value
            End Set
        End Property

        ''' <summary>変更時間</summary>
        ''' <value>変更時間</value>
        ''' <returns>変更時間</returns>
        Public Property UpdateJikan() As String
            Get
                Return _UpdateJikan
            End Get
            Set(ByVal value As String)
                _UpdateJikan = value
            End Set
        End Property

        ''' <summary>列項目コードの更新前</summary>
        ''' <value>列項目コードの更新前</value>
        ''' <returns>列項目コードの更新前</returns>
        Public Property ShisakuRetuKoumokuCodeBefore() As String
            Get
                Return _ShisakuRetuKoumokuCodeBefore
            End Get
            Set(ByVal value As String)
                _ShisakuRetuKoumokuCodeBefore = value
            End Set
        End Property

        ''' <summary>列項目コードの更新後</summary>
        ''' <value>列項目コードの更新後</value>
        ''' <returns>列項目コードの更新後</returns>
        Public Property ShisakuRetuKoumokuCodeAfter() As String
            Get
                Return _ShisakuRetuKoumokuCodeAfter
            End Get
            Set(ByVal value As String)
                _ShisakuRetuKoumokuCodeAfter = value
            End Set
        End Property

        ''' <summary>試作適用の更新前</summary>
        ''' <value>試作適用の更新前</value>
        ''' <returns>試作適用の更新前</returns>
        Public Property ShisakuTekiyouBefore() As String
            Get
                Return _ShisakuTekiyouBefore
            End Get
            Set(ByVal value As String)
                _ShisakuTekiyouBefore = value
            End Set
        End Property

        ''' <summary>試作適用の更新後</summary>
        ''' <value>試作適用の更新後</value>
        ''' <returns>試作適用の更新後</returns>
        Public Property ShisakuTekiyouAfter() As String
            Get
                Return _ShisakuTekiyouAfter
            End Get
            Set(ByVal value As String)
                _ShisakuTekiyouAfter = value
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
