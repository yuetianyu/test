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


Module mainsub

    'Sub Main()

    '    'アプリケーション初期化
    '    InitApplication()

    '    '初期
    '    Dim SendList As List(Of MUserMailAddressVo)
    '    Dim intMailTsuchiHi01 As Integer
    '    Dim intMailTsuchiHi02 As Integer
    '    Dim i As Integer

    '    '宛先情報を取得
    '    Dim sendDao As IMUserMailAddressDao = New MUserMailAddressDaoImpl()
    '    SendList = sendDao.GetSendList()


    '    '送信内容を取得
    '    Dim blockDao As IShisakuBuhinKaiteiBlockTsuchiDao = New ShisakuBuhinKaiteiBlockTsuchiDaoImpl()
    '    Dim blockList = blockDao.GetBlockTsuchiList(intMailTsuchiHi01, intMailTsuchiHi02)


    '    '宛先及び送信内容の件数毎にメール送信を行う。

    '    'MailMessage、SmtpClientの作成
    '    Dim msg As New System.Net.Mail.MailMessage()
    '    Dim sc As New System.Net.Mail.SmtpClient()

    '    '送信者（メールアドレスが"sender@xxx.xxx"、名前が"鈴木"の場合）
    '    msg.From = New System.Net.Mail.MailAddress("yaginumayasuo@ezweb.ne.jp", "柳沼携帯")

    '    '件名
    '    msg.Subject = "【試作部品表　改訂編集通知】"


    '    'SMTPサーバーを指定する
    '    sc.Host = "smtp.daniel-soft.com"

    '    'portを指定する
    '    sc.Port = 587

    '    'ユーザー名とパスワードを設定する
    '    sc.Credentials = New System.Net.NetworkCredential("dbxsp002", "Syuto002")

    '    '現在は、EnableSslがtrueでは失敗する
    '    sc.EnableSsl = False

    '    'Timeoutを指定しないと失敗するとの報告もあるようだが、不明
    '    'sc.Timeout = 100000;

    '    '本文作成
    '    Dim strMsgBody As String = "以下のイベント、設計課、ブロックで部品表が改訂されました。"

    '    For i = 0 To blockList.Count - 1

    '        '本文
    '        strMsgBody += vbCrLf + vbCrLf + _
    '                    blockList.Item(i).ShisakuEventCode + "  " + _
    '                    blockList.Item(i).ShisakuBukaCode + "  " + _
    '                    blockList.Item(i).ShisakuBlockNo + "  " + _
    '                    blockList.Item(i).ShisakuBlockNoKaiteiNo + "  " + _
    '                    blockList.Item(i).ShisakuBlockName

    '    Next

    '    '本文を設定
    '    msg.Body = strMsgBody

    '    'メールを送信
    '    For i = 0 To SendList.Count - 1

    '        '宛先（メールアドレスが"recipient@xxx.xxx"、名前が"加藤"の場合）
    '        msg.To.Add(New System.Net.Mail.MailAddress(SendList.Item(i).MailAddress, ""))

    '    Next

    '    'メッセージを送信する
    '    sc.Send(msg)

    '    '後始末
    '    msg.Dispose()

    '    MsgBox("改訂通知情報のメール送信が完了しました。", MsgBoxStyle.OkOnly, "改訂通知")


    'End Sub


    Sub Main()

        Try

            'アプリケーション初期化
            InitApplication()

            '初期
            Dim SendList As List(Of MUserMailAddressVo)
            Dim intMailTsuchiHi01 As Integer
            Dim intMailTsuchiHi02 As Integer
            Dim i, j As Integer

            '宛先情報を取得
            Dim sendDao As IMUserMailAddressDao = New MUserMailAddressDaoImpl()
            SendList = sendDao.GetSendList()

            '送信内容を取得
            Dim blockDao As IShisakuBuhinKaiteiBlockTsuchiDao = New ShisakuBuhinKaiteiBlockTsuchiDaoImpl()
            Dim blockList = blockDao.GetBlockTsuchiList(intMailTsuchiHi01, intMailTsuchiHi02)

            '改訂通知情報が無ければ処理終了
            If blockList.Count = 0 Then
                Exit Sub
            Else

                '---------------------------------------------------------------------------------------------
                'メール自動送信環境初期設定
                Dim t As Type = Type.GetTypeFromProgID("CDO.Message")
                Dim cdo As Object = Activator.CreateInstance(t)
                Dim conf As Object = t.InvokeMember("Configuration", _
                    System.Reflection.BindingFlags.GetProperty, _
                    Nothing, cdo, Nothing)
                Dim fields As Object = t.InvokeMember("Fields", _
                    System.Reflection.BindingFlags.GetProperty, _
                    Nothing, conf, Nothing)
                '
                t.InvokeMember("Item", System.Reflection.BindingFlags.SetProperty, _
                    Nothing, fields, New Object() {"http://schemas.microsoft.com/cdo/configuration/sendusing", 2})
                'SMTPサーバーを指定する（172.20.4.150）
                t.InvokeMember("Item", _
                    System.Reflection.BindingFlags.SetProperty, _
                    Nothing, fields, New Object() {"http://schemas.microsoft.com/cdo/configuration/smtpserver", "172.20.4.150"})
                'ポート番号を指定する（25）
                t.InvokeMember("Item", _
                    System.Reflection.BindingFlags.SetProperty, _
                    Nothing, fields, New Object() {"http://schemas.microsoft.com/cdo/configuration/smtpserverport", 25})
                '
                t.InvokeMember("Update", System.Reflection.BindingFlags.InvokeMethod, Nothing, fields, New Object() {})

                '送信者
                t.InvokeMember("From", System.Reflection.BindingFlags.SetProperty, _
                    Nothing, cdo, New Object() {"SKE1システム管理担当 <morij@gkh.subaru-fhi.co.jp>"})

                'メールタイトル
                t.InvokeMember("Subject", _
                    System.Reflection.BindingFlags.SetProperty, _
                    Nothing, cdo, New Object() {"【試作部品表　改訂編集通知】"})

                '宛先及び送信内容の件数毎にメール送信を行う。
                '本文作成
                Dim strMsgBody As String = "以下のイベント、設計課、ブロックで部品表が改訂されました。"
                For i = 0 To blockList.Count - 1
                    '本文
                    strMsgBody += vbCrLf + vbCrLf + _
                                blockList.Item(i).ShisakuEventCode + "  " + _
                                blockList.Item(i).ShisakuBukaCode + "  " + _
                                blockList.Item(i).ShisakuBlockNo + "  " + _
                                blockList.Item(i).ShisakuBlockNoKaiteiNo + "  " + _
                                blockList.Item(i).ShisakuBlockName
                Next

                '本文を設定
                t.InvokeMember("Textbody", _
                    System.Reflection.BindingFlags.SetProperty, _
                    Nothing, cdo, New Object() {strMsgBody})

                'メールを送信
                Dim strSendUser As String = ""

                For i = 0 To SendList.Count - 1
                    'メール送信対象か確認。
                    '   Mailkbnが0：全て、1:改訂通知、2:イベント更新通知
                    '       この機能では0と1が対象。
                    If SendList.Item(i).MailKbn = "0" Or SendList.Item(i).MailKbn = "1" Then
                        If j > 0 Then
                            strSendUser += ","
                        End If
                        strSendUser += SendList.Item(i).MailAddress
                        '1を加算
                        j += 1
                    End If
                Next

                '宛先を設定
                t.InvokeMember("To", _
                    System.Reflection.BindingFlags.SetProperty, _
                    Nothing, cdo, New Object() {strSendUser})

                '送信する
                t.InvokeMember("Send", _
                    System.Reflection.BindingFlags.InvokeMethod, _
                    Nothing, cdo, New Object() {})

                'メール通知日を更新
                Dim blockTsuchiDao As IShisakuBuhinKaiteiBlockTsuchiDao = New ShisakuBuhinKaiteiBlockTsuchiDaoImpl()
                blockTsuchiDao.UpdateByTShisakuSekkeiBlockTsuchi(intMailTsuchiHi01, intMailTsuchiHi02)

                MsgBox("改訂通知情報のメール送信が完了しました。", MsgBoxStyle.OkOnly, "改訂通知")

            End If

        Catch ex As Exception
            ComFunc.ShowErrMsgBox(SYSERR_00001, _
                ex.Message, MethodBase.GetCurrentMethod.Name)
            g_log.WriteException(ex)
            Return
        End Try

    End Sub

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
