Imports ShisakuCommon.Ui
Imports ShisakuCommon.Db.EBom.Vo
Imports ShisakuCommon
Imports EBom.Excel
Imports ShisakuCommon.Db.EBom.Dao
Imports ShisakuCommon.Db.EBom.Dao.Impl
Imports ShisakuCommon.Db.EBom.Vo.Helper
Imports EventSakusei.EventEdit.Dao
Imports EventSakusei.EventEdit.Vo

Namespace EventEdit.ExcelOutput

    ''' <summary>
    ''' イベント情報差分EXCEL出力
    ''' </summary>
    ''' <remarks></remarks>
    Public Class ExportShisakuEventInfoExcel

#Region "メンバ変数"

        ''' <summary>EventEdit Subject</summary>
        Private Subject As Logic.EventEdit
        ''' <summary>ベース車情報</summary>
        Private baseList As New List(Of TShisakuEventBaseSeisakuIchiranVo)
        ''' <summary>完成車情報</summary>
        Private kanseiList As New List(Of TShisakuEventKanseiVo)
        ''' <summary>基本装備仕様情報</summary>
        Private basicList As New List(Of EventSoubiVo)
        ''' <summary>特別装備仕様情報</summary>
        Private specialList As New List(Of EventSoubiVo)
        ''' <summary>基本装備仕様情報（タイトル）</summary>
        Private basicTitelList As New List(Of EventSoubiVo)
        ''' <summary>特別装備仕様情報（タイトル）</summary>
        Private specialTitelList As New List(Of EventSoubiVo)
        ''' <summary>設計展開時のベース車情報</summary>
        Private baseTenkaiList As New List(Of TShisakuEventBaseVo)
        '差異比較用
        ''' <summary>完成車情報比較用</summary>
        Private HkanseiList As New List(Of TShisakuEventKanseiKaiteiVo)
        ''' <summary>ベース車情報比較用</summary>
        Private HbaseList As New List(Of TShisakuEventBaseKaiteiVo)
        ''' <summary>基本装備仕様情報比較用</summary>
        Private HbasicList As New List(Of EventSoubiVo)
        ''' <summary>特別装備仕様情報比較用</summary>
        Private HspecialList As New List(Of EventSoubiVo)
        ''' <summary>基本装備仕様情報比較用（タイトル）</summary>
        Private HbasicTitelList As New List(Of EventSoubiVo)
        ''' <summary>特別装備仕様情報比較用（タイトル）</summary>
        Private HspecialTitelList As New List(Of EventSoubiVo)

        ''' <summary>比較イベントコード</summary>
        Private HikakuEventCode As String

        ''' <summary>EXCEL出力スタートROW</summary>
        Private lngStartRow As Long = 11

        ''' <summary>EXCEL出力装備仕様スタートCOL</summary>
        Private lngStartColumn As Long = 5

        ''' <summary>メモヘッダー情報</summary>
        Private strEgMemo1 As String
        Private strEgMemo2 As String
        Private strTmMemo1 As String
        Private strTmMemo2 As String

#End Region

        ''' <summary>
        ''' コンストラクタ
        ''' </summary>
        ''' <param name="subject">EventEdit Subject</param>
        ''' <param name="kanseiList">完成車情報</param>
        ''' <param name="baseList">ベース車情報</param>
        ''' <param name="basicList">基本装備仕様情報</param>
        ''' <param name="specialList">特別装備仕様情報</param>
        ''' <param name="HkanseiList">完成車情報比較用</param>
        ''' <param name="HbaseList">ベース車情報比較用</param>
        ''' <param name="HbasicList">基本装備仕様情報比較用</param>
        ''' <param name="HSpecialList">特別装備仕様情報比較用</param>
        ''' <param name="HikakuEventCode">比較イベントコード</param>
        ''' <param name="baseTenkaiList">設計展開ベース車情報</param>
        ''' <remarks></remarks>
        Public Sub New(ByVal subject As Logic.EventEdit, _
                        ByVal kanseiList As List(Of TShisakuEventKanseiVo), _
                        ByVal baseList As List(Of TShisakuEventBaseSeisakuIchiranVo), _
                        ByVal basicList As List(Of EventSoubiVo), _
                        ByVal specialList As List(Of EventSoubiVo), _
                        ByVal basicTitelList As List(Of EventSoubiVo), _
                        ByVal specialTitleList As List(Of EventSoubiVo), _
                        ByVal HkanseiList As List(Of TShisakuEventKanseiKaiteiVo), _
                        ByVal HbaseList As List(Of TShisakuEventBaseKaiteiVo), _
                        ByVal HbasicList As List(Of EventSoubiVo), _
                        ByVal HspecialList As List(Of EventSoubiVo), _
                        ByVal HbasicTitelList As List(Of EventSoubiVo), _
                        ByVal HspecialTitleList As List(Of EventSoubiVo), _
                        ByVal HikakuEventCode As String, _
                        ByVal baseTenkaiList As List(Of TShisakuEventBaseVo), _
                        ByVal strEgMemo1 As String, ByVal strEgMemo2 As String, _
                        ByVal strTmMemo1 As String, ByVal strTmMemo2 As String)

            Me.Subject = subject

            Me.kanseiList = kanseiList
            Me.baseList = baseList
            Me.basicList = basicList
            Me.specialList = specialList
            Me.basicTitelList = basicTitelList
            Me.specialTitelList = specialTitleList

            Me.basetenkaiList = baseTenkaiList

            '比較用
            Me.HkanseiList = HkanseiList
            Me.HbaseList = HbaseList
            Me.HbasicList = HbasicList
            Me.HspecialList = HspecialList
            Me.HbasicTitelList = HbasicTitelList
            Me.HspecialTitelList = HspecialTitleList

            'イニシャル以降
            Me.HikakuEventCode = HikakuEventCode

            Me.strEgMemo1 = strEgMemo1
            Me.strEgMemo2 = strEgMemo2
            Me.strTmMemo1 = strTmMemo1
            Me.strTmMemo2 = strTmMemo2

        End Sub

        ''' <summary>
        ''' 試作イベント情報の差分EXCEL出力
        ''' </summary>
        ''' <param name="xls"></param>
        ''' <remarks></remarks>
        Public Sub Execute(ByVal xls As ShisakuExcel)

            '初期設定
            baseUpdateFlg = ""
            kanseiUpdateFlg = ""
            basicUpdateFlg = ""
            specialUpdateFlg = ""

            xls.AddSheet()  '１シート目を作成するとエラーになるので１シート。
            xls.AddSheet()  '２シート目を作成するとエラーになるので１シート。
            xls.AddSheet()  '３シート目を作成するとエラーになるので１シート。
            xls.AddSheet()  '４シート目を作成するとエラーになるので１シート。
            xls.AddSheet()  '５シート目を作成するとエラーになるので１シート。

            '   今回、比較先のデータ共に0件の場合は処理スルー。
            If baseTenkaiList.Count <> 0 Then
                'アクティブシート及びシート名設定
                xls.SetActiveSheet(1)
                xls.SetSheetName("部品表ベース車")
                'ヘッダー部の作成'
                setSheetHeaderPart(xls)
                'タイトル部の作成（部品表ベース車）'
                setSheetTitleBASEtenkai(xls)
                'ボディ部の作成（部品表ベース車シート）'
                setSheet1BodyPartBASEtenkai(xls)
            End If

            '   今回、比較先のデータ共に0件の場合は処理スルー。
            If baseList.Count <> 0 Or HbaseList.Count <> 0 Then
                'アクティブシート及びシート名設定
                xls.SetActiveSheet(2)
                xls.SetSheetName("製作一覧ベース車")
                'ヘッダー部の作成'
                setSheetHeaderPart(xls)
                'タイトル部の作成（製作一覧ベース車情報）'
                setSheetTitleBASE(xls)
                'ボディ部の作成（製作一覧ベース車シート）'
                baseUpdateFlg = setSheet1BodyPartBASE(xls)
            End If

            '   今回、比較先のデータ共に0件の場合は処理スルー。
            If kanseiList.Count <> 0 Or HkanseiList.Count <> 0 Then
                'アクティブシート及びシート名設定
                xls.SetActiveSheet(3)
                xls.SetSheetName("完成車情報")
                'ヘッダー部の作成'
                setSheetHeaderPart(xls)
                'タイトル部の作成（完成車情報）'
                setSheetTitleKANSEI(xls)
                'ボディ部の作成（完成車シート）'
                kanseiUpdateFlg = setSheet1BodyPartKANSEI(xls)
            End If

            '   今回、比較先のデータ共に0件の場合は処理スルー。
            If basicList.Count <> 0 Or HbasicList.Count <> 0 Then
                'アクティブシート及びシート名設定
                xls.SetActiveSheet(4)
                xls.SetSheetName("基本装備仕様")
                'ヘッダー部の作成'
                setSheetHeaderPart(xls)
                'タイトル部の作成（基本装備仕様）'
                setSheetTitleBASIC(xls)
                'ボディ部の作成（基本装備仕様シート）'
                basicUpdateFlg = setSheet1BodyPartBASIC(xls)
            End If

            '   今回、比較先のデータ共に0件の場合は処理スルー。
            If specialList.Count <> 0 Or HspecialList.Count <> 0 Then
                'アクティブシート及びシート名設定
                xls.SetActiveSheet(5)
                xls.SetSheetName("特別装備仕様")
                'ヘッダー部の作成'
                setSheetHeaderPart(xls)
                'タイトル部の作成（特別装備仕様）'
                setSheetTitleSPECIAL(xls)
                'ボディ部の作成（特別装備仕様シート）'
                specialUpdateFlg = setSheet1BodyPartSPECIAL(xls)
            End If

        End Sub

        ''' <summary>
        ''' ヘッダー情報の作成
        ''' </summary>
        ''' <param name="xls">EXCEL</param>
        ''' <remarks></remarks>
        Private Sub setSheetHeaderPart(ByVal xls As ShisakuExcel)

            'xls.SetValueのパラメータは－－－＞　startCol ,startRow ,endCol ,endRow ,value

            Dim strKaihatsuFugo As String = Nothing
            Dim strEventName As String = Nothing
            Dim strHakouNo As String = Nothing
            Dim strKaiteiNo As String = Nothing
            Dim strKaiteiNoHikaku As String = Nothing
            If StringUtil.IsNotEmpty(Subject) Then

                'xls.SetValueのパラメータは－－－＞　startCol ,startRow ,endCol ,endRow ,value

                '登録日
                xls.SetValue(1, 1, 1, 1, "登録日")
                xls.MergeCells(1, 1, 2, 1, True)    'マージ
                xls.SetValue(3, 1, 3, 1, Now().ToString("yyyy/MM/dd HH:mm:ss "))
                xls.SetAlignment(3, 1, 3, 1, XlHAlign.xlHAlignLeft, XlVAlign.xlVAlignCenter, False, False) '位置
                xls.MergeCells(3, 1, 5, 1, True)    'マージ
                'イベント
                xls.SetValue(6, 1, 6, 1, "イベント")
                xls.SetValue(7, 1, 7, 1, Subject.HeaderSubject.ShisakuEventPhaseName)
                xls.SetAlignment(7, 1, 7, 1, XlHAlign.xlHAlignLeft, XlVAlign.xlVAlignCenter, False, False) '位置
                xls.MergeCells(7, 1, 8, 1, True)    'マージ
                '開発符号
                xls.SetValue(1, 2, 1, 2, "開発符号")
                xls.MergeCells(1, 2, 2, 2, True)    'マージ
                xls.SetValue(3, 2, 3, 2, Subject.HeaderSubject.ShisakuKaihatsuFugo)
                xls.SetAlignment(3, 2, 3, 2, XlHAlign.xlHAlignLeft, XlVAlign.xlVAlignCenter, False, False) '位置
                'イベント
                xls.SetValue(6, 2, 6, 2, "イベント名称")
                xls.SetAlignment(6, 2, 6, 2, XlHAlign.xlHAlignLeft, XlVAlign.xlVAlignCenter, True, False) '位置
                xls.SetValue(7, 2, 7, 2, Subject.HeaderSubject.ShisakuEventName)
                xls.SetAlignment(7, 2, 7, 2, XlHAlign.xlHAlignLeft, XlVAlign.xlVAlignCenter, False, False) '位置
                xls.MergeCells(7, 2, 10, 2, True)    'マージ
                'M/T/S区分
                xls.SetValue(1, 3, 1, 3, "M/T/S区分")
                xls.MergeCells(1, 3, 2, 3, True)    'マージ
                xls.SetValue(3, 3, 3, 3, Subject.HeaderSubject.UnitKbn)
                xls.SetAlignment(3, 3, 3, 3, XlHAlign.xlHAlignLeft, XlVAlign.xlVAlignCenter, False, False) '位置
                '製作時期
                Dim seisakuFrom As Date = Subject.HeaderSubject.SeisakujikiFrom
                Dim seisakuTo As Date = Subject.HeaderSubject.SeisakujikiTo
                Dim seisakuDate As String = seisakuFrom.ToString("yyyy/MM/dd") + " - " + seisakuTo.ToString("yyyy/MM/dd")
                xls.SetValue(6, 3, 6, 3, "製作時期")
                xls.SetValue(7, 3, 7, 3, seisakuDate)
                xls.SetAlignment(7, 3, 7, 3, XlHAlign.xlHAlignLeft, XlVAlign.xlVAlignCenter, False, False) '位置
                xls.MergeCells(7, 3, 9, 3, True)    'マージ
                '製作台数
                xls.SetValue(1, 4, 1, 4, "製作台数")
                xls.MergeCells(1, 4, 2, 4, True)    'マージ
                xls.SetValue(3, 4, 3, 4, "完成車")
                xls.SetValue(4, 4, 4, 4, Subject.HeaderSubject.SeisakudaisuKanseisya)
                xls.SetAlignment(4, 4, 4, 4, XlHAlign.xlHAlignLeft, XlVAlign.xlVAlignCenter, False, False) '位置
                xls.SetValue(5, 4, 5, 4, "W/B")
                xls.SetValue(6, 4, 6, 4, Subject.HeaderSubject.SeisakudaisuWb)
                xls.SetAlignment(6, 4, 6, 4, XlHAlign.xlHAlignLeft, XlVAlign.xlVAlignCenter, False, False) '位置
                xls.SetValue(7, 4, 7, 4, "製作中止")
                xls.SetValue(8, 4, 8, 4, Subject.HeaderSubject.SeisakudaisuChushi)
                xls.SetAlignment(8, 4, 8, 4, XlHAlign.xlHAlignLeft, XlVAlign.xlVAlignCenter, False, False) '位置
                '現在
                xls.SetValue(1, 5, 1, 5, "現在")
                xls.MergeCells(1, 5, 2, 5, True)    'マージ
                xls.SetValue(3, 5, 3, 5, Subject.HeaderSubject.StatusName)
                xls.SetAlignment(3, 5, 3, 5, XlHAlign.xlHAlignLeft, XlVAlign.xlVAlignCenter, False, False) '位置
                xls.MergeCells(3, 5, 4, 5, True)    'マージ
                '状態
                xls.SetValue(5, 5, 5, 5, Subject.HeaderSubject.DataKbnName)
                xls.SetAlignment(5, 5, 5, 5, XlHAlign.xlHAlignLeft, XlVAlign.xlVAlignCenter, False, False) '位置
                '製作一覧発行№
                xls.SetValue(1, 6, 1, 6, "製作一覧発行№")
                xls.MergeCells(1, 6, 2, 6, True)    'マージ
                xls.SetValue(3, 6, 3, 6, Subject.HeaderSubject.SeisakuichiranHakouNo)
                xls.SetAlignment(3, 6, 3, 6, XlHAlign.xlHAlignLeft, XlVAlign.xlVAlignCenter, False, False) '位置
                xls.MergeCells(3, 6, 4, 6, True)    'マージ
                xls.SetValue(5, 6, 5, 6, Subject.HeaderSubject.SeisakuichiranHakouNoKai)
                xls.SetAlignment(5, 6, 5, 6, XlHAlign.xlHAlignLeft, XlVAlign.xlVAlignCenter, False, False) '位置
                '発注有無
                xls.SetValue(6, 6, 6, 6, "発注有無")
                xls.SetValue(7, 6, 7, 6, Subject.HeaderSubject.HachuUmu)

                '列幅の調整
                '   ほそくほそくほそくーー発行版はOP列などが変動するので（適用が無い列は非表示等）列№を最新化必要あり。または非表示にするのもありかな？
                '   完成車情報
                'xls.SetColWidth(1, 2, 5)
                'xls.SetColWidth(C_KFUGO_POS, C_GRADE_POS, 8)
                'xls.SetColWidth(C_SEIHO_POS, C_SEIHO_POS, 20)
                'xls.SetColWidth(C_SHIMUKE_POS, C_SHIMUKECODE_POS, 6.5)
                'xls.SetColWidth(C_KATASHIKI_POS, C_KATASHIKI_POS, 13.5)
                'xls.SetColWidth(C_OPCODE_POS, C_OPCODE_POS, 5)
                'xls.SetColWidth(C_GAISOCODE_POS, C_GAISOCODE_POS, 7)
                'xls.SetColWidth(C_GAISONAME_POS, C_GAISONAME_POS, 11.25)
                'xls.SetColWidth(C_NAISOCODE_POS, C_NAISOCODE_POS, 7)
                'xls.SetColWidth(C_NAISONAME_POS, C_NAISONAME_POS, 11.25)
                'xls.SetColWidth(C_SYATAINO_POS, C_SYATAINO_POS, 22.5)
                'xls.SetColWidth(C_OPSPEC_POS, B_KFUGO_POS - 1, 4)
                ''   ベース車情報
                'xls.SetColWidth(B_KFUGO_POS, B_GRADE_POS, 8)
                'xls.SetColWidth(B_SHIMUKE_POS, B_SHIMUKECODE_POS, 6.5)
                'xls.SetColWidth(B_KATASHIKI_POS, B_KATASHIKI_POS, 13.5)
                'xls.SetColWidth(B_OPCODE_POS, B_OPCODE_POS, 5)
                'xls.SetColWidth(B_GAISOCODE_POS, B_GAISOCODE_POS, 7)
                'xls.SetColWidth(B_GAISONAME_POS, B_GAISONAME_POS, 11.25)
                'xls.SetColWidth(B_NAISOCODE_POS, B_NAISOCODE_POS, 7)
                'xls.SetColWidth(B_NAISONAME_POS, B_NAISONAME_POS, 11.25)
                'xls.SetColWidth(B_SYATAINO_POS, B_SYATAINO_POS, 22.5)
                'xls.SetColWidth(B_OPSPEC_POS, BASE_TOKUBETU_ST_COL - 1, 4)
                ''       ベース車特別織込み
                'xls.SetColWidth(BASE_TOKUBETU_ST_COL, BASE_TOKUBETU_ST_COL, 15)
                'If BASE_TOKUBETU_CNT <> 0 Then
                '    xls.SetColWidth(BASE_TOKUBETU_ST_COL, BASE_TOKUBETU_ST_COL + BASE_TOKUBETU_CNT, 4)
                'End If
                ''   試作車特別織込み
                'xls.SetColWidth(SHISAKU_TOKUBETU_ST_COL, SHISAKU_TOKUBETU_ST_COL, 17)
                'If SHISAKU_TOKUBETU_CNT <> 0 Then
                '    xls.SetColWidth(SHISAKU_TOKUBETU_ST_COL, SHISAKU_TOKUBETU_ST_COL + SHISAKU_TOKUBETU_CNT, 4)
                'End If
                ''   完成車情報の続き
                'xls.SetColWidth(B_SHIYOMOKUTEKI_POS, B_SHIYOMOKUTEKI_POS, 5.5)
                'xls.SetColWidth(B_SHUYOKAKUNIN_POS, B_SHUYOKAKUNIN_POS, 22)
                'xls.SetColWidth(B_BUSHO_POS, B_BUSHO_POS, 9)
                'xls.SetColWidth(B_JUNJYO_POS, B_GROUP_POS, 4)
                'xls.SetColWidth(B_KANSEIKIBOBI_POS, B_KANSEIKIBOBI_POS, 14)
                'xls.SetColWidth(B_MEMO_POS, B_MEMO_POS, 22.75)

                ''行幅の調整
                'xls.SetRowHeight(1, 2, 5)
                'xls.SetRowHeight(3, 4, 21)
                'xls.SetRowHeight(5, 6, 21)
                'xls.SetRowHeight(7, 7, 33)
                'xls.SetRowHeight(8, 8, 55)
                'xls.SetRowHeight(9, 9, 132)
                'xls.SetRowHeight(10, 10, 21)
                'xls.SetRowHeight(DATA_START_ROW, DATA_END_ROW, 32.25)

            End If

        End Sub

        ''' <summary>
        ''' タイトル部作成（部品表ベース車情報）
        ''' </summary>
        ''' <param name="xls"></param>
        ''' <remarks></remarks>
        Private Sub setSheetTitleBASEtenkai(ByVal xls As ShisakuExcel)

            'xls.SetValueのパラメータは－－－＞　startCol ,startRow ,endCol ,endRow ,value

            'シンボル
            xls.SetValue(1, 9, 1, 9, "シンボル")
            xls.MergeCells(1, 9, 1, 10, True)
            xls.SetOrientation(1, 9, 1, 9, ShisakuExcel.XlOrientation.xlHorizontal, False, True)   '横
            xls.SetAlignment(1, 9, 1, 9, XlHAlign.xlHAlignLeft, XlVAlign.xlVAlignBottom, False, True) '位置
            '連番
            xls.SetValue(2, 9, 2, 9, "連番")
            xls.MergeCells(2, 9, 2, 10, True)
            xls.SetOrientation(2, 9, 2, 9, ShisakuExcel.XlOrientation.xlHorizontal, False, True)   '横
            xls.SetAlignment(2, 9, 2, 9, XlHAlign.xlHAlignLeft, XlVAlign.xlVAlignBottom, False, True) '位置
            '種別
            xls.SetValue(3, 9, 3, 9, "種別")
            xls.MergeCells(3, 9, 3, 10, True)
            xls.SetOrientation(3, 9, 3, 9, ShisakuExcel.XlOrientation.xlHorizontal, False, True)   '横
            xls.SetAlignment(3, 9, 3, 9, XlHAlign.xlHAlignLeft, XlVAlign.xlVAlignBottom, False, True) '位置
            '号車
            xls.SetValue(4, 9, 4, 9, "号車")
            xls.MergeCells(4, 9, 4, 10, True)
            xls.SetOrientation(4, 9, 4, 9, ShisakuExcel.XlOrientation.xlHorizontal, False, True)   '横
            xls.SetAlignment(4, 9, 4, 9, XlHAlign.xlHAlignLeft, XlVAlign.xlVAlignBottom, False, True) '位置
            '開発符号
            xls.SetValue(5, 9, 5, 9, "開発符号")
            xls.MergeCells(5, 9, 5, 10, True)
            xls.SetOrientation(5, 9, 5, 9, ShisakuExcel.XlOrientation.xlHorizontal, False, True)   '横
            xls.SetAlignment(5, 9, 5, 9, XlHAlign.xlHAlignLeft, XlVAlign.xlVAlignBottom, False, True) '位置
            '仕様情報№
            xls.SetValue(6, 9, 6, 9, "仕様情報№")
            xls.MergeCells(6, 9, 6, 10, True)
            xls.SetOrientation(6, 9, 6, 9, ShisakuExcel.XlOrientation.xlHorizontal, False, True)   '横
            xls.SetAlignment(6, 9, 6, 9, XlHAlign.xlHAlignLeft, XlVAlign.xlVAlignBottom, False, True) '位置
            'アプライドNo
            xls.SetValue(7, 9, 7, 9, "アプライドNo")
            xls.MergeCells(7, 9, 7, 10, True)
            xls.SetOrientation(7, 9, 7, 9, ShisakuExcel.XlOrientation.xlHorizontal, False, True)   '横
            xls.SetAlignment(7, 9, 7, 9, XlHAlign.xlHAlignLeft, XlVAlign.xlVAlignBottom, False, True) '位置
            '型式
            xls.SetValue(8, 9, 8, 9, "型式")
            xls.MergeCells(8, 9, 8, 10, True)
            xls.SetOrientation(8, 9, 8, 9, ShisakuExcel.XlOrientation.xlHorizontal, False, True)   '横
            xls.SetAlignment(8, 9, 8, 9, XlHAlign.xlHAlignLeft, XlVAlign.xlVAlignBottom, False, True) '位置
            '仕向
            xls.SetValue(9, 9, 9, 9, "仕向")
            xls.MergeCells(9, 9, 9, 10, True)
            xls.SetOrientation(9, 9, 9, 9, ShisakuExcel.XlOrientation.xlHorizontal, False, True)   '横
            xls.SetAlignment(9, 9, 9, 9, XlHAlign.xlHAlignLeft, XlVAlign.xlVAlignBottom, False, True) '位置
            'ＯＰ
            xls.SetValue(10, 9, 10, 9, "ＯＰ")
            xls.MergeCells(10, 9, 10, 10, True)
            xls.SetOrientation(10, 9, 10, 9, ShisakuExcel.XlOrientation.xlHorizontal, False, True)   '横
            xls.SetAlignment(10, 9, 10, 9, XlHAlign.xlHAlignLeft, XlVAlign.xlVAlignBottom, False, True) '位置
            '外装色
            xls.SetValue(11, 9, 11, 9, "外装色")
            xls.MergeCells(11, 9, 11, 10, True)
            xls.SetOrientation(11, 9, 11, 9, ShisakuExcel.XlOrientation.xlHorizontal, False, True)   '横
            xls.SetAlignment(11, 9, 11, 9, XlHAlign.xlHAlignLeft, XlVAlign.xlVAlignBottom, False, True) '位置
            '外装色名
            xls.SetValue(12, 9, 12, 9, "外装色名")
            xls.MergeCells(12, 9, 12, 10, True)
            xls.SetOrientation(12, 9, 12, 9, ShisakuExcel.XlOrientation.xlHorizontal, False, True)   '横
            xls.SetAlignment(12, 9, 12, 9, XlHAlign.xlHAlignLeft, XlVAlign.xlVAlignBottom, False, True) '位置
            '内装色
            xls.SetValue(13, 9, 13, 9, "内装色")
            xls.MergeCells(13, 9, 13, 10, True)
            xls.SetOrientation(13, 9, 13, 9, ShisakuExcel.XlOrientation.xlHorizontal, False, True)   '横
            xls.SetAlignment(13, 9, 13, 9, XlHAlign.xlHAlignLeft, XlVAlign.xlVAlignBottom, False, True) '位置
            '内装色名
            xls.SetValue(14, 9, 14, 9, "内装色名")
            xls.MergeCells(14, 9, 14, 10, True)
            xls.SetOrientation(14, 9, 14, 9, ShisakuExcel.XlOrientation.xlHorizontal, False, True)   '横
            xls.SetAlignment(14, 9, 14, 9, XlHAlign.xlHAlignLeft, XlVAlign.xlVAlignBottom, False, True) '位置
            'イベントコード
            xls.SetValue(15, 9, 15, 9, "イベントコード")
            xls.MergeCells(15, 9, 15, 10, True)
            xls.SetOrientation(15, 9, 15, 9, ShisakuExcel.XlOrientation.xlHorizontal, False, True)   '横
            xls.SetAlignment(15, 9, 15, 9, XlHAlign.xlHAlignLeft, XlVAlign.xlVAlignBottom, False, True) '位置
            '号車
            xls.SetValue(16, 9, 16, 9, "号車")
            xls.MergeCells(16, 9, 16, 10, True)
            xls.SetOrientation(16, 9, 16, 9, ShisakuExcel.XlOrientation.xlHorizontal, False, True)   '横
            xls.SetAlignment(16, 9, 16, 9, XlHAlign.xlHAlignLeft, XlVAlign.xlVAlignBottom, False, True) '位置

            'ベース車情報
            xls.SetValue(1, 8, 16, 8, "部品表ベース車情報")
            xls.MergeCells(1, 8, 16, 8, True)
            xls.SetOrientation(1, 8, 16, 8, ShisakuExcel.XlOrientation.xlHorizontal, False, True)   '横
            xls.SetAlignment(1, 8, 16, 8, XlHAlign.xlHAlignCenter, XlVAlign.xlVAlignBottom, False, True) '位置

        End Sub

        ''' <summary>
        ''' EXCEL出力・ボディーパート(部品表ベース車シート)
        ''' </summary>
        ''' <param name="xls"></param>
        ''' <remarks></remarks>
        Private Sub setSheet1BodyPartBASEtenkai(ByVal xls As ShisakuExcel)

            'xls.SetValueのパラメータは－－－＞　startCol ,startRow ,endCol ,endRow ,value
            '初期設定
            Dim lngDataCnt As Long = 0
            Dim voBase As New TShisakuEventBaseVo
            Dim getColorName As New EventEdit.Dao.EventEditBaseCarDaoImpl

            '   ベース車情報ＥＸＣＥＬ貼り付け
            For Each Hvo As TShisakuEventBaseVo In baseTenkaiList   '前回のベース車情報

                'データ件数カウント
                lngDataCnt = lngDataCnt + 1
                '空白分を追加
                If Hvo.HyojijunNo + 1 > lngDataCnt Then
                    lngDataCnt = lngDataCnt + (Hvo.HyojijunNo + 1 - lngDataCnt)
                End If

                '-----------------------------------------------------
                'シンボル

                '連番
                xls.SetValue(2, Hvo.HyojijunNo + lngStartRow, _
                             2, Hvo.HyojijunNo + lngStartRow, Hvo.HyojijunNo + 1)
                '種別
                xls.SetValue(3, Hvo.HyojijunNo + lngStartRow, _
                             3, Hvo.HyojijunNo + lngStartRow, Hvo.ShisakuSyubetu)
                '号車
                xls.SetValue(4, Hvo.HyojijunNo + lngStartRow, _
                             4, Hvo.HyojijunNo + lngStartRow, Hvo.ShisakuGousya)
                '開発符号
                xls.SetValue(5, Hvo.HyojijunNo + lngStartRow, _
                             5, Hvo.HyojijunNo + lngStartRow, Hvo.BaseKaihatsuFugo)
                '仕様情報№
                xls.SetValue(6, Hvo.HyojijunNo + lngStartRow, _
                             6, Hvo.HyojijunNo + lngStartRow, Hvo.BaseShiyoujyouhouNo)
                'アプライドNo
                xls.SetValue(7, Hvo.HyojijunNo + lngStartRow, _
                             7, Hvo.HyojijunNo + lngStartRow, Hvo.BaseAppliedNo)
                '型式
                xls.SetValue(8, Hvo.HyojijunNo + lngStartRow, _
                             8, Hvo.HyojijunNo + lngStartRow, Hvo.BaseKatashiki)
                '仕向
                Dim strBaseShimuke As String = ""
                If StringUtil.IsEmpty(Hvo.BaseShimuke) Then
                    strBaseShimuke = "国内"
                Else
                    strBaseShimuke = Hvo.BaseShimuke
                End If
                xls.SetValue(9, Hvo.HyojijunNo + lngStartRow, _
                             9, Hvo.HyojijunNo + lngStartRow, strBaseShimuke)
                'ＯＰ
                xls.SetValue(10, Hvo.HyojijunNo + lngStartRow, _
                             10, Hvo.HyojijunNo + lngStartRow, Hvo.BaseOp)
                '外装色
                xls.SetValue(11, Hvo.HyojijunNo + lngStartRow, _
                             11, Hvo.HyojijunNo + lngStartRow, Hvo.BaseGaisousyoku)
                '外装色名をエクセルに反映'
                If Not StringUtil.IsEmpty(Hvo.BaseGaisousyoku) Then
                    Dim GaisoName = getColorName.FindGaisouColorName(Hvo.BaseGaisousyoku)
                    xls.SetValue(12, Hvo.HyojijunNo + lngStartRow, _
                                 12, Hvo.HyojijunNo + lngStartRow, Trim(GaisoName.ColorName))
                End If
                '内装色
                xls.SetValue(13, Hvo.HyojijunNo + lngStartRow, _
                             13, Hvo.HyojijunNo + lngStartRow, Hvo.BaseNaisousyoku)
                '内装色名をエクセルに反映'
                If Not StringUtil.IsEmpty(Hvo.BaseNaisousyoku) Then
                    Dim NaisoName = getColorName.FindNaisouColorName(Hvo.BaseNaisousyoku)
                    xls.SetValue(14, Hvo.HyojijunNo + lngStartRow, _
                                 14, Hvo.HyojijunNo + lngStartRow, Trim(NaisoName.ColorName))
                End If
                '試作イベントコード
                xls.SetValue(15, Hvo.HyojijunNo + lngStartRow, _
                             15, Hvo.HyojijunNo + lngStartRow, Hvo.ShisakuBaseEventCode)
                '試作イベント号車
                xls.SetValue(16, Hvo.HyojijunNo + lngStartRow, _
                             16, Hvo.HyojijunNo + lngStartRow, Hvo.ShisakuBaseGousya)

                '=================================================================================================================
            Next

            '罫線を引く
            xls.SetLine(1, 8, 16, lngStartRow + lngDataCnt - 1, XlBordersIndex.xlEdgeTop, XlLineStyle.xlContinuous, XlBorderWeight.xlThin) '上
            xls.SetLine(1, 8, 16, lngStartRow + lngDataCnt - 1, XlBordersIndex.xlEdgeBottom, XlLineStyle.xlContinuous, XlBorderWeight.xlThin) '下
            xls.SetLine(1, 8, 16, lngStartRow + lngDataCnt - 1, XlBordersIndex.xlEdgeLeft, XlLineStyle.xlContinuous, XlBorderWeight.xlThin) '左
            xls.SetLine(1, 8, 16, lngStartRow + lngDataCnt - 1, XlBordersIndex.xlEdgeRight, XlLineStyle.xlContinuous, XlBorderWeight.xlThin) '右
            xls.SetLine(1, 8, 16, lngStartRow + lngDataCnt - 1, XlBordersIndex.xlInsideHorizontal, XlLineStyle.xlContinuous, XlBorderWeight.xlThin) '水平
            xls.SetLine(1, 8, 16, lngStartRow + lngDataCnt - 1, XlBordersIndex.xlInsideVertical, XlLineStyle.xlContinuous, XlBorderWeight.xlThin) '垂直

        End Sub


        ''' <summary>
        ''' タイトル部作成（製作一覧ベース車情報）
        ''' </summary>
        ''' <param name="xls"></param>
        ''' <remarks></remarks>
        Private Sub setSheetTitleBASE(ByVal xls As ShisakuExcel)

            'xls.SetValueのパラメータは－－－＞　startCol ,startRow ,endCol ,endRow ,value

            'シンボル
            xls.SetValue(1, 9, 1, 9, "シンボル")
            xls.MergeCells(1, 9, 1, 10, True)
            xls.SetOrientation(1, 9, 1, 9, ShisakuExcel.XlOrientation.xlHorizontal, False, True)   '横
            xls.SetAlignment(1, 9, 1, 9, XlHAlign.xlHAlignLeft, XlVAlign.xlVAlignBottom, False, True) '位置
            '連番
            xls.SetValue(2, 9, 2, 9, "連番")
            xls.MergeCells(2, 9, 2, 10, True)
            xls.SetOrientation(2, 9, 2, 9, ShisakuExcel.XlOrientation.xlHorizontal, False, True)   '横
            xls.SetAlignment(2, 9, 2, 9, XlHAlign.xlHAlignLeft, XlVAlign.xlVAlignBottom, False, True) '位置
            '種別
            xls.SetValue(3, 9, 3, 9, "種別")
            xls.MergeCells(3, 9, 3, 10, True)
            xls.SetOrientation(3, 9, 3, 9, ShisakuExcel.XlOrientation.xlHorizontal, False, True)   '横
            xls.SetAlignment(3, 9, 3, 9, XlHAlign.xlHAlignLeft, XlVAlign.xlVAlignBottom, False, True) '位置
            '号車
            xls.SetValue(4, 9, 4, 9, "号車")
            xls.MergeCells(4, 9, 4, 10, True)
            xls.SetOrientation(4, 9, 4, 9, ShisakuExcel.XlOrientation.xlHorizontal, False, True)   '横
            xls.SetAlignment(4, 9, 4, 9, XlHAlign.xlHAlignLeft, XlVAlign.xlVAlignBottom, False, True) '位置
            '開発符号
            xls.SetValue(5, 9, 5, 9, "開発符号")
            xls.MergeCells(5, 9, 5, 10, True)
            xls.SetOrientation(5, 9, 5, 9, ShisakuExcel.XlOrientation.xlHorizontal, False, True)   '横
            xls.SetAlignment(5, 9, 5, 9, XlHAlign.xlHAlignLeft, XlVAlign.xlVAlignBottom, False, True) '位置
            '仕様情報№
            xls.SetValue(6, 9, 6, 9, "仕様情報№")
            xls.MergeCells(6, 9, 6, 10, True)
            xls.SetOrientation(6, 9, 6, 9, ShisakuExcel.XlOrientation.xlHorizontal, False, True)   '横
            xls.SetAlignment(6, 9, 6, 9, XlHAlign.xlHAlignLeft, XlVAlign.xlVAlignBottom, False, True) '位置

            '参考情報
            xls.SetValue(7, 9, 7, 9, "［参考情報］")
            xls.MergeCells(7, 9, 15, 9, True) '
            xls.SetOrientation(7, 9, 7, 9, ShisakuExcel.XlOrientation.xlHorizontal, False, True)   '横
            xls.SetAlignment(7, 9, 7, 9, XlHAlign.xlHAlignLeft, XlVAlign.xlVAlignBottom, False, True) '位置
            '車型
            xls.SetValue(7, 10, 7, 10, "車型")
            xls.SetOrientation(7, 10, 7, 10, ShisakuExcel.XlOrientation.xlHorizontal, False, True)   '横
            xls.SetAlignment(7, 10, 7, 10, XlHAlign.xlHAlignLeft, XlVAlign.xlVAlignBottom, False, True) '位置
            'グレード
            xls.SetValue(8, 10, 8, 10, "グレード")
            xls.SetOrientation(8, 10, 8, 10, ShisakuExcel.XlOrientation.xlHorizontal, False, True)   '横
            xls.SetAlignment(8, 10, 8, 10, XlHAlign.xlHAlignLeft, XlVAlign.xlVAlignBottom, False, True) '位置
            '仕向地・仕向け
            xls.SetValue(9, 10, 9, 10, "仕向地・仕向け")
            xls.SetOrientation(9, 10, 9, 10, ShisakuExcel.XlOrientation.xlHorizontal, False, True)   '横
            xls.SetAlignment(9, 10, 9, 10, XlHAlign.xlHAlignLeft, XlVAlign.xlVAlignBottom, False, True) '位置
            '仕向地・ハンドル
            xls.SetValue(10, 10, 10, 10, "仕向地・ハンドル")
            xls.SetOrientation(10, 10, 10, 10, ShisakuExcel.XlOrientation.xlHorizontal, False, True)   '横
            xls.SetAlignment(10, 10, 10, 10, XlHAlign.xlHAlignLeft, XlVAlign.xlVAlignBottom, False, True) '位置
            'E/G仕様・排気量
            xls.SetValue(11, 10, 11, 10, "E/G仕様・排気量")
            xls.SetOrientation(11, 10, 11, 10, ShisakuExcel.XlOrientation.xlHorizontal, False, True)   '横
            xls.SetAlignment(11, 10, 11, 10, XlHAlign.xlHAlignLeft, XlVAlign.xlVAlignBottom, False, True) '位置
            'E/G仕様・型式
            xls.SetValue(12, 10, 12, 10, "E/G仕様・型式")
            xls.SetOrientation(12, 10, 12, 10, ShisakuExcel.XlOrientation.xlHorizontal, False, True)   '横
            xls.SetAlignment(12, 10, 12, 10, XlHAlign.xlHAlignLeft, XlVAlign.xlVAlignBottom, False, True) '位置
            'E/G仕様・過給器
            xls.SetValue(13, 10, 13, 10, "E/G仕様・過給器")
            xls.SetOrientation(13, 10, 13, 10, ShisakuExcel.XlOrientation.xlHorizontal, False, True)   '横
            xls.SetAlignment(13, 10, 13, 10, XlHAlign.xlHAlignLeft, XlVAlign.xlVAlignBottom, False, True) '位置
            'E/G仕様・駆動方式
            xls.SetValue(14, 10, 14, 10, "E/G仕様・駆動方式")
            xls.SetOrientation(14, 10, 14, 10, ShisakuExcel.XlOrientation.xlHorizontal, False, True)   '横
            xls.SetAlignment(14, 10, 14, 10, XlHAlign.xlHAlignLeft, XlVAlign.xlVAlignBottom, False, True) '位置
            'E/G仕様・変速機
            xls.SetValue(15, 10, 15, 10, "E/G仕様・変速機")
            xls.SetOrientation(15, 10, 15, 10, ShisakuExcel.XlOrientation.xlHorizontal, False, True)   '横
            xls.SetAlignment(15, 10, 15, 10, XlHAlign.xlHAlignLeft, XlVAlign.xlVAlignBottom, False, True) '位置

            'アプライドNo
            xls.SetValue(16, 9, 16, 9, "アプライドNo")
            xls.MergeCells(16, 9, 16, 10, True)
            xls.SetOrientation(16, 9, 16, 9, ShisakuExcel.XlOrientation.xlHorizontal, False, True)   '横
            xls.SetAlignment(16, 9, 16, 9, XlHAlign.xlHAlignLeft, XlVAlign.xlVAlignBottom, False, True) '位置
            '型式
            xls.SetValue(17, 9, 17, 9, "型式")
            xls.MergeCells(17, 9, 17, 10, True)
            xls.SetOrientation(17, 9, 17, 9, ShisakuExcel.XlOrientation.xlHorizontal, False, True)   '横
            xls.SetAlignment(17, 9, 17, 9, XlHAlign.xlHAlignLeft, XlVAlign.xlVAlignBottom, False, True) '位置
            '仕向
            xls.SetValue(18, 9, 18, 9, "仕向")
            xls.MergeCells(18, 9, 18, 10, True)
            xls.SetOrientation(18, 9, 18, 9, ShisakuExcel.XlOrientation.xlHorizontal, False, True)   '横
            xls.SetAlignment(18, 9, 18, 9, XlHAlign.xlHAlignLeft, XlVAlign.xlVAlignBottom, False, True) '位置
            'ＯＰ
            xls.SetValue(19, 9, 19, 9, "ＯＰ")
            xls.MergeCells(19, 9, 19, 10, True)
            xls.SetOrientation(19, 9, 19, 9, ShisakuExcel.XlOrientation.xlHorizontal, False, True)   '横
            xls.SetAlignment(19, 9, 19, 9, XlHAlign.xlHAlignLeft, XlVAlign.xlVAlignBottom, False, True) '位置
            '外装色
            xls.SetValue(20, 9, 20, 9, "外装色")
            xls.MergeCells(20, 9, 20, 10, True)
            xls.SetOrientation(20, 9, 20, 9, ShisakuExcel.XlOrientation.xlHorizontal, False, True)   '横
            xls.SetAlignment(20, 9, 20, 9, XlHAlign.xlHAlignLeft, XlVAlign.xlVAlignBottom, False, True) '位置
            '外装色名
            xls.SetValue(21, 9, 21, 9, "外装色名")
            xls.MergeCells(21, 9, 21, 10, True)
            xls.SetOrientation(21, 9, 21, 9, ShisakuExcel.XlOrientation.xlHorizontal, False, True)   '横
            xls.SetAlignment(21, 9, 21, 9, XlHAlign.xlHAlignLeft, XlVAlign.xlVAlignBottom, False, True) '位置
            '内装色
            xls.SetValue(22, 9, 22, 9, "内装色")
            xls.MergeCells(22, 9, 22, 10, True)
            xls.SetOrientation(22, 9, 22, 9, ShisakuExcel.XlOrientation.xlHorizontal, False, True)   '横
            xls.SetAlignment(22, 9, 22, 9, XlHAlign.xlHAlignLeft, XlVAlign.xlVAlignBottom, False, True) '位置
            '内装色名
            xls.SetValue(23, 9, 23, 9, "内装色名")
            xls.MergeCells(23, 9, 23, 10, True)
            xls.SetOrientation(23, 9, 23, 9, ShisakuExcel.XlOrientation.xlHorizontal, False, True)   '横
            xls.SetAlignment(23, 9, 23, 9, XlHAlign.xlHAlignLeft, XlVAlign.xlVAlignBottom, False, True) '位置
            'イベントコード
            xls.SetValue(24, 9, 24, 9, "イベントコード")
            xls.MergeCells(24, 9, 24, 10, True)
            xls.SetOrientation(24, 9, 24, 9, ShisakuExcel.XlOrientation.xlHorizontal, False, True)   '横
            xls.SetAlignment(24, 9, 24, 9, XlHAlign.xlHAlignLeft, XlVAlign.xlVAlignBottom, False, True) '位置
            '号車
            xls.SetValue(25, 9, 25, 9, "号車")
            xls.MergeCells(25, 9, 25, 10, True)
            xls.SetOrientation(25, 9, 25, 9, ShisakuExcel.XlOrientation.xlHorizontal, False, True)   '横
            xls.SetAlignment(25, 9, 25, 9, XlHAlign.xlHAlignLeft, XlVAlign.xlVAlignBottom, False, True) '位置
            '車体№
            xls.SetValue(26, 9, 26, 9, "車体№")
            xls.MergeCells(26, 9, 26, 10, True)
            xls.SetOrientation(26, 9, 26, 9, ShisakuExcel.XlOrientation.xlHorizontal, False, True)   '横
            xls.SetAlignment(26, 9, 26, 9, XlHAlign.xlHAlignLeft, XlVAlign.xlVAlignBottom, False, True) '位置

            'ベース車情報
            xls.SetValue(1, 8, 26, 8, "製作一覧ベース車情報")
            xls.MergeCells(1, 8, 26, 8, True)
            xls.SetOrientation(1, 8, 26, 8, ShisakuExcel.XlOrientation.xlHorizontal, False, True)   '横
            xls.SetAlignment(1, 8, 26, 8, XlHAlign.xlHAlignCenter, XlVAlign.xlVAlignBottom, False, True) '位置

        End Sub

        ''' <summary>
        ''' EXCEL出力・ボディーパート(製作一覧ベース車シート)
        ''' </summary>
        ''' <param name="xls"></param>
        ''' <remarks></remarks>
        Private Function setSheet1BodyPartBASE(ByVal xls As ShisakuExcel) As String

            'xls.SetValueのパラメータは－－－＞　startCol ,startRow ,endCol ,endRow ,value
            '初期設定
            Dim lngDataCnt As Long = 0
            Dim lngChgDataCnt As Long = 0
            Dim lngChgData As Long = 0
            Dim voBase As New TShisakuEventBaseSeisakuIchiranVo
            Dim getColorName As New EventEdit.Dao.EventEditBaseCarDaoImpl

            '戻り値
            '   変更無時はブランク
            Dim baseUpdateFlg As String = ""

            '   ベース車情報ＥＸＣＥＬ貼り付け
            '       比較元の情報より貼り付ける。
            For Each Hvo As TShisakuEventBaseKaiteiVo In HbaseList   '前回のベース車情報

                '変更フラグとして使用
                Dim strUpdateFlg As String = Nothing
                'データ件数カウント
                lngDataCnt = lngDataCnt + 1
                '空白分を追加
                If Hvo.HyojijunNo + 1 > lngDataCnt Then
                    lngDataCnt = lngDataCnt + (Hvo.HyojijunNo + 1 - lngDataCnt)
                End If

                '-----------------------------------------------------
                'シンボル

                '連番
                xls.SetValue(2, Hvo.HyojijunNo + lngChgData + lngStartRow, _
                             2, Hvo.HyojijunNo + lngChgData + lngStartRow, Hvo.HyojijunNo + 1)
                '種別
                xls.SetValue(3, Hvo.HyojijunNo + lngChgData + lngStartRow, _
                             3, Hvo.HyojijunNo + lngChgData + lngStartRow, Hvo.ShisakuSyubetu)
                '号車
                xls.SetValue(4, Hvo.HyojijunNo + lngChgData + lngStartRow, _
                             4, Hvo.HyojijunNo + lngChgData + lngStartRow, Hvo.ShisakuGousya)
                '開発符号
                xls.SetValue(5, Hvo.HyojijunNo + lngChgData + lngStartRow, _
                             5, Hvo.HyojijunNo + lngChgData + lngStartRow, Hvo.BaseKaihatsuFugo)
                '仕様情報№
                xls.SetValue(6, Hvo.HyojijunNo + lngChgData + lngStartRow, _
                             6, Hvo.HyojijunNo + lngChgData + lngStartRow, Hvo.BaseShiyoujyouhouNo)
                '車型
                xls.SetValue(7, Hvo.HyojijunNo + lngChgData + lngStartRow, _
                             7, Hvo.HyojijunNo + lngChgData + lngStartRow, Hvo.SeisakuSyasyu)
                'グレード
                xls.SetValue(8, Hvo.HyojijunNo + lngChgData + lngStartRow, _
                             8, Hvo.HyojijunNo + lngChgData + lngStartRow, Hvo.SeisakuGrade)
                '仕向地・仕向け
                xls.SetValue(9, Hvo.HyojijunNo + lngChgData + lngStartRow, _
                             9, Hvo.HyojijunNo + lngChgData + lngStartRow, ShimukechiShimukeHenkan(Hvo.SeisakuShimuke, Hvo.BaseShimuke))
                '仕向地・ハンドル
                xls.SetValue(10, Hvo.HyojijunNo + lngChgData + lngStartRow, _
                             10, Hvo.HyojijunNo + lngChgData + lngStartRow, Hvo.SeisakuHandoru & "HD")
                'E/G仕様・排気量
                xls.SetValue(11, Hvo.HyojijunNo + lngChgData + lngStartRow, _
                             11, Hvo.HyojijunNo + lngChgData + lngStartRow, Hvo.SeisakuEgHaikiryou)
                'E/G仕様・型式
                xls.SetValue(12, Hvo.HyojijunNo + lngChgData + lngStartRow, _
                             12, Hvo.HyojijunNo + lngChgData + lngStartRow, Hvo.SeisakuEgKatashiki)
                'E/G仕様・過給器
                xls.SetValue(13, Hvo.HyojijunNo + lngChgData + lngStartRow, _
                             13, Hvo.HyojijunNo + lngChgData + lngStartRow, Hvo.SeisakuEgKakyuuki)
                'T/M仕様・駆動方式
                xls.SetValue(14, Hvo.HyojijunNo + lngChgData + lngStartRow, _
                             14, Hvo.HyojijunNo + lngChgData + lngStartRow, Hvo.SeisakuTmKudou)
                'T/M仕様・変速機
                xls.SetValue(15, Hvo.HyojijunNo + lngChgData + lngStartRow, _
                             15, Hvo.HyojijunNo + lngChgData + lngStartRow, Hvo.SeisakuTmHensokuki)
                'アプライドNo
                xls.SetValue(16, Hvo.HyojijunNo + lngChgData + lngStartRow, _
                             16, Hvo.HyojijunNo + lngChgData + lngStartRow, Hvo.BaseAppliedNo)
                '型式
                xls.SetValue(17, Hvo.HyojijunNo + lngChgData + lngStartRow, _
                             17, Hvo.HyojijunNo + lngChgData + lngStartRow, Hvo.BaseKatashiki)
                '仕向
                Dim strBaseShimuke As String = ""
                If StringUtil.IsEmpty(Hvo.BaseShimuke) Then
                    strBaseShimuke = "国内"
                Else
                    strBaseShimuke = Hvo.BaseShimuke
                End If
                xls.SetValue(18, Hvo.HyojijunNo + lngChgData + lngStartRow, _
                             18, Hvo.HyojijunNo + lngChgData + lngStartRow, strBaseShimuke)
                'ＯＰ
                xls.SetValue(19, Hvo.HyojijunNo + lngChgData + lngStartRow, _
                             19, Hvo.HyojijunNo + lngChgData + lngStartRow, Hvo.BaseOp)
                '外装色
                xls.SetValue(20, Hvo.HyojijunNo + lngChgData + lngStartRow, _
                             20, Hvo.HyojijunNo + lngChgData + lngStartRow, Hvo.BaseGaisousyoku)
                '外装色名をエクセルに反映'
                If Not StringUtil.IsEmpty(Hvo.BaseGaisousyoku) Then
                    Dim GaisoName = getColorName.FindGaisouColorName(Hvo.BaseGaisousyoku)
                    If StringUtil.IsNotEmpty(GaisoName) Then
                        xls.SetValue(21, Hvo.HyojijunNo + lngChgData + lngStartRow, _
                                     21, Hvo.HyojijunNo + lngChgData + lngStartRow, Trim(GaisoName.ColorName))
                    End If
                End If
                If StringUtil.Equals(Hvo.BaseGaisousyoku, "-") Then
                    xls.SetValue(21, Hvo.HyojijunNo + lngChgData + lngStartRow, _
                                 21, Hvo.HyojijunNo + lngChgData + lngStartRow, "電着のみ")
                End If
                '内装色
                xls.SetValue(22, Hvo.HyojijunNo + lngChgData + lngStartRow, _
                             22, Hvo.HyojijunNo + lngChgData + lngStartRow, Hvo.BaseNaisousyoku)
                '内装色名をエクセルに反映'
                If Not StringUtil.IsEmpty(Hvo.BaseNaisousyoku) Then
                    Dim NaisoName = getColorName.FindNaisouColorName(Hvo.BaseNaisousyoku)
                    If StringUtil.IsNotEmpty(NaisoName) Then
                        xls.SetValue(23, Hvo.HyojijunNo + lngChgData + lngStartRow, _
                                     23, Hvo.HyojijunNo + lngChgData + lngStartRow, Trim(NaisoName.ColorName))
                    End If
                End If
                '試作イベントコード
                xls.SetValue(24, Hvo.HyojijunNo + lngChgData + lngStartRow, _
                             24, Hvo.HyojijunNo + lngChgData + lngStartRow, Hvo.ShisakuBaseEventCode)
                '試作イベント号車
                xls.SetValue(25, Hvo.HyojijunNo + lngChgData + lngStartRow, _
                             25, Hvo.HyojijunNo + lngChgData + lngStartRow, Hvo.ShisakuBaseGousya)
                '車体№
                xls.SetValue(26, Hvo.HyojijunNo + lngChgData + lngStartRow, _
                             26, Hvo.HyojijunNo + lngChgData + lngStartRow, Hvo.SeisakuSyataiNo)

                '=================================================================================================================
                '変化点はバックカラーを変更する。
                If StringUtil.IsNotEmpty(HikakuEventCode) = True Then
                    'カウントクリア
                    lngChgDataCnt = 0
                    '-----------------------------------------------------
                    '今回取込ファイルのイベントを号車で抽出する。
                    If baseList.Count <> 0 Then
                        For Each vo As TShisakuEventBaseSeisakuIchiranVo In baseList   '今回登録時のデータ
                            Dim hvoShisakuSyubetu As String = ""
                            If StringUtil.IsEmpty(Hvo.ShisakuSyubetu) Then
                                hvoShisakuSyubetu = ""
                            Else
                                hvoShisakuSyubetu = Hvo.ShisakuSyubetu

                            End If
                            Dim voShisakuSyubetu As String = ""
                            If StringUtil.IsEmpty(vo.ShisakuSyubetu) Then
                                voShisakuSyubetu = ""
                            Else
                                voShisakuSyubetu = vo.ShisakuSyubetu

                            End If
                            If StringUtil.Equals(Hvo.ShisakuGousya, vo.ShisakuGousya)  Then
                                'If StringUtil.Equals(Hvo.ShisakuGousya, vo.ShisakuGousya) And _
                                '   StringUtil.Equals(hvoShisakuSyubetu, voShisakuSyubetu) Then
                                voBase = vo
                                lngChgDataCnt = lngChgDataCnt + 1
                                Exit For
                            End If
                        Next
                    End If

                    'シンボル更新
                    If lngChgDataCnt = 0 Then
                        '前回にあって今回にないので削除データとみなす。そしてシンボルへ削除をセット。
                        xls.SetValue(1, Hvo.HyojijunNo + lngChgData + lngStartRow, _
                                     1, Hvo.HyojijunNo + lngChgData + lngStartRow, "削除")
                        '-----------------------------------------------------
                        '背景色を変更
                        '   ～特別装備仕様の最後の列まで
                        xls.SetBackColor(2, Hvo.HyojijunNo + lngChgData + lngStartRow, _
                                         26, Hvo.HyojijunNo + lngChgData + lngStartRow, RGB(192, 192, 192))    '削除カラー
                        '-----------------------------------------------------
                        baseUpdateFlg = "UPD"
                    Else

                        '-----------------------------------------------------
                        '開発符号：比較して差異があればバックカラーを変更
                        '　Nothingとブランクが同居する可能性があるのでブランクに統一
                        If StringUtil.IsEmpty(Hvo.BaseKaihatsuFugo) Then Hvo.BaseKaihatsuFugo = ""
                        If StringUtil.IsEmpty(voBase.BaseKaihatsuFugo) Then voBase.BaseKaihatsuFugo = ""

                        If Not StringUtil.Equals(Hvo.BaseKaihatsuFugo, voBase.BaseKaihatsuFugo) Then
                            xls.SetBackColor(5, Hvo.HyojijunNo + lngChgData + lngStartRow, _
                                             5, Hvo.HyojijunNo + lngChgData + +lngStartRow, RGB(255, 153, 204))
                            strUpdateFlg = "CHANGE"
                            xls.SetValue(5, Hvo.HyojijunNo + lngChgData + lngStartRow, _
                                         5, Hvo.HyojijunNo + lngChgData + lngStartRow, voBase.BaseKaihatsuFugo)
                            Dim MaeAtai As String = ""
                            If StringUtil.IsEmpty(Hvo.BaseKaihatsuFugo) Then
                                MaeAtai = "空白"    'ＶＯ値が空なら空白を設定
                            Else
                                MaeAtai = Hvo.BaseKaihatsuFugo
                            End If
                            xls.SetComment("変更前の値：" & MaeAtai, _
                                            5, Hvo.HyojijunNo + lngChgData + lngStartRow, _
                                            5, Hvo.HyojijunNo + lngChgData + lngStartRow)   '変更前の値をコメントにセット
                        End If
                        '-----------------------------------------------------
                        '仕様情報Ｎｏ：比較して差異があればバックカラーを変更
                        '　Nothingとブランクが同居する可能性があるのでブランクに統一
                        If StringUtil.IsEmpty(Hvo.BaseShiyoujyouhouNo) Then Hvo.BaseShiyoujyouhouNo = ""
                        If StringUtil.IsEmpty(voBase.BaseShiyoujyouhouNo) Then voBase.BaseShiyoujyouhouNo = ""

                        If Not StringUtil.Equals(Hvo.BaseShiyoujyouhouNo, voBase.BaseShiyoujyouhouNo) Then
                            xls.SetBackColor(6, Hvo.HyojijunNo + lngChgData + lngStartRow, _
                                             6, Hvo.HyojijunNo + lngChgData + +lngStartRow, RGB(255, 153, 204))
                            strUpdateFlg = "CHANGE"
                            xls.SetValue(6, Hvo.HyojijunNo + lngChgData + lngStartRow, _
                                         6, Hvo.HyojijunNo + lngChgData + lngStartRow, voBase.BaseShiyoujyouhouNo)
                            Dim MaeAtai As String = ""
                            If StringUtil.IsEmpty(Hvo.BaseShiyoujyouhouNo) Then
                                MaeAtai = "空白"    'ＶＯ値が空なら空白を設定
                            Else
                                MaeAtai = Hvo.BaseShiyoujyouhouNo
                            End If
                            xls.SetComment("変更前の値：" & MaeAtai, _
                                            6, Hvo.HyojijunNo + lngChgData + lngStartRow, _
                                            6, Hvo.HyojijunNo + lngChgData + lngStartRow)   '変更前の値をコメントにセット
                        End If
                        '-----------------------------------------------------
                        '車型：比較して差異があればバックカラーを変更
                        '　Nothingとブランクが同居する可能性があるのでブランクに統一
                        If StringUtil.IsEmpty(Hvo.SeisakuSyasyu) Then Hvo.SeisakuSyasyu = ""
                        If StringUtil.IsEmpty(voBase.SeisakuSyasyu) Then voBase.SeisakuSyasyu = ""

                        If Not StringUtil.Equals(Hvo.SeisakuSyasyu, voBase.SeisakuSyasyu) Then
                            xls.SetBackColor(7, Hvo.HyojijunNo + lngChgData + lngStartRow, _
                                             7, Hvo.HyojijunNo + lngChgData + +lngStartRow, RGB(255, 153, 204))
                            strUpdateFlg = "CHANGE"
                            xls.SetValue(7, Hvo.HyojijunNo + lngChgData + lngStartRow, _
                                         7, Hvo.HyojijunNo + lngChgData + lngStartRow, voBase.SeisakuSyasyu)
                            Dim MaeAtai As String = ""
                            If StringUtil.IsEmpty(Hvo.SeisakuSyasyu) Then
                                MaeAtai = "空白"    'ＶＯ値が空なら空白を設定
                            Else
                                MaeAtai = Hvo.SeisakuSyasyu
                            End If
                            xls.SetComment("変更前の値：" & MaeAtai, _
                                            7, Hvo.HyojijunNo + lngChgData + lngStartRow, _
                                            7, Hvo.HyojijunNo + lngChgData + lngStartRow)   '変更前の値をコメントにセット
                        End If
                        '-----------------------------------------------------
                        'グレード：比較して差異があればバックカラーを変更
                        '　Nothingとブランクが同居する可能性があるのでブランクに統一
                        If StringUtil.IsEmpty(Hvo.SeisakuGrade) Then Hvo.SeisakuGrade = ""
                        If StringUtil.IsEmpty(voBase.SeisakuGrade) Then voBase.SeisakuGrade = ""

                        If Not StringUtil.Equals(Hvo.SeisakuGrade, voBase.SeisakuGrade) Then
                            xls.SetBackColor(8, Hvo.HyojijunNo + lngChgData + lngStartRow, _
                                             8, Hvo.HyojijunNo + lngChgData + +lngStartRow, RGB(255, 153, 204))
                            strUpdateFlg = "CHANGE"
                            xls.SetValue(8, Hvo.HyojijunNo + lngChgData + lngStartRow, _
                                         8, Hvo.HyojijunNo + lngChgData + lngStartRow, voBase.SeisakuGrade)
                            Dim MaeAtai As String = ""
                            If StringUtil.IsEmpty(Hvo.SeisakuGrade) Then
                                MaeAtai = "空白"    'ＶＯ値が空なら空白を設定
                            Else
                                MaeAtai = Hvo.SeisakuGrade
                            End If
                            xls.SetComment("変更前の値：" & MaeAtai, _
                                            8, Hvo.HyojijunNo + lngChgData + lngStartRow, _
                                            8, Hvo.HyojijunNo + lngChgData + lngStartRow)   '変更前の値をコメントにセット
                        End If
                        '-----------------------------------------------------
                        '仕向地・仕向け：比較して差異があればバックカラーを変更
                        '　Nothingとブランクが同居する可能性があるのでブランクに統一
                        If StringUtil.IsEmpty(Hvo.SeisakuShimuke) Then Hvo.SeisakuShimuke = ""
                        If StringUtil.IsEmpty(voBase.SeisakuShimuke) Then voBase.SeisakuShimuke = ""

                        '変換してぶつける。
                        If Not StringUtil.Equals(ShimukechiShimukeHenkan(Hvo.SeisakuShimuke, Hvo.BaseShimuke), _
                                                 ShimukechiShimukeHenkan(voBase.SeisakuShimuke, voBase.BaseShimuke)) Then
                            xls.SetBackColor(9, Hvo.HyojijunNo + lngChgData + lngStartRow, _
                                             9, Hvo.HyojijunNo + lngChgData + +lngStartRow, RGB(255, 153, 204))
                            strUpdateFlg = "CHANGE"
                            xls.SetValue(9, Hvo.HyojijunNo + lngChgData + lngStartRow, _
                                         9, Hvo.HyojijunNo + lngChgData + lngStartRow, ShimukechiShimukeHenkan(voBase.SeisakuShimuke, voBase.BaseShimuke))
                            xls.SetComment("変更前の値：" & ShimukechiShimukeHenkan(Hvo.SeisakuShimuke, Hvo.BaseShimuke), _
                                            9, Hvo.HyojijunNo + lngChgData + lngStartRow, _
                                            9, Hvo.HyojijunNo + lngChgData + lngStartRow)   '変更前の値をコメントにセット
                        End If
                        '-----------------------------------------------------
                        '仕向地・ハンドル：比較して差異があればバックカラーを変更
                        '　Nothingとブランクが同居する可能性があるのでブランクに統一
                        If StringUtil.IsEmpty(Hvo.SeisakuHandoru) Then Hvo.SeisakuHandoru = ""
                        If StringUtil.IsEmpty(voBase.SeisakuHandoru) Then voBase.SeisakuHandoru = ""

                        If Not StringUtil.Equals(Hvo.SeisakuHandoru, voBase.SeisakuHandoru) Then
                            xls.SetBackColor(10, Hvo.HyojijunNo + lngChgData + lngStartRow, _
                                             10, Hvo.HyojijunNo + lngChgData + +lngStartRow, RGB(255, 153, 204))
                            strUpdateFlg = "CHANGE"
                            If StringUtil.IsNotEmpty(voBase.SeisakuHandoru) Then _
                                        voBase.SeisakuHandoru = voBase.SeisakuHandoru & "HD"
                            xls.SetValue(10, Hvo.HyojijunNo + lngChgData + lngStartRow, _
                                         10, Hvo.HyojijunNo + lngChgData + lngStartRow, voBase.SeisakuHandoru)
                            Dim MaeAtai As String = ""
                            If StringUtil.IsEmpty(Hvo.SeisakuHandoru) Then
                                MaeAtai = "空白"    'ＶＯ値が空なら空白を設定
                            Else
                                MaeAtai = Hvo.SeisakuHandoru & "HD"
                            End If
                            xls.SetComment("変更前の値：" & MaeAtai, _
                                            10, Hvo.HyojijunNo + lngChgData + lngStartRow, _
                                            10, Hvo.HyojijunNo + lngChgData + lngStartRow)   '変更前の値をコメントにセット
                        End If
                        '-----------------------------------------------------
                        'E/G仕様・排気量：比較して差異があればバックカラーを変更
                        '　Nothingとブランクが同居する可能性があるのでブランクに統一
                        If StringUtil.IsEmpty(Hvo.SeisakuEgHaikiryou) Then Hvo.SeisakuEgHaikiryou = ""
                        If StringUtil.IsEmpty(voBase.SeisakuEgHaikiryou) Then voBase.SeisakuEgHaikiryou = ""

                        If Not StringUtil.Equals(Hvo.SeisakuEgHaikiryou, voBase.SeisakuEgHaikiryou) Then
                            xls.SetBackColor(11, Hvo.HyojijunNo + lngChgData + lngStartRow, _
                                             11, Hvo.HyojijunNo + lngChgData + +lngStartRow, RGB(255, 153, 204))
                            strUpdateFlg = "CHANGE"
                            xls.SetValue(11, Hvo.HyojijunNo + lngChgData + lngStartRow, _
                                         11, Hvo.HyojijunNo + lngChgData + lngStartRow, voBase.SeisakuEgHaikiryou)
                            Dim MaeAtai As String = ""
                            If StringUtil.IsEmpty(Hvo.SeisakuEgHaikiryou) Then
                                MaeAtai = "空白"    'ＶＯ値が空なら空白を設定
                            Else
                                MaeAtai = Hvo.SeisakuEgHaikiryou
                            End If
                            xls.SetComment("変更前の値：" & MaeAtai, _
                                            11, Hvo.HyojijunNo + lngChgData + lngStartRow, _
                                            11, Hvo.HyojijunNo + lngChgData + lngStartRow)   '変更前の値をコメントにセット
                        End If
                        '-----------------------------------------------------
                        'E/G仕様・型式：比較して差異があればバックカラーを変更
                        '　Nothingとブランクが同居する可能性があるのでブランクに統一
                        If StringUtil.IsEmpty(Hvo.SeisakuEgKatashiki) Then Hvo.SeisakuEgKatashiki = ""
                        If StringUtil.IsEmpty(voBase.SeisakuEgKatashiki) Then voBase.SeisakuEgKatashiki = ""

                        If Not StringUtil.Equals(Hvo.SeisakuEgKatashiki, voBase.SeisakuEgKatashiki) Then
                            xls.SetBackColor(12, Hvo.HyojijunNo + lngChgData + lngStartRow, _
                                             12, Hvo.HyojijunNo + lngChgData + +lngStartRow, RGB(255, 153, 204))
                            strUpdateFlg = "CHANGE"
                            xls.SetValue(12, Hvo.HyojijunNo + lngChgData + lngStartRow, _
                                         12, Hvo.HyojijunNo + lngChgData + lngStartRow, voBase.SeisakuEgKatashiki)
                            Dim MaeAtai As String = ""
                            If StringUtil.IsEmpty(Hvo.SeisakuEgKatashiki) Then
                                MaeAtai = "空白"    'ＶＯ値が空なら空白を設定
                            Else
                                MaeAtai = Hvo.SeisakuEgKatashiki
                            End If
                            xls.SetComment("変更前の値：" & MaeAtai, _
                                            12, Hvo.HyojijunNo + lngChgData + lngStartRow, _
                                            12, Hvo.HyojijunNo + lngChgData + lngStartRow)   '変更前の値をコメントにセット
                        End If
                        '-----------------------------------------------------
                        'E/G仕様・過給器：比較して差異があればバックカラーを変更
                        '　Nothingとブランクが同居する可能性があるのでブランクに統一
                        If StringUtil.IsEmpty(Hvo.SeisakuEgKakyuuki) Then Hvo.SeisakuEgKakyuuki = ""
                        If StringUtil.IsEmpty(voBase.SeisakuEgKakyuuki) Then voBase.SeisakuEgKakyuuki = ""


                        If Not StringUtil.Equals(Hvo.SeisakuEgKakyuuki, voBase.SeisakuEgKakyuuki) Then
                            xls.SetBackColor(13, Hvo.HyojijunNo + lngChgData + lngStartRow, _
                                             13, Hvo.HyojijunNo + lngChgData + +lngStartRow, RGB(255, 153, 204))
                            strUpdateFlg = "CHANGE"
                            xls.SetValue(13, Hvo.HyojijunNo + lngChgData + lngStartRow, _
                                         13, Hvo.HyojijunNo + lngChgData + lngStartRow, voBase.SeisakuEgKakyuuki)
                            Dim MaeAtai As String = ""
                            If StringUtil.IsEmpty(Hvo.SeisakuEgKakyuuki) Then
                                MaeAtai = "空白"    'ＶＯ値が空なら空白を設定
                            Else
                                MaeAtai = Hvo.SeisakuEgKakyuuki
                            End If
                            xls.SetComment("変更前の値：" & MaeAtai, _
                                            13, Hvo.HyojijunNo + lngChgData + lngStartRow, _
                                            13, Hvo.HyojijunNo + lngChgData + lngStartRow)   '変更前の値をコメントにセット
                        End If
                        '-----------------------------------------------------
                        'T/M仕様・駆動方式：比較して差異があればバックカラーを変更
                        '　Nothingとブランクが同居する可能性があるのでブランクに統一
                        If StringUtil.IsEmpty(Hvo.SeisakuTmKudou) Then Hvo.SeisakuTmKudou = ""
                        If StringUtil.IsEmpty(voBase.SeisakuTmKudou) Then voBase.SeisakuTmKudou = ""

                        If Not StringUtil.Equals(Hvo.SeisakuTmKudou, voBase.SeisakuTmKudou) Then
                            xls.SetBackColor(14, Hvo.HyojijunNo + lngChgData + lngStartRow, _
                                             14, Hvo.HyojijunNo + lngChgData + +lngStartRow, RGB(255, 153, 204))
                            strUpdateFlg = "CHANGE"
                            xls.SetValue(14, Hvo.HyojijunNo + lngChgData + lngStartRow, _
                                         14, Hvo.HyojijunNo + lngChgData + lngStartRow, voBase.SeisakuTmKudou)
                            Dim MaeAtai As String = ""
                            If StringUtil.IsEmpty(Hvo.SeisakuTmKudou) Then
                                MaeAtai = "空白"    'ＶＯ値が空なら空白を設定
                            Else
                                MaeAtai = Hvo.SeisakuTmKudou
                            End If
                            xls.SetComment("変更前の値：" & MaeAtai, _
                                            14, Hvo.HyojijunNo + lngChgData + lngStartRow, _
                                            14, Hvo.HyojijunNo + lngChgData + lngStartRow)   '変更前の値をコメントにセット
                        End If
                        '-----------------------------------------------------
                        'T/M仕様・変速機：比較して差異があればバックカラーを変更
                        '　Nothingとブランクが同居する可能性があるのでブランクに統一
                        If StringUtil.IsEmpty(Hvo.SeisakuTmHensokuki) Then Hvo.SeisakuTmHensokuki = ""
                        If StringUtil.IsEmpty(voBase.SeisakuTmHensokuki) Then voBase.SeisakuTmHensokuki = ""

                        If Not StringUtil.Equals(Hvo.SeisakuTmHensokuki, voBase.SeisakuTmHensokuki) Then
                            xls.SetBackColor(15, Hvo.HyojijunNo + lngChgData + lngStartRow, _
                                             15, Hvo.HyojijunNo + lngChgData + +lngStartRow, RGB(255, 153, 204))
                            strUpdateFlg = "CHANGE"
                            xls.SetValue(15, Hvo.HyojijunNo + lngChgData + lngStartRow, _
                                         15, Hvo.HyojijunNo + lngChgData + lngStartRow, voBase.SeisakuTmHensokuki)
                            Dim MaeAtai As String = ""
                            If StringUtil.IsEmpty(Hvo.SeisakuTmHensokuki) Then
                                MaeAtai = "空白"    'ＶＯ値が空なら空白を設定
                            Else
                                MaeAtai = Hvo.SeisakuTmHensokuki
                            End If
                            xls.SetComment("変更前の値：" & MaeAtai, _
                                            15, Hvo.HyojijunNo + lngChgData + lngStartRow, _
                                            15, Hvo.HyojijunNo + lngChgData + lngStartRow)   '変更前の値をコメントにセット
                        End If
                        '-----------------------------------------------------
                        'アプライドNo：比較して差異があればバックカラーを変更
                        '　Nothingとブランクが同居する可能性があるのでブランクに統一
                        If StringUtil.IsEmpty(Hvo.BaseAppliedNo) Then Hvo.BaseAppliedNo = ""
                        If StringUtil.IsEmpty(voBase.BaseAppliedNo) Then voBase.BaseAppliedNo = ""

                        If Not StringUtil.Equals(Hvo.BaseAppliedNo, voBase.BaseAppliedNo) Then
                            xls.SetBackColor(16, Hvo.HyojijunNo + lngChgData + lngStartRow, _
                                             16, Hvo.HyojijunNo + lngChgData + +lngStartRow, RGB(255, 153, 204))
                            strUpdateFlg = "CHANGE"
                            xls.SetValue(16, Hvo.HyojijunNo + lngChgData + lngStartRow, _
                                         16, Hvo.HyojijunNo + lngChgData + lngStartRow, voBase.BaseAppliedNo)
                            Dim MaeAtai As String = ""
                            If StringUtil.IsEmpty(Hvo.BaseAppliedNo) Then
                                MaeAtai = "空白"    'ＶＯ値が空なら空白を設定
                            Else
                                MaeAtai = Hvo.BaseAppliedNo
                            End If
                            xls.SetComment("変更前の値：" & MaeAtai, _
                                            16, Hvo.HyojijunNo + lngChgData + lngStartRow, _
                                            16, Hvo.HyojijunNo + lngChgData + lngStartRow)   '変更前の値をコメントにセット
                        End If
                        '-----------------------------------------------------
                        '型式：比較して差異があればバックカラーを変更
                        '　Nothingとブランクが同居する可能性があるのでブランクに統一
                        If StringUtil.IsEmpty(Hvo.BaseKatashiki) Then Hvo.BaseKatashiki = ""
                        If StringUtil.IsEmpty(voBase.BaseKatashiki) Then voBase.BaseKatashiki = ""

                        If Not StringUtil.Equals(Hvo.BaseKatashiki, voBase.BaseKatashiki) Then
                            xls.SetBackColor(17, Hvo.HyojijunNo + lngChgData + lngStartRow, _
                                             17, Hvo.HyojijunNo + lngChgData + +lngStartRow, RGB(255, 153, 204))
                            strUpdateFlg = "CHANGE"
                            xls.SetValue(17, Hvo.HyojijunNo + lngChgData + lngStartRow, _
                                         17, Hvo.HyojijunNo + lngChgData + lngStartRow, voBase.BaseKatashiki)
                            Dim MaeAtai As String = ""
                            If StringUtil.IsEmpty(Hvo.BaseKatashiki) Then
                                MaeAtai = "空白"    'ＶＯ値が空なら空白を設定
                            Else
                                MaeAtai = Hvo.BaseKatashiki
                            End If
                            xls.SetComment("変更前の値：" & MaeAtai, _
                                            17, Hvo.HyojijunNo + lngChgData + lngStartRow, _
                                            17, Hvo.HyojijunNo + lngChgData + lngStartRow)   '変更前の値をコメントにセット
                        End If
                        '-----------------------------------------------------
                        '仕向：比較して差異があればバックカラーを変更
                        '　Nothingとブランクが同居する可能性があるのでブランクに統一
                        If StringUtil.IsEmpty(Hvo.BaseShimuke) Then Hvo.BaseShimuke = ""
                        If StringUtil.IsEmpty(voBase.BaseShimuke) Then voBase.BaseShimuke = ""

                        If Not StringUtil.Equals(Hvo.BaseShimuke, voBase.BaseShimuke) Then
                            xls.SetBackColor(18, Hvo.HyojijunNo + lngChgData + lngStartRow, _
                                             18, Hvo.HyojijunNo + lngChgData + +lngStartRow, RGB(255, 153, 204))
                            strUpdateFlg = "CHANGE"
                            strBaseShimuke = ""
                            If StringUtil.IsEmpty(voBase.BaseShimuke) Then
                                strBaseShimuke = "国内"
                            Else
                                strBaseShimuke = voBase.BaseShimuke
                            End If
                            xls.SetValue(18, Hvo.HyojijunNo + lngChgData + lngStartRow, _
                                         18, Hvo.HyojijunNo + lngChgData + lngStartRow, strBaseShimuke)
                            strBaseShimuke = ""
                            If StringUtil.IsEmpty(Hvo.BaseShimuke) Then
                                strBaseShimuke = "国内"
                            Else
                                strBaseShimuke = Hvo.BaseShimuke
                            End If
                            xls.SetComment("変更前の値：" & strBaseShimuke, _
                                            18, Hvo.HyojijunNo + lngChgData + lngStartRow, _
                                            18, Hvo.HyojijunNo + lngChgData + lngStartRow)   '変更前の値をコメントにセット
                        End If
                        '-----------------------------------------------------
                        'ＯＰ：比較して差異があればバックカラーを変更
                        '　Nothingとブランクが同居する可能性があるのでブランクに統一
                        If StringUtil.IsEmpty(Hvo.BaseOp) Then Hvo.BaseOp = ""
                        If StringUtil.IsEmpty(voBase.BaseOp) Then voBase.BaseOp = ""

                        If Not StringUtil.Equals(Hvo.BaseOp, voBase.BaseOp) Then
                            xls.SetBackColor(19, Hvo.HyojijunNo + lngChgData + lngStartRow, _
                                             19, Hvo.HyojijunNo + lngChgData + +lngStartRow, RGB(255, 153, 204))
                            strUpdateFlg = "CHANGE"
                            xls.SetValue(19, Hvo.HyojijunNo + lngChgData + lngStartRow, _
                                         19, Hvo.HyojijunNo + lngChgData + lngStartRow, voBase.BaseOp)
                            Dim MaeAtai As String = ""
                            If StringUtil.IsEmpty(Hvo.BaseOp) Then
                                MaeAtai = "空白"    'ＶＯ値が空なら空白を設定
                            Else
                                MaeAtai = Hvo.BaseOp
                            End If
                            xls.SetComment("変更前の値：" & MaeAtai, _
                                            19, Hvo.HyojijunNo + lngChgData + lngStartRow, _
                                            19, Hvo.HyojijunNo + lngChgData + lngStartRow)   '変更前の値をコメントにセット
                        End If
                        '-----------------------------------------------------
                        '外装色：比較して差異があればバックカラーを変更
                        '　Nothingとブランクが同居する可能性があるのでブランクに統一
                        If StringUtil.IsEmpty(Hvo.BaseGaisousyoku) Then Hvo.BaseGaisousyoku = ""
                        If StringUtil.IsEmpty(voBase.BaseGaisousyoku) Then voBase.BaseGaisousyoku = ""

                        If Not StringUtil.Equals(Hvo.BaseGaisousyoku, voBase.BaseGaisousyoku) Then
                            xls.SetBackColor(20, Hvo.HyojijunNo + lngChgData + lngStartRow, _
                                             20, Hvo.HyojijunNo + lngChgData + +lngStartRow, RGB(255, 153, 204))
                            strUpdateFlg = "CHANGE"
                            xls.SetValue(20, Hvo.HyojijunNo + lngChgData + lngStartRow, _
                                         20, Hvo.HyojijunNo + lngChgData + lngStartRow, voBase.BaseGaisousyoku)
                            Dim MaeAtai As String = ""
                            If StringUtil.IsEmpty(Hvo.BaseGaisousyoku) Then
                                MaeAtai = "空白"    'ＶＯ値が空なら空白を設定
                            Else
                                MaeAtai = Hvo.BaseGaisousyoku
                            End If
                            xls.SetComment("変更前の値：" & MaeAtai, _
                                            20, Hvo.HyojijunNo + lngChgData + lngStartRow, _
                                            20, Hvo.HyojijunNo + lngChgData + lngStartRow)   '変更前の値をコメントにセット

                            '外装色名を同様にバックカラーを変更
                            If Not StringUtil.IsEmpty(voBase.BaseGaisousyoku) Then
                                xls.SetBackColor(21, Hvo.HyojijunNo + lngChgData + lngStartRow, _
                                                 21, Hvo.HyojijunNo + lngChgData + +lngStartRow, RGB(255, 153, 204))
                                Dim GaisoName = getColorName.FindGaisouColorName(Hvo.BaseGaisousyoku)

                                Dim strName As String = "空白"
                                If StringUtil.IsNotEmpty(GaisoName) Then
                                    strName = GaisoName.ColorName
                                End If
                                If StringUtil.Equals(Hvo.BaseGaisousyoku, "-") Then
                                    strName = "電着のみ"
                                End If

                                '外装色名をエクセルに反映'
                                xls.SetComment("変更前の値：" & Trim(strName), _
                                                21, Hvo.HyojijunNo + lngChgData + lngStartRow, _
                                                21, Hvo.HyojijunNo + lngChgData + lngStartRow)   '変更前の値をコメントにセット

                                GaisoName = getColorName.FindGaisouColorName(voBase.BaseGaisousyoku)
                                strName = ""
                                If StringUtil.IsNotEmpty(GaisoName) Then
                                    strName = GaisoName.ColorName
                                End If
                                xls.SetValue(21, Hvo.HyojijunNo + lngChgData + lngStartRow, _
                                             21, Hvo.HyojijunNo + lngChgData + lngStartRow, Trim(strName))
                                If StringUtil.Equals(Hvo.BaseGaisousyoku, "-") Then
                                    xls.SetValue(21, Hvo.HyojijunNo + lngChgData + lngStartRow, _
                                                 21, Hvo.HyojijunNo + lngChgData + lngStartRow, "電着のみ")
                                End If
                            End If
                        End If
                        '-----------------------------------------------------
                        '内装色：比較して差異があればバックカラーを変更
                        '　Nothingとブランクが同居する可能性があるのでブランクに統一
                        If StringUtil.IsEmpty(Hvo.BaseNaisousyoku) Then Hvo.BaseNaisousyoku = ""
                        If StringUtil.IsEmpty(voBase.BaseNaisousyoku) Then voBase.BaseNaisousyoku = ""

                        If Not StringUtil.Equals(Hvo.BaseNaisousyoku, voBase.BaseNaisousyoku) Then
                            xls.SetBackColor(22, Hvo.HyojijunNo + lngChgData + lngStartRow, _
                                             22, Hvo.HyojijunNo + lngChgData + +lngStartRow, RGB(255, 153, 204))
                            strUpdateFlg = "CHANGE"
                            xls.SetValue(22, Hvo.HyojijunNo + lngChgData + lngStartRow, _
                                         22, Hvo.HyojijunNo + lngChgData + lngStartRow, voBase.BaseNaisousyoku)
                            Dim MaeAtai As String = ""
                            If StringUtil.IsEmpty(Hvo.BaseNaisousyoku) Then
                                MaeAtai = "空白"    'ＶＯ値が空なら空白を設定
                            Else
                                MaeAtai = Hvo.BaseNaisousyoku
                            End If
                            xls.SetComment("変更前の値：" & MaeAtai, _
                                            22, Hvo.HyojijunNo + lngChgData + lngStartRow, _
                                            22, Hvo.HyojijunNo + lngChgData + lngStartRow)   '変更前の値をコメントにセット

                            '内装色名を同様にバックカラーを変更
                            If Not StringUtil.IsEmpty(voBase.BaseNaisousyoku) Then
                                xls.SetBackColor(23, Hvo.HyojijunNo + lngChgData + lngStartRow, _
                                                 23, Hvo.HyojijunNo + lngChgData + +lngStartRow, RGB(255, 153, 204))
                                Dim NaisoName = getColorName.FindNaisouColorName(Hvo.BaseNaisousyoku)
                                Dim strName As String = "空白"
                                If StringUtil.IsNotEmpty(NaisoName) Then
                                    strName = NaisoName.ColorName
                                End If
                                '内装色名をエクセルに反映'
                                xls.SetComment("変更前の値：" & Trim(strName), _
                                                23, Hvo.HyojijunNo + lngChgData + lngStartRow, _
                                                23, Hvo.HyojijunNo + lngChgData + lngStartRow)   '変更前の値をコメントにセット

                                NaisoName = getColorName.FindNaisouColorName(voBase.BaseNaisousyoku)
                                strName = ""
                                If StringUtil.IsNotEmpty(NaisoName) Then
                                    strName = NaisoName.ColorName
                                End If
                                xls.SetValue(23, Hvo.HyojijunNo + lngChgData + lngStartRow, _
                                             23, Hvo.HyojijunNo + lngChgData + lngStartRow, Trim(strName))
                            End If
                        End If
                        '-----------------------------------------------------
                        'イベントコード：比較して差異があればバックカラーを変更
                        '　Nothingとブランクが同居する可能性があるのでブランクに統一
                        If StringUtil.IsEmpty(Hvo.ShisakuBaseEventCode) Then Hvo.ShisakuBaseEventCode = ""
                        If StringUtil.IsEmpty(voBase.ShisakuBaseEventCode) Then voBase.ShisakuBaseEventCode = ""

                        If Not StringUtil.Equals(Hvo.ShisakuBaseEventCode, voBase.ShisakuBaseEventCode) Then
                            xls.SetBackColor(24, Hvo.HyojijunNo + lngChgData + lngStartRow, _
                                             24, Hvo.HyojijunNo + lngChgData + +lngStartRow, RGB(255, 153, 204))
                            strUpdateFlg = "CHANGE"
                            xls.SetValue(24, Hvo.HyojijunNo + lngChgData + lngStartRow, _
                                         24, Hvo.HyojijunNo + lngChgData + lngStartRow, voBase.ShisakuBaseEventCode)
                            Dim MaeAtai As String = ""
                            If StringUtil.IsEmpty(Hvo.ShisakuBaseEventCode) Then
                                MaeAtai = "空白"    'ＶＯ値が空なら空白を設定
                            Else
                                MaeAtai = Hvo.ShisakuBaseEventCode
                            End If
                            xls.SetComment("変更前の値：" & MaeAtai, _
                                            24, Hvo.HyojijunNo + lngChgData + lngStartRow, _
                                            24, Hvo.HyojijunNo + lngChgData + lngStartRow)   '変更前の値をコメントにセット
                        End If
                        '-----------------------------------------------------
                        '号車：比較して差異があればバックカラーを変更
                        '　Nothingとブランクが同居する可能性があるのでブランクに統一
                        If StringUtil.IsEmpty(Hvo.ShisakuBaseGousya) Then Hvo.ShisakuBaseGousya = ""
                        If StringUtil.IsEmpty(voBase.ShisakuBaseGousya) Then voBase.ShisakuBaseGousya = ""

                        If Not StringUtil.Equals(Hvo.ShisakuBaseGousya, voBase.ShisakuBaseGousya) Then
                            xls.SetBackColor(25, Hvo.HyojijunNo + lngChgData + lngStartRow, _
                                             25, Hvo.HyojijunNo + lngChgData + +lngStartRow, RGB(255, 153, 204))
                            strUpdateFlg = "CHANGE"
                            xls.SetValue(25, Hvo.HyojijunNo + lngChgData + lngStartRow, _
                                         25, Hvo.HyojijunNo + lngChgData + lngStartRow, voBase.ShisakuBaseGousya)
                            Dim MaeAtai As String = ""
                            If StringUtil.IsEmpty(Hvo.ShisakuBaseGousya) Then
                                MaeAtai = "空白"    'ＶＯ値が空なら空白を設定
                            Else
                                MaeAtai = Hvo.ShisakuBaseGousya
                            End If
                            xls.SetComment("変更前の値：" & MaeAtai, _
                                            25, Hvo.HyojijunNo + lngChgData + lngStartRow, _
                                            25, Hvo.HyojijunNo + lngChgData + lngStartRow)   '変更前の値をコメントにセット
                        End If
                        '-----------------------------------------------------
                        '車体№：比較して差異があればバックカラーを変更
                        '　Nothingとブランクが同居する可能性があるのでブランクに統一
                        If StringUtil.IsEmpty(Hvo.SeisakuSyataiNo) Then Hvo.SeisakuSyataiNo = ""
                        If StringUtil.IsEmpty(voBase.SeisakuSyataiNo) Then voBase.SeisakuSyataiNo = ""

                        If Not StringUtil.Equals(Hvo.SeisakuSyataiNo, voBase.SeisakuSyataiNo) Then
                            xls.SetBackColor(26, Hvo.HyojijunNo + lngChgData + lngStartRow, _
                                             26, Hvo.HyojijunNo + lngChgData + +lngStartRow, RGB(255, 153, 204))
                            strUpdateFlg = "CHANGE"
                            xls.SetValue(26, Hvo.HyojijunNo + lngChgData + lngStartRow, _
                                         26, Hvo.HyojijunNo + lngChgData + lngStartRow, voBase.SeisakuSyataiNo)
                            Dim MaeAtai As String = ""
                            If StringUtil.IsEmpty(Hvo.SeisakuSyataiNo) Then
                                MaeAtai = "空白"    'ＶＯ値が空なら空白を設定
                            Else
                                MaeAtai = Hvo.SeisakuSyataiNo
                            End If
                            xls.SetComment("変更前の値：" & MaeAtai, _
                                            26, Hvo.HyojijunNo + lngChgData + lngStartRow, _
                                            26, Hvo.HyojijunNo + lngChgData + lngStartRow)   '変更前の値をコメントにセット
                        End If

                        If StringUtil.Equals(voBase.ShisakuSyubetu, "D") Then
                            '前回にあって今回にないので削除データとみなす。そしてシンボルへ削除をセット。
                            xls.SetValue(1, Hvo.HyojijunNo + lngChgData + lngStartRow, _
                                         1, Hvo.HyojijunNo + lngChgData + lngStartRow, "削除")
                            '-----------------------------------------------------
                            '背景色を変更
                            '   ～特別装備仕様の最後の列まで
                            xls.SetBackColor(2, Hvo.HyojijunNo + lngChgData + lngStartRow, _
                                             26, Hvo.HyojijunNo + lngChgData + lngStartRow, RGB(192, 192, 192))    '削除カラー
                            '-----------------------------------------------------
                            strUpdateFlg = ""   'クリア
                        End If
                        '種別
                        xls.SetValue(3, Hvo.HyojijunNo + lngChgData + lngStartRow, _
                                     3, Hvo.HyojijunNo + lngChgData + lngStartRow, voBase.ShisakuSyubetu)

                        ''変更行のために１カウントＵＰ
                        'If StringUtil.Equals(strUpdateFlg, "CHANGE") Then lngChgData = lngChgData + 1

                        If strUpdateFlg = "CHANGE" Then
                            baseUpdateFlg = "UPD"
                            '今回にあって前回にもあって変更箇所があるならシンボルへ「変更」をセット。
                            xls.SetValue(1, Hvo.HyojijunNo + lngChgData + lngStartRow, _
                                         1, Hvo.HyojijunNo + lngChgData + lngStartRow, "変更")
                        End If
                    End If
                End If
                '=================================================================================================================
            Next

            ''追加された号車が無いか確認する。
            'If StringUtil.IsNotEmpty(HikakuEventCode) = True Then
            '    '
            '    For Each vo As TShisakuEventBaseSeisakuIchiranVo In baseList   '今回ベース車情報
            '        'カウントクリア
            '        lngChgDataCnt = 0
            '        '-----------------------------------------------------
            '        '比較対象のイベントを号車で抽出する。
            '        If HbaseList.Count <> 0 Then
            '            For Each Hvo As TShisakuEventBaseKaiteiVo In HbaseList   '前回ベース車情報
            '                If StringUtil.Equals(Hvo.ShisakuGousya, vo.ShisakuGousya) And _
            '                   StringUtil.Equals(Hvo.ShisakuSyubetu, vo.ShisakuSyubetu) Then
            '                    voBase = vo
            '                    lngChgDataCnt = lngChgDataCnt + 1
            '                    Exit For
            '                End If
            '            Next
            '        End If
            '        '-----------------------------------------------------

            '        '=================================================================================================================
            '        '号車が今回改訂№にあって、比較改訂№にない場合は追加として出力する。
            '        If lngChgDataCnt = 0 Then
            '            baseUpdateFlg = "UPD"
            '            'データ件数カウント
            '            lngDataCnt = lngDataCnt + 1

            '            '-----------------------------------------------------
            '            '連番
            '            xls.SetValue(2, lngDataCnt + lngChgData + lngStartRow, _
            '                         2, lngDataCnt + lngChgData + lngStartRow, lngDataCnt)
            '            '種別
            '            xls.SetValue(3, lngDataCnt + lngChgData + lngStartRow, _
            '                         3, lngDataCnt + lngChgData + lngStartRow, vo.ShisakuSyubetu)
            '            '号車
            '            xls.SetValue(4, lngDataCnt + lngChgData + lngStartRow, _
            '                         4, lngDataCnt + lngChgData + lngStartRow, vo.ShisakuGousya)
            '            '開発符号
            '            xls.SetValue(5, lngDataCnt + lngChgData + lngStartRow, _
            '                         5, lngDataCnt + lngChgData + lngStartRow, vo.BaseKaihatsuFugo)
            '            '仕様情報№
            '            xls.SetValue(6, lngDataCnt + lngChgData + lngStartRow, _
            '                         6, lngDataCnt + lngChgData + lngStartRow, vo.BaseShiyoujyouhouNo)
            '            '車型
            '            xls.SetValue(7, lngDataCnt + lngChgData + lngStartRow, _
            '                         7, lngDataCnt + lngChgData + lngStartRow, vo.SeisakuSyasyu)
            '            'グレード
            '            xls.SetValue(8, lngDataCnt + lngChgData + lngStartRow, _
            '                         8, lngDataCnt + lngChgData + lngStartRow, vo.SeisakuGrade)
            '            '仕向地・仕向け
            '            xls.SetValue(9, lngDataCnt + lngChgData + lngStartRow, _
            '                         9, lngDataCnt + lngChgData + lngStartRow, ShimukechiShimukeHenkan(vo.SeisakuShimuke, vo.BaseShimuke))
            '            '仕向地・ハンドル
            '            xls.SetValue(10, lngDataCnt + lngChgData + lngStartRow, _
            '                         10, lngDataCnt + lngChgData + lngStartRow, vo.SeisakuHandoru & "HD")
            '            'E/G仕様・排気量
            '            xls.SetValue(11, lngDataCnt + lngChgData + lngStartRow, _
            '                         11, lngDataCnt + lngChgData + lngStartRow, vo.SeisakuEgHaikiryou)
            '            'E/G仕様・型式
            '            xls.SetValue(12, lngDataCnt + lngChgData + lngStartRow, _
            '                         12, lngDataCnt + lngChgData + lngStartRow, vo.SeisakuEgKatashiki)
            '            'E/G仕様・過給器
            '            Dim strKakyuuki As String = ""
            '            If StringUtil.Equals(vo.SeisakuEgKakyuuki, "B") Then
            '                strKakyuuki = "ﾀｰﾎﾞ"
            '            Else
            '                strKakyuuki = vo.SeisakuEgKakyuuki
            '            End If
            '            xls.SetValue(13, lngDataCnt + lngChgData + lngStartRow, _
            '                         13, lngDataCnt + lngChgData + lngStartRow, strKakyuuki)
            '            'T/M仕様・駆動方式
            '            xls.SetValue(14, lngDataCnt + lngChgData + lngStartRow, _
            '                         14, lngDataCnt + lngChgData + lngStartRow, vo.SeisakuTmKudou)
            '            'T/M仕様・変速機
            '            xls.SetValue(15, lngDataCnt + lngChgData + lngStartRow, _
            '                         15, lngDataCnt + lngChgData + lngStartRow, vo.SeisakuTmHensokuki)
            '            'アプライドNo
            '            xls.SetValue(16, lngDataCnt + lngChgData + lngStartRow, _
            '                         16, lngDataCnt + lngChgData + lngStartRow, vo.BaseAppliedNo)
            '            '型式
            '            xls.SetValue(17, lngDataCnt + lngChgData + lngStartRow, _
            '                         17, lngDataCnt + lngChgData + lngStartRow, vo.BaseKatashiki)
            '            '仕向
            '            Dim strBaseShimuke As String = ""
            '            If StringUtil.IsEmpty(vo.BaseShimuke) Then
            '                strBaseShimuke = "国内"
            '            Else
            '                strBaseShimuke = vo.BaseShimuke
            '            End If
            '            xls.SetValue(18, lngDataCnt + lngChgData + lngStartRow, _
            '                         18, lngDataCnt + lngChgData + lngStartRow, strBaseShimuke)
            '            'ＯＰ
            '            xls.SetValue(19, lngDataCnt + lngChgData + lngStartRow, _
            '                         19, lngDataCnt + lngChgData + lngStartRow, vo.BaseOp)
            '            '外装色
            '            xls.SetValue(20, lngDataCnt + lngChgData + lngStartRow, _
            '                         20, lngDataCnt + lngChgData + lngStartRow, vo.BaseGaisousyoku)
            '            Dim GaisoName = getColorName.FindGaisouColorName(voBase.BaseGaisousyoku)
            '            xls.SetValue(21, lngDataCnt + lngChgData + lngStartRow, _
            '                         21, lngDataCnt + lngChgData + lngStartRow, GaisoName)
            '            '内装色
            '            xls.SetValue(22, lngDataCnt + lngChgData + lngStartRow, _
            '                         22, lngDataCnt + lngChgData + lngStartRow, vo.BaseNaisousyoku)
            '            Dim NaisoName = getColorName.FindNaisouColorName(voBase.BaseNaisousyoku)
            '            xls.SetValue(23, lngDataCnt + lngChgData + lngStartRow, _
            '                         23, lngDataCnt + lngChgData + lngStartRow, NaisoName)
            '            'イベントコード
            '            xls.SetValue(24, lngDataCnt + lngChgData + lngStartRow, _
            '                         24, lngDataCnt + lngChgData + lngStartRow, vo.ShisakuBaseEventCode)
            '            '号車
            '            xls.SetValue(25, lngDataCnt + lngChgData + lngStartRow, _
            '                         25, lngDataCnt + lngChgData + lngStartRow, vo.ShisakuBaseGousya)
            '            '車体№
            '            xls.SetValue(26, lngDataCnt + lngChgData + lngStartRow, _
            '                         26, lngDataCnt + lngChgData + lngStartRow, vo.SeisakuSyataiNo)
            '            '-----------------------------------------------------

            '            '-----------------------------------------------------
            '            '背景色を変更
            '            '   開発符号～最後の列まで
            '            xls.SetBackColor(2, lngDataCnt + lngChgData + lngStartRow, _
            '                             26, lngDataCnt + lngChgData + lngStartRow, RGB(204, 255, 204))   '追加カラー
            '            '-----------------------------------------------------
            '            'シンボル更新
            '            '今回にあって前回にないので追加データとみなす。そしてシンボルへ「追加」をセット。
            '            xls.SetValue(1, lngDataCnt + lngChgData + lngStartRow, _
            '                         1, lngDataCnt + lngChgData + lngStartRow, "追加")

            '            'ベース車VOにも追加データを更新しておく
            '            '入力用VO定義
            '            Dim HldVoBase As New TShisakuEventBaseSeisakuIchiranVo
            '            HldVoBase = vo
            '            HldVoBase.HyojijunNo = lngDataCnt
            '            baseList.Add(HldVoBase)    '挿入

            '        End If
            '        '===============================================================================================
            '    Next
            'End If

            '罫線を引く
            xls.SetLine(1, 8, 26, lngStartRow + lngDataCnt - 1, XlBordersIndex.xlEdgeTop, XlLineStyle.xlContinuous, XlBorderWeight.xlThin) '上
            xls.SetLine(1, 8, 26, lngStartRow + lngDataCnt - 1, XlBordersIndex.xlEdgeBottom, XlLineStyle.xlContinuous, XlBorderWeight.xlThin) '下
            xls.SetLine(1, 8, 26, lngStartRow + lngDataCnt - 1, XlBordersIndex.xlEdgeLeft, XlLineStyle.xlContinuous, XlBorderWeight.xlThin) '左
            xls.SetLine(1, 8, 26, lngStartRow + lngDataCnt - 1, XlBordersIndex.xlEdgeRight, XlLineStyle.xlContinuous, XlBorderWeight.xlThin) '右
            xls.SetLine(1, 8, 26, lngStartRow + lngDataCnt - 1, XlBordersIndex.xlInsideHorizontal, XlLineStyle.xlContinuous, XlBorderWeight.xlThin) '水平
            xls.SetLine(1, 8, 26, lngStartRow + lngDataCnt - 1, XlBordersIndex.xlInsideVertical, XlLineStyle.xlContinuous, XlBorderWeight.xlThin) '垂直

            Return baseUpdateFlg

        End Function

        ''' <summary>
        ''' タイトル部作成（完成車情報）
        ''' </summary>
        ''' <param name="xls"></param>
        ''' <remarks></remarks>
        Private Sub setSheetTitleKANSEI(ByVal xls As ShisakuExcel)

            'xls.SetValueのパラメータは－－－＞　startCol ,startRow ,endCol ,endRow ,value

            'シンボル
            xls.SetValue(1, 9, 1, 9, "シンボル")
            xls.MergeCells(1, 9, 1, 10, True)
            xls.SetOrientation(1, 9, 1, 9, ShisakuExcel.XlOrientation.xlHorizontal, False, True)   '横
            xls.SetAlignment(1, 9, 1, 9, XlHAlign.xlHAlignLeft, XlVAlign.xlVAlignBottom, False, True) '位置
            '連番
            xls.SetValue(2, 9, 2, 9, "連番")
            xls.MergeCells(2, 9, 2, 10, True)
            xls.SetOrientation(2, 9, 2, 9, ShisakuExcel.XlOrientation.xlHorizontal, False, True)   '横
            xls.SetAlignment(2, 9, 2, 9, XlHAlign.xlHAlignLeft, XlVAlign.xlVAlignBottom, False, True) '位置
            '種別
            xls.SetValue(3, 9, 3, 9, "種別")
            xls.MergeCells(3, 9, 3, 10, True)
            xls.SetOrientation(3, 9, 3, 9, ShisakuExcel.XlOrientation.xlHorizontal, False, True)   '横
            xls.SetAlignment(3, 9, 3, 9, XlHAlign.xlHAlignLeft, XlVAlign.xlVAlignBottom, False, True) '位置
            '号車
            xls.SetValue(4, 9, 4, 9, "号車")
            xls.MergeCells(4, 9, 4, 10, True)
            xls.SetOrientation(4, 9, 4, 9, ShisakuExcel.XlOrientation.xlHorizontal, False, True)   '横
            xls.SetAlignment(4, 9, 4, 9, XlHAlign.xlHAlignLeft, XlVAlign.xlVAlignBottom, False, True) '位置
            '車型
            xls.SetValue(5, 9, 5, 9, "車型")
            xls.MergeCells(5, 9, 5, 10, True)
            xls.SetOrientation(5, 9, 5, 9, ShisakuExcel.XlOrientation.xlHorizontal, False, True)   '横
            xls.SetAlignment(5, 9, 5, 9, XlHAlign.xlHAlignLeft, XlVAlign.xlVAlignBottom, False, True) '位置
            'グレード
            xls.SetValue(6, 9, 6, 9, "グレード")
            xls.MergeCells(6, 9, 6, 10, True)
            xls.SetOrientation(6, 9, 6, 9, ShisakuExcel.XlOrientation.xlHorizontal, False, True)   '横
            xls.SetAlignment(6, 9, 6, 9, XlHAlign.xlHAlignLeft, XlVAlign.xlVAlignBottom, False, True) '位置

            '仕向地
            xls.SetValue(7, 9, 7, 9, "仕向地")
            xls.MergeCells(7, 9, 8, 9, True) '後で設定する。
            xls.SetOrientation(7, 9, 7, 9, ShisakuExcel.XlOrientation.xlHorizontal, False, True)   '横
            xls.SetAlignment(7, 9, 7, 9, XlHAlign.xlHAlignLeft, XlVAlign.xlVAlignBottom, False, True) '位置
            '仕向け
            xls.SetValue(7, 10, 7, 10, "仕向け")
            xls.SetOrientation(7, 10, 7, 10, ShisakuExcel.XlOrientation.xlHorizontal, False, True)   '横
            xls.SetAlignment(7, 10, 7, 10, XlHAlign.xlHAlignLeft, XlVAlign.xlVAlignBottom, False, True) '位置
            'ハンドル
            xls.SetValue(8, 10, 8, 10, "ハンドル")
            xls.SetOrientation(8, 10, 8, 10, ShisakuExcel.XlOrientation.xlHorizontal, False, True)   '横
            xls.SetAlignment(8, 10, 8, 10, XlHAlign.xlHAlignLeft, XlVAlign.xlVAlignBottom, False, True) '位置
            'E/G
            xls.SetValue(9, 9, 9, 9, "E/G")
            xls.MergeCells(9, 9, 14, 9, True)
            xls.SetOrientation(9, 9, 9, 9, ShisakuExcel.XlOrientation.xlHorizontal, False, True)   '横
            xls.SetAlignment(9, 9, 9, 9, XlHAlign.xlHAlignLeft, XlVAlign.xlVAlignBottom, False, True) '位置
            '型式
            xls.SetValue(9, 10, 9, 10, "型式")
            xls.SetOrientation(9, 10, 9, 10, ShisakuExcel.XlOrientation.xlHorizontal, False, True)   '横
            xls.SetAlignment(9, 10, 9, 10, XlHAlign.xlHAlignLeft, XlVAlign.xlVAlignBottom, False, True) '位置
            '排気量
            xls.SetValue(10, 10, 10, 10, "排気量")
            xls.SetOrientation(10, 10, 10, 10, ShisakuExcel.XlOrientation.xlHorizontal, False, True)   '横
            xls.SetAlignment(10, 10, 10, 10, XlHAlign.xlHAlignLeft, XlVAlign.xlVAlignBottom, False, True) '位置
            'システム
            xls.SetValue(11, 10, 11, 10, "システム")
            xls.SetOrientation(11, 10, 11, 10, ShisakuExcel.XlOrientation.xlHorizontal, False, True)   '横
            xls.SetAlignment(11, 10, 11, 10, XlHAlign.xlHAlignLeft, XlVAlign.xlVAlignBottom, False, True) '位置
            '過給機
            xls.SetValue(12, 10, 12, 10, "過給機")
            xls.SetOrientation(12, 10, 12, 10, ShisakuExcel.XlOrientation.xlHorizontal, False, True)   '横
            xls.SetAlignment(12, 10, 12, 10, XlHAlign.xlHAlignLeft, XlVAlign.xlVAlignBottom, False, True) '位置
            'メモ１
            xls.SetValue(13, 10, 13, 10, strEgMemo1)
            xls.SetOrientation(13, 10, 13, 10, ShisakuExcel.XlOrientation.xlHorizontal, False, True)   '横
            xls.SetAlignment(13, 10, 13, 10, XlHAlign.xlHAlignLeft, XlVAlign.xlVAlignBottom, False, True) '位置
            'メモ２
            xls.SetValue(14, 10, 14, 10, strEgMemo2)
            xls.SetOrientation(14, 10, 14, 10, ShisakuExcel.XlOrientation.xlHorizontal, False, True)   '横
            xls.SetAlignment(14, 10, 14, 10, XlHAlign.xlHAlignLeft, XlVAlign.xlVAlignBottom, False, True) '位置
            'T/M
            xls.SetValue(15, 9, 15, 9, "T/M")
            xls.MergeCells(15, 9, 19, 9, True)
            xls.SetOrientation(15, 9, 15, 9, ShisakuExcel.XlOrientation.xlHorizontal, False, True)   '横
            xls.SetAlignment(15, 9, 15, 9, XlHAlign.xlHAlignLeft, XlVAlign.xlVAlignBottom, False, True) '位置
            '駆動
            xls.SetValue(15, 10, 15, 10, "駆動")
            xls.SetOrientation(15, 10, 15, 10, ShisakuExcel.XlOrientation.xlHorizontal, False, True)   '横
            xls.SetAlignment(15, 10, 15, 10, XlHAlign.xlHAlignLeft, XlVAlign.xlVAlignBottom, False, True) '位置
            '変速機
            xls.SetValue(16, 10, 16, 10, "変速機")
            xls.SetOrientation(16, 10, 16, 10, ShisakuExcel.XlOrientation.xlHorizontal, False, True)   '横
            xls.SetAlignment(16, 10, 16, 10, XlHAlign.xlHAlignLeft, XlVAlign.xlVAlignBottom, False, True) '位置
            '副変速
            xls.SetValue(17, 10, 17, 10, "副変速")
            xls.SetOrientation(17, 10, 17, 10, ShisakuExcel.XlOrientation.xlHorizontal, False, True)   '横
            xls.SetAlignment(17, 10, 17, 10, XlHAlign.xlHAlignLeft, XlVAlign.xlVAlignBottom, False, True) '位置
            'メモ１
            xls.SetValue(18, 10, 18, 10, strTmMemo1)
            xls.SetOrientation(18, 10, 18, 10, ShisakuExcel.XlOrientation.xlHorizontal, False, True)   '横
            xls.SetAlignment(18, 10, 18, 10, XlHAlign.xlHAlignLeft, XlVAlign.xlVAlignBottom, False, True) '位置
            'メモ２
            xls.SetValue(19, 10, 19, 10, strTmMemo2)
            xls.SetOrientation(10, 13, 19, 10, ShisakuExcel.XlOrientation.xlHorizontal, False, True)   '横
            xls.SetAlignment(19, 10, 19, 10, XlHAlign.xlHAlignLeft, XlVAlign.xlVAlignBottom, False, True) '位置

            '型式
            xls.SetValue(20, 9, 20, 9, "型式")
            xls.MergeCells(20, 9, 20, 10, True)
            xls.SetOrientation(20, 9, 20, 9, ShisakuExcel.XlOrientation.xlHorizontal, False, True)   '横
            xls.SetAlignment(20, 9, 20, 9, XlHAlign.xlHAlignLeft, XlVAlign.xlVAlignBottom, False, True) '位置
            '仕向
            xls.SetValue(21, 9, 21, 9, "仕向")
            xls.MergeCells(21, 9, 21, 10, True)
            xls.SetOrientation(21, 9, 21, 9, ShisakuExcel.XlOrientation.xlHorizontal, False, True)   '横
            xls.SetAlignment(21, 9, 21, 9, XlHAlign.xlHAlignLeft, XlVAlign.xlVAlignBottom, False, True) '位置
            'ＯＰ
            xls.SetValue(22, 9, 22, 9, "ＯＰ")
            xls.MergeCells(22, 9, 22, 10, True)
            xls.SetOrientation(22, 9, 22, 9, ShisakuExcel.XlOrientation.xlHorizontal, False, True)   '横
            xls.SetAlignment(22, 9, 22, 9, XlHAlign.xlHAlignLeft, XlVAlign.xlVAlignBottom, False, True) '位置
            '外装色
            xls.SetValue(23, 9, 23, 9, "外装色")
            xls.MergeCells(23, 9, 23, 10, True)
            xls.SetOrientation(23, 9, 23, 9, ShisakuExcel.XlOrientation.xlHorizontal, False, True)   '横
            xls.SetAlignment(23, 9, 23, 9, XlHAlign.xlHAlignLeft, XlVAlign.xlVAlignBottom, False, True) '位置
            '外装色名
            xls.SetValue(24, 9, 24, 9, "外装色名")
            xls.MergeCells(24, 9, 24, 10, True)
            xls.SetOrientation(24, 9, 24, 9, ShisakuExcel.XlOrientation.xlHorizontal, False, True)   '横
            xls.SetAlignment(24, 9, 24, 9, XlHAlign.xlHAlignLeft, XlVAlign.xlVAlignBottom, False, True) '位置
            '内装色
            xls.SetValue(25, 9, 25, 9, "内装色")
            xls.MergeCells(25, 9, 25, 10, True)
            xls.SetOrientation(25, 9, 25, 9, ShisakuExcel.XlOrientation.xlHorizontal, False, True)   '横
            xls.SetAlignment(25, 9, 25, 9, XlHAlign.xlHAlignLeft, XlVAlign.xlVAlignBottom, False, True) '位置
            '内装色名
            xls.SetValue(26, 9, 26, 9, "内装色名")
            xls.MergeCells(26, 9, 26, 10, True)
            xls.SetOrientation(26, 9, 26, 9, ShisakuExcel.XlOrientation.xlHorizontal, False, True)   '横
            xls.SetAlignment(26, 9, 26, 9, XlHAlign.xlHAlignLeft, XlVAlign.xlVAlignBottom, False, True) '位置
            '車体№
            xls.SetValue(27, 9, 27, 9, "車体№")
            xls.MergeCells(27, 9, 27, 10, True)
            xls.SetOrientation(27, 9, 27, 9, ShisakuExcel.XlOrientation.xlHorizontal, False, True)   '横
            xls.SetAlignment(27, 9, 27, 9, XlHAlign.xlHAlignLeft, XlVAlign.xlVAlignBottom, False, True) '位置
            '使用目的
            xls.SetValue(28, 9, 28, 9, "使用目的")
            xls.MergeCells(28, 9, 28, 10, True)
            xls.SetOrientation(28, 9, 28, 9, ShisakuExcel.XlOrientation.xlHorizontal, False, True)   '横
            xls.SetAlignment(28, 9, 28, 9, XlHAlign.xlHAlignLeft, XlVAlign.xlVAlignBottom, False, True) '位置
            '主要確認項目
            xls.SetValue(29, 9, 29, 9, "主要確認項目")
            xls.MergeCells(29, 9, 29, 10, True)
            xls.SetOrientation(29, 9, 29, 9, ShisakuExcel.XlOrientation.xlHorizontal, False, True)   '横
            xls.SetAlignment(29, 9, 29, 9, XlHAlign.xlHAlignLeft, XlVAlign.xlVAlignBottom, False, True) '位置
            '使用部署
            xls.SetValue(30, 9, 30, 9, "使用部署")
            xls.MergeCells(30, 9, 30, 10, True)
            xls.SetOrientation(30, 9, 30, 9, ShisakuExcel.XlOrientation.xlHorizontal, False, True)   '横
            xls.SetAlignment(30, 9, 30, 9, XlHAlign.xlHAlignLeft, XlVAlign.xlVAlignBottom, False, True) '位置
            'グループ
            xls.SetValue(31, 9, 31, 9, "グループ")
            xls.MergeCells(31, 9, 31, 10, True)
            xls.SetOrientation(31, 9, 31, 9, ShisakuExcel.XlOrientation.xlHorizontal, False, True)   '横
            xls.SetAlignment(31, 9, 31, 9, XlHAlign.xlHAlignLeft, XlVAlign.xlVAlignBottom, False, True) '位置
            '製作・製作順序
            xls.SetValue(32, 9, 32, 9, "製作・製作順序")
            xls.MergeCells(32, 9, 32, 10, True)
            xls.SetOrientation(32, 9, 32, 9, ShisakuExcel.XlOrientation.xlHorizontal, False, True)   '横
            xls.SetAlignment(32, 9, 32, 9, XlHAlign.xlHAlignLeft, XlVAlign.xlVAlignBottom, False, True) '位置
            '完成日
            xls.SetValue(33, 9, 33, 9, "完成日")
            xls.MergeCells(33, 9, 33, 10, True)
            xls.SetOrientation(33, 9, 33, 9, ShisakuExcel.XlOrientation.xlHorizontal, False, True)   '横
            xls.SetAlignment(33, 9, 33, 9, XlHAlign.xlHAlignLeft, XlVAlign.xlVAlignBottom, False, True) '位置
            '工指No.
            xls.SetValue(34, 9, 34, 9, "工指No.")
            xls.MergeCells(34, 9, 34, 10, True)
            xls.SetOrientation(34, 9, 34, 9, ShisakuExcel.XlOrientation.xlHorizontal, False, True)   '横
            xls.SetAlignment(34, 9, 34, 9, XlHAlign.xlHAlignLeft, XlVAlign.xlVAlignBottom, False, True) '位置
            '製作方法区分
            xls.SetValue(35, 9, 35, 9, "製作方法区分")
            xls.MergeCells(35, 9, 35, 10, True)
            xls.SetOrientation(35, 9, 35, 9, ShisakuExcel.XlOrientation.xlHorizontal, False, True)   '横
            xls.SetAlignment(35, 9, 35, 9, XlHAlign.xlHAlignLeft, XlVAlign.xlVAlignBottom, False, True) '位置
            '製作方法
            xls.SetValue(36, 9, 36, 9, "製作方法")
            xls.MergeCells(36, 9, 36, 10, True)
            xls.SetOrientation(36, 9, 36, 9, ShisakuExcel.XlOrientation.xlHorizontal, False, True)   '横
            xls.SetAlignment(36, 9, 36, 9, XlHAlign.xlHAlignLeft, XlVAlign.xlVAlignBottom, False, True) '位置
            'メモ欄
            xls.SetValue(37, 9, 37, 9, "メモ欄")
            xls.MergeCells(37, 9, 37, 10, True)
            xls.SetOrientation(37, 9, 37, 9, ShisakuExcel.XlOrientation.xlHorizontal, False, True)   '横
            xls.SetAlignment(37, 9, 37, 9, XlHAlign.xlHAlignLeft, XlVAlign.xlVAlignBottom, False, True) '位置

            '完成車情報
            xls.SetValue(1, 8, 37, 8, "完成車情報")
            xls.MergeCells(1, 8, 37, 8, True)
            xls.SetOrientation(1, 8, 37, 8, ShisakuExcel.XlOrientation.xlHorizontal, False, True)   '横
            xls.SetAlignment(1, 8, 37, 8, XlHAlign.xlHAlignCenter, XlVAlign.xlVAlignBottom, False, True) '位置

        End Sub

        ''' <summary>
        ''' EXCEL出力・ボディーパート(完成車シート)
        ''' </summary>
        ''' <param name="xls"></param>
        ''' <remarks></remarks>
        Private Function setSheet1BodyPartKANSEI(ByVal xls As ShisakuExcel) As String

            'xls.SetValueのパラメータは－－－＞　startCol ,startRow ,endCol ,endRow ,value
            '初期設定
            Dim lngDataCnt As Long = 0
            Dim lngChgDataCnt As Long = 0
            Dim lngChgData As Long = 0
            Dim voKansei As New TShisakuEventKanseiVo
            Dim getColorName As New EventEdit.Dao.EventEditBaseCarDaoImpl
            'ベース車情報
            Dim BaseVo As TShisakuEventBaseSeisakuIchiranVo
            Dim HBaseVo As TShisakuEventBaseKaiteiVo
            Dim eventBaseCarDao As EventEditCompleteCarRirekiDao = New EventEditCompleteCarRirekiDaoImpl()

            '戻り値
            '   変更無時はブランク
            Dim kanseiUpdateFlg As String = ""

            '   完成車情報ＥＸＣＥＬ貼り付け
            '       比較元の情報より貼り付ける。
            For Each Hvo As TShisakuEventKanseiKaiteiVo In HkanseiList   '前回の完成車情報

                '変更フラグとして使用
                Dim strUpdateFlg As String = Nothing
                'データ件数カウント
                lngDataCnt = lngDataCnt + 1

                '-----------------------------------------------------
                'ベース車の情報を取得する。（前回登録）
                HBaseVo = eventBaseCarDao.FindShisakuEventBaseKaiteiCar(Hvo.ShisakuEventCode, Hvo.HyojijunNo)
                '-----------------------------------------------------
                'シンボル

                '連番
                xls.SetValue(2, Hvo.HyojijunNo + lngChgData + lngStartRow, _
                             2, Hvo.HyojijunNo + lngChgData + lngStartRow, Hvo.HyojijunNo + 1)
                '種別
                xls.SetValue(3, Hvo.HyojijunNo + lngChgData + lngStartRow, _
                             3, Hvo.HyojijunNo + lngChgData + lngStartRow, HBaseVo.ShisakuSyubetu)
                '号車
                xls.SetValue(4, Hvo.HyojijunNo + lngChgData + lngStartRow, _
                             4, Hvo.HyojijunNo + lngChgData + lngStartRow, HBaseVo.ShisakuGousya)
                '車型
                xls.SetValue(5, Hvo.HyojijunNo + lngChgData + lngStartRow, _
                             5, Hvo.HyojijunNo + lngChgData + lngStartRow, Hvo.ShisakuSyagata)
                'グレード
                xls.SetValue(6, Hvo.HyojijunNo + lngChgData + lngStartRow, _
                             6, Hvo.HyojijunNo + lngChgData + lngStartRow, Hvo.ShisakuGrade)
                '仕向け
                xls.SetValue(7, Hvo.HyojijunNo + lngChgData + lngStartRow, _
                             7, Hvo.HyojijunNo + lngChgData + lngStartRow, Hvo.ShisakuShimukechiShimuke)
                'ハンドル
                xls.SetValue(8, Hvo.HyojijunNo + lngChgData + lngStartRow, _
                             8, Hvo.HyojijunNo + lngChgData + lngStartRow, Hvo.ShisakuHandoru)
                '型式
                xls.SetValue(9, Hvo.HyojijunNo + lngChgData + lngStartRow, _
                             9, Hvo.HyojijunNo + lngChgData + lngStartRow, Hvo.ShisakuEgKatashiki)
                '排気量
                xls.SetValue(10, Hvo.HyojijunNo + lngChgData + lngStartRow, _
                             10, Hvo.HyojijunNo + lngChgData + lngStartRow, Hvo.ShisakuEgHaikiryou)
                'システム
                xls.SetValue(11, Hvo.HyojijunNo + lngChgData + lngStartRow, _
                             11, Hvo.HyojijunNo + lngChgData + lngStartRow, Hvo.ShisakuEgSystem)
                '過給機
                xls.SetValue(12, Hvo.HyojijunNo + lngChgData + lngStartRow, _
                             12, Hvo.HyojijunNo + lngChgData + lngStartRow, Hvo.ShisakuEgKakyuuki)
                'メモ１
                xls.SetValue(13, Hvo.HyojijunNo + lngChgData + lngStartRow, _
                             13, Hvo.HyojijunNo + lngChgData + lngStartRow, Hvo.ShisakuEgMemo1)
                'メモ２
                xls.SetValue(14, Hvo.HyojijunNo + lngChgData + lngStartRow, _
                             14, Hvo.HyojijunNo + lngChgData + lngStartRow, Hvo.ShisakuEgMemo2)
                'T/M仕様・駆動方式
                xls.SetValue(15, Hvo.HyojijunNo + lngChgData + lngStartRow, _
                             15, Hvo.HyojijunNo + lngChgData + lngStartRow, Hvo.ShisakuTmKudou)
                'T/M仕様・変速機
                xls.SetValue(16, Hvo.HyojijunNo + lngChgData + lngStartRow, _
                             16, Hvo.HyojijunNo + lngChgData + lngStartRow, Hvo.ShisakuTmHensokuki)
                '副変速
                xls.SetValue(17, Hvo.HyojijunNo + lngChgData + lngStartRow, _
                             17, Hvo.HyojijunNo + lngChgData + lngStartRow, Hvo.ShisakuTmFukuHensokuki)
                'メモ１
                xls.SetValue(18, Hvo.HyojijunNo + lngChgData + lngStartRow, _
                             18, Hvo.HyojijunNo + lngChgData + lngStartRow, Hvo.ShisakuTmMemo1)
                'メモ２
                xls.SetValue(19, Hvo.HyojijunNo + lngChgData + lngStartRow, _
                             19, Hvo.HyojijunNo + lngChgData + lngStartRow, Hvo.ShisakuTmMemo2)
                '型式
                xls.SetValue(20, Hvo.HyojijunNo + lngChgData + lngStartRow, _
                             20, Hvo.HyojijunNo + lngChgData + lngStartRow, Hvo.ShisakuKatashiki)
                '仕向
                xls.SetValue(21, Hvo.HyojijunNo + lngChgData + lngStartRow, _
                             21, Hvo.HyojijunNo + lngChgData + lngStartRow, Hvo.ShisakuShimuke)
                'ＯＰ
                xls.SetValue(22, Hvo.HyojijunNo + lngChgData + lngStartRow, _
                             22, Hvo.HyojijunNo + lngChgData + lngStartRow, Hvo.ShisakuOp)
                '外装色
                xls.SetValue(23, Hvo.HyojijunNo + lngChgData + lngStartRow, _
                             23, Hvo.HyojijunNo + lngChgData + lngStartRow, Hvo.ShisakuGaisousyoku)
                '外装色名
                xls.SetValue(24, Hvo.HyojijunNo + lngChgData + lngStartRow, _
                             24, Hvo.HyojijunNo + lngChgData + lngStartRow, Hvo.ShisakuGaisousyokuName)
                '内装色
                xls.SetValue(25, Hvo.HyojijunNo + lngChgData + lngStartRow, _
                             25, Hvo.HyojijunNo + lngChgData + lngStartRow, Hvo.ShisakuNaisousyoku)
                '内装色名
                xls.SetValue(26, Hvo.HyojijunNo + lngChgData + lngStartRow, _
                             26, Hvo.HyojijunNo + lngChgData + lngStartRow, Hvo.ShisakuNaisousyokuName)
                '車台No.
                xls.SetValue(27, Hvo.HyojijunNo + lngChgData + lngStartRow, _
                             27, Hvo.HyojijunNo + lngChgData + lngStartRow, Hvo.ShisakuSyadaiNo)
                '使用目的
                xls.SetValue(28, Hvo.HyojijunNo + lngChgData + lngStartRow, _
                             28, Hvo.HyojijunNo + lngChgData + lngStartRow, Hvo.ShisakuShiyouMokuteki)
                '主要確認項目
                xls.SetValue(29, Hvo.HyojijunNo + lngChgData + lngStartRow, _
                             29, Hvo.HyojijunNo + lngChgData + lngStartRow, Hvo.ShisakuShikenMokuteki)
                '使用部署
                xls.SetValue(30, Hvo.HyojijunNo + lngChgData + lngStartRow, _
                             30, Hvo.HyojijunNo + lngChgData + lngStartRow, Hvo.ShisakuSiyouBusyo)
                'グループ
                xls.SetValue(31, Hvo.HyojijunNo + lngChgData + lngStartRow, _
                             31, Hvo.HyojijunNo + lngChgData + lngStartRow, Hvo.ShisakuGroup)
                '製作・製作順序
                xls.SetValue(32, Hvo.HyojijunNo + lngChgData + lngStartRow, _
                             32, Hvo.HyojijunNo + lngChgData + lngStartRow, Hvo.ShisakuSeisakuJunjyo)
                '完成日
                '完成日があれば'
                If Not Hvo.ShisakuKanseibi Is Nothing Then
                    '完成日'
                    xls.SetValue(33, Hvo.HyojijunNo + lngChgData + lngStartRow, _
                                 33, Hvo.HyojijunNo + lngChgData + lngStartRow, ShisakuComFunc.moji8Convert2Date(Hvo.ShisakuKanseibi))
                End If
                '工指No.
                xls.SetValue(34, Hvo.HyojijunNo + lngChgData + lngStartRow, _
                             34, Hvo.HyojijunNo + lngChgData + lngStartRow, Hvo.ShisakuKoushiNo)
                '製作方法区分
                xls.SetValue(35, Hvo.HyojijunNo + lngChgData + lngStartRow, _
                             35, Hvo.HyojijunNo + lngChgData + lngStartRow, Hvo.ShisakuSeisakuHouhouKbn)
                '製作方法
                xls.SetValue(36, Hvo.HyojijunNo + lngChgData + lngStartRow, _
                             36, Hvo.HyojijunNo + lngChgData + lngStartRow, Hvo.ShisakuSeisakuHouhou)
                'メモ欄
                xls.SetValue(37, Hvo.HyojijunNo + lngChgData + lngStartRow, _
                             37, Hvo.HyojijunNo + lngChgData + lngStartRow, Hvo.ShisakuMemo)

                '=================================================================================================================
                '変化点はバックカラーを変更する。
                If StringUtil.IsNotEmpty(HikakuEventCode) = True Then
                    'カウントクリア
                    lngChgDataCnt = 0
                    '-----------------------------------------------------
                    '今回取込ファイルのイベントを号車で抽出する。
                    If kanseiList.Count <> 0 Then
                        For Each vo As TShisakuEventKanseiVo In kanseiList   '今回登録時のデータ
                            '-----------------------------------------------------
                            'ベース車の情報を取得する。（今回登録）
                            BaseVo = eventBaseCarDao.FindShisakuEventBaseSeisakuIchiranCar(vo.ShisakuEventCode, vo.HyojijunNo)
                            '-----------------------------------------------------
                            Dim hvoShisakuSyubetu As String = ""
                            If StringUtil.IsEmpty(HBaseVo.ShisakuSyubetu) Then
                                hvoShisakuSyubetu = ""
                            Else
                                hvoShisakuSyubetu = HBaseVo.ShisakuSyubetu
                            End If
                            Dim voShisakuSyubetu As String = ""
                            If StringUtil.IsEmpty(BaseVo.ShisakuSyubetu) Then
                                voShisakuSyubetu = ""
                            Else
                                voShisakuSyubetu = BaseVo.ShisakuSyubetu
                            End If
                            If StringUtil.Equals(HBaseVo.ShisakuGousya, BaseVo.ShisakuGousya)  Then
                                'If StringUtil.Equals(HBaseVo.ShisakuGousya, BaseVo.ShisakuGousya) And _
                                '   StringUtil.Equals(hvoShisakuSyubetu, voShisakuSyubetu) Then
                                voKansei = vo
                                lngChgDataCnt = lngChgDataCnt + 1
                                Exit For
                            End If
                        Next
                    End If

                    'シンボル更新
                    If lngChgDataCnt = 0 Then
                        '前回にあって今回にないので削除データとみなす。そしてシンボルへ削除をセット。
                        xls.SetValue(1, Hvo.HyojijunNo + lngChgData + lngStartRow, _
                                     1, Hvo.HyojijunNo + lngChgData + lngStartRow, "削除")
                        '-----------------------------------------------------
                        '背景色を変更
                        '   ～特別装備仕様の最後の列まで
                        xls.SetBackColor(2, Hvo.HyojijunNo + lngChgData + lngStartRow, _
                                         37, Hvo.HyojijunNo + lngChgData + lngStartRow, RGB(192, 192, 192))    '削除カラー
                        '-----------------------------------------------------
                        kanseiUpdateFlg = "UPD"
                    Else
                        '-----------------------------------------------------
                        '車型：比較して差異があればバックカラーを変更
                        '　Nothingとブランクが同居する可能性があるのでブランクに統一
                        If StringUtil.IsEmpty(Hvo.ShisakuSyagata) Then Hvo.ShisakuSyagata = ""
                        If StringUtil.IsEmpty(voKansei.ShisakuSyagata) Then voKansei.ShisakuSyagata = ""

                        If Not StringUtil.Equals(Hvo.ShisakuSyagata, voKansei.ShisakuSyagata) Then
                            xls.SetBackColor(5, Hvo.HyojijunNo + lngChgData + lngStartRow, _
                                             5, Hvo.HyojijunNo + lngChgData + +lngStartRow, RGB(255, 153, 204))
                            strUpdateFlg = "CHANGE"
                            xls.SetValue(5, Hvo.HyojijunNo + lngChgData + lngStartRow, _
                                         5, Hvo.HyojijunNo + lngChgData + lngStartRow, voKansei.ShisakuSyagata)
                            Dim MaeAtai As String = ""
                            If StringUtil.IsEmpty(Hvo.ShisakuSyagata) Then
                                MaeAtai = "空白"    'ＶＯ値が空なら空白を設定
                            Else
                                MaeAtai = Hvo.ShisakuSyagata
                            End If
                            xls.SetComment("変更前の値：" & MaeAtai, _
                                            5, Hvo.HyojijunNo + lngChgData + lngStartRow, _
                                            5, Hvo.HyojijunNo + lngChgData + lngStartRow)   '変更前の値をコメントにセット
                        End If
                        '-----------------------------------------------------
                        'グレード：比較して差異があればバックカラーを変更
                        '　Nothingとブランクが同居する可能性があるのでブランクに統一
                        If StringUtil.IsEmpty(Hvo.ShisakuGrade) Then Hvo.ShisakuGrade = ""
                        If StringUtil.IsEmpty(voKansei.ShisakuGrade) Then voKansei.ShisakuGrade = ""

                        If Not StringUtil.Equals(Hvo.ShisakuGrade, voKansei.ShisakuGrade) Then
                            xls.SetBackColor(6, Hvo.HyojijunNo + lngChgData + lngStartRow, _
                                             6, Hvo.HyojijunNo + lngChgData + +lngStartRow, RGB(255, 153, 204))
                            strUpdateFlg = "CHANGE"
                            xls.SetValue(6, Hvo.HyojijunNo + lngChgData + lngStartRow, _
                                         6, Hvo.HyojijunNo + lngChgData + lngStartRow, voKansei.ShisakuGrade)
                            Dim MaeAtai As String = ""
                            If StringUtil.IsEmpty(Hvo.ShisakuGrade) Then
                                MaeAtai = "空白"    'ＶＯ値が空なら空白を設定
                            Else
                                MaeAtai = Hvo.ShisakuGrade
                            End If
                            xls.SetComment("変更前の値：" & MaeAtai, _
                                            6, Hvo.HyojijunNo + lngChgData + lngStartRow, _
                                            6, Hvo.HyojijunNo + lngChgData + lngStartRow)   '変更前の値をコメントにセット
                        End If
                        '-----------------------------------------------------
                        '仕向け：比較して差異があればバックカラーを変更
                        '　Nothingとブランクが同居する可能性があるのでブランクに統一
                        If StringUtil.IsEmpty(Hvo.ShisakuShimukechiShimuke) Then Hvo.ShisakuShimukechiShimuke = ""
                        If StringUtil.IsEmpty(voKansei.ShisakuShimukechiShimuke) Then voKansei.ShisakuShimukechiShimuke = ""

                        If Not StringUtil.Equals(Hvo.ShisakuShimukechiShimuke, voKansei.ShisakuShimukechiShimuke) Then
                            xls.SetBackColor(7, Hvo.HyojijunNo + lngChgData + lngStartRow, _
                                             7, Hvo.HyojijunNo + lngChgData + +lngStartRow, RGB(255, 153, 204))
                            strUpdateFlg = "CHANGE"
                            xls.SetValue(7, Hvo.HyojijunNo + lngChgData + lngStartRow, _
                                         7, Hvo.HyojijunNo + lngChgData + lngStartRow, voKansei.ShisakuShimukechiShimuke)
                            Dim MaeAtai As String = ""
                            If StringUtil.IsEmpty(Hvo.ShisakuShimukechiShimuke) Then
                                MaeAtai = "空白"    'ＶＯ値が空なら空白を設定
                            Else
                                MaeAtai = Hvo.ShisakuShimukechiShimuke
                            End If
                            xls.SetComment("変更前の値：" & MaeAtai, _
                                            7, Hvo.HyojijunNo + lngChgData + lngStartRow, _
                                            7, Hvo.HyojijunNo + lngChgData + lngStartRow)   '変更前の値をコメントにセット
                        End If
                        '-----------------------------------------------------
                        'ハンドル：比較して差異があればバックカラーを変更
                        '　Nothingとブランクが同居する可能性があるのでブランクに統一
                        If StringUtil.IsEmpty(Hvo.ShisakuHandoru) Then Hvo.ShisakuHandoru = ""
                        If StringUtil.IsEmpty(voKansei.ShisakuHandoru) Then voKansei.ShisakuHandoru = ""

                        If Not StringUtil.Equals(Hvo.ShisakuHandoru, voKansei.ShisakuHandoru) Then
                            xls.SetBackColor(8, Hvo.HyojijunNo + lngChgData + lngStartRow, _
                                             8, Hvo.HyojijunNo + lngChgData + +lngStartRow, RGB(255, 153, 204))
                            strUpdateFlg = "CHANGE"
                            xls.SetValue(8, Hvo.HyojijunNo + lngChgData + lngStartRow, _
                                         8, Hvo.HyojijunNo + lngChgData + lngStartRow, voKansei.ShisakuHandoru)
                            Dim MaeAtai As String = ""
                            If StringUtil.IsEmpty(Hvo.ShisakuHandoru) Then
                                MaeAtai = "空白"    'ＶＯ値が空なら空白を設定
                            Else
                                MaeAtai = Hvo.ShisakuHandoru
                            End If
                            xls.SetComment("変更前の値：" & MaeAtai, _
                                            8, Hvo.HyojijunNo + lngChgData + lngStartRow, _
                                            8, Hvo.HyojijunNo + lngChgData + lngStartRow)   '変更前の値をコメントにセット
                        End If
                        '-----------------------------------------------------
                        '型式：比較して差異があればバックカラーを変更
                        '　Nothingとブランクが同居する可能性があるのでブランクに統一
                        If StringUtil.IsEmpty(Hvo.ShisakuEgKatashiki) Then Hvo.ShisakuEgKatashiki = ""
                        If StringUtil.IsEmpty(voKansei.ShisakuEgKatashiki) Then voKansei.ShisakuEgKatashiki = ""

                        If Not StringUtil.Equals(Hvo.ShisakuEgKatashiki, voKansei.ShisakuEgKatashiki) Then
                            xls.SetBackColor(9, Hvo.HyojijunNo + lngChgData + lngStartRow, _
                                             9, Hvo.HyojijunNo + lngChgData + +lngStartRow, RGB(255, 153, 204))
                            strUpdateFlg = "CHANGE"
                            xls.SetValue(9, Hvo.HyojijunNo + lngChgData + lngStartRow, _
                                         9, Hvo.HyojijunNo + lngChgData + lngStartRow, voKansei.ShisakuEgKatashiki)
                            Dim MaeAtai As String = ""
                            If StringUtil.IsEmpty(Hvo.ShisakuEgKatashiki) Then
                                MaeAtai = "空白"    'ＶＯ値が空なら空白を設定
                            Else
                                MaeAtai = Hvo.ShisakuEgKatashiki
                            End If
                            xls.SetComment("変更前の値：" & MaeAtai, _
                                            9, Hvo.HyojijunNo + lngChgData + lngStartRow, _
                                            9, Hvo.HyojijunNo + lngChgData + lngStartRow)   '変更前の値をコメントにセット
                        End If
                        '-----------------------------------------------------
                        '排気量・ハンドル：比較して差異があればバックカラーを変更
                        '　Nothingとブランクが同居する可能性があるのでブランクに統一
                        If StringUtil.IsEmpty(Hvo.ShisakuEgHaikiryou) Then Hvo.ShisakuEgHaikiryou = ""
                        If StringUtil.IsEmpty(voKansei.ShisakuEgHaikiryou) Then voKansei.ShisakuEgHaikiryou = ""

                        If Not StringUtil.Equals(Hvo.ShisakuEgHaikiryou, voKansei.ShisakuEgHaikiryou) Then
                            xls.SetBackColor(10, Hvo.HyojijunNo + lngChgData + lngStartRow, _
                                             10, Hvo.HyojijunNo + lngChgData + +lngStartRow, RGB(255, 153, 204))
                            strUpdateFlg = "CHANGE"
                            xls.SetValue(10, Hvo.HyojijunNo + lngChgData + lngStartRow, _
                                         10, Hvo.HyojijunNo + lngChgData + lngStartRow, voKansei.ShisakuEgHaikiryou)
                            Dim MaeAtai As String = ""
                            If StringUtil.IsEmpty(Hvo.ShisakuEgHaikiryou) Then
                                MaeAtai = "空白"    'ＶＯ値が空なら空白を設定
                            Else
                                MaeAtai = Hvo.ShisakuEgHaikiryou
                            End If
                            xls.SetComment("変更前の値：" & MaeAtai, _
                                            10, Hvo.HyojijunNo + lngChgData + lngStartRow, _
                                            10, Hvo.HyojijunNo + lngChgData + lngStartRow)   '変更前の値をコメントにセット
                        End If
                        '-----------------------------------------------------
                        'システム：比較して差異があればバックカラーを変更
                        '　Nothingとブランクが同居する可能性があるのでブランクに統一
                        If StringUtil.IsEmpty(Hvo.ShisakuEgSystem) Then Hvo.ShisakuEgSystem = ""
                        If StringUtil.IsEmpty(voKansei.ShisakuEgSystem) Then voKansei.ShisakuEgSystem = ""

                        If Not StringUtil.Equals(Hvo.ShisakuEgSystem, voKansei.ShisakuEgSystem) Then
                            xls.SetBackColor(11, Hvo.HyojijunNo + lngChgData + lngStartRow, _
                                             11, Hvo.HyojijunNo + lngChgData + +lngStartRow, RGB(255, 153, 204))
                            strUpdateFlg = "CHANGE"
                            xls.SetValue(11, Hvo.HyojijunNo + lngChgData + lngStartRow, _
                                         11, Hvo.HyojijunNo + lngChgData + lngStartRow, voKansei.ShisakuEgSystem)
                            Dim MaeAtai As String = ""
                            If StringUtil.IsEmpty(Hvo.ShisakuEgSystem) Then
                                MaeAtai = "空白"    'ＶＯ値が空なら空白を設定
                            Else
                                MaeAtai = Hvo.ShisakuEgSystem
                            End If
                            xls.SetComment("変更前の値：" & MaeAtai, _
                                            11, Hvo.HyojijunNo + lngChgData + lngStartRow, _
                                            11, Hvo.HyojijunNo + lngChgData + lngStartRow)   '変更前の値をコメントにセット
                        End If
                        '-----------------------------------------------------
                        '過給機：比較して差異があればバックカラーを変更
                        '　Nothingとブランクが同居する可能性があるのでブランクに統一
                        If StringUtil.IsEmpty(Hvo.ShisakuEgKakyuuki) Then Hvo.ShisakuEgKakyuuki = ""
                        If StringUtil.IsEmpty(voKansei.ShisakuEgKakyuuki) Then voKansei.ShisakuEgKakyuuki = ""

                        If Not StringUtil.Equals(Hvo.ShisakuEgKakyuuki, voKansei.ShisakuEgKakyuuki) Then
                            xls.SetBackColor(12, Hvo.HyojijunNo + lngChgData + lngStartRow, _
                                             12, Hvo.HyojijunNo + lngChgData + +lngStartRow, RGB(255, 153, 204))
                            strUpdateFlg = "CHANGE"
                            xls.SetValue(12, Hvo.HyojijunNo + lngChgData + lngStartRow, _
                                         12, Hvo.HyojijunNo + lngChgData + lngStartRow, voKansei.ShisakuEgKakyuuki)
                            Dim MaeAtai As String = ""
                            If StringUtil.IsEmpty(Hvo.ShisakuEgKakyuuki) Then
                                MaeAtai = "空白"    'ＶＯ値が空なら空白を設定
                            Else
                                MaeAtai = Hvo.ShisakuEgKakyuuki
                            End If
                            xls.SetComment("変更前の値：" & MaeAtai, _
                                            12, Hvo.HyojijunNo + lngChgData + lngStartRow, _
                                            12, Hvo.HyojijunNo + lngChgData + lngStartRow)   '変更前の値をコメントにセット
                        End If
                        '-----------------------------------------------------
                        'E/Gメモ１：比較して差異があればバックカラーを変更
                        '　Nothingとブランクが同居する可能性があるのでブランクに統一
                        If StringUtil.IsEmpty(Hvo.ShisakuEgMemo1) Then Hvo.ShisakuEgMemo1 = ""
                        If StringUtil.IsEmpty(voKansei.ShisakuEgMemo1) Then voKansei.ShisakuEgMemo1 = ""

                        If Not StringUtil.Equals(Hvo.ShisakuEgMemo1, voKansei.ShisakuEgMemo1) Then
                            xls.SetBackColor(13, Hvo.HyojijunNo + lngChgData + lngStartRow, _
                                             13, Hvo.HyojijunNo + lngChgData + +lngStartRow, RGB(255, 153, 204))
                            strUpdateFlg = "CHANGE"
                            xls.SetValue(13, Hvo.HyojijunNo + lngChgData + lngStartRow, _
                                         13, Hvo.HyojijunNo + lngChgData + lngStartRow, voKansei.ShisakuEgMemo1)
                            Dim MaeAtai As String = ""
                            If StringUtil.IsEmpty(Hvo.ShisakuEgMemo1) Then
                                MaeAtai = "空白"    'ＶＯ値が空なら空白を設定
                            Else
                                MaeAtai = Hvo.ShisakuEgMemo1
                            End If
                            xls.SetComment("変更前の値：" & MaeAtai, _
                                            13, Hvo.HyojijunNo + lngChgData + lngStartRow, _
                                            13, Hvo.HyojijunNo + lngChgData + lngStartRow)   '変更前の値をコメントにセット
                        End If
                        '-----------------------------------------------------
                        'E/Gメモ２：比較して差異があればバックカラーを変更
                        '　Nothingとブランクが同居する可能性があるのでブランクに統一
                        If StringUtil.IsEmpty(Hvo.ShisakuEgMemo2) Then Hvo.ShisakuEgMemo2 = ""
                        If StringUtil.IsEmpty(voKansei.ShisakuEgMemo2) Then voKansei.ShisakuEgMemo2 = ""

                        If Not StringUtil.Equals(Hvo.ShisakuEgMemo2, voKansei.ShisakuEgMemo2) Then
                            xls.SetBackColor(14, Hvo.HyojijunNo + lngChgData + lngStartRow, _
                                             14, Hvo.HyojijunNo + lngChgData + +lngStartRow, RGB(255, 153, 204))
                            strUpdateFlg = "CHANGE"
                            xls.SetValue(14, Hvo.HyojijunNo + lngChgData + lngStartRow, _
                                         14, Hvo.HyojijunNo + lngChgData + lngStartRow, voKansei.ShisakuEgMemo2)
                            Dim MaeAtai As String = ""
                            If StringUtil.IsEmpty(Hvo.ShisakuEgMemo2) Then
                                MaeAtai = "空白"    'ＶＯ値が空なら空白を設定
                            Else
                                MaeAtai = Hvo.ShisakuEgMemo2
                            End If
                            xls.SetComment("変更前の値：" & MaeAtai, _
                                            14, Hvo.HyojijunNo + lngChgData + lngStartRow, _
                                            14, Hvo.HyojijunNo + lngChgData + lngStartRow)   '変更前の値をコメントにセット
                        End If
                        '-----------------------------------------------------
                        'T/M駆動：比較して差異があればバックカラーを変更
                        '　Nothingとブランクが同居する可能性があるのでブランクに統一
                        If StringUtil.IsEmpty(Hvo.ShisakuTmKudou) Then Hvo.ShisakuTmKudou = ""
                        If StringUtil.IsEmpty(voKansei.ShisakuTmKudou) Then voKansei.ShisakuTmKudou = ""

                        If Not StringUtil.Equals(Hvo.ShisakuTmKudou, voKansei.ShisakuTmKudou) Then
                            xls.SetBackColor(15, Hvo.HyojijunNo + lngChgData + lngStartRow, _
                                             15, Hvo.HyojijunNo + lngChgData + +lngStartRow, RGB(255, 153, 204))
                            strUpdateFlg = "CHANGE"
                            xls.SetValue(15, Hvo.HyojijunNo + lngChgData + lngStartRow, _
                                         15, Hvo.HyojijunNo + lngChgData + lngStartRow, voKansei.ShisakuTmKudou)
                            Dim MaeAtai As String = ""
                            If StringUtil.IsEmpty(Hvo.ShisakuTmKudou) Then
                                MaeAtai = "空白"    'ＶＯ値が空なら空白を設定
                            Else
                                MaeAtai = Hvo.ShisakuTmKudou
                            End If
                            xls.SetComment("変更前の値：" & MaeAtai, _
                                            15, Hvo.HyojijunNo + lngChgData + lngStartRow, _
                                            15, Hvo.HyojijunNo + lngChgData + lngStartRow)   '変更前の値をコメントにセット
                        End If
                        '-----------------------------------------------------
                        'T/M変速機：比較して差異があればバックカラーを変更
                        '　Nothingとブランクが同居する可能性があるのでブランクに統一
                        If StringUtil.IsEmpty(Hvo.ShisakuTmHensokuki) Then Hvo.ShisakuTmHensokuki = ""
                        If StringUtil.IsEmpty(voKansei.ShisakuTmHensokuki) Then voKansei.ShisakuTmHensokuki = ""

                        If Not StringUtil.Equals(Hvo.ShisakuTmHensokuki, voKansei.ShisakuTmHensokuki) Then
                            xls.SetBackColor(16, Hvo.HyojijunNo + lngChgData + lngStartRow, _
                                             16, Hvo.HyojijunNo + lngChgData + +lngStartRow, RGB(255, 153, 204))
                            strUpdateFlg = "CHANGE"
                            xls.SetValue(16, Hvo.HyojijunNo + lngChgData + lngStartRow, _
                                         16, Hvo.HyojijunNo + lngChgData + lngStartRow, voKansei.ShisakuTmHensokuki)
                            Dim MaeAtai As String = ""
                            If StringUtil.IsEmpty(Hvo.ShisakuTmHensokuki) Then
                                MaeAtai = "空白"    'ＶＯ値が空なら空白を設定
                            Else
                                MaeAtai = Hvo.ShisakuTmHensokuki
                            End If
                            xls.SetComment("変更前の値：" & MaeAtai, _
                                            16, Hvo.HyojijunNo + lngChgData + lngStartRow, _
                                            16, Hvo.HyojijunNo + lngChgData + lngStartRow)   '変更前の値をコメントにセット
                        End If
                        '-----------------------------------------------------
                        'T/M副変速機：比較して差異があればバックカラーを変更
                        '　Nothingとブランクが同居する可能性があるのでブランクに統一
                        If StringUtil.IsEmpty(Hvo.ShisakuTmFukuHensokuki) Then Hvo.ShisakuTmFukuHensokuki = ""
                        If StringUtil.IsEmpty(voKansei.ShisakuTmFukuHensokuki) Then voKansei.ShisakuTmFukuHensokuki = ""

                        If Not StringUtil.Equals(Hvo.ShisakuTmFukuHensokuki, voKansei.ShisakuTmFukuHensokuki) Then
                            xls.SetBackColor(17, Hvo.HyojijunNo + lngChgData + lngStartRow, _
                                             17, Hvo.HyojijunNo + lngChgData + +lngStartRow, RGB(255, 153, 204))
                            strUpdateFlg = "CHANGE"
                            xls.SetValue(17, Hvo.HyojijunNo + lngChgData + lngStartRow, _
                                         17, Hvo.HyojijunNo + lngChgData + lngStartRow, voKansei.ShisakuTmFukuHensokuki)
                            Dim MaeAtai As String = ""
                            If StringUtil.IsEmpty(Hvo.ShisakuTmFukuHensokuki) Then
                                MaeAtai = "空白"    'ＶＯ値が空なら空白を設定
                            Else
                                MaeAtai = Hvo.ShisakuTmFukuHensokuki
                            End If
                            xls.SetComment("変更前の値：" & MaeAtai, _
                                            17, Hvo.HyojijunNo + lngChgData + lngStartRow, _
                                            17, Hvo.HyojijunNo + lngChgData + lngStartRow)   '変更前の値をコメントにセット
                        End If
                        '-----------------------------------------------------
                        'T/Mメモ１：比較して差異があればバックカラーを変更
                        '　Nothingとブランクが同居する可能性があるのでブランクに統一
                        If StringUtil.IsEmpty(Hvo.ShisakuTmMemo1) Then Hvo.ShisakuTmMemo1 = ""
                        If StringUtil.IsEmpty(voKansei.ShisakuTmMemo1) Then voKansei.ShisakuTmMemo1 = ""

                        If Not StringUtil.Equals(Hvo.ShisakuTmMemo1, voKansei.ShisakuTmMemo1) Then
                            xls.SetBackColor(18, Hvo.HyojijunNo + lngChgData + lngStartRow, _
                                             18, Hvo.HyojijunNo + lngChgData + +lngStartRow, RGB(255, 153, 204))
                            strUpdateFlg = "CHANGE"
                            xls.SetValue(18, Hvo.HyojijunNo + lngChgData + lngStartRow, _
                                         18, Hvo.HyojijunNo + lngChgData + lngStartRow, voKansei.ShisakuTmMemo1)
                            Dim MaeAtai As String = ""
                            If StringUtil.IsEmpty(Hvo.ShisakuTmMemo1) Then
                                MaeAtai = "空白"    'ＶＯ値が空なら空白を設定
                            Else
                                MaeAtai = Hvo.ShisakuTmMemo1
                            End If
                            xls.SetComment("変更前の値：" & MaeAtai, _
                                            18, Hvo.HyojijunNo + lngChgData + lngStartRow, _
                                            18, Hvo.HyojijunNo + lngChgData + lngStartRow)   '変更前の値をコメントにセット
                        End If
                        '-----------------------------------------------------
                        'T/Mメモ２：比較して差異があればバックカラーを変更
                        '　Nothingとブランクが同居する可能性があるのでブランクに統一
                        If StringUtil.IsEmpty(Hvo.ShisakuTmMemo2) Then Hvo.ShisakuTmMemo2 = ""
                        If StringUtil.IsEmpty(voKansei.ShisakuTmMemo2) Then voKansei.ShisakuTmMemo2 = ""

                        If Not StringUtil.Equals(Hvo.ShisakuTmMemo2, voKansei.ShisakuTmMemo2) Then
                            xls.SetBackColor(19, Hvo.HyojijunNo + lngChgData + lngStartRow, _
                                             19, Hvo.HyojijunNo + lngChgData + +lngStartRow, RGB(255, 153, 204))
                            strUpdateFlg = "CHANGE"
                            xls.SetValue(19, Hvo.HyojijunNo + lngChgData + lngStartRow, _
                                         19, Hvo.HyojijunNo + lngChgData + lngStartRow, voKansei.ShisakuTmMemo2)
                            Dim MaeAtai As String = ""
                            If StringUtil.IsEmpty(Hvo.ShisakuTmMemo2) Then
                                MaeAtai = "空白"    'ＶＯ値が空なら空白を設定
                            Else
                                MaeAtai = Hvo.ShisakuTmMemo2
                            End If
                            xls.SetComment("変更前の値：" & MaeAtai, _
                                            19, Hvo.HyojijunNo + lngChgData + lngStartRow, _
                                            19, Hvo.HyojijunNo + lngChgData + lngStartRow)   '変更前の値をコメントにセット
                        End If
                        '-----------------------------------------------------
                        '型式：比較して差異があればバックカラーを変更
                        '　Nothingとブランクが同居する可能性があるのでブランクに統一
                        If StringUtil.IsEmpty(Hvo.ShisakuKatashiki) Then Hvo.ShisakuKatashiki = ""
                        If StringUtil.IsEmpty(voKansei.ShisakuKatashiki) Then voKansei.ShisakuKatashiki = ""

                        If Not StringUtil.Equals(Hvo.ShisakuKatashiki, voKansei.ShisakuKatashiki) Then
                            xls.SetBackColor(20, Hvo.HyojijunNo + lngChgData + lngStartRow, _
                                             20, Hvo.HyojijunNo + lngChgData + +lngStartRow, RGB(255, 153, 204))
                            strUpdateFlg = "CHANGE"
                            xls.SetValue(20, Hvo.HyojijunNo + lngChgData + lngStartRow, _
                                         20, Hvo.HyojijunNo + lngChgData + lngStartRow, voKansei.ShisakuKatashiki)
                            Dim MaeAtai As String = ""
                            If StringUtil.IsEmpty(Hvo.ShisakuKatashiki) Then
                                MaeAtai = "空白"    'ＶＯ値が空なら空白を設定
                            Else
                                MaeAtai = Hvo.ShisakuKatashiki
                            End If
                            xls.SetComment("変更前の値：" & MaeAtai, _
                                            20, Hvo.HyojijunNo + lngChgData + lngStartRow, _
                                            20, Hvo.HyojijunNo + lngChgData + lngStartRow)   '変更前の値をコメントにセット
                        End If
                        '-----------------------------------------------------
                        '仕向：比較して差異があればバックカラーを変更
                        '　Nothingとブランクが同居する可能性があるのでブランクに統一
                        If StringUtil.IsEmpty(Hvo.ShisakuShimuke) Then Hvo.ShisakuShimuke = ""
                        If StringUtil.IsEmpty(voKansei.ShisakuShimuke) Then voKansei.ShisakuShimuke = ""

                        If Not StringUtil.Equals(Hvo.ShisakuShimuke, voKansei.ShisakuShimuke) Then
                            xls.SetBackColor(21, Hvo.HyojijunNo + lngChgData + lngStartRow, _
                                             21, Hvo.HyojijunNo + lngChgData + +lngStartRow, RGB(255, 153, 204))
                            strUpdateFlg = "CHANGE"
                            xls.SetValue(21, Hvo.HyojijunNo + lngChgData + lngStartRow, _
                                         21, Hvo.HyojijunNo + lngChgData + lngStartRow, voKansei.ShisakuShimuke)
                            Dim MaeAtai As String = ""
                            If StringUtil.IsEmpty(Hvo.ShisakuShimuke) Then
                                MaeAtai = "空白"    'ＶＯ値が空なら空白を設定
                            Else
                                MaeAtai = Hvo.ShisakuShimuke
                            End If
                            xls.SetComment("変更前の値：" & MaeAtai, _
                                            21, Hvo.HyojijunNo + lngChgData + lngStartRow, _
                                            21, Hvo.HyojijunNo + lngChgData + lngStartRow)   '変更前の値をコメントにセット
                        End If
                        '-----------------------------------------------------
                        'ＯＰ：比較して差異があればバックカラーを変更
                        '　Nothingとブランクが同居する可能性があるのでブランクに統一
                        If StringUtil.IsEmpty(Hvo.ShisakuOp) Then Hvo.ShisakuOp = ""
                        If StringUtil.IsEmpty(voKansei.ShisakuOp) Then voKansei.ShisakuOp = ""

                        If Not StringUtil.Equals(Hvo.ShisakuOp, voKansei.ShisakuOp) Then
                            xls.SetBackColor(22, Hvo.HyojijunNo + lngChgData + lngStartRow, _
                                             22, Hvo.HyojijunNo + lngChgData + +lngStartRow, RGB(255, 153, 204))
                            strUpdateFlg = "CHANGE"
                            xls.SetValue(22, Hvo.HyojijunNo + lngChgData + lngStartRow, _
                                         22, Hvo.HyojijunNo + lngChgData + lngStartRow, voKansei.ShisakuOp)
                            Dim MaeAtai As String = ""
                            If StringUtil.IsEmpty(Hvo.ShisakuOp) Then
                                MaeAtai = "空白"    'ＶＯ値が空なら空白を設定
                            Else
                                MaeAtai = Hvo.ShisakuOp
                            End If
                            xls.SetComment("変更前の値：" & MaeAtai, _
                                            22, Hvo.HyojijunNo + lngChgData + lngStartRow, _
                                            22, Hvo.HyojijunNo + lngChgData + lngStartRow)   '変更前の値をコメントにセット
                        End If
                        '-----------------------------------------------------
                        '外装色：比較して差異があればバックカラーを変更
                        '　Nothingとブランクが同居する可能性があるのでブランクに統一
                        If StringUtil.IsEmpty(Hvo.ShisakuGaisousyoku) Then Hvo.ShisakuGaisousyoku = ""
                        If StringUtil.IsEmpty(voKansei.ShisakuGaisousyoku) Then voKansei.ShisakuGaisousyoku = ""

                        If Not StringUtil.Equals(Hvo.ShisakuGaisousyoku, voKansei.ShisakuGaisousyoku) Then
                            xls.SetBackColor(23, Hvo.HyojijunNo + lngChgData + lngStartRow, _
                                             23, Hvo.HyojijunNo + lngChgData + +lngStartRow, RGB(255, 153, 204))
                            strUpdateFlg = "CHANGE"
                            xls.SetValue(23, Hvo.HyojijunNo + lngChgData + lngStartRow, _
                                         23, Hvo.HyojijunNo + lngChgData + lngStartRow, voKansei.ShisakuGaisousyoku)
                            Dim MaeAtai As String = ""
                            If StringUtil.IsEmpty(Hvo.ShisakuGaisousyoku) Then
                                MaeAtai = "空白"    'ＶＯ値が空なら空白を設定
                            Else
                                MaeAtai = Hvo.ShisakuGaisousyoku
                            End If
                            xls.SetComment("変更前の値：" & MaeAtai, _
                                            23, Hvo.HyojijunNo + lngChgData + lngStartRow, _
                                            23, Hvo.HyojijunNo + lngChgData + lngStartRow)   '変更前の値をコメントにセット
                        End If
                        '-----------------------------------------------------
                        '外装色名：比較して差異があればバックカラーを変更
                        '　Nothingとブランクが同居する可能性があるのでブランクに統一
                        If StringUtil.IsEmpty(Hvo.ShisakuGaisousyokuName) Then Hvo.ShisakuGaisousyokuName = ""
                        If StringUtil.IsEmpty(voKansei.ShisakuGaisousyokuName) Then voKansei.ShisakuGaisousyokuName = ""

                        If Not StringUtil.Equals(Hvo.ShisakuGaisousyokuName, voKansei.ShisakuGaisousyokuName) Then
                            xls.SetBackColor(24, Hvo.HyojijunNo + lngChgData + lngStartRow, _
                                             24, Hvo.HyojijunNo + lngChgData + +lngStartRow, RGB(255, 153, 204))
                            strUpdateFlg = "CHANGE"
                            xls.SetValue(24, Hvo.HyojijunNo + lngChgData + lngStartRow, _
                                         24, Hvo.HyojijunNo + lngChgData + lngStartRow, voKansei.ShisakuGaisousyokuName)
                            Dim MaeAtai As String = ""
                            If StringUtil.IsEmpty(Hvo.ShisakuGaisousyokuName) Then
                                MaeAtai = "空白"    'ＶＯ値が空なら空白を設定
                            Else
                                MaeAtai = Hvo.ShisakuGaisousyokuName
                            End If
                            xls.SetComment("変更前の値：" & MaeAtai, _
                                            24, Hvo.HyojijunNo + lngChgData + lngStartRow, _
                                            24, Hvo.HyojijunNo + lngChgData + lngStartRow)   '変更前の値をコメントにセット
                        End If
                        '-----------------------------------------------------
                        '内装色：比較して差異があればバックカラーを変更
                        '　Nothingとブランクが同居する可能性があるのでブランクに統一
                        If StringUtil.IsEmpty(Hvo.ShisakuNaisousyoku) Then Hvo.ShisakuNaisousyoku = ""
                        If StringUtil.IsEmpty(voKansei.ShisakuNaisousyoku) Then voKansei.ShisakuNaisousyoku = ""

                        If Not StringUtil.Equals(Hvo.ShisakuNaisousyoku, voKansei.ShisakuNaisousyoku) Then
                            xls.SetBackColor(25, Hvo.HyojijunNo + lngChgData + lngStartRow, _
                                             25, Hvo.HyojijunNo + lngChgData + +lngStartRow, RGB(255, 153, 204))
                            strUpdateFlg = "CHANGE"
                            xls.SetValue(25, Hvo.HyojijunNo + lngChgData + lngStartRow, _
                                         25, Hvo.HyojijunNo + lngChgData + lngStartRow, voKansei.ShisakuNaisousyoku)
                            Dim MaeAtai As String = ""
                            If StringUtil.IsEmpty(Hvo.ShisakuNaisousyoku) Then
                                MaeAtai = "空白"    'ＶＯ値が空なら空白を設定
                            Else
                                MaeAtai = Hvo.ShisakuNaisousyoku
                            End If
                            xls.SetComment("変更前の値：" & MaeAtai, _
                                            25, Hvo.HyojijunNo + lngChgData + lngStartRow, _
                                            25, Hvo.HyojijunNo + lngChgData + lngStartRow)   '変更前の値をコメントにセット
                        End If
                        '-----------------------------------------------------
                        '内装色名：比較して差異があればバックカラーを変更
                        '　Nothingとブランクが同居する可能性があるのでブランクに統一
                        If StringUtil.IsEmpty(Hvo.ShisakuNaisousyokuName) Then Hvo.ShisakuNaisousyokuName = ""
                        If StringUtil.IsEmpty(voKansei.ShisakuNaisousyokuName) Then voKansei.ShisakuNaisousyokuName = ""

                        If Not StringUtil.Equals(Hvo.ShisakuNaisousyokuName, voKansei.ShisakuNaisousyokuName) Then
                            xls.SetBackColor(26, Hvo.HyojijunNo + lngChgData + lngStartRow, _
                                             26, Hvo.HyojijunNo + lngChgData + +lngStartRow, RGB(255, 153, 204))
                            strUpdateFlg = "CHANGE"
                            xls.SetValue(26, Hvo.HyojijunNo + lngChgData + lngStartRow, _
                                         26, Hvo.HyojijunNo + lngChgData + lngStartRow, voKansei.ShisakuNaisousyokuName)
                            Dim MaeAtai As String = ""
                            If StringUtil.IsEmpty(Hvo.ShisakuNaisousyokuName) Then
                                MaeAtai = "空白"    'ＶＯ値が空なら空白を設定
                            Else
                                MaeAtai = Hvo.ShisakuNaisousyokuName
                            End If
                            xls.SetComment("変更前の値：" & MaeAtai, _
                                            26, Hvo.HyojijunNo + lngChgData + lngStartRow, _
                                            26, Hvo.HyojijunNo + lngChgData + lngStartRow)   '変更前の値をコメントにセット
                        End If
                        '-----------------------------------------------------
                        '車台No.：比較して差異があればバックカラーを変更
                        '　Nothingとブランクが同居する可能性があるのでブランクに統一
                        If StringUtil.IsEmpty(Hvo.ShisakuSyadaiNo) Then Hvo.ShisakuSyadaiNo = ""
                        If StringUtil.IsEmpty(voKansei.ShisakuSyadaiNo) Then voKansei.ShisakuSyadaiNo = ""

                        If Not StringUtil.Equals(Hvo.ShisakuSyadaiNo, voKansei.ShisakuSyadaiNo) Then
                            xls.SetBackColor(27, Hvo.HyojijunNo + lngChgData + lngStartRow, _
                                             27, Hvo.HyojijunNo + lngChgData + +lngStartRow, RGB(255, 153, 204))
                            strUpdateFlg = "CHANGE"
                            xls.SetValue(27, Hvo.HyojijunNo + lngChgData + lngStartRow, _
                                         27, Hvo.HyojijunNo + lngChgData + lngStartRow, voKansei.ShisakuSyadaiNo)
                            Dim MaeAtai As String = ""
                            If StringUtil.IsEmpty(Hvo.ShisakuSyadaiNo) Then
                                MaeAtai = "空白"    'ＶＯ値が空なら空白を設定
                            Else
                                MaeAtai = Hvo.ShisakuSyadaiNo
                            End If
                            xls.SetComment("変更前の値：" & MaeAtai, _
                                            27, Hvo.HyojijunNo + lngChgData + lngStartRow, _
                                            27, Hvo.HyojijunNo + lngChgData + lngStartRow)   '変更前の値をコメントにセット
                        End If
                        '-----------------------------------------------------
                        '使用目的：比較して差異があればバックカラーを変更
                        '　Nothingとブランクが同居する可能性があるのでブランクに統一
                        If StringUtil.IsEmpty(Hvo.ShisakuShiyouMokuteki) Then Hvo.ShisakuShiyouMokuteki = ""
                        If StringUtil.IsEmpty(voKansei.ShisakuShiyouMokuteki) Then voKansei.ShisakuShiyouMokuteki = ""

                        If Not StringUtil.Equals(Hvo.ShisakuShiyouMokuteki, voKansei.ShisakuShiyouMokuteki) Then
                            xls.SetBackColor(28, Hvo.HyojijunNo + lngChgData + lngStartRow, _
                                             28, Hvo.HyojijunNo + lngChgData + +lngStartRow, RGB(255, 153, 204))
                            strUpdateFlg = "CHANGE"
                            xls.SetValue(28, Hvo.HyojijunNo + lngChgData + lngStartRow, _
                                         28, Hvo.HyojijunNo + lngChgData + lngStartRow, voKansei.ShisakuShiyouMokuteki)
                            Dim MaeAtai As String = ""
                            If StringUtil.IsEmpty(Hvo.ShisakuShiyouMokuteki) Then
                                MaeAtai = "空白"    'ＶＯ値が空なら空白を設定
                            Else
                                MaeAtai = Hvo.ShisakuShiyouMokuteki
                            End If
                            xls.SetComment("変更前の値：" & MaeAtai, _
                                            28, Hvo.HyojijunNo + lngChgData + lngStartRow, _
                                            28, Hvo.HyojijunNo + lngChgData + lngStartRow)   '変更前の値をコメントにセット
                        End If
                        '-----------------------------------------------------
                        '主要確認項目：比較して差異があればバックカラーを変更
                        '　Nothingとブランクが同居する可能性があるのでブランクに統一
                        If StringUtil.IsEmpty(Hvo.ShisakuShikenMokuteki) Then Hvo.ShisakuShikenMokuteki = ""
                        If StringUtil.IsEmpty(voKansei.ShisakuShikenMokuteki) Then voKansei.ShisakuShikenMokuteki = ""

                        If Not StringUtil.Equals(Hvo.ShisakuShikenMokuteki, voKansei.ShisakuShikenMokuteki) Then
                            xls.SetBackColor(29, Hvo.HyojijunNo + lngChgData + lngStartRow, _
                                             29, Hvo.HyojijunNo + lngChgData + +lngStartRow, RGB(255, 153, 204))
                            strUpdateFlg = "CHANGE"
                            xls.SetValue(29, Hvo.HyojijunNo + lngChgData + lngStartRow, _
                                         29, Hvo.HyojijunNo + lngChgData + lngStartRow, voKansei.ShisakuShikenMokuteki)
                            Dim MaeAtai As String = ""
                            If StringUtil.IsEmpty(Hvo.ShisakuShikenMokuteki) Then
                                MaeAtai = "空白"    'ＶＯ値が空なら空白を設定
                            Else
                                MaeAtai = Hvo.ShisakuShikenMokuteki
                            End If
                            xls.SetComment("変更前の値：" & MaeAtai, _
                                            29, Hvo.HyojijunNo + lngChgData + lngStartRow, _
                                            29, Hvo.HyojijunNo + lngChgData + lngStartRow)   '変更前の値をコメントにセット
                        End If
                        '-----------------------------------------------------
                        '使用部署：比較して差異があればバックカラーを変更
                        '　Nothingとブランクが同居する可能性があるのでブランクに統一
                        If StringUtil.IsEmpty(Hvo.ShisakuSiyouBusyo) Then Hvo.ShisakuSiyouBusyo = ""
                        If StringUtil.IsEmpty(voKansei.ShisakuSiyouBusyo) Then voKansei.ShisakuSiyouBusyo = ""

                        If Not StringUtil.Equals(Hvo.ShisakuSiyouBusyo, voKansei.ShisakuSiyouBusyo) Then
                            xls.SetBackColor(30, Hvo.HyojijunNo + lngChgData + lngStartRow, _
                                             30, Hvo.HyojijunNo + lngChgData + +lngStartRow, RGB(255, 153, 204))
                            strUpdateFlg = "CHANGE"
                            xls.SetValue(30, Hvo.HyojijunNo + lngChgData + lngStartRow, _
                                         30, Hvo.HyojijunNo + lngChgData + lngStartRow, voKansei.ShisakuSiyouBusyo)
                            Dim MaeAtai As String = ""
                            If StringUtil.IsEmpty(Hvo.ShisakuSiyouBusyo) Then
                                MaeAtai = "空白"    'ＶＯ値が空なら空白を設定
                            Else
                                MaeAtai = Hvo.ShisakuSiyouBusyo
                            End If
                            xls.SetComment("変更前の値：" & MaeAtai, _
                                            30, Hvo.HyojijunNo + lngChgData + lngStartRow, _
                                            30, Hvo.HyojijunNo + lngChgData + lngStartRow)   '変更前の値をコメントにセット
                        End If
                        '-----------------------------------------------------
                        'グループ：比較して差異があればバックカラーを変更
                        '　Nothingとブランクが同居する可能性があるのでブランクに統一
                        If StringUtil.IsEmpty(Hvo.ShisakuGroup) Then Hvo.ShisakuGroup = ""
                        If StringUtil.IsEmpty(voKansei.ShisakuGroup) Then voKansei.ShisakuGroup = ""

                        If Not StringUtil.Equals(Hvo.ShisakuGroup, voKansei.ShisakuGroup) Then
                            xls.SetBackColor(31, Hvo.HyojijunNo + lngChgData + lngStartRow, _
                                             31, Hvo.HyojijunNo + lngChgData + +lngStartRow, RGB(255, 153, 204))
                            strUpdateFlg = "CHANGE"
                            xls.SetValue(31, Hvo.HyojijunNo + lngChgData + lngStartRow, _
                                         31, Hvo.HyojijunNo + lngChgData + lngStartRow, voKansei.ShisakuGroup)
                            Dim MaeAtai As String = ""
                            If StringUtil.IsEmpty(Hvo.ShisakuGroup) Then
                                MaeAtai = "空白"    'ＶＯ値が空なら空白を設定
                            Else
                                MaeAtai = Hvo.ShisakuGroup
                            End If
                            xls.SetComment("変更前の値：" & MaeAtai, _
                                            31, Hvo.HyojijunNo + lngChgData + lngStartRow, _
                                            31, Hvo.HyojijunNo + lngChgData + lngStartRow)   '変更前の値をコメントにセット
                        End If
                        '-----------------------------------------------------
                        '製作・製作順序：比較して差異があればバックカラーを変更
                        '　Nothingとブランクが同居する可能性があるのでブランクに統一
                        If StringUtil.IsEmpty(Hvo.ShisakuSeisakuJunjyo) Then Hvo.ShisakuSeisakuJunjyo = ""
                        If StringUtil.IsEmpty(voKansei.ShisakuSeisakuJunjyo) Then voKansei.ShisakuSeisakuJunjyo = ""

                        If Not StringUtil.Equals(Hvo.ShisakuSeisakuJunjyo, voKansei.ShisakuSeisakuJunjyo) Then
                            xls.SetBackColor(32, Hvo.HyojijunNo + lngChgData + lngStartRow, _
                                             32, Hvo.HyojijunNo + lngChgData + +lngStartRow, RGB(255, 153, 204))
                            strUpdateFlg = "CHANGE"
                            xls.SetValue(32, Hvo.HyojijunNo + lngChgData + lngStartRow, _
                                         32, Hvo.HyojijunNo + lngChgData + lngStartRow, voKansei.ShisakuSeisakuJunjyo)
                            Dim MaeAtai As String = ""
                            If StringUtil.IsEmpty(Hvo.ShisakuSeisakuJunjyo) Then
                                MaeAtai = "空白"    'ＶＯ値が空なら空白を設定
                            Else
                                MaeAtai = Hvo.ShisakuSeisakuJunjyo
                            End If
                            xls.SetComment("変更前の値：" & MaeAtai, _
                                            32, Hvo.HyojijunNo + lngChgData + lngStartRow, _
                                            32, Hvo.HyojijunNo + lngChgData + lngStartRow)   '変更前の値をコメントにセット
                        End If
                        '-----------------------------------------------------
                        '完成日：比較して差異があればバックカラーを変更
                        If Not StringUtil.Equals(Hvo.ShisakuKanseibi, voKansei.ShisakuKanseibi) Then
                            xls.SetBackColor(33, Hvo.HyojijunNo + lngChgData + lngStartRow, _
                                             33, Hvo.HyojijunNo + lngChgData + +lngStartRow, RGB(255, 153, 204))
                            strUpdateFlg = "CHANGE"
                            If StringUtil.IsNotEmpty(voKansei.ShisakuKanseibi) Then
                                xls.SetValue(33, Hvo.HyojijunNo + lngChgData + lngStartRow, _
                                             33, Hvo.HyojijunNo + lngChgData + lngStartRow, _
                                             ShisakuComFunc.moji8Convert2Date(voKansei.ShisakuKanseibi))
                            Else
                                xls.SetValue(33, Hvo.HyojijunNo + lngChgData + lngStartRow, _
                                             33, Hvo.HyojijunNo + lngChgData + lngStartRow, "")
                            End If
                            If StringUtil.IsNotEmpty(Hvo.ShisakuKanseibi) Then
                                xls.SetComment("変更前の値：" & Hvo.ShisakuKanseibi, _
                                                33, Hvo.HyojijunNo + lngChgData + lngStartRow, _
                                                33, Hvo.HyojijunNo + lngChgData + lngStartRow)   '変更前の値をコメントにセット
                            Else
                                xls.SetComment("変更前の値：" & "空白", _
                                                33, Hvo.HyojijunNo + lngChgData + lngStartRow, _
                                                33, Hvo.HyojijunNo + lngChgData + lngStartRow)   '変更前の値をコメントにセット
                            End If
                        End If
                        '-----------------------------------------------------
                        '工指No.：比較して差異があればバックカラーを変更
                        '　Nothingとブランクが同居する可能性があるのでブランクに統一
                        If StringUtil.IsEmpty(Hvo.ShisakuKoushiNo) Then Hvo.ShisakuKoushiNo = ""
                        If StringUtil.IsEmpty(voKansei.ShisakuKoushiNo) Then voKansei.ShisakuKoushiNo = ""

                        If Not StringUtil.Equals(Hvo.ShisakuKoushiNo, voKansei.ShisakuKoushiNo) Then
                            xls.SetBackColor(34, Hvo.HyojijunNo + lngChgData + lngStartRow, _
                                             34, Hvo.HyojijunNo + lngChgData + +lngStartRow, RGB(255, 153, 204))
                            strUpdateFlg = "CHANGE"
                            xls.SetValue(34, Hvo.HyojijunNo + lngChgData + lngStartRow, _
                                         34, Hvo.HyojijunNo + lngChgData + lngStartRow, voKansei.ShisakuKoushiNo)
                            Dim MaeAtai As String = ""
                            If StringUtil.IsEmpty(Hvo.ShisakuKoushiNo) Then
                                MaeAtai = "空白"    'ＶＯ値が空なら空白を設定
                            Else
                                MaeAtai = Hvo.ShisakuKoushiNo
                            End If
                            xls.SetComment("変更前の値：" & MaeAtai, _
                                            34, Hvo.HyojijunNo + lngChgData + lngStartRow, _
                                            34, Hvo.HyojijunNo + lngChgData + lngStartRow)   '変更前の値をコメントにセット
                        End If
                        '-----------------------------------------------------
                        '製作方法区分：比較して差異があればバックカラーを変更
                        '　Nothingとブランクが同居する可能性があるのでブランクに統一
                        If StringUtil.IsEmpty(Hvo.ShisakuSeisakuHouhouKbn) Then Hvo.ShisakuSeisakuHouhouKbn = ""
                        If StringUtil.IsEmpty(voKansei.ShisakuSeisakuHouhouKbn) Then voKansei.ShisakuSeisakuHouhouKbn = ""

                        If Not StringUtil.Equals(Hvo.ShisakuSeisakuHouhouKbn.Trim, voKansei.ShisakuSeisakuHouhouKbn.Trim) Then
                            xls.SetBackColor(35, Hvo.HyojijunNo + lngChgData + lngStartRow, _
                                             35, Hvo.HyojijunNo + lngChgData + +lngStartRow, RGB(255, 153, 204))
                            strUpdateFlg = "CHANGE"
                            xls.SetValue(35, Hvo.HyojijunNo + lngChgData + lngStartRow, _
                                         35, Hvo.HyojijunNo + lngChgData + lngStartRow, voKansei.ShisakuSeisakuHouhouKbn)
                            Dim MaeAtai As String = ""
                            If StringUtil.IsEmpty(Hvo.ShisakuSeisakuHouhouKbn) Then
                                MaeAtai = "空白"    'ＶＯ値が空なら空白を設定
                            Else
                                MaeAtai = Hvo.ShisakuSeisakuHouhouKbn
                            End If
                            xls.SetComment("変更前の値：" & MaeAtai, _
                                            35, Hvo.HyojijunNo + lngChgData + lngStartRow, _
                                            35, Hvo.HyojijunNo + lngChgData + lngStartRow)   '変更前の値をコメントにセット
                        End If
                        '-----------------------------------------------------
                        '製作方法：比較して差異があればバックカラーを変更
                        '　Nothingとブランクが同居する可能性があるのでブランクに統一
                        If StringUtil.IsEmpty(Hvo.ShisakuSeisakuHouhou) Then Hvo.ShisakuSeisakuHouhou = ""
                        If StringUtil.IsEmpty(voKansei.ShisakuSeisakuHouhou) Then voKansei.ShisakuSeisakuHouhou = ""

                        If Not StringUtil.Equals(Hvo.ShisakuSeisakuHouhou, voKansei.ShisakuSeisakuHouhou) Then
                            xls.SetBackColor(36, Hvo.HyojijunNo + lngChgData + lngStartRow, _
                                             36, Hvo.HyojijunNo + lngChgData + +lngStartRow, RGB(255, 153, 204))
                            strUpdateFlg = "CHANGE"
                            xls.SetValue(36, Hvo.HyojijunNo + lngChgData + lngStartRow, _
                                         36, Hvo.HyojijunNo + lngChgData + lngStartRow, voKansei.ShisakuSeisakuHouhou)
                            Dim MaeAtai As String = ""
                            If StringUtil.IsEmpty(Hvo.ShisakuSeisakuHouhou) Then
                                MaeAtai = "空白"    'ＶＯ値が空なら空白を設定
                            Else
                                MaeAtai = Hvo.ShisakuSeisakuHouhou
                            End If
                            xls.SetComment("変更前の値：" & MaeAtai, _
                                            36, Hvo.HyojijunNo + lngChgData + lngStartRow, _
                                            36, Hvo.HyojijunNo + lngChgData + lngStartRow)   '変更前の値をコメントにセット
                        End If
                        '-----------------------------------------------------
                        'メモ欄：比較して差異があればバックカラーを変更
                        '　Nothingとブランクが同居する可能性があるのでブランクに統一
                        If StringUtil.IsEmpty(Hvo.ShisakuMemo) Then Hvo.ShisakuMemo = ""
                        If StringUtil.IsEmpty(voKansei.ShisakuMemo) Then voKansei.ShisakuMemo = ""

                        If Not StringUtil.Equals(Hvo.ShisakuMemo, voKansei.ShisakuMemo) Then
                            xls.SetBackColor(37, Hvo.HyojijunNo + lngChgData + lngStartRow, _
                                             37, Hvo.HyojijunNo + lngChgData + +lngStartRow, RGB(255, 153, 204))
                            strUpdateFlg = "CHANGE"
                            xls.SetValue(37, Hvo.HyojijunNo + lngChgData + lngStartRow, _
                                         37, Hvo.HyojijunNo + lngChgData + lngStartRow, voKansei.ShisakuMemo)
                            Dim MaeAtai As String = ""
                            If StringUtil.IsEmpty(Hvo.ShisakuMemo) Then
                                MaeAtai = "空白"    'ＶＯ値が空なら空白を設定
                            Else
                                MaeAtai = Hvo.ShisakuMemo
                            End If
                            xls.SetComment("変更前の値：" & MaeAtai, _
                                            37, Hvo.HyojijunNo + lngChgData + lngStartRow, _
                                            37, Hvo.HyojijunNo + lngChgData + lngStartRow)   '変更前の値をコメントにセット
                        End If

                        If StringUtil.Equals(baseList.Item(lngDataCnt - 1).ShisakuSyubetu, "D") Then
                            '前回にあって今回にないので削除データとみなす。そしてシンボルへ削除をセット。
                            xls.SetValue(1, Hvo.HyojijunNo + lngChgData + lngStartRow, _
                                         1, Hvo.HyojijunNo + lngChgData + lngStartRow, "削除")
                            '-----------------------------------------------------
                            '背景色を変更
                            '   ～特別装備仕様の最後の列まで
                            xls.SetBackColor(2, Hvo.HyojijunNo + lngChgData + lngStartRow, _
                                             37, Hvo.HyojijunNo + lngChgData + lngStartRow, RGB(192, 192, 192))    '削除カラー
                            '-----------------------------------------------------
                            strUpdateFlg = ""   'クリア
                        End If
                        '種別
                        xls.SetValue(3, Hvo.HyojijunNo + lngChgData + lngStartRow, _
                                     3, Hvo.HyojijunNo + lngChgData + lngStartRow, baseList.Item(lngDataCnt - 1).ShisakuSyubetu)

                        ''変更行のために１カウントＵＰ
                        'If StringUtil.Equals(strUpdateFlg, "CHANGE") Then lngChgData = lngChgData + 1

                        If strUpdateFlg = "CHANGE" Then
                            kanseiUpdateFlg = "UPD"
                            '今回にあって前回にもあって変更箇所があるならシンボルへ「変更」をセット。
                            xls.SetValue(1, Hvo.HyojijunNo + lngChgData + lngStartRow, _
                                         1, Hvo.HyojijunNo + lngChgData + lngStartRow, "変更")
                        End If
                    End If
                End If
                '=================================================================================================================
            Next

            ''追加された号車が無いか確認する。
            'If StringUtil.IsNotEmpty(HikakuEventCode) = True Then
            '    '
            '    For Each vo As TShisakuEventKanseiVo In kanseiList   '今回完成車情報
            '        'カウントクリア
            '        lngChgDataCnt = 0
            '        '-----------------------------------------------------
            '        'ベース車の情報を取得する。（今回登録）
            '        BaseVo = eventBaseCarDao.FindShisakuEventBaseCar(vo.ShisakuEventCode, vo.HyojijunNo)
            '        '-----------------------------------------------------
            '        '比較対象のイベントを号車で抽出する。
            '        If HbaseList.Count <> 0 Then
            '            For Each Hvo As TShisakuEventKanseiKaiteiVo In HkanseiList   '前回完成車情報
            '                '-----------------------------------------------------
            '                'ベース車の情報を取得する。（前回登録）
            '                HBaseVo = eventBaseCarDao.FindShisakuEventBaseKaiteiCar(Hvo.ShisakuEventCode, Hvo.HyojijunNo)
            '                '-----------------------------------------------------
            '                If StringUtil.Equals(HBaseVo.ShisakuGousya, BaseVo.ShisakuGousya) And _
            '                   StringUtil.Equals(HBaseVo.ShisakuSyubetu, BaseVo.ShisakuSyubetu) Then
            '                    voKansei = vo
            '                    lngChgDataCnt = lngChgDataCnt + 1
            '                    Exit For
            '                End If
            '            Next
            '        End If
            '        '-----------------------------------------------------

            '        '=================================================================================================================
            '        '号車が今回改訂№にあって、比較改訂№にない場合は追加として出力する。
            '        If lngChgDataCnt = 0 Then
            '            kanseiUpdateFlg = "UPD"
            '            'データ件数カウント
            '            lngDataCnt = lngDataCnt + 1

            '            '-----------------------------------------------------
            '            '連番
            '            xls.SetValue(2, lngDataCnt + lngChgData + lngStartRow, _
            '                         2, lngDataCnt + lngChgData + lngStartRow, lngDataCnt)
            '            '種別
            '            xls.SetValue(3, lngDataCnt + lngChgData + lngStartRow, _
            '                         3, lngDataCnt + lngChgData + lngStartRow, BaseVo.ShisakuSyubetu)
            '            '号車
            '            xls.SetValue(4, lngDataCnt + lngChgData + lngStartRow, _
            '                         4, lngDataCnt + lngChgData + lngStartRow, BaseVo.ShisakuGousya)
            '            '車型
            '            xls.SetValue(5, lngDataCnt + lngChgData + lngStartRow, _
            '                         5, lngDataCnt + lngChgData + lngStartRow, vo.ShisakuSyagata)
            '            'グレード
            '            xls.SetValue(6, lngDataCnt + lngChgData + lngStartRow, _
            '                         6, lngDataCnt + lngChgData + lngStartRow, vo.ShisakuGrade)
            '            '仕向け
            '            xls.SetValue(7, lngDataCnt + lngChgData + lngStartRow, _
            '                         7, lngDataCnt + lngChgData + lngStartRow, vo.ShisakuShimukechiShimuke)
            '            'ハンドル
            '            xls.SetValue(8, lngDataCnt + lngChgData + lngStartRow, _
            '                         8, lngDataCnt + lngChgData + lngStartRow, vo.ShisakuHandoru)
            '            '型式
            '            xls.SetValue(9, lngDataCnt + lngChgData + lngStartRow, _
            '                         9, lngDataCnt + lngChgData + lngStartRow, vo.ShisakuEgKatashiki)
            '            '排気量
            '            xls.SetValue(10, lngDataCnt + lngChgData + lngStartRow, _
            '                         10, lngDataCnt + lngChgData + lngStartRow, vo.ShisakuEgHaikiryou)
            '            'システム
            '            xls.SetValue(11, lngDataCnt + lngChgData + lngStartRow, _
            '                         11, lngDataCnt + lngChgData + lngStartRow, vo.ShisakuEgSystem)
            '            '過給器
            '            xls.SetValue(12, lngDataCnt + lngChgData + lngStartRow, _
            '                         12, lngDataCnt + lngChgData + lngStartRow, vo.ShisakuEgKakyuuki)
            '            'メモ１
            '            xls.SetValue(13, lngDataCnt + lngChgData + lngStartRow, _
            '                         13, lngDataCnt + lngChgData + lngStartRow, vo.ShisakuEgMemo1)
            '            'メモ２
            '            xls.SetValue(14, lngDataCnt + lngChgData + lngStartRow, _
            '                         14, lngDataCnt + lngChgData + lngStartRow, vo.ShisakuEgMemo2)
            '            'T/M仕様・駆動方式
            '            xls.SetValue(15, lngDataCnt + lngChgData + lngStartRow, _
            '                         15, lngDataCnt + lngChgData + lngStartRow, vo.ShisakuTmKudou)
            '            'T/M仕様・変速機
            '            xls.SetValue(16, lngDataCnt + lngChgData + lngStartRow, _
            '                         16, lngDataCnt + lngChgData + lngStartRow, vo.ShisakuTmHensokuki)
            '            '副変速
            '            xls.SetValue(17, lngDataCnt + lngChgData + lngStartRow, _
            '                         17, lngDataCnt + lngChgData + lngStartRow, vo.ShisakuTmFukuHensokuki)
            '            'メモ１
            '            xls.SetValue(18, lngDataCnt + lngChgData + lngStartRow, _
            '                         18, lngDataCnt + lngChgData + lngStartRow, vo.ShisakuTmMemo1)
            '            'メモ２
            '            xls.SetValue(19, lngDataCnt + lngChgData + lngStartRow, _
            '                         19, lngDataCnt + lngChgData + lngStartRow, vo.ShisakuTmMemo2)
            '            '型式
            '            xls.SetValue(20, lngDataCnt + lngChgData + lngStartRow, _
            '                         20, lngDataCnt + lngChgData + lngStartRow, vo.ShisakuKatashiki)
            '            '仕向け
            '            xls.SetValue(21, lngDataCnt + lngChgData + lngStartRow, _
            '                         21, lngDataCnt + lngChgData + lngStartRow, vo.ShisakuShimuke)
            '            'ＯＰ
            '            xls.SetValue(22, lngDataCnt + lngChgData + lngStartRow, _
            '                         22, lngDataCnt + lngChgData + lngStartRow, vo.ShisakuOp)
            '            '外装色
            '            xls.SetValue(23, lngDataCnt + lngChgData + lngStartRow, _
            '                         23, lngDataCnt + lngChgData + lngStartRow, vo.ShisakuGaisousyoku)
            '            '外装色名
            '            xls.SetValue(24, lngDataCnt + lngChgData + lngStartRow, _
            '                         24, lngDataCnt + lngChgData + lngStartRow, vo.ShisakuGaisousyokuName)
            '            '内装色
            '            xls.SetValue(25, lngDataCnt + lngChgData + lngStartRow, _
            '                         25, lngDataCnt + lngChgData + lngStartRow, vo.ShisakuNaisousyoku)
            '            '内装色名
            '            xls.SetValue(26, lngDataCnt + lngChgData + lngStartRow, _
            '                         26, lngDataCnt + lngChgData + lngStartRow, vo.ShisakuNaisousyokuName)
            '            '車台No.
            '            xls.SetValue(27, lngDataCnt + lngChgData + lngStartRow, _
            '                         27, lngDataCnt + lngChgData + lngStartRow, vo.ShisakuSyadaiNo)
            '            '使用目的
            '            xls.SetValue(28, lngDataCnt + lngChgData + lngStartRow, _
            '                         28, lngDataCnt + lngChgData + lngStartRow, vo.ShisakuShiyouMokuteki)
            '            '主要確認項目
            '            xls.SetValue(29, lngDataCnt + lngChgData + lngStartRow, _
            '                         29, lngDataCnt + lngChgData + lngStartRow, vo.ShisakuShikenMokuteki)
            '            '使用部署
            '            xls.SetValue(30, lngDataCnt + lngChgData + lngStartRow, _
            '                         30, lngDataCnt + lngChgData + lngStartRow, vo.ShisakuSiyouBusyo)
            '            'グループ
            '            xls.SetValue(31, lngDataCnt + lngChgData + lngStartRow, _
            '                         31, lngDataCnt + lngChgData + lngStartRow, vo.ShisakuGroup)
            '            '製作・製作順序
            '            xls.SetValue(32, lngDataCnt + lngChgData + lngStartRow, _
            '                         32, lngDataCnt + lngChgData + lngStartRow, vo.ShisakuSeisakuJunjyo)
            '            '完成日
            '            '完成日があれば'
            '            If Not vo.ShisakuKanseibi Is Nothing Then
            '                xls.SetValue(33, lngDataCnt + lngChgData + lngStartRow, _
            '                             33, lngDataCnt + lngChgData + lngStartRow, ShisakuComFunc.moji8Convert2Date(vo.ShisakuSeisakuJunjyo))
            '            End If
            '            '工指No.
            '            xls.SetValue(34, lngDataCnt + lngChgData + lngStartRow, _
            '                         34, lngDataCnt + lngChgData + lngStartRow, vo.ShisakuKoushiNo)
            '            '製作方法区分
            '            xls.SetValue(35, lngDataCnt + lngChgData + lngStartRow, _
            '                         35, lngDataCnt + lngChgData + lngStartRow, vo.ShisakuSeisakuHouhouKbn)
            '            '製作方法
            '            xls.SetValue(36, lngDataCnt + lngChgData + lngStartRow, _
            '                         36, lngDataCnt + lngChgData + lngStartRow, vo.ShisakuSeisakuHouhou)
            '            'メモ欄
            '            xls.SetValue(37, lngDataCnt + lngChgData + lngStartRow, _
            '                         37, lngDataCnt + lngChgData + lngStartRow, vo.ShisakuMemo)
            '            '-----------------------------------------------------

            '            '-----------------------------------------------------
            '            '背景色を変更
            '            '   開発符号～最後の列まで
            '            xls.SetBackColor(2, lngDataCnt + lngChgData + lngStartRow, _
            '                             37, lngDataCnt + lngChgData + lngStartRow, RGB(204, 255, 204))   '追加カラー
            '            '-----------------------------------------------------
            '            'シンボル更新
            '            '今回にあって前回にないので追加データとみなす。そしてシンボルへ「追加」をセット。
            '            xls.SetValue(1, lngDataCnt + lngChgData + lngStartRow, _
            '                         1, lngDataCnt + lngChgData + lngStartRow, "追加")

            '            'ベース車VOにも追加データを更新しておく
            '            '入力用VO定義
            '            Dim HldVoKansei As New TShisakuEventKanseiVo
            '            HldVoKansei = vo
            '            HldVoKansei.HyojijunNo = lngDataCnt
            '            kanseiList.Add(HldVoKansei)    '挿入

            '        End If
            '        '===============================================================================================
            '    Next
            'End If

            '罫線を引く
            xls.SetLine(1, 8, 37, lngStartRow + baseList.Item(baseList.Count - 1).HyojijunNo, _
                        XlBordersIndex.xlEdgeTop, XlLineStyle.xlContinuous, XlBorderWeight.xlThin) '上
            xls.SetLine(1, 8, 37, lngStartRow + baseList.Item(baseList.Count - 1).HyojijunNo, _
                        XlBordersIndex.xlEdgeBottom, XlLineStyle.xlContinuous, XlBorderWeight.xlThin) '下
            xls.SetLine(1, 8, 37, lngStartRow + baseList.Item(baseList.Count - 1).HyojijunNo, _
                        XlBordersIndex.xlEdgeLeft, XlLineStyle.xlContinuous, XlBorderWeight.xlThin) '左
            xls.SetLine(1, 8, 37, lngStartRow + baseList.Item(baseList.Count - 1).HyojijunNo, _
                        XlBordersIndex.xlEdgeRight, XlLineStyle.xlContinuous, XlBorderWeight.xlThin) '右
            xls.SetLine(1, 8, 37, lngStartRow + baseList.Item(baseList.Count - 1).HyojijunNo, _
                        XlBordersIndex.xlInsideHorizontal, XlLineStyle.xlContinuous, XlBorderWeight.xlThin) '水平
            xls.SetLine(1, 8, 37, lngStartRow + baseList.Item(baseList.Count - 1).HyojijunNo, _
                        XlBordersIndex.xlInsideVertical, XlLineStyle.xlContinuous, XlBorderWeight.xlThin) '垂直

            Return kanseiUpdateFlg

        End Function

        ''' <summary>
        ''' タイトル部作成（基本装備仕様情報）
        ''' </summary>
        ''' <param name="xls"></param>
        ''' <remarks></remarks>
        Private Sub setSheetTitleBASIC(ByVal xls As ShisakuExcel)

            'xls.SetValueのパラメータは－－－＞　startCol ,startRow ,endCol ,endRow ,value

            'シンボル
            xls.SetValue(1, 8, 1, 8, "シンボル")
            xls.MergeCells(1, 8, 1, 10, True)
            xls.SetOrientation(1, 8, 1, 8, ShisakuExcel.XlOrientation.xlHorizontal, False, True)   '横
            xls.SetAlignment(1, 8, 1, 8, XlHAlign.xlHAlignLeft, XlVAlign.xlVAlignBottom, False, True) '位置
            '連番
            xls.SetValue(2, 8, 2, 8, "連番")
            xls.MergeCells(2, 8, 2, 10, True)
            xls.SetOrientation(2, 8, 2, 8, ShisakuExcel.XlOrientation.xlHorizontal, False, True)   '横
            xls.SetAlignment(2, 8, 2, 8, XlHAlign.xlHAlignLeft, XlVAlign.xlVAlignBottom, False, True) '位置
            '種別
            xls.SetValue(3, 8, 3, 8, "種別")
            xls.MergeCells(3, 8, 3, 10, True)
            xls.SetOrientation(3, 8, 3, 8, ShisakuExcel.XlOrientation.xlHorizontal, False, True)   '横
            xls.SetAlignment(3, 8, 3, 8, XlHAlign.xlHAlignLeft, XlVAlign.xlVAlignBottom, False, True) '位置
            '号車
            xls.SetValue(4, 8, 4, 8, "号車")
            xls.MergeCells(4, 8, 4, 10, True)
            xls.SetOrientation(4, 8, 4, 8, ShisakuExcel.XlOrientation.xlHorizontal, False, True)   '横
            xls.SetAlignment(4, 8, 4, 8, XlHAlign.xlHAlignLeft, XlVAlign.xlVAlignBottom, False, True) '位置

            '書式設定
            xls.SetOrientation(lngStartColumn, 8, lngStartColumn + 200, 10, ShisakuExcel.XlOrientation.xlDownward, True, False)   '縦書き
            xls.SetAlignment(lngStartColumn, 8, lngStartColumn + 200, 10, XlHAlign.xlHAlignCenter, XlVAlign.xlVAlignTop, True, False) '位置
            xls.SetRowHeight(8, 8, 24)
            xls.SetRowHeight(9, 9, 43.5)
            xls.SetRowHeight(10, 10, 180.75)

            '   基本装備仕様列作成
            Dim lngDatacnt As Long = 0
            Dim lngBasicCnt As Long = 0
            Dim lngChgDataCnt As Integer = 0
            Dim voBasic As New EventSoubiVo
            If basicTitelList.Count <> 0 Or HbasicTitelList.Count <> 0 Then
                '   基本装備仕様項目列分作成
                For Each vo As EventSoubiVo In HbasicTitelList
                    '項目列名をセット
                    xls.SetValue(lngStartColumn + vo.ShisakuSoubiHyoujiNo, 8, _
                                 lngStartColumn + vo.ShisakuSoubiHyoujiNo, 8, vo.ShisakuRetuKoumokuNameDai)
                    xls.SetValue(lngStartColumn + vo.ShisakuSoubiHyoujiNo, 9, _
                                 lngStartColumn + vo.ShisakuSoubiHyoujiNo, 9, vo.ShisakuRetuKoumokuNameChu)
                    xls.SetValue(lngStartColumn + vo.ShisakuSoubiHyoujiNo, 10, _
                                 lngStartColumn + vo.ShisakuSoubiHyoujiNo, 10, vo.ShisakuRetuKoumokuName)
                    '列の最大値を求める。
                    If lngDatacnt < vo.ShisakuSoubiHyoujiNo Then
                        lngDatacnt = vo.ShisakuSoubiHyoujiNo
                    End If
                    '=================================================================================================================
                    '変化点はバックカラーを変更する。
                    'カウントクリア
                    lngChgDataCnt = 0
                    '-----------------------------------------------------
                    '今回取込ファイルのイベントを号車で抽出する。
                    If basicTitelList.Count <> 0 Then
                        For Each hikakuVo As EventSoubiVo In basicTitelList   '今回登録時のデータ
                            '
                            If hikakuVo.ShisakuRetuKoumokuNameDai Is Nothing Then hikakuVo.ShisakuRetuKoumokuNameDai = ""
                            If hikakuVo.ShisakuRetuKoumokuNameDai Is Nothing Then hikakuVo.ShisakuRetuKoumokuNameDai = ""
                            If hikakuVo.ShisakuRetuKoumokuName Is Nothing Then hikakuVo.ShisakuRetuKoumokuName = ""
                            '
                            If vo.ShisakuRetuKoumokuNameDai Is Nothing Then vo.ShisakuRetuKoumokuNameDai = ""
                            If vo.ShisakuRetuKoumokuNameChu Is Nothing Then vo.ShisakuRetuKoumokuNameChu = ""
                            If vo.ShisakuRetuKoumokuName Is Nothing Then vo.ShisakuRetuKoumokuName = ""
                            '
                            If StringUtil.Equals(hikakuVo.ShisakuRetuKoumokuNameDai, vo.ShisakuRetuKoumokuNameDai) And _
                               StringUtil.Equals(hikakuVo.ShisakuRetuKoumokuNameChu, vo.ShisakuRetuKoumokuNameChu) And _
                               StringUtil.Equals(hikakuVo.ShisakuRetuKoumokuName, vo.ShisakuRetuKoumokuName) Then
                                voBasic = vo
                                lngChgDataCnt = lngChgDataCnt + 1
                                Exit For
                            End If
                        Next
                    End If

                    'シンボル更新
                    If lngChgDataCnt = 0 Then
                        '前回にあって今回にないので削除データとみなす。
                        '-----------------------------------------------------
                        '背景色を変更
                        '   ～特別装備仕様の最後の列まで
                        xls.SetBackColor(lngStartColumn + lngDatacnt, 8, lngStartColumn + lngDatacnt, 10, _
                                         RGB(192, 192, 192))    '削除カラー
                        '-----------------------------------------------------
                    End If
                Next
            End If
            '================================================================================================================
            '追加された装備仕様が無いか確認する。
            For Each vo As EventSoubiVo In basicTitelList   '今回基本装備情報
                'カウントクリア
                lngChgDataCnt = 0
                '-----------------------------------------------------
                '比較対象のイベントを号車で抽出する。
                If HbasicTitelList.Count <> 0 Then
                    For Each Hvo As EventSoubiVo In HbasicTitelList   '前回基本装備情報
                        '-----------------------------------------------------
                        If Hvo.ShisakuRetuKoumokuNameDai Is Nothing Then Hvo.ShisakuRetuKoumokuNameDai = ""
                        If Hvo.ShisakuRetuKoumokuNameChu Is Nothing Then Hvo.ShisakuRetuKoumokuNameChu = ""
                        If Hvo.ShisakuRetuKoumokuName Is Nothing Then Hvo.ShisakuRetuKoumokuName = ""
                        '
                        If vo.ShisakuRetuKoumokuNameDai Is Nothing Then vo.ShisakuRetuKoumokuNameDai = ""
                        If vo.ShisakuRetuKoumokuNameChu Is Nothing Then vo.ShisakuRetuKoumokuNameChu = ""
                        If vo.ShisakuRetuKoumokuName Is Nothing Then vo.ShisakuRetuKoumokuName = ""
                        '
                        If StringUtil.Equals(Hvo.ShisakuRetuKoumokuNameDai, vo.ShisakuRetuKoumokuNameDai) And _
                           StringUtil.Equals(Hvo.ShisakuRetuKoumokuNameChu, vo.ShisakuRetuKoumokuNameChu) And _
                           StringUtil.Equals(Hvo.ShisakuRetuKoumokuName, vo.ShisakuRetuKoumokuName) Then
                            voBasic = vo
                            lngChgDataCnt = lngChgDataCnt + 1
                            Exit For
                        End If
                    Next
                End If
                '-----------------------------------------------------

                '=================================================================================================================
                '装備仕様が今回改訂にあって、比較改訂にない場合は追加として出力する。
                If lngChgDataCnt = 0 Then

                    '-----------------------------------------------------
                    '項目列名をセット
                    xls.SetValue(lngStartColumn + vo.ShisakuSoubiHyoujiNo, 8, _
                                 lngStartColumn + vo.ShisakuSoubiHyoujiNo, 8, vo.ShisakuRetuKoumokuNameDai)
                    xls.SetValue(lngStartColumn + vo.ShisakuSoubiHyoujiNo, 9, _
                                 lngStartColumn + vo.ShisakuSoubiHyoujiNo, 9, vo.ShisakuRetuKoumokuNameChu)
                    xls.SetValue(lngStartColumn + vo.ShisakuSoubiHyoujiNo, 10, _
                                 lngStartColumn + vo.ShisakuSoubiHyoujiNo, 10, vo.ShisakuRetuKoumokuName)
                    '-----------------------------------------------------
                    '列の最大値を求める。
                    If lngDatacnt < vo.ShisakuSoubiHyoujiNo Then
                        lngDatacnt = vo.ShisakuSoubiHyoujiNo
                    End If
                    '-----------------------------------------------------
                    '今回にあって前回にないので追加データとみなす。
                    '背景色を変更
                    xls.SetBackColor(lngStartColumn + vo.ShisakuSoubiHyoujiNo, 8, _
                                     lngStartColumn + vo.ShisakuSoubiHyoujiNo, _
                                     baseList.Item(baseList.Count - 1).HyojijunNo + lngStartRow, _
                                     RGB(204, 255, 204))   '追加カラー
                    '-----------------------------------------------------

                    ''データ件数カウント
                    'lngDatacnt = lngDatacnt + 1
                End If
                '===============================================================================================
            Next

            '罫線を引く
            xls.SetLine(1, 8, lngStartColumn + lngDatacnt, baseList.Item(baseList.Count - 1).HyojijunNo + lngStartRow, _
                        XlBordersIndex.xlEdgeTop, XlLineStyle.xlContinuous, XlBorderWeight.xlThin) '上
            xls.SetLine(1, 8, lngStartColumn + lngDatacnt, baseList.Item(baseList.Count - 1).HyojijunNo + lngStartRow, _
                        XlBordersIndex.xlEdgeBottom, XlLineStyle.xlContinuous, XlBorderWeight.xlThin) '下
            xls.SetLine(1, 8, lngStartColumn + lngDatacnt, baseList.Item(baseList.Count - 1).HyojijunNo + lngStartRow, _
                        XlBordersIndex.xlEdgeLeft, XlLineStyle.xlContinuous, XlBorderWeight.xlThin) '左
            xls.SetLine(1, 8, lngStartColumn + lngDatacnt, baseList.Item(baseList.Count - 1).HyojijunNo + lngStartRow, _
                        XlBordersIndex.xlEdgeRight, XlLineStyle.xlContinuous, XlBorderWeight.xlThin) '右
            xls.SetLine(1, 8, lngStartColumn + lngDatacnt, baseList.Item(baseList.Count - 1).HyojijunNo + lngStartRow, _
                        XlBordersIndex.xlInsideHorizontal, XlLineStyle.xlContinuous, XlBorderWeight.xlThin) '水平
            xls.SetLine(1, 8, lngStartColumn + lngDatacnt, baseList.Item(baseList.Count - 1).HyojijunNo + lngStartRow, _
                        XlBordersIndex.xlInsideVertical, XlLineStyle.xlContinuous, XlBorderWeight.xlThin) '垂直
            xls.SetAlignment(lngStartColumn, lngStartRow, _
                             lngStartColumn + lngDatacnt, baseList.Item(baseList.Count - 1).HyojijunNo + lngStartRow, _
                             XlHAlign.xlHAlignCenter, ShisakuExcel.XlVAlign.xlVAlignCenter, True, False) '位置

        End Sub

        ''' <summary>
        ''' EXCEL出力・ボディーパート(基本装備仕様シート)
        ''' </summary>
        ''' <param name="xls"></param>
        ''' <remarks></remarks>
        Private Function setSheet1BodyPartBASIC(ByVal xls As ShisakuExcel) As String

            'xls.SetValueのパラメータは－－－＞　startCol ,startRow ,endCol ,endRow ,value
            '初期設定
            Dim lngDataCnt As Long = 0
            Dim lngDataCnt2 As Long = 0
            Dim lngChgDataCnt As Long = 0
            Dim lngChgData As Long = 0
            Dim voBasic As New EventSoubiVo
            Dim getColorName As New EventEdit.Dao.EventEditBaseCarDaoImpl
            Dim eventBaseCarDao As EventEditCompleteCarRirekiDao = New EventEditCompleteCarRirekiDaoImpl()
            'Dim BaseVo As TShisakuEventBaseVo
            'Dim HBaseVo As TShisakuEventBaseKaiteiVo

            Dim HldGousya As String = ""

            '戻り値
            '   変更無時はブランク
            Dim basicUpdateFlg As String = ""

            '   基本装備仕様情報ＥＸＣＥＬ貼り付け
            '       比較元の情報より貼り付ける。
            For Each Hvo As EventSoubiVo In HbasicList   '前回の基本装備仕様情報
                '号車チェックとして使用
                Dim strGousya As String = Nothing
                '変更フラグとして使用
                Dim strUpdateFlg As String = Nothing

                '-----------------------------------------------------
                If Not StringUtil.Equals(HldGousya, Hvo.ShisakuGousya) Then
                    'データ件数カウント
                    lngDataCnt2 = lngDataCnt2 + 1
                End If
                HldGousya = Hvo.ShisakuGousya
                '-----------------------------------------------------

                ''-----------------------------------------------------
                ''ベース車の情報を取得する。（前回登録）
                'HBaseVo = eventBaseCarDao.FindShisakuEventBaseKaiteiCar(Hvo.ShisakuEventCode, Hvo.HyojijunNo)
                '-----------------------------------------------------

                '-----------------------------------------------------
                'シンボル

                If StringUtil.IsEmpty(xls.GetValue(4, Hvo.HyojijunNo + lngChgData + lngStartRow, _
                             4, Hvo.HyojijunNo + lngChgData + lngStartRow)) Then

                    '連番
                    xls.SetValue(2, Hvo.HyojijunNo + lngChgData + lngStartRow, _
                                 2, Hvo.HyojijunNo + lngChgData + lngStartRow, Hvo.HyojijunNo + 1)
                    '種別
                    xls.SetValue(3, Hvo.HyojijunNo + lngChgData + lngStartRow, _
                                 3, Hvo.HyojijunNo + lngChgData + lngStartRow, Hvo.ShisakuSyubetu)
                    '号車
                    xls.SetValue(4, Hvo.HyojijunNo + lngChgData + lngStartRow, _
                                 4, Hvo.HyojijunNo + lngChgData + lngStartRow, Hvo.ShisakuGousya)

                End If
                If StringUtil.IsNotEmpty(Hvo.ShisakuTekiyou) Then
                    '適用
                    xls.SetValue(lngStartColumn + Hvo.ShisakuSoubiHyoujiNo, Hvo.HyojijunNo + lngChgData + lngStartRow, _
                                 lngStartColumn + Hvo.ShisakuSoubiHyoujiNo, Hvo.HyojijunNo + lngChgData + lngStartRow, _
                                 Hvo.ShisakuTekiyou)
                End If

                '=================================================================================================================
                '変化点はバックカラーを変更する。
                'カウントクリア
                lngChgDataCnt = 0
                '-----------------------------------------------------
                '今回取込ファイルのイベントを号車で抽出する。
                If basicList.Count <> 0 Then
                    For Each vo As EventSoubiVo In basicList   '今回登録時のデータ
                        ''-----------------------------------------------------
                        ''ベース車の情報を取得する。（今回登録）
                        'BaseVo = eventBaseCarDao.FindShisakuEventBaseCar(vo.ShisakuEventCode, vo.HyojijunNo)
                        '-----------------------------------------------------
                        Dim hvoShisakuSyubetu As String = ""
                        If StringUtil.IsEmpty(Hvo.ShisakuSyubetu) Then
                            hvoShisakuSyubetu = ""
                        Else
                            hvoShisakuSyubetu = Hvo.ShisakuSyubetu
                        End If
                        Dim voShisakuSyubetu As String = ""
                        If StringUtil.IsEmpty(vo.ShisakuSyubetu) Then
                            voShisakuSyubetu = ""
                        Else
                            voShisakuSyubetu = vo.ShisakuSyubetu
                        End If
                        If StringUtil.Equals(Hvo.ShisakuGousya, vo.ShisakuGousya)  Then
                            'If StringUtil.Equals(Hvo.ShisakuGousya, vo.ShisakuGousya) And _
                            '   StringUtil.Equals(hvoShisakuSyubetu, voShisakuSyubetu) Then
                            strGousya = "OK"
                            'さらに仕様情報（大、中、小）がマッチしているかチェック。
                            If Hvo.ShisakuRetuKoumokuNameDai = vo.ShisakuRetuKoumokuNameDai And _
                               Hvo.ShisakuRetuKoumokuNameChu = vo.ShisakuRetuKoumokuNameChu And _
                               Hvo.ShisakuRetuKoumokuName = vo.ShisakuRetuKoumokuName Then
                                voBasic = vo
                                lngChgDataCnt = lngChgDataCnt + 1
                                Exit For
                            End If

                        End If
                    Next
                End If

                If strGousya = "OK" Then
                    'シンボル更新
                    If lngChgDataCnt = 0 Then
                        '前回にあって今回にないので削除データとみなす。そしてシンボルへ削除をセット。
                        xls.SetValue(1, Hvo.HyojijunNo + lngChgData + lngStartRow, _
                                     1, Hvo.HyojijunNo + lngChgData + lngStartRow, "変更")
                        '-----------------------------------------------------
                        '背景色を変更
                        '   ～特別装備仕様の最後の列まで
                        xls.SetBackColor(Hvo.ShisakuSoubiHyoujiNo + lngStartColumn, 8, _
                                             Hvo.ShisakuSoubiHyoujiNo + lngStartColumn, 10, RGB(192, 192, 192))    '削除カラー
                        '-----------------------------------------------------
                        basicUpdateFlg = "UPD"
                    Else
                        '-----------------------------------------------------
                        '適用：比較して差異があればバックカラーを変更
                        '　Nothingとブランクが同居する可能性があるのでブランクに統一
                        If StringUtil.IsEmpty(Hvo.ShisakuTekiyou) Then Hvo.ShisakuTekiyou = ""
                        If StringUtil.IsEmpty(voBasic.ShisakuTekiyou) Then voBasic.ShisakuTekiyou = ""

                        If Not StringUtil.Equals(Hvo.ShisakuTekiyou, voBasic.ShisakuTekiyou)  Then
                            xls.SetBackColor(Hvo.ShisakuSoubiHyoujiNo + lngStartColumn, Hvo.HyojijunNo + lngChgData + lngStartRow, _
                                             Hvo.ShisakuSoubiHyoujiNo + lngStartColumn, Hvo.HyojijunNo + lngChgData + +lngStartRow, _
                                             RGB(255, 153, 204))
                            strUpdateFlg = "CHANGE"
                            xls.SetValue(Hvo.ShisakuSoubiHyoujiNo + lngStartColumn, Hvo.HyojijunNo + lngChgData + lngStartRow, _
                                         Hvo.ShisakuSoubiHyoujiNo + lngStartColumn, Hvo.HyojijunNo + lngChgData + lngStartRow, _
                                            voBasic.ShisakuTekiyou)
                            Dim MaeAtai As String = ""
                            If StringUtil.IsEmpty(Hvo.ShisakuTekiyou) Then
                                MaeAtai = "空白"    'ＶＯ値が空なら空白を設定
                            Else
                                MaeAtai = Hvo.ShisakuTekiyou
                            End If
                            xls.SetComment("変更前の値：" & MaeAtai, _
                                            Hvo.ShisakuSoubiHyoujiNo + lngStartColumn, Hvo.HyojijunNo + lngChgData + lngStartRow, _
                                            Hvo.ShisakuSoubiHyoujiNo + lngStartColumn, Hvo.HyojijunNo + lngChgData + lngStartRow)   '変更前の値をコメントにセット
                        End If

                        If StringUtil.Equals(baseList.Item(lngDataCnt2 - 1).ShisakuSyubetu, "D") Then
                            '前回にあって今回にないので削除データとみなす。そしてシンボルへ削除をセット。
                            xls.SetValue(1, Hvo.HyojijunNo + lngChgData + lngStartRow, _
                                         1, Hvo.HyojijunNo + lngChgData + lngStartRow, "削除")
                            '-----------------------------------------------------
                            '背景色を変更
                            '   ～特別装備仕様の最後の列まで
                            xls.SetBackColor(2, Hvo.HyojijunNo + lngChgData + lngStartRow, _
                                             200, Hvo.HyojijunNo + lngChgData + lngStartRow, RGB(192, 192, 192))    '削除カラー
                            '-----------------------------------------------------
                            strUpdateFlg = ""   'クリア
                        End If
                        '種別
                        xls.SetValue(3, Hvo.HyojijunNo + lngChgData + lngStartRow, _
                                     3, Hvo.HyojijunNo + lngChgData + lngStartRow, baseList.Item(lngDataCnt2 - 1).ShisakuSyubetu)

                        ''変更行のために１カウントＵＰ
                        'If StringUtil.Equals(strUpdateFlg, "CHANGE") Then lngChgData = lngChgData + 1

                        If strUpdateFlg = "CHANGE" Then
                            basicUpdateFlg = "UPD"
                            '今回にあって前回にもあって変更箇所があるならシンボルへ「変更」をセット。
                            xls.SetValue(1, Hvo.HyojijunNo + lngChgData + lngStartRow, _
                                         1, Hvo.HyojijunNo + lngChgData + lngStartRow, "変更")
                        End If
                    End If
                    '=================================================================================================================
                Else
                    If Not StringUtil.Equals(xls.GetValue(1, Hvo.HyojijunNo + lngChgData + lngStartRow, _
                                     1, Hvo.HyojijunNo + lngChgData + lngStartRow), "削除") Then
                        '前回にあって今回にない号車なので削除データとみなす。そしてシンボルへ削除をセット。
                        xls.SetValue(1, Hvo.HyojijunNo + lngChgData + lngStartRow, _
                                     1, Hvo.HyojijunNo + lngChgData + lngStartRow, "削除")
                        '-----------------------------------------------------
                        '背景色を変更
                        '   ～特別装備仕様の最後の列まで
                        xls.SetBackColor(2, Hvo.HyojijunNo + lngChgData + lngStartRow, _
                                         200, Hvo.HyojijunNo + lngChgData + lngStartRow, RGB(192, 192, 192))    '削除カラー
                        '-----------------------------------------------------
                    End If
                    basicUpdateFlg = "UPD"
                End If
            Next




            For Each Hvo As EventSoubiVo In basicList   '今回の基本装備仕様情報
                '号車チェックとして使用
                Dim strGousya As String = Nothing
                '変更フラグとして使用
                Dim strUpdateFlg As String = Nothing
                'データ件数カウント
                lngDataCnt = lngDataCnt + 1

                '=================================================================================================================
                '変化点はバックカラーを変更する。
                'カウントクリア
                lngChgDataCnt = 0
                '-----------------------------------------------------
                '前回取込ファイルのイベントを号車で抽出する。
                If HbasicList.Count <> 0 Then
                    For Each vo As EventSoubiVo In HbasicList   '今回登録時のデータ
                        ''-----------------------------------------------------
                        Dim hvoShisakuSyubetu As String = ""
                        If StringUtil.IsEmpty(Hvo.ShisakuSyubetu) Then
                            hvoShisakuSyubetu = ""
                        Else
                            hvoShisakuSyubetu = Hvo.ShisakuSyubetu
                        End If
                        Dim voShisakuSyubetu As String = ""
                        If StringUtil.IsEmpty(vo.ShisakuSyubetu) Then
                            voShisakuSyubetu = ""
                        Else
                            voShisakuSyubetu = vo.ShisakuSyubetu
                        End If
                        If StringUtil.Equals(Hvo.ShisakuGousya, vo.ShisakuGousya) And _
                           StringUtil.Equals(hvoShisakuSyubetu, voShisakuSyubetu) Then
                            strGousya = "OK"
                            'さらに仕様情報（大、中、小）がマッチしているかチェック。
                            If Hvo.ShisakuRetuKoumokuNameDai = vo.ShisakuRetuKoumokuNameDai And _
                               Hvo.ShisakuRetuKoumokuNameChu = vo.ShisakuRetuKoumokuNameChu And _
                               Hvo.ShisakuRetuKoumokuName = vo.ShisakuRetuKoumokuName Then
                                lngChgDataCnt = lngChgDataCnt + 1
                                Exit For
                            End If

                        End If
                    Next
                End If

                If strGousya = "OK" Then
                    'シンボル更新
                    If lngChgDataCnt = 0 Then
                        If StringUtil.IsNotEmpty(Hvo.ShisakuTekiyou) Then
                            '今回にあって前回にないので追加データとみなす。そしてシンボルへ変更をセット。
                            xls.SetValue(1, Hvo.HyojijunNo + lngChgData + lngStartRow, _
                                         1, Hvo.HyojijunNo + lngChgData + lngStartRow, "変更")
                            '-----------------------------------------------------
                            '背景色を変更
                            xls.SetValue(Hvo.ShisakuSoubiHyoujiNo + lngStartColumn, Hvo.HyojijunNo + lngChgData + lngStartRow, _
                                         Hvo.ShisakuSoubiHyoujiNo + lngStartColumn, Hvo.HyojijunNo + lngChgData + lngStartRow, _
                                            Hvo.ShisakuTekiyou)
                            '-----------------------------------------------------
                            basicUpdateFlg = "UPD"
                        End If
                    End If
                    '=================================================================================================================
                Else
                    'シンボル更新
                    If lngChgDataCnt = 0 Then
                        '連番
                        xls.SetValue(2, Hvo.HyojijunNo + lngChgData + lngStartRow, _
                                     2, Hvo.HyojijunNo + lngChgData + lngStartRow, Hvo.HyojijunNo + 1)
                        '種別
                        xls.SetValue(3, Hvo.HyojijunNo + lngChgData + lngStartRow, _
                                     3, Hvo.HyojijunNo + lngChgData + lngStartRow, Hvo.ShisakuSyubetu)
                        '号車
                        xls.SetValue(4, Hvo.HyojijunNo + lngChgData + lngStartRow, _
                                     4, Hvo.HyojijunNo + lngChgData + lngStartRow, Hvo.ShisakuGousya)
                        If StringUtil.IsNotEmpty(Hvo.ShisakuTekiyou) Then
                            '今回にあって前回にないので追加データとみなす。そしてシンボルへ変更をセット。
                            xls.SetValue(1, Hvo.HyojijunNo + lngChgData + lngStartRow, _
                                         1, Hvo.HyojijunNo + lngChgData + lngStartRow, "変更")
                            '-----------------------------------------------------
                            '背景色を変更
                            xls.SetValue(Hvo.ShisakuSoubiHyoujiNo + lngStartColumn, Hvo.HyojijunNo + lngChgData + lngStartRow, _
                                         Hvo.ShisakuSoubiHyoujiNo + lngStartColumn, Hvo.HyojijunNo + lngChgData + lngStartRow, _
                                            Hvo.ShisakuTekiyou)
                            '-----------------------------------------------------
                            basicUpdateFlg = "UPD"
                        End If
                    End If
                    '=================================================================================================================
                End If
            Next


            '追加された装備仕様が無いか確認する。
            '
            'For Each vo As EventSoubiVo In basicList   '今回完成車情報
            '    'カウントクリア
            '    lngChgDataCnt = 0
            '    '-----------------------------------------------------
            '    '比較対象のイベントを区分（大、中、小）で抽出する。
            '    If HbasicList.Count <> 0 Then
            '        For Each Hvo As EventSoubiVo In HbasicList   '前回装備仕様情報
            '            '-----------------------------------------------------
            '            If StringUtil.Equals(vo.ShisakuRetuKoumokuNameDai, Hvo.ShisakuRetuKoumokuNameDai) And _
            '               StringUtil.Equals(vo.ShisakuRetuKoumokuNameChu, Hvo.ShisakuRetuKoumokuNameChu) And _
            '               StringUtil.Equals(vo.ShisakuRetuKoumokuName, Hvo.ShisakuRetuKoumokuName) Then
            '                voBasic = vo
            '                lngChgDataCnt = lngChgDataCnt + 1
            '                Exit For
            '            End If
            '        Next
            '    End If
            '    '-----------------------------------------------------

            '    '=================================================================================================================
            '    '号車が今回改訂№にあって、比較改訂№にない場合は追加として出力する。
            '    If lngChgDataCnt = 0 Then
            '        basicUpdateFlg = "UPD"
            '        'データ件数カウント
            '        lngDataCnt = lngDataCnt + 1
            '        '-----------------------------------------------------
            '        ''-----------------------------------------------------
            '        ''ベース車の情報を取得する。（今回登録）
            '        'BaseVo = eventBaseCarDao.FindShisakuEventBaseCar(vo.ShisakuEventCode, vo.HyojijunNo)
            '        '-----------------------------------------------------
            '        '-----------------------------------------------------
            '        '連番
            '        xls.SetValue(2, lngDataCnt + lngChgData + lngStartRow, _
            '                     2, lngDataCnt + lngChgData + lngStartRow, lngDataCnt)
            '        '種別
            '        xls.SetValue(3, lngDataCnt + lngChgData + lngStartRow, _
            '                     3, lngDataCnt + lngChgData + lngStartRow, vo.ShisakuSyubetu)
            '        '号車
            '        xls.SetValue(4, lngDataCnt + lngChgData + lngStartRow, _
            '                     4, lngDataCnt + lngChgData + lngStartRow, vo.ShisakuGousya)

            '        '該当列を検索
            '        Dim intHyojiRetu As Integer = 0
            '        For i As Integer = lngStartColumn To 250
            '            If StringUtil.Equals(xls.GetValue(i, 8, i, 8), vo.ShisakuRetuKoumokuNameDai) And _
            '               StringUtil.Equals(xls.GetValue(i, 9, i, 9), vo.ShisakuRetuKoumokuNameChu) And _
            '               StringUtil.Equals(xls.GetValue(i, 10, i, 10), vo.ShisakuRetuKoumokuName) Then
            '                intHyojiRetu = i
            '                Exit For
            '            End If
            '        Next i

            '        xls.GetValue(lngStartColumn, 8, lngStartColumn, 8)
            '        '適用
            '        xls.SetValue(intHyojiRetu, vo.HyojijunNo + lngStartRow, _
            '                     intHyojiRetu, vo.HyojijunNo + lngStartRow, vo.ShisakuTekiyou)

            '        '-----------------------------------------------------
            '        '背景色を変更
            '        '   開発符号～最後の列まで
            '        xls.SetBackColor(2, vo.HyojijunNo + lngStartRow, _
            '                         200, vo.HyojijunNo + lngStartRow, RGB(204, 255, 204))   '追加カラー
            '        '-----------------------------------------------------
            '        'シンボル更新
            '        '今回にあって前回にないので追加データとみなす。そしてシンボルへ「追加」をセット。
            '        xls.SetValue(1, vo.HyojijunNo + lngStartRow, _
            '                     1, vo.HyojijunNo + lngStartRow, "追加")

            '    End If
            '    '===============================================================================================
            'Next

            Return basicUpdateFlg

        End Function

        ''' <summary>
        ''' タイトル部作成（特別装備仕様情報）
        ''' </summary>
        ''' <param name="xls"></param>
        ''' <remarks></remarks>
        Private Sub setSheetTitleSPECIAL(ByVal xls As ShisakuExcel)

            'xls.SetValueのパラメータは－－－＞　startCol ,startRow ,endCol ,endRow ,value

            'シンボル
            xls.SetValue(1, 8, 1, 8, "シンボル")
            xls.MergeCells(1, 8, 1, 10, True)
            xls.SetOrientation(1, 8, 1, 8, ShisakuExcel.XlOrientation.xlHorizontal, False, True)   '横
            xls.SetAlignment(1, 8, 1, 8, XlHAlign.xlHAlignLeft, XlVAlign.xlVAlignBottom, False, True) '位置
            '連番
            xls.SetValue(2, 8, 2, 8, "連番")
            xls.MergeCells(2, 8, 2, 10, True)
            xls.SetOrientation(2, 8, 2, 8, ShisakuExcel.XlOrientation.xlHorizontal, False, True)   '横
            xls.SetAlignment(2, 8, 2, 8, XlHAlign.xlHAlignLeft, XlVAlign.xlVAlignBottom, False, True) '位置
            '種別
            xls.SetValue(3, 8, 3, 8, "種別")
            xls.MergeCells(3, 8, 3, 10, True)
            xls.SetOrientation(3, 8, 3, 8, ShisakuExcel.XlOrientation.xlHorizontal, False, True)   '横
            xls.SetAlignment(3, 8, 3, 8, XlHAlign.xlHAlignLeft, XlVAlign.xlVAlignBottom, False, True) '位置
            '号車
            xls.SetValue(4, 8, 4, 8, "号車")
            xls.MergeCells(4, 8, 4, 10, True)
            xls.SetOrientation(4, 8, 4, 8, ShisakuExcel.XlOrientation.xlHorizontal, False, True)   '横
            xls.SetAlignment(4, 8, 4, 8, XlHAlign.xlHAlignLeft, XlVAlign.xlVAlignBottom, False, True) '位置

            '書式設定
            xls.SetOrientation(lngStartColumn, 8, lngStartColumn + 200, 10, ShisakuExcel.XlOrientation.xlDownward, True, False)   '縦書き
            xls.SetAlignment(lngStartColumn, 8, lngStartColumn + 200, 10, XlHAlign.xlHAlignCenter, XlVAlign.xlVAlignTop, True, False) '位置
            xls.SetRowHeight(8, 8, 24)
            xls.SetRowHeight(9, 9, 43.5)
            xls.SetRowHeight(10, 10, 180.75)

            '   特別装備仕様列作成
            Dim lngDatacnt As Long = 0
            Dim lngspecialCnt As Long = 0
            Dim lngChgDataCnt As Integer = 0
            Dim vospecial As New EventSoubiVo
            If specialTitelList.Count <> 0 Or HspecialTitelList.Count <> 0 Then
                '   基本装備仕様項目列分作成
                For Each vo As EventSoubiVo In HspecialTitelList
                    '項目列名をセット
                    xls.SetValue(lngStartColumn + vo.ShisakuSoubiHyoujiNo, 8, _
                                 lngStartColumn + vo.ShisakuSoubiHyoujiNo, 8, vo.ShisakuRetuKoumokuNameDai)
                    xls.SetValue(lngStartColumn + vo.ShisakuSoubiHyoujiNo, 9, _
                                 lngStartColumn + vo.ShisakuSoubiHyoujiNo, 9, vo.ShisakuRetuKoumokuNameChu)
                    xls.SetValue(lngStartColumn + vo.ShisakuSoubiHyoujiNo, 10, _
                                 lngStartColumn + vo.ShisakuSoubiHyoujiNo, 10, vo.ShisakuRetuKoumokuName)
                    '列の最大値を求める。
                    If lngDatacnt < vo.ShisakuSoubiHyoujiNo Then
                        lngDatacnt = vo.ShisakuSoubiHyoujiNo
                    End If
                    '=================================================================================================================
                    '変化点はバックカラーを変更する。
                    'カウントクリア
                    lngChgDataCnt = 0
                    '-----------------------------------------------------
                    '今回取込ファイルのイベントを号車で抽出する。
                    If specialTitelList.Count <> 0 Then
                        For Each hikakuVo As EventSoubiVo In specialTitelList   '今回登録時のデータ
                            '
                            If hikakuVo.ShisakuRetuKoumokuNameDai Is Nothing Then hikakuVo.ShisakuRetuKoumokuNameDai = ""
                            If hikakuVo.ShisakuRetuKoumokuNameChu Is Nothing Then hikakuVo.ShisakuRetuKoumokuNameChu = ""
                            If hikakuVo.ShisakuRetuKoumokuName Is Nothing Then hikakuVo.ShisakuRetuKoumokuName = ""
                            '
                            If vo.ShisakuRetuKoumokuNameDai Is Nothing Then vo.ShisakuRetuKoumokuNameDai = ""
                            If vo.ShisakuRetuKoumokuNameChu Is Nothing Then vo.ShisakuRetuKoumokuNameChu = ""
                            If vo.ShisakuRetuKoumokuName Is Nothing Then vo.ShisakuRetuKoumokuName = ""
                            '
                            If StringUtil.Equals(hikakuVo.ShisakuRetuKoumokuNameDai, vo.ShisakuRetuKoumokuNameDai) And _
                               StringUtil.Equals(hikakuVo.ShisakuRetuKoumokuNameChu, vo.ShisakuRetuKoumokuNameChu) And _
                               StringUtil.Equals(hikakuVo.ShisakuRetuKoumokuName, vo.ShisakuRetuKoumokuName) Then
                                vospecial = vo
                                lngChgDataCnt = lngChgDataCnt + 1
                                Exit For
                            End If
                        Next
                    End If

                    'シンボル更新
                    If lngChgDataCnt = 0 Then
                        '前回にあって今回にないので削除データとみなす。
                        '-----------------------------------------------------
                        '背景色を変更
                        '   ～特別装備仕様の最後の列まで
                        xls.SetBackColor(lngStartColumn + lngDatacnt, 8, lngStartColumn + lngDatacnt, 10, _
                                         RGB(192, 192, 192))    '削除カラー
                        '-----------------------------------------------------
                    End If
                Next
            End If
            '================================================================================================================
            '追加された装備仕様が無いか確認する。
            For Each vo As EventSoubiVo In specialTitelList   '今回基本装備情報
                'カウントクリア
                lngChgDataCnt = 0
                '-----------------------------------------------------
                '比較対象のイベントを号車で抽出する。
                If HspecialTitelList.Count <> 0 Then
                    For Each Hvo As EventSoubiVo In HspecialTitelList   '前回基本装備情報
                        '-----------------------------------------------------
                        If Hvo.ShisakuRetuKoumokuNameDai Is Nothing Then Hvo.ShisakuRetuKoumokuNameDai = ""
                        If Hvo.ShisakuRetuKoumokuNameChu Is Nothing Then Hvo.ShisakuRetuKoumokuNameChu = ""
                        If Hvo.ShisakuRetuKoumokuName Is Nothing Then Hvo.ShisakuRetuKoumokuName = ""
                        '
                        If vo.ShisakuRetuKoumokuNameDai Is Nothing Then vo.ShisakuRetuKoumokuNameDai = ""
                        If vo.ShisakuRetuKoumokuNameChu Is Nothing Then vo.ShisakuRetuKoumokuNameChu = ""
                        If vo.ShisakuRetuKoumokuName Is Nothing Then vo.ShisakuRetuKoumokuName = ""
                        '
                        If StringUtil.Equals(Hvo.ShisakuRetuKoumokuNameDai, vo.ShisakuRetuKoumokuNameDai) And _
                           StringUtil.Equals(Hvo.ShisakuRetuKoumokuNameChu, vo.ShisakuRetuKoumokuNameChu) And _
                           StringUtil.Equals(Hvo.ShisakuRetuKoumokuName, vo.ShisakuRetuKoumokuName) Then
                            vospecial = vo
                            lngChgDataCnt = lngChgDataCnt + 1
                            Exit For
                        End If
                    Next
                End If
                '-----------------------------------------------------

                '=================================================================================================================
                '装備仕様が今回改訂にあって、比較改訂にない場合は追加として出力する。
                If lngChgDataCnt = 0 Then

                    '-----------------------------------------------------
                    '項目列名をセット
                    xls.SetValue(lngStartColumn + vo.ShisakuSoubiHyoujiNo, 8, _
                                 lngStartColumn + vo.ShisakuSoubiHyoujiNo, 8, vo.ShisakuRetuKoumokuNameDai)
                    xls.SetValue(lngStartColumn + vo.ShisakuSoubiHyoujiNo, 9, _
                                 lngStartColumn + vo.ShisakuSoubiHyoujiNo, 9, vo.ShisakuRetuKoumokuNameChu)
                    xls.SetValue(lngStartColumn + vo.ShisakuSoubiHyoujiNo, 10, _
                                 lngStartColumn + vo.ShisakuSoubiHyoujiNo, 10, vo.ShisakuRetuKoumokuName)
                    '-----------------------------------------------------
                    '列の最大値を求める。
                    If lngDatacnt < vo.ShisakuSoubiHyoujiNo Then
                        lngDatacnt = vo.ShisakuSoubiHyoujiNo
                    End If
                    '-----------------------------------------------------
                    '今回にあって前回にないので追加データとみなす。
                    '背景色を変更
                    xls.SetBackColor(lngStartColumn + vo.ShisakuSoubiHyoujiNo, 8, _
                                     lngStartColumn + vo.ShisakuSoubiHyoujiNo, _
                                     baseList.Item(baseList.Count - 1).HyojijunNo + lngStartRow, _
                                     RGB(204, 255, 204))   '追加カラー
                    '-----------------------------------------------------

                    'データ件数カウント
                    'lngDatacnt = lngDatacnt + 1

                End If
                '===============================================================================================
            Next

            '罫線を引く
            xls.SetLine(1, 8, lngStartColumn + lngDatacnt, baseList.Item(baseList.Count - 1).HyojijunNo + lngStartRow, _
                        XlBordersIndex.xlEdgeTop, XlLineStyle.xlContinuous, XlBorderWeight.xlThin) '上
            xls.SetLine(1, 8, lngStartColumn + lngDatacnt, baseList.Item(baseList.Count - 1).HyojijunNo + lngStartRow, _
                        XlBordersIndex.xlEdgeBottom, XlLineStyle.xlContinuous, XlBorderWeight.xlThin) '下
            xls.SetLine(1, 8, lngStartColumn + lngDatacnt, baseList.Item(baseList.Count - 1).HyojijunNo + lngStartRow, _
                        XlBordersIndex.xlEdgeLeft, XlLineStyle.xlContinuous, XlBorderWeight.xlThin) '左
            xls.SetLine(1, 8, lngStartColumn + lngDatacnt, baseList.Item(baseList.Count - 1).HyojijunNo + lngStartRow, _
                        XlBordersIndex.xlEdgeRight, XlLineStyle.xlContinuous, XlBorderWeight.xlThin) '右
            xls.SetLine(1, 8, lngStartColumn + lngDatacnt, baseList.Item(baseList.Count - 1).HyojijunNo + lngStartRow, _
                        XlBordersIndex.xlInsideHorizontal, XlLineStyle.xlContinuous, XlBorderWeight.xlThin) '水平
            xls.SetLine(1, 8, lngStartColumn + lngDatacnt, baseList.Item(baseList.Count - 1).HyojijunNo + lngStartRow, _
                        XlBordersIndex.xlInsideVertical, XlLineStyle.xlContinuous, XlBorderWeight.xlThin) '垂直
            xls.SetAlignment(lngStartColumn, lngStartRow, _
                             lngStartColumn + lngDatacnt, baseList.Item(baseList.Count - 1).HyojijunNo + lngStartRow, _
                             XlHAlign.xlHAlignCenter, ShisakuExcel.XlVAlign.xlVAlignCenter, True, False) '位置

        End Sub


        ''' <summary>
        ''' EXCEL出力・ボディーパート(特別装備仕様シート)
        ''' </summary>
        ''' <param name="xls"></param>
        ''' <remarks></remarks>
        Private Function setSheet1BodyPartSPECIAL(ByVal xls As ShisakuExcel) As String

            'xls.SetValueのパラメータは－－－＞　startCol ,startRow ,endCol ,endRow ,value
            '初期設定
            Dim lngDataCnt As Long = 0
            Dim lngDataCnt2 As Long = 0
            Dim lngChgDataCnt As Long = 0
            Dim lngChgData As Long = 0
            Dim vospecial As New EventSoubiVo
            Dim getColorName As New EventEdit.Dao.EventEditBaseCarDaoImpl
            Dim eventBaseCarDao As EventEditCompleteCarRirekiDao = New EventEditCompleteCarRirekiDaoImpl()
            'Dim BaseVo As TShisakuEventBaseVo
            'Dim HBaseVo As TShisakuEventBaseKaiteiVo

            Dim HldGousya As String = ""

            '戻り値
            '   変更無時はブランク
            Dim specialUpdateFlg As String = ""

            '   基本装備仕様情報ＥＸＣＥＬ貼り付け
            '       比較元の情報より貼り付ける。
            For Each Hvo As EventSoubiVo In HspecialList   '前回の基本装備仕様情報
                '号車チェックとして使用
                Dim strGousya As String = Nothing
                '変更フラグとして使用
                Dim strUpdateFlg As String = Nothing

                '-----------------------------------------------------
                If Not StringUtil.Equals(HldGousya, Hvo.ShisakuGousya) Then
                    'データ件数カウント
                    lngDataCnt2 = lngDataCnt2 + 1
                End If
                HldGousya = Hvo.ShisakuGousya
                '-----------------------------------------------------

                ''-----------------------------------------------------
                ''ベース車の情報を取得する。（前回登録）
                'HBaseVo = eventBaseCarDao.FindShisakuEventBaseKaiteiCar(Hvo.ShisakuEventCode, Hvo.HyojijunNo)
                '-----------------------------------------------------

                '-----------------------------------------------------
                'シンボル

                If StringUtil.IsEmpty(xls.GetValue(4, Hvo.HyojijunNo + lngChgData + lngStartRow, _
                             4, Hvo.HyojijunNo + lngChgData + lngStartRow)) Then

                    '連番
                    xls.SetValue(2, Hvo.HyojijunNo + lngChgData + lngStartRow, _
                                 2, Hvo.HyojijunNo + lngChgData + lngStartRow, Hvo.HyojijunNo + 1)
                    '種別
                    xls.SetValue(3, Hvo.HyojijunNo + lngChgData + lngStartRow, _
                                 3, Hvo.HyojijunNo + lngChgData + lngStartRow, Hvo.ShisakuSyubetu)
                    '号車
                    xls.SetValue(4, Hvo.HyojijunNo + lngChgData + lngStartRow, _
                                 4, Hvo.HyojijunNo + lngChgData + lngStartRow, Hvo.ShisakuGousya)

                End If
                If StringUtil.IsNotEmpty(Hvo.ShisakuTekiyou) Then
                    '適用
                    xls.SetValue(lngStartColumn + Hvo.ShisakuSoubiHyoujiNo, Hvo.HyojijunNo + lngChgData + lngStartRow, _
                                 lngStartColumn + Hvo.ShisakuSoubiHyoujiNo, Hvo.HyojijunNo + lngChgData + lngStartRow, _
                                 Hvo.ShisakuTekiyou)
                End If
                '=================================================================================================================
                '変化点はバックカラーを変更する。
                'カウントクリア
                lngChgDataCnt = 0
                '-----------------------------------------------------
                '今回取込ファイルのイベントを号車で抽出する。
                If kanseiList.Count <> 0 Then
                    For Each vo As EventSoubiVo In specialList   '今回登録時のデータ
                        ''-----------------------------------------------------
                        ''ベース車の情報を取得する。（今回登録）
                        'BaseVo = eventBaseCarDao.FindShisakuEventBaseCar(vo.ShisakuEventCode, vo.HyojijunNo)
                        '-----------------------------------------------------
                        Dim hvoShisakuSyubetu As String = ""
                        If StringUtil.IsEmpty(Hvo.ShisakuSyubetu) Then
                            hvoShisakuSyubetu = ""
                        Else
                            hvoShisakuSyubetu = Hvo.ShisakuSyubetu
                        End If
                        Dim voShisakuSyubetu As String = ""
                        If StringUtil.IsEmpty(vo.ShisakuSyubetu) Then
                            voShisakuSyubetu = ""
                        Else
                            voShisakuSyubetu = vo.ShisakuSyubetu
                        End If
                        If StringUtil.Equals(Hvo.ShisakuGousya, vo.ShisakuGousya) Then
                            'If StringUtil.Equals(Hvo.ShisakuGousya, vo.ShisakuGousya) And _
                            '   StringUtil.Equals(hvoShisakuSyubetu, voShisakuSyubetu) Then
                            strGousya = "OK"
                            'さらに仕様情報（大、中、小）がマッチしているかチェック。
                            If Hvo.ShisakuRetuKoumokuNameDai = vo.ShisakuRetuKoumokuNameDai And _
                               Hvo.ShisakuRetuKoumokuNameChu = vo.ShisakuRetuKoumokuNameChu And _
                               Hvo.ShisakuRetuKoumokuName = vo.ShisakuRetuKoumokuName Then
                                vospecial = vo
                                lngChgDataCnt = lngChgDataCnt + 1
                                Exit For
                            End If

                        End If
                    Next
                End If

                If strGousya = "OK" Then
                    'シンボル更新
                    If lngChgDataCnt = 0 Then
                        '前回にあって今回にないので削除データとみなす。そしてシンボルへ削除をセット。
                        xls.SetValue(1, Hvo.HyojijunNo + lngChgData + lngStartRow, _
                                     1, Hvo.HyojijunNo + lngChgData + lngStartRow, "変更")
                        '-----------------------------------------------------
                        '背景色を変更
                        '   ～特別装備仕様の最後の列まで
                        xls.SetBackColor(Hvo.ShisakuSoubiHyoujiNo + lngStartColumn, 8, _
                                         Hvo.ShisakuSoubiHyoujiNo + lngStartColumn, 10, RGB(192, 192, 192))    '削除カラー
                        '-----------------------------------------------------
                        specialUpdateFlg = "UPD"
                    Else
                        '-----------------------------------------------------
                        '適用：比較して差異があればバックカラーを変更
                        '　Nothingとブランクが同居する可能性があるのでブランクに統一
                        If StringUtil.IsEmpty(Hvo.ShisakuTekiyou) Then Hvo.ShisakuTekiyou = ""
                        If StringUtil.IsEmpty(vospecial.ShisakuTekiyou) Then vospecial.ShisakuTekiyou = ""

                        If Not StringUtil.Equals(Hvo.ShisakuTekiyou, vospecial.ShisakuTekiyou)  Then
                            xls.SetBackColor(Hvo.ShisakuSoubiHyoujiNo + lngStartColumn, Hvo.HyojijunNo + lngChgData + lngStartRow, _
                                             Hvo.ShisakuSoubiHyoujiNo + lngStartColumn, Hvo.HyojijunNo + lngChgData + +lngStartRow, _
                                             RGB(255, 153, 204))
                            strUpdateFlg = "CHANGE"
                            xls.SetValue(Hvo.ShisakuSoubiHyoujiNo + lngStartColumn, Hvo.HyojijunNo + lngChgData + lngStartRow, _
                                         Hvo.ShisakuSoubiHyoujiNo + lngStartColumn, Hvo.HyojijunNo + lngChgData + lngStartRow, _
                                            vospecial.ShisakuTekiyou)
                            Dim MaeAtai As String = ""
                            If StringUtil.IsEmpty(Hvo.ShisakuTekiyou) Then
                                MaeAtai = "空白"    'ＶＯ値が空なら空白を設定
                            Else
                                MaeAtai = Hvo.ShisakuTekiyou
                            End If
                            xls.SetComment("変更前の値：" & MaeAtai, _
                                            Hvo.ShisakuSoubiHyoujiNo + lngStartColumn, Hvo.HyojijunNo + lngChgData + lngStartRow, _
                                            Hvo.ShisakuSoubiHyoujiNo + lngStartColumn, Hvo.HyojijunNo + lngChgData + lngStartRow)   '変更前の値をコメントにセット
                        End If

                        If StringUtil.Equals(baseList.Item(lngDataCnt2 - 1).ShisakuSyubetu, "D") Then
                            '前回にあって今回にないので削除データとみなす。そしてシンボルへ削除をセット。
                            xls.SetValue(1, Hvo.HyojijunNo + lngChgData + lngStartRow, _
                                         1, Hvo.HyojijunNo + lngChgData + lngStartRow, "削除")
                            '-----------------------------------------------------
                            '背景色を変更
                            '   ～特別装備仕様の最後の列まで
                            xls.SetBackColor(2, Hvo.HyojijunNo + lngChgData + lngStartRow, _
                                             200, Hvo.HyojijunNo + lngChgData + lngStartRow, RGB(192, 192, 192))    '削除カラー
                            '-----------------------------------------------------
                            strUpdateFlg = ""   'クリア
                        End If
                        '種別
                        xls.SetValue(3, Hvo.HyojijunNo + lngChgData + lngStartRow, _
                                     3, Hvo.HyojijunNo + lngChgData + lngStartRow, baseList.Item(lngDataCnt2 - 1).ShisakuSyubetu)

                        ''変更行のために１カウントＵＰ
                        'If StringUtil.Equals(strUpdateFlg, "CHANGE") Then lngChgData = lngChgData + 1

                        If strUpdateFlg = "CHANGE" Then
                            specialUpdateFlg = "UPD"
                            '今回にあって前回にもあって変更箇所があるならシンボルへ「変更」をセット。
                            xls.SetValue(1, Hvo.HyojijunNo + lngChgData + lngStartRow, _
                                         1, Hvo.HyojijunNo + lngChgData + lngStartRow, "変更")
                        End If
                    End If
                    '=================================================================================================================
                Else
                    If Not StringUtil.Equals(xls.GetValue(1, Hvo.HyojijunNo + lngChgData + lngStartRow, _
                                     1, Hvo.HyojijunNo + lngChgData + lngStartRow), "削除") Then
                        '前回にあって今回にない号車なので削除データとみなす。そしてシンボルへ削除をセット。
                        xls.SetValue(1, Hvo.HyojijunNo + lngChgData + lngStartRow, _
                                     1, Hvo.HyojijunNo + lngChgData + lngStartRow, "削除")
                        '-----------------------------------------------------
                        '背景色を変更
                        '   ～特別装備仕様の最後の列まで
                        xls.SetBackColor(2, Hvo.HyojijunNo + lngChgData + lngStartRow, _
                                         200, Hvo.HyojijunNo + lngChgData + lngStartRow, RGB(192, 192, 192))    '削除カラー
                        '-----------------------------------------------------
                    End If

                    specialUpdateFlg = "UPD"
                End If
            Next


            For Each Hvo As EventSoubiVo In specialList   '今回の特別装備仕様情報
                '号車チェックとして使用
                Dim strGousya As String = Nothing
                '変更フラグとして使用
                Dim strUpdateFlg As String = Nothing
                'データ件数カウント
                lngDataCnt = lngDataCnt + 1

                '=================================================================================================================
                '変化点はバックカラーを変更する。
                'カウントクリア
                lngChgDataCnt = 0
                '-----------------------------------------------------
                '前回取込ファイルのイベントを号車で抽出する。
                If HspecialList.Count <> 0 Then
                    For Each vo As EventSoubiVo In HspecialList   '今回登録時のデータ
                        ''-----------------------------------------------------
                        Dim hvoShisakuSyubetu As String = ""
                        If StringUtil.IsEmpty(Hvo.ShisakuSyubetu) Then
                            hvoShisakuSyubetu = ""
                        Else
                            hvoShisakuSyubetu = Hvo.ShisakuSyubetu
                        End If
                        Dim voShisakuSyubetu As String = ""
                        If StringUtil.IsEmpty(vo.ShisakuSyubetu) Then
                            voShisakuSyubetu = ""
                        Else
                            voShisakuSyubetu = vo.ShisakuSyubetu
                        End If
                        If StringUtil.Equals(Hvo.ShisakuGousya, vo.ShisakuGousya) And _
                           StringUtil.Equals(hvoShisakuSyubetu, voShisakuSyubetu) Then
                            strGousya = "OK"
                            'さらに仕様情報（大、中、小）がマッチしているかチェック。
                            If Hvo.ShisakuRetuKoumokuNameDai = vo.ShisakuRetuKoumokuNameDai And _
                               Hvo.ShisakuRetuKoumokuNameChu = vo.ShisakuRetuKoumokuNameChu And _
                               Hvo.ShisakuRetuKoumokuName = vo.ShisakuRetuKoumokuName Then
                                'vospecial = Hvo
                                lngChgDataCnt = lngChgDataCnt + 1
                                Exit For
                            End If

                        End If
                    Next
                End If

                If strGousya = "OK" Then
                    'シンボル更新
                    If lngChgDataCnt = 0 Then
                        If StringUtil.IsNotEmpty(Hvo.ShisakuTekiyou) Then
                            '今回にあって前回にないので追加データとみなす。そしてシンボルへ変更をセット。
                            xls.SetValue(1, Hvo.HyojijunNo + lngChgData + lngStartRow, _
                                         1, Hvo.HyojijunNo + lngChgData + lngStartRow, "変更")
                            '-----------------------------------------------------
                            '背景色を変更
                            xls.SetValue(Hvo.ShisakuSoubiHyoujiNo + lngStartColumn, Hvo.HyojijunNo + lngChgData + lngStartRow, _
                                         Hvo.ShisakuSoubiHyoujiNo + lngStartColumn, Hvo.HyojijunNo + lngChgData + lngStartRow, _
                                            Hvo.ShisakuTekiyou)
                            '-----------------------------------------------------
                            specialUpdateFlg = "UPD"
                        End If
                    End If
                    '=================================================================================================================
                Else
                    'シンボル更新
                    If lngChgDataCnt = 0 Then
                        '連番
                        xls.SetValue(2, Hvo.HyojijunNo + lngChgData + lngStartRow, _
                                     2, Hvo.HyojijunNo + lngChgData + lngStartRow, Hvo.HyojijunNo + 1)
                        '種別
                        xls.SetValue(3, Hvo.HyojijunNo + lngChgData + lngStartRow, _
                                     3, Hvo.HyojijunNo + lngChgData + lngStartRow, Hvo.ShisakuSyubetu)
                        '号車
                        xls.SetValue(4, Hvo.HyojijunNo + lngChgData + lngStartRow, _
                                     4, Hvo.HyojijunNo + lngChgData + lngStartRow, Hvo.ShisakuGousya)
                        If StringUtil.IsNotEmpty(Hvo.ShisakuTekiyou) Then
                            '今回にあって前回にないので追加データとみなす。そしてシンボルへ変更をセット。
                            xls.SetValue(1, Hvo.HyojijunNo + lngChgData + lngStartRow, _
                                         1, Hvo.HyojijunNo + lngChgData + lngStartRow, "変更")
                            '-----------------------------------------------------
                            '背景色を変更
                            xls.SetValue(Hvo.ShisakuSoubiHyoujiNo + lngStartColumn, Hvo.HyojijunNo + lngChgData + lngStartRow, _
                                         Hvo.ShisakuSoubiHyoujiNo + lngStartColumn, Hvo.HyojijunNo + lngChgData + lngStartRow, _
                                            Hvo.ShisakuTekiyou)
                            '-----------------------------------------------------
                            basicUpdateFlg = "UPD"
                        End If
                    End If
                    '=================================================================================================================
                End If
            Next

            Return specialUpdateFlg

        End Function

        Public Function ShimukechiShimukeHenkan(ByVal strShimuke As String, ByVal strBaseShimuke As String)
            '仕向地・仕向け変換
            Dim strShimukechiShimuke As String = ""
            If strShimuke = "0" Then
                strShimukechiShimuke = _
                    TShisakuEventBaseSeisakuIchiranVoHelper.BaseShimukechiShimukeName.KOKUNAI
            ElseIf strShimuke = "1" Then
                strShimukechiShimuke = _
                    TShisakuEventBaseSeisakuIchiranVoHelper.BaseShimukechiShimukeName.HOKUBEI
            ElseIf strShimuke = "2" Then
                If strBaseShimuke = "KA" Then
                    strShimukechiShimuke = _
                        TShisakuEventBaseSeisakuIchiranVoHelper.BaseShimukechiShimukeName.GOSYU
                Else
                    strShimukechiShimuke = _
                        TShisakuEventBaseSeisakuIchiranVoHelper.BaseShimukechiShimukeName.OHSYUMIGI
                End If
            ElseIf strShimuke = "3" Then
                If strBaseShimuke = "EH" Or strBaseShimuke = "ET" Then
                    strShimukechiShimuke = _
                        TShisakuEventBaseSeisakuIchiranVoHelper.BaseShimukechiShimukeName.CHUGOKU
                Else
                    strShimukechiShimuke = _
                        TShisakuEventBaseSeisakuIchiranVoHelper.BaseShimukechiShimukeName.OHSYUHIDARI
                End If
            End If
            'ブランクなら受け取った仕向けを返す
            If StringUtil.IsEmpty(strShimukechiShimuke) Then strShimukechiShimuke = strShimuke

            Return strShimukechiShimuke
        End Function

    End Class

End Namespace