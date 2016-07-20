Imports EventSakusei.ShisakuBuhinEdit.Kosei.Logic.Matrix
Imports ShisakuCommon.Db.EBom.Vo.Helper
Imports ShisakuCommon.Db.EBom.Dao
Imports ShisakuCommon
Imports ShisakuCommon.Db.EBom.Vo
Imports ShisakuCommon.Db.EBom.Dao.Impl

Namespace ShisakuBuhinEdit.Kosei.Logic
    ''' <summary>
    ''' 試作部品編集INSTL情報の作成データを供給するクラス
    ''' </summary>
    ''' <remarks></remarks>
    Public Class BuhinEditKoseiEditInstlSupplier
        Private _blockKeyVo As TShisakuSekkeiBlockVo
        Private _matrix As BuhinKoseiMatrix

        ''' <summary>
        ''' コンストラクタ
        ''' </summary>
        ''' <param name="blockKeyVo">試作設計ブロック情報（キー情報）</param>
        ''' <param name="matrix">部品表</param>
        ''' <remarks></remarks>
        Public Sub New(ByVal blockKeyVo As TShisakuSekkeiBlockVo, ByVal matrix As BuhinKoseiMatrix)
            Me._blockKeyVo = blockKeyVo
            Me._matrix = matrix
        End Sub

        ''' <summary>
        ''' 登録用のデータを作成する
        ''' </summary>
        ''' <returns>登録用のデータ</returns>
        ''' <remarks></remarks>
        Public Function MakeValues() As List(Of TShisakuBuhinEditInstlVo)
            Dim results As New List(Of TShisakuBuhinEditInstlVo)
            Dim wHyoujiJun As Integer = 0

            For Each rowIndex As Integer In _matrix.GetInputRowIndexes()
                Dim wColumnFlg As String = Nothing

                For Each columnIndex As Integer In _matrix.GetInputInsuColumnIndexesOnRow(rowIndex)
                    If _matrix.InsuVo(rowIndex, columnIndex).InsuSuryo Is Nothing Then
                        Continue For
                    End If

                    With _matrix.InsuVo(rowIndex, columnIndex)

                        'デグレを生む可能性あるので下記処理のコーディング場所を再検討。

                        '柳沼　OR条件を追加（INSTL情報が無い場合登録用のデータを作成しない）

                        '柳沼　試作ブロックINSTL情報からINSTL情報があるかチェックする。
                        Dim instlDao As TShisakuSekkeiBlockInstlDao = New TShisakuSekkeiBlockInstlDaoImpl
                        Dim param As New TShisakuSekkeiBlockInstlVo
                        param.ShisakuEventCode = _blockKeyVo.ShisakuEventCode
                        param.ShisakuBukaCode = _blockKeyVo.ShisakuBukaCode
                        param.ShisakuBlockNo = _blockKeyVo.ShisakuBlockNo
                        param.ShisakuBlockNoKaiteiNo = _blockKeyVo.ShisakuBlockNoKaiteiNo
                        param.InstlHinbanHyoujiJun = columnIndex

                        If 0 = instlDao.CountBy(param) Then
                            Continue For
                        End If

                        .ShisakuEventCode = _blockKeyVo.ShisakuEventCode
                        .ShisakuBukaCode = _blockKeyVo.ShisakuBukaCode
                        .ShisakuBlockNo = _blockKeyVo.ShisakuBlockNo
                        .ShisakuBlockNoKaiteiNo = _blockKeyVo.ShisakuBlockNoKaiteiNo

                        '部品番号表示順を１から採番
                        '.BuhinNoHyoujiJun = rowIndex
                        .BuhinNoHyoujiJun = wHyoujiJun

                        .InstlHinbanHyoujiJun = columnIndex

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
        ''' <param name="editInstlDao">試作部品編集INSTL Dao</param>
        ''' <param name="aDate">試作日付</param>
        ''' <remarks></remarks>
        Public Sub Update(ByVal login As LoginInfo, ByVal editInstlDao As TShisakuBuhinEditInstlDao, ByVal aDate As ShisakuDate)

            Dim param As New TShisakuBuhinEditInstlVo
            param.ShisakuEventCode = _blockKeyVo.ShisakuEventCode
            param.ShisakuBukaCode = _blockKeyVo.ShisakuBukaCode
            param.ShisakuBlockNo = _blockKeyVo.ShisakuBlockNo
            param.ShisakuBlockNoKaiteiNo = _blockKeyVo.ShisakuBlockNoKaiteiNo
            editInstlDao.DeleteBy(param)

            For Each vo As TShisakuBuhinEditInstlVo In MakeValues()
                If StringUtil.IsEmpty(vo.CreatedUserId) Then
                    With vo
                        .CreatedUserId = login.UserId
                        .CreatedDate = aDate.CurrentDateDbFormat
                        .CreatedTime = aDate.CurrentTimeDbFormat
                    End With
                End If
                vo.SaisyuKoushinbi = DateUtil.ConvDateToIneteger(aDate.CurrentDateTime)
                vo.UpdatedUserId = login.UserId
                vo.UpdatedDate = aDate.CurrentDateDbFormat
                vo.UpdatedTime = aDate.CurrentTimeDbFormat
                editInstlDao.InsertBy(vo)
            Next
        End Sub

    End Class
End Namespace