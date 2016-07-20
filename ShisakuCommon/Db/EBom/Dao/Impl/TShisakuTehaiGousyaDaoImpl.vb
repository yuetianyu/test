Imports ShisakuCommon.Db.EBom.Vo

Namespace Db.EBom.Dao.Impl

    ''' <summary>
    ''' 試作手配帳情報（号車情報）テーブルの簡単なCRUDを集めたDAO
    ''' </summary>
    ''' <remarks>実装クラス</remarks>
    Public Class TShisakuTehaiGousyaDaoImpl : Inherits EBomDaoEachTable(Of TShisakuTehaiGousyaVo)
        Implements TShisakuTehaiGousyaDao

        ''' <summary>
        ''' PrimaryKey を設定する
        ''' </summary>
        ''' <param name="table">テーブルに対応するVOのインスタンス</param>
        ''' <remarks></remarks>
        Protected Overrides Sub SettingPkField(ByVal table As PkTable(Of Vo.TShisakuTehaiGousyaVo))
            Dim vo As New TShisakuTehaiGousyaVo
            table.IsA(vo).PkField(vo.ShisakuEventCode) _
                         .PkField(vo.ShisakuListCode) _
                         .PkField(vo.ShisakuListCodeKaiteiNo) _
                         .PkField(vo.ShisakuBukaCode) _
                         .PkField(vo.BuhinNoHyoujiJun)
        End Sub

        ''' <summary>
        ''' テーブル値の検索結果を返す
        ''' </summary>
        ''' <param name="shisakuEventCode">試作イベントコード</param>
        ''' <param name="shisakuListCode">試作リストコード</param>
        ''' <param name="shisakuListCodeKaiteiNo">試作リストコード改訂№</param>
        ''' <param name="shisakuBukaCode">試作部課コード</param>
        ''' <param name="buhinNoHyoujiJun">部品番号表示順</param>
        ''' <returns>該当レコード</returns>
        ''' <remarks></remarks>
        Public Function FindByPk(ByVal shisakuEventCode As String, ByVal shisakuListCode As String, _
                          ByVal shisakuListCodeKaiteiNo As String, ByVal shisakuBukaCode As String, _
                          ByVal buhinNoHyoujiJun As String) As Vo.TShisakuTehaiGousyaVo Implements TShisakuTehaiGousyaDao.FindByPk
            Return FindByPkMain(shisakuEventCode, shisakuListCode, shisakuListCodeKaiteiNo, shisakuBukaCode, buhinNoHyoujiJun)
        End Function

        ''' <summary>
        ''' 該当レコードを削除する
        ''' </summary>
        ''' <param name="shisakuEventCode">試作イベントコード</param>
        ''' <param name="shisakuListCode">試作リストコード</param>
        ''' <param name="shisakuListCodeKaiteiNo">試作リストコード改訂№</param>
        ''' <param name="shisakuBukaCode">試作部課コード</param>
        ''' <param name="buhinNoHyoujiJun">部品番号表示順</param>
        ''' <returns>削除件数</returns>
        ''' <remarks></remarks>
        Public Function DeleteByPk(ByVal shisakuEventCode As String, ByVal shisakuListCode As String, _
                          ByVal shisakuListCodeKaiteiNo As String, ByVal shisakuBukaCode As String, _
                          ByVal buhinNoHyoujiJun As String) As Integer Implements TShisakuTehaiGousyaDao.DeleteByPk
            Return DeleteByPkMain(shisakuEventCode, shisakuListCode, shisakuListCodeKaiteiNo, shisakuBukaCode, buhinNoHyoujiJun)
        End Function

        ''' <summary>
        ''' 指定されたイベントコードとリストコードでテーブルが存在するかチェック.
        ''' </summary>
        ''' <param name="shisakuEventCode">検索するイベントコード</param>
        ''' <param name="shisakuListCode">検索するリストコード</param>
        ''' <returns>True:存在する False:存在しない</returns>
        ''' <remarks></remarks>
        Public Function ExistByShisakuGousya(ByVal shisakuEventCode As String, _
                                         ByVal shisakuListCode As String, _
                                         ByVal shisakuGousyaList As Dictionary(Of Integer, String)) As Boolean Implements TShisakuTehaiGousyaDao.ExistByShisakuGousya

            Dim lstGousha As New ArrayList
            For Each g As String In shisakuGousyaList.Values
                lstGousha.Add(String.Format("'{0}'", g))
            Next
            Dim sql As New System.Text.StringBuilder

            With sql
                .Remove(0, .Length)
                .AppendLine(" SELECT * ")
                .AppendLine(" FROM " & MBOM_DB_NAME & ".dbo.T_SHISAKU_TEHAI_GOUSYA ")
                .AppendLine(" WHERE ")
                .AppendFormat(" SHISAKU_EVENT_CODE ='{0}' ", shisakuEventCode)
                .AppendFormat(" AND SHISAKU_GOUSYA IN ({0}) ", String.Join(",", lstGousha.ToArray))
            End With

            Dim db As New EBomDbClient

            Dim result As List(Of TShisakuTehaiGousyaVo) = db.QueryForList(Of TShisakuTehaiGousyaVo)(sql.ToString)

            Return 0 < result.Count

        End Function

    End Class
End Namespace
