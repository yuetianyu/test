Imports ShisakuCommon.Db.EBom.Vo

Namespace ShisakuBuhinEdit.Al.Dao
    ''' <summary>
    ''' A/L画面用のDao
    ''' </summary>
    ''' <remarks></remarks>
    Public Interface BuhinEditAlDao
        ''' <summary>
        ''' ベース車情報・完成車情報を参照する
        ''' </summary>
        ''' <param name="shisakuEventCode">試作イベントコード</param>
        ''' <returns>ベース車情報・完成車情報</returns>
        ''' <remarks></remarks>
        Function FindEventInfoById(ByVal shisakuEventCode As String) As List(Of BuhinEditAlEventVo)


        'ブロック呼び出し用'
        ''' <summary>
        ''' ブロックNoの情報を取得する
        ''' </summary>
        ''' <param name="shisakuEventCode">イベントコード</param>
        ''' <param name="shisakuBukaCode">部課コード</param>
        ''' <param name="shisakuBlockNo">ブロックNo</param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Function FindByNewBlockNo(ByVal shisakuEventCode As String, ByVal shisakuBukaCode As String, ByVal shisakuBlockNo As String) As TShisakuSekkeiBlockVo

        ''' <summary>
        ''' DBのタイムスタンプを取得
        ''' </summary>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Function FindByDBTimeStamp(ByVal shisakuEventCode As String) As TShisakuSekkeiBlockVo


        ''' <summary>
        ''' 試作設計ブロックの最終更新日を確認する
        ''' </summary>
        ''' <param name="shisakuEventCode"></param>
        ''' <param name="shisakuBukaCode"></param>
        ''' <param name="shisakuBlockNo"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Function FindByLastModifyDateTimeOfSekkeiBlock(ByVal shisakuEventCode As String, ByVal shisakuBukaCode As String, ByVal shisakuBlockNo As String, ByVal openDateTime As String) As TShisakuSekkeiBlockVo


    End Interface
End Namespace