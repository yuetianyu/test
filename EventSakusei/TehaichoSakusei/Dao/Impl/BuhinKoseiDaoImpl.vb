Imports ShisakuCommon.Db
Imports ShisakuCommon.Db.EBom
Imports ShisakuCommon.Db.EBom.Vo.Helper
Imports ShisakuCommon.Db.Impl
Imports ShisakuCommon
Imports ShisakuCommon.Db.EBom.Vo
Imports ShisakuCommon.Db.EBom.Dao
Imports ShisakuCommon.Db.EBom.Dao.Impl
Imports EventSakusei.TehaichoSakusei.Dao


Namespace TehaichoSakusei.Dao
    ''' <summary>
    ''' 部品番号と開発符号を元に部品構成を取得する実験用のクラス
    ''' </summary>
    ''' <remarks>実装クラス</remarks>
    Public Class BuhinKoseiDaoImpl : Inherits DaoEachFeatureImpl
        Implements BuhinKoseiDao

        ''' <summary>
        ''' 部品属性を取得する(パンダ)
        ''' </summary>
        ''' <param name="buhinNo">部品番号</param>
        ''' <returns>該当レコード</returns>
        ''' <remarks></remarks>
        Public Function FindByRhac0530(ByVal buhinNo As String) As Rhac0530Vo Implements BuhinKoseiDao.FindByRhac0530
            Dim sql As String = _
            "SELECT * " _
            & " FROM  " _
            & " " & RHACLIBF_DB_NAME & ".dbo.RHAC0530 R " _
            & " WHERE " _
            & " R.BUHIN_NO = @BuhinNo " _
            & " AND R.HAISI_DATE = '99999999' " _
            & " AND R.KAITEI_NO = ( " _
            & " SELECT MAX ( CONVERT ( INT,COALESCE ( KAITEI_NO,'' ) ) ) AS KAITEI_NO " _
            & " FROM " & RHACLIBF_DB_NAME & ".dbo.RHAC0530 " _
            & " WHERE BUHIN_NO = R.BUHIN_NO ) "

            Dim db As New EBomDbClient
            Dim param As New Rhac0530Vo
            param.BuhinNo = buhinNo

            Return db.QueryForObject(Of Rhac0530Vo)(sql, param)
        End Function

        ''' <summary>
        ''' 部品属性を取得する(図面)
        ''' </summary>
        ''' <param name="buhinNo">部品番号</param>
        ''' <returns>該当レコード</returns>
        ''' <remarks></remarks>
        Public Function FindByRhac0532(ByVal buhinNo As String) As Rhac0532Vo Implements BuhinKoseiDao.FindByRhac0532
            Dim sql As String = _
            "SELECT * " _
            & " FROM  " _
            & " " & RHACLIBF_DB_NAME & ".dbo.RHAC0532 R " _
            & " WHERE " _
            & " R.BUHIN_NO = @BuhinNo " _
            & " AND R.HAISI_DATE = '99999999' " _
            & " AND R.KAITEI_NO = ( " _
            & " SELECT MAX ( CONVERT ( INT,COALESCE ( KAITEI_NO,'' ) ) ) AS KAITEI_NO " _
            & " FROM " & RHACLIBF_DB_NAME & ".dbo.RHAC0532 " _
            & " WHERE BUHIN_NO = R.BUHIN_NO ) "

            Dim db As New EBomDbClient
            Dim param As New Rhac0532Vo
            param.BuhinNo = buhinNo

            Return db.QueryForObject(Of Rhac0532Vo)(sql, param)
        End Function

        ''' <summary>
        ''' 部品属性を取得する(FM5以降)
        ''' </summary>
        ''' <param name="buhinNo">部品番号</param>
        ''' <returns>該当レコード</returns>
        ''' <remarks></remarks>
        Public Function FindByRhac0533(ByVal buhinNo As String) As Rhac0533Vo Implements BuhinKoseiDao.FindByRhac0533
            Dim sql As String = _
            "SELECT * " _
            & " FROM  " _
            & " " & RHACLIBF_DB_NAME & ".dbo.RHAC0533 R " _
            & " WHERE " _
            & " R.BUHIN_NO = @BuhinNo " _
            & " AND R.HAISI_DATE = '99999999' " _
            & " AND R.KAITEI_NO = ( " _
            & " SELECT MAX ( CONVERT ( INT,COALESCE ( KAITEI_NO,'' ) ) ) AS KAITEI_NO " _
            & " FROM " & RHACLIBF_DB_NAME & ".dbo.RHAC0533 " _
            & " WHERE BUHIN_NO = R.BUHIN_NO ) "

            Dim db As New EBomDbClient
            Dim param As New Rhac0533Vo
            param.BuhinNo = buhinNo

            Return db.QueryForObject(Of Rhac0533Vo)(sql, param)
        End Function

        ''' <summary>
        ''' 部品構成を取得する(パンダ)
        ''' </summary>
        ''' <param name="buhinNoOya">部品番号</param>
        ''' <returns>該当レコード</returns>
        ''' <remarks></remarks>
        Public Function FindByRhac0551(ByVal buhinNoOya As String) As List(Of Rhac0551Vo) Implements BuhinKoseiDao.FindByRhac0551
            Dim sql As String = _
            "SELECT * " _
            & " FROM  " _
            & " " & RHACLIBF_DB_NAME & ".dbo.RHAC0551 R " _
            & " WHERE " _
            & " R.BUHIN_NO_OYA = @BuhinNoOya " _
            & " AND R.HAISI_DATE = '99999999' " _
            & " AND R.KAITEI_NO = ( " _
            & " SELECT MAX ( CONVERT ( INT,COALESCE ( KAITEI_NO,'' ) ) ) AS KAITEI_NO " _
            & " FROM " & RHACLIBF_DB_NAME & ".dbo.RHAC0530 " _
            & " WHERE BUHIN_NO = R.BUHIN_NO ) "

            Dim db As New EBomDbClient
            Dim param As New Rhac0551Vo
            param.BuhinNoOya = buhinNoOya

            Return db.QueryForList(Of Rhac0551Vo)(sql, param)
        End Function

        ''' <summary>
        ''' 部品構成を取得する（親部品番号がほしい）(パンダ)
        ''' </summary>
        ''' <param name="buhinNoKo">部品番号(子)</param>
        ''' <returns>該当レコード</returns>
        ''' <remarks></remarks>
        Public Function FindByRhac0551BuhinNoOya(ByVal buhinNoKo As String) As Rhac0551Vo Implements BuhinKoseiDao.FindByRhac0551BuhinNoOya
            Dim sql As String = _
            "SELECT * " _
            & " FROM  " _
            & " " & RHACLIBF_DB_NAME & ".dbo.RHAC0551 R " _
            & " WHERE " _
            & " R.BUHIN_NO_KO = @BuhinNoKo " _
            & " AND R.HAISI_DATE = '99999999' " _
            & " AND R.KAITEI_NO = ( " _
            & " SELECT MAX ( CONVERT ( INT,COALESCE ( KAITEI_NO,'' ) ) ) AS KAITEI_NO " _
            & " FROM " & RHACLIBF_DB_NAME & ".dbo.RHAC0530 " _
            & " WHERE BUHIN_NO_KO = R.BUHIN_NO_KO  " _
            & " AND BUHIN_NO_OYA = R.BUHIN_NO_OYA ) "

            Dim db As New EBomDbClient
            Dim param As New Rhac0551Vo
            param.BuhinNoKo = buhinNoKo

            Return db.QueryForObject(Of Rhac0551Vo)(sql, param)
        End Function


        ''' <summary>
        ''' 部品構成を取得する(図面)
        ''' </summary>
        ''' <param name="buhinNoOya">部品番号(親)</param>
        ''' <returns>該当レコード</returns>
        ''' <remarks></remarks>
        Public Function FindByRhac0552(ByVal buhinNoOya As String) As List(Of Rhac0552Vo) Implements BuhinKoseiDao.FindByRhac0552
            Dim sql As String = _
            "SELECT * " _
            & " FROM  " _
            & " " & RHACLIBF_DB_NAME & ".dbo.RHAC0552 R " _
            & " WHERE " _
            & " R.BUHIN_NO_OYA = @BuhinNoOya " _
            & " AND R.HAISI_DATE = '99999999' " _
            & " AND R.KAITEI_NO = ( " _
            & " SELECT MAX ( CONVERT ( INT,COALESCE ( KAITEI_NO,'' ) ) ) AS KAITEI_NO " _
            & " FROM " & RHACLIBF_DB_NAME & ".dbo.RHAC0552 " _
            & " WHERE BUHIN_NO = R.BUHIN_NO ) "

            Dim db As New EBomDbClient
            Dim param As New Rhac0552Vo
            param.BuhinNoOya = buhinNoOya

            Return db.QueryForList(Of Rhac0552Vo)(sql, param)
        End Function

        ''' <summary>
        ''' 部品構成を取得する(部品番号(親)を取得)(図面)
        ''' </summary>
        ''' <param name="buhinNoKo">部品番号(子)</param>
        ''' <returns>該当レコード</returns>
        ''' <remarks></remarks>
        Public Function FindByRhac0552BuhinNoOya(ByVal buhinNoKo As String) As Rhac0552Vo Implements BuhinKoseiDao.FindByRhac0552BuhinNoOya
            Dim sql As String = _
            "SELECT * " _
            & " FROM  " _
            & " " & RHACLIBF_DB_NAME & ".dbo.RHAC0552 R " _
            & " WHERE " _
            & " R.BUHIN_NO_KO = @BuhinNoKo " _
            & " AND R.HAISI_DATE = '99999999' " _
            & " AND R.KAITEI_NO = ( " _
            & " SELECT MAX ( CONVERT ( INT,COALESCE ( KAITEI_NO,'' ) ) ) AS KAITEI_NO " _
            & " FROM " & RHACLIBF_DB_NAME & ".dbo.RHAC0552 " _
            & " WHERE BUHIN_NO_KO = R.BUHIN_NO_KO  " _
            & " AND BUHIN_NO_OYA = R.BUHIN_NO_OYA ) "

            Dim db As New EBomDbClient
            Dim param As New Rhac0552Vo
            param.BuhinNoKo = buhinNoKo

            Return db.QueryForObject(Of Rhac0552Vo)(sql, param)
        End Function

        ''' <summary>
        ''' 部品構成を取得する(FM5以降)
        ''' </summary>
        ''' <param name="kaihatsuFugo">開発符号</param>
        ''' <param name="buhinNoOya">部品番号(親)</param>
        ''' <returns>該当レコード</returns>
        ''' <remarks></remarks>
        Public Function FindByRhac0553(ByVal kaihatsuFugo As String, ByVal buhinNoOya As String) As List(Of Rhac0553Vo) Implements BuhinKoseiDao.FindByRhac0553
            Dim sql As String = _
            "SELECT * " _
            & " FROM  " _
            & " " & RHACLIBF_DB_NAME & ".dbo.RHAC0553 R " _
            & " WHERE " _
            & " R.BUHIN_NO_OYA = @BuhinNoOya " _
            & " AND R.KAIHATSUFUGO = @KaihatsuFugo " _
            & " AND R.HAISI_DATE = '99999999' " _
            & " AND R.KAITEI_NO = ( " _
            & " SELECT MAX ( CONVERT ( INT,COALESCE ( KAITEI_NO,'' ) ) ) AS KAITEI_NO " _
            & " FROM " & RHACLIBF_DB_NAME & ".dbo.RHAC0553 " _
            & " WHERE BUHIN_NO = R.BUHIN_NO ) "

            Dim db As New EBomDbClient
            Dim param As New Rhac0553Vo
            param.KaihatsuFugo = kaihatsuFugo
            param.BuhinNoOya = buhinNoOya

            Return db.QueryForList(Of Rhac0553Vo)(sql, param)
        End Function

        ''' <summary>
        ''' 部品構成を取得する(FM5以降)
        ''' </summary>
        ''' <param name="kaihatsuFugo">開発符号</param>
        ''' <param name="buhinNoKo">部品番号(子)</param>
        ''' <returns>該当レコード</returns>
        ''' <remarks></remarks>
        Public Function FindByRhac0553BuhinNoOya(ByVal kaihatsuFugo As String, ByVal buhinNoKo As String) As Rhac0553Vo Implements BuhinKoseiDao.FindByRhac0553BuhinNoOya
            Dim sql As String = _
            "SELECT * " _
            & " FROM  " _
            & " " & RHACLIBF_DB_NAME & ".dbo.RHAC0553 R " _
            & " WHERE " _
            & " R.BUHIN_NO_KO = @BuhinNoKo " _
            & " AND R.KAIHATSU_FUGO = @KaihatsuFugo " _
            & " AND R.HAISI_DATE = '99999999' " _
            & " AND R.KAITEI_NO = ( " _
            & " SELECT MAX ( CONVERT ( INT,COALESCE ( KAITEI_NO,'' ) ) ) AS KAITEI_NO " _
            & " FROM " & RHACLIBF_DB_NAME & ".dbo.RHAC0553 " _
            & " WHERE BUHIN_NO_KO = R.BUHIN_NO_KO " _
            & " AND BUHIN_NO_OYA = R.BUHIN_NO_OYA " _
            & " AND KAIHATSU_FUGO = R.KAIHATSU_FUGO ) "

            Dim db As New EBomDbClient
            Dim param As New Rhac0553Vo
            param.KaihatsuFugo = kaihatsuFugo
            param.BuhinNoKo = buhinNoKo

            Return db.QueryForObject(Of Rhac0553Vo)(sql, param)
        End Function

        ''' <summary>
        ''' 図面情報を取得する(パンダ)
        ''' </summary>
        ''' <param name="zumenNo">図面番号</param>
        ''' <returns>該当レコード</returns>
        ''' <remarks></remarks>
        Public Function FindByRhac0700(ByVal zumenNo As String) As List(Of Rhac0700Vo) Implements BuhinKoseiDao.FindByRhac0700
            Dim sql As String = _
            "SELECT * " _
            & " FROM  " _
            & " " & RHACLIBF_DB_NAME & ".dbo.RHAC0700 R " _
            & " WHERE " _
            & " R.ZUMEN_NO = @ZumenNo " _
            & " AND R.HAISI_DATE = '99999999' " _
            & " AND R.ZUMEN_KAITEI_NO = ( " _
            & " SELECT MAX ( CONVERT ( INT,COALESCE ( ZUMEN_KAITEI_NO,'' ) ) ) AS ZUMEN_KAITEI_NO " _
            & " FROM " & RHACLIBF_DB_NAME & ".dbo.RHAC0700 " _
            & " WHERE ZUMEN_NO = R.ZUMEN_NO ) "

            Dim db As New EBomDbClient
            Dim param As New Rhac0700Vo
            param.ZumenNo = zumenNo

            Return db.QueryForList(Of Rhac0700Vo)(sql, param)
        End Function

        ''' <summary>
        ''' 図面情報を取得する(図面)
        ''' </summary>
        ''' <param name="zumenNo">図面番号</param>
        ''' <returns>該当レコード</returns>
        ''' <remarks></remarks>
        Public Function FindByRhac0702(ByVal zumenNo As String) As List(Of Rhac0702Vo) Implements BuhinKoseiDao.FindByRhac0702
            Dim sql As String = _
            "SELECT * " _
            & " FROM  " _
            & " " & RHACLIBF_DB_NAME & ".dbo.RHAC0702 R " _
            & " WHERE " _
            & " R.ZUMEN_NO = @ZumenNo " _
            & " AND R.HAISI_DATE = '99999999' " _
            & " AND R.ZUMEN_KAITEI_NO = ( " _
            & " SELECT MAX ( CONVERT ( INT,COALESCE ( ZUMEN_KAITEI_NO,'' ) ) ) AS ZUMEN_KAITEI_NO " _
            & " FROM " & RHACLIBF_DB_NAME & ".dbo.RHAC0702 " _
            & " WHERE ZUMEN_NO = R.ZUMEN_NO ) "

            Dim db As New EBomDbClient
            Dim param As New Rhac0702Vo
            param.ZumenNo = zumenNo

            Return db.QueryForList(Of Rhac0702Vo)(sql, param)
        End Function

        ''' <summary>
        ''' 図面情報を取得する(FM5以降)
        ''' </summary>
        ''' <param name="zumenNo">図面番号</param>
        ''' <param name="kaihatsuFugo">開発符号</param>
        ''' <returns>該当レコード</returns>
        ''' <remarks></remarks>
        Public Function FindByRhac0703(ByVal zumenNo As String, ByVal kaihatsuFugo As String) As List(Of Rhac0703Vo) Implements BuhinKoseiDao.FindByRhac0703
            Dim sql As String = _
            "SELECT * " _
            & " FROM  " _
            & " " & RHACLIBF_DB_NAME & ".dbo.RHAC0703 R " _
            & " WHERE " _
            & " R.ZUMEN_NO = @ZumenNo " _
            & " AND R.KAIHATSUFUGO = @KaihatsuFugo " _
            & " AND R.HAISI_DATE = '99999999' " _
            & " AND R.ZUMEN_KAITEI_NO = ( " _
            & " SELECT MAX ( CONVERT ( INT,COALESCE ( ZUMEN_KAITEI_NO,'' ) ) ) AS ZUMEN_KAITEI_NO " _
            & " FROM " & RHACLIBF_DB_NAME & ".dbo.RHAC0703 " _
            & " WHERE ZUMEN_NO = R.ZUMEN_NO " _
            & " AND KAIHATSUFUGO = R.KAIHATSUFUGO ) "

            Dim db As New EBomDbClient
            Dim param As New Rhac0703Vo
            param.KaihatsuFugo = kaihatsuFugo
            param.ZumenNo = zumenNo

            Return db.QueryForList(Of Rhac0703Vo)(sql, param)
        End Function

        ''' <summary>
        ''' 部品構成テーブル(FM5以降)とリンクする属性情報を取得する(FM5以降)
        ''' </summary>
        ''' <param name="kaihatsuFugo">開発符号</param>
        ''' <param name="buhinNo">部品番号</param>
        ''' <returns>該当レコード</returns>
        ''' <remarks></remarks>
        Public Function FindByRhac1910(ByVal kaihatsuFugo As String, ByVal buhinNo As String) As List(Of Rhac1910Vo) Implements BuhinKoseiDao.FindByRhac1910
            Dim sql As String = _
            "SELECT * " _
            & " FROM  " _
            & " " & RHACLIBF_DB_NAME & ".dbo.RHAC1910 R " _
            & " WHERE " _
            & " R.BUHIN_NO = @BuhinNo " _
            & " AND R.KAIHATSUFUGO = @KaihatsuFugo " _
            & " AND R.HAISI_DATE = '99999999' " _
            & " AND R.BLK_BUHIN_KAITEI_NO = ( " _
            & " SELECT MAX ( CONVERT ( INT,COALESCE ( BLK_BUHIN_KAITEI_NO,'' ) ) ) AS BLK_BUHIN_KAITEI_NO " _
            & " FROM " & RHACLIBF_DB_NAME & ".dbo.RHAC1910 " _
            & " WHERE BUHIN_NO = R.BUHIN_NO " _
            & " AND KAIHATSUFUGO = R.KAIHATSUFUGO ) "

            Dim db As New EBomDbClient
            Dim param As New Rhac1910Vo
            param.KaihatsuFugo = kaihatsuFugo
            param.BuhinNo = buhinNo

            Return db.QueryForList(Of Rhac1910Vo)(sql, param)
        End Function

        ''' <summary>
        ''' 部品構成テーブル(FM5以降)とリンクする属性情報を取得する(FM5以降)
        ''' </summary>
        ''' <param name="kaihatsuFugo">開発符号</param>
        ''' <param name="buhinNo">部品番号</param>
        ''' <returns>該当レコード</returns>
        ''' <remarks></remarks>
        Public Function FindByRhac1920(ByVal kaihatsuFugo As String, ByVal buhinNo As String) As List(Of Rhac1920Vo) Implements BuhinKoseiDao.FindByRhac1920
            Dim sql As String = _
            "SELECT * " _
            & " FROM  " _
            & " " & RHACLIBF_DB_NAME & ".dbo.RHAC1920 R " _
            & " WHERE " _
            & " R.BUHIN_NO = @BuhinNo " _
            & " AND R.KAIHATSUFUGO = @KaihatsuFugo " _
            & " AND R.HAISI_DATE = '99999999' " _
            & " AND R.BLK_BUHIN_KAITEI_NO = ( " _
            & " SELECT MAX ( CONVERT ( INT,COALESCE ( BLK_BUHIN_KAITEI_NO,'' ) ) ) AS BLK_BUHIN_KAITEI_NO " _
            & " FROM " & RHACLIBF_DB_NAME & ".dbo.RHAC1920 " _
            & " WHERE BUHIN_NO = R.BUHIH_NO " _
            & " AND KAIHATSUFUGO = R.KAIHATSUFUGO ) "

            Dim db As New EBomDbClient
            Dim param As New Rhac1920Vo
            param.KaihatsuFugo = kaihatsuFugo
            param.BuhinNo = buhinNo

            Return db.QueryForList(Of Rhac1920Vo)(sql, param)
        End Function

        ''' <summary>
        ''' 部品構成テーブル(FM5以降)とリンクする属性情報を取得する(FM5以降)
        ''' </summary>
        ''' <param name="kaihatsuFugo">開発符号</param>
        ''' <param name="buhinNo">部品番号</param>
        ''' <returns>該当レコード</returns>
        ''' <remarks></remarks>
        Public Function FindByRhac1930(ByVal kaihatsuFugo As String, ByVal buhinNo As String) As List(Of Rhac1930Vo) Implements BuhinKoseiDao.FindByRhac1930
            Dim sql As String = _
            "SELECT * " _
            & " FROM  " _
            & " " & RHACLIBF_DB_NAME & ".dbo.RHAC1930 R " _
            & " WHERE " _
            & " R.BUHIN_NO = @BuhinNo " _
            & " R.KAIHATSUFUGO = @KaihatsuFugo " _
            & " AND R.HAISI_DATE = '99999999' " _
            & " AND R.BUHIN_KATA_KAITEI_NO = ( " _
            & " SELECT MAX ( CONVERT ( INT,COALESCE ( BUHIN_KATA_KAITEI_NO,'' ) ) ) AS BUHIN_KATA_KAITEI_NO " _
            & " FROM " & RHACLIBF_DB_NAME & ".dbo.RHAC1930 " _
            & " WHERE BUHIN_NO = R.BUHIN_NO " _
            & " AND KAIHATSUFUGO = R.KAIHATSUFUGO ) "

            Dim db As New EBomDbClient
            Dim param As New Rhac1930Vo
            param.KaihatsuFugo = kaihatsuFugo
            param.BuhinNo = buhinNo

            Return db.QueryForList(Of Rhac1930Vo)(sql, param)
        End Function

        ''' <summary>
        ''' 部品テーブル(FM5以降)とリンクする属性情報を取得する(FM5以降)
        ''' </summary>
        ''' <param name="kataDaihyouBuhinNo">型代表部品番号</param>
        ''' <returns>該当レコード</returns>
        ''' <remarks></remarks>
        Public Function FindByRhac1940(ByVal kataDaihyouBuhinNo As String) As System.Collections.Generic.List(Of ShisakuCommon.Db.EBom.Vo.Rhac1940Vo) Implements BuhinKoseiDao.FindByRhac1940
            Dim sql As String = _
            "SELECT * " _
            & " FROM  " _
            & " " & RHACLIBF_DB_NAME & ".dbo.RHAC1940 R " _
            & " WHERE " _
            & " R.KATA_DAIHYOU_BUHIN_NO = @KataDaihyouBuhinNo " _
            & " AND R.HAISI_DATE = '99999999' " _
            & " AND R.KATA_KAITEI_NO = ( " _
            & " SELECT MAX ( CONVERT ( INT,COALESCE ( KATA_KAITEI_NO,'' ) ) ) AS KATA_KAITEI_NO " _
            & " FROM " & RHACLIBF_DB_NAME & ".dbo.RHAC1940 " _
            & " WHERE KATA_DAIHYOU_BUHIN_NO = R.KATA_DAIHYOU_BUHIN_NO )"

            Dim db As New EBomDbClient
            Dim param As New Rhac1940Vo
            param.KataDaihyouBuhinNo = kataDaihyouBuhinNo

            Return db.QueryForList(Of Rhac1940Vo)(sql, param)
        End Function

    End Class

End Namespace