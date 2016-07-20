Imports ShisakuCommon.Db.EBom.Vo

Namespace ShisakuBuhinEdit.Ikkatsu.Dao
    ''' <summary>
    ''' 部品構成呼出（一括設定）専用のDAO
    ''' </summary>
    ''' <remarks></remarks>
    Public Interface BuhinEditIkkatsuDao
        ''' <summary>
        ''' INSTL品番に紐付く試作設計ブロックINSTL情報を取得する
        ''' </summary>
        ''' <param name="instlHinban">INSTL品番</param>
        ''' <param name="shisakuEventCode">除外する試作イベントコード</param>
        ''' <param name="shisakuBukaCode">除外する試作部課コード</param>
        ''' <param name="shisakuBlockNo">除外する試作ブロックNo</param>
        ''' <param name="shisakuBlockNoKaiteiNo">除外する試作ブロックNo改訂No</param>
        ''' <returns>該当データ</returns>
        ''' <remarks>同一区分が複数該当する場合、最終更新日時のデータを取得</remarks>
        Function FindLatestInstlHinbanKbnBy(ByVal instlHinban As String, ByVal shisakuEventCode As String, ByVal shisakuBukaCode As String, ByVal shisakuBlockNo As String, ByVal shisakuBlockNoKaiteiNo As String) As List(Of TShisakuSekkeiBlockInstlVo)
        ''' <summary>
        ''' 試作イベント名を返す
        ''' </summary>
        ''' <param name="shisakuEventCode"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Function FindShisakuEventName(ByVal shisakuEventCode As String) As TShisakuEventVo

        ''' <summary>
        ''' 部品番号を元に553で利用している開発符号を返す
        ''' </summary>
        ''' <param name="instlHinban"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Function FindKaihatsuFugoOf553ByInstlHinban(ByVal instlHinban As String) As List(Of TShisakuEventVo)

        ''' <summary>
        ''' 同一の部品番号、試作区分を持つ試作イベントを設計ブロックINSTLから取得し返す
        ''' </summary>
        ''' <param name="shisakuEventCode"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Function FindShisakuEventByInstlHinbanAndKbn(ByVal instlHinban As String, ByVal shisakuEventCode As String, ByVal shisakuBukaCode As String, ByVal shisakuBlockNo As String, ByVal shisakuBlockNoKaiteiNo As String, ByVal instlHinbanKbn As String) As List(Of TShisakuSekkeiBlockInstlVo)

        ''' <summary>
        ''' イベントコード以外のINSTL品番を取得する
        ''' </summary>
        ''' <param name="shisakuEventCode">イベントコード</param>
        ''' <param name="instlHinban">INSTL品番</param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Function FindBySekkeiBlockInstlKbn(ByVal shisakuEventCode As String, ByVal instlHinban As String) As List(Of TShisakuSekkeiBlockInstlVo)

    End Interface
End Namespace