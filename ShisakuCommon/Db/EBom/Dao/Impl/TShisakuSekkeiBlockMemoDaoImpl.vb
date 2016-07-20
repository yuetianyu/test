Imports ShisakuCommon.Db.EBom.Vo

Namespace Db.EBom.Dao.Impl

    ''' <summary>
    ''' 車系／開発符号マスターテーブルの簡単なCRUDを集めたDAO
    ''' </summary>
    ''' <remarks>実装クラス</remarks>
    Public Class TShisakuSekkeiBlockMemoDaoImpl : Inherits EBomDaoEachTable(Of TShisakuSekkeiBlockMemoVo)
        Implements TShisakuSekkeiBlockMemoDao

        ''' <summary>
        ''' PrimaryKey を設定する
        ''' </summary>
        ''' <param name="table">テーブルに対応するVOのインスタンス</param>
        ''' <remarks></remarks>
        Protected Overrides Sub SettingPkField(ByVal table As PkTable(Of TShisakuSekkeiBlockMemoVo))
            Dim vo As New TShisakuSekkeiBlockMemoVo
            table.IsA(vo) _
            .PkField(vo.ShisakuEventCode) _
            .PkField(vo.ShisakuBukaCode) _
            .PkField(vo.ShisakuBlockNo) _
            .PkField(vo.ShisakuBlockNoKaiteiNo) _
            .PkField(vo.ShisakuGousya) _
            .PkField(vo.ShisakuMemoHyoujiJun)
        End Sub

        ''' <summary>
        ''' テーブル値の検索結果を返す
        ''' </summary>
        ''' <param name="shisakuEventCode">試作イベントコード</param>
        ''' <param name="shisakuBukaCode">試作部課コード</param>
        ''' <param name="shisakuBlockNo">試作ブロック№</param>
        ''' <param name="shisakuBlockNoKaiteiNo">試作ブロック№改訂№</param>
        ''' <param name="shisakuGousya">試作号車</param>
        ''' <param name="shisakuMemoHyoujiJun">試作メモ表示順</param>
        ''' <returns>該当レコード</returns>
        ''' <remarks></remarks>
        Public Function FindByPk(ByVal shisakuEventCode As String, ByVal shisakuBukaCode As String _
                         , ByVal shisakuBlockNo As String, ByVal shisakuBlockNoKaiteiNo As String _
                         , ByVal shisakuGousya As String, ByVal shisakuMemoHyoujiJun As Nullable(Of Int32)) As Vo.TShisakuSekkeiBlockMemoVo Implements TShisakuSekkeiBlockMemoDao.FindByPk
            Return FindByPkMain(shisakuEventCode, shisakuBukaCode, shisakuBlockNo _
                               , shisakuBlockNoKaiteiNo, shisakuGousya, shisakuMemoHyoujiJun)
        End Function

        ''' <summary>
        ''' 該当レコードを削除する
        ''' </summary>
        ''' <param name="shisakuEventCode">試作イベントコード</param>
        ''' <param name="shisakuBukaCode">試作部課コード</param>
        ''' <param name="shisakuBlockNo">試作ブロック№</param>
        ''' <param name="shisakuBlockNoKaiteiNo">試作ブロック№改訂№</param>
        ''' <param name="shisakuGousya">試作号車</param>
        ''' <param name="shisakuMemoHyoujiJun">試作メモ表示順</param>
        ''' <returns>削除件数</returns>
        ''' <remarks></remarks>
        Public Function DeleteByPk(ByVal shisakuEventCode As String, ByVal shisakuBukaCode As String _
                         , ByVal shisakuBlockNo As String, ByVal shisakuBlockNoKaiteiNo As String _
                         , ByVal shisakuGousya As String, ByVal shisakuMemoHyoujiJun As Nullable(Of Int32)) As Integer Implements TShisakuSekkeiBlockMemoDao.DeleteByPk
            Return DeleteByPkMain(shisakuEventCode, shisakuBukaCode, shisakuBlockNo _
                               , shisakuBlockNoKaiteiNo, shisakuGousya, shisakuMemoHyoujiJun)
        End Function

    End Class
End Namespace



