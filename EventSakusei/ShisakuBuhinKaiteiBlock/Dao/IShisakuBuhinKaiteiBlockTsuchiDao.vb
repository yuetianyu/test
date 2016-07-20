Imports ShisakuCommon.Db
Imports ShisakuCommon.Util.LabelValue
Imports EventSakusei.EventEdit.Dao
Imports ShisakuCommon.Db.EBom.Vo

Namespace ShisakuBuhinKaiteiBlock.Dao
    Public Interface IShisakuBuhinKaiteiBlockTsuchiDao : Inherits DaoEachFeature

        Function GetBlockTsuchiList(ByVal MailTsuchiHi1 As Integer, ByVal MailTsuchiHi As Integer _
                                        ) As List(Of TShisakuSekkeiBlockTsuchiVo)

        Sub UpdateByTShisakuSekkeiBlockTsuchi(ByVal MailTsuchiHi1 As Integer, ByVal MailTsuchiHi2 As Integer)

    End Interface

End Namespace

