Namespace Db
    ''' <summary>
    ''' 1�e�[�u���̂��߂�CRUD��񋟂���DAO
    ''' </summary>
    ''' <typeparam name="T">�e�[�u���ɑΉ�����VO</typeparam>
    ''' <remarks>�����N���X�́A[�e�[�u����]��"Dao" �Ƃ��������K���ɏ]����</remarks>
    Public Interface DaoEachTable(Of T)
        ''' <summary>
        ''' �e�[�u���l��S���擾����
        ''' </summary>
        ''' <returns>���ʂ�v�f�̓����v���p�e�B�ɕێ�����List</returns>
        ''' <remarks></remarks>
        Function FindByAll() As List(Of T)

        ''' <summary>
        ''' �e�[�u���l�̌������ʂ�Ԃ�
        ''' </summary>
        ''' <param name="clause">��������</param>
        ''' <returns>���ʂ�v�f�̓����v���p�e�B�ɕێ�����List</returns>
        ''' <remarks></remarks>
        Function FindBy(ByVal clause As T) As List(Of T)

        ''' <summary>
        ''' �Y�����錏����Ԃ�
        ''' </summary>
        ''' <param name="clause">��������</param>
        ''' <returns>����</returns>
        ''' <remarks></remarks>
        Function CountBy(ByVal clause As T) As Integer

        ''' <summary>
        ''' ���R�[�h��ǉ�����
        ''' </summary>
        ''' <param name="value">�ǉ�����l</param>
        ''' <returns>�ǉ���������</returns>
        ''' <remarks></remarks>
        Function InsertBy(ByVal value As T) As Integer

        ''' <summary>
        ''' �Y�����R�[�h���X�V����
        ''' </summary>
        ''' <param name="clauseValue">���������iPK���ځj�ƁA�X�V�l�i���̑����ځj</param>
        ''' <returns>�X�V����</returns>
        ''' <remarks></remarks>
        Function UpdateByPk(ByVal clauseValue As T) As Integer

        ''' <summary>
        ''' ���R�[�h���폜����
        ''' </summary>
        ''' <param name="clause">�폜����</param>
        ''' <returns>�폜��������</returns>
        ''' <remarks></remarks>
        Function DeleteBy(ByVal clause As T) As Integer

        ''' <summary>
        ''' �X�V���b�N��ݒ肷��
        ''' </summary>
        ''' <param name="forUpdate">�X�V���b�N������ꍇ�Atrue</param>
        ''' <remarks></remarks>
        Sub SetForUpdate(ByVal ForUpdate As Boolean)
        ''' <summary>
        ''' PrimaryKey��VO���쐬���ĕԂ�
        ''' </summary>
        ''' <param name="values">PrimaryKey���\������l</param>
        ''' <returns>VO</returns>
        ''' <remarks></remarks>
        Function MakePkVo(ByVal ParamArray values() As Object) As T
    End Interface
End Namespace