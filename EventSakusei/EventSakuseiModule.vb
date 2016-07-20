Module EventSakuseiModule
    Public s As String
    Public s1 As String

    Public cr As FarPoint.Win.Spread.Model.CellRange

    Public frm7Para As String
    Public frm7ParaModori As String

    Public frm35Para As String
    Public frm35ParaModori As String

    Public frm37Para As String
    Public frm37ParaModori As String

    Public frm38Para As String
    Public frm38ParaModori As String

    'Public frm41Para As String
    'Public frm41ParaModori As String

    'Public frm8Para As String
    'Public frm8ParaModori As String

    'Public frm9Para As String
    'Public frm9ParaModori As String
    'Public frm9ParaDBStatus As String 'DBステータスを保持する。保存済み、登録済みなどを区分で。
    '--------------------------------------------
    '２次改修で追加
    '前回入力列を保持する。
    Public ParaBasicOptionCol As Integer
    Public ParaBasicOptionFlg As String
    Public ParaOptionCol As Integer
    Public ParaOptionFlg As String
    '--------------------------------------------

    'Public frm10Para As String
    'Public frm10ParaModori As String

    Public frm18Para As String
    Public frm18ParaModori As String

    Public frm19Para As String
    Public frm19ParaModori As String

    Public frm20Para As String
    Public frm20ParaModori As String

    Public frm21Para As String
    Public frm21ParaModori As String

    Public frm29Para As String
    Public frm29ParaModori As String

    Public pathname As String
    Public ParaStatus As String 'ステータスを保持する。実装時はコードだが紙芝居では名称で。

    '色を返す。
    Public paraCOLOR As String

    Public ParaActRowIdx As Integer
    Public ParaActColIdx As Integer

End Module
