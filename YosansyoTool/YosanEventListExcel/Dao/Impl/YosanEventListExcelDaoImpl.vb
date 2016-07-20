Imports System.Text
Imports ShisakuCommon
Imports ShisakuCommon.Db.EBom
Imports ShisakuCommon.Db.EBom.Vo

Namespace YosanEventListExcel.Dao.Impl

    Public Class YosanEventListExcelDaoImpl : Implements YosanEventListExcelDao

        ''' <summary>
        ''' 予算書イベント別年月別財務実績情報
        ''' </summary>
        ''' <param name="yosanZaimuJisekiYyyyMm">財務実績計上年月</param>
        ''' <returns>予算書イベント別年月別財務実績情報</returns>
        ''' <remarks></remarks>
        Public Function FindYosanZaimuJiseki(ByVal yosanZaimuJisekiYyyyMm As String) As List(Of TYosanZaimuJisekiVo) Implements YosanEventListExcelDao.FindYosanZaimuJiseki

            Dim sql As String = _
               "SELECT " _
               & "   * " _
               & "   FROM " & MBOM_DB_NAME & ".dbo.T_YOSAN_ZAIMU_JISEKI " _
                & "   WHERE" _
                & "   YOSAN_ZAIMU_JISEKI_YYYY_MM =@YosanZaimuJisekiYyyyMm"

            Dim db As New EBomDbClient
            Dim param As New TYosanZaimuJisekiVo
            param.YosanZaimuJisekiYyyyMm = yosanZaimuJisekiYyyyMm

            Return db.QueryForList(Of TYosanZaimuJisekiVo)(sql, param)

        End Function

        ''' <summary>
        ''' 予算書部品編集情報
        ''' </summary>
        ''' <param name="buhinNo">部品番号</param>
        ''' <remarks></remarks>
        Public Function FindYosanBuhinEdit(ByVal buhinNo As String) As List(Of TYosanBuhinEditVo) Implements YosanEventListExcelDao.FindYosanBuhinEdit

            Dim sql As String = _
               "SELECT " _
               & "   * " _
               & "   FROM " & MBOM_DB_NAME & ".dbo.T_YOSAN_BUHIN_EDIT BE" _
               & "   INNER JOIN " & MBOM_DB_NAME & ".dbo.T_YOSAN_EVENT E" _
               & "   ON BE.YOSAN_EVENT_CODE = E.YOSAN_EVENT_CODE" _
               & "   AND E.YOSAN_STATUS = '00'" _
               & "   WHERE" _
               & "   BE.YOSAN_BUHIN_NO =@YosanBuhinNo"

            Dim db As New EBomDbClient
            Dim param As New TYosanBuhinEditVo
            param.YosanBuhinNo = buhinNo

            Return db.QueryForList(Of TYosanBuhinEditVo)(sql, param)

        End Function

        ''' <summary>
        ''' 予算書イベント情報取得
        ''' </summary>
        ''' <param name="yosanEventCode">予算書イベントコード</param>
        ''' <returns>予算書イベント情報</returns>
        ''' <remarks></remarks>
        Public Function FindYosanEvent(ByVal yosanEventCode As String) As TYosanEventVo Implements YosanEventListExcelDao.FindYosanEvent

            Dim sql As String = _
            " SELECT * " _
            & " FROM " & MBOM_DB_NAME & ".dbo.T_YOSAN_EVENT " _
            & " WHERE YOSAN_EVENT_CODE = '" & yosanEventCode & "'"

            Dim db As New EBomDbClient
            Return db.QueryForObject(Of TYosanEventVo)(sql)

        End Function

        ''' <summary>
        ''' 予算書部品編集情報取得
        ''' </summary>
        ''' <param name="yosanEventCode">予算書イベントコード</param>
        ''' <param name="yosanUnitKbn">ユニット区分</param>
        ''' <returns>予算書部品編集情報</returns>
        ''' <remarks></remarks>
        Public Function FindYosanBuhinEdit(ByVal yosanEventCode As String, ByVal yosanUnitKbn As String) As List(Of TYosanBuhinEditVo) Implements YosanEventListExcelDao.FindYosanBuhinEdit
            Dim sql As New System.Text.StringBuilder()
            With sql
                .Remove(0, .Length)
                .AppendLine("SELECT * ")
                .AppendLine("FROM " & MBOM_DB_NAME & ".dbo.T_YOSAN_BUHIN_EDIT ")
                .AppendLine("WHERE YOSAN_EVENT_CODE = @YosanEventCode ")
                If StringUtil.IsNotEmpty(yosanUnitKbn) Then
                    .AppendLine("AND BUHINHYO_NAME = @BuhinhyoName ")
                End If
                .AppendLine("ORDER BY YOSAN_BLOCK_NO, BUHIN_NO_HYOUJI_JUN")
            End With

            Dim db As New EBomDbClient
            Dim param As New TYosanBuhinEditVo
            param.YosanEventCode = yosanEventCode

            If StringUtil.IsNotEmpty(yosanUnitKbn) Then
                param.BuhinhyoName = yosanUnitKbn
            End If

            Return db.QueryForList(Of TYosanBuhinEditVo)(sql.ToString, param)
        End Function

        ''' <summary>
        ''' 予算部品編集員数情報を取得
        ''' </summary>
        ''' <param name="yosanEventCode">予算書イベントコード</param>
        ''' <param name="yosanUnitKbn">ユニット区分</param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Function FindYosanBuhinEditInsu(ByVal yosanEventCode As String, ByVal yosanUnitKbn As String) As List(Of TYosanBuhinEditInsuVo) Implements YosanEventListExcelDao.FindYosanBuhinEditInsu

            Dim sql As New System.Text.StringBuilder()
            With sql
                .Remove(0, .Length)
                .AppendLine("SELECT * ")
                .AppendLine("FROM " & MBOM_DB_NAME & ".dbo.T_YOSAN_BUHIN_EDIT_INSU ")
                .AppendLine("WHERE YOSAN_EVENT_CODE = @YosanEventCode ")
                If StringUtil.IsNotEmpty(yosanUnitKbn) Then
                    .AppendLine("AND BUHINHYO_NAME = @BuhinhyoName ")
                End If
                .AppendLine("ORDER BY YOSAN_BLOCK_NO, BUHIN_NO_HYOUJI_JUN")
            End With

            Dim db As New EBomDbClient
            Dim param As New TYosanBuhinEditInsuVo
            param.YosanEventCode = yosanEventCode

            If StringUtil.IsNotEmpty(yosanUnitKbn) Then
                param.BuhinhyoName = yosanUnitKbn
            End If

            Return db.QueryForList(Of TYosanBuhinEditInsuVo)(sql.ToString, param)
        End Function

        ''' <summary>
        ''' 予算部品編集員数情報を取得
        ''' </summary>
        ''' <param name="yosanEventCode">予算書イベントコード</param>
        ''' <param name="yosanUnitKbn">ユニット区分</param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Function FindYosanBuhinEditPattern(ByVal yosanEventCode As String, ByVal yosanUnitKbn As String) As List(Of TYosanBuhinEditPatternVo) Implements YosanEventListExcelDao.FindYosanBuhinEditPattern
            Dim sql As New System.Text.StringBuilder()
            With sql
                .Remove(0, .Length)
                .AppendLine("SELECT * ")
                .AppendLine("FROM " & MBOM_DB_NAME & ".dbo.T_YOSAN_BUHIN_EDIT_PATTERN ")
                .AppendLine("WHERE YOSAN_EVENT_CODE = @YosanEventCode ")
                If StringUtil.IsNotEmpty(yosanUnitKbn) Then
                    .AppendLine("AND BUHINHYO_NAME = @BuhinhyoName ")
                End If
                .AppendLine("ORDER BY PATTERN_HYOUJI_JUN")
            End With

            Dim db As New EBomDbClient
            Dim param As New TYosanBuhinEditPatternVo
            param.YosanEventCode = yosanEventCode

            If StringUtil.IsNotEmpty(yosanUnitKbn) Then
                param.BuhinhyoName = yosanUnitKbn
            End If

            Return db.QueryForList(Of TYosanBuhinEditPatternVo)(sql.ToString, param)
        End Function

    End Class

End Namespace
