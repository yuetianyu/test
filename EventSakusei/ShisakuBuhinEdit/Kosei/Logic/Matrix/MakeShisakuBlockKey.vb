Imports EventSakusei.ShisakuBuhinEdit.Kosei.Logic.Merge

Namespace ShisakuBuhinEdit.Kosei.Logic.Matrix
    ''' <summary>
    ''' ���암�i�\�����Ǝ��암�i���Ƃŕ��i�\���쐬���郁�\�b�h�N���X
    ''' </summary>
    ''' <remarks></remarks>
    Public Interface MakeShisakuBlockKey

        ''' <summary>
        ''' ���암�i�\�����Ǝ��암�i���Ƃŕ��i�\���쐬����
        ''' </summary>
        ''' <param name="shisakuEventCode">����C�x���g�R�[�h</param>
        ''' <param name="shisakuBukaCode">���암�ۃR�[�h</param>
        ''' <param name="shisakuBlockNo">����u���b�NNo</param>
        ''' <param name="shisakuBlockNoKaiteiNo">����u���b�NNo����No</param>
        ''' <returns>���i�\</returns>
        ''' <remarks></remarks>
        Function Compute(ByVal shisakuEventCode As String, ByVal shisakuBukaCode As String, ByVal shisakuBlockNo As String, ByVal shisakuBlockNoKaiteiNo As String) As BuhinKoseiMatrix
    End Interface
End Namespace