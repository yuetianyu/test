Namespace Db.EBom.Vo
    ''' <summary>試作手配エラー情報</summary>
    Public Class TShisakuTehaiErrorVo
        ''' <summary>試作イベントコード</summary>
        Private _ShisakuEventCode As String
        ''' <summary>試作リストコード</summary>
        Private _ShisakuListCode As String
        ''' <summary>試作リストコード改訂№</summary>
        Private _ShisakuListCodeKaiteiNo As String
        ''' <summary>試作部課コード</summary>
        Private _ShisakuBukaCode As String
        ''' <summary>試作部品表示順</summary>
        Private _ShisakuBuhinHyoujiJun As Nullable(Of Int32)
        ''' <summary>国内現調品フラグ</summary>
        Private _KokunaiGenchoFlg As String
        ''' <summary>エラー判定</summary>
        Private _ErrorKbn As String
        ''' <summary>エラーコード_試作ブロック№</summary>
        Private _EcShisakuBlockNo As String
        ''' <summary>試作ブロック№</summary>
        Private _ShisakuBlockNo As String
        ''' <summary>エラーコード_部品番号</summary>
        Private _EcBuhinNo As String
        ''' <summary>部品番号</summary>
        Private _BuhinNo As String
        ''' <summary>部品番号試作区分</summary>
        Private _BuhinNoKbn As String
        ''' <summary>エラーコード_部品名称</summary>
        Private _EcBuhinName As String
        ''' <summary>部品名称</summary>
        Private _BuhinName As String
        ''' <summary>エラーコード_合計員数</summary>
        Private _EcTotalInsuSuryo As String
        ''' <summary>合計員数</summary>
        Private _TotalInsuSuryo As Nullable(Of Int32)
        ''' <summary>エラーコード_納入指示日</summary>
        Private _EcNounyuShijibi As String
        ''' <summary>納入指示日</summary>
        Private _NounyuShijibi As Nullable(Of Int32)
        ''' <summary>エラーコード_納場</summary>
        Private _EcNouba As String
        ''' <summary>納場</summary>
        Private _Nouba As String
        ''' <summary>エラーコード_供給セクション</summary>
        Private _EcKyoukuSection As String
        ''' <summary>供給セクション</summary>
        Private _KyoukuSection As String
        ''' <summary>エラーコード_購坦</summary>
        Private _EcKoutanSection As String
        ''' <summary>購担</summary>
        Private _Koutan As String
        ''' <summary>エラーコード_取引先</summary>
        Private _EcTorihikisaki As String
        ''' <summary>取引先</summary>
        Private _Torihikisaki As String
        ''' <summary>マスタ参照購担</summary>
        Private _MasterKoutan As String
        ''' <summary>マスタ参照取引先</summary>
        Private _MasterTorihikisaki As String
        ''' <summary>エラーチェック日</summary>
        Private _ErrorCheckBi As Nullable(Of Int32)
        ''' <summary>エラーチェック時間</summary>
        Private _ErrorCheckJikan As Nullable(Of Int32)
        ''' <summary>担当者</summary>
        Private _UserId As String
        ''' <summary>作成ユーザーID</summary>
        Private _CreatedUserId As String
        ''' <summary>作成日</summary>
        Private _CreatedDate As String
        ''' <summary>作成時</summary>
        Private _CreatedTime As String
        ''' <summary>更新ユーザーID</summary>
        Private _UpdatedUserId As String
        ''' <summary>更新日</summary>
        Private _UpdatedDate As String
        ''' <summary>更新時間</summary>
        Private _UpdatedTime As String

        ''' <summary>試作イベントコード</summary>
        ''' <value>試作イベントコード</value>
        ''' <returns>試作イベントコード</returns>
        Public Property ShisakuEventCode() As String
            Get
                Return _ShisakuEventCode
            End Get
            Set(ByVal value As String)
                _ShisakuEventCode = value
            End Set
        End Property
        ''' <summary>試作リストコード</summary>
        ''' <value>試作リストコード</value>
        ''' <returns>試作リストコード</returns>
        Public Property ShisakuListCode() As String
            Get
                Return _ShisakuListCode
            End Get
            Set(ByVal value As String)
                _ShisakuListCode = value
            End Set
        End Property
        ''' <summary>試作リストコード改訂№</summary>
        ''' <value>試作リストコード改訂№</value>
        ''' <returns>試作リストコード改訂№</returns>
        Public Property ShisakuListCodeKaiteiNo() As String
            Get
                Return _ShisakuListCodeKaiteiNo
            End Get
            Set(ByVal value As String)
                _ShisakuListCodeKaiteiNo = value
            End Set
        End Property
        ''' <summary>試作部課コード</summary>
        ''' <value>試作部課コード</value>
        ''' <returns>試作部課コード</returns>
        Public Property ShisakuBukaCode() As String
            Get
                Return _ShisakuBukaCode
            End Get
            Set(ByVal value As String)
                _ShisakuBukaCode = value
            End Set
        End Property
        ''' <summary>試作部品表示順</summary>
        ''' <value>試作部品表示順</value>
        ''' <returns>試作部品表示順</returns>
        Public Property ShisakuBuhinHyoujiJun() As Nullable(Of Int32)
            Get
                Return _ShisakuBuhinHyoujiJun
            End Get
            Set(ByVal value As Nullable(Of Int32))
                _ShisakuBuhinHyoujiJun = value
            End Set
        End Property
        ''' <summary>国内現調品フラグ</summary>
        ''' <value>国内現調品フラグ</value>
        ''' <returns>国内現調品フラグ</returns>
        Public Property KokunaiGenchoFlg() As String
            Get
                Return _KokunaiGenchoFlg
            End Get
            Set(ByVal value As String)
                _KokunaiGenchoFlg = value
            End Set
        End Property
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
        ''' <summary>エラーコード_試作ブロック№</summary>
        ''' <value>エラーコード_試作ブロック№</value>
        ''' <returns>エラーコード_試作ブロック№</returns>
        Public Property EcShisakuBlockNo() As String
            Get
                Return _EcShisakuBlockNo
            End Get
            Set(ByVal value As String)
                _EcShisakuBlockNo = value
            End Set
        End Property
        ''' <summary>試作ブロック№</summary>
        ''' <value>試作ブロック№</value>
        ''' <returns>試作ブロック№</returns>
        Public Property ShisakuBlockNo() As String
            Get
                Return _ShisakuBlockNo
            End Get
            Set(ByVal value As String)
                _ShisakuBlockNo = value
            End Set
        End Property
        ''' <summary>エラーコード_部品番号</summary>
        ''' <value>エラーコード_部品番号</value>
        ''' <returns>エラーコード_部品番号</returns>
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
        ''' <summary>部品番号試作区分</summary>
        ''' <value>部品番号試作区分</value>
        ''' <returns>部品番号試作区分</returns>
        Public Property BuhinNoKbn() As String
            Get
                Return _BuhinNoKbn
            End Get
            Set(ByVal value As String)
                _BuhinNoKbn = value
            End Set
        End Property
        ''' <summary>エラーコード_部品名称</summary>
        ''' <value>エラーコード_部品名称</value>
        ''' <returns>エラーコード_部品名称</returns>
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
        ''' <summary>エラーコード_合計員数</summary>
        ''' <value>エラーコード_合計員数</value>
        ''' <returns>エラーコード_合計員数</returns>
        Public Property EcTotalInsuSuryo() As String
            Get
                Return _EcTotalInsuSuryo
            End Get
            Set(ByVal value As String)
                _EcTotalInsuSuryo = value
            End Set
        End Property
        ''' <summary>合計員数</summary>
        ''' <value>合計員数</value>
        ''' <returns>合計員数</returns>
        Public Property TotalInsuSuryo() As Nullable(Of Int32)
            Get
                Return _TotalInsuSuryo
            End Get
            Set(ByVal value As Nullable(Of Int32))
                _TotalInsuSuryo = value
            End Set
        End Property
        ''' <summary>エラーコード_納入指示日</summary>
        ''' <value>エラーコード_納入指示日</value>
        ''' <returns>エラーコード_納入指示日</returns>
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
        Public Property NounyuShijibi() As Nullable(Of Int32)
            Get
                Return _NounyuShijibi
            End Get
            Set(ByVal value As Nullable(Of Int32))
                _NounyuShijibi = value
            End Set
        End Property
        ''' <summary>エラーコード_納場</summary>
        ''' <value>エラーコード_納場</value>
        ''' <returns>エラーコード_納場</returns>
        Public Property EcNouba() As String
            Get
                Return _EcNouba
            End Get
            Set(ByVal value As String)
                _EcNouba = value
            End Set
        End Property
        ''' <summary>納場</summary>
        ''' <value>納場</value>
        ''' <returns>納場</returns>
        Public Property Nouba() As String
            Get
                Return _Nouba
            End Get
            Set(ByVal value As String)
                _Nouba = value
            End Set
        End Property
        ''' <summary>エラーコード_供給セクション</summary>
        ''' <value>エラーコード_供給セクション</value>
        ''' <returns>エラーコード_供給セクション</returns>
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
        ''' <summary>エラーコード_購坦</summary>
        ''' <value>エラーコード_購坦</value>
        ''' <returns>エラーコード_購坦</returns>
        Public Property EcKoutanSection() As String
            Get
                Return _EcKoutanSection
            End Get
            Set(ByVal value As String)
                _EcKoutanSection = value
            End Set
        End Property
        ''' <summary>購担</summary>
        ''' <value>購担</value>
        ''' <returns>購担</returns>
        Public Property Koutan() As String
            Get
                Return _Koutan
            End Get
            Set(ByVal value As String)
                _Koutan = value
            End Set
        End Property
        ''' <summary>エラーコード_取引先</summary>
        ''' <value>エラーコード_取引先</value>
        ''' <returns>エラーコード_取引先</returns>
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
        ''' <summary>マスタ参照購担</summary>
        ''' <value>マスタ参照購担</value>
        ''' <returns>マスタ参照購担</returns>
        Public Property MasterKoutan() As String
            Get
                Return _MasterKoutan
            End Get
            Set(ByVal value As String)
                _MasterKoutan = value
            End Set
        End Property
        ''' <summary>マスタ参照取引先</summary>
        ''' <value>マスタ参照取引先</value>
        ''' <returns>マスタ参照取引先</returns>
        Public Property MasterTorihikisaki() As String
            Get
                Return _MasterTorihikisaki
            End Get
            Set(ByVal value As String)
                _MasterTorihikisaki = value
            End Set
        End Property
        ''' <summary>エラーチェック日</summary>
        ''' <value>エラーチェック日</value>
        ''' <returns>エラーチェック日</returns>
        Public Property ErrorCheckBi() As Nullable(Of Int32)
            Get
                Return _ErrorCheckBi
            End Get
            Set(ByVal value As Nullable(Of Int32))
                _ErrorCheckBi = value
            End Set
        End Property
        ''' <summary>エラーチェック時間</summary>
        ''' <value>エラーチェック時間</value>
        ''' <returns>エラーチェック時間</returns>
        Public Property ErrorCheckJikan() As Nullable(Of Int32)
            Get
                Return _ErrorCheckJikan
            End Get
            Set(ByVal value As Nullable(Of Int32))
                _ErrorCheckJikan = value
            End Set
        End Property
        ''' <summary>担当者</summary>
        ''' <value>担当者</value>
        ''' <returns>担当者</returns>
        Public Property UserId() As String
            Get
                Return _UserId
            End Get
            Set(ByVal value As String)
                _UserId = value
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