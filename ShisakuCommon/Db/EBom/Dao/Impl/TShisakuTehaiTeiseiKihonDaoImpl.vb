Imports ShisakuCommon.Db.EBom.Vo
Namespace Db.EBom.Dao.Impl
    ''' <summary>の簡単なCRUDを集めたDAO</summary>
    ''' <remarks>実装クラス</remarks>
    Public Class TShisakuTehaiTeiseiKihonDaoImpl : Inherits EBomDaoEachTable(Of TShisakuTehaiTeiseiKihonVo)
        Implements TShisakuTehaiTeiseiKihonDao
        ''' <summary>PrimaryKey を設定する</summary>
        ''' <param name="table">テーブルに対応するVOのインスタンス</param>
        ''' <remarks></remarks>
        Protected Overrides Sub SettingPkField(ByVal table As PkTable(Of Vo.TShisakuTehaiTeiseiKihonVo))
            Dim vo As New TShisakuTehaiTeiseiKihonVo
            table.IsA(vo).PkField(vo.ShisakuEventCode) _
            .PkField(vo.ShisakuListCode) _
            .PkField(vo.ShisakuListCodeKaiteiNo) _
            .PkField(vo.ShisakuBukaCode) _
            .PkField(vo.BuhinNoHyoujiJun) _
            .PkField(vo.Flag)
        End Sub

        ''' <summary>テーブル値の検索結果を返す</summary>
        ''' <param name="ShisakuEventCode">試作イベントコード</param>
        ''' <param name="ShisakuListCode">試作リストコード</param>
        ''' <param name="ShisakuListCodeKaiteiNo">試作リストコード改訂№</param>
        ''' <param name="ShisakuBukaCode">試作部課コード</param>
        ''' <param name="BuhinNoHyoujiJun">部品番号表示順</param>
        ''' <param name="Flag">フラグ</param>
        ''' <returns>該当レコード</returns>
        ''' <remarks></remarks>
        Public Function FindByPk(ByVal ShisakuEventCode As String, _
                          ByVal ShisakuListCode As String, _
                          ByVal ShisakuListCodeKaiteiNo As String, _
                          ByVal ShisakuBukaCode As String, _
                          ByVal BuhinNoHyoujiJun As Nullable(Of Int32), _
                          ByVal Flag As String) As TShisakuTehaiTeiseiKihonVo Implements TShisakuTehaiTeiseiKihonDao.FindByPk

            Return FindByPkMain(ShisakuEventCode, _
                        ShisakuListCode, _
                        ShisakuListCodeKaiteiNo, _
                        ShisakuBukaCode, _
                        BuhinNoHyoujiJun, _
                        Flag)
        End Function

        ''' <summary>該当レコードを削除する</summary>
        ''' <param name="ShisakuEventCode">試作イベントコード</param>
        ''' <param name="ShisakuListCode">試作リストコード</param>
        ''' <param name="ShisakuListCodeKaiteiNo">試作リストコード改訂№</param>
        ''' <param name="ShisakuBukaCode">試作部課コード</param>
        ''' <param name="BuhinNoHyoujiJun">部品番号表示順</param>
        ''' <param name="Flag">フラグ</param>
        ''' <returns>削除件数</returns>
        ''' <remarks></remarks>
        Function DeleteByPk(ByVal ShisakuEventCode As String, _
                          ByVal ShisakuListCode As String, _
                          ByVal ShisakuListCodeKaiteiNo As String, _
                          ByVal ShisakuBukaCode As String, _
                          ByVal BuhinNoHyoujiJun As Nullable(Of Int32), _
                          ByVal Flag As String) As Integer Implements TShisakuTehaiTeiseiKihonDao.DeleteByPk
            Return DeleteByPkMain(ShisakuEventCode, _
                        ShisakuListCode, _
                        ShisakuListCodeKaiteiNo, _
                        ShisakuBukaCode, _
                        BuhinNoHyoujiJun, _
                        Flag)
        End Function

    End Class
End Namespace