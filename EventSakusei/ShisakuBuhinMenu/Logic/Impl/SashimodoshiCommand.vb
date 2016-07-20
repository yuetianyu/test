Imports ShisakuCommon.Db.EBom.Vo.Helper
Imports ShisakuCommon.Db
Imports ShisakuCommon
Imports ShisakuCommon.Db.EBom.Dao
Imports ShisakuCommon.Db.EBom.Vo

Namespace ShisakuBuhinMenu.Logic.Impl
    ''' <summary>
    ''' 差戻し処理を担う処理実装クラス
    ''' </summary>
    ''' <remarks></remarks>
    Public Class SashimodoshiCommand : Implements Command
        Private ReadOnly shisakuEventCode As String
        Private ReadOnly login As LoginInfo
        Private ReadOnly aShisakuDao As ShisakuDao
        Private ReadOnly shisakuEventDao As TShisakuEventDao
        Private ReadOnly blockDao As TShisakuSekkeiBlockDao
        Private ReadOnly soubiDao As TShisakuSekkeiBlockSoubiDao
        Private ReadOnly soubiShiyouDao As TShisakuSekkeiBlockSoubiShiyouDao
        Private ReadOnly memoDao As TShisakuSekkeiBlockMemoDao
        Private ReadOnly instlDao As TShisakuSekkeiBlockInstlDao
        'Private ReadOnly buhinDao As TShisakuBuhinDao
        'Private ReadOnly buhinKouseiDao As TShisakuBuhinKouseiDao
        Private ReadOnly buhinEditDao As TShisakuBuhinEditDao
        Private ReadOnly buhinEditInstlDao As TShisakuBuhinEditInstlDao
        Private ReadOnly buhinEditBaseDao As TShisakuBuhinEditBaseDao
        Private ReadOnly buhinEditInstlBaseDao As TShisakuBuhinEditInstlBaseDao
        Private ReadOnly buhinKanseiRirekiDao As TShisakuEventKanseiRirekiDao
        Private ReadOnly buhinSoubiRirekiDao As TShisakuEventSoubiRirekiDao

        Private _newStatus As String
        Public Sub New(ByVal shisakuEventCode As String, _
                       ByVal login As LoginInfo, _
                       ByVal aShisakuDao As ShisakuDao, _
                       ByVal shisakuEventDao As TShisakuEventDao, _
                       ByVal blockDao As TShisakuSekkeiBlockDao, _
                       ByVal soubiDao As TShisakuSekkeiBlockSoubiDao, _
                       ByVal soubiShiyouDao As TShisakuSekkeiBlockSoubiShiyouDao, _
                       ByVal memoDao As TShisakuSekkeiBlockMemoDao, _
                       ByVal instlDao As TShisakuSekkeiBlockInstlDao, _
                       ByVal buhinEditDao As TShisakuBuhinEditDao, _
                       ByVal buhinEditInstlDao As TShisakuBuhinEditInstlDao, _
                       ByVal buhinEditBaseDao As TShisakuBuhinEditBaseDao, _
                       ByVal buhinEditInstlBaseDao As TShisakuBuhinEditInstlBaseDao, _
                       ByVal buhinKanseiRirekiDao As TShisakuEventKanseiRirekiDao, _
                       ByVal buhinSoubiRirekiDao As TShisakuEventSoubiRirekiDao)
            'ByVal buhinDao As TShisakuBuhinDao, _
            'ByVal buhinKouseiDao As TShisakuBuhinKouseiDao, _
            Me.shisakuEventCode = shisakuEventCode
            Me.login = login
            Me.aShisakuDao = aShisakuDao
            Me.shisakuEventDao = shisakuEventDao
            Me.blockDao = blockDao
            Me.soubiDao = soubiDao
            Me.soubiShiyouDao = soubiShiyouDao
            Me.memoDao = memoDao
            Me.instlDao = instlDao
            'Me.buhinDao = buhinDao
            'Me.buhinKouseiDao = buhinKouseiDao
            Me.buhinEditDao = buhinEditDao
            Me.buhinEditInstlDao = buhinEditInstlDao
            Me.buhinEditBaseDao = buhinEditBaseDao
            Me.buhinEditInstlBaseDao = buhinEditInstlBaseDao
            Me.buhinKanseiRirekiDao = buhinKanseiRirekiDao
            Me.buhinSoubiRirekiDao = buhinSoubiRirekiDao
        End Sub
        Public Function GetNewStatus() As String Implements Command.GetNewStatus
            If _newStatus Is Nothing Then
                Throw New InvalidOperationException("#Perform()メソッドを実行してください.")
            End If
            Return _newStatus
        End Function

        Public Sub Perform() Implements Command.Perform
            Dim aDate As New ShisakuDate(aShisakuDao)
            Dim eventCode As String = shisakuEventCode

            ' 試作設計ブロック情報を削除
            DeleteShisakuSekkeiBlock(eventCode)
            ' 試作設計ブロック装備情報を削除
            DeleteShisakuSekkeiBlockSoubi(eventCode)
            ' 試作設計ブロック装備仕様情報を削除
            DeleteShisakusekkeiBlockSoubiShiyou(eventCode)
            ' 試作設計ブロックメモ情報を削除
            DeleteShisakuSekkeiBlockMemo(eventCode)
            '試作設計ブロックINSTL情報を削除()
            DeleteShisakuSekkeiBlockInstl(eventCode)
            '試作部品情報を削除
            'DeleteShisakuBuhin(eventCode)
            '試作部品構成情報を削除
            'DeleteShisakuBuhinKousei(eventCode)
            ' 試作部品編集情報を削除'
            DeleteShisakuBuhinEdit(eventCode)
            '試作部品表編集・INSTL情報を削除'
            DeleteShisakuBuhinEditInstl(eventCode)
            ' 試作部品編集情報（ベース）を削除'
            DeleteShisakuBuhinEditBase(eventCode)
            '試作部品表編集・INSTL情報（ベース）を削除'
            DeleteShisakuBuhinEditInstlBase(eventCode)

            '--------------------------------------------
            '２次改修
            '   完成車及び装備仕様の履歴情報も削除する。
            '   イベント管理の履歴用に新設したファイル
            ' 試作部品編集情報（ベース）を削除'
            DeleteShisakuEventKanseiRireki(eventCode)
            '試作部品表編集・INSTL情報（ベース）を削除'
            DeleteShisakuEventSoubiRireki(eventCode)
            '--------------------------------------------


            ' T_SHISAKU_EVENT ステータス更新
            _newStatus = TShisakuEventVoHelper.Status.SASHIMODOSHI_ING
            UpdateShisakuEventStatusSashimodoshi(eventCode, _newStatus, aDate)
        End Sub

        ''' <summary>
        ''' 試作イベント情報のステータスを「差戻し」にする
        ''' </summary>
        ''' <param name="eventCode">試作イベントコード</param>
        ''' <param name="newStatus">ステータス</param>
        ''' <param name="aDate">試作システム日付</param>
        ''' <remarks></remarks>
        Private Sub UpdateShisakuEventStatusSashimodoshi(ByVal eventCode As String, ByVal newStatus As String, ByVal aDate As ShisakuDate)

            Dim vo As TShisakuEventVo = shisakuEventDao.FindByPk(eventCode)
            vo.Status = newStatus
            vo.SekkeiTenkaibi = Nothing
            vo.KaiteiSyochiShimekiribi = Nothing
            vo.UpdatedUserId = login.UserId
            vo.UpdatedDate = aDate.CurrentDateDbFormat
            vo.UpdatedTime = aDate.CurrentTimeDbFormat
            '2012/03/07 差し戻しで設計展開ステータスを０に戻す
            vo.TenkaiStatus = "0"
            shisakuEventDao.UpdateByPk(vo)
        End Sub

        ''' <summary>
        ''' 試作設計ブロック情報を削除
        ''' </summary>
        ''' <param name="eventCode">試作イベントコード</param>
        ''' <remarks></remarks>
        Private Sub DeleteShisakuSekkeiBlock(ByVal eventCode As String)

            Dim blockVo As New TShisakuSekkeiBlockVo
            blockVo.ShisakuEventCode = eventCode
            blockDao.DeleteBy(blockVo)
        End Sub

        ''' <summary>
        ''' 試作設計ブロック装備情報を削除
        ''' </summary>
        ''' <param name="eventCode">試作イベントコード</param>
        ''' <remarks></remarks>
        Private Sub DeleteShisakuSekkeiBlockSoubi(ByVal eventCode As String)

            Dim soubiVo As New TShisakuSekkeiBlockSoubiVo
            soubiVo.ShisakuEventCode = eventCode
            soubiDao.DeleteBy(soubiVo)
        End Sub

        ''' <summary>
        ''' 試作設計ブロック装備仕様情報を削除する
        ''' </summary>
        ''' <param name="eventCode">試作イベントコード</param>
        ''' <remarks></remarks>
        Private Sub DeleteShisakusekkeiBlockSoubiShiyou(ByVal eventCode As String)

            Dim soubiShiyouVo As New TShisakuSekkeiBlockSoubiShiyouVo
            soubiShiyouVo.ShisakuEventCode = eventCode
            soubiShiyouDao.DeleteBy(soubiShiyouVo)
        End Sub

        ''' <summary>
        ''' 試作設計ブロックメモ情報を削除
        ''' </summary>
        ''' <param name="eventCode">試作イベントコード</param>
        ''' <remarks></remarks>
        Private Sub DeleteShisakuSekkeiBlockMemo(ByVal eventCode As String)

            Dim memoVo As New TShisakuSekkeiBlockMemoVo
            memoVo.ShisakuEventCode = eventCode
            memoDao.DeleteBy(memoVo)
        End Sub

        ''' <summary>
        ''' 試作設計ブロックINSTL情報を削除
        ''' </summary>
        ''' <param name="eventCode">試作イベントコード</param>
        ''' <remarks></remarks>
        Private Sub DeleteShisakuSekkeiBlockInstl(ByVal eventCode As String)

            Dim instlVo As New TShisakuSekkeiBlockInstlVo
            instlVo.ShisakuEventCode = eventCode
            instlDao.DeleteBy(instlVo)
        End Sub

        '''' <summary>
        '''' 試作部品情報を削除する
        '''' </summary>
        '''' <param name="eventCode">試作イベントコード</param>
        '''' <remarks></remarks>
        'Private Sub DeleteShisakuBuhin(ByVal eventCode As String)
        '    Dim buhinVo As New TShisakuBuhinVo
        '    buhinVo.ShisakuEventCode = eventCode
        '    buhinDao.DeleteBy(buhinVo)
        'End Sub

        '''' <summary>
        '''' 試作部品構成情報を削除する
        '''' </summary>
        '''' <param name="eventCode">試作イベントコード</param>
        '''' <remarks></remarks>
        'Private Sub DeleteShisakuBuhinKousei(ByVal eventCode As String)
        '    Dim buhinVo As New TShisakuBuhinKouseiVo
        '    buhinVo.ShisakuEventCode = eventCode
        '    buhinKouseiDao.DeleteBy(buhinVo)
        'End Sub

        ''' <summary>
        ''' 試作部品表編集情報を削除する
        ''' </summary>
        ''' <param name="eventCode">試作イベントコード</param>
        ''' <remarks></remarks>
        Private Sub DeleteShisakuBuhinEdit(ByVal eventCode As String)
            Dim buhinVo As New TShisakuBuhinEditVo
            buhinVo.ShisakuEventCode = eventCode
            buhinEditDao.DeleteBy(buhinVo)
        End Sub

        ''' <summary>
        ''' 試作部品表編集・INSTL情報を削除する
        ''' </summary>
        ''' <param name="eventCode">試作イベントコード</param>
        ''' <remarks></remarks>
        Private Sub DeleteShisakuBuhinEditInstl(ByVal eventCode As String)
            Dim buhinVo As New TShisakuBuhinEditInstlVo
            buhinVo.ShisakuEventCode = eventCode
            buhinEditInstlDao.DeleteBy(buhinVo)
        End Sub

        ''' <summary>
        ''' 試作部品表編集情報（ベース）を削除する
        ''' </summary>
        ''' <param name="eventCode">試作イベントコード</param>
        ''' <remarks></remarks>
        Private Sub DeleteShisakuBuhinEditBase(ByVal eventCode As String)
            Dim buhinVo As New TShisakuBuhinEditBaseVo
            buhinVo.ShisakuEventCode = eventCode
            buhinEditBaseDao.DeleteBy(buhinVo)
        End Sub

        ''' <summary>
        ''' 試作部品表編集・INSTL情報（ベース）を削除する
        ''' </summary>
        ''' <param name="eventCode">試作イベントコード</param>
        ''' <remarks></remarks>
        Private Sub DeleteShisakuBuhinEditInstlBase(ByVal eventCode As String)
            Dim buhinVo As New TShisakuBuhinEditInstlBaseVo
            buhinVo.ShisakuEventCode = eventCode
            buhinEditInstlBaseDao.DeleteBy(buhinVo)
        End Sub

        '---------------------------------------------------------------------
        '２次改修
        '   完成車及び装備仕様の履歴情報も削除する。
        '   イベント管理の履歴用に新設したファイル
        ''' <summary>
        ''' 試作イベント完成車履歴情報を削除する
        ''' </summary>
        ''' <param name="eventCode">試作イベントコード</param>
        ''' <remarks></remarks>
        Private Sub DeleteShisakuEventKanseiRireki(ByVal eventCode As String)
            Dim buhinVo As New TShisakuEventKanseiRirekiVo
            buhinVo.ShisakuEventCode = eventCode
            buhinKanseiRirekiDao.DeleteBy(buhinVo)
        End Sub

        '---------------------------------------------------------------------
        '２次改修
        '   完成車及び装備仕様の履歴情報も削除する。
        '   イベント管理の履歴用に新設したファイル
        ''' <summary>
        ''' 試作イベント装備仕様履歴情報を削除する
        ''' </summary>
        ''' <param name="eventCode">試作イベントコード</param>
        ''' <remarks></remarks>
        Private Sub DeleteShisakuEventSoubiRireki(ByVal eventCode As String)
            Dim buhinVo As New TShisakuEventSoubiRirekiVo
            buhinVo.ShisakuEventCode = eventCode
            buhinSoubiRirekiDao.DeleteBy(buhinVo)
        End Sub

    End Class
End Namespace