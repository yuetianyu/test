Imports EventSakusei.ShisakuBuhinEdit.Logic
Imports ShisakuCommon.Db.EBom.Vo
Imports EventSakusei.ShisakuBuhinEdit.Kosei.Logic.Tree

Namespace ShisakuBuhinEdit.Kosei.Logic.Merge

    Public Class Rhac0551NodeAccessor : Implements IBuhinNodeAccessor(Of Rhac0551Vo, Rhac0530Vo)


        Private ReadOnly aMakerNameResolver As MakerNameResolver

        ''' <summary>
        ''' コンストラクタ
        ''' </summary>
        ''' <param name="aMakerNameResolver">取引先名を解決するInterface</param>
        ''' <remarks></remarks>
        Public Sub New(ByVal aMakerNameResolver As MakerNameResolver)
            Me.aMakerNameResolver = aMakerNameResolver
        End Sub

        Public Function GetBuhinGencyoCkdKbnFrom(ByVal node As Tree.BuhinNode(Of ShisakuCommon.Db.EBom.Vo.Rhac0551Vo, ShisakuCommon.Db.EBom.Vo.Rhac0530Vo)) As String Implements IBuhinNodeAccessor(Of ShisakuCommon.Db.EBom.Vo.Rhac0551Vo, ShisakuCommon.Db.EBom.Vo.Rhac0530Vo).GetBuhinGencyoCkdKbnFrom
            Return node.KoseiVo.GencyoCkdKbn
        End Function

        Public Function GetBuhinNameFrom(ByVal node As Tree.BuhinNode(Of ShisakuCommon.Db.EBom.Vo.Rhac0551Vo, ShisakuCommon.Db.EBom.Vo.Rhac0530Vo)) As String Implements IBuhinNodeAccessor(Of ShisakuCommon.Db.EBom.Vo.Rhac0551Vo, ShisakuCommon.Db.EBom.Vo.Rhac0530Vo).GetBuhinNameFrom
            Return node.BuhinVo.BuhinName
        End Function

        Public Function GetBuhinNoFrom(ByVal node As Tree.BuhinNode(Of ShisakuCommon.Db.EBom.Vo.Rhac0551Vo, ShisakuCommon.Db.EBom.Vo.Rhac0530Vo)) As String Implements IBuhinNodeAccessor(Of ShisakuCommon.Db.EBom.Vo.Rhac0551Vo, ShisakuCommon.Db.EBom.Vo.Rhac0530Vo).GetBuhinNoFrom
            Return node.KoseiVo.BuhinNoKo
        End Function

        Public Function GetBuhinNoKaiteiNoFrom(ByVal node As Tree.BuhinNode(Of ShisakuCommon.Db.EBom.Vo.Rhac0551Vo, ShisakuCommon.Db.EBom.Vo.Rhac0530Vo)) As String Implements IBuhinNodeAccessor(Of ShisakuCommon.Db.EBom.Vo.Rhac0551Vo, ShisakuCommon.Db.EBom.Vo.Rhac0530Vo).GetBuhinNoKaiteiNoFrom
            Return String.Empty
        End Function

        Public Function GetBuhinNoKbnFrom(ByVal node As Tree.BuhinNode(Of ShisakuCommon.Db.EBom.Vo.Rhac0551Vo, ShisakuCommon.Db.EBom.Vo.Rhac0530Vo)) As String Implements IBuhinNodeAccessor(Of ShisakuCommon.Db.EBom.Vo.Rhac0551Vo, ShisakuCommon.Db.EBom.Vo.Rhac0530Vo).GetBuhinNoKbnFrom
            Return String.Empty
        End Function

        Public Function GetBuhinShukeiCodeFrom(ByVal node As Tree.BuhinNode(Of ShisakuCommon.Db.EBom.Vo.Rhac0551Vo, ShisakuCommon.Db.EBom.Vo.Rhac0530Vo)) As String Implements IBuhinNodeAccessor(Of ShisakuCommon.Db.EBom.Vo.Rhac0551Vo, ShisakuCommon.Db.EBom.Vo.Rhac0530Vo).GetBuhinShukeiCodeFrom
            Return node.BuhinVo.ShukeiCode
            '見当たらないので'
            'Return String.Empty
        End Function

        Public Function GetBuhinSiaShukeiCodeFrom(ByVal node As Tree.BuhinNode(Of ShisakuCommon.Db.EBom.Vo.Rhac0551Vo, ShisakuCommon.Db.EBom.Vo.Rhac0530Vo)) As String Implements IBuhinNodeAccessor(Of ShisakuCommon.Db.EBom.Vo.Rhac0551Vo, ShisakuCommon.Db.EBom.Vo.Rhac0530Vo).GetBuhinSiaShukeiCodeFrom
            Return node.BuhinVo.SiaShukeiCode
            'Return String.Empty

        End Function

        Public Function GetEdaBanFrom(ByVal node As Tree.BuhinNode(Of ShisakuCommon.Db.EBom.Vo.Rhac0551Vo, ShisakuCommon.Db.EBom.Vo.Rhac0530Vo)) As String Implements IBuhinNodeAccessor(Of ShisakuCommon.Db.EBom.Vo.Rhac0551Vo, ShisakuCommon.Db.EBom.Vo.Rhac0530Vo).GetEdaBanFrom
            Return String.Empty

        End Function

        Public Function GetGencyoCkdKbnFrom(ByVal node As Tree.BuhinNode(Of ShisakuCommon.Db.EBom.Vo.Rhac0551Vo, ShisakuCommon.Db.EBom.Vo.Rhac0530Vo)) As String Implements IBuhinNodeAccessor(Of ShisakuCommon.Db.EBom.Vo.Rhac0551Vo, ShisakuCommon.Db.EBom.Vo.Rhac0530Vo).GetGencyoCkdKbnFrom
            Return node.KoseiVo.GencyoCkdKbn
        End Function

        Public Function GetMakerCodeFrom(ByVal node As Tree.BuhinNode(Of ShisakuCommon.Db.EBom.Vo.Rhac0551Vo, ShisakuCommon.Db.EBom.Vo.Rhac0530Vo)) As String Implements IBuhinNodeAccessor(Of ShisakuCommon.Db.EBom.Vo.Rhac0551Vo, ShisakuCommon.Db.EBom.Vo.Rhac0530Vo).GetMakerCodeFrom
            Return String.Empty

        End Function

        Public Function GetMakerNameFrom(ByVal node As Tree.BuhinNode(Of ShisakuCommon.Db.EBom.Vo.Rhac0551Vo, ShisakuCommon.Db.EBom.Vo.Rhac0530Vo)) As String Implements IBuhinNodeAccessor(Of ShisakuCommon.Db.EBom.Vo.Rhac0551Vo, ShisakuCommon.Db.EBom.Vo.Rhac0530Vo).GetMakerNameFrom
            Return String.Empty

        End Function

        Public Function GetShukeiCodeFrom(ByVal node As Tree.BuhinNode(Of ShisakuCommon.Db.EBom.Vo.Rhac0551Vo, ShisakuCommon.Db.EBom.Vo.Rhac0530Vo)) As String Implements IBuhinNodeAccessor(Of ShisakuCommon.Db.EBom.Vo.Rhac0551Vo, ShisakuCommon.Db.EBom.Vo.Rhac0530Vo).GetShukeiCodeFrom
            Return node.KoseiVo.ShukeiCode
        End Function

        Public Function GetShutuzuYoteiDateFrom(ByVal node As Tree.BuhinNode(Of ShisakuCommon.Db.EBom.Vo.Rhac0551Vo, ShisakuCommon.Db.EBom.Vo.Rhac0530Vo)) As Integer? Implements IBuhinNodeAccessor(Of ShisakuCommon.Db.EBom.Vo.Rhac0551Vo, ShisakuCommon.Db.EBom.Vo.Rhac0530Vo).GetShutuzuYoteiDateFrom
            Return node.BuhinVo.ShutuzuYoteiDate
            '見当たらないので'
            'Return 0
        End Function

        Public Function GetSiaShukeiCodeFrom(ByVal node As Tree.BuhinNode(Of ShisakuCommon.Db.EBom.Vo.Rhac0551Vo, ShisakuCommon.Db.EBom.Vo.Rhac0530Vo)) As String Implements IBuhinNodeAccessor(Of ShisakuCommon.Db.EBom.Vo.Rhac0551Vo, ShisakuCommon.Db.EBom.Vo.Rhac0530Vo).GetSiaShukeiCodeFrom
            Return node.KoseiVo.SiaShukeiCode
        End Function

        Public Function GetSaishiyoufuka(ByVal node As Tree.BuhinNode(Of ShisakuCommon.Db.EBom.Vo.Rhac0551Vo, ShisakuCommon.Db.EBom.Vo.Rhac0530Vo)) As String Implements IBuhinNodeAccessor(Of ShisakuCommon.Db.EBom.Vo.Rhac0551Vo, ShisakuCommon.Db.EBom.Vo.Rhac0530Vo).GetSaishiyoufuka
            'Return node.BuhinVo.Saishiyoufuka
            '見当たらないので'
            Return String.Empty
        End Function

        '2012/01/23 供給セクション追加
        Public Function GetKyoukuSection(ByVal node As Tree.BuhinNode(Of ShisakuCommon.Db.EBom.Vo.Rhac0551Vo, ShisakuCommon.Db.EBom.Vo.Rhac0530Vo)) As String Implements IBuhinNodeAccessor(Of ShisakuCommon.Db.EBom.Vo.Rhac0551Vo, ShisakuCommon.Db.EBom.Vo.Rhac0530Vo).GetKyoukuSection
            'Return node.BuhinVo.KyoukuSection
            '見当たらないので'  ＝＝＞供給セクションはどこから取得するのか
            Return String.Empty
        End Function
        '2014/09/23 酒井 ADD
        Public Function GetBaseBuhinFlg(ByVal node As Tree.BuhinNode(Of ShisakuCommon.Db.EBom.Vo.Rhac0551Vo, ShisakuCommon.Db.EBom.Vo.Rhac0530Vo)) As String Implements IBuhinNodeAccessor(Of ShisakuCommon.Db.EBom.Vo.Rhac0551Vo, ShisakuCommon.Db.EBom.Vo.Rhac0530Vo).GetBaseBuhinFlg
            'Return node.BuhinVo.BaseBuhinFlg
            Return String.Empty
        End Function

        '2014/09/08  ADD
        Public Function GetBaseBuhinSeq(ByVal node As Tree.BuhinNode(Of ShisakuCommon.Db.EBom.Vo.Rhac0551Vo, ShisakuCommon.Db.EBom.Vo.Rhac0530Vo)) As Nullable(Of Int32) Implements IBuhinNodeAccessor(Of ShisakuCommon.Db.EBom.Vo.Rhac0551Vo, ShisakuCommon.Db.EBom.Vo.Rhac0530Vo).GetBaseBuhinSeq
            'Return node.BuhinVo.BaseBuhinFlg
            Return Nothing
        End Function

        Public Function GetInstlDataKbn(ByVal node As Tree.BuhinNode(Of ShisakuCommon.Db.EBom.Vo.Rhac0551Vo, ShisakuCommon.Db.EBom.Vo.Rhac0530Vo)) As String Implements IBuhinNodeAccessor(Of ShisakuCommon.Db.EBom.Vo.Rhac0551Vo, ShisakuCommon.Db.EBom.Vo.Rhac0530Vo).GetInstlDataKbn
            'Return node.BuhinVo.BaseBuhinFlg
            Return String.Empty
        End Function

        '2012/02/10 部品ノート追加
        Public Function GetBuhinNote(ByVal node As Tree.BuhinNode(Of ShisakuCommon.Db.EBom.Vo.Rhac0551Vo, ShisakuCommon.Db.EBom.Vo.Rhac0530Vo)) As String Implements IBuhinNodeAccessor(Of ShisakuCommon.Db.EBom.Vo.Rhac0551Vo, ShisakuCommon.Db.EBom.Vo.Rhac0530Vo).GetBuhinNote
            Return String.Empty
        End Function

    End Class
End Namespace