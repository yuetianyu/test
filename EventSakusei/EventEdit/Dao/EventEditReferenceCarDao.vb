Imports ShisakuCommon.Db.EBom.Vo

Namespace EventEdit.Dao
    Public interface EventEditReferenceCarDao
        Function FindKanseiBy(ByVal shisakuEventCode As String, ByVal shisakuGousya As String) As TShisakuEventKanseiVo
    end interface
End NameSpace