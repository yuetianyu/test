Namespace XVLView.Logic

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <remarks></remarks>
    Public Class ViewerImgeSubject

#Region "メンバー変数"
  
        ''' <summary>XVLPLAYERコントロール - ビューコントロールのみ利用可能</summary>
        Private XV_API_VIEW = &H20000000

        ''' <summary>XVLPLAYERコントロール - ファイル操作</summary>
        Private XV_API_OPE_FILE = &H2000000

        ''' <summary>XVLPLAYERコントロール - param1 : 整数．XvReadType 参照 param2 : 文字列．XVL ファイルパス</summary>
        Private XV_VIEW_EXE_READ_MODEL = XV_API_VIEW Or XV_API_OPE_FILE Or &H1

        ' - 指定された XVL ファイルをモデル上に読み込む

        ' -- 指定されたファイルでモデルを開く
        Private XV_READ_OPEN = 1

        ' -- 指定されたファイルをモデルにインポートする
        Private XV_READ_IMPORT = 2

        ''' <summary>３Ｄデータ表示</summary>
        Private _frmVeiwerImge As FrmVeiwerImge

#End Region

#Region "コンストラクタ"

        ''' <summary>
        ''' コンストラクタ
        ''' </summary>
        ''' <param name="aFileName"></param>
        ''' <param name="frm"></param>
        ''' <remarks></remarks>
        Public Sub New(ByVal aFileName As String, ByVal frm As FrmVeiwerImge)

            _frmVeiwerImge = frm

            Dim iFullPath As String = aFileName

            '３Ｄデータ表示
            _frmVeiwerImge.AxXVLPlayer.Execute(XV_VIEW_EXE_READ_MODEL, XV_READ_OPEN, iFullPath, Nothing, Nothing)

        End Sub

        ''' <summary>
        ''' ファイルの追加.
        ''' </summary>
        ''' <param name="aFileName"></param>
        ''' <remarks></remarks>
        Public Sub IMPORT(ByVal aFileName As String)

            Dim obje As Object = Nothing
            obje = _frmVeiwerImge.AxXVLPlayer.Execute(XV_VIEW_EXE_READ_MODEL, XV_READ_IMPORT, aFileName, Nothing, Nothing)

        End Sub

#End Region

    End Class

End Namespace
