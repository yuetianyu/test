Imports ShisakuCommon.Db.EBom.Vo

Namespace Db.EBom.Dao.Impl

    ''' <summary>
    ''' 試作設計ブロックINSTL情報(EBOM設変)テーブルの簡単なCRUDを集めたDAO
    ''' </summary>
    ''' <remarks>実装クラス</remarks>
    Public Class TShisakuSekkeiBlockInstlEbomKanshiDaoImpl : Inherits EBomDaoEachTable(Of TShisakuSekkeiBlockInstlEbomKanshiVo)
        Implements TShisakuSekkeiBlockInstlEbomKanshiDao

        ''' <summary>
        ''' PrimaryKey を設定する
        ''' </summary>
        ''' <param name="table">テーブルに対応するVOのインスタンス</param>
        ''' <remarks></remarks>
        Protected Overrides Sub SettingPkField(ByVal table As PkTable(Of TShisakuSekkeiBlockInstlEbomKanshiVo))
            Dim vo As New TShisakuSekkeiBlockInstlEbomKanshiVo
            table.IsA(vo).PkField(vo.ShisakuEventCode) _
                         .PkField(vo.ShisakuBukaCode) _
                         .PkField(vo.ShisakuBlockNo) _
                         .PkField(vo.ShisakuBlockNoKaiteiNo) _
                         .PkField(vo.ShisakuGousya) _
                         .PkField(vo.InstlHinban) _
                         .PkField(vo.InstlHinbanKbn) _
                         .PkField(vo.InstlDataKbn)

        End Sub

        ''' <summary>
        ''' テーブル値の検索結果を返す
        ''' </summary>
        ''' <param name="shisakuEventCode">試作イベントコード</param>
        ''' <param name="shisakuBukaCode">試作部課コード</param>
        ''' <param name="shisakuBlockNo">試作ブロック№</param>
        ''' <param name="shisakuBlockNoKaiteiNo">試作ブロック№改訂№</param>
        ''' <param name="shisakuGousya">試作号車</param>
        ''' <param name="instlHinban">INSTL品番</param>
        ''' <param name="instlHinbanKbn">INSTL品番区分</param>
        ''' <param name="InstlDataKbn">INSTL元データ区分</param>
        ''' <returns>該当レコード</returns>
        ''' <remarks></remarks>
        Public Function FindByPk(ByVal shisakuEventCode As String, _
                                 ByVal shisakuBukaCode As String, _
                                 ByVal shisakuBlockNo As String, _
                                 ByVal shisakuBlockNoKaiteiNo As String, _
                                 ByVal shisakuGousya As String, _
                                 ByVal instlHinban As String, _
                                 ByVal instlHinbanKbn As String, _
                                 ByVal InstlDataKbn As String) As TShisakuSekkeiBlockInstlEbomKanshiVo Implements TShisakuSekkeiBlockInstlEbomKanshiDao.FindByPk
            Return FindByPkMain(shisakuEventCode, _
                                shisakuBukaCode, _
                                shisakuBlockNo, _
                                shisakuBlockNoKaiteiNo, _
                                shisakuGousya, _
                                instlHinban, _
                                instlHinbanKbn, _
                                InstlDataKbn)
        End Function

        ''' <summary>
        ''' 該当レコードを削除する
        ''' </summary>
        ''' <param name="shisakuEventCode">試作イベントコード</param>
        ''' <param name="shisakuBukaCode">試作部課コード</param>
        ''' <param name="shisakuBlockNo">試作ブロック№</param>
        ''' <param name="shisakuBlockNoKaiteiNo">試作ブロック№改訂№</param>
        ''' <param name="shisakuGousya">試作号車</param>
        ''' <param name="instlHinban">INSTL品番</param>
        ''' <param name="instlHinbanKbn">INSTL品番区分</param>
        ''' <param name="InstlDataKbn">INSTL元データ区分</param>
        ''' <returns>削除件数</returns>
        ''' <remarks></remarks>
        Public Function DeleteByPk(ByVal shisakuEventCode As String, _
                                   ByVal shisakuBukaCode As String, _
                                   ByVal shisakuBlockNo As String, _
                                   ByVal shisakuBlockNoKaiteiNo As String, _
                                   ByVal shisakuGousya As String, _
                                   ByVal instlHinban As String, _
                                   ByVal instlHinbanKbn As String, _
                                   ByVal InstlDataKbn As String) As Integer Implements TShisakuSekkeiBlockInstlEbomKanshiDao.DeleteByPk
            Return DeleteByPkMain(shisakuEventCode, _
                                  shisakuBukaCode, _
                                  shisakuBlockNo, _
                                  shisakuBlockNoKaiteiNo, _
                                  shisakuGousya, _
                                  instlHinban, _
                                  instlHinbanKbn, _
                                  InstlDataKbn)
        End Function

    End Class
End Namespace
