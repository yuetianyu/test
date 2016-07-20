Namespace Db.EBom.Vo
    ''' <summary>
    ''' 図面(FM5以降)
    ''' </summary>
    ''' <remarks></remarks>
    Public Class Rhac0703Vo

        '' 図面番号
        Private _ZumenNo As String
        '' 図面改訂No
        Private _ZumenKaiteiNo As String
        '' 開発符号
        Private _KaihatsuFugo As String
        '' ブロックNo機能
        Private _BlockNoKino As String
        '' 部品かな名称
        Private _BuhinKanaName As String
        '' 関連資料No
        Private _KanrenShiryoNo As String
        '' 正規外配布部
        Private _SeikigaiHaifuBu As String
        '' 生産区分
        Private _SeisanKbn As String
        '' 図面サイズ
        Private _ZumenSize As String
        '' 図面コード
        Private _ZumenCode As String
        '' 授与保安区分
        Private _JuyoHoanKbn As String
        '' 授与保安コード
        Private _JuyoHoanCode As String
        '' 保養品コード
        Private _HoyohinCode As String
        '' リサイクルマーク
        Private _RecycleMark As String
        '' 通知書シリーズ
        Private _TsuchishoSiries As String
        '' 通知書No1
        Private _TsuchishoNo1 As String
        '' 通知書No2
        Private _TsuchishoNo2 As String
        '' 表面処理
        Private _HyomenShori As String
        '' 変更理由コード
        Private _HenkoRiyuCode As String
        '' インターチェンジアビリティ
        Private _InterchangeAblity As String
        '' コンディション
        Private _Condition As String
        '' シリーズ区分
        Private _SiriesKbn As String
        '' 適用メモ
        Private _TekiyoMemo As String
        '' データランク
        Private _DataRank As String
        '' 補規制コード
        Private _HokiseiCode As String
        '' ASSYDWGNo1
        Private _AssyDwgNo1 As String
        '' ASSYDWGNo2
        Private _AssyDwgNo2 As String
        '' ASSYDWGNo3
        Private _AssyDwgNo3 As String
        '' ASSYDWGNo4
        Private _AssyDwgNo4 As String
        '' 作成社員番号
        Private _SakuseiShainNo As String
        '' 出図社員番号
        Private _ShutuzuShainNo As String
        '' 電話番号
        Private _Tel As String
        '' 担当パーツ日
        Private _TantoPartsDate As Nullable(Of Int32)
        '' 出図予定日
        Private _ShutuzuYoteiDate As Nullable(Of Int32)
        '' 採用日
        Private _SaiyoDate As Nullable(Of Int32)
        '' 廃止日
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

        ''' <summary>図面改訂No</summary>
        ''' <value>図面改訂No</value>
        ''' <returns>図面改訂No</returns>
        Public Property ZumenKaiteiNo() As String
            Get
                Return _ZumenKaiteiNo
            End Get
            Set(ByVal value As String)
                _ZumenKaiteiNo = value
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

        ''' <summary>ブロックNo機能</summary>
        ''' <value>ブロックNo機能</value>
        ''' <returns>ブロックNo機能</returns>
        Public Property BlockNoKino() As String
            Get
                Return _BlockNoKino
            End Get
            Set(ByVal value As String)
                _BlockNoKino = value
            End Set
        End Property

        ''' <summary>部品かな名称</summary>
        ''' <value>部品かな名称</value>
        ''' <returns>部品かな名称</returns>
        Public Property BuhinKanaName() As String
            Get
                Return _BuhinKanaName
            End Get
            Set(ByVal value As String)
                _BuhinKanaName = value
            End Set
        End Property

        ''' <summary>関連資料番号</summary>
        ''' <value>関連資料番号</value>
        ''' <returns>関連資料番号</returns>
        Public Property KanrenShiryoNo() As String
            Get
                Return _KanrenShiryoNo
            End Get
            Set(ByVal value As String)
                _KanrenShiryoNo = value
            End Set
        End Property

        ''' <summary>正規外配布部</summary>
        ''' <value>正規外配布部</value>
        ''' <returns>正規外配布部</returns>
        Public Property SeikigaiHaifuBu() As String
            Get
                Return _SeikigaiHaifuBu
            End Get
            Set(ByVal value As String)
                _SeikigaiHaifuBu = value
            End Set
        End Property

        ''' <summary>生産区分</summary>
        ''' <value>生産区分</value>
        ''' <returns>生産区分</returns>
        Public Property SeisanKbn() As String
            Get
                Return _SeisanKbn
            End Get
            Set(ByVal value As String)
                _SeisanKbn = value
            End Set
        End Property

        ''' <summary>図面サイズ</summary>
        ''' <value>図面サイズ</value>
        ''' <returns>図面サイズ</returns>
        Public Property ZumenSize() As String
            Get
                Return _ZumenSize
            End Get
            Set(ByVal value As String)
                _ZumenSize = value
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

        ''' <summary>授与保安区分</summary>
        ''' <value>授与保安区分</value>
        ''' <returns>授与保安区分</returns>
        Public Property JuyoHoanKbn() As String
            Get
                Return _JuyoHoanKbn
            End Get
            Set(ByVal value As String)
                _JuyoHoanKbn = value
            End Set
        End Property

        ''' <summary>授与保安コード</summary>
        ''' <value>授与保安コード</value>
        ''' <returns>授与保安コード</returns>
        Public Property JuyoHoanCode() As String
            Get
                Return _JuyoHoanCode
            End Get
            Set(ByVal value As String)
                _JuyoHoanCode = value
            End Set
        End Property

        ''' <summary>保養品コード</summary>
        ''' <value>保養品コード</value>
        ''' <returns>保養品コード</returns>
        Public Property HoyohinCode() As String
            Get
                Return _HoyohinCode
            End Get
            Set(ByVal value As String)
                _HoyohinCode = value
            End Set
        End Property

        ''' <summary>リサイクルマーク</summary>
        ''' <value>リサイクルマーク</value>
        ''' <returns>リサイクルマーク</returns>
        Public Property RecycleMark() As String
            Get
                Return _RecycleMark
            End Get
            Set(ByVal value As String)
                _RecycleMark = value
            End Set
        End Property

        ''' <summary>通知書シリーズ</summary>
        ''' <value>通知書シリーズ</value>
        ''' <returns>通知書シリーズ</returns>
        Public Property TsuchishoSiries() As String
            Get
                Return _TsuchishoSiries
            End Get
            Set(ByVal value As String)
                _TsuchishoSiries = value
            End Set
        End Property

        ''' <summary>通知書No1</summary>
        ''' <value>通知書No1</value>
        ''' <returns>通知書No1</returns>
        Public Property TsuchishoNo1() As String
            Get
                Return _TsuchishoNo1
            End Get
            Set(ByVal value As String)
                _TsuchishoNo1 = value
            End Set
        End Property

        ''' <summary>通知書No2</summary>
        ''' <value>通知書No2</value>
        ''' <returns>通知書No2</returns>
        Public Property TsuchishoNo2() As String
            Get
                Return _TsuchishoNo2
            End Get
            Set(ByVal value As String)
                _TsuchishoNo2 = value
            End Set
        End Property

        ''' <summary>表面処理</summary>
        ''' <value>表面処理</value>
        ''' <returns>表面処理</returns>
        Public Property HyomenShori() As String
            Get
                Return _HyomenShori
            End Get
            Set(ByVal value As String)
                _HyomenShori = value
            End Set
        End Property

        ''' <summary>変更理由コード</summary>
        ''' <value>変更理由コード</value>
        ''' <returns>変更理由コード</returns>
        Public Property HenkoRiyuCode() As String
            Get
                Return _HenkoRiyuCode
            End Get
            Set(ByVal value As String)
                _HenkoRiyuCode = value
            End Set
        End Property

        ''' <summary>インターチェンジアビリティ</summary>
        ''' <value>インターチェンジアビリティ</value>
        ''' <returns>インターチェンジアビリティ</returns>
        Public Property InterchangeAblity() As String
            Get
                Return _InterchangeAblity
            End Get
            Set(ByVal value As String)
                _InterchangeAblity = value
            End Set
        End Property

        ''' <summary>コンディション</summary>
        ''' <value>コンディション</value>
        ''' <returns>コンディション</returns>
        Public Property Condition() As String
            Get
                Return _Condition
            End Get
            Set(ByVal value As String)
                _Condition = value
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

        ''' <summary>データランク</summary>
        ''' <value>データランク</value>
        ''' <returns>データランク</returns>
        Public Property DataRank() As String
            Get
                Return _DataRank
            End Get
            Set(ByVal value As String)
                _DataRank = value
            End Set
        End Property

        ''' <summary>法規制コード</summary>
        ''' <value>法規制コード</value>
        ''' <returns>法規制コード</returns>
        Public Property HokiseiCode() As String
            Get
                Return _HokiseiCode
            End Get
            Set(ByVal value As String)
                _HokiseiCode = value
            End Set
        End Property

        ''' <summary>ASSYDWGNo1</summary>
        ''' <value>ASSYDWGNo1</value>
        ''' <returns>ASSYDWGNo1</returns>
        Public Property AssyDwgNo1() As String
            Get
                Return _AssyDwgNo1
            End Get
            Set(ByVal value As String)
                _AssyDwgNo1 = value
            End Set
        End Property

        ''' <summary>ASSYDWGNo2</summary>
        ''' <value>ASSYDWGNo2</value>
        ''' <returns>ASSYDWGNo2</returns>
        Public Property AssyDwgNo2() As String
            Get
                Return _AssyDwgNo2
            End Get
            Set(ByVal value As String)
                _AssyDwgNo2 = value
            End Set
        End Property

        ''' <summary>ASSYDWGNo3</summary>
        ''' <value>ASSYDWGNo3</value>
        ''' <returns>ASSYDWGNo3</returns>
        Public Property AssyDwgNo3() As String
            Get
                Return _AssyDwgNo3
            End Get
            Set(ByVal value As String)
                _AssyDwgNo3 = value
            End Set
        End Property

        ''' <summary>ASSYDWGNo4</summary>
        ''' <value>ASSYDWGNo4</value>
        ''' <returns>ASSYDWGNo4</returns>
        Public Property AssyDwgNo4() As String
            Get
                Return _AssyDwgNo4
            End Get
            Set(ByVal value As String)
                _AssyDwgNo4 = value
            End Set
        End Property

        ''' <summary>作成社員No</summary>
        ''' <value>作成社員No</value>
        ''' <returns>作成社員No</returns>
        Public Property SakuseiShainNo() As String
            Get
                Return _SakuseiShainNo
            End Get
            Set(ByVal value As String)
                _SakuseiShainNo = value
            End Set
        End Property

        ''' <summary>出図社員No</summary>
        ''' <value>出図社員No</value>
        ''' <returns>出図社員No</returns>
        Public Property ShutuzuShainNo() As String
            Get
                Return _ShutuzuShainNo
            End Get
            Set(ByVal value As String)
                _ShutuzuShainNo = value
            End Set
        End Property

        ''' <summary>電話番号</summary>
        ''' <value>電話番号</value>
        ''' <returns>電話番号</returns>
        Public Property Tel() As String
            Get
                Return _Tel
            End Get
            Set(ByVal value As String)
                _Tel = value
            End Set
        End Property

        ''' <summary>担当パーツ日</summary>
        ''' <value>担当パーツ日</value>
        ''' <returns>担当パーツ日</returns>
        Public Property TantoPartsDate() As Nullable(Of Int32)
            Get
                Return _TantoPartsDate
            End Get
            Set(ByVal value As Nullable(Of Int32))
                _TantoPartsDate = value
            End Set
        End Property

        ''' <summary>出図予定日</summary>
        ''' <value>出図予定日</value>
        ''' <returns>出図予定日</returns>
        Public Property ShutuzuYoteiDate() As Nullable(Of Int32)
            Get
                Return _ShutuzuYoteiDate
            End Get
            Set(ByVal value As Nullable(Of Int32))
                _ShutuzuYoteiDate = value
            End Set
        End Property

        ''' <summary>採用日</summary>
        ''' <value>採用日</value>
        ''' <returns>採用日</returns>
        Public Property SaiyoDate() As Nullable(Of Int32)
            Get
                Return _SaiyoDate
            End Get
            Set(ByVal value As Nullable(Of Int32))
                _SaiyoDate = value
            End Set
        End Property

        ''' <summary>廃止日</summary>
        ''' <value>廃止日</value>
        ''' <returns>廃止日</returns>
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