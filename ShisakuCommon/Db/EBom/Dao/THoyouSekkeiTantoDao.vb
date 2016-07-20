Imports ShisakuCommon.Db.EBom.Vo

Namespace Db.EBom.Dao
    ''' <summary>
    ''' ��p�݌v�S�����e�[�u���̊ȒP��CRUD���W�߂�DAO
    ''' </summary>
    ''' <remarks></remarks>
    Public Interface THoyouSekkeiTantoDao : Inherits DaoEachTable(Of THoyouSekkeiTantoVo)
        ''' <summary>
        ''' �e�[�u���l�̌������ʂ�Ԃ�
        ''' </summary>
        ''' <param name="hoyouEventCode">��p�C�x���g�R�[�h</param>
        ''' <param name="hoyouBukaCode">��p���ۃR�[�h</param>
        ''' <param name="hoyouTanto">��p�S��</param>
        ''' <param name="hoyouTantoKaiteiNo">��p�S��������</param>
        ''' <returns>�Y�����R�[�h</returns>
        ''' <remarks></remarks>
        Function FindByPk(ByVal hoyouEventCode As String, _
                          ByVal hoyouBukaCode As String, _
                          ByVal hoyouTanto As String, _
                          ByVal hoyouTantoKaiteiNo As String) As THoyouSekkeiTantoVo

        ''' <summary>
        ''' �Y�����R�[�h���폜����
        ''' </summary>
        ''' <param name="hoyouEventCode">��p�C�x���g�R�[�h</param>
        ''' <param name="hoyouBukaCode">��p���ۃR�[�h</param>
        ''' <param name="hoyouTanto">��p�S��</param>
        ''' <param name="hoyouTantoKaiteiNo">��p�S��������</param>
        ''' <returns>�폜����</returns>
        ''' <remarks></remarks>
        Function DeleteByPk(ByVal hoyouEventCode As String, _
                            ByVal hoyouBukaCode As String, _
                            ByVal hoyouTanto As String, _
                            ByVal hoyouTantoKaiteiNo As String) As Integer
    End Interface
End Namespace