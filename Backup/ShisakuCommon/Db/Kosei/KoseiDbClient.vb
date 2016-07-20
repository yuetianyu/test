Imports EBom.Common

Namespace Db.Kosei
    ''' <summary>
    ''' KOSEI_DB用のDbClientクラス
    ''' </summary>
    ''' <remarks></remarks>
    Public Class KoseiDbClient : Inherits ShisakuDbClient
        ''' <summary>
        ''' コンストラクタ
        ''' </summary>
        ''' <remarks></remarks>
        Public Sub New()
            MyBase.New(g_kanrihyoIni(ShisakuGlobal.DB_KEY_KOSEI))
        End Sub
    End Class
End Namespace
