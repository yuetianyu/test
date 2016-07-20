Imports ShisakuCommon
Imports ShisakuCommon.Db
Imports ShisakuCommon.Db.EBom
Imports EventSakusei.XVLView.Dao.Vo


Namespace XVLView.Dao.Impl

    Public Class RHAC2270DaoImpl

        ''' <summary>
        ''' 開発符号、ブロック番号、部品番号からファイル名を取得する.
        ''' </summary>
        ''' <param name="aArg"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Function getXVLFileName(ByVal aArg As XVLView.Dao.Vo.RHAC2270Vo) As List(Of XVLView.Dao.Vo.RHAC2270Vo)
            If aArg.KaihatsuFugo Is Nothing Then Throw New ArgumentException("RHAC2270DaoImpl：必須入力「部品番号」のパラメータが入力されていません.")
            If aArg.BuhinNo Is Nothing Then Throw New ArgumentException("RHAC2270DaoImpl：必須入力「部品番号」のパラメータが入力されていません.")

            Dim iSql As New System.Text.StringBuilder
            With iSql
                .AppendLine("SELECT ")
                .AppendLine("    [KAIHATSU_FUGO]")
                .AppendLine("    ,[BLOCK_NO]")
                .AppendLine("    ,[BUHIN_NO]")
                .AppendLine("    ,[CAD_KAITEI_NO]")
                .AppendLine("    ,[XVL_FILE_NAME]")
                .AppendLine("FROM ")
                .AppendLine("    " & RHACLIBF_DB_NAME & ".[dbo].[RHAC2270]")
                .AppendLine("WHERE")
                .AppendLine("    BUHIN_NO =  @BuhinNo ")
                .AppendLine("    AND KAIHATSU_FUGO = @KaihatsuFugo ")
                '改訂番号が最新のファイル名を取得.
                .AppendLine("    AND CAD_KAITEI_NO = (SELECT")
                .AppendLine("		                     MAX([CAD_KAITEI_NO]) AS KAITEI_NO")
                .AppendLine("	                      FROM")
                .AppendLine("		                     " & RHACLIBF_DB_NAME & ".[DBO].[RHAC2270] ")
                .AppendLine("	                      WHERE")
                .AppendLine("		                     [KAIHATSU_FUGO]=@KaihatsuFugo ")
                If aArg.BlockNo IsNot Nothing Then
                    .AppendLine("		                     AND BLOCK_NO =  @BlockNo ")
                End If
                .AppendLine("		                     AND BUHIN_NO = @BuhinNo)")

                If aArg.BlockNo IsNot Nothing Then
                    .AppendLine("    AND BLOCK_NO =  @BlockNo ")
                End If
                .AppendLine("GROUP BY ")
                .AppendLine("    [KAIHATSU_FUGO]")
                .AppendLine("    ,[BLOCK_NO]")
                .AppendLine("    ,[BUHIN_NO]")
                .AppendLine("    ,[CAD_KAITEI_NO]")
                .AppendLine("    ,[XVL_FILE_NAME]")
                .AppendLine("    ,[CATIA_FILE_KBN]")
                .AppendLine("ORDER BY ")
                .AppendLine("     CATIA_FILE_KBN DESC")
                .AppendLine("     ,CAD_KAITEI_NO DESC")
            End With

            Dim db As New EBomDbClient

            getXVLFileName = db.QueryForList(Of XVLView.Dao.Vo.RHAC2270Vo)(iSql.ToString, aArg)
        End Function

        ''' <summary>
        ''' 開発符号、ブロック番号、部品番号から最新改訂のボディファイル名を取得する.
        ''' </summary>
        ''' <param name="aArg"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Function getBODYXVLFileName(ByVal aArg As XVLView.Dao.Vo.RHAC2270Vo) As List(Of XVLView.Dao.Vo.RHAC2270Vo)

            Dim iSql As New System.Text.StringBuilder
            With iSql
                .AppendLine("SELECT ")
                .AppendLine("    [XVL_FILE_NAME]")
                .AppendLine("    ,[CAD_DATA_EVENT_KBN]")
                .AppendLine("    ,[CAD_KAITEI_NO]")
                .AppendLine("FROM ")
                .AppendLine("    " & RHACLIBF_DB_NAME & ".[dbo].[RHAC2270]")
                .AppendLine("WHERE")
                .AppendLine("    KAIHATSU_FUGO = @KaihatsuFugo ")
                .AppendLine("    AND BLOCK_NO =  @BlockNo ")
                .AppendLine("    AND BUHIN_NO =  @BuhinNo ")
                .AppendLine("ORDER BY ")
                'CADデータイベント区分の優先順位は G > ND > S3 > S2 > S1
                .AppendLine("    CASE [CAD_DATA_EVENT_KBN]")
                .AppendLine("      WHEN 'G'  THEN 1")
                .AppendLine("      WHEN 'ND' THEN 2")
                .AppendLine("      WHEN 'S3' THEN 3")
                .AppendLine("      WHEN 'S2' THEN 4")
                .AppendLine("      WHEN 'S1' THEN 5")
                .AppendLine("    END ASC")
                .AppendLine("    ,CAD_KAITEI_NO DESC")
            End With

            Dim db As New EBomDbClient
            Dim iFiles As New List(Of XVLView.Dao.Vo.RHAC2270Vo)
            iFiles = db.QueryForList(Of XVLView.Dao.Vo.RHAC2270Vo)(iSql.ToString, aArg)


            'レコード件数が一件の場合は問題なし.
            If iFiles.Count <= 1 Then
                Return iFiles
            End If

            '複数件取得された場合、CATIA_FILE_KBNが大きいものを優先して１レコード返す(クエリでソート済みなので先頭レコード）。
            Dim iRet As New List(Of XVLView.Dao.Vo.RHAC2270Vo)
            iRet.Add(iFiles(0))
            Return iRet
        End Function


        ''' <summary>
        ''' 最新改訂を検索.
        '''  開発符号、ブロック番号、キャドデータイベント区分、部品番号からファイル名を取得する.
        ''' </summary>
        ''' <param name="aArg"></param>
        ''' <returns></returns>
        ''' <remarks>
        ''' <para>
        ''' 複数レコード抽出された場合CATIA_FILE_KBNが大きいレコードを１レコードのみ返す.
        ''' </para></remarks>
        Public Function getKbnXVLFileName(ByVal aArg As XVLView.Dao.Vo.RHAC2270Vo) As List(Of XVLView.Dao.Vo.RHAC2270Vo)
            If aArg.KaihatsuFugo Is Nothing Then Throw New ArgumentException("RHAC2270DaoImpl：必須入力「部品番号」のパラメータが入力されていません.")
            If aArg.BuhinNo Is Nothing Then Throw New ArgumentException("RHAC2270DaoImpl：必須入力「部品番号」のパラメータが入力されていません.")
            If aArg.BlockNo Is Nothing Then Throw New ArgumentException("RHAC2270DaoImpl：必須入力「ブロック番号」のパラメータが入力されていません.")

            aArg.BuhinNo = aArg.BuhinNo & "%"

            Dim iSql As New System.Text.StringBuilder
            With iSql
                .AppendLine("SELECT ")
                .AppendLine("    KAIHATSU_FUGO")
                .AppendLine("    ,BLOCK_NO")
                .AppendLine("    ,BUHIN_NO")
                .AppendLine("    ,CAD_KAITEI_NO")
                .AppendLine("    ,XVL_FILE_NAME")
                .AppendLine("    ,CAD_DATA_EVENT_KBN")
                .AppendLine("FROM ")
                .AppendLine("    " & RHACLIBF_DB_NAME & ".dbo.RHAC2270 T227 ")
                .AppendLine("WHERE")
                .AppendLine("    T227.BUHIN_NO LIKE @BuhinNo ")
                '.AppendLine("    AND T227.CAD_DATA_EVENT_KBN =  @CadDataEventKbn ")
                .AppendLine("    AND T227.KAIHATSU_FUGO = @KaihatsuFugo ")
                '改訂番号が最新のファイル名を取得.
                .AppendLine("    AND T227.CAD_KAITEI_NO = (SELECT")
                .AppendLine("		                     MAX(CAD_KAITEI_NO)")
                .AppendLine("	                      FROM")
                .AppendLine("		                     " & RHACLIBF_DB_NAME & ".DBO.RHAC2270 ")
                .AppendLine("	                      WHERE")
                .AppendLine("                            CAD_DATA_EVENT_KBN = T227.CAD_DATA_EVENT_KBN ")
                .AppendLine("		                     AND KAIHATSU_FUGO = T227.KAIHATSU_FUGO ")
                .AppendLine("		                     AND BLOCK_NO =  T227.BLOCK_NO ")
                .AppendLine("		                     AND BUHIN_NO = T227.BUHIN_NO )")
                .AppendLine("    AND BLOCK_NO =  @BlockNo ")
                .AppendLine("GROUP BY ")
                .AppendLine("    KAIHATSU_FUGO")
                .AppendLine("    ,BLOCK_NO")
                .AppendLine("    ,BUHIN_NO")
                .AppendLine("    ,CAD_KAITEI_NO")
                .AppendLine("    ,XVL_FILE_NAME")
                .AppendLine("    ,CATIA_FILE_KBN")
                .AppendLine("    ,CAD_DATA_EVENT_KBN")
                .AppendLine("ORDER BY ")
                .AppendLine(" CASE CAD_DATA_EVENT_KBN ")
                .AppendLine(" WHEN 'G' THEN 1 ")
                .AppendLine(" WHEN 'ND' THEN 2 ")
                .AppendLine(" WHEN 'S3' THEN 3 ")
                .AppendLine(" WHEN 'S2' THEN 4 ")
                .AppendLine(" WHEN 'S1' THEN 5 ")
                .AppendLine(" ELSE 6 ")
                .AppendLine(" END ")
                .AppendLine("     ,CATIA_FILE_KBN DESC ")
                .AppendLine("     ,CAD_KAITEI_NO DESC ")
            End With

            Dim db As New EBomDbClient
            Dim iFiles As New List(Of XVLView.Dao.Vo.RHAC2270Vo)
            iFiles = db.QueryForList(Of XVLView.Dao.Vo.RHAC2270Vo)(iSql.ToString, aArg)


            'レコード件数が一件の場合は問題なし.
            If iFiles.Count <= 1 Then
                Return iFiles
            End If

            '複数件取得された場合、CATIA_FILE_KBNが大きいものを優先して１レコード返す(クエリでソート済みなので先頭レコード）。
            Dim iRet As New List(Of XVLView.Dao.Vo.RHAC2270Vo)
            iRet.Add(iFiles(0))
            Return iRet

        End Function

        ''' <summary>
        ''' 最新改訂を検索.
        '''  開発符号、ブロック番号、キャドデータイベント区分、部品番号からファイル名を取得する.
        ''' </summary>
        ''' <param name="aGUID"></param>
        ''' <param name="kaihatsuFugo"></param>
        ''' <returns></returns>
        ''' 複数レコード抽出された場合CATIA_FILE_KBNが大きいレコードを１レコードのみ返す.
        ''' <remarks></remarks>
        Public Function getKbnXVLFileName(ByVal aGUID As Guid, ByVal kaihatsuFugo As String) As List(Of XVLView.Dao.Vo.RHAC2270Vo)

            Dim iSql As New System.Text.StringBuilder
            With iSql
                .AppendLine("SELECT ")
                .AppendLine("    T227.KAIHATSU_FUGO")
                .AppendLine("    ,T227.BLOCK_NO")
                .AppendLine("    ,T227.BUHIN_NO")
                .AppendLine("    ,T227.CAD_KAITEI_NO")
                .AppendLine("    ,T227.XVL_FILE_NAME")
                .AppendLine("    ,T227.CAD_DATA_EVENT_KBN")
                .AppendLine("FROM ")
                .AppendLine(" " & MBOM_DB_NAME & ".dbo.T_SHISAKU_BUHIN_EDIT_TMP_3D TMP")
                .AppendLine(" INNER JOIN " & RHACLIBF_DB_NAME & ".dbo.RHAC2270 T227 ")
                .AppendLine(" ON T227.KAIHATSU_FUGO = '" & kaihatsuFugo & "'")
                .AppendLine(" AND  ")
                .AppendLine("    T227.BUHIN_NO LIKE SUBSTRING(TMP.BUHIN_NO, 1, 10) + '%' ")
                '改訂番号が最新のファイル名を取得.
                .AppendLine("    AND T227.CAD_KAITEI_NO = (SELECT")
                .AppendLine("		                     MAX(CAD_KAITEI_NO)")
                .AppendLine("	                      FROM")
                .AppendLine("		                     " & RHACLIBF_DB_NAME & ".DBO.RHAC2270 ")
                .AppendLine("	                      WHERE")
                .AppendLine("                            CAD_DATA_EVENT_KBN = T227.CAD_DATA_EVENT_KBN ")
                .AppendLine("		                     AND KAIHATSU_FUGO = T227.KAIHATSU_FUGO ")
                .AppendLine("		                     AND BLOCK_NO =  T227.BLOCK_NO ")
                .AppendLine("		                     AND BUHIN_NO = T227.BUHIN_NO )")
                .AppendLine("    AND T227.BLOCK_NO =  TMP.SHISAKU_BLOCK_NO ")
                .AppendLine("WHERE")
                .AppendLine(" TMP.GUID = '" & aGUID.ToString & "'")
                .AppendLine("GROUP BY ")
                .AppendLine("    T227.KAIHATSU_FUGO")
                .AppendLine("    ,T227.BLOCK_NO")
                .AppendLine("    ,T227.BUHIN_NO")
                .AppendLine("    ,T227.CAD_KAITEI_NO")
                .AppendLine("    ,T227.XVL_FILE_NAME")
                .AppendLine("    ,T227.CATIA_FILE_KBN")
                .AppendLine("    ,T227.CAD_DATA_EVENT_KBN")
                .AppendLine("ORDER BY ")
                .AppendLine(" T227.BLOCK_NO, ")
                .AppendLine(" T227.BUHIN_NO, ")
                .AppendLine(" CASE T227.CAD_DATA_EVENT_KBN ")
                .AppendLine(" WHEN 'G' THEN 1 ")
                .AppendLine(" WHEN 'ND' THEN 2 ")
                .AppendLine(" WHEN 'S3' THEN 3 ")
                .AppendLine(" WHEN 'S2' THEN 4 ")
                .AppendLine(" WHEN 'S1' THEN 5 ")
                .AppendLine(" ELSE 6 ")
                .AppendLine(" END ")
                .AppendLine("     ,T227.CATIA_FILE_KBN DESC ")
                .AppendLine("     ,T227.CAD_KAITEI_NO DESC ")
            End With

            Dim db As New EBomDbClient
            Return db.QueryForList(Of XVLView.Dao.Vo.RHAC2270Vo)(iSql.ToString)
        End Function

    End Class

End Namespace
