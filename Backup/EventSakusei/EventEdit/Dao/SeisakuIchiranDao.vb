Imports ShisakuCommon.Db
Imports ShisakuCommon.Util.LabelValue
Imports ShisakuCommon.Db.EBom.Vo

Namespace EventEdit.Dao
    Public Interface SeisakuIchiranDao : Inherits DaoEachFeature

        ''' <summary>
        ''' 製作一覧発行処理Spread一覧情報を取得する
        ''' </summary>
        ''' <param name="status">ステータス</param>
        ''' <remarks></remarks>
        Function GetSeisakuIchiranHdSpreadList(ByVal status As String, ByVal strHakouNo As String, _
                                               ByVal strKaihatsuFugo As String, ByVal strEvent As String, _
                                               ByVal strEventname As String, ByVal strKaiteiNo As String) As List(Of TSeisakuHakouHdVo)

        ''' <summary>
        ''' 製作一覧の情報を取得する
        ''' </summary>
        ''' <param name="strHakouNo">発行№</param>
        ''' <param name="strKaiteiNo">改訂№</param>
        ''' <remarks></remarks>
        Function GetSeisakuIchiranHd(ByVal strHakouNo As String, ByVal strKaiteiNo As String) As TSeisakuHakouHdVo

        ''' <summary>
        ''' ベース車情報を取得する
        ''' </summary>
        ''' <param name="strHakouNo">発行№</param>
        ''' <param name="strKaiteiNo">改訂№</param>
        ''' <remarks></remarks>
        Function GetTSeisakuIchiranBase(ByVal strHakouNo As String, ByVal strKaiteiNo As String) _
                    As List(Of TSeisakuIchiranBaseVo)

        ''' <summary>
        ''' ベース車情報を取得する
        ''' （発行№、改訂№、号車）
        ''' </summary>
        ''' <param name="strHakouNo">発行№</param>
        ''' <param name="strKaiteiNo">改訂№</param>
        ''' <param name="strGousya">号車</param>
        ''' <remarks></remarks>
        Function GetTSeisakuIchiranBaseGousya(ByVal strHakouNo As String, ByVal strKaiteiNo As String, ByVal strGousya As String) _
                    As TSeisakuIchiranBaseVo

        ''' <summary>
        ''' ＷＢ車情報を取得する
        ''' </summary>
        ''' <param name="strHakouNo">発行№</param>
        ''' <param name="strKaiteiNo">改訂№</param>
        ''' <remarks></remarks>
        Function GetTSeisakuIchiranWb(ByVal strhakouNo As String, ByVal strkaiteiNo As String) _
                    As List(Of TSeisakuIchiranWbVo)

        ''' <summary>
        ''' ＷＢ車情報を取得する
        ''' </summary>
        ''' <param name="strHakouNo">発行№</param>
        ''' <param name="strKaiteiNo">改訂№</param>
        ''' <param name="strGousya">号車</param>
        ''' <remarks></remarks>
        Function GetTSeisakuIchiranWbGousya(ByVal strhakouNo As String, ByVal strkaiteiNo As String, ByVal strGousya As String) _
                    As TSeisakuIchiranWbVo

        ''' <summary>
        ''' 完成車情報を取得する
        ''' </summary>
        ''' <param name="strHakouNo">発行№</param>
        ''' <param name="strKaiteiNo">改訂№</param>
        ''' <remarks></remarks>
        Function GetTSeisakuIchiranKansei(ByVal strhakouNo As String, ByVal strkaiteiNo As String) _
                    As List(Of TSeisakuIchiranKanseiVo)

        ''' <summary>
        ''' 完成車情報を取得する
        ''' </summary>
        ''' <param name="strHakouNo">発行№</param>
        ''' <param name="strKaiteiNo">改訂№</param>
        ''' <param name="strGousya">号車</param>
        ''' <remarks></remarks>
        Function GetTSeisakuIchiranKanseiGousya(ByVal strhakouNo As String, ByVal strkaiteiNo As String, ByVal strGousya As String) _
                    As TSeisakuIchiranKanseiVo

        ''' <summary>
        ''' OP項目列（完成車／ＷＢ車）を取得する
        ''' </summary>
        ''' <param name="strHakouNo">発行№</param>
        ''' <param name="strKaiteiNo">改訂№</param>
        ''' <remarks></remarks>
        Function GetTSeisakuIchiranOpkoumoku(ByVal strhakouNo As String, ByVal strkaiteiNo As String) _
                    As List(Of TSeisakuIchiranOpkoumokuVo)

        ''' <summary>
        ''' OP項目列（完成車／ベース車／ＷＢ車）を取得する
        ''' </summary>
        ''' <param name="strHakouNo">発行№</param>
        ''' <param name="strKaiteiNo">改訂№</param>
        ''' <param name="strSyubetu">種別</param>
        ''' <param name="strGousya">号車</param>
        ''' <remarks></remarks>
        Function GetTSeisakuIchiranOpkoumokuGousya(ByVal strhakouNo As String, ByVal strkaiteiNo As String, _
                                                   ByVal strSyubetu As String, ByVal strGousya As String) _
                    As List(Of TSeisakuIchiranOpkoumokuVo)

        ''' <summary>
        ''' 試作特別織込み項目列及びＷＢ特別装備仕様を取得する
        ''' </summary>
        ''' <param name="strHakouNo">発行№</param>
        ''' <param name="strKaiteiNo">改訂№</param>
        ''' <remarks></remarks>
        Function GetTSeisakuIchiranTokubetu(ByVal strhakouNo As String, ByVal strkaiteiNo As String) _
                    As List(Of TSeisakuTokubetuOrikomiVo)

        ''' <summary>
        ''' 試作特別織込み項目列及びＷＢ特別装備仕様を取得する（ＷＢ車）
        ''' </summary>
        ''' <param name="strHakouNo">発行№</param>
        ''' <param name="strKaiteiNo">改訂№</param>
        ''' <remarks></remarks>
        Function GetTSeisakuIchiranTokubetuWB(ByVal strhakouNo As String, ByVal strkaiteiNo As String) _
                    As List(Of TSeisakuWbSoubiShiyouVo)

        ''' <summary>
        ''' 試作特別織込み項目列及びＷＢ特別装備仕様を取得する
        ''' </summary>
        ''' <param name="strHakouNo">発行№</param>
        ''' <param name="strKaiteiNo">改訂№</param>
        ''' <param name="strGousya">号車</param>
        ''' <remarks></remarks>
        Function GetTSeisakuIchiranTokubetuGousya(ByVal strhakouNo As String, _
                                                  ByVal strkaiteiNo As String, _
                                                  ByVal strGousya As String) _
                    As List(Of TSeisakuTokubetuOrikomiVo)

        ''' <summary>
        ''' 試作特別織込み項目列及びＷＢ特別装備仕様を取得する（ＷＢ車）
        ''' </summary>
        ''' <param name="strHakouNo">発行№</param>
        ''' <param name="strKaiteiNo">改訂№</param>
        ''' <param name="strGousya">号車</param>
        ''' <remarks></remarks>
        Function GetTSeisakuIchiranTokubetuGousyaWB(ByVal strhakouNo As String, _
                                                  ByVal strkaiteiNo As String, _
                                                  ByVal strGousya As String) _
                    As List(Of TSeisakuWbSoubiShiyouVo)

        ''' <summary>
        ''' イベントコードで試作設計ブロック情報から設計担当者情報を取得
        ''' </summary>
        ''' <param name="streventcode">イベントコード</param>
        ''' <remarks></remarks>
        Function GetShisakuSekkeiBlockTanto(ByVal strEventCode As String) _
                    As List(Of SendMailUserAddressVo)

    End Interface

End Namespace

