Namespace XVLView
    ''' <summary>
    ''' 
    ''' </summary>
    ''' <remarks></remarks>
    Public Class FrmVeiwerImge

        ''' <summary>画面制御ロジック</summary>
        Private _ViewerImgeLogic As Logic.ViewerImgeSubject

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

        End Sub
        Public Sub New(ByVal aFileName As String)
   
            ' この呼び出しは、Windows フォーム デザイナで必要です。
            InitializeComponent()

            '画面制御ロジック
            _ViewerImgeLogic = New Logic.ViewerImgeSubject(aFileName, Me)

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
                        _ViewerImgeLogic = New Logic.ViewerImgeSubject(lFilename, Me)
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

    End Class

End Namespace