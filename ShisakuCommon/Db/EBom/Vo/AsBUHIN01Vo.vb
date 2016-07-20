Namespace Db.EBom.Vo
    ''' <summary>
    ''' 部品マスタ情報
    ''' </summary>
    ''' <remarks></remarks>
    Public Class AsBUHIN01Vo

        ''' <summary>設通シリーズ</summary>
        Private _Stsr As String
        ''' <summary>量試区分</summary>
        Private _Rskm As String
        ''' <summary>設通 NO.</summary>
        Private _Dhstba As String
        ''' <summary>設通改訂</summary>
        Private _Kdba As String
        ''' <summary>設通改訂２</summary>
        Private _Kdba2 As String
        ''' <summary>タイトル品番</summary>
        Private _Tzzmba As String
        ''' <summary>図面改訂</summary>
        Private _Tzkdba As String
        ''' <summary>分類</summary>
        Private _Bunrui As String
        ''' <summary>購担</summary>
        Private _Kotan As String
        ''' <summary>取引先</summary>
        Private _Maker As String
        ''' <summary>受信日</summary>
        Private _Judate As Nullable(Of Int32)
        ''' <summary>購技承認日</summary>
        Private _Kogidate As Nullable(Of Int32)
        ''' <summary>窓口承認日</summary>
        Private _Madodate As Nullable(Of Int32)
        ''' <summary>取引先  受領確認日</summary>
        Private _Mkdate As Nullable(Of Int32)
        ''' <summary>状況</summary>
        Private _Jokyo As String
        ''' <summary>レコード追加日</summary>
        Private _Credate As Nullable(Of Int32)
        ''' <summary>レコード追加時間</summary>
        Private _Cretime As Nullable(Of Int32)
        ''' <summary>レコード更新日</summary>
        Private _Upddate As Nullable(Of Int32)
        ''' <summary>レコード更新時間</summary>
        Private _Updtime As Nullable(Of Int32)
        ''' <summary>更新プログラム</summary>
        Private _Updpgm As String
        ''' <summary>集合図面－図面番号</summary>
        Private _Gzzcp As String
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

        ''' <summary>設通シリーズ</summary>
        ''' <value>設通シリーズ</value>
        ''' <returns>設通シリーズ</returns>
        Public Property Stsr() As String
            Get
                Return _Stsr
            End Get
            Set(ByVal value As String)
                _Stsr = value
            End Set
        End Property

        ''' <summary>量試区分</summary>
        ''' <value>量試区分</value>
        ''' <returns>量試区分</returns>
        Public Property Rskm() As String
            Get
                Return _Rskm
            End Get
            Set(ByVal value As String)
                _Rskm = value
            End Set
        End Property

        ''' <summary>設通№</summary>
        ''' <value>設通№</value>
        ''' <returns>設通№</returns>
        Public Property Dhstba() As String
            Get
                Return _Dhstba
            End Get
            Set(ByVal value As String)
                _Dhstba = value
            End Set
        End Property

        ''' <summary>設通改訂</summary>
        ''' <value>設通改訂</value>
        ''' <returns>設通改訂</returns>
        Public Property Kdba() As String
            Get
                Return _Kdba
            End Get
            Set(ByVal value As String)
                _Kdba = value
            End Set
        End Property

        ''' <summary>設通改訂２</summary>
        ''' <value>設通改訂２</value>
        ''' <returns>設通改訂２</returns>
        Public Property Kdba2() As String
            Get
                Return _Kdba2
            End Get
            Set(ByVal value As String)
                _Kdba2 = value
            End Set
        End Property

        ''' <summary>タイトル品番</summary>
        ''' <value>タイトル品番</value>
        ''' <returns>タイトル品番</returns>
        Public Property Tzzmba() As String
            Get
                Return _Tzzmba
            End Get
            Set(ByVal value As String)
                _Tzzmba = value
            End Set
        End Property

        ''' <summary>図面改訂</summary>
        ''' <value>図面改訂</value>
        ''' <returns>図面改訂</returns>
        Public Property Tzkdba() As String
            Get
                Return _Tzkdba
            End Get
            Set(ByVal value As String)
                _Tzkdba = value
            End Set
        End Property

        ''' <summary>分類</summary>
        ''' <value>分類</value>
        ''' <returns>分類</returns>
        Public Property Bunrui() As String
            Get
                Return _Bunrui
            End Get
            Set(ByVal value As String)
                _Bunrui = value
            End Set
        End Property

        ''' <summary>購担</summary>
        ''' <value>購担</value>
        ''' <returns>購担</returns>
        Public Property Kotan() As String
            Get
                Return _Kotan
            End Get
            Set(ByVal value As String)
                _Kotan = value
            End Set
        End Property

        ''' <summary>取引先</summary>
        ''' <value>取引先</value>
        ''' <returns>取引先</returns>
        Public Property Maker() As String
            Get
                Return _Maker
            End Get
            Set(ByVal value As String)
                _Maker = value
            End Set
        End Property

        ''' <summary>受信日</summary>
        ''' <value>受信日</value>
        ''' <returns>受信日</returns>
        Public Property Judate() As Nullable(Of Int32)
            Get
                Return _Judate
            End Get
            Set(ByVal value As Nullable(Of Int32))
                _Judate = value
            End Set
        End Property

        ''' <summary>購技承認日</summary>
        ''' <value>購技承認日</value>
        ''' <returns>購技承認日</returns>
        Public Property Kogidate() As Nullable(Of Int32)
            Get
                Return _Kogidate
            End Get
            Set(ByVal value As Nullable(Of Int32))
                _Kogidate = value
            End Set
        End Property

        ''' <summary>窓口承認日</summary>
        ''' <value>窓口承認日</value>
        ''' <returns>窓口承認日</returns>
        Public Property Madodate() As Nullable(Of Int32)
            Get
                Return _Madodate
            End Get
            Set(ByVal value As Nullable(Of Int32))
                _Madodate = value
            End Set
        End Property

        ''' <summary>取引先  受領確認日</summary>
        ''' <value>取引先  受領確認日</value>
        ''' <returns>取引先  受領確認日</returns>
        Public Property Mkdate() As Nullable(Of Int32)
            Get
                Return _Mkdate
            End Get
            Set(ByVal value As Nullable(Of Int32))
                _Mkdate = value
            End Set
        End Property

        ''' <summary>状況</summary>
        ''' <value>状況</value>
        ''' <returns>状況</returns>
        Public Property Jokyo() As String
            Get
                Return _Jokyo
            End Get
            Set(ByVal value As String)
                _Jokyo = value
            End Set
        End Property

        ''' <summary>レコード追加日</summary>
        ''' <value>レコード追加日</value>
        ''' <returns>レコード追加日</returns>
        Public Property Credate() As Nullable(Of Int32)
            Get
                Return _Credate
            End Get
            Set(ByVal value As Nullable(Of Int32))
                _Credate = value
            End Set
        End Property

        ''' <summary>レコード追加時間</summary>
        ''' <value>レコード追加時間</value>
        ''' <returns>レコード追加時間</returns>
        Public Property Cretime() As Nullable(Of Int32)
            Get
                Return _Cretime
            End Get
            Set(ByVal value As Nullable(Of Int32))
                _Cretime = value
            End Set
        End Property

        ''' <summary>レコード更新日</summary>
        ''' <value>レコード更新日</value>
        ''' <returns>レコード更新日</returns>
        Public Property Upddate() As Nullable(Of Int32)
            Get
                Return _Upddate
            End Get
            Set(ByVal value As Nullable(Of Int32))
                _Upddate = value
            End Set
        End Property

        ''' <summary>レコード更新時間</summary>
        ''' <value>レコード更新時間</value>
        ''' <returns>レコード更新時間</returns>
        Public Property Updtime() As Nullable(Of Int32)
            Get
                Return _Updtime
            End Get
            Set(ByVal value As Nullable(Of Int32))
                _Updtime = value
            End Set
        End Property

        ''' <summary>更新プログラム</summary>
        ''' <value>更新プログラム</value>
        ''' <returns>更新プログラム</returns>
        Public Property Updpgm() As String
            Get
                Return _Updpgm
            End Get
            Set(ByVal value As String)
                _Updpgm = value
            End Set
        End Property

        ''' <summary>集合図面－図面番号</summary>
        ''' <value>集合図面－図面番号</value>
        ''' <returns>集合図面－図面番号</returns>
        Public Property Gzzcp() As String
            Get
                Return _Gzzcp
            End Get
            Set(ByVal value As String)
                _Gzzcp = value
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