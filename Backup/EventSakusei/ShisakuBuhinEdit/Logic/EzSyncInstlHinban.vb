Imports EventSakusei.ShisakuBuhinEdit.Kosei.Logic.Matrix

Namespace ShisakuBuhinEdit.Logic
    ''' <summary>
    ''' INSTL�i�Ԋ֘A�œ�������点��ׂ̊ȒP��interface
    ''' </summary>
    ''' <remarks></remarks>
    Public Interface EzSyncInstlHinban
        ''' <summary>
        ''' INSTL�i�Ԃ��X�V���ꂽ����ʒm����
        ''' </summary>
        ''' <param name="columnIndex">��index</param>
        ''' <remarks></remarks>
        Sub NotifyInstlHinban(ByVal columnIndex As Integer)
        ''' <summary>
        ''' INSTL�i�ԋ敪���X�V���ꂽ����ʒm����
        ''' </summary>
        ''' <param name="columnIndex">��index</param>
        ''' <remarks></remarks>
        Sub NotifyInstlHinbanKbn(ByVal columnIndex As Integer)

        ''����2014/08/22 1)EBOM-�V�����z�V�X�e���ߋ��f�[�^�̑g�ݍ��킹���o ���� ADD BEGIN
        ''' <summary>
        ''' INSTL�f�[�^�敪���X�V���ꂽ����ʒm����
        ''' </summary>
        ''' <param name="columnIndex">��index</param>
        ''' <remarks></remarks>
        Sub NotifyInstlDataKbn(ByVal columnIndex As Integer)
        ''����2014/08/22 1)EBOM-�V�����z�V�X�e���ߋ��f�[�^�̑g�ݍ��킹���o ���� ADD END

        '����2014/08/15 �T.3.�݌v�ҏW �x�[�X���C��p��_t) (TES)�� ADD BEGIN
        ''' <summary>
        ''' �x�[�X���t���O���X�V���ꂽ����ʒm����
        ''' </summary>
        ''' <param name="columnIndex">��index</param>
        ''' <remarks></remarks>
        Sub NotifyBaseInstlFlg(ByVal columnIndex As Integer)
        ''����2014/08/15 �T.3.�݌v�ҏW �x�[�X���C��p��_t) (TES)�� ADD END

        ''' <summary>
        ''' INSTL�i�ԗ�ɗ�}������
        ''' </summary>
        ''' <param name="columnIndex">��index</param>
        ''' <param name="insertCount">��}����</param>
        ''' <remarks></remarks>
        Sub InsertColumnInInstl(ByVal columnIndex As Integer, ByVal insertCount As Integer)

        ''' <summary>
        ''' INSTL�i�ԗ���폜����
        ''' </summary>
        ''' <param name="columnIndex">��index</param>
        ''' <param name="removeCount">�폜��</param>
        ''' <remarks></remarks>
        Sub RemoveColumnInInstl(ByVal columnIndex As Integer, ByVal removeCount As Integer)

        ''' <summary>
        ''' INSTL�i�Ԃ��X�V���ꂽ����ʒm����(�C�x���g�i�ԃR�s�[)
        ''' </summary>
        ''' <param name="columnIndex">��index</param>
        ''' <remarks></remarks>
        Sub NotifyInstlHinbanEventCopy(ByVal columnIndex As Integer)

        ''' <summary>
        ''' �V����INSTL�i�Ԃō\�����쐬����(�C�x���g�i�ԃR�s�[)
        ''' </summary>
        ''' <param name="shisakuEventCode">�C�x���g�R�[�h</param>
        ''' <param name="shisakuBlockNo">�u���b�NNo</param>
        ''' <param name="kaiteiNo">����No�@  2014/08/04 �T.11.�����߂��@�\ i) (TES)�{ �ǉ� </param>
        ''' <remarks></remarks>
        Sub NotifyInstlHinbanGetKosei(ByVal shisakuEventCode As String, ByVal shisakuBlockNo As String, Optional ByVal KaiteiNo As String = "", Optional ByVal eventcopyflg As Boolean = False, Optional ByVal baseFlg As Boolean = False, Optional ByVal addStartIndex As Integer = 0)
        '����2014/10/23 ���� ADD BEGIN
        'Sub NotifyInstlHinbanGetKosei(ByVal shisakuEventCode As String, ByVal shisakuBlockNo As String, Optional ByVal KaiteiNo As String = "", Optional ByVal eventcopyflg As Boolean = False)
        '����2014/10/23 ���� ADD END
        '����2014/09/25 ���� ADD BEGIN
        '        Sub NotifyInstlHinbanGetKosei(ByVal shisakuEventCode As String, ByVal shisakuBlockNo As String, Optional ByVal KaiteiNo As String = "")
        '����2014/09/25 ���� ADD END



        ''����2014/08/01 �T.3.�݌v�ҏW �x�[�X�ԉ��C��p��_bl) (TES)�� ADD BEGIN
        ''' <summary>
        ''' ��p���i������ʂō\�����쐬����(�C�x���g�i�ԃR�s�[) 
        ''' </summary>
        ''' <param name="HoyouKoseiMatrix">��p���i������ʂ�spread�S�s</param>
        ''' <remarks></remarks>
        Sub NotifyHoyouGetKosei(ByVal HoyouKoseiMatrix As BuhinKoseiMatrix)
        ''����2014/08/01 �T.3.�݌v�ҏW �x�[�X�ԉ��C��p��_bl) (TES)�� ADD END

        ''' <summary>
        '''�o�b�N�A�b�v�}�g���N�X���擾
        ''' </summary>
        ''' <remarks></remarks>
        Sub SetBackUpKosei()

        Sub BakEvent()

    End Interface
End Namespace