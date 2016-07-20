Imports ShisakuCommon.Db
Imports ShisakuCommon.Util.LabelValue
Imports EventSakusei.EventEdit.Dao
Imports ShisakuCommon.Db.EBom.Vo

Namespace ShisakuBuhinEditBlock.Dao
    Public Interface IShisakuBuhinEditBlockDao : Inherits DaoEachFeature

        Function GetKabetuJyoutai(ByVal eventCode As String, _
                                                ByVal bukaCode As String _
                                                ) As KabetuJyoutaiVo

        Function GetBlockSpreadList(ByVal eventCode As String, _
                                         ByVal bukaCode As String _
                                        ) As List(Of ShisakuBuhinBlockVo)

        Sub UpdateByPk(ByVal shisakuBlock As TShisakuSekkeiBlockVo)

        Function UpdateByPkHasReturn(ByVal shisakuBlock As TShisakuSekkeiBlockVo) As ShisakuBuhinBlockVo

        'ブロック不要のせいで改訂があがった時用'
        Function UpdateByPkHasReturnBlockFuyou(ByVal shisakuBlock As TShisakuSekkeiBlockVo) As ShisakuBuhinBlockVo


        Sub UpdateByPkMove(ByVal shisakuBlock As TShisakuSekkeiBlockVo)
        '仕様変更により承認欄の中身を空にする処理を追加'
        Sub UpdateByPkMove2(ByVal shisakuBlock As TShisakuSekkeiBlockVo)



        Function FindByPk(ByVal shisakuEventCode As String, _
                          ByVal shisakuBukaCode As String, _
                          ByVal shisakuBlockNo As String, _
                          ByVal shisakuBlockNoKaiteiNo As String) As ShisakuBuhinBlockVo

        Sub InsertByUpdateKaiteiNo(ByVal shisakuBlock As TShisakuSekkeiBlockVo)

        Sub InsertByUpdateKaiteiNoFuyou(ByVal shisakuBlock As TShisakuSekkeiBlockVo)

        '--------------------------------------------------------------------------------------
        '２次改修追加
        '   ブロックの改訂情報を取得する。
        Function GetBlockKaiteiList(ByVal shisakuEventCode As String, ByVal blockNo As String) As List(Of TShisakuSekkeiBlockVo)
        '--------------------------------------------------------------------------------------

    End Interface

End Namespace

