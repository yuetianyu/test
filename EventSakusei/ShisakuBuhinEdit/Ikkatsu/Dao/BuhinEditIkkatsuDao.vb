Imports ShisakuCommon.Db.EBom.Vo

Namespace ShisakuBuhinEdit.Ikkatsu.Dao
    ''' <summary>
    ''' ���i�\���ďo�i�ꊇ�ݒ�j��p��DAO
    ''' </summary>
    ''' <remarks></remarks>
    Public Interface BuhinEditIkkatsuDao
        ''' <summary>
        ''' INSTL�i�ԂɕR�t������݌v�u���b�NINSTL�����擾����
        ''' </summary>
        ''' <param name="instlHinban">INSTL�i��</param>
        ''' <param name="shisakuEventCode">���O���鎎��C�x���g�R�[�h</param>
        ''' <param name="shisakuBukaCode">���O���鎎�암�ۃR�[�h</param>
        ''' <param name="shisakuBlockNo">���O���鎎��u���b�NNo</param>
        ''' <param name="shisakuBlockNoKaiteiNo">���O���鎎��u���b�NNo����No</param>
        ''' <returns>�Y���f�[�^</returns>
        ''' <remarks>����敪�������Y������ꍇ�A�ŏI�X�V�����̃f�[�^���擾</remarks>
        Function FindLatestInstlHinbanKbnBy(ByVal instlHinban As String, ByVal shisakuEventCode As String, ByVal shisakuBukaCode As String, ByVal shisakuBlockNo As String, ByVal shisakuBlockNoKaiteiNo As String) As List(Of TShisakuSekkeiBlockInstlVo)
        ''' <summary>
        ''' ����C�x���g����Ԃ�
        ''' </summary>
        ''' <param name="shisakuEventCode"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Function FindShisakuEventName(ByVal shisakuEventCode As String) As TShisakuEventVo

        ''' <summary>
        ''' ���i�ԍ�������553�ŗ��p���Ă���J��������Ԃ�
        ''' </summary>
        ''' <param name="instlHinban"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Function FindKaihatsuFugoOf553ByInstlHinban(ByVal instlHinban As String) As List(Of TShisakuEventVo)

        ''' <summary>
        ''' ����̕��i�ԍ��A����敪��������C�x���g��݌v�u���b�NINSTL����擾���Ԃ�
        ''' </summary>
        ''' <param name="shisakuEventCode"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Function FindShisakuEventByInstlHinbanAndKbn(ByVal instlHinban As String, ByVal shisakuEventCode As String, ByVal shisakuBukaCode As String, ByVal shisakuBlockNo As String, ByVal shisakuBlockNoKaiteiNo As String, ByVal instlHinbanKbn As String) As List(Of TShisakuSekkeiBlockInstlVo)

        ''' <summary>
        ''' �C�x���g�R�[�h�ȊO��INSTL�i�Ԃ��擾����
        ''' </summary>
        ''' <param name="shisakuEventCode">�C�x���g�R�[�h</param>
        ''' <param name="instlHinban">INSTL�i��</param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Function FindBySekkeiBlockInstlKbn(ByVal shisakuEventCode As String, ByVal instlHinban As String) As List(Of TShisakuSekkeiBlockInstlVo)

    End Interface
End Namespace