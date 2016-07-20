Imports ShisakuCommon.Db.EBom.Vo

Namespace Db.EBom.Dao
    ''' <summary>
    ''' テーブルの簡単なCRUDを集めたDAO
    ''' </summary>
    ''' <remarks></remarks>
    Public Interface Rhac1500Dao : Inherits DaoEachTable(Of Rhac1500Vo)

        ''' <summary>
        ''' テーブル値の検索結果を返す
        ''' </summary>
        ''' <param name="kaihatsuFuGo">開発符号</param>
        ''' <param name="shiyoshoSeqno">仕様書一連No.</param>
        ''' <param name="alKanriNo">A/L管理No.</param>
        ''' <param name="blockNo">ブロックNo.</param>
        ''' <param name="tekiyoKbn">適用区分</param>
        ''' <param name="katashikiFugo">型式符号</param>
        ''' <param name="shimukechiCode">仕向地コード</param>
        ''' <param name="opCode">OPコード</param>
        ''' <param name="colorSpecCode">カラースペックコード</param>
        ''' <param name="ffBuhinNo">付加F品番</param>
        ''' <returns>該当レコード</returns>
        ''' <remarks></remarks>
        Function FindByPk(ByVal kaihatsuFuGo As String, _
                          ByVal shiyoshoSeqno As String, _
                          ByVal alKanriNo As String, _
                          ByVal blockNo As String, _
                          ByVal tekiyoKbn As String, _
                          ByVal katashikiFugo As String, _
                          ByVal shimukechiCode As String, _
                          ByVal opCode As String, _
                          ByVal colorSpecCode As String, _
                          ByVal ffBuhinNo As String) As Rhac1500Vo

        ''' <summary>
        ''' 該当レコードを削除する
        ''' </summary>
        ''' <param name="kaihatsuFuGo">開発符号</param>
        ''' <param name="shiyoshoSeqno">仕様書一連No.</param>
        ''' <param name="alKanriNo">A/L管理No.</param>
        ''' <param name="blockNo">ブロックNo.</param>
        ''' <param name="tekiyoKbn">適用区分</param>
        ''' <param name="katashikiFugo">型式符号</param>
        ''' <param name="shimukechiCode">仕向地コード</param>
        ''' <param name="opCode">OPコード</param>
        ''' <param name="colorSpecCode">カラースペックコード</param>
        ''' <param name="ffBuhinNo">付加F品番</param>
        ''' <returns>削除件数</returns>
        ''' <remarks></remarks>
        Function DeleteByPk(ByVal kaihatsuFuGo As String, _
                            ByVal shiyoshoSeqno As String, _
                            ByVal alKanriNo As String, _
                            ByVal blockNo As String, _
                            ByVal tekiyoKbn As String, _
                            ByVal katashikiFugo As String, _
                            ByVal shimukechiCode As String, _
                            ByVal opCode As String, _
                            ByVal colorSpecCode As String, _
                            ByVal ffBuhinNo As String) As Integer

    End Interface

End Namespace

