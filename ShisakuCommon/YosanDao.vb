Imports ShisakuCommon.Db.EBom.Vo

Namespace Db
    ''' <summary>
    ''' �\�Z���V�X�e���ɂƂ��ċ��ʓI��Dao
    ''' </summary>
    ''' <remarks></remarks>
    Public Interface YosanDao : Inherits DaoEachFeature
        ''' <summary>
        ''' DB�̓�����Ԃ�
        ''' </summary>
        ''' <returns>����</returns>
        ''' <remarks></remarks>
        Function GetCurrentTimestamp() As DateTime

        ''' <summary>
        ''' EBom���[�U�[����Ԃ�
        ''' </summary>
        ''' <param name="userId">���[�U�[ID</param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Function FindUserById(ByVal userId As String) As Rhac0650Vo
    End Interface
End Namespace