Imports ShisakuCommon.Db.EBom.Vo

Namespace ShisakuBuhinEditBlock.Dao

    ''' <summary>
    ''' 試作HeadInfoのデータ
    ''' </summary>
    ''' <remarks></remarks>
    Public Class EditBlock2ExcelTitle3BodyVo : Inherits TShisakuSekkeiBlockVo
        ''試作社員名称
        Private _SekkeiShainName As String
        ''開発符号　
        Private _ShisakuKaihatsuFugo As String
        ''試作イベント名称 
        Private _ShisakuEventName As String

        ''' <summary> 試作社員名称</summary>
        ''' <value> 試作社員名称</value>
        ''' <returns> 試作社員名称</returns>
        Public Property SekkeiShainName() As String
            Get
                Return _SekkeiShainName
            End Get
            Set(ByVal value As String)
                _SekkeiShainName = value
            End Set
        End Property

        ''' <summary> 開発符号　</summary>
        ''' <value> 開発符号　</value>
        ''' <returns> 開発符号　</returns>
        Public Property ShisakuKaihatsuFugo() As String
            Get
                Return _ShisakuKaihatsuFugo
            End Get
            Set(ByVal value As String)
                _ShisakuKaihatsuFugo = value
            End Set
        End Property

        ''' <summary> 試作イベント名称</summary>
        ''' <value> 試作イベント名称</value>
        ''' <returns> 試作イベント名称</returns>
        Public Property ShisakuEventName() As String
            Get
                Return _ShisakuEventName
            End Get
            Set(ByVal value As String)
                _ShisakuEventName = value
            End Set
        End Property

    End Class

End Namespace