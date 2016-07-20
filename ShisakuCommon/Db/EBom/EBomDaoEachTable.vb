
Imports ShisakuCommon.Db.Impl

Namespace Db.EBom
    ''' <summary>
    ''' EBom用のテーブル毎Daoの基底実装クラス
    ''' </summary>
    ''' <typeparam name="T">テーブルのVo型</typeparam>
    ''' <remarks></remarks>
    Public MustInherit Class EBomDaoEachTable(Of T) : Inherits DaoEachTableImpl(Of T)

        ''' <summary>
        ''' 新しい ShisakuDbClient のインスタンスを生成して返す
        ''' </summary>
        ''' <returns>新しい ShisakuDbClient のインスタンス</returns>
        ''' <remarks></remarks>
        Protected Overrides Function NewDbClient() As ShisakuDbClient
            Return New EBomDbClient
        End Function
    End Class
End Namespace
