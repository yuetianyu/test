Imports ShisakuCommon.Db.EBom.Vo

Namespace ShisakuBuhinEditBlock.Dao

    ''' <summary>
    ''' 試作装備タイトル
    ''' </summary>
    ''' <remarks></remarks>
    Public Class EditBlock2ExcelTitle2Vo : Inherits TShisakuSekkeiBlockInstlVo

        '試作装備表示順
        Private _ShisakuSoubiHyoujiJun As String
        '試作装備区分
        Private _ShisakuSoubiKbn As String
        '試作装備コード
        Private _ShisakuRetuKoumokuCode As String
        '試作装備項目名
        Private _ShisakuRetuKoumokuName As String
        '試作装備項目名（大区分）
        Private _ShisakuRetuKoumokuNameDai As String
        '試作装備項目名（中区分）
        Private _ShisakuRetuKoumokuNameChu As String


        ''' <summary> 試作装備表示順</summary>
        ''' <value> 試作装備表示順</value>
        ''' <returns> 試作装備表示順</returns>
        Public Property ShisakuSoubiHyoujiJun() As String
            Get
                Return _ShisakuSoubiHyoujiJun
            End Get
            Set(ByVal value As String)
                _ShisakuSoubiHyoujiJun = value
            End Set
        End Property

        ''' <summary> 試作装備区分</summary>
        ''' <value> 試作装備区分</value>
        ''' <returns> 試作装備区分</returns>
        Public Property ShisakuSoubiKbn() As String
            Get
                Return _ShisakuSoubiKbn
            End Get
            Set(ByVal value As String)
                _ShisakuSoubiKbn = value
            End Set
        End Property

        ''' <summary> 試作装備コード</summary>
        ''' <value> 試作装備コード</value>
        ''' <returns> 試作装備コード</returns>
        Public Property ShisakuRetuKoumokuCode() As String
            Get
                Return _ShisakuRetuKoumokuCode
            End Get
            Set(ByVal value As String)
                _ShisakuRetuKoumokuCode = value
            End Set
        End Property

        ''' <summary> 試作装備項目名</summary>
        ''' <value> 試作装備項目名</value>
        ''' <returns> 試作装備項目名</returns>
        Public Property ShisakuRetuKoumokuName() As String
            Get
                Return _ShisakuRetuKoumokuName
            End Get
            Set(ByVal value As String)
                _ShisakuRetuKoumokuName = value
            End Set
        End Property

        ''' <summary> 試作装備項目名（大区分）</summary>
        ''' <value> 試作装備項目名（大区分）</value>
        ''' <returns> 試作装備項目名（大区分）</returns>
        Public Property ShisakuRetuKoumokuNameDai() As String
            Get
                Return _ShisakuRetuKoumokuNameDai
            End Get
            Set(ByVal value As String)
                _ShisakuRetuKoumokuNameDai = value
            End Set
        End Property
        ''' <summary> 試作装備項目名（中区分）</summary>
        ''' <value> 試作装備項目名（中区分）</value>
        ''' <returns> 試作装備項目名（中区分）</returns>
        Public Property ShisakuRetuKoumokuNameChu() As String
            Get
                Return _ShisakuRetuKoumokuNameChu
            End Get
            Set(ByVal value As String)
                _ShisakuRetuKoumokuNameChu = value
            End Set
        End Property

    End Class

End Namespace