Imports ShisakuCommon.Db.EBom.Vo.Helper
Imports ShisakuCommon.Db.Impl
Imports ShisakuCommon.Db
Imports ShisakuCommon
Imports ShisakuCommon.Db.EBom
Imports ShisakuCommon.Util.LabelValue
Imports ShisakuCommon.Db.EBom.Vo
Imports ShisakuCommon.Db.EBom.Dao
Imports ShisakuCommon.Db.EBom.Dao.Impl
Imports EventSakusei.EventEdit.Vo

Namespace ExportShisakuEventInfoExcel.Dao

    Public Interface ExportShisakuEventInfoExcelDao : Inherits DaoEachFeature

        ''完成車情報を取得'
        'Function GetKnasei(ByVal eventCode As String) _
        '            As List(Of TSeisakuIchiranKanseiVo)

        'ベース車情報を取得'
        Function GetBaseTenkai(ByVal eventCode As String) As List(Of TShisakuEventBaseVo)

        'イベント情報を取得'
        Function GetEvent(ByVal eventCode As String) As List(Of TShisakuEventVo)

        'ベース車情報を取得'
        Function GetBase(ByVal eventCode As String) As List(Of TShisakuEventBaseSeisakuIchiranVo)

        'ベース車情報（改訂）を取得'
        Function GetBaseKaitei(ByVal eventCode As String) As List(Of TShisakuEventBaseKaiteiVo)

        '完成車情報を取得'
        Function GetKansei(ByVal eventCode As String) As List(Of TShisakuEventKanseiVo)

        '完成車情報（改訂）を取得'
        Function GetKanseiKaitei(ByVal eventCode As String) As List(Of TShisakuEventKanseiKaiteiVo)

        '装備仕様情報を取得'
        Function GetSoubi(ByVal ShisakuEventCode As String, ByVal ShisakuSoubiKbn As String) As List(Of EventSoubiVo)

        '装備仕様情報（改訂）を取得'
        Function GetSoubiKaitei(ByVal ShisakuEventCode As String, ByVal ShisakuSoubiKbn As String) As List(Of EventSoubiVo)

        '装備仕様の列情報を取得'
        Function GetSoubiTitle(ByVal ShisakuEventCode As String, ByVal ShisakuSoubiKbn As String) As List(Of EventSoubiVo)

        '装備仕様の列情報（改訂）を取得'
        Function GetSoubiTitleKaitei(ByVal ShisakuEventCode As String, ByVal ShisakuSoubiKbn As String) As List(Of EventSoubiVo)

    End Interface

End Namespace
