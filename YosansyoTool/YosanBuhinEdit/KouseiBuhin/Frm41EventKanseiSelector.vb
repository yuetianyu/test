Imports EBom.Common
Imports ShisakuCommon.Db.EBom.Vo.Helper
Imports ShisakuCommon.Db.EBom.Dao
Imports ShisakuCommon.Ui
Imports ShisakuCommon.Db.EBom.Vo
Imports ShisakuCommon
Imports ShisakuCommon.Db.EBom.Dao.Impl
Imports YosansyoTool.YosanBuhinEdit.KouseiBuhin.Dao

Namespace YosanBuhinEdit.KouseiBuhin
    ''' <summary>
    ''' 部品構成表示画面
    ''' </summary>
    ''' <remarks></remarks>
    Public Class Frm41EventKanseiSelector
        '

        Private _resultOk As Boolean

        Private hoyouEventCode As String
        Private hoyouBukaCode As String
        Private hoyouTantoKey As String
        Private hoyouTanto As String

        Public Sub New(ByVal hoyouEventCode As String, ByVal hoyouBukaCode As String, ByVal hoyouTantoKey As String, ByVal hoyouTanto As String)

            ' This call is required by the Windows Form Designer.
            InitializeComponent()

            '
            Me.hoyouEventCode = hoyouEventCode
            Me.hoyouBukaCode = hoyouBukaCode
            Me.hoyouTantoKey = hoyouTantoKey
            Me.hoyouTanto = hoyouTanto

            ' Add any initialization after the InitializeComponent() call.
            ShisakuFormUtil.Initialize(Me)
            ShisakuFormUtil.setTitleVersion(Me)

            'チェックを付ける。
            setHoyouSekkeiTantoSoubi()

        End Sub

        Private Sub setHoyouSekkeiTantoSoubi()

            '補用設計担当装備情報取得
            Dim getByTHoyouSekkeiTantoSoubi As New KouseiBuhinSelectorDaoImpl
            Dim soubiVos As New List(Of THoyouSekkeiTantoSoubiVo)
            soubiVos = _
                    getByTHoyouSekkeiTantoSoubi.GetByTHoyouSekkeiTantoSoubi(hoyouEventCode, hoyouBukaCode, hoyouTantoKey, hoyouTanto)

            '画面のチェックボックスを制御
            If Not StringUtil.Equals(soubiVos.Count, 0) Then

                For i As Integer = 0 To soubiVos.Count - 1

                    Select Case soubiVos.Item(i).HoyouSoubiHyoujiJun
                        Case TShisakuSekkeiBlockSoubiVoHelper.ShisakuSoubiHyoujiJun.KANSEI_SYAGATA
                            spdCompleteCar.ActiveSheet.Cells(1, 0).Value = True
                        Case TShisakuSekkeiBlockSoubiVoHelper.ShisakuSoubiHyoujiJun.KANSEI_GRADE
                            spdCompleteCar.ActiveSheet.Cells(2, 0).Value = True
                        Case TShisakuSekkeiBlockSoubiVoHelper.ShisakuSoubiHyoujiJun.KANSEI_SHISAKU_SHIMUKECHI_SHIMUKE
                            spdCompleteCar.ActiveSheet.Cells(3, 0).Value = True
                        Case TShisakuSekkeiBlockSoubiVoHelper.ShisakuSoubiHyoujiJun.KANSEI_HANDORU
                            spdCompleteCar.ActiveSheet.Cells(4, 0).Value = True
                        Case TShisakuSekkeiBlockSoubiVoHelper.ShisakuSoubiHyoujiJun.KANSEI_EG_KATASHIKI
                            spdCompleteCar.ActiveSheet.Cells(5, 0).Value = True
                        Case TShisakuSekkeiBlockSoubiVoHelper.ShisakuSoubiHyoujiJun.KANSEI_EG_HAIKIRYOU
                            spdCompleteCar.ActiveSheet.Cells(6, 0).Value = True
                        Case TShisakuSekkeiBlockSoubiVoHelper.ShisakuSoubiHyoujiJun.KANSEI_EG_SYSTEM
                            spdCompleteCar.ActiveSheet.Cells(7, 0).Value = True
                        Case TShisakuSekkeiBlockSoubiVoHelper.ShisakuSoubiHyoujiJun.KANSEI_EG_KAKYUUKI
                            spdCompleteCar.ActiveSheet.Cells(8, 0).Value = True
                        Case TShisakuSekkeiBlockSoubiVoHelper.ShisakuSoubiHyoujiJun.KANSEI_EG_MEMO1
                            spdCompleteCar.ActiveSheet.Cells(9, 0).Value = True
                        Case TShisakuSekkeiBlockSoubiVoHelper.ShisakuSoubiHyoujiJun.KANSEI_EG_MEMO2
                            spdCompleteCar.ActiveSheet.Cells(10, 0).Value = True
                        Case TShisakuSekkeiBlockSoubiVoHelper.ShisakuSoubiHyoujiJun.KANSEI_TM_KUDOU
                            spdCompleteCar.ActiveSheet.Cells(11, 0).Value = True
                        Case TShisakuSekkeiBlockSoubiVoHelper.ShisakuSoubiHyoujiJun.KANSEI_TM_HENSOKUKI
                            spdCompleteCar.ActiveSheet.Cells(12, 0).Value = True
                        Case TShisakuSekkeiBlockSoubiVoHelper.ShisakuSoubiHyoujiJun.KANSEI_TM_FUKU_HENSOKUKI
                            spdCompleteCar.ActiveSheet.Cells(13, 0).Value = True
                        Case TShisakuSekkeiBlockSoubiVoHelper.ShisakuSoubiHyoujiJun.KANSEI_TM_MEMO1
                            spdCompleteCar.ActiveSheet.Cells(14, 0).Value = True
                        Case TShisakuSekkeiBlockSoubiVoHelper.ShisakuSoubiHyoujiJun.KANSEI_TM_MEMO2
                            spdCompleteCar.ActiveSheet.Cells(15, 0).Value = True
                        Case TShisakuSekkeiBlockSoubiVoHelper.ShisakuSoubiHyoujiJun.KANSEI_KATASHIKI
                            spdCompleteCar.ActiveSheet.Cells(16, 0).Value = True
                        Case TShisakuSekkeiBlockSoubiVoHelper.ShisakuSoubiHyoujiJun.KANSEI_SHIMUKE
                            spdCompleteCar.ActiveSheet.Cells(17, 0).Value = True
                        Case TShisakuSekkeiBlockSoubiVoHelper.ShisakuSoubiHyoujiJun.KANSEI_OP
                            spdCompleteCar.ActiveSheet.Cells(18, 0).Value = True
                        Case TShisakuSekkeiBlockSoubiVoHelper.ShisakuSoubiHyoujiJun.KANSEI_GAISOUSYOKU
                            spdCompleteCar.ActiveSheet.Cells(19, 0).Value = True
                        Case TShisakuSekkeiBlockSoubiVoHelper.ShisakuSoubiHyoujiJun.KANSEI_GAISOUSYOKU_NAME
                            spdCompleteCar.ActiveSheet.Cells(20, 0).Value = True
                        Case TShisakuSekkeiBlockSoubiVoHelper.ShisakuSoubiHyoujiJun.KANSEI_NAISOUSYOKU
                            spdCompleteCar.ActiveSheet.Cells(21, 0).Value = True
                        Case TShisakuSekkeiBlockSoubiVoHelper.ShisakuSoubiHyoujiJun.KANSEI_NAISOUSYOKU_NAME
                            spdCompleteCar.ActiveSheet.Cells(22, 0).Value = True
                        Case TShisakuSekkeiBlockSoubiVoHelper.ShisakuSoubiHyoujiJun.KANSEI_SYADAI_NO
                            spdCompleteCar.ActiveSheet.Cells(23, 0).Value = True
                        Case TShisakuSekkeiBlockSoubiVoHelper.ShisakuSoubiHyoujiJun.KANSEI_SHIKEN_MOKUTEKI
                            spdCompleteCar.ActiveSheet.Cells(24, 0).Value = True
                        Case TShisakuSekkeiBlockSoubiVoHelper.ShisakuSoubiHyoujiJun.KANSEI_SHIYOU_MOKUTEKI
                            spdCompleteCar.ActiveSheet.Cells(25, 0).Value = True
                        Case TShisakuSekkeiBlockSoubiVoHelper.ShisakuSoubiHyoujiJun.KANSEI_SIYOU_BUSYO
                            spdCompleteCar.ActiveSheet.Cells(26, 0).Value = True
                        Case TShisakuSekkeiBlockSoubiVoHelper.ShisakuSoubiHyoujiJun.KANSEI_GROUP
                            spdCompleteCar.ActiveSheet.Cells(27, 0).Value = True
                        Case TShisakuSekkeiBlockSoubiVoHelper.ShisakuSoubiHyoujiJun.KANSEI_SEISAKU_JUNJYO
                            spdCompleteCar.ActiveSheet.Cells(28, 0).Value = True
                        Case TShisakuSekkeiBlockSoubiVoHelper.ShisakuSoubiHyoujiJun.KANSEI_KANSEIBI
                            spdCompleteCar.ActiveSheet.Cells(29, 0).Value = True
                        Case TShisakuSekkeiBlockSoubiVoHelper.ShisakuSoubiHyoujiJun.KANSEI_KOUSHI_NO
                            spdCompleteCar.ActiveSheet.Cells(30, 0).Value = True
                        Case TShisakuSekkeiBlockSoubiVoHelper.ShisakuSoubiHyoujiJun.KANSEI_SEISAKU_HOUHOU_KBN
                            spdCompleteCar.ActiveSheet.Cells(31, 0).Value = True
                        Case TShisakuSekkeiBlockSoubiVoHelper.ShisakuSoubiHyoujiJun.KANSEI_SEISAKU_HOUHOU
                            spdCompleteCar.ActiveSheet.Cells(32, 0).Value = True
                        Case TShisakuSekkeiBlockSoubiVoHelper.ShisakuSoubiHyoujiJun.KANSEI_SHISAKU_MEMO
                            spdCompleteCar.ActiveSheet.Cells(33, 0).Value = True
                    End Select

                Next
                '全てチェックか？
                If AllKomokuCheck(spdCompleteCar) Then
                    spdCompleteCar.ActiveSheet.Cells(0, 0).Value = True
                End If
            End If

        End Sub

        Private Sub btnBACK_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnBACK.Click
            Me.Close()
        End Sub

        Private Sub btnADD_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnADD.Click

            Dim KomokuCount As Integer
            KomokuCount = GetKomokuCount()
            If KomokuCount = 0 Then
                ComFunc.ShowErrMsgBox("表示したい項目を一つ以上チェックしてください。")
                Exit Sub
            Else
                _resultOk = True
                Me.Close()
            End If

            Try

                Dim param As New THoyouSekkeiTantoSoubiVo
                Dim hoyouSekkeiTantoSoubiDao As THoyouSekkeiTantoSoubiDao = New THoyouSekkeiTantoSoubiDaoImpl
                param.HoyouEventCode = hoyouEventCode
                param.HoyouBukaCode = hoyouBukaCode
                param.HoyouTantoKey = hoyouTantoKey
                param.HoyouTanto = hoyouTanto
                hoyouSekkeiTantoSoubiDao.DeleteBy(param)


                Dim aShisakuDate As New ShisakuDate
                Dim rowCounts As Integer = spdCompleteCar.ActiveSheet.Rows.Count
                For i As Integer = 0 To rowCounts - 1

                    Dim value As New THoyouSekkeiTantoSoubiVo
                    Dim checkFlg As Boolean = False

                    If StringUtil.Equals(spdCompleteCar.ActiveSheet.Cells(i, 0).Value, True) Then

                        Select Case i
                            Case 1
                                value.HoyouSoubiHyoujiJun = _
                                    TShisakuSekkeiBlockSoubiVoHelper.ShisakuSoubiHyoujiJun.KANSEI_SYAGATA
                                checkFlg = True
                            Case 2
                                value.HoyouSoubiHyoujiJun = _
                                    TShisakuSekkeiBlockSoubiVoHelper.ShisakuSoubiHyoujiJun.KANSEI_GRADE
                                checkFlg = True
                            Case 3
                                value.HoyouSoubiHyoujiJun = _
                                    TShisakuSekkeiBlockSoubiVoHelper.ShisakuSoubiHyoujiJun.KANSEI_SHISAKU_SHIMUKECHI_SHIMUKE
                                checkFlg = True
                            Case 4
                                value.HoyouSoubiHyoujiJun = _
                                    TShisakuSekkeiBlockSoubiVoHelper.ShisakuSoubiHyoujiJun.KANSEI_HANDORU
                                checkFlg = True
                            Case 5
                                value.HoyouSoubiHyoujiJun = _
                                    TShisakuSekkeiBlockSoubiVoHelper.ShisakuSoubiHyoujiJun.KANSEI_EG_KATASHIKI
                                checkFlg = True
                            Case 6
                                value.HoyouSoubiHyoujiJun = _
                                    TShisakuSekkeiBlockSoubiVoHelper.ShisakuSoubiHyoujiJun.KANSEI_EG_HAIKIRYOU
                                checkFlg = True
                            Case 7
                                value.HoyouSoubiHyoujiJun = _
                                    TShisakuSekkeiBlockSoubiVoHelper.ShisakuSoubiHyoujiJun.KANSEI_EG_SYSTEM
                                checkFlg = True
                            Case 8
                                value.HoyouSoubiHyoujiJun = _
                                    TShisakuSekkeiBlockSoubiVoHelper.ShisakuSoubiHyoujiJun.KANSEI_EG_KAKYUUKI
                                checkFlg = True
                            Case 9
                                value.HoyouSoubiHyoujiJun = _
                                    TShisakuSekkeiBlockSoubiVoHelper.ShisakuSoubiHyoujiJun.KANSEI_EG_MEMO1
                                checkFlg = True
                            Case 10
                                value.HoyouSoubiHyoujiJun = _
                                    TShisakuSekkeiBlockSoubiVoHelper.ShisakuSoubiHyoujiJun.KANSEI_EG_MEMO2
                                checkFlg = True
                            Case 11
                                value.HoyouSoubiHyoujiJun = _
                                    TShisakuSekkeiBlockSoubiVoHelper.ShisakuSoubiHyoujiJun.KANSEI_TM_KUDOU
                                checkFlg = True
                            Case 12
                                value.HoyouSoubiHyoujiJun = _
                                    TShisakuSekkeiBlockSoubiVoHelper.ShisakuSoubiHyoujiJun.KANSEI_TM_HENSOKUKI
                                checkFlg = True
                            Case 13
                                value.HoyouSoubiHyoujiJun = _
                                    TShisakuSekkeiBlockSoubiVoHelper.ShisakuSoubiHyoujiJun.KANSEI_TM_FUKU_HENSOKUKI
                                checkFlg = True
                            Case 14
                                value.HoyouSoubiHyoujiJun = _
                                    TShisakuSekkeiBlockSoubiVoHelper.ShisakuSoubiHyoujiJun.KANSEI_TM_MEMO1
                                checkFlg = True
                            Case 15
                                value.HoyouSoubiHyoujiJun = _
                                    TShisakuSekkeiBlockSoubiVoHelper.ShisakuSoubiHyoujiJun.KANSEI_TM_MEMO2
                                checkFlg = True
                            Case 16
                                value.HoyouSoubiHyoujiJun = _
                                    TShisakuSekkeiBlockSoubiVoHelper.ShisakuSoubiHyoujiJun.KANSEI_KATASHIKI
                                checkFlg = True
                            Case 17
                                value.HoyouSoubiHyoujiJun = _
                                    TShisakuSekkeiBlockSoubiVoHelper.ShisakuSoubiHyoujiJun.KANSEI_SHIMUKE
                                checkFlg = True
                            Case 18
                                value.HoyouSoubiHyoujiJun = _
                                    TShisakuSekkeiBlockSoubiVoHelper.ShisakuSoubiHyoujiJun.KANSEI_OP
                                checkFlg = True
                            Case 19
                                value.HoyouSoubiHyoujiJun = _
                                    TShisakuSekkeiBlockSoubiVoHelper.ShisakuSoubiHyoujiJun.KANSEI_GAISOUSYOKU
                                checkFlg = True
                            Case 20
                                value.HoyouSoubiHyoujiJun = _
                                    TShisakuSekkeiBlockSoubiVoHelper.ShisakuSoubiHyoujiJun.KANSEI_GAISOUSYOKU_NAME
                                checkFlg = True
                            Case 21
                                value.HoyouSoubiHyoujiJun = _
                                    TShisakuSekkeiBlockSoubiVoHelper.ShisakuSoubiHyoujiJun.KANSEI_NAISOUSYOKU
                                checkFlg = True
                            Case 22
                                value.HoyouSoubiHyoujiJun = _
                                    TShisakuSekkeiBlockSoubiVoHelper.ShisakuSoubiHyoujiJun.KANSEI_NAISOUSYOKU_NAME
                                checkFlg = True
                            Case 23
                                value.HoyouSoubiHyoujiJun = _
                                    TShisakuSekkeiBlockSoubiVoHelper.ShisakuSoubiHyoujiJun.KANSEI_SYADAI_NO
                                checkFlg = True
                            Case 24
                                value.HoyouSoubiHyoujiJun = _
                                    TShisakuSekkeiBlockSoubiVoHelper.ShisakuSoubiHyoujiJun.KANSEI_SHIKEN_MOKUTEKI
                                checkFlg = True
                            Case 25
                                value.HoyouSoubiHyoujiJun = _
                                    TShisakuSekkeiBlockSoubiVoHelper.ShisakuSoubiHyoujiJun.KANSEI_SHIYOU_MOKUTEKI
                                checkFlg = True
                            Case 26
                                value.HoyouSoubiHyoujiJun = _
                                    TShisakuSekkeiBlockSoubiVoHelper.ShisakuSoubiHyoujiJun.KANSEI_SIYOU_BUSYO
                                checkFlg = True
                            Case 27
                                value.HoyouSoubiHyoujiJun = _
                                    TShisakuSekkeiBlockSoubiVoHelper.ShisakuSoubiHyoujiJun.KANSEI_GROUP
                                checkFlg = True
                            Case 28
                                value.HoyouSoubiHyoujiJun = _
                                    TShisakuSekkeiBlockSoubiVoHelper.ShisakuSoubiHyoujiJun.KANSEI_SEISAKU_JUNJYO
                                checkFlg = True
                            Case 29
                                value.HoyouSoubiHyoujiJun = _
                                    TShisakuSekkeiBlockSoubiVoHelper.ShisakuSoubiHyoujiJun.KANSEI_KANSEIBI
                                checkFlg = True
                            Case 30
                                value.HoyouSoubiHyoujiJun = _
                                    TShisakuSekkeiBlockSoubiVoHelper.ShisakuSoubiHyoujiJun.KANSEI_KOUSHI_NO
                                checkFlg = True
                            Case 31
                                value.HoyouSoubiHyoujiJun = _
                                    TShisakuSekkeiBlockSoubiVoHelper.ShisakuSoubiHyoujiJun.KANSEI_SEISAKU_HOUHOU_KBN
                                checkFlg = True
                            Case 32
                                value.HoyouSoubiHyoujiJun = _
                                    TShisakuSekkeiBlockSoubiVoHelper.ShisakuSoubiHyoujiJun.KANSEI_SEISAKU_HOUHOU
                                checkFlg = True
                            Case 33
                                value.HoyouSoubiHyoujiJun = _
                                    TShisakuSekkeiBlockSoubiVoHelper.ShisakuSoubiHyoujiJun.KANSEI_SHISAKU_MEMO
                                checkFlg = True
                        End Select

                        If StringUtil.Equals(checkFlg, True) Then
                            value.HoyouEventCode = hoyouEventCode
                            value.HoyouBukaCode = hoyouBukaCode
                            value.HoyouTantoKey = hoyouTantoKey
                            value.HoyouTanto = hoyouTanto

                            value.CreatedUserId = LoginInfo.Now.UserId
                            value.CreatedDate = aShisakuDate.CurrentDateDbFormat
                            value.CreatedTime = aShisakuDate.CurrentTimeDbFormat
                            value.UpdatedUserId = LoginInfo.Now.UserId
                            value.UpdatedDate = aShisakuDate.CurrentDateDbFormat
                            value.UpdatedTime = aShisakuDate.CurrentTimeDbFormat

                            hoyouSekkeiTantoSoubiDao.InsertBy(value)
                        End If

                    End If

                Next

                _resultOk = True
                Me.Close()

            Catch ex As Exception
                ComFunc.ShowErrMsgBox(String.Format("装備仕様情報の保存中にエラーが発生しました", ex.Message))
                _resultOk = False
            Finally
                Cursor.Current = Cursors.Default
            End Try

        End Sub

        Public ReadOnly Property ResultOk() As Boolean
            Get
                Return _resultOk
            End Get
        End Property

        ''' <summary>
        ''' 画面全てSpreadの項目数
        ''' </summary>
        ''' <returns>項目合計数</returns>
        ''' <remarks></remarks>
        Private Function GetKomokuCount() As Integer

            Dim fpSKomokuCount As Integer = 0

            Dim CompleteCarCount As Integer = GetCount(spdCompleteCar)

            Return CompleteCarCount
        End Function

        ''' <summary>
        ''' Spreadのチェックボックスのチェック動作
        ''' </summary>
        ''' <param name="sender">Spread-ButtonClickイベントに従う</param>
        ''' <param name="e">Spread-ButtonClickイベントに従う</param>
        ''' <remarks></remarks>
        Private Sub spdCompleteCar_ButtonClicked(ByVal sender As System.Object, _
                                             ByVal e As FarPoint.Win.Spread.EditorNotifyEventArgs) _
                                             Handles spdCompleteCar.ButtonClicked

            Dim fpSpread As FarPoint.Win.Spread.FpSpread = DirectCast(sender, FarPoint.Win.Spread.FpSpread)
            If Not fpSpread Is Nothing Then

                Dim rowCounts As Integer = fpSpread.ActiveSheet.Rows.Count
                Dim isFirstRow As Integer = e.Row
                Dim i As Integer = 0
                If isFirstRow = 0 Then
                    If fpSpread.ActiveSheet.Cells(e.Row, 0).Value = True Then
                        For i = 0 To rowCounts - 1
                            fpSpread.ActiveSheet.Cells(i, 0).Value = True
                        Next
                    Else
                        For i = 0 To rowCounts - 1
                            fpSpread.ActiveSheet.Cells(i, 0).Value = False
                        Next
                    End If
                Else
                    If fpSpread.ActiveSheet.Cells(e.Row, 0).Value = True Then
                        fpSpread.ActiveSheet.Cells(e.Row, 0).Value = True
                        If AllKomokuCheck(fpSpread) Then
                            fpSpread.ActiveSheet.Cells(0, 0).Value = True
                        End If
                    Else
                        fpSpread.ActiveSheet.Cells(e.Row, 0).Value = False
                        If Not AllKomokuUncheck(fpSpread) Then
                            fpSpread.ActiveSheet.Cells(0, 0).Value = False
                        End If
                    End If
                End If
            End If

        End Sub

        ''' <summary>
        ''' Spreadの項目チェックボックスがチェックします。
        ''' </summary>
        ''' <param name="fpSpread">Spread</param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Private Function AllKomokuCheck(ByVal fpSpread As FarPoint.Win.Spread.FpSpread) As Boolean
            Dim isAllCheck As Boolean
            Dim checkCount As Integer = GetCount(fpSpread)

            If checkCount = fpSpread.ActiveSheet.Rows.Count - 1 Then
                isAllCheck = True
            Else
                isAllCheck = False
            End If
            Return isAllCheck
        End Function

        ''' <summary>
        ''' Spreadの項目数
        ''' </summary>
        ''' <param name="fpS">Spread</param>
        ''' <returns>Spreadの項目数</returns>
        ''' <remarks></remarks>
        Private Function GetCount(ByVal fpS As FarPoint.Win.Spread.FpSpread) As Integer

            Dim fpSKomukuCount As Integer = 0
            If Not fpS.ActiveSheet Is Nothing Then
                If fpS.ActiveSheet.Rows.Count > 1 Then
                    Dim i As Integer
                    For i = 1 To fpS.ActiveSheet.Rows.Count - 1
                        If fpS.ActiveSheet.Cells(i, 0).Value = True Then
                            fpSKomukuCount += 1
                        End If
                    Next
                End If
            End If
            Return fpSKomukuCount

        End Function

        ''' <summary>
        ''' Spreadの項目チェックボックスがチェックしません。
        ''' </summary>
        ''' <param name="fpSpread">Spread</param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Private Function AllKomokuUncheck(ByVal fpSpread As FarPoint.Win.Spread.FpSpread) As Boolean
            Dim isAllCheck As Boolean
            Dim checkCount As Integer = GetCount(fpSpread)

            If checkCount = 0 Then
                isAllCheck = True
            Else
                isAllCheck = False
            End If
            Return isAllCheck
        End Function
    End Class

End Namespace