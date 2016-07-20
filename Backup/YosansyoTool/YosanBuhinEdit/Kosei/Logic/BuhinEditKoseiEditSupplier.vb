Imports ShisakuCommon.Db.EBom.Dao
Imports ShisakuCommon
Imports ShisakuCommon.Db.EBom.Vo
Imports System.Text
Imports YosansyoTool.YosanBuhinEdit.Kosei.Logic.Matrix

Namespace YosanBuhinEdit.Kosei.Logic
    ''' <summary>
    ''' 補用部品編集情報の作成データを供給するクラス
    ''' </summary>
    ''' <remarks></remarks>
    Public Class BuhinEditKoseiEditSupplier
        Private _yosanEventCode As String
        Private _unitKbn As String
        Private _matrix As BuhinKoseiMatrix

        ''' <summary>
        ''' コンストラクタ
        ''' </summary>
        ''' <param name="yosanEventCode">予算書イベントコード</param>
        ''' <param name="unitKbn">ユニット区分</param>
        ''' <param name="matrix">部品表</param>
        ''' <remarks></remarks>
        Public Sub New(ByVal yosanEventCode As String, ByVal unitKbn As String, ByVal matrix As BuhinKoseiMatrix)
            Me._yosanEventCode = yosanEventCode
            Me._unitKbn = unitKbn
            Me._matrix = matrix
        End Sub

        ''' <summary>
        ''' 登録用のデータを作成する
        ''' </summary>
        ''' <returns>登録用のデータ</returns>
        ''' <remarks></remarks>
        Public Function MakeValues() As List(Of TYosanBuhinEditVo)
            Dim results As New List(Of TYosanBuhinEditVo)
            Dim BuhinNoHyoujiJun As Integer = 0
            Dim flag As Boolean = False

            'このあたりで登録時に員数の無い行を飛ばさせる必要がある'
            For Each rowIndex As Integer In _matrix.GetInputRowIndexes()
                Dim insuCount As Integer = 0
                For Each colIndex As Integer In _matrix.GetInputInsuColumnIndexes
                    '員数の存在チェック'
                    If Not _matrix.InsuSuryo(rowIndex, colIndex) Is Nothing Then
                        flag = True

                        'NULLだと手配帳で問題が発生するので'
                        If _matrix.Record(rowIndex).YosanBukaCode Is Nothing Then
                            _matrix.Record(rowIndex).YosanBukaCode = ""
                        End If
                        If _matrix.Record(rowIndex).YosanBlockNo Is Nothing Then
                            _matrix.Record(rowIndex).YosanBlockNo = ""
                        End If
                        If _matrix.Record(rowIndex).BuhinNoHyoujiJun Is Nothing Then
                            _matrix.Record(rowIndex).BuhinNoHyoujiJun = BuhinNoHyoujiJun
                        End If
                        If _matrix.Record(rowIndex).YosanLevel Is Nothing Then
                            _matrix.Record(rowIndex).YosanLevel = Nothing
                        End If
                        If _matrix.Record(rowIndex).YosanShukeiCode Is Nothing Then
                            _matrix.Record(rowIndex).YosanShukeiCode = ""
                        End If
                        If _matrix.Record(rowIndex).YosanSiaShukeiCode Is Nothing Then
                            _matrix.Record(rowIndex).YosanSiaShukeiCode = ""
                        End If
                        If _matrix.Record(rowIndex).YosanMakerCode Is Nothing Then
                            _matrix.Record(rowIndex).YosanMakerCode = ""
                        End If
                        If _matrix.Record(rowIndex).YosanMakerName Is Nothing Then
                            _matrix.Record(rowIndex).YosanMakerName = ""
                        End If
                        If _matrix.Record(rowIndex).YosanBuhinNo Is Nothing Then
                            _matrix.Record(rowIndex).YosanBuhinNo = ""
                        End If
                        If _matrix.Record(rowIndex).YosanBuhinNoKbn Is Nothing Then
                            _matrix.Record(rowIndex).YosanBuhinNoKbn = ""
                        End If
                        If _matrix.Record(rowIndex).YosanBuhinName Is Nothing Then
                            _matrix.Record(rowIndex).YosanBuhinName = ""
                        End If
                        If _matrix.Record(rowIndex).YosanKyoukuSection Is Nothing Then
                            _matrix.Record(rowIndex).YosanKyoukuSection = ""
                        End If
                        If _matrix.Record(rowIndex).YosanInsu Is Nothing Then
                            _matrix.Record(rowIndex).YosanInsu = Nothing
                        End If
                        If _matrix.Record(rowIndex).YosanHenkoGaiyo Is Nothing Then
                            _matrix.Record(rowIndex).YosanHenkoGaiyo = ""
                        End If
                        If _matrix.Record(rowIndex).YosanBuhinHiRyosan Is Nothing Then
                            _matrix.Record(rowIndex).YosanBuhinHiRyosan = Nothing
                        End If
                        If _matrix.Record(rowIndex).YosanBuhinHiBuhinhyo Is Nothing Then
                            _matrix.Record(rowIndex).YosanBuhinHiBuhinhyo = Nothing
                        End If
                        If _matrix.Record(rowIndex).YosanBuhinHiTokki Is Nothing Then
                            _matrix.Record(rowIndex).YosanBuhinHiTokki = Nothing
                        End If
                        If _matrix.Record(rowIndex).YosanKataHi Is Nothing Then
                            _matrix.Record(rowIndex).YosanKataHi = Nothing
                        End If
                        If _matrix.Record(rowIndex).YosanJiguHi Is Nothing Then
                            _matrix.Record(rowIndex).YosanJiguHi = Nothing
                        End If
                        If _matrix.Record(rowIndex).YosanKosu Is Nothing Then
                            _matrix.Record(rowIndex).YosanKosu = Nothing
                        End If
                        If _matrix.Record(rowIndex).YosanHachuJisekiMix Is Nothing Then
                            _matrix.Record(rowIndex).YosanHachuJisekiMix = Nothing
                        End If

                        'ある列が存在すれば追加'
                        With _matrix.Record(rowIndex)
                            .YosanEventCode = _yosanEventCode
                            .BuhinhyoName = _unitKbn
                            .BuhinNoHyoujiJun = rowIndex

                            insuCount = insuCount + _matrix.InsuSuryo(rowIndex, colIndex)
                            .YosanInsu = insuCount
                        End With
                        '存在チェックしてから追加'
                        If Not results.Contains(_matrix.Record(rowIndex)) Then
                            results.Add(_matrix.Record(rowIndex))
                        End If
                    End If
                Next
            Next

            '順番を飛ばさない'
            For index As Integer = 0 To results.Count - 1
                results(index).BuhinNoHyoujiJun = index
            Next

            Return results
        End Function

        'テキストチェック用
        Private Function CheckText(ByVal str As String) As Integer

            Static Encode_JIS As Encoding = Encoding.GetEncoding("Shift_JIS")
            Dim Str_Count As Integer = str.Length
            Dim ByteCount = Encode_JIS.GetByteCount(str)

            If Str_Count * 2 = ByteCount Then
                Return vbWide '4   
            ElseIf Str_Count = ByteCount Then
                Return vbNarrow '8   
            Else
                Return -1
            End If

        End Function

        ''' <summary>
        ''' 更新する
        ''' </summary>
        ''' <param name="login">ログイン情報</param>
        ''' <param name="editDao">予算書部品編集Dao</param>
        ''' <param name="editRirekiDao">予算書部品編集履歴Dao</param>
        ''' <param name="aDate">試作日付</param>
        ''' <remarks></remarks>
        Public Sub Update(ByVal login As LoginInfo, ByVal editDao As TYosanBuhinEditDao, _
                          ByVal editRirekiDao As TYosanBuhinEditRirekiDao, ByVal aDate As ShisakuDate)

            Dim param As New TYosanBuhinEditVo
            param.YosanEventCode = _yosanEventCode
            param.BuhinhyoName = _unitKbn
            editDao.DeleteBy(param)

            'Dim params As New TYosanBuhinEditRirekiVo
            'params.YosanEventCode = _yosanEventCode
            'params.UnitKbn = _unitKbn
            'editRirekiDao.DeleteBy(params)

            For Each vo As TYosanBuhinEditVo In MakeValues()
                Dim editVo As New TYosanBuhinEditVo
                editVo.YosanEventCode = _yosanEventCode
                editVo.BuhinhyoName = _unitKbn
                editVo.YosanBukaCode = vo.YosanBukaCode
                editVo.YosanBlockNo = vo.YosanBlockNo
                editVo.BuhinNoHyoujiJun = vo.BuhinNoHyoujiJun
                editVo.YosanLevel = vo.YosanLevel
                editVo.YosanShukeiCode = vo.YosanShukeiCode
                editVo.YosanSiaShukeiCode = vo.YosanSiaShukeiCode
                editVo.YosanMakerCode = vo.YosanMakerCode
                editVo.YosanMakerName = vo.YosanMakerName
                editVo.YosanBuhinNo = vo.YosanBuhinNo
                editVo.YosanBuhinNoKbn = vo.YosanBuhinNoKbn
                editVo.YosanBuhinName = vo.YosanBuhinName
                editVo.YosanKyoukuSection = vo.YosanKyoukuSection
                editVo.YosanInsu = vo.YosanInsu
                editVo.YosanHenkoGaiyo = vo.YosanHenkoGaiyo
                editVo.YosanBuhinHiRyosan = vo.YosanBuhinHiRyosan
                editVo.YosanBuhinHiBuhinhyo = vo.YosanBuhinHiBuhinhyo
                editVo.YosanBuhinHiTokki = vo.YosanBuhinHiTokki
                editVo.YosanKataHi = vo.YosanKataHi
                editVo.YosanJiguHi = vo.YosanJiguHi
                editVo.YosanKosu = vo.YosanKosu
                editVo.YosanHachuJisekiMix = vo.YosanHachuJisekiMix
                If StringUtil.IsEmpty(vo.CreatedUserId) Then
                    With editVo
                        .CreatedUserId = login.UserId
                        .CreatedDate = aDate.CurrentDateDbFormat
                        .CreatedTime = aDate.CurrentTimeDbFormat
                    End With
                Else
                    With editVo
                        .CreatedUserId = vo.CreatedUserId
                        .CreatedDate = vo.CreatedDate
                        .CreatedTime = vo.CreatedTime
                    End With
                End If
                editVo.UpdatedUserId = login.UserId
                editVo.UpdatedDate = aDate.CurrentDateDbFormat
                editVo.UpdatedTime = aDate.CurrentTimeDbFormat
                editDao.InsertBy(editVo)

                'Dim editRirekiVo As New TYosanBuhinEditRirekiVo
                'editRirekiVo.YosanEventCode = _yosanEventCode
                'editRirekiVo.UnitKbn = _unitKbn
                'editRirekiVo.RegisterDate = DateUtil.ConvDateToIneteger(aDate.CurrentDateTime).ToString
                'editRirekiVo.YosanBukaCode = vo.YosanBukaCode
                'editRirekiVo.YosanBlockNo = vo.YosanBlockNo
                'editRirekiVo.BuhinNoHyoujiJun = vo.BuhinNoHyoujiJun
                'editRirekiVo.YosanLevel = vo.YosanLevel
                'editRirekiVo.YosanShukeiCode = vo.YosanShukeiCode
                'editRirekiVo.YosanSiaShukeiCode = vo.YosanSiaShukeiCode
                'editRirekiVo.YosanMakerCode = vo.YosanMakerCode
                'editRirekiVo.YosanMakerName = vo.YosanMakerName
                'editRirekiVo.YosanBuhinNo = vo.YosanBuhinNo
                'editRirekiVo.YosanBuhinNoKbn = vo.YosanBuhinNoKbn
                'editRirekiVo.YosanBuhinName = vo.YosanBuhinName
                'editRirekiVo.YosanKyoukuSection = vo.YosanKyoukuSection
                'editRirekiVo.YosanInsu = vo.YosanInsu
                'editRirekiVo.YosanHenkoGaiyo = vo.YosanHenkoGaiyo
                'editRirekiVo.YosanBuhinHiRyosan = vo.YosanBuhinHiRyosan
                'editRirekiVo.YosanBuhinHiBuhinhyo = vo.YosanBuhinHiBuhinhyo
                'editRirekiVo.YosanBuhinHiTokki = vo.YosanBuhinHiTokki
                'editRirekiVo.YosanKataHi = vo.YosanKataHi
                'editRirekiVo.YosanJiguHi = vo.YosanJiguHi
                'editRirekiVo.YosanKosu = vo.YosanKosu
                'editRirekiVo.YosanHachuJisekiMix = vo.YosanHachuJisekiMix
                'If StringUtil.IsEmpty(vo.CreatedUserId) Then
                '    With editRirekiVo
                '        .CreatedUserId = login.UserId
                '        .CreatedDate = aDate.CurrentDateDbFormat
                '        .CreatedTime = aDate.CurrentTimeDbFormat
                '    End With
                'Else
                '    With editRirekiVo
                '        .CreatedUserId = vo.CreatedUserId
                '        .CreatedDate = vo.CreatedDate
                '        .CreatedTime = vo.CreatedTime
                '    End With
                'End If
                'editRirekiVo.UpdatedUserId = login.UserId
                'editRirekiVo.UpdatedDate = aDate.CurrentDateDbFormat
                'editRirekiVo.UpdatedTime = aDate.CurrentTimeDbFormat
                'editRirekiDao.InsertBy(editRirekiVo)
            Next

        End Sub

    End Class
End Namespace