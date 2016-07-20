Imports ShisakuCommon.Db.EBom.Vo

Namespace ShisakuBuhinEdit.Al.Dao
    ''' <summary>
    ''' A/L��ʗp��Dao
    ''' </summary>
    ''' <remarks></remarks>
    Public Interface BuhinEditAlDao
        ''' <summary>
        ''' �x�[�X�ԏ��E�����ԏ����Q�Ƃ���
        ''' </summary>
        ''' <param name="shisakuEventCode">����C�x���g�R�[�h</param>
        ''' <returns>�x�[�X�ԏ��E�����ԏ��</returns>
        ''' <remarks></remarks>
        Function FindEventInfoById(ByVal shisakuEventCode As String) As List(Of BuhinEditAlEventVo)


        '�u���b�N�Ăяo���p'
        ''' <summary>
        ''' �u���b�NNo�̏����擾����
        ''' </summary>
        ''' <param name="shisakuEventCode">�C�x���g�R�[�h</param>
        ''' <param name="shisakuBukaCode">���ۃR�[�h</param>
        ''' <param name="shisakuBlockNo">�u���b�NNo</param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Function FindByNewBlockNo(ByVal shisakuEventCode As String, ByVal shisakuBukaCode As String, ByVal shisakuBlockNo As String) As TShisakuSekkeiBlockVo

        ''' <summary>
        ''' DB�̃^�C���X�^���v���擾
        ''' </summary>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Function FindByDBTimeStamp(ByVal shisakuEventCode As String) As TShisakuSekkeiBlockVo


        ''' <summary>
        ''' ����݌v�u���b�N�̍ŏI�X�V�����m�F����
        ''' </summary>
        ''' <param name="shisakuEventCode"></param>
        ''' <param name="shisakuBukaCode"></param>
        ''' <param name="shisakuBlockNo"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Function FindByLastModifyDateTimeOfSekkeiBlock(ByVal shisakuEventCode As String, ByVal shisakuBukaCode As String, ByVal shisakuBlockNo As String, ByVal openDateTime As String) As TShisakuSekkeiBlockVo


    End Interface
End Namespace