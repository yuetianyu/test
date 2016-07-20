Imports FarPoint.Win.Spread
Imports FarPoint.Win.Spread.CellType
Imports System.Reflection
Imports ShisakuCommon
Imports ShisakuCommon.Ui
Imports ShisakuCommon.Ui.Spd
Imports ShisakuCommon.Util.LabelValue
Imports EventSakusei.ShisakuBuhinEdit.Kosei.Logic

Namespace ShisakuBuhinEdit
    ''' <summary>
    ''' 部品構成表示画面
    ''' </summary>
    ''' <remarks></remarks>
    Public Class Frm41DispShisakuBuhinEdit25

#Region "local member"
        ''' <summary>カラム</summary>
        Private _StrColumn As Integer
        ''' <summary>FpSpread共通メソッド</summary>
        Private m_spCom As SpreadCommon

#End Region

        Public Sub New(ByVal KoseiSubject As BuhinEditKoseiSubject)

            ' This call is required by the Windows Form Designer.
            InitializeComponent()
            ShisakuFormUtil.Initialize(Me)

            spdParts.FocusRenderer = New FarPoint.Win.Spread.MarqueeFocusIndicatorRenderer(Color.Blue, 1)
            m_spCom = New SpreadCommon(spdParts)

            Me._KoseiSubject = KoseiSubject

            InitializeHeader()

            initColumn()

        End Sub

        Private _KoseiSubject As BuhinEditKoseiSubject

#Region "初期化"
        ''' <summary>
        ''' 画面ヘーダ部分の初期化
        ''' </summary>
        ''' <remarks></remarks>
        Public Sub InitializeHeader()
            ShisakuFormUtil.setTitleVersion(Me)
        End Sub

        ''' <summary>
        ''' 画面オープン時カラムをコンボボックスへ設定する
        ''' </summary>
        Private Sub initColumn()
            FormUtil.BindLabelValuesToComboBox(cmbCloumn, GetLabelValues_Column)
        End Sub

        ''' <summary>
        ''' カラムコンボボックスへラベル、値を設定。
        ''' 後でINSTL品番と紐付け、必要分のみ設定する。
        ''' </summary>
        Public Shared Function GetLabelValues_Column() As List(Of LabelValueVo)

            Dim results As New List(Of LabelValueVo)
            Dim index As Integer = 0
            Dim alphabet As String = ""

            For index = 0 To 1000
                alphabet = EzUtil.ConvIndexToAlphabet(index)
                results.Add(New LabelValueVo(alphabet, index))
            Next

            Return results
        End Function

        ''' <summary>スプレッドの初期表示行</summary>
        Public Const SPREAD_DEFAULT_ROW_COUNT As Integer = 1000

        ''' <summary>
        ''' 部品構成一覧画面のSpreadのタイトル行数を返す
        ''' </summary>
        ''' <param name="spreadSheet">SheetView</param>
        ''' <returns>タイトル行数</returns>
        ''' <remarks></remarks>
        Public Shared Function GetTitleRows(ByVal spreadSheet As SheetView) As Integer
            If spreadSheet.RowCount > 0 Then
                Return spreadSheet.Cells(0, 0).RowSpan
            Else : Return 0
            End If

        End Function

        ''' <summary>
        ''' 部品構成一覧画面のSpread初期化
        ''' </summary>
        ''' <param name="aSpread">対象Spread</param>
        ''' <remarks></remarks>
        Public Shared Sub InitializeFrm9(ByVal aSpread As FpSpread)

            SpreadUtil.Initialize(aSpread)
            '
            Dim my_font As New Font("MS Gothic", 9)
            aSpread.ActiveSheet.Columns.Get(1).Font = my_font

            If aSpread.Sheets.Count = 0 Then
                Throw New ArgumentException
            End If
            Dim sheet As SheetView = aSpread.Sheets(0)

            Dim im As FarPoint.Win.Spread.InputMap = aSpread.GetInputMap(FarPoint.Win.Spread.InputMapMode.WhenFocused)

            sheet.RowCount = GetTitleRows(sheet)
            sheet.RowCount = GetTitleRows(sheet) + SPREAD_DEFAULT_ROW_COUNT
        End Sub
#End Region

#Region "SpreadのColumnのTag"
        ''' <summary>レベル</summary>
        Private ReadOnly TAG_LEVEL As String = "LEVEL_Column"
        ''' <summary>部品番号</summary>
        Private ReadOnly TAG_BUHIN_NO As String = "BUHIN_NO_Column"
        ''' <summary>部品名称</summary>
        Private ReadOnly TAG_BUHIN_NAME As String = "BUHIN_NAME_Column"
#End Region

#Region "スプレッド表示"

        ''' <summary>
        ''' 画面のカラムを受取ってスプレッドを再表示する。
        ''' </summary>
        Private Sub cmbCloumn_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbCloumn.SelectedIndexChanged
            _StrColumn = Integer.Parse(cmbCloumn.SelectedValue)
            If _StrColumn >= 0 Then
                InitializeFrm9(spdParts)

                initSpreadData()
            End If
        End Sub

        ''' <summary>
        ''' スプレッドデータを設定する
        ''' </summary>
        Private Sub initSpreadData()
            Dim sheet = spdParts_Sheet1
            Dim index As Integer = 0

            sheet.Columns(EzUtil.Increment(index)).Tag = TAG_LEVEL
            sheet.Columns(EzUtil.Increment(index)).Tag = TAG_BUHIN_NO
            sheet.Columns(EzUtil.Increment(index)).Tag = TAG_BUHIN_NAME

            updateSpreadData()

        End Sub

#End Region

#Region "spread　一覧設定"
        ''' <summary>
        ''' spread　一覧を表示する
        ''' </summary>
        ''' <remarks></remarks>
        Private Sub updateSpreadData()
            'Dim LevelNo As Integer
            Dim BuhinYohaku As String = ""
            Dim RowNo As Integer = 0
            Dim ColumnIndex As Integer = _StrColumn
            For Each rowIndex As Integer In _KoseiSubject.GetInputRowIndexes()
                With _KoseiSubject.Matrix.Record(rowIndex)

                    If Not (StringUtil.IsEmpty(_KoseiSubject.InsuSuryo(rowIndex, ColumnIndex))) Then

                        m_spCom.GetCell(TAG_LEVEL, RowNo).Text = .Level

                        '以下のロジック再検討。
                        m_spCom.GetCell(TAG_LEVEL, RowNo).BackColor = Nothing
                        m_spCom.GetCell(TAG_BUHIN_NO, RowNo).BackColor = Nothing
                        m_spCom.GetCell(TAG_BUHIN_NAME, RowNo).BackColor = Nothing
                        Select Case Integer.Parse(.Level)
                            Case 0
                                BuhinYohaku = ""
                                m_spCom.GetCell(TAG_LEVEL, RowNo).BackColor = Color.DarkGray
                                m_spCom.GetCell(TAG_BUHIN_NO, RowNo).BackColor = Color.DarkGray
                                m_spCom.GetCell(TAG_BUHIN_NAME, RowNo).BackColor = Color.DarkGray
                            Case 1
                                BuhinYohaku = "  "
                                m_spCom.GetCell(TAG_LEVEL, RowNo).BackColor = Color.Gainsboro
                                m_spCom.GetCell(TAG_BUHIN_NO, RowNo).BackColor = Color.Gainsboro
                                m_spCom.GetCell(TAG_BUHIN_NAME, RowNo).BackColor = Color.Gainsboro
                            Case 2
                                BuhinYohaku = "    "
                            Case 3
                                BuhinYohaku = "      "
                            Case 4
                                BuhinYohaku = "        "
                            Case Else
                                BuhinYohaku = "          "
                        End Select

                        m_spCom.GetCell(TAG_BUHIN_NO, RowNo).Text = BuhinYohaku & .BuhinNo
                        m_spCom.GetCell(TAG_BUHIN_NAME, RowNo).Text = .BuhinName
                        .BuhinNoHyoujiJun = RowNo

                        'カウント
                        RowNo = RowNo + 1
                    End If
                End With
            Next
            '該当データ無しの処理
            If RowNo = 0 Then
                '色を変える。
                m_spCom.GetCell(TAG_LEVEL, RowNo).BackColor = Color.Red
                m_spCom.GetCell(TAG_BUHIN_NO, RowNo).BackColor = Color.Red
                m_spCom.GetCell(TAG_BUHIN_NO, RowNo).ForeColor = Color.White
                m_spCom.GetCell(TAG_BUHIN_NAME, RowNo).BackColor = Color.Red
                'メッセージを表示する。
                m_spCom.GetCell(TAG_LEVEL, RowNo).Text = ""
                m_spCom.GetCell(TAG_BUHIN_NO, 0).Text = "構成がありません。"
                m_spCom.GetCell(TAG_BUHIN_NAME, RowNo).Text = ""
            End If
        End Sub

#End Region

    End Class

End Namespace