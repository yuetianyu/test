Imports ShisakuCommon
Imports ShisakuCommon.Db.Impl
Imports ShisakuCommon.Db.Kosei.Vo
Imports ShisakuCommon.Db.Kosei
Imports ShisakuCommon.Db.Kosei.Vo.Helper

Namespace ShisakuBuhinEditBlock.Dao
    Public Class AuthorityUserDaoImpl : Inherits DaoEachFeatureImpl
        Implements IAuthorityUserDao

        Public Function IsKachouShouninKengen(ByVal userId As String) As Boolean Implements IAuthorityUserDao.IsKachouShouninKengen
            Dim kengen = Me.GetMAuthorityUserVo(userId)
            Return (Not kengen Is Nothing)
        End Function

        Public Function GetMAuthorityUserVo(ByVal userId As String) As MAuthorityUserVo
            Dim sql As String = _
            "SELECT * " _
            & "FROM " + MBOM_DB_NAME + ".dbo.M_AUTHORITY_USER WITH (NOLOCK, NOWAIT) " _
            & "WHERE APP_NO=@AppNo " _
            & "  AND KINO_ID_1=@KinoId1 " _
            & "  AND KINO_ID_2=@KinoId2 " _
            & "  AND USER_ID=@UserId " _
            & "  AND AUTHORITY_KBN=@AuthorityKbn "

            Dim param As New MAuthorityUserVo
            param.UserId = userId
            param.AppNo = MAuthorityUserVoHelper.AppNo.TRIAL_MANUFACTURE
            param.KinoId1 = MAuthorityUserVoHelper.KinoId1.SHISAKU_BUHIN
            param.KinoId2 = MAuthorityUserVoHelper.KinoId2.SHOUNIN
            param.AuthorityKbn = MAuthorityUserVoHelper.AuthorityKbn.KENGEN_ARI

            Dim db As New KoseiDbClient
            Return db.QueryForObject(Of MAuthorityUserVo)(sql, param)
        End Function
    End Class
End Namespace

