Imports EBom.Common
Imports EventSakusei.ShisakuBuhinEdit.Selector.Logic
Imports EventSakusei.ShisakuBuhinEdit.Logic
Imports EventSakusei.ShisakuBuhinEdit.Al.Logic
Imports ShisakuCommon.Db.EBom.Vo.Helper
Imports ShisakuCommon.Db.EBom.Dao
Imports ShisakuCommon.Ui
Imports ShisakuCommon.Util
Imports EventSakusei.Soubi
Imports EventSakusei.ShisakuBuhinEdit.Selector.Ui
Imports ShisakuCommon.Db.EBom.Vo
Imports EventSakusei.ShisakuBuhinEdit.Al.Ui
Imports EventSakusei.ExportShisakuEventInfoExcel.Dao
Imports ShisakuCommon
Imports EventSakusei.EventEdit.Dao

Namespace ShisakuBuhinEdit.Selector
    ''' <summary>
    ''' 部品構成表示画面
    ''' </summary>
    ''' <remarks></remarks>
    Public Class Frm43DispShisakuBuhinEditSelector : Implements Observer
        'TODO ""ではなく全選択時の値がもてるようなモノを入れるべき
        Private Shared ReadOnly BASE_CAR_LIST_VALUES As String() = {"", _
                                                                    TShisakuSekkeiBlockSoubiVoHelper.ShisakuSoubiHyoujiJun.BASE_KAIHATUFUGOU, _
                                                                    TShisakuSekkeiBlockSoubiVoHelper.ShisakuSoubiHyoujiJun.BASE_SHIYOUJYOUHOU_NO, _
                                                                    TShisakuSekkeiBlockSoubiVoHelper.ShisakuSoubiHyoujiJun.SEISAKU_SYASYU, _
                                                                    TShisakuSekkeiBlockSoubiVoHelper.ShisakuSoubiHyoujiJun.SEISAKU_GRADE, _
                                                                    TShisakuSekkeiBlockSoubiVoHelper.ShisakuSoubiHyoujiJun.SEISAKU_SHIMUKE, _
                                                                    TShisakuSekkeiBlockSoubiVoHelper.ShisakuSoubiHyoujiJun.SEISAKU_HANDORU, _
                                                                    TShisakuSekkeiBlockSoubiVoHelper.ShisakuSoubiHyoujiJun.SEISAKU_EG_HAIKIRYOU, _
                                                                    TShisakuSekkeiBlockSoubiVoHelper.ShisakuSoubiHyoujiJun.SEISAKU_EG_KATASHIKI, _
                                                                    TShisakuSekkeiBlockSoubiVoHelper.ShisakuSoubiHyoujiJun.SEISAKU_EG_KAKYUUKI, _
                                                                    TShisakuSekkeiBlockSoubiVoHelper.ShisakuSoubiHyoujiJun.SEISAKU_TM_KUDOU, _
                                                                    TShisakuSekkeiBlockSoubiVoHelper.ShisakuSoubiHyoujiJun.SEISAKU_TM_HENSOKUKI, _
                                                                    TShisakuSekkeiBlockSoubiVoHelper.ShisakuSoubiHyoujiJun.BASE_APPLIED_NO, _
                                                                    TShisakuSekkeiBlockSoubiVoHelper.ShisakuSoubiHyoujiJun.BASE_KATASHIKI, _
                                                                    TShisakuSekkeiBlockSoubiVoHelper.ShisakuSoubiHyoujiJun.BASE_SHIMUKE, _
                                                                    TShisakuSekkeiBlockSoubiVoHelper.ShisakuSoubiHyoujiJun.BASE_OP, _
                                                                    TShisakuSekkeiBlockSoubiVoHelper.ShisakuSoubiHyoujiJun.BASE_GAISOUSYOKU, _
                                                                    TShisakuSekkeiBlockSoubiVoHelper.ShisakuSoubiHyoujiJun.BASE_NAISOUSYOKU, _
                                                                    TShisakuSekkeiBlockSoubiVoHelper.ShisakuSoubiHyoujiJun.BASE_EVENT_CODE, _
                                                                    TShisakuSekkeiBlockSoubiVoHelper.ShisakuSoubiHyoujiJun.BASE_GOUSYA, _
                                                                    TShisakuSekkeiBlockSoubiVoHelper.ShisakuSoubiHyoujiJun.SEISAKU_SYATAI_NO}

        Private Shared ReadOnly BASE_TENKAI_CAR_LIST_VALUES As String() = {"", _
                                                                    TShisakuSekkeiBlockSoubiVoHelper.ShisakuSoubiHyoujiJun.TENKAI_BASE_KAIHATUFUGOU, _
                                                                    TShisakuSekkeiBlockSoubiVoHelper.ShisakuSoubiHyoujiJun.TENKAI_BASE_SHIYOUJYOUHOU_NO, _
                                                                    TShisakuSekkeiBlockSoubiVoHelper.ShisakuSoubiHyoujiJun.TENKAI_BASE_APPLIED_NO, _
                                                                    TShisakuSekkeiBlockSoubiVoHelper.ShisakuSoubiHyoujiJun.TENKAI_BASE_KATASHIKI, _
                                                                    TShisakuSekkeiBlockSoubiVoHelper.ShisakuSoubiHyoujiJun.TENKAI_BASE_SHIMUKE, _
                                                                    TShisakuSekkeiBlockSoubiVoHelper.ShisakuSoubiHyoujiJun.TENKAI_BASE_OP, _
                                                                    TShisakuSekkeiBlockSoubiVoHelper.ShisakuSoubiHyoujiJun.TENKAI_BASE_GAISOUSYOKU, _
                                                                    TShisakuSekkeiBlockSoubiVoHelper.ShisakuSoubiHyoujiJun.TENKAI_BASE_NAISOUSYOKU, _
                                                                    TShisakuSekkeiBlockSoubiVoHelper.ShisakuSoubiHyoujiJun.TENKAI_BASE_EVENT_CODE, _
                                                                    TShisakuSekkeiBlockSoubiVoHelper.ShisakuSoubiHyoujiJun.TENKAI_BASE_GOUSYA}

        Private Shared ReadOnly COMPLETE_CAR_LIST_VALUES As String() = {"", _
                                                                        TShisakuSekkeiBlockSoubiVoHelper.ShisakuSoubiHyoujiJun.KANSEI_SYAGATA, _
                                                                        TShisakuSekkeiBlockSoubiVoHelper.ShisakuSoubiHyoujiJun.KANSEI_GRADE, _
                                                                        TShisakuSekkeiBlockSoubiVoHelper.ShisakuSoubiHyoujiJun.KANSEI_SHISAKU_SHIMUKECHI_SHIMUKE, _
                                                                        TShisakuSekkeiBlockSoubiVoHelper.ShisakuSoubiHyoujiJun.KANSEI_HANDORU, _
                                                                        TShisakuSekkeiBlockSoubiVoHelper.ShisakuSoubiHyoujiJun.KANSEI_EG_KATASHIKI, _
                                                                        TShisakuSekkeiBlockSoubiVoHelper.ShisakuSoubiHyoujiJun.KANSEI_EG_HAIKIRYOU, _
                                                                        TShisakuSekkeiBlockSoubiVoHelper.ShisakuSoubiHyoujiJun.KANSEI_EG_SYSTEM, _
                                                                        TShisakuSekkeiBlockSoubiVoHelper.ShisakuSoubiHyoujiJun.KANSEI_EG_KAKYUUKI, _
                                                                        TShisakuSekkeiBlockSoubiVoHelper.ShisakuSoubiHyoujiJun.KANSEI_EG_MEMO1, _
                                                                        TShisakuSekkeiBlockSoubiVoHelper.ShisakuSoubiHyoujiJun.KANSEI_EG_MEMO2, _
                                                                        TShisakuSekkeiBlockSoubiVoHelper.ShisakuSoubiHyoujiJun.KANSEI_TM_KUDOU, _
                                                                        TShisakuSekkeiBlockSoubiVoHelper.ShisakuSoubiHyoujiJun.KANSEI_TM_HENSOKUKI, _
                                                                        TShisakuSekkeiBlockSoubiVoHelper.ShisakuSoubiHyoujiJun.KANSEI_TM_FUKU_HENSOKUKI, _
                                                                        TShisakuSekkeiBlockSoubiVoHelper.ShisakuSoubiHyoujiJun.KANSEI_TM_MEMO1, _
                                                                        TShisakuSekkeiBlockSoubiVoHelper.ShisakuSoubiHyoujiJun.KANSEI_TM_MEMO2, _
                                                                        TShisakuSekkeiBlockSoubiVoHelper.ShisakuSoubiHyoujiJun.KANSEI_KATASHIKI, _
                                                                        TShisakuSekkeiBlockSoubiVoHelper.ShisakuSoubiHyoujiJun.KANSEI_SHIMUKE, _
                                                                        TShisakuSekkeiBlockSoubiVoHelper.ShisakuSoubiHyoujiJun.KANSEI_OP, _
                                                                        TShisakuSekkeiBlockSoubiVoHelper.ShisakuSoubiHyoujiJun.KANSEI_GAISOUSYOKU, _
                                                                        TShisakuSekkeiBlockSoubiVoHelper.ShisakuSoubiHyoujiJun.KANSEI_GAISOUSYOKU_NAME, _
                                                                        TShisakuSekkeiBlockSoubiVoHelper.ShisakuSoubiHyoujiJun.KANSEI_NAISOUSYOKU, _
                                                                        TShisakuSekkeiBlockSoubiVoHelper.ShisakuSoubiHyoujiJun.KANSEI_NAISOUSYOKU_NAME, _
                                                                        TShisakuSekkeiBlockSoubiVoHelper.ShisakuSoubiHyoujiJun.KANSEI_SYADAI_NO, _
                                                                        TShisakuSekkeiBlockSoubiVoHelper.ShisakuSoubiHyoujiJun.KANSEI_SHIYOU_MOKUTEKI, _
                                                                        TShisakuSekkeiBlockSoubiVoHelper.ShisakuSoubiHyoujiJun.KANSEI_SHIKEN_MOKUTEKI, _
                                                                        TShisakuSekkeiBlockSoubiVoHelper.ShisakuSoubiHyoujiJun.KANSEI_SIYOU_BUSYO, _
                                                                        TShisakuSekkeiBlockSoubiVoHelper.ShisakuSoubiHyoujiJun.KANSEI_GROUP, _
                                                                        TShisakuSekkeiBlockSoubiVoHelper.ShisakuSoubiHyoujiJun.KANSEI_SEISAKU_JUNJYO, _
                                                                        TShisakuSekkeiBlockSoubiVoHelper.ShisakuSoubiHyoujiJun.KANSEI_KANSEIBI, _
                                                                        TShisakuSekkeiBlockSoubiVoHelper.ShisakuSoubiHyoujiJun.KANSEI_KOUSHI_NO, _
                                                                        TShisakuSekkeiBlockSoubiVoHelper.ShisakuSoubiHyoujiJun.KANSEI_SEISAKU_HOUHOU_KBN, _
                                                                        TShisakuSekkeiBlockSoubiVoHelper.ShisakuSoubiHyoujiJun.KANSEI_SEISAKU_HOUHOU, _
                                                                        TShisakuSekkeiBlockSoubiVoHelper.ShisakuSoubiHyoujiJun.KANSEI_SHISAKU_MEMO}

        Private ReadOnly subject As BuhinEditSelectorSubject
        Private ReadOnly baseObserver As SpdSelectorBaseCompleteObserver
        Private ReadOnly baseTenkaiObserver As SpdSelectorBaseCompleteObserver
        Private ReadOnly completeObserver As SpdSelectorBaseCompleteObserver
        Private ReadOnly basicObserver As SpdSelectorOptionObserver
        Private ReadOnly specialObserver As SpdSelectorOptionObserver
        Private ReadOnly specialWbObserver As SpdSelectorOptionObserver
        Private bag As BuhinEditAlShowColumnBag
        Private _resultOk As Boolean
        Private _result As BuhinEditAlShowColumnBag

        Public Sub New(ByVal shisakuEventCode As String, ByVal bag As BuhinEditAlShowColumnBag)

            ' This call is required by the Windows Form Designer.
            InitializeComponent()

            ' Add any initialization after the InitializeComponent() call.
            ShisakuFormUtil.Initialize(Me)
            ShisakuFormUtil.setTitleVersion(Me)
            Me.bag = bag

            Dim anEventSoubiDao As New EventSoubiDaoImpl
            Dim shisakuSekkeiBlockSoubiDao As TShisakuSekkeiBlockSoubiDao = Nothing
            Dim shisakuSekkeiBlockSoubiShiyouDao As TShisakuSekkeiBlockSoubiShiyouDao = Nothing
            subject = New BuhinEditSelectorSubject(shisakuEventCode, anEventSoubiDao, bag)
            subject.addObserver(Me)

            baseObserver = New SpdSelectorBaseCompleteObserver(spdBaseCar, BASE_CAR_LIST_VALUES, subject.BaseCarSubject)
            baseTenkaiObserver = New SpdSelectorBaseCompleteObserver(spdBaseTenkaiCar, BASE_TENKAI_CAR_LIST_VALUES, subject.BaseTenkaiCarSubject)
            completeObserver = New SpdSelectorBaseCompleteObserver(spdCompleteCar, COMPLETE_CAR_LIST_VALUES, subject.CompleteCarSubject)
            basicObserver = New SpdSelectorOptionObserver(spdBasicOption, subject.BasicOptionSubject)
            specialObserver = New SpdSelectorOptionObserver(spdSpecialOption, subject.SpecialOptionSubject)
            specialWbObserver = New SpdSelectorOptionObserver(spdWBSpecialOption, subject.SpecialOptionWbSubject)

            baseObserver.Initialize()
            baseTenkaiObserver.Initialize()
            completeObserver.Initialize()
            basicObserver.Initialize()
            specialObserver.Initialize()
            specialWbObserver.Initialize()

            subject.NotifyObservers()

            '完成車SPREADのE/Gメモ１，２、T/Mメモ１，２をセットする。
            ' イベント情報取得
            Dim eventVO As New List(Of TShisakuEventVo)
            Dim eventInfo = New ExportShisakuEventInfoExcelDaoImpl
            eventVO = eventInfo.GetEvent(shisakuEventCode)
            '改訂№を文字タイプへ変更
            Dim KaiteiNo As String = CStr(eventVO.Item(0).SeisakuichiranHakouNoKai)
            If StringUtil.Equals(KaiteiNo.Length, 1) Then
                KaiteiNo = KaiteiNo.PadLeft(2, "0"c)
            End If
            '製作一覧HD情報取得
            Dim getSeisakuIchiranHd As New SeisakuIchiranDaoImpl
            Dim tSeisakuHakouHdVo As New TSeisakuHakouHdVo
            tSeisakuHakouHdVo = _
                    getSeisakuIchiranHd.GetSeisakuIchiranHd(eventVO.Item(0).SeisakuichiranHakouNo, KaiteiNo)

            If StringUtil.IsNotEmpty(tSeisakuHakouHdVo) Then
                Me.spdCompleteCar.ActiveSheet.Cells(9, 2).Value = tSeisakuHakouHdVo.KanseiEgMemo1
                Me.spdCompleteCar.ActiveSheet.Cells(10, 2).Value = tSeisakuHakouHdVo.KanseiEgMemo2
                Me.spdCompleteCar.ActiveSheet.Cells(14, 2).Value = tSeisakuHakouHdVo.KanseiTmMemo1
                Me.spdCompleteCar.ActiveSheet.Cells(15, 2).Value = tSeisakuHakouHdVo.KanseiTmMemo2
            End If

        End Sub

        Private Sub btnBACK_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnBACK.Click
            Me.Close()
        End Sub

        Private Sub btnADD_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnADD.Click

            Dim KomokuCount As Integer
            KomokuCount = GetKomokuCount()
            If KomokuCount > 40 Then
                Dim overCount As Integer = KomokuCount - 40
                ComFunc.ShowErrMsgBox("選択できる項目数の最大は４０項目です。（" + overCount.ToString() + "個オーバー）")
            ElseIf KomokuCount = 0 Then
                ComFunc.ShowErrMsgBox("表示したい項目を一つ以上チェックしてください。（最大４０項目まで）")
            Else
                _resultOk = True
                Me.Close()
            End If
        End Sub

        Private Sub Frm43DispShisakuBuhinEditSelector_FormClosing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
            If Not _resultOk Then
                _result = Nothing
                Return
            End If

            Dim soubiVos As New List(Of TShisakuSekkeiBlockSoubiVo)
            soubiVos.AddRange(subject.BaseCarSubject.GetCheckedValues)
            soubiVos.AddRange(subject.BaseTenkaiCarSubject.GetCheckedValues)
            soubiVos.AddRange(subject.CompleteCarSubject.GetCheckedValues)

            Dim soubiShiyouVos As New List(Of TShisakuSekkeiBlockSoubiShiyouVo)
            soubiShiyouVos.AddRange(subject.BasicOptionSubject.GetCheckedValues)
            soubiShiyouVos.AddRange(subject.SpecialOptionSubject.GetCheckedValues(soubiShiyouVos.Count))
            soubiShiyouVos.AddRange(subject.SpecialOptionWbSubject.GetCheckedValues(soubiShiyouVos.Count))

            _result = New BuhinEditAlShowColumnBag(soubiVos, soubiShiyouVos)
        End Sub

        Public ReadOnly Property ResultOk() As Boolean
            Get
                Return _resultOk
            End Get
        End Property
        Public ReadOnly Property Result() As BuhinEditAlShowColumnBag
            Get
                Return _result
            End Get
        End Property

        Public Sub UpdateObserver(ByVal observable As Observable, ByVal arg As Object) Implements Observer.Update
            'If observable Is headerSubject Then
            '    UpdateBaseComplete(observable, arg)
            'Else
            '    subject.notifyObservers(arg)
            '    subject.BasicOptionSubject.notifyObservers(arg)
            '    subject.SpecialOptionSubject.notifyObservers(arg)
            'End If
            subject.BaseCarSubject.notifyObservers(arg)
            subject.BaseTenkaiCarSubject.NotifyObservers(arg)
            subject.CompleteCarSubject.NotifyObservers(arg)
            subject.BasicOptionSubject.notifyObservers(arg)
            subject.SpecialOptionSubject.notifyObservers(arg)
            subject.SpecialOptionWbSubject.NotifyObservers(arg)
        End Sub

        ''' <summary>
        ''' Spreadのチェックボックスのチェック動作
        ''' </summary>
        ''' <param name="sender">Spread-ButtonClickイベントに従う</param>
        ''' <param name="e">Spread-ButtonClickイベントに従う</param>
        ''' <remarks></remarks>
        Private Sub spdBaseCar_ButtonClicked(ByVal sender As System.Object, _
                                             ByVal e As FarPoint.Win.Spread.EditorNotifyEventArgs) _
                                             Handles spdBaseCar.ButtonClicked, spdBaseTenkaiCar.ButtonClicked, _
                                             spdSpecialOption.ButtonClicked, spdWBSpecialOption.ButtonClicked, _
                                             spdCompleteCar.ButtonClicked, spdBasicOption.ButtonClicked

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
        ''' 画面全てSpreadの項目数
        ''' </summary>
        ''' <returns>項目合計数</returns>
        ''' <remarks></remarks>
        Private Function GetKomokuCount() As Integer

            Dim fpSKomokuCount As Integer = 0

            Dim BaseCarCount As Integer = GetCount(spdBaseCar)
            Dim BaseTenkaiCarCount As Integer = GetCount(spdBaseTenkaiCar)
            Dim BasicOptionCount As Integer = GetCount(spdBasicOption)
            Dim CompleteCarCount As Integer = GetCount(spdCompleteCar)
            Dim SpecialOptionCount As Integer = GetCount(spdSpecialOption)
            Dim WbSpecialOptionCount As Integer = GetCount(spdWBSpecialOption)

            fpSKomokuCount = BaseCarCount + BasicOptionCount + CompleteCarCount + _
                             SpecialOptionCount + BaseTenkaiCarCount + WbSpecialOptionCount

            Return fpSKomokuCount
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