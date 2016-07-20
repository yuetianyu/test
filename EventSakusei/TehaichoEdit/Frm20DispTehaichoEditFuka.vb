Imports EBom.Common
Imports FarPoint.Win
Imports System.Reflection

''' <summary>
''' 手配帳編集画面・手配情報付加
'''  
''' ※ NmDtFukaにより取得データテーブルは項目にアクセス
''' 
''' </summary>
''' <remarks></remarks>
Public Class frm20DispTehaichoEditFuka

#Region "コンストラクタ"
    Public Sub New()

        ' この呼び出しは、Windows フォーム デザイナで必要です。
        InitializeComponent()

    End Sub
#End Region

#Region "メソッド"

#Region " 列インデックス取得 "
    ''' <summary>
    ''' 列タグを元に列インデックスを取得します.
    ''' </summary>
    ''' <param name="tag">列タグ</param>
    ''' <returns>列インデックス</returns>
    ''' <remarks></remarks>
    Private Function GetTagIdx(ByVal tag As String) As Integer

        Dim col As Spread.Column = spdTehaiOpt_Sheet1.Columns(tag)

        If col Is Nothing Then
            Return -1
        End If

        Return col.Index
    End Function

#End Region

#End Region

#Region "イベント"

#Region "ボタン(戻る)"

    ''' <summary>
    ''' 戻るボタン
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub btnBACK_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnBACK.Click
        Me.DialogResult = Windows.Forms.DialogResult.Cancel
        Me.Close()
    End Sub

#End Region

#Region "ボタン(実行)"
    ''' <summary>
    ''' ボタン(実行)
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub btnTehaiJyouhouFuka_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnTehaiJyouhouFuka.Click

        Dim result As Integer = frm00Kakunin.Confirm("確認", "手配付加情報を設定しますか？", "", "OK", "CANCEL")

        If result = MsgBoxResult.Ok Then
            Me.DialogResult = Windows.Forms.DialogResult.OK
            Me.Close()
        Else
            Me.DialogResult = Windows.Forms.DialogResult.Cancel
        End If

    End Sub

#End Region

#End Region

End Class

#Region "テーブル項目名称アクセス用クラス"
''' <summary>
''' テーブル項目名称アクセス用クラス
''' </summary>
''' <remarks></remarks>
Public Class NmDtFuka
    Public Const TEHAI_KIGOU As String = "TEHAI_KIGOU"
    Public Const NOUBA As String = "NOUBA"
    Public Const KYOUKYU_SECTION As String = "KYOUKYU_SECTION"
End Class

#End Region

#Region "手配OPTスプレッドTAGアクセスクラス"
''' <summary>
''' 手配OPTスプレッドTAGアクセスクラス
''' </summary>
''' <remarks></remarks>
Public Class NmSpdTehaiOpt
    Public Const TEHAI_KIGOU As String = "TEHAI_KIGOU"
    Public Const NOUBA As String = "NOUBA"
    Public Const KYOUKYU_SECTION As String = "KYOUKYU_SECTION"
End Class

#End Region

#Region "手配記号付加固定文字列(納場)"
Public Class NmTehaiConst
    '手配記号
    Public Class NmConst_TehaiKigou
        Public Const F As String = "F"
        Public Const BLANK As String = "空白"
        Public Const A As String = "A"
        Public Const D As String = "D"
        Public Const J As String = "J"
        Public Const B As String = "B"
        Public Const M As String = "M"
    End Class
    '納場列
    Public Class NmConst_Nouba
        Public Const FUYOU As String = "不要"
        Public Const X1 As String = "X1"
        Public Const A0 As String = "A0"
        Public Const US As String = "US"
    End Class
    '供給セクション
    Public Class NmConst_KyoukyuSection
        Public Const FUYOU As String = "不要"
        Public Const NO_9SH10 As String = "9SH10"
        Public Const OYA_BUHIN_MAKER As String = "親部品のメーカーコード"
        Public Const NO_9SS00 As String = "9SS00"
    End Class
End Class
#End Region
