Imports EventSakusei.ShisakuBuhinEdit.Kosei.Dao
Imports ShisakuCommon.Db.EBom.Vo
'↓↓2014/10/01 酒井 ADD BEGIN
Imports EventSakusei.ShisakuBuhinMenu.Dao
'↑↑2014/10/01 酒井 ADD END

Public Class Frm41DispKosei

    ''↓↓2014/08/05 2集計コード R/Yのブロック間紐付け f) (TES)施 ADD 

    Public Sub setrhac0552(ByVal BuhinNoOya As String, ByVal rhac0552Vos As List(Of Rhac0552Vo))
        Dim index As Integer = 0
        '↓↓2014/10/23 酒井 ADD BEGIN
        'Dim level As Integer = 1
        Dim level As Integer = 0
        '↑↑2014/10/23 酒井 ADD END
        '↓↓2014/10/22 酒井 ADD BEGIN
        'Dim buhinnooyalist As New List(Of String)
        'Dim buhinnokolist As New List(Of String)
        'lbMain.Items.Add(BuhinNoOya)
        Dim rowindex As Integer = 1
        Dim outputVos As New List(Of DispKoseiVo)
        Dim tmpVos As New List(Of DispKoseiVo)
        Dim oyaVo As New DispKoseiVo
        oyaVo.level = level
        oyaVo.BuhinNo = BuhinNoOya
        oyaVo.BuhinNoOutput = BuhinNoOya
        outputVos.Add(oyaVo)
        'buhinnooyalist.Add(BuhinNoOya)
        '↑↑2014/10/22 酒井 ADD END

        '↓↓2014/10/01 酒井 ADD BEGIN
        Dim impl As BuhinEditBaseDao = New BuhinEditBaseDaoimpl
        '↑↑2014/10/01 酒井 ADD END

        Do While index < rhac0552Vos.Count
            '↓↓2014/10/22 酒井 ADD BEGIN
            'For Each oya As String In buhinnooyalist
            '    For Each vo As Rhac0552Vo In rhac0552Vos
            '        If oya.Trim = vo.BuhinNoOya.Trim Then
            '            '↓↓2014/10/01 酒井 ADD BEGIN
            '            '部品名称取得
            '            Dim aRhac0532Vo As New Rhac0532Vo
            '            aRhac0532Vo = impl.FindByRhac0532(vo.BuhinNoKo)
            '            '取引先コード取得
            '            Dim aTShisakuBuhinEditVo As New TShisakuBuhinEditVo
            '            aTShisakuBuhinEditVo = impl.FindByKoutanTorihikisaki(vo.BuhinNoKo)
            '            '                        lbMain.Items.Add(Space(level) & "∟" & vo.BuhinNoKo)
            '            '↓↓2014/10/22 酒井 ADD BEGIN
            '            'lbMain.Items.Add( _
            '            'Space(level) & "∟" & vo.BuhinNoKo & Space(15 - Len(Space(level) & "∟" & Trim(vo.BuhinNoKo))) _
            '            '& "," & aRhac0532Vo.BuhinName & Space(30 - Len(aRhac0532Vo.BuhinName)) _
            '            '& "," & vo.InsuSuryo & Space(3 - Len(vo.InsuSuryo.ToString)) _
            '            '& "," & vo.ShukeiCode & Space(1 - Len(vo.ShukeiCode)) _
            '            '& "," & vo.SiaShukeiCode & Space(1 - Len(vo.SiaShukeiCode)) _
            '            '& "," & aTShisakuBuhinEditVo.MakerCode & Space(4 - Len(aTShisakuBuhinEditVo.MakerCode)) _
            '            ')
            '            rowindex = rowindex + 1
            '            '↑↑2014/10/22 酒井 ADD END
            '            '↑↑2014/10/01 酒井 ADD END
            '            buhinnokolist.Add(vo.BuhinNoKo)
            '            index = index + 1
            '            Continue For
            '        End If
            '    Next
            '            Next
            For Each tmpVo As DispKoseiVo In outputVos
                tmpVos.Add(tmpVo)

                If tmpVo.level = level Then
                Else
                    '直下の子部品抽出済みのため、スキップ
                    Continue For
                End If

                For Each vo As Rhac0552Vo In rhac0552Vos
                    If tmpVo.BuhinNo.Trim = vo.BuhinNoOya.Trim Then
                        '部品名称取得
                        Dim aRhac0532Vo As New Rhac0532Vo
                        aRhac0532Vo = impl.FindByRhac0532(vo.BuhinNoKo)
                        '取引先コード取得
                        Dim aTShisakuBuhinEditVo As New TShisakuBuhinEditVo
                        aTShisakuBuhinEditVo = impl.FindByKoutanTorihikisaki(vo.BuhinNoKo)
                        Dim aVo As New DispKoseiVo
                        aVo.level = level + 1
                        aVo.BuhinNo = vo.BuhinNoKo
                        aVo.BuhinNoOutput = Space(level) & "∟" & vo.BuhinNoKo
                        aVo.BuhinName = "," & aRhac0532Vo.BuhinName
                        aVo.InsuSuryo = "," & vo.InsuSuryo
                        aVo.ShukeiCode = "," & vo.ShukeiCode
                        aVo.SiaShukeiCode = "," & vo.SiaShukeiCode
                        aVo.MakerCode = "," & aTShisakuBuhinEditVo.MakerCode
                        tmpVos.Add(aVo)
                        rowindex = rowindex + 1
                        index = index + 1
                        Continue For
                    End If
                Next
            Next
            '↑↑2014/10/22 酒井 ADD END
            outputVos = tmpVos
            tmpVos = New List(Of DispKoseiVo)
            ''↓↓2014/09/17 酒井 AND BEGIN
            level = level + 1
            ''↑↑2014/09/17 酒井 AND BEGIN
        Loop
        Me.Text = "図面情報"

        '↓↓2014/10/22 酒井 ADD BEGIN
        rowindex = 0
        For Each outputVo As DispKoseiVo In outputVos
            spdResult_Sheet1.Cells(rowindex, 0).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Left
            spdResult_Sheet1.Cells(rowindex, 0).Value = outputVo.BuhinNoOutput
            spdResult_Sheet1.Cells(rowindex, 1).Value = outputVo.BuhinName
            spdResult_Sheet1.Cells(rowindex, 2).Value = outputVo.InsuSuryo
            spdResult_Sheet1.Cells(rowindex, 3).Value = outputVo.ShukeiCode
            spdResult_Sheet1.Cells(rowindex, 4).Value = outputVo.SiaShukeiCode
            spdResult_Sheet1.Cells(rowindex, 5).Value = outputVo.MakerCode
            rowindex = rowindex + 1
        Next
        '↑↑2014/10/22 酒井 ADD END
        Exit Sub
    End Sub

    Public Sub setrhac0553(ByVal BuhinNoOya As String, ByVal rhac0553Vos As List(Of Rhac0553Vo))
        Dim index As Integer = 0
        '↓↓2014/10/23 酒井 ADD BEGIN
        'Dim level As Integer = 1
        Dim level As Integer = 0
        '↑↑2014/10/23 酒井 ADD END
        '↓↓2014/10/22 酒井 ADD BEGIN
        'Dim buhinnooyalist As New List(Of String)
        'Dim buhinnokolist As New List(Of String)
        'lbMain.Items.Add(BuhinNoOya)
        Dim rowindex As Integer = 1
        Dim outputVos As New List(Of DispKoseiVo)
        Dim tmpVos As New List(Of DispKoseiVo)
        Dim oyaVo As New DispKoseiVo
        oyaVo.level = level
        oyaVo.BuhinNo = BuhinNoOya
        oyaVo.BuhinNoOutput = BuhinNoOya
        outputVos.Add(oyaVo)
        'buhinnooyalist.Add(BuhinNoOya)
        '↑↑2014/10/22 酒井 ADD END

        '↓↓2014/10/01 酒井 ADD BEGIN
        Dim impl As BuhinEditBaseDao = New BuhinEditBaseDaoImpl
        '↑↑2014/10/01 酒井 ADD END

        Do While index < rhac0553Vos.Count
            '↓↓2014/10/22 酒井 ADD BEGIN
            'For Each oya As String In buhinnooyalist
            '    For Each vo As Rhac0552Vo In rhac0552Vos
            '        If oya.Trim = vo.BuhinNoOya.Trim Then
            '            '↓↓2014/10/01 酒井 ADD BEGIN
            '            '部品名称取得
            '            Dim aRhac0532Vo As New Rhac0532Vo
            '            aRhac0532Vo = impl.FindByRhac0532(vo.BuhinNoKo)
            '            '取引先コード取得
            '            Dim aTShisakuBuhinEditVo As New TShisakuBuhinEditVo
            '            aTShisakuBuhinEditVo = impl.FindByKoutanTorihikisaki(vo.BuhinNoKo)
            '            '                        lbMain.Items.Add(Space(level) & "∟" & vo.BuhinNoKo)
            '            '↓↓2014/10/22 酒井 ADD BEGIN
            '            'lbMain.Items.Add( _
            '            'Space(level) & "∟" & vo.BuhinNoKo & Space(15 - Len(Space(level) & "∟" & Trim(vo.BuhinNoKo))) _
            '            '& "," & aRhac0532Vo.BuhinName & Space(30 - Len(aRhac0532Vo.BuhinName)) _
            '            '& "," & vo.InsuSuryo & Space(3 - Len(vo.InsuSuryo.ToString)) _
            '            '& "," & vo.ShukeiCode & Space(1 - Len(vo.ShukeiCode)) _
            '            '& "," & vo.SiaShukeiCode & Space(1 - Len(vo.SiaShukeiCode)) _
            '            '& "," & aTShisakuBuhinEditVo.MakerCode & Space(4 - Len(aTShisakuBuhinEditVo.MakerCode)) _
            '            ')
            '            rowindex = rowindex + 1
            '            '↑↑2014/10/22 酒井 ADD END
            '            '↑↑2014/10/01 酒井 ADD END
            '            buhinnokolist.Add(vo.BuhinNoKo)
            '            index = index + 1
            '            Continue For
            '        End If
            '    Next
            '            Next
            For Each tmpVo As DispKoseiVo In outputVos
                tmpVos.Add(tmpVo)

                If tmpVo.level = level Then
                Else
                    '直下の子部品抽出済みのため、スキップ
                    Continue For
                End If

                For Each vo As Rhac0553Vo In rhac0553Vos
                    If tmpVo.BuhinNo.Trim = vo.BuhinNoOya.Trim Then
                        '部品名称取得
                        Dim aRhac0532Vo As New Rhac0532Vo
                        aRhac0532Vo = impl.FindByRhac0532(vo.BuhinNoKo)
                        '取引先コード取得
                        Dim aTShisakuBuhinEditVo As New TShisakuBuhinEditVo
                        aTShisakuBuhinEditVo = impl.FindByKoutanTorihikisaki(vo.BuhinNoKo)
                        Dim aVo As New DispKoseiVo
                        aVo.level = level + 1
                        aVo.BuhinNo = vo.BuhinNoKo
                        aVo.BuhinNoOutput = Space(level) & "∟" & vo.BuhinNoKo
                        aVo.BuhinName = "," & aRhac0532Vo.BuhinName
                        aVo.InsuSuryo = "," & vo.InsuSuryo
                        aVo.ShukeiCode = "," & vo.ShukeiCode
                        aVo.SiaShukeiCode = "," & vo.SiaShukeiCode
                        aVo.MakerCode = "," & aTShisakuBuhinEditVo.MakerCode
                        tmpVos.Add(aVo)
                        rowindex = rowindex + 1
                        index = index + 1
                        Continue For
                    End If
                Next
            Next
            '↑↑2014/10/22 酒井 ADD END
            outputVos = tmpVos
            tmpVos = New List(Of DispKoseiVo)
            ''↓↓2014/09/17 酒井 AND BEGIN
            level = level + 1
            ''↑↑2014/09/17 酒井 AND BEGIN
        Loop
        Me.Text = "開発符号と構成編集ツール"

        '↓↓2014/10/22 酒井 ADD BEGIN
        rowindex = 0
        For Each outputVo As DispKoseiVo In outputVos
            spdResult_Sheet1.Cells(rowindex, 0).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Left
            spdResult_Sheet1.Cells(rowindex, 0).Value = outputVo.BuhinNoOutput
            spdResult_Sheet1.Cells(rowindex, 1).Value = outputVo.BuhinName
            spdResult_Sheet1.Cells(rowindex, 2).Value = outputVo.InsuSuryo
            spdResult_Sheet1.Cells(rowindex, 3).Value = outputVo.ShukeiCode
            spdResult_Sheet1.Cells(rowindex, 4).Value = outputVo.SiaShukeiCode
            spdResult_Sheet1.Cells(rowindex, 5).Value = outputVo.MakerCode
            rowindex = rowindex + 1
        Next
        '↑↑2014/10/22 酒井 ADD END
        Exit Sub

    End Sub

    ''↑↑2014/08/05 2集計コード R/Yのブロック間紐付け f) (TES)施 ADD 

End Class