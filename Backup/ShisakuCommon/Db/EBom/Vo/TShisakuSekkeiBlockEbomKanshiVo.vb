Namespace Db.EBom.Vo
    ''' <summary>
    ''' 試作設計ブロック情報(EBOM設変)
    ''' </summary>
    ''' <remarks></remarks>
    Public Class TShisakuSekkeiBlockEbomKanshiVo
        '' 試作イベントコード
        Private _ShisakuEventCode As String
        '' 試作部課コード
        Private _ShisakuBukaCode As String
        '' 試作ブロック№表示順
        Private _ShisakuBlockNoHyoujiJun As Nullable(of Int32)
        '' 試作ブロック№
        Private _ShisakuBlockNo As String
        '' 試作ブロック№改訂№
        Private _ShisakuBlockNoKaiteiNo As String
        '' ブロック不要
        Private _BlockFuyou As String
        '' 状態
        Private _Jyoutai As String
        '' ユニット区分
        Private _UnitKbn As String
        '' ブロック名称
        Private _ShisakuBlockName As String
        '' 担当者
        Private _UserId As String
        '' ＴＥＬ
        Private _TelNo As String
        '' 改訂内容
        Private _KaiteiNaiyou As String
        '' 最終更新日
        Private _SaisyuKoushinbi As Nullable(of Int32)
        '' 最終更新時間
        Private _SaisyuKoushinjikan As Nullable(Of Int32)
        '' 一時保存時のメモ
        Private _Memo As String
        '' 担当承認状態
        Private _TantoSyouninJyoutai As String
        '' 担当承認・所属
        Private _TantoSyouninKa As String
        '' 担当承認・承認者
        Private _TantoSyouninSya As String
        '' 担当承認・承認日
        Private _TantoSyouninHi As Nullable(of Int32)
        '' 担当承認・承認時間
        Private _TantoSyouninJikan As Nullable(of Int32)
        '' 課長主査承認状態
        Private _KachouSyouninJyoutai As String
        '' 課長主査承認・所属
        Private _KachouSyouninKa As String
        '' 課長主査承認・承認者
        Private _KachouSyouninSya As String
        '' 課長主査承認・承認日
        Private _KachouSyouninHi As Nullable(of Int32)
        '' 課長主査承認・承認時間
        Private _KachouSyouninJikan As Nullable(of Int32)
        '' 改訂判断フラグ
        Private _KaiteiHandanFlg As String
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

        ''' <summary>試作ブロック№表示順</summary>
        ''' <value>試作ブロック№表示順</value>
        ''' <returns>試作ブロック№表示順</returns>
        Public Property ShisakuBlockNoHyoujiJun() As Nullable(of Int32)
            Get
                Return _ShisakuBlockNoHyoujiJun
            End Get
            Set(ByVal value As Nullable(of Int32))
                _ShisakuBlockNoHyoujiJun = value
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

        ''' <summary>ブロック不要</summary>
        ''' <value>ブロック不要</value>
        ''' <returns>ブロック不要</returns>
        Public Property BlockFuyou() As String
            Get
                Return _BlockFuyou
            End Get
            Set(ByVal value As String)
                _BlockFuyou = value
            End Set
        End Property

        ''' <summary>状態</summary>
        ''' <value>状態</value>
        ''' <returns>状態</returns>
        Public Property Jyoutai() As String
            Get
                Return _Jyoutai
            End Get
            Set(ByVal value As String)
                _Jyoutai = value
            End Set
        End Property

        ''' <summary>ユニット区分</summary>
        ''' <value>ユニット区分</value>
        ''' <returns>ユニット区分</returns>
        Public Property UnitKbn() As String
            Get
                Return _UnitKbn
            End Get
            Set(ByVal value As String)
                _UnitKbn = value
            End Set
        End Property

        ''' <summary>ブロック名称</summary>
        ''' <value>ブロック名称</value>
        ''' <returns>ブロック名称</returns>
        Public Property ShisakuBlockName() As String
            Get
                Return _ShisakuBlockName
            End Get
            Set(ByVal value As String)
                _ShisakuBlockName = value
            End Set
        End Property

        ''' <summary>担当者</summary>
        ''' <value>担当者</value>
        ''' <returns>担当者</returns>
        Public Property UserId() As String
            Get
                Return _UserId
            End Get
            Set(ByVal value As String)
                _UserId = value
            End Set
        End Property

        ''' <summary>ＴＥＬ</summary>
        ''' <value>ＴＥＬ</value>
        ''' <returns>ＴＥＬ</returns>
        Public Property TelNo() As String
            Get
                Return _TelNo
            End Get
            Set(ByVal value As String)
                _TelNo = value
            End Set
        End Property

        ''' <summary>改訂内容</summary>
        ''' <value>改訂内容</value>
        ''' <returns>改訂内容</returns>
        Public Property KaiteiNaiyou() As String
            Get
                Return _KaiteiNaiyou
            End Get
            Set(ByVal value As String)
                _KaiteiNaiyou = value
            End Set
        End Property

        ''' <summary>最終更新日</summary>
        ''' <value>最終更新日</value>
        ''' <returns>最終更新日</returns>
        Public Property SaisyuKoushinbi() As Nullable(of Int32)
            Get
                Return _SaisyuKoushinbi
            End Get
            Set(ByVal value As Nullable(of Int32))
                _SaisyuKoushinbi = value
            End Set
        End Property

        ''' <summary>最終更新時間</summary>
        ''' <value>最終更新時間</value>
        ''' <returns>最終更新時間</returns>
        Public Property SaisyuKoushinjikan() As Nullable(Of Int32)
            Get
                Return _SaisyuKoushinjikan
            End Get
            Set(ByVal value As Nullable(Of Int32))
                _SaisyuKoushinjikan = value
            End Set
        End Property

        ''' <summary>一時保存時のメモ</summary>
        ''' <value>一時保存時のメモ</value>
        ''' <returns>一時保存時のメモ</returns>
        Public Property Memo() As String
            Get
                Return _Memo
            End Get
            Set(ByVal value As String)
                _Memo = value
            End Set
        End Property

        ''' <summary>担当承認状態</summary>
        ''' <value>担当承認状態</value>
        ''' <returns>担当承認状態</returns>
        Public Property TantoSyouninJyoutai() As String
            Get
                Return _TantoSyouninJyoutai
            End Get
            Set(ByVal value As String)
                _TantoSyouninJyoutai = value
            End Set
        End Property

        ''' <summary>担当承認・所属</summary>
        ''' <value>担当承認・所属</value>
        ''' <returns>担当承認・所属</returns>
        Public Property TantoSyouninKa() As String
            Get
                Return _TantoSyouninKa
            End Get
            Set(ByVal value As String)
                _TantoSyouninKa = value
            End Set
        End Property

        ''' <summary>担当承認・承認者</summary>
        ''' <value>担当承認・承認者</value>
        ''' <returns>担当承認・承認者</returns>
        Public Property TantoSyouninSya() As String
            Get
                Return _TantoSyouninSya
            End Get
            Set(ByVal value As String)
                _TantoSyouninSya = value
            End Set
        End Property

        ''' <summary>担当承認・承認日</summary>
        ''' <value>担当承認・承認日</value>
        ''' <returns>担当承認・承認日</returns>
        Public Property TantoSyouninHi() As Nullable(of Int32)
            Get
                Return _TantoSyouninHi
            End Get
            Set(ByVal value As Nullable(of Int32))
                _TantoSyouninHi = value
            End Set
        End Property

        ''' <summary>担当承認・承認時間</summary>
        ''' <value>担当承認・承認時間</value>
        ''' <returns>担当承認・承認時間</returns>
        Public Property TantoSyouninJikan() As Nullable(of Int32)
            Get
                Return _TantoSyouninJikan
            End Get
            Set(ByVal value As Nullable(of Int32))
                _TantoSyouninJikan = value
            End Set
        End Property

        ''' <summary>課長主査承認状態</summary>
        ''' <value>課長主査承認状態</value>
        ''' <returns>課長主査承認状態</returns>
        Public Property KachouSyouninJyoutai() As String
            Get
                Return _KachouSyouninJyoutai
            End Get
            Set(ByVal value As String)
                _KachouSyouninJyoutai = value
            End Set
        End Property

        ''' <summary>課長主査承認・所属</summary>
        ''' <value>課長主査承認・所属</value>
        ''' <returns>課長主査承認・所属</returns>
        Public Property KachouSyouninKa() As String
            Get
                Return _KachouSyouninKa
            End Get
            Set(ByVal value As String)
                _KachouSyouninKa = value
            End Set
        End Property

        ''' <summary>課長主査承認・承認者</summary>
        ''' <value>課長主査承認・承認者</value>
        ''' <returns>課長主査承認・承認者</returns>
        Public Property KachouSyouninSya() As String
            Get
                Return _KachouSyouninSya
            End Get
            Set(ByVal value As String)
                _KachouSyouninSya = value
            End Set
        End Property

        ''' <summary>課長主査承認・承認日</summary>
        ''' <value>課長主査承認・承認日</value>
        ''' <returns>課長主査承認・承認日</returns>
        Public Property KachouSyouninHi() As Nullable(of Int32)
            Get
                Return _KachouSyouninHi
            End Get
            Set(ByVal value As Nullable(of Int32))
                _KachouSyouninHi = value
            End Set
        End Property

        ''' <summary>課長主査承認・承認時間</summary>
        ''' <value>課長主査承認・承認時間</value>
        ''' <returns>課長主査承認・承認時間</returns>
        Public Property KachouSyouninJikan() As Nullable(of Int32)
            Get
                Return _KachouSyouninJikan
            End Get
            Set(ByVal value As Nullable(of Int32))
                _KachouSyouninJikan = value
            End Set
        End Property

        ''' <summary>改訂判断フラグ</summary>
        ''' <value>改訂判断フラグ</value>
        ''' <returns>改訂判断フラグ</returns>
        Public Property KaiteiHandanFlg() As String
            Get
                Return _KaiteiHandanFlg
            End Get
            Set(ByVal value As String)
                _KaiteiHandanFlg = value
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
