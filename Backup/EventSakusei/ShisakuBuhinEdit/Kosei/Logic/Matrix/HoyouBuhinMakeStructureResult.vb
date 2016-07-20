Imports EventSakusei.ShisakuBuhinEdit.Logic.Detect
Imports EventSakusei.ShisakuBuhinEdit.Kosei.Logic.Merge

Namespace ShisakuBuhinEdit.Kosei.Logic.Matrix
    ''' <summary>
    ''' �u�\���̏��v�ŕ��i�\���쐬���郁�\�b�h�N���X
    ''' </summary>
    ''' <remarks></remarks>
    Public Interface HoyouBuhinMakeStructureResult

        '2012/01/16 �����ǉ�

        ''' <summary>
        ''' �u�\���̏��v�����ɕ��i�\���쐬����
        ''' </summary>
        ''' <param name="aStructureResult">�\���̏��</param>
        ''' <returns>���i�\</returns>
        ''' <remarks></remarks>
        Function Compute(ByVal aStructureResult As HoyouBuhinStructureResult, _
                         ByVal baseLevel As Integer?, _
                         ByVal kaihatsuFugo As String) As HoyouBuhinBuhinKoseiMatrix

        ''' <summary>
        ''' �u�\���̏��v�����ɕ��i�\���쐬����(�����i�L��)
        ''' </summary>
        ''' <param name="aStructureResult">�\���̏��</param>
        ''' <param name="a0553Flag">�ǂ̑��삩�痈���̂� 0:�݌v�W�J,1:�\���ēW�J�A�ŐV���A���i�\���Ăяo���A2:�q���i�Ăяo��</param>
        ''' <param name="baseLevel">��_�̃��x��</param>
        ''' <returns>���i�\</returns>
        ''' <remarks></remarks>
        Function Compute(ByVal aStructureResult As HoyouBuhinStructureResult, _
                         ByVal a0553Flag As Integer, _
                         ByVal baseLevel As Integer?, _
                         ByVal kaihatsuFugo As String) As HoyouBuhinBuhinKoseiMatrix

        '''' <summary>
        '''' �u�\���̏��v�����ɕ��i�\���쐬����(�\���ēW�J�A�ŐV���A�ꊇ�\���Ăяo����)
        '''' </summary>
        '''' <param name="aStructureResult">�\���̏��</param>
        '''' <param name="a0553Flag">�ǂ̑��삩�痈���̂� 0:�݌v�W�J,1:�\���ēW�J�A�ŐV���A���i�\���Ăяo���A2:�q���i�Ăяo��</param>
        '''' <param name="baseLevel">��_�̃��x��</param>
        '''' <returns>���i�\</returns>
        '''' <remarks></remarks>
        'Function ComputeKosei(ByVal aStructureResult As HoyouBuhinStructureResult, ByVal a0553Flag As Integer, ByVal baseLevel As Integer?) As HoyouBuhinBuhinKoseiMatrix

        Function Compute2(ByVal aStructureResult As HoyouBuhinStructureResult, ByVal a0553Flag As Integer, ByVal baseLevel As Integer?) As HoyouBuhinBuhinKoseiMatrix

        Sub KaihatsuFugoSet(ByVal KaihatsuFugo As String)

        ''' <summary>
        ''' INSTL�����͂��ꂽ�Ƃ��ɓ������擾����
        ''' </summary>
        ''' <param name="aStructureResult"></param>
        ''' <param name="a0553Flag"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Function GetBuhinKosei(ByVal aStructureResult As HoyouBuhinStructureResult, ByVal a0553Flag As Integer) As HoyouBuhinBuhinKoseiMatrix

    End Interface
End NameSpace