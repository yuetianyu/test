
Imports ShisakuCommon.Db.EBom.Vo

Namespace Db.EBom.Dao.Impl
    ''' <summary>
    ''' 試作システム権限マスターテーブルの簡単なCRUDを集めたDAO
    ''' </summary>
    ''' <remarks>実装クラス</remarks>
    Public Class MShisakuKengenDaoImpl : Inherits EBomDaoEachTable(Of MShisakuKengenVo)
        Implements MShisakuKengenDao

        ''' <summary>
        ''' PrimaryKey を設定する
        ''' </summary>
        ''' <param name="table">テーブルに対応するVOのインスタンス</param>
        ''' <remarks></remarks>
        Protected Overrides Sub SettingPkField(ByVal table As PkTable(Of Vo.MShisakuKengenVo))
            Dim vo As New MShisakuKengenVo
            table.IsA(vo) _
            .PkField(vo.ShisakuProgramId1) _
            .PkField(vo.ShisakuKinoId1) _
            .PkField(vo.ShisakuKinoId2)
        End Sub

        ''' <summary>
        ''' テーブル値の検索結果を返す
        ''' </summary>
        ''' <param name="shisakuProgramId1">試作プログラムID </param>
        ''' <param name="shisakuKinoId1">試作機能ID01</param>
        ''' <param name="shisakuKinoId2">試作機能ID02</param>
        ''' <returns>該当レコード</returns>
        ''' <remarks></remarks>
        Public Function FindByPk(ByVal shisakuProgramId1 As String, _
                                 ByVal shisakuKinoId1 As String, _
                                 ByVal shisakuKinoId2 As String) As Vo.MShisakuKengenVo Implements MShisakuKengenDao.FindByPk
            Return FindByPkMain(shisakuProgramId1, shisakuKinoId1, shisakuKinoId2)
        End Function

        ''' <summary>
        ''' 該当レコードを削除する
        ''' </summary>
        ''' <param name="shisakuProgramId1">試作プログラムID </param>
        ''' <param name="shisakuKinoId1">試作機能ID01</param>
        ''' <param name="shisakuKinoId2">試作機能ID02</param>
        ''' <returns>削除件数</returns>
        ''' <remarks></remarks>
        Public Function DeleteByPk(ByVal shisakuProgramId1 As String, _
                                 ByVal shisakuKinoId1 As String, _
                                 ByVal shisakuKinoId2 As String) As Integer Implements MShisakuKengenDao.DeleteByPk
            Return DeleteByPkMain(shisakuProgramId1, shisakuKinoId1, shisakuKinoId2)
        End Function

    End Class
End Namespace