
Namespace ShisakuBuhinEditBlock.Dao

    Friend Class EditBlock2ExcelParamVo

        '' 試作イベントコード
        Private _ShisakuEventCode As String
        '' 試作設計課 
        Private _ShisakuBukaCode As String
        '' 試作ブロックNo 
        Private _ShisakuBlockNo As String
        '' 試作ブロックNo. 改訂No. 
        Private _ShisakuBlockNoKaiteiNo As String
        '' 号車. 
        Private _ShisakuGousya As String

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

        ''' <summary>試作設計課 </summary>
        ''' <value>試作設計課 </value>
        ''' <returns>試作設計課 </returns>
        Public Property ShisakuBukaCode() As String
            Get
                Return _ShisakuBukaCode
            End Get
            Set(ByVal value As String)
                _ShisakuBukaCode = value
            End Set
        End Property

        ''' <summary>試作ブロックNo </summary>
        ''' <value>試作ブロックNo </value>
        ''' <returns>試作ブロックNo </returns>
        Public Property ShisakuBlockNo() As String
            Get
                Return _ShisakuBlockNo
            End Get
            Set(ByVal value As String)
                _ShisakuBlockNo = value
            End Set
        End Property

        ''' <summary>試作ブロックNo. 改訂No. </summary>
        ''' <value>試作ブロックNo. 改訂No. </value>
        ''' <returns>試作ブロックNo. 改訂No. </returns>
        Public Property ShisakuBlockNoKaiteiNo() As String
            Get
                Return _ShisakuBlockNoKaiteiNo
            End Get
            Set(ByVal value As String)
                _ShisakuBlockNoKaiteiNo = value
            End Set
        End Property

        ''' <summary>号車</summary>
        ''' <value>号車</value>
        ''' <returns>号車 </returns>
        Public Property ShisakuGousya() As String
            Get
                Return _ShisakuGousya
            End Get
            Set(ByVal value As String)
                _ShisakuGousya = value
            End Set
        End Property

    End Class

End Namespace