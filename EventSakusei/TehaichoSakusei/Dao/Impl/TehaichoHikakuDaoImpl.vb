Imports ShisakuCommon.Db
Imports ShisakuCommon.Db.EBom
Imports ShisakuCommon.Db.EBom.Vo.Helper
Imports ShisakuCommon.Db.Impl
Imports ShisakuCommon
Imports ShisakuCommon.Db.EBom.Vo
Imports ShisakuCommon.Db.EBom.Dao
Imports ShisakuCommon.Db.EBom.Dao.Impl
Imports EventSakusei.TehaichoSakusei.Dao
Imports EBom.Data
Imports EBom.Common
Imports System.Text

Namespace TehaichoSakusei.Dao
    Public Class TehaichoHikakuDaoImpl : Inherits DaoEachFeatureImpl
        Implements TehaichoHikakuDao

        Private _Eventvo As TShisakuEventVo

        Public WriteOnly Property EventVo() As TShisakuEventVo Implements TehaichoHikakuDao.EventVo
            Set(ByVal value As TShisakuEventVo)
                _Eventvo = value
            End Set
        End Property

#Region "比較織込みに使用するFind"

        ''' <summary>
        ''' 追加品番の存在チェック
        ''' </summary>
        ''' <param name="shisakuEventCode">試作イベントコード</param>
        ''' <param name="shisakuBukaCode">試作部課コード</param>
        ''' <param name="shisakuBlockNo">試作ブロックNo</param>
        ''' <param name="Level">レベル</param>
        ''' <param name="BuhinNo">部品番号</param>
        ''' <param name="BuhinNoKbn">部品番号試作区分</param>
        ''' <returns>試作部品表編集ベース情報</returns>
        Public Function FindByTsuikaHinban(ByVal shisakuEventCode As String, _
                                           ByVal shisakuBukaCode As String, _
                                           ByVal shisakuBlockNo As String, _
                                           ByVal Level As String, _
                                           ByVal BuhinNo As String, _
                                           ByVal BuhinNoKbn As String, _
                                                 Optional ByVal kyokyuSection As String = Nothing, _
                                                 Optional ByVal BaseBuhinFlag As String = Nothing) As List(Of TShisakuBuhinEditBaseVo) Implements TehaichoHikakuDao.FindByTsuikaHinban
            'Dim param As New TShisakuBuhinEditBaseVo
            'param.ShisakuEventCode = shisakuEventCode
            'param.ShisakuBukaCode = shisakuBukaCode
            'param.ShisakuBlockNo = shisakuBlockNo
            'param.BuhinNo = BuhinNo
            'param.Level = Level
            'param.BuhinNoKbn = BuhinNoKbn
            'param.KyoukuSection = kyokyuSection
            'param.BaseBuhinFlg = BaseBuhinFlag

            'Dim sql As String = _
            '" SELECT  * " _
            '& " FROM " _
            '& " " & MBOM_DB_NAME & ".dbo.T_SHISAKU_BUHIN_EDIT_TMP " _
            '& " WHERE " _
            '& " SHISAKU_EVENT_CODE = '" & param.ShisakuEventCode & "'" _
            '& " AND SHISAKU_BUKA_CODE = '" & param.ShisakuBukaCode & "'" _
            '& " AND SHISAKU_BLOCK_NO = '" & param.ShisakuBlockNo & "'" _
            '& " AND SHISAKU_BLOCK_NO_KAITEI_NO = '000' " _
            '& " AND GYOU_ID = '888' " _
            '& " AND BUHIN_NO = '" & param.BuhinNo & "'" _
            '& " AND LEVEL = " & param.Level _
            '& " AND BUHIN_NO_KBN = '" & param.BuhinNoKbn & "'"

            Dim sql As New System.Text.StringBuilder
            With sql
                .AppendLine(" SELECT * ")
                .AppendLine(" FROM " & MBOM_DB_NAME & ".dbo.T_SHISAKU_BUHIN_EDIT_TMP ")
                .AppendLine(" WHERE ")
                .AppendFormat(" SHISAKU_EVENT_CODE = '{0}' ", shisakuEventCode)
                .AppendFormat(" AND SHISAKU_BUKA_CODE = '{0}' ", shisakuBukaCode)
                .AppendFormat(" AND SHISAKU_BLOCK_NO = '{0}' ", shisakuBlockNo)
                .AppendFormat(" AND SHISAKU_BLOCK_NO_KAITEI_NO = '000' ")
                .AppendFormat(" AND GYOU_ID = '888' ")
                .AppendFormat(" AND BUHIN_NO = '{0}' ", BuhinNo)
                .AppendFormat(" AND LEVEL = {0} ", Level)
                .AppendFormat(" AND BUHIN_NO_KBN = '{0}' ", BuhinNoKbn)
                If kyokyuSection IsNot Nothing Then
                    .AppendFormat(" AND KYOUKU_SECTION = '{0}' ", kyokyuSection)
                End If
                If BaseBuhinFlag IsNot Nothing Then
                    .AppendFormat(" AND BASE_BUHIN_FLG = '{0}' ", BaseBuhinFlag)
                End If
            End With



            Dim db As New EBomDbClient
            Return db.QueryForList(Of TShisakuBuhinEditBaseVo)(sql.ToString)
        End Function


        ''' <summary>
        ''' 追加品番の存在チェック
        ''' </summary>
        ''' <param name="param">パラメータ</param>
        ''' <returns>試作部品表編集ベース情報</returns>
        Public Function FindByTsuikaHinban(ByVal param As TShisakuBuhinEditBaseVo) As List(Of TShisakuBuhinEditBaseVo) Implements TehaichoHikakuDao.FindByTsuikaHinban

            'Dim sql As String = _
            '" SELECT  * " _
            '& " FROM " _
            '& " " & MBOM_DB_NAME & ".dbo.T_SHISAKU_BUHIN_EDIT_TMP " _
            '& " WHERE " _
            '& " SHISAKU_EVENT_CODE = '" & param.ShisakuEventCode & "'" _
            '& " AND SHISAKU_BUKA_CODE = '" & param.ShisakuBukaCode & "'" _
            '& " AND SHISAKU_BLOCK_NO = '" & param.ShisakuBlockNo & "'" _
            '& " AND SHISAKU_BLOCK_NO_KAITEI_NO = '000' " _
            '& " AND GYOU_ID = '888' " _
            '& " AND BUHIN_NO = '" & param.BuhinNo & "'" _
            '& " AND LEVEL = " & param.Level _
            '& " AND BUHIN_NO_KBN = '" & param.BuhinNoKbn & "'"

            Dim sql As New System.Text.StringBuilder
            With sql
                .AppendLine(" SELECT * ")
                .AppendLine(" FROM " & MBOM_DB_NAME & ".dbo.T_SHISAKU_BUHIN_EDIT_TMP ")
                .AppendLine(" WHERE ")
                .AppendFormat(" SHISAKU_EVENT_CODE = '{0}' ", param.ShisakuEventCode)
                .AppendFormat(" AND SHISAKU_BUKA_CODE = '{0}' ", param.ShisakuBukaCode)
                .AppendFormat(" AND SHISAKU_BLOCK_NO = '{0}' ", param.ShisakuBlockNo)
                .AppendFormat(" AND SHISAKU_BLOCK_NO_KAITEI_NO = '000' ")
                .AppendFormat(" AND GYOU_ID = '888' ")
                .AppendFormat(" AND BUHIN_NO = '{0}' ", param.BuhinNo)
                .AppendFormat(" AND LEVEL = {0} ", param.Level)
                .AppendFormat(" AND BUHIN_NO_KBN = '{0}' ", param.BuhinNoKbn)

                ''2015/10/01 追加 E.Ubukata Ver.2.11.4
                '' 以下の条件は使用しない
                'If param.ShukeiCode IsNot Nothing Then
                '    .AppendLine(" AND SHUKEI_CODE = @ShukeiCode ")
                'End If
                'If param.SiaShukeiCode IsNot Nothing Then
                '    .AppendLine(" AND SIA_SHUKEI_CODE = @SiaShukeiCode ")
                'End If
                'If param.GencyoCkdKbn IsNot Nothing Then
                '    .AppendLine(" AND GENCYO_CKD_KBN = @GencyoCkdKbn ")
                'End If

                If param.KyoukuSection IsNot Nothing Then
                    .AppendFormat(" AND KYOUKU_SECTION = '{0}' ", param.KyoukuSection)
                End If
                If param.BaseBuhinFlg IsNot Nothing Then
                    .AppendFormat(" AND BASE_BUHIN_FLG = '{0}' ", param.BaseBuhinFlg)
                End If
            End With
            Dim db As New EBomDbClient

            ''2015/10/01 追加 E.Ubukata Ver.2.11.4
            '' 国内集計コードが無い場合は海外集計コードで比較する
            Dim shukeiCode1 As String
            Dim shukeiCode2 As String
            If StringUtil.IsNotEmpty(param.ShukeiCode) Then
                shukeiCode1 = Trim(param.ShukeiCode)
            Else
                shukeiCode1 = Trim(param.SiaShukeiCode)
            End If
            Dim rtn As New List(Of TShisakuBuhinEditBaseVo)
            For Each vo As TShisakuBuhinEditBaseVo In db.QueryForList(Of TShisakuBuhinEditBaseVo)(sql.ToString)
                If StringUtil.IsNotEmpty(vo.ShukeiCode) Then
                    shukeiCode2 = Trim(vo.ShukeiCode)
                Else
                    shukeiCode2 = Trim(vo.SiaShukeiCode)
                End If
                If shukeiCode1 = shukeiCode2 Then
                    rtn.Add(vo)
                End If

            Next
            Return rtn
        End Function



        ''' <summary>
        ''' 部品編集号車情報TMP情報を取得する
        ''' </summary>
        ''' <param name="BuhinEditVo">試作編集情報</param>
        ''' <returns>部品編集号車情報TMP情報</returns>
        ''' <remarks></remarks>
        Public Function FindByGousyaTmp(ByVal BuhinEditVo As TShisakuBuhinEditVo) As List(Of TShisakuBuhinEditGousyaTmpVo) Implements TehaichoHikakuDao.FindByGousyaTmp
            Dim sql As String = _
            " SELECT  " _
            & " BI.SHISAKU_EVENT_CODE, " _
            & " BI.SHISAKU_BUKA_CODE, " _
            & " BI.SHISAKU_BLOCK_NO, " _
            & " BI.SHISAKU_BLOCK_NO_KAITEI_NO, " _
            & " EI.BUHIN_NO_HYOUJI_JUN, " _
            & " BI.SHISAKU_GOUSYA, " _
            & " EI.INSU_SURYO " _
            & " FROM " _
            & " " & MBOM_DB_NAME & ".dbo.T_SHISAKU_SEKKEI_BLOCK_INSTL BI " _
            & " LEFT JOIN " & MBOM_DB_NAME & ".dbo.T_SHISAKU_BUHIN_EDIT_INSTL EI " _
            & " ON BI.SHISAKU_EVENT_CODE = EI.SHISAKU_EVENT_CODE " _
            & " AND BI.SHISAKU_BUKA_CODE = EI.SHISAKU_BUKA_CODE " _
            & " AND BI.SHISAKU_BLOCK_NO = EI.SHISAKU_BLOCK_NO " _
            & " AND BI.SHISAKU_BLOCK_NO_KAITEI_NO = EI.SHISAKU_BLOCK_NO_KAITEI_NO " _
            & " WHERE " _
            & " BI.SHISAKU_EVENT_CODE = @ShisakuEventCode " _
            & " AND BI.SHISAKU_BUKA_CODE = @ShisakuBukaCode " _
            & " AND BI.SHISAKU_BLOCK_NO = @ShisakuBlockNo " _
            & " AND EI.BUHIN_NO_HYOUJI_JUN = @BuhinNoHyoujiJun " _
            & " AND BI.SHISAKU_BLOCK_NO_KAITEI_NO = ( " _
            & " SELECT MAX ( CONVERT ( INT,COALESCE ( SHISAKU_BLOCK_NO_KAITEI_NO,'' ) ) ) AS SHISAKU_BLOCK_NO_KAITEI_NO " _
            & " FROM " & MBOM_DB_NAME & ".dbo.T_SHISAKU_SEKKEI_BLOCK_INSTL " _
            & " WHERE SHISAKU_EVENT_CODE = BI.SHISAKU_EVENT_CODE " _
            & " AND SHISAKU_BUKA_CODE = BI.SHISAKU_BUKA_CODE " _
            & " AND SHISAKU_BLOCK_NO = BI.SHISAKU_BLOCK_NO) "

            Dim db As New EBomDbClient
            Dim param As New TShisakuBuhinEditInstlVo

            param.ShisakuEventCode = BuhinEditVo.ShisakuEventCode
            param.ShisakuBukaCode = BuhinEditVo.ShisakuBukaCode
            param.ShisakuBlockNo = BuhinEditVo.ShisakuBlockNo
            param.BuhinNoHyoujiJun = BuhinEditVo.BuhinNoHyoujiJun

            Return db.QueryForList(Of TShisakuBuhinEditGousyaTmpVo)(sql, param)
        End Function

        ''' <summary>
        ''' 号車表示順の最後の番号を取得する
        ''' </summary>
        ''' <param name="GousyaVo">部品編集号車情報</param>
        ''' <returns>部品編集号車情報TMP情報</returns>
        ''' <remarks></remarks>
        Public Function FindByGousyaHyoujiJun(ByVal GousyaVo As TShisakuBuhinEditGousyaTmpVo) As Integer Implements TehaichoHikakuDao.FindByGousyaHyoujiJun
            Dim sql As String = _
            " SELECT * " _
            & " FROM " _
            & " " & MBOM_DB_NAME & ".dbo.T_SHISAKU_BUHIN_EDIT_GOUSYA_TMP TG" _
            & " WHERE " _
            & " TG.SHISAKU_EVENT_CODE = @ShisakuEventCode " _
            & " AND TG.SHISAKU_BUKA_CODE = @ShisakuBukaCode " _
            & " AND TG.SHISAKU_BLOCK_NO = @ShisakuBlockNo " _
            & " AND TG.BUHIN_NO_HYOUJI_JUN = @BuhinNoHyoujiJun " _
            & " AND TG.SHISAKU_BLOCK_NO_KAITEI_NO = ( " _
            & " SELECT MAX ( CONVERT ( INT,COALESCE ( SHISAKU_BLOCK_NO_KAITEI_NO,'' ) ) ) AS SHISAKU_BLOCK_NO_KAITEI_NO " _
            & " FROM " & MBOM_DB_NAME & ".dbo.T_SHISAKU_BUHIN_EDIT_GOUSYA_TMP " _
            & " WHERE SHISAKU_EVENT_CODE = TG.SHISAKU_EVENT_CODE " _
            & " AND SHISAKU_BUKA_CODE = TG.SHISAKU_BUKA_CODE " _
            & " AND SHISAKU_BLOCK_NO = TG.SHISAKU_BLOCK_NO) "

            Dim db As New EBomDbClient
            Dim param As New TShisakuBuhinEditGousyaTmpVo

            param.ShisakuEventCode = GousyaVo.ShisakuEventCode
            param.ShisakuBukaCode = GousyaVo.ShisakuBukaCode
            param.ShisakuBlockNo = GousyaVo.ShisakuBlockNo
            param.BuhinNoHyoujiJun = GousyaVo.BuhinNoHyoujiJun

            Dim vo As New TShisakuBuhinEditGousyaTmpVo
            vo = db.QueryForObject(Of TShisakuBuhinEditGousyaTmpVo)(sql, param)

            If vo Is Nothing Then
                Return 0
            Else
                Return vo.ShisakuGousyaHyoujiJun
            End If

        End Function

        ''' <summary>
        ''' 追加品番の存在チェック(TMP)
        ''' </summary>
        ''' <param name="BaseVo">部品編集ベース情報</param>
        ''' <returns>追加品番があればTrue</returns>
        ''' <remarks></remarks>
        Public Function FindByTsuikaHinbanTmp(ByVal BaseVo As TShisakuBuhinEditVo) As Boolean Implements TehaichoHikakuDao.FindByTsuikaHinbanTmp
            Dim sql As String = _
            " SELECT  * " _
            & " FROM " _
            & " " & MBOM_DB_NAME & ".dbo.T_SHISAKU_BUHIN_EDIT_TMP " _
            & " WHERE " _
            & " SHISAKU_EVENT_CODE = @ShisakuEventCode " _
            & " AND SHISAKU_BUKA_CODE = @ShisakuBukaCode " _
            & " AND SHISAKU_BLOCK_NO = @ShisakuBlockNo " _
            & " AND LEVEL = @Level " _
            & " AND BUHIN_NO = @BuhinNo " _
            & " AND BUHIN_NO_KBN = @BuhinNoKbn "

            Dim db As New EBomDbClient
            Dim param As New TShisakuBuhinEditTmpVo
            param.ShisakuEventCode = BaseVo.ShisakuEventCode
            param.ShisakuBukaCode = BaseVo.ShisakuBukaCode
            param.ShisakuBlockNo = BaseVo.ShisakuBlockNo

            param.Level = BaseVo.Level
            param.BuhinNo = BaseVo.BuhinNo
            param.BuhinNoKbn = BaseVo.BuhinNoKbn

            Dim vo As New TShisakuBuhinEditTmpVo

            vo = db.QueryForObject(Of TShisakuBuhinEditTmpVo)(sql, param)

            If vo Is Nothing Then
                Return False
            End If

            Return True

        End Function

        ''' <summary>
        ''' 部品表編集ベース情報の員数取得
        ''' </summary>
        ''' <param name="shisakuEventCode">試作イベントコード</param>
        ''' <param name="shisakuBukaCode">試作部課コード</param>
        ''' <param name="shisakuBlockNo">試作ブロックNo</param>
        ''' <param name="buhinNoHyoujiJun">部品番号表示順</param>
        ''' <returns>該当する員数のリスト</returns>
        ''' <remarks></remarks>
        Public Function FindByBaseInsu(ByVal shisakuEventCode As String, _
                                ByVal shisakuBukaCode As String, _
                                ByVal shisakuBlockNo As String, _
                                ByVal buhinNoHyoujiJun As Integer) As List(Of TShisakuBuhinEditGousyaTmpVo) Implements TehaichoHikakuDao.FindByBaseInsu
            Dim sql As String = _
            "SELECT " _
            & " SBI.SHISAKU_EVENT_CODE, " _
            & " SBI.SHISAKU_BUKA_CODE, " _
            & " SBI.SHISAKU_BLOCK_NO, " _
            & " SBI.SHISAKU_BLOCK_NO_KAITEI_NO, " _
            & " BEI.BUHIN_NO_HYOUJI_JUN, " _
            & " B.HYOJIJUN_NO AS SHISAKU_GOUSYA_HYOUJI_JUN, " _
            & " BEI.INSU_SURYO  " _
            & " FROM " _
            & " " & MBOM_DB_NAME & ".dbo.T_SHISAKU_BUHIN_EDIT_INSTL_BASE BEI " _
            & " INNER JOIN " & MBOM_DB_NAME & ".dbo.T_SHISAKU_BUHIN_EDIT_BASE BE " _
            & " ON BE.SHISAKU_EVENT_CODE = BEI.SHISAKU_EVENT_CODE " _
            & " AND BE.SHISAKU_BUKA_CODE = BEI.SHISAKU_BUKA_CODE " _
            & " AND BE.SHISAKU_BLOCK_NO = BEI.SHISAKU_BLOCK_NO " _
            & " AND BE.SHISAKU_BLOCK_NO_KAITEI_NO = BEI.SHISAKU_BLOCK_NO_KAITEI_NO " _
            & " AND BE.BUHIN_NO_HYOUJI_JUN = BEI.BUHIN_NO_HYOUJI_JUN " _
            & " INNER JOIN " & MBOM_DB_NAME & ".dbo.T_SHISAKU_SEKKEI_BLOCK_INSTL SBI " _
            & " ON SBI.SHISAKU_EVENT_CODE = BE.SHISAKU_EVENT_CODE " _
            & " AND SBI.SHISAKU_BUKA_CODE = BE.SHISAKU_BUKA_CODE " _
            & " AND SBI.SHISAKU_BLOCK_NO = BE.SHISAKU_BLOCK_NO " _
            & " AND SBI.SHISAKU_BLOCK_NO_KAITEI_NO = BE.SHISAKU_BLOCK_NO_KAITEI_NO " _
            & " INNER JOIN " & MBOM_DB_NAME & ".dbo.T_SHISAKU_EVENT_BASE B " _
            & " ON B.SHISAKU_EVENT_CODE = SBI.SHISAKU_EVENT_CODE " _
            & " AND B.SHISAKU_GOUSYA = SBI.SHISAKU_GOUSYA " _
            & " WHERE " _
            & " BEI.SHISAKU_EVENT_CODE = @ShisakuEventCode " _
            & " AND BEI.SHISAKU_BUKA_CODE = @ShisakuBukaCode " _
            & " AND BEI.SHISAKU_BLOCK_NO = @ShisakuBlockNo " _
            & " AND BEI.BUHIN_NO_HYOUJI_JUN = @BuhinNoHyoujiJun " _
            & " AND BEI.SHISAKU_BLOCK_NO_KAITEI_NO = '000' " _
            & " ORDER BY SBI.BUHIN_NO_HYOUJI_JUN, SHISAKU_GOUSYA_HYOUJI_JUN "

            Dim db As New EBomDbClient
            Dim param As New TShisakuBuhinEditInstlBaseVo

            param.ShisakuEventCode = shisakuEventCode
            param.ShisakuBukaCode = shisakuBukaCode
            param.ShisakuBlockNo = shisakuBlockNo
            param.BuhinNoHyoujiJun = buhinNoHyoujiJun

            Return db.QueryForList(Of TShisakuBuhinEditGousyaTmpVo)(sql, param)
        End Function

        ''' <summary>
        ''' 部品表編集情報の員数取得
        ''' </summary>
        ''' <param name="shisakuEventCode">試作イベントコード</param>
        ''' <param name="shisakuBukaCode">試作部課コード</param>
        ''' <param name="shisakuBlockNo">試作ブロックNo</param>
        ''' <returns>該当する員数のリスト</returns>
        ''' <remarks></remarks>
        Public Function FindByInsuSuryo(ByVal shisakuEventCode As String, _
                                ByVal shisakuBukaCode As String, _
                                ByVal shisakuBlockNo As String, _
                                ByVal buhinNoHyoujiJun As Integer) As List(Of TShisakuBuhinEditGousyaTmpVo) Implements TehaichoHikakuDao.FindByInsuSuryo
            Dim sql As String = _
            "SELECT " _
            & " SBI.SHISAKU_EVENT_CODE, " _
            & " SBI.SHISAKU_BUKA_CODE, " _
            & " SBI.SHISAKU_BLOCK_NO, " _
            & " SBI.SHISAKU_BLOCK_NO_KAITEI_NO, " _
            & " BEI.BUHIN_NO_HYOUJI_JUN, " _
            & " B.HYOJIJUN_NO AS SHISAKU_GOUSYA_HYOUJI_JUN, " _
            & " BEI.INSU_SURYO  " _
            & " FROM " _
            & " " & MBOM_DB_NAME & ".dbo.T_SHISAKU_BUHIN_EDIT_INSTL BEI " _
            & " INNER JOIN " & MBOM_DB_NAME & ".dbo.T_SHISAKU_BUHIN_EDIT BE " _
            & " ON BE.SHISAKU_EVENT_CODE = BEI.SHISAKU_EVENT_CODE " _
            & " AND BE.SHISAKU_BUKA_CODE = BEI.SHISAKU_BUKA_CODE " _
            & " AND BE.SHISAKU_BLOCK_NO = BEI.SHISAKU_BLOCK_NO " _
            & " AND BE.SHISAKU_BLOCK_NO_KAITEI_NO = BEI.SHISAKU_BLOCK_NO_KAITEI_NO " _
            & " AND BE.BUHIN_NO_HYOUJI_JUN = BEI.BUHIN_NO_HYOUJI_JUN " _
            & " INNER JOIN " & MBOM_DB_NAME & ".dbo.T_SHISAKU_SEKKEI_BLOCK_INSTL SBI " _
            & " ON SBI.SHISAKU_EVENT_CODE = BE.SHISAKU_EVENT_CODE " _
            & " AND SBI.SHISAKU_BUKA_CODE = BE.SHISAKU_BUKA_CODE " _
            & " AND SBI.SHISAKU_BLOCK_NO = BE.SHISAKU_BLOCK_NO " _
            & " AND SBI.SHISAKU_BLOCK_NO_KAITEI_NO = BE.SHISAKU_BLOCK_NO_KAITEI_NO " _
            & " INNER JOIN " & MBOM_DB_NAME & ".dbo.T_SHISAKU_EVENT_BASE B " _
            & " ON B.SHISAKU_EVENT_CODE = SBI.SHISAKU_EVENT_CODE " _
            & " AND B.SHISAKU_GOUSYA = SBI.SHISAKU_GOUSYA " _
            & " WHERE " _
            & " BEI.SHISAKU_EVENT_CODE = @ShisakuEventCode " _
            & " AND BEI.SHISAKU_BUKA_CODE = @ShisakuBukaCode " _
            & " AND BEI.SHISAKU_BLOCK_NO = @ShisakuBlockNo " _
            & " AND BEI.BUHIN_NO_HYOUJI_JUN = @BuhinNoHyoujiJun " _
            & " AND BEI.SHISAKU_BLOCK_NO_KAITEI_NO = ( " _
            & "   SELECT MAX ( CONVERT(INT,COALESCE ( SHISAKU_BLOCK_NO_KAITEI_NO,'' ) ) ) AS SHISAKU_BLOCK_NO_KAITEI_NO  " _
            & " FROM " & MBOM_DB_NAME & ".dbo.T_SHISAKU_BUHIN_EDIT_INSTL " _
            & " WHERE SHISAKU_EVENT_CODE = BEI.SHISAKU_EVENT_CODE " _
            & " AND SHISAKU_BUKA_CODE = BEI.SHISAKU_BUKA_CODE " _
            & " AND SHISAKU_BLOCK_NO = BEI.SHISAKU_BLOCK_NO ) " _
            & " ORDER BY SBI.BUHIN_NO_HYOUJI_JUN, SHISAKU_GOUSYA_HYOUJI_JUN "

            Dim db As New EBomDbClient
            Dim param As New TShisakuBuhinEditInstlVo

            param.ShisakuEventCode = shisakuEventCode
            param.ShisakuBukaCode = shisakuBukaCode
            param.ShisakuBlockNo = shisakuBlockNo
            param.BuhinNoHyoujiJun = buhinNoHyoujiJun

            Return db.QueryForList(Of TShisakuBuhinEditGousyaTmpVo)(sql, param)
        End Function

        ''' <summary>
        ''' 員数の増減チェック
        ''' </summary>
        ''' <param name="shisakuEventCode">試作イベントコード</param>
        ''' <param name="shisakuBukaCode">試作部課コード</param>
        ''' <param name="shisakuBlockNo">試作ブロックNo</param>
        ''' <param name="buhinNoHyoujiJun">部品番号表示順</param>
        ''' <param name="instlHinbanHyoujiJun">INSTL品番表示順</param>
        ''' <returns>増えていればU、減っていればD、同じならE</returns>
        ''' <remarks></remarks>
        Public Function CheckInsuUpDown(ByVal shisakuEventCode As String, _
                                ByVal shisakuBukaCode As String, _
                                ByVal shisakuBlockNo As String, _
                                ByVal buhinNoHyoujiJun As Integer, _
                                ByVal instlHinbanHyoujiJun As Integer, _
                                ByVal insuSuryo As Integer, _
                                ByVal baseFlg As Boolean) As String Implements TehaichoHikakuDao.CheckInsuUpDown

            Dim sFrom As String
            Dim result As String

            If baseFlg Then
                sFrom = " " & MBOM_DB_NAME & ".dbo.T_SHISAKU_BUHIN_EDIT_INSTL "
            Else
                sFrom = " " & MBOM_DB_NAME & ".dbo.T_SHISAKU_BUHIN_EDIT_INSTL_BASE "
            End If

            Dim sql As String = _
            "SELECT * " _
            & " FROM " _
            & sFrom & " " _
            & " WHERE " _
            & " SHISAKU_EVENT_CODE = @ShisakuEventCode " _
            & " AND SHISAKU_BUKA_CODE = @ShisakuBukaCode " _
            & " AND SHISAKU_BLOCK_NO = @ShisakuBlockNo " _
            & " AND BUHIN_NO_HYOUJI_JUN = @BuhinNoHyoujiJun " _
            & " AND INSTL_HINBAN_HYOUJI_JUN = @InstlHinbanHyoujiJun "


            Dim db As New EBomDbClient
            Dim param As New TShisakuBuhinEditInstlVo

            param.ShisakuEventCode = shisakuEventCode
            param.ShisakuBukaCode = shisakuBukaCode
            param.ShisakuBlockNo = shisakuBlockNo
            param.BuhinNoHyoujiJun = buhinNoHyoujiJun
            param.InstlHinbanHyoujiJun = instlHinbanHyoujiJun

            'DBへの干渉のみにすべきか？'
            If baseFlg Then
                Dim baseInstlVo As New TShisakuBuhinEditInstlBaseVo
                baseInstlVo = db.QueryForObject(Of TShisakuBuhinEditInstlBaseVo)(sql, param)

                If baseInstlVo Is Nothing Then
                    result = "D"
                    Return result
                Else
                    If insuSuryo < baseInstlVo.InsuSuryo Then
                        result = "U"
                        Return result
                    ElseIf insuSuryo > baseInstlVo.InsuSuryo Then
                        result = "D"
                        Return result
                    Else
                        result = "E"
                        Return result
                    End If
                End If
            Else
                Dim instlVo As New TShisakuBuhinEditInstlVo
                instlVo = db.QueryForObject(Of TShisakuBuhinEditInstlVo)(sql, param)

                If instlVo Is Nothing Then
                    result = "D"
                    Return result
                Else
                    If insuSuryo < instlVo.InsuSuryo Then
                        result = "D"
                        Return result
                    ElseIf insuSuryo > instlVo.InsuSuryo Then
                        result = "U"
                        Return result
                    Else
                        result = "E"
                        Return result
                    End If
                End If
            End If

        End Function

        ''' <summary>
        ''' 最終の部品番号表示順 + 1を取得する
        ''' </summary>
        ''' <param name="shisakuEventCode">試作イベントコード</param>
        ''' <param name="shisakuBukaCode">試作部課コード</param>
        ''' <param name="shisakuBlockNo">試作ブロックNo</param>
        ''' <returns>最後の部品番号表示順+1、取得できなかったら-1</returns>
        ''' <remarks></remarks>
        Public Function FindByNewBuhinNoHyoujiJun(ByVal shisakuEventCode As String, _
                                ByVal shisakuBukaCode As String, _
                                ByVal shisakuBlockNo As String) As Integer Implements TehaichoHikakuDao.FindByNewBuhinNoHyoujiJun
            Dim sql As String = _
            "SELECT * " _
            & " FROM " & MBOM_DB_NAME & ".dbo.T_SHISAKU_BUHIN_EDIT_TMP TMP " _
            & " WHERE " _
            & " TMP.SHISAKU_EVENT_CODE = '" & shisakuEventCode & "'" _
            & " AND TMP.SHISAKU_BUKA_CODE = '" & shisakuBukaCode & "'" _
            & " AND TMP.SHISAKU_BLOCK_NO = '" & shisakuBlockNo & "'" _
            & " AND TMP.BUHIN_NO_HYOUJI_JUN = ( " _
            & " SELECT MAX ( CONVERT ( INT,COALESCE ( BUHIN_NO_HYOUJI_JUN,'' ) ) ) AS BUHIN_NO_HYOUJI_JUN " _
            & " FROM " & MBOM_DB_NAME & ".dbo.T_SHISAKU_BUHIN_EDIT_TMP " _
            & " WHERE SHISAKU_EVENT_CODE = TMP.SHISAKU_EVENT_CODE " _
            & " AND SHISAKU_BUKA_CODE = TMP.SHISAKU_BUKA_CODE " _
            & " AND SHISAKU_BLOCK_NO = TMP.SHISAKU_BLOCK_NO) "

            Dim db As New EBomDbClient

            Dim vo As New TShisakuBuhinEditTmpVo

            vo = db.QueryForObject(Of TShisakuBuhinEditTmpVo)(sql)

            If Not vo Is Nothing Then
                Dim newBuhinNoHyoujiJun As Integer = vo.BuhinNoHyoujiJun + 1

                Return newBuhinNoHyoujiJun
            End If

            Return -1
        End Function

        ''' <summary>
        ''' 最終の部品番号表示順 + 1を取得する
        ''' </summary>
        ''' <param name="shisakuEventCode">試作イベントコード</param>
        ''' <param name="shisakuBukaCode">試作部課コード</param>
        ''' <param name="shisakuBlockNo">試作ブロックNo</param>
        ''' <returns>最後の部品番号表示順+1、取得できなかったら-1</returns>
        ''' <remarks></remarks>
        Public Function FindByNewGousyaBuhinNoHyoujiJun(ByVal shisakuEventCode As String, _
                                ByVal shisakuBukaCode As String, _
                                ByVal shisakuBlockNo As String) As Integer Implements TehaichoHikakuDao.FindByNewGousyaBuhinNoHyoujiJun
            Dim sql As String = _
            "SELECT * " _
            & " FROM " & MBOM_DB_NAME & ".dbo.T_SHISAKU_BUHIN_EDIT_GOUSYA_TMP TMP " _
            & " WHERE " _
            & " TMP.SHISAKU_EVENT_CODE = '" & shisakuEventCode & "'" _
            & " AND TMP.SHISAKU_BUKA_CODE = '" & shisakuBukaCode & "'" _
            & " AND TMP.SHISAKU_BLOCK_NO = '" & shisakuBlockNo & "'" _
            & " AND TMP.BUHIN_NO_HYOUJI_JUN = ( " _
            & " SELECT MAX ( CONVERT ( INT,COALESCE ( BUHIN_NO_HYOUJI_JUN,'' ) ) ) AS BUHIN_NO_HYOUJI_JUN " _
            & " FROM " & MBOM_DB_NAME & ".dbo.T_SHISAKU_BUHIN_EDIT_GOUSYA_TMP " _
            & " WHERE SHISAKU_EVENT_CODE = TMP.SHISAKU_EVENT_CODE " _
            & " AND SHISAKU_BUKA_CODE = TMP.SHISAKU_BUKA_CODE " _
            & " AND SHISAKU_BLOCK_NO = TMP.SHISAKU_BLOCK_NO) "

            Dim db As New EBomDbClient

            Dim vo As New TShisakuBuhinEditGousyaTmpVo

            vo = db.QueryForObject(Of TShisakuBuhinEditGousyaTmpVo)(sql)

            If Not vo Is Nothing Then
                Dim newBuhinNoHyoujiJun As Integer = vo.BuhinNoHyoujiJun + 1

                Return newBuhinNoHyoujiJun
            End If

            Return -1
        End Function

        ''' <summary>
        ''' 員数の合計(INSTL)を取得
        ''' </summary>
        ''' <param name="shisakuEventCode">試作イベントコード</param>
        ''' <param name="shisakuBukaCode">試作部課コード</param>
        ''' <param name="shisakuBlockNo">試作ブロックNo</param>
        ''' <param name="buhinNoHyoujiJun">部品番号表示順</param>
        ''' <returns>員数の合計、無ければ0</returns>
        ''' <remarks></remarks>
        Public Function FindBySumInsu(ByVal shisakuEventCode As String, _
                                ByVal shisakuBukaCode As String, _
                                ByVal shisakuBlockNo As String, _
                                ByVal buhinNoHyoujiJun As Integer) As Integer Implements TehaichoHikakuDao.FindBySumInsu
            Dim sql As String = _
            "SELECT SUM(INSU_SURYO) INSU_SURYO " _
            & " FROM " & MBOM_DB_NAME & ".dbo.T_SHISAKU_BUHIN_EDIT_INSTL EI " _
            & " WHERE " _
            & " AND EI.SHISAKU_EVENT_CODE = @ShisakuEventCode " _
            & " AND EI.SHISAKU_BUKA_CODE = @ShisakuBukaCode " _
            & " AND EI.SHISAKU_BLOCK_NO = @ShisakuBlockNo " _
            & " AND EI.BUHIN_NO_HYOUJI_JUN = @BuhinNoHyoujiJun " _
            & " AND EI.BUHIN_NO_KAITEI_NO = ( " _
            & " SELECT MAX ( CONVERT ( INT,COALESCE ( SHISAKU_BLOCK_NO_KAITEI_NO,'' ) ) ) AS SHISAKU_BLOCK_NO_KAITEI_NO " _
            & " FROM " & MBOM_DB_NAME & ".dbo.T_SHISAKU_BUHIN_EDIT_INSTL " _
            & " WHERE SHISAKU_EVENT_CODE = EI.SHISAKU_EVENT_CODE " _
            & " AND SHISAKU_BUKA_CODE = EI.SHISAKU_BUKA_CODE " _
            & " AND SHISAKU_BLOCK_NO = EI.SHISAKU_BLOCK_NO) "

            Dim db As New EBomDbClient
            Dim param As New TShisakuBuhinEditInstlBaseVo
            param.ShisakuEventCode = shisakuEventCode
            param.ShisakuBukaCode = shisakuBukaCode
            param.ShisakuBlockNo = shisakuBlockNo
            param.BuhinNoHyoujiJun = buhinNoHyoujiJun

            Dim vo As New TShisakuBuhinEditInstlVo
            vo = db.QueryForObject(Of TShisakuBuhinEditInstlVo)(sql, param)

            If Not vo Is Nothing Then
                Return vo.InsuSuryo
            End If

            Return 0

        End Function

        ''' <summary>
        ''' 員数の合計(INSTLBase)
        ''' </summary>
        ''' <param name="shisakuEventCode">試作イベントコード</param>
        ''' <param name="shisakuBukaCode">試作部課コード</param>
        ''' <param name="shisakuBlockNo">試作ブロックNo</param>
        ''' <param name="buhinNoHyoujiJun">部品番号表示順</param>
        ''' <returns>員数、無ければ0</returns>
        ''' <remarks></remarks>
        Public Function FindBySumInsuTmp(ByVal shisakuEventCode As String, _
                                ByVal shisakuBukaCode As String, _
                                ByVal shisakuBlockNo As String, _
                                ByVal buhinNoHyoujiJun As Integer) As Integer Implements TehaichoHikakuDao.FindBySumInsuTmp
            Dim sql As String = _
            "SELECT SUM(INSU_SURYO) INSU_SURYO " _
            & " FROM " & MBOM_DB_NAME & ".dbo.T_SHISAKU_BUHIN_EDIT_INSTL_BASE EI " _
            & " WHERE " _
            & " AND EI.SHISAKU_EVENT_CODE = @ShisakuEventCode " _
            & " AND EI.SHISAKU_BUKA_CODE = @ShisakuBukaCode " _
            & " AND EI.SHISAKU_BLOCK_NO = @ShisakuBlockNo " _
            & " AND EI.BUHIN_NO_HYOUJI_JUN = @BuhinNoHyoujiJun " _
            & " AND EI.BUHIN_NO_KAITEI_NO = ( " _
            & " SELECT MAX ( CONVERT ( INT,COALESCE ( SHISAKU_BLOCK_NO_KAITEI_NO,'' ) ) ) AS SHISAKU_BLOCK_NO_KAITEI_NO " _
            & " FROM " & MBOM_DB_NAME & ".dbo.T_SHISAKU_BUHIN_EDIT_INSTL_BASE " _
            & " WHERE SHISAKU_EVENT_CODE = EI.SHISAKU_EVENT_CODE " _
            & " AND SHISAKU_BUKA_CODE = EI.SHISAKU_BUKA_CODE " _
            & " AND SHISAKU_BLOCK_NO = EI.SHISAKU_BLOCK_NO) "

            Dim db As New EBomDbClient
            Dim param As New TShisakuBuhinEditInstlBaseVo
            param.ShisakuEventCode = shisakuEventCode
            param.ShisakuBukaCode = shisakuBukaCode
            param.ShisakuBlockNo = shisakuBlockNo
            param.BuhinNoHyoujiJun = buhinNoHyoujiJun

            Dim vo As New TShisakuBuhinEditInstlBaseVo

            vo = db.QueryForObject(Of TShisakuBuhinEditInstlBaseVo)(sql, param)

            If Not vo Is Nothing Then
                Return vo.InsuSuryo
            End If

            Return 0
        End Function

        ''' <summary>
        ''' 開発符号を取得
        ''' </summary>
        ''' <param name="shisakuEventCode">試作イベントコード</param>
        ''' <returns>開発符号、無ければNOTHING</returns>
        ''' <remarks></remarks>
        Public Function FindByKaihatsuFugo(ByVal shisakuEventCode As String) As String Implements TehaichoHikakuDao.FindByKaihatsuFugo
            Dim sql As String = _
            " SELECT * " _
            & " FROM " & MBOM_DB_NAME & ".dbo.T_SHISAKU_EVENT " _
            & " WHERE " _
            & " SHISAKU_EVENT_CODE = @ShisakuEventCode "

            Dim db As New EBomDbClient
            Dim param As New TShisakuEventVo
            param.ShisakuEventCode = shisakuEventCode

            Dim vo As New TShisakuEventVo
            vo = db.QueryForObject(Of TShisakuEventVo)(sql, param)

            If Not vo Is Nothing Then
                Return vo.ShisakuKaihatsuFugo
            End If

            Return Nothing
        End Function

        ''' <summary>
        ''' 号車のリストを取得
        ''' </summary>
        ''' <param name="shisakuEventCode">イベントコード</param>
        ''' <param name="shisakuGroup">グループNo</param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Function FindByGousyaList(ByVal shisakuEventCode As String, ByVal shisakuGroup As String) Implements TehaichoHikakuDao.FindByGousyaList
            Dim sql As String = _
            " SELECT * " _
            & " FROM " & MBOM_DB_NAME & ".dbo.T_SHISAKU_EVENT_KANSEI K " _
            & " INNER JOIN " & MBOM_DB_NAME & ".dbo.T_SHISAKU_EVENT_BASE B" _
            & " ON B.SHISAKU_EVENT_CODE = K.SHISAKU_EVENT_CODE " _
            & " AND B.HYOJIJUN_NO = K.HYOJIJUN_NO " _
            & " WHERE " _
            & " K.SHISAKU_EVENT_CODE = '" & shisakuEventCode & "'" _
            & " AND K.SHISAKU_GROUP = '" & shisakuGroup & "'"

            Dim db As New EBomDbClient
            Return db.QueryForList(Of TShisakuEventBaseVo)(sql)
        End Function

        ''' <summary>
        ''' 部品編集INSTL情報を取得する
        ''' </summary>
        ''' <param name="shisakuEventCode">イベントコード</param>
        ''' <param name="shisakuBukaCode">部課コード</param>
        ''' <param name="shisakuBlockNo">ブロックNo</param>
        ''' <param name="shisakuBlockNoKaiteiNo">ブロックNo改訂No</param>
        ''' <param name="BuhinNoHyoujiJun">部品番号表示順</param>
        ''' <returns>該当する部品編集INSTL情報</returns>
        ''' <remarks></remarks>
        Public Function FindByBuhinEditGousya(ByVal shisakuEventCode As String, _
                                              ByVal shisakuBukaCode As String, _
                                              ByVal shisakuBlockNo As String, _
                                              ByVal shisakuBlockNoKaiteiNo As String, _
                                              ByVal BuhinNoHyoujiJun As Integer) As List(Of TShisakuBuhinEditGousyaTmpVo) Implements TehaichoHikakuDao.FindByBuhinEditGousya
            Dim sql As String = _
            " SELECT * " _
            & " FROM " & MBOM_DB_NAME & ".dbo.T_SHISAKU_BUHIN_EDIT_GOUSYA_TMP G " _
            & " WHERE " _
            & " G.SHISAKU_EVENT_CODE = '" & shisakuEventCode & "' " _
            & " AND G.SHISAKU_BUKA_CODE =  '" & shisakuBukaCode & "' " _
            & " AND G.SHISAKU_BLOCK_NO =  '" & shisakuBlockNo & "' " _
            & " AND G.BUHIN_NO_HYOUJI_JUN = " & BuhinNoHyoujiJun & "" _
            & " AND G.SHISAKU_BLOCK_NO_KAITEI_NO = '" & shisakuBlockNoKaiteiNo & "'" _
            & " AND G.GYOU_ID = '999' " _
            & " ORDER BY G.SHISAKU_GOUSYA_HYOUJI_JUN "

            Dim db As New EBomDbClient
            Return db.QueryForList(Of TShisakuBuhinEditGousyaTmpVo)(sql)
        End Function

        ''' <summary>
        ''' 部品編集INSTL情報を取得する
        ''' </summary>
        ''' <param name="shisakuEventCode">イベントコード</param>
        ''' <param name="shisakuBukaCode">部課コード</param>
        ''' <param name="shisakuBlockNo">ブロックNo</param>
        ''' <param name="BuhinNoHyoujiJun">部品番号表示順</param>
        ''' <returns>該当するベースとなる部品編集INSTL情報</returns>
        ''' <remarks></remarks>
        Public Function FindByBuhinEditGousyaBase(ByVal shisakuEventCode As String, _
                                                 ByVal shisakuBukaCode As String, _
                                                 ByVal shisakuBlockNo As String, _
                                                 ByVal BuhinNoHyoujiJun As Integer) As List(Of TShisakuBuhinEditGousyaTmpVo) Implements TehaichoHikakuDao.FindByBuhinEditGousyaBase

            Dim sb As New StringBuilder
            With sb
                .Remove(0, .Length)
                .AppendLine(" SELECT * ")
                .AppendLine(" FROM " & MBOM_DB_NAME & ".dbo.T_SHISAKU_BUHIN_EDIT_GOUSYA_TMP BEI ")
                .AppendLine(" WHERE ")
                .AppendLine(" BEI.SHISAKU_EVENT_CODE = '" & shisakuEventCode & "' ")
                .AppendLine(" AND BEI.SHISAKU_BUKA_CODE =  '" & shisakuBukaCode & "' ")
                .AppendLine(" AND BEI.SHISAKU_BLOCK_NO =  '" & shisakuBlockNo & "' ")
                .AppendLine(" AND BEI.BUHIN_NO_HYOUJI_JUN = " & BuhinNoHyoujiJun & "")
                .AppendLine(" AND BEI.SHISAKU_BLOCK_NO_KAITEI_NO = '000' ")
                .AppendLine(" AND BEI.GYOU_ID = '888' ")
                .AppendLine(" ORDER BY BEI.SHISAKU_GOUSYA_HYOUJI_JUN ")

            End With

            Dim db As New EBomDbClient
            Return db.QueryForList(Of TShisakuBuhinEditGousyaTmpVo)(sb.ToString)
        End Function

        ''' <summary>
        ''' ベースの情報を取得する
        ''' </summary>
        ''' <param name="shisakuEventCode">イベントコード</param>
        ''' <param name="shisakuGroup">グループNo</param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Function FindByBaseTmpVoList(ByVal shisakuEventCode As String, ByVal shisakuGroup As String, ByVal JikyuFlg As Boolean) As List(Of TehaichoBuhinEditTmpVo) Implements TehaichoHikakuDao.FindByBaseTmpVoList
            Dim Jikyuhin As String
            '自給品の消しこみがありかなしかでSQL文を追加'
            If JikyuFlg = True Then
                Jikyuhin = " AND NOT ( BE.SHUKEI_CODE = 'J' AND BE.SIA_SHUKEI_CODE = 'J' OR BE.SHUKEI_CODE = 'J' AND  BE.SIA_SHUKEI_CODE  <> 'J' OR BE.SHUKEI_CODE = ' ' AND BE.SIA_SHUKEI_CODE = 'J') "
            Else
                Jikyuhin = ""
            End If

            Dim db As New EBomDbClient

            Dim BuhinList As New List(Of TehaichoBuhinEditTmpVo)

            Dim sql1 As New System.Text.StringBuilder
            With sql1
                .AppendFormat(" SELECT B.* ")
                .AppendFormat(" FROM {0}.dbo.T_SHISAKU_EVENT_KANSEI K ", MBOM_DB_NAME)
                .AppendFormat(" INNER JOIN {0}.dbo.T_SHISAKU_EVENT_BASE B ", MBOM_DB_NAME)
                .AppendFormat(" ON B.SHISAKU_EVENT_CODE = K.SHISAKU_EVENT_CODE ")
                .AppendFormat(" AND B.HYOJIJUN_NO = K.HYOJIJUN_NO ")
                .AppendFormat(" AND (NOT B.SHISAKU_SYUBETU = 'D' OR B.SHISAKU_SYUBETU = 'W' OR B.SHISAKU_SYUBETU IS NULL) ")
                .AppendFormat(" WHERE ")
                .AppendFormat(" K.SHISAKU_EVENT_CODE = '{0}'", shisakuEventCode)
                .AppendFormat(" AND K.SHISAKU_GROUP = '{0}'", shisakuGroup)
                .AppendFormat(" ORDER BY B.HYOJIJUN_NO ")
            End With

            '小分けにする'
            Dim BaseList As List(Of TShisakuEventBaseVo) = db.QueryForList(Of TShisakuEventBaseVo)(sql1.ToString)

            '号車リスト'
            For Each BaseVo As TShisakuEventBaseVo In BaseList
                Dim sql2 As New System.Text.StringBuilder
                With sql2
                    .AppendFormat(" SELECT SBI.*, SB.KACHOU_SYOUNIN_JYOUTAI ")
                    .AppendFormat(" FROM {0}.dbo.T_SHISAKU_SEKKEI_BLOCK_INSTL SBI ", MBOM_DB_NAME)
                    .AppendFormat(" INNER JOIN {0}.dbo.T_SHISAKU_SEKKEI_BLOCK SB ", MBOM_DB_NAME)
                    .AppendFormat(" ON SB.SHISAKU_EVENT_CODE = SBI.SHISAKU_EVENT_CODE")
                    .AppendFormat(" AND SB.SHISAKU_BUKA_CODE = SBI.SHISAKU_BUKA_CODE ")
                    .AppendFormat(" AND SB.SHISAKU_BLOCK_NO = SBI.SHISAKU_BLOCK_NO ")
                    .AppendFormat(" AND SB.SHISAKU_BLOCK_NO_KAITEI_NO = 000 ")
                    .AppendFormat(" AND ( SB.BLOCK_FUYOU = '0' OR SB.BLOCK_FUYOU = '') ")
                    .AppendFormat(" WHERE  ")
                    .AppendFormat(" SBI.SHISAKU_EVENT_CODE = '{0}'", BaseVo.ShisakuEventCode)
                    .AppendFormat(" AND SBI.SHISAKU_GOUSYA = '{0}'", BaseVo.ShisakuGousya)
                    .AppendFormat(" AND SBI.SHISAKU_BLOCK_NO_KAITEI_NO = '  0' ")
                    .AppendFormat(" AND NOT SBI.INSU_SURYO IS NULL ")
                    .AppendFormat(" AND SBI.INSU_SURYO > 0 ")
                    .AppendFormat(" ORDER BY SBI.SHISAKU_BLOCK_NO ")
                End With

                Dim sekkeiBlockInstlList As List(Of TShisakuSekkeiBlockInstlVo) = db.QueryForList(Of TShisakuSekkeiBlockInstlVo)(sql2.ToString)

                '設計ブロックINSTLリスト'
                For Each sekkeiBlockInstlVo As TShisakuSekkeiBlockInstlVo In sekkeiBlockInstlList
                    Dim sql3 As New System.Text.StringBuilder
                    With sql3
                        .AppendFormat(" SELECT BE.*, BEI.INSU_SURYO ")
                        .AppendFormat(" FROM {0}.dbo.T_SHISAKU_BUHIN_EDIT_INSTL_BASE BEI ", MBOM_DB_NAME)
                        .AppendFormat(" INNER JOIN {0}.dbo.T_SHISAKU_BUHIN_EDIT_BASE BE ", MBOM_DB_NAME)
                        .AppendFormat(" ON BE.SHISAKU_EVENT_CODE = BEI.SHISAKU_EVENT_CODE ")
                        .AppendFormat(" AND BE.SHISAKU_BUKA_CODE = BEI.SHISAKU_BUKA_CODE ")
                        .AppendFormat(" AND BE.SHISAKU_BLOCK_NO = BEI.SHISAKU_BLOCK_NO ")
                        .AppendFormat(" AND BE.SHISAKU_BLOCK_NO_KAITEI_NO = BEI.SHISAKU_BLOCK_NO_KAITEI_NO ")
                        .AppendFormat(" AND BE.BUHIN_NO_HYOUJI_JUN = BEI.BUHIN_NO_HYOUJI_JUN ")
                        .AppendFormat(Jikyuhin)
                        .AppendFormat(" WHERE ")
                        .AppendFormat(" BEI.SHISAKU_EVENT_CODE = '{0}'", sekkeiBlockInstlVo.ShisakuEventCode)
                        .AppendFormat(" AND BEI.SHISAKU_BUKA_CODE = '{0}'", sekkeiBlockInstlVo.ShisakuBukaCode)
                        .AppendFormat(" AND BEI.SHISAKU_BLOCK_NO = '{0}'", sekkeiBlockInstlVo.ShisakuBlockNo)
                        .AppendFormat(" AND BEI.SHISAKU_BLOCK_NO_KAITEI_NO = '000'")
                        .AppendFormat(" AND BEI.INSTL_HINBAN_HYOUJI_JUN = '{0}'", sekkeiBlockInstlVo.InstlHinbanHyoujiJun)
                    End With

                    Dim buhinEditList As List(Of TehaichoBuhinEditTmpVo) = db.QueryForList(Of TehaichoBuhinEditTmpVo)(sql3.ToString)

                    For Each buhinEditVo As TehaichoBuhinEditTmpVo In buhinEditList

                        Dim buhinNo As String = Trim(buhinEditVo.BuhinNo)
                        buhinEditVo.BuhinNo = buhinNo

                        buhinEditVo.ShisakuGousya = BaseVo.ShisakuGousya
                        buhinEditVo.ShisakuGousyaHyoujiJun = BaseVo.HyojijunNo

                        BuhinList.Add(buhinEditVo)
                    Next
                Next
            Next

            Return BuhinList
        End Function

#End Region


#Region "更新する処理"

        ''' <summary>
        ''' 部品編集情報(TMP)を更新する(比較結果->変更無し)
        ''' </summary>
        ''' <param name="BuhinEditVo">試作部品編集情報</param>
        ''' <remarks></remarks>
        Public Sub UpdateByBuhinTmpNotChange(ByVal BuhinEditVo As TShisakuBuhinEditVo) Implements TehaichoHikakuDao.UpdateByBuhinTmpNotChange
            Dim sql As String = _
            " UPDATE " & MBOM_DB_NAME & ".dbo.T_SHISAKU_BUHIN_EDIT_TMP " _
            & " SET TEHAI_KIGOU = @TehaiKigou, " _
            & " UPDATED_USER_ID = @UpdatedUserId, " _
            & " UPDATED_DATE = @UpdatedDate, " _
            & " UPDATED_TIME = @UpdatedTime " _
            & " WHERE SHISAKU_EVENT_CODE = @ShisakuEventCode " _
            & " AND SHISAKU_BUKA_CODE = @ShisakuBukaCode " _
            & " AND SHISAKU_BLOCK_NO = @ShisakuBlockNo " _
            & " AND SHISAKU_BLOCK_NO_KAITEI_NO = @ShisakuBlockNoKaiteiNo " _
            & " AND BUHIN_NO_HYOUJI_JUN = @BuhinNoHyoujiJun "

            Dim db As New EBomDbClient
            Dim aDate As New ShisakuDate
            Dim param As New TShisakuBuhinEditTmpVo

            param.ShisakuEventCode = BuhinEditVo.ShisakuEventCode
            param.ShisakuBukaCode = BuhinEditVo.ShisakuBukaCode
            param.ShisakuBlockNo = BuhinEditVo.ShisakuBlockNo
            param.ShisakuBlockNoKaiteiNo = BuhinEditVo.ShisakuBlockNoKaiteiNo
            param.BuhinNoHyoujiJun = BuhinEditVo.BuhinNoHyoujiJun
            If BuhinEditVo.Level = 0 Then
                param.TehaiKigou = ""
            Else
                param.TehaiKigou = "F"
            End If

            param.UpdatedUserId = LoginInfo.Now.UserId
            param.UpdatedDate = aDate.CurrentDateDbFormat
            param.UpdatedTime = aDate.CurrentTimeDbFormat

            db.Update(sql, param)
        End Sub

        ''' <summary>
        ''' 部品編集号車情報(TMP)を更新する(比較結果->変更無し)
        ''' </summary>
        ''' <param name="GousyaVo">試作部品編集号車情報</param>
        ''' <remarks></remarks>
        Public Sub UpdateByGousyaTmpNotChange(ByVal GousyaVo As TShisakuBuhinEditGousyaTmpVo, _
                                              ByVal gousyaNo As Integer) Implements TehaichoHikakuDao.UpdateByGousyaTmpNotChange
            Dim sql As String = _
            " UPDATE " & MBOM_DB_NAME & ".dbo.T_SHISAKU_BUHIN_EDIT_GOUSYA_TMP  " _
            & " SET " _
            & " UPDATED_USER_ID = @UpdatedUserId, " _
            & " UPDATED_DATE = @UpdatedDate, " _
            & " UPDATED_TIME = @UpdatedTime " _
            & " WHERE SHISAKU_EVENT_CODE = @ShisakuEventCode " _
            & " AND SHISAKU_BUKA_CODE = @ShisakuBukaCode " _
            & " AND SHISAKU_BLOCK_NO = @ShisakuBlockNo " _
            & " AND SHISAKU_BLOCK_NO_KAITEI_NO = @ShisakuBlockNoKaiteiNo " _
            & " AND BUHIN_NO_HYOUJI_JUN = @BuhinNoHyoujiJun " _
            & " AND SHISAKU_GOUSYA_HYOUJI_JUN = @ShisakuGousyaHyoujiJun "

            Dim db As New EBomDbClient
            Dim aDate As New ShisakuDate
            Dim param As New TShisakuBuhinEditGousyaTmpVo

            param.ShisakuEventCode = GousyaVo.ShisakuEventCode
            param.ShisakuBukaCode = GousyaVo.ShisakuBukaCode
            param.ShisakuBlockNo = GousyaVo.ShisakuBlockNo
            param.ShisakuBlockNoKaiteiNo = GousyaVo.ShisakuBlockNoKaiteiNo
            param.BuhinNoHyoujiJun = GousyaVo.BuhinNoHyoujiJun
            param.ShisakuGousyaHyoujiJun = gousyaNo

            param.UpdatedUserId = LoginInfo.Now.UserId
            param.UpdatedDate = aDate.CurrentDateDbFormat
            param.UpdatedTime = aDate.CurrentTimeDbFormat

            db.Update(sql, param)
        End Sub

        ''' <summary>
        ''' 部品編集情報(TMP)を更新する(集計コードからの展開)
        ''' </summary>
        ''' <param name="shisakuTmp">試作部品編集情報TMPのリスト</param>
        ''' <remarks></remarks>
        Public Sub UpdateByBuhinTmpSyukeiTenkai(ByVal shisakuTmp As List(Of TShisakuBuhinEditTmpVo), ByVal SeihinKbn As String) Implements TehaichoHikakuDao.UpdateByBuhinTmpSyukeiTenkai
            Dim db As New EBomDbClient
            Dim aDate As New ShisakuDate

            Dim impl As TehaichoSakuseiDao = New TehaichoSakuseiDaoImpl
            Dim eventVo As New TShisakuEventVo

            If shisakuTmp.Count = 0 Then
                Return
            End If

            eventVo = impl.FindByEventName(shisakuTmp(0).ShisakuEventCode)

            Using update As New SqlAccess(g_kanrihyoIni(DB_KEY_EBOM))
                update.Open()
                update.BeginTransaction()

                For index As Integer = 0 To shisakuTmp.Count - 1
                    Dim param As New TShisakuBuhinEditTmpVo

                    Dim buhinNoOyaVo As New TShisakuBuhinEditTmpVo
                    Dim buhinEditTmpVo As New TShisakuBuhinEditTmpVo

                    param.ShisakuEventCode = shisakuTmp(index).ShisakuEventCode
                    param.ShisakuBukaCode = shisakuTmp(index).ShisakuBukaCode
                    param.ShisakuBlockNo = shisakuTmp(index).ShisakuBlockNo
                    param.ShisakuBlockNoKaiteiNo = shisakuTmp(index).ShisakuBlockNoKaiteiNo
                    param.BuhinNoHyoujiJun = shisakuTmp(index).BuhinNoHyoujiJun
                    param.KyoukuSection = shisakuTmp(index).KyoukuSection

                    'レベル0(親品番は展開対象外)'
                    'If shisakuTmp(index).Level = 0 Then
                    '    Continue For
                    'End If

                    '手配記号がすでにFなら何もしない'
                    If shisakuTmp(index).TehaiKigou = "F" Then
                        Continue For
                    Else
                        '比較織り込みで手配記号Fにならないやつはいったい誰？'
                        Dim a As String = shisakuTmp(index).BuhinNo
                    End If

                    '集計コードがあるかチェック(無ければ海外集計有)'
                    If shisakuTmp(index).ShukeiCode.TrimEnd <> "" Then
                        If shisakuTmp(index).ShukeiCode = "X" Then
                            param.TehaiKigou = "F"
                            param.Nouba = ""
                        ElseIf shisakuTmp(index).ShukeiCode = "A" Then
                            param.TehaiKigou = ""
                            param.Nouba = "X1"
                        ElseIf shisakuTmp(index).ShukeiCode = "E" Or shisakuTmp(index).ShukeiCode = "Y" Then
                            '専用品チェック'
                            If StringUtil.IsEmpty(shisakuTmp(index).SenyouMark) Then
                                '共用品なら'
                                param.TehaiKigou = "A"
                                param.Nouba = "A0"
                            Else
                                '専用品なら'
                                param.TehaiKigou = "D"
                                param.Nouba = "X1"
                                param.SenyouMark = "*"
                            End If
                        ElseIf shisakuTmp(index).ShukeiCode = "R" Or shisakuTmp(index).ShukeiCode = "J" Then
                            param.TehaiKigou = "F"
                            param.Nouba = ""
                        End If
                    Else

                        '海外集計コード'
                        '取引先コードで処理が分岐する'
                        If StringUtil.IsEmpty(shisakuTmp(index).MakerCode) Then
                            If shisakuTmp(index).SiaShukeiCode = "X" Then
                                param.TehaiKigou = "F"
                                param.Nouba = ""
                            ElseIf shisakuTmp(index).SiaShukeiCode = "A" Then
                                param.TehaiKigou = "J"
                                param.Nouba = "US"
                            ElseIf shisakuTmp(index).SiaShukeiCode = "E" Then
                                param.TehaiKigou = "B"
                                param.Nouba = "US"
                            ElseIf shisakuTmp(index).SiaShukeiCode = "Y" Then
                                param.TehaiKigou = "B"
                                param.Nouba = "US"
                            ElseIf shisakuTmp(index).SiaShukeiCode = "J" Or shisakuTmp(index).SiaShukeiCode = "R" Then
                                param.TehaiKigou = "F"
                                param.Nouba = ""
                            End If
                        Else
                            If Left(shisakuTmp(index).MakerCode, 1) <> "A" Then
                                If shisakuTmp(index).SiaShukeiCode = "X" Then
                                    param.TehaiKigou = "F"
                                    param.Nouba = ""
                                ElseIf shisakuTmp(index).SiaShukeiCode = "A" Then
                                    param.TehaiKigou = ""
                                    param.Nouba = "X1"
                                ElseIf shisakuTmp(index).SiaShukeiCode = "E" Or shisakuTmp(index).SiaShukeiCode = "Y" Then
                                    '専用品チェック'
                                    If StringUtil.IsEmpty(shisakuTmp(index).SenyouMark) Then
                                        '共用品なら'
                                        param.TehaiKigou = "A"
                                        param.Nouba = "A0"
                                    Else
                                        '専用品なら'
                                        param.TehaiKigou = "D"
                                        param.Nouba = "X1"
                                        param.SenyouMark = "*"
                                    End If
                                ElseIf shisakuTmp(index).SiaShukeiCode = "R" Or shisakuTmp(index).SiaShukeiCode = "J" Then
                                    param.TehaiKigou = "F"
                                    param.Nouba = ""
                                End If
                            Else

                                If shisakuTmp(index).SiaShukeiCode = "X" Then
                                    param.TehaiKigou = "F"
                                    param.Nouba = ""
                                ElseIf shisakuTmp(index).SiaShukeiCode = "A" Then
                                    param.TehaiKigou = "J"
                                    param.Nouba = "US"
                                ElseIf shisakuTmp(index).SiaShukeiCode = "E" Then
                                    param.TehaiKigou = "B"
                                    param.Nouba = "US"
                                ElseIf shisakuTmp(index).SiaShukeiCode = "Y" Then
                                    param.TehaiKigou = "B"
                                    param.Nouba = "US"
                                ElseIf shisakuTmp(index).SiaShukeiCode = "J" Or shisakuTmp(index).SiaShukeiCode = "R" Then
                                    param.TehaiKigou = "F"
                                    param.Nouba = ""
                                End If
                            End If
                        End If
                    End If

                    'If _Eventvo.BlockAlertKind = 2 AndAlso _
                    '   _Eventvo.KounyuShijiFlg = "0" AndAlso _
                    '   shisakuTmp(index).BaseBuhinFlg = "1" Then

                    '    param.TehaiKigou = "F"
                    '    param.Nouba = ""
                    '    param.Henkaten = ""


                    'End If



                    param.UpdatedUserId = LoginInfo.Now.UserId
                    param.UpdatedDate = aDate.CurrentDateDbFormat
                    param.UpdatedTime = aDate.CurrentTimeDbFormat

                    Dim sql As New System.Text.StringBuilder
                    With sql
                        .AppendFormat(" UPDATE {0}.dbo.T_SHISAKU_BUHIN_EDIT_TMP ", MBOM_DB_NAME)
                        .AppendLine(" SET ")
                        .AppendFormat(" TEHAI_KIGOU = '{0}' ", param.TehaiKigou)
                        .AppendFormat(" ,NOUBA = '{0}' ", param.Nouba)
                        .AppendFormat(" ,KYOUKU_SECTION = '{0}' ", param.KyoukuSection)
                        .AppendFormat(" ,UPDATED_USER_ID = '{0}' ", param.UpdatedUserId)
                        .AppendFormat(" ,UPDATED_DATE = '{0}' ", param.UpdatedDate)
                        .AppendFormat(" ,UPDATED_TIME = '{0}' ", param.UpdatedTime)
                        .AppendFormat(" WHERE SHISAKU_EVENT_CODE = '{0}' ", param.ShisakuEventCode)
                        .AppendFormat(" AND SHISAKU_BUKA_CODE = '{0}' ", param.ShisakuBukaCode)
                        .AppendFormat(" AND SHISAKU_BLOCK_NO = '{0}' ", param.ShisakuBlockNo)
                        .AppendFormat(" AND SHISAKU_BLOCK_NO_KAITEI_NO = '{0}' ", param.ShisakuBlockNoKaiteiNo)
                        .AppendFormat(" AND BUHIN_NO_HYOUJI_JUN = '{0}' ", param.BuhinNoHyoujiJun)
                    End With

                    update.ExecuteNonQuery(sql.ToString)

                Next
                update.Commit()
            End Using

        End Sub

        Public Sub UpdateByBuhinTmpIkanshaKaishu(ByVal eventCode As String, ByVal listCode As String) Implements TehaichoHikakuDao.UpdateByBuhinTmpIkanshaKaishu
            Using update As New SqlAccess(g_kanrihyoIni(DB_KEY_EBOM))
                update.Open()
                update.BeginTransaction()
                Dim sql As New System.Text.StringBuilder
                With sql
                    .AppendFormat(" UPDATE {0}.dbo.T_SHISAKU_BUHIN_EDIT_TMP ", MBOM_DB_NAME)
                    .AppendLine(" SET ")
                    .AppendFormat(" TEHAI_KIGOU = 'F' ")
                    .AppendFormat(" ,HENKATEN = '' ")
                    .AppendFormat(" WHERE SHISAKU_EVENT_CODE = '{0}' ", eventCode)
                    .AppendFormat(" AND SHISAKU_LIST_CODE = '{0}' ", listCode)
                    .AppendFormat(" AND BASE_BUHIN_FLG = '1' ")
                End With
                update.ExecuteNonQuery(sql.ToString)
                update.Commit()
            End Using
        End Sub


        ''' <summary>
        ''' 部品編集情報(TMP)を更新する(比較結果->追加品番)
        ''' </summary>
        ''' <param name="shisakuTmp">試作部品編集情報TMP</param>
        ''' <remarks></remarks>
        Public Sub UpdateByBuhinTmpTsuikaHinban(ByVal shisakuTmp As TShisakuBuhinEditVo, ByVal SeihinKbn As String) Implements TehaichoHikakuDao.UpdateByBuhinTmpTsuikaHinban
            Dim sql As String = _
            " UPDATE " & MBOM_DB_NAME & ".dbo.T_SHISAKU_BUHIN_EDIT_TMP " _
            & " SET " _
            & " TEHAI_KIGOU = @TehaiKigou, " _
            & " NOUBA = @Nouba, " _
            & " KYOUKU_SECTION = @KyoukuSection, " _
            & " HENKATEN = @Henkaten, " _
            & " UPDATED_USER_ID = @UpdatedUserId, " _
            & " UPDATED_DATE = @UpdatedDate, " _
            & " UPDATED_TIME = @UpdatedTime " _
            & " WHERE SHISAKU_EVENT_CODE = @ShisakuEventCode " _
            & " AND SHISAKU_BUKA_CODE = @ShisakuBukaCode " _
            & " AND SHISAKU_BLOCK_NO = @ShisakuBlockNo " _
            & " AND SHISAKU_BLOCK_NO_KAITEI_NO = @ShisakuBlockNoKaiteiNo " _
            & " AND BUHIN_NO_HYOUJI_JUN = @BuhinNoHyoujiJun "

            Dim db As New EBomDbClient
            Dim aDate As New ShisakuDate
            Dim param As New TShisakuBuhinEditTmpVo

            Dim impl As TehaichoSakuseiDao = New TehaichoSakuseiDaoImpl

            param.ShisakuEventCode = shisakuTmp.ShisakuEventCode
            param.ShisakuBukaCode = shisakuTmp.ShisakuBukaCode
            param.ShisakuBlockNo = shisakuTmp.ShisakuBlockNo
            param.ShisakuBlockNoKaiteiNo = shisakuTmp.ShisakuBlockNoKaiteiNo
            param.BuhinNoHyoujiJun = shisakuTmp.BuhinNoHyoujiJun
            param.KyoukuSection = shisakuTmp.KyoukuSection

            Dim kaihatsuFugo As String = ""
            Dim MakerCode As String = ""

            '集計コードがあるかチェック(無ければ海外集計有)'
            If Not StringUtil.IsEmpty(shisakuTmp.ShukeiCode) Then
                If shisakuTmp.ShukeiCode = "X" Then
                    param.TehaiKigou = "F"
                    param.Nouba = ""
                ElseIf shisakuTmp.ShukeiCode = "A" Then
                    param.TehaiKigou = ""
                    param.Nouba = "X1"
                ElseIf shisakuTmp.ShukeiCode = "E" Or shisakuTmp.ShukeiCode = "Y" Then
                    param.TehaiKigou = "D"
                    param.Nouba = ""
                ElseIf shisakuTmp.ShukeiCode = "R" Or shisakuTmp.ShukeiCode = "J" Then
                    param.TehaiKigou = "F"
                    param.Nouba = ""
                End If
            Else
                If StringUtil.IsEmpty(shisakuTmp.MakerCode) Then
                    If shisakuTmp.SiaShukeiCode = "X" Then
                        param.TehaiKigou = "F"
                        param.Nouba = ""
                    ElseIf shisakuTmp.SiaShukeiCode = "A" Then
                        param.TehaiKigou = ""
                        param.Nouba = "X1"
                    ElseIf shisakuTmp.SiaShukeiCode = "E" Or shisakuTmp.SiaShukeiCode = "Y" Then
                        param.TehaiKigou = "D"
                        param.Nouba = ""
                    ElseIf shisakuTmp.SiaShukeiCode = "R" Or shisakuTmp.SiaShukeiCode = "J" Then
                        param.TehaiKigou = "F"
                        param.Nouba = ""
                    End If
                Else
                    If StringUtil.Equals(Left(shisakuTmp.MakerCode, 1), "A") Then
                        '海外集計コード'
                        If shisakuTmp.SiaShukeiCode = "A" Then
                            param.TehaiKigou = "J"
                            param.Nouba = "US"
                        ElseIf shisakuTmp.SiaShukeiCode = "E" Then
                            param.TehaiKigou = "B"
                            param.Nouba = "US"

                        ElseIf shisakuTmp.SiaShukeiCode = "Y" Then
                            param.TehaiKigou = "B"
                            param.Nouba = "US"
                        End If
                    Else
                        If shisakuTmp.SiaShukeiCode = "X" Then
                            param.TehaiKigou = "F"
                            param.Nouba = ""
                        ElseIf shisakuTmp.SiaShukeiCode = "A" Then
                            param.TehaiKigou = ""
                            param.Nouba = "X1"
                        ElseIf shisakuTmp.SiaShukeiCode = "E" Or shisakuTmp.SiaShukeiCode = "Y" Then
                            param.TehaiKigou = "D"
                            param.Nouba = ""
                        ElseIf shisakuTmp.SiaShukeiCode = "R" Or shisakuTmp.SiaShukeiCode = "J" Then
                            param.TehaiKigou = "F"
                            param.Nouba = ""
                        End If

                    End If
                End If
            End If

            If param.Level = 0 Then
                param.TehaiKigou = ""
                param.Nouba = ""
            End If

            param.Henkaten = "1"
            param.UpdatedUserId = LoginInfo.Now.UserId
            param.UpdatedDate = aDate.CurrentDateDbFormat
            param.UpdatedTime = aDate.CurrentTimeDbFormat

            db.Update(sql, param)
        End Sub

        ''' <summary>
        ''' 試作部品表編集情報(TMP)を更新する(比較結果->員数増)
        ''' </summary>
        ''' <param name="BuhinTmp">試作部品表編集情報(TMP)</param>
        ''' <param name="SeihinKbn">製品区分</param>
        ''' <remarks></remarks>
        Public Sub UpdateByBuhinTmpInsuUp(ByVal BuhinTmp As TShisakuBuhinEditVo, ByVal SeihinKbn As String) Implements TehaichoHikakuDao.UpdateByBuhinTmpInsuUp
            Dim sql As String = _
            " UPDATE " & MBOM_DB_NAME & ".dbo.T_SHISAKU_BUHIN_EDIT_TMP " _
            & " SET " _
            & " BUHIN_NO_HYOUJI_JUN = ( " _
            & " SELECT MAX ( CONVERT ( INT,COALESCE ( BUHIN_NO_HYOUJI_JUN,'' ) ) ) AS BUHIN_NO_HYOUJI_JUN " _
            & " FROM " & MBOM_DB_NAME & ".dbo.T_SHISAKU_BUHIN_EDIT_TMP " _
            & " WHERE SHISAKU_EVENT_CODE = " & MBOM_DB_NAME & ".dbo.T_SHISAKU_BUHIN_EDIT_TMP.SHISAKU_EVENT_CODE " _
            & " AND SHISAKU_BUKA_CODE = " & MBOM_DB_NAME & ".dbo.T_SHISAKU_BUHIN_EDIT_TMP.SHISAKU_BUKA_CODE " _
            & " AND SHISAKU_BLOCK_NO = " & MBOM_DB_NAME & ".dbo.T_SHISAKU_BUHIN_EDIT_TMP.SHISAKU_BLOCK_NO) + 1 " _
            & " TEHAI_KIGOU = @TehaiKigou, " _
            & " NOUBA = @Nouba, " _
            & " KYOUKU_SECTION = @KyoukuSection, " _
            & " HENKATEN = @Henkaten " _
            & " UPDATED_USER_ID = @UpdatedUserId, " _
            & " UPDATED_DATE = @UpdatedDate, " _
            & " UPDATED_TIME = @UpdatedTime " _
            & " WHERE SHISAKU_EVENT_CODE = @ShisakuEventCode " _
            & " AND SHISAKU_BUKA_CODE = @ShisakuBukaCode " _
            & " AND SHISAKU_BLOCK_NO = @ShisakuBlockNo " _
            & " AND SHISAKU_BLOCK_NO_KAITEI_NO = @ShisakuBlockNoKaiteiNo " _
            & " AND BUHIN_NO_HYOUJI_JUN = @BuhinNoHyoujiJun "

            Dim db As New EBomDbClient
            Dim aDate As New ShisakuDate
            Dim param As New TShisakuBuhinEditTmpVo

            Dim impl As TehaichoSakuseiDao = New TehaichoSakuseiDaoImpl

            param.ShisakuEventCode = BuhinTmp.ShisakuEventCode
            param.ShisakuBukaCode = BuhinTmp.ShisakuBukaCode
            param.ShisakuBlockNo = BuhinTmp.ShisakuBlockNo
            param.ShisakuBlockNoKaiteiNo = BuhinTmp.ShisakuBlockNoKaiteiNo
            param.BuhinNoHyoujiJun = BuhinTmp.BuhinNoHyoujiJun

            Dim kaihatsuFugo As String = ""
            Dim makerCode As String = ""

            '集計コードがあるかチェック(無ければ海外集計有)'
            If Not StringUtil.IsEmpty(BuhinTmp.ShukeiCode) Then
                If BuhinTmp.ShukeiCode = "X" Then
                    param.TehaiKigou = "F"
                    param.Nouba = ""
                ElseIf BuhinTmp.ShukeiCode = "A" Then
                    param.TehaiKigou = ""
                    param.Nouba = "X1"
                ElseIf BuhinTmp.ShukeiCode = "E" Or BuhinTmp.ShukeiCode = "Y" Then
                    param.TehaiKigou = "D"
                    param.Nouba = ""
                ElseIf BuhinTmp.ShukeiCode = "R" Or BuhinTmp.ShukeiCode = "J" Then
                    param.TehaiKigou = "F"
                    param.Nouba = ""
                End If
            Else

                If StringUtil.IsEmpty(BuhinTmp.MakerCode) Then
                    If BuhinTmp.SiaShukeiCode = "X" Then
                        param.TehaiKigou = "F"
                        param.Nouba = ""
                    ElseIf BuhinTmp.SiaShukeiCode = "A" Then
                        param.TehaiKigou = ""
                        param.Nouba = "X1"
                    ElseIf BuhinTmp.SiaShukeiCode = "E" Or BuhinTmp.SiaShukeiCode = "Y" Then
                        param.TehaiKigou = "D"
                        param.Nouba = ""
                    ElseIf BuhinTmp.SiaShukeiCode = "R" Or BuhinTmp.SiaShukeiCode = "J" Then
                        param.TehaiKigou = "F"
                        param.Nouba = ""
                    End If
                Else
                    If StringUtil.Equals(Left(BuhinTmp.MakerCode, 1), "A") Then
                        '海外集計コード'
                        If BuhinTmp.SiaShukeiCode = "A" Then
                            param.TehaiKigou = "J"
                            param.Nouba = "US"
                        ElseIf BuhinTmp.SiaShukeiCode = "E" Then
                            param.TehaiKigou = "B"
                            param.Nouba = "US"
                        ElseIf BuhinTmp.SiaShukeiCode = "Y" Then
                            param.TehaiKigou = "B"
                            param.Nouba = "US"
                        End If

                    Else
                        If BuhinTmp.SiaShukeiCode = "X" Then
                            param.TehaiKigou = "F"
                            param.Nouba = ""
                        ElseIf BuhinTmp.SiaShukeiCode = "A" Then
                            param.TehaiKigou = ""
                            param.Nouba = "X1"
                        ElseIf BuhinTmp.SiaShukeiCode = "E" Or BuhinTmp.SiaShukeiCode = "Y" Then
                            param.TehaiKigou = "D"
                            param.Nouba = ""
                        ElseIf BuhinTmp.SiaShukeiCode = "R" Or BuhinTmp.SiaShukeiCode = "J" Then
                            param.TehaiKigou = "F"
                            param.Nouba = ""
                        End If
                    End If
                End If

            End If
            If param.Level = 0 Then
                param.TehaiKigou = ""
                param.Nouba = ""
            End If

            param.Henkaten = "1"
            param.UpdatedUserId = LoginInfo.Now.UserId
            param.UpdatedDate = aDate.CurrentDateDbFormat
            param.UpdatedTime = aDate.CurrentTimeDbFormat

            db.Update(sql, param)
        End Sub

        ''' <summary>
        ''' 試作部品表号車編集情報(TMP)を更新する(比較結果->員数増)
        ''' </summary>
        ''' <param name="GousyaTmpVo">部品編集号車情報</param>
        ''' <param name="gousyaNo">号車No</param>
        ''' <remarks></remarks>
        Public Sub UpdateByGousyaTmpInsuUp(ByVal GousyaTmpVo As TShisakuBuhinEditGousyaTmpVo, ByVal gousyaNo As Integer, ByVal insu As Integer) Implements TehaichoHikakuDao.UpdateByGousyaTmpInsuUp

            Dim sql As String = _
            " UPDATE " & MBOM_DB_NAME & ".dbo.T_SHISAKU_BUHIN_EDIT_GOUSYA_TMP  " _
            & " SET " _
            & " INSU_SURYO = @InsuSuryo, " _
            & " UPDATED_USER_ID = @UpdatedUserId, " _
            & " UPDATED_DATE = @UpdatedDate, " _
            & " UPDATED_TIME = @UpdatedTime " _
            & " WHERE SHISAKU_EVENT_CODE = @ShisakuEventCode " _
            & " AND SHISAKU_BUKA_CODE = @ShisakuBukaCode " _
            & " AND SHISAKU_BLOCK_NO = @ShisakuBlockNo " _
            & " AND SHISAKU_BLOCK_NO_KAITEI_NO = @ShisakuBlockNoKaiteiNo " _
            & " AND BUHIN_NO_HYOUJI_JUN = @BuhinNoHyoujiJun " _
            & " AND SHISAKU_GOUSYA_HYOUJI_JUN = @ShisakuGousyaHyoujiJun "

            Dim db As New EBomDbClient
            Dim aDate As New ShisakuDate
            Dim param As New TShisakuBuhinEditGousyaTmpVo

            param.ShisakuEventCode = GousyaTmpVo.ShisakuEventCode
            param.ShisakuBukaCode = GousyaTmpVo.ShisakuBukaCode
            param.ShisakuBlockNo = GousyaTmpVo.ShisakuBlockNo
            param.ShisakuBlockNoKaiteiNo = GousyaTmpVo.ShisakuBlockNoKaiteiNo
            param.InsuSuryo = GousyaTmpVo.InsuSuryo - insu
            param.ShisakuGousyaHyoujiJun = gousyaNo

            param.UpdatedUserId = LoginInfo.Now.UserId
            param.UpdatedDate = aDate.CurrentDateDbFormat
            param.UpdatedTime = aDate.CurrentTimeDbFormat

            db.Update(sql, param)
        End Sub


#End Region

#Region "追加する処理"

        ''' <summary>
        ''' 試作部品表編集情報(TMP)を追加する(比較結果->変更無し)
        ''' </summary>
        ''' <param name="BuhinEditVo">部品編集情報</param>
        ''' <param name="seihinKbn">製品区分</param>
        ''' <param name="shisakuListCode">試作リストコード</param>
        ''' <remarks></remarks>
        Public Sub InsertByBuhinTmpNotChange(ByVal BuhinEditVo As TShisakuBuhinEditVo, ByVal seihinKbn As String, _
                                 ByVal shisakuListCode As String) Implements TehaichoHikakuDao.InsertByBuhinTmpNotChange
            Dim sql As String = _
            " INSERT INTO " & MBOM_DB_NAME & ".dbo.T_SHISAKU_BUHIN_EDIT_TMP (" _
            & " SHISAKU_EVENT_CODE, " _
            & " SHISAKU_BUKA_CODE, " _
            & " SHISAKU_BLOCK_NO, " _
            & " SHISAKU_BLOCK_NO_KAITEI_NO, " _
            & " BUHIN_NO_HYOUJI_JUN, " _
            & " LEVEL, " _
            & " SHUKEI_CODE, " _
            & " SIA_SHUKEI_CODE, " _
            & " GENCYO_CKD_KBN, " _
            & " MAKER_CODE, " _
            & " MAKER_NAME, " _
            & " BUHIN_NO, " _
            & " BUHIN_NO_KBN, " _
            & " BUHIN_NO_KAITEI_NO, " _
            & " EDA_BAN, " _
            & " BUHIN_NAME, " _
            & " SAISHIYOUFUKA, " _
            & " SHUTUZU_YOTEI_DATE, " _
            & " TSUKURIKATA_SEISAKU, " _
            & " TSUKURIKATA_KATASHIYOU_1, " _
            & " TSUKURIKATA_KATASHIYOU_2, " _
            & " TSUKURIKATA_KATASHIYOU_3, " _
            & " TSUKURIKATA_TIGU, " _
            & " TSUKURIKATA_NOUNYU, " _
            & " TSUKURIKATA_KIBO, " _
            & " BASE_BUHIN_FLG, " _
            & " ZAISHITU_KIKAKU_1, " _
            & " ZAISHITU_KIKAKU_2, " _
            & " ZAISHITU_KIKAKU_3, " _
            & " ZAISHITU_MEKKI, " _
            & " SHISAKU_BANKO_SURYO, " _
            & " SHISAKU_BANKO_SURYO_U, " _
            & " MATERIAL_INFO_LENGTH, " _
            & " MATERIAL_INFO_WIDTH, " _
            & " DATA_ITEM_KAITEI_NO, " _
            & " DATA_ITEM_AREA_NAME, " _
            & " DATA_ITEM_SET_NAME, " _
            & " DATA_ITEM_KAITEI_INFO, " _
            & " SHISAKU_BUHIN_HI, " _
            & " SHISAKU_KATA_HI, " _
            & " BIKOU, " _
            & " EDIT_TOUROKUBI, " _
            & " EDIT_TOUROKUJIKAN, " _
            & " KAITEI_HANDAN_FLG, " _
            & " TEHAI_KIGOU, " _
            & " NOUBA, " _
            & " KYOUKU_SECTION, " _
            & " SENYOU_MARK, " _
            & " KOUTAN, " _
            & " STSR_DHSTBA, " _
            & " HENKATEN, " _
            & " SHISAKU_SEIHIN_KBN, " _
            & " SHISAKU_LIST_CODE, " _
            & " CREATED_USER_ID, " _
            & " CREATED_DATE, " _
            & " CREATED_TIME, " _
            & " UPDATED_USER_ID, " _
            & " UPDATED_DATE, " _
            & " UPDATED_TIME " _
            & " ) " _
            & " VALUES ( " _
            & " @ShisakuEventCode, " _
            & " @ShisakuBukaCode, " _
            & " @ShisakuBlockNo, " _
            & " @ShisakuBlockNoKaiteiNo, " _
            & " @BuhinNoHyoujiJun, " _
            & " @Level, " _
            & " @ShukeiCode, " _
            & " @SiaShukeiCode, " _
            & " @GencyoCkdKbn, " _
            & " @MakerCode, " _
            & " @MakerName, " _
            & " @BuhinNo, " _
            & " @BuhinNoKbn, " _
            & " @BuhinNoKaiteiNo, " _
            & " @EdaBan, " _
            & " @BuhinName, " _
            & " @Saishiyoufuka, " _
            & " @ShutuzuYoteiDate, " _
            & " @TsukurikataSeisaku, " _
            & " @TsukurikataKatashiyou1, " _
            & " @TsukurikataKatashiyou2, " _
            & " @TsukurikataKatashiyou3, " _
            & " @TsukurikataTigu, " _
            & " @TsukurikataNounyu, " _
            & " @TsukurikataKibo, " _
            & " @BaseBuhinFlg, " _
            & " @ZaishituKikaku1, " _
            & " @ZaishituKikaku2, " _
            & " @ZaishituKikaku3, " _
            & " @ZaishituMekki, " _
            & " @ShisakuBankoSuryo, " _
            & " @ShisakuBankoSuryoU, " _
            & " @MaterialInfoLength, " _
            & " @MaterialInfoWidth, " _
            & " @DataItemKaiteiNo, " _
            & " @DataItemAreaName, " _
            & " @DataItemSetName, " _
            & " @DataItemKaiteiInfo, " _
            & " @ShisakuBuhinHi, " _
            & " @ShisakuKataHi, " _
            & " @Bikou, " _
            & " @EditTourokubi, " _
            & " @EditTourokujikan, " _
            & " @KaiteiHandanFlg, " _
            & " @TehaiKigou, " _
            & " @Nouba, " _
            & " @KyoukuSection, " _
            & " @SenyouMark, " _
            & " @Koutan, " _
            & " @StsrDhstba, " _
            & " @Henkaten, " _
            & " @ShisakuSeihinKbn, " _
            & " @ShisakuListCode, " _
            & " @CreatedUserId, " _
            & " @CreatedDate, " _
            & " @CreatedTime, " _
            & " @UpdatedUserId, " _
            & " @UpdatedDate, " _
            & " @UpdatedTime " _
            & " ) "
            ''↑↑2014/08/26 Ⅰ.2.管理項目追加 酒井 ADD

            Dim db As New EBomDbClient
            Dim aDate As New ShisakuDate
            Dim KoutanTorihikisaki As New TShisakuBuhinEditTmpVo
            Dim param As New TShisakuBuhinEditTmpVo
            Dim paramImpl As TehaichoSakuseiDao = New TehaichoSakuseiDaoImpl

            param.ShisakuEventCode = BuhinEditVo.ShisakuEventCode
            param.ShisakuBukaCode = BuhinEditVo.ShisakuBukaCode
            param.ShisakuBlockNo = BuhinEditVo.ShisakuBlockNo
            param.ShisakuBlockNoKaiteiNo = BuhinEditVo.ShisakuBlockNoKaiteiNo
            param.BuhinNoHyoujiJun = FindByNewBuhinNoHyoujiJun(BuhinEditVo.ShisakuEventCode, BuhinEditVo.ShisakuBukaCode, BuhinEditVo.ShisakuBlockNo)
            param.Level = BuhinEditVo.Level
            param.ShukeiCode = BuhinEditVo.ShukeiCode
            param.SiaShukeiCode = BuhinEditVo.SiaShukeiCode
            param.GencyoCkdKbn = BuhinEditVo.GencyoCkdKbn
            param.MakerCode = BuhinEditVo.MakerCode
            param.MakerName = BuhinEditVo.MakerName
            param.BuhinNo = BuhinEditVo.BuhinNo
            param.BuhinNoKbn = BuhinEditVo.BuhinNoKbn
            param.BuhinNoKaiteiNo = BuhinEditVo.BuhinNoKaiteiNo
            param.EdaBan = BuhinEditVo.EdaBan
            param.BuhinName = BuhinEditVo.BuhinName
            param.Saishiyoufuka = BuhinEditVo.Saishiyoufuka
            param.ShutuzuYoteiDate = BuhinEditVo.ShutuzuYoteiDate

            ''↓↓2014/08/26 Ⅰ.2.管理項目追加 酒井 ADD BEGIN
            param.TsukurikataSeisaku = BuhinEditVo.TsukurikataSeisaku
            param.TsukurikataKatashiyou1 = BuhinEditVo.TsukurikataKatashiyou1
            param.TsukurikataKatashiyou2 = BuhinEditVo.TsukurikataKatashiyou2
            param.TsukurikataKatashiyou3 = BuhinEditVo.TsukurikataKatashiyou3
            param.TsukurikataTigu = BuhinEditVo.TsukurikataTigu
            param.TsukurikataNounyu = BuhinEditVo.TsukurikataNounyu
            param.TsukurikataKibo = BuhinEditVo.TsukurikataKibo
            ''↑↑2014/08/26 Ⅰ.2.管理項目追加 酒井 ADD END
            '↓↓2014/09/26 酒井 ADD BEGIN
            param.BaseBuhinFlg = BuhinEditVo.BaseBuhinFlg

            '↑↑2014/09/26 酒井 ADD END
            param.ZaishituKikaku1 = BuhinEditVo.ZaishituKikaku1
            param.ZaishituKikaku2 = BuhinEditVo.ZaishituKikaku2
            param.ZaishituKikaku3 = BuhinEditVo.ZaishituKikaku3
            param.ZaishituMekki = BuhinEditVo.ZaishituMekki
            param.ShisakuBankoSuryo = BuhinEditVo.ShisakuBankoSuryo
            param.ShisakuBankoSuryoU = BuhinEditVo.ShisakuBankoSuryoU


            ''↓↓2014/12/29 メタル対応追加フィールド (DANIEL)柳沼 ADD BEGIN
            param.MaterialInfoLength = BuhinEditVo.MaterialInfoLength
            param.MaterialInfoWidth = BuhinEditVo.MaterialInfoWidth
            param.DataItemKaiteiNo = BuhinEditVo.DataItemKaiteiNo
            param.DataItemAreaName = BuhinEditVo.DataItemAreaName
            param.DataItemSetName = BuhinEditVo.DataItemSetName
            param.DataItemKaiteiInfo = BuhinEditVo.DataItemKaiteiInfo
            ''↑↑2014/12/29 メタル対応追加フィールド (DANIEL)柳沼 ADD END


            param.ShisakuBuhinHi = BuhinEditVo.ShisakuBuhinHi
            param.ShisakuKataHi = BuhinEditVo.ShisakuKataHi

            '不正文字(')が入ってくることがあるので
            '   半角スペースに置き換える。
            If StringUtil.IsNotEmpty(BuhinEditVo.Bikou) Then
                BuhinEditVo.Bikou = BuhinEditVo.Bikou.Replace("'", " ")
            End If
            param.Bikou = BuhinEditVo.Bikou

            param.EditTourokubi = BuhinEditVo.EditTourokubi
            param.EditTourokujikan = BuhinEditVo.EditTourokujikan
            param.KaiteiHandanFlg = BuhinEditVo.KaiteiHandanFlg
            param.TehaiKigou = ""
            param.Nouba = ""

            param.KyoukuSection = ""

            If Not paramImpl.FindBySenyouCheck(BuhinEditVo.BuhinNo, seihinKbn) Then
                param.SenyouMark = "*"
            Else
                param.SenyouMark = ""
            End If

            KoutanTorihikisaki = paramImpl.FindByKoutanTorihikisaki(BuhinEditVo.BuhinNo)
            If StringUtil.IsEmpty(param.MakerCode) Then
                param.MakerCode = KoutanTorihikisaki.MakerCode
            End If
            param.Koutan = KoutanTorihikisaki.Koutan

            param.StsrDhstba = paramImpl.FindByZumenNo(BuhinEditVo.BuhinNo)

            param.Henkaten = ""
            param.ShisakuSeihinKbn = seihinKbn
            param.ShisakuListCode = shisakuListCode

            param.CreatedUserId = LoginInfo.Now.UserId
            param.CreatedDate = aDate.CurrentDateDbFormat
            param.CreatedTime = aDate.CurrentTimeDbFormat
            param.UpdatedUserId = LoginInfo.Now.UserId
            param.UpdatedDate = aDate.CurrentDateDbFormat
            param.UpdatedTime = aDate.CurrentTimeDbFormat

            db.Insert(sql, param)
        End Sub

        ''' <summary>
        ''' 部品表編集号車情報テンポラリ情報を追加する(比較結果織込み用)
        ''' </summary>
        ''' <param name="gousyaTmpvo">部品表編集号車情報VO</param>
        ''' <remarks></remarks>
        Public Sub InsertByGousyaTMPHikakuKekka(ByVal gousyaTmpvo As TShisakuBuhinEditGousyaTmpVo, _
                                                 ByVal gousyaHyoujijun As Integer) Implements TehaichoHikakuDao.InsertByGousyaTMPHikakuKekka
            Dim sql As String = _
            " INSERT INTO " & MBOM_DB_NAME & ".dbo.T_SHISAKU_BUHIN_EDIT_GOUSYA_TMP ( " _
            & " SHISAKU_EVENT_CODE, " _
            & " SHISAKU_BUKA_CODE, " _
            & " SHISAKU_BLOCK_NO, " _
            & " SHISAKU_BLOCK_NO_KAITEI_NO, " _
            & " BUHIN_NO_HYOUJI_JUN, " _
            & " SHISAKU_GOUSYA_HYOUJI_JUN, " _
            & " SHISAKU_GOUSYA, " _
            & " INSU_SURYO, " _
            & " CREATED_USER_ID, " _
            & " CREATED_DATE, " _
            & " CREATED_TIME, " _
            & " UPDATED_USER_ID, " _
            & " UPDATED_DATE, " _
            & " UPDATED_TIME " _
            & " ) " _
            & " VALUES ( " _
            & " @ShisakuEventCode, " _
            & " @ShisakuBukaCode, " _
            & " @ShisakuBlockNo, " _
            & " @ShisakuBlockNoKaiteiNo, " _
            & " @BuhinNoHyoujiJun, " _
            & " @ShisakuGousyaHyoujiJun, " _
            & " @ShisakuGousya, " _
            & " @InsuSuryo, " _
            & " @CreatedUserId, " _
            & " @CreatedDate, " _
            & " @CreatedTime, " _
            & " @UpdatedUserId, " _
            & " @UpdatedDate, " _
            & " @UpdatedTime " _
            & " ) "

            Dim sspl As String = _
            " SELECT * " _
            & " FROM " & MBOM_DB_NAME & ".dbo.T_SHISAKU_BUHIN_EDIT_GOUSYA_TMP " _
            & " WHERE " _
            & " SHISAKU_EVENT_CODE = @ShisakuEventCode " _
            & " AND SHISAKU_BUKA_CODE = @ShisakuBukaCode " _
            & " AND SHISAKU_BLOCK_NO = @ShisakuBlockNo " _
            & " AND SHISAKU_BLOCK_NO_KAITEI_NO = @ShisakuBlockNoKaiteiNo " _
            & " AND BUHIN_NO_HYOUJI_JUN = @BuhinNoHyoujiJun " _
            & " AND SHISAKU_GOUSYA_HYOUJI_JUN = @ShisakuGousyaHyoujiJun "

            Dim impl As TehaichoSakuseiDao = New TehaichoSakuseiDaoImpl
            Dim db As New EBomDbClient
            Dim aDate As New ShisakuDate
            Dim param As New TShisakuBuhinEditGousyaTmpVo

            param.ShisakuEventCode = gousyaTmpvo.ShisakuEventCode
            param.ShisakuBukaCode = gousyaTmpvo.ShisakuBukaCode
            param.ShisakuBlockNo = gousyaTmpvo.ShisakuBlockNo
            param.ShisakuBlockNoKaiteiNo = gousyaTmpvo.ShisakuBlockNoKaiteiNo
            param.BuhinNoHyoujiJun = FindByNewGousyaBuhinNoHyoujiJun(gousyaTmpvo.ShisakuEventCode, gousyaTmpvo.ShisakuBukaCode, gousyaTmpvo.ShisakuBlockNo)
            param.ShisakuGousyaHyoujiJun = gousyaHyoujijun
            param.ShisakuGousya = gousyaTmpvo.ShisakuGousya
            param.InsuSuryo = gousyaTmpvo.InsuSuryo

            Dim Vo As New TShisakuBuhinEditGousyaTmpVo

            Vo = db.QueryForObject(Of TShisakuBuhinEditGousyaTmpVo)(sspl, param)

            '存在しているならばインサートするとプライマリキー違反が発生する'
            '親品番の存在は取得していないので'
            If Not Vo Is Nothing Then
                param.ShisakuGousya = gousyaTmpvo.ShisakuGousya
                param.InsuSuryo = gousyaTmpvo.InsuSuryo

                param.CreatedUserId = LoginInfo.Now.UserId
                param.CreatedDate = aDate.CurrentDateDbFormat
                param.CreatedTime = aDate.CurrentTimeDbFormat
                param.UpdatedUserId = LoginInfo.Now.UserId
                param.UpdatedDate = aDate.CurrentDateDbFormat
                param.UpdatedTime = aDate.CurrentTimeDbFormat

                If Vo.InsuSuryo < 0 Then
                    param.InsuSuryo = -1
                    impl.UpdateByBuhinEditGousyaTmp(param)
                    Return
                Else
                    If param.InsuSuryo < 0 Then
                        param.InsuSuryo = -1
                        impl.UpdateByBuhinEditGousyaTmp(param)
                        Return
                    Else
                        Dim NewInsuSuryo As Integer
                        NewInsuSuryo = gousyaTmpvo.InsuSuryo + Vo.InsuSuryo
                        param.InsuSuryo = NewInsuSuryo
                        impl.UpdateByBuhinEditGousyaTmp(param)
                        Return
                    End If
                End If
            End If


            param.CreatedUserId = LoginInfo.Now.UserId
            param.CreatedDate = aDate.CurrentDateDbFormat
            param.CreatedTime = aDate.CurrentTimeDbFormat
            param.UpdatedUserId = LoginInfo.Now.UserId
            param.UpdatedDate = aDate.CurrentDateDbFormat
            param.UpdatedTime = aDate.CurrentTimeDbFormat

            db.Insert(sql, param)

        End Sub

        ''' <summary>
        ''' 試作部品表編集情報(TMP)を追加する(比較結果->追加品番)
        ''' </summary>
        ''' <param name="BuhinEditVo">部品編集情報</param>
        ''' <param name="seihinKbn">製品区分</param>
        ''' <param name="shisakuListCode">試作リストコード</param>
        ''' <remarks></remarks>
        Public Sub InsertByBuhinTmpTsuikaHinban(ByVal BuhinEditvo As TShisakuBuhinEditVo, ByVal seihinKbn As String, _
                                 ByVal shisakuListCode As String) Implements TehaichoHikakuDao.InsertByBuhinTmpTsuikaHinban
            Dim sql As String = _
            " INSERT INTO " & MBOM_DB_NAME & ".dbo.T_SHISAKU_BUHIN_EDIT_TMP (" _
            & " SHISAKU_EVENT_CODE, " _
            & " SHISAKU_BUKA_CODE, " _
            & " SHISAKU_BLOCK_NO, " _
            & " SHISAKU_BLOCK_NO_KAITEI_NO, " _
            & " BUHIN_NO_HYOUJI_JUN, " _
            & " LEVEL, " _
            & " SHUKEI_CODE, " _
            & " SIA_SHUKEI_CODE, " _
            & " GENCYO_CKD_KBN, " _
            & " MAKER_CODE, " _
            & " MAKER_NAME, " _
            & " BUHIN_NO, " _
            & " BUHIN_NO_KBN, " _
            & " BUHIN_NO_KAITEI_NO, " _
            & " EDA_BAN, " _
            & " BUHIN_NAME, " _
            & " SAISHIYOUFUKA, " _
            & " SHUTUZU_YOTEI_DATE, " _
            & " TSUKURIKATA_SEISAKU, " _
            & " TSUKURIKATA_KATASHIYOU_1, " _
            & " TSUKURIKATA_KATASHIYOU_2, " _
            & " TSUKURIKATA_KATASHIYOU_3, " _
            & " TSUKURIKATA_TIGU, " _
            & " TSUKURIKATA_NOUNYU, " _
            & " TSUKURIKATA_KIBO, " _
            & " BASE_BUHIN_FLG, " _
            & " ZAISHITU_KIKAKU_1, " _
            & " ZAISHITU_KIKAKU_2, " _
            & " ZAISHITU_KIKAKU_3, " _
            & " ZAISHITU_MEKKI, " _
            & " SHISAKU_BANKO_SURYO, " _
            & " SHISAKU_BANKO_SURYO_U, " _
            & " MATERIAL_INFO_LENGTH, " _
            & " MATERIAL_INFO_WIDTH, " _
            & " DATA_ITEM_KAITEI_NO, " _
            & " DATA_ITEM_AREA_NAME, " _
            & " DATA_ITEM_SET_NAME, " _
            & " DATA_ITEM_KAITEI_INFO, " _
            & " SHISAKU_BUHIN_HI, " _
            & " SHISAKU_KATA_HI, " _
            & " BIKOU, " _
            & " EDIT_TOUROKUBI, " _
            & " EDIT_TOUROKUJIKAN, " _
            & " KAITEI_HANDAN_FLG, " _
            & " TEHAI_KIGOU, " _
            & " NOUBA, " _
            & " KYOUKU_SECTION, " _
            & " SENYOU_MARK, " _
            & " KOUTAN, " _
            & " STSR_DHSTBA, " _
            & " HENKATEN, " _
            & " SHISAKU_SEIHIN_KBN, " _
            & " SHISAKU_LIST_CODE, " _
            & " CREATED_USER_ID, " _
            & " CREATED_DATE, " _
            & " CREATED_TIME, " _
            & " UPDATED_USER_ID, " _
            & " UPDATED_DATE, " _
            & " UPDATED_TIME " _
            & " ) " _
            & " VALUES ( " _
            & " @ShisakuEventCode, " _
            & " @ShisakuBukaCode, " _
            & " @ShisakuBlockNo, " _
            & " @ShisakuBlockNoKaiteiNo, " _
            & " @BuhinNoHyoujiJun, " _
            & " @Level, " _
            & " @ShukeiCode, " _
            & " @SiaShukeiCode, " _
            & " @GencyoCkdKbn, " _
            & " @MakerCode, " _
            & " @MakerName, " _
            & " @BuhinNo, " _
            & " @BuhinNoKbn, " _
            & " @BuhinNoKaiteiNo, " _
            & " @EdaBan, " _
            & " @BuhinName, " _
            & " @Saishiyoufuka, " _
            & " @ShutuzuYoteiDate, " _
            & " @TsukurikataSeisaku, " _
            & " @TsukurikataKatashiyou1, " _
            & " @TsukurikataKatashiyou2, " _
            & " @TsukurikataKatashiyou3, " _
            & " @TsukurikataTigu, " _
            & " @TsukurikataNounyu, " _
            & " @TsukurikataKibo, " _
            & " @BaseBuhinFlg, " _
            & " @ZaishituKikaku1, " _
            & " @ZaishituKikaku2, " _
            & " @ZaishituKikaku3, " _
            & " @ZaishituMekki, " _
            & " @ShisakuBankoSuryo, " _
            & " @ShisakuBankoSuryoU, " _
            & " @MaterialInfoLength, " _
            & " @MaterialInfoWidth, " _
            & " @DataItemKaiteiNo, " _
            & " @DataItemAreaName, " _
            & " @DataItemSetName, " _
            & " @DataItemKaiteiInfo, " _
            & " @ShisakuBuhinHi, " _
            & " @ShisakuKataHi, " _
            & " @Bikou, " _
            & " @EditTourokubi, " _
            & " @EditTourokujikan, " _
            & " @KaiteiHandanFlg, " _
            & " @TehaiKigou, " _
            & " @Nouba, " _
            & " @KyoukuSection, " _
            & " @SenyouMark, " _
            & " @Koutan, " _
            & " @StsrDhstba, " _
            & " @Henkaten, " _
            & " @ShisakuSeihinKbn, " _
            & " @ShisakuListCode, " _
            & " @CreatedUserId, " _
            & " @CreatedDate, " _
            & " @CreatedTime, " _
            & " @UpdatedUserId, " _
            & " @UpdatedDate, " _
            & " @UpdatedTime " _
            & " ) "
            ''↑↑2014/08/26 Ⅰ.2.管理項目追加 酒井 ADD

            Dim db As New EBomDbClient
            Dim aDate As New ShisakuDate
            Dim aRhac0532vo As New Rhac0532Vo
            Dim param As New TShisakuBuhinEditTmpVo
            Dim impl As TehaichoSakuseiDao = New TehaichoSakuseiDaoImpl
            Dim eventVo As New TShisakuEventVo
            Dim buhinEditTmpVo As New TShisakuBuhinEditTmpVo

            eventVo = impl.FindByEventName(BuhinEditvo.ShisakuEventCode)

            param.ShisakuEventCode = BuhinEditvo.ShisakuEventCode
            param.ShisakuBukaCode = BuhinEditvo.ShisakuBukaCode
            param.ShisakuBlockNo = BuhinEditvo.ShisakuBlockNo
            param.ShisakuBlockNoKaiteiNo = BuhinEditvo.ShisakuBlockNoKaiteiNo
            param.BuhinNoHyoujiJun = FindByNewBuhinNoHyoujiJun(BuhinEditvo.ShisakuEventCode, BuhinEditvo.ShisakuBukaCode, BuhinEditvo.ShisakuBlockNo)
            If param.BuhinNoHyoujiJun = -1 Then
                Return
            End If
            param.Level = BuhinEditvo.Level
            param.ShukeiCode = BuhinEditvo.ShukeiCode
            param.SiaShukeiCode = BuhinEditvo.SiaShukeiCode
            param.GencyoCkdKbn = BuhinEditvo.GencyoCkdKbn
            param.MakerCode = BuhinEditvo.MakerCode
            param.MakerName = BuhinEditvo.MakerName
            param.BuhinNo = BuhinEditvo.BuhinNo
            param.BuhinNoKbn = BuhinEditvo.BuhinNoKbn
            param.BuhinNoKaiteiNo = BuhinEditvo.BuhinNoKaiteiNo
            param.EdaBan = BuhinEditvo.EdaBan
            param.BuhinName = BuhinEditvo.BuhinName
            param.Saishiyoufuka = BuhinEditvo.Saishiyoufuka
            param.ShutuzuYoteiDate = BuhinEditvo.ShutuzuYoteiDate
            ''↓↓2014/08/26 Ⅰ.2.管理項目追加 酒井 ADD BEGIN
            param.TsukurikataSeisaku = BuhinEditvo.TsukurikataSeisaku
            param.TsukurikataKatashiyou1 = BuhinEditvo.TsukurikataKatashiyou1
            param.TsukurikataKatashiyou2 = BuhinEditvo.TsukurikataKatashiyou2
            param.TsukurikataKatashiyou3 = BuhinEditvo.TsukurikataKatashiyou3
            param.TsukurikataTigu = BuhinEditvo.TsukurikataTigu
            param.TsukurikataNounyu = BuhinEditvo.TsukurikataNounyu
            param.TsukurikataKibo = BuhinEditvo.TsukurikataKibo
            ''↑↑2014/08/26 Ⅰ.2.管理項目追加 酒井 ADD END
            '↓↓2014/09/26 酒井 ADD BEGIN
            param.BaseBuhinFlg = BuhinEditvo.BaseBuhinFlg

            '↑↑2014/09/26 酒井 ADD END
            param.ZaishituKikaku1 = BuhinEditvo.ZaishituKikaku1
            param.ZaishituKikaku2 = BuhinEditvo.ZaishituKikaku2
            param.ZaishituKikaku3 = BuhinEditvo.ZaishituKikaku3
            param.ZaishituMekki = BuhinEditvo.ZaishituMekki
            param.ShisakuBankoSuryo = BuhinEditvo.ShisakuBankoSuryo
            param.ShisakuBankoSuryoU = BuhinEditvo.ShisakuBankoSuryoU


            ''↓↓2014/12/29 メタル対応追加フィールド (DANIEL)柳沼 ADD BEGIN
            param.MaterialInfoLength = BuhinEditvo.MaterialInfoLength
            param.MaterialInfoWidth = BuhinEditvo.MaterialInfoWidth
            param.DataItemKaiteiNo = BuhinEditvo.DataItemKaiteiNo
            param.DataItemAreaName = BuhinEditvo.DataItemAreaName
            param.DataItemSetName = BuhinEditvo.DataItemSetName
            param.DataItemKaiteiInfo = BuhinEditvo.DataItemKaiteiInfo
            ''↑↑2014/12/29 メタル対応追加フィールド (DANIEL)柳沼 ADD END


            param.ShisakuBuhinHi = BuhinEditvo.ShisakuBuhinHi
            param.ShisakuKataHi = BuhinEditvo.ShisakuKataHi

            '不正文字(')が入ってくることがあるので
            '   半角スペースに置き換える。
            If StringUtil.IsNotEmpty(BuhinEditvo.Bikou) Then
                BuhinEditvo.Bikou = BuhinEditvo.Bikou.Replace("'", " ")
            End If
            param.Bikou = BuhinEditvo.Bikou

            param.EditTourokubi = BuhinEditvo.EditTourokubi
            param.EditTourokujikan = BuhinEditvo.EditTourokujikan
            param.KaiteiHandanFlg = BuhinEditvo.KaiteiHandanFlg
            param.KyoukuSection = BuhinEditvo.KyoukuSection

            Dim kaihatsuFugo As String = ""
            Dim makerCode As String = ""


            '集計コードがあるかチェック(無ければ海外集計有)'
            If Not StringUtil.IsEmpty(BuhinEditvo.ShukeiCode) Then
                If StringUtil.Equals(BuhinEditvo.ShukeiCode, "X") Then
                    param.TehaiKigou = "F"
                    param.Nouba = ""
                    'param.KyoukuSection = ""
                ElseIf StringUtil.Equals(BuhinEditvo.ShukeiCode, "A") Then
                    param.TehaiKigou = ""
                    param.Nouba = "X1"
                    'param.KyoukuSection = "9SH10"
                ElseIf StringUtil.Equals(BuhinEditvo.ShukeiCode, "E") Or StringUtil.Equals(BuhinEditvo.ShukeiCode, "Y") Then
                    '専用品チェック'
                    If impl.FindBySenyouCheck(BuhinEditvo.BuhinNo, seihinKbn) Then
                        '共用品なら'
                        param.TehaiKigou = "A"
                        param.Nouba = "A0"
                    Else
                        '専用品なら'
                        param.TehaiKigou = "D"
                        param.Nouba = "X1"
                    End If

                ElseIf StringUtil.Equals(BuhinEditvo.ShukeiCode, "R") Or StringUtil.Equals(BuhinEditvo.ShukeiCode, "J") Then
                    param.TehaiKigou = "F"
                    param.Nouba = ""
                End If
            Else

                If StringUtil.IsEmpty(BuhinEditvo.MakerCode) Then
                    If BuhinEditvo.SiaShukeiCode = "X" Then
                        param.TehaiKigou = "F"
                        param.Nouba = ""
                    ElseIf BuhinEditvo.SiaShukeiCode = "A" Then
                        param.TehaiKigou = ""
                        param.Nouba = "X1"
                    ElseIf BuhinEditvo.SiaShukeiCode = "E" Or BuhinEditvo.SiaShukeiCode = "Y" Then
                        param.TehaiKigou = "D"
                        param.Nouba = ""
                    ElseIf BuhinEditvo.SiaShukeiCode = "R" Or BuhinEditvo.SiaShukeiCode = "J" Then
                        param.TehaiKigou = "F"
                        param.Nouba = ""
                    End If
                Else
                    If StringUtil.Equals(Left(BuhinEditvo.MakerCode, 1), "A") Then
                        '海外集計コード'
                        If StringUtil.Equals(BuhinEditvo.SiaShukeiCode, "A") Then
                            param.TehaiKigou = "J"
                            param.Nouba = "US"
                        ElseIf StringUtil.Equals(BuhinEditvo.SiaShukeiCode, "E") Then
                            param.TehaiKigou = "B"
                            param.Nouba = "US"
                        ElseIf StringUtil.Equals(BuhinEditvo.SiaShukeiCode, "Y") Then
                            param.TehaiKigou = "B"
                            param.Nouba = "US"
                        End If
                    Else
                        '頭がAじゃない場合国内と同じ'
                        If BuhinEditvo.SiaShukeiCode = "X" Then
                            param.TehaiKigou = "F"
                            param.Nouba = ""
                        ElseIf BuhinEditvo.SiaShukeiCode = "A" Then
                            param.TehaiKigou = ""
                            param.Nouba = "X1"
                        ElseIf BuhinEditvo.SiaShukeiCode = "E" Or BuhinEditvo.SiaShukeiCode = "Y" Then
                            param.TehaiKigou = "D"
                            param.Nouba = ""
                        ElseIf BuhinEditvo.SiaShukeiCode = "R" Or BuhinEditvo.SiaShukeiCode = "J" Then
                            param.TehaiKigou = "F"
                            param.Nouba = ""
                        End If
                    End If
                End If
            End If

            If BuhinEditvo.Level = 0 Then
                param.TehaiKigou = "F"
                param.Nouba = ""
            End If

            param.Henkaten = "1"

            param.ShisakuSeihinKbn = seihinKbn
            param.ShisakuListCode = shisakuListCode

            param.CreatedUserId = LoginInfo.Now.UserId
            param.CreatedDate = aDate.CurrentDateDbFormat
            param.CreatedTime = aDate.CurrentTimeDbFormat
            param.UpdatedUserId = LoginInfo.Now.UserId
            param.UpdatedDate = aDate.CurrentDateDbFormat
            param.UpdatedTime = aDate.CurrentTimeDbFormat

            db.Insert(sql, param)
        End Sub

        ''' <summary>
        ''' 試作部品表編集情報(TMP)を追加する(比較結果->員数増)
        ''' </summary>
        ''' <param name="BuhinEditVo">部品編集情報</param>
        ''' <param name="seihinKbn">製品区分</param>
        ''' <param name="shisakuListCode">試作リストコード</param>
        ''' <remarks></remarks>
        Public Sub InsertByBuhinTmpInsuUp(ByVal BuhinEditvo As TShisakuBuhinEditVo, ByVal seihinKbn As String, _
                                 ByVal shisakuListCode As String) Implements TehaichoHikakuDao.InsertByBuhinTmpInsuUp
            Dim sql As String = _
            " INSERT INTO " & MBOM_DB_NAME & ".dbo.T_SHISAKU_BUHIN_EDIT_TMP (" _
            & " SHISAKU_EVENT_CODE, " _
            & " SHISAKU_BUKA_CODE, " _
            & " SHISAKU_BLOCK_NO, " _
            & " SHISAKU_BLOCK_NO_KAITEI_NO, " _
            & " BUHIN_NO_HYOUJI_JUN, " _
            & " LEVEL, " _
            & " SHUKEI_CODE, " _
            & " SIA_SHUKEI_CODE, " _
            & " GENCYO_CKD_KBN, " _
            & " MAKER_CODE, " _
            & " MAKER_NAME, " _
            & " BUHIN_NO, " _
            & " BUHIN_NO_KBN, " _
            & " BUHIN_NO_KAITEI_NO, " _
            & " EDA_BAN, " _
            & " BUHIN_NAME, " _
            & " SAISHIYOUFUKA, " _
            & " SHUTUZU_YOTEI_DATE, " _
            & " TSUKURIKATA_SEISAKU, " _
            & " TSUKURIKATA_KATASHIYOU_1, " _
            & " TSUKURIKATA_KATASHIYOU_2, " _
            & " TSUKURIKATA_KATASHIYOU_3, " _
            & " TSUKURIKATA_TIGU, " _
            & " TSUKURIKATA_NOUNYU, " _
            & " TSUKURIKATA_KIBO, " _
            & " BASE_BUHIN_FLG, " _
            & " ZAISHITU_KIKAKU_1, " _
            & " ZAISHITU_KIKAKU_2, " _
            & " ZAISHITU_KIKAKU_3, " _
            & " ZAISHITU_MEKKI, " _
            & " SHISAKU_BANKO_SURYO, " _
            & " SHISAKU_BANKO_SURYO_U, " _
            & " MATERIAL_INFO_LENGTH, " _
            & " MATERIAL_INFO_WIDTH, " _
            & " DATA_ITEM_KAITEI_NO, " _
            & " DATA_ITEM_AREA_NAME, " _
            & " DATA_ITEM_SET_NAME, " _
            & " DATA_ITEM_KAITEI_INFO, " _
            & " SHISAKU_BUHIN_HI, " _
            & " SHISAKU_KATA_HI, " _
            & " BIKOU, " _
            & " EDIT_TOUROKUBI, " _
            & " EDIT_TOUROKUJIKAN, " _
            & " KAITEI_HANDAN_FLG, " _
            & " TEHAI_KIGOU, " _
            & " NOUBA, " _
            & " KYOUKU_SECTION, " _
            & " SENYOU_MARK, " _
            & " KOUTAN, " _
            & " STSR_DHSTBA, " _
            & " HENKATEN, " _
            & " SHISAKU_SEIHIN_KBN, " _
            & " SHISAKU_LIST_CODE, " _
            & " CREATED_USER_ID, " _
            & " CREATED_DATE, " _
            & " CREATED_TIME, " _
            & " UPDATED_USER_ID, " _
            & " UPDATED_DATE, " _
            & " UPDATED_TIME " _
            & " ) " _
            & " VALUES ( " _
            & " @ShisakuEventCode, " _
            & " @ShisakuBukaCode, " _
            & " @ShisakuBlockNo, " _
            & " @ShisakuBlockNoKaiteiNo, " _
            & " @BuhinNoHyoujiJun, " _
            & " @Level, " _
            & " @ShukeiCode, " _
            & " @SiaShukeiCode, " _
            & " @GencyoCkdKbn, " _
            & " @MakerCode, " _
            & " @MakerName, " _
            & " @BuhinNo, " _
            & " @BuhinNoKbn, " _
            & " @BuhinNoKaiteiNo, " _
            & " @EdaBan, " _
            & " @BuhinName, " _
            & " @Saishiyoufuka, " _
            & " @ShutuzuYoteiDate, " _
            & " @TsukurikataSeisaku, " _
            & " @TsukurikataKatashiyou1, " _
            & " @TsukurikataKatashiyou2, " _
            & " @TsukurikataKatashiyou3, " _
            & " @TsukurikataTigu, " _
            & " @TsukurikataNounyu, " _
            & " @TsukurikataKibo, " _
            & " @BaseBuhinFlg, " _
            & " @ZaishituKikaku1, " _
            & " @ZaishituKikaku2, " _
            & " @ZaishituKikaku3, " _
            & " @ZaishituMekki, " _
            & " @ShisakuBankoSuryo, " _
            & " @ShisakuBankoSuryoU, " _
            & " @MaterialInfoLength, " _
            & " @MaterialInfoWidth, " _
            & " @DataItemKaiteiNo, " _
            & " @DataItemAreaName, " _
            & " @DataItemSetName, " _
            & " @DataItemKaiteiInfo, " _
            & " @ShisakuBuhinHi, " _
            & " @ShisakuKataHi, " _
            & " @Bikou, " _
            & " @EditTourokubi, " _
            & " @EditTourokujikan, " _
            & " @KaiteiHandanFlg, " _
            & " @TehaiKigou, " _
            & " @Nouba, " _
            & " @KyoukuSection, " _
            & " @SenyouMark, " _
            & " @Koutan, " _
            & " @StsrDhstba, " _
            & " @Henkaten, " _
            & " @ShisakuSeihinKbn, " _
            & " @ShisakuListCode, " _
            & " @CreatedUserId, " _
            & " @CreatedDate, " _
            & " @CreatedTime, " _
            & " @UpdatedUserId, " _
            & " @UpdatedDate, " _
            & " @UpdatedTime " _
            & " ) "
            ''↑↑2014/08/26 Ⅰ.2.管理項目追加 酒井 ADD
            '↑↑2014/09/26 酒井 ADD BaseBuhinFlg

            Dim db As New EBomDbClient
            Dim aDate As New ShisakuDate
            Dim aRhac0532vo As New Rhac0532Vo
            Dim param As New TShisakuBuhinEditTmpVo
            Dim impl As TehaichoSakuseiDao = New TehaichoSakuseiDaoImpl

            param.ShisakuEventCode = BuhinEditvo.ShisakuEventCode
            param.ShisakuBukaCode = BuhinEditvo.ShisakuBukaCode
            param.ShisakuBlockNo = BuhinEditvo.ShisakuBlockNo
            param.ShisakuBlockNoKaiteiNo = BuhinEditvo.ShisakuBlockNoKaiteiNo
            param.BuhinNoHyoujiJun = FindByNewBuhinNoHyoujiJun(BuhinEditvo.ShisakuEventCode, BuhinEditvo.ShisakuBukaCode, _
                                                BuhinEditvo.ShisakuBlockNo)

            If param.BuhinNoHyoujiJun = -1 Then
                Return
            End If

            param.Level = BuhinEditvo.Level
            param.ShukeiCode = BuhinEditvo.ShukeiCode
            param.SiaShukeiCode = BuhinEditvo.SiaShukeiCode
            param.GencyoCkdKbn = BuhinEditvo.GencyoCkdKbn
            param.MakerCode = BuhinEditvo.MakerCode
            param.MakerName = BuhinEditvo.MakerName
            param.BuhinNo = BuhinEditvo.BuhinNo
            param.BuhinNoKbn = BuhinEditvo.BuhinNoKbn
            param.BuhinNoKaiteiNo = BuhinEditvo.BuhinNoKaiteiNo
            param.EdaBan = BuhinEditvo.EdaBan
            param.BuhinName = BuhinEditvo.BuhinName
            param.Saishiyoufuka = BuhinEditvo.Saishiyoufuka
            param.ShutuzuYoteiDate = BuhinEditvo.ShutuzuYoteiDate
            ''↓↓2014/08/26 Ⅰ.2.管理項目追加 酒井 ADD BEGIN
            param.TsukurikataSeisaku = BuhinEditvo.TsukurikataSeisaku
            param.TsukurikataKatashiyou1 = BuhinEditvo.TsukurikataKatashiyou1
            param.TsukurikataKatashiyou2 = BuhinEditvo.TsukurikataKatashiyou2
            param.TsukurikataKatashiyou3 = BuhinEditvo.TsukurikataKatashiyou3
            param.TsukurikataTigu = BuhinEditvo.TsukurikataTigu
            param.TsukurikataNounyu = BuhinEditvo.TsukurikataNounyu
            param.TsukurikataKibo = BuhinEditvo.TsukurikataKibo
            ''↑↑2014/08/26 Ⅰ.2.管理項目追加 酒井 ADD END
            '↓↓2014/09/26 酒井 ADD BEGIN
            param.BaseBuhinFlg = BuhinEditvo.BaseBuhinFlg
            '↑↑2014/09/26 酒井 ADD END
            param.ZaishituKikaku1 = BuhinEditvo.ZaishituKikaku1
            param.ZaishituKikaku2 = BuhinEditvo.ZaishituKikaku2
            param.ZaishituKikaku3 = BuhinEditvo.ZaishituKikaku3
            param.ZaishituMekki = BuhinEditvo.ZaishituMekki
            param.ShisakuBankoSuryo = BuhinEditvo.ShisakuBankoSuryo
            param.ShisakuBankoSuryoU = BuhinEditvo.ShisakuBankoSuryoU


            ''↓↓2014/12/29 メタル対応追加フィールド (DANIEL)柳沼 ADD BEGIN
            param.MaterialInfoLength = BuhinEditvo.MaterialInfoLength
            param.MaterialInfoWidth = BuhinEditvo.MaterialInfoWidth
            param.DataItemKaiteiNo = BuhinEditvo.DataItemKaiteiNo
            param.DataItemAreaName = BuhinEditvo.DataItemAreaName
            param.DataItemSetName = BuhinEditvo.DataItemSetName
            param.DataItemKaiteiInfo = BuhinEditvo.DataItemKaiteiInfo
            ''↑↑2014/12/29 メタル対応追加フィールド (DANIEL)柳沼 ADD END


            param.ShisakuBuhinHi = BuhinEditvo.ShisakuBuhinHi
            param.ShisakuKataHi = BuhinEditvo.ShisakuKataHi

            '不正文字(')が入ってくることがあるので
            '   半角スペースに置き換える。
            If StringUtil.IsNotEmpty(BuhinEditvo.Bikou) Then
                BuhinEditvo.Bikou = BuhinEditvo.Bikou.Replace("'", " ")
            End If
            param.Bikou = BuhinEditvo.Bikou

            param.EditTourokubi = BuhinEditvo.EditTourokubi
            param.EditTourokujikan = BuhinEditvo.EditTourokujikan
            param.KaiteiHandanFlg = BuhinEditvo.KaiteiHandanFlg
            param.KyoukuSection = BuhinEditvo.KyoukuSection


            '集計コードがあるかチェック(無ければ海外集計有)'
            If Not StringUtil.IsEmpty(BuhinEditvo.ShukeiCode) Then
                If BuhinEditvo.ShukeiCode = "X" Then
                    param.TehaiKigou = "F"
                    param.Nouba = ""
                ElseIf BuhinEditvo.ShukeiCode = "A" Then
                    param.TehaiKigou = ""
                    param.Nouba = "X1"
                ElseIf BuhinEditvo.ShukeiCode = "E" Or BuhinEditvo.ShukeiCode = "Y" Then

                ElseIf BuhinEditvo.ShukeiCode = "R" Or BuhinEditvo.ShukeiCode = "J" Then
                    param.TehaiKigou = "F"
                    param.Nouba = ""
                End If
            Else
                If StringUtil.IsEmpty(BuhinEditvo.MakerCode) Then
                    If BuhinEditvo.SiaShukeiCode = "X" Then
                        param.TehaiKigou = "F"
                        param.Nouba = ""
                    ElseIf BuhinEditvo.SiaShukeiCode = "A" Then
                        param.TehaiKigou = ""
                        param.Nouba = "X1"
                    ElseIf BuhinEditvo.SiaShukeiCode = "E" Or BuhinEditvo.SiaShukeiCode = "Y" Then
                        param.TehaiKigou = "D"
                        param.Nouba = ""
                    ElseIf BuhinEditvo.SiaShukeiCode = "R" Or BuhinEditvo.SiaShukeiCode = "J" Then
                        param.TehaiKigou = "F"
                        param.Nouba = ""
                    End If
                Else
                    If StringUtil.Equals(Left(BuhinEditvo.MakerCode, 1), "A") Then
                        '海外集計コード'
                        If StringUtil.Equals(BuhinEditvo.SiaShukeiCode, "A") Then
                            param.TehaiKigou = "J"
                            param.Nouba = "US"
                        ElseIf StringUtil.Equals(BuhinEditvo.SiaShukeiCode, "E") Then
                            param.TehaiKigou = "B"
                            param.Nouba = "US"
                        ElseIf StringUtil.Equals(BuhinEditvo.SiaShukeiCode, "Y") Then
                            param.TehaiKigou = "B"
                            param.Nouba = "US"
                        End If
                    Else
                        '頭がAなら国内と同じ'
                        If BuhinEditvo.SiaShukeiCode = "X" Then
                            param.TehaiKigou = "F"
                            param.Nouba = ""
                        ElseIf BuhinEditvo.SiaShukeiCode = "A" Then
                            param.TehaiKigou = ""
                            param.Nouba = "X1"
                        ElseIf BuhinEditvo.SiaShukeiCode = "E" Or BuhinEditvo.SiaShukeiCode = "Y" Then
                            param.TehaiKigou = "D"
                            param.Nouba = ""
                        ElseIf BuhinEditvo.SiaShukeiCode = "R" Or BuhinEditvo.SiaShukeiCode = "J" Then
                            param.TehaiKigou = "F"
                            param.Nouba = ""
                        End If
                    End If
                End If
            End If
            param.Henkaten = "1"

            '製品区分ブランク？'
            param.ShisakuSeihinKbn = ""
            param.ShisakuListCode = shisakuListCode

            param.CreatedUserId = LoginInfo.Now.UserId
            param.CreatedDate = aDate.CurrentDateDbFormat
            param.CreatedTime = aDate.CurrentTimeDbFormat
            param.UpdatedUserId = LoginInfo.Now.UserId
            param.UpdatedDate = aDate.CurrentDateDbFormat
            param.UpdatedTime = aDate.CurrentTimeDbFormat

            db.Insert(sql, param)
        End Sub

        ''' <summary>
        ''' 部品表編集号車情報テンポラリ情報に追加する(員数増)
        ''' </summary>
        ''' <param name="gousyaTmpvo">部品表編集号車情報VO</param>
        ''' <remarks></remarks>
        Public Sub InsertByGousyaTMPInsuUp(ByVal gousyaTmpvo As TShisakuBuhinEditGousyaTmpVo, ByVal gousyaHyoujijun As Integer, ByVal insu As Integer) Implements TehaichoHikakuDao.InsertByGousyaTMPInsuUp
            Dim sql As String = _
            " INSERT INTO " & MBOM_DB_NAME & ".dbo.T_SHISAKU_BUHIN_EDIT_GOUSYA_TMP ( " _
            & " SHISAKU_EVENT_CODE, " _
            & " SHISAKU_BUKA_CODE, " _
            & " SHISAKU_BUKA_CODE, " _
            & " SHISAKU_BLOCK_NO, " _
            & " SHISAKU_BLOCK_NO_KAITEI_NO, " _
            & " BUHIN_NO_HYOUJI_JUN, " _
            & " SHISAKU_GOUSYA_HYOUJI_JUN, " _
            & " SHISAKU_GOUSYA, " _
            & " INSU_SURYO, " _
            & " CREATED_USER_ID, " _
            & " CREATED_DATE, " _
            & " CREATED_TIME, " _
            & " UPDATED_USER_ID, " _
            & " UPDATED_DATE, " _
            & " UPDATED_TIME " _
            & " ) " _
            & " VALUES ( " _
            & " @ShisakuEventCode, " _
            & " @ShisakuBukaCode, " _
            & " @ShisakuBlockNo, " _
            & " @ShisakuBlockNoKaiteiNo, " _
            & " @BuhinNoHyoujiJun, " _
            & " @ShisakuGousyaHyoujiJun, " _
            & " @ShisakuGousya, " _
            & " @InsuSuryo, " _
            & " @CreatedUserId, " _
            & " @CreatedDate, " _
            & " @CreatedTime, " _
            & " @UpdatedUserId, " _
            & " @UpdatedDate, " _
            & " @UpdatedTime " _
            & " ) "

            Dim sspl As String = _
            " SELECT * " _
            & " FROM " & MBOM_DB_NAME & ".dbo.T_SHISAKU_BUHIN_EDIT_GOUSYA_TMP " _
            & " WHERE " _
            & " SHISAKU_EVENT_CODE = @ShisakuEventCode " _
            & " AND SHISAKU_BUKA_CODE = @ShisakuBukaCode " _
            & " AND SHISAKU_BLOCK_NO = @ShisakuBlockNo " _
            & " AND SHISAKU_BLOCK_NO_KAITEI_NO = @ShisakuBlockNoKaiteiNo " _
            & " AND BUHIN_NO_HYOUJI_JUN = @BuhinNoHyoujiJun " _
            & " AND SHISAKU_GOUSYA_HYOUJI_JUN = @ShisakuGousyaHyoujiJun "



            Dim impl As TehaichoSakuseiDao = New TehaichoSakuseiDaoImpl
            Dim db As New EBomDbClient
            Dim aDate As New ShisakuDate
            Dim param As New TShisakuBuhinEditGousyaTmpVo

            param.ShisakuEventCode = gousyaTmpvo.ShisakuEventCode
            param.ShisakuBukaCode = gousyaTmpvo.ShisakuBukaCode
            param.ShisakuBlockNo = gousyaTmpvo.ShisakuBlockNo
            param.ShisakuBlockNoKaiteiNo = gousyaTmpvo.ShisakuBlockNoKaiteiNo
            param.BuhinNoHyoujiJun = FindByNewGousyaBuhinNoHyoujiJun(gousyaTmpvo.ShisakuEventCode, gousyaTmpvo.ShisakuBukaCode, gousyaTmpvo.ShisakuBlockNo)
            param.ShisakuGousyaHyoujiJun = gousyaHyoujijun
            param.ShisakuGousya = gousyaTmpvo.ShisakuGousya
            param.InsuSuryo = insu


            Dim Vo As New TShisakuBuhinEditGousyaTmpVo

            Vo = db.QueryForObject(Of TShisakuBuhinEditGousyaTmpVo)(sspl, param)

            '存在しているならばインサートするとプライマリキー違反が発生する'
            '親品番の存在は取得していないので'
            If Not Vo Is Nothing Then
                param.ShisakuGousya = gousyaTmpvo.ShisakuGousya
                param.InsuSuryo = gousyaTmpvo.InsuSuryo

                param.CreatedUserId = LoginInfo.Now.UserId
                param.CreatedDate = aDate.CurrentDateDbFormat
                param.CreatedTime = aDate.CurrentTimeDbFormat
                param.UpdatedUserId = LoginInfo.Now.UserId
                param.UpdatedDate = aDate.CurrentDateDbFormat
                param.UpdatedTime = aDate.CurrentTimeDbFormat

                If Vo.InsuSuryo < 0 Then
                    param.InsuSuryo = -1
                    impl.UpdateByBuhinEditGousyaTmp(param)
                    Return
                Else
                    If param.InsuSuryo < 0 Then
                        param.InsuSuryo = -1
                        impl.UpdateByBuhinEditGousyaTmp(param)
                        Return
                    Else
                        Dim NewInsuSuryo As Integer
                        NewInsuSuryo = gousyaTmpvo.InsuSuryo + Vo.InsuSuryo
                        param.InsuSuryo = NewInsuSuryo
                        impl.UpdateByBuhinEditGousyaTmp(param)
                        Return
                    End If
                End If
            End If



            param.CreatedUserId = LoginInfo.Now.UserId
            param.CreatedDate = aDate.CurrentDateDbFormat
            param.CreatedTime = aDate.CurrentTimeDbFormat
            param.UpdatedUserId = LoginInfo.Now.UserId
            param.UpdatedDate = aDate.CurrentDateDbFormat
            param.UpdatedTime = aDate.CurrentTimeDbFormat

            db.Insert(sql, param)
        End Sub

        Private _BuhinList As New ArrayList

        ''' <summary>
        ''' 部品表編集情報テンポラリ情報を追加する
        ''' </summary>
        ''' <param name="BuhinEditTMPvo">部品表編集情報VO</param>
        ''' <remarks></remarks>
        Public Sub InsertByBuhinEditTmpBase(ByVal BuhinEditTMPvo As List(Of TehaichoBuhinEditTmpVo), _
                                            ByVal seihinKbn As String) Implements TehaichoHikakuDao.InsertByBuhinEditTMPBase


            'Dim sqlList(BuhinEditTMPvo.Count - 1) As String

            Dim db As New EBomDbClient
            Dim aDate As New ShisakuDate

            Using insert As New SqlAccess(g_kanrihyoIni(DB_KEY_EBOM))
                insert.Open()
                insert.BeginTransaction()
                For index As Integer = 0 To BuhinEditTMPvo.Count - 1
                    'マージ対象は不要
                    If BuhinEditTMPvo(index).CreatedUserId = "Merge" Then
                        Continue For
                    End If

                    If StringUtil.IsEmpty(BuhinEditTMPvo(index).BuhinNo) Then
                        Continue For
                    End If
                    Dim param As New TShisakuBuhinEditTmpVo

                    param.ShisakuEventCode = BuhinEditTMPvo(index).ShisakuEventCode
                    param.ShisakuBukaCode = BuhinEditTMPvo(index).ShisakuBukaCode
                    param.ShisakuBlockNo = BuhinEditTMPvo(index).ShisakuBlockNo
                    param.ShisakuBlockNoKaiteiNo = BuhinEditTMPvo(index).ShisakuBlockNoKaiteiNo
                    param.BuhinNoHyoujiJun = BuhinEditTMPvo(index).BuhinNoHyoujiJun
                    param.Level = BuhinEditTMPvo(index).Level
                    param.ShukeiCode = BuhinEditTMPvo(index).ShukeiCode
                    param.SiaShukeiCode = BuhinEditTMPvo(index).SiaShukeiCode
                    param.GencyoCkdKbn = BuhinEditTMPvo(index).GencyoCkdKbn
                    param.MakerCode = BuhinEditTMPvo(index).MakerCode
                    param.MakerName = BuhinEditTMPvo(index).MakerName
                    param.BuhinNo = BuhinEditTMPvo(index).BuhinNo.Trim
                    param.BuhinNoKbn = BuhinEditTMPvo(index).BuhinNoKbn
                    param.BuhinNoKaiteiNo = BuhinEditTMPvo(index).BuhinNoKaiteiNo
                    param.EdaBan = BuhinEditTMPvo(index).EdaBan
                    param.BuhinName = BuhinEditTMPvo(index).BuhinName
                    param.GyouId = "888"

                    If BuhinEditTMPvo(index).Saishiyoufuka Is Nothing Then
                        param.Saishiyoufuka = ""
                    Else
                        param.Saishiyoufuka = BuhinEditTMPvo(index).Saishiyoufuka
                    End If

                    If BuhinEditTMPvo(index).ShutuzuYoteiDate Is Nothing Then
                        param.ShutuzuYoteiDate = 0
                    Else
                        param.ShutuzuYoteiDate = BuhinEditTMPvo(index).ShutuzuYoteiDate
                    End If

                    param.ZaishituKikaku1 = BuhinEditTMPvo(index).ZaishituKikaku1
                    param.ZaishituKikaku2 = BuhinEditTMPvo(index).ZaishituKikaku2
                    param.ZaishituKikaku3 = BuhinEditTMPvo(index).ZaishituKikaku3
                    param.ZaishituMekki = BuhinEditTMPvo(index).ZaishituMekki
                    ''↓↓2014/07/24 Ⅰ.2.管理項目追加_ap) (TES)張 ADD BEGIN
                    param.TsukurikataSeisaku = BuhinEditTMPvo(index).TsukurikataSeisaku
                    param.TsukurikataKatashiyou1 = BuhinEditTMPvo(index).TsukurikataKatashiyou1
                    param.TsukurikataKatashiyou2 = BuhinEditTMPvo(index).TsukurikataKatashiyou2
                    param.TsukurikataKatashiyou3 = BuhinEditTMPvo(index).TsukurikataKatashiyou3
                    param.TsukurikataTigu = BuhinEditTMPvo(index).TsukurikataTigu
                    If BuhinEditTMPvo(index).TsukurikataNounyu Is Nothing Then
                        param.TsukurikataNounyu = 0
                    Else
                        param.TsukurikataNounyu = BuhinEditTMPvo(index).TsukurikataNounyu
                    End If
                    If StringUtil.IsNotEmpty(BuhinEditTMPvo(index).TsukurikataKibo) Then
                        BuhinEditTMPvo(index).TsukurikataKibo = BuhinEditTMPvo(index).TsukurikataKibo.Replace("'", " ")
                    End If
                    param.TsukurikataKibo = BuhinEditTMPvo(index).TsukurikataKibo
                    ''↑↑2014/07/24 Ⅰ.2.管理項目追加_ap) (TES)張 ADD END
                    '↓↓2014/09/26 酒井 ADD BEGIN
                    param.BaseBuhinFlg = BuhinEditTMPvo(index).BaseBuhinFlg
                    '↑↑2014/09/26 酒井 ADD END
                    param.ShisakuBankoSuryo = BuhinEditTMPvo(index).ShisakuBankoSuryo
                    param.ShisakuBankoSuryoU = BuhinEditTMPvo(index).ShisakuBankoSuryoU

                    ''↓↓2014/12/26 18手配帳作成 (TES)張 ADD BEGIN
                    If BuhinEditTMPvo(index).MaterialInfoLength Is Nothing Then
                        param.MaterialInfoLength = 0
                    Else
                        param.MaterialInfoLength = BuhinEditTMPvo(index).MaterialInfoLength
                    End If
                    If BuhinEditTMPvo(index).MaterialInfoWidth Is Nothing Then
                        param.MaterialInfoWidth = 0
                    Else
                        param.MaterialInfoWidth = BuhinEditTMPvo(index).MaterialInfoWidth
                    End If
                    param.DataItemKaiteiNo = BuhinEditTMPvo(index).DataItemKaiteiNo
                    param.DataItemAreaName = BuhinEditTMPvo(index).DataItemAreaName
                    param.DataItemSetName = BuhinEditTMPvo(index).DataItemSetName
                    param.DataItemKaiteiInfo = BuhinEditTMPvo(index).DataItemKaiteiInfo
                    ''↑↑2014/12/26 18手配帳作成 (TES)張 ADD END
                    param.ShisakuBuhinHi = BuhinEditTMPvo(index).ShisakuBuhinHi
                    If BuhinEditTMPvo(index).ShisakuBuhinHi Is Nothing Then
                        param.ShisakuBuhinHi = 0
                    Else
                        param.ShisakuKataHi = BuhinEditTMPvo(index).ShisakuBuhinHi
                    End If

                    If BuhinEditTMPvo(index).ShisakuKataHi Is Nothing Then
                        param.ShisakuKataHi = 0
                    Else
                        param.ShisakuKataHi = BuhinEditTMPvo(index).ShisakuKataHi
                    End If


                    '不正文字(')が入ってくることがあるので
                    '   半角スペースに置き換える。
                    If StringUtil.IsNotEmpty(BuhinEditTMPvo(index).Bikou) Then
                        BuhinEditTMPvo(index).Bikou = BuhinEditTMPvo(index).Bikou.Replace("'", " ")
                    End If
                    param.Bikou = BuhinEditTMPvo(index).Bikou

                    param.EditTourokubi = BuhinEditTMPvo(index).EditTourokubi
                    param.EditTourokujikan = BuhinEditTMPvo(index).EditTourokujikan
                    If StringUtil.IsEmpty(param.EditTourokubi) Then
                        param.EditTourokubi = 0
                    End If
                    If StringUtil.IsEmpty(param.EditTourokujikan) Then
                        param.EditTourokujikan = 0
                    End If


                    param.KaiteiHandanFlg = BuhinEditTMPvo(index).KaiteiHandanFlg
                    param.TehaiKigou = ""
                    param.Nouba = ""
                    param.KyoukuSection = BuhinEditTMPvo(index).KyoukuSection
                    param.SenyouMark = ""
                    param.ShukeiCode = BuhinEditTMPvo(index).ShukeiCode
                    param.SiaShukeiCode = BuhinEditTMPvo(index).SiaShukeiCode
                    param.Henkaten = ""
                    param.ShisakuSeihinKbn = seihinKbn
                    param.ShisakuListCode = ""
                    param.CreatedUserId = LoginInfo.Now.UserId
                    param.CreatedDate = aDate.CurrentDateDbFormat
                    param.CreatedTime = aDate.CurrentTimeDbFormat
                    param.UpdatedUserId = LoginInfo.Now.UserId
                    param.UpdatedDate = aDate.CurrentDateDbFormat
                    param.UpdatedTime = aDate.CurrentTimeDbFormat

                    Dim key As New System.Text.StringBuilder
                    With key
                        .AppendLine(param.ShisakuEventCode)
                        .AppendLine(param.ShisakuBukaCode)
                        .AppendLine(param.ShisakuBlockNo)
                        .AppendLine(param.ShisakuBlockNoKaiteiNo)
                        .AppendLine(param.BuhinNoHyoujiJun)
                        .AppendLine(param.GyouId)
                    End With


                    If Not _BuhinList.Contains(key.ToString) Then
                        _BuhinList.Add(key.ToString)

                        Dim sql As String = _
                       " INSERT INTO " & MBOM_DB_NAME & ".dbo.T_SHISAKU_BUHIN_EDIT_TMP (" _
                       & " SHISAKU_EVENT_CODE, " _
                       & " SHISAKU_BUKA_CODE, " _
                       & " SHISAKU_BLOCK_NO, " _
                       & " SHISAKU_BLOCK_NO_KAITEI_NO, " _
                       & " BUHIN_NO_HYOUJI_JUN, " _
                       & " GYOU_ID, " _
                       & " LEVEL, " _
                       & " SHUKEI_CODE, " _
                       & " SIA_SHUKEI_CODE, " _
                       & " GENCYO_CKD_KBN, " _
                       & " MAKER_CODE, " _
                       & " MAKER_NAME, " _
                       & " BUHIN_NO, " _
                       & " BUHIN_NO_KBN, " _
                       & " BUHIN_NO_KAITEI_NO, " _
                       & " EDA_BAN, " _
                       & " BUHIN_NAME, " _
                       & " SAISHIYOUFUKA, " _
                       & " SHUTUZU_YOTEI_DATE, " _
                       & " TSUKURIKATA_SEISAKU, " _
                       & " TSUKURIKATA_KATASHIYOU_1, " _
                       & " TSUKURIKATA_KATASHIYOU_2, " _
                       & " TSUKURIKATA_KATASHIYOU_3, " _
                       & " TSUKURIKATA_TIGU, " _
                       & " TSUKURIKATA_NOUNYU, " _
                       & " TSUKURIKATA_KIBO, " _
                       & " BASE_BUHIN_FLG, " _
                       & " ZAISHITU_KIKAKU_1, " _
                       & " ZAISHITU_KIKAKU_2, " _
                       & " ZAISHITU_KIKAKU_3, " _
                       & " ZAISHITU_MEKKI, " _
                       & " SHISAKU_BANKO_SURYO, " _
                       & " SHISAKU_BANKO_SURYO_U, " _
                       & " MATERIAL_INFO_LENGTH, " _
                       & " MATERIAL_INFO_WIDTH, " _
                       & " DATA_ITEM_KAITEI_NO, " _
                       & " DATA_ITEM_AREA_NAME, " _
                       & " DATA_ITEM_SET_NAME, " _
                       & " DATA_ITEM_KAITEI_INFO, " _
                       & " SHISAKU_BUHIN_HI, " _
                       & " SHISAKU_KATA_HI, " _
                       & " BIKOU, " _
                       & " EDIT_TOUROKUBI, " _
                       & " EDIT_TOUROKUJIKAN, " _
                       & " KAITEI_HANDAN_FLG, " _
                       & " TEHAI_KIGOU, " _
                       & " NOUBA, " _
                       & " KYOUKU_SECTION, " _
                       & " SENYOU_MARK, " _
                       & " KOUTAN, " _
                       & " STSR_DHSTBA, " _
                       & " HENKATEN, " _
                       & " SHISAKU_SEIHIN_KBN, " _
                       & " SHISAKU_LIST_CODE, " _
                       & " CREATED_USER_ID, " _
                       & " CREATED_DATE, " _
                       & " CREATED_TIME, " _
                       & " UPDATED_USER_ID, " _
                       & " UPDATED_DATE, " _
                       & " UPDATED_TIME " _
                       & " ) " _
                       & " VALUES ( " _
                       & " '" & param.ShisakuEventCode & "' , " _
                       & " '" & param.ShisakuBukaCode & "' , " _
                       & " '" & param.ShisakuBlockNo & "' , " _
                       & " '" & param.ShisakuBlockNoKaiteiNo & "' , " _
                       & param.BuhinNoHyoujiJun & " , " _
                       & "'" & param.GyouId & "' , " _
                       & param.Level & ", " _
                       & " '" & param.ShukeiCode & "' , " _
                       & " '" & param.SiaShukeiCode & "' , " _
                       & " '" & param.GencyoCkdKbn & "' , " _
                       & " '" & param.MakerCode & "' , " _
                       & " '" & param.MakerName & "' , " _
                       & " '" & param.BuhinNo & "' , " _
                       & " '" & param.BuhinNoKbn & "' , " _
                       & " '" & param.BuhinNoKaiteiNo & "' , " _
                       & " '" & param.EdaBan & "' , " _
                       & " '" & param.BuhinName & "' , " _
                       & " '" & param.Saishiyoufuka & "' , " _
                       & param.ShutuzuYoteiDate & " , " _
                       & " '" & param.TsukurikataSeisaku & "' , " _
                       & " '" & param.TsukurikataKatashiyou1 & "' , " _
                       & " '" & param.TsukurikataKatashiyou2 & "' , " _
                       & " '" & param.TsukurikataKatashiyou3 & "' , " _
                       & " '" & param.TsukurikataTigu & "' , " _
                       & param.TsukurikataNounyu & " , " _
                       & " '" & param.TsukurikataKibo & "' , " _
                       & " '" & param.BaseBuhinFlg & "' , " _
                       & " '" & param.ZaishituKikaku1 & "' , " _
                       & " '" & param.ZaishituKikaku2 & "' , " _
                       & " '" & param.ZaishituKikaku3 & "' , " _
                       & " '" & param.ZaishituMekki & "' , " _
                       & " '" & param.ShisakuBankoSuryo & "' , " _
                       & " '" & param.ShisakuBankoSuryoU & "' , " _
                       & param.MaterialInfoLength & " , " _
                       & param.MaterialInfoWidth & " , " _
                       & " '" & param.DataItemKaiteiNo & "' , " _
                       & " '" & param.DataItemAreaName & "' , " _
                       & " '" & param.DataItemSetName & "' , " _
                       & " '" & param.DataItemKaiteiInfo & "' , " _
                       & param.ShisakuBuhinHi & " , " _
                       & param.ShisakuKataHi & " , " _
                       & " '" & param.Bikou & "' , " _
                       & param.EditTourokubi & " , " _
                       & param.EditTourokujikan & " , " _
                       & " '" & param.KaiteiHandanFlg & "' , " _
                       & " '" & param.TehaiKigou & "' , " _
                       & " '' , " _
                       & " '" & param.KyoukuSection & "' , " _
                       & " '" & param.SenyouMark & "' , " _
                       & " '" & param.Koutan & "' , " _
                       & " '" & param.StsrDhstba & "' , " _
                       & " '' , " _
                       & " '" & param.ShisakuSeihinKbn & "' , " _
                       & " '" & param.ShisakuListCode & "' , " _
                       & " '" & param.CreatedUserId & "' , " _
                       & " '" & param.CreatedDate & "' , " _
                       & " '" & param.CreatedTime & "' , " _
                       & " '" & param.UpdatedUserId & "' , " _
                       & " '" & param.UpdatedDate & "' , " _
                       & " '" & param.UpdatedTime & "'" _
                       & " ) "
                        ''↑↑2014/12/26 18手配帳作成 (TES)張 ADD END
                        ''↑↑2014/08/26 Ⅰ.2.管理項目追加_ap) 酒井 ADD
                        '↓↓2014/09/26 酒井 ADD BEGIN
                        '& " BASE_BUHIN_FLG, " _
                        '& " '" & param.BaseBuhinFlg & "' , " _
                        '↑↑2014/09/26 酒井 ADD END

                        Try
                            insert.ExecuteNonQuery(sql)
                        Catch ex As SqlClient.SqlException
                            'Dim prm As Integer = ex.Message.IndexOf("PRIMARY")
                            'If prm < 0 Then
                            '    Dim msg As String = sql + ex.Message
                            '    MsgBox(ex.Message)
                            'Else
                            Continue For
                            'End If
                        End Try
                    End If

                Next
                insert.Commit()
            End Using

            'Using insert As New SqlAccess(g_kanrihyoIni(DB_KEY_EBOM))
            '    insert.Open()
            '    insert.BeginTransaction()
            '    For index As Integer = 0 To BuhinEditTMPvo.Count - 1
            '        Try
            '            If Not StringUtil.IsEmpty(sqlList(index)) Then
            '                insert.ExecuteNonQuery(sqlList(index))
            '            End If
            '        Catch ex As SqlClient.SqlException
            '            Dim prm As Integer = ex.Message.IndexOf("PRIMARY")
            '            If prm < 0 Then
            '                Dim msg As String = sqlList(index) + ex.Message
            '                MsgBox(ex.Message)
            '            Else
            '                Continue For
            '            End If
            '        End Try
            '    Next
            '    insert.Commit()
            'End Using
        End Sub

        Private _GoshaList As New ArrayList

        ''' <summary>
        ''' 部品表編集号車情報テンポラリ情報を追加する
        ''' </summary>
        ''' <param name="gousyaTmpvo">部品表編集号車情報VO</param>
        ''' <remarks></remarks>
        Public Sub InsertByGousyaTMP(ByVal gousyaTmpvo As List(Of TehaichoBuhinEditTmpVo)) Implements TehaichoHikakuDao.InsertByGousyaTMPBase

            Dim db As New EBomDbClient
            Dim aDate As New ShisakuDate
            Dim param As New TShisakuBuhinEditGousyaTmpVo

            'Dim sqlList(gousyaTmpvo.Count - 1) As String
            'Dim uSqlList(gousyaTmpvo.Count - 1) As String

            For index As Integer = 0 To gousyaTmpvo.Count - 1

                param.ShisakuEventCode = gousyaTmpvo(index).ShisakuEventCode
                param.ShisakuBukaCode = gousyaTmpvo(index).ShisakuBukaCode
                param.ShisakuBlockNo = gousyaTmpvo(index).ShisakuBlockNo
                param.ShisakuBlockNoKaiteiNo = gousyaTmpvo(index).ShisakuBlockNoKaiteiNo

                param.BuhinNoHyoujiJun = gousyaTmpvo(index).BuhinNoHyoujiJun
                param.GyouId = "888"
                param.ShisakuGousyaHyoujiJun = gousyaTmpvo(index).ShisakuGousyaHyoujiJun

                param.ShisakuGousya = gousyaTmpvo(index).ShisakuGousya
                param.InsuSuryo = gousyaTmpvo(index).InsuSuryo

                param.CreatedUserId = LoginInfo.Now.UserId
                param.CreatedDate = aDate.CurrentDateDbFormat
                param.CreatedTime = aDate.CurrentTimeDbFormat
                param.UpdatedUserId = LoginInfo.Now.UserId
                param.UpdatedDate = aDate.CurrentDateDbFormat
                param.UpdatedTime = aDate.CurrentTimeDbFormat

                Dim sql As String = _
                " INSERT INTO " & MBOM_DB_NAME & ".dbo.T_SHISAKU_BUHIN_EDIT_GOUSYA_TMP ( " _
                & " SHISAKU_EVENT_CODE, " _
                & " SHISAKU_BUKA_CODE, " _
                & " SHISAKU_BLOCK_NO, " _
                & " SHISAKU_BLOCK_NO_KAITEI_NO, " _
                & " BUHIN_NO_HYOUJI_JUN, " _
                & " SHISAKU_GOUSYA_HYOUJI_JUN, " _
                & " GYOU_ID, " _
                & " SHISAKU_GOUSYA, " _
                & " INSU_SURYO, " _
                & " CREATED_USER_ID, " _
                & " CREATED_DATE, " _
                & " CREATED_TIME, " _
                & " UPDATED_USER_ID, " _
                & " UPDATED_DATE, " _
                & " UPDATED_TIME " _
                & " ) " _
                & " VALUES ( " _
                & " '" & param.ShisakuEventCode & "' ," _
                & " '" & param.ShisakuBukaCode & "' ," _
                & " '" & param.ShisakuBlockNo & "' ," _
                & " '" & param.ShisakuBlockNoKaiteiNo & "' ," _
                & param.BuhinNoHyoujiJun & "," _
                & param.ShisakuGousyaHyoujiJun & "," _
                & " '" & param.GyouId & "' , " _
                & " '" & param.ShisakuGousya & "' ," _
                & param.InsuSuryo & "," _
                & " '" & param.CreatedUserId & "' ," _
                & " '" & param.CreatedDate & "' ," _
                & " '" & param.CreatedTime & "' ," _
                & " '" & param.UpdatedUserId & "' ," _
                & " '" & param.UpdatedDate & "' ," _
                & " '" & param.UpdatedTime & "' " _
                & " ) "

                Dim key As New System.Text.StringBuilder
                With key
                    .AppendLine(param.ShisakuEventCode)
                    .AppendLine(param.ShisakuBukaCode)
                    .AppendLine(param.ShisakuBlockNo)
                    .AppendLine(param.ShisakuBlockNoKaiteiNo)
                    .AppendLine(param.BuhinNoHyoujiJun.ToString)
                    .AppendLine(param.ShisakuGousyaHyoujiJun.ToString)
                    .AppendLine(param.GyouId)
                End With

                If Not _GoshaList.Contains(key.ToString) Then
                    _GoshaList.Add(key.ToString)
                    Try
                        db.Insert(sql)
                    Catch ex As SqlClient.SqlException
                        'Dim prm As Integer = ex.Message.IndexOf("PRIMARY")
                        'If prm < 0 Then
                        '    MsgBox(ex.Message)
                        'Else
                        'End If
                        Dim Usql As String = _
                        " UPDATE  " & MBOM_DB_NAME & ".dbo.T_SHISAKU_BUHIN_EDIT_GOUSYA_TMP " _
                        & " SET INSU_SURYO = INSU_SURYO + " & param.InsuSuryo & "" _
                        & " WHERE SHISAKU_EVENT_CODE = '" & param.ShisakuEventCode & "'" _
                        & " AND SHISAKU_BUKA_CODE =  '" & param.ShisakuBukaCode & "'" _
                        & " AND SHISAKU_BLOCK_NO = '" & param.ShisakuBlockNo & "'" _
                        & " AND SHISAKU_BLOCK_NO_KAITEI_NO = '" & param.ShisakuBlockNoKaiteiNo & "'" _
                        & " AND BUHIN_NO_HYOUJI_JUN = '" & param.BuhinNoHyoujiJun & "'" _
                        & " AND SHISAKU_GOUSYA_HYOUJI_JUN = '" & param.ShisakuGousyaHyoujiJun & "'" _
                        & " AND GYOU_ID = '" & param.GyouId & "'"
                        '
                        db.Update(Usql)
                    End Try
                Else
                    Dim Usql As String = _
                    " UPDATE  " & MBOM_DB_NAME & ".dbo.T_SHISAKU_BUHIN_EDIT_GOUSYA_TMP " _
                    & " SET INSU_SURYO = INSU_SURYO + " & param.InsuSuryo & "" _
                    & " WHERE SHISAKU_EVENT_CODE = '" & param.ShisakuEventCode & "'" _
                    & " AND SHISAKU_BUKA_CODE =  '" & param.ShisakuBukaCode & "'" _
                    & " AND SHISAKU_BLOCK_NO = '" & param.ShisakuBlockNo & "'" _
                    & " AND SHISAKU_BLOCK_NO_KAITEI_NO = '" & param.ShisakuBlockNoKaiteiNo & "'" _
                    & " AND BUHIN_NO_HYOUJI_JUN = '" & param.BuhinNoHyoujiJun & "'" _
                    & " AND SHISAKU_GOUSYA_HYOUJI_JUN = '" & param.ShisakuGousyaHyoujiJun & "'" _
                    & " AND GYOU_ID = '" & param.GyouId & "'"
                    '
                    db.Update(Usql)
                End If





            Next

        End Sub




#End Region



        ''' <summary>
        ''' ブロック一覧の取得
        ''' </summary>
        ''' <param name="eventCode">イベントコード</param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Function getBlockList(ByVal eventCode As String) As List(Of TShisakuSekkeiBlockVo) Implements TehaichoHikakuDao.getBlockList
            Dim sql As New System.Text.StringBuilder
            With sql
                .AppendLine(" SELECT SB.SHISAKU_BLOCK_NO, SB.SHISAKU_BLOCK_NO_KAITEI_NO, SB.KACHOU_SYOUNIN_JYOUTAI, SB.BLOCK_FUYOU ")
                .AppendLine(" FROM " & MBOM_DB_NAME & ".dbo.T_SHISAKU_SEKKEI_BLOCK SB ")
                .AppendLine(" WHERE  ")
                .AppendLine(" SB.SHISAKU_EVENT_CODE = @ShisakuEventCode ")
                .AppendLine(" AND SB.SHISAKU_BLOCK_NO_KAITEI_NO ")
                .AppendLine(" = (  SELECT MAX ( CONVERT(INT,COALESCE ( SHISAKU_BLOCK_NO_KAITEI_NO,'' ) ) ) AS SHISAKU_BLOCK_NO_KAITEI_NO  ")
                .AppendLine(" FROM " & MBOM_DB_NAME & ".dbo.T_SHISAKU_SEKKEI_BLOCK ")
                .AppendLine(" WHERE SHISAKU_EVENT_CODE = SB.SHISAKU_EVENT_CODE ")
                .AppendLine(" AND SHISAKU_BUKA_CODE = SB.SHISAKU_BUKA_CODE ")
                .AppendLine(" AND SHISAKU_BLOCK_NO = SB.SHISAKU_BLOCK_NO ) ")
                .AppendLine(" ORDER BY SB.SHISAKU_BLOCK_NO ")
            End With
            Dim param As New TShisakuSekkeiBlockVo
            param.ShisakuEventCode = eventCode
            Dim db As New EBomDbClient
            Return db.QueryForList(Of TShisakuSekkeiBlockVo)(sql.ToString, param)
        End Function

        ''' <summary>
        ''' 部品表の取得
        ''' </summary>
        ''' <param name="eventCode">イベントコード</param>
        ''' <param name="blockNo">ブロックNo</param>
        ''' <param name="kaiteiNo">改訂No</param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Function FindTShisakuBuhinEdit(ByVal eventCode As String, ByVal blockNo As String, ByVal kaiteiNo As String) As List(Of TShisakuBuhinEditVo) Implements TehaichoHikakuDao.FindTShisakuBuhinEdit
            Dim sql As New System.Text.StringBuilder
            With sql
                If kaiteiNo = "BASE" Then
                    .AppendLine("SELECT * FROM " & MBOM_DB_NAME & ".dbo.T_SHISAKU_BUHIN_EDIT_BASE")
                Else
                    .AppendLine("SELECT * FROM " & MBOM_DB_NAME & ".dbo.T_SHISAKU_BUHIN_EDIT")
                End If
                .AppendLine(" WHERE SHISAKU_EVENT_CODE = @ShisakuEventCode")
                .AppendLine(" AND SHISAKU_BLOCK_NO = @ShisakuBlockNo")
                If kaiteiNo = "BASE" Then
                    .AppendLine(" AND SHISAKU_BLOCK_NO_KAITEI_NO = '000'")
                Else
                    .AppendLine(" AND SHISAKU_BLOCK_NO_KAITEI_NO = @ShisakuBlockNoKaiteiNo")
                End If
                .AppendLine(" AND BASE_BUHIN_FLG = '1'")
                .AppendLine(" ORDER BY BUHIN_NO_HYOUJI_JUN")
            End With
            Dim param As New TShisakuBuhinEditVo
            param.ShisakuEventCode = eventCode
            param.ShisakuBlockNo = blockNo
            param.ShisakuBlockNoKaiteiNo = kaiteiNo
            Dim db As New EBomDbClient
            Return db.QueryForList(Of TShisakuBuhinEditVo)(sql.ToString, param)
        End Function





    End Class
End Namespace