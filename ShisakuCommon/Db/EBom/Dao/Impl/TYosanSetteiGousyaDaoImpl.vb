Imports ShisakuCommon.Db.EBom.Vo
Namespace Db.EBom.Dao.Impl

    ''' <summary>
    ''' 予算書イベント別製作台数情報テーブルの簡単なCRUDを集めたDAO
    ''' </summary>
    ''' <remarks>実装クラス</remarks>
    Public Class TYosanSetteiGousyaDaoImpl : Inherits EBomDaoEachTable(Of TYosanSetteiGousyaVo)
        Implements TYosanSetteiGousyaDao

        ''' <summary>
        ''' PrimaryKey を設定する
        ''' </summary>
        ''' <param name="table">テーブルに対応するVOのインスタンス</param>
        ''' <remarks></remarks>
        Protected Overrides Sub SettingPkField(ByVal table As PkTable(Of TYosanSetteiGousyaVo))
            Dim vo As New TYosanSetteiGousyaVo
            table.IsA(vo).PkField(vo.ShisakuEventCode) _
                         .PkField(vo.YosanListCode) _
                         .PkField(vo.YosanBukaCode) _
                         .PkField(vo.YosanBlockNo) _
                         .PkField(vo.BuhinNoHyoujiJun) _
                         .PkField(vo.YosanGousyaHyoujiJun)

        End Sub
        ''' <summary>
        ''' テーブル値の検索結果を返す
        ''' </summary>
        ''' <param name="ShisakuEventCode">試作イベントコード</param>
        ''' <param name="YosanListCode">予算リストコード</param>
        ''' <param name="YosanBukaCode">予算部課コード</param>
        ''' <param name="YosanBlockNo">予算ブロック№</param>
        ''' <param name="BuhinNoHyoujiJun">部品番号表示順</param>
        ''' <param name="YosanGousyaHyoujiJun">試作号車表示順</param>
        ''' <returns>該当レコード</returns>
        ''' <remarks></remarks>
        Public Function FindByPk(ByVal ShisakuEventCode As String, _
                                 ByVal YosanListCode As String, _
                                 ByVal YosanBukaCode As String, _
                                 ByVal YosanBlockNo As String, _
                                 ByVal BuhinNoHyoujiJun As Nullable(Of Int32), _
                                 ByVal YosanGousyaHyoujiJun As Nullable(Of Int32)) As Vo.TYosanSetteiGousyaVo Implements TYosanSetteiGousyaDao.FindByPk
            Return FindByPkMain(ShisakuEventCode, YosanListCode, YosanBukaCode, YosanBlockNo, BuhinNoHyoujiJun, YosanGousyaHyoujiJun)
        End Function

        ''' <summary>
        ''' 該当レコードを削除する
        ''' </summary>
        ''' <param name="ShisakuEventCode">試作イベントコード</param>
        ''' <param name="YosanListCode">予算リストコード</param>
        ''' <param name="YosanBukaCode">予算部課コード</param>
        ''' <param name="YosanBlockNo">予算ブロック№</param>
        ''' <param name="BuhinNoHyoujiJun">部品番号表示順</param>
        ''' <param name="YosanGousyaHyoujiJun">試作号車表示順</param>
        ''' <returns>削除件数</returns>
        ''' <remarks></remarks>
        Public Function DeleteByPk(ByVal ShisakuEventCode As String, _
                                  ByVal YosanListCode As String, _
                                  ByVal YosanBukaCode As String, _
                                  ByVal YosanBlockNo As String, _
                                  ByVal BuhinNoHyoujiJun As Nullable(Of Int32), _
                                  ByVal YosanGousyaHyoujiJun As Nullable(Of Int32)) As Integer Implements TYosanSetteiGousyaDao.DeleteByPk
            Return DeleteByPkMain(ShisakuEventCode, YosanListCode, YosanBukaCode, YosanBlockNo, BuhinNoHyoujiJun, YosanGousyaHyoujiJun)
        End Function

    End Class
End Namespace



