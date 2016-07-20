Imports System.Text
Imports Microsoft.VisualBasic.FileIO

Imports EBom.Data
Imports EBom.Common

Imports ShisakuCommon
Imports ShisakuCommon.Db.EBom
Imports ShisakuCommon.Db.EBom.Dao
Imports ShisakuCommon.Db.EBom.Dao.Impl
Imports ShisakuCommon.Db.EBom.Vo
Imports ShisakuCommon.Db.EBom.Vo.Helper
Imports ShisakuCommon.Ui
Imports ShisakuCommon.Util.LabelValue

Imports EventSakusei.KouseiBuhin.Dao
Imports EventSakusei.ShisakuBuhinEdit.KouseiBuhin.Dao
Imports EventSakusei.ShisakuBuhinEdit.KouseiBuhin.Dao.Vo
Imports EventSakusei.ShisakuBuhinMenu
Imports ShisakuCommon.Ui.Spd
Imports EventSakusei.ShisakuBuhinEdit.Kosei.Logic.Matrix
Imports FarPoint.Win.Spread.CellType

Namespace KouseiBuhin.Logic

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <remarks></remarks>
    Public Class HoyouBuhinOldBuhinSelectorSubject

#Region "メンバー変数"

        '''<summary>前画面引継ぎ項目</summary>>
        Private _HoyouEventCode As String
        Private _HoyouEventName As String

        ''' <summary>E-BOM部品情報取込フォーム</summary>
        Private _frmDispOldBuhinSelector As HoyouBuhinFrm41DispOldBuhinSelector

        '''' <summary>ロジック</summary>
        'Private m_kubunList As DispKubunList = Nothing
        'Private m_blockList As DispBlockList = Nothing
        'Private m_buhinList As DispBuhinList = Nothing
        'Private m_shiyouJyouhouList As DispShiyouJyouhouList = Nothing

        'Private m_gousyaList As DispGousyaList = Nothing

        ''' <summary>FpSpread共通メソッド</summary>
        Private m_spCom As SpreadCommon

        '''' <summary>CSVカラム</summary>
        'Private Const columnBUKA_CODE As Integer = 35
        'Private Const columnBLOCK_NO As Integer = 0
        'Private Const columnLEVEL As Integer = 1
        'Private Const columnSHUKEI_CODE As Integer = 4
        'Private Const columnSIA_SHUKEI_CODE As Integer = 5
        'Private Const columnGENCYO_CKD_KBN As Integer = 6
        'Private Const columnKYOYO_KBN As Integer = 7
        'Private Const columnBUHIN_NAME As Integer = 8
        'Private Const columnHOJO_NAME As Integer = 9
        'Private Const columnBUHIN_NO As Integer = 2
        'Private Const columnKOUSEI As Integer = 33

        'Private Const columnintGousyaStart As Integer = 44

        '''' <summary>基本情報カラム</summary>
        'Private Const basicBuhinNo As Integer = 13
        'Private Const basicBuhinName As Integer = 11
        'Private Const basicBlockNo As Integer = 1
        'Private Const basicLevel As Integer = 6
        'Private Const basicShukeiCode As Integer = 7
        'Private Const basicSiaShukeiCode As Integer = 8
        'Private Const basicBuhinNote As Integer = 18
        'Private Const basicBuhinKousei As Integer = 20

        'セルタイプ
        'Private buttonType As New ButtonCellType
        'Private textType As New TextCellType

        ''' <summary>基本情報カラム</summary>
        Private Const TAG_HOYOU_EVENT_CODE As String = "HOYOU_EVENT_CODE"
        Private Const TAG_TANTO_FUYOU As String = "TANTO_FUYOU"
        Private Const TAG_HOYOU_BUKA_CODE As String = "HOYOU_BUKA_CODE"
        Private Const TAG_HOYOU_TANTO As String = "HOYOU_TANTO"
        Private Const TAG_HOYOU_TANTO_NAME As String = "HOYOU_TANTO_NAME"
        Private Const TAG_HOYOU_TANTO_KAITEI_NO As String = "HOYOU_TANTO_KAITEI_NO"
        Private Const TAG_TANTO_MEMO As String = "TANTO_MEMO"
        Private Const TAG_SAISYU_KOUSHINBI As String = "SAISYU_KOUSHINBI"
        Private Const TAG_TANTO_SYOUNIN_JYOUTAI As String = "TANTO_SYOUNIN_JYOUTAI"
        Private Const TAG_KACHOU_SYOUNIN_JYOUTAI As String = "KACHOU_SYOUNIN_JYOUTAI"

        '''' <summary>部品SPREADカラム</summary>

        'Private Const spd_Buhin_Col_EventCode As Integer = 0
        'Private Const spd_Buhin_Col_TantoFuyou As Integer = 1
        'Private Const spd_Buhin_Col_BukaCode As Integer = 2
        'Private Const spd_Buhin_Col_Tanto As Integer = 3
        'Private Const spd_Buhin_Col_TantoName As Integer = 4
        'Private Const spd_Buhin_Col_KaiteiNo As Integer = 5
        'Private Const spd_Buhin_Col_Memo As Integer = 6
        'Private Const spd_Buhin_Col_SaisyuKoushinbi As Integer = 7
        'Private Const spd_Buhin_Col_TantoSyounin As Integer = 8
        'Private Const spd_Buhin_Col_KachouSyounin As Integer = 9

        'Private Const spd_Buhin_Col_EventCode As Integer = 0
        'Private Const spd_Buhin_Col_Level As Integer = 1
        'Private Const spd_Buhin_Col_ShukeiCode As Integer = 2
        'Private Const spd_Buhin_Col_SiaShukeiCode As Integer = 3
        'Private Const spd_Buhin_Col_Tenkai As Integer = 4
        'Private Const spd_Buhin_Col_Select As Integer = 5
        'Private Const spd_Buhin_Col_BuhinNo As Integer = 6
        'Private Const spd_Buhin_Col_Insu As Integer = 7
        'Private Const spd_Buhin_Col_BuhinName As Integer = 8
        'Private Const spd_Buhin_Col_Note As Integer = 9
        'Private Const spd_Buhin_Col_SelectionMethod As Integer = 10
        'Private Const spd_Buhin_Col_KyoKyu As Integer = 11

        'Private Shared ReadOnly EMPTY_LIST As New List(Of LabelValueVo)

        'Private Shared ReadOnly EMPTY_FINAL_HINBAN_LIST As New List(Of FinalHinbanListVo)

        'Public Const spd_BuhinShiyou_startRow As Integer = 4
        'Private Const spd_BuhinShiyou_KaihatuFugoRow As Integer = 1
        'Private Const spd_BuhinShiyou_OpSpecCodeRow As Integer = 2
        'Private Const spd_BuhinShiyou_OpSpecNameRow As Integer = 3

#End Region

#Region "プロパティ"

#Region "CSV位置情報"

        ''質量情報
        'Private _MassStart As Integer
        'Private _MassCount As Integer
        ''コスト情報
        'Private _CostStart As Integer
        'Private _CostCount As Integer
        ''号車情報
        'Private _GoshaStart As Integer
        'Private _GoshaCount As Integer

        'Public Property MassStart() As Integer
        '    Get
        '        Return _MassStart
        '    End Get
        '    Set(ByVal value As Integer)
        '        _MassStart = value
        '    End Set
        'End Property

        'Public Property MassCount() As Integer
        '    Get
        '        Return _MassCount
        '    End Get
        '    Set(ByVal value As Integer)
        '        _MassCount = value
        '    End Set
        'End Property

        'Public Property CostStart() As Integer
        '    Get
        '        Return _CostStart
        '    End Get
        '    Set(ByVal value As Integer)
        '        _CostStart = value
        '    End Set
        'End Property

        'Public Property CostCount() As Integer
        '    Get
        '        Return _CostCount
        '    End Get
        '    Set(ByVal value As Integer)
        '        _CostCount = value
        '    End Set
        'End Property

        'Public Property GoshaStart() As Integer
        '    Get
        '        Return _GoshaStart
        '    End Get
        '    Set(ByVal value As Integer)
        '        _GoshaStart = value
        '    End Set
        'End Property

        'Public Property GoshaCount() As Integer
        '    Get
        '        Return _GoshaCount
        '    End Get
        '    Set(ByVal value As Integer)
        '        _GoshaCount = value
        '    End Set
        'End Property

#End Region

#End Region

#Region "コンストラクタ"

        ''' <summary>
        ''' コンストラクタ
        ''' </summary>
        ''' <param name="aHoyouEventCode">補用イベントコード</param>
        ''' <param name="frm"></param>
        ''' <remarks></remarks>
        Public Sub New(ByVal aHoyouEventCode As String, ByVal frm As HoyouBuhinFrm41DispOldBuhinSelector)

            '前画面引継
            _HoyouEventCode = aHoyouEventCode

            'E-BOM部品情報取込
            _frmDispOldBuhinSelector = frm

        End Sub

#End Region

#Region "ヘッダー部初期化"
        ''' <summary>
        '''ヘッダー部を初期化する
        ''' </summary>
        ''' <remarks></remarks>
        Public Sub InitializeHeader()

            Dim frm As HoyouBuhinFrm41DispOldBuhinSelector = _frmDispOldBuhinSelector

            'ヘッダー部分の初期設定
            ShisakuFormUtil.setTitleVersion(frm)
            ShisakuFormUtil.SetDateTimeNow(frm.LblDateNow, frm.LblTimeNow)
            ShisakuFormUtil.SetIdAndBuka(frm.LblCurrUserId, frm.LblCurrBukaName)

            '画面のPG-IDが表示されます。
            frm.LblCurrPGId.Text = "PG-ID :" + "TEST"

            ''Spreadの初期化
            SpreadUtil.Initialize(frm.spdParts)
            SetSpdColTag()
            SetSpdDataField()
            ''spreadにデータを設定する
            SetSpreadSource()
        End Sub

#End Region

#Region "データテーブル初期化"

#Region "spreadのbind"
        Private Sub SetSpreadSource()
            'SPREADのデータソースを設定する
            _frmDispOldBuhinSelector.spdParts_Sheet1.DataSource = GetIchiranList()
            'SPREADの列のセルの水平方向の配置を再設定する。
            SetSpdColPro()
        End Sub
#End Region

#Region " SPREADの列のデータフィールドを設定する "
        ''' <summary>
        ''' SPREADの列のデータフィールドを設定する        ''' </summary>
        ''' <remarks></remarks>
        Public Sub SetSpdDataField()

            '列の項目を設定する           
            For i As Integer = 0 To _frmDispOldBuhinSelector.spdParts_Sheet1.ColumnCount - 1
                _frmDispOldBuhinSelector.spdParts_Sheet1.Columns(i).DataField _
                    = _frmDispOldBuhinSelector.spdParts_Sheet1.Columns(i).Tag
            Next
        End Sub
#End Region

#Region " SPREADの列のタグ値を設定する "
        ''' <summary>
        ''' SPREADの列のタグ値を設定する        ''' </summary>
        ''' <remarks></remarks>
        Public Sub SetSpdColTag()
            '列の項目を設定する           
            _frmDispOldBuhinSelector.spdParts_Sheet1.Columns(0).Tag = TAG_HOYOU_EVENT_CODE
            _frmDispOldBuhinSelector.spdParts_Sheet1.Columns(1).Tag = TAG_TANTO_FUYOU
            _frmDispOldBuhinSelector.spdParts_Sheet1.Columns(2).Tag = TAG_HOYOU_BUKA_CODE
            _frmDispOldBuhinSelector.spdParts_Sheet1.Columns(3).Tag = TAG_HOYOU_TANTO
            _frmDispOldBuhinSelector.spdParts_Sheet1.Columns(4).Tag = TAG_HOYOU_TANTO_NAME
            _frmDispOldBuhinSelector.spdParts_Sheet1.Columns(5).Tag = TAG_HOYOU_TANTO_KAITEI_NO
            _frmDispOldBuhinSelector.spdParts_Sheet1.Columns(6).Tag = TAG_TANTO_MEMO
            _frmDispOldBuhinSelector.spdParts_Sheet1.Columns(7).Tag = TAG_SAISYU_KOUSHINBI
            _frmDispOldBuhinSelector.spdParts_Sheet1.Columns(8).Tag = TAG_TANTO_SYOUNIN_JYOUTAI
            _frmDispOldBuhinSelector.spdParts_Sheet1.Columns(9).Tag = TAG_KACHOU_SYOUNIN_JYOUTAI
        End Sub
#End Region

#Region "SPREADで 列のセルの水平方向の配置を設定する。行と列のサイズを変更できないことを設定する。"
        Public Sub SetSpdColPro()
            'm_spCom.GetColFromTag(TAG_HOYOU_EVENT_CODE).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Left
            'm_spCom.GetColFromTag(TAG_HOYOU_BUKA_CODE).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Left
            'm_spCom.GetColFromTag(TAG_HOYOU_TANTO).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Left
            'm_spCom.GetColFromTag(TAG_HOYOU_TANTO_NAME).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Left
            'm_spCom.GetColFromTag(TAG_HOYOU_TANTO_KAITEI_NO).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Left
            'm_spCom.GetColFromTag(TAG_TANTO_MEMO).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Right
            'm_spCom.GetColFromTag(TAG_SAISYU_KOUSHINBI).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Right
            'm_spCom.GetColFromTag(TAG_TANTO_SYOUNIN_JYOUTAI).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Left
            'm_spCom.GetColFromTag(TAG_KACHOU_SYOUNIN_JYOUTAI).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Left

            '列の幅は変更できるようにする。　2011/2/17　By柳沼
            'For i As Integer = 0 To m_view.spdParts_Sheet1.ColumnCount - 1
            '    m_view.spdParts_Sheet1.Columns(i).Resizable = False
            'Next

            For i As Integer = 0 To _frmDispOldBuhinSelector.spdParts_Sheet1.RowCount - 1
                _frmDispOldBuhinSelector.spdParts_Sheet1.Rows(i).Resizable = False
            Next
        End Sub
#End Region

#Region "列が自動ソートされたら　処理する "
        'Public Sub AutoSortedColumn(ByVal sender As System.Object, ByVal e As FarPoint.Win.Spread.AutoSortedColumnEventArgs)
        '    'ResetColor()
        '    'filterAndSort.SortItem = _frmDispOldBuhinSelector.spdParts_Sheet1.Columns(e.Column).Tag
        '    'filterAndSort.SortAscending = e.Ascending
        'End Sub

        '''' <summary>
        '''' 列の自動フィルタリングが実行されたら　処理する
        '''' </summary>
        '''' <param name="sender"></param>
        '''' <param name="e"></param>
        '''' <remarks></remarks>
        'Private Sub spdParts_AutoFilteredColumn(ByVal sender As System.Object, ByVal e As FarPoint.Win.Spread.AutoFilteredColumnEventArgs) Handles spdParts.AutoFilteredColumn
        '    m_buhinSakuseiList.AutoFilteredColumn(sender, e)
        'End Sub

#End Region

#Region "列の自動フィルタリングが実行されたら　処理する "
        'Public Sub AutoFilteredColumn(ByVal sender As System.Object, ByVal e As FarPoint.Win.Spread.AutoFilteredColumnEventArgs)
        '    'ResetColor()
        '    'Select Case _frmDispOldBuhinSelector.spdParts_Sheet1.Columns(e.Column).Tag
        '    '    Case TAG_HOYOU_KAIHATSU_FUGO
        '    '        filterAndSort.KaihatsuFugo = FilterStringFormat(e.FilterString)
        '    '    Case TAG_HOYOU_EVENT_PHASE_NAME
        '    '        filterAndSort.EventPhaseName = FilterStringFormat(e.FilterString)
        '    '    Case TAG_STATUS_NAME
        '    '        filterAndSort.StatusName = FilterStringFormat(e.FilterString)
        '    'End Select
        'End Sub

        'Public Function FilterStringFormat(ByVal filterString As String) As String
        '    If filterString.Equals(m_view.spdParts_Sheet1.RowFilter.NonBlanksString) Then
        '        Return m_view.spdParts_Sheet1.RowFilter.NonBlanksString

        '    End If
        '    If filterString.Equals(m_view.spdParts_Sheet1.RowFilter.AllString) Then
        '        Return m_view.spdParts_Sheet1.RowFilter.AllString
        '    End If
        '    If filterString.Equals(m_view.spdParts_Sheet1.RowFilter.BlanksString) Then
        '        Return m_view.spdParts_Sheet1.RowFilter.BlanksString
        '    End If
        '    Return filterString
        'End Function
#End Region

#Region "バックカラーを戻る "
        ''' <summary>
        ''' バックカラーを戻る
        ''' </summary>
        ''' <remarks></remarks>
        Public Sub ResetColor()
            For i As Integer = 0 To _frmDispOldBuhinSelector.spdParts_Sheet1.RowCount - 1
                ShisakuFormUtil.initlColor(_frmDispOldBuhinSelector.spdParts_Sheet1.Rows(i))
            Next
        End Sub

#End Region
#Region " spreadでデータを取得する "
        Public Function GetIchiranList() As DataTable
            Dim dtData As New DataTable

            Using db As New SqlAccess(g_kanrihyoIni(DB_KEY_EBOM))
                db.Open()
                db.Fill(GetHoyouSekkeiTantoSql(), dtData)
            End Using

            For i As Integer = 0 To dtData.Rows.Count - 1

                ' ダブルクリックされた「補用部品編集情報」を読み出す
                Dim vos As List(Of ShisakuCommon.Db.EBom.Vo.THoyouBuhinEditVo) _
                    = ChkHoyouBuhinEdit(dtData.Rows(i)(TAG_HOYOU_EVENT_CODE).ToString, _
                                        dtData.Rows(i)(TAG_HOYOU_BUKA_CODE).ToString, _
                                        dtData.Rows(i)(TAG_HOYOU_TANTO).ToString, _
                                        dtData.Rows(i)(TAG_HOYOU_TANTO_KAITEI_NO).ToString)

                If vos.Count = 0 Then
                    'レコード削除
                    dtData.Rows(i).Delete()
                    Continue For
                End If


                Dim Str As String = String.Empty
                '担当不要
                Str = dtData.Rows(i)(TAG_TANTO_FUYOU).ToString.Trim
                If Str.Equals(THoyouSekkeiTantoVoHelper.TantoFuyou.UNNECESSARY) Then
                    dtData.Rows(i)(TAG_TANTO_FUYOU) = "不要"
                Else
                    dtData.Rows(i)(TAG_TANTO_FUYOU) = String.Empty
                End If

                '最終更新日
                Str = dtData.Rows(i)(TAG_SAISYU_KOUSHINBI).ToString.Trim
                If Str.Equals("0") = True Then
                    dtData.Rows(i)(TAG_SAISYU_KOUSHINBI) = String.Empty
                Else
                    dtData.Rows(i)(TAG_SAISYU_KOUSHINBI) = Str.Substring(0, 4) & "/" & Str.Substring(4, 2) & "/" & Str.Substring(6, 2)
                End If

                '担当承認状態
                Str = ShisakuComFunc.getBlockJyoutaiMoji(dtData.Rows(i)(TAG_TANTO_SYOUNIN_JYOUTAI).ToString)
                dtData.Rows(i)(TAG_TANTO_SYOUNIN_JYOUTAI) = Str
                '課長主査承認状態
                Str = ShisakuComFunc.getBlockJyoutaiMoji(dtData.Rows(i)(TAG_KACHOU_SYOUNIN_JYOUTAI).ToString)
                dtData.Rows(i)(TAG_KACHOU_SYOUNIN_JYOUTAI) = Str
            Next


            Return dtData
        End Function
#End Region

#End Region


#Region "データセット"

#Region " 補用過去部品表　検索画面用の取得SQL "

        ''' <summary>
        ''' 「補用設計担当情報」取得SQL
        ''' </summary>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Shared Function GetHoyouSekkeiTantoSql() As String
            Dim sql As New System.Text.StringBuilder()
            With sql
                .Remove(0, .Length)
                .AppendLine("SELECT ")
                .AppendLine(TAG_HOYOU_EVENT_CODE & ", ")
                .AppendLine(TAG_TANTO_FUYOU & ", ")
                .AppendLine(TAG_HOYOU_BUKA_CODE & ", ")
                .AppendLine(TAG_HOYOU_TANTO & ", ")
                .AppendLine(TAG_HOYOU_TANTO_NAME & ", ")
                .AppendLine(TAG_HOYOU_TANTO_KAITEI_NO & ", ")
                .AppendLine(TAG_TANTO_MEMO & ", ")
                .AppendLine("STR(" & TAG_SAISYU_KOUSHINBI & ") AS " & TAG_SAISYU_KOUSHINBI & ", ")
                .AppendLine(TAG_TANTO_SYOUNIN_JYOUTAI & ", ")
                .AppendLine(TAG_KACHOU_SYOUNIN_JYOUTAI & ", ")
                .AppendLine("HOYOU_TANTO_HYOUJI_JUN")

                .AppendLine("FROM " + MBOM_DB_NAME + ".dbo.T_HOYOU_SEKKEI_TANTO ")

                '.AppendLine(" WHERE TANTO_FUYOU = '0' ")  '0:必要
                '.AppendLine(" AND KACHOU_SYOUNIN_JYOUTAI = '36' ")     '36:完了データ

                .AppendLine(" ORDER BY")
                .AppendLine(" HOYOU_EVENT_CODE,")
                .AppendLine(" HOYOU_BUKA_CODE,")
                .AppendLine(" HOYOU_TANTO_HYOUJI_JUN,")
                .AppendLine(" HOYOU_TANTO,")
                .AppendLine(" HOYOU_TANTO_KAITEI_NO")
            End With
            Return sql.ToString()
        End Function
#End Region


#Region " 「補用部品表 編集」画面へのデータセット "

        ''' <summary>
        ''' ダブルクリックされた「補用部品編集情報」を読み出す
        ''' </summary>
        ''' <remarks></remarks>
        Public Function ChkHoyouBuhinEdit(ByVal EventCode As String, ByVal BukaCode As String, ByVal Tanto As String, ByVal KaiteiNo As String) As List(Of THoyouBuhinEditVo)

            Try

                Dim vos As New List(Of THoyouBuhinEditVo)

                Dim db As New ShisakuCommon.Db.EBom.EBomDbClient
                Dim sb2 As New StringBuilder
                '補用部品編集情報
                With sb2
                    .Remove(0, .Length)

                    .AppendLine(" SELECT * ")
                    .AppendLine(" FROM " & MBOM_DB_NAME & ".dbo.T_HOYOU_BUHIN_EDIT ")
                    .AppendLine(" WHERE  ")
                    .AppendLine(" HOYOU_EVENT_CODE = '" & EventCode & "'")
                    .AppendLine(" AND HOYOU_BUKA_CODE = '" & BukaCode & "'")
                    .AppendLine(" AND HOYOU_TANTO = '" & Tanto & "'")
                    .AppendLine(" AND HOYOU_TANTO_KAITEI_NO = '" & KaiteiNo & "'")
                    .AppendLine(" AND NOT INSU_SURYO IS NULL ")
                    .AppendLine(" AND INSU_SURYO > 0 ")
                    .AppendLine(" ORDER BY BUHIN_NO_HYOUJI_JUN ")
                End With

                Dim BuhinList As New List(Of THoyouBuhinEditVo)

                BuhinList = db.QueryForList(Of THoyouBuhinEditVo)(sb2.ToString)

                For Each BuhinVo As THoyouBuhinEditVo In BuhinList

                    If BuhinVo.BuhinNo.TrimEnd = "" Then
                        Continue For
                    End If

                    Dim vo As New ShisakuCommon.Db.EBom.Vo.THoyouBuhinEditVo
                    'レコード全体をコピー
                    vo = BuhinVo

                    ''補用イベントコード
                    'vo.HoyouEventCode = hoyouEventCode
                    ''補用部課コード
                    'vo.HoyouBukaCode = hoyouBukaCode
                    ''補用担当
                    'vo.HoyouTanto = vo.HoyouTanto
                    ''補用担当改訂№
                    'vo.HoyouTantoKaiteiNo = String.Empty

                    vos.Add(vo)
                Next

                Return vos

            Catch ex As Exception
                Console.WriteLine("Error:" & ex.Message)
                Return New List(Of THoyouBuhinEditVo)
            End Try

        End Function
#End Region

#End Region

    End Class

End Namespace
