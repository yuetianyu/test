Imports ShisakuCommon.Db.EBom.Vo

Namespace ShisakuBuhinEdit.Al.Logic

    ''' <summary>
    ''' 装備品列表示情報
    ''' </summary>
    ''' <remarks></remarks>
    Public Class BuhinEditAlShowColumnBag
        ''' <summary>試作設計ブロック装備情報</summary>
        Public SoubiVos As List(Of TShisakuSekkeiBlockSoubiVo)
        ''' <summary>試作設計ブロック装備仕様情報</summary>
        Public SoubiShiyouVos As List(Of TShisakuSekkeiBlockSoubiShiyouVo)

        ''' <summary>
        ''' コンストラクタ
        ''' </summary>
        ''' <remarks></remarks>
        Public Sub New()
            Me.New(New List(Of TShisakuSekkeiBlockSoubiVo), New List(Of TShisakuSekkeiBlockSoubiShiyouVo))
        End Sub
        ''' <summary>
        ''' コンストラクタ
        ''' </summary>
        ''' <param name="SoubiVos">試作設計ブロック装備情報</param>
        ''' <param name="SoubiShiyouVos">試作設計ブロック装備仕様情報</param>
        ''' <remarks></remarks>
        Public Sub New(ByVal SoubiVos As List(Of TShisakuSekkeiBlockSoubiVo), ByVal SoubiShiyouVos As List(Of TShisakuSekkeiBlockSoubiShiyouVo))
            Me.SoubiVos = SoubiVos
            Me.SoubiShiyouVos = SoubiShiyouVos
        End Sub
    End Class
End Namespace