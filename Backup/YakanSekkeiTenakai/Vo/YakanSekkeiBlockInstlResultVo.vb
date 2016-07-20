Imports ShisakuCommon.Db.EBom.Vo

Namespace Vo
    Public Class YakanSekkeiBlockInstlResultVo : Inherits TShisakuSekkeiBlockInstlEbomKanshiVo
        '' ブロック名称
        Private _ShisakuBlockName As String
        ' ベース車情報の号車
        Private _baseShisakuGousya As String

        ''' <summary>ブロック名称</summary>
        ''' <value>ブロック名称</value>
        ''' <returns>ブロック名称</returns>
        Public Property ShisakuBlockName() As String
            Get
                Return _ShisakuBlockName
            End Get
            Set(ByVal value As String)
                _ShisakuBlockName = value
            End Set
        End Property

        ''' <summary>ベース車情報の号車</summary>
        ''' <value>ベース車情報の号車</value>
        ''' <returns>ベース車情報の号車</returns>
        Public Property BaseShisakuGousya() As String
            Get
                Return _baseShisakuGousya
            End Get
            Set(ByVal value As String)
                _baseShisakuGousya = value
            End Set
        End Property
    End Class
End Namespace