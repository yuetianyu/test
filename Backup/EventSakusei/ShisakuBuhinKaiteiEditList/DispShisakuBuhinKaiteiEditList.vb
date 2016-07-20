Imports EBom.Common.mdlConstraint
Imports EBom.Common
Imports EBom.Data
Imports FarPoint.Win
Imports ShisakuCommon
Imports ShisakuCommon.Ui
Imports ShisakuCommon.Ui.Spd

Namespace DispShisakuBuhinEditList


    Public Class DispShisakuBuhinKaiteiEditList

        Private m_view As New ShisakuBuhinKaiteiEditList.Frm36DispShisakuBuhinKaiteiEditList
        Private m_spCom As SpreadCommon

        Public Sub New(ByVal f As ShisakuBuhinKaiteiEditList.Frm36DispShisakuBuhinKaiteiEditList)
            m_view = f
            m_spCom = New SpreadCommon(m_view.spdParts)
        End Sub



#Region "  '試作部品表 改訂編集一覧初期化 "

        Public Sub InitView()
            '画面のTitle VerNo.
            ShisakuFormUtil.setTitleVersion(m_view)
            '画面の時間が表示されます。
            'ShisakuComFunc.SetDateTimeNow(m_view.LblDateNow, m_view.LblTimeNow)
            ''画面の時間が表示されます。
            'ShisakuComFunc.SetIdAndBuka(m_view.LblCurrUserId, m_view.LblCurrBukaName)
            ''画面のPG-IDが表示されます。
            'm_view.LblCurrPGId.Text = "PG-ID :" + PROGRAM_ID_
            ''画面のPG-Nameが表示されます。
            'm_view.LblCurrPGName.Text = PROGRAM_NAME_
            ''画面のMessageが表示されます。(ダブルクリックで選択してください。)
            'm_view.lblMsg.Text = T000
            m_view.txtIbentoNo.Focus()

            'Spreadのデータが取得。
            SetDataList()

        End Sub

        Public Sub SetDataList()

            '画面Open時、全てデーターが表示されます(権限1と2)。
            m_view.spdParts_Sheet1.Columns(0).DataField = "SHISAKU_EVENT_CODE"
            m_view.spdParts_Sheet1.Columns(1).DataField = "SHISAKU_KAIHATSU_FUGO"
            m_view.spdParts_Sheet1.Columns(2).DataField = "SHISAKU_EVENT_PHASE_NAME"
            m_view.spdParts_Sheet1.Columns(3).DataField = "UNIT_KBN"
            m_view.spdParts_Sheet1.Columns(4).DataField = "SHISAKU_EVENT_NAME"
            m_view.spdParts_Sheet1.Columns(5).DataField = "DAISU"
            m_view.spdParts_Sheet1.Columns(6).DataField = "KAITEI_SYOCHI_SHIMEKIRIBI"

            m_view.spdParts.ActiveSheet.RowHeader.Columns(0).Width = 35
            m_view.spdParts_Sheet1.SetRowSizeable(m_view.spdParts_Sheet1.RowCount, False)

            'Get一覧画面のデータ
            'm_view.spdParts_Sheet1.DataSource = GetDataListInfo()

            SetSpreadInit()
        End Sub

#End Region


#Region " SPREAD初期化を設定する "

        Public Sub SetSpreadInit()

            m_view.spdParts_Sheet1.SetColumnWidth(0, 80)
            m_view.spdParts_Sheet1.SetColumnWidth(1, 150)
            m_view.spdParts_Sheet1.SetColumnWidth(2, 75)
            m_view.spdParts_Sheet1.SetColumnWidth(3, 56)
            m_view.spdParts_Sheet1.SetColumnWidth(4, 85)
            m_view.spdParts_Sheet1.SetColumnWidth(5, 85)
            m_view.spdParts_Sheet1.SetColumnWidth(5, 85)
            m_view.spdParts_Sheet1.Columns(0).HorizontalAlignment = Spread.CellHorizontalAlignment.Left
            m_view.spdParts_Sheet1.Columns(1).HorizontalAlignment = Spread.CellHorizontalAlignment.Left
            m_view.spdParts_Sheet1.Columns(2).HorizontalAlignment = Spread.CellHorizontalAlignment.Left
            m_view.spdParts_Sheet1.Columns(3).HorizontalAlignment = Spread.CellHorizontalAlignment.Left
            m_view.spdParts_Sheet1.Columns(4).HorizontalAlignment = Spread.CellHorizontalAlignment.Left
            m_view.spdParts_Sheet1.Columns(5).HorizontalAlignment = Spread.CellHorizontalAlignment.Left
            m_view.spdParts_Sheet1.Columns(6).HorizontalAlignment = Spread.CellHorizontalAlignment.Right
            m_view.spdParts_Sheet1.Columns(0).Resizable = False
            m_view.spdParts_Sheet1.Columns(1).Resizable = False
            m_view.spdParts_Sheet1.Columns(2).Resizable = False
            m_view.spdParts_Sheet1.Columns(3).Resizable = False
            m_view.spdParts_Sheet1.Columns(4).Resizable = False
            m_view.spdParts_Sheet1.Columns(5).Resizable = False
            m_view.spdParts_Sheet1.Columns(6).Resizable = False

            m_view.spdParts.FocusRenderer = New FarPoint.Win.Spread.MarqueeFocusIndicatorRenderer(Color.Blue, 1)

        End Sub

        Public Function GetDataListInfo(ByVal strUserId As String, ByVal strUserName As String, ByVal strUserDepart As String, ByVal strSite As String) As DataTable
            'データーテーブル
            Dim dtData As New DataTable
            'DB Access
            Using db As New SqlAccess(g_kanrihyoIni(DB_KEY_KOSEI))
                'DB Open
                db.Open()

                'Spreadのデータが検索
                'db.Fill(DataSqlCommon.GetDisplaySql(), dtData)
            End Using

            Return dtData
        End Function

#End Region

#Region "遷移画面Open"

        Public Sub InitUpdateView()

        End Sub
#End Region

    End Class


End Namespace