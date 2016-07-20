Imports ShisakuCommon.Db.EBom.Vo

Namespace ShisakuBuhinEdit.Kosei.Logic
    ''' <summary>
    ''' 部品構成編集のレコード情報Vo
    ''' </summary>
    ''' <remarks></remarks>
    Public Class HoyouBuhinBuhinKoseiRecordVo : Inherits THoyouBuhinEditVo
        '' 圧縮フラグ  
        Private _MergeFlag As Integer

        '' 圧縮フラグ  
        Private _SortFlag As Integer

        ''' <summary>圧縮フラグ </summary>
        ''' <value>圧縮フラグ </value>
        ''' <returns>圧縮フラグ </returns>
        Public Property MergeFlag() As Integer
            Get
                Return _MergeFlag
            End Get
            Set(ByVal value As Integer)
                _MergeFlag = value
            End Set
        End Property

        ''' <summary>ソートフラグ </summary>
        ''' <value>ソートフラグ </value>
        ''' <returns>ソートフラグ </returns>
        Public Property SortFlag() As Integer
            Get
                Return _SortFlag
            End Get
            Set(ByVal value As Integer)
                _SortFlag = value
            End Set
        End Property

    End Class
End Namespace