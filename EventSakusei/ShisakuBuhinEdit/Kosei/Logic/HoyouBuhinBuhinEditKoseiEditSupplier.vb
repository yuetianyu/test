Imports EventSakusei.ShisakuBuhinEdit.Kosei.Logic.Matrix
Imports ShisakuCommon.Db.EBom.Dao
Imports ShisakuCommon
Imports ShisakuCommon.Db.EBom.Vo
Imports System.Text

Namespace ShisakuBuhinEdit.Kosei.Logic
    ''' <summary>
    ''' 補用部品編集情報の作成データを供給するクラス
    ''' </summary>
    ''' <remarks></remarks>
    Public Class HoyouBuhinBuhinEditKoseiEditSupplier
        Private _tantoKeyVo As THoyouSekkeiTantoVo
        Private _matrix As HoyouBuhinBuhinKoseiMatrix

        ''' <summary>
        ''' コンストラクタ
        ''' </summary>
        ''' <param name="tantoKeyVo">補用設計ブ担当情報（キー情報）</param>
        ''' <param name="matrix">部品表</param>
        ''' <remarks></remarks>
        Public Sub New(ByVal tantoKeyVo As THoyouSekkeiTantoVo, ByVal matrix As HoyouBuhinBuhinKoseiMatrix)
            Me._tantoKeyVo = tantoKeyVo
            Me._matrix = matrix
        End Sub

        ''' <summary>
        ''' 登録用のデータを作成する
        ''' </summary>
        ''' <returns>登録用のデータ</returns>
        ''' <remarks></remarks>
        Public Function MakeValues() As List(Of THoyouBuhinEditVo)
            Dim results As New List(Of THoyouBuhinEditVo)
            Dim BuhinNoHyoujiJun As Integer = 0

            'このあたりで登録時に員数の無い行を飛ばさせる必要がある'
            For Each rowIndex As Integer In _matrix.GetInputRowIndexes()

                'NULLだと手配帳で問題が発生するので'
                If _matrix.Record(rowIndex).ShukeiCode Is Nothing Then
                    _matrix.Record(rowIndex).ShukeiCode = ""
                End If
                If _matrix.Record(rowIndex).SiaShukeiCode Is Nothing Then
                    _matrix.Record(rowIndex).SiaShukeiCode = ""
                End If
                If _matrix.Record(rowIndex).GencyoCkdKbn Is Nothing Then
                    _matrix.Record(rowIndex).GencyoCkdKbn = ""
                End If
                If _matrix.Record(rowIndex).MakerCode Is Nothing Then
                    _matrix.Record(rowIndex).MakerCode = ""
                End If
                If _matrix.Record(rowIndex).MakerName Is Nothing Then
                    _matrix.Record(rowIndex).MakerName = ""
                End If
                If _matrix.Record(rowIndex).InsuSuryo Is Nothing Then
                    _matrix.Record(rowIndex).InsuSuryo = 0
                ElseIf _matrix.Record(rowIndex).InsuSuryo = "**" Then
                    _matrix.Record(rowIndex).InsuSuryo = -1
                End If
                If _matrix.Record(rowIndex).KyoukuSection Is Nothing Then
                    _matrix.Record(rowIndex).KyoukuSection = ""
                End If
                If _matrix.Record(rowIndex).Memo1 Is Nothing Then
                    _matrix.Record(rowIndex).Memo1 = ""
                End If
                If _matrix.Record(rowIndex).Memo2 Is Nothing Then
                    _matrix.Record(rowIndex).Memo2 = ""
                End If
                If _matrix.Record(rowIndex).Memo3 Is Nothing Then
                    _matrix.Record(rowIndex).Memo3 = ""
                End If
                If _matrix.Record(rowIndex).Memo4 Is Nothing Then
                    _matrix.Record(rowIndex).Memo4 = ""
                End If
                If _matrix.Record(rowIndex).Memo5 Is Nothing Then
                    _matrix.Record(rowIndex).Memo5 = ""
                End If
                If _matrix.Record(rowIndex).Memo6 Is Nothing Then
                    _matrix.Record(rowIndex).Memo6 = ""
                End If
                If _matrix.Record(rowIndex).Memo7 Is Nothing Then
                    _matrix.Record(rowIndex).Memo7 = ""
                End If
                If _matrix.Record(rowIndex).Memo8 Is Nothing Then
                    _matrix.Record(rowIndex).Memo8 = ""
                End If
                If _matrix.Record(rowIndex).Memo9 Is Nothing Then
                    _matrix.Record(rowIndex).Memo9 = ""
                End If
                If _matrix.Record(rowIndex).Memo10 Is Nothing Then
                    _matrix.Record(rowIndex).Memo10 = ""
                End If
                '
                If _matrix.Record(rowIndex).Memo1T Is Nothing Then
                    _matrix.Record(rowIndex).Memo1T = ""
                End If
                If _matrix.Record(rowIndex).Memo2T Is Nothing Then
                    _matrix.Record(rowIndex).Memo2T = ""
                End If
                If _matrix.Record(rowIndex).Memo3T Is Nothing Then
                    _matrix.Record(rowIndex).Memo3T = ""
                End If
                If _matrix.Record(rowIndex).Memo4T Is Nothing Then
                    _matrix.Record(rowIndex).Memo4T = ""
                End If
                If _matrix.Record(rowIndex).Memo5T Is Nothing Then
                    _matrix.Record(rowIndex).Memo5T = ""
                End If
                If _matrix.Record(rowIndex).Memo6T Is Nothing Then
                    _matrix.Record(rowIndex).Memo6T = ""
                End If
                If _matrix.Record(rowIndex).Memo7T Is Nothing Then
                    _matrix.Record(rowIndex).Memo7T = ""
                End If
                If _matrix.Record(rowIndex).Memo8T Is Nothing Then
                    _matrix.Record(rowIndex).Memo8T = ""
                End If
                If _matrix.Record(rowIndex).Memo9T Is Nothing Then
                    _matrix.Record(rowIndex).Memo9T = ""
                End If
                If _matrix.Record(rowIndex).Memo10T Is Nothing Then
                    _matrix.Record(rowIndex).Memo10T = ""
                End If
                '
                If _matrix.Record(rowIndex).BuhinNote Is Nothing Then
                    _matrix.Record(rowIndex).BuhinNote = ""
                End If

                'ある列が存在すれば追加'
                With _matrix.Record(rowIndex)
                    .HoyouEventCode = _tantoKeyVo.HoyouEventCode
                    .HoyouBukaCode = _tantoKeyVo.HoyouBukaCode
                    .HoyouTanto = _tantoKeyVo.HoyouTanto
                    .HoyouTantoKaiteiNo = _tantoKeyVo.HoyouTantoKaiteiNo
                    .BuhinNoHyoujiJun = rowIndex

                    If .BuhinNoHyoujiJunZenkai Is Nothing Then
                        .BuhinNoHyoujiJunZenkai = -1
                    End If

                End With
                '存在チェックしてから追加'
                If Not results.Contains(_matrix.Record(rowIndex)) Then
                    results.Add(_matrix.Record(rowIndex))
                End If
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
        ''' <param name="editDao">補用部品編集Dao</param>
        ''' <param name="aDate">試作日付</param>
        ''' <remarks></remarks>
        Public Sub Update(ByVal login As LoginInfo, ByVal editDao As THoyouBuhinEditDao, ByVal aDate As ShisakuDate)

            Dim param As New THoyouBuhinEditVo
            param.HoyouEventCode = _tantoKeyVo.HoyouEventCode
            param.HoyouBukaCode = _tantoKeyVo.HoyouBukaCode
            param.HoyouTanto = _tantoKeyVo.HoyouTanto
            param.HoyouTantoKaiteiNo = _tantoKeyVo.HoyouTantoKaiteiNo
            editDao.DeleteBy(param)

            For Each vo As THoyouBuhinEditVo In MakeValues()

                Dim editVo As New THoyouBuhinEditVo
                editVo.HoyouEventCode = vo.HoyouEventCode
                editVo.HoyouBukaCode = vo.HoyouBukaCode
                editVo.HoyouTanto = vo.HoyouTanto
                editVo.HoyouTantoKaiteiNo = vo.HoyouTantoKaiteiNo
                editVo.BuhinNoHyoujiJun = vo.BuhinNoHyoujiJun
                editVo.BuhinNoHyoujiJunZenkai = vo.BuhinNoHyoujiJunZenkai
                editVo.Level = vo.Level
                editVo.KyoukuSection = vo.KyoukuSection
                editVo.ShukeiCode = vo.ShukeiCode
                editVo.SiaShukeiCode = vo.SiaShukeiCode
                editVo.MakerCode = vo.MakerCode
                editVo.MakerName = vo.MakerName
                editVo.BuhinNo = vo.BuhinNo
                editVo.BuhinName = vo.BuhinName
                editVo.InsuSuryo = vo.InsuSuryo
                editVo.GencyoCkdKbn = vo.GencyoCkdKbn
                editVo.BuhinNote = vo.BuhinNote

                'メモ欄
                editVo.Memo1 = vo.Memo1
                editVo.Memo2 = vo.Memo2
                editVo.Memo3 = vo.Memo3
                editVo.Memo4 = vo.Memo4
                editVo.Memo5 = vo.Memo5
                editVo.Memo6 = vo.Memo6
                editVo.Memo7 = vo.Memo7
                editVo.Memo8 = vo.Memo8
                editVo.Memo9 = vo.Memo9
                editVo.Memo10 = vo.Memo10
                '   バイト数をチェックする。
                If StringUtil.IsNotEmpty(vo.Memo1) Then
                    Dim ret As Integer = CheckText(vo.Memo1)
                    If ret = 4 Or ret = -1 Then
                        editVo.Memo1 = Left(vo.Memo1, 1)
                    End If
                End If
                If StringUtil.IsNotEmpty(vo.Memo2) Then
                    Dim ret As Integer = CheckText(vo.Memo2)
                    If ret = 4 Or ret = -1 Then
                        editVo.Memo2 = Left(vo.Memo2, 1)
                    End If
                End If
                If StringUtil.IsNotEmpty(vo.Memo3) Then
                    Dim ret As Integer = CheckText(vo.Memo3)
                    If ret = 4 Or ret = -1 Then
                        editVo.Memo3 = Left(vo.Memo3, 1)
                    End If
                End If
                If StringUtil.IsNotEmpty(vo.Memo4) Then
                    Dim ret As Integer = CheckText(vo.Memo4)
                    If ret = 4 Or ret = -1 Then
                        editVo.Memo4 = Left(vo.Memo4, 1)
                    End If
                End If
                If StringUtil.IsNotEmpty(vo.Memo5) Then
                    Dim ret As Integer = CheckText(vo.Memo5)
                    If ret = 4 Or ret = -1 Then
                        editVo.Memo5 = Left(vo.Memo5, 1)
                    End If
                End If
                If StringUtil.IsNotEmpty(vo.Memo6) Then
                    Dim ret As Integer = CheckText(vo.Memo6)
                    If ret = 4 Or ret = -1 Then
                        editVo.Memo6 = Left(vo.Memo6, 1)
                    End If
                End If
                If StringUtil.IsNotEmpty(vo.Memo7) Then
                    Dim ret As Integer = CheckText(vo.Memo7)
                    If ret = 4 Or ret = -1 Then
                        editVo.Memo7 = Left(vo.Memo7, 1)
                    End If
                End If
                If StringUtil.IsNotEmpty(vo.Memo8) Then
                    Dim ret As Integer = CheckText(vo.Memo8)
                    If ret = 4 Or ret = -1 Then
                        editVo.Memo8 = Left(vo.Memo8, 1)
                    End If
                End If
                If StringUtil.IsNotEmpty(vo.Memo9) Then
                    Dim ret As Integer = CheckText(vo.Memo9)
                    If ret = 4 Or ret = -1 Then
                        editVo.Memo9 = Left(vo.Memo9, 1)
                    End If
                End If
                If StringUtil.IsNotEmpty(vo.Memo10) Then
                    Dim ret As Integer = CheckText(vo.Memo10)
                    If ret = 4 Or ret = -1 Then
                        editVo.Memo10 = Left(vo.Memo10, 1)
                    End If
                End If
                'メモタイトル欄
                editVo.Memo1T = vo.Memo1T
                editVo.Memo2T = vo.Memo2T
                editVo.Memo3T = vo.Memo3T
                editVo.Memo4T = vo.Memo4T
                editVo.Memo5T = vo.Memo5T
                editVo.Memo6T = vo.Memo6T
                editVo.Memo7T = vo.Memo7T
                editVo.Memo8T = vo.Memo8T
                editVo.Memo9T = vo.Memo9T
                editVo.Memo10T = vo.Memo10T
                '   バイト数をチェックする。
                If StringUtil.IsNotEmpty(vo.Memo1T) Then
                    Dim ret As Integer = CheckText(vo.Memo1T)
                    If ret >= 122 Or ret = -1 Then
                        editVo.Memo1T = Left(vo.Memo1T, 61)
                    End If
                End If
                If StringUtil.IsNotEmpty(vo.Memo2T) Then
                    Dim ret As Integer = CheckText(vo.Memo2T)
                    If ret >= 122 Or ret = -1 Then
                        editVo.Memo2T = Left(vo.Memo2T, 61)
                    End If
                End If
                If StringUtil.IsNotEmpty(vo.Memo3T) Then
                    Dim ret As Integer = CheckText(vo.Memo3T)
                    If ret >= 122 Or ret = -1 Then
                        editVo.Memo3T = Left(vo.Memo3T, 61)
                    End If
                End If
                If StringUtil.IsNotEmpty(vo.Memo4T) Then
                    Dim ret As Integer = CheckText(vo.Memo4T)
                    If ret >= 122 Or ret = -1 Then
                        editVo.Memo4T = Left(vo.Memo4T, 61)
                    End If
                End If
                If StringUtil.IsNotEmpty(vo.Memo5T) Then
                    Dim ret As Integer = CheckText(vo.Memo5T)
                    If ret >= 122 Or ret = -1 Then
                        editVo.Memo5T = Left(vo.Memo5T, 61)
                    End If
                End If
                If StringUtil.IsNotEmpty(vo.Memo6T) Then
                    Dim ret As Integer = CheckText(vo.Memo6T)
                    If ret >= 122 Or ret = -1 Then
                        editVo.Memo6T = Left(vo.Memo6T, 61)
                    End If
                End If
                If StringUtil.IsNotEmpty(vo.Memo7T) Then
                    Dim ret As Integer = CheckText(vo.Memo7T)
                    If ret >= 122 Or ret = -1 Then
                        editVo.Memo7T = Left(vo.Memo7T, 61)
                    End If
                End If
                If StringUtil.IsNotEmpty(vo.Memo8T) Then
                    Dim ret As Integer = CheckText(vo.Memo8T)
                    If ret >= 122 Or ret = -1 Then
                        editVo.Memo8T = Left(vo.Memo8T, 61)
                    End If
                End If
                If StringUtil.IsNotEmpty(vo.Memo9T) Then
                    Dim ret As Integer = CheckText(vo.Memo9T)
                    If ret >= 122 Or ret = -1 Then
                        editVo.Memo9T = Left(vo.Memo9T, 61)
                    End If
                End If
                If StringUtil.IsNotEmpty(vo.Memo10T) Then
                    Dim ret As Integer = CheckText(vo.Memo10T)
                    If ret >= 122 Or ret = -1 Then
                        editVo.Memo10T = Left(vo.Memo10T, 61)
                    End If
                End If

                '固定メモ欄
                editVo.MemoSheet1 = vo.MemoSheet1
                editVo.MemoSheet2 = vo.MemoSheet2
                editVo.MemoSheet3 = vo.MemoSheet3
                editVo.MemoSheet4 = vo.MemoSheet4
                editVo.MemoSheet5 = vo.MemoSheet5
                editVo.MemoSheet6 = vo.MemoSheet6
                editVo.MemoSheet7 = vo.MemoSheet7
                editVo.MemoSheet8 = vo.MemoSheet8
                editVo.MemoDoorTrim1 = vo.MemoDoorTrim1
                editVo.MemoDoorTrim2 = vo.MemoDoorTrim2
                editVo.MemoRoofTrim1 = vo.MemoRoofTrim1
                editVo.MemoRoofTrim2 = vo.MemoRoofTrim2
                editVo.MemoSunroofTrim1 = vo.MemoSunroofTrim1
                '   バイト数をチェックする。
                If StringUtil.IsNotEmpty(vo.MemoSheet1) Then
                    Dim ret As Integer = CheckText(vo.MemoSheet1)
                    If ret = 4 Or ret = -1 Then
                        editVo.MemoSheet1 = Left(vo.MemoSheet1, 1)
                    End If
                End If
                If StringUtil.IsNotEmpty(vo.MemoSheet2) Then
                    Dim ret As Integer = CheckText(vo.MemoSheet2)
                    If ret = 4 Or ret = -1 Then
                        editVo.MemoSheet2 = Left(vo.MemoSheet2, 1)
                    End If
                End If
                If StringUtil.IsNotEmpty(vo.MemoSheet3) Then
                    Dim ret As Integer = CheckText(vo.MemoSheet3)
                    If ret = 4 Or ret = -1 Then
                        editVo.MemoSheet3 = Left(vo.MemoSheet3, 1)
                    End If
                End If
                If StringUtil.IsNotEmpty(vo.MemoSheet4) Then
                    Dim ret As Integer = CheckText(vo.MemoSheet4)
                    If ret = 4 Or ret = -1 Then
                        editVo.MemoSheet4 = Left(vo.MemoSheet4, 1)
                    End If
                End If
                If StringUtil.IsNotEmpty(vo.MemoSheet5) Then
                    Dim ret As Integer = CheckText(vo.MemoSheet5)
                    If ret = 4 Or ret = -1 Then
                        editVo.MemoSheet5 = Left(vo.MemoSheet5, 1)
                    End If
                End If
                If StringUtil.IsNotEmpty(vo.MemoSheet6) Then
                    Dim ret As Integer = CheckText(vo.MemoSheet6)
                    If ret = 4 Or ret = -1 Then
                        editVo.MemoSheet6 = Left(vo.MemoSheet6, 1)
                    End If
                End If
                If StringUtil.IsNotEmpty(vo.MemoSheet7) Then
                    Dim ret As Integer = CheckText(vo.MemoSheet7)
                    If ret = 4 Or ret = -1 Then
                        editVo.MemoSheet7 = Left(vo.MemoSheet7, 1)
                    End If
                End If
                If StringUtil.IsNotEmpty(vo.MemoSheet8) Then
                    Dim ret As Integer = CheckText(vo.MemoSheet8)
                    If ret = 4 Or ret = -1 Then
                        editVo.MemoSheet8 = Left(vo.MemoSheet8, 1)
                    End If
                End If
                If StringUtil.IsNotEmpty(vo.MemoDoorTrim1) Then
                    Dim ret As Integer = CheckText(vo.MemoDoorTrim1)
                    If ret = 4 Or ret = -1 Then
                        editVo.MemoDoorTrim1 = Left(vo.MemoDoorTrim1, 1)
                    End If
                End If
                If StringUtil.IsNotEmpty(vo.MemoDoorTrim2) Then
                    Dim ret As Integer = CheckText(vo.MemoDoorTrim2)
                    If ret = 4 Or ret = -1 Then
                        editVo.MemoDoorTrim2 = Left(vo.MemoDoorTrim2, 1)
                    End If
                End If
                If StringUtil.IsNotEmpty(vo.MemoRoofTrim1) Then
                    Dim ret As Integer = CheckText(vo.MemoRoofTrim1)
                    If ret = 4 Or ret = -1 Then
                        editVo.MemoRoofTrim1 = Left(vo.MemoRoofTrim1, 1)
                    End If
                End If
                If StringUtil.IsNotEmpty(vo.MemoRoofTrim2) Then
                    Dim ret As Integer = CheckText(vo.MemoRoofTrim2)
                    If ret = 4 Or ret = -1 Then
                        editVo.MemoRoofTrim2 = Left(vo.MemoRoofTrim2, 1)
                    End If
                End If
                If StringUtil.IsNotEmpty(vo.MemoSunroofTrim1) Then
                    Dim ret As Integer = CheckText(vo.MemoSunroofTrim1)
                    If ret = 4 Or ret = -1 Then
                        editVo.MemoSunroofTrim1 = Left(vo.MemoSunroofTrim1, 1)
                    End If
                End If

                '
                editVo.Bikou = vo.Bikou
                editVo.KaiteiHandanFlg = vo.KaiteiHandanFlg
                editVo.HoyouListCode = vo.HoyouListCode
                editVo.InputFlg = vo.InputFlg
                If StringUtil.IsEmpty(editVo.CreatedUserId) Then
                    With editVo
                        .CreatedUserId = login.UserId
                        .CreatedDate = aDate.CurrentDateDbFormat
                        .CreatedTime = aDate.CurrentTimeDbFormat
                    End With
                End If
                editVo.EditTourokubi = DateUtil.ConvDateToIneteger(aDate.CurrentDateTime)
                editVo.EditTourokujikan = DateUtil.ConvTimeToIneteger(aDate.CurrentDateTime)
                editVo.UpdatedUserId = login.UserId
                editVo.UpdatedDate = aDate.CurrentDateDbFormat
                editVo.UpdatedTime = aDate.CurrentTimeDbFormat
                editDao.InsertBy(editVo)
            Next
        End Sub
    End Class
End Namespace