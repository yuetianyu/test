Imports ShisakuCommon.Db
Imports ShisakuCommon.Util.LabelValue
Imports EventSakusei.EventEdit.Dao
Imports ShisakuCommon.Db.EBom.Vo

Namespace ShisakuBuhinKaiteiBlock.Dao
    Public Interface IShisakuBuhinKaiteiBlockDao : Inherits DaoEachFeature

        Function GetBlockSpreadList(ByVal eventCode As String, _
                                         ByVal bukaCode As String, _
                                         ByVal blockCode1 As String, ByVal blockCode2 As String, _
                                         ByVal saisyuKoushinbi1 As Integer, ByVal saisyuKoushinbi2 As Integer _
                                        ) As List(Of ShisakuBuhinBlockVo)

    End Interface

End Namespace

