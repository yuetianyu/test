Imports ShisakuCommon.Db.EBom.Vo
Imports EventSakusei.TehaichoMenu.Dao
Imports EventSakusei.TehaichoMenu.Vo
Namespace TehaichoMenu.Dao
    Public Interface KaiteiUpDao


        ''' <summary>
        ''' 設計ブロック情報を取得
        ''' </summary>
        ''' <param name="shisakuEventCode">イベントコード</param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Function FindBySekkeiBlock(ByVal shisakuEventCode As String)

        ''' <summary>
        ''' 試作手配帳改訂抽出ブロック情報を追加する
        ''' </summary>
        ''' <param name="shisakuEventCode"></param>
        ''' <param name="shisakuListCode"></param>
        ''' <param name="shisakuListCodeKaiteiNo"></param>
        ''' <param name="sekkeiBlockVoList"></param>
        ''' <remarks></remarks>
        Sub InsertByKaiteiBlock(ByVal shisakuEventCode As String, _
                                ByVal shisakuListCode As String, _
                                ByVal shisakuListCodeKaiteiNo As String, _
                                ByVal sekkeiBlockVoList As List(Of TShisakuSekkeiBlockVo))



        ''' <summary>
        ''' リストコードの改訂繰上げ
        ''' </summary>
        ''' <param name="ListVo">リスト情報</param>
        ''' <remarks></remarks>
        Sub InsertByListKaiteiNo(ByVal ListVo As TShisakuListcodeVo)

        ''' <summary>
        ''' 手配帳基本情報の改訂繰上げ
        ''' </summary>
        ''' <param name="KihonVo">手配基本情報</param>
        ''' <remarks></remarks>
        Sub InsertByTehaiKihonKaiteiNo(ByVal KihonVo As List(Of TShisakuTehaiKihonVo))

        ''' <summary>
        ''' 手配帳号車情報の改訂繰上げ
        ''' </summary>
        ''' <param name="GousyaVo">手配号車情報</param>
        ''' <remarks></remarks>
        Sub InsertByTehaiGousyaKaiteiNo(ByVal GousyaVo As List(Of TShisakuTehaiGousyaVo))

        ''' <summary>
        ''' 手配帳号車情報の取得
        ''' </summary>
        ''' <param name="shisakuEventCode">イベントコード</param>
        ''' <param name="shisakuListCode">リストコード</param>
        ''' <remarks></remarks>
        Function FindByTehaiGousya(ByVal shisakuEventCode As String, ByVal shisakuListCode As String) As List(Of TShisakuTehaiGousyaVo)

        ''' <summary>
        ''' 手配帳基本情報の取得
        ''' </summary>
        ''' <param name="shisakuEventCode">イベントコード</param>
        ''' <param name="shisakuListCode">リストコード</param>
        ''' <remarks></remarks>
        Function FindByTehaiKihon(ByVal shisakuEventCode As String, ByVal shisakuListCode As String) As List(Of TShisakuTehaiKihonVo)

        ''' <summary>
        ''' 試作手配出図実績情報の取得
        ''' </summary>
        ''' <param name="shisakuEventCode">イベントコード</param>
        ''' <param name="shisakuListCode">リストコード</param>
        ''' <remarks></remarks>
        Function FindByTehaiShutuzuJiseki(ByVal shisakuEventCode As String, ByVal shisakuListCode As String) As List(Of TShisakuTehaiShutuzuJisekiVo)
        ''' <summary>
        ''' 試作手配出図実績手入力情報の取得
        ''' </summary>
        ''' <param name="shisakuEventCode">イベントコード</param>
        ''' <param name="shisakuListCode">リストコード</param>
        ''' <remarks></remarks>
        Function FindByTehaiShutuzuJisekiInput(ByVal shisakuEventCode As String, ByVal shisakuListCode As String) As List(Of TShisakuTehaiShutuzuJisekiInputVo)
        ''' <summary>
        ''' 試作手配出図織込情報の取得
        ''' </summary>
        ''' <param name="shisakuEventCode">イベントコード</param>
        ''' <param name="shisakuListCode">リストコード</param>
        ''' <remarks></remarks>
        Function FindByTehaiShutuzuOrikomi(ByVal shisakuEventCode As String, ByVal shisakuListCode As String) As List(Of TShisakuTehaiShutuzuOrikomiVo)
        ''' <summary>
        ''' 試作手配帳情報（号車グループ情報）の取得
        ''' </summary>
        ''' <param name="shisakuEventCode">イベントコード</param>
        ''' <param name="shisakuListCode">リストコード</param>
        ''' <remarks></remarks>
        Function FindByTehaiGousyaGroup(ByVal shisakuEventCode As String, ByVal shisakuListCode As String) As List(Of TShisakuTehaiGousyaGroupVo)

        ''' <summary>
        ''' 試作手配出図実績情報の改訂繰上げ
        ''' </summary>
        ''' <param name="Vo">試作手配出図実績情報</param>
        ''' <param name="KaiteiNo">リストコード改訂№</param>
        ''' <remarks></remarks>
        Sub InsertByTehaiShutuzuJisekiKaiteiNo(ByVal Vo As List(Of TShisakuTehaiShutuzuJisekiVo), ByVal KaiteiNo As String)
        ''' <summary>
        ''' 試作手配出図実績手入力情報の改訂繰上げ
        ''' </summary>
        ''' <param name="Vo">試作手配出図実績手入力情報</param>
        ''' <param name="KaiteiNo">リストコード改訂№</param>
        ''' <remarks></remarks>
        Sub InsertByTehaiShutuzuJisekiInputKaiteiNo(ByVal Vo As List(Of TShisakuTehaiShutuzuJisekiInputVo), ByVal KaiteiNo As String)
        ''' <summary>
        ''' 試作手配出図織込情報の改訂繰上げ
        ''' </summary>
        ''' <param name="Vo">試作手配出図織込情報</param>
        ''' <param name="KaiteiNo">リストコード改訂№</param>
        ''' <remarks></remarks>
        Sub InsertByTehaiShutuzuOrikomiKaiteiNo(ByVal Vo As List(Of TShisakuTehaiShutuzuOrikomiVo), ByVal KaiteiNo As String)
        ''' <summary>
        ''' 試作手配帳情報（号車グループ情報）の改訂繰上げ
        ''' </summary>
        ''' <param name="Vo">試作手配帳情報（号車グループ情報）</param>
        ''' <param name="KaiteiNo">リストコード改訂№</param>
        ''' <remarks></remarks>
        Sub InsertByTehaiGousyaGroupKaiteiNo(ByVal Vo As List(Of TShisakuTehaiGousyaGroupVo), ByVal KaiteiNo As String)

    End Interface
End Namespace