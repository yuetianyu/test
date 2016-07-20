Imports ShisakuCommon.Db.EBom.Vo

Namespace YosanEventList.Dao

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <remarks></remarks>
    Public Interface YosanEventListDao

        ''' <summary>
        ''' 予算書イベント別年月別財務実績情報
        ''' </summary>
        ''' <param name="yosanZaimuJisekiYyyyMm">財務実績計上年月</param>
        ''' <returns>予算書イベント別年月別財務実績情報</returns>
        ''' <remarks></remarks>
        Function FindYosanZaimuJiseki(ByVal yosanZaimuJisekiYyyyMm As String) As List(Of TYosanZaimuJisekiVo)

        ''' <summary>
        ''' 予算書部品編集情報
        ''' </summary>
        ''' <param name="buhinNo">部品番号</param>
        ''' <remarks></remarks>
        Function FindYosanBuhinEdit(ByVal buhinNo As String) As List(Of TYosanBuhinEditVo)

    End Interface

End Namespace