Imports System.Text
Imports ShisakuCommon
Imports ShisakuCommon.Db.EBom
Imports ShisakuCommon.Db.EBom.Vo

Namespace YosanshoEdit.Dao.Impl

    Public Class YosanshoExportDaoImpl : Implements YosanshoExportDao

        ''' <summary>
        ''' 予算書イベント情報取得
        ''' </summary>
        ''' <param name="yosanEventCode">予算書イベントコード</param>
        ''' <returns>予算書イベント情報</returns>
        ''' <remarks></remarks>
        Public Function FindYosanEvent(ByVal yosanEventCode As String) As TYosanEventVo Implements YosanshoExportDao.FindYosanEvent

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
        Public Function FindYosanBuhinEdit(ByVal yosanEventCode As String, ByVal yosanUnitKbn As String) As List(Of TYosanBuhinEditVo) Implements YosanshoExportDao.FindYosanBuhinEdit
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
        Public Function FindYosanBuhinEditInsu(ByVal yosanEventCode As String, ByVal yosanUnitKbn As String) As List(Of TYosanBuhinEditInsuVo) Implements YosanshoExportDao.FindYosanBuhinEditInsu

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
        Public Function FindYosanBuhinEditPattern(ByVal yosanEventCode As String, ByVal yosanUnitKbn As String) As List(Of TYosanBuhinEditPatternVo) Implements YosanshoExportDao.FindYosanBuhinEditPattern
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
