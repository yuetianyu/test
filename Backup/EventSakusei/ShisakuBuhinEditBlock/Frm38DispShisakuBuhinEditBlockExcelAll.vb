Imports EBom.Common
Imports EventSakusei.ShisakuBuhinEditBlock.Dao
Imports ShisakuCommon.Util.LabelValue
Imports ShisakuCommon.Ui
Imports EventSakusei.ShisakuBuhinEditBlock.Export2Excel
Imports ShisakuCommon.Db.EBom.Vo
Imports ShisakuCommon



Namespace ShisakuBuhinEditBlock
    ''' <summary>
    ''' 試作部品表編集・改訂編集（ブロック）・全ブロックEXCEL出力画面
    ''' </summary>
    ''' <remarks></remarks>
    Public Class Frm38DispShisakuBuhinEditBlockExcelAll


        Private shisakuEventCode As String
        Private shisakuBukaCode As String
        Friend strButtonFlag As String
        Friend strBlockNo As String
        Private strBlockKaiteNo As String
        Private kaiteiNo As Integer
        Private _strSekkeika As String
        Private _blockNoList As List(Of String)

        ''' <summary>
        ''' コンストラクタ
        ''' </summary>
        ''' <remarks></remarks>
        Public Sub New(ByVal buttonFlag As String, _
                       ByVal strEventCode As String, _
                       ByVal strBukaCode As String, _
                       ByVal strSekkeika As String)

            ' This call is required by the Windows Form Designer.
            InitializeComponent()
            ShisakuFormUtil.Initialize(Me)

            strButtonFlag = buttonFlag
            _blockNoList = Nothing
            shisakuEventCode = strEventCode
            shisakuBukaCode = strBukaCode
            _strSekkeika = strSekkeika

        End Sub

        ''' <summary>
        ''' 最新チェックボックス
        ''' </summary>
        ''' <param name="sender"></param>
        ''' <param name="e"></param>
        ''' <remarks></remarks>
        Private Sub RbtnNew_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RbtnNew.CheckedChanged

        End Sub

        ''' <summary>
        ''' 差分チェックボックス
        ''' </summary>
        ''' <param name="sender"></param>
        ''' <param name="e"></param>
        ''' <remarks></remarks>
        Private Sub RbtnRbtnSabun_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RbtnSabun.CheckedChanged

        End Sub


        ''' <summary>
        ''' 出力ボタン
        ''' </summary>
        ''' <param name="sender"></param>
        ''' <param name="e"></param>
        ''' <remarks></remarks>
        Private Sub btnOutput_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnOutput.Click
            '画面を綺麗に、実行中のカーソルへ変更。
            Application.DoEvents()
            Cursor.Current = Cursors.WaitCursor

            'ラジオボタン
            If RbtnNew.Checked Then
                '最新
                Dim test As New ShisakuBuhinEditBlock2Excel(strButtonFlag, shisakuEventCode, shisakuBukaCode, strBlockNo, strBlockKaiteNo, _strSekkeika, _blockNoList, False, False)
            Else
                '差分
                Dim sabun As New ShisakuBuhinEditCondition(shisakuEventCode, _
                                           shisakuBukaCode, _
                                           strBlockNo, _
                                           strBlockKaiteNo, _
                                           "", _
                                           _blockNoList, _
                                           True)
                sabun.ExportShisakuBuhinEditConditon()
            End If


        End Sub

        ''' <summary>
        ''' キャンセルボタン
        ''' </summary>
        ''' <param name="sender"></param>
        ''' <param name="e"></param>
        ''' <remarks></remarks>
        Private Sub btnCancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCancel.Click
            Me.Close()
        End Sub

        Private Sub Frm38DispShisakuBuhinEditBlockExcelAll_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

        End Sub
    End Class
End Namespace
