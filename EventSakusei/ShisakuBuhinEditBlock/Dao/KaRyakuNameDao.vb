Imports ShisakuCommon.Db.EBom.Vo.Helper
Imports ShisakuCommon.Db.Impl
Imports ShisakuCommon.Db
Imports ShisakuCommon
Imports ShisakuCommon.Db.EBom
Imports ShisakuCommon.Util.LabelValue
Imports ShisakuCommon.Db.EBom.Vo
Imports EventSakusei.EventEdit.Dao
Imports ShisakuCommon.Db.EBom.Dao
Imports ShisakuCommon.Db.EBom.Dao.Impl

Namespace ShisakuBuhinEditBlock.Dao

    Public Interface KaRyakuNameDao : Inherits DaoEachFeature

        '担当承認課と課長承認課に部課コードではなく、課略名を表示させるための処理を追加'
        Function GetKa_Ryaku_Name(ByVal syoninka As String) As Rhac1560Vo


    End Interface

End Namespace
