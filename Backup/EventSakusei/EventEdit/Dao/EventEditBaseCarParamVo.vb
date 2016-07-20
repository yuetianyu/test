Namespace EventEdit.Dao
    Friend Class EventEditBaseCarParamVo
        '' 開発符号 
        Private _KaihatsuFugo As String
        '' 仕様書一連No. 
        Private _ShiyoshoSeqno As String
        '' アプライドNo. 
        Private _AppliedNo As Nullable(Of Int32)
        '' 7桁型式符号
        Private _KatashikiFugo7 As String
        '' 仕向地コード 
        Private _ShimukechiCode As String
        '' OPコード 
        Private _OpCode As String
        '' 内外装区分 
        Private _NaigaisoKbn As String
        '' カラーコード
        Private _ColorCode As String


        ''' <summary>開発符号</summary>
        ''' <value>開発符号</value>
        ''' <returns>開発符号</returns>
        Public Property KaihatsuFugo() As String
            Get
                Return _KaihatsuFugo
            End Get
            Set(ByVal value As String)
                _KaihatsuFugo = value
            End Set
        End Property

        ''' <summary>仕様書一連No.</summary>
        ''' <value>仕様書一連No.</value>
        ''' <returns>仕様書一連No.</returns>
        Public Property ShiyoshoSeqno() As String
            Get
                Return _ShiyoshoSeqno
            End Get
            Set(ByVal value As String)
                _ShiyoshoSeqno = value
            End Set
        End Property

        ''' <summary>アプライドNo.</summary>
        ''' <value>アプライドNo.</value>
        ''' <returns>アプライドNo.</returns>
        Public Property AppliedNo() As Nullable(Of Int32)
            Get
                Return _AppliedNo
            End Get
            Set(ByVal value As Nullable(Of Int32))
                _AppliedNo = value
            End Set
        End Property

        ''' <summary>7桁型式符号</summary>
        ''' <value>7桁型式符号</value>
        ''' <returns>7桁型式符号</returns>
        Public Property KatashikiFugo7() As String
            Get
                Return _KatashikiFugo7
            End Get
            Set(ByVal value As String)
                _KatashikiFugo7 = value
            End Set
        End Property

        ''' <summary>仕向地コード</summary>
        ''' <value>仕向地コード</value>
        ''' <returns>仕向地コード</returns>
        Public Property ShimukechiCode() As String
            Get
                Return _ShimukechiCode
            End Get
            Set(ByVal value As String)
                _ShimukechiCode = value
            End Set
        End Property

        ''' <summary>OPコード</summary>
        ''' <value>OPコード</value>
        ''' <returns>OPコード</returns>
        Public Property OpCode() As String
            Get
                Return _OpCode
            End Get
            Set(ByVal value As String)
                _OpCode = value
            End Set
        End Property

        ''' <summary>内外装区分</summary>
        ''' <value>内外装区分</value>
        ''' <returns>内外装区分</returns>
        Public Property NaigaisoKbn() As String
            Get
                Return _NaigaisoKbn
            End Get
            Set(ByVal value As String)
                _NaigaisoKbn = value
            End Set
        End Property

        ''' <summary>カラーコード</summary>
        ''' <value>カラーコード</value>
        ''' <returns>カラーコード</returns>
        Public Property ColorCode() As String
            Get
                Return _ColorCode
            End Get
            Set(ByVal value As String)
                _ColorCode = value
            End Set
        End Property
    End Class
End Namespace