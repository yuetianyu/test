Imports ShisakuCommon.Db.EBom.Vo

Namespace Db.EBom.Dao
    ''' <summary>
    ''' ����݌v�u���b�NINSTL���e�[�u���̊ȒP��CRUD���W�߂�DAO
    ''' </summary>
    ''' <remarks></remarks>
    Public Interface TShisakuSekkeiBlockInstlDao : Inherits DaoEachTable(Of TShisakuSekkeiBlockInstlVo)
        ''' <summary>
        ''' �e�[�u���l�̌������ʂ�Ԃ�
        ''' </summary>
        ''' <param name="shisakuEventCode">����C�x���g�R�[�h</param>
        ''' <param name="shisakuBukaCode">���암�ۃR�[�h</param>
        ''' <param name="shisakuBlockNo">����u���b�N��</param>
        ''' <param name="shisakuBlockNoKaiteiNo">����u���b�N��������</param>
        ''' <param name="shisakuGousya">���썆��</param>
        ''' <param name="instlHinban">INSTL�i��</param>
        ''' <param name="instlHinbanKbn">INSTL�i�ԋ敪</param>
        ''' <param name="InstlDataKbn">INSTL���f�[�^�敪</param>
        ''' <param name="BaseInstlFlg">�x�[�X���t���O</param>
        ''' <returns>�Y�����R�[�h</returns>
        ''' <remarks></remarks>
        Function FindByPk(ByVal shisakuEventCode As String, _
                          ByVal shisakuBukaCode As String, _
                          ByVal shisakuBlockNo As String, _
                          ByVal shisakuBlockNoKaiteiNo As String, _
                          ByVal shisakuGousya As String, _
                          ByVal instlHinban As String, _
                          ByVal instlHinbanKbn As String, _
                          ByVal InstlDataKbn As String, _
                          ByVal BaseInstlFlg As String) As TShisakuSekkeiBlockInstlVo

        ''' <summary>
        ''' �Y�����R�[�h���폜����
        ''' </summary>
        ''' <param name="shisakuEventCode">����C�x���g�R�[�h</param>
        ''' <param name="shisakuBukaCode">���암�ۃR�[�h</param>
        ''' <param name="shisakuBlockNo">����u���b�N��</param>
        ''' <param name="shisakuBlockNoKaiteiNo">����u���b�N��������</param>
        ''' <param name="shisakuGousya">���썆��</param>
        ''' <param name="instlHinban">INSTL�i��</param>
        ''' <param name="instlHinbanKbn">INSTL�i�ԋ敪</param>
        ''' <param name="InstlDataKbn">INSTL���f�[�^�敪</param>
        ''' <param name="BaseInstlFlg">�x�[�X���t���O</param>
        ''' <returns>�폜����</returns>
        ''' <remarks></remarks>
        Function DeleteByPk(ByVal shisakuEventCode As String, _
                            ByVal shisakuBukaCode As String, _
                            ByVal shisakuBlockNo As String, _
                            ByVal shisakuBlockNoKaiteiNo As String, _
                            ByVal shisakuGousya As String, _
                            ByVal instlHinban As String, _
                            ByVal instlHinbanKbn As String, _
                            ByVal InstlDataKbn As String, _
                            ByVal BaseInstlFlg As String) As Integer
    End Interface
End Namespace