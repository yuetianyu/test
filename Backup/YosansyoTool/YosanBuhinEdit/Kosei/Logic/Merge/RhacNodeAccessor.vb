Imports ShisakuCommon.Db.EBom.Vo
Imports YosansyoTool.YosanBuhinEdit.Logic
Imports YosansyoTool.YosanBuhinEdit.Kosei.Logic.Tree

Namespace YosanBuhinEdit.Kosei.Logic.Merge
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

        Public Function GetInsuVoFrom(ByVal node As BuhinNode(Of Rhac0552Vo, Rhac0532Vo)) As String Implements IBuhinNodeAccessor _
                                                                                                                          (Of Rhac0552Vo, Rhac0532Vo) _
                                                                                                                           .GetInsuVoFrom

            Return node.KoseiVo.InsuSuryo
        End Function

        'Public Function GetMakerNameFrom(ByVal node As BuhinNode(Of Rhac0552Vo, Rhac0532Vo)) As String Implements IBuhinNodeAccessor _
        '                                                                                                                  (Of Rhac0552Vo, Rhac0532Vo) _
        '                                                                                                                  .GetMakerNameFrom


        '    Return aMakerNameResolver.Resolve(node.BuhinVo.MakerCode)
        'End Function

        Public Function GetShukeiCodeFrom(ByVal node As BuhinNode(Of Rhac0552Vo, Rhac0532Vo)) As String Implements IBuhinNodeAccessor _
                                                                                                                           (Of Rhac0552Vo, Rhac0532Vo) _
                                                                                                                           .GetBuhinShukeiCodeFrom
            Return node.KoseiVo.ShukeiCode
        End Function

        Public Function GetSiaShukeiCodeFrom(ByVal node As BuhinNode(Of Rhac0552Vo, Rhac0532Vo)) As String Implements IBuhinNodeAccessor _
                                                                                                                              (Of Rhac0552Vo, Rhac0532Vo) _
                                                                                                                              .GetBuhinSiaShukeiCodeFrom
            Return node.KoseiVo.SiaShukeiCode
        End Function

        'Public Function GetGencyoCkdKbnFrom(ByVal node As BuhinNode(Of Rhac0552Vo, Rhac0532Vo)) As String Implements IBuhinNodeAccessor _
        '                                                                                                                     (Of Rhac0552Vo, Rhac0532Vo) _
        '                                                                                                                     .GetGencyoCkdKbnFrom
        '    Return node.KoseiVo.GencyoCkdKbn
        'End Function

        'Public Function GetBuhinShukeiCodeFrom(ByVal node As BuhinNode(Of Rhac0552Vo, Rhac0532Vo)) As String Implements IBuhinNodeAccessor _
        '                                                                                                                        (Of Rhac0552Vo, Rhac0532Vo) _
        '                                                                                                                        .GetBuhinShukeiCodeFrom
        '    Return node.BuhinVo.ShukeiCode
        'End Function

        'Public Function GetBuhinSiaShukeiCodeFrom(ByVal node As BuhinNode(Of Rhac0552Vo, Rhac0532Vo)) As String Implements IBuhinNodeAccessor _
        '                                                                                                                           (Of Rhac0552Vo, Rhac0532Vo) _
        '                                                                                                                           .GetBuhinSiaShukeiCodeFrom
        '    Return node.BuhinVo.SiaShukeiCode
        'End Function

        'Public Function GetBuhinGencyoCkdKbnFrom(ByVal node As BuhinNode(Of Rhac0552Vo, Rhac0532Vo)) As String Implements IBuhinNodeAccessor _
        '                                                                                                                          (Of Rhac0552Vo, Rhac0532Vo) _
        '                                                                                                                          .GetBuhinGencyoCkdKbnFrom
        '    Return node.BuhinVo.GencyoCkdKbn
        'End Function

        Public Function GetKyoukuSection(ByVal node As Tree.BuhinNode(Of Rhac0552Vo, Rhac0532Vo)) As String Implements IBuhinNodeAccessor(Of Rhac0552Vo, Rhac0532Vo).GetKyoukuSection
            '見当たらないので'  ＝＝＞供給セクションはどこから取得するのか
            Return String.Empty
        End Function

        Public Function GetBuhinNote(ByVal node As Tree.BuhinNode(Of Rhac0552Vo, Rhac0532Vo)) As String Implements IBuhinNodeAccessor(Of Rhac0552Vo, Rhac0532Vo).GetBuhinNote
            '見当たらないので'  ＝＝＞供給セクションはどこから取得するのか
            Return node.BuhinVo.ChukiKijutsu
        End Function
    End Class
End Namespace