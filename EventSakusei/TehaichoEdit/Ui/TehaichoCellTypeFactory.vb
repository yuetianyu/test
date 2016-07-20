Imports FarPoint.Win.Spread.CellType
Imports EventSakusei.ShisakuBuhinEdit.Ui
Imports ShisakuCommon.Ui
Imports ShisakuCommon.Ui.Spd

Namespace TehaichoEdit.Ui
    ''' <summary>
    ''' 手配帳編集画面で使用するCellTypeを一意に管理するクラス
    ''' </summary>
    ''' <remarks></remarks>
    Public Class TehaichoCellTypeFactory
        '' 専用マーク
        Private _SenyouMarkCellType As TextCellType
        '' レベル
        Private _LevelCellType As TextCellType
        '' 部品番号  
        Private _BuhinNoCellType As TextCellType
        '' 部品番号試作区分  
        Private _BuhinNoKbnCellType As TextCellType
        '' 部品番号改訂No.  
        Private _BuhinNoKaiteiNoCellType As TextCellType
        '' 枝番  
        Private _EdaBanCellType As TextCellType
        '' 部品名称  
        Private _BuhinNameCellType As TextCellType
        '' 購担
        Private _KoutanCellType As TextCellType
        '' 取引先コード  
        Private _MakerCodeCellType As TextCellType
        '' 納場  
        Private _NoubaCellType As TextCellType
        '' 供給セクション
        Private _KyoukuSectionCellType As TextCellType
        '' 再使用不可
        Private _SaishiyoufukaCellType As TextCellType

        ''↓↓2014/08/25 Ⅰ.2.管理項目追加 酒井 ADD BEGIN
        Private _TsukurikataSeisakuCellType As TextCellType
        Private _TsukurikataKatashiyou1CellType As TextCellType
        Private _TsukurikataKatashiyou2CellType As TextCellType
        Private _TsukurikataKatashiyou3CellType As TextCellType
        Private _TsukurikataTiguCellType As TextCellType
        Private _TsukurikataNounyuCellType As DateTimeCellType
        Private _TsukurikataKiboCellType As TextCellType
        ''↑↑2014/08/25 Ⅰ.2.管理項目追加 酒井 ADD END

        '' 材質・規格１  
        Private _ZaishituKikaku1CellType As TextCellType
        '' 材質・規格２  
        Private _ZaishituKikaku2CellType As TextCellType
        '' 材質・規格３  
        Private _ZaishituKikaku3CellType As TextCellType
        '' 材質・メッキ  
        Private _ZaishituMekkiCellType As TextCellType
        '' 板厚・板厚       
        Private _ShisakuBankoSuryoCellType As TextCellType
        '' 板厚・ｕ
        Private _ShisakuBankoSuryoUCellType As TextCellType
        '' 試作部品費（円）  
        Private _ShisakuBuhinHiCellType As CurrencyCellType
        '' 試作型費（千円）  
        Private _ShisakuKataHiCellType As CurrencyCellType
        '' メーカー名  
        Private _MakerNameCellType As TextCellType
        '' 備考  
        Private _BikouCellType As TextCellType
        '' 親部品番号  
        Private _BuhinNoOyaCellType As TextCellType
        '' 親部品番号試作区分  
        Private _BuhinNoKbnOyaCellType As TextCellType
        '' 変化点  
        Private _HenkatenCellType As TextCellType
        '' 自動織込み改訂No
        Private _AutoOrikomiKaiteiNoCellType As TextCellType

        '↓↓↓2014/12/26 メタル項目を追加 TES)張 ADD BEGIN
        '' 材料情報・製品長  
        Private _MaterialInfoLengthCellType As NumberCellType
        '' 材料情報・製品幅  
        Private _MaterialInfoWidthCellType As NumberCellType
        '' 材料情報・発注対象
        Private _MaterialInfoOrderTargetCellType As CheckBoxCellType
        '' 材料情報・発注済
        Private _MaterialInfoOrderChkCellType As CheckBoxCellType
        '' データ項目・改訂№
        Private _DataItemKaiteiNoCellType As TextCellType
        '' データ項目・エリア名  
        Private _DataItemAreaNameCellType As TextCellType
        '' データ項目・セット名  
        Private _DataItemSetNameCellType As TextCellType
        '' データ項目・改訂情報  
        Private _DataItemKaiteiInfoCellType As TextCellType
        '' データ項目・データ支給チェック欄
        Private _DataItemDataProvisionCellType As CheckBoxCellType
        '↑↑↑2014/12/26 メタル項目を追加 TES)張 ADD END

        ''' <summary>専用マーク</summary>
        ''' <value>専用マーク</value>
        ''' <returns>専用マーク</returns>
        Public ReadOnly Property SenyouMarkCellType() As TextCellType
            Get
                If _SenyouMarkCellType Is Nothing Then

                    _SenyouMarkCellType = ShisakuSpreadUtil.NewGeneralTextCellType()
                    _SenyouMarkCellType.MaxLength = 1
                End If
                Return _SenyouMarkCellType
            End Get
        End Property

        ''' <summary>レベル</summary>
        ''' <value>レベル</value>
        ''' <returns>レベル</returns>
        Public ReadOnly Property LevelCellType() As TextCellType
            Get
                If _LevelCellType Is Nothing Then

                    _LevelCellType = ShisakuSpreadUtil.NewGeneralTextCellType()
                    _LevelCellType.MaxLength = 1
                    _LevelCellType.CharacterSet = CharacterSet.Numeric
                End If
                Return _LevelCellType
            End Get
        End Property

        ''' <summary>部品番号</summary>
        ''' <value>部品番号</value>
        ''' <returns>部品番号</returns>
        Public ReadOnly Property BuhinNoCellType() As TextCellType
            Get
                If _BuhinNoCellType Is Nothing Then

                    _BuhinNoCellType = ShisakuSpreadUtil.NewGeneralTextCellType()
                    _BuhinNoCellType.MaxLength = 15
                End If
                Return _BuhinNoCellType
            End Get
        End Property

        ''' <summary>部品番号試作区分</summary>
        ''' <value>部品番号試作区分</value>
        ''' <returns>部品番号試作区分</returns>
        Public ReadOnly Property BuhinNoKbnCellType() As TextCellType
            Get
                If _BuhinNoKbnCellType Is Nothing Then

                    _BuhinNoKbnCellType = ShisakuSpreadUtil.NewGeneralTextCellType()
                    _BuhinNoKbnCellType.MaxLength = 5
                End If
                Return _BuhinNoKbnCellType
            End Get
        End Property

        ''' <summary>部品番号改訂No.</summary>
        ''' <value>部品番号改訂No.</value>
        ''' <returns>部品番号改訂No.</returns>
        Public ReadOnly Property BuhinNoKaiteiNoCellType() As TextCellType
            Get
                If _BuhinNoKaiteiNoCellType Is Nothing Then

                    _BuhinNoKaiteiNoCellType = ShisakuSpreadUtil.NewGeneralTextCellType()
                    _BuhinNoKaiteiNoCellType.MaxLength = 2
                End If
                Return _BuhinNoKaiteiNoCellType
            End Get
        End Property

        ''' <summary>枝番</summary>
        ''' <value>枝番</value>
        ''' <returns>枝番</returns>
        Public ReadOnly Property EdaBanCellType() As TextCellType
            Get
                If _EdaBanCellType Is Nothing Then

                    _EdaBanCellType = ShisakuSpreadUtil.NewGeneralTextCellType()
                    _EdaBanCellType.MaxLength = 2
                End If
                Return _EdaBanCellType
            End Get
        End Property

        ''' <summary>部品名称</summary>
        ''' <value>部品名称</value>
        ''' <returns>部品名称</returns>
        Public ReadOnly Property BuhinNameCellType() As TextCellType
            Get
                If _BuhinNameCellType Is Nothing Then

                    _BuhinNameCellType = ShisakuSpreadUtil.NewGeneralTextCellType()
                    _BuhinNameCellType.MaxLength = 20
                End If
                Return _BuhinNameCellType
            End Get
        End Property

        ''' <summary>購担</summary>
        ''' <value>購担</value>
        ''' <returns>購担</returns>
        Public ReadOnly Property KoutanCellType() As TextCellType
            Get
                If _KoutanCellType Is Nothing Then

                    _KoutanCellType = ShisakuSpreadUtil.NewGeneralTextCellType()
                    _KoutanCellType.MaxLength = 2
                End If
                Return _KoutanCellType
            End Get
        End Property

        ''' <summary>取引先コード</summary>
        ''' <value>取引先コード</value>
        ''' <returns>取引先コード</returns>
        Public ReadOnly Property MakerCodeCellType() As TextCellType
            Get
                If _MakerCodeCellType Is Nothing Then

                    _MakerCodeCellType = ShisakuSpreadUtil.NewGeneralTextCellType()
                    _MakerCodeCellType.MaxLength = 4
                End If
                Return _MakerCodeCellType
            End Get
        End Property

        ''' <summary>納場</summary>
        ''' <value>納場</value>
        ''' <returns>納場</returns>
        Public ReadOnly Property NoubaCellType() As TextCellType
            Get
                If _NoubaCellType Is Nothing Then

                    _NoubaCellType = ShisakuSpreadUtil.NewGeneralTextCellType()
                    _NoubaCellType.MaxLength = 2
                End If
                Return _NoubaCellType
            End Get
        End Property

        ''' <summary>供給セクション</summary>
        ''' <value>供給セクション</value>
        ''' <returns>供給セクション</returns>
        Public ReadOnly Property KyoukuSectionCellType() As TextCellType
            Get
                If _KyoukuSectionCellType Is Nothing Then

                    _KyoukuSectionCellType = ShisakuSpreadUtil.NewGeneralTextCellType()
                    _KyoukuSectionCellType.MaxLength = 5
                End If
                Return _KyoukuSectionCellType
            End Get
        End Property

        ''' <summary>再使用不可</summary>
        ''' <value>再使用不可</value>
        ''' <returns>再使用不可</returns>
        Public ReadOnly Property SaishiyoufukaCellType() As TextCellType
            Get
                If _SaishiyoufukaCellType Is Nothing Then

                    _SaishiyoufukaCellType = ShisakuSpreadUtil.NewGeneralTextCellType()
                    _SaishiyoufukaCellType.MaxLength = 1
                End If
                Return _SaishiyoufukaCellType
            End Get
        End Property


        ''↓↓2014/08/25 Ⅰ.2.管理項目追加 酒井 ADD BEGIN

        Public ReadOnly Property TsukurikataKiboCellType() As TextCellType
            Get
                If _TsukurikataKiboCellType Is Nothing Then

                    _TsukurikataKiboCellType = ShisakuSpreadUtil.NewGeneralTextCellType()
                    _TsukurikataKiboCellType.MaxLength = 32
                End If
                Return _TsukurikataKiboCellType
            End Get
        End Property
        ''↑↑2014/08/25 Ⅰ.2.管理項目追加 酒井 ADD END

        ''' <summary>材質・規格１</summary>
        ''' <value>材質・規格１</value>
        ''' <returns>材質・規格１</returns>
        Public ReadOnly Property ZaishituKikaku1CellType() As TextCellType
            Get
                If _ZaishituKikaku1CellType Is Nothing Then

                    _ZaishituKikaku1CellType = ShisakuSpreadUtil.NewGeneralTextCellType()
                    _ZaishituKikaku1CellType.MaxLength = 4
                End If
                Return _ZaishituKikaku1CellType
            End Get
        End Property

        ''' <summary>材質・規格２</summary>
        ''' <value>材質・規格２</value>
        ''' <returns>材質・規格２</returns>
        Public ReadOnly Property ZaishituKikaku2CellType() As TextCellType
            Get
                If _ZaishituKikaku2CellType Is Nothing Then

                    _ZaishituKikaku2CellType = ShisakuSpreadUtil.NewGeneralTextCellType()
                    _ZaishituKikaku2CellType.MaxLength = 4
                End If
                Return _ZaishituKikaku2CellType
            End Get
        End Property

        ''' <summary>材質・規格３</summary>
        ''' <value>材質・規格３</value>
        ''' <returns>材質・規格３</returns>
        Public ReadOnly Property ZaishituKikaku3CellType() As TextCellType
            Get
                If _ZaishituKikaku3CellType Is Nothing Then

                    _ZaishituKikaku3CellType = ShisakuSpreadUtil.NewGeneralTextCellType()
                    _ZaishituKikaku3CellType.MaxLength = 2
                End If
                Return _ZaishituKikaku3CellType
            End Get
        End Property

        ''' <summary>材質・メッキ</summary>
        ''' <value>材質・メッキ</value>
        ''' <returns>材質・メッキ</returns>
        Public ReadOnly Property ZaishituMekkiCellType() As TextCellType
            Get
                If _ZaishituMekkiCellType Is Nothing Then

                    _ZaishituMekkiCellType = ShisakuSpreadUtil.NewGeneralTextCellType()
                    With _ZaishituMekkiCellType
                        .MaxLength = 6
                        .CharacterSet = CharacterSet.AllIME
                    End With
                End If
                Return _ZaishituMekkiCellType
            End Get
        End Property

        ''' <summary>板厚・板厚</summary>
        ''' <value>板厚・板厚</value>
        ''' <returns>板厚・板厚</returns>
        Public ReadOnly Property ShisakuBankoSuryoCellType() As TextCellType
            Get
                If _ShisakuBankoSuryoCellType Is Nothing Then

                    _ShisakuBankoSuryoCellType = ShisakuSpreadUtil.NewGeneralTextCellType()
                    With _ShisakuBankoSuryoCellType
                        .MaxLength = 5
                        .CharacterCasing = CharacterCasing.Normal   ' 板厚は小文字ok
                    End With
                End If
                Return _ShisakuBankoSuryoCellType
            End Get
        End Property

        ''' <summary>板厚・ｕ</summary>
        ''' <value>板厚・ｕ</value>
        ''' <returns>板厚・ｕ</returns>
        Public ReadOnly Property ShisakuBankoSuryoUCellType() As TextCellType
            Get
                If _ShisakuBankoSuryoUCellType Is Nothing Then

                    _ShisakuBankoSuryoUCellType = ShisakuSpreadUtil.NewGeneralTextCellType()
                    With _ShisakuBankoSuryoUCellType
                        .MaxLength = 1
                        .CharacterCasing = CharacterCasing.Normal   ' 板厚は小文字ok
                    End With
                End If
                Return _ShisakuBankoSuryoUCellType
            End Get
        End Property

        ''' <summary>試作部品費（円）</summary>
        ''' <value>試作部品費（円）</value>
        ''' <returns>試作部品費（円）</returns>
        Public ReadOnly Property ShisakuBuhinHiCellType() As CurrencyCellType
            Get
                If _ShisakuBuhinHiCellType Is Nothing Then
                    _ShisakuBuhinHiCellType = ShisakuSpreadUtil.NewGeneralCurrencyCellType
                    With _ShisakuBuhinHiCellType
                        .MaximumValue = 9999999
                        .MinimumValue = 0
                        .DecimalPlaces = 0
                    End With
                End If
                Return _ShisakuBuhinHiCellType
            End Get
        End Property

        ''' <summary>試作型費（千円）</summary>
        ''' <value>試作型費（千円）</value>
        ''' <returns>試作型費（千円）</returns>
        Public ReadOnly Property ShisakuKataHiCellType() As CurrencyCellType
            Get
                If _ShisakuKataHiCellType Is Nothing Then
                    _ShisakuKataHiCellType = ShisakuSpreadUtil.NewGeneralCurrencyCellType
                    With _ShisakuKataHiCellType
                        .MaximumValue = 999999
                        .MinimumValue = 0
                        .DecimalPlaces = 0
                    End With
                End If
                Return _ShisakuKataHiCellType
            End Get
        End Property

        ''' <summary>取引先名称</summary>
        ''' <value>取引先名称</value>
        ''' <returns>取引先名称</returns>
        Public ReadOnly Property MakerNameCellType() As TextCellType
            Get
                If _MakerNameCellType Is Nothing Then

                    _MakerNameCellType = ShisakuSpreadUtil.NewGeneralTextCellType()
                    _MakerNameCellType.MaxLength = 102
                    _MakerNameCellType.CharacterSet = CharacterSet.AllIME  '' 全角も半角も入力出来る
                End If
                Return _MakerNameCellType
            End Get
        End Property

        ''' <summary>備考</summary>
        ''' <value>備考</value>
        ''' <returns>備考</returns>
        Public ReadOnly Property BikouCellType() As TextCellType
            Get
                If _BikouCellType Is Nothing Then

                    _BikouCellType = ShisakuSpreadUtil.NewGeneralTextCellType()
                    _BikouCellType.MaxLength = 60
                    _BikouCellType.CharacterSet = CharacterSet.AllIME  '' 全角も半角も入力出来る
                End If
                Return _BikouCellType
            End Get
        End Property

        ''' <summary>部品番号親</summary>
        ''' <value>部品番号親</value>
        ''' <returns>部品番号親</returns>
        Public ReadOnly Property BuhinNoOyaCellType() As TextCellType
            Get
                If _BuhinNoOyaCellType Is Nothing Then

                    _BuhinNoOyaCellType = ShisakuSpreadUtil.NewGeneralTextCellType()
                    _BuhinNoOyaCellType.MaxLength = 13
                End If
                Return _BuhinNoOyaCellType
            End Get
        End Property

        ''' <summary>部品番号試作区分親</summary>
        ''' <value>部品番号試作区分親</value>
        ''' <returns>部品番号試作区分親</returns>
        Public ReadOnly Property BuhinNoKbnOyaCellType() As TextCellType
            Get
                If _BuhinNoKbnOyaCellType Is Nothing Then

                    _BuhinNoKbnOyaCellType = ShisakuSpreadUtil.NewGeneralTextCellType()
                    _BuhinNoKbnOyaCellType.MaxLength = 5
                End If
                Return _BuhinNoKbnOyaCellType
            End Get
        End Property

        ''' <summary>変化点</summary>
        ''' <value>変化点</value>
        ''' <returns>変化点</returns>
        Public ReadOnly Property HenkatenCellType() As TextCellType
            Get
                If _HenkatenCellType Is Nothing Then

                    _HenkatenCellType = ShisakuSpreadUtil.NewGeneralTextCellType()
                    _HenkatenCellType.MaxLength = 1
                End If
                Return _HenkatenCellType
            End Get
        End Property

        ''' <summary>自動織込み改訂No</summary>
        ''' <value>自動織込み改訂No</value>
        ''' <returns>自動織込み改訂No</returns>
        Public ReadOnly Property AutoOrikomiKaiteiNo() As TextCellType
            Get
                If _AutoOrikomiKaiteiNoCellType Is Nothing Then

                    _AutoOrikomiKaiteiNoCellType = ShisakuSpreadUtil.NewGeneralTextCellType()
                    _AutoOrikomiKaiteiNoCellType.MaxLength = 3
                End If
                Return _AutoOrikomiKaiteiNoCellType
            End Get
        End Property

        '↓↓↓2014/12/26 メタル項目を追加 TES)張 ADD BEGIN
        ''' <summary>材料情報・製品長</summary>
        ''' <value>材料情報・製品長</value>
        ''' <returns>材料情報・製品長</returns>
        Public ReadOnly Property MaterialInfoLengthCellType() As NumberCellType
            Get
                If _MaterialInfoLengthCellType Is Nothing Then
                    _MaterialInfoLengthCellType = ShisakuSpreadUtil.NewGeneralNumberCellType
                    With _MaterialInfoLengthCellType
                        .MaximumValue = 9999
                        .MinimumValue = 0
                        .DecimalPlaces = 0
                    End With
                End If
                Return _MaterialInfoLengthCellType
            End Get
        End Property
        ''' <summary>材料情報・製品幅</summary>
        ''' <value>材料情報・製品幅</value>
        ''' <returns>材料情報・製品幅</returns>
        Public ReadOnly Property MaterialInfoWidthCellType() As NumberCellType
            Get
                If _MaterialInfoWidthCellType Is Nothing Then
                    _MaterialInfoWidthCellType = ShisakuSpreadUtil.NewGeneralNumberCellType
                    With _MaterialInfoWidthCellType
                        .MaximumValue = 9999
                        .MinimumValue = 0
                        .DecimalPlaces = 0
                    End With
                End If
                Return _MaterialInfoWidthCellType
            End Get
        End Property
        ''' <summary>材料情報・発注対象</summary>
        ''' <value>材料情報・発注対象</value>
        ''' <returns>材料情報・発注対象</returns>
        Public ReadOnly Property MaterialInfoOrderTargetCellType() As CheckBoxCellType
            Get
                If _MaterialInfoOrderTargetCellType Is Nothing Then
                    _MaterialInfoOrderTargetCellType = ShisakuSpreadUtil.NewGeneralCheckBoxCellType
                End If
                Return _MaterialInfoOrderTargetCellType
            End Get
        End Property
        ''' <summary>材料情報・発注済</summary>
        ''' <value>材料情報・発注済</value>
        ''' <returns>材料情報・発注済</returns>
        Public ReadOnly Property MaterialInfoOrderChkCellType() As CheckBoxCellType
            Get
                If _MaterialInfoOrderChkCellType Is Nothing Then
                    _MaterialInfoOrderChkCellType = ShisakuSpreadUtil.NewGeneralCheckBoxCellType
                End If
                Return _MaterialInfoOrderChkCellType
            End Get
        End Property
        ''' <summary>データ項目・改訂№</summary>
        ''' <value>データ項目・改訂№</value>
        ''' <returns>データ項目・改訂№</returns>
        Public ReadOnly Property DataItemKaiteiNoCellType() As TextCellType
            Get
                If _DataItemKaiteiNoCellType Is Nothing Then
                    _DataItemKaiteiNoCellType = ShisakuSpreadUtil.NewGeneralTextCellType
                    _DataItemKaiteiNoCellType.MaxLength = 5
                End If
                Return _DataItemKaiteiNoCellType
            End Get
        End Property
        ''' <summary>データ項目・エリア名</summary>
        ''' <value>データ項目・エリア名</value>
        ''' <returns>データ項目・エリア名</returns>
        Public ReadOnly Property DataItemAreaNameCellType() As TextCellType
            Get
                If _DataItemAreaNameCellType Is Nothing Then
                    _DataItemAreaNameCellType = ShisakuSpreadUtil.NewGeneralTextCellType
                    _DataItemAreaNameCellType.MaxLength = 256
                End If
                Return _DataItemAreaNameCellType
            End Get
        End Property
        ''' <summary>データ項目・セット名</summary>
        ''' <value>データ項目・セット名</value>
        ''' <returns>データ項目・セット名</returns>
        Public ReadOnly Property DataItemSetNameCellType() As TextCellType
            Get
                If _DataItemSetNameCellType Is Nothing Then
                    _DataItemSetNameCellType = ShisakuSpreadUtil.NewGeneralTextCellType
                    _DataItemSetNameCellType.MaxLength = 256
                End If
                Return _DataItemSetNameCellType
            End Get
        End Property
        ''' <summary>データ項目・改訂情報</summary>
        ''' <value>データ項目・改訂情報</value>
        ''' <returns>データ項目・改訂情報</returns>
        Public ReadOnly Property DataItemKaiteiInfoCellType() As TextCellType
            Get
                If _DataItemKaiteiInfoCellType Is Nothing Then
                    _DataItemKaiteiInfoCellType = ShisakuSpreadUtil.NewGeneralTextCellType
                    _DataItemKaiteiInfoCellType.MaxLength = 256
                End If
                Return _DataItemKaiteiInfoCellType
            End Get
        End Property
        ''' <summary>データ項目・データ支給チェック欄</summary>
        ''' <value>データ項目・データ支給チェック欄</value>
        ''' <returns>データ項目・データ支給チェック欄</returns>
        Public ReadOnly Property DataItemDataProvisionCellType() As CheckBoxCellType
            Get
                If _DataItemDataProvisionCellType Is Nothing Then
                    _DataItemDataProvisionCellType = ShisakuSpreadUtil.NewGeneralCheckBoxCellType
                End If
                Return _DataItemDataProvisionCellType
            End Get
        End Property
        '↑↑↑2014/12/26 メタル項目を追加 TES)張 ADD END


        '' 出図実績_改訂№
        Private _ShutuzuJisekiKaiteiNoCellType As TextCellType
        ''' <summary>出図実績_改訂№</summary>
        ''' <value>出図実績_改訂№</value>
        ''' <returns>出図実績_改訂№</returns>
        Public ReadOnly Property ShutuzuJisekiKaiteiNoCellType() As TextCellType
            Get
                If _ShutuzuJisekiKaiteiNoCellType Is Nothing Then
                    _ShutuzuJisekiKaiteiNoCellType = ShisakuSpreadUtil.NewGeneralTextCellType
                    _ShutuzuJisekiKaiteiNoCellType.MaxLength = 2
                End If
                Return _ShutuzuJisekiKaiteiNoCellType
            End Get
        End Property
        '' 出図実績_設通№
        Private _ShutuzuJisekiStsrDhstbaCellType As TextCellType
        ''' <summary>出図実績_設通№</summary>
        ''' <value>出図実績_設通№</value>
        ''' <returns>出図実績_設通№</returns>
        Public ReadOnly Property ShutuzuJisekiStsrDhstbaCellType() As TextCellType
            Get
                If _ShutuzuJisekiStsrDhstbaCellType Is Nothing Then
                    _ShutuzuJisekiStsrDhstbaCellType = ShisakuSpreadUtil.NewGeneralTextCellType
                    _ShutuzuJisekiStsrDhstbaCellType.MaxLength = 10
                End If
                Return _ShutuzuJisekiStsrDhstbaCellType
            End Get
        End Property
        '' 最終織込設変情報_改訂№
        Private _SaisyuSetsuhenKaiteiNoCellType As TextCellType
        ''' <summary>最終織込設変情報_改訂№</summary>
        ''' <value>最終織込設変情報_改訂№</value>
        ''' <returns>最終織込設変情報_改訂№</returns>
        Public ReadOnly Property SaisyuSetsuhenKaiteiNoCellType() As TextCellType
            Get
                If _SaisyuSetsuhenKaiteiNoCellType Is Nothing Then
                    _SaisyuSetsuhenKaiteiNoCellType = ShisakuSpreadUtil.NewGeneralTextCellType
                    _SaisyuSetsuhenKaiteiNoCellType.MaxLength = 2
                End If
                Return _SaisyuSetsuhenKaiteiNoCellType
            End Get
        End Property
        '' 最終織込設変情報_設通№
        Private _SaisyuSetsuhenStsrDhstbaCellType As TextCellType
        ''' <summary>最終織込設変情報_設通№</summary>
        ''' <value>最終織込設変情報_設通№</value>
        ''' <returns>最終織込設変情報_設通№</returns>
        Public ReadOnly Property SaisyuSetsuhenStsrDhstbaCellType() As TextCellType
            Get
                If _SaisyuSetsuhenStsrDhstbaCellType Is Nothing Then
                    _SaisyuSetsuhenStsrDhstbaCellType = ShisakuSpreadUtil.NewGeneralTextCellType
                    _SaisyuSetsuhenStsrDhstbaCellType.MaxLength = 10
                End If
                Return _SaisyuSetsuhenStsrDhstbaCellType
            End Get
        End Property
        '' 材料寸法_X(mm)
        Private _ZairyoSunpoXCellType As NumberCellType
        ''' <summary>材料寸法_X(mm)</summary>
        ''' <value>材料寸法_X(mm)</value>
        ''' <returns>材料寸法_X(mm)</returns>
        Public ReadOnly Property ZairyoSunpoXCellType() As NumberCellType
            Get
                If _ZairyoSunpoXCellType Is Nothing Then
                    _ZairyoSunpoXCellType = ShisakuSpreadUtil.NewGeneralNumberCellType
                    With _ZairyoSunpoXCellType
                        .MaximumValue = 99999999
                        .MinimumValue = 0
                        .DecimalPlaces = 2
                    End With
                End If
                Return _ZairyoSunpoXCellType
            End Get
        End Property
        '' 材料寸法_Y(mm)
        Private _ZairyoSunpoYCellType As NumberCellType
        ''' <summary>材料寸法_Y(mm)</summary>
        ''' <value>材料寸法_Y(mm)</value>
        ''' <returns>材料寸法_Y(mm)</returns>
        Public ReadOnly Property ZairyoSunpoYCellType() As NumberCellType
            Get
                If _ZairyoSunpoYCellType Is Nothing Then
                    _ZairyoSunpoYCellType = ShisakuSpreadUtil.NewGeneralNumberCellType
                    With _ZairyoSunpoYCellType
                        .MaximumValue = 99999999
                        .MinimumValue = 0
                        .DecimalPlaces = 2
                    End With
                End If
                Return _ZairyoSunpoYCellType
            End Get
        End Property
        '' 材料寸法_Z(mm)
        Private _ZairyoSunpoZCellType As NumberCellType
        ''' <summary>材料寸法_Z(mm)</summary>
        ''' <value>材料寸法_Z(mm)</value>
        ''' <returns>材料寸法_Z(mm)</returns>
        Public ReadOnly Property ZairyoSunpoZCellType() As NumberCellType
            Get
                If _ZairyoSunpoZCellType Is Nothing Then
                    _ZairyoSunpoZCellType = ShisakuSpreadUtil.NewGeneralNumberCellType
                    With _ZairyoSunpoZCellType
                        .MaximumValue = 99999999
                        .MinimumValue = 0
                        .DecimalPlaces = 2
                    End With
                End If
                Return _ZairyoSunpoZCellType
            End Get
        End Property
        '' 材料寸法_X+Y(mm)
        Private _ZairyoSunpoXyCellType As NumberCellType
        ''' <summary>材料寸法_X+Y(mm)</summary>
        ''' <value>材料寸法_X+Y(mm)</value>
        ''' <returns>材料寸法_X+Y(mm)</returns>
        Public ReadOnly Property ZairyoSunpoXyCellType() As NumberCellType
            Get
                If _ZairyoSunpoXyCellType Is Nothing Then
                    _ZairyoSunpoXyCellType = ShisakuSpreadUtil.NewGeneralNumberCellType
                    With _ZairyoSunpoXyCellType
                        .MaximumValue = 99999999
                        .MinimumValue = 0
                        .DecimalPlaces = 2
                    End With
                End If
                Return _ZairyoSunpoXyCellType
            End Get
        End Property
        '' 材料寸法_X+Z(mm)
        Private _ZairyoSunpoXzCellType As NumberCellType
        ''' <summary>材料寸法_X+Z(mm)</summary>
        ''' <value>材料寸法_X+Z(mm)</value>
        ''' <returns>材料寸法_X+Z(mm)</returns>
        Public ReadOnly Property ZairyoSunpoXzCellType() As NumberCellType
            Get
                If _ZairyoSunpoXzCellType Is Nothing Then
                    _ZairyoSunpoXzCellType = ShisakuSpreadUtil.NewGeneralNumberCellType
                    With _ZairyoSunpoXzCellType
                        .MaximumValue = 99999999
                        .MinimumValue = 0
                        .DecimalPlaces = 2
                    End With
                End If
                Return _ZairyoSunpoXzCellType
            End Get
        End Property
        '' 材料寸法_Y+Z(mm)
        Private _ZairyoSunpoYzCellType As NumberCellType
        ''' <summary>材料寸法_Y+Z(mm)</summary>
        ''' <value>材料寸法_Y+Z(mm)</value>
        ''' <returns>材料寸法_Y+Z(mm)</returns>
        Public ReadOnly Property ZairyoSunpoYzCellType() As NumberCellType
            Get
                If _ZairyoSunpoYzCellType Is Nothing Then
                    _ZairyoSunpoYzCellType = ShisakuSpreadUtil.NewGeneralNumberCellType
                    With _ZairyoSunpoYzCellType
                        .MaximumValue = 99999999
                        .MinimumValue = 0
                        .DecimalPlaces = 2
                    End With
                End If
                Return _ZairyoSunpoYzCellType
            End Get
        End Property


    End Class
End Namespace