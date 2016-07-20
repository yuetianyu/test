Imports ShisakuCommon.Db.EBom.Vo

Namespace Db.EBom.Dao.Impl
    ''' <summary>
    ''' 属性テーブルの簡単なCRUDを集めたDAO
    ''' </summary>
    ''' <remarks>実装クラス</remarks>

    Public Class Rhac1920DaoImpl : Inherits EBomDaoEachTable(Of Rhac1920Vo)
        Implements Rhac1920Dao

        Protected Overrides Sub SettingPkField(ByVal table As PkTable(Of Vo.Rhac1920Vo))
            Dim vo As New Rhac1920Vo
            table.IsA(vo) _
            .PkField(vo.KaihatsuFugo) _
            .PkField(vo.BlockNo) _
            .PkField(vo.BuhinNo) _
            .PkField(vo.BlkBuhinKaiteiNo)
        End Sub

        ''' <summary>
        ''' テーブル値の検索結果を返す
        ''' </summary>
        ''' <param name="kaihatsuFugo">開発符号</param>
        ''' <param name="blockNo">ブロックNo</param>
        ''' <param name="buhinNo">部品番号</param>
        ''' <param name="blkBuhinKaiteiNo">BLK部品改訂No</param>
        ''' <returns>該当レコード</returns>
        ''' <remarks></remarks>
        Public Function FindByPk1(ByVal kaihatsuFugo As String, _
                                  ByVal blockNo As String, _
                                  ByVal buhinNo As String, _
                                  ByVal blkBuhinKaiteiNo As String) As Rhac1920Vo Implements Rhac1920Dao.FindByPk
            Return FindByPkMain(kaihatsuFugo, blockNo, buhinNo, blkBuhinKaiteiNo)
        End Function

        ''' <summary>
        ''' 該当レコードを削除する
        ''' </summary>
        ''' <param name="kaihatsuFugo">開発符号</param>
        ''' <param name="blockNo">ブロックNo</param>
        ''' <param name="buhinNo">部品番号</param>
        ''' <param name="blkBuhinKaiteiNo">BLK部品改訂No</param>
        ''' <returns>削除件数</returns>
        ''' <remarks></remarks>
        Public Function DeleteByPk1(ByVal kaihatsuFugo As String, _
                                    ByVal blockNo As String, _
                                    ByVal buhinNo As String, _
                                    ByVal blkBuhinKaiteiNo As String) As Integer Implements Rhac1920Dao.DeleteByPk
            Return DeleteByPkMain(kaihatsuFugo, buhinNo, blkBuhinKaiteiNo)
        End Function

    End Class
End Namespace