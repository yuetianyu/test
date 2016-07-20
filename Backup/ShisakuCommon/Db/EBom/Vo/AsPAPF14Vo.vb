Namespace Db.EBom.Vo
    ''' <summary>
    ''' 新調達取引先マスタ情報
    ''' </summary>
    ''' <remarks></remarks>
    Public Class AsPAPF14Vo
        '' 取引先コード  
        Private _Toricd As String
        '' 取引先名称
        Private _Torinm As String
        '' 第一購坦
        Private _Koutan1 As String
        '' 第二購坦
        Private _Koutan2 As String
        '' 支払区分
        Private _Shihark As String
        '' 取引先名称
        Private _Trnmknj As String
        '' 作成日
        Private _Sakuymd As String
        '' 第三購坦
        Private _Koutan3 As String

        ''' <summary>取引先コード</summary>
        ''' <value>取引先コード</value>
        ''' <returns>取引先コード</returns>
        Public Property Toricd() As String
            Get
                Return _Toricd
            End Get
            Set(ByVal value As String)
                _Toricd = value
            End Set
        End Property

        ''' <summary>取引先名称</summary>
        ''' <value>取引先名称</value>
        ''' <returns>取引先名称</returns>
        Public Property Torinm() As String
            Get
                Return _Torinm
            End Get
            Set(ByVal value As String)
                _Torinm = value
            End Set
        End Property

        ''' <summary>第一購坦</summary>
        ''' <value>第一購坦</value>
        ''' <returns>第一購坦</returns>
        Public Property Koutan1() As String
            Get
                Return _Koutan1
            End Get
            Set(ByVal value As String)
                _Koutan1 = value
            End Set
        End Property

        ''' <summary>第二購坦</summary>
        ''' <value>第二購坦</value>
        ''' <returns>第二購坦</returns>
        Public Property Koutan2() As String
            Get
                Return _Koutan2
            End Get
            Set(ByVal value As String)
                _Koutan2 = value
            End Set
        End Property

        ''' <summary>支払区分</summary>
        ''' <value>支払区分</value>
        ''' <returns>支払区分</returns>
        Public Property Shihark() As String
            Get
                Return _Shihark
            End Get
            Set(ByVal value As String)
                _Shihark = value
            End Set
        End Property

        ''' <summary>取引先名称</summary>
        ''' <value>取引先名称</value>
        ''' <returns>取引先名称</returns>
        Public Property Trnmknj() As String
            Get
                Return _Trnmknj
            End Get
            Set(ByVal value As String)
                _Trnmknj = value
            End Set
        End Property

        ''' <summary>作成日</summary>
        ''' <value>作成日</value>
        ''' <returns>作成日</returns>
        Public Property Sakuymd() As String
            Get
                Return _Sakuymd
            End Get
            Set(ByVal value As String)
                _Sakuymd = value
            End Set
        End Property

        ''' <summary>第三購坦</summary>
        ''' <value>第三購坦</value>
        ''' <returns>第三購坦</returns>
        Public Property Koutan3() As String
            Get
                Return _Koutan3
            End Get
            Set(ByVal value As String)
                _Koutan3 = value
            End Set
        End Property

    End Class
End Namespace