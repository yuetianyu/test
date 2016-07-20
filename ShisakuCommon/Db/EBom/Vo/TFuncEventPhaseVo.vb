Namespace Db.EBom.Vo

    Public Class TFuncEventPhaseVo
        '' 現調イベントコード
        Private _GenchoEventCode As String
        '' フェイズNo
        Private _PhaseNo As Integer
        '' フェイズ名称
        Private _PhaseName As String
        '' 取込元種別
        Private _SouceKbn As String
        '' 参照元イベントコード
        Private _SouceEventCode As String

        Private _SyncFlg As String

        '' フェイズ並び順
        Private _PhaseHyoujiNo As Integer
        '' 保存フラグ
        Private _SaveFlg As String

        Private _KoujiShireiNo As String


        '' 作成ユーザーID
        Private _CreatedUserId As String
        '' 作成年月日
        Private _CreatedDate As String
        '' 作成時分秒
        Private _CreatedTime As String
        '' 更新ユーザーID
        Private _UpdatedUserId As String
        '' 更新年月日
        Private _UpdatedDate As String
        '' 更新時分秒
        Private _UpdatedTime As String


        ''' <summary>現調イベントコード</summary>
        ''' <value></value>
        ''' <returns></returns>
        Public Property GenchoEventCode() As String
            Get
                Return _GenchoEventCode
            End Get
            Set(ByVal value As String)
                _GenchoEventCode = value
            End Set
        End Property

        ''' <summary>フェイズNo</summary>
        ''' <value></value>
        ''' <returns></returns>
        Public Property PhaseNo() As Integer
            Get
                Return _PhaseNo
            End Get
            Set(ByVal value As Integer)
                _PhaseNo = value
            End Set
        End Property

        ''' <summary>フェイズ名称</summary>
        ''' <value></value>
        ''' <returns></returns>
        Public Property PhaseName() As String
            Get
                Return _PhaseName
            End Get
            Set(ByVal value As String)
                _PhaseName = value
            End Set
        End Property

        ''' <summary>取込元種別</summary>
        ''' <value></value>
        ''' <returns></returns>
        Public Property SouceKbn() As String
            Get
                Return _SouceKbn
            End Get
            Set(ByVal value As String)
                _SouceKbn = value
            End Set
        End Property

        ''' <summary>参照元イベントコード</summary>
        ''' <value></value>
        ''' <returns></returns>
        Public Property SouceEventCode() As String
            Get
                Return _SouceEventCode
            End Get
            Set(ByVal value As String)
                _SouceEventCode = value
            End Set
        End Property

        Public Property SyncFlg() As String
            Get
                Return _SyncFlg
            End Get
            Set(ByVal value As String)
                _SyncFlg = value
            End Set
        End Property

        ''' <summary>フェイズ並び順</summary>
        ''' <value></value>
        ''' <returns></returns>
        Public Property PhaseHyoujiNo() As Integer
            Get
                Return _PhaseHyoujiNo
            End Get
            Set(ByVal value As Integer)
                _PhaseHyoujiNo = value
            End Set
        End Property

        ''' <summary>保存フラグ</summary>
        ''' <value></value>
        ''' <returns></returns>
        Public Property SaveFlg() As String
            Get
                Return _SaveFlg
            End Get
            Set(ByVal value As String)
                _SaveFlg = value
            End Set
        End Property
        Public Property KoujiShireiNo() As String
            Get
                Return _KoujiShireiNo
            End Get
            Set(ByVal value As String)
                _KoujiShireiNo = value
            End Set
        End Property

        ''' <summary>作成ユーザーID</summary>
        ''' <value></value>
        ''' <returns></returns>
        Public Property CreatedUserId() As String
            Get
                Return _CreatedUserId
            End Get
            Set(ByVal value As String)
                _CreatedUserId = value
            End Set
        End Property

        ''' <summary>作成年月日</summary>
        ''' <value></value>
        ''' <returns></returns>
        Public Property CreatedDate() As String
            Get
                Return _CreatedDate
            End Get
            Set(ByVal value As String)
                _CreatedDate = value
            End Set
        End Property

        ''' <summary>作成時分秒</summary>
        ''' <value></value>
        ''' <returns></returns>
        Public Property CreatedTime() As String
            Get
                Return _CreatedTime
            End Get
            Set(ByVal value As String)
                _CreatedTime = value
            End Set
        End Property

        ''' <summary>更新ユーザーID</summary>
        ''' <value></value>
        ''' <returns></returns>
        Public Property UpdatedUserId() As String
            Get
                Return _UpdatedUserId
            End Get
            Set(ByVal value As String)
                _UpdatedUserId = value
            End Set
        End Property

        ''' <summary>更新年月日</summary>
        ''' <value></value>
        ''' <returns></returns>
        Public Property UpdatedDate() As String
            Get
                Return _UpdatedDate
            End Get
            Set(ByVal value As String)
                _UpdatedDate = value
            End Set
        End Property

        ''' <summary>更新時分秒</summary>
        ''' <value></value>
        ''' <returns></returns>
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
