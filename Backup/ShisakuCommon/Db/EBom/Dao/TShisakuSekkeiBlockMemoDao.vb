Imports ShisakuCommon.Db.EBom.Vo

Namespace Db.EBom.Dao
    ''' <summary>
    ''' ����݌v�u���b�N�������e�[�u���̊ȒP��CRUD���W�߂�DAO
    ''' </summary>
    ''' <remarks></remarks>
    Public Interface TShisakuSekkeiBlockMemoDao : Inherits DaoEachTable(Of TShisakuSekkeiBlockMemoVo)
        ''' <summary>
        ''' �e�[�u���l�̌������ʂ�Ԃ�
        ''' </summary>
        ''' <param name="shisakuEventCode">����C�x���g�R�[�h</param>
        ''' <param name="shisakuBukaCode">���암�ۃR�[�h</param>
        ''' <param name="shisakuBlockNo">����u���b�N��</param>
        ''' <param name="shisakuBlockNoKaiteiNo">����u���b�N��������</param>
        ''' <param name="shisakuGousya">���썆��</param>
        ''' <param name="shisakuMemoHyoujiJun">���상���\����</param>
        ''' <returns>�Y�����R�[�h</returns>
        ''' <remarks></remarks>
        Function FindByPk(ByVal shisakuEventCode As String, ByVal shisakuBukaCode As String _
                         , ByVal shisakuBlockNo As String, ByVal shisakuBlockNoKaiteiNo As String _
                         , ByVal shisakuGousya As String, ByVal shisakuMemoHyoujiJun As Nullable(Of Int32)) _
                         As TShisakuSekkeiBlockMemoVo

        ''' <summary>
        ''' �Y�����R�[�h���폜����
        ''' </summary>
        ''' <param name="shisakuEventCode">����C�x���g�R�[�h</param>
        ''' <param name="shisakuBukaCode">���암�ۃR�[�h</param>
        ''' <param name="shisakuBlockNo">����u���b�N��</param>
        ''' <param name="shisakuBlockNoKaiteiNo">����u���b�N��������</param>
        ''' <param name="shisakuGousya">���썆��</param>
        ''' <param name="shisakuMemoHyoujiJun">���상���\����</param>
        ''' <returns>�폜����</returns>
        ''' <remarks></remarks>
        Function DeleteByPk(ByVal shisakuEventCode As String, ByVal shisakuBukaCode As String _
                         , ByVal shisakuBlockNo As String, ByVal shisakuBlockNoKaiteiNo As String _
                         , ByVal shisakuGousya As String, ByVal shisakuMemoHyoujiJun As Nullable(Of Int32)) As Integer
    End Interface
End Namespace