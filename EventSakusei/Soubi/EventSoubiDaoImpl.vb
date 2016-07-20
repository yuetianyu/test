Imports EventSakusei.EventEdit.Dao
Imports ShisakuCommon.Db.EBom.Vo.Helper
Imports ShisakuCommon.Db.Impl
Imports ShisakuCommon.Db.EBom
Imports ShisakuCommon.Db.EBom.Vo
Imports ShisakuCommon

Namespace Soubi
    Public Class EventSoubiDaoImpl : Inherits DaoEachFeatureImpl
        Implements EventSoubiDao

        Public Function FindWithTitleNameBy(ByVal shisakuEventCode As String, Optional ByVal shisakuSoubiKbn As String = Nothing) _
            As List(Of TShisakuEventSoubiNameVo) _
            Implements EventSoubiDao.FindWithTitleNameBy
            Return FindWithNameBy(shisakuEventCode, shisakuSoubiKbn, TShisakuEventSoubiVoHelper.HyojijunNo.TITLE)
        End Function
        Public Function FindWithNameBy(ByVal shisakuEventCode As String, Optional ByVal shisakuSoubiKbn As String = Nothing) As List(Of TShisakuEventSoubiNameVo) Implements EventSoubiDao.FindWithNameBy
            Return FindWithNameBy(shisakuEventCode, shisakuSoubiKbn, Nothing)
        End Function
        Private Function FindWithNameBy(ByVal shisakuEventCode As String, ByVal shisakuSoubiKbn As String, ByVal hyojijunNo As Integer?) _
            As List(Of TShisakuEventSoubiNameVo)
            Dim sql As String = "SELECT T.*, M.SHISAKU_RETU_KOUMOKU_NAME, SHISAKU_RETU_KOUMOKU_NAME_DAI, SHISAKU_RETU_KOUMOKU_NAME_CHU " _
                                & "FROM " & MBOM_DB_NAME & ".dbo.T_SHISAKU_EVENT_SOUBI T WITH (NOLOCK, NOWAIT) LEFT JOIN " & MBOM_DB_NAME & ".dbo.M_SHISAKU_SOUBI M " _
                                & "    ON T.SHISAKU_SOUBI_KBN = M.SHISAKU_SOUBI_KBN " _
                                & "    AND T.SHISAKU_RETU_KOUMOKU_CODE = M.SHISAKU_RETU_KOUMOKU_CODE " _
                                & "WHERE T.SHISAKU_EVENT_CODE = @ShisakuEventCode " _
                                & "    <if test='@HyojijunNo != null'> " _
                                & "        AND T.HYOJIJUN_NO = @HyojijunNo " _
                                & "    </if> " _
                                & "    <if test='@ShisakuSoubiKbn != null'> " _
                                & "        AND T.SHISAKU_SOUBI_KBN = @ShisakuSoubiKbn " _
                                & "    </if> " _
                                & "ORDER BY T.HYOJIJUN_NO, T.SHISAKU_SOUBI_HYOUJI_NO"
            Dim db As New EBomDbClient
            Dim param As New TShisakuEventSoubiVo
            param.ShisakuEventCode = shisakuEventCode
            param.HyojijunNo = hyojijunNo
            param.ShisakuSoubiKbn = shisakuSoubiKbn
            Return db.QueryForList(Of TShisakuEventSoubiNameVo)(sql, param)
        End Function


        Public Function FindWithTitleNameBySoubi(ByVal shisakuEventCode As String, Optional ByVal shisakuSoubiKbn As String = Nothing) _
            As List(Of TShisakuEventSoubiNameVo) _
            Implements EventSoubiDao.FindWithTitleNameBySoubi
            Return FindWithNameBySoubi(shisakuEventCode, shisakuSoubiKbn, TShisakuEventSoubiVoHelper.HyojijunNo.TITLE)
        End Function
        Public Function FindWithNameBySoubi(ByVal shisakuEventCode As String, Optional ByVal shisakuSoubiKbn As String = Nothing) As List(Of TShisakuEventSoubiNameVo) Implements EventSoubiDao.FindWithNameBySoubi
            Return FindWithNameBySoubi(shisakuEventCode, shisakuSoubiKbn, Nothing)
        End Function
        Private Function FindWithNameBySoubi(ByVal shisakuEventCode As String, ByVal shisakuSoubiKbn As String, ByVal hyojijunNo As Integer?) _
            As List(Of TShisakuEventSoubiNameVo)
            Dim sql As String = "SELECT T.*, M.SHISAKU_RETU_KOUMOKU_NAME, SHISAKU_RETU_KOUMOKU_NAME_DAI, SHISAKU_RETU_KOUMOKU_NAME_CHU " _
                                & "FROM " & MBOM_DB_NAME & ".dbo.T_SHISAKU_EVENT_SOUBI T WITH (NOLOCK, NOWAIT) LEFT JOIN " & MBOM_DB_NAME & ".dbo.M_SHISAKU_SOUBI M " _
                                & "    ON T.SHISAKU_SOUBI_KBN = M.SHISAKU_SOUBI_KBN " _
                                & "    AND T.SHISAKU_RETU_KOUMOKU_CODE = M.SHISAKU_RETU_KOUMOKU_CODE " _
                                & "WHERE T.SHISAKU_EVENT_CODE = @ShisakuEventCode " _
                                & "    <if test='@HyojijunNo != null'> " _
                                & "        AND T.HYOJIJUN_NO = @HyojijunNo " _
                                & "    </if> " _
                                & "    <if test='@ShisakuSoubiKbn = 1'> " _
                                & "        AND T.SHISAKU_SOUBI_KBN = @ShisakuSoubiKbn " _
                                & "    </if> " _
                                & "    <if test='@ShisakuSoubiKbn = 2'> " _
                                & "        AND (T.SHISAKU_SOUBI_KBN = @ShisakuSoubiKbn " _
                                & "              AND left(M.SHISAKU_RETU_KOUMOKU_NAME_DAI,1) != 'W' " _
                                & "              or  T.SHISAKU_SOUBI_KBN = @ShisakuSoubiKbn " _
                                & "              AND M.SHISAKU_RETU_KOUMOKU_NAME_DAI IS NULL)" _
                                & "    </if> " _
                                & "    <if test='@ShisakuSoubiKbn = 3'> " _
                                & "        AND T.SHISAKU_SOUBI_KBN = 2 " _
                                & "        AND left(M.SHISAKU_RETU_KOUMOKU_NAME_DAI,1) = 'W' " _
                                & "    </if> " _
                                & "ORDER BY T.HYOJIJUN_NO, T.SHISAKU_SOUBI_HYOUJI_NO"
            Dim db As New EBomDbClient
            Dim param As New TShisakuEventSoubiVo
            param.ShisakuEventCode = shisakuEventCode
            param.HyojijunNo = hyojijunNo
            param.ShisakuSoubiKbn = shisakuSoubiKbn
            Return db.QueryForList(Of TShisakuEventSoubiNameVo)(sql, param)
        End Function


    End Class
End Namespace