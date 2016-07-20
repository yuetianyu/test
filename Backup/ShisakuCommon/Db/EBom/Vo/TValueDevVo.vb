
Namespace Db.EBom.Vo
    ''' <summary>属性管理(開発符号毎)</summary>
    Public Class TValueDevVo
        ''' <summary>開発符号</summary>
        Private _KaihatuFugo As String
        ''' <summary>部品番号</summary>
        Private _BuhinNo As String
        ''' <summary>案レベル</summary>
        Private _AnLevel As String
        ''' <summary>行ID</summary>
        Private _RowId As String
        ''' <summary>計画図入力図面名称</summary>
        Private _KeikakuzuInputZumenName As String
        ''' <summary>計画図入力図面番号</summary>
        Private _KeikakuzuInputZumenNo As String
        ''' <summary>計画図入力図面改訂No</summary>
        Private _KeikakuzuInputZumenKaiteiNo As String
        ''' <summary>計画図種別</summary>
        Private _KeikakuzuShubetsu As String
        ''' <summary>計画図造型指示種別</summary>
        Private _KeikakuzuZokeiShijiShubetsu As String
        ''' <summary>計画図通知書</summary>
        Private _KeikakuzuTsuchisho As String
        ''' <summary>計画図回答要求期日</summary>
        Private _KeikakuzuKaitoYokyuKijitsu As String
        ''' <summary>計画図造型データリリース期日</summary>
        Private _KeikakuzuZokeiDataReleaseKijitsu As String
        ''' <summary>計画図回答要求先数</summary>
        Private _KeikakuzuKaitoYokyusakiSuu As String
        ''' <summary>計画図予定</summary>
        Private _KeikakuzuYotei As String
        ''' <summary>計画図実績</summary>
        Private _KeikakuzuJisseki As String
        ''' <summary>計画図出図CADエリア</summary>
        Private _KeikakuzuShutsuzuCadArea As String
        ''' <summary>計画図出図CADモデル</summary>
        Private _KeikakuzuShutsuzuCadModel As String
        ''' <summary>計画図出図備考</summary>
        Private _KeikakuzuShutsuzuBiko As String
        ''' <summary>計画図設計受領</summary>
        Private _KeikakuzuSekkeiJuryo As String
        ''' <summary>計画図設計備考</summary>
        Private _KeikakuzuSekkeiBiko As String
        ''' <summary>計画図回答依頼未回答部署</summary>
        Private _KeikakuzuKaitoiraiMikaitoBusho As String
        ''' <summary>計画図回答依頼回答</summary>
        Private _KeikakuzuKaitoiraiKaito As String
        ''' <summary>CE担当</summary>
        Private _CeTanto As String
        ''' <summary>FHI生産</summary>
        Private _FhiSeisan As String
        ''' <summary>SIA生産</summary>
        Private _SiaSeisan As String
        ''' <summary>SIACKD</summary>
        Private _SiaCkd As String
        ''' <summary>選定優先度</summary>
        Private _SenteiYusendo As String
        ''' <summary>選定SENKAI</summary>
        Private _SenteiSenkai As String
        ''' <summary>選定SENKAI決定</summary>
        Private _SenteiSenkaiKettei As String
        ''' <summary>選定ゲスト</summary>
        Private _SenteiGuest As String
        ''' <summary>選定ゲスト決定</summary>
        Private _SenteiGuestKettei As String
        ''' <summary>選定コンセプト</summary>
        Private _SenteiComcept As String
        ''' <summary>選定コンセプト決定</summary>
        Private _SenteiComceptKettei As String
        ''' <summary>選定デザイン</summary>
        Private _SenteiDesign As String
        ''' <summary>選定デザイン決定</summary>
        Private _SenteiDesignKettei As String
        ''' <summary>選定ノミネート</summary>
        Private _SenteiNominate As String
        ''' <summary>選定切片</summary>
        Private _SenteiSeppen As String
        ''' <summary>選定切片改訂</summary>
        Private _SenteiSeppenKaitei As String
        ''' <summary>選定供与</summary>
        Private _SenteiKyoyo As String
        ''' <summary>選定供与車種</summary>
        Private _SenteiKyoyoSyasyu As String
        ''' <summary>選定供与決定</summary>
        Private _SenteiKyoyoKettei As String
        ''' <summary>ノミネート決定希望</summary>
        Private _NominateKetteiKibo As String
        ''' <summary>使用外装予定</summary>
        Private _ShiyoGaisoYotei As String
        ''' <summary>使用外装実績</summary>
        Private _ShiyoGaisoJisseki As String
        ''' <summary>ノミネート発行予定</summary>
        Private _NominateHakkoYotei As String
        ''' <summary>ノミネート発行実績FHI</summary>
        Private _NominateHakkoJissekiFhi As String
        ''' <summary>ノミネート発行実績SIA</summary>
        Private _NominateHakkoJissekiSia As String
        ''' <summary>ノミネート受付実績FHI</summary>
        Private _NominateUketsukeJissekiFhi As String
        ''' <summary>ノミネート受付実績SIA</summary>
        Private _NominateUketsukeJissekiSia As String
        ''' <summary>ノミネート承認予定</summary>
        Private _NominateSyoninYotei As String
        ''' <summary>ノミネート承認実績FFI</summary>
        Private _NominateSyoninJissekiFhi As String
        ''' <summary>ノミネート承認実績SIA</summary>
        Private _NominateSyoninJissekiSia As String
        ''' <summary>ノミネート備考</summary>
        Private _NominateBiko As String
        ''' <summary>原価発行予定</summary>
        Private _GenkaHakkoYotei As String
        ''' <summary>原価受付実績</summary>
        Private _GenkaUketsukeJisseki As String
        ''' <summary>原価設定実績</summary>
        Private _GenkaSetteiJisseki As String
        ''' <summary>見積もり依頼実績FHI</summary>
        Private _MitsumoriIraiJissekiFhi As String
        ''' <summary>見積もり依頼実績SIA</summary>
        Private _MitsumoriIraiJissekiSIA As String
        ''' <summary>見積もり受領予定</summary>
        Private _MitsumoriJuryoYotei As String
        ''' <summary>見積もり受領実績FHI</summary>
        Private _MitsumoriJuryoJissekiFhi As String
        ''' <summary>見積もり受領実績SIA</summary>
        Private _MitsumoriJuryoJissekiSia As String
        ''' <summary>見積もり受領取引先</summary>
        Private _MitsumoriJuryoTorihikisaki As String
        ''' <summary>見積もり未受領取引先</summary>
        Private _MitsumoriMijuryoTorihikisaki As String
        ''' <summary>目標達成</summary>
        Private _MokuhyoTassei As String
        ''' <summary>承認状況</summary>
        Private _SyoninJoukyo As String
        ''' <summary>承認願い発行予定</summary>
        Private _SyoninNegaiHakkoYotei As String
        ''' <summary>承認願い発行実績FHI</summary>
        Private _SyoninNegaiHakkoJissekiFhi As String
        ''' <summary>承認願い発行実績SIA</summary>
        Private _SyoninNegaiHakkoJissekiSia As String
        ''' <summary>承認願い受付実績Fhi</summary>
        Private _SyoninNegaiUketsukeJissekiFhi As String
        ''' <summary>承認願い受付実績Sia</summary>
        Private _SyoninNegaiUketsukeJissekiSia As String
        ''' <summary>取引先決定予定FHI</summary>
        Private _TorihikisakiKetteiYoteiFhi As String
        ''' <summary>取引先決定予定SIA</summary>
        Private _TorihikisakiKetteiYoteiSia As String
        ''' <summary>取引先日程FHI</summary>
        Private _TorihikisakiNitteiFhi As String
        ''' <summary>取引先日程SIA</summary>
        Private _TorihikisakiNitteiSia As String
        ''' <summary>取引先実績FHI</summary>
        Private _TorihikisakiJissekiFhi As String
        ''' <summary>取引先実績SIA</summary>
        Private _TorihikisakiJissekiSia As String
        ''' <summary>コスト精査</summary>
        Private _CostSeisa As String
        ''' <summary>FHIノミネート区分</summary>
        Private _FhiNominateKbn As String
        ''' <summary>FHIノミネート1</summary>
        Private _FhiNominate1 As String
        ''' <summary>FHIノミネート2</summary>
        Private _FhiNominate2 As String
        ''' <summary>FHIノミネート3</summary>
        Private _FhiNominate3 As String
        ''' <summary>FHIノミネート4</summary>
        Private _FhiNominate4 As String
        ''' <summary>FHIノミネート5</summary>
        Private _FhiNominate5 As String
        ''' <summary>FHIノミネート6</summary>
        Private _FhiNominate6 As String
        ''' <summary>FHIノミネート追加</summary>
        Private _FhiNominateAdd As String
        ''' <summary>FHIノミネート購担</summary>
        Private _FhiNominateKotan As String
        ''' <summary>SIAノミネート区分</summary>
        Private _SiaNominateKbn As String
        ''' <summary>SIAノミネート1</summary>
        Private _SiaNominate1 As String
        ''' <summary>SIAノミネート2</summary>
        Private _SiaNominate2 As String
        ''' <summary>SIAノミネート3</summary>
        Private _SiaNominate3 As String
        ''' <summary>SIAノミネート4</summary>
        Private _SiaNominate4 As String
        ''' <summary>SIAノミネート5</summary>
        Private _SiaNominate5 As String
        ''' <summary>SIAノミネート6</summary>
        Private _SiaNominate6 As String
        ''' <summary>SIAノミネート追加</summary>
        Private _SiaNominateAdd As String
        ''' <summary>SIAノミネート購担</summary>
        Private _SiaNominateKotan As String
        ''' <summary>取引先コード</summary>
        Private _TorihikisakiCode As String
        ''' <summary>取引先可否FHI</summary>
        Private _TorihikisakiKahiFhi As String
        ''' <summary>取引先可否SIA</summary>
        Private _TorihikisakiKahiSia As String
        ''' <summary>選定未完部品</summary>
        Private _SenteiMikanBuhin As String
        ''' <summary>計画図入力図面番号URL</summary>
        Private _KeikakuzuInputZumenNoUrl As String
        ''' <summary>計画図通知書URL</summary>
        Private _KeikakuzuTsuchishoUrl As String
        ''' <summary>計画図回答依頼造型予定</summary>
        Private _KeikakuzuKaitoiraiZokeiYotei As String
        ''' <summary>計画図回答依頼造型備考</summary>
        Private _KeikakuzuKaitoiraiZokeiBiko As String
        ''' <summary>計画図回答依頼造型発行日</summary>
        Private _KeikakuzuKaitoiraiZokeiHakkobi As String
        ''' <summary>計画図回答依頼造型発行タイトル</summary>
        Private _KeikakuzuKaitoiraiZokeiHakkoTitle As String
        ''' <summary>計画図回答依頼造型発行回数</summary>
        Private _KeikakuzuKaitoiraiZokeiHakkoKaisu As String
        ''' <summary>計画図回答依頼造型CD</summary>
        Private _KeikakuzuKaitoiraiZokeiCd As String
        ''' <summary>計画図回答依頼造型エリア</summary>
        Private _KeikakuzuKaitoiraiZokeiArea As String
        ''' <summary>計画図回答依頼造型モデル</summary>
        Private _KeikakuzuKaitoiraiZokeiModel As String
        ''' <summary>生産図パーツ番号</summary>
        Private _SeisanzuPartsNum As String
        ''' <summary>SIA生産図パーツ番号</summary>
        Private _SiaSeisanzuPartsNum As String
        ''' <summary>石膏書パーツ番号</summary>
        Private _SekkoushoPartsNum As String
        ''' <summary>S0パーツ番号</summary>
        Private _S0PartsNum As String
        ''' <summary>S1パーツ番号</summary>
        Private _S1PartsNum As String
        ''' <summary>S2パーツ番号</summary>
        Private _S2PartsNum As String
        ''' <summary>受信区分</summary>
        Private _JushinKbn As String
        ''' <summary>受信X</summary>
        Private _JushinX As String
        ''' <summary>受信Y</summary>
        Private _JushinY As String
        ''' <summary>受信Z</summary>
        Private _JushinZ As String
        ''' <summary>作成日</summary>
        Private _CreatedDate As String
        ''' <summary>作成時間</summary>
        Private _CreatedTime As String
        ''' <summary>作成ユーザーID</summary>
        Private _CreatedUserId As String
        ''' <summary>更新日</summary>
        Private _UpdatedDate As String
        ''' <summary>更新時間</summary>
        Private _UpdatedTime As String
        ''' <summary>更新ユーザーID</summary>
        Private _UpdatedUserId As String
        ''' <summary>取引先未決</summary>
        Private _TorihikisakiMiketsu As String
        ''' <summary>色コード</summary>
        Private _ColorCode As String
        ''' <summary>計画図チェックリストファイルパス</summary>
        Private _KeikakuzuCheckListFilePath As String
        ''' <summary>ノミネート承認決定希望設計</summary>
        Private _NominateSyoninKetteiKiboSekkei As String
        ''' <summary>ノミネートノート</summary>
        Private _NominateNote As String
        ''' <summary>クォリティー完成パーツ</summary>
        Private _QualityCompParts As String
        ''' <summary>クォリティー完成パーツデザインコンフ</summary>
        Private _QualityCompPartsDesignConf As String
        ''' <summary>描画プレゼンテーションプラン</summary>
        Private _DrawingPresentationPlan As String
        ''' <summary>回答結果</summary>
        Private _AnswersResults As String
        ''' <summary>クォリティー完成パーツ対象外理由</summary>
        Private _QualityCompPartsTaisyogaiRiyu As String
        ''' <summary>SEKメンテノート</summary>
        Private _SekMenteNote As String
        ''' <summary>SGマーカー開発費</summary>
        Private _SGMakerKaihatsuhi As String
        ''' <summary>SGマーカーパレット</summary>
        Private _SGMakerPallet As String
        ''' <summary>見積もり仕様</summary>
        Private _MitsumoriShiyou As String
        ''' <summary>SGミックス開発費</summary>
        Private _SGMixKaihatsuhi As String
        ''' <summary>SGミックスパレット</summary>
        Private _SGMixPallet As String
        ''' <summary>データランク予定A</summary>
        Private _DataRankYoteiA As String
        ''' <summary>データランク予定B</summary>
        Private _DataRankYoteiB As String
        ''' <summary>データランク予定C</summary>
        Private _DataRankYoteiC As String
        ''' <summary>データランク予定D</summary>
        Private _DataRankYoteiD As String
        ''' <summary>データランク予定E</summary>
        Private _DataRankYoteiE As String
        ''' <summary>データランク履歴A</summary>
        Private _DataRankRirekiA As String
        ''' <summary>データランク履歴B</summary>
        Private _DataRankRirekiB As String
        ''' <summary>データランク履歴C</summary>
        Private _DataRankRirekiC As String
        ''' <summary>データランク履歴D</summary>
        Private _DataRankRirekiD As String
        ''' <summary>データランク履歴E</summary>
        Private _DataRankRirekiE As String
        ''' <summary>データランク現行</summary>
        Private _DataRankGenko As String
        ''' <summary>公開レベル</summary>
        Private _KoukaiLevel As String
        ''' <summary>CATIAダウンロード名称</summary>
        Private _CatiaDlname As String
        ''' <summary>CATIAファイル名称</summary>
        Private _CatiaFileName As String
        ''' <summary>CATIA更新</summary>
        Private _CatiaUpdate As String
        ''' <summary>試作備考１</summary>
        Private _ShisakuBiko1 As String
        ''' <summary>試作備考2</summary>
        Private _ShisakuBiko2 As String
        ''' <summary>Catiaデータ詳細</summary>
        Private _CatiaDataShosai As String
        ''' <summary>Catiaファイルタイムスタンプ</summary>
        Private _CatiaFileTimestamp As String
        ''' <summary>データランク予定W</summary>
        Private _DataRankYoteiW As String
        ''' <summary>データランク予定S1</summary>
        Private _DataRankYoteiS1 As String
        ''' <summary>データランク予定S2</summary>
        Private _DataRankYoteiS2 As String
        ''' <summary>データランク予定S3</summary>
        Private _DataRankYoteiS3 As String
        ''' <summary>データランク予定G</summary>
        Private _DataRankYoteiG As String
        ''' <summary>データランク実績W</summary>
        Private _DataRankJissekiW As String
        ''' <summary>データランク実績S1</summary>
        Private _DataRankJissekiS1 As String
        ''' <summary>データランク実績S2</summary>
        Private _DataRankJissekiS2 As String
        ''' <summary>データランク実績S3</summary>
        Private _DataRankJissekiS3 As String
        ''' <summary>データランク実績G</summary>
        Private _DataRankJissekiG As String
        ''' <summary>承認願い発行予定修正</summary>
        Private _ShoninNegaiHakkoYoteiShusei As String

        ''' <summary>開発符号</summary>
        ''' <value>開発符号</value>
        ''' <returns>開発符号</returns>
        Public Property KaihatsuFugo() As String
            Get
                Return _KaihatuFugo
            End Get
            Set(ByVal value As String)
                _KaihatuFugo = value
            End Set
        End Property

        ''' <summary>部品番号</summary>
        ''' <value>部品番号</value>
        ''' <returns>部品番号</returns>
        Public Property BuhinNo() As String
            Get
                Return _BuhinNo
            End Get
            Set(ByVal value As String)
                _BuhinNo = value
            End Set
        End Property

        ''' <summary>案レベル</summary>
        ''' <value>案レベル</value>
        ''' <returns>案レベル</returns>
        Public Property AnLevel() As String
            Get
                Return _AnLevel
            End Get
            Set(ByVal value As String)
                _AnLevel = value
            End Set
        End Property

        ''' <summary>行ID</summary>
        ''' <value>行ID</value>
        ''' <returns>行ID</returns>
        Public Property RowId() As String
            Get
                Return _RowId
            End Get
            Set(ByVal value As String)
                _RowId = value
            End Set
        End Property

        ''' <summary>計画図入力図面名称</summary>
        ''' <value>計画図入力図面名称</value>
        ''' <returns>計画図入力図面名称</returns>
        Public Property KeikakuzuInputZumenName() As String
            Get
                Return _KeikakuzuInputZumenName
            End Get
            Set(ByVal value As String)
                _KeikakuzuInputZumenName = value
            End Set
        End Property

        ''' <summary>計画図入力図面番号</summary>
        ''' <value>計画図入力図面番号</value>
        ''' <returns>計画図入力図面番号</returns>
        Public Property KeikakuzuInputZumenNo() As String
            Get
                Return _KeikakuzuInputZumenNo
            End Get
            Set(ByVal value As String)
                _KeikakuzuInputZumenNo = value
            End Set
        End Property

        ''' <summary>計画図入力図面改訂No</summary>
        ''' <value>計画図入力図面改訂No</value>
        ''' <returns>計画図入力図面改訂No</returns>
        Public Property KeikakuzuInputZumenKaiteiNo() As String
            Get
                Return _KeikakuzuInputZumenKaiteiNo
            End Get
            Set(ByVal value As String)
                _KeikakuzuInputZumenKaiteiNo = value
            End Set
        End Property

        ''' <summary>計画図種別</summary>
        ''' <value>計画図種別</value>
        ''' <returns>計画図種別</returns>
        Public Property KeikakuzuShubetsu() As String
            Get
                Return _KeikakuzuShubetsu
            End Get
            Set(ByVal value As String)
                _KeikakuzuShubetsu = value
            End Set
        End Property

        ''' <summary>計画図造型指示種別</summary>
        ''' <value>計画図造型指示種別</value>
        ''' <returns>計画図造型指示種別</returns>
        Public Property KeikakuzuZokeiShijiShubetsu() As String
            Get
                Return _KeikakuzuZokeiShijiShubetsu
            End Get
            Set(ByVal value As String)
                _KeikakuzuZokeiShijiShubetsu = value
            End Set
        End Property

        ''' <summary>計画図通知書</summary>
        ''' <value>計画図通知書</value>
        ''' <returns>計画図通知書</returns>
        Public Property KeikakuzuTsuchisho() As String
            Get
                Return _KeikakuzuTsuchisho
            End Get
            Set(ByVal value As String)
                _KeikakuzuTsuchisho = value
            End Set
        End Property

        ''' <summary>計画図回答要求期日</summary>
        ''' <value>計画図回答要求期日</value>
        ''' <returns>計画図回答要求期日</returns>
        Public Property KeikakuzuKaitoYokyuKijitsu() As String
            Get
                Return _KeikakuzuKaitoYokyuKijitsu
            End Get
            Set(ByVal value As String)
                _KeikakuzuKaitoYokyuKijitsu = value
            End Set
        End Property

        ''' <summary>計画図造型データリリース期日</summary>
        ''' <value>計画図造型データリリース期日</value>
        ''' <returns>計画図造型データリリース期日</returns>
        Public Property KeikakuzuZokeiDataReleaseKijitsu() As String
            Get
                Return _KeikakuzuZokeiDataReleaseKijitsu
            End Get
            Set(ByVal value As String)
                _KeikakuzuZokeiDataReleaseKijitsu = value
            End Set
        End Property

        ''' <summary>計画図回答要求先数</summary>
        ''' <value>計画図回答要求先数</value>
        ''' <returns>計画図回答要求先数</returns>
        Public Property KeikakuzuKaitoYokyusakiSuu() As String
            Get
                Return _KeikakuzuKaitoYokyusakiSuu
            End Get
            Set(ByVal value As String)
                _KeikakuzuKaitoYokyusakiSuu = value
            End Set
        End Property

        ''' <summary>計画図予定</summary>
        ''' <value>計画図予定</value>
        ''' <returns>計画図予定</returns>
        Public Property KeikakuzuYotei() As String
            Get
                Return _KeikakuzuYotei
            End Get
            Set(ByVal value As String)
                _KeikakuzuYotei = value
            End Set
        End Property

        ''' <summary>計画図実績</summary>
        ''' <value>計画図実績</value>
        ''' <returns>計画図実績</returns>
        Public Property KeikakuzuJisseki() As String
            Get
                Return _KeikakuzuJisseki
            End Get
            Set(ByVal value As String)
                _KeikakuzuJisseki = value
            End Set
        End Property

        ''' <summary>計画図出図CADエリア</summary>
        ''' <value>計画図出図CADエリア</value>
        ''' <returns>計画図出図CADエリア</returns>
        Public Property KeikakuzuShutsuzuCadArea() As String
            Get
                Return _KeikakuzuShutsuzuCadArea
            End Get
            Set(ByVal value As String)
                _KeikakuzuShutsuzuCadArea = value
            End Set
        End Property

        ''' <summary>計画図出図CADモデル</summary>
        ''' <value>計画図出図CADモデル</value>
        ''' <returns>計画図出図CADモデル</returns>
        Public Property KeikakuzuShutsuzuCadModel() As String
            Get
                Return _KeikakuzuShutsuzuCadModel
            End Get
            Set(ByVal value As String)
                _KeikakuzuShutsuzuCadModel = value
            End Set
        End Property

        ''' <summary>計画図出図備考</summary>
        ''' <value>計画図出図備考</value>
        ''' <returns>計画図出図備考</returns>
        Public Property KeikakuzuShutsuzuBiko() As String
            Get
                Return _KeikakuzuShutsuzuBiko
            End Get
            Set(ByVal value As String)
                _KeikakuzuShutsuzuBiko = value
            End Set
        End Property

        ''' <summary>計画図設計受領</summary>
        ''' <value>計画図設計受領</value>
        ''' <returns>計画図設計受領</returns>
        Public Property KeikakuzuSekkeiJuryo() As String
            Get
                Return _KeikakuzuSekkeiJuryo
            End Get
            Set(ByVal value As String)
                _KeikakuzuSekkeiJuryo = value
            End Set
        End Property

        ''' <summary>計画図設計備考</summary>
        ''' <value>計画図設計備考</value>
        ''' <returns>計画図設計備考</returns>
        Public Property KeikakuzuSekkeiBiko() As String
            Get
                Return _KeikakuzuSekkeiBiko
            End Get
            Set(ByVal value As String)
                _KeikakuzuSekkeiBiko = value
            End Set
        End Property

        ''' <summary>計画図回答依頼未回答部署</summary>
        ''' <value>計画図回答依頼未回答部署</value>
        ''' <returns>計画図回答依頼未回答部署</returns>
        Public Property KeikakuzuKaitoiraiMikaitoBusho() As String
            Get
                Return _KeikakuzuKaitoiraiMikaitoBusho
            End Get
            Set(ByVal value As String)
                _KeikakuzuKaitoiraiMikaitoBusho = value
            End Set
        End Property

        ''' <summary>計画図回答依頼回答</summary>
        ''' <value>計画図回答依頼回答</value>
        ''' <returns>計画図回答依頼回答</returns>
        Public Property KeikakuzuKaitoiraiKaito() As String
            Get
                Return _KeikakuzuKaitoiraiKaito
            End Get
            Set(ByVal value As String)
                _KeikakuzuKaitoiraiKaito = value
            End Set
        End Property

        ''' <summary>CE担当</summary>
        ''' <value>CE担当</value>
        ''' <returns>CE担当</returns>
        Public Property CeTanto() As String
            Get
                Return _CeTanto
            End Get
            Set(ByVal value As String)
                _CeTanto = value
            End Set
        End Property

        ''' <summary>FHI生産</summary>
        ''' <value>FHI生産</value>
        ''' <returns>FHI生産</returns>
        Public Property FhiSeisan() As String
            Get
                Return _FhiSeisan
            End Get
            Set(ByVal value As String)
                _FhiSeisan = value
            End Set
        End Property

        ''' <summary>SIA生産</summary>
        ''' <value>SIA生産</value>
        ''' <returns>SIA生産</returns>
        Public Property SiaSeisan() As String
            Get
                Return _SiaSeisan
            End Get
            Set(ByVal value As String)
                _SiaSeisan = value
            End Set
        End Property

        ''' <summary>SIACKD</summary>
        ''' <value>SIACKD</value>
        ''' <returns>SIACKD</returns>
        Public Property SiaCkd() As String
            Get
                Return _SiaCkd
            End Get
            Set(ByVal value As String)
                _SiaCkd = value
            End Set
        End Property

        ''' <summary>選定優先度</summary>
        ''' <value>選定優先度</value>
        ''' <returns>選定優先度</returns>
        Public Property SenteiYusendo() As String
            Get
                Return _SenteiYusendo
            End Get
            Set(ByVal value As String)
                _SenteiYusendo = value
            End Set
        End Property

        '必要なモノだけに入れておく'

        ''' <summary>取引先コード</summary>
        ''' <value>取引先コード</value>
        ''' <returns>取引先コード</returns>
        Public Property TorihikisakiCode() As String
            Get
                Return _TorihikisakiCode
            End Get
            Set(ByVal value As String)
                _TorihikisakiCode = value
            End Set
        End Property

        ''' <summary>FHIノミネート購担</summary>
        ''' <value>FHIノミネート購担</value>
        ''' <returns>FHIノミネート購担</returns>
        Public Property FhiNominateKotan() As String
            Get
                Return _FhiNominateKotan
            End Get
            Set(ByVal value As String)
                _FhiNominateKotan = value
            End Set
        End Property

    End Class
End Namespace