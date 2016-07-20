Imports ShisakuCommon.Db.EBom.Vo

Namespace Db.EBom.Dao
    ''' <summary>
    ''' 予算書イベント別製作台数情報テーブルの簡単なCRUDを集めたDAO
    ''' </summary>
    ''' <remarks></remarks>
    Public Interface TYosanSeisakuDaisuDao : Inherits DaoEachTable(Of TYosanSeisakuDaisuVo)
        ''' <summary>
        ''' テーブル値の検索結果を返す
        ''' </summary>
        ''' <param name="yosanEventCode">予算イベントコード</param>
        ''' <param name="ShisakuSyubetu">試作種別</param>
        ''' <param name="KoujiShireiNoHyojijunNo">工事指令№表示順</param>
        ''' <returns>該当レコード</returns>
        ''' <remarks></remarks>
        Function FindByPk(ByVal yosanEventCode As String, ByVal ShisakuSyubetu As String, ByVal KoujiShireiNoHyojijunNo As Integer) As TYosanSeisakuDaisuVo

        ''' <summary>
        ''' 該当レコードを削除する
        ''' </summary>
        ''' <param name="yosanEventCode">予算イベントコード</param>
        ''' <param name="ShisakuSyubetu">試作種別</param>
        ''' <param name="KoujiShireiNoHyojijunNo">工事指令№表示順</param>
        ''' <returns>削除件数</returns>
        ''' <remarks></remarks>
        Function DeleteByPk(ByVal yosanEventCode As String, ByVal ShisakuSyubetu As String, ByVal KoujiShireiNoHyojijunNo As Integer) As Integer
    End Interface
End Namespace