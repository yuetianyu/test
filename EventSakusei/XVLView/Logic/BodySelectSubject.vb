Imports ShisakuCommon
Imports ShisakuCommon.Db
Imports ShisakuCommon.Db.EBom
Imports EBom.Common
Imports EBom.Data

Namespace XVLView.Logic


    Public Class BodySelectSubject


#Region "メンバ変数"
        Private mEventCode As String
        Private mKaihatsuFugo As String
#End Region

#Region "プロパティー"
        ''' <summary>
        ''' ブロック№.
        ''' </summary>
        ''' <value></value>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public ReadOnly Property EventCode() As String
            Get
                Return mEventCode
            End Get
        End Property

        ''' <summary>
        ''' 開発符号.
        ''' </summary>
        ''' <value></value>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Property KaihatsuFugo() As String
            Get
                Return mKaihatsuFugo
            End Get
            Set(ByVal value As String)
                mKaihatsuFugo = value
            End Set
        End Property
#End Region

#Region "コンストラクタ"

        ''' <summary>
        ''' コンストラクタ
        ''' </summary>
        ''' <param name="aEventCode"></param>
        ''' <remarks></remarks>
        Public Sub New(ByVal aEventCode As String)

            'イベントコードの退避.
            mEventCode = aEventCode

            'Initialize()

        End Sub

#End Region

        Private Sub Initialize()

            '開発符号を設定.
            Initialize_KaihatsuFugo(mEventCode)


        End Sub


        ''' <summary>
        ''' ボディーマスタからデータを抽出してボディーコンボボックスにアイテムを設定する.
        ''' </summary>
        ''' <param name="aComboBox"></param>
        ''' <remarks></remarks>
        Public Function setBodyTypeComboBox(ByRef aComboBox As System.Windows.Forms.ComboBox) As Boolean

            'Dim iDao As New Dao.Impl.BodyMSTMainteImpl()
            'Dim iArg As New Dao.Vo.BodyMSTMainteVO
            Dim sFileList As String() = Nothing
            Dim sDirectory As String
            Dim sFileParam As String

            'iArg.KaihatsuFugo = mKaihatsuFugo

            'BODY情報をDBからの取得ではなくBODY用3D画像リストから取得するように変更

            'Dim iVos As List(Of Dao.Vo.BodyMSTMainteVO) = iDao.SelectBodyList(iArg)

            ''取得したボディ名をコンボボックスに設定.
            'aComboBox.Items.Add("")
            'For Each lItem In iVos
            '    aComboBox.Items.Add(lItem.BodyName)
            'Next

            'コンボボックスクリア
            aComboBox.Items.Clear()

            '3Dファイル取得先情報設定
            sDirectory = ShisakuCommon.ShisakuGlobal.XVLFileBodyDir
            'アクセス権テスト用'
            'sDirectory = "\\Fgnt32\E-BOM共有\"

            '開発符号が選択されている場合は、対象の3Dファイルのみを取得する
            If mKaihatsuFugo = String.Empty Then
                sFileParam = "*.xv*"
            Else
                sFileParam = "*" & mKaihatsuFugo & "*.xv*"
            End If

            Try
                sFileList = System.IO.Directory.GetFiles(sDirectory, sFileParam, System.IO.SearchOption.AllDirectories)
            Catch ex As UnauthorizedAccessException
                MessageBox.Show("アクセス権がありません。BODY情報が取得できませんでした。", "3D情報", MessageBoxButtons.OK, MessageBoxIcon.Information)
                Return False
            Catch ex As Exception
                MessageBox.Show("3D情報がありません。", "3D情報", MessageBoxButtons.OK, MessageBoxIcon.Information)

            End Try

            '取得したBODY用3Dファイル名をコンボボックスに設定
            If Not sFileList Is Nothing Then
                For Each sFileName In sFileList
                    If StringUtil.IsNotEmpty(sFileName) Then
                        aComboBox.Items.Add(System.IO.Path.GetFileName(sFileName))
                    End If
                Next
            End If
            Return True
        End Function

        ''' <summary>
        ''' 開発符号コンボボックスのアイテム設定.
        ''' </summary>
        ''' <remarks></remarks>
        Public Sub SetItemToCmbKaihatuFugo(ByRef aComboObject As ComboBox)
            '開発符号一覧を取得してコンボボックスに設定する.

            Dim m_dtItem As New DataTable

            'データ取得
            Using db As New SqlAccess(g_kanrihyoIni(DB_KEY_mBOM))
                db.Fill(DataSqlCommon.GetDevSignSql(), m_dtItem)
            End Using
            EBom.Common.ComFunc.CreateComboBox(aComboObject, m_dtItem, "VALUE", "DISPLAY", True, , )

        End Sub

        ''' <summary>
        ''' イベントコードをベースに開発符号を取得する.
        ''' </summary>
        ''' <param name="aEventCode"></param>
        ''' <remarks></remarks>
        Private Sub Initialize_KaihatsuFugo(ByVal aEventCode As String)
            Dim iDao As New ShisakuCommon.Db.EBom.Dao.Impl.TShisakuEventDaoImpl()
            Dim iVos As ShisakuCommon.Db.EBom.Vo.TShisakuEventVo
            iVos = iDao.FindByPk(aEventCode)

            '開発符号を設定.
            mKaihatsuFugo = iVos.ShisakuKaihatsuFugo

        End Sub


    End Class
End Namespace
