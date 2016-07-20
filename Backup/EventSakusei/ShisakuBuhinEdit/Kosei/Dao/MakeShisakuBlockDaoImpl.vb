Imports ShisakuCommon.Db.EBom
Imports ShisakuCommon.Db.EBom.Vo
Imports ShisakuCommon.Db.Impl
Imports System.Text
Imports ShisakuCommon
Imports ShisakuCommon.Util.LabelValue

Namespace ShisakuBuhinEdit.Kosei.Dao
    Public Class MakeShisakuBlockDaoImpl : Inherits DaoEachFeatureImpl
        Implements MakeShisakuBlockDao

        ''' <summary>
        ''' 試作部品情報のメーカー情報を返す
        ''' </summary>
        ''' <param name="ShisakuEventCode">試作イベントコード</param>
        ''' <param name="ShisakuBukaCode">試作部課コード</param>
        ''' <param name="ShisakuBlockNo">試作ブロックNo</param>
        ''' <param name="ShisakuBlockNoKaiteiNo">試作ブロックNo改訂No</param>
        ''' <returns>メーカー情報</returns>
        ''' <remarks></remarks>
        Public Function FindMakerByShisakuBuhin(ByVal ShisakuEventCode As String, ByVal ShisakuBukaCode As String, ByVal ShisakuBlockNo As String, ByVal ShisakuBlockNoKaiteiNo As String) As List(Of Rhac0610Vo) Implements MakeShisakuBlockDao.FindMakerByShisakuBuhin
            Dim sql As String = _
                "SELECT * FROM " & RHACLIBF_DB_NAME & ".dbo.RHAC0610 WITH (NOLOCK, NOWAIT) WHERE MAKER_CODE IN (SELECT MAKER_CODE FROM " & MBOM_DB_NAME & ".dbo.T_SHISAKU_BUHIN WHERE SHISAKU_EVENT_CODE = @ShisakuEventCode AND SHISAKU_BUKA_CODE = @ShisakuBukaCode AND SHISAKU_BLOCK_NO = @ShisakuBlockNo AND SHISAKU_BLOCK_NO_KAITEI_NO = @ShisakuBlockNoKaiteiNo)"
            Dim param As New TShisakuBuhinVo
            param.ShisakuEventCode = ShisakuEventCode
            param.ShisakuBukaCode = ShisakuBukaCode
            param.ShisakuBlockNo = ShisakuBlockNo
            param.ShisakuBlockNoKaiteiNo = ShisakuBlockNoKaiteiNo
            Dim db As New EBomDbClient
            Return db.QueryForList(Of Rhac0610Vo)(sql, param)
        End Function

        ''' <summary>
        ''' 自給品の有無を取得する
        ''' </summary>
        ''' <param name="ShisakuEventCode">試作イベントコード</param>
        ''' <returns>自給品の有無</returns>
        ''' <remarks></remarks>
        Function FindByJikyuUmu(ByVal ShisakuEventCode As String) As String Implements MakeShisakuBlockDao.FindByJikyuUmu
            Dim sb As New StringBuilder
            With sb
                .AppendLine(" SELECT JIKYU_UMU ")
                .AppendLine(" FROM " & MBOM_DB_NAME & ".dbo.T_SHISAKU_EVENT")
                .AppendLine(" WHERE SHISAKU_EVENT_CODE = @ShisakuEventCode")
            End With
            Dim param As New TShisakuEventVo
            param.ShisakuEventCode = ShisakuEventCode
            Dim db As New EBomDbClient
            Dim Vo As New TShisakuEventVo
            Dim result As String = ""
            Vo = db.QueryForObject(Of TShisakuEventVo)(sb.ToString, param)

            result = Vo.JikyuUmu
            Return result
        End Function

        ''↓↓2014/07/24 Ⅰ.3.設計編集 ベース改修専用化_g) (TES)張 ADD BEGIN
        ''' <summary>
        ''' 購入指示の有無を取得する
        ''' </summary>
        ''' <param name="ShisakuEventCode">試作イベントコード</param>
        ''' <returns>購入指示の有無</returns>
        ''' <remarks></remarks>
        Function FindByKounyuShiji(ByVal ShisakuEventCode As String) As String Implements MakeShisakuBlockDao.FindByKounyuShiji
            Dim sb As New StringBuilder
            With sb
                .AppendLine(" SELECT KOUNYU_SHIJI_FLG ")
                .AppendLine(" FROM " & MBOM_DB_NAME & ".dbo.T_SHISAKU_EVENT")
                .AppendLine(" WHERE SHISAKU_EVENT_CODE = @ShisakuEventCode")
            End With
            Dim param As New TShisakuEventVo
            param.ShisakuEventCode = ShisakuEventCode
            Dim db As New EBomDbClient
            Dim Vo As New TShisakuEventVo
            Dim result As String = ""
            Vo = db.QueryForObject(Of TShisakuEventVo)(sb.ToString, param)

            result = Vo.KounyuShijiFlg
            Return result
        End Function
        ''↑↑2014/07/24 Ⅰ.3.設計編集 ベース改修専用化_g) (TES)張 ADD END

        ''↓↓2014/07/23 Ⅰ.2.管理項目追加_ag) (TES)張 ADD BEGIN
        ''' <summary>
        ''' 作り方を取得する
        ''' </summary>
        ''' <param name="ShisakuEventCode">試作イベントコード</param>
        ''' <returns>自給品の有無</returns>
        ''' <remarks></remarks>
        Function FindByTsukurikata(ByVal ShisakuEventCode As String) As String Implements MakeShisakuBlockDao.FindByTsukurikata
            Dim sb As New StringBuilder
            With sb
                .AppendLine(" SELECT TSUKURIKATA_FLG ")
                .AppendLine(" FROM " & MBOM_DB_NAME & ".dbo.T_SHISAKU_EVENT")
                .AppendLine(" WHERE SHISAKU_EVENT_CODE = @ShisakuEventCode")
            End With
            Dim param As New TShisakuEventVo
            param.ShisakuEventCode = ShisakuEventCode
            Dim db As New EBomDbClient
            Dim Vo As New TShisakuEventVo
            Dim result As String = ""
            Vo = db.QueryForObject(Of TShisakuEventVo)(sb.ToString, param)

            result = Vo.TsukurikataFlg
            Return result
        End Function
        ''↑↑2014/07/23 Ⅰ.2.管理項目追加_ag) (TES)張 ADD END

        ''↓↓2014/07/23 Ⅰ.2.管理項目追加_bn) (TES)張 ADD BEGIN
        ''' <summary>
        ''' 作り方コンボ表示内容取得処理
        ''' </summary>
        ''' <returns>作り方</returns>
        ''' <remarks></remarks>
        Public Function FindTsukurikataSeisakuLabelValues() As List(Of LabelValueVo) Implements MakeShisakuBlockDao.FindTsukurikataSeisakuLabelValues
            Dim sql As String = _
                "SELECT " _
                & " TSUKURIKATA_NAME AS LABEL, " _
                & " TSUKURIKATA_NAME AS VALUE " _
                & " FROM " & MBOM_DB_NAME & ".dbo.M_SHISAKU_TSUKURIKATA WITH (NOLOCK, NOWAIT) " _
                & " WHERE " _
                & " TSUKURIKATA_KBN = '1' " _
                & " ORDER BY " _
                & " TSUKURIKATA_NO "

            Dim db As New EBomDbClient
            Return db.QueryForList(Of LabelValueVo)(sql)
        End Function
        ''↑↑2014/07/23 Ⅰ.2.管理項目追加_bn) (TES)張 ADD END

        ''↓↓2014/07/23 Ⅰ.2.管理項目追加_bt) (TES)張 ADD BEGIN
        ''' <summary>
        ''' 作り方コンボ表示内容取得処理
        ''' </summary>
        ''' <returns>作り方</returns>
        ''' <remarks></remarks>
        Public Function FindTsukurikataKatashiyou1LabelValues() As List(Of LabelValueVo) Implements MakeShisakuBlockDao.FindTsukurikataKatashiyou1LabelValues
            Dim sql As String = _
                "SELECT " _
                & " CAST(TSUKURIKATA_NO AS varchar) + '.' + TSUKURIKATA_NAME AS LABEL, " _
                & " CAST(TSUKURIKATA_NO AS varchar) + '.' + TSUKURIKATA_NAME AS VALUE " _
                & " FROM " & MBOM_DB_NAME & ".dbo.M_SHISAKU_TSUKURIKATA WITH (NOLOCK, NOWAIT) " _
                & " WHERE " _
                & " TSUKURIKATA_KBN = '3' " _
                & " ORDER BY " _
                & " TSUKURIKATA_NO "
            ''↓↓2014/09/02 Ⅰ.2.管理項目追加 酒井 ADD BEGIN
            '    & " TSUKURIKATA_NAME AS LABEL, " _
            '    & " TSUKURIKATA_NAME AS VALUE " _
            ''↑↑2014/09/02 Ⅰ.2.管理項目追加 酒井 ADD END

            Dim db As New EBomDbClient
            Return db.QueryForList(Of LabelValueVo)(sql)
        End Function
        Public Function FindTsukurikataKatashiyou2LabelValues() As List(Of LabelValueVo) Implements MakeShisakuBlockDao.FindTsukurikataKatashiyou2LabelValues
            Dim sql As String = _
                "SELECT " _
                & " CAST(TSUKURIKATA_NO AS varchar) + '.' + TSUKURIKATA_NAME AS LABEL, " _
                & " CAST(TSUKURIKATA_NO AS varchar) + '.' + TSUKURIKATA_NAME AS VALUE " _
                & " FROM " & MBOM_DB_NAME & ".dbo.M_SHISAKU_TSUKURIKATA WITH (NOLOCK, NOWAIT) " _
                & " WHERE " _
                & " TSUKURIKATA_KBN = '3' " _
                & " ORDER BY " _
                & " TSUKURIKATA_NO "
            ''↓↓2014/09/02 Ⅰ.2.管理項目追加 酒井 ADD BEGIN
            '    & " TSUKURIKATA_NAME AS LABEL, " _
            '    & " TSUKURIKATA_NAME AS VALUE " _
            ''↑↑2014/09/02 Ⅰ.2.管理項目追加 酒井 ADD END

            Dim db As New EBomDbClient
            Return db.QueryForList(Of LabelValueVo)(sql)
        End Function
        Public Function FindTsukurikataKatashiyou3LabelValues() As List(Of LabelValueVo) Implements MakeShisakuBlockDao.FindTsukurikataKatashiyou3LabelValues
            Dim sql As String = _
                "SELECT " _
                & " CAST(TSUKURIKATA_NO AS varchar) + '.' + TSUKURIKATA_NAME AS LABEL, " _
                & " CAST(TSUKURIKATA_NO AS varchar) + '.' + TSUKURIKATA_NAME AS VALUE " _
                & " FROM " & MBOM_DB_NAME & ".dbo.M_SHISAKU_TSUKURIKATA WITH (NOLOCK, NOWAIT) " _
                & " WHERE " _
                & " TSUKURIKATA_KBN = '3' " _
                & " ORDER BY " _
                & " TSUKURIKATA_NO "
            ''↓↓2014/09/02 Ⅰ.2.管理項目追加 酒井 ADD BEGIN
            '    & " TSUKURIKATA_NAME AS LABEL, " _
            '    & " TSUKURIKATA_NAME AS VALUE " _
            ''↑↑2014/09/02 Ⅰ.2.管理項目追加 酒井 ADD END

            Dim db As New EBomDbClient
            Return db.QueryForList(Of LabelValueVo)(sql)
        End Function
        ''↑↑2014/07/23 Ⅰ.2.管理項目追加_bt) (TES)張 ADD END

        ''↓↓2014/07/23 Ⅰ.2.管理項目追加_bw) (TES)張 ADD BEGIN
        ''' <summary>
        ''' 作り方コンボ表示内容取得処理
        ''' </summary>
        ''' <returns>作り方</returns>
        ''' <remarks></remarks>
        Public Function FindTsukurikataTiguLabelValues() As List(Of LabelValueVo) Implements MakeShisakuBlockDao.FindTsukurikataTiguLabelValues
            Dim sql As String = _
                "SELECT " _
                & " TSUKURIKATA_NAME AS LABEL, " _
                & " TSUKURIKATA_NAME AS VALUE " _
                & " FROM " & MBOM_DB_NAME & ".dbo.M_SHISAKU_TSUKURIKATA WITH (NOLOCK, NOWAIT) " _
                & " WHERE " _
                & " TSUKURIKATA_KBN = '4' " _
                & " ORDER BY " _
                & " TSUKURIKATA_NO "

            Dim db As New EBomDbClient
            Return db.QueryForList(Of LabelValueVo)(sql)
        End Function
        ''↑↑2014/07/23 Ⅰ.2.管理項目追加_bw) (TES)張 ADD END

    End Class
End Namespace