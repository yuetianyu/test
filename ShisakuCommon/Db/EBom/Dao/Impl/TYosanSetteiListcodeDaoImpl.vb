Imports ShisakuCommon.Db.EBom.Vo
Namespace Db.EBom.Dao.Impl
    ''' <summary>予算設定リストコード情報の簡単なCRUDを集めたDAO</summary>
    ''' <remarks>実装クラス</remarks>

    Public Class TYosanSetteiListcodeDaoImpl : Inherits EBomDaoEachTable(Of TYosanSetteiListcodeVo)
        Implements TYosanSetteiListcodeDao

        ''' <summary>
        ''' PrimaryKey を設定する
        ''' </summary>
        ''' <param name="table">テーブルに対応するVOのインスタンス</param>
        ''' <remarks></remarks>
        Protected Overrides Sub SettingPkField(ByVal table As PkTable(Of Vo.TYosanSetteiListcodeVo))
            Dim vo As New TYosanSetteiListcodeVo
            table.IsA(vo).PkField(vo.ShisakuEventCode) _
            .PkField(vo.YosanListHyojijunNo) _
            .PkField(vo.YosanListCode) _
            .PkField(vo.YosanGroupNo)
        End Sub


        ''' <summary>
        ''' テーブル値の検索結果を返す
        ''' </summary>
        ''' <param name="_ShisakuEventCode">試作イベントコード</param>
        ''' <param name="_YosanListHyojijunNo">予算リスト表示順</param>
        ''' <param name="_YosanListCode">予算リストコード</param>
        ''' <param name="_YosanGroupNo">予算グループ№</param>      
        ''' <returns>削除件数</returns>
        ''' <remarks></remarks>
        Public Function DeleteByPk(ByVal _ShisakuEventCode As String, _
                                   ByVal _YosanListHyojijunNo As Nullable(Of Int32), _
                                   ByVal _YosanListCode As String, _
                                   ByVal _YosanGroupNo As String) As Integer Implements TYosanSetteiListcodeDao.DeleteByPk
            Return DeleteByPkMain(_ShisakuEventCode, _
                                  _YosanListHyojijunNo, _
                                  _YosanListCode, _
                                  _YosanGroupNo)
        End Function
        ''' <summary>
        ''' テーブル値の検索結果を返す
        ''' </summary>
        ''' <param name="_ShisakuEventCode">試作イベントコード</param>
        ''' <param name="_YosanListHyojijunNo">予算リスト表示順</param>
        ''' <param name="_YosanListCode">予算リストコード</param>
        ''' <param name="_YosanGroupNo">予算グループ№</param>      
        ''' <returns>該当レコード</returns>
        ''' <remarks></remarks>
        Public Function FindByPk(ByVal _ShisakuEventCode As String, _
                                   ByVal _YosanListHyojijunNo As Nullable(Of Int32), _
                                   ByVal _YosanListCode As String, _
                                   ByVal _YosanGroupNo As String) As Vo.TYosanSetteiListcodeVo Implements TYosanSetteiListcodeDao.FindByPk
            Return FindByPkMain(_ShisakuEventCode, _
                                  _YosanListHyojijunNo, _
                                  _YosanListCode, _
                                  _YosanGroupNo)
        End Function

    End Class
End Namespace





