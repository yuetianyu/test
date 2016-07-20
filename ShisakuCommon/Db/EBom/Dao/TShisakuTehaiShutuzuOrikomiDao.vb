Imports ShisakuCommon.Db.EBom.Vo

Namespace Db.EBom.Dao
    ''' <summary>
    '''�����z�o�}�D�����e�[�u���̊ȒP��CRUD���W�߂�DAO
    ''' </summary>
    ''' <remarks></remarks>
    Public Interface TShisakuTehaiShutuzuOrikomiDao : Inherits DaoEachTable(Of TShisakuTehaiShutuzuOrikomiVo)
        ''' <summary>
        ''' �e�[�u���l�̌������ʂ�Ԃ�
        ''' </summary>
        ''' <param name="ShisakuEventCode">����C�x���g�R�[�h</param>
        ''' <param name="ShisakuListCode">���샊�X�g�R�[�h</param>
        ''' <param name="ShisakuListCodeKaiteiNo">���샊�X�g�R�[�h������</param>
        ''' <param name="ShisakuBlockNo">����u���b�N��</param>
        ''' <param name="GyouId">�sID</param>
        ''' <param name="BuhinNo">���i�ԍ�</param>
        ''' <returns>�Y�����R�[�h</returns>
        ''' <remarks></remarks>
        Function FindByPk(ByVal ShisakuEventCode As String, ByVal ShisakuListCode As String _
                         , ByVal ShisakuListCodeKaiteiNo As String, ByVal ShisakuBlockNo As String _
                         , ByVal GyouId As String, ByVal BuhinNo As String) _
                         As TShisakuTehaiShutuzuOrikomiVo

        ''' <summary>
        ''' �Y�����R�[�h���폜����
        ''' </summary>
        ''' <param name="ShisakuEventCode">����C�x���g�R�[�h</param>
        ''' <param name="ShisakuListCode">���샊�X�g�R�[�h</param>
        ''' <param name="ShisakuListCodeKaiteiNo">���샊�X�g�R�[�h������</param>
        ''' <param name="ShisakuBlockNo">����u���b�N��</param>
        ''' <param name="GyouId">�sID</param>
        ''' <param name="BuhinNo">���i�ԍ�</param>
        ''' <returns>>�폜����</returns>
        ''' <remarks></remarks>
        Function DeleteByPk(ByVal ShisakuEventCode As String, ByVal ShisakuListCode As String _
                          , ByVal ShisakuListCodeKaiteiNo As String, ByVal ShisakuBlockNo As String _
                         , ByVal GyouId As String, ByVal BuhinNo As String) _
                          As Integer
    End Interface
End Namespace