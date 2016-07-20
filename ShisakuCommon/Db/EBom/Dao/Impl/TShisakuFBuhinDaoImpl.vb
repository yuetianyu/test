Imports ShisakuCommon.Db.EBom.Vo

Namespace Db.EBom.Dao.Impl
    ''' <summary>
    ''' 試作12桁型式-F品番テーブルの簡単なCRUDを集めたDAO
    ''' </summary>
    ''' <remarks>実装クラス</remarks>
    Public Class TShisakuFBuhinDaoImpl : Inherits EBomDaoEachTable(Of TShisakuFBuhinVo)
        Implements TShisakuFBuhinDao

        ''' <summary>
        ''' PrimaryKey を設定する
        ''' </summary>
        ''' <param name="table">テーブルに対応するVOのインスタンス</param>
        ''' <remarks></remarks>
        Protected Overrides Sub SettingPkField(ByVal table As PkTable(Of Vo.TShisakuFBuhinVo))
            Dim vo As New TShisakuFBuhinVo
            table.IsA(vo) _
            .PkField(vo.ShisakuEventCode) _
            .PkField(vo.ShisakuBukaCode) _
            .PkField(vo.ShisakuBlockNo) _
            .PkField(vo.ShisakuBlockNoKaiteiNo) _
            .PkField(vo.InstlHinban) _
            .PkField(vo.InstlHinbanKbn) _
            .PkField(vo.FBuhinNo) _
            .PkField(vo.FBuhinNoKbn)
        End Sub

        ''' <summary>
        ''' テーブル値の検索結果を返す
        ''' </summary>
        ''' <param name="shisakuEventCode">試作イベントコード</param>
        ''' <param name="shisakuBukaCode">試作部課コード</param>
        ''' <param name="shisakuBlockNo">試作ブロック№</param>
        ''' <param name="shisakuBlockNoKaiteiNo">試作ブロック№改訂№</param>
        ''' <param name="instlHinban">INSTL品番</param>
        ''' <param name="instlHinbanKbn">INSTL品番区分</param>
        ''' <param name="fBuhinNo">Ｆ品番</param>
        ''' <param name="fBuhinNoKbn">Ｆ品番区分</param>
        ''' <returns>該当レコード</returns>
        ''' <remarks></remarks>      
        Public Function FindByPk1(ByVal shisakuEventCode As String, _
                                  ByVal shisakuBukaCode As String, _
                                  ByVal shisakuBlockNo As String, _
                                  ByVal shisakuBlockNoKaiteiNo As String, _
                                  ByVal instlHinban As String, _
                                  ByVal instlHinbanKbn As String, _
                                  ByVal fBuhinNo As String, _
                                  ByVal fBuhinNoKbn As String) As Vo.TShisakuFBuhinVo Implements TShisakuFBuhinDao.FindByPk
            Return FindByPkMain(shisakuEventCode, _
                                shisakuBukaCode, _
                                shisakuBlockNo, _
                                shisakuBlockNoKaiteiNo, _
                                instlHinban, _
                                instlHinbanKbn, _
                                fBuhinNo, _
                                fBuhinNoKbn)
        End Function

        ''' <summary>
        ''' 該当レコードを削除する
        ''' </summary>
        ''' <param name="shisakuEventCode">試作イベントコード</param>
        ''' <param name="shisakuBukaCode">試作部課コード</param>
        ''' <param name="shisakuBlockNo">試作ブロック№</param>
        ''' <param name="shisakuBlockNoKaiteiNo">試作ブロック№改訂№</param>
        ''' <param name="instlHinban">INSTL品番</param>
        ''' <param name="instlHinbanKbn">INSTL品番区分</param>
        ''' <param name="fBuhinNo">Ｆ品番</param>
        ''' <param name="fBuhinNoKbn">Ｆ品番区分</param>
        ''' <returns>削除件数</returns>
        ''' <remarks></remarks>
        Public Function DeleteByPk1(ByVal shisakuEventCode As String, _
                                    ByVal shisakuBukaCode As String, _
                                    ByVal shisakuBlockNo As String, _
                                    ByVal shisakuBlockNoKaiteiNo As String, _
                                    ByVal instlHinban As String, _
                                    ByVal instlHinbanKbn As String, _
                                    ByVal fBuhinNo As String, _
                                    ByVal fBuhinNoKbn As String) As Integer Implements TShisakuFBuhinDao.DeleteByPk
            Return DeleteByPkMain(shisakuEventCode, _
                                  shisakuBukaCode, _
                                  shisakuBlockNo, _
                                  shisakuBlockNoKaiteiNo, _
                                  instlHinban, _
                                  instlHinbanKbn, _
                                  fBuhinNo, _
                                  fBuhinNoKbn)
        End Function
    End Class
End Namespace

