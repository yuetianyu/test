Namespace ShisakuBuhinEditBlock.Dao
    ''' <summary>
    ''' 課長承認権限チェック
    ''' </summary>
    ''' <remarks></remarks>
    Public Interface IAuthorityUserDao
        Function IsKachouShouninKengen(ByVal userId As String) As Boolean
    End Interface
End Namespace

