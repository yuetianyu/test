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

    Public Class ConditionSelectDaoImpl


        ''' <summary>
        ''' イベントコード、グループコードから号車を取得.
        ''' </summary>
        ''' <param name="aEventCode"></param>
        ''' <param name="aGroupCode"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Function GetGousya(ByVal aEventCode As String, ByVal aGroupCode As String) As List(Of Vo.ConditionSelectVo)

            Dim sb As New StringBuilder
            With sb
                .Remove(0, .Length)
                .AppendLine("SELECT ")
                .AppendLine("   SEK.SHISAKU_EVENT_CODE")
                .AppendLine("  ,SEK.HYOJIJUN_NO")
                .AppendLine("  ,[SHISAKU_GROUP]")
                .AppendLine("  ,[SHISAKU_GOUSYA]")
                .AppendLine("FROM [" & MBOM_DB_NAME & "].[dbo].[T_SHISAKU_EVENT_KANSEI] SEK")
                '.AppendLine("   INNER JOIN  [" & MBOM_DB_NAME & "].[dbo].[T_SHISAKU_EVENT_BASE_KAITEI] EBK")
                .AppendLine("   INNER JOIN  [" & MBOM_DB_NAME & "].[dbo].[T_SHISAKU_EVENT_BASE] EBK")
                .AppendLine("      ON SEK.SHISAKU_EVENT_CODE = EBK.SHISAKU_EVENT_CODE")
                .AppendLine("      AND SEK.[HYOJIJUN_NO] = EBK.[HYOJIJUN_NO]")
                .AppendLine("")
                .AppendLine("WHERE ")
                .AppendLine("   SEK.SHISAKU_EVENT_CODE=@ShisakuEventCode")
                .AppendLine("   AND SEK.SHISAKU_GROUP=@ShisakuGroup")

            End With

            Dim db As New EBomDbClient
            Dim iArg As New Vo.ConditionSelectVo
            iArg.ShisakuEventCode = aEventCode
            iArg.ShisakuGroup = aGroupCode

            Return db.QueryForList(Of Vo.ConditionSelectVo)(sb.ToString, iArg)

        End Function

#Region "挿入する処理(Insert)"

        ''' <summary>
        ''' 部品表編集情報テンポラリ情報を追加する
        ''' </summary>
        ''' <param name="BuhinEditTMPvo">部品表編集情報VO</param>
        ''' <param name="seihinKbn">製品区分</param>
        ''' <param name="shisakuListCode">リストコード</param>
        ''' <param name="KaihatsuFugo">開発符号</param>
        ''' <param name="SyukeiSuru">集計コードからの展開をするフラグ</param>
        ''' <param name="aGUID">挿入するGUIDを指定</param>
        ''' <param name="aAsGkpsm10p">使用しない</param>
        ''' <param name="aAsKpsm10p">使用しない</param>
        ''' <param name="aAsPartsp">使用しない</param>
        ''' <remarks></remarks>
        Public Sub InsertByBuhinEditTmp(ByVal aGUID As Guid, _
                                        ByVal BuhinEditTMPvo As List(Of TehaichoBuhinEditTmpVo), _
                                        ByVal seihinKbn As String, _
                                        ByVal shisakuListCode As String, _
                                        ByVal KaihatsuFugo As String, _
                                        ByVal SyukeiSuru As Boolean, _
                                        ByVal aAsKpsm10p As List(Of AsKPSM10PVo), _
                                        ByVal aAsPartsp As List(Of AsPARTSPVo), _
                                        ByVal aAsGkpsm10p As List(Of AsGKPSM10PVo))

            'Dim sqlList(BuhinEditTMPvo.Count - 1) As String

            Dim aDate As New ShisakuDate

            Dim strSaishiyoufuka As String = ""
            Dim intShutuzuYoteiDate As Integer = 0
            Dim intShisakuBuhinHi As Integer = 0
            Dim intShisakuKataHi As Integer = 0
            Dim intEditTourokubi As Integer = 0
            Dim intEditTourokujikan As Integer = 0
            Dim strTehaiKigou As String = ""
            Dim strSenyouMark As String = ""



            Dim primaryList As New List(Of String)


            Using insert As New SqlAccess(g_kanrihyoIni(DB_KEY_EBOM))
                insert.Open()
                insert.BeginTransaction()

                For index As Integer = 0 To BuhinEditTMPvo.Count - 1

                    If BuhinEditTMPvo(index).CreatedUserId = "Merge" Then
                        Continue For
                    End If

                    If BuhinEditTMPvo(index).BuhinNo.TrimEnd = "" Then
                        Continue For
                    End If

                    If BuhinEditTMPvo(index).Saishiyoufuka Is Nothing Then
                        strSaishiyoufuka = ""
                    Else
                        strSaishiyoufuka = BuhinEditTMPvo(index).Saishiyoufuka
                    End If
                    If BuhinEditTMPvo(index).ShutuzuYoteiDate Is Nothing Then
                        intShutuzuYoteiDate = 0
                    Else
                        intShutuzuYoteiDate = BuhinEditTMPvo(index).ShutuzuYoteiDate
                    End If
                    intShisakuBuhinHi = 0 'BuhinEditTMPvo(index).ShisakuBuhinHi
                    intShisakuKataHi = 0 'BuhinEditTMPvo(index).ShisakuKataHi
                    intEditTourokubi = 0 'BuhinEditTMPvo(index).EditTourokubi
                    intEditTourokujikan = 0 'BuhinEditTMPvo(index).EditTourokujikan
                    If SyukeiSuru Then
                        strTehaiKigou = ""
                    Else
                        strTehaiKigou = "F"
                    End If
                    '専用マークをここでフル。
                    strSenyouMark = ""
                    'Dim TehaichoSakusei As New TehaichoSakusei.Dao.TehaichoSakuseiDaoImpl()
                    'If Not TehaichoSakusei.FindBySenyouCheckSakusei(BuhinEditTMPvo(index).BuhinNo, BuhinEditTMPvo(index).ShisakuSeihinKbn, _
                    '                         aAsKpsm10p, aAsPartsp, aAsGkpsm10p) Then
                    '    strSenyouMark = "*"
                    'End If

                    'メーカーコード
                    Dim strMakerCode As String = ""
                    If StringUtil.IsNotEmpty(BuhinEditTMPvo(index).MakerCode) Then
                        strMakerCode = BuhinEditTMPvo(index).MakerCode.TrimEnd
                    Else
                        strMakerCode = ""
                    End If



                    Dim sql As String = _
                    " INSERT INTO " & MBOM_DB_NAME & ".dbo.T_SHISAKU_BUHIN_EDIT_TMP_3D (" _
                    & " GUID, " _
                    & " SHISAKU_EVENT_CODE, " _
                    & " SHISAKU_BUKA_CODE, " _
                    & " SHISAKU_BLOCK_NO, " _
                    & " SHISAKU_BLOCK_NO_KAITEI_NO, " _
                    & " BUHIN_NO_HYOUJI_JUN, " _
                    & " GYOU_ID, " _
                    & " LEVEL, " _
                    & " SHUKEI_CODE, " _
                    & " SIA_SHUKEI_CODE, " _
                    & " GENCYO_CKD_KBN, " _
                    & " MAKER_CODE, " _
                    & " MAKER_NAME, " _
                    & " BUHIN_NO, " _
                    & " BUHIN_NO_KBN, " _
                    & " BUHIN_NO_KAITEI_NO, " _
                    & " EDA_BAN, " _
                    & " BUHIN_NAME, " _
                    & " SAISHIYOUFUKA, " _
                    & " SHUTUZU_YOTEI_DATE, " _
                    & " ZAISHITU_KIKAKU_1, " _
                    & " ZAISHITU_KIKAKU_2, " _
                    & " ZAISHITU_KIKAKU_3, " _
                    & " ZAISHITU_MEKKI, " _
                    & " SHISAKU_BANKO_SURYO, " _
                    & " SHISAKU_BANKO_SURYO_U, " _
                    & " SHISAKU_BUHIN_HI, " _
                    & " SHISAKU_KATA_HI, " _
                    & " BIKOU, " _
                    & " EDIT_TOUROKUBI, " _
                    & " EDIT_TOUROKUJIKAN, " _
                    & " KAITEI_HANDAN_FLG, " _
                    & " TEHAI_KIGOU, " _
                    & " NOUBA, " _
                    & " KYOUKU_SECTION, " _
                    & " SENYOU_MARK, " _
                    & " KOUTAN, " _
                    & " STSR_DHSTBA, " _
                    & " HENKATEN, " _
                    & " SHISAKU_SEIHIN_KBN, " _
                    & " SHISAKU_LIST_CODE, " _
                    & " CREATED_USER_ID, " _
                    & " CREATED_DATE, " _
                    & " CREATED_TIME, " _
                    & " UPDATED_USER_ID, " _
                    & " UPDATED_DATE, " _
                    & " UPDATED_TIME " _
                    & " ) " _
                    & " VALUES ( " _
                    & " '" & aGUID.ToString() & "' ," _
                    & " '" & BuhinEditTMPvo(index).ShisakuEventCode & "' , " _
                    & " '" & BuhinEditTMPvo(index).ShisakuBukaCode & "' , " _
                    & " '" & BuhinEditTMPvo(index).ShisakuBlockNo & "' , " _
                    & " '" & BuhinEditTMPvo(index).ShisakuBlockNoKaiteiNo & "' , " _
                    & BuhinEditTMPvo(index).BuhinNoHyoujiJun & " , " _
                    & "'999' , " _
                    & BuhinEditTMPvo(index).Level & ", " _
                    & " '" & BuhinEditTMPvo(index).ShukeiCode & "' , " _
                    & " '" & BuhinEditTMPvo(index).SiaShukeiCode & "' , " _
                    & " '" & BuhinEditTMPvo(index).GencyoCkdKbn & "' , " _
                    & " '" & strMakerCode & "' , " _
                    & " '" & BuhinEditTMPvo(index).MakerName & "' , " _
                    & " '" & BuhinEditTMPvo(index).BuhinNo.Trim & "' , " _
                    & " '" & BuhinEditTMPvo(index).BuhinNoKbn & "' , " _
                    & " '" & BuhinEditTMPvo(index).BuhinNoKaiteiNo & "' , " _
                    & " '" & BuhinEditTMPvo(index).EdaBan & "' , " _
                    & " '" & BuhinEditTMPvo(index).BuhinName & "' , " _
                    & " '" & strSaishiyoufuka & "' , " _
                    & intShutuzuYoteiDate & " , " _
                    & " '" & BuhinEditTMPvo(index).ZaishituKikaku1 & "' , " _
                    & " '" & BuhinEditTMPvo(index).ZaishituKikaku2 & "' , " _
                    & " '" & BuhinEditTMPvo(index).ZaishituKikaku3 & "' , " _
                    & " '" & BuhinEditTMPvo(index).ZaishituMekki & "' , " _
                    & " '" & BuhinEditTMPvo(index).ShisakuBankoSuryo & "' , " _
                    & " '" & BuhinEditTMPvo(index).ShisakuBankoSuryoU & "' , " _
                    & intShisakuBuhinHi & " , " _
                    & intShisakuKataHi & " , " _
                    & " '" & BuhinEditTMPvo(index).Bikou & "' , " _
                    & intEditTourokubi & " , " _
                    & intEditTourokujikan & " , " _
                    & " '" & BuhinEditTMPvo(index).KaiteiHandanFlg & "' , " _
                    & " '" & strTehaiKigou & "' , " _
                    & " '' , " _
                    & " '" & BuhinEditTMPvo(index).KyoukuSection & "' , " _
                    & " '" & strSenyouMark & "' , " _
                    & " '' , " _
                    & " '' , " _
                    & " '' , " _
                    & " '" & seihinKbn & "' , " _
                    & " '" & shisakuListCode & "' , " _
                    & " '" & LoginInfo.Now.UserId & "' , " _
                    & " '" & aDate.CurrentDateDbFormat & "' , " _
                    & " '" & aDate.CurrentTimeDbFormat & "' , " _
                    & " '" & LoginInfo.Now.UserId & "' , " _
                    & " '" & aDate.CurrentDateDbFormat & "' , " _
                    & " '" & aDate.CurrentTimeDbFormat & "'" _
                    & " ) "
                    Try

                        Dim key As String = EzUtil.MakeKey(New String() {aGUID.ToString(), _
                                                                        BuhinEditTMPvo(index).ShisakuEventCode, _
                                                                        BuhinEditTMPvo(index).ShisakuBukaCode, _
                                                                        BuhinEditTMPvo(index).ShisakuBlockNo, _
                                                                        BuhinEditTMPvo(index).ShisakuBlockNoKaiteiNo, _
                                                                        BuhinEditTMPvo(index).BuhinNoHyoujiJun.ToString})

                        If Not primaryList.Contains(key) Then
                            insert.ExecuteNonQuery(sql)
                            primaryList.Add(key)
                        End If

#If DEBUG Then

                        '2014/04/10 kabasawa'
                        '手配帳のリストを指定しないでここにきているので重複はほぼ確実に起こる'
                    Catch exsql As SqlClient.SqlException
                        'Dim errorStr As String
                        'errorStr = aGUID.ToString() & "' ," _
                        '& " '" & BuhinEditTMPvo(index).ShisakuEventCode & "' , " _
                        '& " '" & BuhinEditTMPvo(index).ShisakuBukaCode & "' , " _
                        '& " '" & BuhinEditTMPvo(index).ShisakuBlockNo & "' , " _
                        '& " '" & BuhinEditTMPvo(index).ShisakuBlockNoKaiteiNo & "' , " _
                        '& BuhinEditTMPvo(index).BuhinNoHyoujiJun

                        'Debug.WriteLine(errorStr)
                    Catch ex As Exception
                        'Debug.WriteLine(ex.Message)
#End If
                    Finally

                    End Try

                Next

                insert.Commit()

            End Using

        End Sub

#End Region


        ''' <summary>
        ''' 部品表編集号車情報テンポラリ情報を追加する
        ''' </summary>
        ''' <param name="gousyaTmpvo">部品表編集号車情報VO</param>
        ''' <remarks></remarks>
        Public Sub InsertByGousyaTMP(ByVal aGUID As Guid, ByVal gousyaTmpvo As List(Of TehaichoBuhinEditTmpVo))

            Using db As New EBomDbClient

                Dim aDate As New ShisakuDate
                Dim primaryList As New List(Of String)

                For index As Integer = 0 To gousyaTmpvo.Count - 1

                    Dim sql As String = _
                    " INSERT INTO " & MBOM_DB_NAME & ".dbo.T_SHISAKU_BUHIN_EDIT_GOUSYA_TMP_3D ( " _
                    & " GUID," _
                    & " SHISAKU_EVENT_CODE, " _
                    & " SHISAKU_BUKA_CODE, " _
                    & " SHISAKU_BLOCK_NO, " _
                    & " SHISAKU_BLOCK_NO_KAITEI_NO, " _
                    & " BUHIN_NO_HYOUJI_JUN, " _
                    & " SHISAKU_GOUSYA_HYOUJI_JUN, " _
                    & " GYOU_ID, " _
                    & " SHISAKU_GOUSYA, " _
                    & " INSU_SURYO, " _
                    & " CREATED_USER_ID, " _
                    & " CREATED_DATE, " _
                    & " CREATED_TIME, " _
                    & " UPDATED_USER_ID, " _
                    & " UPDATED_DATE, " _
                    & " UPDATED_TIME " _
                    & " ) " _
                    & " VALUES ( " _
                    & " '" & aGUID.ToString() & "' ," _
                    & " '" & gousyaTmpvo(index).ShisakuEventCode & "' ," _
                    & " '" & gousyaTmpvo(index).ShisakuBukaCode & "' ," _
                    & " '" & gousyaTmpvo(index).ShisakuBlockNo & "' ," _
                    & " '" & gousyaTmpvo(index).ShisakuBlockNoKaiteiNo & "' ," _
                    & gousyaTmpvo(index).BuhinNoHyoujiJun & "," _
                    & gousyaTmpvo(index).ShisakuGousyaHyoujiJun & "," _
                    & " '999', " _
                    & " '" & gousyaTmpvo(index).ShisakuGousya & "' ," _
                    & gousyaTmpvo(index).InsuSuryo & "," _
                    & " '" & LoginInfo.Now.UserId & "' ," _
                    & " '" & aDate.CurrentDateDbFormat & "' ," _
                    & " '" & aDate.CurrentTimeDbFormat & "' ," _
                    & " '" & LoginInfo.Now.UserId & "' ," _
                    & " '" & aDate.CurrentDateDbFormat & "' ," _
                    & " '" & aDate.CurrentTimeDbFormat & "' " _
                    & " ) "


                    Dim key As String = EzUtil.MakeKey(New String() {aGUID.ToString, _
                                                gousyaTmpvo(index).ShisakuEventCode, _
                                                gousyaTmpvo(index).ShisakuBukaCode, _
                                                gousyaTmpvo(index).ShisakuBlockNo, _
                                                gousyaTmpvo(index).ShisakuBlockNoKaiteiNo, _
                                                gousyaTmpvo(index).BuhinNoHyoujiJun.ToString, _
                                                gousyaTmpvo(index).ShisakuGousyaHyoujiJun.ToString})

                    Try

                        If Not primaryList.Contains(key) Then
                            db.Insert(sql)

                            primaryList.Add(key)
                        End If

                    Catch exsql As SqlClient.SqlException
                        'Debug.WriteLine(exsql.Message)
                    Catch ex As Exception

                    End Try


                Next

                db.Commit()

            End Using

        End Sub


    End Class

End Namespace
