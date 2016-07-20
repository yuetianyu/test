Imports ShisakuCommon.Db.EBom.Vo

Namespace ShisakuBuhinEdit.Kosei.Logic
    ''' <summary>
    ''' 部品構成表の員数セルを表す情報クラス
    ''' </summary>
    ''' <remarks></remarks>
    Public Class BuhinKoseiInsuCellVo : Inherits TShisakuBuhinEditInstlVo

        Public Function Clone() As BuhinKoseiInsuCellVo
            Return CType(MemberwiseClone(), BuhinKoseiInsuCellVo)
        End Function
    End Class
End Namespace