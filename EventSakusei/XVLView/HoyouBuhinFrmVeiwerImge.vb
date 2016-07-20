Imports EventSakusei.XVLView
Imports EventSakusei.XVLView.Logic

''' <summary>
''' 
''' </summary>
''' <remarks></remarks>
Public Class HoyouBuhinFrmVeiwerImge

    ''' <summary>画面制御ロジック</summary>
    Private _ViewerImgeLogic As Logic.HoyouBuhinViewerImgeSubject

    '''<summary>初期化完了フラグ</summary>>
    Private _InitComplete As Boolean = False

#Region "コンストラクタ"

    ''' <summary>
    ''' コンストラクタ
    ''' </summary>
    ''' <remarks></remarks>
    ''' 
    Public Sub New()

        ' この呼び出しは、Windows フォーム デザイナで必要です。
        InitializeComponent()

        ' InitializeComponent() 呼び出しの後で初期化を追加します。

    End Sub
    Public Sub New(ByVal aFileName As String)
        'Public Sub New(ByVal KaihatsuFugo As String, ByVal BuhinNo As String, ByVal BuhinName As String, ByVal HojyoName As String)

        ' この呼び出しは、Windows フォーム デザイナで必要です。
        InitializeComponent()

        ' InitializeComponent() 呼び出しの後で初期化を追加します。

        'パラメータ退避
        'Dim ViewVo As New ViewerImgeVo
        'ViewVo.KaihatsuFugo = KaihatsuFugo
        'ViewVo.BuhinNo = BuhinNo
        'ViewVo.BuhinName = BuhinName
        'ViewVo.HojyoName = HojyoName

        '画面制御ロジック
        _ViewerImgeLogic = New Logic.HoyouBuhinViewerImgeSubject(aFileName, Me)

        'Initialize()

    End Sub

    Protected Overrides Sub Finalize()
        MyBase.Finalize()
    End Sub

#End Region

#Region "メソッド"

#Region "初期化メイン"

    ''' <summary>
    ''' 初期化メイン
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub Initialize(ByVal aFileNames As List(Of String))
        Try

            Cursor.Current = Cursors.WaitCursor


            _ViewerImgeLogic = Nothing

            For Each lFilename In aFileNames
                If _ViewerImgeLogic Is Nothing Then
                    '先頭のファイルを親として登録.
                    _ViewerImgeLogic = New Logic.HoyouBuhinViewerImgeSubject(lFilename, Me)
                Else
                    '要素を追加する.
                    _ViewerImgeLogic.IMPORT(lFilename)
                End If
            Next

            '初期化完了
            _InitComplete = True

        Catch ex As Exception

            Dim msg As String
            msg = String.Format("画面の初期化中に問題が発生しました(ERR={0})", ex.Message)
            Me.Close()

        Finally
            Cursor.Current = Cursors.Default
        End Try

    End Sub

#End Region
    ''' <summary>
    ''' 親部品に対して部品を追加.
    ''' </summary>
    ''' <param name="aFilename"></param>
    ''' <remarks></remarks>
    Public Sub Import(ByVal aFilename As String)
        _ViewerImgeLogic.IMPORT(aFilename)
    End Sub

#End Region

    Private Sub FrmVeiwerImge_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

    End Sub
End Class
