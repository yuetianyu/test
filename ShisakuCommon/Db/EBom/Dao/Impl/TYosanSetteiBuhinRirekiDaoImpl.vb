Imports ShisakuCommon.Db.EBom.Vo
Namespace Db.EBom.Dao.Impl
    ''' <summary>予算設定部品表の簡単なCRUDを集めたDAO</summary>
    ''' <remarks>実装クラス</remarks>

    Public Class TYosanSetteiBuhinRirekiDaoImpl : Inherits EBomDaoEachTable(Of TYosanSetteiBuhinRirekiVo)
        Implements TYosanSetteiBuhinRirekiDao

        ''' <summary>
        ''' PrimaryKey を設定する
        ''' </summary>
        ''' <param name="table">テーブルに対応するVOのインスタンス</param>
        ''' <remarks></remarks>
        Protected Overrides Sub SettingPkField(ByVal table As PkTable(Of Vo.TYosanSetteiBuhinRirekiVo))
            Dim vo As New TYosanSetteiBuhinRirekiVo
            table.IsA(vo).PkField(vo.ShisakuEventCode) _
            .PkField(vo.YosanListCode) _
            .PkField(vo.YosanBukaCode) _
            .PkField(vo.YosanBlockNo) _
            .PkField(vo.YosanBuhinNo) _
            .PkField(vo.BuhinNoHyoujiJun) _
            .PkField(vo.ColumnId) _
            .PkField(vo.ColumnName) _
            .PkField(vo.UpdateBi) _
            .PkField(vo.UpdateJikan)
        End Sub

        ''' <summary>
        ''' テーブル値の検索結果を返す
        ''' </summary>
        ''' <param name="ShisakuEventCode">試作イベントコード</param>
        ''' <param name="YosanListCode">予算リストコード</param>
        ''' <param name="YosanBukaCode">予算部課コード</param>
            ''' <param name="YosanBlockNo">予算ブロック№</param>
        ''' <param name="BuhinNoHyoujiJun">部品番号表示順</param>
        ''' <param name="ColumnId">列ID</param>
        ''' <param name="ColumnName">列名</param>
        ''' <param name="UpdateBi">変更日</param>
        ''' <param name="UpdateJikan">変更時間</param>
        ''' <returns>該当レコード</returns>
        ''' <remarks></remarks>
        Public Function DeleteByPk(ByVal ShisakuEventCode As String, _
                                   ByVal YosanListCode As String, _
                                   ByVal YosanBukaCode As String, _
                                   ByVal YosanBlockNo As String, _
                                   ByVal YosanBuhinNo As String, _
                                   ByVal BuhinNoHyoujiJun As Nullable(Of Int32), _
                                   ByVal ColumnId As String, _
                                   ByVal ColumnName As String, _
                                   ByVal UpdateBi As String, _
                                   ByVal UpdateJikan As String) As Integer Implements TYosanSetteiBuhinRirekiDao.DeleteByPk
            Return DeleteByPkMain(ShisakuEventCode, _
                                  YosanListCode, _
                                  YosanBukaCode, _
                                  YosanBlockNo, _
                                  YosanBuhinNo, _
                                  BuhinNoHyoujiJun, _
                                  ColumnId, _
                                  ColumnName, _
                                  UpdateBi, _
                                  UpdateJikan)
        End Function
        ''' <summary>
        ''' 該当レコードを削除する
        ''' </summary>
        ''' <param name="ShisakuEventCode">試作イベントコード</param>
        ''' <param name="YosanListCode">予算リストコード</param>
        ''' <param name="YosanBukaCode">予算部課コード</param>
        ''' <param name="YosanBlockNo">予算ブロック№</param>
        ''' <param name="YosanBuhinNo"></param>
        ''' <param name="BuhinNoHyoujiJun">部品番号表示順</param>       
        ''' <param name="ColumnId">列ID</param>
        ''' <param name="ColumnName">列名</param>
        ''' <param name="UpdateBi">変更日</param>
        ''' <param name="UpdateJikan">変更時間</param>
        ''' <returns>削除件数</returns>
        ''' <remarks></remarks>
        Public Function FindByPk(ByVal ShisakuEventCode As String, _
                                 ByVal YosanListCode As String, _
                                 ByVal YosanBukaCode As String, _
                                 ByVal YosanBlockNo As String, _
                                 ByVal YosanBuhinNo As String, _
                                 ByVal BuhinNoHyoujiJun As Nullable(Of Int32), _
                                 ByVal ColumnId As String, _
                                 ByVal ColumnName As String, _
                                 ByVal UpdateBi As String, _
                                 ByVal UpdateJikan As String) As Vo.TYosanSetteiBuhinRirekiVo Implements TYosanSetteiBuhinRirekiDao.FindByPk
            Return FindByPkMain(ShisakuEventCode, _
                                YosanListCode, _
                                YosanBukaCode, _
                                YosanBlockNo, _
                                BuhinNoHyoujiJun, _
                                ColumnId, _
                                ColumnName, _
                                UpdateBi, _
                                UpdateJikan)
        End Function

    End Class
End Namespace


