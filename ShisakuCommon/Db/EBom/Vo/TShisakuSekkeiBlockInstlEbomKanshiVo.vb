Namespace Db.EBom.Vo
    ''' <summary>
    ''' 試作設計ブロックINSTL情報(EBOM設変)
    ''' </summary>
    ''' <remarks></remarks>
    Public Class TShisakuSekkeiBlockInstlEbomKanshiVo
        '' 試作イベントコード
        Private _ShisakuEventCode As String
        '' 試作部課コード
        Private _ShisakuBukaCode As String
        '' 試作ブロック№
        Private _ShisakuBlockNo As String
        '' 試作ブロック№改訂№
        Private _ShisakuBlockNoKaiteiNo As String
        '' 試作号車
        Private _ShisakuGousya As String
        '' INSTL品番表示順
        Private _InstlHinbanHyoujiJun As Nullable(of Int32)
        '' INSTL品番
        Private _InstlHinban As String
        '' INSTL品番区分
        Private _InstlHinbanKbn As String


        '' INSTL元データ区分
        Private _InstlDataKbn As String
        '' ベース情報フラグ
        Private _BaseInstlFlg As String

        ''↓↓2014/07/25 Ⅰ.3.設計編集 ベース改修専用化_o) (TES)張 ADD BEGIN
        ''↓↓2014/08/27 Ⅰ.3.設計編集 ベース改修専用化_o) 酒井 ADD BEGIN
        'Private _BaseFlg As String
        ''↑↑2014/08/27 Ⅰ.3.設計編集 ベース改修専用化_o) 酒井 ADD END
        ''↑↑2014/07/25 Ⅰ.3.設計編集 ベース改修専用化_o) (TES)張 ADD END




        '' 基本F品番
        Private _BfBuhinNo As String
        '' 員数
        Private _InsuSuryo As Nullable(of Int32)
        '' 最終更新日
        Private _SaisyuKoushinbi As Nullable(of Int32)
        '' 作成ユーザーID
        Private _CreatedUserId As String
        '' 作成日
        Private _CreatedDate As String
        '' 作成時
        Private _CreatedTime As String
        '' 更新ユーザーID
        Private _UpdatedUserId As String
        '' 更新日
        Private _UpdatedDate As String
        '' 更新時間
        Private _UpdatedTime As String

        ''' <summary>試作イベントコード</summary>
        ''' <value>試作イベントコード</value>
        ''' <returns>試作イベントコード</returns>
        Public Property ShisakuEventCode() As String
            Get
                Return _ShisakuEventCode
            End Get
            Set(ByVal value As String)
                _ShisakuEventCode = value
            End Set
        End Property

        ''' <summary>試作部課コード</summary>
        ''' <value>試作部課コード</value>
        ''' <returns>試作部課コード</returns>
        Public Property ShisakuBukaCode() As String
            Get
                Return _ShisakuBukaCode
            End Get
            Set(ByVal value As String)
                _ShisakuBukaCode = value
            End Set
        End Property

        ''' <summary>試作ブロック№</summary>
        ''' <value>試作ブロック№</value>
        ''' <returns>試作ブロック№</returns>
        Public Property ShisakuBlockNo() As String
            Get
                Return _ShisakuBlockNo
            End Get
            Set(ByVal value As String)
                _ShisakuBlockNo = value
            End Set
        End Property

        ''' <summary>試作ブロック№改訂№</summary>
        ''' <value>試作ブロック№改訂№</value>
        ''' <returns>試作ブロック№改訂№</returns>
        Public Property ShisakuBlockNoKaiteiNo() As String
            Get
                Return _ShisakuBlockNoKaiteiNo
            End Get
            Set(ByVal value As String)
                _ShisakuBlockNoKaiteiNo = value
            End Set
        End Property

        ''' <summary>試作号車</summary>
        ''' <value>試作号車</value>
        ''' <returns>試作号車</returns>
        Public Property ShisakuGousya() As String
            Get
                Return _ShisakuGousya
            End Get
            Set(ByVal value As String)
                _ShisakuGousya = value
            End Set
        End Property

        ''' <summary>INSTL品番表示順</summary>
        ''' <value>INSTL品番表示順</value>
        ''' <returns>INSTL品番表示順</returns>
        Public Property InstlHinbanHyoujiJun() As Nullable(of Int32)
            Get
                Return _InstlHinbanHyoujiJun
            End Get
            Set(ByVal value As Nullable(of Int32))
                _InstlHinbanHyoujiJun = value
            End Set
        End Property

        ''' <summary>INSTL品番</summary>
        ''' <value>INSTL品番</value>
        ''' <returns>INSTL品番</returns>
        Public Property InstlHinban() As String
            Get
                Return _InstlHinban
            End Get
            Set(ByVal value As String)
                _InstlHinban = value
            End Set
        End Property

        ''' <summary>INSTL品番区分</summary>
        ''' <value>INSTL品番区分</value>
        ''' <returns>INSTL品番区分</returns>
        Public Property InstlDataKbn() As String
            Get
                Return _InstlDataKbn
            End Get
            Set(ByVal value As String)
                _InstlDataKbn = value
            End Set
        End Property

        ''↓↓2014/07/25 Ⅰ.3.設計編集 ベース改修専用化_o) (TES)張 ADD BEGIN
        ''↓↓2014/08/27 Ⅰ.3.設計編集 ベース改修専用化_o) 酒井 ADD BEGIN
        'Public Property BaseFlg() As String
        '    Get
        '        Return _BaseFlg
        '    End Get
        '    Set(ByVal value As String)
        '        _BaseFlg = value
        '    End Set
        ''↑↑2014/07/25 Ⅰ.3.設計編集 ベース改修専用化_o) (TES)張 ADD END
        'End Property
        ''↑↑2014/08/27 Ⅰ.3.設計編集 ベース改修専用化_o) 酒井 ADD END


        ''' <summary>INSTL元データ区分</summary>
        ''' <value>INSTL元データ区分</value>
        ''' <returns>INSTL元データ区分</returns>
        Public Property InstlHinbanKbn() As String
            Get
                Return _InstlHinbanKbn
            End Get
            Set(ByVal value As String)
                _InstlHinbanKbn = value
            End Set
        End Property
        ''' <summary>ベース情報フラグ</summary>
        ''' <value>ベース情報フラグ</value>
        ''' <returns>ベース情報フラグ</returns>
        Public Property BaseInstlFlg() As String
            Get
                Return _BaseInstlFlg
            End Get
            Set(ByVal value As String)
                _BaseInstlFlg = value
            End Set
        End Property



        ''' <summary>
        ''' 基本F品番
        ''' </summary>
        ''' <value>基本F品番</value>
        ''' <returns>基本F品番</returns>
        ''' <remarks></remarks>
        Public Property BfBuhinNo() As String
            Get
                Return _BfBuhinNo
            End Get
            Set(ByVal value As String)
                _BfBuhinNo = value
            End Set
        End Property

        ''' <summary>員数</summary>
        ''' <value>員数</value>
        ''' <returns>員数</returns>
        Public Property InsuSuryo() As Nullable(Of Int32)
            Get
                Return _InsuSuryo
            End Get
            Set(ByVal value As Nullable(Of Int32))
                _InsuSuryo = value
            End Set
        End Property

        ''' <summary>最終更新日</summary>
        ''' <value>最終更新日</value>
        ''' <returns>最終更新日</returns>
        Public Property SaisyuKoushinbi() As Nullable(Of Int32)
            Get
                Return _SaisyuKoushinbi
            End Get
            Set(ByVal value As Nullable(Of Int32))
                _SaisyuKoushinbi = value
            End Set
        End Property

        ''' <summary>作成ユーザーID</summary>
        ''' <value>作成ユーザーID</value>
        ''' <returns>作成ユーザーID</returns>
        Public Property CreatedUserId() As String
            Get
                Return _CreatedUserId
            End Get
            Set(ByVal value As String)
                _CreatedUserId = value
            End Set
        End Property

        ''' <summary>作成日</summary>
        ''' <value>作成日</value>
        ''' <returns>作成日</returns>
        Public Property CreatedDate() As String
            Get
                Return _CreatedDate
            End Get
            Set(ByVal value As String)
                _CreatedDate = value
            End Set
        End Property

        ''' <summary>作成時</summary>
        ''' <value>作成時</value>
        ''' <returns>作成時</returns>
        Public Property CreatedTime() As String
            Get
                Return _CreatedTime
            End Get
            Set(ByVal value As String)
                _CreatedTime = value
            End Set
        End Property

        ''' <summary>更新ユーザーID</summary>
        ''' <value>更新ユーザーID</value>
        ''' <returns>更新ユーザーID</returns>
        Public Property UpdatedUserId() As String
            Get
                Return _UpdatedUserId
            End Get
            Set(ByVal value As String)
                _UpdatedUserId = value
            End Set
        End Property

        ''' <summary>更新日</summary>
        ''' <value>更新日</value>
        ''' <returns>更新日</returns>
        Public Property UpdatedDate() As String
            Get
                Return _UpdatedDate
            End Get
            Set(ByVal value As String)
                _UpdatedDate = value
            End Set
        End Property

        ''' <summary>更新時間</summary>
        ''' <value>更新時間</value>
        ''' <returns>更新時間</returns>
        Public Property UpdatedTime() As String
            Get
                Return _UpdatedTime
            End Get
            Set(ByVal value As String)
                _UpdatedTime = value
            End Set
        End Property
    End Class
End Namespace
