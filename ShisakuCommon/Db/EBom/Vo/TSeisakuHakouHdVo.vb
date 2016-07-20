Namespace Db.EBom.Vo
    ''' <summary>
    ''' 製作一覧発行情報
    ''' </summary>
    ''' <remarks></remarks>
    Public Class TSeisakuHakouHdVo
        '' 発行№
        Private _HakouNo As String
        '' 改訂№
        Private _KaiteiNo As String
        '' 開発符号
        Private _KaihatsuFugo As String
        '' イベント
        Private _SeisakuEvent As String
        '' イベント名称
        Private _SeisakuEventName As String
        '' ステータス
        Private _Status As String
        '' 製作台数・完成車
        Private _SeisakudaisuKanseisya As Nullable(Of Int32)
        '' 製作台数・Ｗ／Ｂ
        Private _SeisakudaisuWb As Nullable(of Int32)
        '' 登録日
        Private _TourokuDate As Nullable(of Int32)
        '' 登録ユーザーID
        Private _TourokuUserId As String
        '' 承認日
        Private _SyoninDate As Nullable(Of Int32)
        '' 承認ユーザーID
        Private _SyoninUserId As String
        '' 最終出力日
        Private _SyutsuryokuDate As Nullable(of Int32)
        '' 最終出力形態
        Private _SyutsuryokuKeitai As String
        '' 最終出力ユーザーID
        Private _SyutsuryokuUserId As String
        '' 保存先・編集用
        Private _HozonToHensyu As String
        '' 保存先・発行用
        Private _HozonToHakkou As String
        '' 改訂№・比較用
        Private _KaiteiNoHikaku As String
        '' 中止フラグ
        Private _ChushiFlg As String
        '' 中止日
        Private _ChushiDate As Nullable(Of Int32)
        '' 中止ユーザーID
        Private _ChushiUserId As String
        '' 完成車Ｅ/Ｇ仕様・メモ１
        Private _KanseiEgMemo1 As String
        '' 完成車Ｅ/Ｇ仕様・メモ２
        Private _KanseiEgMemo2 As String
        '' 完成車Ｔ/Ｍ仕様・メモ１
        Private _KanseiTmMemo1 As String
        '' 完成車Ｔ/Ｍ仕様・メモ２
        Private _KanseiTmMemo2 As String
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

        ''' <summary>発行№</summary>
        ''' <value>発行№</value>
        ''' <returns>発行№</returns>
        Public Property HakouNo() As String
            Get
                Return _HakouNo
            End Get
            Set(ByVal value As String)
                _HakouNo = value
            End Set
        End Property

        ''' <summary>改訂№</summary>
        ''' <value>改訂№</value>
        ''' <returns>改訂№</returns>
        Public Property KaiteiNo() As String
            Get
                Return _KaiteiNo
            End Get
            Set(ByVal value As String)
                _KaiteiNo = value
            End Set
        End Property

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

        ''' <summary>イベント</summary>
        ''' <value>イベント</value>
        ''' <returns>イベント</returns>
        Public Property SeisakuEvent() As String
            Get
                Return _SeisakuEvent
            End Get
            Set(ByVal value As String)
                _SeisakuEvent = value
            End Set
        End Property

        ''' <summary>イベント名称</summary>
        ''' <value>イベント名称</value>
        ''' <returns>イベント名称</returns>
        Public Property SeisakuEventName() As String
            Get
                Return _SeisakuEventName
            End Get
            Set(ByVal value As String)
                _SeisakuEventName = value
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

        ''' <summary>製作台数・完成車</summary>
        ''' <value>製作台数・完成車</value>
        ''' <returns>製作台数・完成車</returns>
        Public Property SeisakudaisuKanseisya() As Nullable(Of Int32)
            Get
                Return _SeisakudaisuKanseisya
            End Get
            Set(ByVal value As Nullable(Of Int32))
                _SeisakudaisuKanseisya = value
            End Set
        End Property

        ''' <summary>製作台数・Ｗ／Ｂ</summary>
        ''' <value>製作台数・Ｗ／Ｂ</value>
        ''' <returns>製作台数・Ｗ／Ｂ</returns>
        Public Property SeisakudaisuWb() As Nullable(of Int32)
            Get
                Return _SeisakudaisuWb
            End Get
            Set(ByVal value As Nullable(of Int32))
                _SeisakudaisuWb = value
            End Set
        End Property

        ''' <summary>登録日</summary>
        ''' <value>登録日</value>
        ''' <returns>登録日</returns>
        Public Property TourokuDate() As Nullable(of Int32)
            Get
                Return _TourokuDate
            End Get
            Set(ByVal value As Nullable(of Int32))
                _TourokuDate = value
            End Set
        End Property

        ''' <summary>登録ユーザーID</summary>
        ''' <value>登録ユーザーID</value>
        ''' <returns>登録ユーザーID</returns>
        Public Property TourokuUserId() As String
            Get
                Return _TourokuUserId
            End Get
            Set(ByVal value As String)
                _TourokuUserId = value
            End Set
        End Property

        ''' <summary>承認日</summary>
        ''' <value>承認日</value>
        ''' <returns>承認日</returns>
        Public Property SyoninDate() As Nullable(Of Int32)
            Get
                Return _SyoninDate
            End Get
            Set(ByVal value As Nullable(Of Int32))
                _SyoninDate = value
            End Set
        End Property

        ''' <summary>承認ユーザーID</summary>
        ''' <value>承認ユーザーID</value>
        ''' <returns>承認ユーザーID</returns>
        Public Property SyoninUserId() As String
            Get
                Return _SyoninUserId
            End Get
            Set(ByVal value As String)
                _SyoninUserId = value
            End Set
        End Property

        ''' <summary>最終出力日</summary>
        ''' <value>最終出力日</value>
        ''' <returns>最終出力日</returns>
        Public Property SyutsuryokuDate() As Nullable(of Int32)
            Get
                Return _SyutsuryokuDate
            End Get
            Set(ByVal value As Nullable(of Int32))
                _SyutsuryokuDate = value
            End Set
        End Property

        ''' <summary>最終出力形態</summary>
        ''' <value>最終出力形態</value>
        ''' <returns>最終出力形態</returns>
        Public Property SyutsuryokuKeitai() As String
            Get
                Return _SyutsuryokuKeitai
            End Get
            Set(ByVal value As String)
                _SyutsuryokuKeitai = value
            End Set
        End Property

        ''' <summary>最終出力ユーザーID</summary>
        ''' <value>最終出力ユーザーID</value>
        ''' <returns>最終出力ユーザーID</returns>
        Public Property SyutsuryokuUserId() As String
            Get
                Return _SyutsuryokuUserId
            End Get
            Set(ByVal value As String)
                _SyutsuryokuUserId = value
            End Set
        End Property

        ''' <summary>保存先・編集用</summary>
        ''' <value>保存先・編集用</value>
        ''' <returns>保存先・編集用</returns>
        Public Property HozonToHensyu() As String
            Get
                Return _HozonToHensyu
            End Get
            Set(ByVal value As String)
                _HozonToHensyu = value
            End Set
        End Property

        ''' <summary>保存先・発行用</summary>
        ''' <value>保存先・発行用</value>
        ''' <returns>保存先・発行用</returns>
        Public Property HozonToHakkou() As String
            Get
                Return _HozonToHakkou
            End Get
            Set(ByVal value As String)
                _HozonToHakkou = value
            End Set
        End Property

        ''' <summary>改訂№・比較用</summary>
        ''' <value>改訂№・比較用</value>
        ''' <returns>改訂№・比較用</returns>
        Public Property KaiteiNoHikaku() As String
            Get
                Return _KaiteiNoHikaku
            End Get
            Set(ByVal value As String)
                _KaiteiNoHikaku = value
            End Set
        End Property

        ''' <summary>中止フラグ</summary>
        ''' <value>中止フラグ</value>
        ''' <returns>中止フラグ</returns>
        Public Property ChushiFlg() As String
            Get
                Return _ChushiFlg
            End Get
            Set(ByVal value As String)
                _ChushiFlg = value
            End Set
        End Property

        ''' <summary>中止日</summary>
        ''' <value>中止日</value>
        ''' <returns>中止日</returns>
        Public Property ChushiDate() As Nullable(Of Int32)
            Get
                Return _ChushiDate
            End Get
            Set(ByVal value As Nullable(Of Int32))
                _ChushiDate = value
            End Set
        End Property

        ''' <summary>中止ユーザーID</summary>
        ''' <value>中止ユーザーID</value>
        ''' <returns>中止ユーザーID</returns>
        Public Property ChushiUserId() As String
            Get
                Return _ChushiUserId
            End Get
            Set(ByVal value As String)
                _ChushiUserId = value
            End Set
        End Property

        ''' <summary>完成車Ｅ/Ｇ仕様・メモ１</summary>
        ''' <value>完成車Ｅ/Ｇ仕様・メモ１</value>
        ''' <returns>完成車Ｅ/Ｇ仕様・メモ１</returns>
        Public Property KanseiEgMemo1() As String
            Get
                Return _KanseiEgMemo1
            End Get
            Set(ByVal value As String)
                _KanseiEgMemo1 = value
            End Set
        End Property

        ''' <summary>完成車Ｅ/Ｇ仕様・メモ２</summary>
        ''' <value>完成車Ｅ/Ｇ仕様・メモ２</value>
        ''' <returns>完成車Ｅ/Ｇ仕様・メモ２</returns>
        Public Property KanseiEgMemo2() As String
            Get
                Return _KanseiEgMemo2
            End Get
            Set(ByVal value As String)
                _KanseiEgMemo2 = value
            End Set
        End Property

        ''' <summary>完成車Ｔ/Ｍ仕様・メモ１</summary>
        ''' <value>完成車Ｔ/Ｍ仕様・メモ１</value>
        ''' <returns>完成車Ｔ/Ｍ仕様・メモ１</returns>
        Public Property KanseiTmMemo1() As String
            Get
                Return _KanseiTmMemo1
            End Get
            Set(ByVal value As String)
                _KanseiTmMemo1 = value
            End Set
        End Property

        ''' <summary>完成車Ｔ/Ｍ仕様・メモ２</summary>
        ''' <value>完成車Ｔ/Ｍ仕様・メモ２</value>
        ''' <returns>完成車Ｔ/Ｍ仕様・メモ２</returns>
        Public Property KanseiTmMemo2() As String
            Get
                Return _KanseiTmMemo2
            End Get
            Set(ByVal value As String)
                _KanseiTmMemo2 = value
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
