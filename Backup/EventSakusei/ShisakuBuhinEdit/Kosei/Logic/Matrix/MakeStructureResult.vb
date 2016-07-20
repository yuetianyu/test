Imports EventSakusei.ShisakuBuhinEdit.Logic.Detect
Imports EventSakusei.ShisakuBuhinEdit.Kosei.Logic.Merge

Namespace ShisakuBuhinEdit.Kosei.Logic.Matrix
    ''' <summary>
    ''' �u�\���̏��v�ŕ��i�\���쐬���郁�\�b�h�N���X
    ''' </summary>
    ''' <remarks></remarks>
    Public Interface MakeStructureResult

        '2012/01/16 �����ǉ�

        ''' <summary>
        ''' �u�\���̏��v�����ɕ��i�\���쐬����
        ''' </summary>
        ''' <param name="aStructureResult">�\���̏��</param>
        ''' <returns>���i�\</returns>
        ''' <param name="JikyuUmu">�����i�̗L��</param>
        ''' <remarks></remarks>
        Function Compute(ByVal aStructureResult As StructureResult, ByVal JikyuUmu As String) As BuhinKoseiMatrix
        ''' <summary>
        ''' �u�\���̏��v�����ɕ��i�\���쐬����(�����i�L��)
        ''' </summary>
        ''' <param name="aStructureResult">�\���̏��</param>
        ''' <param name="a0553Flag">�ǂ̑��삩�痈���̂� 0:�݌v�W�J,1:�\���ēW�J�A�ŐV���A���i�\���Ăяo���A2:�q���i�Ăяo��</param>
        ''' <param name="baseLevel">��_�̃��x��</param>
        ''' <param name="kaiteiNo">����No�@  2014/08/04 �T.11.�����߂��@�\ ��) (TES)�{ �ǉ� </param>
        ''' <returns>���i�\</returns>
        ''' <remarks></remarks>
        Function Compute(ByVal aStructureResult As StructureResult, ByVal a0553Flag As Integer, ByVal baseLevel As Integer?, Optional ByVal KaiteiNo As String = "", Optional ByVal aInstlDataKbn As String = "", Optional ByVal aBaseInstlFlg As String = "", Optional ByVal eventCopyFlg As Boolean = False, Optional ByVal yakanflg As Boolean = False) As BuhinKoseiMatrix
        '����2014/09/25 ���� ADD BEGIN
        '        Function Compute(ByVal aStructureResult As StructureResult, ByVal a0553Flag As Integer, ByVal baseLevel As Integer?, Optional ByVal KaiteiNo As String = "", Optional ByVal aInstlDataKbn As String = "", Optional ByVal aBaseInstlFlg As String = "") As BuhinKoseiMatrix
        '����2014/09/25 ���� ADD END
        '2014/09/23 ���� ADD BEGIN
        'Function Compute(ByVal aStructureResult As StructureResult, ByVal a0553Flag As Integer, ByVal baseLevel As Integer?, Optional ByVal KaiteiNo As String = "") As BuhinKoseiMatrix
        '2014/09/23 ���� ADD END

        ''' <summary>
        ''' �u�\���̏��v�����ɕ��i�\���쐬����(�\���ēW�J�A�ŐV���A�ꊇ�\���Ăяo����)
        ''' </summary>
        ''' <param name="aStructureResult">�\���̏��</param>
        ''' <param name="a0553Flag">�ǂ̑��삩�痈���̂� 0:�݌v�W�J,1:�\���ēW�J�A�ŐV���A���i�\���Ăяo���A2:�q���i�Ăяo��</param>
        ''' <param name="baseLevel">��_�̃��x��</param>
        ''' <returns>���i�\</returns>
        ''' <remarks></remarks>
        Function ComputeKosei(ByVal aStructureResult As StructureResult, ByVal a0553Flag As Integer, ByVal baseLevel As Integer?) As BuhinKoseiMatrix

        Function Compute2(ByVal aStructureResult As StructureResult, ByVal a0553Flag As Integer, ByVal baseLevel As Integer?) As BuhinKoseiMatrix

        Sub KaihatsuFugoSet(ByVal KaihatsuFugo As String)

        ''' <summary>
        ''' INSTL�����͂��ꂽ�Ƃ��ɓ������擾����
        ''' </summary>
        ''' <param name="aStructureResult"></param>
        ''' <param name="a0553Flag"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Function GetBuhinKosei(ByVal aStructureResult As StructureResult, ByVal a0553Flag As Integer) As BuhinKoseiMatrix

    End Interface
End NameSpace