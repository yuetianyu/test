Imports ShisakuCommon.Db.EBom.Vo

Namespace Soubi
    Public Class TShisakuEventSoubiNameVo : Inherits TShisakuEventSoubiVo

        '' 列項目名
        Private _ShisakuRetuKoumokuName As String

        '' 列項目名（大区分）
        Private _ShisakuRetuKoumokuNameDai As String

        '' 列項目名（中区分）
        Private _ShisakuRetuKoumokuNameChu As String

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

    End Class
End Namespace