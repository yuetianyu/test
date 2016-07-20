Imports ShisakuCommon.Db.EBom.Vo

Namespace Db.EBom.Dao
    ''' <summary>
    ''' ���암�i�ҏW�݌v�ޗ�(EBOM�ݕ�)�e�[�u���̊ȒP��CRUD���W�߂�DAO
    ''' </summary>
    ''' <remarks></remarks>
    Public Interface TShisakuBuhinEditEbomKanshiZairyouDao : Inherits DaoEachTable(Of TShisakuBuhinEditEbomKanshiZairyouVo)
        ''' <summary>
        ''' �e�[�u���l�̌������ʂ�Ԃ�
        ''' </summary>
        ''' <param name="shisakuEventCode">����C�x���g�R�[�h</param>
        ''' <param name="shisakuBukaCode">���암�ۃR�[�h</param>
        ''' <param name="shisakuBlockNo">����u���b�N��</param>
        ''' <param name="shisakuBlockNoKaiteiNo">����u���b�N��������</param>
        ''' <param name="buhinNoHyoujiJun">���i�ԍ��\����</param>
        ''' <returns>�Y�����R�[�h</returns>
        ''' <remarks></remarks>
        Function FindByPk(ByVal shisakuEventCode As String, _
                          ByVal shisakuBukaCode As String, _
                          ByVal shisakuBlockNo As String, _
                          ByVal shisakuBlockNoKaiteiNo As String, _
                          ByVal buhinNoHyoujiJun As Integer) As TShisakuBuhinEditEbomKanshiZairyouVo

        ''' <summary>
        ''' �Y�����R�[�h���폜����
        ''' </summary>
        ''' <param name="shisakuEventCode">����C�x���g�R�[�h</param>
        ''' <param name="shisakuBukaCode">���암�ۃR�[�h</param>
        ''' <param name="shisakuBlockNo">����u���b�N��</param>
        ''' <param name="shisakuBlockNoKaiteiNo">����u���b�N��������</param>
        ''' <param name="buhinNoHyoujiJun">���i�ԍ��\����</param>
        ''' <returns>�폜����</returns>
        ''' <remarks></remarks>
        Function DeleteByPk(ByVal shisakuEventCode As String, _
                            ByVal shisakuBukaCode As String, _
                            ByVal shisakuBlockNo As String, _
                            ByVal shisakuBlockNoKaiteiNo As String, _
                            ByVal buhinNoHyoujiJun As Integer) As Integer
    End Interface
End Namespace