Imports ShisakuCommon.Db.EBom.Vo

Namespace ShisakuBuhinEditBlock.Dao
    ''' <summary>
    ''' 試作装備仕様
    ''' </summary>
    ''' <remarks></remarks>
    Public Class EditBlock2ExcelTitle1Vo : Inherits TShisakuSekkeiBlockInstlVo

        '' 項目No  SHISAKU_SOUBI_HYOUJI_JUN
        Private _ShisakuSoubiHyoujiJun As String

        ''' <summary> 項目No</summary>
        ''' <value> 項目No</value>
        ''' <returns> 項目No</returns>
        Public Property ShisakuSoubiHyoujiJun() As String
            Get
                Return _ShisakuSoubiHyoujiJun
            End Get
            Set(ByVal value As String)
                _ShisakuSoubiHyoujiJun = value
            End Set
        End Property

    End Class

End Namespace