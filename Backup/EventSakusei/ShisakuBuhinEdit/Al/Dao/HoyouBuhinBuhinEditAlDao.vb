Imports ShisakuCommon.Db.EBom.Vo

Namespace ShisakuBuhinEdit.Al.Dao
    ''' <summary>
    ''' A/L��ʗp��Dao
    ''' </summary>
    ''' <remarks></remarks>
    Public Interface HoyouBuhinBuhinEditAlDao
        '''' <summary>
        '''' �x�[�X�ԏ��E�����ԏ����Q�Ƃ���
        '''' </summary>
        '''' <param name="hoyouEventCode">��p�C�x���g�R�[�h</param>
        '''' <returns>�x�[�X�ԏ��E�����ԏ��</returns>
        '''' <remarks></remarks>
        'Function FindEventInfoById(ByVal hoyouEventCode As String) As List(Of BuhinEditAlEventVo)


        '�S���ҌĂяo���p'
        ''' <summary>
        ''' �S���҂̏����擾����
        ''' </summary>
        ''' <param name="hoyouEventCode">�C�x���g�R�[�h</param>
        ''' <param name="hoyouBukaCode">���ۃR�[�h</param>
        ''' <param name="hoyouTanto">�S����</param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Function FindByNewTanto(ByVal hoyouEventCode As String, ByVal hoyouBukaCode As String, ByVal hoyouTanto As String) As THoyouSekkeiTantoVo

        ''' <summary>
        ''' DB�̃^�C���X�^���v���擾
        ''' </summary>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Function FindByDBTimeStamp(ByVal hoyouEventCode As String) As THoyouSekkeiTantoVo


        ''' <summary>
        ''' ����݌v�u���b�N�̍ŏI�X�V�����m�F����
        ''' </summary>
        ''' <param name="hoyouEventCode"></param>
        ''' <param name="hoyouBukaCode"></param>
        ''' <param name="hoyouTanto"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Function FindByLastModifyDateTimeOfSekkeiTanto(ByVal hoyouEventCode As String, ByVal hoyouBukaCode As String, ByVal hoyouTanto As String, ByVal openDateTime As String) As THoyouSekkeiTantoVo


    End Interface
End Namespace