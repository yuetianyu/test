Imports ShisakuCommon.Db.EBom.Vo.Helper
Imports ShisakuCommon.Db.Impl
Imports ShisakuCommon.Db
Imports ShisakuCommon
Imports ShisakuCommon.Db.EBom
Imports ShisakuCommon.Util.LabelValue
Imports ShisakuCommon.Db.EBom.Vo
Imports EventSakusei.EventEdit.Dao
Imports ShisakuCommon.Db.EBom.Dao
Imports ShisakuCommon.Db.EBom.Dao.Impl
Imports System.Text

Namespace ShisakuBuhinKaiteiBlock.Dao

    Public Class MUserMailAddressDaoImpl : Inherits DaoEachFeatureImpl
        Implements IMUserMailAddressDao

#Region "宛先情報を取得する"
        ''' <summary>
        ''' 宛先情報を取得する
        ''' </summary>
        ''' <returns>宛先一覧</returns>
        ''' <remarks></remarks>
        Public Function GetBlockSpreadList() As List(Of MUserMailAddressVo) Implements Dao.IMUserMailAddressDao.GetSendList
            Dim sql As String = _
            "SELECT " _
            & "   USER_ID , " _
            & "   MAIL_ADDRESS , " _
            & "   MAIL_KBN " _
            & "FROM  " _
            & "   " & MBOM_DB_NAME & ".dbo.M_USER_MAIL_ADDRESS WITH (NOLOCK, NOWAIT) " _
            & "ORDER BY  " _
            & "   USER_ID ASC "

            Dim db As New EBomDbClient
            Return db.QueryForList(Of MUserMailAddressVo)(sql)
        End Function
#End Region

    End Class

End Namespace
