Namespace YosanBuhinEdit.KouseiBuhin.Dao.Vo

    ''' <summary>
    ''' ＯＰスペック情報リストVo
    ''' </summary>
    ''' <remarks></remarks>
    Public Class OpSpecListVo

        '開発符号
        Private _KaihatsuFugo As String

        'リストコード
        Private _ListCode As String

        'ＯＰコード桁位置
        Private _OpcdKetaichi As Nullable(Of Int32)

        'ＯＰ記号
        Private _OpKigo As String

        'ＯＰコード
        Private _OpSpecCode As String

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

        ''' <summary>リストコード</summary>
        ''' <value>リストコード</value>
        ''' <returns>リストコード</returns>
        Public Property ListCode() As String
            Get
                Return _ListCode
            End Get
            Set(ByVal value As String)
                _ListCode = value
            End Set
        End Property

        ''' <summary>ＯＰ桁位置</summary>
        ''' <value>ＯＰ桁位置</value>
        ''' <returns>ＯＰ桁位置</returns>
        Public Property OpcdKetaichi() As Nullable(Of Int32)
            Get
                Return _OpcdKetaichi
            End Get
            Set(ByVal value As Nullable(Of Int32))
                _OpcdKetaichi = value
            End Set
        End Property

        ''' <summary>ＯＰ記号</summary>
        ''' <value>ＯＰ記号</value>
        ''' <returns>ＯＰ記号</returns>
        Public Property OpKigo() As String
            Get
                Return _OpKigo
            End Get
            Set(ByVal value As String)
                _OpKigo = value
            End Set
        End Property

        ''' <summary>ＯＰコード</summary>
        ''' <value>ＯＰコード</value>
        ''' <returns>ＯＰコード</returns>
        Public Property OpSpecCode() As String
            Get
                Return _OpSpecCode
            End Get
            Set(ByVal value As String)
                _OpSpecCode = value
            End Set
        End Property

    End Class

End Namespace

