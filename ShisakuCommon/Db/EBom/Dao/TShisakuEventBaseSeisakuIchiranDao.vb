﻿Imports ShisakuCommon.Db.EBom.Vo

Namespace Db.EBom.Dao
    ''' <summary>
    '''  試作イベントベース車情報（製作一覧）テーブルの簡単なCRUDを集めたDAO
    ''' </summary>
    ''' <remarks></remarks>
    Public Interface TShisakuEventBaseSeisakuIchiranDao : Inherits DaoEachTable(Of TShisakuEventBaseSeisakuIchiranVo)
        ''' <summary>
        ''' テーブル値の検索結果を返す
        ''' </summary>
        ''' <param name="shisakuEventCode">試作イベントコード</param>
        ''' <param name="hyoJiJun_No">表示順</param>
        ''' <returns>該当レコード</returns>
        ''' <remarks></remarks>
        Function FindByPk(ByVal shisakuEventCode As String, ByVal hyoJiJun_No As Integer) As TShisakuEventBaseSeisakuIchiranVo

        ''' <summary>
        ''' 該当レコードを削除する
        ''' </summary>
        ''' <param name="shisakuEventCode">試作イベントコード</param>
        ''' <param name="hyoJiJun_No">表示順</param>
        ''' <returns>削除件数</returns>
        ''' <remarks></remarks>
        Function DeleteByPk(ByVal shisakuEventCode As String, ByVal hyoJiJun_No As Integer) As Integer
    End Interface


End Namespace

