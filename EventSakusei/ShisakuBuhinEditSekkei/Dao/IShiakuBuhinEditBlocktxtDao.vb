Imports ShisakuCommon.Db.EBom.Vo

Namespace ShisakuBuhinEditSekkei.Dao

    Public Interface IShiakuBuhinEditBlocktxtDao
        ''' <summary>
        ''' 試作部品表編集・改定編集（ブロック）取得する
        ''' </summary>
        ''' <param name="txt">設計課</param>
        ''' <returns>部課コード</returns>
        ''' <remarks></remarks>
        Function GetBuKaCode(ByVal txt As String) As Rhac1560Vo

        ''' <summary>
        ''' ブロック情報を取得する
        ''' </summary>
        ''' <param name="ShisakuEventCode">試作イベントコード</param>
        ''' <param name="ShisakuBukaCode">部課コード</param>
        ''' <returns>ブロック情報</returns>
        ''' <remarks></remarks>
        Function GetBlock(ByVal ShisakuEventCode As String, ByVal ShisakuBukaCode As String) As List(Of TShisakuSekkeiBlockVo)

    End Interface
End Namespace