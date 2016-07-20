Imports ShisakuCommon.Db.EBom.Vo

Namespace ShisakuBuhinEdit.Kosei.Dao
    ''' <summary>
    ''' 部品情報に取引先名称を追加したVO
    ''' </summary>
    ''' <remarks></remarks>
    Public Class Rhac0530MakerNameVo : Inherits Rhac0530Vo

        '' 取引先名称 
        Private _MakerName As String

        ''' <summary>取引先名称</summary>
        ''' <value>取引先名称</value>
        ''' <returns>取引先名称</returns>
        Public Property MakerName() As String
            Get
                Return _MakerName
            End Get
            Set(ByVal value As String)
                _MakerName = value
            End Set
        End Property

    End Class
End Namespace