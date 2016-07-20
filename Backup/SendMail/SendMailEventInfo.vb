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


Module subSendMailEventInfo

    Sub Main()

        Try

            'アプリケーション初期化
            InitApplication()

            '初期
            Dim SendList As List(Of MUserMailAddressVo)
            Dim i, j, intCnt As Integer
            Dim strMainFolder As String = "\\fgnt30\pt\試作イベント管理\試作イベント情報"

            '宛先情報を取得
            Dim sendDao As IMUserMailAddressDao = New MUserMailAddressDaoImpl()
            SendList = sendDao.GetSendList()

            '送信内容を取得
            '   strMainFolder以下のファイルをすべて取得
            '   ワイルドカード"*"は、すべてのファイルを意味する
            Dim strEventfiles As String() = System.IO.Directory.GetFiles( _
                    strMainFolder, "*", System.IO.SearchOption.AllDirectories)

            '改訂通知情報が無ければ処理終了
            If strEventfiles.Length = 0 Then
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
                    Nothing, cdo, New Object() {"【試作イベント管理　更新通知】"})

                '宛先及び送信内容の件数毎にメール送信を行う。
                '本文作成
                Dim strMsgBody As String = "以下のファイルが更新されました。"
                For i = 0 To strEventfiles.Length - 1

                    ' 更新日時を取得する
                    Dim fileUpdate As DateTime = System.IO.File.GetLastWriteTime(strEventfiles(i))
                    Dim nowDateTime As DateTime = DateTime.Now

                    '実行した日時から１７時間前の日付を計算する。10時に起動
                    Dim startDateTime As DateTime = nowDateTime.AddHours(-17)
                    '実行した日時から４時間前の日付を計算する。14時に起動
                    'Dim startDateTime As DateTime = nowDateTime.AddHours(-4)
                    '実行した日時から３時間前の日付を計算する。17時に起動
                    'Dim startDateTime As DateTime = nowDateTime.AddHours(-3)

                    '２時間前までに該当するかチェック。
                    '拡張子がMDBファイル、LDBファイルはシステムなので除外する。
                    '   該当したらメール本文へ追加する。
                    If startDateTime <= fileUpdate And Right(strEventfiles(i), 4) <> ".mdb" _
                                                   And Right(strEventfiles(i), 4) <> ".ldb" Then
                        '本文
                        strMsgBody += vbCrLf + vbCrLf + _
                                    "<<file://" + strEventfiles(i) + ">>  更新日： " + _
                                    fileUpdate

                        '更新ファイル件数
                        intCnt = +1
                    End If

                Next

                'ファイルが無ければ処理終了
                If intCnt = 0 Then
                    Exit Sub
                End If

                '本文を設定
                t.InvokeMember("Textbody", _
                    System.Reflection.BindingFlags.SetProperty, _
                    Nothing, cdo, New Object() {strMsgBody})

                'メールを送信
                Dim strSendUser As String = ""

                For i = 0 To SendList.Count - 1
                    'メール送信対象か確認。
                    '   Mailkbnが0：全て、1:改訂通知、2:イベント更新通知
                    '       この機能では0と2が対象。
                    If SendList.Item(i).MailKbn = "0" Or SendList.Item(i).MailKbn = "2" Then
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

                'MsgBox("更新通知情報のメール送信が完了しました。", MsgBoxStyle.OkOnly, "イベント更新通知")

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
