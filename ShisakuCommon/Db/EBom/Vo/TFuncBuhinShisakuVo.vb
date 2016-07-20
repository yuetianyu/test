Namespace Db.EBom.Vo
    ''' <summary>
    ''' 現調部品情報(試作)(T_GENCHO_BUHIN_SHISAKU)のVo
    ''' </summary>
    ''' <remarks></remarks>
    Public Class TFuncBuhinShisakuVo

        '' 現調イベントコード
        Private _GenchoEventCode As String
        '' 現調部課コード
        Private _GenchoBukaCode As String
        '' 現調ブロック№
        Private _GenchoBlockNo As String
        '' フェイズ№
        Private _PhaseNo As Integer
        '' 国内集計コード
        Private _ShukeiCode As String
        '' 海外ASIA集計コード
        Private _SiaShukeiCode As String
        '' 部品番号
        Private _BuhinNo As String

        '' 手配_JPN親子関係
        Private _TehaiJpnMaker As String
        '' 手配_専用
        Private _TehaiSenyou As String
        '' 手配_早期品玉
        Private _TehaiSoukiHintama As String
        '' 手配_手配記号
        Private _TehaiKigo As String
        '' 手配_発注先コード
        Private _TehaiMakerCode As String
        '' 手配_発送先名称
        Private _TehaiMakerName As String
        '' 手配_支給先コード
        Private _TehaiShikyusakiCode As String
        '' 手配_支給先名称
        Private _TehaiShikyusakiName As String
        '' 手配_製作地
        Private _TehaiSeisakuChi As String
        '' 手配_納入予定日
        Private _TehaiNounyuYoteiBi As Nullable(Of Int32)
        '' 手配_納入予定日
        Private _TehaiNounyuJissekiBi As Nullable(Of Int32)
        '' 手配_備考
        Private _TehaiBikou As String
        '' 輸送_長さ(cm)
        Private _YusouLongCm As Decimal
        '' 輸送_幅(cm)
        Private _YusouWidthCm As Decimal
        '' 輸送_高さ(cm)
        Private _YusouHeightCm As Decimal
        '' 輸送_容積(cm3)
        Private _YusouVolimeCm3 As Decimal
        '' 輸送_梱包係数
        Private _YusouKonpoKeisu As Decimal
        '' 輸送_Volumeweight/Grossweight(kg)
        Private _YusouVolimeGrossWeightKg As Decimal
        '' 輸送_輸送方法
        Private _YusouHouhou As String
        '' 輸送_輸送係数(千円/kg)
        Private _YusouKeisuSenYen As Decimal
        '' 輸送_試算輸送費(千円/台)
        Private _YusouShisanYusouHiSenYen As Decimal
        '' 輸送_過去ﾃﾞｰﾀ輸送費(千円/台)
        Private _YusouOldDataYusouHiSenYen As Decimal
        '' 輸送_備考
        Private _YusouBikou As String
        '' 予算_MIX値倍率
        Private _YosanMixBairitsu As Decimal
        '' 予算_MIX値部品費(円/個)
        Private _YosanMixBuhinHiYen As Decimal
        '' 予算_引用元MIX値部品費
        Private _YosanInyouMixBuhinHi As String
        '' 予算_①部品製作費(円)
        Private _YosanBuhinSeisakuHi As Decimal
        '' 予算_②梱包/輸送/諸経費(円)
        Private _YosanKonpoYusouSyokeiHiYen As Decimal
        '' 予算_輸送梱包備考
        Private _YosanYusouKonpoBikou As String
        '' 予算_③割付値(円/個)(=①+②)
        Private _YosanWaritukeChiYen As Decimal
        '' 発注値(円/個)
        Private _YosanHachuChi As Decimal
        '' 引用元MIX値型費
        Private _YosanInyouMixKataHi As String
        '' 予算_型費(千円)
        Private _YosanKataHiSenYen As Decimal
        '' 予算_工法
        Private _YosanKouhou As String
        '' 予算_備考
        Private _YosanBikou As String

        Private _JissekiMixBuhinHi As Nullable(Of Decimal)
        Private _JissekiMixKataHi As Nullable(Of Decimal)

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

        ''' <summary>現調イベントコード</summary>
        ''' <value></value>
        ''' <returns></returns>
        Public Property GenchoEventCode() As String
            Get
                Return _GenchoEventCode
            End Get
            Set(ByVal value As String)
                _GenchoEventCode = value
            End Set
        End Property

        ''' <summary>現調部課コード</summary>
        ''' <value></value>
        ''' <returns></returns>
        Public Property GenchoBukaCode() As String
            Get
                Return _GenchoBukaCode
            End Get
            Set(ByVal value As String)
                _GenchoBukaCode = value
            End Set
        End Property

        ''' <summary>現調ブロック№</summary>
        ''' <value></value>
        ''' <returns></returns>
        Public Property GenchoBlockNo() As String
            Get
                Return _GenchoBlockNo
            End Get
            Set(ByVal value As String)
                _GenchoBlockNo = value
            End Set
        End Property

        ''' <summary>フェイズ№</summary>
        ''' <value></value>
        ''' <returns></returns>
        Public Property PhaseNo() As Integer
            Get
                Return _PhaseNo
            End Get
            Set(ByVal value As Integer)
                _PhaseNo = value
            End Set
        End Property

        ''' <summary>国内集計コード</summary>
        ''' <value></value>
        ''' <returns></returns>
        Public Property ShukeiCode() As String
            Get
                Return _ShukeiCode
            End Get
            Set(ByVal value As String)
                _ShukeiCode = value
            End Set
        End Property

        ''' <summary>海外SIA集計コード</summary>
        ''' <value></value>
        ''' <returns></returns>
        Public Property SiaShukeiCode() As String
            Get
                Return _SiaShukeiCode
            End Get
            Set(ByVal value As String)
                _SiaShukeiCode = value
            End Set
        End Property

        ''' <summary>部品番号</summary>
        ''' <value></value>
        ''' <returns></returns>
        Public Property BuhinNo() As String
            Get
                Return _BuhinNo
            End Get
            Set(ByVal value As String)
                _BuhinNo = value
            End Set
        End Property


        ''' <summary>手配_JPN親子関係</summary>
        ''' <value></value>
        ''' <returns></returns>
        Public Property TehaiJpnMaker() As String
            Get
                Return _TehaiJpnMaker
            End Get
            Set(ByVal value As String)
                _TehaiJpnMaker = value
            End Set
        End Property

        ''' <summary>手配_専用</summary>
        ''' <value></value>
        ''' <returns></returns>
        Public Property TehaiSenyou() As String
            Get
                Return _TehaiSenyou
            End Get
            Set(ByVal value As String)
                _TehaiSenyou = value
            End Set
        End Property

        ''' <summary>手配_早期品玉</summary>
        ''' <value></value>
        ''' <returns></returns>
        Public Property TehaiSoukiHintama() As String
            Get
                Return _TehaiSoukiHintama
            End Get
            Set(ByVal value As String)
                _TehaiSoukiHintama = value
            End Set
        End Property

        ''' <summary>手配_手配記号</summary>
        ''' <value></value>
        ''' <returns></returns>
        Public Property TehaiKigo() As String
            Get
                Return _TehaiKigo
            End Get
            Set(ByVal value As String)
                _TehaiKigo = value
            End Set
        End Property

        ''' <summary>手配_発注先コード</summary>
        ''' <value></value>
        ''' <returns></returns>
        Public Property TehaiMakerCode() As String
            Get
                Return _TehaiMakerCode
            End Get
            Set(ByVal value As String)
                _TehaiMakerCode = value
            End Set
        End Property

        ''' <summary>手配_発送先名称</summary>
        ''' <value></value>
        ''' <returns></returns>
        Public Property TehaiMakerName() As String
            Get
                Return _TehaiMakerName
            End Get
            Set(ByVal value As String)
                _TehaiMakerName = value
            End Set
        End Property

        ''' <summary>手配_支給先コード</summary>
        ''' <value></value>
        ''' <returns></returns>
        Public Property TehaiShikyusakiCode() As String
            Get
                Return _TehaiShikyusakiCode
            End Get
            Set(ByVal value As String)
                _TehaiShikyusakiCode = value
            End Set
        End Property

        ''' <summary>手配_支給先名称</summary>
        ''' <value></value>
        ''' <returns></returns>
        Public Property TehaiShikyusakiName() As String
            Get
                Return _TehaiShikyusakiName
            End Get
            Set(ByVal value As String)
                _TehaiShikyusakiName = value
            End Set
        End Property

        ''' <summary>手配_製作地</summary>
        ''' <value></value>
        ''' <returns></returns>
        Public Property TehaiSeisakuChi() As String
            Get
                Return _TehaiSeisakuChi
            End Get
            Set(ByVal value As String)
                _TehaiSeisakuChi = value
            End Set
        End Property

        ''' <summary>手配_納入予定日</summary>
        ''' <value></value>
        ''' <returns></returns>
        Public Property TehaiNounyuYoteiBi() As Nullable(Of Int32)
            Get
                Return _TehaiNounyuYoteiBi
            End Get
            Set(ByVal value As Nullable(Of Int32))
                _TehaiNounyuYoteiBi = value
            End Set
        End Property

        Public Property TehaiNounyuJissekiBi() As Nullable(Of Int32)
            Get
                Return _TehaiNounyuJissekiBi
            End Get
            Set(ByVal value As Nullable(Of Int32))
                _TehaiNounyuJissekiBi = value
            End Set
        End Property

        ''' <summary>手配_備考</summary>
        ''' <value></value>
        ''' <returns></returns>
        Public Property TehaiBikou() As String
            Get
                Return _TehaiBikou
            End Get
            Set(ByVal value As String)
                _TehaiBikou = value
            End Set
        End Property

        ''' <summary>輸送_長さ(cm)</summary>
        ''' <value></value>
        ''' <returns></returns>
        Public Property YusouLongCm() As Decimal
            Get
                Return _YusouLongCm
            End Get
            Set(ByVal value As Decimal)
                _YusouLongCm = value
            End Set
        End Property

        ''' <summary>輸送_幅(cm)</summary>
        ''' <value></value>
        ''' <returns></returns>
        Public Property YusouWidthCm() As Decimal
            Get
                Return _YusouWidthCm
            End Get
            Set(ByVal value As Decimal)
                _YusouWidthCm = value
            End Set
        End Property

        ''' <summary>輸送_高さ(cm)</summary>
        ''' <value></value>
        ''' <returns></returns>
        Public Property YusouHeightCm() As Decimal
            Get
                Return _YusouHeightCm
            End Get
            Set(ByVal value As Decimal)
                _YusouHeightCm = value
            End Set
        End Property

        ''' <summary>輸送_容積(cm3)</summary>
        ''' <value></value>
        ''' <returns></returns>
        Public Property YusouVolimeCm3() As Decimal
            Get
                Return _YusouVolimeCm3
            End Get
            Set(ByVal value As Decimal)
                _YusouVolimeCm3 = value
            End Set
        End Property

        ''' <summary>輸送_梱包係数</summary>
        ''' <value></value>
        ''' <returns></returns>
        Public Property YusouKonpoKeisu() As Decimal
            Get
                Return _YusouKonpoKeisu
            End Get
            Set(ByVal value As Decimal)
                _YusouKonpoKeisu = value
            End Set
        End Property

        ''' <summary>輸送_Volumeweight/Grossweight(kg)</summary>
        ''' <value></value>
        ''' <returns></returns>
        Public Property YusouVolimeGrossWeightKg() As Decimal
            Get
                Return _YusouVolimeGrossWeightKg
            End Get
            Set(ByVal value As Decimal)
                _YusouVolimeGrossWeightKg = value
            End Set
        End Property

        ''' <summary>輸送_輸送方法</summary>
        ''' <value></value>
        ''' <returns></returns>
        Public Property YusouHouhou() As String
            Get
                Return _YusouHouhou
            End Get
            Set(ByVal value As String)
                _YusouHouhou = value
            End Set
        End Property

        ''' <summary>輸送_輸送係数(千円/kg)</summary>
        ''' <value></value>
        ''' <returns></returns>
        Public Property YusouKeisuSenYen() As Decimal
            Get
                Return _YusouKeisuSenYen
            End Get
            Set(ByVal value As Decimal)
                _YusouKeisuSenYen = value
            End Set
        End Property

        ''' <summary>輸送_試算輸送費(千円/台)</summary>
        ''' <value></value>
        ''' <returns></returns>
        Public Property YusouShisanYusouHiSenYen() As Decimal
            Get
                Return _YusouShisanYusouHiSenYen
            End Get
            Set(ByVal value As Decimal)
                _YusouShisanYusouHiSenYen = value
            End Set
        End Property

        ''' <summary>輸送_過去ﾃﾞｰﾀ輸送費(千円/台)</summary>
        ''' <value></value>
        ''' <returns></returns>
        Public Property YusouOldDataYusouHiSenYen() As Decimal
            Get
                Return _YusouOldDataYusouHiSenYen
            End Get
            Set(ByVal value As Decimal)
                _YusouOldDataYusouHiSenYen = value
            End Set
        End Property

        ''' <summary>輸送_備考</summary>
        ''' <value></value>
        ''' <returns></returns>
        Public Property YusouBikou() As String
            Get
                Return _YusouBikou
            End Get
            Set(ByVal value As String)
                _YusouBikou = value
            End Set
        End Property

        ''' <summary>予算_MIX値倍率</summary>
        ''' <value></value>
        ''' <returns></returns>
        Public Property YosanMixBairitsu() As Decimal
            Get
                Return _YosanMixBairitsu
            End Get
            Set(ByVal value As Decimal)
                _YosanMixBairitsu = value
            End Set
        End Property

        ''' <summary>予算_MIX値部品費(円/個)</summary>
        ''' <value></value>
        ''' <returns></returns>
        Public Property YosanMixBuhinHiYen() As Decimal
            Get
                Return _YosanMixBuhinHiYen
            End Get
            Set(ByVal value As Decimal)
                _YosanMixBuhinHiYen = value
            End Set
        End Property

        ''' <summary>予算_引用元MIX値部品費</summary>
        ''' <value></value>
        ''' <returns></returns>
        Public Property YosanInyouMixBuhinHi() As String
            Get
                Return _YosanInyouMixBuhinHi
            End Get
            Set(ByVal value As String)
                _YosanInyouMixBuhinHi = value
            End Set
        End Property

        ''' <summary>予算_①部品製作費(円)</summary>
        ''' <value></value>
        ''' <returns></returns>
        Public Property YosanBuhinSeisakuHi() As Decimal
            Get
                Return _YosanBuhinSeisakuHi
            End Get
            Set(ByVal value As Decimal)
                _YosanBuhinSeisakuHi = value
            End Set
        End Property

        ''' <summary>予算_②梱包/輸送/諸経費(円)</summary>
        ''' <value></value>
        ''' <returns></returns>
        Public Property YosanKonpoYusouSyokeiHiYen() As Decimal
            Get
                Return _YosanKonpoYusouSyokeiHiYen
            End Get
            Set(ByVal value As Decimal)
                _YosanKonpoYusouSyokeiHiYen = value
            End Set
        End Property

        ''' <summary>予算_輸送梱包備考</summary>
        ''' <value></value>
        ''' <returns></returns>
        Public Property YosanYusouKonpoBikou() As String
            Get
                Return _YosanYusouKonpoBikou
            End Get
            Set(ByVal value As String)
                _YosanYusouKonpoBikou = value
            End Set
        End Property

        ''' <summary>予算_③割付値(円/個)(=①+②)</summary>
        ''' <value></value>
        ''' <returns></returns>
        Public Property YosanWaritukeChiYen() As Decimal
            Get
                Return _YosanWaritukeChiYen
            End Get
            Set(ByVal value As Decimal)
                _YosanWaritukeChiYen = value
            End Set
        End Property

        ''' <summary>発注値(円/個)</summary>
        ''' <value></value>
        ''' <returns></returns>
        Public Property YosanHachuChi() As Decimal
            Get
                Return _YosanHachuChi
            End Get
            Set(ByVal value As Decimal)
                _YosanHachuChi = value
            End Set
        End Property

        ''' <summary>引用元MIX値型費</summary>
        ''' <value></value>
        ''' <returns></returns>
        Public Property YosanInyouMixKataHi() As String
            Get
                Return _YosanInyouMixKataHi
            End Get
            Set(ByVal value As String)
                _YosanInyouMixKataHi = value
            End Set
        End Property

        ''' <summary>予算_型費(千円)</summary>
        ''' <value></value>
        ''' <returns></returns>
        Public Property YosanKataHiSenYen() As Decimal
            Get
                Return _YosanKataHiSenYen
            End Get
            Set(ByVal value As Decimal)
                _YosanKataHiSenYen = value
            End Set
        End Property

        ''' <summary>予算_工法</summary>
        ''' <value></value>
        ''' <returns></returns>
        Public Property YosanKouhou() As String
            Get
                Return _YosanKouhou
            End Get
            Set(ByVal value As String)
                _YosanKouhou = value
            End Set
        End Property

        ''' <summary>予算_備考</summary>
        ''' <value></value>
        ''' <returns></returns>
        Public Property YosanBikou() As String
            Get
                Return _YosanBikou
            End Get
            Set(ByVal value As String)
                _YosanBikou = value
            End Set
        End Property


        Public Property JissekiMixBuhinHi() As Nullable(Of Decimal)
            Get
                Return _JissekiMixBuhinHi
            End Get
            Set(ByVal value As Nullable(Of Decimal))
                _JissekiMixBuhinHi = value
            End Set
        End Property


        Public Property JissekiMixKataHi() As Nullable(Of Decimal)
            Get
                Return _JissekiMixKataHi
            End Get
            Set(ByVal value As Nullable(Of Decimal))
                _JissekiMixKataHi = value
            End Set
        End Property









        ''' <summary>作成ユーザーID</summary>
        ''' <value></value>
        ''' <returns></returns>
        Public Property CreatedUserId() As String
            Get
                Return _CreatedUserId
            End Get
            Set(ByVal value As String)
                _CreatedUserId = value
            End Set
        End Property

        ''' <summary>作成年月日</summary>
        ''' <value></value>
        ''' <returns></returns>
        Public Property CreatedDate() As String
            Get
                Return _CreatedDate
            End Get
            Set(ByVal value As String)
                _CreatedDate = value
            End Set
        End Property

        ''' <summary>作成時分秒</summary>
        ''' <value></value>
        ''' <returns></returns>
        Public Property CreatedTime() As String
            Get
                Return _CreatedTime
            End Get
            Set(ByVal value As String)
                _CreatedTime = value
            End Set
        End Property

        ''' <summary>更新ユーザーID</summary>
        ''' <value></value>
        ''' <returns></returns>
        Public Property UpdatedUserId() As String
            Get
                Return _UpdatedUserId
            End Get
            Set(ByVal value As String)
                _UpdatedUserId = value
            End Set
        End Property

        ''' <summary>更新年月日</summary>
        ''' <value></value>
        ''' <returns></returns>
        Public Property UpdatedDate() As String
            Get
                Return _UpdatedDate
            End Get
            Set(ByVal value As String)
                _UpdatedDate = value
            End Set
        End Property

        ''' <summary>更新時分秒</summary>
        ''' <value></value>
        ''' <returns></returns>
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