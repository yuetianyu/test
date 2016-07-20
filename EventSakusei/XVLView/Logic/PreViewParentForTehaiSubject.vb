
Imports EventSakusei.XVLView
Imports EventSakusei.XVLView.Dao.Vo
Imports EventSakusei.XVLView.Dao.Impl
Imports EventSakusei.TehaichoSakusei.Logic
Imports EventSakusei.TehaichoSakusei.Dao
Imports EventSakusei.ShisakuBuhinEdit.Kosei

Imports ShisakuCommon
Imports ShisakuCommon.Db
Imports ShisakuCommon.Db.EBom
Imports ShisakuCommon.Db.EBom.Vo
Imports ShisakuCommon.Db.EBom.Dao
Imports ShisakuCommon.Db.EBom.Dao.Impl

Imports FarPoint.Win.Spread




Public Class PreViewParentSubjectForTehai


#Region "Const"
    'コンストラクタパラメータエラー.
    Private Const CONST_THROW_MESSAGE = "パラメータが正しくありません"

    Private Const CONST_3D_FILE_UMU = "3DFILE_UMU"
    Private Const CONST_3D_VIEW = "3D_VIEW"
    Private Const COSNT_BLOCK = "ShisakuBlockNo"
    Private Const COSNT_LEVEL = "level"
    Private Const COSNT_BUHIN_NO = "ShisakuBuhinNo"
    Private Const CONST_KBN = "Kbn"
    Private Const CONST_BUHIN_NAME = "ShisakuBuhinName"
    Private Const CONST_MAKER_NAME = "MakerName"
    Private Const COSNT_SYUKEI_CODE = "SyukeiCode"
    Private Const COSNT_MAKER_CODE = "MakerCode"

    'スプレッド基本情報タグの設定値(号車のタグには号車名を設定する).
    Public CONST_SPREAD_COLUMNS As New List(Of String)(New String() {CONST_3D_FILE_UMU, _
                                                                     CONST_3D_VIEW, _
                                                                     COSNT_BLOCK, _
                                                                     COSNT_LEVEL, _
                                                                     COSNT_BUHIN_NO, _
                                                                     CONST_KBN, _
                                                                     CONST_BUHIN_NAME, _
                                                                     CONST_MAKER_NAME, _
                                                                     COSNT_SYUKEI_CODE, _
                                                                     COSNT_MAKER_CODE})
    'データ開始位置.
    Private Shadows Const DETAIL_ROW As Integer = 4
    '号車の開始位置.
    Private CONST_INZU_START_POS = CONST_SPREAD_COLUMNS.Count

    Private Const FOLDER_PATH As String = "T:\新試作手配システム\XVLTMP\"

#End Region

#Region "メンバ変数"

    Private mMdiParentForm As frmPreViewParentForTehai
    Private _FrmParts As frmPartsForTehai
    Private spdParts As FpSpread                'スプレッド
    Private spdParts_Sheet1 As SheetView        'スプレッド（アクティブシート）
    Private mGUID As Guid                       'DB更新時に使用したGUID
    Private mEventCode As String                'イベントコード.
    Private mListCode As String                 'リストコード.
    Private mKaihatsuFugo As String             '開発符号.
    Private mGroup As String                    'グループ名
    '選択号車情報（表示位置,号車名）
    Private mGousya As Dictionary(Of Integer, String)

    '基本情報.
    Private mBaseData As List(Of TShisakuBuhinEditTmp3dVo)
    '号車情報.
    Private mGousyaData As List(Of TShisakuBuhinEditGousyaTmp3dVo)
    'ボディー名
    Private mBodyName As String

    '表示するファイルリスト(ファイル名,表示可否).
    Public mXLVFileList As Dictionary(Of String, String)

    '表示するファイルリスト(ファイル名,フルパス).
    Public mXLVFilePathList As Dictionary(Of String, String)

    'XLVウィンドハンドルを保持.(F品番、ウィンドハンドル）
    Private mXVLWindowList As Dictionary(Of String, FrmVeiwerImge)
    Private mXVLWindowMoreBodyList As Dictionary(Of String, FrmVeiwerImge)
    '指定フォルダ内3Dファイル一覧
    Dim mFiles As List(Of String)

    Dim timetest As New Timer

#End Region

#Region "プロパティ"
    ''' <summary>GUID</summary>
    Public Property GUID() As Guid
        Get
            Return mGUID
        End Get
        Set(ByVal value As Guid)
            mGUID = value
        End Set
    End Property

    ''' <summary>イベントコード</summary>
    Public Property EventCode() As String
        Get
            Return mEventCode
        End Get
        Set(ByVal value As String)
            mEventCode = value
        End Set
    End Property

    ''' <summary>リストコード</summary>
    Public Property ListCode() As String
        Get
            Return mListCode
        End Get
        Set(ByVal value As String)
            mListCode = value
        End Set
    End Property

    ''' <summary>グループ</summary>
    Public Property Group() As String
        Get
            Return mGroup
        End Get
        Set(ByVal value As String)
            mGroup = value
        End Set
    End Property

    ''' <summary>号車</summary>
    Public Property Gousya() As Dictionary(Of Integer, String)
        Get
            Return mGousya
        End Get
        Set(ByVal value As Dictionary(Of Integer, String))
            mGousya = value
        End Set
    End Property

    ''' <summary>ボディー名</summary>
    Public Property BodyName() As String
        Get
            Return mBodyName
        End Get
        Set(ByVal value As String)
            mBodyName = value
        End Set
    End Property
#End Region

    ''' <summary>
    ''' コンストラクタ
    ''' </summary>
    ''' <param name="aGUID">GUID</param>
    ''' <param name="aEventCode">イベントコード</param>
    ''' <param name="aGroup">グループ名</param>
    ''' <param name="aGousya">選択号車情報</param>
    ''' <remarks></remarks>
    Public Sub New(ByVal aMdiForm As frmPreViewParentForTehai, ByVal aGUID As Guid, ByVal aEventCode As String, ByVal aGroup As String, ByVal aGousya As Dictionary(Of Integer, String))

        If aGUID = GUID.Empty Then Throw New Exception(CONST_THROW_MESSAGE)
        If aEventCode Is Nothing Then Throw New Exception(CONST_THROW_MESSAGE)
        If aGroup Is Nothing Then Throw New Exception(CONST_THROW_MESSAGE)
        If aGousya Is Nothing Then Throw New Exception(CONST_THROW_MESSAGE)

        _FrmParts = New frmPartsForTehai(Me)

        spdParts = _FrmParts.spdParts
        spdParts_Sheet1 = _FrmParts.spdParts.ActiveSheet

        mMdiParentForm = aMdiForm
        mGUID = aGUID
        mEventCode = aEventCode
        mKaihatsuFugo = getKaihatsuFugo(aEventCode)
        mGroup = aGroup
        mGousya = aGousya

        mBodyName = ""

    End Sub
#Region "スプレッド初期化"

    ''' <summary>
    ''' スプレッド初期化.
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub InitSpd()

        'スプレッドに基本情報タグを設定.
        InitSpd_SetBaseTag()

        'スプレッドに号車タグを設定.
        InitSpd_SetGousyaTag()

        '基本情報を設定.
        set3DShisakuBuhinEdit()

        '号車号車を設定.
        set3DShisakuTehaiGousya()

    End Sub

    ''' <summary>
    ''' スプレッドに基本情報のタグを設定する.
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub InitSpd_SetBaseTag()
        Dim iIndex As Integer = 0

        For Each lTag In CONST_SPREAD_COLUMNS
            'タグを設定.
            spdParts_Sheet1.Columns(EzUtil.Increment(iIndex)).Tag = lTag
        Next

    End Sub

    ''' <summary>
    ''' 号車タグを設定.
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub InitSpd_SetGousyaTag()
        Dim iDao As New XVLView.Dao.Impl.ConditionSelectDaoImpl
        Dim iVOs As List(Of XVLView.Dao.Vo.ConditionSelectVo) = iDao.GetGousya(EventCode, Group)

        Dim iIndex As Integer = CONST_INZU_START_POS      '全号車に対するカラム位置
        Dim iViewGousyaNo As Integer = 1                             'スプレッド表示時の号車位置採番用

        '2014/04/08 kabasawa'
        '号車開始位置+そのイベントの最大号車表示順'
        'spdParts_Sheet1.ColumnCount = 200


        Dim maxGousyaHyoujiJun As Integer = -1

        For Each vo As ConditionSelectVo In iVOs
            If maxGousyaHyoujiJun < Integer.Parse(vo.HyojijunNo) Then
                maxGousyaHyoujiJun = vo.HyojijunNo
            End If
        Next

        spdParts_Sheet1.ColumnCount = CONST_INZU_START_POS + maxGousyaHyoujiJun + 1

        'ベースとなる号車を設定するセルをバックアップ.
        Dim iCell As FarPoint.Win.Spread.Cell = spdParts_Sheet1.Cells(2, CONST_INZU_START_POS)

        '号車セルの整形'
        For columnIndex As Integer = CONST_INZU_START_POS To spdParts_Sheet1.ColumnCount - 1
            '列数の変更.
            'spdParts_Sheet1.Columns.Count += 1



            'spdParts_Sheet1.Columns.Add(spdParts_Sheet1.ColumnCount - 1, 1)
            '1行目
            spdParts_Sheet1.Cells(0, columnIndex).Text = "号車"
            'コルスパンの設定.
            'spdParts_Sheet1.Cells(0, CONST_SPREAD_COLUMNS.Count + 1).ColumnSpan = columnIndex - CONST_SPREAD_COLUMNS.Count

            '2行目
            '2014/04/15 kabasawa'
            'チェックボックスに改造'
            Dim cCelltype As New CellType.CheckBoxCellType
            spdParts_Sheet1.Cells(1, columnIndex).CellType = cCelltype
            spdParts_Sheet1.Cells(1, columnIndex).Locked = False
            spdParts_Sheet1.Cells(1, columnIndex).BackColor = Color.White

            '3行目
            Dim iCellType As New CellType.TextCellType()
            iCellType.TextOrientation = FarPoint.Win.TextOrientation.TextVertical
            iCellType.ReadOnly = True
            spdParts_Sheet1.Cells(2, columnIndex).CellType = iCellType
            spdParts_Sheet1.Cells(2, columnIndex).BackColor = Color.White

            '4行目
            spdParts_Sheet1.Cells(3, columnIndex).BackColor = Color.White

            spdParts_Sheet1.Columns(columnIndex).Locked = True
            spdParts_Sheet1.Columns(columnIndex).Width = 20

            'spdParts_Sheet1.Cells(1, columnIndex).Text = iViewGousyaNo.ToString

            'テキストに値（号車）を設定.
            spdParts_Sheet1.Cells(2, columnIndex).Font = iCell.Font

        Next
        '合体させる'
        spdParts_Sheet1.Cells(0, CONST_INZU_START_POS).ColumnSpan = spdParts_Sheet1.ColumnCount


        Dim border As New FarPoint.Win.ComplexBorderSide(FarPoint.Win.ComplexBorderSideStyle.ThinLine)
        Dim cBorder As New FarPoint.Win.ComplexBorder(Nothing, Nothing, Nothing, border)
        '線を引く'
        spdParts_Sheet1.Rows(3).Border = cBorder



        For Each lTag In iVOs
            spdParts_Sheet1.Cells(2, CONST_INZU_START_POS + Integer.Parse(lTag.HyojijunNo)).Text = lTag.ShisakuGousya

            '全画面で選択されていない場合は非表示とする
            If False = Gousya.ContainsValue(lTag.ShisakuGousya) Then
                '列非表示.
                'spdParts_Sheet1.Columns(iIndex).Visible = False
            Else
                '号車採番をインクリメント.
                iViewGousyaNo += 1
            End If

            'タグを設定.
            spdParts_Sheet1.Columns(CONST_INZU_START_POS + Integer.Parse(lTag.HyojijunNo)).Tag = lTag.ShisakuGousya
            'spdParts_Sheet1.Columns(EzUtil.Increment(iIndex)).Tag = lTag.ShisakuGousya

        Next

        '2014/04/08 kabasawa'
        '余計な列を非表示にする'
        For columnIndex As Integer = CONST_INZU_START_POS To spdParts_Sheet1.ColumnCount - 1
            If Not Gousya.ContainsValue(spdParts_Sheet1.Cells(2, columnIndex).Text) Then
                spdParts_Sheet1.Columns(columnIndex).Visible = False
            End If
        Next

    End Sub

#End Region

#Region "スプレッドデータセット"

    ''' <summary>
    ''' 手配帳基本情報の取得.
    ''' </summary>
    ''' <remarks></remarks>
    Private Function getShisakuTehaiGousya(ByVal aEventCode As String, ByVal aListCode As String) As List(Of TehaichoBuhinEditTmpVo)
        'リストコードを利用していないのはなぜ？'
        '場合によっては重複データが大量発生する'

        Dim sb As New System.Text.StringBuilder
        Dim db As New EBomDbClient

        Dim lstGousha As New ArrayList
        For Each g As String In Gousya.Values
            lstGousha.Add(String.Format("'{0}'", g))
        Next


        'ブロック別最新改訂の取得.
        With sb
            .Remove(0, .Length)
            .AppendLine(" SELECT ")
            '.AppendLine(" SELECT TOP 10")
            .AppendLine("     [SHISAKU_BLOCK_NO]")
            .AppendLine("     ,[SHISAKU_BLOCK_NO_KAITEI_NO]")
            .AppendLine(" FROM " & MBOM_DB_NAME & ".[dbo].[T_SHISAKU_BUHIN_EDIT] BE")
            .AppendLine(" WHERE")
            .AppendFormat("     [SHISAKU_EVENT_CODE] = '{0}'", aEventCode)
            .AppendLine("     AND [SHISAKU_BLOCK_NO_KAITEI_NO] = (")
            .AppendLine("             SELECT ")
            .AppendLine("                 MAX([SHISAKU_BLOCK_NO_KAITEI_NO])")
            .AppendLine("             FROM " & MBOM_DB_NAME & ".[dbo].[T_SHISAKU_BUHIN_EDIT]")
            .AppendLine("             WHERE")
            .AppendLine("                 [SHISAKU_EVENT_CODE] = BE.[SHISAKU_EVENT_CODE] ")
            .AppendLine("                 AND [SHISAKU_BLOCK_NO] = BE.[SHISAKU_BLOCK_NO] ")
            .AppendLine("             )")
            .AppendLine(" GROUP BY")
            .AppendLine("     [SHISAKU_EVENT_CODE]")
            .AppendLine("     ,[SHISAKU_BLOCK_NO]")
            .AppendLine("     ,[SHISAKU_BLOCK_NO_KAITEI_NO]")
            .AppendLine(" ORDER BY")
            .AppendLine("     [SHISAKU_BLOCK_NO]")

        End With

        Dim iBlocks As List(Of TShisakuBuhinEditVo) = db.QueryForList(Of TShisakuBuhinEditVo)(sb.ToString)
        If iBlocks.Count = 0 Then
            Return New List(Of TehaichoBuhinEditTmpVo)
        End If


        Dim sbBlocks As New System.Text.StringBuilder
        '検索対象のブロックリストを作成.
        For Each lBlockData In iBlocks
            If sbBlocks.Length = 0 Then
                '先頭行の設定.
                sbBlocks.AppendFormat(" AND (EDI.SHISAKU_BLOCK_NO='{0}'", lBlockData.ShisakuBlockNo)
                sbBlocks.AppendFormat(" AND [SHISAKU_BLOCK_NO_KAITEI_NO]='{0}'", lBlockData.ShisakuBlockNoKaiteiNo)
            Else
                sbBlocks.AppendFormat(" OR EDI.SHISAKU_BLOCK_NO='{0}' ", lBlockData.ShisakuBlockNo)
                sbBlocks.AppendFormat(" AND [SHISAKU_BLOCK_NO_KAITEI_NO]='{0}'", lBlockData.ShisakuBlockNoKaiteiNo)
            End If
        Next
        sbBlocks.Append(")")

        With sb
            sb.Remove(0, sb.Length)

            .AppendLine(" SELECT ")
            .AppendLine("     EDI.[SHISAKU_EVENT_CODE]")
            .AppendLine("     ,GOU.[SHISAKU_LIST_CODE]")
            .AppendLine("     ,EDI.[LEVEL]")
            .AppendLine("     ,EDI.[GENCYO_CKD_KBN]")
            .AppendLine("     ,EDI.[SHISAKU_BUKA_CODE]")
            .AppendLine("     ,EDI.[SHISAKU_BLOCK_NO]")
            .AppendLine("     ,EDI.[SHISAKU_BLOCK_NO_KAITEI_NO]")
            .AppendLine("     ,EDI.[BUHIN_NO_HYOUJI_JUN]")
            .AppendLine("     ,EDI.BUHIN_NO")
            .AppendLine("     ,EDI.BUHIN_NAME")
            .AppendLine("     ,EDI.MAKER_CODE")
            .AppendLine("     ,EDI.MAKER_NAME")
            .AppendLine("     ,EDI.SHUKEI_CODE")
            .AppendLine("     ,EDI.SIA_SHUKEI_CODE")
            .AppendLine("     ,GOU.SHISAKU_GOUSYA_HYOUJI_JUN")
            .AppendLine("     ,GOU.SHISAKU_GOUSYA")
            .AppendLine("     ,GOU.INSU_SURYO")
            .AppendLine(" FROM ")
            .AppendLine("     " & MBOM_DB_NAME & ".[dbo].[T_SHISAKU_BUHIN_EDIT] EDI")
            .AppendLine("     INNER JOIN")
            .AppendLine("     " & MBOM_DB_NAME & ".[dbo].[T_SHISAKU_TEHAI_GOUSYA] GOU")
            .AppendLine("     ON EDI.SHISAKU_EVENT_CODE = GOU.SHISAKU_EVENT_CODE")
            .AppendLine("     AND EDI.[SHISAKU_BUKA_CODE] = GOU.[SHISAKU_BUKA_CODE]")
            .AppendLine("     AND EDI.[SHISAKU_BLOCK_NO] = GOU.[SHISAKU_BLOCK_NO]")
            .AppendLine("     AND EDI.[BUHIN_NO_HYOUJI_JUN] = GOU.[BUHIN_NO_HYOUJI_JUN]")
            '2014/04/10 kabasawa'
            'ダミーは取得しない。'
            '員数0も取得しない?'
            '.AppendLine(" 	  AND NOT GOU.SHISAKU_GOUSYA = 'DUMMY' ")
            '.AppendLine("     AND NOT GOU.INSU_SURYO = 0 ")
            .AppendFormat("     AND GOU.SHISAKU_GOUSYA IN ({0})", String.Join(",", lstGousha.ToArray))
            .AppendLine(" WHERE")
            .AppendFormat("     EDI.[SHISAKU_EVENT_CODE] = '{0}'", aEventCode)
            ''
            '.AppendLine("     AND GOU.[SHISAKU_LIST_CODE] = '" & aListCode & "'")
            'ブロック一覧を追加.
            .AppendLine(sbBlocks.ToString)
            .AppendLine(" GROUP BY")
            .AppendLine("     EDI.[SHISAKU_EVENT_CODE]")
            .AppendLine("     ,GOU.[SHISAKU_LIST_CODE]")
            .AppendLine("     ,EDI.[LEVEL]")
            .AppendLine("     ,EDI.[GENCYO_CKD_KBN]")
            .AppendLine("     ,EDI.[SHISAKU_BUKA_CODE]")
            .AppendLine("     ,EDI.[SHISAKU_BLOCK_NO]")
            .AppendLine("     ,EDI.[SHISAKU_BLOCK_NO_KAITEI_NO]")
            .AppendLine("     ,EDI.[BUHIN_NO_HYOUJI_JUN]")
            .AppendLine("     ,EDI.BUHIN_NO")
            .AppendLine("     ,EDI.BUHIN_NAME")
            .AppendLine("     ,EDI.MAKER_CODE")
            .AppendLine("     ,EDI.MAKER_NAME")
            .AppendLine("     ,EDI.SHUKEI_CODE")
            .AppendLine("     ,EDI.SIA_SHUKEI_CODE")
            .AppendLine("     ,GOU.SHISAKU_GOUSYA_HYOUJI_JUN")
            .AppendLine("     ,GOU.SHISAKU_GOUSYA")
            .AppendLine("     ,GOU.INSU_SURYO")
            .AppendLine(" ORDER BY")
            .AppendLine("     EDI.[SHISAKU_EVENT_CODE]")
            .AppendLine("     ,EDI.[SHISAKU_BUKA_CODE]")
            .AppendLine("     ,EDI.[SHISAKU_BLOCK_NO]")

        End With

        Return db.QueryForList(Of TehaichoBuhinEditTmpVo)(sb.ToString)

    End Function

    ''' <summary>
    ''' 基本情報をスプレッドにセット.
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub set3DShisakuBuhinEdit()

        Dim iGousyaDao As TShisakuBuhinEditGousyaTmp3dDao = New TShisakuBuhinEditGousyaTmp3dDaoImpl()
        Dim iBaseDao As New ShisakuBuhinEditTmp3DDaoImpl


        'GUIDをベースに表示データを抽出する.
        mBaseData = iBaseDao.FindByShisakuBuhinEditTmp3D(GUID.ToString)

        If mBaseData.Count = 0 Then
            Exit Sub
        End If

        spdParts_Sheet1.RowCount = mBaseData.Count + DETAIL_ROW

        'Doeventタイマー設定.
        timetest.Interval = 100
        AddHandler timetest.Tick, AddressOf Doevent
        timetest.Enabled = True
        timetest.Start()

        Dim iRowIndex As Integer = DETAIL_ROW

        Try

            For Each lRowData In mBaseData
                Dim iColIndex As Integer = 2

                spdParts_Sheet1.Cells(iRowIndex, EzUtil.Increment(iColIndex)).Text = lRowData.ShisakuBlockNo
                spdParts_Sheet1.Cells(iRowIndex, EzUtil.Increment(iColIndex)).Text = lRowData.Level
                spdParts_Sheet1.Cells(iRowIndex, EzUtil.Increment(iColIndex)).Text = lRowData.BuhinNo
                spdParts_Sheet1.Cells(iRowIndex, EzUtil.Increment(iColIndex)).Text = lRowData.BuhinNoKbn
                spdParts_Sheet1.Cells(iRowIndex, EzUtil.Increment(iColIndex)).Text = lRowData.BuhinName
                spdParts_Sheet1.Cells(iRowIndex, EzUtil.Increment(iColIndex)).Text = lRowData.MakerName
                spdParts_Sheet1.Cells(iRowIndex, EzUtil.Increment(iColIndex)).Text = lRowData.ShukeiCode
                spdParts_Sheet1.Cells(iRowIndex, EzUtil.Increment(iColIndex)).Text = lRowData.MakerCode
                'Application.DoEvents()
                '次の行へ.
                iRowIndex += 1
            Next
        Catch ex As Exception
            Throw New Exception(ex.Message)
        Finally

            timetest.Enabled = False

        End Try

        timetest.Stop()

    End Sub

    Private Sub Doevent()
        Application.DoEvents()
    End Sub

    ''' <summary>
    ''' 号車情報号車をスプレッドにセット.
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub set3DShisakuTehaiGousya()
        Dim iRowIndex As Integer = DETAIL_ROW

        For Each lBaseData In mBaseData

            'GUID,イベントコード、部品番号をキーにしてテーブル検索.

            Dim iDao As New ShisakuBuhinEditGousyaTmp3DDaoImpl
            'Dim iDao As TShisakuBuhinEditGousyaTmp3dDao = New TShisakuBuhinEditGousyaTmp3dDaoImpl()

            Dim iArg As New TShisakuBuhinEditGousyaTmp3dVo
            iArg.Guid = lBaseData.Guid
            iArg.ShisakuEventCode = lBaseData.ShisakuEventCode
            iArg.ShisakuBukaCode = lBaseData.ShisakuBukaCode
            iArg.ShisakuBlockNo = lBaseData.ShisakuBlockNo
            iArg.ShisakuBlockNoKaiteiNo = lBaseData.ShisakuBlockNoKaiteiNo
            iArg.BuhinNoHyoujiJun = lBaseData.BuhinNoHyoujiJun
            iArg.GyouId = lBaseData.GyouId

            Dim iVos As List(Of TShisakuBuhinEditGousyaTmp3dVo) = iDao.FindByShisakuBuhinEditGousyaTmp3D(iArg)
            'Dim iVos As List(Of TShisakuBuhinEditGousyaTmp3dVo) = iDao.FindBy(iArg)
            Dim iColindex As Integer = 0

            If iVos.Count = 0 Then Debug.WriteLine("号車情報なし.")

            For Each lGousyaData In iVos

                'ShisakuGousyaHyoujiJunごとに号車を設定.
                'ShisakuGousyaHyoujiJunは０から始まる.
                iColindex = CONST_INZU_START_POS + lGousyaData.ShisakuGousyaHyoujiJun

                '号車が存在しない場合は次の号車へ.
                If Not lGousyaData.InsuSuryo.HasValue Then Continue For

                '号車を設定.
                spdParts_Sheet1.Cells(iRowIndex, iColindex).Text = lGousyaData.InsuSuryo

            Next

            '次の行へ.
            iRowIndex += 1

        Next

        '余計な行を非表示にする'
        For rowIndex As Integer = DETAIL_ROW To spdParts_Sheet1.RowCount - 1
            Dim rowVisible As Boolean = False
            For columnIndex As Integer = CONST_INZU_START_POS To spdParts_Sheet1.ColumnCount - 1
                If spdParts_Sheet1.Columns(columnIndex).Visible Then
                    If StringUtil.IsNotEmpty(spdParts_Sheet1.Cells(rowIndex, columnIndex).Text) Then
                        rowVisible = True
                        Exit For
                    End If
                End If
            Next
            If Not rowVisible Then
                spdParts_Sheet1.Rows(rowIndex).Visible = False
            End If
        Next


    End Sub


#End Region

#Region "XVLウィンドウを作成"

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

        '号車名格納行.
        Dim iRow As Integer = 2

        With spdParts_Sheet1
            For nCol As Integer = CONST_INZU_START_POS To spdParts_Sheet1.ColumnCount - 1

                '号車が有効かチェック（visible=falseの場合は前画面で非選択）
                If False = .Columns(nCol).Visible Then Continue For

                '号車チェックボックスにチェックが入っているか？'
                If .Cells(1, nCol).Value Is Nothing Then
                    Continue For
                Else
                    If Not CBool(.Cells(1, nCol).Value) Then
                        Continue For
                    End If
                End If

                '号車の取得.
                Dim iGousya As String = .Cells(iRow, nCol).Value

                If StringUtil.IsEmpty(iGousya) Then Continue For

                Dim iFinalComposition As List(Of String)
                Dim iFinalComposition2 As New List(Of String)
                Dim iFinalComposition3 As New List(Of String)


                'F品番構成を取得.
                iFinalComposition = getGousyaComposition(iGousya)

                'F品番構成のXVLファイルが存在するかチェック.
                FinalCompositionExistXVL(iFinalComposition, iFinalComposition2)

                'ボディー部品を追加
                'moreBodyParts(iFinalComposition)
                'ボディーが選択されている場合、ボディーの3Dファイルを追加
                If Not String.IsNullOrEmpty(mBodyName) Then
                    iFinalComposition3.Add(ShisakuCommon.XVLFileBodyDir & mBodyName)
                    'iFinalComposition2.Add(ShisakuCommon.XVLFileBodyDir & mBodyName)
                End If

                For Each Str As String In iFinalComposition2
                    iFinalComposition3.Add(Str)
                Next

                '表示する部品が存在しない場合は無視'
                If iFinalComposition3.Count = 0 Then
                    Continue For
                End If

                ''表示する部品が存在しない場合は処理中断.
                'If iFinalComposition.Count = 0 Then Exit For

                Try
                    'mMdiParentForm.LayoutMdi(MdiLayout.TileVertical)
                    'mMdiParentForm.LayoutMdi(MdiLayout.TileVertical)

                    '存在している部品でウィンドウを作成する.
                    ' XVLウィンドウインスタンスを作成.
                    Dim iXVlWindow As New FrmVeiwerImge()

                    '初期化.
                    iXVlWindow.Initialize(iFinalComposition3, LoginInfo.Now.UserId, mEventCode, iGousya)

                    'iXVlWindow.Initialize(iFinalComposition2, LoginInfo.Now.UserId, mEventCode, iGousya)
                    '親フォームの指定.
                    iXVlWindow.MdiParent = mMdiParentForm

                    'ウィンドウタイトルを指定
                    iXVlWindow.Text = String.Format("[{0}] ", iGousya)

                    'フォームのTagにファイナル品番を保持
                    iXVlWindow.Tag = iGousya

                    '部品番号をラベルに表示
                    iXVlWindow.lblHinban.Text = iGousya

                    iXVlWindow.Show()
                    'html直接表示だとwindows7で表示されない現象が発生した。'
                    'Process.Start(FOLDER_PATH & LoginInfo.Now.UserId & "_" & mEventCode & "_" & iGousya & ".html")
                    'mMdiParentForm.spltViewer.Panel1.Controls.Add(iXVlWindow)


                    '(部品番号,XLVウィンドウハンドル）
                    mXVLWindowList.Add(iGousya, iXVlWindow)

#If DEBUG Then
                Catch ex As Exception
                    Throw New Exception(String.Format("[{0}]ウィンドウ作成中にエラーが発生しました。:{1}", iGousya, ex.Message))
#End If
                Finally
                    '_MdiForm.pnlRedraw.Visible = False
                End Try

            Next

        End With

    End Sub

#End Region

#Region "部品表ウィンドウを表示する."
    ''' <summary>
    ''' 部品表ウィンドウを表示する.
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub ShowFormParts()

        _FrmParts.MdiParent = mMdiParentForm
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
        Dim iGousya As String = frm.Tag

        Dim iFinalComposition As New List(Of String)
        Dim iFinalcomposition2 As New List(Of String)

        'F品番構成を取得.
        iFinalComposition = getGousyaComposition(iGousya)

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
            iXVlWindow.Initialize(iFinalcomposition2, LoginInfo.Now.UserId, mEventCode, iGousya)
            '出力ウィンドウ指定.
            iXVlWindow.MdiParent = mMdiParentForm

            'ウィンドウタイトルを指定.
            iXVlWindow.Text = String.Format("[{0}] ", iGousya)

            'フォームのTagにファイナル品番を保持
            iXVlWindow.Tag = iGousya

            '部品番号をラベルに表示
            iXVlWindow.lblHinban.Text = iGousya

            'フォーム表示状態プロパティを設定
            iXVlWindow.Left = mXVLWindowList(iGousya).Left
            iXVlWindow.Top = mXVLWindowList(iGousya).Top
            iXVlWindow.Height = mXVLWindowList(iGousya).Height
            iXVlWindow.Width = mXVLWindowList(iGousya).Width
            iXVlWindow.WindowState = mXVLWindowList(iGousya).WindowState

            'ウィンドウ表示位置をマニュアルに変更.
            iXVlWindow.StartPosition = FormStartPosition.Manual

            Application.DoEvents()

            'iXVlWindow.Show()

            Process.Start(FOLDER_PATH & LoginInfo.Now.UserId & "_" & mEventCode & "_" & iGousya & ".html")

            Application.DoEvents()


            '変更前のデータを削除
            mXVLWindowList(iGousya).Dispose()


            '(部品番号,XLVウィンドウハンドル）
            mXVLWindowList(iGousya) = iXVlWindow


#If DEBUG Then
        Catch ex As Exception
            Throw New Exception(String.Format("[{0}]ウィンドウ作成中にエラーが発生しました。:{1}", iGousya, ex.Message))
#End If
        Finally
            '_MdiForm.pnlRedraw.Visible = False
        End Try
    End Sub
#End Region

    ''' <summary>
    ''' チェックボックス変更時にXVLファイルウィンドウに表示している構成を更新する.
    ''' </summary>
    ''' <param name="aPartsNo"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function WindowReDraw(ByVal aPartsNo As String) As Boolean

        '2014/04/09 kabasawa'
        '初期化対応'
        If mXVLWindowList Is Nothing Then
            '表示中構成保持メンバを初期化.
            mXVLWindowList = New Dictionary(Of String, FrmVeiwerImge)
        End If


        '指定された部品番号を含むF品番のウィンドウを更新する.
        'Dim iRowIndex As Integer = getRowIndex(aPartsNo)
        Dim iRowIndex As Integer = aPartsNo
        For lColIndex As Integer = 0 To spdParts_Sheet1.ColumnCount - 1

            '号車が有効かチェック（visible=falseの場合は前画面で非選択）
            If False = spdParts_Sheet1.Columns(lColIndex).Visible Then Continue For

            'F品番列チェック.
            If False = isGousya(lColIndex) Then Continue For

            Dim iInzuStr As String = spdParts_Sheet1.Cells(iRowIndex, lColIndex).Value
            Dim iInzuInt As Integer = 0
            '数値チェック.
            If False = Integer.TryParse(iInzuStr, iInzuInt) Then Continue For

            'F品番の取得.
            Dim iFhinban As String = spdParts_Sheet1.Columns(lColIndex).Tag

            Dim iFinalComposition As List(Of String)
            Dim iFinalcomposition2 As New List(Of String)

            'F品番構成を取得.
            iFinalComposition = getGousyaComposition(iFhinban)

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
                iXVlWindow.MdiParent = mMdiParentForm

                'ウィンドウタイトルを指定.
                iXVlWindow.Text = String.Format("[{0}] ", iFhinban)

                'フォームのTagにファイナル品番を保持
                iXVlWindow.Tag = iFhinban

                '部品番号をラベルに表示
                iXVlWindow.lblHinban.Text = iFhinban

                If mXVLWindowList.ContainsKey(iFhinban) Then
                    iXVlWindow.Left = mXVLWindowList(iFhinban).Left
                    iXVlWindow.Top = mXVLWindowList(iFhinban).Top
                    iXVlWindow.WindowState = mXVLWindowList(iFhinban).WindowState
                End If

                'ウィンドウ表示位置をマニュアルに変更.
                iXVlWindow.StartPosition = FormStartPosition.Manual

                'iXVlWindow.Show()
                Process.Start(FOLDER_PATH & LoginInfo.Now.UserId & "_" & mEventCode & "_" & iFhinban & ".html")

                Application.DoEvents()

                'mMdiParentForm.spltViewer.Panel1.Controls.Add(iXVlWindow)

                '変更前のデータを削除
                If mXVLWindowList.ContainsKey(iFhinban) Then
                    mXVLWindowList(iFhinban).Dispose()
                End If
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

    ''' <summary>
    ''' 号車品番の構成を取得.
    ''' </summary>
    ''' <param name="aGousya">号車名を指定.</param>
    ''' <returns>号車ごとの部品構成を復帰。構成が無い場合はNothingを復帰</returns>
    ''' <remarks></remarks>
    Private Function getGousyaComposition(ByVal aGousya As String) As List(Of String)
        getGousyaComposition = Nothing

        Dim iRetList As New List(Of String)

        With spdParts_Sheet1
            Dim iCol As Integer? = Nothing
            Try
                'ファイナル品番の列位置を取得.
                iCol = .Columns(aGousya).Index

            Catch ex As Exception
                Exit Try
            End Try

            '列位置情報が存在しない場合は処理中断.
            If False = iCol.HasValue Then Exit Function

            For lRow As Integer = DETAIL_ROW To .RowCount - 1
                '表示チェックボックスチェック.
                If False = .Cells(lRow, 0).Value Then
                    Continue For
                End If

                '号車取得.
                Dim iInzuStr As String = .Cells(lRow, iCol).Value
                Dim iInzuInt As Integer = 0
                '数値チェック.
                If False = Integer.TryParse(iInzuStr, iInzuInt) Then Continue For

                '部品番号の取得.
                Dim iPartsNo As String = .Cells(lRow, .Columns(COSNT_BUHIN_NO).Index).Value

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

        getGousyaComposition = iRetList

    End Function

    ''' <summary>
    ''' XVLファイル存在チェック.
    ''' </summary>
    ''' <param name="aComposition"></param>
    ''' <remarks></remarks>
    Private Sub FinalCompositionExistXVL(ByRef aComposition As List(Of String), ByRef aComposition2 As List(Of String))
        Dim iChk As New List(Of String)
        Dim iChk2 As New List(Of String)

        If aComposition Is Nothing Then Exit Sub
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
    ''' 表示可否チェックボックスの状態を取得.
    ''' </summary>
    ''' <param name="aPartsNo"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function getViewCheck(ByVal aPartsNo As String) As Boolean
        Dim iRowIndex As Integer = getRowIndex(aPartsNo)
        Return spdParts_Sheet1.Cells(iRowIndex, 0).Value
    End Function

    ''' <summary>
    ''' 部品番号から行位置を取得.
    ''' </summary>
    ''' <param name="aPartsNo"></param>
    ''' <returns></returns>
    ''' <remarks><para></para></remarks>
    Private Function getRowIndex(ByVal aPartsNo As String) As Integer
        Dim iRetRowIndex As New List(Of Integer)

        Dim iRowindex As Integer = DETAIL_ROW - 1
        For Each lData In mBaseData
            '行位置を下に移動.
            iRowindex += 1

            If lData.BuhinNo Is Nothing Then Continue For
            If lData.BuhinNo <> aPartsNo Then Continue For

            '行位置を設定.
            iRetRowIndex.Add(iRowindex)
            Exit For

        Next

        'For iRow As Integer = 0 To spdParts_Sheet1.RowCount - 1
        '    Dim iStr As String = spdParts_Sheet1.Cells(iRow, 8).Value

        '    If iStr Is Nothing Then Continue For

        '    If iStr <> aPartsNo Then Continue For
        '    '行位置を設定.
        '    iRowIndex.Add(iRow)

        'Next
        If 1 < iRetRowIndex.Count Then Throw New Exception("同一部品番号が複数あります.")

        Return iRetRowIndex(0)

    End Function

#Region "ボディー選択時の部品追加処理"


    ''' <summary>
    ''' ボディー選択時にボディー構成パーツをベースのパーツリストに追加する.
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub moreBodyParts(ByRef aBodyComp As List(Of String))

        'ベースデータが存在しない場合はボディーを表示させない.
        If aBodyComp Is Nothing Then Exit Sub

        Dim iArg As New BodyMSTMainteVO
        iArg.KaihatsuFugo = mKaihatsuFugo
        iArg.BodyName = BodyName

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
                'iArg2270.BlockNo = lParts.BlockNo
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

            'XVLファイルが存在するかチェック.
            Dim iFullPath As String = XVLFileDir & XVLFileSubDir & "\" & iVo(0).XvlFileName
            If mFiles IsNot Nothing AndAlso False = mFiles.Contains(iFullPath) Then Continue For

            'XVLファイル名を取得.
            aBodyComp.Add(iVo(0).XvlFileName)
        Next

    End Sub

#End Region

#Region "あとで共通化するメソッド"
    ''' <summary>
    ''' 3D有無チェック
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub chkFileUmu(ByVal aBlockNo As String)
        'ブロックリストを取得
        Dim BlockList As List(Of String) = ShisakuCommon.Db.EBom.Dao.Impl.TShisakuBuhinEditGousyaTmp3dDaoImpl.getBlockList(GUID)
        'XVLファイルリストを作成.
        mFiles = New List(Of String)(getFileName3D(BlockList))


        With spdParts_Sheet1

            mXLVFileList = New Dictionary(Of String, String)
            mXLVFilePathList = New Dictionary(Of String, String)

            For lRow As Integer = DETAIL_ROW To .RowCount - 1

                '部品番号が空白であれば、有無状態を設定しない。
                If StringUtil.IsEmpty(.Cells(lRow, .Columns(COSNT_BUHIN_NO).Index).Value) Then
                    .Cells(lRow, 0).Locked = True
                    .Cells(lRow, 0).Value = False
                End If

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
                    'ブロック番号
                    Dim iBlockNo As String = ""

                    '部品№の取得
                    iBuhinNo = .Cells(lRow, .Columns(COSNT_BUHIN_NO).Index).Text
                    'ブロック№の取得
                    iBlockNo = .Cells(lRow, .Columns(COSNT_BLOCK).Index).Text

                    'キー情報が足らない場合は次のレコードへ.
                    If StringUtil.IsEmpty(iBuhinNo) Then Continue For
                    If StringUtil.IsEmpty(iBlockNo) Then Continue For

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

                        iArg.BlockNo = iBlockNo
                        iVo = get3DFileName(iArg)
#If DEBUG Then
                    Catch ex As Exception
                        MessageBox.Show(ex.Message)
#End If
                    Finally
                    End Try

                    '基本的に１フォルダに同一名のファイルは存在しない.
                    '複数フォルダを同時に検索する場合、ファイル名は注意すること.
                    '抽出件数チェック.
                    If 1 < iVo.Count Then Throw New Exception("３Ｄファイル名検索中に、複数のレコードが抽出されました.条件を見直してください.")

                    'ファイル名の取得.
                    If iVo.Count <> 0 Then iFileName = iVo(0).XvlFileName

                    If StringUtil.IsNotEmpty(iFileName) Then
                        'If mFiles.Contains(XVLFileDir & XVLFileSubDir & "\" & iFileName) Then
                        '    '
                        '    mXLVFileList.Add(iBuhinNo, iFileName)
                        '    .Cells(lRow, 1).Value = "○"
                        '    'チェックボックス
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
                                If mXLVFileList.ContainsKey(iBuhinNo) = False Then
                                    mXLVFileList.Add(iBuhinNo, iFileName)
                                    If Not mXLVFilePathList.ContainsKey(iFileName) Then
                                        mXLVFilePathList.Add(iFileName, sFileName)
                                    End If
                                End If

                                .Cells(lRow, 1).Value = "○"
                                'チェックボックス = ON(1)
                                '.Cells(lRow, 0).Value = True
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
    ''' 3D有無チェック
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub chkFileUmu()

        Dim list As List(Of RHAC2270Vo) = get3DFileName(GUID, mKaihatsuFugo)
        'ブロック、部品番号、ファイル名'
        Dim dic As New Dictionary(Of String, Dictionary(Of String, String))

        For Each vo As RHAC2270Vo In list
            vo.BlockNo = vo.BlockNo.Trim
            vo.BuhinNo = vo.BuhinNo.Trim
            If Not dic.ContainsKey(vo.BlockNo) Then
                dic.Add(vo.BlockNo, New Dictionary(Of String, String))
            End If
            If Not dic(vo.BlockNo).ContainsKey(vo.BuhinNo) Then
                dic(vo.BlockNo).Add(vo.BuhinNo, vo.XvlFileName)
            End If
        Next


        With spdParts_Sheet1

            mXLVFileList = New Dictionary(Of String, String)
            mXLVFilePathList = New Dictionary(Of String, String)

            For lRow As Integer = DETAIL_ROW To .RowCount - 1

                '部品番号が空白であれば、有無状態を設定しない。
                If StringUtil.IsEmpty(.Cells(lRow, .Columns(COSNT_BUHIN_NO).Index).Value) Then
                    .Cells(lRow, 0).Locked = True
                    .Cells(lRow, 0).Value = False
                End If

                If Not .Rows(lRow).Visible Then
                    .Cells(lRow, 0).Locked = True
                    .Cells(lRow, 0).Value = False
                    .Cells(lRow, 1).Value = "×"

                    Continue For
                End If


                '部品番号
                Dim iBuhinNo As String = ""
                'ファイル名
                Dim iFileName As String = ""
                'ブロック番号
                Dim iBlockNo As String = ""

                '部品№の取得
                iBuhinNo = .Cells(lRow, .Columns(COSNT_BUHIN_NO).Index).Text
                'ブロック№の取得
                iBlockNo = .Cells(lRow, .Columns(COSNT_BLOCK).Index).Text

                'キー情報が足らない場合は次のレコードへ.
                If StringUtil.IsEmpty(iBuhinNo) Then Continue For
                If StringUtil.IsEmpty(iBlockNo) Then Continue For

                iFileName = getFileName3D(dic, iBlockNo, iBuhinNo)
                'ファイル名の取得.
                If StringUtil.IsNotEmpty(iFileName) Then

                    '指定ディレクトリから取得したパス付ファイル名にDBから取得したファイル名が含まれるか
                    Dim SearchFlg As Integer = 0

                    '20140523
                    'RHAC2270ファイルパスがフルパスとなった為、パス設定の変更を行なう
                    Dim sFileName As String = iFileName


                    '同一部品が有る場合は追加しない
                    If mXLVFileList.ContainsKey(iBuhinNo) = False Then
                        mXLVFileList.Add(iBuhinNo, iFileName)
                        If Not mXLVFilePathList.ContainsKey(iFileName) Then
                            mXLVFilePathList.Add(iFileName, sFileName)
                        End If
                    End If

                    .Cells(lRow, 1).Value = "○"
                    'チェックボックス = ON(1)
                    '.Cells(lRow, 0).Value = True
                    SearchFlg = 1

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

            Next

        End With

    End Sub

    ''' <summary>
    ''' ファイル名取得
    ''' </summary>
    ''' <param name="dic"></param>
    ''' <param name="blockNo"></param>
    ''' <param name="buhinNo"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function getFileName3D(ByVal dic As Dictionary(Of String, Dictionary(Of String, String)), ByVal blockNo As String, ByVal buhinNo As String) As String
        Dim result As String = ""
        If dic.ContainsKey(blockNo) Then
            If dic(blockNo).ContainsKey(buhinNo) Then
                result = dic(blockNo)(buhinNo)
            Else
                '10桁で探索'
                If buhinNo.Length > 10 Then
                    Dim buhinNo10 As String = Left(buhinNo, 10)
                    If dic(blockNo).ContainsKey(buhinNo10) Then
                        result = dic(blockNo)(buhinNo10)
                    Else
                        Dim buhinNoColor As String = buhinNo10 & "##"
                        If dic(blockNo).ContainsKey(buhinNoColor) Then
                            result = dic(blockNo)(buhinNoColor)
                        End If

                    End If
                Else



                End If

            End If
        End If


        Return result
    End Function



    ''' <summary>
    ''' 3Dファイル名称取得
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function getFileName3D(ByVal aBlockNo As List(Of String)) As String()

        Dim time As New Stopwatch
        time.Start()

        Dim iFiles As String() = Nothing

        Dim i3DFileList As New List(Of String)

        For Each lBlockNo In aBlockNo
            Dim iBlockFiles As New List(Of String)

            Try
                '３ｄファイルのフルパスを生成.
                Dim aDirectory As String = ShisakuCommon.ShisakuGlobal.XVLFileDir1 & mKaihatsuFugo & ShisakuCommon.ShisakuGlobal.XVLFileDir2

                Dim aFileParam As String = "*" & lBlockNo & "*.xv*"
                iFiles = System.IO.Directory.GetFiles(aDirectory, aFileParam, System.IO.SearchOption.AllDirectories)

                iBlockFiles = New List(Of String)(iFiles)

            Catch ex As Exception
                Debug.WriteLine("blockno:" & lBlockNo & "の3D情報はありません。：" & ex.Message)
            End Try

            'ブロック毎のファイル一覧に退避.
            i3DFileList.AddRange(iBlockFiles)

        Next

        If i3DFileList.Count = 0 Then
            MessageBox.Show("3D表示用ファイルがありません。", "3D情報", MessageBoxButtons.OK, MessageBoxIcon.Information)
        End If

        Console.WriteLine("ファイル取得完了" & time.Elapsed.ToString)

        Return i3DFileList.ToArray
    End Function
#End Region

#Region "DBアクセス"

    ''' <summary>
    ''' 開発符号の取得.
    ''' </summary>
    ''' <remarks></remarks>
    Private Function getKaihatsuFugo(ByVal aEventCode As String) As String
        Dim iDao As New ShisakuCommon.Db.EBom.Dao.Impl.TShisakuEventDaoImpl()
        Dim iVos As ShisakuCommon.Db.EBom.Vo.TShisakuEventVo
        iVos = iDao.FindByPk(aEventCode)

        Dim iKaihatsuFugo As String = ""

        '開発符号を設定.
        iKaihatsuFugo = iVos.ShisakuKaihatsuFugo

        '#If DEBUG Then
        '        iKaihatsuFugo = InputBox("デバッグ用に開発符号を入力", "デバッグ用", "BF4")
        '        If StringUtil.IsEmpty(iKaihatsuFugo) Then Throw New Exception("デバッグ用開発符号が入力されませんでした.")
        '#End If

        Return iKaihatsuFugo

    End Function

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

        ''キャドデータイベント区分”G”で検索
        'aArg.CadDataEventKbn = "G"
        'iVos = iDao.getKbnXVLFileName(aArg)
        'If iVos.Count <> 0 Then Return iVos

        ''キャドデータイベント区分”ND”で検索
        'aArg.CadDataEventKbn = "ND"
        'iVos = iDao.getKbnXVLFileName(aArg)
        'If iVos.Count <> 0 Then Return iVos

        ' ''ここでファイルがなければ抜ける.
        'iVos = iDao.getXVLFileName(aArg)
        'If iVos.Count = 0 Then Return iVos

        ''キャドデータイベント区分”S3”で検索
        'aArg.CadDataEventKbn = "S3"
        'iVos = iDao.getKbnXVLFileName(aArg)
        'If iVos.Count <> 0 Then Return iVos

        ''キャドデータイベント区分”S2”で検索
        'aArg.CadDataEventKbn = "S2"
        'iVos = iDao.getKbnXVLFileName(aArg)
        'If iVos.Count <> 0 Then Return iVos

        ''キャドデータイベント区分”S1”で検索
        'aArg.CadDataEventKbn = "S1"
        'iVos = iDao.getKbnXVLFileName(aArg)
        'If iVos.Count <> 0 Then Return iVos

        iVos = iDao.getKbnXVLFileName(aArg)
        If iVos.Count <> 0 Then Return iVos


        'ここまで来てた場合はファイルが存在しないこととする.
        Return New List(Of RHAC2270Vo)

    End Function

    ''' <summary>
    ''' DBからファイル名の取得
    ''' </summary>
    ''' <param name="aGUID">GUID</param>
    ''' <param name="kaihatsuFugo">開発符号</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function get3DFileName(ByVal aGUID As Guid, ByVal kaihatsuFugo As String) As List(Of RHAC2270Vo)
        '開発符号、ブロック番号、部品番号からファイル名を取得
        Dim iDao As New RHAC2270DaoImpl
        Dim iVos As New List(Of RHAC2270Vo)

        '上から順に優先度高、
        '優先順位はキャドデータ区分が
        'G>ND>S3>S2>S1
        '上記で存在しない場合は
        'ファイル拡張子をチェック

        iVos = iDao.getKbnXVLFileName(aGUID, kaihatsuFugo)
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

#Region "Tempテーブルから該当するGUIDのデータを削除"
    ''' <summary>
    ''' プライマリーキーを指定して一括削除.
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub DeleteTempData()
        Dim iGuid As String = GUID.ToString
        Dim iEvent As String = EventCode
        Dim iBuka As String = Nothing
        Dim iBlock As String = Nothing
        Dim iBlockKaitei As String = Nothing
        Dim iBuhinJunjyo As Integer? = Nothing
        Dim iGousyaJunjyo As Integer? = Nothing
        Dim iGyouid As Integer? = Nothing

        '2014/04/08 kabasawa'
        'sqlベタ書きじゃないとテーブルアクセスに失敗するので'

        '基本情報テーブルの削除
        Dim iDao As New ShisakuBuhinEditTmp3DDaoImpl
        'Dim iDao As New TShisakuBuhinEditTmp3dDaoImpl
        'iDao.DeleteByPk(iGuid, iEvent, iBuka, iBlock, iBlockKaitei, iBuhinJunjyo, iGyouid)
        Dim iArg As New TShisakuBuhinEditTmp3dVo
        iArg.Guid = GUID.ToString
        iArg.ShisakuEventCode = EventCode
        'Dim iDelCnt As Integer = iDao.DeleteBy(iArg)
        iDao.DeleteByShisakuBuhinEditTmp3D(iArg)

        '号車テーブルの削除.
        'Dim iGousyaDao As New TShisakuBuhinEditGousyaTmp3dDaoImpl
        'iGousyaDao.DeleteByPk(iGuid, iEvent, iBuka, iBlock, iBlockKaitei, iBuhinJunjyo, iGousyaJunjyo, iGyouid)
        'Dim iGousyaArg As New TShisakuBuhinEditGousyaTmp3dVo
        'iGousyaArg.Guid = iGuid
        'iGousyaArg.ShisakuEventCode = iEvent
        'Dim iDeleteGouisyaCnt As Integer = iGousyaDao.DeleteBy(iGousyaArg)
        Dim iGousyaDao As New ShisakuBuhinEditGousyaTmp3DDaoImpl
        Dim iGousyaArg As New TShisakuBuhinEditGousyaTmp3dVo
        iGousyaArg.Guid = iGuid
        iGousyaArg.ShisakuEventCode = iEvent

        iGousyaDao.DeleteByShisakuBuhinEditGousyaTmp3D(iGousyaArg)

    End Sub

#End Region


#End Region

#Region "作成済みデータから作成"

    ''' <summary>
    ''' T_SHISAKU_BUHIN_EDIT,T_SHISAKU_TEHAI_GOUSYAテーブルにレコードが存在するかチェック.
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function existTable() As Boolean
        Dim iRetCode As Boolean = False

        'イベントコードを指定.
        Dim iBuhinTBL As Boolean = existShisakuBuhinEdit(EventCode)

        'イベントコードとリストコードを指定.
#If DEBUG Then
        'ListCode = "BF4-d-T01-BBB01"
#End If

        '2014/04/15 kabasawa'


        Dim iGousyaTBL As Boolean = existShisakuTehaiGousya(EventCode, ListCode)

        'レコードが両テーブルに存在する場合はＴｒｕｅ
        If iBuhinTBL And iGousyaTBL Then
            iRetCode = True
        End If

        Return iRetCode

    End Function

    ''' <summary>
    ''' 試作部品編集テーブルに該当指定のイベントコードが作成ずみかチェック
    ''' </summary>
    ''' <param name="aEventCode"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function existShisakuBuhinEdit(ByVal aEventCode As String) As Boolean

        Dim iDao As TShisakuBuhinEditDao = New TShisakuBuhinEditDaoImpl()

        'イベントコードをキーに試作部品テーブルにレコードが存在するかチェック.
        Return iDao.ExistByEventCode(aEventCode)

    End Function

    ''' <summary>
    ''' 試作部品編集テーブルに該当指定のイベントコードが作成ずみかチェック
    ''' </summary>
    ''' <param name="aEventCode"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function existShisakuTehaiGousya(ByVal aEventCode As String, ByVal aListCode As String) As Boolean

        Dim iDao As TShisakuTehaiGousyaDao = New TShisakuTehaiGousyaDaoImpl()

        'イベントコードをキーに試作部品テーブルにレコードが存在するかチェック.
        Return iDao.ExistByShisakuGousya(aEventCode, aListCode, Gousya)

    End Function




#End Region

#Region "Initialからデータ作成"


    Public Sub CreateTempTable()


        '*****************************************************************************
        '*****************************************************************************
        '専用マークを使用しないのでコメントアウトしてパラメータセット用インスタンスのみ作成.
        Dim aAsKpsm10p As New List(Of AsKPSM10PVo)
        Dim aAsPartsp As New List(Of AsPARTSPVo)
        Dim aAsGkpsm10p As New List(Of AsGKPSM10PVo)
        ''専用マーク、購担、取引先取得用データをここで取得しておく。
        'Dim dbTori As New EBomDbClient
        'Dim sqlaAsKpsm10p As String = _
        '    " SELECT BUBA_15, KA, " _
        '    & " TRCD " _
        '    & " FROM " & MBOM_DB_NAME & ".dbo.AS_KPSM10P WITH (NOLOCK, NOWAIT) " _
        '    & " ORDER BY UPDATED_DATE DESC, UPDATED_TIME DESC "
        'Dim aAsKpsm10p As New List(Of AsKPSM10PVo)
        'aAsKpsm10p = dbTori.QueryForList(Of AsKPSM10PVo)(sqlaAsKpsm10p)
        ''
        'Dim sqlaAsPartsp As String = _
        '    " SELECT BUBA_13, KA, " _
        '    & " TRCD " _
        '    & " FROM " & MBOM_DB_NAME & ".dbo.AS_PARTSP WITH (NOLOCK, NOWAIT) " _
        '    & " ORDER BY UPDATED_DATE DESC, UPDATED_TIME DESC "
        'Dim aAsPartsp As New List(Of AsPARTSPVo)
        'aAsPartsp = dbTori.QueryForList(Of AsPARTSPVo)(sqlaAsPartsp)
        ''
        'Dim sqlaAsGkpsm10p As String = _
        '    " SELECT BUBA_15, KA, " _
        '    & " TRCD " _
        '    & " FROM " & MBOM_DB_NAME & ".dbo.AS_GKPSM10P WITH (NOLOCK, NOWAIT) " _
        '    & " ORDER BY UPDATED_DATE DESC, UPDATED_TIME DESC "
        'Dim aAsGkpsm10p As New List(Of AsGKPSM10PVo)
        'aAsGkpsm10p = dbTori.QueryForList(Of AsGKPSM10PVo)(sqlaAsGkpsm10p)

        Dim iMergeList As New List(Of TehaichoBuhinEditTmpVo)

        '手配長作成済みかチェック.
        'If existTable() Then
        '    'GUIDが未設定の場合に作成する.
        '    If GUID = GUID.Empty Then GUID = New Guid
        '    '手配長テーブルからTMP登録用データを作成.
        '    iMergeList = createDataFromTehaityou()
        '    MergeTmp(iMergeList)
        'Else

        '2014/04/18 KABASAWA'
        '部品表からだけ取る'
        'InitialテーブルからTMP登録用データを作成.
        iMergeList = createDataFromInitialTable()

        'End If


        'TMPテーブルにデータを格納.
        InsertBuhinTmp(iMergeList, aAsKpsm10p, aAsPartsp, aAsGkpsm10p)

    End Sub

    ''' <summary>
    ''' 手配帳からTMPに登録するためのListを作成.
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function createDataFromTehaityou() As List(Of TehaichoBuhinEditTmpVo)

        'T_SHISAKU_TEHAI_GOUSYA、T_SHISAKU_BUHIN_EDIT
        'の項目
        'SHISAKU_EVENT_CODE
        'SHISAKU_BUKA_CODE
        'BUHIN_NO_HYOUJI_JUN
        'をキーにinnerJoinする.
        '部品改訂№は値が大きいものを使用する.

        Return getShisakuTehaiGousya(EventCode, ListCode)
    End Function


    ''' <summary>
    ''' IntialからTMPに登録するためのListを作成.
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function createDataFromInitialTable() As List(Of TehaichoBuhinEditTmpVo)

        Dim tmpimpl As TehaichoSakuseiDao = New TehaichoSakuseiDaoImpl
        Dim aBuhinvo As New List(Of TehaichoBuhinEditTmpVo)

        '試作部品編集情報を取得する
        aBuhinvo = tmpimpl.FindByBuhin3D(mEventCode, Group, Gousya)

        Dim MergeList As New List(Of TehaichoBuhinEditTmpVo)

        'TMPをマージする'
        MergeList = MergeTmp(aBuhinvo)

        Return MergeList

    End Function


    '試作部品表編集情報(TMP)を作成する'
    Private Sub InsertBuhinTmp(ByVal MergeList As List(Of TehaichoBuhinEditTmpVo), _
                               ByVal aAsKpsm10p As List(Of AsKPSM10PVo), _
                               ByVal aAsPartsp As List(Of AsPARTSPVo), _
                               ByVal aAsGkpsm10p As List(Of AsGKPSM10PVo))

        Dim iDaoInst As New XVLView.Dao.Impl.ConditionSelectDaoImpl()

        'イベントコードから開発符号を取得.
        Dim iDao As New ShisakuCommon.Db.EBom.Dao.Impl.TShisakuEventDaoImpl()
        Dim iVos As ShisakuCommon.Db.EBom.Vo.TShisakuEventVo
        iVos = iDao.FindByPk(EventCode)
        mKaihatsuFugo = iVos.ShisakuKaihatsuFugo

        '*************************************************************
        'TEMPテーブルにアイテムを追加.
        'マージした部品編集情報を追加'
        iDaoInst.InsertByBuhinEditTmp(GUID, MergeList, Nothing, Nothing, mKaihatsuFugo, Nothing, _
                                     aAsKpsm10p, aAsPartsp, aAsGkpsm10p)

        'マージした部品編集TMP情報を追加'
        iDaoInst.InsertByGousyaTMP(GUID, MergeList)

    End Sub

    ''' <summary>
    ''' 号車列チェック.
    ''' </summary>
    ''' <param name="aColumn"></param>
    ''' <returns></returns>
    ''' <remarks>基本情報以上のカラム値が入力されたら、号車のカラムと判断する.</remarks>
    Public Function isGousya(ByVal aColumn As Integer) As Boolean
        Return CONST_INZU_START_POS <= aColumn
    End Function

    ''' <summary>
    ''' 指定号車ウィンドウを最前面に.
    ''' </summary>
    ''' <param name="aCol"></param>
    ''' <remarks></remarks>
    Public Sub XLVActive(ByVal aCol As Integer)

        If Not isGousya(aCol) Then Exit Sub

        'タグを取得
        Dim iTag As String = spdParts_Sheet1.Columns(aCol).Tag
        If mXVLWindowList Is Nothing Then Exit Sub

        If False = mXVLWindowList.ContainsKey(iTag) Then Exit Sub

        If mXVLWindowList(iTag).IsDisposed = False Then
            ''ウィンドウをアクティベート.
            mXVLWindowList(iTag).BringToFront()
        End If
    End Sub


    ''' <summary>
    ''' マージする(TMP用)
    ''' </summary>
    ''' <param name="MergeList"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function MergeTmp(ByVal MergeList As List(Of TehaichoBuhinEditTmpVo)) As List(Of TehaichoBuhinEditTmpVo)


        '別のやり方を模索中・・・・'
        Dim kyoku1 As String = ""
        Dim kyoku2 As String = ""
        For index As Integer = 0 To MergeList.Count - 1
            'マージ済みならスルー
            If MergeList(index).CreatedUserId = "Merge" Then
                Continue For
            End If

            For index2 As Integer = 0 To MergeList.Count - 1

                'マージ済みならスルー
                If MergeList(index2).CreatedUserId = "Merge" Then
                    Continue For
                End If

                '設計課が同一かチェック'
                If MergeList(index).ShisakuBukaCode <> MergeList(index2).ShisakuBukaCode Then
                    Continue For
                End If

                'ブロックNoが同一かチェック'
                If MergeList(index).ShisakuBlockNo <> MergeList(index2).ShisakuBlockNo Then
                    Continue For
                End If
                'レベルが同一かチェック'
                If MergeList(index).Level <> MergeList(index2).Level Then
                    Continue For
                End If

                '部品番号が同一かチェック'
                If MergeList(index).BuhinNo <> MergeList(index2).BuhinNo Then
                    Continue For
                End If

                '部品番号区分が同一かチェック'
                If MergeList(index).BuhinNoKbn <> MergeList(index2).BuhinNoKbn Then
                    Continue For
                End If
                '-------------------------------------------------------------------------------
                '2012/01/21 供給セクションのチェックを追加
                kyoku1 = MergeList(index).KyoukuSection
                kyoku2 = MergeList(index2).KyoukuSection
                If StringUtil.IsEmpty(kyoku1) Then
                    kyoku1 = ""
                End If
                If StringUtil.IsEmpty(kyoku2) Then
                    kyoku2 = ""
                End If
                '-------------------------------------------------------------------------------

                '集計コードのチェックは複数パターンある'
                If MergeList(index).ShukeiCode.TrimEnd = "" Then

                    If MergeList(index2).ShukeiCode.TrimEnd <> "" Then
                        Continue For
                    End If
                    '国内集計コードが両方空なら海外集計コードを比較'
                    If MergeList(index).SiaShukeiCode <> MergeList(index2).SiaShukeiCode Then
                        Continue For
                    End If
                    '海外集計コードが同一ならマージ可能'

                    '供給セクションを比較'
                    If kyoku1 <> kyoku2 Then
                        Continue For
                    End If

                    '再使用区分が同一か？'
                    If MergeList(index).Saishiyoufuka <> MergeList(index2).Saishiyoufuka Then
                        Continue For
                    End If

                    '号車表示順が若い方はどっち？'
                    If MergeList(index).ShisakuGousyaHyoujiJun < MergeList(index2).ShisakuGousyaHyoujiJun Then
                        MergeList(index2).BuhinNoHyoujiJun = MergeList(index).BuhinNoHyoujiJun
                        MergeList(index2).CreatedUserId = "Merge"
                        Continue For
                    End If

                    If MergeList(index).ShisakuGousyaHyoujiJun > MergeList(index2).ShisakuGousyaHyoujiJun Then
                        MergeList(index).BuhinNoHyoujiJun = MergeList(index2).BuhinNoHyoujiJun
                        MergeList(index).CreatedUserId = "Merge"
                        Continue For
                    End If

                    '号車表示順が同じでも部品番号表示順が違うパターンがいる'
                    If MergeList(index).BuhinNoHyoujiJun < MergeList(index2).BuhinNoHyoujiJun Then
                        MergeList(index2).BuhinNoHyoujiJun = MergeList(index).BuhinNoHyoujiJun
                        MergeList(index2).CreatedUserId = "Merge"
                        Continue For
                    End If
                    If MergeList(index).BuhinNoHyoujiJun > MergeList(index2).BuhinNoHyoujiJun Then
                        MergeList(index).BuhinNoHyoujiJun = MergeList(index2).BuhinNoHyoujiJun
                        MergeList(index).CreatedUserId = "Merge"
                        Continue For
                    End If


                Else
                    '両方からでないかチェック'
                    If MergeList(index2).ShukeiCode.TrimEnd = "" Then
                        Continue For
                    End If
                    '両方空でないならチェック'
                    If MergeList(index).ShukeiCode <> MergeList(index2).ShukeiCode Then
                        Continue For
                    End If
                    '国内集計コードが同一'

                    '供給セクションを比較'
                    If kyoku1 <> kyoku2 Then
                        Continue For
                    End If

                    '再使用区分が同一か？'
                    If MergeList(index).Saishiyoufuka <> MergeList(index2).Saishiyoufuka Then
                        Continue For
                    End If
                    If MergeList(index).ShisakuGousyaHyoujiJun < MergeList(index2).ShisakuGousyaHyoujiJun Then
                        MergeList(index2).BuhinNoHyoujiJun = MergeList(index).BuhinNoHyoujiJun
                        MergeList(index2).CreatedUserId = "Merge"
                        Continue For
                    End If
                    If MergeList(index).ShisakuGousyaHyoujiJun > MergeList(index2).ShisakuGousyaHyoujiJun Then
                        MergeList(index).BuhinNoHyoujiJun = MergeList(index2).BuhinNoHyoujiJun
                        MergeList(index).CreatedUserId = "Merge"
                        Continue For
                    End If

                    '号車表示順が同じでも部品番号表示順が違うパターンがいる'
                    If MergeList(index).BuhinNoHyoujiJun < MergeList(index2).BuhinNoHyoujiJun Then
                        MergeList(index2).BuhinNoHyoujiJun = MergeList(index).BuhinNoHyoujiJun
                        MergeList(index2).CreatedUserId = "Merge"
                        Continue For
                    End If
                    If MergeList(index).BuhinNoHyoujiJun > MergeList(index2).BuhinNoHyoujiJun Then
                        MergeList(index).BuhinNoHyoujiJun = MergeList(index2).BuhinNoHyoujiJun
                        MergeList(index).CreatedUserId = "Merge"
                        Continue For
                    End If

                End If

            Next
        Next

        Return MergeList


    End Function

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
