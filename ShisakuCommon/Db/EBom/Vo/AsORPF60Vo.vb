Namespace Db.EBom.Vo
    ''' <summary>
    ''' 新調達手配進捗情報
    ''' </summary>
    ''' <remarks></remarks>
    Public Class AsORPF60Vo

        ''' <summary>作業依頼書№</summary>
        Private _Sgisba As String
        ''' <summary>管理№</summary>
        Private _Kbba As String
        ''' <summary>注文書№</summary>
        Private _Cmba As String
        ''' <summary>納入区分</summary>
        Private _Nokm As String
        ''' <summary>発注年月日</summary>
        Private _Haym As Nullable(Of Int32)
        ''' <summary>部品区分</summary>
        Private _Bukbn As String
        ''' <summary>納入方法（同期）</summary>
        Private _Doki As String
        ''' <summary>納入方法（分納）</summary>
        Private _Bunno As String
        ''' <summary>進捗結果（ネック）</summary>
        Private _Neck As String
        ''' <summary>進捗結果（暫・欠）</summary>
        Private _Zantei As String
        ''' <summary>進捗結果（その他）</summary>
        Private _Other As String
        ''' <summary>型</summary>
        Private _Kata As String
        ''' <summary>工法</summary>
        Private _Koho As String
        ''' <summary>見積り型費（円）</summary>
        Private _Katahi As Nullable(Of Int32)
        ''' <summary>見積り型費（千円）</summary>
        Private _Katahis As Nullable(Of Int32)
        ''' <summary>見積り部品費（円）</summary>
        Private _Buhinhi As Nullable(Of Int32)
        ''' <summary>備考</summary>
        Private _Newbiko As String
        ''' <summary>設通№（最新）</summary>
        Private _Nwstba As String
        ''' <summary>設通№（実績）</summary>
        Private _Jistba As String
        ''' <summary>出図予定日（最新）</summary>
        Private _Nwyozp As String
        ''' <summary>出図実績日</summary>
        Private _Jizpbi As String
        ''' <summary>手番</summary>
        Private _Teban As Nullable(Of Int32)
        ''' <summary>日付１</summary>
        Private _Day1 As Nullable(Of Int32)
        ''' <summary>日付２</summary>
        Private _Day2 As Nullable(Of Int32)
        ''' <summary>備考１</summary>
        Private _Biko1 As String
        ''' <summary>備考２</summary>
        Private _Biko2 As String
        ''' <summary>納期回答１</summary>
        Private _Nqans1 As Nullable(Of Int32)
        ''' <summary>予定数１</summary>
        Private _Ytca1 As Nullable(Of Int32)
        ''' <summary>納期回答２</summary>
        Private _Nqans2 As Nullable(Of Int32)
        ''' <summary>予定数２</summary>
        Private _Ytca2 As Nullable(Of Int32)
        ''' <summary>納期回答３</summary>
        Private _Nqans3 As Nullable(Of Int32)
        ''' <summary>予定数３</summary>
        Private _Ytca3 As Nullable(Of Int32)
        ''' <summary>納期回答４</summary>
        Private _Nqans4 As Nullable(Of Int32)
        ''' <summary>予定数４</summary>
        Private _Ytca4 As Nullable(Of Int32)
        ''' <summary>納期回答５</summary>
        Private _Nqans5 As Nullable(Of Int32)
        ''' <summary>予定数５</summary>
        Private _Ytca5 As Nullable(Of Int32)
        ''' <summary>納期回答６</summary>
        Private _Nqans6 As Nullable(Of Int32)
        ''' <summary>予定数６</summary>
        Private _Ytca6 As Nullable(Of Int32)
        ''' <summary>納期回答７</summary>
        Private _Nqans7 As Nullable(Of Int32)
        ''' <summary>予定数７</summary>
        Private _Ytca7 As Nullable(Of Int32)
        ''' <summary>納期回答８</summary>
        Private _Nqans8 As Nullable(Of Int32)
        ''' <summary>予定数８</summary>
        Private _Ytca8 As Nullable(Of Int32)
        ''' <summary>処置</summary>
        Private _Syoti As String
        ''' <summary>理由</summary>
        Private _Riyu As String
        ''' <summary>対応</summary>
        Private _Taio As String
        ''' <summary>部署</summary>
        Private _Busho As String
        ''' <summary>設計担当者</summary>
        Private _Tanto As String
        ''' <summary>ＴＥＬ</summary>
        Private _Tel As Nullable(Of Int32)
        ''' <summary>暫定品納入日</summary>
        Private _Zanonyu As Nullable(Of Int32)
        ''' <summary>正規扱OR後交換有</summary>
        Private _Seiki As String
        ''' <summary>作成ユーザーID</summary>
        Private _CreatedUserId As String
        ''' <summary>作成日</summary>
        Private _CreatedDate As String
        ''' <summary>作成時間</summary>
        Private _CreatedTime As String
        ''' <summary>更新ユーザーID</summary>
        Private _UpdatedUserId As String
        ''' <summary>更新日</summary>
        Private _UpdatedDate As String
        ''' <summary>更新時間</summary>
        Private _UpdatedTime As String


        ''' <summary>作業依頼書№</summary>
        ''' <value>作業依頼書№</value>
        ''' <returns>作業依頼書№</returns>
        Public Property Sgisba() As String
            Get
                Return _Sgisba
            End Get
            Set(ByVal value As String)
                _Sgisba = value
            End Set
        End Property

        ''' <summary>管理№</summary>
        ''' <value>管理№</value>
        ''' <returns>管理№</returns>
        Public Property Kbba() As String
            Get
                Return _Kbba
            End Get
            Set(ByVal value As String)
                _Kbba = value
            End Set
        End Property

        ''' <summary>注文書№</summary>
        ''' <value>注文書№</value>
        ''' <returns>注文書№</returns>
        Public Property Cmba() As String
            Get
                Return _Cmba
            End Get
            Set(ByVal value As String)
                _Cmba = value
            End Set
        End Property

        ''' <summary>納入区分</summary>
        ''' <value>納入区分</value>
        ''' <returns>納入区分</returns>
        Public Property Nokm() As String
            Get
                Return _Nokm
            End Get
            Set(ByVal value As String)
                _Nokm = value
            End Set
        End Property

        ''' <summary>発注年月日</summary>
        ''' <value>発注年月日</value>
        ''' <returns>発注年月日</returns>
        Public Property Haym() As Nullable(Of Int32)
            Get
                Return _Haym
            End Get
            Set(ByVal value As Nullable(Of Int32))
                _Haym = value
            End Set
        End Property

        ''' <summary>部品区分</summary>
        ''' <value>部品区分</value>
        ''' <returns>部品区分</returns>
        Public Property Bukbn() As String
            Get
                Return _Bukbn
            End Get
            Set(ByVal value As String)
                _Bukbn = value
            End Set
        End Property

        ''' <summary>納入方法（同期）</summary>
        ''' <value>納入方法（同期）</value>
        ''' <returns>納入方法（同期）</returns>
        Public Property Doki() As String
            Get
                Return _Doki
            End Get
            Set(ByVal value As String)
                _Doki = value
            End Set
        End Property

        ''' <summary>納入方法（分納）</summary>
        ''' <value>納入方法（分納）</value>
        ''' <returns>納入方法（分納）</returns>
        Public Property Bunno() As String
            Get
                Return _Bunno
            End Get
            Set(ByVal value As String)
                _Bunno = value
            End Set
        End Property

        ''' <summary>進捗結果（ネック）</summary>
        ''' <value>進捗結果（ネック）</value>
        ''' <returns>進捗結果（ネック）</returns>
        Public Property Neck() As String
            Get
                Return _Neck
            End Get
            Set(ByVal value As String)
                _Neck = value
            End Set
        End Property

        ''' <summary>進捗結果（暫・欠）</summary>
        ''' <value>進捗結果（暫・欠）</value>
        ''' <returns>進捗結果（暫・欠）</returns>
        Public Property Zantei() As String
            Get
                Return _Zantei
            End Get
            Set(ByVal value As String)
                _Zantei = value
            End Set
        End Property

        ''' <summary>進捗結果（その他）</summary>
        ''' <value>進捗結果（その他）</value>
        ''' <returns>進捗結果（その他）</returns>
        Public Property Other() As String
            Get
                Return _Other
            End Get
            Set(ByVal value As String)
                _Other = value
            End Set
        End Property

        ''' <summary>型</summary>
        ''' <value>型</value>
        ''' <returns>型</returns>
        Public Property Kata() As String
            Get
                Return _Kata
            End Get
            Set(ByVal value As String)
                _Kata = value
            End Set
        End Property

        ''' <summary>工法</summary>
        ''' <value>工法</value>
        ''' <returns>工法</returns>
        Public Property Koho() As String
            Get
                Return _Koho
            End Get
            Set(ByVal value As String)
                _KOHO = value
            End Set
        End Property

        ''' <summary>見積り型費（円）</summary>
        ''' <value>見積り型費（円）</value>
        ''' <returns>見積り型費（円）</returns>
        Public Property Katahi() As Nullable(Of Int32)
            Get
                Return _Katahi
            End Get
            Set(ByVal value As Nullable(Of Int32))
                _Katahi = value
            End Set
        End Property

        ''' <summary>見積り型費（千円）</summary>
        ''' <value>見積り型費（千円）</value>
        ''' <returns>見積り型費（千円）</returns>
        Public Property Katahis() As Nullable(Of Int32)
            Get
                Return _Katahis
            End Get
            Set(ByVal value As Nullable(Of Int32))
                _Katahis = value
            End Set
        End Property

        ''' <summary>見積り部品費（円）</summary>
        ''' <value>見積り部品費（円）</value>
        ''' <returns>見積り部品費（円）</returns>
        Public Property Buhinhi() As Nullable(Of Int32)
            Get
                Return _Buhinhi
            End Get
            Set(ByVal value As Nullable(Of Int32))
                _Buhinhi = value
            End Set
        End Property

        ''' <summary>備考</summary>
        ''' <value>備考</value>
        ''' <returns>備考</returns>
        Public Property Newbiko() As String
            Get
                Return _Newbiko
            End Get
            Set(ByVal value As String)
                _Newbiko = value
            End Set
        End Property

        ''' <summary>設通№（最新）</summary>
        ''' <value>設通№（最新）</value>
        ''' <returns>設通№（最新）</returns>
        Public Property Nwstba() As String
            Get
                Return _Nwstba
            End Get
            Set(ByVal value As String)
                _Nwstba = value
            End Set
        End Property

        ''' <summary>設通№（実績）</summary>
        ''' <value>設通№（実績）</value>
        ''' <returns>設通№（実績）</returns>
        Public Property Jistba() As String
            Get
                Return _Jistba
            End Get
            Set(ByVal value As String)
                _Jistba = value
            End Set
        End Property

        ''' <summary>出図予定日（最新）</summary>
        ''' <value>出図予定日（最新）</value>
        ''' <returns>出図予定日（最新）</returns>
        Public Property Nwyozp() As String
            Get
                Return _Nwyozp
            End Get
            Set(ByVal value As String)
                _Nwyozp = value
            End Set
        End Property

        ''' <summary>出図実績日</summary>
        ''' <value>出図実績日</value>
        ''' <returns>出図実績日</returns>
        Public Property Jizpbi() As String
            Get
                Return _Jizpbi
            End Get
            Set(ByVal value As String)
                _Jizpbi = value
            End Set
        End Property

        ''' <summary>手番</summary>
        ''' <value>手番</value>
        ''' <returns>手番</returns>
        Public Property Teban() As Nullable(Of Int32)
            Get
                Return _Teban
            End Get
            Set(ByVal value As Nullable(Of Int32))
                _Teban = value
            End Set
        End Property

        ''' <summary>日付１</summary>
        ''' <value>日付１</value>
        ''' <returns>日付１</returns>
        Public Property Day1() As Nullable(Of Int32)
            Get
                Return _Day1
            End Get
            Set(ByVal value As Nullable(Of Int32))
                _Day1 = value
            End Set
        End Property

        ''' <summary>日付２</summary>
        ''' <value>日付２</value>
        ''' <returns>日付２</returns>
        Public Property Day2() As Nullable(Of Int32)
            Get
                Return _Day2
            End Get
            Set(ByVal value As Nullable(Of Int32))
                _Day2 = value
            End Set
        End Property

        ''' <summary>備考１</summary>
        ''' <value>備考１</value>
        ''' <returns>備考１</returns>
        Public Property Biko1() As String
            Get
                Return _Biko1
            End Get
            Set(ByVal value As String)
                _Biko1 = value
            End Set
        End Property

        ''' <summary>備考２</summary>
        ''' <value>備考２</value>
        ''' <returns>備考２</returns>
        Public Property Biko2() As String
            Get
                Return _Biko2
            End Get
            Set(ByVal value As String)
                _Biko2 = value
            End Set
        End Property

        ''' <summary>納期回答１</summary>
        ''' <value>納期回答１</value>
        ''' <returns>納期回答１</returns>
        Public Property Nqans1() As Nullable(Of Int32)
            Get
                Return _Nqans1
            End Get
            Set(ByVal value As Nullable(Of Int32))
                _NQANS1 = value
            End Set
        End Property

        ''' <summary>予定数１</summary>
        ''' <value>予定数１</value>
        ''' <returns>予定数１</returns>
        Public Property Ytca1() As Nullable(Of Int32)
            Get
                Return _Ytca1
            End Get
            Set(ByVal value As Nullable(Of Int32))
                _Ytca1 = value
            End Set
        End Property

        ''' <summary>納期回答２</summary>
        ''' <value>納期回答２</value>
        ''' <returns>納期回答２</returns>
        Public Property Nqans2() As Nullable(Of Int32)
            Get
                Return _Nqans2
            End Get
            Set(ByVal value As Nullable(Of Int32))
                _Nqans2 = value
            End Set
        End Property

        ''' <summary>予定数２</summary>
        ''' <value>予定数２</value>
        ''' <returns>予定数２</returns>
        Public Property Ytca2() As Nullable(Of Int32)
            Get
                Return _Ytca2
            End Get
            Set(ByVal value As Nullable(Of Int32))
                _Ytca2 = value
            End Set
        End Property

        ''' <summary>納期回答３</summary>
        ''' <value>納期回答３</value>
        ''' <returns>納期回答３</returns>
        Public Property Nqans3() As Nullable(Of Int32)
            Get
                Return _Nqans3
            End Get
            Set(ByVal value As Nullable(Of Int32))
                _Nqans3 = value
            End Set
        End Property

        ''' <summary>予定数３</summary>
        ''' <value>予定数３</value>
        ''' <returns>予定数３</returns>
        Public Property Ytca3() As Nullable(Of Int32)
            Get
                Return _Ytca3
            End Get
            Set(ByVal value As Nullable(Of Int32))
                _Ytca3 = value
            End Set
        End Property

        ''' <summary>納期回答４</summary>
        ''' <value>納期回答４</value>
        ''' <returns>納期回答４</returns>
        Public Property Nqans4() As Nullable(Of Int32)
            Get
                Return _Nqans4
            End Get
            Set(ByVal value As Nullable(Of Int32))
                _Nqans4 = value
            End Set
        End Property

        ''' <summary>予定数４</summary>
        ''' <value>予定数４</value>
        ''' <returns>予定数４</returns>
        Public Property Ytca4() As Nullable(Of Int32)
            Get
                Return _Ytca4
            End Get
            Set(ByVal value As Nullable(Of Int32))
                _Ytca4 = value
            End Set
        End Property

        ''' <summary>納期回答５</summary>
        ''' <value>納期回答５</value>
        ''' <returns>納期回答５</returns>
        Public Property Nqans5() As Nullable(Of Int32)
            Get
                Return _Nqans5
            End Get
            Set(ByVal value As Nullable(Of Int32))
                _Nqans5 = value
            End Set
        End Property

        ''' <summary>予定数５</summary>
        ''' <value>予定数５</value>
        ''' <returns>予定数５</returns>
        Public Property Ytca5() As Nullable(Of Int32)
            Get
                Return _Ytca5
            End Get
            Set(ByVal value As Nullable(Of Int32))
                _Ytca5 = value
            End Set
        End Property

        ''' <summary>納期回答６</summary>
        ''' <value>納期回答６</value>
        ''' <returns>納期回答６</returns>
        Public Property Nqans6() As Nullable(Of Int32)
            Get
                Return _Nqans6
            End Get
            Set(ByVal value As Nullable(Of Int32))
                _Nqans6 = value
            End Set
        End Property

        ''' <summary>予定数６</summary>
        ''' <value>予定数６</value>
        ''' <returns>予定数６</returns>
        Public Property Ytca6() As Nullable(Of Int32)
            Get
                Return _Ytca6
            End Get
            Set(ByVal value As Nullable(Of Int32))
                _Ytca6 = value
            End Set
        End Property

        ''' <summary>納期回答７</summary>
        ''' <value>納期回答７</value>
        ''' <returns>納期回答７</returns>
        Public Property Nqans7() As Nullable(Of Int32)
            Get
                Return _Nqans7
            End Get
            Set(ByVal value As Nullable(Of Int32))
                _Nqans7 = value
            End Set
        End Property

        ''' <summary>予定数７</summary>
        ''' <value>予定数７</value>
        ''' <returns>予定数７</returns>
        Public Property Ytca7() As Nullable(Of Int32)
            Get
                Return _Ytca7
            End Get
            Set(ByVal value As Nullable(Of Int32))
                _Ytca7 = value
            End Set
        End Property

        ''' <summary>納期回答８</summary>
        ''' <value>納期回答８</value>
        ''' <returns>納期回答８</returns>
        Public Property Nqans8() As Nullable(Of Int32)
            Get
                Return _Nqans8
            End Get
            Set(ByVal value As Nullable(Of Int32))
                _Nqans8 = value
            End Set
        End Property

        ''' <summary>予定数８</summary>
        ''' <value>予定数８</value>
        ''' <returns>予定数８</returns>
        Public Property Ytca8() As Nullable(Of Int32)
            Get
                Return _Ytca8
            End Get
            Set(ByVal value As Nullable(Of Int32))
                _Ytca8 = value
            End Set
        End Property

        ''' <summary>処置</summary>
        ''' <value>処置</value>
        ''' <returns>処置</returns> 
        Public Property Syoti() As String
            Get
                Return _Syoti
            End Get
            Set(ByVal value As String)
                _Syoti = value
            End Set
        End Property

        ''' <summary>理由</summary>
        ''' <value>理由</value>
        ''' <returns>理由</returns>
        Public Property Riyu() As String
            Get
                Return _Riyu
            End Get
            Set(ByVal value As String)
                _Riyu = value
            End Set
        End Property

        ''' <summary>対応</summary>
        ''' <value>対応</value>
        ''' <returns>対応</returns>
        Public Property Taio() As String
            Get
                Return _Taio
            End Get
            Set(ByVal value As String)
                _Taio = value
            End Set
        End Property

        ''' <summary>部署</summary>
        ''' <value>部署</value>
        ''' <returns>部署</returns>
        Public Property Busho() As String
            Get
                Return _Busho
            End Get
            Set(ByVal value As String)
                _Busho = value
            End Set
        End Property

        ''' <summary>設計担当者</summary>
        ''' <value>設計担当者</value>
        ''' <returns>設計担当者</returns>
        Public Property Tanto() As String
            Get
                Return _Tanto
            End Get
            Set(ByVal value As String)
                _Tanto = value
            End Set
        End Property

        ''' <summary>ＴＥＬ</summary>
        ''' <value>ＴＥＬ</value>
        ''' <returns>ＴＥＬ</returns>
        Public Property Tel() As Nullable(Of Int32)
            Get
                Return _Tel
            End Get
            Set(ByVal value As Nullable(Of Int32))
                _Tel = value
            End Set
        End Property

        ''' <summary>暫定品納入日</summary>
        ''' <value>暫定品納入日</value>
        ''' <returns>暫定品納入日</returns>
        Public Property Zanonyu() As Nullable(Of Int32)
            Get
                Return _Zanonyu
            End Get
            Set(ByVal value As Nullable(Of Int32))
                _Zanonyu = value
            End Set
        End Property

        ''' <summary>正規扱OR後交換有</summary>
        ''' <value>正規扱OR後交換有</value>
        ''' <returns>正規扱OR後交換有</returns>
        Public Property Seiki() As String
            Get
                Return _Seiki
            End Get
            Set(ByVal value As String)
                _Seiki = value
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