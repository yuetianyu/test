Imports ShisakuCommon.Db.EBom.Vo

Namespace Db.EBom.Dao
    ''' <summary>
    ''' âpÝvSõîñe[uÌÈPÈCRUDðWß½DAO
    ''' </summary>
    ''' <remarks></remarks>
    Public Interface THoyouSekkeiTantoSoubiDao : Inherits DaoEachTable(Of THoyouSekkeiTantoSoubiVo)
        ''' <summary>
        ''' e[ulÌõÊðÔ·
        ''' </summary>
        ''' <param name="hoyouEventCode">âpCxgR[h</param>
        ''' <param name="hoyouBukaCode">âpÛR[h</param>
        ''' <param name="hoyouTantoKey">âpSjdx</param>
        ''' <param name="hoyouTanto">âpS</param>
        ''' <param name="hoyouSoubiHyoujiJun">Ú</param>
        ''' <returns>YR[h</returns>
        ''' <remarks></remarks>
        Function FindByPk(ByVal hoyouEventCode As String, _
                          ByVal hoyouBukaCode As String, _
                          ByVal hoyouTantoKey As String, _
                          ByVal hoyouTanto As String, _
                          ByVal hoyouSoubiHyoujiJun As String) As THoyouSekkeiTantoSoubiVo

        ''' <summary>
        ''' YR[hðí·é
        ''' </summary>
        ''' <param name="hoyouEventCode">âpCxgR[h</param>
        ''' <param name="hoyouBukaCode">âpÛR[h</param>
        ''' <param name="hoyouTantoKey">âpSjdx</param>
        ''' <param name="hoyouTanto">âpS</param>
        ''' <param name="hoyouSoubiHyoujiJun">Ú</param>
        ''' <returns>í</returns>
        ''' <remarks></remarks>
        Function DeleteByPk(ByVal hoyouEventCode As String, _
                            ByVal hoyouBukaCode As String, _
                            ByVal hoyouTantoKey As String, _
                            ByVal hoyouTanto As String, _
                            ByVal hoyouSoubiHyoujiJun As String) As Integer
    End Interface
End Namespace