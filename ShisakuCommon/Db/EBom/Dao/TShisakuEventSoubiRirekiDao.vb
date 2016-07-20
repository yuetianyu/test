Imports ShisakuCommon.Db.EBom.Vo

Namespace Db.EBom.Dao
    ''' <summary>
    ''' ����C�x���g���ʑ����������e�[�u���̊ȒP��CRUD���W�߂�DAO
    ''' </summary>
    ''' <remarks></remarks>
    Public Interface TShisakuEventSoubiRirekiDao : Inherits DaoEachTable(Of TShisakuEventSoubiRirekiVo)
        ''' <summary>
        ''' �e�[�u���l�̌������ʂ�Ԃ�
        ''' </summary>
        ''' <param name="shisakuEventCode">����C�x���g�R�[�h</param>
        ''' <param name="hyojijunNo">�\����</param>
        ''' <param name="shisakuTokubetuSoubiHyoujiNo">������ʑ����\����</param>
        ''' <param name="shisakuTokubetuSoubiKbn">������ʑ����敪</param>
        ''' <param name="updateBi">�X�V��</param>
        ''' <param name="updateJikan">�X�V����</param>
        ''' <returns>�Y�����R�[�h</returns>
        ''' <remarks></remarks>
        Function FindByPk(ByVal shisakuEventCode As String, ByVal hyojijunNo As Nullable(Of Int32) _
                         , ByVal shisakuTokubetuSoubiHyoujiNo As Nullable(Of Int32), ByVal shisakuTokubetuSoubiKbn As String _
                         , ByVal updateBi As String, Byval updateJikan As String) As TShisakuEventSoubiRirekiVo

        ''' <summary>
        ''' �Y�����R�[�h���폜����
        ''' </summary>
        ''' <param name="shisakuEventCode">����C�x���g�R�[�h</param>
        ''' <param name="hyojijunNo">�\����</param>
        ''' <param name="shisakuTokubetuSoubiHyoujiNo">������ʑ����\����</param>
        ''' <param name="shisakuTokubetuSoubiKbn">������ʑ����敪</param>
        ''' <param name="updateBi">�X�V��</param>
        ''' <param name="updateJikan">�X�V����</param>
        ''' <returns>�폜����</returns>
        ''' <remarks></remarks>
        Function DeleteByPk(ByVal shisakuEventCode As String, ByVal hyojijunNo As Nullable(Of Int32) _
                         , ByVal shisakuTokubetuSoubiHyoujiNo As Nullable(Of Int32), ByVal shisakuTokubetuSoubiKbn As String _
                         , ByVal updateBi As String, ByVal updateJikan As String) As Integer
    End Interface
End Namespace