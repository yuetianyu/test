Imports ShisakuCommon.Db.EBom.Vo

Namespace Db.EBom.Dao.Impl

    ''' <summary>
    ''' 試作手配帳情報（号車グループ情報）の簡単なCRUDを集めたDAO
    ''' </summary>
    ''' <remarks>実装クラス</remarks>
    Public Class TShisakuTehaiGousyaGroupDaoImpl : Inherits EBomDaoEachTable(Of TShisakuTehaiGousyaGroupVo)
        Implements TShisakuTehaiGousyaGroupDao

        ''' <summary>
        ''' PrimaryKey を設定する
        ''' </summary>
        ''' <param name="table">テーブルに対応するVOのインスタンス</param>
        ''' <remarks></remarks>
        Protected Overrides Sub SettingPkField(ByVal table As PkTable(Of TShisakuTehaiGousyaGroupVo))
            Dim vo As New TShisakuTehaiGousyaGroupVo
            table.IsA(vo) _
            .PkField(vo.ShisakuEventCode) _
            .PkField(vo.ShisakuListCode) _
            .PkField(vo.ShisakuListCodeKaiteiNo) _
            .PkField(vo.ShisakuGousya)
        End Sub

        ''' <summary>
        ''' テーブル値の検索結果を返す
        ''' </summary>
        ''' <param name="ShisakuEventCode">試作イベントコード</param>
        ''' <param name="ShisakuListCode">試作リストコード</param>
        ''' <param name="ShisakuListCodeKaiteiNo">試作リストコード改訂№</param>
        ''' <param name="ShisakuGousya">試作号車</param>
        ''' <returns>該当レコード</returns>
        ''' <remarks></remarks>
        Public Function FindByPk(ByVal ShisakuEventCode As String, _
                                 ByVal ShisakuListCode As String, _
                                 ByVal ShisakuListCodeKaiteiNo As String, _
                                 ByVal ShisakuGousya As String) As Vo.TShisakuTehaiGousyaGroupVo Implements TShisakuTehaiGousyaGroupDao.FindByPk
            Return FindByPkMain(ShisakuEventCode, ShisakuListCode, ShisakuListCodeKaiteiNo, ShisakuGousya)
        End Function

        ''' <summary>
        ''' 該当レコードを削除する
        ''' </summary>
        ''' <param name="ShisakuEventCode">試作イベントコード</param>
        ''' <param name="ShisakuListCode">試作リストコード</param>
        ''' <param name="ShisakuListCodeKaiteiNo">試作リストコード改訂№</param>
        ''' <param name="ShisakuGousya">試作号車</param>
        ''' <returns>削除件数ド</returns>
        ''' <remarks></remarks>
        Public Function DeleteByPk(ByVal ShisakuEventCode As String, _
                             ByVal ShisakuListCode As String, _
                             ByVal ShisakuListCodeKaiteiNo As String, _
                             ByVal ShisakuGousya As String) As Integer Implements TShisakuTehaiGousyaGroupDao.DeleteByPk
            Return DeleteByPkMain(ShisakuEventCode, ShisakuListCode, ShisakuListCodeKaiteiNo, ShisakuGousya)
        End Function
      

    End Class
End Namespace


