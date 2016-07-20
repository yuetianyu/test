Namespace Db.EBom.Vo
    ''' <summary>
    ''' 試作手配出図実績手入力情報
    ''' </summary>
    ''' <remarks></remarks>
    Public Class TShisakuTehaiShutuzuJisekiInputVo
        ''' <summary>
        ''' 試作イベントコード
        ''' </summary>
        ''' <remarks></remarks>
        Private _ShisakuEventCode As String
        Public Property ShisakuEventCode() As String
            Get
                Return _ShisakuEventCode
            End Get
            Set(ByVal value As String)
                _ShisakuEventCode = value
            End Set
        End Property

        ''' <summary>
        ''' 試作リストコード
        ''' </summary>
        ''' <remarks></remarks>
        Private _ShisakuListCode As String
        Public Property ShisakuListCode() As String
            Get
                Return _ShisakuListCode
            End Get
            Set(ByVal value As String)
                _ShisakuListCode = value
            End Set
        End Property

        ''' <summary>
        ''' 試作リストコード改訂№
        ''' </summary>
        ''' <remarks></remarks>
        Private _ShisakuListCodeKaiteiNo As String
        Public Property ShisakuListCodeKaiteiNo() As String
            Get
                Return _ShisakuListCodeKaiteiNo
            End Get
            Set(ByVal value As String)
                _ShisakuListCodeKaiteiNo = value
            End Set
        End Property

        ''' <summary>
        ''' 試作ブロック№
        ''' </summary>
        ''' <remarks></remarks>
        Private _ShisakuBlockNo As String
        Public Property ShisakuBlockNo() As String
            Get
                Return _ShisakuBlockNo
            End Get
            Set(ByVal value As String)
                _ShisakuBlockNo = value
            End Set
        End Property

        ''' <summary>
        ''' 部品番号
        ''' </summary>
        ''' <remarks></remarks>
        Private _BuhinNo As String
        Public Property BuhinNo() As String
            Get
                Return _BuhinNo
            End Get
            Set(ByVal value As String)
                _BuhinNo = value
            End Set
        End Property

        ''' <summary>
        ''' 出図実績_受領日
        ''' </summary>
        ''' <remarks></remarks>
        Private _ShutuzuJisekiJyuryoDate As Integer
        Public Property ShutuzuJisekiJyuryoDate() As Integer
            Get
                Return _ShutuzuJisekiJyuryoDate
            End Get
            Set(ByVal value As Integer)
                _ShutuzuJisekiJyuryoDate = value
            End Set
        End Property

        ''' <summary>
        ''' 出図実績_改訂№
        ''' </summary>
        ''' <remarks></remarks>
        Private _ShutuzuJisekiKaiteiNo As String
        Public Property ShutuzuJisekiKaiteiNo() As String
            Get
                Return _ShutuzuJisekiKaiteiNo
            End Get
            Set(ByVal value As String)
                _ShutuzuJisekiKaiteiNo = value
            End Set
        End Property

        ''' <summary>
        ''' 出図実績_設通№
        ''' </summary>
        ''' <remarks></remarks>
        Private _ShutuzuJisekiStsrDhstba As String
        Public Property ShutuzuJisekiStsrDhstba() As String
            Get
                Return _ShutuzuJisekiStsrDhstba
            End Get
            Set(ByVal value As String)
                _ShutuzuJisekiStsrDhstba = value
            End Set
        End Property

        ''' <summary>
        ''' 件名
        ''' </summary>
        ''' <remarks></remarks>
        Private _ShutuzuKenmei As String
        Public Property ShutuzuKenmei() As String
            Get
                Return _ShutuzuKenmei
            End Get
            Set(ByVal value As String)
                _ShutuzuKenmei = value
            End Set
        End Property

        ''' <summary>
        ''' コメント
        ''' </summary>
        ''' <remarks></remarks>
        Private _Comment As String
        Public Property Comment() As String
            Get
                Return _Comment
            End Get
            Set(ByVal value As String)
                _Comment = value
            End Set
        End Property

        ''' <summary>
        ''' 作成ユーザーID
        ''' </summary>
        ''' <remarks></remarks>
        Private _CreatedUserId As String
        Public Property CreatedUserId() As String
            Get
                Return _CreatedUserId
            End Get
            Set(ByVal value As String)
                _CreatedUserId = value
            End Set
        End Property

        ''' <summary>
        ''' 作成年月日
        ''' </summary>
        ''' <remarks></remarks>
        Private _CreatedDate As String
        Public Property CreatedDate() As String
            Get
                Return _CreatedDate
            End Get
            Set(ByVal value As String)
                _CreatedDate = value
            End Set
        End Property

        ''' <summary>
        ''' 作成時分秒
        ''' </summary>
        ''' <remarks></remarks>
        Private _CreatedTime As String
        Public Property CreatedTime() As String
            Get
                Return _CreatedTime
            End Get
            Set(ByVal value As String)
                _CreatedTime = value
            End Set
        End Property

        ''' <summary>
        ''' 更新ユーザーID
        ''' </summary>
        ''' <remarks></remarks>
        Private _UpdatedUserId As String
        Public Property UpdatedUserId() As String
            Get
                Return _UpdatedUserId
            End Get
            Set(ByVal value As String)
                _UpdatedUserId = value
            End Set
        End Property

        ''' <summary>
        ''' 更新年月日
        ''' </summary>
        ''' <remarks></remarks>
        Private _UpdatedDate As String
        Public Property UpdatedDate() As String
            Get
                Return _UpdatedDate
            End Get
            Set(ByVal value As String)
                _UpdatedDate = value
            End Set
        End Property

        ''' <summary>
        ''' 更新時分秒
        ''' </summary>
        ''' <remarks></remarks>
        Private _UpdatedTime As String
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