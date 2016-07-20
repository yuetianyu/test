''↓↓2014/08/04 Ⅰ.8.号車別仕様書 作成機能_i) (TES)張 ADD BEGIN
Namespace XVLView.Dao.Vo

    ''' <summary>
    ''' OP項目取得VO
    ''' </summary>
    ''' <remarks></remarks>
    Public Class SeisakuOPVo
        ''' <summary>
        '''ﾗｲﾝOP項目名
        ''' </summary>
        ''' <remarks></remarks>
        Private mOPName As String
        ''' <summary>
        '''イベント
        ''' </summary>
        ''' <remarks></remarks>
        Private mSeisakuEventCode As String
        ''' <summary>
        '''号車
        ''' </summary>
        ''' <remarks></remarks>
        Private mGousya As String
        '↓↓2014/10/01 酒井 ADD BEGIN
        Private mKaiteiNo As String
        Public Property KaiteiNo() As String
            Get
                Return mKaiteiNo
            End Get
            Set(ByVal value As String)
                mKaiteiNo = value
            End Set
        End Property

        '↑↑2014/10/01 酒井 ADD END
        ''↓↓2014/09/10 Ⅰ.8.号車別仕様書 作成機能 酒井 ADD BEGIN
        Private mTekiyou As String
        Public Property Tekiyou() As String
            Get
                Return mTekiyou
            End Get
            Set(ByVal value As String)
                mTekiyou = value
            End Set
        End Property
        ''↑↑2014/09/10 Ⅰ.8.号車別仕様書 作成機能 酒井 ADD END
       
        Public Property OpName() As String
            Get
                Return mOPName
            End Get
            Set(ByVal value As String)
                mOPName = value
            End Set
        End Property
        Public Property SeisakuEventCode() As String
            Get
                Return mSeisakuEventCode
            End Get
            Set(ByVal value As String)
                mSeisakuEventCode = value
            End Set
        End Property
        Public Property Gousya() As String
            Get
                Return mGousya
            End Get
            Set(ByVal value As String)
                mGousya = value
            End Set
        End Property
    End Class

End Namespace


