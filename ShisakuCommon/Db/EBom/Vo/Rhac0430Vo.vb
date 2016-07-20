Namespace Db.EBom.Vo
    ''' <summary>
    ''' カラー
    ''' </summary>
    ''' <remarks></remarks>
    Public Class Rhac0430Vo
        '' カラーコード 
        Private _ColorCode As String
        '' 内外装区分 
        Private _NaigaisoKbn As String
        '' カラー名称 
        Private _ColorName As String
        '' カラー名称（英文） 
        Private _ColorEgName As String
        '' カラースペックコード 
        Private _ColorSpecCode As String
        '' 登録開発符号 
        Private _TrkKaihatsuFugo As String
        '' 登録日付 
        Private _TorokuDate As Nullable(of Int32)
        '' モデルイヤーコード 
        Private _ModelYearCode As String
        '' カラー備考 
        Private _ColorBiko As String
        '' 付加番号 
        Private _FukaNo As String
        '' 付加番号SIA 
        Private _FukaNoSia As String
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

        ''' <summary>カラーコード</summary>
        ''' <value>カラーコード</value>
        ''' <returns>カラーコード</returns>
        Public Property ColorCode() As String
            Get
                Return _ColorCode
            End Get
            Set(ByVal value As String)
                _ColorCode = value
            End Set
        End Property

        ''' <summary>内外装区分</summary>
        ''' <value>内外装区分</value>
        ''' <returns>内外装区分</returns>
        Public Property NaigaisoKbn() As String
            Get
                Return _NaigaisoKbn
            End Get
            Set(ByVal value As String)
                _NaigaisoKbn = value
            End Set
        End Property

        ''' <summary>カラー名称</summary>
        ''' <value>カラー名称</value>
        ''' <returns>カラー名称</returns>
        Public Property ColorName() As String
            Get
                Return _ColorName
            End Get
            Set(ByVal value As String)
                _ColorName = value
            End Set
        End Property

        ''' <summary>カラー名称（英文）</summary>
        ''' <value>カラー名称（英文）</value>
        ''' <returns>カラー名称（英文）</returns>
        Public Property ColorEgName() As String
            Get
                Return _ColorEgName
            End Get
            Set(ByVal value As String)
                _ColorEgName = value
            End Set
        End Property

        ''' <summary>カラースペックコード</summary>
        ''' <value>カラースペックコード</value>
        ''' <returns>カラースペックコード</returns>
        Public Property ColorSpecCode() As String
            Get
                Return _ColorSpecCode
            End Get
            Set(ByVal value As String)
                _ColorSpecCode = value
            End Set
        End Property

        ''' <summary>登録開発符号</summary>
        ''' <value>登録開発符号</value>
        ''' <returns>登録開発符号</returns>
        Public Property TrkKaihatsuFugo() As String
            Get
                Return _TrkKaihatsuFugo
            End Get
            Set(ByVal value As String)
                _TrkKaihatsuFugo = value
            End Set
        End Property

        ''' <summary>登録日付</summary>
        ''' <value>登録日付</value>
        ''' <returns>登録日付</returns>
        Public Property TorokuDate() As Nullable(of Int32)
            Get
                Return _TorokuDate
            End Get
            Set(ByVal value As Nullable(of Int32))
                _TorokuDate = value
            End Set
        End Property

        ''' <summary>モデルイヤーコード</summary>
        ''' <value>モデルイヤーコード</value>
        ''' <returns>モデルイヤーコード</returns>
        Public Property ModelYearCode() As String
            Get
                Return _ModelYearCode
            End Get
            Set(ByVal value As String)
                _ModelYearCode = value
            End Set
        End Property

        ''' <summary>カラー備考</summary>
        ''' <value>カラー備考</value>
        ''' <returns>カラー備考</returns>
        Public Property ColorBiko() As String
            Get
                Return _ColorBiko
            End Get
            Set(ByVal value As String)
                _ColorBiko = value
            End Set
        End Property

        ''' <summary>付加番号</summary>
        ''' <value>付加番号</value>
        ''' <returns>付加番号</returns>
        Public Property FukaNo() As String
            Get
                Return _FukaNo
            End Get
            Set(ByVal value As String)
                _FukaNo = value
            End Set
        End Property

        ''' <summary>付加番号SIA</summary>
        ''' <value>付加番号SIA</value>
        ''' <returns>付加番号SIA</returns>
        Public Property FukaNoSia() As String
            Get
                Return _FukaNoSia
            End Get
            Set(ByVal value As String)
                _FukaNoSia = value
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
