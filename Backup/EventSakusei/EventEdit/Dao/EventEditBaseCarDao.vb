Imports ShisakuCommon.Db
Imports ShisakuCommon.Util.LabelValue
Imports ShisakuCommon.Db.EBom.Vo

Namespace EventEdit.Dao
    Public Interface EventEditBaseCarDao : Inherits DaoEachFeature
        ''' <summary>
        ''' 開発符号のLabelValue一覧を返す
        ''' </summary>
        ''' <returns>一覧</returns>
        ''' <remarks></remarks>
        Function FindKaihatsuFugoLabelValues() As List(Of LabelValueVo)
        ''' <summary>
        ''' 仕様書一連NoのLabelValue一覧を返す
        ''' </summary>
        ''' <param name="kaihatsuFugo">開発符号</param>
        ''' <returns>一覧</returns>
        ''' <remarks></remarks>
        Function FindShiyoshoSeqnoLabelValues(ByVal kaihatsuFugo As String) As List(Of LabelValueVo)

        ''' <summary>
        ''' アプライドNoのLabelValue一覧を返す
        ''' </summary>
        ''' <param name="kaihatsuFugo">開発符号</param>
        ''' <param name="shiyoshoSeqno">仕様書一連No</param>
        ''' <returns>アプライドNoの一覧</returns>
        ''' <remarks></remarks>
        Function FindAppliedNoLabelValues(ByVal kaihatsuFugo As String, _
                                          ByVal shiyoshoSeqno As String) As List(Of LabelValueVo)
        ''' <summary>
        ''' ７桁開発符号を取得する
        ''' </summary>
        ''' <param name="kaihatsuFugo">開発符号</param>
        ''' <param name="shiyoshoSeqno">仕様書一連No</param>
        ''' <param name="appliedNo">アプライドNo</param>
        ''' <returns>７桁開発符号の一覧</returns>
        ''' <remarks></remarks>
        Function FindKatashikiFugo7LabelValues(ByVal kaihatsuFugo As String, _
                                               ByVal shiyoshoSeqno As String, _
                                               ByVal appliedNo As String) As List(Of LabelValueVo)

        ''' <summary>
        ''' 仕向地コードの一覧を取得する
        ''' </summary>
        ''' <param name="kaihatsuFugo">開発符号</param>
        ''' <param name="shiyoshoSeqno">仕様書一連No</param>
        ''' <param name="appliedNo">アプライドNo</param>
        ''' <param name="katashikiFugo7">７桁開発符号</param>
        ''' <returns>仕向地コードの一覧</returns>
        ''' <remarks></remarks>
        Function FindShimukechiCodeLabelValues(ByVal kaihatsuFugo As String, _
                                               ByVal shiyoshoSeqno As String, _
                                               ByVal appliedNo As String, _
                                               ByVal katashikiFugo7 As String) As List(Of LabelValueVo)

        ''' <summary>
        ''' OPコードの一覧を取得する
        ''' </summary>
        ''' <param name="kaihatsuFugo">開発符号</param>
        ''' <param name="shiyoshoSeqno">仕様書一連No</param>
        ''' <param name="appliedNo">アプライドNo</param>
        ''' <param name="katashikiFugo7">７桁開発符号</param>
        ''' <param name="shimukechiCode">仕向地コード</param>
        ''' <returns>OPコードの一覧</returns>
        ''' <remarks></remarks>
        Function FindOpCodeLabelValues(ByVal kaihatsuFugo As String, _
                                       ByVal shiyoshoSeqno As String, _
                                       ByVal appliedNo As String, _
                                       ByVal katashikiFugo7 As String, _
                                       ByVal shimukechiCode As String) As List(Of LabelValueVo)

        ''' <summary>
        ''' 外装色の一覧を取得する
        ''' </summary>
        ''' <param name="kaihatsuFugo">開発符号</param>
        ''' <param name="shiyoshoSeqno">仕様書一連No</param>
        ''' <param name="appliedNo">アプライドNo</param>
        ''' <param name="katashikiFugo7">７桁開発符号</param>
        ''' <param name="shimukechiCode">仕向地コード</param>
        ''' <param name="opCode">OPコード</param>
        ''' <returns>外装色の一覧</returns>
        ''' <remarks></remarks>
        Function FindGaisoShokuLabelValues(ByVal kaihatsuFugo As String, _
                                           ByVal shiyoshoSeqno As String, _
                                           ByVal appliedNo As String, _
                                           ByVal katashikiFugo7 As String, _
                                           ByVal shimukechiCode As String, _
                                           ByVal opCode As String) As List(Of LabelValueVo)

        ''' <summary>
        ''' 内装色の一覧を取得する
        ''' </summary>
        ''' <param name="kaihatsuFugo">開発符号</param>
        ''' <param name="shiyoshoSeqno">仕様書一連No</param>
        ''' <param name="appliedNo">アプライドNo</param>
        ''' <param name="katashikiFugo7">７桁開発符号</param>
        ''' <param name="shimukechiCode">仕向地コード</param>
        ''' <param name="opCode">OPコード</param>
        ''' <returns>内装色の一覧</returns>
        ''' <remarks></remarks>
        Function FindNaisoShokuLabelValues(ByVal kaihatsuFugo As String, _
                                           ByVal shiyoshoSeqno As String, _
                                           ByVal appliedNo As String, _
                                           ByVal katashikiFugo7 As String, _
                                           ByVal shimukechiCode As String, _
                                           ByVal opCode As String) As List(Of LabelValueVo)

        ''' <summary>
        ''' ７桁型式の取得
        ''' </summary>
        ''' <param name="kaihatsuFugo">開発符号</param>
        ''' <param name="shiyoshoSeqno">仕様書一連No</param>
        ''' <param name="katashikiFugo7">７桁開発符号</param>
        ''' <param name="appliedNo">アプライドNo</param>
        ''' <param name="shimukechiCode">仕向地コード</param>
        ''' <param name="opCode">OPコード</param>
        ''' <param name="colorCode">色コード</param>
        ''' <param name="naigaisoKbn">内外装区分</param>
        ''' <returns>７桁型式</returns>
        ''' <remarks></remarks>
        Function FindRhac0230By(ByVal kaihatsuFugo As String, ByVal shiyoshoSeqno As String, ByVal katashikiFugo7 As String, ByVal appliedNo As String, ByVal shimukechiCode As String, ByVal opCode As String, ByVal colorCode As String, ByVal naigaisoKbn As String) As Rhac0230Vo

        ''' <summary>
        ''' ７桁型式の取得
        ''' </summary>
        ''' <param name="kaihatsuFugo">開発符号</param>
        ''' <param name="shiyoshoSeqno">仕様書一連No</param>
        ''' <returns>７桁型式</returns>
        ''' <remarks></remarks>
        Function FindRhac0230By(ByVal kaihatsuFugo As String, ByVal shiyoshoSeqno As String) As List(Of Rhac0230Vo)

        ''' <summary>
        ''' ベース車の「試作イベントコード」コンボボックス表示用の値を返す
        ''' </summary>
        ''' <param name="withoutShisakuEventCode">取り除く試作イベントコード</param>
        ''' <returns>試作設計ブロックINSTL情報</returns>
        ''' <remarks></remarks>
        Function FindBlockInstlForLabelValuesByWithout(ByVal withoutShisakuEventCode As String) As List(Of TShisakuSekkeiBlockInstlVo)

        ''' <summary>
        ''' ベース車の「号車」コンボボックス表示用の値を返す
        ''' </summary>
        ''' <param name="shisakuEventCode">選択された「試作イベントコード」</param>
        ''' <returns>試作設計ブロックINSTL情報</returns>
        ''' <remarks></remarks>
        Function FindBlockInstlForLabelValuesBy(ByVal shisakuEventCode As String) As List(Of TShisakuSekkeiBlockInstlVo)

        ''' <summary>
        ''' 外装色コードから外装色名を返す
        ''' </summary>
        ''' <param name="colorCode">外装色コード</param>
        ''' <returns>外装色名</returns>
        ''' <remarks></remarks>
        Function FindGaisouColorName(ByVal colorCode As String) As Rhac0430Vo

        ''' <summary>
        ''' 内装色コードから内装色名を返す
        ''' </summary>
        ''' <param name="colorCode">内装色コード</param>
        ''' <returns>内装色名</returns>
        ''' <remarks></remarks>
        Function FindNaisouColorName(ByVal colorCode As String) As Rhac0430Vo

        ''' <summary>
        ''' rhac0230から装備改訂№、型式符号７を返す
        ''' </summary>
        ''' <param name="kaihatsuFugo">開発符号</param>
        ''' <param name="shiyoshoSeqno">仕様情報№</param>
        ''' <param name="katashiki">型式</param>
        ''' <remarks></remarks>
        Function FindSobiKaitei(ByVal kaihatsuFugo As String, ByVal shiyoshoSeqno As String, ByVal katashiki As String) As Rhac0230Vo


    End Interface
End Namespace