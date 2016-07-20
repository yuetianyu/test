Namespace Db.EBom.Vo
    ''' <summary>
    ''' 図面(図面テーブル群)
    ''' </summary>
    ''' <remarks></remarks>
    Public Class Rhac0702Vo
        '' 図面番号
        Private _ZumenNo As String
        '' 図面改訂No.
        Private _ZumenKaiteiNo As String
        '' ブロックNo.(機能)
        Private _BlockNoKino As String
        '' 設計通知書番号
        Private _TsuchishoNo As String
        '' 改訂コード
        Private _KaiteiCode As String
        '' 設計変更理由コード
        Private _HenkoRiyuCode As String
        '' シリーズ区分
        Private _SiriesKbn As String
        '' 設計通知書シリーズ
        Private _TsuchishoSiries As String
        '' 図判コード
        Private _ZuhanCode As String
        '' 図面コード
        Private _ZumenCode As String
        '' 作成社員番号
        Private _SakuseiShainNo As String
        '' 作成日付
        Private _SakuseiDate As Nullable(Of Int32)
        '' 出図社員番号
        Private _ShutuzuShainNo As String
        '' 出図日付
        Private _ShutuzuDate As Nullable(Of Int32)
        '' 尺度
        Private _ShakudoRitsu As String
        '' 統合改訂No.
        Private _TogoKaiteiNo As String
        '' 関連資料No.
        Private _KanrenShiryoNo As String
        '' 正規外配布部署名称
        Private _SeikigaiHaifuBu As String
        '' 3次元データランク
        Private _DataRank As String
        '' 普通寸法許容差
        Private _FutsusunpoKyoyosa As String
        '' 分割枚数
        Private _BunkatsuMaisu As Nullable(Of Int32)
        '' CATIA承認済エリア名
        Private _ShoninAreaName As String
        '' CATIA作業エリア名
        Private _SagyoAreaName As String
        '' 確認サイン
        Private _KakuninSign As String
        '' 確認日付
        Private _KakuninDate As Nullable(Of Int32)
        '' 承認サイン
        Private _ShoninSign As String
        '' 承認日付
        Private _ShoninDate As Nullable(Of Int32)
        '' 適用メモ
        Private _TekiyoMemo As String
        '' 採用年月日
        Private _SaiyoDate As Nullable(Of Int32)
        '' 廃止年月日
        Private _HaisiDate As Nullable(Of Int32)
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

        ''' <summary>図面番号</summary>
        ''' <value>図面番号</value>
        ''' <returns>図面番号</returns>
        Public Property ZumenNo() As String
            Get
                Return _ZumenNo
            End Get
            Set(ByVal value As String)
                _ZumenNo = value
            End Set
        End Property

        ''' <summary>図面改訂No.</summary>
        ''' <value>図面改訂No.</value>
        ''' <returns>図面改訂No.</returns>
        Public Property ZumenKaiteiNo() As String
            Get
                Return _ZumenKaiteiNo
            End Get
            Set(ByVal value As String)
                _ZumenKaiteiNo = value
            End Set
        End Property

        ''' <summary>ブロックNo.(機能)</summary>
        ''' <value>ブロックNo.(機能)</value>
        ''' <returns>ブロックNo.(機能)</returns>
        Public Property BlockNoKino() As String
            Get
                Return _BlockNoKino
            End Get
            Set(ByVal value As String)
                _BlockNoKino = value
            End Set
        End Property

        ''' <summary>設計通知書番号</summary>
        ''' <value>設計通知書番号</value>
        ''' <returns>設計通知書番号</returns>
        Public Property TsuchishoNo() As String
            Get
                Return _TsuchishoNo
            End Get
            Set(ByVal value As String)
                _TsuchishoNo = value
            End Set
        End Property

        ''' <summary>改訂コード</summary>
        ''' <value>改訂コード</value>
        ''' <returns>改訂コード</returns>
        Public Property KaiteiCode() As String
            Get
                Return _KaiteiCode
            End Get
            Set(ByVal value As String)
                _KaiteiCode = value
            End Set
        End Property

        ''' <summary>設計変更理由コード</summary>
        ''' <value>設計変更理由コード</value>
        ''' <returns>設計変更理由コード</returns>
        Public Property HenkoRiyuCode() As String
            Get
                Return _HenkoRiyuCode
            End Get
            Set(ByVal value As String)
                _HenkoRiyuCode = value
            End Set
        End Property

        ''' <summary>シリーズ区分</summary>
        ''' <value>シリーズ区分</value>
        ''' <returns>シリーズ区分</returns>
        Public Property SiriesKbn() As String
            Get
                Return _SiriesKbn
            End Get
            Set(ByVal value As String)
                _SiriesKbn = value
            End Set
        End Property

        ''' <summary>設計通知書シリーズ</summary>
        ''' <value>設計通知書シリーズ</value>
        ''' <returns>設計通知書シリーズ</returns>
        Public Property TsuchishoSiries() As String
            Get
                Return _TsuchishoSiries
            End Get
            Set(ByVal value As String)
                _TsuchishoSiries = value
            End Set
        End Property

        ''' <summary>図判コード</summary>
        ''' <value>図判コード</value>
        ''' <returns>図判コード</returns>
        Public Property ZuhanCode() As String
            Get
                Return _ZuhanCode
            End Get
            Set(ByVal value As String)
                _ZuhanCode = value
            End Set
        End Property

        ''' <summary>図面コード</summary>
        ''' <value>図面コード</value>
        ''' <returns>図面コード</returns>
        Public Property ZumenCode() As String
            Get
                Return _ZumenCode
            End Get
            Set(ByVal value As String)
                _ZumenCode = value
            End Set
        End Property

        ''' <summary>作成社員番号</summary>
        ''' <value>作成社員番号</value>
        ''' <returns>作成社員番号</returns>
        Public Property SakuseiShainNo() As String
            Get
                Return _SakuseiShainNo
            End Get
            Set(ByVal value As String)
                _SakuseiShainNo = value
            End Set
        End Property

        ''' <summary>作成日付</summary>
        ''' <value>作成日付</value>
        ''' <returns>作成日付</returns>
        Public Property SakuseiDate() As Nullable(Of Int32)
            Get
                Return _SakuseiDate
            End Get
            Set(ByVal value As Nullable(Of Int32))
                _SakuseiDate = value
            End Set
        End Property

        ''' <summary>出図社員番号</summary>
        ''' <value>出図社員番号</value>
        ''' <returns>出図社員番号</returns>
        Public Property ShutuzuShainNo() As String
            Get
                Return _ShutuzuShainNo
            End Get
            Set(ByVal value As String)
                _ShutuzuShainNo = value
            End Set
        End Property

        ''' <summary>出図日付</summary>
        ''' <value>出図日付</value>
        ''' <returns>出図日付</returns>
        Public Property ShutuzuDate() As Nullable(Of Int32)
            Get
                Return _ShutuzuDate
            End Get
            Set(ByVal value As Nullable(Of Int32))
                _ShutuzuDate = value
            End Set
        End Property

        ''' <summary>尺度</summary>
        ''' <value>尺度</value>
        ''' <returns>尺度</returns>
        Public Property ShakudoRitsu() As String
            Get
                Return _ShakudoRitsu
            End Get
            Set(ByVal value As String)
                _ShakudoRitsu = value
            End Set
        End Property

        ''' <summary>統合改訂No.</summary>
        ''' <value>統合改訂No.</value>
        ''' <returns>統合改訂No.</returns>
        Public Property TogoKaiteiNo() As String
            Get
                Return _TogoKaiteiNo
            End Get
            Set(ByVal value As String)
                _TogoKaiteiNo = value
            End Set
        End Property

        ''' <summary>関連資料No.</summary>
        ''' <value>関連資料No.</value>
        ''' <returns>関連資料No.</returns>
        Public Property KanrenShiryoNo() As String
            Get
                Return _KanrenShiryoNo
            End Get
            Set(ByVal value As String)
                _KanrenShiryoNo = value
            End Set
        End Property

        ''' <summary>正規外配布部署名称</summary>
        ''' <value>正規外配布部署名称</value>
        ''' <returns>正規外配布部署名称</returns>
        Public Property SeikigaiHaifuBu() As String
            Get
                Return _SeikigaiHaifuBu
            End Get
            Set(ByVal value As String)
                _SeikigaiHaifuBu = value
            End Set
        End Property

        ''' <summary>3次元データランク</summary>
        ''' <value>3次元データランク</value>
        ''' <returns>3次元データランク</returns>
        Public Property DataRank() As String
            Get
                Return _DataRank
            End Get
            Set(ByVal value As String)
                _DataRank = value
            End Set
        End Property

        ''' <summary>普通寸法許容差</summary>
        ''' <value>普通寸法許容差</value>
        ''' <returns>普通寸法許容差</returns>
        Public Property FutsusunpoKyoyosa() As String
            Get
                Return _FutsusunpoKyoyosa
            End Get
            Set(ByVal value As String)
                _FutsusunpoKyoyosa = value
            End Set
        End Property

        ''' <summary>分割枚数</summary>
        ''' <value>分割枚数</value>
        ''' <returns>分割枚数</returns>
        Public Property BunkatsuMaisu() As Nullable(Of Int32)
            Get
                Return _BunkatsuMaisu
            End Get
            Set(ByVal value As Nullable(Of Int32))
                _BunkatsuMaisu = value
            End Set
        End Property

        ''' <summary>CATIA承認済エリア名</summary>
        ''' <value>CATIA承認済エリア名</value>
        ''' <returns>CATIA承認済エリア名</returns>
        Public Property ShoninAreaName() As String
            Get
                Return _ShoninAreaName
            End Get
            Set(ByVal value As String)
                _ShoninAreaName = value
            End Set
        End Property

        ''' <summary>CATIA作業エリア名</summary>
        ''' <value>CATIA作業エリア名</value>
        ''' <returns>CATIA作業エリア名</returns>
        Public Property SagyoAreaName() As String
            Get
                Return _SagyoAreaName
            End Get
            Set(ByVal value As String)
                _SagyoAreaName = value
            End Set
        End Property

        ''' <summary>確認サイン</summary>
        ''' <value>確認サイン</value>
        ''' <returns>確認サイン</returns>
        Public Property KakuninSign() As String
            Get
                Return _KakuninSign
            End Get
            Set(ByVal value As String)
                _KakuninSign = value
            End Set
        End Property

        ''' <summary>確認日付</summary>
        ''' <value>確認日付</value>
        ''' <returns>確認日付</returns>
        Public Property KakuninDate() As Nullable(Of Int32)
            Get
                Return _KakuninDate
            End Get
            Set(ByVal value As Nullable(Of Int32))
                _KakuninDate = value
            End Set
        End Property

        ''' <summary>承認サイン</summary>
        ''' <value>承認サイン</value>
        ''' <returns>承認サイン</returns>
        Public Property ShoninSign() As String
            Get
                Return _ShoninSign
            End Get
            Set(ByVal value As String)
                _ShoninSign = value
            End Set
        End Property

        ''' <summary>承認日付</summary>
        ''' <value>承認日付</value>
        ''' <returns>承認日付</returns>
        Public Property ShoninDate() As Nullable(Of Int32)
            Get
                Return _ShoninDate
            End Get
            Set(ByVal value As Nullable(Of Int32))
                _ShoninDate = value
            End Set
        End Property

        ''' <summary>適用メモ</summary>
        ''' <value>適用メモ</value>
        ''' <returns>適用メモ</returns>
        Public Property TekiyoMemo() As String
            Get
                Return _TekiyoMemo
            End Get
            Set(ByVal value As String)
                _TekiyoMemo = value
            End Set
        End Property

        ''' <summary>採用年月日</summary>
        ''' <value>採用年月日</value>
        ''' <returns>採用年月日</returns>
        Public Property SaiyoDate() As Nullable(Of Int32)
            Get
                Return _SaiyoDate
            End Get
            Set(ByVal value As Nullable(Of Int32))
                _SaiyoDate = value
            End Set
        End Property

        ''' <summary>廃止年月日</summary>
        ''' <value>廃止年月日</value>
        ''' <returns>廃止年月日</returns>
        Public Property HaisiDate() As Nullable(Of Int32)
            Get
                Return _HaisiDate
            End Get
            Set(ByVal value As Nullable(Of Int32))
                _HaisiDate = value
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