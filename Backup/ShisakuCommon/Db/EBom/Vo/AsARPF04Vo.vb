Namespace Db.EBom.Vo
    ''' <summary>
    ''' 手配記号マスター情報
    ''' </summary>
    ''' <remarks></remarks>
    Public Class AsARPF04Vo
        '' B/U区分  
        Private _BUKbn As Char
        '' 記号ID  
        Private _Kgba As String
        '' 記号区分  
        Private _Kgkm As Char
        '' 手配記号
        Private _Tikg As String
        '' 内容40  
        Private _Ny40 As String
        '' 製品区分名称  
        Private _Zmmu As String
        '' 供給セクション  
        Private _Kysx As String
        '' 納入場所 
        Private _Nobs As String
        '' B/U区分  
        Private _Cekm As Char
        '' 製品区分  
        Private _Prkm As Char

        ''' <summary>B/U区分</summary>
        ''' <value>B/U</value>
        ''' <returns>社員番号</returns>
        Public Property BUKbn() As Char
            Get
                Return _BUKbn
            End Get
            Set(ByVal value As Char)
                _BUKbn = value
            End Set
        End Property

        ''' <summary>記号ID</summary>
        ''' <value>記号ID</value>
        ''' <returns>記号ID</returns>
        Public Property Kgba() As String
            Get
                Return _Kgba
            End Get
            Set(ByVal value As String)
                _Kgba = value
            End Set
        End Property

        ''' <summary>記号区分</summary>
        ''' <value>記号区分</value>
        ''' <returns>記号区分</returns>
        Public Property Kgkm() As Char
            Get
                Return _Kgkm
            End Get
            Set(ByVal value As Char)
                _Kgkm = value
            End Set
        End Property

        ''' <summary>手配記号</summary>
        ''' <value>手配記号</value>
        ''' <returns>手配記号</returns>
        Public Property Tikg() As String
            Get
                Return _Tikg
            End Get
            Set(ByVal value As String)
                _Tikg = value
            End Set
        End Property

        ''' <summary>内容40</summary>
        ''' <value>内容40</value>
        ''' <returns>内容40</returns>
        Public Property Ny40() As String
            Get
                Return _Ny40
            End Get
            Set(ByVal value As String)
                _Tikg = Ny40
            End Set
        End Property

        ''' <summary>図面枚数</summary>
        ''' <value>図面枚数</value>
        ''' <returns>図面枚数</returns>
        Public Property Zmmu() As Char
            Get
                Return _Zmmu
            End Get
            Set(ByVal value As Char)
                _Tikg = Zmmu
            End Set
        End Property

        ''' <summary>供給セクション</summary>
        ''' <value>供給セクション</value>
        ''' <returns>供給セクション</returns>
        Public Property Kysx() As String
            Get
                Return _Kysx
            End Get
            Set(ByVal value As String)
                _Tikg = Kysx
            End Set
        End Property

        ''' <summary>納入場所</summary>
        ''' <value>納入場所</value>
        ''' <returns>納入場所</returns>
        Public Property Nobs() As String
            Get
                Return _Nobs
            End Get
            Set(ByVal value As String)
                _Tikg = Nobs
            End Set
        End Property

        ''' <summary>調達区分</summary>
        ''' <value>調達区分</value>
        ''' <returns>調達区分</returns>
        Public Property Cekm() As Char
            Get
                Return _Cekm
            End Get
            Set(ByVal value As Char)
                _Tikg = Cekm
            End Set
        End Property

        ''' <summary>帳票区分</summary>
        ''' <value>帳票区分</value>
        ''' <returns>帳票区分</returns>
        Public Property Prkm() As Char
            Get
                Return _Prkm
            End Get
            Set(ByVal value As Char)
                _Tikg = Prkm
            End Set
        End Property


    End Class
End Namespace