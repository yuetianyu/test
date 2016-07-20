Imports FarPoint.Win.Spread.CellType
Imports EventSakusei.ShisakuBuhinEdit.Ui
Imports ShisakuCommon.Ui
Imports ShisakuCommon.Ui.Spd

Namespace YosanSetteiBuhinEdit.Ui
    ''' <summary>
    ''' 手配帳編集画面で使用するCellTypeを一意に管理するクラス
    ''' </summary>
    ''' <remarks></remarks>
    Public Class YosanSetteiBuhinEditCellTypeFactory
        '' フラグ
        Private _AudFlagCellType As TextCellType
        '' レベル
        Private _YosanLevelCellType As TextCellType
        '' 国内集計コード
        Private _YosanShukeiCodeCellType As TextCellType
        '' 海外集計コード
        Private _YosanSiaShukeiCodeCellType As TextCellType
        '' 部品番号  
        Private _YosanBuhinNoCellType As TextCellType
        '' 部品名称  
        Private _YosanBuhinNameCellType As TextCellType
        '' 合計員数
        Private _YosanInsuCellType As TextCellType
        '' 取引先コード
        Private _YosanMakerCodeCellType As TextCellType
        '' 供給セクション
        Private _YosanKyoukuSectionCellType As TextCellType
        '' 購担
        Private _YosanKoutanCellType As TextCellType
        '' 手配記号
        Private _YosanTehaiKigouCellType As TextCellType
        '' 設計情報_作り方・製作方法
        Private _YosanTsukurikataSeisakuCellType As TextCellType
        '' 設計情報_作り方・型仕様_1
        Private _YosanTsukurikataKatashiyou1CellType As TextCellType
        '' 設計情報_作り方・型仕様_2
        Private _YosanTsukurikataKatashiyou2CellType As TextCellType
        '' 設計情報_作り方・型仕様_3
        Private _YosanTsukurikataKatashiyou3CellType As TextCellType
        '' 設計情報_作り方・治具
        Private _YosanTsukurikataTiguCellType As TextCellType
        '' 設計情報_作り方・部品製作規模・概要
        Private _YosanTsukurikataKiboCellType As TextCellType
        '' 設計情報_試作部品費（円）
        Private _YosanShisakuBuhinHiCellType As CurrencyCellType
        '' 設計情報_試作型費（千円）
        Private _YosanShisakuKataHiCellType As CurrencyCellType
        '' 設計情報_部品ノート
        Private _YosanBuhinNoteCellType As TextCellType
        '' 設計情報_備考
        Private _YosanBikouCellType As TextCellType
        '' 部品費根拠_国外区分
        Private _YosanKonkyoKokugaiKbnCellType As TextCellType
        '' 部品費根拠_MIXコスト部品費(円/ｾﾝﾄ)
        Private _YosanKonkyoMixBuhinHiCellType As CurrencyCellType
        '' 部品費根拠_引用元MIX値部品費
        Private _YosanKonkyoInyouMixBuhinHiCellType As TextCellType
        '' 部品費根拠_係数１
        Private _YosanKonkyoKeisu1CellType As NumberCellType
        '' 部品費根拠_工法
        Private _YosanKonkyoKouhouCellType As TextCellType
        '' 割付予算_部品費(円)
        Private _YosanWaritukeBuhinHiCellType As CurrencyCellType
        '' 部品費根拠_係数２
        Private _YosanWaritukeKeisu2CellType As NumberCellType
        '' 割付予算_部品費合計(円)
        Private _YosanWaritukeBuhinHiTotalCellType As CurrencyCellType
        '' 割付予算_型費(千円)
        Private _YosanWaritukeKataHiCellType As CurrencyCellType
        '' 購入希望_購入希望単価(円)
        Private _YosanKounyuKibouTankaCellType As CurrencyCellType
        '' 購入希望_部品費(円)
        Private _YosanKounyuKibouBuhinHiCellType As CurrencyCellType
        '' 購入希望_部品費合計(円)
        Private _YosanKounyuKibouBuhinHiTotalCellType As CurrencyCellType
        '' 購入希望_型費(円)
        Private _YosanKounyuKibouKataHiCellType As CurrencyCellType
        '' 予算汎用
        Private _YosanDecimalCellType As CurrencyCellType

        Private _KakoKojiShireiNoCellType As TextCellType
        Private _KakoEventNameCellType As TextCellType
        Private _KakoHacchuDateCellType As TextCellType
        Private _KakoKenshuDateCellType As TextCellType

        '' 変化点  
        Private _HenkatenCellType As TextCellType

        ''' <summary>フラグ</summary>
        ''' <value>フラグ</value>
        ''' <returns>フラグ</returns>
        Public ReadOnly Property AudFlagCellType() As TextCellType
            Get
                If _AudFlagCellType Is Nothing Then

                    _AudFlagCellType = ShisakuSpreadUtil.NewGeneralTextCellType()
                    _AudFlagCellType.MaxLength = 1
                End If
                Return _AudFlagCellType
            End Get
        End Property

        ''' <summary>レベル</summary>
        ''' <value>レベル</value>
        ''' <returns>レベル</returns>
        Public ReadOnly Property YosanLevelCellType() As TextCellType
            Get
                If _YosanLevelCellType Is Nothing Then

                    _YosanLevelCellType = ShisakuSpreadUtil.NewGeneralTextCellType()
                    _YosanLevelCellType.MaxLength = 1
                    _YosanLevelCellType.CharacterSet = CharacterSet.Numeric
                End If
                Return _YosanLevelCellType
            End Get
        End Property

        ''' <summary>国内集計コード</summary>
        ''' <value>国内集計コード</value>
        ''' <returns>国内集計コード</returns>
        Public ReadOnly Property YosanShukeiCodeCellType() As TextCellType
            Get
                If _YosanShukeiCodeCellType Is Nothing Then

                    _YosanShukeiCodeCellType = ShisakuSpreadUtil.NewGeneralTextCellType()
                    _YosanShukeiCodeCellType.MaxLength = 1
                End If
                Return _YosanShukeiCodeCellType
            End Get
        End Property

        ''' <summary>海外集計コード</summary>
        ''' <value>海外集計コード</value>
        ''' <returns>海外集計コード</returns>
        Public ReadOnly Property YosanSiaShukeiCodeCellType() As TextCellType
            Get
                If _YosanSiaShukeiCodeCellType Is Nothing Then

                    _YosanSiaShukeiCodeCellType = ShisakuSpreadUtil.NewGeneralTextCellType()
                    _YosanSiaShukeiCodeCellType.MaxLength = 1
                End If
                Return _YosanSiaShukeiCodeCellType
            End Get
        End Property

        ''' <summary>部品番号</summary>
        ''' <value>部品番号</value>
        ''' <returns>部品番号</returns>
        Public ReadOnly Property YosanBuhinNoCellType() As TextCellType
            Get
                If _YosanBuhinNoCellType Is Nothing Then

                    _YosanBuhinNoCellType = ShisakuSpreadUtil.NewGeneralTextCellType()
                    _YosanBuhinNoCellType.MaxLength = 15
                End If
                Return _YosanBuhinNoCellType
            End Get
        End Property

        ''' <summary>部品名称</summary>
        ''' <value>部品名称</value>
        ''' <returns>部品名称</returns>
        Public ReadOnly Property YosanBuhinNameCellType() As TextCellType
            Get
                If _YosanBuhinNameCellType Is Nothing Then

                    _YosanBuhinNameCellType = ShisakuSpreadUtil.NewGeneralTextCellType()
                    _YosanBuhinNameCellType.MaxLength = 30
                End If
                Return _YosanBuhinNameCellType
            End Get
        End Property

        ''' <summary>合計員数</summary>
        ''' <value>合計員数</value>
        ''' <returns>合計員数</returns>
        Public ReadOnly Property YosanInsuCellType() As TextCellType
            Get
                If _YosanInsuCellType Is Nothing Then
                    _YosanInsuCellType = ShisakuSpreadUtil.NewGeneralTextCellType()
                    _YosanInsuCellType.MaxLength = 4
                End If
                Return _YosanInsuCellType
            End Get
        End Property

        ''' <summary>取引先コード</summary>
        ''' <value>取引先コード</value>
        ''' <returns>取引先コード</returns>
        Public ReadOnly Property YosanMakerCodeCellType() As TextCellType
            Get
                If _YosanMakerCodeCellType Is Nothing Then

                    _YosanMakerCodeCellType = ShisakuSpreadUtil.NewGeneralTextCellType()
                    _YosanMakerCodeCellType.MaxLength = 4
                End If
                Return _YosanMakerCodeCellType
            End Get
        End Property

        ''' <summary>供給セクション</summary>
        ''' <value>供給セクション</value>
        ''' <returns>供給セクション</returns>
        Public ReadOnly Property YosanKyoukuSectionCellType() As TextCellType
            Get
                If _YosanKyoukuSectionCellType Is Nothing Then

                    _YosanKyoukuSectionCellType = ShisakuSpreadUtil.NewGeneralTextCellType()
                    _YosanKyoukuSectionCellType.MaxLength = 5
                End If
                Return _YosanKyoukuSectionCellType
            End Get
        End Property

        ''' <summary>購担</summary>
        ''' <value>購担</value>
        ''' <returns>購担</returns>
        Public ReadOnly Property YosanKoutanCellType() As TextCellType
            Get
                If _YosanKoutanCellType Is Nothing Then

                    _YosanKoutanCellType = ShisakuSpreadUtil.NewGeneralTextCellType()
                    _YosanKoutanCellType.MaxLength = 2
                End If
                Return _YosanKoutanCellType
            End Get
        End Property

        ''' <summary>手配記号</summary>
        ''' <value>手配記号</value>
        ''' <returns>手配記号</returns>
        Public ReadOnly Property YosanTehaiKigouCellType() As TextCellType
            Get
                If _YosanTehaiKigouCellType Is Nothing Then

                    _YosanTehaiKigouCellType = ShisakuSpreadUtil.NewGeneralTextCellType()
                    _YosanTehaiKigouCellType.MaxLength = 1
                End If
                Return _YosanTehaiKigouCellType
            End Get
        End Property

        ''' <summary>設計情報_作り方・部品製作規模・概要</summary>
        ''' <value>設計情報_作り方・部品製作規模・概要</value>
        ''' <returns>設計情報_作り方・部品製作規模・概要</returns>
        Public ReadOnly Property YosanTsukurikataKiboCellType() As TextCellType
            Get
                If _YosanTsukurikataKiboCellType Is Nothing Then

                    _YosanTsukurikataKiboCellType = ShisakuSpreadUtil.NewGeneralTextCellType()
                    _YosanTsukurikataKiboCellType.MaxLength = 200
                End If
                Return _YosanTsukurikataKiboCellType
            End Get
        End Property

        ''' <summary>試作部品費（円）</summary>
        ''' <value>試作部品費（円）</value>
        ''' <returns>試作部品費（円）</returns>
        Public ReadOnly Property YosanShisakuBuhinHiCellType() As CurrencyCellType
            Get
                If _YosanShisakuBuhinHiCellType Is Nothing Then
                    _YosanShisakuBuhinHiCellType = ShisakuSpreadUtil.NewGeneralCurrencyCellType
                    With _YosanShisakuBuhinHiCellType
                        .MaximumValue = 9999999
                        .MinimumValue = 0
                        .DecimalPlaces = 0
                    End With
                End If
                Return _YosanShisakuBuhinHiCellType
            End Get
        End Property

        ''' <summary>試作型費（千円）</summary>
        ''' <value>試作型費（千円）</value>
        ''' <returns>試作型費（千円）</returns>
        Public ReadOnly Property YosanShisakuKataHiCellType() As CurrencyCellType
            Get
                If _YosanShisakuKataHiCellType Is Nothing Then
                    _YosanShisakuKataHiCellType = ShisakuSpreadUtil.NewGeneralCurrencyCellType
                    With _YosanShisakuKataHiCellType
                        .MaximumValue = 999999
                        .MinimumValue = 0
                        .DecimalPlaces = 0
                    End With
                End If
                Return _YosanShisakuKataHiCellType
            End Get
        End Property

        ''' <summary>設計情報_部品ノート</summary>
        ''' <value>設計情報_部品ノート</value>
        ''' <returns>設計情報_部品ノート</returns>
        Public ReadOnly Property YosanBuhinNoteCellType() As TextCellType
            Get
                If _YosanBuhinNoteCellType Is Nothing Then

                    _YosanBuhinNoteCellType = ShisakuSpreadUtil.NewGeneralTextCellType()
                    '大文字化はしない'
                    _YosanBuhinNoteCellType.CharacterCasing = CharacterCasing.Normal
                    _YosanBuhinNoteCellType.MaxLength = 102
                End If
                Return _YosanBuhinNoteCellType
            End Get
        End Property

        ''' <summary>設計情報_備考</summary>
        ''' <value>設計情報_備考</value>
        ''' <returns>設計情報_備考</returns>
        Public ReadOnly Property YosanBikouCellType() As TextCellType
            Get
                If _YosanBikouCellType Is Nothing Then

                    _YosanBikouCellType = ShisakuSpreadUtil.NewGeneralTextCellType()
                    _YosanBikouCellType.CharacterCasing = CharacterCasing.Normal
                    _YosanBikouCellType.MaxLength = 60
                End If
                Return _YosanBikouCellType
            End Get
        End Property

        ''' <summary>部品費根拠_国外区分</summary>
        ''' <value>部品費根拠_国外区分</value>
        ''' <returns>部品費根拠_国外区分</returns>
        Public ReadOnly Property YosanKonkyoKokugaiKbnCellType() As TextCellType
            Get
                If _YosanKonkyoKokugaiKbnCellType Is Nothing Then

                    _YosanKonkyoKokugaiKbnCellType = ShisakuSpreadUtil.NewGeneralTextCellType()
                    _YosanKonkyoKokugaiKbnCellType.MaxLength = 2
                End If
                Return _YosanKonkyoKokugaiKbnCellType
            End Get
        End Property

        ''' <summary>部品費根拠_MIXコスト部品費(円/ｾﾝﾄ)</summary>
        ''' <value>部品費根拠_MIXコスト部品費(円/ｾﾝﾄ)</value>
        ''' <returns>部品費根拠_MIXコスト部品費(円/ｾﾝﾄ)</returns>
        Public ReadOnly Property YosanKonkyoMixBuhinHiCellType() As CurrencyCellType
            Get
                If _YosanKonkyoMixBuhinHiCellType Is Nothing Then
                    _YosanKonkyoMixBuhinHiCellType = ShisakuSpreadUtil.NewGeneralCurrencyCellType
                    With _YosanKonkyoMixBuhinHiCellType
                        .MaximumValue = 9999999999
                        .MinimumValue = 0
                        .DecimalPlaces = 2
                    End With
                End If
                Return _YosanKonkyoMixBuhinHiCellType
            End Get
        End Property

        ''' <summary>部品費根拠_引用元MIX値部品費</summary>
        ''' <value>部品費根拠_引用元MIX値部品費</value>
        ''' <returns>部品費根拠_引用元MIX値部品費</returns>
        Public ReadOnly Property YosanKonkyoInyouMixBuhinHiCellType() As TextCellType
            Get
                If _YosanKonkyoInyouMixBuhinHiCellType Is Nothing Then

                    _YosanKonkyoInyouMixBuhinHiCellType = ShisakuSpreadUtil.NewGeneralTextCellType()
                    _YosanKonkyoInyouMixBuhinHiCellType.MaxLength = 100
                End If
                Return _YosanKonkyoInyouMixBuhinHiCellType
            End Get
        End Property

        ''' <summary>部品費根拠_係数１</summary>
        ''' <value>部品費根拠_係数１</value>
        ''' <returns>部品費根拠_係数１</returns>
        Public ReadOnly Property YosanKonkyoKeisu1CellType() As NumberCellType
            Get
                If _YosanKonkyoKeisu1CellType Is Nothing Then

                    _YosanKonkyoKeisu1CellType = ShisakuSpreadUtil.NewGeneralNumberCellType()
                    With _YosanKonkyoKeisu1CellType
                        .MaximumValue = 99999
                        .MinimumValue = 0
                        .DecimalPlaces = 2
                    End With
                End If
                Return _YosanKonkyoKeisu1CellType
            End Get
        End Property

        ''' <summary>部品費根拠_工法</summary>
        ''' <value>部品費根拠_工法</value>
        ''' <returns>部品費根拠_工法</returns>
        Public ReadOnly Property YosanKonkyoKouhouCellType() As TextCellType
            Get
                If _YosanKonkyoKouhouCellType Is Nothing Then

                    _YosanKonkyoKouhouCellType = ShisakuSpreadUtil.NewGeneralTextCellType()
                    _YosanKonkyoKouhouCellType.CharacterCasing = CharacterCasing.Normal
                    _YosanKonkyoKouhouCellType.MaxLength = 60
                End If
                Return _YosanKonkyoKouhouCellType
            End Get
        End Property

        ''' <summary>割付予算_部品費(円)</summary>
        ''' <value>割付予算_部品費(円)</value>
        ''' <returns>割付予算_部品費(円)</returns>
        Public ReadOnly Property YosanWaritukeBuhinHiCellType() As CurrencyCellType
            Get
                If _YosanWaritukeBuhinHiCellType Is Nothing Then
                    _YosanWaritukeBuhinHiCellType = ShisakuSpreadUtil.NewGeneralCurrencyCellType
                    With _YosanWaritukeBuhinHiCellType
                        .MaximumValue = 9999999999
                        .MinimumValue = 0
                        .DecimalPlaces = 2
                    End With
                End If
                Return _YosanWaritukeBuhinHiCellType
            End Get
        End Property

        ''' <summary>部品費根拠_係数２</summary>
        ''' <value>部品費根拠_係数２</value>
        ''' <returns>部品費根拠_係数２</returns>
        Public ReadOnly Property YosanWaritukeKeisu2CellType() As NumberCellType
            Get
                If _YosanWaritukeKeisu2CellType Is Nothing Then

                    _YosanWaritukeKeisu2CellType = ShisakuSpreadUtil.NewGeneralNumberCellType()
                    With _YosanWaritukeKeisu2CellType
                        .MaximumValue = 99999
                        .MinimumValue = 0
                        .DecimalPlaces = 2
                    End With
                End If
                Return _YosanWaritukeKeisu2CellType
            End Get
        End Property

        ''' <summary>割付予算_部品費合計(円)</summary>
        ''' <value>割付予算_部品費合計(円)</value>
        ''' <returns>割付予算_部品費合計(円)</returns>
        Public ReadOnly Property YosanWaritukeBuhinHiTotalCellType() As CurrencyCellType
            Get
                If _YosanWaritukeBuhinHiTotalCellType Is Nothing Then
                    _YosanWaritukeBuhinHiTotalCellType = ShisakuSpreadUtil.NewGeneralCurrencyCellType
                    With _YosanWaritukeBuhinHiTotalCellType
                        .MaximumValue = 9999999999
                        .MinimumValue = 0
                        .DecimalPlaces = 2
                    End With
                End If
                Return _YosanWaritukeBuhinHiTotalCellType
            End Get
        End Property

        ''' <summary>割付予算_型費(千円)</summary>
        ''' <value>割付予算_型費(千円)</value>
        ''' <returns>割付予算_型費(千円)</returns>
        Public ReadOnly Property YosanWaritukeKataHiCellType() As CurrencyCellType
            Get
                If _YosanWaritukeKataHiCellType Is Nothing Then
                    _YosanWaritukeKataHiCellType = ShisakuSpreadUtil.NewGeneralCurrencyCellType
                    With _YosanWaritukeKataHiCellType
                        .MaximumValue = 999999
                        .MinimumValue = 0
                        .DecimalPlaces = 0
                    End With
                End If
                Return _YosanWaritukeKataHiCellType
            End Get
        End Property

        ''' <summary>購入希望_購入希望単価(円)</summary>
        ''' <value>購入希望_購入希望単価(円)</value>
        ''' <returns>購入希望_購入希望単価(円)</returns>
        Public ReadOnly Property YosanKounyuKibouTankaCellType() As CurrencyCellType
            Get
                If _YosanKounyuKibouTankaCellType Is Nothing Then
                    _YosanKounyuKibouTankaCellType = ShisakuSpreadUtil.NewGeneralCurrencyCellType
                    With _YosanKounyuKibouTankaCellType
                        .MaximumValue = 9999999999
                        .MinimumValue = 0
                        .DecimalPlaces = 2
                    End With
                End If
                Return _YosanKounyuKibouTankaCellType
            End Get
        End Property

        ''' <summary>購入希望_部品費(円)</summary>
        ''' <value>購入希望_部品費(円)</value>
        ''' <returns>購入希望_部品費(円)</returns>
        Public ReadOnly Property YosanKounyuKibouBuhinHiCellType() As CurrencyCellType
            Get
                If _YosanKounyuKibouBuhinHiCellType Is Nothing Then
                    _YosanKounyuKibouBuhinHiCellType = ShisakuSpreadUtil.NewGeneralCurrencyCellType
                    With _YosanKounyuKibouBuhinHiCellType
                        .MaximumValue = 9999999999
                        .MinimumValue = 0
                        .DecimalPlaces = 2
                    End With
                End If
                Return _YosanKounyuKibouBuhinHiCellType
            End Get
        End Property

        ''' <summary>購入希望_部品費合計(円)</summary>
        ''' <value>購入希望_部品費合計(円)</value>
        ''' <returns>購入希望_部品費合計(円)</returns>
        Public ReadOnly Property YosanKounyuKibouBuhinHiTotalCellType() As CurrencyCellType
            Get
                If _YosanKounyuKibouBuhinHiTotalCellType Is Nothing Then
                    _YosanKounyuKibouBuhinHiTotalCellType = ShisakuSpreadUtil.NewGeneralCurrencyCellType
                    With _YosanKounyuKibouBuhinHiTotalCellType
                        .MaximumValue = 9999999999
                        .MinimumValue = 0
                        .DecimalPlaces = 2
                    End With
                End If
                Return _YosanKounyuKibouBuhinHiTotalCellType
            End Get
        End Property

        ''' <summary>購入希望_型費(円)</summary>
        ''' <value>購入希望_型費(円)</value>
        ''' <returns>購入希望_型費(円)</returns>
        Public ReadOnly Property YosanKounyuKibouKataHiCellType() As CurrencyCellType
            Get
                If _YosanKounyuKibouKataHiCellType Is Nothing Then
                    _YosanKounyuKibouKataHiCellType = ShisakuSpreadUtil.NewGeneralCurrencyCellType
                    With _YosanKounyuKibouKataHiCellType
                        .MaximumValue = 9999999999
                        .MinimumValue = 0
                        .DecimalPlaces = 2
                    End With
                End If
                Return _YosanKounyuKibouKataHiCellType
            End Get
        End Property


        ''' <summary>予算汎用</summary>
        ''' <value>予算汎用</value>
        ''' <returns>予算汎用</returns>
        Public ReadOnly Property YosanKakoCellType() As CurrencyCellType
            Get
                If _YosanDecimalCellType Is Nothing Then
                    _YosanDecimalCellType = ShisakuSpreadUtil.NewGeneralCurrencyCellType
                    With _YosanDecimalCellType
                        .MaximumValue = 9999999999
                        .MinimumValue = 0
                        .DecimalPlaces = 2
                    End With
                End If
                Return _YosanDecimalCellType
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


        ''' <summary>工事指令№</summary>
        ''' <value>工事指令№</value>
        ''' <returns>工事指令№</returns>
        Public ReadOnly Property KakoKojiShireiNoCellType() As TextCellType
            Get
                If _KakoKojiShireiNoCellType Is Nothing Then

                    _KakoKojiShireiNoCellType = ShisakuSpreadUtil.NewGeneralTextCellType()
                    _KakoKojiShireiNoCellType.CharacterCasing = CharacterCasing.Normal
                    _KakoKojiShireiNoCellType.MaxLength = 10
                End If
                Return _KakoKojiShireiNoCellType
            End Get
        End Property

        ''' <summary>イベント名称</summary>
        ''' <value>イベント名称</value>
        ''' <returns>イベント名称</returns>
        Public ReadOnly Property KakoEventNameCellType() As TextCellType
            Get
                If _KakoEventNameCellType Is Nothing Then

                    _KakoEventNameCellType = ShisakuSpreadUtil.NewGeneralTextCellType()
                    _KakoEventNameCellType.CharacterCasing = CharacterCasing.Normal
                    _KakoEventNameCellType.MaxLength = 10
                End If
                Return _KakoEventNameCellType
            End Get
        End Property

        ''' <summary>発注日</summary>
        ''' <value>発注日</value>
        ''' <returns>発注日</returns>
        Public ReadOnly Property KakoHacchuDateCellType() As TextCellType
            Get
                If _KakoHacchuDateCellType Is Nothing Then

                    _KakoHacchuDateCellType = ShisakuSpreadUtil.NewGeneralTextCellType()
                    _KakoHacchuDateCellType.MaxLength = 10
                End If
                Return _KakoHacchuDateCellType
            End Get
        End Property

        ''' <summary>検収日</summary>
        ''' <value>検収日</value>
        ''' <returns>検収日</returns>
        Public ReadOnly Property KakoKenshuDateCellType() As TextCellType
            Get
                If _KakoKenshuDateCellType Is Nothing Then

                    _KakoKenshuDateCellType = ShisakuSpreadUtil.NewGeneralTextCellType()
                    _KakoKenshuDateCellType.MaxLength = 10
                End If
                Return _KakoKenshuDateCellType
            End Get
        End Property


    End Class
End Namespace