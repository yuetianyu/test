Namespace Db.EBom.Vo
    ''' <summary>
    ''' 仕様書
    ''' </summary>
    ''' <remarks></remarks>
    Public Class Rhac0030Vo
        '' 開発符号 
        Private _KaihatsuFugo As String
        '' 装備改訂No. 
        Private _SobiKaiteiNo As String
        '' ラインOP改訂No. 
        Private _LineOpKaiteiNo As String
        '' 内外装色改訂No. 
        Private _NaigaisoKaiteiNo As String
        '' 仕様書一連No. 
        Private _ShiyoshoSeqno As String
        '' 量試区分 
        Private _RyoshiKbn As String
        '' 改訂区分 
        Private _KaiteiKbn As String
        '' 承認日付 
        Private _ShoninDate As Nullable(of Int32)
        '' 承認社員番号 
        Private _ShoninShainNo As String
        '' 処置期限 
        Private _ShochiKigen As Nullable(of Int32)
        '' A/L造成要否 
        Private _AlZouseiYohi As String
        '' 作成ユーザーID
        Private _CreatedUserId As String
        '' 作成日
        Private _CreatedDate As String
        '' 作成時
        Private _CreatedTime As String
        '' 更新ユーザーID
        Private _UpdatedUserId As String
        '' 更新日
        Private _UpdatedDate As String
        '' 更新時間
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

        ''' <summary>装備改訂No.</summary>
        ''' <value>装備改訂No.</value>
        ''' <returns>装備改訂No.</returns>
        Public Property SobiKaiteiNo() As String
            Get
                Return _SobiKaiteiNo
            End Get
            Set(ByVal value As String)
                _SobiKaiteiNo = value
            End Set
        End Property

        ''' <summary>ラインOP改訂No.</summary>
        ''' <value>ラインOP改訂No.</value>
        ''' <returns>ラインOP改訂No.</returns>
        Public Property LineOpKaiteiNo() As String
            Get
                Return _LineOpKaiteiNo
            End Get
            Set(ByVal value As String)
                _LineOpKaiteiNo = value
            End Set
        End Property

        ''' <summary>内外装色改訂No.</summary>
        ''' <value>内外装色改訂No.</value>
        ''' <returns>内外装色改訂No.</returns>
        Public Property NaigaisoKaiteiNo() As String
            Get
                Return _NaigaisoKaiteiNo
            End Get
            Set(ByVal value As String)
                _NaigaisoKaiteiNo = value
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

        ''' <summary>改訂区分</summary>
        ''' <value>改訂区分</value>
        ''' <returns>改訂区分</returns>
        Public Property KaiteiKbn() As String
            Get
                Return _KaiteiKbn
            End Get
            Set(ByVal value As String)
                _KaiteiKbn = value
            End Set
        End Property

        ''' <summary>承認日付</summary>
        ''' <value>承認日付</value>
        ''' <returns>承認日付</returns>
        Public Property ShoninDate() As Nullable(of Int32)
            Get
                Return _ShoninDate
            End Get
            Set(ByVal value As Nullable(of Int32))
                _ShoninDate = value
            End Set
        End Property

        ''' <summary>承認社員番号</summary>
        ''' <value>承認社員番号</value>
        ''' <returns>承認社員番号</returns>
        Public Property ShoninShainNo() As String
            Get
                Return _ShoninShainNo
            End Get
            Set(ByVal value As String)
                _ShoninShainNo = value
            End Set
        End Property

        ''' <summary>処置期限</summary>
        ''' <value>処置期限</value>
        ''' <returns>処置期限</returns>
        Public Property ShochiKigen() As Nullable(of Int32)
            Get
                Return _ShochiKigen
            End Get
            Set(ByVal value As Nullable(of Int32))
                _ShochiKigen = value
            End Set
        End Property

        ''' <summary>A/L造成要否</summary>
        ''' <value>A/L造成要否</value>
        ''' <returns>A/L造成要否</returns>
        Public Property AlZouseiYohi() As String
            Get
                Return _AlZouseiYohi
            End Get
            Set(ByVal value As String)
                _AlZouseiYohi = value
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
