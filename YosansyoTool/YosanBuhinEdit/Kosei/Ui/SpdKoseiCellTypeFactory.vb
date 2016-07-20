Imports FarPoint.Win.Spread.CellType
Imports ShisakuCommon.Ui.Spd

Namespace YosanBuhinEdit.Kosei.Ui
    ''' <summary>
    ''' 予算書部品構成編集画面で使用するCellTypeを一意に管理するクラス
    ''' </summary>
    ''' <remarks></remarks>
    Public Class SpdKoseiCellTypeFactory
        '' 設計課
        Private _BukaCodeCellType As TextCellType
        '' ブロック
        Private _BlockNoCellType As TextCellType
        '' レベル
        Private _LevelCellType As TextCellType
        '' 国内集計
        Private _ShukeiCodeCellType As TextCellType
        '' 海外集計
        Private _SiaShukeiCodeCellType As TextCellType
        '' 取引先コード
        Private _MakerCodeCellType As TextCellType
        '' 取引先名称
        Private _MakerNameCellType As TextCellType
        '' 部品番号  
        Private _BuhinNoCellType As TextCellType
        '' 部品番号試作区分
        Private _BuhinNoKbnCellType As TextCellType
        '' 部品名称  
        Private _BuhinNameCellType As TextCellType
        '' 供給セクション
        Private _KyoukuSectionCellType As TextCellType
        '' 変更概要  
        Private _HenkoGaiyoCellType As TextCellType
        '' 部品費（量産） 
        Private _BuhinHiRyosanCellType As NumberCellType
        '' 部品費（部品表）
        Private _BuhinHiBuhinhyoCellType As NumberCellType
        '' 部品費（特記） 
        Private _BuhinHiTokkiCellType As NumberCellType
        '' 型費  
        Private _KataHiCellType As NumberCellType
        '' 治具費
        Private _JiguHiCellType As NumberCellType
        '' 工数
        Private _KosuCellType As NumberCellType
        '' 発注実績(割付&MIX値全体と平均値)
        Private _HachuJisekiMixCellType As NumberCellType
   
        ''' <summary>設計課</summary>
        ''' <value>設計課</value>
        ''' <returns>設計課</returns>
        Public ReadOnly Property BukaCodeCellType() As TextCellType
            Get
                If _BukaCodeCellType Is Nothing Then
                    _BukaCodeCellType = ShisakuSpreadUtil.NewGeneralTextCellType()
                    _BukaCodeCellType.MaxLength = 4
                    '_BukaCodeCellType.CharacterSet = CharacterSet.AlphaNumeric
                End If
                Return _BukaCodeCellType
            End Get
        End Property

        ''' <summary>ブロック</summary>
        ''' <value>ブロック</value>
        ''' <returns>ブロック</returns>
        Public ReadOnly Property BlockNoCellType() As TextCellType
            Get
                If _BlockNoCellType Is Nothing Then
                    _BlockNoCellType = ShisakuSpreadUtil.NewGeneralTextCellType()
                    _BlockNoCellType.MaxLength = 4
                    '_BlockNoCellType.CharacterSet = CharacterSet.AlphaNumeric
                End If
                Return _BukaCodeCellType
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

        ''' <summary>国内集計</summary>
        ''' <value>国内集計</value>
        ''' <returns>国内集計</returns>
        Public ReadOnly Property ShukeiCodeCellType() As TextCellType
            Get
                If _ShukeiCodeCellType Is Nothing Then
                    _ShukeiCodeCellType = ShisakuSpreadUtil.NewGeneralTextCellType()
                    _ShukeiCodeCellType.MaxLength = 1
                    '_ShukeiCodeCellType.CharacterSet = CharacterSet.AlphaNumeric
                End If
                Return _ShukeiCodeCellType
            End Get
        End Property

        ''' <summary>海外集計</summary>
        ''' <value>海外集計</value>
        ''' <returns>海外集計</returns>
        Public ReadOnly Property SiaShukeiCodeCellType() As TextCellType
            Get
                If _SiaShukeiCodeCellType Is Nothing Then
                    _SiaShukeiCodeCellType = ShisakuSpreadUtil.NewGeneralTextCellType()
                    _SiaShukeiCodeCellType.MaxLength = 1
                    '_SiaShukeiCodeCellType.CharacterSet = CharacterSet.AlphaNumeric
                End If
                Return _SiaShukeiCodeCellType
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
                    '_MakerCodeCellType.CharacterSet = CharacterSet.AlphaNumeric
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
                    _MakerNameCellType.MaxLength = 102
                    '_MakerNameCellType.CharacterSet = CharacterSet.AlphaNumeric
                End If
                Return _BuhinNoCellType
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
                    '_BuhinNoCellType.CharacterSet = CharacterSet.AlphaNumeric
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
                    '_BuhinNoKbnCellType.CharacterSet = CharacterSet.AlphaNumeric
                End If
                Return _BuhinNoKbnCellType
            End Get
        End Property

        ''' <summary>部品名称</summary>
        ''' <value>部品名称</value>
        ''' <returns>部品名称</returns>
        Public ReadOnly Property BuhinNameCellType() As TextCellType
            Get
                If _BuhinNameCellType Is Nothing Then

                    _BuhinNameCellType = ShisakuSpreadUtil.NewGeneralTextCellType()
                    _BuhinNameCellType.MaxLength = 30
                    '_BuhinNameCellType.CharacterSet = CharacterSet.AlphaNumeric
                End If
                Return _BuhinNameCellType
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

        ''' <summary>変更概要 </summary>
        ''' <value>変更概要 </value>
        ''' <returns>変更概要 </returns>
        Public ReadOnly Property HenkoGaiyoCellType() As TextCellType
            Get
                If _HenkoGaiyoCellType Is Nothing Then
                    _HenkoGaiyoCellType = ShisakuSpreadUtil.NewGeneralTextCellType()
                    _HenkoGaiyoCellType.CharacterSet = CharacterSet.AllIME
                    _HenkoGaiyoCellType.MaxLength = 256
                End If
                Return _HenkoGaiyoCellType
            End Get
        End Property

        ''' <summary>部品費（量産）</summary>
        ''' <value>部品費（量産）</value>
        ''' <returns>部品費（量産）</returns>
        Public ReadOnly Property BuhinHiRyosanCellType() As NumberCellType
            Get
                If _BuhinHiRyosanCellType Is Nothing Then
                    _BuhinHiRyosanCellType = ShisakuSpreadUtil.NewGeneralNumberCellType()
                    _BuhinHiRyosanCellType.MaximumValue = 99999999999999
                    _BuhinHiRyosanCellType.MinimumValue = 0
                    _BuhinHiRyosanCellType.DecimalPlaces = 2
                End If
                Return _BuhinHiRyosanCellType
            End Get
        End Property

        ''' <summary>部品費（部品表）</summary>
        ''' <value>部品費（部品表）</value>
        ''' <returns>部品費（部品表）</returns>
        Public ReadOnly Property BuhinHiBuhinhyoCellType() As NumberCellType
            Get
                If _BuhinHiBuhinhyoCellType Is Nothing Then
                    _BuhinHiBuhinhyoCellType = ShisakuSpreadUtil.NewGeneralNumberCellType()
                    _BuhinHiBuhinhyoCellType.MaximumValue = 99999999999999
                    _BuhinHiBuhinhyoCellType.MinimumValue = 0
                    _BuhinHiBuhinhyoCellType.DecimalPlaces = 2
                End If
                Return _BuhinHiBuhinhyoCellType
            End Get
        End Property

        ''' <summary>部品費（特記）</summary>
        ''' <value>部品費（特記）</value>
        ''' <returns>部品費（特記）</returns>
        Public ReadOnly Property BuhinHiTokkiCellType() As NumberCellType
            Get
                If _BuhinHiTokkiCellType Is Nothing Then
                    _BuhinHiTokkiCellType = ShisakuSpreadUtil.NewGeneralNumberCellType()
                    _BuhinHiTokkiCellType.MaximumValue = 99999999999999
                    _BuhinHiTokkiCellType.MinimumValue = 0
                    _BuhinHiTokkiCellType.DecimalPlaces = 2
                End If
                Return _BuhinHiTokkiCellType
            End Get
        End Property

        ''' <summary>型費</summary>
        ''' <value>型費</value>
        ''' <returns>型費</returns>
        Public ReadOnly Property KataHiCellType() As NumberCellType
            Get
                If _KataHiCellType Is Nothing Then
                    _KataHiCellType = ShisakuSpreadUtil.NewGeneralNumberCellType()
                    _KataHiCellType.MaximumValue = 999999999.9
                    _KataHiCellType.MinimumValue = 0
                    _KataHiCellType.DecimalPlaces = 1
                End If
                Return _KataHiCellType
            End Get
        End Property

        ''' <summary>
        ''' 治具費
        ''' </summary>
        ''' <value>治具費</value>
        ''' <returns>治具費</returns>
        ''' <remarks>治具費</remarks>
        Public ReadOnly Property JiguHiCellType() As NumberCellType
            Get
                If _JiguHiCellType Is Nothing Then
                    _JiguHiCellType = ShisakuSpreadUtil.NewGeneralNumberCellType()
                    _JiguHiCellType.MaximumValue = 999999999.9
                    _JiguHiCellType.MinimumValue = 0
                    _JiguHiCellType.DecimalPlaces = 1
                End If
                Return _JiguHiCellType
            End Get
        End Property

        ''' <summary>工数</summary>
        ''' <value>工数</value>
        ''' <returns>工数</returns>
        Public ReadOnly Property KosuCellType() As NumberCellType
            Get
                If _KosuCellType Is Nothing Then
                    _KosuCellType = ShisakuSpreadUtil.NewGeneralNumberCellType()
                    _KosuCellType.MaximumValue = 9999.9
                    _KosuCellType.MinimumValue = 0
                    _KosuCellType.DecimalPlaces = 1
                End If
                Return _KosuCellType
            End Get
        End Property

        ''' <summary>発注実績(割付値全体と平均値)</summary>
        ''' <value>発注実績(割付値全体と平均値)</value>
        ''' <returns>発注実績(割付値全体と平均値)</returns>
        Public ReadOnly Property HachuJisekiMixCellType() As NumberCellType
            Get
                If _HachuJisekiMixCellType Is Nothing Then                   
                    _HachuJisekiMixCellType = ShisakuSpreadUtil.NewGeneralNumberCellType()
                    _HachuJisekiMixCellType.MaximumValue = 99999999999999
                    _HachuJisekiMixCellType.MinimumValue = 0
                    _HachuJisekiMixCellType.DecimalPlaces = 2
                End If
                Return _HachuJisekiMixCellType
            End Get
        End Property

    End Class
End Namespace