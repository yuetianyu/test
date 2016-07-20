Imports ShisakuCommon.Db.EBom.Dao
Imports ShisakuCommon
Imports ShisakuCommon.Db.EBom.Vo
Imports ShisakuCommon.Db.EBom.Dao.Impl
Imports YosansyoTool.YosanBuhinEdit.Kosei.Logic.Matrix

Namespace YosanBuhinEdit.Kosei.Logic
    ''' <summary>
    ''' 試作部品編集INSTL情報の作成データを供給するクラス
    ''' </summary>
    ''' <remarks></remarks>
    Public Class BuhinEditKoseiEditInstlSupplier
        Private _yosanEventCode As String
        Private _unitKbn As String
        Private _matrix As BuhinKoseiMatrix
        Private _patternVos As List(Of TYosanBuhinEditPatternVo)

        ''' <summary>
        ''' コンストラクタ
        ''' </summary>
        ''' <param name="yosanEventCode">予算書イベントコード</param>
        ''' <param name="unitKbn">ユニット区分</param>
        ''' <param name="matrix">部品表</param>
        ''' <param name="patternVos">パターン</param>
        ''' <remarks></remarks>
        Public Sub New(ByVal yosanEventCode As String, ByVal unitKbn As String, ByVal matrix As BuhinKoseiMatrix, ByVal patternVos As List(Of TYosanBuhinEditPatternVo))
            Me._yosanEventCode = yosanEventCode
            Me._unitKbn = unitKbn
            Me._matrix = matrix
            Me._patternVos = patternVos
        End Sub

        ''' <summary>
        ''' 登録用のデータを作成する
        ''' </summary>
        ''' <returns>登録用のデータ</returns>
        ''' <remarks></remarks>
        Public Function MakeValues(ByVal patternVos As List(Of TYosanBuhinEditPatternVo)) As List(Of TYosanBuhinEditInsuVo)
            Dim results As New List(Of TYosanBuhinEditInsuVo)
            Dim wHyoujiJun As Integer = 0

            For Each rowIndex As Integer In _matrix.GetInputRowIndexes()
                Dim wColumnFlg As String = Nothing


                'For Each columnIndex As Integer In _matrix.GetInputInsuColumnIndexesOnRow(rowIndex)
                For columnIndex As Integer = 0 To patternVos.Count - 1
                    If _matrix.InsuVo(rowIndex, columnIndex).InsuSuryo Is Nothing Then
                        Continue For
                    End If

                    With _matrix.InsuVo(rowIndex, columnIndex)
                        Dim patternDao As TYosanBuhinEditPatternDao = New TYosanBuhinEditPatternDaoImpl
                        Dim param As New TYosanBuhinEditPatternVo
                        param.YosanEventCode = _yosanEventCode
                        param.BuhinhyoName = _unitKbn
                        param.PatternHyoujiJun = columnIndex

                        If 0 = patternDao.CountBy(param) Then
                            Continue For
                        End If

                        .YosanEventCode = _yosanEventCode
                        .BuhinhyoName = _unitKbn
                        .YosanBukaCode = _matrix.Record(rowIndex).YosanBukaCode
                        .YosanBlockNo = _matrix.Record(rowIndex).YosanBlockNo

                        '部品番号表示順を１から採番
                        .BuhinNoHyoujiJun = wHyoujiJun
                        .PatternHyoujiJun = columnIndex

                        '行に値があればチェック
                        wColumnFlg = "OK"
                    End With
                    results.Add(_matrix.InsuVo(rowIndex, columnIndex))
                Next
                If wColumnFlg = "OK" Then
                    '部品番号表示順を＋１
                    wHyoujiJun = wHyoujiJun + 1
                End If
            Next

            Return results
        End Function

        ''' <summary>
        ''' 更新する
        ''' </summary>
        ''' <param name="login">ログイン情報</param>
        ''' <param name="editInsuDao">試作部品編集員数Dao</param>
        ''' <param name="editInsuRirekiDao">試作部品編集員数履歴Dao</param>
        ''' <param name="editPatternDao">試作部品編集パターン履歴Dao</param>
        ''' <param name="editPatternRirekiDao">試作部品編集パターン履歴Dao</param>
        ''' <param name="aDate">試作日付</param>
        ''' <remarks></remarks>
        Public Sub Update(ByVal login As LoginInfo, ByVal editInsuDao As TYosanBuhinEditInsuDao, ByVal editInsuRirekiDao As TYosanBuhinEditInsuRirekiDao, _
                          ByVal editPatternDao As TYosanBuhinEditPatternDao, ByVal editPatternRirekiDao As TYosanBuhinEditPatternRirekiDao, ByVal aDate As ShisakuDate)

            Dim paramPattern As New TYosanBuhinEditPatternVo
            paramPattern.YosanEventCode = _yosanEventCode
            paramPattern.BuhinhyoName = _unitKbn
            editPatternDao.DeleteBy(paramPattern)

            'Dim paramPatterns As New TYosanBuhinEditPatternRirekiVo
            'paramPatterns.YosanEventCode = _yosanEventCode
            'paramPatterns.UnitKbn = _unitKbn
            'editPatternRirekiDao.DeleteBy(paramPatterns)

            Dim wHyoujiJun As Integer = 0
            For Each vo As TYosanBuhinEditPatternVo In _patternVos
                vo.YosanEventCode = _yosanEventCode
                vo.BuhinhyoName = _unitKbn
                vo.PatternHyoujiJun = wHyoujiJun
                If StringUtil.IsEmpty(vo.CreatedUserId) Then
                    With vo
                        .CreatedUserId = login.UserId
                        .CreatedDate = aDate.CurrentDateDbFormat
                        .CreatedTime = aDate.CurrentTimeDbFormat
                    End With
                Else
                    With vo
                        .CreatedUserId = vo.CreatedUserId
                        .CreatedDate = vo.CreatedDate
                        .CreatedTime = vo.CreatedTime
                    End With
                End If
                vo.UpdatedUserId = login.UserId
                vo.UpdatedDate = aDate.CurrentDateDbFormat
                vo.UpdatedTime = aDate.CurrentTimeDbFormat
                editPatternDao.InsertBy(vo)

                'Dim rirekiVo As New TYosanBuhinEditPatternRirekiVo
                'rirekiVo.YosanEventCode = _yosanEventCode
                'rirekiVo.UnitKbn = _unitKbn
                'rirekiVo.RegisterDate = DateUtil.ConvDateToIneteger(aDate.CurrentDateTime).ToString
                'rirekiVo.PatternHyoujiJun = wHyoujiJun
                'rirekiVo.PatternName = vo.PatternName
                'If StringUtil.IsEmpty(vo.CreatedUserId) Then
                '    With rirekiVo
                '        .CreatedUserId = login.UserId
                '        .CreatedDate = aDate.CurrentDateDbFormat
                '        .CreatedTime = aDate.CurrentTimeDbFormat
                '    End With
                'Else
                '    With rirekiVo
                '        .CreatedUserId = vo.CreatedUserId
                '        .CreatedDate = vo.CreatedDate
                '        .CreatedTime = vo.CreatedTime
                '    End With
                'End If
                'rirekiVo.UpdatedUserId = login.UserId
                'rirekiVo.UpdatedDate = aDate.CurrentDateDbFormat
                'rirekiVo.UpdatedTime = aDate.CurrentTimeDbFormat
                'editPatternRirekiDao.InsertBy(rirekiVo)

                wHyoujiJun = wHyoujiJun + 1
            Next

            Dim paramInsu As New TYosanBuhinEditInsuVo
            paramInsu.YosanEventCode = _yosanEventCode
            paramInsu.BuhinhyoName = _unitKbn
            editInsuDao.DeleteBy(paramInsu)

            'Dim paramInsuRireki As New TYosanBuhinEditInsuRirekiVo
            'paramInsuRireki.YosanEventCode = _yosanEventCode
            'paramInsuRireki.UnitKbn = _unitKbn
            'editInsuRirekiDao.DeleteBy(paramInsuRireki)

            For Each vo As TYosanBuhinEditInsuVo In MakeValues(_patternVos)
                vo.SaisyuKoushinbi = DateUtil.ConvDateToIneteger(aDate.CurrentDateTime)
                If StringUtil.IsEmpty(vo.CreatedUserId) Then
                    With vo
                        .CreatedUserId = login.UserId
                        .CreatedDate = aDate.CurrentDateDbFormat
                        .CreatedTime = aDate.CurrentTimeDbFormat
                    End With
                Else
                    With vo
                        .CreatedUserId = vo.CreatedUserId
                        .CreatedDate = vo.CreatedDate
                        .CreatedTime = vo.CreatedTime
                    End With
                End If
                vo.UpdatedUserId = login.UserId
                vo.UpdatedDate = aDate.CurrentDateDbFormat
                vo.UpdatedTime = aDate.CurrentTimeDbFormat
                editInsuDao.InsertBy(vo)

                'Dim rirekiVo As New TYosanBuhinEditInsuRirekiVo
                'rirekiVo.YosanEventCode = _yosanEventCode
                'rirekiVo.UnitKbn = _unitKbn
                'rirekiVo.RegisterDate = DateUtil.ConvDateToIneteger(aDate.CurrentDateTime).ToString
                'rirekiVo.YosanBukaCode = vo.YosanBukaCode
                'rirekiVo.YosanBlockNo = vo.YosanBlockNo
                'rirekiVo.BuhinNoHyoujiJun = vo.BuhinNoHyoujiJun
                'rirekiVo.PatternHyoujiJun = vo.PatternHyoujiJun
                'rirekiVo.InsuSuryo = vo.InsuSuryo
                'rirekiVo.SaisyuKoushinbi = DateUtil.ConvDateToIneteger(aDate.CurrentDateTime)
                'If StringUtil.IsEmpty(vo.CreatedUserId) Then
                '    With rirekiVo
                '        .CreatedUserId = login.UserId
                '        .CreatedDate = aDate.CurrentDateDbFormat
                '        .CreatedTime = aDate.CurrentTimeDbFormat
                '    End With
                'Else
                '    With rirekiVo
                '        .CreatedUserId = vo.CreatedUserId
                '        .CreatedDate = vo.CreatedDate
                '        .CreatedTime = vo.CreatedTime
                '    End With
                'End If
                'rirekiVo.UpdatedUserId = login.UserId
                'rirekiVo.UpdatedDate = aDate.CurrentDateDbFormat
                'rirekiVo.UpdatedTime = aDate.CurrentTimeDbFormat
                'editInsuRirekiDao.InsertBy(rirekiVo)
            Next

        End Sub

    End Class
End Namespace