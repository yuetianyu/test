Imports ShisakuCommon.Db.EBom.Vo

Namespace Db.EBom.Dao
    ''' <summary>
    ''' ���샊�X�g�R�[�h���e�[�u���̊ȒP��CRUD���W�߂�DAO
    ''' </summary>
    ''' <remarks></remarks>
    Public Interface TShisakuListcodeDao : Inherits DaoEachTable(Of TShisakuListcodeVo)
        ''' <summary>
        ''' �e�[�u���l�̌������ʂ�Ԃ�
        ''' </summary>
        ''' <param name="shisakuEventCode">����C�x���g�R�[�h</param>
        ''' <param name="shisakuListHyojijunNo">���샊�X�g�\����</param>
        ''' <param name="shisakuListCode">���샊�X�g�R�[�h</param>
        ''' <param name="shisakuListCodeKaiteiNo">���샊�X�g�R�[�h������</param>
        ''' <param name="shisakuGroupNo">����O���[�v��</param>
        ''' <returns>�Y�����R�[�h</returns>
        ''' <remarks></remarks>
        Function FindByPk(ByVal shisakuEventCode As String, _
                          ByVal shisakuListHyojijunNo As Nullable(Of Int32), _
                          ByVal shisakuListCode As String, _
                          ByVal shisakuListCodeKaiteiNo As String, _
                          ByVal shisakuGroupNo As String) As TShisakuListcodeVo

        ''' <summary>
        ''' �Y�����R�[�h���폜����
        ''' </summary>
        ''' <param name="shisakuEventCode">����C�x���g�R�[�h</param>
        ''' <param name="shisakuListHyojijunNo">���샊�X�g�\����</param>
        ''' <param name="shisakuListCode">���샊�X�g�R�[�h</param>
        ''' <param name="shisakuListCodeKaiteiNo">���샊�X�g�R�[�h������</param>
        ''' <param name="shisakuGroupNo">����O���[�v��</param>
        ''' <returns>�폜����</returns>
        ''' <remarks></remarks>
        Function DeleteByPk(ByVal shisakuEventCode As String, _
                            ByVal shisakuListHyojijunNo As Nullable(Of Int32), _
                            ByVal shisakuListCode As String, _
                            ByVal shisakuListCodeKaiteiNo As String, _
                            ByVal shisakuGroupNo As String) As Integer
    End Interface
End Namespace