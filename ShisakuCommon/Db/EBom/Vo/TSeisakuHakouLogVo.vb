Namespace Db.EBom.Vo
    ''' <summary>
    ''' 製作一覧発行ログ情報
    ''' </summary>
    ''' <remarks></remarks>
    Public Class TSeisakuHakouLogVo
        '' 発行№
        Private _HakouNo As String
        '' 改訂№
        Private _KaiteiNo As String
        '' カウント№
        Private _No As Nullable(Of Int32)
        '' 最終出力日
        Private _SyutsuryokuDate As Nullable(Of Int32)
        '' 最終出力形態
        Private _SyutsuryokuKeitai As String
        '' 最終出力ユーザーID
        Private _SyutsuryokuUserId As String
        '' 中止フラグ
        Private _ChushiFlg As String
        '' 中止日
        Private _ChushiDate As Nullable(Of Int32)
        '' 中止ユーザーID
        Private _ChushiUserId As String
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

        ''' <summary>発行№</summary>
        ''' <value>発行№</value>
        ''' <returns>発行№</returns>
        Public Property HakouNo() As String
            Get
                Return _HakouNo
            End Get
            Set(ByVal value As String)
                _HakouNo = value
            End Set
        End Property

        ''' <summary>改訂№</summary>
        ''' <value>改訂№</value>
        ''' <returns>改訂№</returns>
        Public Property KaiteiNo() As String
            Get
                Return _KaiteiNo
            End Get
            Set(ByVal value As String)
                _KaiteiNo = value
            End Set
        End Property

        ''' <summary>カウント№</summary>
        ''' <value>カウント№</value>
        ''' <returns>カウント№</returns>
        Public Property No() As Nullable(Of Int32)
            Get
                Return _No
            End Get
            Set(ByVal value As Nullable(Of Int32))
                _No = value
            End Set
        End Property

        ''' <summary>最終出力日</summary>
        ''' <value>最終出力日</value>
        ''' <returns>最終出力日</returns>
        Public Property SyutsuryokuDate() As Nullable(Of Int32)
            Get
                Return _SyutsuryokuDate
            End Get
            Set(ByVal value As Nullable(Of Int32))
                _SyutsuryokuDate = value
            End Set
        End Property

        ''' <summary>最終出力形態</summary>
        ''' <value>最終出力形態</value>
        ''' <returns>最終出力形態</returns>
        Public Property SyutsuryokuKeitai() As String
            Get
                Return _SyutsuryokuKeitai
            End Get
            Set(ByVal value As String)
                _SyutsuryokuKeitai = value
            End Set
        End Property

        ''' <summary>最終出力ユーザーID</summary>
        ''' <value>最終出力ユーザーID</value>
        ''' <returns>最終出力ユーザーID</returns>
        Public Property SyutsuryokuUserId() As String
            Get
                Return _SyutsuryokuUserId
            End Get
            Set(ByVal value As String)
                _SyutsuryokuUserId = value
            End Set
        End Property

        ''' <summary>中止フラグ</summary>
        ''' <value>中止フラグ</value>
        ''' <returns>中止フラグ</returns>
        Public Property ChushiFlg() As String
            Get
                Return _ChushiFlg
            End Get
            Set(ByVal value As String)
                _ChushiFlg = value
            End Set
        End Property

        ''' <summary>中止日</summary>
        ''' <value>中止日</value>
        ''' <returns>中止日</returns>
        Public Property ChushiDate() As Nullable(Of Int32)
            Get
                Return _ChushiDate
            End Get
            Set(ByVal value As Nullable(Of Int32))
                _ChushiDate = value
            End Set
        End Property

        ''' <summary>中止ユーザーID</summary>
        ''' <value>中止ユーザーID</value>
        ''' <returns>中止ユーザーID</returns>
        Public Property ChushiUserId() As String
            Get
                Return _ChushiUserId
            End Get
            Set(ByVal value As String)
                _ChushiUserId = value
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
