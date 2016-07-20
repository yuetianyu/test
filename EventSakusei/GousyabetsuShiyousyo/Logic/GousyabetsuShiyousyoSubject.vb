Imports System.IO
Imports ShisakuCommon
Imports ShisakuCommon.Db
Imports ShisakuCommon.Db.EBom
Imports EBom.Excel
Imports ShisakuCommon.Ui
Imports ShisakuCommon.Db.Impl
Imports ShisakuCommon.Db.EBom.Exclusion
Imports ShisakuCommon.Db.EBom.Dao.Impl
Imports ShisakuCommon.Db.EBom.Dao

Imports ShisakuCommon.Util
Imports ShisakuCommon.Db.EBom.Vo

Imports EventSakusei.TehaichoSakusei.Logic
Imports EventSakusei.TehaichoSakusei.Dao
Imports ShisakuCommon.Util.LabelValue
Imports EventSakusei.TehaichoSakusei.Vo


Imports EBom.Common

Public Class GousyabetsuShiyousyoSubject

#Region "メンバ変数"

    'GUID
    Private mGUID As Guid

    'イベントコード.
    Private mEventCode As String
    '↓↓2014/10/01 酒井 ADD BEGIN
    Private mKaiteiNo As String
    '↑↑2014/10/01 酒井 ADD END
    'イベント名
    Private mEventName As String
    '開発符号.
    Private mKaihatsuFugo As String
    ' 試作イベントコード
    Private mShisakuEventCode As String
    ' グループNo
    Private mGroupNo As String
    '各_OP_EXISTの位置
    Private mOPExistlist As ArrayList
    '各_SHO_KBN_EXISTの位置
    Private mShoExistlist As ArrayList
    ''特別織込み項目の存在ＦＬＧ
    Dim ShokbnFlg As Boolean = True

    Dim _KeyWord As New ArrayList


#End Region

#Region "プロパティー"
    ''' <summary>
    ''' イベントコード.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property EventCode() As String
        Get
            Return mEventCode
        End Get
        Set(ByVal value As String)
            mEventCode = value
        End Set
    End Property
    '↓↓2014/10/01 酒井 ADD BEGIN
    Public Property KaiteiNo() As String
        Get
            Return mKaiteiNo
        End Get
        Set(ByVal value As String)
            mKaiteiNo = value
        End Set
    End Property
    '↑↑2014/10/01 酒井 ADD END

    ''' <summary>
    ''' 開発符号.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public ReadOnly Property KaihatsuFugo() As String
        Get
            Return mKaihatsuFugo
        End Get
    End Property

    ''' <summary>グループNo</summary>
    ''' <returns>グループNo</returns>
    Public Property GroupNo() As String
        Get
            Return mGroupNo
        End Get
        Set(ByVal value As String)
            mGroupNo = value

        End Set
    End Property

    ''' <summary>
    ''' GUID
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property GUID() As Guid
        Get
            Return mGUID
        End Get
        Set(ByVal value As Guid)
            mGUID = value
        End Set
    End Property

#End Region


#Region "コンストラクタ"

    ''' <summary>
    ''' コンストラクタ
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub New()
        Initialize()

        _KeyWord.Add("_OP_NAME1")
        _KeyWord.Add("_OP_NAME")
        _KeyWord.Add("_OP_EXIST1")
        _KeyWord.Add("_OP_EXIST")
        _KeyWord.Add("ﾗｲﾝOP")
    End Sub

#End Region

#Region "初期化"

    ''' <summary>
    ''' 初期化
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub Initialize()

        'GUIDの作成.
        Dim guidValue As Guid = System.Guid.NewGuid()
        mGUID = guidValue

    End Sub

#End Region

#Region "コンボボックスにアイテム設定."

    ''' <summary>
    ''' イベントコードをコンボボックスに設定する.
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub setEventCodeToComboBox(ByRef aComboBox As System.Windows.Forms.ComboBox)
        Dim iDao As New XVLView.Dao.Impl.GousyabetsuShiyousyoDaoImpl
        '全てのイベントコードを取得.
        Dim iVos As List(Of XVLView.Dao.Vo.GousyabetsuShiyousyoVo) = iDao.GetSeisakuEvent()

        'アイテムを削除.
        aComboBox.Items.Clear()

        ''取得したボディ名をコンボボックスに設定.
        For Each lItem In iVos
            '↓↓2014/09/30 酒井 ADD BEGIN
            'Dim itemStr As String = String.Format("{0}:{1}", lItem.SeisakuEventCode, lItem.SeisakuEventName)
            Dim itemStr As String = String.Format("{0}:{1}:{2}", lItem.SeisakuEventCode, lItem.KaiteiNo, lItem.SeisakuEventName)
            '↑↑2014/09/30 酒井 ADD END
            'グループを追加.
            aComboBox.Items.Add(itemStr)
        Next
        'イベントコードを設定.
        If 0 < iVos.Count Then
            aComboBox.Text = aComboBox.Items(0).ToString
            mEventCode = iVos(0).SeisakuEventCode
        End If


    End Sub

    ''' <summary>
    ''' 試作イベント完成車情報からグループ情報を抽出.
    ''' </summary>
    ''' <param name="aComboBox"></param>
    ''' <remarks></remarks>
    Public Sub setGroupItemtoComboBox(ByRef aComboBox As System.Windows.Forms.ComboBox)

        'イベントコードが未設定の場合はエラーを返す.
        If EventCode Is Nothing Then Throw New ShisakuException("イベントコードが選択されていません.")

        Dim iDao As New XVLView.Dao.Impl.GousyabetsuShiyousyoDaoImpl
        Dim iVos As List(Of XVLView.Dao.Vo.GousyabetsuShiyousyoVo) = iDao.GetGroup(EventCode)

        'アイテムを削除.
        aComboBox.Items.Clear()
        aComboBox.Text = ""

        ''取得したボディ名をコンボボックスに設定.
        For Each lItem In iVos

            'イベントコードが異なる場合は次のレコードへ.
            If lItem.SeisakuEventCode <> EventCode Then Continue For
            If lItem.SeisakuGroup Is Nothing Then Continue For
            '同じグループ№の場合
            If aComboBox.Items.Contains(lItem.SeisakuGroup) Then Continue For

            'グループを追加.
            aComboBox.Items.Add(lItem.SeisakuGroup)

        Next

        If 0 < aComboBox.Items.Count Then aComboBox.Text = aComboBox.Items(0).ToString

    End Sub

    ''' <summary>
    ''' 号車情報の更新.
    ''' </summary>
    ''' <param name="aCheckList"></param>
    ''' <remarks></remarks>
    Public Sub setGousyatoComboBox(ByRef aCheckList As System.Windows.Forms.CheckedListBox)
        aCheckList.Items.Clear()

        Dim iDao As New XVLView.Dao.Impl.GousyabetsuShiyousyoDaoImpl
        Dim iVOs As List(Of XVLView.Dao.Vo.GousyabetsuShiyousyoVo) = iDao.GetGousya(mEventCode, mGroupNo)

        If iVOs Is Nothing OrElse 0 >= iVOs.Count Then Exit Sub

        For Each lItem In iVOs
            aCheckList.Items.Add(lItem.SeisakuGousya)
        Next

    End Sub

    ''' <summary>
    '''テンプレートをコンボボックスに設定する.
    ''' </summary>
    ''' <param name="aComboBox"></param>
    ''' <remarks></remarks>
    Public Sub setTemplateToComboBox(ByRef aComboBox As System.Windows.Forms.ComboBox)

        'テンプレートファイル格納フォルダ（C:\（仮））配下のExcelファイル名を取得して、
        'String配列iVosに格納する。
        Dim iVos As List(Of String) = New List(Of String)

        Dim path As String = ExcelGousyaTemplateDir & "\"
        Dim allFiles As String() = System.IO.Directory.GetFiles(path)
        For Each fileName As String In allFiles
            If fileName.EndsWith(".xls") Then
                iVos.Add(fileName)
            End If
        Next

        'アイテムを削除.
        aComboBox.Items.Clear()
        aComboBox.Text = ""

        ''取得したボディ名をコンボボックスに設定.
        For Each lItem In iVos
            aComboBox.Items.Add(lItem)
        Next

        If 0 < aComboBox.Items.Count Then aComboBox.Text = aComboBox.Items(0).ToString

    End Sub

#End Region

    Public Sub ExcelOutput(ByVal EventCode As String, ByVal KaiteiNo As String, ByVal GroupNo As String, ByVal iGousya As List(Of String), ByVal Template As String)

        Dim tempName As String() = Template.Split(".")
        Dim workFileName As String = tempName(0) + " " + Now.ToString("MMdd") + " " + Now.ToString("HHmm") + "_work" + "." + tempName(1)
        Dim fileUrlList As String() = workFileName.Split("\")
        Dim isWB As Boolean = StrConv(fileUrlList(fileUrlList.Length - 1), VbStrConv.Narrow).Contains("WB")

        If (FileIO.FileSystem.FileExists(workFileName)) Then
            FileIO.FileSystem.DeleteFile(workFileName)
        End If

        ''Templateファイルをコピーして、workファイル（Templateファイル+_work.拡張子）を上書き作成する。
        FileCopy(Template, workFileName)
        Using xls As New ShisakuExcel(workFileName)
            Try
                xls.SetActiveSheet(2)
                '↓↓2014/10/01 酒井 ADD BEGIN
                'setUyiOP(xls, EventCode)
                'setChukbn(xls, EventCode)
                setUyiOP(xls, EventCode, KaiteiNo, isWB)
                setChukbn(xls, EventCode, KaiteiNo, isWB)
                '↑↑2014/10/01 酒井 ADD END
                xls.Save()
                xls.Dispose()
            Catch ex As Exception
                xls.Dispose()
                FileIO.FileSystem.DeleteFile(workFileName)
                Exit Sub
            End Try
        End Using
        'workファイルをコピーして号車別出力ファイルを作成()
        Dim saveFileFolder As String = ""
        For Each strUrl As String In fileUrlList
            If Not strUrl.EndsWith("_work" + "." + tempName(1)) Then
                saveFileFolder = saveFileFolder + strUrl + "\"
            End If
        Next

        Dim iDao As New XVLView.Dao.Impl.SeisakuNeedNameDaoImpl
        Dim iVOs As List(Of XVLView.Dao.Vo.SeiSakuGouSya)
        iVOs = iDao.GetEvent(EventCode, KaiteiNo)
        '↑↑2014/10/21 酒井 ADD END


        For index = 1 To iGousya.Count
            '↓↓2014/09/30 酒井 ADD BEGIN
            'Dim saveFilename As String = saveFileFolder + iGousya(index - 1).ToString + "." + tempName(1)
            '↓↓2014/10/06 酒井 ADD BEGIN
            '            Dim saveFilename As String = ExcelGousyaOutPutDir + "\" + iGousya(index - 1).ToString + "." + tempName(1)
            '↓↓2014/10/21 酒井 ADD BEGIN
            'Dim saveFilename As String = ExcelGousyaOutPutDir + "\" + iGousya(index - 1).ToString + " " + Now.ToString("MMdd") + " " + Now.ToString("HHmm") + "." + tempName(1)
            Dim saveFilename As String = ExcelGousyaOutPutDir + "\" + iVOs.Item(0).KaihatsuFugo + " " + iVOs.Item(0).SeisakuEventName + " " + iGousya(index - 1).ToString + " " + Now.ToString("MMdd") + " " + Now.ToString("HHmm") + "." + tempName(1)
            '↑↑2014/10/21 酒井 ADD END
            '↑↑2014/10/06 酒井 ADD END
            '↑↑2014/09/30 酒井 ADD END
            '
            If (FileIO.FileSystem.FileExists(saveFilename)) Then
                FileIO.FileSystem.DeleteFile(saveFilename)
            End If
            FileCopy(workFileName, saveFilename)
            Using filexls As New ShisakuExcel(saveFilename)
                'Sheet(1)から、各データが上書
                '↓↓2014/10/01 酒井 ADD BEGIN
                'setSheet1Data(filexls, EventCode, iGousya(index - 1))
                setSheet1Data(filexls, EventCode, KaiteiNo, iGousya(index - 1))
                '↑↑2014/10/01 酒井 ADD END
                'Sheet(2)から、各データが上書
                filexls.SetActiveSheet(2)
                '↓↓2014/10/01 酒井 ADD BEGIN
                'setSheet1Data(filexls, EventCode, iGousya(index - 1))
                'SetSheet2Data(filexls, EventCode, iGousya(index - 1))
                setSheet1Data(filexls, EventCode, KaiteiNo, iGousya(index - 1))
                SetSheet2Data(filexls, EventCode, KaiteiNo, iGousya(index - 1), isWB)
                '↑↑2014/10/01 酒井 ADD END
                filexls.SetActiveSheet(1)
                filexls.Save()
                filexls.Dispose()
            End Using
        Next

        FileIO.FileSystem.DeleteFile(workFileName)
        '↓↓2014/09/30 酒井 ADD BEGIN
        Process.Start(ExcelGousyaOutPutDir)
        '↑↑2014/09/30 酒井 ADD END
    End Sub

#Region "ﾗｲﾝOP"
    ''' <summary>
    ''' ﾗｲﾝOP出力
    ''' </summary>
    ''' <param name="xls"></param>
    ''' <remarks></remarks>
    Private Sub setUyiOP(ByVal xls As ShisakuExcel, ByVal EventCode As String, ByVal KaiteiNo As String, ByVal isWB As Boolean)
        Dim iDao As New XVLView.Dao.Impl.SeisakuNeedNameDaoImpl
        Dim iVOs As List(Of XVLView.Dao.Vo.SeisakuOPVo) = iDao.GetOPName(EventCode, KaiteiNo, isWB)
        mOPExistlist = New ArrayList(iVOs.Count)
        If iVOs Is Nothing OrElse 0 >= iVOs.Count Then Exit Sub
        Dim ReturnRow As Integer
        Dim ReturnCol As Integer
        '_OP_EXISTの列数
        Dim ReturnExcol As Integer
        '_OP_EXISTの行数
        Dim ReturnExrow As Integer
        '特別織込み項目テンプレートの列数
        Dim ReturnChoCol As Integer
        '特別織込み項目テンプレートの行数
        Dim ReturnChoRow As Integer

        Dim BaseMergedCellsRowCount As Integer
        Dim BaseNextRowCount As Integer
        '1行また、_OP_NAME(i) の数
        Dim StartRowCount As Integer
        Dim OpName1Row As Integer
        Dim OpName1col As Integer
        Dim OpExist1Row As Integer
        Dim OpExist1col As Integer
        If xls.Find("_OP_NAME1", ReturnCol, StartRowCount) Then
            OpName1col = ReturnCol
            OpName1Row = StartRowCount
            BaseMergedCellsRowCount = xls.GetMergedCellsRowCount(ReturnCol, StartRowCount)
            Dim index As Integer = 0
            Do
                index = index + 1
                If xls.Find("_OP_NAME" & index, ReturnCol, ReturnRow) Then
                    If ReturnRow > StartRowCount Then
                        BaseNextRowCount = ReturnRow - StartRowCount
                        StartRowCount = index - 1
                        Exit Do
                    End If
                End If
            Loop
        End If

        If xls.Find("_OP_EXIST1", ReturnCol, ReturnRow) Then
            OpExist1col = ReturnCol
            OpExist1Row = ReturnRow
        End If

        Dim i As Integer = 0
        Dim tmp As Integer
        Do
            i = i + 1
            If xls.Find("_OP_NAME" & i, ReturnCol, ReturnRow) Then
                tmp = i
            ElseIf xls.Find("_OP_EXIST" & i, ReturnCol, ReturnRow) Then
                tmp = i
            Else
                Exit Do
            End If
        Loop

        If iVOs.Count <= 0 Then
            Dim NameCol As Integer
            Dim NameRow As Integer
            Dim NameMergedCellsRowCount As Integer
            Dim Existcol As Integer
            Dim ExistRow As Integer
            Dim ExistMergedCellsRowCount As Integer
            Dim MinRow As Integer = 0
            Dim MaxRow As Integer = 0
            Dim flg As Boolean

            i = 0
            Do
                i = i + 1
                flg = False
                If xls.Find("_OP_NAME" & i, NameCol, NameRow) Then
                    flg = True
                    NameMergedCellsRowCount = xls.GetMergedCellsRowCount(NameCol, NameRow)
                    If MinRow = 0 Then
                        MinRow = NameRow
                    ElseIf MinRow > NameRow Then
                        MinRow = NameRow
                    End If

                    If MaxRow = 0 Then
                        MaxRow = NameRow + NameMergedCellsRowCount - 1
                    ElseIf MaxRow < NameRow + NameMergedCellsRowCount - 1 Then
                        MaxRow = NameRow + NameMergedCellsRowCount - 1
                    End If
                End If

                If xls.Find("_OP_EXIST" & i, Existcol, ExistRow) Then
                    flg = True
                    ExistMergedCellsRowCount = xls.GetMergedCellsRowCount(Existcol, ExistRow)
                    If MinRow = 0 Then
                        MinRow = ExistRow
                    ElseIf MinRow > ExistRow Then
                        MinRow = ExistRow
                    End If

                    If MaxRow = 0 Then
                        MaxRow = ExistRow + ExistMergedCellsRowCount - 1
                    ElseIf MaxRow < ExistRow + ExistMergedCellsRowCount - 1 Then
                        MaxRow = ExistRow + ExistMergedCellsRowCount - 1
                    End If
                End If

                If Not flg Then
                    Exit Do
                End If
            Loop

            xls.DeleteRow(MinRow, MaxRow - MinRow + 1)
            Exit Sub
        End If
        Dim PageCount As Integer = 1
        Dim ReturnCol2 As Integer
        Dim ReturnRow2 As Integer
        For index = 1 To iVOs.Count
            If Not xls.Find("_OP_NAME" & index, ReturnCol, ReturnRow) Then
                If xls.Find("_OP_NAME" & index - StartRowCount, ReturnCol, ReturnRow) Then
                    If index Mod StartRowCount = 1 Then
                        xls.InsertRow(ReturnRow + BaseMergedCellsRowCount, BaseNextRowCount)
                    End If
                    Dim AddNameMergedCellsColumnCount As Integer = xls.GetMergedCellsColumnCount(ReturnCol, ReturnRow)
                    Dim AddNameMergedCellsRowCount As Integer = xls.GetMergedCellsRowCount(ReturnCol, ReturnRow)
                    Dim AddNameMergedCellsRowBegin As Integer
                    If index Mod StartRowCount = 1 Then
                        AddNameMergedCellsRowBegin = ReturnRow + BaseNextRowCount
                    Else
                        '2列目以降の項目は、1列目の項目追加時に改ページヘッダー/タイトル行が挿入されているため、直前項目の行を参照する。
                        xls.Find("_OP_NAME" & index - 1, ReturnCol2, ReturnRow2)
                        AddNameMergedCellsRowBegin = ReturnRow2
                    End If
                    Dim AddNameMergedCellsRowEnd As Integer = AddNameMergedCellsRowBegin + AddNameMergedCellsRowCount - 1

                    xls.MergeCells(ReturnCol, AddNameMergedCellsRowBegin, ReturnCol + AddNameMergedCellsColumnCount - 1, AddNameMergedCellsRowEnd, True)

                    xls.SetLine(ReturnCol, AddNameMergedCellsRowBegin, ReturnCol + AddNameMergedCellsColumnCount - 1, AddNameMergedCellsRowEnd, XlBordersIndex.xlEdgeTop)

                    xls.SetLine(ReturnCol, AddNameMergedCellsRowBegin, ReturnCol + AddNameMergedCellsColumnCount - 1, AddNameMergedCellsRowEnd, XlBordersIndex.xlEdgeBottom)

                    xls.SetLine(ReturnCol, AddNameMergedCellsRowBegin, ReturnCol + AddNameMergedCellsColumnCount - 1, AddNameMergedCellsRowEnd, XlBordersIndex.xlEdgeLeft)

                    xls.SetLine(ReturnCol, AddNameMergedCellsRowBegin, ReturnCol + AddNameMergedCellsColumnCount - 1, AddNameMergedCellsRowEnd, XlBordersIndex.xlEdgeRight)

                    xls.SetValue(ReturnCol, AddNameMergedCellsRowBegin, ReturnCol + AddNameMergedCellsColumnCount - 1, AddNameMergedCellsRowEnd, "_OP_NAME" & index)

                    xls.CopyFont(OpName1col, OpName1Row, ReturnCol, AddNameMergedCellsRowBegin)

                    '_OP_EXIST(Index)を上書
                    xls.Find("_OP_EXIST" & index - StartRowCount, ReturnCol, ReturnRow)
                    Dim AddExistMergedCellsColumnCount As Integer = xls.GetMergedCellsColumnCount(ReturnCol, ReturnRow)
                    Dim AddExistMergedCellsRowCount As Integer = xls.GetMergedCellsRowCount(ReturnCol, ReturnRow)

                    Dim AddExistMergedCellsRowBegin As Integer
                    If index Mod StartRowCount = 1 Then
                        AddExistMergedCellsRowBegin = ReturnRow + BaseNextRowCount
                    Else
                        '2列目以降の項目は、1列目の項目追加時に改ページヘッダー/タイトル行が挿入されているため、直前項目の行を参照する。
                        xls.Find("_OP_EXIST" & index - 1, ReturnCol2, ReturnRow2)
                        AddExistMergedCellsRowBegin = ReturnRow2
                    End If
                    Dim AddExistMergedCellsRowEnd As Integer = AddExistMergedCellsRowBegin + AddExistMergedCellsRowCount - 1

                    xls.MergeCells(ReturnCol, AddNameMergedCellsRowBegin, ReturnCol + AddExistMergedCellsColumnCount - 1, AddExistMergedCellsRowEnd, True)

                    xls.SetLine(ReturnCol, AddNameMergedCellsRowBegin, ReturnCol + AddExistMergedCellsColumnCount - 1, AddExistMergedCellsRowEnd, XlBordersIndex.xlEdgeTop)

                    xls.SetLine(ReturnCol, AddNameMergedCellsRowBegin, ReturnCol + AddExistMergedCellsColumnCount - 1, AddExistMergedCellsRowEnd, XlBordersIndex.xlEdgeBottom)

                    xls.SetLine(ReturnCol, AddNameMergedCellsRowBegin, ReturnCol + AddExistMergedCellsColumnCount - 1, AddExistMergedCellsRowEnd, XlBordersIndex.xlEdgeLeft)

                    xls.SetLine(ReturnCol, AddNameMergedCellsRowBegin, ReturnCol + AddExistMergedCellsColumnCount - 1, AddExistMergedCellsRowEnd, XlBordersIndex.xlEdgeRight)

                    xls.SetValue(ReturnCol, AddNameMergedCellsRowBegin, ReturnCol + AddExistMergedCellsColumnCount - 1, AddExistMergedCellsRowEnd, "_OP_EXIST" & index)

                    xls.CopyFont(OpExist1col, OpExist1Row, ReturnCol, AddNameMergedCellsRowBegin)

                    '改ページ時のタイトル行挿入対応
                    Dim PageBreaksList As List(Of Integer) = xls.GetPageBreaks()
                    For k As Integer = PageBreaksList.Count - 1 To 0 Step -1
                        If PageBreaksList(k) <= AddNameMergedCellsRowEnd Then
                            If k + 2 = PageCount Then
                                '現在ページに変更がない（改ページしていない）
                                Exit For
                            End If

                            If PageBreaksList(k) > AddNameMergedCellsRowBegin Then
                                '結合セルの泣き別れ予防
                                xls.InsertRow(AddNameMergedCellsRowBegin, PageBreaksList(k) - AddNameMergedCellsRowBegin)
                            End If

                            If xls.Find("ﾗｲﾝOP", ReturnCol, ReturnRow) Then
                                Dim TitleMergedCellsRowCount As Integer = xls.GetMergedCellsRowCount(ReturnCol, ReturnRow)
                                xls.InsertRow(PageBreaksList(k), 1 + TitleMergedCellsRowCount + ReturnRow - 1)

                                'ヘッダー行（ﾗｲﾝOPより上）を追加
                                xls.CopySheetRowInsert2(2, 2, 1, PageBreaksList(k), ReturnRow - 1)

                                'タイトル行（ﾗｲﾝOP）を追加
                                xls.CopySheetRowInsert2(2, 2, ReturnRow, PageBreaksList(k) + ReturnRow - 1, TitleMergedCellsRowCount)
                            End If


                            '現在ページ数をセット
                            PageCount = PageCount + 1
                            Exit For
                        End If
                    Next
                End If
            Else
            End If
        Next
        For index = 1 To iVOs.Count
            '_OP_NAME(Index)を取得
            If xls.Find("_OP_NAME" & index, ReturnCol, ReturnRow) Then
                xls.SetValue(ReturnCol, ReturnRow, iVOs.Item(index - 1).OpName)
            End If
            '_OP_EXIST(Index)を取得
            If xls.Find("_OP_EXIST" & index, ReturnExcol, ReturnExrow) Then
                xls.SetAlignment(ReturnExcol, ReturnExrow, ReturnExcol, ReturnExrow + 1, ShisakuExcel.XlHAlign.xlHAlignCenter)
                xls.SetValue(ReturnExcol, ReturnExrow, "_" & iVOs.Item(index - 1).OpName)
            End If
            mOPExistlist.Add(ReturnExcol & "," & ReturnExrow)
        Next

        '空白行を削除()
        xls.Find("特別織込み項目", ReturnChoCol, ReturnChoRow)
        If ReturnChoCol < 0 AndAlso ReturnChoRow < 0 Then Exit Sub
        If (ReturnChoRow - BaseNextRowCount - ReturnExrow > 0) Then
            Dim ExMergedCellsRowCountw As Integer = xls.GetMergedCellsRowCount(ReturnExcol, ReturnExrow)
            xls.DeleteRow(ReturnExrow + ExMergedCellsRowCountw, ReturnChoRow - BaseNextRowCount - ReturnExrow)
        End If

        For j As Integer = 1 To tmp
            Do
                If xls.Find("_OP_NAME" & j, ReturnCol, ReturnRow) Then
                    xls.SetValue(ReturnCol, ReturnRow, ReturnCol, ReturnRow, "")
                Else
                    Exit Do
                End If
            Loop
            Do
                If xls.Find("_OP_EXIST" & j, ReturnCol, ReturnRow) Then
                    xls.SetValue(ReturnCol, ReturnRow, ReturnCol, ReturnRow, "")
                Else
                    Exit Do
                End If
            Loop
        Next
    End Sub

#End Region

#Region "特別織込み項目"
    ''' <summary>
    ''' 特別織込み項目出力
    ''' </summary>
    ''' <param name="xls"></param>
    ''' <remarks></remarks>
    Private Sub setChukbn(ByVal xls As ShisakuExcel, ByVal EventCode As String, ByVal KaiteiNo As String, ByVal isWB As Boolean)
        '特別織込み項目テンプレートの列数
        Dim RfChokbnCol As Integer
        '特別織込み項目テンプレートの行数
        Dim RfChoKbnRow As Integer
        Dim ReturnCol As Integer
        Dim ReturnRow As Integer
        Dim ReturnExcol As Integer
        Dim ReturnExrow As Integer

        Dim Name1Col As Integer
        Dim Name1Row As Integer
        Dim Exist1Col As Integer
        Dim Exist1Row As Integer

        Dim iDao As New XVLView.Dao.Impl.SeisakuNeedNameDaoImpl
        Dim iVOs As List(Of XVLView.Dao.Vo.SeisakuoOikomiVo) = iDao.GetShokbnName(EventCode, KaiteiNo, isWB)
        Dim BaseMergedCellsRowCount As Integer
        Dim BaseNextRowCount As Integer
        '1行また、_SHO_KBN_NAME(i) の数
        Dim StartRowCount As Integer
        If xls.Find("_SHO_KBN_NAME1", ReturnCol, StartRowCount) Then
            Name1Col = ReturnCol
            Name1Row = StartRowCount
            BaseMergedCellsRowCount = xls.GetMergedCellsRowCount(ReturnCol, StartRowCount)
        End If
        Dim index As Integer = 0
        Do
            index = index + 1
            If xls.Find("_SHO_KBN_NAME" & index, ReturnCol, ReturnRow) Then
                If ReturnRow > StartRowCount Then
                    BaseNextRowCount = ReturnRow - StartRowCount
                    StartRowCount = index - 1
                    Exit Do
                End If
            End If
        Loop

        If xls.Find("_SHO_KBN_EXIST1", ReturnCol, ReturnRow) Then
            Exist1Col = ReturnCol
            Exist1Row = ReturnRow
        End If

        Dim i As Integer = 0
        Dim tmp As Integer
        Do
            i = i + 1
            If xls.Find("_SHO_KBN_NAME" & i, ReturnCol, ReturnRow) Then
                tmp = i
            ElseIf xls.Find("_SHO_KBN_EXIST" & i, ReturnCol, ReturnRow) Then
                tmp = i
            Else
                Exit Do
            End If
        Loop

        Dim PageCount As Integer = 1
        Dim ReturnCol2 As Integer
        Dim ReturnRow2 As Integer
        Dim ReturnCol3 As Integer
        Dim ReturnRow3 As Integer
        Dim LastChuKbnRow As Integer

        '後続のfor文の中で、追加行に対する改ページ処理しか組み込んでいないため、
        '１）３行目以降は一旦消しておく（後続の追加行に対する改ページ処理に流す）
        '２）２行目以前（タイトル行（特別織込み）から２行目まで）は、事前に改ページチェックする
        If xls.Find("_SHO_KBN_NAME" & index + StartRowCount + StartRowCount, ReturnCol, ReturnRow) Then
            xls.DeleteRow(ReturnRow, xls.EndRow - ReturnRow + 1)
        End If

        xls.Find("_SHO_KBN_NAME" & index + StartRowCount, ReturnCol, ReturnRow)
        xls.Find("特別織込み項目", ReturnCol2, ReturnRow2)
        Dim PageBreaksList As List(Of Integer) = xls.GetPageBreaks()
        For k As Integer = PageBreaksList.Count - 1 To 0 Step -1
            If PageBreaksList(k) <= ReturnRow + BaseMergedCellsRowCount - 1 Then
                If PageBreaksList(k) > ReturnRow2 Then
                    xls.Find("ﾗｲﾝOP", ReturnCol3, ReturnRow3)
                    '結合セルの泣き別れ予防
                    xls.InsertRow(ReturnRow2, (ReturnRow3 - 1) + ((ReturnRow + BaseMergedCellsRowCount - 1) - ReturnRow2))
                    xls.CopySheetRowInsert2(2, 2, 1, PageBreaksList(k), ReturnRow3 - 1)
                End If
                PageCount = k + 2
                Exit For
            End If
        Next

        'workファイルに「_CHU_KBN_NAME」が入力されているセルがない場合
        If Not xls.Find("_CHU_KBN_NAME", RfChokbnCol, RfChoKbnRow) Then
            If iVOs.Count <= 0 Then
                ShokbnFlg = False
                If xls.Find("_SHO_KBN_NAME1", ReturnCol, ReturnRow) Then
                    xls.DeleteRow(ReturnRow, xls.EndRow - ReturnRow + BaseMergedCellsRowCount)
                End If
                Exit Sub
            End If
            mShoExistlist = New ArrayList(iVOs.Count)
            For index = 1 To iVOs.Count
                If Not xls.Find("_SHO_KBN_NAME" & index, ReturnCol, ReturnRow) Then
                    If xls.Find("_SHO_KBN_NAME" & index - StartRowCount, ReturnCol, ReturnRow) Then
                        If index Mod StartRowCount = 1 Then
                            xls.InsertRow(ReturnRow + BaseMergedCellsRowCount, BaseNextRowCount)
                        End If
                        Dim AddNameMergedCellsColumnCount As Integer = xls.GetMergedCellsColumnCount(ReturnCol, ReturnRow)
                        Dim AddNameMergedCellsRowCount As Integer = xls.GetMergedCellsRowCount(ReturnCol, ReturnRow)
                        Dim AddNameMergedCellsRowBegin As Integer
                        If index Mod StartRowCount = 1 Then
                            AddNameMergedCellsRowBegin = ReturnRow + BaseNextRowCount
                        Else
                            '2列目以降の項目は、1列目の項目追加時に改ページヘッダー/タイトル行が挿入されているため、直前項目の行を参照する。
                            xls.Find("_SHO_KBN_NAME" & index - 1, ReturnCol2, ReturnRow2)
                            AddNameMergedCellsRowBegin = ReturnRow2
                        End If
                        Dim AddNameMergedCellsRowEnd As Integer = AddNameMergedCellsRowBegin + AddNameMergedCellsRowCount - 1

                        xls.MergeCells(ReturnCol, AddNameMergedCellsRowBegin, ReturnCol + AddNameMergedCellsColumnCount - 1, AddNameMergedCellsRowEnd, True)
                        xls.SetLine(ReturnCol, AddNameMergedCellsRowBegin, ReturnCol + AddNameMergedCellsColumnCount - 1, AddNameMergedCellsRowEnd, XlBordersIndex.xlEdgeTop)
                        xls.SetLine(ReturnCol, AddNameMergedCellsRowBegin, ReturnCol + AddNameMergedCellsColumnCount - 1, AddNameMergedCellsRowEnd, XlBordersIndex.xlEdgeBottom)
                        xls.SetLine(ReturnCol, AddNameMergedCellsRowBegin, ReturnCol + AddNameMergedCellsColumnCount - 1, AddNameMergedCellsRowEnd, XlBordersIndex.xlEdgeLeft)
                        xls.SetLine(ReturnCol, AddNameMergedCellsRowBegin, ReturnCol + AddNameMergedCellsColumnCount - 1, AddNameMergedCellsRowEnd, XlBordersIndex.xlEdgeRight)
                        xls.SetValue(ReturnCol, AddNameMergedCellsRowBegin, ReturnCol + AddNameMergedCellsColumnCount - 1, AddNameMergedCellsRowEnd, "_SHO_KBN_NAME" & index)
                        xls.CopyFont(Name1Col, Name1Row, ReturnCol, AddNameMergedCellsRowBegin)

                        '_SHO_KBN_EXIST(Index)を上書
                        xls.Find("_SHO_KBN_EXIST" & index - StartRowCount, ReturnCol, ReturnRow)
                        Dim AddExistMergedCellsColumnCount As Integer = xls.GetMergedCellsColumnCount(ReturnCol, ReturnRow)
                        Dim AddExistMergedCellsRowCount As Integer = xls.GetMergedCellsRowCount(ReturnCol, ReturnRow)

                        Dim AddExistMergedCellsRowBegin As Integer
                        If index Mod StartRowCount = 1 Then
                            AddExistMergedCellsRowBegin = ReturnRow + BaseNextRowCount
                        Else
                            '2列目以降の項目は、1列目の項目追加時に改ページヘッダー/タイトル行が挿入されているため、直前項目の行を参照する。
                            xls.Find("_SHO_KBN_EXIST" & index - 1, ReturnCol2, ReturnRow2)
                            AddExistMergedCellsRowBegin = ReturnRow2
                        End If
                        Dim AddExistMergedCellsRowEnd As Integer = AddExistMergedCellsRowBegin + AddExistMergedCellsRowCount - 1

                        xls.MergeCells(ReturnCol, AddExistMergedCellsRowBegin, ReturnCol + AddExistMergedCellsColumnCount - 1, AddExistMergedCellsRowEnd, True)
                        xls.SetLine(ReturnCol, AddExistMergedCellsRowBegin, ReturnCol + AddExistMergedCellsColumnCount - 1, AddExistMergedCellsRowEnd, XlBordersIndex.xlEdgeTop)
                        xls.SetLine(ReturnCol, AddExistMergedCellsRowBegin, ReturnCol + AddExistMergedCellsColumnCount - 1, AddExistMergedCellsRowEnd, XlBordersIndex.xlEdgeBottom)
                        xls.SetLine(ReturnCol, AddExistMergedCellsRowBegin, ReturnCol + AddExistMergedCellsColumnCount - 1, AddExistMergedCellsRowEnd, XlBordersIndex.xlEdgeLeft)
                        xls.SetLine(ReturnCol, AddExistMergedCellsRowBegin, ReturnCol + AddExistMergedCellsColumnCount - 1, AddExistMergedCellsRowEnd, XlBordersIndex.xlEdgeRight)
                        xls.SetValue(ReturnCol, AddExistMergedCellsRowBegin, ReturnCol + AddExistMergedCellsColumnCount - 1, AddExistMergedCellsRowEnd, "_SHO_KBN_EXIST" & index)
                        xls.CopyFont(Exist1Col, Exist1Row, ReturnCol, AddExistMergedCellsRowBegin)

                        '改ページ時のタイトル行挿入対応
                        PageBreaksList = xls.GetPageBreaks()
                        For k As Integer = PageBreaksList.Count - 1 To 0 Step -1
                            If PageBreaksList(k) <= AddNameMergedCellsRowEnd Then
                                If k + 2 = PageCount Then
                                    '現在ページに変更がない（改ページしていない）
                                    Exit For
                                End If

                                If PageBreaksList(k) > AddNameMergedCellsRowBegin Then
                                    '結合セルの泣き別れ予防
                                    xls.InsertRow(AddNameMergedCellsRowBegin, PageBreaksList(k) - AddNameMergedCellsRowBegin)
                                End If

                                If xls.Find("ﾗｲﾝOP", ReturnCol, ReturnRow) Then
                                    If xls.Find("特別織込み項目", ReturnCol2, ReturnRow2) Then
                                        Dim TitleMergedCellsRowCount As Integer = xls.GetMergedCellsRowCount(ReturnCol2, ReturnRow2)
                                        xls.InsertRow(PageBreaksList(k), 1 + TitleMergedCellsRowCount + ReturnRow - 1)

                                        'ヘッダー行（ﾗｲﾝOPより上）を追加
                                        xls.CopySheetRowInsert2(2, 2, 1, PageBreaksList(k), ReturnRow - 1)

                                        'タイトル行（特別織込み項目）を追加
                                        xls.CopySheetRowInsert2(2, 2, ReturnRow2, PageBreaksList(k) + ReturnRow - 1, TitleMergedCellsRowCount)
                                    End If
                                End If

                                '現在ページ数をセット
                                PageCount = PageCount + 1
                                Exit For
                            End If
                        Next
                    End If
                End If
            Next
            For index = 1 To iVOs.Count
                '_SHO_KBN_NAME(Index)を取得
                If xls.Find("_SHO_KBN_NAME" & index, ReturnCol, ReturnRow) Then
                    xls.SetValue(ReturnCol, ReturnRow, iVOs.Item(index - 1).ShoKbnName)
                End If
                '_SHO_KBN_EXIST(Index)を取得
                If xls.Find("_SHO_KBN_EXIST" & index, ReturnExcol, ReturnExrow) Then
                    xls.SetValue(ReturnExcol, ReturnExrow, "_" & iVOs.Item(index - 1).ShoKbnName)
                End If
                mShoExistlist.Add(ReturnExcol & "," & ReturnExrow)
            Next
            '空白行を削除
            xls.DeleteRow(ReturnRow + BaseMergedCellsRowCount, xls.EndRow - ReturnRow - BaseMergedCellsRowCount + BaseMergedCellsRowCount)
        Else
            Dim BaseChuKbnNameRow As Integer = RfChoKbnRow
            Dim BaseChuKbnNamecol As Integer = RfChokbnCol
            Dim BaseChuKbnNameMergedCellsRowCount As Integer = xls.GetMergedCellsRowCount(BaseChuKbnNamecol, BaseChuKbnNameRow)
            If iVOs.Count <= 0 Then
                ShokbnFlg = False
                If xls.Find("_CHU_KBN_NAME", ReturnCol, ReturnRow) Then
                    xls.DeleteRow(ReturnRow, xls.EndRow - ReturnRow + BaseMergedCellsRowCount)
                End If
                Exit Sub
            End If
            mShoExistlist = New ArrayList(iVOs.Count)
            Dim ChuKbnNameMae As String = ""
            '特別織込み項目の中項目から、各中項目は「_SHO_KBN_NAME」のCount
            ''削除行Flg
            Dim BreakFlg As Integer = 1
            Dim SeReturnCol As Integer = -1
            Dim SeReturnRow As Integer = -1
            Dim FromtoEndCol As Integer = -1
            xls.Find("_SHO_KBN_NAME1", ReturnCol, ReturnRow)
            xls.Find("_SHO_KBN_NAME2", SeReturnCol, SeReturnRow)
            If SeReturnCol > 0 Then
                FromtoEndCol = SeReturnCol - ReturnCol
            End If
            ''特別織込み項目テンプレートの行数を削除する
            xls.DeleteRow(ReturnRow + BaseNextRowCount, xls.EndRow - ReturnRow - BaseNextRowCount + BaseMergedCellsRowCount)
            For indexCount = 1 To iVOs.Count
                Dim StrChuKbnName As String = ""
                If Not String.IsNullOrEmpty(iVOs.Item(indexCount - 1).ChuKbnName) Then
                    StrChuKbnName = iVOs.Item(indexCount - 1).ChuKbnName.ToString
                End If
                If Not String.Equals(ChuKbnNameMae, StrChuKbnName) Then
                    If xls.Find("_CHU_KBN_NAME", RfChokbnCol, RfChoKbnRow) Then
                        xls.SetValue(RfChokbnCol, RfChoKbnRow, iVOs.Item(indexCount - 1).ChuKbnName)
                        LastChuKbnRow = RfChoKbnRow
                    End If
                    Dim ExitFlg As Boolean = False

                    ExitFlg = xls.Find("_SHO_KBN_NAME" & indexCount - 1, ReturnCol, ReturnRow)
                    If ExitFlg Then
                        '「_CHU_KBN_NAME」コーピ
                        xls.CopySheetRowInsert(2, 2, BaseChuKbnNameRow, ReturnRow + BaseNextRowCount, BaseChuKbnNameMergedCellsRowCount)
                        xls.SetActiveSheet(2)
                        xls.SetValue(BaseChuKbnNamecol, ReturnRow + BaseNextRowCount, iVOs.Item(indexCount - 1).ChuKbnName)
                        LastChuKbnRow = ReturnRow + BaseNextRowCount

                        '_SHO_KBN_NAME(Index)を上書
                        Dim AddNameMergedCellsRowCount As Integer = xls.GetMergedCellsRowCount(Name1Col, Name1Row)
                        Dim AddNameMergedCellsColumnCount As Integer = xls.GetMergedCellsColumnCount(Name1Col, Name1Row)

                        Dim AddNameMergedCellsRowBegin As Integer
                        AddNameMergedCellsRowBegin = ReturnRow + AddNameMergedCellsRowCount - 1 + 1 + BaseChuKbnNameMergedCellsRowCount + 1 + 1
                        Dim AddNameMergedCellsRowEnd As Integer = AddNameMergedCellsRowBegin + AddNameMergedCellsRowCount - 1

                        xls.MergeCells(Name1Col, AddNameMergedCellsRowBegin, Name1Col + AddNameMergedCellsColumnCount - 1, AddNameMergedCellsRowEnd, True)
                        xls.SetLine(Name1Col, AddNameMergedCellsRowBegin, Name1Col + AddNameMergedCellsColumnCount - 1, AddNameMergedCellsRowEnd, XlBordersIndex.xlEdgeTop)
                        xls.SetLine(Name1Col, AddNameMergedCellsRowBegin, Name1Col + AddNameMergedCellsColumnCount - 1, AddNameMergedCellsRowEnd, XlBordersIndex.xlEdgeBottom)
                        xls.SetLine(Name1Col, AddNameMergedCellsRowBegin, Name1Col + AddNameMergedCellsColumnCount - 1, AddNameMergedCellsRowEnd, XlBordersIndex.xlEdgeLeft)
                        xls.SetLine(Name1Col, AddNameMergedCellsRowBegin, Name1Col + AddNameMergedCellsColumnCount - 1, AddNameMergedCellsRowEnd, XlBordersIndex.xlEdgeRight)
                        xls.SetFont(Name1Col, AddNameMergedCellsRowBegin, Name1Col + AddNameMergedCellsColumnCount - 1, AddNameMergedCellsRowEnd, "ＭＳ Ｐゴシック", 12, , True)
                        xls.SetValue(Name1Col, AddNameMergedCellsRowBegin, Name1Col + AddNameMergedCellsColumnCount - 1, AddNameMergedCellsRowEnd, "_SHO_KBN_NAME" & indexCount)
                        xls.CopyFont(Name1Col, Name1Row, Name1Col, AddNameMergedCellsRowBegin)

                        '_SHO_KBN_EXIST(Index)を上書
                        xls.Find("_SHO_KBN_EXIST" & indexCount - 1, ReturnCol, ReturnRow)
                        Dim AddExistMergedCellsRowCount As Integer = xls.GetMergedCellsRowCount(Exist1Col, Exist1Row)
                        Dim AddExistMergedCellsColumnCount As Integer = xls.GetMergedCellsColumnCount(Exist1Col, Exist1Row)

                        Dim AddExistMergedCellsRowBegin As Integer
                        AddExistMergedCellsRowBegin = ReturnRow + AddExistMergedCellsRowCount - 1 + 1 + BaseChuKbnNameMergedCellsRowCount + 1 + 1
                        Dim AddExistMergedCellsRowEnd As Integer = AddExistMergedCellsRowBegin + AddExistMergedCellsRowCount - 1

                        xls.MergeCells(Exist1Col, AddExistMergedCellsRowBegin, Exist1Col + AddExistMergedCellsColumnCount - 1, AddExistMergedCellsRowEnd, True)
                        xls.SetLine(Exist1Col, AddExistMergedCellsRowBegin, Exist1Col + AddExistMergedCellsColumnCount - 1, AddExistMergedCellsRowEnd, XlBordersIndex.xlEdgeTop)
                        xls.SetLine(Exist1Col, AddExistMergedCellsRowBegin, Exist1Col + AddExistMergedCellsColumnCount - 1, AddExistMergedCellsRowEnd, XlBordersIndex.xlEdgeBottom)
                        xls.SetLine(Exist1Col, AddExistMergedCellsRowBegin, Exist1Col + AddExistMergedCellsColumnCount - 1, AddExistMergedCellsRowEnd, XlBordersIndex.xlEdgeLeft)
                        xls.SetLine(Exist1Col, AddExistMergedCellsRowBegin, Exist1Col + AddExistMergedCellsColumnCount - 1, AddExistMergedCellsRowEnd, XlBordersIndex.xlEdgeRight)
                        xls.SetFont(Exist1Col, AddExistMergedCellsRowBegin, Exist1Col + AddExistMergedCellsColumnCount - 1, AddExistMergedCellsRowEnd, "ＭＳ Ｐゴシック", 20, , True)
                        xls.SetValue(Exist1Col, AddExistMergedCellsRowBegin, Exist1Col + AddExistMergedCellsColumnCount - 1, AddExistMergedCellsRowEnd, "_SHO_KBN_EXIST" & indexCount)
                        xls.CopyFont(Exist1Col, Exist1Row, Exist1Col, AddExistMergedCellsRowBegin)

                        '改ページ時のタイトル行挿入対応
                        PageBreaksList = xls.GetPageBreaks()
                        For k As Integer = PageBreaksList.Count - 1 To 0 Step -1
                            If PageBreaksList(k) <= AddNameMergedCellsRowEnd Then
                                If k + 2 = PageCount Then
                                    '現在ページに変更がない（改ページしていない）
                                    Exit For
                                End If

                                If PageBreaksList(k) > LastChuKbnRow Then
                                    'サブタイトル行～１項目目の泣き別れ予防
                                    xls.InsertRow(LastChuKbnRow, PageBreaksList(k) - LastChuKbnRow)
                                End If

                                If xls.Find("ﾗｲﾝOP", ReturnCol, ReturnRow) Then
                                    If xls.Find("特別織込み項目", ReturnCol2, ReturnRow2) Then
                                        Dim TitleMergedCellsRowCount As Integer = xls.GetMergedCellsRowCount(ReturnCol2, ReturnRow2)
                                        xls.InsertRow(PageBreaksList(k), 1 + TitleMergedCellsRowCount + ReturnRow - 1)

                                        'ヘッダー行（ﾗｲﾝOPより上）を追加
                                        xls.CopySheetRowInsert2(2, 2, 1, PageBreaksList(k), ReturnRow - 1)

                                        'タイトル行（特別織込み項目）を追加
                                        xls.CopySheetRowInsert2(2, 2, ReturnRow2, PageBreaksList(k) + ReturnRow - 1, TitleMergedCellsRowCount)

                                    End If
                                End If

                                '現在ページ数をセット
                                PageCount = PageCount + 1
                                Exit For
                            End If
                        Next
                    End If
                    BreakFlg = 2
                Else
                    If xls.Find("_CHU_KBN_NAME", RfChokbnCol, RfChoKbnRow) Then
                        xls.SetValue(RfChokbnCol, RfChoKbnRow, iVOs.Item(indexCount - 1).ChuKbnName)
                    End If
                    If Not xls.Find("_SHO_KBN_NAME" & indexCount, ReturnCol, ReturnRow) Then
                        If BreakFlg Mod StartRowCount = 1 Then
                            If xls.Find("_SHO_KBN_NAME" & (indexCount - StartRowCount), ReturnCol, ReturnRow) Then
                                '_SHO_KBN_NAME(Index)を上書
                                Dim AddNameMergedCellsRowCount As Integer = xls.GetMergedCellsRowCount(ReturnCol, ReturnRow)
                                Dim AddNameMergedCellsColumnCount As Integer = xls.GetMergedCellsColumnCount(ReturnCol, ReturnRow)

                                Dim AddNameMergedCellsRowBegin As Integer
                                AddNameMergedCellsRowBegin = ReturnRow + BaseNextRowCount
                                Dim AddNameMergedCellsRowEnd As Integer = AddNameMergedCellsRowBegin + AddNameMergedCellsRowCount - 1

                                xls.MergeCells(ReturnCol, AddNameMergedCellsRowBegin, ReturnCol + AddNameMergedCellsColumnCount - 1, AddNameMergedCellsRowEnd, True)
                                xls.SetLine(ReturnCol, AddNameMergedCellsRowBegin, ReturnCol + AddNameMergedCellsColumnCount - 1, AddNameMergedCellsRowEnd, XlBordersIndex.xlEdgeTop)
                                xls.SetLine(ReturnCol, AddNameMergedCellsRowBegin, ReturnCol + AddNameMergedCellsColumnCount - 1, AddNameMergedCellsRowEnd, XlBordersIndex.xlEdgeBottom)
                                xls.SetLine(ReturnCol, AddNameMergedCellsRowBegin, ReturnCol + AddNameMergedCellsColumnCount - 1, AddNameMergedCellsRowEnd, XlBordersIndex.xlEdgeLeft)
                                xls.SetLine(ReturnCol, AddNameMergedCellsRowBegin, ReturnCol + AddNameMergedCellsColumnCount - 1, AddNameMergedCellsRowEnd, XlBordersIndex.xlEdgeRight)
                                xls.SetFont(ReturnCol, AddNameMergedCellsRowBegin, ReturnCol + AddNameMergedCellsColumnCount - 1, AddNameMergedCellsRowEnd, "ＭＳ Ｐゴシック", 12, , True)
                                xls.SetValue(ReturnCol, AddNameMergedCellsRowBegin, ReturnCol + AddNameMergedCellsColumnCount - 1, AddNameMergedCellsRowEnd, "_SHO_KBN_NAME" & indexCount)
                                xls.CopyFont(Name1Col, Name1Row, ReturnCol, AddNameMergedCellsRowBegin)

                                '_SHO_KBN_EXIST(Index)を上書
                                xls.Find("_SHO_KBN_EXIST" & (indexCount - StartRowCount), ReturnCol, ReturnRow)
                                Dim AddExistMergedCellsRowCount As Integer = xls.GetMergedCellsRowCount(ReturnCol, ReturnRow)
                                Dim AddExistMergedCellsColumnCount As Integer = xls.GetMergedCellsColumnCount(ReturnCol, ReturnRow)

                                Dim AddExistMergedCellsRowBegin As Integer
                                AddExistMergedCellsRowBegin = ReturnRow + BaseNextRowCount
                                Dim AddExistMergedCellsRowEnd As Integer = AddExistMergedCellsRowBegin + AddExistMergedCellsRowCount - 1

                                xls.MergeCells(ReturnCol, AddExistMergedCellsRowBegin, ReturnCol + AddExistMergedCellsColumnCount - 1, AddExistMergedCellsRowEnd, True)
                                xls.SetLine(ReturnCol, AddExistMergedCellsRowBegin, ReturnCol + AddExistMergedCellsColumnCount - 1, AddExistMergedCellsRowEnd, XlBordersIndex.xlEdgeTop)
                                xls.SetLine(ReturnCol, AddExistMergedCellsRowBegin, ReturnCol + AddExistMergedCellsColumnCount - 1, AddExistMergedCellsRowEnd, XlBordersIndex.xlEdgeBottom)
                                xls.SetLine(ReturnCol, AddExistMergedCellsRowBegin, ReturnCol + AddExistMergedCellsColumnCount - 1, AddExistMergedCellsRowEnd, XlBordersIndex.xlEdgeLeft)
                                xls.SetLine(ReturnCol, AddExistMergedCellsRowBegin, ReturnCol + AddExistMergedCellsColumnCount - 1, AddExistMergedCellsRowEnd, XlBordersIndex.xlEdgeRight)
                                xls.SetFont(ReturnCol, AddExistMergedCellsRowBegin, ReturnCol + AddExistMergedCellsColumnCount - 1, AddExistMergedCellsRowEnd, "ＭＳ Ｐゴシック", 20, , True)
                                xls.SetValue(ReturnCol, AddExistMergedCellsRowBegin, ReturnCol + AddExistMergedCellsColumnCount - 1, AddExistMergedCellsRowEnd, "_SHO_KBN_EXIST" & indexCount)
                                xls.CopyFont(Exist1Col, Exist1Row, ReturnCol, AddExistMergedCellsRowBegin)

                                '改ページ時のタイトル行挿入対応
                                PageBreaksList = xls.GetPageBreaks()
                                For k As Integer = PageBreaksList.Count - 1 To 0 Step -1
                                    If PageBreaksList(k) <= AddNameMergedCellsRowEnd Then
                                        If k + 2 = PageCount Then
                                            '現在ページに変更がない（改ページしていない）
                                            Exit For
                                        End If

                                        If PageBreaksList(k) > AddNameMergedCellsRowBegin Then
                                            '結合セルの泣き別れ予防
                                            xls.InsertRow(AddNameMergedCellsRowBegin, PageBreaksList(k) - AddNameMergedCellsRowBegin)
                                        End If

                                        If xls.Find("ﾗｲﾝOP", ReturnCol, ReturnRow) Then
                                            If xls.Find("特別織込み項目", ReturnCol2, ReturnRow2) Then
                                                Dim TitleMergedCellsRowCount As Integer = xls.GetMergedCellsRowCount(ReturnCol2, ReturnRow2)
                                                xls.InsertRow(PageBreaksList(k), 1 + BaseChuKbnNameMergedCellsRowCount + 1 + TitleMergedCellsRowCount + ReturnRow - 1)

                                                'ヘッダー行（ﾗｲﾝOPより上）を追加
                                                xls.CopySheetRowInsert2(2, 2, 1, PageBreaksList(k), ReturnRow - 1)

                                                'タイトル行（特別織込み項目）を追加
                                                xls.CopySheetRowInsert2(2, 2, ReturnRow2, PageBreaksList(k) + ReturnRow - 1, TitleMergedCellsRowCount)

                                                'サブタイトル行（中項目）を追加
                                                xls.CopySheetRowInsert2(2, 2, BaseChuKbnNameRow, PageBreaksList(k) + 1 + TitleMergedCellsRowCount + ReturnRow - 1, BaseChuKbnNameMergedCellsRowCount)
                                                xls.SetActiveSheet(2)
                                                xls.SetValue(BaseChuKbnNamecol, PageBreaksList(k) + 1 + TitleMergedCellsRowCount + ReturnRow - 1, iVOs.Item(indexCount - 1).ChuKbnName)
                                            End If
                                        End If

                                        '現在ページ数をセット
                                        PageCount = PageCount + 1
                                        Exit For
                                    End If
                                Next
                                ''↑↑2014/09/11 Ⅰ.8.号車別仕様書 作成機能 酒井 ADD END

                            End If
                        Else
                            If (xls.Find("_SHO_KBN_NAME" & (indexCount - 1), ReturnCol, ReturnRow)) Then
                                Dim AddNameMergedCellsRowCount As Integer = xls.GetMergedCellsRowCount(ReturnCol, ReturnRow)
                                Dim AddNameMergedCellsColumnCount As Integer = xls.GetMergedCellsColumnCount(ReturnCol, ReturnRow)
                                ReturnCol = ReturnCol + FromtoEndCol
                                '_SHO_KBN_NAME(Index)を上書
                                xls.MergeCells(ReturnCol, ReturnRow, ReturnCol + AddNameMergedCellsColumnCount - 1, ReturnRow + AddNameMergedCellsRowCount - 1, True)
                                xls.SetLine(ReturnCol, ReturnRow, ReturnCol + AddNameMergedCellsColumnCount - 1, ReturnRow + AddNameMergedCellsRowCount - 1, XlBordersIndex.xlEdgeTop)
                                xls.SetLine(ReturnCol, ReturnRow, ReturnCol + AddNameMergedCellsColumnCount - 1, ReturnRow + AddNameMergedCellsRowCount - 1, XlBordersIndex.xlEdgeBottom)
                                xls.SetLine(ReturnCol, ReturnRow, ReturnCol + AddNameMergedCellsColumnCount - 1, ReturnRow + AddNameMergedCellsRowCount - 1, XlBordersIndex.xlEdgeLeft)
                                xls.SetLine(ReturnCol, ReturnRow, ReturnCol + AddNameMergedCellsColumnCount - 1, ReturnRow + AddNameMergedCellsRowCount - 1, XlBordersIndex.xlEdgeRight)
                                xls.SetFont(ReturnCol, ReturnRow, ReturnCol + AddNameMergedCellsColumnCount - 1, ReturnRow + AddNameMergedCellsRowCount - 1, "ＭＳ Ｐゴシック", 12, , True)
                                xls.SetValue(ReturnCol, ReturnRow, ReturnCol + AddNameMergedCellsColumnCount - 1, ReturnRow + AddNameMergedCellsRowCount - 1, "_SHO_KBN_NAME" & indexCount)
                                xls.CopyFont(Name1Col, Name1Row, ReturnCol, ReturnRow)

                                '_SHO_KBN_EXIST(Index)を上書
                                xls.Find("_SHO_KBN_EXIST" & (indexCount - 1), ReturnCol, ReturnRow)
                                Dim AddExistMergedCellsRowCount As Integer = xls.GetMergedCellsRowCount(ReturnCol, ReturnRow)
                                Dim AddExistMergedCellsColumnCount As Integer = xls.GetMergedCellsColumnCount(ReturnCol, ReturnRow)
                                ReturnCol = ReturnCol + FromtoEndCol

                                xls.MergeCells(ReturnCol, ReturnRow, ReturnCol + AddExistMergedCellsColumnCount - 1, ReturnRow + AddExistMergedCellsRowCount - 1, True)
                                xls.SetLine(ReturnCol, ReturnRow, ReturnCol + AddExistMergedCellsColumnCount - 1, ReturnRow + AddExistMergedCellsRowCount - 1, XlBordersIndex.xlEdgeTop)
                                xls.SetLine(ReturnCol, ReturnRow, ReturnCol + AddExistMergedCellsColumnCount - 1, ReturnRow + AddExistMergedCellsRowCount - 1, XlBordersIndex.xlEdgeBottom)
                                xls.SetLine(ReturnCol, ReturnRow, ReturnCol + AddExistMergedCellsColumnCount - 1, ReturnRow + AddExistMergedCellsRowCount - 1, XlBordersIndex.xlEdgeLeft)
                                xls.SetLine(ReturnCol, ReturnRow, ReturnCol + AddExistMergedCellsColumnCount - 1, ReturnRow + AddExistMergedCellsRowCount - 1, XlBordersIndex.xlEdgeRight)
                                xls.SetFont(ReturnCol, ReturnRow, ReturnCol + AddExistMergedCellsColumnCount - 1, ReturnRow + AddExistMergedCellsRowCount - 1, "ＭＳ Ｐゴシック", 20, , True)
                                xls.SetValue(ReturnCol, ReturnRow, ReturnCol + AddExistMergedCellsColumnCount - 1, ReturnRow + AddExistMergedCellsRowCount - 1, "_SHO_KBN_EXIST" & indexCount)
                                xls.CopyFont(Exist1Col, Exist1Row, ReturnCol, ReturnRow)
                            End If
                        End If
                    End If
                    BreakFlg += 1
                End If
                If String.IsNullOrEmpty(iVOs.Item(indexCount - 1).ChuKbnName) Then
                    ChuKbnNameMae = ""
                Else
                    ChuKbnNameMae = iVOs.Item(indexCount - 1).ChuKbnName
                End If
                xls.Save()
            Next
            For indexCount = 1 To iVOs.Count
                '_SHO_KBN_NAME(Index)を取得
                If xls.Find("_SHO_KBN_NAME" & indexCount, ReturnCol, ReturnRow) Then
                    xls.SetValue(ReturnCol, ReturnRow, iVOs.Item(indexCount - 1).ShoKbnName)
                End If
                '_SHO_KBN_EXIST(Index)を取得
                If xls.Find("_SHO_KBN_EXIST" & indexCount, ReturnExcol, ReturnExrow) Then
                    xls.SetValue(ReturnExcol, ReturnExrow, "_" & iVOs.Item(indexCount - 1).ShoKbnName)
                End If
                mShoExistlist.Add(ReturnExcol & "," & ReturnExrow)
            Next
        End If
        For j As Integer = 1 To tmp
            Do
                If xls.Find("_SHO_KBN_NAME" & j, ReturnCol, ReturnRow) Then
                    xls.SetValue(ReturnCol, ReturnRow, ReturnCol, ReturnRow, "")
                Else
                    Exit Do
                End If
            Loop
            Do
                If xls.Find("_SHO_KBN_EXIST" & j, ReturnCol, ReturnRow) Then
                    xls.SetValue(ReturnCol, ReturnRow, ReturnCol, ReturnRow, "")
                Else
                    Exit Do
                End If
            Loop
        Next
    End Sub

#End Region

#Region "Sheet1から、各データが上書"
    Private Sub setSheet1Data(ByVal xls As ShisakuExcel, ByVal EventCode As String, ByVal KaiteiNo As String, ByVal Gousya As String)
        Dim iDao As New XVLView.Dao.Impl.SeisakuNeedNameDaoImpl
        Dim iVOs As List(Of XVLView.Dao.Vo.SeiSakuGouSya)
        Dim ReturnCol As Integer
        Dim ReturnRow As Integer
        Dim ReturnFlg As Boolean = False
        iVOs = iDao.GetEventName(EventCode, KaiteiNo)
        ''イベント名を取得
        mEventName = iVOs.Item(0).SeisakuEventName

        Do
            If Not xls.Find("_SEISAKU_EVENT", ReturnCol, ReturnRow) Then
                Exit Do
            End If
            xls.SetValue(ReturnCol, ReturnRow, mEventName)
        Loop
        'A1セルが結合セルの場合、Findでヒットしなかったので、個別にチェックする。
        If xls.GetValue(1, 1) = "_SEISAKU_EVENT" Then
            xls.SetValue(1, 1, mEventName)
        End If
        ''↑↑2014/09/09 Ⅰ.8.号車別仕様書 作成機能_q) 酒井 ADD END

        'テーブル「T_SEISAKU_ICHIRAN_KANSEI」のすべてデータを取得
        '↓↓2014/10/01 酒井 ADD BEGIN
        '        iVOs = iDao.GetIchirankanseiData(EventCode, Gousya)
        iVOs = iDao.GetIchirankanseiData(EventCode, KaiteiNo, Gousya)
        '↑↑2014/10/01 酒井 ADD END
        '↓↓2014/10/01 酒井 ADD BEGIN
        'If (iVOs.Count < 0) Then
        If Not (iVOs.Count > 0) Then
            '↑↑2014/10/01 酒井 ADD END
            'テーブル「T_SEISAKU_ICHIRAN_WB」のすべてデータを取得
            '↓↓2014/10/01 酒井 ADD BEGIN
            '            iVOs = iDao.GetIchiranwbData(EventCode, Gousya)
            iVOs = iDao.GetIchiranwbData(EventCode, KaiteiNo, Gousya)
            '↑↑2014/10/01 酒井 ADD END
            If (iVOs.Count > 0) Then
                ReturnFlg = True
            End If
        Else
            ReturnFlg = True
        End If
        '結果セット.COUNT>0の場合
        If ReturnFlg Then

            ''↓↓2014/09/09 Ⅰ.8.号車別仕様書 作成機能_q) 酒井 ADD BEGIN
            ''号車
            Do
                If Not xls.Find("_GOUSYA", ReturnCol, ReturnRow) Then
                    Exit Do
                End If
                xls.SetValue(ReturnCol, ReturnRow, iVOs.Item(0).Gousya)
            Loop
            ''車型
            Do
                If Not xls.Find("_SYASYU", ReturnCol, ReturnRow) Then
                    Exit Do
                End If
                xls.SetValue(ReturnCol, ReturnRow, iVOs.Item(0).Syasyu)
            Loop
            ''ｸﾞﾚｰﾄﾞ
            Do
                If Not xls.Find("_GRADE", ReturnCol, ReturnRow) Then
                    Exit Do
                End If
                xls.SetValue(ReturnCol, ReturnRow, iVOs.Item(0).Grade)
            Loop
            ''E/G
            Do
                If Not xls.Find("_EG_EG_NAME", ReturnCol, ReturnRow) Then
                    Exit Do
                End If
                xls.SetValue(ReturnCol, ReturnRow, iVOs.Item(0).EgEgName)
            Loop
            '↓↓2014/10/21 酒井 ADD BEGIN
            Do
                If Not xls.Find("_EG_EG_HAIKIRYOU", ReturnCol, ReturnRow) Then
                    Exit Do
                End If
                xls.SetValue(ReturnCol, ReturnRow, iVOs.Item(0).EgHaikiryou)
            Loop
            '↑↑2014/10/21 酒井 ADD END
            ''過給器
            Do
                If Not xls.Find("_EG_KAKYUUKI", ReturnCol, ReturnRow) Then
                    Exit Do
                End If
                xls.SetValue(ReturnCol, ReturnRow, iVOs.Item(0).EgKakyuuki)
            Loop
            ''T/M
            Do
                If Not xls.Find("_TM_TM_NAME", ReturnCol, ReturnRow) Then
                    Exit Do
                End If
                xls.SetValue(ReturnCol, ReturnRow, iVOs.Item(0).TmTmName)
            Loop
            '↓↓2014/10/21 酒井 ADD BEGIN
            Do
                If Not xls.Find("_TM_TM_HENSOKUKI", ReturnCol, ReturnRow) Then
                    Exit Do
                End If
                xls.SetValue(ReturnCol, ReturnRow, iVOs.Item(0).TmHensokuki)
            Loop
            '↑↑2014/10/21 酒井 ADD END
            ''仕向け
            Do
                If Not xls.Find("_SHIMUKE", ReturnCol, ReturnRow) Then
                    Exit Do
                End If
                xls.SetValue(ReturnCol, ReturnRow, iVOs.Item(0).Shimuke)
            Loop
            ''ﾊﾝﾄﾞﾙ
            Do
                If Not xls.Find("_HANDORU", ReturnCol, ReturnRow) Then
                    Exit Do
                End If
                xls.SetValue(ReturnCol, ReturnRow, iVOs.Item(0).Handoru)
            Loop
            ''ｸﾞﾙｰﾌﾟ
            Do
                If Not xls.Find("_SEISAKU_GROUP", ReturnCol, ReturnRow) Then
                    Exit Do
                End If
                xls.SetValue(ReturnCol, ReturnRow, iVOs.Item(0).SeisakuGroup)
            Loop
            ''完成日
            Do
                ''↓↓2014/09/10 Ⅰ.8.号車別仕様書 作成機能_q) 酒井 ADD BEGIN
                'If Not xls.Find("_CREATED_DATE", ReturnCol, ReturnRow) Then
                If Not xls.Find("_KANSEI_KIBOU_BI", ReturnCol, ReturnRow) Then
                    ''↓↓2014/09/10 Ⅰ.8.号車別仕様書 作成機能_q) 酒井 ADD END
                    Exit Do
                End If
                ''↓↓2014/09/10 Ⅰ.8.号車別仕様書 作成機能_q) 酒井 ADD BEGIN
                'xls.SetValue(ReturnCol, ReturnRow, iVOs.Item(0).CreatedDate)
                xls.SetValue(ReturnCol, ReturnRow, iVOs.Item(0).KanseiKibouBi)
                ''↓↓2014/09/10 Ⅰ.8.号車別仕様書 作成機能_q) 酒井 ADD END
            Loop
            ''型式
            Do
                If Not xls.Find("_KATASHIKI_SCD_7", ReturnCol, ReturnRow) Then
                    Exit Do
                End If
                xls.SetValue(ReturnCol, ReturnRow, iVOs.Item(0).KatashikiScd7)
            Loop
            ''OPｺｰﾄﾞ
            Do
                If Not xls.Find("_KATASHIKI_OP", ReturnCol, ReturnRow) Then
                    Exit Do
                End If
                xls.SetValue(ReturnCol, ReturnRow, iVOs.Item(0).KatashikiOp)
            Loop
            ''（外）色ｺｰﾄﾞ
            Do
                If Not xls.Find("_GAISOUSYOKU", ReturnCol, ReturnRow) Then
                    Exit Do
                End If
                xls.SetValue(ReturnCol, ReturnRow, iVOs.Item(0).Gaisousyoku)
            Loop
            ''外装色
            Do
                If Not xls.Find("_GAISOUSYOKU_NAME", ReturnCol, ReturnRow) Then
                    Exit Do
                End If
                xls.SetValue(ReturnCol, ReturnRow, iVOs.Item(0).GaisousyokuName)
            Loop
            ''（内）色ｺｰﾄﾞ
            Do
                If Not xls.Find("_NAISOUSYOKU", ReturnCol, ReturnRow) Then
                    Exit Do
                End If
                xls.SetValue(ReturnCol, ReturnRow, iVOs.Item(0).Naisousyoku)
            Loop
            ''内装色
            Do
                If Not xls.Find("_NAISOUSYOKU_NAME", ReturnCol, ReturnRow) Then
                    Exit Do
                End If
                xls.SetValue(ReturnCol, ReturnRow, iVOs.Item(0).NaisousyokuName)
            Loop
            ''車体№
            Do
                If Not xls.Find("_SYATAI_NO", ReturnCol, ReturnRow) Then
                    Exit Do
                End If
                xls.SetValue(ReturnCol, ReturnRow, iVOs.Item(0).SyataiNo)
            Loop
            ''使用部署
            Do
                If Not xls.Find("_SHIYOU_BUSYO", ReturnCol, ReturnRow) Then
                    Exit Do
                End If
                xls.SetValue(ReturnCol, ReturnRow, iVOs.Item(0).ShiyouBusyo)
            Loop
            ''使用目的
            Do
                If Not xls.Find("_SHIYOU_MOKUTEKI", ReturnCol, ReturnRow) Then
                    Exit Do
                End If
                xls.SetValue(ReturnCol, ReturnRow, iVOs.Item(0).ShiyouMokuteki)
            Loop
            ''主要確認項目
            Do
                If Not xls.Find("_SYUYOUKAKUNIN_KOUMOKU", ReturnCol, ReturnRow) Then
                    Exit Do
                End If
                xls.SetValue(ReturnCol, ReturnRow, iVOs.Item(0).SyuyoukakuninKoumoku)
            Loop
            ''ﾒﾓ欄
            Do
                If Not xls.Find("_MEMO", ReturnCol, ReturnRow) Then
                    Exit Do
                End If
                xls.SetValue(ReturnCol, ReturnRow, iVOs.Item(0).Memo)
            Loop
            ''↑↑2014/09/09 Ⅰ.8.号車別仕様書 作成機能_q) 酒井 ADD END
        End If
    End Sub
#End Region

#Region "Sheet2から、各データが上書"
    Private Sub SetSheet2Data(ByVal xls As ShisakuExcel, ByVal EventCode As String, ByVal KaiteiNo As String, ByVal Gousya As String, ByVal isWB As Boolean)
        Dim iDao As New XVLView.Dao.Impl.SeisakuNeedNameDaoImpl
        ''ﾗｲﾝOP名を取得
        Dim iVOs As List(Of XVLView.Dao.Vo.SeisakuOPVo) = iDao.GetIchiranopkoumokuData(EventCode, KaiteiNo, Gousya, isWB)

        ''特別織込み項目名を取得
        '        Dim miVOs As List(Of XVLView.Dao.Vo.SeisakuoOikomiVo) = iDao.GetTokubetuorikomiData(EventCode, Gousya)
        Dim miVOs As List(Of XVLView.Dao.Vo.SeisakuoOikomiVo) = iDao.GetTokubetuorikomiData(EventCode, KaiteiNo, Gousya, isWB)
        ''各ﾗｲﾝOP名を存在の場合
        If Not mOPExistlist Is Nothing Then
            For index = 0 To mOPExistlist.Count - 1
                Dim strOp As String() = mOPExistlist.Item(index).ToString.Split(",")
                Dim strOpName As String = xls.GetValue(Integer.Parse(strOp(0)), Integer.Parse(strOp(1)))
                For indexCol = 0 To iVOs.Count - 1
                    If String.Equals("_" & iVOs.Item(indexCol).OpName, strOpName) Then
                        If StringUtil.IsNotEmpty(iVOs.Item(indexCol).Tekiyou) Then
                            'xls.SetValue(strOp(0), strOp(1), "○")
                            xls.SetValue(strOp(0), strOp(1), iVOs.Item(indexCol).Tekiyou)
                            Exit For
                        End If
                    End If
                Next
            Next
        End If

        ''各特別織込み項目名を存在の場合
        If Not mShoExistlist Is Nothing Then
            For mindex = 0 To mShoExistlist.Count - 1
                Dim strSho As String() = mShoExistlist.Item(mindex).ToString.Split(",")
                Dim strShoName As String = xls.GetValue(Integer.Parse(strSho(0)), Integer.Parse(strSho(1)))
                For index = 0 To miVOs.Count - 1
                    If String.Equals("_" & miVOs.Item(index).ShoKbnName, strShoName) Then
                        If StringUtil.IsNotEmpty(miVOs.Item(index).Tekiyou) Then
                            'xls.SetValue(strSho(0), strSho(1), "○")
                            xls.SetValue(strSho(0), strSho(1), miVOs.Item(index).Tekiyou)
                            Exit For
                        End If
                    End If
                Next
            Next
        End If

        ''各ﾗｲﾝOP名を存在しないの場合
        If Not mOPExistlist Is Nothing Then
            For col = 0 To mOPExistlist.Count - 1
                Dim strOp As String() = mOPExistlist.Item(col).ToString.Split(",")
                Dim strName As String = xls.GetValue(Integer.Parse(strOp(0)), Integer.Parse(strOp(1)))
                If Not String.Equals(strName, "○") Then
                    'xls.SetValue(strOp(0), strOp(1), "")
                End If
                If strName.Substring(0, 1) = "_" AndAlso Not _KeyWord.Contains(strName) Then
                    xls.SetValue(strOp(0), strOp(1), "")
                End If
            Next
        End If

        ''各特別織込み項目名を存在しないの場合
        If Not mShoExistlist Is Nothing Then
            For mcol = 0 To mShoExistlist.Count - 1
                Dim strSho As String() = mShoExistlist.Item(mcol).ToString.Split(",")
                Dim strName As String = xls.GetValue(Integer.Parse(strSho(0)), Integer.Parse(strSho(1)))
                If Not String.Equals(strName, "○") Then
                    'xls.SetValue(strSho(0), strSho(1), "")
                End If
                If strName.Substring(0, 1) = "_" AndAlso Not _KeyWord.Contains(strName) Then
                    xls.SetValue(strSho(0), strSho(1), "")
                End If
            Next
        End If
    End Sub
#End Region
End Class

