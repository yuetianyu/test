Imports EBom.Common

Namespace Db.EBom
    ''' <summary>
    ''' EBOM_DB用のDbClientクラス
    ''' </summary>
    ''' <remarks></remarks>
    Public Class EBomDbClient : Inherits ShisakuDbClient
        ''' <summary>
        ''' コンストラクタ
        ''' </summary>
        ''' <remarks></remarks>
        Public Sub New()
            MyBase.New(g_kanrihyoIni(ShisakuGlobal.DB_KEY_EBOM))
        End Sub
    End Class
End Namespace
