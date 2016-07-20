Imports ShisakuCommon.Db.EBom.Vo

Namespace TehaichoMenu.Dao
    Public Class TeiseiTsuchiExcelVo : Inherits TShisakuTehaiTeiseiKihonVo
        'エクセル出力用にAS/400の部品情報を追加する'

        '' 発行No 
        Private _Cmba As String
        '' 発行No
        Private _Edono As String

        ''' <summary>取引先名称</summary>
        ''' <value>取引先名称</value>
        ''' <returns>取引先名称</returns>
        Public Property Cmba() As String
            Get
                Return _Cmba
            End Get
            Set(ByVal value As String)
                _Cmba = value
            End Set
        End Property



    End Class
End Namespace