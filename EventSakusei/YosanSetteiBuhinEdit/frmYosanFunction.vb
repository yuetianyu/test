Imports FarPoint.Win.Spread
Imports ShisakuCommon.Ui.Spd
Imports EventSakusei.YosanSetteiBuhinEdit.Dao
Imports ShisakuCommon.Db.EBom.Vo
Imports ShisakuCommon
Imports ShisakuCommon.Db.EBom.Dao
Imports ShisakuCommon.Db.EBom.Dao.Impl
Imports FarPoint.Win.Spread.CellType

Namespace YosanSetteiBuhinEdit

    ''' <summary>
    ''' ファンクション/取引先単価設定画面
    ''' </summary>
    ''' <remarks></remarks>
    Public Class frmYosanFunction

#Region "メンバ変数"

        ''' <summary>試作イベントコード</summary>
        Private shisakuEventCode As String

        ''' <summary>予算リストコード</summary>
        Private yosanListCode As String

        Private sheet As sheetView

        Private spread As FpSpread

#End Region

#Region "定数"

        ''' <summary>ファンクション品番</summary>
        Private Const COLUMN_TAG_YOSAN_FUNCTION_HINBAN As String = "YOSAN_FUNCTION_HINBAN"

        ''' <summary>取引先コード</summary>
        Private Const COLUMN_TAG_YOSAN_MAKER_CODE As String = "YOSAN_MAKER_CODE"

        ''' <summary>単価</summary>
        Private Const COLUMN_TAG_YOSAN_TANKA As String = "YOSAN_TANKA"

        ''' <summary>備考</summary>
        Private Const COLUMN_TAG_YOSAN_BIKO As String = "YOSAN_BIKO"

#End Region

#Region "コンストラクタ"

        ''' <summary>
        ''' コンストラクタ
        ''' </summary>
        ''' <remarks></remarks>
        Public Sub New()

            ' この呼び出しは、Windows フォーム デザイナで必要です。
            InitializeComponent()

            ' InitializeComponent() 呼び出しの後で初期化を追加します。


        End Sub

        Private Sub frmYosanFunction_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
            Try
                Initialize()
            Catch ex As Exception
                Me.Close()  '' フォーム表示中の例外なので、自身を閉じて例外throw
                Throw
            End Try
        End Sub

#End Region

#Region "初期設定"

        ''' <summary>
        ''' 初期設定
        ''' </summary>
        ''' <remarks></remarks>
        Private Sub Initialize()

            'スプレッドの初期設定'
            sheet = Me.EBomSpread1_Sheet1

            spread = Me.EBomSpread1
            SpreadUtil.Initialize(spread)


            '列設定'
            InitSpreadColumn()

            'データの初期設定'
            Dim dao As YosanSetteiBuhinEditHeaderDao = New YosansetteibuhineditheaderDaoImpl
            Dim vos As New List(Of tyosansetteiBuhinFunctionVo)
            vos = dao.FindByTYosanSetteiBuhinFunction()

            'とりあえず+10行設定'
            sheet.RowCount = vos.Count + 100

            Dim rowIndex As Integer = 0
            For Each vo As TYosanSetteiBuhinFunctionVo In vos

                sheet.Cells(rowIndex, sheet.Columns(COLUMN_TAG_YOSAN_FUNCTION_HINBAN).Index).Value = vo.YosanFunctionHinban
                sheet.Cells(rowIndex, sheet.Columns(COLUMN_TAG_YOSAN_MAKER_CODE).Index).Value = vo.YosanMakerCode
                sheet.Cells(rowIndex, sheet.Columns(COLUMN_TAG_YOSAN_TANKA).Index).Value = vo.YosanTanka
                sheet.Cells(rowIndex, sheet.Columns(COLUMN_TAG_YOSAN_BIKO).Index).Value = vo.YosanBiko

                rowIndex = rowIndex + 1
            Next


        End Sub

#Region "スプレッド列設定"

        ''' <summary>
        ''' 列情報設定
        ''' </summary>
        ''' <remarks></remarks>
        Private Sub InitSpreadColumn()

            'ファンクション品番'
            Dim funcCellType As TextCellType
            funcCellType = ShisakuSpreadUtil.NewGeneralTextCellType()
            funcCellType.MaxLength = 5
            '' 小文字を大文字にする
            funcCellType.CharacterCasing = CharacterCasing.Upper
            '' 半角英数記号(Asciiだと全角も入力出来る)
            funcCellType.CharacterSet = CharacterSet.Ascii
            sheet.Columns(COLUMN_TAG_YOSAN_FUNCTION_HINBAN).CellType = funcCellType

            '取引先コード'
            Dim makerCellType As TextCellType
            makerCellType = ShisakuSpreadUtil.NewGeneralTextCellType()
            makerCellType.MaxLength = 4
            '' 小文字を大文字にする
            makerCellType.CharacterCasing = CharacterCasing.Upper
            '' 半角英数記号(Asciiだと全角も入力出来る)
            makerCellType.CharacterSet = CharacterSet.Ascii
            sheet.Columns(COLUMN_TAG_YOSAN_MAKER_CODE).CellType = makerCellType

            '単価'
            Dim tankaCellType As CurrencyCellType
            tankaCellType = ShisakuSpreadUtil.NewGeneralCurrencyCellType
            tankaCellType.MaximumValue = 9999999999
            tankaCellType.MinimumValue = 0
            tankaCellType.DecimalPlaces = 2
            sheet.Columns(COLUMN_TAG_YOSAN_TANKA).CellType = tankaCellType

            Dim bikoCellType As TextCellType
            bikoCellType = ShisakuSpreadUtil.NewGeneralTextCellType()
            bikoCellType.MaxLength = 256
            sheet.Columns(COLUMN_TAG_YOSAN_BIKO).CellType = bikoCellType

        End Sub


#End Region

#End Region

#Region "イベント"

        ''' <summary>
        ''' 登録
        ''' </summary>
        ''' <param name="sender"></param>
        ''' <param name="e"></param>
        ''' <remarks></remarks>
        Private Sub btnRegist_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnRegist.Click
            '画面ままを登録する'
            Dim vos As New List(Of TYosanSetteiBuhinFunctionVo)
            Dim aDate As New ShisakuDate
            Dim errFlg As Boolean = False
            Dim dic As New Dictionary(Of String, TYosanSetteiBuhinFunctionVo)


            For rowindex As Integer = 0 To sheet.RowCount - 1
                For columnIndex As Integer = 0 To sheet.ColumnCount - 1
                    sheet.Cells(rowindex, columnIndex).BackColor = Nothing
                Next
            Next

            For rowIndex As Integer = 0 To sheet.RowCount - 1
                Dim vo As New TYosanSetteiBuhinFunctionVo

                Dim functionHinban As String = sheet.Cells(rowIndex, sheet.Columns(COLUMN_TAG_YOSAN_FUNCTION_HINBAN).Index).Value
                Dim makerCode As String = sheet.Cells(rowIndex, sheet.Columns(COLUMN_TAG_YOSAN_MAKER_CODE).Index).Value
                Dim tanka As Decimal = sheet.Cells(rowIndex, sheet.Columns(COLUMN_TAG_YOSAN_TANKA).Index).Value
                Dim biko As String = sheet.Cells(rowIndex, sheet.Columns(COLUMN_TAG_YOSAN_BIKO).Index).Value


                If StringUtil.IsEmpty(functionHinban) _
                AndAlso StringUtil.IsEmpty(makerCode) _
                AndAlso tanka = 0 _
                AndAlso StringUtil.IsEmpty(biko) Then
                    Continue For
                End If

                'ファンクション5桁以外はエラー'
                If StringUtil.IsNotEmpty(functionHinban) Then
                    If functionHinban.Length <> 5 Then
                        'エラー'
                        sheet.Cells(rowIndex, sheet.Columns(COLUMN_TAG_YOSAN_FUNCTION_HINBAN).Index).BackColor = Color.Red
                        errFlg = True
                    End If
                Else
                    'エラー'
                    sheet.Cells(rowIndex, sheet.Columns(COLUMN_TAG_YOSAN_FUNCTION_HINBAN).Index).BackColor = Color.Red
                    errFlg = True
                End If


                '取引先コード未入力はエラー'
                If StringUtil.IsEmpty(makerCode) Then
                    'エラー'
                    sheet.Cells(rowIndex, sheet.Columns(COLUMN_TAG_YOSAN_MAKER_CODE).Index).BackColor = Color.Red
                    errFlg = True
                End If

                '単価0円はエラー'
                If tanka = 0 Then
                    'エラー'
                    sheet.Cells(rowIndex, sheet.Columns(COLUMN_TAG_YOSAN_TANKA).Index).BackColor = Color.Red
                    errFlg = True
                End If
                '備考NULLは空文字にしておく'
                If StringUtil.IsEmpty(biko) Then
                    biko = ""
                End If


                vo.YosanFunctionHinban = functionHinban
                vo.YosanMakerCode = makerCode
                vo.YosanTanka = tanka

                vo.YosanBiko = biko
                vo.CreatedUserId = loginInfo.now.userId
                vo.CreatedDate = aDate.CurrentDateDbFormat
                vo.CreatedTime = aDate.CurrentTimeDbFormat
                vo.UpdatedUserId = LoginInfo.Now.UserId
                vo.UpdatedDate = aDate.CurrentDateDbFormat
                vo.UpdatedTime = aDate.CurrentTimeDbFormat

                Dim key As String = EzUtil.MakeKey(vo.YosanFunctionHinban, vo.YosanMakerCode)

                If dic.ContainsKey(key) Then
                    sheet.Cells(rowIndex, sheet.Columns(COLUMN_TAG_YOSAN_FUNCTION_HINBAN).Index).BackColor = Color.Red
                    sheet.Cells(rowIndex, sheet.Columns(COLUMN_TAG_YOSAN_MAKER_CODE).Index).BackColor = Color.Red
                    errFlg = True
                Else
                    dic.Add(key, vo)
                End If

                vos.Add(vo)
            Next


            If errFlg Then
                MsgBox("エラーがあります。", MsgBoxStyle.OkOnly, "エラー")
                Exit Sub
            End If


            '既存削除'
            Dim dao As TYosanSetteiBuhinFunctionDao = New TYosanSetteiBuhinFunctionDaoImpl
            '全削除
            dao.DeleteBy(New TYosanSetteiBuhinFunctionVo)

            Using sqlConn As New SqlClient.SqlConnection(NitteiDbComFunc.GetConnectString)
                sqlConn.Open()
                Using tr As SqlClient.SqlTransaction = sqlConn.BeginTransaction

                    Using addData As DataTable = NitteiDbComFunc.ConvListToDataTable(vos)
                        Using bulkCopy As SqlClient.SqlBulkCopy = New SqlClient.SqlBulkCopy(sqlConn, SqlClient.SqlBulkCopyOptions.KeepIdentity, tr)
                            'マッピングが必要
                            NitteiDbComFunc.SetColumnMappings(bulkCopy, addData)

                            'タイムアウト制限
                            bulkCopy.BulkCopyTimeout = 0  ' in seconds
                            '書き込み先指定
                            bulkCopy.DestinationTableName = MBOM_DB_NAME & ".dbo.T_YOSAN_SETTEI_BUHIN_FUNCTION"
                            'ここで書き込み
                            bulkCopy.WriteToServer(addData)
                            bulkCopy.Close()
                        End Using
                    End Using

                    tr.Commit()
                End Using
            End Using

            MsgBox("登録しました。", MsgBoxStyle.OkOnly)

            Me.Close()
        End Sub

        ''' <summary>
        ''' キャンセル
        ''' </summary>
        ''' <param name="sender"></param>
        ''' <param name="e"></param>
        ''' <remarks></remarks>
        Private Sub btnCancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCancel.Click
            Me.Close()
        End Sub


#End Region

    End Class
End Namespace
