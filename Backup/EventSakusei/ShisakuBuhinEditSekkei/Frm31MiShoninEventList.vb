Imports EventSakusei.ShisakuBuhinEditSekkei
Imports EventSakusei.ShisakuBuhinEditBlock
Imports ShisakuCommon
Imports EBom.Common
Imports EBom.Data
Imports ShisakuCommon.Ui
Imports EventSakusei.ShisakuBuhinEditSekkei.Dao
Imports ShisakuCommon.Db.EBom.Vo
Imports ShisakuCommon.Db.EBom.Vo.Helper
Imports ShisakuCommon.Ui.Spd
Imports System.Text
Namespace ShisakuBuhinEditSekkei
    Public Class Frm31MiShoninEventList
        Private _strBukaCode As String
        Private lnkEventInfos() As System.Windows.Forms.LinkLabel
        'Private lnkPreviousEventInfos() As System.Windows.Forms.LinkLabel
        Private anchorHash As Hashtable
        Private _strMode As String
        ''開発符号
        Private _KaihatuFugou As String
        ''イベント名
        Private _IbentoName As String
        ''処置期限
        Private _Syochikigen As String
        ''あとXX日
        Private _Syochikigen2 As String
        '手配帳作成日
        Private _strTeihaiDate As String
        Private _iEventCount As String



        Private Sub Frm31MiShoninEventList_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

            ShisakuFormUtil.setTitleVersion(Me)

            '未承認ブロックのリストを作る
            CreateMiShoninLink()

        End Sub
        ''' <summary>
        ''' 動的に未承認リンクを作成する
        ''' </summary>
        ''' <remarks></remarks>
        Private Sub CreateMiShoninLink()

            'Me.lnkPreviousEventInfos = New System.Windows.Forms.LinkLabel(_lt.Count) {}

            '未承認ブロックを有するイベントを抽出する

            If Not Me.lnkEventInfos Is Nothing Then
                For j As Integer = 0 To _iEventCount - 1
                    Me.lnkEventInfos(j).Dispose()
                Next
            End If

            '--------------------------------------------------------

            '未承認ブロックを有するイベントを抽出する
            Dim lt As List(Of Hashtable) = Me.FindMiShoninBlock()
            If lt.Count = 0 Then
                Me.Close()
            End If
            Dim i As Integer = 0

            Me.lnkEventInfos = New System.Windows.Forms.LinkLabel(lt.Count) {}

            Dim kaImpl As New ShisakuBuhinEditBlock.Dao.KaRyakuNameDaoImpl

            _iEventCount = 0
            anchorHash = New Hashtable
            For Each vo As Hashtable In lt

                '動的コントロールのプロパティを設定する
                Me.lnkEventInfos(i) = New System.Windows.Forms.LinkLabel
                Me.lnkEventInfos(i).Name = "lnkEventInfo" + i.ToString

                Dim kaName As New Rhac1560Vo

                kaName = kaImpl.GetKa_Ryaku_Name(vo("SHISAKU_BUKA_CODE").ToString)

                ''課略称
                'CallShisakuBuhinEditBlocktxt.StrDeptNo = kaName.KaRyakuName

                'Me.lnkEventInfos(i).Text = " ▪ " + vo("SHISAKU_EVENT_NAME").ToString + vo("SHISAKU_BUKA_CODE").ToString
                Me.lnkEventInfos(i).Text = " ▪ " + vo("SHISAKU_EVENT_NAME").ToString + kaName.KaRyakuName

                'サイズと位置を設定する
                Me.lnkEventInfos(i).Location = New Point(90, 10 + 30 * i)
                Me.lnkEventInfos(i).Size = New System.Drawing.Size(450, 25)
                Me.lnkEventInfos(i).Font = New Font("MS UI Gothic", 14.25, FontStyle.Bold)
                'MS UI Gothic, 14.25pt, style=Bold

                'フォームに追加する
                Me.Controls.Add(Me.lnkEventInfos(i))
                Me.lnkEventInfos(i).Parent = Me.pnlEventView

                AddHandler Me.lnkEventInfos(i).Click, AddressOf Me.lnkEventInfos_LinkClicked
                Dim ht As Hashtable = New Hashtable
                ht.Add("SHISAKU_EVENT_CODE", vo("SHISAKU_EVENT_CODE").ToString)
                ht.Add("SHISAKU_BUKA_CODE", vo("SHISAKU_BUKA_CODE").ToString)
                ht.Add("STATUS", vo("STATUS").ToString)
                anchorHash.Add(Me.lnkEventInfos(i).Name.ToString, ht)
                i = i + 1
                _iEventCount = _iEventCount + 1
            Next
            Me.pnlEventView.AutoScroll = True

        End Sub

        Private Sub lnkEventInfos_LinkClicked(ByVal sender As Object, _
        ByVal e As EventArgs)
            'クリックされたボタンの情報を取得する
            Dim ht As New Hashtable
            ht = anchorHash(CType(sender, System.Windows.Forms.LinkLabel).Name)
            Me.Hide()
            Me.CallShisakuBuhinEditBlock(ht("SHISAKU_EVENT_CODE"), ht("SHISAKU_BUKA_CODE"), ht("STATUS"))

            CreateMiShoninLink()
            Me.Show()
        End Sub
        Private Sub btnBACK_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
            Me.Close()
        End Sub
        ''' <summary>
        ''' 
        ''' </summary>
        ''' <param name="strBukaCode">部課コード</param>
        ''' <remarks></remarks>
        Public Sub New(ByVal strBukaCode As String)

            Me._strBukaCode = strBukaCode

            ' この呼び出しは、Windows フォーム デザイナで必要です。
            InitializeComponent()

            ' InitializeComponent() 呼び出しの後で初期化を追加します。
            ShisakuFormUtil.Initialize(Me)


        End Sub
        Public Function FindMiShoninBlock() As List(Of Hashtable)

            Dim lt As New List(Of Hashtable)


            Dim dtResData As New DataTable()
            Using db As New SqlAccess(g_kanrihyoIni(DB_KEY_mBOM))
                db.Open()

                '------------------------------------------------
                '２次改修
                '   部課コード対策。
                '   課マスタをLEFT JOIN
                '------------------------------------------------

                Dim sql As New System.Text.StringBuilder()
                With sql
                    .Remove(0, .Length)
                    .AppendLine("SELECT")
                    .AppendLine("    SE.SHISAKU_EVENT_CODE,")
                    .AppendLine("    SE.SHISAKU_EVENT_NAME,")
                    .AppendLine("    SE.HYOJIJUN_NO,")
                    .AppendLine("    SE.STATUS,")
                    .AppendLine("    SB.SHISAKU_BUKA_CODE, ")
                    .AppendLine("    KA.KA_RYAKU_NAME ")
                    .AppendLine("FROM")
                    .AppendLine("        " + MBOM_DB_NAME + ".dbo.T_SHISAKU_SEKKEI_BLOCK SB INNER JOIN ")
                    .AppendLine("        (SELECT SHISAKU_EVENT_CODE,SHISAKU_BUKA_CODE,SHISAKU_BLOCK_NO,MAX(SHISAKU_BLOCK_NO_KAITEI_NO) AS MAX_SHISAKU_BLOCK_NO_KAITEI_NO FROM " + MBOM_DB_NAME + ".dbo.T_SHISAKU_SEKKEI_BLOCK ")
                    .AppendLine("        GROUP BY SHISAKU_EVENT_CODE,SHISAKU_BUKA_CODE,SHISAKU_BLOCK_NO) SB1 ")
                    .AppendLine("        ON SB.SHISAKU_EVENT_CODE = SB1.SHISAKU_EVENT_CODE")
                    .AppendLine("        AND  SB.SHISAKU_BUKA_CODE = SB1.SHISAKU_BUKA_CODE")
                    .AppendLine("        AND   SB.SHISAKU_BLOCK_NO = SB1.SHISAKU_BLOCK_NO")
                    .AppendLine("        AND   SB.SHISAKU_BLOCK_NO_KAITEI_NO = SB1.MAX_SHISAKU_BLOCK_NO_KAITEI_NO")
                    .AppendLine("        INNER JOIN")
                    .AppendLine("        " + MBOM_DB_NAME + ".dbo.T_SHISAKU_EVENT SE ")
                    .AppendLine("        ON SB.SHISAKU_EVENT_CODE = SE.SHISAKU_EVENT_CODE  ")
                    .AppendLine("        Left JOIN")
                    .AppendLine("        RHACLIBF.dbo.RHAC1560 KA")
                    .AppendLine("        ON SB.SHISAKU_BUKA_CODE = KA.BU_CODE + KA.KA_CODE")
                    .AppendLine("WHERE ")
                    .AppendLine("    (SB.JYOUTAI = '34' OR SB.BLOCK_FUYOU = '1')  ")
                    .AppendLine("AND SB.TANTO_SYOUNIN_JYOUTAI = '35'")
                    .AppendLine("AND (SB.KACHOU_SYOUNIN_JYOUTAI <> '36' OR SB.KACHOU_SYOUNIN_JYOUTAI IS NULL)")
                    .AppendLine("AND (SB.SHISAKU_BUKA_CODE = @SHISAKU_BUKA_CODE OR KA.KA_RYAKU_NAME = @SHISAKU_BUKA_CODE)")
                    .AppendLine("")
                    .AppendLine("GROUP BY ")
                    .AppendLine("    SE.SHISAKU_EVENT_CODE,")
                    .AppendLine("    SE.SHISAKU_EVENT_NAME,")
                    .AppendLine("    SE.HYOJIJUN_NO,")
                    .AppendLine("    SE.STATUS,")
                    .AppendLine("    SB.SHISAKU_BUKA_CODE,")
                    .AppendLine("    KA.KA_RYAKU_NAME")
                    .AppendLine("ORDER BY ")
                    .AppendLine("    SE.HYOJIJUN_NO,")
                    .AppendLine("    SB.SHISAKU_BUKA_CODE")
                End With

                db.AddParameter("@SHISAKU_BUKA_CODE", _strBukaCode, DbType.AnsiString)
                db.Fill(sql.ToString, dtResData)

                If dtResData.Rows.Count > 0 Then
                    For i As Integer = 0 To dtResData.Rows.Count - 1
                        Dim ht As Hashtable = New Hashtable
                        ht.Add("SHISAKU_EVENT_CODE", dtResData.Rows(i)("SHISAKU_EVENT_CODE"))
                        ht.Add("SHISAKU_EVENT_NAME", dtResData.Rows(i)("SHISAKU_EVENT_NAME"))
                        ht.Add("SHISAKU_BUKA_CODE", dtResData.Rows(i)("SHISAKU_BUKA_CODE"))
                        ht.Add("STATUS", dtResData.Rows(i)("STATUS"))
                        lt.Add(ht)
                    Next
                Else
                    Return lt
                End If
                Return lt

            End Using


        End Function

        ''' <summary>
        ''' 
        ''' </summary>
        ''' <param name="strEventCode"></param>
        ''' <param name="strBukaCode"></param>
        ''' <param name="strStatus"></param>
        ''' <remarks></remarks>
        Private Sub CallShisakuBuhinEditBlock(ByVal strEventCode As String, ByVal strBukaCode As String, ByVal strStatus As String)
            '

            'ステータス２１が試作編集モード
            If strStatus = "21" Then
                _strMode = ShishakuHensyuMode
            Else
                _strMode = ShishakuKaiteiHensyuMode
            End If
            Using CallShisakuBuhinEditBlocktxt As New Frm38DispShisakuBuhinEditBlock
                Dim Bu_Code As String = ""
                Dim Ka_Code As String = ""
                'Dim BuKaComonFunc As New ShisakuBuhinEditBlocktxtDaoImpl
                getEventInfo(strEventCode)
                'モード
                CallShisakuBuhinEditBlocktxt.StrMode = _strMode
                'イベントコード
                CallShisakuBuhinEditBlocktxt.StrEventCode = strEventCode
                ''開発符号
                CallShisakuBuhinEditBlocktxt.StrFugo = _KaihatuFugou
                ''イベント名
                CallShisakuBuhinEditBlocktxt.StrEventName = _IbentoName
                ''処置期限
                CallShisakuBuhinEditBlocktxt.StrDate = _Syochikigen
                ''あとXX日
                CallShisakuBuhinEditBlocktxt.StrPeriod = _Syochikigen2
                '部課コード
                CallShisakuBuhinEditBlocktxt.StrDept = strBukaCode
                '課略名を取得する'
                Dim kaImpl As New ShisakuBuhinEditBlock.Dao.KaRyakuNameDaoImpl
                Dim kaName As New Rhac1560Vo

                kaName = kaImpl.GetKa_Ryaku_Name(strBukaCode)

                ''課略称
                CallShisakuBuhinEditBlocktxt.StrDeptNo = kaName.KaRyakuName
                'SKE1最終抽出日
                CallShisakuBuhinEditBlocktxt.StrOutDate = _strTeihaiDate
                'テキストボックスの設計課情報を次画面へ引き渡す。
                CallShisakuBuhinEditBlocktxt.Execute()
                CallShisakuBuhinEditBlocktxt.ShowDialog()
            End Using
            'Inital()
        End Sub
        ''' <summary>
        ''' 
        ''' </summary>
        ''' <param name="strEventCode"></param>
        ''' <remarks></remarks>
        Private Sub getEventInfo(ByVal strEventCode As String)
            Dim strKigen As String = Nothing
            Dim strDate As String = Nothing
            Dim dtList As DataTable = New DataTable()
            Using db As New SqlAccess(g_kanrihyoIni(DB_KEY_EBOM))
                db.Open()
                If Not strEventCode = String.Empty Then
                    db.AddParameter("@SHISAKU_EVENT_CODE", strEventCode, DbType.AnsiString)
                End If
                db.Fill(DataSqlCommon.GetIbento(_strMode), dtList)
            End Using
            If dtList.Rows.Count > 0 Then
                If (Not dtList.Rows(0)("SHISAKU_KAIHATSU_FUGO").Equals(DBNull.Value)) Then
                    _KaihatuFugou = dtList.Rows(0)("SHISAKU_KAIHATSU_FUGO").ToString()
                End If
                If (Not dtList.Rows(0)("SHISAKU_EVENT_NAME").Equals(DBNull.Value)) Then
                    _IbentoName = dtList.Rows(0)("SHISAKU_EVENT_NAME").ToString()
                End If
                If (_strMode = ShishakuHensyuMode) AndAlso (Not dtList.Rows(0)("KAITEI_SYOCHI_SHIMEKIRIBI").Equals(DBNull.Value)) Then
                    strKigen = dtList.Rows(0)("KAITEI_SYOCHI_SHIMEKIRIBI")
                End If
                If (_strMode = ShishakuKaiteiHensyuMode) AndAlso (Not dtList.Rows(0)("SHIMEKIRIBI").Equals(DBNull.Value)) Then
                    strKigen = dtList.Rows(0)("SHIMEKIRIBI")
                End If
                If Not strKigen = String.Empty Then
                    _Syochikigen = strKigen.Substring(0, 4) & "/" & strKigen.Substring(4, 2) & "/" & strKigen.Substring(6, 2)
                End If

                Dim wToday As Integer = Integer.Parse(DateTime.Now.ToString("yyyyMMdd"))
                Dim wSyochikigen As Integer = Integer.Parse(strKigen)

                Dim kabetuJyoutaiDao As New ShisakuBuhinEditBlockDaoImpl
                Dim wAto As Integer = kabetuJyoutaiDao.GetKadoubi(wToday, wSyochikigen).Kadobi

                '残り日数を見てメッセージを変える。
                If wAto > 0 Then
                    '期限が切れていない時
                    _Syochikigen2 = "あと　" & wAto & "日"
                    'lblSyochikigen2.Font = New Font("MS Gothic", 11, FontStyle.Regular)
                Else
                    '期限が切れた時は以下のメッセージを出力する。
                    _Syochikigen2 = "もう過ぎてます。"
                    'lblSyochikigen2.Font = New Font("MS Gothic", 11, FontStyle.Bold)
                End If
                'If (Not dtList.Rows(0)("DIFF_DATE").Equals(DBNull.Value)) And dtList.Rows(0)("DIFF_DATE").ToString() >= 0 Then
                '    '期限が切れていない時
                '    lblSyochikigen2.Text = "あと　" & dtList.Rows(0)("DIFF_DATE").ToString() & "日"
                '    lblSyochikigen2.Font = New Font("MS Gothic", 11, FontStyle.Regular)
                'Else
                '    '期限が切れた時は以下のメッセージを出力する。
                '    lblSyochikigen2.Text = "もう過ぎてます"
                '    lblSyochikigen2.Font = New Font("MS Gothic", 11, FontStyle.Bold)
                'End If

                If Not StringUtil.IsEmpty(dtList.Rows(0)("SKEIDATE")) Then
                    strDate = dtList.Rows(0)("SKEIDATE")
                    _strTeihaiDate = strDate.Substring(0, 4) & "/" & strDate.Substring(4, 2) & "/" & strDate.Substring(6, 2)
                End If
            End If
        End Sub

        Private Sub btnBACK_Click_1(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnBACK.Click
            Me.Close()
        End Sub
    End Class
End Namespace
