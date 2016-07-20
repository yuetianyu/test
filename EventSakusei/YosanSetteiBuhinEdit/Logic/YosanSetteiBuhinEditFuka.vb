Imports System.Data
Imports System.Text
Imports EBom.Common
Imports EBom.Data
Imports EventSakusei.YosanSetteiBuhinEdit.Dao

''' <summary>
''' 手配情報付加機能テーブル検索クラス
''' </summary>
''' <remarks></remarks>
''' 
Public Class YosanSetteiBuhinEditFuka

#Region "プライベート変数"
    'Dbコネクション
    Private _ebomDb As SqlAccess
    Private _koseiDb As SqlAccess

    '入力変数
    Private _kaihatsuFugo As String
    Private _buhinNo As String
    Private _seihinKubun As String

    '出力変数
    Private _koutan As String = String.Empty
    Private _torihikisaki As String = String.Empty
    Private _senyouMark As String = String.Empty
    Private _zumenSettsu As String = String.Empty


#End Region

#Region "コンストラクタ"
    ''' <summary>
    '''  部品番号と製品区分を受付ける
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub New(ByVal aEbomDb As SqlAccess, ByVal aKoseiDb As SqlAccess, _
                            ByVal aKaihatsuFugo As String, ByVal aBuhinNo As String, ByVal aSeihinkubun As String)
        _ebomDb = aEbomDb
        _koseiDb = aKoseiDb

        _kaihatsuFugo = aKaihatsuFugo
        _buhinNo = aBuhinNo
        _seihinKubun = aSeihinkubun

        'テーブル検索
        Me.FindMain()

        If _zumenSettsu.Equals(String.Empty) Then
            '図面設通No取得
            FindBUHIN01()
        End If

    End Sub

#End Region

#Region "プロパティ"
    ''' <summary>
    ''' 購入担当を返す
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public ReadOnly Property koutan() As String
        Get
            Return _koutan
        End Get
    End Property
    ''' <summary>
    ''' 取引先を返す
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public ReadOnly Property Torihikisaki() As String
        Get
            Return _torihikisaki
        End Get
    End Property
    ''' <summary>
    ''' 専用マークを返す
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public ReadOnly Property SenyouMark() As String
        Get
            Return _senyouMark
        End Get
    End Property
    ''' <summary>
    ''' 図面設通番号を返す
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public ReadOnly Property ZumenSettsu()
        Get
            Return _zumenSettsu
        End Get
    End Property
#End Region

#Region "メソッド"

#Region "テーブル検索メイン"
    ''' <summary>
    ''' テーブル検索メイン
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub FindMain()

        _senyouMark = String.Empty

        '3ヶ月インフォメーション
        If FindKPSM10P() = True Then
            Exit Sub
        End If

        'パーツプライスリスト
        If findPARTSP() = True Then
            Exit Sub
        End If

        '海外生産マスタ
        If FindGKPSM10P() = True Then
            Exit Sub
        End If

        '専用マーク設定(上記３テーブルに存在しない場合)
        _senyouMark = "*"

        '部品マスタ
        If FindBUHIN01() = True Then
            Exit Sub
        End If

        '属性管理(開発符号毎)
        FindValueDev()



    End Sub

#End Region

#Region "3ヶ月インフォメーション"
    ''' <summary>
    ''' 3ヶ月インフォメーション
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function FindKPSM10P() As Boolean

        Dim dtResult As DataTable = Nothing

        '3ヶ月インフォメーション
        dtResult = YosanSetteiBuhinEditImpl.FindKPSM10P(_ebomDb, _buhinNo, _seihinKubun)

        If dtResult.Rows.Count >= 1 Then

            _koutan = dtResult.Rows(0)("KA").ToString.Trim
            _torihikisaki = dtResult.Rows(0)("TRCD").ToString.Trim

            '購入担当もしくは取引コードに値が入っている場合は抜ける
            If _koutan.Equals(String.Empty) = False OrElse _torihikisaki.Equals(String.Empty) Then
                _senyouMark = String.Empty
            Else
                Return False
            End If

        Else
            Return False
        End If

        Return True

    End Function

#End Region

#Region "パーツプライスリスト"
    ''' <summary>
    ''' パーツプライスリスト
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function findPARTSP() As Boolean
        Dim dtResult As DataTable = Nothing

        dtResult = YosanSetteiBuhinEditImpl.FindPARTSP(_ebomDb, _buhinNo)

        If dtResult.Rows.Count >= 1 Then

            _koutan = dtResult.Rows(0)("KA").ToString.Trim
            _torihikisaki = dtResult.Rows(0)("TRCD").ToString.Trim

            '購入担当、取引コードのどちらにも値が入っていない場合は抜ける
            If _koutan.Equals(String.Empty) = False AndAlso _torihikisaki.Equals(String.Empty) Then
                Return False
            End If

        Else
            Return False
        End If

        Return True

    End Function

#End Region

#Region "海外生産マスタ"
    ''' <summary>
    ''' 海外生産マスタ
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function FindGKPSM10P() As Boolean

        Dim dtResult As DataTable = Nothing

        dtResult = YosanSetteiBuhinEditImpl.FindGKPSM10P(_ebomDb, _buhinNo, _seihinKubun)

        If dtResult.Rows.Count >= 1 Then

            _koutan = dtResult.Rows(0)("KA").ToString.Trim
            _torihikisaki = dtResult.Rows(0)("TRCD").ToString.Trim

            '購入担当、取引コードのどちらにも値が入っていない場合は抜ける
            If _koutan.Equals(String.Empty) = False AndAlso _torihikisaki.Equals(String.Empty) Then
                Return False
            End If
        Else
            Return False
        End If

        Return True

    End Function

#End Region

#Region "部品マスタ"
    Private Function FindBUHIN01() As Boolean
        Dim dtResult As DataTable = Nothing
        Dim buhiNo10 As String = Left(_buhinNo, 10)
        'BUHIN01からだけ見てる・・・'
        dtResult = YosanSetteiBuhinEditImpl.FindBUHIN01(_ebomDb, buhiNo10)

        If dtResult.Rows.Count >= 1 Then

            _koutan = dtResult.Rows(0)("KOTAN").ToString.Trim
            _torihikisaki = dtResult.Rows(0)("MAKER").ToString.Trim

            Dim series As String = dtResult.Rows(0)("STSR").ToString.Trim
            Dim settsu As String = dtResult.Rows(0)("DHSTBA").ToString.Trim

            '図面設通No設定
            If series.Equals(String.Empty) = False AndAlso settsu.Equals(String.Empty) = False Then
                _zumenSettsu = String.Format("{0} {1}", series, settsu)
            End If


            'Dim sakuseiImpl As YosanSetteiBuhinEditSakusei.Dao.YosanSetteiBuhinEditSakuseiDao = New YosanSetteiBuhinEditSakusei.Dao.YosanSetteiBuhinEditSakuseiDaoImpl
            'Dim vo As New ShisakuCommon.Db.EBom.Vo.TShisakuBuhinEditTmpVo
            'vo = sakuseiImpl.FindByKoutanTorihikisaki(_buhinNo)
            'If _koutan.Equals(String.Empty) Then
            '    _koutan = vo.Koutan
            'End If
            'If _torihikisaki.Equals(String.Empty) Then
            '    _torihikisaki = vo.MakerCode
            'End If


            '購入担当、取引コードのどちらにも値が入っていない場合は抜ける
            If _koutan.Equals(String.Empty) = False AndAlso _torihikisaki.Equals(String.Empty) Then
                Return False
            End If
        Else
            Return False
        End If

        Return True

    End Function

#End Region

#Region "属性管理(開発符号毎)"
    ''' <summary>
    ''' 属性管理(開発符号毎)
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function FindValueDev() As Boolean
        Dim dtResult As DataTable = Nothing
        Dim buhiNo10 As String = Left(_buhinNo, 10)

        dtResult = YosanSetteiBuhinEditImpl.FindValueDev(_koseiDb, _kaihatsuFugo, buhiNo10)

        If dtResult.Rows.Count >= 1 Then

            _koutan = dtResult.Rows(0)("FHI_NOMINATE_KOTAN").ToString.Trim
            _torihikisaki = dtResult.Rows(0)("TORIHIKISAKI_CODE").ToString.Trim

            '購入担当、取引コードのどちらにも値が入っていない場合は抜ける
            If _koutan.Equals(String.Empty) = False AndAlso _torihikisaki.Equals(String.Empty) Then
                Return False
            End If

        Else

            Return False

        End If

        Return True

    End Function

#End Region

#End Region

#Region "Sharedメソッド"
#Region "　Left メソッド　"
    ''' -----------------------------------------------------------------------------------
    ''' <summary>
    '''     文字列の左端から指定された文字数分の文字列を返します。</summary>
    ''' <param name="stTarget">
    '''     取り出す元になる文字列。</param>
    ''' <param name="iLength">
    '''     取り出す文字数。</param>
    ''' <returns>
    '''     左端から指定された文字数分の文字列。
    '''     文字数を超えた場合は、文字列全体が返されます。</returns>
    ''' -----------------------------------------------------------------------------------
    Public Shared Function Left(ByVal stTarget As String, ByVal iLength As Integer) As String
        If iLength <= stTarget.Length Then
            Return stTarget.Substring(0, iLength)
        End If

        Return stTarget
    End Function

#End Region

#End Region

End Class
