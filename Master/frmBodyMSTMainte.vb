Imports EventSakusei
Imports ShisakuCommon.Ui


Public Class FrmBodyMSTMainte

#Region "定数"

#End Region

#Region "メンバ変数"
    Dim mSubject As ShisakuBuhinMSTMaintenance.BodyMSTMainte.Logic.BodyMSTMainteSubject

    Dim mKaihatuFugo As String
    Dim mBodyName As String
#End Region

#Region "プロパティ"

#End Region

#Region "コンストラクタ、デストラクタ"
    Public Sub New()

        Me.New(Nothing, Nothing)

    End Sub

    Public Sub New(ByVal aKaihatuFugo As String, ByVal aBodyName As String)


        ' この呼び出しは、Windows フォーム デザイナで必要です。
        InitializeComponent()

        ' InitializeComponent() 呼び出しの後で初期化を追加します。

        mKaihatuFugo = aKaihatuFugo
        mBodyName = aBodyName

        'フォーム初期化.
        Initialize_Main()

    End Sub

    Protected Overrides Sub Finalize()
        MyBase.Finalize()
    End Sub

#End Region

#Region "初期化"
    ''' <summary>
    ''' 初期化メイン.
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub Initialize_Main()
        Try
            'サブジェクトの作成.
            mSubject = New ShisakuBuhinMSTMaintenance.BodyMSTMainte.Logic.BodyMSTMainteSubject(mKaihatuFugo, mBodyName, spdParts)

        Catch ex As Exception
            MessageBox.Show("初期化中にエラーが発生しました。", "エラー", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    ''' <summary>
    ''' フォームの初期化.
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub Initialize_Form()

        '********************************************
        'フォームタイトル設定
        ShisakuFormUtil.setTitleVersion(Me)
        ShisakuFormUtil.SetDateTimeNow(LblDateNow, LblTimeNow)
        ShisakuFormUtil.SetIdAndBuka(LblCurrUserId, LblCurrBukaName)

        '********************************************
        '開発符号コンボボックスのアイテム設定.
        mSubject.SetItemToCmbKaihatuFugo(cmbKaihatuFugo)

        '********************************************
        'ボディー名コンボボックスのアイテム設定.
        mSubject.SetItemToCmbBodyName(cmbBodyName)

        '********************************************
        'スプレッドのデータセット.
        Dim iBodyData As New List(Of ShisakuBuhinMSTMaintenance.BodyMSTMainte.Dao.Vo.BodyMSTMainteVO)
        'データを取得.
        iBodyData = mSubject.SelectBodyMST(mSubject.KaihatsuFugo, mSubject.BodyName)
        'ヘッダー情報を付加する.
        iBodyData.Insert(0, mSubject.SetSpreadHeader())
        mSubject.SetSpreadData(iBodyData)

        'ハンドラ設定
        AddHandler cmbKaihatuFugo.SelectedIndexChanged, AddressOf cmbKaihatuFugo_SelectedIndexChanged
        AddHandler cmbKaihatuFugo.TextUpdate, AddressOf cmbKaihatuFugo_SelectedIndexChanged



    End Sub

    ''' <summary>
    ''' 戻るボタン
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub btnReturn_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnReturn.Click

        Me.Close()

    End Sub

    ''' <summary>
    ''' アプリケーション終了
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub btnEND_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnEND.Click

        Application.Exit()

    End Sub



#End Region

    ''' <summary>
    ''' 削除ボタン押下.
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub btnDelete_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDelete.Click
        Try
            Dim iDeleteList As List(Of ShisakuBuhinMSTMaintenance.BodyMSTMainte.Dao.Vo.BodyMSTMainteVO)
            '削除処理実施.
            iDeleteList = mSubject.DeleteBodyMST(cmbKaihatuFugo.Text, cmbBodyName.Text)

            If iDeleteList IsNot Nothing Then

                'スプレッドの表示情報を削除
                spdParts_Sheet1.RowCount = 1

                'ボディ名のアイテム情報を更新する.
                mSubject.SetItemToCmbBodyName(cmbBodyName)

                MessageBox.Show("削除しました。　　　　", "削除", MessageBoxButtons.OK, MessageBoxIcon.Information)
            Else
                MessageBox.Show("キャンセルしました。　　　　", "削除", MessageBoxButtons.OK, MessageBoxIcon.Information)
            End If


        Catch exArg As ArgumentException
            'このエラーは想定内.
            MessageBox.Show(exArg.Message, "確認", MessageBoxButtons.OK, MessageBoxIcon.Information)
        Catch ex As Exception
            'このエラーは想定外.
            Throw New Exception(ex.Message)
        End Try

    End Sub

    ''' <summary>
    ''' 登録ボタン押下.
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub btnRegist_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnRegist.Click

        Try
            Dim iRegistCnt As Integer = 0

            If Windows.Forms.DialogResult.Yes <> MessageBox.Show("登録を行います、よろしいですか？", "確認", MessageBoxButtons.YesNo, MessageBoxIcon.Question) Then Exit Try

            '登録処理を実施
            iRegistCnt = mSubject.InsertBodyMST()

            '登録数チェック.
            If 0 < iRegistCnt Then

                'ボディ名のアイテム情報を更新する.
                mSubject.SetItemToCmbBodyName(cmbBodyName)

                MessageBox.Show("登録しました。　　　　", "登録", MessageBoxButtons.OK, MessageBoxIcon.Information)

            End If

        Catch ex As Exception
            'このエラーは想定外.
            MessageBox.Show(ex.Message, "エラー", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try

        '登録処理

    End Sub

    ''' <summary>
    ''' エクセル出力押下.
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub btnExcelOutPut_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnExcelOutPut.Click

        mSubject.WriteToExcel()

    End Sub

    ''' <summary>
    ''' エクセル取込押下
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub btnExcelInput_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnExcelInput.Click
        '課題
        '取り込んだデータに複数のボディー名称が存在する場合の動作は？
        Try

            mSubject.SetSpreadData(mSubject.ReadFromExcel())

            'コンボボックスのテキスト
            cmbKaihatuFugo.Text = ""
            cmbKaihatuFugo_SelectedIndexChanged(Nothing, Nothing)

        Catch exArg As ArgumentException
            If Not exArg.Message.ToUpper.Equals("CANCEL") Then
                MessageBox.Show(exArg.Message, "確認", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            End If
        Catch exIO As IO.IOException
            MessageBox.Show(exIO.Message, "通知", MessageBoxButtons.OK, MessageBoxIcon.Warning)
        Catch ex As Exception
            MessageBox.Show(ex.Message, "エラー", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try

    End Sub

    ''' <summary>
    ''' 検索ボタン押下.
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub btnFind_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnFind.Click
        Dim iData As New List(Of ShisakuBuhinMSTMaintenance.BodyMSTMainte.Dao.Vo.BodyMSTMainteVO)

        'データを取得.
        iData = mSubject.SelectBodyMST(cmbKaihatuFugo.Text, cmbBodyName.Text)

        'ヘッダー情報を付加する.
        iData.Insert(0, mSubject.SetSpreadHeader())

        'スプレッドのデータセット.
        mSubject.SetSpreadData(iData)

    End Sub

    ''' <summary>
    ''' ファイル引当ボタン押下.
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub btnAllocate_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnAllocate.Click
        Dim iData As New List(Of ShisakuBuhinMSTMaintenance.BodyMSTMainte.Dao.Vo.BodyMSTMainteVO)

        'データを取得.
        iData = mSubject.SelectXVLFileData()

        'ヘッダー情報を付加する.
        iData.Insert(0, mSubject.SetSpreadHeader())

        'スプレッドのデータセット.
        mSubject.SetSpreadData(iData)
    End Sub

#Region "コンボボックス_開発符号イベント"

    Private Sub cmbKaihatuFugo_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)

        '選択アイテムを更新する.
        mSubject.KaihatsuFugo = cmbKaihatuFugo.Text

        'ボディ名のアイテム情報を更新する.
        mSubject.SetItemToCmbBodyName(cmbBodyName)

    End Sub

#End Region

End Class
