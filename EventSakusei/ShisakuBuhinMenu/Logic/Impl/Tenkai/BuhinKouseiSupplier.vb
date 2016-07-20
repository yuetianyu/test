Imports ShisakuCommon.Db.EBom.Dao
Imports EventSakusei.ShisakuBuhinMenu.Dao
Imports ShisakuCommon
Imports ShisakuCommon.Db.EBom.Vo.Helper
Imports ShisakuCommon.Db.EBom.Vo

Namespace ShisakuBuhinMenu.Logic.Impl.Tenkai
    Public Class BuhinKouseiSupplier
        Private ReadOnly shisakuEventCode As String
        Private ReadOnly dao As BuhinKouseiDao
        Public Sub New(ByVal shisakuEventCode As String)
            Me.New(shisakuEventCode, New BuhinKouseiDaoImpl)
        End Sub
        Public Sub New(ByVal shisakuEventCode As String, _
                       ByVal dao As BuhinKouseiDao)
            Me.shisakuEventCode = shisakuEventCode
            Me.dao = dao
        End Sub

        Public Function MakeRegisterValues() As List(Of TShisakuBuhinKouseiVo)
            ' INSTL情報のINSTL品番で、RHAC0552 を取得。
            Dim hoges As List(Of BuhinKouseiResultVo) = dao.FindNew0552ByShisakuInslt(shisakuEventCode)

            Dim results As New List(Of TShisakuBuhinKouseiVo)

            For Each kouseiVo As BuhinKouseiResultVo In hoges
                Dim vo As New TShisakuBuhinKouseiVo
                results.Add(vo)

                vo.ShisakuEventCode = shisakuEventCode
                vo.SakuseiCount = TShisakuBuhinKouseiVoHelper.SakuseiCount.DEFAULT_VALUE
                vo.ShisakuBukaCode = kouseiVo.ShisakuBukaCode
                vo.ShisakuBlockNo = kouseiVo.ShisakuBlockNo
                vo.ShisakuBlockNoKaiteiNo = TShisakuBuhinKouseiVoHelper.ShisakuBlockNoKaiteiNo.DEFAULT_VALUE
                vo.ShisakuGousya = kouseiVo.ShisakuGousya
                vo.InstlHinban = kouseiVo.InstlHinban
                vo.InstlHinbanKbn = kouseiVo.InstlHinbanKbn
                vo.BuhinNoOya = Rhac0552VoHelper.ResolveBuhinNoOyaFrom(kouseiVo)
                vo.BuhinNoKbnOya = String.Empty
                vo.BuhinNoKo = Rhac0552VoHelper.ResolveBuhinNoKoFrom(kouseiVo)
                vo.BuhinNoKbnKo = String.Empty
                vo.KaiteiNo = kouseiVo.KaiteiNo
                vo.ZumenNo = kouseiVo.ZumenNo
                vo.MidashiNo = kouseiVo.MidashiNo
                vo.MidashiNoShurui = kouseiVo.MidashiNoShurui
                vo.MidashiNoHojo = kouseiVo.MidashiNoHojo
                vo.InsuSuryo = kouseiVo.InsuSuryo
                vo.ShukeiCode = kouseiVo.ShukeiCode
                vo.SiaShukeiCode = kouseiVo.SiaShukeiCode
                vo.GencyoCkdKbn = kouseiVo.GencyoCkdKbn
                vo.ShoninKbn = kouseiVo.ShoninKbn
                vo.SaiyoDate = kouseiVo.SaiyoDate
                vo.HaisiDate = kouseiVo.HaisiDate
                vo.SashimodoshiDate = TShisakuBuhinKouseiVoHelper.SashimodoshiDate.DEFAULT_VALUE


            Next
            Return results
        End Function

        ''' <summary>
        ''' 試作部品構成情報へ登録する
        ''' </summary>
        ''' <param name="login">ログイン情報</param>
        ''' <param name="kouseiDao">試作部品構成情報Dao</param>
        ''' <param name="aShisakuDate">試作システム日付</param>
        ''' <remarks></remarks>
        Public Sub Register(ByVal login As LoginInfo, _
                            ByVal kouseiDao As TShisakuBuhinKouseiDao, _
                            ByVal aShisakuDate As ShisakuDate)

            Dim vos As List(Of TShisakuBuhinKouseiVo) = MakeRegisterValues()

            For Each vo As TShisakuBuhinKouseiVo In vos
                vo.CreatedUserId = login.UserId
                vo.CreatedDate = aShisakuDate.CurrentDateDbFormat
                vo.CreatedTime = aShisakuDate.CurrentTimeDbFormat
                vo.UpdatedUserId = login.UserId
                vo.UpdatedDate = aShisakuDate.CurrentDateDbFormat
                vo.UpdatedTime = aShisakuDate.CurrentTimeDbFormat

                kouseiDao.InsertBy(vo)
            Next
        End Sub


        ''' <summary>
        ''' 試作部品構成情報へ登録する
        ''' </summary>
        ''' <param name="shisakuEventCode">試作イベントコード</param>
        ''' <remarks></remarks>
        Public Sub FindKaihatsuFugo(ByVal shisakuEventCode)



        End Sub


    End Class
End Namespace