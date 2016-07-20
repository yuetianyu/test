Namespace Db.EBom.Vo
    ''' <summary>試作リストコード情報</summary>
    Public Class TShisakuListcodeVo
        ''' <summary>試作イベントコード</summary>
        Private _ShisakuEventCode As String
        ''' <summary>試作リスト表示順</summary>
        Private _ShisakuListHyojijunNo As Nullable(Of Int32)
        ''' <summary>試作リストコード</summary>
        Private _ShisakuListCode As String
        ''' <summary>試作リストコード改訂№</summary>
        Private _ShisakuListCodeKaiteiNo As String
        ''' <summary>試作グループ№</summary>
        Private _ShisakuGroupNo As String
        ''' <summary>試作工事区分</summary>
        Private _ShisakuKoujiKbn As String
        ''' <summary>試作製品区分</summary>
        Private _ShisakuSeihinKbn As String
        ''' <summary>試作工事指令№</summary>
        Private _ShisakuKoujiShireiNo As String
        ''' <summary>試作工事№</summary>
        Private _ShisakuKoujiNo As String
        ''' <summary>試作イベント名称</summary>
        Private _ShisakuEventName As String
        ''' <summary>試作自給品の消し込み</summary>
        Private _ShisakuJikyuhin As String
        ''' <summary>試作比較結果折込済み</summary>
        Private _ShisakuHikakukekka As String
        ''' <summary>試作集計コードからの展開</summary>
        Private _ShisakuSyuukeiCode As String
        ''' <summary>試作台数</summary>
        Private _ShisakuDaisu As String
        ''' <summary>試作手配帳作成日</summary>
        Private _ShisakuTehaichoSakuseibi As Nullable(Of Int32)
        ''' <summary>試作手配帳作成時間</summary>
        Private _ShisakuTehaichoSakuseijikan As Nullable(Of Int32)
        ''' <summary>旧リストコード</summary>
        Private _OldListCode As String
        ''' <summary>試作手配帳発注用データ登録日</summary>
        Private _ShisakuDataTourokubi As Nullable(Of Int32)
        ''' <summary>試作手配帳発注用データ登録時間</summary>
        Private _ShisakuDataTourokujikan As Nullable(Of Int32)
        ''' <summary>履歴</summary>
        Private _Rireki As String
        ''' <summary>試作メモ欄</summary>
        Private _ShisakuMemo As String
        ''' <summary>訂正通知データ抽出日</summary>
        Private _TeiseiChusyutubi As Nullable(Of Int32)
        ''' <summary>訂正通知データ抽出時間</summary>
        Private _TeiseiChusyutujikan As Nullable(Of Int32)
        ''' <summary>試作新調達への転送日</summary>
        Private _ShisakuTensoubi As Nullable(Of Int32)
        ''' <summary>試作新調達への転送時間</summary>
        Private _ShisakuTensoujikan As Nullable(Of Int32)
        ''' <summary>前回改訂日</summary>
        Private _ZenkaiKaiteibi As Nullable(Of Int32)
        ''' <summary>最新抽出日</summary>
        Private _SaishinChusyutubi As Nullable(Of Int32)
        ''' <summary>最新抽出時間</summary>
        Private _SaishinChusyutujikan As Nullable(Of Int32)
        ''' <summary>ステータス</summary>
        Private _Status As String
        ''' <summary>ユニット区分</summary>
        Private _UnitKbn As String
        ''' <summary>自動織込みフラグ</summary>
        Private _AutoOrikomiFlag As String
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
        ''' <summary>試作リスト表示順</summary>
        ''' <value>試作リスト表示順</value>
        ''' <returns>試作リスト表示順</returns>
        Public Property ShisakuListHyojijunNo() As Nullable(Of Int32)
            Get
                Return _ShisakuListHyojijunNo
            End Get
            Set(ByVal value As Nullable(Of Int32))
                _ShisakuListHyojijunNo = value
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
        ''' <summary>試作グループ№</summary>
        ''' <value>試作グループ№</value>
        ''' <returns>試作グループ№</returns>
        Public Property ShisakuGroupNo() As String
            Get
                Return _ShisakuGroupNo
            End Get
            Set(ByVal value As String)
                _ShisakuGroupNo = value
            End Set
        End Property
        ''' <summary>試作工事区分</summary>
        ''' <value>試作工事区分</value>
        ''' <returns>試作工事区分</returns>
        Public Property ShisakuKoujiKbn() As String
            Get
                Return _ShisakuKoujiKbn
            End Get
            Set(ByVal value As String)
                _ShisakuKoujiKbn = value
            End Set
        End Property
        ''' <summary>試作製品区分</summary>
        ''' <value>試作製品区分</value>
        ''' <returns>試作製品区分</returns>
        Public Property ShisakuSeihinKbn() As String
            Get
                Return _ShisakuSeihinKbn
            End Get
            Set(ByVal value As String)
                _ShisakuSeihinKbn = value
            End Set
        End Property
        ''' <summary>試作工事指令№</summary>
        ''' <value>試作工事指令№</value>
        ''' <returns>試作工事指令№</returns>
        Public Property ShisakuKoujiShireiNo() As String
            Get
                Return _ShisakuKoujiShireiNo
            End Get
            Set(ByVal value As String)
                _ShisakuKoujiShireiNo = value
            End Set
        End Property
        ''' <summary>試作工事№</summary>
        ''' <value>試作工事№</value>
        ''' <returns>試作工事№</returns>
        Public Property ShisakuKoujiNo() As String
            Get
                Return _ShisakuKoujiNo
            End Get
            Set(ByVal value As String)
                _ShisakuKoujiNo = value
            End Set
        End Property
        ''' <summary>試作イベント名称</summary>
        ''' <value>試作イベント名称</value>
        ''' <returns>試作イベント名称</returns>
        Public Property ShisakuEventName() As String
            Get
                Return _ShisakuEventName
            End Get
            Set(ByVal value As String)
                _ShisakuEventName = value
            End Set
        End Property
        ''' <summary>試作自給品の消し込み</summary>
        ''' <value>試作自給品の消し込み</value>
        ''' <returns>試作自給品の消し込み</returns>
        Public Property ShisakuJikyuhin() As String
            Get
                Return _ShisakuJikyuhin
            End Get
            Set(ByVal value As String)
                _ShisakuJikyuhin = value
            End Set
        End Property
        ''' <summary>試作比較結果折込済み</summary>
        ''' <value>試作比較結果折込済み</value>
        ''' <returns>試作比較結果折込済み</returns>
        Public Property ShisakuHikakukekka() As String
            Get
                Return _ShisakuHikakukekka
            End Get
            Set(ByVal value As String)
                _ShisakuHikakukekka = value
            End Set
        End Property
        ''' <summary>試作集計コードからの展開</summary>
        ''' <value>試作集計コードからの展開</value>
        ''' <returns>試作集計コードからの展開</returns>
        Public Property ShisakuSyuukeiCode() As String
            Get
                Return _ShisakuSyuukeiCode
            End Get
            Set(ByVal value As String)
                _ShisakuSyuukeiCode = value
            End Set
        End Property
        ''' <summary>試作台数</summary>
        ''' <value>試作台数</value>
        ''' <returns>試作台数</returns>
        Public Property ShisakuDaisu() As String
            Get
                Return _ShisakuDaisu
            End Get
            Set(ByVal value As String)
                _ShisakuDaisu = value
            End Set
        End Property
        ''' <summary>試作手配帳作成日</summary>
        ''' <value>試作手配帳作成日</value>
        ''' <returns>試作手配帳作成日</returns>
        Public Property ShisakuTehaichoSakuseibi() As Nullable(Of Int32)
            Get
                Return _ShisakuTehaichoSakuseibi
            End Get
            Set(ByVal value As Nullable(Of Int32))
                _ShisakuTehaichoSakuseibi = value
            End Set
        End Property
        ''' <summary>試作手配帳作成時間</summary>
        ''' <value>試作手配帳作成時間</value>
        ''' <returns>試作手配帳作成時間</returns>
        Public Property ShisakuTehaichoSakuseijikan() As Nullable(Of Int32)
            Get
                Return _ShisakuTehaichoSakuseijikan
            End Get
            Set(ByVal value As Nullable(Of Int32))
                _ShisakuTehaichoSakuseijikan = value
            End Set
        End Property
        ''' <summary>旧リストコード</summary>
        ''' <value>旧リストコード</value>
        ''' <returns>旧リストコード</returns>
        Public Property OldListCode() As String
            Get
                Return _OldListCode
            End Get
            Set(ByVal value As String)
                _OldListCode = value
            End Set
        End Property
        ''' <summary>試作手配帳発注用データ登録日</summary>
        ''' <value>試作手配帳発注用データ登録日</value>
        ''' <returns>試作手配帳発注用データ登録日</returns>
        Public Property ShisakuDataTourokubi() As Nullable(Of Int32)
            Get
                Return _ShisakuDataTourokubi
            End Get
            Set(ByVal value As Nullable(Of Int32))
                _ShisakuDataTourokubi = value
            End Set
        End Property
        ''' <summary>試作手配帳発注用データ登録時間</summary>
        ''' <value>試作手配帳発注用データ登録時間</value>
        ''' <returns>試作手配帳発注用データ登録時間</returns>
        Public Property ShisakuDataTourokujikan() As Nullable(Of Int32)
            Get
                Return _ShisakuDataTourokujikan
            End Get
            Set(ByVal value As Nullable(Of Int32))
                _ShisakuDataTourokujikan = value
            End Set
        End Property
        ''' <summary>履歴</summary>
        ''' <value>履歴</value>
        ''' <returns>履歴</returns>
        Public Property Rireki() As String
            Get
                Return _Rireki
            End Get
            Set(ByVal value As String)
                _Rireki = value
            End Set
        End Property
        ''' <summary>試作メモ欄</summary>
        ''' <value>試作メモ欄</value>
        ''' <returns>試作メモ欄</returns>
        Public Property ShisakuMemo() As String
            Get
                Return _ShisakuMemo
            End Get
            Set(ByVal value As String)
                _ShisakuMemo = value
            End Set
        End Property
        ''' <summary>訂正通知データ抽出日</summary>
        ''' <value>訂正通知データ抽出日</value>
        ''' <returns>訂正通知データ抽出日</returns>
        Public Property TeiseiChusyutubi() As Nullable(Of Int32)
            Get
                Return _TeiseiChusyutubi
            End Get
            Set(ByVal value As Nullable(Of Int32))
                _TeiseiChusyutubi = value
            End Set
        End Property
        ''' <summary>訂正通知データ抽出時間</summary>
        ''' <value>訂正通知データ抽出時間</value>
        ''' <returns>訂正通知データ抽出時間</returns>
        Public Property TeiseiChusyutujikan() As Nullable(Of Int32)
            Get
                Return _TeiseiChusyutujikan
            End Get
            Set(ByVal value As Nullable(Of Int32))
                _TeiseiChusyutujikan = value
            End Set
        End Property
        ''' <summary>試作新調達への転送日</summary>
        ''' <value>試作新調達への転送日</value>
        ''' <returns>試作新調達への転送日</returns>
        Public Property ShisakuTensoubi() As Nullable(Of Int32)
            Get
                Return _ShisakuTensoubi
            End Get
            Set(ByVal value As Nullable(Of Int32))
                _ShisakuTensoubi = value
            End Set
        End Property
        ''' <summary>試作新調達への転送時間</summary>
        ''' <value>試作新調達への転送時間</value>
        ''' <returns>試作新調達への転送時間</returns>
        Public Property ShisakuTensoujikan() As Nullable(Of Int32)
            Get
                Return _ShisakuTensoujikan
            End Get
            Set(ByVal value As Nullable(Of Int32))
                _ShisakuTensoujikan = value
            End Set
        End Property
        ''' <summary>前回改訂日</summary>
        ''' <value>前回改訂日</value>
        ''' <returns>前回改訂日</returns>
        Public Property ZenkaiKaiteibi() As Nullable(Of Int32)
            Get
                Return _ZenkaiKaiteibi
            End Get
            Set(ByVal value As Nullable(Of Int32))
                _ZenkaiKaiteibi = value
            End Set
        End Property
        ''' <summary>最新抽出日</summary>
        ''' <value>最新抽出日</value>
        ''' <returns>最新抽出日</returns>
        Public Property SaishinChusyutubi() As Nullable(Of Int32)
            Get
                Return _SaishinChusyutubi
            End Get
            Set(ByVal value As Nullable(Of Int32))
                _SaishinChusyutubi = value
            End Set
        End Property
        ''' <summary>最新抽出時間</summary>
        ''' <value>最新抽出時間</value>
        ''' <returns>最新抽出時間</returns>
        Public Property SaishinChusyutujikan() As Nullable(Of Int32)
            Get
                Return _SaishinChusyutujikan
            End Get
            Set(ByVal value As Nullable(Of Int32))
                _SaishinChusyutujikan = value
            End Set
        End Property
        ''' <summary>ステータス</summary>
        ''' <value>ステータス</value>
        ''' <returns>ステータス</returns>
        Public Property Status() As String
            Get
                Return _Status
            End Get
            Set(ByVal value As String)
                _Status = value
            End Set
        End Property
        ''' <summary>自動織込みフラグ</summary>
        ''' <value>自動織込みフラグ</value>
        ''' <returns>自動織込みフラグ</returns>
        Public Property AutoOrikomiFlag() As String
            Get
                Return _AutoOrikomiFlag
            End Get
            Set(ByVal value As String)
                _AutoOrikomiFlag = value
            End Set
        End Property
        ''' <summary>ユニット区分</summary>
        ''' <value>ユニット区分</value>
        ''' <returns>ユニット区分</returns>
        Public Property UnitKbn() As String
            Get
                Return _UnitKbn
            End Get
            Set(ByVal value As String)
                _UnitKbn = value
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