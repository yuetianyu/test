Imports System.Reflection
Imports ShisakuCommon
Imports ShisakuCommon.Ui
Imports EventSakusei.ShisakuBuhinKaiteiBlock.Dao
Imports EventSakusei.ShisakuBuhinKaiteiBlock.Ui
Imports ShisakuCommon.Db.EBom.Vo.Helper
Imports ShisakuCommon.Db.EBom.Vo
Imports ShisakuCommon.Db.EBom.Dao
Imports ShisakuCommon.Ui.Spd
Imports EventSakusei.ShisakuBuhinEditEvent.Dao
Imports EBom.Common.mdlConstraint
Imports EBom.Common
Imports EBom.Data
Imports ShisakuCommon.Db.EBom.Dao.Impl
Imports ShisakuCommon.Db.Impl
Imports YakanSekkeiTenakai.ShisakuBuhinMenu.Logic.Impl
Imports YakanSekkeiTenakai.Vo
Imports YakanSekkeiTenakai.ExcelProcess

''' <summary>
''' 
''' </summary>
''' <remarks>  ''↓↓2014/08/05 Ⅰ.5.EBOM差分出力  ax) (TES)施 ADD </remarks>
Module mainsub
    ''↓↓2014/08/05 Ⅰ.5.EBOM差分出力  ay) (TES)施 ADD BEGIN
    Sub Main()

        Try
            'アプリケーション初期化
            InitApplication()

            'DB初期化
            Dim yakanSekkeiTenkaiDao As New YakanSekkeiTenakai.Dao.YakanSekkeiTenkaiDaoImpl
            yakanSekkeiTenkaiDao.InitYakanSekkeiTenkai()

            Dim aDate As New ShisakuDate

            'T_SHISAKU_EVENT_EBOM_KANSHIから設計展開済み、かつ、T_SHISAKU_EVENT.EBOM_KANSHI_FLG=1の「監視対象イベントコード一覧」を取得する
            '"ay)について、
            '1.T_SHISAKU_EVENT_EBOM_KANSHIから設計展開済み、かつ、T_SHISAKU_EVENT.EBOM_KANSHI_FLG=1の「監視対象イベントコード一覧」を取得する。設計展開済みは如何判断する出しか？

            ''↓↓2014/08/28 Ⅰ.5.EBOM差分出力 酒井 ADD BEGIN
            'Dim shisakuEventVos As List(Of TShisakuEventEbomKanshiVo) = yakanSekkeiTenkaiDao.GetKanshiTaisyouEventCode()
            Dim shisakuEventVos As List(Of TShisakuEventVo) = yakanSekkeiTenkaiDao.GetKanshiTaisyouEventCode()

            'For Each shisakuEventVo As TShisakuEventEbomKanshiVo In shisakuEventVos
            For Each shisakuEventVo As TShisakuEventVo In shisakuEventVos
                ''↑↑2014/08/28 Ⅰ.5.EBOM差分出力 酒井 ADD END
                Dim shisakuEventDao As New TShisakuEventDaoImpl

                Dim command As New YakanSekkeiTenkaiCommand(shisakuEventDao.FindByPk(shisakuEventVo.ShisakuEventCode), _
                                                            Nothing, _
                                                            New ShisakuDaoImpl, _
                                                            shisakuEventDao, _
                                                            New TShisakuSekkeiBlockEbomKanshiDaoImpl, _
                                                            New TShisakuSekkeiBlockInstlEbomKanshiDaoImpl, _
                                                            New TShisakuBuhinKouseiDaoImpl, _
                                                            New TShisakuBuhinDaoImpl, _
                                                            aDate.CurrentDateTime)

                command.Perform()
                command.GetTsuchishoNoZairyou()
            Next
            'T_SHISAKU_SEKKEI_BLOCK_EBOM_KANSHIをイベントコードと試作ブロック№改訂№でgroupし、																																																				
            'count(*)=2（試作ブロック№改訂№=000と999）のイベントコードを監視対象イベントコードとして取得する。
            ''↓↓2014/08/28 Ⅰ.5.EBOM差分出力 酒井 ADD BEGIN
            'Dim sekkeiBlockVos As List(Of TShisakuSekkeiBlockEbomKanshiVo) = yakanSekkeiTenkaiDao.GetKanshiTaisyouEventCodeByKaiteiNo()

            'Dim kanshiEventCodes As New List(Of TShisakuSekkeiBlockEbomKanshiVo)
            'For index As Integer = 1 To sekkeiBlockVos.Count - 1
            'If sekkeiBlockVos.Item(index).ShisakuEventCode.Equals(sekkeiBlockVos.Item(index - 1).ShisakuEventCode) Then
            'kanshiEventCodes.Add(sekkeiBlockVos.Item(index))
            'End If
            'Next
            Dim kanshiEventCodes As List(Of TShisakuSekkeiBlockEbomKanshiVo) = yakanSekkeiTenkaiDao.GetKanshiTaisyouEventCodeByKaiteiNo()
            ''↑↑2014/08/28 Ⅰ.5.EBOM差分出力 酒井 ADD END

            '部品編集情報を取得
            For Each sekkeiBlockVo As TShisakuSekkeiBlockEbomKanshiVo In kanshiEventCodes
                '↓↓2014/09/26 酒井 ADD BEGIN
                '差異あり
                Dim diffVos As New List(Of YakanSekkeiBlockVoHelperExcel)
                '差異なし
                Dim sameVos As New List(Of YakanSekkeiBlockVoHelperExcel)

                'イベント出力フラグ
                Dim eventOutputFlag As Boolean = False

                '↓↓2014/10/10 酒井 ADD BEGIN
                'Dim EventEbomKanshiVos As List(Of TShisakuEventEbomKanshiVo) = yakanSekkeiTenkaiDao.FindByGousya(sekkeiBlockVo.ShisakuEventCode)
                'For Each EventEbomKanshiVo As TShisakuEventEbomKanshiVo In EventEbomKanshiVos
                '↑↑2014/10/10 酒井 ADD END
                '↑↑2014/09/26 酒井 ADD END
                '結果セット
                '↓↓2014/09/26 酒井 ADD BEGIN
                '                    Dim buhinEditVos As List(Of YakanSekkeiBlockVoHelperExcel) = yakanSekkeiTenkaiDao.FindByAllBuhinEdit(sekkeiBlockVo.ShisakuEventCode)
                '↓↓2014/10/10 酒井 ADD BEGIN
                '                Dim buhinEditVos As List(Of YakanSekkeiBlockVoHelperExcel) = yakanSekkeiTenkaiDao.FindByAllBuhinEdit(sekkeiBlockVo.ShisakuEventCode, EventEbomKanshiVo.ShisakuGousya)
                Dim buhinEditVos As List(Of YakanSekkeiBlockVoHelperExcel) = yakanSekkeiTenkaiDao.FindByAllBuhinEdit(sekkeiBlockVo.ShisakuEventCode)
                '↑↑2014/10/10 酒井 ADD END
                '↑↑2014/09/26 酒井 ADD END

                'Excel出力用結果セット
                Dim excelVos As New List(Of YakanSekkeiBlockVoHelperExcel)

                'ブロック差異フラグ
                Dim blockDiffFlag As Boolean = False

                For index As Integer = 0 To buhinEditVos.Count - 1
                    If index <> 0 Then

                        'BlockNoが変わったら、ブロック毎に、diffVosかsameVosに移す
                        If Not (buhinEditVos.Item(index).ShisakuEventCode.Equals(buhinEditVos.Item(index - 1).ShisakuEventCode) AndAlso _
                                buhinEditVos.Item(index).ShisakuBukaCode.Equals(buhinEditVos.Item(index - 1).ShisakuBukaCode) AndAlso _
                                buhinEditVos.Item(index).ShisakuBlockNo.Equals(buhinEditVos.Item(index - 1).ShisakuBlockNo)) Then
                            If blockDiffFlag = True Then
                                'Excel出力用結果セットをExcel（開発符号＋イベント名＋ＥＢＯＭ差分＋ＭＭＤＤ　ＨＨＭＭ.xls）に出力する
                                diffVos.AddRange(excelVos)
                                'Excel出力用結果セットをNewする
                                excelVos = New List(Of YakanSekkeiBlockVoHelperExcel)
                                blockDiffFlag = False
                                eventOutputFlag = True
                            Else
                                sameVos.AddRange(excelVos)
                                'Excel出力用結果セットをNewする
                                excelVos = New List(Of YakanSekkeiBlockVoHelperExcel)
                                blockDiffFlag = False
                            End If
                        End If
                        If Not (buhinEditVos.Item(index).ShisakuEventCode.Equals(buhinEditVos.Item(index - 1).ShisakuEventCode)) Then
                            If eventOutputFlag = True Then
                                eventOutputFlag = False
                            Else
                                '差異なしExcelファイル（開発符号＋イベント名＋ＥＢＯＭ差分＋ＭＭＤＤ　ＨＨＭＭ差異なし.xls）を出力する

                                eventOutputFlag = False
                            End If
                        End If
                    End If

                    '前行のチェックで一致とならかった場合の最終行は比較対象となる次行がないので、AorD
                    If index = buhinEditVos.Count - 1 Then
                        If buhinEditVos.Item(index).ShisakuBlockNoKaiteiNo.Equals("000") Then
                            buhinEditVos.Item(index).Symbol = "A"
                            blockDiffFlag = True
                            '結果セット（index)をExcel出力用結果セットにaddする
                            excelVos.Add(buhinEditVos.Item(index))
                        ElseIf buhinEditVos.Item(index).ShisakuBlockNoKaiteiNo.Equals("999") Then
                            buhinEditVos.Item(index).Symbol = "D"
                            blockDiffFlag = True
                            '結果セット（index)をExcel出力用結果セットにaddする
                            excelVos.Add(buhinEditVos.Item(index))
                        End If
                    Else

                        'キーが一致しているかチェック
                        '↓↓2014/10/10 酒井 ADD BEGIN
                        'If (buhinEditVos.Item(index).ShisakuEventCode.Equals(buhinEditVos.Item(index + 1).ShisakuEventCode) AndAlso _
                        '        buhinEditVos.Item(index).ShisakuBukaCode.Equals(buhinEditVos.Item(index + 1).ShisakuBukaCode) AndAlso _
                        '        buhinEditVos.Item(index).ShisakuBlockNo.Equals(buhinEditVos.Item(index + 1).ShisakuBlockNo) AndAlso _
                        '        buhinEditVos.Item(index).BuhinNo.Equals(buhinEditVos.Item(index + 1).BuhinNo)) Then
                        If (buhinEditVos.Item(index).ShisakuEventCode.Equals(buhinEditVos.Item(index + 1).ShisakuEventCode) AndAlso _
                                buhinEditVos.Item(index).ShisakuBukaCode.Equals(buhinEditVos.Item(index + 1).ShisakuBukaCode) AndAlso _
                                buhinEditVos.Item(index).ShisakuBlockNo.Equals(buhinEditVos.Item(index + 1).ShisakuBlockNo) AndAlso _
                                buhinEditVos.Item(index).BuhinNo.Equals(buhinEditVos.Item(index + 1).BuhinNo) AndAlso _
                                buhinEditVos.Item(index).ShisakuGousya.Equals(buhinEditVos.Item(index + 1).ShisakuGousya)) Then
                            '↑↑2014/10/10 酒井 ADD END

                            '比較項目が一致しているかチェック
                            ''↓↓2014/08/27 Ⅰ.5.EBOM差分出力 ay) 酒井 ADD BEGIN
                            '                            If (buhinEditVos.Item(index).Level.Equals(buhinEditVos.Item(index + 1).Level) And _
                            '        buhinEditVos.Item(index).ShukeiCode.Equals(buhinEditVos.Item(index + 1).ShukeiCode) And _
                            '        buhinEditVos.Item(index).SiaShukeiCode.Equals(buhinEditVos.Item(index + 1).SiaShukeiCode) And _
                            '        buhinEditVos.Item(index).MakerCode.Equals(buhinEditVos.Item(index + 1).MakerCode) And _
                            '        buhinEditVos.Item(index).ZumenKaiteiNo.Equals(buhinEditVos.Item(index + 1).ZumenKaiteiNo) And _
                            '        buhinEditVos.Item(index).InsuSuryo.Equals(buhinEditVos.Item(index + 1).InsuSuryo) And _
                            '        buhinEditVos.Item(index).ZairyoKijutsu.Equals(buhinEditVos.Item(index + 1).ZairyoKijutsu) And _
                            '        buhinEditVos.Item(index).BankoSuryo.Equals(buhinEditVos.Item(index + 1).BankoSuryo)) Then
                            If (buhinEditVos.Item(index).Level.Equals(buhinEditVos.Item(index + 1).Level) AndAlso _
                                    buhinEditVos.Item(index).ShukeiCode.Equals(buhinEditVos.Item(index + 1).ShukeiCode) AndAlso _
                                    buhinEditVos.Item(index).SiaShukeiCode.Equals(buhinEditVos.Item(index + 1).SiaShukeiCode) AndAlso _
                                    buhinEditVos.Item(index).MakerCode.Equals(buhinEditVos.Item(index + 1).MakerCode) AndAlso _
                                    buhinEditVos.Item(index).ZumenKaiteiNo.Equals(buhinEditVos.Item(index + 1).ZumenKaiteiNo) AndAlso _
                                    buhinEditVos.Item(index).TsuchishoNo.Equals(buhinEditVos.Item(index + 1).TsuchishoNo) AndAlso _
                                    buhinEditVos.Item(index).InsuSuryo.Equals(buhinEditVos.Item(index + 1).InsuSuryo) AndAlso _
                                    buhinEditVos.Item(index).ZairyoKijutsu.Equals(buhinEditVos.Item(index + 1).ZairyoKijutsu) AndAlso _
                                    buhinEditVos.Item(index).BankoSuryo.Equals(buhinEditVos.Item(index + 1).BankoSuryo)) Then
                                ''↑↑2014/08/27 Ⅰ.5.EBOM差分出力 ay) 酒井 ADD END
                                '結果セット（index)をExcel出力用結果セットにaddする
                                excelVos.Add(buhinEditVos.Item(index))
                                '↓↓2014/10/13 酒井 ADD BEGIN
                                '号車別には同一レコードでも一旦Excel出力し、
                                'Excel出力時、全号車レコードを1行にまとめる時に、
                                '不要であれば削除する()
                                excelVos.Add(buhinEditVos.Item(index + 1))
                                '↑↑2014/10/13 酒井 ADD END
                                ''↓↓2014/08/27 Ⅰ.5.EBOM差分出力 ay) 酒井 ADD BEGIN
                                'index = index + 2
                                index = index + 1
                                ''↑↑2014/08/27 Ⅰ.5.EBOM差分出力 ay) 酒井 ADD END
                            Else
                                buhinEditVos.Item(index).Symbol = "C"
                                buhinEditVos.Item(index + 1).Symbol = "C"
                                blockDiffFlag = True
                                '結果セット（index)をExcel出力用結果セットにaddする
                                excelVos.Add(buhinEditVos.Item(index))
                                '結果セット（index + 1)をExcel出力用結果セットにaddする
                                excelVos.Add(buhinEditVos.Item(index + 1))
                                ''↓↓2014/08/27 Ⅰ.5.EBOM差分出力 ay) 酒井 ADD BEGIN
                                'index = index + 2
                                index = index + 1
                                ''↑↑2014/08/27 Ⅰ.5.EBOM差分出力 ay) 酒井 ADD END
                            End If
                        Else
                            If buhinEditVos.Item(index).ShisakuBlockNoKaiteiNo.Equals("000") Then
                                buhinEditVos.Item(index).Symbol = "A"
                                blockDiffFlag = True
                                '結果セット（index)をExcel出力用結果セットにaddする
                                excelVos.Add(buhinEditVos.Item(index))
                            ElseIf buhinEditVos.Item(index).ShisakuBlockNoKaiteiNo.Equals("999") Then
                                buhinEditVos.Item(index).Symbol = "D"
                                blockDiffFlag = True
                                '結果セット（index)をExcel出力用結果セットにaddする
                                excelVos.Add(buhinEditVos.Item(index))
                            End If
                        End If
                    End If
                Next

                ''↓↓2014/08/28 Ⅰ.5.EBOM差分出力 ay) 酒井 ADD BEGIN
                If blockDiffFlag = True Then
                    'Excel出力用結果セットをExcel（開発符号＋イベント名＋ＥＢＯＭ差分＋ＭＭＤＤ　ＨＨＭＭ.xls）に出力する
                    diffVos.AddRange(excelVos)
                End If
                ''↑↑2014/08/28 Ⅰ.5.EBOM差分出力 ay) 酒井 ADD END

                '↓↓2014/10/10 酒井 ADD BEGIN
                '↓↓2014/09/26 酒井 ADD BEGIN
                'Next
                '↑↑2014/09/26 酒井 ADD END
                '↑↑2014/10/10 酒井 ADD END
                'Excel出力
                Dim excelOutput As New ExportYakanSekkeiBlockExcel(sekkeiBlockVo.ShisakuEventCode, diffVos, sameVos)
            Next

        Catch ex As Exception
            ComFunc.ShowErrMsgBox(SYSERR_00001, _
                ex.Message, MethodBase.GetCurrentMethod.Name)
            g_log.WriteException(ex)
            Return
        End Try

    End Sub
    ''↑↑2014/08/05 Ⅰ.5.EBOM差分出力 ay) (TES)施 ADD END

#Region " アプリケーション初期化 "
    Private Function InitApplication() As RESULT
        Try
            '環境変数確認

            Dim ebomEnv As String = ComFunc.ChkEBomEnv()

            If ebomEnv = String.Empty Then
                Return RESULT.NG
            End If

            g_log.WriteInfo("SDISINI:'{0}'", ebomEnv)

            'Kanrihyo.ini 初期化

            If Not ComFunc.InitIni(g_kanrihyoIni, mdlConstraint.INI_KANRIHYO_FILE) = RESULT.OK Then
                Return RESULT.NG
            End If

            g_log.WriteInfo("kanrihyo.ini  EBom.DB:'{0}' - '{1}'\n" & _
                            "kanrihyo.ini  WORK.DB:'{2}' - '{3}'\n" & _
                            "kanrihyo.ini  mBOM.DB:'{4}' - '{5}'", _
                ComFunc.GetServer(g_kanrihyoIni("EBOM_DB")), _
                ComFunc.GetDatabaseName(g_kanrihyoIni("EBOM_DB")), _
                ComFunc.GetServer(g_kanrihyoIni("KOSEI_DB")), _
                ComFunc.GetDatabaseName(g_kanrihyoIni("KOSEI_DB")), _
                ComFunc.GetServer(g_kanrihyoIni("mBOM_DB")), _
                ComFunc.GetDatabaseName(g_kanrihyoIni("mBOM_DB")))

            RHACLIBF_DB_NAME = ComFunc.GetDatabaseName(g_kanrihyoIni("EBOM_DB"))
            EBOM_DB_NAME = ComFunc.GetDatabaseName(g_kanrihyoIni("KOSEI_DB"))
            MBOM_DB_NAME = ComFunc.GetDatabaseName(g_kanrihyoIni("mBOM_DB"))


            'DB接続チェック
            If Not CanDbConnect() = RESULT.OK Then
                Return RESULT.NG
            End If

            Return RESULT.OK

        Catch ex As Exception
            ComFunc.ShowErrMsgBox(SYSERR_00001, _
                ex.Message, MethodBase.GetCurrentMethod.Name)
            g_log.WriteException(ex)
            Return RESULT.NG
        End Try
    End Function
#End Region

#Region " DB接続チェック "
    ''' <summary>
    ''' DB接続チェック
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function CanDbConnect() As RESULT
        Dim errCnStr As String = String.Empty       'エラー発生した接続文字列

        Try
            'EBOM DB
            errCnStr = g_kanrihyoIni("EBOM_DB")

            Using db As New SqlAccess(g_kanrihyoIni("EBOM_DB"))
                db.Open()
            End Using

            'WORK DB
            errCnStr = g_kanrihyoIni("KOSEI_DB")

            Using db As New SqlAccess(g_kanrihyoIni("KOSEI_DB"))
                db.Open()
            End Using

            'mBOM DB
            errCnStr = g_kanrihyoIni("mBOM_DB")

            Using db As New SqlAccess(g_kanrihyoIni("mBOM_DB"))
                db.Open()
            End Using

            Return RESULT.OK

        Catch ex As Exception
            ComFunc.ShowErrMsgBox("エラー {0} {1}", ex.Message, errCnStr)
            g_log.WriteException(ex)
            Return RESULT.NG
        End Try
    End Function
#End Region

End Module
