Imports ShisakuCommon.Db.EBom.Vo

Namespace Db.EBom.Dao
    ''' <summary>
    ''' ��p�C�x���g���e�[�u���̊ȒP��CRUD���W�߂�DAO
    ''' </summary>
    ''' <remarks></remarks>
    Public Interface THoyouEventDao : Inherits DaoEachTable(Of THoyouEventVo)
        ''' <summary>
        ''' �e�[�u���l�̌������ʂ�Ԃ�
        ''' </summary>
        ''' <param name="hoyouEventCode">��p�C�x���g�R�[�h</param>
        ''' <returns>�Y�����R�[�h</returns>
        ''' <remarks></remarks>
        Function FindByPk(ByVal hoyouEventCode As String) As THoyouEventVo

        ''' <summary>
        ''' �Y�����R�[�h���폜����
        ''' </summary>
        ''' <param name="hoyouEventCode">��p�C�x���g�R�[�h</param>
        ''' <returns>�폜����</returns>
        ''' <remarks></remarks>
        Function DeleteByPk(ByVal hoyouEventCode As String) As Integer
    End Interface
End Namespace