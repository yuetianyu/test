Namespace Db.EBom.Vo
    ''' <summary>試作部品編集・号車情報（改訂抽出）</summary>
    Public Class TShisakuBuhinEditGousyaKaiteiVo
        ''' <summary>試作イベントコード</summary>
        Private _ShisakuEventCode As String
        ''' <summary>試作リストコード</summary>
        Private _ShisakuListCode As String
        ''' <summary>試作リストコード改訂№</summary>
        Private _ShisakuListCodeKaiteiNo As String
        ''' <summary>試作部課コード</summary>
        Private _ShisakuBukaCode As String
        ''' <summary>試作ブロック№</summary>
        Private _ShisakuBlockNo As String
        ''' <summary>試作ブロック№改訂№</summary>
        Private _ShisakuBlockNoKaiteiNo As String
        ''' <summary>前回試作ブロック№改訂№</summary>
        Private _ZenkaiShisakuBlockNoKaiteiNo As String
        ''' <summary>フラグ</summary>
        Private _Flag As String
        ''' <summary>部品番号表示順</summary>
        Private _BuhinNoHyoujiJun As Nullable(Of Int32)
        ''' <summary>試作号車表示順</summary>
        Private _ShisakuGousyaHyoujiJun As Nullable(Of Int32)
        ''' <summary>試作号車</summary>
        Private _ShisakuGousya As String
        ''' <summary>員数</summary>
        Private _InsuSuryo As Nullable(Of Int32)
        ''' <summary>メタル納入指示日</summary>
        Private _MNounyuShijibi As Nullable(Of Int32)
        ''' <summary>トリム納入指示日</summary>
        Private _TNounyuShijibi As Nullable(Of Int32)
        ''' <summary>作成ユーザーID</summary>
        Private _CreatedUserId As String
        ''' <summary>作成日</summary>
        Private _CreatedDate As String
        ''' <summary>作成時</summary>
        Private _CreatedTime As String
        ''' <summary>更新ユーザーID</summary>
        Private _UpdatedUserId As String
        ''' <summary>更新日</summary>
        Private _UpdatedDate As String
        ''' <summary>更新時間</summary>
        Private _UpdatedTime As String


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
        ''' <summary>試作リストコード</summary>
        ''' <value>試作リストコード</value>
        ''' <returns>試作リストコード</returns>
        Public Property ShisakuListCode() As String
            Get
                Return _ShisakuListCode
            End Get
            Set(ByVal value As String)
                _ShisakuListCode = value
            End Set
        End Property
        ''' <summary>試作リストコード改訂№</summary>
        ''' <value>試作リストコード改訂№</value>
        ''' <returns>試作リストコード改訂№</returns>
        Public Property ShisakuListCodeKaiteiNo() As String
            Get
                Return _ShisakuListCodeKaiteiNo
            End Get
            Set(ByVal value As String)
                _ShisakuListCodeKaiteiNo = value
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
        ''' <summary>前回試作ブロック№改訂№</summary>
        ''' <value>前回試作ブロック№改訂№</value>
        ''' <returns>前回試作ブロック№改訂№</returns>
        Public Property ZenkaiShisakuBlockNoKaiteiNo() As String
            Get
                Return _ZenkaiShisakuBlockNoKaiteiNo
            End Get
            Set(ByVal value As String)
                _ZenkaiShisakuBlockNoKaiteiNo = value
            End Set
        End Property
        ''' <summary>フラグ</summary>
        ''' <value>フラグ</value>
        ''' <returns>フラグ</returns>
        Public Property Flag() As String
            Get
                Return _Flag
            End Get
            Set(ByVal value As String)
                _Flag = value
            End Set
        End Property
        ''' <summary>部品番号表示順</summary>
        ''' <value>部品番号表示順</value>
        ''' <returns>部品番号表示順</returns>
        Public Property BuhinNoHyoujiJun() As Nullable(Of Int32)
            Get
                Return _BuhinNoHyoujiJun
            End Get
            Set(ByVal value As Nullable(Of Int32))
                _BuhinNoHyoujiJun = value
            End Set
        End Property
        ''' <summary>試作号車表示順</summary>
        ''' <value>試作号車表示順</value>
        ''' <returns>試作号車表示順</returns>
        Public Property ShisakuGousyaHyoujiJun() As Nullable(Of Int32)
            Get
                Return _ShisakuGousyaHyoujiJun
            End Get
            Set(ByVal value As Nullable(Of Int32))
                _ShisakuGousyaHyoujiJun = value
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
        ''' <summary>員数</summary>
        ''' <value>員数</value>
        ''' <returns>員数</returns>
        Public Property InsuSuryo() As Nullable(Of Int32)
            Get
                Return _InsuSuryo
            End Get
            Set(ByVal value As Nullable(Of Int32))
                _InsuSuryo = value
            End Set
        End Property
        ''' <summary>メタル納入指示日</summary>
        ''' <value>メタル納入指示日</value>
        ''' <returns>メタル納入指示日</returns>
        Public Property MNounyuShijibi() As String
            Get
                Return _MNounyuShijibi
            End Get
            Set(ByVal value As String)
                _MNounyuShijibi = value
            End Set
        End Property
        ''' <summary>トリム納入指示日</summary>
        ''' <value>トリム納入指示日</value>
        ''' <returns>トリム納入指示日</returns>
        Public Property TNounyuShijibi() As String
            Get
                Return _TNounyuShijibi
            End Get
            Set(ByVal value As String)
                _TNounyuShijibi = value
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