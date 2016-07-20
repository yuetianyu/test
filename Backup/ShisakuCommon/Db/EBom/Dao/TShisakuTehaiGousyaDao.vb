Imports ShisakuCommon.Db.EBom.Vo

Namespace Db.EBom.Dao
    ''' <summary>
    ''' ����݌v�u���b�N�������e�[�u���̊ȒP��CRUD���W�߂�DAO
    ''' </summary>
    ''' <remarks></remarks>
    Public Interface TShisakuTehaiGousyaDao : Inherits DaoEachTable(Of TShisakuTehaiGousyaVo)
        ''' <summary>
        ''' �e�[�u���l�̌������ʂ�Ԃ�
        ''' </summary>
        ''' <param name="shisakuEventCode">����C�x���g�R�[�h</param>
        ''' <param name="shisakuListCode">���샊�X�g�R�[�h</param>
        ''' <param name="shisakuListCodeKaiteiNo">���샊�X�g�R�[�h������</param>
        ''' <param name="shisakuBukaCode">���암�ۃR�[�h</param>
        ''' <param name="buhinNoHyoujiJun">���i�ԍ��\����</param>
        ''' <returns>�Y�����R�[�h</returns>
        ''' <remarks></remarks>
        Function FindByPk(ByVal shisakuEventCode As String, ByVal shisakuListCode As String, _
                          ByVal shisakuListCodeKaiteiNo As String, ByVal shisakuBukaCode As String, _
                          ByVal buhinNoHyoujiJun As String) As TShisakuTehaiGousyaVo

        ''' <summary>
        ''' �Y�����R�[�h���폜����
        ''' </summary>
        ''' <param name="shisakuEventCode">����C�x���g�R�[�h</param>
        ''' <param name="shisakuListCode">���샊�X�g�R�[�h</param>
        ''' <param name="shisakuListCodeKaiteiNo">���샊�X�g�R�[�h������</param>
        ''' <param name="shisakuBukaCode">���암�ۃR�[�h</param>
        ''' <param name="buhinNoHyoujiJun">���i�ԍ��\����</param>
        ''' <returns>�폜����</returns>
        ''' <remarks></remarks>
        Function DeleteByPk(ByVal shisakuEventCode As String, ByVal shisakuListCode As String, _
                          ByVal shisakuListCodeKaiteiNo As String, ByVal shisakuBukaCode As String, _
                          ByVal buhinNoHyoujiJun As String) As Integer
        ''' <summary>
        ''' �w�肳�ꂽ���ԏ�񂪃e�[�u���ɑ��݂��邩�`�F�b�N.
        ''' </summary>
        ''' <param name="shisakuEventCode">��������C�x���g�R�[�h</param>
        ''' <param name="listCode">���X�g�R�[�h</param>
        ''' <param name="shisakuGousyaList">���ԏ��</param>
        ''' <returns>True:���݂��� False:���݂��Ȃ�</returns>
        ''' <remarks></remarks>
        Function ExistByShisakuGousya(ByVal shisakuEventCode As String, _
                                      ByVal listCode As String, _
                                      ByVal shisakuGousyaList As Dictionary(Of Integer, String)) As Boolean

    End Interface
End Namespace