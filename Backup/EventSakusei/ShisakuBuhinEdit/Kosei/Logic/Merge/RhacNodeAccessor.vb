Imports EventSakusei.ShisakuBuhinEdit.Logic
Imports ShisakuCommon.Db.EBom.Vo
Imports EventSakusei.ShisakuBuhinEdit.Kosei.Logic.Tree

Namespace ShisakuBuhinEdit.Kosei.Logic.Merge
    ''' <summary>
    ''' EBomテーブル用のNodeアクセッサ実装クラス
    ''' </summary>
    ''' <remarks></remarks>
    Public Class RhacNodeAccessor : Implements IBuhinNodeAccessor(Of Rhac0552Vo, Rhac0532Vo)

        Private ReadOnly aMakerNameResolver As MakerNameResolver
        ''' <summary>
        ''' コンストラクタ
        ''' </summary>
        ''' <param name="aMakerNameResolver">取引先名を解決するInterface</param>
        ''' <remarks></remarks>
        Public Sub New(ByVal aMakerNameResolver As MakerNameResolver)
            Me.aMakerNameResolver = aMakerNameResolver
        End Sub

        Public Function GetBuhinNoFrom(ByVal node As BuhinNode(Of Rhac0552Vo, Rhac0532Vo)) As String Implements IBuhinNodeAccessor _
                                                                                                        (Of Rhac0552Vo, Rhac0532Vo) _
                                                                                                        .GetBuhinNoFrom
            Return node.KoseiVo.BuhinNoKo
        End Function

        Public Function GetBuhinNameFrom(ByVal node As BuhinNode(Of Rhac0552Vo, Rhac0532Vo)) As String Implements IBuhinNodeAccessor _
                                                                                                                          (Of Rhac0552Vo, Rhac0532Vo) _
                                                                                                                          .GetBuhinNameFrom
            Return node.BuhinVo.BuhinName
        End Function

        Public Function GetBuhinNoKbnFrom(ByVal node As BuhinNode(Of Rhac0552Vo, Rhac0532Vo)) As String Implements IBuhinNodeAccessor _
                                                                                                                           (Of Rhac0552Vo, Rhac0532Vo) _
                                                                                                                           .GetBuhinNoKbnFrom
            Return String.Empty
        End Function

        Public Function GetBuhinNoKaiteiNoFrom(ByVal node As BuhinNode(Of Rhac0552Vo, Rhac0532Vo)) As String Implements IBuhinNodeAccessor _
                                                                                                                                (Of Rhac0552Vo, Rhac0532Vo) _
                                                                                                                                .GetBuhinNoKaiteiNoFrom
            Return String.Empty
        End Function

        Public Function GetEdaBanFrom(ByVal node As BuhinNode(Of Rhac0552Vo, Rhac0532Vo)) As String Implements IBuhinNodeAccessor _
                                                                                                                       (Of Rhac0552Vo, Rhac0532Vo) _
                                                                                                                       .GetEdaBanFrom
            Return String.Empty
        End Function

        Public Function GetMakerCodeFrom(ByVal node As BuhinNode(Of Rhac0552Vo, Rhac0532Vo)) As String Implements IBuhinNodeAccessor _
                                                                                                                          (Of Rhac0552Vo, Rhac0532Vo) _
                                                                                                                          .GetMakerCodeFrom
            '　NOTHINGが帰ってきたので　樺澤'
            'If node.BuhinVo Is Nothing Then
            '    Dim str As String = ""
            '    Return str
            'End If


            Return node.BuhinVo.MakerCode
        End Function

        Public Function GetMakerNameFrom(ByVal node As BuhinNode(Of Rhac0552Vo, Rhac0532Vo)) As String Implements IBuhinNodeAccessor _
                                                                                                                          (Of Rhac0552Vo, Rhac0532Vo) _
                                                                                                                          .GetMakerNameFrom


            Return aMakerNameResolver.Resolve(node.BuhinVo.MakerCode)
        End Function

        Public Function GetShukeiCodeFrom(ByVal node As BuhinNode(Of Rhac0552Vo, Rhac0532Vo)) As String Implements IBuhinNodeAccessor _
                                                                                                                           (Of Rhac0552Vo, Rhac0532Vo) _
                                                                                                                           .GetShukeiCodeFrom
            Return node.KoseiVo.ShukeiCode
        End Function

        Public Function GetSiaShukeiCodeFrom(ByVal node As BuhinNode(Of Rhac0552Vo, Rhac0532Vo)) As String Implements IBuhinNodeAccessor _
                                                                                                                              (Of Rhac0552Vo, Rhac0532Vo) _
                                                                                                                              .GetSiaShukeiCodeFrom
            Return node.KoseiVo.SiaShukeiCode
        End Function

        Public Function GetGencyoCkdKbnFrom(ByVal node As BuhinNode(Of Rhac0552Vo, Rhac0532Vo)) As String Implements IBuhinNodeAccessor _
                                                                                                                             (Of Rhac0552Vo, Rhac0532Vo) _
                                                                                                                             .GetGencyoCkdKbnFrom
            Return node.KoseiVo.GencyoCkdKbn
        End Function

        Public Function GetBuhinShukeiCodeFrom(ByVal node As BuhinNode(Of Rhac0552Vo, Rhac0532Vo)) As String Implements IBuhinNodeAccessor _
                                                                                                                                (Of Rhac0552Vo, Rhac0532Vo) _
                                                                                                                                .GetBuhinShukeiCodeFrom
            Return node.BuhinVo.ShukeiCode
        End Function

        Public Function GetBuhinSiaShukeiCodeFrom(ByVal node As BuhinNode(Of Rhac0552Vo, Rhac0532Vo)) As String Implements IBuhinNodeAccessor _
                                                                                                                                   (Of Rhac0552Vo, Rhac0532Vo) _
                                                                                                                                   .GetBuhinSiaShukeiCodeFrom
            Return node.BuhinVo.SiaShukeiCode
        End Function

        Public Function GetBuhinGencyoCkdKbnFrom(ByVal node As BuhinNode(Of Rhac0552Vo, Rhac0532Vo)) As String Implements IBuhinNodeAccessor _
                                                                                                                                  (Of Rhac0552Vo, Rhac0532Vo) _
                                                                                                                                  .GetBuhinGencyoCkdKbnFrom
            Return node.BuhinVo.GencyoCkdKbn
        End Function

        Public Function GetShutuzuYoteiDateFrom(ByVal node As BuhinNode(Of Rhac0552Vo, Rhac0532Vo)) As Integer? Implements IBuhinNodeAccessor _
                                                                                                                                   (Of Rhac0552Vo, Rhac0532Vo) _
                                                                                                                                   .GetShutuzuYoteiDateFrom
            Return node.BuhinVo.ShutuzuYoteiDate
        End Function

        Public Function GetSaishiyoufuka(ByVal node As Tree.BuhinNode(Of Rhac0552Vo, Rhac0532Vo)) As String Implements IBuhinNodeAccessor(Of Rhac0552Vo, Rhac0532Vo).GetSaishiyoufuka
            'Return node.BuhinVo.Saishiyoufuka
            '見当たらないので'
            Return String.Empty
        End Function

        '2012/01/23 供給セクション追加
        Public Function GetKyoukuSection(ByVal node As Tree.BuhinNode(Of Rhac0552Vo, Rhac0532Vo)) As String Implements IBuhinNodeAccessor(Of Rhac0552Vo, Rhac0532Vo).GetKyoukuSection
            'Return node.BuhinVo.KyoukuSection
            '見当たらないので'  ＝＝＞供給セクションはどこから取得するのか
            Return String.Empty
        End Function
        '2014/09/23 酒井 ADD
        Public Function GetBaseBuhinFlg(ByVal node As Tree.BuhinNode(Of Rhac0552Vo, Rhac0532Vo)) As String Implements IBuhinNodeAccessor(Of Rhac0552Vo, Rhac0532Vo).GetBaseBuhinFlg
            'Return node.BuhinVo.BaseBuhinFlg
            Return String.Empty
        End Function

        '2014/09/08 ADD
        Public Function GetBaseBuhinSeq(ByVal node As Tree.BuhinNode(Of Rhac0552Vo, Rhac0532Vo)) As Nullable(Of Int32) Implements IBuhinNodeAccessor(Of Rhac0552Vo, Rhac0532Vo).GetBaseBuhinSeq
            'Return node.BuhinVo.BaseBuhinFlg
            Return Nothing
        End Function

        Public Function GetInstlDataKbn(ByVal node As Tree.BuhinNode(Of Rhac0552Vo, Rhac0532Vo)) As String Implements IBuhinNodeAccessor(Of Rhac0552Vo, Rhac0532Vo).GetInstlDataKbn
            'Return node.BuhinVo.BaseBuhinFlg
            Return String.Empty
        End Function

        Public Function GetBuhinNote(ByVal node As Tree.BuhinNode(Of Rhac0552Vo, Rhac0532Vo)) As String Implements IBuhinNodeAccessor(Of Rhac0552Vo, Rhac0532Vo).GetBuhinNote
            'Return node.BuhinVo.KyoukuSection
            '見当たらないので'  ＝＝＞供給セクションはどこから取得するのか
            Return node.BuhinVo.ChukiKijutsu
        End Function
    End Class
End Namespace