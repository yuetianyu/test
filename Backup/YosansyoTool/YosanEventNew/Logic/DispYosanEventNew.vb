Imports EBom.Data
Imports EBom.Common
Imports EBom.Common.mdlConstraint
Imports ShisakuCommon.Util.LabelValue
Imports ShisakuCommon
Imports ShisakuCommon.Db.EBom
Imports ShisakuCommon.Db.EBom.Dao
Imports ShisakuCommon.Db.EBom.Dao.Impl
Imports ShisakuCommon.Db.EBom.Vo
Imports ShisakuCommon.Ui
Imports ShisakuCommon.Util
Imports YosansyoTool.YosanEventNew.Dao.Impl

Namespace YosanEventNew.Logic

    Public Class DispYosanEventNew : Inherits Observable

#Region " メンバー変数 "
        ''' <summary>ビュー</summary>
        Private m_view As FrmYosanEventNew
        ''' <summary>イベント</summary>
        Private _eventCode As String
        ''' <summary>リジェストエラー</summary>
        Private _registError As Boolean
        Private ReadOnly aHeaderSubject As DispYosanEventNewHeader
        Private ReadOnly aLoginInfo As LoginInfo
        Private ReadOnly aShisakuDate As ShisakuDate
        Private aEventDao As TYosanEventDao
        Private aExclusiveDao As TYosanExclusiveControlEventDao
#End Region

#Region " プロパテイー "
        ''' <summary>リジェストエラー</summary>
        ''' <returns>リジェストエラー</returns>
        Public ReadOnly Property RegistError() As Boolean
            Get
                Return _registError
            End Get
        End Property

        ''' <summary>ヘッダーサブジェクト</summary>
        ''' <returns>ヘッダーサブジェクト</returns>
        Public ReadOnly Property HeaderSubject() As DispYosanEventNewHeader
            Get
                Return aHeaderSubject
            End Get
        End Property
#End Region

#Region " コンストラクタ "
        ''' <summary>
        ''' コンストラクタ
        ''' </summary>
        ''' <param name="f">画面対象</param>
        ''' <param name="aEventCode">イベント</param>
        ''' <param name="aLoginInfo">ログイン情報</param>
        ''' <remarks></remarks>
        Public Sub New(ByVal f As FrmYosanEventNew, _
                       ByVal aEventCode As String, _
                       ByVal aLoginInfo As LoginInfo)
            m_view = f
            Me._eventCode = aEventCode
            Me.aLoginInfo = aLoginInfo
            Me.aShisakuDate = New ShisakuDate
            aEventDao = New TYosanEventDaoImpl
            aExclusiveDao = New TYosanExclusiveControlEventDaoImpl

            Me._registError = False

            Me.aHeaderSubject = New DispYosanEventNewHeader(_eventCode, _
                                                            aLoginInfo, _
                                                            New MShisakuKaihatufugoDaoImpl, _
                                                            aEventDao, _
                                                            New YosanEventNewDaoImpl, _
                                                            aShisakuDate)
            SetChanged()

        End Sub
#End Region

#Region " ビュー初期化 "
        ''' <summary>
        ''' ビューの初期化
        ''' </summary>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Function InitView() As RESULT

            '初期起動時には選択ボタンを使用不可とする。
            ShisakuFormUtil.setTitleVersion(m_view)
            ShisakuFormUtil.SetIdAndBuka(m_view.LblLoginUserId, m_view.LblLoginBukaName)
            ShisakuFormUtil.SetDateTimeNow(m_view.LblDateNow, m_view.LblTimeNow)

            '画面のPG-IDが表示されます。
            m_view.LblCurrPGId.Text = "PG-ID :" + "EVENT NEW"

            ShisakuFormUtil.SettingDefaultProperty(m_view.cmbKubun)
            m_view.cmbKubun.ImeMode = Windows.Forms.ImeMode.Hiragana

            ShisakuFormUtil.SettingDefaultProperty(m_view.cmbKaihatsuFugo)
            m_view.cmbKaihatsuFugo.MaxLength = 4
            m_view.cmbKaihatsuFugo.ImeMode = Windows.Forms.ImeMode.Disable

            ShisakuFormUtil.SettingDefaultProperty(m_view.cmbEvent)
            m_view.cmbEvent.ImeMode = Windows.Forms.ImeMode.Hiragana

            ShisakuFormUtil.SettingDefaultProperty(m_view.txtEventName)
            m_view.txtEventName.ImeMode = Windows.Forms.ImeMode.Hiragana

            ShisakuFormUtil.SettingDefaultProperty(m_view.txtYosanCode)
            m_view.txtYosanCode.MaxLength = 2
            m_view.txtYosanCode.ImeMode = Windows.Forms.ImeMode.Disable

            ShisakuFormUtil.SettingDefaultProperty(m_view.txtMainHenkoGaiyo)
            m_view.txtMainHenkoGaiyo.ImeMode = Windows.Forms.ImeMode.Hiragana

            ShisakuFormUtil.SettingDefaultProperty(m_view.txtTsukurikataSeisakujyoken)
            m_view.txtTsukurikataSeisakujyoken.ImeMode = Windows.Forms.ImeMode.Hiragana

            ShisakuFormUtil.SettingDefaultProperty(m_view.txtSonota)
            m_view.txtSonota.ImeMode = Windows.Forms.ImeMode.Hiragana

            'SettingDefaultProperty(m_view.cmbEventName)
            'SettingDefaultProperty(m_view.cmbKoujiShireiNo)
            'SettingDefaultProperty(m_view.cmbKanseisha)
            'SettingDefaultProperty(m_view.cmbWb)

        End Function
#End Region

#Region " 登録／保存 "
        ''' <summary>
        ''' 登録
        ''' </summary>
        ''' <remarks></remarks>
        Public Sub Register()
            Using db As New EBomDbClient
                db.BeginTransaction()
                Try
                    aShisakuDate.Clear()
                    aHeaderSubject.Register()
                Catch ex As Exception
                    db.Rollback()
                    MsgBox("イベントを登録出来ませんでした。不正な文字が入力されている可能性があります。")
                    _registError = True
                    Return
                End Try

                db.Commit()
                _registError = False
            End Using
        End Sub

        ''' <summary>
        ''' 保存
        ''' </summary>
        ''' <remarks></remarks>
        Public Sub Save()
            Using db As New EBomDbClient
                db.BeginTransaction()
                Try
                    aShisakuDate.Clear()
                    aHeaderSubject.Save()
                Catch ex As Exception
                    db.Rollback()
                    MsgBox("イベントを登録出来ませんでした。不正な文字が入力されている可能性があります。")
                    _registError = True
                    Return
                End Try

                db.Commit()
                _registError = False
            End Using
        End Sub
#End Region

        '#Region " イベント情報排他管理 "
        '        ''' <summary>
        '        ''' イベント情報排他管理からデータ削除
        '        ''' </summary>
        '        ''' <remarks></remarks>
        '        Public Sub DeleteExclusiveEvent()
        '            Using db As New EBomDbClient
        '                db.BeginTransaction()
        '                Try
        '                    If IsAddMode() Then
        '                    Else
        '                        aShisakuDate.Clear()
        '                        Dim vo As TYosanExclusiveControlEventVo = aExclusiveDao.FindByPk(aHeaderSubject.YosanEventCode, aLoginInfo.UserId)
        '                        If vo Is Nothing Then
        '                        Else
        '                            aExclusiveDao.DeleteByPk(aHeaderSubject.YosanEventCode, aLoginInfo.UserId)
        '                        End If
        '                    End If
        '                Catch ex As Exception
        '                    db.Rollback()
        '                    MsgBox("イベント情報排他管理から、イベントを削除出来ませんでした。")
        '                    _registError = True
        '                    Return
        '                End Try

        '                db.Commit()
        '                _registError = False
        '            End Using
        '        End Sub
        '#End Region

#Region " チェックについて "
        ''' <summary>
        ''' 該当データは既に登録するかどうか
        ''' </summary>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Function IsExist() As Boolean
            Try
                Dim vo As TYosanEventVo = aEventDao.FindByPk(_eventCode)
                If vo Is Nothing Then
                    Return False
                Else
                    Return True
                End If
            Catch ex As Exception
                Throw ex
            End Try
        End Function

        ''' <summary>
        ''' 登録モードかどうか
        ''' </summary>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Function IsAddMode() As Boolean
            Return _eventCode Is Nothing OrElse StringUtil.IsEmpty(_eventCode)
        End Function
#End Region

#Region "数値8桁で格納された日付をYYYY/MM/DD形式に変換"
        ''' <summary>
        ''' 数値8桁で格納された日付をYYYY/MM/DD形式に変換
        ''' 
        '''  ※　8桁未満数値の場合はString.Emptyを返す
        '''  ※  8桁で変換不可の場合はThrow 
        ''' 
        ''' </summary>
        ''' <param name="aIntDate"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Shared Function ConvInt8ToDataStr(ByVal aIntDate As Integer) As String
            Dim strDate As String = aIntDate.ToString.Trim

            '変換対象外判定(99999999はとりあえず除外)
            If strDate.Equals(String.Empty) OrElse strDate.Equals("0") OrElse strDate.Equals("99999999") OrElse strDate.Length <= 7 Then
                Return String.Empty
            End If

            strDate = String.Format("{0}/{1}/{2}", strDate.Substring(0, 4), strDate.Substring(4, 2), strDate.Substring(6, 2))

            If IsDate(strDate) = False Then
                Throw New Exception(String.Format("日付型に変換できない8桁数値が見つかりました。管理者に連絡してください。(対象数値:{0})", aIntDate.ToString))
            End If

            Return strDate
        End Function
#End Region

#Region "文字型(YYYY/MM/DD)をInt8桁に変換 "
        ''' <summary>
        ''' 文字型(YYYY/MM/DD)をInt8桁に変換
        ''' </summary>
        ''' <param name="aStrDate"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Shared Function ConvDateStrToInt8(ByVal aStrDate As String) As Integer
            Dim wDate As Date = Nothing
            Dim intDate As Integer = Nothing

            If IsDate(aStrDate) Then
                wDate = Date.Parse(aStrDate)
                intDate = wDate.ToString("yyyyMMdd")
            Else
                intDate = 0
            End If

            Return intDate
        End Function
#End Region

    End Class

End Namespace
