
Namespace ShisakuBuhinMenu.Dao
    ''' <summary>
    ''' 試作部品メニュー出力の集計部分（SHEET１）
    ''' </summary>
    ''' <remarks></remarks>
    Public Class ShisakuSekkeiBlockCountVo
        ''' <summary>設計課</summary>
        Private _ShisakuSekkeika As String
        ''' <summary>課略称</summary>
        Private _KaRyakuName As String
        ''' <summary>ブロック数</summary>
        Private _TotalBlock As Integer
        ''' <summary>処置完了数</summary>
        Private _TotalJyoutai As Integer
        ''' <summary>承認１完了数</summary>
        Private _TotalSyouninJyoutai As Integer
        ''' <summary>承認２完了数</summary>
        Private _TotalKachouSyouninJyoutai As Integer

        ''' <summary>
        ''' 設計課
        ''' </summary>
        ''' <value>設計課</value>
        ''' <returns>設計課</returns>
        ''' <remarks></remarks>
        Public Property ShisakuSekkeika() As String
            Get
                Return _ShisakuSekkeika
            End Get
            Set(ByVal value As String)
                _ShisakuSekkeika = value
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
        ''' ブロック数
        ''' </summary>
        ''' <value>ブロック数</value>
        ''' <returns>ブロック数</returns>
        ''' <remarks></remarks>
        Public Property TotalBlock() As String
            Get
                Return _TotalBlock
            End Get
            Set(ByVal value As String)
                _TotalBlock = value
            End Set
        End Property
        ''' <summary>
        ''' 処置完了数
        ''' </summary>
        ''' <value>処置完了数</value>
        ''' <returns>処置完了数</returns>
        ''' <remarks></remarks>
        Public Property TotalJyoutai() As String
            Get
                Return _TotalJyoutai
            End Get
            Set(ByVal value As String)
                _TotalJyoutai = value
            End Set
        End Property
        ''' <summary>
        ''' 承認１完了数
        ''' </summary>
        ''' <value></value>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Property TotalSyouninJyoutai() As String
            Get
                Return _TotalSyouninJyoutai
            End Get
            Set(ByVal value As String)
                _TotalSyouninJyoutai = value
            End Set
        End Property

        ''' <summary>
        ''' 承認２完了数
        ''' </summary>
        ''' <value>承認２完了数</value>
        ''' <returns>承認２完了数</returns>
        ''' <remarks></remarks>
        Public Property TotalKachouSyouninJyoutai() As String
            Get
                Return _TotalKachouSyouninJyoutai
            End Get
            Set(ByVal value As String)
                _TotalKachouSyouninJyoutai = value
            End Set
        End Property


    End Class

End Namespace


