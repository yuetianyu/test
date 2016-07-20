Namespace ShisakuBuhinMenu.Dao
    Public Interface BuhinKouseiDao
        ''' <summary>
        ''' INSTL����INSTL�i�ԂŁA�ŐV������ RHAC0552 ���擾
        ''' </summary>
        ''' <param name="shisakuEventCode">����C�x���g�R�[�h</param>
        ''' <returns>�ŐV������RHAC0552���</returns>
        ''' <remarks></remarks>
        Function FindNew0552ByShisakuInslt(ByVal shisakuEventCode As String) As List(Of BuhinKouseiResultVo)

        ''' <summary>
        ''' INSTL����INSTL�i�ԂŁA�ŐV������ RHAC0553 ���擾
        ''' </summary>
        ''' <param name="shisakuEventCode">����C�x���g�R�[�h</param>
        ''' <returns>�ŐV������RHAC0553���</returns>
        ''' <remarks></remarks>
        Function FindNew0553ByShisakuInslt(ByVal shisakuEventCode As String) As List(Of BuhinKouseiResultVo)

        ''' <summary>
        ''' INSTL����INSTL�i�ԂŁA�ŐV������ RHAC0551 ���擾
        ''' </summary>
        ''' <param name="shisakuEventCode">����C�x���g�R�[�h</param>
        ''' <returns>�ŐV������RHAC0551���</returns>
        ''' <remarks></remarks>
        Function FindNew0551ByShisakuInslt(ByVal shisakuEventCode As String) As List(Of BuhinKouseiResultVo)

    End Interface
End NameSpace