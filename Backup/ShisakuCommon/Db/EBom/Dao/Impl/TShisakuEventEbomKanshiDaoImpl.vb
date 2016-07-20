Imports ShisakuCommon.Db.EBom.Vo

Namespace Db.EBom.Dao.Impl

    ''' <summary>
    ''' 試作イベントベース車情報(EBOM設変)テーブルの簡単なCRUDを集めたDAO
    ''' </summary>
    ''' <remarks>実装クラス</remarks>
    Public Class TShisakuEventEbomKanshiDaoImpl : Inherits EBomDaoEachTable(Of TShisakuEventEbomKanshiVo)
        Implements TShisakuEventEbomKanshiDao

        ''' <summary>
        ''' PrimaryKey を設定する
        ''' </summary>
        ''' <param name="table">テーブルに対応するVOのインスタンス</param>
        ''' <remarks></remarks>
        Protected Overrides Sub SettingPkField(ByVal table As PkTable(Of TShisakuEventEbomKanshiVo))
            Dim vo As New TShisakuEventEbomKanshiVo
            table.IsA(vo).PkField(vo.ShisakuEventCode) _
                         .PkField(vo.HyojijunNo)
        End Sub
        ''' <summary>
        ''' テーブル値の検索結果を返す
        ''' </summary>
        ''' <param name="shisakuEventCode">試作イベントコード</param>
        ''' <param name="hyoJiJun_No">表示順</param>
        ''' <returns>該当レコード</returns>
        ''' <remarks></remarks>
        Public Function FindByPk(ByVal shisakuEventCode As String, ByVal hyoJiJun_No As Integer) As TShisakuEventEbomKanshiVo Implements TShisakuEventEbomKanshiDao.FindByPk
            Return FindByPkMain(shisakuEventCode, _
                                hyoJiJun_No)
        End Function
        ''' <summary>
        ''' 該当レコードを削除する
        ''' </summary>
        ''' <param name="shisakuEventCode">試作イベントコード</param>
        ''' <param name="hyoJiJun_No">表示順</param>
        ''' <returns>削除件数</returns>
        ''' <remarks></remarks>
        Public Function DeleteByPk(ByVal shisakuEventCode As String, ByVal hyoJiJun_No As Integer) As Integer Implements TShisakuEventEbomKanshiDao.DeleteByPk
            Return DeleteByPkMain(shisakuEventCode, _
                                  hyoJiJun_No)
        End Function



        Public Function FindByEvent(ByVal eventCode As String) As List(Of TShisakuEventEbomKanshiVo) Implements TShisakuEventEbomKanshiDao.FindByEvent
            Dim sql As New System.Text.StringBuilder
            With sql
                .AppendLine("SELECT ")
                .AppendLine(" EB.SHISAKU_EVENT_CODE")
                .AppendLine(", EB.HYOJIJUN_NO")
                .AppendLine(", EB.SHISAKU_SYUBETU")
                .AppendLine(", EB.SHISAKU_GOUSYA")
                .AppendLine(", EK.SEKKEI_TENKAI_KBN")
                .AppendLine(", EK.BASE_KAIHATSU_FUGO")
                .AppendLine(", EK.BASE_SHIYOUJYOUHOU_NO")
                .AppendLine(", EK.BASE_APPLIED_NO")
                .AppendLine(", EK.EBOM_KANSHI_KATASHIKI")
                .AppendLine(", EK.BASE_KATASHIKI_SCD_7")
                .AppendLine(", EK.BASE_SOBI_KAITEI_NO")
                .AppendLine(", EK.BASE_SHIMUKE")
                .AppendLine(", EK.BASE_OP")
                .AppendLine(", EK.BASE_GAISOUSYOKU")
                .AppendLine(", EK.BASE_NAISOUSYOKU")
                .AppendLine(", EK.SHISAKU_BASE_EVENT_CODE")
                .AppendLine(", EK.SHISAKU_BASE_GOUSYA")
                .AppendLine(", EK.CREATED_USER_ID")
                .AppendLine(", EK.CREATED_DATE")
                .AppendLine(", EK.CREATED_TIME")
                .AppendLine(", EK.UPDATED_USER_ID")
                .AppendLine(", EK.UPDATED_DATE")
                .AppendLine(", EK.UPDATED_TIME")
                .AppendLine(" FROM " & MBOM_DB_NAME & ".dbo.T_SHISAKU_EVENT_BASE EB")
                .AppendLine(" LEFT JOIN " & MBOM_DB_NAME & ".dbo.T_SHISAKU_EVENT_EBOM_KANSHI EK")
                .AppendLine(" ON EB.SHISAKU_EVENT_CODE = EK.SHISAKU_EVENT_CODE")
                .AppendLine(" AND EB.HYOJIJUN_NO = EK.HYOJIJUN_NO")
                .AppendLine(" WHERE EB.SHISAKU_EVENT_CODE = @ShisakuEventCode")
            End With

            Dim db As New EBomDbClient
            Dim param As New TShisakuEventEbomKanshiVo
            param.ShisakuEventCode = eventCode
            Return db.QueryForList(Of TShisakuEventEbomKanshiVo)(sql.ToString, param)
        End Function
    End Class
End Namespace