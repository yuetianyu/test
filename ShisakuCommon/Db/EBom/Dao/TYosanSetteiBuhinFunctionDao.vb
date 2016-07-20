Imports ShisakuCommon.Db.EBom.Vo
Namespace Db.EBom.Dao
    ''' <summary>試作部品編集・予算設定部品表_ﾌｧﾝｸｼｮﾝの簡単なCRUDを集めたDAO</summary>
    Public Interface TYosanSetteiBuhinFunctionDao : Inherits DaoEachTable(Of TYosanSetteiBuhinFunctionVo)

        ''' <summary>
        ''' テーブル値の検索結果を返す
        ''' </summary>
        ''' <param name="YosanFunctionHinban">予算ファンクション品番</param>
        ''' <param name="YosanMakerCode">予算取引先コード</param>
        ''' <returns>該当レコード</returns>
        ''' <remarks></remarks>
        Function FindByPk(ByVal YosanFunctionHinban As String, _
                          ByVal YosanMakerCode As String) As TYosanSetteiBuhinFunctionVo

        ''' <summary>
        ''' 該当レコードを削除する
        ''' </summary>
        ''' <param name="YosanFunctionHinban">予算ファンクション品番</param>
        ''' <param name="YosanMakerCode">取引先コード</param>
        ''' <returns>削除件数</returns>
        ''' <remarks></remarks>
        Function DeleteByPk(ByVal YosanFunctionHinban As String, _
                            ByVal YosanMakerCode As String) As Integer
    End Interface
End Namespace


