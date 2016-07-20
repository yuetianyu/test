Imports ShisakuCommon
Imports EBom.Common
Imports EBom.Common.mdlConstraint
Imports EBom.Data
Imports ShisakuCommon.Db.EBom.Dao.Impl
Imports ShisakuCommon.Db.EBom.Dao
Imports ShisakuCommon.Db.EBom.Vo
Imports ShisakuCommon.Db.EBom
Imports ShisakuCommon.Ui
Imports ShisakuCommon.Ui.Spd
Imports EventSakusei.ShisakuBuhinEditEvent.Dao
Imports EventSakusei.ShisakuBuhinEditBlock.Dao
'↓↓2014/10/02 酒井 ADD BEGIN
Imports EventSakusei.ShisakuBuhinMenu.Dao
Imports EventSakusei.ShisakuBuhinMenu.Logic.Impl
'↑↑2014/10/02 酒井 ADD END

Namespace ShisakuBuhinSakuseiList
    Public Class DispShisakuBuhinSakuseiList
#Region " メンバー変数 "
        ''' <summary>ビュー</summary>
        Private m_view As Frm7DispShisakuBuhinSakuseiList
        ''' <summary>FpSpread 共通</summary>
        Private m_spCom As SpreadCommon
        ''' <summary>Excel出力タイトル</summary>
        Private lstExcelTitle As New List(Of String)
        ''' <summary>Excel出力項目</summary>
        Private lstExcelItem As New List(Of String)
        ''' <summary>列が自動ソートされた、列の自動フィルタリングが実行された条件</summary>
        Private filterAndSort As ShisakuBuhinSakuseiListFilterAndSortVo

        'Public Shared FILTER_NON_BLANKS_STRING As String = "a"
        'Public Shared FILTER_ALL_STRING As String = "b"
        'Public Shared FILTER_BLANKS_STRING As String = "d"

        Private Const TAG_SHISAKU_EVENT_CODE As String = "SHISAKU_EVENT_CODE"
        Private Const TAG_SHISAKU_KAIHATSU_FUGO As String = "SHISAKU_KAIHATSU_FUGO"
        Private Const TAG_SHISAKU_EVENT_PHASE_NAME As String = "SHISAKU_EVENT_PHASE_NAME"
        Private Const TAG_UNIT_KBN As String = "UNIT_KBN"
        Private Const TAG_SHISAKU_EVENT_NAME As String = "SHISAKU_EVENT_NAME"
        Private Const TAG_DAISUU As String = "DAISUU"
        Private Const TAG_HACHU As String = "HACHU"
        Private Const TAG_SEKKEI_TENKAIBI As String = "SEKKEI_TENKAIBI"
        Private Const TAG_KAITEI_SHOCHI_SHIMEKIRIBI As String = "KAITEI_SYOCHI_SHIMEKIRIBI"
        Private Const TAG_STATUS_NAME As String = "STATUS_NAME"
        Private Const TAG_STATUS As String = "STATUS"

#End Region

#Region " コンストラクタ "
        ''' <summary>
        ''' コンストラクタ
        ''' </summary>
        ''' <param name="f"></param>
        ''' <remarks></remarks>
        Public Sub New(ByVal f As Frm7DispShisakuBuhinSakuseiList)
            m_view = f
            m_spCom = New SpreadCommon(m_view.spdParts)
            filterAndSort = New ShisakuBuhinSakuseiListFilterAndSortVo()
        End Sub
#End Region

#Region " ビュー初期化 "
        ''' <summary>
        ''' ビューの初期化        ''' </summary>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Function InitView() As RESULT
            '初期起動時には削除ボタンと呼出しボタンを使用不可とする。
            ''↓↓2014/12/23 2試作部品表作成一覧 (TES)張 ADD BEGIN
            '初期起動時に試作1課メニュー画面から遷移した場合は新規作成ボタンと削除ボタンを使用不可とする。
            SetButtonBySisaku()
            ''↑↑2014/12/23 2試作部品表作成一覧 (TES)張 ADD END
            ShisakuFormUtil.setTitleVersion(m_view)
            ShisakuFormUtil.SetIdAndBuka(m_view.LblLoginUserId, m_view.LblLoginBukaName)
            ''Spreadの初期化
            SpreadUtil.Initialize(m_view.spdParts)
            SetSpdColTag()
            SetSpdDataField()
            ''spreadにデータを設定する
            SetSpreadSource()

        End Function
#End Region

#Region "削除ボタンをおしたら　処理する。"
        ''' <summary>
        ''' ボタンを押す
        ''' </summary>
        ''' <remarks></remarks>
        Public Sub DeleteBtnClick()
            '確認画面を表示し、処理続行の確認を行う
            frm01Kakunin.lblKakunin.Text = "削除を実施します。"
            frm7Para = "btnDel"
            frm01Kakunin.ShowDialog()
            If frm7ParaModori.Equals("3") Then
                'バックカラーを戻る
                ResetColor()
                'エラーチェックを行う
                If Not DelCheck() = RESULT.OK Then
                    Exit Sub
                Else
                    DeleteDbData()
                    'spreadから行を削除する
                    For i As Integer = 0 To m_view.spdParts_Sheet1.RowCount - 1
                        If m_spCom.GetValue(TAG_SHISAKU_EVENT_CODE, i).Equals(m_view.txtIbentoNo.Text) Then
                            m_view.spdParts_Sheet1.Rows(i).Remove()
                            Exit For
                        End If
                    Next
                    m_view.txtIbentoNo.Text = ""
                    SetGamenByEventCode()
                End If
            End If
        End Sub

        ''' <summary>
        ''' 削除のエラーチェック チェックエラーがあると　バックカラーは赤
        ''' </summary>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Private Function DelCheck() As RESULT
            Dim rowIndex As Integer = 0
            For i As Integer = 0 To m_view.spdParts_Sheet1.RowCount - 1
                If m_spCom.GetValue(TAG_SHISAKU_EVENT_CODE, i).Equals(m_view.txtIbentoNo.Text) Then
                    rowIndex = i
                    Exit For
                End If
            Next
            Dim status As String = m_spCom.GetValue(TAG_STATUS, rowIndex)

            If status Is Nothing Then
                status = ""
            End If
            If status.Equals(ShisakuGlobal.StatusSekkei) Then
                ShisakuFormUtil.onErro(m_view.spdParts_Sheet1.Rows(rowIndex))
                ComFunc.ShowErrMsgBox(ShisakuMsg.E0026)
                Return RESULT.NG
            End If
            If status.Equals(ShisakuGlobal.StatusKaitei) Then
                ShisakuFormUtil.onErro(m_view.spdParts_Sheet1.Rows(rowIndex))
                ComFunc.ShowErrMsgBox(ShisakuMsg.E0027)
                Return RESULT.NG
            End If
            If status.Equals(ShisakuGlobal.StatusKanryou) Then
                ShisakuFormUtil.onErro(m_view.spdParts_Sheet1.Rows(rowIndex))
                ComFunc.ShowErrMsgBox(ShisakuMsg.E0028)
                Return RESULT.NG
            End If
            Return RESULT.OK
        End Function
        ''' <summary>
        ''' バックカラーを戻る
        ''' </summary>
        ''' <remarks></remarks>
        Public Sub ResetColor()
            For i As Integer = 0 To m_view.spdParts_Sheet1.RowCount - 1
                ShisakuFormUtil.initlColor(m_view.spdParts_Sheet1.Rows(i))
            Next
        End Sub
        ''' <summary>
        ''' イベントコードに該当する試作イベント情報を削除する。
        ''' </summary>
        ''' <remarks></remarks>
        Private Sub DeleteDbData()
            Dim eventCode As String = m_view.txtIbentoNo.Text
            Using db As New EBomDbClient
                db.BeginTransaction()
                '試作イベント情報
                Dim tShisakuEventDaoImpl As TShisakuEventDao = New TShisakuEventDaoImpl()
                tShisakuEventDaoImpl.DeleteByPk(eventCode)
                '試作イベント・ベース車情報
                Dim tShisakuEventBaseDaoImpl As TShisakuEventBaseDao = New TShisakuEventBaseDaoImpl()
                Dim tShisakuEventBaseVo As New TShisakuEventBaseVo()
                tShisakuEventBaseVo.ShisakuEventCode = eventCode
                tShisakuEventBaseDaoImpl.DeleteBy(tShisakuEventBaseVo)
                ''↓↓2014/08/05 Ⅰ.5.EBOM差分出力 c) (TES)施 ADD BEGIN
                Dim tShisakuEventEbomKanshiDaoImpl As TShisakuEventEbomKanshiDao = New TShisakuEventEbomKanshiDaoImpl()
                Dim tShisakuEventEbomKanshiVo As New TShisakuEventEbomKanshiVo()
                tShisakuEventEbomKanshiVo.ShisakuEventCode = eventCode
                tShisakuEventEbomKanshiDaoImpl.DeleteBy(tShisakuEventEbomKanshiVo)

                ''↑↑2014/08/05 Ⅰ.5.EBOM差分出力 c) (TES)施 ADD END
                '試作イベント・完成車情報
                Dim tShisakuEventKanseiDaoImpl As TShisakuEventKanseiDao = New TShisakuEventKanseiDaoImpl()
                Dim tShisakuEventKanseiVo As New TShisakuEventKanseiVo()
                tShisakuEventKanseiVo.ShisakuEventCode = eventCode
                tShisakuEventKanseiDaoImpl.DeleteBy(tShisakuEventKanseiVo)
                '試作イベント・装備仕様
                Dim aShisakuEventSoubiDao As TShisakuEventSoubiDao = New TShisakuEventSoubiDaoImpl()
                Dim aShisakuEventSoubiVo As New TShisakuEventSoubiVo()
                aShisakuEventSoubiVo.ShisakuEventCode = eventCode
                aShisakuEventSoubiDao.DeleteBy(aShisakuEventSoubiVo)
                '試作リストコード情報
                Dim tShisakuListcodeDaoImpl As TShisakuListcodeDao = New TShisakuListcodeDaoImpl()
                Dim tShisakuListcodeVo As New TShisakuListcodeVo()
                tShisakuListcodeVo.ShisakuEventCode = eventCode
                tShisakuListcodeDaoImpl.DeleteBy(tShisakuListcodeVo)
                '試作手配帳情報（基本情報）
                Dim tShisakuTehaiKihonDaoImpl As TShisakuTehaiKihonDao = New TShisakuTehaiKihonDaoImpl()
                Dim tShisakuTehaiKihonVo As New TShisakuTehaiKihonVo()
                tShisakuTehaiKihonVo.ShisakuEventCode = eventCode
                tShisakuTehaiKihonDaoImpl.DeleteBy(tShisakuTehaiKihonVo)
                '試作手配帳情報（号車情報）
                Dim tShisakuTehaiGousyaDaoImpl As TShisakuTehaiGousyaDao = New TShisakuTehaiGousyaDaoImpl()
                Dim tShisakuTehaiGousyaVo As New TShisakuTehaiGousyaVo()
                tShisakuTehaiGousyaVo.ShisakuEventCode = eventCode
                tShisakuTehaiGousyaDaoImpl.DeleteBy(tShisakuTehaiGousyaVo)
                '試作設計ブロック情報
                Dim tShisakuSekkeiBlockDaoImpl As TShisakuSekkeiBlockDao = New TShisakuSekkeiBlockDaoImpl()
                Dim tShisakuSekkeiBlockVo As New TShisakuSekkeiBlockVo()
                tShisakuSekkeiBlockVo.ShisakuEventCode = eventCode
                tShisakuSekkeiBlockDaoImpl.DeleteBy(tShisakuSekkeiBlockVo)
                '試作設計ブロック装備情報
                Dim tShisakuSekkeiBlockSoubiDaoImpl As TShisakuSekkeiBlockSoubiDao = New TShisakuSekkeiBlockSoubiDaoImpl()
                Dim tShisakuSekkeiBlockSoubiVo As New TShisakuSekkeiBlockSoubiVo()
                tShisakuSekkeiBlockSoubiVo.ShisakuEventCode = eventCode
                tShisakuSekkeiBlockSoubiDaoImpl.DeleteBy(tShisakuSekkeiBlockSoubiVo)
                '試作設計ブロック装備仕様情報
                Dim tShisakuSekkeiBlockSoubiShiyouDaoImpl As TShisakuSekkeiBlockSoubiShiyouDao = New TShisakuSekkeiBlockSoubiShiyouDaoImpl()
                Dim tShisakuSekkeiBlockSoubiShiyouVo As New TShisakuSekkeiBlockSoubiShiyouVo()
                tShisakuSekkeiBlockSoubiShiyouVo.ShisakuEventCode = eventCode
                tShisakuSekkeiBlockSoubiShiyouDaoImpl.DeleteBy(tShisakuSekkeiBlockSoubiShiyouVo)
                '試作設計ブロックメモ情報
                Dim tShisakuSekkeiBlockMemoDaoImpl As TShisakuSekkeiBlockMemoDao = New TShisakuSekkeiBlockMemoDaoImpl()
                Dim tShisakuSekkeiBlockMemoVo As New TShisakuSekkeiBlockMemoVo()
                tShisakuSekkeiBlockMemoVo.ShisakuEventCode = eventCode
                tShisakuSekkeiBlockMemoDaoImpl.DeleteBy(tShisakuSekkeiBlockMemoVo)
                '試作設計ブロックINSTAL情報
                Dim tShisakuSekkeiBlockInstlDaoImpl As TShisakuSekkeiBlockInstlDao = New TShisakuSekkeiBlockInstlDaoImpl()
                Dim tShisakuSekkeiBlockInstlVo As New TShisakuSekkeiBlockInstlVo()
                tShisakuSekkeiBlockInstlVo.ShisakuEventCode = eventCode
                tShisakuSekkeiBlockInstlDaoImpl.DeleteBy(tShisakuSekkeiBlockInstlVo)
                ''試作部品情報
                'Dim tShisakuBuhinDaoImpl As TShisakuBuhinDao = New TShisakuBuhinDaoImpl()
                'Dim tShisakuBuhinVo As New TShisakuBuhinVo()
                'tShisakuBuhinVo.ShisakuEventCode = eventCode
                'tShisakuBuhinDaoImpl.DeleteBy(tShisakuBuhinVo)
                ''試作部品構成情報
                'Dim tShisakuBuhinKouseiDaoImpl As TShisakuBuhinKouseiDao = New TShisakuBuhinKouseiDaoImpl()
                'Dim tShisakuBuhinKouseiVo As New TShisakuBuhinKouseiVo()
                'tShisakuBuhinKouseiVo.ShisakuEventCode = eventCode
                'tShisakuBuhinKouseiDaoImpl.DeleteBy(tShisakuBuhinKouseiVo)
                '仕様変更のため、試作部品表編集情報を削除する処理を追加'
                Dim tShisakuBuhinEditDaoImpl As TShisakuBuhinEditDao = New TShisakuBuhinEditDaoImpl()
                Dim tShisakuBuhinEditVo As New TShisakuBuhinEditVo()
                tShisakuBuhinEditVo.ShisakuEventCode = eventCode
                tShisakuBuhinEditDaoImpl.DeleteBy(tShisakuBuhinEditVo)
                '仕様変更のため、試作部品表編集・INSTL情報を削除する処理を追加'
                Dim tShisakuBuhinEditInstlDaoImpl As TShisakuBuhinEditInstlDao = New TShisakuBuhinEditInstlDaoImpl()
                Dim tShisakuBuhinEditInstlVo As New TShisakuBuhinEditInstlVo()
                tShisakuBuhinEditInstlVo.ShisakuEventCode = eventCode
                tShisakuBuhinEditInstlDaoImpl.DeleteBy(tShisakuBuhinEditInstlVo)

                '仕様変更のため、試作部品表編集情報（ベース）を削除する処理を追加'
                Dim tShisakuBuhinEditBaseDaoImpl As TShisakuBuhinEditBaseDao = New TShisakuBuhinEditBaseDaoImpl()
                Dim tShisakuBuhinEditBaseVo As New TShisakuBuhinEditBaseVo()
                tShisakuBuhinEditBaseVo.ShisakuEventCode = eventCode
                tShisakuBuhinEditBaseDaoImpl.DeleteBy(tShisakuBuhinEditBaseVo)
                '仕様変更のため、試作部品表編集（ベース）・INSTL情報を削除する処理を追加'
                Dim tShisakuBuhinEditInstlBaseDaoImpl As TShisakuBuhinEditInstlBaseDao = New TShisakuBuhinEditInstlBaseDaoImpl()
                Dim tShisakuBuhinEditInstlBaseVo As New TShisakuBuhinEditInstlBaseVo()
                tShisakuBuhinEditInstlBaseVo.ShisakuEventCode = eventCode
                tShisakuBuhinEditInstlBaseDaoImpl.DeleteBy(tShisakuBuhinEditInstlBaseVo)

                '試作手配エラー情報
                Dim tShisakuTehaiErrorDaoImpl As TShisakuTehaiErrorDao = New TShisakuTehaiErrorDaoImpl()
                Dim tShisakuTehaiErrorVo As New TShisakuTehaiErrorVo()
                tShisakuTehaiErrorVo.ShisakuEventCode = eventCode
                tShisakuTehaiErrorDaoImpl.DeleteBy(tShisakuTehaiErrorVo)

                '試作手配帳訂正通知基本情報
                Dim tShisakuTehaiTeiseiKihonDaoImpl As TShisakuTehaiTeiseiKihonDao = New TShisakuTehaiTeiseiKihonDaoImpl()
                Dim tShisakuTehaiTeiseiKihonVo As New TShisakuTehaiTeiseiKihonVo()
                tShisakuTehaiTeiseiKihonVo.ShisakuEventCode = eventCode
                tShisakuTehaiTeiseiKihonDaoImpl.DeleteBy(tShisakuTehaiTeiseiKihonVo)

                '試作手配帳訂正通知号車情報
                Dim tShisakuTehaiTeiseiGousyaDaoImpl As TShisakuTehaiTeiseiGousyaDao = New TShisakuTehaiTeiseiGousyaDaoImpl()
                Dim tShisakuTehaiTeiseiGousyaVo As New TShisakuTehaiTeiseiGousyaVo()
                tShisakuTehaiTeiseiGousyaVo.ShisakuEventCode = eventCode
                tShisakuTehaiTeiseiGousyaDaoImpl.DeleteBy(tShisakuTehaiTeiseiGousyaVo)

                '試作手配帳改訂抽出ブロック情報
                '↓↓2014/10/02 酒井 ADD BEGIN
                'Dim tShisakuTehaiKaiteiBlockDaoImpl As TShisakuTehaiKaiteiBlockDao = New TShisakuTehaiKaiteiBlockDaoImpl()
                'Dim tShisakuTehaiKaiteiBlockVo As New TShisakuTehaiKaiteiBlockVo()
                'tShisakuTehaiKaiteiBlockVo.ShisakuEventCode = eventCode
                'tShisakuTehaiKaiteiBlockDaoImpl.DeleteBy(tShisakuTehaiKaiteiBlockVo)
                Dim aListCodeDaoImpl As ListCodeDao = New ListCodeDaoImpl
                aListCodeDaoImpl.DeleteByTehaiKaiteiBlock2(eventCode)
                '↑↑2014/10/02 酒井 ADD END

                '試作部品編集号車情報（改訂抽出）情報
                Dim tShisakuBuhinEditGousyaKaiteiDaoImpl As TShisakuBuhinEditGousyaKaiteiDao = New TShisakuBuhinEditGousyaKaiteiDaoImpl()
                Dim tShisakuBuhinEditGousyaKaiteiVo As New TShisakuBuhinEditGousyaKaiteiVo()
                tShisakuBuhinEditGousyaKaiteiVo.ShisakuEventCode = eventCode
                tShisakuBuhinEditGousyaKaiteiDaoImpl.DeleteBy(tShisakuBuhinEditGousyaKaiteiVo)

                '--------------------------------------------------------------------------------------------------------------
                '２次改修
                '   イベント履歴情報を持つようにしたため、イベント削除時に履歴情報があれば削除するように修正。
                '試作イベント完成車履歴情報
                Dim tShisakuEventKanseiRirekiDaoImpl As TShisakuEventKanseiRirekiDao = New TShisakuEventKanseiRirekiDaoImpl()
                Dim tShisakuEventKanseiRirekiVo As New TShisakuEventKanseiRirekiVo()
                tShisakuEventKanseiRirekiVo.ShisakuEventCode = eventCode
                tShisakuEventKanseiRirekiDaoImpl.DeleteBy(tShisakuEventKanseiRirekiVo)
                '試作イベント・装備仕様履歴情報
                Dim aShisakuEventSoubiRirekiDao As TShisakuEventSoubiRirekiDao = New TShisakuEventSoubiRirekiDaoImpl()
                Dim aShisakuEventSoubiRirekiVo As New TShisakuEventSoubiRirekiVo()
                aShisakuEventSoubiRirekiVo.ShisakuEventCode = eventCode
                aShisakuEventSoubiRirekiDao.DeleteBy(aShisakuEventSoubiRirekiVo)
                '--------------------------------------------------------------------------------------------------------------

                '--------------------------------------------------------------------------------------------------------------
                '３次改修()
                '   試作イベント・ベース車製作一覧情報
                Dim tShisakuEventBaseSeisakuIchiranDaoImpl As TShisakuEventBaseSeisakuIchiranDao = _
                                                                New TShisakuEventBaseSeisakuIchiranDaoImpl()
                Dim tShisakuEventBaseSeisakuIchiranVo As New TShisakuEventBaseSeisakuIchiranVo()
                tShisakuEventBaseSeisakuIchiranVo.ShisakuEventCode = eventCode
                tShisakuEventBaseSeisakuIchiranDaoImpl.DeleteBy(tShisakuEventBaseSeisakuIchiranVo)
                '試作イベント・ベース車情報（登録時の情報）
                Dim tShisakuEventBaseKaiteiDaoImpl As TShisakuEventBaseKaiteiDao = New TShisakuEventBaseKaiteiDaoImpl()
                Dim tShisakuEventBaseKaiteiVo As New TShisakuEventBaseKaiteiVo()
                tShisakuEventBaseKaiteiVo.ShisakuEventCode = eventCode
                tShisakuEventBaseKaiteiDaoImpl.DeleteBy(tShisakuEventBaseKaiteiVo)
                '試作イベント・完成車情報（登録時の情報）
                Dim tShisakuEventKanseiKaiteiDaoImpl As TShisakuEventKanseiKaiteiDao = New TShisakuEventKanseiKaiteiDaoImpl()
                Dim tShisakuEventKanseiKaiteiVo As New TShisakuEventKanseiKaiteiVo()
                tShisakuEventKanseiKaiteiVo.ShisakuEventCode = eventCode
                tShisakuEventKanseiKaiteiDaoImpl.DeleteBy(tShisakuEventKanseiKaiteiVo)
                '試作イベント・装備仕様（登録時の情報）
                Dim aShisakuEventSoubiKaiteiDao As TShisakuEventSoubiKaiteiDao = New TShisakuEventSoubiKaiteiDaoImpl()
                Dim aShisakuEventSoubiKaiteiVo As New TShisakuEventSoubiKaiteiVo()
                aShisakuEventSoubiKaiteiVo.ShisakuEventCode = eventCode
                aShisakuEventSoubiKaiteiDao.DeleteBy(aShisakuEventSoubiKaiteiVo)

                '--------------------------------------------------------------------------------------------------------------

                '試作手配出図実績情報
                Dim aShisakuTehaiShutuzuJisekiDao As TShisakuTehaiShutuzuJisekiDao = New TShisakuTehaiShutuzuJisekiDaoImpl()
                Dim aShisakuTehaiShutuzuJisekiVo As New TShisakuTehaiShutuzuJisekiVo()
                aShisakuTehaiShutuzuJisekiVo.ShisakuEventCode = eventCode
                aShisakuTehaiShutuzuJisekiDao.DeleteBy(aShisakuTehaiShutuzuJisekiVo)
                '試作手配出図実績手入力情報
                Dim aShisakuTehaiShutuzuJisekiInputDao As TShisakuTehaiShutuzuJisekiInputDao = New TShisakuTehaiShutuzuJisekiInputDaoImpl()
                Dim aShisakuTehaiShutuzuJisekiInputVo As New TShisakuTehaiShutuzuJisekiInputVo()
                aShisakuTehaiShutuzuJisekiInputVo.ShisakuEventCode = eventCode
                aShisakuTehaiShutuzuJisekiInputDao.DeleteBy(aShisakuTehaiShutuzuJisekiInputVo)
                '試作手配出図織込情報
                Dim aShisakuTehaiShutuzuOrikomiDao As TShisakuTehaiShutuzuOrikomiDao = New TShisakuTehaiShutuzuOrikomiDaoImpl()
                Dim aShisakuTehaiShutuzuOrikomiVo As New TShisakuTehaiShutuzuOrikomiVo()
                aShisakuTehaiShutuzuOrikomiVo.ShisakuEventCode = eventCode
                aShisakuTehaiShutuzuOrikomiDao.DeleteBy(aShisakuTehaiShutuzuOrikomiVo)
                '試作手配帳情報（号車グループ情報）
                Dim aShisakuTehaiGousyaGroupDao As TShisakuTehaiGousyaGroupDao = New TShisakuTehaiGousyaGroupDaoImpl()
                Dim aShisakuTehaiGousyaGroupVo As New TShisakuTehaiGousyaGroupVo()
                aShisakuTehaiGousyaGroupVo.ShisakuEventCode = eventCode
                aShisakuTehaiGousyaGroupDao.DeleteBy(aShisakuTehaiGousyaGroupVo)

                '予算書２次追加テーブル
                '   T_YOSAN_SETTEI_LISTCODE
                Dim aYosanSetteiListcodeDao As TYosanSetteiListcodeDao = New TYosanSetteiListcodeDaoImpl()
                Dim aYosanSetteiListcodeVo As New TYosanSetteiListcodeVo()
                aYosanSetteiListcodeVo.ShisakuEventCode = eventCode
                aYosanSetteiListcodeDao.DeleteBy(aYosanSetteiListcodeVo)
                '   T_YOSAN_SETTEI_GOUSYA
                Dim aYosanSetteiGousyaDao As TYosanSetteiGousyaDao = New TYosanSetteiGousyaDaoImpl()
                Dim aYosanSetteiGousyaVo As New TYosanSetteiGousyaVo()
                aYosanSetteiGousyaVo.ShisakuEventCode = eventCode
                aYosanSetteiGousyaDao.DeleteBy(aYosanSetteiGousyaVo)
                '   T_YOSAN_SETTEI_BUHIN_RIREKI
                Dim aYosanSetteiBuhinRirekiDao As TYosanSetteiBuhinRirekiDao = New TYosanSetteiBuhinRirekiDaoImpl()
                Dim aYosanSetteiBuhinRirekiVo As New TYosanSetteiBuhinRirekiVo()
                aYosanSetteiBuhinRirekiVo.ShisakuEventCode = eventCode
                aYosanSetteiBuhinRirekiDao.DeleteBy(aYosanSetteiBuhinRirekiVo)
                '   T_YOSAN_SETTEI_BUHIN
                Dim aYosanSetteiBuhinDao As TYosanSetteiBuhinDao = New TYosanSetteiBuhinDaoImpl()
                Dim aYosanSetteiBuhinVo As New TYosanSetteiBuhinVo()
                aYosanSetteiBuhinVo.ShisakuEventCode = eventCode
                aYosanSetteiBuhinDao.DeleteBy(aYosanSetteiBuhinVo)

                db.Commit()
            End Using

        End Sub
#End Region

#Region "EXCEL出力ボタンを押したら　処理する。"
        ''' <summary>
        ''' EXCELボタンを押す。
        ''' </summary>
        ''' <remarks></remarks>
        Public Sub ExcelBtnClick()
            ResetColor()
            Dim spdExcel As New FarPoint.Win.Spread.FpSpread()
            spdExcel.Sheets.Count = 1
            'EXCEl列タイトルと項目を取得する
            SetTitleAndItemList()
            Dim sheetExcel As FarPoint.Win.Spread.SheetView = spdExcel.Sheets(0)
            sheetExcel.ColumnCount = lstExcelTitle.Count
            sheetExcel.AutoGenerateColumns = False
            '列タグの設定
            SetSheetTag(sheetExcel)
            '列タイトルの設定
            SetSheetTitle(sheetExcel)
            'データフィールドの設定
            SetSheetDataField(sheetExcel)
            'データの取得
            Dim dtExcelData As DataTable = GetExcelData()
            sheetExcel.DataSource = dtExcelData
            SetSpdExcelColPro(spdExcel)
            '2012/01/21
            ExcelCommon.SaveExcelFile("試作部品表作成一覧 " + Now.ToString("MMdd") + Now.ToString("HHmm"), spdExcel, "ShisakuBuhinSakuseiList")
        End Sub
        ''' <summary>
        '''　列タグを設定する
        ''' </summary>
        ''' <param name="sheetExcel"></param>
        ''' <remarks></remarks>
        Private Sub SetSheetTag(ByVal sheetExcel As FarPoint.Win.Spread.SheetView)
            For i As Integer = 0 To sheetExcel.ColumnCount - 1
                sheetExcel.Columns(i).Tag = lstExcelItem(i)
            Next
        End Sub
        ''' <summary>
        ''' 列タイトルを設定する
        ''' </summary>
        ''' <param name="sheetExcel"></param>
        ''' <remarks></remarks>
        Private Sub SetSheetTitle(ByVal sheetExcel As FarPoint.Win.Spread.SheetView)
            For i As Integer = 0 To sheetExcel.ColumnCount - 1
                sheetExcel.ColumnHeader.Columns(i).Label = lstExcelTitle(i)
            Next
        End Sub
        ''' <summary>
        ''' データフィールドを設定する
        ''' </summary>
        ''' <param name="sheetExcel"></param>
        ''' <remarks></remarks>
        Private Sub SetSheetDataField(ByVal sheetExcel As FarPoint.Win.Spread.SheetView)
            For i As Integer = 0 To sheetExcel.ColumnCount - 1
                sheetExcel.Columns(i).DataField = lstExcelItem(i)
            Next
        End Sub
        ''' <summary>
        ''' Excel出力用データを取得する
        ''' </summary>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Private Function GetExcelData() As DataTable
            Dim dtData As New DataTable
            Using db As New SqlAccess(g_kanrihyoIni(DB_KEY_EBOM))
                db.Open()
                db.Fill(GetBuhinSakuseiListExcel(filterAndSort), dtData)
            End Using
            Dim col As New DataColumn
            col.ColumnName = "KOUBAN"
            dtData.Columns.Add(col)
            Dim kouban As Integer = 1
            For i As Integer = 0 To dtData.Rows.Count - 1
                If Not i = 0 AndAlso Not dtData.Rows(i)("SHISAKU_EVENT_CODE").Equals(dtData.Rows(i - 1)("SHISAKU_EVENT_CODE")) Then
                    kouban += 1
                End If
                dtData.Rows(i)("KOUBAN") = kouban
            Next
            Return dtData
        End Function
        ''' <summary>
        ''' EXCEl列タイトルと項目を取得する
        ''' </summary>
        ''' <remarks></remarks>
        Private Sub SetTitleAndItemList()
            '1
            lstExcelTitle.Add("項番")
            lstExcelItem.Add("KOUBAN")
            '2
            lstExcelTitle.Add("イベントコード")
            lstExcelItem.Add("SHISAKU_EVENT_CODE")
            '3
            lstExcelTitle.Add("開発符号")
            lstExcelItem.Add("SHISAKU_KAIHATSU_FUGO")
            '4
            lstExcelTitle.Add("イベント")
            lstExcelItem.Add("SHISAKU_EVENT_PHASE_NAME")
            '5
            lstExcelTitle.Add("ユニット区分")
            lstExcelItem.Add("UNIT_KBN")
            '6
            lstExcelTitle.Add("イベント名称")
            lstExcelItem.Add("SHISAKU_EVENT_NAME")
            '7
            lstExcelTitle.Add("台数")
            lstExcelItem.Add("DAISUU")
            '8
            lstExcelTitle.Add("発注")
            lstExcelItem.Add("HACHU")
            '9
            lstExcelTitle.Add("設計展開日")
            lstExcelItem.Add("SEKKEI_TENKAIBI")
            '10
            lstExcelTitle.Add("訂正処置〆切日")
            lstExcelItem.Add("SHIMEKIRIBI")
            '11
            lstExcelTitle.Add("状態")
            lstExcelItem.Add("STATUS_NAME")
            '12
            lstExcelTitle.Add("リストコード")
            lstExcelItem.Add("SHISAKU_LIST_CODE")
            '13
            lstExcelTitle.Add("グループ")
            lstExcelItem.Add("SHISAKU_GROUP_NO")
            '14
            lstExcelTitle.Add("工事指令No.")
            lstExcelItem.Add("SHISAKU_KOUJI_SHIREI_NO")
            '15
            lstExcelTitle.Add("イベント名称")
            lstExcelItem.Add("SHISAKU_EVENT_NAME2")
            '16
            lstExcelTitle.Add("台数")
            lstExcelItem.Add("SHISAKU_DAISU")
            '17
            lstExcelTitle.Add("工事区分")
            lstExcelItem.Add("SHISAKU_KOUJI_KBN")
            '18
            lstExcelTitle.Add("製品区分")
            lstExcelItem.Add("SHISAKU_SEIHIN_KBN")
            '19
            lstExcelTitle.Add("工事No.")
            lstExcelItem.Add("SHISAKU_KOUJI_NO")
            '20
            lstExcelTitle.Add("改訂")
            lstExcelItem.Add("SHISAKU_LIST_CODE_KAITEI_NO")
            '21
            lstExcelTitle.Add("メモ")
            lstExcelItem.Add("SHISAKU_MEMO")
        End Sub
        ''' <summary>
        ''' 列のセルの水平方向の配置を設定する。
        ''' </summary>
        ''' <remarks></remarks>
        Private Sub SetSpdExcelColPro(ByVal spdExcel As FarPoint.Win.Spread.FpSpread)
            Dim horiLeft() As String = New String() {lstExcelItem(1), lstExcelItem(2), lstExcelItem(3), _
                                                     lstExcelItem(4), lstExcelItem(5), lstExcelItem(6), _
                                                     lstExcelItem(7), lstExcelItem(10), lstExcelItem(11), _
                                                     lstExcelItem(12), lstExcelItem(13), lstExcelItem(14), _
                                                     lstExcelItem(15), lstExcelItem(17), lstExcelItem(18), lstExcelItem(20)}
            Dim horiRight() As String = New String() {lstExcelItem(0), lstExcelItem(8), lstExcelItem(9), lstExcelItem(16), lstExcelItem(19)}
            Dim spExcelCom = New SpreadCommon(spdExcel)
            For i As Integer = 0 To horiLeft.Length - 1
                spExcelCom.GetColFromTag(horiLeft(i)).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Left
            Next
            For i As Integer = 0 To horiRight.Length - 1
                spExcelCom.GetColFromTag(horiRight(i)).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Right
            Next
        End Sub
#End Region

#Region "列が自動ソートされたら　処理する "
        Public Sub AutoSortedColumn(ByVal sender As System.Object, ByVal e As FarPoint.Win.Spread.AutoSortedColumnEventArgs)
            ResetColor()
            filterAndSort.SortItem = m_view.spdParts_Sheet1.Columns(e.Column).Tag
            filterAndSort.SortAscending = e.Ascending
        End Sub
#End Region

#Region "列の自動フィルタリングが実行されたら　処理する "
        Public Sub AutoFilteredColumn(ByVal sender As System.Object, ByVal e As FarPoint.Win.Spread.AutoFilteredColumnEventArgs)
            ResetColor()
            Select Case m_view.spdParts_Sheet1.Columns(e.Column).Tag
                Case TAG_SHISAKU_KAIHATSU_FUGO
                    filterAndSort.KaihatsuFugo = FilterStringFormat(e.FilterString)
                Case TAG_SHISAKU_EVENT_PHASE_NAME
                    filterAndSort.EventPhaseName = FilterStringFormat(e.FilterString)
                Case TAG_UNIT_KBN
                    filterAndSort.UnitKbn = FilterStringFormat(e.FilterString)
                Case TAG_STATUS_NAME
                    filterAndSort.StatusName = FilterStringFormat(e.FilterString)
            End Select
        End Sub
        Public Function FilterStringFormat(ByVal filterString As String) As String
            If filterString.Equals(m_view.spdParts_Sheet1.RowFilter.NonBlanksString) Then
                Return m_view.spdParts_Sheet1.RowFilter.NonBlanksString

            End If
            If filterString.Equals(m_view.spdParts_Sheet1.RowFilter.AllString) Then
                Return m_view.spdParts_Sheet1.RowFilter.AllString
            End If
            If filterString.Equals(m_view.spdParts_Sheet1.RowFilter.BlanksString) Then
                Return m_view.spdParts_Sheet1.RowFilter.BlanksString
            End If
            Return filterString
        End Function
#End Region


#Region "排他制御イベント情報を更新する"
        ''' <summary>
        ''' 排他制御ブロック情報の更新処理
        ''' </summary>
        ''' <param name="StrEventCode">イベントコード</param>
        ''' <param name="StrEditMode">編集モード（1:手配担当,2:予算担当）</param>
        ''' <param name="isMode">「登録」の場合、true</param>
        ''' <remarks></remarks>
        Private Sub RegisterMain(ByVal StrEventCode As String, _
                                 ByVal StrEditMode As String, _
                                 ByVal isMode As Boolean)

            Dim aShisakuDate As New ShisakuDate
            Dim eventVo As New TExclusiveControlEventVo
            Dim eventDao As ExclusiveControlEventDao = New ExclusiveControlEventImpl

            'KEY情報
            eventVo.ShisakuEventCode = StrEventCode 'イベントコード
            eventVo.EditMode = StrEditMode         '編集モード（1:手配担当,2:予算担当）
            '編集情報
            eventVo.EditUserId = LoginInfo.Now.UserId
            eventVo.EditDate = aShisakuDate.CurrentDateDbFormat
            eventVo.EditTime = aShisakuDate.CurrentTimeDbFormat
            '作成情報
            eventVo.CreatedUserId = LoginInfo.Now.UserId
            eventVo.CreatedDate = aShisakuDate.CurrentDateDbFormat
            eventVo.CreatedTime = aShisakuDate.CurrentTimeDbFormat
            '更新情報
            eventVo.UpdatedUserId = LoginInfo.Now.UserId
            eventVo.UpdatedDate = aShisakuDate.CurrentDateDbFormat
            eventVo.UpdatedTime = aShisakuDate.CurrentTimeDbFormat

            '追加モードの場合、インサートする。
            If isMode = True Then
                eventDao.InsetByPk(eventVo)
            Else
                eventDao.UpdateByPk(eventVo)
            End If

        End Sub

#End Region

#Region "一覧をダブルクリックしたら　または　呼出しボタンを押したら　編集モードで「試作部品表作成メニュー」へ遷移する"
        Public Sub ToSakuseiMenuUpdateMode(ByVal editMode As String)

            '------------------------------------------------------------------------------------------------------
            '------------------------------------------------------------------------------------------------------
            '２次改修

            '他のユーザーが編集中か確認する。
            Dim tExclusiveControlEventDaoImpl As ExclusiveControlEventDao = New ExclusiveControlEventImpl
            Dim tExclusiveControlEventVo As New TExclusiveControlEventVo()
            Dim isExclusive As Boolean  'true=編集OK、false=編集NG
            Dim isReg As Boolean  'true=追加、false=更新
            Dim tanTousya As String = Nothing
            '担当者名取得用に。
            Dim getDate As New EditBlock2ExcelDaoImpl()
            'イベントコード
            Dim eventCode As String = m_view.txtIbentoNo.Text

            '排他制御イベント情報から試作イベントコードが登録されているかチェック。
            tExclusiveControlEventVo = tExclusiveControlEventDaoImpl.FindByPk(eventCode, editMode)

            If IsNothing(tExclusiveControlEventVo) Then
                'MsgBox("選択したイベントは誰も使用していません。")
                isExclusive = True '編集OK
                isReg = True '追加モード
            Else
                isReg = False '更新モード
                'レコードが有る場合、ログインユーザーと編集者コードを比較する。
                '担当者名を取得する'
                tanTousya = getDate.FindByShainName(tExclusiveControlEventVo.EditUserId)
                '同じなら編集OK。
                If tExclusiveControlEventVo.EditUserId = LoginInfo.Now.UserId Then
                    isExclusive = True '編集OK
                    MsgBox("選択したイベントは下記の方が編集中に異常終了したもようです。編集できます" & vbCrLf & vbCrLf & _
                            "担当者「" & tExclusiveControlEventVo.EditUserId & _
                                     "：" & tanTousya & "」", MsgBoxStyle.OkOnly, "警告")
                Else
                    '違うなら編集NG。
                    isExclusive = False '編集NG
                End If
            End If

            '排他チェック
            If isExclusive = True Then
                '他のユーザーが編集していなければ排他制御イベントテーブルにレコード更新。
                RegisterMain(eventCode, editMode, isReg)

                '続けて処理を続行。

                m_view.Hide()

                If StringUtil.Equals(editMode, TehaiTantoMode) Then
                    Dim frm8DispShisakuBuhinMenu As New ShisakuBuhinMenu.Frm8DispShisakuBuhinMenu(eventCode, editMode, m_view.IsSisaku1Ka)
                    frm8DispShisakuBuhinMenu.ShowDialog()
                    If frm8DispShisakuBuhinMenu.WasUpdate() Then

                        'spreadにデータを設定する
                        SetSpreadSource()
                    End If
                Else
                    Dim frmDispYosanSetteiBuhinMenu As New YosanSetteiBuhinMenu.FrmDispYosanSetteiBuhinMenu(eventCode, editMode)
                    frmDispYosanSetteiBuhinMenu.ShowDialog()
                End If
                m_view.Show()
            Else
                '他のユーザーが編集していれば警告を表示して終了。
                MsgBox("選択したイベントは編集中です。" & vbCrLf & vbCrLf & _
                        "担当者「" & tExclusiveControlEventVo.EditUserId & _
                                 "：" & tanTousya & "」", MsgBoxStyle.OkOnly, "警告")
            End If

        End Sub
#End Region

#Region "一覧の「新規作成」ボタンを押したら　新規モードで「試作部品表作成メニュー」へ遷移する"
        Public Sub ToSakuseiMenuAddMode()
            m_view.Hide()
            Dim frm8DispShisakuBuhinMenu As New ShisakuBuhinMenu.Frm8DispShisakuBuhinMenu()
            frm8DispShisakuBuhinMenu.ShowDialog()

            If frm8DispShisakuBuhinMenu.WasUpdate() Then

                'spreadにデータを設定する
                SetSpreadSource()
            End If
            m_view.Show()
        End Sub
#End Region

#Region " SPREADの列のデータフィールドを設定する "
        ''' <summary>
        ''' SPREADの列のデータフィールドを設定する        ''' </summary>
        ''' <remarks></remarks>
        Public Sub SetSpdDataField()

            '列の項目を設定する           
            For i As Integer = 0 To m_view.spdParts_Sheet1.ColumnCount - 1
                m_view.spdParts_Sheet1.Columns(i).DataField = m_view.spdParts_Sheet1.Columns(i).Tag
            Next
        End Sub
#End Region

#Region " SPREADの列のタグ値を設定する "
        ''' <summary>
        ''' SPREADの列のタグ値を設定する        ''' </summary>
        ''' <remarks></remarks>
        Public Sub SetSpdColTag()
            '列の項目を設定する           
            m_view.spdParts_Sheet1.Columns(0).Tag = TAG_SHISAKU_EVENT_CODE
            m_view.spdParts_Sheet1.Columns(1).Tag = TAG_SHISAKU_KAIHATSU_FUGO
            m_view.spdParts_Sheet1.Columns(2).Tag = TAG_SHISAKU_EVENT_PHASE_NAME
            m_view.spdParts_Sheet1.Columns(3).Tag = TAG_UNIT_KBN
            m_view.spdParts_Sheet1.Columns(4).Tag = TAG_SHISAKU_EVENT_NAME
            m_view.spdParts_Sheet1.Columns(5).Tag = TAG_DAISUU
            m_view.spdParts_Sheet1.Columns(6).Tag = TAG_HACHU
            m_view.spdParts_Sheet1.Columns(7).Tag = TAG_SEKKEI_TENKAIBI
            m_view.spdParts_Sheet1.Columns(8).Tag = TAG_KAITEI_SHOCHI_SHIMEKIRIBI
            m_view.spdParts_Sheet1.Columns(9).Tag = TAG_STATUS_NAME
            m_view.spdParts_Sheet1.Columns(10).Tag = TAG_STATUS
        End Sub
#End Region

#Region "SPREADで 列のセルの水平方向の配置を設定する。行と列のサイズを変更できないことを設定する。"
        Public Sub SetSpdColPro()
            m_spCom.GetColFromTag(TAG_SHISAKU_EVENT_CODE).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Left
            m_spCom.GetColFromTag(TAG_SHISAKU_KAIHATSU_FUGO).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Left
            m_spCom.GetColFromTag(TAG_SHISAKU_EVENT_PHASE_NAME).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Left
            m_spCom.GetColFromTag(TAG_UNIT_KBN).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center
            m_spCom.GetColFromTag(TAG_SHISAKU_EVENT_NAME).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Left
            m_spCom.GetColFromTag(TAG_DAISUU).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Left
            m_spCom.GetColFromTag(TAG_HACHU).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Left
            m_spCom.GetColFromTag(TAG_SEKKEI_TENKAIBI).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Right
            m_spCom.GetColFromTag(TAG_KAITEI_SHOCHI_SHIMEKIRIBI).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Right
            m_spCom.GetColFromTag(TAG_STATUS_NAME).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Left

            '列の幅は変更できるようにする。　2011/2/17　By柳沼
            'For i As Integer = 0 To m_view.spdParts_Sheet1.ColumnCount - 1
            '    m_view.spdParts_Sheet1.Columns(i).Resizable = False
            'Next

            For i As Integer = 0 To m_view.spdParts_Sheet1.RowCount - 1
                m_view.spdParts_Sheet1.Rows(i).Resizable = False
            Next
        End Sub
#End Region

#Region " spreadでデータを取得する "
        Public Function GetIchiranList() As DataTable
            Dim dtData As New DataTable
            Using db As New SqlAccess(g_kanrihyoIni(DB_KEY_EBOM))
                db.Open()
                db.Fill(GetBuhinSakuseiList(), dtData)
            End Using
            Return dtData
        End Function
#End Region

#Region "ボタンを使用可または使用不可とする。"
        ''' <summary>
        ''' 削除ボタンを使用不可とする
        ''' </summary>
        ''' <remarks></remarks>
        Private Sub SetDelLock()
            m_view.btnDel.ForeColor = Color.Black
            m_view.btnDel.BackColor = Color.White
            m_view.btnDel.Enabled = False
        End Sub
        ''' <summary>
        ''' 削除ボタンを使用可とする
        ''' </summary>
        ''' <remarks></remarks>
        Private Sub SetDelUnlock()
            m_view.btnDel.ForeColor = Color.Black
            m_view.btnDel.BackColor = Color.HotPink
            m_view.btnDel.Enabled = True
        End Sub
        ''' <summary>
        ''' 呼出しボタンを使用不可とする
        ''' </summary>
        ''' <remarks></remarks>
        Private Sub SetCallLock()
            m_view.btnCall.ForeColor = Color.Black
            m_view.btnCall.BackColor = Color.White
            m_view.btnCall.Enabled = False
        End Sub
        ''' <summary>
        ''' 呼出しボタンを使用可とする
        ''' </summary>
        ''' <remarks></remarks>
        Private Sub SetCallUnlock()
            m_view.btnCall.ForeColor = Color.Black
            m_view.btnCall.BackColor = Color.LightCyan
            m_view.btnCall.Enabled = True
        End Sub
        '↓↓2014/12/23 2試作部品表作成一覧 (TES)張 ADD BEGIN
        ''' <summary>
        ''' 新規作成ボタンを使用不可とする
        ''' </summary>
        ''' <remarks></remarks>
        Private Sub SetNewLock()
            m_view.btnNEW.ForeColor = Color.Black
            m_view.btnNEW.BackColor = Color.White
            m_view.btnNEW.Enabled = False
        End Sub
        ''' <summary>
        ''' 新規作成ボタンを使用可とする
        ''' </summary>
        ''' <remarks></remarks>
        Private Sub SetNewUnlock()
            m_view.btnNEW.ForeColor = Color.Black
            m_view.btnNEW.BackColor = Color.LightCyan
            m_view.btnNEW.Enabled = True
        End Sub
        ''↑↑2014/12/23 2試作部品表作成一覧 (TES)張 ADD END
#End Region

#Region "イベントコード設定して　呼出しボタンと削除ボタンの使用可かどうかを設定する"
        Public Sub SetGamenByEventCode()
            If m_view.txtIbentoNo.Text.Equals(String.Empty) Then
                SetCallLock()
                SetDelLock()
            Else
                SetCallUnlock()
                ''↓↓2014/12/23 2試作部品表作成一覧 (TES)張 UPD BEGIN
                If m_view.IsSisaku1Ka = 1 Then
                    SetDelLock()
                Else
                    SetDelUnlock()
                End If
                ''↑↑2014/12/23 2試作部品表作成一覧 (TES)張 UPD END
            End If
        End Sub
#End Region
        ''↓↓2014/12/23 2試作部品表作成一覧 (TES)張 ADD BEGIN
#Region "試作１課メニューフラグ設定して　新規作成ボタンボタンと削除ボタンの使用可かどうかを設定する"
        Public Sub SetButtonBySisaku()
            If m_view.IsSisaku1Ka = 1 Then
                SetNewLock()
                SetDelLock()
            Else
                SetNewUnlock()
                SetDelUnlock()
            End If
        End Sub
#End Region
        ''↑↑2014/12/23 2試作部品表作成一覧 (TES)張 ADD END

#Region "spreadのbind"
        Private Sub SetSpreadSource()
            'SPREADのデータソースを設定する
            m_view.spdParts_Sheet1.DataSource = GetIchiranList()
            'filterAndSort再初期化
            'filterAndSort = New ShisakuBuhinSakuseiListFilterAndSortVo()
            'SPREADの列のセルの水平方向の配置を再設定する。
            SetSpdColPro()
            m_view.txtIbentoNo.Text = ""
            SetGamenByEventCode()
        End Sub
#End Region

#Region "試作部品作成一覧"
        ''' <summary>
        ''' 画面でデータを取得する
        ''' </summary>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Shared Function GetBuhinSakuseiList() As String
            Dim sql As New System.Text.StringBuilder()
            With sql
                .Remove(0, .Length)
                .AppendLine("SELECT ")
                .AppendLine("    SHISAKU_EVENT_CODE, ")
                .AppendLine("    SHISAKU_KAIHATSU_FUGO, ")
                .AppendLine("    SHISAKU_EVENT_PHASE_NAME, ")
                .AppendLine("    UNIT_KBN, ")
                .AppendLine("    SHISAKU_EVENT_NAME, ")
                .AppendLine("    LTRIM(STR(ISNULL(SEISAKUDAISU_KANSEISYA,0)))+'+'+LTRIM(STR(ISNULL(SEISAKUDAISU_WB,0)))  AS DAISUU, ")
                '発注
                .AppendLine("    HACHU_UMU, ")
                '発注の有無が'1'の時、「有」と表示する
                .AppendLine("    (CASE WHEN HACHU_UMU = 1 THEN '有'  ELSE '' END) AS HACHU, ")
                'YYYY/MM/DD 
                .AppendFormat("    ({0})  AS SEKKEI_TENKAIBI, ", DataSqlCommon.IntToDateFormatSql("SEKKEI_TENKAIBI"))
                'YYYY/MM/DD 
                .AppendFormat("    ({0})  AS KAITEI_SYOCHI_SHIMEKIRIBI, ", DataSqlCommon.IntToDateFormatSql("KAITEI_SYOCHI_SHIMEKIRIBI"))
                '状態
                .AppendLine("    STATUS, ")
                .AppendLine("    " & MBOM_DB_NAME & ".dbo.M_SHISAKU_STATUS.SHISAKU_STATUS_NAME AS STATUS_NAME ")
                .AppendLine("FROM " & MBOM_DB_NAME & ".dbo.T_SHISAKU_EVENT ")
                .AppendLine("   LEFT JOIN " & MBOM_DB_NAME & ".dbo.M_SHISAKU_STATUS ")
                .AppendLine("    ON " & MBOM_DB_NAME & ".dbo.M_SHISAKU_STATUS.SHISAKU_STATUS_CODE=" & MBOM_DB_NAME & ".dbo.T_SHISAKU_EVENT.STATUS ")
                .AppendLine("ORDER BY SHISAKU_EVENT_CODE,SHISAKU_KAIHATSU_FUGO ")
            End With
            Return sql.ToString()
        End Function
        ''' <summary>
        ''' excel出力のデータを取得する
        ''' </summary>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Shared Function GetBuhinSakuseiListExcel(ByVal filterAndSort As ShisakuBuhinSakuseiListFilterAndSortVo) As String
            Dim sqlWhere As New System.Text.StringBuilder()
            With sqlWhere
                AppendWhere(sqlWhere, "" & MBOM_DB_NAME & ".dbo.T_SHISAKU_EVENT.SHISAKU_KAIHATSU_FUGO", filterAndSort.KaihatsuFugo)
                AppendWhere(sqlWhere, "" & MBOM_DB_NAME & ".dbo.T_SHISAKU_EVENT.SHISAKU_EVENT_PHASE_NAME", filterAndSort.EventPhaseName)
                AppendWhere(sqlWhere, "" & MBOM_DB_NAME & ".dbo.T_SHISAKU_EVENT.UNIT_KBN", filterAndSort.UnitKbn)
                AppendWhere(sqlWhere, "" & MBOM_DB_NAME & ".dbo.M_SHISAKU_STATUS.SHISAKU_STATUS_NAME", filterAndSort.StatusName)
            End With
            Dim sql As New System.Text.StringBuilder()
            With sql
                .Remove(0, .Length)
                .AppendLine("SELECT ")
                .AppendLine("    " & MBOM_DB_NAME & ".dbo.T_SHISAKU_EVENT.SHISAKU_EVENT_CODE, ")
                .AppendLine("    " & MBOM_DB_NAME & ".dbo.T_SHISAKU_EVENT.SHISAKU_KAIHATSU_FUGO, ")
                .AppendLine("    " & MBOM_DB_NAME & ".dbo.T_SHISAKU_EVENT.SHISAKU_EVENT_PHASE_NAME, ")
                .AppendLine("    " & MBOM_DB_NAME & ".dbo.T_SHISAKU_EVENT.UNIT_KBN, ")
                .AppendLine("    " & MBOM_DB_NAME & ".dbo.T_SHISAKU_EVENT.SHISAKU_EVENT_NAME, ")
                .AppendLine("    LTRIM(STR(ISNULL(SEISAKUDAISU_KANSEISYA,0)))+'+'+LTRIM(STR(ISNULL(SEISAKUDAISU_WB,0)))  AS DAISUU, ")
                '発注
                .AppendLine("    " & MBOM_DB_NAME & ".dbo.T_SHISAKU_EVENT.HACHU_UMU, ")
                '発注の有無が'1'の時、「有」と表示する
                .AppendLine("    (CASE WHEN HACHU_UMU = 1 THEN '有'  ELSE '' END) AS HACHU, ")
                'YYYY/MM/DD 
                .AppendFormat("    ({0})  AS SEKKEI_TENKAIBI, ", DataSqlCommon.IntToDateFormatSql("" & MBOM_DB_NAME & ".dbo.T_SHISAKU_EVENT.SEKKEI_TENKAIBI"))
                'YYYY/MM/DD 
                .AppendFormat("    ({0})  AS kAITEI_SYOCHI_SHIMEKIRIBI, ", DataSqlCommon.IntToDateFormatSql("" & MBOM_DB_NAME & ".dbo.T_SHISAKU_EVENT.KAITEI_SYOCHI_SHIMEKIRIBI"))
                '状態
                .AppendLine("    " & MBOM_DB_NAME & ".dbo.T_SHISAKU_EVENT.STATUS, ")
                .AppendLine("    " & MBOM_DB_NAME & ".dbo.M_SHISAKU_STATUS.SHISAKU_STATUS_NAME AS STATUS_NAME, ")
                'T_SHISAKU_LISTCODE
                .AppendLine("    " & MBOM_DB_NAME & ".dbo.T_SHISAKU_LISTCODE.SHISAKU_LIST_CODE,")
                .AppendLine("    " & MBOM_DB_NAME & ".dbo.T_SHISAKU_LISTCODE.SHISAKU_GROUP_NO,")
                .AppendLine("    " & MBOM_DB_NAME & ".dbo.T_SHISAKU_LISTCODE.SHISAKU_KOUJI_SHIREI_NO,")
                .AppendLine("    " & MBOM_DB_NAME & ".dbo.T_SHISAKU_LISTCODE.SHISAKU_EVENT_NAME AS SHISAKU_EVENT_NAME2,")
                .AppendLine("    " & MBOM_DB_NAME & ".dbo.T_SHISAKU_LISTCODE.SHISAKU_DAISU,")
                .AppendLine("    " & MBOM_DB_NAME & ".dbo.T_SHISAKU_LISTCODE.SHISAKU_KOUJI_KBN,")
                .AppendLine("    " & MBOM_DB_NAME & ".dbo.T_SHISAKU_LISTCODE.SHISAKU_SEIHIN_KBN,")
                .AppendLine("    " & MBOM_DB_NAME & ".dbo.T_SHISAKU_LISTCODE.SHISAKU_KOUJI_NO,")
                .AppendLine("    " & MBOM_DB_NAME & ".dbo.T_SHISAKU_LISTCODE.SHISAKU_LIST_CODE_KAITEI_NO,")
                .AppendLine("    " & MBOM_DB_NAME & ".dbo.T_SHISAKU_LISTCODE.SHISAKU_MEMO ")
                .AppendLine("FROM " & MBOM_DB_NAME & ".dbo.T_SHISAKU_EVENT ")
                .AppendLine("   LEFT JOIN " & MBOM_DB_NAME & ".dbo.M_SHISAKU_STATUS ")
                .AppendLine("    ON " & MBOM_DB_NAME & ".dbo.M_SHISAKU_STATUS.SHISAKU_STATUS_CODE=" & MBOM_DB_NAME & ".dbo.T_SHISAKU_EVENT.STATUS ")
                .AppendLine("   LEFT JOIN " & MBOM_DB_NAME & ".dbo.T_SHISAKU_LISTCODE ")
                .AppendLine("    ON " & MBOM_DB_NAME & ".dbo.T_SHISAKU_LISTCODE.SHISAKU_EVENT_CODE=" & MBOM_DB_NAME & ".dbo.T_SHISAKU_EVENT.SHISAKU_EVENT_CODE ")
                'WHERE
                .AppendLine(sqlWhere.ToString)
                'ORDER BY
                If filterAndSort.SortItem = String.Empty Then
                    .AppendLine("ORDER BY SHISAKU_EVENT_CODE")
                Else
                    .AppendFormat("ORDER BY {0} ", filterAndSort.SortItem)
                    If filterAndSort.SortAscending = False Then
                        .AppendLine(" DESC")
                    End If
                    .AppendLine(",SHISAKU_EVENT_CODE")
                End If
            End With
            Return sql.ToString()
        End Function
        Public Shared Sub AppendWhere(ByRef sqlWhere As System.Text.StringBuilder, ByVal item As String, ByVal value As String)

            If Not value = String.Empty AndAlso Not value.Equals(SpreadUtil.FILTER_NON_BLANKS_STRING) Then
                If sqlWhere.Length > 0 Then
                    sqlWhere.AppendFormat(" AND ")
                Else
                    sqlWhere.AppendFormat(" WHERE")
                End If
                If value.Equals(SpreadUtil.FILTER_ALL_STRING) Then
                    sqlWhere.AppendFormat(" ({0} IS NOT NULL AND {0}<>'' )", item)
                ElseIf value.Equals(SpreadUtil.FILTER_BLANKS_STRING) Then
                    sqlWhere.AppendFormat(" ({0} IS  NULL OR {0}='' )", item)
                Else
                    sqlWhere.AppendFormat(" {0}='{1}'", item, value)
                End If
            End If
        End Sub
#End Region
    End Class
End Namespace
