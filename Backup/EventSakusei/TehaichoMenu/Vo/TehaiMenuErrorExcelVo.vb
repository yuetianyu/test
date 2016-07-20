
Namespace TehaichoMenu.Vo
    '手配帳エラーチェック用のVOクラス'
    Public Class TehaiMenuErrorExcelVo


        '' エラー判定 
        Private _ErrorKbn As String

        '' ブロックNoEC 
        Private _EcShisakuBlockNo As String

        '' ブロックNo 
        Private _ShisakuBlockNo As String

        '' 工事NoEC 
        'Private _KoujiNoEC As String '今回は使用しない 

        '' 工事No 
        Private _ShisakuKoujiNo As String

        '' 行ID 
        Private _GyouId As String

        '' 専用マーク 
        Private _SenyouMark As String

        '' 手配記号 
        Private _TehaiKigou As String

        '' 部品番号EC 
        Private _EcBuhinNo As String

        '' 部品番号 
        Private _BuhinNo As String

        '' 試作区分 
        Private _BuhinNoKbn As String

        '' 部品名称EC 
        Private _EcBuhinName As String

        '' 部品名称 
        Private _BuhinName As String

        '' 納入指示数EC 
        Private _EcTotalInsuSuryo As String

        '' 納入指示数 
        Private _TotalInsuSuryo As Integer

        '' 納入指示日EC 
        Private _EcNounyuShijibi As String

        '' 納入指示日 
        Private _NounyuShijibi As Integer

        '' 納入場所EC 
        Private _EcNouba As String

        '' 納入場所 
        Private _Nouba As String

        '' 供給セクションEC 
        Private _EcKyoukuSection As String

        '' 供給セクション 
        Private _KyoukuSection As String

        '' 購入希望単価EC 
        'Private _EcKounyuKiboTanka As String '今回は使用しない

        '' 購入希望単価 
        'Private _KounyuKiboTanka As String '今回は使用しない

        '' 購担EC 
        Private _EcKouTanSection As String

        '' 購担 
        Private _KouTan As String

        '' 取引先EC 
        Private _EcTorihikisaki As String

        '' 取引先 
        Private _Torihikisaki As String

        '' マスタ参照・購担 
        Private _MasterKoutan As String

        '' マスタ参照・取引先 
        Private _MasterTorihikisaki As String


        ''' <summary>エラー判定</summary>
        ''' <value>エラー判定</value>
        ''' <returns>エラー判定</returns>
        Public Property ErrorKbn() As String
            Get
                Return _ErrorKbn
            End Get
            Set(ByVal value As String)
                _ErrorKbn = value
            End Set
        End Property


        ''' <summary>ブロックNoEC</summary>
        ''' <value>ブロックNoEC</value>
        ''' <returns>ブロックNoEC</returns>
        Public Property EcShisakuBlockNo() As String
            Get
                Return _EcShisakuBlockNo
            End Get
            Set(ByVal value As String)
                _EcShisakuBlockNo = value
            End Set
        End Property

        ''' <summary>ブロックNo</summary>
        ''' <value>ブロックNo</value>
        ''' <returns>ブロックNo</returns>
        Public Property ShisakuBlockNo() As String
            Get
                Return _ShisakuBlockNo
            End Get
            Set(ByVal value As String)
                _ShisakuBlockNo = value
            End Set
        End Property

        ''' <summary>工事No</summary>
        ''' <value>工事No</value>
        ''' <returns>工事No</returns>
        Public Property ShisakuKoujiNo() As String
            Get
                Return _ShisakuKoujiNo
            End Get
            Set(ByVal value As String)
                _ShisakuKoujiNo = value
            End Set
        End Property

        ''' <summary>行ID</summary>
        ''' <value>行ID</value>
        ''' <returns>行ID</returns>
        Public Property GyouId() As String
            Get
                Return _GyouId
            End Get
            Set(ByVal value As String)
                _GyouId = value
            End Set
        End Property

        ''' <summary>専用マーク</summary>
        ''' <value>専用マーク</value>
        ''' <returns>専用マーク</returns>
        Public Property SenyouMark() As String
            Get
                Return _SenyouMark
            End Get
            Set(ByVal value As String)
                _SenyouMark = value
            End Set
        End Property

        ''' <summary>手配記号</summary>
        ''' <value>手配記号</value>
        ''' <returns>手配記号</returns>
        Public Property TehaiKigou() As String
            Get
                Return _TehaiKigou
            End Get
            Set(ByVal value As String)
                _TehaiKigou = value
            End Set
        End Property

        ''' <summary>部品番号EC</summary>
        ''' <value>部品番号EC</value>
        ''' <returns>部品番号EC</returns>
        Public Property EcBuhinNo() As String
            Get
                Return _EcBuhinNo
            End Get
            Set(ByVal value As String)
                _EcBuhinNo = value
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

        ''' <summary>試作区分</summary>
        ''' <value>試作区分</value>
        ''' <returns>試作区分</returns>
        Public Property BuhinNoKbn() As String
            Get
                Return _BuhinNoKbn
            End Get
            Set(ByVal value As String)
                _BuhinNoKbn = value
            End Set
        End Property

        ''' <summary>部品名称EC</summary>
        ''' <value>部品名称EC</value>
        ''' <returns>部品名称EC</returns>
        Public Property EcBuhinName() As String
            Get
                Return _EcBuhinName
            End Get
            Set(ByVal value As String)
                _EcBuhinName = value
            End Set
        End Property

        ''' <summary>部品名称</summary>
        ''' <value>部品名称</value>
        ''' <returns>部品名称</returns>
        Public Property BuhinName() As String
            Get
                Return _BuhinName
            End Get
            Set(ByVal value As String)
                _BuhinName = value
            End Set
        End Property

        ''' <summary>納入指示数EC</summary>
        ''' <value>納入指示数EC</value>
        ''' <returns>納入指示数EC</returns>
        Public Property EcTotalInsuSuryo() As String
            Get
                Return _EcTotalInsuSuryo
            End Get
            Set(ByVal value As String)
                _EcTotalInsuSuryo = value
            End Set
        End Property

        ''' <summary>納入指示数</summary>
        ''' <value>納入指示数</value>
        ''' <returns>納入指示数</returns>
        Public Property TotalInsuSuryo() As String
            Get
                Return _TotalInsuSuryo
            End Get
            Set(ByVal value As String)
                _TotalInsuSuryo = value
            End Set
        End Property

        ''' <summary>納入指示日EC</summary>
        ''' <value>納入指示日EC</value>
        ''' <returns>納入指示日EC</returns>
        Public Property EcNounyuShijibi() As String
            Get
                Return _EcNounyuShijibi
            End Get
            Set(ByVal value As String)
                _EcNounyuShijibi = value
            End Set
        End Property

        ''' <summary>納入指示日</summary>
        ''' <value>納入指示日</value>
        ''' <returns>納入指示日</returns>
        Public Property NounyuShijibi() As String
            Get
                Return _NounyuShijibi
            End Get
            Set(ByVal value As String)
                _NounyuShijibi = value
            End Set
        End Property

        ''' <summary>納入場所EC</summary>
        ''' <value>納入場所EC</value>
        ''' <returns>納入場所EC</returns>
        Public Property EcNouba() As String
            Get
                Return _EcNouba
            End Get
            Set(ByVal value As String)
                _EcNouba = value
            End Set
        End Property

        ''' <summary>納入場所</summary>
        ''' <value>納入場所</value>
        ''' <returns>納入場所</returns>
        Public Property Nouba() As String
            Get
                Return _Nouba
            End Get
            Set(ByVal value As String)
                _Nouba = value
            End Set
        End Property

        ''' <summary>供給セクションEC</summary>
        ''' <value>供給セクションEC</value>
        ''' <returns>供給セクションEC</returns>
        Public Property EcKyoukuSection() As String
            Get
                Return _EcKyoukuSection
            End Get
            Set(ByVal value As String)
                _EcKyoukuSection = value
            End Set
        End Property

        ''' <summary>供給セクション</summary>
        ''' <value>供給セクション</value>
        ''' <returns>供給セクション</returns>
        Public Property KyoukuSection() As String
            Get
                Return _KyoukuSection
            End Get
            Set(ByVal value As String)
                _KyoukuSection = value
            End Set
        End Property

        ''' <summary>購担EC</summary>
        ''' <value>購担EC</value>
        ''' <returns>購担EC</returns>
        Public Property EcKoutanSection() As String
            Get
                Return _EcKouTanSection
            End Get
            Set(ByVal value As String)
                _EcKouTanSection = value
            End Set
        End Property

        ''' <summary>購担</summary>
        ''' <value>購担</value>
        ''' <returns>購担</returns>
        Public Property Koutan() As String
            Get
                Return _KouTan
            End Get
            Set(ByVal value As String)
                _KouTan = value
            End Set
        End Property

        ''' <summary>取引先EC</summary>
        ''' <value>取引先EC</value>
        ''' <returns>取引先EC</returns>
        Public Property EcTorihikisaki() As String
            Get
                Return _EcTorihikisaki
            End Get
            Set(ByVal value As String)
                _EcTorihikisaki = value
            End Set
        End Property

        ''' <summary>取引先</summary>
        ''' <value>取引先</value>
        ''' <returns>取引先</returns>
        Public Property Torihikisaki() As String
            Get
                Return _Torihikisaki
            End Get
            Set(ByVal value As String)
                _Torihikisaki = value
            End Set
        End Property

        ''' <summary>マスタ購担</summary>
        ''' <value>マスタ購担</value>
        ''' <returns>マスタ購担</returns>
        Public Property MasterKoutan() As String
            Get
                Return _MasterKoutan
            End Get
            Set(ByVal value As String)
                _MasterKoutan = value
            End Set
        End Property

        ''' <summary>マスタ取引先</summary>
        ''' <value>マスタ取引先</value>
        ''' <returns>マスタ取引先</returns>
        Public Property MasterTorihikisaki() As String
            Get
                Return _MasterTorihikisaki
            End Get
            Set(ByVal value As String)
                _MasterTorihikisaki = value
            End Set
        End Property

    End Class
End Namespace