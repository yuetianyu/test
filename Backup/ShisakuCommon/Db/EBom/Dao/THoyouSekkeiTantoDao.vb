Imports ShisakuCommon.Db.EBom.Vo

Namespace Db.EBom.Dao
    ''' <summary>
    ''' âpÝvSîñe[uÌÈPÈCRUDðWß½DAO
    ''' </summary>
    ''' <remarks></remarks>
    Public Interface THoyouSekkeiTantoDao : Inherits DaoEachTable(Of THoyouSekkeiTantoVo)
        ''' <summary>
        ''' e[ulÌõÊðÔ·
        ''' </summary>
        ''' <param name="hoyouEventCode">âpCxgR[h</param>
        ''' <param name="hoyouBukaCode">âpÛR[h</param>
        ''' <param name="hoyouTanto">âpS</param>
        ''' <param name="hoyouTantoKaiteiNo">âpSüù</param>
        ''' <returns>YR[h</returns>
        ''' <remarks></remarks>
        Function FindByPk(ByVal hoyouEventCode As String, _
                          ByVal hoyouBukaCode As String, _
                          ByVal hoyouTanto As String, _
                          ByVal hoyouTantoKaiteiNo As String) As THoyouSekkeiTantoVo

        ''' <summary>
        ''' YR[hðí·é
        ''' </summary>
        ''' <param name="hoyouEventCode">âpCxgR[h</param>
        ''' <param name="hoyouBukaCode">âpÛR[h</param>
        ''' <param name="hoyouTanto">âpS</param>
        ''' <param name="hoyouTantoKaiteiNo">âpSüù</param>
        ''' <returns>í</returns>
        ''' <remarks></remarks>
        Function DeleteByPk(ByVal hoyouEventCode As String, _
                            ByVal hoyouBukaCode As String, _
                            ByVal hoyouTanto As String, _
                            ByVal hoyouTantoKaiteiNo As String) As Integer
    End Interface
End Namespace