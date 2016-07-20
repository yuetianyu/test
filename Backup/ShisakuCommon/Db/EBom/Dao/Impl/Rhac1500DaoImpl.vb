Imports ShisakuCommon.Db.EBom.Vo

Namespace Db.EBom.Dao.Impl

    ''' <summary>
    ''' ALの素テーブルの簡単なCRUDを集めたDAO
    ''' </summary>
    ''' <remarks>実装クラス</remarks>
    Public Class Rhac1500DaoImpl : Inherits EBomDaoEachTable(Of Rhac1500Vo)
        Implements Rhac1500Dao
        ''' <summary>
        ''' PrimaryKey を設定する
        ''' </summary>
        ''' <param name="table">テーブルに対応するVOのインスタンス</param>
        ''' <remarks></remarks>
        Protected Overrides Sub SettingPkField(ByVal table As PkTable(Of Vo.Rhac1500Vo))
            Dim vo As New Rhac1500Vo
            table.IsA(vo) _
            .PkField(vo.KaihatsuFugo) _
            .PkField(vo.ShiyoshoSeqno) _
            .PkField(vo.AlKanriNo) _
            .PkField(vo.BlockNo) _
            .PkField(vo.TekiyoKbn) _
            .PkField(vo.KatashikiFugo) _
            .PkField(vo.ShimukechiCode) _
            .PkField(vo.OpCode) _
            .PkField(vo.ColorSpecCode) _
            .PkField(vo.FfBuhinNo)
        End Sub
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
        Public Function FindByPk1(ByVal kaihatsuFuGo As String, _
                                  ByVal shiyoshoSeqno As String, _
                                  ByVal alKanriNo As String, _
                                  ByVal blockNo As String, _
                                  ByVal tekiyoKbn As String, _
                                  ByVal katashikiFugo As String, _
                                  ByVal shimukechiCode As String, _
                                  ByVal opCode As String, _
                                  ByVal colorSpecCode As String, _
                                  ByVal ffBuhinNo As String) As Vo.Rhac1500Vo Implements Rhac1500Dao.FindByPk
            Return FindByPkMain(kaihatsuFuGo, _
                                shiyoshoSeqno, _
                                alKanriNo, _
                                blockNo, _
                                tekiyoKbn, _
                                katashikiFugo, _
                                shimukechiCode, _
                                opCode, _
                                colorSpecCode, _
                                ffBuhinNo)
        End Function
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
        Public Function DeleteByPk1(ByVal kaihatsuFuGo As String, _
                                    ByVal shiyoshoSeqno As String, _
                                    ByVal alKanriNo As String, _
                                    ByVal blockNo As String, _
                                    ByVal tekiyoKbn As String, _
                                    ByVal katashikiFugo As String, _
                                    ByVal shimukechiCode As String, _
                                    ByVal opCode As String, _
                                    ByVal colorSpecCode As String, _
                                    ByVal ffBuhinNo As String) As Integer Implements Rhac1500Dao.DeleteByPk
            Return DeleteByPkMain(kaihatsuFuGo, _
                                  shiyoshoSeqno, _
                                  alKanriNo, _
                                  blockNo, _
                                  tekiyoKbn, _
                                  katashikiFugo, _
                                  shimukechiCode, _
                                  opCode, _
                                  colorSpecCode, _
                                  ffBuhinNo)
        End Function

    End Class


End Namespace
