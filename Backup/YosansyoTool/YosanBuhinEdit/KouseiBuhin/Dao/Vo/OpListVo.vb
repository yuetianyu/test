Namespace YosanBuhinEdit.KouseiBuhin.Dao.Vo

    ''' <summary>
    ''' ＯＰ情報リストVo
    ''' </summary>
    ''' <remarks></remarks>
    Public Class OpListVo

        '開発符号
        Private _KaihatsuFugo As String

        'ＯＰコード
        Private _OpSpecCode As String

        'ＯＰ項目名
        Private _OpSpecName As String

        '列番号
        Private _HyojijunNo As Nullable(Of Int32)

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

        ''' <summary>ＯＰ項目名</summary>
        ''' <value>ＯＰ項目名</value>
        ''' <returns>ＯＰ項目名</returns>
        Public Property OpSpecName() As String
            Get
                Return _OpSpecName
            End Get
            Set(ByVal value As String)
                _OpSpecName = value
            End Set
        End Property

        ''' <summary>列番号</summary>
        ''' <value>列番号</value>
        ''' <returns>列番号</returns>
        Public Property HyojijunNo() As Nullable(Of Int32)
            Get
                Return _HyojijunNo
            End Get
            Set(ByVal value As Nullable(Of Int32))
                _HyojijunNo = value
            End Set
        End Property

    End Class

End Namespace

