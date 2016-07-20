Namespace Db.EBom.Vo
    ''' <summary>
    ''' 予算設定部品表情報を保持する。
    ''' </summary>
    ''' <remarks></remarks>
    Public Class TYosanSetteiListcodeVo
        ''' <summary>試作イベントコード</summary>
        Private _ShisakuEventCode As String
        Public Property ShisakuEventCode() As String
            Get
                Return _ShisakuEventCode
            End Get
            Set(ByVal value As String)
                _ShisakuEventCode = value
            End Set
        End Property

        ''' <summary>予算リスト表示順</summary>
        Private _YosanListHyojijunNo As Nullable(Of Int32)
        Public Property YosanListHyojijunNo() As Nullable(Of Int32)
            Get
                Return _YosanListHyojijunNo
            End Get
            Set(ByVal value As Nullable(Of Int32))
                _YosanListHyojijunNo = value
            End Set
        End Property

        ''' <summary>予算リストコード</summary>
        Private _YosanListCode As String
        Public Property YosanListCode() As String
            Get
                Return _YosanListCode
            End Get
            Set(ByVal value As String)
                _YosanListCode = value
            End Set
        End Property

        ''' <summary>予算グループ№</summary>
        Private _YosanGroupNo As String
        Public Property YosanGroupNo() As String
            Get
                Return _YosanGroupNo
            End Get
            Set(ByVal value As String)
                _YosanGroupNo = value
            End Set
        End Property

        ''' <summary>予算工事区分</summary>
        Private _YosanKoujiKbn As String
        Public Property YosanKoujiKbn() As String
            Get
                Return _YosanKoujiKbn
            End Get
            Set(ByVal value As String)
                _YosanKoujiKbn = value
            End Set
        End Property

        ''' <summary>予算製品区分</summary>
        Private _YosanSeihinKbn As String
        Public Property YosanSeihinKbn() As String
            Get
                Return _YosanSeihinKbn
            End Get
            Set(ByVal value As String)
                _YosanSeihinKbn = value
            End Set
        End Property

        ''' <summary>予算工事指令№</summary>
        Private _YosanKoujiShireiNo As String
        Public Property YosanKoujiShireiNo() As String
            Get
                Return _YosanKoujiShireiNo
            End Get
            Set(ByVal value As String)
                _YosanKoujiShireiNo = value
            End Set
        End Property

        ''' <summary>予算工事№</summary>
        Private _YosanKoujiNo As String
        Public Property YosanKoujiNo() As String
            Get
                Return _YosanKoujiNo
            End Get
            Set(ByVal value As String)
                _YosanKoujiNo = value
            End Set
        End Property

        ''' <summary>予算イベント名称</summary>
        Private _YosanEventName As String
        Public Property YosanEventName() As String
            Get
                Return _YosanEventName
            End Get
            Set(ByVal value As String)
                _YosanEventName = value
            End Set
        End Property

        ''' <summary>予算自給品の消し込み</summary>
        Private _YosanJikyuhin As String
        Public Property YosanJikyuhin() As String
            Get
                Return _YosanJikyuhin
            End Get
            Set(ByVal value As String)
                _YosanJikyuhin = value
            End Set
        End Property

        ''' <summary>予算比較結果折込済み</summary>
        Private _YosanHikakukekka As String
        Public Property YosanHikakukekka() As String
            Get
                Return _YosanHikakukekka
            End Get
            Set(ByVal value As String)
                _YosanHikakukekka = value
            End Set
        End Property

        ''' <summary>予算集計コードからの展開</summary>
        Private _YosanSyuukeiCode As String
        Public Property YosanSyuukeiCode() As String
            Get
                Return _YosanSyuukeiCode
            End Get
            Set(ByVal value As String)
                _YosanSyuukeiCode = value
            End Set
        End Property

        ''' <summary>予算台数</summary>
        Private _YosanDaisu As String
        Public Property YosanDaisu() As String
            Get
                Return _YosanDaisu
            End Get
            Set(ByVal value As String)
                _YosanDaisu = value
            End Set
        End Property

        ''' <summary>予算部品表作成日</summary>
        Private _YosanBuhinSakuseibi As Nullable(Of Int32)
        Public Property YosanBuhinSakuseibi() As Nullable(Of Int32)
            Get
                Return _YosanBuhinSakuseibi
            End Get
            Set(ByVal value As Nullable(Of Int32))
                _YosanBuhinSakuseibi = value
            End Set
        End Property

        ''' <summary>予算部品表作成時間</summary>
        Private _YosanBuhinSakuseijikan As Nullable(Of Int32)
        Public Property YosanBuhinSakuseijikan() As Nullable(Of Int32)
            Get
                Return _YosanBuhinSakuseijikan
            End Get
            Set(ByVal value As Nullable(Of Int32))
                _YosanBuhinSakuseijikan = value
            End Set
        End Property

        ''' <summary>ステータス</summary>
        Private _Status As String
        Public Property Status() As String
            Get
                Return _Status
            End Get
            Set(ByVal value As String)
                _Status = value
            End Set
        End Property

        ''' <summary>最終最新化実行日</summary>
        Private _LastSaishinbi As Nullable(Of Int32)
        Public Property LastSaishinbi() As Nullable(Of Int32)
            Get
                Return _LastSaishinbi
            End Get
            Set(ByVal value As Nullable(Of Int32))
                _LastSaishinbi = value
            End Set
        End Property

        ''' <summary>最終最新化実行時間</summary>
        Private _LastSaishinjikan As Nullable(Of Int32)
        Public Property LastSaishinjikan() As Nullable(Of Int32)
            Get
                Return _LastSaishinjikan
            End Get
            Set(ByVal value As Nullable(Of Int32))
                _LastSaishinjikan = value
            End Set
        End Property

        ''' <summary>最終割付実行日</summary>
        Private _LastWaritukebi As Nullable(Of Int32)
        Public Property LastWaritukebi() As Nullable(Of Int32)
            Get
                Return _LastWaritukebi
            End Get
            Set(ByVal value As Nullable(Of Int32))
                _LastWaritukebi = value
            End Set
        End Property

        ''' <summary>最終割付実行時間</summary>
        Private _LastWaritukejikan As Nullable(Of Int32)
        Public Property LastWaritukejikan() As Nullable(Of Int32)
            Get
                Return _LastWaritukejikan
            End Get
            Set(ByVal value As Nullable(Of Int32))
                _LastWaritukejikan = value
            End Set
        End Property

        ''' <summary>最終ファンクション単価設定日</summary>
        Private _LastFunctionTankaSetteibi As Nullable(Of Int32)
        Public Property LastFunctionTankaSetteibi() As Nullable(Of Int32)
            Get
                Return _LastFunctionTankaSetteibi
            End Get
            Set(ByVal value As Nullable(Of Int32))
                _LastFunctionTankaSetteibi = value
            End Set
        End Property

        ''' <summary>最終ファンクション単価設定時間</summary>
        Private _LastFunctionTankaSetteijikan As Nullable(Of Int32)
        Public Property LastFunctionTankaSetteijikan() As Nullable(Of Int32)
            Get
                Return _LastFunctionTankaSetteijikan
            End Get
            Set(ByVal value As Nullable(Of Int32))
                _LastFunctionTankaSetteijikan = value
            End Set
        End Property

        ''' <summary>最終単価自動設定日</summary>
        Private _LastTankaAutoSetteibi As Nullable(Of Int32)
        Public Property LastTankaAutoSetteibi() As Nullable(Of Int32)
            Get
                Return _LastTankaAutoSetteibi
            End Get
            Set(ByVal value As Nullable(Of Int32))
                _LastTankaAutoSetteibi = value
            End Set
        End Property

        ''' <summary>最終単価自動設定時間</summary>
        Private _LastTankaAutoSetteijikan As Nullable(Of Int32)
        Public Property LastTankaAutoSetteijikan() As Nullable(Of Int32)
            Get
                Return _LastTankaAutoSetteijikan
            End Get
            Set(ByVal value As Nullable(Of Int32))
                _LastTankaAutoSetteijikan = value
            End Set
        End Property

        ''' <summary>最終EXCEL取込日</summary>
        Private _LastExcelImportbi As Nullable(Of Int32)
        Public Property LastExcelImportbi() As Nullable(Of Int32)
            Get
                Return _LastExcelImportbi
            End Get
            Set(ByVal value As Nullable(Of Int32))
                _LastExcelImportbi = value
            End Set
        End Property

        ''' <summary>最終EXCEL取込時間</summary>
        Private _LastExcelImportjikan As Nullable(Of Int32)
        Public Property LastExcelImportjikan() As Nullable(Of Int32)
            Get
                Return _LastExcelImportjikan
            End Get
            Set(ByVal value As Nullable(Of Int32))
                _LastExcelImportjikan = value
            End Set
        End Property

        ''' <summary>最終部品費端数処理日</summary>
        Private _LastBuhinHiHasubi As Nullable(Of Int32)
        Public Property LastBuhinHiHasubi() As Nullable(Of Int32)
            Get
                Return _LastBuhinHiHasubi
            End Get
            Set(ByVal value As Nullable(Of Int32))
                _LastBuhinHiHasubi = value
            End Set
        End Property

        ''' <summary>最終部品費端数処理時間</summary>
        Private _LastBuhinHiHasujikan As Nullable(Of Int32)
        Public Property LastBuhinHiHasujikan() As Nullable(Of Int32)
            Get
                Return _LastBuhinHiHasujikan
            End Get
            Set(ByVal value As Nullable(Of Int32))
                _LastBuhinHiHasujikan = value
            End Set
        End Property

        ''' <summary>メモ欄</summary>
        Private _YosanMemo As String
        Public Property YosanMemo() As String
            Get
                Return _YosanMemo
            End Get
            Set(ByVal value As String)
                _YosanMemo = value
            End Set
        End Property

        ''' <summary>作成ユーザーID</summary>
        Private _CreatedUserId As String
        Public Property CreatedUserId() As String
            Get
                Return _CreatedUserId
            End Get
            Set(ByVal value As String)
                _CreatedUserId = value
            End Set
        End Property

        ''' <summary>作成年月日</summary>
        Private _CreatedDate As String
        Public Property CreatedDate() As String
            Get
                Return _CreatedDate
            End Get
            Set(ByVal value As String)
                _CreatedDate = value
            End Set
        End Property

        ''' <summary>作成時分秒</summary>
        Private _CreatedTime As String
        Public Property CreatedTime() As String
            Get
                Return _CreatedTime
            End Get
            Set(ByVal value As String)
                _CreatedTime = value
            End Set
        End Property

        ''' <summary>更新ユーザーID</summary>
        Private _UpdatedUserId As String
        Public Property UpdatedUserId() As String
            Get
                Return _UpdatedUserId
            End Get
            Set(ByVal value As String)
                _UpdatedUserId = value
            End Set
        End Property

        ''' <summary>更新年月日</summary>
        Private _UpdatedDate As String
        Public Property UpdatedDate() As String
            Get
                Return _UpdatedDate
            End Get
            Set(ByVal value As String)
                _UpdatedDate = value
            End Set
        End Property

        ''' <summary>更新時分秒</summary>
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


