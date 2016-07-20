Imports ShisakuCommon.Db.EBom.Vo
Namespace Db.EBom.Dao.Impl
    ''' <summary>の簡単なCRUDを集めたDAO</summary>
    ''' <remarks>実装クラス</remarks>
    Public Class TShisakuBuhinEditTmp3dDaoImpl : Inherits EBomDaoEachTable(Of TShisakuBuhinEditTmp3dVo)
        Implements TShisakuBuhinEditTmp3dDao
        ''' <summary>PrimaryKey を設定する</summary>
        ''' <param name="table">テーブルに対応するVOのインスタンス</param>
        ''' <remarks></remarks>
        Protected Overrides Sub SettingPkField(ByVal table As PkTable(Of Vo.TShisakuBuhinEditTmp3dVo))
            Dim vo As New TShisakuBuhinEditTmp3dVo
            table.IsA(vo).PkField(vo.ShisakuEventCode) _
            .PkField(vo.ShisakuBukaCode) _
            .PkField(vo.ShisakuBlockNo) _
            .PkField(vo.ShisakuBlockNoKaiteiNo) _
            .PkField(vo.BuhinNoHyoujiJun)
        End Sub

        ''' <summary>テーブル値の検索結果を返す</summary>
        ''' <param name="GUID">GUID</param>
        ''' <param name="ShisakuEventCode">試作イベントコード</param>
        ''' <param name="ShisakuBukaCode">試作部課コード</param>
        ''' <param name="ShisakuBlockNo">試作ブロック№</param>
        ''' <param name="ShisakuBlockNoKaiteiNo">試作ブロック№改訂№</param>
        ''' <param name="BuhinNoHyoujiJun">部品番号表示順</param>
        ''' <returns>該当レコード</returns>
        ''' <remarks></remarks>
        Public Function FindByPk(ByVal Guid As String, _
                          ByVal ShisakuEventCode As String, _
                          ByVal ShisakuBukaCode As String, _
                          ByVal ShisakuBlockNo As String, _
                          ByVal ShisakuBlockNoKaiteiNo As String, _
                          ByVal BuhinNoHyoujiJun As Nullable(Of Int32), _
                          ByVal GyouId As String) As TShisakuBuhinEditTmp3dVo Implements TShisakuBuhinEditTmp3dDao.FindByPk

            Return FindByPkMain(Guid, _
                        ShisakuEventCode, _
                        ShisakuBukaCode, _
                        ShisakuBlockNo, _
                        ShisakuBlockNoKaiteiNo, _
                        BuhinNoHyoujiJun, _
                        GyouId)
        End Function

        ''' <summary>該当レコードを削除する</summary>
        ''' <param name="GUID">GUID</param>
        ''' <param name="ShisakuEventCode">試作イベントコード</param>
        ''' <param name="ShisakuBukaCode">試作部課コード</param>
        ''' <param name="ShisakuBlockNo">試作ブロック№</param>
        ''' <param name="ShisakuBlockNoKaiteiNo">試作ブロック№改訂№</param>
        ''' <param name="BuhinNoHyoujiJun">部品番号表示順</param>
        ''' <returns>削除件数</returns>
        ''' <remarks></remarks>
        Function DeleteByPk(ByVal Guid As String, _
                            ByVal ShisakuEventCode As String, _
                          ByVal ShisakuBukaCode As String, _
                          ByVal ShisakuBlockNo As String, _
                          ByVal ShisakuBlockNoKaiteiNo As String, _
                          ByVal BuhinNoHyoujiJun As Nullable(Of Int32), _
                          ByVal GyouId As String) As Integer Implements TShisakuBuhinEditTmp3dDao.DeleteByPk
            Return DeleteByPkMain(Guid, ShisakuEventCode, _
                        ShisakuBukaCode, _
                        ShisakuBlockNo, _
                        ShisakuBlockNoKaiteiNo, _
                        BuhinNoHyoujiJun, _
                        GyouId)
        End Function


    End Class
End Namespace