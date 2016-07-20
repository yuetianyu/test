Imports ShisakuCommon.Db.EBom.Vo

Namespace TehaichoMenu.Vo
    Public Class TShisakuBuhinEditVoHelperExcel : Inherits TShisakuBuhinEditVo
        ''' <summary>INSTL品番表示順</summary>
        Private _InstlHinbanHyoujiJun As String

        ''' <summary>試作号車</summary>
        Private _ShisakuGousya As String

        ''' <summary>員数数量</summary>
        Private _InsuSuryo As Integer

        ''' <summary>号車表示順</summary>
        Private _HyojijunNo As Integer

        ''' <summary>担当ユーザーID</summary>
        Private _UserId As String

        ''' <summary>電話番号</summary>
        Private _TelNo As String

        ''' <summary>フラグ</summary>
        Private _Flag As Boolean


        Private _BaseInstlFlg As String

        Private _ShisakuGousyaHyoujiJun As Integer

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

        ''' <summary>INSTL品番表示順</summary>
        ''' <value>INSTL品番表示順</value>
        ''' <returns>INSTL品番表示順</returns>
        Public Property InstlHinbanHyoujiJun() As String
            Get
                Return _InstlHinbanHyoujiJun
            End Get
            Set(ByVal value As String)
                _InstlHinbanHyoujiJun = value
            End Set
        End Property

        ''' <summary>試作号車</summary>
        ''' <value>試作号車</value>
        ''' <returns>試作号車</returns>
        Public Property ShisakuGousya() As String
            Get
                Return _ShisakuGousya
            End Get
            Set(ByVal value As String)
                _ShisakuGousya = value
            End Set
        End Property

        ''' <summary>担当ユーザーID</summary>
        ''' <value>担当ユーザーID</value>
        ''' <returns>担当ユーザーID</returns>
        Public Property UserId() As String
            Get
                Return _UserId
            End Get
            Set(ByVal value As String)
                _UserId = value
            End Set
        End Property

        ''' <summary>電話番号</summary>
        ''' <value>電話番号</value>
        ''' <returns>電話番号</returns>
        Public Property TelNo() As String
            Get
                Return _TelNo
            End Get
            Set(ByVal value As String)
                _TelNo = value
            End Set
        End Property

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

        ''' <summary>号車表示順</summary>
        ''' <value>号車表示順</value>
        ''' <returns>号車表示順</returns>
        Public Property HyojijunNo() As Integer
            Get
                Return _HyojijunNo
            End Get
            Set(ByVal value As Integer)
                _HyojijunNo = value
            End Set
        End Property

        Public Property Flag() As Boolean
            Get
                Return _Flag
            End Get
            Set(ByVal value As Boolean)
                _Flag = value
            End Set
        End Property

        ''' <summary>号車表示順</summary>
        ''' <value>号車表示順</value>
        ''' <returns>号車表示順</returns>
        Public Property ShisakuGousyaHyoujiJun() As Integer
            Get
                Return _ShisakuGousyaHyoujiJun
            End Get
            Set(ByVal value As Integer)
                _ShisakuGousyaHyoujiJun = value
            End Set
        End Property

        Public Shared Function Comparison(ByVal x As TShisakuBuhinEditVoHelperExcel, ByVal y As TShisakuBuhinEditVoHelperExcel) As Integer
            Dim rtn As Integer = 0
            rtn = x.ShisakuBlockNo.CompareTo(y.ShisakuBlockNo)
            If rtn = 0 Then rtn = x.BuhinNoHyoujiJun - y.BuhinNoHyoujiJun
            Return rtn
        End Function

    End Class
End Namespace
