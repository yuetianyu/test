Namespace ShisakuBuhinMenu.Dao
    Public Class ShisakuSekkeiBlockHeadVo
        ''' <summary>処置期限</summary>
        Private _KaiteiSyochiShimekiribi As Integer
        ''' <summary>処置期限</summary>
        Private _Shimekiribi As Integer
        ''' <summary>総ブロック数</summary>
        Private _Sousuu As Integer
        ''' <summary>完了数</summary>
        Private _Kanryou As Integer
        ''' <summary>Status</summary>
        Private _Status As String

        ''' <summary>
        ''' 処置期限
        ''' </summary>
        ''' <value>処置期限</value>
        ''' <returns>処置期限</returns>
        ''' <remarks>試作部品編集一覧から遷移した場合＝「KAITEI_SYOCHI_SHIMEKIRIBI」を設定</remarks>
        Public Property KaiteiSyochiShimekiribi() As String
            Get
                Return _KaiteiSyochiShimekiribi
            End Get
            Set(ByVal value As String)
                _KaiteiSyochiShimekiribi = value
            End Set
        End Property

        ''' <summary>
        ''' 処置期限
        ''' </summary>
        ''' <value>処置期限</value>
        ''' <returns>処置期限</returns>
        ''' <remarks>試作部品改訂編集一覧から遷移した場合＝「SHIMEKIRIBI」を設定</remarks>
        Public Property Shimekiribi() As String
            Get
                Return _Shimekiribi
            End Get
            Set(ByVal value As String)
                _Shimekiribi = value
            End Set
        End Property

        ''' <summary>
        ''' 総ブロック数
        ''' </summary>
        ''' <value>総ブロック数</value>
        ''' <returns>総ブロック数</returns>
        ''' <remarks></remarks>
        Public Property Sousuu() As String
            Get
                Return _Sousuu
            End Get
            Set(ByVal value As String)
                _Sousuu = value
            End Set
        End Property

        ''' <summary>
        ''' 完了数
        ''' </summary>
        ''' <value>完了数</value>
        ''' <returns>完了数</returns>
        ''' <remarks></remarks>
        Public Property Kanryou() As String
            Get
                Return _Kanryou
            End Get
            Set(ByVal value As String)
                _Kanryou = value
            End Set
        End Property

        ''' <summary>
        ''' 状態
        ''' </summary>
        ''' <value>状態</value>
        ''' <returns>状態</returns>
        ''' <remarks></remarks>
        Public Property Status() As String
            Get
                Return _Status
            End Get
            Set(ByVal value As String)
                _Status = value
            End Set
        End Property

    End Class
End Namespace

