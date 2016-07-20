Imports ShisakuCommon.Util

Namespace YosanSetteiBuhinMenu
    Public Interface Frm8SpdObserver : Inherits Observer

        ''' <summary>
        ''' シートの背景色をクリアする
        ''' </summary>
        ''' <remarks></remarks>
        Sub ClearSheetBackColor()
        ''' <summary>
        ''' シートの入力値・書式をクリアする
        ''' </summary>
        ''' <remarks></remarks>
        Sub ClearSheetData()
        ''' <summary>
        ''' 初期化する
        ''' </summary>
        ''' <remarks></remarks>
        Sub Initialize()
        ''' <summary>
        ''' 再初期化する
        ''' </summary>
        ''' <remarks></remarks>
        Sub ReInitialize()
        ''' <summary>
        ''' 変更時のイベント
        ''' </summary>
        ''' <param name="row">変更したセルの行No</param>
        ''' <param name="column"></param>
        ''' <remarks></remarks>
        Sub OnChange(ByVal row As Integer, ByVal column As Integer)
    End Interface
End Namespace
