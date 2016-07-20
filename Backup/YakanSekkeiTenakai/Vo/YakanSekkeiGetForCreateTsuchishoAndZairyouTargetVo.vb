Namespace Vo
    ''' <summary>
    ''' 試作部品編集情報(EBOM設変)
    ''' </summary>
    ''' <remarks></remarks>
    Public Class YakanSekkeiGetForCreateTsuchishoAndZairyouTargetVo

        '' 試作イベントコード  
        Private _ShisakuEventCode As String
        '' 試作部課コード  
        Private _ShisakuBukaCode As String
        '' 試作ブロック№  
        Private _ShisakuBlockNo As String
        '' 試作ブロック№改訂№  
        Private _ShisakuBlockNoKaiteiNo As String
        '' 部品番号表示順  
        Private _BuhinNoHyoujiJun As Int32?

        '' 部品番号  
        Private _BuhinNo As String

        ''' <summary>試作イベントコード</summary>
        ''' <value>試作イベントコード</value>
        ''' <returns>試作イベントコード</returns>
        Public Property ShisakuEventCode() As String
            Get
                Return _ShisakuEventCode
            End Get
            Set(ByVal value As String)
                _ShisakuEventCode = value
            End Set
        End Property

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

        ''' <summary>部品番号表示順</summary>
        ''' <value>部品番号表示順</value>
        ''' <returns>部品番号表示順</returns>
        Public Property BuhinNoHyoujiJun() As Int32?
            Get
                Return _BuhinNoHyoujiJun
            End Get
            Set(ByVal value As Int32?)
                _BuhinNoHyoujiJun = value
            End Set
        End Property



        ''' <summary>部品番号</summary>
        ''' <value>部品番号</value>
        ''' <returns>部品番号</returns>
        Public Property BuhinNo() As String
            Get
                Return _BuhinNo
            End Get
            Set(ByVal value As String)
                _BuhinNo = value
            End Set
        End Property
    End Class
End Namespace
