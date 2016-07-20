Imports ShisakuCommon.Db.Impl
Imports ShisakuCommon.Db

''' <summary>
''' 試作システムの標準日時クラス
''' </summary>
''' <remarks></remarks>
Public Class ShisakuDate
    Private _shisakuDao As ShisakuDao
    Private _currentDateTime As Nullable(Of DateTime)
    ''' <summary>
    ''' コンストラクタ
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub New()
        _shisakuDao = New ShisakuDaoImpl
    End Sub
    ''' <summary>
    ''' コンストラクタ
    ''' </summary>
    ''' <param name="shisakuDao"></param>
    ''' <remarks></remarks>
    Public Sub New(ByVal shisakuDao As ShisakuDao)
        _shisakuDao = shisakuDao
    End Sub

    ''' <summary>
    ''' 現在日時を返す
    ''' </summary>
    ''' <returns>現在日時</returns>
    ''' <remarks></remarks>
    Public ReadOnly Property CurrentDateTime() As DateTime
        Get
            If _currentDateTime Is Nothing Then
                _currentDateTime = _shisakuDao.GetCurrentTimestamp
            End If
            Return _currentDateTime
        End Get
    End Property

    ''' <summary>
    ''' 現在日付をDB書式にして返す
    ''' </summary>
    ''' <returns>現在日付(DB書式)</returns>
    ''' <remarks></remarks>
    Public ReadOnly Property CurrentDateDbFormat() As String
        Get
            Return CurrentDateTime.ToString("yyyy-MM-dd")
        End Get
    End Property

    ''' <summary>
    ''' 現在時刻をDB書式にして返す
    ''' </summary>
    ''' <returns>現在時刻(DB書式)</returns>
    ''' <remarks></remarks>
    Public ReadOnly Property CurrentTimeDbFormat() As String
        Get
            Return CurrentDateTime.ToString("HH:mm:ss")
        End Get
    End Property

    ''' <summary>
    ''' 標準日時をクリアし、再度取得することを促す
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub Clear()
        _currentDateTime = Nothing
    End Sub
End Class
