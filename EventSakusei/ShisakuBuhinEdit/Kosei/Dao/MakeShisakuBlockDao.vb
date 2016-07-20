Imports ShisakuCommon.Db.EBom.Vo
Imports ShisakuCommon.Util.LabelValue

Namespace ShisakuBuhinEdit.Kosei.Dao
    Public Interface MakeShisakuBlockDao

        ''' <summary>
        ''' 試作部品情報のメーカー情報を返す
        ''' </summary>
        ''' <param name="ShisakuEventCode">試作イベントコード</param>
        ''' <param name="ShisakuBukaCode">試作部課コード</param>
        ''' <param name="ShisakuBlockNo">試作ブロックNo</param>
        ''' <param name="ShisakuBlockNoKaiteiNo">試作ブロックNo改訂No</param>
        ''' <returns>メーカー情報</returns>
        ''' <remarks></remarks>
        Function FindMakerByShisakuBuhin(ByVal ShisakuEventCode As String, ByVal ShisakuBukaCode As String, ByVal ShisakuBlockNo As String, ByVal ShisakuBlockNoKaiteiNo As String) As List(Of Rhac0610Vo)

        ''' <summary>
        ''' 自給品の有無を取得する
        ''' </summary>
        ''' <param name="ShisakuEventCode">試作イベントコード</param>
        ''' <returns>自給品の有無</returns>
        ''' <remarks></remarks>
        Function FindByJikyuUmu(ByVal ShisakuEventCode As String) As String

        ''↓↓2014/07/24 Ⅰ.3.設計編集 ベース改修専用化_g) (TES)張 ADD BEGIN
        ''' <summary>
        ''' 購入指示の有無を取得する
        ''' </summary>
        ''' <param name="ShisakuEventCode">試作イベントコード</param>
        ''' <returns>購入指示の有無</returns>
        ''' <remarks></remarks>
        Function FindByKounyuShiji(ByVal ShisakuEventCode As String) As String
        ''↑↑2014/07/24 Ⅰ.3.設計編集 ベース改修専用化_g) (TES)張 ADD END

        ''↓↓2014/07/23 Ⅰ.2.管理項目追加_ah) (TES)張 ADD BEGIN
        ''' <summary>
        ''' 作り方を取得する
        ''' </summary>
        ''' <param name="ShisakuEventCode">試作イベントコード</param>
        ''' <returns>作り方</returns>
        ''' <remarks></remarks>
        Function FindByTsukurikata(ByVal ShisakuEventCode As String) As String
        ''↑↑2014/07/23 Ⅰ.2.管理項目追加_ah) (TES)張 ADD END

        ''↓↓2014/07/23 Ⅰ.2.管理項目追加_bm) (TES)張 ADD BEGIN
        ''' <summary>
        ''' 作り方コンボ表示内容取得処理
        ''' </summary>
        ''' <returns>作り方</returns>
        ''' <remarks></remarks>
        Function FindTsukurikataSeisakuLabelValues() As List(Of LabelValueVo)
        ''↑↑2014/07/23 Ⅰ.2.管理項目追加_bm) (TES)張 ADD END

        ''↓↓2014/07/23 Ⅰ.2.管理項目追加_bs) (TES)張 ADD BEGIN
        ''' <summary>
        ''' 作り方コンボ表示内容取得処理
        ''' </summary>
        ''' <returns>作り方</returns>
        ''' <remarks></remarks>
        Function FindTsukurikataKatashiyou1LabelValues() As List(Of LabelValueVo)
        Function FindTsukurikataKatashiyou2LabelValues() As List(Of LabelValueVo)
        Function FindTsukurikataKatashiyou3LabelValues() As List(Of LabelValueVo)
        ''↑↑2014/07/23 Ⅰ.2.管理項目追加_bs) (TES)張 ADD END

        ''↓↓2014/07/23 Ⅰ.2.管理項目追加_bv) (TES)張 ADD BEGIN
        ''' <summary>
        ''' 作り方コンボ表示内容取得処理
        ''' </summary>
        ''' <returns>作り方</returns>
        ''' <remarks></remarks>
        Function FindTsukurikataTiguLabelValues() As List(Of LabelValueVo)
        ''↑↑2014/07/23 Ⅰ.2.管理項目追加_bv) (TES)張 ADD END

    End Interface
End Namespace