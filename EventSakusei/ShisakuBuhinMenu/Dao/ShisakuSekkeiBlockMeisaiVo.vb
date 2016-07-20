Imports ShisakuCommon.Db.EBom.Vo
Namespace ShisakuBuhinMenu.Dao
    ''' <summary>
    ''' 試作部品メニュー出力の明細部分（SHEET２）
    ''' </summary>
    ''' <remarks></remarks>
    Public Class ShisakuSekkeiBlockMeisaiVo : Inherits TShisakuSekkeiBlockVo
        ''' <summary>担当者</summary>
        Private _SyainName As String
        ''' <summary>課略称</summary>
        Private _KaRyakuName As String
        ''' <summary>承認１の担当者名</summary>
        Private _TantoName As String
        ''' <summary>承認２の課長名</summary>
        Private _KachouName As String

        ''' <summary>
        ''' 担当者
        ''' </summary>
        ''' <value>担当者</value>
        ''' <returns>担当者</returns>
        ''' <remarks></remarks>
        Public Property SyainName() As String
            Get
                Return _SyainName
            End Get
            Set(ByVal value As String)
                _SyainName = value
            End Set
        End Property

        ''' <summary>
        ''' 課略称
        ''' </summary>
        ''' <value>課略称</value>
        ''' <returns>課略称</returns>
        ''' <remarks></remarks>
        Public Property KaRyakuName() As String
            Get
                Return _KaRyakuName
            End Get
            Set(ByVal value As String)
                _KaRyakuName = value
            End Set
        End Property

        ''' <summary>
        ''' 承認１の担当者名
        ''' </summary>
        ''' <value>承認１の担当者名</value>
        ''' <returns>承認１の担当者名</returns>
        ''' <remarks></remarks>
        Public Property TantoName() As String
            Get
                Return _TantoName
            End Get
            Set(ByVal value As String)
                _TantoName = value
            End Set
        End Property

        ''' <summary>
        ''' 承認１の担当者名
        ''' </summary>
        ''' <value>承認１の担当者名</value>
        ''' <returns>承認１の担当者名</returns>
        ''' <remarks></remarks>
        Public Property KachouName() As String
            Get
                Return _KachouName
            End Get
            Set(ByVal value As String)
                _KachouName = value
            End Set
        End Property

    End Class
End Namespace

