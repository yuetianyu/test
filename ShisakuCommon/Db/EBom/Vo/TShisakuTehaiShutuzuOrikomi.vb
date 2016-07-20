Namespace Db.EBom.Vo
    ''' <summary>
    ''' 試作手配出図織込情報
    ''' </summary>
    ''' <remarks></remarks>
    Public Class TShisakuTehaiShutuzuOrikomiVo
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

        ''' <summary>行ID</summary>
        Private _GyouId As String
        Public Property GyouId() As String
            Get
                Return _GyouId
            End Get
            Set(ByVal value As String)
                _GyouId = value
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
        ''' 代表品番
        ''' </summary>
        ''' <remarks></remarks>
        Private _KataDaihyouBuhinNo As String
        Public Property KataDaihyouBuhinNo() As String
            Get
                Return _KataDaihyouBuhinNo
            End Get
            Set(ByVal value As String)
                _KataDaihyouBuhinNo = value
            End Set
        End Property

        ''' <summary>
        ''' 確定
        ''' </summary>
        ''' <remarks></remarks>
        Private _Kakutei As String
        Public Property Kakutei() As String
            Get
                Return _Kakutei
            End Get
            Set(ByVal value As String)
                _Kakutei = value
            End Set
        End Property

        ''' <summary>
        ''' 最新出図_受領日
        ''' </summary>
        ''' <remarks></remarks>
        Private _NewShutuzuJisekiJyuryoDate As Integer
        Public Property NewShutuzuJisekiJyuryoDate() As Integer
            Get
                Return _NewShutuzuJisekiJyuryoDate
            End Get
            Set(ByVal value As Integer)
                _NewShutuzuJisekiJyuryoDate = value
            End Set
        End Property

        ''' <summary>
        ''' 最新出図_改訂№
        ''' </summary>
        ''' <remarks></remarks>
        Private _NewShutuzuJisekiKaiteiNo As String
        Public Property NewShutuzuJisekiKaiteiNo() As String
            Get
                Return _NewShutuzuJisekiKaiteiNo
            End Get
            Set(ByVal value As String)
                _NewShutuzuJisekiKaiteiNo = value
            End Set
        End Property

        ''' <summary>
        ''' 最新出図_設通№
        ''' </summary>
        ''' <remarks></remarks>
        Private _NewShutuzuJisekiStsrDhstba As String
        Public Property NewShutuzuJisekiStsrDhstba() As String
            Get
                Return _NewShutuzuJisekiStsrDhstba
            End Get
            Set(ByVal value As String)
                _NewShutuzuJisekiStsrDhstba = value
            End Set
        End Property

        ''' <summary>
        ''' 最新出図_件名
        ''' </summary>
        ''' <remarks></remarks>
        Private _NewShutuzuKenmei As String
        Public Property NewShutuzuKenmei() As String
            Get
                Return _NewShutuzuKenmei
            End Get
            Set(ByVal value As String)
                _NewShutuzuKenmei = value
            End Set
        End Property

        ''' <summary>
        ''' 最終織込設変_受領日
        ''' </summary>
        ''' <remarks></remarks>
        Private _LastShutuzuJisekiJyuryoDate As Integer
        Public Property LastShutuzuJisekiJyuryoDate() As Integer
            Get
                Return _LastShutuzuJisekiJyuryoDate
            End Get
            Set(ByVal value As Integer)
                _LastShutuzuJisekiJyuryoDate = value
            End Set
        End Property

        ''' <summary>
        ''' 最終織込設変_改訂№
        ''' </summary>
        ''' <remarks></remarks>
        Private _LastShutuzuJisekiKaiteiNo As String
        Public Property LastShutuzuJisekiKaiteiNo() As String
            Get
                Return _LastShutuzuJisekiKaiteiNo
            End Get
            Set(ByVal value As String)
                _LastShutuzuJisekiKaiteiNo = value
            End Set
        End Property

        ''' <summary>
        ''' 最終織込設変_設通№
        ''' </summary>
        ''' <remarks></remarks>
        Private _LastShutuzuJisekiStsrDhstba As String
        Public Property LastShutuzuJisekiStsrDhstba() As String
            Get
                Return _LastShutuzuJisekiStsrDhstba
            End Get
            Set(ByVal value As String)
                _LastShutuzuJisekiStsrDhstba = value
            End Set
        End Property

        ''' <summary>
        ''' 最終織込設変_件名
        ''' </summary>
        ''' <remarks></remarks>
        Private _LastShutuzuKenmei As String
        Public Property LastShutuzuKenmei() As String
            Get
                Return _LastShutuzuKenmei
            End Get
            Set(ByVal value As String)
                _LastShutuzuKenmei = value
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