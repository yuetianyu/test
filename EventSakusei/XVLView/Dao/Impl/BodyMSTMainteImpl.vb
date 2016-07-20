Imports System.Text

Imports ShisakuCommon
Imports ShisakuCommon.Db
Imports ShisakuCommon.Db.EBom

Namespace XVLView.Dao.Impl

    ''' <summary>
    ''' 未決リストを作成する.
    ''' </summary>
    ''' <remarks></remarks>
    Public Class BodyMSTMainteImpl


        ''' <summary>
        ''' ボディリストの作成.
        ''' </summary>
        ''' <param name="aArg"></param>
        ''' <returns></returns>
        ''' <remarks>
        ''' 専用品なのでInterfaceは作成していません.
        ''' クラスのサフィックスImplはほかの構成と合わせるためについけています。
        ''' </remarks>
        Public Function SelectBodyList(ByVal aArg As Vo.BodyMSTMainteVO) As List(Of Vo.BodyMSTMainteVO)
            If aArg.KaihatsuFugo Is Nothing Then Throw New ArgumentException("開発符号を入力してください.")


            Dim sb As New StringBuilder
            With sb
                .Remove(0, .Length)
                .AppendLine("SELECT ")
                .AppendLine("       KAIHATSU_FUGO")
                '.AppendLine("       ,BUHIN_NO")
                .AppendLine("       ,BODY_NAME")
                .AppendLine("FROM  ")
                .AppendLine("      " & MBOM_DB_NAME & ".dbo.M_SHISAKU_BODY_3D ")
                .AppendLine("WHERE ")
                .AppendLine("      KAIHATSU_FUGO = @KaihatsuFugo")
                If StringUtil.IsNotEmpty(aArg.BodyName) Then
                    .AppendLine("      AND BODY_NAME = @BodyName")
                End If
                .AppendLine("GROUP BY ")
                .AppendLine("      KAIHATSU_FUGO")
                .AppendLine("      ,BODY_NAME")


            End With


            Dim db As New EBomDbClient
            Return db.QueryForList(Of Vo.BodyMSTMainteVO)(sb.ToString, aArg)
        End Function

        ''' <summary>
        ''' 部品リストの作成.
        ''' </summary>
        ''' <param name="aArg"></param>
        ''' <returns></returns>
        ''' <remarks>
        ''' <para>専用品なのでInterfaceは作成していません.</para>
        ''' <para>クラスのサフィックスImplはほかの構成と合わせるためについけています。</para>
        ''' </remarks>
        Public Function SelectPartsList(ByVal aArg As Vo.BodyMSTMainteVO) As List(Of Vo.BodyMSTMainteVO)
            If aArg.KaihatsuFugo Is Nothing Then Throw New ArgumentException("開発符号を入力してください.")
            If aArg.BodyName Is Nothing Then Throw New ArgumentException("ボディー名を入力してください.")

            Dim sb As New StringBuilder
            With sb
                .Remove(0, .Length)
                .AppendLine("SELECT ")
                .AppendLine("       KAIHATSU_FUGO")
                .AppendLine("       ,BODY_NAME")
                .AppendLine("       ,BUHIN_NO")
                .AppendLine("FROM  ")
                .AppendLine("      " & MBOM_DB_NAME & ".dbo.M_SHISAKU_BODY_3D ")
                .AppendLine("WHERE ")
                .AppendLine("      KAIHATSU_FUGO = @KaihatsuFugo")
                .AppendLine("      AND BODY_NAME = @BodyName")
            End With

            Dim db As New EBomDbClient
            Return db.QueryForList(Of Vo.BodyMSTMainteVO)(sb.ToString, aArg)
        End Function

#Region "ボディーマスタへの登録"
        ''' <summary>
        ''' ボディーマスタへの登録
        ''' </summary>
        ''' <param name="paramVo"></param>
        ''' <remarks>
        ''' 開発符号、ボディ名ごとに登録処理を行いたい場合はこのメソッドを使用する.
        ''' Dao.Vo.BodyMSTMainteVOリストの開発符号、ボディ名はすべて一致していないといけない.
        ''' </remarks>
        Public Sub InsertBodyList(ByVal paramVo As List(Of Dao.Vo.BodyMSTMainteVO))
            '
            Dim iKeyVo As New Dao.Vo.BodyMSTMainteVO
            iKeyVo.KaihatsuFugo = paramVo(0).KaihatsuFugo
            iKeyVo.BodyName = paramVo(0).BodyName

            'パラメータ入力チェック
            If StringUtil.IsEmpty(iKeyVo.KaihatsuFugo) Then Throw New ArgumentException("開発符号が指定されていないため登録を行えません.")
            If StringUtil.IsEmpty(iKeyVo.BodyName) Then Throw New ArgumentException("ボディー名称が指定されていないため登録を行えません.")

            ' 初期設定
            '(呼び出し元でEBomDbClientのインスタンスを作成しておくこと.またCommitも呼び出し元で行うこと.)
            Using db As New EBomDbClient
                ''トランザクション開始.
                'db.BeginTransaction()

                Dim sb As New StringBuilder

                '開発符号、ボディ名をキーに、削除クエリ作成.
                With sb

                    .Remove(0, .Length)
                    .AppendLine(" DELETE FROM " & MBOM_DB_NAME & ".dbo.M_SHISAKU_BODY_3D ")
                    .AppendLine(" WHERE KAIHATSU_FUGO = @KaihatsuFugo ")
                    .AppendLine(" AND BODY_NAME = @BodyName ")

                End With

                '削除.
                db.Delete(sb.ToString(), iKeyVo)


                sb = New StringBuilder

                '挿入処理.
                For Each lInsertRec In paramVo

                    If iKeyVo.KaihatsuFugo <> lInsertRec.KaihatsuFugo Then Throw New ArgumentException("正しい開発符号キー情報が設定されていません")
                    If iKeyVo.BodyName <> lInsertRec.BodyName Then Throw New ArgumentException("正しいボディ名キー情報が設定されていません")


                    With sb

                        .Remove(0, .Length)
                        .AppendLine("INSERT INTO " & MBOM_DB_NAME & ".dbo.M_SHISAKU_BODY_3D (")
                        .AppendLine("       KAIHATSU_FUGO")
                        .AppendLine("       ,BUHIN_NO")
                        .AppendLine("       ,BODY_NAME")
                        .AppendLine("       ,CREATED_USER_ID")
                        .AppendLine(" ) VALUES ( ")
                        .AppendLine("       @KaihatsuFugo")
                        .AppendLine("       ,@BuhinNo")
                        .AppendLine("       ,@BodyName")
                        .AppendLine("       ,@CreatedUserId")
                        .AppendLine(" ) ")

                    End With

                    ' パラメータセット
                    '=== 共通項目 ===
                    lInsertRec.CreatedUserId = LoginInfo.Now.UserId

                    '=== 挿入 ===
                    db.Insert(sb.ToString, lInsertRec)

                Next

                ''正常終了の場合にCommit（削除と挿入を実施.）
                'db.Commit()
            End Using


        End Sub
#End Region

#Region "削除"
        ''' <summary>
        ''' ボディーテーブルから開発符号、ボディー名称をキーにレコードを削除する.
        ''' </summary>
        ''' <remarks></remarks>
        Public Sub DeleteBodyList(ByVal paramVo As Dao.Vo.BodyMSTMainteVO)
            Dim sb As New StringBuilder

            'パラメータ入力チェック
            If StringUtil.IsEmpty(paramVo.KaihatsuFugo) Then Throw New ArgumentException("開発符号が指定されていないため削除を行いません.")
            If StringUtil.IsEmpty(paramVo.BodyName) Then Throw New ArgumentException("ボディー名称が指定されていないため削除を行いません.")

            'クエリ作成.
            With sb

                .Remove(0, .Length)
                .AppendLine(" DELETE FROM " & MBOM_DB_NAME & ".dbo.M_SHISAKU_BODY_3D ")
                .AppendLine(" WHERE KAIHATSU_FUGO = @KaihatsuFugo ")
                .AppendLine(" AND BODY_NAME = @BodyName ")

                If StringUtil.IsNotEmpty(paramVo.BuhinNo) Then
                    .AppendLine(" AND BUHIN_NO = @BuhinNo ")
                End If


            End With

            Using db As New EBomDbClient
                'トランザクションの開始.
                db.BeginTransaction()

                db.Delete(sb.ToString(), paramVo)

                db.Commit()
            End Using



        End Sub

#End Region

    End Class
End Namespace