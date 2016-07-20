Imports ShisakuCommon.Db.EBom.Vo

Namespace YosanEventListExcel.Dao

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <remarks></remarks>
    Public Interface YosanEventListExcelDao

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

        ''' <summary>
        ''' 予算書イベント情報取得
        ''' </summary>
        ''' <param name="yosanEventCode">予算書イベントコード</param>
        ''' <returns>予算書イベント情報</returns>
        ''' <remarks></remarks>
        Function FindYosanEvent(ByVal yosanEventCode As String) As TYosanEventVo

        ''' <summary>
        ''' 予算書部品編集情報取得
        ''' </summary>
        ''' <param name="yosanEventCode">予算書イベントコード</param>
        ''' <param name="yosanUnitKbn">ユニット区分</param>
        ''' <returns>予算書部品編集情報</returns>
        ''' <remarks></remarks>
        Function FindYosanBuhinEdit(ByVal yosanEventCode As String, ByVal yosanUnitKbn As String) As List(Of TYosanBuhinEditVo)

        ''' <summary>
        ''' 予算部品編集員数情報を取得
        ''' </summary>
        ''' <param name="yosanEventCode">予算書イベントコード</param>
        ''' <param name="yosanUnitKbn">ユニット区分</param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Function FindYosanBuhinEditInsu(ByVal yosanEventCode As String, ByVal yosanUnitKbn As String) As List(Of TYosanBuhinEditInsuVo)

        ''' <summary>
        ''' 予算部品編集員数情報を取得
        ''' </summary>
        ''' <param name="yosanEventCode">予算書イベントコード</param>
        ''' <param name="yosanUnitKbn">ユニット区分</param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Function FindYosanBuhinEditPattern(ByVal yosanEventCode As String, ByVal yosanUnitKbn As String) As List(Of TYosanBuhinEditPatternVo)

    End Interface

End Namespace