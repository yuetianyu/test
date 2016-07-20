Imports ShisakuCommon.Db.EBom.Vo

Namespace Db.EBom.Dao.Impl

    ''' <summary>
    ''' 試作リストコード情報テーブルの簡単なCRUDを集めたDAO
    ''' </summary>
    ''' <remarks>実装クラス</remarks>
    Public Class TShisakuListcodeDaoImpl : Inherits EBomDaoEachTable(Of TShisakuListcodeVo)
        Implements TShisakuListcodeDao

        ''' <summary>
        ''' PrimaryKey を設定する
        ''' </summary>
        ''' <param name="table">テーブルに対応するVOのインスタンス</param>
        ''' <remarks></remarks>
        Protected Overrides Sub SettingPkField(ByVal table As PkTable(Of TShisakuListcodeVo))
            Dim vo As New TShisakuListcodeVo
            table.IsA(vo).PkField(vo.ShisakuEventCode) _
                         .PkField(vo.ShisakuListHyojijunNo) _
                         .PkField(vo.ShisakuListCode) _
                         .PkField(vo.ShisakuListCodeKaiteiNo) _
                         .PkField(vo.ShisakuGroupNo)

        End Sub

        ''' <summary>
        ''' テーブル値の検索結果を返す
        ''' </summary>
        ''' <param name="shisakuEventCode">試作イベントコード</param>
        ''' <param name="shisakuListHyojijunNo">試作リスト表示順</param>
        ''' <param name="shisakuListCode">試作リストコード</param>
        ''' <param name="shisakuListCodeKaiteiNo">試作リストコード改訂№</param>
        ''' <param name="shisakuGroupNo">試作グループ№</param>
        ''' <returns>該当レコード</returns>
        ''' <remarks></remarks>
        Public Function FindByPk(ByVal shisakuEventCode As String, _
                                 ByVal shisakuListHyojijunNo As Nullable(Of Int32), _
                                 ByVal shisakuListCode As String, _
                                 ByVal shisakuListCodeKaiteiNo As String, _
                                 ByVal shisakuGroupNo As String) As TShisakuListcodeVo Implements TShisakuListcodeDao.FindByPk
            Return FindByPkMain(shisakuEventCode, _
                                shisakuListHyojijunNo, _
                                shisakuListCode, _
                                shisakuListCodeKaiteiNo, _
                                shisakuGroupNo)
        End Function

        ''' <summary>
        ''' 該当レコードを削除する
        ''' </summary>
        ''' <param name="shisakuEventCode">試作イベントコード</param>
        ''' <param name="shisakuListHyojijunNo">試作リスト表示順</param>
        ''' <param name="shisakuListCode">試作リストコード</param>
        ''' <param name="shisakuListCodeKaiteiNo">試作リストコード改訂№</param>
        ''' <param name="shisakuGroupNo">試作グループ№</param>
        ''' <returns>削除件数</returns>
        ''' <remarks></remarks>
        Public Function DeleteByPk(ByVal shisakuEventCode As String, _
                                   ByVal shisakuListHyojijunNo As Nullable(Of Int32), _
                                   ByVal shisakuListCode As String, _
                                   ByVal shisakuListCodeKaiteiNo As String, _
                                   ByVal shisakuGroupNo As String) As Integer Implements TShisakuListcodeDao.DeleteByPk
            Return DeleteByPkMain(shisakuEventCode, _
                                  shisakuListHyojijunNo, _
                                  shisakuListCode, _
                                  shisakuListCodeKaiteiNo, _
                                  shisakuGroupNo)
        End Function

    End Class
End Namespace
