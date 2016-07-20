Imports ShisakuCommon.Db.EBom.Vo

'号車と員数'
Public Class EditBlockExcelBlockInstlVoHelper : Inherits TShisakuSekkeiBlockInstlVo
    '' 部品番号表示順
    Private _BuhinNoHyoujiJun As String
    '' 部品員数数量
    Private _BuhinInsuSuryo As Integer
    '' 号車表示順No
    Private _HyojijunNo As Integer


    ''' <summary>部品番号表示順</summary>
    ''' <value>部品番号表示順</value>
    ''' <returns>部品番号表示順</returns>
    Public Property BuhinNoHyoujiJun() As String
        Get
            Return _BuhinNoHyoujiJun
        End Get
        Set(ByVal value As String)
            _BuhinNoHyoujiJun = value
        End Set
    End Property


    ''' <summary>部品員数数量</summary>
    ''' <value>部品員数数量</value>
    ''' <returns>部品員数数量</returns>
    Public Property BuhinInsuSuryo() As Integer
        Get
            Return _BuhinInsuSuryo
        End Get
        Set(ByVal value As Integer)
            _BuhinInsuSuryo = value
        End Set
    End Property

    ''' <summary>号車表示順No</summary>
    ''' <value>号車表示順No</value>
    ''' <returns>号車表示順No</returns>
    Public Property HyojijunNo() As Integer
        Get
            Return _HyojijunNo
        End Get
        Set(ByVal value As Integer)
            _HyojijunNo = value
        End Set
    End Property


End Class
