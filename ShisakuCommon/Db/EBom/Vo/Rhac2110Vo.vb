Namespace Db.EBom.Vo
    ''' <summary>
    ''' パーツプライス
    ''' </summary>
    ''' <remarks></remarks>
    Public Class Rhac2110Vo
        ' 部品№
        Private _BuhinNo As String
        ' 改訂№
        Private _KaiteiNo As String
        ' 発注区分
        Private _HaccyuKbn As String
        ' 単価契約区分
        Private _TankaKeiyakuKbn As String
        ' 通貨区分
        Private _TsukaKbn As String
        ' 単価状態区分
        Private _TankaJyotaiKbn As String
        ' 契約単価合計
        Private _KeiyakuTankaKei As Decimal
        ' 有償支給部品費
        Private _YuusyouSikyuBuhinhi As Decimal
        ' 契約単価日付
        Private _KeiyakuTankaDate As Integer

        ' 採用年月日
        Private _SaiyoDate As Integer
        ' 廃止年月日
        Private _HaisiDate As Integer


        ' 作成ユーザーID
        Private _createdUserId As String
        ' 作成年月日
        Private _createdDate As String
        ' 作成時分秒
        Private _createdTime As String
        ' 更新ユーザーID
        Private _updatedUserId As String
        ' 更新年月日
        Private _updatedDate As String
        ' 更新時分秒
        Private _updatedTime As String

        '2014/05/08 K.Haraguchi コスト取得日追加
        Private _CostGetDate As Integer

        ''' <summary>部品№</summary>
        Public Property BuhinNo() As String
            Get
                Return _BuhinNo
            End Get
            Set(ByVal value As String)
                _BuhinNo = value
            End Set
        End Property

        ''' <summary>改訂№</summary>
        Public Property KaiteiNo() As String
            Get
                Return _KaiteiNo
            End Get
            Set(ByVal value As String)
                _KaiteiNo = value
            End Set
        End Property

        ''' <summary>発注区分</summary>
        Public Property HaccyuKbn() As String
            Get
                Return _HaccyuKbn
            End Get
            Set(ByVal value As String)
                _HaccyuKbn = value
            End Set
        End Property

        ''' <summary>単価契約区分</summary>
        Public Property TankaKeiyakuKbn() As String
            Get
                Return _TankaKeiyakuKbn
            End Get
            Set(ByVal value As String)
                _TankaKeiyakuKbn = value
            End Set
        End Property

        ''' <summary>通貨区分</summary>
        Public Property TsukaKbn() As String
            Get
                Return _TsukaKbn
            End Get
            Set(ByVal value As String)
                _TsukaKbn = value
            End Set
        End Property

        ''' <summary>単価状態区分</summary>
        Public Property TankaJyotaiKbn() As String
            Get
                Return _TankaJyotaiKbn
            End Get
            Set(ByVal value As String)
                _TankaJyotaiKbn = value
            End Set
        End Property

        ''' <summary>契約単価合計</summary>
        Public Property KeiyakuTankaKei() As Decimal
            Get
                Return _KeiyakuTankaKei
            End Get
            Set(ByVal value As Decimal)
                _KeiyakuTankaKei = value
            End Set
        End Property


        ''' <summary>有償支給部品費</summary>
        Public Property YuusyouSikyuBuhinhi() As Decimal
            Get
                Return _YuusyouSikyuBuhinhi
            End Get
            Set(ByVal value As Decimal)
                _YuusyouSikyuBuhinhi = value
            End Set
        End Property

        ''' <summary>契約単価日付</summary>
        Public Property KeiyakuTankaDate() As Integer
            Get
                Return _KeiyakuTankaDate
            End Get
            Set(ByVal value As Integer)
                _KeiyakuTankaDate = value
            End Set
        End Property

        ''' <summary>採用日</summary>
        Public Property SaiyoDate() As Integer
            Get
                Return _SaiyoDate
            End Get
            Set(ByVal value As Integer)
                _SaiyoDate = value
            End Set
        End Property

        ''' <summary>廃止日</summary>
        Public Property HaisiDate() As Integer
            Get
                Return _HaisiDate
            End Get
            Set(ByVal value As Integer)
                _HaisiDate = value
            End Set
        End Property


        ''' <summary>作成ユーザーID</summary>
        Public Property CreatedUserId() As String
            Get
                Return _createdUserId
            End Get
            Set(ByVal value As String)
                _createdUserId = value
            End Set
        End Property

        ''' <summary>作成年月日</summary>
        Public Property CreatedDate() As String
            Get
                Return _createdDate
            End Get
            Set(ByVal value As String)
                _createdDate = value
            End Set
        End Property

        ''' <summary>作成時分秒</summary>
        Public Property CreatedTime() As String
            Get
                Return _createdTime
            End Get
            Set(ByVal value As String)
                _createdTime = value
            End Set
        End Property

        ''' <summary>更新ユーザーID</summary>
        Public Property UpdatedUserId() As String
            Get
                Return _updatedUserId
            End Get
            Set(ByVal value As String)
                _updatedUserId = value
            End Set
        End Property

        ''' <summary>更新年月日</summary>
        Public Property UpdatedDate() As String
            Get
                Return _updatedDate
            End Get
            Set(ByVal value As String)
                _updatedDate = value
            End Set
        End Property

        ''' <summary>更新時分秒</summary>
        Public Property UpdatedTime() As String
            Get
                Return _updatedTime
            End Get
            Set(ByVal value As String)
                _updatedTime = value
            End Set
        End Property

        ''' <summary>コスト取得日</summary>
        Public Property CostGetDate() As Integer
            Get
                Return _CostGetDate
            End Get
            Set(ByVal value As Integer)
                _CostGetDate = value
            End Set
        End Property
    End Class
End Namespace