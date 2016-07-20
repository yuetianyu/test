Imports ShisakuCommon.Db.EBom.Vo

Namespace ShisakuBuhinEditSekkei.Dao

    Public Interface IShisakuBuhinEditBlockDao
        ''' <summary>
        ''' 試作部品表編集・改定編集（ブロック）課別状況取得する
        ''' </summary>
        ''' <param name="eventCode">イベントコード</param>
        ''' <returns>進度状況合計</returns>
        ''' <remarks></remarks>
        Function GetKabetuJyoutai(ByVal eventCode As String) As String
        Function GetTotalJyoutai(ByVal eventCode As String) As KabetuJyoutaiVo


        ''' <summary>
        ''' RHAC1860カレンダーマスタ
        ''' </summary>
        ''' <param name="Tojitu">当日</param>
        ''' <param name="SyochiKigenbi">処置期限日</param>
        ''' <returns>稼働日合計</returns>
        ''' <remarks></remarks>
        Function GetKadoubi(ByVal Tojitu As Integer, ByVal SyochiKigenbi As Integer) As Rhac1860VoHelper


    End Interface
End Namespace


