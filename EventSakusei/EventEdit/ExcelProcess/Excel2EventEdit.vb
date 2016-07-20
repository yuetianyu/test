Imports FarPoint.Win.Spread
Imports FarPoint.Win
Imports ShisakuCommon
Imports EBom.Excel
Imports EventSakusei.EventEdit
Imports EventSakusei.EventEdit.Logic
Imports ShisakuCommon.Ui
Imports EventSakusei.EventEdit.Dao
Imports ShisakuCommon.Util.LabelValue
Imports ShisakuCommon.Db.EBom.Vo

Namespace EventEdit.ImportFromExcel
    ''' <summary>
    ''' Excelファイルから入力
    ''' </summary>
    ''' <remarks></remarks>
    Public Class Excel2EventEdit

#Region "Construct"
        ''' <summary>
        ''' Construct
        ''' </summary>
        ''' <remarks></remarks>
        Public Sub New()

        End Sub

        ''' <summary>
        ''' Construct
        ''' </summary>
        ''' <param name="subject">EventEdit Subject</param>
        ''' <remarks></remarks>
        Public Sub New(ByVal subject As Logic.EventEdit)
            Me.Subject = subject
            Me.BaseCarSubject = subject.BaseCarSubject
            Me.BaseTenkaiCarSubject = subject.BaseTenkaiCarSubject
            Me.CompleteCarSubject = subject.CompleteCarSubject
            Me.BasicOptionSubject = subject.BasicOptionSubject
            Me.SpecialOptionSubject = subject.SpecialOptionSubject
            Me.BasicOptionStartColumn = 58
            ColumnTagRenban()

        End Sub
#End Region

#Region "起動"
        ''' <summary>
        ''' 起動
        ''' </summary>
        ''' <remarks></remarks>
        Public Function Execute() As Boolean
            Dim systemDrive As String = System.Environment.GetFolderPath(Environment.SpecialFolder.Personal)
            Dim resFlag As Boolean = False

            Using ofd As New OpenFileDialog()
                ofd.Filter = "Excel files(*.xls)|*.xls|XML files(*.xml)|*.xml"
                If ofd.ShowDialog() = DialogResult.OK Then
                    '画面を綺麗に、実行中のカーソルへ変更。
                    'Application.DoEvents()
                    Cursor.Current = Cursors.WaitCursor

                    If ofd.FilterIndex = 1 Then
                        Dim excelBaseCarList As List(Of Vo.EventEditBaseCarVo)
                        Dim excelBaseTenkaiCarList As List(Of Vo.EventEditBaseTenkaiCarVo)
                        Dim excelCompleteCarList As List(Of Vo.EventEditCompleteCarVo)
                        Dim excelBasicOptionTitleList As List(Of Vo.EventEditOptionTitleVo)
                        Dim excelBasicOptionList As List(Of Vo.EventEditOptionVo)
                        Dim excelSpecialOptionTitleList As List(Of Vo.EventEditOptionTitleVo)
                        Dim excelSpecialOptionList As List(Of Vo.EventEditOptionVo)
                        Using xls As New ShisakuExcel(ofd.FileName)
                            getRowsCount(xls)
                            excelBaseCarList = getBaseCar(xls)
                            excelBaseTenkaiCarList = getBaseTenkaiCar(xls)
                            excelCompleteCarList = getCompleteCar(xls)
                            Dim basicOptionColumnCount = xls.GetMergedCellsColumnCount(BasicOptionStartColumn, 5)
                            excelBasicOptionTitleList = getBasicOptionTitle(xls, basicOptionColumnCount)
                            excelBasicOptionList = getBasicOption(xls, basicOptionColumnCount)
                            Dim specialOptionColumnCount = xls.GetMergedCellsColumnCount(SpecialOptionStartColumn, 5)
                            excelSpecialOptionTitleList = getSpecialOptionTitle(xls, specialOptionColumnCount)
                            excelSpecialOptionList = getSpecialOption(xls, specialOptionColumnCount)
                        End Using
                        setBaseCar(excelBaseCarList)
                        setBaseTenkaiCar(excelBaseTenkaiCarList)
                        setCompleteCar(excelCompleteCarList)
                        setBasicOption(excelBasicOptionTitleList, excelBasicOptionList)
                        setSpecialOption(excelSpecialOptionTitleList, excelSpecialOptionList)
                    End If
                    resFlag = True
                Else : resFlag = False
                End If
            End Using
            Return resFlag
        End Function

#End Region


#Region "ベース車部分を取り込み"
        ''' <summary>
        ''' ベース車部分を取り込み
        ''' </summary>
        ''' <param name="xls">目的Excelファイル</param>
        ''' <returns>EventEditBaseCarVoのList</returns>
        ''' <remarks></remarks>
        Public Function getBaseCar(ByVal xls As ShisakuExcel) As List(Of Vo.EventEditBaseCarVo)
            Dim baseCarList As New List(Of Vo.EventEditBaseCarVo)
            Dim baseCarDao As EventEditBaseCarDao
            baseCarDao = New EventEditBaseCarDaoImpl
            Dim i As Integer
            For i = 0 To RowsCount - 1
                Dim baseCarVo As New Vo.EventEditBaseCarVo
                '2012/03/07 文字数制限を付加'
                With baseCarVo
                    '種別は１文字'
                    .ShisakuSyubetu = Left(xls.GetValue(TagShisakuSyubetu, dataStartRow + i), 1)
                    '号車は８文字'
                    .ShisakuGousya = Left(xls.GetValue(TagShisakuGousya, dataStartRow + i), 8)
                    '開発符号は４文字'
                    .BaseKaihatsuFugo = Left(xls.GetValue(TagBaseKaihatsuFugo, dataStartRow + i), 4)
                    '仕様情報は４文字'
                    .BaseShiyoujyouhouNo = Left(xls.GetValue(TagBaseShiyoujyouhouNo, dataStartRow + i), 4)

                    '車種は２０文字'
                    .SeisakuSyasyu = Left(xls.GetValue(TagBaseSeisakuSyasyu, dataStartRow + i), 20)
                    'グレードは２０文字'
                    .SeisakuGrade = Left(xls.GetValue(TagBaseSeisakuGrade, dataStartRow + i), 20)
                    '仕向地・仕向けは６文字'
                    .SeisakuShimuke = Left(xls.GetValue(TagBaseSeisakuShimuke, dataStartRow + i), 6)
                    '仕向地・ハンドルは１文字'
                    .SeisakuHandoru = Left(xls.GetValue(TagBaseSeisakuHandoru, dataStartRow + i), 1)
                    'E/G仕様・排気量は４文字'
                    .SeisakuEgHaikiryou = Left(xls.GetValue(TagBaseSeisakuEgHaikiryou, dataStartRow + i), 4)
                    'E/G仕様・型式は４文字'
                    .SeisakuEgKatashiki = Left(xls.GetValue(TagBaseSeisakuEgKatashiki, dataStartRow + i), 4)
                    'E/G仕様・過給器は４文字'
                    .SeisakuEgKakyuuki = Left(xls.GetValue(TagBaseSeisakuEgKakyuuki, dataStartRow + i), 4)
                    'T/M仕様・駆動方式は４文字'
                    .SeisakuTmKudou = Left(xls.GetValue(TagBaseSeisakuTmKudou, dataStartRow + i), 4)
                    'T/M仕様・変速機は８文字'
                    .SeisakuTmHensokuki = Left(xls.GetValue(TagBaseSeisakuTmHensokuki, dataStartRow + i), 8)

                    'アプライNoと型式を取得
                    Dim rhac0230 As List(Of Rhac0230Vo) = baseCarDao.FindRhac0230By(xls.GetValue(TagBaseKaihatsuFugo, dataStartRow + i), xls.GetValue(TagBaseShiyoujyouhouNo, dataStartRow + i))
                    'アプライドNoは３文字'
                    .BaseAppliedNo = Left(xls.GetValue(TagBaseAppliedNo, dataStartRow + i), 3)
                    '型式は７文字'
                    .BaseKatashiki = Left(xls.GetValue(TagBaseKatashiki, dataStartRow + i), 7)

                    ''2012/02/22 ここでアプライNoが無かった場合、型式から取得して無理やり突っ込む
                    If .BaseAppliedNo Is Nothing Then
                        If Not .BaseKatashiki Is Nothing Then
                            For Each Vo As Rhac0230Vo In rhac0230
                                If .BaseKatashiki = Vo.KatashikiFugo7 Then
                                    .BaseAppliedNo = Vo.AppliedNo
                                    Exit For
                                End If
                            Next
                        End If
                    End If
                    ''2012/02/22 ここで型式が無かった場合、アプライNoから取得して無理やり突っ込む
                    If .BaseKatashiki Is Nothing Then
                        If Not .BaseAppliedNo Is Nothing Then
                            For Each Vo As Rhac0230Vo In rhac0230
                                If .BaseAppliedNo = Vo.AppliedNo Then
                                    .BaseKatashiki = Vo.KatashikiFugo7
                                    Exit For
                                End If
                            Next
                        End If
                    End If

                    '2012/02/24 国内または空白になっているものは「国内」と表示させるようにする
                    If StringUtil.IsEmpty(xls.GetValue(TagBaseShimuke, dataStartRow + i)) Then
                        .BaseShimuke = ""
                    ElseIf xls.GetValue(TagBaseShimuke, dataStartRow + i) = "国内" Then
                        .BaseShimuke = ""
                    Else
                        '仕向けは２文字'
                        .BaseShimuke = Left(xls.GetValue(TagBaseShimuke, dataStartRow + i), 2)
                    End If
                    'OPは３文字'
                    .BaseOp = Left(xls.GetValue(TagBaseOp, dataStartRow + i), 3)
                    '外装色コードは３文字'
                    .BaseGaisousyoku = Left(xls.GetValue(TagBaseGaisousyoku, dataStartRow + i), 3)
                    '内装色コードは３文字'
                    .BaseNaisousyoku = Left(xls.GetValue(TagBaseNaisousyoku, dataStartRow + i), 3)
                    'ベースとなるイベントは１２文字'
                    .ShisakuBaseEventCode = Left(xls.GetValue(TagShisakuBaseEventCode, dataStartRow + i), 12)
                    'ベースとなる号車は８文字'
                    .ShisakuBaseGousya = Left(xls.GetValue(TagShisakuBaseGousya, dataStartRow + i), 8)
                End With
                baseCarList.Add(baseCarVo)
            Next
            Return baseCarList
        End Function

#End Region

#Region "設計展開ベース車部分を取り込み"
        ''' <summary>
        ''' 設計展開ベース車部分を取り込み
        ''' </summary>
        ''' <param name="xls">目的Excelファイル</param>
        ''' <returns>EventEditReferenceCarVoのList</returns>
        ''' <remarks></remarks>
        Public Function getBaseTenkaiCar(ByVal xls As ShisakuExcel) As List(Of Vo.EventEditBaseTenkaiCarVo)
            Dim baseCarTenkaiList As New List(Of Vo.EventEditBaseTenkaiCarVo)
            Dim baseCarDao As EventEditBaseCarDao
            baseCarDao = New EventEditBaseCarDaoImpl
            Dim i As Integer
            For i = 0 To RowsCount - 1
                Dim baseTenkaiCarVo As New Vo.EventEditBaseTenkaiCarVo
                '2012/03/07 文字数制限を付加'
                With baseTenkaiCarVo
                    '種別は１文字'
                    .ShisakuSyubetu = Left(xls.GetValue(TagShisakuSyubetu, dataStartRow + i), 1)
                    '号車は８文字'
                    .ShisakuGousya = Left(xls.GetValue(TagShisakuGousya, dataStartRow + i), 8)
                    '開発符号は４文字'
                    .BaseKaihatsuFugo = Left(xls.GetValue(TagBaseKaihatsuFugo, dataStartRow + i), 4)
                    '仕様情報は４文字'
                    .BaseShiyoujyouhouNo = Left(xls.GetValue(TagBaseShiyoujyouhouNo, dataStartRow + i), 4)

                    'アプライNoと型式を取得
                    Dim rhac0230 As List(Of Rhac0230Vo) = baseCarDao.FindRhac0230By(xls.GetValue(TagBaseKaihatsuFugo, dataStartRow + i), xls.GetValue(TagBaseShiyoujyouhouNo, dataStartRow + i))
                    'アプライドNoは３文字'
                    .BaseAppliedNo = Left(xls.GetValue(TagBaseAppliedNo, dataStartRow + i), 3)
                    '型式は７文字'
                    .BaseKatashiki = Left(xls.GetValue(TagBaseKatashiki, dataStartRow + i), 7)

                    ''2012/02/22 ここでアプライNoが無かった場合、型式から取得して無理やり突っ込む
                    If .BaseAppliedNo Is Nothing Then
                        If Not .BaseKatashiki Is Nothing Then
                            For Each Vo As Rhac0230Vo In rhac0230
                                If .BaseKatashiki = Vo.KatashikiFugo7 Then
                                    .BaseAppliedNo = Vo.AppliedNo
                                    Exit For
                                End If
                            Next
                        End If
                    End If
                    ''2012/02/22 ここで型式が無かった場合、アプライNoから取得して無理やり突っ込む
                    If .BaseKatashiki Is Nothing Then
                        If Not .BaseAppliedNo Is Nothing Then
                            For Each Vo As Rhac0230Vo In rhac0230
                                If .BaseAppliedNo = Vo.AppliedNo Then
                                    .BaseKatashiki = Vo.KatashikiFugo7
                                    Exit For
                                End If
                            Next
                        End If
                    End If

                    '2012/02/24 国内または空白になっているものは「国内」と表示させるようにする
                    If StringUtil.IsEmpty(xls.GetValue(TagBaseShimuke, dataStartRow + i)) Then
                        .BaseShimuke = ""
                    ElseIf xls.GetValue(TagBaseShimuke, dataStartRow + i) = "国内" Then
                        .BaseShimuke = ""
                    Else
                        '仕向けは２文字'
                        .BaseShimuke = Left(xls.GetValue(TagBaseShimuke, dataStartRow + i), 2)
                    End If
                    'OPは３文字'
                    .BaseOp = Left(xls.GetValue(TagBaseOp, dataStartRow + i), 3)
                    '外装色コードは３文字'
                    .BaseGaisousyoku = Left(xls.GetValue(TagBaseGaisousyoku, dataStartRow + i), 3)
                    '内装色コードは３文字'
                    .BaseNaisousyoku = Left(xls.GetValue(TagBaseNaisousyoku, dataStartRow + i), 3)
                    'ベースとなるイベントは１２文字'
                    .ShisakuBaseEventCode = Left(xls.GetValue(TagShisakuBaseEventCode, dataStartRow + i), 12)
                    'ベースとなる号車は８文字'
                    .ShisakuBaseGousya = Left(xls.GetValue(TagShisakuBaseGousya, dataStartRow + i), 8)
                End With
                baseCarTenkaiList.Add(baseTenkaiCarVo)
            Next
            Return baseCarTenkaiList
        End Function
#End Region

#Region "完成車部分を取り込み"
        ''' <summary>
        ''' 完成車部分を取り込み
        ''' </summary>
        ''' <param name="xls">目的Excelファイル</param>
        ''' <returns>EventEditCompleteCarVoのList</returns>
        ''' <remarks></remarks>
        Public Function getCompleteCar(ByVal xls As ShisakuExcel) As List(Of Vo.EventEditCompleteCarVo)
            Dim completeCarList As New List(Of Vo.EventEditCompleteCarVo)
            Dim i As Integer
            For i = 0 To RowsCount - 1
                Dim completeCarVo As New Vo.EventEditCompleteCarVo
                '2012/03/07 文字数制限付加'
                With completeCarVo
                    '車型は２０文字'
                    .ShisakuSyagata = Left(xls.GetValue(TagCompleteCarShisakuSyagata, dataStartRow + i), 20)
                    'グレードは２０文字'
                    .ShisakuGrade = Left(xls.GetValue(TagCompleteCarShisakuGrade, dataStartRow + i), 20)
                    '仕向地・仕向けは６文字'
                    .ShisakuShimukechiShimuke = Left(xls.GetValue(TagCompleteCarShisakuShimukechiShimuke, dataStartRow + i), 6)
                    'ハンドルは３文字'
                    .ShisakuHandoru = Left(xls.GetValue(TagCompleteCarShisakuHandoru, dataStartRow + i), 3)
                    'E/G・型式は３文字'
                    .ShisakuEgKatashiki = Left(xls.GetValue(TagCompleteCarShisakuEgKatashiki, dataStartRow + i), 3)
                    'E/G・排気量は４文字'
                    .ShisakuEgHaikiryou = Left(xls.GetValue(TagCompleteCarShisakuEgHaikiryou, dataStartRow + i), 4)
                    'E/G・システムは４文字'
                    .ShisakuEgSystem = Left(xls.GetValue(TagCompleteCarShisakuEgSystem, dataStartRow + i), 4)
                    'E/G・過給機は４文字'
                    .ShisakuEgKakyuuki = Left(xls.GetValue(TagCompleteCarShisakuEgKakyuuki, dataStartRow + i), 4)
                    'E/G・メモ１は５０文字'
                    .ShisakuEgMemo1 = Left(xls.GetValue(TagCompleteCarShisakuEgMemo1, dataStartRow + i), 50)
                    'E/G・メモ２は５０文字'
                    .ShisakuEgMemo2 = Left(xls.GetValue(TagCompleteCarShisakuEgMemo2, dataStartRow + i), 50)
                    'T/M・駆動は３文字'
                    .ShisakuTmKudou = Left(xls.GetValue(TagCompleteCarShisakuTmKudou, dataStartRow + i), 3)
                    'T/M・変速機は４文字'
                    .ShisakuTmHensokuki = Left(xls.GetValue(TagCompleteCarShisakuTmHensokuki, dataStartRow + i), 4)
                    'T/M・副変速機は３文字'
                    .ShisakuTmFukuHensokuki = Left(xls.GetValue(TagCompleteCarShisakuTmFukuHensokuki, dataStartRow + i), 3)
                    'T/M・メモ１は５０文字'
                    .ShisakuTmMemo1 = Left(xls.GetValue(TagCompleteCarShisakuTmMemo1, dataStartRow + i), 50)
                    'T/M・メモ２は５０文字'
                    .ShisakuTmMemo2 = Left(xls.GetValue(TagCompleteCarShisakuTmMemo2, dataStartRow + i), 50)
                    '型式は７文字'
                    .ShisakuKatashiki = Left(xls.GetValue(TagCompleteCarShisakuKatashiki, dataStartRow + i), 7)
                    '仕向けは４文字'
                    .ShisakuShimuke = Left(xls.GetValue(TagCompleteCarShisakuShimuke, dataStartRow + i), 4)
                    'OPは４文字'
                    .ShisakuOp = Left(xls.GetValue(TagCompleteCarShisakuOp, dataStartRow + i), 4)
                    '外装色は３文字'
                    .ShisakuGaisousyoku = Left(xls.GetValue(TagCompleteCarShisakuGaisousyoku, dataStartRow + i), 3)
                    '外装色名は５０文字'
                    .ShisakuGaisousyokuName = Left(xls.GetValue(TagCompleteCarShisakuGaisousyokuName, dataStartRow + i), 50)
                    '内装色は３文字'
                    .ShisakuNaisousyoku = Left(xls.GetValue(TagCompleteCarShisakuNaisousyoku, dataStartRow + i), 3)
                    '内装色名は５０文字'
                    .ShisakuNaisousyokuName = Left(xls.GetValue(TagCompleteCarShisakuNaisousyokuName, dataStartRow + i), 50)
                    '車台No.は２０文字'
                    .ShisakuSyadaiNo = Left(xls.GetValue(TagCompleteCarShisakuSyadaiNo, dataStartRow + i), 20)
                    '使用目的は１２文字'
                    .ShisakuShiyouMokuteki = Left(xls.GetValue(TagCompleteCarShisakuShiyouMokuteki, dataStartRow + i), 12)
                    '試験目的は２５６文字'
                    .ShisakuShikenMokuteki = Left(xls.GetValue(TagCompleteCarShisakuShikenMokuteki, dataStartRow + i), 256)
                    '使用部署は１０文字'
                    .ShisakuSiyouBusyo = Left(xls.GetValue(TagCompleteCarShisakuSiyouBusyo, dataStartRow + i), 10)
                    'グループは３文字'
                    .ShisakuGroup = Left(xls.GetValue(TagCompleteCarShisakuGroup, dataStartRow + i), 3)
                    '製作順序は３文字'
                    .ShisakuSeisakuJunjyo = Left(xls.GetValue(TagCompleteCarShisakuSeisakuJunjyo, dataStartRow + i), 3)
                    Dim kaseibi As String = xls.GetValue(TagCompleteCarShisakuKanseibi, dataStartRow + i)
                    '完成日
                    'ブランクの時は処理対象外にしないと。レングスチェックで落ちてしまうよ。　By柳沼
                    If Not StringUtil.IsEmpty(kaseibi) Then
                        'yyyymmdd形式でもyyyy/mm/dd形式でも読み込めるようにする'
                        If kaseibi.Length = 10 Then
                            Try
                                Dim shisakuKanseibi = DateUtil.ConvDateValueToDateTime(xls.GetValue(TagCompleteCarShisakuKanseibi, dataStartRow + i))
                                .ShisakuKanseibi = DateUtil.ConvDateToIneteger(shisakuKanseibi)
                            Catch ex As Exception
                                .ShisakuKanseibi = Nothing
                            End Try
                        ElseIf kaseibi.Length = 8 Then
                            Dim kanseiDate As String = Mid(kaseibi, 1, 4) + "/" + Mid(kaseibi, 5, 2) + "/" + Mid(kaseibi, 7, 2)
                            Try
                                .ShisakuKanseibi = DateUtil.ConvDateToIneteger(kanseiDate)
                            Catch ex As Exception
                                .ShisakuKanseibi = Nothing
                            End Try
                        End If
                    End If
                    '工指Noは読み込まない'
                    .ShisakuKoushiNo = ""
                    '製作方法区分は２文字'
                    .ShisakuSeisakuHouhouKbn = Left(xls.GetValue(TagCompleteCarShisakuSeisakuHouhouKbn, dataStartRow + i), 2)
                    '製作方法は１００文字'
                    .ShisakuSeisakuHouhou = Left(xls.GetValue(TagCompleteCarShisakuSeisakuHouhou, dataStartRow + i), 100)
                    'メモ欄は２５６文字'
                    .ShisakuMemo = Left(xls.GetValue(TagCompleteCarShisakuMemo, dataStartRow + i), 256)

                End With
                completeCarList.Add(completeCarVo)
            Next
            Return completeCarList
        End Function
#End Region

#Region "基本装備部分タイトルを取り込み"
        ''' <summary>
        ''' 基本装備部分タイトルを取り込み
        ''' </summary>
        ''' <param name="xls">目的Excelファイル</param>
        ''' <param name="columnCount">基本装備の項目列数</param>
        ''' <returns>EventEditOptionTitleVoのList</returns>
        ''' <remarks></remarks>
        Public Function getBasicOptionTitle(ByVal xls As ShisakuExcel, ByVal columnCount As Integer) As List(Of Vo.EventEditOptionTitleVo)
            Dim basicOptionTitleList As New List(Of Vo.EventEditOptionTitleVo)
            Dim i As Integer
            For i = 0 To columnCount - 1
                Dim basicOptionTitle As New Vo.EventEditOptionTitleVo
                With basicOptionTitle
                    .TitleColumnNo = BasicOptionStartColumn + i
                    .TitleNameDai = Left(xls.GetValue(BasicOptionStartColumn + i, 6), 61)
                    .TitleNameChu = Left(xls.GetValue(BasicOptionStartColumn + i, 7), 61)
                    .TitleName = Left(xls.GetValue(BasicOptionStartColumn + i, 8), 61)
                End With
                basicOptionTitleList.Add(basicOptionTitle)
            Next
            Return basicOptionTitleList
        End Function

#End Region

#Region "基本装備部分を取り込み"
        ''' <summary>
        ''' 基本装備部分データを取り込み
        ''' </summary>
        ''' <param name="xls">目的Excelファイル</param>
        ''' <param name="columnCount">基本装備の項目列数</param>
        ''' <returns>EventEditOptionVoのList</returns>
        ''' <remarks></remarks>
        Public Function getBasicOption(ByVal xls As ShisakuExcel, ByVal columnCount As Integer) As List(Of Vo.EventEditOptionVo)
            Dim basicOptionList As New List(Of Vo.EventEditOptionVo)
            Dim i As Integer
            Dim j As Integer
            For i = 0 To RowsCount - 1
                For j = 0 To columnCount - 1
                    Dim basicOption As New Vo.EventEditOptionVo
                    With basicOption
                        .ColumnNo = BasicOptionStartColumn + j
                        .RowNo = dataStartRow + i
                        .ShisakuTekiyou = Left(xls.GetValue(BasicOptionStartColumn + j, dataStartRow + i), 1)
                    End With
                    basicOptionList.Add(basicOption)
                Next
            Next
            Me.SpecialOptionStartColumn += BasicOptionStartColumn + columnCount
            Return basicOptionList
        End Function

#End Region

#Region "特別装備部分タイトルを取り込み"
        ''' <summary>
        ''' 特別装備部分タイトルを取り込み
        ''' </summary>
        ''' <param name="xls">目的Excelファイル</param>
        ''' <param name="columnCount">特別装備の項目列数</param>
        ''' <returns>EventEditOptionTitleVoのList</returns>
        ''' <remarks></remarks>
        Public Function getSpecialOptionTitle(ByVal xls As ShisakuExcel, ByVal columnCount As Integer) As List(Of Vo.EventEditOptionTitleVo)
            Dim specialOptionTitleList As New List(Of Vo.EventEditOptionTitleVo)
            Dim i As Integer
            For i = 0 To columnCount - 1
                Dim specialOptionTitle As New Vo.EventEditOptionTitleVo
                With specialOptionTitle
                    .TitleColumnNo = SpecialOptionStartColumn + i
                    .TitleNameDai = Left(xls.GetValue(SpecialOptionStartColumn + i, 6), 61)
                    .TitleNameChu = Left(xls.GetValue(SpecialOptionStartColumn + i, 7), 61)
                    .TitleName = Left(xls.GetValue(SpecialOptionStartColumn + i, 8), 61)
                End With
                specialOptionTitleList.Add(specialOptionTitle)
            Next
            Return specialOptionTitleList
        End Function

#End Region

#Region "特別装備部分を取り込み"
        ''' <summary>
        ''' 特別装備部分データを取り込み
        ''' </summary>
        ''' <param name="xls">目的Excelファイル</param>
        ''' <param name="columnCount">特別装備の項目列数</param>
        ''' <returns>EventEditOptionVoのList</returns>
        ''' <remarks></remarks>
        Public Function getSpecialOption(ByVal xls As ShisakuExcel, ByVal columnCount As Integer) As List(Of Vo.EventEditOptionVo)
            Dim specialOptionList As New List(Of Vo.EventEditOptionVo)
            Dim i As Integer
            Dim j As Integer
            For i = 0 To RowsCount - 1
                For j = 0 To columnCount - 1
                    Dim specialOption As New Vo.EventEditOptionVo
                    With specialOption
                        .ColumnNo = SpecialOptionStartColumn + j
                        .RowNo = dataStartRow + i
                        .ShisakuTekiyou = Left(xls.GetValue(SpecialOptionStartColumn + j, dataStartRow + i), 1)
                    End With
                    specialOptionList.Add(specialOption)
                Next
            Next
            Return specialOptionList
        End Function

#End Region

#Region "文字列をバイト数でチェックして抜き出す"

        ''' <summary>
        ''' 文字列をバイト数でチェックして抜き出す
        ''' </summary>
        ''' <param name="str">文字列</param>
        ''' <param name="start">開始位置</param>
        ''' <param name="length">終了位置(バイト数)</param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Private Function MidB(ByVal str As String, ByVal start As Integer, Optional ByVal length As Integer = 0) As String
            If StringUtil.IsEmpty(str) Then
                Return ""
            End If
            Dim RestLength As Integer = System.Text.Encoding.GetEncoding("Shift_JIS").GetByteCount(str) - start + 1
            If length = 0 OrElse length > RestLength Then
                length = RestLength
            End If
            Dim SJIS As System.Text.Encoding = System.Text.Encoding.GetEncoding("Shift-JIS")
            Dim B() As Byte = CType(Array.CreateInstance(GetType(Byte), length), Byte())

            Array.Copy(SJIS.GetBytes(str), start - 1, B, 0, length)

            Dim st1 As String = SJIS.GetString(B)

            '▼切り抜いた結果、最後の１バイトが全角文字の半分だった場合、その半分は切り捨てる。

            Dim ResultLength As Integer = System.Text.Encoding.GetEncoding("Shift_JIS").GetByteCount(st1) - start + 1

            If Asc(Strings.Right(st1, 1)) = 0 Then
                'VB.NET2002,2003の場合、最後の１バイトが全角の半分の時
                Return st1.Substring(0, st1.Length - 1)
            ElseIf length = ResultLength - 1 Then
                'VB2005の場合で最後の１バイトが全角の半分の時
                Return st1.Substring(0, st1.Length - 1)
            Else
                'その他の場合
                Return st1
            End If

        End Function

#End Region


#Region "Observerにベース車部分をSet"
        ''' <summary>
        ''' ベース車をOberverに更新
        ''' </summary>
        ''' <param name="excelBaseCarList">Excelファイルから取り込みベース車の部分</param>
        ''' <remarks></remarks>
        Public Sub setBaseCar(ByVal excelBaseCarList As List(Of Vo.EventEditBaseCarVo))

            For Each Row As Integer In BaseCarSubject.GetInputRowNos()
                With BaseCarSubject
                    .ShisakuSyubetu(Row) = Nothing
                    .ShisakuGousya(Row) = Nothing
                    .SekkeiTenkaiKbn(Row) = Nothing
                    .BaseKaihatsuFugo(Row) = Nothing
                    .BaseShiyoujyouhouNo(Row) = Nothing
                    '参考情報
                    .SeisakuSyasyu(Row) = Nothing
                    .SeisakuGrade(Row) = Nothing
                    .SeisakuShimuke(Row) = Nothing
                    .SeisakuHandoru(Row) = Nothing
                    .SeisakuEgHaikiryou(Row) = Nothing
                    .SeisakuEgKatashiki(Row) = Nothing
                    .SeisakuEgKakyuuki(Row) = Nothing
                    .SeisakuTmKudou(Row) = Nothing
                    .SeisakuTmHensokuki(Row) = Nothing

                    .BaseAppliedNo(Row) = Nothing
                    .BaseKatashiki(Row) = Nothing
                    .BaseShimuke(Row) = Nothing
                    .BaseOp(Row) = Nothing
                    .BaseGaisousyoku(Row) = Nothing
                    .BaseNaisousyoku(Row) = Nothing
                    .ShisakuBaseEventCode(Row) = Nothing
                    .ShisakuBaseGousya(Row) = Nothing
                    '製作一覧・車体№
                    .SeisakuSyataiNo(Row) = Nothing
                End With
            Next

            Dim i As Integer
            For i = 0 To excelBaseCarList.Count - 1
                Dim baseCarVo = excelBaseCarList.Item(i)
                With BaseCarSubject
                    .ShisakuSyubetu(i) = baseCarVo.ShisakuSyubetu
                    .ShisakuGousya(i) = baseCarVo.ShisakuGousya
                    .SekkeiTenkaiKbn(i) = baseCarVo.SekkeiTenkaiKbn
                    .BaseKaihatsuFugo(i) = baseCarVo.BaseKaihatsuFugo
                    .BaseShiyoujyouhouNo(i) = baseCarVo.BaseShiyoujyouhouNo
                    '参考情報
                    .SeisakuSyasyu(i) = baseCarVo.SeisakuSyasyu
                    .SeisakuGrade(i) = baseCarVo.SeisakuGrade
                    .SeisakuShimuke(i) = baseCarVo.SeisakuShimuke
                    .SeisakuHandoru(i) = baseCarVo.SeisakuHandoru
                    .SeisakuEgHaikiryou(i) = baseCarVo.SeisakuEgHaikiryou
                    .SeisakuEgKatashiki(i) = baseCarVo.SeisakuEgKatashiki
                    .SeisakuEgKakyuuki(i) = baseCarVo.SeisakuEgKakyuuki
                    .SeisakuTmKudou(i) = baseCarVo.SeisakuTmKudou
                    .SeisakuTmHensokuki(i) = baseCarVo.SeisakuTmHensokuki

                    .BaseAppliedNo(i) = baseCarVo.BaseAppliedNo
                    .BaseKatashiki(i) = baseCarVo.BaseKatashiki
                    .BaseShimuke(i) = baseCarVo.BaseShimuke
                    .BaseOp(i) = baseCarVo.BaseOp
                    .BaseGaisousyoku(i) = baseCarVo.BaseGaisousyoku
                    .BaseNaisousyoku(i) = baseCarVo.BaseNaisousyoku
                    .ShisakuBaseEventCode(i) = baseCarVo.ShisakuBaseEventCode
                    .ShisakuBaseGousya(i) = baseCarVo.ShisakuBaseGousya
                    '製作一覧・車体№
                    .SeisakuSyataiNo(i) = baseCarVo.SeisakuSyataiNo
                End With
            Next
        End Sub
#End Region

#Region "Observerに設計展開ベース車部分をSet"
        ''' <summary>
        ''' 設計展開ベース車をOberverに更新
        ''' </summary>
        ''' <param name="excelBaseTenkaiCarList">Excelファイルから取り込み設計展開ベース車の部分</param>
        ''' <remarks></remarks>
        Public Sub setBaseTenkaiCar(ByVal excelBaseTenkaiCarList As List(Of Vo.EventEditBaseTenkaiCarVo))

            For Each Row As Integer In BaseTenkaiCarSubject.GetInputRowNos()
                With BaseTenkaiCarSubject
                    .ShisakuSyubetu(Row) = Nothing
                    .ShisakuGousya(Row) = Nothing
                    .BaseKaihatsuFugo(Row) = Nothing
                    .BaseShiyoujyouhouNo(Row) = Nothing
                    .BaseAppliedNo(Row) = Nothing
                    .BaseKatashiki(Row) = Nothing
                    .BaseShimuke(Row) = Nothing
                    .BaseOp(Row) = Nothing
                    .BaseGaisousyoku(Row) = Nothing
                    .BaseNaisousyoku(Row) = Nothing
                    .ShisakuBaseEventCode(Row) = Nothing
                    .ShisakuBaseGousya(Row) = Nothing
                End With
            Next

            Dim i As Integer
            For i = 0 To excelBaseTenkaiCarList.Count - 1
                Dim BaseTenkaiCarVo = excelBaseTenkaiCarList.Item(i)
                With BaseTenkaiCarSubject
                    .ShisakuSyubetu(i) = BaseTenkaiCarVo.ShisakuSyubetu
                    .ShisakuGousya(i) = BaseTenkaiCarVo.ShisakuGousya
                    .BaseKaihatsuFugo(i) = BaseTenkaiCarVo.BaseKaihatsuFugo
                    .BaseShiyoujyouhouNo(i) = BaseTenkaiCarVo.BaseShiyoujyouhouNo

                    .BaseAppliedNo(i) = BaseTenkaiCarVo.BaseAppliedNo
                    .BaseKatashiki(i) = BaseTenkaiCarVo.BaseKatashiki
                    .BaseShimuke(i) = BaseTenkaiCarVo.BaseShimuke
                    .BaseOp(i) = BaseTenkaiCarVo.BaseOp
                    .BaseGaisousyoku(i) = BaseTenkaiCarVo.BaseGaisousyoku
                    .BaseNaisousyoku(i) = BaseTenkaiCarVo.BaseNaisousyoku
                    .ShisakuBaseEventCode(i) = BaseTenkaiCarVo.ShisakuBaseEventCode
                    .ShisakuBaseGousya(i) = BaseTenkaiCarVo.ShisakuBaseGousya
                End With
            Next
        End Sub
#End Region

#Region "Observerに完成車部分をSet"
        ''' <summary>
        ''' 完成車をOberverに更新
        ''' </summary>
        ''' <param name="excelCompleteCarList">Excelファイルから取り込み完成車の部分</param>
        ''' <remarks></remarks>
        Public Sub setCompleteCar(ByVal excelCompleteCarList As List(Of Vo.EventEditCompleteCarVo))

            For Each Row As Integer In CompleteCarSubject.GetInputRowNos()
                With CompleteCarSubject
                    .ShisakuSyagata(Row) = Nothing
                    .ShisakuGrade(Row) = Nothing
                    .ShisakuShimukechiShimuke(Row) = Nothing
                    .ShisakuHandoru(Row) = Nothing

                    .ShisakuEgKatashiki(Row) = Nothing
                    .ShisakuEgHaikiryou(Row) = Nothing
                    .ShisakuEgSystem(Row) = Nothing
                    .ShisakuEgKakyuuki(Row) = Nothing
                    .ShisakuEgMemo1(Row) = Nothing
                    .ShisakuEgMemo2(Row) = Nothing

                    .ShisakuTmKudou(Row) = Nothing
                    .ShisakuTmHensokuki(Row) = Nothing
                    .ShisakuTmFukuHensokuki(Row) = Nothing
                    .ShisakuTmMemo1(Row) = Nothing
                    .ShisakuTmMemo2(Row) = Nothing

                    .ShisakuKatashiki(Row) = Nothing
                    .ShisakuShimuke(Row) = Nothing
                    .ShisakuOp(Row) = Nothing
                    .ShisakuGaisousyoku(Row) = Nothing
                    .ShisakuGaisousyokuName(Row) = Nothing
                    .ShisakuNaisousyoku(Row) = Nothing
                    .ShisakuNaisousyokuName(Row) = Nothing
                    .ShisakuSyadaiNo(Row) = Nothing
                    .ShisakuShiyouMokuteki(Row) = Nothing
                    .ShisakuShikenMokuteki(Row) = Nothing
                    .ShisakuSiyouBusyo(Row) = Nothing
                    .ShisakuGroup(Row) = Nothing
                    .ShisakuSeisakuJunjyo(Row) = Nothing
                    .ShisakuKanseibi(Row) = Nothing
                    .ShisakuKoushiNo(Row) = Nothing
                    .ShisakuSeisakuHouhouKbn(Row) = Nothing
                    .ShisakuSeisakuHouhou(Row) = Nothing
                    .ShisakuMemo(Row) = Nothing
                End With
            Next

            Dim i As Integer
            For i = 0 To excelCompleteCarList.Count - 1
                Dim completeCarVo = excelCompleteCarList.Item(i)
                With CompleteCarSubject
                    .ShisakuSyagata(i) = completeCarVo.ShisakuSyagata
                    .ShisakuGrade(i) = completeCarVo.ShisakuGrade
                    .ShisakuShimukechiShimuke(i) = completeCarVo.ShisakuShimukechiShimuke
                    .ShisakuHandoru(i) = completeCarVo.ShisakuHandoru

                    .ShisakuEgKatashiki(i) = completeCarVo.ShisakuEgKatashiki
                    .ShisakuEgHaikiryou(i) = completeCarVo.ShisakuEgHaikiryou
                    .ShisakuEgSystem(i) = completeCarVo.ShisakuEgSystem
                    .ShisakuEgKakyuuki(i) = completeCarVo.ShisakuEgKakyuuki
                    .ShisakuEgMemo1(i) = completeCarVo.ShisakuEgMemo1
                    .ShisakuEgMemo2(i) = completeCarVo.ShisakuEgMemo2

                    .ShisakuTmKudou(i) = completeCarVo.ShisakuTmKudou
                    .ShisakuTmHensokuki(i) = completeCarVo.ShisakuTmHensokuki
                    .ShisakuTmFukuHensokuki(i) = completeCarVo.ShisakuTmFukuHensokuki
                    .ShisakuTmMemo1(i) = completeCarVo.ShisakuTmMemo1
                    .ShisakuTmMemo2(i) = completeCarVo.ShisakuTmMemo2

                    .ShisakuKatashiki(i) = completeCarVo.ShisakuKatashiki
                    .ShisakuShimuke(i) = completeCarVo.ShisakuShimuke
                    .ShisakuOp(i) = completeCarVo.ShisakuOp
                    .ShisakuGaisousyoku(i) = completeCarVo.ShisakuGaisousyoku
                    .ShisakuGaisousyokuName(i) = completeCarVo.ShisakuGaisousyokuName
                    .ShisakuNaisousyoku(i) = completeCarVo.ShisakuNaisousyoku
                    .ShisakuNaisousyokuName(i) = completeCarVo.ShisakuNaisousyokuName
                    .ShisakuSyadaiNo(i) = completeCarVo.ShisakuSyadaiNo
                    .ShisakuShiyouMokuteki(i) = completeCarVo.ShisakuShiyouMokuteki
                    .ShisakuShikenMokuteki(i) = completeCarVo.ShisakuShikenMokuteki
                    .ShisakuSiyouBusyo(i) = completeCarVo.ShisakuSiyouBusyo
                    .ShisakuGroup(i) = completeCarVo.ShisakuGroup
                    .ShisakuSeisakuJunjyo(i) = completeCarVo.ShisakuSeisakuJunjyo
                    .ShisakuKanseibi(i) = completeCarVo.ShisakuKanseibi
                    .ShisakuKoushiNo(i) = completeCarVo.ShisakuKoushiNo
                    .ShisakuSeisakuHouhouKbn(i) = completeCarVo.ShisakuSeisakuHouhouKbn
                    .ShisakuSeisakuHouhou(i) = completeCarVo.ShisakuSeisakuHouhou
                    .ShisakuMemo(i) = completeCarVo.ShisakuMemo
                End With
            Next
        End Sub
#End Region

#Region "Observerに基本装備部分をSet"
        ''' <summary>
        ''' 基本装備をOberverに更新
        ''' </summary>
        ''' <param name="excelBasicOptionTitleList">Excelファイルから取り込み基本装備のタイトル部分</param>
        ''' <param name="excelBasicOptionList">Excelファイルから取り込み基本装備のデータ部分</param>
        ''' <remarks></remarks>
        Public Sub setBasicOption(ByVal excelBasicOptionTitleList As List(Of Vo.EventEditOptionTitleVo), _
                                  ByVal excelBasicOptionList As List(Of Vo.EventEditOptionVo))
            setBasicOptionTitle(excelBasicOptionTitleList)
            setBasicOptionBody(excelBasicOptionList)
        End Sub
        ''' <summary>
        ''' 基本装備をOberverに更新(タイトル)
        ''' </summary>
        ''' <param name="excelBasicOptionTitleList">Excelファイルから取り込み基本装備のタイトル部分</param>
        ''' <remarks></remarks>
        Public Sub setBasicOptionTitle(ByVal excelBasicOptionTitleList As List(Of Vo.EventEditOptionTitleVo))
            Dim i As Integer
            For i = 0 To excelBasicOptionTitleList.Count - 1
                Dim basicOptionTitle = excelBasicOptionTitleList.Item(i)
                With BasicOptionSubject
                    .TitleNameDai(basicOptionTitle.TitleColumnNo - BasicOptionStartColumn) = basicOptionTitle.TitleNameDai
                    .TitleNameChu(basicOptionTitle.TitleColumnNo - BasicOptionStartColumn) = basicOptionTitle.TitleNameChu
                    .TitleName(basicOptionTitle.TitleColumnNo - BasicOptionStartColumn) = basicOptionTitle.TitleName
                End With
            Next
        End Sub
        ''' <summary>
        ''' 基本装備をOberverに更新（データ）
        ''' </summary>
        ''' <param name="excelBasicOptionList">Excelファイルから取り込み基本装備のデータ部分</param>
        ''' <remarks></remarks>
        Public Sub setBasicOptionBody(ByVal excelBasicOptionList As List(Of Vo.EventEditOptionVo))
            Dim i As Integer
            For i = 0 To excelBasicOptionList.Count - 1
                Dim basicOptionBody = excelBasicOptionList.Item(i)
                With BasicOptionSubject
                    .ShisakuTekiyou(basicOptionBody.RowNo - dataStartRow, _
                                    basicOptionBody.ColumnNo - BasicOptionStartColumn) = basicOptionBody.ShisakuTekiyou
                End With
            Next
        End Sub
#End Region

#Region "Observerに特別装備部分をSet"
        ''' <summary>
        ''' 特別装備をOberverに更新
        ''' </summary>
        ''' <param name="excelSpecialOptionTitleList">Excelファイルから取り込み特別装備のタイトル部分</param>
        ''' <param name="excelSpecialOptionList">Excelファイルから取り込み特別装備のデータ部分</param>
        ''' <remarks></remarks>
        Public Sub setSpecialOption(ByVal excelSpecialOptionTitleList As List(Of Vo.EventEditOptionTitleVo), _
                                  ByVal excelSpecialOptionList As List(Of Vo.EventEditOptionVo))
            setSpecialOptionTitle(excelSpecialOptionTitleList)
            setSpecialOptionBody(excelSpecialOptionList)
        End Sub
        ''' <summary>
        ''' 特別装備をOberverに更新（タイトル）
        ''' </summary>
        ''' <param name="excelSpecialOptionTitleList">Excelファイルから取り込み特別装備のタイトル部分</param>
        ''' <remarks></remarks>
        Public Sub setSpecialOptionTitle(ByVal excelSpecialOptionTitleList As List(Of Vo.EventEditOptionTitleVo))
            Dim i As Integer
            For i = 0 To excelSpecialOptionTitleList.Count - 1
                Dim specialOptionTitle = excelSpecialOptionTitleList.Item(i)
                With SpecialOptionSubject
                    .TitleNameDai(specialOptionTitle.TitleColumnNo - SpecialOptionStartColumn) = specialOptionTitle.TitleNameDai
                    .TitleNameChu(specialOptionTitle.TitleColumnNo - SpecialOptionStartColumn) = specialOptionTitle.TitleNameChu
                    .TitleName(specialOptionTitle.TitleColumnNo - SpecialOptionStartColumn) = specialOptionTitle.TitleName
                End With
            Next
        End Sub
        ''' <summary>
        ''' 特別装備をOberverに更新（データ）
        ''' </summary>
        ''' <param name="excelSpecialOptionList">Excelファイルから取り込み特別装備のデータ部分</param>
        ''' <remarks></remarks>
        Public Sub setSpecialOptionBody(ByVal excelSpecialOptionList As List(Of Vo.EventEditOptionVo))
            Dim i As Integer
            For i = 0 To excelSpecialOptionList.Count - 1
                Dim specialOptionBody = excelSpecialOptionList.Item(i)
                With SpecialOptionSubject
                    .ShisakuTekiyou(specialOptionBody.RowNo - dataStartRow, _
                                    specialOptionBody.ColumnNo - SpecialOptionStartColumn) = specialOptionBody.ShisakuTekiyou
                End With
            Next
        End Sub
#End Region

#Region "Excelファイルにデータ行数の判断"
        ''' <summary>
        ''' Excelファイルにデータ行数の判断
        ''' </summary>
        ''' <param name="xls">目的Excel</param>
        ''' <remarks></remarks>
        Public Sub getRowsCount(ByVal xls As ShisakuExcel)
            Dim i As Integer = 0
            Try
                While (Not (xls.GetValue(1, 12 + i)).Equals(String.Empty)) AndAlso (Not xls.GetValue(1, 12 + i) Is Nothing)
                    i += 1
                End While
            Catch ex As System.NullReferenceException

            Finally
                Me.RowsCount = i
            End Try
        End Sub
#End Region

        ''' <summary>
        ''' 列のタグ設定
        ''' </summary>
        ''' <remarks></remarks>
        Private Sub ColumnTagRenban()
            Dim column As Integer = 1
            TagRenban = EzUtil.Increment(column)
            TagShisakuSyubetu = EzUtil.Increment(column)
            TagShisakuGousya = EzUtil.Increment(column)
            TagBaseKaihatsuFugo = EzUtil.Increment(column)
            TagBaseShiyoujyouhouNo = EzUtil.Increment(column)

            '参考情報
            TagBaseSeisakuSyasyu = EzUtil.Increment(column)
            TagBaseSeisakuGrade = EzUtil.Increment(column)
            TagBaseSeisakuShimuke = EzUtil.Increment(column)
            TagBaseSeisakuHandoru = EzUtil.Increment(column)
            TagBaseSeisakuEgHaikiryou = EzUtil.Increment(column)
            TagBaseSeisakuEgKatashiki = EzUtil.Increment(column)
            TagBaseSeisakuEgKakyuuki = EzUtil.Increment(column)
            TagBaseSeisakuTmKudou = EzUtil.Increment(column)
            TagBaseSeisakuTmHensokuki = EzUtil.Increment(column)
            TagBaseAppliedNo = EzUtil.Increment(column)
            TagBaseKatashiki = EzUtil.Increment(column)
            TagBaseShimuke = EzUtil.Increment(column)
            TagBaseOp = EzUtil.Increment(column)
            TagBaseGaisousyoku = EzUtil.Increment(column)
            TagBaseGaisousyokuName = EzUtil.Increment(column)
            TagBaseNaisousyoku = EzUtil.Increment(column)
            TagBaseNaisousyokuName = EzUtil.Increment(column)
            TagShisakuBaseEventCode = EzUtil.Increment(column)
            TagShisakuBaseGousya = EzUtil.Increment(column)
            TagSeisakuSyataiNo = EzUtil.Increment(column)

            TagBaseTenkaiKaihatsuFugo = EzUtil.Increment(column)
            TagBaseTenkaiShiyoujyouhouNo = EzUtil.Increment(column)
            TagBaseTenkaiAppliedNo = EzUtil.Increment(column)
            TagBaseTenkaiKatashiki = EzUtil.Increment(column)
            TagBaseTenkaiShimuke = EzUtil.Increment(column)
            TagBaseTenkaiOp = EzUtil.Increment(column)
            TagBaseTenkaiGaisousyoku = EzUtil.Increment(column)
            TagBaseTenkaiGaisousyokuName = EzUtil.Increment(column)
            TagBaseTenkaiNaisousyoku = EzUtil.Increment(column)
            TagBaseTenkaiNaisousyokuName = EzUtil.Increment(column)
            TagTenkaiShisakuBaseEventCode = EzUtil.Increment(column)
            TagTenkaiShisakuBaseGousya = EzUtil.Increment(column)

            TagCompleteCarShisakuSyagata = EzUtil.Increment(column)
            TagCompleteCarShisakuGrade = EzUtil.Increment(column)
            TagCompleteCarShisakuShimukechiShimuke = EzUtil.Increment(column)
            TagCompleteCarShisakuHandoru = EzUtil.Increment(column)
            TagCompleteCarShisakuEgKatashiki = EzUtil.Increment(column)
            TagCompleteCarShisakuEgHaikiryou = EzUtil.Increment(column)
            TagCompleteCarShisakuEgSystem = EzUtil.Increment(column)
            TagCompleteCarShisakuEgKakyuuki = EzUtil.Increment(column)
            TagCompleteCarShisakuEgMemo1 = EzUtil.Increment(column)
            TagCompleteCarShisakuEgMemo2 = EzUtil.Increment(column)
            TagCompleteCarShisakuTmKudou = EzUtil.Increment(column)
            TagCompleteCarShisakuTmHensokuki = EzUtil.Increment(column)
            TagCompleteCarShisakuTmFukuHensokuki = EzUtil.Increment(column)
            TagCompleteCarShisakuTmMemo1 = EzUtil.Increment(column)
            TagCompleteCarShisakuTmMemo2 = EzUtil.Increment(column)
            TagCompleteCarShisakuKatashiki = EzUtil.Increment(column)
            TagCompleteCarShisakuShimuke = EzUtil.Increment(column)
            TagCompleteCarShisakuOp = EzUtil.Increment(column)
            TagCompleteCarShisakuGaisousyoku = EzUtil.Increment(column)
            TagCompleteCarShisakuGaisousyokuName = EzUtil.Increment(column)
            TagCompleteCarShisakuNaisousyoku = EzUtil.Increment(column)
            TagCompleteCarShisakuNaisousyokuName = EzUtil.Increment(column)
            TagCompleteCarShisakuSyadaiNo = EzUtil.Increment(column)
            TagCompleteCarShisakuShiyouMokuteki = EzUtil.Increment(column)
            TagCompleteCarShisakuShikenMokuteki = EzUtil.Increment(column)
            TagCompleteCarShisakuSiyouBusyo = EzUtil.Increment(column)
            TagCompleteCarShisakuGroup = EzUtil.Increment(column)
            TagCompleteCarShisakuSeisakuJunjyo = EzUtil.Increment(column)
            TagCompleteCarShisakuKanseibi = EzUtil.Increment(column)
            TagCompleteCarShisakuKoushiNo = EzUtil.Increment(column)
            TagCompleteCarShisakuSeisakuHouhouKbn = EzUtil.Increment(column)
            TagCompleteCarShisakuSeisakuHouhou = EzUtil.Increment(column)
            TagCompleteCarShisakuMemo = EzUtil.Increment(column)

            Me.BasicOptionStartColumn = EzUtil.Increment(column)

        End Sub


#Region "Tag"

        ''' <summary>連番</summary>
        Private TagRenban As Integer

        ''' <summary>種別</summary>
        Private TagShisakuSyubetu As Integer
        ''' <summary>号車</summary>
        Private TagShisakuGousya As Integer
#Region "ベース車Column"
        ''' <summary>開発符号</summary>
        Private TagBaseKaihatsuFugo As Integer
        ''' <summary>仕様情報NO</summary>
        Private TagBaseShiyoujyouhouNo As Integer

        ''' <summary>製作一覧_車型</summary>
        Private TagBaseSeisakuSyasyu As Integer
        ''' <summary>製作一覧_グレード</summary>
        Private TagBaseSeisakuGrade As Integer
        ''' <summary>製作一覧_仕向地・仕向け</summary>
        Private TagBaseSeisakuShimuke As Integer
        ''' <summary>製作一覧_仕向地・ハンドル</summary>
        Private TagBaseSeisakuHandoru As Integer
        ''' <summary>製作一覧_E/G仕様・排気量</summary>
        Private TagBaseSeisakuEgHaikiryou As Integer
        ''' <summary>製作一覧_E/G仕様・型式</summary>
        Private TagBaseSeisakuEgKatashiki As Integer
        ''' <summary>製作一覧_E/G仕様・過給器</summary>
        Private TagBaseSeisakuEgKakyuuki As Integer
        ''' <summary>製作一覧_T/M仕様・駆動方式</summary>
        Private TagBaseSeisakuTmKudou As Integer
        ''' <summary>製作一覧_T/M仕様・変速機</summary>
        Private TagBaseSeisakuTmHensokuki As Integer

        ''' <summary>アブライドNO</summary>
        Private TagBaseAppliedNo As Integer
        ''' <summary>型式</summary>
        Private TagBaseKatashiki As Integer
        ''' <summary>仕向</summary>
        Private TagBaseShimuke As Integer
        ''' <summary>OP</summary>
        Private TagBaseOp As Integer
        ''' <summary>外装色</summary>
        Private TagBaseGaisousyoku As Integer
        ''' <summary>外装色名</summary>
        Private TagBaseGaisousyokuName As Integer
        ''' <summary>内装色</summary>
        Private TagBaseNaisousyoku As Integer
        ''' <summary>外装色名</summary>
        Private TagBaseNaisousyokuName As Integer
        ''' <summary>イベントコード</summary>
        Private TagShisakuBaseEventCode As Integer
        ''' <summary>号車</summary>
        Private TagShisakuBaseGousya As Integer

        ''' <summary>製作一覧・車体№</summary>
        Private TagSeisakuSyataiNo As Integer
#End Region

#Region "設計展開ベース車Column"
        ''' <summary>開発符号</summary>
        Private TagBaseTenkaiKaihatsuFugo As Integer
        ''' <summary>仕様情報NO</summary>
        Private TagBaseTenkaiShiyoujyouhouNo As Integer
        ''' <summary>アブライドNO</summary>
        Private TagBaseTenkaiAppliedNo As Integer
        ''' <summary>型式</summary>
        Private TagBaseTenkaiKatashiki As Integer
        ''' <summary>仕向</summary>
        Private TagBaseTenkaiShimuke As Integer
        ''' <summary>OP</summary>
        Private TagBaseTenkaiOp As Integer
        ''' <summary>外装色</summary>
        Private TagBaseTenkaiGaisousyoku As Integer
        ''' <summary>外装色名</summary>
        Private TagBaseTenkaiGaisousyokuName As Integer
        ''' <summary>内装色</summary>
        Private TagBaseTenkaiNaisousyoku As Integer
        ''' <summary>内装色名</summary>
        Private TagBaseTenkaiNaisousyokuName As Integer
        ''' <summary>イベントコード</summary>
        Private TagTenkaiShisakuBaseEventCode As Integer
        ''' <summary>号車</summary>
        Private TagTenkaiShisakuBaseGousya As Integer
#End Region

#Region "完成車"
        ''' <summary>車型</summary>
        Private TagCompleteCarShisakuSyagata As Integer
        ''' <summary>グレード</summary>
        Private TagCompleteCarShisakuGrade As Integer
        ''' <summary>仕向地・仕向け</summary>
        Private TagCompleteCarShisakuShimukechiShimuke As Integer
        ''' <summary>ハンドル</summary>
        Private TagCompleteCarShisakuHandoru As Integer
        ''' <summary>E/G型式</summary>
        Private TagCompleteCarShisakuEgKatashiki As Integer
        ''' <summary>排気量</summary>
        Private TagCompleteCarShisakuEgHaikiryou As Integer
        ''' <summary>システム</summary>
        Private TagCompleteCarShisakuEgSystem As Integer
        ''' <summary>過給機</summary>
        Private TagCompleteCarShisakuEgKakyuuki As Integer
        ''' <summary>試作E/Gメモ１</summary>
        Private TagCompleteCarShisakuEgMemo1 As Integer
        ''' <summary>試作E/Gメモ２</summary>
        Private TagCompleteCarShisakuEgMemo2 As Integer
        ''' <summary>駆動</summary>
        Private TagCompleteCarShisakuTmKudou As Integer
        ''' <summary>変速機</summary>
        Private TagCompleteCarShisakuTmHensokuki As Integer
        ''' <summary>副変速</summary>
        Private TagCompleteCarShisakuTmFukuHensokuki As Integer
        ''' <summary>試作T/Mメモ１</summary>
        Private TagCompleteCarShisakuTmMemo1 As Integer
        ''' <summary>試作T/Mメモ２</summary>
        Private TagCompleteCarShisakuTmMemo2 As Integer
        ''' <summary>型式</summary>
        Private TagCompleteCarShisakuKatashiki As Integer
        ''' <summary>仕向</summary>
        Private TagCompleteCarShisakuShimuke As Integer
        ''' <summary>ＯＰ</summary>
        Private TagCompleteCarShisakuOp As Integer
        ''' <summary>外装色</summary>
        Private TagCompleteCarShisakuGaisousyoku As Integer
        ''' <summary>外装色名</summary>
        Private TagCompleteCarShisakuGaisousyokuName As Integer
        ''' <summary>内装色</summary>
        Private TagCompleteCarShisakuNaisousyoku As Integer
        ''' <summary>内装色名</summary>
        Private TagCompleteCarShisakuNaisousyokuName As Integer
        ''' <summary>車台No.</summary>
        Private TagCompleteCarShisakuSyadaiNo As Integer
        ''' <summary>使用目的</summary>
        Private TagCompleteCarShisakuShiyouMokuteki As Integer
        ''' <summary>試作試験目的（主要確認項目）</summary>
        Private TagCompleteCarShisakuShikenMokuteki As Integer
        ''' <summary>使用部署</summary>
        Private TagCompleteCarShisakuSiyouBusyo As Integer
        ''' <summary>グループ</summary>
        Private TagCompleteCarShisakuGroup As Integer
        ''' <summary>製作・製作順序</summary>
        Private TagCompleteCarShisakuSeisakuJunjyo As Integer
        ''' <summary>完成日</summary>
        Private TagCompleteCarShisakuKanseibi As Integer
        ''' <summary>工指No.</summary>
        Private TagCompleteCarShisakuKoushiNo As Integer
        ''' <summary>製作方法区分</summary>
        Private TagCompleteCarShisakuSeisakuHouhouKbn As Integer
        ''' <summary>製作方法</summary>
        Private TagCompleteCarShisakuSeisakuHouhou As Integer
        ''' <summary>メモ欄</summary>
        Private TagCompleteCarShisakuMemo As Integer
#End Region

#End Region


#Region "Local Property"
        ''' <summary>
        ''' EventEditSubject
        ''' </summary>
        ''' <remarks></remarks>
        Private _Subject As Logic.EventEdit
        ''' <summary>
        ''' ベース車subject
        ''' </summary>
        ''' <remarks></remarks>
        Private _BaseCarSubject As EventEditBaseCar
        ''' <summary>
        ''' 設計展開ベース車subject
        ''' </summary>
        ''' <remarks></remarks>
        Private _BaseTenkaiCarSubject As EventEditBaseTenkaiCar
        ''' <summary>
        ''' 完了車subject
        ''' </summary>
        ''' <remarks></remarks>
        Private _CompleteCarSubject As EventEditCompleteCar
        ''' <summary>
        ''' 基本装備仕様subject
        ''' </summary>
        ''' <remarks></remarks>
        Private _BasicOptionSubject As EventEditOption
        ''' <summary>
        ''' 特別装備仕様subject
        ''' </summary>
        ''' <remarks></remarks>
        Private _SpecialOptionSubject As EventEditOption
        ''' <summary>
        ''' 出力時「基本装備仕様」部分1番目列の列数
        ''' </summary>
        ''' <remarks></remarks>
        Private _BasicOptionStartColumn As Integer
        ''' <summary>
        ''' 出力時「特別装備仕様」部分1番目列の列数
        ''' </summary>
        ''' <remarks></remarks>
        Private _SpecialOptionStartColumn As Integer
        ''' <summary>
        ''' データ部分の一番目の行位置
        ''' </summary>
        ''' <remarks></remarks>
        ''' １０→１２へ（基本及び特別装備仕様に大、中を追加により2行追加）
        Private dataStartRow = 12
        ''' <summary>
        ''' データ部分の行数（データレコド数）
        ''' </summary>
        ''' <remarks></remarks>
        Private _RowsCount As Integer
#End Region

#Region "ローカルメンバーgetとset"

        ''' <summary>
        ''' EventEditSubject
        ''' </summary>
        ''' <value>EventEditSubject</value>
        ''' <returns>EventEditSubject</returns>
        ''' <remarks></remarks>
        Public Property Subject() As Logic.EventEdit
            Get
                Return _Subject
            End Get
            Set(ByVal value As Logic.EventEdit)
                _Subject = value
            End Set
        End Property

        ''' <summary>
        ''' ベース車subject
        ''' </summary>
        ''' <value>ベース車subject</value>
        ''' <returns>ベース車subject</returns>
        ''' <remarks></remarks>
        Public Property BaseCarSubject() As EventEditBaseCar
            Get
                Return _BaseCarSubject
            End Get
            Set(ByVal value As EventEditBaseCar)
                _BaseCarSubject = value
            End Set
        End Property

        ''' <summary>
        ''' 設計展開ベース車subject
        ''' </summary>
        ''' <value>設計展開ベース車subject</value>
        ''' <returns>設計展開ベース車subject</returns>
        ''' <remarks></remarks>
        Public Property BaseTenkaiCarSubject() As EventEditBaseTenkaiCar
            Get
                Return _BaseTenkaiCarSubject
            End Get
            Set(ByVal value As EventEditBaseTenkaiCar)
                _BaseTenkaiCarSubject = value
            End Set
        End Property

        ''' <summary>
        ''' 完了車subject
        ''' </summary>
        ''' <value>完了車subject</value>
        ''' <returns>完了車subject</returns>
        ''' <remarks></remarks>
        Public Property CompleteCarSubject() As EventEditCompleteCar
            Get
                Return _CompleteCarSubject
            End Get
            Set(ByVal value As EventEditCompleteCar)
                _CompleteCarSubject = value
            End Set
        End Property

        ''' <summary>
        ''' 基本装備仕様subject
        ''' </summary>
        ''' <value>基本装備仕様subject</value>
        ''' <returns>基本装備仕様subject</returns>
        ''' <remarks></remarks>
        Public Property BasicOptionSubject() As EventEditOption
            Get
                Return _BasicOptionSubject
            End Get
            Set(ByVal value As EventEditOption)
                _BasicOptionSubject = value
            End Set
        End Property

        ''' <summary>
        ''' 特別装備仕様subject
        ''' </summary>
        ''' <value>特別装備仕様subject</value>
        ''' <returns>特別装備仕様subject</returns>
        ''' <remarks></remarks>
        Public Property SpecialOptionSubject() As EventEditOption
            Get
                Return _SpecialOptionSubject
            End Get
            Set(ByVal value As EventEditOption)
                _SpecialOptionSubject = value
            End Set
        End Property

        ''' <summary>
        ''' 出力時「基本装備仕様」部分1番目列の列数
        ''' </summary>
        ''' <value>出力時「基本装備仕様」部分1番目列の列数</value>
        ''' <returns>出力時「基本装備仕様」部分1番目列の列数</returns>
        ''' <remarks></remarks>
        Public Property BasicOptionStartColumn() As Integer
            Get
                Return _BasicOptionStartColumn
            End Get
            Set(ByVal value As Integer)
                _BasicOptionStartColumn = value
            End Set
        End Property

        ''' <summary>
        ''' 出力時「特別装備仕様」部分1番目列の列数
        ''' </summary>
        ''' <value>出力時「特別装備仕様」部分1番目列の列数</value>
        ''' <returns>出力時「特別装備仕様」部分1番目列の列数</returns>
        ''' <remarks></remarks>
        Public Property SpecialOptionStartColumn() As Integer
            Get
                Return _SpecialOptionStartColumn
            End Get
            Set(ByVal value As Integer)
                _SpecialOptionStartColumn = value
            End Set
        End Property

        ''' <summary>
        ''' データ部分の行数（データレコド数）
        ''' </summary>
        ''' <value>データ部分の行数（データレコド数）</value>
        ''' <returns>データ部分の行数（データレコド数）</returns>
        ''' <remarks></remarks>
        Public Property RowsCount() As Integer
            Get
                Return _RowsCount
            End Get
            Set(ByVal value As Integer)
                _RowsCount = value
            End Set
        End Property
#End Region

    End Class

End Namespace