Imports ShisakuCommon.Ui
Imports EventSakusei.ShisakuBuhinEditBlock.Dao
Imports ShisakuCommon.Db.EBom.Vo
Imports ShisakuCommon
Imports EBom.Excel
Imports EventSakusei.ShisakuBuhinEdit.Kosei.Logic

''↓↓2014/09/18 酒井 ADD BEGIN
Imports ShisakuCommon.Db.EBom.Dao
Imports ShisakuCommon.Db.EBom.Dao.Impl
''↑↑2014/09/18 酒井 ADD END

Namespace ShisakuBuhinEditBlock.Export2Excel

    ''' <summary>
    ''' AL比較Excel
    ''' </summary>
    ''' <remarks></remarks>
    Public Class ExportShisakuBuhinEditALConditionExcel

#Region "メンバ変数"

        ''' <summary>試作イベントコード</summary>
        Private shisakuEventCode As String
        ''' <summary>試作部課コード</summary>
        Private shisakuBukaCode As String
        ''' <summary>試作ブロックNo</summary>
        Private shisakuBlockNo As String
        ''' <summary>試作ブロックNo改訂No(新)</summary>
        Private newShisakuBlockNoKaiteiNo As String
        ''' <summary>試作ブロックNo改訂No(旧)</summary>
        Private oldShisakuBlockNoKaiteiNo As String
        ''' <summary>号車の開始位置</summary>
        Private pointBodyTitleGousya As EditBlock2ExcelPointVo
        ''' <summary>号車列カウント数</summary>
        Private colTitleGousyaCount As Integer
        ''' <summary>装備列カウント数</summary>
        Private colTitleSoubiCount As Integer
        ''' <summary>メモ列カウント数</summary>
        Private colTitleMemoCount As Integer
        ''' <summary>INSTL列カウント数</summary>
        Private colTitleInsuCount As Integer
        ''' <summary>タイトルリスト</summary>
        Private titleVos As List(Of EditBlock2ExcelTitle1Vo)
        ''' <summary>装備仕様タイトルリスト</summary>
        Private title2Vos As List(Of EditBlock2ExcelTitle2Vo)
        ''' <summary>メモタイトルリスト</summary>
        Private title3Vos As List(Of TShisakuSekkeiBlockMemoVo)
        ''' <summary>ベースの改訂No</summary>
        Private kaiNo As String
        ''' <summary>INSTLタイトルリスト</summary>
        Private title4VosA As List(Of TShisakuSekkeiBlockInstlVo)
        ''' <summary>INSTLタイトルリスト</summary>
        Private title4VosB As List(Of TShisakuSekkeiBlockInstlVo)
        ''' <summary>装備タイトル開始位置</summary>
        Private pointBodyTitleSoubi As EditBlock2ExcelPointVo
        ''' <summary>メモタイトル開始位置</summary>
        Private pointBodyTitleMemo As EditBlock2ExcelPointVo
        ''' <summary>員数タイトル開始位置</summary>
        Private pointBodyTitleInsu As EditBlock2ExcelPointVo

        Private EventVo As TShisakuEventVo
#End Region

        ''' <summary>
        ''' コンストラクタ
        ''' </summary>
        ''' <param name="shisakuEventCode">試作イベントコード</param>
        ''' <param name="shisakuBukaCode">試作部課コード</param>
        ''' <param name="shisakuBlockNo">試作ブロックNo</param>
        ''' <param name="newShisakuBlockNoKaiteiNo">試作ブロックNo改訂No(新)</param>
        ''' <param name="oldShisakuBlockNoKaiteiNo">試作ブロックNo改訂No(旧)</param>
        ''' <remarks></remarks>
        Public Sub New(ByVal shisakuEventCode As String, _
                        ByVal shisakuBukaCode As String, _
                        ByVal shisakuBlockNo As String, _
                        ByVal newshisakuBlockNoKaiteiNo As String, _
                        ByVal oldshisakuBlockNoKaiteiNo As String)

            Me.shisakuEventCode = shisakuEventCode
            Me.shisakuBukaCode = shisakuBukaCode
            Me.shisakuBlockNo = shisakuBlockNo
            Me.newShisakuBlockNoKaiteiNo = newshisakuBlockNoKaiteiNo
            Me.oldShisakuBlockNoKaiteiNo = oldshisakuBlockNoKaiteiNo

            Dim eventDao As TShisakuEventDao = New TShisakuEventDaoImpl
            EventVo = eventDao.FindByPk(shisakuEventCode)

        End Sub
        ''↓↓2014/07/25 Ⅰ.3.設計編集 ベース改修専用化_v) (TES)張 ADD BEGIN
        Private ReadOnly subject As BuhinEditKoseiSubject
        ''↑↑2014/07/25 Ⅰ.3.設計編集 ベース改修専用化_v) (TES)張 ADD END

        ''' <summary>
        ''' AL比較エクセル出力
        ''' </summary>
        ''' <param name="xls"></param>
        ''' <param name="tansousya">担当者名</param>
        ''' <param name="telNo">電話番号</param>
        ''' <param name="shisakuEventName">試作イベント名称</param>
        ''' <param name="shisakuKaihatsuFugo">試作開発符号</param>
        ''' <remarks></remarks>
        Public Sub Execute(ByVal xls As ShisakuExcel, _
                            ByVal tansousya As String, _
                            ByVal telNo As String, _
                            ByVal shisakuEventName As String, _
                            ByVal shisakuKaihatsuFugo As String, _
                            ByVal isBase As Boolean)

            xls.SetActiveSheet(1)

            'HeadPart開始設定
            Dim headPartPoint As New EditBlock2ExcelPointVo()

            headPartPoint.RowStart = 1
            headPartPoint.RowEnd = 1
            headPartPoint.ColStart = 1
            headPartPoint.ColEnd = 1

            ''Draw HeadPart1
            headPartPoint = setSheetHeaderPart(xls, tansousya, telNo, shisakuKaihatsuFugo, shisakuEventName, headPartPoint)
            headPartPoint.RowStart = headPartPoint.RowStart + 2
            headPartPoint.RowEnd = headPartPoint.RowEnd + 2

            Dim headPart2Point As New EditBlock2ExcelPointVo
            pointBodyTitleGousya = New EditBlock2ExcelPointVo
            pointBodyTitleMemo = New EditBlock2ExcelPointVo
            pointBodyTitleInsu = New EditBlock2ExcelPointVo

            ''Draw HeadPart2
            headPart2Point = headPartPoint
            pointBodyTitleSoubi = New EditBlock2ExcelPointVo
            colTitleGousyaCount = 0
            colTitleSoubiCount = 0
            colTitleInsuCount = 0
            titleVos = New List(Of EditBlock2ExcelTitle1Vo)
            title2Vos = New List(Of EditBlock2ExcelTitle2Vo)
            title3Vos = New List(Of TShisakuSekkeiBlockMemoVo)
            title4VosA = New List(Of TShisakuSekkeiBlockInstlVo)
            title4VosB = New List(Of TShisakuSekkeiBlockInstlVo)

            Dim getDate As New EditBlock2ExcelDaoImpl()

            'タイトル部の作成'
            setSheetTitle(xls, getDate, headPart2Point, isBase)

            'ボディ部の作成'
            setSheet1BodyPart(xls, getDate, headPart2Point, isBase)

        End Sub

        ''' <summary>
        ''' ヘッダー情報の作成
        ''' </summary>
        ''' <param name="xls">EXCEL</param>
        ''' <param name="tanTouSya">担当者</param>
        ''' <param name="tel">電話番号</param>
        ''' <param name="shisakuKaihatsuFugo">開発符号</param>
        ''' <param name="shisakuEventName">イベント名称</param>
        ''' <param name="point"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Private Function setSheetHeaderPart(ByVal xls As ShisakuExcel, _
                                           ByVal tanTouSya As String, _
                                           ByVal tel As String, _
                                           ByVal shisakuKaihatsuFugo As String, _
                                           ByVal shisakuEventName As String, _
                                           ByVal point As EditBlock2ExcelPointVo) As EditBlock2ExcelPointVo

            xls.SetValue(point.ColStart, point.RowStart, shisakuKaihatsuFugo + " " + shisakuEventName)
            '担当設計課を取得する'
            Dim tantoImpl As New ShisakuBuhinEditBlock.Dao.KaRyakuNameDaoImpl
            Dim TantoName As New Rhac1560Vo

            '担当設計が無い場合、コードを課略名称へ設定する。
            If Not StringUtil.IsEmpty(tantoImpl.GetKa_Ryaku_Name(shisakuBukaCode)) Then
                TantoName = tantoImpl.GetKa_Ryaku_Name(shisakuBukaCode)
            Else
                TantoName.KaRyakuName = shisakuBukaCode
            End If

            '担当者名を取得する'
            Dim ExcelImpl As EditBlock2ExcelDao = New EditBlock2ExcelDaoImpl
            Dim UserName As String

            UserName = ExcelImpl.FindByShainName(tanTouSya)

            xls.SetValue(point.ColStart + 6, point.RowStart, "担当設計：" + TantoName.KaRyakuName + " 担当者：" + tanTouSya + " Tel: " + tel)
            xls.SetValue(point.ColStart, point.RowStart + 1, "ブロックNo：" + shisakuBlockNo)

            Return point
        End Function

        ''' <summary>
        ''' タイトル部作成
        ''' </summary>
        ''' <param name="xls"></param>
        ''' <param name="getDate"></param>
        ''' <param name="point"></param>
        ''' <param name="isBase"></param>
        ''' <remarks></remarks>
        Private Sub setSheetTitle(ByVal xls As ShisakuExcel, _
                                  ByVal getDate As EditBlock2ExcelDaoImpl, _
                                  ByVal point As EditBlock2ExcelPointVo, _
                                  ByVal isBase As Boolean)

            Dim blockNoList As New List(Of String)
            blockNoList.Add(shisakuBlockNo)

            ''第二分部TitlePart2(1.イベントベース車、2.装備仕様内容、3.メモ欄、4.INSTL品番)


            ''処理開始
            ''Get装備仕様内容タイトルデータ---------------------------------------------------
            '2012/09/01 kabasawa ベースの装備に関してベースの情報は必要ない'
            titleVos = getDate.FindWithTitle1NameBy(shisakuEventCode, shisakuBukaCode, blockNoList, newShisakuBlockNoKaiteiNo, False)

            ''Get Title1の列数
            colTitleGousyaCount = titleVos.Count ''Title1列数
            '号車開始Point'
            pointBodyTitleGousya.RowStart = point.RowStart
            pointBodyTitleGousya.RowEnd = point.RowEnd + 5
            pointBodyTitleGousya.ColStart = point.ColStart
            pointBodyTitleGousya.ColEnd = point.ColEnd

            ''第一列Title号車設定
            xls.SetValue(pointBodyTitleGousya.ColStart, pointBodyTitleGousya.RowEnd, pointBodyTitleGousya.ColEnd, pointBodyTitleGousya.RowEnd, "号車")
            ''位置はCenter
            xls.SetAlignment(pointBodyTitleGousya.ColStart, pointBodyTitleGousya.RowEnd, pointBodyTitleGousya.ColEnd, pointBodyTitleGousya.RowEnd, _
                             XlHAlign.xlHAlignCenter, XlVAlign.xlVAlignCenter, True, True)
            xls.SetLine(pointBodyTitleGousya.ColStart, pointBodyTitleGousya.RowEnd, pointBodyTitleGousya.ColEnd, pointBodyTitleGousya.RowEnd, _
                            XlBordersIndex.xlEdgeBottom, XlLineStyle.xlContinuous, XlBorderWeight.xlThin)

            If titleVos.Count > 0 Then

                ''試作設計ブロックInstal情報Title
                '号車列の列番号(開始位置)'
                pointBodyTitleGousya.ColStart = pointBodyTitleGousya.ColStart + 1
                pointBodyTitleGousya.ColEnd = pointBodyTitleGousya.ColEnd + 1
                Dim i As Integer

                'アプライドNoとか取得'
                ''Get 項目No.対応表の値(例:項目の1001 = 開発符号)
                For i = 0 To titleVos.Count - 1
                    ''Get項目名Byコード
                    Dim title1VoHelp As New EditBlock2ExcelTitle1VoHelper(titleVos.Item(i))

                    'Set項目名のCellValue
                    xls.SetValue(pointBodyTitleGousya.ColStart + i, pointBodyTitleGousya.RowEnd, pointBodyTitleGousya.ColEnd + i, pointBodyTitleGousya.RowEnd, title1VoHelp.ShisakuSoubiHyoujiJunMei)
                    ''位置がCenterになります｡
                    xls.SetAlignment(pointBodyTitleGousya.ColStart + i, pointBodyTitleGousya.RowEnd, pointBodyTitleGousya.ColEnd + i, pointBodyTitleGousya.RowEnd, _
                                     XlHAlign.xlHAlignCenter, XlVAlign.xlVAlignCenter, True, True)
                    ''線を書きます
                    xls.SetLine(pointBodyTitleGousya.ColStart + i, pointBodyTitleGousya.RowEnd, pointBodyTitleGousya.ColEnd + i, pointBodyTitleGousya.RowEnd, _
                             XlBordersIndex.xlEdgeBottom, XlLineStyle.xlContinuous, XlBorderWeight.xlThin)
                Next

                pointBodyTitleGousya.ColEnd = pointBodyTitleGousya.ColEnd + titleVos.Count

            Else
                ''タイトルない場合は
                pointBodyTitleGousya.RowStart = point.RowStart
                pointBodyTitleGousya.RowEnd = point.RowEnd + 5
                pointBodyTitleGousya.ColStart = point.ColStart
                pointBodyTitleGousya.ColEnd = point.ColEnd
            End If


            ''Get装備仕様内容タイトルデータ-------------------------------------------------------
            '2012/09/01 kabasawa ベースの装備に関してベースの情報は必要ない'
            title2Vos = getDate.FindWithTitle2NameBy(shisakuEventCode, shisakuBukaCode, blockNoList, newShisakuBlockNoKaiteiNo, False)

            ''Get 装備仕様の列数

            'カウントNなのに中身が無い場合があるので'
            If title2Vos.Count > 0 Then
                If StringUtil.IsEmpty(title2Vos(0).ShisakuSoubiHyoujiJun) Then
                    colTitleSoubiCount = 0
                Else
                    colTitleSoubiCount = title2Vos.Count ''Title2列数
                End If
            Else
                colTitleSoubiCount = 0
            End If

            '開始位置を設定'
            pointBodyTitleSoubi.RowStart = pointBodyTitleGousya.RowStart
            pointBodyTitleSoubi.RowEnd = pointBodyTitleGousya.RowEnd
            pointBodyTitleSoubi.ColStart = pointBodyTitleGousya.ColEnd
            pointBodyTitleSoubi.ColEnd = pointBodyTitleGousya.ColEnd

            If colTitleSoubiCount > 0 Then
                If Not title2Vos Is Nothing Then
                    If Not StringUtil.IsEmpty(title2Vos(0).ShisakuSoubiHyoujiJun) Then

                        pointBodyTitleSoubi.ColStart = pointBodyTitleSoubi.ColStart
                        pointBodyTitleSoubi.ColEnd = pointBodyTitleSoubi.ColEnd + colTitleSoubiCount

                        ''Cells合併
                        xls.MergeCells(pointBodyTitleSoubi.ColStart, pointBodyTitleSoubi.RowStart, pointBodyTitleSoubi.ColEnd - 1, pointBodyTitleSoubi.RowStart, True)
                        ''位置はCenter
                        xls.SetAlignment(pointBodyTitleSoubi.ColStart, pointBodyTitleSoubi.RowStart, pointBodyTitleSoubi.ColStart, pointBodyTitleSoubi.RowStart, _
                                         XlHAlign.xlHAlignCenter, XlVAlign.xlVAlignCenter, True, True)
                        ''Set項目名のCellValue
                        xls.SetValue(pointBodyTitleSoubi.ColStart, pointBodyTitleSoubi.RowStart, pointBodyTitleSoubi.ColStart, pointBodyTitleSoubi.RowStart, "装備仕様内容")

                        ''装備仕様内容Title設定()----------------------
                        Dim i As Integer
                        For i = 0 To title2Vos.Count - 1

                            ''Cells合併
                            xls.MergeCells(pointBodyTitleSoubi.ColStart + i, pointBodyTitleSoubi.RowStart + 1, pointBodyTitleSoubi.ColStart + i, pointBodyTitleSoubi.RowEnd, True)
                            xls.SetOrientation(pointBodyTitleSoubi.ColStart + i, pointBodyTitleSoubi.RowStart + 1, pointBodyTitleSoubi.ColStart + i, pointBodyTitleSoubi.RowStart + 1, _
                                               ShisakuExcel.XlOrientation.xlVertical, False, False)
                            ''位置はCenter
                            xls.SetAlignment(pointBodyTitleSoubi.ColStart + i, pointBodyTitleSoubi.RowStart + 1, pointBodyTitleSoubi.ColStart + i, pointBodyTitleSoubi.RowEnd, _
                                         XlHAlign.xlHAlignCenter, XlVAlign.xlVAlignTop, False, False)
                            ''幅設定
                            xls.SetColWidth(pointBodyTitleSoubi.ColStart + i, 2.8)
                            xls.SetRowHeight(pointBodyTitleSoubi.RowStart + 2, 200)
                            ''Value設定
                            xls.SetValue(pointBodyTitleSoubi.ColStart + i, pointBodyTitleSoubi.RowStart + 1, pointBodyTitleSoubi.ColStart + i, pointBodyTitleSoubi.RowStart + 1, _
                                         title2Vos.Item(i).ShisakuRetuKoumokuNameDai & "・" & _
                                          title2Vos.Item(i).ShisakuRetuKoumokuNameChu & "・" & _
                                           title2Vos.Item(i).ShisakuRetuKoumokuName)

                        Next

                        '上の線'
                        xls.SetLine(pointBodyTitleSoubi.ColStart, pointBodyTitleSoubi.RowStart + 1, pointBodyTitleSoubi.ColEnd, pointBodyTitleSoubi.RowStart + 1, _
                                 XlBordersIndex.xlEdgeTop, XlLineStyle.xlContinuous, XlBorderWeight.xlThin)
                        '下の線'
                        xls.SetLine(pointBodyTitleSoubi.ColStart, pointBodyTitleSoubi.RowStart + 5, pointBodyTitleSoubi.ColEnd, pointBodyTitleSoubi.RowEnd, _
                                    XlBordersIndex.xlEdgeBottom, XlLineStyle.xlContinuous, XlBorderWeight.xlThin)

                    End If
                End If
            End If


            ''Getメモ欄内容タイトルデータ-------------------------------------------------------
            title3Vos = _
            getDate.FindWithTitle3NameBy(shisakuEventCode, shisakuBukaCode, blockNoList, newShisakuBlockNoKaiteiNo)

            ''Get Titleメモの列数

            For voindex As Integer = 0 To title3Vos.Count - 1
                If colTitleMemoCount < title3Vos(voindex).ShisakuMemoHyoujiJun Then
                    colTitleMemoCount = title3Vos(voindex).ShisakuMemoHyoujiJun
                End If
            Next


            pointBodyTitleMemo.RowStart = pointBodyTitleSoubi.RowStart
            pointBodyTitleMemo.RowEnd = pointBodyTitleSoubi.RowEnd

            pointBodyTitleMemo.ColStart = pointBodyTitleSoubi.ColEnd
            pointBodyTitleMemo.ColEnd = pointBodyTitleSoubi.ColEnd

            If colTitleMemoCount > 0 Then
                'エクセルの数値にあわせる'

                pointBodyTitleMemo.ColStart = pointBodyTitleMemo.ColStart
                pointBodyTitleMemo.ColEnd = pointBodyTitleMemo.ColEnd + colTitleMemoCount

                ''Cells合併
                xls.MergeCells(pointBodyTitleMemo.ColStart, pointBodyTitleMemo.RowStart, pointBodyTitleMemo.ColEnd, pointBodyTitleMemo.RowStart, True)
                ''位置はCenter
                xls.SetAlignment(pointBodyTitleMemo.ColStart, _
                                 pointBodyTitleMemo.RowStart, _
                                 pointBodyTitleMemo.ColStart, pointBodyTitleMemo.RowStart, _
                                 XlHAlign.xlHAlignCenter, XlVAlign.xlVAlignCenter, True, True)
                ''Set項目名のCellValue
                xls.SetValue(pointBodyTitleMemo.ColStart, pointBodyTitleMemo.RowStart, pointBodyTitleMemo.ColStart, pointBodyTitleMemo.RowStart, "メモ欄")

                ''メモ内容Title設定()----------------------
                Dim i As Integer
                For i = 0 To title3Vos.Count - 1

                    If StringUtil.Equals(title3Vos(i).ShisakuGousya, "<TITLE;>") Then
                        ''Cells合併
                        xls.MergeCells(pointBodyTitleMemo.ColStart + title3Vos(i).ShisakuMemoHyoujiJun, pointBodyTitleMemo.RowStart + 1, pointBodyTitleMemo.ColStart + title3Vos(i).ShisakuMemoHyoujiJun, pointBodyTitleMemo.RowEnd, True)
                        xls.SetOrientation(pointBodyTitleMemo.ColStart + title3Vos(i).ShisakuMemoHyoujiJun, pointBodyTitleMemo.RowStart + 1, pointBodyTitleMemo.ColStart + title3Vos(i).ShisakuMemoHyoujiJun, pointBodyTitleMemo.RowStart + 1, _
                                           ShisakuExcel.XlOrientation.xlVertical, False, False)
                        ''位置はCenter
                        xls.SetAlignment(pointBodyTitleMemo.ColStart + title3Vos(i).ShisakuMemoHyoujiJun, pointBodyTitleMemo.RowStart + 1, pointBodyTitleMemo.ColStart + title3Vos(i).ShisakuMemoHyoujiJun, pointBodyTitleMemo.RowEnd, _
                                     XlHAlign.xlHAlignCenter, XlVAlign.xlVAlignTop, False, False)
                        ''幅設定
                        xls.SetColWidth(pointBodyTitleMemo.ColStart + title3Vos(i).ShisakuMemoHyoujiJun, 2.8)
                        xls.SetRowHeight(pointBodyTitleMemo.RowStart + 2, 200)
                        ''Value設定
                        xls.SetValue(pointBodyTitleMemo.ColStart + title3Vos(i).ShisakuMemoHyoujiJun, pointBodyTitleMemo.RowStart + 1, _
                                     pointBodyTitleMemo.ColStart + title3Vos(i).ShisakuMemoHyoujiJun, pointBodyTitleMemo.RowStart + 1, _
                                     title3Vos(i).ShisakuMemo)
                    End If
                Next

                '上の線'
                xls.SetLine(pointBodyTitleMemo.ColStart, pointBodyTitleMemo.RowStart + 1, pointBodyTitleMemo.ColEnd, pointBodyTitleMemo.RowStart + 1, _
                         XlBordersIndex.xlEdgeTop, XlLineStyle.xlContinuous, XlBorderWeight.xlThin)
                '下の線'
                xls.SetLine(pointBodyTitleMemo.ColStart, pointBodyTitleMemo.RowStart + 5, pointBodyTitleMemo.ColEnd, pointBodyTitleMemo.RowEnd, _
                            XlBordersIndex.xlEdgeBottom, XlLineStyle.xlContinuous, XlBorderWeight.xlThin)
            End If


            'GetINSTL品番タイトルデータ-------------------------------------------------------
            Dim title4Vos As List(Of EditBlock2ExcelTitle4Vo) = _
                      getDate.FindWithTitle4NameBy(shisakuEventCode, shisakuBukaCode, blockNoList, newShisakuBlockNoKaiteiNo, False)

            '最新のINSTL品番列を取得'
            ''20141204 TSUNODA ADD START
            ''ベースｲﾝｽﾄﾚｰｼｮﾝ品番に対する、ノート欄取得が行えていなかった為これを追加
            Dim title4VosOld As List(Of EditBlock2ExcelTitle4Vo) = _
                        getDate.FindWithTitle4NameBy(shisakuEventCode, shisakuBukaCode, blockNoList, newShisakuBlockNoKaiteiNo, isBase)

            title4VosA = getDate.FindByInstlHinbanCondition(shisakuEventCode, shisakuBukaCode, shisakuBlockNo, newShisakuBlockNoKaiteiNo)
            '比較先のINSTL品番列を取得'
            If isBase Then
                title4VosB = getDate.FindByInstlHinbanCondition(shisakuEventCode, shisakuBukaCode, shisakuBlockNo, oldShisakuBlockNoKaiteiNo, isBase)
            Else
                title4VosB = getDate.FindByInstlHinbanCondition(shisakuEventCode, shisakuBukaCode, shisakuBlockNo, oldShisakuBlockNoKaiteiNo, False)
            End If



            'Get Title4の列数
            colTitleInsuCount = title4VosA.Count ''Title2列数


            pointBodyTitleInsu.RowStart = pointBodyTitleMemo.RowStart
            pointBodyTitleInsu.RowEnd = pointBodyTitleMemo.RowEnd

            pointBodyTitleInsu.ColStart = pointBodyTitleMemo.ColEnd
            pointBodyTitleInsu.ColEnd = pointBodyTitleMemo.ColEnd


            Dim allGousyaList As List(Of TShisakuEventBaseVo) = getDate.FindAllGouSya(shisakuEventCode)

            'INSTL列作成'
            If colTitleInsuCount > 0 Then
                'メモがずれる'
                If pointBodyTitleMemo.ColEnd = pointBodyTitleSoubi.ColEnd And colTitleSoubiCount = 0 Then

                    pointBodyTitleInsu.ColStart = pointBodyTitleInsu.ColStart
                    pointBodyTitleInsu.ColEnd = pointBodyTitleInsu.ColEnd + colTitleInsuCount - 1
                Else
                    pointBodyTitleInsu.ColStart = pointBodyTitleInsu.ColStart + 1
                    pointBodyTitleInsu.ColEnd = pointBodyTitleInsu.ColEnd + colTitleInsuCount
                End If

                ''Cells合併
                xls.MergeCells(pointBodyTitleInsu.ColStart, pointBodyTitleInsu.RowStart, pointBodyTitleInsu.ColEnd, pointBodyTitleInsu.RowStart, True)
                ''位置はCenter
                xls.SetAlignment(pointBodyTitleInsu.ColStart, pointBodyTitleInsu.RowStart, pointBodyTitleInsu.ColStart, pointBodyTitleInsu.RowStart, _
                                 XlHAlign.xlHAlignCenter, XlVAlign.xlVAlignCenter, True, True)
                ''Set項目名のCellValue
                xls.SetValue(pointBodyTitleInsu.ColStart, pointBodyTitleInsu.RowStart, pointBodyTitleInsu.ColStart, pointBodyTitleInsu.RowStart, "INSTL品番")
                'INSTL品番は横書き'
                xls.SetOrientation(pointBodyTitleInsu.ColStart, pointBodyTitleInsu.RowStart, pointBodyTitleInsu.ColStart, pointBodyTitleInsu.RowStart, ShisakuExcel.XlOrientation.xlHorizontal)
                ''INSTL品番内容Title設定----------------------
                Dim i As Integer

                '最新を追加'
                For i = 0 To title4VosA.Count - 1
                    ''Cells合併
                    xls.MergeCells(pointBodyTitleInsu.ColStart + i, pointBodyTitleInsu.RowStart + 5, pointBodyTitleInsu.ColStart + i, pointBodyTitleInsu.RowEnd, True)
                    xls.SetOrientation(pointBodyTitleInsu.ColStart + i, pointBodyTitleInsu.RowStart + 2, pointBodyTitleInsu.ColStart + i, pointBodyTitleInsu.RowStart + 2, _
                                       ShisakuExcel.XlOrientation.xlVertical, False, False)
                    xls.SetOrientation(pointBodyTitleInsu.ColStart + i, pointBodyTitleInsu.RowStart + 3, pointBodyTitleInsu.ColStart + i, pointBodyTitleInsu.RowStart + 3, _
                                       ShisakuExcel.XlOrientation.xlVertical, False, False)
                    xls.SetOrientation(pointBodyTitleInsu.ColStart + i, pointBodyTitleInsu.RowStart + 4, pointBodyTitleInsu.ColStart + i, pointBodyTitleInsu.RowStart + 4, _
                                        ShisakuExcel.XlOrientation.xlVertical, False, False)
                    ''位置はCenter
                    xls.SetAlignment(pointBodyTitleInsu.ColStart + i, pointBodyTitleInsu.RowStart + 1, pointBodyTitleInsu.ColStart + i, pointBodyTitleInsu.RowEnd + 3, _
                                 XlHAlign.xlHAlignCenter, XlVAlign.xlVAlignTop, False, False)
                    ''幅設定
                    xls.SetColWidth(pointBodyTitleInsu.ColStart + i, 2.8)
                    xls.SetRowHeight(pointBodyTitleInsu.RowStart + 4, 150)
                    xls.SetRowHeight(pointBodyTitleInsu.RowStart + 5, 50)
                    ''Value設定
                    xls.SetValue(pointBodyTitleInsu.ColStart + i, pointBodyTitleInsu.RowStart + 1, pointBodyTitleInsu.ColStart + i, pointBodyTitleInsu.RowStart + 1, _
                                 IntToString(i))

                    ''ブロックNo Value設定
                    If Not String.IsNullOrEmpty(shisakuBlockNo) Then
                        ''仕様書情報 Value設定
                        xls.SetValue(pointBodyTitleInsu.ColStart + i, pointBodyTitleInsu.RowStart + 3, pointBodyTitleInsu.ColStart + i, pointBodyTitleInsu.RowStart + 3, _
                                     Trim(title4Vos.Item(i).BuhinNote))
                    Else
                        xls.SetValue(pointBodyTitleInsu.ColStart + i, pointBodyTitleInsu.RowStart + 2, pointBodyTitleInsu.ColStart + i, pointBodyTitleInsu.RowStart + 2, _
                                    title4Vos.Item(i).ShisakuBlockNo)
                        xls.SetValue(pointBodyTitleInsu.ColStart + i, pointBodyTitleInsu.RowStart + 3, pointBodyTitleInsu.ColStart + i, pointBodyTitleInsu.RowStart + 3, _
                                    Trim(title4Vos.Item(i).BuhinNote))
                    End If

                    ''INSTL品番Value設定
                    xls.SetValue(pointBodyTitleInsu.ColStart + i, pointBodyTitleInsu.RowStart + 4, pointBodyTitleInsu.ColStart + i, pointBodyTitleInsu.RowStart + 4, _
                                 title4VosA.Item(i).InstlHinban)

                    ''INSTL品番区分Value設定
                    xls.SetValue(pointBodyTitleInsu.ColStart + i, pointBodyTitleInsu.RowStart + 5, pointBodyTitleInsu.ColStart + i, pointBodyTitleInsu.RowStart + 5, _
                                 title4VosA.Item(i).InstlHinbanKbn)

                    '変更チェック'
                    If Not ChkInstl(title4VosA.Item(i).InstlHinban, _
                                title4VosA.Item(i).InstlHinbanKbn, _
                                title4VosA.Item(i).InstlDataKbn, _
                                title4VosA.Item(i).BaseInstlFlg, _
                                title4VosB) Then
                        '最新に存在、過去に存在しないので追加'

                        '色の順番BGR'
                        '追加は薄黄色'
                        xls.SetBackColor(pointBodyTitleInsu.ColStart + i, pointBodyTitleInsu.RowStart + 4, pointBodyTitleInsu.ColStart + i, pointBodyTitleInsu.RowStart + 5, &H9FFFFF)
                    End If
                Next


                '旧INSTLを追加'
                For k As Integer = 0 To title4VosB.Count - 1
                    '変更チェック'
                    If Not ChkInstl(title4VosB.Item(k).InstlHinban, _
                                    title4VosB.Item(k).InstlHinbanKbn, _
                                    title4VosB.Item(k).InstlDataKbn, _
                                    title4VosB.Item(k).BaseInstlFlg, _
                                    title4VosA) Then


                        '過去に存在、最新に存在しないので削除'
                        xls.MergeCells(pointBodyTitleInsu.ColStart + i, pointBodyTitleInsu.RowStart + 5, pointBodyTitleInsu.ColStart + i, pointBodyTitleInsu.RowEnd, True)

                        xls.SetOrientation(pointBodyTitleInsu.ColStart + i, pointBodyTitleInsu.RowStart + 2, pointBodyTitleInsu.ColStart + i, pointBodyTitleInsu.RowStart + 2, _
                                           ShisakuExcel.XlOrientation.xlVertical, False, False)
                        xls.SetOrientation(pointBodyTitleInsu.ColStart + i, pointBodyTitleInsu.RowStart + 3, pointBodyTitleInsu.ColStart + i, pointBodyTitleInsu.RowStart + 3, _
                                           ShisakuExcel.XlOrientation.xlVertical, False, False)
                        xls.SetOrientation(pointBodyTitleInsu.ColStart + i, pointBodyTitleInsu.RowStart + 4, pointBodyTitleInsu.ColStart + i, pointBodyTitleInsu.RowStart + 4, _
                                            ShisakuExcel.XlOrientation.xlVertical, False, False)

                        ''位置はCenter
                        xls.SetAlignment(pointBodyTitleInsu.ColStart + i, pointBodyTitleInsu.RowStart + 1, pointBodyTitleInsu.ColStart + i, pointBodyTitleInsu.RowEnd + 3, _
                                     XlHAlign.xlHAlignCenter, XlVAlign.xlVAlignTop, False, False)
                        ''幅設定
                        xls.SetColWidth(pointBodyTitleInsu.ColStart + i, 2.8)
                        xls.SetRowHeight(pointBodyTitleInsu.RowStart + 4, 150)
                        xls.SetRowHeight(pointBodyTitleInsu.RowStart + 5, 50)

                        ''Value設定
                        xls.SetValue(pointBodyTitleInsu.ColStart + i, pointBodyTitleInsu.RowStart + 1, pointBodyTitleInsu.ColStart + i, pointBodyTitleInsu.RowStart + 1, _
                                     IntToString(i))

                        ''ブロックNo Value設定
                        ''20141204 TSUNODA EDIT title4Vos ⇒ title4VosOld に変更と、Item(i) ⇒ Item(k)としてカウンター変更
                        If k < title4VosOld.Count Then
                            If Not String.IsNullOrEmpty(shisakuBlockNo) Then
                                ''仕様書情報 Value設定
                                xls.SetValue(pointBodyTitleInsu.ColStart + i, pointBodyTitleInsu.RowStart + 3, pointBodyTitleInsu.ColStart + i, pointBodyTitleInsu.RowStart + 3, _
                                             Trim(title4VosOld.Item(k).BuhinNote))
                            Else
                                xls.SetValue(pointBodyTitleInsu.ColStart + i, pointBodyTitleInsu.RowStart + 2, pointBodyTitleInsu.ColStart + i, pointBodyTitleInsu.RowStart + 2, _
                                            title4VosOld.Item(k).ShisakuBlockNo)
                                xls.SetValue(pointBodyTitleInsu.ColStart + i, pointBodyTitleInsu.RowStart + 3, pointBodyTitleInsu.ColStart + i, pointBodyTitleInsu.RowStart + 3, _
                                            Trim(title4VosOld.Item(k).BuhinNote))
                            End If
                        End If



                        ''INSTL品番Value設定
                        xls.SetValue(pointBodyTitleInsu.ColStart + i, pointBodyTitleInsu.RowStart + 4, pointBodyTitleInsu.ColStart + i, pointBodyTitleInsu.RowStart + 4, _
                                     title4VosB.Item(k).InstlHinban)

                        ''INSTL品番区分Value設定
                        xls.SetValue(pointBodyTitleInsu.ColStart + i, pointBodyTitleInsu.RowStart + 5, pointBodyTitleInsu.ColStart + i, pointBodyTitleInsu.RowStart + 5, _
                                     title4VosB.Item(k).InstlHinbanKbn)
                        '色の順番BGR'
                        '追加は灰色'

                        ''20141204 TSUNOD EDIT ｲﾝｽﾄﾚｰｼｮﾝ品番の変更に伴い、ノート欄の変更も同様に行われる為、ノート欄まで色付けを行う対応として変更
                        xls.SetBackColor(pointBodyTitleInsu.ColStart + i, pointBodyTitleInsu.RowStart + 3, pointBodyTitleInsu.ColStart + i, pointBodyTitleInsu.RowStart + 5, &HA0A0A0)
                        'xls.SetBackColor(pointBodyTitleInsu.ColStart + i, pointBodyTitleInsu.RowStart + 3, pointBodyTitleInsu.ColStart + i, pointBodyTitleInsu.RowStart + 5, RGB(176, 215, 237))

                        ''↑↑2014/09/18 酒井 ADD END
                        i = i + 1
                        colTitleInsuCount = colTitleInsuCount + 1
                    End If
                Next

                '変更内容のタイトル設定'
                xls.SetValue(pointBodyTitleInsu.ColStart + colTitleInsuCount, pointBodyTitleGousya.RowStart + 5, pointBodyTitleInsu.ColStart + colTitleInsuCount, pointBodyTitleGousya.RowStart + 5, "変更内容")
                xls.SetBackColor(pointBodyTitleInsu.ColStart + colTitleInsuCount, pointBodyTitleGousya.RowStart + 5, pointBodyTitleInsu.ColStart + colTitleInsuCount, pointBodyTitleGousya.RowStart + 5, &HB3DEF5)

                '上の線'
                'xls.SetLine(pointBodyTitleInsu.ColStart, pointBodyTitleInsu.RowStart + 1, pointBodyTitleInsu.ColEnd, pointBodyTitleInsu.RowStart + 1, _
                '         XlBordersIndex.xlEdgeTop, XlLineStyle.xlContinuous, XlBorderWeight.xlThin)
                xls.SetLine(pointBodyTitleInsu.ColStart, pointBodyTitleInsu.RowStart + 1, pointBodyTitleInsu.ColStart + colTitleInsuCount, pointBodyTitleInsu.RowStart + 1, _
                            XlBordersIndex.xlEdgeTop, XlLineStyle.xlContinuous, XlBorderWeight.xlThin)
                '下の線'
                xls.SetLine(pointBodyTitleInsu.ColStart, pointBodyTitleInsu.RowStart + 5, pointBodyTitleInsu.ColStart + colTitleInsuCount, pointBodyTitleInsu.RowEnd, _
                            XlBordersIndex.xlEdgeBottom, XlLineStyle.xlContinuous, XlBorderWeight.xlThin)
            Else
                ''Cells合併
                xls.MergeCells(pointBodyTitleInsu.ColStart + 1, pointBodyTitleInsu.RowStart, pointBodyTitleInsu.ColEnd + 1, pointBodyTitleInsu.RowStart, True)
                ''位置はCenter
                xls.SetAlignment(pointBodyTitleInsu.ColStart + 1, pointBodyTitleInsu.RowStart, pointBodyTitleInsu.ColStart + 1, pointBodyTitleInsu.RowStart, _
                                 XlHAlign.xlHAlignCenter, XlVAlign.xlVAlignCenter, True, True)
                ''Set項目名のCellValue
                xls.SetValue(pointBodyTitleInsu.ColStart + 1, pointBodyTitleInsu.RowStart, pointBodyTitleInsu.ColStart + 1, pointBodyTitleInsu.RowStart, "INSTL品番")
                'INSTL品番は横書き'
                xls.SetOrientation(pointBodyTitleInsu.ColStart + 1, pointBodyTitleInsu.RowStart, pointBodyTitleInsu.ColStart + 1, pointBodyTitleInsu.RowStart, ShisakuExcel.XlOrientation.xlHorizontal)
                '上の線'
                xls.SetLine(pointBodyTitleInsu.ColStart + 1, pointBodyTitleInsu.RowStart + 1, pointBodyTitleInsu.ColEnd + 1, pointBodyTitleInsu.RowStart + 1, _
                         XlBordersIndex.xlEdgeTop, XlLineStyle.xlContinuous, XlBorderWeight.xlThin)
                '下の線'
                xls.SetLine(pointBodyTitleInsu.ColStart + 1, pointBodyTitleInsu.RowStart + 5, pointBodyTitleInsu.ColEnd + 1, pointBodyTitleInsu.RowEnd, _
                            XlBordersIndex.xlEdgeBottom, XlLineStyle.xlContinuous, XlBorderWeight.xlThin)
            End If
        End Sub

        ''' <summary>
        ''' INSTL品番の変更あるなしをチェックする
        ''' </summary>
        ''' <param name="InstlHinbanA">比較元</param>
        ''' <param name="InstlHinbanKbnA">比較元</param>
        ''' <param name="InstlList">比較先</param>
        ''' <returns>変更なしならTrue </returns>
        ''' <remarks></remarks>
        Private Function ChkInstl(ByVal InstlHinbanA As String, _
                                  ByVal InstlHinbanKbnA As String, _
                                  ByVal InstlDataKbn As String, _
                                  ByVal BaseInstlFlg As String, _
                                  ByVal InstlList As List(Of TShisakuSekkeiBlockInstlVo)) As Boolean
            'INSTL品番の表示順は当てにならないので'
            For Each vo As TShisakuSekkeiBlockInstlVo In InstlList
                'INSTL品番が同一か？'
                If EzUtil.IsEqualIfNull(InstlHinbanA, vo.InstlHinban) Then
                    'INSTL品番区分が同一か？'
                    If EzUtil.IsEqualIfNull(InstlHinbanKbnA, vo.InstlHinbanKbn) Then
                        '両方同じものが存在するなら変更なし'
                        If EzUtil.IsEqualIfNull(InstlDataKbn, vo.InstlDataKbn) Then
                            If Me.EventVo.BlockAlertKind = "2" And Me.EventVo.KounyuShijiFlg = "0" Then
                                If EzUtil.IsEqualIfNull(BaseInstlFlg, vo.BaseInstlFlg) Then
                                    Return True
                                End If
                            Else
                                Return True
                            End If
                        End If
                    End If
                End If
            Next
            '両方同じものが存在しない場合は変更'
            Return False
        End Function

        ''' <summary>
        ''' INSTL品番区分に色を塗る
        ''' </summary>
        ''' <param name="InstlHinbanA">比較元</param>
        ''' <param name="InstlHinbanKbnA">比較元</param>
        ''' <param name="InstlHinbanHyoujiJunA">比較元</param>
        ''' <param name="InstlList">比較先</param>
        ''' <remarks></remarks>
        Private Function setInstlKbnColor(ByVal InstlHinbanA As String, _
                                          ByVal InstlHinbanKbnA As String, _
                                          ByVal InstlHinbanHyoujiJunA As Integer, _
                                          ByVal InstlList As List(Of TShisakuSekkeiBlockInstlVo)) As Boolean

            Dim result As Integer = 0

            'INSTL品番の表示順は当てにならないので'
            For Each vo As TShisakuSekkeiBlockInstlVo In InstlList
                'INSTL品番が同一か？'
                If StringUtil.Equals(InstlHinbanA, vo.InstlHinban) Then
                    'INSTL品番区分が同一か？'
                    If StringUtil.Equals(InstlHinbanKbnA, vo.InstlHinbanKbn) Then
                        '両方同じものが存在するなら変更なし'
                        Return False
                    Else
                        'INSTL品番が同じで区分が違う'
                        result = 1
                    End If
                Else
                    'INSTLが違う'
                    '区分が同じか？'
                    If StringUtil.Equals(InstlHinbanKbnA, vo.InstlHinbanKbn) Then
                        '区分が同じ場合'
                        If StringUtil.IsEmpty(InstlHinbanKbnA) AndAlso StringUtil.IsEmpty(vo.InstlHinbanKbn) Then
                            '両方空なら色は塗らない'
                        End If
                    Else
                        'INSTLも区分も違うので色を塗る'
                        result = 1
                    End If
                End If
            Next
            If result = 1 Then
                Return True
            End If
            Return False
        End Function



        ''' <summary>
        ''' EXCEL出力
        ''' </summary>
        ''' <param name="xls"></param>
        ''' <param name="point">開始位置</param>
        ''' <param name="isBase"></param>
        ''' <remarks></remarks>
        Private Sub setSheet1BodyPart(ByVal xls As ShisakuExcel, _
                                      ByVal getDate As EditBlock2ExcelDaoImpl, _
                                      ByVal point As EditBlock2ExcelPointVo, _
                                      ByVal isBase As Boolean)

            ''Body作成
            '号車名取得'
            Dim allGousyaList As List(Of TShisakuEventBaseVo) = getDate.FindAllGouSya(shisakuEventCode)

            Dim strGousya As String

            '号車の開始位置'
            Dim pointGousya As New EditBlock2ExcelPointVo()
            pointGousya.RowStart = pointBodyTitleGousya.RowEnd + 1
            pointGousya.RowEnd = pointBodyTitleGousya.RowEnd + 1
            pointGousya.ColStart = 1
            pointGousya.ColEnd = 1

            '号車の中身の開始'
            Dim pointBodyGousya As New EditBlock2ExcelPointVo()
            pointBodyGousya.RowStart = pointBodyTitleGousya.RowEnd + 1
            pointBodyGousya.RowEnd = pointBodyTitleGousya.RowEnd + 1
            pointBodyGousya.ColStart = 2
            pointBodyGousya.ColEnd = 2

            '装備仕様の開始位置'
            Dim pointBodySoubi As New EditBlock2ExcelPointVo()
            pointBodySoubi.RowStart = pointBodyTitleGousya.RowEnd + 1
            pointBodySoubi.RowEnd = pointBodyTitleGousya.RowEnd + 1
            pointBodySoubi.ColStart = 2 + colTitleGousyaCount
            pointBodySoubi.ColEnd = 1 + colTitleGousyaCount + colTitleSoubiCount

            'メモ欄の開始位置'
            Dim pointBodyMemo As New EditBlock2ExcelPointVo()
            pointBodyMemo.RowStart = pointBodyTitleGousya.RowEnd + 1
            pointBodyMemo.RowEnd = pointBodyTitleGousya.RowEnd + 1
            pointBodyMemo.ColStart = 2 + colTitleGousyaCount + colTitleSoubiCount
            pointBodyMemo.ColEnd = 1 + colTitleGousyaCount + colTitleSoubiCount + colTitleMemoCount

            Dim pointBodyInsu As New EditBlock2ExcelPointVo()

            pointBodyInsu.RowStart = pointBodyTitleGousya.RowEnd + 1
            pointBodyInsu.RowEnd = pointBodyTitleGousya.RowEnd + 1

            'ずれまくるので一旦無理やり'
            If colTitleSoubiCount = 0 Then

                If colTitleMemoCount = 0 Then
                    pointBodyInsu.ColStart = 2 + colTitleGousyaCount + colTitleSoubiCount + colTitleMemoCount
                    pointBodyInsu.ColEnd = 2 + colTitleGousyaCount + colTitleSoubiCount + colTitleMemoCount

                Else
                    pointBodyInsu.ColStart = 3 + colTitleGousyaCount + colTitleSoubiCount + colTitleMemoCount
                    pointBodyInsu.ColEnd = 2 + colTitleGousyaCount + colTitleSoubiCount + colTitleMemoCount + colTitleInsuCount
                End If

            Else
                pointBodyInsu.ColStart = 3 + colTitleGousyaCount + colTitleSoubiCount + colTitleMemoCount
                pointBodyInsu.ColEnd = 2 + colTitleGousyaCount + colTitleSoubiCount + colTitleMemoCount + colTitleInsuCount
            End If


            Dim title4VosC As New List(Of TShisakuSekkeiBlockInstlVo)

            For Each vo1 As TShisakuSekkeiBlockInstlVo In title4VosB
                If Not ChkInstl(vo1.InstlHinban, _
                        vo1.InstlHinbanKbn, _
                        vo1.InstlDataKbn, _
                        vo1.BaseInstlFlg, _
                        title4VosA) Then
                    title4VosC.Add(vo1)
                End If
            Next



            Dim endrow As Integer = xls.EndRow
            If Not allGousyaList.Count = 0 Then

                Dim MaxIndex As Integer = 0
                For Each g As TShisakuEventBaseVo In allGousyaList
                    If MaxIndex < g.HyojijunNo Then
                        MaxIndex = g.HyojijunNo
                    End If
                Next

                '-----------------------------------------------------------------------------------------------------------
                '2013/02/12修正
                '０なら１をセット。
                '   号車が１件の場合、０だと「dataMatrixForInsu(gVo.HyojijunNo + gousyaMergeCount, p) 」でインデックスエラー
                If MaxIndex = 0 Then
                    MaxIndex = 1
                End If
                '-----------------------------------------------------------------------------------------------------------

                MaxIndex = MaxIndex * 3

                'メモ出力用のマトリクス
                Dim dataMatrixForMemo(MaxIndex, colTitleMemoCount) As String
                For x As Integer = 0 To UBound(dataMatrixForMemo)
                    For y As Integer = 0 To UBound(dataMatrixForMemo, 2)
                        dataMatrixForMemo(x, y) = ""
                    Next
                Next

                '員数出力用のマトリクス
                Dim dataMatrixForInsu(MaxIndex, colTitleInsuCount) As String
                For s As Integer = 0 To UBound(dataMatrixForInsu)
                    For t As Integer = 0 To UBound(dataMatrixForInsu, 2)
                        dataMatrixForInsu(s, t) = ""
                    Next
                Next

                ''BodyDataLoop
                Dim gousyaMergeCount As Integer = 1
                Dim gMcend As Integer = 0
                For Each gVo As TShisakuEventBaseVo In allGousyaList


                    'Get号車値(step1)
                    strGousya = gVo.ShisakuGousya
                    ''号車行のマージ
                    xls.MergeCells(pointGousya.ColStart, _
                                   pointGousya.RowStart + gVo.HyojijunNo + gMcend, _
                                   pointGousya.ColEnd, _
                                   pointGousya.RowEnd + gVo.HyojijunNo + gousyaMergeCount, True)

                    ''号車値の設定
                    xls.SetValue(pointGousya.ColStart, _
                                 pointGousya.RowStart + gVo.HyojijunNo + gMcend, _
                                 pointGousya.ColEnd, _
                                 pointGousya.RowEnd + gVo.HyojijunNo + gousyaMergeCount, strGousya)

                    '位置はCenter
                    xls.SetAlignment(pointGousya.ColStart, _
                                     pointGousya.RowStart + gVo.HyojijunNo + gMcend, _
                                     pointGousya.ColEnd, _
                                     pointGousya.RowEnd + gVo.HyojijunNo + gousyaMergeCount, _
                                     XlHAlign.xlHAlignCenter, XlVAlign.xlVAlignCenter, True, True)

                    '上下に罫線追加'
                    xls.SetLine(pointGousya.ColStart, _
                                pointGousya.RowStart + gVo.HyojijunNo + gMcend + 1, _
                                pointBodyTitleInsu.ColStart + colTitleInsuCount, _
                                pointGousya.RowStart + gVo.HyojijunNo + gMcend + 1, _
                                XlBordersIndex.xlEdgeBottom, _
                                XlLineStyle.xlContinuous, _
                                XlBorderWeight.xlThin)

                    xls.SetLine(pointGousya.ColStart, _
                                pointGousya.RowStart + gVo.HyojijunNo + gMcend, _
                                pointBodyTitleInsu.ColStart + colTitleInsuCount, _
                                pointGousya.RowStart + gVo.HyojijunNo + gMcend, _
                                XlBordersIndex.xlEdgeTop, _
                                XlLineStyle.xlContinuous, _
                                XlBorderWeight.xlThin)

                    endrow = pointGousya.RowStart + gVo.HyojijunNo + gMcend + 1


                    'Get試作イベントベース車情報と試作イベント完成車情報の値(step2)
                    Dim titleGousyaBody As EditBlock2ExcelTitle1BodyVo = _
                        getDate.FindWithTitle1BodyDataBy(shisakuEventCode, shisakuBukaCode, shisakuBlockNo, newShisakuBlockNoKaiteiNo, strGousya)

                    Dim k As Integer

                    For k = 0 To titleVos.Count - 1

                        Dim strTitleGousya As String = ""
                        Dim strTitleGousyaBodyValue As String = ""
                        strTitleGousya = titleVos.Item(k).ShisakuSoubiHyoujiJun
                        strTitleGousyaBodyValue = GetTitle1Value(strTitleGousya, titleGousyaBody)

                        xls.MergeCells(pointBodyGousya.ColStart + k, _
                                       pointBodyGousya.RowStart + gVo.HyojijunNo + gMcend, _
                                       pointBodyGousya.ColStart + k, _
                                       pointBodyGousya.RowEnd + gVo.HyojijunNo + gousyaMergeCount, _
                                       True)

                        ''試作イベントベース車情報と試作イベント完成車情報の値設定
                        xls.SetValue(pointBodyGousya.ColStart + k, _
                                     pointBodyGousya.RowStart + gVo.HyojijunNo + gMcend, _
                                     pointBodyGousya.ColStart + k, _
                                     pointBodyGousya.RowEnd + gVo.HyojijunNo + gousyaMergeCount, _
                                     strTitleGousyaBodyValue)
                        ''位置はCenter
                        xls.SetAlignment(pointBodyGousya.ColStart + k, _
                                         pointBodyGousya.RowStart + gVo.HyojijunNo + gMcend, _
                                         pointBodyGousya.ColStart + k, _
                                         pointBodyGousya.RowEnd + gVo.HyojijunNo + gousyaMergeCount, _
                                         XlHAlign.xlHAlignCenter, XlVAlign.xlVAlignCenter, True, True)
                    Next

                    ''Get試作装備仕様の値(step3)
                    Dim titleSoubiBody As List(Of EditBlock2ExcelTitle2BodyVo) = _
                        getDate.FindWithTitle2BodyDataBy(shisakuEventCode, shisakuBukaCode, shisakuBlockNo, newShisakuBlockNoKaiteiNo, strGousya)

                    Dim l As Integer
                    For l = 0 To title2Vos.Count - 1
                        Dim strtitle2BodyValue As String = ""
                        Dim strSoubiTitle As String = ""
                        For i As Integer = 0 To titleSoubiBody.Count - 1
                            'エラー回避'
                            If Not titleSoubiBody.Count = 0 Then
                                strtitle2BodyValue = titleSoubiBody.Item(i).ShisakuTekiyou
                                strSoubiTitle = titleSoubiBody(i).ShisakuRetuKoumokuNameDai & "・" & _
                                titleSoubiBody(i).ShisakuRetuKoumokuNameChu & "・" & _
                                titleSoubiBody(i).ShisakuRetuKoumokuName
                            End If

                            ''20110831樺澤 タイトル名が無い場合は飛ばす処理を追加
                            If Not strtitle2BodyValue Is Nothing Then
                                If xls.GetValue(Integer.Parse(pointBodySoubi.ColStart) + l, 4).ToString = strSoubiTitle Then
                                    xls.MergeCells(pointBodySoubi.ColStart + l, _
                                                   pointBodySoubi.RowStart + gVo.HyojijunNo + gMcend, _
                                                   pointBodySoubi.ColStart + l, _
                                                   pointBodySoubi.RowEnd + gVo.HyojijunNo + gousyaMergeCount, True)

                                    ''試作イベントベース車情報と試作イベント完成車情報の値設定
                                    xls.SetValue(pointBodySoubi.ColStart + l, _
                                                 pointBodySoubi.RowStart + gVo.HyojijunNo + gMcend, _
                                                 pointBodySoubi.ColStart + l, _
                                                 pointBodySoubi.RowEnd + gVo.HyojijunNo + gousyaMergeCount, strtitle2BodyValue)
                                    ''位置はCenter
                                    xls.SetAlignment(pointBodySoubi.ColStart + l, _
                                                     pointBodySoubi.RowStart + gVo.HyojijunNo + gMcend, _
                                                     pointBodySoubi.ColStart + l, _
                                                     pointBodySoubi.RowEnd + gVo.HyojijunNo + gousyaMergeCount, _
                                                     XlHAlign.xlHAlignCenter, XlVAlign.xlVAlignCenter, True, True)
                                End If
                            End If

                        Next

                    Next

                    'Get試作メモの値(step4)
                    Dim blockNoList As New List(Of String)
                    blockNoList.Add(shisakuBlockNo)
                    Dim titleMemoBody As List(Of TShisakuSekkeiBlockMemoVo) = _
                        getDate.FindWithTitle3BodyDataBy(shisakuEventCode, shisakuBukaCode, blockNoList, newShisakuBlockNoKaiteiNo, strGousya)

                    Dim m As Integer
                    Dim n As Integer

                    Dim index As Integer = 0

                    '-------------------------------------------------------------------
                    'ここをチューニング
                    '-------------------------------------------------------------------
                    For m = 0 To title3Vos.Count - 1

                        If titleMemoBody.Count > 0 Then
                            For n = 0 To titleMemoBody.Count - 1


                                If title3Vos(m).ShisakuMemoHyoujiJun = titleMemoBody(n).ShisakuMemoHyoujiJun Then
                                    If gVo.HyojijunNo = 0 Then
                                        dataMatrixForMemo(gVo.HyojijunNo, titleMemoBody(n).ShisakuMemoHyoujiJun) = titleMemoBody(n).ShisakuTekiyou
                                    Else
                                        dataMatrixForMemo(gVo.HyojijunNo + gMcend, titleMemoBody(n).ShisakuMemoHyoujiJun) = titleMemoBody(n).ShisakuTekiyou
                                    End If


                                End If

                            Next
                        End If

                    Next

                    'Get試作INSTL品番の人数値(step5)
                    '最新の改訂Noで取得'
                    Dim title4Body As List(Of TShisakuSekkeiBlockInstlVo) = _
                    getDate.FindWithTitle4BodyDataBy(shisakuEventCode, shisakuBukaCode, blockNoList, newShisakuBlockNoKaiteiNo, strGousya)

                    Dim title4BodyB As New List(Of TShisakuSekkeiBlockInstlVo)

                    If isBase Then
                        title4BodyB = _
                        getDate.FindWithTitle4BodyDataBy(shisakuEventCode, shisakuBukaCode, blockNoList, oldShisakuBlockNoKaiteiNo, strGousya, isBase)
                    Else
                        title4BodyB = _
                        getDate.FindWithTitle4BodyDataBy(shisakuEventCode, shisakuBukaCode, blockNoList, oldShisakuBlockNoKaiteiNo, strGousya)
                    End If

                    Dim p As Integer
                    Dim q As Integer
                    Dim pindex As Integer = 0
                    '普通に設定'
                    For p = 0 To title4VosA.Count - 1
                        If title4Body.Count > 0 Then
                            For q = 0 To title4Body.Count - 1
                                If title4VosA(p).InstlHinbanHyoujiJun = title4Body(q).InstlHinbanHyoujiJun Then
                                    ''試作INSTL品番の人数値設定
                                    If title4Body.Item(q).InsuSuryo < 0 Then
                                        dataMatrixForInsu(gVo.HyojijunNo + gMcend, pindex) = "**"
                                    Else
                                        dataMatrixForInsu(gVo.HyojijunNo + gMcend, pindex) = title4Body.Item(q).InsuSuryo.ToString()
                                    End If
                                End If
                            Next
                        End If

                        pindex = pindex + 1
                    Next

                    '2012/08/02 比較先の員数を設定'
                    '全体のタイトルを用意する'
                    'INSTL品番自体は既に貼られているのでそれを元に配置する列を算出する'
                    '2014/12/05 INSTL品番が複数存在するので変更
                    'For p = 0 To colTitleInsuCount - 1
                    '    If title4BodyB.Count > 0 Then
                    '        For q = 0 To title4BodyB.Count - 1
                    '            'INSTL品番が同じ'
                    '            If StringUtil.Equals(xls.GetValue(pointBodyTitleInsu.ColStart + p, _
                    '                                              pointBodyTitleInsu.RowStart + 4, _
                    '                                              pointBodyTitleInsu.ColStart + p, _
                    '                                              pointBodyTitleInsu.RowStart + 4), _
                    '                                              title4BodyB(q).InstlHinban) Then
                    '                'INSTL品番区分が同じ'
                    '                If StringUtil.Equals(xls.GetValue(pointBodyTitleInsu.ColStart + p, _
                    '                                                  pointBodyTitleInsu.RowStart + 5, _
                    '                                                  pointBodyTitleInsu.ColStart + p, _
                    '                                                  pointBodyTitleInsu.RowStart + 5), _
                    '                                                  title4BodyB(q).InstlHinbanKbn) Then
                    '                    ''試作INSTL品番の人数値設定
                    '                    If title4BodyB.Item(q).InsuSuryo < 0 Then
                    '                        dataMatrixForInsu(gVo.HyojijunNo + gousyaMergeCount, p) = "**"
                    '                    Else
                    '                        dataMatrixForInsu(gVo.HyojijunNo + gousyaMergeCount, p) = title4BodyB.Item(q).InsuSuryo.ToString()
                    '                    End If
                    '                End If
                    '            End If
                    '        Next
                    '    End If
                    'Next
                    pindex = 0
                    For p = 0 To title4VosA.Count - 1
                        If title4BodyB.Count > 0 Then
                            For q = 0 To title4BodyB.Count - 1
                                If EzUtil.IsEqualIfNull(title4BodyB(q).InstlHinban, title4VosA(p).InstlHinban) And _
                                   EzUtil.IsEqualIfNull(title4BodyB(q).InstlHinbanKbn, title4VosA(p).InstlHinbanKbn) And _
                                   EzUtil.IsEqualIfNull(title4BodyB(q).InstlDataKbn, title4VosA(p).InstlDataKbn) Then
                                    If Me.EventVo.BlockAlertKind = "2" And Me.EventVo.KounyuShijiFlg = "0" Then
                                        If EzUtil.IsEqualIfNull(title4BodyB(q).BaseInstlFlg, title4VosA(p).BaseInstlFlg) Then
                                            ''試作INSTL品番の人数値設定
                                            If title4BodyB.Item(q).InsuSuryo < 0 Then
                                                dataMatrixForInsu(gVo.HyojijunNo + gousyaMergeCount, pindex) = "**"
                                            Else
                                                dataMatrixForInsu(gVo.HyojijunNo + gousyaMergeCount, pindex) = title4BodyB.Item(q).InsuSuryo.ToString()
                                            End If
                                        End If
                                    Else
                                        ''試作INSTL品番の人数値設定
                                        If title4BodyB.Item(q).InsuSuryo < 0 Then
                                            dataMatrixForInsu(gVo.HyojijunNo + gousyaMergeCount, pindex) = "**"
                                        Else
                                            dataMatrixForInsu(gVo.HyojijunNo + gousyaMergeCount, pindex) = title4BodyB.Item(q).InsuSuryo.ToString()
                                        End If
                                    End If
                                End If
                            Next
                        End If
                        pindex = pindex + 1
                    Next
                    For p = 0 To title4VosC.Count - 1
                        '変更チェック'
                        If title4BodyB.Count > 0 Then
                            For q = 0 To title4BodyB.Count - 1
                                If EzUtil.IsEqualIfNull(title4BodyB(q).InstlHinban, title4VosC(p).InstlHinban) And _
                                   EzUtil.IsEqualIfNull(title4BodyB(q).InstlHinbanKbn, title4VosC(p).InstlHinbanKbn) And _
                                   EzUtil.IsEqualIfNull(title4BodyB(q).InstlDataKbn, title4VosC(p).InstlDataKbn) Then
                                    If Me.EventVo.BlockAlertKind = "2" And Me.EventVo.KounyuShijiFlg = "0" Then
                                        If EzUtil.IsEqualIfNull(title4BodyB(q).BaseInstlFlg, title4VosA(p).BaseInstlFlg) Then
                                            ''試作INSTL品番の人数値設定
                                            If title4BodyB.Item(q).InsuSuryo < 0 Then
                                                dataMatrixForInsu(gVo.HyojijunNo + gousyaMergeCount, pindex) = "**"
                                            Else
                                                dataMatrixForInsu(gVo.HyojijunNo + gousyaMergeCount, pindex) = title4BodyB.Item(q).InsuSuryo.ToString()
                                            End If
                                        End If
                                    Else
                                        ''試作INSTL品番の人数値設定
                                        If title4BodyB.Item(q).InsuSuryo < 0 Then
                                            dataMatrixForInsu(gVo.HyojijunNo + gousyaMergeCount, pindex) = "**"
                                        Else
                                            dataMatrixForInsu(gVo.HyojijunNo + gousyaMergeCount, pindex) = title4BodyB.Item(q).InsuSuryo.ToString()
                                        End If
                                    End If
                                End If
                            Next
                        End If
                        pindex = pindex + 1
                    Next


                    gMcend = gMcend + 1
                    gousyaMergeCount = gousyaMergeCount + 1
                Next

                'メモはマージしないと貼り付けできない?'
                xls.CopyRange(pointBodyMemo.ColStart - 1, pointBodyGousya.RowStart, pointBodyMemo.ColStart + UBound(dataMatrixForMemo, 2) - 1, pointBodyGousya.RowStart + UBound(dataMatrixForMemo), dataMatrixForMemo)
                '員数は組み合わせればいける？'
                xls.CopyRange(pointBodyInsu.ColStart - 1, pointBodyGousya.RowStart, pointBodyInsu.ColStart + UBound(dataMatrixForInsu, 2) - 1, pointBodyGousya.RowStart + UBound(dataMatrixForInsu), dataMatrixForInsu)

                'ここでまとめてAlignmentの設定
                xls.SetAlignment(pointBodyMemo.ColStart, pointBodyGousya.RowStart, pointBodyMemo.ColStart + UBound(dataMatrixForMemo, 2) - 1, pointBodyGousya.RowStart + UBound(dataMatrixForMemo), _
                                 XlHAlign.xlHAlignCenter, XlVAlign.xlVAlignCenter, True, True)

                xls.SetAlignment(pointBodyInsu.ColStart, pointBodyInsu.RowStart, pointBodyInsu.ColStart + UBound(dataMatrixForInsu, 2) - 1, pointBodyInsu.RowStart + UBound(dataMatrixForInsu), _
                                 XlHAlign.xlHAlignCenter, XlVAlign.xlVAlignCenter, True, True)

                '比較する'
                Condition(xls, allGousyaList, pointBodyInsu, pointBodyGousya)
            End If

            '縦線を描く
            xls.SetLine(pointBodyTitleGousya.ColEnd, point.RowStart, pointBodyTitleGousya.ColEnd, endrow, XlBordersIndex.xlEdgeLeft, XlLineStyle.xlDouble, XlBorderWeight.xlThick)
            xls.SetLine(pointBodyTitleSoubi.ColEnd, point.RowStart, pointBodyTitleSoubi.ColEnd, endrow, XlBordersIndex.xlEdgeLeft, XlLineStyle.xlDouble, XlBorderWeight.xlThick)
            xls.SetLine(pointBodyTitleMemo.ColStart, point.RowStart, pointBodyTitleMemo.ColStart, endrow, XlBordersIndex.xlEdgeLeft, XlLineStyle.xlDouble, XlBorderWeight.xlThick)
            xls.SetLine(pointBodyTitleInsu.ColStart, point.RowStart, pointBodyTitleInsu.ColStart, endrow, XlBordersIndex.xlEdgeLeft, XlLineStyle.xlDouble, XlBorderWeight.xlThick)

            For i As Integer = 0 To title4VosA.Count - 1
                '該当イベント取得
                If EventVo.BlockAlertKind = 2 And EventVo.KounyuShijiFlg = "0" Then
                    If title4VosA.Item(i).BaseInstlFlg = 1 Then
                        xls.SetBackColor(pointBodyTitleInsu.ColStart + i, pointBodyTitleInsu.RowStart + 1, pointBodyTitleInsu.ColStart + i, endrow, RGB(176, 215, 237))
                    End If
                End If
            Next

        End Sub


        ''' <summary>
        ''' 比較する
        ''' </summary>
        ''' <param name="xls"></param>
        ''' <param name="allGousyaList"></param>
        ''' <param name="pointBodyInsu"></param>
        ''' <param name="pointBodyGousya"></param>
        ''' <remarks></remarks>
        Private Sub Condition(ByVal xls As ShisakuExcel, _
                              ByVal allGousyaList As List(Of TShisakuEventBaseVo), _
                              ByVal pointBodyInsu As EditBlock2ExcelPointVo, _
                              ByVal pointBodyGousya As EditBlock2ExcelPointVo)
            'マージした行が１ずつ入っているので'
            Dim mergeRowCount As Integer = 0
            Dim str As String = ""



            '号車単位で比較する'
            For Each gVo As TShisakuEventBaseVo In allGousyaList
                Dim index As Integer = gVo.HyojijunNo + mergeRowCount
                '削除フラグ'
                Dim delFlag As Boolean = False
                Dim InsFlag As Boolean = False

                For column As Integer = 0 To colTitleInsuCount - 1
                    '最新の員数'
                    Dim strInsuA As String = ""
                    '改訂前の員数'
                    Dim strInsuB As String = ""

                    '員数を取得する'
                    strInsuA = xls.GetValue(pointBodyInsu.ColStart + column, pointBodyGousya.RowStart + index, pointBodyInsu.ColStart + column, pointBodyGousya.RowStart + index)
                    strInsuB = xls.GetValue(pointBodyInsu.ColStart + column, pointBodyGousya.RowStart + index + 1, pointBodyInsu.ColStart + column, pointBodyGousya.RowStart + index + 1)

                    '員数が同じなら色はつけない'
                    If Not StringUtil.Equals(strInsuA, strInsuB) Then
                        If StringUtil.IsEmpty(strInsuA) Then
                            '員数に差があり、最新が空ならば改訂前のセルを灰色にする'
                            xls.SetBackColor(pointBodyInsu.ColStart + column, pointBodyGousya.RowStart + index + 1, pointBodyInsu.ColStart + column, pointBodyGousya.RowStart + index + 1, &HA0A0A0)
                            delFlag = True
                        Else
                            '員数に差があり、最新が空でないならば改訂前のセルを薄黄色にする'
                            xls.SetBackColor(pointBodyInsu.ColStart + column, pointBodyGousya.RowStart + index, pointBodyInsu.ColStart + column, pointBodyGousya.RowStart + index, &H9FFFFF)
                            InsFlag = True
                        End If
                    End If
                Next

                '適用セルをマージする'
                xls.MergeCells(pointBodyInsu.ColStart + colTitleInsuCount, pointBodyGousya.RowStart + index, pointBodyInsu.ColStart + colTitleInsuCount, pointBodyGousya.RowStart + index + 1, True)
                '適用セルに比較結果を設定する'

                If delFlag AndAlso InsFlag Then
                    '両方TRUEなら適用変更'
                    str = "C:適用変更"
                ElseIf delFlag AndAlso Not InsFlag Then
                    str = "D:適用削除"
                ElseIf Not delFlag AndAlso InsFlag Then
                    str = "A:適用追加"
                Else
                    str = "-"
                End If

                xls.SetValue(pointBodyInsu.ColStart + colTitleInsuCount, pointBodyGousya.RowStart + index, pointBodyInsu.ColStart + colTitleInsuCount, pointBodyGousya.RowStart + index, str)

                mergeRowCount = mergeRowCount + 1

            Next
        End Sub

        ''' <summary>
        ''' ベース車データを読む
        ''' </summary>
        ''' <param name="strKomokuNo">項目No.</param>
        ''' <param name="vo">ベース車データ</param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Function GetTitle1Value(ByVal strKomokuNo As String, ByVal vo As EditBlock2ExcelTitle1BodyVo) As String
            Dim strKomokuName As String = ""

            '無いものは呼ばない'
            If vo Is Nothing Then
                Return ""
            End If


            Select Case strKomokuNo
                ''ベース車開発符号
                Case "1001"
                    strKomokuName = vo.BaseKaihatsuFugo
                    ''ベース車仕様情報№
                Case "1002"
                    strKomokuName = vo.BaseShiyoujyouhouNo
                    ''ベース車アプライド№
                Case "1003"
                    strKomokuName = vo.BaseAppliedNo
                    ''ベース車型式
                Case "1004"
                    strKomokuName = vo.BaseKatashiki
                    ''ベース車仕向
                Case "1005"
                    strKomokuName = vo.BaseShimuke
                    ''ベース車OP
                Case "1006"
                    strKomokuName = vo.BaseOp
                    ''ベース車外装色
                Case "1007"
                    strKomokuName = vo.BaseGaisousyoku
                    ''ベース車内装色
                Case "1008"
                    strKomokuName = vo.BaseNaisousyoku
                    ''試作ベースイベントコード
                Case "1009"
                    strKomokuName = vo.ShisakuBaseEventCode
                    ''試作ベース号車
                Case "1010"
                    strKomokuName = vo.ShisakuBaseGousya

                    ''完成試作型式
                Case "2001"
                    strKomokuName = vo.ShisakuKatashiki
                    ''完成試作仕向け
                Case "2002"
                    strKomokuName = vo.ShisakuShimuke
                    ''完成試作OP
                Case "2003"
                    strKomokuName = vo.ShisakuOp
                    ''完成試作ハンドル
                Case "2004"
                    strKomokuName = vo.ShisakuHandoru
                    ''完成試作車型
                Case "2005"
                    strKomokuName = vo.ShisakuSyagata
                    ''完成試作グレード
                Case "2006"
                    strKomokuName = vo.ShisakuGrade
                    ''完成試作車台№
                Case "2007"
                    strKomokuName = vo.ShisakuSyadaiNo
                    ''完成試作外装色
                Case "2008"
                    strKomokuName = vo.ShisakuGaisousyoku
                    ''完成試作内装色
                Case "2009"
                    strKomokuName = vo.ShisakuNaisousyoku
                    ''完成試作グループ
                Case "2010"
                    strKomokuName = vo.ShisakuGroup
                    ''完成試作工指№
                Case "2011"
                    strKomokuName = vo.ShisakuKoushiNo
                    ''完成試作完成日
                Case "2012"
                    strKomokuName = vo.ShisakuKanseibi
                    ''完成試作E/G型式
                Case "2013"
                    strKomokuName = vo.ShisakuEgKatashiki
                    ''完成試作E/G排気量
                Case "2014"
                    strKomokuName = vo.ShisakuEgHaikiryou
                    ''完成試作E/Gシステム
                Case "2015"
                    strKomokuName = vo.ShisakuEgSystem
                    ''完成試作E/G過給機
                Case "2016"
                    strKomokuName = vo.ShisakuEgKakyuuki
                    ''完成試作T/M駆動
                Case "2017"
                    strKomokuName = vo.ShisakuTmKudou
                    ''完成試作T/M変速機
                Case "2018"
                    strKomokuName = vo.ShisakuTmHensokuki
                    ''完成試作T/M副変速機
                Case "2019"
                    strKomokuName = vo.ShisakuTmFukuHensokuki
                    ''完成試作使用部署
                Case "2020"
                    strKomokuName = vo.ShisakuSiyouBusyo
                    ''完成試作試験目的
                Case "2021"
                    strKomokuName = vo.ShisakuShikenMokuteki


                Case "1011"
                    strKomokuName = vo.SeisakuSyasyu()
                    '' 参考情報・グレード
                Case "1012"
                    strKomokuName = vo.SeisakuGrade
                    '' 参考情報・仕向地・仕向け
                Case "1013"
                    strKomokuName = vo.SeisakuShimuke
                    '' 参考情報・仕向地・ハンドル
                Case "1014"
                    strKomokuName = vo.SeisakuHandoru
                    '' 参考情報・E/G排気量
                Case "1015"
                    strKomokuName = vo.SeisakuEgHaikiryou
                    '' 参考情報・E/G型式
                Case "1016"
                    strKomokuName = vo.SeisakuEgKatashiki
                    '' 参考情報・E/G過給器
                Case "1017"
                    strKomokuName = vo.SeisakuEgKakyuuki
                    '' 参考情報・T/M駆動方式
                Case "1018"
                    strKomokuName = vo.SeisakuTmKudou
                    '' 参考情報・T/M変速機
                Case "1019"
                    strKomokuName = vo.SeisakuTmHensokuki
                    '' 参考情報・車体№
                Case "1020"
                    strKomokuName = vo.SeisakuSyataiNo


                    ''設計展開ベース車開発符号
                Case "1100"
                    strKomokuName = vo.TenkaiBaseKaihatsuFugo
                    ''設計展開ベース車仕様情報№
                Case "1101"
                    strKomokuName = vo.TenkaiBaseShiyoujyouhouNo
                    ''設計展開ベース車アプライド№
                Case "1102"
                    strKomokuName = vo.TenkaiBaseAppliedNo
                    ''設計展開ベース車型式
                Case "1103"
                    strKomokuName = vo.TenkaiBaseKatashiki
                    ''設計展開ベース車仕向
                Case "1104"
                    strKomokuName = vo.TenkaiBaseShimuke
                    ''設計展開ベース車OP
                Case "1105"
                    strKomokuName = vo.TenkaiBaseOp
                    ''設計展開ベース車外装色
                Case "1106"
                    strKomokuName = vo.TenkaiBaseGaisousyoku
                    ''設計展開ベース車内装色
                Case "1107"
                    strKomokuName = vo.TenkaiBaseNaisousyoku
                    ''設計展開試作ベースイベントコード
                Case "1108"
                    strKomokuName = vo.TenkaiShisakuBaseEventCode
                    ''設計展開試作ベース号車
                Case "1109"
                    strKomokuName = vo.TenkaiShisakuBaseGousya


                    '' 仕向地・仕向け
                Case "2022"
                    strKomokuName = vo.ShisakuShimukechiShimuke
                    '' 完成試作E/Gメモ１
                Case "2023"
                    strKomokuName = vo.ShisakuEgMemo1
                    '' 完成試作E/Gメモ２
                Case "2024"
                    strKomokuName = vo.ShisakuEgMemo2
                    '' 完成試作T/Mメモ１
                Case "2025"
                    strKomokuName = vo.ShisakuTmMemo1
                    '' 完成試作T/Mメモ２
                Case "2026"
                    strKomokuName = vo.ShisakuTmMemo2
                    '' 完成試作外装色名
                Case "2027"
                    strKomokuName = vo.ShisakuGaisousyokuName
                    '' 完成試作内装色名
                Case "2028"
                    strKomokuName = vo.ShisakuNaisousyokuName
                    '' 完成試作使用目的
                Case "2029"
                    strKomokuName = vo.ShisakuShiyouMokuteki
                    '' 完成試作製作順序
                Case "2030"
                    strKomokuName = vo.ShisakuSeisakuJunjyo
                    '' 完成試作製作方法区分
                Case "2031"
                    strKomokuName = vo.ShisakuSeisakuHouhouKbn
                    '' 完成試作製作方法
                Case "2032"
                    strKomokuName = vo.ShisakuSeisakuHouhou
                    '' 完成試作メモ欄
                Case "2033"
                    strKomokuName = vo.ShisakuMemo


            End Select
            Return strKomokuName
        End Function
        ''' <summary>
        ''' GetタイトルA,B,C...
        ''' </summary>
        ''' <param name="loopNum">Loop number</param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Private Function IntToString(ByVal loopNum As Integer) As String
            If loopNum >= 702 Then
                Return ""
            Else
                If loopNum < 26 Then
                    Return IntToChar(loopNum)
                Else
                    Dim num1 As Integer = loopNum \ 26 - 1
                    Dim num2 = loopNum Mod 26
                    Return IntToChar(num1) & IntToChar(num2)
                End If
            End If
        End Function
        ''' <summary>
        ''' Chrを返す
        ''' </summary>
        ''' <param name="num">number</param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Private Function IntToChar(ByVal num As Integer) As String
            Return Chr(Asc("A") + num)
        End Function

    End Class

End Namespace