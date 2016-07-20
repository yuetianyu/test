Namespace ShisakuBuhinMenu.Dao
    Public interface BuhinDao
        '''' <summary>
        '''' INSTL����INSTL�i�Ԃ��\�����Ă��镔�i RHAC0532 ���擾
        '''' </summary>
        '''' <param name="shisakuEventCode">����C�x���g�R�[�h</param>
        '''' <returns>���i RHAC0532 ���</returns>
        '''' <remarks></remarks>
        'Function Find0532ByShisakuInstl(ByVal shisakuEventCode As String) As List(Of BuhinResultVo)

        ''' <summary>
        ''' ����̍\����񂩂�A���i RHAC0532 ���擾
        ''' </summary>
        ''' <param name="shisakuEventCode">����C�x���g�R�[�h</param>
        ''' <param name="sakuseiCount">�쐬��</param>
        ''' <returns>���i RHAC0532 ���</returns>
        ''' <remarks></remarks>
        Function Find0532ByShisakuKousei(ByVal shisakuEventCode As String, ByVal sakuseiCount As Integer) As List(Of BuhinResultVo)

        ''' <summary>
        ''' ����̍\����񂩂�A���i RHAC0533 ���擾
        ''' </summary>
        ''' <param name="shisakuEventCode">����C�x���g�R�[�h</param>
        ''' <param name="sakuseiCount">�쐬��</param>
        ''' <returns>���i RHAC0533 ���</returns>
        ''' <remarks></remarks>
        Function Find0533ByShisakuKousei(ByVal shisakuEventCode As String, ByVal sakuseiCount As Integer) As List(Of BuhinResultVo)

        ''' <summary>
        ''' ����̍\����񂩂�A���i RHAC0530 ���擾
        ''' </summary>
        ''' <param name="shisakuEventCode">����C�x���g�R�[�h</param>
        ''' <param name="sakuseiCount">�쐬��</param>
        ''' <returns>���i RHAC0530 ���</returns>
        ''' <remarks></remarks>
        Function Find0530ByShisakuKousei(ByVal shisakuEventCode As String, ByVal sakuseiCount As Integer) As List(Of BuhinResultVo)

    End Interface
End NameSpace