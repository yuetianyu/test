Imports FarPoint.Win.Spread.CellType
Imports FarPoint.Win.Spread
Imports EventSakusei.TehaichoEdit.Ui
Imports EventSakusei.TehaichoEdit

Public Class TehaichoEditSpreadUtil : Inherits ShisakuCommon.Ui.Spd.SpreadUtil

    ''' <summary>
    ''' セル右クリック時のイベントを追加する
    ''' </summary>
    ''' <param name="spread">追加対象のSpread</param>
    ''' <param name="inputSupport">入力サポートを使用していたら指定</param>
    ''' <remarks></remarks>
    Public Shared Sub AddEventCellRightClick(ByVal aFrmTehaichoEdit As frm20DispTehaichoEdit, ByVal spread As FpSpread, ByVal inputSupport As TehaichoeditInputSupport)
        Dim clickEvent As New TehaichoEditRightClickEvent(spread, aFrmTehaichoEdit, inputSupport)
        AddHandler spread.CellClick, AddressOf clickEvent.Spread_CellClick
    End Sub

End Class
