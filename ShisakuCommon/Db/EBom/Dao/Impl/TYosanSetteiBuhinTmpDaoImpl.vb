Imports ShisakuCommon.Db.EBom.Vo
Namespace Db.EBom.Dao.Impl
    ''' <summary>予算設定部品表情報（TMP）の簡単なCRUDを集めたDAO</summary>
    ''' <remarks>実装クラス</remarks>
    Public Class TYosanSetteiBuhinTmpDaoImpl : Inherits EBomDaoEachTable(Of TYosanSetteiBuhinTmpVo)
        Implements TYosanSetteiBuhinTmpDao
        ''' <summary>PrimaryKey を設定する</summary>
        ''' <param name="table">テーブルに対応するVOのインスタンス</param>
        ''' <remarks></remarks>
        Protected Overrides Sub SettingPkField(ByVal table As PkTable(Of Vo.TYosanSetteiBuhinTmpVo))
            Dim vo As New TYosanSetteiBuhinTmpVo
            table.IsA(vo).PkField(vo.ShisakuEventCode) _
            .PkField(vo.ShisakuBukaCode) _
            .PkField(vo.ShisakuBlockNo) _
            .PkField(vo.ShisakuBlockNoKaiteiNo) _
            .PkField(vo.BuhinNoHyoujiJun) _
            .PkField(vo.GyouId)
        End Sub

        ''' <summary>テーブル値の検索結果を返す</summary>
        ''' <param name="ShisakuEventCode">試作イベントコード</param>
        ''' <param name="ShisakuBukaCode">試作部課コード</param>
        ''' <param name="ShisakuBlockNo">試作ブロック№</param>
        ''' <param name="ShisakuBlockNoKaiteiNo">試作ブロック№改訂№</param>
        ''' <param name="BuhinNoHyoujiJun">部品番号表示順</param>
        ''' <returns>該当レコード</returns>
        ''' <remarks></remarks>
        Public Function FindByPk(ByVal ShisakuEventCode As String, _
                          ByVal ShisakuBukaCode As String, _
                          ByVal ShisakuBlockNo As String, _
                          ByVal ShisakuBlockNoKaiteiNo As String, _
                          ByVal BuhinNoHyoujiJun As Nullable(Of Int32), _
                          ByVal GyouId As String) As TYosanSetteiBuhinTmpVo Implements TYosanSetteiBuhinTmpDao.FindByPk

            Return FindByPkMain(ShisakuEventCode, _
                        ShisakuBukaCode, _
                        ShisakuBlockNo, _
                        ShisakuBlockNoKaiteiNo, _
                        BuhinNoHyoujiJun, _
                        GyouId)
        End Function

        ''' <summary>該当レコードを削除する</summary>
        ''' <param name="ShisakuEventCode">試作イベントコード</param>
        ''' <param name="ShisakuBukaCode">試作部課コード</param>
        ''' <param name="ShisakuBlockNo">試作ブロック№</param>
        ''' <param name="ShisakuBlockNoKaiteiNo">試作ブロック№改訂№</param>
        ''' <param name="BuhinNoHyoujiJun">部品番号表示順</param>
        ''' <returns>削除件数</returns>
        ''' <remarks></remarks>
        Function DeleteByPk(ByVal ShisakuEventCode As String, _
                          ByVal ShisakuBukaCode As String, _
                          ByVal ShisakuBlockNo As String, _
                          ByVal ShisakuBlockNoKaiteiNo As String, _
                          ByVal BuhinNoHyoujiJun As Nullable(Of Int32), _
                          ByVal GyouId As String) As Integer Implements TYosanSetteiBuhinTmpDao.DeleteByPk
            Return DeleteByPkMain(ShisakuEventCode, _
                        ShisakuBukaCode, _
                        ShisakuBlockNo, _
                        ShisakuBlockNoKaiteiNo, _
                        BuhinNoHyoujiJun, _
                        GyouId)
        End Function

    End Class
End Namespace