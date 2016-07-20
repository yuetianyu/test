Imports ShisakuCommon.Db.EBom.Vo
Namespace Db.EBom.Dao
    Public Interface AsARPF04Dao
        ''' <summary>
        ''' テーブル値の検索結果を返す
        ''' </summary>
        ''' <param name="BUKbn">B/U区分</param>
        ''' <param name="Kgba">記号ID</param>
        ''' <param name="Kgkm">記号区分</param>
        ''' <param name="Tikg">手配記号</param>
        ''' <returns>該当レコード</returns>
        ''' <remarks></remarks>
        Function FindByPk(ByVal BUKbn As Char, _
                          ByVal Kgba As String, _
                          ByVal Kgkm As Char, _
                          ByVal Tikg As String) As AsARPF04Vo

        ''' <summary>
        ''' 該当レコードを削除する
        ''' </summary>
        ''' <param name="BUKbn">B/U区分</param>
        ''' <param name="Kgba">記号ID</param>
        ''' <param name="Kgkm">記号区分</param>
        ''' <param name="Tikg">手配記号</param>
        ''' <returns>削除件数</returns>
        ''' <remarks></remarks>
        Function DeleteByPk(ByVal BUKbn As Char, _
                            ByVal Kgba As String, _
                            ByVal Kgkm As Char, _
                            ByVal Tikg As String) As Integer

    End Interface
End Namespace