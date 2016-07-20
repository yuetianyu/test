Imports FarPoint.Win.Spread.CellType
Imports EventSakusei.ShisakuBuhinEdit.Ui
Imports ShisakuCommon.Ui
Imports ShisakuCommon.Ui.Spd

Namespace ShisakuBuhinEdit.Kosei.Ui
    ''' <summary>
    ''' 試作部品構成編集画面で使用するCellTypeを一意に管理するクラス
    ''' </summary>
    ''' <remarks></remarks>
    Public Class SpdKoseiCellTypeFactory
        '' レベル
        Private _LevelCellType As TextCellType
        '' 国内集計コード  
        Private _ShukeiCodeCellType As TextCellType
        '' 海外SIA集計コード  
        Private _SiaShukeiCodeCellType As TextCellType
        '' 現調CKD区分  
        Private _GencyoCkdKbnCellType As TextCellType
        '' 取引先コード  
        Private _MakerCodeCellType As TextCellType
        '' 取引先名称  
        Private _MakerNameCellType As TextCellType
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
        '' 再使用不可  
        Private _SaishiyoufukaCellType As TextCellType
        '' 供給セクション 2012/01/23 追加
        Private _KyoukuSectionCellType As TextCellType
        '' 出図予定日  
        Private _ShutuzuYoteiDateCellType As DateTimeCellType
        '' 材質・規格１  
        Private _ZaishituKikaku1CellType As TextCellType
        '' 材質・規格２  
        Private _ZaishituKikaku2CellType As TextCellType
        '' 材質・規格３  
        Private _ZaishituKikaku3CellType As TextCellType
        '' 材質・メッキ  
        Private _ZaishituMekkiCellType As TextCellType


        '' 作り方・製作方法
        Private _TsukurikataSeisakuCellType As TextCellType
        '' 作り方・工法
        Private _TsukurikataKouhouCellType As TextCellType
        '' 作り方・型仕様
        Private _TsukurikataKatashiyouCellType As TextCellType
        '' 作り方・冶具
        Private _TsukurikataTiguCellType As TextCellType
        '' 作り方・納入見直し
        Private _TsukurikataNounyuCellType As DateTimeCellType
        '' 作り方・部品製作規模・概要
        Private _TsukurikataKiboCellType As TextCellType


        '' 板厚・板厚       
        Private _ShisakuBankoSuryoCellType As TextCellType
        '' 板厚・ｕ
        Private _ShisakuBankoSuryoUCellType As TextCellType
        '' 試作部品費（円）  
        Private _ShisakuBuhinHiCellType As CurrencyCellType
        '' 試作型費（千円）  
        Private _ShisakuKataHiCellType As CurrencyCellType
        '' 備考  
        Private _BikouCellType As TextCellType
        '' 部品ノート（2012/01/25追加）  
        Private _BuhinNoteCellType As TextCellType

        '↓↓↓2014/12/24 メタル項目を追加 TES)張 ADD BEGIN
        '' 製品サイズ・製品長  
        Private _MaterialInfoLengthCellType As NumberCellType
        '' 製品サイズ・製品幅  
        Private _MaterialInfoWidthCellType As NumberCellType
        '' データ項目・改訂№
        Private _DataItemKaiteiNoCellType As TextCellType
        '' データ項目・エリア名  
        Private _DataItemAreaNameCellType As TextCellType
        '' データ項目・セット名  
        Private _DataItemSetNameCellType As TextCellType
        '' データ項目・改訂情報  
        Private _DataItemKaiteiInfoCellType As TextCellType
        '↑↑↑2014/12/24 メタル項目を追加 TES)張 ADD END

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

        ''' <summary>国内集計コード</summary>
        ''' <value>国内集計コード</value>
        ''' <returns>国内集計コード</returns>
        Public ReadOnly Property ShukeiCodeCellType() As TextCellType
            Get
                If _ShukeiCodeCellType Is Nothing Then

                    _ShukeiCodeCellType = ShisakuSpreadUtil.NewGeneralTextCellType()
                    _ShukeiCodeCellType.MaxLength = 1
                End If
                Return _ShukeiCodeCellType
            End Get
        End Property

        ''' <summary>海外SIA集計コード</summary>
        ''' <value>海外SIA集計コード</value>
        ''' <returns>海外SIA集計コード</returns>
        Public ReadOnly Property SiaShukeiCodeCellType() As TextCellType
            Get
                If _SiaShukeiCodeCellType Is Nothing Then

                    _SiaShukeiCodeCellType = ShisakuSpreadUtil.NewGeneralTextCellType()
                    _SiaShukeiCodeCellType.MaxLength = 1
                End If
                Return _SiaShukeiCodeCellType
            End Get
        End Property

        ''' <summary>現調CKD区分</summary>
        ''' <value>現調CKD区分</value>
        ''' <returns>現調CKD区分</returns>
        Public ReadOnly Property GencyoCkdKbnCellType() As TextCellType
            Get
                If _GencyoCkdKbnCellType Is Nothing Then

                    _GencyoCkdKbnCellType = ShisakuSpreadUtil.NewGeneralTextCellType()
                    _GencyoCkdKbnCellType.MaxLength = 1
                End If
                Return _GencyoCkdKbnCellType
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

        ''' <summary>取引先名称</summary>
        ''' <value>取引先名称</value>
        ''' <returns>取引先名称</returns>
        Public ReadOnly Property MakerNameCellType() As TextCellType
            Get
                If _MakerNameCellType Is Nothing Then

                    _MakerNameCellType = ShisakuSpreadUtil.NewGeneralTextCellType()
                    _MakerNameCellType.MaxLength = 51
                    '_MakerNameCellType.CharacterSet = CharacterSet.AllIME  '' 全角も半角も入力出来る
                End If
                Return _MakerNameCellType
            End Get
        End Property

        ''' <summary>部品番号</summary>
        ''' <value>部品番号</value>
        ''' <returns>部品番号</returns>
        Public ReadOnly Property BuhinNoCellType() As TextCellType
            Get
                If _BuhinNoCellType Is Nothing Then

                    _BuhinNoCellType = ShisakuSpreadUtil.NewGeneralTextCellType()
                    _BuhinNoCellType.MaxLength = 13
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
        ''' <summary>
        ''' 供給セクション
        ''' </summary>
        ''' <value>供給セクション</value>
        ''' <returns>供給セクション</returns>
        ''' <remarks>供給セクション</remarks>
        Public ReadOnly Property KyoukuSectionCellType() As TextCellType
            '2012/01/23 供給セクション追加
            Get
                If _KyoukuSectionCellType Is Nothing Then

                    _KyoukuSectionCellType = ShisakuSpreadUtil.NewGeneralTextCellType()
                    _KyoukuSectionCellType.MaxLength = 5
                End If
                Return _KyoukuSectionCellType
            End Get
        End Property

        ''' <summary>出図予定日</summary>
        ''' <value>出図予定日</value>
        ''' <returns>出図予定日</returns>
        Public ReadOnly Property ShutuzuYoteiDateCellType() As DateTimeCellType
            Get
                If _ShutuzuYoteiDateCellType Is Nothing Then
                    _ShutuzuYoteiDateCellType = BuhinEditSpreadUtil.NewGeneralDateTimeCellType
                End If
                Return _ShutuzuYoteiDateCellType
            End Get
        End Property

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
                        '.CharacterSet = CharacterSet.AllIME
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


        ''↓↓2014/07/23 Ⅰ.2.管理項目追加_t) (TES)張 ADD BEGIN
        ''' <summary>作り方・納入見直し</summary>
        ''' <value>作り方・納入見直し</value>
        ''' <returns>作り方・納入見直し</returns>
        Public ReadOnly Property TsukurikataNounyuCellType() As DateTimeCellType
            Get
                If _TsukurikataNounyuCellType Is Nothing Then
                    _TsukurikataNounyuCellType = BuhinEditSpreadUtil.NewGeneralDateTimeCellType
                End If
                Return _TsukurikataNounyuCellType
            End Get
        End Property
        ''↑↑2014/07/23 Ⅰ.2.管理項目追加_t) (TES)張 ADD END

        ''↓↓2014/07/23 Ⅰ.2.管理項目追加_u) (TES)張 ADD BEGIN
        ''' <summary>作り方・部品製作規模・概要</summary>
        ''' <value>作り方・部品製作規模・概要</value>
        ''' <returns>作り方・部品製作規模・概要</returns>
        Public ReadOnly Property TsukurikataKiboCellType() As TextCellType
            Get
                If _TsukurikataKiboCellType Is Nothing Then

                    _TsukurikataKiboCellType = ShisakuSpreadUtil.NewGeneralTextCellType()
                    _TsukurikataKiboCellType.MaxLength = 200

                End If
                Return _TsukurikataKiboCellType
            End Get
        End Property
        ''↑↑2014/07/23 Ⅰ.2.管理項目追加_u) (TES)張 ADD END

        '↓↓↓2014/12/24 メタル項目を追加 TES)張 ADD BEGIN
        ''' <summary>製品サイズ・製品長</summary>
        ''' <value>製品サイズ・製品長</value>
        ''' <returns>製品サイズ・製品長</returns>
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
        ''' <summary>製品サイズ・製品幅</summary>
        ''' <value>製品サイズ・製品幅</value>
        ''' <returns>製品サイズ・製品幅</returns>
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
        '↑↑↑2014/12/24 メタル項目を追加 TES)張 ADD END

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

        ''' <summary>備考</summary>
        ''' <value>備考</value>
        ''' <returns>備考</returns>
        Public ReadOnly Property BikouCellType() As TextCellType
            Get
                If _BikouCellType Is Nothing Then

                    _BikouCellType = ShisakuSpreadUtil.NewGeneralTextCellType()
                    _BikouCellType.MaxLength = 30
                    '_BikouCellType.CharacterSet = CharacterSet.AllIME  '' 全角も半角も入力出来る
                End If
                Return _BikouCellType
            End Get
        End Property

        ''' <summary>部品ノート</summary>
        ''' <value>部品ノート</value>
        ''' <returns>部品ノート</returns>
        Public ReadOnly Property BuhinNoteCellType() As TextCellType
            '2012/01/25 部品ノート追加
            Get
                If _BuhinNoteCellType Is Nothing Then

                    _BuhinNoteCellType = ShisakuSpreadUtil.NewGeneralTextCellType()
                    _BuhinNoteCellType.MaxLength = 51
                    '_BuhinNoteCellType.CharacterSet = CharacterSet.AllIME  '' 全角も半角も入力出来る
                End If
                Return _BuhinNoteCellType
            End Get
        End Property
    End Class
End Namespace