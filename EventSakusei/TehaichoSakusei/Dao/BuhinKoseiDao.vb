Imports ShisakuCommon.Db.EBom.Vo
Imports EventSakusei.TehaichoSakusei.Dao

Namespace TehaichoSakusei.Dao
    ''' <summary>
    ''' 部品番号と開発符号を元に部品構成を取得する実験用のクラス
    ''' </summary>
    ''' <remarks>完成したら置き場所を変更する</remarks>
    Public Interface BuhinKoseiDao

#Region "パンダ(最も古い部品構成テーブル)"

        ''' <summary>
        ''' 部品構成を取得する(パンダ)
        ''' </summary>
        ''' <param name="buhinNoOya">部品番号</param>
        ''' <returns>該当レコード</returns>
        ''' <remarks></remarks>
        Function FindByRhac0551(ByVal buhinNoOya As String) As List(Of Rhac0551Vo)

        ''' <summary>
        ''' 部品属性を取得する(パンダ)
        ''' </summary>
        ''' <param name="buhinNo">部品番号</param>
        ''' <returns>該当レコード</returns>
        ''' <remarks></remarks>
        Function FindByRhac0530(ByVal buhinNo As String) As Rhac0530Vo

        ''' <summary>
        ''' 図面情報を取得する(パンダ)
        ''' </summary>
        ''' <param name="zumenNo">図面番号</param>
        ''' <returns>該当レコード</returns>
        ''' <remarks></remarks>
        Function FindByRhac0700(ByVal zumenNo As String) As List(Of Rhac0700Vo)

        ''' <summary>
        ''' 部品構成を取得する(部品番号(親)を取得)(パンダ)
        ''' </summary>
        ''' <param name="buhinNoKo">部品番号(子)</param>
        ''' <returns>該当レコード</returns>
        ''' <remarks></remarks>
        Function FindByRhac0551BuhinNoOya(ByVal buhinNoKo As String) As Rhac0551Vo

#End Region

#Region "図面テーブル(比較的最近の部品構成テーブル)"

        ''' <summary>
        ''' 部品構成を取得する(図面)
        ''' </summary>
        ''' <param name="buhinNoOya">部品番号(親)</param>
        ''' <returns>該当レコード</returns>
        ''' <remarks></remarks>
        Function FindByRhac0552(ByVal buhinNoOya As String) As List(Of Rhac0552Vo)

        ''' <summary>
        ''' 部品属性を取得する(図面)
        ''' </summary>
        ''' <param name="buhinNo">部品番号</param>
        ''' <returns>該当レコード</returns>
        ''' <remarks></remarks>
        Function FindByRhac0532(ByVal buhinNo As String) As Rhac0532Vo

        ''' <summary>
        ''' 図面情報を取得する(図面)
        ''' </summary>
        ''' <param name="zumenNo">図面番号</param>
        ''' <returns>該当レコード</returns>
        ''' <remarks></remarks>
        Function FindByRhac0702(ByVal zumenNo As String) As List(Of Rhac0702Vo)

        ''' <summary>
        ''' 部品構成を取得する(部品番号(親)を取得する)(図面)
        ''' </summary>
        ''' <param name="buhinNoKo">部品番号(子)</param>
        ''' <returns>該当レコード</returns>
        ''' <remarks></remarks>
        Function FindByRhac0552BuhinNoOya(ByVal buhinNoKo As String) As Rhac0552Vo

#End Region

#Region "FM5以降(最新の部品構成テーブル)"

        ''' <summary>
        ''' 部品構成を取得する(FM5以降)
        ''' </summary>
        ''' <param name="kaihatsuFugo">開発符号</param>
        ''' <param name="buhinNoOya">部品番号(親)</param>
        ''' <returns>該当レコード</returns>
        ''' <remarks></remarks>
        Function FindByRhac0553(ByVal kaihatsuFugo As String, ByVal buhinNoOya As String) As List(Of Rhac0553Vo)

        ''' <summary>
        ''' 部品構成を取得する(FM5以降)
        ''' </summary>
        ''' <param name="kaihatsuFugo">開発符号</param>
        ''' <param name="buhinNoKo">部品番号(子)</param>
        ''' <returns>該当レコード</returns>
        ''' <remarks></remarks>
        Function FindByRhac0553BuhinNoOya(ByVal kaihatsuFugo As String, ByVal buhinNoKo As String) As Rhac0553Vo

        ''' <summary>
        ''' 部品属性を取得する(FM5以降)
        ''' </summary>
        ''' <param name="buhinNo">部品番号</param>
        ''' <returns>該当レコード</returns>
        ''' <remarks></remarks>
        Function FindByRhac0533(ByVal buhinNo As String) As Rhac0533Vo

        ''' <summary>
        ''' 図面情報を取得する(FM5以降)
        ''' </summary>
        ''' <param name="zumenNo">図面番号</param>
        ''' <param name="kaihatsuFugo">開発符号</param>
        ''' <returns>該当レコード</returns>
        ''' <remarks></remarks>
        Function FindByRhac0703(ByVal zumenNo As String, ByVal kaihatsuFugo As String) As List(Of Rhac0703Vo)

        ''' <summary>
        ''' 部品構成テーブル(FM5以降)とリンクする属性情報を取得する(FM5以降)
        ''' </summary>
        ''' <param name="kaihatsuFugo">開発符号</param>
        ''' <param name="buhinNo">部品番号</param>
        ''' <returns>該当レコード</returns>
        ''' <remarks></remarks>
        Function FindByRhac1910(ByVal kaihatsuFugo As String, ByVal buhinNo As String) As List(Of Rhac1910Vo)

        ''' <summary>
        ''' 部品構成テーブル(FM5以降)とリンクする属性情報を取得する(FM5以降)
        ''' </summary>
        ''' <param name="kaihatsuFugo">開発符号</param>
        ''' <param name="buhinNo">部品番号</param>
        ''' <returns>該当レコード</returns>
        ''' <remarks></remarks>
        Function FindByRhac1920(ByVal kaihatsuFugo As String, ByVal buhinNo As String) As List(Of Rhac1920Vo)

        ''' <summary>
        ''' 部品構成テーブル(FM5以降)とリンクする属性情報を取得する(FM5以降)
        ''' </summary>
        ''' <param name="kaihatsuFugo">開発符号</param>
        ''' <param name="buhinNo">部品番号</param>
        ''' <returns>該当レコード</returns>
        ''' <remarks></remarks>
        Function FindByRhac1930(ByVal kaihatsuFugo As String, ByVal buhinNo As String) As List(Of Rhac1930Vo)

        ''' <summary>
        ''' 部品テーブル(FM5以降)とリンクする属性情報を取得する(FM5以降)
        ''' </summary>
        ''' <param name="buhinNo">部品番号</param>
        ''' <returns>該当レコード</returns>
        ''' <remarks></remarks>
        Function FindByRhac1940(ByVal buhinNo As String) As List(Of Rhac1940Vo)

#End Region

    End Interface
End Namespace