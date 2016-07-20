Namespace Db.EBom.Vo
    ''' <summary>
    ''' 試作12桁型式-F品番
    ''' </summary>
    ''' <remarks></remarks>
    Public Class TShisakuFBuhinVo
        ''試作イベントコード	
        Private _ShisakuEventCode As String
        '' 試作部課コード
        Private _ShisakuBukaCode As String
        ''試作ブロック№	
        Private _ShisakuBlockNo As String
        ''試作ブロック№改訂№	
        Private _ShisakuBlockNoKaiteiNo As String
        ''INSTL品番	
        Private _InstlHinban As String
        ''INSTL品番区分	
        Private _InstlHinbanKbn As String
        ''Ｆ品番	
        Private _FBuhinNo As String
        ''Ｆ品番区分	
        Private _FBuhinNoKbn As String
        ''開発符号	
        Private _KaihatsuFugo As String
        ''装備改訂No	
        Private _SobiKaiteiNo As String
        ''7桁型式識別コード	
        Private _KatashikiScd7 As String
        ''仕向地コード	
        Private _ShimukechiCode As String
        ''列No	
        Private _ColNo As Nullable(Of Int32)
        ''仕様差品番改訂No.	
        Private _ShiyosaKaiteiNo As String
        ''部位コード	
        Private _BuiCode As String
        ''装備仕様差組合せコード	
        Private _KumiawaseCodeSo As Nullable(Of Int32)
        ''採用年月日	
        Private _SaiyoDate As Nullable(Of Int32)
        ''廃止年月日	
        Private _HaisiDate As Nullable(Of Int32)
        ''作成ユーザーID	
        Private _CreatedUserId As String
        ''作成日	
        Private _CreatedDate As String
        ''作成時	
        Private _CreatedTime As String
        ''更新ユーザーID	
        Private _UpadtedUserId As String
        ''更新日	
        Private _UpadtedDate As String
        ''更新時間	
        Private _UpadtedTime As String


        ''' <summary>''試作イベントコード</summary>
        ''' <value>''試作イベントコード</value>
        ''' <returns>''試作イベントコード</returns>
        Public Property ShisakuEventCode() As String
            Get
                Return _ShisakuEventCode
            End Get
            Set(ByVal value As String)
                _ShisakuEventCode = value
            End Set
        End Property
        ''' <summary>''試作部課コード</summary>
        ''' <value>''試作部課コード</value>
        ''' <returns>''試作部課コード</returns>
        Public Property ShisakuBukaCode() As String
            Get
                Return _ShisakuBukaCode
            End Get
            Set(ByVal value As String)
                _ShisakuBukaCode = value
            End Set
        End Property
        ''' <summary>''試作ブロック№</summary>
        ''' <value>''試作ブロック№</value>
        ''' <returns>''試作ブロック№</returns>
        Public Property ShisakuBlockNo() As String
            Get
                Return _ShisakuBlockNo
            End Get
            Set(ByVal value As String)
                _ShisakuBlockNo = value
            End Set
        End Property
        ''' <summary>''試作ブロック№改訂№</summary>
        ''' <value>''試作ブロック№改訂№</value>
        ''' <returns>''試作ブロック№改訂№</returns>
        Public Property ShisakuBlockNoKaiteiNo() As String
            Get
                Return _ShisakuBlockNoKaiteiNo
            End Get
            Set(ByVal value As String)
                _ShisakuBlockNoKaiteiNo = value
            End Set
        End Property
        ''' <summary>''INSTL品番</summary>
        ''' <value>''INSTL品番</value>
        ''' <returns>''INSTL品番</returns>
        Public Property InstlHinban() As String
            Get
                Return _InstlHinban
            End Get
            Set(ByVal value As String)
                _InstlHinban = value
            End Set
        End Property
        ''' <summary>''INSTL品番区分</summary>
        ''' <value>''INSTL品番区分</value>
        ''' <returns>''INSTL品番区分</returns>
        Public Property InstlHinbanKbn() As String
            Get
                Return _InstlHinbanKbn
            End Get
            Set(ByVal value As String)
                _InstlHinbanKbn = value
            End Set
        End Property
        ''' <summary>''開発符号</summary>
        ''' <value>''開発符号</value>
        ''' <returns>''開発符号</returns>
        Public Property KaihatsuFugo() As String
            Get
                Return _KaihatsuFugo
            End Get
            Set(ByVal value As String)
                _KaihatsuFugo = value
            End Set
        End Property
        ''' <summary>''装備改訂No</summary>
        ''' <value>''装備改訂No</value>
        ''' <returns>''装備改訂No</returns>
        Public Property SobiKaiteiNo() As String
            Get
                Return _SobiKaiteiNo
            End Get
            Set(ByVal value As String)
                _SobiKaiteiNo = value
            End Set
        End Property
        ''' <summary>''7桁型式識別コード</summary>
        ''' <value>''7桁型式識別コード</value>
        ''' <returns>''7桁型式識別コード</returns>
        Public Property KatashikiScd7() As String
            Get
                Return _KatashikiScd7
            End Get
            Set(ByVal value As String)
                _KatashikiScd7 = value
            End Set
        End Property
        ''' <summary>''仕向地コード</summary>
        ''' <value>''仕向地コード</value>
        ''' <returns>''仕向地コード</returns>
        Public Property ShimukechiCode() As String
            Get
                Return _ShimukechiCode
            End Get
            Set(ByVal value As String)
                _ShimukechiCode = value
            End Set
        End Property
        ''' <summary>''列No</summary>
        ''' <value>''列No</value>
        ''' <returns>''列No</returns>
        Public Property ColNo() As Nullable(Of Int32)
            Get
                Return _ColNo
            End Get
            Set(ByVal value As Nullable(Of Int32))
                _ColNo = value
            End Set
        End Property
        ''' <summary>''Ｆ品番</summary>
        ''' <value>''Ｆ品番</value>
        ''' <returns>''Ｆ品番</returns>
        Public Property FBuhinNo() As String
            Get
                Return _FBuhinNo
            End Get
            Set(ByVal value As String)
                _FBuhinNo = value
            End Set
        End Property
        ''' <summary>''Ｆ品番区分</summary>
        ''' <value>''Ｆ品番区分</value>
        ''' <returns>''Ｆ品番区分</returns>
        Public Property FBuhinNoKbn() As String
            Get
                Return _FBuhinNoKbn
            End Get
            Set(ByVal value As String)
                _FBuhinNoKbn = value
            End Set
        End Property
        ''' <summary>''仕様差品番改訂No.</summary>
        ''' <value>''仕様差品番改訂No.</value>
        ''' <returns>''仕様差品番改訂No.</returns>
        Public Property ShiyosaKaiteiNo() As String
            Get
                Return _ShiyosaKaiteiNo
            End Get
            Set(ByVal value As String)
                _ShiyosaKaiteiNo = value
            End Set
        End Property
        ''' <summary>''部位コード</summary>
        ''' <value>''部位コード</value>
        ''' <returns>''部位コード</returns>
        Public Property BuiCode() As String
            Get
                Return _BuiCode
            End Get
            Set(ByVal value As String)
                _BuiCode = value
            End Set
        End Property
        ''' <summary>''装備仕様差組合せコード</summary>
        ''' <value>''装備仕様差組合せコード</value>
        ''' <returns>''装備仕様差組合せコード</returns>
        Public Property KumiawaseCodeSo() As Nullable(Of Int32)
            Get
                Return _KumiawaseCodeSo
            End Get
            Set(ByVal value As Nullable(Of Int32))
                _KumiawaseCodeSo = value
            End Set
        End Property
        ''' <summary>''採用年月日</summary>
        ''' <value>''採用年月日</value>
        ''' <returns>''採用年月日</returns>
        Public Property SaiyoDate() As Nullable(Of Int32)
            Get
                Return _SaiyoDate
            End Get
            Set(ByVal value As Nullable(Of Int32))
                _SaiyoDate = value
            End Set
        End Property
        ''' <summary>''廃止年月日</summary>
        ''' <value>''廃止年月日</value>
        ''' <returns>''廃止年月日</returns>
        Public Property HaisiDate() As Nullable(Of Int32)
            Get
                Return _HaisiDate
            End Get
            Set(ByVal value As Nullable(Of Int32))
                _HaisiDate = value
            End Set
        End Property

        ''' <summary>''作成ユーザーID</summary>
        ''' <value>''作成ユーザーID</value>
        ''' <returns>''作成ユーザーID</returns>
        Public Property CreatedUserId() As String
            Get
                Return _CreatedUserId
            End Get
            Set(ByVal value As String)
                _CreatedUserId = value
            End Set
        End Property

        ''' <summary>''作成日</summary>
        ''' <value>''作成日D</value>
        ''' <returns>''作成日</returns>
        Public Property CreatedDate() As String
            Get
                Return _CreatedDate
            End Get
            Set(ByVal value As String)
                _CreatedDate = value
            End Set
        End Property

        ''' <summary>''作成時</summary>
        ''' <value>''作成時</value>
        ''' <returns>''作成時</returns>
        Public Property CreatedTime() As String
            Get
                Return _CreatedTime
            End Get
            Set(ByVal value As String)
                _CreatedTime = value
            End Set
        End Property


        ''' <summary>''更新ユーザーID</summary>
        ''' <value>''更新ユーザーID</value>
        ''' <returns>''更新ユーザーID</returns>
        Public Property UpadtedUserId() As String
            Get
                Return _UpadtedUserId
            End Get
            Set(ByVal value As String)
                _UpadtedUserId = value
            End Set
        End Property

        ''' <summary>''更新日</summary>
        ''' <value>''更新日</value>
        ''' <returns>''更新日</returns>
        Public Property UpadtedDate() As String
            Get
                Return _UpadtedDate
            End Get
            Set(ByVal value As String)
                _UpadtedDate = value
            End Set
        End Property

        ''' <summary>''更新時間</summary>
        ''' <value>''更新時間</value>
        ''' <returns>''更新時間</returns>
        Public Property UpadtedTime() As String
            Get
                Return _UpadtedTime
            End Get
            Set(ByVal value As String)
                _UpadtedTime = value
            End Set
        End Property

    End Class

End Namespace


