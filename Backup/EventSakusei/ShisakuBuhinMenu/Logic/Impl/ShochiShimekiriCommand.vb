Imports ShisakuCommon.Db
Imports ShisakuCommon
Imports ShisakuCommon.Db.EBom.Vo.Helper
Imports ShisakuCommon.Db.EBom.Dao
Imports ShisakuCommon.Db.EBom.Vo

Namespace ShisakuBuhinMenu.Logic.Impl
    Public Class ShochiShimekiriCommand : Implements Command
        Private ReadOnly shisakuEventCode As String
        Private ReadOnly shimekiribi As DateTime
        Private ReadOnly login As LoginInfo
        Private ReadOnly aShisakuDao As ShisakuDao
        Private ReadOnly shisakuEventDao As TShisakuEventDao
        Private _newStatus As String
        Public Sub New(ByVal shisakuEventCode As String, _
                       ByVal shimekiribi As DateTime, _
                       ByVal login As LoginInfo, _
                       ByVal aShisakuDao As ShisakuDao, _
                       ByVal shisakuEventDao As TShisakuEventDao)
            Me.shisakuEventCode = shisakuEventCode
            Me.shimekiribi = shimekiribi
            Me.login = login
            Me.aShisakuDao = aShisakuDao
            Me.shisakuEventDao = shisakuEventDao
        End Sub

        Public Function GetNewStatus() As String Implements Command.GetNewStatus
            If _newStatus Is Nothing Then
                Throw New InvalidOperationException("#Perform()メソッドを実行してください.")
            End If
            Return _newStatus
        End Function

        Public Sub Perform() Implements Command.Perform
            Dim aDate As New ShisakuDate(aShisakuDao)

            _newStatus = TShisakuEventVoHelper.Status.KAITEI_UKETSUKE_ING
            Dim vo As TShisakuEventVo = shisakuEventDao.FindByPk(shisakuEventCode)
            vo.Status = _newStatus
            vo.Shimekiribi = DateUtil.ConvDateToIneteger(shimekiribi)

            vo.UpdatedUserId = login.UserId
            vo.UpdatedDate = aDate.CurrentDateDbFormat
            vo.UpdatedTime = aDate.CurrentTimeDbFormat
            shisakuEventDao.UpdateByPk(vo)
        End Sub
    End Class
End Namespace