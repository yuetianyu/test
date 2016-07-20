Imports EBom.Common.mdlConstraint
Imports EBom.Common
Imports EBom.Data
Imports FarPoint.Win
Imports ShisakuCommon
Imports EventSakusei.ShisakuBuhinEditSekkei
Imports ShisakuCommon.Ui
Imports ShisakuCommon.Ui.Spd

Namespace ShisakuBuhinEditList

    Public Class DispShisakuBuhinEditList
        ''試作部品表　一覧画面
        Private m_view As Frm35DispShisakuBuhinEditList
        ''Spreadの共通
        Private m_spCom As SpreadCommon

        Public Sub New(ByVal f As Frm35DispShisakuBuhinEditList)

            m_view = New Frm35DispShisakuBuhinEditList
            m_view = f
            m_spCom = New SpreadCommon(m_view.spdParts)

        End Sub


#Region "  35試作部品表 編集一覧　と　36試作部品表 改訂編集一覧 初期化 "
        ''' <summary>
        ''' 35試作部品表 編集一覧　と　36試作部品表 改訂編集一覧 初期化
        ''' </summary>
        ''' <param name="strMode">0は "編集モード" , 1は"改訂編集モード" , 2は"完了イベント閲覧モード"</param>
        ''' <remarks></remarks>
        Public Sub InitView(ByVal strMode As String)

            ''画面のTitle VerNo.
            ShisakuFormUtil.setTitleVersion(m_view)
            ''画面の時間が表示されます。
            ShisakuFormUtil.SetDateTimeNow(m_view.LblDateNow, m_view.LblTimeNow)
            ''画面の時間が表示されます。
            ShisakuFormUtil.SetIdAndBuka(m_view.LblCurrUserId, m_view.LblCurrBukaName)

            If strMode = ShishakuHensyuMode Then
                ''画面のPG-IDが表示されます。
                m_view.LblCurrPGId.Text = "PG-ID :" + PROGRAM_ID_06
                ''画面のPG-Nameが表示されます。
                m_view.LblCurrPGName.Text = PROGRAM_NAME_06
            ElseIf strMode = ShishakuKaiteiHensyuMode Then
                ''画面のPG-IDが表示されます。
                m_view.LblCurrPGId.Text = "PG-ID :" + PROGRAM_ID_07
                ''画面のPG-Nameが表示されます。
                m_view.LblCurrPGName.Text = PROGRAM_NAME_07
            Else
                '完了イベント閲覧モード
                ''画面のPG-IDが表示されます。
                m_view.LblCurrPGId.Text = "PG-ID :" + PROGRAM_ID_21
                ''画面のPG-Nameが表示されます。
                m_view.LblCurrPGName.Text = PROGRAM_NAME_21

            End If

            ''画面のMessageが表示されます。(ダブルクリックで選択してください。)
            m_view.lblMsg.Text = T0013
            m_view.txtIbentoNo.Focus()
            ''Spreadの初期化
            SpreadUtil.Initialize(m_view.spdParts)
            ''Spreadのデータが設定。
            SetDataList(strMode)

        End Sub

#Region " Spreadのデータが設定 "
        ''' <summary>
        ''' Spreadのデータが設定
        ''' </summary>
        ''' <param name="strMode">0は "編集モード" , 1は"改訂編集モード" , 2は"完了イベント閲覧モード"</param>
        ''' <remarks></remarks>
        Public Sub SetDataList(ByVal strMode As String)

            ''画面Open時、全てデーターが表示されます
            m_view.spdParts_Sheet1.Columns(0).DataField = "SHISAKU_EVENT_CODE"
            m_view.spdParts_Sheet1.Columns(1).DataField = "SHISAKU_KAIHATSU_FUGO"
            m_view.spdParts_Sheet1.Columns(2).DataField = "SHISAKU_EVENT_PHASE_NAME"
            m_view.spdParts_Sheet1.Columns(3).DataField = "UNIT_KBN"
            m_view.spdParts_Sheet1.Columns(4).DataField = "SHISAKU_EVENT_NAME"
            m_view.spdParts_Sheet1.Columns(5).DataField = "SHISAKU_DAISU"
            m_view.spdParts_Sheet1.Columns(6).DataField = "DATE"
            If strMode = ShishakuHensyuMode Then
                m_view.spdParts_Sheet1.Columns(6).DataField = "KAITEI_SYOCHI_SHIMEKIRIBI"
            Else
                m_view.spdParts_Sheet1.Columns(6).DataField = "STATUS"
            End If
            m_view.spdParts_Sheet1.Columns(7).DataField = "AlARM"
            m_view.spdParts_Sheet1.Columns(8).DataField = "DIFF_DATE"
            ''RowHeader 4桁時、幅の設定
            m_view.spdParts.ActiveSheet.RowHeader.Columns(0).Width = 35
            m_view.spdParts_Sheet1.SetRowSizeable(m_view.spdParts_Sheet1.RowCount, False)

            ''Get一覧画面のデータ
            m_view.spdParts_Sheet1.DataSource = GetDataListInfo(strMode)

            Dim rowIndex As Integer
            Dim strDateValue As String

            If strMode = ShishakuHensyuMode Then

                For rowIndex = 0 To m_view.spdParts_Sheet1.RowCount - 1
                    strDateValue = m_spCom.GetValue("DIFF_DATE", rowIndex)

                    If Convert.ToInt32(strDateValue) <= -1 Then
                        ''A0001-メッセージ : "処置期限が過ぎています。"
                        m_spCom.SetValue("ALARM", rowIndex, A0001)
                        m_view.spdParts_Sheet1.Cells.Item(rowIndex, 7).BackColor = Color.Red
                        m_view.spdParts_Sheet1.Cells.Item(rowIndex, 7).ForeColor = Color.Black
                    ElseIf Convert.ToInt32(strDateValue) >= 0 And Convert.ToInt32(strDateValue) <= 3 Then
                        ''A0002-メッセージ : "処置〆切り間近です。"
                        m_spCom.SetValue("ALARM", rowIndex, A0002)
                        m_view.spdParts_Sheet1.Cells.Item(rowIndex, 7).BackColor = Color.Yellow
                        m_view.spdParts_Sheet1.Cells.Item(rowIndex, 7).ForeColor = Color.Black

                    End If
                Next

            ElseIf strMode = ShishakuKaiteiHensyuMode Then    '*****完了閲覧モード対応*****

                For rowIndex = 0 To m_view.spdParts_Sheet1.RowCount - 1
                    strDateValue = m_spCom.GetValue("DIFF_DATE", rowIndex)

                    '2012/03/16 処置期限が過ぎています。アラームを改訂編集時も表示するように変更
                    If Convert.ToInt32(strDateValue) <= -1 Then
                        ''A0001-メッセージ : "処置期限が過ぎています。"
                        m_spCom.SetValue("ALARM", rowIndex, A0001)
                        m_view.spdParts_Sheet1.Cells.Item(rowIndex, 7).BackColor = Color.Red
                        m_view.spdParts_Sheet1.Cells.Item(rowIndex, 7).ForeColor = Color.Black
                    ElseIf Convert.ToInt32(strDateValue) >= -10 And Convert.ToInt32(strDateValue) <= 0 Then
                        ''A0003-メッセージ : "受付〆切り間近です。"
                        m_spCom.SetValue("ALARM", rowIndex, A0003)
                        m_view.spdParts_Sheet1.Cells.Item(rowIndex, 7).BackColor = Color.Yellow
                        m_view.spdParts_Sheet1.Cells.Item(rowIndex, 7).ForeColor = Color.Black
                    End If
                    'If Convert.ToInt32(strDateValue) >= -10 And Convert.ToInt32(strDateValue) <= 0 Then
                    '    ''A0003-メッセージ : "受付〆切り間近です。"
                    '    m_spCom.SetValue("ALARM", rowIndex, A0003)
                    '    m_view.spdParts_Sheet1.Cells.Item(rowIndex, 7).BackColor = Color.Yellow
                    '    m_view.spdParts_Sheet1.Cells.Item(rowIndex, 7).ForeColor = Color.Black
                    'End If
                Next

            Else

                For rowIndex = 0 To m_view.spdParts_Sheet1.RowCount - 1
                    strDateValue = m_spCom.GetValue("DIFF_DATE", rowIndex)
                    ''A0005-メッセージ : "イベントは終了しています。"
                    m_spCom.SetValue("ALARM", rowIndex, A0005)

                    '2012/03/16 処置期限が過ぎています。アラームを改訂編集時も表示するように変更
                    'If Convert.ToInt32(strDateValue) <= -1 Then
                    '    ''A0001-メッセージ : "処置期限が過ぎています。"
                    '    m_spCom.SetValue("ALARM", rowIndex, A0001)
                    '    m_view.spdParts_Sheet1.Cells.Item(rowIndex, 7).BackColor = Color.Red
                    '    m_view.spdParts_Sheet1.Cells.Item(rowIndex, 7).ForeColor = Color.Black
                    'ElseIf Convert.ToInt32(strDateValue) >= -10 And Convert.ToInt32(strDateValue) <= 0 Then
                    '    ''A0003-メッセージ : "イベントは終了しています。"
                    '    m_spCom.SetValue("ALARM", rowIndex, A0004)
                    'End If
                    'If Convert.ToInt32(strDateValue) >= -10 And Convert.ToInt32(strDateValue) <= 0 Then
                    '    ''A0003-メッセージ : "受付〆切り間近です。"
                    '    m_spCom.SetValue("ALARM", rowIndex, A0003)
                    '    m_view.spdParts_Sheet1.Cells.Item(rowIndex, 7).BackColor = Color.Yellow
                    '    m_view.spdParts_Sheet1.Cells.Item(rowIndex, 7).ForeColor = Color.Black
                    'End If
                Next

            End If
            ''Modeによって、Spreadの項目を構造します。
            SetSpreadInit(strMode)

        End Sub
#End Region

#End Region

#Region " SPREAD画面項目初期化を設定する "
        ''' <summary>
        ''' SPREAD画面項目初期化を設定する 
        ''' </summary>
        ''' <param name="strMode">0は "編集モード" , 1は"改訂編集モード" , 2は"完了イベント閲覧モード"</param>
        ''' <remarks></remarks>
        Public Sub SetSpreadInit(ByVal strMode As String)
            ''Spread項目の幅の設定
            m_view.spdParts_Sheet1.SetColumnWidth(0, 91)
            m_view.spdParts_Sheet1.SetColumnWidth(1, 71)
            m_view.spdParts_Sheet1.SetColumnWidth(2, 115)
            m_view.spdParts_Sheet1.SetColumnWidth(3, 88)
            m_view.spdParts_Sheet1.SetColumnWidth(4, 225)
            m_view.spdParts_Sheet1.SetColumnWidth(5, 51)
            If strMode = ShishakuHensyuMode Then
                m_view.spdParts_Sheet1.SetColumnWidth(6, 100)
                m_view.spdParts_Sheet1.SetColumnWidth(7, 188)
            ElseIf strMode = ShishakuKaiteiHensyuMode Then  '*****完了閲覧モード対応*****
                m_view.spdParts_Sheet1.SetColumnWidth(6, 164)
                m_view.spdParts_Sheet1.SetColumnWidth(7, 124)
            Else
                m_view.spdParts_Sheet1.SetColumnWidth(6, 130)
                m_view.spdParts_Sheet1.SetColumnWidth(7, 170)
            End If

            ''Spread項目の位置の設定
            m_view.spdParts_Sheet1.Columns(0).HorizontalAlignment = Spread.CellHorizontalAlignment.Left
            m_view.spdParts_Sheet1.Columns(1).HorizontalAlignment = Spread.CellHorizontalAlignment.Left
            m_view.spdParts_Sheet1.Columns(2).HorizontalAlignment = Spread.CellHorizontalAlignment.Left
            m_view.spdParts_Sheet1.Columns(3).HorizontalAlignment = Spread.CellHorizontalAlignment.Center
            m_view.spdParts_Sheet1.Columns(4).HorizontalAlignment = Spread.CellHorizontalAlignment.Left
            m_view.spdParts_Sheet1.Columns(5).HorizontalAlignment = Spread.CellHorizontalAlignment.Left
            m_view.spdParts_Sheet1.Columns(6).HorizontalAlignment = Spread.CellHorizontalAlignment.Right
            m_view.spdParts_Sheet1.Columns(7).HorizontalAlignment = Spread.CellHorizontalAlignment.Left
            'm_view.spdParts_Sheet1.Columns(0).Resizable = False
            'm_view.spdParts_Sheet1.Columns(1).Resizable = False
            'm_view.spdParts_Sheet1.Columns(2).Resizable = False
            'm_view.spdParts_Sheet1.Columns(3).Resizable = False
            'm_view.spdParts_Sheet1.Columns(4).Resizable = False
            'm_view.spdParts_Sheet1.Columns(5).Resizable = False
            'm_view.spdParts_Sheet1.Columns(6).Resizable = False
            'm_view.spdParts_Sheet1.Columns(7).Resizable = False
            ''Diff_Dateの値
            m_view.spdParts_Sheet1.Columns(8).Visible = False

        End Sub


#Region "一覧画面のデータGet"
        ''' <summary>
        ''' 一覧画面のデータGet
        ''' </summary>
        ''' <param name="strMode">0は "編集モード" , 1は"改訂編集モード" , 2は"完了イベント閲覧モード"</param>
        ''' <returns>データList</returns>
        ''' <remarks></remarks>
        Public Function GetDataListInfo(ByVal strMode As String) As DataTable
            ''データーテーブル
            Dim dtData As New DataTable

            Using db As New SqlAccess(g_kanrihyoIni(DB_KEY_EBOM))
                ''DB Open
                db.Open()
                ''Spreadのデータが検索
                db.Fill(DataSqlCommon.GetBuhinEditDisplaySql(strMode), dtData)
            End Using

            Return dtData
        End Function
#End Region


#End Region

#Region "遷移画面Open"

        ''' <summary>
        ''' 遷移画面のOpen
        ''' </summary>
        ''' <param name="strMode">0は "編集モード" , 1は"改訂編集モード" , 2は"完了イベント閲覧モード"</param>
        ''' <remarks></remarks>
        Public Sub InitSenIView(ByVal strMode As String)

            ''イベント番号を
            Dim strIbentoNo As String = m_view.txtIbentoNo.Text

            If String.IsNullOrEmpty(strIbentoNo) Then
                Exit Sub
            End If

            Dim dtList As DataTable = New DataTable()

            Using db As New SqlAccess(g_kanrihyoIni(DB_KEY_EBOM))
                db.Open()

                db.AddParameter("@SHISAKU_EVENT_CODE", strIbentoNo, DbType.AnsiString)
                db.AddParameter("@HITSUYOU", ShishakuSekkeiBlockHitsuyou, DbType.AnsiString)
                db.AddParameter("@SHOUCHIKANRYOU", ShishakuSekkeiBlockStatusShouchiKanryou, DbType.AnsiString)
                db.AddParameter("@SHOUNIN1", ShishakuSekkeiBlockStatusShounin1, DbType.AnsiString)
                db.AddParameter("@SHOUNIN2", ShishakuSekkeiBlockStatusShounin2, DbType.AnsiString)
                db.Fill(DataSqlCommon.GetSpreadList(), dtList)
                ''シートにデータセットを接続します。
            End Using

            '呼出し時のデータチェックは不要
            '　ベース車情報が無い場合でも次画面へ遷移し、設計課、ブロック共に新規で登録可能とする。

            'If dtList.Rows.Count > 0 Then

            ''試作部品表 編集・改訂編集（設計）画面
            Dim m_Disp37Copy As New Frm37DispShisakuBuhinEditSekkei

            ''引数を引き渡します。
            ''モード
            m_Disp37Copy.strMode = strMode
            ''イベント番号
            m_Disp37Copy.strEventCode = strIbentoNo

            ''試作部品表 編集・改訂編集（設計）画面Open
            m_Disp37Copy.ShowDialog()
            'Else
            ' ''エラー(検索したデータが存在しません。)
            'ComFunc.ShowInfoMsgBox(A0004)
            'End If


        End Sub

#End Region

    End Class
End Namespace
