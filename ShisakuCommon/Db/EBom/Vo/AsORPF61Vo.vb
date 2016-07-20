Namespace Db.EBom.Vo
    ''' <summary>
    ''' 現調品手配進捗情報
    ''' </summary>
    ''' <remarks></remarks>
    Public Class AsORPF61Vo

        ''' <summary>グループ№</summary>
        Private _Grno As String
        ''' <summary>シリアル№</summary>
        Private _Srno As String
        ''' <summary>部品区分</summary>
        Private _Bukbn As String
        ''' <summary>進捗結果（ネック）</summary>
        Private _Neck As String
        ''' <summary>進捗結果（暫･欠）</summary>
        Private _Zantei As String
        ''' <summary>進捗結果（その他）</summary>
        Private _Other As String
        ''' <summary>設通№（出荷予定）</summary>
        Private _Stbash As String
        ''' <summary>設通№（出荷実績）</summary>
        Private _Stbaji As String
        ''' <summary>輸送会社（現地）</summary>
        Private _Yusosia As String
        ''' <summary>トラッキング№</summary>
        Private _Trkba As String
        ''' <summary>回答年月日</summary>
        Private _Ansymd As Nullable(Of Int32)
        ''' <summary>遅延理由</summary>
        Private _Tien As String
        ''' <summary>受入日</summary>
        Private _Ukeday As Nullable(Of Int32)
        ''' <summary>受入数</summary>
        Private _Ukesuu As Nullable(Of Int32)
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
        ''' <summary>正規扱ＯＲ後交換有</summary>
        Private _Seiki As String
        ''' <summary>備考</summary>
        Private _Newbiko As String
        ''' <summary>出図予定日（最新）</summary>
        Private _Nwyozp As String
        ''' <summary>出図実績日</summary>
        Private _Jizpbi As String
        ''' <summary>日付１</summary>
        Private _Day1 As Nullable(Of Int32)
        ''' <summary>日付２</summary>
        Private _Day2 As Nullable(Of Int32)
        ''' <summary>備考１</summary>
        Private _Biko1 As String
        ''' <summary>備考２</summary>
        Private _Biko2 As String
        ''' <summary>輸送会社</summary>
        Private _Yusonm As String
        ''' <summary>出荷輸送手段</summary>
        Private _Yushcd As String
        ''' <summary>出荷指示備考</summary>
        Private _Ssbiko As String
        ''' <summary>WAY BILL №</summary>
        Private _Wayno As String
        ''' <summary>インボイス№</summary>
        Private _Invno As String
        ''' <summary>配達輸送手段</summary>
        Private _Shjit As String
        ''' <summary>ケース№</summary>
        Private _Caseno As String
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


        ''' <summary>グループ№</summary>
        ''' <value>グループ№</value>
        ''' <returns>グループ№</returns>
        Public Property Grno() As String
            Get
                Return _Grno
            End Get
            Set(ByVal value As String)
                _Grno = value
            End Set
        End Property

        ''' <summary>シリアル№</summary>
        ''' <value>シリアル№</value>
        ''' <returns>シリアル№</returns>
        Public Property Srno() As String
            Get
                Return _Srno
            End Get
            Set(ByVal value As String)
                _Srno = value
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

        ''' <summary>進捗結果（暫･欠）</summary>
        ''' <value>進捗結果（暫･欠）</value>
        ''' <returns>進捗結果（暫･欠）</returns>
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

        ''' <summary>設通№（出荷予定）</summary>
        ''' <value>設通№（出荷予定）</value>
        ''' <returns>設通№（出荷予定）</returns>
        Public Property Stbash() As String
            Get
                Return _Stbash
            End Get
            Set(ByVal value As String)
                _Stbash = value
            End Set
        End Property

        ''' <summary>設通№（出荷実績）</summary>
        ''' <value>設通№（出荷実績）</value>
        ''' <returns>設通№（出荷実績）</returns>
        Public Property Stbaji() As String
            Get
                Return _Stbaji
            End Get
            Set(ByVal value As String)
                _Stbaji = value
            End Set
        End Property

        ''' <summary>輸送会社（現地）</summary>
        ''' <value>輸送会社（現地）</value>
        ''' <returns>輸送会社（現地）</returns>
        Public Property Yusosia() As String
            Get
                Return _Yusosia
            End Get
            Set(ByVal value As String)
                _Yusosia = value
            End Set
        End Property

        ''' <summary>トラッキング№</summary>
        ''' <value>トラッキング№</value>
        ''' <returns>トラッキング№</returns>
        Public Property Trkba() As String
            Get
                Return _Trkba
            End Get
            Set(ByVal value As String)
                _Trkba = value
            End Set
        End Property

        ''' <summary>回答年月日</summary>
        ''' <value>回答年月日</value>
        ''' <returns>回答年月日</returns>
        Public Property Ansymd() As Nullable(Of Int32)
            Get
                Return _Ansymd
            End Get
            Set(ByVal value As Nullable(Of Int32))
                _Ansymd = value
            End Set
        End Property

        ''' <summary>遅延理由</summary>
        ''' <value>遅延理由</value>
        ''' <returns>遅延理由</returns>
        Public Property Tien() As String
            Get
                Return _Tien
            End Get
            Set(ByVal value As String)
                _Tien = value
            End Set
        End Property

        ''' <summary>受入日</summary>
        ''' <value>受入日</value>
        ''' <returns>受入日</returns>
        Public Property Ukeday() As Nullable(Of Int32)
            Get
                Return _Ukeday
            End Get
            Set(ByVal value As Nullable(Of Int32))
                _Ukeday = value
            End Set
        End Property

        ''' <summary>受入数</summary>
        ''' <value>受入数</value>
        ''' <returns>受入数</returns>
        Public Property Ukesuu() As Nullable(Of Int32)
            Get
                Return _Ukesuu
            End Get
            Set(ByVal value As Nullable(Of Int32))
                _Ukesuu = value
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

        ''' <summary>正規扱ＯＲ後交換有</summary>
        ''' <value>正規扱ＯＲ後交換有</value>
        ''' <returns>正規扱ＯＲ後交換有</returns>
        Public Property Seiki() As String
            Get
                Return _Seiki
            End Get
            Set(ByVal value As String)
                _Seiki = value
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

        ''' <summary>輸送会社</summary>
        ''' <value>輸送会社</value>
        ''' <returns>輸送会社</returns>
        Public Property Yusonm() As String
            Get
                Return _Yusonm
            End Get
            Set(ByVal value As String)
                _Yusonm = value
            End Set
        End Property

        ''' <summary>出荷輸送手段</summary>
        ''' <value>出荷輸送手段</value>
        ''' <returns>出荷輸送手段</returns>
        Public Property Yushcd() As String
            Get
                Return _Yushcd
            End Get
            Set(ByVal value As String)
                _Yushcd = value
            End Set
        End Property

        ''' <summary>出荷指示備考</summary>
        ''' <value>出荷指示備考</value>
        ''' <returns>出荷指示備考</returns>
        Public Property Ssbiko() As String
            Get
                Return _Ssbiko
            End Get
            Set(ByVal value As String)
                _Ssbiko = value
            End Set
        End Property

        ''' <summary>WAY BILL №</summary>
        ''' <value>WAY BILL №</value>
        ''' <returns>WAY BILL №</returns>
        Public Property Wayno() As String
            Get
                Return _Wayno
            End Get
            Set(ByVal value As String)
                _Wayno = value
            End Set
        End Property

        ''' <summary>インボイス№</summary>
        ''' <value>インボイス№</value>
        ''' <returns>インボイス№</returns>
        Public Property Invno() As String
            Get
                Return _Invno
            End Get
            Set(ByVal value As String)
                _Invno = value
            End Set
        End Property

        ''' <summary>配達輸送手段</summary>
        ''' <value>配達輸送手段</value>
        ''' <returns>配達輸送手段</returns>
        Public Property Shjit() As String
            Get
                Return _Shjit
            End Get
            Set(ByVal value As String)
                _Shjit = value
            End Set
        End Property

        ''' <summary>ケース№</summary>
        ''' <value>ケース№</value>
        ''' <returns>ケース№</returns>
        Public Property Caseno() As String
            Get
                Return _Caseno
            End Get
            Set(ByVal value As String)
                _Caseno = value
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