Imports EventSakusei.ShisakuBuhinEdit.Logic.Detect
Imports EventSakusei.ShisakuBuhinEdit.Kosei.Logic
Imports EventSakusei.ShisakuBuhinEdit.Kosei.Logic.Matrix
Imports ShisakuCommon.Db.EBom.Exclusion
Imports ShisakuCommon.Db.EBom
Imports EventSakusei.ShisakuBuhinEdit.Al.Dao
Imports EventSakusei.ShisakuBuhinEdit.Al.Logic
Imports ShisakuCommon.Db.Impl
Imports ShisakuCommon.Db.EBom.Dao.Impl
Imports ShisakuCommon.Db
Imports ShisakuCommon.Db.EBom.Dao
Imports ShisakuCommon
Imports ShisakuCommon.Util
Imports ShisakuCommon.Db.EBom.Vo
Imports System.Text

Namespace ShisakuBuhinEdit.Logic
    ''' <summary>
    ''' 試作部品表編集画面の表示全般を担うクラス
    ''' </summary>
    ''' <remarks></remarks>
    Public Class BuhinEditSubject : Inherits Observable
        Private ReadOnly aShisakuDate As New ShisakuDate
        Private ReadOnly exclusionShisakuEvent As New TShisakuEventExclusion

        Private ReadOnly aLoginInfo As LoginInfo
        Private ReadOnly aHeaderSubject As BuhinEditHeaderSubject
        Private ReadOnly anAlSubject As BuhinEditAlSubject
        Private ReadOnly aKoseiSubject As BuhinEditKoseiSubject

        Private ReadOnly anEzSync As EzSyncInstlHinbanImpl

        Public Sub New(ByVal shisakuEventCode As String, ByVal shisakuBukaCode As String, ByVal shisakuBlockNo As String, ByVal aLoginInfo As LoginInfo, ByVal KSMode As Integer)
            Me.aLoginInfo = aLoginInfo
            Me.anEzSync = New EzSyncInstlHinbanImpl

            Dim eventDao As TShisakuEventDao = New TShisakuEventDaoImpl
            Dim blockDao As TShisakuSekkeiBlockDao = New TShisakuSekkeiBlockDaoImpl
            Dim aRhac1560Dao As Rhac1560Dao = New Rhac1560DaoImpl
            Dim aShisakuDao As ShisakuDao = New ShisakuDaoImpl
            Dim aRhac2130Dao As Rhac2130Dao = New Rhac2130DaoImpl
            Dim telDao As TShisakuTelNoDao = New TShisakuTelNoDaoImpl

            aHeaderSubject = New BuhinEditHeaderSubject(shisakuEventCode, shisakuBukaCode, shisakuBlockNo, aLoginInfo, aShisakuDate, eventDao, blockDao, aRhac1560Dao, aRhac2130Dao, aShisakuDao, KSMode, telDao)

            Dim ezTunnel As New EzTunnelAlKoseiImpl
            Dim detectImpl As DetectLatestStructureImpl = New DetectLatestStructureImpl(aHeaderSubject.BlockVo)

            Dim soubiDao As New TShisakuSekkeiBlockSoubiDaoImpl
            Dim soubiShiyoDao As New TShisakuSekkeiBlockSoubiShiyouDaoImpl
            Dim alDao As New BuhinEditAlDaoImpl
            anAlSubject = New BuhinEditAlSubject(aHeaderSubject.BlockVo, aLoginInfo, aShisakuDate, anEzSync, soubiDao, soubiShiyoDao, alDao)

            'イベントコードがいる'
            aKoseiSubject = New BuhinEditKoseiSubject(aHeaderSubject.BlockVo, aLoginInfo, aShisakuDate, ezTunnel, _
                                                      detectImpl, _
                                                      New MakeStructureResultImpl(shisakuEventCode, shisakuBukaCode, shisakuBlockNo), _
                                                      New MakerNameResolverImpl, _
                                                      New TShisakuBuhinEditDaoImpl, _
                                                      New TShisakuBuhinEditInstlDaoImpl)

            anEzSync.Initialize(anAlSubject, aKoseiSubject)
            ezTunnel.Initialize(anAlSubject, aKoseiSubject)
            'ここから部品構成の取得開始'
            'aKoseiSubject.PerformInitialized()
            exclusionShisakuEvent.Save(shisakuEventCode)
            SetChanged()
        End Sub

        Private Class MakerNameResolverImpl : Implements MakerNameResolver
            Private dao As New Rhac0610DaoImpl
            Private cache As New Dictionary(Of String, Rhac0610Vo)
            Public Function Resolve(ByVal makerCode As String) As String Implements MakerNameResolver.Resolve
                If StringUtil.IsEmpty(makerCode) Then
                    Return Nothing
                End If
                If Not cache.ContainsKey(makerCode) Then
                    cache.Add(makerCode, dao.FindByPk(makerCode))
                End If

                Dim result As Rhac0610Vo = cache(makerCode)
                If result Is Nothing Then
                    Return Nothing
                End If
                Return result.MakerName
            End Function
        End Class

        ''' <summary>
        ''' 登録処理
        ''' </summary>
        ''' <remarks></remarks>
        Public Sub Register()
            aHeaderSubject.Memo = Nothing
            Register("登録", AddressOf RegisterMain)
        End Sub
        ''' <summary>
        ''' 登録処理本体
        ''' </summary>
        ''' <remarks></remarks>
        Private Sub RegisterMain()
            aShisakuDate.Clear()
            aHeaderSubject.Register()
            anAlSubject.Register()
            aKoseiSubject.Register()
        End Sub
        ''' <summary>
        ''' 保存処理
        ''' </summary>
        ''' <remarks></remarks>
        Public Sub Save(ByVal memo As String)
            aHeaderSubject.Memo = memo
            Register("保存", AddressOf SaveMain)
        End Sub
        ''' <summary>
        ''' 保存処理本体
        ''' </summary>
        ''' <remarks></remarks>
        Private Sub SaveMain()
            aShisakuDate.Clear()
            aHeaderSubject.Save()
            anAlSubject.Register()
            aKoseiSubject.Register()
        End Sub

        Private Delegate Sub RegisterInner()
        ''' <summary>
        ''' 登録処理
        ''' </summary>
        ''' <param name="processName">処理名</param>
        ''' <param name="aRegisterInner">処理本体のDelegate</param>
        ''' <remarks></remarks>
        Private Sub Register(ByVal processName As String, ByVal aRegisterInner As RegisterInner)
            Using db As New EBomDbClient
                db.BeginTransaction()
                'If exclusionShisakuEvent.WasUpdatedBySomeone() Then
                '    db.Rollback()
                '    Dim userName As String = ResolveUserName(exclusionShisakuEvent.GetUpdatedUserId)
                '    ' TODO メッセージ適当。適宜修正する。
                '    Throw New TableExclusionException(String.Format("このデータは先程 {0} 様に更新されました。" & vbCrLf & "「戻る」から更新内容を確認してください。", userName))
                'End If

                aRegisterInner()

                'exclusionShisakuEvent.UpdateAndSave(aLoginInfo.UserId)

                db.Commit()
            End Using
        End Sub

        ''' <summary>
        ''' ユーザー名を解決する
        ''' </summary>
        ''' <param name="userId">ユーザーID</param>
        ''' <returns>ユーザー名</returns>
        ''' <remarks></remarks>
        Private Function ResolveUserName(ByVal userId As String) As String

            Dim dao As New ShisakuDaoImpl
            Dim userVo As Rhac0650Vo = dao.FindUserById(userId)

            If userVo Is Nothing Then
                Return userId
            End If
            Return userVo.ShainName
        End Function

        ''' <summary>ヘッダー部用のSubject</summary>
        Public ReadOnly Property HeaderSubject() As BuhinEditHeaderSubject
            Get
                Return aHeaderSubject
            End Get
        End Property
        ''' <summary>A/L画面用のSubject</summary>
        Public ReadOnly Property AlSubject() As BuhinEditAlSubject
            Get
                Return anAlSubject
            End Get
        End Property
        ''' <summary>部品構成画面用のSubject</summary>
        Public ReadOnly Property KoseiSubject() As BuhinEditKoseiSubject
            Get
                Return aKoseiSubject
            End Get
        End Property

        ''' <summary>
        ''' ブロックNo変更を、反映する
        ''' </summary>
        ''' <remarks></remarks>
        Public Sub ApplyBlockNo()
            aHeaderSubject.ApplyBlockNo()
            anAlSubject.SupersedeBlockVo(aHeaderSubject.BlockVo)
            aKoseiSubject.SupersedeBlockVo(aHeaderSubject.BlockVo)
            anEzSync.Initialize(anAlSubject, aKoseiSubject)

            Dim ezTunnel As New EzTunnelAlKoseiImpl
            ezTunnel.Initialize(anAlSubject, aKoseiSubject)

            SetChanged()
        End Sub

        '↓↓↓2014/12/25 ブロック№のMT区分を取得 TES)張 ADD BEGIN
        Public Function FindByMTKbnFromRhac0080(ByVal blockNo As String) As String
            Dim sb As New StringBuilder

            With sb
                .Remove(0, .Length)
                .AppendLine("SELECT BLOCK_NO_KINO, ")
                .AppendLine("MT_KBN ")
                .AppendLine("FROM " & RHACLIBF_DB_NAME & ".dbo.RHAC0080 WITH (NOLOCK, NOWAIT) ")
                .AppendLine("WHERE BLOCK_NO_KINO = @Value ")
                .AppendLine("GROUP BY BLOCK_NO_KINO, ")
                .AppendLine("MT_KBN ")
                .AppendLine("ORDER BY BLOCK_NO_KINO, ")
                .AppendLine("MT_KBN ")
            End With

            Dim db As New EBomDbClient
            Dim rhac0080Vos As List(Of Rhac0080Vo) = db.QueryForList(Of Rhac0080Vo)(sb.ToString, blockNo)

            If rhac0080Vos.Count > 0 Then
                If StringUtil.IsEmpty(rhac0080Vos(0).MtKbn) Then
                    rhac0080Vos.RemoveAt(0)
                End If
                If rhac0080Vos.Count > 0 Then
                    Return rhac0080Vos(0).MtKbn
                Else
                    Return String.Empty
                End If
            Else
                Return String.Empty
            End If
        End Function
        '↑↑↑2014/12/25 ブロック№のMT区分を取得 TES)張 ADD END
    End Class
End Namespace