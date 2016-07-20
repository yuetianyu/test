Namespace Db.EBom.Vo
    ''' <summary>
    ''' Rhac0553と紐付く属性
    ''' </summary>
    ''' <remarks></remarks>
    Public Class Rhac1920Vo
        '' 開発符号
        Private _KaihatsuFugo As String
        '' ブロックNo
        Private _BlockNo As String
        '' 部品番号
        Private _BuhinNo As String
        '' 改訂No
        Private _KaiteiNo As String
        '' BLK部品改訂No
        Private _BlkBuhinKaiteiNo As String
        '' 承認年月日
        Private _ShoninDate As Integer
        '' 採用年月日
        Private _SaiyoDate As Integer
        '' 廃止年月日
        Private _HaisiDate As Integer
        '' 作成AppNo
        Private _CreatedAppNo As String
        '' 更新AppNo
        Private _UpdatedAppNo As String
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

        ''' <summary>ブロックNo</summary>
        ''' <value>ブロックNo</value>
        ''' <returns>ブロックNo</returns>
        Public Property BlockNo() As String
            Get
                Return _BlockNo
            End Get
            Set(ByVal value As String)
                _BlockNo = value
            End Set
        End Property

        ''' <summary>部品番号</summary>
        ''' <value>部品番号</value>
        ''' <returns>部品番号</returns>
        Public Property BuhinNo() As String
            Get
                Return _BuhinNo
            End Get
            Set(ByVal value As String)
                _BuhinNo = value
            End Set
        End Property

        ''' <summary>改訂No</summary>
        ''' <value>改訂No</value>
        ''' <returns>改訂No</returns>
        Public Property KaiteiNo() As String
            Get
                Return _KaiteiNo
            End Get
            Set(ByVal value As String)
                _KaiteiNo = value
            End Set
        End Property

        ''' <summary>BLK部品改訂No</summary>
        ''' <value>BLK部品改訂No</value>
        ''' <returns>BLK部品改訂No</returns>
        Public Property BlkBuhinKaiteiNo() As String
            Get
                Return _BlkBuhinKaiteiNo
            End Get
            Set(ByVal value As String)
                _BlkBuhinKaiteiNo = value
            End Set
        End Property

        ''' <summary>承認日付</summary>
        ''' <value>承認日付</value>
        ''' <returns>承認日付</returns>
        Public Property ShoninDate() As Integer
            Get
                Return _ShoninDate
            End Get
            Set(ByVal value As Integer)
                _ShoninDate = value
            End Set
        End Property

        ''' <summary>採用年月日</summary>
        ''' <value>採用年月日</value>
        ''' <returns>採用年月日</returns>
        Public Property SaiyoDate() As Integer
            Get
                Return _SaiyoDate
            End Get
            Set(ByVal value As Integer)
                _SaiyoDate = value
            End Set
        End Property

        ''' <summary>廃止年月日</summary>
        ''' <value>廃止年月日</value>
        ''' <returns>廃止年月日</returns>
        Public Property HaisiDate() As Integer
            Get
                Return _HaisiDate
            End Get
            Set(ByVal value As Integer)
                _HaisiDate = value
            End Set
        End Property

        ''' <summary>作成AppNo</summary>
        ''' <value>作成AppNo</value>
        ''' <returns>作成AppNo</returns>
        Public Property CreatedAppNo() As String
            Get
                Return _CreatedAppNo
            End Get
            Set(ByVal value As String)
                _CreatedAppNo = value
            End Set
        End Property

        ''' <summary>更新AppNo</summary>
        ''' <value>更新AppNo</value>
        ''' <returns>更新AppNo</returns>
        Public Property UpdatedAppNo() As String
            Get
                Return _UpdatedAppNo
            End Get
            Set(ByVal value As String)
                _UpdatedAppNo = value
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