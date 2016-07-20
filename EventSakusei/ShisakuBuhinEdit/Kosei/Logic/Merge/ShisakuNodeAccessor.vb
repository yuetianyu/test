Imports EventSakusei.ShisakuBuhinEdit.Logic
Imports ShisakuCommon.Db.EBom.Vo
Imports EventSakusei.ShisakuBuhinEdit.Kosei.Logic.Tree

Namespace ShisakuBuhinEdit.Kosei.Logic.Merge
    ''' <summary>
    ''' 試作テーブル用のNodeアクセッサ実装クラス
    ''' </summary>
    ''' <remarks></remarks>
    Public Class ShisakuNodeAccessor : Implements IBuhinNodeAccessor(Of TShisakuBuhinKouseiVo, TShisakuBuhinVo)

        Private ReadOnly aMakerNameResolver As MakerNameResolver
        ''' <summary>
        ''' コンストラクタ
        ''' </summary>
        ''' <param name="aMakerNameResolver">取引先名を解決するInterface</param>
        ''' <remarks></remarks>
        Public Sub New(ByVal aMakerNameResolver As MakerNameResolver)
            Me.aMakerNameResolver = aMakerNameResolver
        End Sub

        Public Function GetBuhinNoFrom(ByVal node As BuhinNode(Of TShisakuBuhinKouseiVo, TShisakuBuhinVo)) As String Implements IBuhinNodeAccessor _
                                                                                                                        (Of TShisakuBuhinKouseiVo, TShisakuBuhinVo) _
                                                                                                                        .GetBuhinNoFrom
            Return node.KoseiVo.BuhinNoKo
        End Function

        Public Function GetBuhinNameFrom(ByVal node As BuhinNode(Of TShisakuBuhinKouseiVo, TShisakuBuhinVo)) As String Implements IBuhinNodeAccessor _
                                                                                                                          (Of TShisakuBuhinKouseiVo, TShisakuBuhinVo) _
                                                                                                                          .GetBuhinNameFrom
            Return node.BuhinVo.BuhinName
        End Function

        Public Function GetBuhinNoKbnFrom(ByVal node As BuhinNode(Of TShisakuBuhinKouseiVo, TShisakuBuhinVo)) As String Implements IBuhinNodeAccessor _
                                                                                                                           (Of TShisakuBuhinKouseiVo, TShisakuBuhinVo) _
                                                                                                                           .GetBuhinNoKbnFrom
            Return node.BuhinVo.BuhinNoKbn
        End Function

        Public Function GetBuhinNoKaiteiNoFrom(ByVal node As BuhinNode(Of TShisakuBuhinKouseiVo, TShisakuBuhinVo)) As String Implements IBuhinNodeAccessor _
                                                                                                                                (Of TShisakuBuhinKouseiVo, TShisakuBuhinVo) _
                                                                                                                                .GetBuhinNoKaiteiNoFrom
            Return node.BuhinVo.BuhinNoKaiteiNo
        End Function

        Public Function GetEdaBanFrom(ByVal node As BuhinNode(Of TShisakuBuhinKouseiVo, TShisakuBuhinVo)) As String Implements IBuhinNodeAccessor _
                                                                                                                       (Of TShisakuBuhinKouseiVo, TShisakuBuhinVo) _
                                                                                                                       .GetEdaBanFrom
            Return node.BuhinVo.EdaBan
        End Function

        Public Function GetMakerCodeFrom(ByVal node As BuhinNode(Of TShisakuBuhinKouseiVo, TShisakuBuhinVo)) As String Implements IBuhinNodeAccessor _
                                                                                                                          (Of TShisakuBuhinKouseiVo, TShisakuBuhinVo) _
                                                                                                                          .GetMakerCodeFrom
            Return node.BuhinVo.MakerCode
        End Function

        Public Function GetMakerNameFrom(ByVal node As BuhinNode(Of TShisakuBuhinKouseiVo, TShisakuBuhinVo)) As String Implements IBuhinNodeAccessor _
                                                                                                                          (Of TShisakuBuhinKouseiVo, TShisakuBuhinVo) _
                                                                                                                          .GetMakerNameFrom
            Return aMakerNameResolver.Resolve(node.BuhinVo.MakerCode)
        End Function

        Public Function GetShukeiCodeFrom(ByVal node As BuhinNode(Of TShisakuBuhinKouseiVo, TShisakuBuhinVo)) As String Implements IBuhinNodeAccessor _
                                                                                                                           (Of TShisakuBuhinKouseiVo, TShisakuBuhinVo) _
                                                                                                                           .GetShukeiCodeFrom
            Return node.KoseiVo.ShukeiCode
        End Function

        Public Function GetSiaShukeiCodeFrom(ByVal node As BuhinNode(Of TShisakuBuhinKouseiVo, TShisakuBuhinVo)) As String Implements IBuhinNodeAccessor _
                                                                                                                              (Of TShisakuBuhinKouseiVo, TShisakuBuhinVo) _
                                                                                                                              .GetSiaShukeiCodeFrom
            Return node.KoseiVo.SiaShukeiCode
        End Function

        Public Function GetGencyoCkdKbnFrom(ByVal node As BuhinNode(Of TShisakuBuhinKouseiVo, TShisakuBuhinVo)) As String Implements IBuhinNodeAccessor _
                                                                                                                             (Of TShisakuBuhinKouseiVo, TShisakuBuhinVo) _
                                                                                                                             .GetGencyoCkdKbnFrom
            Return node.KoseiVo.GencyoCkdKbn
        End Function

        Public Function GetBuhinShukeiCodeFrom(ByVal node As BuhinNode(Of TShisakuBuhinKouseiVo, TShisakuBuhinVo)) As String Implements IBuhinNodeAccessor _
                                                                                                                                (Of TShisakuBuhinKouseiVo, TShisakuBuhinVo) _
                                                                                                                                .GetBuhinShukeiCodeFrom
            Return node.BuhinVo.ShukeiCode
        End Function

        Public Function GetBuhinSiaShukeiCodeFrom(ByVal node As BuhinNode(Of TShisakuBuhinKouseiVo, TShisakuBuhinVo)) As String Implements IBuhinNodeAccessor _
                                                                                                                                   (Of TShisakuBuhinKouseiVo, TShisakuBuhinVo) _
                                                                                                                                   .GetBuhinSiaShukeiCodeFrom
            Return node.BuhinVo.SiaShukeiCode
        End Function

        Public Function GetBuhinGencyoCkdKbnFrom(ByVal node As BuhinNode(Of TShisakuBuhinKouseiVo, TShisakuBuhinVo)) As String Implements IBuhinNodeAccessor _
                                                                                                                                  (Of TShisakuBuhinKouseiVo, TShisakuBuhinVo) _
                                                                                                                                  .GetBuhinGencyoCkdKbnFrom
            Return node.BuhinVo.GencyoCkdKbn
        End Function

        Public Function GetShutuzuYoteiDateFrom(ByVal node As BuhinNode(Of TShisakuBuhinKouseiVo, TShisakuBuhinVo)) As Integer? Implements IBuhinNodeAccessor _
                                                                                                                                   (Of TShisakuBuhinKouseiVo, TShisakuBuhinVo) _
                                                                                                                                   .GetShutuzuYoteiDateFrom
            Return node.BuhinVo.ShutuzuYoteiDate
        End Function

        Public Function GetSaishiyoufuka(ByVal node As BuhinNode(Of TShisakuBuhinKouseiVo, TShisakuBuhinVo)) As String Implements IBuhinNodeAccessor _
                                                                                                                                  (Of TShisakuBuhinKouseiVo, TShisakuBuhinVo) _
                                                                                                                                  .GetSaishiyoufuka
            Return node.BuhinVo.Saishiyoufuka
        End Function

        '2012/01/23 供給セクション追加
        Public Function GetKyoukuSection(ByVal node As BuhinNode(Of TShisakuBuhinKouseiVo, TShisakuBuhinVo)) As String Implements IBuhinNodeAccessor _
                                                                                                                                  (Of TShisakuBuhinKouseiVo, TShisakuBuhinVo) _
                                                                                                                                  .GetKyoukuSection
            Return node.KoseiVo.KyoukuSection
        End Function
        '2014/09/23 酒井 ADD
        Public Function GetBaseBuhinFlg(ByVal node As BuhinNode(Of TShisakuBuhinKouseiVo, TShisakuBuhinVo)) As String Implements IBuhinNodeAccessor _
                                                                                                                                  (Of TShisakuBuhinKouseiVo, TShisakuBuhinVo) _
                                                                                                                                  .GetBaseBuhinFlg
            'Return node.KoseiVo.BaseBuhinFlg
            Return String.Empty
        End Function

        '2014/09/08 ADD
        Public Function GetBaseBuhinSeq(ByVal node As BuhinNode(Of TShisakuBuhinKouseiVo, TShisakuBuhinVo)) As Nullable(Of Int32) Implements IBuhinNodeAccessor _
                                                                                                                                  (Of TShisakuBuhinKouseiVo, TShisakuBuhinVo) _
                                                                                                                                  .GetBaseBuhinSeq
            'Return node.KoseiVo.BaseBuhinFlg
            Return Nothing
        End Function

        Public Function GetInstlDataKbn(ByVal node As Tree.BuhinNode(Of TShisakuBuhinKouseiVo, TShisakuBuhinVo)) As String Implements IBuhinNodeAccessor(Of TShisakuBuhinKouseiVo, TShisakuBuhinVo).GetInstlDataKbn
            'Return node.BuhinVo.BaseBuhinFlg
            Return String.Empty
        End Function

        '2012/02/10 部品ノート追加
        Public Function GetBuhinNote(ByVal node As BuhinNode(Of TShisakuBuhinKouseiVo, TShisakuBuhinVo)) As String Implements IBuhinNodeAccessor _
                                                                                                                                  (Of TShisakuBuhinKouseiVo, TShisakuBuhinVo) _
                                                                                                                                  .GetBuhinNote
            Return node.BuhinVo.BuhinNote
        End Function

    End Class
End Namespace