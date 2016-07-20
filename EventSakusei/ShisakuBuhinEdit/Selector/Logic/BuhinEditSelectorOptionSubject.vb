Imports ShisakuCommon
Imports ShisakuCommon.Util
Imports EventSakusei.Soubi
Imports ShisakuCommon.Db.EBom.Vo

Namespace ShisakuBuhinEdit.Selector.Logic


    Public Class BuhinEditSelectorOptionSubject : Inherits Observable
        Private ReadOnly shisakuEventCode As String
        Private ReadOnly shisakuSoubiKbn As String
        Private ReadOnly anEventSoubiDao As EventSoubiDao
        Private ReadOnly checkedValues As List(Of TShisakuSekkeiBlockSoubiShiyouVo)
        Private checkingValues As List(Of TShisakuSekkeiBlockSoubiShiyouVo)

        Private strCompWb As String

        Private optionVos As List(Of TShisakuEventSoubiNameVo)

        Public Sub New(ByVal shisakuEventCode As String, _
                       ByVal shisakuSoubiKbn As String, _
                       ByVal anEventSoubiDao As EventSoubiDao, _
                       ByVal checkedValues As List(Of TShisakuSekkeiBlockSoubiShiyouVo), _
                       ByVal strCompWb As String)
            Me.shisakuEventCode = shisakuEventCode
            Me.shisakuSoubiKbn = shisakuSoubiKbn
            Me.anEventSoubiDao = anEventSoubiDao
            Me.checkedValues = checkedValues

            Me.strCompWb = strCompWb

            ' 表示の内容 eventSoubiDao
            '   WB用に装備区分を変更
            Dim strSoubiKbn As String = ""
            If StringUtil.Equals(strCompWb, "W") Then
                strSoubiKbn = "3"
            Else
                strSoubiKbn = shisakuSoubiKbn
            End If
            optionVos = anEventSoubiDao.FindWithTitleNameBySoubi(shisakuEventCode, strSoubiKbn)

            ' チェック済みの内容 soubiShiyouDao
            Dim map As New Dictionary(Of String, TShisakuSekkeiBlockSoubiShiyouVo)
            For Each vo As TShisakuSekkeiBlockSoubiShiyouVo In checkedValues
                If Not shisakuSoubiKbn.Equals(vo.ShisakuSoubiKbn)  Then
                    Continue For
                End If
                map.Add(vo.ShisakuRetuKoumokuCode, vo)
            Next
            checkingValues = New List(Of TShisakuSekkeiBlockSoubiShiyouVo)
            For Each vo As TShisakuEventSoubiNameVo In optionVos

                Dim flgCompWb As String = ""

                '完成車かＷＢ車か判断する。
                If StringUtil.Equals(Left(vo.ShisakuRetuKoumokuNameDai, 1), strCompWb) And _
                    Left(vo.ShisakuRetuKoumokuNameDai, 1) = "W" Then
                    flgCompWb = "WB"
                ElseIf Not StringUtil.Equals(Left(vo.ShisakuRetuKoumokuNameDai, 1), "W") And _
                    StringUtil.Equals(strCompWb, "C") Then
                    flgCompWb = "COMP"

                ElseIf StringUtil.Equals(strCompWb, "B") Then
                    flgCompWb = "BASE"
                End If

                'フラグに値が有れば実行する。
                If StringUtil.IsNotEmpty(flgCompWb) Then
                    If map.ContainsKey(vo.ShisakuRetuKoumokuCode) Then
                        checkingValues.Add(map(vo.ShisakuRetuKoumokuCode))
                    Else
                        checkingValues.Add(New TShisakuSekkeiBlockSoubiShiyouVo)
                    End If
                End If

            Next

            SetChanged()
        End Sub

        Public ReadOnly Property OptionCount() As Integer
            Get
                Return optionVos.Count
            End Get
        End Property

        Public ReadOnly Property OptionName(ByVal index As Integer) As String
            Get
                Return optionVos(index).ShisakuRetuKoumokuName
            End Get
        End Property
        Public ReadOnly Property OptionNameDai(ByVal index As Integer) As String
            Get
                Return optionVos(index).ShisakuRetuKoumokuNameDai
            End Get
        End Property
        Public ReadOnly Property OptionNameChu(ByVal index As Integer) As String
            Get
                Return optionVos(index).ShisakuRetuKoumokuNameChu
            End Get
        End Property

        Public Property OptionCheck(ByVal index As Integer) As Boolean
            Get
                Return IsChecking(checkingValues(index))
            End Get
            Set(ByVal value As Boolean)
                If value Then
                    If Not StringUtil.IsEmpty(checkingValues(index).ShisakuSoubiKbn) Then
                        Return
                    End If
                    checkingValues(index).ShisakuSoubiKbn = shisakuSoubiKbn
                    checkingValues(index).ShisakuRetuKoumokuCode = optionVos(index).ShisakuRetuKoumokuCode
                Else
                    If StringUtil.IsEmpty(checkingValues(index).ShisakuSoubiKbn) Then
                        Return
                    End If
                    checkingValues(index).ShisakuSoubiKbn = Nothing
                    checkingValues(index).ShisakuRetuKoumokuCode = Nothing
                End If
                setChanged()
            End Set
        End Property

        Private Function IsChecking(ByVal value As TShisakuSekkeiBlockSoubiShiyouVo) As Boolean

            Return Not StringUtil.IsEmpty(value.ShisakuRetuKoumokuCode)
        End Function

        Public Function GetCheckedValues(Optional ByVal ShisakuSoubiHyoujiJun As Integer = 0) As List(Of TShisakuSekkeiBlockSoubiShiyouVo)
            Dim results As New List(Of TShisakuSekkeiBlockSoubiShiyouVo)
            For Each vo As TShisakuSekkeiBlockSoubiShiyouVo In checkingValues
                If Not IsChecking(vo) Then
                    Continue For
                End If
                vo.ShisakuSoubiHyoujiJun = EzUtil.Increment(ShisakuSoubiHyoujiJun)
                results.Add(vo)
            Next
            Return results
        End Function

    End Class
End Namespace