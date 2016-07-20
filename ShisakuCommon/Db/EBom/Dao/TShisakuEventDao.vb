Imports ShisakuCommon.Db.EBom.Vo

Namespace Db.EBom.Dao
    ''' <summary>
    ''' ����C�x���g���e�[�u���̊ȒP��CRUD���W�߂�DAO
    ''' </summary>
    ''' <remarks></remarks>
    Public Interface TShisakuEventDao : Inherits DaoEachTable(Of TShisakuEventVo)
        ''' <summary>
        ''' �e�[�u���l�̌������ʂ�Ԃ�
        ''' </summary>
        ''' <param name="shisakuEventCode">����C�x���g�R�[�h</param>
        ''' <returns>�Y�����R�[�h</returns>
        ''' <remarks></remarks>
        Function FindByPk(ByVal shisakuEventCode As String) As TShisakuEventVo

        ''' <summary>
        ''' �Y�����R�[�h���폜����
        ''' </summary>
        ''' <param name="shisakuEventCode">����C�x���g�R�[�h</param>
        ''' <returns>�폜����</returns>
        ''' <remarks></remarks>
        Function DeleteByPk(ByVal shisakuEventCode As String) As Integer
    End Interface
End Namespace