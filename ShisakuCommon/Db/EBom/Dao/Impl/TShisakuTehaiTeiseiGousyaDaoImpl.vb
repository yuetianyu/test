﻿Imports ShisakuCommon.Db.EBom.Vo
Namespace Db.EBom.Dao.Impl
    ''' <summary>の簡単なCRUDを集めたDAO</summary>
    ''' <remarks>実装クラス</remarks>
    Public Class TShisakuTehaiTeiseiGousyaDaoImpl : Inherits EBomDaoEachTable(Of TShisakuTehaiTeiseiGousyaVo)
        Implements TShisakuTehaiTeiseiGousyaDao
        ''' <summary>PrimaryKey を設定する</summary>
        ''' <param name="table">テーブルに対応するVOのインスタンス</param>
        ''' <remarks></remarks>
        Protected Overrides Sub SettingPkField(ByVal table As PkTable(Of Vo.TShisakuTehaiTeiseiGousyaVo))
            Dim vo As New TShisakuTehaiTeiseiGousyaVo
            table.IsA(vo).PkField(vo.ShisakuEventCode) _
            .PkField(vo.ShisakuListCode) _
            .PkField(vo.ShisakuListCodeKaiteiNo) _
            .PkField(vo.ShisakuBukaCode) _
            .PkField(vo.ShisakuBlockNo) _
            .PkField(vo.BuhinNoHyoujiJun) _
            .PkField(vo.Flag) _
            .PkField(vo.ShisakuGousyaHyoujiJun)
        End Sub

        ''' <summary>テーブル値の検索結果を返す</summary>
        ''' <param name="ShisakuEventCode">試作イベントコード</param>
        ''' <param name="ShisakuListCode">試作リストコード</param>
        ''' <param name="ShisakuListCodeKaiteiNo">試作リストコード改訂№</param>
        ''' <param name="ShisakuBukaCode">試作部課コード</param>
        ''' <param name="ShisakuBlockNo">試作ブロック№</param>
        ''' <param name="BuhinNoHyoujiJun">部品番号表示順</param>
        ''' <param name="Flag">フラグ</param>
        ''' <param name="ShisakuGousyaHyoujiJun">試作号車表示順</param>
        ''' <returns>該当レコード</returns>
        ''' <remarks></remarks>
        Public Function FindByPk(ByVal ShisakuEventCode As String, _
                          ByVal ShisakuListCode As String, _
                          ByVal ShisakuListCodeKaiteiNo As String, _
                          ByVal ShisakuBukaCode As String, _
                          ByVal ShisakuBlockNo As String, _
                          ByVal BuhinNoHyoujiJun As Nullable(Of Int32), _
                          ByVal Flag As String, _
                          ByVal ShisakuGousyaHyoujiJun As Nullable(Of Int32)) As TShisakuTehaiTeiseiGousyaVo Implements TShisakuTehaiTeiseiGousyaDao.FindByPk

            Return FindByPkMain(ShisakuEventCode, _
                        ShisakuListCode, _
                        ShisakuListCodeKaiteiNo, _
                        ShisakuBukaCode, _
                        ShisakuBlockNo, _
                        BuhinNoHyoujiJun, _
                        Flag, _
                        ShisakuGousyaHyoujiJun)
        End Function

        ''' <summary>該当レコードを削除する</summary>
        ''' <param name="ShisakuEventCode">試作イベントコード</param>
        ''' <param name="ShisakuListCode">試作リストコード</param>
        ''' <param name="ShisakuListCodeKaiteiNo">試作リストコード改訂№</param>
        ''' <param name="ShisakuBukaCode">試作部課コード</param>
        ''' <param name="ShisakuBlockNo">試作ブロック№</param>
        ''' <param name="BuhinNoHyoujiJun">部品番号表示順</param>
        ''' <param name="Flag">フラグ</param>
        ''' <param name="ShisakuGousyaHyoujiJun">試作号車表示順</param>
        ''' <returns>削除件数</returns>
        ''' <remarks></remarks>
        Function DeleteByPk(ByVal ShisakuEventCode As String, _
                          ByVal ShisakuListCode As String, _
                          ByVal ShisakuListCodeKaiteiNo As String, _
                          ByVal ShisakuBukaCode As String, _
                          ByVal ShisakuBlockNo As String, _
                          ByVal BuhinNoHyoujiJun As Nullable(Of Int32), _
                          ByVal Flag As String, _
                          ByVal ShisakuGousyaHyoujiJun As Nullable(Of Int32)) As Integer Implements TShisakuTehaiTeiseiGousyaDao.DeleteByPk
            Return DeleteByPkMain(ShisakuEventCode, _
                        ShisakuListCode, _
                        ShisakuListCodeKaiteiNo, _
                        ShisakuBukaCode, _
                        ShisakuBlockNo, _
                        BuhinNoHyoujiJun, _
                        Flag, _
                        ShisakuGousyaHyoujiJun)
        End Function

    End Class
End Namespace