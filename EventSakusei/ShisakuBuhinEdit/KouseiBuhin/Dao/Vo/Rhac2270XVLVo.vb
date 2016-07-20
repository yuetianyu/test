Namespace ShisakuBuhinEdit.KouseiBuhin.Dao.Vo

    ''' <summary>
    ''' RHAC2270のXVL情報取得用VO
    ''' </summary>
    ''' <remarks></remarks>
    Public Class Rhac2270XVLVo

        ''' <summary>
        ''' 開発符号
        ''' </summary>
        ''' <remarks></remarks>
        Private _KaihatsuFugo As String
        ''' <summary>
        ''' ブロック№
        ''' </summary>
        ''' <remarks></remarks>
        Private _BlockNo As String
        ''' <summary>
        ''' 部品番号
        ''' </summary>
        ''' <remarks></remarks>
        Private _BuhinNo As String
        ''' <summary>
        ''' XVLのファイル名
        ''' </summary>
        ''' <remarks></remarks>
        Private _XVLFileName As String
        ''' <summary>
        ''' CADデータ区分
        ''' </summary>
        ''' <remarks></remarks>
        Private _CadDataEventKbn As String

        ''' <summary>
        ''' 開発符号
        ''' </summary>
        ''' <value></value>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Property KaihatsuFugo() As String
            Get
                Return _KaihatsuFugo
            End Get
            Set(ByVal value As String)
                _KaihatsuFugo = value
            End Set
        End Property

        ''' <summary>
        ''' ブロック№
        ''' </summary>
        ''' <value></value>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Property BlockNo() As String
            Get
                Return _BlockNo
            End Get
            Set(ByVal value As String)
                _BlockNo = value
            End Set
        End Property

        ''' <summary>
        ''' 部品番号
        ''' </summary>
        ''' <value></value>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Property BuhinNo() As String
            Get
                Return _BuhinNo
            End Get
            Set(ByVal value As String)
                _BuhinNo = value
            End Set
        End Property

        ''' <summary>
        ''' XVLファイル名
        ''' </summary>
        ''' <value></value>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Property XVLFileName() As String
            Get
                Return _XVLFileName
            End Get
            Set(ByVal value As String)
                _XVLFileName = value
            End Set
        End Property

        ''' <summary>
        ''' CADデータ区分
        ''' </summary>
        ''' <value></value>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Property CadDataEventKbn() As String
            Get
                Return _CadDataEventKbn
            End Get
            Set(ByVal value As String)
                _CadDataEventKbn = value
            End Set
        End Property


    End Class

End Namespace
