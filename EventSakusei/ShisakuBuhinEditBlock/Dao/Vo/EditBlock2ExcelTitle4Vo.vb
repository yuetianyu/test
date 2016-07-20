Imports ShisakuCommon.Db.EBom.Vo

Namespace ShisakuBuhinEditBlock.Dao
    ''' <summary>
    ''' 試作装備仕様
    ''' </summary>
    ''' <remarks></remarks>
    Public Class EditBlock2ExcelTitle4Vo : Inherits TShisakuSekkeiBlockInstlVo

        '' 部品NOTE  BUHIN_NOTE
        Private _BuhinNote As String

        ''' <summary> 部品NOTE</summary>
        ''' <value> 部品NOTE</value>
        ''' <returns> 部品NOTE</returns>
        Public Property BuhinNote() As String
            Get
                Return _BuhinNote
            End Get
            Set(ByVal value As String)
                _BuhinNote = value
            End Set
        End Property
    End Class

End Namespace