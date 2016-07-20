Imports ShisakuCommon.Db.EBom.Vo

Namespace ShisakuBuhinMenu.Dao
    Public Class BuhinResultVo : Inherits Rhac0532Vo
        '' 試作部課コード
        Private _ShisakuBukaCode As String
        '' 試作ブロック№
        Private _ShisakuBlockNo As String
        '' 試作ブロック№改訂№
        Private _ShisakuBlockNoKaiteiNo As String
        '' 試作号車
        Private _ShisakuGousya As String
        '' 承認サイン
        Private _ShoninSign As String
        '' 承認日付
        Private _ShoninDate As Nullable(Of Int32)

        ''' <summary>試作部課コード</summary>
        ''' <value>試作部課コード</value>
        ''' <returns>試作部課コード</returns>
        Public Property ShisakuBukaCode() As String
            Get
                Return _ShisakuBukaCode
            End Get
            Set(ByVal value As String)
                _ShisakuBukaCode = value
            End Set
        End Property

        ''' <summary>試作ブロック№</summary>
        ''' <value>試作ブロック№</value>
        ''' <returns>試作ブロック№</returns>
        Public Property ShisakuBlockNo() As String
            Get
                Return _ShisakuBlockNo
            End Get
            Set(ByVal value As String)
                _ShisakuBlockNo = value
            End Set
        End Property

        ''' <summary>試作ブロック№改訂№</summary>
        ''' <value>試作ブロック№改訂№</value>
        ''' <returns>試作ブロック№改訂№</returns>
        Public Property ShisakuBlockNoKaiteiNo() As String
            Get
                Return _ShisakuBlockNoKaiteiNo
            End Get
            Set(ByVal value As String)
                _ShisakuBlockNoKaiteiNo = value
            End Set
        End Property

        ''' <summary>試作号車</summary>
        ''' <value>試作号車</value>
        ''' <returns>試作号車</returns>
        Public Property ShisakuGousya() As String
            Get
                Return _ShisakuGousya
            End Get
            Set(ByVal value As String)
                _ShisakuGousya = value
            End Set
        End Property

        ''' <summary>承認サイン</summary>
        ''' <value>承認サイン</value>
        ''' <returns>承認サイン</returns>
        Public Property ShoninSign() As String
            Get
                Return _ShoninSign
            End Get
            Set(ByVal value As String)
                _ShoninSign = value
            End Set
        End Property

        ''' <summary>承認日付</summary>
        ''' <value>承認日付</value>
        ''' <returns>承認日付</returns>
        Public Property ShoninDate() As Nullable(Of Int32)
            Get
                Return _ShoninDate
            End Get
            Set(ByVal value As Nullable(Of Int32))
                _ShoninDate = value
            End Set
        End Property
    End Class
End Namespace