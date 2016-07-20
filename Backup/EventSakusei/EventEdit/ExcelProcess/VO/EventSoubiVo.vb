Namespace EventEdit.Vo
    ''' <summary>
    ''' 試作イベント特別装備仕様＋名称
    ''' </summary>
    ''' <remarks></remarks>
    Public Class EventSoubiVo
        '' 試作イベントコード
        Private _ShisakuEventCode As String
        '' 表示順
        Private _HyojijunNo As Nullable(of Int32)
        '' 試作装備表示順
        Private _ShisakuSoubiHyoujiNo As Nullable(Of Int32)
        '' 試作装備区分
        Private _ShisakuSoubiKbn As String
        '' 列項目コード
        Private _ShisakuRetuKoumokuCode As String
        '' 試作適用
        Private _ShisakuTekiyou As String
        '' 列項目名
        Private _ShisakuRetuKoumokuName As String
        '' 列項目名（大区分）
        Private _ShisakuRetuKoumokuNameDai As String
        '' 列項目名（中区分））
        Private _ShisakuRetuKoumokuNameChu As String
        '' 試作種別
        Private _ShisakuSyubetu As String
        '' 試作号車
        Private _ShisakuGousya As String

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

        ''' <summary>試作種別</summary>
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

        ''' <summary>試作号車</summary>
        ''' <value>試作号車</value>
        ''' <returns>試作号車</returns>
        Public Property ShisakuGousya() As String
            Get
                Return _ShisakuGousya
            End Get
            Set(ByVal value As String)
                _ShisakuGousya = value
            End Set
        End Property

    End Class
End Namespace
