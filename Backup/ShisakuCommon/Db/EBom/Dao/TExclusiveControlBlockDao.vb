Imports ShisakuCommon.Db.EBom.Vo

Namespace Db.EBom.Dao
    ''' <summary>
    ''' �r������u���b�N���e�[�u���̊ȒP��CRUD���W�߂�DAO
    ''' </summary>
    ''' <remarks></remarks>
    Public Interface TExclusiveControlBlockDao : Inherits DaoEachTable(Of TExclusiveControlBlockVo)
        ''' <summary>
        ''' �e�[�u���l�̌������ʂ�Ԃ�
        ''' </summary>
        ''' <param name="shisakuEventCode">����C�x���g�R�[�h</param>
        ''' <param name="shisakuBukaCode">���암�ۃR�[�h</param>
        ''' <param name="shisakuBlockNo">����u���b�N��</param>
        ''' <returns>�Y�����R�[�h</returns>
        ''' <remarks></remarks>
        Function FindByPk(ByVal shisakuEventCode As String, _
                          ByVal shisakuBukaCode As String, _
                          ByVal shisakuBlockNo As String) As TExclusiveControlBlockVo

        ''' <summary>
        ''' �Y�����R�[�h���폜����
        ''' </summary>
        ''' <param name="shisakuEventCode">����C�x���g�R�[�h</param>
        ''' <param name="shisakuBukaCode">���암�ۃR�[�h</param>
        ''' <param name="shisakuBlockNo">����u���b�N��</param>
        ''' <returns>�폜����</returns>
        ''' <remarks></remarks>
        Function DeleteByPk(ByVal shisakuEventCode As String, _
                            ByVal shisakuBukaCode As String, _
                            ByVal shisakuBlockNo As String) As Integer
    End Interface
End Namespace