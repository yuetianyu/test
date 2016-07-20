Namespace Db.EBom.Vo

    ''' <summary>
    ''' イベント別台当たり単価情報_型情報
    ''' </summary>
    ''' <remarks></remarks>
    Public Class TSeisakuEventGousyaKataPriceRirekiVo
        '発行№
        Private _HakouNo As String
        'バックアップ日
        Private _BackupBi As String
        'ブロック№
        Private _BlockNo As String
        '型費・トリム予算
        Private _KataTYosan As Decimal
        '型費・トリム見通し
        Private _KataTMitoshi As Decimal
        '型費・メタル予算
        Private _KataMYosan As Decimal
        '型費・メタル見通し
        Private _KataMMitoshi As Decimal
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

        ''' <summary>
        ''' 発行№
        ''' </summary>
        ''' <value></value>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Property HakouNo() As String
            Get
                Return _HakouNo
            End Get
            Set(ByVal value As String)
                _HakouNo = value
            End Set
        End Property

        ''' <summary>
        ''' バックアップ
        ''' </summary>
        ''' <value></value>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Property BackupBi() As String
            Get
                Return _BackupBi
            End Get
            Set(ByVal value As String)
                _BackupBi = value
            End Set
        End Property

        ''' <summary>
        ''' ブロックNo.
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
        ''' 型費・トリム予算
        ''' </summary>
        ''' <value></value>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Property KataTYosan() As Decimal
            Get
                Return _KataTYosan
            End Get
            Set(ByVal value As Decimal)
                _KataTYosan = value
            End Set
        End Property

        ''' <summary>
        ''' 型費・トリム見通し
        ''' </summary>
        ''' <value></value>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Property KataTMitoshi() As Decimal
            Get
                Return _KataTMitoshi
            End Get
            Set(ByVal value As Decimal)
                _KataTMitoshi = value
            End Set
        End Property

        ''' <summary>
        ''' 型費・メタル予算
        ''' </summary>
        ''' <value></value>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Property KataMYosan() As Decimal
            Get
                Return _KataMYosan
            End Get
            Set(ByVal value As Decimal)
                _KataMYosan = value
            End Set
        End Property

        ''' <summary>
        ''' 型費・メタル見通し
        ''' </summary>
        ''' <value></value>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Property KataMMitoshi() As Decimal
            Get
                Return _KataMMitoshi
            End Get
            Set(ByVal value As Decimal)
                _KataMMitoshi = value
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
