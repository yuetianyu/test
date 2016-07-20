Imports YosansyoTool.YosanBuhinEdit.Logic
Imports ShisakuCommon.Db.EBom.Vo

Namespace YosanBuhinEdit.Kosei.Logic.Merge

    Public Class Rhac0553NodeAccessor : Implements IBuhinNodeAccessor(Of Rhac0553Vo, Rhac0533Vo)

        Private ReadOnly aMakerNameResolver As MakerNameResolver

        ''' <summary>
        ''' コンストラクタ
        ''' </summary>
        ''' <param name="aMakerNameResolver">取引先名を解決するInterface</param>
        ''' <remarks></remarks>
        Public Sub New(ByVal aMakerNameResolver As MakerNameResolver)
            Me.aMakerNameResolver = aMakerNameResolver
        End Sub

        Public Function GetBuhinNoFrom(ByVal node As Tree.BuhinNode(Of ShisakuCommon.Db.EBom.Vo.Rhac0553Vo, ShisakuCommon.Db.EBom.Vo.Rhac0533Vo)) As String Implements IBuhinNodeAccessor(Of ShisakuCommon.Db.EBom.Vo.Rhac0553Vo, ShisakuCommon.Db.EBom.Vo.Rhac0533Vo).GetBuhinNoFrom
            Return node.BuhinVo.BuhinNo
        End Function

        Public Function GetBuhinNameFrom(ByVal node As Tree.BuhinNode(Of ShisakuCommon.Db.EBom.Vo.Rhac0553Vo, ShisakuCommon.Db.EBom.Vo.Rhac0533Vo)) As String Implements IBuhinNodeAccessor(Of ShisakuCommon.Db.EBom.Vo.Rhac0553Vo, ShisakuCommon.Db.EBom.Vo.Rhac0533Vo).GetBuhinNameFrom
            Return node.BuhinVo.BuhinName
        End Function

        'Public Function GetBuhinNoKbnFrom(ByVal node As Tree.BuhinNode(Of ShisakuCommon.Db.EBom.Vo.Rhac0553Vo, ShisakuCommon.Db.EBom.Vo.Rhac0533Vo)) As String Implements IBuhinNodeAccessor(Of ShisakuCommon.Db.EBom.Vo.Rhac0553Vo, ShisakuCommon.Db.EBom.Vo.Rhac0533Vo).GetBuhinNoKbnFrom
        '    Return node.KoseiVo
        'End Function

        'Public Function GetBlockNoFrom(ByVal node As Tree.BuhinNode(Of ShisakuCommon.Db.EBom.Vo.Rhac0553Vo, ShisakuCommon.Db.EBom.Vo.Rhac0533Vo)) As String Implements IBuhinNodeAccessor(Of ShisakuCommon.Db.EBom.Vo.Rhac0553Vo, ShisakuCommon.Db.EBom.Vo.Rhac0533Vo).GetBlockNoFrom
        '    '見当たらないので'
        '    Return node.BuhinVo.
        'End Function

        Public Function GetInsuVoFrom(ByVal node As Tree.BuhinNode(Of ShisakuCommon.Db.EBom.Vo.Rhac0553Vo, ShisakuCommon.Db.EBom.Vo.Rhac0533Vo)) As String Implements IBuhinNodeAccessor(Of ShisakuCommon.Db.EBom.Vo.Rhac0553Vo, ShisakuCommon.Db.EBom.Vo.Rhac0533Vo).GetInsuVoFrom
            Return node.KoseiVo.InsuSuryo
        End Function

        'Public Function GetGencyoCkdKbnFrom(ByVal node As Tree.BuhinNode(Of ShisakuCommon.Db.EBom.Vo.Rhac0553Vo, ShisakuCommon.Db.EBom.Vo.Rhac0533Vo)) As String Implements IBuhinNodeAccessor(Of ShisakuCommon.Db.EBom.Vo.Rhac0553Vo, ShisakuCommon.Db.EBom.Vo.Rhac0533Vo).GetGencyoCkdKbnFrom
        '    Return node.KoseiVo.GencyoCkdKbn
        'End Function

        'Public Function GetMakerCodeFrom(ByVal node As Tree.BuhinNode(Of ShisakuCommon.Db.EBom.Vo.Rhac0553Vo, ShisakuCommon.Db.EBom.Vo.Rhac0533Vo)) As String Implements IBuhinNodeAccessor(Of ShisakuCommon.Db.EBom.Vo.Rhac0553Vo, ShisakuCommon.Db.EBom.Vo.Rhac0533Vo).GetMakerCodeFrom
        '    Return String.Empty
        'End Function

        'Public Function GetMakerNameFrom(ByVal node As Tree.BuhinNode(Of ShisakuCommon.Db.EBom.Vo.Rhac0553Vo, ShisakuCommon.Db.EBom.Vo.Rhac0533Vo)) As String Implements IBuhinNodeAccessor(Of ShisakuCommon.Db.EBom.Vo.Rhac0553Vo, ShisakuCommon.Db.EBom.Vo.Rhac0533Vo).GetMakerNameFrom
        '    Return String.Empty
        'End Function

        Public Function GetShukeiCodeFrom(ByVal node As Tree.BuhinNode(Of ShisakuCommon.Db.EBom.Vo.Rhac0553Vo, ShisakuCommon.Db.EBom.Vo.Rhac0533Vo)) As String Implements IBuhinNodeAccessor(Of ShisakuCommon.Db.EBom.Vo.Rhac0553Vo, ShisakuCommon.Db.EBom.Vo.Rhac0533Vo).GetBuhinShukeiCodeFrom
            Return node.KoseiVo.ShukeiCode
        End Function

        Public Function GetSiaShukeiCodeFrom(ByVal node As Tree.BuhinNode(Of ShisakuCommon.Db.EBom.Vo.Rhac0553Vo, ShisakuCommon.Db.EBom.Vo.Rhac0533Vo)) As String Implements IBuhinNodeAccessor(Of ShisakuCommon.Db.EBom.Vo.Rhac0553Vo, ShisakuCommon.Db.EBom.Vo.Rhac0533Vo).GetBuhinSiaShukeiCodeFrom
            Return node.KoseiVo.SiaShukeiCode
        End Function

        Public Function GetKyoukuSection(ByVal node As Tree.BuhinNode(Of ShisakuCommon.Db.EBom.Vo.Rhac0553Vo, ShisakuCommon.Db.EBom.Vo.Rhac0533Vo)) As String Implements IBuhinNodeAccessor(Of ShisakuCommon.Db.EBom.Vo.Rhac0553Vo, ShisakuCommon.Db.EBom.Vo.Rhac0533Vo).GetKyoukuSection
            '見当たらないので'    ＝＝＞供給セクションはどこから取得するのか
            Return String.Empty
        End Function
        Public Function GetBuhinNote(ByVal node As Tree.BuhinNode(Of ShisakuCommon.Db.EBom.Vo.Rhac0553Vo, ShisakuCommon.Db.EBom.Vo.Rhac0533Vo)) As String Implements IBuhinNodeAccessor(Of ShisakuCommon.Db.EBom.Vo.Rhac0553Vo, ShisakuCommon.Db.EBom.Vo.Rhac0533Vo).GetBuhinNote
            Return node.BuhinVo.ZumenNote
        End Function

    End Class
End Namespace