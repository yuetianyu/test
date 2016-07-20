Imports ShisakuCommon
Imports ShisakuCommon.Ui
Imports EventSakusei.ShisakuBuhinEditBlock.Dao
Imports ShisakuCommon.Db.EBom.Vo

Namespace ShisakuBuhinEditBlock.Export2Excel
    ''' <summary>
    ''' 構成比較用クラス
    ''' </summary>
    ''' <remarks></remarks>
    Public Class ShisakuBuhinEditCondition

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
        ''' <summary>試作ブロックNoリスト</summary>
        Private blockNoList As List(Of String)
        ''' <summary>ベースか？</summary>
        Private isBase As Boolean
        ''' <summary>担当者名</summary>
        Private tantousya As String
        ''' <summary>TEL.No</summary>
        Private TelNo As String
        ''' <summary>試作イベント名称</summary>
        Private shisakuEventName As String
        ''' <summary>試作開発符号</summary>
        Private shisakuKaihatsuFugo As String
        ''' <summary>Excelヘッダー用データ</summary>
        Private headTitleVos As EditBlock2ExcelTitle3BodyVo



#End Region



        ''' <summary>
        ''' コンストラクタ
        ''' </summary>
        ''' <param name="shisakuEventCode">試作イベントコード</param>
        ''' <param name="shisakuBukaCode">試作部課コード</param>
        ''' <param name="shisakuBlockNo">試作ブロックNo</param>
        ''' <param name="newShisakuBlockNoKaiteiNo">試作ブロックNo改訂No(新)</param>
        ''' <param name="oldShisakuBlockNoKaiteiNo">試作ブロックNo改訂No(旧)</param>
        ''' <param name="blockNoList">ブロックNoリスト</param>
        ''' <param name="isBase">ベースか?</param>
        ''' <remarks></remarks>
        Public Sub New(ByVal shisakuEventCode As String, _
                       ByVal shisakuBukaCode As String, _
                       ByVal shisakuBlockNo As String, _
                       ByVal newShisakuBlockNoKaiteiNo As String, _
                       ByVal oldShisakuBlockNoKaiteiNo As String, _
                       ByVal blockNoList As List(Of String), _
                       ByVal isBase As Boolean)

            Me.shisakuEventCode = shisakuEventCode
            Me.shisakuBukaCode = shisakuBukaCode
            Me.shisakuBlockNo = shisakuBlockNo
            Me.newShisakuBlockNoKaiteiNo = newShisakuBlockNoKaiteiNo
            Me.oldShisakuBlockNoKaiteiNo = oldShisakuBlockNoKaiteiNo
            Me.blockNoList = blockNoList
            Me.isBase = isBase
            Dim getDate As New EditBlock2ExcelDaoImpl()

            'タイトルデータ取得'
            headTitleVos = getDate.FindHeadInfoWithSekkeiBlockBy(shisakuEventCode, _
                                                                 shisakuBukaCode, _
                                                                 shisakuBlockNo, _
                                                                 newShisakuBlockNoKaiteiNo)
            'タイトル用変数取得'
            If Not headTitleVos Is Nothing Then
                tantousya = headTitleVos.UserId
                TelNo = headTitleVos.TelNo
                shisakuEventName = headTitleVos.ShisakuEventName
                shisakuKaihatsuFugo = headTitleVos.ShisakuKaihatsuFugo
            Else
                tantousya = ""
                TelNo = ""
                shisakuEventName = ""
                shisakuKaihatsuFugo = ""
            End If

        End Sub

        ''' <summary>
        ''' エクセル出力
        ''' </summary>
        ''' <remarks></remarks>
        Public Sub ExportShisakuBuhinEditConditon()
            Dim fileName As String = ""
            Dim getDate As New EditBlock2ExcelDaoImpl()
            Dim shisakiEventVo As TShisakuEventVo = getDate.FindByEvent(shisakuEventCode)

            '出力Excel名取得'
            Using sfd As New SaveFileDialog()
                sfd.FileName = ShisakuCommon.ShisakuGlobal.ExcelOutPut
                sfd.InitialDirectory = System.Environment.GetFolderPath(Environment.SpecialFolder.Personal)


                If Not blockNoList Is Nothing Then
                    If blockNoList.Count = 1 Then
                        '比較対象が新>旧になっているようにする'
                        If Integer.Parse(newShisakuBlockNoKaiteiNo) < Integer.Parse(oldShisakuBlockNoKaiteiNo) Then
                            Dim kaitei As String = newShisakuBlockNoKaiteiNo
                            newShisakuBlockNoKaiteiNo = oldShisakuBlockNoKaiteiNo
                            oldShisakuBlockNoKaiteiNo = kaitei
                        End If
                        If isBase Then
                            '補用品不具合展開
                            fileName = shisakiEventVo.ShisakuKaihatsuFugo + shisakiEventVo.ShisakuEventName + " " + shisakuBukaCode + shisakuBlockNo + "比較" + "ベース" + "⇔" + newShisakuBlockNoKaiteiNo + " " + Now.ToString("MMdd") + " " + Now.ToString("HHmm") + ".xls"
                        Else
                            fileName = shisakiEventVo.ShisakuKaihatsuFugo + shisakiEventVo.ShisakuEventName + " " + shisakuBukaCode + shisakuBlockNo + "比較" + oldShisakuBlockNoKaiteiNo + "⇔" + newShisakuBlockNoKaiteiNo + " " + Now.ToString("MMdd") + " " + Now.ToString("HHmm") + ".xls"
                        End If
                    Else
                        fileName = shisakiEventVo.ShisakuKaihatsuFugo + shisakiEventVo.ShisakuEventName + " " + shisakuBukaCode + "比較ベース⇔最新" + " " + Now.ToString("MMdd") + " " + Now.ToString("HHmm") + ".xls"
                    End If
                Else
                    '全ブロック'
                    fileName = shisakiEventVo.ShisakuKaihatsuFugo + shisakiEventVo.ShisakuEventName + " " + shisakuBukaCode + "比較ベース⇔最新" + " " + Now.ToString("MMdd") + " " + Now.ToString("HHmm") + ".xls"
                End If

                '--------------------------------------------------
                'EXCEL出力先フォルダチェック
                ShisakuCommon.ShisakuComFunc.ExcelOutPutDirCheck()
                '--------------------------------------------------

                '出力Excel名'
                fileName = ShisakuCommon.ShisakuGlobal.ExcelOutPutDir + "\" + StringUtil.ReplaceInvalidCharForFileName(fileName)
            End Using

            'エクセル出力'
            If Not ShisakuComFunc.IsFileOpen(ShisakuCommon.ShisakuGlobal.ExcelOutPut) Then
                Using xls As New ShisakuExcel(fileName)
                    xls.OpenBook(fileName)
                    xls.ClearWorkBook()
                    If Not blockNoList Is Nothing Then
                        If blockNoList.Count = 1 Then
                            'AL比較'(Sheet1)
                            Dim AL As New ExportShisakuBuhinEditALConditionExcel(shisakuEventCode, _
                                                                                 shisakuBukaCode, _
                                                                                 shisakuBlockNo, _
                                                                                 newShisakuBlockNoKaiteiNo, _
                                                                                 oldShisakuBlockNoKaiteiNo)
                            AL.Execute(xls, tantousya, TelNo, shisakuEventName, shisakuKaihatsuFugo, isBase)

                            '部品比較'(Sheet2)
                            Dim Buhin As New ExportShisakuBuhinEditBuhinConditionExcel(shisakuEventCode, _
                                                                                       shisakuBukaCode, _
                                                                                       shisakuBlockNo, _
                                                                                       newShisakuBlockNoKaiteiNo, _
                                                                                       oldShisakuBlockNoKaiteiNo)
                            Buhin.Excute(xls, tantousya, TelNo, shisakuKaihatsuFugo, shisakuEventName, fileName, isBase)

                            '号車展開'(Sheet3)
                            Dim Gousya As New ExportShisakuBuhinEditGousyaConditionExcel(shisakuEventCode, _
                                                                                         shisakuBukaCode, _
                                                                                         shisakuBlockNo, _
                                                                                         newShisakuBlockNoKaiteiNo, _
                                                                                         oldShisakuBlockNoKaiteiNo)

                            Gousya.Excute(xls, tantousya, TelNo, shisakuKaihatsuFugo, shisakuEventName, fileName, "", isBase)
                        Else
                            '号車複数展開'
                            Dim Multi As New ExportShisakuBuhinEditConditonMultiBlockExcel(shisakuEventCode, shisakuBukaCode)
                            Multi.Initialize(blockNoList)
                            Multi.Excute(xls, fileName, "")

                        End If
                    Else
                        '号車複数展開'
                        Dim Multi As New ExportShisakuBuhinEditConditonMultiBlockExcel(shisakuEventCode, shisakuBukaCode)
                        Multi.Initialize(blockNoList)
                        Multi.Excute(xls, fileName, "")
                    End If




                    xls.SetActiveSheet(1)
                    '2012/02/02'
                    'A4横で印刷できるように変更'
                    xls.PrintPaper(fileName, 1, "A4")
                    xls.PrintOrientation(fileName, 1, 1, False)
                    xls.Save()
                End Using
                Process.Start(fileName)
            End If

        End Sub




    End Class
End Namespace