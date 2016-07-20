Imports System.Text
Imports ShisakuCommon
Imports ShisakuCommon.Db.EBom
Imports ShisakuCommon.Db.EBom.Vo

Namespace YosanEventList.Dao.Impl

    Public Class YosanEventListDaoImpl : Implements YosanEventListDao

        ''' <summary>
        ''' 予算書イベント別年月別財務実績情報
        ''' </summary>
        ''' <param name="yosanZaimuJisekiYyyyMm">財務実績計上年月</param>
        ''' <returns>予算書イベント別年月別財務実績情報</returns>
        ''' <remarks></remarks>
        Public Function FindYosanZaimuJiseki(ByVal yosanZaimuJisekiYyyyMm As String) As List(Of TYosanZaimuJisekiVo) Implements YosanEventListDao.FindYosanZaimuJiseki

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
        Public Function FindYosanBuhinEdit(ByVal buhinNo As String) As List(Of TYosanBuhinEditVo) Implements YosanEventListDao.FindYosanBuhinEdit

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

    End Class

End Namespace
