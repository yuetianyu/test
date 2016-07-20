Imports ShisakuCommon.Db.EBom.Vo

Namespace Db.EBom.Dao
    ''' <summary>
    ''' �����z�����i��{���j�e�[�u���̊ȒP��CRUD���W�߂�DAO
    ''' </summary>
    ''' <remarks></remarks>
    Public Interface TShisakuTehaiKihonDao : Inherits DaoEachTable(Of TShisakuTehaiKihonVo)
        ''' <summary>
        ''' �e�[�u���l�̌������ʂ�Ԃ�
        ''' </summary>
        ''' <param name="shisakuEventCode">����C�x���g�R�[�h</param>
        ''' <param name="shisakuListCode">���샊�X�g�R�[�h</param>
        ''' <param name="shisakuListCodeKaiteiNo">���샊�X�g�R�[�h������</param>
        ''' <param name="shisakuBukaCode">���암�ۃR�[�h</param>
        ''' <param name="shisakuBlockNo">����u���b�NNo</param>
        ''' <param name="buhinNoHyoujiJun">���i�ԍ��\����</param>
        ''' <returns>�Y�����R�[�h</returns>
        ''' <remarks></remarks>
        Function FindByPk(ByVal shisakuEventCode As String, ByVal shisakuListCode As String, _
                          ByVal shisakuListCodeKaiteiNo As String, ByVal shisakuBukaCode As String, _
                          ByVal shisakuBlockNo As String, ByVal buhinNoHyoujiJun As Int32) As TShisakuTehaiKihonVo

        ''' <summary>
        ''' �Y�����R�[�h���폜����
        ''' </summary>
        ''' <param name="shisakuEventCode">����C�x���g�R�[�h</param>
        ''' <param name="shisakuListCode">���샊�X�g�R�[�h</param>
        ''' <param name="shisakuListCodeKaiteiNo">���샊�X�g�R�[�h������</param>
        ''' <param name="shisakuBukaCode">���암�ۃR�[�h</param>
        ''' <param name="shisakuBlockNo">����u���b�NNo</param>
        ''' <param name="buhinNoHyoujiJun">���i�ԍ��\����</param>
        ''' <returns>�폜����</returns>
        ''' <remarks></remarks>
        Function DeleteByPk(ByVal shisakuEventCode As String, ByVal shisakuListCode As String, _
                          ByVal shisakuListCodeKaiteiNo As String, ByVal shisakuBukaCode As String, _
                          ByVal shisakuBlockNo As String, ByVal buhinNoHyoujiJun As Int32) As Integer
    End Interface
End Namespace