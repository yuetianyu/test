Namespace Db
    ''' <summary>
    ''' 1テーブルのためのCRUDを提供するDAO
    ''' </summary>
    ''' <typeparam name="T">テーブルに対応するVO</typeparam>
    ''' <remarks>実装クラスは、[テーブル名]＆"Dao" という命名規則に従う事</remarks>
    Public Interface DaoEachTable(Of T)
        ''' <summary>
        ''' テーブル値を全件取得する
        ''' </summary>
        ''' <returns>結果を要素の同名プロパティに保持したList</returns>
        ''' <remarks></remarks>
        Function FindByAll() As List(Of T)

        ''' <summary>
        ''' テーブル値の検索結果を返す
        ''' </summary>
        ''' <param name="clause">検索条件</param>
        ''' <returns>結果を要素の同名プロパティに保持したList</returns>
        ''' <remarks></remarks>
        Function FindBy(ByVal clause As T) As List(Of T)

        ''' <summary>
        ''' 該当する件数を返す
        ''' </summary>
        ''' <param name="clause">検索条件</param>
        ''' <returns>件数</returns>
        ''' <remarks></remarks>
        Function CountBy(ByVal clause As T) As Integer

        ''' <summary>
        ''' レコードを追加する
        ''' </summary>
        ''' <param name="value">追加する値</param>
        ''' <returns>追加した件数</returns>
        ''' <remarks></remarks>
        Function InsertBy(ByVal value As T) As Integer

        ''' <summary>
        ''' 該当レコードを更新する
        ''' </summary>
        ''' <param name="clauseValue">検索条件（PK項目）と、更新値（その他項目）</param>
        ''' <returns>更新件数</returns>
        ''' <remarks></remarks>
        Function UpdateByPk(ByVal clauseValue As T) As Integer

        ''' <summary>
        ''' レコードを削除する
        ''' </summary>
        ''' <param name="clause">削除条件</param>
        ''' <returns>削除した件数</returns>
        ''' <remarks></remarks>
        Function DeleteBy(ByVal clause As T) As Integer

        ''' <summary>
        ''' 更新ロックを設定する
        ''' </summary>
        ''' <param name="forUpdate">更新ロックをする場合、true</param>
        ''' <remarks></remarks>
        Sub SetForUpdate(ByVal ForUpdate As Boolean)
        ''' <summary>
        ''' PrimaryKeyのVOを作成して返す
        ''' </summary>
        ''' <param name="values">PrimaryKeyを構成する値</param>
        ''' <returns>VO</returns>
        ''' <remarks></remarks>
        Function MakePkVo(ByVal ParamArray values() As Object) As T
    End Interface
End Namespace