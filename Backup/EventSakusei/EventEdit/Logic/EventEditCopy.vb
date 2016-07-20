Imports EventSakusei.EventEdit.Dao
Imports ShisakuCommon
Imports ShisakuCommon.Db.EBom.Vo.Helper
Imports ShisakuCommon.Db.EBom.Dao
Imports ShisakuCommon.Util

Namespace EventEdit.Logic
    Public Class EventEditCopy : Inherits Observable
        Private ReadOnly copyDao As EventEditCopyDao
        Public Sub New(ByVal copyDao As EventEditCopyDao)
            Me.copyDao = copyDao
        End Sub

        ' イベントコード
        Private _EventCode As String

        ''' <summary>イベントコード</summary>
        ''' <value>イベントコード</value>
        ''' <returns>イベントコード</returns>
        Public Property EventCode() As String
            Get
                Return _EventCode
            End Get
            Set(ByVal value As String)
                _EventCode = value
            End Set
        End Property

        Public ReadOnly Property RecordSize() As Integer
            Get
                Return GetCopyResultVos.Count
            End Get
        End Property

        Private ReadOnly Property Records(ByVal row As Integer) As EventEditCopyResultVo
            Get
                Return GetCopyResultVos(row)
            End Get
        End Property

        Public ReadOnly Property EventCode(ByVal row As Integer) As String
            Get
                Return Records(row).ShisakuEventCode
            End Get
        End Property
        Public ReadOnly Property KaihatsuFugo(ByVal row As Integer) As String
            Get
                Return Records(row).ShisakuKaihatsuFugo
            End Get
        End Property
        Public ReadOnly Property EventPhaseName(ByVal row As Integer) As String
            Get
                Return Records(row).ShisakuEventPhaseName
            End Get
        End Property
        Public ReadOnly Property UnitKbn(ByVal row As Integer) As String
            Get
                Return Records(row).UnitKbn
            End Get
        End Property
        Public ReadOnly Property EventName(ByVal row As Integer) As String
            Get
                Return Records(row).ShisakuEventName
            End Get
        End Property
        Public ReadOnly Property Daisu(ByVal row As Integer) As String
            Get
                Return String.Format("{0}+{1}", Records(row).SeisakudaisuKanseisya, Records(row).SeisakudaisuWb)
            End Get
        End Property
        Public ReadOnly Property HachuUmuName(ByVal row As Integer) As String
            Get
                Dim helper As New TShisakuEventVoHelper(Records(row))
                Return helper.GetHachuUmuName
            End Get
        End Property
        Public ReadOnly Property SekkeiTenkaiBi(ByVal row As Integer) As String
            Get
                Return DateUtil.ConvYyyymmddToSlashYyyymmdd(Records(row).SekkeiTenkaibi)
            End Get
        End Property
        Public ReadOnly Property TeiseiShochiShimekiriBi(ByVal row As Integer) As String
            Get
                Return DateUtil.ConvYyyymmddToSlashYyyymmdd(Records(row).Shimekiribi)
            End Get
        End Property
        Public ReadOnly Property StatusName(ByVal row As Integer) As String
            Get
                Return Records(row).ShisakuStatusName
            End Get
        End Property


        Private _copyResultVos As List(Of EventEditCopyResultVo)
        Private Sub ClearCopyResultVos()
            _copyResultVos = Nothing
        End Sub

        Private Function GetCopyResultVos() As List(Of EventEditCopyResultVo)
            If _copyResultVos Is Nothing Then
                _copyResultVos = copyDao.FindByAll()
                setChanged()
            End If
            Return _copyResultVos
        End Function

    End Class
End Namespace