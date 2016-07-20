Imports EBom.Common
Imports EBom.Data
Imports System.Text

Imports ShisakuCommon
Imports ShisakuCommon.Db.EBom
Imports EventSakusei.XVLView

Namespace ShisakuBuhinMSTMaintenance.BodyMSTMainte.Logic



    Public Class BodyMSTMainteSubject

#Region "定数"

        Dim TagBodyName As String = "TagBodyName"
        Dim TagKaihatuFugo As String = "TagKaihatuFugo"
        Dim TagPartsNo As String = "TagPartsNo"
        Dim TagBlockNo As String = "TagBlockNo"
        Dim TagXVLFileName As String = "TagXVLFileName"
        Dim TagCADDataEventKbn As String = "TagCADDataEventKbn"
        Dim TagKaitei As String = "TagKaitei"
        Public SpreadTagList As New List(Of String)(New String() {TagKaihatuFugo, TagBodyName, TagBlockNo, TagPartsNo, _
                                                                  TagXVLFileName, TagCADDataEventKbn, TagKaitei})

#End Region

#Region "メンバ変数"

        Dim mKaihatuFugo As String
        Dim mBodyName As String
        Dim mSpreadView As FarPoint.Win.Spread.FpSpread
        Dim mSheetView As FarPoint.Win.Spread.SheetView

#End Region

#Region "プロパティ"
        ''' <summary>
        ''' 選択中の開発符号.
        ''' </summary>
        ''' <value></value>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Property KaihatsuFugo() As String
            Get
                Return mKaihatuFugo
            End Get
            Set(ByVal value As String)
                mKaihatuFugo = value
            End Set
        End Property

        ''' <summary>
        ''' 選択中のボディー名
        ''' </summary>
        ''' <value></value>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Property BodyName() As String
            Get
                Return mBodyName
            End Get
            Set(ByVal value As String)
                mBodyName = value
            End Set
        End Property

#End Region

#Region "コンストラクタ"

        ''' <summary>
        ''' コンストラクタ.
        ''' </summary>
        ''' <param name="aKaihatuFugo">初期表示する開発符号を指定</param>
        ''' <param name="aBodyName">初期表示するボディー名を指定</param>
        ''' <param name="aSheetView">データ設定用のスプレッド</param>
        ''' <remarks></remarks>
        Public Sub New(ByVal aKaihatuFugo As String, ByVal aBodyName As String, ByVal aSheetView As FarPoint.Win.Spread.FpSpread)

            ''パラメータ入力チェック.
            'If aKaihatuFugo Is Nothing Then Throw New ArgumentException("開発符号が指定されていません.")
            'If aBodyName Is Nothing Then Throw New ArgumentException("ボディー名が指定されていません.")
            'If aSheetView Is Nothing Then Throw New ArgumentException("シートビューが指定されていません.")

            'パラメータ退避.
            mKaihatuFugo = aKaihatuFugo
            mBodyName = aBodyName
            mSpreadView = aSheetView
            mSheetView = aSheetView.ActiveSheet

            Initialize_Main()

        End Sub

        Protected Overrides Sub Finalize()
            MyBase.Finalize()
        End Sub

#End Region

#Region "初期化"

        Private Sub Initialize_Main()

            'スプレッドにタグを設定
            Initialize_SetTag()

        End Sub


        ''' <summary>
        ''' スプレッドにタグを設定する.
        ''' </summary>
        ''' <remarks></remarks>
        Private Sub Initialize_SetTag()
            Dim iCnt As Integer = 0

            For Each lTag In SpreadTagList

                'コンストからタグを取得指定設定.
                mSheetView.Columns(EzUtil.Increment(iCnt)).Tag = lTag

            Next


        End Sub


#End Region


#Region "コンボボックスアイテムの設定."

        ''' <summary>
        ''' 開発符号コンボボックスのアイテム設定.
        ''' </summary>
        ''' <remarks></remarks>
        Public Sub SetItemToCmbKaihatuFugo(ByRef aComboObject As ComboBox)
            '開発符号一覧を取得してコンボボックスに設定する.

            Dim m_dtItem As New DataTable

            'データ取得
            Using db As New SqlAccess(g_kanrihyoIni(DB_KEY_mBOM))
                db.Fill(DataSqlCommon.GetDevSignSql(), m_dtItem)
            End Using

            EBom.Common.ComFunc.CreateComboBox(aComboObject, m_dtItem, "VALUE", "DISPLAY", False, , )

            '初期値を設定.
            If StringUtil.IsNotEmpty(mKaihatuFugo) Then aComboObject.Text = mKaihatuFugo.ToString

        End Sub


        ''' <summary>
        ''' ボディー名コンボボックスのアイテム設定.
        ''' </summary>
        ''' <remarks></remarks>
        Public Sub SetItemToCmbBodyName(ByRef aComboObject As ComboBox)
            '指定された開発符号からボディー名を取得する.

            Dim iSQL As New DataSqlCommon
            Dim m_dtItem As New DataTable

            'データ取得
            Using db As New SqlAccess(g_kanrihyoIni(DB_KEY_mBOM))
                db.Fill(iSQL.GetBodyNameSql(mKaihatuFugo), m_dtItem)
            End Using

            aComboObject.Text = ""

            EBom.Common.ComFunc.CreateComboBox(aComboObject, m_dtItem, "VALUE", "DISPLAY", False, , )

            '初期値を設定.
            mBodyName = aComboObject.Text

        End Sub

#End Region 'コンボボックスアイテムの設定.

#Region "DBアクセス"


#Region "DBからデータ取得."
        ''' <summary>
        ''' ボディーマスタからデータを取得
        ''' </summary>
        ''' <param name="aKaihatuFugo">開発符号.</param>
        ''' <param name="aBodyName">ボディー名.</param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Function SelectBodyMST(ByVal aKaihatuFugo As String, ByVal aBodyName As String) As List(Of Dao.Vo.BodyMSTMainteVO)
            Dim iData As New List(Of Dao.Vo.BodyMSTMainteVO)

            'データ取得のためのインスタンス作成.
            Dim iDao As New Dao.Impl.BodyMSTMainteImpl

            'パラメータ設定
            Dim iArg As New Dao.Vo.BodyMSTMainteVO
            iArg.KaihatsuFugo = aKaihatuFugo
            iArg.BodyName = aBodyName

            'DBからデータ取得.
            iData = iDao.SelectBodyList(iArg)

            Return iData
        End Function
#End Region

#Region "DBからXVLファイルデータ取得."
        ''' <summary>
        ''' CATIA情報管理テーブルからXVLファイルデータを取得
        ''' </summary>
        ''' <remarks></remarks>
        Public Function SelectXVLFileData() As List(Of Dao.Vo.BodyMSTMainteVO)
            Dim iDao As New EventSakusei.XVLView.Dao.Impl.RHAC2270DaoImpl
            Dim iReturnData As New List(Of Dao.Vo.BodyMSTMainteVO)

            '開発符号列位置の取得.
            Dim iKahatuFugoIndex As Integer = mSpreadView.ActiveSheet.Columns(TagKaihatuFugo).Index
            'ボディー名列位置の取得.
            Dim iBodyNameIndex As Integer = mSheetView.Columns(TagBodyName).Index
            'ブロック番号列位置の取得.
            Dim iBlockNoIndex As Integer = mSheetView.Columns(TagBlockNo).Index
            '部品番号列位置の取得.
            Dim iBuhinNoIndex As Integer = mSheetView.Columns(TagPartsNo).Index
            'XVLファイル名列位置の取得.
            Dim iXVLFileNameIndex As Integer = mSheetView.Columns(TagXVLFileName).Index
            'CADデータイベント区分列位置の取得.
            Dim iCADDataEventKbnIndex As Integer = mSheetView.Columns(TagCADDataEventKbn).Index
            '改訂番号列位置の取得.
            Dim iKaiteiNoIndex As Integer = mSheetView.Columns(TagKaitei).Index

            'スプレッドの各データに対してXVLファイルの引当を行う
            For iRow As Integer = 1 To mSheetView.RowCount - 1
                Dim iData As New List(Of EventSakusei.XVLView.Dao.Vo.RHAC2270Vo)
                Dim iArg As New EventSakusei.XVLView.Dao.Vo.RHAC2270Vo
                Dim iMSTData As New Dao.Vo.BodyMSTMainteVO

                With mSpreadView
                    'スプレッドデータからパラメータセット
                    '開発符号.
                    iArg.KaihatsuFugo = mSheetView.Cells(iRow, iKahatuFugoIndex).Text
                    'ブロック番号.
                    iArg.BlockNo = mSheetView.Cells(iRow, iBlockNoIndex).Text
                    '部品番号.
                    iArg.BuhinNo = mSheetView.Cells(iRow, iBuhinNoIndex).Text

                    '開発符号、ブロック番号、部品番号が有る行に対して処理を行う
                    If Not String.IsNullOrEmpty(mSheetView.Cells(iRow, iKahatuFugoIndex).Text) AndAlso _
                        Not String.IsNullOrEmpty(mSheetView.Cells(iRow, iBlockNoIndex).Text) AndAlso _
                        Not String.IsNullOrEmpty(mSheetView.Cells(iRow, iBuhinNoIndex).Text) Then

                        '最新改訂のボディXVLファイル情報取得
                        iData = iDao.getBODYXVLFileName(iArg)
                    End If

                    '検索結果をボディマスタVoにセット
                    iMSTData.KaihatsuFugo = iArg.KaihatsuFugo
                    iMSTData.BodyName = mSheetView.Cells(iRow, iBodyNameIndex).Text
                    iMSTData.BlockNo = iArg.BlockNo
                    iMSTData.BuhinNo = iArg.BuhinNo
                    iMSTData.XvlFileName = mSheetView.Cells(iRow, iXVLFileNameIndex).Text
                    iMSTData.CadDataEventKbn = mSheetView.Cells(iRow, iCADDataEventKbnIndex).Text
                    iMSTData.CadKaiteiNo = mSheetView.Cells(iRow, iKaiteiNoIndex).Text

                    'XVLファイル情報は検索結果有りの場合のみセット
                    If iData.Count <> 0 Then
                        iMSTData.XvlFileName = iData(0).XvlFileName
                        iMSTData.CadDataEventKbn = iData(0).CadDataEventKbn
                        iMSTData.CadKaiteiNo = iData(0).CadKaiteiNo
                    End If

                End With

                '戻り値用ボディマスタVoに追加
                iReturnData.Add(iMSTData)
            Next

            Return iReturnData
        End Function
#End Region

#Region "DBからデータ削除"

        ''' <summary>
        ''' DBから開発符号、ボディー名称を指定して削除を行う.
        ''' </summary>
        ''' <param name="aKaihatuFugo">開発符号を指定してください.</param>
        ''' <param name="aBodyName">ボディー名称を指定してださい.</param>
        ''' <param name="aConfirmMsg">削除確認メッセージ表示可否 true:表示 false:非表示</param>
        ''' <returns>削除対象データを返す.</returns>
        ''' <remarks></remarks>
        Public Function DeleteBodyMST(ByVal aKaihatuFugo As String, ByVal aBodyName As String, _
                                      Optional ByVal aConfirmMsg As Boolean = True) As List(Of Dao.Vo.BodyMSTMainteVO)
            DeleteBodyMST = Nothing

            Dim iData As New List(Of Dao.Vo.BodyMSTMainteVO)

            'パラメータ入力チェック.
            If StringUtil.IsEmpty(aKaihatuFugo) Then Throw New ArgumentException("開発符号を指定してください")
            If StringUtil.IsEmpty(aBodyName) Then Throw New ArgumentException("ボディー名称を指定してください")


            '削除を行う為のDAOを作成.
            Dim iDao As New Dao.Impl.BodyMSTMainteImpl
            Dim iDeletData As New List(Of Dao.Vo.BodyMSTMainteVO)

            '削除DAO用パラメータ設定
            Dim iArg As New Dao.Vo.BodyMSTMainteVO
            iArg.KaihatsuFugo = aKaihatuFugo
            iArg.BodyName = aBodyName


            '確認目メッセージ表示オプションのチェック.falseの場合は飛ばす.
            If aConfirmMsg Then

                '削除確認.
                iDeletData = DeleteConfirm(iArg)

                'キャンセルした場合はiDeletDataがNothingとなる.
                If iDeletData Is Nothing Then Exit Function

            End If

            'DBからデータ取得.
            iDao.DeleteBodyList(iArg)

            DeleteBodyMST = iDeletData
        End Function


        ''' <summary>
        ''' 削除前データ存在チェック.
        ''' </summary>
        ''' <param name="aArg"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Private Function DeleteConfirm(ByVal aArg As Dao.Vo.BodyMSTMainteVO) As List(Of Dao.Vo.BodyMSTMainteVO)
            '削除を行う為のDAOを作成.
            Dim iDao As New Dao.Impl.BodyMSTMainteImpl
            Dim iDeletData As New List(Of Dao.Vo.BodyMSTMainteVO)

            DeleteConfirm = iDeletData


            '削除対象のデータを抽出作成.
            iDeletData = iDao.SelectBodyList(aArg)

            '削除確認.
            Dim iDeleteConfirmMsg As New StringBuilder

            '削除対象データが存在しないい場合はエラーをThrowする.
            If iDeletData.Count = 0 Then Throw New ArgumentException("削除対象のデータは存在しません。")

            iDeleteConfirmMsg.AppendLine("                                   ")
            iDeleteConfirmMsg.AppendLine(String.Format("開発符号    ：{0} ", aArg.KaihatsuFugo))
            iDeleteConfirmMsg.AppendLine(String.Format("ボディー名    ：{0}", aArg.BodyName))
            iDeleteConfirmMsg.AppendLine("")
            iDeleteConfirmMsg.AppendLine("")
            iDeleteConfirmMsg.AppendLine(String.Format("この条件の削除対象のデータは {0} 件あります。", iDeletData.Count))
            iDeleteConfirmMsg.AppendLine("削除を行いますか？")
            iDeleteConfirmMsg.AppendLine("")
            If DialogResult.Yes <> MessageBox.Show(iDeleteConfirmMsg.ToString, "削除確認", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) Then
                Exit Function
            End If

            DeleteConfirm = iDeletData
        End Function

#End Region

#Region "DBへデータ登録."
        ''' <summary>
        ''' DB登録処理.
        ''' </summary>
        ''' <remarks></remarks>
        Public Function InsertBodyMST() As Integer
            'スプレッドに登録されているデータを登録する.

            Dim iRepetitionFlg As Boolean = False           '重複キー管理.

            'スプレッドから登録対象のデータ(開発符号、ボディ名)を抽出する.
            Dim iRegDic As Dictionary(Of String, List(Of String)) = SelectRegistKaihatuFugoList()

            '登録件数をカウント.
            Dim iRegistCnt As Integer = 0

            '開発符号列位置の取得.
            Dim iKahatuFugoIndex As Integer = mSheetView.Columns(TagKaihatuFugo).Index
            'ボディー名列位置の取得.
            Dim iBodyNameIndex As Integer = mSheetView.Columns(TagBodyName).Index
            'ブロック番号列位置の取得.
            Dim iBlockNoIndex As Integer = mSheetView.Columns(TagBlockNo).Index
            '部品番号列位置の取得.
            Dim iBuhinNoIndex As Integer = mSheetView.Columns(TagPartsNo).Index
            'XVLファイル名列位置の取得.
            Dim iXvlFileNameIndex As Integer = mSheetView.Columns(TagXVLFileName).Index
            'CADデータイベント区分列位置の取得.
            Dim iCadDataEventKbnIndex As Integer = mSheetView.Columns(TagCADDataEventKbn).Index
            '改訂番号列位置の取得.
            Dim iKaiteiNoIndex As Integer = mSheetView.Columns(TagKaitei).Index
            
            '複数クエリ実行のためコネクションをここで作成.
            Using db As New EBomDbClient
                db.BeginTransaction()

                '開発符号を取得
                For Each lKaihatuFugo In iRegDic
                    'ボディ名称を取得.
                    For Each lBodyName In lKaihatuFugo.Value
                        Dim InsertList As New List(Of Dao.Vo.BodyMSTMainteVO)
                        Dim lSameKeychkBlock As New List(Of String)
                        Dim lSameKeychkParts As New List(Of String)

                        For iRow As Integer = 1 To mSheetView.RowCount - 1
                            'スプレッドから開発符号、ボディ名称が一致する部品番号を総なめする.
                            If mSheetView.Cells(iRow, iKahatuFugoIndex).Value <> lKaihatuFugo.Key Then Continue For
                            If mSheetView.Cells(iRow, iBodyNameIndex).Value <> lBodyName Then Continue For

                            'ブロック番号、部品番号の指定が無い場合は登録対象外
                            If String.IsNullOrEmpty(mSheetView.Cells(iRow, iBlockNoIndex).Text) Then Continue For
                            If String.IsNullOrEmpty(mSheetView.Cells(iRow, iBuhinNoIndex).Text) Then Continue For

                            Dim lInsertRec As New Dao.Vo.BodyMSTMainteVO

                            lInsertRec.KaihatsuFugo = lKaihatuFugo.Key
                            lInsertRec.BodyName = lBodyName
                            lInsertRec.BlockNo = mSheetView.Cells(iRow, iBlockNoIndex).Value
                            lInsertRec.BuhinNo = mSheetView.Cells(iRow, iBuhinNoIndex).Value
                            lInsertRec.XvlFileName = mSheetView.Cells(iRow, iXvlFileNameIndex).Value
                            lInsertRec.CadDataEventKbn = mSheetView.Cells(iRow, iCadDataEventKbnIndex).Value
                            lInsertRec.CadKaiteiNo = mSheetView.Cells(iRow, iKaiteiNoIndex).Value

                            'XVLファイル情報項目がNothingの場合は、空文字を格納
                            If lInsertRec.XvlFileName Is Nothing Then lInsertRec.XvlFileName = String.Empty
                            If lInsertRec.CadDataEventKbn Is Nothing Then lInsertRec.CadDataEventKbn = String.Empty
                            If lInsertRec.CadKaiteiNo Is Nothing Then lInsertRec.CadKaiteiNo = String.Empty

                            If lSameKeychkBlock.Contains(lInsertRec.BlockNo) And _
                                lSameKeychkParts.Contains(lInsertRec.BuhinNo) Then
                                '重複キーあり.
                                iRepetitionFlg = True
                                '重複データカラー変更.
                                mSheetView.Rows(iRow).BackColor = Color.Red

                                '重複データを飛ばすための確認ロジック、重複キーをカラー表示させるロジックを入れたためコメントアウト.
                                'Dim lErrorMsg As New StringBuilder
                                'lErrorMsg.AppendLine(String.Format("開発符号({0})", lInsertRec.KaihatsuFugo))
                                'lErrorMsg.AppendLine(String.Format("ボディー名({0})", lInsertRec.BodyName))
                                'lErrorMsg.AppendLine(String.Format("部品番号({0})", lInsertRec.BuhinNo))
                                'lErrorMsg.AppendLine("")
                                'lErrorMsg.AppendLine("が重複しています。")
                                'lErrorMsg.AppendLine("")
                                'lErrorMsg.AppendLine("重複データを飛ばす場合は「ＯＫ」")
                                'lErrorMsg.AppendLine("登録処理を中断する場合は「キャンセル」")
                                'lErrorMsg.AppendLine("をクリックしてださい。")
                                'If DialogResult.Cancel = MessageBox.Show(lErrorMsg.ToString, "エラー", MessageBoxButtons.OKCancel, MessageBoxIcon.Error) Then
                                '    Exit Function
                                'End If
                                'Continue For
                                '重複データを飛ばすための確認ロジック、重複キーをカラー表示させるロジックを入れたためコメントアウト.
                            End If
                            lSameKeychkBlock.Add(lInsertRec.BlockNo)
                            lSameKeychkParts.Add(lInsertRec.BuhinNo)

                            'DB挿入リストに追加.
                            InsertList.Add(lInsertRec)

                        Next

                        '重複データ存在チェック(重複データが)
                        If iRepetitionFlg Then Continue For


                        '登録前チェック.登録しない場合は次の開発符号、ボディ名へ...
                        If False = RegistConfirm(InsertList) Then Continue For

                        Dim iDao As New Dao.Impl.BodyMSTMainteImpl
                        '開発符号、ボディー名をキーに追加処理を実行.
                        iDao.InsertBodyList(InsertList)

                        '登録件数をインクリメント.
                        iRegistCnt += 1
                    Next

                Next

                '重複キーデータが存在する場合に出力するメッセージ
                If iRepetitionFlg Then
                    Dim lMsg As String = "重複するデータが存在するため、保存できません。"
                    MessageBox.Show(lMsg, "エラー", MessageBoxButtons.OK, MessageBoxIcon.Error)
                    Exit Function
                End If
                '重複キーデータが存在する場合に出力するメッセージ


                db.Commit()

            End Using



            '登録件数を復帰.
            InsertBodyMST = iRegistCnt
        End Function

#Region "登録前チェック."

        ''' <summary>
        ''' 登録前DBチェック
        ''' </summary>
        ''' <remarks></remarks>
        Private Function RegistConfirm(ByVal aInsertList As List(Of Dao.Vo.BodyMSTMainteVO)) As Boolean
            RegistConfirm = False
            Dim iDao As New Dao.Impl.BodyMSTMainteImpl

            '登録DAO用パラメータ設定
            Dim iArg As New Dao.Vo.BodyMSTMainteVO

            Dim iRegistData As List(Of Dao.Vo.BodyMSTMainteVO)
            '登録前確認.
            Dim iDeleteConfirmMsg As New StringBuilder

            iArg.KaihatsuFugo = aInsertList(0).KaihatsuFugo
            iArg.BodyName = aInsertList(0).BodyName

            '登録前に対象のデータが存在するかチェック.
            iRegistData = iDao.SelectBodyList(iArg)

            '削除対象データが存在しないい場合はエラーをThrowする.

            iDeleteConfirmMsg.AppendLine("                                   ")
            iDeleteConfirmMsg.AppendLine(String.Format("開発符号    ：{0} ", iArg.KaihatsuFugo))
            iDeleteConfirmMsg.AppendLine(String.Format("ボディー名    ：{0}", iArg.BodyName))
            iDeleteConfirmMsg.AppendLine("")
            iDeleteConfirmMsg.AppendLine("")
            iDeleteConfirmMsg.AppendLine("登録を行いますよろしいですか？　　　")
            iDeleteConfirmMsg.AppendLine("")
            If DialogResult.Yes <> MessageBox.Show(iDeleteConfirmMsg.ToString, "登録確認", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) Then
                Exit Function
            End If

            'If iRegistData.Count <> 0 Then

            '    iDeleteConfirmMsg.AppendLine("                                   ")
            '    iDeleteConfirmMsg.AppendLine(String.Format("開発符号    ：{0} ", iArg.KaihatsuFugo))
            '    iDeleteConfirmMsg.AppendLine(String.Format("ボディー名    ：{0}", iArg.BodyName))
            '    iDeleteConfirmMsg.AppendLine("")
            '    iDeleteConfirmMsg.AppendLine("")
            '    iDeleteConfirmMsg.AppendLine(String.Format("この条件での登録対象データは {0} 件あります。", iRegistData.Count))
            '    iDeleteConfirmMsg.AppendLine("このまま登録を行った場合データベースのデータは削除され、")
            '    iDeleteConfirmMsg.AppendLine("新たに作成したデータが保存されます。")
            '    iDeleteConfirmMsg.AppendLine("登録を行いますか？")
            '    iDeleteConfirmMsg.AppendLine("")
            '    If DialogResult.Yes <> MessageBox.Show(iDeleteConfirmMsg.ToString, "登録確認", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) Then
            '        Exit Function
            '    End If
            'End If

            RegistConfirm = True
        End Function
#End Region

#End Region

#End Region 'DBアクセス.


#Region "スプレッドにデータセット"
        Dim IsSpreadNowUpdate As Boolean = False     'スプレッド更新中フラグ.
        Dim IsSpreadCalcel As Boolean = False     'スプレッド更新キャンセル.

        ''' <summary>
        ''' スプレッドにヘッダー情報を付加、VOに設定するヘッダー情報を作成.
        ''' </summary>
        ''' <remarks></remarks>
        Public Function SetSpreadHeader(Optional ByVal aSpreadHeaderSet As Boolean = False) As Dao.Vo.BodyMSTMainteVO
            Dim iHeader As New Dao.Vo.BodyMSTMainteVO

            iHeader.BodyName = "ボディー名"
            iHeader.BuhinNo = "部品番号"
            iHeader.KaihatsuFugo = "開発符号"
            iHeader.BlockNo = "ブロック番号"
            iHeader.CADDataEventKbn = "CADデータイベント区分"
            iHeader.CadKaiteiNo = "改訂"
            iHeader.XVLFileName = "XVLファイル名"

            'スプレッドにヘッダー情報を付加する.
            If aSpreadHeaderSet Then
                mSheetView.Cells(1, 1).Text = ""
                mSheetView.Cells(1, 2).Text = ""
                mSheetView.Cells(1, 3).Text = ""
                mSheetView.Cells(1, 4).Text = ""
                mSheetView.Cells(1, 5).Text = ""
                mSheetView.Cells(1, 6).Text = ""
                mSheetView.Cells(1, 7).Text = ""
            End If

            Return iHeader

        End Function


        ''' <summary>
        ''' スプレッドのデータセット.
        ''' </summary>
        ''' <remarks></remarks>
        Public Sub SetSpreadData(ByVal aData As List(Of Dao.Vo.BodyMSTMainteVO))

            ''更新中の場合はとりあえず処理を中断.
            'If IsSpreadNowUpdate Then
            '    'キャンセルフラグ=true
            '    IsSpreadCalcel = True

            '    Dim iThread As New System.Threading.Thread(AddressOf SetSpreadData)
            '    iThread.Start(aData)

            'End If
            '複数条件データ取り込みチェック.
            Dim iMultiCondition As New List(Of String)

            '開発符号列位置の取得.
            Dim iKahatuFugoIndex As Integer = mSpreadView.ActiveSheet.Columns(TagKaihatuFugo).Index
            'ボディー名列位置の取得.
            Dim iBodyNameIndex As Integer = mSheetView.Columns(TagBodyName).Index
            '部品番号列位置の取得.
            Dim iBuhinNoIndex As Integer = mSheetView.Columns(TagPartsNo).Index
            'ブロック番号列位置の取得.
            Dim iBlockNoIndex As Integer = mSheetView.Columns(TagBlockNo).Index
            'XVLファイル名列位置の取得.
            Dim iXVLFileNameIndex As Integer = mSheetView.Columns(TagXVLFileName).Index
            'CADデータイベント区分列位置の取得.
            Dim iCADDataEventKbnIndex As Integer = mSheetView.Columns(TagCADDataEventKbn).Index
            '改訂番号列位置の取得.
            Dim iKaiteiNoIndex As Integer = mSheetView.Columns(TagKaitei).Index
            Try

                IsSpreadNowUpdate = True

                Dim iRow As Integer = 0
                mSheetView.RowCount = 0
                mSheetView.RowCount = aData.Count
                For Each lData In aData
                    Dim iCol As Integer = 0
                    '開発符号.
                    mSheetView.Cells(iRow, iKahatuFugoIndex).Text = lData.KaihatsuFugo
                    'ボディー名.
                    mSheetView.Cells(iRow, iBodyNameIndex).Text = lData.BodyName
                    '部品番号.
                    mSheetView.Cells(iRow, iBuhinNoIndex).Text = lData.BuhinNo
                    'ブロック番号.
                    mSheetView.Cells(iRow, iBlockNoIndex).Text = lData.BlockNo
                    'XVLファイル名.
                    mSheetView.Cells(iRow, iXVLFileNameIndex).Text = lData.XVLFileName
                    'CADデータイベント区分.
                    mSheetView.Cells(iRow, iCADDataEventKbnIndex).Text = lData.CADDataEventKbn
                    '改訂番号.
                    mSheetView.Cells(iRow, iKaiteiNoIndex).Text = lData.CadKaiteiNo

                    ''キャンセル処理
                    'If IsSpreadCalcel Then
                    '    mSheetView.Reset()
                    '    Exit Try
                    'End If
                    Dim iKey As String = String.Format("開発符号{0}_ボディー名{1}", lData.KaihatsuFugo, lData.BodyName)
                    If Not iMultiCondition.Contains(iKey) Then
                        iMultiCondition.Add(iKey)
                    End If


                    EzUtil.Increment(iRow)
                Next


            Catch ex As Exception

            Finally
                '更新中フラグを元に戻す.
                IsSpreadNowUpdate = False
                IsSpreadCalcel = False
            End Try

            '複数条件データ読み込みチェック(ヘッダー行も含んでいるため2以上の場合が複数条件読み込みとなる).
            If 2 < iMultiCondition.Count Then
                Dim lMsg As String = "複数の開発符号またはボディー名が読み込まれました。"
                MessageBox.Show(lMsg, "確認", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            End If


        End Sub

#End Region 'スプレッドのデータセット

#Region "登録対象の開発符号を取得"

        ''' <summary>
        ''' 登録対象
        ''' </summary>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Function SelectRegistKaihatuFugoList() As Dictionary(Of String, List(Of String))
            Dim iDic As New Dictionary(Of String, List(Of String))

            '開発符号列位置の取得.
            Dim iKahatuFugoIndex As Integer = mSheetView.Columns(TagKaihatuFugo).Index
            'ボディー名列位置の取得.
            Dim iBodyNameIndex As Integer = mSheetView.Columns(TagBodyName).Index

            For iRow As Integer = 1 To mSheetView.RowCount - 1
                Dim lKaihatuFugoStr As String = mSheetView.Cells(iRow, iKahatuFugoIndex).Value
                Dim lBodynameStr As String = mSheetView.Cells(iRow, iBodyNameIndex).Value


                If StringUtil.IsEmpty(lKaihatuFugoStr) Then Continue For
                If StringUtil.IsEmpty(lBodynameStr) Then Continue For


                If Not iDic.ContainsKey(lKaihatuFugoStr) Then
                    '開発符号が未登録の場合はボディー名のリストを作成し、開発符号を連想配列に追加する.
                    Dim iList As New List(Of String)
                    iList.Add(lBodynameStr)

                    '開発符号を登録リストに追加.
                    iDic.Add(lKaihatuFugoStr, iList)
                ElseIf Not iDic(lKaihatuFugoStr).Contains(lBodynameStr) Then
                    '開発符号をキーに、ボディ名登録
                    iDic(lKaihatuFugoStr).Add(lBodynameStr)
                Else
                    '登録済みの場合は次のレコードへ.
                End If
            Next

            SelectRegistKaihatuFugoList = iDic
        End Function

#End Region

#Region "エクセル制御"

#Region "エクセル出力"

        ''' <summary>
        ''' エクセル出力.
        ''' </summary>
        ''' <remarks></remarks>
        Public Sub WriteToExcel()

            Dim iFileName As String = String.Format("ボディーマスタ_{0}_{1}_", mKaihatuFugo, mBodyName).ToString
            'Dim iCondition As New System.Text.StringBuilder
            'iCondition.Append(String.Format("イベント：{0}_部課：{1}_取引先：{2}_発注先：{3}_ブロック№：{4})", mArg.EventCode, mArg.BukaCode, mArg.MakerCode, mArg.TehaiMakerCode, mArg.BlockNo))

            Try
                Dim MessageLine1 As String = "Excelファイルに出力しますか？"
                Dim MessageLine2 As String = ""
                'If frm01Kakunin.ConfirmOkCancel(MessageLine1, MessageLine2) <> MsgBoxResult.Ok Then
                '    Return
                'End If
                'Excelファイルにエクスポート
                Dim pathname As String = ShisakuCommon.ShisakuGlobal.ExcelOutPutDir
                Dim fileName As String = iFileName & Now.ToString("yyyyMMddHHmmss") & ".xls"

                '2014/03/17 Add 今泉 出力Excelファイルのシートが保護されてしまうので、一時的にprotectプロパティを解除
                mSheetView.Protect = False

                mSpreadView.SaveExcel(pathname + "\" + fileName, FarPoint.Excel.ExcelSaveFlags.NoFormulas)

                '2014/03/17 Add 今泉 出力Excelファイルのシートが保護されてしまうので、一時的にprotectプロパティを解除
                mSheetView.Protect = True

                'メッセージ
                MessageLine1 = "「" & pathname & "\" & fileName & "」"
                MessageLine2 = "にファイルを出力しました。"
                MessageBox.Show(MessageLine1 & vbCrLf & MessageLine2, iFileName, MessageBoxButtons.OK, MessageBoxIcon.Information)
            Catch ex As Exception
                MessageBox.Show(ex.Message, "異常", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End Try

        End Sub


#End Region

#Region "エクセル取込"

        ''' <summary>
        ''' ファイル読み込み.
        ''' </summary>
        ''' <remarks></remarks>
        Public Function ReadFromExcel() As List(Of Dao.Vo.BodyMSTMainteVO)
            Dim iFilePath As String = "" '= String.Format("ボディーマスタ_{0}_{1}_", aKaihatuFugo, aBodyName).ToString

            'データ格納用VOリストの作成.
            Dim ibuf As New List(Of Dao.Vo.BodyMSTMainteVO)
            '空の戻り値を設定.
            ReadFromExcel = ibuf

            Dim iXls As Common.ExcelRead

            'ファイルパスを取得するダイアログを表示する.
            iFilePath = GetFilePath()

            If iFilePath Is Nothing Then Throw New ArgumentException("Cancel")

            'ファイルリード.
            iXls = New Common.ExcelRead(iFilePath, 7)

            'ヘッダー情報を参照
            '正しくヘッダーが記載されていない場合はエラー？

            '開発符号列位置の取得.
            Dim iKahatuFugoIndex As Integer = mSheetView.Columns(TagKaihatuFugo).Index + 1
            'ボディー名列位置の取得.
            Dim iBodyNameIndex As Integer = mSheetView.Columns(TagBodyName).Index + 1
            'ブロック番号列位置の取得.
            Dim iBlockNoIndex As Integer = mSheetView.Columns(TagBlockNo).Index + 1
            '部品番号列位置の取得.
            Dim iBuhinNoIndex As Integer = mSheetView.Columns(TagPartsNo).Index + 1
            'XVLファイル名列位置の取得.
            Dim iXvlFileNameIndex As Integer = mSheetView.Columns(TagXVLFileName).Index + 1
            'CADデータイベント区分列位置の取得.
            Dim iCadDataEventKbnIndex As Integer = mSheetView.Columns(TagCADDataEventKbn).Index + 1
            '改訂番号列位置の取得.
            Dim iKaiteiNoIndex As Integer = mSheetView.Columns(TagKaitei).Index + 1

            '取り込みファイルヘッダーチェック.
            Dim iHeader As Dao.Vo.BodyMSTMainteVO = SetSpreadHeader()
            If iHeader.KaihatsuFugo <> iXls.bufRead(1, iKahatuFugoIndex) Then Throw New ArgumentException("ヘッダーが異なるためインポート出来ません.")
            If iHeader.BodyName <> iXls.bufRead(1, iBodyNameIndex) Then Throw New ArgumentException("ヘッダーが異なるためインポート出来ません.")
            If iHeader.BlockNo <> iXls.bufRead(1, iBlockNoIndex) Then Throw New ArgumentException("ヘッダーが異なるためインポート出来ません.")
            If iHeader.BuhinNo <> iXls.bufRead(1, iBuhinNoIndex) Then Throw New ArgumentException("ヘッダーが異なるためインポート出来ません.")
            If iHeader.XvlFileName <> iXls.bufRead(1, iXvlFileNameIndex) Then Throw New ArgumentException("ヘッダーが異なるためインポート出来ません.")
            If iHeader.CadDataEventKbn <> iXls.bufRead(1, iCadDataEventKbnIndex) Then Throw New ArgumentException("ヘッダーが異なるためインポート出来ません.")
            If iHeader.CadKaiteiNo <> iXls.bufRead(1, iKaiteiNoIndex) Then Throw New ArgumentException("ヘッダーが異なるためインポート出来ません.")


            'ヘッダー部を読み飛ばすため２行目から読み込み開始.
            For lRowCnt As Integer = 1 To iXls.RowCnt
                'VOに退避させる.
                Dim NewVo As New Dao.Vo.BodyMSTMainteVO
                'ボディー名.
                NewVo.BodyName = iXls.bufRead(lRowCnt, iBodyNameIndex)
                '開発符号.
                NewVo.KaihatsuFugo = iXls.bufRead(lRowCnt, iKahatuFugoIndex)
                'ブロック番号.
                NewVo.BlockNo = iXls.bufRead(lRowCnt, iBlockNoIndex)
                '部品番号.
                NewVo.BuhinNo = iXls.bufRead(lRowCnt, iBuhinNoIndex)
                'XVLファイル名.
                NewVo.XvlFileName = iXls.bufRead(lRowCnt, iXvlFileNameIndex)
                'CADデータイベント区分.
                NewVo.CadDataEventKbn = iXls.bufRead(lRowCnt, iCadDataEventKbnIndex)
                '改訂番号.
                NewVo.CadKaiteiNo = iXls.bufRead(lRowCnt, iKaiteiNoIndex)

                'リストに追加.
                ibuf.Add(NewVo)

            Next

            Return ibuf


        End Function

#End Region

#Region "ファイルパスを取得する."
        ''' <summary>
        ''' ファイルパスを取得するダイアログを表示.
        ''' </summary>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Private Function GetFilePath() As String
            Dim iFilePath As String = ""

            'ファイルダイアログインスタンスの生成.
            Dim fd As OpenFileDialog = New OpenFileDialog()

            fd.Title = "取り込むエクセルファイルを選択してください"
            fd.InitialDirectory = ShisakuCommon.ShisakuGlobal.ExcelOutPutDir
            fd.Filter = "ExcelFile (*.xls)|*.xls"
            'fd.Filter = "All files (*.*)|*.*|All files (*.*)|*.*"
            fd.FilterIndex = 1
            fd.RestoreDirectory = True
            If fd.ShowDialog() = DialogResult.OK Then
                iFilePath = fd.FileName
            End If

            Return iFilePath
        End Function
#End Region

#End Region 'エクセル制御.

    End Class

End Namespace


