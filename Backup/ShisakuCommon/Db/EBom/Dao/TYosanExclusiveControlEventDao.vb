Imports ShisakuCommon.Db.EBom.Vo

Namespace Db.EBom.Dao
    ''' <summary>
    ''' �r���Ǘ��\�Z���C�x���g���e�[�u���̊ȒP��CRUD���W�߂�DAO
    ''' </summary>
    ''' <remarks></remarks>
    Public Interface TYosanExclusiveControlEventDao : Inherits DaoEachTable(Of TYosanExclusiveControlEventVo)
        ''' <summary>
        ''' �e�[�u���l�̌������ʂ�Ԃ�
        ''' </summary>
        ''' <param name="yosanEventCode">�\�Z�C�x���g�R�[�h</param>
        ''' <param name="editUserId">�ҏW�ҐE��</param>
        ''' <returns>�Y�����R�[�h</returns>
        ''' <remarks></remarks>
        Function FindByPk(ByVal yosanEventCode As String, ByVal editUserId As String) As TYosanExclusiveControlEventVo

        ''' <summary>
        ''' �Y�����R�[�h���폜����
        ''' </summary>
        ''' <param name="yosanEventCode">�\�Z�C�x���g�R�[�h</param>
        ''' <param name="editUserId">�ҏW�ҐE��</param>
        ''' <returns>�폜����</returns>
        ''' <remarks></remarks>
        Function DeleteByPk(ByVal yosanEventCode As String, ByVal editUserId As String) As Integer
    End Interface
End Namespace