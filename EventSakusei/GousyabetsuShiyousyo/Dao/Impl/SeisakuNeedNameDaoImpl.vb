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


Namespace XVLView.Dao.Impl

    Public Class SeisakuNeedNameDaoImpl


        ''' <summary>
        ''' イベントコードからOP_NAMEを取得.
        ''' </summary>
        ''' <param name="aEventCode"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Function GetOPName(ByVal aEventCode As String, ByVal aKaiteiNo As String, ByVal isWB As Boolean) As List(Of Vo.SeisakuOPVo)
            Dim sb As New StringBuilder
            With sb
                .Remove(0, .Length)
                .AppendLine(" SELECT ")
                .AppendLine(" DISTINCT OP_NAME ")
                .AppendLine(" FROM ")
                .AppendLine("  " & MBOM_DB_NAME & ".dbo.T_SEISAKU_ICHIRAN_OPKOUMOKU  OPKOUMOKU")
                .AppendLine(" LEFT JOIN " & MBOM_DB_NAME & ".dbo.T_SEISAKU_HAKOU_HD  HAKOUHD")
                .AppendLine(" ON OPKOUMOKU.HAKOU_NO = HAKOUHD.HAKOU_NO ")
                .AppendLine(" AND OPKOUMOKU.KAITEI_NO = HAKOUHD.KAITEI_NO ")
                If isWB Then
                    .AppendLine(" AND OPKOUMOKU.SYUBETU = 'W' ")
                Else
                    .AppendLine(" AND OPKOUMOKU.SYUBETU = 'C' ")
                End If
                .AppendLine(" WHERE ")
                .AppendLine(" HAKOUHD.HAKOU_NO = @SeisakuEventCode ")
                .AppendLine(" AND HAKOUHD.KAITEI_NO = @KaiteiNo ")
            End With

            Dim db As New EBomDbClient
            Dim iArg As New Vo.SeisakuOPVo
            iArg.SeisakuEventCode = aEventCode
            iArg.KaiteiNo = aKaiteiNo
            Return db.QueryForList(Of Vo.SeisakuOPVo)(sb.ToString, iArg)

        End Function

        ''' <summary>
        ''' イベントコードからSHO_KBN_NAMEを取得.
        ''' </summary>
        ''' <param name="aEventCode"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Function GetShokbnName(ByVal aEventCode As String, ByVal aKaiteiNo As String, ByVal isWB As Boolean) As List(Of Vo.SeisakuoOikomiVo)
            Dim sb As New StringBuilder
            With sb
                .Remove(0, .Length)
                .AppendLine(" SELECT ")
                ''↓↓2014/09/10 Ⅰ.8.号車別仕様書 作成機能 酒井 ADD BEGIN
                .AppendLine(" DISTINCT CHU_KBN_NAME,SHO_KBN_NAME  ")
                ''↑↑2014/09/10 Ⅰ.8.号車別仕様書 作成機能 酒井 ADD END
                .AppendLine(" FROM ")
                If isWB Then
                    .AppendLine("  " & MBOM_DB_NAME & ".dbo.T_SEISAKU_WB_SOUBI_SHIYOU  ORIKOMI")
                Else
                    .AppendLine("  " & MBOM_DB_NAME & ".dbo.T_SEISAKU_TOKUBETU_ORIKOMI  ORIKOMI")
                End If
                .AppendLine(" LEFT JOIN " & MBOM_DB_NAME & ".dbo.T_SEISAKU_HAKOU_HD  HAKOUHD")
                .AppendLine(" ON ORIKOMI.HAKOU_NO = HAKOUHD.HAKOU_NO ")
                .AppendLine(" AND ORIKOMI.KAITEI_NO = HAKOUHD.KAITEI_NO ")
                .AppendLine(" WHERE ")
                .AppendLine(" HAKOUHD.HAKOU_NO = @SeisakuEventCode ")
                .AppendLine(" AND HAKOUHD.KAITEI_NO = @KaiteiNo ")
            End With

            Dim db As New EBomDbClient
            Dim iArg As New Vo.SeisakuoOikomiVo
            iArg.SeisakuEventCode = aEventCode
            iArg.KaiteiNo = aKaiteiNo
            Return db.QueryForList(Of Vo.SeisakuoOikomiVo)(sb.ToString, iArg)

        End Function
        ''' <summary>
        ''' イベントコードからCHU_KBN_NAME、SHO_KBN_NAMEを取得.
        ''' </summary>
        ''' <param name="aEventCode"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Function GetChbKbn(ByVal aEventCode As String) As List(Of Vo.SeisakuoOikomiVo)

            Dim sb As New StringBuilder
            With sb
                .Remove(0, .Length)
                .AppendLine(" SELECT ")
                .AppendLine(" DISTINCT ORIKOMI.SHO_KBN_NAME  ")
                .AppendLine(" ,ORIKOMI.CHU_KBN_NAME ")
                .AppendLine(" ,ORIKOMI.TOKUBETU_ORIKOMI_HYOJIJUN_NO ")
                .AppendLine(" FROM ")
                .AppendLine("  " & MBOM_DB_NAME & ".dbo.T_SEISAKU_TOKUBETU_ORIKOMI  ORIKOMI ")
                .AppendLine(" WHERE ")
                .AppendLine(" ORIKOMI.HAKOU_NO = @SeisakuEventCode ")
                .AppendLine(" ORDER BY ORIKOMI.CHU_KBN_NAME ")
                .AppendLine(" ,ORIKOMI.TOKUBETU_ORIKOMI_HYOJIJUN_NO ")
            End With

            Dim db As New EBomDbClient
            Dim iArg As New Vo.SeisakuoOikomiVo
            iArg.SeisakuEventCode = aEventCode

            Return db.QueryForList(Of Vo.SeisakuoOikomiVo)(sb.ToString, iArg)

        End Function
        ''' <summary>
        ''' イベントコードからEventNameを取得.
        ''' </summary>
        ''' <param name="aEventCode"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Function GetEventName(ByVal aEventCode As String, ByVal aKaiteiNo As String) As List(Of Vo.SeiSakuGouSya)
            Dim sb As New StringBuilder
            With sb
                .Remove(0, .Length)
                .AppendLine(" SELECT ")
                .AppendLine(" SEISAKU_EVENT_NAME   ")
                .AppendLine(" FROM ")
                .AppendLine("  " & MBOM_DB_NAME & ".dbo.T_SEISAKU_HAKOU_HD  HAKOUHD ")
                .AppendLine(" WHERE ")
                .AppendLine(" HAKOUHD.HAKOU_NO = @SeisakuEventCode ")
                .AppendLine(" AND HAKOUHD.KAITEI_NO = @KaiteiNo ")
                .AppendLine(" ORDER BY HAKOUHD.KAITEI_NO DESC")
            End With

            Dim db As New EBomDbClient
            Dim iArg As New Vo.SeiSakuGouSya
            iArg.SeisakuEventCode = aEventCode
            iArg.KaiteiNo = aKaiteiNo

            Return db.QueryForList(Of Vo.SeiSakuGouSya)(sb.ToString, iArg)

        End Function

        Public Function GetEvent(ByVal aEventCode As String, ByVal aKaiteiNo As String) As List(Of Vo.SeiSakuGouSya)
            Dim sb As New StringBuilder
            With sb
                .Remove(0, .Length)
                .AppendLine(" SELECT ")
                .AppendLine(" *   ")
                .AppendLine(" FROM ")
                .AppendLine("  " & MBOM_DB_NAME & ".dbo.T_SEISAKU_HAKOU_HD  HAKOUHD ")
                .AppendLine(" WHERE ")
                .AppendLine(" HAKOUHD.HAKOU_NO = @SeisakuEventCode ")
                .AppendLine(" AND HAKOUHD.KAITEI_NO = @KaiteiNo ")
                .AppendLine(" ORDER BY HAKOUHD.KAITEI_NO DESC")
            End With

            Dim db As New EBomDbClient
            Dim iArg As New Vo.SeiSakuGouSya
            iArg.SeisakuEventCode = aEventCode
            iArg.KaiteiNo = aKaiteiNo

            Return db.QueryForList(Of Vo.SeiSakuGouSya)(sb.ToString, iArg)

        End Function

        ''' <summary>
        ''' テーブル「T_SEISAKU_ICHIRAN_KANSEI」各データを取得.
        ''' </summary>
        ''' <param name="aEventCode"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Function GetIchirankanseiData(ByVal aEventCode As String, ByVal aKaiteiNo As String, ByVal aGousya As String) As List(Of Vo.SeiSakuGouSya)
            Dim sb As New StringBuilder
            With sb
                .Remove(0, .Length)
                .AppendLine(" SELECT ")
                .AppendLine(" *   ")
                .AppendLine(" FROM ")
                .AppendLine("  " & MBOM_DB_NAME & ".dbo.T_SEISAKU_ICHIRAN_KANSEI  KANSEI ")
                .AppendLine(" WHERE ")
                .AppendLine(" KANSEI.HAKOU_NO = @SeisakuEventCode AND ")
                .AppendLine(" KANSEI.GOUSYA = @Gousya  ")
                .AppendLine(" AND KANSEI.KAITEI_NO = @KaiteiNo  ")
                .AppendLine(" ORDER BY KANSEI.KAITEI_NO DESC")
            End With

            Dim db As New EBomDbClient
            Dim iArg As New Vo.SeiSakuGouSya
            iArg.SeisakuEventCode = aEventCode
            iArg.Gousya = aGousya
            iArg.KaiteiNo = aKaiteiNo
            Return db.QueryForList(Of Vo.SeiSakuGouSya)(sb.ToString, iArg)

        End Function

        ''' <summary>
        '''  テーブル「T_SEISAKU_ICHIRAN_WB」各データを取得.
        ''' </summary>
        ''' <param name="aEventCode"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Function GetIchiranwbData(ByVal aEventCode As String, ByVal aKaiteiNo As String, ByVal aGousya As String) As List(Of Vo.SeiSakuGouSya)
            '↓↓2014/10/01 酒井 ADD BEGIN
            '        Public Function GetIchiranwbData(ByVal aEventCode As String, ByVal aGousya As String) As List(Of Vo.SeiSakuGouSya)
            '↑↑2014/10/01 酒井 ADD END
            Dim sb As New StringBuilder
            With sb
                .Remove(0, .Length)
                .AppendLine(" SELECT ")
                .AppendLine(" *   ")
                .AppendLine(" FROM ")
                .AppendLine("  " & MBOM_DB_NAME & ".dbo.T_SEISAKU_ICHIRAN_WB  WB ")
                .AppendLine(" WHERE ")
                .AppendLine(" WB.HAKOU_NO = @SeisakuEventCode AND ")
                .AppendLine(" WB.GOUSYA = @Gousya  ")
                '↓↓2014/10/01 酒井 ADD BEGIN
                .AppendLine(" AND WB.KAITEI_NO = @KaiteiNo  ")
                '↑↑2014/10/01 酒井 ADD END
                .AppendLine(" ORDER BY WB.KAITEI_NO DESC")
            End With

            Dim db As New EBomDbClient
            Dim iArg As New Vo.SeiSakuGouSya
            iArg.SeisakuEventCode = aEventCode
            iArg.Gousya = aGousya
            '↓↓2014/10/01 酒井 ADD BEGIN
            iArg.KaiteiNo = aKaiteiNo
            '↑↑2014/10/01 酒井 ADD END
            Return db.QueryForList(Of Vo.SeiSakuGouSya)(sb.ToString, iArg)

        End Function
        ''' <summary>
        '''  テーブル「T_SEISAKU_ICHIRAN_OPKOUMOKU」各データを取得.
        ''' </summary>
        ''' <param name="aEventCode"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Function GetIchiranopkoumokuData(ByVal aEventCode As String, ByVal aKaiteiNo As String, ByVal aGousya As String, ByVal isWB As Boolean) As List(Of Vo.SeisakuOPVo)
            '↓↓2014/10/01 酒井 ADD BEGIN
            '        Public Function GetIchiranopkoumokuData(ByVal aEventCode As String, ByVal aGousya As String) As List(Of Vo.SeisakuOPVo)
            '↑↑2014/10/01 酒井 ADD END
            Dim sb As New StringBuilder
            With sb
                .Remove(0, .Length)
                .AppendLine(" SELECT ")
                .AppendLine(" *   ")
                .AppendLine(" FROM ")
                .AppendLine("  " & MBOM_DB_NAME & ".dbo.T_SEISAKU_ICHIRAN_OPKOUMOKU  OPKOUMOKU ")
                .AppendLine(" WHERE ")
                .AppendLine(" OPKOUMOKU.HAKOU_NO = @SeisakuEventCode AND ")
                .AppendLine(" OPKOUMOKU.GOUSYA = @Gousya  ")
                '↓↓2014/10/01 酒井 ADD BEGIN
                .AppendLine(" AND OPKOUMOKU.KAITEI_NO = @KaiteiNo  ")
                '↑↑2014/10/01 酒井 ADD END
                If isWB Then
                    .AppendLine(" AND OPKOUMOKU.SYUBETU = 'W' ")
                Else
                    .AppendLine(" AND OPKOUMOKU.SYUBETU = 'C' ")
                End If
            End With

            Dim db As New EBomDbClient
            Dim iArg As New Vo.SeisakuOPVo
            iArg.SeisakuEventCode = aEventCode
            iArg.Gousya = aGousya
            '↓↓2014/10/01 酒井 ADD BEGIN
            iArg.KaiteiNo = aKaiteiNo
            '↑↑2014/10/01 酒井 ADD END
            Return db.QueryForList(Of Vo.SeisakuOPVo)(sb.ToString, iArg)

        End Function
        ''' <summary>
        '''  テーブル「T_SEISAKU_TOKUBETU_ORIKOMI」各データを取得.
        ''' </summary>
        ''' <param name="aEventCode"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Function GetTokubetuorikomiData(ByVal aEventCode As String, ByVal aKaiteiNo As String, ByVal aGousya As String, ByVal isWB As Boolean) As List(Of Vo.SeisakuoOikomiVo)
            '↓↓2014/10/01 酒井 ADD BEGIN
            '        Public Function GetTokubetuorikomiData(ByVal aEventCode As String, ByVal aGousya As String) As List(Of Vo.SeisakuoOikomiVo)
            '↑↑2014/10/01 酒井 ADD END
            Dim sb As New StringBuilder
            With sb
                .Remove(0, .Length)
                .AppendLine(" SELECT ")
                .AppendLine(" *   ")
                .AppendLine(" FROM ")
                If isWB Then
                    .AppendLine("  " & MBOM_DB_NAME & ".dbo.T_SEISAKU_WB_SOUBI_SHIYOU  ORIKOMI ")
                Else
                    .AppendLine("  " & MBOM_DB_NAME & ".dbo.T_SEISAKU_TOKUBETU_ORIKOMI  ORIKOMI ")
                End If
                .AppendLine(" WHERE ")
                .AppendLine(" ORIKOMI.HAKOU_NO = @SeisakuEventCode AND ")
                .AppendLine(" ORIKOMI.GOUSYA = @Gousya  ")
                '↓↓2014/10/01 酒井 ADD BEGIN
                .AppendLine(" AND ORIKOMI.KAITEI_NO = @KaiteiNo  ")
                '↑↑2014/10/01 酒井 ADD END
            End With

            Dim db As New EBomDbClient
            Dim iArg As New Vo.SeisakuoOikomiVo
            iArg.SeisakuEventCode = aEventCode
            iArg.Gousya = aGousya
            '↓↓2014/10/01 酒井 ADD BEGIN
            iArg.KaiteiNo = aKaiteiNo
            '↑↑2014/10/01 酒井 ADD END
            Return db.QueryForList(Of Vo.SeisakuoOikomiVo)(sb.ToString, iArg)

        End Function
    End Class

End Namespace


