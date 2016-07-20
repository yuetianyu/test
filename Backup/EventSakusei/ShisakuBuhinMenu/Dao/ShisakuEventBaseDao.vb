Imports ShisakuCommon.Db.EBom.Vo
Namespace ShisakuBuhinMenu.Dao
    Public Interface ShisakuEventBaseDao

        '2012/01/12
        ''' <summary>
        ''' �x�[�X�ԏ����擾
        ''' </summary>
        ''' <param name="shisakuEventCode">����C�x���g�R�[�h</param>
        ''' <returns>�x�[�X�ԏ��</returns>
        ''' <remarks></remarks>
        Function FindShisakuEventBase(ByVal shisakuEventCode As String) As List(Of TShisakuEventBaseVo)
        '����2014/09/24 ���� ADD BEGIN
        Function FindShisakuEventEbomKanshi(ByVal shisakuEventCode As String) As List(Of TShisakuEventEbomKanshiVo)
        '����2014/09/24 ���� ADD END
        '2012/02/16
        ''' <summary>
        ''' �x�[�X�Ԃ̊J���������擾
        ''' </summary>
        ''' <param name="shisakuEventCode">����C�x���g�R�[�h</param>
        ''' <param name="shisakuGousya">���썆��</param>
        ''' <returns>�x�[�X�ԏ��</returns>
        ''' <remarks>�ʏ�̃x�[�X�Ԃ̍��Ԃ���J���������擾����</remarks>
        Function FindShisakuEventBaseByEventCodeAndGousyaForShisakuGousya(ByVal shisakuEventCode As String, ByVal shisakuGousya As String) As TShisakuEventBaseVo
        '����2014/09/24 ���� ADD BEGIN
        Function FindShisakuEventEbomKanshiByEventCodeAndGousyaForShisakuGousya(ByVal shisakuEventCode As String, ByVal shisakuGousya As String) As TShisakuEventEbomKanshiVo
        '����2014/09/24 ���� ADD END
        '2012/02/16
        ''' <summary>
        ''' �x�[�X�ԏ����J���������擾
        ''' </summary>
        ''' <param name="shisakuEventCode">����C�x���g�R�[�h</param>
        ''' <param name="shisakuGousya">���썆��</param>
        ''' <returns>�x�[�X�ԏ��</returns>
        ''' <remarks>���쑤�f�[�^�̃x�[�X�ƂȂ鍆�Ԃ���J���������擾����</remarks>
        Function FindShisakuEventBaseByEventCodeAndGousyaForShisakuBaseGousya(ByVal shisakuEventCode As String, ByVal shisakuGousya As String) As TShisakuEventBaseVo
        '����2014/09/24 ���� ADD BEGIN
        Function FindShisakuEventEbomKanshiByEventCodeAndGousyaForShisakuBaseGousya(ByVal shisakuEventCode As String, ByVal shisakuGousya As String) As TShisakuEventEbomKanshiVo
        '����2014/09/24 ���� ADD END

        ''' <summary>
        ''' �x�[�X�ԊJ���������擾
        ''' </summary>
        ''' <param name="buhinNo">���iNo</param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Function FindBaseKaihatsuFugo(ByVal buhinNo As String) As List(Of TShisakuEventBaseVo)

    End Interface
End Namespace