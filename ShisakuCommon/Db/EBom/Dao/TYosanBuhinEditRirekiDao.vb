Imports ShisakuCommon.Db.EBom.Vo

Namespace Db.EBom.Dao
    ''' <summary>
    ''' �\�Z�����i�������e�[�u���̊ȒP��CRUD���W�߂�DAO
    ''' </summary>
    ''' <remarks></remarks>
    Public Interface TYosanBuhinEditRirekiDao : Inherits DaoEachTable(Of TYosanBuhinEditRirekiVo)
        ''' <summary>
        ''' �e�[�u���l�̌������ʂ�Ԃ�
        ''' </summary>
        ''' <param name="yosanEventCode">�\�Z�C�x���g�R�[�h</param>
        ''' <param name="UnitKbn">���j�b�g�敪</param>
        ''' <param name="registerDate">�o�^��</param>
        ''' <param name="yosanBlockNo">�\�Z�u���b�N��</param>
        ''' <param name="buhinNoHyoujiJun">���i�ԍ��\����</param>
        ''' <returns>�Y�����R�[�h</returns>
        ''' <remarks></remarks>
        Function FindByPk(ByVal yosanEventCode As String, ByVal UnitKbn As String, ByVal registerDate As String, _
                          ByVal yosanBlockNo As String, ByVal buhinNoHyoujiJun As Int32) As TYosanBuhinEditRirekiVo

        ''' <summary>
        ''' �Y�����R�[�h���폜����
        ''' </summary>
        ''' <param name="yosanEventCode">�\�Z�C�x���g�R�[�h</param>
        ''' <param name="UnitKbn">���j�b�g�敪</param>
        ''' <param name="registerDate">�o�^��</param>
        ''' <param name="yosanBlockNo">�\�Z�u���b�N��</param>
        ''' <param name="buhinNoHyoujiJun">���i�ԍ��\����</param>
        ''' <returns>�폜����</returns>
        ''' <remarks></remarks>
        Function DeleteByPk(ByVal yosanEventCode As String, ByVal UnitKbn As String, ByVal registerDate As String, _
                            ByVal yosanBlockNo As String, ByVal buhinNoHyoujiJun As Int32) As Integer
    End Interface
End Namespace