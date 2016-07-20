Imports ShisakuCommon.Db.EBom.Vo.Helper
Imports ShisakuCommon.Db.Impl
Imports ShisakuCommon.Db
Imports ShisakuCommon
Imports ShisakuCommon.Db.EBom
Imports ShisakuCommon.Db.EBom.Vo
Imports ShisakuCommon.Db.EBom.Dao
Imports ShisakuCommon.Db.EBom.Dao.Impl
Imports EventSakusei.EventEdit.Vo

Namespace ExportShisakuEventInfoExcel.Dao

    Public Class ExportShisakuEventInfoExcelDaoImpl : Inherits DaoEachFeatureImpl
        Implements ExportShisakuEventInfoExcelDao

        'イベント情報を取得'
        Public Function GetEvent(ByVal eventCode As String) _
                            As List(Of TShisakuEventVo) Implements ExportShisakuEventInfoExcelDao.GetEvent
            Dim sql As String = _
            " SELECT * " _
            & " FROM " & MBOM_DB_NAME & ".dbo.T_SHISAKU_EVENT EVENT WITH (NOLOCK, NOWAIT) " _
            & " WHERE  " _
            & "     SHISAKU_EVENT_CODE = @ShisakuEventCode " _
            & " ORDER BY  " _
            & "     SHISAKU_EVENT_CODE"

            Dim db As New EBomDbClient
            Dim param As New TShisakuEventVo
            param.ShisakuEventCode = eventCode

            Return db.QueryForList(Of TShisakuEventVo)(sql, param)
        End Function

        '設計展開時のベース車情報を取得'
        Public Function GetBaseTenkai(ByVal eventCode As String) _
                            As List(Of TShisakuEventBaseVo) Implements ExportShisakuEventInfoExcelDao.GetBaseTenkai
            Dim sql As String = _
            " SELECT * " _
            & " FROM " & MBOM_DB_NAME & ".dbo.T_SHISAKU_EVENT_BASE BASE WITH (NOLOCK, NOWAIT) " _
            & " WHERE  " _
            & "     SHISAKU_EVENT_CODE = @ShisakuEventCode " _
            & " ORDER BY  " _
            & "     SHISAKU_EVENT_CODE," _
            & "     HYOJIJUN_NO"

            Dim db As New EBomDbClient
            Dim param As New TShisakuEventBaseVo
            param.ShisakuEventCode = eventCode

            Return db.QueryForList(Of TShisakuEventBaseVo)(sql, param)
        End Function

        'ベース車情報を取得'
        Public Function GetBase(ByVal eventCode As String) _
                            As List(Of TShisakuEventBaseSeisakuIchiranVo) Implements ExportShisakuEventInfoExcelDao.GetBase
            Dim sql As String = _
            " SELECT * " _
            & " FROM " & MBOM_DB_NAME & ".dbo.T_SHISAKU_EVENT_BASE_SEISAKU_ICHIRAN BASE WITH (NOLOCK, NOWAIT) " _
            & " WHERE  " _
            & "     SHISAKU_EVENT_CODE = @ShisakuEventCode " _
            & " ORDER BY  " _
            & "     SHISAKU_EVENT_CODE," _
            & "     HYOJIJUN_NO"

            Dim db As New EBomDbClient
            Dim param As New TShisakuEventBaseSeisakuIchiranVo
            param.ShisakuEventCode = eventCode

            Return db.QueryForList(Of TShisakuEventBaseSeisakuIchiranVo)(sql, param)
        End Function

        'ベース車情報（改訂）を取得'
        Public Function GetBaseKaitei(ByVal eventCode As String) _
                            As List(Of TShisakuEventBaseKaiteiVo) Implements ExportShisakuEventInfoExcelDao.GetBaseKaitei
            Dim sql As String = _
            " SELECT * " _
            & " FROM " & MBOM_DB_NAME & ".dbo.T_SHISAKU_EVENT_BASE_KAITEI BASE WITH (NOLOCK, NOWAIT) " _
            & " WHERE  " _
            & "     SHISAKU_EVENT_CODE = @ShisakuEventCode " _
            & " ORDER BY  " _
            & "     SHISAKU_EVENT_CODE," _
            & "     HYOJIJUN_NO"

            Dim db As New EBomDbClient
            Dim param As New TShisakuEventBaseKaiteiVo
            param.ShisakuEventCode = eventCode

            Return db.QueryForList(Of TShisakuEventBaseKaiteiVo)(sql, param)
        End Function


        '完成車情報を取得'
        Public Function GetKansei(ByVal eventCode As String) _
                            As List(Of TShisakuEventKanseiVo) Implements ExportShisakuEventInfoExcelDao.GetKansei
            Dim sql As String = _
            " SELECT * " _
            & " FROM " & MBOM_DB_NAME & ".dbo.T_SHISAKU_EVENT_KANSEI KANSEI WITH (NOLOCK, NOWAIT) " _
            & " WHERE  " _
            & "     SHISAKU_EVENT_CODE = @ShisakuEventCode " _
            & " ORDER BY  " _
            & "     SHISAKU_EVENT_CODE," _
            & "     HYOJIJUN_NO"

            Dim db As New EBomDbClient
            Dim param As New TShisakuEventKanseiVo
            param.ShisakuEventCode = eventCode

            Return db.QueryForList(Of TShisakuEventKanseiVo)(sql, param)
        End Function

        '完成車情報（改訂）を取得'
        Public Function GetKanseiKaitei(ByVal eventCode As String) _
                            As List(Of TShisakuEventKanseiKaiteiVo) Implements ExportShisakuEventInfoExcelDao.GetKanseiKaitei
            Dim sql As String = _
            " SELECT * " _
            & " FROM " & MBOM_DB_NAME & ".dbo.T_SHISAKU_EVENT_KANSEI_KAITEI KANSEI WITH (NOLOCK, NOWAIT) " _
            & " WHERE  " _
            & "     SHISAKU_EVENT_CODE = @ShisakuEventCode " _
            & " ORDER BY  " _
            & "     SHISAKU_EVENT_CODE," _
            & "     HYOJIJUN_NO"

            Dim db As New EBomDbClient
            Dim param As New TShisakuEventKanseiKaiteiVo
            param.ShisakuEventCode = eventCode

            Return db.QueryForList(Of TShisakuEventKanseiKaiteiVo)(sql, param)
        End Function

        '装備仕様情報を取得'
        Public Function GetSoubi(ByVal ShisakuEventCode As String, ByVal ShisakuSoubiKbn As String) _
                            As List(Of EventSoubiVo) Implements ExportShisakuEventInfoExcelDao.GetSoubi
            Dim sql As String = _
            " SELECT " _
            & "  E.SHISAKU_SYUBETU, " _
            & "  E.SHISAKU_GOUSYA, " _
            & "  SOUBI.SHISAKU_EVENT_CODE, " _
            & "  SOUBI.HYOJIJUN_NO, " _
            & "  SOUBI.SHISAKU_SOUBI_HYOUJI_NO, " _
            & "  SOUBI.SHISAKU_SOUBI_KBN, " _
            & "  SOUBI.SHISAKU_RETU_KOUMOKU_CODE,  " _
            & "  SOUBI.SHISAKU_TEKIYOU, " _
            & "  M.SHISAKU_RETU_KOUMOKU_NAME_DAI, " _
            & "  M.SHISAKU_RETU_KOUMOKU_NAME_CHU, " _
            & "  M.SHISAKU_RETU_KOUMOKU_NAME " _
            & " FROM " & MBOM_DB_NAME & ".dbo.T_SHISAKU_EVENT_SOUBI SOUBI WITH (NOLOCK, NOWAIT) " _
            & "    INNER JOIN " & MBOM_DB_NAME & ".dbo.M_SHISAKU_SOUBI M ON " _
            & "       SOUBI.SHISAKU_SOUBI_KBN = M.SHISAKU_SOUBI_KBN AND " _
            & "       SOUBI.SHISAKU_RETU_KOUMOKU_CODE = M.SHISAKU_RETU_KOUMOKU_CODE " _
            & "    INNER JOIN " & MBOM_DB_NAME & ".dbo.T_SHISAKU_EVENT_BASE E ON " _
            & "       SOUBI.SHISAKU_EVENT_CODE = E.SHISAKU_EVENT_CODE AND " _
            & "       Soubi.HYOJIJUN_NO = E.HYOJIJUN_NO " _
            & " WHERE  " _
            & "     SOUBI.SHISAKU_EVENT_CODE = @ShisakuEventCode AND" _
            & "     SOUBI.SHISAKU_SOUBI_KBN = @ShisakuSoubiKbn AND" _
            & "     SOUBI.HYOJIJUN_NO <> -1" _
            & " ORDER BY  " _
            & "     SOUBI.SHISAKU_EVENT_CODE," _
            & "     SOUBI.HYOJIJUN_NO," _
            & "     SOUBI.SHISAKU_SOUBI_HYOUJI_NO"

            Dim db As New EBomDbClient
            Dim param As New EventSoubiVo
            param.ShisakuEventCode = ShisakuEventCode
            param.ShisakuSoubiKbn = ShisakuSoubiKbn

            Return db.QueryForList(Of EventSoubiVo)(sql, param)
        End Function

        '装備仕様（改訂）を取得'
        Public Function GetSoubiKaitei(ByVal ShisakuEventCode As String, ByVal ShisakuSoubiKbn As String) _
                            As List(Of EventSoubiVo) Implements ExportShisakuEventInfoExcelDao.GetSoubiKaitei
            Dim sql As String = _
            " SELECT " _
            & "  E.SHISAKU_SYUBETU, " _
            & "  E.SHISAKU_GOUSYA, " _
            & "  SOUBI.SHISAKU_EVENT_CODE, " _
            & "  SOUBI.HYOJIJUN_NO, " _
            & "  SOUBI.SHISAKU_SOUBI_HYOUJI_NO, " _
            & "  SOUBI.SHISAKU_SOUBI_KBN, " _
            & "  SOUBI.SHISAKU_RETU_KOUMOKU_CODE,  " _
            & "  SOUBI.SHISAKU_TEKIYOU, " _
            & "  M.SHISAKU_RETU_KOUMOKU_NAME_DAI, " _
            & "  M.SHISAKU_RETU_KOUMOKU_NAME_CHU, " _
            & "  M.SHISAKU_RETU_KOUMOKU_NAME " _
            & " FROM " & MBOM_DB_NAME & ".dbo.T_SHISAKU_EVENT_SOUBI_KAITEI SOUBI WITH (NOLOCK, NOWAIT) " _
            & "    INNER JOIN " & MBOM_DB_NAME & ".dbo.M_SHISAKU_SOUBI M ON " _
            & "       SOUBI.SHISAKU_SOUBI_KBN = M.SHISAKU_SOUBI_KBN AND " _
            & "       SOUBI.SHISAKU_RETU_KOUMOKU_CODE = M.SHISAKU_RETU_KOUMOKU_CODE " _
            & "    INNER JOIN " & MBOM_DB_NAME & ".dbo.T_SHISAKU_EVENT_BASE_KAITEI E ON " _
            & "       SOUBI.SHISAKU_EVENT_CODE = E.SHISAKU_EVENT_CODE AND " _
            & "       SOUBI.HYOJIJUN_NO = E.HYOJIJUN_NO " _
            & " WHERE  " _
            & "     SOUBI.SHISAKU_EVENT_CODE = @ShisakuEventCode AND" _
            & "     SOUBI.SHISAKU_SOUBI_KBN = @ShisakuSoubiKbn AND" _
            & "     SOUBI.HYOJIJUN_NO <> -1" _
            & " ORDER BY  " _
            & "     SOUBI.SHISAKU_EVENT_CODE," _
            & "     SOUBI.HYOJIJUN_NO," _
            & "     SOUBI.SHISAKU_SOUBI_HYOUJI_NO"

            Dim db As New EBomDbClient
            Dim param As New EventSoubiVo
            param.ShisakuEventCode = ShisakuEventCode
            param.ShisakuSoubiKbn = ShisakuSoubiKbn

            Return db.QueryForList(Of EventSoubiVo)(sql, param)
        End Function

        '装備仕様情報を取得'
        Public Function GetSoubiTitle(ByVal ShisakuEventCode As String, ByVal ShisakuSoubiKbn As String) _
                            As List(Of EventSoubiVo) Implements ExportShisakuEventInfoExcelDao.GetSoubiTitle
            Dim sql As String = _
            " SELECT " _
            & "  SOUBI.SHISAKU_EVENT_CODE, " _
            & "  SOUBI.HYOJIJUN_NO, " _
            & "  SOUBI.SHISAKU_SOUBI_HYOUJI_NO, " _
            & "  SOUBI.SHISAKU_SOUBI_KBN, " _
            & "  SOUBI.SHISAKU_RETU_KOUMOKU_CODE,  " _
            & "  SOUBI.SHISAKU_TEKIYOU, " _
            & "  M.SHISAKU_RETU_KOUMOKU_NAME_DAI, " _
            & "  M.SHISAKU_RETU_KOUMOKU_NAME_CHU, " _
            & "  M.SHISAKU_RETU_KOUMOKU_NAME " _
            & " FROM " & MBOM_DB_NAME & ".dbo.T_SHISAKU_EVENT_SOUBI SOUBI WITH (NOLOCK, NOWAIT) " _
            & "    INNER JOIN " & MBOM_DB_NAME & ".dbo.M_SHISAKU_SOUBI M ON " _
            & "       SOUBI.SHISAKU_SOUBI_KBN = M.SHISAKU_SOUBI_KBN AND " _
            & "       SOUBI.SHISAKU_RETU_KOUMOKU_CODE = M.SHISAKU_RETU_KOUMOKU_CODE " _
            & " WHERE  " _
            & "     SOUBI.SHISAKU_EVENT_CODE = @ShisakuEventCode AND" _
            & "     SOUBI.SHISAKU_SOUBI_KBN = @ShisakuSoubiKbn AND" _
            & "     SOUBI.HYOJIJUN_NO = -1" _
            & " ORDER BY  " _
            & "     SOUBI.SHISAKU_EVENT_CODE," _
            & "     SOUBI.SHISAKU_SOUBI_HYOUJI_NO"

            Dim db As New EBomDbClient
            Dim param As New EventSoubiVo
            param.ShisakuEventCode = ShisakuEventCode
            param.ShisakuSoubiKbn = ShisakuSoubiKbn

            Return db.QueryForList(Of EventSoubiVo)(sql, param)
        End Function

        '装備仕様情報（改訂）を取得'
        Public Function GetSoubiTitleKaitei(ByVal ShisakuEventCode As String, ByVal ShisakuSoubiKbn As String) _
                            As List(Of EventSoubiVo) Implements ExportShisakuEventInfoExcelDao.GetSoubiTitleKaitei
            Dim sql As String = _
            " SELECT " _
            & "  SOUBI.SHISAKU_EVENT_CODE, " _
            & "  SOUBI.HYOJIJUN_NO, " _
            & "  SOUBI.SHISAKU_SOUBI_HYOUJI_NO, " _
            & "  SOUBI.SHISAKU_SOUBI_KBN, " _
            & "  SOUBI.SHISAKU_RETU_KOUMOKU_CODE,  " _
            & "  SOUBI.SHISAKU_TEKIYOU, " _
            & "  M.SHISAKU_RETU_KOUMOKU_NAME_DAI, " _
            & "  M.SHISAKU_RETU_KOUMOKU_NAME_CHU, " _
            & "  M.SHISAKU_RETU_KOUMOKU_NAME " _
            & " FROM " & MBOM_DB_NAME & ".dbo.T_SHISAKU_EVENT_SOUBI_KAITEI SOUBI WITH (NOLOCK, NOWAIT) " _
            & "    INNER JOIN " & MBOM_DB_NAME & ".dbo.M_SHISAKU_SOUBI M ON " _
            & "       SOUBI.SHISAKU_SOUBI_KBN = M.SHISAKU_SOUBI_KBN AND " _
            & "       SOUBI.SHISAKU_RETU_KOUMOKU_CODE = M.SHISAKU_RETU_KOUMOKU_CODE " _
            & " WHERE  " _
            & "     SOUBI.SHISAKU_EVENT_CODE = @ShisakuEventCode AND" _
            & "     SOUBI.SHISAKU_SOUBI_KBN = @ShisakuSoubiKbn AND" _
            & "     SOUBI.HYOJIJUN_NO = -1" _
            & " ORDER BY  " _
            & "     SOUBI.SHISAKU_EVENT_CODE," _
            & "     SOUBI.SHISAKU_SOUBI_HYOUJI_NO"

            Dim db As New EBomDbClient
            Dim param As New EventSoubiVo
            param.ShisakuEventCode = ShisakuEventCode
            param.ShisakuSoubiKbn = ShisakuSoubiKbn

            Return db.QueryForList(Of EventSoubiVo)(sql, param)
        End Function

    End Class
End Namespace