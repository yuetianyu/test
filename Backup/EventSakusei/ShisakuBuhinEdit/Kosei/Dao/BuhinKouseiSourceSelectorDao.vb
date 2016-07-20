Imports ShisakuCommon.Db.EBom.Vo

Namespace ShisakuBuhinEdit.Kosei.Dao
    Public interface BuhinKouseiSourceSelectorDao

        ''' <summary>
        ''' EBOMデータに構成が存在するかどうか取得する
        ''' </summary>
        ''' <param name="instlNinban">INSTL品番</param>
        ''' <returns>True:存在する False:存在しない</returns>
        ''' <remarks></remarks>
        Function FindByDataEbom(ByVal instlNinban As String) As Boolean
        ''' <summary>
        ''' 試作側にイベントが存在する場合そのイベントコードを取得する
        ''' </summary>
        ''' <param name="instlNinban">INSTL品番</param>
        ''' <returns>イベントコードのリスト</returns>
        ''' <remarks></remarks>
        Function FindByDataShisaku(ByVal instlNinban As String) As List(Of String)

    End Interface
End NameSpace