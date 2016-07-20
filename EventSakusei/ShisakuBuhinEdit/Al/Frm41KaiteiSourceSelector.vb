Imports ShisakuCommon.Db.EBom.Dao
Imports ShisakuCommon.Db.EBom.Dao.Impl
Imports ShisakuCommon.Db.EBom.Vo
Imports EventSakusei.ShisakuBuhinMenu.Dao

''↓↓2014/08/04 Ⅰ.11.改訂戻し機能_c) (TES)施 ADD BEGIN
''' <summary>
''' 改訂コピー：改訂番号選択
''' </summary>
''' <remarks></remarks>
Public Class Frm41KaiteiSourceSelector

    Public selectedKaiteiNo As String

    Private _count As Integer = 0
    Public ReadOnly Property Count() As Integer
        Get
            Return _count
        End Get
    End Property
    Public result As MsgBoxResult

    ''↓↓2014/08/04 Ⅰ.11.改訂戻し機能_d) (TES)施 ADD BEGIN
    Public Sub New(ByVal KaihatsuFugo As String, ByVal EventCode As String, ByVal EventName As String, ByVal BlockNo As String, ByVal BukaCode As String)
        InitializeComponent()

        '画面ヘッダーにKaihatsuFugoを設定する()
        lblKaihatsuFugo.Text = KaihatsuFugo
        '画面ヘッダーにEventNameを設定する()
        lblEventName.Text = EventName
        '画面ヘッダーにBlockNoを設定する()
        lblBlockNo.Text = BlockNo
        '入力不可
        cbbKaiteiNo.DropDownStyle = ComboBoxStyle.DropDownList
        Dim dao As IShisakuSekkeiBlockDao = New ShisakuSekkeiBlockDaoImpl
        Dim kaiteiNoList As List(Of TShisakuSekkeiBlockVo) = dao.GetShisakuSekkeiKaiteiNo(EventCode, BukaCode, BlockNo)

        _count = kaiteiNoList.Count
        cbbKaiteiNo.DataSource = kaiteiNoList
        cbbKaiteiNo.DisplayMember = "ShisakuBlockNoKaiteiNo"
        'cbbKaiteiNo.DataBindings = kaiteiNoList

        '	Select Distinct SHISAKU_BLOCK_NO_KAITEI_NO FROM T_SHISAKU_SEKKEI_BLOCK WHERE SHISAKU_EVENT_CODE=EventCode AND SHISAKU_BUKA_CODE=BukaCode AND SHISAKU_BLOCK_NO=BlockNo																																																																														
        '結果セットを画面コンボボックスに設定する()
        '   _count = 結果セット.COUNT

    End Sub

    ''↑↑2014/08/04 Ⅰ.11.改訂戻し機能 _d) (TES)施 ADD END



    Private Sub btnCancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCancel.Click
        Me.Close()
    End Sub

    Private Sub btnExcute_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnExcute.Click

        selectedKaiteiNo = cbbKaiteiNo.Text
        result = MsgBoxResult.Ok
        Me.Close()
    End Sub
End Class
''↑↑2014/08/04 Ⅰ.11.改訂戻し機能 _c) (TES)施 ADD END
