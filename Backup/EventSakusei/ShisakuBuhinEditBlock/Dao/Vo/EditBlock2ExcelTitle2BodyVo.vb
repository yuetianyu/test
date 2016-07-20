Imports ShisakuCommon.Db.EBom.Vo

Namespace ShisakuBuhinEditBlock.Dao


    ''' <summary>
    ''' 試作装備データ
    ''' </summary>
    ''' <remarks></remarks>
    Public Class EditBlock2ExcelTitle2BodyVo : Inherits TShisakuSekkeiBlockInstlVo
        ''試作装備区分
        Private _ShisakuSoubiKbn As String
        ''列項目コード
        Private _ShisakuRetuKoumokuCode As String
        ''列項目名前
        Private _ShisakuRetuKoumokuName As String
        ''列項目名前（大区分）
        Private _ShisakuRetuKoumokuNameDai As String
        ''列項目名前（中区分）
        Private _ShisakuRetuKoumokuNameChu As String
        ''試作適用
        Private _ShisakuTekiyou As String

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


        ''' <summary>列項目名前</summary>
        ''' <value>列項目名前</value>
        ''' <returns>列項目名前</returns>
        Public Property ShisakuRetuKoumokuName() As String
            Get
                Return _ShisakuRetuKoumokuName
            End Get
            Set(ByVal value As String)
                _ShisakuRetuKoumokuName = value
            End Set
        End Property

        ''' <summary>列項目名前（大区分）</summary>
        ''' <value>列項目名前（大区分）</value>
        ''' <returns>列項目名前（大区分）</returns>
        Public Property ShisakuRetuKoumokuNameDai() As String
            Get
                Return _ShisakuRetuKoumokuNameDai
            End Get
            Set(ByVal value As String)
                _ShisakuRetuKoumokuNameDai = value
            End Set
        End Property

        ''' <summary>列項目名前（中区分）</summary>
        ''' <value>列項目名前（中区分）</value>
        ''' <returns>列項目名前（中区分）</returns>
        Public Property ShisakuRetuKoumokuNameChu() As String
            Get
                Return _ShisakuRetuKoumokuNameChu
            End Get
            Set(ByVal value As String)
                _ShisakuRetuKoumokuNameChu = value
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

    End Class


End Namespace