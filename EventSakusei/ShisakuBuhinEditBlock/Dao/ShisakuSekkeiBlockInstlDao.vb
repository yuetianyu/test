Imports ShisakuCommon.Db.EBom.Vo
Namespace ShisakuBuhinEditBlock.Dao
    Public Interface ShisakuSekkeiBlockInstlDao

        ''' <summary>
        ''' ����݌v�u���b�NINSTL�����擾
        ''' </summary>
        ''' <param name="shisakuEventCode">����C�x���g�R�[�h</param>
        ''' <param name="shisakuBukaCode">���암�ۃR�[�h</param>
        ''' <param name="shisakuBlockNo">����u���b�NNo</param>
        ''' <param name="shisakuBlockNoKaiteiNo">����u���b�N��������</param>
        ''' <param name="instlHinban">INSTL�i��</param>
        ''' <param name="instlHinbanKbn">INSTL�i�Ԏ���敪</param>
        ''' <param name="shisakuGousya">���썆��</param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Function FindShisakuSekkeiBlockInstl(ByVal shisakuEventCode As String, ByVal shisakuBukaCode As String, ByVal shisakuBlockNo As String, ByVal shisakuBlockNoKaiteiNo As String, ByVal instlHinban As String, ByVal instlHinbanKbn As String, ByVal shisakuGousya As String) As TShisakuSekkeiBlockInstlVo

    End Interface
End Namespace