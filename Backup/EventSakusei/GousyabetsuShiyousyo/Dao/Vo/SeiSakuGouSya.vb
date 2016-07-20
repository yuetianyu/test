''↓↓2014/08/04 Ⅰ.8.号車別仕様書 作成機能_i) (TES)張 ADD BEGIN
Namespace XVLView.Dao.Vo
    Public Class SeiSakuGouSya
        ''' <summary>
        ''' イベントコード
        ''' </summary>
        ''' <remarks></remarks>
        Private mSeisakuEventCode As String
        '↓↓2014/10/21 酒井 ADD BEGIN
        Private mKaihatsuFugo As String
        '↑↑2014/10/21 酒井 ADD END
        ''' <summary>
        ''' イベント名
        ''' </summary>
        ''' <remarks></remarks>
        Private mSeisakuEventName As String
        ''' <summary>
        ''' 号車
        ''' </summary>
        ''' <remarks></remarks>
        Private mGousya As String
        ''' <summary>
        ''' 車型
        ''' </summary>
        ''' <remarks></remarks>
        Private mSyasyu As String
        ''' <summary>
        ''' ｸﾞﾚｰﾄﾞ
        ''' </summary>
        ''' <remarks></remarks>
        Private mGrade As String
        ''' <summary>
        ''' E/G
        ''' </summary>
        ''' <remarks></remarks>
        Private mEgName As String
        '↓↓2014/10/21 酒井 ADD BEGIN
        Private mEgHaikiryou As String
        '↑↑2014/10/21 酒井 ADD END
        ''' <summary>
        ''' 過給器
        ''' </summary>
        ''' <remarks></remarks>
        Private mEgKakyuuki As String
        ''' <summary>
        ''' T/M
        ''' </summary>
        ''' <remarks></remarks>
        Private mTmName As String
        '↓↓2014/10/21 酒井 ADD BEGIN
        Private mTmHensokuki As String
        '↑↑2014/10/21 酒井 ADD END
        ''' <summary>
        ''' 仕向け
        ''' </summary>
        ''' <remarks></remarks>
        Private mShimuke As String
        ''' <summary>
        ''' ﾊﾝﾄﾞﾙ
        ''' </summary>
        ''' <remarks></remarks>
        Private mHandoru As String
        ''' <summary>
        ''' ｸﾞﾙｰﾌﾟ
        ''' </summary>
        ''' <remarks></remarks>
        Private mSeisakuGroup As String
        ''↓↓2014/09/10 Ⅰ.8.号車別仕様書 作成機能_q) 酒井 ADD BEGIN
        ''' <summary>
        ''' 完成日
        ''' </summary>
        ''' <remarks></remarks>
        Private mKanseiKibouBi As String
        'Private mCreatedDate As Date
        ''↑↑2014/09/10 Ⅰ.8.号車別仕様書 作成機能_q) 酒井 ADD END
        ''' <summary>
        ''' 型式
        ''' </summary>
        ''' <remarks></remarks>
        Private mKatashikiscd7 As String
        ''' <summary>
        ''' OPｺｰﾄﾞ
        ''' </summary>
        ''' <remarks></remarks>
        Private mKatashikiop As String
        ''' <summary>
        ''' （外）色ｺｰﾄﾞ
        ''' </summary>
        ''' <remarks></remarks>
        Private mGaisousyoku As String
        ''' <summary>
        ''' 外装色
        ''' </summary>
        ''' <remarks></remarks>
        Private mGaisousyokuName As String
        ''' <summary>
        ''' （内）色ｺｰﾄﾞ
        ''' </summary>
        ''' <remarks></remarks>
        Private mNaisousyoku As String
        ''' <summary>
        ''' 内装色
        ''' </summary>
        ''' <remarks></remarks>
        Private mNaisousyokuName As String
        ''' <summary>
        ''' 車体№
        ''' </summary>
        ''' <remarks></remarks>
        Private mSyataiNo As String
        ''' <summary>
        ''' 使用部署
        ''' </summary>
        ''' <remarks></remarks>
        Private mShiyoubusyo As String
        ''' <summary>
        ''' 使用目的
        ''' </summary>
        ''' <remarks></remarks>
        Private mShiyoumokuteki As String
        ''' <summary>
        ''' 主要確認項目
        ''' </summary>
        ''' <remarks></remarks>
        Private mSyuyoukakuninkoumoku As String
        ''' <summary>
        ''' ﾒﾓ欄
        ''' </summary>
        ''' <remarks></remarks>
        Private mMemo As String
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
        '↓↓2014/10/21 酒井 ADD BEGIN
        Public Property EgHaikiryou() As String
            Get
                Return mEgHaikiryou
            End Get
            Set(ByVal value As String)
                mEgHaikiryou = value
            End Set
        End Property
        Public Property TmHensokuki() As String
            Get
                Return mTmHensokuki
            End Get
            Set(ByVal value As String)
                mTmHensokuki = value
            End Set
        End Property
        Public Property KaihatsuFugo() As String
            Get
                Return mKaihatsuFugo
            End Get
            Set(ByVal value As String)
                mKaihatsuFugo = value
            End Set
        End Property
        '↑↑2014/10/21 酒井 ADD END
        ''' <summary>
        ''' イベント名
        ''' </summary>
        ''' <value></value>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Property SeisakuEventName() As String
            Get
                Return mSeisakuEventName
            End Get
            Set(ByVal value As String)
                mSeisakuEventName = value
            End Set
        End Property
        ''' <summary>
        ''' イベントコード
        ''' </summary>
        ''' <value></value>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Property SeisakuEventCode() As String
            Get
                Return mSeisakuEventCode
            End Get
            Set(ByVal value As String)
                mSeisakuEventCode = value
            End Set
        End Property
        ''' <summary>
        ''' 号車
        ''' </summary>
        ''' <value></value>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Property Gousya() As String
            Get
                Return mGousya
            End Get
            Set(ByVal value As String)
                mGousya = value
            End Set
        End Property
        ''' <summary>
        ''' 車型
        ''' </summary>
        ''' <value></value>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Property Syasyu() As String
            Get
                Return mSyasyu
            End Get
            Set(ByVal value As String)
                mSyasyu = value
            End Set
        End Property
        ''' <summary>
        '''  ｸﾞﾚｰﾄﾞ
        ''' </summary>
        ''' <value></value>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Property Grade() As String
            Get
                Return mGrade
            End Get
            Set(ByVal value As String)
                mGrade = value
            End Set
        End Property
        ''' <summary>
        '''  E/G
        ''' </summary>
        ''' <value></value>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Property EgEgName() As String
            Get
                Return mEgName
            End Get
            Set(ByVal value As String)
                mEgName = value
            End Set
        End Property
        ''' <summary>
        '''  過給器
        ''' </summary>
        ''' <value></value>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Property EgKakyuuki() As String
            Get
                Return mEgKakyuuki
            End Get
            Set(ByVal value As String)
                mEgKakyuuki = value
            End Set
        End Property
        ''' <summary>
        '''  T/M
        ''' </summary>
        ''' <value></value>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Property TmTmName() As String
            Get
                Return mTmName
            End Get
            Set(ByVal value As String)
                mTmName = value
            End Set
        End Property
        ''' <summary>
        '''  仕向け
        ''' </summary>
        ''' <value></value>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Property Shimuke() As String
            Get
                Return mShimuke
            End Get
            Set(ByVal value As String)
                mShimuke = value
            End Set
        End Property
        ''' <summary>
        '''   ﾊﾝﾄﾞﾙ
        ''' </summary>
        ''' <value></value>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Property Handoru() As String
            Get
                Return mHandoru
            End Get
            Set(ByVal value As String)
                mHandoru = value
            End Set
        End Property
        ''' <summary>
        '''  ｸﾞﾙｰﾌﾟ
        ''' </summary>
        ''' <value></value>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Property SeisakuGroup() As String
            Get
                Return mSeisakuGroup
            End Get
            Set(ByVal value As String)
                mSeisakuGroup = value
            End Set
        End Property

        ''↓↓2014/09/10 Ⅰ.8.号車別仕様書 作成機能_q) 酒井 ADD BEGIN
        ''' <summary>
        '''  完成日
        ''' </summary>
        ''' <value></value>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Property KanseiKibouBi() As String
            'Public Property CreatedDate() As String
            Get
                If IsDate(mKanseiKibouBi) Then
                    '↓↓2014/10/01 酒井 ADD BEGIN
                    'Return mKanseiKibouBi.ToString("yy/MM")
                    'Return Right(mKanseiKibouBi, 5)
                    Return DatePart(DateInterval.Month, DateTime.Parse(mKanseiKibouBi)) & "/" & DatePart(DateInterval.Day, DateTime.Parse(mKanseiKibouBi))
                    '↑↑2014/10/01 酒井 ADD END
                Else
                    Return mKanseiKibouBi
                End If
            End Get
            Set(ByVal value As String)
                mKanseiKibouBi = value
            End Set
        End Property

        ''↑↑2014/09/10 Ⅰ.8.号車別仕様書 作成機能_q) 酒井 ADD END
        ''' <summary>
        '''  型式
        ''' </summary>
        ''' <value></value>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Property KatashikiScd7() As String
            Get
                Return mKatashikiscd7
            End Get
            Set(ByVal value As String)
                mKatashikiscd7 = value
            End Set
        End Property
        ''' <summary>
        '''  OPｺｰﾄﾞ
        ''' </summary>
        ''' <value></value>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Property KatashikiOp() As String
            Get
                Return mKatashikiop
            End Get
            Set(ByVal value As String)
                mKatashikiop = value
            End Set
        End Property
        ''' <summary>
        ''' （外）色ｺｰﾄﾞ
        ''' </summary>
        ''' <value></value>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Property Gaisousyoku() As String
            Get
                Return mGaisousyoku
            End Get
            Set(ByVal value As String)
                mGaisousyoku = value
            End Set
        End Property
        ''' <summary>
        ''' 外装色
        ''' </summary>
        ''' <value></value>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Property GaisousyokuName() As String
            Get
                Return mGaisousyokuName
            End Get
            Set(ByVal value As String)
                mGaisousyokuName = value
            End Set
        End Property
        ''' <summary>
        ''' （内）色ｺｰﾄﾞ
        ''' </summary>
        ''' <value></value>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Property Naisousyoku() As String
            Get
                Return mNaisousyoku
            End Get
            Set(ByVal value As String)
                mNaisousyoku = value
            End Set
        End Property
        ''' <summary>
        ''' 内装色
        ''' </summary>
        ''' <value></value>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Property NaisousyokuName() As String
            Get
                Return mNaisousyokuName
            End Get
            Set(ByVal value As String)
                mNaisousyokuName = value
            End Set
        End Property
        ''' <summary>
        '''  車体№
        ''' </summary>
        ''' <value></value>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Property SyataiNo() As String
            Get
                Return mSyataiNo
            End Get
            Set(ByVal value As String)
                mSyataiNo = value
            End Set
        End Property
        ''' <summary>
        '''  使用部署
        ''' </summary>
        ''' <value></value>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Property ShiyouBusyo() As String
            Get
                Return mShiyoubusyo
            End Get
            Set(ByVal value As String)
                mShiyoubusyo = value
            End Set
        End Property
        ''' <summary>
        '''  使用目的
        ''' </summary>
        ''' <value></value>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Property ShiyouMokuteki() As String
            Get
                Return mShiyoumokuteki
            End Get
            Set(ByVal value As String)
                mShiyoumokuteki = value
            End Set
        End Property
        ''' <summary>
        '''  主要確認項目
        ''' </summary>
        ''' <value></value>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Property SyuyoukakuninKoumoku() As String
            Get
                Return mSyuyoukakuninkoumoku
            End Get
            Set(ByVal value As String)
                mSyuyoukakuninkoumoku = value
            End Set
        End Property
        ''' <summary>
        '''  ﾒﾓ欄
        ''' </summary>
        ''' <value></value>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Property Memo() As String
            Get
                Return mMemo
            End Get
            Set(ByVal value As String)
                mMemo = value
            End Set
        End Property
    End Class
End Namespace

