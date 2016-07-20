Imports ShisakuCommon.Db.EBom.Vo

Namespace Db.EBom.Dao.Impl

    ''' <summary>
    ''' 車系／開発符号マスターテーブルの簡単なCRUDを集めたDAO
    ''' </summary>
    ''' <remarks>実装クラス</remarks>
    Public Class MShisakuKaihatufugoDaoImpl : Inherits EBomDaoEachTable(Of MShisakuKaihatufugoVo)
        Implements MShisakuKaihatufugoDao

        ''' <summary>
        ''' PrimaryKey を設定する
        ''' </summary>
        ''' <param name="table">テーブルに対応するVOのインスタンス</param>
        ''' <remarks></remarks>
        Protected Overrides Sub SettingPkField(ByVal table As PkTable(Of MShisakuKaihatufugoVo))
            Dim vo As New MShisakuKaihatufugoVo
            table.IsA(vo) _
            .PkField(vo.ShisakuShakeiCode) _
            .PkField(vo.ShisakuKaihatsuFugo) _
            .PkField(vo.HyojijunNo)
        End Sub

        ''' <summary>
        ''' テーブル値の検索結果を返す
        ''' </summary>
        ''' <param name="shisakuShakeiCode">試作車系</param>
        ''' <param name="shisakuKaihatsuFugo">試作開発符号</param>
        ''' <param name="hyojijunNo">表示順 </param>
        ''' <returns>該当レコード</returns>
        ''' <remarks></remarks>
        Public Function FindByPk(ByVal shisakuShakeiCode As String, _
                                 ByVal shisakuKaihatsuFugo As String, _
                                 ByVal hyojijunNo As Nullable(Of Int32)) As Vo.MShisakuKaihatufugoVo Implements MShisakuKaihatufugoDao.FindByPk
            Return FindByPkMain(shisakuShakeiCode, shisakuKaihatsuFugo)
        End Function

        ''' <summary>
        ''' 該当レコードを削除する
        ''' </summary>
        ''' <param name="shisakuShakeiCode">試作車系</param>
        ''' <param name="shisakuKaihatsuFugo">試作開発符号</param>
        ''' <param name="hyojijunNo">表示順 </param>
        ''' <returns>削除件数</returns>
        ''' <remarks></remarks>
        Public Function DeleteByPk(ByVal shisakuShakeiCode As String, _
                                   ByVal shisakuKaihatsuFugo As String, _
                                   ByVal hyojijunNo As Nullable(Of Int32)) As Integer Implements MShisakuKaihatufugoDao.DeleteByPk
            Return DeleteByPkMain(shisakuShakeiCode, shisakuKaihatsuFugo)
        End Function

    End Class
End Namespace


