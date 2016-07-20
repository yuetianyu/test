Imports ShisakuCommon.Db.EBom.Vo

Namespace Db.EBom.Dao
    ''' <summary>
    ''' �\�Z�����i�ҏW���e�[�u���̊ȒP��CRUD���W�߂�DAO
    ''' </summary>
    ''' <remarks></remarks>
    Public Interface TYosanBuhinEditDao : Inherits DaoEachTable(Of TYosanBuhinEditVo)
        ''' <summary>
        ''' �e�[�u���l�̌������ʂ�Ԃ�
        ''' </summary>
        ''' <param name="yosanEventCode">�\�Z�C�x���g�R�[�h</param>
        ''' <param name="buhinName">���i�\��</param>
        ''' <param name="yosanBlockNo">�\�Z�u���b�N��</param>
        ''' <param name="buhinNoHyoujiJun">���i�ԍ��\����</param>
        ''' <returns>�Y�����R�[�h</returns>
        ''' <remarks></remarks>
        Function FindByPk(ByVal yosanEventCode As String, ByVal buhinName As String, _
                          ByVal yosanBlockNo As String, ByVal buhinNoHyoujiJun As Int32) As TYosanBuhinEditVo

        ''' <summary>
        ''' �Y�����R�[�h���폜����
        ''' </summary>
        ''' <param name="yosanEventCode">�\�Z�C�x���g�R�[�h</param>
        ''' <param name="buhinName">���i�\��</param>
        ''' <param name="yosanBlockNo">�\�Z�u���b�N��</param>
        ''' <param name="buhinNoHyoujiJun">���i�ԍ��\����</param>
        ''' <returns>�폜����</returns>
        ''' <remarks></remarks>
        Function DeleteByPk(ByVal yosanEventCode As String, ByVal buhinName As String, _
                            ByVal yosanBlockNo As String, ByVal buhinNoHyoujiJun As Int32) As Integer
    End Interface
End Namespace