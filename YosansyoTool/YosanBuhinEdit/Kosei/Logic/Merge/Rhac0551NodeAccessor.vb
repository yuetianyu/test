Imports YosansyoTool.YosanBuhinEdit.Logic
Imports ShisakuCommon.Db.EBom.Vo

Namespace YosanBuhinEdit.Kosei.Logic.Merge

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

        'Public Function GetBukaCodeFrom(ByVal node As Tree.BuhinNode(Of ShisakuCommon.Db.EBom.Vo.Rhac0551Vo, ShisakuCommon.Db.EBom.Vo.Rhac0530Vo)) As String Implements IBuhinNodeAccessor(Of ShisakuCommon.Db.EBom.Vo.Rhac0551Vo, ShisakuCommon.Db.EBom.Vo.Rhac0530Vo).GetBukaCodeFrom
        '    Return node.BuhinVo.ShukeiBukaCode
        'End Function

        Public Function GetBuhinNameFrom(ByVal node As Tree.BuhinNode(Of ShisakuCommon.Db.EBom.Vo.Rhac0551Vo, ShisakuCommon.Db.EBom.Vo.Rhac0530Vo)) As String Implements IBuhinNodeAccessor(Of ShisakuCommon.Db.EBom.Vo.Rhac0551Vo, ShisakuCommon.Db.EBom.Vo.Rhac0530Vo).GetBuhinNameFrom
            Return node.BuhinVo.BuhinName
        End Function

        Public Function GetBuhinNoFrom(ByVal node As Tree.BuhinNode(Of ShisakuCommon.Db.EBom.Vo.Rhac0551Vo, ShisakuCommon.Db.EBom.Vo.Rhac0530Vo)) As String Implements IBuhinNodeAccessor(Of ShisakuCommon.Db.EBom.Vo.Rhac0551Vo, ShisakuCommon.Db.EBom.Vo.Rhac0530Vo).GetBuhinNoFrom
            Return node.KoseiVo.BuhinNoKo
        End Function

        'Public Function GetBlockNoFrom(ByVal node As Tree.BuhinNode(Of ShisakuCommon.Db.EBom.Vo.Rhac0551Vo, ShisakuCommon.Db.EBom.Vo.Rhac0530Vo)) As String Implements IBuhinNodeAccessor(Of ShisakuCommon.Db.EBom.Vo.Rhac0551Vo, ShisakuCommon.Db.EBom.Vo.Rhac0530Vo).GetBlockNoFrom
        '    Return node.BuhinVo.ShukeiCode
        'End Function

        Public Function GetInsuVoFrom(ByVal node As Tree.BuhinNode(Of ShisakuCommon.Db.EBom.Vo.Rhac0551Vo, ShisakuCommon.Db.EBom.Vo.Rhac0530Vo)) As String Implements IBuhinNodeAccessor(Of ShisakuCommon.Db.EBom.Vo.Rhac0551Vo, ShisakuCommon.Db.EBom.Vo.Rhac0530Vo).GetInsuVoFrom
            Return node.KoseiVo.InsuSuryo

        End Function

        'Public Function GetBuhinNoKbnFrom(ByVal node As Tree.BuhinNode(Of ShisakuCommon.Db.EBom.Vo.Rhac0551Vo, ShisakuCommon.Db.EBom.Vo.Rhac0530Vo)) As String Implements IBuhinNodeAccessor(Of ShisakuCommon.Db.EBom.Vo.Rhac0551Vo, ShisakuCommon.Db.EBom.Vo.Rhac0530Vo).GetBuhinNoKbnFrom
        '    Return node.KoseiVo.BuhinNoKo
        'End Function

        'Public Function GetMakerCodeFrom(ByVal node As Tree.BuhinNode(Of ShisakuCommon.Db.EBom.Vo.Rhac0551Vo, ShisakuCommon.Db.EBom.Vo.Rhac0530Vo)) As String Implements IBuhinNodeAccessor(Of ShisakuCommon.Db.EBom.Vo.Rhac0551Vo, ShisakuCommon.Db.EBom.Vo.Rhac0530Vo).GetMakerCodeFrom
        '    Return String.Empty

        'End Function

        'Public Function GetMakerNameFrom(ByVal node As Tree.BuhinNode(Of ShisakuCommon.Db.EBom.Vo.Rhac0551Vo, ShisakuCommon.Db.EBom.Vo.Rhac0530Vo)) As String Implements IBuhinNodeAccessor(Of ShisakuCommon.Db.EBom.Vo.Rhac0551Vo, ShisakuCommon.Db.EBom.Vo.Rhac0530Vo).GetMakerNameFrom
        '    Return String.Empty

        'End Function

        Public Function GetShukeiCodeFrom(ByVal node As Tree.BuhinNode(Of ShisakuCommon.Db.EBom.Vo.Rhac0551Vo, ShisakuCommon.Db.EBom.Vo.Rhac0530Vo)) As String Implements IBuhinNodeAccessor(Of ShisakuCommon.Db.EBom.Vo.Rhac0551Vo, ShisakuCommon.Db.EBom.Vo.Rhac0530Vo).GetBuhinShukeiCodeFrom
            Return node.KoseiVo.ShukeiCode
        End Function

        Public Function GetSiaShukeiCodeFrom(ByVal node As Tree.BuhinNode(Of ShisakuCommon.Db.EBom.Vo.Rhac0551Vo, ShisakuCommon.Db.EBom.Vo.Rhac0530Vo)) As String Implements IBuhinNodeAccessor(Of ShisakuCommon.Db.EBom.Vo.Rhac0551Vo, ShisakuCommon.Db.EBom.Vo.Rhac0530Vo).GetBuhinSiaShukeiCodeFrom
            Return node.KoseiVo.SiaShukeiCode
        End Function

        Public Function GetKyoukuSection(ByVal node As Tree.BuhinNode(Of ShisakuCommon.Db.EBom.Vo.Rhac0551Vo, ShisakuCommon.Db.EBom.Vo.Rhac0530Vo)) As String Implements IBuhinNodeAccessor(Of ShisakuCommon.Db.EBom.Vo.Rhac0551Vo, ShisakuCommon.Db.EBom.Vo.Rhac0530Vo).GetKyoukuSection
            '見当たらないので'  ＝＝＞供給セクションはどこから取得するのか
            Return String.Empty
        End Function

        Public Function GetBuhinNote(ByVal node As Tree.BuhinNode(Of ShisakuCommon.Db.EBom.Vo.Rhac0551Vo, ShisakuCommon.Db.EBom.Vo.Rhac0530Vo)) As String Implements IBuhinNodeAccessor(Of ShisakuCommon.Db.EBom.Vo.Rhac0551Vo, ShisakuCommon.Db.EBom.Vo.Rhac0530Vo).GetBuhinNote
            Return String.Empty
        End Function

    End Class
End Namespace