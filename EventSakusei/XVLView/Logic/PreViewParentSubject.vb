Imports EventSakusei.ShisakuBuhinEdit.Kosei
Imports ShisakuCommon

Imports EventSakusei.XVLView
Imports EventSakusei.XVLView.Dao.Vo
Imports EventSakusei.XVLView.Dao.Impl
Imports FarPoint.Win.Spread.CellType

''' <summary>
''' 3Dツール
''' </summary>
''' <remarks></remarks>
Public Class PreViewParentSubject

    'スプレッド開始位置
    Private Const FHINBANSTARTPOS As Integer = 13                           'F品番開始位置
    Private Const FHINBANCOUNT As Integer = 14                              'F品番員数数
    Private Const FHINBANENDPOS As Integer = FHINBANSTARTPOS + FHINBANCOUNT '員数終了位置.
    Private column As Integer
    Private Shadows Const DETAIL_ROW As Integer = 4

    ''' <summary>開発シリーズ</summary>
    Private _KaihatsuSerise As New ArrayList
    ''' <summary>部品番号</summary>
    Private _BuhinNo As New ArrayList
    ''' <summary>部品名称</summary>
    Private _BuhinName As New ArrayList
    ''' <summary>補助名称</summary>
    Private _HojyoName As New ArrayList
    ''' <summary>親フォーム</summary>
    Private _MdiForm As PreViewParent
    ''' <summary>選択員数列</summary>
    Dim mRange As FarPoint.Win.Spread.Model.CellRange

    Private _Frm41 As Frm41DispShisakuBuhinEdit20

    Private _FrmParts As frmParts

    '表示するファイルリスト(ファイル名,表示可否).
    Public mXLVFileList As Dictionary(Of String, String)

    '表示するファイルリスト(ファイル名,フルパス).
    Public mXLVFilePathList As Dictionary(Of String, String)

    'XLVウィンドハンドルを保持.(F品番、ウィンドハンドル）
    Private mXVLWindowList As Dictionary(Of String, FrmVeiwerImge)
    Private mXVLWindowMoreBodyList As Dictionary(Of String, FrmVeiwerImge)

    Dim mFiles As List(Of String)                                           '指定フォルダ内3Dファイル一覧
    Private mEventCode As String                                            'イベントコード.
    Private mKaihatsuFugo As String                                         '開発符号
    Private mBlockNo As String                                              'ブロック№
    Private mBodyName As String                                             '選択中ボディー名.

    ''' <summary>HTMLファイル格納</summary>
    Private xvlHtmlLIst As List(Of String)

#Region "プロパティ"

    ''' <summary>
    ''' ボディー名称
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property BodyName() As String
        Get
            Return mBodyName
        End Get
        Set(ByVal value As String)
            mBodyName = value
        End Set
    End Property

#End Region

#Region "コンストラクタ"

    ''' <summary>
    ''' コンストラクタ
    ''' </summary>
    ''' <param name="aEventCode">イベントコード</param>
    ''' <param name="blockNo">ブロック№</param>
    ''' <param name="frm41"></param>
    ''' <param name="mdiForm">子画面を追加するフォーム.</param>
    ''' <param name="aRange">親画面の員数選択列情報</param>
    ''' <remarks></remarks>
    Public Sub New(ByVal aEventCode As String, _
                   ByVal blockNo As String, _
                   ByVal frm41 As Frm41DispShisakuBuhinEdit20, _
                   ByVal mdiForm As PreViewParent, _
                   ByVal aRange As FarPoint.Win.Spread.Model.CellRange)

        'パラメータ取得
        mEventCode = aEventCode
        mBlockNo = blockNo
        _Frm41 = frm41

        '親フォーム
        _MdiForm = mdiForm

        '員数の選択列情報.
        mRange = aRange

        'ボディー名の初期化
        mBodyName = ""

        _FrmParts = New frmParts(Me)

        Initialize()
    End Sub

#End Region

    Private Sub Initialize()

        '_MdiForm.spdParts_Sheet1.OperationMode = FarPoint.Win.Spread.OperationMode.ReadOnly

        getKaihatsuFugo()

    End Sub

    ''' <summary>
    ''' 開発符号の取得.
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub getKaihatsuFugo()
        Dim iDao As New ShisakuCommon.Db.EBom.Dao.Impl.TShisakuEventDaoImpl()
        Dim iVos As ShisakuCommon.Db.EBom.Vo.TShisakuEventVo
        iVos = iDao.FindByPk(mEventCode)

        '開発符号を設定.
        mKaihatsuFugo = iVos.ShisakuKaihatsuFugo

    End Sub


    Public Sub InitSpd()
        _FrmParts.spdParts_Sheet1.RowCount = _Frm41.spdParts_Sheet1.RowCount
        column = FHINBANENDPOS
        '+2にする'
        If _FrmParts.spdParts_Sheet1.ColumnCount < _Frm41.spdParts_Sheet1.ColumnCount + 2 Then
            Dim border As New ShisakuCommon.Ui.Spd.SpreadBorderFactory
            'F品番列を増やす'

            For columnIndex As Integer = 0 To _Frm41.spdParts_Sheet1.ColumnCount - _FrmParts.spdParts_Sheet1.ColumnCount + 2
                column = FHINBANENDPOS + columnIndex
                _FrmParts.spdParts_Sheet1.Columns.Add(FHINBANENDPOS, 1)
                With _FrmParts.spdParts_Sheet1.Columns(FHINBANENDPOS)
                    Dim celltype As TextCellType = ShisakuCommon.Ui.Spd.ShisakuSpreadUtil.NewGeneralTextCellType()
                    celltype.MaxLength = 2
                    .CellType = celltype
                    .Width = 20.0!
                    .HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center
                End With
                ' セル（INSTL品番）
                With _FrmParts.spdParts_Sheet1.Cells(2, FHINBANENDPOS)
                    ' .CellTypeは行に設定済み
                    .VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Top
                End With
                ' セル（INSTL品番区分）
                With _FrmParts.spdParts_Sheet1.Cells(3, FHINBANENDPOS)
                    ' .CellTypeは行に設定済み
                    .Border = border.GetUnderLine()
                    .VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Top
                    .RowSpan = 1 ' ALと違う
                End With
            Next

            For spreadColumn As Integer = FHINBANSTARTPOS To column
                ' セル（"A"とか"AB"とか）
                With _FrmParts.spdParts_Sheet1.Cells(1, spreadColumn)
                    ' .CellTypeは行に設定済み
                    .Border = border.GetUnderLine()
                    .Value = EzUtil.ConvIndexToAlphabet(spreadColumn - FHINBANSTARTPOS)
                    .BackColor = System.Drawing.SystemColors.Control

                End With

                With _FrmParts.spdParts_Sheet1.Cells(2, spreadColumn)
                    Dim celltype As New TextCellType
                    celltype.TextOrientation = FarPoint.Win.TextOrientation.TextTopDown
                    .CellType = celltype
                    .BackColor = Color.White
                End With
                With _FrmParts.spdParts_Sheet1.Cells(3, spreadColumn)
                    Dim celltype As New TextCellType
                    celltype.TextOrientation = FarPoint.Win.TextOrientation.TextTopDown
                    .CellType = celltype
                    .BackColor = Color.White
                    .VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Top
                End With
            Next

            With _FrmParts.spdParts_Sheet1.Cells(0, FHINBANSTARTPOS)
                ' .CellTypeは行に設定済み
                .BackColor = System.Drawing.SystemColors.Control
                .Border = border.GetTopWLine() ' ALと違う
                .ColumnSpan = column - FHINBANSTARTPOS + 1
                .Value = "員数" ' ALと違う
            End With


        End If


        setDataHeader()

        setDataDetail()

    End Sub

    ''' <summary>
    ''' INSTL品番列を挿入する
    ''' </summary>
    ''' <param name="baseInstlColumnIndex">基点となるINSTL品番列index (0 start)</param>
    ''' <param name="columnCount">挿入列数</param>
    ''' <remarks></remarks>
    Public Sub InsertColumnInstl(ByVal baseInstlColumnIndex As Integer, ByVal columnCount As Integer)

        'Dim spreadStartColumn As Integer = FHINBANCOUNT + baseInstlColumnIndex

        ''Dim oldInstlColumnCount As Integer = Me._instlColumnCount
        'InsertColumns(spreadStartColumn, columnCount)

        '' ** 挿入した列に新規設定 **
        'For columnIndex As Integer = 0 To columnCount - 1
        '    Dim spreadColumn As Integer = spreadStartColumn + columnIndex
        '    ' 列
        '    With sheet.Columns(spreadColumn)
        '        .CellType = GetInstlInsuCellType()
        '        .Width = 20.0!
        '        .HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center
        '    End With
        '    ' セル（INSTL品番）
        '    With sheet.Cells(INSTL_HINBAN_ROW_INDEX, spreadColumn)
        '        ' .CellTypeは行に設定済み
        '        .VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Top
        '    End With
        '    ' セル（INSTL品番区分）
        '    With sheet.Cells(INSTL_HINBAN_KBN_ROW_INDEX, spreadColumn)
        '        ' .CellTypeは行に設定済み
        '        .Border = borderFactory.GetUnderLine()
        '        .VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Top
        '        .RowSpan = 1 ' ALと違う
        '    End With
        'Next

        '' ** 挿入した列と、その右の既存の列に設定 **
        'For spreadColumn As Integer = INSTL_INFO_START_COLUMN + baseInstlColumnIndex To INSTL_INFO_START_COLUMN + oldInstlColumnCount + columnCount - 1
        '    ' セル（"A"とか"AB"とか）
        '    With sheet.Cells(INSTL_HINBAN_NO_ROW_INDEX, spreadColumn)
        '        ' .CellTypeは行に設定済み
        '        .Border = borderFactory.GetUnderLine()
        '        .Value = EzUtil.ConvIndexToAlphabet(spreadColumn - INSTL_INFO_START_COLUMN)
        '    End With
        'Next

        'With sheet.Cells(INSTL_HINBAN_MIDASHI_ROW_INDEX, INSTL_INFO_START_COLUMN)
        '    ' .CellTypeは行に設定済み
        '    .BackColor = System.Drawing.SystemColors.Control
        '    .Border = borderFactory.GetTopWLine() ' ALと違う
        '    .ColumnSpan = oldInstlColumnCount + columnCount
        '    .Value = "員数" ' ALと違う
        'End With

        'Me._instlColumnCount += columnCount
    End Sub



    Private Sub setDataHeader()

        For nRow As Integer = 2 To 3
            With _FrmParts.spdParts_Sheet1
                For nCol As Integer = 11 To column - 2
                    'テキスト側に反映されていない'
                    .Cells(nRow, nCol + 2).Text = _Frm41.spdParts_Sheet1.Cells(nRow + 1, nCol).Text

                    If nRow = 2 Then
                        'F品番を設定する.
                        '品番が存在しない場合は次の品番へ.
                        If StringUtil.IsEmpty(_Frm41.spdParts_Sheet1.Cells(nRow + 1, nCol).Value) Then
                            If isFhinban(nCol + 2) Then
                                '列非表示.
                                .Columns(nCol + 2).Visible = False
                            End If
                            Continue For
                        End If


                        'F品番をスプレッドのタグ名として指定.
                        .Columns(nCol + 2).Tag = _Frm41.spdParts_Sheet1.Cells(nRow + 1, nCol).Value

                        '選択されたF品番以外は非表示にする.
                        If Not (mRange.Column <= nCol AndAlso nCol <= (mRange.Column + mRange.ColumnCount - 1)) Then
                            If isFhinban(nCol + 2) Then
                                '列非表示.
                                .Columns(nCol + 2).Visible = False
                            End If
                        End If


                    End If
                Next
            End With
        Next

    End Sub

    Private Sub setDataDetail()

        For nRow As Integer = DETAIL_ROW To _Frm41.spdParts_Sheet1.RowCount - 1
            '2014/04/09 kabasawa'
            '適用の無い部品は非表示にする'
            With _FrmParts.spdParts_Sheet1
                For nCol As Integer = 0 To _Frm41.spdParts_Sheet1.ColumnCount - 1
                    .Cells(nRow, nCol + 2).Value = _Frm41.spdParts_Sheet1.Cells(nRow, nCol).Value
                Next
            End With
        Next
        With _FrmParts.spdParts_Sheet1
            For rowIndex As Integer = DETAIL_ROW To _FrmParts.spdParts_Sheet1.RowCount - 1
                Dim rowVisble As Boolean = False
                For columnIndex As Integer = mRange.Column + 2 To mRange.Column + mRange.ColumnCount - 1 + 2
                    If StringUtil.IsNotEmpty(.Cells(rowIndex, columnIndex).Text) Then
                        rowVisble = True
                        Exit For
                    End If
                Next

                If Not rowVisble Then
                    .Rows(rowIndex).Visible = False
                End If
            Next
        End With

    End Sub

    ''' <summary>
    ''' 3D有無チェック
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub chkFileUmu()

        '3Dデータの取得
        'Dim iFiles As String() = getFileName3D()
        mFiles = New List(Of String)(getFileName3D())

        With _FrmParts.spdParts_Sheet1

            mXLVFileList = New Dictionary(Of String, String)
            mXLVFilePathList = New Dictionary(Of String, String)

            For lRow As Integer = DETAIL_ROW To _FrmParts.spdParts_Sheet1.RowCount - 1

                '部品番号が空白であれば、設定しない。
                If StringUtil.IsEmpty(.Cells(lRow, 8).Value) Then
                    .Cells(lRow, 0).Locked = True
                    .Cells(lRow, 0).Value = False
                End If

                '2014/04/09 kabasawa'
                '非表示行は対象外にする'
                If Not .Rows(lRow).Visible Then
                    .Cells(lRow, 0).Locked = True
                    .Cells(lRow, 0).Value = False
                    .Cells(lRow, 1).Value = "×"
                    Continue For
                End If


                If mFiles Is Nothing Then

                    .Cells(lRow, 0).Locked = True
                    .Cells(lRow, 0).Value = False
                    .Cells(lRow, 1).Value = "×"

                Else
                    '部品番号
                    Dim iBuhinNo As String = ""
                    'ファイル名
                    Dim iFileName As String = ""

                    '
                    iBuhinNo = .Cells(lRow, 8).Value
                    If StringUtil.IsEmpty(iBuhinNo) Then Continue For

                    Dim iVo As New List(Of RHAC2270Vo)
                    Try
                        'ファイル名称取得用のパラメータを作成.
                        Dim iArg As New RHAC2270Vo()
                        iArg.KaihatsuFugo = mKaihatsuFugo

                        If iBuhinNo.Length > 10 Then
                            iArg.BuhinNo = Left(iBuhinNo, 10)
                        Else
                            iArg.BuhinNo = iBuhinNo
                        End If

                        iArg.BlockNo = mBlockNo
                        iVo = get3DFileName(iArg)
#If DEBUG Then
                    Catch ex As Exception
                        MessageBox.Show(ex.Message)
#End If
                    Finally
                    End Try

                    '抽出件数チェック.
                    If 1 < iVo.Count Then Throw New Exception("複数のレコードが抽出されました.条件を見直してください.")

                    'ファイル名の取得.
                    If iVo.Count <> 0 Then iFileName = iVo(0).XvlFileName

                    If StringUtil.IsNotEmpty(iFileName) Then
                        '部品番号で絞り込む
                        'Dim iBuhinNoFiles As String() = Array.FindAll(iFiles, Function(str As String) str.Contains(iFileName))

                        'If 1 < iFileName.Length Then Throw New Exception("複数ファイルが抽出されました。条件を見直してください.")


                        'If mFiles.Contains(XVLFileDir & XVLFileSubDir & "\" & iFileName) Then
                        '    '
                        '    mXLVFileList.Add(iBuhinNo, iFileName)
                        '    .Cells(lRow, 1).Value = "○"
                        '    'チェックボックス = ON(1)
                        '    .Cells(lRow, 0).Value = True

                        'Else
                        '    'ファイルが存在しない
                        '    .Cells(lRow, 0).Locked = True
                        '    .Cells(lRow, 0).Value = False
                        '    .Cells(lRow, 1).Value = "×"

                        'End If

                        '指定ディレクトリから取得したパス付ファイル名にDBから取得したファイル名が含まれるか
                        Dim SearchFlg As Integer = 0
                        For Each sFileName In mFiles
                            If sFileName.IndexOf(iFileName) >= 0 Then
                                '同一部品が有る場合は追加しない
                                If Not mXLVFileList.ContainsKey(iBuhinNo) Then
                                    mXLVFileList.Add(iBuhinNo, iFileName)
                                    mXLVFilePathList.Add(iFileName, sFileName)
                                End If

                                .Cells(lRow, 1).Value = "○"
                                'チェックボックス = ON(1)
                                .Cells(lRow, 0).Value = True
                                SearchFlg = 1
                                Exit For
                            End If
                        Next

                        'ファイルが存在しない
                        If SearchFlg = 0 Then
                            .Cells(lRow, 0).Locked = True
                            .Cells(lRow, 0).Value = False
                            .Cells(lRow, 1).Value = "×"
                        End If
                    Else
                        'ファイルが存在しない
                        .Cells(lRow, 0).Locked = True
                        .Cells(lRow, 0).Value = False
                        .Cells(lRow, 1).Value = "×"
                    End If
                End If
            Next

        End With

    End Sub

    ''' <summary>
    ''' 3Dファイル名称取得
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function getFileName3D() As String()
        Dim iFiles As String() = Nothing
        Dim i3DFileList As New List(Of String)

        Try
            Dim aDirectory As String = ShisakuCommon.ShisakuGlobal.XVLFileDir1 & mKaihatsuFugo & ShisakuCommon.ShisakuGlobal.XVLFileDir2

            Dim aFileParam As String = "*" & mBlockNo & "*.xv*"
            iFiles = System.IO.Directory.GetFiles(aDirectory, aFileParam, System.IO.SearchOption.AllDirectories)

            i3DFileList = New List(Of String)(iFiles)
        Catch ex As Exception
            Debug.WriteLine("3D情報がありません。", "3D情報" & ex.Message)
        End Try

        If i3DFileList.Count = 0 Then
            MessageBox.Show("3D情報がありません。", "3D情報", MessageBoxButtons.OK, MessageBoxIcon.Information)
        End If

        Return i3DFileList.ToArray
    End Function

#Region "DBアクセス"


    ''' <summary>
    ''' DBからファイル名の取得
    ''' </summary>
    ''' <param name="aArg">開発符号、ブロック番号、部品番号を指定.</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function get3DFileName(ByVal aArg As RHAC2270Vo) As List(Of RHAC2270Vo)
        '開発符号、ブロック番号、部品番号からファイル名を取得
        Dim iDao As New RHAC2270DaoImpl
        Dim iVos As New List(Of RHAC2270Vo)

        '上から順に優先度高、
        '優先順位はキャドデータ区分が
        'G>ND>S3>S2>S1
        '上記で存在しない場合は
        'ファイル拡張子をチェック

        'キャドデータイベント区分”G”で検索
        aArg.CadDataEventKbn = "G"
        iVos = iDao.getKbnXVLFileName(aArg)
        If iVos.Count <> 0 Then Return iVos

        'キャドデータイベント区分”ND”で検索
        aArg.CadDataEventKbn = "ND"
        iVos = iDao.getKbnXVLFileName(aArg)
        If iVos.Count <> 0 Then Return iVos

        ''ここでファイルがなければ抜ける.
        iVos = iDao.getXVLFileName(aArg)
        If iVos.Count = 0 Then Return iVos

        'キャドデータイベント区分”ND”で検索
        aArg.CadDataEventKbn = "S3"
        iVos = iDao.getKbnXVLFileName(aArg)
        If iVos.Count <> 0 Then Return iVos

        'キャドデータイベント区分”ND”で検索
        aArg.CadDataEventKbn = "S2"
        iVos = iDao.getKbnXVLFileName(aArg)
        If iVos.Count <> 0 Then Return iVos

        'キャドデータイベント区分”ND”で検索
        aArg.CadDataEventKbn = "S1"
        iVos = iDao.getKbnXVLFileName(aArg)
        If iVos.Count <> 0 Then Return iVos


        'ここまで来てた場合はファイルが存在しないこととする.
        Return New List(Of RHAC2270Vo)

    End Function

    ''' <summary>
    ''' DBからパーツ一覧を取得する.
    ''' </summary>
    ''' <param name="aArg">
    ''' <para>開発符号、ボディー名は必ず入力してください.</para>
    ''' </param>
    ''' <returns>部品リスト</returns>
    ''' <remarks></remarks>
    Private Function getBodyMstCompotiParts(ByVal aArg As BodyMSTMainteVO) As List(Of BodyMSTMainteVO)
        '部品番号を取得
        Dim iDao As New BodyMSTMainteImpl

        Dim iVo As List(Of BodyMSTMainteVO) = iDao.SelectPartsList(aArg)

        Return iVo

    End Function

#End Region

#Region "部品表ウィンドウを表示する."
    ''' <summary>
    ''' 部品表ウィンドウを表示する.
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub ShowFormParts()

        _FrmParts.MdiParent = _MdiForm
        _FrmParts.Show()

    End Sub

#End Region

#Region "ボディー選択後にアクティブな3Dフォームを再描画する."
    ''' <summary>
    ''' ボディー選択後にアクティブな3Dフォームを再描画する.
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub RedrawActiveForm(ByVal frm As Form)

        'F品番の取得.
        Dim iFhinban As String = frm.Tag

        Dim iFinalComposition As New List(Of String)
        Dim iFinalcomposition2 As New List(Of String)

        'F品番構成を取得.
        iFinalComposition = getFinalComposition(iFhinban)

        'F品番構成のXVLファイルが存在するかチェック.
        FinalCompositionExistXVL(iFinalComposition, iFinalcomposition2)

        'ボディーが選択されている場合、ボディーの3Dファイルを追加
        If Not String.IsNullOrEmpty(mBodyName) Then
            iFinalcomposition2.Add(ShisakuCommon.XVLFileBodyDir & mBodyName)
        End If

        Try
            '存在している部品でウィンドウを作成する.
            ' XVLウィンドウインスタンスを作成.
            Dim iXVlWindow As New FrmVeiwerImge()

            '初期化.
            iXVlWindow.Initialize(iFinalcomposition2, LoginInfo.Now.UserId, mEventCode, iFhinban)
            '出力ウィンドウ指定.
            iXVlWindow.MdiParent = _MdiForm

            'ウィンドウタイトルを指定.
            iXVlWindow.Text = String.Format("[{0}] ", iFhinban)

            'フォームのTagにファイナル品番を保持
            iXVlWindow.Tag = iFhinban

            '部品番号をラベルに表示
            iXVlWindow.lblHinban.Text = iFhinban

            'フォーム表示状態プロパティを設定
            iXVlWindow.Left = mXVLWindowList(iFhinban).Left
            iXVlWindow.Top = mXVLWindowList(iFhinban).Top
            iXVlWindow.Height = mXVLWindowList(iFhinban).Height
            iXVlWindow.Width = mXVLWindowList(iFhinban).Width
            iXVlWindow.WindowState = mXVLWindowList(iFhinban).WindowState

            'ウィンドウ表示位置をマニュアルに変更.
            iXVlWindow.StartPosition = FormStartPosition.Manual

            Application.DoEvents()

            iXVlWindow.Show()

            Application.DoEvents()


            '変更前のデータを削除
            mXVLWindowList(iFhinban).Dispose()


            '(部品番号,XLVウィンドウハンドル）
            mXVLWindowList(iFhinban) = iXVlWindow


#If DEBUG Then
        Catch ex As Exception
            Throw New Exception(String.Format("[{0}]ウィンドウ作成中にエラーが発生しました。:{1}", iFhinban, ex.Message))
#End If
        Finally
            '_MdiForm.pnlRedraw.Visible = False
        End Try
    End Sub
#End Region

#Region "XVLファイルを開いたウィンドウを表示する."

    ''' <summary>
    ''' XVLウィンドウを表示する.
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub ShowForm()

        If mXVLWindowList IsNot Nothing Then
            '使用済み画面の破棄.
            For Each lWindow In mXVLWindowList
                lWindow.Value.Dispose()
            Next
        End If

        '表示中構成保持メンバを初期化.
        mXVLWindowList = New Dictionary(Of String, FrmVeiwerImge)

        'ファイナル品番格納位置.
        Dim iRow As Integer = 2

        With _FrmParts.spdParts_Sheet1
            For nCol As Integer = FHINBANSTARTPOS To column

                '号車が有効かチェック（visible=falseの場合は前画面で非選択）
                If False = .Columns(nCol).Visible Then Continue For

                'F品番の取得.
                Dim iFhinban As String = .Cells(iRow, nCol).Value
                If StringUtil.IsEmpty(iFhinban) Then Continue For

                '構成退避用.
                Dim iFinalComposition As New List(Of String)
                Dim iFinalcomposition2 As New List(Of String)

                'F品番構成を取得.
                iFinalComposition = getFinalComposition(iFhinban)

                'F品番構成のXVLファイルが存在するかチェック.
                FinalCompositionExistXVL(iFinalComposition, iFinalcomposition2)

                'ボディー部品を追加
                'moreBodyParts(iFinalComposition)

                'ボディーが選択されている場合、ボディーの3Dファイルを追加
                If Not String.IsNullOrEmpty(mBodyName) Then
                    iFinalcomposition2.Add(ShisakuCommon.XVLFileBodyDir & mBodyName)
                End If

                Try
                    '存在している部品でウィンドウを作成する.
                    ' XVLウィンドウインスタンスを作成.
                    Dim iXVlWindow As New FrmVeiwerImge()

                    '初期化.
                    iXVlWindow.Initialize(iFinalcomposition2, LoginInfo.Now.UserId, mEventCode, iFhinban)

                    iXVlWindow.MdiParent = _MdiForm

                    'ウィンドウタイトルを指定.
                    iXVlWindow.Text = String.Format("[{0}] ", iFhinban)

                    'フォームのTagにファイナル品番を保持
                    iXVlWindow.Tag = iFhinban

                    '部品番号をラベルに表示
                    iXVlWindow.lblHinban.Text = iFhinban

                    iXVlWindow.Show()

                    '_MdiForm.spltViewer.Panel1.Controls.Add(iXVlWindow)

                    '(部品番号,XLVウィンドウハンドル）
                    mXVLWindowList.Add(iFhinban, iXVlWindow)

#If DEBUG Then
                Catch ex As Exception
                    Throw New Exception(String.Format("[{0}]ウィンドウ作成中にエラーが発生しました。:{1}", iFhinban, ex.Message))
#End If
                Finally
                End Try

            Next

        End With
    End Sub

#End Region

    ''' <summary>
    ''' 指定F品番ウィンドウを最前面に.
    ''' </summary>
    ''' <param name="aCol"></param>
    ''' <remarks></remarks>
    Public Sub XLVActive(ByVal aCol As Integer)

        If Not isFhinban(aCol) Then Exit Sub

        'タグを取得
        Dim iTag As String = _FrmParts.spdParts_Sheet1.Columns(aCol).Tag

        If False = mXVLWindowList.ContainsKey(iTag) Then Exit Sub


        If mXVLWindowList(iTag).IsDisposed = False Then
            ''ウィンドウをアクティベート.
            mXVLWindowList(iTag).BringToFront()
        End If
    End Sub

    ''' <summary>
    ''' ファイナル品番の構成を取得.
    ''' </summary>
    ''' <param name="aFinalHinban"></param>
    ''' <returns>ファイナル品番の部品構成、存在しないファイナルが指定された場合はNothingを変えす。</returns>
    ''' <remarks></remarks>
    Private Function getFinalComposition(ByVal aFinalHinban As String) As List(Of String)
        getFinalComposition = Nothing

        Dim iRetList As New List(Of String)

        With _FrmParts.spdParts_Sheet1
            Dim iCol As Integer? = Nothing
            Try
                'ファイナル品番の列位置を取得.
                iCol = .Columns(aFinalHinban).Index

            Catch ex As Exception
                Exit Try
            End Try

            '列位置情報が存在しない場合は処理中断.
            If False = iCol.HasValue Then Exit Function

            For lRow As Integer = 4 To .RowCount - 1
                '員数取得.
                Dim iInzuStr As String = .Cells(lRow, iCol).Value
                Dim iInzuInt As Integer = 0
                '員数の数値チェック.FALSEなら文字列.
                If False = Integer.TryParse(iInzuStr, iInzuInt) Then Continue For

                '部品番号の取得.
                Dim iPartsNo As String = .Cells(lRow, 8).Value

                '同一部品番号が存在した場合は追加しない.
                If iRetList.Contains(iPartsNo) Then Continue For
                Try
                    '構成部品一覧に追加.
                    iRetList.Add(iPartsNo)
#If DEBUG Then
                Catch ex As Exception
                    MessageBox.Show("削除された？")
#End If
                Finally
                End Try

            Next
        End With

        getFinalComposition = iRetList

    End Function

    ''' <summary>
    ''' XVLファイル存在チェック.
    ''' </summary>
    ''' <param name="aComposition"></param>
    ''' <remarks></remarks>
    Private Sub FinalCompositionExistXVL(ByRef aComposition As List(Of String), ByRef aComposition2 As List(Of String))
        Dim iChk As New List(Of String)
        Dim iChk2 As New List(Of String)

        For Each iPartsNo In aComposition
            'XLVファイルが存在するかチェック.
            If Not mXLVFileList.ContainsKey(iPartsNo) Then Continue For
            '表示部品番号がリストに追加済みかチェック.
            If iChk.Contains(iPartsNo) Then Continue For
            'チェックボックスがONかチェック.
            If getViewCheck(iPartsNo) = False Then Continue For

            '表示リストに追加.
            iChk.Add(mXLVFileList(iPartsNo))
            iChk2.Add(mXLVFilePathList(mXLVFileList(iPartsNo)))
        Next

        aComposition = iChk
        aComposition2 = iChk2
    End Sub

    ''' <summary>
    ''' F品番列チェック.
    ''' </summary>
    ''' <param name="aColumn"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function isFhinban(ByVal aColumn As Integer) As Boolean
        Return FHINBANSTARTPOS <= aColumn AndAlso aColumn <= column
        'Return FHINBANSTARTPOS <= aColumn AndAlso aColumn <= FHINBANENDPOS
    End Function

    ''' <summary>
    ''' 部品番号から行位置を取得.
    ''' </summary>
    ''' <param name="aPartsNo"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function getRowIndex(ByVal aPartsNo As String) As Integer
        Dim iRowIndex As New List(Of Integer)

        For iRow As Integer = 0 To _FrmParts.spdParts_Sheet1.RowCount - 1
            Dim iStr As String = _FrmParts.spdParts_Sheet1.Cells(iRow, 8).Value

            If iStr Is Nothing Then Continue For

            If iStr <> aPartsNo Then Continue For
            '行位置を設定.
            iRowIndex.Add(iRow)
            Exit For
        Next
        If 1 < iRowIndex.Count Then Throw New Exception("同一部品番号が複数あります.")

        Return iRowIndex(0)

    End Function

    ''' <summary>
    ''' 表示可否チェックボックスの状態を取得.
    ''' </summary>
    ''' <param name="aPartsNo"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function getViewCheck(ByVal aPartsNo As String) As Boolean
        Dim iRowIndex As Integer = getRowIndex(aPartsNo)
        Return _FrmParts.spdParts_Sheet1.Cells(iRowIndex, 0).Value
    End Function

    ''' <summary>
    ''' チェックボックス変更時にXVLファイルウィンドウに表示している構成を更新する.
    ''' </summary>
    ''' <param name="aPartsNo"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function WindowReDraw(ByVal aPartsNo As String) As Boolean
        '指定された部品番号を含むF品番のウィンドウを更新する.
        'Dim iRowIndex As Integer = getRowIndex(aPartsNo)
        Dim iRowIndex As Integer = aPartsNo
        For lColIndex As Integer = 0 To _FrmParts.spdParts_Sheet1.ColumnCount - 1

            '号車が有効かチェック（visible=falseの場合は前画面で非選択）
            If False = _FrmParts.spdParts_Sheet1.Columns(lColIndex).Visible Then Continue For

            'F品番列チェック.
            If False = isFhinban(lColIndex) Then Continue For

            Dim iInzuStr As String = _FrmParts.spdParts_Sheet1.Cells(iRowIndex, lColIndex).Value
            Dim iInzuInt As Integer = 0
            '数値チェック.
            If False = Integer.TryParse(iInzuStr, iInzuInt) Then Continue For

            'F品番の取得.
            Dim iFhinban As String = _FrmParts.spdParts_Sheet1.Columns(lColIndex).Tag

            Dim iFinalComposition As New List(Of String)
            Dim iFinalcomposition2 As New List(Of String)

            'F品番構成を取得.
            iFinalComposition = getFinalComposition(iFhinban)

            'F品番構成のXVLファイルが存在するかチェック.
            FinalCompositionExistXVL(iFinalComposition, iFinalcomposition2)

            'ボディー部品を追加
            'moreBodyParts(iFinalComposition)

            'ボディーが選択されている場合、ボディーの3Dファイルを追加
            If Not String.IsNullOrEmpty(mBodyName) Then
                iFinalcomposition2.Add(ShisakuCommon.XVLFileBodyDir & mBodyName)
            End If

            Try
                '存在している部品でウィンドウを作成する.
                ' XVLウィンドウインスタンスを作成.
                Dim iXVlWindow As New FrmVeiwerImge()

                '初期化.
                iXVlWindow.Initialize(iFinalcomposition2, LoginInfo.Now.UserId, mEventCode, iFhinban)
                '出力ウィンドウ指定.
                iXVlWindow.MdiParent = _MdiForm

                'ウィンドウタイトルを指定.
                iXVlWindow.Text = String.Format("[{0}] ", iFhinban)

                'フォームのTagにファイナル品番を保持
                iXVlWindow.Tag = iFhinban

                '部品番号をラベルに表示
                iXVlWindow.lblHinban.Text = iFhinban

                iXVlWindow.Left = mXVLWindowList(iFhinban).Left
                iXVlWindow.Top = mXVLWindowList(iFhinban).Top

                iXVlWindow.WindowState = mXVLWindowList(iFhinban).WindowState

                'ウィンドウ表示位置をマニュアルに変更.
                iXVlWindow.StartPosition = FormStartPosition.Manual

                '_MdiForm.pnlRedraw.Height = _MdiForm.spltViewer.Panel1.Height
                '_MdiForm.pnlRedraw.Visible = True

                Application.DoEvents()

                iXVlWindow.SuspendLayout()
                iXVlWindow.Show()
                'iXVlWindow.Activate()

                '_MdiForm.spltViewer.Panel1.Controls.Add(iXVlWindow)
                iXVlWindow.ResumeLayout()

                Application.DoEvents()


                '変更前のデータを削除
                mXVLWindowList(iFhinban).Dispose()


                '(部品番号,XLVウィンドウハンドル）
                mXVLWindowList(iFhinban) = iXVlWindow


#If DEBUG Then
            Catch ex As Exception
                Throw New Exception(String.Format("[{0}]ウィンドウ作成中にエラーが発生しました。:{1}", iFhinban, ex.Message))
#End If
            Finally
                '_MdiForm.pnlRedraw.Visible = False
            End Try


        Next

    End Function

#Region "表示するXVLウィンドウリストの変更"

    ''' <summary>
    ''' ボディー未追加Windowリストの表示.
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub showWindowList()

        For Each lWindow In mXVLWindowList
            lWindow.Value.Dispose()
        Next

        Application.DoEvents()

        ShowForm()


    End Sub

#End Region

#Region "ボディー選択時の部品追加処理"


    ''' <summary>
    ''' ボディー選択時にボディー構成パーツをベースのパーツリストに追加する.
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub moreBodyParts(ByRef aBodyComp As List(Of String))


        Dim iArg As New BodyMSTMainteVO
        iArg.KaihatsuFugo = mKaihatsuFugo
        iArg.BodyName = mBodyName

        'ボディー構成パーツリストを取得.
        Dim iMoreBodyPartslist As List(Of BodyMSTMainteVO) = getBodyMstCompotiParts(iArg)
        If iMoreBodyPartslist.Count = 0 Then Exit Sub


        '部品番号のみのリストを作成.
        For Each lParts In iMoreBodyPartslist
            Dim iVo As New List(Of RHAC2270Vo)
            Try
                'ファイル名称取得用のパラメータを作成.
                Dim iArg2270 As New RHAC2270Vo()
                iArg2270.KaihatsuFugo = lParts.KaihatsuFugo
                iArg2270.BuhinNo = lParts.BuhinNo
                iArg2270.BlockNo = mBlockNo
                iVo = get3DFileName(iArg2270)
#If DEBUG Then

            Catch ex As Exception
                MessageBox.Show(ex.Message)
#End If
            Finally
            End Try

            If 1 < iVo.Count Then Throw New Exception("ボディーの３Ｄファイル名を検索中に複数のレコードが抽出されました.条件を見直してください.")

            'レコードが存在しない場合は次の部品へ.
            If iVo.Count = 0 Then Continue For

            '20140523
            'RHAC2270ファイルパスがフルパスとなった為、パス設定の変更を行なう
            Dim iFullPath As String = iVo(0).XvlFileName

            If False = mFiles.Contains(iFullPath) Then Continue For

            'XVLファイル名を取得.
            aBodyComp.Add(iVo(0).XvlFileName)
        Next

    End Sub


    ''' <summary>
    ''' XVLウィンドウのEnableを変更する.
    ''' </summary>
    ''' <param name="aWindowList"></param>
    ''' <param name="aEnable"></param>
    ''' <remarks></remarks>
    Private Sub ChangeWindwoListEnabled(ByVal aWindowList As Dictionary(Of String, FrmVeiwerImge), ByVal aEnable As Boolean)
        For Each lWindow In aWindowList
            lWindow.Value.Visible = aEnable
        Next
    End Sub


#End Region

    Public Sub DeleteXVL()
        If mXVLWindowList IsNot Nothing Then
            '使用済み画面の破棄.
            For Each lWindow In mXVLWindowList
                lWindow.Value.DeleteXVL()
            Next
        End If
    End Sub

End Class
