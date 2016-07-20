Imports ShisakuCommon.Db.EBom.Vo

Namespace Db.EBom.Dao
    ''' <summary>
    ''' 予算書単価情報テーブルの簡単なCRUDを集めたDAO
    ''' </summary>
    ''' <remarks></remarks>
    Public Interface TYosanTankaDao : Inherits DaoEachTable(Of TYosanTankaVo)
        ''' <summary>
        ''' テーブル値の検索結果を返す
        ''' </summary>
        ''' <param name="kokunaiKaigaiKbn">国内海外区分</param>
        ''' <param name="yosanBuhinNo">部品番号</param>
        ''' <returns>該当レコード</returns>
        ''' <remarks></remarks>
        Function FindByPk(ByVal kokunaiKaigaiKbn As String, ByVal yosanBuhinNo As String) As TYosanTankaVo

        ''' <summary>
        ''' 該当レコードを削除する
        ''' </summary>
        ''' <param name="kokunaiKaigaiKbn">国内海外区分</param>
        ''' <param name="yosanBuhinNo">部品番号</param>
        ''' <returns>削除件数</returns>
        ''' <remarks></remarks>
        Function DeleteByPk(ByVal kokunaiKaigaiKbn As String, ByVal yosanBuhinNo As String) As Integer
    End Interface
End Namespace