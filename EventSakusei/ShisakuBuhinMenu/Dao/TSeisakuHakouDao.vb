Imports ShisakuCommon.Db.EBom.Vo.Helper
Imports ShisakuCommon.Db.Impl
Imports ShisakuCommon.Db
Imports ShisakuCommon
Imports ShisakuCommon.Db.EBom
Imports ShisakuCommon.Util.LabelValue
Imports ShisakuCommon.Db.EBom.Vo
Imports ShisakuCommon.Db.EBom.Dao
Imports ShisakuCommon.Db.EBom.Dao.Impl

Namespace ShisakuBuhinMenu.Dao

    Public Interface TSeisakuHakouDao : Inherits DaoEachFeature

        ''' <summary>
        ''' ヘッダー情報を取得（最新の改訂№を取得する）
        ''' </summary>
        ''' <param name="hakouNo">発行№</param>
        ''' <param name="status">ステータス</param>
        ''' <remarks></remarks>
        Function GetTSeisakuHakouHdStatus(ByVal hakouNo As String, ByVal status As String) As TSeisakuHakouHdVo

        ''' <summary>
        ''' ヘッダー情報を取得
        ''' </summary>
        ''' <param name="hakouNo">発行№</param>
        ''' <param name="kaiteiNo">改訂№</param>
        ''' <remarks></remarks>
        Function GetTSeisakuHakouHdKaitei(ByVal hakouNo As String, ByVal kaiteiNo As String) As List(Of TSeisakuHakouHdVo)

    End Interface

End Namespace
