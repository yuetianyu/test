Namespace Db.EBom.Vo
    ''' <summary>
    ''' ALの素
    ''' </summary>
    ''' <remarks></remarks>
    Public Class Rhac1500Vo

        ''開発符号
        Private _KaihatsuFugo As String
        ''仕様書一連No.
        Private _ShiyoshoSeqno As String
        ''A/L管理No.
        Private _AlKanriNo As String
        ''ブロックNo.
        Private _BlockNo As String
        ''適用区分
        Private _TekiyoKbn As String
        ''型式符号
        Private _KatashikiFugo As String
        ''仕向地コード
        Private _ShimukechiCode As String
        ''OPコード
        Private _OpCode As String
        ''カラースペックコー
        Private _ColorSpecCode As String
        ''付加F品番
        Private _FfBuhinNo As String
        ''基本F品番
        Private _BfBuhinNo As String
        ''リストコード
        Private _ListCode As String
        ''量試区分
        Private _RyoshiKbn As String
        ''アプライドNo.
        Private _AppliedNo As Nullable(Of Int32)
        ''AL出力用グレードコード
        Private _AlGradeCode As String
        ''OPスペック並び
        Private _OpSpecRow As String
        ''仕向地スペックコード
        Private _ShimukechiSpecCode As String
        ''設計メモ
        Private _SekkeiMemo As String
        ''ブロック名称
        Private _BlockName As String
        ''設計社員名称
        Private _SekkeiShainName As String
        ''内線番号
        Private _NaisenNo As String
        ''サイト区分
        Private _SiteKbn As String
        ''部課コード
        Private _BukaCode As String
        ''A/Lの素作成日
        Private _AlmMakeDate As String
        ''A/Lの素作成時刻
        Private _AlmMakeTime As String
        ''作成ユーザーID
        Private _CreatedUserId As String
        ''作成年月日
        Private _CreatedDate As String
        ''作成時分秒
        Private _CreatedTime As String
        ''更新ユーザーID
        Private _UpdatedUserId As String
        ''更新年月日
        Private _UpdatedDate As String
        ''更新時分秒
        Private _UpdatedTime As String



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

        ''' <summary>A/L管理No.</summary>
        ''' <value>A/L管理No.</value>
        ''' <returns>A/L管理No.</returns>
        Public Property AlKanriNo() As String
            Get
                Return _AlKanriNo
            End Get
            Set(ByVal value As String)
                _AlKanriNo = value
            End Set
        End Property

        ''' <summary>ブロックNo.</summary>
        ''' <value>ブロックNo.</value>
        ''' <returns>ブロックNo.</returns>
        Public Property BlockNo() As String
            Get
                Return _BlockNo
            End Get
            Set(ByVal value As String)
                _BlockNo = value
            End Set
        End Property

        ''' <summary>適用区分</summary>
        ''' <value>適用区分</value>
        ''' <returns>適用区分</returns>
        Public Property TekiyoKbn() As String
            Get
                Return _TekiyoKbn
            End Get
            Set(ByVal value As String)
                _TekiyoKbn = value
            End Set
        End Property

        ''' <summary>型式符号</summary>
        ''' <value>型式符号</value>
        ''' <returns>型式符号</returns>
        Public Property KatashikiFugo() As String
            Get
                Return _KatashikiFugo
            End Get
            Set(ByVal value As String)
                _KatashikiFugo = value
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

        ''' <summary>カラースペックコード</summary>
        ''' <value>カラースペックコード</value>
        ''' <returns>カラースペックコード</returns>
        Public Property ColorSpecCode() As String
            Get
                Return _ColorSpecCode
            End Get
            Set(ByVal value As String)
                _ColorSpecCode = value
            End Set
        End Property

        ''' <summary>付加F品番</summary>
        ''' <value>付加F品番</value>
        ''' <returns>付加F品番</returns>
        Public Property FfBuhinNo() As String
            Get
                Return _FfBuhinNo
            End Get
            Set(ByVal value As String)
                _FfBuhinNo = value
            End Set
        End Property

        ''' <summary>基本F品番</summary>
        ''' <value>基本F品番</value>
        ''' <returns>基本F品番</returns>
        Public Property BfBuhinNo() As String
            Get
                Return _BfBuhinNo
            End Get
            Set(ByVal value As String)
                _BfBuhinNo = value
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

        ''' <summary>量試区分</summary>
        ''' <value>量試区分</value>
        ''' <returns>量試区分</returns>
        Public Property RyoshiKbn() As String
            Get
                Return _RyoshiKbn
            End Get
            Set(ByVal value As String)
                _RyoshiKbn = value
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

        ''' <summary>AL出力用グレードコード</summary>
        ''' <value>AL出力用グレードコード</value>
        ''' <returns>AL出力用グレードコード</returns>
        Public Property AlGradeCode() As String
            Get
                Return _AlGradeCode
            End Get
            Set(ByVal value As String)
                _AlGradeCode = value
            End Set
        End Property

        ''' <summary>OPスペック並び</summary>
        ''' <value>OPスペック並び</value>
        ''' <returns>OPスペック並び</returns>
        Public Property OpSpecRow() As String
            Get
                Return _OpSpecRow
            End Get
            Set(ByVal value As String)
                _OpSpecRow = value
            End Set
        End Property

        ''' <summary>仕向地スペックコード</summary>
        ''' <value>仕向地スペックコード</value>
        ''' <returns>仕向地スペックコード</returns>
        Public Property ShimukechiSpecCode() As String
            Get
                Return _ShimukechiSpecCode
            End Get
            Set(ByVal value As String)
                _ShimukechiSpecCode = value
            End Set
        End Property

        ''' <summary>設計メモ</summary>
        ''' <value>設計メモ</value>
        ''' <returns>設計メモ</returns>
        Public Property SekkeiMemo() As String
            Get
                Return _SekkeiMemo
            End Get
            Set(ByVal value As String)
                _SekkeiMemo = value
            End Set
        End Property

        ''' <summary>ブロック名称</summary>
        ''' <value>ブロック名称</value>
        ''' <returns>ブロック名称</returns>
        Public Property BlockName() As String
            Get
                Return _BlockName
            End Get
            Set(ByVal value As String)
                _BlockName = value
            End Set
        End Property


        ''' <summary>設計社員名称</summary>
        ''' <value>設計社員名称</value>
        ''' <returns>設計社員名称</returns>
        Public Property SekkeiShainName() As String
            Get
                Return _SekkeiShainName
            End Get
            Set(ByVal value As String)
                _SekkeiShainName = value
            End Set
        End Property

        ''' <summary>内線番号</summary>
        ''' <value>内線番号</value>
        ''' <returns>内線番号</returns>
        Public Property NaisenNo() As String
            Get
                Return _NaisenNo
            End Get
            Set(ByVal value As String)
                _NaisenNo = value
            End Set
        End Property

        ''' <summary>サイト区分</summary>
        ''' <value>サイト区分</value>
        ''' <returns>サイト区分</returns>
        Public Property SiteKbn() As String
            Get
                Return _SiteKbn
            End Get
            Set(ByVal value As String)
                _SiteKbn = value
            End Set
        End Property

        ''' <summary>部課コード</summary>
        ''' <value>部課コード</value>
        ''' <returns>部課コード</returns>
        Public Property BukaCode() As String
            Get
                Return _BukaCode
            End Get
            Set(ByVal value As String)
                _BukaCode = value
            End Set
        End Property

        ''' <summary>A/Lの素作成日</summary>
        ''' <value>A/Lの素作成日</value>
        ''' <returns>A/Lの素作成日</returns>
        Public Property AlmMakeDate() As String
            Get
                Return _AlmMakeDate
            End Get
            Set(ByVal value As String)
                _AlmMakeDate = value
            End Set
        End Property

        ''' <summary>A/Lの素作成時刻</summary>
        ''' <value>A/Lの素作成時刻</value>
        ''' <returns>A/Lの素作成時刻</returns>
        Public Property AlmMakeTime() As String
            Get
                Return _AlmMakeTime
            End Get
            Set(ByVal value As String)
                _AlmMakeTime = value
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