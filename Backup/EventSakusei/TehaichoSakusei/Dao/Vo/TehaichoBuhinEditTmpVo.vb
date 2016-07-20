Imports ShisakuCommon.Db.EBom.Vo

'部品編集TMPと部品編集号車TMPの合体Vo'
Public Class TehaichoBuhinEditTmpVo : Inherits TShisakuBuhinEditTmpVo
    ''' <summary>試作号車表示順</summary>
    Private _ShisakuGousyaHyoujiJun As Nullable(Of Int32)
    ''' <summary>試作号車</summary>
    Private _ShisakuGousya As String
    ''' <summary>員数</summary>
    Private _InsuSuryo As Nullable(Of Int32)
    ''' <summary>INSTL品番表示順</summary>
    Private _InstlHinbanHyoujiJun As Nullable(Of Int32)
    ''' <summary>マージフラグ</summary>
    Private _MergeFlag As Boolean

    ''' <summary>試作号車表示順</summary>
    ''' <value>試作号車表示順</value>
    ''' <returns>試作号車表示順</returns>
    Public Property ShisakuGousyaHyoujiJun() As Nullable(Of Int32)
        Get
            Return _ShisakuGousyaHyoujiJun
        End Get
        Set(ByVal value As Nullable(Of Int32))
            _ShisakuGousyaHyoujiJun = value
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
    ''' <summary>員数</summary>
    ''' <value>員数</value>
    ''' <returns>員数</returns>
    Public Property InsuSuryo() As Nullable(Of Int32)
        Get
            Return _InsuSuryo
        End Get
        Set(ByVal value As Nullable(Of Int32))
            _InsuSuryo = value
        End Set
    End Property
    ''' <summary>INSTL品番表示順</summary>
    ''' <value>INSTL品番表示順</value>
    ''' <returns>INSTL品番表示順</returns>
    Public Property InstlHinbanHyoujiJun() As Nullable(Of Int32)
        Get
            Return _InstlHinbanHyoujiJun
        End Get
        Set(ByVal value As Nullable(Of Int32))
            _InstlHinbanHyoujiJun = value
        End Set
    End Property
    ''' <summary>マージフラグ</summary>
    ''' <value>マージフラグ</value>
    ''' <returns>マージフラグ</returns>
    Public Property MergeFlag() As Boolean
        Get
            Return _MergeFlag
        End Get
        Set(ByVal value As Boolean)
            _MergeFlag = value
        End Set
    End Property




End Class
