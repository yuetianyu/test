Namespace Db.EBom.Vo
    ''' <summary>
    ''' 補用イベント情報
    ''' </summary>
    ''' <remarks></remarks>
    Public Class THoyouEventVo
        '' 補用イベントコード
        Private _HoyouEventCode As String
        '' 補用車系
        Private _HoyouShakeiCode As String
        '' 補用開発符号
        Private _HoyouKaihatsuFugo As String
        '' 車系/開発符号マスター表示順
        Private _HyojijunNo As Nullable(of Int32)
        '' 補用イベントフェーズ
        Private _HoyouEventPhase As String
        '' 補用イベントフェーズ名
        Private _HoyouEventPhaseName As String
        '' ユニット区分
        Private _UnitKbn As String
        '' 補用イベント名称
        Private _HoyouEventName As String
        '' メモ
        Private _Memo As String
        '' イベント情報登録担当所属
        Private _EventTourokuTantoKa As String
        '' イベント情報登録担当者
        Private _EventTourokuTanto As String
        '' 設計社員番号
        Private _SekkeiShainNo As String
        '' 設計展開日
        Private _SekkeiTenkaibi As Nullable(Of Int32)
        '' 訂正処置〆切日
        Private _KaiteiSyochiShimekiribi As Nullable(of Int32)
        '' 〆切日
        Private _Shimekiribi As Nullable(Of Int32)
        '' 完了日
        Private _Kanryoubi As Nullable(of Int32)
        '' 中止日
        Private _Chuushibi As Nullable(of Int32)
        '' データ区分
        Private _DataKbn As String
        '' ステータス
        Private _Status As String
        '' 総ブロック数
        Private _TotalQty As Nullable(Of Int32)
        '' 完了数
        Private _TotalCompQty As Nullable(of Int32)
        '' 手配帳作成日
        Private _TehaichouSakuseibi As Nullable(of Int32)
        '' 最終改訂抽出日
        Private _LastKaiteiChusyutubi As Nullable(of Int32)
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
        '' 登録日
        Private _RegisterDate As String
        '' 登録時間
        Private _RegisterTime As String
        '' 展開ステータス
        Private _TenkaiStatus As String
        '' 保存フラグ
        Private _SaveFlg As String

        ''' <summary>補用イベントコード</summary>
        ''' <value>補用イベントコード</value>
        ''' <returns>補用イベントコード</returns>
        Public Property HoyouEventCode() As String
            Get
                Return _HoyouEventCode
            End Get
            Set(ByVal value As String)
                _HoyouEventCode = value
            End Set
        End Property

        ''' <summary>補用車系</summary>
        ''' <value>補用車系</value>
        ''' <returns>補用車系</returns>
        Public Property HoyouShakeiCode() As String
            Get
                Return _HoyouShakeiCode
            End Get
            Set(ByVal value As String)
                _HoyouShakeiCode = value
            End Set
        End Property

        ''' <summary>補用開発符号</summary>
        ''' <value>補用開発符号</value>
        ''' <returns>補用開発符号</returns>
        Public Property HoyouKaihatsuFugo() As String
            Get
                Return _HoyouKaihatsuFugo
            End Get
            Set(ByVal value As String)
                _HoyouKaihatsuFugo = value
            End Set
        End Property

        ''' <summary>車系/開発符号マスター表示順</summary>
        ''' <value>車系/開発符号マスター表示順</value>
        ''' <returns>車系/開発符号マスター表示順</returns>
        Public Property HyojijunNo() As Nullable(of Int32)
            Get
                Return _HyojijunNo
            End Get
            Set(ByVal value As Nullable(of Int32))
                _HyojijunNo = value
            End Set
        End Property

        ''' <summary>補用イベントフェーズ</summary>
        ''' <value>補用イベントフェーズ</value>
        ''' <returns>補用イベントフェーズ</returns>
        Public Property HoyouEventPhase() As String
            Get
                Return _HoyouEventPhase
            End Get
            Set(ByVal value As String)
                _HoyouEventPhase = value
            End Set
        End Property

        ''' <summary>補用イベントフェーズ名</summary>
        ''' <value>補用イベントフェーズ名</value>
        ''' <returns>補用イベントフェーズ名</returns>
        Public Property HoyouEventPhaseName() As String
            Get
                Return _HoyouEventPhaseName
            End Get
            Set(ByVal value As String)
                _HoyouEventPhaseName = value
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

        ''' <summary>補用イベント名称</summary>
        ''' <value>補用イベント名称</value>
        ''' <returns>補用イベント名称</returns>
        Public Property HoyouEventName() As String
            Get
                Return _HoyouEventName
            End Get
            Set(ByVal value As String)
                _HoyouEventName = value
            End Set
        End Property

        ''' <summary>メモ</summary>
        ''' <value>メモ</value>
        ''' <returns>メモ</returns>
        Public Property Memo() As String
            Get
                Return _Memo
            End Get
            Set(ByVal value As String)
                _Memo = value
            End Set
        End Property


        ''' <summary>イベント情報登録担当所属</summary>
        ''' <value>イベント情報登録担当所属</value>
        ''' <returns>イベント情報登録担当所属</returns>
        Public Property EventTourokuTantoKa() As String
            Get
                Return _EventTourokuTantoKa
            End Get
            Set(ByVal value As String)
                _EventTourokuTantoKa = value
            End Set
        End Property

        ''' <summary>イベント情報登録担当者</summary>
        ''' <value>イベント情報登録担当者</value>
        ''' <returns>イベント情報登録担当者</returns>
        Public Property EventTourokuTanto() As String
            Get
                Return _EventTourokuTanto
            End Get
            Set(ByVal value As String)
                _EventTourokuTanto = value
            End Set
        End Property

        ''' <summary>設計社員番号</summary>
        ''' <value>設計社員番号</value>
        ''' <returns>設計社員番号</returns>
        Public Property SekkeiShainNo() As String
            Get
                Return _SekkeiShainNo
            End Get
            Set(ByVal value As String)
                _SekkeiShainNo = value
            End Set
        End Property

        ''' <summary>設計展開日</summary>
        ''' <value>設計展開日</value>
        ''' <returns>設計展開日</returns>
        Public Property SekkeiTenkaibi() As Nullable(Of Int32)
            Get
                Return _SekkeiTenkaibi
            End Get
            Set(ByVal value As Nullable(Of Int32))
                _SekkeiTenkaibi = value
            End Set
        End Property

        ''' <summary>訂正処置〆切日</summary>
        ''' <value>訂正処置〆切日</value>
        ''' <returns>訂正処置〆切日</returns>
        Public Property KaiteiSyochiShimekiribi() As Nullable(of Int32)
            Get
                Return _KaiteiSyochiShimekiribi
            End Get
            Set(ByVal value As Nullable(of Int32))
                _KaiteiSyochiShimekiribi = value
            End Set
        End Property

        ''' <summary>受付日</summary>
        ''' <value>受付日</value>
        ''' <returns>受付日</returns>
        Public Property Shimekiribi() As Nullable(Of Int32)
            Get
                Return _Shimekiribi
            End Get
            Set(ByVal value As Nullable(Of Int32))
                _Shimekiribi = value
            End Set
        End Property

        ''' <summary>完了日</summary>
        ''' <value>完了日</value>
        ''' <returns>完了日</returns>
        Public Property Kanryoubi() As Nullable(of Int32)
            Get
                Return _Kanryoubi
            End Get
            Set(ByVal value As Nullable(of Int32))
                _Kanryoubi = value
            End Set
        End Property

        ''' <summary>中止日</summary>
        ''' <value>中止日</value>
        ''' <returns>中止日</returns>
        Public Property Chuushibi() As Nullable(of Int32)
            Get
                Return _Chuushibi
            End Get
            Set(ByVal value As Nullable(of Int32))
                _Chuushibi = value
            End Set
        End Property

        ''' <summary>データ区分</summary>
        ''' <value>データ区分</value>
        ''' <returns>データ区分</returns>
        Public Property DataKbn() As String
            Get
                Return _DataKbn
            End Get
            Set(ByVal value As String)
                _DataKbn = value
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

        ''' <summary>総数</summary>
        ''' <value>総数</value>
        ''' <returns>総数</returns>
        Public Property TotalQty() As Nullable(Of Int32)
            Get
                Return _TotalQty
            End Get
            Set(ByVal value As Nullable(Of Int32))
                _TotalQty = value
            End Set
        End Property

        ''' <summary>完了数</summary>
        ''' <value>完了数</value>
        ''' <returns>完了数</returns>
        Public Property TotalCompQty() As Nullable(of Int32)
            Get
                Return _TotalCompQty
            End Get
            Set(ByVal value As Nullable(of Int32))
                _TotalCompQty = value
            End Set
        End Property

        ''' <summary>手配帳作成日</summary>
        ''' <value>手配帳作成日</value>
        ''' <returns>手配帳作成日</returns>
        Public Property TehaichouSakuseibi() As Nullable(of Int32)
            Get
                Return _TehaichouSakuseibi
            End Get
            Set(ByVal value As Nullable(of Int32))
                _TehaichouSakuseibi = value
            End Set
        End Property

        ''' <summary>最終改訂抽出日</summary>
        ''' <value>最終改訂抽出日</value>
        ''' <returns>最終改訂抽出日</returns>
        Public Property LastKaiteiChusyutubi() As Nullable(of Int32)
            Get
                Return _LastKaiteiChusyutubi
            End Get
            Set(ByVal value As Nullable(of Int32))
                _LastKaiteiChusyutubi = value
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
        ''' <summary>登録日</summary>
        ''' <value>登録日</value>
        ''' <returns>登録日</returns>
        Public Property RegisterDate() As String
            Get
                Return _RegisterDate
            End Get
            Set(ByVal value As String)
                _RegisterDate = value
            End Set
        End Property

        ''' <summary>登録時間</summary>
        ''' <value>登録時間</value>
        ''' <returns>登録時間</returns>
        Public Property RegisterTime() As String
            Get
                Return _RegisterTime
            End Get
            Set(ByVal value As String)
                _RegisterTime = value
            End Set
        End Property
        ''' <summary>展開ステータス</summary>
        ''' <value>展開ステータス</value>
        ''' <returns>展開ステータス</returns>
        Public Property TenkaiStatus() As String
            Get
                Return _TenkaiStatus
            End Get
            Set(ByVal value As String)
                _TenkaiStatus = value
            End Set
        End Property

        ''' <summary>保存フラグ</summary>
        ''' <value>保存フラグ</value>
        ''' <returns>保存フラグ</returns>
        Public Property SaveFlg() As String
            Get
                Return _SaveFlg
            End Get
            Set(ByVal value As String)
                _SaveFlg = value
            End Set
        End Property

    End Class
End Namespace
