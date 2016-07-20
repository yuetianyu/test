Imports ShisakuCommon.Db.EBom.Vo.Helper
Imports ShisakuCommon
Imports ShisakuCommon.Db
Imports ShisakuCommon.Db.EBom.Dao
Imports ShisakuCommon.Db.EBom.Vo

Namespace ShisakuBuhinMenu.Logic.Impl
    Public Class ChushiCommand : Implements Command

        Private ReadOnly shisakuEventCode As String
        Private ReadOnly login As LoginInfo
        Private ReadOnly aShisakuDao As ShisakuDao
        Private ReadOnly shisakuEventDao As TShisakuEventDao
        Private _newStatus As String
        Public Sub New(ByVal shisakuEventCode As String, _
                       ByVal login As LoginInfo, _
                       ByVal aShisakuDao As ShisakuDao, _
                       ByVal shisakuEventDao As TShisakuEventDao)
            Me.shisakuEventCode = shisakuEventCode
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

            Dim vo As TShisakuEventVo = shisakuEventDao.FindByPk(shisakuEventCode)
            Dim helper As New TShisakuEventVoHelper(vo)
            If helper.IsStatusSekkeiMaintaining OrElse helper.IsStatusSashimodoshiIng Then
                _newStatus = TShisakuEventVoHelper.Status.CHUSHI_UKETSUKE_MAE
            Else
                _newStatus = TShisakuEventVoHelper.Status.CHUSHI_UKETSUKE_GO
            End If
            vo.Status = _newStatus
            vo.Chuushibi = DateUtil.ConvDateToIneteger(aDate.CurrentDateTime)

            vo.UpdatedUserId = login.UserId
            vo.UpdatedDate = aDate.CurrentDateDbFormat
            vo.UpdatedTime = aDate.CurrentTimeDbFormat
            shisakuEventDao.UpdateByPk(vo)
        End Sub


    End Class
End Namespace
