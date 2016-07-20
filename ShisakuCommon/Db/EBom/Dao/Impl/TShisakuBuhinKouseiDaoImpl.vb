
Imports ShisakuCommon.Db.EBom.Vo

Namespace Db.EBom.Dao.Impl
    ''' <summary>
    ''' 試作部品構成情報テーブルの簡単なCRUDを集めたDAO
    ''' </summary>
    ''' <remarks>実装クラス</remarks>
    Public Class TShisakuBuhinKouseiDaoImpl : Inherits EBomDaoEachTable(Of TShisakuBuhinKouseiVo)
        Implements TShisakuBuhinKouseiDao

        ''' <summary>
        ''' PrimaryKey を設定する
        ''' </summary>
        ''' <param name="table">テーブルに対応するVOのインスタンス</param>
        ''' <remarks></remarks>
        Protected Overrides Sub SettingPkField(ByVal table As PkTable(Of Vo.TShisakuBuhinKouseiVo))
            Dim vo As New TShisakuBuhinKouseiVo
            table.IsA(vo) _
            .PkField(vo.ShisakuEventCode) _
            .PkField(vo.ShisakuBukaCode) _
            .PkField(vo.ShisakuBlockNo) _
            .PkField(vo.ShisakuBlockNoKaiteiNo) _
            .PkField(vo.ShisakuGousya) _
            .PkField(vo.InstlHinban) _
            .PkField(vo.InstlHinbanKbn) _
            .PkField(vo.BuhinNoOya) _
            .PkField(vo.BuhinNoKbnOya) _
            .PkField(vo.BuhinNoKo) _
            .PkField(vo.BuhinNoKbnKo) _
            .PkField(vo.KaiteiNo)
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
        ''' <param name="instlHinbanKbn">INSTL品番試作区分</param>
        ''' <param name="buhinNoOya">部品番号(親)</param>
        ''' <param name="buhinNoKbnOya">部品番号区分（親）</param>
        ''' <param name="buhinNoKo">部品番号(子)</param>
        ''' <param name="buhinNoKbnKo">部品番号区分（子）</param>
        ''' <param name="kaiteiNo">構成改訂No.</param>
        ''' <returns>該当レコード</returns>
        ''' <remarks></remarks>
        Public Function FindByPk(ByVal shisakuEventCode As String, _
                                 ByVal shisakuBukaCode As String, _
                                 ByVal shisakuBlockNo As String, _
                                 ByVal shisakuBlockNoKaiteiNo As String, _
                                 ByVal shisakuGousya As String, _
                                 ByVal instlHinban As String, _
                                 ByVal instlHinbanKbn As String, _
                                 ByVal buhinNoOya As String, _
                                 ByVal buhinNoKbnOya As String, _
                                 ByVal buhinNoKo As String, _
                                 ByVal buhinNoKbnKo As String, _
                                 ByVal kaiteiNo As String) As Vo.TShisakuBuhinKouseiVo Implements TShisakuBuhinKouseiDao.FindByPk
            Return FindByPkMain(shisakuEventCode, _
                                shisakuBukaCode, _
                                shisakuBlockNo, _
                                shisakuBlockNoKaiteiNo, _
                                shisakuGousya, _
                                instlHinban, _
                                instlHinbanKbn, _
                                buhinNoOya, _
                                buhinNoKbnOya, _
                                buhinNoKo, _
                                buhinNoKbnKo, _
                                kaiteiNo)
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
        ''' <param name="instlHinbanKbn">INSTL品番試作区分</param>
        ''' <param name="buhinNoOya">部品番号(親)</param>
        ''' <param name="buhinNoKbnOya">部品番号区分（親）</param>
        ''' <param name="buhinNoKo">部品番号(子)</param>
        ''' <param name="buhinNoKbnKo">部品番号区分（子）</param>
        ''' <param name="kaiteiNo">構成改訂No.</param>
        ''' <returns>削除件数</returns>
        ''' <remarks></remarks>         
        Public Function DeleteByPk(ByVal shisakuEventCode As String, _
                                   ByVal shisakuBukaCode As String, _
                                   ByVal shisakuBlockNo As String, _
                                   ByVal shisakuBlockNoKaiteiNo As String, _
                                   ByVal shisakuGousya As String, _
                                   ByVal instlHinban As String, _
                                   ByVal instlHinbanKbn As String, _
                                   ByVal buhinNoOya As String, _
                                   ByVal buhinNoKbnOya As String, _
                                   ByVal buhinNoKo As String, _
                                   ByVal buhinNoKbnKo As String, _
                                   ByVal kaiteiNo As String) As Integer Implements TShisakuBuhinKouseiDao.DeleteByPk
            Return DeleteByPkMain(shisakuEventCode, _
                                  shisakuBukaCode, _
                                  shisakuBlockNo, _
                                  shisakuBlockNoKaiteiNo, _
                                  shisakuGousya, _
                                  instlHinban, _
                                  instlHinbanKbn, _
                                  buhinNoOya, _
                                  buhinNoKbnOya, _
                                  buhinNoKo, _
                                  buhinNoKbnKo, _
                                  kaiteiNo)
        End Function

    End Class
End Namespace