Imports ShisakuCommon.Db
Imports ShisakuCommon.Util.LabelValue
Imports EventSakusei.EventEdit.Dao
Imports ShisakuCommon.Db.EBom.Vo

Namespace ShisakuBuhinKaiteiBlock.Dao

    Public Interface IMUserMailAddressDao : Inherits DaoEachFeature

        Function GetSendList() As List(Of MUserMailAddressVo)

    End Interface

End Namespace

