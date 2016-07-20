Imports ShisakuCommon.Db.EBom
Imports ShisakuCommon.Db.EBom.Vo

Namespace Db.Impl
    Public Class YosanDaoImpl : Inherits DaoEachFeatureImpl
        Implements YosanDao
        Private Const SQL_CURRENT_TIMESTAMP = "SELECT CURRENT_TIMESTAMP"
        ''' <summary>
        ''' DBの日時を返す
        ''' </summary>
        ''' <returns>日時</returns>
        ''' <remarks></remarks>
        Public Overridable Function GetCurrentTimestamp() As DateTime Implements YosanDao.GetCurrentTimestamp
            Dim db As New EBomDbClient
            Return db.QueryForObject(Of DateTime)(SQL_CURRENT_TIMESTAMP)
        End Function

        ''' <summary>
        ''' EBomユーザー情報を返す
        ''' </summary>
        ''' <param name="userId">ユーザーID</param>
        ''' <returns></returns>
        ''' <remarks>内部的にはRHAC0650とRHAC2130の両テーブルを参照している</remarks>
        Public Overridable Function FindUserById(ByVal userId As String) As Rhac0650Vo Implements YosanDao.FindUserById
            Dim sql As String = String.Format("SELECT * FROM ({0}) A WHERE SHAIN_NO = @Value", DataSqlCommon.Get_All_Syain_Sql)
            Dim db As New EBomDbClient
            Return db.QueryForObject(Of Rhac0650Vo)(sql, userId)
        End Function
    End Class
End Namespace