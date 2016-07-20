Imports ShisakuCommon
Imports ShisakuCommon.Db.EBom.Exclusion
Imports EventSakusei.ShisakuBuhinEditBlock.Dao
Imports ShisakuCommon.Db.EBom.Vo
Imports EventSakusei.ShisakuBuhinEdit.Al.Dao
Imports EventSakusei.ShisakuBuhinEdit.Logic.Detect
Imports ShisakuCommon.Db.EBom
Imports EventSakusei.ShisakuBuhinEdit.Logic
Imports ShisakuCommon.Db.EBom.Dao
Imports ShisakuCommon.Db.EBom.Dao.Impl
Imports ShisakuCommon.Db
Imports ShisakuCommon.Db.Impl
Imports EventSakusei.ShisakuBuhinEdit.Kosei.Logic.Matrix

Namespace ShisakuBuhinEditBlock.Logic.Tenkai
    '構成用(実験クラス)'
    Public Class BuhinEditTenkaiSubject

        Private ReadOnly aShisakuDate As New ShisakuDate
        Private ReadOnly exclusionShisakuEvent As New TShisakuEventExclusion

        Private ReadOnly aLoginInfo As LoginInfo
        Private ReadOnly anAlSubject As BuhinEditInstlSupplier
        Private ReadOnly aKoseiSubject As BuhinEditBaseSupplier

        Private ReadOnly anEzSync As BuhinEditInstlHinbanBlock
        Private impl As BuhinEditBaseDao
        Private sbVoList As List(Of TShisakuSekkeiBlockVo)
        'Private sbVoListB As List(Of TShisakuSekkeiBlockVo)
        Private eventCode As String
        '
        Private blockNoList As List(Of String)
        Private strBukaCode As String

        '
        Private buhinEditDao As TShisakuBuhinEditDao = New TShisakuBuhinEditDaoImpl
        Private buhinEditInstlDao As TShisakuBuhinEditInstlDao = New TShisakuBuhinEditInstlDaoImpl
        Private buhinEditBaseDao As TShisakuBuhinEditBaseDao = New TShisakuBuhinEditBaseDaoImpl
        Private buhinEditInstlBaseDao As TShisakuBuhinEditInstlBaseDao = New TShisakuBuhinEditInstlBaseDaoImpl

        ''' <summary>
        ''' 試作部品表編集情報を削除する
        ''' </summary>
        ''' <param name="eventCode">試作イベントコード</param>
        ''' <param name="bukaCode">試作部課コード</param>
        ''' <param name="strBlockNo">試作ブロック№</param>
        ''' <param name="strBlockNoKaiteiNo">試作ブロック№改訂№</param>
        ''' <remarks></remarks>
        Private Sub DeleteShisakuBuhinEdit(ByVal eventCode As String, _
                                           ByVal bukaCode As String, _
                                           ByVal strBlockNo As String, _
                                           ByVal strblockNoKaiteiNo As String)
            Dim buhinVo As New TShisakuBuhinEditVo
            buhinVo.ShisakuEventCode = eventCode
            buhinVo.ShisakuBukaCode = bukaCode
            buhinVo.ShisakuBlockNo = strBlockNo
            buhinVo.ShisakuBlockNoKaiteiNo = strblockNoKaiteiNo
            buhinEditDao.DeleteBy(buhinVo)
        End Sub

        ''' <summary>
        ''' 試作部品表編集・INSTL情報を削除する
        ''' </summary>
        ''' <param name="eventCode">試作イベントコード</param>
        ''' <param name="bukaCode">試作部課コード</param>
        ''' <param name="strBlockNo">試作ブロック№</param>
        ''' <param name="strBlockNoKaiteiNo">試作ブロック№改訂№</param>
        ''' <remarks></remarks>
        Private Sub DeleteShisakuBuhinEditInstl(ByVal eventCode As String, _
                                                ByVal bukaCode As String, _
                                                ByVal strBlockNo As String, _
                                                ByVal strblockNoKaiteiNo As String)
            Dim buhinVo As New TShisakuBuhinEditInstlVo
            buhinVo.ShisakuEventCode = eventCode
            buhinVo.ShisakuBukaCode = bukaCode
            buhinVo.ShisakuBlockNo = strBlockNo
            buhinVo.ShisakuBlockNoKaiteiNo = strblockNoKaiteiNo
            buhinEditInstlDao.DeleteBy(buhinVo)
        End Sub

        ''' <summary>
        ''' 試作部品表編集情報（ベース）を削除する
        ''' </summary>
        ''' <param name="eventCode">試作イベントコード</param>
        ''' <param name="bukaCode">試作部課コード</param>
        ''' <param name="strBlockNo">試作ブロック№</param>
        ''' <param name="strBlockNoKaiteiNo">試作ブロック№改訂№</param>
        ''' <remarks></remarks>
        Private Sub DeleteShisakuBuhinEditBase(ByVal eventCode As String, _
                                               ByVal bukaCode As String, _
                                               ByVal strBlockNo As String, _
                                               ByVal strblockNoKaiteiNo As String)
            Dim buhinVo As New TShisakuBuhinEditBaseVo
            buhinVo.ShisakuEventCode = eventCode
            buhinVo.ShisakuBukaCode = bukaCode
            buhinVo.ShisakuBlockNo = strBlockNo
            buhinVo.ShisakuBlockNoKaiteiNo = strblockNoKaiteiNo
            buhinEditBaseDao.DeleteBy(buhinVo)
        End Sub

        ''' <summary>
        ''' 試作部品表編集・INSTL情報（ベース）を削除する
        ''' </summary>
        ''' <param name="eventCode">試作イベントコード</param>
        ''' <param name="bukaCode">試作部課コード</param>
        ''' <param name="strBlockNo">試作ブロック№</param>
        ''' <param name="strBlockNoKaiteiNo">試作ブロック№改訂№</param>
        ''' <remarks></remarks>
        Private Sub DeleteShisakuBuhinEditInstlBase(ByVal eventCode As String, _
                                                    ByVal bukaCode As String, _
                                                    ByVal strBlockNo As String, _
                                                    ByVal strblockNoKaiteiNo As String)
            Dim buhinVo As New TShisakuBuhinEditInstlBaseVo
            buhinVo.ShisakuEventCode = eventCode
            buhinVo.ShisakuBukaCode = bukaCode
            buhinVo.ShisakuBlockNo = strBlockNo
            buhinVo.ShisakuBlockNoKaiteiNo = strblockNoKaiteiNo
            buhinEditInstlBaseDao.DeleteBy(buhinVo)
        End Sub

        Public Sub New(ByVal shisakuEventCode As String, ByVal strBukaCode As String, ByVal blockNoList As List(Of String))
            Me.aLoginInfo = aLoginInfo
            Me.anEzSync = New BuhinEditInstlHinbanBlock
            impl = New BuhinEditBaseDaoImpl
            '
            eventCode = shisakuEventCode
            '
            Me.strBukaCode = strBukaCode
            Me.blockNoList = blockNoList

            'ブロック№分、繰り返す。
            For Each v As String In blockNoList

                '---------------------------------------------------------------------------------------
                'ブロック№と改訂№を分割する。
                Dim foundIndex As Integer = v.IndexOf(":")
                Dim strBlockNo As String = Left(v, foundIndex)
                Dim strBlockNoKaiteiNo As String = Right(v, foundIndex - 1)

                '---------------------------------------------------------------------------------------
                '前回のブロックの部品構成情報を削除する。
                '    試作部品編集情報を削除'
                DeleteShisakuBuhinEdit(eventCode, strBukaCode, strBlockNo, strBlockNoKaiteiNo)
                '    試作部品表編集・INSTL情報を削除'
                DeleteShisakuBuhinEditInstl(eventCode, strBukaCode, strBlockNo, strBlockNoKaiteiNo)
                'ベースの削除処理は不要
                ''    試作部品編集情報（ベース）を削除'
                'DeleteShisakuBuhinEditBase(eventCode, strBukaCode, strBlockNo, strBlockNoKaiteiNo)
                ''    試作部品表編集・INSTL情報（ベース）を削除'
                'DeleteShisakuBuhinEditInstlBase(eventCode, strBukaCode, strBlockNo, strBlockNoKaiteiNo)
                '---------------------------------------------------------------------------------------

                '試作イベントコード、部課コード、ブロック№で
                sbVoList = New List(Of TShisakuSekkeiBlockVo)
                sbVoList = impl.FindBySekkeiBlock(shisakuEventCode, strBukaCode, strBlockNo, strBlockNoKaiteiNo)

                Dim eventDao As TShisakuEventDao = New TShisakuEventDaoImpl
                Dim blockDao As TShisakuSekkeiBlockDao = New TShisakuSekkeiBlockDaoImpl
                Dim aRhac1560Dao As Rhac1560Dao = New Rhac1560DaoImpl
                Dim aShisakuDao As ShisakuDao = New ShisakuDaoImpl
                Dim aRhac2130Dao As Rhac2130Dao = New Rhac2130DaoImpl

                Dim ezTunnel As New BuhinEditEzTunnelBlock

                For Each AVo As TShisakuSekkeiBlockVo In sbVoList

                    Dim alDao As New BuhinEditAlDaoImpl
                    anAlSubject = New BuhinEditInstlSupplier(AVo, aLoginInfo, aShisakuDate, anEzSync, Nothing, Nothing, alDao)

                    'イベントコードがいる'
                    aKoseiSubject = New BuhinEditBaseSupplier(AVo, aLoginInfo, aShisakuDate, ezTunnel, _
                                                              Nothing, _
                                                              New MakeStructureResultImpl(shisakuEventCode, AVo.ShisakuBukaCode, AVo.ShisakuBlockNo), _
                                                              New MakerNameResolverImpl, _
                                                              New TShisakuBuhinEditDaoImpl, _
                                                              New TShisakuBuhinEditInstlDaoImpl, _
                                                              New ShisakuSekkeiBlockInstlDaoImpl)

                    anEzSync.Initialize(anAlSubject, aKoseiSubject)
                    ezTunnel.Initialize(anAlSubject, aKoseiSubject)
                    'ここから部品構成の取得開始'
                    aKoseiSubject.PerformInitialized()

                    exclusionShisakuEvent.Save(shisakuEventCode)
                Next

            Next

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
            Register("登録", AddressOf RegisterMain)
        End Sub
        ''' <summary>
        ''' 登録処理本体
        ''' </summary>
        ''' <remarks></remarks>
        Private Sub RegisterMain()
            aShisakuDate.Clear()
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
                If exclusionShisakuEvent.WasUpdatedBySomeone() Then
                    db.Rollback()
                    Dim userName As String = ResolveUserName(exclusionShisakuEvent.GetUpdatedUserId)
                    ' TODO メッセージ適当。適宜修正する。
                    Throw New TableExclusionException(String.Format("このデータは先程 {0} 様に更新されました。" & vbCrLf & "「戻る」から更新内容を確認してください。", userName))
                End If

                aRegisterInner()

                exclusionShisakuEvent.UpdateAndSave(aLoginInfo.UserId)

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

        ''' <summary>A/L画面用のSubject</summary>
        Public ReadOnly Property AlSubject() As BuhinEditInstlSupplier
            Get
                Return anAlSubject
            End Get
        End Property
        ''' <summary>部品構成画面用のSubject</summary>
        Public ReadOnly Property KoseiSubject() As BuhinEditBaseSupplier
            Get
                Return aKoseiSubject
            End Get
        End Property

    End Class
End Namespace