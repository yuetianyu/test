Imports EventSakusei.ShisakuBuhinEdit.KouseiBuhin.Dao.Vo

Namespace KouseiBuhin.Dao

    ''' <summary>
    ''' RHAC1320のイベント情報
    ''' </summary>
    ''' <remarks></remarks>
    Public Interface Rhac1320EventDao

        ''' <summary>
        ''' イベント情報を取得
        ''' </summary>
        ''' <param name="KaihatsuFugo"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Function GetByEventInfo(ByVal KaihatsuFugo As String) As List(Of Rhac1320EventVo)

    End Interface

End Namespace
