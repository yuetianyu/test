Imports ShisakuCommon.Db.EBom.Vo

Namespace ShisakuBuhinExcel.Vo
    Public Class BuhinEditVoHelper : Inherits TShisakuBuhinEditVo

        Private _InsuSuryo As Integer
        Private _HyojijunNo As Integer
        Private _InstlHinbanHyoujiJun As Integer
        Private _BaseInstlFlg As String

        ''' <summary>員数数量</summary>
        ''' <value>員数数量</value>
        ''' <returns>員数数量</returns>
        Public Property InsuSuryo() As Integer
            Get
                Return _InsuSuryo
            End Get
            Set(ByVal value As Integer)
                _InsuSuryo = value
            End Set
        End Property

        ''' <summary>試作号車表示順</summary>
        ''' <value>試作号車表示順</value>
        ''' <returns>試作号車表示順</returns>
        Public Property HyojijunNo() As Integer
            Get
                Return _HyojijunNo
            End Get
            Set(ByVal value As Integer)
                _HyojijunNo = value
            End Set
        End Property

        ''' <summary>INSTL表示順</summary>
        ''' <value>INSTL表示順</value>
        ''' <returns>INSTL表示順</returns>
        Public Property InstlHinbanHyoujiJun() As Integer
            Get
                Return _InstlHinbanHyoujiJun
            End Get
            Set(ByVal value As Integer)
                _InstlHinbanHyoujiJun = value
            End Set
        End Property

        ''' <summary>INSTLフラグ</summary>
        ''' <value>INSTLフラグ</value>
        ''' <returns>INSTLフラグ</returns>
        Public Property BaseInstlFlg() As String
            Get
                Return _BaseInstlFlg
            End Get
            Set(ByVal value As String)
                _BaseInstlFlg = value
            End Set
        End Property

        ''' <summary>
        ''' リストソート用Comparison
        ''' </summary>
        ''' <param name="x">比較元</param>
        ''' <param name="y">比較先</param>
        ''' <returns>比較結果</returns>
        ''' <remarks>2014/12/17 新規：生方</remarks>
        Public Shared Function Comparison(ByVal x As BuhinEditVoHelper, ByVal y As BuhinEditVoHelper) As Integer
            Dim rtn As Integer = 0
            rtn = x.ShisakuBlockNo.CompareTo(y.ShisakuBlockNo)
            If rtn = 0 Then rtn = x.BuhinNoHyoujiJun - y.BuhinNoHyoujiJun
            If y.BaseInstlFlg IsNot Nothing Then
                If rtn = 0 Then rtn = y.BaseInstlFlg.CompareTo(x.BaseInstlFlg)
            End If
            Return rtn
        End Function

    End Class
End Namespace