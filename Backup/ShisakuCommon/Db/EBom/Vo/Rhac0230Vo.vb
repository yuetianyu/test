Namespace Db.EBom.Vo
    ''' <summary>
    ''' 7桁型式
    ''' </summary>
    ''' <remarks></remarks>
    Public Class Rhac0230Vo
        '' 開発符号 
        Private _KaihatsuFugo As String
        '' 装備改訂No. 
        Private _SobiKaiteiNo As String
        '' 7桁型式識別コード 
        Private _KatashikiScd7 As String
        '' 開発符号(認証型式) 
        Private _KaihatsufgNinsho As String
        '' 認証型式符号 
        Private _NinshoKatasiki As String
        '' 7桁型式符号 
        Private _KatashikiFugo7 As String
        '' リストコード 
        Private _ListCode As String
        '' アプライドNo. 
        Private _AppliedNo As Nullable(of Int32)
        '' 表示順 
        Private _HyojijunNo As Nullable(of Int32)
        '' 車系コード 
        Private _ShakeiCode As String
        '' ボディ基本型 
        Private _BodyKihonKata As String
        '' E/G排気量 
        Private _EgHaikiryo As String
        '' 駆動方式 
        Private _KudoHosiki As String
        '' 懸架装置 
        Private _KenkaSochi As String
        '' 動弁系 
        Private _DobenkeiCode As String
        '' 年改符号 
        Private _NenkaiFugo As String
        '' ドア数 
        Private _DoorSu As String
        '' 仕向地(大区分) 
        Private _ShimukeDaiKbn As String
        '' グレードコード 
        Private _GradeCode As String
        '' T/M 
        Private _TransMission As String
        '' 燃料供給システム 
        Private _NenryoKyokyuSys As String
        '' 加給機 
        Private _KakyukiCode As String
        '' ハンドル位置 
        Private _HandlePos As String
        '' 長さ 
        Private _SharyoNagasa As Nullable(of Decimal)
        '' 幅 
        Private _SharyoHaba As Nullable(of Decimal)
        '' 高さ 
        Private _SharyoTakasa As Nullable(of Decimal)
        '' 軸距 
        Private _JikukyoNagasa As Nullable(of Decimal)
        '' 輪距(後輪) 
        Private _RinkyoKorin As Nullable(of Decimal)
        '' 輪距(前輪) 
        Private _RinkyoZenrin As Nullable(of Decimal)
        '' 室内の内側寸法(長さ) 
        Private _InRoomNagasa As Nullable(of Decimal)
        '' 室内の内側寸法(幅) 
        Private _InRoomHaba As Nullable(of Decimal)
        '' 室内の内側寸法(高さ) 
        Private _InRoomTakasa As Nullable(of Decimal)
        '' 室内の内側寸法(高さ)サンルーフ装着車 
        Private _InRoomTakasaSr As Nullable(of Decimal)
        '' 車両重量 
        Private _Juryo As Nullable(of Int32)
        '' 車両重量(後輪重) 
        Private _JuryoKorin As Nullable(of Int32)
        '' 車両重量(前輪重) 
        Private _JuryoZenrin As Nullable(of Int32)
        '' 乗車定員(人) 
        Private _JoshaTeiin As Nullable(of Int32)
        '' 車両総重量 
        Private _SharyoSojuryo As Nullable(of Int32)
        '' 車両総重量(後輪重) 
        Private _SojuryoKorin As Nullable(of Int32)
        '' 車両総重量(前輪重) 
        Private _SojuryoZenrin As Nullable(of Int32)
        '' 燃料消費率 
        Private _NenryoShohiRitu As Nullable(of Decimal)
        '' 制動停止距離 
        Private _SeidoteishiKyori As Nullable(of Int32)
        '' 制動停止初速 
        Private _SeidoteishiShosoku As Nullable(of Int32)
        '' 原動機の型式 
        Private _GendokiKatasiki As String
        '' 最高出力(回転数) 
        Private _MaxHpKaiten As Nullable(of Int32)
        '' 最高出力(グロス) 
        Private _MaxHpGross As Nullable(of Int32)
        '' 最高出力(ネット) 
        Private _MaxHpNet As Nullable(of Int32)
        '' 最大トルク(回転数) 
        Private _MaxTorqueKaiten As Nullable(of Int32)
        '' 最大トルク(グロス) 
        Private _MaxTorqueGross As Nullable(of Int32)
        '' 最大トルク(ネット) 
        Private _MaxTorqueNet As Nullable(of Int32)
        '' 騒音dB排気(近接) 
        Private _NoiseHaiki As Nullable(of Int32)
        '' 騒音dB加速 
        Private _NoiseKasoku As Nullable(of Int32)
        '' 騒音dB定常 
        Private _NoiseTeijo As Nullable(of Int32)
        '' 排ガス清浄方式 
        Private _HaigasSeijo As String
        '' 排ガス重量10・15モード(CO) 
        Private _Haigas1015Co As Nullable(of Decimal)
        '' 排ガス重量10・15モード(HC) 
        Private _Haigas1015Hc As Nullable(of Decimal)
        '' 排ガス重量10・15モード(Nox) 
        Private _Haigas1015Nox As Nullable(of Decimal)
        '' 排ガス重量11モード(CO) 
        Private _Haigas11Co As Nullable(of Decimal)
        '' 排ガス重量11モード(HC) 
        Private _Haigas11Hc As Nullable(of Decimal)
        '' 排ガス重量11モード(Nox) 
        Private _Haigas11Nox As Nullable(of Decimal)
        '' 最大安定傾斜角度(左) 
        Private _KeishaLeft As Nullable(of Int32)
        '' 最大安定傾斜角度(左)算出区分 
        Private _KeishaLeftKbn As String
        '' 最大安定傾斜角度(右) 
        Private _KeishaRight As Nullable(of Int32)
        '' 最大安定傾斜角度(右)算出区分 
        Private _KeishaRightKbn As String
        '' 車輪配列 
        Private _SharinHairetsu As String
        '' 旅客運送事業用自動車の適否 
        Private _RyokakuTekihi As String
        '' 型式種別コード 
        Private _KataShubetsuCode As String
        '' ステータス 
        Private _Status As String
        '' 工場シリーズ 
        Private _KojoSiries As String
        '' 生産拠点コード 
        Private _KyotenCode As String
        '' 採用年月日 
        Private _SaiyoDate As Nullable(of Int32)
        '' 廃止年月日 
        Private _HaisiDate As Nullable(of Int32)
        '' アライアンス用識別区分 
        Private _AlianceSkbn As String
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

        ''' <summary>装備改訂No.</summary>
        ''' <value>装備改訂No.</value>
        ''' <returns>装備改訂No.</returns>
        Public Property SobiKaiteiNo() As String
            Get
                Return _SobiKaiteiNo
            End Get
            Set(ByVal value As String)
                _SobiKaiteiNo = value
            End Set
        End Property

        ''' <summary>7桁型式識別コード</summary>
        ''' <value>7桁型式識別コード</value>
        ''' <returns>7桁型式識別コード</returns>
        Public Property KatashikiScd7() As String
            Get
                Return _KatashikiScd7
            End Get
            Set(ByVal value As String)
                _KatashikiScd7 = value
            End Set
        End Property

        ''' <summary>開発符号(認証型式)</summary>
        ''' <value>開発符号(認証型式)</value>
        ''' <returns>開発符号(認証型式)</returns>
        Public Property KaihatsufgNinsho() As String
            Get
                Return _KaihatsufgNinsho
            End Get
            Set(ByVal value As String)
                _KaihatsufgNinsho = value
            End Set
        End Property

        ''' <summary>認証型式符号</summary>
        ''' <value>認証型式符号</value>
        ''' <returns>認証型式符号</returns>
        Public Property NinshoKatasiki() As String
            Get
                Return _NinshoKatasiki
            End Get
            Set(ByVal value As String)
                _NinshoKatasiki = value
            End Set
        End Property

        ''' <summary>7桁型式符号</summary>
        ''' <value>7桁型式符号</value>
        ''' <returns>7桁型式符号</returns>
        Public Property KatashikiFugo7() As String
            Get
                Return _KatashikiFugo7
            End Get
            Set(ByVal value As String)
                _KatashikiFugo7 = value
            End Set
        End Property

        ''' <summary>リストコード</summary>
        ''' <value>リストコード</value>
        ''' <returns>リストコード</returns>
        Public Property ListCode() As String
            Get
                Return _ListCode
            End Get
            Set(ByVal value As String)
                _ListCode = value
            End Set
        End Property

        ''' <summary>アプライドNo.</summary>
        ''' <value>アプライドNo.</value>
        ''' <returns>アプライドNo.</returns>
        Public Property AppliedNo() As Nullable(of Int32)
            Get
                Return _AppliedNo
            End Get
            Set(ByVal value As Nullable(of Int32))
                _AppliedNo = value
            End Set
        End Property

        ''' <summary>表示順</summary>
        ''' <value>表示順</value>
        ''' <returns>表示順</returns>
        Public Property HyojijunNo() As Nullable(of Int32)
            Get
                Return _HyojijunNo
            End Get
            Set(ByVal value As Nullable(of Int32))
                _HyojijunNo = value
            End Set
        End Property

        ''' <summary>車系コード</summary>
        ''' <value>車系コード</value>
        ''' <returns>車系コード</returns>
        Public Property ShakeiCode() As String
            Get
                Return _ShakeiCode
            End Get
            Set(ByVal value As String)
                _ShakeiCode = value
            End Set
        End Property

        ''' <summary>ボディ基本型</summary>
        ''' <value>ボディ基本型</value>
        ''' <returns>ボディ基本型</returns>
        Public Property BodyKihonKata() As String
            Get
                Return _BodyKihonKata
            End Get
            Set(ByVal value As String)
                _BodyKihonKata = value
            End Set
        End Property

        ''' <summary>E/G排気量</summary>
        ''' <value>E/G排気量</value>
        ''' <returns>E/G排気量</returns>
        Public Property EgHaikiryo() As String
            Get
                Return _EgHaikiryo
            End Get
            Set(ByVal value As String)
                _EgHaikiryo = value
            End Set
        End Property

        ''' <summary>駆動方式</summary>
        ''' <value>駆動方式</value>
        ''' <returns>駆動方式</returns>
        Public Property KudoHosiki() As String
            Get
                Return _KudoHosiki
            End Get
            Set(ByVal value As String)
                _KudoHosiki = value
            End Set
        End Property

        ''' <summary>懸架装置</summary>
        ''' <value>懸架装置</value>
        ''' <returns>懸架装置</returns>
        Public Property KenkaSochi() As String
            Get
                Return _KenkaSochi
            End Get
            Set(ByVal value As String)
                _KenkaSochi = value
            End Set
        End Property

        ''' <summary>動弁系</summary>
        ''' <value>動弁系</value>
        ''' <returns>動弁系</returns>
        Public Property DobenkeiCode() As String
            Get
                Return _DobenkeiCode
            End Get
            Set(ByVal value As String)
                _DobenkeiCode = value
            End Set
        End Property

        ''' <summary>年改符号</summary>
        ''' <value>年改符号</value>
        ''' <returns>年改符号</returns>
        Public Property NenkaiFugo() As String
            Get
                Return _NenkaiFugo
            End Get
            Set(ByVal value As String)
                _NenkaiFugo = value
            End Set
        End Property

        ''' <summary>ドア数</summary>
        ''' <value>ドア数</value>
        ''' <returns>ドア数</returns>
        Public Property DoorSu() As String
            Get
                Return _DoorSu
            End Get
            Set(ByVal value As String)
                _DoorSu = value
            End Set
        End Property

        ''' <summary>仕向地(大区分)</summary>
        ''' <value>仕向地(大区分)</value>
        ''' <returns>仕向地(大区分)</returns>
        Public Property ShimukeDaiKbn() As String
            Get
                Return _ShimukeDaiKbn
            End Get
            Set(ByVal value As String)
                _ShimukeDaiKbn = value
            End Set
        End Property

        ''' <summary>グレードコード</summary>
        ''' <value>グレードコード</value>
        ''' <returns>グレードコード</returns>
        Public Property GradeCode() As String
            Get
                Return _GradeCode
            End Get
            Set(ByVal value As String)
                _GradeCode = value
            End Set
        End Property

        ''' <summary>T/M</summary>
        ''' <value>T/M</value>
        ''' <returns>T/M</returns>
        Public Property TransMission() As String
            Get
                Return _TransMission
            End Get
            Set(ByVal value As String)
                _TransMission = value
            End Set
        End Property

        ''' <summary>燃料供給システム</summary>
        ''' <value>燃料供給システム</value>
        ''' <returns>燃料供給システム</returns>
        Public Property NenryoKyokyuSys() As String
            Get
                Return _NenryoKyokyuSys
            End Get
            Set(ByVal value As String)
                _NenryoKyokyuSys = value
            End Set
        End Property

        ''' <summary>加給機</summary>
        ''' <value>加給機</value>
        ''' <returns>加給機</returns>
        Public Property KakyukiCode() As String
            Get
                Return _KakyukiCode
            End Get
            Set(ByVal value As String)
                _KakyukiCode = value
            End Set
        End Property

        ''' <summary>ハンドル位置</summary>
        ''' <value>ハンドル位置</value>
        ''' <returns>ハンドル位置</returns>
        Public Property HandlePos() As String
            Get
                Return _HandlePos
            End Get
            Set(ByVal value As String)
                _HandlePos = value
            End Set
        End Property

        ''' <summary>長さ</summary>
        ''' <value>長さ</value>
        ''' <returns>長さ</returns>
        Public Property SharyoNagasa() As Nullable(of Decimal)
            Get
                Return _SharyoNagasa
            End Get
            Set(ByVal value As Nullable(of Decimal))
                _SharyoNagasa = value
            End Set
        End Property

        ''' <summary>幅</summary>
        ''' <value>幅</value>
        ''' <returns>幅</returns>
        Public Property SharyoHaba() As Nullable(of Decimal)
            Get
                Return _SharyoHaba
            End Get
            Set(ByVal value As Nullable(of Decimal))
                _SharyoHaba = value
            End Set
        End Property

        ''' <summary>高さ</summary>
        ''' <value>高さ</value>
        ''' <returns>高さ</returns>
        Public Property SharyoTakasa() As Nullable(of Decimal)
            Get
                Return _SharyoTakasa
            End Get
            Set(ByVal value As Nullable(of Decimal))
                _SharyoTakasa = value
            End Set
        End Property

        ''' <summary>軸距</summary>
        ''' <value>軸距</value>
        ''' <returns>軸距</returns>
        Public Property JikukyoNagasa() As Nullable(of Decimal)
            Get
                Return _JikukyoNagasa
            End Get
            Set(ByVal value As Nullable(of Decimal))
                _JikukyoNagasa = value
            End Set
        End Property

        ''' <summary>輪距(後輪)</summary>
        ''' <value>輪距(後輪)</value>
        ''' <returns>輪距(後輪)</returns>
        Public Property RinkyoKorin() As Nullable(of Decimal)
            Get
                Return _RinkyoKorin
            End Get
            Set(ByVal value As Nullable(of Decimal))
                _RinkyoKorin = value
            End Set
        End Property

        ''' <summary>輪距(前輪)</summary>
        ''' <value>輪距(前輪)</value>
        ''' <returns>輪距(前輪)</returns>
        Public Property RinkyoZenrin() As Nullable(of Decimal)
            Get
                Return _RinkyoZenrin
            End Get
            Set(ByVal value As Nullable(of Decimal))
                _RinkyoZenrin = value
            End Set
        End Property

        ''' <summary>室内の内側寸法(長さ)</summary>
        ''' <value>室内の内側寸法(長さ)</value>
        ''' <returns>室内の内側寸法(長さ)</returns>
        Public Property InRoomNagasa() As Nullable(of Decimal)
            Get
                Return _InRoomNagasa
            End Get
            Set(ByVal value As Nullable(of Decimal))
                _InRoomNagasa = value
            End Set
        End Property

        ''' <summary>室内の内側寸法(幅)</summary>
        ''' <value>室内の内側寸法(幅)</value>
        ''' <returns>室内の内側寸法(幅)</returns>
        Public Property InRoomHaba() As Nullable(of Decimal)
            Get
                Return _InRoomHaba
            End Get
            Set(ByVal value As Nullable(of Decimal))
                _InRoomHaba = value
            End Set
        End Property

        ''' <summary>室内の内側寸法(高さ)</summary>
        ''' <value>室内の内側寸法(高さ)</value>
        ''' <returns>室内の内側寸法(高さ)</returns>
        Public Property InRoomTakasa() As Nullable(of Decimal)
            Get
                Return _InRoomTakasa
            End Get
            Set(ByVal value As Nullable(of Decimal))
                _InRoomTakasa = value
            End Set
        End Property

        ''' <summary>室内の内側寸法(高さ)サンルーフ装着車</summary>
        ''' <value>室内の内側寸法(高さ)サンルーフ装着車</value>
        ''' <returns>室内の内側寸法(高さ)サンルーフ装着車</returns>
        Public Property InRoomTakasaSr() As Nullable(of Decimal)
            Get
                Return _InRoomTakasaSr
            End Get
            Set(ByVal value As Nullable(of Decimal))
                _InRoomTakasaSr = value
            End Set
        End Property

        ''' <summary>車両重量</summary>
        ''' <value>車両重量</value>
        ''' <returns>車両重量</returns>
        Public Property Juryo() As Nullable(of Int32)
            Get
                Return _Juryo
            End Get
            Set(ByVal value As Nullable(of Int32))
                _Juryo = value
            End Set
        End Property

        ''' <summary>車両重量(後輪重)</summary>
        ''' <value>車両重量(後輪重)</value>
        ''' <returns>車両重量(後輪重)</returns>
        Public Property JuryoKorin() As Nullable(of Int32)
            Get
                Return _JuryoKorin
            End Get
            Set(ByVal value As Nullable(of Int32))
                _JuryoKorin = value
            End Set
        End Property

        ''' <summary>車両重量(前輪重)</summary>
        ''' <value>車両重量(前輪重)</value>
        ''' <returns>車両重量(前輪重)</returns>
        Public Property JuryoZenrin() As Nullable(of Int32)
            Get
                Return _JuryoZenrin
            End Get
            Set(ByVal value As Nullable(of Int32))
                _JuryoZenrin = value
            End Set
        End Property

        ''' <summary>乗車定員(人)</summary>
        ''' <value>乗車定員(人)</value>
        ''' <returns>乗車定員(人)</returns>
        Public Property JoshaTeiin() As Nullable(of Int32)
            Get
                Return _JoshaTeiin
            End Get
            Set(ByVal value As Nullable(of Int32))
                _JoshaTeiin = value
            End Set
        End Property

        ''' <summary>車両総重量</summary>
        ''' <value>車両総重量</value>
        ''' <returns>車両総重量</returns>
        Public Property SharyoSojuryo() As Nullable(of Int32)
            Get
                Return _SharyoSojuryo
            End Get
            Set(ByVal value As Nullable(of Int32))
                _SharyoSojuryo = value
            End Set
        End Property

        ''' <summary>車両総重量(後輪重)</summary>
        ''' <value>車両総重量(後輪重)</value>
        ''' <returns>車両総重量(後輪重)</returns>
        Public Property SojuryoKorin() As Nullable(of Int32)
            Get
                Return _SojuryoKorin
            End Get
            Set(ByVal value As Nullable(of Int32))
                _SojuryoKorin = value
            End Set
        End Property

        ''' <summary>車両総重量(前輪重)</summary>
        ''' <value>車両総重量(前輪重)</value>
        ''' <returns>車両総重量(前輪重)</returns>
        Public Property SojuryoZenrin() As Nullable(of Int32)
            Get
                Return _SojuryoZenrin
            End Get
            Set(ByVal value As Nullable(of Int32))
                _SojuryoZenrin = value
            End Set
        End Property

        ''' <summary>燃料消費率</summary>
        ''' <value>燃料消費率</value>
        ''' <returns>燃料消費率</returns>
        Public Property NenryoShohiRitu() As Nullable(of Decimal)
            Get
                Return _NenryoShohiRitu
            End Get
            Set(ByVal value As Nullable(of Decimal))
                _NenryoShohiRitu = value
            End Set
        End Property

        ''' <summary>制動停止距離</summary>
        ''' <value>制動停止距離</value>
        ''' <returns>制動停止距離</returns>
        Public Property SeidoteishiKyori() As Nullable(of Int32)
            Get
                Return _SeidoteishiKyori
            End Get
            Set(ByVal value As Nullable(of Int32))
                _SeidoteishiKyori = value
            End Set
        End Property

        ''' <summary>制動停止初速</summary>
        ''' <value>制動停止初速</value>
        ''' <returns>制動停止初速</returns>
        Public Property SeidoteishiShosoku() As Nullable(of Int32)
            Get
                Return _SeidoteishiShosoku
            End Get
            Set(ByVal value As Nullable(of Int32))
                _SeidoteishiShosoku = value
            End Set
        End Property

        ''' <summary>原動機の型式</summary>
        ''' <value>原動機の型式</value>
        ''' <returns>原動機の型式</returns>
        Public Property GendokiKatasiki() As String
            Get
                Return _GendokiKatasiki
            End Get
            Set(ByVal value As String)
                _GendokiKatasiki = value
            End Set
        End Property

        ''' <summary>最高出力(回転数)</summary>
        ''' <value>最高出力(回転数)</value>
        ''' <returns>最高出力(回転数)</returns>
        Public Property MaxHpKaiten() As Nullable(of Int32)
            Get
                Return _MaxHpKaiten
            End Get
            Set(ByVal value As Nullable(of Int32))
                _MaxHpKaiten = value
            End Set
        End Property

        ''' <summary>最高出力(グロス)</summary>
        ''' <value>最高出力(グロス)</value>
        ''' <returns>最高出力(グロス)</returns>
        Public Property MaxHpGross() As Nullable(of Int32)
            Get
                Return _MaxHpGross
            End Get
            Set(ByVal value As Nullable(of Int32))
                _MaxHpGross = value
            End Set
        End Property

        ''' <summary>最高出力(ネット)</summary>
        ''' <value>最高出力(ネット)</value>
        ''' <returns>最高出力(ネット)</returns>
        Public Property MaxHpNet() As Nullable(of Int32)
            Get
                Return _MaxHpNet
            End Get
            Set(ByVal value As Nullable(of Int32))
                _MaxHpNet = value
            End Set
        End Property

        ''' <summary>最大トルク(回転数)</summary>
        ''' <value>最大トルク(回転数)</value>
        ''' <returns>最大トルク(回転数)</returns>
        Public Property MaxTorqueKaiten() As Nullable(of Int32)
            Get
                Return _MaxTorqueKaiten
            End Get
            Set(ByVal value As Nullable(of Int32))
                _MaxTorqueKaiten = value
            End Set
        End Property

        ''' <summary>最大トルク(グロス)</summary>
        ''' <value>最大トルク(グロス)</value>
        ''' <returns>最大トルク(グロス)</returns>
        Public Property MaxTorqueGross() As Nullable(of Int32)
            Get
                Return _MaxTorqueGross
            End Get
            Set(ByVal value As Nullable(of Int32))
                _MaxTorqueGross = value
            End Set
        End Property

        ''' <summary>最大トルク(ネット)</summary>
        ''' <value>最大トルク(ネット)</value>
        ''' <returns>最大トルク(ネット)</returns>
        Public Property MaxTorqueNet() As Nullable(of Int32)
            Get
                Return _MaxTorqueNet
            End Get
            Set(ByVal value As Nullable(of Int32))
                _MaxTorqueNet = value
            End Set
        End Property

        ''' <summary>騒音dB排気(近接)</summary>
        ''' <value>騒音dB排気(近接)</value>
        ''' <returns>騒音dB排気(近接)</returns>
        Public Property NoiseHaiki() As Nullable(of Int32)
            Get
                Return _NoiseHaiki
            End Get
            Set(ByVal value As Nullable(of Int32))
                _NoiseHaiki = value
            End Set
        End Property

        ''' <summary>騒音dB加速</summary>
        ''' <value>騒音dB加速</value>
        ''' <returns>騒音dB加速</returns>
        Public Property NoiseKasoku() As Nullable(of Int32)
            Get
                Return _NoiseKasoku
            End Get
            Set(ByVal value As Nullable(of Int32))
                _NoiseKasoku = value
            End Set
        End Property

        ''' <summary>騒音dB定常</summary>
        ''' <value>騒音dB定常</value>
        ''' <returns>騒音dB定常</returns>
        Public Property NoiseTeijo() As Nullable(of Int32)
            Get
                Return _NoiseTeijo
            End Get
            Set(ByVal value As Nullable(of Int32))
                _NoiseTeijo = value
            End Set
        End Property

        ''' <summary>排ガス清浄方式</summary>
        ''' <value>排ガス清浄方式</value>
        ''' <returns>排ガス清浄方式</returns>
        Public Property HaigasSeijo() As String
            Get
                Return _HaigasSeijo
            End Get
            Set(ByVal value As String)
                _HaigasSeijo = value
            End Set
        End Property

        ''' <summary>排ガス重量10・15モード(CO)</summary>
        ''' <value>排ガス重量10・15モード(CO)</value>
        ''' <returns>排ガス重量10・15モード(CO)</returns>
        Public Property Haigas1015Co() As Nullable(of Decimal)
            Get
                Return _Haigas1015Co
            End Get
            Set(ByVal value As Nullable(of Decimal))
                _Haigas1015Co = value
            End Set
        End Property

        ''' <summary>排ガス重量10・15モード(HC)</summary>
        ''' <value>排ガス重量10・15モード(HC)</value>
        ''' <returns>排ガス重量10・15モード(HC)</returns>
        Public Property Haigas1015Hc() As Nullable(of Decimal)
            Get
                Return _Haigas1015Hc
            End Get
            Set(ByVal value As Nullable(of Decimal))
                _Haigas1015Hc = value
            End Set
        End Property

        ''' <summary>排ガス重量10・15モード(Nox)</summary>
        ''' <value>排ガス重量10・15モード(Nox)</value>
        ''' <returns>排ガス重量10・15モード(Nox)</returns>
        Public Property Haigas1015Nox() As Nullable(of Decimal)
            Get
                Return _Haigas1015Nox
            End Get
            Set(ByVal value As Nullable(of Decimal))
                _Haigas1015Nox = value
            End Set
        End Property

        ''' <summary>排ガス重量11モード(CO)</summary>
        ''' <value>排ガス重量11モード(CO)</value>
        ''' <returns>排ガス重量11モード(CO)</returns>
        Public Property Haigas11Co() As Nullable(of Decimal)
            Get
                Return _Haigas11Co
            End Get
            Set(ByVal value As Nullable(of Decimal))
                _Haigas11Co = value
            End Set
        End Property

        ''' <summary>排ガス重量11モード(HC)</summary>
        ''' <value>排ガス重量11モード(HC)</value>
        ''' <returns>排ガス重量11モード(HC)</returns>
        Public Property Haigas11Hc() As Nullable(of Decimal)
            Get
                Return _Haigas11Hc
            End Get
            Set(ByVal value As Nullable(of Decimal))
                _Haigas11Hc = value
            End Set
        End Property

        ''' <summary>排ガス重量11モード(Nox)</summary>
        ''' <value>排ガス重量11モード(Nox)</value>
        ''' <returns>排ガス重量11モード(Nox)</returns>
        Public Property Haigas11Nox() As Nullable(of Decimal)
            Get
                Return _Haigas11Nox
            End Get
            Set(ByVal value As Nullable(of Decimal))
                _Haigas11Nox = value
            End Set
        End Property

        ''' <summary>最大安定傾斜角度(左)</summary>
        ''' <value>最大安定傾斜角度(左)</value>
        ''' <returns>最大安定傾斜角度(左)</returns>
        Public Property KeishaLeft() As Nullable(of Int32)
            Get
                Return _KeishaLeft
            End Get
            Set(ByVal value As Nullable(of Int32))
                _KeishaLeft = value
            End Set
        End Property

        ''' <summary>最大安定傾斜角度(左)算出区分</summary>
        ''' <value>最大安定傾斜角度(左)算出区分</value>
        ''' <returns>最大安定傾斜角度(左)算出区分</returns>
        Public Property KeishaLeftKbn() As String
            Get
                Return _KeishaLeftKbn
            End Get
            Set(ByVal value As String)
                _KeishaLeftKbn = value
            End Set
        End Property

        ''' <summary>最大安定傾斜角度(右)</summary>
        ''' <value>最大安定傾斜角度(右)</value>
        ''' <returns>最大安定傾斜角度(右)</returns>
        Public Property KeishaRight() As Nullable(of Int32)
            Get
                Return _KeishaRight
            End Get
            Set(ByVal value As Nullable(of Int32))
                _KeishaRight = value
            End Set
        End Property

        ''' <summary>最大安定傾斜角度(右)算出区分</summary>
        ''' <value>最大安定傾斜角度(右)算出区分</value>
        ''' <returns>最大安定傾斜角度(右)算出区分</returns>
        Public Property KeishaRightKbn() As String
            Get
                Return _KeishaRightKbn
            End Get
            Set(ByVal value As String)
                _KeishaRightKbn = value
            End Set
        End Property

        ''' <summary>車輪配列</summary>
        ''' <value>車輪配列</value>
        ''' <returns>車輪配列</returns>
        Public Property SharinHairetsu() As String
            Get
                Return _SharinHairetsu
            End Get
            Set(ByVal value As String)
                _SharinHairetsu = value
            End Set
        End Property

        ''' <summary>旅客運送事業用自動車の適否</summary>
        ''' <value>旅客運送事業用自動車の適否</value>
        ''' <returns>旅客運送事業用自動車の適否</returns>
        Public Property RyokakuTekihi() As String
            Get
                Return _RyokakuTekihi
            End Get
            Set(ByVal value As String)
                _RyokakuTekihi = value
            End Set
        End Property

        ''' <summary>型式種別コード</summary>
        ''' <value>型式種別コード</value>
        ''' <returns>型式種別コード</returns>
        Public Property KataShubetsuCode() As String
            Get
                Return _KataShubetsuCode
            End Get
            Set(ByVal value As String)
                _KataShubetsuCode = value
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

        ''' <summary>工場シリーズ</summary>
        ''' <value>工場シリーズ</value>
        ''' <returns>工場シリーズ</returns>
        Public Property KojoSiries() As String
            Get
                Return _KojoSiries
            End Get
            Set(ByVal value As String)
                _KojoSiries = value
            End Set
        End Property

        ''' <summary>生産拠点コード</summary>
        ''' <value>生産拠点コード</value>
        ''' <returns>生産拠点コード</returns>
        Public Property KyotenCode() As String
            Get
                Return _KyotenCode
            End Get
            Set(ByVal value As String)
                _KyotenCode = value
            End Set
        End Property

        ''' <summary>採用年月日</summary>
        ''' <value>採用年月日</value>
        ''' <returns>採用年月日</returns>
        Public Property SaiyoDate() As Nullable(of Int32)
            Get
                Return _SaiyoDate
            End Get
            Set(ByVal value As Nullable(of Int32))
                _SaiyoDate = value
            End Set
        End Property

        ''' <summary>廃止年月日</summary>
        ''' <value>廃止年月日</value>
        ''' <returns>廃止年月日</returns>
        Public Property HaisiDate() As Nullable(of Int32)
            Get
                Return _HaisiDate
            End Get
            Set(ByVal value As Nullable(of Int32))
                _HaisiDate = value
            End Set
        End Property

        ''' <summary>アライアンス用識別区分</summary>
        ''' <value>アライアンス用識別区分</value>
        ''' <returns>アライアンス用識別区分</returns>
        Public Property AlianceSkbn() As String
            Get
                Return _AlianceSkbn
            End Get
            Set(ByVal value As String)
                _AlianceSkbn = value
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
