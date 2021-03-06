Imports ShisakuCommon.Db.EBom.Vo

Namespace Db.EBom.Dao
    ''' <summary>
    ''' ìCxgÁÊõîñe[uÌÈPÈCRUDðWß½DAO
    ''' </summary>
    ''' <remarks></remarks>
    Public Interface TShisakuEventSoubiKaiteiDao : Inherits DaoEachTable(Of TShisakuEventSoubiKaiteiVo)
        ''' <summary>
        ''' e[ulÌõÊðÔ·
        ''' </summary>
        ''' <param name="shisakuEventCode">ìCxgR[h</param>
        ''' <param name="hyojijunNo">\¦</param>
        ''' <param name="shisakuTokubetuSoubiHyoujiNo">ìÁÊõ\¦</param>
        ''' <param name="shisakuTokubetuSoubiKbn">ìÁÊõæª</param>
        ''' <param name="shisakuRetuKoumokuCode">ñÚR[h</param>
        ''' <returns>YR[h</returns>
        ''' <remarks></remarks>
        Function FindByPk(ByVal shisakuEventCode As String, ByVal hyojijunNo As Nullable(Of Int32) _
                         , ByVal shisakuTokubetuSoubiHyoujiNo As Nullable(Of Int32), ByVal shisakuTokubetuSoubiKbn As String _
                         , ByVal shisakuRetuKoumokuCode As String) As TShisakuEventSoubikaiteiVo

        ''' <summary>
        ''' YR[hðí·é
        ''' </summary>
        ''' <param name="shisakuEventCode">ìCxgR[h</param>
        ''' <param name="hyojijunNo">\¦</param>
        ''' <param name="shisakuTokubetuSoubiHyoujiNo">ìÁÊõ\¦</param>
        ''' <param name="shisakuTokubetuSoubiKbn">ìÁÊõæª</param>
        ''' <param name="shisakuRetuKoumokuCode">ñÚR[h</param>
        ''' <returns>í</returns>
        ''' <remarks></remarks>
        Function DeleteByPk(ByVal shisakuEventCode As String, ByVal hyojijunNo As Nullable(Of Int32) _
                         , ByVal shisakuTokubetuSoubiHyoujiNo As Nullable(Of Int32), ByVal shisakuTokubetuSoubiKbn As String _
                         , ByVal shisakuRetuKoumokuCode As String) As Integer
    End Interface
End Namespace