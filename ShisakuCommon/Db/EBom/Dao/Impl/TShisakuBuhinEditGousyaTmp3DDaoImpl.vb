Imports ShisakuCommon.Db.EBom.Vo
Namespace Db.EBom.Dao.Impl
    ''' <summary>の簡単なCRUDを集めたDAO</summary>
    ''' <remarks>実装クラス</remarks>
    Public Class TShisakuBuhinEditGousyaTmp3dDaoImpl : Inherits EBomDaoEachTable(Of TShisakuBuhinEditGousyaTmp3dVo)
        Implements TShisakuBuhinEditGousyaTmp3dDao
        ''' <summary>PrimaryKey を設定する</summary>
        ''' <param name="table">テーブルに対応するVOのインスタンス</param>
        ''' <remarks></remarks>
        Protected Overrides Sub SettingPkField(ByVal table As PkTable(Of Vo.TShisakuBuhinEditGousyaTmp3dVo))
            Dim vo As New TShisakuBuhinEditGousyaTmp3dVo
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
        Public Function FindByPk(ByVal GUID As String, _
                                 ByVal ShisakuEventCode As String, _
                          ByVal ShisakuBukaCode As String, _
                          ByVal ShisakuBlockNo As String, _
                          ByVal ShisakuBlockNoKaiteiNo As String, _
                          ByVal BuhinNoHyoujiJun As Nullable(Of Int32), _
                          ByVal ShisakuGousyaHyoujiJun As Nullable(Of Int32), _
                          ByVal GyouId As String) As TShisakuBuhinEditGousyaTmp3dVo Implements TShisakuBuhinEditGousyaTmp3dDao.FindByPk

            Return FindByPkMain(ShisakuEventCode, _
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
        Function DeleteByPk(ByVal GUID As String, _
                            ByVal ShisakuEventCode As String, _
                          ByVal ShisakuBukaCode As String, _
                          ByVal ShisakuBlockNo As String, _
                          ByVal ShisakuBlockNoKaiteiNo As String, _
                          ByVal BuhinNoHyoujiJun As Nullable(Of Int32), _
                          ByVal ShisakuGousyaHyoujiJun As Nullable(Of Int32), _
                          ByVal GyouId As String) As Integer Implements TShisakuBuhinEditGousyaTmp3dDao.DeleteByPk
            Return DeleteByPkMain(ShisakuEventCode, _
                        ShisakuBukaCode, _
                        ShisakuBlockNo, _
                        ShisakuBlockNoKaiteiNo, _
                        BuhinNoHyoujiJun, _
                        GyouId)
        End Function

        ''' <summary>
        ''' GUIDをベースにブロック一覧を取得する.
        ''' </summary>
        ''' <param name="aGUID">GUID</param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Shared Function getBlockList(ByVal aGUID As Guid) As List(Of String)
            Dim sb As New System.Text.StringBuilder

            With sb
                .AppendLine("SELECT SHISAKU_BLOCK_NO")
                .AppendLine("    FROM " & MBOM_DB_NAME & ".DBO.T_SHISAKU_BUHIN_EDIT_GOUSYA_TMP_3D ")
                .AppendLine("WHERE")

                .AppendLine("    GUID = '" & aGUID.ToString & "'")
                'MessageBox.Show("デバッグ値を設定中.")
                '.AppendLine("    e.guid = '34A147EB-45F9-4243-B1DB-324D3FB0A116'")

                .AppendLine("GROUP BY ")
                .AppendLine("    SHISAKU_BLOCK_NO")

            End With

            Dim db As New EBomDbClient
            Dim BlockList As List(Of TShisakuBuhinEditGousyaTmp3dVo) = db.QueryForList(Of TShisakuBuhinEditGousyaTmp3dVo)(sb.ToString)
            Dim retBlock As New List(Of String)

            For Each lBlock In BlockList
                retBlock.Add(lBlock.ShisakuBlockNo)
            Next

            Return retBlock

        End Function


    End Class
End Namespace