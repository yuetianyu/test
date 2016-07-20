Imports FarPoint.Win.Spread
Imports ShisakuCommon.Db.Impl
Imports ShisakuCommon.Db.EBom
Imports ShisakuCommon.Db.EBom.Dao.Impl
Imports ShisakuCommon.Db.EBom.Dao
Imports ShisakuCommon
Imports ShisakuCommon.Util
Imports ShisakuCommon.Db.EBom.Vo
Imports ShisakuCommon.Db

Namespace YosanSetteiBuhinMenu
    Public Class spdListObserver

        Private ReadOnly spread As FpSpread
        Private ReadOnly sheet As SheetView

        Public Sub New(ByVal spread As FpSpread, ByVal shisakuEventCode As String)
            Me.spread = spread
            If spread.Sheets.Count = 0 Then
                Throw New ArgumentException
            End If
            Me.sheet = spread.Sheets(0)

        End Sub

        'リストコードを取得する'
        Private Sub GetListCode(ByVal shisakuEventCode As String)

            '試作イベントコードを元にリストコードを探してくる'


            '適当につける'

        End Sub

    End Class
End Namespace