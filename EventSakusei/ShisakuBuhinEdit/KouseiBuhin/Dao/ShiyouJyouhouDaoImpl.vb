Imports ShisakuCommon.Db.EBom
Imports ShisakuCommon.Db.EBom.Vo
Imports ShisakuCommon
Imports System.Text
Imports EventSakusei.ShisakuBuhinEdit.KouseiBuhin.Dao.Vo
Imports ShisakuCommon.Db.Impl
Imports ShisakuCommon.Util.LabelValue

Namespace KouseiBuhin.Dao

    ''' <summary>
    ''' 仕様情報
    ''' </summary>
    ''' <remarks></remarks>
    Public Class ShiyouJyouhouDaoImpl : Inherits DaoEachFeatureImpl
        Implements ShiyouJyouhouDao

        ''' <summary>
        ''' 仕様情報№を取得
        ''' </summary>
        ''' <param name="KaihatsuFugo"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Function GetByShiyouJyouhouNoLabelValues(ByVal KaihatsuFugo As String) As List(Of LabelValueVo) Implements ShiyouJyouhouDao.GetByShiyouJyouhouNoLabelValues
            Dim sb As New StringBuilder
            With sb
                .Remove(0, .Length)
                .AppendLine("SELECT  ")
                .AppendLine("    T0030.SHIYOSHO_SEQNO AS LABEL, ")
                .AppendLine("    T0030.SHIYOSHO_SEQNO AS VALUE ")
                .AppendLine("FROM ")
                .AppendLine(RHACLIBF_DB_NAME & ".dbo.RHAC0030 AS T0030 ")
                .AppendLine("WHERE ")
                .AppendLine("    T0030.KAIHATSU_FUGO = '" & KaihatsuFugo & "' ")
                .AppendLine("AND T0030.SHONIN_DATE <> 0 ")
                .AppendLine("AND T0030.RYOSHI_KBN <> 'K' ")
                .AppendLine("ORDER BY ")
                .AppendLine("   T0030.SHIYOSHO_SEQNO DESC ")
            End With
            Dim db As New EBomDbClient
            Return db.QueryForList(Of LabelValueVo)(sb.ToString)
        End Function

        ''' <summary>
        ''' 仕様情報№の最新を取得
        ''' </summary>
        ''' <param name="KaihatsuFugo"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Function GetByNewShiyouJyouhouNo(ByVal KaihatsuFugo As String) As Rhac0030Vo Implements ShiyouJyouhouDao.GetByNewShiyouJyouhouNo
            Dim sb As New StringBuilder
            With sb
                .Remove(0, .Length)
                .AppendLine("SELECT  ")
                .AppendLine("    MAX(T0030.SHIYOSHO_SEQNO) AS SHIYOSHO_SEQNO ")
                .AppendLine("FROM ")
                .AppendLine(RHACLIBF_DB_NAME & ".dbo.RHAC0030 AS T0030 ")
                .AppendLine("WHERE ")
                .AppendLine("    T0030.KAIHATSU_FUGO = '" & KaihatsuFugo & "' ")
                .AppendLine("AND T0030.SHONIN_DATE <> 0 ")
                .AppendLine("AND T0030.RYOSHI_KBN <> 'K' ")
            End With
            Dim db As New EBomDbClient
            Return db.QueryForObject(Of Rhac0030Vo)(sb.ToString)
        End Function

        ''' <summary>
        ''' 車型を取得
        ''' </summary>
        ''' <param name="KaihatsuFugo"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Function GetBySyagataLabelValues(ByVal KaihatsuFugo As String, _
                                                ByVal ShiyouJyouhouNo As String, _
                                                ByVal syagata As String, _
                                                ByVal Grade As String, _
                                                ByVal Handle As String, _
                                                ByVal Haikiryo As String, _
                                                ByVal Keishiki As String, _
                                                ByVal Kakyuki As String, _
                                                ByVal Kudou As String, _
                                                ByVal Mission As String, _
                                                ByVal Katashiki7 As String, _
                                                ByVal Shimukechi As String, _
                                                ByVal OpCode As String) As List(Of LabelValueVo) Implements ShiyouJyouhouDao.GetBySyagataLabelValues
            Dim sb As New StringBuilder
            With sb
                .Remove(0, .Length)
                .AppendLine("SELECT  ")
                .AppendLine("    RHAC0230.BODY_KIHON_KATA AS LABEL, ")
                .AppendLine("    RHAC0230.BODY_KIHON_KATA AS VALUE ")
                .AppendLine("FROM ")
                .AppendLine(RHACLIBF_DB_NAME & ".dbo.RHAC0030 INNER JOIN " & RHACLIBF_DB_NAME & ".dbo.RHAC0230 ON  ")
                .AppendLine("RHAC0030.KAIHATSU_FUGO = RHAC0230.KAIHATSU_FUGO AND ")
                .AppendLine("RHAC0030.SOBI_KAITEI_NO = RHAC0230.SOBI_KAITEI_NO ")
                .AppendLine("INNER JOIN " & RHACLIBF_DB_NAME & ".dbo.RHAC0540 ON  ")
                .AppendLine("RHAC0230.KAIHATSU_FUGO = RHAC0540.KAIHATSU_FUGO AND ")
                .AppendLine("RHAC0230.SOBI_KAITEI_NO = RHAC0540.SOBI_KAITEI_NO AND ")
                .AppendLine("RHAC0230.KATASHIKI_SCD_7 = RHAC0540.KATASHIKI_SCD_7 ")
                .AppendLine("WHERE RHAC0030.KAIHATSU_FUGO = '" & KaihatsuFugo & "' AND RHAC0030.SHIYOSHO_SEQNO='" & ShiyouJyouhouNo & "' ")
                '車型
                If StringUtil.IsNotEmpty(syagata) Then
                    .AppendLine(" AND RHAC0230.BODY_KIHON_KATA = '" & syagata & "' ")
                End If
                'ｸﾞﾚｰﾄﾞ
                If StringUtil.IsNotEmpty(Grade) Then
                    .AppendLine(" AND RHAC0230.GRADE_CODE = '" & Grade & "' ")
                End If
                ''仕向け
                'If StringUtil.IsNotEmpty(Shimuke) Then
                '    .AppendLine(" AND RHAC0230.SHIMUKE_DAI_KBN = '" & Shimuke & "' ")
                'End If
                'If StringUtil.IsNotEmpty(Shimuke) Then
                '    If StringUtil.Equals(Shimuke, HOYOU_SHIMUKECHI_KOKUNAI) Then
                '        .AppendLine(" AND RHAC0230.SHIMUKE_DAI_KBN = '" & HOYOU_SHIMUKECHI_KOKUNAI_CD & "' ")
                '    End If
                '    If StringUtil.Equals(Shimuke, HOYOU_SHIMUKECHI_HOKUBEI) Then
                '        .AppendLine(" AND RHAC0230.SHIMUKE_DAI_KBN = '" & HOYOU_SHIMUKECHI_HOKUBEI_CD & "' ")
                '    End If
                '    If StringUtil.Equals(Shimuke, HOYOU_SHIMUKECHI_GOUSYU) Then
                '        .AppendLine(" AND RHAC0230.SHIMUKE_DAI_KBN = '" & HOYOU_SHIMUKECHI_GOUSYU_CD & "' ")
                '    End If
                '    If StringUtil.Equals(Shimuke, HOYOU_SHIMUKECHI_OHSYU_MIGI) Then
                '        .AppendLine(" AND RHAC0230.SHIMUKE_DAI_KBN = '" & HOYOU_SHIMUKECHI_OHSYU_MIGI_CD & "' ")
                '    End If
                '    If StringUtil.Equals(Shimuke, HOYOU_SHIMUKECHI_CHUGOKU) Then
                '        .AppendLine(" AND RHAC0230.SHIMUKE_DAI_KBN = '" & HOYOU_SHIMUKECHI_CHUGOKU_CD & "' ")
                '    End If
                '    If StringUtil.Equals(Shimuke, HOYOU_SHIMUKECHI_OHSYU_HIDARI) Then
                '        .AppendLine(" AND RHAC0230.SHIMUKE_DAI_KBN = '" & HOYOU_SHIMUKECHI_OHSYU_HIDARI_CD & "' ")
                '    End If
                'End If
                'ﾊﾝﾄﾞﾙ
                If StringUtil.IsNotEmpty(Handle) Then
                    .AppendLine(" AND RHAC0230.HANDLE_POS = '" & Handle & "' ")
                End If
                '排気量
                If StringUtil.IsNotEmpty(Haikiryo) Then
                    .AppendLine(" AND RHAC0230.EG_HAIKIRYO = '" & Haikiryo & "' ")
                End If
                '形式
                If StringUtil.IsNotEmpty(Keishiki) Then
                    .AppendLine(" AND RHAC0230.DOBENKEI_CODE = '" & Keishiki & "' ")
                End If
                '過給器
                If StringUtil.IsNotEmpty(Kakyuki) Then
                    .AppendLine(" AND RHAC0230.KAKYUKI_CODE = '" & Kakyuki & "' ")
                End If
                '駆動方式
                If StringUtil.IsNotEmpty(Kudou) Then
                    .AppendLine(" AND RHAC0230.KUDO_HOSIKI = '" & Kudou & "' ")
                End If
                '変速機
                If StringUtil.IsNotEmpty(Mission) Then
                    .AppendLine(" AND RHAC0230.TRANS_MISSION = '" & Mission & "' ")
                End If
                '７桁型式
                If StringUtil.IsNotEmpty(Katashiki7) Then
                    .AppendLine(" AND RHAC0230.KATASHIKI_FUGO_7 = '" & Katashiki7 & "' ")
                End If
                '仕向地
                '   ブランクがあるので
                If StringUtil.IsNotEmpty(Shimukechi) Then
                    If Shimukechi = "国内" Then
                        .AppendLine(" AND RHAC0540.SHIMUKECHI_CODE = '' ")
                    Else
                        .AppendLine(" AND RHAC0540.SHIMUKECHI_CODE = '" & Shimukechi & "' ")
                    End If
                End If
                'ＯＰ
                If StringUtil.IsNotEmpty(OpCode) Then
                    .AppendLine(" AND RHAC0540.OP_CODE = '" & OpCode & "' ")
                End If
                .AppendLine("   GROUP BY RHAC0230.BODY_KIHON_KATA ")
                .AppendLine("   ORDER BY RHAC0230.BODY_KIHON_KATA ")
            End With
            Dim db As New EBomDbClient
            Return db.QueryForList(Of LabelValueVo)(sb.ToString)
        End Function

        ''' <summary>
        ''' ｸﾞﾚｰﾄﾞを取得
        ''' </summary>
        ''' <param name="KaihatsuFugo"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Function GetByGradeLabelValues(ByVal KaihatsuFugo As String, _
                                                ByVal ShiyouJyouhouNo As String, _
                                                ByVal syagata As String, _
                                                ByVal Grade As String, _
                                                ByVal Handle As String, _
                                                ByVal Haikiryo As String, _
                                                ByVal Keishiki As String, _
                                                ByVal Kakyuki As String, _
                                                ByVal Kudou As String, _
                                                ByVal Mission As String, _
                                                ByVal Katashiki7 As String, _
                                                ByVal Shimukechi As String, _
                                                ByVal OpCode As String) As List(Of LabelValueVo) Implements ShiyouJyouhouDao.GetByGradeLabelValues
            Dim sb As New StringBuilder
            With sb
                .Remove(0, .Length)
                .AppendLine("SELECT  ")
                .AppendLine("    RHAC0230.GRADE_CODE AS LABEL, ")
                .AppendLine("    RHAC0230.GRADE_CODE AS VALUE ")
                .AppendLine("FROM ")
                .AppendLine(RHACLIBF_DB_NAME & ".dbo.RHAC0030 INNER JOIN " & RHACLIBF_DB_NAME & ".dbo.RHAC0230 ON  ")
                .AppendLine("RHAC0030.KAIHATSU_FUGO = RHAC0230.KAIHATSU_FUGO AND ")
                .AppendLine("RHAC0030.SOBI_KAITEI_NO = RHAC0230.SOBI_KAITEI_NO ")
                .AppendLine("INNER JOIN " & RHACLIBF_DB_NAME & ".dbo.RHAC0540 ON  ")
                .AppendLine("RHAC0230.KAIHATSU_FUGO = RHAC0540.KAIHATSU_FUGO AND ")
                .AppendLine("RHAC0230.SOBI_KAITEI_NO = RHAC0540.SOBI_KAITEI_NO AND ")
                .AppendLine("RHAC0230.KATASHIKI_SCD_7 = RHAC0540.KATASHIKI_SCD_7 ")
                .AppendLine("WHERE RHAC0030.KAIHATSU_FUGO = '" & KaihatsuFugo & "' AND RHAC0030.SHIYOSHO_SEQNO='" & ShiyouJyouhouNo & "' ")
                '車型
                If StringUtil.IsNotEmpty(syagata) Then
                    .AppendLine(" AND RHAC0230.BODY_KIHON_KATA = '" & syagata & "' ")
                End If
                'ｸﾞﾚｰﾄﾞ
                If StringUtil.IsNotEmpty(Grade) Then
                    .AppendLine(" AND RHAC0230.GRADE_CODE = '" & Grade & "' ")
                End If
                ''仕向け
                'If StringUtil.IsNotEmpty(Shimuke) Then
                '    .AppendLine(" AND RHAC0230.SHIMUKE_DAI_KBN = '" & Shimuke & "' ")
                'End If
                'If StringUtil.IsNotEmpty(Shimuke) Then
                '    If StringUtil.Equals(Shimuke, HOYOU_SHIMUKECHI_KOKUNAI) Then
                '        .AppendLine(" AND RHAC0230.SHIMUKE_DAI_KBN = '" & HOYOU_SHIMUKECHI_KOKUNAI_CD & "' ")
                '    End If
                '    If StringUtil.Equals(Shimuke, HOYOU_SHIMUKECHI_HOKUBEI) Then
                '        .AppendLine(" AND RHAC0230.SHIMUKE_DAI_KBN = '" & HOYOU_SHIMUKECHI_HOKUBEI_CD & "' ")
                '    End If
                '    If StringUtil.Equals(Shimuke, HOYOU_SHIMUKECHI_GOUSYU) Then
                '        .AppendLine(" AND RHAC0230.SHIMUKE_DAI_KBN = '" & HOYOU_SHIMUKECHI_GOUSYU_CD & "' ")
                '    End If
                '    If StringUtil.Equals(Shimuke, HOYOU_SHIMUKECHI_OHSYU_MIGI) Then
                '        .AppendLine(" AND RHAC0230.SHIMUKE_DAI_KBN = '" & HOYOU_SHIMUKECHI_OHSYU_MIGI_CD & "' ")
                '    End If
                '    If StringUtil.Equals(Shimuke, HOYOU_SHIMUKECHI_CHUGOKU) Then
                '        .AppendLine(" AND RHAC0230.SHIMUKE_DAI_KBN = '" & HOYOU_SHIMUKECHI_CHUGOKU_CD & "' ")
                '    End If
                '    If StringUtil.Equals(Shimuke, HOYOU_SHIMUKECHI_OHSYU_HIDARI) Then
                '        .AppendLine(" AND RHAC0230.SHIMUKE_DAI_KBN = '" & HOYOU_SHIMUKECHI_OHSYU_HIDARI_CD & "' ")
                '    End If
                'End If
                'ﾊﾝﾄﾞﾙ
                If StringUtil.IsNotEmpty(Handle) Then
                    .AppendLine(" AND RHAC0230.HANDLE_POS = '" & Handle & "' ")
                End If
                '排気量
                If StringUtil.IsNotEmpty(Haikiryo) Then
                    .AppendLine(" AND RHAC0230.EG_HAIKIRYO = '" & Haikiryo & "' ")
                End If
                '形式
                If StringUtil.IsNotEmpty(Keishiki) Then
                    .AppendLine(" AND RHAC0230.DOBENKEI_CODE = '" & Keishiki & "' ")
                End If
                '過給器
                If StringUtil.IsNotEmpty(Kakyuki) Then
                    .AppendLine(" AND RHAC0230.KAKYUKI_CODE = '" & Kakyuki & "' ")
                End If
                '駆動方式
                If StringUtil.IsNotEmpty(Kudou) Then
                    .AppendLine(" AND RHAC0230.KUDO_HOSIKI = '" & Kudou & "' ")
                End If
                '変速機
                If StringUtil.IsNotEmpty(Mission) Then
                    .AppendLine(" AND RHAC0230.TRANS_MISSION = '" & Mission & "' ")
                End If
                '７桁型式
                If StringUtil.IsNotEmpty(Katashiki7) Then
                    .AppendLine(" AND RHAC0230.KATASHIKI_FUGO_7 = '" & Katashiki7 & "' ")
                End If
                '仕向地
                '   ブランクがあるので
                If StringUtil.IsNotEmpty(Shimukechi) Then
                    If Shimukechi = "国内" Then
                        .AppendLine(" AND RHAC0540.SHIMUKECHI_CODE = '' ")
                    Else
                        .AppendLine(" AND RHAC0540.SHIMUKECHI_CODE = '" & Shimukechi & "' ")
                    End If
                End If
                'ＯＰ
                If StringUtil.IsNotEmpty(OpCode) Then
                    .AppendLine(" AND RHAC0540.OP_CODE = '" & OpCode & "' ")
                End If
                .AppendLine("   GROUP BY RHAC0230.GRADE_CODE ")
                .AppendLine("   ORDER BY RHAC0230.GRADE_CODE ")
            End With
            Dim db As New EBomDbClient
            Return db.QueryForList(Of LabelValueVo)(sb.ToString)
        End Function

        ''' <summary>
        ''' 仕向地・仕向けを取得
        ''' </summary>
        ''' <param name="KaihatsuFugo"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Function GetByShimukechiShimukeLabelValues(ByVal KaihatsuFugo As String, _
                                                ByVal ShiyouJyouhouNo As String, _
                                                ByVal syagata As String, _
                                                ByVal Grade As String, _
                                                ByVal Handle As String, _
                                                ByVal Haikiryo As String, _
                                                ByVal Keishiki As String, _
                                                ByVal Kakyuki As String, _
                                                ByVal Kudou As String, _
                                                ByVal Mission As String, _
                                                ByVal Katashiki7 As String, _
                                                ByVal Shimukechi As String, _
                                                ByVal OpCode As String) As List(Of LabelValueVo) Implements ShiyouJyouhouDao.GetByShimukechiShimukeLabelValues
            Dim sb As New StringBuilder
            With sb
                .Remove(0, .Length)
                .AppendLine("SELECT  ")
                .AppendLine("    RHAC0230.SHIMUKE_DAI_KBN AS LABEL, ")
                .AppendLine("    RHAC0230.SHIMUKE_DAI_KBN AS VALUE ")
                '.AppendLine(" CASE WHEN RHAC0230.SHIMUKE_DAI_KBN = '0'  THEN '国内' ")
                '.AppendLine("      WHEN RHAC0230.SHIMUKE_DAI_KBN = '1'  THEN '北米' ")
                '.AppendLine("      WHEN RHAC0230.SHIMUKE_DAI_KBN = '2' AND RHAC0540.SHIMUKECHI_CODE ='KA' THEN '豪州' ")
                '.AppendLine("      WHEN RHAC0230.SHIMUKE_DAI_KBN = '2' AND RHAC0540.SHIMUKECHI_CODE <>'KA' THEN '欧州右' ")
                '.AppendLine("      WHEN RHAC0230.SHIMUKE_DAI_KBN = '3' AND ( RHAC0540.SHIMUKECHI_CODE ='EH' OR RHAC0540.SHIMUKECHI_CODE ='ET' ) THEN '中国' ")
                '.AppendLine("      WHEN RHAC0230.SHIMUKE_DAI_KBN = '3' AND ( RHAC0540.SHIMUKECHI_CODE <> 'EH' AND RHAC0540.SHIMUKECHI_CODE <> 'ET' ) THEN '欧州左' ")
                '.AppendLine(" END ")
                '.AppendLine(" AS LABEL, ")
                '.AppendLine(" CASE WHEN RHAC0230.SHIMUKE_DAI_KBN = '0'  THEN '国内' ")
                '.AppendLine("      WHEN RHAC0230.SHIMUKE_DAI_KBN = '1'  THEN '北米' ")
                '.AppendLine("      WHEN RHAC0230.SHIMUKE_DAI_KBN = '2' AND RHAC0540.SHIMUKECHI_CODE ='KA' THEN '豪州' ")
                '.AppendLine("      WHEN RHAC0230.SHIMUKE_DAI_KBN = '2' AND RHAC0540.SHIMUKECHI_CODE <>'KA' THEN '欧州右' ")
                '.AppendLine("      WHEN RHAC0230.SHIMUKE_DAI_KBN = '3' AND ( RHAC0540.SHIMUKECHI_CODE ='EH' OR RHAC0540.SHIMUKECHI_CODE ='ET' ) THEN '中国' ")
                '.AppendLine("      WHEN RHAC0230.SHIMUKE_DAI_KBN = '3' AND ( RHAC0540.SHIMUKECHI_CODE <> 'EH' AND RHAC0540.SHIMUKECHI_CODE <> 'ET' ) THEN '欧州左' ")
                '.AppendLine(" END ")
                '.AppendLine(" AS VALUE ")
                .AppendLine("FROM ")
                .AppendLine(RHACLIBF_DB_NAME & ".dbo.RHAC0030 INNER JOIN " & RHACLIBF_DB_NAME & ".dbo.RHAC0230 ON  ")
                .AppendLine("RHAC0030.KAIHATSU_FUGO = RHAC0230.KAIHATSU_FUGO AND ")
                .AppendLine("RHAC0030.SOBI_KAITEI_NO = RHAC0230.SOBI_KAITEI_NO ")
                .AppendLine("INNER JOIN " & RHACLIBF_DB_NAME & ".dbo.RHAC0540 ON  ")
                .AppendLine("RHAC0230.KAIHATSU_FUGO = RHAC0540.KAIHATSU_FUGO AND ")
                .AppendLine("RHAC0230.SOBI_KAITEI_NO = RHAC0540.SOBI_KAITEI_NO AND ")
                .AppendLine("RHAC0230.KATASHIKI_SCD_7 = RHAC0540.KATASHIKI_SCD_7 ")
                .AppendLine("WHERE RHAC0030.KAIHATSU_FUGO = '" & KaihatsuFugo & "' AND RHAC0030.SHIYOSHO_SEQNO='" & ShiyouJyouhouNo & "' ")
                '車型
                If StringUtil.IsNotEmpty(syagata) Then
                    .AppendLine(" AND RHAC0230.BODY_KIHON_KATA = '" & syagata & "' ")
                End If
                'ｸﾞﾚｰﾄﾞ
                If StringUtil.IsNotEmpty(Grade) Then
                    .AppendLine(" AND RHAC0230.GRADE_CODE = '" & Grade & "' ")
                End If
                '仕向け
                'If StringUtil.IsNotEmpty(Shimuke) Then
                '    .AppendLine(" AND RHAC0230.SHIMUKE_DAI_KBN = '" & Shimuke & "' ")
                'End If
                'If StringUtil.IsNotEmpty(Shimuke) Then
                '    If StringUtil.Equals(Shimuke, HOYOU_SHIMUKECHI_KOKUNAI) Then
                '        .AppendLine(" AND RHAC0230.SHIMUKE_DAI_KBN = '" & HOYOU_SHIMUKECHI_KOKUNAI_CD & "' ")
                '    End If
                '    If StringUtil.Equals(Shimuke, HOYOU_SHIMUKECHI_HOKUBEI) Then
                '        .AppendLine(" AND RHAC0230.SHIMUKE_DAI_KBN = '" & HOYOU_SHIMUKECHI_HOKUBEI_CD & "' ")
                '    End If
                '    If StringUtil.Equals(Shimuke, HOYOU_SHIMUKECHI_GOUSYU) Then
                '        .AppendLine(" AND RHAC0230.SHIMUKE_DAI_KBN = '" & HOYOU_SHIMUKECHI_GOUSYU_CD & "' ")
                '    End If
                '    If StringUtil.Equals(Shimuke, HOYOU_SHIMUKECHI_OHSYU_MIGI) Then
                '        .AppendLine(" AND RHAC0230.SHIMUKE_DAI_KBN = '" & HOYOU_SHIMUKECHI_OHSYU_MIGI_CD & "' ")
                '    End If
                '    If StringUtil.Equals(Shimuke, HOYOU_SHIMUKECHI_CHUGOKU) Then
                '        .AppendLine(" AND RHAC0230.SHIMUKE_DAI_KBN = '" & HOYOU_SHIMUKECHI_CHUGOKU_CD & "' ")
                '    End If
                '    If StringUtil.Equals(Shimuke, HOYOU_SHIMUKECHI_OHSYU_HIDARI) Then
                '        .AppendLine(" AND RHAC0230.SHIMUKE_DAI_KBN = '" & HOYOU_SHIMUKECHI_OHSYU_HIDARI_CD & "' ")
                '    End If
                'End If
                'ﾊﾝﾄﾞﾙ
                If StringUtil.IsNotEmpty(Handle) Then
                    .AppendLine(" AND RHAC0230.HANDLE_POS = '" & Handle & "' ")
                End If
                '排気量
                If StringUtil.IsNotEmpty(Haikiryo) Then
                    .AppendLine(" AND RHAC0230.EG_HAIKIRYO = '" & Haikiryo & "' ")
                End If
                '形式
                If StringUtil.IsNotEmpty(Keishiki) Then
                    .AppendLine(" AND RHAC0230.DOBENKEI_CODE = '" & Keishiki & "' ")
                End If
                '過給器
                If StringUtil.IsNotEmpty(Kakyuki) Then
                    .AppendLine(" AND RHAC0230.KAKYUKI_CODE = '" & Kakyuki & "' ")
                End If
                '駆動方式
                If StringUtil.IsNotEmpty(Kudou) Then
                    .AppendLine(" AND RHAC0230.KUDO_HOSIKI = '" & Kudou & "' ")
                End If
                '変速機
                If StringUtil.IsNotEmpty(Mission) Then
                    .AppendLine(" AND RHAC0230.TRANS_MISSION = '" & Mission & "' ")
                End If
                '７桁型式
                If StringUtil.IsNotEmpty(Katashiki7) Then
                    .AppendLine(" AND RHAC0230.KATASHIKI_FUGO_7 = '" & Katashiki7 & "' ")
                End If
                '仕向地
                '   ブランクがあるので
                If StringUtil.IsNotEmpty(Shimukechi) Then
                    If Shimukechi = "国内" Then
                        .AppendLine(" AND RHAC0540.SHIMUKECHI_CODE = '' ")
                    Else
                        .AppendLine(" AND RHAC0540.SHIMUKECHI_CODE = '" & Shimukechi & "' ")
                    End If
                End If
                'ＯＰ
                If StringUtil.IsNotEmpty(OpCode) Then
                    .AppendLine(" AND RHAC0540.OP_CODE = '" & OpCode & "' ")
                End If
                .AppendLine("   GROUP BY RHAC0230.SHIMUKE_DAI_KBN ")
                .AppendLine("   ORDER BY RHAC0230.SHIMUKE_DAI_KBN ")
            End With
            Dim db As New EBomDbClient
            Return db.QueryForList(Of LabelValueVo)(sb.ToString)
        End Function

        ''' <summary>
        ''' 仕向地・ﾊﾝﾄﾞﾙを取得
        ''' </summary>
        ''' <param name="KaihatsuFugo"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Function GetByShimukechiHandleLabelValues(ByVal KaihatsuFugo As String, _
                                                ByVal ShiyouJyouhouNo As String, _
                                                ByVal syagata As String, _
                                                ByVal Grade As String, _
                                                ByVal Handle As String, _
                                                ByVal Haikiryo As String, _
                                                ByVal Keishiki As String, _
                                                ByVal Kakyuki As String, _
                                                ByVal Kudou As String, _
                                                ByVal Mission As String, _
                                                ByVal Katashiki7 As String, _
                                                ByVal Shimukechi As String, _
                                                ByVal OpCode As String) As List(Of LabelValueVo) Implements ShiyouJyouhouDao.GetByShimukechiHandleLabelValues
            Dim sb As New StringBuilder
            With sb
                .Remove(0, .Length)
                .AppendLine("SELECT  ")
                .AppendLine("    RHAC0230.HANDLE_POS AS LABEL, ")
                .AppendLine("    RHAC0230.HANDLE_POS AS VALUE ")
                .AppendLine("FROM ")
                .AppendLine(RHACLIBF_DB_NAME & ".dbo.RHAC0030 INNER JOIN " & RHACLIBF_DB_NAME & ".dbo.RHAC0230 ON  ")
                .AppendLine("RHAC0030.KAIHATSU_FUGO = RHAC0230.KAIHATSU_FUGO AND ")
                .AppendLine("RHAC0030.SOBI_KAITEI_NO = RHAC0230.SOBI_KAITEI_NO ")
                .AppendLine("INNER JOIN " & RHACLIBF_DB_NAME & ".dbo.RHAC0540 ON  ")
                .AppendLine("RHAC0230.KAIHATSU_FUGO = RHAC0540.KAIHATSU_FUGO AND ")
                .AppendLine("RHAC0230.SOBI_KAITEI_NO = RHAC0540.SOBI_KAITEI_NO AND ")
                .AppendLine("RHAC0230.KATASHIKI_SCD_7 = RHAC0540.KATASHIKI_SCD_7 ")
                .AppendLine("WHERE RHAC0030.KAIHATSU_FUGO = '" & KaihatsuFugo & "' AND RHAC0030.SHIYOSHO_SEQNO='" & ShiyouJyouhouNo & "' ")
                '車型
                If StringUtil.IsNotEmpty(syagata) Then
                    .AppendLine(" AND RHAC0230.BODY_KIHON_KATA = '" & syagata & "' ")
                End If
                'ｸﾞﾚｰﾄﾞ
                If StringUtil.IsNotEmpty(Grade) Then
                    .AppendLine(" AND RHAC0230.GRADE_CODE = '" & Grade & "' ")
                End If
                ''仕向け
                'If StringUtil.IsNotEmpty(Shimuke) Then
                '    .AppendLine(" AND RHAC0230.SHIMUKE_DAI_KBN = '" & Shimuke & "' ")
                'End If
                'If StringUtil.IsNotEmpty(Shimuke) Then
                '    If StringUtil.Equals(Shimuke, HOYOU_SHIMUKECHI_KOKUNAI) Then
                '        .AppendLine(" AND RHAC0230.SHIMUKE_DAI_KBN = '" & HOYOU_SHIMUKECHI_KOKUNAI_CD & "' ")
                '    End If
                '    If StringUtil.Equals(Shimuke, HOYOU_SHIMUKECHI_HOKUBEI) Then
                '        .AppendLine(" AND RHAC0230.SHIMUKE_DAI_KBN = '" & HOYOU_SHIMUKECHI_HOKUBEI_CD & "' ")
                '    End If
                '    If StringUtil.Equals(Shimuke, HOYOU_SHIMUKECHI_GOUSYU) Then
                '        .AppendLine(" AND RHAC0230.SHIMUKE_DAI_KBN = '" & HOYOU_SHIMUKECHI_GOUSYU_CD & "' ")
                '    End If
                '    If StringUtil.Equals(Shimuke, HOYOU_SHIMUKECHI_OHSYU_MIGI) Then
                '        .AppendLine(" AND RHAC0230.SHIMUKE_DAI_KBN = '" & HOYOU_SHIMUKECHI_OHSYU_MIGI_CD & "' ")
                '    End If
                '    If StringUtil.Equals(Shimuke, HOYOU_SHIMUKECHI_CHUGOKU) Then
                '        .AppendLine(" AND RHAC0230.SHIMUKE_DAI_KBN = '" & HOYOU_SHIMUKECHI_CHUGOKU_CD & "' ")
                '    End If
                '    If StringUtil.Equals(Shimuke, HOYOU_SHIMUKECHI_OHSYU_HIDARI) Then
                '        .AppendLine(" AND RHAC0230.SHIMUKE_DAI_KBN = '" & HOYOU_SHIMUKECHI_OHSYU_HIDARI_CD & "' ")
                '    End If
                'End If
                'ﾊﾝﾄﾞﾙ
                If StringUtil.IsNotEmpty(Handle) Then
                    .AppendLine(" AND RHAC0230.HANDLE_POS = '" & Handle & "' ")
                End If
                '排気量
                If StringUtil.IsNotEmpty(Haikiryo) Then
                    .AppendLine(" AND RHAC0230.EG_HAIKIRYO = '" & Haikiryo & "' ")
                End If
                '形式
                If StringUtil.IsNotEmpty(Keishiki) Then
                    .AppendLine(" AND RHAC0230.DOBENKEI_CODE = '" & Keishiki & "' ")
                End If
                '過給器
                If StringUtil.IsNotEmpty(Kakyuki) Then
                    .AppendLine(" AND RHAC0230.KAKYUKI_CODE = '" & Kakyuki & "' ")
                End If
                '駆動方式
                If StringUtil.IsNotEmpty(Kudou) Then
                    .AppendLine(" AND RHAC0230.KUDO_HOSIKI = '" & Kudou & "' ")
                End If
                '変速機
                If StringUtil.IsNotEmpty(Mission) Then
                    .AppendLine(" AND RHAC0230.TRANS_MISSION = '" & Mission & "' ")
                End If
                '７桁型式
                If StringUtil.IsNotEmpty(Katashiki7) Then
                    .AppendLine(" AND RHAC0230.KATASHIKI_FUGO_7 = '" & Katashiki7 & "' ")
                End If
                '仕向地
                '   ブランクがあるので
                If StringUtil.IsNotEmpty(Shimukechi) Then
                    If Shimukechi = "国内" Then
                        .AppendLine(" AND RHAC0540.SHIMUKECHI_CODE = '' ")
                    Else
                        .AppendLine(" AND RHAC0540.SHIMUKECHI_CODE = '" & Shimukechi & "' ")
                    End If
                End If
                'ＯＰ
                If StringUtil.IsNotEmpty(OpCode) Then
                    .AppendLine(" AND RHAC0540.OP_CODE = '" & OpCode & "' ")
                End If
                .AppendLine("   GROUP BY RHAC0230.HANDLE_POS ")
                .AppendLine("   ORDER BY RHAC0230.HANDLE_POS ")
            End With
            Dim db As New EBomDbClient
            Return db.QueryForList(Of LabelValueVo)(sb.ToString)
        End Function

        ''' <summary>
        ''' E/G・排気量を取得
        ''' </summary>
        ''' <param name="KaihatsuFugo"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Function GetByEgHaikiryouLabelValues(ByVal KaihatsuFugo As String, _
                                                ByVal ShiyouJyouhouNo As String, _
                                                ByVal syagata As String, _
                                                ByVal Grade As String, _
                                                ByVal Handle As String, _
                                                ByVal Haikiryo As String, _
                                                ByVal Keishiki As String, _
                                                ByVal Kakyuki As String, _
                                                ByVal Kudou As String, _
                                                ByVal Mission As String, _
                                                ByVal Katashiki7 As String, _
                                                ByVal Shimukechi As String, _
                                                ByVal OpCode As String) As List(Of LabelValueVo) Implements ShiyouJyouhouDao.GetByEgHaikiryouLabelValues
            Dim sb As New StringBuilder
            With sb
                .Remove(0, .Length)
                .AppendLine("SELECT  ")
                .AppendLine("    RHAC0230.EG_HAIKIRYO AS LABEL, ")
                .AppendLine("    RHAC0230.EG_HAIKIRYO AS VALUE ")
                .AppendLine("FROM ")
                .AppendLine(RHACLIBF_DB_NAME & ".dbo.RHAC0030 INNER JOIN " & RHACLIBF_DB_NAME & ".dbo.RHAC0230 ON  ")
                .AppendLine("RHAC0030.KAIHATSU_FUGO = RHAC0230.KAIHATSU_FUGO AND ")
                .AppendLine("RHAC0030.SOBI_KAITEI_NO = RHAC0230.SOBI_KAITEI_NO ")
                .AppendLine("INNER JOIN " & RHACLIBF_DB_NAME & ".dbo.RHAC0540 ON  ")
                .AppendLine("RHAC0230.KAIHATSU_FUGO = RHAC0540.KAIHATSU_FUGO AND ")
                .AppendLine("RHAC0230.SOBI_KAITEI_NO = RHAC0540.SOBI_KAITEI_NO AND ")
                .AppendLine("RHAC0230.KATASHIKI_SCD_7 = RHAC0540.KATASHIKI_SCD_7 ")
                .AppendLine("WHERE RHAC0030.KAIHATSU_FUGO = '" & KaihatsuFugo & "' AND RHAC0030.SHIYOSHO_SEQNO='" & ShiyouJyouhouNo & "' ")
                '車型
                If StringUtil.IsNotEmpty(syagata) Then
                    .AppendLine(" AND RHAC0230.BODY_KIHON_KATA = '" & syagata & "' ")
                End If
                'ｸﾞﾚｰﾄﾞ
                If StringUtil.IsNotEmpty(Grade) Then
                    .AppendLine(" AND RHAC0230.GRADE_CODE = '" & Grade & "' ")
                End If
                ''仕向け
                'If StringUtil.IsNotEmpty(Shimuke) Then
                '    .AppendLine(" AND RHAC0230.SHIMUKE_DAI_KBN = '" & Shimuke & "' ")
                'End If
                'If StringUtil.IsNotEmpty(Shimuke) Then
                '    If StringUtil.Equals(Shimuke, HOYOU_SHIMUKECHI_KOKUNAI) Then
                '        .AppendLine(" AND RHAC0230.SHIMUKE_DAI_KBN = '" & HOYOU_SHIMUKECHI_KOKUNAI_CD & "' ")
                '    End If
                '    If StringUtil.Equals(Shimuke, HOYOU_SHIMUKECHI_HOKUBEI) Then
                '        .AppendLine(" AND RHAC0230.SHIMUKE_DAI_KBN = '" & HOYOU_SHIMUKECHI_HOKUBEI_CD & "' ")
                '    End If
                '    If StringUtil.Equals(Shimuke, HOYOU_SHIMUKECHI_GOUSYU) Then
                '        .AppendLine(" AND RHAC0230.SHIMUKE_DAI_KBN = '" & HOYOU_SHIMUKECHI_GOUSYU_CD & "' ")
                '    End If
                '    If StringUtil.Equals(Shimuke, HOYOU_SHIMUKECHI_OHSYU_MIGI) Then
                '        .AppendLine(" AND RHAC0230.SHIMUKE_DAI_KBN = '" & HOYOU_SHIMUKECHI_OHSYU_MIGI_CD & "' ")
                '    End If
                '    If StringUtil.Equals(Shimuke, HOYOU_SHIMUKECHI_CHUGOKU) Then
                '        .AppendLine(" AND RHAC0230.SHIMUKE_DAI_KBN = '" & HOYOU_SHIMUKECHI_CHUGOKU_CD & "' ")
                '    End If
                '    If StringUtil.Equals(Shimuke, HOYOU_SHIMUKECHI_OHSYU_HIDARI) Then
                '        .AppendLine(" AND RHAC0230.SHIMUKE_DAI_KBN = '" & HOYOU_SHIMUKECHI_OHSYU_HIDARI_CD & "' ")
                '    End If
                'End If
                'ﾊﾝﾄﾞﾙ
                If StringUtil.IsNotEmpty(Handle) Then
                    .AppendLine(" AND RHAC0230.HANDLE_POS = '" & Handle & "' ")
                End If
                '排気量
                If StringUtil.IsNotEmpty(Haikiryo) Then
                    .AppendLine(" AND RHAC0230.EG_HAIKIRYO = '" & Haikiryo & "' ")
                End If
                '形式
                If StringUtil.IsNotEmpty(Keishiki) Then
                    .AppendLine(" AND RHAC0230.DOBENKEI_CODE = '" & Keishiki & "' ")
                End If
                '過給器
                If StringUtil.IsNotEmpty(Kakyuki) Then
                    .AppendLine(" AND RHAC0230.KAKYUKI_CODE = '" & Kakyuki & "' ")
                End If
                '駆動方式
                If StringUtil.IsNotEmpty(Kudou) Then
                    .AppendLine(" AND RHAC0230.KUDO_HOSIKI = '" & Kudou & "' ")
                End If
                '変速機
                If StringUtil.IsNotEmpty(Mission) Then
                    .AppendLine(" AND RHAC0230.TRANS_MISSION = '" & Mission & "' ")
                End If
                '７桁型式
                If StringUtil.IsNotEmpty(Katashiki7) Then
                    .AppendLine(" AND RHAC0230.KATASHIKI_FUGO_7 = '" & Katashiki7 & "' ")
                End If
                '仕向地
                '   ブランクがあるので
                If StringUtil.IsNotEmpty(Shimukechi) Then
                    If Shimukechi = "国内" Then
                        .AppendLine(" AND RHAC0540.SHIMUKECHI_CODE = '' ")
                    Else
                        .AppendLine(" AND RHAC0540.SHIMUKECHI_CODE = '" & Shimukechi & "' ")
                    End If
                End If
                'ＯＰ
                If StringUtil.IsNotEmpty(OpCode) Then
                    .AppendLine(" AND RHAC0540.OP_CODE = '" & OpCode & "' ")
                End If
                .AppendLine("   GROUP BY RHAC0230.EG_HAIKIRYO ")
                .AppendLine("   ORDER BY RHAC0230.EG_HAIKIRYO ")
            End With
            Dim db As New EBomDbClient
            Return db.QueryForList(Of LabelValueVo)(sb.ToString)
        End Function

        ''' <summary>
        ''' E/G・形式を取得
        ''' </summary>
        ''' <param name="KaihatsuFugo"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Function GetByEgKeishikiLabelValues(ByVal KaihatsuFugo As String, _
                                                ByVal ShiyouJyouhouNo As String, _
                                                ByVal syagata As String, _
                                                ByVal Grade As String, _
                                                ByVal Handle As String, _
                                                ByVal Haikiryo As String, _
                                                ByVal Keishiki As String, _
                                                ByVal Kakyuki As String, _
                                                ByVal Kudou As String, _
                                                ByVal Mission As String, _
                                                ByVal Katashiki7 As String, _
                                                ByVal Shimukechi As String, _
                                                ByVal OpCode As String) As List(Of LabelValueVo) Implements ShiyouJyouhouDao.GetByEgKeishikiLabelValues
            Dim sb As New StringBuilder
            With sb
                .Remove(0, .Length)
                .AppendLine("SELECT  ")
                .AppendLine("    RHAC0230.DOBENKEI_CODE AS LABEL, ")
                .AppendLine("    RHAC0230.DOBENKEI_CODE AS VALUE ")
                .AppendLine("FROM ")
                .AppendLine(RHACLIBF_DB_NAME & ".dbo.RHAC0030 INNER JOIN " & RHACLIBF_DB_NAME & ".dbo.RHAC0230 ON  ")
                .AppendLine("RHAC0030.KAIHATSU_FUGO = RHAC0230.KAIHATSU_FUGO AND ")
                .AppendLine("RHAC0030.SOBI_KAITEI_NO = RHAC0230.SOBI_KAITEI_NO ")
                .AppendLine("INNER JOIN " & RHACLIBF_DB_NAME & ".dbo.RHAC0540 ON  ")
                .AppendLine("RHAC0230.KAIHATSU_FUGO = RHAC0540.KAIHATSU_FUGO AND ")
                .AppendLine("RHAC0230.SOBI_KAITEI_NO = RHAC0540.SOBI_KAITEI_NO AND ")
                .AppendLine("RHAC0230.KATASHIKI_SCD_7 = RHAC0540.KATASHIKI_SCD_7 ")
                .AppendLine("WHERE RHAC0030.KAIHATSU_FUGO = '" & KaihatsuFugo & "' AND RHAC0030.SHIYOSHO_SEQNO='" & ShiyouJyouhouNo & "' ")
                '車型
                If StringUtil.IsNotEmpty(syagata) Then
                    .AppendLine(" AND RHAC0230.BODY_KIHON_KATA = '" & syagata & "' ")
                End If
                'ｸﾞﾚｰﾄﾞ
                If StringUtil.IsNotEmpty(Grade) Then
                    .AppendLine(" AND RHAC0230.GRADE_CODE = '" & Grade & "' ")
                End If
                ''仕向け
                'If StringUtil.IsNotEmpty(Shimuke) Then
                '    .AppendLine(" AND RHAC0230.SHIMUKE_DAI_KBN = '" & Shimuke & "' ")
                'End If
                'If StringUtil.IsNotEmpty(Shimuke) Then
                '    If StringUtil.Equals(Shimuke, HOYOU_SHIMUKECHI_KOKUNAI) Then
                '        .AppendLine(" AND RHAC0230.SHIMUKE_DAI_KBN = '" & HOYOU_SHIMUKECHI_KOKUNAI_CD & "' ")
                '    End If
                '    If StringUtil.Equals(Shimuke, HOYOU_SHIMUKECHI_HOKUBEI) Then
                '        .AppendLine(" AND RHAC0230.SHIMUKE_DAI_KBN = '" & HOYOU_SHIMUKECHI_HOKUBEI_CD & "' ")
                '    End If
                '    If StringUtil.Equals(Shimuke, HOYOU_SHIMUKECHI_GOUSYU) Then
                '        .AppendLine(" AND RHAC0230.SHIMUKE_DAI_KBN = '" & HOYOU_SHIMUKECHI_GOUSYU_CD & "' ")
                '    End If
                '    If StringUtil.Equals(Shimuke, HOYOU_SHIMUKECHI_OHSYU_MIGI) Then
                '        .AppendLine(" AND RHAC0230.SHIMUKE_DAI_KBN = '" & HOYOU_SHIMUKECHI_OHSYU_MIGI_CD & "' ")
                '    End If
                '    If StringUtil.Equals(Shimuke, HOYOU_SHIMUKECHI_CHUGOKU) Then
                '        .AppendLine(" AND RHAC0230.SHIMUKE_DAI_KBN = '" & HOYOU_SHIMUKECHI_CHUGOKU_CD & "' ")
                '    End If
                '    If StringUtil.Equals(Shimuke, HOYOU_SHIMUKECHI_OHSYU_HIDARI) Then
                '        .AppendLine(" AND RHAC0230.SHIMUKE_DAI_KBN = '" & HOYOU_SHIMUKECHI_OHSYU_HIDARI_CD & "' ")
                '    End If
                'End If
                'ﾊﾝﾄﾞﾙ
                If StringUtil.IsNotEmpty(Handle) Then
                    .AppendLine(" AND RHAC0230.HANDLE_POS = '" & Handle & "' ")
                End If
                '排気量
                If StringUtil.IsNotEmpty(Haikiryo) Then
                    .AppendLine(" AND RHAC0230.EG_HAIKIRYO = '" & Haikiryo & "' ")
                End If
                '形式
                If StringUtil.IsNotEmpty(Keishiki) Then
                    .AppendLine(" AND RHAC0230.DOBENKEI_CODE = '" & Keishiki & "' ")
                End If
                '過給器
                If StringUtil.IsNotEmpty(Kakyuki) Then
                    .AppendLine(" AND RHAC0230.KAKYUKI_CODE = '" & Kakyuki & "' ")
                End If
                '駆動方式
                If StringUtil.IsNotEmpty(Kudou) Then
                    .AppendLine(" AND RHAC0230.KUDO_HOSIKI = '" & Kudou & "' ")
                End If
                '変速機
                If StringUtil.IsNotEmpty(Mission) Then
                    .AppendLine(" AND RHAC0230.TRANS_MISSION = '" & Mission & "' ")
                End If
                '７桁型式
                If StringUtil.IsNotEmpty(Katashiki7) Then
                    .AppendLine(" AND RHAC0230.KATASHIKI_FUGO_7 = '" & Katashiki7 & "' ")
                End If
                '仕向地
                '   ブランクがあるので
                If StringUtil.IsNotEmpty(Shimukechi) Then
                    If Shimukechi = "国内" Then
                        .AppendLine(" AND RHAC0540.SHIMUKECHI_CODE = '' ")
                    Else
                        .AppendLine(" AND RHAC0540.SHIMUKECHI_CODE = '" & Shimukechi & "' ")
                    End If
                End If
                'ＯＰ
                If StringUtil.IsNotEmpty(OpCode) Then
                    .AppendLine(" AND RHAC0540.OP_CODE = '" & OpCode & "' ")
                End If
                .AppendLine("   GROUP BY RHAC0230.DOBENKEI_CODE ")
                .AppendLine("   ORDER BY RHAC0230.DOBENKEI_CODE ")
            End With
            Dim db As New EBomDbClient
            Return db.QueryForList(Of LabelValueVo)(sb.ToString)
        End Function

        ''' <summary>
        ''' E/G・過給器を取得
        ''' </summary>
        ''' <param name="KaihatsuFugo"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Function GetByEgKakyukiLabelValues(ByVal KaihatsuFugo As String, _
                                                ByVal ShiyouJyouhouNo As String, _
                                                ByVal syagata As String, _
                                                ByVal Grade As String, _
                                                ByVal Handle As String, _
                                                ByVal Haikiryo As String, _
                                                ByVal Keishiki As String, _
                                                ByVal Kakyuki As String, _
                                                ByVal Kudou As String, _
                                                ByVal Mission As String, _
                                                ByVal Katashiki7 As String, _
                                                ByVal Shimukechi As String, _
                                                ByVal OpCode As String) As List(Of LabelValueVo) Implements ShiyouJyouhouDao.GetByEgKakyukiLabelValues
            Dim sb As New StringBuilder
            With sb
                .Remove(0, .Length)
                .AppendLine("SELECT  ")
                .AppendLine("    RHAC0230.KAKYUKI_CODE AS LABEL, ")
                .AppendLine("    RHAC0230.KAKYUKI_CODE AS VALUE ")
                .AppendLine("FROM ")
                .AppendLine(RHACLIBF_DB_NAME & ".dbo.RHAC0030 INNER JOIN " & RHACLIBF_DB_NAME & ".dbo.RHAC0230 ON  ")
                .AppendLine("RHAC0030.KAIHATSU_FUGO = RHAC0230.KAIHATSU_FUGO AND ")
                .AppendLine("RHAC0030.SOBI_KAITEI_NO = RHAC0230.SOBI_KAITEI_NO ")
                .AppendLine("INNER JOIN " & RHACLIBF_DB_NAME & ".dbo.RHAC0540 ON  ")
                .AppendLine("RHAC0230.KAIHATSU_FUGO = RHAC0540.KAIHATSU_FUGO AND ")
                .AppendLine("RHAC0230.SOBI_KAITEI_NO = RHAC0540.SOBI_KAITEI_NO AND ")
                .AppendLine("RHAC0230.KATASHIKI_SCD_7 = RHAC0540.KATASHIKI_SCD_7 ")
                .AppendLine("WHERE RHAC0030.KAIHATSU_FUGO = '" & KaihatsuFugo & "' AND RHAC0030.SHIYOSHO_SEQNO='" & ShiyouJyouhouNo & "' ")
                '車型
                If StringUtil.IsNotEmpty(syagata) Then
                    .AppendLine(" AND RHAC0230.BODY_KIHON_KATA = '" & syagata & "' ")
                End If
                'ｸﾞﾚｰﾄﾞ
                If StringUtil.IsNotEmpty(Grade) Then
                    .AppendLine(" AND RHAC0230.GRADE_CODE = '" & Grade & "' ")
                End If
                ''仕向け
                'If StringUtil.IsNotEmpty(Shimuke) Then
                '    .AppendLine(" AND RHAC0230.SHIMUKE_DAI_KBN = '" & Shimuke & "' ")
                'End If
                'If StringUtil.IsNotEmpty(Shimuke) Then
                '    If StringUtil.Equals(Shimuke, HOYOU_SHIMUKECHI_KOKUNAI) Then
                '        .AppendLine(" AND RHAC0230.SHIMUKE_DAI_KBN = '" & HOYOU_SHIMUKECHI_KOKUNAI_CD & "' ")
                '    End If
                '    If StringUtil.Equals(Shimuke, HOYOU_SHIMUKECHI_HOKUBEI) Then
                '        .AppendLine(" AND RHAC0230.SHIMUKE_DAI_KBN = '" & HOYOU_SHIMUKECHI_HOKUBEI_CD & "' ")
                '    End If
                '    If StringUtil.Equals(Shimuke, HOYOU_SHIMUKECHI_GOUSYU) Then
                '        .AppendLine(" AND RHAC0230.SHIMUKE_DAI_KBN = '" & HOYOU_SHIMUKECHI_GOUSYU_CD & "' ")
                '    End If
                '    If StringUtil.Equals(Shimuke, HOYOU_SHIMUKECHI_OHSYU_MIGI) Then
                '        .AppendLine(" AND RHAC0230.SHIMUKE_DAI_KBN = '" & HOYOU_SHIMUKECHI_OHSYU_MIGI_CD & "' ")
                '    End If
                '    If StringUtil.Equals(Shimuke, HOYOU_SHIMUKECHI_CHUGOKU) Then
                '        .AppendLine(" AND RHAC0230.SHIMUKE_DAI_KBN = '" & HOYOU_SHIMUKECHI_CHUGOKU_CD & "' ")
                '    End If
                '    If StringUtil.Equals(Shimuke, HOYOU_SHIMUKECHI_OHSYU_HIDARI) Then
                '        .AppendLine(" AND RHAC0230.SHIMUKE_DAI_KBN = '" & HOYOU_SHIMUKECHI_OHSYU_HIDARI_CD & "' ")
                '    End If
                'End If
                'ﾊﾝﾄﾞﾙ
                If StringUtil.IsNotEmpty(Handle) Then
                    .AppendLine(" AND RHAC0230.HANDLE_POS = '" & Handle & "' ")
                End If
                '排気量
                If StringUtil.IsNotEmpty(Haikiryo) Then
                    .AppendLine(" AND RHAC0230.EG_HAIKIRYO = '" & Haikiryo & "' ")
                End If
                '形式
                If StringUtil.IsNotEmpty(Keishiki) Then
                    .AppendLine(" AND RHAC0230.DOBENKEI_CODE = '" & Keishiki & "' ")
                End If
                '過給器
                If StringUtil.IsNotEmpty(Kakyuki) Then
                    .AppendLine(" AND RHAC0230.KAKYUKI_CODE = '" & Kakyuki & "' ")
                End If
                '駆動方式
                If StringUtil.IsNotEmpty(Kudou) Then
                    .AppendLine(" AND RHAC0230.KUDO_HOSIKI = '" & Kudou & "' ")
                End If
                '変速機
                If StringUtil.IsNotEmpty(Mission) Then
                    .AppendLine(" AND RHAC0230.TRANS_MISSION = '" & Mission & "' ")
                End If
                '７桁型式
                If StringUtil.IsNotEmpty(Katashiki7) Then
                    .AppendLine(" AND RHAC0230.KATASHIKI_FUGO_7 = '" & Katashiki7 & "' ")
                End If
                '仕向地
                '   ブランクがあるので
                If StringUtil.IsNotEmpty(Shimukechi) Then
                    If Shimukechi = "国内" Then
                        .AppendLine(" AND RHAC0540.SHIMUKECHI_CODE = '' ")
                    Else
                        .AppendLine(" AND RHAC0540.SHIMUKECHI_CODE = '" & Shimukechi & "' ")
                    End If
                End If
                'ＯＰ
                If StringUtil.IsNotEmpty(OpCode) Then
                    .AppendLine(" AND RHAC0540.OP_CODE = '" & OpCode & "' ")
                End If
                .AppendLine("   GROUP BY RHAC0230.KAKYUKI_CODE ")
                .AppendLine("   ORDER BY RHAC0230.KAKYUKI_CODE ")
            End With
            Dim db As New EBomDbClient
            Return db.QueryForList(Of LabelValueVo)(sb.ToString)
        End Function

        ''' <summary>
        ''' T/M・駆動方式を取得
        ''' </summary>
        ''' <param name="KaihatsuFugo"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Function GetByTmKudouLabelValues(ByVal KaihatsuFugo As String, _
                                                ByVal ShiyouJyouhouNo As String, _
                                                ByVal syagata As String, _
                                                ByVal Grade As String, _
                                                ByVal Handle As String, _
                                                ByVal Haikiryo As String, _
                                                ByVal Keishiki As String, _
                                                ByVal Kakyuki As String, _
                                                ByVal Kudou As String, _
                                                ByVal Mission As String, _
                                                ByVal Katashiki7 As String, _
                                                ByVal Shimukechi As String, _
                                                ByVal OpCode As String) As List(Of LabelValueVo) Implements ShiyouJyouhouDao.GetByTmKudouLabelValues
            Dim sb As New StringBuilder
            With sb
                .Remove(0, .Length)
                .AppendLine("SELECT  ")
                .AppendLine("    RHAC0230.KUDO_HOSIKI AS LABEL, ")
                .AppendLine("    RHAC0230.KUDO_HOSIKI AS VALUE ")
                .AppendLine("FROM ")
                .AppendLine(RHACLIBF_DB_NAME & ".dbo.RHAC0030 INNER JOIN " & RHACLIBF_DB_NAME & ".dbo.RHAC0230 ON  ")
                .AppendLine("RHAC0030.KAIHATSU_FUGO = RHAC0230.KAIHATSU_FUGO AND ")
                .AppendLine("RHAC0030.SOBI_KAITEI_NO = RHAC0230.SOBI_KAITEI_NO ")
                .AppendLine("INNER JOIN " & RHACLIBF_DB_NAME & ".dbo.RHAC0540 ON  ")
                .AppendLine("RHAC0230.KAIHATSU_FUGO = RHAC0540.KAIHATSU_FUGO AND ")
                .AppendLine("RHAC0230.SOBI_KAITEI_NO = RHAC0540.SOBI_KAITEI_NO AND ")
                .AppendLine("RHAC0230.KATASHIKI_SCD_7 = RHAC0540.KATASHIKI_SCD_7 ")
                .AppendLine("WHERE RHAC0030.KAIHATSU_FUGO = '" & KaihatsuFugo & "' AND RHAC0030.SHIYOSHO_SEQNO='" & ShiyouJyouhouNo & "' ")
                '車型
                If StringUtil.IsNotEmpty(syagata) Then
                    .AppendLine(" AND RHAC0230.BODY_KIHON_KATA = '" & syagata & "' ")
                End If
                'ｸﾞﾚｰﾄﾞ
                If StringUtil.IsNotEmpty(Grade) Then
                    .AppendLine(" AND RHAC0230.GRADE_CODE = '" & Grade & "' ")
                End If
                ''仕向け
                'If StringUtil.IsNotEmpty(Shimuke) Then
                '    .AppendLine(" AND RHAC0230.SHIMUKE_DAI_KBN = '" & Shimuke & "' ")
                'End If
                'If StringUtil.IsNotEmpty(Shimuke) Then
                '    If StringUtil.Equals(Shimuke, HOYOU_SHIMUKECHI_KOKUNAI) Then
                '        .AppendLine(" AND RHAC0230.SHIMUKE_DAI_KBN = '" & HOYOU_SHIMUKECHI_KOKUNAI_CD & "' ")
                '    End If
                '    If StringUtil.Equals(Shimuke, HOYOU_SHIMUKECHI_HOKUBEI) Then
                '        .AppendLine(" AND RHAC0230.SHIMUKE_DAI_KBN = '" & HOYOU_SHIMUKECHI_HOKUBEI_CD & "' ")
                '    End If
                '    If StringUtil.Equals(Shimuke, HOYOU_SHIMUKECHI_GOUSYU) Then
                '        .AppendLine(" AND RHAC0230.SHIMUKE_DAI_KBN = '" & HOYOU_SHIMUKECHI_GOUSYU_CD & "' ")
                '    End If
                '    If StringUtil.Equals(Shimuke, HOYOU_SHIMUKECHI_OHSYU_MIGI) Then
                '        .AppendLine(" AND RHAC0230.SHIMUKE_DAI_KBN = '" & HOYOU_SHIMUKECHI_OHSYU_MIGI_CD & "' ")
                '    End If
                '    If StringUtil.Equals(Shimuke, HOYOU_SHIMUKECHI_CHUGOKU) Then
                '        .AppendLine(" AND RHAC0230.SHIMUKE_DAI_KBN = '" & HOYOU_SHIMUKECHI_CHUGOKU_CD & "' ")
                '    End If
                '    If StringUtil.Equals(Shimuke, HOYOU_SHIMUKECHI_OHSYU_HIDARI) Then
                '        .AppendLine(" AND RHAC0230.SHIMUKE_DAI_KBN = '" & HOYOU_SHIMUKECHI_OHSYU_HIDARI_CD & "' ")
                '    End If
                'End If
                'ﾊﾝﾄﾞﾙ
                If StringUtil.IsNotEmpty(Handle) Then
                    .AppendLine(" AND RHAC0230.HANDLE_POS = '" & Handle & "' ")
                End If
                '排気量
                If StringUtil.IsNotEmpty(Haikiryo) Then
                    .AppendLine(" AND RHAC0230.EG_HAIKIRYO = '" & Haikiryo & "' ")
                End If
                '形式
                If StringUtil.IsNotEmpty(Keishiki) Then
                    .AppendLine(" AND RHAC0230.DOBENKEI_CODE = '" & Keishiki & "' ")
                End If
                '過給器
                If StringUtil.IsNotEmpty(Kakyuki) Then
                    .AppendLine(" AND RHAC0230.KAKYUKI_CODE = '" & Kakyuki & "' ")
                End If
                '駆動方式
                If StringUtil.IsNotEmpty(Kudou) Then
                    .AppendLine(" AND RHAC0230.KUDO_HOSIKI = '" & Kudou & "' ")
                End If
                '変速機
                If StringUtil.IsNotEmpty(Mission) Then
                    .AppendLine(" AND RHAC0230.TRANS_MISSION = '" & Mission & "' ")
                End If
                '７桁型式
                If StringUtil.IsNotEmpty(Katashiki7) Then
                    .AppendLine(" AND RHAC0230.KATASHIKI_FUGO_7 = '" & Katashiki7 & "' ")
                End If
                '仕向地
                '   ブランクがあるので
                If StringUtil.IsNotEmpty(Shimukechi) Then
                    If Shimukechi = "国内" Then
                        .AppendLine(" AND RHAC0540.SHIMUKECHI_CODE = '' ")
                    Else
                        .AppendLine(" AND RHAC0540.SHIMUKECHI_CODE = '" & Shimukechi & "' ")
                    End If
                End If
                'ＯＰ
                If StringUtil.IsNotEmpty(OpCode) Then
                    .AppendLine(" AND RHAC0540.OP_CODE = '" & OpCode & "' ")
                End If
                .AppendLine("   GROUP BY RHAC0230.KUDO_HOSIKI ")
                .AppendLine("   ORDER BY RHAC0230.KUDO_HOSIKI ")
            End With
            Dim db As New EBomDbClient
            Return db.QueryForList(Of LabelValueVo)(sb.ToString)
        End Function

        ''' <summary>
        ''' T/M・変速機を取得
        ''' </summary>
        ''' <param name="KaihatsuFugo"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Function GetByTmHensokukiLabelValues(ByVal KaihatsuFugo As String, _
                                                ByVal ShiyouJyouhouNo As String, _
                                                ByVal syagata As String, _
                                                ByVal Grade As String, _
                                                ByVal Handle As String, _
                                                ByVal Haikiryo As String, _
                                                ByVal Keishiki As String, _
                                                ByVal Kakyuki As String, _
                                                ByVal Kudou As String, _
                                                ByVal Mission As String, _
                                                ByVal Katashiki7 As String, _
                                                ByVal Shimukechi As String, _
                                                ByVal OpCode As String) As List(Of LabelValueVo) Implements ShiyouJyouhouDao.GetByTmHensokukiLabelValues
            Dim sb As New StringBuilder
            With sb
                .Remove(0, .Length)
                .AppendLine("SELECT  ")
                .AppendLine("    RHAC0230.TRANS_MISSION AS LABEL, ")
                .AppendLine("    RHAC0230.TRANS_MISSION AS VALUE ")
                .AppendLine("FROM ")
                .AppendLine(RHACLIBF_DB_NAME & ".dbo.RHAC0030 INNER JOIN " & RHACLIBF_DB_NAME & ".dbo.RHAC0230 ON  ")
                .AppendLine("RHAC0030.KAIHATSU_FUGO = RHAC0230.KAIHATSU_FUGO AND ")
                .AppendLine("RHAC0030.SOBI_KAITEI_NO = RHAC0230.SOBI_KAITEI_NO ")
                .AppendLine("INNER JOIN " & RHACLIBF_DB_NAME & ".dbo.RHAC0540 ON  ")
                .AppendLine("RHAC0230.KAIHATSU_FUGO = RHAC0540.KAIHATSU_FUGO AND ")
                .AppendLine("RHAC0230.SOBI_KAITEI_NO = RHAC0540.SOBI_KAITEI_NO AND ")
                .AppendLine("RHAC0230.KATASHIKI_SCD_7 = RHAC0540.KATASHIKI_SCD_7 ")
                .AppendLine("WHERE RHAC0030.KAIHATSU_FUGO = '" & KaihatsuFugo & "' AND RHAC0030.SHIYOSHO_SEQNO='" & ShiyouJyouhouNo & "' ")
                '車型
                If StringUtil.IsNotEmpty(syagata) Then
                    .AppendLine(" AND RHAC0230.BODY_KIHON_KATA = '" & syagata & "' ")
                End If
                'ｸﾞﾚｰﾄﾞ
                If StringUtil.IsNotEmpty(Grade) Then
                    .AppendLine(" AND RHAC0230.GRADE_CODE = '" & Grade & "' ")
                End If
                ''仕向け
                'If StringUtil.IsNotEmpty(Shimuke) Then
                '    .AppendLine(" AND RHAC0230.SHIMUKE_DAI_KBN = '" & Shimuke & "' ")
                'End If
                'If StringUtil.IsNotEmpty(Shimuke) Then
                '    If StringUtil.Equals(Shimuke, HOYOU_SHIMUKECHI_KOKUNAI) Then
                '        .AppendLine(" AND RHAC0230.SHIMUKE_DAI_KBN = '" & HOYOU_SHIMUKECHI_KOKUNAI_CD & "' ")
                '    End If
                '    If StringUtil.Equals(Shimuke, HOYOU_SHIMUKECHI_HOKUBEI) Then
                '        .AppendLine(" AND RHAC0230.SHIMUKE_DAI_KBN = '" & HOYOU_SHIMUKECHI_HOKUBEI_CD & "' ")
                '    End If
                '    If StringUtil.Equals(Shimuke, HOYOU_SHIMUKECHI_GOUSYU) Then
                '        .AppendLine(" AND RHAC0230.SHIMUKE_DAI_KBN = '" & HOYOU_SHIMUKECHI_GOUSYU_CD & "' ")
                '    End If
                '    If StringUtil.Equals(Shimuke, HOYOU_SHIMUKECHI_OHSYU_MIGI) Then
                '        .AppendLine(" AND RHAC0230.SHIMUKE_DAI_KBN = '" & HOYOU_SHIMUKECHI_OHSYU_MIGI_CD & "' ")
                '    End If
                '    If StringUtil.Equals(Shimuke, HOYOU_SHIMUKECHI_CHUGOKU) Then
                '        .AppendLine(" AND RHAC0230.SHIMUKE_DAI_KBN = '" & HOYOU_SHIMUKECHI_CHUGOKU_CD & "' ")
                '    End If
                '    If StringUtil.Equals(Shimuke, HOYOU_SHIMUKECHI_OHSYU_HIDARI) Then
                '        .AppendLine(" AND RHAC0230.SHIMUKE_DAI_KBN = '" & HOYOU_SHIMUKECHI_OHSYU_HIDARI_CD & "' ")
                '    End If
                'End If
                'ﾊﾝﾄﾞﾙ
                If StringUtil.IsNotEmpty(Handle) Then
                    .AppendLine(" AND RHAC0230.HANDLE_POS = '" & Handle & "' ")
                End If
                '排気量
                If StringUtil.IsNotEmpty(Haikiryo) Then
                    .AppendLine(" AND RHAC0230.EG_HAIKIRYO = '" & Haikiryo & "' ")
                End If
                '形式
                If StringUtil.IsNotEmpty(Keishiki) Then
                    .AppendLine(" AND RHAC0230.DOBENKEI_CODE = '" & Keishiki & "' ")
                End If
                '過給器
                If StringUtil.IsNotEmpty(Kakyuki) Then
                    .AppendLine(" AND RHAC0230.KAKYUKI_CODE = '" & Kakyuki & "' ")
                End If
                '駆動方式
                If StringUtil.IsNotEmpty(Kudou) Then
                    .AppendLine(" AND RHAC0230.KUDO_HOSIKI = '" & Kudou & "' ")
                End If
                '変速機
                If StringUtil.IsNotEmpty(Mission) Then
                    .AppendLine(" AND RHAC0230.TRANS_MISSION = '" & Mission & "' ")
                End If
                '７桁型式
                If StringUtil.IsNotEmpty(Katashiki7) Then
                    .AppendLine(" AND RHAC0230.KATASHIKI_FUGO_7 = '" & Katashiki7 & "' ")
                End If
                '仕向地
                '   ブランクがあるので
                If StringUtil.IsNotEmpty(Shimukechi) Then
                    If Shimukechi = "国内" Then
                        .AppendLine(" AND RHAC0540.SHIMUKECHI_CODE = '' ")
                    Else
                        .AppendLine(" AND RHAC0540.SHIMUKECHI_CODE = '" & Shimukechi & "' ")
                    End If
                End If
                'ＯＰ
                If StringUtil.IsNotEmpty(OpCode) Then
                    .AppendLine(" AND RHAC0540.OP_CODE = '" & OpCode & "' ")
                End If
                .AppendLine("   GROUP BY RHAC0230.TRANS_MISSION ")
                .AppendLine("   ORDER BY RHAC0230.TRANS_MISSION ")
            End With
            Dim db As New EBomDbClient
            Return db.QueryForList(Of LabelValueVo)(sb.ToString)
        End Function

        ''' <summary>
        ''' ７桁型式を取得
        ''' </summary>
        ''' <param name="KaihatsuFugo"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Function GetByKatashiki7LabelValues(ByVal KaihatsuFugo As String, _
                                                ByVal ShiyouJyouhouNo As String, _
                                                ByVal syagata As String, _
                                                ByVal Grade As String, _
                                                ByVal Handle As String, _
                                                ByVal Haikiryo As String, _
                                                ByVal Keishiki As String, _
                                                ByVal Kakyuki As String, _
                                                ByVal Kudou As String, _
                                                ByVal Mission As String, _
                                                ByVal Katashiki7 As String, _
                                                ByVal Shimukechi As String, _
                                                ByVal OpCode As String) As List(Of LabelValueVo) Implements ShiyouJyouhouDao.GetByKatashiki7LabelValues
            Dim sb As New StringBuilder
            With sb
                .Remove(0, .Length)
                .AppendLine("SELECT  ")
                .AppendLine("    RHAC0230.KATASHIKI_FUGO_7 AS LABEL, ")
                .AppendLine("    RHAC0230.KATASHIKI_FUGO_7 AS VALUE ")
                .AppendLine("FROM ")
                .AppendLine(RHACLIBF_DB_NAME & ".dbo.RHAC0030 INNER JOIN " & RHACLIBF_DB_NAME & ".dbo.RHAC0230 ON  ")
                .AppendLine("RHAC0030.KAIHATSU_FUGO = RHAC0230.KAIHATSU_FUGO AND ")
                .AppendLine("RHAC0030.SOBI_KAITEI_NO = RHAC0230.SOBI_KAITEI_NO ")
                .AppendLine("INNER JOIN " & RHACLIBF_DB_NAME & ".dbo.RHAC0540 ON  ")
                .AppendLine("RHAC0230.KAIHATSU_FUGO = RHAC0540.KAIHATSU_FUGO AND ")
                .AppendLine("RHAC0230.SOBI_KAITEI_NO = RHAC0540.SOBI_KAITEI_NO AND ")
                .AppendLine("RHAC0230.KATASHIKI_SCD_7 = RHAC0540.KATASHIKI_SCD_7 ")
                .AppendLine("WHERE RHAC0030.KAIHATSU_FUGO = '" & KaihatsuFugo & "' AND RHAC0030.SHIYOSHO_SEQNO='" & ShiyouJyouhouNo & "' ")
                '車型
                If StringUtil.IsNotEmpty(syagata) Then
                    .AppendLine(" AND RHAC0230.BODY_KIHON_KATA = '" & syagata & "' ")
                End If
                'ｸﾞﾚｰﾄﾞ
                If StringUtil.IsNotEmpty(Grade) Then
                    .AppendLine(" AND RHAC0230.GRADE_CODE = '" & Grade & "' ")
                End If
                ''仕向け
                'If StringUtil.IsNotEmpty(Shimuke) Then
                '    .AppendLine(" AND RHAC0230.SHIMUKE_DAI_KBN = '" & Shimuke & "' ")
                'End If
                'If StringUtil.IsNotEmpty(Shimuke) Then
                '    If StringUtil.Equals(Shimuke, HOYOU_SHIMUKECHI_KOKUNAI) Then
                '        .AppendLine(" AND RHAC0230.SHIMUKE_DAI_KBN = '" & HOYOU_SHIMUKECHI_KOKUNAI_CD & "' ")
                '    End If
                '    If StringUtil.Equals(Shimuke, HOYOU_SHIMUKECHI_HOKUBEI) Then
                '        .AppendLine(" AND RHAC0230.SHIMUKE_DAI_KBN = '" & HOYOU_SHIMUKECHI_HOKUBEI_CD & "' ")
                '    End If
                '    If StringUtil.Equals(Shimuke, HOYOU_SHIMUKECHI_GOUSYU) Then
                '        .AppendLine(" AND RHAC0230.SHIMUKE_DAI_KBN = '" & HOYOU_SHIMUKECHI_GOUSYU_CD & "' ")
                '    End If
                '    If StringUtil.Equals(Shimuke, HOYOU_SHIMUKECHI_OHSYU_MIGI) Then
                '        .AppendLine(" AND RHAC0230.SHIMUKE_DAI_KBN = '" & HOYOU_SHIMUKECHI_OHSYU_MIGI_CD & "' ")
                '    End If
                '    If StringUtil.Equals(Shimuke, HOYOU_SHIMUKECHI_CHUGOKU) Then
                '        .AppendLine(" AND RHAC0230.SHIMUKE_DAI_KBN = '" & HOYOU_SHIMUKECHI_CHUGOKU_CD & "' ")
                '    End If
                '    If StringUtil.Equals(Shimuke, HOYOU_SHIMUKECHI_OHSYU_HIDARI) Then
                '        .AppendLine(" AND RHAC0230.SHIMUKE_DAI_KBN = '" & HOYOU_SHIMUKECHI_OHSYU_HIDARI_CD & "' ")
                '    End If
                'End If
                'ﾊﾝﾄﾞﾙ
                If StringUtil.IsNotEmpty(Handle) Then
                    .AppendLine(" AND RHAC0230.HANDLE_POS = '" & Handle & "' ")
                End If
                '排気量
                If StringUtil.IsNotEmpty(Haikiryo) Then
                    .AppendLine(" AND RHAC0230.EG_HAIKIRYO = '" & Haikiryo & "' ")
                End If
                '形式
                If StringUtil.IsNotEmpty(Keishiki) Then
                    .AppendLine(" AND RHAC0230.DOBENKEI_CODE = '" & Keishiki & "' ")
                End If
                '過給器
                If StringUtil.IsNotEmpty(Kakyuki) Then
                    .AppendLine(" AND RHAC0230.KAKYUKI_CODE = '" & Kakyuki & "' ")
                End If
                '駆動方式
                If StringUtil.IsNotEmpty(Kudou) Then
                    .AppendLine(" AND RHAC0230.KUDO_HOSIKI = '" & Kudou & "' ")
                End If
                '変速機
                If StringUtil.IsNotEmpty(Mission) Then
                    .AppendLine(" AND RHAC0230.TRANS_MISSION = '" & Mission & "' ")
                End If
                '７桁型式
                If StringUtil.IsNotEmpty(Katashiki7) Then
                    .AppendLine(" AND RHAC0230.KATASHIKI_FUGO_7 = '" & Katashiki7 & "' ")
                End If
                '仕向地
                '   ブランクがあるので
                If StringUtil.IsNotEmpty(Shimukechi) Then
                    If Shimukechi = "国内" Then
                        .AppendLine(" AND RHAC0540.SHIMUKECHI_CODE = '' ")
                    Else
                        .AppendLine(" AND RHAC0540.SHIMUKECHI_CODE = '" & Shimukechi & "' ")
                    End If
                End If
                'ＯＰ
                If StringUtil.IsNotEmpty(OpCode) Then
                    .AppendLine(" AND RHAC0540.OP_CODE = '" & OpCode & "' ")
                End If
                .AppendLine("   GROUP BY RHAC0230.KATASHIKI_FUGO_7 ")
                .AppendLine("   ORDER BY RHAC0230.KATASHIKI_FUGO_7 ")
            End With
            Dim db As New EBomDbClient
            Return db.QueryForList(Of LabelValueVo)(sb.ToString)
        End Function

        ''' <summary>
        ''' 仕向けコードを取得
        ''' </summary>
        ''' <param name="KaihatsuFugo"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Function GetByKataShimukeLabelValues(ByVal KaihatsuFugo As String, _
                                                ByVal ShiyouJyouhouNo As String, _
                                                ByVal syagata As String, _
                                                ByVal Grade As String, _
                                                ByVal Handle As String, _
                                                ByVal Haikiryo As String, _
                                                ByVal Keishiki As String, _
                                                ByVal Kakyuki As String, _
                                                ByVal Kudou As String, _
                                                ByVal Mission As String, _
                                                ByVal Katashiki7 As String, _
                                                ByVal Shimukechi As String, _
                                                ByVal OpCode As String) As List(Of LabelValueVo) Implements ShiyouJyouhouDao.GetByKataShimukeLabelValues
            Dim sb As New StringBuilder
            With sb
                .Remove(0, .Length)
                .AppendLine("SELECT  ")
                '.AppendLine("    RHAC0540.SHIMUKECHI_CODE AS LABEL, ")
                '.AppendLine("    RHAC0540.SHIMUKECHI_CODE AS VALUE ")
                .AppendLine(" CASE WHEN RHAC0540.SHIMUKECHI_CODE = ''  THEN '国内' ")
                .AppendLine("      WHEN RHAC0540.SHIMUKECHI_CODE <> ''  THEN RHAC0540.SHIMUKECHI_CODE ")
                .AppendLine(" END ")
                .AppendLine(" AS LABEL, ")
                .AppendLine(" CASE WHEN RHAC0540.SHIMUKECHI_CODE = ''  THEN '国内' ")
                .AppendLine("      WHEN RHAC0540.SHIMUKECHI_CODE <> ''  THEN RHAC0540.SHIMUKECHI_CODE ")
                .AppendLine(" END ")
                .AppendLine(" AS VALUE ")
                .AppendLine("FROM ")
                .AppendLine(RHACLIBF_DB_NAME & ".dbo.RHAC0030 INNER JOIN " & RHACLIBF_DB_NAME & ".dbo.RHAC0230 ON  ")
                .AppendLine("RHAC0030.KAIHATSU_FUGO = RHAC0230.KAIHATSU_FUGO AND ")
                .AppendLine("RHAC0030.SOBI_KAITEI_NO = RHAC0230.SOBI_KAITEI_NO ")
                .AppendLine("INNER JOIN " & RHACLIBF_DB_NAME & ".dbo.RHAC0540 ON  ")
                .AppendLine("RHAC0230.KAIHATSU_FUGO = RHAC0540.KAIHATSU_FUGO AND ")
                .AppendLine("RHAC0230.SOBI_KAITEI_NO = RHAC0540.SOBI_KAITEI_NO AND ")
                .AppendLine("RHAC0230.KATASHIKI_SCD_7 = RHAC0540.KATASHIKI_SCD_7 ")
                .AppendLine("WHERE RHAC0030.KAIHATSU_FUGO = '" & KaihatsuFugo & "' AND RHAC0030.SHIYOSHO_SEQNO='" & ShiyouJyouhouNo & "' ")
                '車型
                If StringUtil.IsNotEmpty(syagata) Then
                    .AppendLine(" AND RHAC0230.BODY_KIHON_KATA = '" & syagata & "' ")
                End If
                'ｸﾞﾚｰﾄﾞ
                If StringUtil.IsNotEmpty(Grade) Then
                    .AppendLine(" AND RHAC0230.GRADE_CODE = '" & Grade & "' ")
                End If
                ''仕向け
                'If StringUtil.IsNotEmpty(Shimuke) Then
                '    .AppendLine(" AND RHAC0230.SHIMUKE_DAI_KBN = '" & Shimuke & "' ")
                'End If
                'If StringUtil.IsNotEmpty(Shimuke) Then
                '    If StringUtil.Equals(Shimuke, HOYOU_SHIMUKECHI_KOKUNAI) Then
                '        .AppendLine(" AND RHAC0230.SHIMUKE_DAI_KBN = '" & HOYOU_SHIMUKECHI_KOKUNAI_CD & "' ")
                '    End If
                '    If StringUtil.Equals(Shimuke, HOYOU_SHIMUKECHI_HOKUBEI) Then
                '        .AppendLine(" AND RHAC0230.SHIMUKE_DAI_KBN = '" & HOYOU_SHIMUKECHI_HOKUBEI_CD & "' ")
                '    End If
                '    If StringUtil.Equals(Shimuke, HOYOU_SHIMUKECHI_GOUSYU) Then
                '        .AppendLine(" AND RHAC0230.SHIMUKE_DAI_KBN = '" & HOYOU_SHIMUKECHI_GOUSYU_CD & "' ")
                '    End If
                '    If StringUtil.Equals(Shimuke, HOYOU_SHIMUKECHI_OHSYU_MIGI) Then
                '        .AppendLine(" AND RHAC0230.SHIMUKE_DAI_KBN = '" & HOYOU_SHIMUKECHI_OHSYU_MIGI_CD & "' ")
                '    End If
                '    If StringUtil.Equals(Shimuke, HOYOU_SHIMUKECHI_CHUGOKU) Then
                '        .AppendLine(" AND RHAC0230.SHIMUKE_DAI_KBN = '" & HOYOU_SHIMUKECHI_CHUGOKU_CD & "' ")
                '    End If
                '    If StringUtil.Equals(Shimuke, HOYOU_SHIMUKECHI_OHSYU_HIDARI) Then
                '        .AppendLine(" AND RHAC0230.SHIMUKE_DAI_KBN = '" & HOYOU_SHIMUKECHI_OHSYU_HIDARI_CD & "' ")
                '    End If
                'End If
                'ﾊﾝﾄﾞﾙ
                If StringUtil.IsNotEmpty(Handle) Then
                    .AppendLine(" AND RHAC0230.HANDLE_POS = '" & Handle & "' ")
                End If
                '排気量
                If StringUtil.IsNotEmpty(Haikiryo) Then
                    .AppendLine(" AND RHAC0230.EG_HAIKIRYO = '" & Haikiryo & "' ")
                End If
                '形式
                If StringUtil.IsNotEmpty(Keishiki) Then
                    .AppendLine(" AND RHAC0230.DOBENKEI_CODE = '" & Keishiki & "' ")
                End If
                '過給器
                If StringUtil.IsNotEmpty(Kakyuki) Then
                    .AppendLine(" AND RHAC0230.KAKYUKI_CODE = '" & Kakyuki & "' ")
                End If
                '駆動方式
                If StringUtil.IsNotEmpty(Kudou) Then
                    .AppendLine(" AND RHAC0230.KUDO_HOSIKI = '" & Kudou & "' ")
                End If
                '変速機
                If StringUtil.IsNotEmpty(Mission) Then
                    .AppendLine(" AND RHAC0230.TRANS_MISSION = '" & Mission & "' ")
                End If
                '７桁型式
                If StringUtil.IsNotEmpty(Katashiki7) Then
                    .AppendLine(" AND RHAC0230.KATASHIKI_FUGO_7 = '" & Katashiki7 & "' ")
                End If
                '仕向地
                '   ブランクがあるので
                If StringUtil.IsNotEmpty(Shimukechi) Then
                    If Shimukechi = "国内" Then
                        .AppendLine(" AND RHAC0540.SHIMUKECHI_CODE = '' ")
                    Else
                        .AppendLine(" AND RHAC0540.SHIMUKECHI_CODE = '" & Shimukechi & "' ")
                    End If
                End If
                'ＯＰ
                If StringUtil.IsNotEmpty(OpCode) Then
                    .AppendLine(" AND RHAC0540.OP_CODE = '" & OpCode & "' ")
                End If
                .AppendLine("   GROUP BY RHAC0540.SHIMUKECHI_CODE ")
                .AppendLine("   ORDER BY RHAC0540.SHIMUKECHI_CODE ")
            End With
            Dim db As New EBomDbClient
            Return db.QueryForList(Of LabelValueVo)(sb.ToString)
        End Function

        ''' <summary>
        ''' 仕向けコード(HELP)を取得
        ''' </summary>
        ''' <param name="KaihatsuFugo"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Function GetByKataShimukeHelpValues(ByVal KaihatsuFugo As String, _
                                                ByVal ShiyouJyouhouNo As String, _
                                                ByVal syagata As String, _
                                                ByVal Grade As String, _
                                                ByVal Handle As String, _
                                                ByVal Haikiryo As String, _
                                                ByVal Keishiki As String, _
                                                ByVal Kakyuki As String, _
                                                ByVal Kudou As String, _
                                                ByVal Mission As String, _
                                                ByVal Katashiki7 As String, _
                                                ByVal Shimukechi As String, _
                                                ByVal OpCode As String) As List(Of LabelValueVo) Implements ShiyouJyouhouDao.GetByKataShimukeHelpValues
            Dim sb As New StringBuilder
            With sb
                .Remove(0, .Length)
                .AppendLine("SELECT")
                .AppendLine(" CASE")
                .AppendLine("  WHEN RHAC0230.SHIMUKE_DAI_KBN = '0' THEN '国内'")
                .AppendLine("  WHEN RHAC0230.SHIMUKE_DAI_KBN = '1' THEN '北米'")
                .AppendLine("  WHEN RHAC0230.SHIMUKE_DAI_KBN = '2' THEN '欧州一般右'")
                '.AppendLine("  WHEN RHAC0230.SHIMUKE_DAI_KBN = '2' AND RHAC0540.SHIMUKECHI_CODE = 'KA' THEN '豪州'")
                '.AppendLine("  WHEN RHAC0230.SHIMUKE_DAI_KBN = '2' AND RHAC0540.SHIMUKECHI_CODE <> 'KA' THEN '豪州'")
                .AppendLine("  WHEN RHAC0230.SHIMUKE_DAI_KBN = '3' AND (RHAC0540.SHIMUKECHI_CODE = 'EH' OR RHAC0540.SHIMUKECHI_CODE = 'ET') THEN '中国'")
                .AppendLine("  WHEN RHAC0230.SHIMUKE_DAI_KBN = '3' AND RHAC0540.SHIMUKECHI_CODE <> 'EH' AND RHAC0540.SHIMUKECHI_CODE <> 'ET' THEN '欧州一般左'")
                '.AppendLine(" END AS SHIMUKE_DAI_KBN,")
                .AppendLine(" END AS VALUE,")
                .AppendLine(" RHAC0540.SHIMUKECHI_CODE")
                .AppendLine(" AS LABEL ")

                .AppendLine("FROM ")
                .AppendLine(RHACLIBF_DB_NAME & ".dbo.RHAC0030 INNER JOIN " & RHACLIBF_DB_NAME & ".dbo.RHAC0230 ON  ")
                .AppendLine("RHAC0030.KAIHATSU_FUGO = RHAC0230.KAIHATSU_FUGO AND ")
                .AppendLine("RHAC0030.SOBI_KAITEI_NO = RHAC0230.SOBI_KAITEI_NO ")
                .AppendLine("INNER JOIN " & RHACLIBF_DB_NAME & ".dbo.RHAC0540 ON  ")
                .AppendLine("RHAC0230.KAIHATSU_FUGO = RHAC0540.KAIHATSU_FUGO AND ")
                .AppendLine("RHAC0230.SOBI_KAITEI_NO = RHAC0540.SOBI_KAITEI_NO AND ")
                .AppendLine("RHAC0230.KATASHIKI_SCD_7 = RHAC0540.KATASHIKI_SCD_7 ")
                .AppendLine("WHERE RHAC0030.KAIHATSU_FUGO = '" & KaihatsuFugo & "' AND RHAC0030.SHIYOSHO_SEQNO='" & ShiyouJyouhouNo & "' ")
                '車型
                If StringUtil.IsNotEmpty(syagata) Then
                    .AppendLine(" AND RHAC0230.BODY_KIHON_KATA = '" & syagata & "' ")
                End If
                'ｸﾞﾚｰﾄﾞ
                If StringUtil.IsNotEmpty(Grade) Then
                    .AppendLine(" AND RHAC0230.GRADE_CODE = '" & Grade & "' ")
                End If
                ''仕向け
                'If StringUtil.IsNotEmpty(Shimuke) Then
                '    .AppendLine(" AND RHAC0230.SHIMUKE_DAI_KBN = '" & Shimuke & "' ")
                'End If
                'If StringUtil.IsNotEmpty(Shimuke) Then
                '    If StringUtil.Equals(Shimuke, HOYOU_SHIMUKECHI_KOKUNAI) Then
                '        .AppendLine(" AND RHAC0230.SHIMUKE_DAI_KBN = '" & HOYOU_SHIMUKECHI_KOKUNAI_CD & "' ")
                '    End If
                '    If StringUtil.Equals(Shimuke, HOYOU_SHIMUKECHI_HOKUBEI) Then
                '        .AppendLine(" AND RHAC0230.SHIMUKE_DAI_KBN = '" & HOYOU_SHIMUKECHI_HOKUBEI_CD & "' ")
                '    End If
                '    If StringUtil.Equals(Shimuke, HOYOU_SHIMUKECHI_GOUSYU) Then
                '        .AppendLine(" AND RHAC0230.SHIMUKE_DAI_KBN = '" & HOYOU_SHIMUKECHI_GOUSYU_CD & "' ")
                '    End If
                '    If StringUtil.Equals(Shimuke, HOYOU_SHIMUKECHI_OHSYU_MIGI) Then
                '        .AppendLine(" AND RHAC0230.SHIMUKE_DAI_KBN = '" & HOYOU_SHIMUKECHI_OHSYU_MIGI_CD & "' ")
                '    End If
                '    If StringUtil.Equals(Shimuke, HOYOU_SHIMUKECHI_CHUGOKU) Then
                '        .AppendLine(" AND RHAC0230.SHIMUKE_DAI_KBN = '" & HOYOU_SHIMUKECHI_CHUGOKU_CD & "' ")
                '    End If
                '    If StringUtil.Equals(Shimuke, HOYOU_SHIMUKECHI_OHSYU_HIDARI) Then
                '        .AppendLine(" AND RHAC0230.SHIMUKE_DAI_KBN = '" & HOYOU_SHIMUKECHI_OHSYU_HIDARI_CD & "' ")
                '    End If
                'End If
                'ﾊﾝﾄﾞﾙ
                If StringUtil.IsNotEmpty(Handle) Then
                    .AppendLine(" AND RHAC0230.HANDLE_POS = '" & Handle & "' ")
                End If
                '排気量
                If StringUtil.IsNotEmpty(Haikiryo) Then
                    .AppendLine(" AND RHAC0230.EG_HAIKIRYO = '" & Haikiryo & "' ")
                End If
                '形式
                If StringUtil.IsNotEmpty(Keishiki) Then
                    .AppendLine(" AND RHAC0230.DOBENKEI_CODE = '" & Keishiki & "' ")
                End If
                '過給器
                If StringUtil.IsNotEmpty(Kakyuki) Then
                    .AppendLine(" AND RHAC0230.KAKYUKI_CODE = '" & Kakyuki & "' ")
                End If
                '駆動方式
                If StringUtil.IsNotEmpty(Kudou) Then
                    .AppendLine(" AND RHAC0230.KUDO_HOSIKI = '" & Kudou & "' ")
                End If
                '変速機
                If StringUtil.IsNotEmpty(Mission) Then
                    .AppendLine(" AND RHAC0230.TRANS_MISSION = '" & Mission & "' ")
                End If
                '７桁型式
                If StringUtil.IsNotEmpty(Katashiki7) Then
                    .AppendLine(" AND RHAC0230.KATASHIKI_FUGO_7 = '" & Katashiki7 & "' ")
                End If
                '仕向地
                '   ブランクがあるので
                If StringUtil.IsNotEmpty(Shimukechi) Then
                    If Shimukechi = "国内" Then
                        .AppendLine(" AND RHAC0540.SHIMUKECHI_CODE = '' ")
                    Else
                        .AppendLine(" AND RHAC0540.SHIMUKECHI_CODE = '" & Shimukechi & "' ")
                    End If
                End If
                'ＯＰ
                If StringUtil.IsNotEmpty(OpCode) Then
                    .AppendLine(" AND RHAC0540.OP_CODE = '" & OpCode & "' ")
                End If
                .AppendLine("   GROUP BY RHAC0230.SHIMUKE_DAI_KBN, RHAC0540.SHIMUKECHI_CODE ")
                .AppendLine("   ORDER BY RHAC0230.SHIMUKE_DAI_KBN, RHAC0540.SHIMUKECHI_CODE ")
            End With
            Dim db As New EBomDbClient
            Return db.QueryForList(Of LabelValueVo)(sb.ToString)
        End Function

        ''' <summary>
        ''' OPを取得
        ''' </summary>
        ''' <param name="KaihatsuFugo"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Function GetByKataOpLabelValues(ByVal KaihatsuFugo As String, _
                                                ByVal ShiyouJyouhouNo As String, _
                                                ByVal syagata As String, _
                                                ByVal Grade As String, _
                                                ByVal Handle As String, _
                                                ByVal Haikiryo As String, _
                                                ByVal Keishiki As String, _
                                                ByVal Kakyuki As String, _
                                                ByVal Kudou As String, _
                                                ByVal Mission As String, _
                                                ByVal Katashiki7 As String, _
                                                ByVal Shimukechi As String, _
                                                ByVal OpCode As String) As List(Of LabelValueVo) Implements ShiyouJyouhouDao.GetByKataOpLabelValues
            Dim sb As New StringBuilder
            With sb
                .Remove(0, .Length)
                .AppendLine("SELECT  ")
                .AppendLine("    RHAC0540.OP_CODE AS LABEL, ")
                .AppendLine("    RHAC0540.OP_CODE AS VALUE ")
                .AppendLine("FROM ")
                .AppendLine(RHACLIBF_DB_NAME & ".dbo.RHAC0030 INNER JOIN " & RHACLIBF_DB_NAME & ".dbo.RHAC0230 ON  ")
                .AppendLine("RHAC0030.KAIHATSU_FUGO = RHAC0230.KAIHATSU_FUGO AND ")
                .AppendLine("RHAC0030.SOBI_KAITEI_NO = RHAC0230.SOBI_KAITEI_NO ")
                .AppendLine("INNER JOIN " & RHACLIBF_DB_NAME & ".dbo.RHAC0540 ON  ")
                .AppendLine("RHAC0230.KAIHATSU_FUGO = RHAC0540.KAIHATSU_FUGO AND ")
                .AppendLine("RHAC0230.SOBI_KAITEI_NO = RHAC0540.SOBI_KAITEI_NO AND ")
                .AppendLine("RHAC0230.KATASHIKI_SCD_7 = RHAC0540.KATASHIKI_SCD_7 ")
                .AppendLine("WHERE RHAC0030.KAIHATSU_FUGO = '" & KaihatsuFugo & "' AND RHAC0030.SHIYOSHO_SEQNO='" & ShiyouJyouhouNo & "' ")
                '車型
                If StringUtil.IsNotEmpty(syagata) Then
                    .AppendLine(" AND RHAC0230.BODY_KIHON_KATA = '" & syagata & "' ")
                End If
                'ｸﾞﾚｰﾄﾞ
                If StringUtil.IsNotEmpty(Grade) Then
                    .AppendLine(" AND RHAC0230.GRADE_CODE = '" & Grade & "' ")
                End If
                ''仕向け
                'If StringUtil.IsNotEmpty(Shimuke) Then
                '    .AppendLine(" AND RHAC0230.SHIMUKE_DAI_KBN = '" & Shimuke & "' ")
                'End If
                'If StringUtil.IsNotEmpty(Shimuke) Then
                '    If StringUtil.Equals(Shimuke, HOYOU_SHIMUKECHI_KOKUNAI) Then
                '        .AppendLine(" AND RHAC0230.SHIMUKE_DAI_KBN = '" & HOYOU_SHIMUKECHI_KOKUNAI_CD & "' ")
                '    End If
                '    If StringUtil.Equals(Shimuke, HOYOU_SHIMUKECHI_HOKUBEI) Then
                '        .AppendLine(" AND RHAC0230.SHIMUKE_DAI_KBN = '" & HOYOU_SHIMUKECHI_HOKUBEI_CD & "' ")
                '    End If
                '    If StringUtil.Equals(Shimuke, HOYOU_SHIMUKECHI_GOUSYU) Then
                '        .AppendLine(" AND RHAC0230.SHIMUKE_DAI_KBN = '" & HOYOU_SHIMUKECHI_GOUSYU_CD & "' ")
                '    End If
                '    If StringUtil.Equals(Shimuke, HOYOU_SHIMUKECHI_OHSYU_MIGI) Then
                '        .AppendLine(" AND RHAC0230.SHIMUKE_DAI_KBN = '" & HOYOU_SHIMUKECHI_OHSYU_MIGI_CD & "' ")
                '    End If
                '    If StringUtil.Equals(Shimuke, HOYOU_SHIMUKECHI_CHUGOKU) Then
                '        .AppendLine(" AND RHAC0230.SHIMUKE_DAI_KBN = '" & HOYOU_SHIMUKECHI_CHUGOKU_CD & "' ")
                '    End If
                '    If StringUtil.Equals(Shimuke, HOYOU_SHIMUKECHI_OHSYU_HIDARI) Then
                '        .AppendLine(" AND RHAC0230.SHIMUKE_DAI_KBN = '" & HOYOU_SHIMUKECHI_OHSYU_HIDARI_CD & "' ")
                '    End If
                'End If
                'ﾊﾝﾄﾞﾙ
                If StringUtil.IsNotEmpty(Handle) Then
                    .AppendLine(" AND RHAC0230.HANDLE_POS = '" & Handle & "' ")
                End If
                '排気量
                If StringUtil.IsNotEmpty(Haikiryo) Then
                    .AppendLine(" AND RHAC0230.EG_HAIKIRYO = '" & Haikiryo & "' ")
                End If
                '形式
                If StringUtil.IsNotEmpty(Keishiki) Then
                    .AppendLine(" AND RHAC0230.DOBENKEI_CODE = '" & Keishiki & "' ")
                End If
                '過給器
                If StringUtil.IsNotEmpty(Kakyuki) Then
                    .AppendLine(" AND RHAC0230.KAKYUKI_CODE = '" & Kakyuki & "' ")
                End If
                '駆動方式
                If StringUtil.IsNotEmpty(Kudou) Then
                    .AppendLine(" AND RHAC0230.KUDO_HOSIKI = '" & Kudou & "' ")
                End If
                '変速機
                If StringUtil.IsNotEmpty(Mission) Then
                    .AppendLine(" AND RHAC0230.TRANS_MISSION = '" & Mission & "' ")
                End If
                '７桁型式
                If StringUtil.IsNotEmpty(Katashiki7) Then
                    .AppendLine(" AND RHAC0230.KATASHIKI_FUGO_7 = '" & Katashiki7 & "' ")
                End If
                '仕向地
                '   ブランクがあるので
                If StringUtil.IsNotEmpty(Shimukechi) Then
                    If Shimukechi = "国内" Then
                        .AppendLine(" AND RHAC0540.SHIMUKECHI_CODE = '' ")
                    Else
                        .AppendLine(" AND RHAC0540.SHIMUKECHI_CODE = '" & Shimukechi & "' ")
                    End If
                End If
                '変更可能に修正（もう１か所あり）
                'ＯＰ
                'If StringUtil.IsNotEmpty(OpCode) Then
                '    .AppendLine(" AND RHAC0540.OP_CODE = '" & OpCode & "' ")
                'End If
                .AppendLine("   GROUP BY RHAC0540.OP_CODE ")
                .AppendLine("   ORDER BY RHAC0540.OP_CODE ")
            End With
            Dim db As New EBomDbClient
            Return db.QueryForList(Of LabelValueVo)(sb.ToString)
        End Function

        ''' <summary>
        ''' ＯＰ項目列を取得
        ''' </summary>
        ''' <param name="KaihatsuFugo"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Function GetByOpKoumokuRetuValues(ByVal KaihatsuFugo As String) As List(Of OpListVo) Implements ShiyouJyouhouDao.GetByOpKoumokuRetuValues
            Dim sb As New StringBuilder
            With sb
                .Remove(0, .Length)
                .AppendLine("SELECT  ")
                .AppendLine("    DISTINCT RHAC0340.KAIHATSU_FUGO,  ")
                .AppendLine("    RHAC0340.OP_SPEC_CODE, ")
                .AppendLine("    RHAC0340.OP_SPEC_NAME, ")
                .AppendLine("    RHAC0340.HYOJIJUN_NO ")
                .AppendLine("FROM ")
                .AppendLine(RHACLIBF_DB_NAME & ".dbo.RHAC0340 INNER JOIN ")
                .AppendLine(" (" & RHACLIBF_DB_NAME & ".dbo.RHAC0810 INNER JOIN " & RHACLIBF_DB_NAME & ".dbo.RHAC0890 ON  ")
                .AppendLine("RHAC0810.KAIHATSU_FUGO = RHAC0890.KAIHATSU_FUGO AND  ")
                .AppendLine("RHAC0810.KANRENKUMI_CATE_NO = RHAC0890.KANRENKUMI_CATE_NO AND   ")
                .AppendLine("RHAC0810.KANRENKUMI_GROUP_NO = RHAC0890.KANRENKUMI_GROUP_NO AND  ")
                .AppendLine("RHAC0810.KAITEI_NO_KKUMI = RHAC0890.KAITEI_NO_KKUMI) ON  ")
                .AppendLine("RHAC0340.OP_SPEC_CODE = RHAC0890.OP_SPEC_CODE AND ")
                .AppendLine("RHAC0340.KAIHATSU_FUGO = RHAC0890.KAIHATSU_FUGO ")
                .AppendLine(" WHERE RHAC0340.KAIHATSU_FUGO = '" & KaihatsuFugo & "' ")
                .AppendLine(" ORDER BY RHAC0340.HYOJIJUN_NO ")
            End With
            Dim db As New EBomDbClient
            Return db.QueryForList(Of OpListVo)(sb.ToString)
        End Function

        ''' <summary>ＯＰスペック情報を取得</summary>
        ''' <param name="KaihatsuFugo"></param>
        ''' <param name="ShiyouJyouhouNo"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Function GetByOpSpecValues(ByVal KaihatsuFugo As String, ByVal ShiyouJyouhouNo As String) As List(Of OpSpecListVo) _
                                                                                    Implements ShiyouJyouhouDao.GetByOpSpecValues
            Dim sb As New StringBuilder
            With sb
                .Remove(0, .Length)
                .AppendLine("SELECT  ")
                .AppendLine(" RHAC0400.KAIHATSU_FUGO, ")
                .AppendLine(" RHAC0400.LIST_CODE, ")
                .AppendLine(" RHAC0400.OPCD_KETAICHI, ")
                .AppendLine(" RHAC0400.OP_KIGO, ")
                .AppendLine(" RHAC0400.OP_SPEC_CODE  ")
                .AppendLine("FROM ")
                .AppendLine(RHACLIBF_DB_NAME & ".dbo.RHAC0030 INNER JOIN ")
                .AppendLine(RHACLIBF_DB_NAME & ".dbo.RHAC1080 ON ")
                .AppendLine(" RHAC0030.KAIHATSU_FUGO = RHAC1080.KAIHATSU_FUGO AND ")
                .AppendLine(" RHAC0030.LINE_OP_KAITEI_NO = RHAC1080.LINE_OP_KAITEI_NO ")
                .AppendLine(" INNER JOIN RHAC0400 ON ")
                .AppendLine(" RHAC1080.KAIHATSU_FUGO = RHAC0400.KAIHATSU_FUGO And ")
                .AppendLine(" RHAC1080.OP_KAITEI_NO = RHAC0400.OP_KAITEI_NO ")
                .AppendLine("WHERE ")
                .AppendLine(" RHAC0030.KAIHATSU_FUGO = '" & KaihatsuFugo & "' AND ")
                .AppendLine(" RHAC0030.SHIYOSHO_SEQNO = '" & ShiyouJyouhouNo & "' ")
            End With
            Dim db As New EBomDbClient
            Return db.QueryForList(Of OpSpecListVo)(sb.ToString)

        End Function

        ''↓↓2014/07/31 Ⅰ.3.設計編集 ベース車改修専用化_ba) (TES)張 CHG BEGIN
        'Public Function GetByFinalHinbanValues(ByVal KaihatsuFugo As String, _
        '                                        ByVal BlockNo As String, _
        '                                        ByVal ShiyouJyouhouNo As String, _
        '                                        ByVal syagata As String, _
        '                                        ByVal Grade As String, _
        '                                        ByVal Handle As String, _
        '                                        ByVal Haikiryo As String, _
        '                                        ByVal Keishiki As String, _
        '                                        ByVal Kakyuki As String, _
        '                                        ByVal Kudou As String, _
        '                                        ByVal Mission As String, _
        '                                        ByVal Katashiki7 As String, _
        '                                        ByVal Shimukechi As String, _
        '                                        ByVal OpCode As String) As List(Of FinalHinbanListVo) Implements ShiyouJyouhouDao.GetByFinalHinbanValues
        ''' <summary>ファイナル品番を取得</summary>
        ''' <param name="KaihatsuFugo"></param>
        ''' <param name="blockNo"></param>
        ''' <param name="ShiyouJyouhouNo"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Function GetByFinalHinbanValues(ByVal KaihatsuFugo As String, _
                                                ByVal BlockNo As String, _
                                                ByVal fBuhinNo As String, _
                                                ByVal ShiyouJyouhouNo As String, _
                                                ByVal syagata As String, _
                                                ByVal Grade As String, _
                                                ByVal Handle As String, _
                                                ByVal Haikiryo As String, _
                                                ByVal Keishiki As String, _
                                                ByVal Kakyuki As String, _
                                                ByVal Kudou As String, _
                                                ByVal Mission As String, _
                                                ByVal Katashiki7 As String, _
                                                ByVal Shimukechi As String, _
                                                ByVal OpCode As String) As List(Of FinalHinbanListVo) Implements ShiyouJyouhouDao.GetByFinalHinbanValues
            ''↑↑2014/07/31 Ⅰ.3.設計編集 ベース車改修専用化_ba) (TES)張 CHG END
            Dim sb As New StringBuilder
            With sb
                .Remove(0, .Length)
                .AppendLine("SELECT  ")
                .AppendLine("    AL.KAIHATSU_FUGO, ")
                .AppendLine("    AL.BLOCK_NO, ")
                .AppendLine("    AL.F_BUHIN_NO, ")
                .AppendLine("    AL.BLOCK_NO, ")
                .AppendLine("    MAX(AL2.FUKA_NO) AS FUKA_NO ")
                .AppendLine("FROM ")
                .AppendLine(RHACLIBF_DB_NAME & ".dbo.RHAC2210 AL WITH (NOLOCK, NOWAIT, INDEX(PK_RHAC2210)) ")
                .AppendLine(" INNER JOIN " & RHACLIBF_DB_NAME & ".dbo.RHAC0030 SHIYO WITH (NOLOCK, NOWAIT) ")
                .AppendLine("    ON SHIYO.KAIHATSU_FUGO = AL.KAIHATSU_FUGO ")
                .AppendLine("      AND SHIYO.SHIYOSHO_SEQNO = '" & ShiyouJyouhouNo & "' ")
                .AppendLine(" LEFT OUTER JOIN " & RHACLIBF_DB_NAME & ".dbo.RHAC2220 AL2 WITH (NOLOCK, NOWAIT) ")
                .AppendLine("    ON AL2.KAIHATSU_FUGO = AL.KAIHATSU_FUGO ")
                .AppendLine("      AND AL2.KATASHIKI_SCD_7 = AL.KATASHIKI_SCD_7 ")
                .AppendLine("      AND AL2.COL_NO = AL.COL_NO ")
                .AppendLine("      AND AL2.BLOCK_NO = AL.BLOCK_NO ")
                .AppendLine("      AND AL2.F_BUHIN_NO = AL.F_BUHIN_NO ")
                .AppendLine("      AND AL2.TOP_COLOR_KAITEI_NO = (SELECT MAX(TOP_COLOR_KAITEI_NO) ")
                .AppendLine(" FROM " & RHACLIBF_DB_NAME & ".dbo.RHAC2220 WITH (NOLOCK, NOWAIT) ")
                .AppendLine("       WHERE(KAIHATSU_FUGO = AL2.KAIHATSU_FUGO) ")
                .AppendLine("         AND KATASHIKI_SCD_7 = AL2.KATASHIKI_SCD_7 ")
                .AppendLine("         AND COL_NO = AL2.COL_NO ")
                .AppendLine("         AND BLOCK_NO = AL2.BLOCK_NO ")
                .AppendLine("         AND F_BUHIN_NO = AL2.F_BUHIN_NO ")
                .AppendLine("         AND COLOR_CODE = AL2.COLOR_CODE ) ")
                .AppendLine(" LEFT JOIN " & RHACLIBF_DB_NAME & ".dbo.RHAC0532 BUHINM WITH (NOLOCK, NOWAIT) ")
                .AppendLine("      ON BUHINM.BUHIN_NO = AL.F_BUHIN_NO ")
                .AppendLine("         AND BUHINM.HAISI_DATE = 99999999 ")
                .AppendLine(" LEFT OUTER JOIN " & RHACLIBF_DB_NAME & ".dbo.RHAC0790 BUIM WITH (NOLOCK, NOWAIT) ")
                .AppendLine("      ON BUIM.ZUMEN_NO = BUHINM.ZUMEN_NO ")
                .AppendLine("         AND BUIM.KAIHATSU_FUGO = AL.KAIHATSU_FUGO ")
                .AppendLine("         AND BUIM.HAISI_DATE = 99999999 ")
                .AppendLine(" LEFT OUTER JOIN " & RHACLIBF_DB_NAME & ".dbo.RHAC0460 COLORM WITH (NOLOCK, NOWAIT) ")
                .AppendLine("      ON COLORM.KAIHATSU_FUGO = AL.KAIHATSU_FUGO ")
                .AppendLine("         AND COLORM.COLOR_SETTEI_KBN = BUIM.COLOR_SETTEI_KBN ")
                .AppendLine("         AND COLORM.DESIGN_GROUP_CODE = BUIM.DESIGN_GROUP_CODE ")
                .AppendLine("         AND COLORM.DESIGN_BUI_CODE = BUIM.DESIGN_BUI_CODE ")
                .AppendLine("         AND COLORM.KYOTEN_CODE = BUIM.KYOTEN_CODE ")
                .AppendLine("         AND COLORM.DESIGN_GROUP_KAITEI_NO = (SELECT MAX(DESIGN_GROUP_KAITEI_NO) ")
                .AppendLine("                 FROM  " & RHACLIBF_DB_NAME & ".dbo.RHAC0460 WITH (NOLOCK, NOWAIT)")
                .AppendLine("                      WHERE(KAIHATSU_FUGO = AL.KAIHATSU_FUGO)")
                .AppendLine("                        AND COLOR_SETTEI_KBN = BUIM.COLOR_SETTEI_KBN")
                .AppendLine("                        AND DESIGN_GROUP_CODE = BUIM.DESIGN_GROUP_CODE")
                .AppendLine("                        AND DESIGN_BUI_CODE = BUIM.DESIGN_BUI_CODE")
                .AppendLine("                        AND KYOTEN_CODE = BUIM.KYOTEN_CODE)")
                .AppendLine(" INNER JOIN  " & RHACLIBF_DB_NAME & ".dbo.RHAC0230 ")
                .AppendLine("      ON AL.KAIHATSU_FUGO = RHAC0230.KAIHATSU_FUGO ")
                .AppendLine("         AND AL.SOBI_KAITEI_NO = RHAC0230.SOBI_KAITEI_NO ")
                .AppendLine("         AND AL.KATASHIKI_SCD_7 = RHAC0230.KATASHIKI_SCD_7  ")
                .AppendLine(" INNER JOIN  " & RHACLIBF_DB_NAME & ".dbo.RHAC0540 ")
                .AppendLine("      ON AL.KAIHATSU_FUGO = RHAC0540.KAIHATSU_FUGO ")
                .AppendLine("         AND AL.SOBI_KAITEI_NO = RHAC0540.SOBI_KAITEI_NO ")
                .AppendLine("         AND AL.KATASHIKI_SCD_7 = RHAC0540.KATASHIKI_SCD_7 ")
                .AppendLine("         AND AL.SHIMUKECHI_CODE = RHAC0540.SHIMUKECHI_CODE  ")
                .AppendLine("         AND AL.OP_CODE = RHAC0540.OP_CODE ")
                .AppendLine(" WHERE ")
                .AppendLine("       AL.HAISI_DATE = 99999999 ")
                ''↓↓2014/09/19 Ⅰ.3.設計編集 ベース車改修専用化_ba) 酒井 ADD BEGIN
                If Not StringUtil.IsEmpty(KaihatsuFugo) Then
                    .AppendLine("   AND AL.KAIHATSU_FUGO = '" & KaihatsuFugo & "' ")
                End If
                ''↑↑2014/09/19 Ⅰ.3.設計編集 ベース車改修専用化_ba) 酒井 ADD END
                ''↓↓2014/07/31 Ⅰ.3.設計編集 ベース車改修専用化_ba) (TES)張 ADD BEGIN
                .AppendLine("   AND AL.F_BUHIN_NO LIKE '" & fBuhinNo & "%' ")
                ''↑↑2014/07/31 Ⅰ.3.設計編集 ベース車改修専用化_ba) (TES)張 ADD END

                ''↓↓2014/09/01 Ⅰ.3.設計編集 ベース車改修専用化_ba) 酒井 ADD BEGIN
                If fBuhinNo = "" Then
                    .AppendLine("   AND AL.BLOCK_NO = '" & BlockNo & "' ")
                    '車型
                    If StringUtil.IsNotEmpty(syagata) Then
                        .AppendLine(" AND RHAC0230.BODY_KIHON_KATA = '" & syagata & "' ")
                    End If
                    'ｸﾞﾚｰﾄﾞ
                    If StringUtil.IsNotEmpty(Grade) Then
                        .AppendLine(" AND RHAC0230.GRADE_CODE = '" & Grade & "' ")
                    End If
                    ''仕向け
                    'If StringUtil.IsNotEmpty(Shimuke) Then
                    '    .AppendLine(" AND RHAC0230.SHIMUKE_DAI_KBN = '" & Shimuke & "' ")
                    'End If
                    'If StringUtil.IsNotEmpty(Shimuke) Then
                    '    If StringUtil.Equals(Shimuke, HOYOU_SHIMUKECHI_KOKUNAI) Then
                    '        .AppendLine(" AND RHAC0230.SHIMUKE_DAI_KBN = '" & HOYOU_SHIMUKECHI_KOKUNAI_CD & "' ")
                    '    End If
                    '    If StringUtil.Equals(Shimuke, HOYOU_SHIMUKECHI_HOKUBEI) Then
                    '        .AppendLine(" AND RHAC0230.SHIMUKE_DAI_KBN = '" & HOYOU_SHIMUKECHI_HOKUBEI_CD & "' ")
                    '    End If
                    '    If StringUtil.Equals(Shimuke, HOYOU_SHIMUKECHI_GOUSYU) Then
                    '        .AppendLine(" AND RHAC0230.SHIMUKE_DAI_KBN = '" & HOYOU_SHIMUKECHI_GOUSYU_CD & "' ")
                    '    End If
                    '    If StringUtil.Equals(Shimuke, HOYOU_SHIMUKECHI_OHSYU_MIGI) Then
                    '        .AppendLine(" AND RHAC0230.SHIMUKE_DAI_KBN = '" & HOYOU_SHIMUKECHI_OHSYU_MIGI_CD & "' ")
                    '    End If
                    '    If StringUtil.Equals(Shimuke, HOYOU_SHIMUKECHI_CHUGOKU) Then
                    '        .AppendLine(" AND RHAC0230.SHIMUKE_DAI_KBN = '" & HOYOU_SHIMUKECHI_CHUGOKU_CD & "' ")
                    '    End If
                    '    If StringUtil.Equals(Shimuke, HOYOU_SHIMUKECHI_OHSYU_HIDARI) Then
                    '        .AppendLine(" AND RHAC0230.SHIMUKE_DAI_KBN = '" & HOYOU_SHIMUKECHI_OHSYU_HIDARI_CD & "' ")
                    '    End If
                    'End If
                    'ﾊﾝﾄﾞﾙ
                    If StringUtil.IsNotEmpty(Handle) Then
                        .AppendLine(" AND RHAC0230.HANDLE_POS = '" & Handle & "' ")
                    End If
                    '排気量
                    If StringUtil.IsNotEmpty(Haikiryo) Then
                        .AppendLine(" AND RHAC0230.EG_HAIKIRYO = '" & Haikiryo & "' ")
                    End If
                    '形式
                    If StringUtil.IsNotEmpty(Keishiki) Then
                        .AppendLine(" AND RHAC0230.DOBENKEI_CODE = '" & Keishiki & "' ")
                    End If
                    '過給器
                    If StringUtil.IsNotEmpty(Kakyuki) Then
                        .AppendLine(" AND RHAC0230.KAKYUKI_CODE = '" & Kakyuki & "' ")
                    End If
                    '駆動方式
                    If StringUtil.IsNotEmpty(Kudou) Then
                        .AppendLine(" AND RHAC0230.KUDO_HOSIKI = '" & Kudou & "' ")
                    End If
                    '変速機
                    If StringUtil.IsNotEmpty(Mission) Then
                        .AppendLine(" AND RHAC0230.TRANS_MISSION = '" & Mission & "' ")
                    End If
                    '７桁型式
                    If StringUtil.IsNotEmpty(Katashiki7) Then
                        .AppendLine(" AND RHAC0230.KATASHIKI_FUGO_7 = '" & Katashiki7 & "' ")
                    End If
                    '仕向地
                    '   ブランクがあるので
                    If StringUtil.IsNotEmpty(Shimukechi) Then
                        If Shimukechi = "国内" Then
                            .AppendLine(" AND RHAC0540.SHIMUKECHI_CODE = '' ")
                        Else
                            .AppendLine(" AND RHAC0540.SHIMUKECHI_CODE = '" & Shimukechi & "' ")
                        End If
                    End If
                    'ＯＰ
                    If StringUtil.IsNotEmpty(OpCode) Then
                        .AppendLine(" AND RHAC0540.OP_CODE = '" & OpCode & "' ")
                    End If
                End If
                ''↑↑2014/09/01 Ⅰ.3.設計編集 ベース車改修専用化_ba) 酒井 ADD END
                .AppendLine("   GROUP BY AL.KAIHATSU_FUGO,AL.BLOCK_NO,AL.F_BUHIN_NO ")
                .AppendLine("   ORDER BY AL.KAIHATSU_FUGO,AL.BLOCK_NO,AL.F_BUHIN_NO ")
            End With
            Dim db As New EBomDbClient
            Return db.QueryForList(Of FinalHinbanListVo)(sb.ToString)

        End Function


        ''' <summary>RHAC0533を取得</summary>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Function GetByRHAC0533Values(ByVal BuhinNo As String) As List(Of Rhac0533Vo) Implements ShiyouJyouhouDao.GetByRHAC0533Values
            Dim sb As New StringBuilder
            With sb
                .Remove(0, .Length)
                .AppendLine("SELECT * ")
                .AppendLine("FROM ")
                .AppendLine(RHACLIBF_DB_NAME & ".dbo.RHAC0533 WITH (NOLOCK, NOWAIT) ")
                .AppendLine(" WHERE ")
                .AppendLine("       RHAC0533.HAISI_DATE = 99999999 ")
                .AppendLine("   AND RHAC0533.BUHIN_NO = '" & BuhinNo & "' ")
            End With
            Dim db As New EBomDbClient
            Return db.QueryForList(Of Rhac0533Vo)(sb.ToString)

        End Function

        ''' <summary>RHAC0532を取得</summary>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Function GetByRHAC0532Values(ByVal BuhinNo As String) As List(Of Rhac0532Vo) Implements ShiyouJyouhouDao.GetByRHAC0532Values
            Dim sb As New StringBuilder
            With sb
                .Remove(0, .Length)
                .AppendLine("SELECT * ")
                .AppendLine("FROM ")
                .AppendLine(RHACLIBF_DB_NAME & ".dbo.RHAC0532 WITH (NOLOCK, NOWAIT) ")
                .AppendLine(" WHERE ")
                .AppendLine("       RHAC0532.HAISI_DATE = 99999999 ")
                .AppendLine("   AND RHAC0532.BUHIN_NO = '" & BuhinNo & "' ")
            End With
            Dim db As New EBomDbClient
            Return db.QueryForList(Of Rhac0532Vo)(sb.ToString)

        End Function

        ''' <summary>RHAC0553を取得</summary>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Function GetByRHAC0553Values(ByVal KaihatsuFugo As String, ByVal BuhinNo As String) As List(Of Rhac0553Vo) Implements ShiyouJyouhouDao.GetByRHAC0553Values
            Dim sb As New StringBuilder
            With sb
                .Remove(0, .Length)
                .AppendLine("SELECT * ")
                .AppendLine("FROM ")
                .AppendLine(RHACLIBF_DB_NAME & ".dbo.RHAC0553 WITH (NOLOCK, NOWAIT) ")
                .AppendLine(" WHERE ")
                .AppendLine("       RHAC0553.HAISI_DATE = 99999999 ")
                ''↓↓2014/07/23 Ⅰ.3.設計編集 ベース車改修専用化_bb) (TES)張 CHG BEGIN
                '.AppendLine("   AND RHAC0553.KAIHATSU_FUGO = '" & KaihatsuFugo & "' ")
                If EzUtil.ContainsNull(KaihatsuFugo) = False Then
                    .AppendLine("   AND RHAC0553.KAIHATSU_FUGO = '" & KaihatsuFugo & "' ")
                End If
                .AppendLine("   AND RHAC0553.BUHIN_NO_OYA = 'T' ")
                '.AppendLine("   AND RHAC0553.BUHIN_NO_KO = '" & BuhinNo & "' ")
                .AppendLine("   AND RHAC0553.BUHIN_NO_KO LIKE '" & BuhinNo & "%' ")
                ''↑↑2014/07/23 Ⅰ.3.設計編集 ベース車改修専用化_bb) (TES)張 CHG END
            End With
            Dim db As New EBomDbClient
            Return db.QueryForList(Of Rhac0553Vo)(sb.ToString)

        End Function

        ''' <summary>RHAC0532を取得</summary>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Function GetByRHAC0552Values(ByVal BuhinNo As String) As List(Of Rhac0552Vo) Implements ShiyouJyouhouDao.GetByRHAC0552Values
            Dim sb As New StringBuilder
            With sb
                .Remove(0, .Length)
                .AppendLine("SELECT * ")
                .AppendLine("FROM ")
                .AppendLine(RHACLIBF_DB_NAME & ".dbo.RHAC0552 WITH (NOLOCK, NOWAIT) ")
                .AppendLine(" WHERE ")
                .AppendLine("       RHAC0552.HAISI_DATE = 99999999 ")
                .AppendLine("   AND RHAC0552.BUHIN_NO_OYA = '" & BuhinNo & "' ")
            End With
            Dim db As New EBomDbClient
            Return db.QueryForList(Of Rhac0552Vo)(sb.ToString)

        End Function

    End Class

End Namespace
