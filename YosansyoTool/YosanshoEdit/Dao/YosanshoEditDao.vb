Imports ShisakuCommon.Db.EBom.Vo

Namespace YosanshoEdit.Dao

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <remarks></remarks>
    Public Interface YosanshoEditDao

        ''' <summary>
        ''' 予算書イベント別製作台数情報取得
        ''' </summary>
        ''' <param name="yosanEventCode">予算書イベントコード</param>
        ''' <returns>予算書イベント別製作台数情報</returns>
        ''' <remarks></remarks>
        Function FindYosanSeisakuDaisuBy(ByVal yosanEventCode As String) As List(Of TYosanSeisakuDaisuVo)

        ''' <summary>
        ''' 予算書イベント別金材情報取得
        ''' </summary>
        ''' <param name="yosanEventCode">予算書イベントコード</param>
        ''' <returns>予算書イベント別金材情報</returns>
        ''' <remarks></remarks>
        Function FindYosanKanazaiBy(ByVal yosanEventCode As String) As List(Of TYosanKanazaiVo)

        ''' <summary>
        ''' 予算書部品編集ﾊﾟﾀｰﾝ情報取得
        ''' </summary>
        ''' <param name="yosanEventCode">予算書イベントコード</param>
        ''' <returns>予算書部品編集ﾊﾟﾀｰﾝ情報</returns>
        ''' <remarks></remarks>
        Function FindYosanBuhinEditPatternBy(ByVal yosanEventCode As String) As List(Of TYosanBuhinEditPatternVo)

        ''' <summary>
        ''' ﾊﾟﾀｰﾝ情報取得
        ''' </summary>
        ''' <param name="yosanEventCode">予算書イベントコード</param>
        ''' <returns>ﾊﾟﾀｰﾝ情報</returns>
        ''' <remarks></remarks>
        Function FindPatternNameBy(ByVal yosanEventCode As String) As List(Of TYosanBuhinEditPatternVo)

        ''' <summary>
        ''' 予算書イベント別造り方情報取得
        ''' </summary>
        ''' <param name="yosanEventCode">予算書イベントコード</param>
        ''' <param name="shisakuSyubetu">試作種別</param>
        ''' <returns>予算書イベント別造り方情報</returns>
        ''' <remarks></remarks>
        Function FindYosanTukurikataBy(ByVal yosanEventCode As String, ByVal shisakuSyubetu As String) As List(Of TYosanTukurikataVo)

        ''' <summary>
        ''' 予算書イベント別年月別財務実績情報取得
        ''' </summary>
        ''' <param name="yosanCode">予算書コード</param>
        ''' <returns>予算書イベント別年月別財務実績情報</returns>
        ''' <remarks></remarks>
        Function FindYosanZaimuJisekiBy(ByVal yosanCode As String) As List(Of TYosanZaimuJisekiVo)

        ''' <summary>
        ''' 予予算書イベント別見通情報取得
        ''' </summary>
        ''' <param name="yosanEventCode">予算書イベントコード</param>
        ''' <returns>予算書イベント別見通情報</returns>
        ''' <remarks></remarks>
        Function FindYosanEventMitoshiBy(ByVal yosanEventCode As String) As List(Of TYosanEventMitoshiVo)

        ''' <summary>
        ''' 購入品予算進度管理表取得
        ''' </summary>
        ''' <param name="kojishireiNo">工事指令№</param>
        ''' <param name="unitKbn">ユニット</param>
        ''' <returns>予算書イベント別部品費</returns>
        ''' <remarks></remarks>
        Function FindSeisakuAsKounyuYosanBy(ByVal kojishireiNo As String, ByVal unitKbn As String) As List(Of SeisakuAsKounyuYosanVo)

        ''' <summary>
        ''' 購入品予算進度管理表取得
        ''' </summary>
        ''' <param name="kojishireiNo">工事指令№</param>
        ''' <param name="isMax">最大の年月かどうか</param>
        ''' <returns>予算書イベント別部品費</returns>
        ''' <remarks></remarks>
        Function FindSeisakuAsKounyuYosanBy(ByVal kojishireiNo As String, ByVal isMax As Boolean) As String

        ''' <summary>
        ''' 予算書イベント別年月別財務実績情報の最大または最小の財務実績計上年月取得
        ''' </summary>
        ''' <param name="yosanCode">予算書コード</param>
        ''' <returns>財務実績計上年月</returns>
        ''' <remarks></remarks>
        Function FindYosanZaimuJisekiYyyyMmBy(ByVal yosanCode As String, ByVal isMax As Boolean) As String

        ''' <summary>
        ''' 予算書部品表選択情報取得
        ''' </summary>
        ''' <param name="yosanEventCode">予算書イベントコード</param>
        ''' <returns>予算書部品表選択情報</returns>
        ''' <remarks></remarks>
        Function FindBuhinhyoNameBy(ByVal yosanEventCode As String) As List(Of TYosanBuhinSelectVo)

#Region "追加"

        Function FindYosanTukurikataListBy(ByVal yosanEventCode As String) As List(Of TYosanTukurikataVo)

#End Region


    End Interface

End Namespace