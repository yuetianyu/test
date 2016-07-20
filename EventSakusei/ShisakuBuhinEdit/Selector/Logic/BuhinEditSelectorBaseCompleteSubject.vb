Imports ShisakuCommon
Imports ShisakuCommon.Db.EBom.Vo
Imports EventSakusei.Soubi
Imports ShisakuCommon.Util

Namespace ShisakuBuhinEdit.Selector.Logic
    Public Class BuhinEditSelectorBaseCompleteSubject : Inherits Observable

        Private ReadOnly checkedValues As List(Of TShisakuSekkeiBlockSoubiVo)
        Private checkingValues As List(Of TShisakuSekkeiBlockSoubiVo)

        Private optionVos As List(Of TShisakuEventSoubiNameVo)

        Public Sub New(ByVal checkedValues As List(Of TShisakuSekkeiBlockSoubiVo))
            If checkedValues Is Nothing Then
                Me.checkedValues = New List(Of TShisakuSekkeiBlockSoubiVo)
            Else
                Me.checkedValues = checkedValues
            End If

            setChanged()
        End Sub

        ' 一覧の値
        Private _listValues As String()

        ''' <summary>一覧の件数</summary>
        ''' <returns>一覧の件数</returns>
        Public ReadOnly Property ListCount() As Integer
            Get
                Return _listValues.Length
            End Get
        End Property

        ''' <summary>一覧の値</summary>
        ''' <value>一覧の値</value>
        ''' <returns>一覧の値</returns>
        Public Property ListValues() As String()
            Get
                Return _listValues
            End Get
            Set(ByVal value As String())
                _listValues = value

                ' チェック済みの内容 soubiShiyouDao
                Dim map As New Dictionary(Of String, TShisakuSekkeiBlockSoubiVo)
                For Each vo As TShisakuSekkeiBlockSoubiVo In checkedValues
                    map.Add(vo.ShisakuSoubiHyoujiJun, vo)
                Next
                checkingValues = New List(Of TShisakuSekkeiBlockSoubiVo)
                For Each val As String In _listValues
                    If map.ContainsKey(val) Then
                        Dim newVo As New TShisakuSekkeiBlockSoubiVo
                        VoUtil.CopyProperties(map(val), newVo)
                        checkingValues.Add(newVo)
                    Else
                        checkingValues.Add(New TShisakuSekkeiBlockSoubiVo)
                    End If
                Next

            End Set
        End Property

        Public Property ListCheck(ByVal index As Integer) As Boolean
            Get
                Return IsChecking(checkingValues(index))
            End Get
            Set(ByVal value As Boolean)
                If value Then
                    If Not StringUtil.IsEmpty(checkingValues(index).ShisakuSoubiHyoujiJun) Then
                        Return
                    End If
                    checkingValues(index).ShisakuSoubiHyoujiJun = _listValues(index)
                Else
                    If StringUtil.IsEmpty(checkingValues(index).ShisakuSoubiHyoujiJun) Then
                        Return
                    End If
                    checkingValues(index).ShisakuSoubiHyoujiJun = Nothing
                End If
                setChanged()
            End Set
        End Property

        Private Function IsChecking(ByVal value As TShisakuSekkeiBlockSoubiVo) As Boolean

            Return Not StringUtil.IsEmpty(value.ShisakuSoubiHyoujiJun)
        End Function

        Public Function GetCheckedValues() As List(Of TShisakuSekkeiBlockSoubiVo)
            Dim results As New List(Of TShisakuSekkeiBlockSoubiVo)
            Dim ShisakuSoubiHyoujiJun As Integer = 0
            For Each vo As TShisakuSekkeiBlockSoubiVo In checkingValues
                If Not IsChecking(vo) Then
                    Continue For
                End If
                results.Add(vo)
            Next
            Return results
        End Function
    End Class
End Namespace