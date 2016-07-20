Imports ShisakuCommon.Db.EBom.Vo
Namespace Db.EBom.Dao.Impl
    ''' <summary>予算設定部品表_ﾌｧﾝｸｼｮﾝの簡単なCRUDを集めたDAO</summary>
    ''' <remarks>実装クラス</remarks>

    Public Class TYosanSetteiBuhinFunctionDaoImpl : Inherits EBomDaoEachTable(Of TYosanSetteiBuhinFunctionVo)
        Implements TYosanSetteiBuhinFunctionDao

        ''' <summary>
        ''' PrimaryKey を設定する
        ''' </summary>
        ''' <param name="table">テーブルに対応するVOのインスタンス</param>
        ''' <remarks></remarks>
        Protected Overrides Sub SettingPkField(ByVal table As PkTable(Of Vo.TYosanSetteiBuhinFunctionVo))
            Dim vo As New TYosanSetteiBuhinFunctionVo
            table.IsA(vo).PkField(vo.YosanFunctionHinban) _
            .PkField(vo.YosanMakerCode)
        End Sub

        ''' <summary>
        ''' テーブル値の検索結果を返す
        ''' </summary>
        ''' <param name="YosanFunctionHinban">予算ファンクション品番</param>
        ''' <param name="YosanMakerCode">予算取引先コード</param>
        ''' <returns>該当レコード</returns>
        ''' <remarks></remarks>
        Public Function DeleteByPk(ByVal YosanFunctionHinban As String, _
                                   ByVal YosanMakerCode As String) As Integer Implements TYosanSetteiBuhinFunctionDao.DeleteByPk
            Return DeleteByPkMain(YosanFunctionHinban, _
                                  YosanMakerCode)
        End Function
        ''' <summary>
        ''' 該当レコードを削除する
        ''' </summary>
        ''' <param name="YosanFunctionHinban">予算ファンクション品番</param>
        ''' <returns>削除件数</returns>
        ''' <remarks></remarks>
        Public Function FindByPk(ByVal YosanFunctionHinban As String, _
                                 ByVal YosanMakerCode As String) As Vo.TYosanSetteiBuhinFunctionVo Implements TYosanSetteiBuhinFunctionDao.FindByPk
            Return FindByPkMain(YosanFunctionHinban, _
                                YosanMakerCode)
        End Function

    End Class
End Namespace


