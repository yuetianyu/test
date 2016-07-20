Imports ShisakuCommon.Db.EBom.Vo

Namespace Db.EBom.Dao
    ''' <summary>
    ''' ��p�݌v�S���������e�[�u���̊ȒP��CRUD���W�߂�DAO
    ''' </summary>
    ''' <remarks></remarks>
    Public Interface THoyouSekkeiTantoSoubiDao : Inherits DaoEachTable(Of THoyouSekkeiTantoSoubiVo)
        ''' <summary>
        ''' �e�[�u���l�̌������ʂ�Ԃ�
        ''' </summary>
        ''' <param name="hoyouEventCode">��p�C�x���g�R�[�h</param>
        ''' <param name="hoyouBukaCode">��p���ۃR�[�h</param>
        ''' <param name="hoyouTantoKey">��p�S���j�d�x</param>
        ''' <param name="hoyouTanto">��p�S��</param>
        ''' <param name="hoyouSoubiHyoujiJun">���ڇ�</param>
        ''' <returns>�Y�����R�[�h</returns>
        ''' <remarks></remarks>
        Function FindByPk(ByVal hoyouEventCode As String, _
                          ByVal hoyouBukaCode As String, _
                          ByVal hoyouTantoKey As String, _
                          ByVal hoyouTanto As String, _
                          ByVal hoyouSoubiHyoujiJun As String) As THoyouSekkeiTantoSoubiVo

        ''' <summary>
        ''' �Y�����R�[�h���폜����
        ''' </summary>
        ''' <param name="hoyouEventCode">��p�C�x���g�R�[�h</param>
        ''' <param name="hoyouBukaCode">��p���ۃR�[�h</param>
        ''' <param name="hoyouTantoKey">��p�S���j�d�x</param>
        ''' <param name="hoyouTanto">��p�S��</param>
        ''' <param name="hoyouSoubiHyoujiJun">���ڇ�</param>
        ''' <returns>�폜����</returns>
        ''' <remarks></remarks>
        Function DeleteByPk(ByVal hoyouEventCode As String, _
                            ByVal hoyouBukaCode As String, _
                            ByVal hoyouTantoKey As String, _
                            ByVal hoyouTanto As String, _
                            ByVal hoyouSoubiHyoujiJun As String) As Integer
    End Interface
End Namespace