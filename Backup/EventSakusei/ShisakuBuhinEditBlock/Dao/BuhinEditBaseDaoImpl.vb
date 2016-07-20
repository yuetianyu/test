Imports ShisakuCommon.Db.EBom
Imports ShisakuCommon.Db.EBom.Vo
Imports EventSakusei.ShisakuBuhinEdit.Kosei.Logic.Matrix
Imports ShisakuCommon
Imports EBom.Data
Imports EBom.Common
Imports System.Text

Namespace ShisakuBuhinEditBlock.Dao
    Public Class BuhinEditBaseDaoImpl : Implements BuhinEditBaseDao

        ''' <summary>
        ''' 設計ブロック情報を取得する
        ''' </summary>
        ''' <param name="shisakuEventCode">試作イベントコード</param>
        ''' <param name="shisakuBukaCode">試作部課コード</param>
        ''' <param name="shisakuBlockNo">試作ブロックNo</param>
        ''' <param name="shisakuBlockNoKaiteiNo">試作ブロックNo改訂№</param>
        ''' <returns>設計ブロック情報</returns>
        ''' <remarks></remarks>
        Public Function FindBySekkeiBlock(ByVal shisakuEventCode As String, ByVal shisakuBukaCode As String, _
                                          ByVal shisakuBlockNo As String, ByVal shisakuBlockNoKaiteiNo As String) As List(Of TShisakuSekkeiBlockVo) Implements BuhinEditBaseDao.FindBySekkeiBlock
            Dim sql As New StringBuilder

            With sql
                .Remove(0, .Length)
                .AppendLine(" SELECT SB.* ")
                .AppendLine(" FROM " & MBOM_DB_NAME & ".dbo.T_SHISAKU_SEKKEI_BLOCK SB WITH (NOLOCK, NOWAIT) ")
                .AppendLine(" WHERE")
                .AppendLine(" SB.SHISAKU_EVENT_CODE = @ShisakuEventCode ")
                .AppendLine(" AND SB.SHISAKU_BUKA_CODE = @ShisakuBukaCode ")
                .AppendLine(" AND SB.SHISAKU_BLOCK_NO = @ShisakuBlockNo ")
                .AppendLine(" AND SB.SHISAKU_BLOCK_NO_KAITEI_NO = @ShisakuBlockNoKaiteiNo ")
            End With

            Dim param As New TShisakuSekkeiBlockVo
            param.ShisakuEventCode = shisakuEventCode
            param.ShisakuBukaCode = shisakuBukaCode
            param.ShisakuBlockNo = shisakuBlockNo
            param.ShisakuBlockNoKaiteiNo = shisakuBlockNoKaiteiNo

            Dim db As New EBomDbClient
            Return db.QueryForList(Of TShisakuSekkeiBlockVo)(sql.ToString, param)
        End Function

        ''' <summary>
        ''' 全設計ブロック情報を取得
        ''' </summary>
        ''' <param name="shisakuEventCode">試作イベントコード</param>
        ''' <returns>設計ブロック情報</returns>
        ''' <remarks></remarks>
        Public Function FindBySekkeiBlockAll(ByVal shisakuEventCode As String) As List(Of TShisakuSekkeiBlockVo) Implements BuhinEditBaseDao.FindBySekkeiBlockAll
            Dim sql As New StringBuilder

            With sql
                .Remove(0, .Length)
                .AppendLine(" SELECT SB.* ")
                .AppendLine(" FROM " & MBOM_DB_NAME & ".dbo.T_SHISAKU_SEKKEI_BLOCK SB WITH (NOLOCK, NOWAIT) ")
                .AppendLine(" WHERE")
                .AppendLine(" SB.SHISAKU_EVENT_CODE = @ShisakuEventCode ")
            End With

            Dim param As New TShisakuSekkeiBlockVo
            param.ShisakuEventCode = shisakuEventCode

            Dim db As New EBomDbClient
            Return db.QueryForList(Of TShisakuSekkeiBlockVo)(sql.ToString, param)

        End Function

        ''' <summary>
        ''' 開発符号を取得
        ''' </summary>
        ''' <param name="shisakuEventCode">試作イベントコード</param>
        ''' <returns>イベント情報</returns>
        ''' <remarks></remarks>
        Public Function FindByKaihatsufugo(ByVal shisakuEventCode As String) As TShisakuEventVo Implements BuhinEditBaseDao.FindByKaihatsufugo
            Dim sql As String = _
             " SELECT * " _
             & " FROM " _
             & " " & MBOM_DB_NAME & ".dbo.T_SHISAKU_EVENT WITH (NOLOCK, NOWAIT) " _
             & " WHERE " _
             & " SHISAKU_EVENT_CODE = @ShisakuEventCode "

            Dim db As New EBomDbClient
            Dim param As New TShisakuEventVo
            param.ShisakuEventCode = shisakuEventCode

            Return db.QueryForObject(Of TShisakuEventVo)(sql, param)
        End Function

        ''↓↓2014/07/24 Ⅰ.2.管理項目追加_al) (TES)張 CHG BEGIN
        'Public Sub InsertBySekkeiBuhinEdit(ByVal shisakuEventCode As String, _
        '                            ByVal shisakuBukaCode As String, _
        '                            ByVal shisakuBlockNo As String, _
        '                            ByVal shisakuBlockNoKaiteiNo As String, _
        '                            ByVal koseiMatrix As BuhinKoseiMatrix, _
        '                            ByVal JikyuUmu As String) Implements BuhinEditBaseDao.InsertBySekkeiBuhinEdit
        ''' <summary>
        ''' 部品表編集と部品編集ベースにINSERTする
        ''' </summary>
        ''' <param name="shisakuEventCode">試作イベントコード</param>
        ''' <param name="shisakuBukaCode">部課コード</param>
        ''' <param name="shisakuBlockNo">ブロックNo</param>
        ''' <param name="shisakuBlockNoKaiteiNo">ブロックNo改訂No</param>
        ''' <param name="koseiMatrix">構成マトリクス</param>
        ''' <param name="JikyuUmu">自給品の有無</param>
        ''' <param name="TsukurikataTenkaiFlg">作り方フラグ</param>
        ''' <remarks></remarks>
        Public Sub InsertBySekkeiBuhinEdit(ByVal shisakuEventCode As String, _
                                     ByVal shisakuBukaCode As String, _
                                     ByVal shisakuBlockNo As String, _
                                     ByVal shisakuBlockNoKaiteiNo As String, _
                                     ByVal koseiMatrix As BuhinKoseiMatrix, _
                                     ByVal JikyuUmu As String, _
                                     ByVal TsukurikataTenkaiFlg As String, _
                                     ByVal login As LoginInfo) Implements BuhinEditBaseDao.InsertBySekkeiBuhinEdit
            ''↑↑2014/07/24 Ⅰ.2.管理項目追加_al) (TES)張 CHG END

            Dim param As New TShisakuBuhinEditVo
            Dim buhinNoHyoujiJun As Integer = 0
            Dim aDate As New ShisakuDate

            '配列定義
            Dim sqlHairetu(5000) As String

            For index As Integer = 0 To koseiMatrix.GetInputRowIndexes.Count - 1

                'Dim MakerVo As New TShisakuBuhinEditVo

                'MakerVo = FindByKoutanTorihikisaki(koseiMatrix(index).BuhinNo)

                '部品番号がNULLの項目が存在している'
                If StringUtil.IsEmpty(koseiMatrix(index).BuhinNo) Then
                    Continue For
                End If

                '自給品の削除'
                If StringUtil.Equals(JikyuUmu, "0") Then
                    If StringUtil.IsEmpty(koseiMatrix(index).ShukeiCode) Then
                        If StringUtil.Equals(koseiMatrix(index).SiaShukeiCode, "J") Then
                            Continue For
                        End If
                    ElseIf StringUtil.Equals(koseiMatrix(index).ShukeiCode, "J") Then
                        Continue For
                    End If
                End If
                ''↓↓2014/07/24 Ⅰ.2.管理項目追加_ak) (TES)張 ADD BEGIN
                If StringUtil.Equals(TsukurikataTenkaiFlg, "0") Then
                    koseiMatrix(index).TsukurikataSeisaku = ""
                    koseiMatrix(index).TsukurikataKatashiyou1 = ""
                    koseiMatrix(index).TsukurikataKatashiyou2 = ""
                    koseiMatrix(index).TsukurikataKatashiyou3 = ""
                    koseiMatrix(index).TsukurikataTigu = ""
                    koseiMatrix(index).TsukurikataNounyu = 0
                    koseiMatrix(index).TsukurikataKibo = ""
                End If
                ''↑↑2014/07/24 Ⅰ.2.管理項目追加_ak) (TES)張 ADD END
                '出図予定日が99999999の場合、0を設定する。
                If koseiMatrix(index).ShutuzuYoteiDate = 99999999 Then
                    koseiMatrix(index).ShutuzuYoteiDate = 0
                End If
                If StringUtil.IsEmpty(koseiMatrix(index).ShutuzuYoteiDate) Then
                    koseiMatrix(index).ShutuzuYoteiDate = 0
                End If

                'Nothingなら０を設定
                If StringUtil.IsEmpty(koseiMatrix(index).ShisakuBuhinHi) Then
                    koseiMatrix(index).ShisakuBuhinHi = 0
                End If
                If StringUtil.IsEmpty(koseiMatrix(index).ShisakuKataHi) Then
                    koseiMatrix(index).ShisakuKataHi = 0
                End If
                If StringUtil.IsEmpty(koseiMatrix(index).EditTourokubi) Then
                    koseiMatrix(index).EditTourokubi = 0
                End If
                If StringUtil.IsEmpty(koseiMatrix(index).EditTourokujikan) Then
                    koseiMatrix(index).EditTourokujikan = 0
                End If

                '試作部品編集情報
                '2012/01/23 供給セクション追加
                ''↓↓2014/07/24 Ⅰ.2.管理項目追加_ak) (TES)張 ADD BEGIN
                ''↓↓2014/07/24 Ⅰ.3.設計編集 ベース改修専用化_j) (TES)張 CHG BEGIN


                Dim sql As String
                ''↓↓2014/08/19 Ⅰ.5.EBOM差分出力 ba) (TES)張 CHG START
                ''夜間展開するかどうかの判断を追加
                'sql = " INSERT INTO " & MBOM_DB_NAME & ".dbo.T_SHISAKU_BUHIN_EDIT "
                If login IsNot Nothing Then
                    sql = " INSERT INTO " & MBOM_DB_NAME & ".dbo.T_SHISAKU_BUHIN_EDIT "
                Else
                    sql = " INSERT INTO " & MBOM_DB_NAME & ".dbo.T_SHISAKU_BUHIN_EDIT_EBOM_KANSHI "
                End If
                ''↑↑2014/08/19 Ⅰ.5.EBOM差分出力 ba) (TES)張 CHG END
                sql = sql & " WITH (UPDLOCK) " _
                    & " ( " _
                    & " SHISAKU_EVENT_CODE, " _
                    & " SHISAKU_BUKA_CODE, " _
                    & " SHISAKU_BLOCK_NO, " _
                    & " SHISAKU_BLOCK_NO_KAITEI_NO, " _
                    & " BUHIN_NO_HYOUJI_JUN, " _
                    & " LEVEL, " _
                    & " SHUKEI_CODE, " _
                    & " SIA_SHUKEI_CODE, " _
                    & " GENCYO_CKD_KBN, " _
                    & " KYOUKU_SECTION, " _
                    & " MAKER_CODE, " _
                    & " MAKER_NAME, " _
                    & " BUHIN_NO, " _
                    & " BUHIN_NO_KBN, " _
                    & " BUHIN_NO_KAITEI_NO, " _
                    & " EDA_BAN, " _
                    & " BUHIN_NAME, " _
                    & " SAISHIYOUFUKA, " _
                    & " SHUTUZU_YOTEI_DATE, " _
                    & " BASE_BUHIN_FLG, " _
                    & " ZAISHITU_KIKAKU_1, " _
                    & " ZAISHITU_KIKAKU_2, " _
                    & " ZAISHITU_KIKAKU_3, " _
                    & " ZAISHITU_MEKKI, " _
                    & " TSUKURIKATA_SEISAKU, " _
                    & " TSUKURIKATA_KATASHIYOU_1, " _
                    & " TSUKURIKATA_KATASHIYOU_2, " _
                    & " TSUKURIKATA_KATASHIYOU_3, " _
                    & " TSUKURIKATA_TIGU, " _
                    & " TSUKURIKATA_NOUNYU, " _
                    & " TSUKURIKATA_KIBO, " _
                    & " SHISAKU_BANKO_SURYO, " _
                    & " SHISAKU_BANKO_SURYO_U, " _
                    & " MATERIAL INFO_LENGTH, " _
                    & " MATERIAL INFO_WIDTH, " _
                    & " DATA_ITEM_KAITEI_NO, " _
                    & " DATA_ITEM_AREA NAME, " _
                    & " DATA_ITEM_SET_NAME, " _
                    & " DATA_ITEM_KAITEI_INFO, " _
                    & " SHISAKU_BUHIN_HI, " _
                    & " SHISAKU_KATA_HI, " _
                    & " BIKOU, " _
                    & " BUHIN_NOTE, " _
                    & " EDIT_TOUROKUBI, " _
                    & " EDIT_TOUROKUJIKAN, " _
                    & " KAITEI_HANDAN_FLG, " _
                    & " SHISAKU_LIST_CODE, " _
                    & " CREATED_USER_ID, " _
                    & " CREATED_DATE, " _
                    & " CREATED_TIME, " _
                    & " UPDATED_USER_ID, " _
                    & " UPDATED_DATE, " _
                    & " UPDATED_TIME " _
                    & " ) " _
                    & "VALUES ( " _
                    & "'" & shisakuEventCode & "', " _
                    & "'" & shisakuBukaCode & "', " _
                    & "'" & shisakuBlockNo & "', " _
                    & "'" & shisakuBlockNoKaiteiNo & "', " _
                    & buhinNoHyoujiJun & ", " _
                    & koseiMatrix(index).Level & ", " _
                    & "'" & StringUtil.Nvl(koseiMatrix(index).ShukeiCode) & "', " _
                    & "'" & StringUtil.Nvl(koseiMatrix(index).SiaShukeiCode) & "', " _
                    & "'" & StringUtil.Nvl(koseiMatrix(index).GencyoCkdKbn) & "', " _
                    & "'" & StringUtil.Nvl(koseiMatrix(index).KyoukuSection) & "', " _
                    & "'" & StringUtil.Nvl(koseiMatrix(index).MakerCode) & "', " _
                    & "'" & Trim(koseiMatrix(index).MakerName) & "', " _
                    & "'" & StringUtil.Nvl(koseiMatrix(index).BuhinNo) & "', " _
                    & "'" & StringUtil.Nvl(koseiMatrix(index).BuhinNoKbn) & "', " _
                    & "'" & StringUtil.Nvl(koseiMatrix(index).BuhinNoKaiteiNo) & "', " _
                    & "'" & StringUtil.Nvl(koseiMatrix(index).EdaBan) & "', " _
                    & "'" & Trim(StringUtil.Nvl(koseiMatrix(index).BuhinName)) & "', " _
                    & "'" & StringUtil.Nvl(koseiMatrix(index).Saishiyoufuka) & "', " _
                    & koseiMatrix(index).ShutuzuYoteiDate & ", " _
                    & "'1', " _
                    & "'" & StringUtil.Nvl(koseiMatrix(index).ZaishituKikaku1) & "', " _
                    & "'" & StringUtil.Nvl(koseiMatrix(index).ZaishituKikaku2) & "', " _
                    & "'" & StringUtil.Nvl(koseiMatrix(index).ZaishituKikaku3) & "', " _
                    & "'" & StringUtil.Nvl(koseiMatrix(index).ZaishituMekki) & "', " _
                    & "'" & StringUtil.Nvl(koseiMatrix(index).TsukurikataSeisaku) & "', " _
                    & "'" & StringUtil.Nvl(koseiMatrix(index).TsukurikataKatashiyou1) & "', " _
                    & "'" & StringUtil.Nvl(koseiMatrix(index).TsukurikataKatashiyou2) & "', " _
                    & "'" & StringUtil.Nvl(koseiMatrix(index).TsukurikataTigu) & "', " _
                    & "'" & StringUtil.Nvl(koseiMatrix(index).TsukurikataNounyu) & "', " _
                    & "'" & StringUtil.Nvl(koseiMatrix(index).TsukurikataKibo) & "', " _
                    & "'" & StringUtil.Nvl(koseiMatrix(index).ShisakuBankoSuryo) & "', " _
                    & "'" & StringUtil.Nvl(koseiMatrix(index).ShisakuBankoSuryoU) & "', " _
                    & koseiMatrix(index).MaterialInfoLength & ", " _
                    & koseiMatrix(index).MaterialInfoWidth & ", " _
                    & "'" & StringUtil.Nvl(koseiMatrix(index).DataItemKaiteiNo) & "', " _
                    & "'" & StringUtil.Nvl(koseiMatrix(index).DataItemAreaName) & "', " _
                    & "'" & StringUtil.Nvl(koseiMatrix(index).DataItemSetName) & "', " _
                    & "'" & StringUtil.Nvl(koseiMatrix(index).DataItemKaiteiInfo) & "', " _
                    & koseiMatrix(index).ShisakuBuhinHi & ", " _
                    & koseiMatrix(index).ShisakuKataHi & ", " _
                    & "'" & StringUtil.Nvl(koseiMatrix(index).Bikou) & "', " _
                    & "'" & StringUtil.Nvl(koseiMatrix(index).BuhinNote) & "'," _
                    & koseiMatrix(index).EditTourokubi & ", " _
                    & koseiMatrix(index).EditTourokujikan & ", " _
                    & "'" & koseiMatrix(index).KaiteiHandanFlg & "', " _
                    & "'" & koseiMatrix(index).ShisakuListCode & "', " _
                    & "'" & LoginInfo.Now.UserId & "', " _
                    & "'" & aDate.CurrentDateDbFormat & "', " _
                    & "'" & aDate.CurrentTimeDbFormat & "', " _
                    & "'" & LoginInfo.Now.UserId & "', " _
                    & "'" & aDate.CurrentDateDbFormat & "', " _
                    & "'" & aDate.CurrentTimeDbFormat & "' " _
                    & " ) "
                '20140818 Sakai Add
                '& " TSUKURIKATA_KOUHOU, " _
                '& " TSUKURIKATA_KATASHIYOU, " _
                ''↑↑2014/07/24 Ⅰ.2.管理項目追加_ak) (TES)張 ADD END

                sqlHairetu(index) = sql
                ''↑↑2014/07/24 Ⅰ.3.設計編集 ベース改修専用化_j) (TES)張 CHG END
                '表示順を採番
                buhinNoHyoujiJun = buhinNoHyoujiJun + 1
            Next

            Using db As New SqlAccess(g_kanrihyoIni(DB_KEY_EBOM))
                db.Open()
                db.BeginTransaction()
                For index As Integer = 0 To buhinNoHyoujiJun - 1
                    If Not sqlHairetu(index) Is Nothing Then
                        db.ExecuteNonQuery(sqlHairetu(index))
                    End If
                Next
                db.Commit()
            End Using

            '配列クリア
            Array.Clear(sqlHairetu, 0, sqlHairetu.Length)

        End Sub

        ''' <summary>
        ''' 部品表編集INSTLと部品編集ベースINSTLにINSERTする
        ''' </summary>
        ''' <param name="shisakuEventCode">試作イベントコード</param>
        ''' <param name="shisakuBukaCode">部課コード</param>
        ''' <param name="shisakuBlockNo">ブロックNo</param>
        ''' <param name="shisakuBlockNoKaiteiNo">ブロックNo改訂No</param>
        ''' <param name="koseiMatrix">構成マトリクス</param>
        ''' <remarks></remarks>
        Public Sub InsertBySekkeiBuhinEditInstl(ByVal shisakuEventCode As String, _
                                         ByVal shisakuBukaCode As String, _
                                         ByVal shisakuBlockNo As String, _
                                         ByVal shisakuBlockNoKaiteiNo As String, _
                                         ByVal koseiMatrix As BuhinKoseiMatrix, _
                                         ByVal login As LoginInfo) Implements BuhinEditBaseDao.InsertBySekkeiBuhinEditInstl

            Dim instlHinbanHyoujiJun As Integer = 0
            Dim aDate As New ShisakuDate
            '配列定義
            Dim sqlHairetu(1000) As String

            '縦'
            Dim row As Integer = 0
            Dim col As Integer = 0

            For rowindex As Integer = 0 To koseiMatrix.GetInputRowIndexes.Count - 1

                '部品番号がNULLの項目が存在している'
                If StringUtil.IsEmpty(koseiMatrix(rowindex).BuhinNo) Then
                    Continue For
                End If

                '横'
                For columnIndex As Integer = 0 To koseiMatrix.GetInputInsuColumnIndexes.Count - 1

                    Dim param As New TShisakuBuhinEditInstlVo

                    'Nothingの項目を飛ばす
                    If koseiMatrix.InsuSuryo(rowindex, columnIndex) Is Nothing Then
                        Continue For
                    End If

                    '試作部品編集INSTL情報
                    Dim sql As String
                    ''↓↓2014/08/19 Ⅰ.5.EBOM差分出力 bb) (TES)張 CHG START
                    ''夜間展開するかどうかの判断を追加
                    'sql = " INSERT INTO " & MBOM_DB_NAME & ".dbo.T_SHISAKU_BUHIN_EDIT_INSTL "
                    If login IsNot Nothing Then
                        sql = " INSERT INTO " & MBOM_DB_NAME & ".dbo.T_SHISAKU_BUHIN_EDIT_INSTL "
                    Else
                        sql = " INSERT INTO " & MBOM_DB_NAME & ".dbo.T_SHISAKU_BUHIN_EDIT_INSTL_EBOM_KANSHI "
                    End If
                    ''↑↑2014/08/19 Ⅰ.5.EBOM差分出力 bb) (TES)張 CHG END
                    sql = sql & " WITH (UPDLOCK) " _
                & " ( " _
                & " SHISAKU_EVENT_CODE, " _
                & " SHISAKU_BUKA_CODE, " _
                & " SHISAKU_BLOCK_NO, " _
                & " SHISAKU_BLOCK_NO_KAITEI_NO, " _
                & " BUHIN_NO_HYOUJI_JUN, " _
                & " INSTL_HINBAN_HYOUJI_JUN, " _
                & " INSU_SURYO, " _
                & " SAISYU_KOUSHINBI, " _
                & " CREATED_USER_ID, " _
                & " CREATED_DATE, " _
                & " CREATED_TIME, " _
                & " UPDATED_USER_ID, " _
                & " UPDATED_DATE, " _
                & " UPDATED_TIME " _
                & " ) " _
                & " VALUES ( " _
                & "'" & shisakuEventCode & "', " _
                & "'" & shisakuBukaCode & "', " _
                & "'" & shisakuBlockNo & "', " _
                & "'" & shisakuBlockNoKaiteiNo & "', " _
                & rowindex & ", " _
                & columnIndex & ", " _
                & koseiMatrix.InsuSuryo(rowindex, columnIndex) & ", " _
                & "'" & Integer.Parse(Replace(aDate.CurrentDateDbFormat, "-", "")) & "', " _
                & "'" & LoginInfo.Now.UserId & "', " _
                & "'" & aDate.CurrentDateDbFormat & "', " _
                & "'" & aDate.CurrentTimeDbFormat & "', " _
                & "'" & LoginInfo.Now.UserId & "', " _
                & "'" & aDate.CurrentDateDbFormat & "', " _
                & "'" & aDate.CurrentTimeDbFormat & "' " _
                & " ) "

                    sqlHairetu(columnIndex) = sql

                    instlHinbanHyoujiJun = columnIndex

                Next

                Using db As New SqlAccess(g_kanrihyoIni(DB_KEY_EBOM))
                    db.Open()
                    db.BeginTransaction()
                    For index As Integer = 0 To instlHinbanHyoujiJun
                        If Not StringUtil.IsEmpty(sqlHairetu(index)) Then
                            db.ExecuteNonQuery(sqlHairetu(index))
                        End If
                    Next
                    db.Commit()
                End Using

                '配列クリア
                Array.Clear(sqlHairetu, 0, sqlHairetu.Length)

                row = row + 1
            Next

        End Sub

        ''↓↓2014/07/23 Ⅰ.2.管理項目追加_ai) (TES)張 CHG BEGIN
        'Public Sub InsertBySekkeiBuhinEditEvent(ByVal shisakuEventCode As String, _
        '                                        ByVal shisakuBukaCode As String, _
        '                                        ByVal shisakuBlockNo As String, _
        '                                        ByVal shisakuBlockNoKaiteiNo As String, _
        '                                        ByVal koseiMatrix As BuhinKoseiMatrix, _
        '                                        ByVal JikyuUmu As String) Implements BuhinEditBaseDao.InsertBySekkeiBuhinEditEvent
        ''' <summary>
        ''' 部品表編集と部品編集ベースにINSERTする
        ''' </summary>
        ''' <param name="shisakuEventCode">試作イベントコード</param>
        ''' <param name="shisakuBukaCode">部課コード</param>
        ''' <param name="shisakuBlockNo">ブロックNo</param>
        ''' <param name="shisakuBlockNoKaiteiNo">ブロックNo改訂No</param>
        ''' <param name="koseiMatrix">構成マトリクス</param>
        ''' <param name="JikyuUmu">自給品の有無</param>
        ''' <param name="TsukurikataTenkaiFlg">作り方フラグ</param>
        ''' <remarks></remarks>
        Public Sub InsertBySekkeiBuhinEditEvent(ByVal shisakuEventCode As String, _
                                                ByVal shisakuBukaCode As String, _
                                                ByVal shisakuBlockNo As String, _
                                                ByVal shisakuBlockNoKaiteiNo As String, _
                                                ByVal koseiMatrix As BuhinKoseiMatrix, _
                                                ByVal JikyuUmu As String, _
                                                ByVal TsukurikataTenkaiFlg As String) Implements BuhinEditBaseDao.InsertBySekkeiBuhinEditEvent
            ''↑↑2014/07/23 Ⅰ.2.管理項目追加_ai) (TES)張 CHG END

            Dim param As New TShisakuBuhinEditVo
            Dim buhinNoHyoujiJun As Integer = 0
            Dim aDate As New ShisakuDate

            '配列定義
            Dim sqlHairetu(5000) As String

            '先にレベル0'
            For index As Integer = 0 To koseiMatrix.GetInputRowIndexes.Count - 1

                '部品番号がNULLの項目が存在している'
                If StringUtil.IsEmpty(koseiMatrix(index).BuhinNo) Then
                    Continue For
                End If

                '自給品の削除'
                If StringUtil.Equals(JikyuUmu, "0") Then
                    If StringUtil.IsEmpty(koseiMatrix(index).ShukeiCode) Then
                        If StringUtil.Equals(koseiMatrix(index).SiaShukeiCode, "J") Then
                            Continue For
                        End If
                    ElseIf StringUtil.Equals(koseiMatrix(index).ShukeiCode, "J") Then
                        Continue For
                    End If
                End If

                ''↓↓2014/07/23 Ⅰ.2.管理項目追加_ad) (TES)張 ADD BEGIN
                '作り方
                If StringUtil.Equals(TsukurikataTenkaiFlg, "0") Then
                    koseiMatrix(index).TsukurikataSeisaku = ""
                    koseiMatrix(index).TsukurikataKatashiyou1 = ""
                    koseiMatrix(index).TsukurikataKatashiyou2 = ""
                    koseiMatrix(index).TsukurikataKatashiyou3 = ""
                    koseiMatrix(index).TsukurikataTigu = ""
                    koseiMatrix(index).TsukurikataNounyu = 0
                    koseiMatrix(index).TsukurikataKibo = ""
                End If
                ''↑↑2014/07/23 Ⅰ.2.管理項目追加_ad) (TES)張 ADD END


                '出図予定日が99999999の場合、0を設定する。
                If koseiMatrix(index).ShutuzuYoteiDate = 99999999 Then
                    koseiMatrix(index).ShutuzuYoteiDate = 0
                End If
                If StringUtil.IsEmpty(koseiMatrix(index).ShutuzuYoteiDate) Then
                    koseiMatrix(index).ShutuzuYoteiDate = 0
                End If

                'Nothingなら０を設定
                If StringUtil.IsEmpty(koseiMatrix(index).ShisakuBuhinHi) Then
                    koseiMatrix(index).ShisakuBuhinHi = 0
                End If
                If StringUtil.IsEmpty(koseiMatrix(index).ShisakuKataHi) Then
                    koseiMatrix(index).ShisakuKataHi = 0
                End If
                If StringUtil.IsEmpty(koseiMatrix(index).EditTourokubi) Then
                    koseiMatrix(index).EditTourokubi = 0
                End If
                If StringUtil.IsEmpty(koseiMatrix(index).EditTourokujikan) Then
                    koseiMatrix(index).EditTourokujikan = 0
                End If

                '↓↓↓2014/12/26 メタル項目を追加 (DANIEL)柳沼 ADD BEGIN
                'Nothingなら０を設定
                If StringUtil.IsEmpty(koseiMatrix(index).MaterialInfoLength) Then
                    koseiMatrix(index).MaterialInfoLength = 0
                End If
                If StringUtil.IsEmpty(koseiMatrix(index).MaterialInfoWidth) Then
                    koseiMatrix(index).MaterialInfoWidth = 0
                End If
                '↑↑↑2014/12/26 メタル項目を追加 (DANIEL)柳沼 ADD END


                'ba) (TES)施
                '試作部品編集情報
                '2012/01/23 供給セクション追加
                Dim sb As New StringBuilder
                With sb
                    .Remove(0, .Length)
                    .AppendLine(" INSERT INTO " & MBOM_DB_NAME & ".dbo.T_SHISAKU_BUHIN_EDIT ")
                    .AppendLine(" WITH (UPDLOCK) ")
                    .AppendLine(" ( ")
                    .AppendLine(" SHISAKU_EVENT_CODE, ")
                    .AppendLine(" SHISAKU_BUKA_CODE, ")
                    .AppendLine(" SHISAKU_BLOCK_NO, ")
                    .AppendLine(" SHISAKU_BLOCK_NO_KAITEI_NO, ")
                    .AppendLine(" BUHIN_NO_HYOUJI_JUN, ")
                    .AppendLine(" LEVEL, ")
                    .AppendLine(" SHUKEI_CODE, ")
                    .AppendLine(" SIA_SHUKEI_CODE, ")
                    .AppendLine(" GENCYO_CKD_KBN, ")
                    .AppendLine(" KYOUKU_SECTION, ")
                    .AppendLine(" MAKER_CODE, ")
                    .AppendLine(" MAKER_NAME, ")
                    .AppendLine(" BUHIN_NO, ")
                    .AppendLine(" BUHIN_NO_KBN, ")
                    .AppendLine(" BUHIN_NO_KAITEI_NO, ")
                    .AppendLine(" EDA_BAN, ")
                    .AppendLine(" BUHIN_NAME, ")
                    .AppendLine(" SAISHIYOUFUKA, ")
                    .AppendLine(" SHUTUZU_YOTEI_DATE, ")
                    ''↓↓2014/07/24 Ⅰ.3.設計編集 ベース改修専用化_i) (TES)張 ADD BEGIN
                    .AppendLine(" BASE_BUHIN_FLG, ")
                    ''↑↑2014/07/24 Ⅰ.3.設計編集 ベース改修専用化_i) (TES)張 ADD END
                    .AppendLine(" ZAISHITU_KIKAKU_1, ")
                    .AppendLine(" ZAISHITU_KIKAKU_2, ")
                    .AppendLine(" ZAISHITU_KIKAKU_3, ")
                    .AppendLine(" ZAISHITU_MEKKI, ")

                    ''↓↓2014/07/23 Ⅰ.2.管理項目追加_ad) (TES)張 ADD BEGIN
                    .AppendLine(" TSUKURIKATA_SEISAKU, ")
                    '20140818 Sakai Add
                    '.AppendLine(" TSUKURIKATA_KOUHOU, ")
                    '.AppendLine(" TSUKURIKATA_KATASHIYOU, ")
                    .AppendLine(" TSUKURIKATA_KATASHIYOU_1, ")
                    .AppendLine(" TSUKURIKATA_KATASHIYOU_2, ")
                    .AppendLine(" TSUKURIKATA_KATASHIYOU_3, ")
                    .AppendLine(" TSUKURIKATA_TIGU, ")
                    .AppendLine(" TSUKURIKATA_NOUNYU, ")
                    .AppendLine(" TSUKURIKATA_KIBO, ")
                    ''↑↑2014/07/23 Ⅰ.2.管理項目追加_ad) (TES)張 ADD END

                    .AppendLine(" SHISAKU_BANKO_SURYO, ")
                    .AppendLine(" SHISAKU_BANKO_SURYO_U, ")


                    '↓↓↓2014/12/26 メタル項目を追加 (DANIEL)柳沼 ADD BEGIN
                    .AppendLine(" MATERIAL_INFO_LENGTH, ")
                    .AppendLine(" MATERIAL_INFO_WIDTH, ")
                    .AppendLine(" DATA_ITEM_KAITEI_NO, ")
                    .AppendLine(" DATA_ITEM_AREA_NAME, ")
                    .AppendLine(" DATA_ITEM_SET_NAME, ")
                    .AppendLine(" DATA_ITEM_KAITEI_INFO, ")
                    '↑↑↑2014/12/26 メタル項目を追加 (DANIEL)柳沼 ADD END


                    .AppendLine(" SHISAKU_BUHIN_HI, ")
                    .AppendLine(" SHISAKU_KATA_HI, ")
                    .AppendLine(" BIKOU, ")
                    .AppendLine(" BUHIN_NOTE, ")
                    .AppendLine(" EDIT_TOUROKUBI, ")
                    .AppendLine(" EDIT_TOUROKUJIKAN, ")
                    .AppendLine(" KAITEI_HANDAN_FLG, ")
                    .AppendLine(" SHISAKU_LIST_CODE, ")
                    .AppendLine(" CREATED_USER_ID, ")
                    .AppendLine(" CREATED_DATE, ")
                    .AppendLine(" CREATED_TIME, ")
                    .AppendLine(" UPDATED_USER_ID, ")
                    .AppendLine(" UPDATED_DATE, ")
                    .AppendLine(" UPDATED_TIME ")
                    .AppendLine(" ) ")
                    .AppendLine("VALUES ( ")
                    .AppendLine("'" & shisakuEventCode & "', ")
                    .AppendLine("'" & shisakuBukaCode & "', ")
                    .AppendLine("'" & shisakuBlockNo & "', ")
                    .AppendLine("'" & shisakuBlockNoKaiteiNo & "', ")
                    .AppendLine(buhinNoHyoujiJun & ", ")
                    .AppendLine(koseiMatrix(index).Level & ", ")
                    .AppendLine("'" & koseiMatrix(index).ShukeiCode & "', ")
                    .AppendLine("'" & koseiMatrix(index).SiaShukeiCode & "', ")
                    .AppendLine("'" & koseiMatrix(index).GencyoCkdKbn & "', ")
                    .AppendLine("'" & koseiMatrix(index).KyoukuSection & "', ")
                    .AppendLine("'" & koseiMatrix(index).MakerCode & "', ")
                    .AppendLine("'" & Trim(koseiMatrix(index).MakerName) & "', ")
                    .AppendLine("'" & koseiMatrix(index).BuhinNo & "', ")
                    .AppendLine("'" & koseiMatrix(index).BuhinNoKbn & "', ")
                    .AppendLine("'" & koseiMatrix(index).BuhinNoKaiteiNo & "', ")
                    .AppendLine("'" & koseiMatrix(index).EdaBan & "', ")
                    .AppendLine("'" & Trim(koseiMatrix(index).BuhinName) & "', ")
                    .AppendLine("'" & koseiMatrix(index).Saishiyoufuka & "', ")
                    .AppendLine(koseiMatrix(index).ShutuzuYoteiDate & ", ")
                    ''↓↓2014/07/24 Ⅰ.3.設計編集 ベース改修専用化_i) (TES)張 ADD BEGIN
                    .AppendLine("'1', ")
                    ''↑↑2014/07/24 Ⅰ.3.設計編集 ベース改修専用化_i) (TES)張 ADD END
                    .AppendLine("'" & koseiMatrix(index).ZaishituKikaku1 & "', ")
                    .AppendLine("'" & koseiMatrix(index).ZaishituKikaku2 & "', ")
                    .AppendLine("'" & koseiMatrix(index).ZaishituKikaku3 & "', ")
                    .AppendLine("'" & koseiMatrix(index).ZaishituMekki & "', ")

                    ''↓↓2014/07/23 Ⅰ.2.管理項目追加_ad) (TES)張 ADD BEGIN
                    .AppendLine("'" & koseiMatrix(index).TsukurikataSeisaku & "', ")
                    .AppendLine("'" & koseiMatrix(index).TsukurikataKatashiyou1 & "', ")
                    .AppendLine("'" & koseiMatrix(index).TsukurikataKatashiyou2 & "', ")
                    .AppendLine("'" & koseiMatrix(index).TsukurikataTigu & "', ")
                    .AppendLine("'" & koseiMatrix(index).TsukurikataNounyu & "', ")
                    .AppendLine("'" & koseiMatrix(index).TsukurikataKibo & "', ")
                    ''↑↑2014/07/23 Ⅰ.2.管理項目追加_ad) (TES)張 ADD END

                    .AppendLine("'" & koseiMatrix(index).ShisakuBankoSuryo & "', ")
                    .AppendLine("'" & koseiMatrix(index).ShisakuBankoSuryoU & "', ")


                    '↓↓↓2014/12/26 メタル項目を追加 (DANIEL)柳沼 ADD BEGIN
                    .AppendLine(koseiMatrix(index).MaterialInfoLength & ", ")
                    .AppendLine(koseiMatrix(index).MaterialInfoWidth & ", ")
                    .AppendLine("'" & koseiMatrix(index).DataItemKaiteiNo & "', ")
                    .AppendLine("'" & koseiMatrix(index).DataItemAreaName & "', ")
                    .AppendLine("'" & koseiMatrix(index).DataItemSetName & "', ")
                    .AppendLine("'" & koseiMatrix(index).DataItemKaiteiInfo & "', ")
                    '↑↑↑2014/12/26 メタル項目を追加 (DANIEL)柳沼 ADD END


                    .AppendLine(koseiMatrix(index).ShisakuBuhinHi & ", ")
                    .AppendLine(koseiMatrix(index).ShisakuKataHi & ", ")
                    .AppendLine("'" & koseiMatrix(index).Bikou & "', ")
                    .AppendLine("'" & koseiMatrix(index).BuhinNote & "',")
                    .AppendLine(koseiMatrix(index).EditTourokubi & ", ")
                    .AppendLine(koseiMatrix(index).EditTourokujikan & ", ")
                    .AppendLine("'" & koseiMatrix(index).KaiteiHandanFlg & "', ")
                    .AppendLine("'" & koseiMatrix(index).ShisakuListCode & "', ")
                    .AppendLine("'" & LoginInfo.Now.UserId & "', ")
                    .AppendLine("'" & aDate.CurrentDateDbFormat & "', ")
                    .AppendLine("'" & aDate.CurrentTimeDbFormat & "', ")
                    .AppendLine("'" & LoginInfo.Now.UserId & "', ")
                    .AppendLine("'" & aDate.CurrentDateDbFormat & "', ")
                    .AppendLine("'" & aDate.CurrentTimeDbFormat & "' ")
                    .AppendLine(" ) ")
                End With

                '試作部品編集情報（ベース）
                Dim sb2 As New StringBuilder
                With sb2
                    .Remove(0, .Length)
                    .AppendLine(" INSERT INTO " & MBOM_DB_NAME & ".dbo.T_SHISAKU_BUHIN_EDIT_BASE ")
                    .AppendLine(" WITH (UPDLOCK) ")
                    .AppendLine(" ( ")
                    .AppendLine(" SHISAKU_EVENT_CODE, ")
                    .AppendLine(" SHISAKU_BUKA_CODE, ")
                    .AppendLine(" SHISAKU_BLOCK_NO, ")
                    .AppendLine(" SHISAKU_BLOCK_NO_KAITEI_NO, ")
                    .AppendLine(" BUHIN_NO_HYOUJI_JUN, ")
                    .AppendLine(" LEVEL, ")
                    .AppendLine(" SHUKEI_CODE, ")
                    .AppendLine(" SIA_SHUKEI_CODE, ")
                    .AppendLine(" GENCYO_CKD_KBN, ")
                    .AppendLine(" KYOUKU_SECTION, ")
                    .AppendLine(" MAKER_CODE, ")
                    .AppendLine(" MAKER_NAME, ")
                    .AppendLine(" BUHIN_NO, ")
                    .AppendLine(" BUHIN_NO_KBN, ")
                    .AppendLine(" BUHIN_NO_KAITEI_NO, ")
                    .AppendLine(" EDA_BAN, ")
                    .AppendLine(" BUHIN_NAME, ")
                    .AppendLine(" SAISHIYOUFUKA, ")
                    .AppendLine(" SHUTUZU_YOTEI_DATE, ")
                    ''↓↓2014/07/24 Ⅰ.3.設計編集 ベース改修専用化_i) (TES)張 ADD BEGIN
                    .AppendLine(" BASE_BUHIN_FLG, ")
                    ''↑↑2014/07/24 Ⅰ.3.設計編集 ベース改修専用化_i) (TES)張 ADD END
                    .AppendLine(" ZAISHITU_KIKAKU_1, ")
                    .AppendLine(" ZAISHITU_KIKAKU_2, ")
                    .AppendLine(" ZAISHITU_KIKAKU_3, ")
                    .AppendLine(" ZAISHITU_MEKKI, ")

                    ''↓↓2014/07/23 Ⅰ.2.管理項目追加_ad) (TES)張 ADD BEGIN
                    .AppendLine(" TSUKURIKATA_SEISAKU, ")
                    '20140818 Sakai Add
                    '.AppendLine(" TSUKURIKATA_KOUHOU, ")
                    '.AppendLine(" TSUKURIKATA_KATASHIYOU, ")
                    .AppendLine(" TSUKURIKATA_KATASHIYOU_1, ")
                    .AppendLine(" TSUKURIKATA_KATASHIYOU_2, ")
                    .AppendLine(" TSUKURIKATA_KATASHIYOU_3, ")
                    .AppendLine(" TSUKURIKATA_TIGU, ")
                    .AppendLine(" TSUKURIKATA_NOUNYU, ")
                    .AppendLine(" TSUKURIKATA_KIBO, ")
                    ''↑↑2014/07/23 Ⅰ.2.管理項目追加_ad) (TES)張 ADD END

                    .AppendLine(" SHISAKU_BANKO_SURYO, ")
                    .AppendLine(" SHISAKU_BANKO_SURYO_U, ")


                    '↓↓↓2014/12/26 メタル項目を追加 (DANIEL)柳沼 ADD BEGIN
                    .AppendLine(" MATERIAL_INFO_LENGTH, ")
                    .AppendLine(" MATERIAL_INFO_WIDTH, ")
                    .AppendLine(" DATA_ITEM_KAITEI_NO, ")
                    .AppendLine(" DATA_ITEM_AREA_NAME, ")
                    .AppendLine(" DATA_ITEM_SET_NAME, ")
                    .AppendLine(" DATA_ITEM_KAITEI_INFO, ")
                    '↑↑↑2014/12/26 メタル項目を追加 (DANIEL)柳沼 ADD END


                    .AppendLine(" SHISAKU_BUHIN_HI, ")
                    .AppendLine(" SHISAKU_KATA_HI, ")
                    .AppendLine(" BIKOU, ")
                    .AppendLine(" BUHIN_NOTE, ")
                    .AppendLine(" EDIT_TOUROKUBI, ")
                    .AppendLine(" EDIT_TOUROKUJIKAN, ")
                    .AppendLine(" KAITEI_HANDAN_FLG, ")
                    .AppendLine(" SHISAKU_LIST_CODE, ")
                    .AppendLine(" CREATED_USER_ID, ")
                    .AppendLine(" CREATED_DATE, ")
                    .AppendLine(" CREATED_TIME, ")
                    .AppendLine(" UPDATED_USER_ID, ")
                    .AppendLine(" UPDATED_DATE, ")
                    .AppendLine(" UPDATED_TIME ")
                    .AppendLine(" ) ")
                    .AppendLine("VALUES ( ")
                    .AppendLine("'" & shisakuEventCode & "', ")
                    .AppendLine("'" & shisakuBukaCode & "', ")
                    .AppendLine("'" & shisakuBlockNo & "', ")
                    .AppendLine("'" & shisakuBlockNoKaiteiNo & "', ")
                    .AppendLine(buhinNoHyoujiJun & ", ")
                    .AppendLine(koseiMatrix(index).Level & ", ")
                    .AppendLine("'" & koseiMatrix(index).ShukeiCode & "', ")
                    .AppendLine("'" & koseiMatrix(index).SiaShukeiCode & "', ")
                    .AppendLine("'" & koseiMatrix(index).GencyoCkdKbn & "', ")
                    .AppendLine("'" & koseiMatrix(index).KyoukuSection & "', ")
                    .AppendLine("'" & koseiMatrix(index).MakerCode & "', ")
                    .AppendLine("'" & Trim(koseiMatrix(index).MakerName) & "', ")
                    .AppendLine("'" & koseiMatrix(index).BuhinNo & "', ")
                    .AppendLine("'" & koseiMatrix(index).BuhinNoKbn & "', ")
                    .AppendLine("'" & koseiMatrix(index).BuhinNoKaiteiNo & "', ")
                    .AppendLine("'" & koseiMatrix(index).EdaBan & "', ")
                    .AppendLine("'" & Trim(koseiMatrix(index).BuhinName) & "', ")
                    .AppendLine("'" & koseiMatrix(index).Saishiyoufuka & "', ")
                    .AppendLine(koseiMatrix(index).ShutuzuYoteiDate & ", ")
                    ''↓↓2014/07/24 Ⅰ.3.設計編集 ベース改修専用化_i) (TES)張 ADD BEGIN
                    .AppendLine("'1', ")
                    ''↑↑2014/07/24 Ⅰ.3.設計編集 ベース改修専用化_i) (TES)張 ADD END
                    .AppendLine("'" & koseiMatrix(index).ZaishituKikaku1 & "', ")
                    .AppendLine("'" & koseiMatrix(index).ZaishituKikaku2 & "', ")
                    .AppendLine("'" & koseiMatrix(index).ZaishituKikaku3 & "', ")
                    .AppendLine("'" & koseiMatrix(index).ZaishituMekki & "', ")

                    ''↓↓2014/07/23 Ⅰ.2.管理項目追加_ad) (TES)張 ADD BEGIN
                    .AppendLine("'" & koseiMatrix(index).TsukurikataSeisaku & "', ")
                    .AppendLine("'" & koseiMatrix(index).TsukurikataKatashiyou1 & "', ")
                    .AppendLine("'" & koseiMatrix(index).TsukurikataKatashiyou2 & "', ")
                    .AppendLine("'" & koseiMatrix(index).TsukurikataTigu & "', ")
                    .AppendLine("'" & koseiMatrix(index).TsukurikataNounyu & "', ")
                    .AppendLine("'" & koseiMatrix(index).TsukurikataKibo & "', ")
                    ''↑↑2014/07/23 Ⅰ.2.管理項目追加_ad) (TES)張 ADD END

                    .AppendLine("'" & koseiMatrix(index).ShisakuBankoSuryo & "', ")
                    .AppendLine("'" & koseiMatrix(index).ShisakuBankoSuryoU & "', ")


                    '↓↓↓2014/12/26 メタル項目を追加 (DANIEL)柳沼 ADD BEGIN
                    .AppendLine(koseiMatrix(index).MaterialInfoLength & ", ")
                    .AppendLine(koseiMatrix(index).MaterialInfoWidth & ", ")
                    .AppendLine("'" & koseiMatrix(index).DataItemKaiteiNo & "', ")
                    .AppendLine("'" & koseiMatrix(index).DataItemAreaName & "', ")
                    .AppendLine("'" & koseiMatrix(index).DataItemSetName & "', ")
                    .AppendLine("'" & koseiMatrix(index).DataItemKaiteiInfo & "', ")
                    '↑↑↑2014/12/26 メタル項目を追加 (DANIEL)柳沼 ADD END


                    .AppendLine(koseiMatrix(index).ShisakuBuhinHi & ", ")
                    .AppendLine(koseiMatrix(index).ShisakuKataHi & ", ")
                    .AppendLine("'" & koseiMatrix(index).Bikou & "', ")
                    .AppendLine("'" & koseiMatrix(index).BuhinNote & "',")
                    .AppendLine(koseiMatrix(index).EditTourokubi & ", ")
                    .AppendLine(koseiMatrix(index).EditTourokujikan & ", ")
                    .AppendLine("'" & koseiMatrix(index).KaiteiHandanFlg & "', ")
                    .AppendLine("'" & koseiMatrix(index).ShisakuListCode & "', ")
                    .AppendLine("'" & LoginInfo.Now.UserId & "', ")
                    .AppendLine("'" & aDate.CurrentDateDbFormat & "', ")
                    .AppendLine("'" & aDate.CurrentTimeDbFormat & "', ")
                    .AppendLine("'" & LoginInfo.Now.UserId & "', ")
                    .AppendLine("'" & aDate.CurrentDateDbFormat & "', ")
                    .AppendLine("'" & aDate.CurrentTimeDbFormat & "' ")
                    .AppendLine(" ) ")
                End With

                '本体とベースを合体
                sqlHairetu(buhinNoHyoujiJun) = sb.ToString + sb2.ToString

                '表示順を採番
                buhinNoHyoujiJun = buhinNoHyoujiJun + 1
            Next

            Using db As New SqlAccess(g_kanrihyoIni(DB_KEY_EBOM))
                db.Open()
                db.BeginTransaction()
                For index As Integer = 0 To buhinNoHyoujiJun - 1
                    If Not sqlHairetu(index) Is Nothing Then
                        db.ExecuteNonQuery(sqlHairetu(index))
                    End If
                Next
                db.Commit()
            End Using

            '配列クリア
            Array.Clear(sqlHairetu, 0, sqlHairetu.Length)

        End Sub

        ''' <summary>
        ''' 部品表編集INSTLと部品編集ベースINSTLにINSERTする
        ''' </summary>
        ''' <param name="shisakuEventCode">試作イベントコード</param>
        ''' <param name="shisakuBukaCode">部課コード</param>
        ''' <param name="shisakuBlockNo">ブロックNo</param>
        ''' <param name="shisakuBlockNoKaiteiNo">ブロックNo改訂No</param>
        ''' <param name="koseiMatrix">構成マトリクス</param>
        ''' <param name="JikyuUmu">自給品の有無</param>
        ''' <remarks></remarks>
        Public Sub InsertBySekkeiBuhinEditInstlEvent(ByVal shisakuEventCode As String, _
                                                     ByVal shisakuBukaCode As String, _
                                                     ByVal shisakuBlockNo As String, _
                                                     ByVal shisakuBlockNoKaiteiNo As String, _
                                                     ByVal koseiMatrix As BuhinKoseiMatrix, _
                                                     ByVal JikyuUmu As String) Implements BuhinEditBaseDao.InsertBySekkeiBuhinEditInstlEvent

            Dim instlHinbanHyoujiJun As Integer = 0
            Dim aDate As New ShisakuDate
            '配列定義
            Dim sqlHairetu(1000) As String

            '縦'
            Dim row As Integer = 0
            Dim col As Integer = 0
            Dim flag As Boolean
            Dim BuhinNoHyoujiJun As Integer = 0

            For rowindex As Integer = 0 To koseiMatrix.GetInputRowIndexes.Count - 1
                flag = False
                '自給品の削除'
                If StringUtil.Equals(JikyuUmu, "0") Then
                    If StringUtil.IsEmpty(koseiMatrix(rowindex).ShukeiCode) Then
                        If StringUtil.Equals(koseiMatrix(rowindex).SiaShukeiCode, "J") Then
                            Continue For
                        End If
                    ElseIf StringUtil.Equals(koseiMatrix(rowindex).ShukeiCode, "J") Then
                        Continue For
                    End If
                End If
                For Each columnIndex As Integer In koseiMatrix.GetInputInsuColumnIndexes
                    Dim param As New TShisakuBuhinEditInstlVo
                    If koseiMatrix.InsuSuryo(rowindex, columnIndex) Is Nothing Then
                        Continue For
                    End If

                    '構成の部品番号表示順と同じ員数のみ登録'

                    ''↓↓2014/08/05 Ⅰ.5.EBOM差分出力  bb) (TES)施 ADD BEGIN

                    Dim sql As String
                    If True Then
                        sql = " INSERT INTO " & MBOM_DB_NAME & ".dbo.T_SHISAKU_BUHIN_EDIT_INSTL "
                    Else
                        sql = " INSERT INTO " & MBOM_DB_NAME & ".dbo.T_SHISAKU_BUHIN_EDIT_INSTL_EBOM_KANSHI "
                    End If
                    ''↑↑2014/08/05 Ⅰ.5.EBOM差分出力 bb) (TES)施 ADD END
                    sql = sql & " WITH (UPDLOCK) " _
                         & " ( " _
                         & " SHISAKU_EVENT_CODE, " _
                         & " SHISAKU_BUKA_CODE, " _
                         & " SHISAKU_BLOCK_NO, " _
                         & " SHISAKU_BLOCK_NO_KAITEI_NO, " _
                         & " BUHIN_NO_HYOUJI_JUN, " _
                         & " INSTL_HINBAN_HYOUJI_JUN, " _
                         & " INSU_SURYO, " _
                         & " SAISYU_KOUSHINBI, " _
                         & " CREATED_USER_ID, " _
                         & " CREATED_DATE, " _
                         & " CREATED_TIME, " _
                         & " UPDATED_USER_ID, " _
                         & " UPDATED_DATE, " _
                         & " UPDATED_TIME " _
                         & " ) " _
                         & " VALUES ( " _
                         & "'" & shisakuEventCode & "', " _
                         & "'" & shisakuBukaCode & "', " _
                         & "'" & shisakuBlockNo & "', " _
                         & "'" & shisakuBlockNoKaiteiNo & "', " _
                         & BuhinNoHyoujiJun & ", " _
                         & columnIndex & ", " _
                         & koseiMatrix.InsuSuryo(rowindex, columnIndex) & ", " _
                         & "'" & Integer.Parse(Replace(aDate.CurrentDateDbFormat, "-", "")) & "', " _
                         & "'" & LoginInfo.Now.UserId & "', " _
                         & "'" & aDate.CurrentDateDbFormat & "', " _
                         & "'" & aDate.CurrentTimeDbFormat & "', " _
                         & "'" & LoginInfo.Now.UserId & "', " _
                         & "'" & aDate.CurrentDateDbFormat & "', " _
                         & "'" & aDate.CurrentTimeDbFormat & "' " _
                         & " ) "

                    '試作部品編集INSTL情報（ベース）
                    Dim sqlBase As String
                    ''↓↓2014/08/05 Ⅰ.5.EBOM差分出力 bb) (TES)施 ADD BEGIN
                    If 1 = 1 Then  '夜間展開処理からの起動でなければ sxc

                        sqlBase = " INSERT INTO " & MBOM_DB_NAME & ".dbo.T_SHISAKU_BUHIN_EDIT_INSTL_BASE " _
                        & " WITH (UPDLOCK) " _
                        & " ( " _
                        & " SHISAKU_EVENT_CODE, " _
                        & " SHISAKU_BUKA_CODE, " _
                        & " SHISAKU_BLOCK_NO, " _
                        & " SHISAKU_BLOCK_NO_KAITEI_NO, " _
                        & " BUHIN_NO_HYOUJI_JUN, " _
                        & " INSTL_HINBAN_HYOUJI_JUN, " _
                        & " INSU_SURYO, " _
                        & " SAISYU_KOUSHINBI, " _
                        & " CREATED_USER_ID, " _
                        & " CREATED_DATE, " _
                        & " CREATED_TIME, " _
                        & " UPDATED_USER_ID, " _
                        & " UPDATED_DATE, " _
                        & " UPDATED_TIME " _
                        & " ) " _
                        & " VALUES ( " _
                        & "'" & shisakuEventCode & "', " _
                        & "'" & shisakuBukaCode & "', " _
                        & "'" & shisakuBlockNo & "', " _
                        & "'" & shisakuBlockNoKaiteiNo & "', " _
                        & BuhinNoHyoujiJun & ", " _
                        & columnIndex & ", " _
                        & koseiMatrix.InsuSuryo(rowindex, columnIndex) & ", " _
                        & "'" & Integer.Parse(Replace(aDate.CurrentDateDbFormat, "-", "")) & "', " _
                        & "'" & LoginInfo.Now.UserId & "', " _
                        & "'" & aDate.CurrentDateDbFormat & "', " _
                        & "'" & aDate.CurrentTimeDbFormat & "', " _
                        & "'" & LoginInfo.Now.UserId & "', " _
                        & "'" & aDate.CurrentDateDbFormat & "', " _
                        & "'" & aDate.CurrentTimeDbFormat & "' " _
                        & " ) "
                    Else
                        sqlBase = ""
                    End If
                    ''↑↑2014/08/05 Ⅰ.5.EBOM差分出力 bb) (TES)施 ADD END
                    '本体とベースを合体
                    sqlHairetu(columnIndex) = sql + sqlBase

                    instlHinbanHyoujiJun = columnIndex
                    flag = True
                Next
                Using db As New SqlAccess(g_kanrihyoIni(DB_KEY_EBOM))
                    db.Open()
                    db.BeginTransaction()
                    For index As Integer = 0 To instlHinbanHyoujiJun
                        If Not StringUtil.IsEmpty(sqlHairetu(index)) Then
                            db.ExecuteNonQuery(sqlHairetu(index))
                        End If
                    Next
                    db.Commit()
                End Using

                '配列クリア
                Array.Clear(sqlHairetu, 0, sqlHairetu.Length)
                If flag Then
                    BuhinNoHyoujiJun = BuhinNoHyoujiJun + 1
                End If
            Next

        End Sub

        ''' <summary>
        ''' 設計ブロックINSTLの表示順を更新する
        ''' </summary>
        ''' <param name="shisakuEventCode">試作イベントコード</param>
        ''' <param name="shisakuBukaCode">部課コード</param>
        ''' <param name="shisakuBlockNo">ブロックNo</param>
        ''' <param name="koseiMatrix">構成マトリクス</param>
        ''' <remarks></remarks>
        Public Sub UpdateBySekkeiBlockInstl(ByVal shisakuEventCode As String, _
                                            ByVal shisakuBukaCode As String, _
                                            ByVal shisakuBlockNo As String, _
                                            ByVal koseiMatrix As BuhinKoseiMatrix) Implements BuhinEditBaseDao.UpdateBySekkeiBlockInstl
            Dim sb As New StringBuilder

            Dim col As Integer = 0
            For columnindex As Integer = 0 To koseiMatrix.GetInputInsuColumnIndexes.Count - 1
                If koseiMatrix.InstlColumn(columnindex).Count = 0 Then
                    Continue For
                End If
                For rowindex As Integer = 0 To koseiMatrix.InstlColumn(columnindex).Count - 1

                    If koseiMatrix.InstlColumn(columnindex).Record(rowindex).Level = 0 Then
                        With sb
                            .Remove(0, .Length)
                            .AppendLine(" UPDATE " & MBOM_DB_NAME & ".dbo.T_SHISAKU_SEKKEI_BLOCK_INSTL ")
                            .AppendLine(" SET INSTL_HINBAN_HYOUJI_JUN = '" & col & "' ")
                            .AppendLine(" FROM " & MBOM_DB_NAME & ".dbo.T_SHISAKU_BUHIN_EDIT ")
                            .AppendLine(" WHERE ")
                            .AppendLine(" " & MBOM_DB_NAME & ".dbo.T_SHISAKU_BUHIN_EDIT.SHISAKU_EVENT_CODE = " & MBOM_DB_NAME & ".dbo.T_SHISAKU_SEKKEI_BLOCK_INSTL.SHISAKU_EVENT_CODE ")
                            .AppendLine(" AND " & MBOM_DB_NAME & ".dbo.T_SHISAKU_BUHIN_EDIT.SHISAKU_BUKA_CODE = " & MBOM_DB_NAME & ".dbo.T_SHISAKU_SEKKEI_BLOCK_INSTL.SHISAKU_BUKA_CODE ")
                            .AppendLine(" AND " & MBOM_DB_NAME & ".dbo.T_SHISAKU_BUHIN_EDIT.SHISAKU_BLOCK_NO = " & MBOM_DB_NAME & ".dbo.T_SHISAKU_SEKKEI_BLOCK_INSTL.SHISAKU_BLOCK_NO ")
                            '.AppendLine(" AND " & MBOM_DB_NAME & ".dbo.T_SHISAKU_BUHIN_EDIT.SHISAKU_BLOCK_NO_KAITEI_NO = " & MBOM_DB_NAME & ".dbo.T_SHISAKU_SEKKEI_BLOCK_INSTL.SHISAKU_BLOCK_NO_KAITEI_NO ")
                            .AppendLine(" AND " & MBOM_DB_NAME & ".dbo.T_SHISAKU_BUHIN_EDIT.BUHIN_NO = " & MBOM_DB_NAME & ".dbo.T_SHISAKU_SEKKEI_BLOCK_INSTL.INSTL_HINBAN ")
                            .AppendLine(" AND " & MBOM_DB_NAME & ".dbo.T_SHISAKU_BUHIN_EDIT.LEVEL = 0 ")
                            .AppendLine(" AND " & MBOM_DB_NAME & ".dbo.T_SHISAKU_BUHIN_EDIT.SHISAKU_EVENT_CODE = @ShisakuEventCode ")
                            .AppendLine(" AND " & MBOM_DB_NAME & ".dbo.T_SHISAKU_BUHIN_EDIT.SHISAKU_BUKA_CODE = @ShisakuBukaCode ")
                            .AppendLine(" AND " & MBOM_DB_NAME & ".dbo.T_SHISAKU_BUHIN_EDIT.SHISAKU_BLOCK_NO = @ShisakuBlockNo ")
                            .AppendLine(" AND " & MBOM_DB_NAME & ".dbo.T_SHISAKU_BUHIN_EDIT.BUHIN_NO = @BuhinNo ")
                            .AppendLine(" AND " & MBOM_DB_NAME & ".dbo.T_SHISAKU_BUHIN_EDIT.BUHIN_NO_KBN = " & MBOM_DB_NAME & ".dbo.T_SHISAKU_SEKKEI_BLOCK_INSTL.INSTL_HINBAN_KBN ")
                            .AppendLine(" AND " & MBOM_DB_NAME & ".dbo.T_SHISAKU_BUHIN_EDIT.BUHIN_NO_KBN = @BuhinNoKbn ")
                        End With

                        Dim param As New TShisakuBuhinEditVo
                        param.ShisakuEventCode = shisakuEventCode
                        param.ShisakuBukaCode = shisakuBukaCode
                        param.ShisakuBlockNo = shisakuBlockNo
                        param.BuhinNo = koseiMatrix.InstlColumn(columnindex).Record(rowindex).BuhinNo
                        param.BuhinNoKbn = koseiMatrix.InstlColumn(columnindex).Record(rowindex).BuhinNoKbn

                        Dim db As New EBomDbClient
                        db.Update(sb.ToString, param)
                        col = col + 1
                        Exit For
                    End If
                Next
            Next

        End Sub

        ''' <summary>
        ''' 購担/取引先を取得する
        ''' </summary>
        ''' <param name="buhinNo">部品No</param>
        ''' <returns>購担と取引先</returns>
        ''' <remarks></remarks>
        Public Function FindByKoutanTorihikisaki(ByVal buhinNo As String) As TShisakuBuhinEditVo Implements BuhinEditBaseDao.FindByKoutanTorihikisaki

            Dim db As New EBomDbClient

            Dim sql As String = _
            " SELECT KA, " _
            & " TRCD " _
            & " FROM " & MBOM_DB_NAME & ".dbo.AS_KPSM10P WITH (NOLOCK, NOWAIT)" _
            & " WHERE  " _
            & " BUBA_15 = @Buba15 " _
            & " ORDER BY UPDATED_DATE DESC, UPDATED_TIME DESC "

            Dim NewBuhinNo As String = ""
            If StringUtil.Equals(Left(buhinNo, 1), "-") Then
                NewBuhinNo = Replace(buhinNo, "-", " ")
            Else
                NewBuhinNo = buhinNo
            End If

            Dim paramK As New AsKPSM10PVo
            Dim ETVO As New TShisakuBuhinEditVo

            paramK.Buba15 = NewBuhinNo

            Dim aKPSM As New AsKPSM10PVo

            aKPSM = db.QueryForObject(Of AsKPSM10PVo)(sql, paramK)
            '存在チェックその１(３ヶ月インフォメーション)'
            If aKPSM Is Nothing Then
                NewBuhinNo = ""
                '無ければパーツプライリスト'
                Dim sql2 As String = _
                " SELECT KA, " _
                & " TRCD " _
                & " FROM " & MBOM_DB_NAME & ".dbo.AS_PARTSP WITH (NOLOCK, NOWAIT)" _
                & " WHERE BUBA_13 = @Buba13 " _
                & " ORDER BY UPDATED_DATE DESC, UPDATED_TIME DESC "

                Dim paramP As New AsPARTSPVo

                If buhinNo.Length < 13 Then
                    If StringUtil.Equals(Left(buhinNo, 1), "-") Then
                        NewBuhinNo = Replace(buhinNo, "-", " ")
                    Else
                        NewBuhinNo = buhinNo
                    End If
                    paramP.Buba13 = NewBuhinNo
                Else
                    If StringUtil.Equals(Left(buhinNo, 1), "-") Then
                        NewBuhinNo = Left(Replace(buhinNo, "-", " "), 13)
                    Else
                        NewBuhinNo = Left(buhinNo, 13)
                    End If
                    paramP.Buba13 = NewBuhinNo
                End If

                Dim aPARTS As New AsPARTSPVo
                aPARTS = db.QueryForObject(Of AsPARTSPVo)(sql2, paramP)

                '存在チェックその２(パーツプライリスト)'
                If aPARTS Is Nothing Then
                    NewBuhinNo = ""
                    '無ければ海外生産マスタ'
                    Dim sql3 As String = _
                    " SELECT KA, " _
                    & " TRCD " _
                    & " FROM " & MBOM_DB_NAME & ".dbo.AS_GKPSM10P WITH (NOLOCK, NOWAIT)" _
                    & " WHERE BUBA_15 = @Buba15 " _
                    & " ORDER BY UPDATED_DATE DESC, UPDATED_TIME DESC "

                    Dim paramG As New AsGKPSM10PVo
                    If StringUtil.Equals(Left(buhinNo, 1), "-") Then
                        NewBuhinNo = Replace(buhinNo, "-", " ")
                    Else
                        NewBuhinNo = buhinNo
                    End If
                    paramG.Buba15 = NewBuhinNo
                    Dim aGKPSM As New AsGKPSM10PVo
                    aGKPSM = db.QueryForObject(Of AsGKPSM10PVo)(sql3, paramG)

                    '存在チェックその３(海外生産マスタ) '
                    If aGKPSM Is Nothing Then
                        NewBuhinNo = ""
                        '無ければ部品マスタ'
                        If buhinNo.Length < 10 Then
                            If StringUtil.Equals(Left(buhinNo, 1), "-") Then
                                NewBuhinNo = Replace(buhinNo, "-", " ")
                            Else
                                NewBuhinNo = buhinNo
                            End If
                        Else
                            If StringUtil.Equals(Left(buhinNo, 1), "-") Then
                                NewBuhinNo = Left(Replace(buhinNo, "-", " "), 10)
                            Else
                                NewBuhinNo = Left(buhinNo, 10)
                            End If
                        End If
                        Dim sql4 As String = _
                        " SELECT KOTAN, " _
                        & " MAKER " _
                        & " FROM " & MBOM_DB_NAME & ".dbo.AS_BUHIN01 WITH (NOLOCK, NOWAIT)" _
                        & " WHERE " _
                        & " GZZCP = @Gzzcp " _
                        & " ORDER BY UPDATED_DATE DESC, UPDATED_TIME DESC "

                        Dim param4 As New AsBUHIN01Vo
                        param4.Gzzcp = NewBuhinNo

                        Dim aBuhin01 As New AsBUHIN01Vo
                        aBuhin01 = db.QueryForObject(Of AsBUHIN01Vo)(sql4, param4)

                        '存在チェックその４(部品マスタ)'
                        If aBuhin01 Is Nothing Then
                            '無ければ属性管理'
                            Dim sql5 As String = _
                            "SELECT " _
                            & " FHI_NOMINATE_KOTAN, " _
                            & " TORIHIKISAKI_CODE " _
                            & " FROM " _
                            & " " + EBOM_DB_NAME + ".dbo.T_VALUE_DEV WITH (NOLOCK, NOWAIT)" _
                            & " WHERE " _
                            & "  AN_LEVEL = '1' " _
                            & " AND BUHIN_NO = @BuhinNo " _
                            & " ORDER BY UPDATED_DATE DESC, UPDATED_TIME DESC "

                            Dim aDev As New TValueDevVo
                            Dim paramT As New TValueDevVo
                            'paramT.KaihatsuFugo = KaihatsuFugo
                            If buhinNo.Length < 10 Then
                                paramT.BuhinNo = buhinNo
                            Else
                                paramT.BuhinNo = Left(buhinNo, 10)
                            End If

                            aDev = db.QueryForObject(Of TValueDevVo)(sql5, paramT)

                            '存在チェックその５(属性管理(開発符号毎)) '
                            If aDev Is Nothing Then
                                ETVO.MakerCode = ""
                            Else
                                ETVO.MakerCode = aDev.TorihikisakiCode
                            End If

                        Else
                            ETVO.MakerCode = aBuhin01.Maker
                        End If
                    Else
                        ETVO.MakerCode = aGKPSM.Trcd
                    End If
                Else
                    ETVO.MakerCode = aPARTS.Trcd
                End If
            Else
                ETVO.MakerCode = aKPSM.Trcd
            End If

            If Not StringUtil.IsEmpty(ETVO.MakerCode) Then
                Dim Msql As String = _
                " SELECT MAKER_NAME " _
                & " FROM " & RHACLIBF_DB_NAME & ".dbo.RHAC0610 WITH (NOLOCK, NOWAIT)" _
                & " WHERE " _
                & " MAKER_CODE = @MakerCode "

                Dim Mparam As New Rhac0610Vo
                Mparam.MakerCode = ETVO.MakerCode
                Dim MVo As New Rhac0610Vo
                MVo = db.QueryForObject(Of Rhac0610Vo)(Msql, Mparam)
                If MVo Is Nothing Then
                    ETVO.MakerName = ""
                Else
                    ETVO.MakerName = MVo.MakerName

                End If
            End If

            Return ETVO


            '抜けるまでの間に何も無ければNOTHING'
            'Return Nothing
        End Function

        ''' <summary>
        ''' 部品編集情報を取得する
        ''' </summary>
        ''' <param name="shisakuEventCode">イベントコード</param>
        ''' <param name="shisakuBukaCode">試作部課コード</param>
        ''' <param name="shisakuBlockNo">試作ブロックNo</param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Function FindByBuhinEdit(ByVal shisakuEventCode As String, _
                                        ByVal shisakuBukaCode As String, _
                                        ByVal shisakuBlockNo As String) As List(Of TShisakuBuhinEditVo) Implements BuhinEditBaseDao.FindByBuhinEdit
            Dim sql As String = _
            " SELECT DISTINCT BE.* " _
            & " FROM " & MBOM_DB_NAME & ".dbo.T_SHISAKU_EVENT_BASE B WITH (NOLOCK, NOWAIT) " _
            & " INNER JOIN " & MBOM_DB_NAME & ".dbo.T_SHISAKU_SEKKEI_BLOCK_INSTL SBI " _
            & " ON SBI.SHISAKU_EVENT_CODE = B.SHISAKU_BASE_EVENT_CODE " _
            & " AND SBI.SHISAKU_GOUSYA = B.SHISAKU_BASE_GOUSYA " _
            & " AND SBI.SHISAKU_BUKA_CODE = @ShisakuBukaCode " _
            & " AND SBI.SHISAKU_BLOCK_NO = @ShisakuBlockNo " _
            & " AND SBI.SHISAKU_BLOCK_NO_KAITEI_NO  = ( " _
            & "  SELECT MAX ( CONVERT(INT,COALESCE ( SHISAKU_BLOCK_NO_KAITEI_NO,'' ) ) ) AS SHISAKU_BLOCK_NO_KAITEI_NO  " _
            & "  FROM " & MBOM_DB_NAME & ".dbo.T_SHISAKU_SEKKEI_BLOCK_INSTL " _
            & "  WHERE SHISAKU_EVENT_CODE = SBI.SHISAKU_EVENT_CODE " _
            & "  AND SHISAKU_BUKA_CODE = SBI.SHISAKU_BUKA_CODE " _
            & "  AND SHISAKU_BLOCK_NO = SBI.SHISAKU_BLOCK_NO ) " _
            & " AND NOT SHISAKU_BLOCK_NO_KAITEI_NO = '  0' " _
            & " INNER JOIN " & MBOM_DB_NAME & ".dbo.T_SHISAKU_SEKKEI_BLOCK SB " _
            & " ON SB.SHISAKU_EVENT_CODE = SBI.SHISAKU_EVENT_CODE " _
            & " AND SB.SHISAKU_BUKA_CODE = SBI.SHISAKU_BUKA_CODE " _
            & " AND SB.SHISAKU_BLOCK_NO = SBI.SHISAKU_BLOCK_NO " _
            & " AND ( SB.BLOCK_FUYOU = '0' OR SB.BLOCK_FUYOU = '') " _
            & " AND SB.SHISAKU_BLOCK_NO_KAITEI_NO = SBI.SHISAKU_BLOCK_NO_KAITEI_NO " _
            & " INNER JOIN " & MBOM_DB_NAME & ".dbo.T_SHISAKU_BUHIN_EDIT_INSTL BEI " _
            & " ON BEI.SHISAKU_EVENT_CODE = SB.SHISAKU_EVENT_CODE " _
            & " AND BEI.SHISAKU_BUKA_CODE = SB.SHISAKU_BUKA_CODE " _
            & " AND BEI.SHISAKU_BLOCK_NO = SB.SHISAKU_BLOCK_NO " _
            & " AND BEI.SHISAKU_BLOCK_NO_KAITEI_NO = SB.SHISAKU_BLOCK_NO_KAITEI_NO " _
            & " AND BEI.INSTL_HINBAN_HYOUJI_JUN = SBI.INSTL_HINBAN_HYOUJI_JUN " _
            & " INNER JOIN " & MBOM_DB_NAME & ".dbo.T_SHISAKU_BUHIN_EDIT BE " _
            & " ON BE.SHISAKU_EVENT_CODE = BEI.SHISAKU_EVENT_CODE " _
            & " AND BE.SHISAKU_BUKA_CODE = BEI.SHISAKU_BUKA_CODE " _
            & " AND BE.SHISAKU_BLOCK_NO = BEI.SHISAKU_BLOCK_NO " _
            & " AND BE.SHISAKU_BLOCK_NO_KAITEI_NO = BEI.SHISAKU_BLOCK_NO_KAITEI_NO " _
            & " AND BE.BUHIN_NO_HYOUJI_JUN = BEI.BUHIN_NO_HYOUJI_JUN " _
            & " WHERE  " _
            & " B.SHISAKU_EVENT_CODE = @ShisakuEventCode "

            Dim param As New TShisakuBuhinEditVo
            param.ShisakuEventCode = shisakuEventCode
            param.ShisakuBukaCode = shisakuBukaCode
            param.ShisakuBlockNo = shisakuBlockNo

            Dim db As New EBomDbClient
            Return db.QueryForList(Of TShisakuBuhinEditVo)(sql, param)
        End Function

        ''' <summary>
        ''' 部品編集情報を取得する
        ''' </summary>
        ''' <param name="shisakuEventCode">イベントコード</param>
        ''' <param name="shisakuBlockNo">ブロックNo</param>
        ''' <param name="instlHinban">INSTL品番</param>
        ''' <param name="instlHinbanKbn">INSTL品番区分</param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Function FindByBuhinEdit2(ByVal shisakuEventCode As String, _
                                         ByVal shisakuBlockNo As String, _
                                         ByVal instlHinban As String, _
                                         ByVal instlHinbanKbn As String, _
                                         ByVal InstlDataKbn As String) As List(Of TShisakuBuhinEditVo) Implements BuhinEditBaseDao.FindByBuhinEdit2
            'ByVal shisakuBukaCode As String, _
            ' <param name="shisakuBukaCode">部課コード</param>
            '.AppendLine(" AND SBI.SHISAKU_BUKA_CODE = @ShisakuBukaCode ")
            'param.ShisakuBukaCode = shisakuBukaCode
            Dim sb As New StringBuilder
            With sb
                .Remove(0, .Length)
                .AppendLine(" SELECT DISTINCT BE.* ")
                '.AppendLine(" SELECT DISTINCT BE.* ")
                .AppendLine(" FROM " & MBOM_DB_NAME & ".dbo.T_SHISAKU_SEKKEI_BLOCK_INSTL SBI WITH (NOLOCK, NOWAIT) ")
                .AppendLine(" INNER JOIN " & MBOM_DB_NAME & ".dbo.T_SHISAKU_SEKKEI_BLOCK SB ")
                .AppendLine(" ON SB.SHISAKU_EVENT_CODE = SBI.SHISAKU_EVENT_CODE ")
                .AppendLine(" AND SB.SHISAKU_BUKA_CODE = SBI.SHISAKU_BUKA_CODE ")
                .AppendLine(" AND SB.SHISAKU_BLOCK_NO = SBI.SHISAKU_BLOCK_NO ")
                .AppendLine(" AND ( SB.BLOCK_FUYOU = '0' OR SB.BLOCK_FUYOU = '') ")
                .AppendLine(" AND SB.SHISAKU_BLOCK_NO_KAITEI_NO = SBI.SHISAKU_BLOCK_NO_KAITEI_NO ")
                .AppendLine(" INNER JOIN " & MBOM_DB_NAME & ".dbo.T_SHISAKU_BUHIN_EDIT_INSTL BEI ")
                .AppendLine(" ON BEI.SHISAKU_EVENT_CODE = SB.SHISAKU_EVENT_CODE ")
                .AppendLine(" AND BEI.SHISAKU_BUKA_CODE = SB.SHISAKU_BUKA_CODE ")
                .AppendLine(" AND BEI.SHISAKU_BLOCK_NO = SB.SHISAKU_BLOCK_NO ")
                .AppendLine(" AND BEI.SHISAKU_BLOCK_NO_KAITEI_NO = SB.SHISAKU_BLOCK_NO_KAITEI_NO ")
                .AppendLine(" AND BEI.INSTL_HINBAN_HYOUJI_JUN = SBI.INSTL_HINBAN_HYOUJI_JUN ")
                .AppendLine(" INNER JOIN " & MBOM_DB_NAME & ".dbo.T_SHISAKU_BUHIN_EDIT BE ")
                .AppendLine(" ON BEI.SHISAKU_EVENT_CODE = BE.SHISAKU_EVENT_CODE ")
                .AppendLine(" AND BEI.SHISAKU_BUKA_CODE = BE.SHISAKU_BUKA_CODE ")
                .AppendLine(" AND BEI.SHISAKU_BLOCK_NO = BE.SHISAKU_BLOCK_NO ")
                .AppendLine(" AND BEI.SHISAKU_BLOCK_NO_KAITEI_NO = BE.SHISAKU_BLOCK_NO_KAITEI_NO ")
                .AppendLine(" AND BEI.BUHIN_NO_HYOUJI_JUN = BE.BUHIN_NO_HYOUJI_JUN ")
                .AppendLine(" WHERE  ")
                .AppendLine("  SBI.SHISAKU_EVENT_CODE = @ShisakuEventCode ")
                .AppendLine(" AND SBI.SHISAKU_BLOCK_NO = @ShisakuBlockNo ")
                .AppendLine(" AND SBI.SHISAKU_BLOCK_NO_KAITEI_NO  = ( ")
                .AppendLine("  SELECT MAX ( CONVERT(INT,COALESCE ( SHISAKU_BLOCK_NO_KAITEI_NO,'' ) ) ) AS SHISAKU_BLOCK_NO_KAITEI_NO  ")
                .AppendLine("  FROM " & MBOM_DB_NAME & ".dbo.T_SHISAKU_SEKKEI_BLOCK_INSTL ")
                .AppendLine("  WHERE SHISAKU_EVENT_CODE = SBI.SHISAKU_EVENT_CODE ")
                .AppendLine("  AND SHISAKU_BUKA_CODE = SBI.SHISAKU_BUKA_CODE ")
                .AppendLine("  AND SHISAKU_BLOCK_NO = SBI.SHISAKU_BLOCK_NO ) ")
                .AppendLine(" AND NOT SBI.SHISAKU_BLOCK_NO_KAITEI_NO = '  0' ")
                .AppendLine(" AND NOT SBI.INSU_SURYO IS NULL ")
                .AppendLine(" AND SBI.INSTL_HINBAN = @InstlHinban ")
                .AppendLine(" AND SBI.INSTL_HINBAN_KBN = @InstlHinbanKbn ")
                .AppendLine(" AND SBI.INSTL_DATA_KBN = @InstlDataKbn ")

            End With

            Dim param As New TShisakuSekkeiBlockInstlVo
            param.ShisakuEventCode = shisakuEventCode
            param.ShisakuBlockNo = shisakuBlockNo
            param.InstlHinban = instlHinban
            param.InstlHinbanKbn = instlHinbanKbn
            param.InstlDataKbn = InstlDataKbn

            Dim db As New EBomDbClient
            Return db.QueryForList(Of TShisakuBuhinEditVo)(sb.ToString, param)
        End Function

        ''' <summary>
        ''' 部品編集INSTL情報を取得する
        ''' </summary>
        ''' <param name="shisakuEventCode">イベントコード</param>
        ''' <param name="shisakuBlockNo">ブロックNo</param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Function FindByBuhinEditInstl2(ByVal shisakuEventCode As String, _
                                      ByVal shisakuBlockNo As String, _
                                      ByVal instlHinban As String, _
                                      ByVal instlHinbanKbn As String, _
                                      ByVal InstlDataKbn As String) As List(Of TShisakuBuhinEditInstlVo) Implements BuhinEditBaseDao.FindByBuhinEditInstl2
            ' <param name="shisakuBukaCode">部課コード</param>
            'ByVal shisakuBukaCode As String, _
            '.AppendLine(" AND SBI.SHISAKU_BUKA_CODE = @ShisakuBukaCode ")
            'param.ShisakuBukaCode = shisakuBukaCode
            Dim sb As New StringBuilder
            With sb
                .Remove(0, .Length)
                .AppendLine(" SELECT DISTINCT BEI.* ")
                .AppendLine(" FROM " & MBOM_DB_NAME & ".dbo.T_SHISAKU_SEKKEI_BLOCK_INSTL SBI WITH (NOLOCK, NOWAIT) ")
                .AppendLine(" INNER JOIN " & MBOM_DB_NAME & ".dbo.T_SHISAKU_SEKKEI_BLOCK SB ")
                .AppendLine(" ON SB.SHISAKU_EVENT_CODE = SBI.SHISAKU_EVENT_CODE ")
                .AppendLine(" AND SB.SHISAKU_BUKA_CODE = SBI.SHISAKU_BUKA_CODE ")
                .AppendLine(" AND SB.SHISAKU_BLOCK_NO = SBI.SHISAKU_BLOCK_NO ")
                .AppendLine(" AND ( SB.BLOCK_FUYOU = '0' OR SB.BLOCK_FUYOU = '') ")
                .AppendLine(" AND SB.SHISAKU_BLOCK_NO_KAITEI_NO = SBI.SHISAKU_BLOCK_NO_KAITEI_NO ")
                .AppendLine(" INNER JOIN " & MBOM_DB_NAME & ".dbo.T_SHISAKU_BUHIN_EDIT_INSTL BEI ")
                .AppendLine(" ON BEI.SHISAKU_EVENT_CODE = SB.SHISAKU_EVENT_CODE ")
                .AppendLine(" AND BEI.SHISAKU_BUKA_CODE = SB.SHISAKU_BUKA_CODE ")
                .AppendLine(" AND BEI.SHISAKU_BLOCK_NO = SB.SHISAKU_BLOCK_NO ")
                .AppendLine(" AND BEI.SHISAKU_BLOCK_NO_KAITEI_NO = SB.SHISAKU_BLOCK_NO_KAITEI_NO ")
                .AppendLine(" AND BEI.INSTL_HINBAN_HYOUJI_JUN = SBI.INSTL_HINBAN_HYOUJI_JUN ")
                .AppendLine(" INNER JOIN " & MBOM_DB_NAME & ".dbo.T_SHISAKU_BUHIN_EDIT BE ")
                .AppendLine(" ON BEI.SHISAKU_EVENT_CODE = BE.SHISAKU_EVENT_CODE ")
                .AppendLine(" AND BEI.SHISAKU_BUKA_CODE = BE.SHISAKU_BUKA_CODE ")
                .AppendLine(" AND BEI.SHISAKU_BLOCK_NO = BE.SHISAKU_BLOCK_NO ")
                .AppendLine(" AND BEI.SHISAKU_BLOCK_NO_KAITEI_NO = BE.SHISAKU_BLOCK_NO_KAITEI_NO ")
                .AppendLine(" AND BEI.BUHIN_NO_HYOUJI_JUN = BE.BUHIN_NO_HYOUJI_JUN ")
                .AppendLine(" WHERE  ")
                .AppendLine("  SBI.SHISAKU_EVENT_CODE = @ShisakuEventCode ")
                .AppendLine(" AND SBI.SHISAKU_BLOCK_NO = @ShisakuBlockNo ")
                .AppendLine(" AND SBI.SHISAKU_BLOCK_NO_KAITEI_NO  = ( ")
                .AppendLine("  SELECT MAX ( CONVERT(INT,COALESCE ( SHISAKU_BLOCK_NO_KAITEI_NO,'' ) ) ) AS SHISAKU_BLOCK_NO_KAITEI_NO  ")
                .AppendLine("  FROM " & MBOM_DB_NAME & ".dbo.T_SHISAKU_SEKKEI_BLOCK_INSTL ")
                .AppendLine("  WHERE SHISAKU_EVENT_CODE = SBI.SHISAKU_EVENT_CODE ")
                .AppendLine("  AND SHISAKU_BUKA_CODE = SBI.SHISAKU_BUKA_CODE ")
                .AppendLine("  AND SHISAKU_BLOCK_NO = SBI.SHISAKU_BLOCK_NO ) ")
                .AppendLine(" AND NOT SBI.SHISAKU_BLOCK_NO_KAITEI_NO = '  0' ")
                .AppendLine(" AND NOT SBI.INSU_SURYO IS NULL ")
                .AppendLine(" AND SBI.INSTL_HINBAN = @InstlHinban ")
                .AppendLine(" AND SBI.INSTL_HINBAN_KBN = @InstlHinbanKbn ")
                .AppendLine(" AND SBI.INSTL_DATA_KBN = @InstlDataKbn ")

            End With

            Dim param As New TShisakuSekkeiBlockInstlVo
            param.ShisakuEventCode = shisakuEventCode
            param.ShisakuBlockNo = shisakuBlockNo
            param.InstlHinban = instlHinban
            param.InstlHinbanKbn = instlHinbanKbn
            param.InstlDataKbn = InstlDataKbn

            Dim db As New EBomDbClient
            Return db.QueryForList(Of TShisakuBuhinEditInstlVo)(sb.ToString, param)
        End Function


        ''' <summary>
        ''' 部品編集情報を取得する
        ''' </summary>
        ''' <param name="shisakuEventCode">イベントコード</param>
        ''' <param name="shisakuBlockNo">試作ブロックNo</param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Function FindByBuhinEditKai(ByVal shisakuEventCode As String, _
                                        ByVal shisakuBlockNo As String) As List(Of TShisakuBuhinEditVo) Implements BuhinEditBaseDao.FindByBuhinEditKai
            '" SELECT DISTINCT SBI.INSTL_HINBAN AS BUHIN_NO ,SBI.INSTL_HINBAN_KBN AS BUHIN_NO_KBN,B.SHISAKU_GOUSYA AS BIKOU, " _
            ' <param name="shisakuBukaCode">試作部課コード</param>
            'ByVal shisakuBukaCode As String, _
            '& " AND SBI.SHISAKU_BUKA_CODE = @ShisakuBukaCode " _
            'param.ShisakuBukaCode = shisakuBukaCode
            Dim sql As String = _
            " SELECT DISTINCT SBI.INSTL_HINBAN AS BUHIN_NO ,SBI.INSTL_HINBAN_KBN AS BUHIN_NO_KBN, " _
            & " BE.SHISAKU_EVENT_CODE,BE.SHISAKU_BUKA_CODE,BE.SHISAKU_BLOCK_NO,BE.SHISAKU_BLOCK_NO_KAITEI_NO,BE.BUHIN_NO_HYOUJI_JUN,BE.LEVEL,BE.SHUKEI_CODE,BE.SIA_SHUKEI_CODE,BE.GENCYO_CKD_KBN,BE.KYOUKU_SECTION,BE.MAKER_CODE,BE.MAKER_NAME,BE.BUHIN_NO_KAITEI_NO,BE.EDA_BAN,BE.BUHIN_NAME,BE.SAISHIYOUFUKA,BE.SHUTUZU_YOTEI_DATE,BE.ZAISHITU_KIKAKU_1,BE.ZAISHITU_KIKAKU_2,BE.ZAISHITU_KIKAKU_3,BE.ZAISHITU_MEKKI,BE.SHISAKU_BANKO_SURYO,BE.SHISAKU_BANKO_SURYO_U,BE.SHISAKU_BUHIN_HI,BE.SHISAKU_KATA_HI,BE.BUHIN_NOTE,BE.EDIT_TOUROKUBI,BE.EDIT_TOUROKUJIKAN,BE.KAITEI_HANDAN_FLG,BE.SHISAKU_LIST_CODE,BE.CREATED_USER_ID,BE.CREATED_DATE,BE.CREATED_TIME,BE.UPDATED_USER_ID,BE.UPDATED_DATE,BE.UPDATED_TIME " _
            & " FROM " & MBOM_DB_NAME & ".dbo.T_SHISAKU_EVENT_BASE B WITH (NOLOCK, NOWAIT) " _
            & " INNER JOIN " & MBOM_DB_NAME & ".dbo.T_SHISAKU_SEKKEI_BLOCK_INSTL SBI " _
            & " ON SBI.SHISAKU_EVENT_CODE = B.SHISAKU_BASE_EVENT_CODE " _
            & " AND SBI.SHISAKU_GOUSYA = B.SHISAKU_BASE_GOUSYA " _
            & " AND SBI.SHISAKU_BLOCK_NO = @ShisakuBlockNo " _
            & " AND SBI.SHISAKU_BLOCK_NO_KAITEI_NO  = ( " _
            & "  SELECT MAX ( CONVERT(INT,COALESCE ( SHISAKU_BLOCK_NO_KAITEI_NO,'' ) ) ) AS SHISAKU_BLOCK_NO_KAITEI_NO  " _
            & "  FROM " & MBOM_DB_NAME & ".dbo.T_SHISAKU_SEKKEI_BLOCK_INSTL " _
            & "  WHERE SHISAKU_EVENT_CODE = SBI.SHISAKU_EVENT_CODE " _
            & "  AND SHISAKU_BUKA_CODE = SBI.SHISAKU_BUKA_CODE " _
            & "  AND SHISAKU_BLOCK_NO = SBI.SHISAKU_BLOCK_NO ) " _
            & " AND NOT SBI.SHISAKU_BLOCK_NO_KAITEI_NO = '  0' " _
            & " INNER JOIN " & MBOM_DB_NAME & ".dbo.T_SHISAKU_SEKKEI_BLOCK SB " _
            & " ON SB.SHISAKU_EVENT_CODE = SBI.SHISAKU_EVENT_CODE " _
            & " AND SB.SHISAKU_BUKA_CODE = SBI.SHISAKU_BUKA_CODE " _
            & " AND SB.SHISAKU_BLOCK_NO = SBI.SHISAKU_BLOCK_NO " _
            & " AND ( SB.BLOCK_FUYOU = '0' OR SB.BLOCK_FUYOU = '') " _
            & " AND SB.SHISAKU_BLOCK_NO_KAITEI_NO = SBI.SHISAKU_BLOCK_NO_KAITEI_NO " _
            & " INNER JOIN " & MBOM_DB_NAME & ".dbo.T_SHISAKU_BUHIN_EDIT_INSTL BEI " _
            & " ON BEI.SHISAKU_EVENT_CODE = SB.SHISAKU_EVENT_CODE " _
            & " AND BEI.SHISAKU_BUKA_CODE = SB.SHISAKU_BUKA_CODE " _
            & " AND BEI.SHISAKU_BLOCK_NO = SB.SHISAKU_BLOCK_NO " _
            & " AND BEI.SHISAKU_BLOCK_NO_KAITEI_NO = SB.SHISAKU_BLOCK_NO_KAITEI_NO " _
            & " AND BEI.INSTL_HINBAN_HYOUJI_JUN = SBI.INSTL_HINBAN_HYOUJI_JUN " _
            & " INNER JOIN " & MBOM_DB_NAME & ".dbo.T_SHISAKU_BUHIN_EDIT BE " _
            & " ON BE.SHISAKU_EVENT_CODE = BEI.SHISAKU_EVENT_CODE " _
            & " AND BE.SHISAKU_BUKA_CODE = BEI.SHISAKU_BUKA_CODE " _
            & " AND BE.SHISAKU_BLOCK_NO = BEI.SHISAKU_BLOCK_NO " _
            & " AND BE.SHISAKU_BLOCK_NO_KAITEI_NO = BEI.SHISAKU_BLOCK_NO_KAITEI_NO " _
            & " AND BE.BUHIN_NO_HYOUJI_JUN = BEI.BUHIN_NO_HYOUJI_JUN " _
            & " WHERE  " _
            & " B.SHISAKU_EVENT_CODE = @ShisakuEventCode "

            Dim param As New TShisakuBuhinEditVo
            param.ShisakuEventCode = shisakuEventCode
            param.ShisakuBlockNo = shisakuBlockNo

            Dim db As New EBomDbClient
            Return db.QueryForList(Of TShisakuBuhinEditVo)(sql, param)
        End Function

        ''' <summary>
        ''' 部品編集INSTL情報を取得する
        ''' </summary>
        ''' <param name="shisakuEventCode">イベントコード</param>
        ''' <param name="shisakuBlockNo">ブロックNo</param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Function FindByBuhinEditInstl(ByVal shisakuEventCode As String, _
                                             ByVal shisakuBlockNo As String) As List(Of TShisakuBuhinEditInstlVo) Implements BuhinEditBaseDao.FindByBuhinEditInstl
            ' <param name="shisakuBukaCode">部課コード</param>
            'ByVal shisakuBukaCode As String, _
            '& " AND SBI.SHISAKU_BUKA_CODE = @ShisakuBukaCode " _
            'param.ShisakuBukaCode = shisakuBukaCode
            Dim sql As String = _
            " SELECT DISTINCT BEI.* " _
            & " FROM " & MBOM_DB_NAME & ".dbo.T_SHISAKU_EVENT_BASE B WITH (NOLOCK, NOWAIT) " _
            & " INNER JOIN " & MBOM_DB_NAME & ".dbo.T_SHISAKU_SEKKEI_BLOCK_INSTL SBI " _
            & " ON SBI.SHISAKU_EVENT_CODE = B.SHISAKU_BASE_EVENT_CODE " _
            & " AND SBI.SHISAKU_GOUSYA = B.SHISAKU_BASE_GOUSYA " _
            & " AND SBI.SHISAKU_BLOCK_NO = @ShisakuBlockNo " _
            & " AND SBI.SHISAKU_BLOCK_NO_KAITEI_NO  = ( " _
            & "  SELECT MAX ( CONVERT(INT,COALESCE ( SHISAKU_BLOCK_NO_KAITEI_NO,'' ) ) ) AS SHISAKU_BLOCK_NO_KAITEI_NO  " _
            & "  FROM " & MBOM_DB_NAME & ".dbo.T_SHISAKU_SEKKEI_BLOCK_INSTL " _
            & "  WHERE SHISAKU_EVENT_CODE = SBI.SHISAKU_EVENT_CODE " _
            & "  AND SHISAKU_BUKA_CODE = SBI.SHISAKU_BUKA_CODE " _
            & "  AND SHISAKU_BLOCK_NO = SBI.SHISAKU_BLOCK_NO ) " _
            & " AND NOT SHISAKU_BLOCK_NO_KAITEI_NO = '  0' " _
            & " INNER JOIN " & MBOM_DB_NAME & ".dbo.T_SHISAKU_SEKKEI_BLOCK SB " _
            & " ON SB.SHISAKU_EVENT_CODE = SBI.SHISAKU_EVENT_CODE " _
            & " AND SB.SHISAKU_BUKA_CODE = SBI.SHISAKU_BUKA_CODE " _
            & " AND SB.SHISAKU_BLOCK_NO = SBI.SHISAKU_BLOCK_NO " _
            & " AND ( SB.BLOCK_FUYOU = '0' OR SB.BLOCK_FUYOU = '') " _
            & " AND SB.SHISAKU_BLOCK_NO_KAITEI_NO = SBI.SHISAKU_BLOCK_NO_KAITEI_NO " _
            & " INNER JOIN " & MBOM_DB_NAME & ".dbo.T_SHISAKU_BUHIN_EDIT_INSTL BEI " _
            & " ON BEI.SHISAKU_EVENT_CODE = SB.SHISAKU_EVENT_CODE " _
            & " AND BEI.SHISAKU_BUKA_CODE = SB.SHISAKU_BUKA_CODE " _
            & " AND BEI.SHISAKU_BLOCK_NO = SB.SHISAKU_BLOCK_NO " _
            & " AND BEI.SHISAKU_BLOCK_NO_KAITEI_NO = SB.SHISAKU_BLOCK_NO_KAITEI_NO " _
            & " AND BEI.INSTL_HINBAN_HYOUJI_JUN = SBI.INSTL_HINBAN_HYOUJI_JUN " _
            & " WHERE  " _
            & " B.SHISAKU_EVENT_CODE = @ShisakuEventCode "

            Dim param As New TShisakuBuhinEditVo
            param.ShisakuEventCode = shisakuEventCode
            param.ShisakuBlockNo = shisakuBlockNo

            Dim db As New EBomDbClient
            Return db.QueryForList(Of TShisakuBuhinEditInstlVo)(sql, param)
        End Function

        ''' <summary>
        ''' 設計ブロックINSTL情報を取得する
        ''' </summary>
        ''' <param name="shisakuEventCode">イベントコード</param>
        ''' <param name="shisakuBukaCode">部課コード</param>
        ''' <param name="shisakuBlockNo">ブロックNo</param>
        ''' <param name="instlHinban">INSTL品番</param>
        ''' <param name="instlHinbanKbn">INSTL品番区分</param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Function FindByInstlHinban(ByVal shisakuEventCode As String, _
                                          ByVal shisakuBukaCode As String, _
                                          ByVal shisakuBlockNo As String, _
                                          ByVal instlHinban As String, _
                                          ByVal instlHinbanKbn As String) As TShisakuSekkeiBlockInstlVo Implements BuhinEditBaseDao.FindByInstlHinban

            Dim sql As String = _
            " SELECT * " _
            & " FROM T_SHISAKU_SEKKEI_BLOCK_INSTL SBI " _
            & " WHERE " _
            & " SBI.SHISAKU_EVENT_CODE = @ShisakuEventCode " _
            & " AND SBI.SHISAKU_BUKA_CODE = @ShisakuBukaCode " _
            & " AND SBI.SHISAKU_BLOCK_NO = @ShisakuBlock_No " _
            & " AND SBI.INSTL_HINBAN = @InstlHinban " _
            & " AND SBI.INSTL_HINBAN_KBN = @InstlHinbanKbn "

            Dim param As New TShisakuSekkeiBlockInstlVo
            param.ShisakuEventCode = shisakuEventCode
            param.ShisakuBukaCode = shisakuBukaCode
            param.ShisakuBlockNo = shisakuBlockNo
            param.InstlHinban = instlHinban
            param.InstlHinbanKbn = instlHinbanKbn

            Dim db As New EBomDbClient
            Return db.QueryForObject(Of TShisakuSekkeiBlockInstlVo)(sql)

        End Function

        ''' <summary>
        ''' 設計ブロックINSTL情報を取得する
        ''' </summary>
        ''' <param name="shisakuEventCode">イベントコード</param>
        ''' <param name="shisakuBlockNo">ブロックNo</param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Function FindBySekkeiBlockInstl(ByVal shisakuEventCode As String, _
                                               ByVal shisakuBlockNo As String) As List(Of TShisakuSekkeiBlockInstlVo) Implements BuhinEditBaseDao.FindBySekkeiBlockInstl
            '''' <param name="shisakuBukaCode">部課コード</param>
            'ByVal shisakuBukaCode As String, _
            '.AppendLine(" AND SBI.SHISAKU_BUKA_CODE = @ShisakuBukaCode ")
            'param.ShisakuBukaCode = shisakuBukaCode
            Dim sb As New StringBuilder
            With sb
                .Remove(0, .Length)
                .AppendLine(" SELECT DISTINCT SBI.SHISAKU_EVENT_CODE, SBI.SHISAKU_BUKA_CODE, SBI.SHISAKU_BLOCK_NO, SBI.INSTL_HINBAN, ")
                .AppendLine(" SBI.INSTL_DATA_KBN, ")
                .AppendLine(" SBI.INSTL_HINBAN_KBN, ")
                .AppendLine(" SBI.INSTL_HINBAN_HYOUJI_JUN ")
                .AppendLine(" FROM " & MBOM_DB_NAME & ".dbo.T_SHISAKU_EVENT_BASE B WITH (NOLOCK, NOWAIT) ")
                .AppendLine(" INNER JOIN " & MBOM_DB_NAME & ".dbo.T_SHISAKU_SEKKEI_BLOCK_INSTL SBI ")
                .AppendLine(" ON SBI.SHISAKU_EVENT_CODE = B.SHISAKU_BASE_EVENT_CODE ")
                .AppendLine(" AND SBI.SHISAKU_GOUSYA = B.SHISAKU_BASE_GOUSYA ")
                .AppendLine(" AND SBI.SHISAKU_BLOCK_NO = @ShisakuBlockNo ")
                .AppendLine(" AND SBI.SHISAKU_BLOCK_NO_KAITEI_NO  = ( ")
                .AppendLine("  SELECT MAX ( CONVERT(INT,COALESCE ( SHISAKU_BLOCK_NO_KAITEI_NO,'' ) ) ) AS SHISAKU_BLOCK_NO_KAITEI_NO  ")
                .AppendLine("  FROM " & MBOM_DB_NAME & ".dbo.T_SHISAKU_SEKKEI_BLOCK_INSTL ")
                .AppendLine("  WHERE SHISAKU_EVENT_CODE = SBI.SHISAKU_EVENT_CODE ")
                .AppendLine("  AND SHISAKU_BUKA_CODE = SBI.SHISAKU_BUKA_CODE ")
                .AppendLine("  AND SHISAKU_BLOCK_NO = SBI.SHISAKU_BLOCK_NO ) ")
                .AppendLine(" AND NOT SBI.SHISAKU_BLOCK_NO_KAITEI_NO = '  0' ")
                .AppendLine(" AND NOT SBI.INSU_SURYO IS NULL ")
                .AppendLine(" INNER JOIN " & MBOM_DB_NAME & ".dbo.T_SHISAKU_SEKKEI_BLOCK SB ")
                .AppendLine(" ON SB.SHISAKU_EVENT_CODE = SBI.SHISAKU_EVENT_CODE ")
                .AppendLine(" AND SB.SHISAKU_BUKA_CODE = SBI.SHISAKU_BUKA_CODE ")
                .AppendLine(" AND SB.SHISAKU_BLOCK_NO = SBI.SHISAKU_BLOCK_NO ")
                .AppendLine(" AND ( SB.BLOCK_FUYOU = '0' OR SB.BLOCK_FUYOU = '') ")
                .AppendLine(" AND SB.SHISAKU_BLOCK_NO_KAITEI_NO = SBI.SHISAKU_BLOCK_NO_KAITEI_NO ")
                .AppendLine(" WHERE  ")
                .AppendLine(" B.SHISAKU_EVENT_CODE = @ShisakuEventCode ")
                .AppendLine(" ORDER BY SBI.INSTL_HINBAN, SBI.INSTL_HINBAN_KBN, SBI.INSTL_DATA_KBN ")
            End With

            Dim param As New TShisakuSekkeiBlockInstlVo
            param.ShisakuEventCode = shisakuEventCode
            param.ShisakuBlockNo = shisakuBlockNo

            Dim db As New EBomDbClient
            Return db.QueryForList(Of TShisakuSekkeiBlockInstlVo)(sb.ToString, param)
        End Function

        ''' <summary>
        ''' 設計ブロック情報と設計ブロックINSTL情報を削除
        ''' </summary>
        ''' <param name="shisakuEventCode">試作イベントコード</param>
        ''' <param name="shisakuBukaCode">試作部課コード</param>
        ''' <param name="shisakuBlockNo">試作ブロックNo</param>
        ''' <remarks></remarks>
        Public Sub DeleteBySekkeiBlockAndInstl(ByVal shisakuEventCode As String, _
                                               ByVal shisakuBukaCode As String, _
                                               ByVal shisakuBlockNo As String) Implements BuhinEditBaseDao.DeleteBySekkeiBlockAndInstl
            Dim sb As New StringBuilder
            With sb
                .Remove(0, .Length)
                .AppendLine(" DELETE ")
                .AppendLine(" FROM " & MBOM_DB_NAME & ".dbo.T_SHISAKU_SEKKEI_BLOCK ")
                .AppendLine(" WHERE ")
                .AppendLine(" SHISAKU_EVENT_CODE = '" & shisakuEventCode & "' ")
                .AppendLine(" AND SHISAKU_BUKA_CODE = '" & shisakuBukaCode & "' ")
                .AppendLine(" AND SHISAKU_BLOCK_NO = '" & shisakuBlockNo & "' ")
            End With

            Dim db As New EBomDbClient
            db.Delete(sb.ToString)

            With sb
                .Remove(0, .Length)
                .AppendLine(" DELETE ")
                .AppendLine(" FROM " & MBOM_DB_NAME & ".dbo.T_SHISAKU_SEKKEI_BLOCK_INSTL ")
                .AppendLine(" WHERE ")
                .AppendLine(" SHISAKU_EVENT_CODE = '" & shisakuEventCode & "' ")
                .AppendLine(" AND SHISAKU_BUKA_CODE = '" & shisakuBukaCode & "' ")
                .AppendLine(" AND SHISAKU_BLOCK_NO = '" & shisakuBlockNo & "' ")
            End With
            db.Delete(sb.ToString)



        End Sub

#Region "パンダ前"

        ''' <summary>
        ''' 部品構成情報を取得
        ''' </summary>
        ''' <param name="BuhinNoOya">部品番号(親)</param>
        ''' <returns>部品構成情報</returns>
        ''' <remarks></remarks>
        Public Function FindByRhac0551(ByVal BuhinNoOya As String) As List(Of Rhac0551Vo) Implements BuhinEditBaseDao.FindByRhac0551
            Dim sql As String = _
             " SELECT * " _
             & " FROM " _
             & " " & RHACLIBF_DB_NAME & ".dbo.RHAC0551 WITH (NOLOCK, NOWAIT) " _
             & " WHERE " _
             & " BUHIN_NO_OYA = @BuhinNoOya "

            Dim db As New EBomDbClient
            Dim param As New Rhac0551Vo
            param.BuhinNoOya = BuhinNoOya

            Return db.QueryForList(Of Rhac0551Vo)(sql, param)

        End Function

        ''' <summary>
        ''' 部品情報を取得
        ''' </summary>
        ''' <param name="BuhinNo">部品番号</param>
        ''' <returns>部品情報</returns>
        ''' <remarks></remarks>
        Public Function FindByRhac0530(ByVal BuhinNo As String) As Rhac0530Vo Implements BuhinEditBaseDao.FindByRhac0530
            Dim sql As String = _
             " SELECT R.* " _
             & " FROM " _
             & " " & RHACLIBF_DB_NAME & ".dbo.RHAC0530 R WITH (NOLOCK, NOWAIT) " _
             & " WHERE " _
             & " R.BUHIN_NO = @BuhinNo " _
             & " AND R.HAISI_DATE = '99999999' " _
             & " AND R.KAITEI_NO = ( " _
             & " SELECT MAX ( CONVERT ( INT,COALESCE ( KAITEI_NO,'' ) ) ) AS KAITEI_NO " _
             & " FROM " & RHACLIBF_DB_NAME & ".dbo.RHAC0530 WITH (NOLOCK, NOWAIT) " _
             & " WHERE BUHIN_NO = R.BUHIN_NO ) "

            Dim db As New EBomDbClient
            Dim param As New Rhac0530Vo
            param.BuhinNo = BuhinNo

            Return db.QueryForObject(Of Rhac0530Vo)(sql, param)
        End Function

#End Region

#Region "図面"

        ''' <summary>
        ''' 部品構成情報を取得
        ''' </summary>
        ''' <param name="BuhinNoOya">部品番号(親)</param>
        ''' <returns>部品構成情報</returns>
        ''' <remarks></remarks>
        Public Function FindByRhac0552(ByVal BuhinNoOya As String) As List(Of Rhac0552Vo) Implements BuhinEditBaseDao.FindByRhac0552
            Dim sql As String = _
            " SELECT * " _
            & " FROM " _
            & " " & RHACLIBF_DB_NAME & ".dbo.RHAC0552 WITH (NOLOCK, NOWAIT) " _
            & " WHERE " _
            & " BUHIN_NO_OYA = @BuhinNoOya "

            Dim db As New EBomDbClient
            Dim param As New Rhac0552Vo
            param.BuhinNoOya = BuhinNoOya

            Return db.QueryForList(Of Rhac0552Vo)(sql, param)
        End Function

        ''' <summary>
        ''' 部品情報を取得
        ''' </summary>
        ''' <param name="BuhinNo">部品番号</param>
        ''' <returns>部品情報</returns>
        ''' <remarks></remarks>
        Public Function FindByRhac0532(ByVal BuhinNo As String) As Rhac0532Vo Implements BuhinEditBaseDao.FindByRhac0532
            Dim sql As String = _
            " SELECT R.* " _
            & " FROM " _
            & " " & RHACLIBF_DB_NAME & ".dbo.RHAC0532 R WITH (NOLOCK, NOWAIT) " _
            & " WHERE " _
            & " R.BUHIN_NO = @BuhinNo " _
            & " AND R.KAITEI_NO = ( " _
            & " SELECT MAX ( CONVERT ( VARCHAR,COALESCE ( KAITEI_NO,'' ) ) ) AS KAITEI_NO " _
            & " FROM " & RHACLIBF_DB_NAME & ".dbo.RHAC0532 WITH (NOLOCK, NOWAIT) " _
            & " WHERE BUHIN_NO = R.BUHIN_NO ) "

            Dim db As New EBomDbClient
            Dim param As New Rhac0532Vo
            param.BuhinNo = BuhinNo

            Return db.QueryForObject(Of Rhac0532Vo)(sql, param)
        End Function



#End Region

#Region "FM5以降"

        ''' <summary>
        ''' 部品構成情報を取得
        ''' </summary>
        ''' <param name="BuhinNoOya">部品番号(親)</param>
        ''' <param name="kaihatsuFugo">開発符号</param>
        ''' <returns>部品構成情報</returns>
        ''' <remarks></remarks>
        Public Function FindByRhac0553(ByVal BuhinNoOya As String, ByVal kaihatsuFugo As String) As List(Of Rhac0553Vo) Implements BuhinEditBaseDao.FindByRhac0553
            Dim sql As String = _
             " SELECT R.* " _
             & " FROM " _
             & " " & RHACLIBF_DB_NAME & ".dbo.RHAC0553 R WITH (NOLOCK, NOWAIT) " _
             & " WHERE " _
             & " R.BUHIN_NO_OYA = @BuhinNoOya " _
             & " AND R.KAIHATSU_FUGO = @KaihatsuFugo " _
             & " AND R.KAITEI_NO =  " _
             & " SELECT MAX ( CONVERT ( INT,COALESCE ( KAITEI_NO,'' ) ) ) AS KAITEI_NO " _
             & " FROM " & RHACLIBF_DB_NAME & ".dbo.RHAC0553 WITH (NOLOCK, NOWAIT) " _
             & " WHERE BUHIN_NO = R.BUHIN_NO ) "

            Dim db As New EBomDbClient
            Dim param As New Rhac0553Vo
            param.BuhinNoOya = BuhinNoOya
            param.KaihatsuFugo = kaihatsuFugo

            Return db.QueryForList(Of Rhac0553Vo)(sql, param)
        End Function

        ''' <summary>
        ''' 部品情報を取得
        ''' </summary>
        ''' <param name="BuhinNo">部品番号</param>
        ''' <returns>部品情報</returns>
        ''' <remarks></remarks>
        Public Function FindByRhac0533(ByVal BuhinNo As String) As Rhac0533Vo Implements BuhinEditBaseDao.FindByRhac0533
            Dim sql As String = _
             " SELECT R.* " _
             & " FROM " _
             & " " & RHACLIBF_DB_NAME & ".dbo.RHAC0533 R WITH (NOLOCK, NOWAIT) " _
             & " WHERE " _
             & " R.BUHIN_NO = @BuhinNo " _
             & " AND R.HAISI_DATE = '99999999' " _
             & " AND R.KAITEI_NO = ( " _
             & " SELECT MAX ( CONVERT ( INT,COALESCE ( KAITEI_NO,'' ) ) ) AS KAITEI_NO " _
             & " FROM " & RHACLIBF_DB_NAME & ".dbo.RHAC0533 WITH (NOLOCK, NOWAIT) " _
             & " WHERE BUHIN_NO = R.BUHIN_NO ) "

            Dim db As New EBomDbClient
            Dim param As New Rhac0533Vo
            param.BuhinNo = BuhinNo

            Return db.QueryForObject(Of Rhac0533Vo)(sql, param)
        End Function

        ''' <summary>
        ''' 部品情報を取得
        ''' </summary>
        ''' <param name="kaihatsuFugo">開発符号</param>
        ''' <param name="BuhinNo">部品番号</param>
        ''' <returns>部品情報</returns>
        ''' <remarks></remarks>
        Public Function FindByRhac1910(ByVal kaihatsuFugo As String, ByVal BuhinNo As String) As Rhac1910Vo Implements BuhinEditBaseDao.FindByRhac1910
            Dim sql As String = _
             " SELECT R.* " _
             & " FROM " _
             & " " & RHACLIBF_DB_NAME & ".dbo.RHAC1910 R WITH (NOLOCK, NOWAIT) " _
             & " WHERE " _
             & " R.BUHIN_NO = @BuhinNo " _
             & " R.KAIHATSU_FUGO = @KiahatsuFugo " _
             & " AND R.HAISI_DATE = '99999999' " _
             & " AND R.KAITEI_NO = ( " _
             & " SELECT MAX ( CONVERT ( INT,COALESCE ( KAITEI_NO,'' ) ) ) AS KAITEI_NO " _
             & " FROM " & RHACLIBF_DB_NAME & ".dbo.RHAC1910 WITH (NOLOCK, NOWAIT) " _
             & " WHERE BUHIN_NO = R.BUHIN_NO  " _
             & " AND KAIHATSU_FUGO = R.KAIHATSU_FUGO ) "

            Dim db As New EBomDbClient
            Dim param As New Rhac1910Vo
            param.BuhinNo = BuhinNo
            param.KaihatsuFugo = kaihatsuFugo

            Return db.QueryForObject(Of Rhac1910Vo)(sql, param)
        End Function

#End Region

    End Class
End Namespace
