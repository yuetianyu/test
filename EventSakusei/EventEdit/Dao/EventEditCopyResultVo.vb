Imports ShisakuCommon.Db.EBom.Vo

Namespace EventEdit.Dao

    Public Class EventEditCopyResultVo : Inherits TShisakuEventVo
        '' 試作ステータス名
        Private _ShisakuStatusName As String

        ''' <summary>試作ステータス名</summary>
        ''' <value>試作ステータス名</value>
        ''' <returns>試作ステータス名</returns>
        Public Property ShisakuStatusName() As String
            Get
                Return _ShisakuStatusName
            End Get
            Set(ByVal value As String)
                _ShisakuStatusName = value
            End Set
        End Property
    End Class
End Namespace