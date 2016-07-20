Imports FarPoint.Win.Spread.CellType
Imports FarPoint.Win.Spread
Imports EventSakusei.YosanSetteiBuhinEdit.Ui
Imports EventSakusei.YosanSetteiBuhinEdit

Public Class YosanSetteiBuhinEditSpreadUtil : Inherits ShisakuCommon.Ui.Spd.SpreadUtil

    ''' <summary>
    ''' セル右クリック時のイベントを追加する
    ''' </summary>
    ''' <param name="spread">追加対象のSpread</param>
    ''' <param name="inputSupport">入力サポートを使用していたら指定</param>
    ''' <remarks></remarks>
    Public Shared Sub AddEventCellRightClick(ByVal aFrmYosanSetteiBuhinEdit As frmDispYosanSetteiBuhinEdit, ByVal spread As FpSpread, ByVal inputSupport As YosanSetteiBuhinEditInputSupport)
        Dim clickEvent As New YosanSetteiBuhinEditRightClickEvent(spread, aFrmYosanSetteiBuhinEdit, inputSupport)
        AddHandler spread.CellClick, AddressOf clickEvent.Spread_CellClick
    End Sub

End Class
