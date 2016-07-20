Imports ShisakuCommon.Db.EBom.Vo

Namespace YosanEventNew.Dao

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <remarks></remarks>
    Public Interface YosanEventNewDao

        ''' <summary>
        ''' MAX予算イベントコード
        ''' </summary>
        ''' <param name="eventCode">予算書</param>
        ''' <returns>MAX予算イベントコード</returns>
        ''' <remarks></remarks>
        Function FindMaxEventCode(ByVal eventCode As TYosanEventVo) As TYosanEventVo

    End Interface

End Namespace