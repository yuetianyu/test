Imports EBom.Common
Imports EventSakusei.TehaichoSakusei
Imports ShisakuCommon
Imports ShisakuCommon.Util
Imports ShisakuCommon.Ui
Imports ShisakuCommon.Db.Impl
Imports ShisakuCommon.Db.EBom
Imports ShisakuCommon.Db.EBom.Exclusion
Imports ShisakuCommon.Db.EBom.Dao.Impl
Imports ShisakuCommon.Db.EBom.Dao
Imports ShisakuCommon.Db.EBom.Vo
Imports ShisakuCommon.Ui.Access
Imports ShisakuCommon.Db
Imports EventSakusei.TehaichoMenu.Impl
Imports EventSakusei.TehaichoMenu.Dao
Imports EventSakusei.TehaichoMenu
Imports EventSakusei.TehaichoMenu.Vo


Public Class frm1KaiteiCyushutu

    Private shisakuEventCode As String
    Private shisakuListCode As String
    Private shisakuListCodeKaiteiNo As String
    Private kaiteiNo As String
    Private subject As TehaichoMenu.Logic.Frm19TehaichoMenu
    Private shisakuEventName As String
    Private _result As Boolean

    ''' <summary>
    ''' 改訂抽出用情報の設定
    ''' </summary>
    ''' <param name="shisakuEventCode">試作イベントコード</param>
    ''' <param name="shisakuListCode">試作リストコード</param>
    ''' <param name="shisakuListCodeKaiteiNo">試作リストコード改訂No</param>
    ''' <param name="kaiteiNo">改訂No</param>
    ''' <param name="subject">手配帳編集メニュー</param>
    ''' <param name="shisakuEventName">試作イベント名称</param>
    ''' <remarks></remarks>
    Public Sub setEvent(ByVal shisakuEventCode As String, ByVal shisakuListCode As String, ByVal shisakuListCodeKaiteiNo As String, ByVal kaiteiNo As String, ByVal subject As TehaichoMenu.Logic.Frm19TehaichoMenu, ByVal shisakuEventName As String)
        Me.shisakuEventCode = shisakuEventCode
        Me.shisakuListCode = shisakuListCode
        Me.shisakuListCodeKaiteiNo = shisakuListCodeKaiteiNo
        Me.kaiteiNo = kaiteiNo
        Me.subject = subject
        Me.shisakuEventName = shisakuEventName
    End Sub

    ''' <summary>
    ''' Excel抽出ボタン
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        'EXCEL抽出ボタン'
        Application.DoEvents()
        Cursor.Current = Cursors.WaitCursor
        subject.KaiteiChushutu(shisakuEventCode, shisakuListCode, shisakuListCodeKaiteiNo, kaiteiNo)
        Cursor.Current = Cursors.Default
        _result = True
        Me.Close()
    End Sub

    ''' <summary>
    ''' EXCEL抽出後、自動織込みボタン
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub Button2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button2.Click
        'EXCEl抽出後、自動織込みボタン'
        Application.DoEvents()
        Cursor.Current = Cursors.WaitCursor
        subject.KaiteiChushutuAuto(shisakuEventCode, shisakuListCode, shisakuListCodeKaiteiNo, kaiteiNo)
        Cursor.Current = Cursors.Default
        _result = True
        Me.Close()
    End Sub

    ''' <summary>
    ''' キャンセルボタン
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub Button3_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button3.Click
        'キャンセルボタン'
        _result = False
        Me.Close()
    End Sub

    ''' <summary>
    ''' 結果の取得
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public ReadOnly Property getResult() As Boolean
        Get
            Return _result
        End Get
    End Property


    ''' <summary>
    ''' 初期設定
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub New()

        ' この呼び出しは、Windows フォーム デザイナで必要です。
        InitializeComponent()
        ShisakuFormUtil.setTitleVersion(Me)
        ' InitializeComponent() 呼び出しの後で初期化を追加します。

    End Sub

    Private Sub Button4_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button4.Click
        'EXCELベースで抽出'
        Application.DoEvents()
        Cursor.Current = Cursors.WaitCursor
        subject.KaiteiChushutuBase(shisakuEventCode, shisakuListCode, shisakuListCodeKaiteiNo, kaiteiNo)
        Cursor.Current = Cursors.Default
        _result = True
        Me.Close()

    End Sub
End Class