Imports ShisakuCommon.Db
Imports ShisakuCommon.Db.EBom
Imports ShisakuCommon.Db.EBom.Vo.Helper
Imports ShisakuCommon.Db.Impl
Imports ShisakuCommon
Imports ShisakuCommon.Db.EBom.Vo
Imports ShisakuCommon.Db.EBom.Dao
Imports ShisakuCommon.Db.EBom.Dao.Impl
Imports EventSakusei.TehaichoSakusei.Dao
Imports EventSakusei.TehaichoSakusei.Vo
Imports EBom.Data
Imports EBom.Common
Imports System.Text


''↓↓2014/08/04 Ⅰ.8.号車別仕様書 作成機能_g) (TES)張 ADD BEGIN
Namespace XVLView.Dao.Impl

    Public Class GousyabetsuShiyousyoDaoImpl


        ''' <summary>
        ''' イベントコード、グループコードから号車を取得.
        ''' </summary>
        ''' <param name="aEventCode"></param>
        ''' <param name="aGroupCode"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Function GetGousya(ByVal aEventCode As String, ByVal aGroupCode As String) As List(Of Vo.GousyabetsuShiyousyoVo)

            Dim sb As New StringBuilder
            With sb
                .Remove(0, .Length)
                .AppendLine(" SELECT ")
                .AppendLine(" DISTINCT KANSEI.GOUSYA AS SEISAKU_GOUSYA ")
                .AppendLine(" FROM ")
                .AppendLine(MBOM_DB_NAME & ".dbo.T_SEISAKU_ICHIRAN_KANSEI KANSEI ")
                .AppendLine(" INNER JOIN " & MBOM_DB_NAME & ".dbo.T_SEISAKU_HAKOU_HD HAKOU ")
                .AppendLine(" ON ")
                .AppendLine(" HAKOU.HAKOU_NO = KANSEI.HAKOU_NO ")
                .AppendLine(" AND ")
                .AppendLine(" HAKOU.KAITEI_NO = KANSEI.KAITEI_NO ")
                ''↓↓2014/09/22 Ⅰ.8.号車別仕様書 作成機能 酒井 ADD BEGIN
                '.AppendLine(" INNER JOIN " & MBOM_DB_NAME & ".dbo.M_SEISAKU_EVENT EVENT ")
                '.AppendLine(" ON ")
                '.AppendLine(" EVENT.SHISAKU_EVENT_NAME = HAKOU.SEISAKU_EVENT_NAME ")
                ''↑↑2014/09/22 Ⅰ.8.号車別仕様書 作成機能 酒井 ADD END
                .AppendLine(" WHERE ")
                .AppendLine(" HAKOU.HAKOU_NO = @SeisakuEventCode ")
                If aGroupCode <> "" Then
                    .AppendLine(" AND KANSEI.SEISAKU_GROUP = @SeisakuGroup ")
                End If
                .AppendLine(" UNION ")
                .AppendLine(" SELECT ")
                .AppendLine(" DISTINCT WB.GOUSYA AS SEISAKU_GOUSYA ")
                .AppendLine(" FROM ")
                .AppendLine(MBOM_DB_NAME & ".dbo.T_SEISAKU_ICHIRAN_WB WB ")
                .AppendLine(" INNER JOIN " & MBOM_DB_NAME & ".dbo.T_SEISAKU_HAKOU_HD HAKOU ")
                .AppendLine(" ON ")
                .AppendLine(" HAKOU.HAKOU_NO = WB.HAKOU_NO ")
                .AppendLine(" AND ")
                .AppendLine(" HAKOU.KAITEI_NO = WB.KAITEI_NO ")
                ''↓↓2014/09/22 Ⅰ.8.号車別仕様書 作成機能 酒井 ADD BEGIN
                '.AppendLine(" INNER JOIN " & MBOM_DB_NAME & ".dbo.M_SEISAKU_EVENT EVENT ")
                '.AppendLine(" ON ")
                '.AppendLine(" EVENT.SHISAKU_EVENT_NAME = HAKOU.SEISAKU_EVENT_NAME ")
                ''↑↑2014/09/22 Ⅰ.8.号車別仕様書 作成機能 酒井 ADD END
                .AppendLine(" WHERE ")
                .AppendLine(" WB.HAKOU_NO = @SeisakuEventCode ")
                If aGroupCode <> "" Then
                    .AppendLine(" AND WB.SEISAKU_GROUP = @SeisakuGroup ")
                End If
                .AppendLine(" ORDER BY GOUSYA ")
            End With

            Dim db As New EBomDbClient
            Dim iArg As New Vo.GousyabetsuShiyousyoVo
            iArg.SeisakuEventCode = aEventCode
            iArg.SeisakuGroup = aGroupCode

            Return db.QueryForList(Of Vo.GousyabetsuShiyousyoVo)(sb.ToString, iArg)

        End Function

        ''' <summary>
        ''' 制作イベントを取得.
        ''' </summary>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Function GetSeisakuEvent() As List(Of Vo.GousyabetsuShiyousyoVo)

            Dim sb As New StringBuilder
            With sb
                .Remove(0, .Length)
                .AppendLine("SELECT ")
                .AppendLine("   HAKOU_NO AS SEISAKU_EVENT_CODE ")
                '↓↓2014/09/30 酒井 ADD BEGIN
                .AppendLine("  ,KAITEI_NO")
                '↑↑2014/09/30 酒井 ADD END
                .AppendLine("  ,SEISAKU_EVENT_NAME")
                '.AppendLine("  ,[HYOUJIJUN]")
                '.AppendLine("  ,[SEISAKU_GROUP]")
                '.AppendLine("  ,[SEISAKU_GOUSYA]")
                .AppendLine("FROM [" & MBOM_DB_NAME & "].[dbo].[T_SEISAKU_HAKOU_HD]")
                .AppendLine("ORDER BY HAKOU_NO")
            End With

            Dim db As New EBomDbClient
            Dim iArg As New Vo.GousyabetsuShiyousyoVo
            Return db.QueryForList(Of Vo.GousyabetsuShiyousyoVo)(sb.ToString, iArg)

        End Function

        ''' <summary>
        ''' イベントコードからグループを取得.
        ''' </summary>
        ''' <param name="aEventCode"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Function GetGroup(ByVal aEventCode As String) As List(Of Vo.GousyabetsuShiyousyoVo)

            Dim sb As New StringBuilder
            With sb
                .Remove(0, .Length)
                .AppendLine(" SELECT ")
                .AppendLine(" DISTINCT KANSEI.SEISAKU_GROUP AS SEISAKU_GROUP , ")
                .AppendLine(" HAKOU.HAKOU_NO AS SEISAKU_EVENT_CODE ")
                .AppendLine(" FROM ")
                .AppendLine(MBOM_DB_NAME & ".dbo.T_SEISAKU_ICHIRAN_KANSEI KANSEI ")
                .AppendLine(" INNER JOIN " & MBOM_DB_NAME & ".dbo.T_SEISAKU_HAKOU_HD HAKOU ")
                .AppendLine(" ON ")
                .AppendLine(" HAKOU.HAKOU_NO = KANSEI.HAKOU_NO ")
                .AppendLine(" AND ")
                .AppendLine(" HAKOU.KAITEI_NO = KANSEI.KAITEI_NO ")
                ''↓↓2014/09/22 Ⅰ.8.号車別仕様書 作成機能 酒井 ADD BEGIN
                '.AppendLine(" INNER JOIN " & MBOM_DB_NAME & ".dbo.M_SEISAKU_EVENT EVENT ")
                '.AppendLine(" ON ")
                '.AppendLine(" EVENT.SHISAKU_EVENT_NAME = HAKOU.SEISAKU_EVENT_NAME ")
                ''↑↑2014/09/22 Ⅰ.8.号車別仕様書 作成機能 酒井 ADD END
                .AppendLine(" WHERE ")
                .AppendLine(" KANSEI.HAKOU_NO = @SeisakuEventCode ")
                .AppendLine(" UNION ")
                .AppendLine(" SELECT ")
                .AppendLine(" DISTINCT WB.SEISAKU_GROUP AS SEISAKU_GROUP , ")
                .AppendLine(" WB.HAKOU_NO AS SEISAKU_EVENT_CODE ")
                .AppendLine(" FROM ")
                .AppendLine(MBOM_DB_NAME & ".dbo.T_SEISAKU_ICHIRAN_WB WB ")
                .AppendLine(" INNER JOIN " & MBOM_DB_NAME & ".dbo.T_SEISAKU_HAKOU_HD HAKOU ")
                .AppendLine(" ON ")
                .AppendLine(" HAKOU.HAKOU_NO = WB.HAKOU_NO ")
                .AppendLine(" AND ")
                .AppendLine(" HAKOU.KAITEI_NO = WB.KAITEI_NO ")
                ''↓↓2014/09/22 Ⅰ.8.号車別仕様書 作成機能 酒井 ADD BEGIN
                '.AppendLine(" INNER JOIN " & MBOM_DB_NAME & ".dbo.M_SEISAKU_EVENT EVENT ")
                '.AppendLine(" ON ")
                '.AppendLine(" EVENT.SHISAKU_EVENT_NAME = HAKOU.SEISAKU_EVENT_NAME ")
                ''↑↑2014/09/22 Ⅰ.8.号車別仕様書 作成機能 酒井 ADD END
                .AppendLine(" WHERE ")
                .AppendLine(" WB.HAKOU_NO = @SeisakuEventCode ")
                .AppendLine(" ORDER BY SEISAKU_GROUP ")
            End With

            Dim db As New EBomDbClient
            Dim iArg As New Vo.GousyabetsuShiyousyoVo
            iArg.SeisakuEventCode = aEventCode


            Return db.QueryForList(Of Vo.GousyabetsuShiyousyoVo)(sb.ToString, iArg)

        End Function

    End Class

End Namespace
