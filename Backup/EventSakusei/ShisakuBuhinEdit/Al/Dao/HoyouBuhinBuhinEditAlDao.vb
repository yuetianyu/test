Imports ShisakuCommon.Db.EBom.Vo

Namespace ShisakuBuhinEdit.Al.Dao
    ''' <summary>
    ''' A/L画面用のDao
    ''' </summary>
    ''' <remarks></remarks>
    Public Interface HoyouBuhinBuhinEditAlDao
        '''' <summary>
        '''' ベース車情報・完成車情報を参照する
        '''' </summary>
        '''' <param name="hoyouEventCode">補用イベントコード</param>
        '''' <returns>ベース車情報・完成車情報</returns>
        '''' <remarks></remarks>
        'Function FindEventInfoById(ByVal hoyouEventCode As String) As List(Of BuhinEditAlEventVo)


        '担当者呼び出し用'
        ''' <summary>
        ''' 担当者の情報を取得する
        ''' </summary>
        ''' <param name="hoyouEventCode">イベントコード</param>
        ''' <param name="hoyouBukaCode">部課コード</param>
        ''' <param name="hoyouTanto">担当者</param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Function FindByNewTanto(ByVal hoyouEventCode As String, ByVal hoyouBukaCode As String, ByVal hoyouTanto As String) As THoyouSekkeiTantoVo

        ''' <summary>
        ''' DBのタイムスタンプを取得
        ''' </summary>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Function FindByDBTimeStamp(ByVal hoyouEventCode As String) As THoyouSekkeiTantoVo


        ''' <summary>
        ''' 試作設計ブロックの最終更新日を確認する
        ''' </summary>
        ''' <param name="hoyouEventCode"></param>
        ''' <param name="hoyouBukaCode"></param>
        ''' <param name="hoyouTanto"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Function FindByLastModifyDateTimeOfSekkeiTanto(ByVal hoyouEventCode As String, ByVal hoyouBukaCode As String, ByVal hoyouTanto As String, ByVal openDateTime As String) As THoyouSekkeiTantoVo


    End Interface
End Namespace