Imports ShisakuCommon.Db

Namespace Soubi

    ''' <summary>
    ''' �C�x���g�������p��Dao
    ''' </summary>
    ''' <remarks></remarks>
    Public Interface EventSoubiDao : Inherits DaoEachFeature
        ''' <summary>
        ''' �C�x���g�������̑������^�C�g�����݂̂�Ԃ�
        ''' </summary>
        ''' <param name="shisakuEventCode">����C�x���g�R�[�h</param>
        ''' <param name="shisakuSoubiKbn">���쑕���敪</param>
        ''' <returns>�������^�C�g�����݂̂̃��R�[�h</returns>
        ''' <remarks></remarks>
        Function FindWithTitleNameBy(ByVal shisakuEventCode As String, Optional ByVal shisakuSoubiKbn As String = Nothing) As List(Of TShisakuEventSoubiNameVo)
        ''' <summary>
        ''' �C�x���g�������ɑ�������t���ĕԂ�
        ''' </summary>
        ''' <param name="shisakuEventCode">����C�x���g�R�[�h</param>
        ''' <param name="shisakuSoubiKbn">���쑕���敪</param>
        ''' <returns>�������t���̃��R�[�h</returns>
        ''' <remarks></remarks>
        Function FindWithNameBy(ByVal shisakuEventCode As String, Optional ByVal shisakuSoubiKbn As String = Nothing) As List(Of TShisakuEventSoubiNameVo)

        ''' <summary>
        ''' �C�x���g�������̑������^�C�g�����݂̂�Ԃ�
        '''�@�@�\�d�l�\���I����ʗp�Ɏg�p
        ''' </summary>
        ''' <param name="shisakuEventCode">����C�x���g�R�[�h</param>
        ''' <param name="shisakuSoubiKbn">���쑕���敪</param>
        ''' <returns>�������^�C�g�����݂̂̃��R�[�h</returns>
        ''' <remarks></remarks>
        Function FindWithTitleNameBySoubi(ByVal shisakuEventCode As String, Optional ByVal shisakuSoubiKbn As String = Nothing) As List(Of TShisakuEventSoubiNameVo)
        ''' <summary>
        ''' �C�x���g�������ɑ�������t���ĕԂ�
        '''�@�@�\�d�l�\���I����ʗp�Ɏg�p
        ''' </summary>
        ''' <param name="shisakuEventCode">����C�x���g�R�[�h</param>
        ''' <param name="shisakuSoubiKbn">���쑕���敪</param>
        ''' <returns>�������t���̃��R�[�h</returns>
        ''' <remarks></remarks>
        Function FindWithNameBySoubi(ByVal shisakuEventCode As String, Optional ByVal shisakuSoubiKbn As String = Nothing) As List(Of TShisakuEventSoubiNameVo)

    End Interface
End Namespace