Imports ShisakuCommon.Db.EBom.Vo

Namespace Db.EBom.Dao
    ''' <summary>
    ''' ìÝvubNõdlîñe[uÌÈPÈCRUDðWß½DAO
    ''' </summary>
    ''' <remarks></remarks>
    Public Interface TShisakuSekkeiBlockSoubiShiyouDao : Inherits DaoEachTable(Of TShisakuSekkeiBlockSoubiShiyouVo)
        ''' <summary>
        ''' e[ulÌõÊðÔ·
        ''' </summary>
        ''' <param name="shisakuEventCode">ìCxgR[h</param>
        ''' <param name="shisakuBukaCode">ìÛR[h</param>
        ''' <param name="shisakuBlockNo">ìubN</param>
        ''' <param name="shisakuBlockNoKaiteiNo">ìubNüù</param>
        ''' <param name="shisakuSoubiHyoujiJun">ìõ\¦</param>
        ''' <returns>YR[h</returns>
        ''' <remarks></remarks>
        Function FindByPk(ByVal shisakuEventCode As String, _
                          ByVal shisakuBukaCode As String, _
                          ByVal shisakuBlockNo As String, _
                          ByVal shisakuBlockNoKaiteiNo As String, _
                          ByVal shisakuSoubiHyoujiJun As Nullable(Of Int32)) As TShisakuSekkeiBlockSoubiShiyouVo

        ''' <summary>
        ''' YR[hðí·é
        ''' </summary>
        ''' <param name="shisakuEventCode">ìCxgR[h</param>
        ''' <param name="shisakuBukaCode">ìÛR[h</param>
        ''' <param name="shisakuBlockNo">ìubN</param>
        ''' <param name="shisakuBlockNoKaiteiNo">ìubNüù</param>
        ''' <param name="shisakuSoubiHyoujiJun">ìõ\¦</param>
        ''' <returns>í</returns>
        ''' <remarks></remarks>
        Function DeleteByPk(ByVal shisakuEventCode As String, _
                            ByVal shisakuBukaCode As String, _
                            ByVal shisakuBlockNo As String, _
                            ByVal shisakuBlockNoKaiteiNo As String, _
                            ByVal shisakuSoubiHyoujiJun As Nullable(Of Int32)) As Integer
    End Interface
End Namespace