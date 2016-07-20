Namespace Db.EBom.Vo
    ''' <summary>予算書イベント情報</summary>
    Public Class TYosanEventVo
        ''' <summary>予算イベントコード</summary>
        Private _YosanEventCode As String
        ''' <summary>予算イベント区分名</summary>
        Private _YosanEventKbnName As String
        ''' <summary>予算開発符号</summary>
        Private _YosanKaihatsuFugo As String
        ''' <summary>予算イベント</summary>
        Private _YosanEvent As String
        ''' <summary>予算イベント名称</summary>
        Private _YosanEventName As String
        ''' <summary>予算コード</summary>
        Private _YosanCode As String
        ''' <summary>予算期間FROM年</summary>
        Private _YosanKikanFromYyyy As String
        ''' <summary>予算期間FROM上期下期</summary>
        Private _YosanKikanFromKs As String
        ''' <summary>予算期間TO年</summary>
        Private _YosanKikanToYyyy As String
        ''' <summary>予算期間TO上期下期</summary>
        Private _YosanKikanToKs As String
        ''' <summary>予算製作台数・完成車</summary>
        Private _YosanSeisakudaisuKanseisya As Nullable(Of Int32)
        ''' <summary>予算製作台数・W/B</summary>
        Private _YosanSeisakudaisuWb As Nullable(Of Int32)
        ''' <summary>主な変更概要</summary>
        Private _YosanMainHenkoGaiyo As String
        ''' <summary>造り方及び製作条件</summary>
        Private _YosanTsukurikataSeisakujyoken As String
        ''' <summary>その他</summary>
        Private _YosanSonota As String
        ''' <summary>ステータス</summary>
        Private _YosanStatus As String
        ''' <summary>イベント登録日</summary>
        Private _YosanEventTourokubi As Nullable(Of Int32)
        ''' <summary>イベント完了日</summary>
        Private _YosanEventKanryoubi As Nullable(Of Int32)
        ''' <summary>最終部品表保存日</summary>
        Private _YosanSaveDateBuhin As String
        ''' <summary>最終部品表保存時刻</summary>
        Private _YosanSaveTimeBuhin As String
        ''' <summary>最終部品表登録日</summary>
        Private _YosanRegisterDateBuhin As String
        ''' <summary>最終部品表登録時刻</summary>
        Private _YosanRegisterTimeBuhin As String
        ''' <summary>最終財務実績取込日</summary>
        Private _YosanImportDateZaimu As String
        ''' <summary>最終財務実績取込時刻</summary>
        Private _YosanImportTimeZaimu As String
        ''' <summary>最終量産単価表取込日</summary>
        Private _YosanImportDateRyosan As String
        ''' <summary>最終量産単価表取込時刻</summary>
        Private _YosanImportTimeRyosan As String
        ''' <summary>一時保存時のメモ</summary>
        Private _Memo As String
        ''' <summary>W/B係数</summary>
        Private _YosanWbKeisu As Nullable(Of Decimal)
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

        ''' <summary>予算イベントコード</summary>
        ''' <value>予算イベントコード</value>
        ''' <returns>予算イベントコード</returns>
        Public Property YosanEventCode() As String
            Get
                Return _YosanEventCode
            End Get
            Set(ByVal value As String)
                _YosanEventCode = value
            End Set
        End Property
        ''' <summary>予算イベント区分名</summary>
        ''' <value>予算イベント区分名</value>
        ''' <returns>予算イベント区分名</returns>
        Public Property YosanEventKbnName() As String
            Get
                Return _YosanEventKbnName
            End Get
            Set(ByVal value As String)
                _YosanEventKbnName = value
            End Set
        End Property
        ''' <summary>予算開発符号</summary>
        ''' <value>予算開発符号</value>
        ''' <returns>予算開発符号</returns>
        Public Property YosanKaihatsuFugo() As String
            Get
                Return _YosanKaihatsuFugo
            End Get
            Set(ByVal value As String)
                _YosanKaihatsuFugo = value
            End Set
        End Property
        ''' <summary>予算イベント</summary>
        ''' <value>予算イベント</value>
        ''' <returns>予算イベント</returns>
        Public Property YosanEvent() As String
            Get
                Return _YosanEvent
            End Get
            Set(ByVal value As String)
                _YosanEvent = value
            End Set
        End Property
        ''' <summary>予算イベント名称</summary>
        ''' <value>予算イベント名称</value>
        ''' <returns>予算イベント名称</returns>
        Public Property YosanEventName() As String
            Get
                Return _YosanEventName
            End Get
            Set(ByVal value As String)
                _YosanEventName = value
            End Set
        End Property
        ''' <summary>予算コード</summary>
        ''' <value>予算コード</value>
        ''' <returns>予算コード</returns>
        Public Property YosanCode() As String
            Get
                Return _YosanCode
            End Get
            Set(ByVal value As String)
                _YosanCode = value
            End Set
        End Property
        ''' <summary>予算期間FROM年</summary>
        ''' <value>予算期間FROM年</value>
        ''' <returns>予算期間FROM年</returns>
        Public Property YosanKikanFromYyyy() As String
            Get
                Return _YosanKikanFromYyyy
            End Get
            Set(ByVal value As String)
                _YosanKikanFromYyyy = value
            End Set
        End Property
        ''' <summary>予算期間FROM上期下期</summary>
        ''' <value>予算期間FROM上期下期</value>
        ''' <returns>予算期間FROM上期下期</returns>
        Public Property YosanKikanFromKs() As String
            Get
                Return _YosanKikanFromKs
            End Get
            Set(ByVal value As String)
                _YosanKikanFromKs = value
            End Set
        End Property
        ''' <summary>予算期間TO年</summary>
        ''' <value>予算期間TO年</value>
        ''' <returns>予算期間TO年</returns>
        Public Property YosanKikanToYyyy() As String
            Get
                Return _YosanKikanToYyyy
            End Get
            Set(ByVal value As String)
                _YosanKikanToYyyy = value
            End Set
        End Property
        ''' <summary>予算期間TO上期下期</summary>
        ''' <value>予算期間TO上期下期</value>
        ''' <returns>予算期間TO上期下期</returns>
        Public Property YosanKikanToKs() As String
            Get
                Return _YosanKikanToKs
            End Get
            Set(ByVal value As String)
                _YosanKikanToKs = value
            End Set
        End Property
        ''' <summary>予算製作台数・完成車</summary>
        ''' <value>予算製作台数・完成車</value>
        ''' <returns>予算製作台数・完成車</returns>
        Public Property YosanSeisakudaisuKanseisya() As Nullable(Of Int32)
            Get
                Return _YosanSeisakudaisuKanseisya
            End Get
            Set(ByVal value As Nullable(Of Int32))
                _YosanSeisakudaisuKanseisya = value
            End Set
        End Property
        ''' <summary>予算製作台数・W/B</summary>
        ''' <value>予算製作台数・W/B</value>
        ''' <returns>予算製作台数・W/B</returns>
        Public Property YosanSeisakudaisuWb() As Nullable(Of Int32)
            Get
                Return _YosanSeisakudaisuWb
            End Get
            Set(ByVal value As Nullable(Of Int32))
                _YosanSeisakudaisuWb = value
            End Set
        End Property
        ''' <summary>主な変更概要</summary>
        ''' <value>主な変更概要</value>
        ''' <returns>主な変更概要</returns>
        Public Property YosanMainHenkoGaiyo() As String
            Get
                Return _YosanMainHenkoGaiyo
            End Get
            Set(ByVal value As String)
                _YosanMainHenkoGaiyo = value
            End Set
        End Property
        ''' <summary>造り方及び製作条件</summary>
        ''' <value>造り方及び製作条件</value>
        ''' <returns>造り方及び製作条件</returns>
        Public Property YosanTsukurikataSeisakujyoken() As String
            Get
                Return _YosanTsukurikataSeisakujyoken
            End Get
            Set(ByVal value As String)
                _YosanTsukurikataSeisakujyoken = value
            End Set
        End Property
        ''' <summary>その他</summary>
        ''' <value>その他</value>
        ''' <returns>その他</returns>
        Public Property YosanSonota() As String
            Get
                Return _YosanSonota
            End Get
            Set(ByVal value As String)
                _YosanSonota = value
            End Set
        End Property
        ''' <summary>ステータス</summary>
        ''' <value>ステータス</value>
        ''' <returns>ステータス</returns>
        Public Property YosanStatus() As String
            Get
                Return _YosanStatus
            End Get
            Set(ByVal value As String)
                _YosanStatus = value
            End Set
        End Property
        ''' <summary>イベント登録日</summary>
        ''' <value>イベント登録日</value>
        ''' <returns>イベント登録日</returns>
        Public Property YosanEventTourokubi() As Nullable(Of Int32)
            Get
                Return _YosanEventTourokubi
            End Get
            Set(ByVal value As Nullable(Of Int32))
                _YosanEventTourokubi = value
            End Set
        End Property
        ''' <summary>イベント完了日</summary>
        ''' <value>イベント完了日</value>
        ''' <returns>イベント完了日</returns>
        Public Property YosanEventKanryoubi() As Nullable(Of Int32)
            Get
                Return _YosanEventKanryoubi
            End Get
            Set(ByVal value As Nullable(Of Int32))
                _YosanEventKanryoubi = value
            End Set
        End Property
        ''' <summary>最終部品表保存日</summary>
        ''' <value>最終部品表保存日</value>
        ''' <returns>最終部品表保存日</returns>
        Public Property YosanSaveDateBuhin() As String
            Get
                Return _YosanSaveDateBuhin
            End Get
            Set(ByVal value As String)
                _YosanSaveDateBuhin = value
            End Set
        End Property
        ''' <summary>最終部品表保存時刻</summary>
        ''' <value>最終部品表保存時刻</value>
        ''' <returns>最終部品表保存時刻</returns>
        Public Property YosanSaveTimeBuhin() As String
            Get
                Return _YosanSaveTimeBuhin
            End Get
            Set(ByVal value As String)
                _YosanSaveTimeBuhin = value
            End Set
        End Property
        ''' <summary>最終部品表登録日</summary>
        ''' <value>最終部品表登録日</value>
        ''' <returns>最終部品表登録日</returns>
        Public Property YosanRegisterDateBuhin() As String
            Get
                Return _YosanRegisterDateBuhin
            End Get
            Set(ByVal value As String)
                _YosanRegisterDateBuhin = value
            End Set
        End Property
        ''' <summary>最終部品表登録時刻</summary>
        ''' <value>最終部品表登録時刻</value>
        ''' <returns>最終部品表登録時刻</returns>
        Public Property YosanRegisterTimeBuhin() As String
            Get
                Return _YosanRegisterTimeBuhin
            End Get
            Set(ByVal value As String)
                _YosanRegisterTimeBuhin = value
            End Set
        End Property
        ''' <summary>最終財務実績取込日</summary>
        ''' <value>最終財務実績取込日</value>
        ''' <returns>最終財務実績取込日</returns>
        Public Property YosanImportDateZaimu() As String
            Get
                Return _YosanImportDateZaimu
            End Get
            Set(ByVal value As String)
                _YosanImportDateZaimu = value
            End Set
        End Property
        ''' <summary>最終財務実績取込時刻</summary>
        ''' <value>最終財務実績取込時刻</value>
        ''' <returns>最終財務実績取込時刻</returns>
        Public Property YosanImportTimeZaimu() As String
            Get
                Return _YosanImportTimeZaimu
            End Get
            Set(ByVal value As String)
                _YosanImportTimeZaimu = value
            End Set
        End Property
        ''' <summary>最終量産単価表取込日</summary>
        ''' <value>最終量産単価表取込日</value>
        ''' <returns>最終量産単価表取込日</returns>
        Public Property YosanImportDateRyosan() As String
            Get
                Return _YosanImportDateRyosan
            End Get
            Set(ByVal value As String)
                _YosanImportDateRyosan = value
            End Set
        End Property
        ''' <summary>最終量産単価表取込時刻</summary>
        ''' <value>最終量産単価表取込時刻</value>
        ''' <returns>最終量産単価表取込時刻</returns>
        Public Property YosanImportTimeRyosan() As String
            Get
                Return _YosanImportTimeRyosan
            End Get
            Set(ByVal value As String)
                _YosanImportTimeRyosan = value
            End Set
        End Property
        ''' <summary>一時保存時のメモ</summary>
        ''' <value>一時保存時のメモ</value>
        ''' <returns>一時保存時のメモ</returns>
        Public Property Memo() As String
            Get
                Return _Memo
            End Get
            Set(ByVal value As String)
                _Memo = value
            End Set
        End Property
        ''' <summary>W/B係数</summary>
        ''' <value>W/B係数</value>
        ''' <returns>W/B係数</returns>
        Public Property YosanWbKeisu() As Nullable(Of Decimal)
            Get
                Return _YosanWbKeisu
            End Get
            Set(ByVal value As Nullable(Of Decimal))
                _YosanWbKeisu = value
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