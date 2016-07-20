''↓↓2014/08/04 Ⅰ.8.号車別仕様書 作成機能_i) (TES)張 ADD BEGIN
Namespace XVLView.Dao.Vo

    ''' <summary>
    ''' 号車別仕様書取得VO
    ''' </summary>
    ''' <remarks></remarks>
    Public Class GousyabetsuShiyousyoVo
        ''' <summary>
        ''' イベントコード
        ''' </summary>
        ''' <remarks></remarks>
        Private mSeisakuEventCode As String
        ''' <summary>
        ''' 表示順
        ''' </summary>
        ''' <remarks></remarks>
        Private mHyojijunNo As String
        ''' <summary>
        ''' 試作グループ
        ''' </summary>
        ''' <remarks></remarks>
        Private mSeisakuGroup As String
        ''' <summary>
        ''' 号車名.
        ''' </summary>
        ''' <remarks></remarks>
        Private mSeisakuGousya As String
        ''' <summary>
        ''' イベント名.
        ''' </summary>
        ''' <remarks></remarks>
        Private mSeisakuEventName As String
        '↓↓2014/09/30 酒井 ADD BEGIN
        Private mKaiteiNo As String
        Public Property KaiteiNo() As String
            Get
                Return mKaiteiNo
            End Get
            Set(ByVal value As String)
                mKaiteiNo = value
            End Set
        End Property
        '↑↑2014/09/30 酒井 ADD END

        Public Property SeisakuEventName() As String
            Get
                Return mSeisakuEventName
            End Get
            Set(ByVal value As String)
                mSeisakuEventName = value
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

        Public Property HyojijunNo() As String
            Get
                Return mHyojijunNo
            End Get
            Set(ByVal value As String)
                mHyojijunNo = value
            End Set
        End Property

        Public Property SeisakuGroup() As String
            Get
                Return mSeisakuGroup
            End Get
            Set(ByVal value As String)
                mSeisakuGroup = value
            End Set
        End Property

        Public Property SeisakuGousya() As String
            Get
                Return mSeisakuGousya
            End Get
            Set(ByVal value As String)
                mSeisakuGousya = value
            End Set
        End Property

    End Class

End Namespace
