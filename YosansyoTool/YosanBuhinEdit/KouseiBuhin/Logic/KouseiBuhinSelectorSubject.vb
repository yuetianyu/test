Imports System.Text
Imports Microsoft.VisualBasic.FileIO
Imports EBom.Data
Imports EBom.Common
Imports FarPoint.Win.Spread.CellType
Imports ShisakuCommon
Imports ShisakuCommon.Db.EBom
Imports ShisakuCommon.Db.EBom.Dao
Imports ShisakuCommon.Db.EBom.Dao.Impl
Imports ShisakuCommon.Db.EBom.Vo
Imports ShisakuCommon.Ui
Imports ShisakuCommon.Ui.Spd
Imports ShisakuCommon.Util.LabelValue
Imports YosansyoTool.YosanBuhinEdit.KouseiBuhin.Dao
Imports YosansyoTool.YosanBuhinEdit.KouseiBuhin.Dao.Vo
Imports YosansyoTool.YosanBuhinEdit.Kosei.Logic.Matrix
Imports YosansyoTool.YosanBuhinEdit.Logic
Imports YosansyoTool.XVLView
Imports EventSakusei.TehaichoMenu.Vo
Imports EventSakusei.ShisakuBuhinMenu.Dao


Namespace YosanBuhinEdit.KouseiBuhin.Logic

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <remarks></remarks>
    Public Class KouseiBuhinSelectorSubject

#Region "メンバー変数"

        '''<summary>前画面引継ぎ項目</summary>>
        Private _YosanEventCode As String
        Private _HoyouEventName As String

        ''' <summary>E-BOM部品情報取込フォーム</summary>
        Private _frmKouseiBuhinSelector As Frm41KouseiBuhinSelector

        ''' <summary>ロジック</summary>
        Private m_PatternList As DispPatternList = Nothing

        Private m_kubunList As DispKubunList = Nothing
        Private m_blockList As DispBlockList = Nothing
        Private m_buhinList As DispBuhinList = Nothing
        Private m_shiyouJyouhouList As DispShiyouJyouhouList = Nothing

        Private m_gousyaList As DispGousyaList = Nothing

        ''' <summary>FpSpread共通メソッド</summary>
        Private m_spCom As SpreadCommon

        ''' <summary>CSVカラム</summary>
        Private Const columnBUKA_CODE As Integer = 35
        Private Const columnBLOCK_NO As Integer = 0
        Private Const columnLEVEL As Integer = 1
        Private Const columnSHUKEI_CODE As Integer = 4
        Private Const columnSIA_SHUKEI_CODE As Integer = 5
        Private Const columnGENCYO_CKD_KBN As Integer = 6
        Private Const columnKYOYO_KBN As Integer = 7
        Private Const columnBUHIN_NAME As Integer = 8
        Private Const columnHOJO_NAME As Integer = 9
        Private Const columnBUHIN_NO As Integer = 2
        Private Const columnKOUSEI As Integer = 33

        Private Const columnintGousyaStart As Integer = 44

        ''' <summary>基本情報カラム</summary>
        Private Const basicBuhinNo As Integer = 13
        Private Const basicBuhinName As Integer = 11
        Private Const basicBlockNo As Integer = 1
        Private Const basicLevel As Integer = 6
        Private Const basicShukeiCode As Integer = 7
        Private Const basicSiaShukeiCode As Integer = 8
        Private Const basicBuhinNote As Integer = 18
        Private Const basicBuhinKousei As Integer = 20

        'セルタイプ
        Private buttonType As New ButtonCellType
        Private textType As New TextCellType


#Region "部品SPREADカラム"
        '
        ''' <summary>部品SPREADカラム</summary>
        ''' 
#If 1 Then
        Public spd_Buhin_Col_3DUMU As Integer = 0
        Public spd_Buhin_Col_BlockNo As Integer = spd_Buhin_Col_3DUMU + 1
        Public spd_Buhin_Col_Level As Integer = spd_Buhin_Col_BlockNo + 1
        Public spd_Buhin_Col_ShukeiCode As Integer = spd_Buhin_Col_Level + 1
        Public spd_Buhin_Col_SiaShukeiCode As Integer = spd_Buhin_Col_ShukeiCode + 1
        Public spd_Buhin_Col_Tenkai As Integer = spd_Buhin_Col_SiaShukeiCode + 1
        Public spd_Buhin_Col_Select As Integer = spd_Buhin_Col_Tenkai + 1
        Public spd_Buhin_Col_BuhinNo As Integer = spd_Buhin_Col_Select + 1
        Public spd_Buhin_Col_Insu As Integer = spd_Buhin_Col_BuhinNo + 1
        Public spd_Buhin_Col_BuhinName As Integer = spd_Buhin_Col_Insu + 1
        Public spd_Buhin_Col_Note As Integer = spd_Buhin_Col_BuhinName + 1
        Public spd_Buhin_Col_SelectionMethod As Integer = spd_Buhin_Col_Note + 1
        Public spd_Buhin_Col_KyoKyu As Integer = spd_Buhin_Col_SelectionMethod + 1
        Public spd_Buhin_Col_BukaCode As Integer = spd_Buhin_Col_KyoKyu + 1

#Else
        Private Const spd_Buhin_Col_BlockNo As Integer = 0
        Private Const spd_Buhin_Col_Level As Integer = 1
        Private Const spd_Buhin_Col_ShukeiCode As Integer = 2
        Private Const spd_Buhin_Col_SiaShukeiCode As Integer = 3
        Private Const spd_Buhin_Col_Tenkai As Integer = 4
        Private Const spd_Buhin_Col_Select As Integer = 5
        Private Const spd_Buhin_Col_BuhinNo As Integer = 6
        Private Const spd_Buhin_Col_Insu As Integer = 7
        Private Const spd_Buhin_Col_BuhinName As Integer = 8
        Private Const spd_Buhin_Col_Note As Integer = 9
        Private Const spd_Buhin_Col_SelectionMethod As Integer = 10
        Private Const spd_Buhin_Col_KyoKyu As Integer = 11
        Private Const spd_Buhin_Col_BukaCode As Integer = 12
#End If

#End Region



        Private Shared ReadOnly EMPTY_LIST As New List(Of LabelValueVo)

        Private Shared ReadOnly EMPTY_FINAL_HINBAN_LIST As New List(Of FinalHinbanListVo)

        Public Const spd_BuhinShiyou_startRow As Integer = 4
        Private Const spd_BuhinShiyou_KaihatuFugoRow As Integer = 1
        Private Const spd_BuhinShiyou_OpSpecCodeRow As Integer = 2
        Private Const spd_BuhinShiyou_OpSpecNameRow As Integer = 3

#End Region

#Region "プロパティ"

#Region "CSV位置情報"

        '質量情報
        Private _MassStart As Integer
        Private _MassCount As Integer
        'コスト情報
        Private _CostStart As Integer
        Private _CostCount As Integer
        '号車情報
        Private _GoshaStart As Integer
        Private _GoshaCount As Integer

        Public Property MassStart() As Integer
            Get
                Return _MassStart
            End Get
            Set(ByVal value As Integer)
                _MassStart = value
            End Set
        End Property

        Public Property MassCount() As Integer
            Get
                Return _MassCount
            End Get
            Set(ByVal value As Integer)
                _MassCount = value
            End Set
        End Property

        Public Property CostStart() As Integer
            Get
                Return _CostStart
            End Get
            Set(ByVal value As Integer)
                _CostStart = value
            End Set
        End Property

        Public Property CostCount() As Integer
            Get
                Return _CostCount
            End Get
            Set(ByVal value As Integer)
                _CostCount = value
            End Set
        End Property

        Public Property GoshaStart() As Integer
            Get
                Return _GoshaStart
            End Get
            Set(ByVal value As Integer)
                _GoshaStart = value
            End Set
        End Property

        Public Property GoshaCount() As Integer
            Get
                Return _GoshaCount
            End Get
            Set(ByVal value As Integer)
                _GoshaCount = value
            End Set
        End Property

#End Region

#End Region

#Region "コンストラクタ"

        ''' <summary>
        ''' コンストラクタ
        ''' </summary>
        ''' <param name="aYosanEventCode">補用イベントコード</param>
        ''' <param name="frm"></param>
        ''' <remarks></remarks>
        Public Sub New(ByVal aYosanEventCode As String, ByVal frm As Frm41KouseiBuhinSelector)

            '前画面引継
            _YosanEventCode = aYosanEventCode

            'E-BOM部品情報取込
            _frmKouseiBuhinSelector = frm

            '仕様情報リストセルタイプ指定
            Try
                m_shiyouJyouhouList = New DispShiyouJyouhouList(frm)
                m_shiyouJyouhouList.InitView()
            Catch ex As Exception
                Throw
            End Try

        End Sub

#End Region

#Region "ヘッダー部初期化"
        ''' <summary>
        '''ヘッダー部を初期化する
        ''' </summary>
        ''' <remarks></remarks>
        Public Sub InitializeHeader()

            Dim frm As Frm41KouseiBuhinSelector = _frmKouseiBuhinSelector

            'ヘッダー部分の初期設定
            ShisakuFormUtil.setTitleVersion(frm)
            ShisakuFormUtil.SetDateTimeNow(frm.LblDateNow, frm.LblTimeNow)
            ShisakuFormUtil.SetIdAndBuka(frm.LblCurrUserId, frm.LblCurrBukaName)

            '画面のPG-IDが表示されます。
            frm.LblCurrPGId.Text = "PG-ID :" + "BUHIN SELECTOR"

            'コンボボックス初期設定
            ShisakuFormUtil.SettingDefaultProperty(frm.cmbKaihatsuFugo)
            ShisakuFormUtil.SettingDefaultProperty(frm.cmbSelect)
            ShisakuFormUtil.SettingDefaultProperty(frm.cmbJikyuhinUmu)
            ShisakuFormUtil.SettingDefaultProperty(frm.cmbShisakuEventCode)
            ShisakuFormUtil.SettingDefaultProperty(frm.cmbShisakuEventName)

            ShisakuFormUtil.SettingDefaultProperty(frm.cmbBlockName)
        End Sub

#End Region

#Region "開発符号のコンボボックスを作成"

        ''' <summary>
        ''' 開発符号のコンボボックスを作成
        ''' </summary>
        ''' <remarks></remarks>
        Public Sub SetKaihatsuFugoCombo()
            Try
                '開発符号コンボボックスに値を追加
                Dim dtKaihatsufugo As DataTable = GetKaihatsuFugoData()
                Dim vos As New List(Of LabelValueVo)
                For idx As Integer = 0 To dtKaihatsufugo.Rows.Count - 1
                    Dim vo As New LabelValueVo
                    vo.Label = dtKaihatsufugo.Rows(idx).Item(0)
                    vo.Value = vo.Label
                    vos.Add(vo)
                Next
                Dim frm As Frm41KouseiBuhinSelector = _frmKouseiBuhinSelector
                FormUtil.BindLabelValuesToComboBox(frm.cmbKaihatsuFugo, vos, True)
            Catch ex As Exception
                MessageBox.Show(ex.Message, "異常", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End Try
        End Sub

        ''' <summary>
        ''' E-BOM開発符号よりイベントコードを設定する。
        ''' </summary>
        ''' <returns>全データテーブル</returns>
        ''' <remarks></remarks>
        Private Function GetKaihatsuFugoData() As DataTable
            Dim dtData As New DataTable
            Using db As New SqlAccess(g_kanrihyoIni(DB_KEY_mBOM))
                db.Open()
                db.Fill(DataSqlCommon.GetDevSignSql, dtData)
            End Using
            Return dtData
        End Function

#End Region

#Region "3D画面表示処理"
        ''' <summary>
        ''' 3D画面表示処理メイン
        ''' </summary>
        ''' <param name="nRow">対象行</param>
        ''' <returns>エラーメッセージ</returns>
        ''' <remarks></remarks>
        Public Function Show3D(ByVal nRow As Integer) As String
            Dim frm As Frm41KouseiBuhinSelector = _frmKouseiBuhinSelector
            Dim strKaihatuFugo As String
            Dim strBlockNo As String
            Dim strBuhinNo As String
            Dim strLevel As String
            Dim strMsg As String = String.Empty

            Dim lFileList As New List(Of String)
            Dim lBuhinList As New List(Of String)

            Dim nNextRow As Integer = 0

            Try
                '選択行の情報を取得
                strKaihatuFugo = frm.cmbKaihatsuFugo.Text
                strBlockNo = frm.spdBuhin_Sheet1.GetValue(nRow, spd_Buhin_Col_BlockNo)
                strBuhinNo = frm.spdBuhin_Sheet1.GetValue(nRow, spd_Buhin_Col_BuhinNo)
                strLevel = frm.spdBuhin_Sheet1.GetValue(nRow, spd_Buhin_Col_Level)

                '3D表示対象部品リスト取得
                '検索方法コンボが試作手配システムの場合
                If StringUtil.Equals(frm.cmbSelect.Text, HOYOU_SELECT_MBOM_SHISAKUTEHAI) Then
                    '選択行のみを3D表示対象とする
                    lBuhinList.Add(strBuhinNo)
                Else
                    '選択行の次の行を設定
                    If nRow = frm.spdBuhin_Sheet1.RowCount - 1 Then
                        nNextRow = nRow
                    Else
                        nNextRow = nRow + 1
                    End If

                    '選択行のレベルが０でかつ選択行の次の行のレベルが０なら構成未展開とみなす。
                    If strLevel = "0" AndAlso frm.spdBuhin_Sheet1.Cells(nNextRow, spd_Buhin_Col_Level).Value = "0" Then

                        '自給品の有無
                        Dim strJikyu As String = ""
                        If StringUtil.Equals(frm.cmbJikyuhinUmu.Text, "無") Then
                            strJikyu = "0"
                        Else
                            strJikyu = "1"
                        End If

                        '部品構成取得処理(getKouseiDataメソッドの構成情報取得部分を流用)
                        Dim Shisakudate As ShisakuDate = New ShisakuDate
                        Dim buhinstruct As New TehaichoEditBuhinStructure(_YosanEventCode, "ListCodeDummy", strBlockNo, "BukaCodeDummy", Shisakudate)
                        Dim newBuhinMatrix As BuhinKoseiMatrix = buhinstruct.GetKouseiMatrix(strBuhinNo, "", 0, strKaihatuFugo)

                        If newBuhinMatrix Is Nothing OrElse newBuhinMatrix.Records.Count = 0 Then
                            strMsg = "部品構成情報が取得できません"
                            Return strMsg
                        End If

                        '部品構成情報から3D表示対象の部品番号を取得
                        For Each i As Integer In newBuhinMatrix.GetInputRowIndexes

                            'レベル０の行は無条件で部品リストに追加
                            If StringUtil.Equals(newBuhinMatrix(i).YosanLevel, 0) Then
                                lBuhinList.Add(newBuhinMatrix(i).YosanBuhinNo.TrimEnd)
                                Continue For
                            End If

                            ''集計コード取得
                            'Dim strShukeiCode As String = newBuhinMatrix(i).ShukeiCode.TrimEnd
                            'Dim strSiaShukeiCode As String = newBuhinMatrix(i).SiaShukeiCode.TrimEnd

                            ''自給品有無＝無の場合　国内または国外集計コードＪは読み飛ばす。
                            'If strJikyu = "1" Or _
                            '    strJikyu = "0" And strShukeiCode <> "" And strShukeiCode <> "J" Or _
                            '    strJikyu = "0" And strShukeiCode = "" And strSiaShukeiCode <> "" And strSiaShukeiCode <> "J" Then
                            '    '部品リストに構成部品を追加
                            '    lBuhinList.Add(newBuhinMatrix(i).BuhinNo.TrimEnd)
                            'End If
                        Next

                    Else

                        '選択行の部品を部品リストに追加
                        lBuhinList.Add(strBuhinNo.Trim())

                        '構成展開済みの場合は、選択行の次の行から3D表示対象部品を取得
                        For i As Integer = nNextRow To frm.spdBuhin_Sheet1.RowCount - 1
                            '選択行よりレベルが大きい部品までを同一構成とする
                            If Integer.Parse(strLevel) >= Integer.Parse(frm.spdBuhin_Sheet1.GetValue(i, spd_Buhin_Col_Level)) Then
                                Exit For
                            End If

                            '部品リストに追加
                            lBuhinList.Add(CStr(frm.spdBuhin_Sheet1.GetValue(i, spd_Buhin_Col_BuhinNo)).Trim())
                        Next
                    End If
                End If


                '3D画像ファイルリスト取得処理
                lFileList = Get3DFileList(strKaihatuFugo, strBlockNo, lBuhinList)

                '3D画像ファイルが1つも存在しない場合は処理終了
                If lFileList.Count = 0 Then
                    strMsg = "3D情報が存在しません"
                    Return strMsg
                End If

#If DEBUG Then
                For Each file In lFileList
                    Debug.Print(file)
                Next
#End If

                '3D画面表示処理
                Dim iXVlWindow As New FrmVeiwerImge

                '初期化
                iXVlWindow.Initialize(lFileList)

                'ウィンドウタイトルを指定.
                iXVlWindow.Text = String.Format("[{0}] ", strBuhinNo)

                '部品番号をラベルに表示
                iXVlWindow.lblHinban.Text = strBuhinNo

                iXVlWindow.ShowDialog(frm)

                Return strMsg

            Catch ex As Exception
                MessageBox.Show(ex.Message, "異常", MessageBoxButtons.OK, MessageBoxIcon.Error)
                Return strMsg
            End Try
        End Function

        ''' <summary>
        ''' 3D画像ファイルリスト取得処理
        ''' </summary>
        ''' <param name="aKaihatuFugo">開発符号</param>
        ''' <param name="aBlockNo">ブロック番号</param>
        ''' <param name="aBuhinList">部品番号リスト</param>
        ''' <returns>3D画像ファイルリスト</returns>
        ''' <remarks></remarks>
        Private Function Get3DFileList(ByVal aKaihatuFugo As String, ByVal aBlockNo As String, ByVal aBuhinList As List(Of String)) As List(Of String)
            Dim iAll3DFileList As New List(Of String)
            Dim i3DFileList As New List(Of String)
            Dim iFiles As String() = Nothing

            '対象開発符号、ブロックNoの全XVLファイル取得
            Try
                Dim aDirectory As String = ShisakuCommon.ShisakuGlobal.XVLFileDir1 & aKaihatuFugo & ShisakuCommon.ShisakuGlobal.XVLFileDir2
                Dim aFileParam As String = "*" & aBlockNo & "*.xv*"

                iFiles = System.IO.Directory.GetFiles(aDirectory, aFileParam, System.IO.SearchOption.AllDirectories)

                iAll3DFileList = New List(Of String)(iFiles)
            Catch ex As Exception
                Debug.WriteLine("3D情報がありません。", "3D情報" & ex.Message)
                Return i3DFileList
            End Try

            'XVLファイルが存在しない場合は処理終了
            If iAll3DFileList.Count = 0 Then
                Return i3DFileList
            End If

            '3D表示対象の部品番号リストに各部品番号に対して処理
            For Each iBuhin In aBuhinList
                Dim iVo As New List(Of EventSakusei.XVLView.Dao.Vo.RHAC2270Vo)
                Dim iArg As New EventSakusei.XVLView.Dao.Vo.RHAC2270Vo()
                Dim iFileName As String = String.Empty

                iArg.KaihatsuFugo = aKaihatuFugo
                iArg.BuhinNo = iBuhin
                iArg.BlockNo = aBlockNo

                'RHAC2270からXVLファイル名を取得
                iVo = GetXVLFileName(iArg)

                If iVo.Count <> 0 Then iFileName = iVo(0).XvlFileName

                If StringUtil.IsNotEmpty(iFileName) Then
                    '全XVLファイルリストに取得したファイル名が含まれるか
                    For Each sFileName In iAll3DFileList
                        If sFileName.IndexOf(iFileName) >= 0 Then
                            '同一ファイル名が有る場合は追加しない
                            If i3DFileList.Contains(sFileName) = False Then
                                i3DFileList.Add(sFileName)
                            End If

                            Exit For
                        End If
                    Next
                End If
            Next

            Return i3DFileList
        End Function
        ''' <summary>
        ''' DBからXVLファイル名の取得
        ''' </summary>
        ''' <param name="aArg">開発符号、ブロック番号、部品番号を指定.</param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Function GetXVLFileName(ByVal aArg As EventSakusei.XVLView.Dao.Vo.RHAC2270Vo) As List(Of EventSakusei.XVLView.Dao.Vo.RHAC2270Vo)

            Dim iSql As New System.Text.StringBuilder
            With iSql
                .AppendLine("SELECT ")
                .AppendLine("    [XVL_FILE_NAME]")
                .AppendLine("    ,[CAD_DATA_EVENT_KBN]")
                .AppendLine("    ,[CAD_KAITEI_NO]")
                .AppendLine("FROM ")
                .AppendLine("    " & RHACLIBF_DB_NAME & ".[dbo].[RHAC2270]")
                .AppendLine("WHERE")
                .AppendLine("    KAIHATSU_FUGO = @KaihatsuFugo ")
                .AppendLine("    AND BLOCK_NO =  @BlockNo ")
                .AppendLine("    AND BUHIN_NO =  @BuhinNo ")
                .AppendLine("ORDER BY ")
                'CADデータイベント区分の優先順位は G > ND > S3 > S2 > S1
                .AppendLine("    CASE [CAD_DATA_EVENT_KBN]")
                .AppendLine("      WHEN 'G'  THEN 1")
                .AppendLine("      WHEN 'ND' THEN 2")
                .AppendLine("      WHEN 'S3' THEN 3")
                .AppendLine("      WHEN 'S2' THEN 4")
                .AppendLine("      WHEN 'S1' THEN 5")
                .AppendLine("    END ASC")
                .AppendLine("    ,CAD_KAITEI_NO DESC")
            End With

            Dim db As New EBomDbClient
            Dim iFiles As New List(Of EventSakusei.XVLView.Dao.Vo.RHAC2270Vo)
            iFiles = db.QueryForList(Of EventSakusei.XVLView.Dao.Vo.RHAC2270Vo)(iSql.ToString, aArg)


            'レコード件数が一件の場合は問題なし.
            If iFiles.Count <= 1 Then
                Return iFiles
            End If

            '複数件取得された場合、CATIA_FILE_KBNが大きいものを優先して１レコード返す(クエリでソート済みなので先頭レコード）。
            Dim iRet As New List(Of EventSakusei.XVLView.Dao.Vo.RHAC2270Vo)
            iRet.Add(iFiles(0))
            Return iRet
        End Function
#End Region

#Region "補用部品表検索の仕向地をHELP項目に設定"
        ''' <summary>
        ''' 補用部品表検索の仕向地を改行コードを追加して文字列で返す
        ''' </summary>
        ''' <remarks></remarks>
        Public Function SetShimukeDataString() As String
            Try
                Dim Shimuke As String = String.Empty

                Dim dtShimukeList As List(Of LabelValueVo) = GetLabelValues_KataShimukeHelp()

                For idx As Integer = 0 To dtShimukeList.Count - 1
                    '文字列の設定
                    Shimuke = Shimuke & dtShimukeList(idx).Label & "：" & dtShimukeList(idx).Value

                    '改行コード追加鑑定
                    If idx <> dtShimukeList.Count - 1 Then
                        Shimuke = Shimuke & vbCrLf
                    End If
                Next

                '文字列を戻す
                Return Shimuke
            Catch ex As Exception
                MessageBox.Show(ex.Message, "異常", MessageBoxButtons.OK, MessageBoxIcon.Error)
                '文字列を戻す
                Return String.Empty
            End Try
        End Function

#End Region

#Region "ブロック名称のコンボボックスを作成"

        ''' <summary>
        ''' ブロック名称のコンボボックスを作成
        ''' </summary>
        ''' <remarks></remarks>
        Public Sub SetBlockNameCombo(ByVal strUnitKbn As String, ByVal strUnitKbnChk As Boolean)
            Try
                'ブロック名称コンボボックスに値を追加
                Dim dtBlockName As DataTable = GetBlockNameData(strUnitKbn, strUnitKbnChk)
                Dim vos As New List(Of LabelValueVo)
                For idx As Integer = 0 To dtBlockName.Rows.Count - 1
                    Dim vo As New LabelValueVo
                    vo.Label = dtBlockName.Rows(idx).Item(1)
                    vo.Value = vo.Label
                    vos.Add(vo)
                Next
                Dim frm As Frm41KouseiBuhinSelector = _frmKouseiBuhinSelector
                FormUtil.BindLabelValuesToComboBox(frm.cmbBlockName, vos, True)
            Catch ex As Exception
                MessageBox.Show(ex.Message, "異常", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End Try
        End Sub

        ''' <summary>
        ''' E-BOM開発符号よりブロック名称を設定する。
        ''' </summary>
        ''' <returns>全データテーブル</returns>
        ''' <remarks></remarks>
        Private Function GetBlockNameData(ByVal unitKbn As String, ByVal unitKbnChk As Boolean) As DataTable

            Dim frm As Frm41KouseiBuhinSelector = _frmKouseiBuhinSelector

            Dim dtData As New DataTable
            Using db As New SqlAccess(g_kanrihyoIni(DB_KEY_mBOM))
                db.Open()
                db.Fill(DataSqlCommon.GetBlockNameSqlUnitChk(frm.cmbKaihatsuFugo.Text, unitKbn, unitKbnChk), dtData)
            End Using
            Return dtData
        End Function

#End Region

#Region "フォームから呼出し仕様情報一覧のコンボボックスを生成"

        ''' <summary>車型のコンボボックスを生成する。</summary>
        ''' <remarks></remarks>
        Public Sub setShiyouJyouhouListSyagata()

            Dim frm As Frm41KouseiBuhinSelector = _frmKouseiBuhinSelector
            SpreadUtil.BindCellTypeToColumn(frm.spdBuhinShiyou_Sheet1, _
                                            DispShiyouJyouhouList.TAG_SYAGATA.ToString, _
                                            NewSyagataCellType)
            'カウント
            Dim nCount As Integer = 0
            For i As Integer = 0 To NewSyagataCellType.Items.Length - 1
                nCount += 1
            Next
            'ITEM数が１なら画面に表示する。
            If StringUtil.Equals(nCount, 1) Then
                frm.spdBuhinShiyou_Sheet1.SetValue(spd_BuhinShiyou_startRow, _
                frm.spdBuhinShiyou_Sheet1.Columns(DispShiyouJyouhouList.TAG_SYAGATA.ToString).Index(), _
                NewSyagataCellType.Items(0).ToString())
            Else
                frm.spdBuhinShiyou_Sheet1.SetValue(spd_BuhinShiyou_startRow, _
                    frm.spdBuhinShiyou_Sheet1.Columns(DispShiyouJyouhouList.TAG_SYAGATA.ToString).Index(), "")
            End If

        End Sub

        ''' <summary>ｸﾞﾚｰﾄﾞのコンボボックスを生成する。</summary>
        ''' <remarks></remarks>
        Public Sub setShiyouJyouhouListGrade()

            Dim frm As Frm41KouseiBuhinSelector = _frmKouseiBuhinSelector
            SpreadUtil.BindCellTypeToColumn(frm.spdBuhinShiyou_Sheet1, _
                                            DispShiyouJyouhouList.TAG_GRADE.ToString, _
                                            NewGradeCellType)

            'カウント
            Dim nCount As Integer = 0
            For i As Integer = 0 To NewGradeCellType.Items.Length - 1
                nCount += 1
            Next
            'ITEM数が１なら画面に表示する。
            If StringUtil.Equals(nCount, 1) Then
                frm.spdBuhinShiyou_Sheet1.SetValue(spd_BuhinShiyou_startRow, _
                frm.spdBuhinShiyou_Sheet1.Columns(DispShiyouJyouhouList.TAG_GRADE.ToString).Index(), _
                NewGradeCellType.Items(0).ToString())
            Else
                frm.spdBuhinShiyou_Sheet1.SetValue(spd_BuhinShiyou_startRow, _
                    frm.spdBuhinShiyou_Sheet1.Columns(DispShiyouJyouhouList.TAG_GRADE.ToString).Index(), "")
            End If

        End Sub

        ''' <summary>仕向地・ﾊﾝﾄﾞﾙのコンボボックスを生成する。</summary>
        ''' <remarks></remarks>
        Public Sub setShiyouJyouhouListShimukechiHandle()

            Dim frm As Frm41KouseiBuhinSelector = _frmKouseiBuhinSelector
            SpreadUtil.BindCellTypeToColumn(frm.spdBuhinShiyou_Sheet1, _
                                            DispShiyouJyouhouList.TAG_SHIMUKECHI_HANDLE.ToString, _
                                            NewShimukechiHandleCellType)
            'カウント
            Dim nCount As Integer = 0
            For i As Integer = 0 To NewShimukechiHandleCellType.Items.Length - 1
                nCount += 1
            Next
            'ITEM数が１なら画面に表示する。
            If StringUtil.Equals(nCount, 1) Then
                frm.spdBuhinShiyou_Sheet1.SetValue(spd_BuhinShiyou_startRow, _
                frm.spdBuhinShiyou_Sheet1.Columns(DispShiyouJyouhouList.TAG_SHIMUKECHI_HANDLE.ToString).Index(), _
                NewShimukechiHandleCellType.Items(0).ToString())
            Else
                frm.spdBuhinShiyou_Sheet1.SetValue(spd_BuhinShiyou_startRow, _
                    frm.spdBuhinShiyou_Sheet1.Columns(DispShiyouJyouhouList.TAG_SHIMUKECHI_HANDLE.ToString).Index(), "")
            End If

        End Sub

        ''' <summary>E/G・排気量のコンボボックスを生成する。</summary>
        ''' <remarks></remarks>
        Public Sub setEgHaikiryouListShimukechiHandle()

            Dim frm As Frm41KouseiBuhinSelector = _frmKouseiBuhinSelector
            SpreadUtil.BindCellTypeToColumn(frm.spdBuhinShiyou_Sheet1, _
                                            DispShiyouJyouhouList.TAG_EG_HAIKIRYOU.ToString, _
                                            NewEgHaikiryouCellType)
            'カウント
            Dim nCount As Integer = 0
            For i As Integer = 0 To NewEgHaikiryouCellType.Items.Length - 1
                nCount += 1
            Next
            'ITEM数が１なら画面に表示する。
            If StringUtil.Equals(nCount, 1) Then
                frm.spdBuhinShiyou_Sheet1.SetValue(spd_BuhinShiyou_startRow, _
                frm.spdBuhinShiyou_Sheet1.Columns(DispShiyouJyouhouList.TAG_EG_HAIKIRYOU.ToString).Index(), _
                NewEgHaikiryouCellType.Items(0).ToString())
            Else
                frm.spdBuhinShiyou_Sheet1.SetValue(spd_BuhinShiyou_startRow, _
                    frm.spdBuhinShiyou_Sheet1.Columns(DispShiyouJyouhouList.TAG_EG_HAIKIRYOU.ToString).Index(), "")
            End If

        End Sub

        ''' <summary>E/G・形式のコンボボックスを生成する。</summary>
        ''' <remarks></remarks>
        Public Sub setEgKeishikiListShimukechiHandle()

            Dim frm As Frm41KouseiBuhinSelector = _frmKouseiBuhinSelector
            SpreadUtil.BindCellTypeToColumn(frm.spdBuhinShiyou_Sheet1, _
                                            DispShiyouJyouhouList.TAG_EG_KEISHIKI.ToString, _
                                            NewEgKeishikiCellType)
            'カウント
            Dim nCount As Integer = 0
            For i As Integer = 0 To NewEgKeishikiCellType.Items.Length - 1
                nCount += 1
            Next
            'ITEM数が１なら画面に表示する。
            If StringUtil.Equals(nCount, 1) Then
                frm.spdBuhinShiyou_Sheet1.SetValue(spd_BuhinShiyou_startRow, _
                frm.spdBuhinShiyou_Sheet1.Columns(DispShiyouJyouhouList.TAG_EG_KEISHIKI.ToString).Index(), _
                NewEgKeishikiCellType.Items(0).ToString())
            Else
                frm.spdBuhinShiyou_Sheet1.SetValue(spd_BuhinShiyou_startRow, _
                    frm.spdBuhinShiyou_Sheet1.Columns(DispShiyouJyouhouList.TAG_EG_KEISHIKI.ToString).Index(), "")
            End If

        End Sub

        ''' <summary>E/G・過給器のコンボボックスを生成する。</summary>
        ''' <remarks></remarks>
        Public Sub setEgKakyukiListShimukechiHandle()

            Dim frm As Frm41KouseiBuhinSelector = _frmKouseiBuhinSelector
            SpreadUtil.BindCellTypeToColumn(frm.spdBuhinShiyou_Sheet1, _
                                            DispShiyouJyouhouList.TAG_EG_KAKYUKI.ToString, _
                                            NewEgKakyukiCellType)
            'カウント
            Dim nCount As Integer = 0
            For i As Integer = 0 To NewEgKakyukiCellType.Items.Length - 1
                nCount += 1
            Next
            'ITEM数が１なら画面に表示する。
            If StringUtil.Equals(nCount, 1) Then
                frm.spdBuhinShiyou_Sheet1.SetValue(spd_BuhinShiyou_startRow, _
                frm.spdBuhinShiyou_Sheet1.Columns(DispShiyouJyouhouList.TAG_EG_KAKYUKI.ToString).Index(), _
                NewEgKakyukiCellType.Items(0).ToString())
            Else
                frm.spdBuhinShiyou_Sheet1.SetValue(spd_BuhinShiyou_startRow, _
                    frm.spdBuhinShiyou_Sheet1.Columns(DispShiyouJyouhouList.TAG_EG_KAKYUKI.ToString).Index(), "")
            End If

        End Sub

        ''' <summary>T/M・駆動方式のコンボボックスを生成する。</summary>
        ''' <remarks></remarks>
        Public Sub setTmKudouListShimukechiHandle()

            Dim frm As Frm41KouseiBuhinSelector = _frmKouseiBuhinSelector
            SpreadUtil.BindCellTypeToColumn(frm.spdBuhinShiyou_Sheet1, _
                                            DispShiyouJyouhouList.TAG_TM_KUDOU.ToString, _
                                            NewTmKudouCellType)
            'カウント
            Dim nCount As Integer = 0
            For i As Integer = 0 To NewTmKudouCellType.Items.Length - 1
                nCount += 1
            Next
            'ITEM数が１なら画面に表示する。
            If StringUtil.Equals(nCount, 1) Then
                frm.spdBuhinShiyou_Sheet1.SetValue(spd_BuhinShiyou_startRow, _
                frm.spdBuhinShiyou_Sheet1.Columns(DispShiyouJyouhouList.TAG_TM_KUDOU.ToString).Index(), _
                NewTmKudouCellType.Items(0).ToString())
            Else
                frm.spdBuhinShiyou_Sheet1.SetValue(spd_BuhinShiyou_startRow, _
                    frm.spdBuhinShiyou_Sheet1.Columns(DispShiyouJyouhouList.TAG_TM_KUDOU.ToString).Index(), "")
            End If

        End Sub

        ''' <summary>T/M・変速機のコンボボックスを生成する。</summary>
        ''' <remarks></remarks>
        Public Sub setTmHensokukiListShimukechiHandle()

            Dim frm As Frm41KouseiBuhinSelector = _frmKouseiBuhinSelector
            SpreadUtil.BindCellTypeToColumn(frm.spdBuhinShiyou_Sheet1, _
                                            DispShiyouJyouhouList.TAG_TM_HENSOKUKI.ToString, _
                                            NewTmHensokukiCellType)
            'カウント
            Dim nCount As Integer = 0
            For i As Integer = 0 To NewTmHensokukiCellType.Items.Length - 1
                nCount += 1
            Next
            'ITEM数が１なら画面に表示する。
            If StringUtil.Equals(nCount, 1) Then
                frm.spdBuhinShiyou_Sheet1.SetValue(spd_BuhinShiyou_startRow, _
                frm.spdBuhinShiyou_Sheet1.Columns(DispShiyouJyouhouList.TAG_TM_HENSOKUKI.ToString).Index(), _
                NewTmHensokukiCellType.Items(0).ToString())
            Else
                frm.spdBuhinShiyou_Sheet1.SetValue(spd_BuhinShiyou_startRow, _
                    frm.spdBuhinShiyou_Sheet1.Columns(DispShiyouJyouhouList.TAG_TM_HENSOKUKI.ToString).Index(), "")
            End If

        End Sub

        ''' <summary>７桁型式のコンボボックスを生成する。</summary>
        ''' <remarks></remarks>
        Public Sub setKatashiki7ListShimukechiHandle()

            Dim frm As Frm41KouseiBuhinSelector = _frmKouseiBuhinSelector
            SpreadUtil.BindCellTypeToColumn(frm.spdBuhinShiyou_Sheet1, _
                                            DispShiyouJyouhouList.TAG_KATA_7_KETA_KATASHIKI.ToString, _
                                            NewKatashiki7CellType)
            'カウント
            Dim nCount As Integer = 0
            For i As Integer = 0 To NewKatashiki7CellType.Items.Length - 1
                nCount += 1
            Next
            'ITEM数が１なら画面に表示する。
            If StringUtil.Equals(nCount, 1) Then
                frm.spdBuhinShiyou_Sheet1.SetValue(spd_BuhinShiyou_startRow, _
                frm.spdBuhinShiyou_Sheet1.Columns(DispShiyouJyouhouList.TAG_KATA_7_KETA_KATASHIKI.ToString).Index(), _
                NewKatashiki7CellType.Items(0).ToString())
            Else
                frm.spdBuhinShiyou_Sheet1.SetValue(spd_BuhinShiyou_startRow, _
                    frm.spdBuhinShiyou_Sheet1.Columns(DispShiyouJyouhouList.TAG_KATA_7_KETA_KATASHIKI.ToString).Index(), "")
            End If

        End Sub

        ''' <summary>仕向けコードのコンボボックスを生成する。</summary>
        ''' <remarks></remarks>
        Public Sub setKataShimukeListShimukechiHandle()

            Dim frm As Frm41KouseiBuhinSelector = _frmKouseiBuhinSelector
            SpreadUtil.BindCellTypeToColumn(frm.spdBuhinShiyou_Sheet1, _
                                            DispShiyouJyouhouList.TAG_KATA_SHIMUKE.ToString, _
                                            NewKataShimukeCellType)
            'カウント
            Dim nCount As Integer = 0
            For i As Integer = 0 To NewKataShimukeCellType.Items.Length - 1
                nCount += 1
            Next
            'ITEM数が１なら画面に表示する。
            If StringUtil.Equals(nCount, 1) Then
                frm.spdBuhinShiyou_Sheet1.SetValue(spd_BuhinShiyou_startRow, _
                frm.spdBuhinShiyou_Sheet1.Columns(DispShiyouJyouhouList.TAG_KATA_SHIMUKE.ToString).Index(), _
                NewKataShimukeCellType.Items(0).ToString())
            Else
                frm.spdBuhinShiyou_Sheet1.SetValue(spd_BuhinShiyou_startRow, _
                    frm.spdBuhinShiyou_Sheet1.Columns(DispShiyouJyouhouList.TAG_KATA_SHIMUKE.ToString).Index(), "")
            End If

        End Sub

        ''' <summary>ＯＰコードのコンボボックスを生成する。</summary>
        ''' <remarks></remarks>
        Public Sub setKataOpListShimukechiHandle()

            Dim frm As Frm41KouseiBuhinSelector = _frmKouseiBuhinSelector
            SpreadUtil.BindCellTypeToColumn(frm.spdBuhinShiyou_Sheet1, _
                                            DispShiyouJyouhouList.TAG_KATA_OP.ToString, _
                                            NewKataOpCellType)
            'カウント
            Dim nCount As Integer = 0
            For i As Integer = 0 To NewKataOpCellType.Items.Length - 1
                nCount += 1
            Next

        End Sub

        ''' <summary>ＯＰ列の値をセットする。</summary>
        ''' <remarks></remarks>
        Public Sub setOpRetsuHandle()

            'ＯＰ項目列を作成する。
            setOpKoumokuRetuHandle()

        End Sub

#End Region

#Region "仕様情報のコンボボックスを作成"

        ''' <summary>仕様情報№のコンボボックスの表示値を返す</summary>
        ''' <returns>表示値</returns>
        ''' <remarks></remarks>
        Public Function GetLabelValues_ShiyouJyouhouNo() As List(Of LabelValueVo)
            If EzUtil.ContainsNull(_frmKouseiBuhinSelector.cmbKaihatsuFugo.Text) Then
                Return EMPTY_LIST
            End If
            Dim ShiyouJyouhouDao As ShiyouJyouhouDao = New ShiyouJyouhouDaoImpl

            Return ShiyouJyouhouDao.GetByShiyouJyouhouNoLabelValues(_frmKouseiBuhinSelector.cmbKaihatsuFugo.Text)

        End Function
        ''' <summary>車型のコンボボックスの表示値を返す</summary>
        ''' <returns>表示値</returns>
        ''' <remarks></remarks>
        Public Function GetLabelValues_Syagata() As List(Of LabelValueVo)
            Dim frm As Frm41KouseiBuhinSelector = _frmKouseiBuhinSelector

            If EzUtil.ContainsNull(frm.cmbKaihatsuFugo.Text) Then
                Return EMPTY_LIST
            End If

            Dim ShiyouJyouhouDao As ShiyouJyouhouDao = New ShiyouJyouhouDaoImpl
            Return ShiyouJyouhouDao.GetBySyagataLabelValues(frm.cmbKaihatsuFugo.Text, _
                                                            frm.spdBuhinShiyou_Sheet1.GetValue(spd_BuhinShiyou_startRow, _
                                                            frm.spdBuhinShiyou_Sheet1.Columns(DispShiyouJyouhouList.TAG_SHIYOU_JYOUHOU_NO.ToString).Index()), _
                                                            frm.spdBuhinShiyou_Sheet1.GetValue(spd_BuhinShiyou_startRow, _
                                                            frm.spdBuhinShiyou_Sheet1.Columns(DispShiyouJyouhouList.TAG_SYAGATA.ToString).Index()), _
                                                            frm.spdBuhinShiyou_Sheet1.GetValue(spd_BuhinShiyou_startRow, _
                                                            frm.spdBuhinShiyou_Sheet1.Columns(DispShiyouJyouhouList.TAG_GRADE.ToString).Index()), _
                                                            frm.spdBuhinShiyou_Sheet1.GetValue(spd_BuhinShiyou_startRow, _
                                                            frm.spdBuhinShiyou_Sheet1.Columns(DispShiyouJyouhouList.TAG_SHIMUKECHI_HANDLE.ToString).Index()), _
                                                            frm.spdBuhinShiyou_Sheet1.GetValue(spd_BuhinShiyou_startRow, _
                                                            frm.spdBuhinShiyou_Sheet1.Columns(DispShiyouJyouhouList.TAG_EG_HAIKIRYOU.ToString).Index()), _
                                                            frm.spdBuhinShiyou_Sheet1.GetValue(spd_BuhinShiyou_startRow, _
                                                            frm.spdBuhinShiyou_Sheet1.Columns(DispShiyouJyouhouList.TAG_EG_KEISHIKI.ToString).Index()), _
                                                            frm.spdBuhinShiyou_Sheet1.GetValue(spd_BuhinShiyou_startRow, _
                                                            frm.spdBuhinShiyou_Sheet1.Columns(DispShiyouJyouhouList.TAG_EG_KAKYUKI.ToString).Index()), _
                                                            frm.spdBuhinShiyou_Sheet1.GetValue(spd_BuhinShiyou_startRow, _
                                                            frm.spdBuhinShiyou_Sheet1.Columns(DispShiyouJyouhouList.TAG_TM_KUDOU.ToString).Index()), _
                                                            frm.spdBuhinShiyou_Sheet1.GetValue(spd_BuhinShiyou_startRow, _
                                                            frm.spdBuhinShiyou_Sheet1.Columns(DispShiyouJyouhouList.TAG_TM_HENSOKUKI.ToString).Index()), _
                                                            frm.spdBuhinShiyou_Sheet1.GetValue(spd_BuhinShiyou_startRow, _
                                                            frm.spdBuhinShiyou_Sheet1.Columns(DispShiyouJyouhouList.TAG_KATA_7_KETA_KATASHIKI.ToString).Index()), _
                                                            frm.spdBuhinShiyou_Sheet1.GetValue(spd_BuhinShiyou_startRow, _
                                                            frm.spdBuhinShiyou_Sheet1.Columns(DispShiyouJyouhouList.TAG_KATA_SHIMUKE.ToString).Index()), _
                                                            frm.spdBuhinShiyou_Sheet1.GetValue(spd_BuhinShiyou_startRow, _
                                                            frm.spdBuhinShiyou_Sheet1.Columns(DispShiyouJyouhouList.TAG_KATA_OP.ToString).Index()))

        End Function

        ''' <summary>ｸﾞﾚｰﾄﾞのコンボボックスの表示値を返す</summary>
        ''' <returns>表示値</returns>
        ''' <remarks></remarks>
        Public Function GetLabelValues_Grade() As List(Of LabelValueVo)
            Dim frm As Frm41KouseiBuhinSelector = _frmKouseiBuhinSelector

            If EzUtil.ContainsNull(frm.cmbKaihatsuFugo.Text) Then
                Return EMPTY_LIST
            End If

            Dim ShiyouJyouhouDao As ShiyouJyouhouDao = New ShiyouJyouhouDaoImpl
            Return ShiyouJyouhouDao.GetByGradeLabelValues(frm.cmbKaihatsuFugo.Text, _
                                                            frm.spdBuhinShiyou_Sheet1.GetValue(spd_BuhinShiyou_startRow, _
                                                            frm.spdBuhinShiyou_Sheet1.Columns(DispShiyouJyouhouList.TAG_SHIYOU_JYOUHOU_NO.ToString).Index()), _
                                                            frm.spdBuhinShiyou_Sheet1.GetValue(spd_BuhinShiyou_startRow, _
                                                            frm.spdBuhinShiyou_Sheet1.Columns(DispShiyouJyouhouList.TAG_SYAGATA.ToString).Index()), _
                                                            frm.spdBuhinShiyou_Sheet1.GetValue(spd_BuhinShiyou_startRow, _
                                                            frm.spdBuhinShiyou_Sheet1.Columns(DispShiyouJyouhouList.TAG_GRADE.ToString).Index()), _
                                                            frm.spdBuhinShiyou_Sheet1.GetValue(spd_BuhinShiyou_startRow, _
                                                            frm.spdBuhinShiyou_Sheet1.Columns(DispShiyouJyouhouList.TAG_SHIMUKECHI_HANDLE.ToString).Index()), _
                                                            frm.spdBuhinShiyou_Sheet1.GetValue(spd_BuhinShiyou_startRow, _
                                                            frm.spdBuhinShiyou_Sheet1.Columns(DispShiyouJyouhouList.TAG_EG_HAIKIRYOU.ToString).Index()), _
                                                            frm.spdBuhinShiyou_Sheet1.GetValue(spd_BuhinShiyou_startRow, _
                                                            frm.spdBuhinShiyou_Sheet1.Columns(DispShiyouJyouhouList.TAG_EG_KEISHIKI.ToString).Index()), _
                                                            frm.spdBuhinShiyou_Sheet1.GetValue(spd_BuhinShiyou_startRow, _
                                                            frm.spdBuhinShiyou_Sheet1.Columns(DispShiyouJyouhouList.TAG_EG_KAKYUKI.ToString).Index()), _
                                                            frm.spdBuhinShiyou_Sheet1.GetValue(spd_BuhinShiyou_startRow, _
                                                            frm.spdBuhinShiyou_Sheet1.Columns(DispShiyouJyouhouList.TAG_TM_KUDOU.ToString).Index()), _
                                                            frm.spdBuhinShiyou_Sheet1.GetValue(spd_BuhinShiyou_startRow, _
                                                            frm.spdBuhinShiyou_Sheet1.Columns(DispShiyouJyouhouList.TAG_TM_HENSOKUKI.ToString).Index()), _
                                                            frm.spdBuhinShiyou_Sheet1.GetValue(spd_BuhinShiyou_startRow, _
                                                            frm.spdBuhinShiyou_Sheet1.Columns(DispShiyouJyouhouList.TAG_KATA_7_KETA_KATASHIKI.ToString).Index()), _
                                                            frm.spdBuhinShiyou_Sheet1.GetValue(spd_BuhinShiyou_startRow, _
                                                            frm.spdBuhinShiyou_Sheet1.Columns(DispShiyouJyouhouList.TAG_KATA_SHIMUKE.ToString).Index()), _
                                                            frm.spdBuhinShiyou_Sheet1.GetValue(spd_BuhinShiyou_startRow, _
                                                            frm.spdBuhinShiyou_Sheet1.Columns(DispShiyouJyouhouList.TAG_KATA_OP.ToString).Index()))

        End Function

        ''' <summary>仕向地・仕向けのコンボボックスの表示値を返す</summary>
        ''' <returns>表示値</returns>
        ''' <remarks></remarks>
        Public Function GetLabelValues_ShimukechiShimuke() As List(Of LabelValueVo)

            Dim frm As Frm41KouseiBuhinSelector = _frmKouseiBuhinSelector

            If EzUtil.ContainsNull(frm.cmbKaihatsuFugo.Text) Then
                Return EMPTY_LIST
            End If

            Dim ShiyouJyouhouDao As ShiyouJyouhouDao = New ShiyouJyouhouDaoImpl
            Return ShiyouJyouhouDao.GetByShimukechiShimukeLabelValues(frm.cmbKaihatsuFugo.Text, _
                                                            frm.spdBuhinShiyou_Sheet1.GetValue(spd_BuhinShiyou_startRow, _
                                                            frm.spdBuhinShiyou_Sheet1.Columns(DispShiyouJyouhouList.TAG_SHIYOU_JYOUHOU_NO.ToString).Index()), _
                                                            frm.spdBuhinShiyou_Sheet1.GetValue(spd_BuhinShiyou_startRow, _
                                                            frm.spdBuhinShiyou_Sheet1.Columns(DispShiyouJyouhouList.TAG_SYAGATA.ToString).Index()), _
                                                            frm.spdBuhinShiyou_Sheet1.GetValue(spd_BuhinShiyou_startRow, _
                                                            frm.spdBuhinShiyou_Sheet1.Columns(DispShiyouJyouhouList.TAG_GRADE.ToString).Index()), _
                                                            frm.spdBuhinShiyou_Sheet1.GetValue(spd_BuhinShiyou_startRow, _
                                                            frm.spdBuhinShiyou_Sheet1.Columns(DispShiyouJyouhouList.TAG_SHIMUKECHI_HANDLE.ToString).Index()), _
                                                            frm.spdBuhinShiyou_Sheet1.GetValue(spd_BuhinShiyou_startRow, _
                                                            frm.spdBuhinShiyou_Sheet1.Columns(DispShiyouJyouhouList.TAG_EG_HAIKIRYOU.ToString).Index()), _
                                                            frm.spdBuhinShiyou_Sheet1.GetValue(spd_BuhinShiyou_startRow, _
                                                            frm.spdBuhinShiyou_Sheet1.Columns(DispShiyouJyouhouList.TAG_EG_KEISHIKI.ToString).Index()), _
                                                            frm.spdBuhinShiyou_Sheet1.GetValue(spd_BuhinShiyou_startRow, _
                                                            frm.spdBuhinShiyou_Sheet1.Columns(DispShiyouJyouhouList.TAG_EG_KAKYUKI.ToString).Index()), _
                                                            frm.spdBuhinShiyou_Sheet1.GetValue(spd_BuhinShiyou_startRow, _
                                                            frm.spdBuhinShiyou_Sheet1.Columns(DispShiyouJyouhouList.TAG_TM_KUDOU.ToString).Index()), _
                                                            frm.spdBuhinShiyou_Sheet1.GetValue(spd_BuhinShiyou_startRow, _
                                                            frm.spdBuhinShiyou_Sheet1.Columns(DispShiyouJyouhouList.TAG_TM_HENSOKUKI.ToString).Index()), _
                                                            frm.spdBuhinShiyou_Sheet1.GetValue(spd_BuhinShiyou_startRow, _
                                                            frm.spdBuhinShiyou_Sheet1.Columns(DispShiyouJyouhouList.TAG_KATA_7_KETA_KATASHIKI.ToString).Index()), _
                                                            frm.spdBuhinShiyou_Sheet1.GetValue(spd_BuhinShiyou_startRow, _
                                                            frm.spdBuhinShiyou_Sheet1.Columns(DispShiyouJyouhouList.TAG_KATA_SHIMUKE.ToString).Index()), _
                                                            frm.spdBuhinShiyou_Sheet1.GetValue(spd_BuhinShiyou_startRow, _
                                                            frm.spdBuhinShiyou_Sheet1.Columns(DispShiyouJyouhouList.TAG_KATA_OP.ToString).Index()))

        End Function

        ''' <summary>仕向地・ﾊﾝﾄﾞﾙのコンボボックスの表示値を返す</summary>
        ''' <returns>表示値</returns>
        ''' <remarks></remarks>
        Public Function GetLabelValues_ShimukechiHandle() As List(Of LabelValueVo)

            Dim frm As Frm41KouseiBuhinSelector = _frmKouseiBuhinSelector

            If EzUtil.ContainsNull(frm.cmbKaihatsuFugo.Text) Then
                Return EMPTY_LIST
            End If

            Dim ShiyouJyouhouDao As ShiyouJyouhouDao = New ShiyouJyouhouDaoImpl
            Return ShiyouJyouhouDao.GetByShimukechiHandleLabelValues(frm.cmbKaihatsuFugo.Text, _
                                                            frm.spdBuhinShiyou_Sheet1.GetValue(spd_BuhinShiyou_startRow, _
                                                            frm.spdBuhinShiyou_Sheet1.Columns(DispShiyouJyouhouList.TAG_SHIYOU_JYOUHOU_NO.ToString).Index()), _
                                                            frm.spdBuhinShiyou_Sheet1.GetValue(spd_BuhinShiyou_startRow, _
                                                            frm.spdBuhinShiyou_Sheet1.Columns(DispShiyouJyouhouList.TAG_SYAGATA.ToString).Index()), _
                                                            frm.spdBuhinShiyou_Sheet1.GetValue(spd_BuhinShiyou_startRow, _
                                                            frm.spdBuhinShiyou_Sheet1.Columns(DispShiyouJyouhouList.TAG_GRADE.ToString).Index()), _
                                                            frm.spdBuhinShiyou_Sheet1.GetValue(spd_BuhinShiyou_startRow, _
                                                            frm.spdBuhinShiyou_Sheet1.Columns(DispShiyouJyouhouList.TAG_SHIMUKECHI_HANDLE.ToString).Index()), _
                                                            frm.spdBuhinShiyou_Sheet1.GetValue(spd_BuhinShiyou_startRow, _
                                                            frm.spdBuhinShiyou_Sheet1.Columns(DispShiyouJyouhouList.TAG_EG_HAIKIRYOU.ToString).Index()), _
                                                            frm.spdBuhinShiyou_Sheet1.GetValue(spd_BuhinShiyou_startRow, _
                                                            frm.spdBuhinShiyou_Sheet1.Columns(DispShiyouJyouhouList.TAG_EG_KEISHIKI.ToString).Index()), _
                                                            frm.spdBuhinShiyou_Sheet1.GetValue(spd_BuhinShiyou_startRow, _
                                                            frm.spdBuhinShiyou_Sheet1.Columns(DispShiyouJyouhouList.TAG_EG_KAKYUKI.ToString).Index()), _
                                                            frm.spdBuhinShiyou_Sheet1.GetValue(spd_BuhinShiyou_startRow, _
                                                            frm.spdBuhinShiyou_Sheet1.Columns(DispShiyouJyouhouList.TAG_TM_KUDOU.ToString).Index()), _
                                                            frm.spdBuhinShiyou_Sheet1.GetValue(spd_BuhinShiyou_startRow, _
                                                            frm.spdBuhinShiyou_Sheet1.Columns(DispShiyouJyouhouList.TAG_TM_HENSOKUKI.ToString).Index()), _
                                                            frm.spdBuhinShiyou_Sheet1.GetValue(spd_BuhinShiyou_startRow, _
                                                            frm.spdBuhinShiyou_Sheet1.Columns(DispShiyouJyouhouList.TAG_KATA_7_KETA_KATASHIKI.ToString).Index()), _
                                                            frm.spdBuhinShiyou_Sheet1.GetValue(spd_BuhinShiyou_startRow, _
                                                            frm.spdBuhinShiyou_Sheet1.Columns(DispShiyouJyouhouList.TAG_KATA_SHIMUKE.ToString).Index()), _
                                                            frm.spdBuhinShiyou_Sheet1.GetValue(spd_BuhinShiyou_startRow, _
                                                            frm.spdBuhinShiyou_Sheet1.Columns(DispShiyouJyouhouList.TAG_KATA_OP.ToString).Index()))

        End Function

        ''' <summary>E/G・排気量のコンボボックスの表示値を返す</summary>
        ''' <returns>表示値</returns>
        ''' <remarks></remarks>
        Public Function GetLabelValues_EgHaikiryou() As List(Of LabelValueVo)

            Dim frm As Frm41KouseiBuhinSelector = _frmKouseiBuhinSelector

            If EzUtil.ContainsNull(frm.cmbKaihatsuFugo.Text) Then
                Return EMPTY_LIST
            End If

            Dim ShiyouJyouhouDao As ShiyouJyouhouDao = New ShiyouJyouhouDaoImpl
            Return ShiyouJyouhouDao.GetByEgHaikiryouLabelValues(frm.cmbKaihatsuFugo.Text, _
                                                            frm.spdBuhinShiyou_Sheet1.GetValue(spd_BuhinShiyou_startRow, _
                                                            frm.spdBuhinShiyou_Sheet1.Columns(DispShiyouJyouhouList.TAG_SHIYOU_JYOUHOU_NO.ToString).Index()), _
                                                            frm.spdBuhinShiyou_Sheet1.GetValue(spd_BuhinShiyou_startRow, _
                                                            frm.spdBuhinShiyou_Sheet1.Columns(DispShiyouJyouhouList.TAG_SYAGATA.ToString).Index()), _
                                                            frm.spdBuhinShiyou_Sheet1.GetValue(spd_BuhinShiyou_startRow, _
                                                            frm.spdBuhinShiyou_Sheet1.Columns(DispShiyouJyouhouList.TAG_GRADE.ToString).Index()), _
                                                            frm.spdBuhinShiyou_Sheet1.GetValue(spd_BuhinShiyou_startRow, _
                                                            frm.spdBuhinShiyou_Sheet1.Columns(DispShiyouJyouhouList.TAG_SHIMUKECHI_HANDLE.ToString).Index()), _
                                                            frm.spdBuhinShiyou_Sheet1.GetValue(spd_BuhinShiyou_startRow, _
                                                            frm.spdBuhinShiyou_Sheet1.Columns(DispShiyouJyouhouList.TAG_EG_HAIKIRYOU.ToString).Index()), _
                                                            frm.spdBuhinShiyou_Sheet1.GetValue(spd_BuhinShiyou_startRow, _
                                                            frm.spdBuhinShiyou_Sheet1.Columns(DispShiyouJyouhouList.TAG_EG_KEISHIKI.ToString).Index()), _
                                                            frm.spdBuhinShiyou_Sheet1.GetValue(spd_BuhinShiyou_startRow, _
                                                            frm.spdBuhinShiyou_Sheet1.Columns(DispShiyouJyouhouList.TAG_EG_KAKYUKI.ToString).Index()), _
                                                            frm.spdBuhinShiyou_Sheet1.GetValue(spd_BuhinShiyou_startRow, _
                                                            frm.spdBuhinShiyou_Sheet1.Columns(DispShiyouJyouhouList.TAG_TM_KUDOU.ToString).Index()), _
                                                            frm.spdBuhinShiyou_Sheet1.GetValue(spd_BuhinShiyou_startRow, _
                                                            frm.spdBuhinShiyou_Sheet1.Columns(DispShiyouJyouhouList.TAG_TM_HENSOKUKI.ToString).Index()), _
                                                            frm.spdBuhinShiyou_Sheet1.GetValue(spd_BuhinShiyou_startRow, _
                                                            frm.spdBuhinShiyou_Sheet1.Columns(DispShiyouJyouhouList.TAG_KATA_7_KETA_KATASHIKI.ToString).Index()), _
                                                            frm.spdBuhinShiyou_Sheet1.GetValue(spd_BuhinShiyou_startRow, _
                                                            frm.spdBuhinShiyou_Sheet1.Columns(DispShiyouJyouhouList.TAG_KATA_SHIMUKE.ToString).Index()), _
                                                            frm.spdBuhinShiyou_Sheet1.GetValue(spd_BuhinShiyou_startRow, _
                                                            frm.spdBuhinShiyou_Sheet1.Columns(DispShiyouJyouhouList.TAG_KATA_OP.ToString).Index()))

        End Function

        ''' <summary>E/G・形式のコンボボックスの表示値を返す</summary>
        ''' <returns>表示値</returns>
        ''' <remarks></remarks>
        Public Function GetLabelValues_EgKeishiki() As List(Of LabelValueVo)

            Dim frm As Frm41KouseiBuhinSelector = _frmKouseiBuhinSelector

            If EzUtil.ContainsNull(frm.cmbKaihatsuFugo.Text) Then
                Return EMPTY_LIST
            End If

            Dim ShiyouJyouhouDao As ShiyouJyouhouDao = New ShiyouJyouhouDaoImpl
            Return ShiyouJyouhouDao.GetByEgKeishikiLabelValues(frm.cmbKaihatsuFugo.Text, _
                                                            frm.spdBuhinShiyou_Sheet1.GetValue(spd_BuhinShiyou_startRow, _
                                                            frm.spdBuhinShiyou_Sheet1.Columns(DispShiyouJyouhouList.TAG_SHIYOU_JYOUHOU_NO.ToString).Index()), _
                                                            frm.spdBuhinShiyou_Sheet1.GetValue(spd_BuhinShiyou_startRow, _
                                                            frm.spdBuhinShiyou_Sheet1.Columns(DispShiyouJyouhouList.TAG_SYAGATA.ToString).Index()), _
                                                            frm.spdBuhinShiyou_Sheet1.GetValue(spd_BuhinShiyou_startRow, _
                                                            frm.spdBuhinShiyou_Sheet1.Columns(DispShiyouJyouhouList.TAG_GRADE.ToString).Index()), _
                                                            frm.spdBuhinShiyou_Sheet1.GetValue(spd_BuhinShiyou_startRow, _
                                                            frm.spdBuhinShiyou_Sheet1.Columns(DispShiyouJyouhouList.TAG_SHIMUKECHI_HANDLE.ToString).Index()), _
                                                            frm.spdBuhinShiyou_Sheet1.GetValue(spd_BuhinShiyou_startRow, _
                                                            frm.spdBuhinShiyou_Sheet1.Columns(DispShiyouJyouhouList.TAG_EG_HAIKIRYOU.ToString).Index()), _
                                                            frm.spdBuhinShiyou_Sheet1.GetValue(spd_BuhinShiyou_startRow, _
                                                            frm.spdBuhinShiyou_Sheet1.Columns(DispShiyouJyouhouList.TAG_EG_KEISHIKI.ToString).Index()), _
                                                            frm.spdBuhinShiyou_Sheet1.GetValue(spd_BuhinShiyou_startRow, _
                                                            frm.spdBuhinShiyou_Sheet1.Columns(DispShiyouJyouhouList.TAG_EG_KAKYUKI.ToString).Index()), _
                                                            frm.spdBuhinShiyou_Sheet1.GetValue(spd_BuhinShiyou_startRow, _
                                                            frm.spdBuhinShiyou_Sheet1.Columns(DispShiyouJyouhouList.TAG_TM_KUDOU.ToString).Index()), _
                                                            frm.spdBuhinShiyou_Sheet1.GetValue(spd_BuhinShiyou_startRow, _
                                                            frm.spdBuhinShiyou_Sheet1.Columns(DispShiyouJyouhouList.TAG_TM_HENSOKUKI.ToString).Index()), _
                                                            frm.spdBuhinShiyou_Sheet1.GetValue(spd_BuhinShiyou_startRow, _
                                                            frm.spdBuhinShiyou_Sheet1.Columns(DispShiyouJyouhouList.TAG_KATA_7_KETA_KATASHIKI.ToString).Index()), _
                                                            frm.spdBuhinShiyou_Sheet1.GetValue(spd_BuhinShiyou_startRow, _
                                                            frm.spdBuhinShiyou_Sheet1.Columns(DispShiyouJyouhouList.TAG_KATA_SHIMUKE.ToString).Index()), _
                                                            frm.spdBuhinShiyou_Sheet1.GetValue(spd_BuhinShiyou_startRow, _
                                                            frm.spdBuhinShiyou_Sheet1.Columns(DispShiyouJyouhouList.TAG_KATA_OP.ToString).Index()))

        End Function

        ''' <summary>E/G・過給器のコンボボックスの表示値を返す</summary>
        ''' <returns>表示値</returns>
        ''' <remarks></remarks>
        Public Function GetLabelValues_EgKakyuki() As List(Of LabelValueVo)

            Dim frm As Frm41KouseiBuhinSelector = _frmKouseiBuhinSelector

            If EzUtil.ContainsNull(frm.cmbKaihatsuFugo.Text) Then
                Return EMPTY_LIST
            End If

            Dim ShiyouJyouhouDao As ShiyouJyouhouDao = New ShiyouJyouhouDaoImpl
            Return ShiyouJyouhouDao.GetByEgKakyukiLabelValues(frm.cmbKaihatsuFugo.Text, _
                                                            frm.spdBuhinShiyou_Sheet1.GetValue(spd_BuhinShiyou_startRow, _
                                                            frm.spdBuhinShiyou_Sheet1.Columns(DispShiyouJyouhouList.TAG_SHIYOU_JYOUHOU_NO.ToString).Index()), _
                                                            frm.spdBuhinShiyou_Sheet1.GetValue(spd_BuhinShiyou_startRow, _
                                                            frm.spdBuhinShiyou_Sheet1.Columns(DispShiyouJyouhouList.TAG_SYAGATA.ToString).Index()), _
                                                            frm.spdBuhinShiyou_Sheet1.GetValue(spd_BuhinShiyou_startRow, _
                                                            frm.spdBuhinShiyou_Sheet1.Columns(DispShiyouJyouhouList.TAG_GRADE.ToString).Index()), _
                                                            frm.spdBuhinShiyou_Sheet1.GetValue(spd_BuhinShiyou_startRow, _
                                                            frm.spdBuhinShiyou_Sheet1.Columns(DispShiyouJyouhouList.TAG_SHIMUKECHI_HANDLE.ToString).Index()), _
                                                            frm.spdBuhinShiyou_Sheet1.GetValue(spd_BuhinShiyou_startRow, _
                                                            frm.spdBuhinShiyou_Sheet1.Columns(DispShiyouJyouhouList.TAG_EG_HAIKIRYOU.ToString).Index()), _
                                                            frm.spdBuhinShiyou_Sheet1.GetValue(spd_BuhinShiyou_startRow, _
                                                            frm.spdBuhinShiyou_Sheet1.Columns(DispShiyouJyouhouList.TAG_EG_KEISHIKI.ToString).Index()), _
                                                            frm.spdBuhinShiyou_Sheet1.GetValue(spd_BuhinShiyou_startRow, _
                                                            frm.spdBuhinShiyou_Sheet1.Columns(DispShiyouJyouhouList.TAG_EG_KAKYUKI.ToString).Index()), _
                                                            frm.spdBuhinShiyou_Sheet1.GetValue(spd_BuhinShiyou_startRow, _
                                                            frm.spdBuhinShiyou_Sheet1.Columns(DispShiyouJyouhouList.TAG_TM_KUDOU.ToString).Index()), _
                                                            frm.spdBuhinShiyou_Sheet1.GetValue(spd_BuhinShiyou_startRow, _
                                                            frm.spdBuhinShiyou_Sheet1.Columns(DispShiyouJyouhouList.TAG_TM_HENSOKUKI.ToString).Index()), _
                                                            frm.spdBuhinShiyou_Sheet1.GetValue(spd_BuhinShiyou_startRow, _
                                                            frm.spdBuhinShiyou_Sheet1.Columns(DispShiyouJyouhouList.TAG_KATA_7_KETA_KATASHIKI.ToString).Index()), _
                                                            frm.spdBuhinShiyou_Sheet1.GetValue(spd_BuhinShiyou_startRow, _
                                                            frm.spdBuhinShiyou_Sheet1.Columns(DispShiyouJyouhouList.TAG_KATA_SHIMUKE.ToString).Index()), _
                                                            frm.spdBuhinShiyou_Sheet1.GetValue(spd_BuhinShiyou_startRow, _
                                                            frm.spdBuhinShiyou_Sheet1.Columns(DispShiyouJyouhouList.TAG_KATA_OP.ToString).Index()))

        End Function

        ''' <summary>T/M・駆動方式のコンボボックスの表示値を返す</summary>
        ''' <returns>表示値</returns>
        ''' <remarks></remarks>
        Public Function GetLabelValues_TmKudou() As List(Of LabelValueVo)

            Dim frm As Frm41KouseiBuhinSelector = _frmKouseiBuhinSelector

            If EzUtil.ContainsNull(frm.cmbKaihatsuFugo.Text) Then
                Return EMPTY_LIST
            End If

            Dim ShiyouJyouhouDao As ShiyouJyouhouDao = New ShiyouJyouhouDaoImpl
            Return ShiyouJyouhouDao.GetByTmKudouLabelValues(frm.cmbKaihatsuFugo.Text, _
                                                            frm.spdBuhinShiyou_Sheet1.GetValue(spd_BuhinShiyou_startRow, _
                                                            frm.spdBuhinShiyou_Sheet1.Columns(DispShiyouJyouhouList.TAG_SHIYOU_JYOUHOU_NO.ToString).Index()), _
                                                            frm.spdBuhinShiyou_Sheet1.GetValue(spd_BuhinShiyou_startRow, _
                                                            frm.spdBuhinShiyou_Sheet1.Columns(DispShiyouJyouhouList.TAG_SYAGATA.ToString).Index()), _
                                                            frm.spdBuhinShiyou_Sheet1.GetValue(spd_BuhinShiyou_startRow, _
                                                            frm.spdBuhinShiyou_Sheet1.Columns(DispShiyouJyouhouList.TAG_GRADE.ToString).Index()), _
                                                            frm.spdBuhinShiyou_Sheet1.GetValue(spd_BuhinShiyou_startRow, _
                                                            frm.spdBuhinShiyou_Sheet1.Columns(DispShiyouJyouhouList.TAG_SHIMUKECHI_HANDLE.ToString).Index()), _
                                                            frm.spdBuhinShiyou_Sheet1.GetValue(spd_BuhinShiyou_startRow, _
                                                            frm.spdBuhinShiyou_Sheet1.Columns(DispShiyouJyouhouList.TAG_EG_HAIKIRYOU.ToString).Index()), _
                                                            frm.spdBuhinShiyou_Sheet1.GetValue(spd_BuhinShiyou_startRow, _
                                                            frm.spdBuhinShiyou_Sheet1.Columns(DispShiyouJyouhouList.TAG_EG_KEISHIKI.ToString).Index()), _
                                                            frm.spdBuhinShiyou_Sheet1.GetValue(spd_BuhinShiyou_startRow, _
                                                            frm.spdBuhinShiyou_Sheet1.Columns(DispShiyouJyouhouList.TAG_EG_KAKYUKI.ToString).Index()), _
                                                            frm.spdBuhinShiyou_Sheet1.GetValue(spd_BuhinShiyou_startRow, _
                                                            frm.spdBuhinShiyou_Sheet1.Columns(DispShiyouJyouhouList.TAG_TM_KUDOU.ToString).Index()), _
                                                            frm.spdBuhinShiyou_Sheet1.GetValue(spd_BuhinShiyou_startRow, _
                                                            frm.spdBuhinShiyou_Sheet1.Columns(DispShiyouJyouhouList.TAG_TM_HENSOKUKI.ToString).Index()), _
                                                            frm.spdBuhinShiyou_Sheet1.GetValue(spd_BuhinShiyou_startRow, _
                                                            frm.spdBuhinShiyou_Sheet1.Columns(DispShiyouJyouhouList.TAG_KATA_7_KETA_KATASHIKI.ToString).Index()), _
                                                            frm.spdBuhinShiyou_Sheet1.GetValue(spd_BuhinShiyou_startRow, _
                                                            frm.spdBuhinShiyou_Sheet1.Columns(DispShiyouJyouhouList.TAG_KATA_SHIMUKE.ToString).Index()), _
                                                            frm.spdBuhinShiyou_Sheet1.GetValue(spd_BuhinShiyou_startRow, _
                                                            frm.spdBuhinShiyou_Sheet1.Columns(DispShiyouJyouhouList.TAG_KATA_OP.ToString).Index()))

        End Function

        ''' <summary>T/M・変速機のコンボボックスの表示値を返す</summary>
        ''' <returns>表示値</returns>
        ''' <remarks></remarks>
        Public Function GetLabelValues_TmHensokuki() As List(Of LabelValueVo)

            Dim frm As Frm41KouseiBuhinSelector = _frmKouseiBuhinSelector

            If EzUtil.ContainsNull(frm.cmbKaihatsuFugo.Text) Then
                Return EMPTY_LIST
            End If

            Dim ShiyouJyouhouDao As ShiyouJyouhouDao = New ShiyouJyouhouDaoImpl
            Return ShiyouJyouhouDao.GetByTmHensokukiLabelValues(frm.cmbKaihatsuFugo.Text, _
                                                            frm.spdBuhinShiyou_Sheet1.GetValue(spd_BuhinShiyou_startRow, _
                                                            frm.spdBuhinShiyou_Sheet1.Columns(DispShiyouJyouhouList.TAG_SHIYOU_JYOUHOU_NO.ToString).Index()), _
                                                            frm.spdBuhinShiyou_Sheet1.GetValue(spd_BuhinShiyou_startRow, _
                                                            frm.spdBuhinShiyou_Sheet1.Columns(DispShiyouJyouhouList.TAG_SYAGATA.ToString).Index()), _
                                                            frm.spdBuhinShiyou_Sheet1.GetValue(spd_BuhinShiyou_startRow, _
                                                            frm.spdBuhinShiyou_Sheet1.Columns(DispShiyouJyouhouList.TAG_GRADE.ToString).Index()), _
                                                            frm.spdBuhinShiyou_Sheet1.GetValue(spd_BuhinShiyou_startRow, _
                                                            frm.spdBuhinShiyou_Sheet1.Columns(DispShiyouJyouhouList.TAG_SHIMUKECHI_HANDLE.ToString).Index()), _
                                                            frm.spdBuhinShiyou_Sheet1.GetValue(spd_BuhinShiyou_startRow, _
                                                            frm.spdBuhinShiyou_Sheet1.Columns(DispShiyouJyouhouList.TAG_EG_HAIKIRYOU.ToString).Index()), _
                                                            frm.spdBuhinShiyou_Sheet1.GetValue(spd_BuhinShiyou_startRow, _
                                                            frm.spdBuhinShiyou_Sheet1.Columns(DispShiyouJyouhouList.TAG_EG_KEISHIKI.ToString).Index()), _
                                                            frm.spdBuhinShiyou_Sheet1.GetValue(spd_BuhinShiyou_startRow, _
                                                            frm.spdBuhinShiyou_Sheet1.Columns(DispShiyouJyouhouList.TAG_EG_KAKYUKI.ToString).Index()), _
                                                            frm.spdBuhinShiyou_Sheet1.GetValue(spd_BuhinShiyou_startRow, _
                                                            frm.spdBuhinShiyou_Sheet1.Columns(DispShiyouJyouhouList.TAG_TM_KUDOU.ToString).Index()), _
                                                            frm.spdBuhinShiyou_Sheet1.GetValue(spd_BuhinShiyou_startRow, _
                                                            frm.spdBuhinShiyou_Sheet1.Columns(DispShiyouJyouhouList.TAG_TM_HENSOKUKI.ToString).Index()), _
                                                            frm.spdBuhinShiyou_Sheet1.GetValue(spd_BuhinShiyou_startRow, _
                                                            frm.spdBuhinShiyou_Sheet1.Columns(DispShiyouJyouhouList.TAG_KATA_7_KETA_KATASHIKI.ToString).Index()), _
                                                            frm.spdBuhinShiyou_Sheet1.GetValue(spd_BuhinShiyou_startRow, _
                                                            frm.spdBuhinShiyou_Sheet1.Columns(DispShiyouJyouhouList.TAG_KATA_SHIMUKE.ToString).Index()), _
                                                            frm.spdBuhinShiyou_Sheet1.GetValue(spd_BuhinShiyou_startRow, _
                                                            frm.spdBuhinShiyou_Sheet1.Columns(DispShiyouJyouhouList.TAG_KATA_OP.ToString).Index()))

        End Function

        ''' <summary>７桁型式のコンボボックスの表示値を返す</summary>
        ''' <returns>表示値</returns>
        ''' <remarks></remarks>
        Public Function GetLabelValues_Katashiki7() As List(Of LabelValueVo)

            Dim frm As Frm41KouseiBuhinSelector = _frmKouseiBuhinSelector

            If EzUtil.ContainsNull(frm.cmbKaihatsuFugo.Text) Then
                Return EMPTY_LIST
            End If

            Dim ShiyouJyouhouDao As ShiyouJyouhouDao = New ShiyouJyouhouDaoImpl
            Return ShiyouJyouhouDao.GetByKatashiki7LabelValues(frm.cmbKaihatsuFugo.Text, _
                                                            frm.spdBuhinShiyou_Sheet1.GetValue(spd_BuhinShiyou_startRow, _
                                                            frm.spdBuhinShiyou_Sheet1.Columns(DispShiyouJyouhouList.TAG_SHIYOU_JYOUHOU_NO.ToString).Index()), _
                                                            frm.spdBuhinShiyou_Sheet1.GetValue(spd_BuhinShiyou_startRow, _
                                                            frm.spdBuhinShiyou_Sheet1.Columns(DispShiyouJyouhouList.TAG_SYAGATA.ToString).Index()), _
                                                            frm.spdBuhinShiyou_Sheet1.GetValue(spd_BuhinShiyou_startRow, _
                                                            frm.spdBuhinShiyou_Sheet1.Columns(DispShiyouJyouhouList.TAG_GRADE.ToString).Index()), _
                                                            frm.spdBuhinShiyou_Sheet1.GetValue(spd_BuhinShiyou_startRow, _
                                                            frm.spdBuhinShiyou_Sheet1.Columns(DispShiyouJyouhouList.TAG_SHIMUKECHI_HANDLE.ToString).Index()), _
                                                            frm.spdBuhinShiyou_Sheet1.GetValue(spd_BuhinShiyou_startRow, _
                                                            frm.spdBuhinShiyou_Sheet1.Columns(DispShiyouJyouhouList.TAG_EG_HAIKIRYOU.ToString).Index()), _
                                                            frm.spdBuhinShiyou_Sheet1.GetValue(spd_BuhinShiyou_startRow, _
                                                            frm.spdBuhinShiyou_Sheet1.Columns(DispShiyouJyouhouList.TAG_EG_KEISHIKI.ToString).Index()), _
                                                            frm.spdBuhinShiyou_Sheet1.GetValue(spd_BuhinShiyou_startRow, _
                                                            frm.spdBuhinShiyou_Sheet1.Columns(DispShiyouJyouhouList.TAG_EG_KAKYUKI.ToString).Index()), _
                                                            frm.spdBuhinShiyou_Sheet1.GetValue(spd_BuhinShiyou_startRow, _
                                                            frm.spdBuhinShiyou_Sheet1.Columns(DispShiyouJyouhouList.TAG_TM_KUDOU.ToString).Index()), _
                                                            frm.spdBuhinShiyou_Sheet1.GetValue(spd_BuhinShiyou_startRow, _
                                                            frm.spdBuhinShiyou_Sheet1.Columns(DispShiyouJyouhouList.TAG_TM_HENSOKUKI.ToString).Index()), _
                                                            frm.spdBuhinShiyou_Sheet1.GetValue(spd_BuhinShiyou_startRow, _
                                                            frm.spdBuhinShiyou_Sheet1.Columns(DispShiyouJyouhouList.TAG_KATA_7_KETA_KATASHIKI.ToString).Index()), _
                                                            frm.spdBuhinShiyou_Sheet1.GetValue(spd_BuhinShiyou_startRow, _
                                                            frm.spdBuhinShiyou_Sheet1.Columns(DispShiyouJyouhouList.TAG_KATA_SHIMUKE.ToString).Index()), _
                                                            frm.spdBuhinShiyou_Sheet1.GetValue(spd_BuhinShiyou_startRow, _
                                                            frm.spdBuhinShiyou_Sheet1.Columns(DispShiyouJyouhouList.TAG_KATA_OP.ToString).Index()))

        End Function

        ''' <summary>仕向けコードのコンボボックスの表示値を返す</summary>
        ''' <returns>表示値</returns>
        ''' <remarks></remarks>
        Public Function GetLabelValues_KataShimuke() As List(Of LabelValueVo)

            Dim frm As Frm41KouseiBuhinSelector = _frmKouseiBuhinSelector

            If EzUtil.ContainsNull(frm.cmbKaihatsuFugo.Text) Then
                Return EMPTY_LIST
            End If

            Dim ShiyouJyouhouDao As ShiyouJyouhouDao = New ShiyouJyouhouDaoImpl
            Return ShiyouJyouhouDao.GetByKataShimukeLabelValues(frm.cmbKaihatsuFugo.Text, _
                                                            frm.spdBuhinShiyou_Sheet1.GetValue(spd_BuhinShiyou_startRow, _
                                                            frm.spdBuhinShiyou_Sheet1.Columns(DispShiyouJyouhouList.TAG_SHIYOU_JYOUHOU_NO.ToString).Index()), _
                                                            frm.spdBuhinShiyou_Sheet1.GetValue(spd_BuhinShiyou_startRow, _
                                                            frm.spdBuhinShiyou_Sheet1.Columns(DispShiyouJyouhouList.TAG_SYAGATA.ToString).Index()), _
                                                            frm.spdBuhinShiyou_Sheet1.GetValue(spd_BuhinShiyou_startRow, _
                                                            frm.spdBuhinShiyou_Sheet1.Columns(DispShiyouJyouhouList.TAG_GRADE.ToString).Index()), _
                                                            frm.spdBuhinShiyou_Sheet1.GetValue(spd_BuhinShiyou_startRow, _
                                                            frm.spdBuhinShiyou_Sheet1.Columns(DispShiyouJyouhouList.TAG_SHIMUKECHI_HANDLE.ToString).Index()), _
                                                            frm.spdBuhinShiyou_Sheet1.GetValue(spd_BuhinShiyou_startRow, _
                                                            frm.spdBuhinShiyou_Sheet1.Columns(DispShiyouJyouhouList.TAG_EG_HAIKIRYOU.ToString).Index()), _
                                                            frm.spdBuhinShiyou_Sheet1.GetValue(spd_BuhinShiyou_startRow, _
                                                            frm.spdBuhinShiyou_Sheet1.Columns(DispShiyouJyouhouList.TAG_EG_KEISHIKI.ToString).Index()), _
                                                            frm.spdBuhinShiyou_Sheet1.GetValue(spd_BuhinShiyou_startRow, _
                                                            frm.spdBuhinShiyou_Sheet1.Columns(DispShiyouJyouhouList.TAG_EG_KAKYUKI.ToString).Index()), _
                                                            frm.spdBuhinShiyou_Sheet1.GetValue(spd_BuhinShiyou_startRow, _
                                                            frm.spdBuhinShiyou_Sheet1.Columns(DispShiyouJyouhouList.TAG_TM_KUDOU.ToString).Index()), _
                                                            frm.spdBuhinShiyou_Sheet1.GetValue(spd_BuhinShiyou_startRow, _
                                                            frm.spdBuhinShiyou_Sheet1.Columns(DispShiyouJyouhouList.TAG_TM_HENSOKUKI.ToString).Index()), _
                                                            frm.spdBuhinShiyou_Sheet1.GetValue(spd_BuhinShiyou_startRow, _
                                                            frm.spdBuhinShiyou_Sheet1.Columns(DispShiyouJyouhouList.TAG_KATA_7_KETA_KATASHIKI.ToString).Index()), _
                                                            frm.spdBuhinShiyou_Sheet1.GetValue(spd_BuhinShiyou_startRow, _
                                                            frm.spdBuhinShiyou_Sheet1.Columns(DispShiyouJyouhouList.TAG_KATA_SHIMUKE.ToString).Index()), _
                                                            frm.spdBuhinShiyou_Sheet1.GetValue(spd_BuhinShiyou_startRow, _
                                                            frm.spdBuhinShiyou_Sheet1.Columns(DispShiyouJyouhouList.TAG_KATA_OP.ToString).Index()))

        End Function

        ''' <summary>仕向けコードのＨＥＬＰの表示値を返す</summary>
        ''' <returns>表示値</returns>
        ''' <remarks></remarks>
        Public Function GetLabelValues_KataShimukeHelp() As List(Of LabelValueVo)

            Dim frm As Frm41KouseiBuhinSelector = _frmKouseiBuhinSelector

            Dim ShiyouJyouhouDao As ShiyouJyouhouDao = New ShiyouJyouhouDaoImpl
            Return ShiyouJyouhouDao.GetByKataShimukeHelpValues(frm.cmbKaihatsuFugo.Text, _
                                                            frm.spdBuhinShiyou_Sheet1.GetValue(spd_BuhinShiyou_startRow, _
                                                            frm.spdBuhinShiyou_Sheet1.Columns(DispShiyouJyouhouList.TAG_SHIYOU_JYOUHOU_NO.ToString).Index()), _
                                                            frm.spdBuhinShiyou_Sheet1.GetValue(spd_BuhinShiyou_startRow, _
                                                            frm.spdBuhinShiyou_Sheet1.Columns(DispShiyouJyouhouList.TAG_SYAGATA.ToString).Index()), _
                                                            frm.spdBuhinShiyou_Sheet1.GetValue(spd_BuhinShiyou_startRow, _
                                                            frm.spdBuhinShiyou_Sheet1.Columns(DispShiyouJyouhouList.TAG_GRADE.ToString).Index()), _
                                                            frm.spdBuhinShiyou_Sheet1.GetValue(spd_BuhinShiyou_startRow, _
                                                            frm.spdBuhinShiyou_Sheet1.Columns(DispShiyouJyouhouList.TAG_SHIMUKECHI_HANDLE.ToString).Index()), _
                                                            frm.spdBuhinShiyou_Sheet1.GetValue(spd_BuhinShiyou_startRow, _
                                                            frm.spdBuhinShiyou_Sheet1.Columns(DispShiyouJyouhouList.TAG_EG_HAIKIRYOU.ToString).Index()), _
                                                            frm.spdBuhinShiyou_Sheet1.GetValue(spd_BuhinShiyou_startRow, _
                                                            frm.spdBuhinShiyou_Sheet1.Columns(DispShiyouJyouhouList.TAG_EG_KEISHIKI.ToString).Index()), _
                                                            frm.spdBuhinShiyou_Sheet1.GetValue(spd_BuhinShiyou_startRow, _
                                                            frm.spdBuhinShiyou_Sheet1.Columns(DispShiyouJyouhouList.TAG_EG_KAKYUKI.ToString).Index()), _
                                                            frm.spdBuhinShiyou_Sheet1.GetValue(spd_BuhinShiyou_startRow, _
                                                            frm.spdBuhinShiyou_Sheet1.Columns(DispShiyouJyouhouList.TAG_TM_KUDOU.ToString).Index()), _
                                                            frm.spdBuhinShiyou_Sheet1.GetValue(spd_BuhinShiyou_startRow, _
                                                            frm.spdBuhinShiyou_Sheet1.Columns(DispShiyouJyouhouList.TAG_TM_HENSOKUKI.ToString).Index()), _
                                                            frm.spdBuhinShiyou_Sheet1.GetValue(spd_BuhinShiyou_startRow, _
                                                            frm.spdBuhinShiyou_Sheet1.Columns(DispShiyouJyouhouList.TAG_KATA_7_KETA_KATASHIKI.ToString).Index()), _
                                                            frm.spdBuhinShiyou_Sheet1.GetValue(spd_BuhinShiyou_startRow, _
                                                            frm.spdBuhinShiyou_Sheet1.Columns(DispShiyouJyouhouList.TAG_KATA_SHIMUKE.ToString).Index()), _
                                                            frm.spdBuhinShiyou_Sheet1.GetValue(spd_BuhinShiyou_startRow, _
                                                            frm.spdBuhinShiyou_Sheet1.Columns(DispShiyouJyouhouList.TAG_KATA_OP.ToString).Index()))

        End Function

        ''' <summary>ＯＰコードのコンボボックスの表示値を返す</summary>
        ''' <returns>表示値</returns>
        ''' <remarks></remarks>
        Public Function GetLabelValues_KataOp() As List(Of LabelValueVo)

            Dim frm As Frm41KouseiBuhinSelector = _frmKouseiBuhinSelector

            If EzUtil.ContainsNull(frm.cmbKaihatsuFugo.Text) Then
                Return EMPTY_LIST
            End If

            Dim ShiyouJyouhouDao As ShiyouJyouhouDao = New ShiyouJyouhouDaoImpl
            Return ShiyouJyouhouDao.GetByKataOpLabelValues(frm.cmbKaihatsuFugo.Text, _
                                                            frm.spdBuhinShiyou_Sheet1.GetValue(spd_BuhinShiyou_startRow, _
                                                            frm.spdBuhinShiyou_Sheet1.Columns(DispShiyouJyouhouList.TAG_SHIYOU_JYOUHOU_NO.ToString).Index()), _
                                                            frm.spdBuhinShiyou_Sheet1.GetValue(spd_BuhinShiyou_startRow, _
                                                            frm.spdBuhinShiyou_Sheet1.Columns(DispShiyouJyouhouList.TAG_SYAGATA.ToString).Index()), _
                                                            frm.spdBuhinShiyou_Sheet1.GetValue(spd_BuhinShiyou_startRow, _
                                                            frm.spdBuhinShiyou_Sheet1.Columns(DispShiyouJyouhouList.TAG_GRADE.ToString).Index()), _
                                                            frm.spdBuhinShiyou_Sheet1.GetValue(spd_BuhinShiyou_startRow, _
                                                            frm.spdBuhinShiyou_Sheet1.Columns(DispShiyouJyouhouList.TAG_SHIMUKECHI_HANDLE.ToString).Index()), _
                                                            frm.spdBuhinShiyou_Sheet1.GetValue(spd_BuhinShiyou_startRow, _
                                                            frm.spdBuhinShiyou_Sheet1.Columns(DispShiyouJyouhouList.TAG_EG_HAIKIRYOU.ToString).Index()), _
                                                            frm.spdBuhinShiyou_Sheet1.GetValue(spd_BuhinShiyou_startRow, _
                                                            frm.spdBuhinShiyou_Sheet1.Columns(DispShiyouJyouhouList.TAG_EG_KEISHIKI.ToString).Index()), _
                                                            frm.spdBuhinShiyou_Sheet1.GetValue(spd_BuhinShiyou_startRow, _
                                                            frm.spdBuhinShiyou_Sheet1.Columns(DispShiyouJyouhouList.TAG_EG_KAKYUKI.ToString).Index()), _
                                                            frm.spdBuhinShiyou_Sheet1.GetValue(spd_BuhinShiyou_startRow, _
                                                            frm.spdBuhinShiyou_Sheet1.Columns(DispShiyouJyouhouList.TAG_TM_KUDOU.ToString).Index()), _
                                                            frm.spdBuhinShiyou_Sheet1.GetValue(spd_BuhinShiyou_startRow, _
                                                            frm.spdBuhinShiyou_Sheet1.Columns(DispShiyouJyouhouList.TAG_TM_HENSOKUKI.ToString).Index()), _
                                                            frm.spdBuhinShiyou_Sheet1.GetValue(spd_BuhinShiyou_startRow, _
                                                            frm.spdBuhinShiyou_Sheet1.Columns(DispShiyouJyouhouList.TAG_KATA_7_KETA_KATASHIKI.ToString).Index()), _
                                                            frm.spdBuhinShiyou_Sheet1.GetValue(spd_BuhinShiyou_startRow, _
                                                            frm.spdBuhinShiyou_Sheet1.Columns(DispShiyouJyouhouList.TAG_KATA_SHIMUKE.ToString).Index()), _
                                                            frm.spdBuhinShiyou_Sheet1.GetValue(spd_BuhinShiyou_startRow, _
                                                            frm.spdBuhinShiyou_Sheet1.Columns(DispShiyouJyouhouList.TAG_KATA_OP.ToString).Index()))

        End Function

#End Region

#Region "OP項目列の作成"
        ''' <summary>ＯＰ項目列を作成する。</summary>
        ''' <remarks></remarks>
        Public Sub setOpKoumokuRetuHandle()

            Dim frm As Frm41KouseiBuhinSelector = _frmKouseiBuhinSelector

            Dim j As Integer = 0
            Dim ShiyouJyouhouDao As ShiyouJyouhouDao = New ShiyouJyouhouDaoImpl
            'OPlistを取得
            Dim OpListVos As New List(Of OpListVo)
            OpListVos = ShiyouJyouhouDao.GetByOpKoumokuRetuValues(frm.cmbKaihatsuFugo.Text)

            'ＯＰ項目列用
            Dim labels As New List(Of String)
            Dim values As New List(Of String)
            labels.Add(String.Empty)
            values.Add(Nothing)
            labels.Add("○")
            values.Add("○")
            Dim NewComboBoxCellType As ComboBoxCellType = ShisakuSpreadUtil.NewGeneralComboBoxCellType()
            NewComboBoxCellType.MaxLength = 1
            NewComboBoxCellType.Items = labels.ToArray
            NewComboBoxCellType.Items = labels.ToArray
            NewComboBoxCellType.ItemData = values.ToArray
            NewComboBoxCellType.EditorValue = EditorValue.ItemData

            Dim newBorder As BorderStyle = BorderStyle.None

            Dim NewTextType As New TextCellType
            NewTextType.TextOrientation = FarPoint.Win.TextOrientation.TextVertical


            'Op項目列リストに値があれば続行
            If OpListVos.Count > 0 Then
                '追加位置確保
                Dim rowCnt As Integer = frm.spdBuhinShiyou_Sheet1.Columns(DispShiyouJyouhouList.TAG_KATA_OP.ToString).Index() + 1
                '前回生成したOP項目列が存在したら削除する。
                If frm.spdBuhinShiyou_Sheet1.ColumnCount > (rowCnt + 2) Then
                    frm.spdBuhinShiyou_Sheet1.RemoveColumns((rowCnt + 2), frm.spdBuhinShiyou_Sheet1.ColumnCount - (rowCnt + 2))
                End If
                '今回取得したOP項目数分列を挿入する。
                frm.spdBuhinShiyou_Sheet1.AddColumns((rowCnt + 1), OpListVos.Count - 2)
                'ＯＰ項目列に値を設定する。
                For i As Integer = rowCnt To rowCnt + OpListVos.Count - 1
                    frm.spdBuhinShiyou_Sheet1.SetValue(spd_BuhinShiyou_KaihatuFugoRow, i, OpListVos.Item(j).KaihatsuFugo)
                    frm.spdBuhinShiyou_Sheet1.SetValue(spd_BuhinShiyou_OpSpecCodeRow, i, OpListVos.Item(j).OpSpecCode)
                    frm.spdBuhinShiyou_Sheet1.SetValue(spd_BuhinShiyou_OpSpecNameRow, i, OpListVos.Item(j).OpSpecName)
                    frm.spdBuhinShiyou_Sheet1.Cells(spd_BuhinShiyou_startRow, i).CellType = New TextCellType
                    '
                    'ブランクを設定
                    frm.spdBuhinShiyou_Sheet1.Cells(spd_BuhinShiyou_startRow, i).Text = ""

                    frm.spdBuhinShiyou_Sheet1.Cells(spd_BuhinShiyou_startRow, i).Locked = True

                    j += 1
                Next
                frm.spdBuhinShiyou_Sheet1.Cells(0, rowCnt).ColumnSpan = OpListVos.Count
                frm.spdBuhinShiyou_Sheet1.Cells(0, rowCnt, spd_BuhinShiyou_OpSpecNameRow, rowCnt + OpListVos.Count - 1).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center
                frm.spdBuhinShiyou_Sheet1.Cells(0, rowCnt, spd_BuhinShiyou_OpSpecNameRow, rowCnt + OpListVos.Count - 1).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Top
                frm.spdBuhinShiyou_Sheet1.Cells(0, rowCnt, spd_BuhinShiyou_OpSpecNameRow, rowCnt + OpListVos.Count - 1).Font = New Font("MS UI Gothic", 8, FontStyle.Regular)
                frm.spdBuhinShiyou_Sheet1.Cells(0, rowCnt, spd_BuhinShiyou_OpSpecNameRow, rowCnt + OpListVos.Count - 1).BackColor = Color.Yellow
                frm.spdBuhinShiyou_Sheet1.Cells(1, rowCnt, spd_BuhinShiyou_OpSpecNameRow, rowCnt + OpListVos.Count - 1).CellType = NewTextType
                'ＯＰ項目列をロックする。
                frm.spdBuhinShiyou_Sheet1.Cells(1, rowCnt, spd_BuhinShiyou_OpSpecNameRow, rowCnt + OpListVos.Count - 1).Locked = True
                '幅を調整（コンボボックスなので４０にしている）
                frm.spdBuhinShiyou_Sheet1.Columns(rowCnt, rowCnt + OpListVos.Count - 1).Width = 40

                'OPspeclistを取得
                Dim ShiyouJyouhouNo As String = _
                    frm.spdBuhinShiyou_Sheet1.GetValue(spd_BuhinShiyou_startRow, _
                                                                frm.spdBuhinShiyou_Sheet1.Columns(DispShiyouJyouhouList.TAG_SHIYOU_JYOUHOU_NO.ToString).Index())
                Dim OpSpecListVos As New List(Of OpSpecListVo)
                OpSpecListVos = ShiyouJyouhouDao.GetByOpSpecValues(frm.cmbKaihatsuFugo.Text, ShiyouJyouhouNo)

                'OPspecListから適用を設定
                For keta As Integer = 1 To 3

                    For m As Integer = 0 To OpSpecListVos.Count - 1

                        'ＯＰコード
                        Dim cOpCode As String = _
                            frm.spdBuhinShiyou_Sheet1.GetValue(spd_BuhinShiyou_startRow, _
                                                                        frm.spdBuhinShiyou_Sheet1.Columns(DispShiyouJyouhouList.TAG_KATA_OP.ToString).Index())
                        'ＯＰコードが入力されていたら
                        If StringUtil.IsNotEmpty(cOpCode) Then

                            'ＯＰ桁位置と記号がが一致したら
                            If keta = OpSpecListVos.Item(m).OpcdKetaichi And Mid(cOpCode, keta, 1) = OpSpecListVos.Item(m).OpKigo Then

                                For i As Integer = rowCnt To rowCnt + OpListVos.Count - 1

                                    '開発符号とOPスペックコードが一致したら

                                    '開発符号
                                    Dim cKaihatsuFugo As String = _
                                        frm.spdBuhinShiyou_Sheet1.GetValue(spd_BuhinShiyou_KaihatuFugoRow, i)
                                    'ＯＰスペックコード
                                    Dim cOpSpecCode As String = _
                                        frm.spdBuhinShiyou_Sheet1.GetValue(spd_BuhinShiyou_OpSpecCodeRow, i)

                                    '同じか？
                                    If StringUtil.Equals(OpSpecListVos.Item(m).KaihatsuFugo, cKaihatsuFugo) And _
                                        StringUtil.Equals(OpSpecListVos.Item(m).OpSpecCode, cOpSpecCode) Then

                                        frm.spdBuhinShiyou_Sheet1.Cells(spd_BuhinShiyou_startRow, i).Text = "○"

                                    End If

                                Next

                            End If

                        End If

                    Next

                Next keta
            End If

        End Sub

#End Region

#Region "セルタイプ指定"

        ''' <summary>仕様情報№</summary>
        ''' <returns>表示値</returns>
        ''' <remarks></remarks>
        Public Function NewShiyouJyouhouNoCellType() As ComboBoxCellType
            Dim result As ComboBoxCellType = ShisakuSpreadUtil.NewGeneralComboBoxCellType()
            SpreadUtil.BindLabelValuesToComboBoxCellType(result, GetLabelValues_ShiyouJyouhouNo(), False)
            result.MaxLength = 4
            Return result
        End Function

        ''' <summary>車型</summary>
        ''' <returns>表示値</returns>
        ''' <remarks></remarks>
        Public Function NewSyagataCellType() As ComboBoxCellType
            Dim result As ComboBoxCellType = ShisakuSpreadUtil.NewGeneralComboBoxCellType()
            SpreadUtil.BindLabelValuesToComboBoxCellType(result, GetLabelValues_Syagata(), False)
            result.MaxLength = 20
            Return result
        End Function

        ''' <summary>ｸﾞﾚｰﾄﾞ</summary>
        ''' <returns>表示値</returns>
        ''' <remarks></remarks>
        Public Function NewGradeCellType() As ComboBoxCellType
            Dim result As ComboBoxCellType = ShisakuSpreadUtil.NewGeneralComboBoxCellType()
            SpreadUtil.BindLabelValuesToComboBoxCellType(result, GetLabelValues_Grade(), False)
            result.MaxLength = 20
            Return result
        End Function

        ''' <summary>仕向地・仕向け</summary>
        ''' <returns>表示値</returns>
        ''' <remarks></remarks>
        Public Function NewShimukechiShimukeCellType() As ComboBoxCellType
            Dim result As ComboBoxCellType = ShisakuSpreadUtil.NewGeneralComboBoxCellType()
            SpreadUtil.BindLabelValuesToComboBoxCellType(result, GetLabelValues_ShimukechiShimuke(), False)
            result.MaxLength = 6
            Return result
        End Function

        ''' <summary>仕向地・ﾊﾝﾄﾞﾙ</summary>
        ''' <returns>表示値</returns>
        ''' <remarks></remarks>
        Public Function NewShimukechiHandleCellType() As ComboBoxCellType
            Dim result As ComboBoxCellType = ShisakuSpreadUtil.NewGeneralComboBoxCellType()
            SpreadUtil.BindLabelValuesToComboBoxCellType(result, GetLabelValues_ShimukechiHandle(), False)
            result.MaxLength = 1
            Return result
        End Function

        ''' <summary>E/G・排気量</summary>
        ''' <returns>表示値</returns>
        ''' <remarks></remarks>
        Public Function NewEgHaikiryouCellType() As ComboBoxCellType
            Dim result As ComboBoxCellType = ShisakuSpreadUtil.NewGeneralComboBoxCellType()
            SpreadUtil.BindLabelValuesToComboBoxCellType(result, GetLabelValues_EgHaikiryou(), False)
            result.MaxLength = 4
            Return result
        End Function

        ''' <summary>E/G・形式</summary>
        ''' <returns>表示値</returns>
        ''' <remarks></remarks>
        Public Function NewEgKeishikiCellType() As ComboBoxCellType
            Dim result As ComboBoxCellType = ShisakuSpreadUtil.NewGeneralComboBoxCellType()
            SpreadUtil.BindLabelValuesToComboBoxCellType(result, GetLabelValues_EgKeishiki(), False)
            result.MaxLength = 4
            Return result
        End Function

        ''' <summary>E/G・過給器</summary>
        ''' <returns>表示値</returns>
        ''' <remarks></remarks>
        Public Function NewEgKakyukiCellType() As ComboBoxCellType
            Dim result As ComboBoxCellType = ShisakuSpreadUtil.NewGeneralComboBoxCellType()
            SpreadUtil.BindLabelValuesToComboBoxCellType(result, GetLabelValues_EgKakyuki(), False)
            result.MaxLength = 4
            Return result
        End Function

        ''' <summary>T/M・駆動方式</summary>
        ''' <returns>表示値</returns>
        ''' <remarks></remarks>
        Public Function NewTmKudouCellType() As ComboBoxCellType
            Dim result As ComboBoxCellType = ShisakuSpreadUtil.NewGeneralComboBoxCellType()
            SpreadUtil.BindLabelValuesToComboBoxCellType(result, GetLabelValues_TmKudou(), False)
            result.MaxLength = 4
            Return result
        End Function

        ''' <summary>T/M・変速機</summary>
        ''' <returns>表示値</returns>
        ''' <remarks></remarks>
        Public Function NewTmHensokukiCellType() As ComboBoxCellType
            Dim result As ComboBoxCellType = ShisakuSpreadUtil.NewGeneralComboBoxCellType()
            SpreadUtil.BindLabelValuesToComboBoxCellType(result, GetLabelValues_TmHensokuki(), False)
            result.MaxLength = 8
            Return result
        End Function

        ''' <summary>７桁型式</summary>
        ''' <returns>表示値</returns>
        ''' <remarks></remarks>
        Public Function NewKatashiki7CellType() As ComboBoxCellType
            Dim result As ComboBoxCellType = ShisakuSpreadUtil.NewGeneralComboBoxCellType()
            SpreadUtil.BindLabelValuesToComboBoxCellType(result, GetLabelValues_Katashiki7(), False)
            result.MaxLength = 7
            Return result
        End Function

        ''' <summary>仕向けコード</summary>
        ''' <returns>表示値</returns>
        ''' <remarks></remarks>
        Public Function NewKataShimukeCellType() As ComboBoxCellType
            Dim result As ComboBoxCellType = ShisakuSpreadUtil.NewGeneralComboBoxCellType()
            SpreadUtil.BindLabelValuesToComboBoxCellType(result, GetLabelValues_KataShimuke(), False)
            result.MaxLength = 4
            Return result
        End Function

        ''' <summary>ＯＰコード</summary>
        ''' <returns>表示値</returns>
        ''' <remarks></remarks>
        Public Function NewKataOpCellType() As ComboBoxCellType
            Dim result As ComboBoxCellType = ShisakuSpreadUtil.NewGeneralComboBoxCellType()
            SpreadUtil.BindLabelValuesToComboBoxCellType(result, GetLabelValues_KataOp(), False)
            result.MaxLength = 3
            Return result
        End Function

#End Region

#Region "開発符号、ブロック、仕様情報に該当するファイナル品番を返す。"

        ''' <summary>開発符号、ブロック、仕様情報に該当するファイナル品番を返す。</summary>
        ''' <returns>表示値</returns>
        ''' <remarks></remarks>
        Public Function GetFinalHinbanValues(ByVal KaihatsuFugo As String, ByVal BlockNo As String) As List(Of FinalHinbanListVo)

            Dim frm As Frm41KouseiBuhinSelector = _frmKouseiBuhinSelector

            If EzUtil.ContainsNull(KaihatsuFugo) And EzUtil.ContainsNull(BlockNo) Then
                Return EMPTY_FINAL_HINBAN_LIST
            End If

            Dim ShiyouJyouhouDao As ShiyouJyouhouDao = New ShiyouJyouhouDaoImpl
            Return ShiyouJyouhouDao.GetByFinalHinbanValues(KaihatsuFugo, BlockNo, _
                                                            frm.spdBuhinShiyou_Sheet1.GetValue(spd_BuhinShiyou_startRow, _
                                                            frm.spdBuhinShiyou_Sheet1.Columns(DispShiyouJyouhouList.TAG_SHIYOU_JYOUHOU_NO.ToString).Index()), _
                                                            frm.spdBuhinShiyou_Sheet1.GetValue(spd_BuhinShiyou_startRow, _
                                                            frm.spdBuhinShiyou_Sheet1.Columns(DispShiyouJyouhouList.TAG_SYAGATA.ToString).Index()), _
                                                            frm.spdBuhinShiyou_Sheet1.GetValue(spd_BuhinShiyou_startRow, _
                                                            frm.spdBuhinShiyou_Sheet1.Columns(DispShiyouJyouhouList.TAG_GRADE.ToString).Index()), _
                                                            frm.spdBuhinShiyou_Sheet1.GetValue(spd_BuhinShiyou_startRow, _
                                                            frm.spdBuhinShiyou_Sheet1.Columns(DispShiyouJyouhouList.TAG_SHIMUKECHI_HANDLE.ToString).Index()), _
                                                            frm.spdBuhinShiyou_Sheet1.GetValue(spd_BuhinShiyou_startRow, _
                                                            frm.spdBuhinShiyou_Sheet1.Columns(DispShiyouJyouhouList.TAG_EG_HAIKIRYOU.ToString).Index()), _
                                                            frm.spdBuhinShiyou_Sheet1.GetValue(spd_BuhinShiyou_startRow, _
                                                            frm.spdBuhinShiyou_Sheet1.Columns(DispShiyouJyouhouList.TAG_EG_KEISHIKI.ToString).Index()), _
                                                            frm.spdBuhinShiyou_Sheet1.GetValue(spd_BuhinShiyou_startRow, _
                                                            frm.spdBuhinShiyou_Sheet1.Columns(DispShiyouJyouhouList.TAG_EG_KAKYUKI.ToString).Index()), _
                                                            frm.spdBuhinShiyou_Sheet1.GetValue(spd_BuhinShiyou_startRow, _
                                                            frm.spdBuhinShiyou_Sheet1.Columns(DispShiyouJyouhouList.TAG_TM_KUDOU.ToString).Index()), _
                                                            frm.spdBuhinShiyou_Sheet1.GetValue(spd_BuhinShiyou_startRow, _
                                                            frm.spdBuhinShiyou_Sheet1.Columns(DispShiyouJyouhouList.TAG_TM_HENSOKUKI.ToString).Index()), _
                                                            frm.spdBuhinShiyou_Sheet1.GetValue(spd_BuhinShiyou_startRow, _
                                                            frm.spdBuhinShiyou_Sheet1.Columns(DispShiyouJyouhouList.TAG_KATA_7_KETA_KATASHIKI.ToString).Index()), _
                                                            frm.spdBuhinShiyou_Sheet1.GetValue(spd_BuhinShiyou_startRow, _
                                                            frm.spdBuhinShiyou_Sheet1.Columns(DispShiyouJyouhouList.TAG_KATA_SHIMUKE.ToString).Index()), _
                                                            frm.spdBuhinShiyou_Sheet1.GetValue(spd_BuhinShiyou_startRow, _
                                                            frm.spdBuhinShiyou_Sheet1.Columns(DispShiyouJyouhouList.TAG_KATA_OP.ToString).Index()))

        End Function

#End Region

#Region "パターン名制御"

        ''' <summary>
        ''' パターン名SPD作成
        ''' </summary>
        ''' <param name="patternListVos"></param>
        ''' <remarks></remarks>
        Public Sub setPattern(ByVal patternListVos As List(Of TYosanBuhinEditPatternVo))

            Dim frm As Frm41KouseiBuhinSelector = _frmKouseiBuhinSelector

            frm.spdPattern_Sheet1.RowCount = 0

            Try
                m_PatternList = New DispPatternList(frm)
                m_PatternList.InitView(patternListVos)
            Catch ex As Exception
                Throw
            End Try

        End Sub
#End Region

#Region "検索イベント"

        ''' <summary>
        ''' 検索イベント
        ''' </summary>
        ''' <param name="kaihatsuFugo"></param>
        ''' <param name="selectKbn"></param>
        ''' <remarks></remarks>
        Public Sub doSearchEvent(ByVal kaihatsuFugo As String, ByVal selectKbn As String, ByVal unitkbn As String, ByVal unitKbnChk As Boolean)
            'Try
            If StringUtil.IsEmpty(kaihatsuFugo) AndAlso StringUtil.IsEmpty(selectKbn) Then
                Return
            End If

            Dim frm As Frm41KouseiBuhinSelector = _frmKouseiBuhinSelector

            If StringUtil.Equals(frm.cmbSelect.Text, HOYOU_SELECT_MBOM_SHISAKUTEHAI) Then
                frm.pnl2.Visible = True '試作手配システム検索用
                frm.cmbShisakuEventCode.Enabled = True
                frm.cmbShisakuEventName.Enabled = True
                '仕様情報の検索スプレッド関連は非表示にする。
                frm.lblBuhinShiyou.Visible = False
                frm.spdBuhinShiyou.Visible = False
                frm.btnShiyouOnOff.Visible = False
                frm.btnReset.Visible = False
                'フェーズコンボボックス非表示
                frm.cmbPhase.Visible = False
                '
                ShiyouFormSeigyo("MBOM")
            ElseIf StringUtil.Equals(frm.cmbSelect.Text, HOYOU_SELECT_VER1) Then
                frm.pnl2.Visible = True '試作手配システム検索用
                frm.cmbShisakuEventCode.Enabled = True
                frm.lblBuhinShiyou.Visible = False
                frm.spdBuhinShiyou.Visible = False
                frm.btnShiyouOnOff.Visible = False
                frm.btnReset.Visible = False
                'フェーズコンボボックス非表示
                frm.cmbPhase.Visible = False
                '
                ShiyouFormSeigyo("MBOM")
            ElseIf StringUtil.Equals(frm.cmbSelect.Text, HOYOU_SELECT_VER2) Then
                frm.pnl2.Visible = True '試作手配システム検索用
                frm.cmbShisakuEventCode.Enabled = True
                frm.lblBuhinShiyou.Visible = False
                frm.spdBuhinShiyou.Visible = False
                frm.btnShiyouOnOff.Visible = False
                frm.btnReset.Visible = False
                'フェーズコンボボックス表示
                frm.cmbPhase.Visible = True
                '
                ShiyouFormSeigyo("MBOM")
            Else
                frm.cmbShisakuEventCode.Enabled = False
                frm.cmbShisakuEventName.Enabled = False
                frm.pnl2.Visible = False '試作手配システム検索用
                '仕様情報の検索スプレッド関連を表示する。
                frm.lblBuhinShiyou.Visible = True
                frm.spdBuhinShiyou.Visible = True
                frm.btnShiyouOnOff.Visible = True
                frm.btnReset.Visible = True
                '
                ShiyouFormSeigyo("EBOM")

                '==========================================================
                '項目初期化
                For i As Integer = frm.spdBuhinShiyou_Sheet1.Columns(DispShiyouJyouhouList.TAG_SHIYOU_JYOUHOU_NO.ToString).Index() _
                                To frm.spdBuhinShiyou_Sheet1.Columns(DispShiyouJyouhouList.TAG_KATA_OP.ToString).Index()
                    frm.spdBuhinShiyou_Sheet1.SetValue(spd_BuhinShiyou_startRow, i, String.Empty)
                Next
                '==========================================================

                '仕様情報一覧の仕様情報№の設定
                SpreadUtil.BindCellTypeToColumn(frm.spdBuhinShiyou_Sheet1, _
                                                DispShiyouJyouhouList.TAG_SHIYOU_JYOUHOU_NO.ToString, _
                                                NewShiyouJyouhouNoCellType)
                '仕様情報一覧の仕様情報№へ最新の№を設定
                Dim ShiyouJyouhouDao As ShiyouJyouhouDao = New ShiyouJyouhouDaoImpl
                Dim rhac0030Vo As Rhac0030Vo = _
                            ShiyouJyouhouDao.GetByNewShiyouJyouhouNo(frm.cmbKaihatsuFugo.Text)
                frm.spdBuhinShiyou_Sheet1.SetValue(spd_BuhinShiyou_startRow, _
                                                   frm.spdBuhinShiyou_Sheet1.Columns(DispShiyouJyouhouList.TAG_SHIYOU_JYOUHOU_NO.ToString).Index(), _
                                                   rhac0030Vo.ShiyoshoSeqno)
                ' 車型のコンボボックスを生成する。
                setShiyouJyouhouListSyagata()
                'ｸﾞﾚｰﾄﾞのコンボボックスを生成する。
                setShiyouJyouhouListGrade()
                '仕向地・仕向けのコンボボックスを生成する。
                'setShiyouJyouhouListShimukechiShimuke()
                '仕向地・ﾊﾝﾄﾞﾙのコンボボックスを生成する。
                setShiyouJyouhouListShimukechiHandle()
                'E/G・排気量のコンボボックスを生成する。
                setEgHaikiryouListShimukechiHandle()
                'E/G・形式のコンボボックスを生成する。
                setEgKeishikiListShimukechiHandle()
                'E/G・過給器のコンボボックスを生成する。
                setEgKakyukiListShimukechiHandle()
                'T/M・駆動方式のコンボボックスを生成する。
                setTmKudouListShimukechiHandle()
                'T/M・変速機のコンボボックスを生成する。
                setTmHensokukiListShimukechiHandle()
                '７桁型式のコンボボックスを生成する。
                setKatashiki7ListShimukechiHandle()
                '仕向けコードのコンボボックスを生成する。
                setKataShimukeListShimukechiHandle()
                'ＯＰコードのコンボボックスを生成する。
                setKataOpListShimukechiHandle()

                'ＯＰ項目列を作成する。
                setOpKoumokuRetuHandle()

            End If

            'frm.cmbShisakuEventCode.Text = ""
            'frm.cmbShisakuEventName.Text = ""

            '各名称はコモンに定義すること。
            Select Case selectKbn
                Case HOYOU_SELECT_EBOM_SYSTEM_DAI
                    '
                    frm.spdKubun_Sheet1.RowCount = 0

                    Try
                        m_kubunList = New DispKubunList(frm)
                        m_kubunList.InitView(kaihatsuFugo, selectKbn)
                    Catch ex As Exception
                        Throw
                    End Try

                    '
                    frm.spdBlock_Sheet1.RowCount = 0

                    Try
                        m_blockList = New DispBlockList(frm)
                        m_blockList.InitView(kaihatsuFugo, "", "", unitkbn, unitKbnChk, "")
                    Catch ex As Exception
                        Throw
                    End Try
                    '
                    frm.spdBuhin_Sheet1.RowCount = 0

                    '開発符号、区分が選択されていたらブロック名称リスト作成
                    If StringUtil.IsNotEmpty(kaihatsuFugo) And StringUtil.IsNotEmpty(selectKbn) Then
                        SetBlockNameCombo(unitkbn, unitKbnChk)
                    End If

                Case HOYOU_SELECT_EBOM_SYSTEM
                    '
                    frm.spdKubun_Sheet1.RowCount = 0

                    Try
                        m_kubunList = New DispKubunList(frm)
                        m_kubunList.InitView(kaihatsuFugo, selectKbn)
                    Catch ex As Exception
                        Throw
                    End Try

                    '
                    frm.spdBlock_Sheet1.RowCount = 0

                    Try
                        m_blockList = New DispBlockList(frm)
                        m_blockList.InitView(kaihatsuFugo, "", "", unitkbn, unitKbnChk, "")
                    Catch ex As Exception
                        Throw
                    End Try

                    '
                    frm.spdBuhin_Sheet1.RowCount = 0

                    '開発符号、区分が選択されていたらブロック名称リスト作成
                    If StringUtil.IsNotEmpty(kaihatsuFugo) And StringUtil.IsNotEmpty(selectKbn) Then
                        SetBlockNameCombo(unitkbn, unitKbnChk)
                    End If

                Case HOYOU_SELECT_EBOM_BLOCK
                    '
                    frm.spdKubun_Sheet1.RowCount = 0

                    '
                    frm.spdBlock_Sheet1.RowCount = 0

                    Try
                        m_blockList = New DispBlockList(frm)
                        m_blockList.InitView(kaihatsuFugo, "", "", unitkbn, unitKbnChk, "")
                    Catch ex As Exception
                        Throw
                    End Try

                    '
                    frm.spdBuhin_Sheet1.RowCount = 0

                    '開発符号、区分が選択されていたらブロック名称リスト作成
                    If StringUtil.IsNotEmpty(kaihatsuFugo) And StringUtil.IsNotEmpty(selectKbn) Then
                        SetBlockNameCombo(unitkbn, unitKbnChk)
                    End If

                Case HOYOU_SELECT_MBOM_SHISAKUTEHAI

                    frm.cmbShisakuEventCode.Text = ""
                    frm.cmbShisakuEventName.Text = ""
                    '
                    frm.spdKubun_Sheet1.RowCount = 0
                    '
                    frm.spdBlock_Sheet1.RowCount = 0
                    '
                    frm.spdBuhin_Sheet1.RowCount = 0

                Case HOYOU_SELECT_EBOM_SOUBISHIYOU

                    frm.cmbShisakuEventCode.Text = ""
                    frm.cmbShisakuEventName.Text = ""
                    '
                    frm.spdKubun_Sheet1.RowCount = 0
                    '
                    frm.spdBlock_Sheet1.RowCount = 0
                    '
                    frm.spdBuhin_Sheet1.RowCount = 0

                Case HOYOU_SELECT_VER1

                    frm.cmbShisakuEventCode.Text = ""
                    frm.cmbShisakuEventName.Text = ""
                    '
                    frm.spdKubun_Sheet1.RowCount = 0
                    '
                    frm.spdBlock_Sheet1.RowCount = 0
                    '
                    frm.spdBuhin_Sheet1.RowCount = 0

                Case HOYOU_SELECT_VER2

                    frm.cmbShisakuEventCode.Text = ""
                    frm.cmbShisakuEventName.Text = ""
                    '
                    frm.spdKubun_Sheet1.RowCount = 0
                    '
                    frm.spdBlock_Sheet1.RowCount = 0
                    '
                    frm.spdBuhin_Sheet1.RowCount = 0

                Case Else
                    '
                    frm.spdKubun_Sheet1.RowCount = 0
                    '
                    frm.spdBlock_Sheet1.RowCount = 0
                    '
                    frm.spdBuhin_Sheet1.RowCount = 0

            End Select

            '位置
            frm.spdKubun_Sheet1.ClearSelection()
            frm.spdKubun.ActiveSheet.SetActiveCell(0, 0)
            frm.spdKubun.ShowActiveCell(FarPoint.Win.Spread.VerticalPosition.Top, FarPoint.Win.Spread.HorizontalPosition.Center)
            frm.spdBlock_Sheet1.ClearSelection()
            frm.spdBlock.ActiveSheet.SetActiveCell(0, 0)
            frm.spdBlock.ShowActiveCell(FarPoint.Win.Spread.VerticalPosition.Top, FarPoint.Win.Spread.HorizontalPosition.Center)

        End Sub
        ''' <summary>
        ''' 号車検索イベント
        ''' </summary>
        ''' <param name="strshisakuevent"></param>
        ''' <remarks></remarks>
        Public Sub doSearchGousya(ByVal strKaihatsuFugo As String, _
                                  ByVal strShisakuEvent As String, ByVal strShisakuEventName As String, _
                                  ByVal strEventCode As String, ByVal strBukaCode As String, _
                                  ByVal strTantoKey As String, ByVal strTanto As String, ByVal strKuke As String, ByVal strChkKuke As Boolean, ByVal strSelect As String)
            Try
                If StringUtil.IsEmpty(strShisakuEvent) And StringUtil.IsEmpty(strShisakuEventName) Then
                    Return
                End If

                Dim frm As Frm41KouseiBuhinSelector = _frmKouseiBuhinSelector
                Dim kouseiBuhinDao As KouseiBuhinSelectorDao = New KouseiBuhinSelectorDaoImpl
                Dim strShisakuSyubetu As String = ""

                '
                frm.spdKubun_Sheet1.RowCount = 0
                If StringUtil.Equals(strSelect, HOYOU_SELECT_VER1) = False Then
                    Try
                        m_gousyaList = New DispGousyaList(frm)
                        m_gousyaList.InitView(strKaihatsuFugo, strShisakuEvent, strShisakuEventName, _
                                              strEventCode, strBukaCode, strTantoKey, strTanto)
                    Catch ex As Exception
                        Throw
                    End Try
                End If


                '
                frm.spdBlock_Sheet1.RowCount = 0
                Try
                    m_blockList = New DispBlockList(frm)
                    m_blockList.InitView(strKaihatsuFugo, strShisakuEvent, strShisakuEventName, strKuke, strChkKuke, strSelect)
                Catch ex As Exception
                    Throw
                End Try

                '
                frm.spdBuhin_Sheet1.RowCount = 0

            Catch ex As Exception
                MessageBox.Show(ex.Message, "異常", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End Try
        End Sub

        ''' <summary>
        ''' ブロック名称あいまい検索イベント
        ''' </summary>
        ''' <param name="kaihatsuFugo"></param>
        ''' <param name="blockName"></param>
        ''' <remarks></remarks>
        Public Sub doSearchBlockName(ByVal kaihatsuFugo As String, ByVal blockName As String)
            Try
                If StringUtil.IsEmpty(kaihatsuFugo) AndAlso StringUtil.IsEmpty(blockName) Then
                    Return
                End If

                Dim frm As Frm41KouseiBuhinSelector = _frmKouseiBuhinSelector

                '
                frm.spdBlock_Sheet1.RowCount = 0

                Try
                    m_blockList = New DispBlockList(frm)
                    m_blockList.SetSpreadSourceAimai(kaihatsuFugo, blockName)
                Catch ex As Exception
                    Throw
                End Try

                '位置
                frm.spdKubun_Sheet1.ClearSelection()
                frm.spdKubun.ActiveSheet.SetActiveCell(0, 0)
                frm.spdKubun.ShowActiveCell(FarPoint.Win.Spread.VerticalPosition.Top, FarPoint.Win.Spread.HorizontalPosition.Center)
                frm.spdBlock_Sheet1.ClearSelection()
                frm.spdBlock.ActiveSheet.SetActiveCell(0, 0)
                frm.spdBlock.ShowActiveCell(FarPoint.Win.Spread.VerticalPosition.Top, FarPoint.Win.Spread.HorizontalPosition.Center)

            Catch ex As Exception
                MessageBox.Show(ex.Message, "異常", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End Try
        End Sub
#End Region

#Region "ブロック反転表示"

        ''' <summary>
        ''' ブロック反転表示
        ''' </summary>
        ''' <param name="kaihatsuFugo"></param>
        ''' <param name="selectKbn"></param>
        ''' <remarks></remarks>
        Public Sub selectBlockNo(ByVal kaihatsuFugo As String, ByVal selectKbn As String)
            Try
                If StringUtil.IsEmpty(kaihatsuFugo) AndAlso StringUtil.IsEmpty(selectKbn) Then
                    Return
                End If

                Dim frm As Frm41KouseiBuhinSelector = _frmKouseiBuhinSelector

                Dim sysKbnVos As New List(Of SystemKbnVo)

                '
                Dim selection As FarPoint.Win.Spread.Model.CellRange() = frm.spdKubun_Sheet1.GetSelections()
                For rowindex As Integer = 0 To selection.Length - 1
                    Dim sysKbnVo As New SystemKbnVo
                    sysKbnVo.IdField = frm.spdKubun_Sheet1.Cells(selection(rowindex).Row, 0).Value
                    sysKbnVos.Add(sysKbnVo)
                Next

                Dim kouseiBuhinDao As KouseiBuhinSelectorDao = New KouseiBuhinSelectorDaoImpl
                Dim blockList As New List(Of BlockListVo)

                '各名称はコモンに定義すること。
                Select Case selectKbn
                    Case HOYOU_SELECT_EBOM_SYSTEM_DAI
                        blockList = kouseiBuhinDao.GetBySystemDaiKbnToBlockEbom(kaihatsuFugo, sysKbnVos)
                    Case HOYOU_SELECT_EBOM_SYSTEM
                        blockList = kouseiBuhinDao.GetBySystemKbnToBlockEbom(kaihatsuFugo, sysKbnVos)
                    Case Else
                        Return
                End Select

                frm.spdBlock_Sheet1.ClearSelection()
                Dim i As Integer = 0

                For Each list In blockList
                    For idx As Integer = 0 To frm.spdBlock_Sheet1.RowCount - 1
                        Dim blockNo As String = frm.spdBlock_Sheet1.Cells(idx, 0).Value
                        If blockNo = list.BlockNo Then

                            If i = 0 Then
                                frm.spdBlock.ActiveSheet.SetActiveCell(idx, 0)
                                frm.spdBlock.ShowActiveCell(FarPoint.Win.Spread.VerticalPosition.Top, FarPoint.Win.Spread.HorizontalPosition.Center)
                            End If
                            frm.spdBlock_Sheet1.AddSelection(idx, 1, 1, 1)
                            i = i + 1

                            Exit For
                        End If
                    Next
                Next
            Catch ex As Exception
                MessageBox.Show(ex.Message, "異常", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End Try
        End Sub


        ''' <summary>
        ''' 試作ブロック反転表示
        ''' </summary>
        ''' <param name="kaihatsuFugo"></param>
        ''' <remarks></remarks>
        Public Sub selectShisakuBlockNo(ByVal kaihatsuFugo As String, ByVal shisakuEventCode As String, ByVal shisakuEventName As String)
            Try
                If StringUtil.IsEmpty(shisakuEventCode) And StringUtil.IsEmpty(shisakuEventName) Then
                    Return
                End If

                Dim frm As Frm41KouseiBuhinSelector = _frmKouseiBuhinSelector
                Dim sysKbnVos As New List(Of SystemKbnVo)
                '
                Dim selection As FarPoint.Win.Spread.Model.CellRange() = frm.spdKubun_Sheet1.GetSelections()
                For rowindex As Integer = 0 To selection.Length - 1
                    Dim sysKbnVo As New SystemKbnVo
                    sysKbnVo.IdField = frm.spdKubun_Sheet1.Cells(selection(rowindex).Row, 1).Value
                    sysKbnVos.Add(sysKbnVo)
                Next

                Dim kouseiBuhinDao As KouseiBuhinSelectorDao = New KouseiBuhinSelectorDaoImpl
                Dim blockList As New List(Of BlockListVo)

                If StringUtil.IsEmpty(shisakuEventName) Then
                    blockList = kouseiBuhinDao.GetByGousyaToBlockMbomFromC(kaihatsuFugo, shisakuEventCode, sysKbnVos)
                Else
                    blockList = kouseiBuhinDao.GetByGousyaToBlockMbomFromN(kaihatsuFugo, shisakuEventName, sysKbnVos)
                End If

                frm.spdBlock_Sheet1.ClearSelection()
                Dim i As Integer = 0

                For Each list In blockList
                    For idx As Integer = 0 To frm.spdBlock_Sheet1.RowCount - 1
                        Dim blockNo As String = frm.spdBlock_Sheet1.Cells(idx, 0).Value
                        If blockNo = list.BlockNo Then

                            If i = 0 Then
                                frm.spdBlock.ActiveSheet.SetActiveCell(idx, 0)
                                frm.spdBlock.ShowActiveCell(FarPoint.Win.Spread.VerticalPosition.Top, FarPoint.Win.Spread.HorizontalPosition.Center)
                            End If
                            frm.spdBlock_Sheet1.AddSelection(idx, 1, 1, 1)
                            i = i + 1

                            Exit For
                        End If
                    Next
                Next

            Catch ex As Exception
                MessageBox.Show(ex.Message, "異常", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End Try
        End Sub

#End Region

#Region "部品番号表示"

        ''' <summary>
        ''' 補用部品番号表示
        ''' </summary>
        ''' <param name="kaihatsuFugo"></param>
        ''' <remarks></remarks>
        Public Sub ExecuteShisakuBuhinInfo(ByVal kaihatsuFugo As String, _
                                           ByVal shisakuEventCode As String, _
                                           ByVal strSelect As String, _
                                           ByVal phaseNo As Integer)
            Try
                If StringUtil.IsEmpty(shisakuEventCode) Then
                    Return
                End If

                Dim frm As Frm41KouseiBuhinSelector = _frmKouseiBuhinSelector

                Dim blockListVos As New List(Of BlockListVo)
                'ブロック一覧の選択項目を確保
                Dim selection As FarPoint.Win.Spread.Model.CellRange() = frm.spdBlock_Sheet1.GetSelections()
                For rowindex As Integer = 0 To selection.Length - 1
                    Dim blockListVo As New BlockListVo
                    blockListVo.BlockNo = frm.spdBlock_Sheet1.Cells(selection(rowindex).Row, 0).Value
                    blockListVos.Add(blockListVo)
                Next

                '****** スプレッドの初期化 ******
                m_buhinList = New DispBuhinList(frm)
                m_buhinList.InitView()
                frm.spdBuhin_Sheet1.ClearSelection()
                frm.spdBuhin_Sheet1.RowCount = 0    '最初に全行削除

                '自給品の有無
                Dim jikyu As String = ""
                If StringUtil.Equals(frm.cmbJikyuhinUmu.Text, "無") Then
                    jikyu = "0"
                Else
                    jikyu = "1"
                End If

                Dim kouseiBuhinDao As KouseiBuhinSelectorDao = New KouseiBuhinSelectorDaoImpl
                Dim buhinList As New List(Of TShisakuBuhinEditVoSekkeiHelper)

                'strSelectによって部品情報のテーブルを変更
                buhinList = kouseiBuhinDao.GetByShisakuBuhinToSpreadFromC(kaihatsuFugo, shisakuEventCode, blockListVos, strSelect, phaseNo)

                'スプレットサイズ設定
                frm.spdBuhin_Sheet1.RowCount = buhinList.Count

                frm.pbBuhinBango.Minimum = 1
                frm.pbBuhinBango.Value = 1
                frm.pbBuhinBango.Maximum = buhinList.Count

                '構成列のボタンタイプ
                buttonType.ButtonColor = Color.LightBlue
                buttonType.ButtonColor2 = Color.LightBlue
                buttonType.TextColor = Color.Black


                '号車一覧の選択項目を確保
                Dim sysKbnVos As New List(Of SystemKbnVo)
                Dim selection1 As FarPoint.Win.Spread.Model.CellRange() = frm.spdKubun_Sheet1.GetSelections()
                For rowindex As Integer = 0 To selection1.Length - 1
                    Dim sysKbnVo As New SystemKbnVo
                    sysKbnVo.IdField = frm.spdKubun_Sheet1.Cells(selection1(rowindex).Row, 1).Value
                    sysKbnVos.Add(sysKbnVo)
                Next
                Dim InstlList As New List(Of TShisakuBuhinEditInstlVo)

                Dim fShisakuEventCode As String = String.Empty
                Dim i As Integer = 0

                For Each bVo As TShisakuBuhinEditVoSekkeiHelper In buhinList

                    ' 試作号車に紐づく部品情報を判定
                    If sysKbnVos.Count > 0 Then
                        Dim bFlg As Boolean = False
                        '試作イベントコードが変更されたら、実行
                        If fShisakuEventCode <> bVo.ShisakuEventCode Then
                            ' 試作号車に紐づく部品情報を設定
                            InstlList = kouseiBuhinDao.GetByShisakuBuhinToSpreadBase(bVo.ShisakuEventCode, blockListVos, sysKbnVos)
                            fShisakuEventCode = bVo.ShisakuEventCode
                        End If

                        For Each lVo As TShisakuBuhinEditInstlVo In InstlList

                            If bVo.ShisakuEventCode = lVo.ShisakuEventCode _
                            And bVo.ShisakuBukaCode = lVo.ShisakuBukaCode _
                            And bVo.ShisakuBlockNo = lVo.ShisakuBlockNo _
                            And bVo.ShisakuBlockNoKaiteiNo = lVo.ShisakuBlockNoKaiteiNo _
                            And bVo.BuhinNoHyoujiJun = lVo.BuhinNoHyoujiJun Then
                                bFlg = True
                                Exit For
                            End If
                        Next
                        If bFlg = False Then
                            Continue For
                        End If
                    End If
                    '自給品有無＝無の場合　国内または国外集計コードＪは読み飛ばす。
                    Dim ShukeiCode As String = ""
                    Dim siaShukeiCode As String = ""

                    If StringUtil.IsNotEmpty(bVo.ShukeiCode) Then
                        ShukeiCode = bVo.ShukeiCode.ToString
                    End If
                    If StringUtil.IsNotEmpty(bVo.SiaShukeiCode) Then
                        siaShukeiCode = bVo.SiaShukeiCode.ToString
                    End If

                    If jikyu = "1" Or _
                        jikyu = "0" And ShukeiCode <> "" And ShukeiCode <> "J" Or _
                        jikyu = "0" And ShukeiCode = "" And siaShukeiCode <> "J" Then

                        frm.spdBuhin_Sheet1.Cells(i, spd_Buhin_Col_BlockNo).Value = bVo.ShisakuBlockNo.ToString
                        frm.spdBuhin_Sheet1.Cells(i, spd_Buhin_Col_Level).Value = bVo.Level.ToString
                        frm.spdBuhin_Sheet1.Cells(i, spd_Buhin_Col_ShukeiCode).Value = ShukeiCode
                        frm.spdBuhin_Sheet1.Cells(i, spd_Buhin_Col_SiaShukeiCode).Value = siaShukeiCode
                        '構成表示ボタン
                        frm.spdBuhin_Sheet1.Cells(i, spd_Buhin_Col_Tenkai).CellType = buttonType
                        frm.spdBuhin_Sheet1.Cells(i, spd_Buhin_Col_Tenkai).BackColor = Color.LightBlue
                        '選択チェックボックス
                        frm.spdBuhin_Sheet1.Cells(i, spd_Buhin_Col_Select).Value = False

                        '-------------------------------------------------------------------------------------
                        '試作開発管理表（Ver1、Ver2）の場合一桁目が" "（半角スペース）の部品番号が存在する。
                        '   その場合、"-"（半角ハイフン）に置き換える。
                        If StringUtil.IsEmpty(Mid(bVo.BuhinNo.ToString, 1, 1)) Then
                            frm.spdBuhin_Sheet1.Cells(i, spd_Buhin_Col_BuhinNo).Value = "-" & bVo.BuhinNo.ToString.TrimStart
                        Else
                            frm.spdBuhin_Sheet1.Cells(i, spd_Buhin_Col_BuhinNo).Value = bVo.BuhinNo.ToString
                        End If
                        '-------------------------------------------------------------------------------------

                        frm.spdBuhin_Sheet1.Cells(i, spd_Buhin_Col_Insu).Value = bVo.InsuSuryo
                        frm.spdBuhin_Sheet1.Cells(i, spd_Buhin_Col_BuhinName).Value = bVo.BuhinName.ToString

                        If IsNothing(bVo.BuhinNote) Then
                            frm.spdBuhin_Sheet1.Cells(i, spd_Buhin_Col_Note).Value = ""
                        Else
                            frm.spdBuhin_Sheet1.Cells(i, spd_Buhin_Col_Note).Value = bVo.BuhinNote.ToString
                        End If
                        '供給セクション

                        If IsNothing(bVo.KyoukuSection) Then
                            frm.spdBuhin_Sheet1.Cells(i, spd_Buhin_Col_KyoKyu).Value = ""
                        Else
                            frm.spdBuhin_Sheet1.Cells(i, spd_Buhin_Col_KyoKyu).Value = bVo.KyoukuSection.ToString
                        End If

                        '部課コード
                        frm.spdBuhin_Sheet1.Cells(i, spd_Buhin_Col_BukaCode).Value = bVo.ShisakuBukaCode.ToString

                        frm.pbBuhinBango.Value = i + 1
                        i += 1

                    End If
                Next
                'スプレットサイズ再設定
                frm.spdBuhin_Sheet1.RowCount = i

            Catch ex As Exception
                MessageBox.Show(ex.Message, "異常", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End Try
        End Sub

#End Region

#Region "E-BOMから部品構成情報取込処理＆チェックを付ける処理"

        ''' <summary>
        ''' E-BOMから部品構成情報取込処理
        ''' </summary>
        ''' <remarks></remarks>
        Public Function ExecuteBuhinKouseiInfo(ByVal aBlockNo As String, _
                                               ByVal aInstlHinban As String, _
                                               ByVal nRow As Integer) As Boolean

            Dim IsReturn As Boolean = True
            Try
                Dim frm As Frm41KouseiBuhinSelector = _frmKouseiBuhinSelector

                Dim aUserId As String = LoginInfo.Now.UserId
                Dim aKaihatsuFugo As String = frm.cmbKaihatsuFugo.Text

                '構成列のボタンタイプ
                buttonType.ButtonColor = Color.LightBlue
                buttonType.ButtonColor2 = Color.LightBlue
                buttonType.TextColor = Color.Black

                '自給品の有無
                Dim aJikyu As String = ""
                If StringUtil.Equals(frm.cmbJikyuhinUmu.Text, "無") Then
                    aJikyu = "0"
                Else
                    aJikyu = "1"
                End If

                '選択行の次の行のレベルが０なら未展開とみなす。
                Dim nNextRow As Integer = 0
                If nRow = frm.spdBuhin_Sheet1.RowCount - 1 Then
                    nNextRow = nRow
                Else
                    nNextRow = nRow + 1
                End If

                If frm.spdBuhin_Sheet1.Cells(nNextRow, spd_Buhin_Col_Level).Value = "0" Then
                    '=== INSTL品番を親として構成情報取得する。 ===
                    '   パラメータはブロック№とインストル品番で作成したCSVファイルから構成部品情報を取得する。
                    IsReturn = getKouseiData(aKaihatsuFugo, aBlockNo, aInstlHinban, aJikyu, nRow)
                Else
                    '構成展開
                    Dim j As Integer = 0
                    '展開済みの場合行削除する。
                    For i As Integer = nRow + 1 To frm.spdBuhin_Sheet1.RowCount - 1
                        If frm.spdBuhin_Sheet1.Cells(i, spd_Buhin_Col_Level).Value = "0" Then
                            Exit For
                        End If
                        j += 1
                    Next
                    '対象行があれば削除。
                    If j <> 0 Then
                        frm.spdBuhin_Sheet1.RemoveRows(nRow + 1, j)
                    End If
                    '
                    frm.spdBuhin_Sheet1.Cells(nRow, spd_Buhin_Col_Tenkai).CellType = buttonType
                    frm.spdBuhin_Sheet1.Cells(nRow, spd_Buhin_Col_Tenkai).BackColor = Color.LightBlue

                End If

            Catch ex As Exception
                IsReturn = False
                Console.WriteLine("Error:" & ex.Message)
            End Try
            Return IsReturn
        End Function

        ''' <summary>
        ''' E-BOMから部品構成情報取込処理
        ''' </summary>
        ''' <remarks></remarks>
        Public Function ExecuteBuhinKouseiCheck(ByVal aBlockNo As String, _
                                               ByVal aInstlHinban As String, _
                                               ByVal nRow As Integer) As Boolean

            Dim IsReturn As Boolean = True
            Try
                Dim frm As Frm41KouseiBuhinSelector = _frmKouseiBuhinSelector

                'クリア
                frm.spdBuhin_Sheet1.Cells(nRow, spd_Buhin_Col_SelectionMethod).Value = ""

                '自分が０レベルでチェックが付いていて、
                If frm.spdBuhin_Sheet1.Cells(nRow, spd_Buhin_Col_Level).Value = "0" And _
                    frm.spdBuhin_Sheet1.Cells(nRow, spd_Buhin_Col_Select).Value = True Then
                    '次の行が無いなら親品番構成全部対象。
                    If (nRow + 1) > (frm.spdBuhin_Sheet1.RowCount - 1) Then
                        '
                        frm.spdBuhin_Sheet1.Cells(nRow, spd_Buhin_Col_SelectionMethod).Value = HOYOU_NOMAL_ALL
                    Else
                        '次の行が０レベルなら親品番構成全部対象。
                        If frm.spdBuhin_Sheet1.Cells(nRow + 1, spd_Buhin_Col_Level).Value = "0" Then
                            '
                            frm.spdBuhin_Sheet1.Cells(nRow, spd_Buhin_Col_SelectionMethod).Value = HOYOU_NOMAL_ALL
                        End If
                    End If
                End If

                If frm.rbtNomal.Checked = True Then
                    '通常の場合
                    '   自分から次の０レベルの前行までチェックを付ける。
                    For i As Integer = nRow + 1 To frm.spdBuhin_Sheet1.RowCount - 1
                        If frm.spdBuhin_Sheet1.Cells(i, spd_Buhin_Col_Level).Value = "0" Then
                            Exit For
                        End If
                        '０レベルにチェックが付いた場合。
                        If frm.spdBuhin_Sheet1.Cells(nRow, spd_Buhin_Col_Level).Value = "0" Then
                            If frm.spdBuhin_Sheet1.Cells(nRow, spd_Buhin_Col_Select).Value = True Then
                                'チェックを付ける。
                                frm.spdBuhin_Sheet1.Cells(i, spd_Buhin_Col_Select).Value = True
                                '構成状況。
                                frm.spdBuhin_Sheet1.Cells(i, spd_Buhin_Col_SelectionMethod).Value = HOYOU_NOMAL
                                '構成状況
                                frm.spdBuhin_Sheet1.Cells(nRow, spd_Buhin_Col_SelectionMethod).Value = HOYOU_NOMAL
                            Else
                                'チェックを外す。
                                frm.spdBuhin_Sheet1.Cells(i, spd_Buhin_Col_Select).Value = False
                                '構成状況。
                                frm.spdBuhin_Sheet1.Cells(i, spd_Buhin_Col_SelectionMethod).Value = ""
                                '構成状況
                                frm.spdBuhin_Sheet1.Cells(nRow, spd_Buhin_Col_SelectionMethod).Value = ""
                                '背景色を元に戻す。
                                If i / 2 = Int(i / 2) Then
                                    frm.spdBuhin_Sheet1.Cells(i, spd_Buhin_Col_Select).BackColor = Color.WhiteSmoke
                                    frm.spdBuhin_Sheet1.Cells(i, spd_Buhin_Col_BuhinNo).BackColor = Color.WhiteSmoke
                                Else
                                    frm.spdBuhin_Sheet1.Cells(i, spd_Buhin_Col_Select).BackColor = Color.Gainsboro
                                    frm.spdBuhin_Sheet1.Cells(i, spd_Buhin_Col_BuhinNo).BackColor = Color.Gainsboro
                                End If
                            End If
                        Else
                            '０レベル以外で構成を持つ品番の場合。
                            '   下位レベルに構成があるなら。
                            If frm.spdBuhin_Sheet1.Cells(nRow, spd_Buhin_Col_Level).Value < _
                                                frm.spdBuhin_Sheet1.Cells(i, spd_Buhin_Col_Level).Value Then
                                If frm.spdBuhin_Sheet1.Cells(nRow, spd_Buhin_Col_Select).Value = True Then
                                    'チェックを付ける。
                                    frm.spdBuhin_Sheet1.Cells(i, spd_Buhin_Col_Select).Value = True
                                    '構成状況。
                                    frm.spdBuhin_Sheet1.Cells(i, spd_Buhin_Col_SelectionMethod).Value = HOYOU_NOMAL
                                    '構成状況
                                    frm.spdBuhin_Sheet1.Cells(nRow, spd_Buhin_Col_SelectionMethod).Value = HOYOU_NOMAL
                                Else
                                    'チェックを外す。
                                    frm.spdBuhin_Sheet1.Cells(i, spd_Buhin_Col_Select).Value = False
                                    '構成状況。
                                    frm.spdBuhin_Sheet1.Cells(i, spd_Buhin_Col_SelectionMethod).Value = ""
                                    '構成状況
                                    frm.spdBuhin_Sheet1.Cells(nRow, spd_Buhin_Col_SelectionMethod).Value = ""
                                    '背景色を元に戻す。
                                    If i / 2 = Int(i / 2) Then
                                        frm.spdBuhin_Sheet1.Cells(i, spd_Buhin_Col_Select).BackColor = Color.WhiteSmoke
                                        frm.spdBuhin_Sheet1.Cells(i, spd_Buhin_Col_BuhinNo).BackColor = Color.WhiteSmoke
                                    Else
                                        frm.spdBuhin_Sheet1.Cells(i, spd_Buhin_Col_Select).BackColor = Color.Gainsboro
                                        frm.spdBuhin_Sheet1.Cells(i, spd_Buhin_Col_BuhinNo).BackColor = Color.Gainsboro
                                    End If
                                End If
                            Else
                                '下位レベルに構成が無いなら。
                                '   チェックがついているなら選択方法に通常をセット。
                                If frm.spdBuhin_Sheet1.Cells(nRow, spd_Buhin_Col_Select).Value = True Then
                                    '構成状況
                                    frm.spdBuhin_Sheet1.Cells(nRow, spd_Buhin_Col_SelectionMethod).Value = HOYOU_NOMAL
                                Else
                                    'チェックがついていないなら選択方法をクリア。
                                    '構成状況
                                    frm.spdBuhin_Sheet1.Cells(nRow, spd_Buhin_Col_SelectionMethod).Value = ""
                                End If
                                '
                                Exit For
                            End If
                        End If
                    Next
                Else
                    '単品の場合
                    For i As Integer = nRow To frm.spdBuhin_Sheet1.RowCount - 1
                        If frm.spdBuhin_Sheet1.Cells(i, spd_Buhin_Col_Level).Value = "0" Then
                            Exit For
                        End If

                        'Y,E　国内集計コードがＹ，Ｅ，Ｊ、国内集計コードがブランクで海外集計コードがＹ，Ｅ，Ｊの場合。
                        If frm.spdBuhin_Sheet1.Cells(i, spd_Buhin_Col_ShukeiCode).Value = "Y" Or _
                                                        frm.spdBuhin_Sheet1.Cells(i, spd_Buhin_Col_ShukeiCode).Value = "E" Or _
                                                        frm.spdBuhin_Sheet1.Cells(i, spd_Buhin_Col_ShukeiCode).Value = "J" Or _
                            (StringUtil.IsEmpty(frm.spdBuhin_Sheet1.Cells(i, spd_Buhin_Col_ShukeiCode).Value) And _
                                (frm.spdBuhin_Sheet1.Cells(i, spd_Buhin_Col_SiaShukeiCode).Value = "Y" Or _
                                 frm.spdBuhin_Sheet1.Cells(i, spd_Buhin_Col_SiaShukeiCode).Value = "E" Or _
                                 frm.spdBuhin_Sheet1.Cells(i, spd_Buhin_Col_SiaShukeiCode).Value = "J")) Then
                            '０レベルにチェックが付いた場合。
                            If frm.spdBuhin_Sheet1.Cells(nRow, spd_Buhin_Col_Level).Value = "0" Then
                                If frm.spdBuhin_Sheet1.Cells(nRow, spd_Buhin_Col_Select).Value = True Then
                                    'チェックを付ける。
                                    frm.spdBuhin_Sheet1.Cells(i, spd_Buhin_Col_Select).Value = True
                                    '構成状況。
                                    frm.spdBuhin_Sheet1.Cells(i, spd_Buhin_Col_SelectionMethod).Value = HOYOU_TANPIN
                                    '構成状況
                                    frm.spdBuhin_Sheet1.Cells(nRow, spd_Buhin_Col_SelectionMethod).Value = HOYOU_TANPIN
                                Else
                                    'チェックを外す。
                                    frm.spdBuhin_Sheet1.Cells(i, spd_Buhin_Col_Select).Value = False
                                    '構成状況。
                                    frm.spdBuhin_Sheet1.Cells(i, spd_Buhin_Col_SelectionMethod).Value = ""
                                    '構成状況
                                    frm.spdBuhin_Sheet1.Cells(nRow, spd_Buhin_Col_SelectionMethod).Value = ""
                                    '背景色を元に戻す。
                                    If i / 2 = Int(i / 2) Then
                                        frm.spdBuhin_Sheet1.Cells(i, spd_Buhin_Col_Select).BackColor = Color.WhiteSmoke
                                        frm.spdBuhin_Sheet1.Cells(i, spd_Buhin_Col_BuhinNo).BackColor = Color.WhiteSmoke
                                    Else
                                        frm.spdBuhin_Sheet1.Cells(i, spd_Buhin_Col_Select).BackColor = Color.Gainsboro
                                        frm.spdBuhin_Sheet1.Cells(i, spd_Buhin_Col_BuhinNo).BackColor = Color.Gainsboro
                                    End If
                                End If
                            Else
                                '０レベル以外の品番の場合。
                                If frm.spdBuhin_Sheet1.Cells(nRow, spd_Buhin_Col_Select).Value = True Then
                                    '構成状況
                                    frm.spdBuhin_Sheet1.Cells(nRow, spd_Buhin_Col_SelectionMethod).Value = HOYOU_TANPIN
                                Else
                                    '構成状況
                                    frm.spdBuhin_Sheet1.Cells(nRow, spd_Buhin_Col_SelectionMethod).Value = ""
                                End If
                            End If
                        End If
                    Next
                End If
            Catch ex As Exception
                IsReturn = False
                Console.WriteLine("Error:" & ex.Message)
            End Try
            Return IsReturn

        End Function

        ''' <summary>
        ''' E-BOMから部品構成情報取込処理（補用部品表から）
        ''' </summary>
        ''' <remarks></remarks>
        Public Function ExecuteBuhinKouseiCheckShisaku(ByVal aBlockNo As String, _
                                                       ByVal aInstlHinban As String, _
                                                       ByVal nRow As Integer) As Boolean

            Dim IsReturn As Boolean = True
            Try
                Dim frm As Frm41KouseiBuhinSelector = _frmKouseiBuhinSelector

                'クリア
                frm.spdBuhin_Sheet1.Cells(nRow, spd_Buhin_Col_SelectionMethod).Value = ""

                If frm.rbtNomal.Checked = True Then
                Else
                    '０レベル以外の品番の場合。
                    If frm.spdBuhin_Sheet1.Cells(nRow, spd_Buhin_Col_Select).Value = True Then
                        '構成状況
                        frm.spdBuhin_Sheet1.Cells(nRow, spd_Buhin_Col_SelectionMethod).Value = HOYOU_TANPIN_SHISAKU
                    Else
                        '構成状況
                        frm.spdBuhin_Sheet1.Cells(nRow, spd_Buhin_Col_SelectionMethod).Value = ""
                    End If
                End If

            Catch ex As Exception
                IsReturn = False
                Console.WriteLine("Error:" & ex.Message)
            End Try
            Return IsReturn

        End Function

#End Region

#Region "仕様情報の制御"

        ''' <summary>
        ''' 仕様情報の制御
        ''' </summary>
        ''' <remarks></remarks>
        Public Sub ShiyouFormSeigyo(ByVal fEbomOrMbom As String)

            Dim frm As Frm41KouseiBuhinSelector = _frmKouseiBuhinSelector
            Dim fKakuSyuku As String = ""

            'MBOM:試作手配システム
            'EBOM:イーボム
            'EBOM_B:イーボムボタンクリック

            '変更サイズ
            Dim rSize As Integer = 215
            If StringUtil.Equals(fEbomOrMbom, "MBOM") Then
                'MBOMなら拡大（仕様情報を表示しないため）
                fKakuSyuku = "KAKUDAI"
            ElseIf StringUtil.Equals(fEbomOrMbom, "EBOM_B") And _
                StringUtil.Equals(frm.spdBuhinShiyou.Size.Height, 245) Then
                '拡大縮小ボタンクリックかつサイズが小の場合拡大
                fKakuSyuku = "KAKUDAI"
            ElseIf StringUtil.Equals(fEbomOrMbom, "EBOM_B") And _
                Not StringUtil.Equals(frm.spdBuhinShiyou.Size.Height, 245) Then
                '拡大縮小ボタンクリックかつサイズが大の場合縮小
                fKakuSyuku = "SYUKUSYOU"
            Else
                '上記以外は拡大
                fKakuSyuku = "SYUKUSYOU"
            End If

            'サイズを調整する。
            If StringUtil.Equals(fKakuSyuku, "KAKUDAI") Then
                'フォームより開始位置がちいさくならないか判定（開始位置－（変更サイズ＋枠分））
                If 0 < (frm.spdBuhin.Location.Y - (rSize + 35)) Then
                    '
                    frm.spdBuhinShiyou.Size = New Size(frm.spdBuhinShiyou.Size.Width, 30)
                    frm.btnShiyouOnOff.Text = "仕様情報最大化"
                    frm.btnShiyouOnOff.BackColor = Color.Yellow
                    '
                    frm.lblBuhinBango.Location = New Point(frm.lblBuhinBango.Location.X, frm.spdBuhinShiyou.Size.Height + 143)
                    '
                    frm.pbBuhinBango.Location = New Point(frm.pbBuhinBango.Location.X, frm.spdBuhinShiyou.Size.Height + 138)
                    ''
                    'frm.BtnHanei.Location = New Point(frm.BtnHanei.Location.X, frm.spdBuhinShiyou.Size.Height + 138)

                    frm.spdBuhin.Location = New Point(frm.spdBuhin.Location.X, frm.spdBuhinShiyou.Size.Height + 158)
                    frm.spdBuhin.Size = New Size(frm.spdBuhin.Size.Width, frm.Size.Height - frm.spdBuhin.Location.Y - 30)
                    '
                    frm.spdBuhinShiyou_Sheet1.AddSelection(0, 0, 1, 1)
                End If
            Else
                'フォームより大きくならないか判定（開始位置＋（変更サイズ＋枠分））
                If frm.Size.Height > (frm.spdBuhin.Location.Y + (rSize + 35)) Then
                    '
                    frm.spdBuhinShiyou.Size = New Size(frm.spdBuhinShiyou.Size.Width, 30 + rSize)
                    frm.btnShiyouOnOff.Text = "仕様情報最小化"
                    frm.btnShiyouOnOff.BackColor = Color.LightYellow
                    '
                    frm.lblBuhinBango.Location = New Point(frm.lblBuhinBango.Location.X, frm.spdBuhinShiyou.Size.Height + 143)
                    '
                    frm.pbBuhinBango.Location = New Point(frm.pbBuhinBango.Location.X, frm.spdBuhinShiyou.Size.Height + 138)
                    ''
                    'frm.BtnHanei.Location = New Point(frm.BtnHanei.Location.X, frm.spdBuhinShiyou.Size.Height + 138)
                    '
                    frm.spdBuhin.Location = New Point(frm.spdBuhin.Location.X, frm.spdBuhinShiyou.Size.Height + 158)
                    frm.spdBuhin.Size = New Size(frm.spdBuhin.Size.Width, frm.Size.Height - frm.spdBuhin.Location.Y - 30)
                    '
                    frm.spdBuhinShiyou_Sheet1.ClearSelection()
                End If
            End If

        End Sub

#End Region

#Region "部品情報取込処理"

        ''' <summary>
        ''' 部品情報取込処理（バッチを使用しないバージョン）
        ''' </summary>
        ''' <remarks></remarks>
        Public Function ExecuteBuhinFinalInfo() As Boolean

            Dim IsReturn As Boolean = True
            Try
                Dim frm As Frm41KouseiBuhinSelector = _frmKouseiBuhinSelector

                Dim aKaihatsuFugo As String = frm.cmbKaihatsuFugo.Text
                '
                Dim ShiyouJyouhouDao As ShiyouJyouhouDao = New ShiyouJyouhouDaoImpl

                '****** スプレッドの初期化 ******
                m_buhinList = New DispBuhinList(frm)
                m_buhinList.InitView()
                frm.spdBuhin_Sheet1.RowCount = 0
                Dim rowCount As Integer = 0
                '
                Dim r0080Vo As Rhac0080Vo
                Dim SekkeiBlockDao As New SekkeiBlockDaoImpl
                '
                Dim selection As FarPoint.Win.Spread.Model.CellRange() = frm.spdBlock_Sheet1.GetSelections()
                For rowindex As Integer = 0 To selection.Length - 1

                    Dim FinalHinbanListVos As New List(Of FinalHinbanListVo)
                    FinalHinbanListVos = _
                        GetFinalHinbanValues(aKaihatsuFugo, frm.spdBlock_Sheet1.Cells(selection(rowindex).Row, 0).Value)
                    If FinalHinbanListVos.Count > 0 Then

                        'データ件数分行を挿入
                        frm.spdBuhin_Sheet1.AddRows(rowCount, FinalHinbanListVos.Count)
                        '
                        For i As Integer = 0 To FinalHinbanListVos.Count - 1

                            Dim FinalHinban As String = ""

                            '１１桁～１２桁がブランクかつ、色付加コードもブランクの場合
                            If StringUtil.IsEmpty(Mid(FinalHinbanListVos.Item(i).FBuhinNo, 11, 2)) And _
                                StringUtil.IsEmpty(FinalHinbanListVos.Item(i).FukaNo) Then
                                FinalHinban = FinalHinbanListVos.Item(i).FBuhinNo
                                '１１桁～１２桁がブランクかつ、色付加コードはブランクではない場合
                            ElseIf StringUtil.IsEmpty(Mid(FinalHinbanListVos.Item(i).FBuhinNo, 11, 2)) And _
                                    StringUtil.IsNotEmpty(FinalHinbanListVos.Item(i).FukaNo) Then
                                FinalHinban = FinalHinbanListVos.Item(i).FBuhinNo & "##"
                                '１１桁～１２桁がブランクではない場合
                            ElseIf StringUtil.IsNotEmpty(Mid(FinalHinbanListVos.Item(i).FBuhinNo, 11, 2)) Then
                                FinalHinban = FinalHinbanListVos.Item(i).FBuhinNo
                            Else
                                FinalHinban = FinalHinbanListVos.Item(i).FBuhinNo
                            End If

                            ''--- 553から取得 ---
                            Dim aRhac0553 As List(Of Rhac0553Vo) = ShiyouJyouhouDao.GetByRHAC0553Values(FinalHinbanListVos.Item(i).KaihatsuFugo, _
                                                                                    FinalHinban)
                            ''--- 532から注記（CHUKI_KIJUTSU）取得 ---
                            Dim aRhac0532 As List(Of Rhac0532Vo) = ShiyouJyouhouDao.GetByRHAC0532Values(FinalHinban)
                            ''--- 533から図面集合NOTE（ZUMEN_NOTE）取得 ---
                            Dim aRhac0533 As List(Of Rhac0533Vo) = ShiyouJyouhouDao.GetByRHAC0533Values(FinalHinban)

                            '
                            'ブロック№
                            frm.spdBuhin_Sheet1.Cells(rowCount, spd_Buhin_Col_BlockNo).Value = FinalHinbanListVos.Item(i).BlockNo

                            '部課コード
                            '設計課更新画面からの呼出しの場合、最新設計課に置き換える
                            r0080Vo = SekkeiBlockDao.FindTantoBushoByBlock(aKaihatsuFugo, FinalHinbanListVos.Item(i).BlockNo)
                            If r0080Vo IsNot Nothing Then
                                frm.spdBuhin_Sheet1.Cells(rowCount, spd_Buhin_Col_BukaCode).Value = r0080Vo.TantoBusho
                            End If

                            'LV
                            frm.spdBuhin_Sheet1.Cells(rowCount, spd_Buhin_Col_Level).Value = 0
                            '国内集計コード、海外集計コード、部品名称、員数、ＮＯＴＥ
                            Dim aShukeiCode As String = ""
                            Dim aSiaShukeiCode As String = ""
                            Dim aBuhinName As String = ""
                            Dim aInsu As String = "1"
                            Dim aNote As String = ""
                            '構成情報
                            If StringUtil.IsNotEmpty(aRhac0553) Then
                                If aRhac0553.Count > 0 Then
                                    aShukeiCode = aRhac0553.Item(0).ShukeiCode
                                    aSiaShukeiCode = aRhac0553.Item(0).SiaShukeiCode
                                    aInsu = 1   '1固定で良いのかな？
                                End If
                            End If
                            '部品属性情報
                            If StringUtil.IsNotEmpty(aRhac0533) Then
                                If aRhac0533.Count > 0 Then
                                    aBuhinName = aRhac0533.Item(0).BuhinName
                                    aNote = aRhac0533.Item(0).ZumenNote.TrimEnd
                                    aInsu = 1   '1固定で良いのかな？
                                    '532が優先
                                    If aRhac0532.Count > 0 Then
                                        aBuhinName = aRhac0532.Item(0).BuhinName
                                        aNote = aRhac0532.Item(0).ChukiKijutsu
                                        aShukeiCode = aRhac0532.Item(0).ShukeiCode
                                        aSiaShukeiCode = aRhac0532.Item(0).SiaShukeiCode
                                        aInsu = 1   '1固定で良いのかな？
                                    End If
                                Else
                                    If aRhac0532.Count > 0 Then
                                        aBuhinName = aRhac0532.Item(0).BuhinName
                                        aNote = aRhac0532.Item(0).ChukiKijutsu
                                        aShukeiCode = aRhac0532.Item(0).ShukeiCode
                                        aSiaShukeiCode = aRhac0532.Item(0).SiaShukeiCode
                                        aInsu = 1   '1固定で良いのかな？
                                    End If
                                End If
                            End If

                            '国内集計コード
                            frm.spdBuhin_Sheet1.Cells(rowCount, spd_Buhin_Col_ShukeiCode).Value = aShukeiCode
                            '海外集計コード
                            frm.spdBuhin_Sheet1.Cells(rowCount, spd_Buhin_Col_SiaShukeiCode).Value = aSiaShukeiCode
                            '構成表示ボタン
                            frm.spdBuhin_Sheet1.Cells(rowCount, spd_Buhin_Col_Tenkai).CellType = buttonType
                            frm.spdBuhin_Sheet1.Cells(rowCount, spd_Buhin_Col_Tenkai).BackColor = Color.LightBlue
                            '選択チェックボックス
                            frm.spdBuhin_Sheet1.Cells(rowCount, spd_Buhin_Col_Select).Value = False
                            '部品番号
                            frm.spdBuhin_Sheet1.Cells(rowCount, spd_Buhin_Col_BuhinNo).Value = FinalHinban
                            '員数
                            frm.spdBuhin_Sheet1.Cells(rowCount, spd_Buhin_Col_Insu).Value = aInsu
                            '部品名称
                            frm.spdBuhin_Sheet1.Cells(rowCount, spd_Buhin_Col_BuhinName).Value = aBuhinName
                            '部品ノート
                            frm.spdBuhin_Sheet1.Cells(rowCount, spd_Buhin_Col_Note).Value = aNote
                            '部品構成
                            frm.spdBuhin_Sheet1.Cells(rowCount, spd_Buhin_Col_SelectionMethod).Value = ""
                            '
                            rowCount += 1
                        Next
                    End If

                Next

            Catch ex As Exception
                IsReturn = False
                Console.WriteLine("Error:" & ex.Message)
            End Try
            Return IsReturn
        End Function

#End Region

#Region "部品3Dデータ有無チェック"

        ''' <summary>
        ''' 部品3Dデータ有無チェック
        ''' </summary>
        ''' <param name="KaihatsuFugo">開発符号</param>        
        ''' <param name="iRow">処理対象の行(指定しないと全部が対象）</param>
        ''' <remarks></remarks>
        Sub CheckBuhinIn3Ddata(ByVal KaihatsuFugo As String, Optional ByVal iRow As Integer = -1)

            '   作業用スプレッドシートの設定
            Dim sht As FarPoint.Win.Spread.SheetView = _frmKouseiBuhinSelector.spdBuhin_Sheet1

            '   行指定
            '
            Dim Row_Start As Integer = 0
            Dim Row_End As Integer = sht.Rows.Count - 1

            '   引数で対象行を指定した場合、ループ開始・終了の行を固定化する
            If iRow >= 0 And iRow < sht.Rows.Count Then
                Row_Start = iRow
                Row_End = iRow
            End If

            '   現在表示されている、SpreadのBlock番号全てを取得
            Dim Bvo As New List(Of XVLExistVo)

            Dim bufBlock As String = vbNullString

            Dim blockNo As New StringBuilder
            blockNo.Remove(0, blockNo.Length)

            '行数分ループを行う
            For aRow As Integer = Row_Start To Row_End

                Dim voBuf As New XVLExistVo

                voBuf.RowIndex = aRow
                voBuf.BlockNo = sht.GetValue(aRow, Me.spd_Buhin_Col_BlockNo).ToString

                'ブロックバッファーとブロック番号が変わった場合の判定
                If bufBlock <> voBuf.BlockNo Then

                    If blockNo.Length = 0 Then
                        '初期値の場合はブロック№をそのまま格納
                        blockNo.Append(voBuf.BlockNo.Trim)
                    Else
                        '最終的にブロック番号はIn句で発行するので、カンマ区切りの文字列生成できるようにしておく
                        blockNo.Append(",")
                        blockNo.Append(voBuf.BlockNo.Trim)
                    End If
                    bufBlock = voBuf.BlockNo
                End If

                voBuf.BuhinNo = sht.GetValue(aRow, Me.spd_Buhin_Col_BuhinNo).ToString.Trim

                Bvo.Add(voBuf)

            Next

            'RHAC2270から、開発符号・ブロックで内容を取得（ブロック番号はin句にて取得する方向）
            Dim vo2270 As List(Of Rhac2270XVLVo)
            vo2270 = GetXVLFileNameS(KaihatsuFugo, blockNo.ToString)

            For i As Integer = 0 To Bvo.Count - 1

                Dim isExist As Boolean = False
                Dim key As String = Bvo(i).BlockNo & Bvo(i).BuhinNo

                For j As Integer = 0 To vo2270.Count - 1

                    Dim bufKey As String = vo2270(j).BlockNo.Trim & vo2270(j).BuhinNo.Trim

                    If key <> bufKey Then Continue For

                    Bvo(i).IsExistXVL = "○"
                    isExist = True
                    Exit For

                Next

                If Not isExist Then
                    Bvo(i).IsExistXVL = "×"
                End If

            Next

            For i As Integer = 0 To Bvo.Count - 1

                Dim row As Integer = Bvo(i).RowIndex
                Dim mark As String = Bvo(i).IsExistXVL

                sht.Cells(row, spd_Buhin_Col_3DUMU).Value = mark

            Next

        End Sub


        ''' <summary>
        ''' XVL情報の取得
        ''' </summary>
        ''' <param name="KaihatsuFugo"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Private Function GetXVLFileNameS(ByVal KaihatsuFugo As String, ByVal blockNo As String) As List(Of Rhac2270XVLVo)
            Dim Rhac2270Vos As New List(Of Rhac2270XVLVo)

            Try
                If StringUtil.IsNotEmpty(KaihatsuFugo) Then
                    Dim Rhac2270Dao As Rhac2270XVLDao = New Rhac2270XVLDaoImpl
                    Rhac2270Vos = Rhac2270Dao.GetXVLFileNameS(KaihatsuFugo, blockNo)
                End If
            Catch ex As Exception
                MessageBox.Show(ex.Message, "異常", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End Try
            Return Rhac2270Vos
        End Function

#End Region

#Region "構成部品情報を取得"

        ''' <summary>
        ''' 構成部品情報を取得する。
        ''' </summary>
        ''' <remarks></remarks>
        Public Function getKouseiData(ByVal aKaihatsuFugo As String, _
                                      ByVal aBlockNo As String, _
                                      ByVal aInstlHinban As String, _
                                      ByVal aJikyu As String, _
                                      ByVal nRow As Integer) As Boolean

            Dim frm As Frm41KouseiBuhinSelector = _frmKouseiBuhinSelector

            frm.pbBuhinBango.Minimum = 1
            frm.pbBuhinBango.Value = 1

            Dim IsReturn As Boolean = False
            Try

                '部品構成取得
                '   手配帳編集画面の部品展開ロジックを使用
                '   手配帳編集画面から当機能へ持ってくるかは編集画面の改修内容を考慮して再検討する。
                Dim Shisakudate As ShisakuDate = New ShisakuDate
                Dim buhinstruct As New TehaichoEditBuhinStructure(_YosanEventCode, "ListCodeDummy", aBlockNo, "BukaCodeDummy", Shisakudate)
                Dim newBuhinMatrix As BuhinKoseiMatrix = buhinstruct.GetKouseiMatrix(aInstlHinban, "", 0, aKaihatsuFugo)

                If newBuhinMatrix Is Nothing OrElse newBuhinMatrix.Records.Count = 0 Then

                    Return False

                End If

                Dim intRowCnt As Integer = nRow + 1
                Dim intDataCnt As Integer = 0
                frm.pbBuhinBango.Maximum = newBuhinMatrix.Records.Count + 1 '１はヘッダー部の行数

                '構成分行を追加
                '   子部品が無い場合は処理を終了する。
                If StringUtil.Equals(newBuhinMatrix.Records.Count, 1) Then
                    Return True
                End If
                frm.spdBuhin_Sheet1.AddRows(nRow + 1, newBuhinMatrix.Records.Count - 1)  '構成分行を追加

                '構成展開
                '構成列のボタンタイプ
                buttonType.ButtonColor = Color.Blue
                buttonType.ButtonColor2 = Color.Blue
                buttonType.TextColor = Color.White
                frm.spdBuhin_Sheet1.Cells(nRow, spd_Buhin_Col_Tenkai).CellType = buttonType
                frm.spdBuhin_Sheet1.Cells(nRow, spd_Buhin_Col_Tenkai).BackColor = Color.Blue
                frm.spdBuhin_Sheet1.Cells(nRow, spd_Buhin_Col_Tenkai).ForeColor = Color.White
                '親品番のチェック状況を保持する。
                Dim fKousei As Boolean = frm.spdBuhin_Sheet1.Cells(nRow, spd_Buhin_Col_Select).Value

                'frm.spdBuhin_Sheet1.Cells(nRow, 4).BackColor = Color.Blue
                'frm.spdBuhin_Sheet1.Cells(nRow, 4).Text = "On"
                'frm.spdBuhin_Sheet1.Cells(nRow, 4).ForeColor = Color.White

                '部課コード
                Dim r0080Vo As Rhac0080Vo
                Dim SekkeiBlockDao As New SekkeiBlockDaoImpl
                r0080Vo = SekkeiBlockDao.FindTantoBushoByBlock(aKaihatsuFugo, aBlockNo)

                '構成展開
                For Each i As Integer In newBuhinMatrix.GetInputRowIndexes

                    '自分は除く
                    If Not StringUtil.Equals(newBuhinMatrix(i).YosanLevel, 0) Then
                        '値を確保
                        Dim aShukeiCode As String = newBuhinMatrix(i).YosanShukeiCode.TrimEnd
                        Dim aSiaShukeiCode As String = newBuhinMatrix(i).YosanSiaShukeiCode.TrimEnd
                        Dim aLevel As String = newBuhinMatrix(i).YosanLevel
                        Dim aBuhinNo As String = newBuhinMatrix(i).YosanBuhinNo.TrimEnd
                        Dim aBuhinName As String = newBuhinMatrix(i).YosanBuhinName.TrimEnd
                        'Dim aBuhinNote As String = newBuhinMatrix(i).yosanBuhinNote.TrimEnd
                        Dim aKyoukuSection As String = newBuhinMatrix(i).YosanKyoukuSection.TrimEnd

                        '=== 明細 ===
                        'If StringUtil.Equals(aBlockNo, newBuhinMatrix.Record(i).ShisakuBlockNo) Then

                        '自給品有無＝無の場合　国内または国外集計コードＪは読み飛ばす。
                        If aJikyu = "1" Or _
                            aJikyu = "0" And aShukeiCode <> "" And aShukeiCode <> "J" Or _
                            aJikyu = "0" And aShukeiCode = "" And aSiaShukeiCode <> "" And aSiaShukeiCode <> "J" Then

                            '員数を求める。

                            Dim lstInsu As List(Of Integer) = newBuhinMatrix.GetInputInsuColumnIndexes
                            If lstInsu.Count <= 0 Then
                                Continue For
                            End If

                            Dim getInsu As Integer = 0

                            For k As Integer = 0 To lstInsu.Count - 1
                                '号車員数ゲット
                                If newBuhinMatrix.InsuSuryo(i, lstInsu(k)) <> 0 Then
                                    getInsu = newBuhinMatrix.InsuSuryo(i, lstInsu(k))
                                    Exit For
                                End If
                            Next

                            Dim strInsu As String = ""
                            If StringUtil.Equals(getInsu, -1) Then
                                strInsu = "**"
                            Else
                                strInsu = CStr(getInsu)
                            End If

                            '余白を求める
                            Dim strYohaku As String = "                              "

                            '部課コード
                            If r0080Vo IsNot Nothing Then
                                frm.spdBuhin_Sheet1.Cells(intRowCnt, spd_Buhin_Col_BukaCode).Value = r0080Vo.TantoBusho
                            End If

                            'ブロック№
                            'frm.spdBuhin_Sheet1.Cells(intRowCnt, 0).Value = newBuhinMatrix.Record(i).ShisakuBlockNo
                            frm.spdBuhin_Sheet1.Cells(intRowCnt, spd_Buhin_Col_BlockNo).Value = aBlockNo
                            'LV
                            frm.spdBuhin_Sheet1.Cells(intRowCnt, spd_Buhin_Col_Level).Value = aLevel
                            '国内集計コード
                            frm.spdBuhin_Sheet1.Cells(intRowCnt, spd_Buhin_Col_ShukeiCode).Value = aShukeiCode
                            '海外集計コード
                            frm.spdBuhin_Sheet1.Cells(intRowCnt, spd_Buhin_Col_SiaShukeiCode).Value = aSiaShukeiCode
                            '構成展開
                            frm.spdBuhin_Sheet1.Cells(intRowCnt, spd_Buhin_Col_Tenkai).CellType = textType
                            frm.spdBuhin_Sheet1.Cells(intRowCnt, spd_Buhin_Col_Tenkai).Locked = True
                            frm.spdBuhin_Sheet1.Cells(intRowCnt, spd_Buhin_Col_Tenkai).BackColor = Color.LightBlue
                            frm.spdBuhin_Sheet1.Cells(intRowCnt, spd_Buhin_Col_Tenkai).ForeColor = Color.Black
                            If i = newBuhinMatrix.Records.Count - 1 Then
                                frm.spdBuhin_Sheet1.Cells(intRowCnt, spd_Buhin_Col_Tenkai).Text = "└"
                            Else
                                frm.spdBuhin_Sheet1.Cells(intRowCnt, spd_Buhin_Col_Tenkai).Text = "│"
                            End If

                            '通常の場合
                            If frm.rbtNomal.Checked = True Then
                                '選択チェックボックス
                                frm.spdBuhin_Sheet1.Cells(intRowCnt, spd_Buhin_Col_Select).Value = fKousei '親品番と同等の☑を付ける。
                                '
                                If fKousei = True Then
                                    '構成状況。
                                    frm.spdBuhin_Sheet1.Cells(intRowCnt, spd_Buhin_Col_SelectionMethod).Value = HOYOU_NOMAL
                                End If
                                'X,A　国内集計コードがＸ，Ａか、国内集計コードがブランクで海外集計コードがＸ，Ａの場合。
                                If aShukeiCode = "X" Or aShukeiCode = "A" Or _
                                    (StringUtil.IsEmpty(aShukeiCode) And _
                                        (aSiaShukeiCode = "X" Or aSiaShukeiCode = "A")) Then
                                    'チェックボックスは使用できる。
                                    frm.spdBuhin_Sheet1.Cells(intRowCnt, spd_Buhin_Col_Select).Locked = False
                                Else
                                    'チェックボックスは使用できない。
                                    frm.spdBuhin_Sheet1.Cells(intRowCnt, spd_Buhin_Col_Select).Locked = True
                                End If
                            End If
                            '単品の場合
                            If frm.rbtTanpin.Checked = True Then
                                'Y,E　国内集計コードがＹ，Ｅ，Ｊか、国内集計コードがブランクで海外集計コードがＹ，Ｅ，Ｊの場合。
                                If aShukeiCode = "Y" Or _
                                    aShukeiCode = "E" Or _
                                    aShukeiCode = "J" Or _
                                    (StringUtil.IsEmpty(aShukeiCode) And _
                                        (aSiaShukeiCode = "Y" Or aSiaShukeiCode = "E" Or aSiaShukeiCode = "J")) Then
                                    '選択チェックボックス
                                    frm.spdBuhin_Sheet1.Cells(intRowCnt, spd_Buhin_Col_Select).Value = fKousei '親品番と同等の☑を付ける。
                                    If fKousei = True Then
                                        '構成状況。

#If 1 Then

                                        frm.spdBuhin_Sheet1.Cells(intRowCnt, spd_Buhin_Col_SelectionMethod).Value = HOYOU_TANPIN
#Else
’
’  列が固定値だったので、定数化
’
                                        frm.spdBuhin_Sheet1.Cells(intRowCnt, 10).Value = HOYOU_TANPIN
#End If
                                    End If
                                    'チェックボックスは使用できる。
                                    frm.spdBuhin_Sheet1.Cells(intRowCnt, spd_Buhin_Col_Select).Locked = False
                                Else
                                    '選択チェックボックス
                                    frm.spdBuhin_Sheet1.Cells(intRowCnt, spd_Buhin_Col_Select).Value = False 'チェックはつかない。
                                    '構成状況。
                                    frm.spdBuhin_Sheet1.Cells(intRowCnt, spd_Buhin_Col_SelectionMethod).Value = ""
                                    'チェックボックスは使用できない。
                                    frm.spdBuhin_Sheet1.Cells(intRowCnt, spd_Buhin_Col_Select).Locked = True
                                End If
                            End If

                            '下記の処理は一旦保留
                            ''選択チェックボックス
                            'frm.spdBuhin_Sheet1.Cells(intRowCnt, 5).Value = fKousei '親品番と同等の☑を付ける。
                            'If frm.rbtNomal.Checked = True Then
                            '    frm.spdBuhin_Sheet1.Cells(intRowCnt, 5).Locked = True
                            'Else
                            '    frm.spdBuhin_Sheet1.Cells(intRowCnt, 5).Locked = False
                            'End If

                            '部品番号レベル毎に頭に半角空文字セット
                            frm.spdBuhin_Sheet1.Cells(intRowCnt, spd_Buhin_Col_BuhinNo).Value = Left(strYohaku, CInt(aLevel)) + _
                                                                            aBuhinNo
                            '員数
                            frm.spdBuhin_Sheet1.Cells(intRowCnt, spd_Buhin_Col_Insu).Value = strInsu
                            '部品名称
                            frm.spdBuhin_Sheet1.Cells(intRowCnt, spd_Buhin_Col_BuhinName).Value = aBuhinName
                            '部品ノート
                            frm.spdBuhin_Sheet1.Cells(intRowCnt, spd_Buhin_Col_Note).Value = ""
                            ''部品構成
                            'frm.spdBuhin_Sheet1.Cells(intRowCnt, 10).Value = "KO"

                            '供給セクション
                            frm.spdBuhin_Sheet1.Cells(intRowCnt, spd_Buhin_Col_KyoKyu).Value = aKyoukuSection

                            intRowCnt += 1

                            'R構成を取得する。
                            If aShukeiCode = "R" Or aSiaShukeiCode = "R" Then

                                '    '部品番号、試作区分、レベル、Instl品番か？（True/False）
                                '    Dim newBuhinMatrixRkousei As BuhinKoseiMatrix = _
                                '        buhinstruct.GetKouseiMatrix(newBuhinMatrix.Record(i).BuhinNo, "", _
                                '                                    newBuhinMatrix.Record(i).Level, False)

                                '    If newBuhinMatrixRkousei Is Nothing OrElse newBuhinMatrixRkousei.Records.Count = 0 Then
                                '    Else

                                '        '構成分行を追加
                                '        frm.spdBuhin_Sheet1.AddRows(intRowCnt, newBuhinMatrixRkousei.Records.Count)  '構成分行を追加

                                '        '構成展開
                                '        For j As Integer = 0 To newBuhinMatrixRkousei.Records.Count - 1


                                '            '=== 明細 ===

                                '            '自給品有無＝無の場合　国内または国外集計コードＪは読み飛ばす。
                                '            If aJikyu = "1" Or _
                                '                aJikyu = "0" And newBuhinMatrixRkousei.Record(j).ShukeiCode <> "" _
                                '                                And newBuhinMatrixRkousei.Record(j).ShukeiCode <> "J" Or _
                                '                aJikyu = "0" And newBuhinMatrixRkousei.Record(j).ShukeiCode = "" _
                                '                                And newBuhinMatrixRkousei.Record(j).SiaShukeiCode <> "J" Then

                                '                '員数を求める。

                                '                Dim lstInsuRkousei As List(Of Integer) = newBuhinMatrixRkousei.GetInputInsuColumnIndexes
                                '                If lstInsuRkousei.Count <= 0 Then
                                '                    Continue For
                                '                End If

                                '                Dim getInsuRkousei As Integer = 0

                                '                For k As Integer = 0 To lstInsuRkousei.Count - 1
                                '                    '号車員数ゲット
                                '                    If newBuhinMatrixRkousei.InsuSuryo(j, lstInsuRkousei(k)) <> 0 Then
                                '                        getInsuRkousei = newBuhinMatrixRkousei.InsuSuryo(j, lstInsuRkousei(k))
                                '                        Exit For
                                '                    End If
                                '                Next

                                '                Dim strInsuRkousei As String = ""
                                '                If StringUtil.Equals(getInsuRkousei, -1) Then
                                '                    strInsuRkousei = "**"
                                '                Else
                                '                    strInsuRkousei = CStr(getInsuRkousei)
                                '                End If

                                '                'ブロック№
                                '                frm.spdBuhin_Sheet1.Cells(intRowCnt, 0).Value = aBlockNo
                                '                'LV
                                '                frm.spdBuhin_Sheet1.Cells(intRowCnt, 1).Value = newBuhinMatrixRkousei.Record(j).Level
                                '                '国内集計コード
                                '                frm.spdBuhin_Sheet1.Cells(intRowCnt, 2).Value = newBuhinMatrixRkousei.Record(j).ShukeiCode
                                '                '海外集計コード
                                '                frm.spdBuhin_Sheet1.Cells(intRowCnt, 3).Value = newBuhinMatrixRkousei.Record(j).SiaShukeiCode
                                '                '構成展開
                                '                frm.spdBuhin_Sheet1.Cells(intRowCnt, 4).CellType = textType
                                '                frm.spdBuhin_Sheet1.Cells(intRowCnt, 4).Locked = True
                                '                frm.spdBuhin_Sheet1.Cells(intRowCnt, 4).BackColor = Color.LightBlue
                                '                frm.spdBuhin_Sheet1.Cells(intRowCnt, 4).ForeColor = Color.Black
                                '                If j = newBuhinMatrixRkousei.Records.Count - 1 Then
                                '                    frm.spdBuhin_Sheet1.Cells(intRowCnt, 4).Text = "└"
                                '                Else
                                '                    frm.spdBuhin_Sheet1.Cells(intRowCnt, 4).Text = "│"
                                '                End If
                                '                '選択チェックボックス
                                '                frm.spdBuhin_Sheet1.Cells(intRowCnt, 5).Value = False
                                '                '部品番号レベル毎に頭に半角空文字セット
                                '                frm.spdBuhin_Sheet1.Cells(intRowCnt, 6).Value = Left(strYohaku, CInt(newBuhinMatrixRkousei.Record(j).Level)) + _
                                '                                                                newBuhinMatrixRkousei.Record(j).BuhinNo
                                '                '員数
                                '                frm.spdBuhin_Sheet1.Cells(intRowCnt, 7).Value = strInsuRkousei
                                '                '部品名称
                                '                frm.spdBuhin_Sheet1.Cells(intRowCnt, 8).Value = newBuhinMatrixRkousei.Record(j).BuhinName
                                '                '部品ノート
                                '                frm.spdBuhin_Sheet1.Cells(intRowCnt, 9).Value = newBuhinMatrixRkousei.Record(j).BuhinNote
                                '                '部品構成
                                '                frm.spdBuhin_Sheet1.Cells(intRowCnt, 10).Value = "rKO"

                                '                intRowCnt += 1

                                '            Else
                                '                frm.spdBuhin_Sheet1.RemoveRows(intRowCnt, 1)
                                '            End If

                                '        Next



                                '    End If


                            End If


                        Else
                            frm.spdBuhin_Sheet1.RemoveRows(intRowCnt, 1)
                        End If
                        'End If

                        intDataCnt += 1
                        '進度バー
                        frm.pbBuhinBango.Value = intDataCnt

                    End If

                Next

                '最終行が集計コードJの場合があるのでここでもセットする。
                '構成展開
                frm.spdBuhin_Sheet1.Cells(intRowCnt - 1, spd_Buhin_Col_Tenkai).Locked = True
                frm.spdBuhin_Sheet1.Cells(intRowCnt - 1, spd_Buhin_Col_Tenkai).BackColor = Color.LightBlue
                frm.spdBuhin_Sheet1.Cells(intRowCnt - 1, spd_Buhin_Col_Tenkai).ForeColor = Color.Black
                frm.spdBuhin_Sheet1.Cells(intRowCnt - 1, spd_Buhin_Col_Tenkai).Text = "└"

                'XVLファイルの有無判定を行い、画面へ表示する
                CheckBuhinIn3Ddata(aKaihatsuFugo)

            Catch ex As Exception
                Console.WriteLine("Error:" & ex.Message)
            End Try
            Return IsReturn

            '2013/11/25 対応
            '下記はCSVより構成を持ってくるロジック
            '   E-BOMから構成を取得するようにしたので下記のロジックはコメントアウトしておく。

            'Dim IsReturn As Boolean = False
            'Try
            '    '*** E-BOM部品情報 ***
            '    Dim aPath As String = "C:\Program Files\GS-Tool\CSV\"
            '    Dim aUserId As String = LoginInfo.Now.UserId
            '    Dim aFileName As String = aUserId & aKaihatsuFugo & "PL.csv"

            '    '--- データ件数 ---
            '    Dim aMaxLineCnt As Integer = GetLinesOfTextFile(aPath & aFileName, False, aJikyu, aBlockNo, aInstlHinban)
            '    frm.ProgressBar1.Maximum = aMaxLineCnt + 1 '１はヘッダー部の行数

            '    '--- データ読み込み ---
            '    'ファイル名と文字エンコードを指定してパーサを実体化
            '    Dim txtParser As TextFieldParser = New TextFieldParser(aPath & aFileName, System.Text.Encoding.GetEncoding("Shift-JIS"))
            '    '内容は区切り文字形式
            '    txtParser.TextFieldType = FieldType.Delimited
            '    'デリミタはカンマ
            '    txtParser.SetDelimiters(",")
            '    'ファイルの終わりまで一行ずつ処理
            '    _MassStart = 0
            '    _MassCount = 0
            '    _CostStart = 0
            '    _CostCount = 0
            '    _GoshaStart = 0
            '    Dim aIsDataSet As Boolean = True 'データ設定(True:設定,False:スキップ)
            '    Dim aLineCnt As Integer = 0

            '    Dim intRowCnt As Integer = nRow + 1
            '    Dim intDataCnt As Integer = 0

            '    '****** データテーブルの初期化 ******
            '    Dim aDtGosya As DataTable = initDataOfGosya()
            '    Dim aDtBuhin As DataTable = initDataOfEbom()
            '    'aDtBuhin = initDataOfShisaku(aDtBuhin)

            '    '****** スプレッドの初期化 ******
            '    m_buhinList = New DispBuhinList(frm)
            '    m_buhinList.InitView()

            '    'frm.spdBuhin_Sheet1.RowCount = 0    '最初に全行削除
            '    'frm.spdBuhin_Sheet1.RemoveRows(nRow, 1) '選択行のみ削除
            '    frm.spdBuhin_Sheet1.AddRows(nRow + 1, aMaxLineCnt - 1)  '構成分行を追加

            '    'frm.spdBuhin_Sheet1.RowCount = aMaxLineCnt

            '    While Not txtParser.EndOfData
            '        '一行を読み込んで配列に結果を受け取る
            '        Dim splittedResult As String() = txtParser.ReadFields()
            '        If aLineCnt = 0 Then
            '            '=== ヘッダ ===
            '            Dim aCol As Integer = 0
            '            For Each item In splittedResult
            '                If item = "Mass" Then
            '                    If _MassStart = 0 Then
            '                        _MassStart = aCol
            '                    End If
            '                    _MassCount += 1
            '                End If
            '                If item = "Cost" Then
            '                    If _CostStart = 0 Then
            '                        _CostStart = aCol
            '                    End If
            '                    _CostCount += 1
            '                End If
            '                If _GoshaStart = 0 Then
            '                    If 0 <= item.IndexOf("/") Then
            '                        _GoshaStart = aCol
            '                    End If
            '                End If
            '                aCol += 1
            '            Next
            '            aDtGosya = SetDataOfGosya(aDtGosya, splittedResult)
            '            _GoshaCount = aDtGosya.Rows.Count
            '            aDtBuhin = initDataOfGosyaCol(aDtBuhin, _GoshaCount)
            '        Else
            '            '=== 明細 ===
            '            If StringUtil.Equals(aBlockNo, splittedResult(columnBLOCK_NO).ToString()) And _
            '                0 <= splittedResult(columnKOUSEI).ToString().IndexOf(aInstlHinban) Then
            '                If splittedResult(columnLEVEL).ToString() = "0" Or _
            '                    splittedResult(columnLEVEL).ToString() >= "a" And splittedResult(columnLEVEL).ToString() <= "z" Then
            '                Else

            '                    '自給品有無＝無の場合　国内または国外集計コードＪは読み飛ばす。
            '                    If aJikyu = "1" Or _
            '                        aJikyu = "0" And splittedResult(columnSHUKEI_CODE).ToString() <> "" _
            '                                        And splittedResult(columnSHUKEI_CODE).ToString() <> "J" Or _
            '                        aJikyu = "0" And splittedResult(columnSHUKEI_CODE).ToString() = "" _
            '                                        And splittedResult(columnSIA_SHUKEI_CODE).ToString() <> "J" Then

            '                        ' 追加データ作成
            '                        Dim wNewRow As DataRow = aDtBuhin.NewRow
            '                        Dim aAddData As DataRow = setLineOfData(aKaihatsuFugo, aLineCnt, wNewRow, splittedResult)

            '                        '員数を求める。
            '                        Dim intGousyaStart As Integer = columnintGousyaStart
            '                        Dim intInsu As Integer = 0
            '                        Dim strInsu As String = ""
            '                        For i As Integer = intGousyaStart To intGousyaStart + GoshaCount - 1
            '                            If StringUtil.IsNotEmpty(aAddData(i)) Then
            '                                If aAddData(i) > intInsu Then
            '                                    intInsu = aAddData(i)
            '                                End If
            '                            Else
            '                                strInsu = "**"
            '                                Exit For
            '                            End If
            '                        Next
            '                        If Not StringUtil.Equals(strInsu, "**") Then
            '                            strInsu = CStr(intInsu)
            '                        End If

            '                        '余白を求める
            '                        Dim intKetaBuhinNo As Integer = 15
            '                        Dim intKetaBuhinName As Integer = 30
            '                        Dim intKetaInsu As Integer = 2
            '                        Dim strYohaku As String = "                              "

            '                        intKetaBuhinNo = intKetaBuhinNo - Len(aAddData(basicBuhinNo))
            '                        intKetaBuhinName = intKetaBuhinName - Len(aAddData(basicBuhinName))
            '                        intKetaInsu = intKetaInsu - Len(CStr(strInsu))

            '                        'ブロック№
            '                        frm.spdBuhin_Sheet1.Cells(intRowCnt, 0).Value = aAddData(basicBlockNo)
            '                        'LV
            '                        frm.spdBuhin_Sheet1.Cells(intRowCnt, 1).Value = aAddData(basicLevel)
            '                        '国内集計コード
            '                        frm.spdBuhin_Sheet1.Cells(intRowCnt, 2).Value = aAddData(basicShukeiCode)
            '                        '海外集計コード
            '                        frm.spdBuhin_Sheet1.Cells(intRowCnt, 3).Value = aAddData(basicSiaShukeiCode)
            '                        '選択チェックボックス
            '                        frm.spdBuhin_Sheet1.Cells(intRowCnt, 4).Value = True
            '                        '部品番号レベル毎に頭に半角空文字セット
            '                        frm.spdBuhin_Sheet1.Cells(intRowCnt, 5).Value = Left(strYohaku, CInt(aAddData(basicLevel))) + _
            '                                                                        aAddData(basicBuhinNo)
            '                        '員数
            '                        frm.spdBuhin_Sheet1.Cells(intRowCnt, 6).Value = strInsu
            '                        '部品名称
            '                        frm.spdBuhin_Sheet1.Cells(intRowCnt, 7).Value = aAddData(basicBuhinName)
            '                        '部品ノート
            '                        frm.spdBuhin_Sheet1.Cells(intRowCnt, 8).Value = aAddData(basicBuhinNote)
            '                        '部品構成
            '                        frm.spdBuhin_Sheet1.Cells(intRowCnt, 9).Value = aAddData(basicBuhinKousei)

            '                        intRowCnt += 1
            '                        intDataCnt += 1
            '                        '進度バー
            '                        frm.ProgressBar1.Value = intDataCnt

            '                    End If
            '                End If
            '            End If

            '        End If
            '        aLineCnt += 1
            '    End While

            '    '最後に閉じる
            '    txtParser.Close()

            '    IsReturn = True
            'Catch ex As Exception
            '    Console.WriteLine("Error:" & ex.Message)
            'End Try
            'Return IsReturn


        End Function

#End Region

#Region "データテーブル初期化"

#Region "現調部品情報(号車情報)"
        ''' <summary>
        ''' 現調部品情報(号車情報)
        ''' </summary>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Private Function initDataOfGosya() As DataTable
            Dim wTable As New DataTable
            Try
                'カラム追加
                wTable.Columns.Add("SYASYU", Type.GetType("System.String"))             ' 車種
                wTable.Columns.Add("EG_HAIKIRYOU", Type.GetType("System.String"))       ' 排気量
                wTable.Columns.Add("EG_KATASHIKI", Type.GetType("System.String"))       ' 型式(動弁系)
                wTable.Columns.Add("TM_HENSOKUKI", Type.GetType("System.String"))       ' 変速機
                wTable.Columns.Add("EG_KAKYUUKI", Type.GetType("System.String"))        ' 過給器
                wTable.Columns.Add("GRADE", Type.GetType("System.String"))              ' グレード
                wTable.Columns.Add("SHIMUKECHI_CODE", Type.GetType("System.String"))    ' 仕向地コード
                wTable.Columns.Add("HANDLE_POS", Type.GetType("System.String"))         ' ハンドル位置
                wTable.Columns.Add("KATASHIKI_SCD_7", Type.GetType("System.String"))    ' 7桁型式コード
                wTable.Columns.Add("APPLIED_NO", Type.GetType("System.Int32"))          ' アプライド№
                wTable.Columns.Add("KATASHIKI", Type.GetType("System.String"))          ' 7桁型式
                wTable.Columns.Add("OP", Type.GetType("System.String"))                 ' OP
                wTable.Columns.Add("TM_KUDOU", Type.GetType("System.String"))           ' 駆動方式
            Catch ex As Exception
                Console.WriteLine("Error:" & ex.Message)
            End Try
            Return wTable
        End Function
#End Region

#Region "部品情報(Ebom)"
        ''' <summary>
        ''' 部品情報(Ebom)
        ''' </summary>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Private Function initDataOfEbom() As DataTable
            Dim wTable As New DataTable
            Try
                'カラム追加
                '**** 共通 ****
                wTable.Columns.Add("GENCHO_BUKA_CODE", Type.GetType("System.String"))                   ' 現調部課コード
                wTable.Columns.Add("GENCHO_BLOCK_NO", Type.GetType("System.String"))                    ' 現調ブロック№
                wTable.Columns.Add("BUHIN_NO_HYOUJI_JUN", Type.GetType("System.Int32"))                 ' 部品番号表示順
                wTable.Columns.Add("SORT_JUN", Type.GetType("System.Int32"))                            ' ソート順
                '**** 基本情報 ****
                wTable.Columns.Add("CSV_SEQ", Type.GetType("System.Int32"))                             ' CSVデータの取込順序
                wTable.Columns.Add("UNIT_KBN", Type.GetType("System.String"))                           ' ユニット区分(システム区分)
                wTable.Columns.Add("LEVEL", Type.GetType("System.String"))                              ' LEVEL
                wTable.Columns.Add("SHUKEI_CODE", Type.GetType("System.String"))                        ' 国内集計コード
                wTable.Columns.Add("SIA_SHUKEI_CODE", Type.GetType("System.String"))                    ' 海外SIA集計コード
                wTable.Columns.Add("GENCYO_CKD_KBN", Type.GetType("System.String"))                     ' 現調CKD区分
                wTable.Columns.Add("KYOYO_KBN", Type.GetType("System.String"))                          ' 共用区分
                wTable.Columns.Add("BUHIN_NAME", Type.GetType("System.String"))                         ' 部品名称
                wTable.Columns.Add("HOJO_NAME", Type.GetType("System.String"))                          ' 補助名称
                wTable.Columns.Add("BUHIN_NO", Type.GetType("System.String"))                           ' 部品番号
                wTable.Columns.Add("KAIHATSU_SERIES", Type.GetType("System.String"))                    ' 開発シリーズ
                wTable.Columns.Add("SEKKOUSHO_BUHIN_NO", Type.GetType("System.String"))                 ' 設構書品番
                wTable.Columns.Add("KOUTORITEN", Type.GetType("System.String"))                         ' 公取店
                wTable.Columns.Add("SHIYO_JYOHO_NO", Type.GetType("System.String"))                     ' 仕様情報№
                '　NOTE情報
                wTable.Columns.Add("CHUKI_KIJUTSU", Type.GetType("System.String"))                      ' NOTE(532)
                wTable.Columns.Add("ZUMEN_NOTE", Type.GetType("System.String"))                         ' NOTE(533)
                '　部品構成情報
                wTable.Columns.Add("KOUSEI", Type.GetType("System.String"))                             ' 部品構成情報

                '**** 取引先情報 ****
                wTable.Columns.Add("MAKER_CODE", Type.GetType("System.String"))                         ' 取引先コード
                wTable.Columns.Add("MAKER_NAME", Type.GetType("System.String"))                         ' 取引先名称
                '**** 質量情報 ****
                wTable.Columns.Add("BUHIN_SEKKEI_CHI", Type.GetType("System.Decimal"))                  ' 部品設計値(g/個)
                wTable.Columns.Add("SEKKOUSHO_SHITURYO", Type.GetType("System.Decimal"))                ' 設構書時点質量(g/個)
                '**** 原価情報 ****
                wTable.Columns.Add("MIX_BUHIN_HI_YEN", Type.GetType("System.Decimal"))                  ' MIX値部品費(円/個)
                wTable.Columns.Add("SEKKEI_SHISAN_BUHIN_HI_YEN", Type.GetType("System.Decimal"))        ' 設計試算部品費(円/個)
                wTable.Columns.Add("SEKKOUSHO_TANKA_YEN", Type.GetType("System.Decimal"))               ' 設構書時点単価(円/個)
                wTable.Columns.Add("MIX_BUHIN_HI_CENT", Type.GetType("System.Decimal"))                 ' MIX値部品費(ｾﾝﾄ)
                wTable.Columns.Add("SEKKEI_SHISAN_BUHIN_HI_CENT", Type.GetType("System.Decimal"))       ' 設計試算部品費(ｾﾝﾄ)
                wTable.Columns.Add("SEKKOUSHO_TANKA_CENT", Type.GetType("System.Decimal"))              ' 設構書時点単価(ｾﾝﾄ/個)
                wTable.Columns.Add("MIX_KATA_HI_SEN_YEN", Type.GetType("System.Decimal"))               ' MIX値型費(千円)
                wTable.Columns.Add("SEKKEI_SHISAN_KATA_HI_SEN_YEN", Type.GetType("System.Decimal"))     ' 設計試算型費(千円)
                wTable.Columns.Add("SEKKOUSHO_KATA_HI_SEN_YEN", Type.GetType("System.Decimal"))         ' 設構書時点型費(千円)
                wTable.Columns.Add("MIX_KATA_HI_10_DOLLAR", Type.GetType("System.Decimal"))             ' MIX値型費(10$)
                wTable.Columns.Add("SEKKEI_SHISAN_KATA_HI_10_DOLLAR", Type.GetType("System.Decimal"))   ' 設計試算型費(10$)
                wTable.Columns.Add("SEKKOUSHO_KATA_HI_10_DOLLAR", Type.GetType("System.Decimal"))       ' 設構書時点型費(10$)
                wTable.Columns.Add("SEKKOUSHO_KAIHATSU_HI_SEN_YEN", Type.GetType("System.Decimal"))     ' 設構書時点開発費(千円)
                wTable.Columns.Add("SEKKOUSHO_KAIHATSU_HI_10_DOLLAR", Type.GetType("System.Decimal"))   ' 設構書時点開発費(10$)
                '**** 図面情報 ****
                wTable.Columns.Add("SYUTSUZU_YOTEI_DATE", Type.GetType("System.Int32"))                 ' 出図予定日
                wTable.Columns.Add("ZUMEN_SETTSU_NO", Type.GetType("System.String"))                    ' 図面設通№
                wTable.Columns.Add("SYUTSUZU_SKA_JYURYO", Type.GetType("System.Int32"))                 ' 出図完了日/SKA受領
                wTable.Columns.Add("SYUTSUZU_TANTO", Type.GetType("System.String"))                     ' 出図担当者(DSGN)
                wTable.Columns.Add("TEL", Type.GetType("System.String"))                                ' TEL
            Catch ex As Exception
                Console.WriteLine("Error:" & ex.Message)
            End Try
            Return wTable
        End Function
#End Region

#Region "現調部品情報(号車列)"
        ''' <summary>
        ''' 現調部品情報(号車列)
        ''' </summary>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Private Function initDataOfGosyaCol(ByVal wDt As DataTable, ByVal wGosyaCount As Integer) As DataTable
            Dim wTable As DataTable = wDt
            Try
                'カラム追加
                '**** 号車情報 ****
                For idx As Integer = 0 To wGosyaCount - 1
                    wTable.Columns.Add("GOSYA_" & idx.ToString, Type.GetType("System.Int32"))           ' 号車情報(員数)
                Next
            Catch ex As Exception
                Console.WriteLine("Error:" & ex.Message)
            End Try
            Return wTable
        End Function
#End Region

#End Region

#Region "CSVレコード件数取得"

        ''' <summary>
        ''' CSVレコード件数取得
        ''' </summary>
        ''' <param name="nFilePath">CSVファイルパス</param>
        ''' <param name="bTenkaiInstl">INSTLor全構成</param>
        ''' <param name="aJikyu">自給の有無（１：有、０：無））</param>
        ''' <param name="aBlockNo">ブロック№（INSTL展開時）</param>
        ''' <param name="aInstlHinban">インストル品番（INSTL展開時）</param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Private Function GetLinesOfTextFile(ByVal nFilePath As String, ByVal bTenkaiInstl As Boolean, ByVal aJikyu As String, _
                                            ByVal aBlockNo As String, ByVal aInstlHinban As String) As Int32
            Dim hReader As New System.IO.StreamReader(nFilePath)
            Dim lCount As Int32 = 0
            Try
                '--- データ読み込み ---
                'ファイル名と文字エンコードを指定してパーサを実体化
                Dim txtParser As TextFieldParser = New TextFieldParser(nFilePath, System.Text.Encoding.GetEncoding("Shift-JIS"))
                '内容は区切り文字形式
                txtParser.TextFieldType = FieldType.Delimited
                'デリミタはカンマ
                txtParser.SetDelimiters(",")
                'ファイルの終わりまで一行ずつ処理
                Dim aLineCnt As Integer = 0
                While Not txtParser.EndOfData
                    '一行を読み込んで配列に結果を受け取る
                    Dim splittedResult As String() = txtParser.ReadFields()
                    If aLineCnt = 0 Then
                        '=== ヘッダ ===
                    Else
                        '=== 明細 ===

                        '自給品の有無
                        If aJikyu = "1" Or _
                            aJikyu = "0" And splittedResult(4).ToString() <> "" And splittedResult(4).ToString() <> "J" Or _
                            aJikyu = "0" And splittedResult(4).ToString() = "" And splittedResult(5).ToString() <> "J" Then

                            'ブロック、インストル品番指定の場合
                            If StringUtil.IsNotEmpty(aBlockNo) And _
                                StringUtil.IsNotEmpty(aInstlHinban) Then

                                If StringUtil.Equals(aBlockNo, splittedResult(0).ToString()) And _
                                    0 <= splittedResult(columnKOUSEI).ToString().IndexOf(aInstlHinban) Then

                                    lCount += 1

                                End If

                            Else
                                'instl品番を選択したか？
                                If StringUtil.Equals(splittedResult(columnLEVEL).ToString(), "0") Then
                                    lCount += 1
                                End If
                            End If

                        End If

                    End If
                    aLineCnt += 1
                End While
                '最後に閉じる
                txtParser.Close()
            Catch ex As Exception
                MessageBox.Show(ex.Message, "異常", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End Try
            Return lCount
        End Function

#End Region

#Region "データセット(号車情報)"
        ''' <summary>
        ''' データセット(号車情報)
        ''' </summary>
        ''' <param name="strTemp"></param>
        ''' <remarks></remarks>
        ''' <returns></returns>
        Private Function SetDataOfGosya(ByVal wDt As DataTable, ByVal strTemp As String()) As DataTable
            Dim wTable As DataTable = wDt
            Try
                'データセット
                For Each wTemp In strTemp
                    If 0 <= wTemp.IndexOf("/") Then
                        Dim arrItems As String() = wTemp.Split("/")
                        If arrItems.Length > 0 Then
                            Dim aData As DataRow = wTable.NewRow
                            aData("SYASYU") = arrItems(0)
                            aData("EG_HAIKIRYOU") = arrItems(1)
                            aData("EG_KATASHIKI") = arrItems(2)
                            aData("TM_HENSOKUKI") = arrItems(3)
                            aData("EG_KAKYUUKI") = arrItems(4)
                            aData("GRADE") = arrItems(5)
                            aData("SHIMUKECHI_CODE") = arrItems(6)
                            aData("HANDLE_POS") = arrItems(7)
                            aData("KATASHIKI_SCD_7") = arrItems(8)
                            aData("APPLIED_NO") = arrItems(9)
                            aData("KATASHIKI") = arrItems(10)
                            aData("OP") = arrItems(11)
                            aData("TM_KUDOU") = arrItems(12)
                            wTable.Rows.Add(aData)
                        End If
                    End If
                Next
            Catch ex As Exception
                Console.WriteLine("Error:" & ex.Message)
            End Try
            Return wTable
        End Function
#End Region

#Region "データセット"

#Region "データテーブルにデータセット"
        ''' <summary>
        ''' スプレッドにデータセット
        ''' </summary>
        ''' <param name="wKaihatsuFugo"></param>
        ''' <param name="wLineCnt"></param>
        ''' <param name="wDataRow"></param>
        ''' <param name="strTemp"></param>
        ''' <remarks></remarks>
        Private Function setLineOfData(ByVal wKaihatsuFugo As String, ByVal wLineCnt As Integer, ByVal wDataRow As DataRow, ByVal strTemp As String()) As DataRow
            Dim aDataRow As DataRow = wDataRow
            Try
                '=== 基本情報 ===
                aDataRow = setLineOfDataBasic(wKaihatsuFugo, wLineCnt, aDataRow, strTemp)
                ''=== 取引先決定情報 ===
                'aDataRow = setLineOfDataMaker(aDataRow, strTemp)
                '=== 号車情報 ===
                aDataRow = setLineOfDataGoshaInfo(aDataRow, strTemp)

            Catch ex As Exception
                Console.WriteLine("Error:" & ex.Message)
            End Try
            Return aDataRow
        End Function
#End Region

#Region "データセット(基本情報)"
        ''' <summary>
        ''' データセット(基本情報)
        ''' </summary>
        ''' <param name="wKaihatsuFugo"></param>
        ''' <param name="wLineCnt"></param>
        ''' <param name="wDataRow"></param>
        ''' <param name="strTemp"></param>
        ''' <remarks></remarks>
        Private Function setLineOfDataBasic(ByVal wKaihatsuFugo As String, ByVal wLineCnt As Integer, ByVal wDataRow As DataRow, ByVal strTemp As String()) As DataRow
            Dim aDataRow As DataRow = wDataRow
            Try
                Dim aBuhinNo As String = strTemp(columnBUHIN_NO).TrimEnd

                ''--- システム区分取得 ---
                Dim aBlockNo As String = strTemp(columnBLOCK_NO).TrimEnd
                Dim aMtKbn As String = getSystemKbn(wKaihatsuFugo, aBlockNo)

                ''--- 532から注記（CHUKI_KIJUTSU）取得 ---
                Dim aNote532 As String = getNote532(aBuhinNo)
                ''--- 533から図面集合NOTE（ZUMEN_NOTE）取得 ---
                Dim aNote533 As String = getNote533(aBuhinNo)

                ' CSVデータの取込順序
                aDataRow("CSV_SEQ") = wLineCnt
                ' 課名
                aDataRow("GENCHO_BUKA_CODE") = strTemp(columnBUKA_CODE).TrimEnd
                ' システム区分
                aDataRow("UNIT_KBN") = aMtKbn.TrimEnd
                ' ブロック№
                aDataRow("GENCHO_BLOCK_NO") = aBlockNo
                ' Lv
                aDataRow("LEVEL") = strTemp(1).TrimEnd
                ' 国内集計
                aDataRow("SHUKEI_CODE") = strTemp(columnSHUKEI_CODE).TrimEnd
                If StringUtil.IsEmpty(strTemp(columnSHUKEI_CODE).TrimEnd) Then
                    aDataRow("SHUKEI_CODE") = " "
                Else
                    aDataRow("SHUKEI_CODE") = strTemp(columnSHUKEI_CODE).TrimEnd
                End If
                ' 海外集計
                If StringUtil.IsEmpty(strTemp(columnSIA_SHUKEI_CODE).TrimEnd) Then
                    aDataRow("SIA_SHUKEI_CODE") = " "
                Else
                    aDataRow("SIA_SHUKEI_CODE") = strTemp(columnSIA_SHUKEI_CODE).TrimEnd
                End If

                ' 現調区分
                aDataRow("GENCYO_CKD_KBN") = strTemp(columnGENCYO_CKD_KBN).TrimEnd
                ' 共用区分
                aDataRow("KYOYO_KBN") = strTemp(columnKYOYO_KBN).TrimEnd
                ' 部品名称
                aDataRow("BUHIN_NAME") = strTemp(columnBUHIN_NAME).TrimEnd
                ' 補助名称
                aDataRow("HOJO_NAME") = strTemp(columnHOJO_NAME).TrimEnd
                ' 部品番号(MIX品番)
                If StringUtil.IsNotEmpty(aBuhinNo) Then
                    aDataRow("BUHIN_NO") = aBuhinNo
                End If

                ' NOTE情報
                aDataRow("CHUKI_KIJUTSU") = aNote532
                aDataRow("ZUMEN_NOTE") = aNote533

                ' 部品構成
                aDataRow("KOUSEI") = strTemp(columnKOUSEI).TrimEnd

            Catch ex As Exception
                Console.WriteLine("Error:" & ex.Message)
            End Try
            Return aDataRow
        End Function
#End Region

#Region "データセット(取引先情報)"
        ''' <summary>
        ''' データセット(取引先情報)
        ''' </summary>
        ''' <param name="wDataRow"></param>
        ''' <param name="strTemp"></param>
        ''' <remarks></remarks>
        Private Function setLineOfDataMaker(ByVal wDataRow As DataRow, ByVal strTemp As String()) As DataRow
            Dim aDataRow As DataRow = wDataRow
            Try
                Dim aBuhinNo As String = strTemp(2).TrimEnd

                If StringUtil.IsNotEmpty(aBuhinNo) Then
                    Dim impl As KouseiBuhinSelectorDao = New KouseiBuhinSelectorDaoImpl
                    Dim vo As New BuhinListVo
                    vo = impl.FindByBuhinMaker(aBuhinNo)
                    If Not vo Is Nothing Then
                        '取引先コード
                        If Not StringUtil.IsEmpty(vo.TorihikisakiCode) Then
                            aDataRow("MAKER_CODE") = vo.TorihikisakiCode
                        End If
                        '取引先名称
                        If Not StringUtil.IsEmpty(vo.TorihikisakiName) Then
                            aDataRow("MAKER_NAME") = vo.TorihikisakiName
                        End If
                    End If
                End If
            Catch ex As Exception
                Console.WriteLine("Error:" & ex.Message)
            End Try
            Return aDataRow
        End Function
#End Region

#Region "データセット(号車情報)"
        ''' <summary>
        ''' データセット(号車情報)
        ''' </summary>
        ''' <param name="wDataRow"></param>
        ''' <param name="strTemp"></param>
        ''' <remarks></remarks>
        Private Function setLineOfDataGoshaInfo(ByVal wDataRow As DataRow, ByVal strTemp As String()) As DataRow
            Dim aDataRow As DataRow = wDataRow
            Try
                Dim cnt As Integer = 0
                For idx As Integer = _GoshaStart To _GoshaStart + _GoshaCount - 1
                    aDataRow("GOSYA_" & cnt.ToString) = strTemp(idx)
                    cnt += 1
                Next
            Catch ex As Exception
                Console.WriteLine("Error:" & ex.Message)
            End Try
            Return aDataRow
        End Function
#End Region

#End Region

#Region "システム区分取得"
        ''' <summary>
        ''' システム区分取得
        ''' </summary>
        ''' <param name="aKaihatsuFugo">開発符号</param>
        ''' <param name="aBlockNo">ブロック№</param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Function getSystemKbn(ByVal aKaihatsuFugo As String, ByVal aBlockNo As String) As String
            Dim aSystemKbn As String = ""
            Try
                Dim HelperDao As KouseiBuhinSelectorDao = New KouseiBuhinSelectorDaoImpl
                Dim Vos0080 As New List(Of Rhac0080Vo)
                Dim Vo0080 As New Rhac0080Vo
                If StringUtil.IsNotEmpty(aKaihatsuFugo) Then
                    Vos0080 = HelperDao.GetByKaiteiNoRhac0080(aKaihatsuFugo, aBlockNo)
                    If Vos0080.Count > 0 Then
                        Dim Rhac0080Dao As Rhac0080Dao = New Rhac0080DaoImpl
                        Vo0080 = Rhac0080Dao.FindByPk(aKaihatsuFugo, aBlockNo, Vos0080(0).KaiteiNoKino)
                        aSystemKbn = Vo0080.MtKbn
                    End If
                End If
            Catch ex As Exception
                Throw ex
            End Try
            Return aSystemKbn
        End Function
#End Region

#Region "NOTE532取得"
        ''' <summary>
        ''' NOTE532取得
        ''' </summary>
        ''' <param name="aBuhinNo">部品番号</param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Function getNote532(ByVal aBuhinNo As String) As String
            Dim aNote As String = ""
            Try
                Dim HelperDao As KouseiBuhinSelectorDao = New KouseiBuhinSelectorDaoImpl
                Dim Vos0532 As New List(Of Rhac0532Vo)
                Dim Vo0532 As New Rhac0532Vo
                If StringUtil.IsNotEmpty(aBuhinNo) Then
                    Vos0532 = HelperDao.GetByKaiteiNoRhac0532(aBuhinNo)
                    If Vos0532.Count > 0 Then
                        Dim Rhac0532Dao As Rhac0532Dao = New Rhac0532DaoImpl
                        Vo0532 = Rhac0532Dao.FindByPk(aBuhinNo, Vos0532(0).KaiteiNo)
                        aNote = Vo0532.ChukiKijutsu
                    End If
                End If
            Catch ex As Exception
                Throw ex
            End Try
            Return aNote
        End Function
#End Region

#Region "NOTE533取得"
        ''' <summary>
        ''' NOTE533取得
        ''' </summary>
        ''' <param name="aBuhinNo">部品番号</param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Function getNote533(ByVal aBuhinNo As String) As String
            Dim aNote As String = ""
            Try
                Dim HelperDao As KouseiBuhinSelectorDao = New KouseiBuhinSelectorDaoImpl
                Dim Vos0533 As New List(Of Rhac0533Vo)
                Dim Vo0533 As New Rhac0533Vo
                If StringUtil.IsNotEmpty(aBuhinNo) Then
                    Vos0533 = HelperDao.GetByKaiteiNoRhac0533(aBuhinNo)
                    If Vos0533.Count > 0 Then
                        Dim Rhac0533Dao As Rhac0533Dao = New Rhac0533DaoImpl
                        Vo0533 = Rhac0533Dao.FindByPk(aBuhinNo, Vos0533(0).KaiteiNo)
                        aNote = Vo0533.ZumenNote
                    End If
                End If
            Catch ex As Exception
                Throw ex
            End Try
            Return aNote
        End Function
#End Region

#Region "RHAC0532取得（VOで取得）"
        ''' <summary>
        ''' RHAC0532取得（VOで取得）
        ''' </summary>
        ''' <param name="aBuhinNo">部品番号</param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Function getRhac0532(ByVal aBuhinNo As String) As List(Of Rhac0532Vo)
            Try
                If StringUtil.IsNotEmpty(aBuhinNo) Then

                    Dim param As New Rhac0532Vo
                    param.BuhinNo = aBuhinNo
                    param.HaisiDate = 99999999

                    Dim Rhac0532Dao As Rhac0532Dao = New Rhac0532DaoImpl
                    getRhac0532 = Rhac0532Dao.FindBy(param)

                    If getRhac0532.Count = 0 Then
                        Return Nothing
                    End If

                Else
                    Return Nothing
                End If
            Catch ex As Exception
                Return Nothing
            End Try
        End Function
#End Region

#Region "RHAC0533取得（VOで取得）"
        ''' <summary>
        ''' RHAC0533取得（VOで取得）
        ''' </summary>
        ''' <param name="aBuhinNo">部品番号</param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Function getRHAC0533(ByVal aBuhinNo As String) As List(Of Rhac0533Vo)
            Try
                If StringUtil.IsNotEmpty(aBuhinNo) Then

                    Dim param As New Rhac0533Vo
                    param.BuhinNo = aBuhinNo
                    param.HaisiDate = 99999999

                    Dim Rhac0533Dao As Rhac0533Dao = New Rhac0533DaoImpl
                    getRHAC0533 = Rhac0533Dao.FindBy(param)

                    If getRHAC0533.Count = 0 Then
                        Return Nothing
                    End If

                Else
                    Return Nothing
                End If
            Catch ex As Exception
                Return Nothing
            End Try
        End Function
#End Region

#Region "RHAC0552取得（VOで取得）"
        ''' <summary>
        ''' RHAC0552取得（VOで取得）
        ''' </summary>
        ''' <param name="aBuhinNo">部品番号</param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Function getRhac0552(ByVal aBuhinNo As String) As List(Of Rhac0552Vo)
            Try
                If StringUtil.IsNotEmpty(aBuhinNo) Then

                    Dim param As New Rhac0552Vo
                    param.BuhinNoOya = aBuhinNo
                    param.HaisiDate = 99999999

                    Dim Rhac0552Dao As Rhac0552Dao = New Rhac0552DaoImpl
                    getRhac0552 = Rhac0552Dao.FindBy(param)

                    If getRhac0552.Count = 0 Then
                        Return Nothing
                    End If

                Else
                    Return Nothing
                End If
            Catch ex As Exception
                Return Nothing
            End Try
        End Function
#End Region

#Region "RHAC0553取得（VOで取得）"
        ''' <summary>
        ''' RHAC0553取得（VOで取得）
        ''' </summary>
        ''' <param name="aBuhinNo">部品番号</param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Function getRhac0553(ByVal aKaihatsuFugo As String, ByVal aBuhinNo As String) As List(Of Rhac0553Vo)
            Try
                If StringUtil.IsNotEmpty(aKaihatsuFugo) And StringUtil.IsNotEmpty(aBuhinNo) Then

                    Dim param As New Rhac0553Vo
                    param.KaihatsuFugo = aKaihatsuFugo
                    param.BuhinNoOya = "T"
                    param.BuhinNoKo = aBuhinNo
                    param.HaisiDate = 99999999
                    param.InsuSuryo = ""

                    Dim Rhac0553Dao As Rhac0553Dao = New Rhac0553DaoImpl
                    getRhac0553 = Rhac0553Dao.FindBy(param)

                    If getRhac0553.Count = 0 Then
                        Return Nothing
                    End If

                Else
                    Return Nothing
                End If
            Catch ex As Exception
                Return Nothing
            End Try
        End Function
#End Region

#Region "手配記号"

        ''' <summary>
        ''' 手配記号の設定
        ''' </summary>
        ''' <param name="wDataRow"></param>
        ''' <remarks></remarks>
        Public Function setTehaikigo(ByVal wDataRow As DataRow) As String
            Dim aTehaikigo As String = ""
            Try
                ' 国内集計コード
                Dim SyukeiCode As String = wDataRow("SHUKEI_CODE")
                ' 海外SIA集計コード
                Dim SiaSyukeiCode As String = wDataRow("SIA_SHUKEI_CODE")

                If StringUtil.IsEmpty(SyukeiCode) AndAlso StringUtil.IsNotEmpty(SiaSyukeiCode) Then
                    'US
                    Select Case SiaSyukeiCode
                        Case "A"
                            aTehaikigo = "J"
                        Case "E"
                            aTehaikigo = "B"
                        Case "Y"
                            aTehaikigo = "B"
                    End Select
                ElseIf StringUtil.IsNotEmpty(SyukeiCode) Then
                    'JP
                    Select Case SyukeiCode
                        Case "X"
                            aTehaikigo = "F"
                        Case "A"
                            aTehaikigo = ""
                        Case "E"
                            aTehaikigo = "D"
                        Case "Y"
                            aTehaikigo = "D"
                        Case "R"
                            aTehaikigo = "F"
                        Case "J"
                            aTehaikigo = "F"
                    End Select
                End If

            Catch ex As Exception
                Console.WriteLine("Error:" & ex.Message)
            End Try
            Return aTehaikigo
        End Function

#End Region

#Region "部品番号のチェックを外す。"

        ''' <summary>
        ''' 部品番号のチェックを外す。
        ''' </summary>
        ''' <remarks></remarks>
        Public Sub selectBuhinNoCheckLess()
            Try

                Dim frm As Frm41KouseiBuhinSelector = _frmKouseiBuhinSelector

                Dim aBuhinList As String = ""
                '
                Dim selection As FarPoint.Win.Spread.Model.CellRange() = frm.spdBuhin_Sheet1.GetSelections()
                For rowindex As Integer = selection(0).Row To selection(0).RowCount + selection(0).Row - 1
                    'Instl品番以外
                    If frm.spdBuhin_Sheet1.Cells(rowindex, spd_Buhin_Col_Level).Value <> "0" And _
                        frm.spdBuhin_Sheet1.Cells(rowindex, spd_Buhin_Col_Select).Value = True Then
                        'Y,E　国内集計コードがＹ，Ｅ，Ｊか、国内集計コードがブランクで海外集計コードがＹ，Ｅ，Ｊの場合。
                        If frm.spdBuhin_Sheet1.Cells(rowindex, spd_Buhin_Col_ShukeiCode).Value = "Y" Or _
                            frm.spdBuhin_Sheet1.Cells(rowindex, spd_Buhin_Col_ShukeiCode).Value = "E" Or _
                            frm.spdBuhin_Sheet1.Cells(rowindex, spd_Buhin_Col_ShukeiCode).Value = "J" Or _
                            (StringUtil.IsEmpty(frm.spdBuhin_Sheet1.Cells(rowindex, spd_Buhin_Col_ShukeiCode).Value) And _
                                (frm.spdBuhin_Sheet1.Cells(rowindex, spd_Buhin_Col_SiaShukeiCode).Value = "Y" Or _
                                 frm.spdBuhin_Sheet1.Cells(rowindex, spd_Buhin_Col_SiaShukeiCode).Value = "E" Or _
                                 frm.spdBuhin_Sheet1.Cells(rowindex, spd_Buhin_Col_SiaShukeiCode).Value = "J")) Then
                            '本文
                            aBuhinList += vbCrLf & Space(2) & frm.spdBuhin_Sheet1.Cells(rowindex, spd_Buhin_Col_BlockNo).Value & _
                                                   Space(2) & frm.spdBuhin_Sheet1.Cells(rowindex, spd_Buhin_Col_Level).Value & _
                                                   Space(2) & frm.spdBuhin_Sheet1.Cells(rowindex, spd_Buhin_Col_BuhinNo).Value
                        End If
                    End If
                Next
                'チェックを外す対象がいるなら続行
                If StringUtil.IsNotEmpty(aBuhinList) Then
                    Dim nYesNo As Integer = MsgBox("下記の部品番号のチェックを外しますか？" & vbLf & aBuhinList, _
                                                   MsgBoxStyle.YesNo, "確認")
                    'Yesだったら処理続行
                    If nYesNo = MsgBoxResult.Yes Then
                        For rowindex As Integer = selection(0).Row To selection(0).RowCount + selection(0).Row - 1
                            'Instl品番以外
                            If frm.spdBuhin_Sheet1.Cells(rowindex, spd_Buhin_Col_Level).Value <> "0" Then
                                'Y,E　国内集計コードがＹ，Ｅ，Ｊか、国内集計コードがブランクで海外集計コードがＹ，Ｅ，Ｊの場合。
                                If frm.spdBuhin_Sheet1.Cells(rowindex, spd_Buhin_Col_ShukeiCode).Value = "Y" Or _
                                    frm.spdBuhin_Sheet1.Cells(rowindex, spd_Buhin_Col_ShukeiCode).Value = "E" Or _
                                    frm.spdBuhin_Sheet1.Cells(rowindex, spd_Buhin_Col_ShukeiCode).Value = "J" Or _
                                    (StringUtil.IsEmpty(frm.spdBuhin_Sheet1.Cells(rowindex, spd_Buhin_Col_ShukeiCode).Value) And _
                                        (frm.spdBuhin_Sheet1.Cells(rowindex, spd_Buhin_Col_SiaShukeiCode).Value = "Y" Or _
                                         frm.spdBuhin_Sheet1.Cells(rowindex, spd_Buhin_Col_SiaShukeiCode).Value = "E" Or _
                                         frm.spdBuhin_Sheet1.Cells(rowindex, spd_Buhin_Col_SiaShukeiCode).Value = "J")) Then
                                    'チェックを外す。
                                    frm.spdBuhin_Sheet1.Cells(rowindex, spd_Buhin_Col_Select).Value = False
                                    '背景色も変えてみる。
                                    frm.spdBuhin_Sheet1.Cells(rowindex, spd_Buhin_Col_Select).BackColor = Color.Yellow
                                    frm.spdBuhin_Sheet1.Cells(rowindex, spd_Buhin_Col_BuhinNo).BackColor = Color.Yellow
                                    '構成状況。
                                    frm.spdBuhin_Sheet1.Cells(rowindex, spd_Buhin_Col_SelectionMethod).Value = HOYOU_NOMAL_LESS
                                End If
                            End If
                        Next
                    End If
                End If

            Catch ex As Exception
                ComFunc.ShowInfoMsgBox("部品番号を選択後、再度実行して下さい。")
            End Try
        End Sub

        ''' <summary>
        ''' 補用部品番号のチェックを外す。
        ''' </summary>
        ''' <remarks></remarks>
        Public Sub selectShisakuBuhinNoCheckLess()

        End Sub

#End Region

        ''' <summary>
        ''' 初期化イベント
        ''' </summary>
        ''' <remarks></remarks>
        Public Sub doSearchResetEvent()

            Dim frm As Frm41KouseiBuhinSelector = _frmKouseiBuhinSelector
            '==========================================================
            '項目初期化
            For i As Integer = frm.spdBuhinShiyou_Sheet1.Columns(DispShiyouJyouhouList.TAG_SHIYOU_JYOUHOU_NO.ToString).Index() _
                            To frm.spdBuhinShiyou_Sheet1.Columns(DispShiyouJyouhouList.TAG_KATA_OP.ToString).Index()
                frm.spdBuhinShiyou_Sheet1.SetValue(spd_BuhinShiyou_startRow, i, String.Empty)
            Next
            ''仕様情報一覧の初期化
            'frm.spdBuhinShiyou_Sheet1.SetValue(spd_BuhinShiyou_startRow, _
            '                                   frm.spdBuhinShiyou_Sheet1.Columns(DispShiyouJyouhouList.TAG_SHIYOU_JYOUHOU_NO.ToString).Index(), _
            '                                   String.Empty)
            '' 車型の初期化
            'frm.spdBuhinShiyou_Sheet1.SetValue(spd_BuhinShiyou_startRow, _
            '                       frm.spdBuhinShiyou_Sheet1.Columns(DispShiyouJyouhouList.TAG_SYAGATA.ToString).Index(), _
            '                       String.Empty)
            ''ｸﾞﾚｰﾄﾞの初期化
            'frm.spdBuhinShiyou_Sheet1.SetValue(spd_BuhinShiyou_startRow, _
            '                       frm.spdBuhinShiyou_Sheet1.Columns(DispShiyouJyouhouList.TAG_GRADE.ToString).Index(), _
            '                       String.Empty)
            ''仕向地・ﾊﾝﾄﾞﾙの初期化
            'frm.spdBuhinShiyou_Sheet1.SetValue(spd_BuhinShiyou_startRow, _
            '                       frm.spdBuhinShiyou_Sheet1.Columns(DispShiyouJyouhouList.TAG_SHIMUKECHI_HANDLE.ToString).Index(), _
            '                       String.Empty)
            ''E/G・排気量の初期化
            'frm.spdBuhinShiyou_Sheet1.SetValue(spd_BuhinShiyou_startRow, _
            '                       frm.spdBuhinShiyou_Sheet1.Columns(DispShiyouJyouhouList.TAG_EG_HAIKIRYOU.ToString).Index(), _
            '                       String.Empty)
            ''E/G・形式の初期化
            'frm.spdBuhinShiyou_Sheet1.SetValue(spd_BuhinShiyou_startRow, _
            '                       frm.spdBuhinShiyou_Sheet1.Columns(DispShiyouJyouhouList.TAG_EG_KEISHIKI.ToString).Index(), _
            '                       String.Empty)
            ''E/G・過給器の初期化
            'frm.spdBuhinShiyou_Sheet1.SetValue(spd_BuhinShiyou_startRow, _
            '                       frm.spdBuhinShiyou_Sheet1.Columns(DispShiyouJyouhouList.TAG_EG_KAKYUKI.ToString).Index(), _
            '                       String.Empty)
            ''T/M・駆動方式の初期化
            'frm.spdBuhinShiyou_Sheet1.SetValue(spd_BuhinShiyou_startRow, _
            '                       frm.spdBuhinShiyou_Sheet1.Columns(DispShiyouJyouhouList.TAG_TM_KUDOU.ToString).Index(), _
            '                       String.Empty)
            ''T/M・変速機の初期化
            'frm.spdBuhinShiyou_Sheet1.SetValue(spd_BuhinShiyou_startRow, _
            '                       frm.spdBuhinShiyou_Sheet1.Columns(DispShiyouJyouhouList.TAG_TM_HENSOKUKI.ToString).Index(), _
            '                       String.Empty)
            ''７桁型式の初期化
            'frm.spdBuhinShiyou_Sheet1.SetValue(spd_BuhinShiyou_startRow, _
            '                       frm.spdBuhinShiyou_Sheet1.Columns(DispShiyouJyouhouList.TAG_KATA_7_KETA_KATASHIKI.ToString).Index(), _
            '                       String.Empty)
            ''仕向けコードの初期化
            'frm.spdBuhinShiyou_Sheet1.SetValue(spd_BuhinShiyou_startRow, _
            '                       frm.spdBuhinShiyou_Sheet1.Columns(DispShiyouJyouhouList.TAG_KATA_SHIMUKE.ToString).Index(), _
            '                       String.Empty)
            ''ＯＰコードの初期化
            'frm.spdBuhinShiyou_Sheet1.SetValue(spd_BuhinShiyou_startRow, _
            '                       frm.spdBuhinShiyou_Sheet1.Columns(DispShiyouJyouhouList.TAG_KATA_OP.ToString).Index(), _
            '                       String.Empty)
            '==========================================================
            '仕様情報一覧の仕様情報№の設定
            SpreadUtil.BindCellTypeToColumn(frm.spdBuhinShiyou_Sheet1, _
                                            DispShiyouJyouhouList.TAG_SHIYOU_JYOUHOU_NO.ToString, _
                                            NewShiyouJyouhouNoCellType)
            '仕様情報一覧の仕様情報№へ最新の№を設定
            Dim ShiyouJyouhouDao As ShiyouJyouhouDao = New ShiyouJyouhouDaoImpl
            Dim rhac0030Vo As Rhac0030Vo = _
                        ShiyouJyouhouDao.GetByNewShiyouJyouhouNo(frm.cmbKaihatsuFugo.Text)
            frm.spdBuhinShiyou_Sheet1.SetValue(spd_BuhinShiyou_startRow, _
                                               frm.spdBuhinShiyou_Sheet1.Columns(DispShiyouJyouhouList.TAG_SHIYOU_JYOUHOU_NO.ToString).Index(), _
                                               rhac0030Vo.ShiyoshoSeqno)
            ' 車型のコンボボックスを生成する。
            setShiyouJyouhouListSyagata()
            'ｸﾞﾚｰﾄﾞのコンボボックスを生成する。
            setShiyouJyouhouListGrade()
            '仕向地・仕向けのコンボボックスを生成する。
            'setShiyouJyouhouListShimukechiShimuke()
            '仕向地・ﾊﾝﾄﾞﾙのコンボボックスを生成する。
            setShiyouJyouhouListShimukechiHandle()
            'E/G・排気量のコンボボックスを生成する。
            setEgHaikiryouListShimukechiHandle()
            'E/G・形式のコンボボックスを生成する。
            setEgKeishikiListShimukechiHandle()
            'E/G・過給器のコンボボックスを生成する。
            setEgKakyukiListShimukechiHandle()
            'T/M・駆動方式のコンボボックスを生成する。
            setTmKudouListShimukechiHandle()
            'T/M・変速機のコンボボックスを生成する。
            setTmHensokukiListShimukechiHandle()
            '７桁型式のコンボボックスを生成する。
            setKatashiki7ListShimukechiHandle()
            '仕向けコードのコンボボックスを生成する。
            setKataShimukeListShimukechiHandle()
            'ＯＰコードのコンボボックスを生成する。
            setKataOpListShimukechiHandle()

            'ＯＰ項目列を作成する。
            setOpKoumokuRetuHandle()

        End Sub

    End Class

End Namespace
