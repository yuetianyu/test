Imports EventSakusei.ShisakuBuhinMenu.Dao
Imports ShisakuCommon.Db.EBom.Dao.Impl
Imports ShisakuCommon.Db.EBom.Vo.Helper
Imports ShisakuCommon.Db.EBom.Dao
Imports ShisakuCommon
Imports ShisakuCommon.Db.EBom.Vo

Namespace ShisakuBuhinMenu.Logic.Impl.Tenkai
    Public Class BuhinSupplier
        Private ReadOnly shisakuEventCode As String
        Private ReadOnly baseDao As TShisakuEventBaseDao
        Private ReadOnly dao As BuhinDao
        Public Sub New(ByVal shisakuEventCode As String)
            Me.New(shisakuEventCode, New TShisakuEventBaseDaoImpl, New BuhinDaoImpl)
        End Sub
        Public Sub New(ByVal shisakuEventCode As String, _
                       ByVal baseDao As TShisakuEventBaseDao, _
                       ByVal dao As BuhinDao)
            Me.shisakuEventCode = shisakuEventCode
            Me.baseDao = baseDao
            Me.dao = dao
        End Sub

        Public Function GetJuryoGoInfos() As Dictionary(Of String, Boolean)
            Dim param As New TShisakuEventBaseVo
            param.ShisakuEventCode = shisakuEventCode
            Dim vos As List(Of TShisakuEventBaseVo) = baseDao.FindBy(param)

            Dim results As New Dictionary(Of String, Boolean)
            For Each vo As TShisakuEventBaseVo In vos
                Dim helper As New TShisakuEventBaseVoHelper(vo)
                results.Add(vo.ShisakuGousya, helper.IsSekkeiTenkaiKbnJuryoGo)
            Next
            Return results
        End Function

        Public Function Extract0532() As List(Of BuhinResultVo)
            Dim juryoGoInfos As Dictionary(Of String, Boolean) = GetJuryoGoInfos()
            ' 
            Dim kouhoVo As BuhinResultVo = Nothing
            Dim buhins As List(Of BuhinResultVo) = dao.Find0532ByShisakuKousei(shisakuEventCode, 1)

            If buhins.Count = 0 Then
                buhins = dao.Find0533ByShisakuKousei(shisakuEventCode, 1)
            End If

            If buhins.Count = 0 Then
                buhins = dao.Find0530ByShisakuKousei(shisakuEventCode, 1)
            End If


            Dim results As New List(Of BuhinResultVo)
            For Each vo As BuhinResultVo In buhins
                If Not (kouhoVo Is Nothing) _
                   AndAlso Not (EzUtil.IsEqualIfNull(kouhoVo.ShisakuGousya, vo.ShisakuGousya) AndAlso EzUtil.IsEqualIfNull(kouhoVo.BuhinNo, vo.BuhinNo)) Then
                    results.Add(kouhoVo)
                    kouhoVo = vo
                End If
                If juryoGoInfos(vo.ShisakuGousya) Then
                    If 0 < vo.ShoninDate Then
                        kouhoVo = vo
                    End If
                Else
                    If vo.ShoninDate = 0 Then
                        kouhoVo = vo
                    End If
                End If
            Next
            If kouhoVo IsNot Nothing Then
                results.Add(kouhoVo)
            End If
            Return results
        End Function

        Public Function MakeRegisterValues() As List(Of TShisakuBuhinVo)
            ' INSTL情報のINSTL品番で、RHAC0552 を取得。
            Dim buhinResultVos As List(Of BuhinResultVo) = Extract0532()

            Dim results As New List(Of TShisakuBuhinVo)

            For Each buhin As BuhinResultVo In buhinResultVos
                Dim vo As New TShisakuBuhinVo
                results.Add(vo)

                vo.ShisakuEventCode = shisakuEventCode
                vo.ShisakuBukaCode = buhin.ShisakuBukaCode
                vo.ShisakuBlockNo = buhin.ShisakuBlockNo
                vo.ShisakuBlockNoKaiteiNo = buhin.ShisakuBlockNoKaiteiNo
                vo.ShisakuGousya = buhin.ShisakuGousya
                vo.BuhinNo = buhin.BuhinNo
                vo.BuhinNoKbn = String.Empty
                vo.BuhinNoKaiteiNo = buhin.KaiteiNo
                vo.EdaBan = String.Empty
                vo.SekkeiShainNo = buhin.SekkeiShainNo
                vo.MakerCode = buhin.MakerCode
                vo.SiaMakerCode = buhin.SiaMakerCode
                vo.TantoCode = buhin.TantoCode
                vo.SekininBukaCode = buhin.SekininBukaCode
                vo.SekininSiteKbn = buhin.SekininSiteKbn
                vo.ZumenNo = buhin.ZumenNo
                vo.ZumenKaiteiNo = buhin.ZumenKaiteiNo
                vo.ShoninSign = buhin.ShoninSign
                vo.ShoninDate = buhin.ShoninDate
                vo.HinmokuNo = buhin.HinmokuNo
                vo.BuhinName = buhin.BuhinName
                vo.BuhinKanaName = buhin.BuhinKanaName
                vo.HojoName = buhin.HojoName
                vo.KeisuCode = buhin.KeisuCode
                vo.KyoyoModel = buhin.KyoyoModel
                vo.NonyuTani = buhin.NonyuTani
                vo.BuhinKind = buhin.BuhinKind
                vo.NaiseiKbn = buhin.NaiseiKbn
                vo.LowLevel = buhin.LowLevel
                vo.BankoSuryoInput = buhin.BankoSuryoInput
                vo.BuhinShitsuryo = buhin.BuhinShitsuryo
                vo.KinzokuShitsuryoInput = buhin.KinzokuShitsuryoInput
                vo.KinzokuShitsuryo = buhin.KinzokuShitsuryo
                vo.BuhinhiKingaku = buhin.BuhinhiKingaku
                vo.BuhinDateId = buhin.BuhinDateId
                vo.SiaBuhinhi = buhin.SiaBuhinhi
                vo.SiaBuhinDateId = buhin.SiaBuhinDateId
                vo.Tenshinzai = buhin.Tenshinzai
                vo.SiaTenshinzai = buhin.SiaTenshinzai
                vo.KatahiKingaku = buhin.KatahiKingaku
                vo.SiaKatahiKingaku = buhin.SiaKatahiKingaku
                vo.Pallet = buhin.Pallet
                vo.SiaPallet = buhin.SiaPallet
                vo.Kaihatsuhi = buhin.Kaihatsuhi
                vo.SiaKaihatsuhi = buhin.SiaKaihatsuhi
                vo.BankoSuryo = buhin.BankoSuryo
                vo.ShukeiCode = buhin.ShukeiCode
                vo.SiaShukeiCode = buhin.SiaShukeiCode
                vo.GencyoCkdKbn = buhin.GencyoCkdKbn
                '2012/01/23 供給セクション追加 ＝＝＞必要ないかも
                'vo.KyoukuSection = buhin.KyoukuSection
                vo.ShukeiBukaCode = buhin.ShukeiBukaCode
                vo.ZumenOver = buhin.ZumenOver
                vo.ZumenColumn = buhin.ZumenColumn
                vo.TekiyoKbn = buhin.TekiyoKbn
                vo.ChukiKijutsu = buhin.ChukiKijutsu
                vo.NaigaisoKbn = buhin.NaigaisoKbn
                vo.ZairyoKijutsu = buhin.ZairyoKijutsu
                vo.SeihoKijutu = buhin.SeihoKijutu
                vo.DsgnMemo = buhin.DsgnMemo
                vo.FinalKbn = buhin.FinalKbn
                vo.JuyoHoanKbn = buhin.JuyoHoanKbn
                vo.JuyoHoanCode = buhin.JuyoHoanCode
                vo.HoyohinCode = buhin.HoyohinCode
                vo.RecycleMark = buhin.RecycleMark
                vo.HokiseiCode = buhin.HokiseiCode
                vo.SaiyoDate = buhin.SaiyoDate
                vo.HaisiDate = buhin.HaisiDate
                vo.SiriesCode = buhin.SiriesCode
                vo.HyomenShori = buhin.HyomenShori
                vo.SeisanKbn = buhin.SeisanKbn
                vo.BuhinStatusKbn = buhin.BuhinStatusKbn
                vo.SankoHyojiCode = buhin.SankoHyojiCode
                vo.ShutuzuYoteiDate = buhin.ShutuzuYoteiDate
                vo.Status = buhin.Status
                vo.UpdateKbn = buhin.UpdateKbn
                vo.BuhinItemName = buhin.BuhinItemName
                vo.HendoShubetsu = buhin.HendoShubetsu
                vo.KonpoYusohi = buhin.KonpoYusohi
                vo.BenchiBuhinhi = buhin.BenchiBuhinhi
                vo.SiaBenchiBuhinhi = buhin.SiaBenchiBuhinhi
                vo.BenchiKatahi = buhin.BenchiKatahi
                vo.SiaBenchiKatahi = buhin.SiaBenchiKatahi
                vo.KokunaiCostKbn = buhin.KokunaiCostKbn
                vo.KaigaiCostKbn = buhin.KaigaiCostKbn
                vo.Saishiyoufuka = String.Empty
                vo.ZaishituKikaku1 = String.Empty
                vo.ZaishituKikaku2 = String.Empty
                vo.ZaishituKikaku3 = String.Empty
                vo.ZaishituMekki = String.Empty
                vo.ShisakuBankoSuryo = String.Empty
                vo.ShisakuBankoSuryoU = String.Empty
                vo.ShisakuBuhinnHi = Nothing
                vo.ShisakuKataHi = Nothing
                vo.Bikou = String.Empty
                vo.HensyuTourokubi = Nothing
                vo.HensyuTourokujikan = Nothing
                vo.KaiteiHandanFlg = String.Empty
                vo.ShisakuListCode = String.Empty
            Next
            Return results
        End Function

        ''' <summary>
        ''' 試作部品情報へ登録する
        ''' </summary>
        ''' <param name="login">ログイン情報</param>
        ''' <param name="buhinDao">試作部品情報Dao</param>
        ''' <param name="aShisakuDate">試作システム日付</param>
        ''' <remarks></remarks>
        Public Sub Register(ByVal login As LoginInfo, _
                            ByVal buhinDao As TShisakuBuhinDao, _
                            ByVal aShisakuDate As ShisakuDate)

            Dim vos As List(Of TShisakuBuhinVo) = MakeRegisterValues()

            For Each vo As TShisakuBuhinVo In vos
                vo.CreatedUserId = login.UserId
                vo.CreatedDate = aShisakuDate.CurrentDateDbFormat
                vo.CreatedTime = aShisakuDate.CurrentTimeDbFormat
                vo.UpdatedUserId = login.UserId
                vo.UpdatedDate = aShisakuDate.CurrentDateDbFormat
                vo.UpdatedTime = aShisakuDate.CurrentTimeDbFormat

                buhinDao.InsertBy(vo)
            Next
        End Sub
    End Class
End Namespace