Imports ShisakuCommon.Db.EBom.Dao.Impl
Imports ShisakuCommon.Db.EBom.Dao
Imports ShisakuCommon.Db.EBom.Vo
Imports EventSakusei.EventEdit.Dao

Namespace ShisakuBuhinEditBlock.Logic
    ''' <summary>
    ''' 試作イベントコードを採番するクラス
    ''' </summary>
    ''' <remarks></remarks>
    Public Class NumberingShisakuEventCode
        Private ReadOnly shisakuEventDao As TShisakuEventDao
        Private ReadOnly shisakuKaihatsuFugo As String
        Private ReadOnly shisakuEventPhase As String
        Private ReadOnly unitKbn As String
        ''' <summary>
        ''' コンストラクタ
        ''' </summary>
        ''' <param name="ShisakuKaihatsuFugo">試作開発符号</param>
        ''' <param name="ShisakuEventPhase">試作イベントフェーズ</param>
        ''' <param name="UnitKbn">ユニット区分</param>
        ''' <remarks></remarks>
        Public Sub New(ByVal ShisakuKaihatsuFugo As String, ByVal ShisakuEventPhase As String, ByVal UnitKbn As String)
            Me.New(New TShisakuEventDaoImpl, ShisakuKaihatsuFugo, ShisakuEventPhase, UnitKbn)
        End Sub
        ''' <summary>
        ''' コンストラクタ
        ''' </summary>
        ''' <param name="ShisakuEventDao">試作イベントDao</param>
        ''' <param name="ShisakuKaihatsuFugo">試作開発符号</param>
        ''' <param name="ShisakuEventPhase">試作イベントフェーズ</param>
        ''' <param name="UnitKbn">ユニット区分</param>
        ''' <remarks></remarks>
        Public Sub New(ByVal ShisakuEventDao As TShisakuEventDao, ByVal ShisakuKaihatsuFugo As String, ByVal ShisakuEventPhase As String, ByVal UnitKbn As String)
            Me.shisakuEventDao = ShisakuEventDao
            Me.shisakuKaihatsuFugo = ShisakuKaihatsuFugo
            Me.shisakuEventPhase = ShisakuEventPhase
            Me.unitKbn = UnitKbn
        End Sub

        ''' <summary>
        ''' 次の試作イベントコードを返す
        ''' </summary>
        ''' <returns>次の試作イベントコード</returns>
        ''' <remarks></remarks>
        Public Function NextShisakuEventCode() As String
            Dim eventVo As New TShisakuEventVo
            Dim regImpl As EventEditRegistDao = New EventEditRegistDaoImpl
            eventVo = regImpl.FindMaxShisakuEventCode(shisakuKaihatsuFugo, shisakuEventPhase, unitKbn)

            Dim count As Integer = 0

            If Not eventVo Is Nothing Then
                count = Integer.Parse(Right(eventVo.ShisakuEventCode, 2))
            End If

            Return String.Format("{0}-{1}-{2}{3,2:00}", shisakuKaihatsuFugo, shisakuEventPhase, unitKbn, count + 1)
        End Function
    End Class
End Namespace