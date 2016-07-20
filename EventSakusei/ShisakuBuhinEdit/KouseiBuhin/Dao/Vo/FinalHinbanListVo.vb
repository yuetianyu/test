Namespace ShisakuBuhinEdit.KouseiBuhin.Dao.Vo

    ''' <summary>
    ''' ファイナル品番情報リストVo
    ''' </summary>
    ''' <remarks></remarks>
    Public Class FinalHinbanListVo

        '開発符号
        Private _KaihatsuFugo As String

        'ブロック№
        Private _BlockNo As String

        'ファイナル品番
        Private _FBuhinNo As String

        '付加№
        Private _FukaNo As String

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

        ''' <summary>ブロック№</summary>
        ''' <value>ブロック№</value>
        ''' <returns>ブロック№</returns>
        Public Property BlockNo() As String
            Get
                Return _BlockNo
            End Get
            Set(ByVal value As String)
                _BlockNo = value
            End Set
        End Property

        ''' <summary>ファイナル品番</summary>
        ''' <value>ファイナル品番</value>
        ''' <returns>ファイナル品番</returns>
        Public Property FBuhinNo() As String
            Get
                Return _FBuhinNo
            End Get
            Set(ByVal value As String)
                _FBuhinNo = value
            End Set
        End Property

        ''' <summary>付加№</summary>
        ''' <value>付加№</value>
        ''' <returns>付加№</returns>
        Public Property FukaNo() As String
            Get
                Return _FukaNo
            End Get
            Set(ByVal value As String)
                _FukaNo = value
            End Set
        End Property

    End Class

End Namespace

