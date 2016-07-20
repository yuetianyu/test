Namespace Db.EBom.Vo
    ''' <summary>
    ''' パーツプライスリストマスタ情報
    ''' </summary>
    ''' <remarks></remarks>
    Public Class AsPARTSPVo

        ''' <summary>部品番号１３</summary>
        Private _Buba13 As String
        ''' <summary>空白０１</summary>
        Private _Ku01 As String
        ''' <summary>部品名称</summary>
        Private _Bunm As String
        ''' <summary>空白０２</summary>
        Private _Ku02 As String
        ''' <summary>支給単価</summary>
        Private _Sitan As Nullable(Of Int32)
        ''' <summary>購入単価</summary>
        Private _Kntan As Nullable(Of Int32)
        ''' <summary>標準単価</summary>
        Private _Hjtan As Nullable(Of Int32)
        ''' <summary>空白０３</summary>
        Private _Ku03 As String
        ''' <summary>仮単価マーク</summary>
        Private _Ktmk As String
        ''' <summary>メーカーコード</summary>
        Private _Trcd As String
        ''' <summary>空白０４</summary>
        Private _Ku04 As String
        ''' <summary>購坦</summary>
        Private _Ka As String
        ''' <summary>空白０５</summary>
        Private _Ku05 As String
        ''' <summary>作成ユーザーID</summary>
        Private _CreatedUserId As String
        ''' <summary>作成日</summary>
        Private _CreatedDate As String
        ''' <summary>作成時間</summary>
        Private _CreatedTime As String
        ''' <summary>更新ユーザーID</summary>
        Private _UpdatedUserId As String
        ''' <summary>更新日</summary>
        Private _UpdatedDate As String
        ''' <summary>更新時間</summary>
        Private _UpdatedTime As String

        ''' <summary>部品番号１３</summary>
        ''' <value>部品番号１３</value>
        ''' <returns>部品番号１３</returns>
        Public Property Buba13() As String
            Get
                Return _Buba13
            End Get
            Set(ByVal value As String)
                _Buba13 = value
            End Set
        End Property

        ''' <summary>空白０１</summary>
        ''' <value>空白０１</value>
        ''' <returns>空白０１</returns>
        Public Property Ku01() As String
            Get
                Return _Ku01
            End Get
            Set(ByVal value As String)
                _Ku01 = value
            End Set
        End Property

        ''' <summary>部品名称</summary>
        ''' <value>部品名称</value>
        ''' <returns>部品名称</returns>
        Public Property Bunm() As String
            Get
                Return _Bunm
            End Get
            Set(ByVal value As String)
                _Bunm = value
            End Set
        End Property

        ''' <summary>空白０２</summary>
        ''' <value>空白０２</value>
        ''' <returns>空白０２</returns>
        Public Property Ku02() As String
            Get
                Return _Ku02
            End Get
            Set(ByVal value As String)
                _Ku02 = value
            End Set
        End Property

        ''' <summary>支給単価</summary>
        ''' <value>支給単価</value>
        ''' <returns>支給単価</returns>
        Public Property Sitan() As Nullable(Of Int32)
            Get
                Return _Sitan
            End Get
            Set(ByVal value As Nullable(Of Int32))
                _Sitan = value
            End Set
        End Property

        ''' <summary>購入単価</summary>
        ''' <value>購入単価</value>
        ''' <returns>購入単価</returns>
        Public Property Kntan() As Nullable(Of Int32)
            Get
                Return _Kntan
            End Get
            Set(ByVal value As Nullable(Of Int32))
                _Kntan = value
            End Set
        End Property

        ''' <summary>標準単価</summary>
        ''' <value>標準単価</value>
        ''' <returns>標準単価</returns>
        Public Property Hjtan() As Nullable(Of Int32)
            Get
                Return _Hjtan
            End Get
            Set(ByVal value As Nullable(Of Int32))
                _Hjtan = value
            End Set
        End Property

        ''' <summary>空白０３</summary>
        ''' <value>空白０３</value>
        ''' <returns>空白０３</returns>
        Public Property Ku03() As String
            Get
                Return _Ku03
            End Get
            Set(ByVal value As String)
                _Ku03 = value
            End Set
        End Property

        ''' <summary>仮単価マーク</summary>
        ''' <value>仮単価マーク</value>
        ''' <returns>仮単価マーク</returns>
        Public Property Ktmk() As String
            Get
                Return _Ktmk
            End Get
            Set(ByVal value As String)
                _Ktmk = value
            End Set
        End Property

        ''' <summary>メーカーコード</summary>
        ''' <value>メーカーコード</value>
        ''' <returns>メーカーコード</returns>
        Public Property Trcd() As String
            Get
                Return _Trcd
            End Get
            Set(ByVal value As String)
                _Trcd = value
            End Set
        End Property

        ''' <summary>空白０４</summary>
        ''' <value>空白０４</value>
        ''' <returns>空白０４</returns>
        Public Property Ku04() As String
            Get
                Return _Ku04
            End Get
            Set(ByVal value As String)
                _Ku04 = value
            End Set
        End Property

        ''' <summary>購坦</summary>
        ''' <value>購坦</value>
        ''' <returns>購坦</returns>
        Public Property Ka() As String
            Get
                Return _Ka
            End Get
            Set(ByVal value As String)
                _Ka = value
            End Set
        End Property

        ''' <summary>空白０５</summary>
        ''' <value>空白０５</value>
        ''' <returns>空白０５</returns>
        Public Property Ku05() As String
            Get
                Return _Ku05
            End Get
            Set(ByVal value As String)
                _Ku05 = value
            End Set
        End Property

        ''' <summary>作成ユーザーID</summary>
        ''' <value>作成ユーザーID</value>
        ''' <returns>作成ユーザーID</returns>
        Public Property CreatedUserId() As String
            Get
                Return _CreatedUserId
            End Get
            Set(ByVal value As String)
                _CreatedUserId = value
            End Set
        End Property

        ''' <summary>作成日</summary>
        ''' <value>作成日</value>
        ''' <returns>作成日</returns>
        Public Property CreatedDate() As String
            Get
                Return _CreatedDate
            End Get
            Set(ByVal value As String)
                _CreatedDate = value
            End Set
        End Property

        ''' <summary>作成時</summary>
        ''' <value>作成時</value>
        ''' <returns>作成時</returns>
        Public Property CreatedTime() As String
            Get
                Return _CreatedTime
            End Get
            Set(ByVal value As String)
                _CreatedTime = value
            End Set
        End Property

        ''' <summary>更新ユーザーID</summary>
        ''' <value>更新ユーザーID</value>
        ''' <returns>更新ユーザーID</returns>
        Public Property UpdatedUserId() As String
            Get
                Return _UpdatedUserId
            End Get
            Set(ByVal value As String)
                _UpdatedUserId = value
            End Set
        End Property

        ''' <summary>更新日</summary>
        ''' <value>更新日</value>
        ''' <returns>更新日</returns>
        Public Property UpdatedDate() As String
            Get
                Return _UpdatedDate
            End Get
            Set(ByVal value As String)
                _UpdatedDate = value
            End Set
        End Property

        ''' <summary>更新時間</summary>
        ''' <value>更新時間</value>
        ''' <returns>更新時間</returns>
        Public Property UpdatedTime() As String
            Get
                Return _UpdatedTime
            End Get
            Set(ByVal value As String)
                _UpdatedTime = value
            End Set
        End Property

    End Class
End Namespace