Imports ShisakuCommon.Db.EBom.Vo

''����2014/08/21 1 �x�[�X���i�\�쐬�\�@�\���� ADD 
Namespace ShisakuBuhinMenu.Dao
    Public Interface ShisakuBlockSekkeikaTmpDao

        ''' <summary>
        ''' Delete
        ''' </summary>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Function DeleteByEventCode(ByVal eventCode As String) As Integer
        '����2014/10/24 ���� ADD BEGIN
        Function FindByAll4Ebom(ByVal eventCode As String) As List(Of ShisakuBlockSekkeikaTmp4EbomVo)
        '����2014/10/24 ���� ADD END
    End Interface
End Namespace