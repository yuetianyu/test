
Namespace TehaichoEdit
    '取引先CSV読込用のVOクラス'
    Public Class MakerCsvVo
        '' №
        Private _Num As String

        '' 会社名
        Private _CompName As String

        '' ﾒｰｶｰｺｰﾄﾞ
        Private _MakerCode As String

        '' 担当部署 
        Private _DeptName As String

        '' 担当者
        Private _MakerName As String

        ''' <summary>№</summary>
        ''' <value>№</value>
        ''' <returns>№</returns>
        Public Property Num() As String
            Get
                Return _Num
            End Get
            Set(ByVal value As String)
                _Num = value
            End Set
        End Property


        ''' <summary>会社名</summary>
        ''' <value>会社名</value>
        ''' <returns>会社名</returns>
        Public Property CompName() As String
            Get
                Return _CompName
            End Get
            Set(ByVal value As String)
                _CompName = value
            End Set
        End Property

        ''' <summary>ﾒｰｶｰｺｰﾄﾞ</summary>
        ''' <value>ﾒｰｶｰｺｰﾄﾞ</value>
        ''' <returns>ﾒｰｶｰｺｰﾄﾞ</returns>
        Public Property MakerCode() As String
            Get
                Return _MakerCode
            End Get
            Set(ByVal value As String)
                _MakerCode = value
            End Set
        End Property

        ''' <summary>担当部署</summary>
        ''' <value>担当部署</value>
        ''' <returns>担当部署</returns>
        Public Property DeptName() As String
            Get
                Return _DeptName
            End Get
            Set(ByVal value As String)
                _DeptName = value
            End Set
        End Property

        ''' <summary>担当者</summary>
        ''' <value>担当者</value>
        ''' <returns>担当者</returns>
        Public Property MakerName() As String
            Get
                Return _MakerName
            End Get
            Set(ByVal value As String)
                _MakerName = value
            End Set
        End Property

    End Class

End Namespace